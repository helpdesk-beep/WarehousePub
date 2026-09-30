using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class MasterPages_Nccf_Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            // Redirect to Login page if session expired or not logged in
            Response.Redirect("~/Login/Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                string userId = Convert.ToString(Session["UserID"]);
                string userName = Convert.ToString(Session["UserName"]);
                lblusername.InnerText = userName;
                if (Session["Username"].ToString() == "Indore Business")
                {
                    divwhr.Visible = true;
                    divbill.Visible = false;
                }
                else if (Session["Username"].ToString() == "Bhopal Business")
                {
                    divwhr.Visible = true;
                    divbill.Visible = false;
                }
                else if (Session["Username"].ToString() == "Indore Account")
                {
                    divbill.Visible = true;
                    divwhr.Visible = false;
                }
                else if (Session["Username"].ToString() == "Bhopal Account")
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
        Response.Redirect("~/Login/Nccf_Login.aspx");
    }
}
