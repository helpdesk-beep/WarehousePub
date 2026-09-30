using System;
using System.Web.UI.WebControls;

public partial class MasterPage_adminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = Convert.ToString(Session["Username"]);
        if (String.IsNullOrWhiteSpace(username))
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            lblUserName.ForeColor = System.Drawing.Color.Green;
            lblUserName.Font.Size = FontUnit.Point(15);
            lblUserName.Text = "Welcome " + username;
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        if (Response.Cookies["ASP.NET_SessionId"] != null)
        {
            Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Response.Cookies["AuthToken"] != null)
        {
            Response.Cookies["AuthToken"].Value = string.Empty;
            Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.Redirect("../Login/Login.aspx");
    }
}
