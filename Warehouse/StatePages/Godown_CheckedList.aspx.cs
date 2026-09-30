using iTextSharp.text.pdf.qrcode;
using System;
using System.Activities.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
public partial class Godown_CheckedList : System.Web.UI.Page
{
    // --- Connection Strings ---
    private static string StaticConnStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    private string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();

    // ----------------- Data Models -----------------

    // Data structure for the Summary Grid 
    public class RegionSummary
    {
        public string Region { get; set; }
        public decimal Received_Weight { get; set; }
        public decimal Delivered_Weight { get; set; }
        public decimal Loss { get; set; }
        public decimal Gain { get; set; }
        public decimal Gain_Percentage { get; set; }
    }

    // Data structure for the Detail Grid (Expanded to match all requested columns)
    public class RegionDetail
    {
        public string Region { get; set; } // Added Region for filtering
        public string District { get; set; }
        public string Branch { get; set; }
        public string Godown { get; set; }
        public string Godown_Type { get; set; }
        public string Crop_Year { get; set; } // New
        public int Received_Bags { get; set; } // New
        public decimal Received_Weight { get; set; }
        public int Delivered_Bags { get; set; } // New
        public decimal Delivered_Weight { get; set; }
        public decimal Loss { get; set; }
        public decimal Gain { get; set; }
        public decimal Gain_Percentage { get; set; }
        public string No_Of_Days { get; set; } // New
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

    // ----------------- Dropdown Binding Methods (UNCHANGED) -----------------

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
                    ((DropDownList)control).Items.Insert(0, new System.Web.UI.WebControls.ListItem(defaultText, string.Empty));
                }
            }
        }
    }


    // ----------------- AJAX WebMethods -----------------

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
        // ... (Logic for the regular HTML detail grid remains the same, using DetailQuerySQL) ...
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
            DataTable dt = ExecuteQuery(DetailQuerySQL, quotedCropYears, unquotedCommodityIDs, quotedGodownTypes, quotedRegionName);
            return ConvertDataTableToHtmlTable(dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetRegionDetailGrid: " + ex.Message);
            return "<span style='color:red;'>Error executing detail query: " + HttpUtility.HtmlEncode(ex.Message) + "</span>";
        }
    }

    // ----------------- CONSOLIDATED EXPORT WEB METHOD (OPTIMIZED) -----------------

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

            // 2. Fetch ALL Child Detail Data for ALL regions in a SINGLE BATCH CALL
            List<RegionDetail> allRegionDetails = GetAllDetailsBatch(cropYears, commodityIDs, godownTypes);

            // 3. Loop through Parent Summary and MERGE with filtered Detail Data
            foreach (var regionSummary in parentSummaryData)
            {
                // Add the PARENT row first (formatted to stand out in Excel)
                consolidatedList.Add(new
                {
                    // SUMMARY ROW: Only contains key aggregated values
                    RegionDetail = "REGION: " + regionSummary.Region,
                    District = string.Empty, // Blanked out for alignment
                    Branch = string.Empty,
                    Godown = string.Empty,
                    Godown_Type = string.Empty,
                    Crop_Year = string.Empty,
                    Received_Bags = (int?)null,
                    Received_Weight = regionSummary.Received_Weight,
                    Delivered_Bags = (int?)null,
                    Delivered_Weight = regionSummary.Delivered_Weight,
                    Loss = regionSummary.Loss,
                    Gain = regionSummary.Gain,
                    Gain_Percentage = regionSummary.Gain_Percentage,
                    No_Of_Days = string.Empty
                });

                // Get only the relevant details for the current region from the batch list
                var regionDetails = allRegionDetails
                    .Where(d => d.Region.Equals(regionSummary.Region, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Add CHILD rows
                foreach (var detail in regionDetails)
                {
                    consolidatedList.Add(new
                    {
                        // DETAIL ROW: Contains all requested columns
                        RegionDetail = "> " + detail.Region,
                        detail.District,
                        detail.Branch,
                        detail.Godown,
                        detail.Godown_Type,
                        detail.Crop_Year,
                        detail.Received_Bags,
                        detail.Received_Weight,
                        detail.Delivered_Bags,
                        detail.Delivered_Weight,
                        detail.Loss,
                        detail.Gain,
                        detail.Gain_Percentage,
                        detail.No_Of_Days
                    });
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetConsolidatedExportData: " + ex.Message);
            return new List<object>();
        }

        return consolidatedList;
    }

    // ----------------- PRIVATE HELPER FOR CONSOLIDATED DATA (OPTIMIZED) -----------------

    /// <summary>
    /// Executes the ConsolidatedDetailQuerySQL once to retrieve ALL detail data 
    /// for all regions matching the selected filters.
    /// </summary>
    private static List<RegionDetail> GetAllDetailsBatch(string cropYears, string commodityIDs, string godownTypes)
    {
        List<RegionDetail> detailList = new List<RegionDetail>();

        // 1. Prepare filter strings
        string quotedCropYears = string.Join(",", cropYears.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());
        string quotedGodownTypes = string.Join(",", godownTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(s => "'" + s.Replace("'", "''") + "'").ToArray());
        string unquotedCommodityIDs = commodityIDs;

        // CRITICAL CHECK: If any input is missing, return empty immediately.
        if (string.IsNullOrEmpty(quotedCropYears) || string.IsNullOrEmpty(unquotedCommodityIDs) || string.IsNullOrEmpty(quotedGodownTypes))
        {
            return detailList;
        }

        try
        {
            // Execute the query once. Pass "null" for parameter {3} (RegionName) 
            DataTable dt = ExecuteQuery(ConsolidatedDetailQuerySQL, quotedCropYears, unquotedCommodityIDs, quotedGodownTypes, null);

            foreach (DataRow row in dt.Rows)
            {
                detailList.Add(new RegionDetail
                {
                    Region = row["Region"] != DBNull.Value ? row["Region"].ToString() : string.Empty,
                    District = row["District"] != DBNull.Value ? row["District"].ToString() : string.Empty,
                    Branch = row["Branch"] != DBNull.Value ? row["Branch"].ToString() : string.Empty,
                    Godown = row["Godown"] != DBNull.Value ? row["Godown"].ToString() : string.Empty,
                    Godown_Type = row["Godown Type"] != DBNull.Value ? row["Godown Type"].ToString() : string.Empty,
                    Crop_Year = row["Crop Year"] != DBNull.Value ? row["Crop Year"].ToString() : string.Empty,
                    // Parse values
                    Received_Bags = row["Received Bags"] != DBNull.Value ? Convert.ToInt32(row["Received Bags"]) : 0,
                    Received_Weight = row["Received Weight"] != DBNull.Value ? (decimal)row["Received Weight"] : 0m,
                    Delivered_Bags = row["Delivered Bags"] != DBNull.Value ? Convert.ToInt32(row["Delivered Bags"]) : 0,
                    Delivered_Weight = row["Delivered Weight"] != DBNull.Value ? (decimal)row["Delivered Weight"] : 0m,
                    Loss = row["Loss"] != DBNull.Value ? (decimal)row["Loss"] : 0m,
                    Gain = row["Gain"] != DBNull.Value ? (decimal)row["Gain"] : 0m,
                    Gain_Percentage = row["Gain %"] != DBNull.Value ? (decimal)row["Gain %"] : 0m,
                    No_Of_Days = row["No Of Days"] != DBNull.Value ? row["No Of Days"].ToString() : string.Empty
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetAllDetailsBatch: " + ex.Message);
        }

        return detailList;
    }


    // ----------------- SQL Queries -----------------

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


    // ----------------- CONSOLIDATED DETAIL SQL QUERY (OPTIMIZED - NO REGION FILTER {3}) -----------------
    private const string ConsolidatedDetailQuerySQL = @"
        SELECT
            ttt.Regionnm AS [Region],
            ttt.District_Name AS [District],
            ttt.DepotName AS [Branch],
            ttt.Godown_Name AS [Godown],
            ttt.Hired_Type AS [Godown Type],
            ttt.CropYear AS [Crop Year], 
            ttt.recbags AS [Received Bags],
            CAST(ttt.recweght AS DECIMAL(18, 2)) AS [Received Weight],
            ttt.delbags AS [Delivered Bags],
            CAST(ttt.delwght AS DECIMAL(18, 2)) AS [Delivered Weight],
            CAST(ttt.Loss AS DECIMAL(18, 2)) [Loss],
            CAST(ttt.Gain - ttt.Loss AS DECIMAL(18, 2)) AS [Gain],
            CAST(ttt.Gain * 100 / NULLIF(ttt.recweght, 0) AS DECIMAL(18, 2)) AS [Gain %],
            CDays.days_difference AS [No Of Days]
        FROM
            (
                SELECT
                    WHR.CropYear,
                    dst.Regionnm,
                    dst.District_Name,
                    MD.DepotName,
                    gdn.Godown_Name,
                    gdn.Hired_Type,
                    gdn.Godown_ID,
                    SUM(tt.recbags) AS recbags,
                    SUM(tt.recweght) AS recweght,
                    SUM(tt.delbags) AS delbags,
                    SUM(tt.delwght) AS delwght,
                    SUM(tt.Loss) AS Loss,
                    SUM(tt.Gain) AS Gain,
                    ISNULL((SUM(tt.delwght) - SUM(tt.Gain)) + SUM(tt.Loss), 0) AS ActualWeight
                FROM
                    tbl_MetaData_GODOWN_2018 gdn
                INNER JOIN tbl_MetaData_DEPOT MD
                    ON gdn.BranchID = MD.BranchId
                INNER JOIN tbl_MetaData_DISTRICT dst
                    ON MD.DistrictId = dst.District_Id
                INNER JOIN tbl_storage_Depositor_WHR_Relation WHR
                    ON gdn.Godown_ID = WHR.GodownID
                INNER JOIN
                    (
                        SELECT
                            [Depositor_WHR_Id],
                            [Commodity_Id],
                            [recbags],
                            [recweght],
                            [Loss],
                            [Gain],
                            [delbags],
                            [delwght]
                        FROM
                            [Intergrated_MP_STORAGE].[dbo].[uiondata]
                        WHERE
                            Commodity_Id IN ({1})
                    ) tt
                    ON WHR.Whr_No = tt.Depositor_WHR_Id
                WHERE
                    WHR.DepositorID = '129'
                    AND WHR.CropYear IN ({0})
                    AND gdn.Hired_Type IN ({2})
                GROUP BY
                    WHR.CropYear,
                    dst.Regionnm,
                    dst.District_Name,
                    MD.DepotName,
                    gdn.Godown_Name,
                    gdn.Hired_Type,
                    gdn.Godown_ID
            ) ttt
        INNER JOIN
            (
                SELECT
                    Godown_ID,
                    CropYear,
                    CONVERT(varchar(20), DATEDIFF(day, tt.First_WHR_Issue_Date, tt.Last_DeliveryDate)) AS days_difference
                FROM
                    (
                        SELECT
                            A.Godown_ID,
                            B.CropYear,
                            MIN(A.[WHR_Issue_Date]) AS First_WHR_Issue_Date,
                            MAX(A.[DeliveryDate]) AS Last_DeliveryDate
                        FROM
                            [Intergrated_MP_STORAGE].[dbo].[uiondata] A
                        INNER JOIN tbl_storage_Depositor_WHR_Relation B
                            ON A.Depositor_WHR_Id = B.Whr_No
                        WHERE
                            A.Commodity_Id IN ({1})
                            AND B.CropYear IN ({0})
                            AND B.DepositorID = '129'
                        GROUP BY
                            A.Godown_ID,
                            B.CropYear
                    ) tt
            ) CDays
            ON CDays.Godown_ID = ttt.Godown_ID
            AND CDays.CropYear = ttt.CropYear
        WHERE
            ttt.recweght - ttt.ActualWeight = 0
        ORDER BY
            ttt.Regionnm,
            ttt.District_Name,
            ttt.DepotName,
            ttt.Godown_Name;
    ";


    // ----------------- Data Execution and Conversion Helpers (UNCHANGED) -----------------

    public static DataTable ExecuteQuery(string sqlQuery, string cropYears, string commodityIDs, string godownTypes, string regionName)
    {
        // {0} = CropYears (Quoted), {1} = CommodityIDs (Unquoted), {2} = GodownTypes (Quoted), {3} = RegionName (Quoted - or null)
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
        // ... (HTML table conversion remains the same) ...
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



   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        // 1. Get Selected Crop Years
        var selectedYears = ddlCropYear.Items.Cast<System.Web.UI.WebControls.ListItem>()
                                .Where(li => li.Selected)
                                // Use String.Replace to prevent SQL Injection if ListBox values contained single quotes.
                                // We are using standard string formatting here, not interpolation.
                                .Select(li => "'" + li.Value.Replace("'", "''") + "'")
                                .ToList();

        if (selectedYears.Count == 0)
        {
            // Handle case where no crop year is selected
            // e.g., show a message
            return;
        }

        // This creates a string like: "'2022-23','2023-24'"
        string cropYearFilter = string.Join(",", selectedYears.ToArray()); // Use ToArray() for compatibility

        // 2. Define the SQL Query using string.Format() with placeholders {0}

        // Note: The verbatim string prefix (@) is still valid in C# 4.0 for multi-line strings.
        string baseQuery = @"
            SELECT
                ttt.Regionnm AS [Region],
                ttt.District_Name AS [District],
                ttt.DepotName AS [Branch],
                ttt.Godown_Name AS [Godown],
                ttt.Hired_Type AS [Godown Type],
                ttt.CropYear AS [Crop Year],
                ttt.recbags AS [Received Bags],
                CAST(ttt.recweght AS DECIMAL(18, 2)) AS [Received Weight],
                ttt.delbags AS [Delivered Bags],
                CAST(ttt.delwght AS DECIMAL(18, 2)) AS [Delivered Weight],
                CAST(ttt.Loss AS DECIMAL(18, 2)) [Loss],
                CAST(ttt.Gain - ttt.Loss AS DECIMAL(18, 2)) AS [Gain],
                CAST(ttt.Gain * 100 / NULLIF(ttt.recweght, 0) AS DECIMAL(18, 2)) AS [Gain %],
                CDays.days_difference AS [No Of Days]
            FROM
                (
                    SELECT
                        CropYear,
                        dst.Regionnm,
                        dst.District_Name,
                        MD.DepotName,
                        gdn.Godown_Name,
                        gdn.Hired_Type,
                        gdn.Godown_ID,
                        SUM(tt.recbags) AS recbags,
                        SUM(tt.recweght) AS recweght,
                        SUM(tt.delbags) AS delbags,
                        SUM(tt.delwght) AS delwght,
                        SUM(tt.Loss) AS Loss,
                        SUM(tt.Gain) AS Gain,
                        ISNULL((SUM(tt.delwght) - SUM(tt.Gain)) + SUM(tt.Loss), 0) AS ActualWeight
                    FROM
                        tbl_MetaData_GODOWN_2018 gdn
                    INNER JOIN tbl_MetaData_DEPOT MD
                        ON gdn.BranchID = MD.BranchId
                    INNER JOIN tbl_MetaData_DISTRICT dst
                        ON MD.DistrictId = dst.District_Id
                    INNER JOIN tbl_storage_Depositor_WHR_Relation WHR
                        ON gdn.Godown_ID = WHR.GodownID
                    INNER JOIN
                        (
                            SELECT
                                [Depositor_WHR_Id],
                                [Category_Id],
                                [commodity],
                                [Depositor_Name],
                                [GatePass_No],
                                [Issue_Source_ID],
                                [DeliveryDate],
                                [Delmoisture],
                                [Mode_of_weighment],
                                [AvgMoisture_Content],
                                [MktValue_of_Commodity],
                                [WHR_Issue_Date],
                                [datecom],
                                [AvgMoisture_Content_To],
                                [BranchID],
                                [Godown_ID],
                                [Branch],
                                [Commodity_Id],
                                CONVERT(BIGINT, ROW_NUMBER() OVER (ORDER BY datecom)) ROW_NUM,
                                [recbags],
                                [recweght],
                                [Loss],
                                [Gain],
                                [delbags],
                                [delwght],
                                ([AvgMoisture_Content] + [AvgMoisture_Content_To]) / 2 AS Moisture
                            FROM
                                [Intergrated_MP_STORAGE].[dbo].[uiondata]
                            WHERE
                                Commodity_Id = '22'
                        ) tt
                        ON WHR.Whr_No = tt.Depositor_WHR_Id
                    WHERE
                        WHR.DepositorID = '129'
                        AND CropYear IN ({0}) -- First placeholder for cropYearFilter
                    GROUP BY
                        dst.Regionnm,
                        dst.District_Name,
                        MD.DepotName,
                        gdn.Godown_Name,
                        gdn.Hired_Type,
                        gdn.Godown_ID,
                        CropYear
                ) ttt
            INNER JOIN
                (
                    SELECT
                        Godown_ID,
                        CropYear,
                        CONVERT(varchar(20), DATEDIFF(day, tt.First_WHR_Issue_Date, tt.Last_DeliveryDate)) AS days_difference
                    FROM
                        (
                            SELECT
                                Godown_ID,
                                B.CropYear,
                                MIN(A.[WHR_Issue_Date]) AS First_WHR_Issue_Date,
                                MAX(A.[DeliveryDate]) AS Last_DeliveryDate
                            FROM
                                [Intergrated_MP_STORAGE].[dbo].[uiondata] A
                            INNER JOIN tbl_storage_Depositor_WHR_Relation B
                                ON A.Depositor_WHR_Id = B.Whr_No
                            WHERE
                                A.Commodity_Id = '22'
                                AND B.DepositorID = '129'
                                AND B.CropYear IN ({0}) -- Second placeholder for cropYearFilter
                            GROUP BY
                                A.Godown_ID,
                                B.CropYear
                        ) tt
                ) CDays
                ON CDays.Godown_ID = ttt.Godown_ID
                AND CDays.CropYear = ttt.CropYear
            WHERE
                ttt.recweght - ttt.ActualWeight = 0
            ORDER BY
                ttt.Regionnm,
                ttt.District_Name,
                ttt.DepotName,
                ttt.Godown_Name;
        ";

        // Apply the filter using String.Format. 
        // We pass cropYearFilter once, and it replaces all {0} placeholders.
        string sqlQuery = string.Format(baseQuery, cropYearFilter);

        // 3. Retrieve Data into a DataTable
        DataTable dt = new DataTable();
        // **IMPORTANT**: Replace "MyConnectionString" with your actual connection string name from Web.config
      

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }

        // 4. Export DataTable to Excel
        ExportDataTableToExcel(dt, "GodownReport");
    }

    // --- EXPORT METHOD (Unchanged from previous response) ---
    private void ExportDataTableToExcel(DataTable dt, string fileName)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            // Optional: Show message to user
            return;
        }

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");

        GridView gv = new GridView();
        gv.DataSource = dt;
        gv.DataBind();

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        gv.EnableViewState = false;
        gv.RenderControl(htw);

        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }


    // ... (inside public partial class Godown_CheckedList) ...

    protected void btnExportPDF_Click(object sender, EventArgs e)
    {
        // 1. Get Selected Crop Years
        var selectedYears = ddlCropYear.Items.Cast<System.Web.UI.WebControls.ListItem>()
                                .Where(li => li.Selected)
                                .Select(li => "'" + li.Value.Replace("'", "''") + "'")
                                .ToList();

        if (selectedYears.Count == 0)
        {
            // Optional: Show message to user
            return;
        }

        string cropYearFilter = string.Join(",", selectedYears.ToArray());

        // The provided SQL query (same as the one in btnExport_Click)
        string baseQuery = @"
            SELECT
                ttt.Regionnm AS [Region],
                ttt.District_Name AS [District],
                ttt.DepotName AS [Branch],
                ttt.Godown_Name AS [Godown],
                ttt.Hired_Type AS [Godown Type],
                ttt.CropYear AS [Crop Year],
                ttt.recbags AS [Received Bags],
                CAST(ttt.recweght AS DECIMAL(18, 2)) AS [Received Weight],
                ttt.delbags AS [Delivered Bags],
                CAST(ttt.delwght AS DECIMAL(18, 2)) AS [Delivered Weight],
                CAST(ttt.Loss AS DECIMAL(18, 2)) [Loss],
                CAST(ttt.Gain - ttt.Loss AS DECIMAL(18, 2)) AS [Gain],
                CAST(ttt.Gain * 100 / NULLIF(ttt.recweght, 0) AS DECIMAL(18, 2)) AS [Gain %],
                CDays.days_difference AS [No Of Days]
            FROM
                (
                    SELECT
                        CropYear,
                        dst.Regionnm,
                        dst.District_Name,
                        MD.DepotName,
                        gdn.Godown_Name,
                        gdn.Hired_Type,
                        gdn.Godown_ID,
                        SUM(tt.recbags) AS recbags,
                        SUM(tt.recweght) AS recweght,
                        SUM(tt.delbags) AS delbags,
                        SUM(tt.delwght) AS delwght,
                        SUM(tt.Loss) AS Loss,
                        SUM(tt.Gain) AS Gain,
                        ISNULL((SUM(tt.delwght) - SUM(tt.Gain)) + SUM(tt.Loss), 0) AS ActualWeight
                    FROM
                        tbl_MetaData_GODOWN_2018 gdn
                    INNER JOIN tbl_MetaData_DEPOT MD
                        ON gdn.BranchID = MD.BranchId
                    INNER JOIN tbl_MetaData_DISTRICT dst
                        ON MD.DistrictId = dst.District_Id
                    INNER JOIN tbl_storage_Depositor_WHR_Relation WHR
                        ON gdn.Godown_ID = WHR.GodownID
                    INNER JOIN
                        (
                            SELECT
                                [Depositor_WHR_Id], [Commodity_Id], [recbags], [recweght], [Loss], [Gain], [delbags], [delwght]
                            FROM
                                [Intergrated_MP_STORAGE].[dbo].[uiondata]
                            WHERE
                                Commodity_Id = '22'
                        ) tt
                        ON WHR.Whr_No = tt.Depositor_WHR_Id
                    WHERE
                        WHR.DepositorID = '129'
                        AND CropYear IN ({0})
                    GROUP BY
                        dst.Regionnm, dst.District_Name, MD.DepotName, gdn.Godown_Name, gdn.Hired_Type, gdn.Godown_ID, CropYear
                ) ttt
            INNER JOIN
                (
                    SELECT
                        Godown_ID,
                        CropYear,
                        CONVERT(varchar(20), DATEDIFF(day, tt.First_WHR_Issue_Date, tt.Last_DeliveryDate)) AS days_difference
                    FROM
                        (
                            SELECT
                                Godown_ID,
                                B.CropYear,
                                MIN(A.[WHR_Issue_Date]) AS First_WHR_Issue_Date,
                                MAX(A.[DeliveryDate]) AS Last_DeliveryDate
                            FROM
                                [Intergrated_MP_STORAGE].[dbo].[uiondata] A
                            INNER JOIN tbl_storage_Depositor_WHR_Relation B
                                ON A.Depositor_WHR_Id = B.Whr_No
                            WHERE
                                A.Commodity_Id = '22'
                                AND B.DepositorID = '129'
                                AND B.CropYear IN ({0})
                            GROUP BY
                                A.Godown_ID, B.CropYear
                        ) tt
                ) CDays
                ON CDays.Godown_ID = ttt.Godown_ID
                AND CDays.CropYear = ttt.CropYear
            WHERE
                ttt.recweght - ttt.ActualWeight = 0
            ORDER BY
                ttt.Regionnm, ttt.District_Name, ttt.DepotName, ttt.Godown_Name;
        ";

        string sqlQuery = string.Format(baseQuery, cropYearFilter);

        // 2. Retrieve Data into a DataTable
        DataTable dt = new DataTable();
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }

        // 3. Export DataTable to PDF
        ExportDataTableToPDF(dt, "GodownReport");
    }

    /// <summary>
    /// Exports the provided DataTable to a PDF file using iTextSharp.
    /// </summary>
    private void ExportDataTableToPDF(DataTable dt, string fileName)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            // Optional: Show message to user
            return;
        }

        // Set response headers for PDF download
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // Create the PDF document
        Document pdfDoc = new Document(PageSize.A2.Rotate(), 10f, 10f, 10f, 10f); // A2 Landscape for many columns

        try
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                pdfDoc.Open();

                // Title
                Font fontHeader = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                Paragraph title = new Paragraph("Consolidated Warehouse Report", fontHeader);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 15f;
                pdfDoc.Add(title);

                // Create a table with number of columns equal to the DataTable columns
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                table.WidthPercentage = 100;
                table.DefaultCell.BorderWidth = 1;
                table.DefaultCell.Padding = 3;
                table.DefaultCell.BorderColor = BaseColor.LIGHT_GRAY;

                // Add Header Row
                Font fontColumnHeader = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.WHITE);
                BaseColor headerColor = new BaseColor(66, 139, 202); // Bootstrap Primary Blue

                foreach (DataColumn column in dt.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, fontColumnHeader));
                    headerCell.BackgroundColor = headerColor;
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell);
                }
                table.HeaderRows = 1;

                // Add Data Rows
                Font fontData = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL);
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string cellValue = row[i].ToString();
                        PdfPCell dataCell = new PdfPCell(new Phrase(cellValue, fontData));

                        // Set alignment for numeric columns (adjust column indices as necessary)
                        if (IsNumeric(row[i]))
                        {
                            dataCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }
                        else
                        {
                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }

                        table.AddCell(dataCell);
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                // Write the PDF content to the response stream
                HttpContext.Current.Response.OutputStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., if PDF generation fails)
            System.Diagnostics.Debug.WriteLine("Error in ExportDataTableToPDF: " + ex.Message);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("Error exporting to PDF: " + HttpUtility.HtmlEncode(ex.Message));
            HttpContext.Current.Response.End();
        }
    }

    // Required override for RenderControl method
    public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
    {
        // Allows GridView to render outside an ASP.NET server form tag
    }
}