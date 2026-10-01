using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_FCI_Master_New : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userId = Convert.ToString(Session["UserID"]);
        string userName = Convert.ToString(Session["UserName"]);
        string username = Convert.ToString(Session["Username"]);
        if (String.IsNullOrWhiteSpace(userId) || String.IsNullOrWhiteSpace(username))
        {
            // Redirect to Login page if session expired or not logged in
            Response.Redirect("~/Login/Login.aspx");
            return;
        }
        else
        {
            if (!IsPostBack)
            {
                lblusername.InnerText = userName;
                if (username == "FCI Bhopal")
                {
                    divwhr.Visible = true;
                    divbill.Visible = false;
                }
                else if (username == "Bhopal Business")
                {
                    divwhr.Visible = true;
                    divbill.Visible = false;
                }
                else if (username == "Indore Account")
                {
                    divbill.Visible = true;
                    divwhr.Visible = false;
                }
                else if (username == "Bhopal Account")
                {
                    divbill.Visible = true;
                    divwhr.Visible = false;
                }
            }
            else
            {
                //Response.Redirect("~/Login/Login.aspx");
            }
            Application_PreSendRequestHeaders();
            // Show username on header
            // l.Text = Session["UserName"].ToString();
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
        // Redirect to modal login page and open modal automatically
        Response.Redirect("~/Login/Login.aspx");
    }
}
