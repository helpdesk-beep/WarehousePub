using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_ReceivePaymentFromNAFED_For_Rigion : System.Web.UI.Page
{
    // FIXED: Synced connection database configuration token seamlessly
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
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("Date_Wise_Payment_Received_From_NAFED", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                string userRole = Session["UserName"].ToString();

                if (userRole == "MPSWLC")
                {
                    cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
                }
                else
                {
                    if (Session["Region_ID"] != null && !string.IsNullOrEmpty(Session["Region_ID"].ToString()))
                    {
                        cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString().Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
                    }
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvReport.DataSource = dt;
                    gvReport.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvReport.UseAccessibleHeader = true;
                        gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    // SCREEN RENDER PIPELINE: Automatically appends Consolidated State Grand Total summary row lines cleanly
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0)
        {
            InjectGrandTotalSummaryRow(gvReport);
        }
        base.Render(writer);
    }

    // FIXED ENGINE: Subtotal logic blocks completely dropped -> Calculates absolute Grand Totals array values
    private void InjectGrandTotalSummaryRow(GridView targetGrid)
    {
        Table gridTable = (Table)targetGrid.Controls[0];

        decimal grandStorageAmt = 0; int grandStorageCount = 0; decimal grandPssAmt = 0;
        decimal grandPsfAmt = 0; decimal grandTdsAmt = 0; decimal grandOtherAmt = 0; decimal grandReceivedAmt = 0;

        for (int i = 1; i < gridTable.Rows.Count; i++)
        {
            GridViewRow currentRow = gridTable.Rows[i] as GridViewRow;
            if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
            {
                grandStorageAmt += ParseDecimalCell(currentRow.Cells[2]);
                grandStorageCount += (int)ParseDecimalCell(currentRow.Cells[3]);
                grandPssAmt += ParseDecimalCell(currentRow.Cells[4]);
                grandPsfAmt += ParseDecimalCell(currentRow.Cells[5]);
                grandTdsAmt += ParseDecimalCell(currentRow.Cells[6]);
                grandOtherAmt += ParseDecimalCell(currentRow.Cells[7]);
                grandReceivedAmt += ParseDecimalCell(currentRow.Cells[8]);
            }
        }

        GridViewRow grandRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        grandRow.CssClass = "grandtotal-row";
        grandRow.Font.Bold = true;

        // ColumnSpan covers index 0 and 1 (S.No and Payment Date Column) -> ColumnSpan = 2
        TableCell labelCell = new TableCell { Text = "Grand Total Summary :", ColumnSpan = 2, HorizontalAlign = HorizontalAlign.Right };
        labelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
        grandRow.Cells.Add(labelCell);

        grandRow.Cells.Add(new TableCell { Text = grandStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandStorageCount.ToString("N0"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandPssAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandPsfAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandTdsAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandOtherAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        grandRow.Cells.Add(new TableCell { Text = grandReceivedAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        foreach (TableCell cell in grandRow.Cells)
        {
            if (cell.HorizontalAlign == HorizontalAlign.Right)
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        }
        gridTable.Rows.Add(grandRow);
    }

    private decimal ParseDecimalCell(TableCell cell)
    {
        decimal val = 0;
        string clean = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        decimal.TryParse(clean, out val);
        return val;
    }

    private string GetCellTextRow(TableCell cell)
    {
        string raw = cell.Text;
        if (string.IsNullOrEmpty(raw) && cell.Controls.Count > 0)
        {
            foreach (Control ctrl in cell.Controls)
            {
                if (ctrl is HyperLink) return ((HyperLink)ctrl).Text.Trim();
                if (ctrl is LinkButton) return ((LinkButton)ctrl).Text.Trim();
                if (ctrl is LiteralControl) return ((LiteralControl)ctrl).Text.Trim();
            }
        }
        return raw.Trim();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 2; i <= 8; i++)
            {
                string format = (i == 3) ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; " + format);
            }
        }
    }

    // ISOLATED STREAM EXPORT PIPELINE: Captures and downloads flat text dataset perfectly into Excel sheets without component errors
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0)
        {
            BindReport();
        }
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Date_Wise_Storage_Bill_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvReport.GridLines = GridLines.Both;

                // Strips HTML dynamic hyperlink tags to export clean flat values to Excel sheet columns
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Cells[1].Text = GetCellTextRow(row.Cells[1]);
                    }
                }

                InjectGrandTotalSummaryRow(gvReport);

                Response.Write("<style> .text-right-align { text-align:right !important; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>NAFED Date Wise Storage Charges & Received Payment Report</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Scope: Region Consolidated View</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);

                // Directly serializes target isolated data table control tree context into streaming excel file pipelines
                gvReport.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* REQUIRED: Kept blank to bypass ASP.NET server-form compile checks wrapper limits during excel downloads */
    }
}