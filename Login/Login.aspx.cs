using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

public partial class Login_Login : System.Web.UI.Page
{
    String isBlock = "";
    string IPAddress;
    int iCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Application_PreSendRequestHeaders();
        lblErr.Visible = false;
        if (!IsPostBack)
        {
            //string salt = CreateSalt(5);
            ////Save the salt in session variable
            //Session["salt"] = salt.ToString();
            ////Add the JS function call to button with a parameter
            //txtPassword.Attributes.Add("onchange", "return HashPwdwithSalt('" + salt.ToString() + "');");
        }
    }
    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-Powered-By");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
    }
    //private string CreateSalt(int size) //Generate the salt via Randon Number Genertor cryptography
    //{
    //    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    //    byte[] buff = new byte[size];
    //    rng.GetBytes(buff);
    //    return Convert.ToBase64String(buff);

    //}
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया यूसर आईडी और पासवर्ड डालें !')", true);
        }
        else
        {
            cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
            if (cptCaptcha.UserValidated)
            {
                string username = txtUserName.Text;
                string password = txtPassword.Text;
                string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                SqlCommand com = new SqlCommand("spAdminLogin", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userName", username);
                // com.Parameters.AddWithValue("@userPassword", password);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session["UserID"] = dt.Rows[0]["Id"].ToString();
                    Session["Username"] = dt.Rows[0]["Username"].ToString();
                    Session["Regionid"] = dt.Rows[0]["Region_ID"].ToString();
                    Session["district"] = dt.Rows[0]["District_ID"].ToString();
                    Session["branchid"] = dt.Rows[0]["Branch_ID"].ToString();
                    Session["pwd"] = dt.Rows[0]["Password"].ToString();
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    Session["Mobile_No"] = dt.Rows[0]["Mobile_No"].ToString();
                    Session["role"] = dt.Rows[0]["Role"].ToString();
                    if (dt.Rows[0]["Password"].ToString() == txtPassword.Text.ToString())
                    {
                        string pwd_db = dt.Rows[0]["hashedPassword"].ToString();
                        string salt = dt.Rows[0]["salt"].ToString();
                        byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                        byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
                        string hashString = Convert.ToBase64String(hashBytes);
                        //if (con.State == ConnectionState.Closed)
                        //{
                        //    con.Open();
                        //}
                        //SqlCommand cmd1;
                        //cmd1 = new SqlCommand();
                        //cmd1.Connection = con;
                        //cmd1.CommandText = "User_Login_Unlock";
                        //cmd1.CommandType = CommandType.StoredProcedure;
                        //cmd1.Parameters.AddWithValue("@UserId", username);
                        //cmd1.Parameters.AddWithValue("@isBlock", 'N');
                        //cmd1.Parameters.AddWithValue("@userPassword", password);
                        //cmd1.ExecuteNonQuery();
                        if (hashString == pwd_db)
                        {
                            lblErr.Visible = false;
                            //Session["Username"] = dt.Rows[0]["Username"].ToString();
                            //Session["pwd"] = dt.Rows[0]["Password"].ToString();
                            if (dt.Rows[0]["Role"].ToString() == "Admin" && dt.Rows[0]["Username"].ToString() == "Admin")
                            {
                                Response.Redirect("/Admin_New/adminDefault.aspx");
                            }
                            else if (dt.Rows[0]["Role"].ToString() == "Admin" && dt.Rows[0]["Username"].ToString() == "FCI Bhopal")
                            {
                                //Response.Redirect("/FCI/FCI_Default.aspx");
                                Response.Redirect("/Warehouse/Inspections/FCIHO/AddEmployee.aspx");
                            }
                            else if (dt.Rows[0]["Role"].ToString() == "Region_Admin")
                            {
                                Response.Redirect("/Region/Default.aspx");
                            }
                            else if (dt.Rows[0]["Role"].ToString() == "Branch_Admin")
                            {
                                Response.Redirect("/Branch/Default.aspx");
                            }
                            else if (dt.Rows[0]["Role"].ToString() == "NCCF")
                            {
                                Response.Redirect("/Login/Nccf_Login.aspx");
                            }
                            else if (dt.Rows[0]["Role"].ToString() == "FCI")
                            {
                                Response.Redirect("/FCINew/Default.aspx");
                            }
                        }
                        else
                        {

                            lblErr.Visible = true;
                            lblErr.Text = "Invalid username or password.";
                            // lblErr.Text = hashString + "  Invalid username or password.";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('पासवर्ड गलत हैं , कृपया सही पासवर्ड प्रविष्ट करें!')", true);
                        //check  login aatempt if fail 
                        //string flg = "F";
                        Check_LoginAttempt(username);
                    }

                }
                else
                {
                    lblErr.Visible = true;
                    lblErr.Text = "Invalid username or password.";
                }
            }

            else
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text = "Enter Currect Captcha Image";
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Currect Captcha Image')", true);
                //return;
            }
        }
    }
    //protected void btnLogin_Click(object sender, EventArgs e)
    //{
    //    if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
    //    {
    //        lblErr.Visible = true;
    //        lblErr.Text = "कृपया यूसर आईडी और पासवर्ड डालें";
    //       // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया यूसर आईडी और पासवर्ड डालें !')", true);
    //    }
    //    else
    //    {
    //        cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
    //        if (cptCaptcha.UserValidated)
    //        {
    //            string username = txtUserName.Text;
    //            //string password = txtPassword.Text;              
    //            string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    //            SqlConnection con = new SqlConnection(constr);
    //            con.Open();

    //            SqlCommand com = new SqlCommand("spAdminLogin", con);
    //            com.CommandType = System.Data.CommandType.StoredProcedure;
    //            com.Parameters.AddWithValue("@userName", username);
    //            //com.Parameters.AddWithValue("@Password", txtPassword.Text);
    //            SqlDataAdapter adapter = new SqlDataAdapter(com);
    //            DataTable dt = new DataTable();
    //            adapter.Fill(dt);
    //            if (dt.Rows.Count > 0)
    //            {

    //                object pwd = dt.Rows[0]["Password"].ToString();
    //                Session["pass"] = pwd.ToString();
    //                Response.Write(pwd);
    //                Response.Write(Session["salt"]);
    //                //string hashed_pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd.ToString().ToLower() + Session["salt"].ToString(), "md5");
    //                //Session["hashed_pwd"] = hashed_pwd.ToString();
    //                //if (hashed_pwd.ToLower().Equals(txtPassword.Text))
    //                {
    //                    //    if (dt.Rows[0]["Password"].ToString() == txtPassword.Text.ToString())
    //                    //{

    //                    if (con.State == ConnectionState.Closed)
    //                    {
    //                        con.Open();
    //                    }
    //                    SqlCommand cmd1;
    //                    cmd1 = new SqlCommand();
    //                    cmd1.Connection = con;
    //                    cmd1.CommandText = "User_Login_Unlock";
    //                    cmd1.CommandType = CommandType.StoredProcedure;
    //                    cmd1.Parameters.AddWithValue("@userName", username);
    //                    cmd1.Parameters.AddWithValue("@isBlock", false);
    //                    cmd1.Parameters.AddWithValue("@userPassword", Session["pass"].ToString());
    //                    cmd1.Parameters.AddWithValue("@FailedAttempts", 0);
    //                    cmd1.ExecuteNonQuery();
    //                    lblErr.Visible = false;
    //                    Session["userName"] = dt.Rows[0]["userName"].ToString();
    //                    Session["pwd"] = dt.Rows[0]["Password"].ToString();
    //                    Session["Role"] = dt.Rows[0]["Role"].ToString();
    //                    Session["Token"] = dt.Rows[0]["Token"].ToString();
    //                    if (dt.Rows[0]["Role"].ToString() == "Admin")
    //                    {

    //                        Session["userLoggedin"] = dt.Rows[0]["userName"].ToString();
    //                        string guid = Guid.NewGuid().ToString();
    //                        //Creating second session for the same user and assigning a randmon GUID
    //                        Session["AuthToken"] = guid;

    //                        //Creating cookie and storing the same value of second session in the cookie
    //                        Response.Cookies.Add(new System.Web.HttpCookie("AuthToken", guid));

    //                        //if (Session["Token"].ToString() == Session["AuthToken"].ToString())
    //                        //{
    //                        //    Response.Write("<script>window.parent.location.href = 'Default.aspx';</script>");
    //                        //}
    //                        //else
    //                        //{
    //                        if (con.State == ConnectionState.Closed)
    //                        {
    //                            con.Open();
    //                        }
    //                        SqlCommand cmd2;
    //                        cmd2 = new SqlCommand();
    //                        cmd2.Connection = con;
    //                        cmd2.CommandText = "Update_Token";
    //                        cmd2.CommandType = CommandType.StoredProcedure;
    //                        cmd2.Parameters.AddWithValue("@UserName", username);
    //                        cmd2.Parameters.AddWithValue("@Pwd", Session["pass"].ToString());
    //                        cmd2.Parameters.AddWithValue("@Token", Session["AuthToken"].ToString());
    //                        cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //                        cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //                        cmd2.ExecuteNonQuery();
    //                        string TheResult = cmd2.Parameters["@TheResult"].Value.ToString();
    //                        if (TheResult.StartsWith("SUCCESS"))
    //                        {
    //                            Response.Redirect("/Admin/Default.aspx");
    //                        }
    //                        // }
    //                        //Creating cookie and storing the same value of second session in the cookie

    //                    }
    //                }
    //            }
    //            else
    //            {
    //                lblErr.Visible = true;
    //                lblErr.Text = "Invalid username or password.";
    //                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid username or password.')", true);
    //                // return;
    //            }
    //        }
    //        else
    //        {
    //            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
    //            lblErrorMessage.Text = "Enter Currect Captcha Image";
    //            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Currect Captcha Image')", true);
    //            //return;
    //        }

    //    }
    //    // }
    //}

    private void Check_LoginAttempt(string username)
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //isBlock = "";
            iCount = iCount + 1;
            string LoginCount = iCount.ToString();
            SqlCommand cmd = new SqlCommand("Count_Failed_User_Login", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", username);
            cmd.Parameters.Add("@LoginCount", LoginCount.ToString());
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open) { con.Close(); }
            //if (Convert.ToInt16(LoginCount) == 3)
            //{
            //    lblErr.Visible = true;
            //    lblErr.Text = "You Reached Maximum Attempts. Your account has been locked";
            //    //ShowMessage("You Reached Maximum Attempts. Your account has been locked ", MessageType.Error);
            //    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Reached Maximum Attempts. Your account has been locked')", true);
            //    return;
            //}
            //else
            if (Convert.ToInt16(LoginCount) >= 3)
            {
                lblErr.Visible = true;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd1;
                cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "Count_Failed_User_Login_Block";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserId", username);
                cmd1.Parameters.AddWithValue("@isBlock", true);
                cmd1.ExecuteNonQuery();

                //SqlCommand cmd1 = new SqlCommand("Count_Failed_User_Login0_Block", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@UserId", username);
                //cmd1.Parameters.Add("@isBlock", isBlock.ToString());
                //cmd1.CommandTimeout = 0;
                //cmd1.ExecuteNonQuery();
                //if (con.State == ConnectionState.Open) { con.Close(); }
                lblErr.Text = "You Reached Maximum Attempts. Your account has been Blocked, Please Contact Portal Admin";
                return;
            }
            else
            {
                int lc = Convert.ToInt16(LoginCount);
                lblErr.Visible = true;
                lblErr.Text = "You have  " + (3 - lc) + "  Login Attempts ";

            }
        }
        catch (Exception ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            lblErr.Text = msg.ToString();
            lblErr.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }
}