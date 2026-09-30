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

public partial class SignUp : System.Web.UI.Page
{
    DataTable dt;
    int rvalue;
    
    SqlParameter param;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
           
        }
    }


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

        int rvalue = SavePreRegistration(txtMiller.Text, txtEmail.Text, txtMobile.Text,txtAadhar.Text,txtPan.Text, txtPassword.Text, ClientIP);

        if (rvalue == -1)
        {
            Response.Write("<script>alert('Already Exists this user')</script>");
        }

        else if (rvalue > 0)
        {
            Response.Write("<script>alert('You have successfully create account')</script>");

            //Response.Redirect("Login.aspx");

            DataTable dt = GetUserDetails(txtEmail.Text);

            if (dt.Rows.Count > 0)
            {
                Session["MillerRegId"] = dt.Rows[0]["Registration_ID"].ToString();
                Session["Id"] = dt.Rows[0]["Id"].ToString();
                Session["Miller"] = dt.Rows[0]["Miller"].ToString();

                Response.Redirect("MillerDashboard/Default.aspx");

            }
        }
    }

        

    




    //pre registration or signup
    public int SavePreRegistration(string miller, string email, string mobile, string aadhar, string pan, string pwd, string ip)
    {
        ArrayList list = new ArrayList();

        param = new SqlParameter("@Miller", miller);
        list.Add(param);

        param = new SqlParameter("@Email", email);
        list.Add(param);

        param = new SqlParameter("@Mobile", mobile);
        list.Add(param);

        param = new SqlParameter("@Aadhar", aadhar);
        list.Add(param);

        param = new SqlParameter("@Pan", pan);
        list.Add(param);

        param = new SqlParameter("@Password ", pwd);
        list.Add(param);

        param = new SqlParameter("@IPAddress", ip);
        list.Add(param);


        try
        {
            rvalue = new Sqldatalayer().ExecuteScalar("mycon", "[spPreRegistration_Insert]", list, true);
        }

        catch (Exception ex) { }
        finally { }
        return rvalue;
    }

    //login user
    public DataTable GetUserDetails(string email)
    {
        string qr = "select * from PreRegistration where Email='" + email +  "'";
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