using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;

public partial class DepotAndGodownMapNewRoadMap : System.Web.UI.Page
{
    private static string dbConnStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    [WebMethod]
    public static string GetDepots()
    {
        DataTable dt = new DataTable();
       // SQL Filter for non-zero, valid numeric coordinates in MP [cite: 2]
        string sql = @"SELECT DepotID, DepotName, DepotAddress, latitude, longitude 
                       FROM tbl_MetaData_DEPOT 
                       WHERE latitude IS NOT NULL AND longitude IS NOT NULL 
                       AND ISNUMERIC(latitude) = 1 AND ISNUMERIC(longitude) = 1
                       AND CAST(latitude AS DECIMAL(18,2)) != 0.0;";

        using (SqlConnection conn = new SqlConnection(dbConnStr))
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return DataTableToJSON(dt);
    }

    public static string DataTableToJSON(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer { MaxJsonLength = 2147483647 };
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col] == DBNull.Value ? "" : row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
}