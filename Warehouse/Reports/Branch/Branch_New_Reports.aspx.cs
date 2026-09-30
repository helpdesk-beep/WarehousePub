using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_Branch_New_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnk_issuedfrmtodate_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Issued_Qty_From_To_Date";
        Response.Redirect("ReportViewer_Branch_New_Reports.aspx");
    }


    protected void lnk_issuedqtygdnwise_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Issued_Qty_District_Godownwise";
        Response.Redirect("ReportViewer_Branch_New_Reports.aspx");

    }
    protected void lnk_paytnotrecd_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "PaymentNotReceived";
        Response.Redirect("ReportViewer_BranchReport.aspx");

    }
    
    protected void lnk_whrpendingdays_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_WHR_Pending_Days";
        Response.Redirect("Rpt_WHR_Pending_Days.aspx");

    }
}