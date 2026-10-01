using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Net;
using System.IO;
using System.CodeDom;
using System.Threading.Tasks;
using System.Net.Http;

/// <summary>
/// Summary description for FCI_CFSP_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FCI_CFSP_WS : System.Web.Services.WebService
{

    public FCI_CFSP_WS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class DepotMaster
    {
        public string transaction_reference_id;
        public int lgd_state_code;
        public string data_date;
        public int lgd_district_code;
        public int lgd_subdistrict_code;
        public int lgd_block_code;
        public int lgd_village_code;
        public int depot_status;
        public string depot_code;
        public string depot_name;
        public int ownership_type_id;
        public int ownership_group_type_id;
        public string depot_address;
        public int depot_pin;
        public double latitude;
        public double longitude;
        public double declared_capacity;
        public int predominant_storage_id;
        public int surveillance_cameras;
        public string camera_functional_since;
        public int no_of_cameras;
        public string crop_year;
        public string hired_date;
        public string dehired_date;
        public int data_status;
        public int depotmill;
    }

    public class MinimumStorageSpecifications
    {
        public int lgd_state_code;
        public int lgd_district_code;
        public int lgd_subdistrict_code;
        public int lgd_block_code;
        public int lgd_village_code;
        public string data_date;
        public string depot_code;
        public string commodity_id;
        public string commodity_group_id;
        public double opening_balance;
        public double closing_balance;
        public double storage_loss;
        public double storage_gain;
        public string transaction_reference_id;
        public string crop_year;
        public double data_status;
        public MSSChildQuantityIssued[] mss_issue;//msschild_quantity_issued;
        public MSSChildQuantityDispatched[] mss_dispatch; //msschild_quantity_dispatched;
        public MSSChildQuantityReceived[] mss_received; //msschild_quantity_received;

        /******************Child Elements********************************/

        public class MSSChildQuantityIssued
        {
            //public DateTime data_date;
            public string data_date;
            public string commodity_id;
            public string commodity_group_id;
            public string depot_code;
            public int pool_stock_id;
            //public string crop_year;
            public int categorization_type_id;
            public int upgradable;
            public double net_quantity_issued;
            public string issue_scheme_id;
            public string scheme_id;
            public string scheme_group_id;
            public string scheme_name;
            public int issue_destination_id;
        }

        public class MSSChildQuantityDispatched
        {
            //public DateTime data_date;
            public string data_date;
            public string depot_code;
            public string commodity_id;
            public string commodity_group_id;
            public double net_quantity_dispatched;
            public int dispatch_destination_id;
        }

        public class MSSChildQuantityReceived
        {
            //public DateTime data_date;
            public string data_date;
            public string depot_code;
            public string commodity_id;
            public string commodity_group_id;
            public int inflow_source_id;
            public double net_quantity_received;
        }
    }

    [WebMethod]
    public void DepotMasterMethod(string username, string password)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (WarehouseApiSecurity.AreCredentialsValid(username, password, "FciCfspUsername", "FciCfspPassword"))
        {
            List<DepotMaster> listDepotMaster = new List<DepotMaster>();
            con.ConnectionString = str;
            con.Open();
            //cmd = new SqlCommand("usp_CFSP_DepotMaster", con);
            cmd = new SqlCommand("usp_CFSP_DepotMasterData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DepotMaster ObjDepotMaster = new DepotMaster();
                ObjDepotMaster.transaction_reference_id = Convert.ToString(dr["transaction_reference_id"]);
                ObjDepotMaster.lgd_state_code = Convert.ToInt32(dr["lgd_state_code"]);
                ObjDepotMaster.lgd_district_code = Convert.ToInt32(dr["lgd_district_code"]);
                ObjDepotMaster.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
                ObjDepotMaster.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
                ObjDepotMaster.lgd_village_code = Convert.ToInt32(dr["lgd_village_code"]);
                ObjDepotMaster.depot_status = Convert.ToInt32(dr["depot_status"]);
                ObjDepotMaster.depot_code = Convert.ToString(dr["depot_code"]);
                ObjDepotMaster.depot_name = Convert.ToString(dr["depot_name"]);
                ObjDepotMaster.ownership_type_id = Convert.ToInt32(dr["ownership_type_id"]);
                ObjDepotMaster.ownership_group_type_id = Convert.ToInt32(dr["ownership_group_type_id"]);
                ObjDepotMaster.depot_address = Convert.ToString(dr["depot_address"]);
                ObjDepotMaster.depot_pin = Convert.ToInt32(dr["depot_pin"]);
                ObjDepotMaster.latitude = Convert.ToDouble(dr["latitude"]);
                ObjDepotMaster.longitude = Convert.ToDouble(dr["longitude"]);
                ObjDepotMaster.declared_capacity = Convert.ToDouble(dr["declared_capacity"]);
                ObjDepotMaster.predominant_storage_id = Convert.ToInt32(dr["predominant_storage_id"]);
                ObjDepotMaster.surveillance_cameras = Convert.ToInt32(dr["surveillance_cameras"]);
                DateTime varCamera_functional_since = DateTime.Parse(Convert.ToString(dr["camera_functional_since"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.camera_functional_since = varCamera_functional_since.ToString("yyyy-MM-dd");//varCamera_functional_since.ToString("dd-MM-yyyy"); //
                ObjDepotMaster.no_of_cameras = Convert.ToInt32(dr["no_of_cameras"]);
                ObjDepotMaster.crop_year = Convert.ToString(dr["crop_year"]);
                DateTime varhired_date = DateTime.Parse(Convert.ToString(dr["hired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.hired_date = varhired_date.ToString("yyyy-MM-dd");//varhired_date.ToString("dd-MM-yyyy"); //
                DateTime vardehired_date = DateTime.Parse(Convert.ToString(dr["dehired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.dehired_date = vardehired_date.ToString("yyyy-MM-dd");//vardehired_date.ToString("dd-MM-yyyy"); // 
                ObjDepotMaster.data_status = Convert.ToInt32(dr["data_status"]);
                ObjDepotMaster.depotmill = Convert.ToInt32(dr["depotmill"]);
                listDepotMaster.Add(ObjDepotMaster);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listDepotMaster));

            /**************For submitting /posting data to CFSP********************/
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cfsp.nic.in/cfsp/depot_master/MPWLC");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string jsonDataString = js.Serialize(listDepotMaster);
            //    streamWriter.Write(jsonDataString);
            //}

            //Context.Response.Write("Data Posted Successfully");
        }
        else
        {
            Context.Response.Write("Credentials Invalid");
        }
    }

    [WebMethod]
    public void MinimumStorageSpecificationsMSS(string username, string password, string DataDate)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (WarehouseApiSecurity.AreCredentialsValid(username, password, "FciCfspUsername", "FciCfspPassword"))
        {
            DateTime FromDate = DateTime.ParseExact(DataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture); //Convert.ToDateTime(DataDate);// = "2023-11-03";//DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
            //string ToDate = "2023-11-04";//DateTime.Now.AddDays(-139).ToString("yyyy-MM-dd");
            List<MinimumStorageSpecifications> listMSSTrimmed = new List<MinimumStorageSpecifications>();
            List<MinimumStorageSpecifications.MSSChildQuantityIssued> listMSSChildQuantityIssued = new List<MinimumStorageSpecifications.MSSChildQuantityIssued>();
            List<MinimumStorageSpecifications.MSSChildQuantityDispatched> listMSSChildQuantityDispatched = new List<MinimumStorageSpecifications.MSSChildQuantityDispatched>();
            List<MinimumStorageSpecifications.MSSChildQuantityReceived> listMSSChildQuantityReceived = new List<MinimumStorageSpecifications.MSSChildQuantityReceived>();
            con.ConnectionString = str;
            con.Open();
            //cmd = new SqlCommand("usp_API_MSSTrimmed_FullDataWithCreatedDate", con);
            //cmd = new SqlCommand("usp_CFSP_MSS", con);
            cmd = new SqlCommand("usp_CFSP_MSSDataByDate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataDate", FromDate);
            //cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.CommandTimeout = 200;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MinimumStorageSpecifications ObjMSSTrimmed = new MinimumStorageSpecifications();
                ObjMSSTrimmed.lgd_state_code = Convert.ToInt32(dr["lgd_state_code"]);
                ObjMSSTrimmed.lgd_district_code = Convert.ToInt32(dr["lgd_district_code"]);
                ObjMSSTrimmed.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
                ObjMSSTrimmed.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
                ObjMSSTrimmed.lgd_village_code = Convert.ToInt32(dr["lgd_village_code"]);
                //ObjMSSTrimmed.data_date = Convert.ToString(dr["data_date"]);
                DateTime var_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSTrimmed.data_date = var_data_date.ToString("yyyy-MM-dd");
                ObjMSSTrimmed.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSTrimmed.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSTrimmed.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSTrimmed.opening_balance = Convert.ToDouble(dr["opening_balance"]);
                ObjMSSTrimmed.closing_balance = Convert.ToDouble(dr["closing_balance"]);
                ObjMSSTrimmed.storage_loss = Convert.ToDouble(dr["storage_loss"]);
                ObjMSSTrimmed.storage_gain = Convert.ToDouble(dr["storage_gain"]);
                ObjMSSTrimmed.transaction_reference_id = Convert.ToString(dr["transaction_reference_id"]);
                ObjMSSTrimmed.crop_year = Convert.ToString(dr["crop_year"]);
                ObjMSSTrimmed.data_status = Convert.ToDouble(dr["data_status"]);
                //listMSSTrimmed.Add(ObjMSSTrimmed);

                /**************************MSS Trimmed Child Issue********************************/
                MinimumStorageSpecifications.MSSChildQuantityIssued ObjMSSIssue = new MinimumStorageSpecifications.MSSChildQuantityIssued();
                //ObjMSSIssue.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime var_Issue_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSIssue.data_date = var_Issue_data_date.ToString("yyyy-MM-dd");
                ObjMSSIssue.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSIssue.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSIssue.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSIssue.pool_stock_id = Convert.ToInt32(dr["pool_stock_id"]);
                //ObjMSSIssue.crop_year = Convert.ToString(dr["crop_year"]);
                ObjMSSIssue.categorization_type_id = Convert.ToInt32(dr["categorization_type_id"]);
                ObjMSSIssue.upgradable = Convert.ToInt32(dr["upgradable"]);
                ObjMSSIssue.net_quantity_issued = Convert.ToDouble(dr["net_quantity_issued"]);
                ObjMSSIssue.issue_scheme_id = Convert.ToString(dr["issue_scheme_id"]);
                ObjMSSIssue.scheme_id = Convert.ToString(dr["scheme_id"]);
                ObjMSSIssue.scheme_group_id = Convert.ToString(dr["scheme_group_id"]);
                ObjMSSIssue.scheme_name = Convert.ToString(dr["scheme_name"]);
                ObjMSSIssue.issue_destination_id = Convert.ToInt32(dr["issue_destination_id"]);
                //listMSSChildQuantityIssued.Add(ObjMSSIssue);

                /**************************MSS Trimmed Child Dispatch********************************/
                MinimumStorageSpecifications.MSSChildQuantityDispatched ObjMSSDispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched();
                //ObjMSSDispatch.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varDispatch_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSDispatch.data_date = varDispatch_data_date.ToString("yyyy-MM-dd");
                ObjMSSDispatch.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSDispatch.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSDispatch.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSDispatch.net_quantity_dispatched = Convert.ToDouble(dr["net_quantity_dispatched"]);
                ObjMSSDispatch.dispatch_destination_id = Convert.ToInt32(dr["dispatch_destination_id"]);
                //listMSSChildQuantityDispatched.Add(ObjMSSDispatch);

                /**************************MSS Trimmed Child Receive********************************/
                MinimumStorageSpecifications.MSSChildQuantityReceived ObjMSSReceive = new MinimumStorageSpecifications.MSSChildQuantityReceived();

                //ObjMSSReceive.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varReceive_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSReceive.data_date = varReceive_data_date.ToString("yyyy-MM-dd");
                ObjMSSReceive.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSReceive.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSReceive.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSReceive.inflow_source_id = Convert.ToInt32(dr["inflow_source_id"]);
                ObjMSSReceive.net_quantity_received = Convert.ToDouble(dr["net_quantity_received"]);
                //listMSSChildQuantityReceived.Add(ObjMSSReceive);
                /************************************************************************************/
                ObjMSSTrimmed.mss_issue = new MinimumStorageSpecifications.MSSChildQuantityIssued[] { ObjMSSIssue };
                ObjMSSTrimmed.mss_dispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched[] { ObjMSSDispatch };
                ObjMSSTrimmed.mss_received = new MinimumStorageSpecifications.MSSChildQuantityReceived[] { ObjMSSReceive };

                listMSSTrimmed.Add(ObjMSSTrimmed);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMSSTrimmed));
            //File.WriteAllText(@"D:\MSSData.json", js.Serialize(listMSSTrimmed));
            //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"D:\MSSData1.json"), js.Serialize(listMSSTrimmed));
            string result = Path.GetTempPath();
            //Console.WriteLine(result);
            //System.IO.File.WriteAllText(Server.MapPath(result), js.Serialize(listMSSTrimmed));
            //System.IO.File.WriteAllText(result, js.Serialize(listMSSTrimmed));
            /**************For submitting /posting data to CFSP********************/
            /*
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cfsp.nic.in/cfsp/mss/MPWLC");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonDataString = js.Serialize(listMSSTrimmed);
                streamWriter.Write(jsonDataString);
            }*/

        }
        else
        {
            Context.Response.Write("Credentials Invalid");
        }

    }

    private static List<DataTable> SplitTable(DataTable originalTable, int batchSize)
    {
        List<DataTable> tables = new List<DataTable>();
        int i = 0;
        int j = 1;
        DataTable newDt = originalTable.Clone();
        newDt.TableName = "Table_" + j;
        newDt.Clear();
        foreach (DataRow row in originalTable.Rows)
        {
            DataRow newRow = newDt.NewRow();
            newRow.ItemArray = row.ItemArray;
            newDt.Rows.Add(newRow);
            i++;
            if (i == batchSize)
            {
                tables.Add(newDt);
                j++;
                newDt = originalTable.Clone();
                newDt.TableName = "Table_" + j;
                newDt.Clear();
                i = 0;
            }



        }
        if (newDt.Rows.Count > 0)
        {
            tables.Add(newDt);
            j++;
            newDt = originalTable.Clone();
            newDt.TableName = "Table_" + j;
            newDt.Clear();

        }
        return tables;
    }

    private static DataTable GetData()
    {
        string conString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //string query = "usp_CFSP_DepotMaster";
        string query = "usp_CFSP_DepotMasterData";
        //string query = "usp_CFSP_DepotMaster_Dummy";
        using (SqlConnection con = new SqlConnection(conString))
        {

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }

    [WebMethod]
    public void DataTableToJson()
    {
        DataTable OriginalTable = new DataTable();
        //OriginalTable = GetData();
        OriginalTable = GetRemainingDepotData();
        //OriginalTable.Skip(0).Take(100);
        //OriginalTable.Skip(100).Take(100);
        //OriginalTable.Skip(200).Take(100);
        //OriginalTable.Skip(300).Take(100);
        DataTable t1 = OriginalTable.AsEnumerable().Skip(0).Take(1000).CopyToDataTable();
        DataTable t2 = OriginalTable.AsEnumerable().Skip(1000).Take(1000).CopyToDataTable();
        DataTable t3 = OriginalTable.AsEnumerable().Skip(2000).Take(1000).CopyToDataTable();
        //DataTable t4 = OriginalTable.AsEnumerable().Skip(3000).Take(1000).CopyToDataTable();
        //DataTable t5 = OriginalTable.AsEnumerable().Skip(4000).Take(1000).CopyToDataTable();
        //DataTable t6 = OriginalTable.AsEnumerable().Skip(5000).Take(1000).CopyToDataTable();
        //DataTable t7 = OriginalTable.AsEnumerable().Skip(6000).Take(1000).CopyToDataTable();
        //DataTable t8 = OriginalTable.AsEnumerable().Skip(7000).Take(1000).CopyToDataTable();
        //DataTable t9 = OriginalTable.AsEnumerable().Skip(8000).Take(1000).CopyToDataTable();
        //DataTable t10 = OriginalTable.AsEnumerable().Skip(9000).Take(1000).CopyToDataTable();
        //DataTable t11 = OriginalTable.AsEnumerable().Skip(10000).Take(1000).CopyToDataTable();
        //DataTable[] vardt = {t1,t2};
        //DataTable[] vardt = { t1 };
        DataTable[] vardt = { t2 };
        //DataTable[] vardt = { t3 };
        //DataTable[] vardt = { t4 };
        //DataTable[] vardt = { t5 };
        //DataTable[] vardt = { t6 };
        //DataTable[] vardt = { t7 };
        //DataTable[] vardt = { t8 };
        //DataTable[] vardt = { t9 };
        //DataTable[] vardt = { t10 };
        //DataTable[] vardt = { t11 };

        foreach (var i in vardt)
        {
            var dt1 = i;
            DataTable dt = new DataTable();
            List<DepotMaster> listDepotMaster = new List<DepotMaster>();
            dt = dt1;
            int j = dt.Rows.Count;
            int z = 0;
            for (int x = 0; x <= j - 1; x++)
            {

                DepotMaster ObjDepotMaster = new DepotMaster();
                ObjDepotMaster.transaction_reference_id = Convert.ToString(dt.Rows[x]["transaction_reference_id"]);
                ObjDepotMaster.lgd_state_code = Convert.ToInt32(dt.Rows[x]["lgd_state_code"].ToString());
                ObjDepotMaster.lgd_district_code = Convert.ToInt32(dt.Rows[x]["lgd_district_code"]);
                ObjDepotMaster.lgd_subdistrict_code = Convert.ToInt32(dt.Rows[x]["lgd_subdistrict_code"]);
                ObjDepotMaster.lgd_block_code = Convert.ToInt32(dt.Rows[x]["lgd_block_code"]);
                ObjDepotMaster.lgd_village_code = Convert.ToInt32(dt.Rows[x]["lgd_village_code"]);
                ObjDepotMaster.depot_status = Convert.ToInt32(dt.Rows[x]["depot_status"]);
                ObjDepotMaster.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjDepotMaster.depot_name = Convert.ToString(dt.Rows[x]["depot_name"]);
                ObjDepotMaster.ownership_type_id = Convert.ToInt32(dt.Rows[x]["ownership_type_id"]);
                ObjDepotMaster.ownership_group_type_id = Convert.ToInt32(dt.Rows[x]["ownership_group_type_id"]);
                ObjDepotMaster.depot_address = Convert.ToString(dt.Rows[x]["depot_address"]);
                ObjDepotMaster.depot_pin = Convert.ToInt32(dt.Rows[x]["depot_pin"]);
                ObjDepotMaster.latitude = Convert.ToDouble(dt.Rows[x]["latitude"]);
                ObjDepotMaster.longitude = Convert.ToDouble(dt.Rows[x]["longitude"]);
                ObjDepotMaster.declared_capacity = Convert.ToDouble(dt.Rows[x]["declared_capacity"]);
                ObjDepotMaster.predominant_storage_id = Convert.ToInt32(dt.Rows[x]["predominant_storage_id"]);
                ObjDepotMaster.surveillance_cameras = Convert.ToInt32(dt.Rows[x]["surveillance_cameras"]);
                DateTime varCamera_functional_since = DateTime.Parse(Convert.ToString(dt.Rows[x]["camera_functional_since"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.camera_functional_since = varCamera_functional_since.ToString("yyyy-MM-dd");//varCamera_functional_since.ToString("dd-MM-yyyy"); //
                ObjDepotMaster.no_of_cameras = Convert.ToInt32(dt.Rows[x]["no_of_cameras"]);
                ObjDepotMaster.crop_year = Convert.ToString(dt.Rows[x]["crop_year"]);
                DateTime varhired_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["hired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.hired_date = varhired_date.ToString("yyyy-MM-dd");//varhired_date.ToString("dd-MM-yyyy"); //
                DateTime vardehired_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["dehired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.dehired_date = vardehired_date.ToString("yyyy-MM-dd");//vardehired_date.ToString("dd-MM-yyyy"); // 
                ObjDepotMaster.data_status = Convert.ToInt32(dt.Rows[x]["data_status"]);
                listDepotMaster.Add(ObjDepotMaster);
            }
            //z = z + 1;

            //while (x == dt.Rows.Count)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                Context.Response.Write(js.Serialize(listDepotMaster));
                //z = 0;
            }
        }

    }

    [WebMethod]
    public void MinimumStorageSpecificationsMSSByCropYear(string username, string password, string DataDate, string CropYear)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        if (WarehouseApiSecurity.AreCredentialsValid(username, password, "FciCfspUsername", "FciCfspPassword"))
        {
            DateTime FromDate = DateTime.ParseExact(DataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture); //Convert.ToDateTime(DataDate);// = "2023-11-03";//DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
            //string ToDate = "2023-11-04";//DateTime.Now.AddDays(-139).ToString("yyyy-MM-dd");
            List<MinimumStorageSpecifications> listMSSTrimmed = new List<MinimumStorageSpecifications>();
            List<MinimumStorageSpecifications.MSSChildQuantityIssued> listMSSChildQuantityIssued = new List<MinimumStorageSpecifications.MSSChildQuantityIssued>();
            List<MinimumStorageSpecifications.MSSChildQuantityDispatched> listMSSChildQuantityDispatched = new List<MinimumStorageSpecifications.MSSChildQuantityDispatched>();
            List<MinimumStorageSpecifications.MSSChildQuantityReceived> listMSSChildQuantityReceived = new List<MinimumStorageSpecifications.MSSChildQuantityReceived>();
            con.ConnectionString = str;
            con.Open();
            //cmd = new SqlCommand("usp_API_MSSTrimmed_FullDataWithCreatedDate", con);
            //cmd = new SqlCommand("usp_CFSP_MSS", con);
            //cmd = new SqlCommand("usp_CFSP_MSSDataByDate", con);
            cmd = new SqlCommand("usp_CFSP_MSSDataByDate_ByCropYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataDate", FromDate);
            //cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@CropYear", CropYear);
            cmd.CommandTimeout = 200;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MinimumStorageSpecifications ObjMSSTrimmed = new MinimumStorageSpecifications();
                ObjMSSTrimmed.lgd_state_code = Convert.ToInt32(dr["lgd_state_code"]);
                ObjMSSTrimmed.lgd_district_code = Convert.ToInt32(dr["lgd_district_code"]);
                ObjMSSTrimmed.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
                ObjMSSTrimmed.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
                ObjMSSTrimmed.lgd_village_code = Convert.ToInt32(dr["lgd_village_code"]);
                //ObjMSSTrimmed.data_date = Convert.ToString(dr["data_date"]);
                DateTime var_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSTrimmed.data_date = var_data_date.ToString("yyyy-MM-dd");
                ObjMSSTrimmed.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSTrimmed.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSTrimmed.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSTrimmed.opening_balance = Convert.ToDouble(dr["opening_balance"]);
                ObjMSSTrimmed.closing_balance = Convert.ToDouble(dr["closing_balance"]);
                ObjMSSTrimmed.storage_loss = Convert.ToDouble(dr["storage_loss"]);
                ObjMSSTrimmed.storage_gain = Convert.ToDouble(dr["storage_gain"]);
                ObjMSSTrimmed.transaction_reference_id = Convert.ToString(dr["transaction_reference_id"]);
                ObjMSSTrimmed.crop_year = Convert.ToString(dr["crop_year"]);
                ObjMSSTrimmed.data_status = Convert.ToDouble(dr["data_status"]);
                //listMSSTrimmed.Add(ObjMSSTrimmed);

                /**************************MSS Trimmed Child Issue********************************/
                MinimumStorageSpecifications.MSSChildQuantityIssued ObjMSSIssue = new MinimumStorageSpecifications.MSSChildQuantityIssued();
                //ObjMSSIssue.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime var_Issue_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSIssue.data_date = var_Issue_data_date.ToString("yyyy-MM-dd");
                ObjMSSIssue.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSIssue.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSIssue.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSIssue.pool_stock_id = Convert.ToInt32(dr["pool_stock_id"]);
                //ObjMSSIssue.crop_year = Convert.ToString(dr["crop_year"]);
                ObjMSSIssue.categorization_type_id = Convert.ToInt32(dr["categorization_type_id"]);
                ObjMSSIssue.upgradable = Convert.ToInt32(dr["upgradable"]);
                ObjMSSIssue.net_quantity_issued = Convert.ToDouble(dr["net_quantity_issued"]);
                ObjMSSIssue.issue_scheme_id = Convert.ToString(dr["issue_scheme_id"]);
                ObjMSSIssue.scheme_id = Convert.ToString(dr["scheme_id"]);
                ObjMSSIssue.scheme_group_id = Convert.ToString(dr["scheme_group_id"]);
                ObjMSSIssue.scheme_name = Convert.ToString(dr["scheme_name"]);
                ObjMSSIssue.issue_destination_id = Convert.ToInt32(dr["issue_destination_id"]);
                //listMSSChildQuantityIssued.Add(ObjMSSIssue);

                /**************************MSS Trimmed Child Dispatch********************************/
                MinimumStorageSpecifications.MSSChildQuantityDispatched ObjMSSDispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched();
                //ObjMSSDispatch.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varDispatch_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSDispatch.data_date = varDispatch_data_date.ToString("yyyy-MM-dd");
                ObjMSSDispatch.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSDispatch.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSDispatch.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSDispatch.net_quantity_dispatched = Convert.ToDouble(dr["net_quantity_dispatched"]);
                ObjMSSDispatch.dispatch_destination_id = Convert.ToInt32(dr["dispatch_destination_id"]);
                //listMSSChildQuantityDispatched.Add(ObjMSSDispatch);

                /**************************MSS Trimmed Child Receive********************************/
                MinimumStorageSpecifications.MSSChildQuantityReceived ObjMSSReceive = new MinimumStorageSpecifications.MSSChildQuantityReceived();

                //ObjMSSReceive.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varReceive_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSReceive.data_date = varReceive_data_date.ToString("yyyy-MM-dd");
                ObjMSSReceive.depot_code = Convert.ToString(dr["depot_code"]);
                ObjMSSReceive.commodity_id = Convert.ToString(dr["commodity_id"]);
                ObjMSSReceive.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
                ObjMSSReceive.inflow_source_id = Convert.ToInt32(dr["inflow_source_id"]);
                ObjMSSReceive.net_quantity_received = Convert.ToDouble(dr["net_quantity_received"]);
                //listMSSChildQuantityReceived.Add(ObjMSSReceive);
                /************************************************************************************/
                ObjMSSTrimmed.mss_issue = new MinimumStorageSpecifications.MSSChildQuantityIssued[] { ObjMSSIssue };
                ObjMSSTrimmed.mss_dispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched[] { ObjMSSDispatch };
                ObjMSSTrimmed.mss_received = new MinimumStorageSpecifications.MSSChildQuantityReceived[] { ObjMSSReceive };

                listMSSTrimmed.Add(ObjMSSTrimmed);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            Context.Response.Write(js.Serialize(listMSSTrimmed));
            //File.WriteAllText(@"D:\MSSData.json", js.Serialize(listMSSTrimmed));
            //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"D:\MSSData1.json"), js.Serialize(listMSSTrimmed));
            //string result = Path.GetTempPath();
            //Console.WriteLine(result);
            //System.IO.File.WriteAllText(Server.MapPath(result), js.Serialize(listMSSTrimmed));
            //System.IO.File.WriteAllText(result, js.Serialize(listMSSTrimmed));
            /**************For submitting /posting data to CFSP********************/
            /**
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cfsp.nic.in/cfsp/mss/MPWLC");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonDataString = js.Serialize(listMSSTrimmed);
                streamWriter.Write(jsonDataString);
            }
			**/
        }
        else
        {
            Context.Response.Write("Credentials Invalid");
        }

    }


    public static List<DataTable> Testes(DataTable yourDataTableToSplit, int numberOfDatatablesYouWant)
    {
        List<DataRow> rows = yourDataTableToSplit.AsEnumerable().ToList();
        List<DataTable> result = new List<DataTable>();
        int rowsToTake = yourDataTableToSplit.Rows.Count / numberOfDatatablesYouWant;
        while (rows.Count > 0)
        {
            result.Add(rows.Take(rowsToTake).CopyToDataTable());
            rows = rows.Skip(rowsToTake).ToList();
        }
        return result;
    }

    /**Data Table Splitter Class**/
    public abstract class DataTableSplitter
    {
        protected DataTable SourceTable;
        protected int BatchSize;
        protected string TableNamePrefix;

        protected DataTableSplitter(DataTable table, int size, string namePrefix = "")
        {
            SourceTable = table;
            BatchSize = size;
            TableNamePrefix = namePrefix;
        }

        protected DataTable NewSplitTable(int tableCount)
        {
            DataTable dataTable = SourceTable.Clone();
            dataTable.TableName = !string.IsNullOrEmpty(TableNamePrefix)
                ? TableNamePrefix + "_" + tableCount
                : String.Empty;
            dataTable.Clear();
            return dataTable;
        }

        public abstract IEnumerable<DataTable> Split();
    }

    //****Data Table Regular Splitter Class****//
    public class DataTableRegularSplitter : DataTableSplitter
    {
        public DataTableRegularSplitter
         (DataTable table, int size, string namePrefix = "") : base(table, size, namePrefix)
        {
        }

        public override IEnumerable<DataTable> Split()
        {
            int tableCount = 0;
            int rowCount = 0;
            DataTable dataTable = null;

            foreach (DataRow row in SourceTable.Rows)
            {
                /*create new table*/
                if (dataTable == null)
                {
                    tableCount++;
                    dataTable = NewSplitTable(tableCount);
                }

                /*row add to new table/remove from source*/
                DataRow newRow = dataTable.NewRow();
                newRow.ItemArray = row.ItemArray;
                dataTable.Rows.Add(newRow);
                rowCount++;

                /*return batch sized new table*/
                if (rowCount == BatchSize)
                {
                    yield return dataTable;

                    rowCount = 0;
                    dataTable = null;
                }
            }

            /*return new table with remaining rows*/
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                yield return dataTable;
            }
        }
    }

    //******************************************//
    public static IEnumerable<DataTable>
           Split(DataTable sourceTable, int batchSize, string tableNamePrefix = "")
    {
        return new DataTableRegularSplitter(sourceTable, batchSize, tableNamePrefix).Split();
    }

    //******************************************//

    [WebMethod]
    public void MSSDataTableToJson(string DataDate, string CropYear)
    {
        DataTable OriginalTable = new DataTable();
        OriginalTable = GetMSSData(DataDate, CropYear);
        DataTable t1 = OriginalTable.AsEnumerable().Skip(0).Take(1000).CopyToDataTable();
        DataTable t2 = OriginalTable.AsEnumerable().Skip(1000).Take(1000).CopyToDataTable();
        DataTable t3 = OriginalTable.AsEnumerable().Skip(2000).Take(1000).CopyToDataTable();
        DataTable t4 = OriginalTable.AsEnumerable().Skip(3000).Take(1000).CopyToDataTable();
        //DataTable t5 = OriginalTable.AsEnumerable().Skip(4000).Take(1000).CopyToDataTable();
        //DataTable t6 = OriginalTable.AsEnumerable().Skip(5000).Take(1000).CopyToDataTable();
        //DataTable t7 = OriginalTable.AsEnumerable().Skip(6000).Take(1000).CopyToDataTable();
        //DataTable t8 = OriginalTable.AsEnumerable().Skip(7000).Take(1000).CopyToDataTable();
        //DataTable t9 = OriginalTable.AsEnumerable().Skip(8000).Take(1000).CopyToDataTable();
        //DataTable t10 = OriginalTable.AsEnumerable().Skip(9000).Take(1000).CopyToDataTable();
        //DataTable t11 = OriginalTable.AsEnumerable().Skip(10000).Take(1000).CopyToDataTable();
        //DataTable[] vardt = { t1, t2 };
        DataTable[] vardt = { t1 };
        //DataTable[] vardt = { t2 };
        //DataTable[] vardt = { t3 };
        //DataTable[] vardt = { t4 };
        //DataTable[] vardt = { t5 };
        //DataTable[] vardt = { t6 };
        //DataTable[] vardt = { t7 };
        //DataTable[] vardt = { t8 };
        //DataTable[] vardt = { t9 };
        //DataTable[] vardt = { t10 };
        //DataTable[] vardt = { t11 };

        foreach (var i in vardt)
        {
            var dt1 = i;
            DataTable dt = new DataTable();
            List<MinimumStorageSpecifications> listMSSTrimmed = new List<MinimumStorageSpecifications>();
            List<MinimumStorageSpecifications.MSSChildQuantityIssued> listMSSChildQuantityIssued = new List<MinimumStorageSpecifications.MSSChildQuantityIssued>();
            List<MinimumStorageSpecifications.MSSChildQuantityDispatched> listMSSChildQuantityDispatched = new List<MinimumStorageSpecifications.MSSChildQuantityDispatched>();
            List<MinimumStorageSpecifications.MSSChildQuantityReceived> listMSSChildQuantityReceived = new List<MinimumStorageSpecifications.MSSChildQuantityReceived>();
            dt = dt1;
            int j = dt.Rows.Count;
            int z = 0;
            for (int x = 0; x <= j - 1; x++)
            {
                MinimumStorageSpecifications ObjMSSTrimmed = new MinimumStorageSpecifications();
                ObjMSSTrimmed.lgd_state_code = Convert.ToInt32(dt.Rows[x]["lgd_state_code"]);
                ObjMSSTrimmed.lgd_district_code = Convert.ToInt32(dt.Rows[x]["lgd_district_code"]);
                ObjMSSTrimmed.lgd_subdistrict_code = Convert.ToInt32(dt.Rows[x]["lgd_subdistrict_code"]);
                ObjMSSTrimmed.lgd_block_code = Convert.ToInt32(dt.Rows[x]["lgd_block_code"]);
                ObjMSSTrimmed.lgd_village_code = Convert.ToInt32(dt.Rows[x]["lgd_village_code"]);
                //ObjMSSTrimmed.data_date = Convert.ToString(dr["data_date"]);
                DateTime var_data_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSTrimmed.data_date = var_data_date.ToString("yyyy-MM-dd");
                ObjMSSTrimmed.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjMSSTrimmed.commodity_id = Convert.ToString(dt.Rows[x]["commodity_id"]);
                ObjMSSTrimmed.commodity_group_id = Convert.ToString(dt.Rows[x]["commodity_group_id"]);
                ObjMSSTrimmed.opening_balance = Convert.ToDouble(dt.Rows[x]["opening_balance"]);
                ObjMSSTrimmed.closing_balance = Convert.ToDouble(dt.Rows[x]["closing_balance"]);
                ObjMSSTrimmed.storage_loss = Convert.ToDouble(dt.Rows[x]["storage_loss"]);
                ObjMSSTrimmed.storage_gain = Convert.ToDouble(dt.Rows[x]["storage_gain"]);
                ObjMSSTrimmed.transaction_reference_id = Convert.ToString(dt.Rows[x]["transaction_reference_id"]);
                ObjMSSTrimmed.crop_year = Convert.ToString(dt.Rows[x]["crop_year"]);
                ObjMSSTrimmed.data_status = Convert.ToDouble(dt.Rows[x]["data_status"]);
                //listMSSTrimmed.Add(ObjMSSTrimmed);

                /**************************MSS Trimmed Child Issue********************************/
                MinimumStorageSpecifications.MSSChildQuantityIssued ObjMSSIssue = new MinimumStorageSpecifications.MSSChildQuantityIssued();
                //ObjMSSIssue.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime var_Issue_data_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSIssue.data_date = var_Issue_data_date.ToString("yyyy-MM-dd");
                ObjMSSIssue.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjMSSIssue.commodity_id = Convert.ToString(dt.Rows[x]["commodity_id"]);
                ObjMSSIssue.commodity_group_id = Convert.ToString(dt.Rows[x]["commodity_group_id"]);
                ObjMSSIssue.pool_stock_id = Convert.ToInt32(dt.Rows[x]["pool_stock_id"]);
                //ObjMSSIssue.crop_year = Convert.ToString(dt.Rows[x]["crop_year"]);
                ObjMSSIssue.categorization_type_id = Convert.ToInt32(dt.Rows[x]["categorization_type_id"]);
                ObjMSSIssue.upgradable = Convert.ToInt32(dt.Rows[x]["upgradable"]);
                ObjMSSIssue.net_quantity_issued = Convert.ToDouble(dt.Rows[x]["net_quantity_issued"]);
                ObjMSSIssue.issue_scheme_id = Convert.ToString(dt.Rows[x]["issue_scheme_id"]);
                ObjMSSIssue.scheme_id = Convert.ToString(dt.Rows[x]["scheme_id"]);
                ObjMSSIssue.scheme_group_id = Convert.ToString(dt.Rows[x]["scheme_group_id"]);
                ObjMSSIssue.scheme_name = Convert.ToString(dt.Rows[x]["scheme_name"]);
                ObjMSSIssue.issue_destination_id = Convert.ToInt32(dt.Rows[x]["issue_destination_id"]);
                //listMSSChildQuantityIssued.Add(ObjMSSIssue);

                /**************************MSS Trimmed Child Dispatch********************************/
                MinimumStorageSpecifications.MSSChildQuantityDispatched ObjMSSDispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched();
                //ObjMSSDispatch.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varDispatch_data_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSDispatch.data_date = varDispatch_data_date.ToString("yyyy-MM-dd");
                ObjMSSDispatch.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjMSSDispatch.commodity_id = Convert.ToString(dt.Rows[x]["commodity_id"]);
                ObjMSSDispatch.commodity_group_id = Convert.ToString(dt.Rows[x]["commodity_group_id"]);
                ObjMSSDispatch.net_quantity_dispatched = Convert.ToDouble(dt.Rows[x]["net_quantity_dispatched"]);
                ObjMSSDispatch.dispatch_destination_id = Convert.ToInt32(dt.Rows[x]["dispatch_destination_id"]);
                //listMSSChildQuantityDispatched.Add(ObjMSSDispatch);

                /**************************MSS Trimmed Child Receive********************************/
                MinimumStorageSpecifications.MSSChildQuantityReceived ObjMSSReceive = new MinimumStorageSpecifications.MSSChildQuantityReceived();

                //ObjMSSReceive.data_date = Convert.ToDateTime(dr["data_date"]);
                DateTime varReceive_data_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["data_date"]), CultureInfo.InvariantCulture);
                ObjMSSReceive.data_date = varReceive_data_date.ToString("yyyy-MM-dd");
                ObjMSSReceive.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjMSSReceive.commodity_id = Convert.ToString(dt.Rows[x]["commodity_id"]);
                ObjMSSReceive.commodity_group_id = Convert.ToString(dt.Rows[x]["commodity_group_id"]);
                ObjMSSReceive.inflow_source_id = Convert.ToInt32(dt.Rows[x]["inflow_source_id"]);
                ObjMSSReceive.net_quantity_received = Convert.ToDouble(dt.Rows[x]["net_quantity_received"]);
                //listMSSChildQuantityReceived.Add(ObjMSSReceive);
                /************************************************************************************/
                ObjMSSTrimmed.mss_issue = new MinimumStorageSpecifications.MSSChildQuantityIssued[] { ObjMSSIssue };
                ObjMSSTrimmed.mss_dispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched[] { ObjMSSDispatch };
                ObjMSSTrimmed.mss_received = new MinimumStorageSpecifications.MSSChildQuantityReceived[] { ObjMSSReceive };
                listMSSTrimmed.Add(ObjMSSTrimmed);
            }
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                Context.Response.Write(js.Serialize(listMSSTrimmed));

            }
        }

    }


    private static DataTable GetMSSData(string DataDate, string CropYear)
    {
        string conString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string query = "usp_CFSP_MSSDataByDate_ByCropYear";
        DateTime FromDate = DateTime.ParseExact(DataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture); //Convert.ToDateTime(DataDate);// = "2023-11-03";//DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
        using (SqlConnection con = new SqlConnection(conString))
        {

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataDate", FromDate);
            cmd.Parameters.AddWithValue("@CropYear", CropYear);
            cmd.CommandTimeout = 200;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }
    /**
    [WebMethod]
    public static string GetBearerToken()
    {
        string resultContent = "";

        resultContent= Login();
        return Context.Response.Write(resultContent);
    }

    //async Task Login()
    async Task Login()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://cfsp.nic.in/cfsp/login");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", WarehouseApiSecurity.GetRequiredSetting("FciCfspExternalUsername")),
                new KeyValuePair<string, string>("password", WarehouseApiSecurity.GetRequiredSetting("FciCfspExternalPassword"))
            });

            var result = await client.PostAsync("sign_in", content);
            var token = result.Headers.GetValues("access-token").FirstOrDefault();
            string resultContent = await result.Content.ReadAsStringAsync();
            //Console.WriteLine(resultContent);
            //Context.Response.Write(resultContent);
        }
        //return Context.Response.Write(resultContent);
    }**/


    /************************************/
    [WebMethod]
    public void DataTableToJsonHundred()
    {
        DataTable OriginalTable = new DataTable();
        OriginalTable = GetData();
        //OriginalTable.Skip(0).Take(100);
        //OriginalTable.Skip(100).Take(100);
        //OriginalTable.Skip(200).Take(100);
        //OriginalTable.Skip(300).Take(100);
        /****First Thousand*****/
        DataTable t1 = OriginalTable.AsEnumerable().Skip(0).Take(100).CopyToDataTable();
        DataTable t2 = OriginalTable.AsEnumerable().Skip(100).Take(100).CopyToDataTable();
        DataTable t3 = OriginalTable.AsEnumerable().Skip(200).Take(100).CopyToDataTable();
        DataTable t4 = OriginalTable.AsEnumerable().Skip(300).Take(100).CopyToDataTable();
        DataTable t5 = OriginalTable.AsEnumerable().Skip(400).Take(100).CopyToDataTable();
        DataTable t6 = OriginalTable.AsEnumerable().Skip(500).Take(100).CopyToDataTable();
        //DataTable t7 = OriginalTable.AsEnumerable().Skip(600).Take(100).CopyToDataTable();
        //DataTable t8 = OriginalTable.AsEnumerable().Skip(700).Take(100).CopyToDataTable();
        //DataTable t9 = OriginalTable.AsEnumerable().Skip(800).Take(100).CopyToDataTable();
        //DataTable t10 = OriginalTable.AsEnumerable().Skip(900).Take(100).CopyToDataTable();
        ////DataTable t11 = OriginalTable.AsEnumerable().Skip(10000).Take(1000).CopyToDataTable();
        ///*********Second Thousand*******/
        //DataTable t11 = OriginalTable.AsEnumerable().Skip(1000).Take(100).CopyToDataTable();
        //DataTable t12 = OriginalTable.AsEnumerable().Skip(1100).Take(100).CopyToDataTable();
        //DataTable t13 = OriginalTable.AsEnumerable().Skip(1200).Take(100).CopyToDataTable();
        //DataTable t14 = OriginalTable.AsEnumerable().Skip(1300).Take(100).CopyToDataTable();
        //DataTable t15 = OriginalTable.AsEnumerable().Skip(1400).Take(100).CopyToDataTable();
        //DataTable t16 = OriginalTable.AsEnumerable().Skip(1500).Take(100).CopyToDataTable();
        //DataTable t17 = OriginalTable.AsEnumerable().Skip(1600).Take(100).CopyToDataTable();
        //DataTable t18 = OriginalTable.AsEnumerable().Skip(1700).Take(100).CopyToDataTable();
        //DataTable t19 = OriginalTable.AsEnumerable().Skip(1800).Take(100).CopyToDataTable();
        //DataTable t20 = OriginalTable.AsEnumerable().Skip(1900).Take(100).CopyToDataTable();
        ///**Third Thousand**/
        //DataTable t21 = OriginalTable.AsEnumerable().Skip(2000).Take(100).CopyToDataTable();
        //DataTable t22 = OriginalTable.AsEnumerable().Skip(2100).Take(100).CopyToDataTable();
        //DataTable t23 = OriginalTable.AsEnumerable().Skip(2200).Take(100).CopyToDataTable();
        //DataTable t24 = OriginalTable.AsEnumerable().Skip(2300).Take(100).CopyToDataTable();
        //DataTable t25 = OriginalTable.AsEnumerable().Skip(2400).Take(100).CopyToDataTable();
        //DataTable t26 = OriginalTable.AsEnumerable().Skip(2500).Take(100).CopyToDataTable();
        //DataTable t27 = OriginalTable.AsEnumerable().Skip(2600).Take(100).CopyToDataTable();
        //DataTable t28 = OriginalTable.AsEnumerable().Skip(2700).Take(100).CopyToDataTable();
        //DataTable t29 = OriginalTable.AsEnumerable().Skip(2800).Take(100).CopyToDataTable();
        //DataTable t30 = OriginalTable.AsEnumerable().Skip(2900).Take(100).CopyToDataTable();
        ///**Four Thousand**/
        //DataTable t31 = OriginalTable.AsEnumerable().Skip(3000).Take(100).CopyToDataTable();
        //DataTable t32 = OriginalTable.AsEnumerable().Skip(3100).Take(100).CopyToDataTable();
        //DataTable t33 = OriginalTable.AsEnumerable().Skip(3200).Take(100).CopyToDataTable();
        //DataTable t34 = OriginalTable.AsEnumerable().Skip(3300).Take(100).CopyToDataTable();
        //DataTable t35 = OriginalTable.AsEnumerable().Skip(3400).Take(100).CopyToDataTable();
        //DataTable t36 = OriginalTable.AsEnumerable().Skip(3500).Take(100).CopyToDataTable();
        //DataTable t37 = OriginalTable.AsEnumerable().Skip(3600).Take(100).CopyToDataTable();
        //DataTable t38 = OriginalTable.AsEnumerable().Skip(3700).Take(100).CopyToDataTable();
        //DataTable t39 = OriginalTable.AsEnumerable().Skip(3800).Take(100).CopyToDataTable();
        //DataTable t40 = OriginalTable.AsEnumerable().Skip(3900).Take(100).CopyToDataTable();
        ///**Five Thousand**/
        //DataTable t41 = OriginalTable.AsEnumerable().Skip(4000).Take(100).CopyToDataTable();
        //DataTable t42 = OriginalTable.AsEnumerable().Skip(4100).Take(100).CopyToDataTable();
        //DataTable t43 = OriginalTable.AsEnumerable().Skip(4200).Take(100).CopyToDataTable();
        //DataTable t44 = OriginalTable.AsEnumerable().Skip(4300).Take(100).CopyToDataTable();
        //DataTable t45 = OriginalTable.AsEnumerable().Skip(4400).Take(100).CopyToDataTable();
        //DataTable t46 = OriginalTable.AsEnumerable().Skip(4500).Take(100).CopyToDataTable();
        //DataTable t47 = OriginalTable.AsEnumerable().Skip(4600).Take(100).CopyToDataTable();
        //DataTable t48 = OriginalTable.AsEnumerable().Skip(4700).Take(100).CopyToDataTable();
        //DataTable t49 = OriginalTable.AsEnumerable().Skip(4800).Take(100).CopyToDataTable();
        //DataTable t50 = OriginalTable.AsEnumerable().Skip(4900).Take(100).CopyToDataTable();

        ///**Six Thousand**/
        //DataTable t51 = OriginalTable.AsEnumerable().Skip(5000).Take(100).CopyToDataTable();
        //DataTable t52 = OriginalTable.AsEnumerable().Skip(5100).Take(100).CopyToDataTable();
        //DataTable t53 = OriginalTable.AsEnumerable().Skip(5200).Take(100).CopyToDataTable();
        //DataTable t54 = OriginalTable.AsEnumerable().Skip(5300).Take(100).CopyToDataTable();
        //DataTable t55 = OriginalTable.AsEnumerable().Skip(5400).Take(100).CopyToDataTable();
        //DataTable t56 = OriginalTable.AsEnumerable().Skip(5500).Take(100).CopyToDataTable();
        //DataTable t57 = OriginalTable.AsEnumerable().Skip(5600).Take(100).CopyToDataTable();
        //DataTable t58 = OriginalTable.AsEnumerable().Skip(5700).Take(100).CopyToDataTable();
        //DataTable t59 = OriginalTable.AsEnumerable().Skip(5800).Take(100).CopyToDataTable();
        //DataTable t60 = OriginalTable.AsEnumerable().Skip(5900).Take(100).CopyToDataTable();

        ///**Seven Thousand**/
        //DataTable t61 = OriginalTable.AsEnumerable().Skip(6000).Take(100).CopyToDataTable();
        //DataTable t62 = OriginalTable.AsEnumerable().Skip(6100).Take(100).CopyToDataTable();
        //DataTable t63 = OriginalTable.AsEnumerable().Skip(6200).Take(100).CopyToDataTable();
        //DataTable t64 = OriginalTable.AsEnumerable().Skip(6300).Take(100).CopyToDataTable();
        //DataTable t65 = OriginalTable.AsEnumerable().Skip(6400).Take(100).CopyToDataTable();
        //DataTable t66 = OriginalTable.AsEnumerable().Skip(6500).Take(100).CopyToDataTable();
        //DataTable t67 = OriginalTable.AsEnumerable().Skip(6600).Take(100).CopyToDataTable();
        //DataTable t68 = OriginalTable.AsEnumerable().Skip(6700).Take(100).CopyToDataTable();
        //DataTable t69 = OriginalTable.AsEnumerable().Skip(6800).Take(100).CopyToDataTable();
        //DataTable t70 = OriginalTable.AsEnumerable().Skip(6900).Take(100).CopyToDataTable();


        ///**Eight Thousand**/
        //DataTable t71 = OriginalTable.AsEnumerable().Skip(7000).Take(100).CopyToDataTable();
        //DataTable t72 = OriginalTable.AsEnumerable().Skip(7100).Take(100).CopyToDataTable();
        //DataTable t73 = OriginalTable.AsEnumerable().Skip(7200).Take(100).CopyToDataTable();
        //DataTable t74 = OriginalTable.AsEnumerable().Skip(7300).Take(100).CopyToDataTable();
        //DataTable t75 = OriginalTable.AsEnumerable().Skip(7400).Take(100).CopyToDataTable();
        //DataTable t76 = OriginalTable.AsEnumerable().Skip(7500).Take(100).CopyToDataTable();
        //DataTable t77 = OriginalTable.AsEnumerable().Skip(7600).Take(100).CopyToDataTable();
        //DataTable t78 = OriginalTable.AsEnumerable().Skip(7700).Take(100).CopyToDataTable();
        //DataTable t79 = OriginalTable.AsEnumerable().Skip(7800).Take(100).CopyToDataTable();
        //DataTable t80 = OriginalTable.AsEnumerable().Skip(7900).Take(100).CopyToDataTable();

        ///**Nine Thousand**/
        //DataTable t81 = OriginalTable.AsEnumerable().Skip(8000).Take(100).CopyToDataTable();
        //DataTable t82 = OriginalTable.AsEnumerable().Skip(8100).Take(100).CopyToDataTable();
        //DataTable t83 = OriginalTable.AsEnumerable().Skip(8200).Take(100).CopyToDataTable();
        //DataTable t84 = OriginalTable.AsEnumerable().Skip(8300).Take(100).CopyToDataTable();
        //DataTable t85 = OriginalTable.AsEnumerable().Skip(8400).Take(100).CopyToDataTable();
        //DataTable t86 = OriginalTable.AsEnumerable().Skip(8500).Take(100).CopyToDataTable();
        //DataTable t87 = OriginalTable.AsEnumerable().Skip(8600).Take(100).CopyToDataTable();
        //DataTable t88 = OriginalTable.AsEnumerable().Skip(8700).Take(100).CopyToDataTable();
        //DataTable t89 = OriginalTable.AsEnumerable().Skip(8800).Take(100).CopyToDataTable();
        //DataTable t90 = OriginalTable.AsEnumerable().Skip(8900).Take(100).CopyToDataTable();

        ///**Ten Thousand**/
        //DataTable t91 = OriginalTable.AsEnumerable().Skip(9000).Take(100).CopyToDataTable();
        //DataTable t92 = OriginalTable.AsEnumerable().Skip(9100).Take(100).CopyToDataTable();
        //DataTable t93 = OriginalTable.AsEnumerable().Skip(9200).Take(100).CopyToDataTable();
        //DataTable t94 = OriginalTable.AsEnumerable().Skip(9300).Take(100).CopyToDataTable();
        //DataTable t95 = OriginalTable.AsEnumerable().Skip(9400).Take(100).CopyToDataTable();
        //DataTable t96 = OriginalTable.AsEnumerable().Skip(9500).Take(100).CopyToDataTable();
        //DataTable t97 = OriginalTable.AsEnumerable().Skip(9600).Take(100).CopyToDataTable();
        //DataTable t98 = OriginalTable.AsEnumerable().Skip(9700).Take(100).CopyToDataTable();
        //DataTable t99 = OriginalTable.AsEnumerable().Skip(9800).Take(100).CopyToDataTable();
        //DataTable t100 = OriginalTable.AsEnumerable().Skip(9900).Take(100).CopyToDataTable();

        ///**Eleven Thousand**/
        //DataTable t101 = OriginalTable.AsEnumerable().Skip(10000).Take(100).CopyToDataTable();
        //DataTable t102 = OriginalTable.AsEnumerable().Skip(10100).Take(100).CopyToDataTable();
        //DataTable t103 = OriginalTable.AsEnumerable().Skip(10200).Take(100).CopyToDataTable();
        //DataTable t104 = OriginalTable.AsEnumerable().Skip(10300).Take(100).CopyToDataTable();
        //DataTable t105 = OriginalTable.AsEnumerable().Skip(10400).Take(100).CopyToDataTable();
        //DataTable t106 = OriginalTable.AsEnumerable().Skip(10500).Take(100).CopyToDataTable();
        //DataTable t107 = OriginalTable.AsEnumerable().Skip(10600).Take(100).CopyToDataTable();
        //DataTable t108 = OriginalTable.AsEnumerable().Skip(10700).Take(100).CopyToDataTable();
        //DataTable t109 = OriginalTable.AsEnumerable().Skip(10800).Take(100).CopyToDataTable();
        //DataTable t110 = OriginalTable.AsEnumerable().Skip(10900).Take(100).CopyToDataTable();

        string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t206.txt";
        //DataTable[] vardt = {t1,t2};
        //DataTable[] vardt = { t1 };
        //DataTable[] vardt = { t2 };
        //DataTable[] vardt = { t3 };
        //DataTable[] vardt = { t4 };
        //DataTable[] vardt = { t5 };
        DataTable[] vardt = { t6 };
        //DataTable[] vardt = { t7 };
        //DataTable[] vardt = { t8 };
        //DataTable[] vardt = { t9 };
        //DataTable[] vardt = { t10 };

        /****/
        //DataTable[] vardt = { t11 };
        //DataTable[] vardt = { t12 };
        //DataTable[] vardt = { t13 };
        //DataTable[] vardt = { t14 };
        //DataTable[] vardt = { t15 };
        //DataTable[] vardt = { t16 };
        //DataTable[] vardt = { t17 };
        //DataTable[] vardt = { t18 };
        //DataTable[] vardt = { t19 };
        //DataTable[] vardt = { t20 };

        /*****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t40.txt";
        //DataTable[] vardt = { t21 };
        //DataTable[] vardt = { t22 };
        //DataTable[] vardt = { t23 };
        //DataTable[] vardt = { t24 };
        //DataTable[] vardt = { t25 };
        //DataTable[] vardt = { t26 };
        //DataTable[] vardt = { t27 };
        //DataTable[] vardt = { t28 };
        //DataTable[] vardt = { t29 };
        //DataTable[] vardt = { t30 };

        /****/
        //DataTable[] vardt = { t31 };
        //DataTable[] vardt = { t32 };
        //DataTable[] vardt = { t33 };
        //DataTable[] vardt = { t34 };
        //DataTable[] vardt = { t35 };
        //DataTable[] vardt = { t36 };
        //DataTable[] vardt = { t37 };
        //DataTable[] vardt = { t38 };
        //DataTable[] vardt = { t39 };
        //DataTable[] vardt = { t40 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t50.txt";
        //DataTable[] vardt = { t41 };
        //DataTable[] vardt = { t42 };
        //DataTable[] vardt = { t43 };
        //DataTable[] vardt = { t44 };
        //DataTable[] vardt = { t45 };
        //DataTable[] vardt = { t46 };
        //DataTable[] vardt = { t47 };
        //DataTable[] vardt = { t48 };
        //DataTable[] vardt = { t49 };
        //DataTable[] vardt = { t50 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t60.txt";
        //DataTable[] vardt = { t51 };
        //DataTable[] vardt = { t52 };
        //DataTable[] vardt = { t53 };
        //DataTable[] vardt = { t54 };
        //DataTable[] vardt = { t55 };
        //DataTable[] vardt = { t56 };
        //DataTable[] vardt = { t57 };
        //DataTable[] vardt = { t58 };
        //DataTable[] vardt = { t59 };
        //DataTable[] vardt = { t60 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t70.txt";
        //DataTable[] vardt = { t61 };
        //DataTable[] vardt = { t62 };
        //DataTable[] vardt = { t63 };
        //DataTable[] vardt = { t64 };
        //DataTable[] vardt = { t65 };
        //DataTable[] vardt = { t66 };
        //DataTable[] vardt = { t67 };
        //DataTable[] vardt = { t68 };
        //DataTable[] vardt = { t69 };
        //DataTable[] vardt = { t70 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t80.txt";
        //DataTable[] vardt = { t71 };
        //DataTable[] vardt = { t72 };
        //DataTable[] vardt = { t73 };
        //DataTable[] vardt = { t74 };
        //DataTable[] vardt = { t75 };
        //DataTable[] vardt = { t76 };
        //DataTable[] vardt = { t77 };
        //DataTable[] vardt = { t78 };
        //DataTable[] vardt = { t79 };
        //DataTable[] vardt = { t80 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t90.txt";
        //DataTable[] vardt = { t81 };
        //DataTable[] vardt = { t82 };
        //DataTable[] vardt = { t83 };
        //DataTable[] vardt = { t84 };
        //DataTable[] vardt = { t85 };
        //DataTable[] vardt = { t86 };
        //DataTable[] vardt = { t87 };
        //DataTable[] vardt = { t88 };
        //DataTable[] vardt = { t89 };
        //DataTable[] vardt = { t90 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t100.txt";
        //DataTable[] vardt = { t91 };
        //DataTable[] vardt = { t92 };
        //DataTable[] vardt = { t93 };
        //DataTable[] vardt = { t94 };
        //DataTable[] vardt = { t95 };
        //DataTable[] vardt = { t96 };
        //DataTable[] vardt = { t97 };
        //DataTable[] vardt = { t98 };
        //DataTable[] vardt = { t99 };
        //DataTable[] vardt = { t100 };

        /****/
        //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t106.txt";
        //DataTable[] vardt = { t101 };
        //DataTable[] vardt = { t102 };
        //DataTable[] vardt = { t103 };
        //DataTable[] vardt = { t104 };
        //DataTable[] vardt = { t105 };
        //DataTable[] vardt = { t106 };
        //DataTable[] vardt = { t107 };
        //DataTable[] vardt = { t108 };
        //DataTable[] vardt = { t109 };


        foreach (var i in vardt)
        {
            var dt1 = i;
            DataTable dt = new DataTable();
            List<DepotMaster> listDepotMaster = new List<DepotMaster>();
            dt = dt1;
            int j = dt.Rows.Count;
            int z = 0;
            for (int x = 0; x <= j - 1; x++)
            {

                DepotMaster ObjDepotMaster = new DepotMaster();
                ObjDepotMaster.transaction_reference_id = Convert.ToString(dt.Rows[x]["transaction_reference_id"]);
                ObjDepotMaster.lgd_state_code = Convert.ToInt32(dt.Rows[x]["lgd_state_code"].ToString());
                ObjDepotMaster.lgd_district_code = Convert.ToInt32(dt.Rows[x]["lgd_district_code"]);
                ObjDepotMaster.lgd_subdistrict_code = Convert.ToInt32(dt.Rows[x]["lgd_subdistrict_code"]);
                ObjDepotMaster.lgd_block_code = Convert.ToInt32(dt.Rows[x]["lgd_block_code"]);
                ObjDepotMaster.lgd_village_code = Convert.ToInt32(dt.Rows[x]["lgd_village_code"]);
                ObjDepotMaster.depot_status = Convert.ToInt32(dt.Rows[x]["depot_status"]);
                ObjDepotMaster.depot_code = Convert.ToString(dt.Rows[x]["depot_code"]);
                ObjDepotMaster.depot_name = Convert.ToString(dt.Rows[x]["depot_name"]);
                ObjDepotMaster.ownership_type_id = Convert.ToInt32(dt.Rows[x]["ownership_type_id"]);
                ObjDepotMaster.ownership_group_type_id = Convert.ToInt32(dt.Rows[x]["ownership_group_type_id"]);
                ObjDepotMaster.depot_address = Convert.ToString(dt.Rows[x]["depot_address"]);
                ObjDepotMaster.depot_pin = Convert.ToInt32(dt.Rows[x]["depot_pin"]);
                ObjDepotMaster.latitude = Convert.ToDouble(dt.Rows[x]["latitude"]);
                ObjDepotMaster.longitude = Convert.ToDouble(dt.Rows[x]["longitude"]);
                ObjDepotMaster.declared_capacity = Convert.ToDouble(dt.Rows[x]["declared_capacity"]);
                ObjDepotMaster.predominant_storage_id = Convert.ToInt32(dt.Rows[x]["predominant_storage_id"]);
                ObjDepotMaster.surveillance_cameras = Convert.ToInt32(dt.Rows[x]["surveillance_cameras"]);
                DateTime varCamera_functional_since = DateTime.Parse(Convert.ToString(dt.Rows[x]["camera_functional_since"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.camera_functional_since = varCamera_functional_since.ToString("yyyy-MM-dd");//varCamera_functional_since.ToString("dd-MM-yyyy"); //
                ObjDepotMaster.no_of_cameras = Convert.ToInt32(dt.Rows[x]["no_of_cameras"]);
                ObjDepotMaster.crop_year = Convert.ToString(dt.Rows[x]["crop_year"]);
                DateTime varhired_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["hired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.hired_date = varhired_date.ToString("yyyy-MM-dd");//varhired_date.ToString("dd-MM-yyyy"); //
                DateTime vardehired_date = DateTime.Parse(Convert.ToString(dt.Rows[x]["dehired_date"]), CultureInfo.InvariantCulture);
                ObjDepotMaster.dehired_date = vardehired_date.ToString("yyyy-MM-dd");//vardehired_date.ToString("dd-MM-yyyy"); // 
                ObjDepotMaster.data_status = Convert.ToInt32(dt.Rows[x]["data_status"]);
                listDepotMaster.Add(ObjDepotMaster);
            }
            //z = z + 1;

            //while (x == dt.Rows.Count)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                Context.Response.Write(js.Serialize(listDepotMaster));
                //z = 0;
                //Context.Response.Write(js.Serialize(listMSSTrimmed));
                //File.WriteAllText(@"D:\CFSP-FCI\23082024\DepotMaster\Data\t1.txt", js.Serialize(listDepotMaster));
                //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"D:\MSSData1.json"), js.Serialize(listMSSTrimmed));
                //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\" + vardt[0].ToString() + ".txt";
                //string path = @"D:\CFSP-FCI\23082024\DepotMaster\Data\t9.txt";
                //File.WriteAllText(path, js.Serialize(listDepotMaster));
                File.WriteAllText(path, js.Serialize(listDepotMaster));

            }
        }

    }

    /************************************/
    private static DataTable GetRemainingDepotData()
    {
        string conString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //string query = "usp_CFSP_DepotMaster";
        //string query = "usp_CFSP_DepotMasterData";
        //string query = "usp_CFSP_DepotMaster_Dummy";
        string query = "usp_CFSP_DepotMaster_Dummy_28082024";
        using (SqlConnection con = new SqlConnection(conString))
        {

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 200;
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
    }

    /**********************************/
    /**********************************/
    [WebMethod]
    public void MinimumStorageSpecificationsMSS_New(string DataDate)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string status = "";
        bool isSuccess = false;
        //if (WarehouseApiSecurity.AreCredentialsValid(username, password, "FciCfspUsername", "FciCfspPassword"))
        //{
        //DateTime FromDate = DateTime.ParseExact(DataDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture); //Convert.ToDateTime(DataDate);// = "2023-11-03";//DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
                                                                                                                            //string ToDate = "2023-11-04";//DateTime.Now.AddDays(-139).ToString("yyyy-MM-dd");
        List<MinimumStorageSpecifications> listMSSTrimmed = new List<MinimumStorageSpecifications>();
        List<MinimumStorageSpecifications.MSSChildQuantityIssued> listMSSChildQuantityIssued = new List<MinimumStorageSpecifications.MSSChildQuantityIssued>();
        List<MinimumStorageSpecifications.MSSChildQuantityDispatched> listMSSChildQuantityDispatched = new List<MinimumStorageSpecifications.MSSChildQuantityDispatched>();
        List<MinimumStorageSpecifications.MSSChildQuantityReceived> listMSSChildQuantityReceived = new List<MinimumStorageSpecifications.MSSChildQuantityReceived>();
        con.ConnectionString = str;
        con.Open();
        //cmd = new SqlCommand("usp_API_MSSTrimmed_FullDataWithCreatedDate", con);
        //cmd = new SqlCommand("usp_CFSP_MSS", con);
        //cmd = new SqlCommand("usp_CFSP_MSSDataByDate", con);
        cmd = new SqlCommand("Get_MSS_Data_For_CFSP", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@DataDate", FromDate);
        //cmd.Parameters.AddWithValue("@ToDate", ToDate);
        cmd.CommandTimeout = 200;
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            MinimumStorageSpecifications ObjMSSTrimmed = new MinimumStorageSpecifications();
            ObjMSSTrimmed.lgd_state_code = Convert.ToInt32(dr["lgd_state_code"]);
            ObjMSSTrimmed.lgd_district_code = Convert.ToInt32(dr["lgd_district_code"]);
            ObjMSSTrimmed.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
            ObjMSSTrimmed.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
            ObjMSSTrimmed.lgd_village_code = Convert.ToInt32(dr["lgd_village_code"]);
            //ObjMSSTrimmed.data_date = Convert.ToString(dr["data_date"]);
            DateTime var_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
            ObjMSSTrimmed.data_date = var_data_date.ToString("yyyy-MM-dd");
            ObjMSSTrimmed.depot_code = Convert.ToString(dr["depot_code"]);
            ObjMSSTrimmed.commodity_id = Convert.ToString(dr["commodity_id"]);
            ObjMSSTrimmed.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
            ObjMSSTrimmed.opening_balance = Convert.ToDouble(dr["opening_balance"]);
            ObjMSSTrimmed.closing_balance = Convert.ToDouble(dr["closing_balance"]);
            ObjMSSTrimmed.storage_loss = Convert.ToDouble(dr["storage_loss"]);
            ObjMSSTrimmed.storage_gain = Convert.ToDouble(dr["storage_gain"]);
            ObjMSSTrimmed.transaction_reference_id = Convert.ToString(dr["transaction_reference_id"]);
            ObjMSSTrimmed.crop_year = Convert.ToString(dr["crop_year"]);
            ObjMSSTrimmed.data_status = Convert.ToDouble(dr["data_status"]);
            //listMSSTrimmed.Add(ObjMSSTrimmed);

            /**************************MSS Trimmed Child Issue********************************/
            MinimumStorageSpecifications.MSSChildQuantityIssued ObjMSSIssue = new MinimumStorageSpecifications.MSSChildQuantityIssued();
            //ObjMSSIssue.data_date = Convert.ToDateTime(dr["data_date"]);
            DateTime var_Issue_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
            ObjMSSIssue.data_date = var_Issue_data_date.ToString("yyyy-MM-dd");
            ObjMSSIssue.depot_code = Convert.ToString(dr["depot_code"]);
            ObjMSSIssue.commodity_id = Convert.ToString(dr["commodity_id"]);
            ObjMSSIssue.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
            ObjMSSIssue.pool_stock_id = Convert.ToInt32(dr["pool_stock_id"]);
            //ObjMSSIssue.crop_year = Convert.ToString(dr["crop_year"]);
            ObjMSSIssue.categorization_type_id = Convert.ToInt32(dr["categorization_type_id"]);
            ObjMSSIssue.upgradable = Convert.ToInt32(dr["upgradable"]);
            ObjMSSIssue.net_quantity_issued = Convert.ToDouble(dr["net_quantity_issued"]);
            ObjMSSIssue.issue_scheme_id = Convert.ToString(dr["issue_scheme_id"]);
            ObjMSSIssue.scheme_id = Convert.ToString(dr["scheme_id"]);
            ObjMSSIssue.scheme_group_id = Convert.ToString(dr["scheme_group_id"]);
            ObjMSSIssue.scheme_name = Convert.ToString(dr["scheme_name"]);
            ObjMSSIssue.issue_destination_id = Convert.ToInt32(dr["issue_destination_id"]);
            //listMSSChildQuantityIssued.Add(ObjMSSIssue);

            /**************************MSS Trimmed Child Dispatch********************************/
            MinimumStorageSpecifications.MSSChildQuantityDispatched ObjMSSDispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched();
            //ObjMSSDispatch.data_date = Convert.ToDateTime(dr["data_date"]);
            DateTime varDispatch_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
            ObjMSSDispatch.data_date = varDispatch_data_date.ToString("yyyy-MM-dd");
            ObjMSSDispatch.depot_code = Convert.ToString(dr["depot_code"]);
            ObjMSSDispatch.commodity_id = Convert.ToString(dr["commodity_id"]);
            ObjMSSDispatch.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
            ObjMSSDispatch.net_quantity_dispatched = Convert.ToDouble(dr["net_quantity_dispatched"]);
            ObjMSSDispatch.dispatch_destination_id = Convert.ToInt32(dr["dispatch_destination_id"]);
            //listMSSChildQuantityDispatched.Add(ObjMSSDispatch);

            /**************************MSS Trimmed Child Receive********************************/
            MinimumStorageSpecifications.MSSChildQuantityReceived ObjMSSReceive = new MinimumStorageSpecifications.MSSChildQuantityReceived();

            //ObjMSSReceive.data_date = Convert.ToDateTime(dr["data_date"]);
            DateTime varReceive_data_date = DateTime.Parse(Convert.ToString(dr["data_date"]), CultureInfo.InvariantCulture);
            ObjMSSReceive.data_date = varReceive_data_date.ToString("yyyy-MM-dd");
            ObjMSSReceive.depot_code = Convert.ToString(dr["depot_code"]);
            ObjMSSReceive.commodity_id = Convert.ToString(dr["commodity_id"]);
            ObjMSSReceive.commodity_group_id = Convert.ToString(dr["commodity_group_id"]);
            ObjMSSReceive.inflow_source_id = Convert.ToInt32(dr["inflow_source_id"]);
            ObjMSSReceive.net_quantity_received = Convert.ToDouble(dr["net_quantity_received"]);
            //listMSSChildQuantityReceived.Add(ObjMSSReceive);
            /************************************************************************************/
            ObjMSSTrimmed.mss_issue = new MinimumStorageSpecifications.MSSChildQuantityIssued[] { ObjMSSIssue };
            ObjMSSTrimmed.mss_dispatch = new MinimumStorageSpecifications.MSSChildQuantityDispatched[] { ObjMSSDispatch };
            ObjMSSTrimmed.mss_received = new MinimumStorageSpecifications.MSSChildQuantityReceived[] { ObjMSSReceive };

            listMSSTrimmed.Add(ObjMSSTrimmed);
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = Int32.MaxValue;
        Context.Response.Write(js.Serialize(listMSSTrimmed));
        //File.WriteAllText(@"D:\MSSData.json", js.Serialize(listMSSTrimmed));
        //File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(@"D:\MSSData1.json"), js.Serialize(listMSSTrimmed));
        //string result = Path.GetTempPath();
        //Console.WriteLine(result);
        //System.IO.File.WriteAllText(Server.MapPath(result), js.Serialize(listMSSTrimmed));
        //System.IO.File.WriteAllText(result, js.Serialize(listMSSTrimmed));
        /**************For submitting /posting data to CFSP********************/
        /*
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cfsp.nic.in/cfsp/mss/MPWLC");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string jsonDataString = js.Serialize(listMSSTrimmed);
            streamWriter.Write(jsonDataString);
        }*/

        //}
        //else
        //{
        //Context.Response.Write("Credentials Invalid");
        //}
    }
}
