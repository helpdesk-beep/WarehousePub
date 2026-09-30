using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;

public partial class JointVentureScheme_PaymentTransaction_FileUpload : System.Web.UI.Page
{
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string filename = Path.GetFileNameWithoutExtension(upFile.PostedFile.FileName);
    //        filename = filename + ".csv";
    //        string csvlPath = Server.MapPath("../Audit//") + filename;
    //        upFile.SaveAs(csvlPath);

    //        DataTable dtPayment = GetDataTableFromCsv(csvlPath);
    //        int totalRows = dtPayment.Rows.Count;
    //        int skipCount = 0;
    //        int insertCount = 0;

    //        string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

    //        // Create a new DataTable to hold only unique records
    //        DataTable dtUniqueRecords = dtPayment.Clone();

    //        using (SqlConnection con = new SqlConnection(consString))
    //        {
    //            con.Open();

    //            foreach (DataRow row in dtPayment.Rows)
    //            {
    //                // Check if the Bank Reference No already exists (assuming this is your unique key)
    //                string bankRef = row["Bank Reference No"].ToString();
    //                string query = "SELECT COUNT(*) FROM dbo.tbl_Payment_Status WHERE BankReferenceNo = @BankRef";

    //                using (SqlCommand cmd = new SqlCommand(query, con))
    //                {
    //                    cmd.Parameters.AddWithValue("@BankRef", bankRef);
    //                    int count = (int)cmd.ExecuteScalar();

    //                    if (count == 0)
    //                    {
    //                        dtUniqueRecords.ImportRow(row);
    //                        insertCount++;
    //                    }
    //                    else
    //                    {
    //                        skipCount++;
    //                    }
    //                }
    //            }

    //            // Perform Bulk Copy only with the unique records
    //            if (dtUniqueRecords.Rows.Count > 0)
    //            {
    //                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //                {
    //                    sqlBulkCopy.DestinationTableName = "dbo.tbl_Payment_Status";
    //                    sqlBulkCopy.ColumnMappings.Add("Category Name", "CategoryName");
    //                    sqlBulkCopy.ColumnMappings.Add("Payment Mode", "PaymentMode");
    //                    sqlBulkCopy.ColumnMappings.Add("Bank Reference No", "BankReferenceNo");
    //                    sqlBulkCopy.ColumnMappings.Add("Transaction Date", "TransactionDate");
    //                    sqlBulkCopy.ColumnMappings.Add("Amount", "Amount");
    //                    sqlBulkCopy.ColumnMappings.Add("Status", "Status");
    //                    sqlBulkCopy.ColumnMappings.Add("REGISTRATION ID", "REGISTRATIONID");
    //                    sqlBulkCopy.ColumnMappings.Add("NAME OF DEPOSITOR", "NAMEOFDEPOSITOR");
    //                    sqlBulkCopy.ColumnMappings.Add("CONTACT NO.", "CONTACTNO");
    //                    sqlBulkCopy.ColumnMappings.Add("EMAIL ID", "EMAILID");
    //                    sqlBulkCopy.ColumnMappings.Add("FEE", "FEE");
    //                    sqlBulkCopy.ColumnMappings.Add("Remarks", "Remarks");

    //                    sqlBulkCopy.WriteToServer(dtUniqueRecords);
    //                }
    //            }
    //            con.Close();
    //        }

    //        if (File.Exists(csvlPath)) { File.Delete(csvlPath); }

    //        lblmsg.Text = "Upload Complete. New: {insertCount} | Skipped: {skipCount}";
    //        lblmsg.ForeColor = System.Drawing.Color.Green;
    //    }
    //    catch (Exception ex)
    //    {
    //        lblmsg.Text = ex.Message;
    //        lblmsg.ForeColor = System.Drawing.Color.Red;
    //    }
    //}


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = Path.GetFileNameWithoutExtension(upFile.PostedFile.FileName);
            filename = filename + ".csv";
            string csvlPath = Server.MapPath("../Audit//") + filename;
            upFile.SaveAs(csvlPath);

            DataTable dtPayment = GetDataTableFromCsv(csvlPath);

            int totalRows = dtPayment.Rows.Count;
            int skipCount = 0;
            int insertCount = 0;

            string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

            DataTable dtUniqueRecords = dtPayment.Clone();

            using (SqlConnection con = new SqlConnection(consString))
            {
                con.Open();

                foreach (DataRow row in dtPayment.Rows)
                {
                    if (!dtPayment.Columns.Contains("Bank Reference No")) continue;

                    string bankRef = row["Bank Reference No"].ToString();

                    string query = "SELECT COUNT(*) FROM dbo.tbl_Payment_Status WHERE BankReferenceNo = @BankRef";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BankRef", bankRef);
                        int count = (int)cmd.ExecuteScalar();

                        if (count == 0)
                        {
                            dtUniqueRecords.ImportRow(row);
                            insertCount++;
                        }
                        else
                        {
                            skipCount++;
                        }
                    }
                }

                if (dtUniqueRecords.Rows.Count > 0)
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.tbl_Payment_Status";

                        MapColumn(sqlBulkCopy, dtPayment, "Category Name", "CategoryName");
                        MapColumn(sqlBulkCopy, dtPayment, "Payment Mode", "PaymentMode");
                        MapColumn(sqlBulkCopy, dtPayment, "Bank Reference No", "BankReferenceNo");
                        MapColumn(sqlBulkCopy, dtPayment, "Transaction Date", "TransactionDate");
                        MapColumn(sqlBulkCopy, dtPayment, "Amount", "Amount");
                        MapColumn(sqlBulkCopy, dtPayment, "Status", "Status");
                        MapColumn(sqlBulkCopy, dtPayment, "REGISTRATION ID", "REGISTRATIONID");
                        MapColumn(sqlBulkCopy, dtPayment, "NAME OF DEPOSITOR", "NAMEOFDEPOSITOR");
                        MapColumn(sqlBulkCopy, dtPayment, "CONTACT NO.", "CONTACTNO");
                        MapColumn(sqlBulkCopy, dtPayment, "EMAIL ID", "EMAILID");
                        MapColumn(sqlBulkCopy, dtPayment, "FEE", "FEE");
                        MapColumn(sqlBulkCopy, dtPayment, "Remarks", "Remarks");

                        sqlBulkCopy.WriteToServer(dtUniqueRecords);
                    }
                }

                con.Close();
            }

            if (File.Exists(csvlPath)) File.Delete(csvlPath);

            lblmsg.Text = "Upload Complete. New: "+insertCount+" | Skipped: "+skipCount+"";
            lblmsg.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    private void MapColumn(SqlBulkCopy bulkCopy, DataTable dt, string sourceCol, string destCol)
    {
        if (dt.Columns.Contains(sourceCol))
        {
            bulkCopy.ColumnMappings.Add(sourceCol, destCol);
        }
    }


    //public DataTable GetDataTableFromCsv(string csvlfilePath)
    // {
    //     DataTable csvdt = new DataTable();
    //     string csvData = File.ReadAllText(csvlfilePath);
    //     bool isfirstrow = true;


    //     foreach (string row in csvData.Split('\n'))
    //     {

    //         string rowvalue = row;
    //         rowvalue = rowvalue.Trim();

    //         rowvalue = rowvalue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

    //         if (!string.IsNullOrEmpty(rowvalue) && !(rowvalue.StartsWith("Start")) && !(rowvalue.StartsWith("End")))
    //         {
    //             if (isfirstrow)
    //             {
    //                 string newrow = rowvalue;

    //                 newrow = newrow.Replace("'", "");

    //                 newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");


    //                 string[] split = newrow.Split(',');

    //                 foreach (string s in split)
    //                 {
    //                     csvdt.Columns.Add(s, typeof(System.String));
    //                 }
    //                 isfirstrow = false;
    //             }
    //             else
    //             {
    //                 string newrow = rowvalue;

    //                 newrow = newrow.Replace("'", "");
    //                 newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");


    //                 string[] split = newrow.Split(',');

    //                 DataRow dr = csvdt.NewRow();
    //                 int i = 0;
    //                 foreach (string s in split)
    //                 {
    //                     dr[i] = s;
    //                     i++;
    //                 }

    //                 //Transaction Date
    //                 string trandtformat = dr["Transaction Date"].ToString();
    //                 if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
    //                     trandtformat = "01/01/1900";

    //                 trandtformat = DateTime.Parse(trandtformat.Trim()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //                 // trandtformat = trandtformat.ToString("yyyy-MM-dd");
    //                 dr["Transaction Date"] = trandtformat;


    //                 // Amount
    //                 string amt = dr["Amount"].ToString();

    //                 if (amt == "null" || amt == "" || amt == "-")
    //                 { amt = "0"; }
    //                 dr["Amount"] = amt;

    //                 //Fee
    //                 string Fee = dr["Fee"].ToString();

    //                 if (Fee == "null" || Fee == "" || Fee == "-")
    //                 { Fee = "0"; }
    //                 dr["Fee"] = Fee;


    //                 csvdt.Rows.Add(dr);

    //             }
    //         }
    //     }
    //     return csvdt;
    // }

    //public DataTable GetDataTableFromCsv(string csvlfilePath)
    //{
    //    DataTable csvdt = new DataTable();
    //    string csvData = File.ReadAllText(csvlfilePath);
    //    bool isfirstrow = true;


    //    foreach (string row in csvData.Split('\n'))
    //    {

    //        string rowvalue = row;
    //        rowvalue = rowvalue.Trim();

    //        rowvalue = rowvalue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

    //        if (!string.IsNullOrEmpty(rowvalue) && !(rowvalue.StartsWith("Start")) && !(rowvalue.StartsWith("End")))
    //        {
    //            if (isfirstrow)
    //            {
    //                string newrow = rowvalue;

    //                newrow = newrow.Replace("'", "");

    //                newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");


    //                string[] split = newrow.Split(',');

    //                foreach (string s in split)
    //                {
    //                    csvdt.Columns.Add(s, typeof(System.String));
    //                }
    //                isfirstrow = false;
    //            }
    //            else
    //            {
    //                string newrow = rowvalue;

    //                newrow = newrow.Replace("'", "");
    //                newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");


    //                string[] split = newrow.Split(',');

    //                DataRow dr = csvdt.NewRow();
    //                int i = 0;
    //                foreach (string s in split)
    //                {
    //                    dr[i] = s;
    //                    i++;
    //                }

    //                //Transaction Date
    //                string trandtformat = dr["Transaction Date"].ToString();
    //                if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
    //                    trandtformat = "01/01/1900";

    //                trandtformat = DateTime.Parse(trandtformat.Trim()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //                // trandtformat = trandtformat.ToString("yyyy-MM-dd");
    //                dr["Transaction Date"] = trandtformat;


    //                // Amount
    //                string amt = dr["Amount"].ToString();

    //                if (amt == "null" || amt == "" || amt == "-")
    //                { amt = "0"; }
    //                dr["Amount"] = amt;

    //                //Fee
    //                string Fee = dr["Fee"].ToString();

    //                if (Fee == "null" || Fee == "" || Fee == "-")
    //                { Fee = "0"; }
    //                dr["Fee"] = Fee;


    //                csvdt.Rows.Add(dr);

    //            }
    //        }
    //    }
    //    return csvdt;
    //}


    public DataTable GetDataTableFromCsv(string csvlfilePath)
    {
        DataTable csvdt = new DataTable();
        var lines = File.ReadAllLines(csvlfilePath);

        bool headerFound = false;

        foreach (string rawRow in lines)
        {
            string row = rawRow.Trim();

            if (string.IsNullOrWhiteSpace(row)) continue;

            // ❌ Skip unwanted rows
            if (row.StartsWith("Start") || row.StartsWith("End")) continue;

            // Replace tab with comma
            row = row.Replace("\t", ",");

            string[] split = row.Split(',');

            // ✅ Detect correct header row
            if (!headerFound)
            {
                if (row.Contains("Category Name") && row.Contains("Bank Reference No"))
                {
                    foreach (string col in split)
                    {
                        csvdt.Columns.Add(col.Trim(), typeof(string));
                    }
                    headerFound = true;
                }
                continue;
            }

            // ✅ Data rows
            DataRow dr = csvdt.NewRow();

            for (int i = 0; i < csvdt.Columns.Count; i++)
            {
                dr[i] = i < split.Length ? split[i].Trim() : "";
            }

            // ✅ Safe Transaction Date
            if (csvdt.Columns.Contains("Transaction Date"))
            {
                string trandate = dr["Transaction Date"].ToString();

                if (string.IsNullOrWhiteSpace(trandate) || trandate == "-" || trandate == "null")
                    trandate = "01/01/1900";

                DateTime dt;
                if (DateTime.TryParse(trandate, out dt))
                {
                    dr["Transaction Date"] = dt.ToString("yyyy-MM-dd");
                }
            }

            // ✅ Amount
            if (csvdt.Columns.Contains("Amount"))
            {
                string amt = dr["Amount"].ToString();
                if (string.IsNullOrWhiteSpace(amt) || amt == "-" || amt == "null")
                    dr["Amount"] = "0";
            }

            // ✅ FEE (IMPORTANT FIX)
            if (csvdt.Columns.Contains("FEE"))
            {
                string fee = dr["FEE"].ToString();
                if (string.IsNullOrWhiteSpace(fee) || fee == "-" || fee == "null")
                    dr["FEE"] = "0";
            }

            csvdt.Rows.Add(dr);
        }

        return csvdt;
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
     {
         Session.Abandon();
         Response.Redirect("Logins.aspx");
     }

}