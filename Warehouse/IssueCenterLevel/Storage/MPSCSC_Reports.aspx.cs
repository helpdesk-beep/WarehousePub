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
using System.Globalization;
using System.Threading;
using System.Text;

public partial class IssueCenterLevel_Storage_MPSCSC_Reports : System.Web.UI.Page
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
    protected void lncCropWiseMpscsc_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CropYearWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CommodityWise_CropYearWise_MPSCSC_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkAllC_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_AllCommodityStockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    //protected void LinkButton27_Click(object sender, EventArgs e)
    //{
    //    Session["reporturl"] = "";
    //    Session["reporturl"] = "Rpt_WHR_Detailed_MPWLC";
    //    ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    //}
    protected void LinkButton29_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_WHR_Detailed_MPWLC_Commodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton46_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownsTotalRecDel";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton97_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Gunny_StockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click1(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_Rice_StockReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void Lnk13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_WheatPSS_Stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void lnkTillWheat_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_WheatReportTillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void lnkTillRice_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_RiceReportTillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_CmdHiredTypeWise_TillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "StockReportMPSCSC_TillDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_WheatCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC_TillDate.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_PaddyCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC_TillDate.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_RiceCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC_TillDate.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_MaizeCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC_TillDate.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_CoarseGrainsCropYWiseReport";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC_TillDate.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Wheat_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Paddy_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Rice_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Maize_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_CG_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Salt_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Sugar_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "IssueCenterWise_Gunny_Stock_Report";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GdwnTypeW_Diff_BW_GPIssue_N_EntryDate";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownWiseIssueDetailwithGPandDO";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "MPSCSC_CurrentStock_with_ClosingBalance";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Cropyearwise_cmdtwise_stock";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_MPSCSC.aspx\",\"_blank\")", true);
    }
}
