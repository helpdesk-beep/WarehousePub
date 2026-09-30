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
/// Summary description for Get_MPSCSC_StorageBill_List
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Get_MPSCSC_StorageBill_List : System.Web.Services.WebService {
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Get_MPSCSC_StorageBill_List () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_List(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv24")
            {
                if (Commodity == "22")
                {
                    if (User_Type == "B")
                    {
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + BG_Id + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='22' and Branch_Id='" + BG_Id + "'";
                        query = "select Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and BO_Approval_Status='Y' and Bill_Number in (select distinct BillNo from mpscsc.dbo.[vwGetStorageVerifyStetus] where AproovedByIssue='1' or AproovedTypeDM='1')";


                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                    }
                }
            }
            SqlCommand cmd = new SqlCommand(query, con);
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
        }

        catch (Exception)
        {

            throw;

        }
    }
    public string Get_BranchId(string GodownId)
    {
        string Godown_ID = GodownId;
        string Branch_Id = "";
        string query = "";
        try
        {
            query = "select BranchID from tbl_MetaData_GODOWN_2018 where Godown_ID='" + Godown_ID + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Branch_Id = ds.Tables[0].Rows[0]["BranchID"].ToString();
            }
            else
            {
                Branch_Id = "";
            }

            return Branch_Id;
        }

        catch (Exception)
        {

            throw;

        }
    }
    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_Detail(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv24")
            {
                //if (Commodity == "22")
                //{
                    if (User_Type == "B")
                    {
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + BG_Id + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='B') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='22' and Branch_Id='" + BG_Id + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Storage_Bill_Details.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Storage_Bill_Details where Commodity_Id='"+ Commodity +"' and Branch_Id='"+ BG_Id +"' and BO_Approval_Status='Y' and Bill_Number in (select distinct BillNo from mpscsc.dbo.[vwGetStorageVerifyStetus] where AproovedByIssue='1' or AproovedTypeDM='1') and Bill_Number='"+ Bill_No +"'";

                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";

                    }
                //}
            }
            SqlCommand cmd = new SqlCommand(query, con);
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
        }

        catch (Exception)
        {

            throw;

        }
    }
}

