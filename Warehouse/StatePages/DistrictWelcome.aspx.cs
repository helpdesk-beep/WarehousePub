using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_DistrictWelcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_dist_whr_detail_cropyr";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);

    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "paddyProcmfd1516";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/Region/RegionReportVeiwer.aspx\",\"_blank\")", true);


    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Reports/Depot/Rpt_Pending_WHR_Details.aspx\",\"_blank\")", true);
    }
}