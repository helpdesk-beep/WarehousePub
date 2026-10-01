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
        string username = Convert.ToString(Session["Name"]);
        string role = Convert.ToString(Session["Role"]);
        if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(role))
        {
            Session.Abandon();
            Response.Redirect("/Default.aspx");
            return;
        }
        if (role != "admin")
        {
            Session.Abandon();
            Response.Redirect("/Default.aspx");
            return;
        }
        if (!IsPostBack)
        {
            lblusername.Text = username;
            lblrole.Text = role;
        }
    }
    protected void lb_logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("/Default.aspx");
    }
}
