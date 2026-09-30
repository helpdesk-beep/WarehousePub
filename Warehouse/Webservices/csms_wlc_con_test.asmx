<%@ WebService Language="C#" Class="csms_wlc_con_test" %>
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Security.Principal;
using System.Globalization;
using System.IO;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class csms_wlc_con_test : System.Web.Services.WebService
{

    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLC"].ToString());
     public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ToString());
    private SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;


    //[WebMethod]
    //public string HelloWorld()
    //{
    //    return "Hello World";
    //}

    [WebMethod]
    public string SubmitTest(string UserName, string PassKey, string remark)
    {
        string json = "";
        string Code = "";
        string Remark = "";
        if (UserName == "csms" && PassKey == "csms")
        {
            SqlCommand cmd = new SqlCommand();
            string str = "";
            try
            {
                str = "INSERT INTO [dbo].[csms_wlc_con_test]([insertdate],[remark]) VALUES (getdate(),'" + remark + "'); SELECT CAST(scope_identity() AS int) as AcknoledgementNo";
                da = new SqlDataAdapter(str, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Code = "S0";
                    Remark = "Data Saved";
                    string Acknoledgement = ds.Tables[0].Rows[0]["AcknoledgementNo"].ToString();
                    json += "{\"" + "Response" + "\":" + "[";
                    json += "{" + "\"" + "Code" + "\":" + "\"" + Code + "\"," + "\"" + "Remark" + "\":" + "\"" + Remark + "\" , " + "\"" + "AcknowledgementNo" + "\":" + "\"" + Acknoledgement + "\"";
                    json = json.TrimEnd(',');
                    json += "}]}";
                }
                else
                {
                    Code = "E1";
                    Remark = "Enter Valid Data";
                    json += "{\"" + "Response" + "\":" + "[";
                    json += "{" + "\"" + "Code" + "\":" + "\"" + Code + "\"," + "\"" + "Remark" + "\":" + "\"" + Remark + "\"";
                    json = json.TrimEnd(',');
                    json += "}]}";
                }
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                Code = "E2";
                Remark = ex.ToString();
                json += "{\"" + "Response" + "\":" + "[";
                json += "{" + "\"" + "Code" + "\":" + "\"" + Code + "\"," + "\"" + "Remark" + "\":" + "\"" + Remark + "\"";
                json = json.TrimEnd(',');
                json += "}]}";
            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }
        else
        {

            Code = "E0";
            Remark = "Invalid Key";
            json += "{\"" + "Response" + "\":" + "[";
            json += "{" + "\"" + "Code" + "\":" + "\"" + Code + "\"," + "\"" + "Remark" + "\":" + "\"" + Remark + "\"";
            json = json.TrimEnd(',');
            json += "}]}";

        }
        return json;
    }


    [WebMethod]
    public DataTable ReadTest(string UserName, string PassKey)
    {
        DataTable dt = null;
        if (UserName != "" && PassKey != "")
        {
            if (UserName == "csms" && PassKey == "csms")
            {

                string sql = "SELECT top 100 * from [dbo].[csms_wlc_con_test] order by insertdate desc;";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    return dt;
                }
            }
            else
            {
                return dt;
            }
            return dt;
        }
        else
        {
            return dt;
        }

    }


}