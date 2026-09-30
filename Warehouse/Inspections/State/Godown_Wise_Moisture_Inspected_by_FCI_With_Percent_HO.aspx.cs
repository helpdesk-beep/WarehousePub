using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Godown_Wise_Moisture_Inspected_by_FCI_With_Percent_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Summing accumulators Realigned to match dynamic Level-3 table matrix structures
    int totGdwn = 0, totFciGdwn = 0, totPendGdwn = 0, totMoist = 0, totSentDm = 0, totInspStack = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["BranchId"] != null)
            {
                string branchID = Request.QueryString["BranchId"].ToString().Trim();
                BindWarehouseReport(branchID);
            }
            else
            {
                string fallbackDistId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString() : "0";
                Response.Redirect("Branch_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx?DistrictId=" + fallbackDistId);
            }
        }
    }

    private DataTable FetchWarehouseDataset(string branchId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_Godown_Wise_Moisture_Inspected_by_FCI", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                int parsedBranchId = 0;
                int.TryParse(branchId, out parsedBranchId);

                cmd.Parameters.AddWithValue("@Branch_Id", parsedBranchId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindWarehouseReport(string branchId)
    {
        totGdwn = 0; totFciGdwn = 0; totPendGdwn = 0; totMoist = 0; totSentDm = 0; totInspStack = 0;

        DataTable dt = FetchWarehouseDataset(branchId);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvDetails.UseAccessibleHeader = true;
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            string targetBranchText = dt.Rows[0]["Branch Name"].ToString();
            lblScopeHeader.Text = targetBranchText;
            lblPrintRegion.Text = targetBranchText;
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            totFciGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI_Inspected_Godowns"));
            totPendGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PendingGodownForInspectbyFCI"));
            totMoist += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Moisture"));
            totSentDm += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack_Send_TO_DM_FCI"));
            totInspStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Inspected_Stack"));

            // Numerical alignments logic layout sync
            for (int i = 5; i <= 6; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
            e.Row.Cells[7].Attributes.Add("style", "text-align:right !important; font-weight:bold; mso-number-format:'0.00%';");

            for (int i = 8; i <= 10; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
            e.Row.Cells[11].Attributes.Add("style", "text-align:right !important; font-weight:bold; mso-number-format:'0.00%';");
        }
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "Warehouse Totals Summary :", ColumnSpan = 5 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important; border:1px solid #cbd5e1;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(CreateSummaryCell(totFciGdwn.ToString("N0")));
            grandTotalRow.Cells.Add(CreateSummaryCell(totPendGdwn.ToString("N0")));

            decimal totalGdnPerc = totGdwn > 0 ? ((decimal)totFciGdwn / (decimal)totGdwn) * 100 : 0;
            grandTotalRow.Cells.Add(CreateSummaryCell(totalGdnPerc.ToString("0.00") + "%"));

            grandTotalRow.Cells.Add(CreateSummaryCell(totMoist.ToString("N0")));
            grandTotalRow.Cells.Add(CreateSummaryCell(totSentDm.ToString("N0")));
            grandTotalRow.Cells.Add(CreateSummaryCell(totInspStack.ToString("N0")));

            decimal totalStackPerc = totSentDm > 0 ? ((decimal)totInspStack / (decimal)totSentDm) * 100 : 0;
            grandTotalRow.Cells.Add(CreateSummaryCell(totalStackPerc.ToString("0.00") + "%"));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell CreateSummaryCell(string textDisplay)
    {
        TableCell cell = new TableCell { Text = textDisplay, HorizontalAlign = HorizontalAlign.Right };
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important; border:1px solid #cbd5e1;");
        return cell;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string branchIdParam = Request.QueryString["BranchId"] != null ? Request.QueryString["BranchId"].ToString() : "0";
        DataTable dt = FetchWarehouseDataset(branchIdParam);
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Level_FCI_Moisture_Report.xls");
        Response.ContentType = "application/vnd.ms-excel"; Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; mso-number-format:\\#\\,\\#\\#0; } .text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='12' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='12' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>Godown-Wise Stacks Moisture Clearance Balance Sheet - Center Scope: {0}</th></tr>", lblScopeHeader.Text);
        sb.AppendFormat("<tr><td colspan='12' style='text-align:right; font-weight:bold;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='12' style='border:none;'>&nbsp;</td></tr>");

        // EXACT COLUMNS MATCH: Built precisely according to level-3 structure matrix blocks
        sb.Append("<tr><th>S.No.</th><th>Region Name</th><th>District Name</th><th>Branch Name</th><th>Godown Name</th><th>FCI Inspected Godowns</th><th>Pending Godowns for FCI</th><th>Godowns Inspected %</th><th>Total Moisture Stacks</th><th>Moisture Sent To DM/FCI</th><th>FCI Inspected Stacks</th><th>Clearance Percentage %</th></tr>");

        int sNo = 1; int tG = 0, tFi = 0, tP = 0, tM = 0, tS = 0, tIs = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow r = dt.Rows[i];
            int g = Convert.ToInt32(r["Total_Godown"]);
            int fi = Convert.ToInt32(r["FCI_Inspected_Godowns"]);
            int p = Convert.ToInt32(r["PendingGodownForInspectbyFCI"]);
            int m = Convert.ToInt32(r["Total_Moisture"]);
            int s = Convert.ToInt32(r["Total_Stack_Send_TO_DM_FCI"]);
            int isStk = Convert.ToInt32(r["Total_Inspected_Stack"]);

            tG += g; tFi += fi; tP += p; tM += m; tS += s; tIs += isStk;
            decimal gPct = Convert.ToDecimal(r["GodownPendingPercantage"]);
            decimal sPct = Convert.ToDecimal(r["Percantage"]);

            // FIXED ROW INDEX FORMATTER: Maps 12 precise sequence index layout columns cleanly to completely get rid of exceptions
            sb.AppendFormat("<tr><td class='text-center'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td>" +
                            "<td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{7}%</td>" +
                            "<td class='text-right'>{8}</td><td class='text-right'>{9}</td><td class='text-right'>{10}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{11}%</td></tr>",
                            sNo++, r["Region Name"], r["District Name"], r["Branch Name"], r["Godown Name"], fi, p, gPct.ToString("F2"), m, s, isStk, sPct.ToString("F2"));
        }

        decimal finalGPct = tG > 0 ? ((decimal)tFi / (decimal)tG) * 100 : 0;
        decimal finalSPct = tS > 0 ? ((decimal)tIs / (decimal)tS) * 100 : 0;

        // FIXED FOOTER ROW STRUCTURE
        sb.Append("<tr class='grandtotal-row'><td colspan='5' style='text-align:right; font-weight:bold;'>Warehouse Totals Summary :</td>");
        sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{2}%</td>" +
                        "<td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{6}%</td></tr></table>",
                        tFi, tP, finalGPct.ToString("F2"), tM, tS, tIs, finalSPct.ToString("F2"));

        Response.Write(sb.ToString()); Response.Flush(); Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}