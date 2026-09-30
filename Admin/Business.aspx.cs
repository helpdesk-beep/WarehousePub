using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Data.SqlClient;

public partial class Admin_Business : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload and save the file
            string excelPath = Server.MapPath("business_excel_file//") + Path.GetFileName(upFile.PostedFile.FileName);
            upFile.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(upFile.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            conString = string.Format(conString, excelPath);
            OleDbConnection excel_con = new OleDbConnection(conString);
            excel_con.Open();
            DataTable dt = new DataTable();
            OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", excel_con);
            oda.Fill(dt);
            excel_con.Close();


            string consString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            SqlConnection con = new SqlConnection(consString);
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con);

            //Set the database table name
            sqlBulkCopy.DestinationTableName = "dbo.CapacityUtilize";


            //[OPTIONAL]: Map the Excel columns with that of the database table


            sqlBulkCopy.ColumnMappings.Add("Year", "Year");
            sqlBulkCopy.ColumnMappings.Add("Owned", "Owned");
            sqlBulkCopy.ColumnMappings.Add("Hired", "Hired");
            sqlBulkCopy.ColumnMappings.Add("JVS", "JVS");
            sqlBulkCopy.ColumnMappings.Add("Total", "Total");
            sqlBulkCopy.ColumnMappings.Add("Occupancy", "Occupancy");
            sqlBulkCopy.ColumnMappings.Add("Percentage", "Percentage");

            con.Open();
            sqlBulkCopy.WriteToServer(dt);
            con.Close();

            lblMsg.Text = "Upload Successfully.";
        }

        catch (Exception ex) 
        {
            lblMsg.Text= ex.Message.ToString();
        }

       
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Business.aspx");
    }
}