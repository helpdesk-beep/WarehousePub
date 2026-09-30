using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class StatePages_InsertLicenceDetailsForRabi2026_27 : System.Web.UI.Page
{
    protected void btnInsertLicence_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[sp_InsertLicenceDetailsForRabi2026_27]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Success alert
                    string message = "Licence details inserted successfully!";
                    string script = "alert('" + message.Replace("'", "\\'") + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", script, true);
                }
                catch (Exception ex)
                {
                    // Error alert
                    string message = "Error: " + ex.Message;
                    string script = "alert('" + message.Replace("'", "\\'") + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
                }
            }
        }
    }
}