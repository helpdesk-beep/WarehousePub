using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_New_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnk_issuedqtygdnwise_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Issued_Qty_District_Godownwise";
        Response.Redirect("ReportViewer_New_Reports.aspx");
    }
}