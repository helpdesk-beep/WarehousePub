using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for NAFED_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class NAFED_WS : System.Web.Services.WebService
{
    public NAFED_WS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /*************************************Class for Master Data***************************************************/
    public class MasterStateData
    {
        public string StateName;
        public int StateId;
    }

    public class MasterDistrictData
    {
        public string DistrictName;
        public int DistrictID;
    }

    public class MasterGodownWarehouseData
    {
        public string WHCode;
        public string WHName;
        public string WHState;
        public string WHDistrict;
        public string WHPinCode;
        public string WHAddress;
        public string WHContactPersonName;
        public string WHContactPhone;
        public string WHContactEmail;
        public string WHCapacity;
        public string WHLongitude;
        public string WHLatitude;
        public string WHLicenseNumber;
    }

    public class MasterCommodityData
    {
        public string CommodityCode;
        public string CommodityName;
    }

    public class MasterDepositorData
    {
        public string DepositorCode;
        public string DepositorName;
    }

    public class eWHRAPIData
    {
        public string StateName;
        public string StateCode;
        public string WHCode;
        public string DONumber;
        public string Year;
        public string Season;
        public string Commodity;
        public string WHRNumber;
        //public DateTime WHRCreationDate;
        public string WHRCreationDate;
        public string WHRCreatedBy;
        public string ClientCode;
        public string ClientName;
        public string DepositorCode;
        public string DepositorName;
        public string VehicleType;
        public string VehicleIdentificationNumber;
        public string DriverName;
        public string StockValidityDate;
        public string VehicleLoadedWeight;
        public double TotalWeight;
        public double AcceptedWeight;
        public int AcceptedNumberOfBags;
        public double AvailableNumberOfBags;
        public double RejectedWeight;
        public int RejectedNumberofBags;
        public string RejectionReason;
        public string Remarks;
        public string QualityParametersDetails;
        public string Grade;
        public string GoodsCondition;
        public double RateatthetimeofDeposit;
        public double TotalValueofGoods;
        public double StorageRate;
        public string Godown;
        public string StackNumber;
        public string PackageType;

    }

    [WebMethod]
    public void MasterDataState(string username, string password)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
        {
            List<MasterStateData> listMSD = new List<MasterStateData>();
            con.ConnectionString = str;
            con.Open();
            string query = "select CONVERT(INT,State_Id) as [State LGD Code], CONVERT(VARCHAR(100), State_Name) as [State Name] from tbl_MetaData_STATE";
            cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MasterStateData ObjMSD = new MasterStateData();
                ObjMSD.StateId = Convert.ToInt32(dr["State LGD Code"]);
                ObjMSD.StateName = Convert.ToString(dr["State Name"]);
                listMSD.Add(ObjMSD);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMSD));
        }
    }

    [WebMethod]
    public void MasterDataDistrict(string username, string password)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
        {
            List<MasterDistrictData> listMDD = new List<MasterDistrictData>();
            con.ConnectionString = str;
            con.Open();
            string query = "SELECT CONVERT(VARCHAR(100),District_Name) as [District Name of Warehouse District], CONVERT(INT,District_Id)  as [District LGD code of Warehouse District] from tbl_MetaData_DISTRICT";
            cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MasterDistrictData ObjMDD = new MasterDistrictData();
                ObjMDD.DistrictID = Convert.ToInt32(dr["District LGD code of Warehouse District"]);
                ObjMDD.DistrictName = Convert.ToString(dr["District Name of Warehouse District"]);
                listMDD.Add(ObjMDD);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMDD));
        }
    }

    [WebMethod]
    public void MasterDataWarehouse(string username, string password)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
        {
            List<MasterGodownWarehouseData> listMWGD = new List<MasterGodownWarehouseData>();
            con.ConnectionString = str;
            con.Open();
            string query = "SELECT CONVERT(VARCHAR(50),Godown_ID) as [WH Code], CONVERT(VARCHAR(200),Godown_Name) as [WH Name], CONVERT(VARCHAR(100),StateId) as [WH State], CONVERT(VARCHAR(100),DistrictId) as [WH District], CONVERT(INT,WD.Pincode) as [WH Pincode], CONVERT(VARCHAR(100),WD.PostalAddress) as [WH Address], CONVERT(VARCHAR(100),WD.NameofWarehouseManager) as [WH Contact Person Name], CONVERT(VARCHAR(20),MG.Godown_Mobile) as [WH Contact Number],CONVERT(VARCHAR(50),MG.Godown_Email) as [WH Contact email], CONVERT(Numeric(22,5),MG.Godown_Scientific_Capacity) as [WH Capacity (MT)], CONVERT(VARCHAR(22),MG.Longitude) as [WH Longitudes], CONVERT(VARCHAR(22),MG.Latitude) as [WH Latitudes], CONVERT(VARCHAR(100),MG.LicNum) as [WH License Number] from tbl_MetaData_GODOWN_2018  MG INNER JOIN tbl_Warehouse_Details WD ON MG.WHID = WD.WHID";
            cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MasterGodownWarehouseData ObjMWGD = new MasterGodownWarehouseData();
                ObjMWGD.WHCode = Convert.ToString(dr["WH Code"]);
                ObjMWGD.WHName = Convert.ToString(dr["WH Name"]);
                ObjMWGD.WHState = Convert.ToString(dr["WH State"]);
                ObjMWGD.WHDistrict = Convert.ToString(dr["WH District"]);
                ObjMWGD.WHPinCode = Convert.ToString(dr["WH Pincode"]);
                ObjMWGD.WHAddress = Convert.ToString(dr["WH Address"]);
                ObjMWGD.WHContactPersonName = Convert.ToString(dr["WH Contact Person Name"]);
                ObjMWGD.WHContactPhone = Convert.ToString(dr["WH Contact Number"]);
                ObjMWGD.WHContactEmail = Convert.ToString(dr["WH Contact email"]);
                ObjMWGD.WHCapacity = Convert.ToString(dr["WH Capacity (MT)"]);
                ObjMWGD.WHLongitude = Convert.ToString(dr["WH Longitudes"]);
                ObjMWGD.WHLatitude = Convert.ToString(dr["WH Latitudes"]);
                ObjMWGD.WHLicenseNumber = Convert.ToString(dr["WH License Number"]);
                listMWGD.Add(ObjMWGD);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMWGD));
        }
    }

    [WebMethod]
    public void MasterDataCommodity(string username, string password)
    {
        StringBuilder SB = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
        {
            List<MasterCommodityData> listMCD = new List<MasterCommodityData>();
            con.ConnectionString = str;
            con.Open();
            string query = "SELECT CONVERT(VARCHAR(50),commodity_name) as [Commodity Name], CONVERT(VARCHAR(50),commodity_id) as [Commodity Code] from tbl_Metadata_CFSP_Commodity_Master ";
            cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MasterCommodityData ObjMCD = new MasterCommodityData();
                ObjMCD.CommodityCode = Convert.ToString(dr["Commodity Code"]);
                ObjMCD.CommodityName = Convert.ToString(dr["Commodity Name"]);
                listMCD.Add(ObjMCD);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMCD));
        }

    }

    [WebMethod]
    public void MasterDataClientDepositor(string username, string password)
    {
        StringBuilder SB = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
        {
            List<MasterDepositorData> listMCDD = new List<MasterDepositorData>();
            con.ConnectionString = str;
            con.Open();
            //string query = "SELECT CONVERT(VARCHAR(50),Depositor_ID) as [Client Codes],CONVERT(VARCHAR(100),Depositor_Name) as [Client Names] from tbl_MetaData_DEPOSITOR where Depositor_Name like '%NAFED%'";
            //string query = "SELECT CONVERT(VARCHAR(50),Depositor_ID) as [Client Codes],CONVERT(VARCHAR(100),Depositor_Name) as [Client Names] from tbl_MetaData_DEPOSITOR where Depositor_Code = '219'";
            string query = "SELECT CONVERT(VARCHAR(50),Depositor_ID) as [Client Codes],CONVERT(VARCHAR(100),Depositor_Name) as [Client Names] from tbl_MetaData_DEPOSITOR where Depositor_ID = '10535'";
            cmd = new SqlCommand(query, con);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MasterDepositorData ObjMCDD = new MasterDepositorData();
                ObjMCDD.DepositorCode = Convert.ToString(dr["Client Codes"]);
                ObjMCDD.DepositorName = Convert.ToString(dr["Client Names"]);
                listMCDD.Add(ObjMCDD);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMCDD));
        }

    }

    [WebMethod]
    public void eWHRDataAPIToJson(DateTime FromDate, DateTime ToDate)
    {
        StringBuilder SB = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        List<eWHRAPIData> listEWHRAPIData = new List<eWHRAPIData>();
        con.ConnectionString = str;
        con.Open();
        string query = "usp_eWHRAPIData";
        cmd = new SqlCommand("usp_eWHRAPIData", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FromDate", FromDate);
        cmd.Parameters.AddWithValue("@EndDate", ToDate);
        //cmd = new SqlCommand(query, con);
        cmd.CommandTimeout = 0;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            eWHRAPIData ObjeWHRAPIData = new eWHRAPIData();
            ObjeWHRAPIData.StateName = Convert.ToString(dr["State Name"]);
            ObjeWHRAPIData.StateCode = Convert.ToString(dr["State Code"]);
            ObjeWHRAPIData.WHCode = Convert.ToString(dr["WH Code"]);
            ObjeWHRAPIData.DONumber = Convert.ToString(dr["DO Number"]);
            ObjeWHRAPIData.Year = Convert.ToString(dr["Year"]);
            //ObjeWHRAPIData.Season = Convert.ToString(dr["Season"]);
            ObjeWHRAPIData.Commodity = Convert.ToString(dr["Commodity"]);
            ObjeWHRAPIData.WHRNumber = Convert.ToString(dr["WHR Number"]);
            ObjeWHRAPIData.WHRCreationDate = Convert.ToString(dr["WHR Creation Date"]);
            //ObjeWHRAPIData.WHRCreationDate = Convert.ToDateTime(dr["WHR Creation Date"]);
            ObjeWHRAPIData.WHRCreatedBy = Convert.ToString(dr["WHR Created By"]);
            //ObjeWHRAPIData.ClientCode = Convert.ToString(dr["Client Code"]);
            //ObjeWHRAPIData.ClientName = Convert.ToString(dr["Client Name"]);
            ObjeWHRAPIData.DepositorCode = Convert.ToString(dr["Depositor Code"]);
            ObjeWHRAPIData.DepositorName = Convert.ToString(dr["Depositor Name"]);
            ObjeWHRAPIData.VehicleType = Convert.ToString(dr["Vehicle Type"]);
            ObjeWHRAPIData.VehicleIdentificationNumber = Convert.ToString(dr["Vehicle Identification Number"]);
            ObjeWHRAPIData.DriverName = Convert.ToString(dr["Driver Name"]);
            //ObjeWHRAPIData.StockValidityDate = Convert.ToString(dr["Stock Validity Date"]);
            //ObjeWHRAPIData.VehicleLoadedWeight = Convert.ToString(dr["Vehicle Loaded Weight"]);
            //ObjeWHRAPIData.TotalWeight = Convert.ToDouble(dr["Total Weight"]);
            ObjeWHRAPIData.AcceptedWeight = Convert.ToDouble(dr["Accepted Weight"]);
            ObjeWHRAPIData.AcceptedNumberOfBags = Convert.ToInt32(dr["Accepted Number of Bags"]);
            //ObjeWHRAPIData.RejectedWeight = Convert.ToDouble(dr["Rejected Weight"]);
            //ObjeWHRAPIData.RejectedNumberofBags = Convert.ToInt32(dr["Rejected Number of Bags"]);
            //ObjeWHRAPIData.RejectionReason = Convert.ToString(dr["Rejection Reason"]);
            ObjeWHRAPIData.Remarks = Convert.ToString(dr["Remarks"]);
            //ObjeWHRAPIData.QualityParametersDetails = Convert.ToString(dr["Quality Parameters Details"]);
            ObjeWHRAPIData.Grade = Convert.ToString(dr["Grade"]);
            //ObjeWHRAPIData.GoodsCondition = Convert.ToString(dr["Goods Condition"]);
            //ObjeWHRAPIData.RateatthetimeofDeposit = Convert.ToDouble(dr["Rate at the time of Deposit"]);
            ObjeWHRAPIData.TotalValueofGoods = Convert.ToDouble(dr["Total Value of Goods"]);
            //ObjeWHRAPIData.StorageRate = Convert.ToDouble(dr["Storage Rate"]);
            ObjeWHRAPIData.Godown = Convert.ToString(dr["Godown"]);
            ObjeWHRAPIData.StackNumber = Convert.ToString(dr["Stack Number"]);
            //ObjeWHRAPIData.PackageType = Convert.ToString(dr["Package Type"]);
            listEWHRAPIData.Add(ObjeWHRAPIData);

        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.Write(js.Serialize(listEWHRAPIData));
    }

    //[WebMethod]
    //public void eWHRDataAPI(string username, string password, DateTime FromDate, DateTime ToDate)
    //{
        
    //    if (username == "MPWLC_UAT" && password == "MPWLC_UAT")
    //    {
    //        //DateTime FromDate;
    //        //DateTime ToDate;
    //        //NAFED.NAFED_WS NAFEDDemo = new NAFED.NAFED_WS();
    //        //NAFEDDemo.eWHRDataAPIToJson(FromDate, ToDate);
    //        eWHRDataAPIToJsonAll(FromDate, ToDate);
    //    }


    //}

    //public void eWHRDataAPIToJsonAll(DateTime FromDate, DateTime ToDate)
    //{
    //    StringBuilder SB = new StringBuilder();
    //    DataSet ds = new DataSet();
    //    SqlDataAdapter da = new SqlDataAdapter();
    //    String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
    //    SqlConnection con = new SqlConnection();
    //    SqlCommand cmd = new SqlCommand();
    //    string status = "";
    //    bool isSuccess = false;
    //    List<eWHRAPIData> listEWHRAPIData = new List<eWHRAPIData>();
    //    con.ConnectionString = str;
    //    con.Open();
    //    string query = "usp_eWHRAPIData";
    //    cmd = new SqlCommand("usp_eWHRAPIData", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@FromDate", FromDate);
    //    cmd.Parameters.AddWithValue("@EndDate", ToDate);
    //    //cmd = new SqlCommand(query, con);
    //    cmd.CommandTimeout = 0;
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    while (dr.Read())
    //    {
    //        eWHRAPIData ObjeWHRAPIData = new eWHRAPIData();
    //        ObjeWHRAPIData.StateName = Convert.ToString(dr["State Name"]);
    //        ObjeWHRAPIData.StateCode = Convert.ToString(dr["State Code"]);
    //        ObjeWHRAPIData.WHCode = Convert.ToString(dr["WH Code"]);
    //        ObjeWHRAPIData.DONumber = Convert.ToString(dr["DO Number"]);
    //        ObjeWHRAPIData.Year = Convert.ToString(dr["Year"]);
    //        //ObjeWHRAPIData.Season = Convert.ToString(dr["Season"]);
    //        ObjeWHRAPIData.Commodity = Convert.ToString(dr["Commodity"]);
    //        ObjeWHRAPIData.WHRNumber = Convert.ToString(dr["WHR Number"]);
    //        ObjeWHRAPIData.WHRCreationDate = Convert.ToString("WHR Creation Date");
    //        //ObjeWHRAPIData.WHRCreationDate = Convert.ToDateTime("WHR Creation Date");
    //        ObjeWHRAPIData.WHRCreatedBy = Convert.ToString(dr["WHR Created By"]);
    //        //ObjeWHRAPIData.ClientCode = Convert.ToString(dr["Client Code"]);
    //        //ObjeWHRAPIData.ClientName = Convert.ToString(dr["Client Name"]);
    //        ObjeWHRAPIData.DepositorCode = Convert.ToString(dr["Depositor Code"]);
    //        ObjeWHRAPIData.DepositorName = Convert.ToString(dr["Depositor Name"]);
    //        ObjeWHRAPIData.VehicleType = Convert.ToString(dr["Vehicle Type"]);
    //        ObjeWHRAPIData.VehicleIdentificationNumber = Convert.ToString(dr["Vehicle Identification Number"]);
    //        ObjeWHRAPIData.DriverName = Convert.ToString(dr["Driver Name"]);
    //        //ObjeWHRAPIData.StockValidityDate = Convert.ToString(dr["Stock Validity Date"]);
    //        //ObjeWHRAPIData.VehicleLoadedWeight = Convert.ToString(dr["Vehicle Loaded Weight"]);
    //        //ObjeWHRAPIData.TotalWeight = Convert.ToDouble(dr["Total Weight"]);
    //        ObjeWHRAPIData.AcceptedWeight = Convert.ToDouble(dr["Accepted Weight"]);
    //        ObjeWHRAPIData.AcceptedNumberOfBags = Convert.ToInt32(dr["Accepted Number of Bags"]);
    //        //ObjeWHRAPIData.RejectedWeight = Convert.ToDouble(dr["Rejected Weight"]);
    //        //ObjeWHRAPIData.RejectedNumberofBags = Convert.ToInt32(dr["Rejected Number of Bags"]);
    //        //ObjeWHRAPIData.RejectionReason = Convert.ToString(dr["Rejection Reason"]);
    //        ObjeWHRAPIData.Remarks = Convert.ToString(dr["Remarks"]);
    //        //ObjeWHRAPIData.QualityParametersDetails = Convert.ToString(dr["Quality Parameters Details"]);
    //        ObjeWHRAPIData.Grade = Convert.ToString(dr["Grade"]);
    //        //ObjeWHRAPIData.GoodsCondition = Convert.ToString(dr["Goods Condition"]);
    //        //ObjeWHRAPIData.RateatthetimeofDeposit = Convert.ToDouble(dr["Rate at the time of Deposit"]);
    //        ObjeWHRAPIData.TotalValueofGoods = Convert.ToDouble(dr["Total Value of Goods"]);
    //        //ObjeWHRAPIData.StorageRate = Convert.ToDouble(dr["Storage Rate"]);
    //        ObjeWHRAPIData.Godown = Convert.ToString(dr["Godown"]);
    //        ObjeWHRAPIData.StackNumber = Convert.ToString(dr["Stack Number"]);
    //        //ObjeWHRAPIData.PackageType = Convert.ToString(dr["Package Type"]);
    //        listEWHRAPIData.Add(ObjeWHRAPIData);

    //    }
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    js.MaxJsonLength = Int32.MaxValue;
    //    Context.Response.Write(js.Serialize(listEWHRAPIData));
    //}

    //[WebMethod]
    //public void eWHRDataAPIToJsonAll(DateTime FromDate, DateTime ToDate)
    //{
    //    String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
    //    SqlConnection con = new SqlConnection();
    //    try
    //    {
    //        StringBuilder SB = new StringBuilder();
    //        DataSet ds = new DataSet();
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        //String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
    //        //SqlConnection con = new SqlConnection();
    //        SqlCommand cmd = new SqlCommand();
    //        string status = "";
    //        bool isSuccess = false;
    //        List<NAFEDeWHRAPIData> listEWHRAPIData = new List<NAFEDeWHRAPIData>();
    //        con.ConnectionString = str;
    //        con.Open();
    //        string query = "usp_eWHRAPIData";
    //        cmd = new SqlCommand("usp_eWHRAPIData", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@FromDate", FromDate);
    //        cmd.Parameters.AddWithValue("@EndDate", ToDate);
    //        //cmd = new SqlCommand(query, con);
    //        cmd.CommandTimeout = 0;
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        while (dr.Read())
    //        {
    //            NAFEDeWHRAPIData ObjeWHRAPIData = new NAFEDeWHRAPIData();
    //            ObjeWHRAPIData.StateName = Convert.ToString(dr["State Name"]);
    //            ObjeWHRAPIData.StateCode = Convert.ToString(dr["State Code"]);
    //            ObjeWHRAPIData.WHCode = Convert.ToString(dr["WH Code"]);
    //            ObjeWHRAPIData.DONumber = Convert.ToString(dr["DO Number"]);
    //            ObjeWHRAPIData.Year = Convert.ToString(dr["Year"]);
    //            //ObjeWHRAPIData.Season = Convert.ToString(dr["Season"]);
    //            ObjeWHRAPIData.Commodity = Convert.ToString(dr["Commodity"]);
    //            ObjeWHRAPIData.WHRNumber = Convert.ToString(dr["WHR Number"]);
    //            ObjeWHRAPIData.WHRCreationDate = Convert.ToString("WHR Creation Date");
    //            //ObjeWHRAPIData.WHRCreationDate = Convert.ToDateTime("WHR Creation Date");
    //            ObjeWHRAPIData.WHRCreatedBy = Convert.ToString(dr["WHR Created By"]);
    //            //ObjeWHRAPIData.ClientCode = Convert.ToString(dr["Client Code"]);
    //            //ObjeWHRAPIData.ClientName = Convert.ToString(dr["Client Name"]);
    //            ObjeWHRAPIData.DepositorCode = Convert.ToString(dr["Depositor Code"]);
    //            ObjeWHRAPIData.DepositorName = Convert.ToString(dr["Depositor Name"]);
    //            ObjeWHRAPIData.VehicleType = Convert.ToString(dr["Vehicle Type"]);
    //            ObjeWHRAPIData.VehicleIdentificationNumber = Convert.ToString(dr["Vehicle Identification Number"]);
    //            ObjeWHRAPIData.DriverName = Convert.ToString(dr["Driver Name"]);
    //            //ObjeWHRAPIData.StockValidityDate = Convert.ToString(dr["Stock Validity Date"]);
    //            //ObjeWHRAPIData.VehicleLoadedWeight = Convert.ToString(dr["Vehicle Loaded Weight"]);
    //            //ObjeWHRAPIData.TotalWeight = Convert.ToDouble(dr["Total Weight"]);
    //            ObjeWHRAPIData.AcceptedWeight = Convert.ToDouble(dr["Accepted Weight"]);
    //            ObjeWHRAPIData.AcceptedNumberOfBags = Convert.ToInt32(dr["Accepted Number of Bags"]);
    //            //ObjeWHRAPIData.RejectedWeight = Convert.ToDouble(dr["Rejected Weight"]);
    //            //ObjeWHRAPIData.RejectedNumberofBags = Convert.ToInt32(dr["Rejected Number of Bags"]);
    //            //ObjeWHRAPIData.RejectionReason = Convert.ToString(dr["Rejection Reason"]);
    //            ObjeWHRAPIData.Remarks = Convert.ToString(dr["Remarks"]);
    //            //ObjeWHRAPIData.QualityParametersDetails = Convert.ToString(dr["Quality Parameters Details"]);
    //            ObjeWHRAPIData.Grade = Convert.ToString(dr["Grade"]);
    //            //ObjeWHRAPIData.GoodsCondition = Convert.ToString(dr["Goods Condition"]);
    //            //ObjeWHRAPIData.RateatthetimeofDeposit = Convert.ToDouble(dr["Rate at the time of Deposit"]);
    //            ObjeWHRAPIData.TotalValueofGoods = Convert.ToDouble(dr["Total Value of Goods"]);
    //            //ObjeWHRAPIData.StorageRate = Convert.ToDouble(dr["Storage Rate"]);
    //            ObjeWHRAPIData.Godown = Convert.ToString(dr["Godown"]);
    //            ObjeWHRAPIData.StackNumber = Convert.ToString(dr["Stack Number"]);
    //            //ObjeWHRAPIData.PackageType = Convert.ToString(dr["Package Type"]);
    //            listEWHRAPIData.Add(ObjeWHRAPIData);

    //        }
    //        JavaScriptSerializer js = new JavaScriptSerializer();
    //        js.MaxJsonLength = Int32.MaxValue;
    //        Context.Response.Write(js);
    //        //return js.Serialize(listEWHRAPIData).ToString();

    //    }
    //    catch
    //    {

    //    }
    //    finally
    //    {

    //        con.Close();
    //    }



    //}


}

