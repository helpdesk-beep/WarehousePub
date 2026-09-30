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


public partial class IssueCenterLevel_Storage_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["lang"].ToString() == "Hindi")
        //{
        //    ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
        //    LinkButton1.Text = Resources.hindi.HyperLink1;
        //    LinkButton12.Text = Resources.hindi.HyperLink12;
        //    LinkButton2.Text = Resources.hindi.HyperLink2;
        //    LinkButton13.Text = Resources.hindi.HyperLink13;
        //    LinkButton4.Text = Resources.hindi.HyperLink4;
        //    LinkButton6.Text = Resources.hindi.HyperLink6;
        //    LinkButton7.Text = Resources.hindi.HyperLink7;
        //    //LinkButton8.Text = Resources.hindi.HyperLink8;
        //    LinkButton9.Text = Resources.hindi.HyperLink9;
        //    LinkButton18.Text = Resources.hindi.HyperLink18;
        //    LinkButton16.Text = Resources.hindi.HyperLink16;
        //    LinkButton19.Text = Resources.hindi.HyperLink19;
        //    LinkButton14.Text = Resources.hindi.HyperLink14;
        //    LinkButton15.Text = Resources.hindi.HyperLink15;
        //    LinkButton11.Text = Resources.hindi.HyperLink11;
        //    LinkButton10.Text = Resources.hindi.HyperLink10;
        //    LinkButton17.Text = Resources.hindi.HyperLink17;
        //    LinkButton5.Text = Resources.hindi.HyperLink5;
        //    LinkButton20.Text = Resources.hindi.LinkButton20;
        //    LinkButton3.Text = Resources.hindi.LinkButton3;

        //}
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDepositorLedger";
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockRegister";
        Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Delivery_Order_New";
        Response.Redirect("~/Reports/Depot/ReportViewer_Depot.aspx");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptWarehouseReceipt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStackWiseConditionReportRegister";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);

    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_gatepassdetails";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    //protected void LinkButton18_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "rpt_GodownwiseStackPosition";
    //    Response.Redirect("ReportViewer_Depot.aspx");
    //}
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockValuationRegister";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        // Response.Redirect("~/Reports/Branch/rptStockValuationRegister.aspx");
    }
    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDetails_of_Daily_Issue_Commoditywise";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot_Dates.aspx\",\"_blank\")", true);

    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDaily_Commodity_Receipt_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        // Response.Redirect("~/Reports/Branch/rptDaily_Commodity_Receipt_Details.aspx");
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptSchemeWiseOutflow";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        // Response.Redirect("~/Reports/Branch/rptSchemeWiseOutflow.aspx");
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptTransactions";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        // Response.Redirect("~/Reports/Branch/rptTransactions.aspx");
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptTruckChallan_SendingDetails";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptAcknowledgement";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockRegister_GodownNCommodity";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    //protected void LinkButton21_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "rptDetails_of_Daily_Issue_Commoditywise_ForAllCommodity";
    //   // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    //    Response.Redirect("ReportViewer_Depot.aspx");
    //}
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStackwiseRegister_ForAllStack";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GapassDetailReciverWise";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWiseRecort";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton28_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWisewhrdetailbwdates";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DepositerwiseWhrBwDates";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton30_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DepositorWiseReportTillMonth";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton31_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DepositorWiseGodownDetail";
        //  ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton32_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WhrFromDepositForm";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton33_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MpwlctotalAllwhr";
        //  ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownList";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton35_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "SocietyWiseWHR15";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rptwhrdetailsnew";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Receiptdetails";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton25_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_whrDetail_godownwise_new13";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "RptDailyReceiptAndReleaseRegister_Report";
        //  ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Branch/DailyReceiptRelease.aspx");
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDO_Wise_Issue_Details2";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton36_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "totalrecivingdetailbranch";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton37_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseWHRBranch";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton38_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DepositerWiseWHRRecord";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton39_Click(object sender, EventArgs e)
    {

    }
    //protected void LinkButton41_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "WHRandDepositerformdetails";
    //   // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    //    Response.Redirect("ReportViewer_Depot.aspx");
    //}
    protected void LinkButton42_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchWiseProcDtl";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton43_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/GraphicalReport.aspx");
    }
    protected void LinkButton44_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DOTOwiseGatepass";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton45_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WHRKillReport";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton46_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MenualWHHRDetailBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton47_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Reservation_Register";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton48_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingDOTOBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "rptStackwiseRegister";
        //// ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        //Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton49_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Register";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton50_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommoditywiseOpeningClosing";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton51_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/DailyReceiptReleaseGodown.aspx");
    }
    protected void LinkButton52_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownListBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);

        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton53_Click(object sender, EventArgs e)
    {
        //   Response.Redirect("../../Geo_Stock_godown_Branch.aspx");
        Response.Write("<script>window.open( '../../Geo_Stock_godown_Branch.aspx' , '-blank' );</script>");
    }
    protected void LinkButton54_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.open( '../../GodownGeoBranch.aspx' , '-blank' );</script>");
    }
    protected void LinkButton55_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodowntypewisetotalBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton56_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseGodownWiseTotalBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton57_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchWiseProcDtl2016";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton58_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CropYear_Wise_WHR";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton59_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Mapped_Godown_Details";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton60_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_BranchCommodityLoss_Manual";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton61_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_List_For_Updation";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton62_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_SteelSilo_Godown_CPT_and_UTL";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton65_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_JVS_Godown_CPT_and_UTL";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton63_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_PVT_PEG_Godown_CPT_and_UTL";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton64_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_WDRA_Godown_CPT_and_UTL";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton66_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Owned_Godown_CPT_And_UTL";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton67_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchGodownReceiveIssue_BWDates";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton68_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WhrDetails_Between_Two_Date";
        //Response.Redirect("ReportViewer_Depot.aspx");
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot_Dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton69_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CommodityWiseWHR_Report";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton70_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GodownWiseCommoditySummary";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton71_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_PendingPaddyDO_Detail";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GodownStockHandover";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton72_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_OpeningClosing_CommodityWise_BTWDate";
        //Response.Redirect("ReportViewer_Depot.aspx");
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot_Dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton73_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Branch_WheatPSS_Gain_Manual";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton74_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_KharifProc2016_17";
        Response.Redirect("ReportViewer_Depot.aspx");

    }
    protected void LinkButton75_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_MPSCSC_Stock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton76_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_DepositorWise_commodityWise_Stock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton29_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_HiredTypeWise_GodownWise_MPSCSC_stock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton31_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CommodityWise_GwdnWise_MPSCSCstock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchStock_ChartRepresentation";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton77_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchCommodityStock_GraphRepresentation";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton78_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchCpt_Utl_GraphRepresentation";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton79_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWIssueDetailwithGPandDO";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot_Dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton80_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Godown_Wise_Stack_Capacity";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton81_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_TC_ChallanWsie_ReceiveDetail";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CropYearWise_GodownWise_Stock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton82_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_ProcWheat_17_18";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton83_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingDO_TO_From_DS";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton84_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingDO_TO_From_OS";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton85_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_DateWiseStockIssue_GatepassDetail";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot_Dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton86_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Wheatpss_1718_RemainingDF_For_WHR";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton87_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GodownAvailablelStock";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton88_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseClosingBal_GivenDate";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton89_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CmdtyWiseClosingBal_GivenDate";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton90_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_PrmWise_GdwnComparision";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton91_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_KharifProc2017_18";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton92_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CreatedBillDetails";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton93_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkWheat1819_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_ProcWheat_18_19";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkGram_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_ProcGram_18_19";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton93_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Fill_FAQ_CMS_report";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton95_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_BranchNewVerifyGodownList";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton96_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_VerifyGodownVacantCPT";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton97_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Kharif_Procurement_2018_19";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton98_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CoarseGrain_Procurement_2018_19";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton99_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Paddy_Procurement_2018_19";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton100_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Wheat_Procurement_2019_20";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton101_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_DSC_UploaderDetails";
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton102_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_NAFED_WHRPrint_CMS_Procurement_2019_20";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton103_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/CheckGdwnLink_JVSRegID.aspx");
    }
    protected void LinkButton104_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_CmdWise_CropYearWise_WHR";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton105_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "SubRdl_GodownWiseBillStatus";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton106_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_Bill_Payment_Status";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton107_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_GRent_PassingOrder_Detail";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton108_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_Wheat_Procurement_2020_21";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton109_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_CMS_Procurement_2020_21";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton110_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_CMS_Acceptance";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton111_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_Wheat_Acceptance";
        Response.Redirect("ReportViewer_Depot.aspx");
    }



    protected void LinkButton112_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/Rpt_Whr_Details_And_Available_Stock_For_Issue.aspx");
    }

    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_Wheat_Acceptance_2021_22";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    
    protected void LinkButton114_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/Rpt_Before_Aug_Godown_And_Month_Wise_Pending_Amount_For_Branch.aspx");
    }

    protected void LinkButton116_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_Wheat_Acceptance_2022_23";
        Response.Redirect("ReportViewer_Depot.aspx");
    }

    protected void LinkButton117_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_CMS_Acceptance_2022-23";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton118_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/Rpt_FIFO_Status_For_PDS.aspx");
    }

    protected void LinkButton119_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchPages/Rpt_FIFO_FCI_PDS_Stock_Selection.aspx");
    }

    protected void LinkButton120_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Branch/Rpt_Get_Data_For_Selection_FCI_PDS.aspx");
    }


    protected void LinkButton121_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BO_WHR_Wise_Wheat_Acceptance_2022_23_Paddy";
        Response.Redirect("ReportViewer_Depot.aspx");
    }

   
}
