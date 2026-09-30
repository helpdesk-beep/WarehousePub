using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Region_Wise_GetRent_Storage_Date_HO_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null) { Response.Redirect("~/Login.aspx"); return; }
        if (!IsPostBack)
        {
            if (Request.QueryString["RegID"] != null && Request.QueryString["RegName"] != null)
            {
                lblRegionTitle.Text = Request.QueryString["RegName"].ToString().Trim();
                lblPrintRegion.Text = lblRegionTitle.Text;
                lblDates.Text = Request.QueryString["From"] + " To " + Request.QueryString["To"];
                BindRegionReportData();
            }
            else { Response.Redirect("GetRent_Date_HO_Report.aspx"); }
        }
    }

    private DataTable FetchLevel2Dataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetRent_Level2_Region_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", Request.QueryString["From"]);
                cmd.Parameters.AddWithValue("@ToDate", Request.QueryString["To"]);
                cmd.Parameters.AddWithValue("@RegionID", Request.QueryString["RegID"]);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex) { Response.Write("<script>alert('Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>"); }
        return dt;
    }

    private void BindRegionReportData()
    {
        DataTable dt = FetchLevel2Dataset();
        gvReport.DataSource = dt;
        gvReport.DataBind();
        if (dt.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e) { Response.Redirect("GetRent_Date_HO_Report.aspx"); }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillToBranch")
        {
            string[] args = e.CommandArgument.ToString().Split('|');
            Response.Redirect(string.Format("District_Wise_GetRent_Storage_Date_HO_Report.aspx?DistID={0}&DistName={1}&RegID={2}&RegName={3}&From={4}&To={5}",
                Server.UrlEncode(args[0]), Server.UrlEncode(args[1]), Server.UrlEncode(Request.QueryString["RegID"]), Server.UrlEncode(Request.QueryString["RegName"]), Server.UrlEncode(Request.QueryString["From"]), Server.UrlEncode(Request.QueryString["To"])));
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            for (int i = 3; i <= 7; i++) { e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;"); }
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table gridTable = (Table)gvReport.Controls[0];
            decimal[] dynamicSums = new decimal[6];
            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < 6; i++) { dynamicSums[i] += ParseCellNumericValue(currentRow.Cells[i + 2]); }
                }
            }
            GridViewRow grandRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandRow.CssClass = "grandtotal-row"; grandRow.Font.Bold = true;
            TableCell labelCell = new TableCell { Text = "Regional Grand Total Summary :", ColumnSpan = 2, HorizontalAlign = HorizontalAlign.Right };
            labelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
            grandRow.Cells.Add(labelCell);
            for (int idx = 0; idx < 6; idx++)
            {
                string formatStr = (idx == 0) ? "N0" : "N2";
                string msoFormat = (idx == 0) ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
                TableCell cell = new TableCell { Text = dynamicSums[idx].ToString(formatStr), HorizontalAlign = HorizontalAlign.Right };
                cell.CssClass = "text-right-align"; cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; " + msoFormat);
                grandRow.Cells.Add(grandRow.Cells.Count > 0 ? cell : cell);
            }
            gridTable.Rows.Add(grandRow);
        }
        base.Render(writer);
    }

    private decimal ParseCellNumericValue(TableCell cell)
    {
        decimal output = 0; string cleanString = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        if (string.IsNullOrEmpty(cleanString) && cell.Controls.Count > 0)
        {
            Control ctrl = cell.Controls[0];
            if (ctrl is LinkButton) cleanString = ((LinkButton)ctrl).Text.Replace(",", "").Trim();
        }
        decimal.TryParse(cleanString, out output); return output;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchLevel2Dataset(); if (dt == null || dt.Rows.Count == 0) return;
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_Regional_Rent_Report.xls");
        Response.ContentType = "application/vnd.ms-excel"; Response.Charset = "";
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:11px; } td { border:1px solid #cbd5e1; font-size:12px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; } .text-center { text-align:center !important; } .grandtotal-row td { background-color: #dbeafe !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }</style>");
        sb.Append("<table cellspacing='0' cellpadding='4' border='1'><tr><th colspan='8' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr><tr><th colspan='8' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>NCCF Storage Charges Regional Summary - Active Region: " + lblRegionTitle.Text + "</th></tr><tr><td colspan='8' style='text-align:right; font-weight:bold;'>Period Scope: " + lblDates.Text + " | Generated On: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "</td></tr><tr><td colspan='8' style='border:none;'>&nbsp;</td></tr><tr><th>S.No.</th><th>District Name</th><th>Godown Count</th><th>Rent Bill Amt</th><th>Received Rent Bill Amount</th><th>Pending at MPSCSC</th><th>Pay to Godown Owner</th><th>Pending at MPWLC</th></tr>");
        decimal[] totals = new decimal[6]; int sNo = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            decimal[] rowVals = new decimal[] { Convert.ToDecimal(row["noofgdwn"]), Convert.ToDecimal(row["RentBillAmt"]), Convert.ToDecimal(row["ReceivedRentBillAmount"]), Convert.ToDecimal(row["PendingatMPSCSC"]), Convert.ToDecimal(row["PaytoGodownOwner"]), Convert.ToDecimal(row["PendingatMPWLC"]) };
            for (int j = 0; j < 6; j++) totals[j] += rowVals[j];
            sb.Append("<tr><td class='text-center'>" + sNo++ + "</td><td>" + row["Display_Name"] + "</td>");
            for (int idx = 0; idx < 6; idx++) { sb.AppendFormat("<td class='text-right' style='{0}'>{1}</td>", (idx == 0) ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;", rowVals[idx].ToString((idx == 0) ? "F0" : "F2")); }
            sb.Append("</tr>");
        }
        sb.Append("<tr class='grandtotal-row'><td colspan='2' style='text-align:right; font-weight:bold;'>Regional Grand Total Summary :</td>");
        for (int i = 0; i < 6; i++) { sb.AppendFormat("<td class='text-right' style='font-weight:bold; {0}'>{1}</td>", (i == 0) ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;", totals[i].ToString((i == 0) ? "F0" : "F2")); }
        sb.Append("</tr></table>"); Response.Write(sb.ToString()); Response.Flush(); Response.End();
    }
}