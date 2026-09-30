using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login_Login_NCCF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // पहली बार पेज लोड पर खाली करें
            txtModalUser.Text = "";
            txtModalPass.Text = "";
            hfLoginType.Value = "";
            lblModalError.Text = "";
        }
    }
    protected void btnModalLogin_Click(object sender, EventArgs e)
    {
        string username = txtModalUser.Text.Trim();
        string password = txtModalPass.Text.Trim();
        string loginType = hfLoginType.Value; // "Business" or "Account"

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblModalError.Text = "Please enter user id and password.";
            // keep modal open on client
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "document.getElementById('modalBackdrop').style.display='flex';", true);
            return;
        }

        // OPTIONAL: If you store hashed passwords, verify hash instead.
        // Example below uses plain text (not recommended for production).

        string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            // If your Users table has Role column, match it. If not, remove role condition.
            string query = @"
                    SELECT COUNT(*) FROM Users
                    WHERE Username = @Username AND Password = @Password
                    AND (@Role IS NULL OR Role = @Role)
                ";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                // pass role or null
                if (string.IsNullOrEmpty(loginType))
                    cmd.Parameters.AddWithValue("@Role", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Role", loginType);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    // success: set session and redirect
                    Session["Username"] = username;
                    Session["UserType"] = loginType; // "Business" or "Account"
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblModalError.Text = "Invalid credentials for " + loginType + ".";
                    // keep modal open
                    ScriptManager.RegisterStartupScript(this, GetType(), "showModal", "document.getElementById('modalBackdrop').style.display='flex';", true);
                }
            }
        }
    }
}