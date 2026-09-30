using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Threading;

public partial class WarehouseLevel_PvtGReports_PvtGodownReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_whrDetail_godownwise";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_WHR_Wise_CPT_UTL";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWiseCommoditySummary";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptGodown_DO_Wise_Issue_Details";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_OpeningClosing_CommodityWise_BTWDate";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Wise_Stack_Capacity_Detail";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "OtherLogin_WheatProc1718";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_WhrFromDepositForm";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_CommodityWiseWHRdetails";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_CropYearWise_WHRdetails";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_StackStock";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PvtGodown_GatepassDetail_BetweenDates";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_DateWise_WHRDetails";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Delivery_Order_New";
        Response.Redirect("ReportViewer_Godown.aspx");
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "OtherLogin_Proc2018_19";
        Response.Redirect("ReportViewer_Godown.aspx");
    }

    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Rpt_Get_Rent_Bill_Details.aspx\",\"_blank\")", true);

    }

    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CropYearWise_GodownWise_Stock";
        Response.Redirect("ReportViewer_Godown.aspx");
    }

    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../PvtGReports/Pvt_Whr_Details_And_Available_Stock_For_Issue.aspx\",\"_blank\")", true);
        //Response.Redirect("~/Reports/Branch/Rpt_Whr_Details_And_Available_Stock_For_Issue.aspx");
    }
}
