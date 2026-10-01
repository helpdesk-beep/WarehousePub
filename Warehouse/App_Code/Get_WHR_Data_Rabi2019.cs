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
/// Summary description for Get_WHR_Data_Rabi2019
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Get_WHR_Data_Rabi2019 : System.Web.Services.WebService {
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Get_WHR_Data_Rabi2019 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get WHR Data")]


    //public byte Retrive_WHR_File(string WHR_Id, string DistrictId, string Branch_Id, String WHR_Date, String Commodity, String WHR, Byte WHR_File, String WHR_File_Type, String WHR_File_Name)
    public Byte[] Retrive_WHR_File(string WHR_Id)
    {
        Byte[] WHR_DATA = new Byte[] { 0 };
        if (String.IsNullOrWhiteSpace(WHR_Id))
        {
            throw new SoapException("A valid WHR identifier is required.", SoapException.ClientFaultCode);
        }

        WarehouseApiSecurity.RequireApiKey();
        try
        {
            const string query = "SELECT [WHR_Id],[WHR_File],[WHR_File_Type],[WHR_File_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_WHR_File_Data] where WHR_Id=@WHR_Id";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@WHR_Id", WHR_Id);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("WHR_File")))
                    {
                        WHR_DATA = (Byte[])reader["WHR_File"];
                    }
                }
            }
        }
        catch (Exception)
        {
            throw new SoapException("Unable to retrieve WHR file.", SoapException.ServerFaultCode);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        return WHR_DATA;
    }
    private string getDate_MDY(string inDate)
    {
        string dat = "";
        if (inDate != "")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dat = (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        return dat;
    }

    
}
