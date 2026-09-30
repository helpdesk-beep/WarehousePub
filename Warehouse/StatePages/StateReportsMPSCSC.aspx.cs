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

public partial class StatePages_StateReportsMPSCSC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CMS_eWHR_2020";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2012_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Markfed.aspx\",\"_blank\")", true);
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/Rpt_Pendancy_At_Verius_Level_New.aspx\",\"_blank\")", true);
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/Rpt_Dist_Yearwise_StockPosition.aspx\",\"_blank\")", true);
    }
}
