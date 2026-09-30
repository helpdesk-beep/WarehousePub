using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Upload_DSC : System.Web.Services.WebService
{
    [WebMethod(Description = "This Method Is Used to Upload DSC")]
    public string Insert_DSC_Data(string serialNumber, string SubjectName, string IssuerName, string publicKey, string FriendlyName, string CertificateVerified, string SimpleName, string SignatureAlgorithm, string CertificateArchived, string Thumbprint, DateTime NotBefore, DateTime NotAfter, string CertificateStr, string CertificateXml, int RawDataLenght, string Version, string DSC_Holder, string Mobile, string BG_Name, string BG_ID, string District_Id, string Branch_Id, string User_Type, string ipAddress, string Credential, string Client_IP)
    {
        WarehouseApiSecurity.RequireCredential(Credential, "WarehouseDscApiCredential");

        int parsedId;
        if ((User_Type != "B" && User_Type != "G") ||
            !Int32.TryParse(BG_ID, out parsedId) ||
            !Int32.TryParse(District_Id, out parsedId) ||
            !Int32.TryParse(Branch_Id, out parsedId) ||
            String.IsNullOrWhiteSpace(serialNumber) ||
            String.IsNullOrWhiteSpace(DSC_Holder) ||
            String.IsNullOrWhiteSpace(CertificateStr) ||
            String.IsNullOrWhiteSpace(CertificateXml) ||
            CertificateStr.Length > 1000000 ||
            CertificateXml.Length > 1000000 ||
            RawDataLenght < 0 ||
            NotAfter < NotBefore)
        {
            throw new SoapException("Invalid DSC upload data.", SoapException.ClientFaultCode);
        }

        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["FCIConnectionString"];
        if (settings == null || String.IsNullOrWhiteSpace(settings.ConnectionString))
        {
            throw new SoapException("The DSC database connection is not configured.", SoapException.ServerFaultCode);
        }

        const string query = @"INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_User_Upload_Detail]
            ([District_Id],[Branch_Id],[DSC_ID],[DSC_Holder_Name],[Mobile_No],[BG_Name],[BG_ID],[User_Type],
             [SerialNumber],[SubjectName],[IssuerName],[publicKey],[FriendlyName],[CertificateVerified],[SimpleName],
             [SignatureAlgorithm],[CertificateArchived],[Thumbprint],[NotBefore],[NotAfter],[CertificateStr],
             [CertificateXmlPK],[RawDataLenght],[Version],[Created_Date],[Created_By],Client_Ip)
            VALUES (@District_Id,@Branch_Id,'',@DSC_Holder,@Mobile,@BG_Name,@BG_ID,@User_Type,
             @SerialNumber,@SubjectName,@IssuerName,@publicKey,@FriendlyName,@CertificateVerified,@SimpleName,
             @SignatureAlgorithm,@CertificateArchived,@Thumbprint,@NotBefore,@NotAfter,@CertificateStr,
             @CertificateXml,@RawDataLenght,@Version,GETDATE(),@ipAddress,@Client_IP)";

        try
        {
            using (SqlConnection connection = new SqlConnection(settings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@District_Id", District_Id);
                command.Parameters.AddWithValue("@Branch_Id", Branch_Id);
                command.Parameters.AddWithValue("@DSC_Holder", DSC_Holder);
                command.Parameters.AddWithValue("@Mobile", (object)Mobile ?? DBNull.Value);
                command.Parameters.AddWithValue("@BG_Name", (object)BG_Name ?? DBNull.Value);
                command.Parameters.AddWithValue("@BG_ID", BG_ID);
                command.Parameters.AddWithValue("@User_Type", User_Type);
                command.Parameters.AddWithValue("@SerialNumber", serialNumber);
                command.Parameters.AddWithValue("@SubjectName", (object)SubjectName ?? DBNull.Value);
                command.Parameters.AddWithValue("@IssuerName", (object)IssuerName ?? DBNull.Value);
                command.Parameters.AddWithValue("@publicKey", (object)publicKey ?? DBNull.Value);
                command.Parameters.AddWithValue("@FriendlyName", (object)FriendlyName ?? DBNull.Value);
                command.Parameters.AddWithValue("@CertificateVerified", (object)CertificateVerified ?? DBNull.Value);
                command.Parameters.AddWithValue("@SimpleName", (object)SimpleName ?? DBNull.Value);
                command.Parameters.AddWithValue("@SignatureAlgorithm", (object)SignatureAlgorithm ?? DBNull.Value);
                command.Parameters.AddWithValue("@CertificateArchived", (object)CertificateArchived ?? DBNull.Value);
                command.Parameters.AddWithValue("@Thumbprint", (object)Thumbprint ?? DBNull.Value);
                command.Parameters.AddWithValue("@NotBefore", NotBefore);
                command.Parameters.AddWithValue("@NotAfter", NotAfter);
                command.Parameters.AddWithValue("@CertificateStr", CertificateStr);
                command.Parameters.AddWithValue("@CertificateXml", CertificateXml);
                command.Parameters.AddWithValue("@RawDataLenght", RawDataLenght);
                command.Parameters.AddWithValue("@Version", (object)Version ?? DBNull.Value);
                command.Parameters.AddWithValue("@ipAddress", (object)ipAddress ?? DBNull.Value);
                command.Parameters.AddWithValue("@Client_IP", (object)Client_IP ?? DBNull.Value);

                connection.Open();
                return command.ExecuteNonQuery() > 0 ? "Y" : "N";
            }
        }
        catch (Exception)
        {
            throw new SoapException("Unable to save DSC data.", SoapException.ServerFaultCode);
        }
    }
}
