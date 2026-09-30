using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web; 
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_ReceivePaymentFromNAFED_For_HO : System.Web.UI.Page
{
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Grand Totals Summary Accumulators Mapped Properly
    decimal sumTotalBillAmt = 0;
    int sumTotalBillsCount = 0;
    decimal sumPssBillAmt = 0;
    decimal sumPsfBillAmt = 0;
    decimal sumTdsDeduct = 0;
    decimal sumOtherDeduct = 0;
    decimal sumReceivedPayment = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRegionDropdown();
            BindGrid();
        }
    }

    private void FillRegionDropdown()
    {
        using (SqlConnection con = new SqlConnection(strConn))
        {
            string query = "SELECT DISTINCT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT WHERE Region_ID IS NOT NULL AND Regionnm IS NOT NULL ORDER BY Regionnm";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    ddlRegion.DataSource = dt;
                    ddlRegion.DataTextField = "Regionnm";
                    ddlRegion.DataValueField = "Region_ID";
                    ddlRegion.DataBind();
                }
            }
        }
        ddlRegion.Items.Insert(0, new ListItem("All Region", "0"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        // RESET ACCUMULATORS ON EVERY SEARCH CALL
        sumTotalBillAmt = 0; sumTotalBillsCount = 0; sumPssBillAmt = 0;
        sumPsfBillAmt = 0; sumTdsDeduct = 0; sumOtherDeduct = 0; sumReceivedPayment = 0;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Date_Wise_Payment_Received_From_NAFED_New", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(txtFromDate.Text))
                    cmd.Parameters.AddWithValue("@FromDate", txtFromDate.Text);
                else
                    cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);

                if (!string.IsNullOrEmpty(txtToDate.Text))
                    cmd.Parameters.AddWithValue("@ToDate", txtToDate.Text);
                else
                    cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);

                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvPaymentSummary.DataSource = dt;
                    gvPaymentSummary.DataBind();
                }
            }
        }
    }

    protected void gvPaymentSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Parse and add up row levels dynamic states cell metrics
            sumTotalBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill Amount"));
            sumTotalBillsCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill"));
            sumPssBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSS Bill Amount"));
            sumPsfBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSF Bill Amount"));
            sumTdsDeduct += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS Deduction From NCCF"));
            sumOtherDeduct += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other Deduction From NCCF"));
            sumReceivedPayment += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Received Payment From NCCF"));
        }
    }

    protected void gvPaymentSummary_DataBound(object sender, EventArgs e)
    {
        if (gvPaymentSummary.Rows.Count > 0)
        {
            Table tbl = (Table)gvPaymentSummary.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "Grand Total Summary :", ColumnSpan = 3 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(CreateSummaryCell(sumTotalBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumTotalBillsCount.ToString("N0"), HorizontalAlign.Center));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumPssBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumPsfBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumTdsDeduct.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumOtherDeduct.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumReceivedPayment.ToString("N2"), HorizontalAlign.Right));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell CreateSummaryCell(string textDisplay, HorizontalAlign align)
    {
        TableCell cell = new TableCell { Text = textDisplay, HorizontalAlign = align };
        cell.Attributes.Add("style", "font-weight:bold !important;");
        return cell;
    }

    protected void gvPaymentSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            string[] arguments = e.CommandArgument.ToString().Split('|');
            string paymentDate = arguments[0];
            string regionId = arguments[1];

            if (paymentDate.Contains("Before 2026"))
            {
                paymentDate = "1900-01-01";
            }

            // FIXED FOR TARGET BLANK: Navigates cleanly into a new browser tab context window open
            string targetUrl = string.Format("ReceivePaymentFromNAFED_Details_For_HO.aspx?PDate={0}&Region={1}",
                Server.UrlEncode(paymentDate), Server.UrlEncode(regionId));

            string script = string.Format("window.open('{0}', 'parent');", targetUrl);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OpenDetailsNewTab", script, true);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPaymentSummary.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Payment_Summary_Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            foreach (GridViewRow row in gvPaymentSummary.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnk = (LinkButton)row.FindControl("lnkPaymentDate");
                    if (lnk != null)
                    {
                        Label lbl = new Label { Text = lnk.Text };
                        row.Cells[2].Controls.Clear();
                        row.Cells[2].Controls.Add(lbl);
                    }
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    Table tableContainer = new Table();
                    TableRow rowTitle = new TableRow();
                    TableCell cellTitle = new TableCell();

                    cellTitle.Text = "<h2 style='text-align:center; color:#0056b3;'>MPWLC STORAGE MODULE</h2>" +
                                     "<h3 style='text-align:center;'>Date Wise Payment Received From NAFED</h3>" +
                                     "<p style='text-align:center;'>Region Filter: " + ddlRegion.SelectedItem.Text + " | Generated On: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "</p><br/>";
                    cellTitle.ColumnSpan = gvPaymentSummary.Columns.Count;
                    rowTitle.Cells.Add(cellTitle);
                    tableContainer.Rows.Add(rowTitle);

                    tableContainer.RenderControl(hw);
                    gvPaymentSummary.RenderControl(hw);

                    Response.Write(sbExcelStyleBuilder() + sw.ToString());
                    Response.Flush();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }

    private string sbExcelStyleBuilder()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#4e73df !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #dee2e6; text-transform:uppercase; }");
        sb.Append("td { border:1px solid #dee2e6; font-family: 'Segoe UI', Arial; }");
        sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #4e73df; border-bottom:2px solid #224abe; }");
        sb.Append("</style>");
        return sb.ToString();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}