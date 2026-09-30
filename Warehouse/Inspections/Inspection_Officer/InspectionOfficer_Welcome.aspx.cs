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

public partial class Inspections_Inspection_Officer_InspectionOfficer_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
       
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Inspections/Inspection_Officer/InspectionLogin.aspx");
    }
    protected void btnAddOfficer_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_ScheduledInspDetail.aspx");
    }
    protected void btnAllotInsp_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Inspections/Inspection_Officer/InspOfficer_ViewFilled_Annecure_B.aspx");
    }
}