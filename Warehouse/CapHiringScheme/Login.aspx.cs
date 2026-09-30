using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Collections;


public partial class Login : System.Web.UI.Page
{
    DataTable dt;
    int rvalue;
    ArrayList list = new ArrayList();
    SqlParameter param;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCapctha();
        }
    }

    public void FillCapctha()
    {
        try
        {
            txtCapcha.Attributes.Add("autocomplete", "off");

            txtCapcha.Text = "";
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Session["captcha"] = captcha.ToString();
            imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
        }
        catch
        {

            throw;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["captcha"].ToString() != txtCapcha.Text)
            {
                FillCapctha();
            }
            else if (Session["captcha"].ToString() == txtCapcha.Text)
            {
                DataTable dt = UserLogin(txtEmail.Text, txtPassword.Text);

                if (dt.Rows.Count > 0)
                {
                    Session["MillerRegId"] = dt.Rows[0]["Registration_ID"].ToString();
                    Session["Id"] = dt.Rows[0]["Id"].ToString();
                    Session["Miller"] = dt.Rows[0]["Miller"].ToString();

                    Response.Redirect("MillerDashboard/Default.aspx");

                }

                else
                {
                    Response.Write("<script>alert('Invalid User Id or Password. Please Correct Details')</script>");

                }
            }
        }
        catch (Exception ex)
        {

        }
    }



    //login user
    public DataTable UserLogin(string email, string pwd)
    {
        string qr = "select * from PreRegistration where Email='" + email + "' and Password='" + pwd + "'";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }
        catch (Exception ex)
        {
        }
        finally { }

        return dt;
    }
}