using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_ReceivePaymentFromNAFED_Details_For_Rigion : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["PayDate"] != null && !string.IsNullOrEmpty(Request.QueryString["PayDate"].ToString()))
            {
                string payDate = Request.QueryString["PayDate"].ToString().Trim();
                LoadGodownBillDetails(payDate);
            }
            else
            {
                Response.Redirect("ReceivePaymentFromNAFED_For_Rigion.aspx");
            }
        }
    }

    private void LoadGodownBillDetails(string payDate)
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_NAFED_Storage_Bill_And_Payment_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentDate", payDate);

                if (Session["UserName"].ToString() == "MPSWLC")
                {
                    cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"] != null ? Session["Region_ID"].ToString().Trim() : DBNull.Value.ToString());
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    lblGodownName.Text = payDate;
                    lblPrintGodown.Text = payDate;

                    if (dt.Rows.Count > 0)
                    {
                        gvDetails.DataSource = dt;
                        gvDetails.DataBind();

                        gvDetails.UseAccessibleHeader = true;
                        gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

                        foreach (TableCell cell in gvDetails.HeaderRow.Cells)
                        {
                            cell.Attributes.Add("style", "color:#ffffff !important; text-align:center !important; vertical-align:middle !important; background-color:#1e3a8a !important; font-weight:bold !important;");
                        }
                    }
                    else
                    {
                        gvDetails.DataSource = null;
                        gvDetails.DataBind();
                        lblGodownName.Text = "No settlement records captured on Date: " + payDate;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    // SCREEN DISPLAYS PIPELINE: Intercepts table matrix to dynamically render Branch Subtotals and State Grand Totals
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table gridTable = (Table)gvDetails.Controls[0];

            // Subtotal and Grand Total accumulation structural arrays
            decimal[] rTotals = new decimal[6]; // Indices mapping: 0:BillAmt, 1:PSS, 2:PSF, 3:TDS, 4:Other, 5:Received
            decimal[] gTotals = new decimal[6];

            // Branch (Depot Name) is at cell index 2
            string lastBranch = gridTable.Rows[1].Cells[2].Text.Trim();

            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    string currentBranch = currentRow.Cells[2].Text.Trim();

                    if (currentBranch != lastBranch)
                    {
                        // Branch Switches -> Inject orange formatted summary row directly into control tree
                        GridViewRow subRow = CreateSummaryRow(lastBranch + " Branch Total :", rTotals, "subtotal-row");
                        gridTable.Rows.AddAt(k, subRow);
                        k++;

                        Array.Clear(rTotals, 0, rTotals.Length);
                        lastBranch = currentBranch;
                    }

                    // Map math data extractions from concrete visible indices safely
                    rTotals[0] += ParseDecimalCell(currentRow.Cells[4]); // Bill Amount
                    rTotals[1] += ParseDecimalCell(currentRow.Cells[6]); // PSS Amount
                    rTotals[2] += ParseDecimalCell(currentRow.Cells[7]); // PSF Amount
                    rTotals[3] += ParseDecimalCell(currentRow.Cells[8]); // TDS Deduction
                    rTotals[4] += ParseDecimalCell(currentRow.Cells[9]); // Other Deduction
                    rTotals[5] += ParseDecimalCell(currentRow.Cells[10]); // Payment Received

                    gTotals[0] += ParseDecimalCell(currentRow.Cells[4]);
                    gTotals[1] += ParseDecimalCell(currentRow.Cells[6]);
                    gTotals[2] += ParseDecimalCell(currentRow.Cells[7]);
                    gTotals[3] += ParseDecimalCell(currentRow.Cells[8]);
                    gTotals[4] += ParseDecimalCell(currentRow.Cells[9]);
                    gTotals[5] += ParseDecimalCell(currentRow.Cells[10]);
                }
            }

            // Flush out trailing remaining branch subtotal row lines balances
            GridViewRow finalSubRow = CreateSummaryRow(lastBranch + " Branch Total :", rTotals, "subtotal-row");
            gridTable.Rows.Add(finalSubRow);

            // Append final Blue State Level Grand Total Summary row
            GridViewRow grandRow = CreateSummaryRow("Grand Total Summary :", gTotals, "grandtotal-row");
            gridTable.Rows.Add(grandRow);
        }

        base.Render(writer);
    }

    private GridViewRow CreateSummaryRow(string title, decimal[] totals, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        // ColumnSpan merges S.No, District, Branch, and Godown columns cleanly (Indices 0 to 3) -> ColumnSpan = 4
        TableCell labelCell = new TableCell { Text = title, ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
        labelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
        row.Cells.Add(labelCell);

        // Bind amounts sequentially matching active schema positions
        row.Cells.Add(createSummaryCell(totals[0])); // Bill Amount Cell
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center }); // BM Deduction Status placeholder (-)
        row.Cells.Add(createSummaryCell(totals[1])); // PSS Amount Cell
        row.Cells.Add(createSummaryCell(totals[2])); // PSF Amount Cell
        row.Cells.Add(createSummaryCell(totals[3])); // TDS Deduction Cell
        row.Cells.Add(createSummaryCell(totals[4])); // Other Deduction Cell
        row.Cells.Add(createSummaryCell(totals[5])); // Payment Received Cell

        // Placeholders to complete the 13 columns matrix perfectly without extra bounding boxes leak
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center }); // Owner Payment Status placeholder
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center }); // Credit Date placeholder

        return row;
    }

    private TableCell createSummaryCell(decimal value)
    {
        TableCell cell = new TableCell { Text = value.ToString("N2"), HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        return cell;
    }

    private decimal ParseDecimalCell(TableCell cell)
    {
        decimal val = 0;
        string clean = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        decimal.TryParse(clean, out val);
        return val;
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Align active numeric rows cells indices perfectly over right side margins
            e.Row.Cells[4].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            for (int i = 6; i <= 10; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceivePaymentFromNAFED_For_Rigion.aspx");
    }

    // FIXED EXCEL EXPORT PIPELINE: Forces isolated rendering tree to capture all calculated subtotal blocks seamlessly
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count == 0 && Request.QueryString["PayDate"] != null)
        {
            LoadGodownBillDetails(Request.QueryString["PayDate"].ToString().Trim());
        }
        if (gvDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_NAFED_Datewise_Settlement_Details.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvDetails.GridLines = GridLines.Both;
                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='13' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='13' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise Billwise Settlement History (NAFED)</th></tr>
                        <tr><td colspan='7' style='text-align:left; font-weight:bold; color:#475569;'>Selected Payment Date: {0}</td><td colspan='6' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='13' style='border:none;'>&nbsp;</td></tr>
                    </table>", lblGodownName.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                this.Render(hw); // Captures computed elements buffer safely
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* REQUIRED: Left blank to pass compliance tracking checks during explicit excel conversions */
    }
}