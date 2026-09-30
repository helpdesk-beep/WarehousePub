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

public partial class IssueCenterLevel_Storage_Reports_District : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
        //if (Session["lang"] != null)
        //{
        //    if (Session["lang"].ToString() == "Hindi")
        //    {

        //        ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
        //        lblDistrictReports.Text = rm.GetString("lblDistrictReports");
        //    }
        //}   
    
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_CommodityDetails_regionwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_District.aspx\",\"_blank\")", true);


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U";
        //Response.Redirect("ReportViewer_District.aspx");
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_District.aspx\",\"_blank\")", true);


    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_HQ02";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_CSPS_now";
        //Response.Redirect("ReportViewer_District.aspx");
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_District.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_CSPS_now_Scientific";
        //Response.Redirect("ReportViewer_District.aspx");
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_District.aspx\",\"_blank\")", true);
    }
}
