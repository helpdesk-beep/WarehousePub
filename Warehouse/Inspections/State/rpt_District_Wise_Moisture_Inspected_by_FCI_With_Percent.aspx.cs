using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

public partial class Inspections_State_rpt_District_Wise_Moisture_Inspected_by_FCI_With_Percent : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindReportData();
        }
    }

    private void BindReportData()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("rpt_District_Wise_Moisture_Inspected_by_FCI_With_Percent", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    sda.Fill(dt);

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
            Response.Write("<script>alert('Data Read Failure: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table gridTable = (Table)gvReport.Controls[0];

            // Regional group pools accumulator buckets
            int rGodowns = 0, rInspectedGodowns = 0, rPendingGodowns = 0;
            int rMoisture = 0, rSentDM = 0, rFci = 0;

            // State level grand total pools accumulator buckets
            int gGodowns = 0, gInspectedGodowns = 0, gPendingGodowns = 0;
            int gMoisture = 0, gSentDM = 0, gFci = 0;

            string lastRegion = gridTable.Rows[1].Cells[1].Text.Trim();

            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    string currentRegion = currentRow.Cells[1].Text.Trim();

                    if (currentRegion != lastRegion)
                    {
                        // Region Break Jumps -> Inject Summary Row over active matrix bounds
                        GridViewRow subRow = CreateSummaryRow(lastRegion + " Region Total :", rGodowns, rInspectedGodowns, rPendingGodowns, rMoisture, rSentDM, rFci, "subtotal-row");
                        gridTable.Rows.AddAt(k, subRow);
                        k++;

                        rGodowns = 0; rInspectedGodowns = 0; rPendingGodowns = 0;
                        rMoisture = 0; rSentDM = 0; rFci = 0;
                        lastRegion = currentRegion;
                    }

                    // FIXED INDEX CONFIGURATION: Safely reading columns from GridView Row array
                    int totalGodowns = int.Parse(currentRow.Cells[3].Text.Replace(",", "").Trim());
                    int fciGodowns = int.Parse(currentRow.Cells[4].Text.Replace(",", "").Trim());
                    int pendingGodowns = int.Parse(currentRow.Cells[5].Text.Replace(",", "").Trim());

                    int totalMoisture = int.Parse(currentRow.Cells[7].Text.Replace(",", "").Trim());
                    int sentDM = int.Parse(currentRow.Cells[8].Text.Replace(",", "").Trim());
                    int fciInspected = int.Parse(currentRow.Cells[9].Text.Replace(",", "").Trim());

                    rGodowns += totalGodowns;
                    rInspectedGodowns += fciGodowns;
                    rPendingGodowns += pendingGodowns;
                    rMoisture += totalMoisture;
                    rSentDM += sentDM;
                    rFci += fciInspected;

                    gGodowns += totalGodowns;
                    gInspectedGodowns += fciGodowns;
                    gPendingGodowns += pendingGodowns;
                    gMoisture += totalMoisture;
                    gSentDM += sentDM;
                    gFci += fciInspected;
                }
            }

            // Flush out remaining trailing region subtotal row summary lines
            GridViewRow finalRegRow = CreateSummaryRow(lastRegion + " Region Total :", rGodowns, rInspectedGodowns, rPendingGodowns, rMoisture, rSentDM, rFci, "subtotal-row");
            gridTable.Rows.Add(finalRegRow);

            // Append Final Consolidated State Level Grand Total Summary Row
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";
            grandTotalRow.Font.Bold = true;

            // FIXED FORMULA MATH: Computed via (FCI_Inspected_Godowns * 100) / Total_Godown with divide-by-zero protection
            double grandGodownPendingPerc = gGodowns > 0 ? ((double)gInspectedGodowns * 100 / gGodowns) : 0;
            double grandStackPercentage = gSentDM > 0 ? ((double)gFci * 100 / gSentDM) : 0;

            grandTotalRow.Cells.Add(new TableCell { Text = "State Grand Total Summary", ColumnSpan = 3, HorizontalAlign = HorizontalAlign.Right });
            grandTotalRow.Cells.Add(new TableCell { Text = gGodowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
            grandTotalRow.Cells.Add(new TableCell { Text = gInspectedGodowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
            grandTotalRow.Cells.Add(new TableCell { Text = gPendingGodowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
            grandTotalRow.Cells.Add(new TableCell { Text = grandGodownPendingPerc.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right });

            grandTotalRow.Cells.Add(new TableCell { Text = gMoisture.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
            grandTotalRow.Cells.Add(new TableCell { Text = gSentDM.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
            grandTotalRow.Cells.Add(new TableCell { Text = gFci.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
            grandTotalRow.Cells.Add(new TableCell { Text = grandStackPercentage.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right });

            foreach (TableCell cell in grandTotalRow.Cells)
            {
                cell.Attributes.Add("style", "font-weight:bold !important;");
            }
            gridTable.Rows.Add(grandTotalRow);
        }

        base.Render(writer);
    }

    private GridViewRow CreateSummaryRow(string titleText, int godowns, int fciGodowns, int pendingGodowns, int moisture, int sentDM, int fci, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        // FIXED FORMULA MATH: Automated per Region Group breaks context map explicitly
        double godownPendingPerc = godowns > 0 ? ((double)fciGodowns * 100 / godowns) : 0;
        double stackPercentage = sentDM > 0 ? ((double)fci * 100 / sentDM) : 0;

        row.Cells.Add(new TableCell { Text = titleText, ColumnSpan = 3, HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = godowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
        row.Cells.Add(new TableCell { Text = fciGodowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
        row.Cells.Add(new TableCell { Text = pendingGodowns.ToString("N0"), HorizontalAlign = HorizontalAlign.Center });
        row.Cells.Add(new TableCell { Text = godownPendingPerc.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right });

        row.Cells.Add(new TableCell { Text = moisture.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = sentDM.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = fci.ToString("N0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = stackPercentage.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right });

        foreach (TableCell cell in row.Cells)
        {
            if (cell.HorizontalAlign == HorizontalAlign.Right)
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            else if (cell.HorizontalAlign == HorizontalAlign.Center)
                cell.Attributes.Add("style", "text-align:center !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0;");
        }

        return row;
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Structural layout alignments locks mapped from column cell index 3 through column index 10
            e.Row.Cells[3].Attributes.Add("style", "text-align:center !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[4].Attributes.Add("style", "text-align:center !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[5].Attributes.Add("style", "text-align:center !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[6].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");

            e.Row.Cells[7].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[8].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[9].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[10].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Moisture_FCI_Inspection_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReportData();

                Response.Write("<style> .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='11' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='11' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise Moisture Inspection Progress Summary Percent Statement Report</th></tr>
                        <tr><td colspan='6' style='text-align:left; font-weight:bold; color:#475569;'>Report Context Scope: State Consolidated View</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='11' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);
                this.Render(hw);

                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}