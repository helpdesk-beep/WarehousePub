using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
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
            //if (Session["userName"] != null && Session["AuthToken"] != null
            //              && Request.Cookies["AuthToken"] != null)
            //{
            //    //Second Check, if Cookie we created has the same value as Second Session we've created
            //    if ((Session["AuthToken"].ToString().Equals(
            //               Request.Cookies["AuthToken"].Value)))
            //    {
            //        lblUserName.ForeColor = System.Drawing.Color.Green;
            //        lblUserName.Font.Size = FontUnit.Point(15);
            //        lblUserName.Text = "Welcome " + Session["userName"].ToString();
            //        // btnLogout.Visible = true;
            //        string cookieValue = "";
            //        cookieValue = Request.Cookies["AuthToken"].Value;
            //        lblAuthCookie.ForeColor = System.Drawing.Color.Green;
            //        lblAuthCookie.Font.Size = FontUnit.Point(15);
            //        lblAuthCookie.Text = "You AuthToken is " + cookieValue;
            //    }
            //    else
            //    {
            //        Response.Redirect("~/Login/Login.aspx");
            //    }
            //}
            else
            {
                Response.Redirect("~/Login/Login.aspx");
            }
            Application_PreSendRequestHeaders();
        }
    }
    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-Powered-By");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
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
