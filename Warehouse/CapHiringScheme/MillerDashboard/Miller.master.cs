using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JVSMiller_Miller : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MillerRegId"] == null || Session["Id"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {
            litUser.Text = Session["Miller"].ToString();
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("../Login.aspx");
    }
}
