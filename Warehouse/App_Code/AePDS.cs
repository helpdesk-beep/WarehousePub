using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Text;
using System.Globalization;

/// <summary>
/// Summary description for AePDS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AePDS : System.Web.Services.WebService
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";

    string securityKey = "MPWLCNIC@2023";
    //APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    public AePDS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class InsertIntegrated
    {
        public string IsSuccess;
        public string statusResult;
    }
    [WebMethod(Description = "Web Service has to be Prepared by Warehouse team and will be Consumend by AePDS")]
    public void MovementOrderbyAePDS(string Key, string Movement_order_id, string allotment_month, string allotment_year, string District_id, string godown_id, string destination_id, string Total_allotment, string nfsa_Y_N, string scheme_type, string scheme_ID, string commodity_code, string quantity, string Unit, string Client_IP)
    {

        List<InsertIntegrated> list = new List<InsertIntegrated>();
        if (Key == securityKey)
        {
            try
            {
                string LocalIP = Client_IP;
                String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection();
                DataSet ds = new DataSet();
                con.ConnectionString = strConnString;
                SqlCommand cmd = new SqlCommand("SP_Movement_Order_Receive_by_Aepds", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Movement_order_id", Movement_order_id);
                cmd.Parameters.AddWithValue("@allotment_month", allotment_month);
                cmd.Parameters.AddWithValue("@allotment_year", allotment_year);
                cmd.Parameters.AddWithValue("@District_id", District_id);
                cmd.Parameters.AddWithValue("@godown_id", godown_id);
                cmd.Parameters.AddWithValue("@destination_id", destination_id);
                cmd.Parameters.AddWithValue("@Total_allotment", Total_allotment);
                cmd.Parameters.AddWithValue("@nfsa_Y_N", nfsa_Y_N);
                cmd.Parameters.AddWithValue("@scheme_type", scheme_type);
                cmd.Parameters.AddWithValue("@scheme_ID", scheme_ID);
                cmd.Parameters.AddWithValue("@commodity_code", commodity_code);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@Unit", Unit);
                cmd.Parameters.AddWithValue("@Insert_by", LocalIP);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {

                    InsertIntegrated ob = new InsertIntegrated();

                    ob.statusResult = "1";
                    ob.IsSuccess = "True";
                    list.Add(ob);
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<InsertIntegrated>>(list));
                }
                else
                {
                    InsertIntegrated ob = new InsertIntegrated();

                    ob.statusResult = "0";
                    ob.IsSuccess = "False";
                    list.Add(ob);
                    Context.Response.Write(JsonHelper.JsonSerializer<IList<InsertIntegrated>>(list));
                }
            }
            catch (Exception ex)
            {

                InsertIntegrated ob = new InsertIntegrated();

                ob.statusResult = "-1";
                ob.IsSuccess = "False";
                list.Add(ob);
                Context.Response.Write(JsonHelper.JsonSerializer<IList<InsertIntegrated>>(list));
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            InsertIntegrated ob = new InsertIntegrated();

            ob.statusResult = "2";
            ob.IsSuccess = "Enter valid Key";
            list.Add(ob);
            Context.Response.Write(JsonHelper.JsonSerializer<IList<InsertIntegrated>>(list));
        }
    }

    //// To Get offices Details of Phud Munshi to Fill Phad Dropdown/Phud_Munsi_ID means User_ID
    // [WebMethod]
    // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public void MovementOrderbyAePDS(string Key, string Movement_order_id, string allotment_month, string allotment_year, string District_id, string godown_id, string destination_id, string Total_allotment, string nfsa_Y_N, string scheme_type, string scheme_ID, string commodity_code, string quantity, string Unit, string Client_IP)
    // {
    //     DataSet ds = new DataSet();
    //     DataTable dt = new DataTable();
    //     System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //     this.Context.Response.ContentType = "application/json; charset=utf-8";
    //     if (Key == securityKey)
    //     {
    //         try
    //         {
    //             DataSet ds1 = new DataSet();


    //             ds1 = obj.ByProcedure("SP_Movement_Order_Receive_by_Aepds",
    //              new string[] { "Movement_order_id", "allotment_month", "allotment_year", "District_id", "godown_id", "destination_id", "Total_allotment", "nfsa_Y_N", "scheme_type", "scheme_ID", "commodity_code", "quantity", "Unit", "Insert_by" },
    //                new string[] { Movement_order_id.ToString(), allotment_month.ToString(), allotment_year.ToString(), District_id.ToString(), godown_id.ToString(), destination_id.ToString(), Total_allotment.ToString(), nfsa_Y_N.ToString(), scheme_type.ToString(), scheme_ID.ToString(), commodity_code.ToString(), quantity.ToString(), Unit.ToString(), Client_IP.ToString() }, "dataset");
    //             if (ds1.Tables[0].Rows.Count == 0)
    //             {
    //                 this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
    //             }
    //             else
    //             {

    //                 dt = ds1.Tables[0];
    //                 List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                 Dictionary<string, object> row = null;
    //                 foreach (DataRow rs in dt.Rows)
    //                 {
    //                     row = new Dictionary<string, object>();
    //                     foreach (DataColumn col in dt.Columns)
    //                     {
    //                         row.Add(col.ColumnName, rs[col]);
    //                     }
    //                     rows.Add(row);
    //                 }
    //                 this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

    //             }

    //         }
    //         catch (Exception ex)
    //         {
    //             this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //         }

    //         ds.Clear();
    //         dt.Clear();
    //     }
    //     else
    //     {
    //         this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //     }
    // }


}
