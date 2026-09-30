using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Get_Godown_Bill_Wise_Payment_Status_NCCF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["Region_ID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // Default current month configuration ranges bounds setup
            txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
            txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            BindNCCFReport();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindNCCFReport();
    }

    private void BindNCCFReport()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Bill_Wise_Payment_Status_NCCF", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // FIXED PARAMETERS MAPS VIA REGION ACTIVE PROFILE SESSION MATRIX
                cmd.Parameters.AddWithValue("@Region_ID", Convert.ToString(Session["Region_ID"]).Trim());

                // Date parameter conversion boundary shield
                string fDateFormatted = "";
                if (!string.IsNullOrEmpty(txtFromDate.Text))
                {
                    fDateFormatted = Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy");
                }
                cmd.Parameters.AddWithValue("@FromDate", fDateFormatted);

                string tDateFormatted = "";
                if (!string.IsNullOrEmpty(txtToDate.Text))
                {
                    tDateFormatted = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                }
                cmd.Parameters.AddWithValue("@ToDate", tDateFormatted);

                // Set printing screen labels
                lblPrintFromDate.Text = string.IsNullOrEmpty(fDateFormatted) ? "All Past" : fDateFormatted;
                lblPrintToDate.Text = string.IsNullOrEmpty(tDateFormatted) ? "All Future" : tDateFormatted;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);

                    gvNCCF.DataSource = dt;
                    gvNCCF.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvNCCF.UseAccessibleHeader = true;
                        gvNCCF.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Failure Log Trace: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    // Dynamic Row rendering algorithm handler injects District Level Subtotals safely
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvNCCF.Rows.Count > 0)
        {
            Table gridTable = (Table)gvNCCF.Controls[0];

            decimal dStorage = 0, dPss = 0, dPsf = 0, dTdsNccf = 0, dOtherNccf = 0, dRecdNccf = 0, dRmWet = 0;
            decimal gStorage = 0, gPss = 0, gPsf = 0, gTdsNccf = 0, gOtherNccf = 0, gRecdNccf = 0, gRmWet = 0;

            string lastDistrict = gridTable.Rows[1].Cells[1].Text.Trim();

            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    string currentDistrict = currentRow.Cells[1].Text.Trim();

                    if (currentDistrict != lastDistrict)
                    {
                        GridViewRow subRow = CreateSummaryRow(lastDistrict + " District Sub-Total :", dStorage, dPss, dPsf, dTdsNccf, dOtherNccf, dRecdNccf, dRmWet, "subtotal-row");
                        gridTable.Rows.AddAt(k, subRow);
                        k++;

                        dStorage = 0; dPss = 0; dPsf = 0; dTdsNccf = 0; dOtherNccf = 0; dRecdNccf = 0; dRmWet = 0;
                        lastDistrict = currentDistrict;
                    }

                    dStorage += ParseDecimalCell(currentRow.Cells[8]);
                    dPss += ParseDecimalCell(currentRow.Cells[9]);
                    dPsf += ParseDecimalCell(currentRow.Cells[10]);
                    dTdsNccf += ParseDecimalCell(currentRow.Cells[11]);
                    dOtherNccf += ParseDecimalCell(currentRow.Cells[12]);
                    dRecdNccf += ParseDecimalCell(currentRow.Cells[13]);
                    dRmWet += ParseDecimalCell(currentRow.Cells[14]);

                    gStorage += ParseDecimalCell(currentRow.Cells[8]);
                    gPss += ParseDecimalCell(currentRow.Cells[9]);
                    gPsf += ParseDecimalCell(currentRow.Cells[10]);
                    gTdsNccf += ParseDecimalCell(currentRow.Cells[11]);
                    gOtherNccf += ParseDecimalCell(currentRow.Cells[12]);
                    gRecdNccf += ParseDecimalCell(currentRow.Cells[13]);
                    gRmWet += ParseDecimalCell(currentRow.Cells[14]);
                }
            }

            GridViewRow finalSubRow = CreateSummaryRow(lastDistrict + " District Sub-Total :", dStorage, dPss, dPsf, dTdsNccf, dOtherNccf, dRecdNccf, dRmWet, "subtotal-row");
            gridTable.Rows.Add(finalSubRow);

            GridViewRow grandRow = CreateSummaryRow("Regional Grand Total Summary :", gStorage, gPss, gPsf, gTdsNccf, gOtherNccf, gRecdNccf, gRmWet, "grandtotal-row");
            gridTable.Rows.Add(grandRow);
        }

        base.Render(writer);
    }

    private GridViewRow CreateSummaryRow(string title, decimal storage, decimal pss, decimal psf, decimal tdsNccf, decimal otherNccf, decimal recdNccf, decimal rmTds, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        row.Cells.Add(new TableCell { Text = title, ColumnSpan = 8, HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = storage.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = pss.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = psf.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = tdsNccf.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = otherNccf.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = recdNccf.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = rmTds.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        row.Cells.Add(new TableCell { Text = "" });
        row.Cells.Add(new TableCell { Text = "" });

        foreach (TableCell cell in row.Cells)
        {
            if (cell.HorizontalAlign == HorizontalAlign.Right)
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        }

        return row;
    }

    private decimal ParseDecimalCell(TableCell cell)
    {
        decimal val = 0;
        string clean = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        decimal.TryParse(clean, out val);
        return val;
    }

    protected void gvNCCF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 8; i <= 14; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvNCCF.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Bill_Wise_Payment_Status_NCCF_Region.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindNCCFReport();

                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fef08a !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");

                string fDateFormatted = string.IsNullOrEmpty(txtFromDate.Text) ? "All Past" : Convert.ToDateTime(txtFromDate.Text).ToString("dd/MM/yyyy");
                string tDateFormatted = string.IsNullOrEmpty(txtToDate.Text) ? "All Future" : Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='17' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='17' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Bill Wise Payment Status Report - NCCF</th></tr>
                        <tr><td colspan='9' style='text-align:left; font-weight:bold; color:#475569;'>Payment Filters: {0} to {1}</td><td colspan='8' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {2}</td></tr>
                        <tr><td colspan='17' style='border:none;'>&nbsp;</td></tr>
                    </table>", fDateFormatted, tDateFormatted, dateTimeStr);

                Response.Write(customExcelHeader);
                this.Render(hw);
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}