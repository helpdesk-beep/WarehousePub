using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_StateReportsNafed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton135_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_DisttWiseCMS_rpt";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton136_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_ProvDF_FinalDF_WHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Nafed_BranchWise_CMS_WHRDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton128_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CSM_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton129_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Sarso_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton130_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Masur_Procurement_2018_19";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Nafed_BranchWise_DalhanTilhan_WHRDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
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
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Nafed_WHR_Detail_ToPay";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region_dates.aspx\",\"_blank\")", true);

        //Session["reporturl"] = "";
        //Session["reporturl"] = "Nafed_WHR_Detail_ToPay";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Arhar_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_Com_WHRPrint_CMS_Proc_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CMS_eWHR_2020";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_GodownWise_eWHR_SubRpt_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/States/Rpt_Pending_WHR_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/States/Rpt_Old_New_WHR.aspx\",\"_blank\")", true);
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
        Session["reporturl"] = "Nafed_Moong_Urad_GodownWise_eWHR_SubRpt_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "Nafed_Moong_Urad_GodownWise_eWHR_SubRpt_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022_Nafed.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        //Session["reporturl"] = "Nafed_Moong_Urad_GodownWise_eWHR_SubRpt_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"Rpt_Procurement_Chana_Masoor_Sarson_2022.aspx\",\"_blank\")", true);
    }
}
