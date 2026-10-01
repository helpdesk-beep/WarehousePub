using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Get_Godown_WMS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Get_Godown_WMS : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";

    public Get_Godown_WMS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_WHR_List(string District_Id, string Credential)
    {
        try
        {
            string DistrictID = District_Id;
            WarehouseApiSecurity.RequireCredential(Credential, "LegacyWlcWmsCredential");
            const string query = "select Godown_Name,Godown_ID from tbl_MetaData_GODOWN_2018 where IsActive='Y' and Hired_Type not in ('Others','Virtual','Rack Point','FCI') and DistrictId=@DistrictId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DistrictId", DistrictID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                //ds.WriteXml(DestPdfFileName);
            }
            else
            {

            }
            return ds;
            //}
        }

        catch (Exception)
        {

            throw;

        }
    }

}
