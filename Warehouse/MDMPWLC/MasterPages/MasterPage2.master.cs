using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MDMPWLC_MasterPage2 : System.Web.UI.MasterPage
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void LinkButton_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "Wheat_Procurement_2021_22";

        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "CMS_Procurement_2021_22";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
}
