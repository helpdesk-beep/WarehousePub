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

using System.Resources;
using System.Reflection;
//using ChattishgrahDemo;
using System.Globalization;
using System.Threading;

using System.Text;

public partial class IssueCenterLevel_Storage_Report_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);

                // lblRegionReports.Text = rm.GetString("lblRegionReports");
                lblRegionReports.Text = Resources.hindi.lblRegionReports;
            }

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_CommodityDetails";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_CommodityDetails_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
            //Response.Redirect("~/Reports/States/ReportViewer_Region.aspx");
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_C_U";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_C_U_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_HQ01";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_HQ01_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "RegisteredOperator";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "OperatorLoginReport_issue";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "RegisteredOperatorDM";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_GodownwiseStackPosition";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_CommodityDetails_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_HQ01_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise_between2dates";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);

    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_CSPS_now";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"http://10.131.0.20/wlcgr/StockPosition_Depot.aspx\",\"_blank\")", true);

    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "KharifProc2016-17_GodownWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Stateregion_drilldown";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lnkdelWHRCompare_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Compare_Old_New_WHR";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lnk_allentryReport_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_All_DataEntryDistrictandBrachwise";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lblDeleteAllWHRDOReport_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_state_delete_Summary";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }

    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Pending_gatepassdetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptGodownwiseStackPosition_district";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Pending_gatepassdetails_Opraterwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Whr_Detailed_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
 
    }
    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_WHR_Detailed_MPWLC";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
 
    }
    protected void LinkButton28_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Whr_Detailed_Report_Commodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
 
    }
    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_WHR_Detailed_MPWLC_Commodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
 
    }
    protected void LinkButton30_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WHR_With_CropYear";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton31_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "godownWiseCurrentStock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton32_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"AllpendingReciving.aspx\",\"_blank\")", true);
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingReciving";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton33_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseStateReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownList_Region";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton35_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Crop_Commodity_wise_WHR_State";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton36_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownCapacityDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton37_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "commodityWiseCropyrlygodownsttus";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton38_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingStatus";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton39_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "ProcurmentReport15";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton40_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "StoragetypeWiseCapacity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WHRwithmenualrecord";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton42_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodowntypewisetotalSTATE";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton43_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godownwisewhrstaterpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton44_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "Godowntypewiseprocdtl2015";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    //to be update
    protected void LinkButton45_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Utilization_Status";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton46_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownsTotalRecDel";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton47_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DeleteResetDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton48_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownCapacityDetailsState";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton49_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godowntypewiseprocdtl2015own";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton50_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Master_Bill_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton51_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodowntypewisePaddyprocdtl15own";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton52_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WHRwithmenualrecordPaddy15";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton53_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TotalRecDelCropyrCommGodw";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton54_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DaywiseRecvDelState";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton55_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CapacityNUtilNew";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton56_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DailytransactionState";
       // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Write("<script>window.open( '../../Reports/States/Rpt_BranchWise_DailyTransaction.aspx' , '-blank' );</script>");
    }
    protected void LinkButton57_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CapacityNUtilNew_Bw_Dates";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Write("<script>window.open( '../../Reports/States/Rpt_Utilization_Bw_dates.aspx' , '-blank' );</script>");
    }
    //protected void LinkButton58_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "depositorwisecurrentbwdates";
    //    // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    //    Response.Write("<script>window.open( '../../Reports/States/RPT_DepositorWiseSummry.aspx' , '-blank' );</script>");

    //}
    protected void LinkButton59_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWise_totalCommodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton60_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Bhugtanrashidatewise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Region/RegionBandaranSulkDate.aspx\",\"_blank\")", true);


    }
    protected void LinkButton61_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BhugtanRashiRegion_Progresive";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Region/RegionBandaranSulkDate.aspx\",\"_blank\")", true);


    }
    protected void LinkButton62_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BhugtanrashiMonday";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Region/RegionBandaranSulkDate.aspx\",\"_blank\")", true);


    }
    protected void LinkButton63_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BhugtanRashiTtlRegionWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton64_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godowntypewiseprocdtl2016own";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton65_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TotalWheatWHR2016-17";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton66_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseGodownWiseTotalState";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton67_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "GodownutilGraph";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton68_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "DepositorWiseGraph";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    //protected void LinkButton69_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "GodownStackedChart";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    //}
    protected void LinkButton68_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_StateGodownList";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton70_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "PvtWTotalWheatWHR2016-17";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkAllC_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_AllCommodityStockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton76_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_OnionStorageDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton77_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityLossDuringStorage_Manual";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    
    protected void LinkButton79_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "SteelSilo_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton80_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WDRA_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton81_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PVT_PEG_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton82_Click(object sender, EventArgs e)
    {
          Session["reporturl"] = "";
          Session["reporturl"] = "JVS_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton83_Click(object sender, EventArgs e)
    {
         Session["reporturl"] = "";
         Session["reporturl"] = "Owned_Godown_Capacity_And_Utillazation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton78_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Effective_rate_list";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton84_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityLossStateSummary_Manual";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton85_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_oldStock_before2015_16_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton86_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWiseReceiveIssue_BetweenDates";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton87_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CategoryWise_Current_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton88_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Branch_LastOperation_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton89_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_IssueCenterWiseBranch_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton90_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_None_MPWLCBranch_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton91_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Diff_BW_WHRIssue_And_EntryDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton92_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_RegionWise_GodownTypeWise_Proc_wheat_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton93_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Diff_BW_GatePassIssue_And_EntryDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton94_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_SummaryOfLatiLongti";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton95_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Quality_Control_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton96_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownList_of_unutiliza_tilldate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton97_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Gunny_StockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton98_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchWiseGodownCount";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void SCSR_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "StateOpeningClosing_Rpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void lnkOCBD_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_BranchWise_openindClosing_Btwn_date";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton99_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "KharifProc2016-17";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton100_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GodownWiseCurrentStock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void lnkKharif2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_KharifProc2016_17";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void lnkCGProc_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CourseGrain_KharifProc2016_17";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void lnkAllK_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "KharifProc2016_17_AllComm";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void lncCropWiseMpscsc_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CropYearWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CommodityWise_CropYearWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatPSS_Gain_StateSummary_Manual";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_Rice_StockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void Lnk13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click1(object sender, EventArgs e)
    {

    }
    protected void lnkTillWheat_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_WheatReportTillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void lnkTillRice_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_RiceReportTillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkBtn2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CommodityStock_GraphRepresentation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click2(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pvt_Godown_List";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click2(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatProc201718_GodownWise_HiredTypeWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton27_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatProc1718_districtwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton29_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_wheatpss_1718_RemainingDF_For_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton46_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GatepassDateWiseStockIssueDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton58_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Arhar_Tuar_StorageDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton69_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Urad_StorageDetail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton74_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Arhar_Procurement_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton75_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Onion_Procurement_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton97_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_StateGodownListwithOrg";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton101_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pulses_Procurement_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton102_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NewGodowns_Created_BW_Dates";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton103_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PremisesWise_GodownList";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton104_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GdwnWiseLossGainDtl";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton105_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PendingPremisesWise_GodownList";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton106_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PremisesWise_GodownComparision";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton107_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_StorageCargesBillDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton108_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Wheat2017_GdwnDepositePreority";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton109_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BS_CommodityWiseStockDepositSummary";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton110_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GodownOwner_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton111_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_StorageCargesBillDetails_BMAprove";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton112_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "KharifProc2017_18_CommodityWiseF";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton113_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "KharifPaddyProc2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton114_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BS_WHRWiseStockDepositDtl";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton115_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_MonthWiseCreatedBillDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton116_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WHRCpt_againt_AggrementCpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton117_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_BranchAddtionalDeatils";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton118_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_AgrementJVGodownDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton119_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PaddyProc1718_Without_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton120_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PaddyProc1718_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton121_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_WHRCpt_againt_AggrementCpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton122_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DistrictWiseVacantCpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton123_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "StateWise_ProcurementCenter_GodownMappingWheat2018_2019";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton124_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton125_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WHRCPT_Againt_JVSAgreementCPTNew";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton126_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_1819_RemainingDF_For_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton127_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_DailyStockDepositeEntry2018";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton128_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CSM_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton129_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Sarso_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton130_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Masur_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton131_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatProc1819_Without_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton132_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatProc1819_CWCFCIMFDG";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton133_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_1819_DiffHour";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton134_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_1819_DiffHourBTWN_Date";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton135_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DisttWiseCMS_rpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton136_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_ProvDF_FinalDF_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton137_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_1819_Diff100Hour";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton138_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_1819_BTWN_DateandTime";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }
    protected void LinkButton139_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_RegionWiseDisttWiseCMS_rpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    //protected void LinkButton140_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "State_NafedCMS_StockReport";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    //}
    protected void LinkButton140_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_NafedCMS_StockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton141_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_RegionWise_HiredTypeWise_StockRpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton142_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "WheatProc1819_Without_CWCFCIMFDG_DistWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton143_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Proc1819_Without_CWCFCIMFDG_DistWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton144_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Proc1819_CWCFCIMFDG_DistWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton145_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Wheat2018_GdwnDepositePreority";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton146_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Godown_Village_Mapping";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton147_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Proc1819_Comparative_CMS_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton148_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DeletedWHR_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton149_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_QCDeletedReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton150_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_NonFAQ_CMS_SocietyWisWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton151_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WHRDetails_Proc1819";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton152_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DeletedWHR_WithReqDate_DeletedDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton153_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GodownWise_DFandWHRDetails_Proc1819";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton154_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Commodity_Crop_Wise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton155_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_State_New_Godown_Summary";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton156_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_KrayaParisar_Godown_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton157_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_GreaterCpt_Godown_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton158_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "HiredTypeWiseCpt_VerifyGdwnRpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton159_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Kharif_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton160_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_VerifyGDWN_CPT_LicDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton161_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CoarseGrain_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton162_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton163_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_PaddyStorageInCapCapacity_2018";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton164_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Proc2018_19_RejectionWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton165_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_VerifyGdwnCPT_VacantCPT";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton166_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DistrictWiseGdwnVacantCPT_Closing";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton167_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_NoOfGdwn_GdwnCPT_VacantCPT";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton168_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton169_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DSC_UploaderDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton199_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton171_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_HiredTypeWise_CPT_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton172_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Branch_Resource_Availability";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton173_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Owner_Account_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton174_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_StorageChrg_GnrtBillSummary2019_MPWLCGdwn";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton175_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_StorageChrg_GnrtBillSummary2019_Other";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton176_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_GRent_PassingOrder_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton177_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Bill_Payment_Status";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton178_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "StorageBill_ETE_Billing_Status";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton263_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise_between2dates_HiredType";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }

    protected void LinkButton264_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise_between2dates_StorageType";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);
    }

    protected void LinkButton179_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/State_Storage_Bill_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton180_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_Region_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton181_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_After_August_Region_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton182_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_All_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton183_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_August_to_Till_All_District.aspx\",\"_blank\")", true);
    }

    protected void LinkButton184_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_Region_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton185_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_District_Branch_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton186_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_Region_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton187_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_Before_August_District_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton188_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_After_August_District_Branch_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton189_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_After_August_Region_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton190_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_After_August_District_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton192_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Payment_BillsFromCSMStoMPWLC_Status_Rept";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }

    protected void LinkButton191_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_Region_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton193_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Pendancy_Report_at_Various_Levels_Before_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton194_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Pendancy_Report_at_Various_Levels.aspx\",\"_blank\")", true);

    }

    protected void LinkButton195_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton196_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Pending_Bill_Detail_August_to_Till_All_District.aspx\",\"_blank\")", true);

    }

    protected void LinkButton197_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Pendancy_Report_at_Various_Levels_From_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton198_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Pendancy_Report_at_Various_Levels_From_Aug.aspx\",\"_blank\")", true);
    }

    protected void LinkButton200_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_DBG_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton201_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Date_Wise_Payment_Received_From_MPSCSC_Details_From_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton202_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton203_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Bill_Detail_After_Region_Difference_Wise_With_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton204_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton205_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Pending_Bill_Details_Motn_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton206_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx\",\"_blank\")", true);
    }

    protected void LinkButton208_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton207_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Bill_Detail_After_Region_Difference_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton209_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Search_HO_StorageBillVerification_N.aspx\",\"_blank\")", true);

    }

    protected void LinkButton210_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_StorageBill_SearchStatus.aspx\",\"_blank\")", true);
    }

    protected void LinkButton211_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_And_Month_Wise_Pending_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton212_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Payment_Credit_From_MPWLC_To_Godown.aspx\",\"_blank\")", true);

    }

    protected void LinkButton213_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton214_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_UnexpiredAmount_From_Aug.aspx\",\"_blank\")", true);
    }

    protected void LinkButton215_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_UnexpiredAmount_From_Jan.aspx\",\"_blank\")", true);
    }

    protected void LinkButton216_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug.aspx\",\"_blank\")", true);
    }

    protected void LinkButton217_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Pending_for_DSC_and_Submission.aspx\",\"_blank\")", true);
    }

    protected void LinkButton218_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Loss_Gain_Date_Wise_Sync.aspx\",\"_blank\")", true);
    }

    protected void LinkButton219_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Loss_Gain_District_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton220_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Loss_Gain_Branch_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton221_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Loss_Gain.aspx\",\"_blank\")", true);
    }

    protected void LinkButton222_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Loss_Gain_Branch_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton223_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Loss_Gain.aspx\",\"_blank\")", true);
    }

    protected void LinkButton224_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Hired_Type_WHR_Acceptance_Qty.aspx\",\"_blank\")", true);
    }

    protected void LinkButton225_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Bill_Detail_After_District_Difference_Wise_With_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton226_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Pendancy_At_Verius_Level.aspx\",\"_blank\")", true);
    }

    protected void LinkButton227_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Loss_Gain_Region_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton228_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/Rpt_Pendancy_At_Verius_Level_New.aspx\",\"_blank\")", true);
    }

    protected void LinkButton229_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Summary_Report_for_MD.aspx\",\"_blank\")", true);
    }

    protected void LinkButton230_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_District_and_Date_Wise_Pendancy.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1411_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/rpt_district_wise_qty_available.aspx\",\"_blank\")", true);
    }

    protected void LinkButton231_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton232_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Pendancy_Various_level.aspx\",\"_blank\")", true);

    }

    protected void LinkButton233_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_UTR_Wise_Payment.aspx\",\"_blank\")", true);

    }

    protected void LinkButton234_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_JVS_Payment_Against_Received_Payment_From_MPSCSC.aspx\",\"_blank\")", true);

    }

    protected void LinkButton235_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_After_August_Account_Statement.aspx\",\"_blank\")", true);
    }

    protected void LinkButton236_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_UTR_Wise_NEFT_Generation_Pendency_Details.aspx\",\"_blank\")", true);

    }

    protected void LinkButton237_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Godown_And_Month_Wise_Pending_Amount.aspx\",\"_blank\")", true);

    }
    protected void LinkButton238_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Pending_Bill_for_Submission.aspx\",\"_blank\")", true);

    }
    protected void LinkButton239_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Stock_Position_By_Date.aspx\",\"_blank\")", true);

    }

    protected void LinkButton240_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Warehouse/Reports/States/Rpt_DistrictandRegion_Wise_Qty_Available.aspx\",\"_blank\")", true);
    }

    protected void LinkButton241_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Warehouse/Reports/States/Rpt_District_Wise_Qty_Available2.aspx\",\"_blank\")", true);
                                                                                            


    }

    protected void LinkButton242_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/GodownDifrenceDistance.aspx\",\"_blank\")", true);
    }

    protected void LinkButton243_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_shredi_selecttion.aspx\",\"_blank\")", true);
    }

    protected void LinkButton244_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Capacity_wise_shredi_selecttion.aspx\",\"_blank\")", true);
    }

    protected void LinkButton245_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Godown_Capacity_wise_shredi_selecttion.aspx\",\"_blank\")", true);

    }

    protected void LinkButton246_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Insurance_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton247_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Insurance_Details_ChoiseFilling.aspx\",\"_blank\")", true);

    }

    protected void LinkButton245_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Generation_Pending_Recieved_Bill_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton248_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_No_of_Month_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }
    protected void LinkButton249_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_District_Wise_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton250_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_District_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx\",\"_blank\")", true);
    }

    protected void LinkButton251_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Pandancy_at_RM_Level_For_File_Generation.aspx\",\"_blank\")", true);
    }
    protected void LinkButton252_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Date_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }
    protected void LinkButton253_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Aug_to_Dec_And_Date_Wise_Payment_Details.aspx\",\"_blank\")", true);
    }
    protected void LinkButton254_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_District_Wise_Qty_Available_All_Type.aspx\",\"_blank\")", true);
    }
    protected void LinkButton255_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Get_Stack_Count_For_FCI.aspx\",\"_blank\")", true);
    }
    protected void LinkButton256_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Pending_Warehouse_For_ElectroninWeighbrige_INformation.aspx\",\"_blank\")", true);
    }
    protected void LinkButton257_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Stock_Reconciliation_August_2022_State.aspx\",\"_blank\")", true);
    }
    protected void LinkButton258_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Data_For_Selection_FCI_PDS.aspx\",\"_blank\")", true);
    }

    protected void LinkButton259_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Get_Godown_Capcaity_Hried_Type_State.aspx\",\"_blank\")", true);
    }

    protected void LinkButton260_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_mapping_for_rackpoint_godown_wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton261_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Get_Data_Not_Selection_For_FCI_PDS.aspx\",\"_blank\")", true);
    }

    protected void LinkButton262_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Stock_Reconciliation_August_2022_Region_With_Remark.aspx\",\"_blank\")", true);
    }

    protected void LinkButton265_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Private_Warehouse_Related_Information.aspx\",\"_blank\")", true);
    }

    protected void LinkButton266_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Pending_Mpwlc_To_Mpscsc.aspx\",\"_blank\")", true);

    }
    

    protected void LinkButton267_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Refrance_Number_Wise_Payment_Credit_From_MPWLC_To_Godown.aspx\",\"_blank\")", true);


    }

    protected void LinkButton268_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Get_Region_Wise_Date_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton269_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Date_Region_Wise_Payment_Reports.aspx\",\"_blank\")", true);
    }

    protected void LinkButton270_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Account_Reconcile_Reports_For_Audit_01_Receive_Payment.aspx\",\"_blank\")", true);
    }

    protected void LinkButton271_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Account_Reconcile_Reports_For_Audit_02_Receive_PaymentFrom_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton272_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Tribal_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton273_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_District_Wise_Summary_Tribal_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton274_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Commodity_District_Wise_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton275_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Branch_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton276_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Godown_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton277_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_Update_Loss_Gain_by_RM.aspx\",\"_blank\")", true);
    }

    protected void LinkButton278_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton279_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_District_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton280_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_District_Wise_Payment_Status_All.aspx\",\"_blank\")", true);
    }

    protected void LinkButton281_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_JVS_Payment_Received_From_MPSCSC_Date_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton282_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Finacial_Month_Date_Wise_Payment_Status_From_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton283_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_BOT_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton284_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Nafed_Pendding_Ammount_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton285_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/HiredTypeWisePaymentStatus.aspx\",\"_blank\")", true);
    }

    protected void LinkButton286_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_District_Wise_Payment_Status_All.aspx\",\"_blank\")", true);
    }

    protected void LinkButton287_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Region_Nafed_Pendding_Ammount_Report.aspx\",\"_blank\")", true);
    }

    protected void LinkButton288_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Godown_Type_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton289_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_District_Wise_HiredTypeWise_Capacity.aspx\",\"_blank\")", true);
    }

    protected void LinkButton290_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../StatePages/Rpt_District_Wise_TotalCapacity.aspx\",\"_blank\")", true);
    }

    protected void LinkButton291_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Payment_Status_at_MPSCSC.aspx\",\"_blank\")", true);
    }

    protected void LinkButton292_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Godown_Type_Wise_Payment_Status_For_All.aspx\",\"_blank\")", true);
    }

    protected void LinkButton293_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../Reports/States/Rpt_Region_Wise_Payment_Status_For_State.aspx\",\"_blank\")", true);
    }
}
