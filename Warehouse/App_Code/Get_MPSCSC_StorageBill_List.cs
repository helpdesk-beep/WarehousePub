using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Get_MPSCSC_StorageBill_List : System.Web.Services.WebService
{
    private string ConnectionString
    {
        get
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["FCIConnectionString"];
            if (settings == null || String.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new SoapException("The storage bill database connection is not configured.", SoapException.ServerFaultCode);
            }

            return settings.ConnectionString;
        }
    }

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_List(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity)
    {
        WarehouseApiSecurity.RequireCredential(Credential, "MPSCSCStorageBillApiCredential");
        ValidateRequest(BG_Id, User_Type, Commodity);
        if (Commodity != "22")
        {
            throw new SoapException("Unsupported commodity.", SoapException.ClientFaultCode);
        }

        string query;
        string branchId = null;
        if (User_Type == "B")
        {
            query = @"select Bill_Number
                from tbl_Storage_Bill_Details
                where Commodity_Id=@Commodity and Branch_Id=@BG_Id and BO_Approval_Status='Y'
                  and Bill_Number in
                    (select distinct BillNo from mpscsc.dbo.[vwGetStorageVerifyStetus]
                     where AproovedByIssue='1' or AproovedTypeDM='1')";
        }
        else
        {
            branchId = Get_BranchId(BG_Id);
            if (String.IsNullOrWhiteSpace(branchId))
            {
                throw new SoapException("Unknown godown.", SoapException.ClientFaultCode);
            }

            query = @"SELECT [Depositor_WHR_Id]
                from tbl_storage_Depositor_WHR_Relation as WHR
                where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01'
                  and Commodity_Id=@Commodity and Gid is not null
                  and [Depositor_WHR_Id] not in
                    (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC
                     where DSC.BranchID=@BranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G')
                order by [WHR_Issue_Date] desc";
        }

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@BG_Id", BG_Id);
            command.Parameters.AddWithValue("@Commodity", Commodity);
            if (branchId != null)
            {
                command.Parameters.AddWithValue("@BranchId", branchId);
            }

            DataSet result = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(result);
            }

            return result;
        }
    }

    public string Get_BranchId(string GodownId)
    {
        if (!IsNumericIdentifier(GodownId))
        {
            throw new SoapException("Invalid godown.", SoapException.ClientFaultCode);
        }

        const string query = "select BranchID from tbl_MetaData_GODOWN_2018 where Godown_ID=@GodownId";
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@GodownId", GodownId);
            connection.Open();
            object branchId = command.ExecuteScalar();
            return branchId == null || branchId == DBNull.Value ? String.Empty : Convert.ToString(branchId);
        }
    }

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_Detail(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No)
    {
        WarehouseApiSecurity.RequireCredential(Credential, "MPSCSCStorageBillApiCredential");
        ValidateRequest(BG_Id, User_Type, Commodity);

        string query;
        string branchId = null;
        if (User_Type == "B")
        {
            if (String.IsNullOrWhiteSpace(Bill_No) || Bill_No.Length > 100)
            {
                throw new SoapException("Invalid bill number.", SoapException.ClientFaultCode);
            }

            query = @"select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,
                    CONVERT(decimal(18,0),Net_Amount) as Net_Amount,
                    CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,
                    Service_Tax_Perc as GST_Per,
                    CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,
                    CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,
                    DateName(month, DateAdd(month, tbl_Storage_Bill_Details.Month, 0) - 1) as Bill_Month,
                    Crop_Year,Financial_Year,
                    (select Godown_Name from tbl_MetaData_GODOWN_2018 as G
                     where G.Godown_ID=tbl_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown
                from tbl_Storage_Bill_Details
                where Commodity_Id=@Commodity and Branch_Id=@BG_Id and BO_Approval_Status='Y'
                  and Bill_Number in
                    (select distinct BillNo from mpscsc.dbo.[vwGetStorageVerifyStetus]
                     where AproovedByIssue='1' or AproovedTypeDM='1')
                  and Bill_Number=@Bill_No";
        }
        else
        {
            branchId = Get_BranchId(BG_Id);
            if (String.IsNullOrWhiteSpace(branchId))
            {
                throw new SoapException("Unknown godown.", SoapException.ClientFaultCode);
            }

            query = @"SELECT [Depositor_WHR_Id]
                from tbl_storage_Depositor_WHR_Relation as WHR
                where WHR.GodownID=@BG_Id and WHR.CropYear='2019-20' and WHR.Arrival_Source='01'
                  and Commodity_Id=@Commodity and Gid is not null
                  and [Depositor_WHR_Id] not in
                    (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC
                     where DSC.BranchID=@BranchId and DSC.Commodity_Id=@Commodity and DSC.DSC_User_Type='G')
                order by [WHR_Issue_Date] desc";
        }

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@BG_Id", BG_Id);
            command.Parameters.AddWithValue("@Commodity", Commodity);
            if (User_Type == "B")
            {
                command.Parameters.AddWithValue("@Bill_No", Bill_No);
            }
            if (branchId != null)
            {
                command.Parameters.AddWithValue("@BranchId", branchId);
            }

            DataSet result = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(result);
            }

            return result;
        }
    }

    private static void ValidateRequest(string bgId, string userType, string commodity)
    {
        if (!IsNumericIdentifier(bgId) ||
            (userType != "B" && userType != "G") ||
            !IsNumericIdentifier(commodity))
        {
            throw new SoapException("Invalid storage bill request.", SoapException.ClientFaultCode);
        }
    }

    private static bool IsNumericIdentifier(string value)
    {
        if (String.IsNullOrEmpty(value) || value.Length > 20)
        {
            return false;
        }

        for (int i = 0; i < value.Length; i++)
        {
            if (!Char.IsDigit(value[i]))
            {
                return false;
            }
        }

        return true;
    }
}
