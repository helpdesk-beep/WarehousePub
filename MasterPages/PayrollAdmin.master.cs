using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_PayrollAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            lblusername.Text = Session["Name"].ToString();
            lblrole.Text = Session["Role"].ToString();
            if (lblrole.Text != "admin")
            {
                Session.Abandon();
                Response.Redirect("/Default.aspx");
            }

        }
    }
    protected void lb_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("/Default.aspx");
    }
}
