using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ExcelDataReader;

public partial class JointVentureScheme_PaymentTransaction_FileUploadBulkNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) { }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (!upFile.HasFile)
            {
                lblmsg.Text = "Please select a file to upload.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string extension = Path.GetExtension(upFile.FileName).ToLower();
            string filename = Path.GetFileNameWithoutExtension(upFile.FileName) + "_" +
                              DateTime.Now.ToString("yyyyMMddHHmmss") + extension;

            string folderPath = Server.MapPath("~/Audit/");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, filename);
            upFile.SaveAs(filePath);

            DataTable dtPayment;

            if (extension == ".xls" || extension == ".xlsx")
                dtPayment = GetDataTableFromExcel(filePath);
            else if (extension == ".csv")
                dtPayment = GetDataTableFromCsv(filePath);
            else
                throw new Exception("Only .xls, .xlsx, or .csv files are supported.");

            if (dtPayment == null || dtPayment.Rows.Count == 0)
                throw new Exception("No data found in uploaded file.");

            string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(consString))
            {
                con.Open();

                // Remove duplicates based on primary key BankReferenceNo
                DataTable existingKeys = new DataTable();
                using (SqlCommand cmd = new SqlCommand("SELECT BankReferenceNo FROM dbo.tbl_Payment_Status", con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(existingKeys);
                }

                HashSet<string> existingKeySet = new HashSet<string>();
                foreach (DataRow row in existingKeys.Rows)
                {
                    existingKeySet.Add(row["BankReferenceNo"].ToString());
                }

                for (int i = dtPayment.Rows.Count - 1; i >= 0; i--)
                {
                    string key = dtPayment.Rows[i]["Bank Reference No"].ToString();
                    if (existingKeySet.Contains(key))
                        dtPayment.Rows.RemoveAt(i);
                }

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = "dbo.tbl_Payment_Status";

                    sqlBulkCopy.ColumnMappings.Add("Category Name", "CategoryName");
                    sqlBulkCopy.ColumnMappings.Add("Payment Mode", "PaymentMode");
                    sqlBulkCopy.ColumnMappings.Add("Bank Reference No", "BankReferenceNo");
                    sqlBulkCopy.ColumnMappings.Add("Transaction Date", "TransactionDate");
                    sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                    sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                    sqlBulkCopy.ColumnMappings.Add("REGISTRATION ID", "REGISTRATIONID");
                    sqlBulkCopy.ColumnMappings.Add("NAME OF DEPOSITOR", "NAMEOFDEPOSITOR");
                    sqlBulkCopy.ColumnMappings.Add("CONTACT NO.", "CONTACTNO");
                    sqlBulkCopy.ColumnMappings.Add("EMAIL ID", "EMAILID");
                    sqlBulkCopy.ColumnMappings.Add("FEE", "FEE");
                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");

                    sqlBulkCopy.WriteToServer(dtPayment);
                }
                con.Close();
            }

            if (File.Exists(filePath))
                File.Delete(filePath);

            lblmsg.Text = "File uploaded and data inserted successfully!";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        catch (ExcelDataReader.Exceptions.HeaderException)
        {
            lblmsg.Text = "Invalid Excel file. Please remove extra rows before header or save as proper Excel.";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    private DataTable GetDataTableFromExcel(string filePath)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            IExcelDataReader reader = null;
            string ext = Path.GetExtension(filePath).ToLower();

            try
            {
                if (ext == ".xls")
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                else if (ext == ".xlsx")
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                else
                    throw new Exception("Unsupported Excel file format.");
            }
            catch (ExcelDataReader.Exceptions.HeaderException)
            {
                throw new Exception("The file '" + Path.GetFileName(filePath) +
                                    "' is not a valid Excel " + ext + " file. Please save it as a proper Excel workbook.");
            }

            var conf = new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = false
                }
            };

            using (reader)
            {
                var dataSet = reader.AsDataSet(conf);
                if (dataSet.Tables.Count == 0)
                    throw new Exception("No worksheet found in Excel file.");

                DataTable dt = dataSet.Tables[0].Copy();

                // Find header row dynamically
                int headerRowIndex = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Trim() == "Category Name")
                    {
                        headerRowIndex = i;
                        break;
                    }
                }
                if (headerRowIndex == -1)
                    throw new Exception("No valid header row ('Category Name') found in Excel file.");

                DataTable dtClean = new DataTable();
                for (int c = 0; c < dt.Columns.Count; c++)
                    dtClean.Columns.Add(dt.Rows[headerRowIndex][c].ToString().Trim());

                for (int i = headerRowIndex + 1; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtClean.NewRow();
                    for (int j = 0; j < dtClean.Columns.Count && j < dt.Columns.Count; j++)
                        dr[j] = dt.Rows[i][j];
                    dtClean.Rows.Add(dr);
                }

                CleanAndFormatData(dtClean);
                return dtClean;
            }
        }
    }

    private DataTable GetDataTableFromCsv(string csvPath)
    {
        DataTable dt = new DataTable();
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0) return dt;

        int headerIndex = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("Category Name"))
            {
                headerIndex = i;
                break;
            }
        }

        string[] headers = lines[headerIndex].Split(',');
        foreach (string h in headers) dt.Columns.Add(h.Trim());

        for (int i = headerIndex + 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length == 0) continue;

            DataRow dr = dt.NewRow();
            for (int j = 0; j < headers.Length && j < values.Length; j++)
                dr[j] = values[j].Trim();
            dt.Rows.Add(dr);
        }

        CleanAndFormatData(dt);
        return dt;
    }

    private void CleanAndFormatData(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            if (dt.Columns.Contains("Transaction Date"))
            {
                string trandtformat = dr["Transaction Date"].ToString();
                if (string.IsNullOrWhiteSpace(trandtformat) || trandtformat == "null" || trandtformat == "-")
                    trandtformat = "01/01/1900";

                DateTime parsedDate;
                if (DateTime.TryParse(trandtformat, out parsedDate))
                    dr["Transaction Date"] = parsedDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                else
                    dr["Transaction Date"] = "1900-01-01";
            }

            if (dt.Columns.Contains("Amount"))
            {
                string amt = dr["Amount"].ToString();
                if (string.IsNullOrWhiteSpace(amt) || amt == "null" || amt == "-") amt = "0";
                dr["Amount"] = amt;
            }

            if (dt.Columns.Contains("FEE"))
            {
                string fee = dr["FEE"].ToString();
                if (string.IsNullOrWhiteSpace(fee) || fee == "null" || fee == "-") fee = "0";
                dr["FEE"] = fee;
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
}
