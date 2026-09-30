using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization; // Explicitly add this for serializing the final object

public partial class Goldown_CheckedList : System.Web.UI.Page
{
    // --- Connection Strings ---
    private static string StaticConnStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    private string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();

    // ----------------- Data Models -----------------

    // Data structure for the Summary Grid (MUST match properties in JS DataTables)
    public class RegionSummary
    {
        public string Region { get; set; }
        public decimal Received_Weight { get; set; }
        public decimal Delivered_Weight { get; set; }
        public decimal Loss { get; set; }
        public decimal Gain { get; set; }
        public decimal Gain_Percentage { get; set; }
    }

    // Data structure for the Detail Grid (Used for C# side list generation)
    public class RegionDetail
    {
        public string District { get; set; }
        public string Branch { get; set; }
        public string Godown { get; set; }
        public string Godown_Type { get; set; }
        // Note: The following properties align with the JavaScript export structure
        public decimal Received_Weight { get; set; }
        public decimal Delivered_Weight { get; set; }
        public decimal Loss { get; set; }
        public decimal Gain { get; set; }
        public decimal Gain_Percentage { get; set; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCropYear();
            BindCommodity();
            BindGodownType();
        }
    }

    // ----------------- Dropdown Binding Methods -----------------

    private void BindCropYear()
    {
        string query = "select distinct CropYear from tbl_storage_Depositor_WHR_Relation where Cropyear not in ('','--Select--','0','All','Before 2009','Before 2013','Before 2014') ORDER BY CropYear DESC";
        BindDropdown(ddlCropYear, query, "CropYear", "CropYear");
    }

    private void BindCommodity()
    {
        string query = "SELECT DISTINCT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY ORDER BY Commodity_Name";
        BindDropdown(ddlCommodity, query, "Commodity_Name", "Commodity_Id");
    }

    private void BindGodownType()
    {
        string query = "SELECT DISTINCT Hired_Type AS Godown_Type FROM tbl_MetaData_GODOWN_2018 ORDER BY Godown_Type";
        BindDropdown(ddlGodownType, query, "Godown_Type", "Godown_Type");
    }

    private void BindDropdown(ListControl control, string query, string textField, string valueField, string defaultText = null)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                control.DataSource = dt;
                control.DataTextField = textField;
                control.DataValueField = valueField;
                control.DataBind();

                if (!string.IsNullOrEmpty(defaultText) && control is DropDownList)
                {
                    ((DropDownList)control).Items.Insert(0, new ListItem(defaultText, string.Empty));
                }
            }
        }
    }


    // ----------------- AJAX WebMethods (Client-Side Binding) -----------------

    [WebMethod]
    public static List<RegionSummary> GetRegionSummary(string cropYears, string commodityIDs, string godownTypes)
    {
        // 1. Prepare filter strings for SQL
        string quotedCropYears = string.Join(",", cropYears.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());
        string quotedGodownTypes = string.Join(",", godownTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());

        string unquotedCommodityIDs = commodityIDs;

        if (string.IsNullOrEmpty(quotedCropYears) || string.IsNullOrEmpty(unquotedCommodityIDs) || string.IsNullOrEmpty(quotedGodownTypes))
        {
            return new List<RegionSummary>();
        }

        try
        {
            // The last parameter (regionName) is null for the summary query
            DataTable dt = ExecuteQuery(SummaryQuerySQL, quotedCropYears, unquotedCommodityIDs, quotedGodownTypes, null);

            // 2. Convert DataTable to List<RegionSummary>
            List<RegionSummary> summaryList = new List<RegionSummary>();
            foreach (DataRow row in dt.Rows)
            {
                summaryList.Add(new RegionSummary
                {
                    Region = row["Region"] != DBNull.Value ? row["Region"].ToString() : string.Empty,
                    Received_Weight = row["Received Weight"] != DBNull.Value ? (decimal)row["Received Weight"] : 0m,
                    Delivered_Weight = row["Delivered Weight"] != DBNull.Value ? (decimal)row["Delivered Weight"] : 0m,
                    Loss = row["Loss"] != DBNull.Value ? (decimal)row["Loss"] : 0m,
                    Gain = row["Gain"] != DBNull.Value ? (decimal)row["Gain"] : 0m,
                    Gain_Percentage = row["Gain %"] != DBNull.Value ? (decimal)row["Gain %"] : 0m
                });
            }
            return summaryList;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetRegionSummary: " + ex.Message);
            return new List<RegionSummary>();
        }
    }

    [WebMethod]
    public static string GetRegionDetailGrid(string regionName, string cropYears, string commodityIDs, string godownTypes)
    {
        // 1. Prepare filter strings
        string quotedCropYears = string.Join(",", cropYears.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());
        string quotedGodownTypes = string.Join(",", godownTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());
        string unquotedCommodityIDs = commodityIDs;

        string quotedRegionName = !string.IsNullOrEmpty(regionName) ? "'" + regionName.Replace("'", "''") + "'" : "''";


        if (string.IsNullOrEmpty(quotedCropYears) || string.IsNullOrEmpty(unquotedCommodityIDs) || string.IsNullOrEmpty(quotedGodownTypes))
        {
            return "<span style='color:red;'>Missing required search filters.</span>";
        }

        try
        {
            // Pass all four parameters to execute the detail query
            DataTable dt = ExecuteQuery(DetailQuerySQL, quotedCropYears, unquotedCommodityIDs, quotedGodownTypes, quotedRegionName);

            // 2. Convert the DataTable to an HTML table string for client-side rendering
            return ConvertDataTableToHtmlTable(dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetRegionDetailGrid: " + ex.Message);
            return "<span style='color:red;'>Error executing detail query: " + HttpUtility.HtmlEncode(ex.Message) + "</span>";
        }
    }

    // ----------------- 🌟 NEW: CONSOLIDATED EXPORT WEB METHOD 🌟 -----------------

    /// <summary>
    /// Fetches all parent summary data and all corresponding child detail data, 
    /// combining them into a single list for Excel export.
    /// </summary>
    [WebMethod]
    public static List<object> GetConsolidatedExportData(string cropYears, string commodityIDs, string godownTypes)
    {
        var consolidatedList = new List<object>();

        try
        {
            // 1. Fetch ALL Parent Summary Data
            List<RegionSummary> parentSummaryData = GetRegionSummary(cropYears, commodityIDs, godownTypes);

            foreach (var regionSummary in parentSummaryData)
            {
                // Add the PARENT row first (formatted to stand out in Excel)
                consolidatedList.Add(new
                {
                    // This property name MUST match the one used in the JavaScript exportData function
                    RegionDetail = "REGION: " + regionSummary.Region,
                    regionSummary.Received_Weight,
                    regionSummary.Delivered_Weight,
                    regionSummary.Loss,
                    regionSummary.Gain,
                    regionSummary.Gain_Percentage
                });

                // 2. Fetch Child Detail Data for the current region
                List<RegionDetail> regionDetails = GetDetailsList(regionSummary.Region, cropYears, commodityIDs, godownTypes);

                // 3. Add CHILD rows
                foreach (var detail in regionDetails)
                {
                    consolidatedList.Add(new
                    {
                        // Identifies as detail row in Excel. Includes key detail info.
                        RegionDetail = "> " + detail.Godown + " (" + detail.Godown_Type + ") - Branch: " + detail.Branch,
                        // Map detail fields to the summary fields for consistent column alignment
                        detail.Received_Weight,
                        detail.Delivered_Weight,
                        detail.Loss,
                        detail.Gain,
                        detail.Gain_Percentage
                    });
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetConsolidatedExportData: " + ex.Message);
            // Return an empty list on failure
            return new List<object>();
        }

        // Return the list directly. ASP.NET handles the JSON serialization { "d": [...] }
        return consolidatedList;
    }

    // ----------------- PRIVATE HELPER FOR CONSOLIDATED DATA -----------------

    /// <summary>
    /// Executes the detail query and converts the result to a List<RegionDetail> object, 
    /// avoiding the HTML conversion for the export method.
    /// </summary>
    private static List<RegionDetail> GetDetailsList(string regionName, string cropYears, string commodityIDs, string godownTypes)
    {
        List<RegionDetail> detailList = new List<RegionDetail>();

        // 1. Prepare filter strings
        string quotedCropYears = string.Join(",", cropYears.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(s => "'" + s.Replace("'", "") + "'").ToArray());
        string quotedGodownTypes = string.Join(",", godownTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => "'" + s.Replace("'", "") + "'").ToArray());
        string unquotedCommodityIDs = commodityIDs;
        string quotedRegionName = !string.IsNullOrEmpty(regionName) ? "'" + regionName.Replace("'", "") + "'" : "''";

        if (string.IsNullOrEmpty(quotedCropYears) || string.IsNullOrEmpty(unquotedCommodityIDs) || string.IsNullOrEmpty(quotedGodownTypes))
        {
            return detailList;
        }

        try
        {
            DataTable dt = ExecuteQuery(DetailQuerySQL, quotedCropYears, unquotedCommodityIDs, quotedGodownTypes, quotedRegionName);

            foreach (DataRow row in dt.Rows)
            {
                // Note: The detail query column names are slightly different (e.g., "[Received Weight (x/10)]")
                // We must parse them to get the final decimal values for the export list.
                detailList.Add(new RegionDetail
                {
                    District = row["District"] != DBNull.Value ? row["District"].ToString() : string.Empty,
                    Branch = row["Branch"] != DBNull.Value ? row["Branch"].ToString() : string.Empty,
                    Godown = row["Godown"] != DBNull.Value ? row["Godown"].ToString() : string.Empty,
                    Godown_Type = row["Godown Type"] != DBNull.Value ? row["Godown Type"].ToString() : string.Empty,

                    // Parse the Decimal values from the DataTable based on the SQL output
                    Received_Weight = row["Received Weight (x/10)"] != DBNull.Value ? (decimal)row["Received Weight (x/10)"] : 0m,
                    Delivered_Weight = row["Delivered Weight (x/10)"] != DBNull.Value ? (decimal)row["Delivered Weight (x/10)"] : 0m,
                    Loss = row["Loss (x/10)"] != DBNull.Value ? (decimal)row["Loss (x/10)"] : 0m,
                    Gain = row["Gain (x/10)"] != DBNull.Value ? (decimal)row["Gain (x/10)"] : 0m,
                    Gain_Percentage = row["Gain %"] != DBNull.Value ? (decimal)row["Gain %"] : 0m
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetDetailsList: " + ex.Message);
        }

        return detailList;
    }


    // ----------------- SQL Queries (UNCHANGED) -----------------

    private const string SummaryQuerySQL = @"
        SELECT
            A.Region,
            CAST(SUM(A.[Received Weight])/10 AS DECIMAL(18,2)) AS [Received Weight],
            CAST(SUM(A.[Delivered Weight])/10 AS DECIMAL(18,2)) AS [Delivered Weight],
            CAST(SUM(A.[Gain])/10 AS DECIMAL(18,2)) AS [Gain],
            CAST(SUM(A.[Loss])/10 AS DECIMAL(18,2)) AS [Loss],
            CAST((SUM(A.[Gain])*100 / NULLIF(SUM(A.[Received Weight]), 0)) AS DECIMAL(18,2)) AS [Gain %]
        FROM (
            SELECT
                dst.Regionnm AS [Region],
                SUM(ttt.recweght) AS [Received Weight],
                SUM(ttt.delwght) AS [Delivered Weight],
                SUM(ttt.Loss) AS [Loss],
                SUM(ttt.Gain) AS [Gain]
            FROM tbl_MetaData_GODOWN_2018 gdn
            INNER JOIN tbl_MetaData_DEPOT MD
                ON gdn.BranchID = MD.BranchId
            INNER JOIN tbl_MetaData_DISTRICT dst
                ON MD.DistrictId = dst.District_Id
            INNER JOIN tbl_storage_Depositor_WHR_Relation WHR
                ON gdn.Godown_ID = WHR.GodownID
            INNER JOIN (
                SELECT 
                    [Depositor_WHR_Id],
                    [Godown_ID],
                    [Commodity_Id],
                    [recweght],
                    [Loss],
                    [Gain],
                    [delwght]
                FROM [Intergrated_MP_STORAGE].[dbo].[uiondata]
                WHERE Commodity_Id IN ({1})
            ) ttt 
                ON WHR.Whr_No = ttt.Depositor_WHR_Id
            WHERE 
                WHR.DepositorID = '129'
                AND WHR.CropYear IN ({0})
                AND gdn.Hired_Type IN ({2})
            GROUP BY
                dst.Regionnm, gdn.Godown_ID
            HAVING 
                SUM(ttt.recweght) - ISNULL((SUM(ttt.delwght) - SUM(ttt.Gain)) + SUM(ttt.Loss), 0) = 0
        ) A
        GROUP BY 
            A.Region
        ORDER BY 
            A.Region;
    ";


    private const string DetailQuerySQL = @"
        SELECT
            distinct ttt.District_Name AS [District],
            ttt.DepotName AS [Branch],
            ttt.Godown_Name AS [Godown],
            ttt.Hired_Type AS [Godown Type],
            ttt.recbags AS [Received Bags],
            CAST(ttt.recweght/10 AS DECIMAL(18,2)) AS [Received Weight (x/10)],
            ttt.delbags AS [Delivered Bags],
            CAST(ttt.delwght/10 AS DECIMAL(18,2)) AS [Delivered Weight (x/10)],
            CAST(ttt.Loss/10 AS decimal(18,2)) AS [Loss (x/10)],
            CAST((ttt.Gain-ttt.Loss)/10 AS DECIMAL(18,2)) AS [Gain (x/10)],
            CAST(ttt.Gain * 100 / NULLIF(ttt.recweght,0) AS DECIMAL(18,2)) AS [Gain %],
            CDays.days_difference AS [No Of Days]
        FROM (
            SELECT
                dst.Regionnm, dst.District_Name, MD.DepotName, gdn.Godown_Name, gdn.Hired_Type, gdn.Godown_ID, 
                SUM(tt.recbags) AS recbags, SUM(tt.recweght) AS recweght, SUM(tt.delbags) AS delbags, SUM(tt.delwght) AS delwght, 
                SUM(tt.Loss) AS Loss, SUM(tt.Gain) AS Gain, 
                ISNULL((SUM(tt.delwght) - SUM(tt.Gain)) + SUM(tt.Loss), 0) AS ActualWeight
            FROM tbl_MetaData_GODOWN_2018 gdn
            INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID = MD.BranchId
            INNER JOIN tbl_MetaData_DISTRICT dst ON MD.DistrictId = dst.District_Id
            INNER JOIN tbl_storage_Depositor_WHR_Relation WHR ON gdn.Godown_ID = WHR.GodownID
            INNER JOIN (
                SELECT Depositor_WHR_Id, Godown_ID, Commodity_Id, recbags, recweght, Loss, Gain, delbags, delwght
                FROM [Intergrated_MP_STORAGE].[dbo].[uiondata]
                WHERE Commodity_Id IN ({1})
            ) tt ON WHR.Whr_No = tt.Depositor_WHR_Id
            WHERE
                WHR.CropYear IN ({0})
                AND gdn.Hired_Type IN ({2})
                AND WHR.DepositorID = '129' 
                AND dst.Regionnm IN ({3}) -- Filter by Region Name
            GROUP BY dst.Regionnm, dst.District_Name, MD.DepotName, gdn.Godown_Name, gdn.Hired_Type, gdn.Godown_ID
        ) ttt
        INNER JOIN (
            SELECT Godown_ID, CropYear, CONVERT(varchar(20), DATEDIFF(day, tt.First_WHR_Issue_Date, tt.Last_DeliveryDate)) AS days_difference
            FROM (
                SELECT A.Godown_ID, B.CropYear, MIN(A.[WHR_Issue_Date]) AS First_WHR_Issue_Date, MAX(A.[DeliveryDate]) AS Last_DeliveryDate
                FROM [Intergrated_MP_STORAGE].[dbo].[uiondata] A
                INNER Join tbl_storage_Depositor_WHR_Relation B ON A.Depositor_WHR_Id=B.Whr_No
                WHERE
                    A.Commodity_Id IN ({1})
                    AND B.CropYear IN ({0})
                    AND B.DepositorID = '129'
                GROUP BY A.Godown_ID, B.CropYear
            ) tt
        ) CDays ON CDays.Godown_ID=ttt.Godown_ID 
        INNER JOIN tbl_storage_Depositor_WHR_Relation WHR_Alias ON WHR_Alias.GodownID = ttt.Godown_ID AND WHR_Alias.CropYear = CDays.CropYear
        WHERE ttt.recweght - ttt.ActualWeight = 0
        ORDER BY ttt.District_Name, ttt.DepotName, ttt.Godown_Name;
    ";


    // ----------------- Data Execution and Conversion Helpers (UNCHANGED) -----------------

    public static DataTable ExecuteQuery(string sqlQuery, string cropYears, string commodityIDs, string godownTypes, string regionName)
    {
        // {0} = CropYears (Quoted), {1} = CommodityIDs (Unquoted), {2} = GodownTypes (Quoted), {3} = RegionName (Quoted)
        string finalQuery = string.Format(sqlQuery, cropYears, commodityIDs, godownTypes, regionName);

        using (SqlConnection conn = new SqlConnection(StaticConnStr))
        {
            using (SqlCommand cmd = new SqlCommand(finalQuery, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }

    private static string ConvertDataTableToHtmlTable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='slider'><table class='nestedTable'>");

        // Header Row
        sb.Append("<thead><tr>");
        foreach (DataColumn column in dt.Columns)
        {
            sb.AppendFormat("<th>{0}</th>", HttpUtility.HtmlEncode(column.ColumnName));
        }
        sb.Append("</tr></thead>");

        // Data Rows
        sb.Append("<tbody>");
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                string cellValue = row[column.ColumnName].ToString();

                // Check if the value is numeric and format it if so (C# 4.0 style)
                if (IsNumeric(row[column.ColumnName]))
                {
                    decimal numericValue;
                    if (Decimal.TryParse(cellValue, out numericValue))
                    {
                        // Format all numeric values to N2 (comma thousands separator, 2 decimal places)
                        cellValue = numericValue.ToString("N2");
                    }
                }
                sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(cellValue));
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table></div>");
        return sb.ToString();
    }

    private static bool IsNumeric(object value)
    {
        return value is sbyte || value is byte || value is short || value is ushort
                    || value is int || value is uint || value is long || value is ulong
                    || value is float || value is double || value is decimal;
    }

    [WebMethod]
    public static string GetNestedGridData(string regionName, string godownId, string cropYears, string commodityIDs, string godownTypes)
    {
        try
        {
            // Delegate the call to the existing, fully implemented method
            return GetRegionDetailGrid(regionName, cropYears, commodityIDs, godownTypes);
        }
        catch (Exception ex)
        {
            return "<span style='color:red;'>Error retrieving nested data: " + HttpUtility.HtmlEncode(ex.Message) + "</span>";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // This is now client-side driven.
    }
}