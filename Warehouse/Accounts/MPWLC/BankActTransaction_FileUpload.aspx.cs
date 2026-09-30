using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;

public partial class Accounting_BankActTransaction_FileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = Path.GetFileNameWithoutExtension(upFile.PostedFile.FileName);
            filename = filename + ".csv";
            string csvlPath = Server.MapPath("../Audit//") + filename;
            upFile.SaveAs(csvlPath);

            DataTable dt = GetDataTableFromCsv(csvlPath);
            DataTable DtDebit = new DataTable();
            DataTable DtCredit = new DataTable();


            Response.Write(getData(dt));


            try
            {
                string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                using (SqlConnection constr = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Insert_Transaction_Detail", constr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    constr.Open();
                    cmd.Parameters.AddWithValue("@strXML", getData(dt));
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Dead Stock Details Successfully Submitted|||";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        lblmsg.Text = "Successfully Upload.";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        lblmsg.Text = "You Have Allready Submitted!";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                string strMsg2 = ex.Message.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
            }


            //if (dt.Rows[0]["Record Type"].ToString().Equals("Debit leg"))
            //{
            //    DtDebit = dt.Select("[Record Type]='Debit leg'").CopyToDataTable();
            //}
            //DtCredit = dt.Select("[Record Type]='Credit leg'").CopyToDataTable();


            //if (DtDebit.Rows.Count > 0)
            //{
            //    InsertAccountRecords(DtDebit, "dbo.tblDebit_Transaction_Detail");
            //}

            //InsertAccountRecords(DtCredit, "dbo.tblCredit_Transaction_Detail");


            if ((System.IO.File.Exists(csvlPath)))
            {
                System.IO.File.Delete(csvlPath);
            }

           

        }

        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    public DataTable GetDataTableFromCsv(string csvlfilePath)
    {
        DataTable csvdt = new DataTable();
        string csvData = File.ReadAllText(csvlfilePath);
        bool isfirstrow = true;

        foreach (string row in csvData.Split('\n'))
        {
            string rowvalue = row;
            rowvalue = rowvalue.Trim();
            rowvalue = rowvalue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

            if (!string.IsNullOrEmpty(rowvalue))
            {
                if (isfirstrow)
                {
                    string newrow = rowvalue;

                    newrow = newrow.Replace("'", "");
                    newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");

                    string[] split = newrow.Split(',');

                    foreach (string s in split)
                    {
                        csvdt.Columns.Add(s, typeof(System.String));
                    }
                    isfirstrow = false;
                }
                else
                {
                    string newrow = rowvalue;

                    newrow = newrow.Replace("'", "");
                    newrow = newrow.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", ",");

                    string[] split = newrow.Split(',');

                    DataRow dr = csvdt.NewRow();
                    int i = 0;
                    foreach (string s in split)
                    {
                        dr[i] = s;
                        i++;
                    }



                    //Debit Amount
                    //string debitAmt = dr["Debit Amount"].ToString();

                    //if (debitAmt == "null" || debitAmt == "" || debitAmt == "-")
                    //{ debitAmt = "0"; }
                    //dr["Debit Amount"] = debitAmt;

                    ////Credit Amount
                    //string creditAmt = dr["Credit Amount"].ToString();

                    //if (creditAmt == "null" || creditAmt == "" || creditAmt == "-")
                    //{ creditAmt = "0"; }
                    //dr["Credit Amount"] = creditAmt;

                    ////Transaction Date
                    //string trandtformat = dr["Transaction Date"].ToString();

                    //if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
                    //    trandtformat = "01/01/1900";
                    //else
                    //{
                    //    DateTime date = Convert.ToDateTime(DateTime.Parse(dr["Transaction Date"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //    trandtformat = date.ToString().Replace("-", "/");
                    //}

                    //trandtformat = PadDate(trandtformat);

                    //DateTime dttrandtformat = DateTime.ParseExact(trandtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //trandtformat = dttrandtformat.ToString("yyyy-MM-dd");
                    //dr["Transaction Date"] = trandtformat;


                    ////utr posted date
                    //string utrdtformat = dr["UTR Posted Date"].ToString();
                    //if (utrdtformat == "null" || utrdtformat == "" || utrdtformat == "-")
                    //    utrdtformat = "01/01/1900";
                    //else
                    //{
                    //    DateTime date = Convert.ToDateTime(DateTime.Parse(dr["UTR Posted Date"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    //    utrdtformat = date.ToString().Replace("-", "/");
                    //}
                    //utrdtformat = PadDate(utrdtformat);

                    //DateTime dtutrdtformat = DateTime.ParseExact(utrdtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //utrdtformat = dtutrdtformat.ToString("yyyy-MM-dd");
                    //dr["UTR Posted Date"] = utrdtformat;



                    csvdt.Rows.Add(dr);

                }
            }
        }
        return csvdt;
    }

    public string getData(DataTable dt)
    {
        XmlWriterSettings wsettings = new XmlWriterSettings();
        wsettings.NewLineOnAttributes = true;
        wsettings.Indent = true;
        wsettings.OmitXmlDeclaration = true;
        wsettings.Encoding = Encoding.UTF8;
        wsettings.CloseOutput = false;
        StringBuilder str = new StringBuilder();
        XmlWriter xw = XmlWriter.Create(str, wsettings);
        xw.WriteStartDocument();
        xw.WriteStartElement("ROOT");
        foreach (DataRow row in dt.Rows)
        {
            xw.WriteStartElement("ROWS");
            xw.WriteAttributeString("RECORD_TYPE", row["Record Type"].ToString());
            xw.WriteAttributeString("CORPORATE_ID", row["Corporte Id"].ToString());
            xw.WriteAttributeString("ePAYORDER_No", row["e-PayOrder No."].ToString());
            xw.WriteAttributeString("ACCOUNT_NUMBER", row["Account Number"].ToString());
            xw.WriteAttributeString("BRANCH_CODE", row["Branch Code"].ToString());
            xw.WriteAttributeString("MMID", row["MMID"].ToString());
            xw.WriteAttributeString("MOBILE_NUMBER", row["Mobile Number"].ToString());
            xw.WriteAttributeString("DEBIT_AMOUNT", row["Debit Amount"].ToString());
            xw.WriteAttributeString("CREDIT_AMOUNT", row["Credit Amount"].ToString());
            xw.WriteAttributeString("CORPORATE_REFERENCE_NUMBER", row["Corporate Reference Number"].ToString());
            xw.WriteAttributeString("DESCRIPTION", row["Description"].ToString());
            xw.WriteAttributeString("PAYMENT_TYPE", row["Payment Type"].ToString());
            xw.WriteAttributeString("TRANSACTION_STATUS", row["Transaction Status"].ToString());
            xw.WriteAttributeString("TRANSACTION_DATE", row["Transaction Date"].ToString());
            xw.WriteAttributeString("TRANSACTION_TIME", row["Transaction Time"].ToString());
            xw.WriteAttributeString("STATUS_DESCRIPTION", row["Status Description"].ToString());
            xw.WriteAttributeString("UTR_NUMBER", row["UTR Number"].ToString());
            xw.WriteAttributeString("UTR_POSTED_DATE", row["UTR Posted Date"].ToString());
            xw.WriteAttributeString("UTR_STATUS", row["UTR Status"].ToString());
            xw.WriteAttributeString("MAKER", row["Maker"].ToString());
            xw.WriteAttributeString("AUTHORIZER1", row["Authorizer1"].ToString());
            xw.WriteAttributeString("AUTHORIZER1_DATE", row["Authorizer1 Date"].ToString());
            xw.WriteAttributeString("AUTHORIZER2", row["Authorizer2"].ToString());
            xw.WriteAttributeString("AUTHORIZER2_DATE", row["Authorizer2 Date"].ToString());
            xw.WriteEndElement();
        }
        xw.WriteEndElement();
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        Response.Write(str.ToString());
        return str.ToString();
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


    public void InsertAccountRecords(DataTable DtRecord, string tabname)
    {

        string consString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                //Set the database table name
                sqlBulkCopy.DestinationTableName = tabname;

                //[OPTIONAL]: Map the Excel columns with that of the database table
                sqlBulkCopy.ColumnMappings.Add("Record Type", "RECORD_TYPE");
                sqlBulkCopy.ColumnMappings.Add("Corporte Id", "CORPORATE_ID");
                sqlBulkCopy.ColumnMappings.Add("e-PayOrder No.", "ePAYORDER_No");
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
                sqlBulkCopy.WriteToServer(DtRecord);
                con.Close();
            }
        }
    }
}