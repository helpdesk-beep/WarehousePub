using Jayrock.Json.Conversion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;


/// <summary>
/// Summary description for FarmGateApi
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class FarmGateApi : System.Web.Services.WebService {

    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    String str = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    string File, filename;
    // SqlConnection con;
    string Con_OTP;
    string mobile_no;

    string district_id;
    string name;
    string user_mobile_no;
    string User_Type;
    string dist_name;
    string state_name;

    public FarmGateApi () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    string otp_code;

    public void opt()
    {
        //char[] charArr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        char[] charArr = "0123456789".ToCharArray();

        string strrandom = string.Empty;
        Random objran = new Random();
        int noofcharacters = Convert.ToInt32("4");
        for (int i = 0; i < noofcharacters; i++)
        {
            //It will not allow Repetation of Characters
            int pos = objran.Next(1, charArr.Length);
            if (!strrandom.Contains(charArr.GetValue(pos).ToString()))

                strrandom += charArr.GetValue(pos);
            else
                i--;
        }
        otp_code = strrandom;
    }

    public class LoginSuccess
    {
        public string IsSuccess;
        public string OTP;
        public string statusResult;
    }
    [WebMethod]
    public void LoginWebservices(string MobileNo, string UserType)
    {
        StringBuilder ReturnText = new StringBuilder();
        DataTable dt = new DataTable();
        List<LoginSuccess> list = new List<LoginSuccess>();
        string status = "";
        bool isSuccess = false;
        try
        {
            //SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand("GET_Mobile_No_For_user", con);
            cmd.Parameters.Add("@user_type", UserType);
            cmd.Parameters.Add("@mobile_no", MobileNo);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                isSuccess = true;
                status = "1";
                opt();
                string msg = "Your Verification Code (OTP) : " + otp_code + "";
               // sms.sendSingleSMS(MobileNo, msg);

                foreach (DataRow dr in dt.Rows)
                {
                    LoginSuccess ob = new LoginSuccess();

                    ob.IsSuccess = Convert.ToString("True");
                    ob.OTP = Convert.ToString(otp_code);
                    ob.statusResult = status;

                    list.Add(ob);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "update mst_user set otp=@opt where mobile_no=@mobile_no";
                    cmd.Parameters.AddWithValue("@opt", otp_code);
                    cmd.Parameters.AddWithValue("@mobile_no", MobileNo);
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                    // con.Close();

                }
                Context.Response.Write(JsonHelper.JsonSerializer<IList<LoginSuccess>>(list));
            }
            else
            {
                System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                //  dd.Add("IsSuccess", "False");
                isSuccess = false;
                status = "0";
                // Context.Response.Write(JsonConvert.ExportToString(dd));
            }
            // con.Close();
        }
        catch (Exception ex)
        {
            isSuccess = false;
            status = "-1";
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
            //  dd.Add("FAIL", ex.Message);
        }
        finally
        {
            con.Close();
            if (!isSuccess)
            {
                LoginSuccess ob = new LoginSuccess();

                ob.IsSuccess = Convert.ToString("False");
                ob.OTP = "";
                ob.statusResult = status;

                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<LoginSuccess>>(list));
            }

        }
        Context.Response.Write(ReturnText);
    }



    public class TraderOrFarmerRegistration
    {
        public bool IsSuccess;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void farmerRegistration( int district_id, string tehsil_id, int village_city_id,
        string full_name, string bank_account_no, string ifsc_code, string branch_name,string branch_id,
        string mobile_no, string password, string address, string pincode, string aadhar_no,
        string khasra_no,string device_id, int user_type)
    {
        // Boolean m = false;

        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        SqlConnection con = new SqlConnection();
        DataSet ds = new DataSet();
        con.ConnectionString = strConnString;

        con.Open();
        try
        {
            List<TraderOrFarmerRegistration> list = new List<TraderOrFarmerRegistration>();
            TraderOrFarmerRegistration ob = new TraderOrFarmerRegistration();
            SqlCommand cmd = new SqlCommand("[dbo].[user_registration]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@district_id", district_id);
            cmd.Parameters.AddWithValue("@tehsil_id", tehsil_id);
            cmd.Parameters.AddWithValue("@village_city_id", village_city_id);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@mobile_no", mobile_no);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@pincode", pincode);
            cmd.Parameters.AddWithValue("@bank_account_no", bank_account_no);
            cmd.Parameters.AddWithValue("@ifsc_code", ifsc_code);
            cmd.Parameters.AddWithValue("@branch_id", branch_id);
            cmd.Parameters.AddWithValue("@branch_name", branch_name);
            cmd.Parameters.AddWithValue("@adhar_no", aadhar_no);
            cmd.Parameters.AddWithValue("@khasra_no", khasra_no);
         
            cmd.Parameters.AddWithValue("@user_type", user_type);
            cmd.Parameters.AddWithValue("@device_id", device_id);
           
            int val = cmd.ExecuteNonQuery();

            con.Close();
            if (val > 0)
            {

                System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();

                dd.Add("IsSuccess", "true");
                Context.Response.Write(JsonConvert.ExportToString(dd));

            }
            else
            {
                System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                dd.Add("IsSuccess", "False");
                Context.Response.Write(JsonConvert.ExportToString(dd));

            }

            // return m;
        }
        catch (Exception ex)
        {
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
            dd.Add("IsSuccess", ex.Message);
            Context.Response.Write(JsonConvert.ExportToString(dd));
        }
        finally
        {
            con.Close();
        }
        // return m;

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void traderRegistration(int district_id, string tehsil_id, int village_city_id,
      string full_name, 
      string mobile_no, string password, string address, string pincode, string aadhar_no,
      string trader_licence_no, decimal trader_purchase_limit,int user_type, string device_id)
    {
        // Boolean m = false;

        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        SqlConnection con = new SqlConnection();
        DataSet ds = new DataSet();
        con.ConnectionString = strConnString;

        con.Open();
        try
        {
            List<TraderOrFarmerRegistration> list = new List<TraderOrFarmerRegistration>();
            TraderOrFarmerRegistration ob = new TraderOrFarmerRegistration();
            SqlCommand cmd = new SqlCommand("[dbo].[trader_registration]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@district_id", district_id);
            cmd.Parameters.AddWithValue("@tehsil_id", tehsil_id);
            cmd.Parameters.AddWithValue("@village_city_id", village_city_id);
            cmd.Parameters.AddWithValue("@full_name", full_name);
            cmd.Parameters.AddWithValue("@mobile_no", mobile_no);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@pincode", pincode);
            cmd.Parameters.AddWithValue("@adhar_no", aadhar_no);
            cmd.Parameters.AddWithValue("@traders_licence_no",trader_licence_no);
            cmd.Parameters.AddWithValue("@trader_puchase_limit", trader_purchase_limit);
            cmd.Parameters.AddWithValue("@user_type", user_type);
            cmd.Parameters.AddWithValue("@device_id", device_id);

            int val = cmd.ExecuteNonQuery();

            con.Close();
            if (val > 0)
            {

                System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();

                dd.Add("IsSuccess", "true");
                Context.Response.Write(JsonConvert.ExportToString(dd));

            }
            else
            {
                System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
                dd.Add("IsSuccess", "False");
                Context.Response.Write(JsonConvert.ExportToString(dd));

            }

            // return m;
        }
        catch (Exception ex)
        {
            System.Collections.Generic.Dictionary<string, string> dd = new Dictionary<string, string>();
            dd.Add("IsSuccess", ex.Message);
            Context.Response.Write(JsonConvert.ExportToString(dd));
        }
        finally
        {
            con.Close();
        }
        // return m;

    }

}
