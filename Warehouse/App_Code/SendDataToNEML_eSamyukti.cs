using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for SendDataToNEML_eSamyukti
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SendDataToNEML_eSamyukti : System.Web.Services.WebService
{

    public SendDataToNEML_eSamyukti()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    // Replace with your actual connection string
    //private string connString = "Server=myServer;Database=myDB;User Id=myUser;Password=myPassword;";

    [WebMethod(Description = "Securely fetches data from the stored procedure")]
    public string GetProcedureData(string username, string password, string FromDate, string ToDate)
    {
        try
        {
            if (!IsValidUser(username, password))
            {
                return "Error: Authentication Failed. Invalid credentials.";
            }

            return ExecuteStoredProcedure(FromDate, ToDate);
        }
        catch (ConfigurationErrorsException)
        {
            return "Error: eSamyukti service credentials are not configured.";
        }
        catch (Exception)
        {
            return "Error executing procedure.";
        }
    }

    private bool IsValidUser(string user, string pass)
    {
        string expectedUser = WarehouseApiSecurity.GetRequiredSetting("ESamyuktiServiceUsername");
        string expectedPassword = WarehouseApiSecurity.GetRequiredSetting("ESamyuktiServicePassword");
        return String.Equals(user, expectedUser, StringComparison.Ordinal) &&
               String.Equals(pass, expectedPassword, StringComparison.Ordinal);
    }

    private string ExecuteStoredProcedure(string FromDate, string ToDate)
    {

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("NCCF_WHR_Data_For_E_Samyukti_Portal", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add your parameters here
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                cmd.Parameters.AddWithValue("@ToDate", ToDate);
                cmd.CommandTimeout = 3600;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Convert DataTable to a simple JSON string or XML
                return ConvertDataTableToJSON(dt);
            }
        }
    }
    private string ConvertDataTableToJSON11(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        // Set the limit to the maximum possible integer value
        serializer.MaxJsonLength = Int32.MaxValue;

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            var row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    private string ConvertDataTableToJSON(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;

        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    // Check if the value is a DateTime
                    if (dr[col] is DateTime)
                    {
                        // Manually format the date to ISO 8601 string
                        row.Add(col.ColumnName, ((DateTime)dr[col]).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        else
            return serializer.Serialize("No Record Found");
    }


    ////------------------generate pdf ---------------------------



    private SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    // [WebMethod(EnableSession = true)]
    public string GenerateWHR_PDF(string whrNo, string commodityId)
    {
        try
        {
            // 1. Fetch Data (Replicating your whrdetail and qtyissuedtl logic)
            DataTable dtWHR = GetWHRData(whrNo);
            DataTable dtDelivery = GetDeliveryData(whrNo);

            if (dtWHR.Rows.Count == 0) return "No record found";

            // 2. Build HTML String for both receipts
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<html><head><style>");
            sbHtml.Append(".fountcolor { color: #FF5050; font-size: 10px; }");
            sbHtml.Append("table { border-collapse: collapse; width: 100%; }");
            sbHtml.Append(".border-table { border: 1px solid #FF5050; }");
            sbHtml.Append("</style></head><body>");

            // Build Original Copy [cite: 45]
            sbHtml.Append(BuildReceiptHtml(dtWHR, dtDelivery, "ORIGINAL COPY"));

            // Page Break [cite: 79]
            sbHtml.Append("<div style='page-break-after:always;'>&nbsp;</div>");

            // Build Office Copy [cite: 118]
            sbHtml.Append(BuildReceiptHtml(dtWHR, dtDelivery, "OFFICE COPY"));

            sbHtml.Append("</body></html>");

            // 3. Convert HTML to PDF
            return CreatePdfFromHtml(sbHtml.ToString(), whrNo);
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }

    private string BuildReceiptHtml(DataTable dt, DataTable dtDel, string copyType)
    {
        StringBuilder sb = new StringBuilder();
        // Simplified Header Table [cite: 46, 47]
        sb.Append("<table class='border-table' cellpadding='5'>");
        sb.Append("<tr><td align='center' colspan='2'><b>WAREHOUSE RECEIPT (Form-III)</b><br/>" + copyType + "</td></tr>");
        sb.Append("<tr><td>WHR No: " + dt.Rows[0]["Depositor_WHR_Id"] + "</td>");
        sb.Append("<td align='right'>Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr>");

        // Depositor Info [cite: 50, 51]
        sb.Append("<tr><td colspan='2'>Depositor: " + dt.Rows[0]["Depositor_Name"] + "</td></tr>");
        sb.Append("<tr><td colspan='2'>Warehouse: " + dt.Rows[0]["Godown_Name"] + "</td></tr>");

        // Commodity Table (Replicating ListView1) [cite: 55, 67]
        sb.Append("<tr><td colspan='2'><table border='1'>");
        sb.Append("<tr bgcolor='#C0C0C0'><td>Commodity</td><td>Category</td><td>Bags</td><td>Weight</td></tr>");
        foreach (DataRow row in dt.Rows)
        {
            sb.Append("<tr><td>" + row["Commodity_Name"] + "</td><td>" + row["Category_Name"] + "</td>");
            sb.Append("<td>" + row["TotalBags_Received"] + "</td><td>" + row["Total_Qty_Received"] + "</td></tr>");
        }
        sb.Append("</table></td></tr>");

        // Delivery Table (Replicating ListView2)
        sb.Append("<tr><td colspan='2'><br/><b>Delivery History:</b><table border='1'>");
        sb.Append("<tr bgcolor='#C0C0C0'><td>Date</td><td>Qty Released</td><td>Balance Qty</td></tr>");
        if (dtDel.Rows.Count > 0)
        {
            foreach (DataRow dr in dtDel.Rows)
            {
                sb.Append("<tr><td>" + dr["Date"] + "</td><td>" + dr["DelQty"] + "</td><td>" + dr["AvlQty"] + "</td></tr>");
            }
        }
        else { sb.Append("<tr><td colspan='3' align='center'>No release records</td></tr>"); }
        sb.Append("</table></td></tr></table>");

        return sb.ToString();
    }

    private string CreatePdfFromHtml(string html, string whrNo)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            using (StringReader sr = new StringReader(html))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
            }

            document.Close();
            byte[] bytes = ms.ToArray();

            // Save to server or return as Base64
            string fileName = "WHR_" + whrNo + "_" + DateTime.Now.Ticks + ".pdf";
            string filePath = Server.MapPath("~/GeneratedPDFs/" + fileName);
            File.WriteAllBytes(filePath, bytes);

            return "GeneratedPDFs/" + fileName; // Return the relative path for the client
        }
    }

    private DataTable GetWHRData(string whrNo)
    {
        DataTable dt = new DataTable();
        // Using the connection string from your source 
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString()))
        {
            // Replicating the query from whrdetail() 
            string query = @"SELECT DISTINCT WHR.Depositor_WHR_Id, WHR.Commodity_Id, 
                        ('(' + CONVERT(NVARCHAR(50), FLOOR(CAST(WHR.MktValue_of_Commodity AS DECIMAL(18,2)) * CAST(WHR.Total_Qty_Received AS DECIMAL(18,4)))) + ')' + 
                        dbo.[AnkNumberToWords](FLOOR(CAST(WHR.MktValue_of_Commodity AS DECIMAL(18,2)) * CAST(WHR.Total_Qty_Received AS DECIMAL(18,4)))) + 
                        ' Rupees') AS MktValue_of_Commodity, 
                        CAST(WHR.MktValue_of_Commodity AS DECIMAL(18,2)) AS MktValue_of_Commodityno, 
                        COMM.Commodity_Name, 
                        WHR.Depositor_Name, 
                        LicenseNo, 
                        LicenseDate, 
                        DEP.DepotName, 
                        CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate, 
                        CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS WHR_Issue_Date, 
                        CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate, 
                        ('Moisture % ' + CONVERT(NVARCHAR(250), CAST((CAST(WHR.AvgMoisture_Content AS DECIMAL(18,2)) + CAST(WHR.AvgMoisture_Content_To AS DECIMAL(18,2))) / 2 AS DECIMAL(18,2))) + ' ' + ISNULL(WHR.Remark, '')) AS Remark, 
                        WHR.TotalBags_Received, 
                        CAST(WHR.Total_Qty_Received AS DECIMAL(18,4)) AS Total_Qty_Received, 
                        GOD.Godown_Name, 
                        CAT.Category_Name 
                    FROM tbl_storage_Depositor_WHR_Relation AS WHR WITH (NOLOCK)
                    INNER JOIN tbl_storage_Stacking_Details AS TSD WITH (NOLOCK) 
                        ON WHR.Depositor_WHR_Id = TSD.WHRId 
                    INNER JOIN tbl_MetaData_STORAGE_CATEGORY AS CAT WITH (NOLOCK) 
                        ON WHR.Category_Id = CAT.Category_Id 
                    INNER JOIN tbl_MetaData_STORAGE_COMMODITY AS COMM WITH (NOLOCK) 
                        ON WHR.Commodity_Id = COMM.Commodity_Id 
                    INNER JOIN tbl_MetaData_GODOWN AS GOD WITH (NOLOCK) 
                        ON TSD.Godown_ID = GOD.Godown_ID 
                    INNER JOIN tbl_MetaData_DEPOT AS DEP WITH (NOLOCK) 
                        ON WHR.BranchID = DEP.BranchID 

                    WHERE WHR.Depositor_WHR_Id = @whrNo --AND WHR.BranchID = @branchId";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@whrNo", whrNo);
                //cmd.Parameters.AddWithValue("@branchId", HttpContext.Current.Session["BranchId"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }

    private DataTable GetDeliveryData(string whrNo)
    {
        DataTable dtRaw = new DataTable();
        DataTable dtFinal = new DataTable();
        dtFinal.Columns.Add("Date");
        dtFinal.Columns.Add("DelQty");
        dtFinal.Columns.Add("DelBags");
        dtFinal.Columns.Add("AvlQty");
        dtFinal.Columns.Add("AvlBags");

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString()))
        {
            // Replicating the query from qtyissuedtl() 
            string query = @"SELECT ISNULL(ssd.[No_Of_Bags], 0) AS No_Of_Bags, ISNULL(ssd.[Bags_Weight], 0) AS Bags_Weight, ISNULL(WHR.Total_Qty_Received, 0) AS Total_Qty_Received, 
                        ISNULL(WHR.TotalBags_Received, 0) AS TotalBags_Received, 
                        ISNULL(ssd.Loss, 0) AS Loss, 
                        ISNULL(ssd.Gain, 0) AS Gain, 
                        CONVERT(VARCHAR(10), sge.Issue_Date, 103) AS ID 
                    FROM [tbl_Delivery_Stacking_Details_GatePass] AS ssd WITH (NOLOCK)
                    INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR WITH (NOLOCK) 
                        ON ssd.Depositor_WHR_Id = WHR.Depositor_WHR_Id 
                    INNER JOIN dbo.tbl_Storage_GatePass_Enrty AS sge WITH (NOLOCK) 
                        ON ssd.GatePass_No = sge.GatePass_No 
                    WHERE WHR.Depositor_WHR_Id = @whrNo 
                    ORDER BY sge.Issue_Date ASC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@whrNo", whrNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtRaw);
            }

            if (dtRaw.Rows.Count > 0)
            {
                // Initial totals from the first row 
                decimal avilableQty = Convert.ToDecimal(dtRaw.Rows[0]["Total_Qty_Received"]);
                int avilableBags = Convert.ToInt32(dtRaw.Rows[0]["TotalBags_Received"]);

                foreach (DataRow row in dtRaw.Rows)
                {
                    // Calculating remaining balance based on your logic 
                    avilableQty = avilableQty - Convert.ToDecimal(row["Bags_Weight"]) - Convert.ToDecimal(row["Loss"]) + Convert.ToDecimal(row["Gain"]);
                    avilableBags = avilableBags - Convert.ToInt32(row["No_Of_Bags"]);

                    dtFinal.Rows.Add(
                        row["ID"].ToString(),
                        row["Bags_Weight"].ToString(),
                        row["No_Of_Bags"].ToString(),
                        avilableQty.ToString(),
                        avilableBags.ToString()
                    );
                }
            }
        }
        return dtFinal;
    }

    string imagePath = "";

    [WebMethod(EnableSession = true)]
    public void DownloadWHRAsPDF(string whrNo)
    {
        WarehouseApiSecurity.RequireAuthenticatedSession(Session);
        try
        {
            // 1. Generate the filled HTML string using your core logic
            string htmlContent = GenerateWHRReceipt(whrNo);

            // 2. Convert HTML to PDF (Using a library like SelectPdf)
            // Note: Replace with your specific library's conversion call
            // Use MapPath to get the full disk path


            // Replace the relative path in your HTML string
            htmlContent = htmlContent.Replace("images/dsc1.png", imagePath);
            htmlContent = htmlContent.Trim();
            var converter = new SelectPdf.HtmlToPdf();

            // ADD THESE TWO LINES:
            converter.Options.AutoFitHeight = SelectPdf.HtmlToPdfPageFitMode.NoAdjustment;
            converter.Options.WebPageHeight = 0; // Forces the engine to calculate actual height

            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;
            converter.Options.MaxPageLoadTime = 300;
            converter.Options.MinPageLoadTime = 0;
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;

            // Generate the PDF document
            var doc = converter.ConvertHtmlString(htmlContent);
            while (doc.Pages.Count > 2)
            {
                // Always remove the last page until only 2 remain
                doc.RemovePageAt(doc.Pages.Count - 1);
            }
            byte[] pdfBytes = doc.Save();
            doc.Close();

            // 3. Prepare Response for Download
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=WHR_" + whrNo + ".pdf");
            HttpContext.Current.Response.BinaryWrite(pdfBytes);
            HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();



        }
        catch (NullReferenceException ex) { throw new Exception("शाखा अथवा गोदाम द्वारा इस WHR पर डीएससी (डिजिटल हस्ताक्षर) नहीं किया गया है।"); }    }


    public string GenerateWHRReceipt(string whrNo)
    {
        // Load your HTML template from the file
        string html = File.ReadAllText(Server.MapPath("REcieptHTMLNEW.html"));
        string qrData = "";
        // 1. Fetch Main WHR Data (Replicating logic from whrdetail() )
        DataTable dtMain = GetMainWHRData(whrNo); //Commodity_Id
        dtMain.Columns.Add("DSC_Holder_Name");
        dtMain.Columns.Add("WHR_Signing_Date");
        dtMain.Columns.Add("Client_Ip");
        if (dtMain.Rows.Count > 0)
        {
            DataRow mainRow = dtMain.Rows[0];

            // Fill Main Information
            string Depositor_Form_No = GetDepositorForm_Detail(mainRow["Depositor_WHR_Id"].ToString());
            html = html.Replace("{{DepotName}}", mainRow["DepotName"].ToString());
            html = html.Replace("{{Address}}", mainRow["DepotAddress"].ToString());
            html = html.Replace("{{WHRNo}}", mainRow["Depositor_WHR_Id"].ToString());
            html = html.Replace("{{DepositorName}}", mainRow["Depositor_Name"].ToString());
            html = html.Replace("{{WarehousePalName}}", mainRow["NodalOfficeName"].ToString());
            html = html.Replace("{{DepositorFormNo}}", Depositor_Form_No);
            html = html.Replace("{{LicenseNo}}", mainRow["LicenseNo"].ToString());
            html = html.Replace("{{LicenseExpiry}}", mainRow["LicenseDate"].ToString());
            html = html.Replace("{{WHRDate}}", mainRow["WHR_Issue_Date"].ToString());
            html = html.Replace("{{StorageDateFrom}}", mainRow["StorageRateDate"].ToString());
            html = html.Replace("{{Copy1Type}}", "अपरक्राम्य");
            html = html.Replace("{{Copy1TypeN}}", "Not Negotiable");
            html = html.Replace("{{Copy2Type}}", "अपरक्राम्य");
            html = html.Replace("{{Copy2TypeN}}", "Not Negotiable");



            // 2. Populate Commodity Table (Logic from ListView1 )
            StringBuilder commHtml = new StringBuilder();
            decimal TotalVal = 0;
            foreach (DataRow row in dtMain.Rows)
            {
                commHtml.Append("<tr>");
                commHtml.Append("<td> " + row["Commodity_Name"] + "</td>");
                commHtml.Append("<td>" + row["Category_Name"] + "</td>");
                commHtml.Append("<td>" + row["TotalBags_Received"] + "</td>");
                commHtml.Append("<td>" + row["Total_Qty_Received"] + "</td>");
                commHtml.Append("<td>" + row["MktValue_of_Commodityno"] + "</td>");
                TotalVal = Convert.ToDecimal(row["Total_Qty_Received"]) * Convert.ToDecimal(row["MktValue_of_Commodityno"]);
                /// commHtml.Append("<td>" + row["MktValue_of_Commodity"] + "</td>");
                commHtml.Append("<td>" + TotalVal + "</td>");
                commHtml.Append("<td>" + row["Remark"] + "</td>");
                commHtml.Append("</tr>");
            }
            html = html.Replace("{{CommodityRows}}", commHtml.ToString());

            // 3. Populate Delivery Records (Logic from qtyissuedtl() )

            decimal avlQty = Convert.ToDecimal(mainRow["Total_Qty_Received"]);
            int avlBags = Convert.ToInt32(mainRow["TotalBags_Received"]);
            DataTable dtDel = GetDeliveryHistory(whrNo, avlQty, avlBags);

            html = html.Replace("{{GodownName}}", mainRow["Godown_Name"].ToString());
            html = html.Replace("{{WarehouseName}}", mainRow["Godown_Name"].ToString());

            StringBuilder delHtml = new StringBuilder();

            foreach (DataRow row in dtDel.Rows)
            {
                // Calculate running totals as done in the source 
                avlQty -= Convert.ToDecimal(row["DelQty"]);
                avlBags -= Convert.ToInt32(row["DelBags"]);

                delHtml.Append("<tr>");
                delHtml.Append("<td>" + row["ID"] + "</td><td>" + row["DelQty"] + "</td><td>" + row["DelBags"] + "</td>");
                delHtml.Append("<td></td><td>" + avlQty + "</td><td>" + avlBags + "</td>");
                delHtml.Append("</tr>");
            }
            if (delHtml.Length == 0)
            {
                delHtml.Append("<tr>");
                delHtml.Append("<td> </td><td> </td><td> </td>");
                delHtml.Append("<td> </td><td> </td><td> </td>");
                delHtml.Append("</tr>");
            }
            html = html.Replace("{{DeliveryRows}}", delHtml.ToString());

            string hiredtype = mainRow["Hired_Type"].ToString();



            ////Verify_eWHR(mainRow["Godown_Name"].ToString());
            // 4. Digital Signature Logic (Logic from Verify_eWHR() )
            bool ValidForGBDSC = false;
            if (hiredtype == "Joint Venture(JV)" || hiredtype == "WDRA")
                ValidForGBDSC = true;


            string query1 = "";
            string Commodity_Id = mainRow["Commodity_Id"].ToString();
            if (Commodity_Id == "63" || Commodity_Id == "64" || Commodity_Id == "33" || Commodity_Id == "75")
            {
                //query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2019 as DSWHR inner join tbl_Digital_Signature_Kharif2019 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2019 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
                query1 = "SELECT Distinct DSWHR.DSC_User_Type,[Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_CMS2026 as DSWHR inner join tbl_Digital_Signature_CMS2026 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_CMS2026 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + mainRow["Depositor_WHR_Id"] + "'" +
                    " and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No order by DSC_User_Type asc";
            }
            else if (Commodity_Id == "22")
            {
                query1 = "SELECT Distinct DSWHR.DSC_User_Type,[Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Wheat2020 as DSWHR inner join tbl_Digital_Signature_Wheat2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Wheat2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id " +
                    "where DSWHR.Depositor_WHR_Id='" + mainRow["Depositor_WHR_Id"] + "'  and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No order by DSC_User_Type asc";
            }
            else if (Commodity_Id == "13" || Commodity_Id == "8" || Commodity_Id == "11")
            {
                query1 = "SELECT Distinct DSWHR.DSC_User_Type,[Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2020 as DSWHR inner join tbl_Digital_Signature_Kharif2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id " +
                    "where DSWHR.Depositor_WHR_Id='" + mainRow["Depositor_WHR_Id"] + "' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No order by DSC_User_Type asc";
            }

            SqlCommand cmd = new SqlCommand(query1, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet XMLds = new DataSet();
            da.Fill(XMLds);
            DataTable dt = XMLds.Tables[0].Rows.Count > 0 ? XMLds.Tables[0] : null;
            DataRow Row0 = dt.Rows.Count > 0 ? dt.Rows[0] : null;
            DataRow Row1 = dt.Rows.Count > 1 ? dt.Rows[1] : null;
            if (ValidForGBDSC)
            {
                if (!Verify_DSC_Hash(mainRow)) // Simplified call to your verification logic
                {

                    if (Row0 != null)
                    {
                        string dscText = "e-WHR Digitally Signed By: " +
                         (!string.IsNullOrEmpty(Row0["DSC_Holder_Name"].ToString()) ? Row0["DSC_Holder_Name"].ToString() : "___________") +
                            "<br> Sign Date: " + (!string.IsNullOrEmpty(Row0["CreatedDate"].ToString()) ? Row0["CreatedDate"].ToString() : "___________") +
                            "<br> IP: " + (!string.IsNullOrEmpty(Row0["Client_Ip"].ToString()) ? Row0["Client_Ip"].ToString() : "___________");
                        html = html.Replace("{{BranchDSC}}", dscText);
                        // html = html.Replace("{{DSC_Section1}}", dscText);
                        html = html.Replace("{{BranchDSC}}", dscText);
                        //  html = html.Replace("{{DSC_Section2}}", dscText);
                    }
                    if (Row1 != null)
                    {
                        string dscText = "e-WHR Digitally Signed By: " +
                         (!string.IsNullOrEmpty(Row1["DSC_Holder_Name"].ToString()) ? Row1["DSC_Holder_Name"].ToString() : "___________") +
                            "<br> Sign Date: " + (!string.IsNullOrEmpty(Row1["CreatedDate"].ToString()) ? Row1["CreatedDate"].ToString() : "___________") +
                            "<br> IP: " + (!string.IsNullOrEmpty(Row1["Client_Ip"].ToString()) ? Row1["Client_Ip"].ToString() : "___________");
                        html = html.Replace("{{GodownDSC}}", dscText);
                        // html = html.Replace("{{DSC_Section1}}", dscText);
                        html = html.Replace("{{GodownDSC}}", dscText);
                        //  html = html.Replace("{{DSC_Section2}}", dscText);
                    }
                    else
                    {
                        html = html.Replace("{{GodownDSC}}", "");
                        html = html.Replace("<img id=\"GodownDSC\" src=\"images/dsc1.png\" style=\"height:30px;width:80px;\" /><br />", "");
                    }
                }
            }
            else
            {
                if (!Verify_DSC_Hash(mainRow)) // Simplified call to your verification logic
                {


                    //string dscText = "e-WHR Digitally Signed By: " +
                    // (!string.IsNullOrEmpty(mainRow["DSC_Holder_Name"].ToString()) ? mainRow["DSC_Holder_Name"].ToString() : "___________") +
                    //    "<br> Sign Date: " + (!string.IsNullOrEmpty(mainRow["WHR_Signing_Date"].ToString()) ? mainRow["WHR_Signing_Date"].ToString() : "___________") +
                    //    "<br> IP: " + (!string.IsNullOrEmpty(mainRow["Client_Ip"].ToString()) ? mainRow["Client_Ip"].ToString() : "___________");
                    //html = html.Replace("{{DSC_Section1}}", dscText);
                    //html = html.Replace("{{DSC_Section2}}", dscText);
                    string dscText = "e-WHR Digitally Signed By: " +
                         (!string.IsNullOrEmpty(Row0["DSC_Holder_Name"].ToString()) ? Row0["DSC_Holder_Name"].ToString() : "___________") +
                            "<br> Sign Date: " + (!string.IsNullOrEmpty(Row0["CreatedDate"].ToString()) ? Row0["CreatedDate"].ToString() : "___________") +
                            "<br> IP: " + (!string.IsNullOrEmpty(Row0["Client_Ip"].ToString()) ? Row0["Client_Ip"].ToString() : "___________");
                    html = html.Replace("{{BranchDSC}}", dscText);
                    // html = html.Replace("{{DSC_Section1}}", dscText);
                    html = html.Replace("{{BranchDSC}}", dscText);
                    //  html = html.Replace("{{DSC_Section2}}", dscText);
                    html = html.Replace("{{GodownDSC}}", "");
                    html = html.Replace("<img id=\"GodownDSC\" src=\"images/dsc1.png\" style=\"height:30px;width:80px;\" /><br />", "");

                }
            }

            if (string.IsNullOrEmpty(mainRow["DSC_Holder_Name"].ToString()) && string.IsNullOrEmpty(mainRow["WHR_Signing_Date"].ToString()) && string.IsNullOrEmpty(mainRow["Client_Ip"].ToString()))
            {
                imagePath = "";
            }
            else
            {
                imagePath = HttpContext.Current.Server.MapPath("~/images/dsc1.png");
            }
            // 5. QR Code Data (Logic from Source [cite: 58-59])
            qrData = "WHR No:" + mainRow["Depositor_WHR_Id"] + " Commodity:" + mainRow["Commodity_Name"] + " Qty:" + mainRow["Total_Qty_Received"] + "";
            html = html.Replace("{{QRData}}", qrData);
            html = html.Replace("{{QR}}", qrData);
        }

        return html;
    }

    private string GetDepositorForm_Detail(string whr)
    {
        string query = "";
        query = "select Acpt_FCIRO_No as Depositor_Form_No from tbl_Storage_Receipt_Details where WHR_Id='" + whr + "' and WHR_Flag='Y'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["Depositor_Form_No"].ToString();
        }
        return null;
    }

    public DataTable GetDeliveryHistory(string whrNo, decimal totalQtyReceived, int totalBagsReceived)
    {
        // SQL query to fetch delivery details using joins from the source
        string query = @" SELECT mg.Godown_Name, ms.Stack_Name,ISNULL(ssd.[No_Of_Bags], 0) AS No_Of_Bags, ISNULL(ssd.[Bags_Weight], 0) AS Bags_Weight,
                ISNULL(ssd.Loss, 0) AS Loss,
                    ISNULL(ssd.Gain, 0) AS Gain,
                    CONVERT(VARCHAR(10), sge.Issue_Date, 103) AS ID 
                FROM tbl_Delivery_Stacking_Details_GatePass AS ssd WITH (NOLOCK)
                INNER JOIN tbl_MetaData_GODOWN AS mg WITH (NOLOCK) 
                    ON ssd.Godown_ID = mg.Godown_ID 
                INNER JOIN tbl_MetaData_STACK AS ms WITH (NOLOCK) 
                    ON ssd.Stack_ID = ms.Stack_ID 
                INNER JOIN tbl_Storage_GatePass_Enrty AS sge WITH (NOLOCK) 
                    ON ssd.GatePass_No = sge.GatePass_No 
                WHERE ssd.Depositor_WHR_Id = @whrNo 
                ORDER BY sge.Issue_Date ASC";

        DataTable dtDelivery = new DataTable();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@whrNo", whrNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtDelivery);
            }
        }

        // Prepare the final table for the HTML listview
        DataTable dss = new DataTable();
        dss.Columns.Add("Date", typeof(string));
        dss.Columns.Add("DelQty", typeof(string));
        dss.Columns.Add("AvlQty", typeof(string));
        dss.Columns.Add("DelBags", typeof(string));
        dss.Columns.Add("AvlBags", typeof(string));
        dss.Columns.Add("ID", typeof(string));
        // dss.Columns.Add("Bags_Weight", typeof(string));
        decimal avilableQty = totalQtyReceived;
        int avilableBags = totalBagsReceived;

        // Iterate through records to calculate running balances
        foreach (DataRow row in dtDelivery.Rows)
        {
            // Formula: New Balance = Old Balance - Issued - Loss + Gain
            avilableQty = avilableQty - Convert.ToDecimal(row["Bags_Weight"]) - Convert.ToDecimal(row["Loss"]) + Convert.ToDecimal(row["Gain"]);
            avilableBags = avilableBags - Convert.ToInt32(row["No_Of_Bags"]);

            dss.Rows.Add(
                row["ID"].ToString(),
                row["Bags_Weight"].ToString(),
                avilableQty.ToString(),
                row["No_Of_Bags"].ToString(),
                avilableBags.ToString(),
                row["ID"].ToString()
            );
        }

        return dss;
    }
    public DataTable GetMainWHRData(string whrNo)
    {
        DataTable dtMain = new DataTable();

        // The query logic is derived from the whrdetail() method 
        // It joins WHR relations with Commodity, Category, Godown, and Depot metadata 
        string query = @"
        SELECT 
    WHR.Depositor_WHR_Id,
    WHR.Commodity_Id,
    WHR.Depositor_Name, 
    WHR.TotalBags_Received,
    WHR.Total_Qty_Received,
    WHR.MktValue_of_Commodity,
    WHR.AvgMoisture_Content,
    WHR.AvgMoisture_Content_To,
    WHR.SangrahadDate,
    WHR.CreatedDate AS WHR_CreatedDate,
    WHR.Client_IP AS WHR_Client_IP,
    CAST(WHR.MktValue_of_Commodity AS DECIMAL(18,2)) AS MktValue_of_Commodityno,
    COMM.Commodity_Name, 
    CAT.Category_Name,
    DEP.DepotName, 
    DEP.DepotAddress,
    DEP.NodalOfficeName,
    GOD.Godown_Name,
    GOD.Hired_Type,
    GOD.LicNum AS LicenseNo,
    CONVERT(VARCHAR(10), WHR.Date_of_Deposit, 103) AS Date_of_Deposit,
    CONVERT(VARCHAR(10), GOD.LicDate, 103) AS LicenseDate,
    CONVERT(VARCHAR(10), WHR.WHR_Issue_Date, 103) AS WHR_Issue_Date,
    CONVERT(VARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,
    ('Moisture % ' + CAST((WHR.AvgMoisture_Content + WHR.AvgMoisture_Content_To)/2 AS VARCHAR(100)) + ' ' + ISNULL(WHR.Remark, '')) AS Remark,
    WHR.CropYear, WHR.BranchID, WHR.DepositorID, WHR.GodownID, WHR.Category_Id, WHR.District_Id,WHR.AvgMoisture_Content_To,WHR.SangrahadDate
FROM tbl_storage_Depositor_WHR_Relation AS WHR WITH (NOLOCK)
INNER JOIN tbl_MetaData_STORAGE_CATEGORY AS CAT WITH (NOLOCK) ON WHR.Category_Id = CAT.Category_Id
INNER JOIN tbl_MetaData_STORAGE_COMMODITY AS COMM WITH (NOLOCK) ON WHR.Commodity_Id = COMM.Commodity_Id
INNER JOIN tbl_MetaData_DEPOT AS DEP WITH (NOLOCK) ON WHR.BranchID = DEP.BranchID
INNER JOIN tbl_MetaData_GODOWN_2018 AS GOD WITH (NOLOCK) ON WHR.GodownID = GOD.Godown_ID
WHERE WHR.Depositor_WHR_Id = @whrNo --AND WHR.BranchID = @branchId";

        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Parameters are used to prevent SQL injection, using Session data as seen in the source  [cite: 52-401]
                cmd.Parameters.AddWithValue("@whrNo", whrNo);
                // cmd.Parameters.AddWithValue("@branchId", Session["BranchId"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtMain);
            }
        }

        return dtMain;
    }
    //public void Verify_eWHR()
    //{
    //    //string query
    //    string Commodity_Id_2 = ddlCommodity.SelectedValue;
    //    string Enter_WHR = "";
    //    if (DDLwhr.SelectedItem.Text != "--Select--")
    //    {
    //        Enter_WHR = DDLwhr.SelectedItem.Text;
    //    }
    //    else if (TextBox1.Text.ToString() != "")
    //    {
    //        Enter_WHR = TextBox1.Text.ToString();
    //    }
    //    string Is_DSC_Verified = "";
    //    string Depositor_WHR_Id = "";
    //    string Commodity_Id = "";
    //    string Depositor_Name = "";
    //    string Date_of_Deposit = "";
    //    string TotalBags_Received = "";
    //    string Total_Qty_Received = "";
    //    string AvgMoisture_Content = "";
    //    string MktValue_of_Commodity = "";
    //    string WHR_Issue_Date = "";
    //    string WHR_CreatedDate = "";
    //    string WHR_Client_IP = "";
    //    string CropYear = "";
    //    string Remark = "";
    //    string BranchID = "";
    //    string DepositorID = "";
    //    string GodownID = "";
    //    string Depositor_Form_No = "";
    //    string Grade = "";
    //    string CheckSum = "";
    //    string District_Id = "";
    //    string WHR_Signing_Date = "";
    //    string DSC_Holder_Name = "";
    //    string DSC_Serial_No = "";
    //    string DSC_Signed_Ip = "";
    //    string AvgMoisture_Content_To = "";
    //    string SangrahadDate = "";
    //    string LocalIp = "";
    //    string query1 = "";
    //    if (Commodity_Id_2 == "63" || Commodity_Id_2 == "64" || Commodity_Id_2 == "33")
    //    {
    //        //query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2019 as DSWHR inner join tbl_Digital_Signature_Kharif2019 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2019 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
    //        query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_CMS2020 as DSWHR inner join tbl_Digital_Signature_CMS2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_CMS2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
    //    }
    //    else if (Commodity_Id_2 == "22")
    //    {
    //        query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Wheat2020 as DSWHR inner join tbl_Digital_Signature_Wheat2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Wheat2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
    //    }
    //    else if (Commodity_Id_2 == "13" || Commodity_Id_2 == "8" || Commodity_Id_2 == "11")
    //    {
    //        query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2020 as DSWHR inner join tbl_Digital_Signature_Kharif2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
    //    }
    //    SqlCommand cmd = new SqlCommand(query1, Con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet XMLds = new DataSet();
    //    da.Fill(XMLds);
    //    if (XMLds.Tables[0].Rows.Count > 0)
    //    {
    //        int WS = 0;
    //        //string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
    //        Depositor_WHR_Id = XMLds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
    //        Commodity_Id = XMLds.Tables[0].Rows[0]["Commodity_Id"].ToString();
    //        Depositor_Name = XMLds.Tables[0].Rows[0]["Depositor_Name"].ToString();
    //        Date_of_Deposit = XMLds.Tables[0].Rows[0]["Date_of_Deposit"].ToString();
    //        TotalBags_Received = XMLds.Tables[0].Rows[0]["TotalBags_Received"].ToString();
    //        Total_Qty_Received = XMLds.Tables[0].Rows[0]["Total_Qty_Received"].ToString();
    //        AvgMoisture_Content = XMLds.Tables[0].Rows[0]["AvgMoisture_Content"].ToString();
    //        MktValue_of_Commodity = XMLds.Tables[0].Rows[0]["MktValue_of_Commodity"].ToString();
    //        WHR_Issue_Date = XMLds.Tables[0].Rows[0]["WHR_Issue_Date"].ToString();
    //        WHR_CreatedDate = XMLds.Tables[0].Rows[0]["WHR_CreatedDate"].ToString();
    //        WHR_Client_IP = XMLds.Tables[0].Rows[0]["WHR_Client_IP"].ToString();
    //        CropYear = XMLds.Tables[0].Rows[0]["CropYear"].ToString();
    //        Remark = XMLds.Tables[0].Rows[0]["Remark"].ToString();
    //        BranchID = XMLds.Tables[0].Rows[0]["BranchID"].ToString();
    //        DepositorID = XMLds.Tables[0].Rows[0]["DepositorID"].ToString();
    //        GodownID = XMLds.Tables[0].Rows[0]["GodownID"].ToString();
    //        Depositor_Form_No = XMLds.Tables[0].Rows[0]["Depositor_Form_No"].ToString();
    //        Grade = XMLds.Tables[0].Rows[0]["Category_Id"].ToString();
    //        District_Id = XMLds.Tables[0].Rows[0]["District_Id"].ToString();
    //        CheckSum = XMLds.Tables[0].Rows[0]["WHR_Check_Sum"].ToString();
    //        DateTime Created_Date = Convert.ToDateTime(XMLds.Tables[0].Rows[0]["CreatedDate"]);
    //        WHR_Signing_Date = Created_Date.ToString("dd/MM/yyyy HH:mm:ss");
    //        DSC_Holder_Name = XMLds.Tables[0].Rows[0]["DSC_Holder_Name"].ToString();
    //        DSC_Serial_No = XMLds.Tables[0].Rows[0]["DSC_Serial_No"].ToString();
    //        DSC_Signed_Ip = XMLds.Tables[0].Rows[0]["CreatedBy"].ToString();
    //        AvgMoisture_Content_To = XMLds.Tables[0].Rows[0]["AvgMoisture_Content_To"].ToString();
    //        SangrahadDate = XMLds.Tables[0].Rows[0]["SangrahadDate"].ToString();
    //        LocalIp = XMLds.Tables[0].Rows[0]["Client_Ip"].ToString();
    //        //Generate CSum
    //        string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + DSC_Serial_No + LocalIp + DSC_Holder_Name;
    //        var sha1 = System.Security.Cryptography.SHA1.Create();
    //        byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
    //        byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
    //        //var hashstr  = Convert.ToBase64String(hash);
    //        var hashstrs = System.BitConverter.ToString(hash).Replace("-", "");
    //        //Generate CSum
    //        if (hashstrs == CheckSum)
    //        {
    //            if (LocalIp == null || LocalIp == "")
    //            {
    //                LocalIp = DSC_Signed_Ip;
    //            }
    //            Is_DSC_Verified = "Y";
    //            lbldscT.Text = "e-WHR Digitally Signed By";
    //            lblDSC_Holder.Text = DSC_Holder_Name.ToString();
    //            //lblSerNo.Text = "Serial Number : " + DSC_Serial_No.ToString();
    //            lblSigningDate.Text = "Sign Date : " + WHR_Signing_Date.ToString();
    //            lblIpAdd.Text = "Signed IP Address : " + LocalIp.ToString();

    //            lbldscT2.Text = "e-WHR Digitally Signed By";
    //            lblDSC_Holder2.Text = DSC_Holder_Name.ToString();
    //            //lblSerNo2.Text = "Serial Number : " + DSC_Serial_No.ToString();
    //            lblSigningDate2.Text = "Sign Date : " + WHR_Signing_Date.ToString();
    //            lblIpAdd2.Text = "Signed IP Address : " + LocalIp.ToString();
    //            Image5.Visible = true;
    //            Image6.Visible = true;

    //        }
    //        else
    //        {
    //            Is_DSC_Verified = "N";
    //            lbldscT.Text = "";
    //            lblDSC_Holder.Text = "";
    //            //lblSerNo.Text = "";
    //            lblSigningDate.Text = "";
    //            lblIpAdd.Text = "";

    //            lbldscT2.Text = "";
    //            lblDSC_Holder2.Text = "";
    //            //lblSerNo2.Text = "";
    //            lblSigningDate2.Text = "";
    //            lblIpAdd2.Text = "";
    //            Image5.Visible = false;
    //            Image6.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        lbldscT.Text = "";
    //        lblDSC_Holder.Text = "";
    //        //lblSerNo.Text = "";
    //        lblSigningDate.Text = "";
    //        lblIpAdd.Text = "";

    //        lbldscT2.Text = "";
    //        lblDSC_Holder2.Text = "";
    //        //lblSerNo2.Text = "";
    //        lblSigningDate2.Text = "";
    //        lblIpAdd2.Text = "";
    //        Image5.Visible = false;
    //        Image6.Visible = false;
    //    }
    //}
    public bool Verify_DSC_Hash(DataRow r)
    {
        try
        {
            string Is_DSC_Verified = "";
            // 1. Collect all data fields used in the original hash string 
            string Depositor_WHR_Id = r["Depositor_WHR_Id"].ToString();
            string Commodity_Id = r["Commodity_Id"].ToString();
            string Depositor_Name = r["Depositor_Name"].ToString();
            string Date_of_Deposit = r["Date_of_Deposit"].ToString();
            string TotalBags_Received = r["TotalBags_Received"].ToString();
            string Total_Qty_Received = r["Total_Qty_Received"].ToString();
            string AvgMoisture_Content = r["AvgMoisture_Content"].ToString();
            string MktValue_of_Commodity = r["MktValue_of_Commodity"].ToString();
            string WHR_Issue_Date = r["WHR_Issue_Date"].ToString();
            string WHR_CreatedDate = r["WHR_CreatedDate"].ToString();
            string WHR_Client_IP = r["WHR_Client_IP"].ToString();
            string CropYear = r["CropYear"].ToString();
            string Remark = r["Remark"].ToString();
            string BranchID = r["BranchID"].ToString();
            string DepositorID = r["DepositorID"].ToString();
            string GodownID = r["GodownID"].ToString();
            string Depositor_Form_No = GetDepositorForm_Detail(r["Depositor_WHR_Id"].ToString());
            string Grade = r["Category_Id"].ToString();
            string District_Id = r["District_Id"].ToString();
            string SangrahadDate = "";
            string DSC_Serial_No = "";
            string LocalIp = "";
            string DSC_Holder_Name = "";
            string StoredCheckSum = "";
            string AvgMoisture_Content_To = "";
            string DSC_Signed_Ip = "";
            //string SangrahadDate = r["SangrahadDate"].ToString();
            //string DSC_Serial_No = r["DSC_Serial_No"].ToString();
            //string LocalIp = r["Client_Ip"].ToString();
            //string DSC_Holder_Name = r["DSC_Holder_Name"].ToString();

            string query1 = "";
            if (Commodity_Id == "63" || Commodity_Id == "64" || Commodity_Id == "33" || Commodity_Id == "75")
            {
                //query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2019 as DSWHR inner join tbl_Digital_Signature_Kharif2019 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2019 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + Enter_WHR + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
                //query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_CMS2020 as DSWHR inner join tbl_Digital_Signature_CMS2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_CMS2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + r["Depositor_WHR_Id"] + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
                query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_CMS2026 as DSWHR inner join tbl_Digital_Signature_CMS2026 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_CMS2026 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + r["Depositor_WHR_Id"] + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";

            }
            else if (Commodity_Id == "22")
            {
                query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Wheat2020 as DSWHR inner join tbl_Digital_Signature_Wheat2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Wheat2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + r["Depositor_WHR_Id"] + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
            }
            else if (Commodity_Id == "13" || Commodity_Id == "8" || Commodity_Id == "11")
            {
                query1 = "SELECT [Depositor_WHR_Id],[Depositor_Form_No],[District_Id],[Commodity_Id],[Category_Id],[Depositor_Name],CONVERT(varchar(10),[Date_of_Deposit],103) as Date_of_Deposit,[TotalBags_Received],[Total_Qty_Received],[AvgMoisture_Content],AvgMoisture_Content_To,SangrahadDate,[MktValue_of_Commodity],CONVERT(varchar(10),[WHR_Issue_Date],103) as WHR_Issue_Date,[WHR_CreatedDate],[WHR_Client_IP],[CropYear],[Remark],[BranchID],[DepositorID],[GodownID],[WHR_Check_Sum],DSWHR.[CreatedDate],DSWHR.[CreatedBy],DSWHR.DSC_Serial_No,DSWHR.DSC_Holder_Name,DSWHR.Client_Ip from tbl_Digitally_Signed_WHR_Kharif2020 as DSWHR inner join tbl_Digital_Signature_Kharif2020 as DSSign on DSSign.WHR_No=DSWHR.Depositor_WHR_Id inner join tbl_DSC_WHR_XML_File_Kharif2020 as XWHR on XWHR.WHR_Id=DSWHR.Depositor_WHR_Id where DSWHR.Depositor_WHR_Id='" + r["Depositor_WHR_Id"] + "' and DSWHR.DSC_User_Type!='G' and DSWHR.DSC_Serial_No=DSSign.DSC_Serial_No";
            }

            SqlCommand cmd = new SqlCommand(query1, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet XMLds = new DataSet();
            da.Fill(XMLds);
            if (XMLds.Tables[0].Rows.Count > 0)
            {
                DateTime Created_Date = Convert.ToDateTime(XMLds.Tables[0].Rows[0]["CreatedDate"]);
                r["WHR_Signing_Date"] = Created_Date.ToString("dd/MM/yyyy HH:mm:ss");
                DSC_Signed_Ip = XMLds.Tables[0].Rows[0]["CreatedBy"].ToString();
                SangrahadDate = XMLds.Tables[0].Rows[0]["SangrahadDate"].ToString();
                DSC_Serial_No = XMLds.Tables[0].Rows[0]["DSC_Serial_No"].ToString();
                r["Client_Ip"] = XMLds.Tables[0].Rows[0]["Client_Ip"].ToString();
                r["DSC_Holder_Name"] = XMLds.Tables[0].Rows[0]["DSC_Holder_Name"].ToString();
                StoredCheckSum = XMLds.Tables[0].Rows[0]["WHR_Check_Sum"].ToString();
                AvgMoisture_Content_To = XMLds.Tables[0].Rows[0]["AvgMoisture_Content_To"].ToString();
            }


            string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name + Date_of_Deposit + TotalBags_Received + Total_Qty_Received + AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date + WHR_CreatedDate + WHR_Client_IP + CropYear + Remark + BranchID + DepositorID + GodownID + Depositor_Form_No + Grade + District_Id + AvgMoisture_Content_To + SangrahadDate + DSC_Serial_No + LocalIp + DSC_Holder_Name;
            var sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
            byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
            //var hashstr  = Convert.ToBase64String(hash);
            string hashstrs = System.BitConverter.ToString(hash).Replace("-", "");

            if (hashstrs == CheckSumString)
            {
                if (LocalIp == null || LocalIp == "")
                {
                    LocalIp = DSC_Signed_Ip;
                    Is_DSC_Verified = "Y";
                    //lbldscT.Text = "e-WHR Digitally Signed By";
                    //lblDSC_Holder.Text = DSC_Holder_Name.ToString();
                    ////lblSerNo.Text = "Serial Number : " + DSC_Serial_No.ToString();
                    //lblSigningDate.Text = "Sign Date : " + WHR_Signing_Date.ToString();
                    //lblIpAdd.Text = "Signed IP Address : " + LocalIp.ToString();

                }
            }
            //// 2. Concatenate the strings in the exact order required by the system 
            //string CheckSumString = Depositor_WHR_Id + Commodity_Id + Depositor_Name +
            //                        Date_of_Deposit + TotalBags_Received + Total_Qty_Received +
            //                        AvgMoisture_Content + MktValue_of_Commodity + WHR_Issue_Date +
            //                        WHR_CreatedDate + WHR_Client_IP + CropYear + Remark +
            //                        BranchID + DepositorID + GodownID + Depositor_Form_No +
            //                        Grade + District_Id + AvgMoisture_Content_To +
            //                        SangrahadDate + DSC_Serial_No + LocalIp + DSC_Holder_Name;
            //Is_DSC_Verified = "Y";
            //// 3. Generate SHA1 Hash 
            //using (var sha1 = System.Security.Cryptography.SHA1.Create())
            //{
            //    byte[] buf = System.Text.Encoding.UTF8.GetBytes(CheckSumString);
            //    byte[] hash = sha1.ComputeHash(buf, 0, buf.Length);
            //    string calculatedHash = System.BitConverter.ToString(hash).Replace("-", "");

            //    // 4. Compare calculated hash with the one stored in the database 
            //    return calculatedHash == StoredCheckSum;
            //}
            return hashstrs == CheckSumString;

        }
        catch (Exception ex)
        {
            return false;
        }
    }


    ///Get teh dispatch data from NeML
    ///
    // Static client to prevent socket exhaustion
    private static readonly HttpClient client = new HttpClient();

    [WebMethod]
    public string GetTransactionData(string fromdate, string todate)   //(string username, string password, string fromdate, string todate)
    {
        WarehouseApiSecurity.RequireApiKey();
        try
        {
            // 1. Force TLS 1.2 (Required for many modern APIs)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // 2. Perform Login to get the Token
            string token = LoginAndGetToken();  //(username, password);
            if (token.StartsWith("Error")) return token;

            // 3. Fetch Dispatch Details using the Token
            return FetchDispatchDetails(token, fromdate, todate);
        }
        catch (ConfigurationErrorsException)
        {
            return "Error: NCCF NeML API credentials are not configured.";
        }
        catch (Exception)
        {
            return "Error fetching NeML transactions.";
        }
    }

    private string LoginAndGetToken() //(string user, string pass)
    {
        var loginUrl = "https://nccf-enwhr.neml.in/api/v2/enwhr/auth/login";
        var loginData = new
        {
            username = WarehouseApiSecurity.GetRequiredSetting("NCCFNeMLApiUsername"),
            password = WarehouseApiSecurity.GetRequiredSetting("NCCFNeMLApiPassword")
        };
        var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

        var response = client.PostAsync(loginUrl, content).GetAwaiter().GetResult();

        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            // Assuming the API returns a JSON object like { "token": "..." }
            dynamic result = JsonConvert.DeserializeObject(jsonResponse);
            string token = result.jwtToken;
            return token;
        }

        return "Error during Login: " + response.StatusCode;
    }

    private string FetchDispatchDetails(string token, string fromdate, string todate)
    {
        var url = "https://nccf-enwhr.neml.in/api/v2/enwhr/transactions/mpwlc/getDispatchDetailsByDate";

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", "Bearer " + token);

        // Optional: Only add Cookie if the API requires stateful sessions
        // request.Headers.Add("Cookie", "JSESSIONID=..."); 

        var payload = new
        {
            stateCode = "23",
            fromDate = fromdate,
            toDate = todate
        };

        request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        var response = client.SendAsync(request).GetAwaiter().GetResult();
        string strJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return ProcessAndInsert(strJson);
    }

    public string ProcessAndInsert(string fullJsonResponse)
    {
        // Parse the string into a JObject
        var jo = JObject.Parse(fullJsonResponse);

        // Select the "data" token and convert back to string
        string jsonStringForDb = jo["data"].ToString();

        // Call your existing ADO.NET method
        return InsertJsonViaAdoNet(jsonStringForDb);
    }
    public string InsertJsonViaAdoNet(string jsonResponse)
    {
        // 1. Parse JSON into a List of objects
        List<DispatchRecord> records = JsonConvert.DeserializeObject<List<DispatchRecord>>(jsonResponse);

        int rowsInserted = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            foreach (var item in records)
            {
                string sql = @"IF NOT EXISTS (SELECT 1 FROM Recieved_DispatchId_From_NAFED_ANB WHERE DispatchId = @p6)
                            BEGIN
                                INSERT INTO Recieved_DispatchId_From_NAFED_ANB (
                                    StateName, Commodity, Season, DispatchCreatedDate, VehicleNo, 
                                    DispatchId, DispatchQuantity, DispatchBags, CenterName, Pacs, GodownName,GoodwnID,Moisture,BagType
                                ) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11,@p12, @p13, @p14)
                            END";
                //    string sql = @"INSERT INTO Recieved_DispatchId_From_NAFED_ANB(
                //    StateName, Commodity, Season, DispatchCreatedDate, VehicleNo, 
                //    DispatchId, DispatchQuantity, DispatchBags, CenterName, Pacs, GodownName
                //) VALUES (
                //    @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11
                //)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", item.stateName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p2", item.commodity ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p3", item.season ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p4", item.dispatchCreatedDate);
                    cmd.Parameters.AddWithValue("@p5", item.vehicleNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p6", item.dispatchId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p7", item.dispatchQuantity);
                    cmd.Parameters.AddWithValue("@p8", item.dispatchBags);
                    cmd.Parameters.AddWithValue("@p9", item.centerName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p10", item.pacs ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p11", item.godownName ?? (object)DBNull.Value);
                    // New parameters from JSON
                    cmd.Parameters.AddWithValue("@p12", item.godownCode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@p13", string.IsNullOrEmpty(item.moisture) ? (object)DBNull.Value : item.moisture);
                    cmd.Parameters.AddWithValue("@p14", item.packageType ?? (object)DBNull.Value);
                    int rowsIns = cmd.ExecuteNonQuery();
                    if (rowsIns > 0)
                    {
                        rowsInserted += rowsIns;
                        rowsIns = 0;
                    }
                }
            }
        }
        return rowsInserted.ToString() + " Rows Inserted.";
    }
    public class DispatchRecord
    {
        public string stateName { get; set; }
        public string commodity { get; set; }
        public string season { get; set; }
        public string dispatchCreatedDate { get; set; }
        public string vehicleNo { get; set; }
        public string dispatchId { get; set; }
        public decimal dispatchQuantity { get; set; }
        public int dispatchBags { get; set; }
        public string centerName { get; set; }
        public string pacs { get; set; }
        public string godownName { get; set; }
        public string godownCode { get; set; }
        public string moisture { get; set; }
        public string packageType { get; set; }

    }

}
