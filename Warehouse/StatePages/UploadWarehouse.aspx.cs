using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.UI;

public partial class StatePages_UploadWarehouse : Page
{
    private string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();

    protected void Page_Load(object sender, EventArgs e) { }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (fuExcel.HasFile)
        {
            try
            {
                string fileName = Path.GetFileName(fuExcel.FileName);
                string folderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, fileName);
                fuExcel.SaveAs(filePath);

                DataTable dt = ReadExcelFile(filePath);

                if (dt.Rows.Count > 0)
                {
                    gvPreview.DataSource = dt;
                    gvPreview.DataBind();
                    Session["ExcelData"] = dt;
                    btnUpload.Enabled = true;
                    ShowMessage("Preview loaded. Confirm column order: [ID] then [Capacity].", System.Drawing.Color.Blue);
                }
                else
                {
                    ShowMessage("The file is empty.", System.Drawing.Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("File Error: " + ex.Message, System.Drawing.Color.Red);
            }
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["ExcelData"] as DataTable;
        if (dt != null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    con.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.DestinationTableName = "tbl_Warehouse_Capacity";

                        // Positional Mapping (Safest method)
                        bulkCopy.ColumnMappings.Add(0, "GodownID");
                        bulkCopy.ColumnMappings.Add(1, "GodownCapacity");

                        bulkCopy.WriteToServer(dt);
                    }
                }
                ShowMessage("Successfully uploaded " + dt.Rows.Count + " records.", System.Drawing.Color.Green);
                btnUpload.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowMessage("Database Update Error: " + ex.Message, System.Drawing.Color.Red);
            }
        }
    }

    private DataTable ReadExcelFile(string filePath)
    {
        string excelConnString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'", filePath);
        using (OleDbConnection excelConn = new OleDbConnection(excelConnString))
        {
            excelConn.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", excelConn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    private void ShowMessage(string msg, System.Drawing.Color color)
    {
        lblMessage.Text = msg;
        lblMessage.ForeColor = color;
    }
}