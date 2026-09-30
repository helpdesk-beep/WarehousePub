using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_ReportDeliveryForm : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindBranches();
    }

    private void BindBranches()
    {
        // Requirement 1: Bind Branches from tbl_MetaData_DEPOT
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "SELECT BranchId, DepotName FROM tbl_MetaData_DEPOT order by DepotName Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            ddlBranch.DataSource = cmd.ExecuteReader();
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Branch--", "0"));
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Requirement 2: Bind Godown based on selection
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "SELECT Godown_ID,Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchID = @BranchId order by Godown_Name Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue);
            con.Open();
            ddlGodown.DataSource = cmd.ExecuteReader();
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Godown--", "0"));
        }
    }


    /// <summary>

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    // 1. Connection String (Ensure this is in your web.config)
    //    string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    string deliveryOrderNo = ddlDeliveryOrderNo.SelectedItem.Text.Trim(); 

    //    DataSet ds = new DataSet();

    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        // 2. Define both queries (Query 1 and Query 2)
    //        string query1 = @"select distinct [Depositor/Issuer_Name],md.DepotName,mdis.District_Name,gp.Issue_Source_ID as StockDeliveryOrder_Id,sdo.Delivery_Order_No as Delivery_Order_No,
    //                    convert(varchar(15),sdo.Delivery_Order_Date,103)Delivery_Order_Date,com.Commodity_Name,gp.NO_of_Bage as Qty_Issued_No_Bags_Sound,sdo.deliveredName,
    //                    dbo.CONVERT_NO_TEXT(gp.NO_of_Bage) as 'No_of_bags_in_words',convert(decimal(18,2),gp.Weight) as 'Qty_Issued_Weight',
    //                    dbo.CONVERT_NO_TEXT(gp.Weight) as 'Weight_in_words',sum(DG.Moisture_Content)/count(DG.Moisture_Content) as 'Moisture_Content',
    //                    convert(decimal(18,2),wh.MktValue_of_Commodity) as 'MktValue_of_Commodity',(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=gp.Godown_ID) As godown,gp.Godown_ID
    //                    from tbl_Storage_GatePass_Enrty gp 
    //                    INNER JOIN tbl_Storage_Final_Stock_Delivery_GatePass DG ON gp.GatePass_no=DG.GatePass_no
    //                    inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no 
    //                    join [tbl_storage_Depositor_WHR_Relation] as wh on sgp.[Depositor_WHR_Id]=wh.[Depositor_WHR_Id] 
    //                    join tbl_MetaData_DEPOT as md on gp.BranchID=md.BranchID 
    //                    join tbl_MetaData_DISTRICT as mdis on gp.District_ID=mdis.District_Id 
    //                    join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id 
    //                    join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID=sdo.Delivery_Order_No 
    //                    where gp.Issue_Source='DO' and gp.Status!='Cancel' and gp.Issue_Source_ID!='0' and sdo.Delivery_Order_No=@DONo 
    //                    group by [Depositor/Issuer_Name],gp.Issue_Source_ID,com.Commodity_Name,sdo.deliveredName,mdis.District_Name,md.DepotName,gp.NO_of_Bage,gp.Weight,wh.MktValue_of_Commodity,gp.Godown_ID, sdo.Delivery_Order_No, sdo.Delivery_Order_Date,DG.GatePass_No
    //                    order by Delivery_Order_Date Asc;";

    //        string query2 = @"select sgp.Depositor_WHR_Id, sum(sgp.No_Of_Bags) as Bags, sum(sgp.Bags_Weight) as Wght, (sum(sgp.Bags_Weight)+sum(sgp.Gain)-sum(sgp.Loss)) as DeliveredQty, sdo.Delivery_Order_No
    //                    from tbl_Storage_GatePass_Enrty gp 
    //                    inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no
    //                    join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID=sdo.Delivery_Order_No 
    //                    where gp.Issue_Source='DO' and gp.Status!='Cancel' and gp.Issue_Source_ID!='0' and sdo.Delivery_Order_No=@DONo 
    //                    group by sgp.Depositor_WHR_Id,sdo.Delivery_Order_No";

    //        SqlCommand cmd = new SqlCommand(query1 + query2, conn);
    //        cmd.Parameters.AddWithValue("@DONo", deliveryOrderNo);
    //        cmd.CommandTimeout = 3600;
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(ds);
    //    }

    //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        DataRow dr = ds.Tables[0].Rows[0];
    //        DataRow dr1 = ds.Tables[1].Rows[0];
    //        // 3. Read the HTML Template
    //        string filePath = Server.MapPath("~/GatePassHtmlPage.html");
    //        string htmlContent = File.ReadAllText(filePath);

    //        // 4. Map SQL Columns to HTML Placeholders
    //        htmlContent = htmlContent.Replace("{{RecieptNumber}}", dr["Delivery_Order_No"].ToString());
    //        htmlContent = htmlContent.Replace("{{Moisture_Content}}", dr["Moisture_Content"].ToString());
    //        htmlContent = htmlContent.Replace("{{Commodity_Name}}", dr["Commodity_Name"].ToString());
    //        htmlContent = htmlContent.Replace("{{Qty_Issued_No_Bags_Sound}}", dr["Qty_Issued_No_Bags_Sound"].ToString());
    //        htmlContent = htmlContent.Replace("{{Qty_Issued_Weight}}", dr["Qty_Issued_Weight"].ToString());
    //        htmlContent = htmlContent.Replace("{{Depositor}}", dr["Depositor/Issuer_Name"].ToString());
    //        htmlContent = htmlContent.Replace("{{Delivery_Order_Date}}", dr["Delivery_Order_Date"].ToString());
    //        htmlContent = htmlContent.Replace("{{District_Name}}", dr["District_Name"].ToString());
    //        htmlContent = htmlContent.Replace("{{DepotName}}", dr["DepotName"].ToString());
    //        htmlContent = htmlContent.Replace("{{Godown}}", dr["godown"].ToString());
    //        htmlContent = htmlContent.Replace("{{Godown_ID}}", dr["Godown_ID"].ToString());
    //        htmlContent = htmlContent.Replace("{{MktValue_of_Commodity}}", dr["MktValue_of_Commodity"].ToString());
    //        htmlContent = htmlContent.Replace("{{SubmitedFormNumber}}", dr1["Depositor_WHR_Id"].ToString());
    //        htmlContent = htmlContent.Replace("{{CurrentDateTime}}", DateTime.Now.ToString("dd/MM/yyyy"));
    //        htmlContent = htmlContent.Replace("{{Delivery_Order_No}}", dr["Delivery_Order_No"].ToString());


    //        // Handling Table/Repeated Data from Query 2 (if you have a table in HTML)
    //        if (ds.Tables[1].Rows.Count > 0)
    //        {
    //            // If you need to sum values from the second query to display total
    //            decimal totalDelivered = Convert.ToDecimal(ds.Tables[1].Compute("Sum(DeliveredQty)", ""));
    //            htmlContent = htmlContent.Replace("{{TotalDelivered}}", totalDelivered.ToString("N2"));
    //        }

    //        // 5. Convert to PDF using SelectPdf
    //        SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
    //        SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlContent);

    //        // 6. Send to browser
    //        Response.ContentType = "application/pdf";
    //        Response.AddHeader("content-disposition", "attachment;filename=GatePass_" + deliveryOrderNo + ".pdf");
    //        doc.Save(Response.OutputStream);
    //        doc.Close();
    //        Response.End();
    //    }
    //}
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //    // Show export option after submission
    //    btnExport.Visible = true;
    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Report Generated Successfully');", true);
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string deliveryOrderNo = ddlDeliveryOrderNo.SelectedItem.Text.Trim();
        DataSet ds = new DataSet();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            // Queries provided in source
            string query1 = @"select distinct [Depositor/Issuer_Name],md.DepotName,mdis.District_Name,gp.Issue_Source_ID as StockDeliveryOrder_Id,sdo.Delivery_Order_No as Delivery_Order_No,
                convert(varchar(15),sdo.Delivery_Order_Date,103) Delivery_Order_Date,com.Commodity_Name,gp.NO_of_Bage as Qty_Issued_No_Bags_Sound,sdo.deliveredName,
                dbo.CONVERT_NO_TEXT(gp.NO_of_Bage) as 'No_of_bags_in_words',convert(decimal(18,2),gp.Weight) as 'Qty_Issued_Weight',
                dbo.CONVERT_NO_TEXT(gp.Weight) as 'Weight_in_words',sum(DG.Moisture_Content)/count(DG.Moisture_Content) as 'Moisture_Content',
                convert(decimal(18,2),wh.MktValue_of_Commodity) as 'MktValue_of_Commodity',(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=gp.Godown_ID) As godown,gp.Godown_ID ,sgp.Stack_ID
                from tbl_Storage_GatePass_Enrty gp INNER JOIN tbl_Storage_Final_Stock_Delivery_GatePass DG ON gp.GatePass_no=DG.GatePass_no
                inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no 
                join [tbl_storage_Depositor_WHR_Relation] as wh on sgp.[Depositor_WHR_Id]=wh.[Depositor_WHR_Id] 
                join tbl_MetaData_DEPOT as md on gp.BranchID=md.BranchID 
                join tbl_MetaData_DISTRICT as mdis on gp.District_ID=mdis.District_Id 
                join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id 
                join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID=sdo.Delivery_Order_No 
                where gp.Issue_Source='DO' and gp.Status!='Cancel' and gp.Issue_Source_ID!='0' and sdo.Delivery_Order_No=@DONo 
                group by [Depositor/Issuer_Name],gp.Issue_Source_ID,com.Commodity_Name,sdo.deliveredName,mdis.District_Name,
                md.DepotName,gp.NO_of_Bage,gp.Weight,wh.MktValue_of_Commodity,gp.Godown_ID,
                sdo.Delivery_Order_No,sdo.Delivery_Order_Date,DG.GatePass_No,sgp.Stack_ID order by Delivery_Order_Date Asc; ";

            string query2 = @"select sgp.Depositor_WHR_Id, sum(sgp.No_Of_Bags) as Bags, sum(sgp.Bags_Weight) as Wght, 
                        (sum(sgp.Bags_Weight)+sum(sgp.Gain)-sum(sgp.Loss)) as DeliveredQty,Convert(varchar(10),gp.Issue_Date,103) as WHR_Issue_date
                        from tbl_Storage_GatePass_Enrty gp 
                        inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no
                        where gp.Issue_Source_ID=@DONo 
                        group by sgp.Depositor_WHR_Id,gp.Issue_Date";

            SqlCommand cmd = new SqlCommand(query1 + query2, conn);
	    cmd.CommandTimeout = 3600;
            cmd.Parameters.AddWithValue("@DONo", deliveryOrderNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // Access the main data row for header and commodity details 
            DataRow dr = ds.Tables[0].Rows[0];
            DataRow dr1 = ds.Tables[1].Rows[0];

            // 1. Load the HTML Template from the server
            string filePath = Server.MapPath("~/ReportDeliveryForm.html");
            string htmlContent = File.ReadAllText(filePath);

            // 2. Replace Header and General Variables 
            htmlContent = htmlContent.Replace("{{MPWLC_Hindi_Name}}", "मध्यप्रदेश वेयरहाउसिंग एवं लॉजिस्टिक्स कार्पोरेशन");
            htmlContent = htmlContent.Replace("{{OrderNo}}", dr["Delivery_Order_No"].ToString());
            htmlContent = htmlContent.Replace("{{OrderDate}}", dr["Delivery_Order_Date"].ToString());
            htmlContent = htmlContent.Replace("{{BranchName}}", dr["DepotName"].ToString());
            htmlContent = htmlContent.Replace("{{GodownStakeNo}}", dr["godown"].ToString());
            htmlContent = htmlContent.Replace("{{District}}", dr["District_Name"].ToString());

            // 3. Replace Recipient and Depositor Variables 
            htmlContent = htmlContent.Replace("{{RecipientName}}", "MPSCSC");
            htmlContent = htmlContent.Replace("{{City}}", dr["District_Name"].ToString());
            htmlContent = htmlContent.Replace("{{DepositorName}}", dr["Depositor/Issuer_Name"].ToString());
            htmlContent = htmlContent.Replace("{{Depositor_WHR_Id}}", dr1["Depositor_WHR_Id"].ToString());
            htmlContent = htmlContent.Replace("{{WHR_Issue_date}}", dr1["WHR_Issue_date"].ToString());
            htmlContent = htmlContent.Replace("{{Delivery_Order_Date}}", dr["Delivery_Order_Date"].ToString());

            // 4. Replace Commodity Table Variables 
            htmlContent = htmlContent.Replace("{{CommodityName}}", dr["Commodity_Name"].ToString());
            htmlContent = htmlContent.Replace("{{QtyInFigures}}", dr["Qty_Issued_No_Bags_Sound"].ToString());
            htmlContent = htmlContent.Replace("{{QtyInWords}}", dr["No_of_bags_in_words"].ToString());
            htmlContent = htmlContent.Replace("{{WeightInFigures}}", dr["Qty_Issued_Weight"].ToString());
            htmlContent = htmlContent.Replace("{{WeightInWords}}", dr["Weight_in_words"].ToString());
            htmlContent = htmlContent.Replace("{{MarketTotalValue}}", dr["MktValue_of_Commodity"].ToString());
            htmlContent = htmlContent.Replace("{{MoisturePercentage}}", dr["Moisture_Content"].ToString());

            // 5. Generate Dynamic Rows for Office Use Table (Query 2) 
            System.Text.StringBuilder sbRows = new System.Text.StringBuilder();
            int serialNo = 1;
            decimal totalBags = 0;
            decimal totalWeight = 0;

            foreach (DataRow row in ds.Tables[1].Rows)
            {
                sbRows.Append("<tr>");
                sbRows.Append("<td>" + (serialNo++) + "</td>");
                sbRows.Append("<td>" + row["Depositor_WHR_Id"] + "</td>");
                sbRows.Append("<td>" + row["Bags"] + "</td>");
                sbRows.Append("<td>" + Convert.ToDecimal(row["Wght"]).ToString("F5") + "</td>");
                sbRows.Append("<td>" + Convert.ToDecimal(row["DeliveredQty"]).ToString("F5") + "</td>");
                sbRows.Append("</tr>");

                totalBags += Convert.ToDecimal(row["Bags"]);
                totalWeight += Convert.ToDecimal(row["Wght"]);
            }

            // Replace Table Content and Totals 
            htmlContent = htmlContent.Replace("{{DynamicReceiptRows}}", sbRows.ToString());
            htmlContent = htmlContent.Replace("{{TotalBags}}", totalBags.ToString());
            htmlContent = htmlContent.Replace("{{TotalWeight}}", totalWeight.ToString("F5"));
            htmlContent = htmlContent.Replace("{{TotalReceiptQty}}", totalWeight.ToString("F5"));

            // 6. Convert the populated HTML to PDF using SelectPdf 
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlContent);

            // 7. Push to browser for download 
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=DeliveryOrder_" + dr["Delivery_Order_No"] + ".pdf");
            doc.Save(Response.OutputStream);
            doc.Close();
            Response.End();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        // Exporting the Gatepass to PDF
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            pdfDoc.Add(new Paragraph("M.P. WAREHOUSING & LOGISTICS CORPORATION"));
            pdfDoc.Add(new Paragraph("Gatepass/Deposit Form"));
            pdfDoc.Add(new Paragraph("Branch: " + ddlBranch.SelectedItem.Text + ""));
            pdfDoc.Add(new Paragraph("Receipt No: 2328003030121102231"));
            pdfDoc.Add(new Paragraph("Date: 14/04/2022"));
            pdfDoc.Add(new Paragraph("Depositor: MPSCSC"));
            pdfDoc.Add(new Paragraph("Warehouse: Unique Warehouse 23"));

            pdfDoc.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Gatepass.pdf");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
        }
    }


    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Requirement 2: Bind Godown based on selection
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "select distinct sdo.Delivery_Order_No from tbl_Storage_GatePass_Enrty gp " +
                "inner join tbl_Delivery_Stacking_Details_GatePass as sgp  on gp.GatePass_no = sgp.GatePass_no " +
                "join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID = sdo.Delivery_Order_No where gp.Issue_Source = 'DO' " +
                "and gp.Status != 'Cancel' and gp.Issue_Source_ID != '0' And gp.Godown_ID = '" + ddlGodown.SelectedItem.Value + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BranchId", ddlBranch.SelectedValue);
            cmd.CommandTimeout = 3600;
            con.Open();
            ddlDeliveryOrderNo.DataSource = cmd.ExecuteReader();
            ddlDeliveryOrderNo.DataTextField = "Delivery_Order_No";
            ddlDeliveryOrderNo.DataValueField = "Delivery_Order_No";
            ddlDeliveryOrderNo.DataBind();
        }
    }


}
