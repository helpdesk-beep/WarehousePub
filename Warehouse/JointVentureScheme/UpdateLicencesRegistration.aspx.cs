using System;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Web.UI;

public partial class JointVentureScheme_UpdateLicencesRegistration : System.Web.UI.Page
{
    OleDbConnection Econ;
    SqlConnection con;

    string constr, Query, sqlconn;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Update_LicencesRegistration_2020", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Update Licence Successfully.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                Label5.Text = "Successfully Upload.";
                Label5.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                Label5.Text = TheResult;
                Label5.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        using (SqlConnection constr = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Upload_Licence_File", constr);
            cmd.CommandType = CommandType.StoredProcedure;
            constr.Open();
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Table Truncate Successfully.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                Label5.Text = "Successfully Upload.";
                Label5.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                Label5.Text = TheResult;
                Label5.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //Upload and save the file
        string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(upFile.PostedFile.FileName);
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
        using (OleDbConnection excel_con = new OleDbConnection(conString))
        {
            excel_con.Open();
            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            DataTable dtExcelData = new DataTable();

            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[9]
            { 
                //new DataColumn("Id", typeof(int)),
                //new DataColumn("Name", typeof(string)),
                //new DataColumn("Salary", typeof(decimal))

                    new DataColumn("Whr_ID", typeof(string)),
                    new DataColumn("APPL_CODE", typeof(string)),
                    new DataColumn("Whr_Name", typeof(string)),
                    new DataColumn("DISTRICT_NAME", typeof(string)),
                    new DataColumn("District_ID", typeof(string)),
                    new DataColumn("Total_capicity", typeof(string)),
                    new DataColumn("Name_of_Owner", typeof(string)),
                    new DataColumn("IssuedAnugyptiDate", typeof(string)),
                    new DataColumn("IssuedAnugyptivalidityDate", typeof(string))

            });



            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
            {
                oda.Fill(dtExcelData);
            }
            excel_con.Close();

            string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.tbl_LicencesRegistration_2020_Upload_Dummy_FIle";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Whr_ID", "Whr_ID");
                    sqlBulkCopy.ColumnMappings.Add("APPL_CODE", "APPL_CODE");
                    sqlBulkCopy.ColumnMappings.Add("Whr_Name", "Whr_Name");
                    sqlBulkCopy.ColumnMappings.Add("DISTRICT_NAME", "DISTRICT_NAME");
                    sqlBulkCopy.ColumnMappings.Add("District_ID", "District_ID");
                    sqlBulkCopy.ColumnMappings.Add("Total_capicity", "Total_capicity");
                    sqlBulkCopy.ColumnMappings.Add("Name_of_Owner", "Name_of_Owner");
                    sqlBulkCopy.ColumnMappings.Add("IssuedAnugyptiDate", "IssuedAnugyptiDate");
                    sqlBulkCopy.ColumnMappings.Add("IssuedAnugyptivalidityDate", "IssuedAnugyptivalidityDate");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    string strMsg = "Successfully Upload.";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Label5.Text = "Successfully Upload.";
                    Label5.ForeColor = System.Drawing.Color.Green;
                    con.Close();
                }
            }
        }
    }
    //public string getData()
    //{
    //    //Upload and save the file
    //    string excelPath = Server.MapPath("../Files//") + Path.GetFileName(upFile.PostedFile.FileName);
    //    upFile.SaveAs(excelPath);

    //    string conString = string.Empty;
    //    string extension = Path.GetExtension(upFile.PostedFile.FileName);
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
    //            break;

    //    }
    //    conString = string.Format(conString, excelPath);
    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
    //    {
    //        excel_con.Open();
    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //        sheet1 = sheet1.Replace("'", "");
    //        DataTable dtDebit = new DataTable();
    //        DataTable dtCredit = new DataTable();

    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //        dtDebit.Columns.AddRange(new DataColumn[9]
    //        {
    //                new DataColumn("License Code", typeof(string)),
    //                new DataColumn("Application Code", typeof(string)),
    //                new DataColumn("Warehouse Name", typeof(string)),
    //                new DataColumn("District_ID", typeof(string)),
    //                new DataColumn("District", typeof(string)),
    //                new DataColumn("Licensed Capacity (MT)", typeof(string)),
    //                new DataColumn("Warehouse Man Name", typeof(string)),
    //                new DataColumn("Issuance Date", typeof(string)),
    //                new DataColumn("Valid Till", typeof(string))
    //        });


    //        string qr = string.Empty;
    //        qr = "SELECT * FROM [" + sheet1 + "]";
    //        using (OleDbDataAdapter oda = new OleDbDataAdapter(qr, excel_con))
    //        {
    //            oda.Fill(dtDebit);
    //        }

    //        excel_con.Close();
    //        XmlWriterSettings wsettings = new XmlWriterSettings();
    //        wsettings.NewLineOnAttributes = true;
    //        wsettings.Indent = true;
    //        wsettings.OmitXmlDeclaration = true;
    //        wsettings.Encoding = Encoding.UTF8;
    //        wsettings.CloseOutput = false;
    //        StringBuilder str = new StringBuilder();
    //        XmlWriter xw = XmlWriter.Create(str, wsettings);
    //        xw.WriteStartDocument();
    //        xw.WriteStartElement("ROOT");
    //        for (int i = 0; i < dtDebit.Rows.Count; i++)
    //        {
    //            xw.WriteStartElement("ROWS");
    //            xw.WriteAttributeString("Whr_ID", RemoveSingleQuoteStr(dtDebit.Rows[i]["License Code"].ToString()));
    //            xw.WriteAttributeString("APPL_CODE", RemoveSingleQuoteStr(dtDebit.Rows[i]["Application Code"].ToString()));
    //            xw.WriteAttributeString("Whr_Name", RemoveSingleQuoteStr(dtDebit.Rows[i]["Warehouse Name"].ToString()));
    //            xw.WriteAttributeString("District_ID", RemoveSingleQuoteStr(dtDebit.Rows[i]["District_ID"].ToString()));
    //            xw.WriteAttributeString("DISTRICT_NAME", RemoveSingleQuoteStr(dtDebit.Rows[i]["District"].ToString()));
    //            xw.WriteAttributeString("Total_capicity", RemoveSingleQuoteStr(dtDebit.Rows[i]["Licensed Capacity (MT)"].ToString()));
    //            xw.WriteAttributeString("Name_of_Owner", RemoveSingleQuoteStr(dtDebit.Rows[i]["Warehouse Man Name"].ToString()));
    //            xw.WriteAttributeString("IssuedAnugyptiDate", RemoveSingleQuoteStr(dtDebit.Rows[i]["Issuance Date"].ToString()));
    //            xw.WriteAttributeString("IssuedAnugyptivalidityDate", RemoveSingleQuoteStr(dtDebit.Rows[i]["Valid Till"].ToString()));
    //            xw.WriteEndElement();
    //        }
    //        xw.WriteEndElement();
    //        xw.WriteEndDocument();
    //        xw.Flush();
    //        xw.Close();
    //        Response.Write(str.ToString());
    //        if ((System.IO.File.Exists(excelPath)))
    //        {
    //            System.IO.File.Delete(excelPath);
    //        }
    //        return str.ToString();
    //    }
    //}
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Response.Write(getData());
    //        string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //        using (SqlConnection constr = new SqlConnection(CS))
    //        {
    //            SqlCommand cmd = new SqlCommand("Update_LicencesRegistration_2020", constr);
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            constr.Open();
    //            cmd.Parameters.AddWithValue("@strXML", getData());
    //            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //            cmd.ExecuteNonQuery();
    //            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //            if (TheResult.StartsWith("SUCCESS"))
    //            {
    //                string strMsg = "Successfully Upload.";

    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //                Label5.Text = "Successfully Upload.";
    //                Label5.ForeColor = System.Drawing.Color.Green;
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
    //                Label5.Text = TheResult;
    //                Label5.ForeColor = System.Drawing.Color.Red;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strMsg2 = ex.Message.ToString();
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //    }
    //}

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Upload and save the file
    //        string excelPath = Server.MapPath("../Files//") + Path.GetFileName(upFile.PostedFile.FileName);
    //        upFile.SaveAs(excelPath);

    //        string conString = string.Empty;
    //        string extension = Path.GetExtension(upFile.PostedFile.FileName);
    //        switch (extension)
    //        {
    //            case ".xls": //Excel 97-03
    //                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

    //                break;
    //            case ".xlsx": //Excel 07 or higher
    //                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
    //                break;

    //        }
    //        conString = string.Format(conString, excelPath);
    //        using (OleDbConnection excel_con = new OleDbConnection(conString))
    //        {
    //            excel_con.Open();
    //            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //            sheet1 = sheet1.Replace("'", "");
    //            DataTable dtDebit = new DataTable();
    //            DataTable dtCredit = new DataTable();

    //            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //            dtDebit.Columns.AddRange(new DataColumn[8]
    //            {
    //                new DataColumn("License Code", typeof(string)),
    //                new DataColumn("Application Code", typeof(string)),
    //                new DataColumn("Warehouse Name", typeof(string)),
    //                new DataColumn("District_ID", typeof(string)),
    //                new DataColumn("District", typeof(string)),
    //                new DataColumn("Licensed Capacity (MT)", typeof(string)),
    //                new DataColumn("Warehouse Man Name", typeof(string)),
    //                new DataColumn("Issuance Date", typeof(string))
    //            });


    //            string qr = string.Empty;
    //            qr = "SELECT * FROM [" + sheet1 + "]";
    //            using (OleDbDataAdapter oda = new OleDbDataAdapter(qr, excel_con))
    //            {
    //                oda.Fill(dtDebit);
    //            }

    //            excel_con.Close();

    //            for (int i = 0; i < dtDebit.Rows.Count; i++)
    //            {
    //                string val = string.Empty;
    //                dtDebit.Rows[i]["License Code"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["License Code"].ToString());
    //                dtDebit.Rows[i]["Application Code"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Application Code"].ToString());
    //                dtDebit.Rows[i]["Warehouse Name"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Warehouse Name"].ToString());
    //                dtDebit.Rows[i]["District_ID"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["District_ID"].ToString());
    //                dtDebit.Rows[i]["District"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["District"].ToString());
    //                dtDebit.Rows[i]["Licensed Capacity (MT)"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Licensed Capacity (MT)"].ToString());
    //                dtDebit.Rows[i]["Warehouse Man Name"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Warehouse Man Name"].ToString());
    //                dtDebit.Rows[i]["Issuance Date"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Issuance Date"].ToString());
    //                dtDebit.Rows[i]["Valid Till"] = RemoveSingleQuoteStr(dtDebit.Rows[i]["Valid Till"].ToString());



    //                //Issuance Date
    //                string trandtformat = dtDebit.Rows[i]["Issuance Date"].ToString();

    //                if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
    //                    trandtformat = "01/01/1900";
    //                else
    //                {
    //                    DateTime date = Convert.ToDateTime(DateTime.Parse(dtDebit.Rows[i]["Issuance Date"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
    //                    trandtformat = date.ToString().Replace("-", "/");
    //                }

    //                trandtformat = PadDate(trandtformat);

    //                DateTime dttrandtformat = DateTime.ParseExact(trandtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //                trandtformat = dttrandtformat.ToString("yyyy-MM-dd");
    //                dtDebit.Rows[i]["Issuance Date"] = trandtformat;

    //                //utr posted date
    //                string utrdtformat = dtDebit.Rows[i]["Valid Till"].ToString();

    //                if (utrdtformat == "null" || utrdtformat == "" || utrdtformat == "-")
    //                    utrdtformat = "01/01/1900";
    //                else
    //                {
    //                    DateTime date = Convert.ToDateTime(DateTime.Parse(dtDebit.Rows[i]["Valid Till"].ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
    //                    utrdtformat = date.ToString().Replace("-", "/");
    //                }


    //                utrdtformat = PadDate(utrdtformat);

    //                DateTime dtutrdtformat = DateTime.ParseExact(utrdtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //                utrdtformat = dtutrdtformat.ToString("yyyy-MM-dd");
    //                dtDebit.Rows[i]["Valid Till"] = utrdtformat;

    //            }

    //            string consString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //            using (SqlConnection con = new SqlConnection(consString))
    //            {
    //                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //                {
    //                    //Set the database table name
    //                    sqlBulkCopy.DestinationTableName = "dbo.tbl_LicencesRegistration_2020";

    //                    //[OPTIONAL]: Map the Excel columns with that of the database table
    //                    sqlBulkCopy.ColumnMappings.Add("License Code", "Whr_ID");
    //                    sqlBulkCopy.ColumnMappings.Add("Application Code", "APPL_CODE");
    //                    sqlBulkCopy.ColumnMappings.Add("Warehouse Name", "Whr_Name");
    //                    sqlBulkCopy.ColumnMappings.Add("District_ID", "District_ID");
    //                    sqlBulkCopy.ColumnMappings.Add("District", "DISTRICT_NAME");
    //                    sqlBulkCopy.ColumnMappings.Add("Licensed Capacity (MT)", "Total_capicity");
    //                    sqlBulkCopy.ColumnMappings.Add("Warehouse Man Name", "Name_of_Owner");
    //                    sqlBulkCopy.ColumnMappings.Add("Issuance Date", "IssuedAnugyptiDate");
    //                    sqlBulkCopy.ColumnMappings.Add("Valid Till", "IssuedAnugyptivalidityDate");

    //                    con.Open();
    //                    sqlBulkCopy.WriteToServer(dtDebit);
    //                    con.Close();
    //                }
    //            }

    //        }

    //        Label5.Text = "Successfully Upload.";
    //        Label5.ForeColor = System.Drawing.Color.Green;

    //        if ((System.IO.File.Exists(excelPath)))
    //        {
    //            System.IO.File.Delete(excelPath);
    //        }

    //    }

    //    catch (Exception ex)
    //    {
    //        Label5.Text = ex.Message;
    //        Label5.ForeColor = System.Drawing.Color.Red;
    //    }
    //}


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
    //void InitializeOledbConnection(string filename, string extrn)
    //{
    //    string connString = "";
    //    string filename1 = Path.GetFileName(upFile.PostedFile.FileName);

    //    if (extrn == ".xls")
    //        //Connectionstring for excel v8.0    

    //        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
    //    else
    //        //Connectionstring fo excel v12.0    
    //        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";

    //    Econ = new OleDbConnection(connString);
    //}
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    //try
    //    //{
    //    string filename = Path.GetFileNameWithoutExtension(upFile.PostedFile.FileName);
    //    filename = filename + ".csv";
    //    string csvlPath = Server.MapPath("../Audit//") + filename;
    //    StreamWriter outputFile = new StreamWriter(csvlPath, false, new UTF8Encoding(true));
    //    upFile.SaveAs(csvlPath);


    //    DataTable dt = GetDataTableFromCsv(csvlPath);




    //    InsertAccountRecords(dt, "[dbo].[tbl_LicencesRegistration_2020]");



    //    if ((System.IO.File.Exists(csvlPath)))
    //    {
    //        System.IO.File.Delete(csvlPath);
    //    }

    //    Label5.Text = "Successfully Upload.";
    //    Label5.ForeColor = System.Drawing.Color.Green;

    //    // }

    //    //catch (Exception ex)
    //    //{
    //    //    Label5.Text = "Not Upload File is not currect format";
    //    //    Label5.ForeColor = System.Drawing.Color.Red;
    //    //}


    //}


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

    //        if (!string.IsNullOrEmpty(rowvalue))
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


    //                //Issuance Date
    //                string trandtformat = dr["Issuance Date"].ToString();

    //                if (trandtformat == "null" || trandtformat == "" || trandtformat == "-")
    //                    trandtformat = "01/01/1900";

    //                trandtformat = PadDate(trandtformat);

    //                DateTime dttrandtformat = DateTime.ParseExact(trandtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //                trandtformat = dttrandtformat.ToString("yyyy-MM-dd");
    //                dr["Issuance Date"] = trandtformat;

    //                //utr posted date
    //                string utrdtformat = dr["Valid Till"].ToString();
    //                if (utrdtformat == "null" || utrdtformat == "" || utrdtformat == "-")
    //                    utrdtformat = "01/01/1900";

    //                utrdtformat = PadDate(utrdtformat);

    //                DateTime dtutrdtformat = DateTime.ParseExact(utrdtformat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    //                utrdtformat = dtutrdtformat.ToString("yyyy-MM-dd");
    //                dr["Valid Till"] = utrdtformat;

    //                csvdt.Rows.Add(dr);

    //            }
    //        }
    //    }
    //    return csvdt;
    //}


    //public static string PadDate(string strDateTime)
    //{

    //    string strNewDate = "";

    //    int intdd = strDateTime.IndexOf(@"/");

    //    string strdd = strDateTime.Substring(0, intdd);

    //    int intlegdd = strdd.Length;

    //    if (intlegdd < 2)
    //    {

    //        strdd = "0" + strdd;

    //    }

    //    int intMM = strDateTime.IndexOf(@"/", intdd + 1);

    //    string strMM = "";

    //    strMM = strDateTime.Substring(intdd + 1, 2);

    //    int intMM1 = strMM.IndexOf(@"/");

    //    if (intMM1 > -1)
    //    {

    //        strMM = strMM.Replace(@"/", "");

    //        strMM = "0" + strMM;

    //    }

    //    int intyy = strDateTime.LastIndexOf(@"/");

    //    string stryy = strDateTime.Substring(intyy + 1, 4);

    //    strNewDate = strdd + @"/" + strMM + @"/" + stryy;

    //    return strNewDate;

    //}


    //public void InsertAccountRecords(DataTable DtRecord, string tabname)
    //{

    //    string consString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(consString))
    //    {
    //        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //        {
    //            //Set the database table name
    //            sqlBulkCopy.DestinationTableName = tabname;

    //            //[OPTIONAL]: Map the Excel columns with that of the database table
    //            sqlBulkCopy.ColumnMappings.Add("License Code", "Whr_ID");
    //            sqlBulkCopy.ColumnMappings.Add("Application Code", "APPL_CODE");
    //            sqlBulkCopy.ColumnMappings.Add("Warehouse Name", "Whr_Name");
    //            sqlBulkCopy.ColumnMappings.Add("District_ID", "District_ID");
    //            sqlBulkCopy.ColumnMappings.Add("District", "DISTRICT_NAME");
    //            sqlBulkCopy.ColumnMappings.Add("Licensed Capacity (MT)", "Total_capicity");
    //            sqlBulkCopy.ColumnMappings.Add("Warehouse Man Name", "Name_of_Owner");
    //            sqlBulkCopy.ColumnMappings.Add("Issuance Date", "IssuedAnugyptiDate");
    //            sqlBulkCopy.ColumnMappings.Add("Valid Till", "IssuedAnugyptivalidityDate");

    //            con.Open();
    //            sqlBulkCopy.WriteToServer(DtRecord);
    //            con.Close();
    //        }
    //    }
    //}
    //private void ExcelConn(string FilePath)
    //{
    //    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
    //            break;

    //    }
    //    //constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
    //    Econ = new OleDbConnection(constr);

    //}
    //private void connection()
    //{
    //    sqlconn = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    con = new SqlConnection(sqlconn);
    //}


    //private void InsertExcelRecords(string FilePath)
    //{
    //    ExcelConn(FilePath);

    //    Query = string.Format("Select [License Code] Whr_ID,[Application Code] APPL_CODE,[Warehouse Name] Whr_Name,[District_ID] District_ID,[District] DISTRICT_NAME,[Licensed Capacity (MT)] Total_capicity,[Warehouse Man Name] Name_of_Owner,[Issuance Date] IssuedAnugyptiDate,[Valid Till] IssuedAnugyptivalidityDate FROM [{0}]", "Sheet1$");
    //    OleDbCommand Ecom = new OleDbCommand(Query, Econ);
    //    Econ.Open();

    //    DataSet ds = new DataSet();
    //    OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
    //    Econ.Close();
    //    oda.Fill(ds);
    //    DataTable Exceldt = ds.Tables[0];
    //    connection();
    //    //creating object of SqlBulkCopy  
    //    SqlBulkCopy objbulk = new SqlBulkCopy(con);
    //    //assigning Destination table name  
    //    objbulk.DestinationTableName = "[dbo].[tbl_LicencesRegistration_2020]";
    //    //Mapping Table column  
    //    objbulk.ColumnMappings.Add("Whr_ID", "Whr_ID");
    //    objbulk.ColumnMappings.Add("APPL_CODE", "APPL_CODE");
    //    objbulk.ColumnMappings.Add("Whr_Name", "Whr_Name");
    //    objbulk.ColumnMappings.Add("DISTRICT_NAME", "DISTRICT_NAME");
    //    objbulk.ColumnMappings.Add("District_ID", "District_ID");
    //    objbulk.ColumnMappings.Add("Total_capicity", "Total_capicity");
    //    objbulk.ColumnMappings.Add("Name_of_Owner", "Name_of_Owner");
    //    objbulk.ColumnMappings.Add("IssuedAnugyptiDate", "IssuedAnugyptiDate");
    //    objbulk.ColumnMappings.Add("IssuedAnugyptivalidityDate", "IssuedAnugyptivalidityDate");
    //    //inserting Datatable Records to DataBase  
    //    con.Open();
    //    objbulk.WriteToServer(Exceldt);
    //    con.Close();
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
    //    InsertExcelRecords(CurrentFilePath);
    //}
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    Response.Redirect("JointVentureSchemeApp.aspx");
    //}
    //protected void Button1_Click1(object sender, EventArgs e)
    //{
    //    Response.Redirect("UpdateLicencesRegistration.aspx");
    //}
}