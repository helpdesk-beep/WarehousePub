using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
/// <summary>
/// Summary description for Login_Inspection
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Login_Inspection : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    [WebMethod]
    public XmlElement Get_Login(string Mobile_No,  string password)
    {
        XmlElement xmlElement = null;
       // string respas = "";
        DataSet dt = new DataSet();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        try
        {

            if (con.State == ConnectionState.Closed){con.Open();}
            SqlCommand cmd = new SqlCommand("select User_Mobile_No,Password from tbl_Insp_login where User_Mobile_No=@Mobile_No and Password=@Password", con);
            cmd.Parameters.AddWithValue("@Mobile_No", Mobile_No);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 0;           
            da.Fill(dt);
            if (con.State == ConnectionState.Open){con.Close();}
            if (dt.Tables[0].Rows.Count > 0)
            {
                XmlDataDocument xmldata1 = new XmlDataDocument(dt);
                xmlElement = xmldata1.DocumentElement;
            }
            //return dt;
        }
        catch (Exception)
        {
            throw new SoapException("Unable to authenticate inspection user.", SoapException.ServerFaultCode);
        }
        finally
        {
            if (con.State == ConnectionState.Open){ con.Close(); }

        }
        return xmlElement;     
    }

}
