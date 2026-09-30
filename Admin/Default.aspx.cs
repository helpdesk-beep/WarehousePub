using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["Username"].ToString() != null)
            //{
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Font.Size = FontUnit.Point(15);
                lblMessage.Text = "Welcome " + Session["Username"].ToString();
                // lblMessage.Text = Session["pass"].ToString();
            //}
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
            //else
            //{
            //    Response.Redirect("~/Login/Login.aspx");
            //}
            //TextBox1.Text = Request.Form["txtUserName"];
            //TextBox2.Text = Request.Form["txtPassword"];
            //if (Session["userName"] != null && Session["AuthToken"] != null
            //                && Request.Cookies["AuthToken"] != null)
            //{
            //    //Second Check, if Cookie we created has the same value as Second Session we've created
            //    if ((Session["AuthToken"].ToString().Equals(
            //               Request.Cookies["AuthToken"].Value)))
            //    {
            //        lblMessage.ForeColor = System.Drawing.Color.Green;
            //        lblMessage.Font.Size = FontUnit.Point(15);
            //        //lblMessage.Text = "Welcome " + Session["userLoggedin"].ToString();
            //        lblMessage.Text = Session["userLoggedin"].ToString();
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
            //else
            //{
            //    Response.Redirect("~/Login/Login.aspx");
            //}
        }
    }
}