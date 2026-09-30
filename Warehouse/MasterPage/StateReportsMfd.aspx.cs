using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_StateReportsMfd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Markfed.aspx\",\"_blank\")", true);
        
    }
    //protected void LinkButton45_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "rpt_MPWLC_C_U_Mfd";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
  
    //}
    //protected void LinkButton36_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "GodownCapacityDetails_Mfd";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);

    //}
    //protected void LinkButton37_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "commodityWiseCropyrlygodownsttus_MFd";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);

    //}
    //protected void LinkButton4_Click(object sender, EventArgs e)
    //{
        
    //    //Session["reporturl"] = "";
    //    //Session["reporturl"] = Session["reporturl_mpscsc"];
    //    //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
   
    //}
    //protected void LinkButton26_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Rpt_Whr_Detailed_Report_MFD";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    //}
    //protected void lnk_allentryReport_Click(object sender, EventArgs e)
    //{
    //    //Session["reporturl"] = "";
    //    //Session["reporturl"] = "rpt_All_DataEntryDistrictandBrachwise";
    //    ////ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    //    //Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");

    //}
    //protected void LinkButton28_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Rpt_Whr_Detailed_Report_Commodity_MFD";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
 
    //}
    //protected void LinkButton51_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "paddyProcmfd_state1516";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
  
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Kharif_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Markfed.aspx\",\"_blank\")", true);

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CSM_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Markfed_WHRDetail_Proc201819";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CMS_eWHR_2020";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2012_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_GodownWise_eWHR_SubRpt_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Old_New_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Moong_Urad_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022_Nafed.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2022_23";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2022.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2022_Markfed.aspx\",\"_blank\")", true);
    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_Moong_Urad2022_23";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2023_Nafed.aspx\",\"_blank\")", true);
    }
    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2023.aspx\",\"_blank\")", true);
    }

    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2023_24";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton25_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2023.aspx\",\"_blank\")", true);
    }

    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2023_Markfed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton27_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2023_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton28_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2024_Nafed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2024.aspx\",\"_blank\")", true);
    }

    protected void LinkButton31_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2024.aspx\",\"_blank\")", true);
    }

    protected void LinkButton32_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2024_Markfed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton33_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2024_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton30_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Gram_Sarso_Masur_2024_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Soya_Beans_2024_Markfed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton35_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Soya_Beans_2024_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton36_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2025_Nafed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton37_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2025.aspx\",\"_blank\")", true);
    }

    protected void LinkButton38_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Gram_Sarso_Masur_2025_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton39_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2025_26.aspx\",\"_blank\")", true);
    }

    protected void LinkButton40_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2025_Markfed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Moong_Urad_2025_E_WHR.aspx\",\"_blank\")", true);
    }

    protected void LinkButton42_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2026_Nafed.aspx\",\"_blank\")", true);
    }

    protected void LinkButton43_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2026.aspx\",\"_blank\")", true);
    }

    protected void LinkButton44_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_Moong_Urad_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Gram_Sarso_Masur_2026_E_WHR.aspx\",\"_blank\")", true);
    }
}