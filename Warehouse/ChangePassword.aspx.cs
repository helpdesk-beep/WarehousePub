using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Threading;

public partial class ChangePassword : System.Web.UI.Page
{
    private SqlConnection conStr = null;

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string newPwd = txtNewPwd.Text;
        string confirmPwd = txtConfirmPwd.Text;

        // 1. Password Match Validation
        if (newPwd != confirmPwd)
        {
            ShowMessage("Passwords do not match.");
            return;
        }

        // 2. Security Policy Validation: 15+ alphanumeric, non-sequential 
        if (!IsPolicyValid(newPwd))
        {
            ShowMessage("Security Policy Violation: 8+ characters, alphanumeric, and non-sequential required.");
            return;
        }

        // 3. Generate Secure Hash and Salt 
        string salt;
        string hashedPassword = GenerateHash(newPwd, out salt);

        // 4. Update Database via Dynamic Stored Procedure
        UpdatePasswordStoredProc(hashedPassword, salt, newPwd);
    }

    private bool IsPolicyValid(string pwd)
    {
        if (string.IsNullOrEmpty(pwd) || pwd.Length < 8) return false;

        bool hasText = false;
        bool hasDigit = false;

        for (int i = 0; i < pwd.Length; i++)
        {
            if (char.IsLetter(pwd[i])) hasText = true;
            if (char.IsDigit(pwd[i])) hasDigit = true;

            // Sequential check (e.g., 123, abc) 
            if (i < pwd.Length - 2)
            {
                if (pwd[i] + 1 == pwd[i + 1] && pwd[i] + 2 == pwd[i + 2]) return false;
            }
        }
        return hasText && hasDigit;
    }

    private void UpdatePasswordStoredProc(string hashedPwd, string salt, string password)
    {
        try
        {

            

            string[] strArr = (string[])Session["Securelogin"];
            if (strArr[4] != null)
                conStr = new SqlConnection(ConfigurationManager.ConnectionStrings[strArr[4]].ToString());

 		string ClientQuery = "UPDATE " + strArr[0] + " SET Password=@password, Salt=@Salt, FirstTimeLogin=1,hashedPassword=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            if (strArr[0].Contains("DistrictLogin"))
                ClientQuery = "UPDATE " + strArr[0] + " SET PWD=@password, Salt=@Salt, FirstTimeLogin=1,hashedPassword=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            if (strArr[0].Contains("Insp_Officer_login"))
                ClientQuery = "UPDATE " + strArr[0] + " SET O_Password=@password, Salt=@Salt, FirstTimeLogin=1,hashedPassword=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            if (strArr[0].Contains("tbl_MetaData_DISTRICT"))
                ClientQuery = "UPDATE " + strArr[0] + " SET DMPassword=@password, Salt=@Salt, FirstTimeLogin=1,hashedPassword=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            if (strArr[0].Contains("MetaDataBranchWithIssueCenter"))
                ClientQuery = "UPDATE " + strArr[0] + " SET BranchPwd=@password, Salt=@Salt, FirstTimeLogin=1,hashedPassword=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            if (strArr[0].Contains("tbl_MetaData_DISTRICT_DSO"))
                ClientQuery = "UPDATE tbl_MetaData_DISTRICT SET DSOPassord=@password, Salt_DSO=@Salt, FirstTimeLogin_DSO=1,HashedPassword_DSO=@HashedPassword WHERE " + strArr[1] + " = " + strArr[2] + " And (FirstTimeLogin=0 OR FirstTimeLogin is null) ";
            // using (SqlCommand cmd = new SqlCommand("usp_UpdateUserPassword_Dynamic", conStr))
            using (SqlCommand cmd = new SqlCommand(ClientQuery, conStr))
            {

                cmd.CommandType = CommandType.Text;

                // Pass the table name and session details
                // //    cmd.Parameters.AddWithValue("@TableName", "UserMaster");
                // cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(strArr[1]));
                //// cmd.Parameters.AddWithValue("@Scope", Session["Scope"].ToString());
                cmd.Parameters.AddWithValue("@HashedPassword", hashedPwd);
                cmd.Parameters.AddWithValue("@Salt", salt);
                cmd.Parameters.AddWithValue("@password", password);

                if (conStr.State == ConnectionState.Closed) conStr.Open();
                int res = cmd.ExecuteNonQuery();
                conStr.Close();

                // Success: Redirect to login or home
                if (res > 0)
                {
                    string msg = "Password updated successfully. Please login again.";
                    string redirectUrl = strArr[3];

                    string script = "setTimeout(function() { " +
                                    "alert('" + msg + "'); " +
                                    "window.location='" + redirectUrl + "'; " +
                                    "}, 2000);";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delayedRedirect", script, true);
                }
                else
                    ShowMessage("Password updatedion failed. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ShowMessage("System Error: " + ex.Message);
        }
    }

    private string GenerateHash(string password, out string saltHex)
    {
        byte[] saltBytes = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        saltHex = BitConverter.ToString(saltBytes).Replace("-", "");

        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            return Convert.ToBase64String(hash);
        }
    }

    private void ShowMessage(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msg + "');", true);
    }
}