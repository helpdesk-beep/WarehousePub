using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Collections;
using System.Linq;

/// <summary>
/// Summary description for Retrive_MPSCSC_Storage._Bill
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Retrive_MPSCSC_Storage_Bill : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    string DSC_Serial_No_RAM = "";
    string DSC_Holder_Name_RAM = "";
    string Client_Ip_RAM = "";
    string DSC_User_Type_RAM = "";
    string Bill_Check_Sum_RAM = "";
    string CreatedDate_RAM = "";
    string CreatedBy_RAM = "";
    string T_MPWLC_Part = "";
    string T_TDS = "";
    string T_TDeduction = "";

    //private SqlDataAdapter dataAdapter;
    //private DataSet dataset;
    //private SqlTransaction trans = null;
    //private SqlCommand commandt = null;
    //string Query = "";
    public Retrive_MPSCSC_Storage_Bill()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                }
                else if (Bill_Type == "PVT")
                {
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y')";
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";

                }
                else if (Bill_Type == "VPVT")
                {
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y')";
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Vacant_Capacity_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";
                   // query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";

                }

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

    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail For VC")]
    public DataSet Retrive_Biil_Detail_For_VC(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details_For_VC as B where Bill_Number='" + Bill_No + "'";
                }                
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


    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_For_NAFED(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                }
                else if (Bill_Type == "PVT")
                {
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y')";
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";

                }

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

    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_SteelSilo(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Steel_Silo_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                }
                else if (Bill_Type == "PVT")
                {
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y')";
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Steel_Silo_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";

                }

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
    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_New(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,'' as Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Summary as B where Bill_Number='" + Bill_No + "'";
                }
                else if (Bill_Type == "PVT")
                {
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                    //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y')";
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";

                }

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
    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_R(string Bill_No, string Cred)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.BO_Approval_Status='Y'";
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
    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_R_New2(string Bill_No, string Cred)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                //query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Institution_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and B.BO_Approval_Status='Y'";
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
    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail")]
    public DataSet Retrive_Biil_Detail_RNew(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            string BillType = Bill_Type;
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount ,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay, GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount, RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.JVS_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.DSC_Serial_No='' )";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount ,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay, GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount, RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.JVS_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.JVS_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.JVS_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";

                        }
                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
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
    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_List(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Godown_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //if (Godown_Type == "SS" )
                        //{
                        //    query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' and Bill_Category_Type_ID='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        //}
                        //else
                        //{ 
                        if (Commodity == "33" || Commodity == "63" || Commodity == "64" || Commodity == "92")
                        {
                            query = "select distinct Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_For_Nafed as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null)";
                        }
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //if (Godown_Type == "SS" )
                        //{
                        //    query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' and Bill_Category_Type_ID='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        //}
                        //else
                        //{ 
                        else if(Commodity == "33" || Commodity == "" || Commodity == "0")
                        {
                            //query = "select distinct Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details_For_VC where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        }
                        else
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        }
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //}
                        //wrong
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='AD'";


                    }
                    else if (User_Type == "B" && Godown_Type == "MPWLC SS")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (Godown_Type == "MPWLC SS")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020' and Bill_Category_Type_ID='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020' and Bill_Category_Type_ID='6'";
                            //query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Category_Type_ID in ('6','7')";
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_SSSB as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Category_Type_ID in ('6','7')";

                        }
                        //wrong
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='AD'";


                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (Godown_Type == "SS")
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        }
                        else
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG' or Bill_Type='PM')";

                        }

                    }
                    else if (User_Type == "B" && Godown_Type == "SS")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";
                        query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";

                    }
                    else if (User_Type == "B" && Godown_Type == "VPVT")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";
                        //query = "select distinct Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details  where Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_Details where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details where Godown_Id='" + BG_Id + "' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";
                        query = "select distinct Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details where Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null)";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_For_Vacant_Capacity as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null)";

                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2308003032" || BG_Id == "2312001018" || BG_Id == "2318001213" || BG_Id == "2320007125" || BG_Id == "232600420001" || BG_Id == "2327002091" || BG_Id == "2328002056" || BG_Id == "2329001084" || BG_Id == "234100301002")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        }
                        else
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";

                        }
                    }
                    else if (User_Type == "R" && Godown_Type == "SS")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' )";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I inner join tbl_Institution_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and SB.From_Date<'08/01/2020'";
                        //query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo as I inner join tbl_Institution_Steel_Silo_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='22' and I.Branch_Id='232700601'";
                        query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo as I inner join tbl_Institution_Steel_Silo_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number not in (select Ref_Bill_No from tbl_Digitally_Signed_Bill_RPO_SteelSilo as RPOS where RPOS.Branch_Id=I.Branch_Id)";


                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' )";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";


                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //Regular
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I inner join tbl_Institution_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and SB.From_Date<'08/01/2020'";

                        //July Break
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and I.Ref_Bill_No not in (select Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and MONTH=7 and Branch_Id='" + BG_Id + "')";

                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and Branch_id='" + BG_Id + "') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='' and Branch_Id='" + BG_Id + "')";



                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and Branch_id='" + BG_Id + "') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and Branch_Id='" + BG_Id + "')";



                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2')";




                    }
                }
                else if (Bill_Mode == "F3")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Fourth Repush
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F3' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F3')";




                    }
                }
                else if (Bill_Mode == "D01")
                {

                    if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select UPID AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark='D01')) and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='D1')";




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

    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_List_New(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Godown_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {
                        query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Summary where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_BO as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null)";


                    }
                    //else if (User_Type == "B" && Godown_Type == "PVT")
                    //{
                    //    //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "') and Bill_Type='GR'";
                    //    //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "') and Bill_Type='GR'";
                    //    //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and Bill_Type='GR'";
                    //    query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";

                    //}
                    //else if (User_Type == "G")
                    //{
                    //    string GetBranchId = Get_BranchId(BG_Id);
                    //    if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755" || BG_Id == "2329003099" || BG_Id == "2333002082" || BG_Id == "2310001036" || BG_Id == "2311003146" || BG_Id == "2330001045" || BG_Id == "2327003092")
                    //    {
                    //        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                    //        query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                    //    }
                    //    else
                    //    {
                    //        query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                    //    }
                    //}
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Bill_Number in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Bill_Number in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and I.Bill_Number not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null and DSC_Serial_No is not null)";
                        query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Bill_Number in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and I.Bill_Number not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null and DSC_Serial_No is not null and DSC_Serial_No!='')";


                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //Regular
                        //query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Bill_Number in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I inner join tbl_Institution_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RPO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Bill_Number in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and SB.From_Date>='08/01/2020'";

                        //July Break
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and I.Ref_Bill_No not in (select Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and MONTH=7 and Branch_Id='" + BG_Id + "')";

                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";



                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";



                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2')";




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
    [WebMethod(Description = "This Method Is Used to Get Branch List")]
    public DataSet Get_Branch_List(string BG_Id, string User_Type, string Credential)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv30")
            {
                //if (Commodity == "22")
                //{
                if (User_Type == "R" || User_Type == "M")
                {
                    //query = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + BG_Id + "') and BranchId in (select distinct BranchID from tbl_GdwnRentBill_Detuction_RM) order by DepotName";
                    //query = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + BG_Id + "') and BranchId in (select distinct BranchID from tbl_GdwnRentBill_Detuction_RM) order by DepotName";
                    query = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + BG_Id + "') order by DepotName";
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
    public string Get_Godown_Hired_Type(string Bill_NO)
    {
        string Bill_Number = Bill_NO;
        string Hired_Type = "";
        string query = "";
        try
        {
            query = " select G.Hired_Type from tbl_MetaData_GODOWN_2018 as G inner join tbl_Institution_Storage_Bill_Details as I on I.Godown_Id=G.Godown_ID where I.Bill_Number='" + Bill_Number + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Hired_Type = ds.Tables[0].Rows[0]["Hired_Type"].ToString();
            }
            else
            {
                Hired_Type = "";
            }

            return Hired_Type;
        }

        catch (Exception)
        {

            throw;

        }
    }
    public string Get_Godown_Hired_Type_SS(string Bill_NO)
    {
        string Bill_Number = Bill_NO;
        string Hired_Type = "";
        string query = "";
        try
        {
            query = " select G.Hired_Type from tbl_MetaData_GODOWN_2018 as G inner join tbl_Institution_Steel_Silo_Storage_Bill_Details as I on I.Godown_Id=G.Godown_ID where I.Bill_Number='" + Bill_Number + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Hired_Type = ds.Tables[0].Rows[0]["Hired_Type"].ToString();
            }
            else
            {
                Hired_Type = "";
            }

            return Hired_Type;
        }

        catch (Exception)
        {

            throw;

        }
    }
    public string Get_Bill_Count_Daily(string Bill_NO)
    {
        string Bill_Number = Bill_NO;
        int BillCount = 0;
        string Is_Valid = "N";
        string query = "";
        try
        {
            //query = " select G.Hired_Type from tbl_MetaData_GODOWN_2018 as G inner join tbl_Institution_Storage_Bill_Details as I on I.Godown_Id=G.Godown_ID where I.Bill_Number='" + Bill_Number + "'";
            query = "SELECT COUNT(Bill_Number) as Bill_Count FROM tbl_Bill_Institution_Daily_Charges where Bill_Number='" + Bill_Number + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                BillCount = Convert.ToInt32(ds.Tables[0].Rows[0]["Bill_Count"].ToString());
            }
            else
            {
                BillCount = 0;
            }
            if (BillCount <= 31)
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
    public DataSet Retrive_MPSCSC_Bill_Detail(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            string BillType = Bill_Type;
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && BillType == "MPWLC" && Commodity != "0")
                    {
                        string IsBillCountsValid = Get_Bill_Count_Daily(Bill_No);
                        if (IsBillCountsValid == "Y")
                        {
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                        }

                    }
                   else if (User_Type == "B" && BillType == "MPWLC" && Commodity=="0")
                    {
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details_For_VC.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details_For_VC.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details_For_VC.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details_For_VC.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Storage_Bill_Details_For_VC as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details_For_VC where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details_For_VC.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details_For_VC.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details_For_VC.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details_For_VC.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details_For_VC.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details_For_VC where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details_For_VC.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                    }
                    else if (User_Type == "B" && BillType == "MPWLC SS")
                    {
                        string IsBillCountsValid = Get_Bill_Count_Daily(Bill_No);
                        if (IsBillCountsValid == "Y")
                        {
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Steel_Silo_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                        }

                    }
                    else if (User_Type == "B" && BillType == "MPWLC SSPVT")
                    {
                        string IsBillCountsValid = Get_Bill_Count_Daily(Bill_No);
                        if (IsBillCountsValid == "Y")
                        {
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Steel_Silo_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                        }

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        string IsBillCountsValid = Get_Bill_Count_Daily(Bill_No);
                        if (IsBillCountsValid == "Y")
                        {
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                        }

                    }
                    else if (User_Type == "B" && BillType == "VPVT")
                    {
                        //string IsBillCountsValid = Get_Bill_Count_Daily(Bill_No);
                        //if (IsBillCountsValid == "Y")
                        //{
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Vacant_Capacity_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Vacant_Capacity_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id)+'('+tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Vacant_Capacity_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id and tbl_Institution_Vacant_Capacity_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Vacant_Capacity_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Vacant_Capacity_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                        //}

                    }
                    else if (User_Type == "R" && BillType == "RMPVT")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        string Hired_Types = Get_Godown_Hired_Type_SS(Bill_No);
                        if (Hired_Types == "Steel Silo")
                        {
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        }
                        else
                        {
                            query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                        }


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
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

    [WebMethod(Description = "This Method Is Used to Get Bill Details For NAFED")]
    public DataSet Retrive_MPSCSC_Bill_Detail_For_Nafed(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            string BillType = Bill_Type;
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && BillType == "MPWLC")
                    {

                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Storage_Bill_Details.Godown_Id)+'('+tbl_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                    }

                    else if (User_Type == "R" && BillType == "RMPVT")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                    }

                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                    }
                    //}
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


    [WebMethod(Description = "This Method Is Used to Get WHR List")]
    public DataSet Retrive_MPSCSC_Bill_Detail_New(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            string BillType = Bill_Type;
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    //if (Commodity == "22")
                    //{
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Summary.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Summary.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Summary.Godown_Id)+'('+tbl_Institution_Storage_Bill_Summary.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Summary where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Summary.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Summary.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Summary.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,'' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Summary where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Summary.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "B" && BillType == "PVT")
                    {
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and tbl_Institution_Storage_Bill_Details.Bill_Number='" + Bill_No + "' and (tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type!='Hired')) or tbl_Institution_Storage_Bill_Details.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id and GG.Hired_Type='Hired')))";

                    }
                    else if (User_Type == "R")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";

                        }
                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "SELECT [Depositor_WHR_Id] from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID='" + BG_Id + "' and WHR.CropYear='2019-20' and WHR.Arrival_Source='01' and Commodity_Id='" + Commodity + "' and Gid is not null and [Depositor_WHR_Id] not in (select DSC.Depositor_WHR_Id from tbl_Digitally_Signed_WHR_Wheat2019 as DSC where DSC.BranchID='" + GetBranchId + "' and DSC.Commodity_Id='" + Commodity + "' and DSC.DSC_User_Type='G') order by [WHR_Issue_Date] desc";
                        //query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and tbl_Institution_Storage_Bill_Details.Godown_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "'";


                    }
                    else if (User_Type == "M")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select SB.Bill_Number,SB.Commodity_Rate as Rate_PM,SB.Per_Day_Rate,CONVERT(decimal(18,0),SB.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),SB.Sub_Amount) as Sub_Amount,SB.Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),SB.Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year, SB.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+ '('+SB.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),BDSC.Sub_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
                        else
                        {
                            //query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Prev_Bill_No=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";

                        }
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

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_Bill_Data(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string B_Type, string Bill_Mode)
    {
        // string BillNo = PBillNo;
        string G_Bill_No = "";
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        //string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type = "";
        string Bill_Type_SubStr2 = "";
        int CountBillChar = 0;
        CountBillChar = BillNo.Count();
        if (CountBillChar >= 20)
        {
            Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        }
        else
        {
            Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        }
        //string Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        //string Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        string DDL_Bill_Type = B_Type;
        string Arr_Bill_Type = B_Type;
        string Godown_Type = "";
        Bill_Type = B_Type;

        //if (Bill_Type_SubStr == "23")
        //{
        //    Godown_Type = "GR";
        //}
        if (Bill_Type_SubStr2 == "2")
        {
            Godown_Type = "81";
        }
        else if (Bill_Type_SubStr2 == "3")
        {
            Godown_Type = "83";
        }
        else if (Bill_Type_SubStr2 == "1")
        {
            Godown_Type = "91";
        }
        else if (Bill_Type_SubStr2 == "4")
        {
            Godown_Type = "84";
        }
        else if (Bill_Type_SubStr2 == "6")
        {
            Godown_Type = "86";
        }
        else if (Bill_Type_SubStr2 == "5")
        {
            Godown_Type = "81";
        }
        else if (Bill_Type_SubStr2 == "7")
        {
            Godown_Type = "81";
        }
        string MPWLCParty = "Madhya Pradesh Warehousing and Logistics Corporation";
        string PVTParty = "";

        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //For Failed Payment
                    if (Bill_Mode == "F")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F1";

                    }
                    else if (Bill_Mode == "F2")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F2";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F3";

                    }
                }

                //Generate CSum
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date],[Created_By],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Serial_No + "','" + LocalIP + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //Actual
                string query = "";
                if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && Godown_Type == "91")
                {
                    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    }
                }
                else if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && (Godown_Type == "81" || Godown_Type == "83" || Godown_Type == "84" || Godown_Type == "86"))
                {
                    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    }
                }
                else if (DSC_User_Type == "B" && Bill_Type == "VPVT")
                {
                    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_For_Vacant_Capacity]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    }
                }
                else if (DSC_User_Type == "B" && Bill_Type == "PVT")
                {
                    //Commented for Stop Rent Bill Sign by Branch 18/02/2022
                    //Commented for open Rent Bill Sign by Branch 01/07/2022
                    //savan

                    if (Bill_Mode == "F")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    else
                    {
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    }

                    //joshi

                }
                else if (DSC_User_Type == "G")
                {
                    //Commented for Stop Rent Bill Sign GM 11/12/2021
                    //Commented for Stop Rent Bill Sign GM 28/02/2022
                    if (Bill_Mode == "F")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F3")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    }
                }
                //Test
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //    SqlCommand cmd2 = new SqlCommand(query2, con);
                    //    con.Open();
                    //    int b = cmd2.ExecuteNonQuery();
                    //    con.Close();
                    //    if (b > 0)
                    //    {
                    //        string XMLData = DSString;
                    //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File]) VALUES ('" + WHRID + "','" + XMLData + "')";
                    //        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //        SqlCommand cmd3 = new SqlCommand(query3, con);
                    //        con.Open();
                    //        int c = cmd3.ExecuteNonQuery();
                    //        con.Close();
                    //        if (c > 0)
                    //        {
                    Is_SuccessInsert = "Y";
                    //        }

                    //    }
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
    public string Insert_Bill_Data_SteelSilo(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string B_Type, string Bill_Mode)
    {
        // string BillNo = PBillNo;
        string G_Bill_No = "";
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        //string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type = "";
        string Bill_Type_SubStr2 = "";
        int CountBillChar = 0;
        CountBillChar = BillNo.Count();
        if (CountBillChar >= 20)
        {
            Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        }
        else
        {
            Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        }
        //string Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        //string Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        string DDL_Bill_Type = B_Type;
        string Arr_Bill_Type = B_Type;
        string Godown_Type = "";
        Bill_Type = B_Type;

        //if (Bill_Type_SubStr == "23")
        //{
        //    Godown_Type = "GR";
        //}
        if (Bill_Type_SubStr2 == "2")
        {
            Godown_Type = "81";
        }
        else if (Bill_Type_SubStr2 == "3")
        {
            Godown_Type = "83";
        }
        else if (Bill_Type_SubStr2 == "1")
        {
            Godown_Type = "91";
        }
        else if (Bill_Type_SubStr2 == "4")
        {
            Godown_Type = "84";
        }
        else if (Bill_Type_SubStr2 == "6")
        {
            Godown_Type = "86";
        }
        else if (Bill_Type_SubStr2 == "5")
        {
            Godown_Type = "81";
        }
        string MPWLCParty = "Madhya Pradesh Warehousing and Logistics Corporation";
        string PVTParty = "";

        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //For Failed Payment
                    if (Bill_Mode == "F")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F1";

                    }
                    else if (Bill_Mode == "F2")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F2";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F3";

                    }
                }

                //Generate CSum
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date],[Created_By],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Serial_No + "','" + LocalIP + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //Actual
                string query = "";
                //if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && Godown_Type == "91")
                if (DSC_User_Type == "B" && Bill_Type == "MPWLC")
                {
                    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    {
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_SSSB]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";

                    }
                }
                //else if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && (Godown_Type == "81" || Godown_Type == "83" || Godown_Type == "84" || Godown_Type == "86"))
                //{
                //    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //    {
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                //    }
                //}
                else if (DSC_User_Type == "B" && Bill_Type == "PVT")
                {
                    //if (Bill_Mode == "F")
                    if (Bill_Mode == "N")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_SteelSilo]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_SteelSilo]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    //else if (Bill_Mode == "F2")
                    //{
                    //    string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                    //    //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";

                    //}
                    //else if (Bill_Mode == "F3")
                    //{
                    //    string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                    //    //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";

                    //}
                    //else
                    //{
                    //    //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    //}

                }
                else if (DSC_User_Type == "G")
                {
                    if (Bill_Mode == "F")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_SteelSilo]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    else if (Bill_Mode == "F2")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F3")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_SteelSilo]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    }
                }
                //Test
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //    SqlCommand cmd2 = new SqlCommand(query2, con);
                    //    con.Open();
                    //    int b = cmd2.ExecuteNonQuery();
                    //    con.Close();
                    //    if (b > 0)
                    //    {
                    //        string XMLData = DSString;
                    //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File]) VALUES ('" + WHRID + "','" + XMLData + "')";
                    //        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //        SqlCommand cmd3 = new SqlCommand(query3, con);
                    //        con.Open();
                    //        int c = cmd3.ExecuteNonQuery();
                    //        con.Close();
                    //        if (c > 0)
                    //        {
                    Is_SuccessInsert = "Y";
                    //        }

                    //    }
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
    public string Insert_Bill_Data_New(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string B_Type, string Bill_Mode)
    {
        //string BillNo = PBillNo;
        string G_Bill_No = "";
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type = "";
        //string Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        //string Bill_Type_SubStr2 = BillNo.Substring(1, 1);
        string Bill_Type_SubStr2 = BillNo.Substring(3, 1);
        string DDL_Bill_Type = B_Type;
        string Arr_Bill_Type = B_Type;
        string Godown_Type = "";
        Bill_Type = B_Type;

        //if (Bill_Type_SubStr == "23")
        //{
        //    Godown_Type = "GR";
        //}
        if (Bill_Type_SubStr2 == "2")
        {
            Godown_Type = "2";
        }
        else if (Bill_Type_SubStr2 == "3")
        {
            Godown_Type = "3";
        }
        else if (Bill_Type_SubStr2 == "1")
        {
            Godown_Type = "1";
        }
        else if (Bill_Type_SubStr2 == "4")
        {
            Godown_Type = "4";
        }
        else if (Bill_Type_SubStr2 == "5")
        {
            Godown_Type = "5";
        }
        else if (Bill_Type_SubStr2 == "6")
        {
            Godown_Type = "6";
        }
        string MPWLCParty = "Madhya Pradesh Warehousing and Logistics Corporation";
        string PVTParty = "";

        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //For Failed Payment
                    if (Bill_Mode == "F")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F1";

                    }
                    else if (Bill_Mode == "F2")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F2";

                    }
                }

                //Generate CSum
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;

                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Generate CSum
                //string query = "INSERT INTO [tbl_Digitally_Signed_WHR_Details]([Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],[WHR_Issue_Date],[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],CreatedDate,CreatedBy,DSC_Serial_No,DSC_Holder_Name,Client_Ip,DSC_User_Type) values ('" + Depositor_WHR_Id + "','" + Depositor_Form_No + "','" + District_Id + "','" + Commodity_Id + "','" + Grade + "','" + Depositor_Name + "','" + getDate_MDY(Date_of_Deposit.Trim().ToString()) + "','" + TotalBags_Received + "','" + Total_Qty_Received + "','" + AvgMoisture_Content + "','" + AvgMoisture_Content_To + "','" + SangrahadDate + "','" + MktValue_of_Commodity + "','" + getDate_MDY(WHR_Issue_Date.Trim().ToString()) + "','" + WHR_CreatedDate + "','" + WHR_Client_IP + "','" + CropYear + "','" + Remark + "','" + BranchID + "','" + DepositorID + "','" + GodownID + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date],[Created_By],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Serial_No + "','" + LocalIP + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //Actual
                string query = "";
                //if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && Godown_Type == "91")
                //{
                //    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //    {
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                //    }
                //}
                //else if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && (Godown_Type == "81" || Godown_Type == "83" || Godown_Type == "84" || Godown_Type == "86"))
                //{
                //    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //    {
                //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_BO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_BO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";

                //    }
                //}
                //else if (DSC_User_Type == "B" && Bill_Type == "PVT")
                //{
                //    if (Bill_Mode == "F")
                //    {
                //        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                //        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                //    }
                //    else if (Bill_Mode == "F2")
                //    {
                //        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                //        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";

                //    }
                //    else
                //    {
                //        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //    }
                //}
                //else if (DSC_User_Type == "G")
                //{
                //    if (Bill_Mode == "F")
                //    {
                //        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                //        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                //        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                //    }
                //    else if (Bill_Mode == "F2")
                //    {
                //        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";
                //    }
                //    else
                //    {
                //        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                //    }
                //}
                //Test
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";


                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //    SqlCommand cmd2 = new SqlCommand(query2, con);
                    //    con.Open();
                    //    int b = cmd2.ExecuteNonQuery();
                    //    con.Close();
                    //    if (b > 0)
                    //    {
                    //        string XMLData = DSString;
                    //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File]) VALUES ('" + WHRID + "','" + XMLData + "')";
                    //        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //        SqlCommand cmd3 = new SqlCommand(query3, con);
                    //        con.Open();
                    //        int c = cmd3.ExecuteNonQuery();
                    //        con.Close();
                    //        if (c > 0)
                    //        {
                    Is_SuccessInsert = "Y";
                    //        }

                    //    }
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
    public string Insert_Bill_Data_RO(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string AccountNo_G, string IFSC_G, decimal NetAmtMPWLC, decimal NetAmtGO, string MPWLC_Party, string Private_Party, decimal MPWLC_Amt, decimal TDS_Amt, decimal Deduction_Amt, string Bill_Mode)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Account_No_Godown_Owner = AccountNo_G;
        string IFSC_Code_Godown_Owner = IFSC_G;
        decimal PayMPWLC = NetAmtMPWLC;
        decimal PayGodownOwner = NetAmtGO;
        string Bill_Number_1 = "";
        string Bill_Number_2 = "";
        string MPWLCParty = MPWLC_Party;
        string PVTParty = Private_Party;
        decimal MPWLCAmt = MPWLC_Amt;
        decimal TDSAmt = TDS_Amt;
        decimal DeductionAmt = Deduction_Amt;


        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //Bill_Number_1 = "WS1"+Bill_Number;
                    //Bill_Number_2 = "PR2" + Bill_Number;
                    if (Bill_Mode == "F")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F1";
                        Bill_Number_2 = "81" + Bill_Number + "F1";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F2";
                        Bill_Number_2 = "81" + Bill_Number + "F2";
                    }
                    else if (Bill_Mode == "F3")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F3";
                        Bill_Number_2 = "81" + Bill_Number + "F3";
                    }
                    else
                    {
                        Bill_Number_1 = "91" + Bill_Number;
                        Bill_Number_2 = "81" + Bill_Number;
                    }
                }
                //Generate CSum
                //Get RAM MPWLC
                Get_RAM_DSC(Bill_Number_1);
                //
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM + MPWLCAmt + TDSAmt + DeductionAmt;
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + T_MPWLC_Part + T_TDS + T_TDeduction + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM;

                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

                //Generate CSum for JV
                //Get RAM JVS
                Get_RAM_DSC(Bill_Number_2);
                //
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM + "0" + "0" + "0";
                string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + T_MPWLC_Part + T_TDS + T_TDeduction + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM;


                var sha1G = System.Security.Cryptography.SHA1.Create();
                byte[] bufG = System.Text.Encoding.UTF8.GetBytes(CheckSumStringG);
                byte[] hashG = sha1G.ComputeHash(bufG, 0, bufG.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrsG = System.BitConverter.ToString(hashG).Replace("-", "");
                //Generate CSum
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "" && Bill_Mode == null)
                if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "N")
                {
                    //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt ) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "','" + TDSAmt + "','" + DeductionAmt + "')";
                    //string query = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrs + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "',MPWLC_Amt='" + MPWLCAmt + "',TDS_Amt='" + TDSAmt + "',Deduction_Amt='" + DeductionAmt + "' where Bill_Number='" + Bill_Number_1 + "'";
                    string query = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrs + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_1 + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    if (a > 0)
                    {
                        //string query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "','" + 0 + "','" + 0 + "','" + 0 + "')";
                        //string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "',MPWLC_Amt='0',TDS_Amt='0',Deduction_Amt='0' where Bill_Number='" + Bill_Number_2 + "'";
                        string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_2 + "'";

                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        con.Open();
                        int b = cmd2.ExecuteNonQuery();
                        con.Close();
                        if (b > 0)
                        {
                            //string XMLData = DSString;
                            //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                            //SqlCommand cmd3 = new SqlCommand(query3, con);
                            //con.Open();
                            //int c = cmd3.ExecuteNonQuery();
                            //con.Close();
                            //if (c > 0)
                            //{
                            Is_SuccessInsert = "Y";
                            //}

                        }
                    }
                }
                else
                {
                    string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_2 + "'";

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        //string XMLData = DSString;
                        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        //SqlCommand cmd3 = new SqlCommand(query3, con);
                        //con.Open();
                        //int c = cmd3.ExecuteNonQuery();
                        //con.Close();
                        //if (c > 0)
                        //{
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
    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_Bill_Data_RO_New(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string AccountNo_G, string IFSC_G, decimal NetAmtMPWLC, decimal NetAmtGO, string MPWLC_Party, string Private_Party, decimal MPWLC_Amt, decimal TDS_Amt, decimal Deduction_Amt, string Bill_Mode)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Account_No_Godown_Owner = AccountNo_G;
        string IFSC_Code_Godown_Owner = IFSC_G;
        decimal PayMPWLC = NetAmtMPWLC;
        decimal PayGodownOwner = NetAmtGO;
        string Bill_Number_1 = "";
        string Bill_Number_2 = "";
        string MPWLCParty = MPWLC_Party;
        string PVTParty = Private_Party;
        decimal MPWLCAmt = MPWLC_Amt;
        decimal TDSAmt = TDS_Amt;
        decimal DeductionAmt = Deduction_Amt;


        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //Bill_Number_1 = "WS1"+Bill_Number;
                    //Bill_Number_2 = "PR2" + Bill_Number;
                    if (Bill_Mode == "F")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F1";
                        Bill_Number_2 = "81" + Bill_Number + "F1";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F2";
                        Bill_Number_2 = "81" + Bill_Number + "F2";
                    }
                    else
                    {
                        Bill_Number_1 = "91" + Bill_Number;
                        Bill_Number_2 = "81" + Bill_Number;
                    }
                }
                //Generate CSum
                //Get RAM MPWLC
                Get_RAM_DSC(Bill_Number_1);
                //
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM + MPWLCAmt + TDSAmt + DeductionAmt;
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + T_MPWLC_Part + T_TDS + T_TDeduction + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM;

                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

                //Generate CSum for JV
                //Get RAM JVS
                Get_RAM_DSC(Bill_Number_2);
                //
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM + "0" + "0" + "0";
                string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + T_MPWLC_Part + T_TDS + T_TDeduction + DSC_Serial_No_RAM + DSC_Holder_Name_RAM + Client_Ip_RAM + DSC_User_Type_RAM + CreatedDate_RAM + CreatedBy_RAM + Bill_Check_Sum_RAM;


                var sha1G = System.Security.Cryptography.SHA1.Create();
                byte[] bufG = System.Text.Encoding.UTF8.GetBytes(CheckSumStringG);
                byte[] hashG = sha1G.ComputeHash(bufG, 0, bufG.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrsG = System.BitConverter.ToString(hashG).Replace("-", "");
                //Generate CSum
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "" && Bill_Mode == null)
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "N")
                //{
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt ) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "','" + TDSAmt + "','" + DeductionAmt + "')";
                //string query = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrs + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "',MPWLC_Amt='" + MPWLCAmt + "',TDS_Amt='" + TDSAmt + "',Deduction_Amt='" + DeductionAmt + "' where Bill_Number='" + Bill_Number_1 + "'";
                //string query = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrs + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_1 + "'";
                string query = "update tbl_Digitally_Signed_Bill_RPO set WHR_Check_Sum = '" + hashstrsG + "', CreatedDate = GETDATE(), CreatedBy = '" + IpAdd + "', DSC_Serial_No = '" + Serial_No + "', DSC_Holder_Name = '" + DSC_Holder + "', Client_Ip = '" + LocalIP + "', DSC_User_Type = '" + DSC_User_Type + "' where Bill_Number = '" + Bill_Number_2 + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "','" + 0 + "','" + 0 + "','" + 0 + "')";
                    ////string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "',MPWLC_Amt='0',TDS_Amt='0',Deduction_Amt='0' where Bill_Number='" + Bill_Number_2 + "'";
                    //string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_2 + "'";

                    //SqlCommand cmd2 = new SqlCommand(query2, con);
                    //con.Open();
                    //int b = cmd2.ExecuteNonQuery();
                    //con.Close();
                    //if (b > 0)
                    //{
                    //string XMLData = DSString;
                    //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //SqlCommand cmd3 = new SqlCommand(query3, con);
                    //con.Open();
                    //int c = cmd3.ExecuteNonQuery();
                    //con.Close();
                    //if (c > 0)
                    //{
                    Is_SuccessInsert = "Y";
                    //}

                    //}
                }
                //}
                //else
                //{
                //    string query2 = "update tbl_Digitally_Signed_Bill_RO set WHR_Check_Sum='" + hashstrsG + "',CreatedDate=GETDATE(),CreatedBy='" + IpAdd + "',DSC_Serial_No='" + Serial_No + "',DSC_Holder_Name='" + DSC_Holder + "',Client_Ip='" + LocalIP + "',DSC_User_Type='" + DSC_User_Type + "' where Bill_Number='" + Bill_Number_2 + "'";

                //    SqlCommand cmd2 = new SqlCommand(query2, con);
                //    con.Open();
                //    int b = cmd2.ExecuteNonQuery();
                //    con.Close();
                //    if (b > 0)
                //    {
                //        //string XMLData = DSString;
                //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                //        //SqlCommand cmd3 = new SqlCommand(query3, con);
                //        //con.Open();
                //        //int c = cmd3.ExecuteNonQuery();
                //        //con.Close();
                //        //if (c > 0)
                //        //{
                //        Is_SuccessInsert = "Y";
                //    }
                //}
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }
    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_Bill_Data_RAM(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string AccountNo_G, string IFSC_G, decimal NetAmtMPWLC, decimal NetAmtGO, string MPWLC_Party, string Private_Party, decimal MPWLC_Amt, decimal TDS_Amt, decimal Deduction_Amt, string Bill_Mode)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Account_No_Godown_Owner = AccountNo_G;
        string IFSC_Code_Godown_Owner = IFSC_G;
        decimal PayMPWLC = NetAmtMPWLC;
        decimal PayGodownOwner = NetAmtGO;
        string Bill_Number_1 = "";
        string Bill_Number_2 = "";
        string MPWLCParty = MPWLC_Party;
        string PVTParty = Private_Party;
        decimal MPWLCAmt = MPWLC_Amt;
        decimal TDSAmt = TDS_Amt;
        decimal DeductionAmt = Deduction_Amt;
        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //Bill_Number_1 = "WS1"+Bill_Number;
                    //Bill_Number_2 = "PR2" + Bill_Number;
                    if (Bill_Mode == "F")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F1";
                        Bill_Number_2 = "81" + Bill_Number + "F1";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F2";
                        Bill_Number_2 = "81" + Bill_Number + "F2";
                    }
                    else if (Bill_Mode == "F3")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F3";
                        Bill_Number_2 = "81" + Bill_Number + "F3";
                    }
                    //else if (Bill_Mode == "D01")
                    //{
                    //    Bill_Number_1 = "91" + Bill_Number + "D1";
                    //    Bill_Number_2 = "81" + Bill_Number + "D1";
                    //}
                    else
                    {
                        Bill_Number_1 = "91" + Bill_Number;
                        Bill_Number_2 = "81" + Bill_Number;
                    }
                }
                //Generate CSum
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/12/2019 on 7 PM
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/02/2020 on 7 PM
                string CheckSumString = Bill_Number_1 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;




                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

                //Generate CSum for JV
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE 11/12/2019 on 7 PM
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE  11/02/2020 on 7 PM
                string CheckSumStringG = Bill_Number_2 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';

                var sha1G = System.Security.Cryptography.SHA1.Create();
                byte[] bufG = System.Text.Encoding.UTF8.GetBytes(CheckSumStringG);
                byte[] hashG = sha1G.ComputeHash(bufG, 0, bufG.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrsG = System.BitConverter.ToString(hashG).Replace("-", "");
                //Generate CSum
                string query = "";
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "" && Bill_Mode == null)
                if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "N")
                {
                    //if (Bill_Mode == "F")
                    //{
                    //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','F1')";
                    //}
                    //else
                    //{
                    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "')";
                    //}

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    if (a > 0)
                    {
                        string query2 = "";
                        if (Bill_Mode == "F")
                        {
                            query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                        }
                        else if (Bill_Mode == "F2")
                        {
                            //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                            query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                        }
                        else if (Bill_Mode == "F3")
                        {
                            //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                            query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F3')";

                        }
                        //else if (Bill_Mode == "D01")
                        //{
                        //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','D1')";

                        //}
                        else
                        {
                            query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                        }
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        con.Open();
                        int b = cmd2.ExecuteNonQuery();
                        con.Close();
                        if (b > 0)
                        {
                            //string XMLData = DSString;
                            //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                            //SqlCommand cmd3 = new SqlCommand(query3, con);
                            //con.Open();
                            //int c = cmd3.ExecuteNonQuery();
                            //con.Close();
                            //if (c > 0)
                            //{
                            Is_SuccessInsert = "Y";
                            //}

                        }
                    }
                }
                else
                {
                    string query2 = "";
                    if (Bill_Mode == "F")
                    {
                        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F3')";

                    }
                    //else if (Bill_Mode == "D01")
                    //{
                    //    //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','D1')";

                    //}
                    else
                    {
                        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                    }
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    con.Open();
                    int b = cmd2.ExecuteNonQuery();
                    con.Close();
                    if (b > 0)
                    {
                        //string XMLData = DSString;
                        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                        //SqlCommand cmd3 = new SqlCommand(query3, con);
                        //con.Open();
                        //int c = cmd3.ExecuteNonQuery();
                        //con.Close();
                        //if (c > 0)
                        //{
                        Is_SuccessInsert = "Y";
                        //}

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
    public string Insert_Bill_Data_RAM_New(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string AccountNo_G, string IFSC_G, decimal NetAmtMPWLC, decimal NetAmtGO, string MPWLC_Party, string Private_Party, decimal MPWLC_Amt, decimal TDS_Amt, decimal Deduction_Amt, string Bill_Mode)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Account_No_Godown_Owner = AccountNo_G;
        string IFSC_Code_Godown_Owner = IFSC_G;
        decimal PayMPWLC = NetAmtMPWLC;
        decimal PayGodownOwner = NetAmtGO;
        string Bill_Number_1 = "";
        string Bill_Number_2 = "";
        string MPWLCParty = MPWLC_Party;
        string PVTParty = Private_Party;
        decimal MPWLCAmt = MPWLC_Amt;
        decimal TDSAmt = TDS_Amt;
        decimal DeductionAmt = Deduction_Amt;
        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //Bill_Number_1 = "WS1"+Bill_Number;
                    //Bill_Number_2 = "PR2" + Bill_Number;
                    if (Bill_Mode == "F")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F1";
                        Bill_Number_2 = "81" + Bill_Number + "F1";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F2";
                        Bill_Number_2 = "81" + Bill_Number + "F2";
                    }
                    else
                    {
                        Bill_Number_1 = "91" + Bill_Number;
                        Bill_Number_2 = "81" + Bill_Number;
                    }
                }
                //Generate CSum
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/12/2019 on 7 PM
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/02/2020 on 7 PM
                string CheckSumString = Bill_Number_1 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;




                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

                //Generate CSum for JV
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE 11/12/2019 on 7 PM
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE  11/02/2020 on 7 PM
                string CheckSumStringG = Bill_Number_2 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';

                var sha1G = System.Security.Cryptography.SHA1.Create();
                byte[] bufG = System.Text.Encoding.UTF8.GetBytes(CheckSumStringG);
                byte[] hashG = sha1G.ComputeHash(bufG, 0, bufG.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrsG = System.BitConverter.ToString(hashG).Replace("-", "");
                //Generate CSum
                string query = "";
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "" && Bill_Mode == null)
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "N")
                //{
                //if (Bill_Mode == "F")
                //{
                //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','F1')";
                //}
                //else
                //{
                //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "')";
                //query = "INSERT INTO[Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RPO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values('" + Bill_Number_2 + "', '" + District_Id + "', '" + Branch_Id + "', '" + Depositor_Id + "', '" + Commodity_Id + "', '" + Financial_Year + "', '" + Per_Month_Rate + "', '" + Per_Day_Rate + "', '" + PayGodownOwner + "', '" + Sub_Amount + "', '" + GST_Perc + "', '" + GST_Amt + "', '" + Created_Date + "', '" + Created_By + "', '" + Month_No + "', '" + Godown_Id + "', '" + Crop_Year + "', '" + Account_No_Godown_Owner + "', '" + IFSC_Code_Godown_Owner + "', '', '', '', '', '', '', '', '" + Bill_Number + "', '" + PVTParty + "', '" + Godown_Id + "', 0, 0, 0, '" + Serial_No + "', '" + DSC_Holder + "', '" + LocalIP + "', '" + DSC_User_Type + "', '" + hashstrsG + "', GETDATE(), '" + IpAdd + "')";
                query = "INSERT INTO[Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RPO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values('" + Bill_Number_2 + "', '" + District_Id + "', '" + Branch_Id + "', '" + Depositor_Id + "', '" + Commodity_Id + "', '" + Financial_Year + "', '" + Per_Month_Rate + "', '" + Per_Day_Rate + "', '" + PayGodownOwner + "', '" + Sub_Amount + "', '" + GST_Perc + "', '" + GST_Amt + "', '" + Created_Date + "', '" + Created_By + "', '" + Month_No + "', '" + Godown_Id + "', '" + Crop_Year + "', '" + Account_No_Godown_Owner + "', '" + IFSC_Code_Godown_Owner + "', '', '', '', '', '', '', '', '" + Bill_Number + "', '" + PVTParty + "', '" + Godown_Id + "', '" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ", '" + Serial_No + "', '" + DSC_Holder + "', '" + LocalIP + "', '" + DSC_User_Type + "', '" + hashstrsG + "', GETDATE(), '" + IpAdd + "')";

                //}

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "";
                    //if (Bill_Mode == "F")
                    //{
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    //}
                    //else if (Bill_Mode == "F2")
                    //{
                    //    //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                    //}
                    //else
                    //{
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                    //}
                    //SqlCommand cmd2 = new SqlCommand(query2, con);
                    //con.Open();
                    //int b = cmd2.ExecuteNonQuery();
                    //con.Close();
                    //if (b > 0)
                    //{
                    //string XMLData = DSString;
                    //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //SqlCommand cmd3 = new SqlCommand(query3, con);
                    //con.Open();
                    //int c = cmd3.ExecuteNonQuery();
                    //con.Close();
                    //if (c > 0)
                    //{
                    Is_SuccessInsert = "Y";
                    //}

                }
                //}
                //}
                //else
                //{
                //    string query2 = "";
                //    if (Bill_Mode == "F")
                //    {
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                //    }
                //    else if (Bill_Mode == "F2")
                //    {
                //        //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                //    }
                //    else
                //    {
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                //    }
                //    SqlCommand cmd2 = new SqlCommand(query2, con);
                //    con.Open();
                //    int b = cmd2.ExecuteNonQuery();
                //    con.Close();
                //    if (b > 0)
                //    {
                //        //string XMLData = DSString;
                //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                //        //SqlCommand cmd3 = new SqlCommand(query3, con);
                //        //con.Open();
                //        //int c = cmd3.ExecuteNonQuery();
                //        //con.Close();
                //        //if (c > 0)
                //        //{
                //        Is_SuccessInsert = "Y";
                //        //}

                //    }

                //}
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
            //query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE() AND User_Type='" + UType + "'";
            query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE()";

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
    public void Get_RAM_DSC(string Bill_No)
    {
        //string PartyType = Party_Type;
        string BillNo = Bill_No;
        DSC_Serial_No_RAM = "";
        DSC_Holder_Name_RAM = "";
        Client_Ip_RAM = "";
        DSC_User_Type_RAM = "";
        Bill_Check_Sum_RAM = "";
        CreatedDate_RAM = "";
        CreatedBy_RAM = "";
        T_MPWLC_Part = "";
        T_TDS = "";
        T_TDeduction = "";
        //string UType = UserType;
        string query = "";
        try
        {
            //query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE()";
            //query = " select DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM from tbl_Digitally_Signed_Bill_RO where Bill_Number='" + BillNo + "'";
            query = " select DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,MPWLC_Amt,TDS_Amt,Deduction_Amt from tbl_Digitally_Signed_Bill_RO where Bill_Number='" + BillNo + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                DSC_Serial_No_RAM = ds.Tables[0].Rows[0]["DSC_Serial_No_RAM"].ToString();
                DSC_Holder_Name_RAM = ds.Tables[0].Rows[0]["DSC_Holder_Name_RAM"].ToString();
                Client_Ip_RAM = ds.Tables[0].Rows[0]["Client_Ip_RAM"].ToString();
                DSC_User_Type_RAM = ds.Tables[0].Rows[0]["DSC_User_Type_RAM"].ToString();
                Bill_Check_Sum_RAM = ds.Tables[0].Rows[0]["Bill_Check_Sum_RAM"].ToString();
                CreatedDate_RAM = ds.Tables[0].Rows[0]["CreatedDate_RAM"].ToString();
                CreatedBy_RAM = ds.Tables[0].Rows[0]["CreatedBy_RAM"].ToString();
                T_MPWLC_Part = ds.Tables[0].Rows[0]["MPWLC_Amt"].ToString();
                T_TDS = ds.Tables[0].Rows[0]["TDS_Amt"].ToString();
                T_TDeduction = ds.Tables[0].Rows[0]["Deduction_Amt"].ToString();
            }
            else
            {
                DSC_Serial_No_RAM = "";
                DSC_Holder_Name_RAM = "";
                Client_Ip_RAM = "";
                DSC_User_Type_RAM = "";
                Bill_Check_Sum_RAM = "";
                CreatedDate_RAM = "";
                CreatedBy_RAM = "";
                T_MPWLC_Part = "";
                T_TDS = "";
                T_TDeduction = "";
            }

            //return Is_Valid;
        }

        catch (Exception)
        {

            throw;

        }
    }
    public string Get_Ref_Bill(string Rent_Bill_No)
    {
        string RENT_BILL = Rent_Bill_No;
        string Ref_Bill = "";
        string query = "";
        try
        {
            //query = "select * from [tbl_DSC_User_Upload_Detail] where SerialNumber='" + SerialNo + "' and Verification_Status='Approve' and NotAfter>=GETDATE() and NotBefore<=GETDATE()";
            query = "select Ref_Bill_No from tbl_Godown_Rent_Deduction_Amount where Bill_No='" + RENT_BILL + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Ref_Bill = ds.Tables[0].Rows[0]["Ref_Bill_No"].ToString();
            }
            else
            {
                Ref_Bill = "";
            }

            return Ref_Bill;
        }

        catch (Exception)
        {

            throw;

        }
    }
    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_Bill_Data_RM_SteeSilo(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string AccountNo_G, string IFSC_G, decimal NetAmtMPWLC, decimal NetAmtGO, string MPWLC_Party, string Private_Party, decimal MPWLC_Amt, decimal TDS_Amt, decimal Deduction_Amt, string Bill_Mode)
    {
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        string Account_No_Godown_Owner = AccountNo_G;
        string IFSC_Code_Godown_Owner = IFSC_G;
        decimal PayMPWLC = NetAmtMPWLC;
        decimal PayGodownOwner = NetAmtGO;
        string Bill_Number_1 = "";
        string Bill_Number_2 = "";
        string MPWLCParty = MPWLC_Party;
        string PVTParty = Private_Party;
        decimal MPWLCAmt = MPWLC_Amt;
        decimal TDSAmt = TDS_Amt;
        decimal DeductionAmt = Deduction_Amt;
        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //Bill_Number_1 = "WS1"+Bill_Number;
                    //Bill_Number_2 = "PR2" + Bill_Number;
                    if (Bill_Mode == "F")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F1";
                        Bill_Number_2 = "81" + Bill_Number + "F1";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        Bill_Number_1 = "91" + Bill_Number + "F2";
                        Bill_Number_2 = "81" + Bill_Number + "F2";
                    }
                    else
                    {
                        Bill_Number_1 = "91" + Bill_Number;
                        Bill_Number_2 = "81" + Bill_Number;
                    }
                }
                //Generate CSum
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/12/2019 on 7 PM
                //string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //DATE 11/02/2020 on 7 PM
                string CheckSumString = Bill_Number_1 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayMPWLC + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;




                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

                //Generate CSum for JV
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + MPWLCAmt + TDSAmt + DeductionAmt;
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE 11/12/2019 on 7 PM
                //string CheckSumStringG = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';
                //DATE  11/02/2020 on 7 PM
                string CheckSumStringG = Bill_Number_2 + Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + PayGodownOwner + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No_Godown_Owner + IFSC_Code_Godown_Owner + Serial_No + LocalIP + DSC_Holder + '0' + '0' + '0';

                var sha1G = System.Security.Cryptography.SHA1.Create();
                byte[] bufG = System.Text.Encoding.UTF8.GetBytes(CheckSumStringG);
                byte[] hashG = sha1G.ComputeHash(bufG, 0, bufG.Length);
                //var hashstr  = Convert.ToBase64String(hash);
                var hashstrsG = System.BitConverter.ToString(hashG).Replace("-", "");
                //Generate CSum
                string query = "";
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "" && Bill_Mode == null)
                //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343" && Bill_Mode == "N")
                //{
                //if (Bill_Mode == "F")
                //{
                //    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','F1')";
                //}
                //else
                //{
                //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM) values ('" + Bill_Number_1 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayMPWLC + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','','','','','','','','" + Bill_Number + "','" + MPWLCParty + "','9999','" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ",'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "')";
                //query = "INSERT INTO[Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RPO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values('" + Bill_Number_2 + "', '" + District_Id + "', '" + Branch_Id + "', '" + Depositor_Id + "', '" + Commodity_Id + "', '" + Financial_Year + "', '" + Per_Month_Rate + "', '" + Per_Day_Rate + "', '" + PayGodownOwner + "', '" + Sub_Amount + "', '" + GST_Perc + "', '" + GST_Amt + "', '" + Created_Date + "', '" + Created_By + "', '" + Month_No + "', '" + Godown_Id + "', '" + Crop_Year + "', '" + Account_No_Godown_Owner + "', '" + IFSC_Code_Godown_Owner + "', '', '', '', '', '', '', '', '" + Bill_Number + "', '" + PVTParty + "', '" + Godown_Id + "', 0, 0, 0, '" + Serial_No + "', '" + DSC_Holder + "', '" + LocalIP + "', '" + DSC_User_Type + "', '" + hashstrsG + "', GETDATE(), '" + IpAdd + "')";
                query = "INSERT INTO[Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RPO_SteelSilo]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values('" + Bill_Number_2 + "', '" + District_Id + "', '" + Branch_Id + "', '" + Depositor_Id + "', '" + Commodity_Id + "', '" + Financial_Year + "', '" + Per_Month_Rate + "', '" + Per_Day_Rate + "', '" + PayGodownOwner + "', '" + Sub_Amount + "', '" + GST_Perc + "', '" + GST_Amt + "', '" + Created_Date + "', '" + Created_By + "', '" + Month_No + "', '" + Godown_Id + "', '" + Crop_Year + "', '" + Account_No_Godown_Owner + "', '" + IFSC_Code_Godown_Owner + "', '', '', '', '', '', '', '', '" + Bill_Number + "', '" + PVTParty + "', '" + Godown_Id + "', '" + MPWLCAmt + "'," + TDSAmt + "," + DeductionAmt + ", '" + Serial_No + "', '" + DSC_Holder + "', '" + LocalIP + "', '" + DSC_User_Type + "', '" + hashstrsG + "', GETDATE(), '" + IpAdd + "')";

                //}

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a > 0)
                {
                    //string query2 = "";
                    //if (Bill_Mode == "F")
                    //{
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    //}
                    //else if (Bill_Mode == "F2")
                    //{
                    //    //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                    //}
                    //else
                    //{
                    //    query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                    //}
                    //SqlCommand cmd2 = new SqlCommand(query2, con);
                    //con.Open();
                    //int b = cmd2.ExecuteNonQuery();
                    //con.Close();
                    //if (b > 0)
                    //{
                    //string XMLData = DSString;
                    //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //SqlCommand cmd3 = new SqlCommand(query3, con);
                    //con.Open();
                    //int c = cmd3.ExecuteNonQuery();
                    //con.Close();
                    //if (c > 0)
                    //{
                    Is_SuccessInsert = "Y";
                    //}

                }
                //}
                //}
                //else
                //{
                //    string query2 = "";
                //    if (Bill_Mode == "F")
                //    {
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                //    }
                //    else if (Bill_Mode == "F2")
                //    {
                //        //query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F1')";
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM,Re_Push ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "','F2')";

                //    }
                //    else
                //    {
                //        query2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,WHR_Check_Sum,[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,MPWLC_Amt,TDS_Amt,Deduction_Amt,DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,DSC_User_Type_RAM,Bill_Check_Sum_RAM,CreatedDate_RAM,CreatedBy_RAM ) values ('" + Bill_Number_2 + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + PayGodownOwner + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No_Godown_Owner + "','" + IFSC_Code_Godown_Owner + "','','','','','','','','" + Bill_Number + "','" + PVTParty + "','" + Godown_Id + "',0,0,0,'" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + hashstrsG + "',GETDATE(),'" + IpAdd + "')";
                //    }
                //    SqlCommand cmd2 = new SqlCommand(query2, con);
                //    con.Open();
                //    int b = cmd2.ExecuteNonQuery();
                //    con.Close();
                //    if (b > 0)
                //    {
                //        //string XMLData = DSString;
                //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                //        //SqlCommand cmd3 = new SqlCommand(query3, con);
                //        //con.Open();
                //        //int c = cmd3.ExecuteNonQuery();
                //        //con.Close();
                //        //if (c > 0)
                //        //{
                //        Is_SuccessInsert = "Y";
                //        //}

                //    }

                //}
            }

        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }


    /************************************NAFED Bill MEthod**********************************************/
    [WebMethod(Description = "This Method Is Used to Get Bill List")]
    public DataSet Retrieve_Bill_List_NAFED(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Godown_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {
                        if (Commodity == "33" || Commodity == "63" || Commodity == "64" || Commodity == "92" || Commodity == "75")
                        {
                            query = "select distinct Bill_Number from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' AND From_Date>='2024-04-01' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_Nafed as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null)";
                        }
                        else
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        }
                    }
                    else if (User_Type == "B" && Godown_Type == "MPWLC SS")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (Godown_Type == "MPWLC SS")
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_SSSB as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Category_Type_ID in ('6','7')";

                        }
                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (Godown_Type == "SS")
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        }
                        else
                        {
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG' or Bill_Type='PM')";

                        }

                    }
                    else if (User_Type == "B" && Godown_Type == "SS")
                    {
                        //string GetBranchId = Get_BranchId(BG_Id);
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";
                        query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020' and Bill_Category_Type_ID!='6' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Branch_Id='" + BG_Id + "' and DSC_User_Type='B') and From_Date>='08/01/2020'";

                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2308003032" || BG_Id == "2312001018" || BG_Id == "2318001213" || BG_Id == "2320007125" || BG_Id == "232600420001" || BG_Id == "2327002091" || BG_Id == "2328002056" || BG_Id == "2329001084" || BG_Id == "234100301002")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details  where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date<'08/01/2020' union select distinct Bill_Number from tbl_Institution_Steel_Silo_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='SS' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and From_Date>='08/01/2020'";

                        }
                        else
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";

                        }
                    }
                    else if (User_Type == "R" && Godown_Type == "SS")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' )";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I inner join tbl_Institution_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and SB.From_Date<'08/01/2020'";
                        //query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo as I inner join tbl_Institution_Steel_Silo_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='22' and I.Branch_Id='232700601'";
                        query = "select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_SteelSilo as I inner join tbl_Institution_Steel_Silo_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Bill_Number not in (select Ref_Bill_No from tbl_Digitally_Signed_Bill_RPO_SteelSilo as RPOS where RPOS.Branch_Id=I.Branch_Id)";


                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' )";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details_NAFED as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + GetBranchId + "'";

                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //Regular
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I inner join tbl_Institution_Storage_Bill_Details as SB on SB.Bill_Number=I.Bill_Number where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and SB.From_Date<'08/01/2020'";

                        //July Break
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "') and I.Ref_Bill_No not in (select Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and MONTH=7 and Branch_Id='" + BG_Id + "')";

                    }
                    //}
                }
                else if (Bill_Mode == "F")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='GR' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G') and Bill_Number in (select Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='B')";
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and Branch_id='" + BG_Id + "') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='' and Branch_Id='" + BG_Id + "')";



                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No not in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "')";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00'))";
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and Branch_id='" + BG_Id + "') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and Branch_Id='" + BG_Id + "')";



                    }
                }
                else if (Bill_Mode == "F2")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Trird Repush
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2')";




                    }
                }
                else if (Bill_Mode == "F3")
                {
                    if (User_Type == "B" && Godown_Type == "MPWLC")
                    {

                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where Branch_Id='" + BG_Id + "' and I.Ref_Bill_No is not null) and Bill_Type='AD'";

                    }
                    else if (User_Type == "B" && Godown_Type == "PVT")
                    {
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number not in (select distinct I.Bill_Number as Bill_Number from tbl_Digitally_Signed_Bill_PVT as I where Branch_Id='" + BG_Id + "' and I.DSC_User_Type='B' and I.Bill_Number is not null) and (Bill_Type='GR' or Bill_Type='HG')";
                        //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')";
                        //Valid
                        //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='B')";
                        //Fourth Repush
                        query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and Bill_Number+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3' and DSC_User_Type='B')";



                    }
                    else if (User_Type == "G")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        if (BG_Id == "2328001045" || BG_Id == "2340002070" || BG_Id == "2326001025" || BG_Id == "2337003203" || BG_Id == "232900501090" || BG_Id == "2333002077" || BG_Id == "2340002051" || BG_Id == "2340002070" || BG_Id == "2311003129" || BG_Id == "2311003143" || BG_Id == "2325003023" || BG_Id == "2325003024" || BG_Id == "2305001755")
                        {
                            //query = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and Godown_Id='" + BG_Id + "' and Bill_Type='AD' and Bill_Number not in (select distinct Bill_Number from tbl_Digitally_Signed_Bill_PVT where Godown_Id='" + BG_Id + "' and DSC_User_Type='G')";
                            query = "select distinct I.Bill_Number as Bill_Number from tbl_Institution_Storage_Bill_Details as I  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and I.Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";

                        }
                        else
                        {
                            //query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1' and DSC_User_Type='G')";
                            query = "select distinct D.Bill_No as Bill_Number from tbl_Institution_Storage_Bill_Details as I inner join tbl_Godown_Rent_Deduction_Amount as D on D.Ref_Bill_No=I.Bill_Number  where Commodity_Id='" + Commodity + "' and I.Godown_Id='" + BG_Id + "' and Bill_Number+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00') and I.Bill_Number not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3' and DSC_User_Type='G')";


                        }
                    }
                    else if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1' and DSC_User_Type='')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F3' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F1') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F1')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F2' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F3') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F3')";




                    }
                }
                else if (Bill_Mode == "D01")
                {

                    if (User_Type == "R")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.DSC_Serial_No!='' ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2' and DSC_User_Type='')";




                    }
                    else if (User_Type == "M")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        //query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No+'F1' in (select RIGHT(UPID, LEN(UPID) - 2) AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark!='S00')) and I.Ref_Bill_No in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_PVT where Re_Push='F2') and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='F2')";
                        query = "select distinct I.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_Details as I where I.Commodity_Id='" + Commodity + "' and I.Branch_Id='" + BG_Id + "' and I.Ref_Bill_No in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.Branch_Id='" + BG_Id + "' and R.Ref_Bill_No is not null ) and I.Ref_Bill_No in (select distinct Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM where BranchID='" + BG_Id + "' and I.Ref_Bill_No in (select UPID AS Bill_Number from MPSCSC.dbo.PaymetResponseFromWS_Warehouse where Credit_Remark='D01')) and I.Ref_Bill_No not in (select distinct Ref_Bill_No from tbl_Digitally_Signed_Bill_RO where Re_Push='D1')";




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

    [WebMethod(Description = "This Method Is Used to Get Bill Details For NAFED")]
    public DataSet Retrieve_NAFED_Bill_Detail(string BG_Id, string District_Id, string User_Type, string Credential, string Commodity, string Bill_No, string Bill_Type, string Bill_Mode)
    {
        try
        {
            string query = "";
            string BillType = Bill_Type;
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Mode == "N")
                {
                    if (User_Type == "B" && BillType == "MPWLC")
                    {
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Net_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per,CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month ,tbl_Storage_Bill_Details.Month , 0 ) -1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Storage_Bill_Details.Godown_Id)+'('+tbl_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category,Bill_Type,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code from tbl_Storage_Bill_Details where Commodity_Id='" + Commodity + "' and tbl_Storage_Bill_Details.Branch_Id='" + BG_Id + "' and Bill_Number='" + Bill_No + "' AND tbl_Storage_Bill_Details.From_Date>='2024-04-01'";
                    }

                    else if (User_Type == "R" && BillType == "RMPVT")
                    {
                        string GetBranchId = Get_BranchId(BG_Id);
                        query = "select Bill_Number,Commodity_Rate as Rate_PM,Per_Day_Rate,CONVERT(decimal(18,0),Sub_Amount) as Net_Amount,CONVERT(decimal(18,0),Sub_Amount) as Sub_Amount,Service_Tax_Perc as GST_Per, CONVERT(decimal(18,0),Service_Tax_Amt) as GST_Amount,CONVERT(varchar(10),tbl_Institution_Steel_Silo_Storage_Bill_Details.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , tbl_Institution_Steel_Silo_Storage_Bill_Details.Month , 0 ) - 1 ) as Bill_Month,Crop_Year,Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id)+'('+tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id+')' as Godown,Depositor_Category ,GA.Acc_Holder_Name,GA.Account_No,GA.IFSC_Code,Bill_Type from tbl_Institution_Steel_Silo_Storage_Bill_Details inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=tbl_Institution_Steel_Silo_Storage_Bill_Details.Godown_Id and GA.RO_Approval_Status='Y' and GA.GO_Approval_Status='Y' where Commodity_Id='" + Commodity + "' and Bill_Number='" + Bill_No + "'";

                    }
                    else if (User_Type == "R")
                    {
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y' and SB.Bill_Number in (select distinct R.Ref_Bill_No as Bill_Number from tbl_Digitally_Signed_Bill_RO as R where R.DSC_Serial_No='' )";
                        }
                    }

                    else if (User_Type == "M")
                    {
                        string Hired_Types = Get_Godown_Hired_Type(Bill_No);
                        if (Hired_Types == "Hired")
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='B') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                        else
                        {
                            query = "select SB.Bill_Number,BDSC.Per_Month_Rate as Rate_PM,BDSC.Per_Day_Rate,CONVERT(decimal(18,0),BDSC.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),RM.Net_Amount) as Sub_Amount,BDSC.GST_Perc as GST_Per,CONVERT(decimal(18,0),BDSC.GST_Amt) as GST_Amount,CONVERT(varchar(10),SB.Created_Date,103) as Bill_Generated_Date,DateName( month , DateAdd( month , SB.Month , 0 ) - 1 ) as Bill_Month,BDSC.Crop_Year,BDSC.Financial_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=BDSC.Godown_Id)+ '('+BDSC.Godown_Id+')' as Godown,SB.Depositor_Category ,RM.JVS_Net_Amount as GodownerPay,(RM.Net_Amount-RM.JVS_Net_Amount) as MPWLCPay,GA.Acc_Holder_Name,PVT.Account_No,PVT.IFSC_Code,RM.JVS_Bill_Amount,RM.Res_Detuction_Amount,RM.TDS_Detuction_Amount,RM.Gain_Detuction_Amount,RM.Security_Detuction_Amt,RM.Security_Detuction_Amt,RM.Total_Detuction_Amt,RM.MPWLC_Amt,(select W.Acc_Holder_Name from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Acc_Holder_Name_M,(select W.Account_No from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as Account_No_M,(select W.IFSC_Code from tbl_Institution_Account_Details as W  where W.Institution='MPWLC') as IFSC_Code_M from tbl_Institution_Storage_Bill_Details as SB inner join tbl_GdwnRentBill_Detuction_RM as RM on RM.Ref_Bill_Number=SB.Bill_Number inner join tbl_Godown_Owner_Account_Details as GA on GA.Godown_Id=RM.GodownID and GA.RO_Approval_Status='Y' inner join tbl_Digitally_Signed_Bill_PVT as PVT on (PVT.Bill_Number=RM.JVS_Bill_Number and PVT.DSC_User_Type='G') inner join tbl_Digitally_Signed_Bill_Details as BDSC on BDSC.Ref_Bill_No=RM.Ref_Bill_Number where SB.Commodity_Id='" + Commodity + "' and SB.Branch_Id='" + BG_Id + "' and SB.Bill_Number='" + Bill_No + "' and SB.BO_Approval_Status='Y'";
                        }
                    }
                    //}
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

    [WebMethod(Description = "This Method Is Used to Insert DS WHR Data with XML")]
    public string Insert_Bill_Data_NAFED(string BillNo, DataSet ds, string IP, string Cred, string StrX, string Subject, string SerNo, string Client_IP, string User_Type, string AccountNo, string IFSC, string B_Type, string Bill_Mode)
    {
        string G_Bill_No = "";
        string DSC_User_Type = User_Type;
        string Is_SuccessInsert = "";
        string SN = SerNo;
        string Is_Verify = Is_Verified(SN, DSC_User_Type);
        string Credential = Cred;
        string Account_No = AccountNo;
        string IFSC_Code = IFSC;
        //string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type_SubStr = BillNo.Substring(0, 2);
        string Bill_Type = "";
        string Bill_Type_SubStr2 = "";
        int CountBillChar = 0;
        CountBillChar = BillNo.Count();
        if (CountBillChar >= 20)
        {
            Bill_Type_SubStr2 = BillNo.Substring(2, 1);
        }
        else
        {
            Bill_Type_SubStr2 = BillNo.Substring(4, 1);
        }
        string DDL_Bill_Type = B_Type;
        string Arr_Bill_Type = B_Type;
        string Godown_Type = "";
        Bill_Type = B_Type;

        //if (Bill_Type_SubStr == "23")
        //{
        //    Godown_Type = "GR";
        //}
        if (Bill_Type_SubStr2 == "2")
        {
            Godown_Type = "81";
        }
        else if (Bill_Type_SubStr2 == "3")
        {
            Godown_Type = "83";
        }
        else if (Bill_Type_SubStr2 == "1")
        {
            Godown_Type = "91";
        }
        else if (Bill_Type_SubStr2 == "4")
        {
            Godown_Type = "84";
        }
        else if (Bill_Type_SubStr2 == "6")
        {
            Godown_Type = "86";
        }
        else if (Bill_Type_SubStr2 == "5")
        {
            Godown_Type = "81";
        }
        else if (Bill_Type_SubStr2 == "7")
        {
            Godown_Type = "81";
        }
        string MPWLCParty = "Madhya Pradesh Warehousing and Logistics Corporation";
        string PVTParty = "";

        try
        {
            if (Credential == "WLC2019DSCNicv30" && Is_Verify == "Y" && Client_IP != "" && Client_IP != null && User_Type != "" && User_Type != null)
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
                string WHRID = BillNo;
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

                string Bill_Number = "";
                string District_Id = "";
                string Branch_Id = "";
                string Depositor_Id = "";
                string Commodity_Id = "";
                string Financial_Year = "";
                string Per_Month_Rate = "";
                string Per_Day_Rate = "";
                string Net_Amount = "";
                string Sub_Amount = "";
                string GST_Perc = "";
                string GST_Amt = "";
                string Created_Date = "";
                string Created_By = "";
                string Month_No = "";
                string Godown_Id = "";
                string Crop_Year = "";
                //string Depositor_Form_No = "";
                //string Grade = "";
                //string District_Id = "";
                //string AvgMoisture_Content_To = "";
                //string SangrahadDate = "";
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

                    Bill_Number = XMLds.Tables[12].Rows[0]["Bill_Number"].ToString();
                    District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    Branch_Id = XMLds.Tables[12].Rows[0]["Branch_Id"].ToString();
                    Depositor_Id = XMLds.Tables[12].Rows[0]["Depositor_Id"].ToString();
                    Commodity_Id = XMLds.Tables[12].Rows[0]["Commodity_Id"].ToString();
                    Financial_Year = XMLds.Tables[12].Rows[0]["Financial_Year"].ToString();
                    Per_Month_Rate = XMLds.Tables[12].Rows[0]["Commodity_Rate"].ToString();
                    Per_Day_Rate = XMLds.Tables[12].Rows[0]["Per_Day_Rate"].ToString();
                    Net_Amount = XMLds.Tables[12].Rows[0]["Net_Amount"].ToString();
                    Sub_Amount = XMLds.Tables[12].Rows[0]["Sub_Amount"].ToString();
                    GST_Perc = XMLds.Tables[12].Rows[0]["GST_Perc"].ToString();
                    GST_Amt = XMLds.Tables[12].Rows[0]["GST_Amt"].ToString();
                    Created_Date = XMLds.Tables[12].Rows[0]["Created_Date"].ToString();
                    Created_By = XMLds.Tables[12].Rows[0]["Created_By"].ToString();
                    Month_No = XMLds.Tables[12].Rows[0]["Month"].ToString();
                    Godown_Id = XMLds.Tables[12].Rows[0]["Godown_Id"].ToString();
                    Crop_Year = XMLds.Tables[12].Rows[0]["Crop_Year"].ToString();
                    //Depositor_Form_No = XMLds.Tables[12].Rows[0]["Depositor_Form_No"].ToString();
                    //Grade = XMLds.Tables[12].Rows[0]["Category_Id"].ToString();
                    //District_Id = XMLds.Tables[12].Rows[0]["District_Id"].ToString();
                    //AvgMoisture_Content_To = XMLds.Tables[12].Rows[0]["AvgMoisture_Content_To"].ToString();
                    //SangrahadDate = XMLds.Tables[12].Rows[0]["SangrahadDate"].ToString();
                    //For Failed Payment
                    if (Bill_Mode == "F")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F1";

                    }
                    else if (Bill_Mode == "F2")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F2";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        G_Bill_No = Bill_Number;
                        Bill_Number = Bill_Number + "F3";

                    }
                }

                //Generate CSum
                string CheckSumString = Bill_Number + District_Id + Branch_Id + Depositor_Id + Commodity_Id + Financial_Year + Per_Month_Rate + Per_Day_Rate + Net_Amount + Sub_Amount + GST_Perc + GST_Amt + Created_Date + Created_By + Month_No + Godown_Id + Crop_Year + Account_No + IFSC_Code + Serial_No + LocalIP + DSC_Holder;
                var sha1 = System.Security.Cryptography.SHA1.Create();
                byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
                byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
                var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
                //Actual
                string query = "";
                if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && Godown_Type == "91" && Depositor_Id == "10535")
                {
                    //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    //{
                    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_Nafed]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_Nafed]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Branch_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";

                    //}
                }
                else if (DSC_User_Type == "B" && Bill_Type == "MPWLC" && (Godown_Type == "81" || Godown_Type == "83" || Godown_Type == "84" || Godown_Type == "86") && Depositor_Id == "10535")
                {
                    if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_Nafed]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    }
                }
                else if (DSC_User_Type == "B" && Bill_Type == "PVT" && Depositor_Id == "10535")
                {
                    //Commented for Stop Rent Bill Sign by Branch 18/02/2022
                    //Commented for open Rent Bill Sign by Branch 01/07/2022
                    //savan
                    if (Bill_Mode == "F")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Branch_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    else if (Bill_Mode == "F3")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Branch_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";

                    }
                    else
                    {
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Branch_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    }

                    //joshi

                }
                else if (DSC_User_Type == "G" && Depositor_Id == "10535")
                {
                    //Commented for Stop Rent Bill Sign GM 11/12/2021
                    //Commented for Stop Rent Bill Sign GM 28/02/2022
                    if (Bill_Mode == "F")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1')";
                        //query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "F1','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F1','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F2")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F2','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else if (Bill_Mode == "F3")
                    {
                        string Ref_Bill = Get_Ref_Bill(G_Bill_No);
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Re_Push,Ref_Bill_No,Prev_Bill_No) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','F3','" + Ref_Bill + "','" + G_Bill_No + "')";
                    }
                    else
                    {
                        query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_PVT]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                    }
                }
                //Test
                //string query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_RO]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type]) values ('" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "')";
                else if (DSC_User_Type == "R" && Bill_Type == "MPWLC" && Depositor_Id == "10535")
                {
                    //if (Account_No == "38906616113" && IFSC_Code == "SBIN0030343")
                    //{
                    query = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Digitally_Signed_Bill_Details_Nafed]([Bill_Number],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Financial_Year],[Per_Month_Rate],[Per_Day_Rate],[Net_Amount],[Sub_Amount],[GST_Perc],[GST_Amt],[Created_Date_Bill],[Created_By_Bill],[Month_No],[Godown_Id],[Crop_Year],Account_No,IFSC_Code,[WHR_Check_Sum],[CreatedDate],[CreatedBy],[DSC_Serial_No],[DSC_Holder_Name],[Client_Ip],[DSC_User_Type],Ref_Bill_No,Party_Name,Party_Id,Bill_Type) values ('" + "91" + Bill_Number + "','" + District_Id + "','" + Branch_Id + "','" + Depositor_Id + "','" + Commodity_Id + "','" + Financial_Year + "','" + Per_Month_Rate + "','" + Per_Day_Rate + "','" + Net_Amount + "','" + Sub_Amount + "','" + GST_Perc + "','" + GST_Amt + "','" + Created_Date + "','" + Created_By + "','" + Month_No + "','" + Godown_Id + "','" + Crop_Year + "','" + Account_No + "','" + IFSC_Code + "','" + hashstrs + "',GETDATE(),'" + IpAdd + "','" + Serial_No + "','" + DSC_Holder + "','" + LocalIP + "','" + DSC_User_Type + "','" + Bill_Number + "','" + MPWLCParty + "','9999','" + Godown_Type + "')";
                    //}
                }

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();
                con.Close();
                //Is_SuccessInsert = "Y";
                if (a > 0)
                {
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    //string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "')";
                    //    string query2 = "INSERT INTO [tbl_Digital_Signature_Details]([WHR_No],DSC_Serial_No,DSC_Holder_Name,DSC_Subject,[Sig_SignatureValue],[Cano_Algorithm],[SM_Algorithm],[Ref_DigestValue],[TF_Algorithm],[DM_Algorithm],[KeyInfo_KeyName],[RSA_Modulus],[RSA_Exponent],[X509Certificate],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + Depositor_WHR_Id + "','" + Serial_No + "','" + DSC_Holder + "','" + DSC_Subject + "','" + Sig_SignatureValue + "','" + Cano_Algorithm + "','" + SM_Algorithm + "','" + Ref_DigestValue + "','" + TF_Algorithm + "','" + DM_Algorithm + "','" + KeyInfo_KeyName + "','" + RSA_Modulus + "','" + RSA_Exponent + "','" + X509Certificate + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //    SqlCommand cmd2 = new SqlCommand(query2, con);
                    //    con.Open();
                    //    int b = cmd2.ExecuteNonQuery();
                    //    con.Close();
                    //    if (b > 0)
                    //    {
                    //        string XMLData = DSString;
                    //        //string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File]) VALUES ('" + WHRID + "','" + XMLData + "')";
                    //        string query3 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_DSC_WHR_XML_File] ([WHR_Id],[WHR_XML_File],[DSC_Serial_No],[DSC_User_Type],[CreatedDate],[CreatedBy],[Client_Ip]) VALUES ('" + WHRID + "','" + XMLData + "','" + Serial_No + "','" + DSC_User_Type + "',GETDATE(),'" + WHR_Client_IP + "','" + LocalIP + "')";

                    //        SqlCommand cmd3 = new SqlCommand(query3, con);
                    //        con.Open();
                    //        int c = cmd3.ExecuteNonQuery();
                    //        con.Close();
                    //        if (c > 0)
                    //        {
                    Is_SuccessInsert = "Y";
                    //        }

                }
            }


        }

        catch (Exception)
        {

            throw;

        }
        return Is_SuccessInsert;
    }


    [WebMethod(Description = "This Method Is Used to Get Storage Bill Detail for NAFED")]
    public DataSet Retrieve_NAFEDBillDetail(string Bill_No, string Cred, string Bill_Type)
    {
        string Credential = Cred;
        string query = "";
        try
        {
            if (Credential == "WLC2019DSCNicv30")
            {
                if (Bill_Type == "MPWLC")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id AS Godown_Id,B.Crop_Year from tbl_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "'";
                }
                else if (Bill_Type == "PVT")
                {
                    query = "select B.Bill_Number,B.District_Id as District_Id,B.Branch_Id,B.Depositor_Id,B.Commodity_Id,B.Financial_Year,B.Commodity_Rate,B.Per_Day_Rate,B.Net_Amount,B.Sub_Amount,B.Service_Tax_Perc as GST_Perc,B.Service_Tax_Amt as GST_Amt,B.Created_Date,B.Client_IP as Created_By,B.Month,B.Godown_Id,B.Crop_Year from tbl_Storage_Bill_Details as B where Bill_Number='" + Bill_No + "' and ( B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.GO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type!='Hired')) or B.Godown_Id in (select Godown_Id from tbl_Godown_Owner_Account_Details as G where G.RO_Approval_Status='Y' and G.Godown_Id in (select distinct Godown_ID from tbl_MetaData_GODOWN_2018 as GG where GG.Godown_ID=B.Godown_Id and GG.Hired_Type='Hired')))";
                }

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