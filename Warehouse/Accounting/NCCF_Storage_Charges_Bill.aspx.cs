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

public partial class Accounting_NCCF_Storage_Charges_Bill : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    string NetAmountWord = "";
    string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    string Bill_Type = "";
    int BID = 0;
    decimal ChargeOfTotalWeight = 0;
    DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    DateTime MiddleDate15 = new DateTime();
    DateTime MiddleDate16 = new DateTime();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillCropYear();
                fillBillYear();
                fillFinancialYear_New();
                fillMonth();
                //GetCommodity();
                //GetGodown();
                Session["dt1"] = null;
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        if (ddlGodownType.SelectedValue.ToString() == "1")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Owned') and Storage_Type not in ('Permanent(CAP)','Temporary(CAP)') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Joint Venture(JV)','WDRA') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "3")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "4")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Silo Bags') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "5")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type='Owned' and Storage_Type in ('Permanent(CAP)','Temporary(CAP)') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "6")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type='Tribal Scheme' order by Godown_Name asc";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "7")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type='CAP-PMS' order by Godown_Name asc";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "8")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('BOT','BOT-AUB') order by Godown_Name asc";
        }
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    public void GetCommodity()
    {

        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26','75') order by Commodity_Name asc";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_ID";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedValue == "26")
        {
            txtcomrate.Text = "8.80";
            txtCPRate.Text = "4.40";
        }
        else if (ddlcommodity.SelectedValue == "52" || ddlcommodity.SelectedValue == "92" || ddlcommodity.SelectedValue == "27" || ddlcommodity.SelectedValue == "64" || ddlcommodity.SelectedValue == "63" || ddlcommodity.SelectedValue == "75")
        {
            txtcomrate.Text = "7.98";
            txtCPRate.Text = "3.99";
        }
        else if (ddlcommodity.SelectedValue == "33" || ddlcommodity.SelectedValue == "123" || ddlcommodity.SelectedValue == "65")
        {
            txtcomrate.Text = "7.51";
            txtCPRate.Text = "3.76";
        }

        else if (ddlcommodity.SelectedValue == "31")
        {
            txtcomrate.Text = "12.91";
            txtCPRate.Text = "6.46";
        }
    }
    protected void fillMonth()
    {
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));

        DateTime today = DateTime.Now;

        // Current Month और Previous Month की जानकारी निकालना
        DateTime currentMonthDt = today;
        DateTime previousMonthDt = today.AddMonths(-1);

        string prevMonthValue = previousMonthDt.ToString("MM");
        string prevMonthText = previousMonthDt.ToString("MMMM", CultureInfo.InvariantCulture);

        string currMonthValue = currentMonthDt.ToString("MM");
        string currMonthText = currentMonthDt.ToString("MMMM", CultureInfo.InvariantCulture);

        // केवल पिछला महीना और वर्तमान महीना जोड़ेगा
        ddlmonth.Items.Add(new ListItem(prevMonthText, prevMonthValue));
        ddlmonth.Items.Add(new ListItem(currMonthText, currMonthValue));

        ddlmonth.SelectedIndex = 0;
    }
    //protected void fillBillYear()
    //{
    //    ddlFinYear.Items.Insert(0, "--Select--");
    //    ddlFinYear.Items.Add((DateTime.Now.Year).ToString());
    //    ddlFinYear.Items.Add((DateTime.Now.Year - 1).ToString());
    //    ddlFinYear.Items.Add((DateTime.Now.Year - 2).ToString());
    //}
    //protected void fillMonth()
    //{
    //    //ddlmonth.ClearSelection();
    //    ddlmonth.Items.Clear();
    //    ddlmonth.Items.Add(new ListItem("--Select--", "0"));
    //    ddlmonth.Items.Add(new ListItem("January", "01"));
    //    ddlmonth.Items.Add(new ListItem("February", "02"));
    //    ddlmonth.Items.Add(new ListItem("March", "03"));
    //    ddlmonth.Items.Add(new ListItem("April", "04"));
    //    ddlmonth.Items.Add(new ListItem("May", "05"));
    //    ddlmonth.Items.Add(new ListItem("June", "06"));
    //    ddlmonth.Items.Add(new ListItem("July", "07"));
    //    ddlmonth.Items.Add(new ListItem("August", "08"));
    //    ddlmonth.Items.Add(new ListItem("September", "09"));
    //    ddlmonth.Items.Add(new ListItem("October", "10"));
    //    ddlmonth.Items.Add(new ListItem("November", "11"));
    //    ddlmonth.Items.Add(new ListItem("December", "12"));
    //    ddlmonth.SelectedIndex = 0;
    //}
    //protected void fillFinancialYear_New()
    //{
    //    ddlFyear.Items.Insert(0, "--Select--");
    //    ddlFyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
    //    ddlFyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
    //    ddlFyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
    //}
    protected void fillBillYear()
    {
        ddlFinYear.Items.Clear();
        ddlFinYear.Items.Insert(0, new ListItem("--Select--", "0"));

        // Bill Year Dropdown में केवल Current Year (जैसे: 2026) दिखाना
        ddlFinYear.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
    }
    protected void fillFinancialYear_New()
    {
        ddlFyear.Items.Clear();
        ddlFyear.Items.Insert(0, new ListItem("--Select--", "0"));

        // वर्तमान तारीख के आधार पर Financial Year निकालना
        DateTime today = DateTime.Now;
        int currentYear = today.Year;
        int startYear = (today.Month >= 4) ? currentYear : currentYear - 1;

        // केवल Current Financial Year जोड़ना (जैसे: 2026-27)
        string currentFinYear = startYear.ToString() + "-" + (startYear + 1).ToString().Substring(2, 2);
        ddlFyear.Items.Add(new ListItem(currentFinYear, currentFinYear));
    }
    protected void fillCropYear()
    {
        ddlcropyr.Items.Insert(0, "--Select--");
        ddlcropyr.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity...'); </script> ");
        }
        else if (ddlmonth.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Month...'); </script> ");
        }
        else if (ddlcropyr.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
        }
        else if (ddlFinYear.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Finantial Year...'); </script> ");
        }
        else
        {
            if (CheckDuplicate())
            {
                GetStorageDailyChargesBillDetail();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Bill Already Generated...!');</script>");
            }
        }
    }
    private bool CheckDuplicate()
    {
        try
        {
            int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
            string MonthS = ddlmonth.SelectedItem.Value;
            int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value).Substring(0, 4));
            int LastDay = DateTime.DaysInMonth(Year, Month);
            string FirstDate = "01/" + MonthS + "/" + Year;
            string LastDate = LastDay + "/" + MonthS + "/" + Year;
            string str = "";
            string BranchID = Session["BranchID"].ToString();
            cmd = new SqlCommand("Check_NCCF_DuplicateBillbyGodown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DepositorID", "15478");
            cmd.Parameters.AddWithValue("@CommodityId", ddlcommodity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CropYear", ddlcropyr.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@BillMonth", ddlmonth.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BillYear", ddlFyear.SelectedValue.ToString());
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
    public void GetStorageDailyChargesBillDetail()
    {
        try
        {
            DateTime LDateTime = new DateTime();
            int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
            string MonthS = ddlmonth.SelectedItem.Value;
            //int Year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value));
            int LastDay = DateTime.DaysInMonth(Year, Month);
            string FirstDate = "01/" + MonthS + "/" + Year;
            string MDate15 = "15/" + MonthS + "/" + Year;
            string MDate16 = "16/" + MonthS + "/" + Year;
            string LastDate = LastDay + "/" + MonthS + "/" + Year;
            //StartDate = Convert.ToDateTime(getDate_MDY(FirstDate));
            StartDate = Convert.ToDateTime((FirstDate));
            DateTime MiddleDate15 = Convert.ToDateTime(getDate_MDY(MDate15));
            DateTime MiddleDate16 = Convert.ToDateTime(getDate_MDY(MDate16));
            EndDate = Convert.ToDateTime(getDate_MDY(LastDate));
            decimal PerDayRate = 0;
            //PerDayRate = Convert.ToDecimal("0");

            if (ddlcommodity.SelectedValue == "26")
            {
                PerDayRate = Convert.ToDecimal("4.40");
                //PerDayRate = "";
            }
            else if (ddlcommodity.SelectedValue == "52" || ddlcommodity.SelectedValue == "92" || ddlcommodity.SelectedValue == "27" || ddlcommodity.SelectedValue == "64" || ddlcommodity.SelectedValue == "63" || ddlcommodity.SelectedValue == "75")
            {
                PerDayRate = Convert.ToDecimal("3.99");
            }
            else if (ddlcommodity.SelectedValue == "33" || ddlcommodity.SelectedValue == "123" || ddlcommodity.SelectedValue == "65")
            {
                PerDayRate = Convert.ToDecimal("3.76");
            }
            else if (ddlcommodity.SelectedValue == "31")
            {
                PerDayRate = Convert.ToDecimal("6.46");
            }

            PerDayRate = Convert.ToDecimal(txtCPRate.Text);
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
            //new DataColumn("Static_Date",typeof(DateTime),null),
            new DataColumn("SN",typeof(string),null),
            new DataColumn("OpeningBalance",typeof(decimal)),
            new DataColumn("ReceivedBags",typeof(decimal)),
            new DataColumn("DeliveredBags",typeof(decimal)),
            new DataColumn("ClosingBalance",typeof(decimal)),
            //new DataColumn("Opening_Weight",typeof(decimal)),
            //new DataColumn("Receive_Weight",typeof(decimal)),
            //new DataColumn("Issue_Weight",typeof(decimal)),
            //new DataColumn("Closing_Weight",typeof(decimal)),
            new DataColumn("Per_Day_Rate",typeof(decimal)),
            new DataColumn("Charges",typeof(decimal)),
            new DataColumn("Reserve_Bags",typeof(decimal)),
            new DataColumn("Chargable_Bags",typeof(decimal)),
            //new DataColumn("Weight_Charges",typeof(decimal)),
            //new DataColumn("Godown_Id",typeof(string)),
            });

            DataTable ddt3 = new DataTable();
            ddt3.Columns.AddRange(new DataColumn[]  { 
            //new DataColumn("Deposit_Date",typeof(DateTime),null),
            //new DataColumn("Deposit_Date",typeof(string)),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),
            new DataColumn("Reserve_Bags",typeof(decimal)),
            new DataColumn("Chargable_Bags",typeof(decimal)),
            //new DataColumn("Opening_Weight",typeof(decimal)),
            //new DataColumn("Receive_Weight",typeof(decimal)),
            //new DataColumn("Issue_Weight",typeof(decimal)),
            //new DataColumn("Closing_Weight",typeof(decimal)),
            new DataColumn("Per_Day_Rate",typeof(decimal)),
            new DataColumn("Charges",typeof(decimal)),
            //new DataColumn("Weight_Charges",typeof(decimal)),
            new DataColumn("Godown_Id",typeof(string)),
            // new DataColumn("Per_Day_Rate_Weight",typeof(decimal)),
            //new DataColumn("Charges_Weight",typeof(decimal)),
            });

            string BranchID = Session["BranchID"].ToString();
            //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text + "'";
            con.Open();
            //string str = "usp_NAFED_BillCalculation";
            //string str = "usp_NAFED_StorageBillCalculation";
            string str = "usp_NCCF_StorageBillCalculation_NewTest";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandTimeout = 200;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@DepositorID", "15478");
            cmd.Parameters.AddWithValue("@CommodityId", ddlcommodity.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@CropYear", ddlcropyr.SelectedItem.Text.ToString());
            cmd.Parameters.AddWithValue("@BillMonth", ddlmonth.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@BillYear", ddlFinYear.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k <= dt.Rows.Count - 1; k++)
                {

                    ddt2.Rows.Add();
                    //ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
                    ddt2.Rows[k]["SN"] = Convert.ToString((dt.Rows[k]["SN"]));
                    ddt2.Rows[k]["OpeningBalance"] = Convert.ToDecimal(dt.Rows[k]["OpeningBalance"]);
                    ddt2.Rows[k]["ReceivedBags"] = Convert.ToDecimal(dt.Rows[k]["ReceivedBags"]);
                    ddt2.Rows[k]["DeliveredBags"] = Convert.ToDecimal(dt.Rows[k]["DeliveredBags"]);
                    ddt2.Rows[k]["ClosingBalance"] = Convert.ToDecimal(dt.Rows[k]["ClosingBalance"]);
                    ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                    ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["ClosingBalance"]);
                    ddt2.Rows[k]["Reserve_Bags"] = Convert.ToDecimal("0");
                    ddt2.Rows[k]["Chargable_Bags"] = Convert.ToDecimal(dt.Rows[k]["OpeningBalance"]) + Convert.ToDecimal(dt.Rows[k]["ReceivedBags"]);
                }
                gvnccfStorageCharge.DataSource = ddt2;
                gvnccfStorageCharge.DataBind();
                trRentBill.Visible = true;
                btnGenerateBill.Enabled = false;
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
    //public void GetStorageDailyChargesBillDetail()
    //{
    //    try
    //    {
    //        //DateTime LDateTime = new DateTime();

    //        ////StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
    //        ////EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));
    //        //int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
    //        //string MonthS = ddlmonth.SelectedItem.Value;
    //        ////int Year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
    //        //int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value));
    //        ////int Year = Convert.ToInt32((ddlcropyr.SelectedItem.Value).Substring(0, 4));
    //        ////StartDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
    //        ////EndDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
    //        //int LastDay = DateTime.DaysInMonth(Year, Month);
    //        //string FirstDate = "01/" + MonthS + "/" + Year;
    //        //string MDate15 = "15/" + MonthS + "/" + Year;
    //        //string MDate16 = "16/" + MonthS + "/" + Year;
    //        //string LastDate = LastDay + "/" + MonthS + "/" + Year;
    //        //StartDate = Convert.ToDateTime(getDate_MDY(FirstDate));
    //        //DateTime MiddleDate15 = Convert.ToDateTime(getDate_MDY(MDate15));
    //        //DateTime MiddleDate16 = Convert.ToDateTime(getDate_MDY(MDate16));
    //        //EndDate = Convert.ToDateTime(getDate_MDY(LastDate));

    //        DateTime LDateTime = new DateTime();
    //        int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
    //        string MonthS = ddlmonth.SelectedItem.Value;
    //        int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value));
    //        int LastDay = DateTime.DaysInMonth(Year, Month);
    //        string FirstDate = "01/" + MonthS + "/" + Year;
    //        string MDate15 = "15/" + MonthS + "/" + Year;
    //        string MDate16 = "16/" + MonthS + "/" + Year;
    //        string LastDate = LastDay + "/" + MonthS + "/" + Year;
    //        StartDate = Convert.ToDateTime(getDate_MDY(FirstDate));
    //        DateTime MiddleDate15 = Convert.ToDateTime(getDate_MDY(MDate15));
    //        DateTime MiddleDate16 = Convert.ToDateTime(getDate_MDY(MDate16));
    //        EndDate = Convert.ToDateTime(getDate_MDY(LastDate));

    //        decimal PerDayRate = 0;


    //        //if (txtCPRate.Text != "")
    //        //{
    //        // PerDayRate = Convert.ToDecimal(txtCPRate.Text);
    //        if (ddlcommodity.SelectedValue == "26")
    //        {
    //            PerDayRate = Convert.ToDecimal("4.40");
    //            //PerDayRate = "";
    //        }
    //        else if (ddlcommodity.SelectedValue == "52" || ddlcommodity.SelectedValue == "92" || ddlcommodity.SelectedValue == "27" || ddlcommodity.SelectedValue == "64" || ddlcommodity.SelectedValue == "63" || ddlcommodity.SelectedValue == "75")
    //        {
    //            PerDayRate = Convert.ToDecimal("3.99");
    //        }
    //        else if (ddlcommodity.SelectedValue == "33" || ddlcommodity.SelectedValue == "123" || ddlcommodity.SelectedValue == "65")
    //        {
    //            PerDayRate = Convert.ToDecimal("3.76");
    //        }
    //        else if (ddlcommodity.SelectedValue == "31")
    //        {
    //            PerDayRate = Convert.ToDecimal("6.46");
    //        }
    //        //}
    //        int j = 0;
    //        int h = 0;
    //        int m = 0;
    //        DataTable dw = new DataTable();
    //        dw.Columns.AddRange(new DataColumn[]  {
    //             new DataColumn("Static_Date",typeof(DateTime)),
    //          new DataColumn("Deposit_Bags",typeof(decimal),null),
    //            new DataColumn("Deliver_Bags",typeof(decimal)),
    //            new DataColumn("Deposit_Weight",typeof(decimal)),
    //            new DataColumn("Deliver_Weight",typeof(decimal)),
    //            new DataColumn("Godown_Id",typeof(string)),
    //            new DataColumn("Flag",typeof(string)),
    //        });

    //        DateTime Static_date = new DateTime();
    //        DateTime Recent_Static_date = new DateTime();
    //        DataTable ddt2 = new DataTable();

    //        ddt2.Columns.AddRange(new DataColumn[]  {
    //      new DataColumn("Static_Date",typeof(DateTime),null),
    //        new DataColumn("Opening_Balance",typeof(decimal)),
    //        new DataColumn("Receive_Bags",typeof(decimal)),
    //        new DataColumn("Issue_Bags",typeof(decimal)),
    //        new DataColumn("Closing_Balance",typeof(decimal)),

    //        new DataColumn("Opening_Weight",typeof(decimal)),
    //        new DataColumn("Receive_Weight",typeof(decimal)),
    //        new DataColumn("Issue_Weight",typeof(decimal)),
    //        new DataColumn("Closing_Weight",typeof(decimal)),
    //    new DataColumn("Per_Day_Rate",typeof(decimal)),
    //    new DataColumn("Charges",typeof(decimal)),
    //    //new DataColumn("Weight_Charges",typeof(decimal)),
    //    new DataColumn("Godown_Id",typeof(string)),
    //    });

    //        DataTable ddt3 = new DataTable();
    //        ddt3.Columns.AddRange(new DataColumn[]  { 
    //      //new DataColumn("Deposit_Date",typeof(DateTime),null),
    //      new DataColumn("Deposit_Date",typeof(string)),
    //        new DataColumn("Opening_Balance",typeof(decimal)),
    //        new DataColumn("Receive_Bags",typeof(decimal)),
    //        new DataColumn("Issue_Bags",typeof(decimal)),
    //        new DataColumn("Closing_Balance",typeof(decimal)),
    //        new DataColumn("Reserve_Bags",typeof(decimal)),
    //        new DataColumn("Chargable_Bags",typeof(decimal)),
    //        // new DataColumn("Opening_Weight",typeof(decimal)),
    //        //new DataColumn("Receive_Weight",typeof(decimal)),
    //        //new DataColumn("Issue_Weight",typeof(decimal)),
    //        //new DataColumn("Closing_Weight",typeof(decimal)),
    //    new DataColumn("Per_Day_Rate",typeof(decimal)),
    //    new DataColumn("Charges",typeof(decimal)),
    //     //new DataColumn("Weight_Charges",typeof(decimal)),
    //     new DataColumn("Godown_Id",typeof(string)),
    //    // new DataColumn("Per_Day_Rate_Weight",typeof(decimal)),
    //    //new DataColumn("Charges_Weight",typeof(decimal)),
    //    });

    //        string BranchID = Session["BranchID"].ToString();

    //        //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text.Trim() + "'";
    //        string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "'  AND DWD.Godown_ID='" + ddlgodown.SelectedValue + "' and WHR.DepositorID='15478' and DWD.Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text.Trim() + "'";
    //        //string str = "select * from tbl_Nafed";

    //        //Test
    //        //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[DDDD] as DWD inner join WWWW as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text + "'";
    //        //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[DDDD] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text + "'";


    //        SqlCommand cmd = new SqlCommand(str, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
    //            string Static_date_MM = Static_date.ToString("MM");
    //            string StartDate_MM = StartDate.ToString("MM");
    //            //if (StartDate < Static_date && Static_date_MM != "04" && StartDate_MM != "04")
    //            //{
    //            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");

    //            //    trRentBill.Visible = false;
    //            //    trReportsView.Visible = false;
    //            //    btnGenBill.Visible = false;
    //            //    btncancel2.Visible = false;
    //            //}
    //            //else
    //            {
    //                {
    //                    string IMPFlag = "Y";

    //                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
    //                    {

    //                        Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());

    //                        // For April

    //                        DateTime FirstDates = new DateTime();
    //                        DateTime DMDates = new DateTime();
    //                        DateTime AprilDate = new DateTime();
    //                        string MMFraction = "";
    //                        AprilDate = Convert.ToDateTime("04/01/2018");
    //                        MMFraction = "04";
    //                        //if (BranchID != "2307002")
    //                        //{
    //                        //    AprilDate = Convert.ToDateTime("04/01/2018");
    //                        //    MMFraction = "04";
    //                        //}
    //                        //else if (BranchID == "2307002")
    //                        //{
    //                        //    AprilDate = Convert.ToDateTime("07/01/2018");
    //                        //    MMFraction = "07";
    //                        //}                          
    //                        FirstDates = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
    //                        int DD = FirstDates.Day;
    //                        //Q = 4;
    //                        int DmCount = Convert.ToInt32(DD) - 1;
    //                        int TempDIffDayCount = 0;
    //                        string MonthNo = ddlmonth.SelectedItem.Value;

    //                        //////
    //                        TimeSpan DiffDay = Static_date - AprilDate;
    //                        int NoDiffDay = Math.Abs(DiffDay.Days);

    //                        //////
    //                        if (Static_date > AprilDate && IMPFlag == "Y")
    //                        {
    //                            for (int k = 0; k < NoDiffDay; k++)
    //                            {
    //                                //TempDIffDayCount=DmCount
    //                                DMDates = AprilDate.AddDays(k);
    //                                dw.Rows.Add();
    //                                Static_date = DMDates;
    //                                dw.Rows[h]["Static_date"] = Static_date;
    //                                dw.Rows[h]["Deposit_Bags"] = 0;
    //                                dw.Rows[h]["Deliver_Bags"] = 0;
    //                                dw.Rows[h]["Deposit_Weight"] = 0;
    //                                dw.Rows[h]["Deliver_Weight"] = 0;
    //                                dw.Rows[h]["Godown_Id"] = "";
    //                                dw.Rows[h]["Flag"] = "Y";
    //                                h++;
    //                                //i++;
    //                                //Recent_Static_date = Static_date;
    //                                //if (i == DmCount - 1)
    //                                //{
    //                                //    IMPFlag = "N";
    //                                //    i = -1;
    //                                //}
    //                            }
    //                            i = -1;
    //                            IMPFlag = "N";
    //                        }
    //                        //if (i < DmCount && MonthNo == MMFraction && IMPFlag == "Y")
    //                        //{
    //                        //    //TempDIffDayCount=DmCount
    //                        //    DMDates = AprilDate.AddDays(i);
    //                        //    dw.Rows.Add();
    //                        //    Static_date = DMDates;
    //                        //    dw.Rows[h]["Static_date"] = Static_date;
    //                        //    dw.Rows[h]["Deposit_Bags"] = 0;
    //                        //    dw.Rows[h]["Deliver_Bags"] = 0;
    //                        //    dw.Rows[h]["Deposit_Weight"] = 0;
    //                        //    dw.Rows[h]["Deliver_Weight"] = 0;
    //                        //    dw.Rows[h]["Godown_Id"] = "";
    //                        //    dw.Rows[h]["Flag"] = "Y";
    //                        //    h++;
    //                        //    //i++;
    //                        //    //Recent_Static_date = Static_date;
    //                        //    if (i == DmCount-1)
    //                        //    {
    //                        //        IMPFlag = "N";
    //                        //        i = -1;
    //                        //    }
    //                        //    //i = -1;
    //                        //}
    //                        //else if (i == 0 && MonthNo == "04")
    //                        //{
    //                        //    Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
    //                        //    dw.Rows.Add();
    //                        //    //            
    //                        //    dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
    //                        //    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
    //                        //    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
    //                        //    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
    //                        //    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
    //                        //    dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
    //                        //    dw.Rows[h]["Flag"] = "Y";
    //                        //    Recent_Static_date = Static_date;
    //                        //    h++;

    //                        //}
    //                        //
    //                        else if (i == 0)
    //                        {
    //                            Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
    //                            dw.Rows.Add();
    //                            //            
    //                            dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
    //                            dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
    //                            dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
    //                            dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
    //                            dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
    //                            dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            Recent_Static_date = Static_date;
    //                            h++;

    //                        }
    //                        else if (Recent_Static_date == Static_date)
    //                        {
    //                            h--;
    //                            Static_date = Recent_Static_date;
    //                            dw.Rows.Add();

    //                            dw.Rows[h]["Static_date"] = Static_date;
    //                            dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Bags"]) + Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
    //                            dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Bags"]) + Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
    //                            dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deposit_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
    //                            dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dw.Rows[h]["Deliver_Weight"]) + Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
    //                            if (dw.Rows[h]["Godown_Id"].ToString().Contains(dt.Rows[i]["Godown_Id"].ToString()))
    //                            {

    //                            }
    //                            else
    //                            {
    //                                dw.Rows[h]["Godown_Id"] = dw.Rows[h]["Godown_Id"].ToString() + "," + dt.Rows[i]["Godown_Id"].ToString();
    //                            }
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            Recent_Static_date = Static_date;
    //                            h++;
    //                        }
    //                        else if (Recent_Static_date.AddDays(1) == Static_date)
    //                        {

    //                            dw.Rows.Add();

    //                            dw.Rows[h]["Static_date"] = Static_date;
    //                            dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
    //                            dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
    //                            dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
    //                            dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
    //                            dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            Recent_Static_date = Static_date;
    //                            h++;
    //                        }

    //                        else if (Recent_Static_date != Static_date)
    //                        {
    //                            dw.Rows.Add();
    //                            Static_date = Recent_Static_date.AddDays(1);
    //                            dw.Rows[h]["Static_date"] = Static_date;
    //                            dw.Rows[h]["Deposit_Bags"] = 0;
    //                            dw.Rows[h]["Deliver_Bags"] = 0;
    //                            dw.Rows[h]["Deposit_Weight"] = 0;
    //                            dw.Rows[h]["Deliver_Weight"] = 0;
    //                            dw.Rows[h]["Godown_Id"] = "";
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            h++;
    //                            i--;
    //                            Recent_Static_date = Static_date;
    //                        }
    //                        else if (Recent_Static_date == Static_date && (dt.Rows[i]["recbags"].ToString() == "" && dt.Rows[i]["delbags"].ToString() == ""))
    //                        {
    //                            dw.Rows.Add();
    //                            Static_date = Static_date.AddDays(1);
    //                            dw.Rows[h]["Static_date"] = Static_date;
    //                            dw.Rows[h]["Deposit_Bags"] = 0;
    //                            dw.Rows[h]["Deliver_Bags"] = 0;
    //                            dw.Rows[h]["Deposit_Weight"] = 0;
    //                            dw.Rows[h]["Deliver_Weight"] = 0;
    //                            dw.Rows[h]["Godown_Id"] = "";
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            h++;
    //                            i--;
    //                            Recent_Static_date = Static_date;
    //                        }
    //                        else if (Recent_Static_date != Static_date)
    //                        {
    //                            dw.Rows.Add();
    //                            Static_date = Recent_Static_date.AddDays(1);
    //                            dw.Rows[h]["Static_date"] = Static_date;
    //                            dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
    //                            dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
    //                            dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
    //                            dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
    //                            dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
    //                            dw.Rows[h]["Flag"] = "Y";
    //                            Recent_Static_date = Static_date;
    //                            h++;
    //                        }
    //                    }
    //                    for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 365; i++)
    //                    {
    //                        if (i == (dt.Rows.Count - 1))
    //                        {
    //                            LDateTime = Convert.ToDateTime(dw.Rows[h - 1]["Static_date"]);
    //                            LDateTime = LDateTime.AddDays(1);
    //                        }
    //                        else
    //                        {
    //                            LDateTime = LDateTime.AddDays(1);
    //                        }
    //                        dw.Rows.Add();
    //                        dw.Rows[h]["Static_date"] = LDateTime;
    //                        dw.Rows[h]["Deposit_Bags"] = 0;
    //                        dw.Rows[h]["Deliver_Bags"] = 0;
    //                        dw.Rows[h]["Deposit_Weight"] = 0;
    //                        dw.Rows[h]["Deliver_Weight"] = 0;
    //                        dw.Rows[h]["Godown_Id"] = "";
    //                        dw.Rows[h]["Flag"] = "Y";
    //                        h++;
    //                    }
    //                }
    //                if (dw.Rows.Count > 0)
    //                {
    //                    for (int k = 0; k <= dw.Rows.Count - 1; k++)
    //                    {
    //                        if (k == 0 && dw.Rows[k]["Flag"].ToString() == "Y")
    //                        {
    //                            ddt2.Rows.Add();
    //                            ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
    //                            ddt2.Rows[k]["Opening_Balance"] = 0;
    //                            ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
    //                            ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
    //                            ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
    //                            ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
    //                            ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

    //                            ddt2.Rows[k]["Opening_Weight"] = 0;
    //                            ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
    //                            ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
    //                            ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
    //                            //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
    //                            ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();

    //                        }
    //                        else if (dw.Rows[k]["Flag"].ToString() == "Y")
    //                        {
    //                            ddt2.Rows.Add();
    //                            ddt2.Rows[k]["Static_Date"] = Convert.ToDateTime(dw.Rows[k]["Static_Date"]);
    //                            ddt2.Rows[k]["Opening_Balance"] = ddt2.Rows[k - 1]["Closing_Balance"];
    //                            ddt2.Rows[k]["Receive_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]);
    //                            ddt2.Rows[k]["Issue_Bags"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
    //                            ddt2.Rows[k]["Closing_Balance"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Balance"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Bags"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Bags"]);
    //                            ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
    //                            ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

    //                            ddt2.Rows[k]["Opening_Weight"] = ddt2.Rows[k - 1]["Closing_Weight"];
    //                            ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
    //                            ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
    //                            ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
    //                            //ddt2.Rows[k]["Weight_Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Weight"]);
    //                            ddt2.Rows[k]["Godown_Id"] = dw.Rows[k]["Godown_Id"].ToString();
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
    //                }
    //                if (ddt2.Rows.Count > 0)
    //                {
    //                    int DD2 = 0;
    //                    string MonthNo = ddlmonth.SelectedItem.Value;
    //                    int Q = 0;
    //                    string FDate = "";
    //                    decimal FOpeningBags = 0;
    //                    decimal FRecBags = 0;
    //                    decimal FIssueBags = 0;
    //                    decimal FPer_Day_Rate = 0;
    //                    for (int l = 2200; l <= ddt2.Rows.Count - 1; l++)
    //                    {

    //                        DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
    //                        DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
    //                        if (dt1 >= StartDate && dt1 <= EndDate)
    //                        {

    //                            if (dt1 >= StartDate && dt1 <= MiddleDate15)
    //                            {


    //                                if (Q == 0)
    //                                {
    //                                    //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
    //                                    FDate = FirstDate + " To " + MDate15;
    //                                    //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                                    FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                                    //Q = Convert.ToDecimal(dt1.ToString("DD"));
    //                                    int DD = dt1.Day;
    //                                    //Q = 4;
    //                                    Q = Convert.ToInt32(DD) - 1;

    //                                }
    //                                //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                                FRecBags = FRecBags + Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                                //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                                FIssueBags = FIssueBags + Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                                FPer_Day_Rate = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
    //                                if (Q == 14)
    //                                {
    //                                    decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
    //                                    ddt3.Rows.Add();
    //                                    ddt3.Rows[m]["Deposit_Date"] = FDate;
    //                                    ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
    //                                    ddt3.Rows[m]["Receive_Bags"] = FRecBags;
    //                                    ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
    //                                    ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
    //                                    ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(FPer_Day_Rate);
    //                                    ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
    //                                    ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
    //                                    ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(FPer_Day_Rate);
    //                                    ddt3.Rows[m]["Godown_Id"] = "";
    //                                    FOpeningBags = 0;
    //                                    FRecBags = 0;
    //                                    FIssueBags = 0;
    //                                    m = 1;
    //                                }

    //                            }
    //                            //else if (MonthNo=="04" && Q == 0)
    //                            //{
    //                            //    if (Q == 0)
    //                            //    {
    //                            //        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
    //                            //        FDate = FirstDate + " To " + MDate15;
    //                            //        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                            //        FOpeningBags = 0;
    //                            //        //Q = Convert.ToDecimal(dt1.ToString("DD"));
    //                            //        int DD = dt1.Day;
    //                            //        //Q = 4;
    //                            //        Q = Convert.ToInt32(DD) - 1;

    //                            //    }
    //                            //    Q = 14;

    //                            //    if (Q == 14)
    //                            //    {
    //                            //        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
    //                            //        ddt3.Rows.Add();
    //                            //        ddt3.Rows[m]["Deposit_Date"] = FDate;
    //                            //        ddt3.Rows[m]["Opening_Balance"] = 0;
    //                            //        ddt3.Rows[m]["Receive_Bags"] = 0;
    //                            //        ddt3.Rows[m]["Issue_Bags"] = 0;
    //                            //        ddt3.Rows[m]["Closing_Balance"] = 0;
    //                            //        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
    //                            //        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
    //                            //        ddt3.Rows[m]["Chargable_Bags"] = 0 + 0;
    //                            //        ddt3.Rows[m]["Charges"] = 0;
    //                            //        ddt3.Rows[m]["Godown_Id"] = "";
    //                            //        FOpeningBags = 0;
    //                            //        FRecBags = 0;
    //                            //        FIssueBags = 0;
    //                            //        m = 1;
    //                            //    }
    //                            //    l =l-1;
    //                            //}
    //                            //else if (MonthNo == "04" && dt1 >= MiddleDate16 && dt1 <= EndDate && m == 1)
    //                            //{
    //                            //    //int LastDay = DateTime.DaysInMonth(Year, Month);
    //                            //    if (DD2 == 0)
    //                            //    {
    //                            //        DD2 = dt1.Day;
    //                            //    }
    //                            //    int COuntDAy = LastDay - DD2;
    //                            //    //Q = 15;
    //                            //    if (Q == 15)
    //                            //    {
    //                            //        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
    //                            //        FDate = MDate16 + " To " + LastDate;
    //                            //        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                            //        FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                            //    }
    //                            //    //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                            //    decimal NBags = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                            //    FRecBags = FRecBags + NBags;
    //                            //    //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                            //    FIssueBags = FIssueBags + Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                            //    //if (Q == ((LastDay - 1) - COuntDAy))
    //                            //    if (Q == (((LastDay - 1) - COuntDAy))+4)
    //                            //    {
    //                            //        ddt3.Rows.Add();
    //                            //        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
    //                            //        ddt3.Rows[m]["Deposit_Date"] = FDate;
    //                            //        ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
    //                            //        ddt3.Rows[m]["Receive_Bags"] = FRecBags;
    //                            //        ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
    //                            //        ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
    //                            //        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
    //                            //        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
    //                            //        ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
    //                            //        ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(3.10);
    //                            //        ddt3.Rows[m]["Godown_Id"] = "";
    //                            //        m = 1;
    //                            //    }
    //                            //}
    //                            else if (dt1 >= MiddleDate16 && dt1 <= EndDate && m == 1)
    //                            {
    //                                //Q = 15;
    //                                if (Q == 15)
    //                                {
    //                                    //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
    //                                    FDate = MDate16 + " To " + LastDate;
    //                                    //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                                    FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
    //                                }
    //                                //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                                FRecBags = FRecBags + Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
    //                                //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                                FIssueBags = FIssueBags + Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
    //                                FPer_Day_Rate = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
    //                                if (Q == LastDay - 1)
    //                                {
    //                                    ddt3.Rows.Add();
    //                                    decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
    //                                    ddt3.Rows[m]["Deposit_Date"] = FDate;
    //                                    ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
    //                                    ddt3.Rows[m]["Receive_Bags"] = FRecBags;
    //                                    ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
    //                                    ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
    //                                    ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(FPer_Day_Rate);
    //                                    ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
    //                                    ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
    //                                    ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(FPer_Day_Rate);
    //                                    ddt3.Rows[m]["Godown_Id"] = "";
    //                                    m = 1;
    //                                }
    //                            }

    //                            Q = Q + 1;
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
    //                }
    //                if (ddt3.Rows.Count > 0)
    //                {
    //                    gvnccfStorageCharge.DataSource = ddt3;
    //                    gvnccfStorageCharge.DataBind();
    //                    trRentBill.Visible = true;
    //                    btnGenerateBill.Enabled = false;

    //                }
    //                else
    //                {
    //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found..!');</script>");
    //                    trRentBill.Visible = false;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found.!');</script>");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblmsg.Text = ex.Message;
    //    }
    //}
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            //var res = System.Convert.ToDateTime(converted);
            return converted;
        }
    }
    protected string getDate_MDY1(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd/MM/yyyy");
            return converted;
        }
    }
    protected void btnGenBill_Click(object sender, EventArgs e)
    {
        GetStorageBillNo();
        Insert_Bill_Daily_Detail();
        Insert_Bill_Detail();
        btnGenerateBill.Enabled = true;
        //Button1.Visible = true;
        //Report_Storage_Bill_Daily_Details();
    }
    public void GetStorageBillNo()
    {
        string BranchID = Session["BranchID"].ToString();
        qry = "select max(BId) as BId from tbl_NCCF_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='15478'";
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
                Bill_No = BranchID + "" + 15478 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = BranchID + "" + 15478 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = BranchID + "" + 15478 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
    }
    public void Insert_Bill_Daily_Detail()
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
            decimal Total_Charges = 0;

            //string Godown_Id = "";        
            decimal Reserve_Bags = 0;
            decimal Chargable_Bags = 0;
            decimal Per_Day_Rate_Weight = 0;
            decimal Charges_Weight = 0;

            if (gvnccfStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvnccfStorageCharge.Rows.Count - 1; i++)
                {
                    Dates = gvnccfStorageCharge.Rows[i].Cells[0].Text.ToString();
                    Opening_Balance = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[4].Text);
                    Reserve_Bags = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[5].Text);
                    Chargable_Bags = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[6].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[7].Text);
                    Total_Charges = Convert.ToDecimal(gvnccfStorageCharge.Rows[i].Cells[8].Text);
                    NetAmount = NetAmount + Total_Charges;
                    string qry = "";

                    //qry = "INSERT INTO tbl_Bills_Fifteen_Day_Wise_Dtl(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'')";
                    qry = "INSERT INTO tbl_Bills_Fifteen_Day_Wise_NCCF([Bill_Number],[Dates_Period],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Reserve_Bags],[Chargable_Bags],[Rate],[Total_Charges],[CreatedBy],[CreatedDate]) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Reserve_Bags + "','" + Chargable_Bags + "','" + Per_Day_Rate + "','" + Total_Charges + "','" + ip + "',getdate())";

                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
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
    public void Insert_Bill_Detail()
    {
        try
        {
            string qry = "";
            Bill_No = ViewState["BillNo"].ToString();
            //Month
            DateTime LDateTime = new DateTime();

            //StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            //EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));
            int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
            string MonthS = ddlmonth.SelectedItem.Value;
            int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value).Substring(0, 4));
            //StartDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
            //EndDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
            int LastDay = DateTime.DaysInMonth(Year, Month);
            string FirstDate = "01/" + MonthS + "/" + Year;
            Session["FromDate"] = FirstDate.ToString();
            string MDate15 = "15/" + MonthS + "/" + Year;
            string MDate16 = "16/" + MonthS + "/" + Year;
            string LastDate = LastDay + "/" + MonthS + "/" + Year;
            Session["ToDate"] = LastDate.ToString();
            StartDate = Convert.ToDateTime(getDate_MDY(FirstDate));
            DateTime MiddleDate15 = Convert.ToDateTime(getDate_MDY(MDate15));
            DateTime MiddleDate16 = Convert.ToDateTime(getDate_MDY(MDate16));
            EndDate = Convert.ToDateTime(getDate_MDY(LastDate));
            //Month

            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            string BranchID = Session["BranchID"].ToString();
            //Get_Bill_Type();
            BID = Convert.ToInt32(ViewState["BID"]);

            //string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

            SqlCommand cmd = new SqlCommand("Insert_NCCF_Storage_Bill", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@BId", BID);
            cmd.Parameters.AddWithValue("@Bill_Number", Bill_No);
            cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            cmd.Parameters.AddWithValue("@Branch_Id", BranchID);
            cmd.Parameters.AddWithValue("@Godown_Id", ddlgodown.SelectedValue);
            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyr.SelectedValue);
            cmd.Parameters.AddWithValue("@Financial_Year", ddlFyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Bill_Type", "FD");
            cmd.Parameters.AddWithValue("@Depositor_Type_Id", '4');
            cmd.Parameters.AddWithValue("@Depositor_Id", "15478");
            cmd.Parameters.AddWithValue("@Commodity_Type_Id", '3');
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@From_Date", getDate_MDY(FirstDate));
            cmd.Parameters.AddWithValue("@To_Date", getDate_MDY(LastDate));
            cmd.Parameters.AddWithValue("@Packing_Type", '1');
            cmd.Parameters.AddWithValue("@Weight", '5');
            cmd.Parameters.AddWithValue("@Per_Day_Rate", txtCPRate.Text);
            cmd.Parameters.AddWithValue("@Commodity_Rate", txtcomrate.Text);
            cmd.Parameters.AddWithValue("@Net_Amount", NetAmount);
            cmd.Parameters.AddWithValue("@Sub_Amount", NetAmount);
            cmd.Parameters.AddWithValue("@Service_Tax_Perc", '0');
            cmd.Parameters.AddWithValue("@Service_Tax_Amt", '0');
            cmd.Parameters.AddWithValue("@Is_Rebate", 'N');
            cmd.Parameters.AddWithValue("@Created_by", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@Client_IP", ip);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                lblmsg.Text = "Your Bill Successfully Generated. Bill No : " + Bill_No;
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Submit Sussessfully....'+'Bill_No'+)", true);
                gvnccfStorageCharge.DataSource = "";
                gvnccfStorageCharge.DataBind();
                trRentBill.Visible = false;
                divmsg.Visible = true;
                ddlgodown.ClearSelection();
                ddlcommodity.ClearSelection();
                ddlFinYear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcropyr.ClearSelection();
                ddlFyear.ClearSelection();
                //string strMsg = "Document Upload Successfully |||";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //FillGrid();
                //TextClear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                //TextClear();
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/NCCF_Storage_Charges_Bill.aspx");
    }
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}