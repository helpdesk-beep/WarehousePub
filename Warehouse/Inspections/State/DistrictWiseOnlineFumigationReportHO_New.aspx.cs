using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_DistrictWiseOnlineFumigationReportHO_New : System.Web.UI.Page
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
            FetchFumigationSummary();
        }
    }

    private DataTable GetFumigationData()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Online_Fumigation_Summary_HO_New", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void FetchFumigationSummary()
    {
        DataTable dt = GetFumigationData();
        if (dt.Rows.Count > 0)
        {
            gvFumigation.DataSource = dt;
            gvFumigation.DataBind();

            gvFumigation.UseAccessibleHeader = true;
            gvFumigation.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            gvFumigation.DataSource = null;
            gvFumigation.DataBind();
        }
    }

    protected void gvFumigation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 3; i <= 9; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    protected void gvFumigation_DataBound(object sender, EventArgs e)
    {
        if (gvFumigation.Rows.Count > 0)
        {
            Table gridTable = (Table)gvFumigation.Controls[0];
            int[] rTotals = new int[7];
            int[] gTotals = new int[7];

            string lastRegion = gvFumigation.Rows[0].Cells[1].Text.Trim();

            for (int k = 0; k < gvFumigation.Rows.Count; k++)
            {
                GridViewRow currentRow = gvFumigation.Rows[k];
                string currentRegion = currentRow.Cells[1].Text.Trim();

                if (currentRegion != lastRegion)
                {
                    GridViewRow subRow = CreateSummaryRow(lastRegion + " Region Total :", rTotals, "subtotal-row");
                    int itemIndex = gridTable.Rows.GetRowIndex(currentRow);
                    gridTable.Controls.AddAt(itemIndex, subRow);

                    Array.Clear(rTotals, 0, rTotals.Length);
                    lastRegion = currentRegion;
                }

                rTotals[0] += ParseIntegerCell(currentRow.Cells[3]);
                rTotals[1] += ParseIntegerCell(currentRow.Cells[4]);
                rTotals[2] += ParseIntegerCell(currentRow.Cells[5]);
                rTotals[3] += ParseIntegerCell(currentRow.Cells[6]);
                rTotals[4] += ParseIntegerCell(currentRow.Cells[7]);
                rTotals[5] += ParseIntegerCell(currentRow.Cells[8]);
                rTotals[6] += ParseIntegerCell(currentRow.Cells[9]);

                gTotals[0] += ParseIntegerCell(currentRow.Cells[3]);
                gTotals[1] += ParseIntegerCell(currentRow.Cells[4]);
                gTotals[2] += ParseIntegerCell(currentRow.Cells[5]);
                gTotals[3] += ParseIntegerCell(currentRow.Cells[6]);
                gTotals[4] += ParseIntegerCell(currentRow.Cells[7]);
                gTotals[5] += ParseIntegerCell(currentRow.Cells[8]);
                gTotals[6] += ParseIntegerCell(currentRow.Cells[9]);
            }

            GridViewRow finalSubRow = CreateSummaryRow(lastRegion + " Region Total :", rTotals, "subtotal-row");
            gridTable.Controls.Add(finalSubRow);

            GridViewRow grandRow = CreateSummaryRow("Grand Total Summary :", gTotals, "grandtotal-row");
            gridTable.Controls.Add(grandRow);
        }
    }

    private GridViewRow CreateSummaryRow(string title, int[] totals, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        TableCell labelCell = new TableCell { Text = title, ColumnSpan = 3, HorizontalAlign = HorizontalAlign.Right };
        labelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
        row.Cells.Add(labelCell);

        foreach (int val in totals)
        {
            TableCell cell = new TableCell { Text = val.ToString("N0"), HorizontalAlign = HorizontalAlign.Right };
            cell.CssClass = "text-right-align";
            cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0;");
            row.Cells.Add(cell);
        }

        return row;
    }

    private int ParseIntegerCell(TableCell cell)
    {
        int val = 0;
        string clean = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        int.TryParse(clean, out val);
        return val;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = GetFumigationData();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Online_Fumigation_Summary_HO.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();

        // CSS Styles Embedded directly for clean Excel rendering
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:11px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:12px; font-family: 'Segoe UI', Arial; }");
        sb.Append(".text-right { text-align:right !important; mso-number-format:'\\#\\,\\#\\#0'; }");
        sb.Append(".text-center { text-align:center !important; }");
        sb.Append(".subtotal-row td { background-color: #fed7aa !important; font-weight:bold !important; color:#000000 !important; border-top:1px solid #ea580c; border-bottom:1px solid #ea580c; }");
        sb.Append(".grandtotal-row td { background-color: #dbeafe !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        // Custom Corporate Headers
        sb.Append("<table cellspacing='0' cellpadding='5' border='1'>");
        sb.Append("<tr><th colspan='10' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.Append("<tr><th colspan='10' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise Online Fumigation Report Summary (HO)</th></tr>");
        sb.AppendFormat("<tr><td colspan='10' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>");

        // Columns Grid Header
        sb.Append("<tr>");
        sb.Append("<th>S.No.</th><th>Region Name</th><th>District Name</th>");
        sb.Append("<th>Total Godowns Covered</th><th>Total Stack</th><th>Fumigated Till Yesterday</th>");
        sb.Append("<th>Fumigated Today</th><th>Completed Fumigations</th><th>Pending Stack For Fumigation</th>");
        sb.Append("<th>Total Opened Stacks</th>");
        sb.Append("</tr>");

        int[] rTotals = new int[7];
        int[] gTotals = new int[7];
        string lastRegion = dt.Rows[0]["Region_Name"].ToString().Trim();
        int serialNo = 1;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            string currentRegion = row["Region_Name"].ToString().Trim();

            if (currentRegion != lastRegion)
            {
                // Inject Sub-Total for previous region
                AppendExcelSummaryRow(sb, lastRegion + " Region Total :", rTotals, "subtotal-row");
                Array.Clear(rTotals, 0, rTotals.Length);
                lastRegion = currentRegion;
            }

            // Extract values safely
            int godowns = Convert.ToInt32(row["Total_Godowns_Covered"]);
            int stacks = Convert.ToInt32(row["Total_Stack"]);
            int tillYest = Convert.ToInt32(row["Fumigated_Till_Yesterday"]);
            int today = Convert.ToInt32(row["Fumigated_Today"]);
            int comp = Convert.ToInt32(row["Completed_Fumigations"]);
            int pend = Convert.ToInt32(row["Pending Stack For Fumigation"]);
            int opened = Convert.ToInt32(row["Total_Opened_Stacks"]);

            // Accumulate calculations
            rTotals[0] += godowns; rTotals[1] += stacks; rTotals[2] += tillYest; rTotals[3] += today; rTotals[4] += comp; rTotals[5] += pend; rTotals[6] += opened;
            gTotals[0] += godowns; gTotals[1] += stacks; gTotals[2] += tillYest; gTotals[3] += today; gTotals[4] += comp; gTotals[5] += pend; gTotals[6] += opened;

            // Render pure data row
            sb.Append("<tr>");
            sb.AppendFormat("<td class='text-center'>{0}</td>", serialNo++);
            sb.AppendFormat("<td>{0}</td>", row["Region_Name"]);
            sb.AppendFormat("<td>{0}</td>", row["District_Name"]);
            sb.AppendFormat("<td class='text-right'>{0}</td>", godowns);
            sb.AppendFormat("<td class='text-right'>{0}</td>", stacks);
            sb.AppendFormat("<td class='text-right'>{0}</td>", tillYest);
            sb.AppendFormat("<td class='text-right'>{0}</td>", today);
            sb.AppendFormat("<td class='text-right'>{0}</td>", comp);
            sb.AppendFormat("<td class='text-right'>{0}</td>", pend);
            sb.AppendFormat("<td class='text-right'>{0}</td>", opened);
            sb.Append("</tr>");
        }

        // Final Sub Total & Grand Total inside Excel
        AppendExcelSummaryRow(sb, lastRegion + " Region Total :", rTotals, "subtotal-row");
        AppendExcelSummaryRow(sb, "Grand Total Summary :", gTotals, "grandtotal-row");
        sb.Append("</table>");

        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    private void AppendExcelSummaryRow(StringBuilder sb, string title, int[] totals, string cssClass)
    {
        sb.AppendFormat("<tr class='{0}'>", cssClass);
        sb.AppendFormat("<td colspan='3' style='text-align:right; font-weight:bold;'>{0}</td>", title);
        foreach (int val in totals)
        {
            sb.AppendFormat("<td class='text-right' style='font-weight:bold;'>{0}</td>", val);
        }
        sb.Append("</tr>");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Compliance clearance hook */
    }
}