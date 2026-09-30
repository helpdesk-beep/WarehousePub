using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_FCIHO_RO_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        //lbl_user.Text = Session["UserName"].ToString();
        //lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("InspectionLogin.aspx");
    //}
    //protected void btnNewReg_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("/Warehouse/Inspections/RO/Add_New_Insp_Officer.aspx");
    //}
    //protected void btnPaymentReg_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("/Warehouse/Inspections/RO/ScheduleInspection.aspx");
    //}
    //protected void btnupdatereg_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewPVSummary.aspx");
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewFillAnexB.aspx");
    //}
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewAnexBFillStock_AvlStock_.aspx");
    //}
}