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
//using FCIWebService;

    /// <summary>
    /// Summary description for Transaction
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    //public class Transaction : System.Web.Services.WebService
    public class FCIAPIWebService : System.Web.Services.WebService
    {

        public class MSSTrimmed//MinimumStorageSpecification_Trimmed : System.Web.Services.WebService
        {

            public int lgd_state_code;// { get; set; }
            public int total_godowns; //{ get; set; }
            public string data_date; //{ get; set; }
            public string transaction_reference_id; //{ get; set; }
            public MSSTrimmedChildObject[] mss_trimmed_child;

            public class MSSTrimmedChildObject
            {
                //Child Items
                public int new_update;
                public int lgd_district_code;
                public int depot_status;
                public string depot_code;
                public string depot_name;
                public int ownership_type_id;
                public int ownership_group_type_id;
                public string depot_address;
                public int depot_pin;
                public string latitude;
                public string longitude;
                public double declared_capacity;
                public int predominant_storage_id;
                public int surveillance_cameras;
                public string camera_functional_since;
                public int no_of_cameras;
                public MSSTrimmedSubChildObject[] mss_trimmed_subchild;

                public class MSSTrimmedSubChildObject
                {
                    //SubChildItems
                    public int commodity_group_id;
                    public int commodity_id;
                    public int pool_stock_id;
                    public string crop_year;
                    public int categorization_type_id;
                    public double opening_balance;
                    public double net_quantity_issued;
                    public int issue_scheme_id;
                    public int scheme_id;
                    public int scheme_group_id;
                    public string scheme_name;
                    public int issue_destination_id;
                    public double net_quantity_dispatched;
                    public int dispatch_destination_id;
                    public int inflow_source_id;
                    public double net_quantity_received;
                    public double storage_loss;
                    public double storage_gain;
                    public double transit_loss;
                    public double transit_gain;
                    public double closing_balance;
                }
            }

        }

        [WebMethod]
        public void MSSTrimmedForFCI(string username, string password)
        {
            StringBuilder ReturnText = new StringBuilder();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            string status = "";
            bool isSuccess = false;
            if (username == "MPWLC" && password == "BukH446ywF9r")
            {

            //string FromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //string ToDate = DateTime.Now.ToString("yyyy-MM-dd");

            //string FromDate = DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
            //string ToDate = DateTime.Now.AddDays(-139).ToString("yyyy-MM-dd");
            string FromDate = "2023-11-03";//DateTime.Now.AddDays(-140).ToString("yyyy-MM-dd");
            string ToDate = "2023-11-04";//DateTime.Now.AddDays(-139).ToString("yyyy-MM-dd");

            List<MSSTrimmed> listMSSTrimmed = new List<MSSTrimmed>();
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand("usp_API_MSSTrimmed_FullDataWithCreatedDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                //cmd.Parameters.AddWithValue("@CropYear", CropYear);
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MSSTrimmed ObjMSSTrimmed = new MSSTrimmed();
                    ObjMSSTrimmed.lgd_state_code = Convert.ToInt32(dr["lgd_state_code"]);
                    ObjMSSTrimmed.total_godowns = Convert.ToInt32(dr["total_godowns"]);
                    ObjMSSTrimmed.data_date = Convert.ToString(dr["data_date"]);
                    ObjMSSTrimmed.transaction_reference_id = Convert.ToString(dr["transaction_reference_id"]);

                    MSSTrimmed.MSSTrimmedChildObject ObjMSSTrimmedChild = new MSSTrimmed.MSSTrimmedChildObject();
                    //Child Items
                    ObjMSSTrimmedChild.new_update = Convert.ToInt32(dr["new_update"]);
                    ObjMSSTrimmedChild.lgd_district_code = Convert.ToInt32(dr["lgd_district_code"]);
                    ObjMSSTrimmedChild.depot_status = Convert.ToInt32(dr["depot_status"]);
                    ObjMSSTrimmedChild.depot_code = Convert.ToString(dr["depot_code"]);
                    ObjMSSTrimmedChild.depot_name = Convert.ToString(dr["depot_name"]);
                    ObjMSSTrimmedChild.ownership_type_id = Convert.ToInt32(dr["ownership_type_id"]);
                    ObjMSSTrimmedChild.ownership_group_type_id = Convert.ToInt32(dr["ownership_group_type_id"]);
                    ObjMSSTrimmedChild.depot_address = Convert.ToString(dr["depot_address"]);
                    ObjMSSTrimmedChild.depot_pin = Convert.ToInt32(dr["depot_pin"]);
                    ObjMSSTrimmedChild.latitude = Convert.ToString(dr["latitude"]);
                    ObjMSSTrimmedChild.longitude = Convert.ToString(dr["longitude"]);
                    ObjMSSTrimmedChild.declared_capacity = Convert.ToDouble(dr["declared_capacity"]);
                    ObjMSSTrimmedChild.predominant_storage_id = Convert.ToInt32(dr["predominant_storage_id"]);
                    ObjMSSTrimmedChild.surveillance_cameras = Convert.ToInt32(dr["surveillance_cameras"]);
                    ObjMSSTrimmedChild.camera_functional_since = Convert.ToString(dr["camera_functional_since"]);
                    ObjMSSTrimmedChild.no_of_cameras = Convert.ToInt32(dr["no_of_cameras"]);
                    ObjMSSTrimmed.mss_trimmed_child = new MSSTrimmed.MSSTrimmedChildObject[] { ObjMSSTrimmedChild };

                    MSSTrimmed.MSSTrimmedChildObject.MSSTrimmedSubChildObject ObjMSSTrimmedSubChild = new MSSTrimmed.MSSTrimmedChildObject.MSSTrimmedSubChildObject();
                    //SubChildItems
                    ObjMSSTrimmedSubChild.commodity_group_id = Convert.ToInt32(dr["commodity_group_id"]);
                    ObjMSSTrimmedSubChild.commodity_id = Convert.ToInt32(dr["commodity_id"]);
                    ObjMSSTrimmedSubChild.pool_stock_id = Convert.ToInt32(dr["pool_stock_id"]);
                    ObjMSSTrimmedSubChild.crop_year = Convert.ToString(dr["crop_year"]);
                    ObjMSSTrimmedSubChild.categorization_type_id = Convert.ToInt32(dr["categorization_type_id"]);
                    ObjMSSTrimmedSubChild.opening_balance = Convert.ToDouble(dr["opening_balance"]);
                    ObjMSSTrimmedSubChild.net_quantity_issued = Convert.ToDouble(dr["net_quantity_issued"]);
                    ObjMSSTrimmedSubChild.issue_scheme_id = Convert.ToInt32(dr["issue_scheme_id"]);
                    ObjMSSTrimmedSubChild.scheme_id = Convert.ToInt32(dr["scheme_id"]);
                    ObjMSSTrimmedSubChild.scheme_group_id = Convert.ToInt32(dr["scheme_group_id"]);
                    ObjMSSTrimmedSubChild.scheme_name = Convert.ToString(dr["scheme_name"]);
                    ObjMSSTrimmedSubChild.issue_destination_id = Convert.ToInt32(dr["issue_destination_id"]);
                    ObjMSSTrimmedSubChild.net_quantity_dispatched = Convert.ToDouble(dr["net_quantity_dispatched"]);
                    ObjMSSTrimmedSubChild.dispatch_destination_id = Convert.ToInt32(dr["dispatch_destination_id"]);
                    ObjMSSTrimmedSubChild.inflow_source_id = Convert.ToInt32(dr["inflow_source_id"]);
                    ObjMSSTrimmedSubChild.net_quantity_received = Convert.ToDouble(dr["net_quantity_received"]);
                    ObjMSSTrimmedSubChild.storage_loss = Convert.ToDouble(dr["storage_loss"]);
                    ObjMSSTrimmedSubChild.storage_gain = Convert.ToDouble(dr["storage_gain"]);
                    ObjMSSTrimmedSubChild.transit_loss = Convert.ToDouble(dr["transit_loss"]);
                    ObjMSSTrimmedSubChild.transit_gain = Convert.ToDouble(dr["transit_gain"]);
                    ObjMSSTrimmedSubChild.closing_balance = Convert.ToDouble(dr["closing_balance"]);
                    ObjMSSTrimmedChild.mss_trimmed_subchild = new MSSTrimmed.MSSTrimmedChildObject.MSSTrimmedSubChildObject[] { ObjMSSTrimmedSubChild };
                    listMSSTrimmed.Add(ObjMSSTrimmed);

                    /*var roots = listMSSTrimmed;
                    foreach (var root in roots)
                        ObjMSSTrimmed.BuildTree(root, listMSSTrimmed.ToArray());
                    var json = JsonConvert.SerializeObject(roots, Formatting.Indented);*/

                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                Context.Response.Write(js.Serialize(listMSSTrimmed));
            }

            else
            {
                Context.Response.Write("Credentials Invalid");
            }
        }

        [WebMethod]
        public void ResponseAPI(string cid, string servcode, int cntSuccessR, int cntFailedR, string depotcode, string depotstacknumber, string trRemarks, string trRefId, string ackno, string msg, string msgStatus)
        {
            string vclient_id = cid;
            string vservice_code = servcode;
            int vno_of_success_records = cntSuccessR;
            int vno_of_failed_records = cntFailedR;
            string vdepot_code = depotcode;
            string vdepotstack_number = depotstacknumber;
            string vtransaction_remarks = trRemarks;
            string vtransaction_reference_id = trRefId;
            string vacknowledgement_no = ackno;
            string vmessage = msg;
            string vmessageStatus = msgStatus;

            String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand("usp_ResponseAPIJSONData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@client_id", vclient_id);
            cmd.Parameters.AddWithValue("@service_code", vservice_code);
            cmd.Parameters.AddWithValue("@no_of_success_records", vno_of_success_records);
            cmd.Parameters.AddWithValue("@no_of_failed_records", vno_of_failed_records);
            cmd.Parameters.AddWithValue("@depot_code", vdepot_code);
            cmd.Parameters.AddWithValue("@stack_number", vdepotstack_number);
            cmd.Parameters.AddWithValue("@transaction_remarks", vtransaction_remarks);
            cmd.Parameters.AddWithValue("@transaction_reference_id", vtransaction_reference_id);
            cmd.Parameters.AddWithValue("@acknowledgement_no", vacknowledgement_no);
            cmd.Parameters.AddWithValue("@message", vmessage);
            cmd.Parameters.AddWithValue("@messageStatus", vmessageStatus);
            cmd.CommandTimeout = 0;
            SqlDataReader dr = cmd.ExecuteReader();
        }
    }


