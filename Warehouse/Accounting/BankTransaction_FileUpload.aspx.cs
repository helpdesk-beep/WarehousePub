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
                DataTable dtDebit = new DataTable();
                DataTable dtCredit = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
                dtDebit.Columns.AddRange(new DataColumn[24]
                {
                    new DataColumn("Record Type", typeof(string)),
                    new DataColumn("Corporte Id", typeof(string)),
                    new DataColumn("e-PayOrder No", typeof(string)),
                    new DataColumn("Account Number", typeof(string)),
                    new DataColumn("Branch Code", typeof(string)),
                    new DataColumn("MMID", typeof(string)),
                    new DataColumn("Mobile Number", typeof(string)),
                    new DataColumn("Debit Amount", typeof(string)),
                    new DataColumn("Credit Amount", typeof(string)),
                    new DataColumn("Corporate Reference Number", typeof(string)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Payment Type", typeof(string)),
                    new DataColumn("Transaction Status", typeof(string)),
                    new DataColumn("Transaction Date", typeof(string)),
                    new DataColumn("Transaction Time", typeof(string)),
                    new DataColumn("Status Description", typeof(string)),
                    new DataColumn("UTR Number", typeof(string)),
                    new DataColumn("UTR Posted Date", typeof(string)),
                    new DataColumn("UTR Status", typeof(string)),
                    new DataColumn("Maker", typeof(string)),
                    new DataColumn("Authorizer1", typeof(string)),
                    new DataColumn("Authorizer1 Date", typeof(string)),
                    new DataColumn("Authorizer2", typeof(string)),
                    new DataColumn("Authorizer2 Date", typeof(string))

                });

                dtCredit.Columns.AddRange(new DataColumn[24]
                {
                    new DataColumn("Record Type", typeof(string)),
                    new DataColumn("Corporte Id", typeof(string)),
                    new DataColumn("e-PayOrder No", typeof(string)),
                    new DataColumn("Account Number", typeof(string)),
                    new DataColumn("Branch Code", typeof(string)),
                    new DataColumn("MMID", typeof(string)),
                    new DataColumn("Mobile Number", typeof(string)),
                    new DataColumn("Debit Amount", typeof(string)),
                    new DataColumn("Credit Amount", typeof(string)),
                    new DataColumn("Corporate Reference Number", typeof(string)),
                    new DataColumn("Description", typeof(string)),
                    new DataColumn("Payment Type", typeof(string)),
                    new DataColumn("Transaction Status", typeof(string)),
                    new DataColumn("Transaction Date", typeof(string)),
                    new DataColumn("Transaction Time", typeof(string)),
                    new DataColumn("Status Description", typeof(string)),
                    new DataColumn("UTR Number", typeof(string)),
                    new DataColumn("UTR Posted Date", typeof(string)),
                    new DataColumn("UTR Status", typeof(string)),
                    new DataColumn("Maker", typeof(string)),
                    new DataColumn("Authorizer1", typeof(string)),
                    new DataColumn("Authorizer1 Date", typeof(string)),
                    new DataColumn("Authorizer2", typeof(string)),
                    new DataColumn("Authorizer2 Date", typeof(string))

                });

                string qr = string.Empty;
                qr = "SELECT * FROM [" + sheet1 + "] where [Record Type]='Debit leg'";
                using (OleDbDataAdapter oda = new OleDbDataAdapter(qr, excel_con))
                {
                    oda.Fill(dtDebit);
                }

                qr = "SELECT * FROM [" + sheet1 + "] where [Record Type]='Credit leg'";
                using (OleDbDataAdapter oda1 = new OleDbDataAdapter(qr, excel_con))
                {
                    oda1.Fill(dtCredit);
                }

                excel_con.Close();

                for (int i = 0; i < dtDebit.Rows.Count; i++)
                {
                    string val = string.Empty;
                    dtDebit.Rows[i]["Debit Amount"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Debit Amount"].ToString());
                    dtDebit.Rows[i]["Corporte Id"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Corporte Id"].ToString());
                    dtDebit.Rows[i]["e-PayOrder No"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["e-PayOrder No"].ToString());
                    dtDebit.Rows[i]["Account Number"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Account Number"].ToString());
                    dtDebit.Rows[i]["Branch Code"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Branch Code"].ToString());
                    dtDebit.Rows[i]["MMID"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["MMID"].ToString());
                    dtDebit.Rows[i]["Mobile Number"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Mobile Number"].ToString());
                    dtDebit.Rows[i]["Debit Amount"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Debit Amount"].ToString());
                    dtDebit.Rows[i]["Credit Amount"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Credit Amount"].ToString());
                    dtDebit.Rows[i]["Corporate Reference Number"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Corporate Reference Number"].ToString());
                    dtDebit.Rows[i]["Description"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Description"].ToString());
                    dtDebit.Rows[i]["Payment Type"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Payment Type"].ToString());
                    dtDebit.Rows[i]["Transaction Status"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Transaction Status"].ToString());
                    dtDebit.Rows[i]["Transaction Date"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Transaction Date"].ToString());
                    dtDebit.Rows[i]["Transaction Time"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Transaction Time"].ToString());
                    dtDebit.Rows[i]["Status Description"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Status Description"].ToString());
                    dtDebit.Rows[i]["UTR Number"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["UTR Number"].ToString());
                    dtDebit.Rows[i]["UTR Posted Date"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["UTR Posted Date"].ToString());
                    dtDebit.Rows[i]["UTR Status"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["UTR Status"].ToString());
                    dtDebit.Rows[i]["Maker"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Maker"].ToString());
                    dtDebit.Rows[i]["Authorizer1"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Authorizer1"].ToString());
                    dtDebit.Rows[i]["Authorizer1 Date"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Authorizer1 Date"].ToString());
                    dtDebit.Rows[i]["Authorizer2"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Authorizer2"].ToString());
                    dtDebit.Rows[i]["Authorizer2 Date"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Authorizer2 Date"].ToString());


                    //Debit Amount
                    string debitAmt = dtDebit.Rows[i]["Debit Amount"].ToString();

                    if (debitAmt == "null" || debitAmt == "" || debitAmt == "-")
                    { debitAmt = "0"; }
                    dtDebit.Rows[i]["Debit Amount"] = debitAmt;

                    //Credit Amount
                    string creditAmt = dtDebit.Rows[i]["Credit Amount"].ToString();

                    if (creditAmt == "null" || creditAmt == "" || creditAmt == "-")
                    { creditAmt = "0"; }
                    dtDebit.Rows[i]["Credit Amount"] = creditAmt;

                    //Transaction Date
                    string trandtformat = dtDebit.Rows[i]["Transaction Date"].ToString();

                    if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
                        trandtformat = "01/01/1900";

                    trandtformat = PadDate(trandtformat);

                    DateTime dttrandtformat = DateTime.ParseExact(trandtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    trandtformat = dttrandtformat.ToString("yyyy-MM-dd");
                    dtDebit.Rows[i]["Transaction Date"] = trandtformat;

                    //utr posted date
                    string utrdtformat = dtDebit.Rows[i]["UTR Posted Date"].ToString();

                    if (utrdtformat == "null" || utrdtformat == "" || utrdtformat == "-")
                        utrdtformat = "01/01/1900";
                    else
                    {
                        DateTime date = Convert.ToDateTime(DateTime.Parse(dtCredit.Rows[i]["UTR Posted Date"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        utrdtformat = date.ToString().Replace("-", "/");
                    }


                    utrdtformat = PadDate(utrdtformat);

                    DateTime dtutrdtformat = DateTime.ParseExact(utrdtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    utrdtformat = dtutrdtformat.ToString("yyyy-MM-dd");
                    dtDebit.Rows[i]["UTR Posted Date"] = utrdtformat;

                }


                for (int i = 0; i < dtCredit.Rows.Count; i++)
                {

                    dtCredit.Rows[i]["Debit Amount"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Debit Amount"].ToString());
                    dtCredit.Rows[i]["Corporte Id"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Corporte Id"].ToString());
                    dtCredit.Rows[i]["e-PayOrder No"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["e-PayOrder No"].ToString());
                    dtCredit.Rows[i]["Account Number"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Account Number"].ToString());
                    dtCredit.Rows[i]["Branch Code"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Branch Code"].ToString());
                    dtCredit.Rows[i]["MMID"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["MMID"].ToString());
                    dtCredit.Rows[i]["Mobile Number"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Mobile Number"].ToString());
                    dtCredit.Rows[i]["Debit Amount"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Debit Amount"].ToString());
                    dtCredit.Rows[i]["Credit Amount"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Credit Amount"].ToString());
                    dtCredit.Rows[i]["Corporate Reference Number"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Corporate Reference Number"].ToString());
                    dtCredit.Rows[i]["Description"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Description"].ToString());
                    dtCredit.Rows[i]["Payment Type"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Payment Type"].ToString());
                    dtCredit.Rows[i]["Transaction Status"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Transaction Status"].ToString());
                    dtCredit.Rows[i]["Transaction Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Transaction Date"].ToString());
                    dtCredit.Rows[i]["Transaction Time"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Transaction Time"].ToString());
                    dtCredit.Rows[i]["Status Description"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Status Description"].ToString());
                    dtCredit.Rows[i]["UTR Number"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTR Number"].ToString());
                    dtCredit.Rows[i]["UTR Posted Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTR Posted Date"].ToString());
                    dtCredit.Rows[i]["UTR Status"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["UTR Status"].ToString());
                    dtCredit.Rows[i]["Maker"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Maker"].ToString());
                    dtCredit.Rows[i]["Authorizer1"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer1"].ToString());
                    dtCredit.Rows[i]["Authorizer1 Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer1 Date"].ToString());
                    dtCredit.Rows[i]["Authorizer2"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer2"].ToString());
                    dtCredit.Rows[i]["Authorizer2 Date"] = RemoveSingleQuoteStr(dtCredit.Rows[i]["Authorizer2 Date"].ToString());



                    string val = string.Empty;

                    //Debit Amount
                    string debitAmt = dtCredit.Rows[i]["Debit Amount"].ToString();

                    if (debitAmt == "null" || debitAmt == "" || debitAmt == "-")
                    { debitAmt = "0"; }
                    dtCredit.Rows[i]["Debit Amount"] = debitAmt;

                    //Credit Amount
                    string creditAmt = dtCredit.Rows[i]["Credit Amount"].ToString();

                    if (creditAmt == "null" || creditAmt == "" || creditAmt == "-")
                    { creditAmt = "0"; }
                    dtCredit.Rows[i]["Credit Amount"] = creditAmt;

                    //Transaction Date
                    string trandtformat = dtCredit.Rows[i]["Transaction Date"].ToString();

                    if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
                        trandtformat = "01/01/1900";
                    trandtformat = PadDate(trandtformat);

                    DateTime dttrandtformat = DateTime.ParseExact(trandtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    trandtformat = dttrandtformat.ToString("yyyy-MM-dd");
                    dtCredit.Rows[i]["Transaction Date"] = trandtformat;

                    //utr posted date
                    string utrdtformat = dtCredit.Rows[i]["UTR Posted Date"].ToString();
                    if (utrdtformat == "null" || utrdtformat == "" || utrdtformat == "-")
                        utrdtformat = "01/01/1900";
                    else
                    {
                        DateTime date = Convert.ToDateTime(DateTime.Parse(dtCredit.Rows[i]["UTR Posted Date"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        utrdtformat = date.ToString().Replace("-", "/");
                    }

                    utrdtformat = PadDate(utrdtformat);

                    DateTime dtutrdtformat = DateTime.ParseExact(utrdtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    utrdtformat = dtutrdtformat.ToString("yyyy-MM-dd");
                    dtCredit.Rows[i]["UTR Posted Date"] = utrdtformat;


                }


                string consString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.tblDebit_Transaction_Detail";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Record Type", "RECORD_TYPE");
                        sqlBulkCopy.ColumnMappings.Add("Corporte Id", "CORPORATE_ID");
                        sqlBulkCopy.ColumnMappings.Add("e-PayOrder No", "ePAYORDER_No");
                        sqlBulkCopy.ColumnMappings.Add("Account Number", "ACCOUNT_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Branch Code", "BRANCH_CODE");
                        sqlBulkCopy.ColumnMappings.Add("MMID", "MMID");
                        sqlBulkCopy.ColumnMappings.Add("Mobile Number", "MOBILE_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Debit Amount", "DEBIT_AMOUNT");
                        sqlBulkCopy.ColumnMappings.Add("Credit Amount", "CREDIT_AMOUNT");
                        sqlBulkCopy.ColumnMappings.Add("Corporate Reference Number", "CORPORATE_REFERENCE_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Description", "DESCRIPTION");
                        sqlBulkCopy.ColumnMappings.Add("Payment Type", "PAYMENT_TYPE");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Status", "TRANSACTION_STATUS");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Date", "TRANSACTION_DATE");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Time", "TRANSACTION_TIME");
                        sqlBulkCopy.ColumnMappings.Add("Status Description", "STATUS_DESCRIPTION");
                        sqlBulkCopy.ColumnMappings.Add("UTR Number", "UTR_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("UTR Posted Date", "UTR_POSTED_DATE");
                        sqlBulkCopy.ColumnMappings.Add("UTR Status", "UTR_STATUS");
                        sqlBulkCopy.ColumnMappings.Add("Maker", "MAKER");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1", "AUTHORIZER1");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1 Date", "AUTHORIZER1_DATE");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2", "AUTHORIZER2");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2 Date", "AUTHORIZER2_DATE");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dtDebit);
                        con.Close();
                    }
                }


                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.tblCredit_Transaction_Detail";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Record Type", "RECORD_TYPE");
                        sqlBulkCopy.ColumnMappings.Add("Corporte Id", "CORPORATE_ID");
                        sqlBulkCopy.ColumnMappings.Add("e-PayOrder No", "ePAYORDER_No");
                        sqlBulkCopy.ColumnMappings.Add("Account Number", "ACCOUNT_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Branch Code", "BRANCH_CODE");
                        sqlBulkCopy.ColumnMappings.Add("MMID", "MMID");
                        sqlBulkCopy.ColumnMappings.Add("Mobile Number", "MOBILE_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Debit Amount", "DEBIT_AMOUNT");
                        sqlBulkCopy.ColumnMappings.Add("Credit Amount", "CREDIT_AMOUNT");
                        sqlBulkCopy.ColumnMappings.Add("Corporate Reference Number", "CORPORATE_REFERENCE_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("Description", "DESCRIPTION");
                        sqlBulkCopy.ColumnMappings.Add("Payment Type", "PAYMENT_TYPE");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Status", "TRANSACTION_STATUS");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Date", "TRANSACTION_DATE");
                        sqlBulkCopy.ColumnMappings.Add("Transaction Time", "TRANSACTION_TIME");
                        sqlBulkCopy.ColumnMappings.Add("Status Description", "STATUS_DESCRIPTION");
                        sqlBulkCopy.ColumnMappings.Add("UTR Number", "UTR_NUMBER");
                        sqlBulkCopy.ColumnMappings.Add("UTR Posted Date", "UTR_POSTED_DATE");
                        sqlBulkCopy.ColumnMappings.Add("UTR Status", "UTR_STATUS");
                        sqlBulkCopy.ColumnMappings.Add("Maker", "MAKER");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1", "AUTHORIZER1");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer1 Date", "AUTHORIZER1_DATE");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2", "AUTHORIZER2");
                        sqlBulkCopy.ColumnMappings.Add("Authorizer2 Date", "AUTHORIZER2_DATE");

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