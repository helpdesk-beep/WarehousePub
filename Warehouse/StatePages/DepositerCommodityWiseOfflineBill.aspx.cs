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
using System.Text;

public partial class SRV_StatePages_DepositerCommodityWiseOfflineBill : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    DataTable dtReport;
    int currentPageIndex = 1;
    int pageSize = 50;
    int totalRecords = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindRegions();
            //BindFinancialYears();
            BindCommodities();
            BindDepositors();
            ClearMessage();
            BindReportData();
        }
    }

    #region Data Binding Methods

    private void BindRegions()
    {
        try
        {
            string query = @"SELECT DISTINCT Region_ID, Regionnm 
                           FROM tbl_Metadata_District 
                           WHERE Region_ID IS NOT NULL AND Regionnm IS NOT NULL 
                           ORDER BY Regionnm";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlRegion.DataSource = dt;
                ddlRegion.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error loading regions: " + ex.Message, "danger");
        }
    }

    //private void BindFinancialYears()
    //{
    //    try
    //    {
    //        string query = @"SELECT DISTINCT FinancialYear 
    //                       FROM tbl_GodownDepositorwise_PaymentStatus 
    //                       WHERE FinancialYear IS NOT NULL 
    //                       ORDER BY FinancialYear DESC";

    //        using (SqlCommand cmd = new SqlCommand(query, con))
    //        {
    //            con.Open();
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            da.Fill(dt);
    //            ddlFinancialYear.DataSource = dt;
    //            ddlFinancialYear.DataBind();
    //            con.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowMessage("Error loading financial years: " + ex.Message, "danger");
    //    }
    //}

    private void BindCommodities()
    {
        try
        {
            string query = @"SELECT Commodity_Id, Commodity_Name 
                           FROM tbl_MetaData_STORAGE_COMMODITY 
                           WHERE Commodity_Name IS NOT NULL 
                           ORDER BY Commodity_Name";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlCommodity.DataSource = dt;
                ddlCommodity.DataBind();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error loading commodities: " + ex.Message, "danger");
        }
    }

    private void BindDepositors()
    {
        string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.Items.Clear();
            ddlDepositor.DataSource = ds.Tables[0];
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            //ddlDepositor.Items.Insert(0, "--Select--");
            ddlDepositor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    #endregion

    #region Report Data Methods

    private void BindReportData()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("usp_Get_DepositorWise_PaymentStatus_Commodity_Wise_State", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters
                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue);
                cmd.Parameters.AddWithValue("@CommodityID", ddlCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue);

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtReport = new DataTable();
                da.Fill(dtReport);
                con.Close();


                // Store in ViewState for paging and sorting
                ViewState["ReportData"] = dtReport;
                totalRecords = dtReport.Rows.Count;

                // Calculate and display totals
                CalculateTotals();

                // Bind to GridView with paging
                BindGridView(); 
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error loading report data: " + ex.Message, "danger");
        }
    }




    private void CalculateTotals()
    {
        if (dtReport != null && dtReport.Rows.Count > 0)
        {
            decimal totalAmountPresented = 0;
            decimal totalAmountReceived = 0;
            decimal totalRemaining = 0;
            int totalBills = 0;

            foreach (DataRow row in dtReport.Rows)
            {
                totalBills += Convert.ToInt32(row["TotalBillPresentedinFY"]);
                totalAmountPresented += Convert.ToDecimal(row["TotalBillAmountPresented"]);
                totalAmountReceived += Convert.ToDecimal(row["TotalAmountReceivedInFY"]);
                totalRemaining += Convert.ToDecimal(row["RemainingAmountFromDepositor"]);
            }

            lblTotalRecords.Text = dtReport.Rows.Count.ToString();
            lblTotalAmountPresented.Text = totalAmountPresented.ToString("N2");
            lblTotalAmountReceived.Text = totalAmountReceived.ToString("N2");
            lblTotalRemaining.Text = totalRemaining.ToString("N2");

            // Create totals row for GridView
            DataRow totalsRow = dtReport.NewRow();

            // आपके ASPX पेज के कोलम ऑर्डर के अनुसार
            totalsRow["RegionName"] = "";                     // 1. Region - खाली
            totalsRow["FinancialYear"] = "";                  // 2. Financial Year - खाली
            totalsRow["Commodity_Name"] = "";                 // 3. Commodity - खाली
            totalsRow["DepositorName"] = "TOTAL";             // 4. Depositor Name - TOTAL
            totalsRow["TotalBillPresentedinFY"] = totalBills; // 5. Total Bills - योग
            totalsRow["TotalBillAmountPresented"] = totalAmountPresented;   // 6. Bill Amount (₹) - योग
            totalsRow["TotalAmountReceivedInFY"] = totalAmountReceived;     // 7. Amount Received (₹) - योग
            totalsRow["RemainingAmountFromDepositor"] = totalRemaining;     // 8. Remaining Amount (₹) - योग

            DataTable totalsDt = dtReport.Clone();
            totalsDt.Rows.Add(totalsRow.ItemArray);
            gvTotals.DataSource = totalsDt;
            gvTotals.DataBind();

            // टोटल रो को स्टाइल करने के लिए RowDataBound इवेंट
            if (gvTotals.Rows.Count > 0)
            {
                GridViewRow row = gvTotals.Rows[0];

                // TOTAL लेबल को बोल्ड और अलग रंग दें
                row.Cells[3].Font.Bold = true; // Depositor Name (4th column, zero-based index 3)
                row.Cells[3].ForeColor = System.Drawing.Color.DarkBlue;

                // संख्यात्मक कोलम्स को बोल्ड और right-aligned करें
                for (int i = 4; i <= 7; i++) // Columns 5-8 (zero-based index 4-7)
                {
                    row.Cells[i].Font.Bold = true;
                    row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    row.Cells[i].ForeColor = System.Drawing.Color.DarkGreen;
                }
            }

            gvTotals.Visible = true;
        }
        else
        {
            lblTotalRecords.Text = "0";
            lblTotalAmountPresented.Text = "0.00";
            lblTotalAmountReceived.Text = "0.00";
            lblTotalRemaining.Text = "0.00";
            gvTotals.Visible = false;
        }
    }

    //private void BindGridViewWithPaging()
    //{
    //    if (dtReport != null)
    //    {
    //        DataTable dtPaged = dtReport.Clone();

    //        int startIndex = (currentPageIndex - 1) * pageSize;
    //        int endIndex = Math.Min(startIndex + pageSize - 1, dtReport.Rows.Count - 1);

    //        for (int i = startIndex; i <= endIndex; i++)
    //        {
    //            dtPaged.ImportRow(dtReport.Rows[i]);
    //        }

    //        gvReport.DataSource = dtPaged;
    //        gvReport.DataBind();

    //        // Update page info
    //        UpdatePagingInfo();
    //    }
    //}

    private void BindGridView()
    {
        if (dtReport != null)
        {
            gvReport.AllowPaging = false;   // VERY IMPORTANT
            gvReport.DataSource = dtReport; // FULL DATA
            gvReport.DataBind();
        }
    }

    protected void gvReport_PreRender(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }



    private void UpdatePagingInfo()
    {
        int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        lblPageInfo.Text = string.Format("Page {0} of {1} (Total Records: {2})",
            currentPageIndex, totalPages, totalRecords);

        btnFirst.Enabled = currentPageIndex > 1;
        btnPrev.Enabled = currentPageIndex > 1;
        btnNext.Enabled = currentPageIndex < totalPages;
        btnLast.Enabled = currentPageIndex < totalPages;
    }

    #endregion

    #region Event Handlers

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            currentPageIndex = 1;
            BindReportData();
            //ShowMessage("Report generated successfully.", "success");
        }
        catch (Exception ex)
        {
            ShowMessage("Error: " + ex.Message, "danger");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlRegion.SelectedIndex = 0;
        ddlFinancialYear.SelectedIndex = 0;
        ddlCommodity.SelectedIndex = 0;
        ddlDepositor.SelectedIndex = 0;
        currentPageIndex = 1;
        BindReportData();
        //ShowMessage("Filters reset successfully.", "info");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtReport != null && dtReport.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                    "attachment;filename=DepositorCommodityWiseReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                // Add header
                hw.Write("<h2 style='text-align:center;'>Depositor Commodity Wise Offline Bill Report</h2>");
                hw.Write("<h4 style='margin-bottom:20px;'>Generated on: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</h4>");

                // Add filter criteria
                hw.Write("<table border='1' style='margin-bottom:20px;'>");
                hw.Write("<tr><td style='padding:5px;'><b>Region:</b></td><td style='padding:5px;'>" + ddlRegion.SelectedItem.Text + "</td></tr>");
                hw.Write("<tr><td style='padding:5px;'><b>Financial Year:</b></td><td style='padding:5px;'>" + ddlFinancialYear.SelectedItem.Text + "</td></tr>");
                hw.Write("<tr><td style='padding:5px;'><b>Commodity:</b></td><td style='padding:5px;'>" + ddlCommodity.SelectedItem.Text + "</td></tr>");
                hw.Write("<tr><td style='padding:5px;'><b>Depositor:</b></td><td style='padding:5px;'>" + ddlDepositor.SelectedItem.Text + "</td></tr>");
                hw.Write("</table>");

                // Create table for export
                hw.Write("<table border='1' style='border-collapse:collapse;'>");

                // Add headers
                hw.Write("<tr style='background-color:#2c3e50;color:white;'>");
                hw.Write("<th style='padding:8px;'>Region</th>");
                hw.Write("<th style='padding:8px;'>Financial Year</th>");
                hw.Write("<th style='padding:8px;'>Commodity</th>");
                hw.Write("<th style='padding:8px;'>Depositor Name</th>");
                hw.Write("<th style='padding:8px;'>Total Bills</th>");
                hw.Write("<th style='padding:8px;'>Bill Amount (₹)</th>");
                hw.Write("<th style='padding:8px;'>Amount Received (₹)</th>");
                hw.Write("<th style='padding:8px;'>Remaining Amount (₹)</th>");
                hw.Write("</tr>");

                // Add data rows
                foreach (DataRow row in dtReport.Rows)
                {
                    hw.Write("<tr>");
                    hw.Write("<td style='padding:5px;'>" + row["RegionName"] + "</td>");
                    hw.Write("<td style='padding:5px;'>" + row["FinancialYear"] + "</td>");
                    hw.Write("<td style='padding:5px;'>" + row["Commodity_Name"] + "</td>");
                    hw.Write("<td style='padding:5px;'>" + row["DepositorName"] + "</td>");
                    hw.Write("<td style='padding:5px;text-align:right;'>" + string.Format("{0:N0}", row["TotalBillPresentedinFY"]) + "</td>");
                    hw.Write("<td style='padding:5px;text-align:right;'>" + string.Format("{0:N2}", row["TotalBillAmountPresented"]) + "</td>");
                    hw.Write("<td style='padding:5px;text-align:right;'>" + string.Format("{0:N2}", row["TotalAmountReceivedInFY"]) + "</td>");
                    hw.Write("<td style='padding:5px;text-align:right;'>" + string.Format("{0:N2}", row["RemainingAmountFromDepositor"]) + "</td>");
                    hw.Write("</tr>");
                }

                // Add totals row
                hw.Write("<tr style='font-weight:bold;background-color:#e3f2fd;'>");
                hw.Write("<td colspan='4' style='padding:5px;'>TOTAL</td>");
                hw.Write("<td style='padding:5px;text-align:right;'>" +
                    string.Format("{0:N0}", dtReport.AsEnumerable().Sum(row => Convert.ToInt32(row["TotalBillPresentedinFY"]))) + "</td>");
                hw.Write("<td style='padding:5px;text-align:right;'>" +
                    string.Format("{0:N2}", dtReport.AsEnumerable().Sum(row => Convert.ToDecimal(row["TotalBillAmountPresented"]))) + "</td>");
                hw.Write("<td style='padding:5px;text-align:right;'>" +
                    string.Format("{0:N2}", dtReport.AsEnumerable().Sum(row => Convert.ToDecimal(row["TotalAmountReceivedInFY"]))) + "</td>");
                hw.Write("<td style='padding:5px;text-align:right;'>" +
                    string.Format("{0:N2}", dtReport.AsEnumerable().Sum(row => Convert.ToDecimal(row["RemainingAmountFromDepositor"]))) + "</td>");
                hw.Write("</tr>");

                hw.Write("</table>");

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                ShowMessage("No data to export.", "warning");
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error exporting data: " + ex.Message, "danger");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // JavaScript will handle the print functionality
        ClientScript.RegisterStartupScript(this.GetType(), "Print", "PrintReport();", true);
    }

    protected void gvReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["ReportData"] != null)
        {
            dtReport = (DataTable)ViewState["ReportData"];

            if (dtReport != null)
            {
                DataView dv = new DataView(dtReport);
                dv.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                dtReport = dv.ToTable();
                ViewState["ReportData"] = dtReport;
                //BindGridViewWithPaging();
                BindGridView();
            }
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Format amount cells
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            // Add hover effect
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#f5f5f5'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        currentPageIndex = 1;
        //BindGridViewWithPaging();
        BindGridView();
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (currentPageIndex > 1)
        {
            currentPageIndex--;
            //BindGridViewWithPaging();
            BindGridView();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        if (currentPageIndex < totalPages)
        {
            currentPageIndex++;
            //BindGridViewWithPaging();
            BindGridView();
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        currentPageIndex = totalPages;
        //BindGridViewWithPaging();
        BindGridView();
    }

    #endregion

    #region Helper Methods

    private string GetSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        ViewState["SortExpression"] = column;
        ViewState["SortDirection"] = sortDirection;

        return sortDirection;
    }

    private void ShowMessage(string message, string type)
    {
        pnlMessage.Visible = true;
        lblMessage.Text = message;

        // Set CSS class based on type
        pnlMessage.CssClass = "alert alert-" + type;
    }

    private void ClearMessage()
    {
        pnlMessage.Visible = false;
        lblMessage.Text = "";
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for GridView export to Excel
    }

    #endregion
}