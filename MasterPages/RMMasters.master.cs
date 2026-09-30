using System;
using System.Web.UI.WebControls;

public partial class MasterPage_RMMasters : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Username"].ToString() != null)
            {
                lblUserName.ForeColor = System.Drawing.Color.Green;
                lblUserName.Font.Size = FontUnit.Point(15);
                lblUserName.Text = "Welcome " + Session["Username"].ToString();
            }
            else
            {
                Response.Redirect("~/Login/Login.aspx");
            }
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
