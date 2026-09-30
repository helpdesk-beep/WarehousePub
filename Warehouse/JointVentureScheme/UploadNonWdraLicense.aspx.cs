using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public partial class JointVentureScheme_UploadNonWdraLicense : System.Web.UI.Page
{
    string sqlConnectionString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Security: Prevent back button after logout[cite: 5]
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        // Session check and user display[cite: 3, 5]
        if (Session["UserName"] != null)
        {
            lblUser.Text = Session["UserName"].ToString();
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }

        //if (!IsPostBack)
        //{
        //    BindDistricts();
        //}
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            try
            {
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string folderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                FileUpload1.SaveAs(filePath);

                ProcessImport(filePath, rbListType.SelectedValue);
            }
            catch (Exception ex) { ShowMessage("Error: " + ex.Message, System.Drawing.Color.Red); }
        }
        else { ShowMessage("Please select an Excel file first.", System.Drawing.Color.Red); }
    }

    private void ProcessImport(string filePath, string type)
    {
        string excelConStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'", filePath);

        using (OleDbConnection excelConn = new OleDbConnection(excelConStr))
        {
            try
            {
                excelConn.Open();
                DataTable dtSheet = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();

                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", sheetName), excelConn);
                DataTable dtExcel = new DataTable();
                da.Fill(dtExcel);

                int totalExcelRows = dtExcel.Rows.Count;
                string targetTable = (type == "WDRA") ? "tbl_WDRA_Registration" : "tbl_LicencesRegistration_2020";
                string primaryKey = (type == "WDRA") ? "WHCode" : "Whr_ID";

                using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
                {
                    sqlConn.Open();
                    // 1. Create a schema-matched temporary table
                    new SqlCommand(string.Format("SELECT TOP 0 * INTO #TempRepo FROM {0}", targetTable), sqlConn).ExecuteNonQuery();

                    // 2. Bulk copy Excel rows into staging
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn))
                    {
                        bulkCopy.DestinationTableName = "#TempRepo";
                        MapColumns(bulkCopy, type);
                        bulkCopy.WriteToServer(dtExcel);
                    }

                    // 3. Merge data: Insert only if Primary Key doesn't exist
                    string mergeSql = GetMergeSql(targetTable, primaryKey, type);
                    SqlCommand cmdMerge = new SqlCommand(mergeSql, sqlConn);
                    int rowsInserted = cmdMerge.ExecuteNonQuery();

                    // 4. Calculate skips
                    int rowsSkipped = totalExcelRows - rowsInserted;

                    ShowMessage(string.Format("<b>{0} Import Complete!</b><br/>{1} new records added.<br/>{2} duplicate records were skipped.",
                        type, rowsInserted, rowsSkipped), System.Drawing.Color.Green);
                }
            }
            catch (Exception ex) { ShowMessage("Import Error: " + ex.Message, System.Drawing.Color.Red); }
            finally { excelConn.Close(); }
        }
    }

    private void MapColumns(SqlBulkCopy bulkCopy, string type)
    {
        if (type == "WDRA")
        {
            bulkCopy.ColumnMappings.Add("NameandAddress", "NameandAddress");
            bulkCopy.ColumnMappings.Add("WarehousemanName", "WarehousemanName");
            bulkCopy.ColumnMappings.Add("Mobile", "Mobile");
            bulkCopy.ColumnMappings.Add("Capacity", "Capacity");
            bulkCopy.ColumnMappings.Add("WHCode", "WHCode");
            bulkCopy.ColumnMappings.Add("RegistrationDate", "RegistrationDate");
            bulkCopy.ColumnMappings.Add("Validupto", "Validupto");
            bulkCopy.ColumnMappings.Add("WarehouseName", "WarehouseName");
            bulkCopy.ColumnMappings.Add("WarehousemanID", "WarehousemanID");
            bulkCopy.ColumnMappings.Add("District", "District");
        }
        else
        {
            bulkCopy.ColumnMappings.Add("Whr_ID", "Whr_ID");
            bulkCopy.ColumnMappings.Add("APPL_CODE", "APPL_CODE");
            bulkCopy.ColumnMappings.Add("Whr_Name", "Whr_Name");
            bulkCopy.ColumnMappings.Add("DISTRICT_NAME", "DISTRICT_NAME");
            bulkCopy.ColumnMappings.Add("District_ID", "District_ID");
            bulkCopy.ColumnMappings.Add("Total_capicity", "Total_capicity");
            bulkCopy.ColumnMappings.Add("Name_of_Owner", "Name_of_Owner");
            bulkCopy.ColumnMappings.Add("IssuedAnugyptiDate", "IssuedAnugyptiDate");
            bulkCopy.ColumnMappings.Add("IssuedAnugyptivalidityDate", "IssuedAnugyptivalidityDate");
        }
    }

    private string GetMergeSql(string table, string pk, string type)
    {
        string cols = (type == "WDRA")
            ? "NameandAddress, WarehousemanName, Mobile, Capacity, WHCode, RegistrationDate, Validupto, WarehouseName, WarehousemanID, District"
            : "Whr_ID, APPL_CODE, Whr_Name, DISTRICT_NAME, District_ID, Total_capicity, Name_of_Owner, IssuedAnugyptiDate, IssuedAnugyptivalidityDate";

        return string.Format("INSERT INTO {0} ({1}) SELECT {1} FROM #TempRepo t WHERE NOT EXISTS (SELECT 1 FROM {0} m WHERE m.{2} = t.{2})", table, cols, pk);
    }

    private void ShowMessage(string msg, System.Drawing.Color color) { lblMessage.Text = msg; lblMessage.ForeColor = color; }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        // Redirect to home page as requested
        Response.Redirect("DistrictWiseJVSOffer.aspx");
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        // Clear session and redirect to login
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Logins.aspx");
    }
}