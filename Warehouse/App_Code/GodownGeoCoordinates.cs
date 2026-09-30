using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for GodownGeoCoordinates
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class GodownGeoCoordinates : System.Web.Services.WebService
{
    SqlConnection conn;
    public  GodownGeoCoordinates()
    {
        //Uncomment the following line if using designed components 
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    }




    public class Stock
    {
        public string StockName { get; set; }
        public string IsActive { get; set; }
        public string IsDone { get; set; }
        public string StockID { get; set; }
        public string DistID { get; set; }
        public string BranchID { get; set; }
        public string OfficerName { get; set; }
        public string MobileNumber { get; set; }
        public string DepotName { get; set; }

        public int GodownCount { get; set; }

        public Stock()
        {

        }
    }


    public class Godown
    {
        public Godown(string goDownID, string goDownName, int isUpdate)
        {
            GoDownID = goDownID;
            GoDownName = goDownName;
            isUpdated = isUpdate;
        }

        public string GoDownID { get; set; }
        public string GoDownName { get; set; }

        public int isUpdated { get; set; }

        public Godown()
        {

        }

    }

    ///*Todo : select =1, insert = 2, update = 3, delete=4 */
    ///    TODO : 0 not success, 1 success, 2 excetpion
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void StockMaster(string distID, string branchID, 
    //    string stockName , string flag , string isActive , string stockID )
    //{
    //    /*TODO: THIS PARAM IS OPTIONAL string stockName , string flag , string isActive , string stockID
    //     MAKE OPTION BY KEYWORD BUT ON SERVER FRAMEWORK IS LOWER VERSION SO NOT SUPPORT OPTIONAL*/

    //    List<Stock> stockList = new List<Stock>();
    //    if (flag == "1")
    //    {
    //        using (SqlCommand cmd = new SqlCommand("sp_fertilizer_StockMaster", conn))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@flag", "select");
    //            conn.Open();
    //            SqlDataReader reader = cmd.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                Stock stock = new Stock();
    //                stock.IsDone = "1";
    //                stock.IsActive = Convert.ToString(reader["ISACTIVE"]);
    //                stock.StockName = Convert.ToString(reader["STOCKNAME"]);
    //                stock.StockID = Convert.ToString(reader["STOCKID"]);
    //                stockList.Add(stock);
    //            }
    //            reader.Close();
    //            cmd.Dispose();
    //        }
    //        conn.Close();
    //    }
    //    else if (flag == "2")
    //    {
    //        int isInsert = 0;
    //        try
    //        {
    //            using (SqlCommand cmd = new SqlCommand("sp_fertilizer_StockMaster", conn))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@flag", "insert");
    //                cmd.Parameters.AddWithValue("@stockName", stockName);
    //                conn.Open();
    //                isInsert = cmd.ExecuteNonQuery();
    //                cmd.Dispose();
    //            }
    //            if (isInsert > 0)
    //            {
    //                using (SqlCommand cmdd = new SqlCommand("sp_fertilizer_StockMaster", conn))
    //                {
    //                    cmdd.CommandType = CommandType.StoredProcedure;
    //                    cmdd.Parameters.AddWithValue("@flag", "select");
    //                    //conn.Open();
    //                    SqlDataReader reader = cmdd.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        Stock stock = new Stock();
    //                        stock.IsDone = "1";
    //                        stock.IsActive = Convert.ToString(reader["ISACTIVE"]);
    //                        stock.StockName = Convert.ToString(reader["STOCKNAME"]);
    //                        stock.StockID = Convert.ToString(reader["STOCKID"]);
    //                        stockList.Add(stock);
    //                    }
    //                    reader.Close();
    //                    cmdd.Dispose();
    //                }
    //                conn.Close();
    //            }
    //            else
    //            {
    //                Stock stock = new Stock();
    //                stock.IsDone = "0";
    //                stock.IsActive = "";
    //                stock.StockName = "";
    //                stock.StockID = "";
    //                stockList.Add(stock);
    //            }
    //        }
    //        catch (SqlException sqlExc)
    //        {
    //            SqlError sqlError = sqlExc.Errors[0];
    //            if (sqlError.Class == 14)
    //            {
    //                Stock stock = new Stock();
    //                stock.IsDone = "2";
    //                stock.IsActive = "";
    //                stock.StockName = "Record already exists";
    //                stock.StockID = "";
    //                stockList.Add(stock);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Stock stock = new Stock();
    //            stock.IsDone = "2";
    //            stock.IsActive = "";
    //            stock.StockName = e.Message;
    //            stock.StockID = "";
    //            stockList.Add(stock);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //            conn.Dispose();
    //        }
    //    }

    //    else if (flag == "3")
    //    {
    //        int isUpdated = 0;
    //        try
    //        {
    //            using (SqlCommand cmd = new SqlCommand("sp_fertilizer_StockMaster", conn))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@flag", "update");
    //                cmd.Parameters.AddWithValue("@stockName", stockName);
    //                cmd.Parameters.AddWithValue("@isActive", isActive);
    //                cmd.Parameters.AddWithValue("@stockID", stockID);
    //                conn.Open();
    //                isUpdated = cmd.ExecuteNonQuery();
    //                cmd.Dispose();
    //            }
    //            if (isUpdated > 0)
    //            {
    //                using (SqlCommand cmdd = new SqlCommand("sp_fertilizer_StockMaster", conn))
    //                {
    //                    cmdd.CommandType = CommandType.StoredProcedure;
    //                    cmdd.Parameters.AddWithValue("@flag", "select");

    //                    //conn.Open();
    //                    SqlDataReader reader = cmdd.ExecuteReader();
    //                    while (reader.Read())
    //                    {
    //                        Stock stock = new Stock();
    //                        stock.IsDone = "1";
    //                        stock.IsActive = Convert.ToString(reader["ISACTIVE"]);
    //                        stock.StockName = Convert.ToString(reader["STOCKNAME"]);
    //                        stock.StockID = Convert.ToString(reader["STOCKID"]);
    //                        stockList.Add(stock);
    //                    }
    //                    reader.Close();
    //                    cmdd.Dispose();
    //                }
    //                conn.Close();
    //            }
    //            else
    //            {
    //                Stock stock = new Stock();
    //                stock.IsDone = "0";
    //                stock.IsActive = "";
    //                stock.StockName = "";
    //                stock.StockID = "";
    //                stockList.Add(stock);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Stock stock = new Stock();
    //            stock.IsDone = "2";
    //            stock.IsActive = "";
    //            stock.StockName = e.Message;
    //            stock.StockID = "";
    //            stockList.Add(stock);
    //        }
    //        finally
    //        {
    //            conn.Close();
    //            conn.Dispose();
    //        }
    //    }
    //    Context.Response.Write(JsonHelper.JsonSerializer<IList<Stock>>(stockList));
    //}


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GeoTaggingLogin(string BranchID,string password)
    {
        List<Stock> stockList = new List<Stock>();
        using (SqlCommand cmd = new SqlCommand("SP_fertilizer_LOGINBYMOBILE", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@branchID", BranchID);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@flag", "BRANCH");
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Stock stock = new Stock();
                stock.IsDone = "1";
                stock.BranchID = Convert.ToString(reader["BranchId"]);
                stock.GodownCount = Convert.ToInt32(reader["TOALGODOWN"]);
                stock.OfficerName = Convert.ToString(reader["NodalOfficeName"]);
                stock.MobileNumber = Convert.ToString(reader["NodalOfficerMobile"]);
                stock.DepotName = Convert.ToString(reader["DepotName"]);
                stock.DistID = Convert.ToString(reader["DistrictId"]);
                stockList.Add(stock);
            }
            reader.Close();
            cmd.Dispose();
        }
        conn.Close();
        Context.Response.Write(JsonHelper.JsonSerializer<IList<Stock>>(stockList));
    }


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void StockLogin(string mobileNumber)
    //{
    //    List<Stock> stockList = new List<Stock>();
    //    using (SqlCommand cmd = new SqlCommand("SP_fertilizer_LOGINBYMOBILE", conn))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@MOB", mobileNumber);
    //        cmd.Parameters.AddWithValue("@flag", "BRANCH");
    //        conn.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();
    //        while (reader.Read())
    //        {
    //            Stock stock = new Stock();
    //            stock.IsDone = "1";
    //            stock.BranchID = Convert.ToString(reader["BranchId"]);
    //            stock.GodownCount = Convert.ToInt32(reader["TOALGODOWN"]);
    //            stock.OfficerName = Convert.ToString(reader["NodalOfficeName"]);
    //            stock.MobileNumber = Convert.ToString(reader["NodalOfficerMobile"]);
    //            stock.DepotName = Convert.ToString(reader["DepotName"]);
    //            stock.DistID = Convert.ToString(reader["DistrictId"]);
    //            stockList.Add(stock);
    //        }
    //        reader.Close();
    //        cmd.Dispose();
    //    }
    //    conn.Close();
    //    Context.Response.Write(JsonHelper.JsonSerializer<IList<Stock>>(stockList));
    //}

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetGodown(string BranchID)
    {
        List<Godown> godowns = new List<Godown>();
        using (SqlCommand cmd = new SqlCommand("sp_fertilizer_getGodown", conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DepotID", BranchID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Godown godown = new Godown();
                godown.GoDownID = Convert.ToString(reader["Godown_ID"]);
                godown.GoDownName = Convert.ToString(reader["Godown_Name"]);
                godowns.Add(godown);
            }
            reader.Close();
            cmd.Dispose();
        }
        conn.Close();
        Context.Response.Write(JsonHelper.JsonSerializer<IList<Godown>>(godowns));
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateGodownLatLong(string Lat, string Long, string GodownID, string districtID, string branchID, string IPAddress)
    {
        List<Godown> Godowns = new List<Godown>();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_fertilizer_updateGodownLatLong", conn))
            {
                int isUpdated = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Lat", Lat);
                cmd.Parameters.AddWithValue("@Long", Long);
                cmd.Parameters.AddWithValue("@godownID", GodownID);
                cmd.Parameters.AddWithValue("@districtID", districtID);
                cmd.Parameters.AddWithValue("@branchID", branchID);
                cmd.Parameters.AddWithValue("@IPAddress", IPAddress);
                conn.Open();
                isUpdated = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (isUpdated > 0)
                {
                    Godown godown = new Godown()
                    {
                        GoDownID = GodownID,
                        GoDownName = "null",
                        isUpdated = 1
                    };
                    Godowns.Add(godown);
                }
                else
                {
                    Godown godown = new Godown()
                    {
                        GoDownID = GodownID,
                        GoDownName = "null",
                        isUpdated = 0
                    };
                    Godowns.Add(godown);
                }
            }
        }
        catch (Exception ex)
        {
            Godown godown = new Godown()
            {
                GoDownID = ex.Message,
                GoDownName = "null",
                isUpdated = 2
            };
            Godowns.Add(godown);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Context.Response.Write(JsonHelper.JsonSerializer<IList<Godown>>(Godowns));
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetUpdatedGodown(string DepotID)
    {
        List<Godown> godowns = new List<Godown>();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_fertilizer_getGodown_doneGeoTagging", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepotID", DepotID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Godown godown = new Godown()
                    {
                        GoDownID = Convert.ToString(reader["Godown_ID"]),
                        GoDownName = Convert.ToString(reader["Godown_Name"]),
                        isUpdated = 1
                    };
                    godowns.Add(godown);
                }
                reader.Close();
                cmd.Dispose();
            }
            conn.Close();
        }
        catch (Exception e) {
            Godown godown = new Godown()
            {
                GoDownID = "null",
                GoDownName = e.Message,
                isUpdated = 2
            };
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        Context.Response.Write(JsonHelper.JsonSerializer<IList<Godown>>(godowns));
    }
}

