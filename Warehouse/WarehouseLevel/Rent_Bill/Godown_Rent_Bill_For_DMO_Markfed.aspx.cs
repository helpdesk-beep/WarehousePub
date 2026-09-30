using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using MPSCSC_WS;

public partial class WarehouseLevel_Rent_Bill_Godown_Rent_Bill_For_DMO_Markfed : System.Web.UI.Page
{
    string NetAmountWord = "";
    string Godown_Id = "";
    string Bill_No = "";
    int BID = 0;
    int Days_In_Month = 0;
    decimal Total_Charge = 0;
    decimal Rate_M = 0;
    string CFnancialYear = "";
    string Bill_Type = "";
    int TotalDays = 0;
    int lastDayOfMonth = 0;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    string qry = "";
    decimal TT1;

    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["GodownID_New"] != null)
        {
            if (!IsPostBack)
            {
                Get_Godown_Type();
                fillFinancialYear();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void Get_Rate()
    {
        //Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
        int Month_No = Convert.ToInt32(ddlmonth.SelectedValue);
        if (Month_No == 1 || Month_No == 3 || Month_No == 5 || Month_No == 7 || Month_No == 8 || Month_No == 10 || Month_No == 12)
        {
            Days_In_Month = 31;
        }
        else if (Month_No == 2)
        {
            //Days_In_Month = 29;
            Days_In_Month = 28;
        }
        else
        {
            Days_In_Month = 30;
        }
        //Rate_M = 68;
        Rate_M = 74;
        txtcomrate.Text = Rate_M.ToString();
        //
        string Actual_Rate = "";
        string Value = (Rate_M / Days_In_Month).ToString();
        int U_PerDayRateCount = Value.Length;
        if (U_PerDayRateCount >= 6)
        {
            string PerDayRate = Value;
            int I = PerDayRate.IndexOf(".");

            string U_PerDayRate1 = PerDayRate.Substring(0, I);
            string U_PerDayRate = PerDayRate.Substring(I, I + 5);
            Actual_Rate = U_PerDayRate1 + U_PerDayRate;
        }
        else
        {
            //Actual_Rate = Math.Round((Rate_M / Days_In_Month), 4).ToString();
            Actual_Rate = (Rate_M / Days_In_Month).ToString();
        }
        txtCPRate.Text = Actual_Rate;
        ViewState["Days_In_Month"] = Days_In_Month;
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["G_BranchID"].ToString();
        ddlgodown.Items.Clear();
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["G_BranchID"].ToString() + "' and Hired_Type in ('Joint Venture(JV)','JointVenture(JV)','WDRA') and Godown_ID='"+ Session["GodownID_New"] + "'";
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["G_BranchID"].ToString() + "' and Godown_ID='" + Session["GodownID_New"] + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.SelectedIndex = 1;
        }
    }
    void GetCommodity()
    {
        qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Commodity_Id in ('13', '22', '25', '92') AND Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_ID";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Select--");
        }
    }
    void GetCropYear()
    {
        qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlCropYear.DataSource = ds.Tables[0];
            ddlCropYear.DataTextField = "CropYear";
            ddlCropYear.DataValueField = "CropYear";
            ddlCropYear.DataBind();
            ddlCropYear.Items.Insert(0, "--Select--");
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public void GetStorageDailyChargesBillDetail()
    {
        try
        {
            CFnancialYear = ViewState["CFnancialYear"].ToString();
            int ContMD = 0;
            //ContMD = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            ContMD = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            DateTime LDateTime = new DateTime();
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            string SD = "";
            string ED = "";
            if (Convert.ToInt32(ddlmonth.SelectedValue) <= 9)
            {
                SD = "01/" + "0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }
            else
            {
                SD = "01/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }

            StartDate = Convert.ToDateTime(getDate_MDY(SD));
            EndDate = Convert.ToDateTime(getDate_MDY(ED));

            decimal PerDayRate = 0;
            if (txtCPRate.Text != "")
            {
                PerDayRate = Convert.ToDecimal(txtCPRate.Text);
            }
            int j = 0;
            int h = 0;
            int m = 0;
            DataTable dw = new DataTable();
            dw.Columns.AddRange(new DataColumn[]  {
                 new DataColumn("Static_Date",typeof(DateTime)),
              new DataColumn("Deposit_Bags",typeof(decimal),null),
                new DataColumn("Deliver_Bags",typeof(decimal)),
                new DataColumn("Deposit_Weight",typeof(decimal)),
                new DataColumn("Deliver_Weight",typeof(decimal)),
                new DataColumn("Godown_Id",typeof(string)),
                new DataColumn("Flag",typeof(string)),
            });

            DateTime Static_date = new DateTime();
            DateTime Recent_Static_date = new DateTime();
            DataTable ddt2 = new DataTable();

            ddt2.Columns.AddRange(new DataColumn[]  {
          new DataColumn("Static_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

            new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
        //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
        });

            DataTable ddt3 = new DataTable();
            ddt3.Columns.AddRange(new DataColumn[]  {
          new DataColumn("Deposit_Date",typeof(DateTime),null),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),

             new DataColumn("Opening_Weight",typeof(decimal)),
            new DataColumn("Receive_Weight",typeof(decimal)),
            new DataColumn("Issue_Weight",typeof(decimal)),
            new DataColumn("Closing_Weight",typeof(decimal)),
        new DataColumn("Per_Day_Rate",typeof(decimal)),
        new DataColumn("Charges",typeof(decimal)),
         //new DataColumn("Weight_Charges",typeof(decimal)),
         new DataColumn("Godown_Id",typeof(string)),
        });

            string BranchID = Session["G_BranchID"].ToString();
            string str = "";
            if ((ddlCropYear.SelectedItem.Text == "2019-20") && ddlcomodity.SelectedValue == "22")
            {
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 where BranchID='" + BranchID + "')";
                //Valid
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 where GodownID='" + ddlgodown.SelectedValue.ToString() + "')";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='DMO Markfed' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";
                //str = "select * from [tbl_View_DateWiseDepositDelivery]";


            }
            else if ((ddlCropYear.SelectedItem.Text == "2020-21") && ddlcomodity.SelectedValue == "22")
            {

                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2020 where GodownID='" + ddlgodown.SelectedValue.ToString() + "')";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='DMO Markfed' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";
            }
            else
            {
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='DMO Markfed' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";

            }
            //string str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght+Loss-Gain) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],DATEPART(MONTH,[datecom]) as Month_No,DATEPART(YEAR,[datecom]) as Year,[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [View_DateWiseDepositDelivery] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int iss = 0;
                Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                if (StartDate < Static_date)
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    //trReportsView.Visible = false;

                    //Static_date = Convert.ToDateTime(dt.Rows[iss]["datecom"].ToString());
                    Recent_Static_date = StartDate;
                    int DateDiff = Convert.ToInt32((Static_date - StartDate).Days);
                    for (iss = 0; iss <= DateDiff - 1; iss++)
                    {

                        dw.Rows.Add();
                        ////            
                        dw.Rows[h]["Static_date"] = Convert.ToDateTime(Recent_Static_date);
                        dw.Rows[h]["Deposit_Bags"] = 0;
                        dw.Rows[h]["Deliver_Bags"] = 0;
                        dw.Rows[h]["Deposit_Weight"] = 0;
                        dw.Rows[h]["Deliver_Weight"] = 0;
                        dw.Rows[h]["Godown_Id"] = "0";
                        dw.Rows[h]["Flag"] = "Y";
                        Recent_Static_date = Recent_Static_date.AddDays(1);
                        h++;
                    }
                    Recent_Static_date = Recent_Static_date.AddDays(-1);
                    //h--;
                }

                {
                    {
                        {
                            int i = 0;
                            for (i = 0; i <= dt.Rows.Count - 1; i++)
                            {

                                Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());

                                if (i == 0)
                                {
                                    Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                                    dw.Rows.Add();
                                    //            
                                    dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
                                    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                    dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                    dw.Rows[h]["Flag"] = "Y";
                                    Recent_Static_date = Static_date;
                                    h++;

                                }
                                else if (Recent_Static_date == Static_date)
                                {
                                    h--;
                                    Static_date = Recent_Static_date;
                                    dw.Rows.Add();

                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Bags"]) + Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Bags"]) + Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                    if (dw.Rows[h]["Godown_Id"].ToString().Contains(dt.Rows[i]["Godown_Id"].ToString()))
                                    {

                                    }
                                    else
                                    {
                                        dw.Rows[h]["Godown_Id"] = dw.Rows[h]["Godown_Id"].ToString() + "," + dt.Rows[i]["Godown_Id"].ToString();
                                    }
                                    dw.Rows[h]["Flag"] = "Y";
                                    Recent_Static_date = Static_date;
                                    h++;
                                }
                                else if (Recent_Static_date.AddDays(1) == Static_date)
                                {

                                    dw.Rows.Add();

                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                    dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                    dw.Rows[h]["Flag"] = "Y";
                                    Recent_Static_date = Static_date;
                                    h++;
                                }

                                else if (Recent_Static_date != Static_date)
                                {
                                    dw.Rows.Add();
                                    Static_date = Recent_Static_date.AddDays(1);
                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = 0;
                                    dw.Rows[h]["Deliver_Bags"] = 0;
                                    dw.Rows[h]["Deposit_Weight"] = 0;
                                    dw.Rows[h]["Deliver_Weight"] = 0;
                                    dw.Rows[h]["Godown_Id"] = "";
                                    dw.Rows[h]["Flag"] = "Y";
                                    h++;
                                    i--;
                                    Recent_Static_date = Static_date;
                                }
                                else if (Recent_Static_date == Static_date && (dt.Rows[i]["recbags"].ToString() == "" && dt.Rows[i]["delbags"].ToString() == ""))
                                {
                                    dw.Rows.Add();
                                    Static_date = Static_date.AddDays(1);
                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = 0;
                                    dw.Rows[h]["Deliver_Bags"] = 0;
                                    dw.Rows[h]["Deposit_Weight"] = 0;
                                    dw.Rows[h]["Deliver_Weight"] = 0;
                                    dw.Rows[h]["Godown_Id"] = "";
                                    dw.Rows[h]["Flag"] = "Y";
                                    h++;
                                    i--;
                                    Recent_Static_date = Static_date;
                                }
                                else if (Recent_Static_date != Static_date)
                                {
                                    dw.Rows.Add();
                                    Static_date = Recent_Static_date.AddDays(1);
                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                                    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                                    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                                    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                                    dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                                    dw.Rows[h]["Flag"] = "Y";
                                    Recent_Static_date = Static_date;
                                    h++;
                                }
                            }
                        }
                        for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 3000; i++)
                        {
                            if (i == (dt.Rows.Count - 1))
                            {
                                LDateTime = Convert.ToDateTime(dw.Rows[h - 1]["Static_date"]);
                                LDateTime = LDateTime.AddDays(1);
                            }
                            else
                            {
                                LDateTime = LDateTime.AddDays(1);
                            }
                            dw.Rows.Add();
                            dw.Rows[h]["Static_date"] = LDateTime;
                            dw.Rows[h]["Deposit_Bags"] = 0;
                            dw.Rows[h]["Deliver_Bags"] = 0;
                            dw.Rows[h]["Deposit_Weight"] = 0;
                            dw.Rows[h]["Deliver_Weight"] = 0;
                            dw.Rows[h]["Godown_Id"] = "";
                            dw.Rows[h]["Flag"] = "Y";
                            h++;
                        }
                    }
                    if (dw.Rows.Count > 0)
                    {
                        for (int k = 0; k <= dw.Rows.Count - 1; k++)
                        {
                            if (k == 0 && dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = 0;
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Opening_Weight"] = 0;
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) / 10;
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]) / 10;
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) / 10 - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]) / 10;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();

                            }
                            else if (dw.Rows[k]["Flag"].ToString() == "Y")
                            {
                                ddt2.Rows.Add();
                                ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                                ddt2.Rows[k]["Opening_Balance"] = ddt2.Rows[k - 1]["Closing_Balance"];
                                ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
                                ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
                                ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Balance"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);

                                ddt2.Rows[k]["Opening_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]);
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) / 10;
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]) / 10;
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) / 10 - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]) / 10;
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);

                                //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
                                ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                    if (ddt2.Rows.Count > 0)
                    {
                        for (int l = 0; l <= ddt2.Rows.Count - 1; l++)
                        {
                            DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                            DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
                            if (dt1 >= StartDate && dt1 <= EndDate)
                            {

                                ddt3.Rows.Add();
                                ddt3.Rows[m]["Deposit_Date"] = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                                ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                ddt3.Rows[m]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Closing_Balance"]);
                                //ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
                                ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
                                // ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Charges"]);
                                ddt3.Rows[m]["Charges"] = Math.Round(Convert.ToDecimal(ddt2.Rows[l]["Charges"]), 2);

                                //ddt3.Rows[m]["Opening_Weight"] =  Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"])),2);
                                //ddt3.Rows[m]["Receive_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"])), 2);
                                //ddt3.Rows[m]["Issue_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"])), 2);
                                //ddt3.Rows[m]["Closing_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"])), 2);
                                //ddt3.Rows[m]["Weight_Charges"] = Math.Round(Convert.ToDecimal(ddt2.Rows[l]["Weight_Charges"]), 2);

                                ddt3.Rows[m]["Opening_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]));
                                ddt3.Rows[m]["Receive_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]));
                                ddt3.Rows[m]["Issue_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]));
                                ddt3.Rows[m]["Closing_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]));
                                //ddt3.Rows[m]["Weight_Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Weight_Charges"]);
                                ddt3.Rows[m]["Godown_Id"] = dw.Rows[l]["Godown_Id"].ToString();

                                m = m + 1;
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }

                    if (ddt3.Rows.Count > 0)
                    {
                        //if (ddt3.Rows.Count > 30)
                        //{

                        //    DataRow dr = ddt3.Rows[30];
                        //    //if (dr["name"] == "abc")
                        //    dr.Delete();

                        //}
                        gvIStorageCharge.DataSource = ddt3;
                        gvIStorageCharge.DataBind();
                        gvIStorageCharge.HeaderRow.Cells[1].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[2].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[3].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[4].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[5].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[12].Visible = false;
                        for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                        {
                            gvIStorageCharge.Rows[i].Cells[1].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[2].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[3].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[4].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[5].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[12].Visible = false;
                        }
                        trRentBill.Visible = true;
                        //trReportsView.Visible = false;
                        btnGenBill.Visible = true;
                        btncancel2.Visible = true;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        string BranchID = Session["G_BranchID"].ToString();
        //if (ddlGodownType.SelectedValue.ToString() != "1")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Type as JVS...'); </script> ");
        //} 
        if (ddlgodown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown...'); </script> ");
        }
        else if (ddlcomodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
        }
        else if (ddlCropYear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        }
        else if (ddlmonth.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        }
        else if (txtcomrate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Monthly Rate...'); </script> ");
        }
        else if (txtCPRate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Per Day Rate...'); </script> ");
        }
        else if (ddlFyear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Financial Year...'); </script> ");
        }
        else
        {
            trReportsView.Visible = false;
            GetStorageDailyChargesBillDetail();
        }
    }
    protected void brnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btnGenBill_Click(object sender, EventArgs e)
    {
        if (CheckDupliCate())
        {
            if (Convert.ToDecimal(hdnAmt.Value) > 0)
            {
                GetStorageBillNo();
                Insert_Godown_Rent_Daily_Detail();
                Insert_Bill_Detail();
                Lblmsg2.Text = "Your Bill Successfully Generated. Bill No : " + Bill_No;
                Session["Bill_No"] = Bill_No;
                btnGenBill.Enabled = false;
                btnGenBill.Enabled = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Your Bill Successfully Generated ,Godown Rent Bill No. is =" + Bill_No + " '); window.location =('frm_Gdwn_RentBillDeduction_BM.aspx');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Your Bill Successfully Generated ,Godown Rent Bill No. is =" + Bill_No + "');", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Zero Amount का बिल नहीं बना सकते हैं ');</script>");
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Bill Already Generated...!');</script>");
        }
    }
    public void Insert_Bill_Detail()
    {
        try
        {
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            string SD = "";
            string ED = "";

            string qry = "";
            Bill_No = ViewState["BillNo"].ToString();
            string Bill_Type = "";
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString();
            string BranchID = Session["G_BranchID"].ToString();
            BID = Convert.ToInt32(ViewState["BID"]);


            /////////////////////Get From and To Date////////////////
            CFnancialYear = ViewState["CFnancialYear"].ToString();
            int ContMD = 0;
            ContMD = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));

            if (Convert.ToInt32(ddlmonth.SelectedValue) <= 9)
            {
                SD = "01/" + "0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }
            else
            {
                SD = "01/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }

            StartDate = Convert.ToDateTime(getDate_MDY(SD));
            EndDate = Convert.ToDateTime(getDate_MDY(ED));
            /////////////////////Get From and To Date////////////////
            decimal Rount_Total_Charges = Math.Round(Total_Charge);
            Total_Charge = Rount_Total_Charges;

            string CropYear = ddlCropYear.SelectedItem.Text;
            string FinYear = ddlFyear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            FinYear = NineCrop(FinYear);

            //qry = "INSERT INTO tbl_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + CFnancialYear + "'," + txtcomrate.Text + ",0," + Total_Charge + ",0,0,'',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "')";
            if (ddlGodownType.SelectedValue.ToString() == "1" || ddlGodownType.SelectedValue.ToString() == "3" || ddlGodownType.SelectedValue.ToString() == "4" || ddlGodownType.SelectedValue.ToString() == "5" || ddlGodownType.SelectedValue.ToString() == "15")
            {
                Godown_Id = ddlgodown.SelectedValue.ToString();
                Bill_Type = "GR";
                //qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_For_NAFED(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Godown_Id,Crop_Year) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','129','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + ddlFyear.SelectedItem.Text + "'," + txtcomrate.Text + "," + Total_Charge + "," + Total_Charge + ",0,0,'PVT',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "','" + Godown_Id + "','" + ddlCropYear.SelectedItem.Text + "')";
                qry = "INSERT INTO tbl_Institution_Rent_Bill_Details_For_DMO_Markfed(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Godown_Id,Crop_Year) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','4679','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + FinYear + "'," + txtcomrate.Text + "," + Total_Charge + "," + Total_Charge + ",0,0,'PVT',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "','" + Godown_Id + "','" + CropYear + "')";
            }
            else if (ddlGodownType.SelectedValue.ToString() == "2")
            {
                //Godown_Id = ddlGodown2.SelectedValue.ToString();
                //Bill_Type = "HG";
                Godown_Id = ddlgodown.SelectedValue.ToString();
                Bill_Type = "HG";
                //qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_For_NAFED(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Godown_Id) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + ddlFyear.SelectedItem.Text + "'," + txtcomrate.Text + "," + Total_Charge + "," + Total_Charge + ",0,0,'',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "','" + Godown_Id + "')";
                //qry = "INSERT INTO tbl_Institution_Storage_Bill_Details_For_NAFED(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Godown_Id,Crop_Year) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + ddlFyear.SelectedItem.Text + "'," + txtcomrate.Text + "," + Total_Charge + "," + Total_Charge + ",0,0,'',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "','" + Godown_Id + "','" + ddlCropYear.SelectedItem.Text + "')";
                qry = "INSERT INTO tbl_Institution_Rent_Bill_Details_For_DMO_Markfed(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Godown_Id,Crop_Year) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','','','" + ddlcomodity.SelectedValue.ToString() + "','" + StartDate + "','" + EndDate + "','','','" + FinYear + "'," + txtcomrate.Text + "," + Total_Charge + "," + Total_Charge + ",0,0,'PVT',0,0,0,'','N',getdate(),'','" + ip + "','" + txtCPRate.Text + "','','','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue.ToString() + "','" + Godown_Id + "','" + CropYear + "')";
            }
            string DepositorId = "";
            if (Bill_Type != "HG")
            {
                DepositorId = "4679";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            //MPSCSCDemo.EDAddInstitutionStorageBillDetailsForGodownRent(Bill_No, Dist_id, BranchID, "", DepositorId, ddlcomodity.SelectedValue.ToString()
            //, StartDate.ToString(), EndDate.ToString(), "", "", FinYear, Convert.ToDecimal(txtcomrate.Text)
            //, Total_Charge, Total_Charge, 0, 0, "PVT", 0, 0, 0, "", "", ""
            //, "N", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, Convert.ToDecimal(txtCPRate.Text)
            //, Bill_Type, BID, Convert.ToInt32(ddlmonth.SelectedValue), Godown_Id, CropYear);

            con.Close();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    public void GetStorageBillNo()
    {
        string CropYear = ddlCropYear.SelectedItem.Text;
        string FinYear = ddlFyear.SelectedItem.Text;
        CropYear = NineCrop(CropYear);
        FinYear = NineCrop(FinYear);
        string BranchID = Session["G_BranchID"].ToString();
        if (ddlGodownType.SelectedValue.ToString() == "1" || ddlGodownType.SelectedValue.ToString() == "3" || ddlGodownType.SelectedValue.ToString() == "4" || ddlGodownType.SelectedValue.ToString() == "5" || ddlGodownType.SelectedValue.ToString() == "13" || ddlGodownType.SelectedValue.ToString() == "15")
        {
            Bill_Type = "GR";
            Godown_Id = ddlgodown.SelectedValue.ToString();

            qry = "select max(BId) as BId from tbl_Institution_Rent_Bill_Details_For_DMO_Markfed where branch_Id='" + BranchID + "' and Financial_Year='" + FinYear + "' and Bill_Type='" + Bill_Type + "'";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            Bill_Type = "HG";
            //Godown_Id = ddlGodown2.SelectedValue.ToString();
            Godown_Id = ddlgodown.SelectedValue.ToString();
            //qry = "select max(BId) as BId from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Financial_Year='" + ddlFyear2.SelectedItem.Text + "' and Bill_Type='" + Bill_Type + "'";
            qry = "select max(BId) as BId from tbl_Institution_Rent_Bill_Details_For_DMO_Markfed where branch_Id='" + BranchID + "' and Financial_Year='" + FinYear + "' and Bill_Type='" + Bill_Type + "'";

        }
        string Godown_Type = ddlGodownType.SelectedValue.ToString();
        //Month Detail
        string MonthSub = "";
        string MonthStr = ddlmonth.SelectedValue;
        int Month = Convert.ToInt32(ddlmonth.SelectedValue);
        int MonthLen = MonthStr.Length;
        if (MonthLen == 1)
        {
            MonthSub = "0" + MonthStr;
        }
        else
        {
            MonthSub = MonthStr;
        }
        //CFnancialYear = ViewState["CFnancialYear"].ToString();

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = BID + 1;
                Bill_No = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + Godown_Type + "4679"  + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + Godown_Type + "4679" + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + Godown_Type + "4679" + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
    }
    public void Insert_Godown_Rent_Daily_Detail()
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dates = "";
            //DateTime Dates = new DateTime();
            decimal Opening_Balance = 0;
            decimal Rec_Bags = 0;
            decimal Issue_Bags = 0;
            decimal Closing_Balance = 0;
            decimal Per_Day_Rate = 0;
            //decimal Total_Charges = 0;

            //string Godown_Id = "";
            decimal Charges = 0;
            decimal Opening_Weight = 0;
            decimal Rec_Weight = 0;
            decimal Issue_Weight = 0;
            decimal Closing_Weight = 0;
            string Bill_Type = "";

            if (gvIStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                {
                    Dates = getDate_MDY(gvIStorageCharge.Rows[i].Cells[0].Text.ToString());
                    Opening_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[4].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[10].Text);
                    Charges = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[12].Text);
                    Opening_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[6].Text);
                    Rec_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[7].Text);
                    Issue_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[8].Text);
                    Closing_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[9].Text);
                    //string qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    //string qry = "INSERT INTO tbl_Bill_Godown_JVS_Daily_Rent(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Bag_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    string qry = "INSERT INTO tbl_Bill_PVT_Godown_Daily_Rent__DMO_Markfed(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Bag_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //MPSCSCDemo.EDAddBillPVTGodownDailyRentCharges(ViewState["BillNo"].ToString(), Dates, Opening_Balance, Rec_Bags, Issue_Bags, Closing_Balance, Per_Day_Rate, Charges, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Opening_Weight, Rec_Weight, Issue_Weight, Closing_Weight);
                    con.Close();
                    Total_Charge = Total_Charge + Charges;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    public string Convert_To_Word(decimal Number)
    {
        string Word = "";
        if (Number != 0)
        {
            string NTWqry = "select dbo.AnkNumberToWords('" + Number + "')";    // Convert amount in word
            SqlCommand cmd2 = new SqlCommand(NTWqry, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                Word = dt.Rows[0]["Column1"].ToString();
            }
        }
        else
        {
            Word = "Zero Rupees.";
        }
        return Word;
    }
    protected void btnGenBill_Click1(object sender, EventArgs e)
    {

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(ddlcomodity.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
        //}
        //else if (ddlmonth.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        //}
        //else if (ddlyear.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Year...'); </script> ");
        //}
        //else if (ddlGodownType.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Type...'); </script> ");
        //}
        //else
        //{
        GetCommodity();
        //Get_Rate();
        txtcomrate.ReadOnly = false;
        txtCPRate.ReadOnly = false;
        //}
        trRentBill.Visible = false;
        trReportsView.Visible = false;
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCommodity();
        trRentBill.Visible = false;
        trReportsView.Visible = false;
    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
    }
    protected void fillFinancialYear()
    {

        ddlFyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        //ddlFyear.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
    }
    protected void txtcomrate_TextChanged(object sender, EventArgs e)
    {
        //Get_Rate();
        //Days_In_Month = Convert.ToInt32(ViewState["Days_In_Month"]);
        //Rate_M = Convert.ToDecimal(txtcomrate.Text);
        ////decimal Rate_D = Rate_M / Days_In_Month;
        //decimal Rate_D = Rate_M / 30;
        //txtCPRate.Text = Math.Round(Rate_D, 4).ToString();

        int Month_No = Convert.ToInt32(ddlmonth.SelectedValue);
        if (Month_No == 1 || Month_No == 3 || Month_No == 5 || Month_No == 7 || Month_No == 8 || Month_No == 10 || Month_No == 12)
        {
            Days_In_Month = 31;
        }
        else if (Month_No == 2)
        {
            //Days_In_Month = 29;
            Days_In_Month = 28;
        }
        else
        {
            Days_In_Month = 30;
        }
        //Rate_M = 68;
        Rate_M = Convert.ToDecimal(txtcomrate.Text);
        txtcomrate.Text = Rate_M.ToString();
        //
        string Actual_Rate = "";
        string Value = (Rate_M / Days_In_Month).ToString();
        int U_PerDayRateCount = Value.Length;
        if (U_PerDayRateCount >= 6)
        {
            string PerDayRate = Value;
            int I = PerDayRate.IndexOf(".");

            string U_PerDayRate1 = PerDayRate.Substring(0, I);
            string U_PerDayRate = PerDayRate.Substring(I, I + 5);
            Actual_Rate = U_PerDayRate1 + U_PerDayRate;
        }
        else
        {
            //Actual_Rate = Math.Round((Rate_M / Days_In_Month), 4).ToString();
            Actual_Rate = (Rate_M / Days_In_Month).ToString();
        }
        txtCPRate.Text = Actual_Rate;
        ViewState["Days_In_Month"] = Days_In_Month;
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        }
        else if (ddlmonth.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        }
        else
        {

            string Existance = "";
            Existance = Check_Bill_Existance();
            if (Existance == "N")
            {
                GetYear();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Bill Already Generated...!');</script>");
                txtcomrate.Text = "";
                txtCPRate.Text = "";
                //Response.Redirect("./Accounting/frm_MPSCSC_SC_Bill.aspx");
            }

        }
    }
    public void GetYear()
    {
        string CropY = "";
        int CMonth = 0;
        string FCYear = "";
        string SCYear = "";
        string CFnancialYear = "";
        CMonth = Convert.ToInt32(ddlmonth.SelectedValue);
        //CropY = ddlCropYear.SelectedItem.Text;
        CropY = ddlFyear.SelectedItem.Text;
        if (CropY != "")
        {
            FCYear = CropY.Substring(0, 4);
            SCYear = "20" + CropY.Substring(5, 2);
        }
        if (CMonth != 0)
        {
            if (CMonth <= 3)
            {
                CFnancialYear = SCYear;
            }
            else if (CMonth >= 4)
            {
                CFnancialYear = FCYear;
            }
        }
        trRentBill.Visible = false;
        trReportsView.Visible = false;
        //Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
        Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
        ViewState["Days_In_Month"] = Days_In_Month;
        ViewState["CFnancialYear"] = CFnancialYear;
        //ViewState["CFnancialYear"] = "2018";
        Get_Rate();
    }
    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
        ViewState["Days_In_Month"] = Days_In_Month;
    }
    protected void btncancel2_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void brnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodownType.SelectedValue.ToString() == "1" || ddlGodownType.SelectedValue.ToString() == "3" || ddlGodownType.SelectedValue.ToString() == "4" || ddlGodownType.SelectedValue.ToString() == "5")
        {
            trJVSGodownRent.Visible = true;
            trHiredGodownRent.Visible = false;
            //trJVSGodownRent.Visible = false;
            //trHiredGodownRent.Visible = true;
        }
        else if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            //trJVSGodownRent.Visible = false;
            //trHiredGodownRent.Visible = true;
            trJVSGodownRent.Visible = true;
            trHiredGodownRent.Visible = false;
        }
        //GetGodown();
        //GetCommodity();
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcomodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
        }
        else
        {
            GetCropYear();
        }
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCropYear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        }
        else
        {
            fillMonth();
        }

    }
    protected void ddlFyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetYear();
    }
    protected void btnHCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void txtfdate_TextChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        }
        else if (txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        }
        else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
        }
        else
        {
            GetPeriodsAndRate();
        }
    }
    protected void txttodate_TextChanged1(object sender, EventArgs e)
    {
        if (txtfdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        }
        else if (txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        }
        else if (Convert.ToDateTime(getDate_MDY(txtfdate.Text)) > Convert.ToDateTime(getDate_MDY(txttodate.Text)))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
        }
        else
        {
            GetPeriodsAndRate();
        }
    }
    protected void ddlGodown2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtfdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
            ddlGodown2.ClearSelection();
        }
        else if (txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
            ddlGodown2.ClearSelection();
        }
        else
        {
            ddlFyear2.ClearSelection();
            fillFinancialYear2();
            GetGodownRentRate();
            GetPeriodsAndRate();
            //GetTotalRent();
            //Get_Rate2();
        }
    }
    protected void fillFinancialYear2()
    {

        ddlFyear2.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlFyear2.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlFyear2.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlFyear2.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlFyear2.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlFyear2.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
    }
    protected void btnHSubmit_Click(object sender, EventArgs e)
    {
        if (ddlGodown2.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
        }
        else if (txtgratePM.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Monthly Rate...'); </script> ");
        }
        else if (txtgratePD.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Per Day Rate...'); </script> ");
        }
        if (txtfdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        }
        else if (txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        }
        else if (txtRent.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Fill Rent...'); </script> ");
        }
        else
        {
            GetStorageBillNo();
            Insert_Bill_Detail();
            //Report_Godown_Hired_Bill();
        }
    }
    public void GetGodownRentRate()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["G_BranchID"].ToString();
        qry = "select Rent_M,Storage_Capacity from tbl_Register_Hired_Godown where Godown_No='" + ddlGodown2.SelectedValue.ToString() + "' and Is_Active='Y'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            txtgratePM.Text = "";
            txtgratePD.Text = "";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found in Godown Register...'); </script> ");
        }
        else
        {
            ////////////get month days////////////////
            DateTime CFDate = new DateTime();
            DateTime CTDate = new DateTime();
            CFDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            CTDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));
            int CFDateMC = 0;
            int CTDateMC = 0;
            //int lastDayOfMonth = 0;
            if (CFDateMC == CTDateMC)
            {
                lastDayOfMonth = DateTime.DaysInMonth(CFDate.Year, CFDate.Month);
                ViewState["lastDayOfMonth"] = lastDayOfMonth.ToString();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Date of Same Month...'); </script> ");
            }

            ////////////////////////get month days////////////////
            decimal Monthly_Rate = Convert.ToDecimal(ds.Tables[0].Rows[0]["Rent_M"]);
            decimal PD_Rate = Monthly_Rate / lastDayOfMonth;

            //txtgratePM.Text = Monthly_Rate.ToString();
            //txtgratePD.Text = PD_Rate.ToString();
            txtgratePM.Text = Math.Round(Monthly_Rate, 4).ToString();
            txtgratePD.Text = Math.Round(PD_Rate, 6).ToString();
            txtSCapacity.Text = ds.Tables[0].Rows[0]["Storage_Capacity"].ToString();
        }
    }
    public void GetPeriodsAndRate()
    {
        try
        {

            DateTime fdate = new DateTime();
            DateTime tdate = new DateTime();

            string sfdate = getDate_MDY(txtfdate.Text);

            fdate = DateTime.Parse(sfdate);

            string stodate = getDate_MDY(txttodate.Text);
            tdate = DateTime.Parse(stodate);
            TotalDays = Convert.ToInt32((tdate - fdate).TotalDays) + 1;

            decimal TotalRent = 0;
            if (txtgratePD.Text != "")
            {
                TotalRent = TotalDays * Convert.ToDecimal(txtgratePD.Text);
                txtRent.Text = Math.Round(TotalRent, 4).ToString();
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void txtgratePM_TextChanged(object sender, EventArgs e)
    {

        decimal NPR = 0;
        Rate_M = Convert.ToDecimal(txtgratePM.Text);
        NPR = (Rate_M / Convert.ToInt32(ViewState["lastDayOfMonth"].ToString()));
        txtgratePD.Text = Math.Round(NPR, 6).ToString();
        GetPeriodsAndRate();
    }
    protected void ddlFyear2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtgratePD_TextChanged(object sender, EventArgs e)
    {
        GetPeriodsAndRate();
    }
    public string Check_Bill_Existance()
    {
        string Bill_Type = "";
        if (ddlGodownType.SelectedValue == "1" || ddlGodownType.SelectedValue == "3" || ddlGodownType.SelectedValue == "4" || ddlGodownType.SelectedValue.ToString() == "5" || ddlGodownType.SelectedValue.ToString() == "15")
        {
            Bill_Type = "GR";
        }
        else if (ddlGodownType.SelectedValue == "2")
        {
            Bill_Type = "HG";
        }
        string Existance = "";
        string CropYear = ddlCropYear.SelectedItem.Text;
        string FinYear = ddlFyear.SelectedItem.Text;
        CropYear = NineCrop(CropYear);
        FinYear = NineCrop(FinYear);
        //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details_For_NAFED where Crop_Year='" + ddlCropYear.SelectedItem.Text + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='GR'";
        //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details_For_NAFED where Crop_Year='" + CropYear + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='" + Bill_Type + "'";
        //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details_For_NAFED where Crop_Year='" + CropYear + "' and Financial_Year='" + FinYear + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='" + Bill_Type + "'";
        qry = "select Bill_Number from tbl_Institution_Rent_Bill_Details_For_DMO_Markfed where Crop_Year='" + CropYear + "' and Financial_Year in ('" + FinYear + "','" + ddlFyear.SelectedItem.Text + "') and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='" + Bill_Type + "'";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            Existance = "N";
        }
        else
        {
            Existance = "Y";
        }
        return Existance;
    }
    public string NineCrop(string INCROPS)
    {
        string InCrop = INCROPS;
        string OutCrop = "";
        if (InCrop == "2015-16")
        {
            OutCrop = "2015-2016";
        }
        else if (InCrop == "2016-17")
        {
            OutCrop = "2016-2017";
        }
        else if (InCrop == "2017-18")
        {
            OutCrop = "2017-2018";
        }
        else if (InCrop == "2018-19")
        {
            OutCrop = "2018-2019";
        }
        else if (InCrop == "2019-20")
        {
            OutCrop = "2019-2020";
        }
        else if (InCrop == "2020-21")
        {
            OutCrop = "2020-2021";
        }
        else if (InCrop == "2021-22")
        {
            OutCrop = "2021-2022";
        }
        else if (InCrop == "2022-23")
        {
            OutCrop = "2022-2023";
        }
        else if (InCrop == "2023-24")
        {
            OutCrop = "2023-2024";
        }
        else if (InCrop == "2024-25")
        {
            OutCrop = "2024-2025";
        }
        else if (InCrop == "2025-26")
        {
            OutCrop = "2025-2026";
        }
        return OutCrop;
    }
    public void Get_Godown_Type()
    {
        int Godown_Type = 0;
        ////int Godown_Type = 5;
        string qry = "select GodownTypeId from pvt_Warehouse_Login where Godown_Id='" + Session["GodownID_New"].ToString() + "' and BranchID='" + Session["G_BranchID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        con.Open();
        Godown_Type = Convert.ToInt32(cmd.ExecuteScalar());
        con.Close();
        //return Godown_Type;
        if (Godown_Type == 6 || Godown_Type == 10)
        {
            ddlGodownType.SelectedIndex = 1;
        }
        else if (Godown_Type == 3)
        {
            ddlGodownType.SelectedIndex = 3;
        }
        else if (Godown_Type == 12)
        {
            ddlGodownType.SelectedIndex = 2;
        }
        else if (Godown_Type == 13)
        {
            ddlGodownType.SelectedIndex = 4;
        }
        else
        {
            ddlGodownType.SelectedIndex = 5;
        }
        trJVSGodownRent.Visible = true;
        trHiredGodownRent.Visible = false;
        GetGodown();
        GetCommodity();
    }
    protected void gvIStorageCharge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotal_Charges = (Label)e.Row.FindControl("lblCharges");

            TT1 += Convert.ToDecimal(lblTotal_Charges.Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[6].Text = "<div style='text-align: right'>" + "महायोग :-" + "</div>";
            //e.Row.Cells[7].Text = "<div style='text-align: right'>" + TT1.ToString() + "</div>";
            hdnAmt.Value = TT1.ToString();

        }
    }
    private bool CheckDupliCate()
    {
        try
        {
            CFnancialYear = ViewState["CFnancialYear"].ToString();
            int ContMD = 0;
            //ContMD = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            ContMD = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            //DateTime LDateTime = new DateTime();
            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();
            string SD = "";
            string ED = "";
            if (Convert.ToInt32(ddlmonth.SelectedValue) <= 9)
            {
                SD = "01/" + "0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }
            else
            {
                SD = "01/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }

            StartDate = Convert.ToDateTime(getDate_MDY(SD));
            EndDate = Convert.ToDateTime(getDate_MDY(ED));
            string str = "";
            string FinYear = NineCrop(ddlCropYear.SelectedValue);

            cmd = new SqlCommand("Check_Bill_Duplication_DMO_Markfed", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_Id", ddlgodown.SelectedValue);
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcomodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", FinYear);
            cmd.Parameters.AddWithValue("@From_Date", SD);
            cmd.Parameters.AddWithValue("@To_Date", ED);
            cmd.Parameters.AddWithValue("@Bill_Type", "HG");

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["DupliCate"].ToString().Equals("NEW"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        catch (Exception ex)
        {
            return false;
        }
    }
}