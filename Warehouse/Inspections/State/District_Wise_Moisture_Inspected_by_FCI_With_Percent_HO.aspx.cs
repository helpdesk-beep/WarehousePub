using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_Moisture_Inspected_by_FCI_With_Percent_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Dynamic State Level Summary Counters Realignment
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
            if (Request.QueryString["RegID"] != null)
            {
                string regionID = Request.QueryString["RegID"].ToString().Trim();
                string regionName = (regionID == "0") ? "All Combined Regions" : "Region Code Context: " + regionID;

                lblScopeHeader.Text = regionName;
                lblPrintRegion.Text = regionName;

                BindDistrictReport(regionID);
            }
            else
            {
                Response.Redirect("Region_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx");
            }
        }
    }

    private DataTable FetchDistrictDataset(string regionId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("rpt_District_Wise_Moisture_Inspected_by_FCI_With_Percent", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                int parsedId = 0;
                int.TryParse(regionId, out parsedId);

                cmd.Parameters.AddWithValue("@Region_Id", parsedId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindDistrictReport(string regionId)
    {
        totGdwn = 0; totFciGdwn = 0; totPendGdwn = 0; totMoist = 0; totSentDm = 0; totInspStack = 0;

        DataTable dt = FetchDistrictDataset(regionId);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvDetails.UseAccessibleHeader = true;
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            string datasetRegionName = dt.Rows[0]["Region Name"].ToString();
            lblScopeHeader.Text = "Active Region Group: " + datasetRegionName;
            lblPrintRegion.Text = datasetRegionName;
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Region_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx");
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

            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                string alignmentStyle = (i == 6 || i == 10) ? "text-align:right !important; font-weight:bold; mso-number-format:'0.00%';" : "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;";
                e.Row.Cells[i].Attributes.Add("style", alignmentStyle);
            }
        }
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "Regional Grand Total Summary :", ColumnSpan = 3 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important; border:1px solid #cbd5e1;");
            grandTotalRow.Cells.Add(mainCell);

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
        if (e.CommandName == "DrillToBranch")
        {
            string districtId = e.CommandArgument.ToString().Trim();
            string activeRegId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString().Trim() : "0";

            // FIXED PARAMETERS FORMAT: Added both placeholders {0} and {1} for strict sequential mapping
            string targetUrl = string.Format("Branch_Wise_Moisture_Inspected_by_FCI_With_Percent_HO.aspx?DistrictId={0}",
                Server.UrlEncode(districtId), Server.UrlEncode(activeRegId));

            // FIXED FOR TARGET BLANK: Client-side window.open injection via ScriptManager
            string script = string.Format("window.open('{0}', '_blank');", targetUrl);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectToLevel3NewTab", script, true);
        }
        else if (e.CommandName == "DrillToPendingGodowns")
        {
            string districtId = e.CommandArgument.ToString().Trim();

            // Formulating dynamic secure URL parameters query sequence
            string targetUrl = string.Format("All_Pending_Godowns_for_FCI_HO.aspx?DistrictId={0}", Server.UrlEncode(districtId));

            // JavaScript injection execution framework to force native target blank window load
            string script = string.Format("window.open('{0}', '_blank');", targetUrl);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectToPendingGodownsTab", script, true);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string currentRegId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString() : "0";
        DataTable dt = FetchDistrictDataset(currentRegId);
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Level_FCI_Moisture_Report.xls");
        Response.ContentType = "application/vnd.ms-excel"; Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; mso-number-format:\\#\\,\\#\\#0; } .text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        // FIXED EXCEL STREAM: Swapped out unsafe AppendFormat methods on static layout tags with pure sb.Append
        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='11' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='11' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>District-Wise Moisture Inspection & FCI Clearance Breakdowns - Scope: {0}</th></tr>", lblScopeHeader.Text);
        sb.AppendFormat("<tr><td colspan='11' style='text-align:right; font-weight:bold;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='11' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>Region Name</th><th>District Name</th><th>Total Godowns</th><th>FCI Inspected Godowns</th><th>Pending Godowns for FCI</th><th>Godown Pending %</th><th>Total Moisture Stacks</th><th>Moisture Sent To DM/FCI</th><th>FCI Inspected Stacks</th><th>Inspection Clearance %</th></tr>");

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

            // FIXED DATA ROW INJECTOR: Re-arranged indexes array sequence to strictly pulling up data parameters cleanly
            sb.AppendFormat("<tr><td class='text-center'>{0}</td><td>{1}</td><td>{2}</td>" +
                            "<td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{6}%</td>" +
                            "<td class='text-right'>{7}</td><td class='text-right'>{8}</td><td class='text-right'>{9}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{10}%</td></tr>",
                            sNo++, r["Region Name"], r["District Name"], g, fi, p, gPct.ToString("F2"), m, s, isStk, sPct.ToString("F2"));
        }

        decimal finalGPct = tG > 0 ? ((decimal)tFi / (decimal)tG) * 100 : 0;
        decimal finalSPct = tS > 0 ? ((decimal)tIs / (decimal)tS) * 100 : 0;

        // FIXED GRAND TOTALS SHEET: Fixed missing indexes framework layout mismatch
        sb.Append("<tr class='grandtotal-row'><td colspan='3' style='text-align:right; font-weight:bold;'>Regional Grand Total Summary :</td>");
        sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{3}%</td>" +
                        "<td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{7}%</td></tr></table>",
                        tG, tFi, tP, finalGPct.ToString("F2"), tM, tS, tIs, finalSPct.ToString("F2"));

        Response.Write(sb.ToString()); Response.Flush(); Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}