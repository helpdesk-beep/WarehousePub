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

public partial class Reports_Branch_Branch_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton52_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownListBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);

        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton80_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_Godown_Wise_Stack_Capacity";
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton34_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodownList";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton55_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "GodowntypewisetotalBranch";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton62_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchNew_Godown_Cap_Utl";
        Response.Redirect("Branch_ReportsViewer.aspx");
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
    protected void LinkButton49_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Register";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockRegister_GodownNCommodity";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStackwiseRegister_ForAllStack";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("ReportViewer_Depot.aspx");
    }
    protected void LinkButton39_Click(object sender, EventArgs e)
    {

    }
}
