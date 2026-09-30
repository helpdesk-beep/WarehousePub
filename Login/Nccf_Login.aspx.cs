using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;


public partial class Login_Nccf_Login : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    private object con;
    private object ob_value;
    DataTable dt = new DataTable();
    String isBlock = "";
    string IPAddress;
    int iCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();


        if (!IsPostBack)
        {

            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                if (Session["Username"].ToString() == "INDORE NCCF")
                {
                    divIndore.Visible = true;
                    divBhopal.Visible = false;
                }
                else if (Session["Username"].ToString() == "Indore Business")
                {
                    divIndore.Visible = true;
                    divBhopal.Visible = false;
                }
                else if (Session["Username"].ToString() == "Indore Account")
                {
                    divIndore.Visible = true;
                    divBhopal.Visible = false;
                }
                else if (Session["Username"].ToString() == "BHOPAL NCCF")
                {
                    divIndore.Visible = false;
                    divBhopal.Visible = true;
                }
                else if (Session["Username"].ToString() == "Bhopal Business")
                {
                    divIndore.Visible = false;
                    divBhopal.Visible = true;
                }
                else if (Session["Username"].ToString() == "Bhopal Account")
                {
                    divIndore.Visible = false;
                    divBhopal.Visible = true;
                }
            }
            else
            {
                Response.Redirect("/Login/Login.aspx");
            }
        }
    }

    protected void btnModalLogin_Click(object sender, EventArgs e)
    {
        if (txtModalUser.Text.Trim() == "" || txtModalPass.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया यूसर आईडी और पासवर्ड डालें !')", true);
        }
        else
        {
            string username = txtModalUser.Text;
            string password = txtModalPass.Text;
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand com = new SqlCommand("sp_Nccf_Login", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@userName", username);
            com.Parameters.AddWithValue("@Password", password);
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
                if (dt.Rows[0]["Password"].ToString() == txtModalPass.Text.ToString())
                {
                    string pwd_db = dt.Rows[0]["hashedPassword"].ToString();
                    string salt = dt.Rows[0]["salt"].ToString();
                    byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
                    byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
                    string hashString = Convert.ToBase64String(hashBytes);
                    if (hashString == pwd_db)
                    {
                        lblModalError.Visible = false;
                        if (dt.Rows[0]["Name"].ToString() == "Indore Business")
                        {
                            Response.Redirect("/NCCF/Welcome_Page.aspx");
                        }
                        else if (dt.Rows[0]["Name"].ToString() == "Indore Account")
                        {
                            Response.Redirect("/NCCF/Welcome_Page.aspx");
                        }
                        else if (dt.Rows[0]["Name"].ToString() == "Bhopal Business")
                        {
                            Response.Redirect("/NCCF/Welcome_Page.aspx");
                        }
                        else if (dt.Rows[0]["Name"].ToString() == "Bhopal Account")
                        {
                            Response.Redirect("/NCCF/Welcome_Page.aspx");
                        }
                    }
                    else
                    {
                        lblModalError.Visible = true;
                        lblModalError.Text = "Invalid username or password.";
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
                lblModalError.Visible = true;
                lblModalError.Text = "Invalid username or password.";
            }
        }
    }
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
                lblModalError.Visible = true;
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
                lblModalError.Text = "You Reached Maximum Attempts. Your account has been Blocked, Please Contact Portal Admin";
                return;
            }
            else
            {
                int lc = Convert.ToInt16(LoginCount);
                lblModalError.Visible = true;
                lblModalError.Text = "You have  " + (3 - lc) + "  Login Attempts ";
            }
        }
        catch (Exception ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            lblModalError.Text = msg.ToString();
            lblModalError.Visible = true;
        }
        finally
        {
            con.Close();
        }
    }

    //protected void btnlogout_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("/Login/Login.aspx");
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Login/Login.aspx");
    }
}
