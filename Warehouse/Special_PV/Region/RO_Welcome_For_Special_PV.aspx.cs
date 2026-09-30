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

public partial class Special_PV_Region_RO_Welcome_For_Special_PV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (Session["role"] != null)
        {
            lbl_user.Text = Session["UserName"].ToString();
        }
        else
        {
            Session.Abandon();
            Response.Redirect("../../Special_PV_Default.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void btnNewReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/Add_New_Insp_Officer.aspx");
    }
    protected void btnPaymentReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/ScheduleInspection.aspx");
    }
    protected void btnmpwlcaddemp_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/Add_New_Insp_Officer_For_MPWLC.aspx");
    }
    protected void btnmpwlcsc_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/ScheduleInspection_For_MPWLC.aspx");
    }
}