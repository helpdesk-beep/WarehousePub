using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;

using System.IO;
using System.Data.OleDb;

public partial class Inspection_State_UploadPaymentFile : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    SqlConnection con;
    SqlDataAdapter da = new SqlDataAdapter();

    String str = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString();

    OleDbConnection Econ;


    string constr, Query, sqlconn;
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    //private void ExcelConn(string FilePath)
    //{

    //    constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
    //    Econ = new OleDbConnection(constr);
     
    //}
    //private void connection()
    //{
    //    sqlconn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    con = new SqlConnection(sqlconn);
    
    //}
    protected void btnSaveRecord_Click(object sender, EventArgs e)
    {



        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        SqlConnection con1 = new SqlConnection();
        SqlCommand cmd1 = new SqlCommand();
        DataSet ds = new DataSet();
        con1.ConnectionString = strConnString;
        con1.Open();       
        try
        {

            string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                             //   conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;


                    conString = "Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0";

                    break;
                case ".xlsx": //Excel 07 or higher
                              // conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;

                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0";
                    break;

            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                //dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                //new DataColumn("Name", typeof(string)),
                //new DataColumn("Salary", typeof(decimal)) });

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();

                string consString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "[dbo].[tbl_NEFT_Payment_ACK]";
                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("ePayOrderNo", "ePayOrderNo");
                        sqlBulkCopy.ColumnMappings.Add("DebitAccountNumber", "DebitAccountNumber");
                        sqlBulkCopy.ColumnMappings.Add("DebitBranchCode", "DebitBranchCode");
                        sqlBulkCopy.ColumnMappings.Add("CreditReferenceNo", "CreditReferenceNo");
                        sqlBulkCopy.ColumnMappings.Add("CreditAccountNumber", "CreditAccountNumber");
                        sqlBulkCopy.ColumnMappings.Add("CreditBranchCode", "CreditBranchCode");
                        sqlBulkCopy.ColumnMappings.Add("CreditMMID", "CreditMMID");
                        sqlBulkCopy.ColumnMappings.Add("CreditMobileNumber", "CreditMobileNumber");
                        sqlBulkCopy.ColumnMappings.Add("Commission", "Commission");
                        sqlBulkCopy.ColumnMappings.Add("CreditStatus", "CreditStatus");
                        sqlBulkCopy.ColumnMappings.Add("CorporateReferenceNumber", "CorporateReferenceNumber");
                        sqlBulkCopy.ColumnMappings.Add("UTRNumber", "UTRNumber");
                        sqlBulkCopy.ColumnMappings.Add("UTRPostedDate", "UTRPostedDate");
                        sqlBulkCopy.ColumnMappings.Add("UTRStatus", "UTRStatus");
                        sqlBulkCopy.ColumnMappings.Add("Maker", "Maker");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1", "Authorizer1");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2", "Authorizer2");
                        sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1Date", "Authorizer1Date");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2Date", "Authorizer2Date");
                        sqlBulkCopy.ColumnMappings.Add("ScheduledDate", "ScheduledDate");
                      
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();

                        string message = "alert('Data Sucessfully Saved .')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    }
                }
            }

        }
        catch (Exception ex)
        {

            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);

        }








    }

  
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Truncate_Table_NEFT_Payment_ACK", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();               
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Payment Table Truncate Successfully submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Payment Table Truncate Successfully submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/Default.aspx");
    }
}