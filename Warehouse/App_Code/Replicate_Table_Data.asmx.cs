using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

namespace ReplicateService
{
    /// <summary>
    /// Summary description for Replicate_Table_Data
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Replicate_Table_Data : System.Web.Services.WebService
    {

        [WebMethod(Description="Fetch each table data")]

        public string FetchDataHere(string tbl, string noOfRecord, string fromDate, string toDate)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                if (tbl == "tbl_Storage_GatePass_Enrty")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT top "+noOfRecord+" * FROM " + tbl + " WHERE CONVERT(date, Issue_Date, 101) BETWEEN '"+fromDate+"' and '"+toDate+"' order by Issue_Date desc"))
                    {
                        try
                        {
                            cmd.Connection = con;
                            DataSet ds = new DataSet();
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds, "" + tbl + "");
                                con.Close();
                            }
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                else if(tbl == "tbl_Truck_Chit_Getpass_Entry")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT top "+noOfRecord+" * FROM " + tbl + " WHERE CONVERT(date, Insert_date, 101) BETWEEN '" + fromDate + "' and '" + toDate + "' order by Insert_date desc"))
                    {
                        try
                        {
                            cmd.Connection = con;
                            DataSet ds = new DataSet();
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds, "" + tbl + "");
                                con.Close();
                            }
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                else if(tbl == "tbl_Aepds_Truckchit_Data_DisGodown")
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT top "+noOfRecord+" * FROM " + tbl + " WHERE CONVERT(date, Created_Date, 101) BETWEEN '"+fromDate+"' and '"+toDate+"' order by Created_Date desc"))
                    {
                        try
                        {
                            cmd.Connection = con;
                            DataSet ds = new DataSet();
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds, "" + tbl + "");
                                con.Close();
                            }
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT top "+ noOfRecord +" * FROM " + tbl + " WHERE CONVERT(date, CreatedDate, 101) BETWEEN '"+fromDate+"' and '"+toDate+"' order by CreatedDate desc"))
                    {
                        try
                        {
                            cmd.Connection = con;
                            DataSet ds = new DataSet();
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds, "" + tbl + "");
                                con.Close();
                            }
                            HttpContext.Current.Response.Clear();
                            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

    }
}
