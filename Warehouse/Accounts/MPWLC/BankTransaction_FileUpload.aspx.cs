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

public partial class Accounting_BankTransaction_FileUpload : System.Web.UI.Page
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
               // DataTable dtDebit = new DataTable();
                DataTable dtCredit = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                //dtDebit.Columns.AddRange(new DataColumn[22]
                //{
                //    new DataColumn("ePayOrderNo", typeof(string)),
                //    new DataColumn("DebitAccountNumber", typeof(string)),
                //    new DataColumn("DebitBranchCode", typeof(string)),
                //    new DataColumn("CreditReferenceNo", typeof(string)),
                //    new DataColumn("CreditAccountNumber", typeof(string)),
                //    new DataColumn("CreditBranchCode", typeof(string)),
                //    new DataColumn("CreditMMID", typeof(string)),
                //    new DataColumn("CreditMobileNumber", typeof(string)),
                //    new DataColumn("Commission", typeof(string)),
                //    new DataColumn("CreditStatus", typeof(string)),
                //    new DataColumn("CorporateReferenceNumber", typeof(string)),
                //    new DataColumn("UTRNumber", typeof(string)),
                //    new DataColumn("UTRPostedDate", typeof(string)),
                //    new DataColumn("UTRStatus", typeof(string)),
                //    new DataColumn("Maker", typeof(string)),
                //    new DataColumn("Status Description", typeof(string)),
                //    new DataColumn("Authorizer1", typeof(string)),
                //    new DataColumn("Authorizer2", typeof(string)),
                //    new DataColumn("Amount", typeof(string)),
                //    new DataColumn("Authorizer1Date", typeof(string)),
                //    new DataColumn("Authorizer2Date", typeof(string)),
                //    new DataColumn("ScheduledDate", typeof(string)),
                   

                //});

                dtCredit.Columns.AddRange(new DataColumn[22]
                {
                   new DataColumn("ePayOrderNo", typeof(string)),
                    new DataColumn("DebitAccountNumber", typeof(string)),
                    new DataColumn("DebitBranchCode", typeof(string)),
                    new DataColumn("CreditReferenceNo", typeof(string)),
                    new DataColumn("CreditAccountNumber", typeof(string)),
                    new DataColumn("CreditBranchCode", typeof(string)),
                    new DataColumn("CreditMMID", typeof(string)),
                    new DataColumn("CreditMobileNumber", typeof(string)),
                    new DataColumn("Commission", typeof(string)),
                    new DataColumn("CreditStatus", typeof(string)),
                    new DataColumn("CorporateReferenceNumber", typeof(string)),
                    new DataColumn("UTRNumber", typeof(string)),
                    new DataColumn("UTRPostedDate", typeof(string)),
                    new DataColumn("UTRStatus", typeof(string)),
                    new DataColumn("Maker", typeof(string)),
                    new DataColumn("Status Description", typeof(string)),
                    new DataColumn("Authorizer1", typeof(string)),
                    new DataColumn("Authorizer2", typeof(string)),
                    new DataColumn("Amount", typeof(string)),
                    new DataColumn("Authorizer1Date", typeof(string)),
                    new DataColumn("Authorizer2Date", typeof(string)),
                    new DataColumn("ScheduledDate", typeof(string)),

                });

                string qr = string.Empty;
                //qr = "SELECT * FROM [" + sheet1 + "] where [Record Type]='Debit leg'";
                //using (OleDbDataAdapter oda = new OleDbDataAdapter(qr, excel_con))
                //{
                //    oda.Fill(dtDebit);
                //}

                qr = "SELECT * FROM [" + sheet1 + "]";
                using (OleDbDataAdapter oda1 = new OleDbDataAdapter(qr, excel_con))
                {
                    oda1.Fill(dtCredit);
                }

                excel_con.Close();

                for (int i = 0; i < dtCredit.Rows.Count; i++)
                {
                    string val = string.Empty;
                    dtCredit.Rows[i]["ePayOrderNo"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["ePayOrderNo"].ToString());
                    dtCredit.Rows[i]["DebitAccountNumber"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["DebitAccountNumber"].ToString());
                    dtCredit.Rows[i]["DebitBranchCode"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["DebitBranchCode"].ToString());
                    dtCredit.Rows[i]["CreditReferenceNo"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditReferenceNo"].ToString());
                    dtCredit.Rows[i]["CreditAccountNumber"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditAccountNumber"].ToString());
                    dtCredit.Rows[i]["CreditBranchCode"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditBranchCode"].ToString());
                    dtCredit.Rows[i]["CreditMMID"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditMMID"].ToString());
                    dtCredit.Rows[i]["CreditMobileNumber"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditMobileNumber"].ToString());
                    dtCredit.Rows[i]["Commission"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Commission"].ToString());
                    dtCredit.Rows[i]["CreditStatus"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CreditStatus"].ToString());
                    dtCredit.Rows[i]["CorporateReferenceNumber"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["CorporateReferenceNumber"].ToString());
                    dtCredit.Rows[i]["UTRNumber"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTRNumber"].ToString());
                    dtCredit.Rows[i]["UTRPostedDate"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTRPostedDate"].ToString());
                    dtCredit.Rows[i]["UTRStatus"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTRStatus"].ToString());
                    dtCredit.Rows[i]["Maker"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Maker"].ToString());
                    dtCredit.Rows[i]["Authorizer1"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer1"].ToString());
                    dtCredit.Rows[i]["Authorizer2"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer2"].ToString());
                    dtCredit.Rows[i]["Amount"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Amount"].ToString());
                    dtCredit.Rows[i]["Authorizer1Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer1Date"].ToString());
                    dtCredit.Rows[i]["Maker"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Maker"].ToString());
                    dtCredit.Rows[i]["Authorizer2Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer2Date"].ToString());
                    dtCredit.Rows[i]["ScheduledDate"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["ScheduledDate"].ToString());


                }

                string consString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.tbl_NEFT_Payment_ACK";

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
                        sqlBulkCopy.WriteToServer(dtCredit);
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
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    public string RemoveSingleQuoteStr(string columnvalue)
    {
        string rowvalue = "";
        string val = columnvalue;
        rowvalue = val.Replace("'", "");
        return rowvalue;
    }

    public static string PadDate(string strDateTime)
    {

        string strNewDate = "";

        int intdd = strDateTime.IndexOf(@"/");
        string strdd = "";
        if (intdd > 0)
        {
            strdd = strDateTime.Substring(0, intdd);
        }
        else
        {
            strdd = strDateTime;
        }

        int intlegdd = strdd.Length;

        if (intlegdd < 2)
        {

            strdd = "0" + strdd;

        }

        int intMM = strDateTime.IndexOf(@"/", intdd + 1);

        string strMM = "";

        strMM = strDateTime.Substring(intdd + 1, 2);

        int intMM1 = strMM.IndexOf(@"/");

        if (intMM1 > -1)
        {

            strMM = strMM.Replace(@"/", "");

            strMM = "0" + strMM;

        }

        int intyy = strDateTime.LastIndexOf(@"/");

        string stryy = strDateTime.Substring(intyy + 1, 4);

        strNewDate = strdd + @"/" + strMM + @"/" + stryy;

        return strNewDate;

    }
}