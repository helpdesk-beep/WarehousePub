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
/// Summary description for Insert_DSWHR_Data_Rabi2019
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Insert_DSWHR_Data_Rabi2019 : System.Web.Services.WebService {
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Insert_DSWHR_Data_Rabi2019 () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]

    public string Insert_WHR_File(string WHR,DataSet ds,string IP,string Cred, string StrX,string Subject,string SerNo,string Client_IP,string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;

       
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds=ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "')";
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                         string XMLData = DSString;
                         //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File]) VALUES ('" + WHRID + "','" + XMLData + "')";
                         string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                         SqlCommand cmd3 = new SqlCommand(query3, con);
                         con.Open();
                         int c=cmd3.ExecuteNonQuery();
                         con.Close();
                         if (c > 0)
                         {
                             Is_SuccessInsert = "Y";
                         }
                        
                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public string Is_Verified(string SerNo, string UserType)
    {
        string Is_Valid = "";
        string SerialNo = SerNo;
        string UType = UserType;
        string query = "";
        try
        {
                //string query = "SELECT [Depositor_WHR_Id],[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[CreatedDate],[MadeUpBags],[Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID] FROM [tbl_storage_Depositor_WHR_Relation] where Depositor_WHR_Id='" + WHR_Id + "'";
            //query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE()";
            query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE() AND User_Type='" + UType + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Is_Valid = "Y";
            }
            else
            {
                Is_Valid = "N";
            }

            return Is_Valid;
        }

        catch (Exception)
        {

            throw;

        }
    }
    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2021(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2020]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2020]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";
                    //string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2020]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2020]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2020] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2022(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2020]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2021]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2020]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2021]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2020] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2021] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2023(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2022]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2022]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2022] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }


    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2024(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2023]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2023]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2023] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }


    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2025(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2024]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2024]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2024] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_WHR_File_CMS2025_26(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2026]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2025]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2025] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML 2026 27")]
    public string Insert_WHR_File_CMS2026_27(string WHR, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;


        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcWhrCredential") && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
            {
                string SValue = "";
                string SubValue = "";
                string DSC_Subject = Subject;
                string[] Split_result = DSC_Subject.Split(',');
                foreach (string val in Split_result)
                {
                    SValue = val;
                    if (val.Contains("CN="))
                    {
                        SubValue = SValue;
                    }
                }
                int SubLengh = SubValue.Length;
                int startindex = SubValue.IndexOf('=');
                int Endindex = SubLengh;
                string outputstring = SubValue.Substring(startindex + 1, Endindex - startindex - 1);
                //int startindex = DSC_Subject.IndexOf('=');
                //int Endindex = DSC_Subject.IndexOf(',');
                //string outputstring = DSC_Subject.Substring(startindex + 1, Endindex - startindex - 1);
                string DSC_Holder = outputstring;
                string Serial_No = SerNo;
                // GET LENGHTH
                int LenOfDSCH = DSC_Holder.Length;
                //
                //DataSet dsx = new DataSet();
                //dsx.ReadXml(strReader);
                string DSString = StrX.ToString();
                DataSet XMLds = new DataSet();
                XMLds = ds;
                string WHRID = WHR;
                //string XMLData = "";
                string IpAdd = IP;
                string LocalIP = Client_IP;

                string Sig_SignatureValue = "";
                string Cano_Algorithm = "";
                string SM_Algorithm = "";
                //string SignatureValue = "";
                string Ref_DigestValue = "";
                string TF_Algorithm = "";
                string DM_Algorithm = "";
                string KeyInfo_KeyName = "";
                string RSA_Modulus = "";
                string RSA_Exponent = "";
                string X509Certificate = "";

                string Depositor_WHR_Id = "";
                string Commodity_Id = "";
                string Depositor_Name = "";
                string Date_of_Deposit = "";
                string TotalBags_Received = "";
                string Total_Qty_Received = "";
                string AvgMoisture_Content = "";
                string MktValue_of_Commodity = "";
                string WHR_Issue_Date = "";
                string WHR_CreatedDate = "";
                string WHR_Client_IP = "";
                string CropYear = "";
                string Remark = "";
                string BranchID = "";
                string DepositorID = "";
                string GodownID = "";
                string Depositor_Form_No = "";
                string Grade = "";
                string District_Id = "";
                string AvgMoisture_Content_To = "";
                string SangrahadDate = "";
                if (XMLds.Tables[0].Rows.Count > 0)
                {
                    Sig_SignatureValue = XMLds.Tables[0].Rows[0]["SignatureValue"].ToString();
                    Cano_Algorithm = XMLds.Tables[2].Rows[0]["Algorithm"].ToString();
                    SM_Algorithm = XMLds.Tables[3].Rows[0]["Algorithm"].ToString();
                    Ref_DigestValue = XMLds.Tables[4].Rows[0]["DigestValue"].ToString();
                    TF_Algorithm = XMLds.Tables[6].Rows[0]["Algorithm"].ToString();
                    DM_Algorithm = XMLds.Tables[7].Rows[0]["Algorithm"].ToString();
                    KeyInfo_KeyName = XMLds.Tables[8].Rows[0]["KeyName"].ToString();
                    RSA_Modulus = XMLds.Tables[10].Rows[0]["Modulus"].ToString();
                    RSA_Exponent = XMLds.Tables[10].Rows[0]["Exponent"].ToString();
                    X509Certificate = XMLds.Tables[11].Rows[0]["X509Certificate"].ToString();

                    Depositor_WHR_Id = XMLds.Tables[12].Rows[0]["Depositor_WHR_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Depositor_Name = XMLds.Tables[12].Rows[0]["Depositor_Name"].ToString();
                    Date_of_Deposit = XMLds.Tables[12].Rows[0]["Date_of_Deposit"].ToString();
                    TotalBags_Received = XMLds.Tables[12].Rows[0]["TotalBags_Received"].ToString();
                    Total_Qty_Received = XMLds.Tables[12].Rows[0]["Total_Qty_Received"].ToString();
                    AvgMoisture_Content = XMLds.Tables[12].Rows[0]["AvgMoisture_Content"].ToString();
                    MktValue_of_Commodity = XMLds.Tables[12].Rows[0]["MktValue_of_Commodity"].ToString();
                    WHR_Issue_Date = XMLds.Tables[12].Rows[0]["WHR_Issue_Date"].ToString();
                    WHR_CreatedDate = XMLds.Tables[12].Rows[0]["CreatedDate"].ToString();
                    WHR_Client_IP = XMLds.Tables[12].Rows[0]["Client_IP"].ToString();
                    CropYear = XMLds.Tables[12].Rows[0]["CropYear"].ToString();
                    Remark = XMLds.Tables[12].Rows[0]["Remark"].ToString();
                    BranchID = XMLds.Tables[12].Rows[0]["BranchID"].ToString();
                    DepositorID = XMLds.Tables[12].Rows[0]["DepositorID"].ToString();
                    GodownID = XMLds.Tables[12].Rows[0]["GodownID"].ToString();
                    Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();

                }
                //Generate CSum
                string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                string query = "INSERT INTO [tbl_Digitally_Signed_WHR_CMS2026]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "',N'" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    string query2 = "INSERT INTO [tbl_Digital_Signature_CMS2026]([WHR_No],DSC_Serial_No,DSC_Holder_Name,[Sig_SignatureValue],[Ref_DigestValue],[RSA_Modulus],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + Sig_SignatureValue + "','" + Ref_DigestValue + "','" + RSA_Modulus + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        string XMLData = DSString;
                        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File_CMS2026] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        SqlCommand cmd3 = new SqlCommand(query3, con);
                        con.Open();
                        int c = cmd3.ExecuteNonQuery();
                        con.Close();
                        if (c > 0)
                        {
                            Is_SuccessInsert = "Y";
                        }

                    }
                }
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }
}

