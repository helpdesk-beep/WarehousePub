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

public partial class IssueCenterLevel_Storage_Collector_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);

                //lblRegionReports.Text = Resources.hindi.lblRegionReports;
            }

        }
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "District_Owned_Godown_CPT_And_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_JVS_Godown_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_WDRA_Godown_CPT_And_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_SteelSilo_CPT_and_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpGodownWise_openindClosing_Btwn_date_Dist";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_PVTPEG_CPT_And_UTL";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CommodityWiseStock_Coll_rpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_PaddyKharifProc2016_17";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_CoarseGrainsKharif201617";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_BranchWise_GodonWise_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_CropYearWise_WhrWise_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_GodownWise_MPSCSCStock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_BranchWise_GodownList";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_Godown_Capacity_And_Utillazation";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_CommodityWise_CropYearWise_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_CommodityWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_WheatCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_PaddyCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_RiceCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_MaizeCYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_CoarseGrainsCYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_BranchOpeningClosing";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Collector_DepositorWise_commodityWise_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_TillDate_GunnyCYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton25_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_GodownWiseCmdSummary";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Dist_Godown_Login_Detail";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }
    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_WheatProc_2017_18";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }

    protected void Lnk_btn_paddy1920_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "District_PaddyKharifProc2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);
    }

    protected void Lnk_btn_coarsegrn1920_Click(object sender,EventArgs e)
    {
        Session["reporturl"]= "";
        Session["reporturl"] = "District_CoarseGrainsKharif2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Collector.aspx\",\"_blank\")", true);

    }

    protected void LinkButton100_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pending_Bill_Details.aspx\",\"_blank\")", true);

    }

    protected void LinkButton102_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pandancy_at_Varius_Level_after_recieved_payment.aspx\",\"_blank\")", true);

    }

    protected void LinkButton101_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Pending_Bill_Details_All_Godown.aspx\",\"_blank\")", true);

    }

    protected void LinkButton103_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Pandancy_at_RM_Level_For_File_Generation.aspx\",\"_blank\")", true);

    }

    protected void LinkButton117_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Summary_of_Panding_Payment_For_Godown_For_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton124_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton128_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton129_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_BOT_Payment_Status.aspx\",\"_blank\")", true);
    }

    protected void LinkButton131_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_All.aspx\",\"_blank\")", true);
    }

    protected void LinkButton132_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_BM_not_Deduction_Bill.aspx\",\"_blank\")", true);
    }

    protected void LinkButton134_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_Alll_Commodity_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton138_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Godown_Wise_Payment_Status_For_Owned_Godown.aspx\",\"_blank\")", true);
    }

    protected void LinkButton139_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_District_Wise_Payment_Status_Godown_Type_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton140_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Reports/Region/Rpt_Pending_Bill_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton133_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_District_Godown_Wise_Qty_Available.aspx\",\"_blank\")", true);

    }

    //protected void LinkButton28_Click(object sender, EventArgs e)
    //{
    //    //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Kharif2024_25_For_District.aspx\",\"_blank\")", true);
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"District/Rpt_Procurement_Kharif2024_25_For_District.aspx\",\"_blank\")", true);
    //}
}
