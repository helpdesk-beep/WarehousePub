using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using WLCBusinessLayer;

public partial class AdminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataTable dt;
        dt = new Common().ValidateLogin(txtUserName.Text, txtPassword.Text);
        string code = "";
        if (dt.Rows.Count > 0)
        {
            code = dt.Rows[0][0].ToString();
        }

        switch (code)
        {
            case "TRUE":
                dt = new Common().AdminLogin(txtUserName.Text, txtPassword.Text);
                if (dt.Rows.Count > 0)
                {
                    Session["UserName"] = dt.Rows[0]["Username"].ToString();
                    Response.Redirect("Admin/Default.aspx");
                }
                break;
            case "FALSE":
                Response.Write("<script>alert('Invalid Username or Password')</script>");
                break;
            case "LOCKED":
                Response.Write("<script>alert('Locked User')</script>");
                break;
        }

        //dt = new Common().AdminLogin(txtUserName.Text, txtPassword.Text);
        //if (dt.Rows.Count > 0)
        //{
        //    Session["UserName"] = dt.Rows[0]["Username"].ToString();
        //    Response.Redirect("Admin/Default.aspx");
        //}

        //else
        //{
        //    Response.Write("<script>alert('Invalid Username or Password')</script>");

        //}
    }
}