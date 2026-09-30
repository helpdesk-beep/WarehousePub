using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class StatePages_Stack_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Page load remains empty as DataTable uses the WebMethod
    }

    [WebMethod]
    public static string GetStackData()
    {
        string connString = ConfigurationManager.ConnectionStrings["ConstrMoisture"].ConnectionString;

        // Define Dates
        string currDate = DateTime.Now.ToString("yyyy-MM-dd");
        string prevDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connString))
        {
            // The query pulls data for all 7 columns shown in your image
            string query = @"SELECT 
    ROW_NUMBER() OVER (ORDER BY g.godown_Name) AS [S.No],
    g.godown_Name AS [Godown Name],

    -- Previous Date Stack
    ISNULL(SUM(CASE 
        WHEN CAST(m.Create_on AS DATE) <= @PrevDate 
        THEN 1 ELSE 0 END),0) 
    AS [Prev Stack],

    -- Current Date Stack
    ISNULL(SUM(CASE 
        WHEN CAST(m.Create_on AS DATE) <= @CurrDate 
        THEN 1 ELSE 0 END),0) 
    AS [Current Stack],

    -- Total Stack (Difference or same as current)
    ISNULL(SUM(CASE 
        WHEN CAST(m.Create_on AS DATE) <= @CurrDate 
        THEN 1 ELSE 0 END),0)
    AS [Total Stack],

    -- Moisture Entry Count
    ISNULL(COUNT(m.Stack_ID),0) 
    AS [Moisture Entry]

    -- Sent to DM (Assuming Flag/Column exists)
    --ISNULL(SUM(CASE 
    --    WHEN m.Is_Sent_DM = 1 THEN 1 ELSE 0 END),0) 
    --AS [Sent to DM]

FROM Integrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 g
Inner JOIN tbl_Stack_Wise_Moisture_Entry_By_BM m 
    ON g.Godown_ID = m.Godown_ID COLLATE SQL_Latin1_General_CP1_CI_AS
         
GROUP BY g.godown_Name
ORDER BY g.godown_Name";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CurrDate", currDate);
                cmd.Parameters.AddWithValue("@PrevDate", prevDate);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
        }

        return SerializeDataTable(dt);
    }

    private static string SerializeDataTable(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            var row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns) { row.Add(col.ColumnName, dr[col]); }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    private static string DataTableToJSON(DataTable table)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        // C# 4.0 may need a larger limit for big datasets
        serializer.MaxJsonLength = 2147483647;
        return serializer.Serialize(list);
    }
}