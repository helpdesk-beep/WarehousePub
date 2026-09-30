using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;

/// <summary>
/// Summary description for GodownDetailsForLocation
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GodownDetailsForSCM : System.Web.Services.WebService
{

    public GodownDetailsForSCM()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class GodownDetailsForLocationMapping
    {
        public string Region_ID;
        public string Region_Name;
        public string District_ID;
        public string District_Name;
        public string Branch_ID;
        public string Branch_Name;
        public string Godown_ID;
        public string Godown_Name;
        public string GodownType;
        public string StorageType;
        public double Godown_Capacity;
        public double Godown_Scientific_Capacity;
        public string Godown_Latitude;
        public string Godown_Longitude;
        public string Godown_Status;
    }

    [WebMethod]
    public void GetGodownLocationDetailsForMap()
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        List<GodownDetailsForLocationMapping> listGodownDetails = new List<GodownDetailsForLocationMapping>();
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand("usp_GodownDetailsForLocation", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 200;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            GodownDetailsForLocationMapping ObjGodownDetails = new GodownDetailsForLocationMapping();
            ObjGodownDetails.Region_ID = Convert.ToString(dr["RegionID"]);
            ObjGodownDetails.Region_Name = Convert.ToString(dr["RegionName"]);
            ObjGodownDetails.District_ID = Convert.ToString(dr["DistrictID"]);
            ObjGodownDetails.District_Name = Convert.ToString(dr["DistrictName"]);
            ObjGodownDetails.Branch_ID = Convert.ToString(dr["BranchID"]);
            ObjGodownDetails.Branch_Name = Convert.ToString(dr["BranchName"]);
            ObjGodownDetails.Godown_ID = Convert.ToString(dr["GodownID"]);
            ObjGodownDetails.Godown_Name = Convert.ToString(dr["GodownName"]);
            ObjGodownDetails.GodownType = Convert.ToString(dr["GodownType"]);
            ObjGodownDetails.StorageType = Convert.ToString(dr["StorageType"]);
            ObjGodownDetails.Godown_Capacity = Convert.ToDouble(dr["GodownCapacity(InMTs)"]);
            ObjGodownDetails.Godown_Scientific_Capacity = Convert.ToDouble(dr["GodownScientificCapacity(InMTs)"]);
            ObjGodownDetails.Godown_Latitude = Convert.ToString(dr["GodownLatitude"]);
            ObjGodownDetails.Godown_Longitude = Convert.ToString(dr["GodownLongitude"]);
            ObjGodownDetails.Godown_Status = Convert.ToString(dr["GodownStatus"]);
            listGodownDetails.Add(ObjGodownDetails);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.Write(js.Serialize(listGodownDetails));

    }

    public class BranchIssueCenter
    {
        public string IssueCenterId;
        public string IssueCenterName;
        public string BranchID;
        public string BranchName;
    }

    [WebMethod]
    public void GetBranchIssueCenterDetails()
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        List<BranchIssueCenter> listICDetailsDetails = new List<BranchIssueCenter>();
        con.ConnectionString = str;
        con.Open();
        cmd = new SqlCommand("select IssueCenterId,IssueCenterName,BranchID,BranchName from MetaDataBranchWithIssueCenter", con);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 200;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            BranchIssueCenter ObjICDetails = new BranchIssueCenter();
            ObjICDetails.IssueCenterId = Convert.ToString(dr["IssueCenterId"]);
            ObjICDetails.IssueCenterName = Convert.ToString(dr["IssueCenterName"]);
            ObjICDetails.BranchID = Convert.ToString(dr["BranchID"]);
            ObjICDetails.BranchName = Convert.ToString(dr["BranchName"]);

            listICDetailsDetails.Add(ObjICDetails);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.Write(js.Serialize(listICDetailsDetails));
    }

    public class IssueCenterDetails
    {
        public string District_Name;
        public string DepotName;
        public string Hired_Type;
        public string Godown_Name;
        public decimal Distance;
    }
    [WebMethod(Description = "Issue Center Details")]
    public void GetIssueCenterDetails()
    {
        string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        List<IssueCenterDetails> list = new List<IssueCenterDetails>();
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        try
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd =
                    new SqlCommand("usp_GetIssueCenterDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 200;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            IssueCenterDetails obj = new IssueCenterDetails();
                            obj.District_Name = dr["District_Name"] == DBNull.Value ? "" : Convert.ToString(dr["District_Name"]);
                            obj.DepotName = dr["DepotName"] == DBNull.Value ? "" : Convert.ToString(dr["DepotName"]);
                            obj.Hired_Type = dr["Hired_Type"] == DBNull.Value ? "" : Convert.ToString(dr["Hired_Type"]);
                            obj.Godown_Name = dr["Godown_Name"] == DBNull.Value ? "" : Convert.ToString(dr["Godown_Name"]);
                            obj.Distance = dr["distance"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["distance"]);
                            list.Add(obj);
                        }
                    }
                }
            }

            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(list));
        }
        catch (SqlException ex)
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(new
            {
                Status = false,
                Message = "Database Error",
                Error = ex.Message
            }));
        }
        catch (Exception ex)
        {
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(new
            {
                Status = false,
                Message = "Error",
                Error = ex.Message
            }));
        }
    }


    [WebMethod(Description = "Real Time Godown Stock Position")]
    public void GetRealTimeGodownStockPosition() //GetSerializedCommodityDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        List<CommodityStorageReport> reportList = new List<CommodityStorageReport>();

        // 1. Fetch data from DB using DataReader for maximum performance
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("GetCommodityWiseStoragePivotReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        // 2. Instantiate and assign database values to our C# Class
                        CommodityStorageReport reportItem = new CommodityStorageReport
                        {
                            CommodityName = rdr["CommodityName"].ToString(),
                            Godown_Code = rdr["Godown_Code"].ToString(),
                            CommodityID = Convert.ToInt32(rdr["CommodityID"]),
                            District = rdr["District"].ToString(),
                            Branch = rdr["Branch"].ToString(),
                            Godown = rdr["Godown"].ToString(),
                            HiredType = rdr["Hired_Type"].ToString(),
                            StorageType = rdr["Storage_Type"].ToString(),

                            // Map PIVOT years safely
                            Year16_17 = Convert.ToDecimal(rdr["2016-17"]),
                            Year17_18 = Convert.ToDecimal(rdr["2017-18"]),
                            Year18_19 = Convert.ToDecimal(rdr["2018-19"]),
                            Year19_20 = Convert.ToDecimal(rdr["2019-20"]),
                            Year20_21 = Convert.ToDecimal(rdr["2020-21"]),
                            Year21_22 = Convert.ToDecimal(rdr["2021-22"]),
                            Year22_23 = Convert.ToDecimal(rdr["2022-23"]),
                            Year23_24 = Convert.ToDecimal(rdr["2023-24"]),
                            Year24_25 = Convert.ToDecimal(rdr["2024-25"]),
                            Year25_26 = Convert.ToDecimal(rdr["2025-26"]),

                            Total = Convert.ToDecimal(rdr["Total"])
                        };
                        reportList.Add(reportItem);
                    }
                }
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.ContentType = "application/json";
        Context.Response.Write(js.Serialize(reportList));
        //return jsonResult;
    }
    public class CommodityStorageReport
    {
        public string CommodityName { get; set; }
        public int CommodityID { get; set; }
        public string District { get; set; }
        public string Branch { get; set; }
        public string Godown { get; set; }
        public string Godown_Code { get; set; }
        public string HiredType { get; set; }
        public string StorageType { get; set; }

        // Crop Years
        public decimal Year16_17 { get; set; }
        public decimal Year17_18 { get; set; }
        public decimal Year18_19 { get; set; }
        public decimal Year19_20 { get; set; }
        public decimal Year20_21 { get; set; }
        public decimal Year21_22 { get; set; }
        public decimal Year22_23 { get; set; }
        public decimal Year23_24 { get; set; }
        public decimal Year24_25 { get; set; }
        public decimal Year25_26 { get; set; }

        public decimal Total { get; set; }
    }


    public class FPSMapping
    {
        public string FPS_code { get; set; }
        public string FPS_name { get; set; }
        public string IssueCenterID { get; set; }
        public string GodownID { get; set; }
        public string Godown_Name { get; set; }
        public string District_Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string CreatedBy { get; set; }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Automatically serializes the return list to JSON
    public void GetFPSMappingDetails()
    {
        List<FPSMapping> mappingList = new List<FPSMapping>();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        string query = @"SELECT 
                            A.FPS_code,
                            A.FPS_name,
                            A.IssueCenterID,
                            A.GodownID,
                            B.Godown_Name,
                            C.District_Name,
                            A.Latitude,
                            A.Longitude,
                            A.CreatedBy
                         FROM FPS_issueCenter_godown_mapping A
                         INNER JOIN tbl_MetaData_GODOWN_2018 B ON A.GodownID = B.Godown_ID
                         INNER JOIN tbl_MetaData_DISTRICT C ON C.District_Id = B.DistrictId";

        using (SqlConnection conn = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FPSMapping mapping = new FPSMapping();

                            mapping.FPS_code = reader["FPS_code"] != DBNull.Value ? reader["FPS_code"].ToString() : string.Empty;
                            mapping.FPS_name = reader["FPS_name"] != DBNull.Value ? reader["FPS_name"].ToString() : string.Empty;
                            mapping.IssueCenterID = reader["IssueCenterID"] != DBNull.Value ? Convert.ToString(reader["IssueCenterID"]) : string.Empty;
                            mapping.GodownID = reader["GodownID"] != DBNull.Value ? Convert.ToString(reader["GodownID"]) : string.Empty;
                            mapping.Godown_Name = reader["Godown_Name"] != DBNull.Value ? reader["Godown_Name"].ToString() : string.Empty;
                            mapping.District_Name = reader["District_Name"] != DBNull.Value ? reader["District_Name"].ToString() : string.Empty;

                            // Safe parsing for Latitude and Longitude (handling possible nulls)
                            mapping.Latitude = reader["Latitude"] != DBNull.Value ? Convert.ToDecimal(reader["Latitude"]) : (decimal?)null;
                            mapping.Longitude = reader["Longitude"] != DBNull.Value ? Convert.ToDecimal(reader["Longitude"]) : (decimal?)null;

                            mapping.CreatedBy = reader["CreatedBy"] != DBNull.Value ? reader["CreatedBy"].ToString() : string.Empty;

                            mappingList.Add(mapping);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle or log your exception here
                    throw new Exception("Error retrieving FPS mapping data: " + ex.Message);
                }
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.ContentType = "application/json";
        Context.Response.Write(js.Serialize(mappingList));
    }


}
