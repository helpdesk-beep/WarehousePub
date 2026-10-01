using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

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
            WarehouseApiSecurity.RequireApiKey();

            int recordCount;
            DateTime startDate;
            DateTime endDate;
            if (!Int32.TryParse(noOfRecord, NumberStyles.None, CultureInfo.InvariantCulture, out recordCount) ||
                recordCount < 1 || recordCount > 10000 ||
                !DateTime.TryParse(fromDate, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out startDate) ||
                !DateTime.TryParse(toDate, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out endDate) ||
                startDate.Date > endDate.Date)
            {
                throw new SoapException("Invalid record count or date range.", SoapException.ClientFaultCode);
            }

            string dateColumn;
            switch (tbl)
            {
                case "tbl_Storage_GatePass_Enrty":
                    dateColumn = "Issue_Date";
                    break;
                case "tbl_Truck_Chit_Getpass_Entry":
                    dateColumn = "Insert_date";
                    break;
                case "tbl_Aepds_Truckchit_Data_DisGodown":
                    dateColumn = "Created_Date";
                    break;
                default:
                    throw new SoapException("Requested table is not available.", SoapException.ClientFaultCode);
            }

            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT TOP (@RecordCount) * FROM [" + tbl + "] WHERE CONVERT(date, [" + dateColumn + "], 101) BETWEEN @FromDate AND @ToDate ORDER BY [" + dateColumn + "] DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Value = recordCount;
                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = startDate.Date;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = endDate.Date;
                    DataSet ds = new DataSet();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds, tbl);
                    }

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                    return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                }
            }
        }

    }
}
