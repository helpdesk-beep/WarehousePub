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
using System.Web;

/// <summary>
/// Summary description for JVS_Inspection
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JVS_Inspection : System.Web.Services.WebService
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr_Rabi2022"].ToString());
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    [WebMethod]
    public XmlElement FetchParameters(string BranchId)
    {
        XmlElement xmlElement = null;
        string respas = "";
        try
        {
            const string query = "select * from View_JVS_Inspection_for_JVSKharif_202223 where BranchId=@BranchId and Is_Inspected is null";
            //XmlElement xmlElement = null;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BranchId", BranchId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                XmlDataDocument xmldata1 = new XmlDataDocument(ds);
                xmlElement = xmldata1.DocumentElement;
            }
        }
        catch (Exception)
        {
            throw new SoapException("Unable to fetch inspection parameters.", SoapException.ServerFaultCode);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return xmlElement;
    }


}
