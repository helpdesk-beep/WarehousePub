using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for GodownDetailsForLocation
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GodownDetailsForLocation : System.Web.Services.WebService
{

    public GodownDetailsForLocation()
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
}
