using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;

public partial class DepotAndGodownMap : System.Web.UI.Page
{
    // Ensure "FCIConnectionString" is defined in your Web.config
    private static string dbConnStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    [WebMethod]
    public static string GetDepots()
    {
        DataTable dt = new DataTable();
        string sql = @"SELECT DepotID, DepotName, DepotAddress, latitude, longitude 
                       FROM tbl_MetaData_DEPOT 
                       WHERE latitude IS NOT NULL AND longitude IS NOT NULL 
                       AND ISNUMERIC(latitude) = 1 AND ISNUMERIC(longitude) = 1
                       AND CAST(latitude AS DECIMAL(18,2)) != 0.0;";

        using (SqlConnection conn = new SqlConnection(dbConnStr))
        {
            // Corrected: (sql string, connection object)
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return DataTableToJSON(dt);
    }

    [WebMethod]
    public static string GetGodownsByDepot(string depotId)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(dbConnStr))
        {
            // Note: Integrated your custom Distance_By_LATLONG function
            string sql = @"
                SELECT tt.District_Name, tt.DepotName, tt.Hired_Type, tt.Godown_Name, 
                       tt.Latitude, tt.Longitude, tt.distance, tt.Godown_ID
                FROM (
                    SELECT dst.District_Name, MD.DepotName, G.Hired_Type, G.Godown_Name, G.Godown_ID,
                           G.Latitude, G.Longitude,
                           CONVERT(DECIMAL(10,2), [dbo].[Distance_By_LATLONG](
                                G.Latitude, G.Longitude, MD.latitude, MD.longitude
                           )) AS distance
                    FROM tbl_MetaData_GODOWN_2018 G
                    INNER JOIN tbl_MetaData_DEPOT MD ON G.BranchID = MD.BranchId
                    INNER JOIN tbl_MetaData_DISTRICT dst ON MD.DistrictId = dst.District_Id
                    WHERE MD.DepotID = @DepotID
                    AND ISNUMERIC(G.Latitude) = 1 AND G.Latitude <> '0'
                    AND ISNUMERIC(G.Longitude) = 1 AND G.Longitude <> '0'
                ) tt
                ORDER BY tt.distance";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@DepotID", depotId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return DataTableToJSON(dt);
    }

    public static string DataTableToJSON(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = 2147483647; // Handle large data

        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        foreach (DataRow row in table.Rows)
        {
            Dictionary<string, object> childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                // Corrected: Handle nulls to avoid breaking JSON
                childRow.Add(col.ColumnName, row[col] == DBNull.Value ? "" : row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
}