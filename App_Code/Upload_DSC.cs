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
/// Summary description for Upload_DSC
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Upload_DSC : System.Web.Services.WebService {
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Upload_DSC () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Upload DSC")]
    public string Insert_DSC_Data(string serialNumber, string SubjectName, string IssuerName, string publicKey, string FriendlyName, string CertificateVerified, string SimpleName, string SignatureAlgorithm, string CertificateArchived, string Thumbprint, DateTime NotBefore, DateTime NotAfter, string CertificateStr, string CertificateXml, int RawDataLenght, string Version, string DSC_Holder, string Mobile, string BG_Name, string BG_ID, string District_Id, string Branch_Id, string User_Type, string ipAddress, string Credential, string Client_IP)
    {
        int Count = 0;
        string IsUpload = "";
        if (Credential == "WLC2019DSCNic")
        {
            string LocalIP = Client_IP;
            string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_User_Upload_Detail]([District_Id],[Branch_Id],[DSC_ID],[DSC_Holder_Name],[Mobile_No],[BG_Name],[BG_ID],[User_Type],[SerialNumber],[SubjectName],[IssuerName],[publicKey],[FriendlyName],[CertificateVerified],[SimpleName],[SignatureAlgorithm],[CertificateArchived],[Thumbprint],[NotBefore],[NotAfter],[CertificateStr],[CertificateXmlPK],[RawDataLenght],[Version],[Created_Date],[Created_By],Client_Ip) VALUES ('" + District_Id + "','" + Branch_Id + "','','" + DSC_Holder + "','" + Mobile + "','" + BG_Name + "','" + BG_ID + "','" + User_Type + "','" + serialNumber + "','" + SubjectName + "','" + IssuerName + "','" + publicKey + "','" + FriendlyName + "','" + CertificateVerified + "','" + SimpleName + "','" + SignatureAlgorithm + "','" + CertificateArchived + "','" + Thumbprint + "','" + NotBefore + "','" + NotAfter + "','" + CertificateStr + "','" + CertificateXml + "','" + RawDataLenght + "','" + Version + "',getdate(),'" + ipAddress + "','" + LocalIP + "')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            Count = cmd.ExecuteNonQuery();
            con.Close();
            if (Count > 0)
            {
                IsUpload = "Y";
            }
            else
            {
                IsUpload = "N";
            }
        }
        return IsUpload;
    }
    
}

