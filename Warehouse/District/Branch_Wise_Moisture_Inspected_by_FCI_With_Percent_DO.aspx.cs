using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Branch_Wise_Moisture_Inspected_by_FCI_With_Percent_DO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Summing accumulators Realigned to Grouping Columns
    int totGdwn = 0, totFciGdwn = 0, totPendGdwn = 0, totMoist = 0, totSentDm = 0, totInspStack = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Depot_DistID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Session["Depot_DistID"] != null)
            {
                string districtID = Session["Depot_DistID"].ToString().Trim();
                BindBranchReport(districtID);
            }
            else
            {
                Response.Redirect("District_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx");
            }
        }
    }

    private DataTable FetchBranchDataset(string districtId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_Branch_Wise_Moisture_Inspected_by_FCI", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id",Session["Depot_DistID"].ToString());
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindBranchReport(string districtId)
    {
        totGdwn = 0; totFciGdwn = 0; totPendGdwn = 0; totMoist = 0; totSentDm = 0; totInspStack = 0;

        DataTable dt = FetchBranchDataset(districtId);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvDetails.UseAccessibleHeader = true;
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            string targetDistrictText = dt.Rows[0]["District Name"].ToString();
            lblScopeHeader.Text = targetDistrictText;
            lblPrintRegion.Text = targetDistrictText;
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // FIXED SUMMING PIPELINE: Correctly mapping values to runtime summary counters
            totGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            totFciGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI_Inspected_Godowns"));
            totPendGdwn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PendingGodownForInspectbyFCI"));
            totMoist += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Moisture"));
            totSentDm += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack_Send_TO_DM_FCI"));
            totInspStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Inspected_Stack"));

            // Text layout format styles loops configuration mapped to match frontend indexes bounds safely
            for (int i = 4; i <= 6; i++)
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

            // Total summary descriptive alignment
            TableCell mainCell = new TableCell { Text = "District Totals Summary :", ColumnSpan = 4 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important; border:1px solid #cbd5e1;");
            grandTotalRow.Cells.Add(mainCell);

            // FIXED GRIDVIEW SUMMARY INJECTION: Appending all variables data dynamically
            grandTotalRow.Cells.Add(CreateSummaryCell(totGdwn.ToString("N0")));
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

    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillToGodown")
        {
            string branchId = e.CommandArgument.ToString().Trim();

            string activeDistId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString() : "0";
            string activeRegId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString() : "0";

            string targetUrl = string.Format("Godown_Wise_Moisture_Inspected_by_FCI_With_Percent_DO.aspx?BranchId={0}",
                Server.UrlEncode(branchId), Server.UrlEncode(activeDistId), Server.UrlEncode(activeRegId));

            string script = string.Format("window.open('{0}', '_blank');", targetUrl);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectToNewTab", script, true);
        }
        else if (e.CommandName == "DrillToPendingGodowns")
        {
            string branchId = e.CommandArgument.ToString().Trim();

            // Formulating dynamic secure URL parameters query sequence
            string targetUrl = string.Format("All_Pending_Godowns_for_FCI_DO.aspx?BranchId={0}", Server.UrlEncode(branchId));

            // JavaScript injection execution framework to force native target blank window load
            string script = string.Format("window.open('{0}', '_blank');", targetUrl);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectToPendingGodownsTab", script, true);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string currentDistId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString() : "0";
        DataTable dt = FetchBranchDataset(currentDistId);
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Branch_Wise_FCI_Moisture_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; mso-number-format:\\#\\,\\#\\#0; } .text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='12' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='12' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>Branch-Wise Moisture Inspection Balance Summary Ledger - Scope: {0}</th></tr>", lblScopeHeader.Text);
        sb.AppendFormat("<tr><td colspan='12' style='text-align:right; font-weight:bold;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='12' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>Region Name</th><th>District Name</th><th>Branch Name</th><th>Total Godowns</th><th>FCI Inspected Godowns</th><th>Pending Godowns for FCI</th><th>Godowns Inspected %</th><th>Total Moisture Stacks</th><th>Moisture Sent To DM/FCI</th><th>FCI Inspected Stacks</th><th>Clearance Percentage %</th></tr>");

        int sNo = 1;
        int tG = 0, tFi = 0, tP = 0, tM = 0, tS = 0, tIs = 0;

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

            sb.AppendFormat("<tr><td class='text-center'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>" +
                            "<td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{7}%</td>" +
                            "<td class='text-right'>{8}</td><td class='text-right'>{9}</td><td class='text-right'>{10}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{11}%</td></tr>",
                            sNo++, r["Region Name"], r["District Name"], r["Branch Name"], g, fi, p, gPct.ToString("F2"), m, s, isStk, sPct.ToString("F2"));
        }

        decimal finalGPct = tG > 0 ? ((decimal)tFi / (decimal)tG) * 100 : 0;
        decimal finalSPct = tS > 0 ? ((decimal)tIs / (decimal)tS) * 100 : 0;

        // FIXED EXCEL FOOTER SUMMARY: Balanced all columns variables cleanly into row summary output block
        sb.Append("<tr class='grandtotal-row'><td colspan='4' style='text-align:right; font-weight:bold;'>District Totals Summary :</td>");
        sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{3}%</td>" +
                        "<td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{7}%</td></tr></table>",
                        tG, tFi, tP, finalGPct.ToString("F2"), tM, tS, tIs, finalSPct.ToString("F2"));

        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}