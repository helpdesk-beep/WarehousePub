using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using System.Web.UI.WebControls;


namespace ExportImportData
{
    /// <summary>
    /// Summary description for ExportDataMPWLC_MPSCSC
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ExportDataMPWLC_MPSCSC : System.Web.Services.WebService
    {
        
                
        [WebMethod]
        public void EDAddReceiptDetails(string Dist_Id, string Depot_ID, string Receipt_id, string S_of_arrival, string S_name, string A_dist, string A_Depo, string RO_No,
         string TO_Number, DateTime Dispatch_Date, DateTime arrival_date, string challan_no, DateTime challan_date, double Qty, string Commodity, string Scheme,
         string Crop_year, string Category, string Transporter, string Vehile_no, string Arrival_time, string Gunny_type, int No_of_Bags, double Recd_Qty,
         int Recieved_Bags, double Moisture, string WCM_no, double Variation_qty, int Month, int Year, string IsDeposit, string IP_Address, DateTime Created_date,
         DateTime Updated_date, string Challan_Status, string Godown, string OperatorID,
         string State_Id, string NoTransaction, string Orderno, string Branch, string typeofbags, int Rec_PP_bags, int Rec_Jute_bags, int Rec_OU_bags,
         int Sent_Jute_bags, int Sent_OU_bags,
         int Sent_PP_bags, string whr, string StackNumber, string stackName, string Stacknum, string slogin_name, string slogin_mob, string slogin_des, string Updated_LgnName, string updated_LgnMobile, string updated_LgnDesgn)
        {
            string ResponseMsg = null;
            SqlDataAdapter da = new SqlDataAdapter();
            String str = ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ConnectionString.ToString();
            //String str1 = ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand("usp_ExportReceiptDetailsDataInMPSCSC_MPWLC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar, 250);
            cmd.Parameters.AddWithValue("@CommandType", "1");
            cmd.Parameters.AddWithValue("@Dist_Id", Dist_Id);
            cmd.Parameters.AddWithValue("@Depot_ID", Depot_ID);
            cmd.Parameters.AddWithValue("@Receipt_id", Receipt_id);
            cmd.Parameters.AddWithValue("@S_of_arrival", S_of_arrival);
            cmd.Parameters.AddWithValue("@S_name", S_name);
            cmd.Parameters.AddWithValue("@A_dist", A_dist);
            cmd.Parameters.AddWithValue("@A_Depo", A_Depo);
            cmd.Parameters.AddWithValue("@RO_No", RO_No);
            cmd.Parameters.AddWithValue("@TO_Number", TO_Number);
            cmd.Parameters.AddWithValue("@Dispatch_Date", Dispatch_Date);
            cmd.Parameters.AddWithValue("@arrival_date", arrival_date);
            cmd.Parameters.AddWithValue("@challan_no", challan_no);
            cmd.Parameters.AddWithValue("@challan_date", challan_date);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Commodity", Commodity);
            cmd.Parameters.AddWithValue("@Scheme", Scheme);
            cmd.Parameters.AddWithValue("@Crop_year", Crop_year);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@Transporter", Transporter);
            cmd.Parameters.AddWithValue("@Vehile_no", Vehile_no);
            cmd.Parameters.AddWithValue("@Arrival_time", Arrival_time);
            cmd.Parameters.AddWithValue("@Gunny_type", Gunny_type);
            cmd.Parameters.AddWithValue("@No_of_Bags", No_of_Bags);
            cmd.Parameters.AddWithValue("@Recd_Qty", Recd_Qty);
            cmd.Parameters.AddWithValue("@Recieved_Bags", Recieved_Bags);
            cmd.Parameters.AddWithValue("@Moisture", Moisture);
            cmd.Parameters.AddWithValue("@WCM_no", WCM_no);
            cmd.Parameters.AddWithValue("@Variation_qty", Variation_qty);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@IsDeposit", IsDeposit);
            cmd.Parameters.AddWithValue("@IP_Address", IP_Address);
            cmd.Parameters.AddWithValue("@Created_date", Created_date);
            cmd.Parameters.AddWithValue("@Updated_date", Updated_date);
            cmd.Parameters.AddWithValue("@Challan_Status", Challan_Status);
            cmd.Parameters.AddWithValue("@Godown", Godown);
            cmd.Parameters.AddWithValue("@OperatorID", OperatorID);
            cmd.Parameters.AddWithValue("@State_Id", State_Id);
            cmd.Parameters.AddWithValue("@NoTransaction", NoTransaction);
            cmd.Parameters.AddWithValue("@Orderno", Orderno);
            cmd.Parameters.AddWithValue("@Branch", Branch);
            cmd.Parameters.AddWithValue("@typeofbags", typeofbags);
            cmd.Parameters.AddWithValue("@Rec_PP_bags", Rec_PP_bags);
            cmd.Parameters.AddWithValue("@Rec_Jute_bags", Rec_Jute_bags);
            cmd.Parameters.AddWithValue("@Rec_OU_bags", Rec_OU_bags);
            cmd.Parameters.AddWithValue("@Sent_Jute_bags", Sent_Jute_bags);
            cmd.Parameters.AddWithValue("@Sent_OU_bags", Sent_OU_bags);
            cmd.Parameters.AddWithValue("@Sent_PP_bags", Sent_PP_bags);
            cmd.Parameters.AddWithValue("@whr", whr);
            cmd.Parameters.AddWithValue("@StackNumber", StackNumber);
            cmd.Parameters.AddWithValue("@stackName", stackName);
            cmd.Parameters.AddWithValue("@Stacknum", Stacknum);
            cmd.Parameters.AddWithValue("@slogin_name", slogin_name);
            cmd.Parameters.AddWithValue("@slogin_mob", slogin_mob);
            cmd.Parameters.AddWithValue("@slogin_des", slogin_des);
            cmd.Parameters.AddWithValue("@Updated_LgnName", Updated_LgnName);
            cmd.Parameters.AddWithValue("@updated_LgnMobile", updated_LgnMobile);
            cmd.Parameters.AddWithValue("@updated_LgnDesgn", updated_LgnDesgn);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            //cmd.Parameters["@Result"].Value.ToString();
            ResponseMsg = cmd.Parameters["@Result"].Value.ToString();
            Context.Response.Write(ResponseMsg);
        }


        [WebMethod]
        public void EDUpdateReceiptDetails(string Dist_Id, string Depot_ID, string Receipt_id, string S_of_arrival, string S_name, string A_dist, string A_Depo, string RO_No,
         string TO_Number, DateTime Dispatch_Date, DateTime arrival_date, string challan_no, DateTime challan_date, double Qty, string Commodity, string Scheme,
         string Crop_year, string Category, string Transporter, string Vehile_no, string Arrival_time, string Gunny_type, int No_of_Bags, double Recd_Qty,
         int Recieved_Bags, double Moisture, string WCM_no, double Variation_qty, int Month, int Year, string IsDeposit, string IP_Address, DateTime Created_date,
         DateTime Updated_date, string Challan_Status, string Godown, string OperatorID, string State_Id, string NoTransaction, string Orderno, string Branch, string typeofbags,
         int Rec_PP_bags, int Rec_Jute_bags, int Rec_OU_bags, int Sent_Jute_bags, int Sent_OU_bags, int Sent_PP_bags, string whr, string StackNumber, string stackName, string Stacknum, 
         string slogin_name, string slogin_mob, string slogin_des, string Updated_LgnName, string updated_LgnMobile, string updated_LgnDesgn)
        {
            string ResponseMsg = null;
            SqlDataAdapter da = new SqlDataAdapter();
            String str = ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand("usp_ExportReceiptDetailsDataInMPSCSC_MPWLC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar, 250);
            cmd.Parameters.AddWithValue("@CommandType", "2");
            cmd.Parameters.AddWithValue("@Dist_Id", Dist_Id);
            cmd.Parameters.AddWithValue("@Depot_ID", Depot_ID);
            cmd.Parameters.AddWithValue("@Receipt_id", Receipt_id);
            cmd.Parameters.AddWithValue("@S_of_arrival", S_of_arrival);
            cmd.Parameters.AddWithValue("@S_name", S_name);
            cmd.Parameters.AddWithValue("@A_dist", A_dist);
            cmd.Parameters.AddWithValue("@A_Depo", A_Depo);
            cmd.Parameters.AddWithValue("@RO_No", RO_No);
            cmd.Parameters.AddWithValue("@TO_Number", TO_Number);
            cmd.Parameters.AddWithValue("@Dispatch_Date", Dispatch_Date);
            cmd.Parameters.AddWithValue("@arrival_date", arrival_date);
            cmd.Parameters.AddWithValue("@challan_no", challan_no);
            cmd.Parameters.AddWithValue("@challan_date", challan_date);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Commodity", Commodity);
            cmd.Parameters.AddWithValue("@Scheme", Scheme);
            cmd.Parameters.AddWithValue("@Crop_year", Crop_year);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@Transporter", Transporter);
            cmd.Parameters.AddWithValue("@Vehile_no", Vehile_no);
            cmd.Parameters.AddWithValue("@Arrival_time", Arrival_time);
            cmd.Parameters.AddWithValue("@Gunny_type", Gunny_type);
            cmd.Parameters.AddWithValue("@No_of_Bags", No_of_Bags);
            cmd.Parameters.AddWithValue("@Recd_Qty", Recd_Qty);
            cmd.Parameters.AddWithValue("@Recieved_Bags", Recieved_Bags);
            cmd.Parameters.AddWithValue("@Moisture", Moisture);
            cmd.Parameters.AddWithValue("@WCM_no", WCM_no);
            cmd.Parameters.AddWithValue("@Variation_qty", Variation_qty);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@IsDeposit", IsDeposit);
            cmd.Parameters.AddWithValue("@IP_Address", IP_Address);
            cmd.Parameters.AddWithValue("@Created_date", Created_date);
            cmd.Parameters.AddWithValue("@Updated_date", Updated_date);
            cmd.Parameters.AddWithValue("@Challan_Status", Challan_Status);
            cmd.Parameters.AddWithValue("@Godown", Godown);
            cmd.Parameters.AddWithValue("@OperatorID", OperatorID);
            cmd.Parameters.AddWithValue("@State_Id", State_Id);
            cmd.Parameters.AddWithValue("@NoTransaction", NoTransaction);
            cmd.Parameters.AddWithValue("@Orderno", Orderno);
            cmd.Parameters.AddWithValue("@Branch", Branch);
            cmd.Parameters.AddWithValue("@typeofbags", typeofbags);
            cmd.Parameters.AddWithValue("@Rec_PP_bags", Rec_PP_bags);
            cmd.Parameters.AddWithValue("@Rec_Jute_bags", Rec_Jute_bags);
            cmd.Parameters.AddWithValue("@Rec_OU_bags", Rec_OU_bags);
            cmd.Parameters.AddWithValue("@Sent_Jute_bags", Sent_Jute_bags);
            cmd.Parameters.AddWithValue("@Sent_OU_bags", Sent_OU_bags);
            cmd.Parameters.AddWithValue("@Sent_PP_bags", Sent_PP_bags);
            cmd.Parameters.AddWithValue("@whr", whr);
            cmd.Parameters.AddWithValue("@StackNumber", StackNumber);
            cmd.Parameters.AddWithValue("@stackName", stackName);
            cmd.Parameters.AddWithValue("@Stacknum", Stacknum);
            cmd.Parameters.AddWithValue("@slogin_name", slogin_name);
            cmd.Parameters.AddWithValue("@slogin_mob", slogin_mob);
            cmd.Parameters.AddWithValue("@slogin_des", slogin_des);
            cmd.Parameters.AddWithValue("@Updated_LgnName", Updated_LgnName);
            cmd.Parameters.AddWithValue("@updated_LgnMobile", updated_LgnMobile);
            cmd.Parameters.AddWithValue("@updated_LgnDesgn", updated_LgnDesgn);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            //cmd.Parameters["@Result"].Value.ToString();
            ResponseMsg = cmd.Parameters["@Result"].Value.ToString();
            Context.Response.Write(ResponseMsg);
        }

        [WebMethod]
        public void EDDeleteReceiptDetails(string Receipt_id, string challan_no, string Godown) 
        {
            string ResponseMsg = null;
            SqlDataAdapter da = new SqlDataAdapter();
            String str = ConfigurationManager.ConnectionStrings["MPSCSCConnectionString"].ConnectionString.ToString();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = str;
            con.Open();
            cmd = new SqlCommand("usp_ExportReceiptDetailsDataInMPSCSC_MPWLC_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Result", SqlDbType.VarChar, 250);
            cmd.Parameters.AddWithValue("@CommandType", "3");
            cmd.Parameters.AddWithValue("@Receipt_id", Receipt_id);
            cmd.Parameters.AddWithValue("@challan_no", challan_no);
            cmd.Parameters.AddWithValue("@Godown", Godown);
            cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            //cmd.Parameters["@Result"].Value.ToString();
            ResponseMsg = cmd.Parameters["@Result"].Value.ToString();
            Context.Response.Write(ResponseMsg);


        }
    }
}

