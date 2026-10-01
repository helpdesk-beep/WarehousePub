using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService_for_FCI
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService_for_FCI : System.Web.Services.WebService
{
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    public WebService_for_FCI()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class DepoProfile
    {
        public int lgd_state_code;
        public int lgd_district_code;
        public int depot_status;
        public string depot_code;
        public string depot_name;
        public int declared_capacity;
        public int ownership_group_type;
        public string owner_name;
        public int ownership_type;
        public int hired_by;
        public int hired_from;
        public string hired_from_date;
        public string hired_upto_date;
        public string depot_address;
        public string latitude;
        public string longitude;
        public int pin_code;
        public int electronic_weighbridge_count;
        public string electronic_weighbridge_make;
        public int rail_sided;
        public int rail_siding_count;
        public int lgd_subdistrict_code;
        public int lgd_block_code;
        public int lgd_village_code;
        public string data_date;
        public string transaction_reference_id;
    }
    [WebMethod]
    public void DepoProfileForFCI(string Credential)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        string status = "";
        bool isSuccess = false;

        string Cred = Credential;
        List<DepoProfile> list = new List<DepoProfile>();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "FciCfspApiCredential"))
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand("Sp_Depo_Profile", con);
                // cmd.Parameters.Add("@User_Type", User_Type);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    isSuccess = true;
                    status = "1";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        DepoProfile ob = new DepoProfile();
                        //var ob = new DepoProfile();
                        ob.lgd_state_code = Convert.ToInt32(dr["lgd_state_Code"]);
                        ob.lgd_district_code = Convert.ToInt32(dr["LG_District_Code"]);
                        ob.depot_status = Convert.ToInt32(dr["Depot_Status"]);
                        ob.depot_code = Convert.ToString(dr["Depo_Code"]);
                        ob.depot_name = Convert.ToString(dr["Depot_Name"]);
                        ob.declared_capacity = Convert.ToInt32(dr["StorageCapacityMT"]);
                        ob.ownership_group_type = Convert.ToInt32(dr["ownership_group_type"]);
                        ob.owner_name = Convert.ToString(dr["owner_Name"]);
                        ob.ownership_type = Convert.ToInt32(dr["ownership_type"]);
                        ob.hired_by = Convert.ToInt32(dr["Hired_by"]);
                        // ob.User_Type = Convert.ToString(dr["User_Type"]);
                        ob.hired_from = Convert.ToInt32(dr["hired_from"]);
                        ob.hired_from_date = Convert.ToString(dr["hired_from_date"]);
                        ob.hired_upto_date = Convert.ToString(dr["hired_upto_date"]);
                        ob.depot_address = Convert.ToString(dr["depot_adress"]);
                        ob.latitude = Convert.ToString(dr["Latitude"]);
                        ob.longitude = Convert.ToString(dr["Longitude"]);
                        ob.pin_code = Convert.ToInt32(dr["Pin_code"]);
                        ob.electronic_weighbridge_count = Convert.ToInt32(dr["electroninc_weghbrige_count"]);
                        ob.electronic_weighbridge_make = Convert.ToString(dr["electroninc_weghbrige_make"]);
                        ob.rail_sided = Convert.ToInt32(dr["rail_sided"]);
                        ob.rail_siding_count = Convert.ToInt32(dr["rail_siding_count"]);
                        ob.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
                        ob.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
                        ob.lgd_village_code = Convert.ToInt32(dr["lgd_village_Code"]);
                        ob.data_date = Convert.ToString(dr["data_date"]);
                        //ob.transaction_reference_id = "23" + Convert.ToString(GenerateNumber());
                        ob.transaction_reference_id = Convert.ToString(dr["Transaction_reference_id"]);
                        list.Add(ob);
                        //list
                        //ob.Remove(itemToMove);
                        //ob.Insert(indexToMoveTo, itemToMove);
                    }
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<DepoProfile>>(list));
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                    isSuccess = false;
                    status = "0";
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
        }
        finally
        {
            if (!isSuccess)
            {

                DepoProfile ob = new DepoProfile();

                ob.lgd_state_code = 0;
                ob.lgd_district_code = 0;
                ob.depot_status = 0;
                ob.depot_code = "0";
                ob.depot_name = "";
                ob.declared_capacity = 0;
                ob.ownership_group_type = 0;
                ob.owner_name = "";
                ob.ownership_type = 0;
                ob.hired_by = 0;
                // ob.User_Type = Convert.ToString(dr["User_Type"]);
                ob.hired_from = 0;
                ob.hired_from_date = "";
                ob.hired_upto_date = "";
                ob.depot_address = "";
                ob.latitude = "";
                ob.longitude = "";
                ob.pin_code = 0;
                ob.electronic_weighbridge_count = 0;
                ob.electronic_weighbridge_make = "";
                ob.rail_sided = 0;
                ob.rail_siding_count = 0;
                ob.lgd_subdistrict_code = 0;
                ob.lgd_block_code = 0;
                ob.lgd_village_code = 0;
                ob.data_date = "";
                ob.transaction_reference_id = "";
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<DepoProfile>>(list));
            }
        }
        Context.Response.Write(ReturnText);
    }
    [WebMethod]
    public string GenerateNumber()
    {
        Random random = new Random();
        string r = "";
        int i;
        for (i = 1; i < 11; i++)
        {
            r += random.Next(0, 9).ToString();
        }
        return r;
    }
    public class StackProfile
    {
        public int lgd_state_code;
        public int lgd_district_code;
        public int depot_status;
        public string depot_code;
        public string depot_name;
        public string stack_number;
        public decimal stack_capacity;
        public string stack_created_on;
        public string stack_formed_on;
        public decimal stack_formation_quantity;
        public int storage_type;
        public int commodity;
        public int marketing_season;
        public string crop_year;
        public int crop_type_id;
        public int bag_type;
        public int bag_count;
        public int categorization_type;
        public decimal opening_balance;
        public double closing_balance;
        public string transaction_reference_id;
        public string data_date;
        //public string electroninc_weghbrige_make;
        //public int rail_sided;
        //public int rail_siding_count;
        //public int lgd_subdistrict_code;
        //public int lgd_block_code;
        //public int lgd_village_Code;
        //public string data_date;
        //public string Transaction_reference_id;
    }
    [WebMethod]
    public void StackProfileForFCI(string Credential)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        string status = "";
        bool isSuccess = false;
        string Cred = Credential;

        List<StackProfile> list = new List<StackProfile>();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "FciCfspApiCredential"))
            {
                con.ConnectionString = str;
                con.Open();
                //cmd = new SqlCommand("Sp_Stack_Profile", con);
                cmd = new SqlCommand("Sp_Stack_Profile_From_Sync", con);
                // cmd.Parameters.Add("@User_Type", User_Type);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    isSuccess = true;
                    status = "1";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        StackProfile ob = new StackProfile();
                        ob.lgd_state_code = Convert.ToInt32(dr["lgd_state_Code"]);
                        ob.lgd_district_code = Convert.ToInt32(dr["LG_District_Code"]);
                        ob.depot_status = Convert.ToInt32(dr["Depot_Status"]);
                        ob.depot_code = Convert.ToString(dr["Depo_Code"]);
                        ob.depot_name = Convert.ToString(dr["Depot_Name"]);
                        ob.stack_number = Convert.ToString(dr["Stack_Number"]);
                        ob.stack_capacity = Convert.ToDecimal(dr["Stack_capacity"]);
                        //'Stack_capacity'
                        ob.stack_created_on = Convert.ToString(dr["Stack_Created_on"]);
                        ob.stack_formed_on = Convert.ToString(dr["stack_formed_on"]);
                        ob.stack_formation_quantity = Convert.ToDecimal(dr["stack_formation_quantity"]);
                        ob.storage_type = Convert.ToInt32(dr["Storage_Type"]);
                        ob.commodity = Convert.ToInt32(dr["Commodity_Id"]);
                        // ob.User_Type = Convert.ToString(dr["User_Type"]);
                        ob.marketing_season = Convert.ToInt32(dr["Marketing_Season"]);
                        ob.crop_year = Convert.ToString(dr["Crop_Year"]);
                        ob.crop_type_id = Convert.ToInt32(dr["Crop_type_ID"]);
                        ob.bag_type = Convert.ToInt32(dr["Bag_Type"]);
                        ob.bag_count = Convert.ToInt32(dr["bag_count"]);
                        ob.categorization_type = Convert.ToInt32(dr["categorization_type"]);
                        ob.opening_balance = Convert.ToDecimal(dr["Opening_Balance"]);
                        ob.closing_balance = Convert.ToDouble(dr["Closing_Balance"]);
                        ob.transaction_reference_id = Convert.ToString(dr["Transaction_reference_id"]);
                        ob.data_date = Convert.ToString(dr["Data_Date"]);
                        //ob.electroninc_weghbrige_make = Convert.ToString(dr["electroninc_weghbrige_make"]);
                        //ob.rail_sided = Convert.ToInt32(dr["rail_sided"]);
                        //ob.rail_siding_count = Convert.ToInt32(dr["rail_siding_count"]);
                        //ob.lgd_subdistrict_code = Convert.ToInt32(dr["lgd_subdistrict_code"]);
                        //ob.lgd_block_code = Convert.ToInt32(dr["lgd_block_code"]);
                        //ob.lgd_village_Code = Convert.ToInt32(dr["lgd_village_Code"]);
                        //ob.data_date = Convert.ToString(dr["data_date"]);
                        //ob.Transaction_reference_id = "23" + Convert.ToString(GenerateNumber());
                        list.Add(ob);
                    }
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<StackProfile>>(list));
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                    isSuccess = false;
                    status = "0";
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
        }
        finally
        {
            if (!isSuccess)
            {

                StackProfile ob = new StackProfile();

                ob.lgd_state_code = 0;
                ob.lgd_district_code = 0;
                ob.depot_status = 0;
                ob.depot_code = "0";
                ob.depot_name = "";
                ob.stack_number = "";
                ob.stack_capacity = 0;
                ob.stack_created_on = "";
                ob.stack_formed_on = "";
                ob.stack_formation_quantity =0;
                ob.storage_type = 0;
                ob.commodity = 0;
                // ob.User_Type = Convert.ToString(dr["User_Type"]);
                ob.marketing_season = 0;
                ob.crop_year = "";
                ob.crop_type_id = 0;
                ob.bag_type = 0;
                ob.bag_count = 0;
                ob.categorization_type = 0;
                ob.opening_balance = 0;
                ob.closing_balance = 0;
                ob.transaction_reference_id = "";
                ob.data_date = "";
                //ob.electroninc_weghbrige_make = "";
                //ob.rail_sided = 0;
                //ob.rail_siding_count = 0;
                //ob.lgd_subdistrict_code = 0;
                //ob.lgd_block_code = 0;
                //ob.lgd_village_Code = 0;
                //ob.data_date = "";
                //ob.Transaction_reference_id = "";
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<StackProfile>>(list));
            }
        }
        Context.Response.Write(ReturnText);
    }

    public class InflowProfile
    {
        public int lgd_state_Code;
        public int LG_District_Code;
        public int Depot_Status;
        public string Depo_Code;
        public string Depot_Name;
        public string Truck_Number;
        public string Truck_Chit;
        public double Net_Quantity;
        public string Transaction_reference_id;
        public string Data_Date;
        public int Inflow_Source;
        public string Stack_Number;
        public double Inflow_Quantity;
        public int Commodity_Id;
        public int Marketing_Season;
        public string Crop_Year;
        public string Crop_type_ID;
        public string bag_type;
        public int WLC_Bags;
    }
    [WebMethod]
    public void InflowProfileForFCI(string Credential)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        string status = "";
        bool isSuccess = false;
        string Cred = Credential;

        List<InflowProfile> list = new List<InflowProfile>();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "FciCfspApiCredential"))
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand("SP_Inflow_Profile", con);
                // cmd.Parameters.Add("@User_Type", User_Type);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    isSuccess = true;
                    status = "1";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        InflowProfile ob = new InflowProfile();
                        ob.lgd_state_Code = Convert.ToInt32(dr["lgd_state_Code"]);
                        ob.LG_District_Code = Convert.ToInt32(dr["LG_District_Code"]);
                        ob.Depot_Status = Convert.ToInt32(dr["Depot_Status"]);
                        ob.Depo_Code = Convert.ToString(dr["Depo_Code"]);
                        ob.Depot_Name = Convert.ToString(dr["Depot_Name"]);
                        ob.Truck_Number = Convert.ToString(dr["Truck_Number"]);
                        ob.Truck_Chit = Convert.ToString(dr["Truck_Chit"]);
                        ob.Net_Quantity = Convert.ToDouble(dr["Net_Quantity"]);
                        ob.Transaction_reference_id = Convert.ToString(dr["Transaction_reference_id"]);
                        ob.Data_Date = Convert.ToString(dr["Data_Date"]);

                        ob.Inflow_Source = Convert.ToInt32(dr["Inflow_Source"]);
                        ob.Stack_Number = Convert.ToString(dr["Stack_Number"]);
                        
                        ob.Inflow_Quantity = Convert.ToDouble(dr["Inflow_Quantity"]);
                        ob.Commodity_Id = Convert.ToInt32(dr["Commodity_Id"]);
                        ob.Marketing_Season = Convert.ToInt32(dr["Marketing_Season"]);
                        ob.Crop_Year = Convert.ToString(dr["Crop_Year"]);
                        ob.Crop_type_ID = Convert.ToString(dr["Crop_type_ID"]);
                        ob.bag_type = Convert.ToString(dr["Bag_Type"]);
                        ob.WLC_Bags = Convert.ToInt32(dr["WLC_Bags"]);
                        list.Add(ob);
                    }
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<InflowProfile>>(list));
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                    isSuccess = false;
                    status = "0";
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
        }
        finally
        {
            if (!isSuccess)
            {

                InflowProfile ob = new InflowProfile();

                ob.lgd_state_Code = 0;
                ob.LG_District_Code = 0;
                ob.Depot_Status = 0;
                ob.Depo_Code = "0";
                ob.Depot_Name = "";

                ob.Truck_Number = "";
                ob.Truck_Chit = "";
                ob.Net_Quantity = 0;
                ob.Transaction_reference_id = "";
                ob.Data_Date = "";
                ob.Inflow_Source = 0;
                ob.Stack_Number = ""; 
                ob.Inflow_Quantity = 0;

                ob.Commodity_Id = 0;
                ob.Marketing_Season = 0;
                ob.Crop_Year = "";
                ob.Crop_type_ID = "";
                ob.bag_type = "";
                ob.WLC_Bags = 0;
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<InflowProfile>>(list));
            }
        }
        Context.Response.Write(ReturnText);
    }

    public class OutflowProfile
    {
        public int lgd_state_Code;
        public int LG_District_Code;
        public int Depot_Status;
        public string Depo_Code;
        public string Depot_Name;
        public string Truck_Number;
        public string Truck_Chit;
        public double Net_Quantity;
        public string Transaction_reference_id;
        public string Data_Date;
        public int Outflow_Destination;
        public string Stack_Number;
        public double Outflow_Quantity;
        public int Outflow_Scheme;
        public int Commodity_Id;
        public int Marketing_Season;
        public string Crop_Year;
        public string Crop_Type_ID;
        public string Bag_Type;
        public int Bag_Count;
    }
    [WebMethod]
    public void OutflowProfileForFCI(string Credential)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        string status = "";
        bool isSuccess = false;
        string Cred = Credential;

        List<OutflowProfile> list = new List<OutflowProfile>();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "FciCfspApiCredential"))
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand("SP_Outflow_Profile", con);
                // cmd.Parameters.Add("@User_Type", User_Type);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    isSuccess = true;
                    status = "1";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        OutflowProfile ob = new OutflowProfile();
                        ob.lgd_state_Code = Convert.ToInt32(dr["lgd_state_Code"]);
                        ob.LG_District_Code = Convert.ToInt32(dr["LG_District_Code"]);
                        ob.Depot_Status = Convert.ToInt32(dr["Depot_Status"]);
                        ob.Depo_Code = Convert.ToString(dr["Depo_Code"]);
                        ob.Depot_Name = Convert.ToString(dr["Depot_Name"]);
                        ob.Truck_Number = Convert.ToString(dr["Truck_Number"]);
                        ob.Truck_Chit = Convert.ToString(dr["Truck_Chit"]);
                        ob.Net_Quantity = Convert.ToDouble(dr["Net_Quantity"]);
                        ob.Transaction_reference_id = Convert.ToString(dr["Transaction_reference_id"]);
                        ob.Data_Date = Convert.ToString(dr["Data_Date"]);

                        ob.Outflow_Destination = Convert.ToInt32(dr["Outflow_Destination"]);
                        ob.Stack_Number = Convert.ToString(dr["Stack_Number"]);

                        ob.Outflow_Quantity = Convert.ToDouble(dr["Outflow_Quantity"]);
                        ob.Outflow_Scheme = Convert.ToInt32(dr["Outflow_Scheme"]);
                        ob.Commodity_Id = Convert.ToInt32(dr["Commodity_Id"]);
                        ob.Marketing_Season = Convert.ToInt32(dr["Marketing_Season"]);
                        ob.Crop_Year = Convert.ToString(dr["Crop_Year"]);
                        ob.Crop_Type_ID = Convert.ToString(dr["Crop_Type_ID"]);
                        ob.Bag_Type = Convert.ToString(dr["Bag_Type"]);
                        ob.Bag_Count = Convert.ToInt32(dr["Bag_Count"]);
                        list.Add(ob);
                    }
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<OutflowProfile>>(list));
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                    isSuccess = false;
                    status = "0";
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
        }
        finally
        {
            if (!isSuccess)
            {

                OutflowProfile ob = new OutflowProfile();

                ob.lgd_state_Code = 0;
                ob.LG_District_Code = 0;
                ob.Depot_Status = 0;
                ob.Depo_Code = "0";
                ob.Depot_Name = "";
                ob.Truck_Number = "";
                ob.Truck_Chit = "";
                ob.Net_Quantity = 0;
                ob.Transaction_reference_id = "";
                ob.Data_Date = "";
                ob.Outflow_Destination = 0;
                ob.Stack_Number = "";
                ob.Outflow_Quantity = 0;
                ob.Outflow_Scheme = 0;
                ob.Commodity_Id = 0;
                ob.Marketing_Season = 0;
                ob.Crop_Year = "";
                ob.Crop_Type_ID = "";
                ob.Bag_Type = "";
                ob.Bag_Count = 0;
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<OutflowProfile>>(list));
            }
        }
        Context.Response.Write(ReturnText);
    }

    public class InfestationandTeatment
    {
        public int lgd_state_Code;
        public int LG_District_Code;
        public int Depot_Status;
        public string Depo_Code;
        public string Depot_Name;
        public string Stack_Number;
        public string Stack_Created_on;
        public string Stack_Formed_on;
        public double Stack_Formation_quantity;
        public double quantity;
        public int Commodity_Id;
        public int Marketing_Season;
        public string Crop_Year;
        public int Crop_Type_ID;
        public int Bag_Type;
        public int Bag_Count;
        public int infestetion_type;
        public string teatment_type;
        public string date_of_treatment;
        public string dat_of_availability_for_issue;
        public int categorization_type;
        public string Transaction_reference_id;
        public string Data_Date;
        
    }
    [WebMethod]
    public void InfestationandTeatmentForFCI(string Credential)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        string status = "";
        bool isSuccess = false;
        string Cred = Credential;

        List<InfestationandTeatment> list = new List<InfestationandTeatment>();
        try
        {
            if (WarehouseApiSecurity.IsCredentialValid(Cred, "FciCfspApiCredential"))
            {
                con.ConnectionString = str;
                con.Open();
                cmd = new SqlCommand("Sp_Infestation_and_Teatment", con);
                // cmd.Parameters.Add("@User_Type", User_Type);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds2 = new DataSet();
                da.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    isSuccess = true;
                    status = "1";
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        InfestationandTeatment ob = new InfestationandTeatment();
                        ob.lgd_state_Code = Convert.ToInt32(dr["lgd_state_Code"]);
                        ob.LG_District_Code = Convert.ToInt32(dr["LG_District_Code"]);
                        ob.Depot_Status = Convert.ToInt32(dr["Depot_Status"]);
                        ob.Depo_Code = Convert.ToString(dr["Depo_Code"]);
                        ob.Depot_Name = Convert.ToString(dr["Depot_Name"]);
                        ob.Stack_Number = Convert.ToString(dr["Stack_Number"]);
                        ob.Stack_Created_on = Convert.ToString(dr["Stack_Created_on"]);
                        ob.Stack_Formed_on = Convert.ToString(dr["Stack_Formed_on"]);
                        ob.Stack_Formation_quantity = Convert.ToDouble(dr["Stack_Formation_quantity"]);
                        ob.quantity = Convert.ToDouble(dr["quantity"]);
                        ob.Commodity_Id = Convert.ToInt32(dr["Commodity_Id"]);
                        ob.Marketing_Season = Convert.ToInt32(dr["Marketing_Season"]);
                        ob.Crop_Year = Convert.ToString(dr["Crop_Year"]);
                        ob.Crop_Type_ID = Convert.ToInt32(dr["Crop_Type_ID"]);
                        ob.Bag_Type = Convert.ToInt32(dr["Bag_Type"]);
                        ob.Bag_Count = Convert.ToInt32(dr["bag_count"]);
                        ob.infestetion_type = Convert.ToInt32(dr["infestetion_type"]);
                        ob.teatment_type = Convert.ToString(dr["teatment_type"]);
                        ob.date_of_treatment = Convert.ToString(dr["date_of_treatment"]);
                        ob.dat_of_availability_for_issue = Convert.ToString(dr["dat_of_availability_for_issue"]);
                        ob.categorization_type = Convert.ToInt32(dr["categorization_type"]);
                        ob.Transaction_reference_id = Convert.ToString(dr["Transaction_reference_id"]);
                        ob.Data_Date = Convert.ToString(dr["Data_Date"]);
                        list.Add(ob);
                    }
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<InfestationandTeatment>>(list));
                }
                else
                {
                    System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                    isSuccess = false;
                    status = "0";
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
        }
        finally
        {
            if (!isSuccess)
            {

                InfestationandTeatment ob = new InfestationandTeatment();

                ob.lgd_state_Code = 0;
                ob.LG_District_Code = 0;
                ob.Depot_Status = 0;
                ob.Depo_Code = "0";
                ob.Depot_Name = "";
                ob.Stack_Number = "";
                ob.Stack_Created_on = "";
                ob.Stack_Formed_on = "";
                ob.Stack_Formation_quantity = 0;
                ob.quantity = 0;
                ob.Commodity_Id = 0;
                ob.Marketing_Season = 0;
                ob.Crop_Year = "0";
                ob.Crop_Type_ID = 0;
                ob.Bag_Type = 0;
                ob.Bag_Count = 0;
                ob.infestetion_type = 0;
                ob.teatment_type = "0";
                ob.date_of_treatment = "";
                ob.dat_of_availability_for_issue = "";
                ob.categorization_type = 0;
                ob.Transaction_reference_id = "";
                ob.Data_Date = "";
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<InfestationandTeatment>>(list));
            }
        }
        Context.Response.Write(ReturnText);
    }
}
