using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_Inspection_BO : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["role"] != null)
        {
            lbl_user.Text = Session["UserName"].ToString();
            lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            if (Session["role"].ToString() == "BOAdmin")
            {
               
            }
            else
            {
                Session.Abandon();
                Response.Redirect("/Warehouse/Inspections/Default.aspx");
            }
           
        }
        else
        {
            Session.Abandon();
            Response.Redirect("/Warehouse/Inspections/Default.aspx");
        }
    }
    protected void lb_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("/Warehouse/Inspections/Default.aspx");
    }
}
