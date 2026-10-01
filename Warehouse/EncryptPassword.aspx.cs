using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EncryptPassword : System.Web.UI.Page
{
    private readonly string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasAuthenticatedSession())
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            string passwordSuffix;
            try
            {
                passwordSuffix = WarehouseApiSecurity.GetRequiredSetting("EncryptPasswordDefaultPasswordSuffix");
            }
            catch (ConfigurationErrorsException)
            {
                lblStatus.Text = "Required appSetting 'EncryptPasswordDefaultPasswordSuffix' is not configured.";
                return;
            }

            const string query = "SELECT [User_Name],convert(varchar(20),[login_id])+@PasswordSuffix as [pwd],[login_id] FROM [Storage_Login]";
            DataTable users = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@PasswordSuffix", passwordSuffix);
                adapter.Fill(users);
            }

            gvPwdEN.DataSource = users;
            gvPwdEN.DataBind();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (Session != null)
        {
            ViewStateUserKey = Session.SessionID;
        }
    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!HasAuthenticatedSession())
        {
            Response.Redirect("~/Login/Login.aspx");
            return;
        }

        try
        {
            string masterSecret = WarehouseApiSecurity.GetRequiredSetting("EncryptPasswordMasterSecret");
            for (int i = 0; i < gvPwdEN.Rows.Count; i++)
            {
                string password = ((TextBox)gvPwdEN.Rows[i].FindControl("txtPwd")).Text.Trim();
                string loginId = ((TextBox)gvPwdEN.Rows[i].FindControl("txtUID")).Text.Trim();
                if (String.IsNullOrWhiteSpace(password) || String.IsNullOrWhiteSpace(loginId))
                {
                    lblStatus.Text = "Login ID and password are required for every row.";
                    return;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                for (int i = 0; i < gvPwdEN.Rows.Count; i++)
                {
                    string password = ((TextBox)gvPwdEN.Rows[i].FindControl("txtPwd")).Text.Trim();
                    string loginId = ((TextBox)gvPwdEN.Rows[i].FindControl("txtUID")).Text.Trim();
                    string passwordHash = CreateMd5Hash(password);
                    string masterPasswordHash = CreateMd5Hash(loginId + masterSecret);

                    const string update = @"update Storage_Login
                        set Password=convert(varbinary(300),@PasswordHash),
                            MasterPassword=convert(varbinary(300),@MasterPasswordHash)
                        where login_id=@LoginId";
                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        command.Parameters.AddWithValue("@MasterPasswordHash", masterPasswordHash);
                        command.Parameters.AddWithValue("@LoginId", loginId);
                        command.ExecuteNonQuery();
                    }
                }
            }

            lblStatus.ForeColor = System.Drawing.Color.ForestGreen;
            lblStatus.Text = "Password values updated.";
        }
        catch (ConfigurationErrorsException)
        {
            lblStatus.Text = "Required appSetting 'EncryptPasswordMasterSecret' is not configured.";
        }
        catch (Exception)
        {
            lblStatus.Text = "Unable to update password values.";
        }
    }

    private static bool HasAuthenticatedSession()
    {
        if (HttpContext.Current == null || HttpContext.Current.Session == null)
        {
            return false;
        }

        return !String.IsNullOrWhiteSpace(Convert.ToString(HttpContext.Current.Session["UserID"])) ||
               !String.IsNullOrWhiteSpace(Convert.ToString(HttpContext.Current.Session["Username"])) ||
               !String.IsNullOrWhiteSpace(Convert.ToString(HttpContext.Current.Session["username"]));
    }

    private static string CreateMd5Hash(string value)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            StringBuilder result = new StringBuilder(hash.Length * 2);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("x2"));
            }

            return result.ToString();
        }
    }
}
