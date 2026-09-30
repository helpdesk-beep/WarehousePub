using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for SendDataToCCRL
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SendDataToCCRL : System.Web.Services.WebService
{
    public SendDataToCCRL()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    // Replace with your actual connection string
    //private string connString = "Server=myServer;Database=myDB;User Id=myUser;Password=myPassword;";

    [WebMethod(Description = "Securely fetches data from the stored procedure")]
    public string GetProcedureData(string username, string password, string FromDate, string ToDate)
    {
        // 1. AUTHENTICATION FIRST
        if (!IsValidUser(username, password))
        {
            return "Error: Authentication Failed. Invalid credentials.";
        }

        // 2. IF VALID, GET DATA
        try
        {
            return ExecuteStoredProcedure(FromDate, ToDate);
        }
        catch (Exception ex)
        {
            return "Error executing procedure: " + ex.Message;
        }
    }

    private bool IsValidUser(string user, string pass)
    {
        // Replace this with your actual DB lookup or Identity check
        return (user == "admin" && pass == "secure123");
    }

    private string ExecuteStoredProcedure(string FromDate, string ToDate)
    {

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("NCCF_WHR_Data_For_CCRL", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add your parameters here
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.CommandTimeout = 3600;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Convert DataTable to a simple JSON string or XML
                return ConvertDataTableToJSON(dt);
            }
        }
    }
    private string ConvertDataTableToJSON11(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        // Set the limit to the maximum possible integer value
        serializer.MaxJsonLength = Int32.MaxValue;

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            var row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    private string ConvertDataTableToJSON(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            var row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                // Check if the value is a DateTime
                if (dr[col] is DateTime)
                {
                    // Manually format the date to ISO 8601 string
                    row.Add(col.ColumnName, ((DateTime)dr[col]).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    row.Add(col.ColumnName, dr[col]);
                }
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

}
