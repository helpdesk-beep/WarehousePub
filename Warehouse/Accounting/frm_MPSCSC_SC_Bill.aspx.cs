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
//using WS_MPSCSCBillDetails;

public partial class Accounting_frm_MPSCSC_SC_Bill : System.Web.UI.Page
{
    string Depositor_ID = "129";
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
    //string NetAmountWord = "";
    //string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    //string Bill_Type = "";
    //int BID = 0;
    decimal ChargeOfTotalWeight = 0;
    DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    string Crop_Year = "";

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //SqlCommand cmd = new SqlCommand();
    //DataTable dt = new DataTable();
    //DataSet ds = new DataSet();
    //SqlDataAdapter da = new SqlDataAdapter();
    //string qry = "";


    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    //MPSCSC_ISBD.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_ISBD.MPSCSC_InstituitionStorageBillDetails();
    /*CSMS_WS.MPSCSC_InstituitionStorageBillDetails CSMSDemo = new CSMS_WS.MPSCSC_InstituitionStorageBillDetails();*/
    //WS_MPSCSCBillDetails.MPSCSC_BillDetails MPSCSCDemo = new WS_MPSCSCBillDetails.MPSCSC_BillDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                fillFinancialYear();
                fillMonth();
                GetGodown();
                trJVSGodownRent.Visible = true;
                fillYearNew();
                //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";

                //// ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Branch_Welcome.aspx';", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void Get_Rate()
    {
        //Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(ddlFyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
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
        Rate_M = 0;
        string RPM_Str = "";
        //txtcomrate.Text = Rate_M.ToString();
        //txtcomrate.Text = "93.60";
        if (ddlGodownType.SelectedValue != "5")
        {
            RPM_Str = Set_Rate();
        }
        else if (ddlGodownType.SelectedValue == "5")
        {
            RPM_Str = "24";
        }
        Rate_M = Convert.ToDecimal(RPM_Str);
        //txtcomrate.Text = "93.60";
        txtcomrate.Text = RPM_Str;
        //txtCPRate.Text = Math.Round((93.60 / Days_In_Month), 4).ToString();
        txtCPRate.Text = Math.Round((Rate_M / Days_In_Month), 4).ToString();
        //txtCPRate.Text = Math.Round((Rate_M / 30), 4).ToString();
        ViewState["Days_In_Month"] = Days_In_Month;
        if (ddlGodownType.SelectedValue == "7")
        {
            txtCPRate.Enabled = true;
            txtcomrate.Enabled = true;
        }
    }
    public string Set_Rate()
    {
        string CropYear = ddlCropYear.SelectedItem.Text;
        string CmdId = ddlcomodity.SelectedValue;
        string RatePM = "";
        if (CropYear == "2019-20" && CmdId == "22")
        {
            RatePM = "93.60";
        }
        else if (CropYear == "2020-21" && CmdId == "22")
        {
            RatePM = "104.20";
        }
        else if (CropYear == "2021-22" && CmdId == "22")
        {
            //RatePM = "107.80";
            RatePM = "104.20";
        }
        else if (CropYear == "2022-23" && CmdId == "22")
        {
            //RatePM = "107.80";
            RatePM = "104.20";
        }
        else if (CropYear == "2023-24" && CmdId == "22")
        {
            //RatePM = "107.80";
            //RatePM = "104.20";
            //RatePM = "99.20";
            RatePM = "102.00";
        }
        else if (CropYear == "2024-25" && CmdId == "22")
        {
            //RatePM = "107.80";
            //RatePM = "104.20";
            //RatePM = "101.60";
            RatePM = "102.00";
        }
        else if (CropYear == "2025-26" && CmdId == "22")
        {
            //RatePM = "107.80";
            //RatePM = "104.20";
            RatePM = "102.00";
        }
        else if (CropYear == "2026-27" && CmdId == "22")
        {
            //RatePM = "107.80";
            //RatePM = "104.20";
            //RatePM = "102.00";
            RatePM = "107.80";
        }
        else if (CropYear == "2018-19" && CmdId == "22")
        {
            RatePM = "86";
        }
        else if (CropYear == "2017-18" && CmdId == "22")
        {
            RatePM = "83";
        }
        else if (CropYear == "2016-17" && CmdId == "22")
        {
            RatePM = "74";
        }
        else if (CropYear == "2015-16" && CmdId == "22")
        {
            RatePM = "67.60";
        }
        else if (CropYear == "2014-15" && CmdId == "22")
        {
            RatePM = "61.40";
        }
        else if (CropYear == "2013-14" && CmdId == "22")
        {
            RatePM = "58.40";
        }
        else if (CropYear == "2012-13" && CmdId == "22")
        {
            RatePM = "54.60";
        }
        //other
        else if (CropYear == "2024-25" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            //RatePM = "107.8";
            //RatePM = "99.20";
            RatePM = "102.00";
        }
        else if (CropYear == "2025-26" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            //RatePM = "107.8";
            //RatePM = "99.20";
            RatePM = "102.00";
        }
        else if (CropYear == "2026-27" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            //RatePM = "107.8";
            //RatePM = "99.20";
            RatePM = "107.80";
        }
        else if (CropYear == "2023-24" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            //RatePM = "107.8";
            //RatePM = "99.20";
            RatePM = "102.00";
        }
        else if (CropYear == "2022-23" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            //RatePM = "107.8";
            RatePM = "99.20";
        }
        //21042023
        //if (CropYear == "2022-23" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129"))
        //{
        //    RatePM = "107.8";
        //}
        else if (CropYear == "2021-22" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "107.8";
        }

        else if (CropYear == "2020-21" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "107.8";
        }
        else if (CropYear == "2019-20" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "104.20";
        }
        else if (CropYear == "2018-19" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "86";
        }
        else if (CropYear == "2017-18" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "83";
        }
        else if (CropYear == "2016-17" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "74";
        }
        else if (CropYear == "2015-16" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "67.60";
        }
        else if (CropYear == "2014-15" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "61.40";
        }
        else if (CropYear == "2013-14" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
        {
            RatePM = "58.40";
        }
        //Sugar
        else if (CmdId == "46" || CmdId == "23")
        {
            //RatePM = "104.20";
            RatePM = "137";
        }
        //Salt
        else if (CmdId == "19" || CmdId == "122")
        {
            //RatePM = "104.20";
            RatePM = "100";

        }
        //Gunny
        //else if (CmdId == "25")
        //{
        //    RatePM = "46";
        //}

        return RatePM;
    }
    //public string Set_Rate()
    //{
    //    string CropYear = ddlCropYear.SelectedItem.Text;
    //    string CmdId = ddlcomodity.SelectedValue;
    //    string RatePM = "";
    //    if (CropYear == "2019-20" && CmdId == "22")
    //    {
    //        RatePM = "93.60";
    //        //RatePM = "107.80";
    //    }
    //    else if (CropYear == "2020-21" && CmdId == "22")
    //    {
    //        RatePM = "104.20";
    //        //RatePM = "107.80";
    //    }
    //    else if (CropYear == "2021-22" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        RatePM = "104.20";
    //    }
    //    else if (CropYear == "2022-23" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        RatePM = "104.20";
    //    }
    //    else if (CropYear == "2023-24" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        //RatePM = "104.20";
    //        //RatePM = "99.20";
    //        //RatePM = "102.00";
    //    }
    //    else if (CropYear == "2024-25" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        //RatePM = "104.20";
    //        //RatePM = "101.60";
    //        //RatePM = "102.00";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2025-26" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        //RatePM = "104.20";
    //        //RatePM = "102.00";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2026-27" && CmdId == "22")
    //    {
    //        //RatePM = "107.80";
    //        //RatePM = "104.20";
    //        //RatePM = "102.00";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2018-19" && CmdId == "22")
    //    {
    //        RatePM = "86";
    //    }
    //    else if (CropYear == "2017-18" && CmdId == "22")
    //    {
    //        RatePM = "83";
    //    }
    //    else if (CropYear == "2016-17" && CmdId == "22")
    //    {
    //        RatePM = "74";
    //    }
    //    else if (CropYear == "2015-16" && CmdId == "22")
    //    {
    //        RatePM = "67.60";
    //    }
    //    else if (CropYear == "2014-15" && CmdId == "22")
    //    {
    //        RatePM = "61.40";
    //    }
    //    else if (CropYear == "2013-14" && CmdId == "22")
    //    {
    //        RatePM = "58.40";
    //    }
    //    else if (CropYear == "2012-13" && CmdId == "22")
    //    {
    //        RatePM = "54.60";
    //    }
    //    //other
    //    else if (CropYear == "2024-25" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.8";
    //        //RatePM = "99.20";
    //        //RatePM = "102.00";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2025-26" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.8";
    //        //RatePM = "99.20";
    //        //RatePM = "102.00";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2026-27" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.8";
    //        //RatePM = "99.20";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2023-24" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.80";
    //        //RatePM = "99.20";
    //        //RatePM = "102.00";
    //    }
    //    else if (CropYear == "2022-23" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.8";
    //        //RatePM = "99.20";
    //        RatePM = "107.80";
    //    }
    //    //21042023
    //    //if (CropYear == "2022-23" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129"))
    //    //{
    //    //    RatePM = "107.8";
    //    //}
    //    else if (CropYear == "2021-22" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.8";
    //    }

    //    else if (CropYear == "2020-21" && (CmdId == "13" || CmdId == "3" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.8";
    //    }
    //    else if (CropYear == "2019-20" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "104.20";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2018-19" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "86";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2017-18" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "83";
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2016-17" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "74";
    //    }
    //    else if (CropYear == "2015-16" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "67.60";
    //    }
    //    else if (CropYear == "2014-15" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "61.40";
    //    }
    //    else if (CropYear == "2013-14" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "58.40";
    //    }
    //    //Sugar
    //    else if (CmdId == "46" || CmdId == "23")
    //    {
    //        //RatePM = "104.20";
    //        RatePM = "137";
    //    }
    //    //Salt
    //    else if (CmdId == "19" || CmdId == "122")
    //    {
    //        //RatePM = "104.20";
    //        RatePM = "100";

    //    }
    //    //Gunny
    //    //else if (CmdId == "25")
    //    //{
    //    //    RatePM = "46";
    //    //}

    //    return RatePM;
    //}
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
            //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' order by Godown_Name asc";
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type='Tribal Scheme' order by Godown_Name asc";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "7")
        {
            //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' order by Godown_Name asc";
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type='CAP-PMS' order by Godown_Name asc";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "8")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('BOT','BOT-AUB') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "9")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('PVT.PEG') order by Godown_Name asc";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "10")
        {
            qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN_2018 where DistrictId='23" + Dist_id + "' and BranchID='" + Session["BranchID"].ToString() + "' and Hired_Type in ('CWC') order by Godown_Name asc";
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
            //if (ddlGodownType.SelectedValue.ToString() == "1")
            //{
            //    ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
            //}
            //else if (ddlGodownType.SelectedValue.ToString() == "2")
            //{
            //ddlGodown2.DataSource = ds.Tables[0];
            //ddlGodown2.DataTextField = "Godown_Name";
            //ddlGodown2.DataValueField = "Godown_ID";
            //ddlGodown2.DataBind();
            //ddlGodown2.Items.Insert(0, "--Select--");

            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
            //}
        }
    }
    void GetCommodity()
    {

        //string verity = ddlverity.SelectedValue;
        //qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id in ('22','13','8','11','12','3','19','46','25','23','122')";
        qry = "select distinct Commodity_Id,Commodity_Name from View_WHRcurrentstock where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Depositor_Name='MPSCSC' and Commodity_Id in ('22','13','8','11','12','3','19','46','25','23','122','129','131')";

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
    void GetFYear()
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
    protected void txtcomrate_TextChanged(object sender, EventArgs e)
    {
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
        Rate_M = 0;
        //txtcomrate.Text = Rate_M.ToString();
        //txtcomrate.Text = "93.60";
        txtcomrate.Text = txtcomrate.Text;
        decimal AR = Convert.ToDecimal(txtcomrate.Text);
        //txtCPRate.Text = Math.Round((93.60 / Days_In_Month), 4).ToString();
        txtCPRate.Text = Math.Round((AR / Days_In_Month), 4).ToString();
        //txtCPRate.Text = Math.Round((Rate_M / 30), 4).ToString();
        ViewState["Days_In_Month"] = Days_In_Month;
        /////
        //Days_In_Month = Convert.ToInt32(ViewState["Days_In_Month"]);
        //Rate_M = Convert.ToDecimal(txtcomrate.Text);
        //decimal Rate_D = Rate_M / Days_In_Month;
        //decimal Rate_D = Rate_M / 30;
        //txtCPRate.Text = Math.Round(Rate_D, 4).ToString();
    }
    protected void fillFinancialYear()
    {

        ddlFyear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        ddlFyear.Items.Add((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //trJVSGodownRent.Visible = true;
        GetGodown();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
        btnGenBill.Enabled = true;
        txtcomrate.Text = "0";
        txtCPRate.Text = "0";
        txtcomrate.Enabled = false;
        txtCPRate.Enabled = false;
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCropYear();
        txtcomrate.Text = "0";
        txtCPRate.Text = "0";
        txtcomrate.Enabled = true;
        txtCPRate.Enabled = true;
        fillMonth();
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
    protected void fillYearNew()
    {
        //ddlmonth.ClearSelection();
        ddlFYearNew.Items.Clear();
        ddlFYearNew.Items.Add(new ListItem("--Select--", "0"));
        ddlFYearNew.Items.Add(new ListItem("2027", "2027"));
        ddlFYearNew.Items.Add(new ListItem("2026", "2026"));
        ddlFYearNew.Items.Add(new ListItem("2025", "2025"));
        ddlFYearNew.Items.Add(new ListItem("2024", "2024"));
        ddlFYearNew.Items.Add(new ListItem("2023", "2023"));
        ddlFYearNew.Items.Add(new ListItem("2022", "2022"));
        ddlFYearNew.Items.Add(new ListItem("2021", "2021"));
        ddlFYearNew.Items.Add(new ListItem("2020", "2020"));
        ddlFYearNew.Items.Add(new ListItem("2019", "2019"));
        ddlFYearNew.SelectedIndex = 0;
    }
    protected void btnSumbmitRent_Click(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text != "--Select--")
        {
            GetStorageBillNo();
            GetStorageDailyChargesBillDetail();
        }
    }
    public void GetStorageBillNo()
    {
        //string Depositor_ID = "129";
        Depositor_ID = "129";
        string BillSubType = "";
        Crop_Year = ddlCropYear.SelectedItem.Text;
        string Crop_YearEnd = Crop_Year.Substring(Crop_Year.Length - 2, 2);
        string CommodityId = ddlcomodity.SelectedValue;
        if (ddlGodownType.SelectedValue.ToString() == "1")
        {
            BillSubType = "1";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            BillSubType = "2";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "3")
        {
            BillSubType = "3";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "4")
        {
            BillSubType = "4";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "5")
        {
            BillSubType = "5";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "6")
        {
            BillSubType = "6";
        }
        else if (ddlGodownType.SelectedValue.ToString() == "7")
        {
            BillSubType = "7";
        }
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
        string FinYear = ddlFyear.SelectedItem.Text;
        FinYear = NineCrop(FinYear);
        string BranchID = Session["BranchID"].ToString();
        //qry = "select max(BId) as BId from tbl_Institution_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "'";
        qry = "select max(BId) as BId from tbl_Institution_Storage_Bill_Details where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "' and Financial_Year='" + FinYear + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";
        string Godown_Id = ddlgodown.SelectedValue.ToString();
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + SubBN.ToString();
                Bill_No = Crop_YearEnd + BillSubType + Godown_Id + CommodityId + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                BID = SubBN;
            }
            else
            {
                //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
                Bill_No = Crop_YearEnd + BillSubType + Godown_Id + CommodityId + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            //Bill_No = ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + BillSubType + Godown_Id + "1";
            Bill_No = Crop_YearEnd + BillSubType + Godown_Id + CommodityId + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";

            BID = 1;
        }
        //ViewState["BillNo"] = Bill_No;
        //if (ddlGodownType.SelectedValue == "1")
        //{
        //    ViewState["BillNo"] = "91"+Bill_No;
        //}
        //else
        //{
        ViewState["BillNo"] = Bill_No;
        //}
        ViewState["BID"] = BID;
    }
    public void GetStorageDailyChargesBillDetail()
    {
        try
        {
            Depositor_ID = "129";
            //CFnancialYear = ViewState["CFnancialYear"].ToString();
            //CFnancialYear = ddlFyear.SelectedValue;

            //CFnancialYear = "2019";
            CFnancialYear = ddlFYearNew.SelectedItem.Text;
            int ContMD = 0;
            //ContMD = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            ContMD = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            DateTime LDateTime = new DateTime();
            //DateTime StartDate = new DateTime();
            //DateTime EndDate = new DateTime();
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
            ViewState["SD"] = SD;
            ViewState["ED"] = ED;

            //DateTime LDateTime = new DateTime();
            //DateTime StartDate = new DateTime();
            //DateTime EndDate = new DateTime();
            //StartDate = Convert.ToDateTime(getDate_MDY(txtfdate.Text));
            //EndDate = Convert.ToDateTime(getDate_MDY(txttodate.Text));

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
         new DataColumn("Per_Day_Rate_Weight",typeof(decimal)),
        new DataColumn("Charges_Weight",typeof(decimal)),
        });

            string BranchID = Session["BranchID"].ToString();
            string str = "";
            if ((ddlCropYear.SelectedItem.Text == "2019-20") && ddlcomodity.SelectedValue == "22")
            {
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 where BranchID='" + BranchID + "')";
                //Valid
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 where GodownID='" + ddlgodown.SelectedValue + "')";
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] WITH (NOLOCK) where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 WITH (NOLOCK) where GodownID='" + ddlgodown.SelectedValue + "')";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] WITH (NOLOCK) where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'";

                //Test
                //str = "select * from [tbl_View_DateWiseDepositDelivery]";

            }
            else if ((ddlCropYear.SelectedItem.Text == "2020-21") && ddlcomodity.SelectedValue == "22")
            {
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2019 where GodownID='" + ddlgodown.SelectedValue + "')";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  --and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2020 where GodownID='" + ddlgodown.SelectedValue + "')";

            }
            else if ((ddlCropYear.SelectedItem.Text == "2021-22") && ddlcomodity.SelectedValue == "22")
            {
                //str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'  --and Depositor_WHR_Id in (select Depositor_WHR_Id from View_Digitally_Signed_WHR_Wheat2020 where GodownID='" + ddlgodown.SelectedValue + "')";
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'";

            }
            else
            {
                str = "SELECT [Depositor_WHR_Id],[commodity],[Depositor_Name],[WHR_Issue_Date] as WHR_Issue_Date,[recbags],[delbags],recweght as Rec_Weight,(delwght-Gain+Loss) as Del_Weight,[DeliveryDate] as DeliveryDate,Godown_ID,[datecom],[BranchID],[Branch],[Commodity_Id],CONVERT(bigint,ROW_NUMBER() OVER(ORDER BY datecom)) ROW_NUM FROM [Intergrated_MP_STORAGE].[dbo].[View_DateWiseDepositDelivery] where BranchID='" + BranchID + "' and Depositor_Name='MPSCSC' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and CropYear='" + ddlCropYear.SelectedItem.Text + "' and Godown_ID='" + ddlgodown.SelectedValue + "'";
            }
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
                //test for anupur
                //if ((StartDate < Static_date && Static_date_MM != "04" && StartDate_MM != "04") || (StartDate > Static_date && Static_date_MM != "09" && StartDate_MM != "09"))
                //if ((StartDate > Static_date && Static_date_MM != "09" && StartDate_MM != "09"))
                //if (StartDate < Static_date && Static_date_MM == "04" && StartDate_MM == "04")
                if (StartDate_MM == "04")
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    ////trOverAboveDaily.Visible = false;
                    //trRentBill.Visible = false;
                    ////trReportsView.Visible = false;
                    //btnGenBill.Visible = false;
                    //btncancel2.Visible = false;
                    //Past from else 
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            //if (i == 90)
                            //{
                            //    Static_date = Convert.ToDateTime(dt.Rows[i]["datecom"].ToString());
                            //}

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

                            //else if (Recent_Static_date != Static_date)
                            else if ((Recent_Static_date != Static_date) && Recent_Static_date <= DateTime.Now && Static_date <= DateTime.Now)
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
                        for (int i = dt.Rows.Count - 1; i <= dt.Rows.Count + 4000; i++)
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
                                ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
                                ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Charges"]);

                                ddt3.Rows[m]["Opening_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]) / 10);
                                //ddt3.Rows[m]["Opening_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]) / 10),4);
                                ddt3.Rows[m]["Receive_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]) / 10);
                                //ddt3.Rows[m]["Receive_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]) / 10),4);
                                ddt3.Rows[m]["Issue_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]) / 10);
                                //ddt3.Rows[m]["Issue_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]) / 10), 4);
                                ddt3.Rows[m]["Closing_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10);
                                //ddt3.Rows[m]["Closing_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10),4);
                                //ddt3.Rows[m]["Weight_Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Weight_Charges"]);
                                ddt3.Rows[m]["Godown_Id"] = dw.Rows[l]["Godown_Id"].ToString();
                                //New
                                ddt3.Rows[m]["Per_Day_Rate_Weight"] = Convert.ToDecimal(txtCPRate.Text);
                                //decimal PerDayWeightCharges = ((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text));
                                decimal PerDayWeightCharges = Math.Round(((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text)), 4);
                                //decimal PerDayWeightCharges = ((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text));

                                //Math.Round(a, 2);

                                ddt3.Rows[m]["Charges_Weight"] = Convert.ToDecimal(PerDayWeightCharges);
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
                        gvIStorageCharge.DataSource = ddt3;
                        gvIStorageCharge.DataBind();
                        Button1.Visible = true;
                        //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                        //{
                        //gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[8].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[9].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[10].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[11].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[12].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[13].Visible = false;
                        //}
                        //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                        //{
                        gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[8].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[9].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[10].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[11].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[12].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[13].Visible = true;

                        gvIStorageCharge.HeaderRow.Cells[1].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[2].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[3].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[4].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[5].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[6].Visible = false;
                        //    ;
                        //}
                        decimal total1 = 0;
                        for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                        {

                            //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                            //{
                            //    gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[8].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[9].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[10].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[11].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[12].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[13].Visible = false;
                            //}
                            //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                            //{
                            gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[1].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[2].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[3].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[4].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[5].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[6].Visible = false;
                            total1 += Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);



                            //}
                        }
                        //decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Charges_Weight"));
                        hdnAmt.Value = total1.ToString();
                        trRentBill.Visible = true;
                        //trReportsView.Visible = false;
                        btnGenBill.Visible = true;
                        btncancel2.Visible = true;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                        Button1.Visible = false;
                        hdnAmt.Value = "0";

                    }
                }
                //else if (StartDate < Static_date && Static_date_MM != "04" && StartDate_MM != "04")
                //else if (Static_date_MM != "04" && StartDate_MM != "04")
                //else if (StartDate_MM != "04")
                else if (Depositor_ID == "129")
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid From Date(There are No Deposit/Delivery)...'); </script> ");
                    ////trOverAboveDaily.Visible = false;
                    //trRentBill.Visible = false;
                    ////trReportsView.Visible = false;
                    //btnGenBill.Visible = false;
                    //btncancel2.Visible = false;
                    //For other than April Start
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
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

                            //else if (Recent_Static_date != Static_date)
                            else if ((Recent_Static_date != Static_date) && Recent_Static_date <= DateTime.Now && Static_date <= DateTime.Now)
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
                                ddt3.Rows[m]["Per_Day_Rate"] = Convert.ToDecimal(ddt2.Rows[l]["Per_Day_Rate"]);
                                ddt3.Rows[m]["Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Charges"]);

                                ddt3.Rows[m]["Opening_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]) / 10);
                                //ddt3.Rows[m]["Opening_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Opening_Weight"]) / 10),4);
                                ddt3.Rows[m]["Receive_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]) / 10);
                                //ddt3.Rows[m]["Receive_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Receive_Weight"]) / 10),4);
                                ddt3.Rows[m]["Issue_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]) / 10);
                                //ddt3.Rows[m]["Issue_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Issue_Weight"]) / 10), 4);
                                ddt3.Rows[m]["Closing_Weight"] = (Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10);
                                //ddt3.Rows[m]["Closing_Weight"] = Math.Round((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10),4);
                                //ddt3.Rows[m]["Weight_Charges"] = Convert.ToDecimal(ddt2.Rows[l]["Weight_Charges"]);
                                ddt3.Rows[m]["Godown_Id"] = dw.Rows[l]["Godown_Id"].ToString();
                                //New
                                ddt3.Rows[m]["Per_Day_Rate_Weight"] = Convert.ToDecimal(txtCPRate.Text);
                                //decimal PerDayWeightCharges = ((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text));
                                decimal PerDayWeightCharges = Math.Round(((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text)), 4);
                                //decimal PerDayWeightCharges = ((Convert.ToDecimal(ddt2.Rows[l]["Closing_Weight"]) / 10) * Convert.ToDecimal(txtCPRate.Text));

                                //Math.Round(a, 2);

                                ddt3.Rows[m]["Charges_Weight"] = Convert.ToDecimal(PerDayWeightCharges);
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
                        gvIStorageCharge.DataSource = ddt3;
                        gvIStorageCharge.DataBind();
                        //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                        //{
                        //gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[8].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[9].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[10].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[11].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[12].Visible = false;
                        //    gvIStorageCharge.HeaderRow.Cells[13].Visible = false;
                        //}
                        //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                        //{
                        gvIStorageCharge.HeaderRow.Cells[7].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[8].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[9].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[10].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[11].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[12].Visible = true;
                        gvIStorageCharge.HeaderRow.Cells[13].Visible = true;

                        gvIStorageCharge.HeaderRow.Cells[1].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[2].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[3].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[4].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[5].Visible = false;
                        gvIStorageCharge.HeaderRow.Cells[6].Visible = false;
                        //    ;
                        //}
                        decimal total1 = 0;
                        for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                        {

                            //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                            //{
                            //    gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[8].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[9].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[10].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[11].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[12].Visible = false;
                            //    gvIStorageCharge.Rows[i].Cells[13].Visible = false;
                            //}
                            //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                            //{
                            gvIStorageCharge.Rows[i].Cells[7].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[1].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[2].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[3].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[4].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[5].Visible = false;
                            gvIStorageCharge.Rows[i].Cells[6].Visible = false;
                            total1 += Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);

                            //}
                        }
                        //decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Charges_Weight"));
                        hdnAmt.Value = total1.ToString();
                        trRentBill.Visible = true;
                        //trReportsView.Visible = false;
                        btnGenBill.Visible = true;
                        btncancel2.Visible = true;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Record Not Found...!');</script>");
                        hdnAmt.Value = "0";
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Something record issue...!');</script>");
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
    protected void btnGenBill_Click(object sender, EventArgs e)
    {
        if (CheckFinYear())
        {
            if (CheckDupliCate())
            {
                if (Convert.ToDecimal(hdnAmt.Value) > 0)
                {

                    Insert_Bill_Daily_Detail();
                    Insert_Bill_Detail();
                    Lblmsg2.Text = "Your Bill Successfully Generated. Bill No : " + Bill_No;
                    btnGenBill.Enabled = false;
                    Button1.Visible = true;
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
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('वित्तीय वर्ष सही नहीं है, कृपया सही वित्तीय वर्ष चुनें| ');</script>");
        }
        //Report_Storage_Bill_Daily_Details();
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
            decimal Opening_Weight = 0;
            decimal Rec_Weight = 0;
            decimal Issue_Weight = 0;
            decimal Closing_Weight = 0;

            decimal Per_Day_Rate_Weight = 0;
            decimal Charges_Weight = 0;
            string CropYear = ddlCropYear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            Bill_Type = "AD";
            if (gvIStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                {
                    Dates = getDate_MDY(gvIStorageCharge.Rows[i].Cells[0].Text.ToString());
                    Opening_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[1].Text);
                    Rec_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[2].Text);
                    Issue_Bags = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[3].Text);
                    Closing_Balance = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[4].Text);
                    Per_Day_Rate = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[5].Text);
                    //Total_Charges = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[6].Text);
                    Total_Charges = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);
                    Opening_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[8].Text);
                    Rec_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[9].Text);
                    Issue_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[10].Text);
                    Closing_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[11].Text);

                    Per_Day_Rate_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[12].Text);
                    Charges_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);
                    //string qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    string qry = "";
                    //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
                    //{
                    //qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Client_Ip,Godown_Id) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + ip + "','" + ddlgodown.SelectedValue + "')";
                    //qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Client_Ip,Godown_Id) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + ip + "','" + ddlgodown.SelectedValue + "')";
                    qry = "INSERT INTO tbl_Bill_Institution_Daily_Charges(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight,Client_Ip,Godown_Id,Commodity_ID,Crop_Year,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate + "','" + Total_Charges + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "','" + ip + "','" + ddlgodown.SelectedValue + "','" + ddlcomodity.SelectedValue.ToString() + "','" + CropYear + "','" + Bill_Type + "')";

                    //}
                    //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
                    //{
                    //    qry = "INSERT INTO tbl_Bills_Daily_Storage_Charges_Details(Bill_Number,Dates,Opening_Balance,Receive_Bags,Issue_Bags,Closing_Balance,Per_Day_Rate,Total_Charges,Created_Date,Modified_Date,Opening_Weight,Receive_Weight,Issue_Weight,Closing_Weight) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Opening_Balance + "','" + Rec_Bags + "','" + Issue_Bags + "','" + Closing_Balance + "','" + Per_Day_Rate_Weight + "','" + Charges_Weight + "',getdate(),'','" + Opening_Weight + "','" + Rec_Weight + "','" + Issue_Weight + "','" + Closing_Weight + "')";
                    //}
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //Web Service Call For data to MPSCSC


                    //try
                    //{
                    //    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                    //    MPSCSCDemo.EDAddInstitutionStorageBillCharges(ViewState["BillNo"].ToString(), Dates, Opening_Balance, Rec_Bags, Issue_Bags, Closing_Balance, Per_Day_Rate, Total_Charges, DateTime.Now.ToString(), DateTime.Now.ToString(), Opening_Weight, Rec_Weight, Issue_Weight, Closing_Weight, ip, (ddlgodown.SelectedValue.ToString()), Convert.ToInt32(ddlcomodity.SelectedValue.ToString()), CropYear, Bill_Type);

                    //}
                    //catch (Exception)
                    //{
                    //    //throw;
                    //}
                    //finally {
                    //    con.Close();
                    //}

                    try
                    {
                        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                        MPSCSCDemo.EDAddInstitutionStorageBillCharges(ViewState["BillNo"].ToString(), Dates, Opening_Balance, Rec_Bags, Issue_Bags, Closing_Balance, Per_Day_Rate, Total_Charges, DateTime.Now.ToString(), DateTime.Now.ToString(), Opening_Weight, Rec_Weight, Issue_Weight, Closing_Weight, ip, (ddlgodown.SelectedValue.ToString()), Convert.ToInt32(ddlcomodity.SelectedValue.ToString()), CropYear, Bill_Type);

                    }
                    catch (System.Net.WebException ex)
                    {
                        // 2. THE CODE JUMPS STRAIGHT HERE to handle the ASMX server/network issue
                        //Response.Write("<script>alert('Server or network error occurred.');</script>");
                    }
                    catch (Exception ex)
                    {
                        // OR HERE if it is a local conversion/null error
                        //Response.Write("<script>alert('General error: {ex.Message}');</script>");
                    }
                    finally
                    {
                        // 3. THIS ALWAYS RUNS NEXT (whether an error happened or not)
                        if (con != null && con.State != System.Data.ConnectionState.Closed)
                        {
                            con.Close();
                        }
                    }



                    ///////////////////Insert Godown detail/////////////////////
                    Get_Bill_Type();
                    string Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                    string SG_ID = "";
                    string HGodownId = "";
                    if (Godown_Id != "&nbsp;")
                    {
                        Godown_Id = gvIStorageCharge.Rows[i].Cells[7].Text.ToString();
                        if (Godown_Id.Contains(","))
                        {
                            HGodownId = Godown_Id;
                            string[] G_Id = HGodownId.Split(',');
                            G_Id = G_Id.Distinct().ToArray();
                            for (int g = 0; g < G_Id.Length; g++)
                            {
                                SG_ID = G_Id[g];

                                //if (SG_ID != "")
                                //{
                                //    string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + SG_ID + "','" + Bill_Type + "')";
                                //    SqlCommand cmdg = new SqlCommand(qryg, con);
                                //    con.Open();
                                //    cmdg.ExecuteNonQuery();
                                //    con.Close();
                                //}
                            }
                        }
                        else
                        {
                            string qryg = "INSERT INTO tbl_Storage_Bills_Godown_Details(Bill_Number,Oper_Date,Godown_Id,Bill_Type) values('" + ViewState["BillNo"] + "','" + Dates + "','" + Godown_Id + "','" + Bill_Type + "')";
                            SqlCommand cmdg = new SqlCommand(qryg, con);
                            con.Open();
                            cmdg.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        Godown_Id = "";
                    }
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
    public void Get_Bill_Type()
    {
        //if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "1")
        //{
        //    Bill_Type = "AD";
        //}
        //else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
        //{
        //    Bill_Type = "AU";
        //}
        //else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "1")
        //{
        //    Bill_Type = "OD";
        //}
        //else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
        //{
        //    Bill_Type = "OU";
        //}
        //else if (ddlBillType.SelectedValue.ToString() == "2" && ddlCalcTy.SelectedValue.ToString() == "2")
        //{
        //    Bill_Type = "RB";
        //}
        Bill_Type = "AD";
    }
    public void Insert_Bill_Detail()
    {
        try
        {

            //if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
            //{
            //    GetRebateDiscount();
            //}
            //if (ddlCalcTy.SelectedValue.ToString() != "3")
            //{
            GetRebateDiscount();
            //}
            string qry = "";
            Bill_No = ViewState["BillNo"].ToString();
            string Is_Rebate = "N";
            //string DeposCategory = "";
            string DeposCategory = "";
            if (ddlGodownType.SelectedItem.Text == "MPWLC Godowns")
            {
                DeposCategory = "MPWLC";
            }
            else if (ddlGodownType.SelectedItem.Text == "JVS Godowns")
            {
                DeposCategory = "PVT";
            }
            else if (ddlGodownType.SelectedItem.Text == "Hired Godowns")
            {
                DeposCategory = "PVT";
            }
            else if (ddlGodownType.SelectedItem.Text == "Silo Bags")
            {
                DeposCategory = "PVT";
            }
            else if (ddlGodownType.SelectedItem.Text == "Tribal Scheme" || ddlGodownType.SelectedItem.Text == "CAP-PMS")
            {
                DeposCategory = "PVT";
            }

            decimal RebatePer = 0;
            decimal NoBags = 0;
            string Rin_Pustika_No = "";
            string Cast_Certificate_No = "";

            string CropYear = ddlCropYear.SelectedItem.Text;
            string FinYear = ddlFyear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            FinYear = NineCrop(FinYear);
            //if ((ddlDeposCategory.SelectedItem.Text != "--Select--" && ddlronbags.SelectedItem.Text != "--Select--" && txtdiscount.Text != "" && txtkhasrano.Text != ""))
            //{
            Is_Rebate = "N";
            //DeposCategory = ddlDeposCategory.SelectedItem.Text;
            //DeposCategory = "";
            //RebatePer = Convert.ToDecimal(txtdiscount.Text);
            RebatePer = 0;
            //NoBags = Convert.ToDecimal(ddlronbags.SelectedValue);
            NoBags = 0;
            //}
            //if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
            //{
            //txtcomrate.Text = "0";
            //txtCPRate.Text = "0";
            //Rin_Pustika_No = txtrpn.Text;
            Rin_Pustika_No = "0";
            //Cast_Certificate_No = txtCastCert.Text;
            Cast_Certificate_No = "0";
            //}
            //decimal SerTax = (ChargeOfTotal * Convert.ToDecimal(txtstax.Text)) / 100;
            //Previous
            decimal SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;



            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString();
            string BranchID = Session["BranchID"].ToString();
            string VStartDate = ViewState["SD"].ToString();
            string VEndDate = ViewState["ED"].ToString();
            Get_Bill_Type();
            BID = Convert.ToInt32(ViewState["BID"]);
            //For Silo Bags Only
            decimal MPWLC_SC = 0;
            //Previous
            //MPWLC_SC = Math.Round(((Math.Round(ChargeOfTotalWeight) * 10) / 100));
            if (ddlGodownType.SelectedItem.Text == "CAP-PMS")
            {
                MPWLC_SC = Math.Round(((Math.Round(ChargeOfTotalWeight) * 15) / 100));
            }
            else
            {
                MPWLC_SC = Math.Round(((Math.Round(ChargeOfTotalWeight) * 10) / 100));
            }
            decimal GST_Per_SC = 18;
            decimal GST_Amt_SC = 0;
            decimal Closing_Weight = 0;
            GST_Amt_SC = Math.Round((MPWLC_SC * GST_Per_SC) / 100);
            //
            //Rounding
            decimal Rount_NetAmount = Math.Round(NetAmount);
            NetAmount = Rount_NetAmount;
            decimal Rount_ChargeOfTotalWeight = Math.Round(ChargeOfTotalWeight);
            ChargeOfTotalWeight = Rount_ChargeOfTotalWeight;
            if (gvIStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                {
                    Closing_Weight = Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[11].Text);
                }
            }
            if (ddlGodownType.SelectedValue.ToString() != "4" && ddlGodownType.SelectedValue.ToString() != "6" && ddlGodownType.SelectedValue.ToString() != "7")
            {
                qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlgodown.SelectedValue + "')";
            }
            else if (ddlGodownType.SelectedValue.ToString() == "4")
            {
                NetAmount = NetAmount + MPWLC_SC + GST_Amt_SC;

                qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlgodown.SelectedValue + "','" + MPWLC_SC + "','" + GST_Per_SC + "','" + GST_Amt_SC + "')";
            }
            else if (ddlGodownType.SelectedValue.ToString() == "6" || ddlGodownType.SelectedValue.ToString() == "7")
            {
                //NetAmount = NetAmount + MPWLC_SC;
                NetAmount = NetAmount + MPWLC_SC + GST_Amt_SC;

                //qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlgodown.SelectedValue + "','" + MPWLC_SC + "',0,0)";
                qry = "INSERT INTO tbl_Institution_Storage_Bill_Details(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Godown_Id,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + txtcomrate.Text + "," + NetAmount + "," + ChargeOfTotalWeight + ",'" + txtstax.Text + "'," + SerTax + ",'" + DeposCategory + "'," + RebatePer + "," + RebateAmount + "," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + txtCPRate.Text + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlgodown.SelectedValue + "','" + MPWLC_SC + "','" + GST_Per_SC + "','" + GST_Amt_SC + "')";

            }
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            cmd.ExecuteNonQuery();
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            //Web Service Call For data to MPSCSC


            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
            MPSCSCDemo.EDAddInstitutionStorageBillDetails(Bill_No, Dist_id, BranchID, "4", "129", "3", ddlcomodity.SelectedValue.ToString(), getDate_MDY(VStartDate), getDate_MDY(VEndDate), "1", "5", FinYear, Convert.ToDecimal(txtcomrate.Text), NetAmount
            , ChargeOfTotalWeight, Convert.ToDecimal(txtstax.Text), SerTax, DeposCategory, RebatePer, RebateAmount, Convert.ToInt32(NoBags), "0", Rin_Pustika_No, Cast_Certificate_No, Is_Rebate, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, Convert.ToDecimal(txtCPRate.Text), Bill_Type, BID, Convert.ToInt32(ddlmonth.SelectedValue)
            , ddlgodown.SelectedValue, CropYear, MPWLC_SC, GST_Per_SC, GST_Amt_SC);
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
    public void GetRebateDiscount()
    {
        try
        {
            string BranchID = Session["BranchID"].ToString();
            //if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "3")
            //{
            //}
            //else if (ddlBillType.SelectedValue.ToString() == "1" && ddlCalcTy.SelectedValue.ToString() == "1")
            //{
            //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
            //{
            //if (gvIStorageCharge.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
            //    {
            //        ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[6].Text);
            //    }
            //}
            //if (txtdiscount.Text != "")
            //{
            //    RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
            //}
            //else
            //{
            //RebateAmount = 0;
            //}
            //}
            //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
            //{
            if (gvIStorageCharge.Rows.Count > 0)
            {
                for (int i = 0; i <= gvIStorageCharge.Rows.Count - 1; i++)
                {
                    ChargeOfTotalWeight = ChargeOfTotalWeight + Convert.ToDecimal(gvIStorageCharge.Rows[i].Cells[13].Text);
                }
            }
            //    if (txtdiscount.Text != "")
            //    {
            //        RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
            //    }
            //    else
            //    {
            RebateAmount = 0;
            //    }
            //}
            //}
            //else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "1")
            //{
            //    if (gvOverAboveDaily.Rows.Count > 0)
            //    {
            //        for (int i = 0; i <= gvOverAboveDaily.Rows.Count - 1; i++)
            //        {
            //            ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvOverAboveDaily.Rows[i].Cells[8].Text);
            //        }
            //    }
            //    if (txtdiscount.Text != "")
            //    {
            //        RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
            //    }
            //    else
            //    {
            //        RebateAmount = 0;
            //    }
            //}
            //else if (ddlBillType.SelectedValue.ToString() == "3" && ddlCalcTy.SelectedValue.ToString() == "3")
            //{
            //    if (gvAccruedOverNAbove.Rows.Count > 0)
            //    {
            //        for (int i = 0; i <= gvAccruedOverNAbove.Rows.Count - 1; i++)
            //        {
            //            if (gvAccruedOverNAbove.Rows[i].Cells[10].Text != "&nbsp;")
            //            {
            //                ChargeOfTotal = ChargeOfTotal + Convert.ToDecimal(gvAccruedOverNAbove.Rows[i].Cells[10].Text);
            //            }
            //        }
            //    }
            //    if (txtdiscount.Text != "")
            //    {
            //        RebateAmount = (ChargeOfTotal * Convert.ToDecimal(txtdiscount.Text)) / 100;
            //    }
            //    else
            //    {
            //        RebateAmount = 0;
            //    }
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record found for this Period/Commodity...'); </script> ");
            //}
            //if (ddlBType.SelectedValue.ToString() == "B" && ddlBType.SelectedValue.ToString() != "-1")
            //{
            //decimal SerTax = (ChargeOfTotal * Convert.ToDecimal(txtstax.Text)) / 100;
            //NetAmount = Math.Round(((ChargeOfTotal + SerTax) - RebateAmount), 2);
            //NetAmountWord = Convert_To_Word(NetAmount);
            //}
            //else if (ddlBType.SelectedValue.ToString() == "W" && ddlBType.SelectedValue.ToString() != "-1")
            //{
            decimal SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;
            NetAmount = Math.Round(((ChargeOfTotalWeight + SerTax) - RebateAmount), 2);
            NetAmountWord = Convert_To_Word(NetAmount);
            //}


        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
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
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Existance = "";
        string Existance_CSMS = "";
        Existance = Check_Bill_Existance();
        Existance_CSMS = Check_Bill_Existance_InCSMS();
        if (Existance == "N" && Existance_CSMS == "N")
        {
            Get_Rate();
            //Enable/Disable
            if (ddlGodownType.SelectedItem.Text == "Silo Bags")
            {
                txtcomrate.Enabled = true;
                txtCPRate.Enabled = true;
                txtcomrate.Text = "0";
                txtCPRate.Text = "0";
            }
            else
            {
                txtcomrate.Enabled = true;
                txtCPRate.Enabled = true;
            }

        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('इस माह का बिल पहले से WMS मे बना है या बनकर CSMS मे सबमिट हो चुका है, चेक करे।...!');</script>");
            txtcomrate.Text = "";
            txtCPRate.Text = "";
            //Response.Redirect("./Accounting/frm_MPSCSC_SC_Bill.aspx");
        }
    }
    public bool CheckFinYear()
    {
        bool IsValid = true;
        if (ddlFYearNew.SelectedValue == "2027" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2026-27")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2026-27")
        {
            IsValid = true;
        }
       else if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2025-26")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2025-26")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2024-25")
        {
            IsValid = true;
        }

        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2024-25")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2023-24")
        {
            IsValid = true;
        }

        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2023-24")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2022-23")
        {
            IsValid = true;
        }

        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2022-23")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2021-22")
        {
            IsValid = true;
        }


        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2021-22")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2020-21")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2020-21")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2019-20")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2019-20")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3 && ddlFyear.SelectedValue == "2018-19")
        {
            IsValid = true;
        }
        else if (ddlFYearNew.SelectedValue == "2018" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4 && ddlFyear.SelectedValue == "2018-19")
        {
            IsValid = true;
        }
        else
        {
            IsValid = false;
        }
        return IsValid;

    }
    public string Check_Bill_Existance()
    {

        string Existance = "";

        if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2026-27";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2025-26";
            ddlFyear.Enabled = false;
        }
       else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2025-26";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2024-25";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2024-25";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2023-24";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2023-24";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2022-23";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2022-23";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2021-22";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2021-22";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2020-21";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2020-21";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2019-20";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2019-20";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2018-19";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2018" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2018-19";
            ddlFyear.Enabled = false;
        }
        else
        {
            ddlFyear.Enabled = true;
        }
        if (ddlFYearNew.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Please Select Bill Months Year...!');</script>");
        }
        else
        {
            string CropYear = ddlCropYear.SelectedItem.Text;
            string FinYear = ddlFyear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            FinYear = NineCrop(FinYear);
            //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details where Crop_Year='" + CropYear + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='AD'";
            //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details where Crop_Year='" + CropYear + "' and Financial_Year='"+ FinYear + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='AD'";
            qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details where Crop_Year='" + CropYear + "' and Financial_Year in ('" + FinYear + "','" + ddlFyear.SelectedItem.Text + "') and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='AD'";

            //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details where Crop_Year='" + CropYear + "' and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='AD'";


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
        }
        return Existance;
    }

    public string Check_Bill_Existance_InCSMS()
    {
        string Existance = "";
        if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2026-27";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2026" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2025-26";
            ddlFyear.Enabled = false;
        }
       else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2025-26";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2025" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2024-25";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2024-25";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2024" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2023-24";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2023-24";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2023" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2022-23";
            ddlFyear.Enabled = false;
        }

        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2022-23";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2022" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2021-22";
            ddlFyear.Enabled = false;
        }


        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2021-22";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2021" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2020-21";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2020-21";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2020" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2019-20";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2019-20";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2019" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
        {
            ddlFyear.SelectedValue = "2018-19";
            ddlFyear.Enabled = false;
        }
        else if (ddlFYearNew.SelectedValue == "2018" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
        {
            ddlFyear.SelectedValue = "2018-19";
            ddlFyear.Enabled = false;
        }
        else
        {
            ddlFyear.Enabled = true;
        }
        if (ddlFYearNew.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('Please Select Bill Months Year...!');</script>");
        }
        else
        {
            string CropYear = ddlCropYear.SelectedItem.Text;
            string FinYear = ddlFyear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            FinYear = NineCrop(FinYear);
            string BranchID = Session["BranchID"].ToString();
            //qry = "select Bill_Number from tbl_Institution_Storage_Bill_Details where Crop_Year='" + CropYear + "' and Financial_Year in ('" + FinYear + "','" + ddlFyear.SelectedItem.Text + "') and Month='" + ddlmonth.SelectedValue + "' and Commodity_Id='" + ddlcomodity.SelectedValue + "' and Godown_Id='" + ddlgodown.SelectedValue + "' and Bill_Type='AD'";
            //qry = "select * from mpscsc.dbo.tbl_Processing_StorageBill_AtDM r where r.Godown_Id=" + ddlgodown.SelectedValue + " and r.Branch_Id='"+ BranchID + "' and xyz.Crop_Year='"+ CropYear + "' and xyz.Month_No='" + ddlmonth.SelectedValue + "' and xyz.Year in ('" + FinYear + "','" + ddlFyear.SelectedItem.Text + "') and xyz.Commodity_Id='" + ddlcomodity.SelectedValue + "'";
            qry = "select * from mpscsc.dbo.tbl_Processing_StorageBill_AtDM r where r.Godown_Id='" + ddlgodown.SelectedValue + "' and r.Branch_Id='" + BranchID + "' and r.Crop_Year='" + CropYear + "' and r.Month_No='" + ddlmonth.SelectedValue + "' and r.Financial_Year in ('" + FinYear + "','" + ddlFyear.SelectedItem.Text + "') and r.Commodity_Id='" + ddlcomodity.SelectedValue + "'";

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
        }
        return Existance;
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMonth();
        txtcomrate.Text = "0";
        txtCPRate.Text = "0";
        txtcomrate.Enabled = true;
        txtCPRate.Enabled = true;
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
        else if (InCrop == "2026-27")
        {
            OutCrop = "2026-2027";
        }
        return OutCrop;
    }

    protected void brnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void btncancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_MPSCSC_SC_Bill.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_MPSCSC_SC_Bill.aspx");
    }
    private bool CheckDupliCate()
    {
        try
        {
            string VStartDate = ViewState["SD"].ToString();
            string VEndDate = ViewState["ED"].ToString();
            string str = "";
            string FinYear = NineCrop(ddlCropYear.SelectedValue);
            string CropYear = ddlCropYear.SelectedItem.Text;
            CropYear = NineCrop(CropYear);
            FinYear = NineCrop(FinYear);

            cmd = new SqlCommand("Check_Bill_Duplication", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_Id", ddlgodown.SelectedValue);
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcomodity.SelectedValue);
            //cmd.Parameters.AddWithValue("@Crop_Year", FinYear);
            cmd.Parameters.AddWithValue("@Crop_Year", CropYear);
            cmd.Parameters.AddWithValue("@Financial_Year", FinYear);
            cmd.Parameters.AddWithValue("@From_Date", VStartDate);
            cmd.Parameters.AddWithValue("@To_Date", VEndDate);
            cmd.Parameters.AddWithValue("@Bill_Type", "AD");

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
