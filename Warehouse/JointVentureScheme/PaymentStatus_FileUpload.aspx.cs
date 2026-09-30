using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Globalization;

public partial class JointVentureScheme_PaymentStatus_FileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            //Upload and save the file
            string excelPath = Server.MapPath("../Files//") + Path.GetFileName(upFile.PostedFile.FileName);
            upFile.SaveAs(excelPath);

            
            string conString = string.Empty;
            string extension = Path.GetExtension(upFile.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                    break;

            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                sheet1 = sheet1.Replace("'", "");
                DataTable dtPayment = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtPayment.Columns.AddRange(new DataColumn[12] 
                {
                    new DataColumn("Category Name", typeof(string)),
                    new DataColumn("Payment Mode", typeof(string)),
                    new DataColumn("Bank Reference No", typeof(string)),
                    new DataColumn("Transaction Date", typeof(string)),
                    new DataColumn("Amount", typeof(string)),
                    new DataColumn("Status", typeof(string)),
                    new DataColumn("REGISTRATION ID", typeof(string)),
                    new DataColumn("NAME OF DEPOSITOR", typeof(string)),
                    new DataColumn("CONTACT NO", typeof(string)),
                    new DataColumn("EMAIL ID", typeof(string)),
                    new DataColumn("FEE", typeof(string)),
                    new DataColumn("Remarks", typeof(string))
                   
                });

               

                string qr = string.Empty;
                qr = "SELECT * FROM [" + sheet1 + "] ";
                using (OleDbDataAdapter oda = new OleDbDataAdapter(qr, excel_con))
                {
                    oda.Fill(dtPayment);
                }

               
                excel_con.Close();

                for (int i = 0; i < dtPayment.Rows.Count; i++)
                {
                    string val = string.Empty;
                 
                    //Transaction Date
                    string trandtformat = dtPayment.Rows[i]["Transaction Date"].ToString();
                    if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
                        trandtformat = "01/01/1900";

                    trandtformat = DateTime.Parse(trandtformat.Trim()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                   // trandtformat = trandtformat.ToString("yyyy-MM-dd");
                    dtPayment.Rows[i]["Transaction Date"] = trandtformat;


                    // Amount
                    string amt = dtPayment.Rows[i]["Amount"].ToString();

                    if (amt == "null" || amt == "" || amt == "-")
                    { amt = "0"; }
                    dtPayment.Rows[i]["Amount"] = amt;

                    //Fee
                    string Fee = dtPayment.Rows[i]["Fee"].ToString();

                    if (Fee == "null" || Fee == "" || Fee == "-")
                    { Fee = "0"; }
                    dtPayment.Rows[i]["Fee"] = Fee;

                }




                string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.tbl_Payment_Status";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Category Name", "CategoryName");
                        sqlBulkCopy.ColumnMappings.Add("Payment Mode", "PaymentMode");
                        sqlBulkCopy.ColumnMappings.Add("Bank Reference No", "BankReferenceNo");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Date", "TransactionDate");
                        sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                        sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                        sqlBulkCopy.ColumnMappings.Add("REGISTRATION ID", "REGISTRATIONID");
                        sqlBulkCopy.ColumnMappings.Add("NAME OF DEPOSITOR", "NAMEOFDEPOSITOR");
                        sqlBulkCopy.ColumnMappings.Add("CONTACT NO", "CONTACTNO");
                        sqlBulkCopy.ColumnMappings.Add("EMAIL ID", "EMAILID");
                        sqlBulkCopy.ColumnMappings.Add("FEE", "FEE");
                        sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtPayment);
                        con.Close();
                    }
                }

            }

            lblmsg.Text = "Successfully Upload.";
            lblmsg.ForeColor = System.Drawing.Color.Green;

            if ((System.IO.File.Exists(excelPath)))
            {
                System.IO.File.Delete(excelPath);
            }

        }

        catch (Exception ex)
        {
            lblmsg.Text = "Not Upload File is not currect format";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }

}