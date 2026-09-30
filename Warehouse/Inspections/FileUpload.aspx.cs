using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
public partial class Inspections_FileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        if (FileUploadProduct.HasFile)
        {
            try
            {
                string path = string.Concat(Server.MapPath("~/ Excel /” +FileUploadProduct.FileName));
                FileUploadProduct.SaveAs(path);
                string excelConnectionString = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = { 0 }; Extended Properties = Excel 8.0″, path);
                OleDbConnection connection = new OleDbConnection();
                connection.ConnectionString = excelConnectionString;
                OleDbCommand command = new OleDbCommand("select * from[Sheet1$]”, connection);
                connection.Open();
                DbDataReader dr = command.ExecuteReader();
                string sqlConnectionString = @”Data Source =.; Initial Catalog = Wordpress; Integrated Security = True”;
                SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
                bulkInsert.DestinationTableName = "tbl_bulkupload”;
                bulkInsert.WriteToServer(dr);
                MsgAlert.Text = "Product uploaded successfully”;
                connection.Close();
            }
            catch (Exception ex)
            {
                MsgAlert.Text = ex.Message;
            }
        }