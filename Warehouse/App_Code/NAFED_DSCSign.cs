using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for NAFED_DSCSign
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class NAFED_DSCSign : System.Web.Services.WebService
{
    private string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();

    public NAFED_DSCSign()
    {

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(Description = "This Method Is Used to Get Duplicate Storage Bill List NAFED")]
    public DataSet Retrieve_Bill_List_NAFED_Dublicate_Bill(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Godown_Type, string Bill_Mode)
    {
        DataSet ds = new DataSet();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcBillCredential"))
            {
                if (Bill_Mode == "N" && User_Type == "B" && Godown_Type == "MPWLC")
                {
                    string query = @"SELECT DISTINCT Bill_Number 
                                     FROM tbl_Storage_Bill_Details_Nafed_Duplicate_Bill 
                                     WHERE Commodity_Id=@Commodity 
                                       AND Branch_Id=@BG_Id 
                                       AND From_Date>='2024-04-01' 
                                       AND Bill_Number NOT IN (
                                           SELECT DISTINCT I.Ref_Bill_No as Bill_Number  
                                           FROM tbl_Digitally_Signed_Bill_Details_Nafed_Duplicate_Bill AS I 
                                           WHERE Branch_Id=@BG_Id and I.Ref_Bill_No is not null
                                       ) 
                                       AND Bill_Type='FD'";

                    using (SqlConnection con = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Commodity", Commodity);
                        cmd.Parameters.AddWithValue("@BG_Id", BG_Id);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [WebMethod(Description = "This Method Is Used to Get Duplicate Storage Bill Details For NAFED")]
    public DataSet Retrieve_NAFED_Bill_Detail_Dublicate_Bill(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        DataSet ds = new DataSet();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Credential, "LegacyWlcBillCredential"))
            {
                if (Bill_Mode == "N" && User_Type == "B" && Bill_Type == "MPWLC")
                {
                    // Fixed: Dynamic @Commodity Parameter used instead of Hardcoded '63'
                    string query = @"SELECT Bill_Number,
                                            Commodity_Rate AS Rate_PM,
                                            Per_Day_Rate,
                                            CONVERT(decimal(18,0), Net_Amount) AS Net_Amount,
                                            CONVERT(decimal(18,0), Sub_Amount) AS Sub_Amount,
                                            Service_Tax_Perc AS GST_Per,
                                            CONVERT(decimal(18,0), Service_Tax_Amt) AS GST_Amount,
                                            CONVERT(varchar(10), tbl_Storage_Bill_Details_Nafed_Duplicate_Bill.Created_Date, 103) AS Bill_Generated_Date,
                                            DATENAME(month, DATEADD(month, tbl_Storage_Bill_Details_Nafed_Duplicate_Bill.Month, 0) - 1) AS Bill_Month,
                                            Crop_Year,
                                            Financial_Year,
                                            (SELECT Godown_Name FROM tbl_MetaData_GODOWN_2018 AS G WHERE G.Godown_ID = tbl_Storage_Bill_Details_Nafed_Duplicate_Bill.Godown_Id) + '(' + tbl_Storage_Bill_Details_Nafed_Duplicate_Bill.Godown_Id + ')' AS Godown,
                                            Depositor_Category,
                                            Bill_Type,
                                            (SELECT W.Acc_Holder_Name FROM tbl_Institution_Account_Details AS W WHERE W.Institution = 'MPWLC') AS Acc_Holder_Name,
                                            (SELECT W.Account_No FROM tbl_Institution_Account_Details AS W WHERE W.Institution = 'MPWLC') AS Account_No,
                                            (SELECT W.IFSC_Code FROM tbl_Institution_Account_Details AS W WHERE W.Institution = 'MPWLC') AS IFSC_Code 
                                     FROM tbl_Storage_Bill_Details_Nafed_Duplicate_Bill 
                                     WHERE Commodity_Id = @Commodity 
                                       AND Branch_Id = @BG_Id 
                                       AND Bill_Number = @Bill_No 
                                       AND From_Date >= '2024-04-01'";

                    using (SqlConnection con = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Commodity", Commodity);
                        cmd.Parameters.AddWithValue("@BG_Id", BG_Id);
                        cmd.Parameters.AddWithValue("@Bill_No", Bill_No);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [WebMethod(Description = "This Method Is Used to Get Duplicate Storage Bill Detail for NAFED")]
    public DataSet Retrieve_NAFEDBillDetail_Dublicate_Bill(string Bill_No, string Cred, string Bill_Type)
    {
        DataSet ds = new DataSet();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "LegacyWlcBillCredential") && Bill_Type == "MPWLC")
            {
                string query = @"SELECT B.Bill_Number, B.District_Id AS District_Id, B.Branch_Id, B.Depositor_Id, 
                                        B.Commodity_Id, B.Financial_Year, B.Commodity_Rate, B.Per_Day_Rate, 
                                        B.Net_Amount, B.Sub_Amount, B.Service_Tax_Perc AS GST_Perc, 
                                        B.Service_Tax_Amt AS GST_Amt, B.Created_Date, B.Client_IP AS Created_By, 
                                        B.Month, B.Godown_Id AS Godown_Id, B.Crop_Year 
                                 FROM tbl_Storage_Bill_Details_Nafed_Duplicate_Bill AS B 
                                 WHERE Bill_Number = @Bill_No";

                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Bill_No", Bill_No);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [WebMethod(Description = "This Method Is Used to Insert Duplicate Storage Bill List NAFED")]
    public string Insert_Bill_Data_NAFED_Dublicate_Bill(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string B_Type, string Bill_Mode)
    {
        string Is_SuccessInsert = "N";
        string Is_Verify = Is_Verified(SerNo, User_Type);

        string Bill_Type_SubStr2 = "";
        if (BillNo.Length >= 20)
            Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        else if (BillNo.Length >= 5)
            Bill_Type_SubStr2 = BillNo.Substring(4, 1);

        string Godown_Code = "91";
        if (Bill_Type_SubStr2 == "2" || Bill_Type_SubStr2 == "5" || Bill_Type_SubStr2 == "7") Godown_Code = "81";
        else if (Bill_Type_SubStr2 == "3") Godown_Code = "83";
        else if (Bill_Type_SubStr2 == "1") Godown_Code = "91";
        else if (Bill_Type_SubStr2 == "4") Godown_Code = "84";
        else if (Bill_Type_SubStr2 == "6") Godown_Code = "86";

        string MPWLCParty = "Madhya Pradesh Warehousing and Logistics Corporation";

        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "LegacyWlcBillCredential") && Is_Verify == "Y" && !string.IsNullOrEmpty(Client_IP) && !string.IsNullOrEmpty(User_Type))
            {
                string SubValue = "";
                string[] Split_result = Subject.Split(',');
                foreach (string val in Split_result)
                {
                    if (val.Contains("CN="))
                    {
                        SubValue = val;
                        break;
                    }
                }

                int startindex = SubValue.IndexOf('=');
                string DSC_Holder = (startindex > -1) ? SubValue.Substring(startindex + 1).Trim() : Subject;

                string Bill_Number = "", District_Id = "", Branch_Id = "", Depositor_Id = "", Commodity_Id = "", Financial_Year = "";
                string Per_Month_Rate = "", Per_Day_Rate = "", Net_Amount = "", Sub_Amount = "", GST_Perc = "", GST_Amt = "";
                string Created_Date = "", Created_By = "", Month_No = "", Godown_Id = "", Crop_Year = "", G_Bill_No = "";

                if (ds != null && ds.Tables.Count > 12 && ds.Tables[12].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[12].Rows[0];
                    Bill_Number = row["Bill_Number"].ToString();
                    District_Id = row["District_Id"].ToString();
                    Branch_Id = row["Branch_Id"].ToString();
                    Depositor_Id = row["Depositor_Id"].ToString();
                    Commodity_Id = row["Commodity_Id"].ToString();
                    Financial_Year = row["Financial_Year"].ToString();
                    Per_Month_Rate = row["Commodity_Rate"].ToString();
                    Per_Day_Rate = row["Per_Day_Rate"].ToString();
                    Net_Amount = row["Net_Amount"].ToString();
                    Sub_Amount = row["Sub_Amount"].ToString();
                    GST_Perc = row["GST_Perc"].ToString();
                    GST_Amt = row["GST_Amt"].ToString();
                    Created_Date = row["Created_Date"].ToString();
                    Created_By = row["Created_By"].ToString();
                    Month_No = row["Month"].ToString();
                    Godown_Id = row["Godown_Id"].ToString();
                    Crop_Year = row["Crop_Year"].ToString();

                    G_Bill_No = Bill_Number;

                    if (Bill_Mode == "F") Bill_Number = Bill_Number + "F1";
                    else if (Bill_Mode == "F2") Bill_Number = Bill_Number + "F2";
                    else if (Bill_Mode == "F3") Bill_Number = Bill_Number + "F3";
                }

                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + AccountNo + IFSC + SerNo + Client_IP + DSC_Holder;

                var sha1 = SHA1.Create();
                byte[] buf = Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                string hashstrs = BitConverter.ToString(hash).Replace("-", "");

                // Safe Parameterized Query (Prevents Empty CommandText & Injection)
                string query = @"INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_Nafed_Duplicate_Bill]
                                ([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],
                                 [Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],
                                 [Crop_Year],[Account_No],[IFSC_Code],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],
                                 [Client_Ip],[DSC_User_Type],[Ref_Bill_No],[Party_Name],[Party_Id],[Bill_Type]) 
                                 VALUES 
                                (@Bill_Number, @District_Id, @Branch_Id, @Depositor_Id, @Commodity_Id, @Financial_Year, @Per_Month_Rate, @Per_Day_Rate,
                                 @Net_Amount, @Sub_Amount, @GST_Perc, @GST_Amt, @Created_Date, @Created_By, @Month_No, @Godown_Id,
                                 @Crop_Year, @Account_No, @IFSC_Code, @WHR_Check_Sum, GETDATE(), @CreatedBy, @DSC_Serial_No, @DSC_Holder_Name,
                                 @Client_Ip, @DSC_User_Type, @Ref_Bill_No, @Party_Name, @Party_Id, @Bill_Type)";

                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Bill_Number", Godown_Code + Bill_Number);
                    cmd.Parameters.AddWithValue("@District_Id", District_Id);
                    cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
                    cmd.Parameters.AddWithValue("@Depositor_Id", Depositor_Id);
                    cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id);
                    cmd.Parameters.AddWithValue("@Financial_Year", Financial_Year);
                    cmd.Parameters.AddWithValue("@Per_Month_Rate", Per_Month_Rate);
                    cmd.Parameters.AddWithValue("@Per_Day_Rate", Per_Day_Rate);
                    cmd.Parameters.AddWithValue("@Net_Amount", Net_Amount);
                    cmd.Parameters.AddWithValue("@Sub_Amount", Sub_Amount);
                    cmd.Parameters.AddWithValue("@GST_Perc", GST_Perc);
                    cmd.Parameters.AddWithValue("@GST_Amt", GST_Amt);
                    cmd.Parameters.AddWithValue("@Created_Date", Created_Date);
                    cmd.Parameters.AddWithValue("@Created_By", Created_By);
                    cmd.Parameters.AddWithValue("@Month_No", Month_No);
                    cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                    cmd.Parameters.AddWithValue("@Crop_Year", Crop_Year);
                    cmd.Parameters.AddWithValue("@Account_No", AccountNo);
                    cmd.Parameters.AddWithValue("@IFSC_Code", IFSC);
                    cmd.Parameters.AddWithValue("@WHR_Check_Sum", hashstrs);
                    cmd.Parameters.AddWithValue("@CreatedBy", IP);
                    cmd.Parameters.AddWithValue("@DSC_Serial_No", SerNo);
                    cmd.Parameters.AddWithValue("@DSC_Holder_Name", DSC_Holder);
                    cmd.Parameters.AddWithValue("@Client_Ip", Client_IP);
                    cmd.Parameters.AddWithValue("@DSC_User_Type", User_Type);
                    cmd.Parameters.AddWithValue("@Ref_Bill_No", G_Bill_No);
                    cmd.Parameters.AddWithValue("@Party_Name", MPWLCParty);
                    cmd.Parameters.AddWithValue("@Party_Id", "9999");
                    cmd.Parameters.AddWithValue("@Bill_Type", B_Type);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Is_SuccessInsert = "Y";
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

    public string Get_BranchId(string GodownId)
    {
        string Branch_Id = "";
        try
        {
            string query = "SELECT BranchID FROM tbl_MetaData_GODOWN_2018 WHERE Godown_ID = @Godown_ID";
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Godown_ID", GodownId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Branch_Id = ds.Tables[0].Rows[0]["BranchID"].ToString();
                }
            }
            return Branch_Id;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string GetBranchIdByDepot(string BranchID)
    {
        string Branch_Id = "";
        try
        {
            string query = "SELECT BranchID FROM tbl_MetaData_Depot WHERE BranchID = @BranchID";
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Branch_Id = ds.Tables[0].Rows[0]["BranchID"].ToString();
                }
            }
            return Branch_Id;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string Is_Verified(string SerNo, string UserType)
    {
        string Is_Valid = "N";
        try
        {
            string query = @"SELECT * FROM [tbl_DSC_User_Upload_Detail] 
                             WHERE SerialNumber=@SerialNumber 
                               AND Verification_Status='Approve' 
                               AND NotAfter>=GETDATE() 
                               AND NotBefore<=GETDATE()";

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@SerialNumber", SerNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Is_Valid = "Y";
                }
            }
            return Is_Valid;
        }
        catch (Exception)
        {
            throw;
        }
    }
}