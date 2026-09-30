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

public partial class Reports_Branch_StockBal_ReportForInspection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseCmdWiseStackBal";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_Depot.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseCmdWiseStockBalance";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_Depot.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Branch_GdwnWiseWHRWiseStockBalance";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_Depot.aspx");
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_WheatCropYWise_BranceReport";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_MPSCSC_TillDate.aspx");
    }

       
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_RiceCropYWise_BranchReport";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_MPSCSC_TillDate.aspx");
    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_PaddyCropYWise_BranchReport";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_MPSCSC_TillDate.aspx");
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_MaizeCropYWise_BranchReport";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_MPSCSC_TillDate.aspx");
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "TillDate_CoarseGrainsCropYWise_BranchReport";
        Response.Redirect("~/IssueCenterLevel/Storage/ReportViewer_MPSCSC_TillDate.aspx");
    }

}
