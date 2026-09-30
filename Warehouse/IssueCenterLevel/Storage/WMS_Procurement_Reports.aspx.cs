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

public partial class StatePages_WMS_Procurement_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton199_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
}
