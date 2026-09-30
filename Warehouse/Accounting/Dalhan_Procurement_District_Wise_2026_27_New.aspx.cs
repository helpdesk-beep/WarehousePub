using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Dalhan_Procurement_District_Wise_2026_27_New : System.Web.UI.Page
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
            PopulateRegionFilters();
            BindDalhanReport();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDalhanReport();
    }

    private void PopulateRegionFilters()
    {
        try
        {
            // Pulls active geographic regions master lists natively
            string query = "SELECT DISTINCT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT WHERE Regionnm IS NOT NULL ORDER BY Regionnm";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlRegion.DataSource = dt;
                    ddlRegion.DataTextField = "Regionnm";
                    ddlRegion.DataValueField = "Region_ID";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, new ListItem("-- All Regions Summary --", "0"));
                }
            }
        }
        catch { }
    }

    private void BindDalhanReport()
    {
        try
        {
            // FIXED: Now mapping strictly to your database Stored Procedure
            using (SqlCommand cmd = new SqlCommand("Dalhan_Procurement_District_Wise_2026_27_New", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // FIXED PARAMETER REFERENCE: Explicitly mapping @RegionID into your procedure variable array
                cmd.Parameters.AddWithValue("@RegionID", Convert.ToInt32(ddlRegion.SelectedValue));

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);

                    gvDalhan.DataSource = dt;
                    gvDalhan.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvDalhan.UseAccessibleHeader = true;
                        gvDalhan.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution failure trace: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    // Advanced Post-Data Bind Rendering Engine for Hierarchical Region Group breaks and Grand Totals
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvDalhan.Rows.Count > 0)
        {
            Table gridTable = (Table)gvDalhan.Controls[0];

            // Region Accumulator pools
            decimal rAccept = 0, rTotal = 0;

            // Grand Accumulator pools
            decimal gAccept = 0, gTotal = 0;

            string lastRegion = gridTable.Rows[1].Cells[1].Text.Trim();

            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    string currentRegion = currentRow.Cells[1].Text.Trim();

                    if (currentRegion != lastRegion)
                    {
                        // Region Break Jumps -> Inject Subtotal summary row safely
                        GridViewRow subRow = CreateSummaryRow(lastRegion + " Region Total :", rAccept, rTotal, "subtotal-row");
                        gridTable.Rows.AddAt(k, subRow);
                        k++;

                        rAccept = 0; rTotal = 0;
                        lastRegion = currentRegion;
                    }

                    rAccept += ParseDecimalCell(currentRow.Cells[4]);
                    rTotal += ParseDecimalCell(currentRow.Cells[5]);

                    gAccept += ParseDecimalCell(currentRow.Cells[4]);
                    gTotal += ParseDecimalCell(currentRow.Cells[5]);
                }
            }

            // Append final trailing open region subtotal row matrix bounds
            GridViewRow finalSubRow = CreateSummaryRow(lastRegion + " Region Total :", rAccept, rTotal, "subtotal-row");
            gridTable.Rows.Add(finalSubRow);

            // Append Consolidated State level Grand Total Summary line balance
            GridViewRow grandRow = CreateSummaryRow("State Grand Total Summary :", gAccept, gTotal, "grandtotal-row");
            gridTable.Rows.Add(grandRow);
        }

        base.Render(writer);
    }

    private GridViewRow CreateSummaryRow(string title, decimal accept, decimal total, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        row.Cells.Add(new TableCell { Text = title, ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = accept.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = total.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        // Progress mathematical calculation percentage matrix shield
        decimal avgPercent = accept > 0 ? (total * 100 / accept) : 0;
        row.Cells.Add(new TableCell { Text = avgPercent.ToString("F2") + "%", HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

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
        string clean = cell.Text.Replace("&nbsp;", "").Replace("%", "").Replace(",", "").Trim();
        decimal.TryParse(clean, out val);
        return val;
    }

    protected void gvDalhan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            e.Row.Cells[5].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            e.Row.Cells[6].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDalhan.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Dalhan_Procurement_District_Wise_Summary.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindDalhanReport();

                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='7' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='7' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise Dalhan Procurement & WHR Balance Progress Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Selected Scope Filter Code: {0}</td><td colspan='3' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='7' style='border:none;'>&nbsp;</td></tr>
                    </table>", ddlRegion.SelectedItem.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                this.Render(hw);
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}