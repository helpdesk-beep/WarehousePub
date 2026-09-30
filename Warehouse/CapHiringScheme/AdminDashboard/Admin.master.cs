using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CapHiringScheme_AdminDashboard_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null && Session["UserName"] == null && Session["Scope"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../AdminLogin.aspx");
        }

        if (!IsPostBack)
        {
            litUser.Text = Session["UserName"].ToString();
            string scope = Session["Scope"].ToString();

            if (scope == "H")
            {
                litScope.Text = "State";
            
            }

            else if (scope == "R")
            {
                litScope.Text = "Region";

            }

            else if (scope == "B")
            {
                litScope.Text = "Branch";

            }
        }

    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("../AdminLogin.aspx");
    }
}
