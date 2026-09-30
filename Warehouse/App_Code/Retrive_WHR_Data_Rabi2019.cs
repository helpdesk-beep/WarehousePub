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
/// Summary description for Retrive_WHR_Data_Rabi2019
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Retrive_WHR_Data_Rabi2019 : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    public Retrive_WHR_Data_Rabi2019()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get WHR Data")]
    public DataSet Retrive_WHR_File(string WHR_Id, string Cred)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv24")
            {
                //string query = "SELECT [Depositor_WHR_Id],[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[CreatedDate],[MadeUpBags],[Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID] FROM [tbl_storage_Depositor_WHR_Relation] where Depositor_WHR_Id='" + WHR_Id + "'";
                //query = "SELECT [Depositor_WHR_Id],WHR.[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,WHR.[CreatedDate],[MadeUpBags],WHR.[Client_IP],[CropYear],[Remark],WHR.[BranchID],[DepositorID],[GodownID],WHR.Category_Id,R.Acpt_FCIRO_No as Depositor_Form_No,WHR.District_Id,SangrahadDate FROM [tbl_storage_Depositor_WHR_Relation] as WHR inner join tbl_Storage_Receipt_Details as R on R.WHR_Id=WHR.Depositor_WHR_Id and R.WHR_Flag='Y' where Depositor_WHR_Id='" + WHR_Id + "'";
                query = "SELECT [Depositor_WHR_Id],WHR.[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,WHR.[CreatedDate],[MadeUpBags],WHR.[Client_IP],[CropYear],[Remark],WHR.[BranchID],[DepositorID],[GodownID],WHR.Category_Id,ISNULL(R.Acpt_FCIRO_No,'0') as Depositor_Form_No,WHR.District_Id,SangrahadDate FROM [tbl_storage_Depositor_WHR_Relation] as WHR inner join tbl_Storage_Receipt_Details as R on R.WHR_Id=WHR.Depositor_WHR_Id and R.WHR_Flag='Y' where Depositor_WHR_Id='" + WHR_Id + "'";
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

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
    [WebMethod(Description = "This Method Is Used to Get WHR Data")]
    public DataSet Retrive_WHR_Preview(string WHR_Id, string Cred)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv24")
            {
                query = "SELECT [Depositor_WHR_Id],WHR.[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,WHR.[CreatedDate],[MadeUpBags],WHR.[Client_IP],[CropYear],[Remark],WHR.[BranchID],[DepositorID],[GodownID],WHR.Category_Id,ISNULL(R.Acpt_FCIRO_No,'0') as Depositor_Form_No,WHR.District_Id,SangrahadDate,(select G.Godown_Name+'('+G.Godown_ID+')' as Godown from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=GodownID) as Godown,(select C.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=WHR.Commodity_Id) as Commodity FROM [tbl_storage_Depositor_WHR_Relation] as WHR inner join tbl_Storage_Receipt_Details as R on R.WHR_Id=WHR.Depositor_WHR_Id and R.WHR_Flag='Y' where Depositor_WHR_Id='" + WHR_Id + "'";
                //query = "SELECT [Depositor_WHR_Id],WHR.[Commodity_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,WHR.[CreatedDate],[MadeUpBags],WHR.[Client_IP],[CropYear],[Remark],WHR.[BranchID],[DepositorID],[GodownID],WHR.Category_Id,ISNULL(R.Acpt_FCIRO_No,'0') as Depositor_Form_No,WHR.District_Id,SangrahadDate,(select G.Godown_Name+'('+G.Godown_ID+')' as Godown from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=GodownID) as Godown,(select C.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=WHR.Commodity_Id) as Commodity FROM [tbl_storage_Depositor_WHR_Relation] as WHR inner join tbl_Storage_Receipt_Details as R on R.WHR_Id=WHR.Depositor_WHR_Id and R.WHR_Flag='Y' where Depositor_WHR_Id='" + WHR_Id + "' and WHR.BranchID='2301001'";

            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

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

