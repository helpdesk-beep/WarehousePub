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

public partial class Accounting_Nafed_Storage_Charges_Bill_Old_25072025 : System.Web.UI.Page
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
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {      
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                //Printcurrentdate();
                fillCropYear();
                fillFinancialYear();
                fillMonth();              
                Session["dt1"] = null;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "01"));
        ddlmonth.Items.Add(new ListItem("February", "02"));
        ddlmonth.Items.Add(new ListItem("March", "03"));
        ddlmonth.Items.Add(new ListItem("April", "04"));
        ddlmonth.Items.Add(new ListItem("May", "05"));
        ddlmonth.Items.Add(new ListItem("June", "06"));
        ddlmonth.Items.Add(new ListItem("July", "07"));
        ddlmonth.Items.Add(new ListItem("August", "08"));
        ddlmonth.Items.Add(new ListItem("September", "09"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
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
    protected void fillFinancialYear()
    {
        ddlFinYear.Items.Insert(0, "--Select--");
        ddlFinYear.Items.Add((DateTime.Now.Year).ToString());
        ddlFinYear.Items.Add((DateTime.Now.Year-1).ToString());
        ddlFinYear.Items.Add((DateTime.Now.Year-2).ToString());
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        if (ddlcomodity.SelectedItem.Text == "--Select--")
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
            GetStorageDailyChargesBillDetail();
        }       
        //ViewState["MonthName"] = ddlmonth.SelectedItem.Text;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void GetStorageBillNo()
    {
        string BranchID = Session["BranchID"].ToString();
        qry = "select max(BId) as BId from tbl_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='10535'";
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
                Bill_No = BranchID + "" + 10535 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                Bill_No = BranchID + "" + 10535 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            Bill_No = BranchID + "" + 10535 + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        ViewState["BillNo"] = Bill_No;
        ViewState["BID"] = BID;
    }

    public void GetStorageDailyChargesBillDetail()
    {
        try
        {
            DateTime LDateTime = new DateTime();
           
            //StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            //EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));
            int Month = Convert.ToInt32(ddlmonth.SelectedItem.Value);
            string MonthS = ddlmonth.SelectedItem.Value;
            //int Year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            int Year = Convert.ToInt32((ddlFinYear.SelectedItem.Value));
            //int Year = Convert.ToInt32((ddlcropyr.SelectedItem.Value).Substring(0, 4));
            //StartDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
            //EndDate = Convert.ToDateTime(getDate_MDY("01/01/2018"));
            int LastDay = DateTime.DaysInMonth(Year, Month);
            string FirstDate = "01/" + MonthS + "/" + Year;
            string MDate15 = "15/" + MonthS + "/" + Year;
            string MDate16 = "16/" + MonthS + "/" + Year;
            string LastDate = LastDay + "/" + MonthS + "/" + Year;
            StartDate = Convert.ToDateTime(getDate_MDY(FirstDate));
            DateTime MiddleDate15 = Convert.ToDateTime(getDate_MDY(MDate15));
            DateTime MiddleDate16 = Convert.ToDateTime(getDate_MDY(MDate16));
            EndDate = Convert.ToDateTime(getDate_MDY(LastDate));

            decimal PerDayRate = 0;

           
            //if (txtCPRate.Text != "")
            //{
               // PerDayRate = Convert.ToDecimal(txtCPRate.Text);
                PerDayRate = Convert.ToDecimal("0");
            //}
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
          //new DataColumn("Deposit_Date",typeof(DateTime),null),
          new DataColumn("Deposit_Date",typeof(string)),
            new DataColumn("Opening_Balance",typeof(decimal)),
            new DataColumn("Receive_Bags",typeof(decimal)),
            new DataColumn("Issue_Bags",typeof(decimal)),
            new DataColumn("Closing_Balance",typeof(decimal)),
            new DataColumn("Reserve_Bags",typeof(decimal)),
            new DataColumn("Chargable_Bags",typeof(decimal)),
            // new DataColumn("Opening_Weight",typeof(decimal)),
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
            
            string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text.Trim() + "'";
           //string str = "select * from tbl_Nafed";

            //Test
            //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[DDDD] as DWD inner join WWWW as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text + "'";
            //string str = "SELECT DWD.[Depositor_WHR_Id],DWD.[commodity],DWD.[Depositor_Name],DWD.[WHR_Issue_Date] as WHR_Issue_Date,DWD.[recbags],DWD.[delbags],DWD.recweght as Rec_Weight,(DWD.delwght-DWD.Gain+DWD.Loss) as Del_Weight,DWD.[DeliveryDate] as DeliveryDate,DWD.Godown_ID,DWD.[datecom],DWD.[BranchID],DWD.[Branch],DWD.[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[DDDD] as DWD inner join tbl_storage_Depositor_WHR_Relation as WHR on WHR.Depositor_WHR_Id=DWD.Depositor_WHR_Id where DWD.BranchID='" + BranchID + "' and WHR.DepositorID='10535' and DWD.Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and DWD.CropYear='" + ddlcropyr.SelectedItem.Text + "'";


            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Static_date = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                string Static_date_MM = Static_date.ToString("MM");
                string StartDate_MM = StartDate.ToString("MM");
                //if (StartDate < Static_date && Static_date_MM != "04" && StartDate_MM != "04")
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    
                //    trRentBill.Visible = false;
                //    trReportsView.Visible = false;
                //    btnGenBill.Visible = false;
                //    btncancel2.Visible = false;
                //}
                //else
                {
                    {
                        string IMPFlag = "Y";
                        
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                           
                            // For April
                           
                            DateTime FirstDates=new DateTime();
                            DateTime DMDates = new DateTime();
                            DateTime AprilDate = new DateTime();
                            string MMFraction = "";
                            AprilDate = Convert.ToDateTime("04/01/2018");
                            MMFraction = "04";
                            //if (BranchID != "2307002")
                            //{
                            //    AprilDate = Convert.ToDateTime("04/01/2018");
                            //    MMFraction = "04";
                            //}
                            //else if (BranchID == "2307002")
                            //{
                            //    AprilDate = Convert.ToDateTime("07/01/2018");
                            //    MMFraction = "07";
                            //}                          
                            FirstDates = Convert.ToDateTime(dt.Rows[0]["datecom"].ToString());
                            int DD = FirstDates.Day;
                            //Q = 4;
                            int DmCount = Convert.ToInt32(DD) - 1;
                            int TempDIffDayCount = 0;
                            string MonthNo = ddlmonth.SelectedItem.Value;

                            //////
                            TimeSpan DiffDay=Static_date - AprilDate;
                            int NoDiffDay =Math.Abs(DiffDay.Days);
                            
                            //////
                            if (Static_date > AprilDate && IMPFlag=="Y")
                            {
                                for (int k = 0; k < NoDiffDay; k++)
                                {
                                    //TempDIffDayCount=DmCount
                                    DMDates = AprilDate.AddDays(k);
                                    dw.Rows.Add();
                                    Static_date = DMDates;
                                    dw.Rows[h]["Static_date"] = Static_date;
                                    dw.Rows[h]["Deposit_Bags"] = 0;
                                    dw.Rows[h]["Deliver_Bags"] = 0;
                                    dw.Rows[h]["Deposit_Weight"] = 0;
                                    dw.Rows[h]["Deliver_Weight"] = 0;
                                    dw.Rows[h]["Godown_Id"] = "";
                                    dw.Rows[h]["Flag"] = "Y";
                                    h++;
                                    //i++;
                                    //Recent_Static_date = Static_date;
                                    //if (i == DmCount - 1)
                                    //{
                                    //    IMPFlag = "N";
                                    //    i = -1;
                                    //}
                                }
                                i = -1;
                                IMPFlag = "N";
                            }
                            //if (i < DmCount && MonthNo == MMFraction && IMPFlag == "Y")
                            //{
                            //    //TempDIffDayCount=DmCount
                            //    DMDates = AprilDate.AddDays(i);
                            //    dw.Rows.Add();
                            //    Static_date = DMDates;
                            //    dw.Rows[h]["Static_date"] = Static_date;
                            //    dw.Rows[h]["Deposit_Bags"] = 0;
                            //    dw.Rows[h]["Deliver_Bags"] = 0;
                            //    dw.Rows[h]["Deposit_Weight"] = 0;
                            //    dw.Rows[h]["Deliver_Weight"] = 0;
                            //    dw.Rows[h]["Godown_Id"] = "";
                            //    dw.Rows[h]["Flag"] = "Y";
                            //    h++;
                            //    //i++;
                            //    //Recent_Static_date = Static_date;
                            //    if (i == DmCount-1)
                            //    {
                            //        IMPFlag = "N";
                            //        i = -1;
                            //    }
                            //    //i = -1;
                            //}
                            //else if (i == 0 && MonthNo == "04")
                            //{
                            //    Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                            //    dw.Rows.Add();
                            //    //            
                            //    dw.Rows[h]["Static_date"] = Convert.ToDateTime(Static_date);
                            //    dw.Rows[h]["Deposit_Bags"] = Convert.ToDecimal(dt.Rows[i]["recbags"].ToString());
                            //    dw.Rows[h]["Deliver_Bags"] = Convert.ToDecimal(dt.Rows[i]["delbags"].ToString());
                            //    dw.Rows[h]["Deposit_Weight"] = Convert.ToDecimal(dt.Rows[i]["Rec_Weight"].ToString());
                            //    dw.Rows[h]["Deliver_Weight"] = Convert.ToDecimal(dt.Rows[i]["Del_Weight"].ToString());
                            //    dw.Rows[h]["Godown_Id"] = dt.Rows[i]["Godown_Id"].ToString();
                            //    dw.Rows[h]["Flag"] = "Y";
                            //    Recent_Static_date = Static_date;
                            //    h++;

                            //}
                            //
                            else if (i == 0)
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
                        for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 365; i++)
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
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = 0;
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
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
                                ddt2.Rows[k]["Per_Day_Rate"] = PerDayRate;
                                ddt2.Rows[k]["Charges"] = PerDayRate * Convert.ToDecimal(ddt2.Rows[k]["Closing_Balance"]);

                                ddt2.Rows[k]["Opening_Weight"] = ddt2.Rows[k - 1]["Closing_Weight"];
                                ddt2.Rows[k]["Receive_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]);
                                ddt2.Rows[k]["Issue_Weight"] = Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
                                ddt2.Rows[k]["Closing_Weight"] = Convert.ToDecimal(ddt2.Rows[k - 1]["Closing_Weight"]) + Convert.ToDecimal(dw.Rows[k]["Deposit_Weight"]) - Convert.ToDecimal(dw.Rows[k]["Deliver_Weight"]);
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
                        int DD2 = 0;
                        string MonthNo = ddlmonth.SelectedItem.Value;
                        int Q = 0;
                        string FDate = "";
                        decimal FOpeningBags = 0;
                        decimal FRecBags = 0;
                        decimal FIssueBags = 0;
                        for (int l = 2200; l <= ddt2.Rows.Count - 1; l++)
                        {
                           
                            DateTime dt1 = Convert.ToDateTime(ddt2.Rows[l]["Static_Date"]);
                            DateTime dt2 = Convert.ToDateTime(ddt2.Rows[ddt2.Rows.Count - 1]["Static_Date"]);
                            if (dt1 >= StartDate && dt1 <= EndDate)
                            {
                               
                                if (dt1 >= StartDate && dt1 <= MiddleDate15)
                                {
                                   
                                   
                                    if (Q == 0)
                                    {
                                        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
                                        FDate = FirstDate + " To " + MDate15;
                                        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                        FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                        //Q = Convert.ToDecimal(dt1.ToString("DD"));
                                        int DD = dt1.Day;
                                        //Q = 4;
                                        Q = Convert.ToInt32(DD)-1;

                                    }
                                    //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                    FRecBags = FRecBags+Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                    //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                    FIssueBags = FIssueBags+Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                    
                                    if (Q == 14)
                                    {
                                        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
                                        ddt3.Rows.Add();
                                        ddt3.Rows[m]["Deposit_Date"] = FDate;
                                        ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
                                        ddt3.Rows[m]["Receive_Bags"] = FRecBags;
                                        ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
                                        ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
                                        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
                                        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
                                        ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
                                        ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(3.10);
                                        ddt3.Rows[m]["Godown_Id"] = "";
                                         FOpeningBags = 0;
                                         FRecBags = 0;
                                         FIssueBags = 0;
                                        m = 1;
                                    }
                                   
                                }
                                //else if (MonthNo=="04" && Q == 0)
                                //{
                                //    if (Q == 0)
                                //    {
                                //        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
                                //        FDate = FirstDate + " To " + MDate15;
                                //        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                //        FOpeningBags = 0;
                                //        //Q = Convert.ToDecimal(dt1.ToString("DD"));
                                //        int DD = dt1.Day;
                                //        //Q = 4;
                                //        Q = Convert.ToInt32(DD) - 1;

                                //    }
                                //    Q = 14;

                                //    if (Q == 14)
                                //    {
                                //        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
                                //        ddt3.Rows.Add();
                                //        ddt3.Rows[m]["Deposit_Date"] = FDate;
                                //        ddt3.Rows[m]["Opening_Balance"] = 0;
                                //        ddt3.Rows[m]["Receive_Bags"] = 0;
                                //        ddt3.Rows[m]["Issue_Bags"] = 0;
                                //        ddt3.Rows[m]["Closing_Balance"] = 0;
                                //        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
                                //        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
                                //        ddt3.Rows[m]["Chargable_Bags"] = 0 + 0;
                                //        ddt3.Rows[m]["Charges"] = 0;
                                //        ddt3.Rows[m]["Godown_Id"] = "";
                                //        FOpeningBags = 0;
                                //        FRecBags = 0;
                                //        FIssueBags = 0;
                                //        m = 1;
                                //    }
                                //    l =l-1;
                                //}
                                //else if (MonthNo == "04" && dt1 >= MiddleDate16 && dt1 <= EndDate && m == 1)
                                //{
                                //    //int LastDay = DateTime.DaysInMonth(Year, Month);
                                //    if (DD2 == 0)
                                //    {
                                //        DD2 = dt1.Day;
                                //    }
                                //    int COuntDAy = LastDay - DD2;
                                //    //Q = 15;
                                //    if (Q == 15)
                                //    {
                                //        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
                                //        FDate = MDate16 + " To " + LastDate;
                                //        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                //        FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                //    }
                                //    //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                //    decimal NBags = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                //    FRecBags = FRecBags + NBags;
                                //    //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                //    FIssueBags = FIssueBags + Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                //    //if (Q == ((LastDay - 1) - COuntDAy))
                                //    if (Q == (((LastDay - 1) - COuntDAy))+4)
                                //    {
                                //        ddt3.Rows.Add();
                                //        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
                                //        ddt3.Rows[m]["Deposit_Date"] = FDate;
                                //        ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
                                //        ddt3.Rows[m]["Receive_Bags"] = FRecBags;
                                //        ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
                                //        ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
                                //        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
                                //        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
                                //        ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
                                //        ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(3.10);
                                //        ddt3.Rows[m]["Godown_Id"] = "";
                                //        m = 1;
                                //    }
                                //}
                                else if (dt1 >= MiddleDate16 && dt1 <= EndDate && m == 1)
                                {
                                    //Q = 15;
                                    if (Q == 15)
                                    {
                                        //ddt3.Rows[m]["Deposit_Date"] = FirstDate + " To " + MDate15;
                                        FDate = MDate16 + " To " + LastDate;
                                        //ddt3.Rows[m]["Opening_Balance"] = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                        FOpeningBags = Convert.ToDecimal(ddt2.Rows[l]["Opening_Balance"]);
                                    }
                                    //ddt3.Rows[m]["Receive_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                    FRecBags = FRecBags + Convert.ToDecimal(ddt2.Rows[l]["Receive_Bags"]);
                                    //ddt3.Rows[m]["Issue_Bags"] = Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                    FIssueBags = FIssueBags + Convert.ToDecimal(ddt2.Rows[l]["Issue_Bags"]);
                                    if (Q == LastDay-1)
                                    {
                                        ddt3.Rows.Add();
                                        decimal FClosingBags = FOpeningBags + FRecBags - FIssueBags;
                                        ddt3.Rows[m]["Deposit_Date"] = FDate;
                                        ddt3.Rows[m]["Opening_Balance"] = FOpeningBags;
                                        ddt3.Rows[m]["Receive_Bags"] = FRecBags;
                                        ddt3.Rows[m]["Issue_Bags"] = FIssueBags;
                                        ddt3.Rows[m]["Closing_Balance"] = FClosingBags;
                                        ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal("3.10");
                                        ddt3.Rows[m]["Reserve_Bags"] = Convert.ToDecimal("0");
                                        ddt3.Rows[m]["Chargable_Bags"] = FOpeningBags + FRecBags;
                                        ddt3.Rows[m]["Charges"] = (FOpeningBags + FRecBags) * Convert.ToDecimal(3.10);                                       
                                        ddt3.Rows[m]["Godown_Id"] = "";
                                        m = 1;
                                    }
                                }
                             
                                Q = Q + 1;
                            }
                        }
                        }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                    }
                    if (ddt3.Rows.Count > 0)
                    {
                        gvNafedStorageCharge.DataSource = ddt3;
                        gvNafedStorageCharge.DataBind();
                        trRentBill.Visible = true;
                        btnGenerateBill.Enabled = false;
                       
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found..!');</script>");
                        trRentBill.Visible = false;
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found.!');</script>");
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
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
    protected void btnGenBill_Click(object sender, EventArgs e)
    {
        GetStorageBillNo();
        Insert_Bill_Daily_Detail();
        Insert_Bill_Detail();
        Report_Storage_Bill_Daily_Details();
    }
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
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

            if (gvNafedStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvNafedStorageCharge.Rows.Count - 1; i++)
                {
                    Dates = gvNafedStorageCharge.Rows[i].Cells[0].Text.ToString();
                    Opening_Balance = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[4].Text);
                    Reserve_Bags = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[5].Text);
                    Chargable_Bags = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[6].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[7].Text);
                    Total_Charges = Convert.ToDecimal(gvNafedStorageCharge.Rows[i].Cells[8].Text);
                    NetAmount = NetAmount + Total_Charges;
                    string qry = "";                

                    //qry = "INSERT INTO tbl_Bills_Fifteen_Day_Wise_Dtl(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'')";
                    qry = "INSERT INTO tbl_Bills_Fifteen_Day_Wise_Dtl([Bill_Number],[Dates_Period],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Reserve_Bags],[Chargable_Bags],[Rate],[Total_Charges],[CreatedBy],[CreatedDate]) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Reserve_Bags + "','" + Chargable_Bags + "','" + Per_Day_Rate + "','" + Total_Charges + "','"+ ip +"',getdate())";


                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ///////////////////Insert Godown detail/////////////////////
                    //Get_Bill_Type();
                    //string Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                    //string SG_ID = "";
                    //string HGodownId = "";
                    //if (Godown_Id != "&nbsp;")
                    //{
                    //    Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                    //    if (Godown_Id.Contains(","))
                    //    {
                    //        HGodownId = Godown_Id;
                    //        string[] G_Id = HGodownId.Split(',');
                    //        G_Id = G_Id.Distinct().ToArray();
                    //        for (int g = 0; g < G_Id.Length; g++)
                    //        {
                    //            SG_ID = G_Id[g];

                    //            if (SG_ID != "")
                    //            {
                    //                string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + SG_ID + "','" + Bill_Type + "')";
                    //                SqlCommand cmdg = new SqlCommand(qryg, con);
                    //                con.Open();
                    //                cmdg.ExecuteNonQuery();
                    //                con.Close();
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Godown_Id + "','" + Bill_Type + "')";
                    //        SqlCommand cmdg = new SqlCommand(qryg, con);
                    //        con.Open();
                    //        cmdg.ExecuteNonQuery();
                    //        con.Close();
                    //    }
                    //}
                    //else
                    //{
                    //    Godown_Id = "";
                    //}
                    ///////////////////Insert Godown detail/////////////////////
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
            string MDate15 = "15/" + MonthS + "/" + Year;
            string MDate16 = "16/" + MonthS + "/" + Year;
            string LastDate = LastDay + "/" + MonthS + "/" + Year;
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
        
            qry = "INSERT INTO [tbl_Storage_Bill_Details] ([Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Is_Rebate],[Created_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Crop_Year]) VALUES ('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','10535','3','" + ddlcomodity.SelectedValue + "','" + getDate_MDY(FirstDate) + "','" + getDate_MDY(LastDate) + "','1','5','" + ddlFinYear.SelectedItem.Text + "',6.20," + NetAmount + "," + NetAmount + ",0,0,'N',getdate(),'" + ip + "',3.10,'FD','" + BID + "','" + ddlmonth.SelectedValue + "','" + ddlcropyr.SelectedItem.Text + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
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
    public void Report_Storage_Bill_Daily_Details()
    {
        //string Month_Name = ViewState["MonthName"].ToString();
        string BranchID = Session["BranchID"].ToString();
        trReportsView.Visible = true;
        trRentBill.Visible = false;
        ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;
            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            ReportViewer_SC.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_SC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer_SC.ServerReport.ReportServerUrl = new Uri(reportURL);

            Bill_No = ViewState["BillNo"].ToString();
            NetAmountWord = (Convert_To_Word(NetAmount)).ToString();
            //decimal Comm_Rate = Convert.ToDecimal(txtcomrate.Text);
            ChargeOfTotal = Convert.ToDecimal(ViewState["ChargeOfTotal"]);

            ReportViewer_SC.ServerReport.ReportPath = folder + "/" + "Bill_NafedStorageCharges";

            ReportViewer_SC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_SC.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Branch_Id";
            reportParameterCollection[0].Values.Add(BranchID);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Bill_No";
            reportParameterCollection[1].Values.Add(Bill_No);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "NetAmountWord";
            reportParameterCollection[2].Values.Add(NetAmountWord);

            ReportViewer_SC.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_SC.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    [Serializable]

    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
        out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }

        // Specifies the user to impersonate when connecting to a report server. 
        //A WindowsIdentity object representing the user to impersonate.
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        // Returns network credentials to be used for authentication with the report server. 
        //A NetworkCredentials object.
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                //you can place below settings in configuration xml file
                //string userName = "Administrator";
                //string password = "nic123";
                //string domain_warehouseName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion
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
}
