using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_NCCF_StorageBill_Settlement_Summary : System.Web.UI.Page
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
            txtFromDate.Text = "01/04/2025";
            txtToDate.Text = "31/03/2026";
            BindAdaptiveLedger();
        }
    }

    private string GetFormattedDateString(string inputDate, string defaultDate)
    {
        if (string.IsNullOrEmpty(inputDate)) return defaultDate;
        try
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy" };
            DateTime parsedDate = DateTime.ParseExact(inputDate.Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return parsedDate.ToString("dd/MM/yyyy");
        }
        catch
        {
            return defaultDate;
        }
    }

    private DataTable FetchDrillDownDataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetNCCF_StorageBill_DrillDown_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Level", Convert.ToInt32(hfLevel.Value));
                cmd.Parameters.AddWithValue("@FromDate", GetFormattedDateString(txtFromDate.Text, "01/04/2025"));
                cmd.Parameters.AddWithValue("@ToDate", GetFormattedDateString(txtToDate.Text, "31/03/2026"));
                cmd.Parameters.AddWithValue("@RegionID", string.IsNullOrEmpty(hfRegionID.Value) ? (object)DBNull.Value : hfRegionID.Value);
                cmd.Parameters.AddWithValue("@DistrictID", string.IsNullOrEmpty(hfDistrictID.Value) ? (object)DBNull.Value : hfDistrictID.Value);
                cmd.Parameters.AddWithValue("@BranchID", string.IsNullOrEmpty(hfBranchID.Value) ? (object)DBNull.Value : hfBranchID.Value);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault Log Trace: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindAdaptiveLedger()
    {
        DataTable dt = FetchDrillDownDataset();
        gvReport.DataSource = dt;
        gvReport.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        lnkResetHO.Visible = (Convert.ToInt32(hfLevel.Value) > 1);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAdaptiveLedger();
    }

    protected void lnkResetHO_Click(object sender, EventArgs e)
    {
        hfLevel.Value = "1";
        hfRegionID.Value = "";
        hfDistrictID.Value = "";
        hfBranchID.Value = "";
        lblCurrentScope.Text = "State Wide Region Overview";
        BindAdaptiveLedger();
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillDown")
        {
            string[] args = e.CommandArgument.ToString().Split('|');
            string targetID = args[0];
            string displayName = args[1];
            int currentLevel = Convert.ToInt32(hfLevel.Value);

            if (currentLevel == 1)
            {
                hfRegionID.Value = targetID;
                hfLevel.Value = "2";
                lblCurrentScope.Text = "Region: " + displayName + " -> District breakdown";
            }
            else if (currentLevel == 2)
            {
                hfDistrictID.Value = targetID;
                hfLevel.Value = "3";
                lblCurrentScope.Text = "District: " + displayName + " -> Branch breakdown";
            }
            else if (currentLevel == 3)
            {
                hfBranchID.Value = targetID;
                hfLevel.Value = "4";
                lblCurrentScope.Text = "Branch: " + displayName + " -> Godown detailed ledger";
            }

            BindAdaptiveLedger();
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Node scale hatne ke baad numeric index 2 se lekar 19 tak realign ho gaya hai
            for (int i = 2; i <= 19; i++)
            {
                string styleRule = (i == 2 || i == 3 || i == 5 || i == 7 || i == 9 || i == 11 || i == 13 || i == 16 || i == 18)
                    ? "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;"
                    : "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;";
                e.Row.Cells[i].Attributes.Add("style", styleRule);
            }

            if (hfLevel.Value == "4")
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkDrill");
                Label lbl = (Label)e.Row.FindControl("lblPlain");
                if (lnk != null && lbl != null)
                {
                    lnk.Visible = false;
                    lbl.Visible = true;
                }
            }
        }
    }

    // RUNTIME INJECTION: Appends consolidated grid Grand Total summary row perfectly
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table gridTable = (Table)gvReport.Controls[0];
            decimal[] dynamicSums = new decimal[18];

            for (int k = 1; k < gridTable.Rows.Count; k++)
            {
                GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
                if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < 18; i++)
                    {
                        dynamicSums[i] += ParseCellNumericValue(currentRow.Cells[i + 2]);
                    }
                }
            }

            GridViewRow grandRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandRow.CssClass = "grandtotal-row";
            grandRow.Font.Bold = true;

            // Node scale column hatane se ColumnSpan ab text columns ke liye perfectly "2" ho gaya hai
            TableCell labelCell = new TableCell { Text = "State Grand Total Summary :", ColumnSpan = 2, HorizontalAlign = HorizontalAlign.Right };
            labelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
            grandRow.Cells.Add(labelCell);

            for (int idx = 0; idx < 18; idx++)
            {
                bool isIntType = (idx == 0 || idx == 1 || idx == 3 || idx == 5 || idx == 7 || idx == 9 || idx == 11 || idx == 14 || idx == 16);
                string formatStr = isIntType ? "N0" : "N2";
                string msoFormat = isIntType ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";

                TableCell cell = new TableCell { Text = dynamicSums[idx].ToString(formatStr), HorizontalAlign = HorizontalAlign.Right };
                cell.CssClass = "text-right-align";
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; " + msoFormat);
                grandRow.Cells.Add(cell);
            }

            gridTable.Rows.Add(grandRow);
        }
        base.Render(writer);
    }

    private decimal ParseCellNumericValue(TableCell cell)
    {
        decimal output = 0;
        string cleanString = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        if (string.IsNullOrEmpty(cleanString) && cell.Controls.Count > 0)
        {
            Control ctrl = cell.Controls[0];
            if (ctrl is LinkButton) cleanString = ((LinkButton)ctrl).Text.Replace(",", "").Trim();
            else if (ctrl is Label) cleanString = ((Label)ctrl).Text.Replace(",", "").Trim();
        }
        decimal.TryParse(cleanString, out output);
        return output;
    }

    // BULLETPROOF DIRECT EXCEL RENDER PIPE WITHOUT "NODE SCALE" COLUMN
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchDrillDownDataset();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_DrillDown_StorageCharges_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; }");
        sb.Append(".text-right { text-align:right !important; }");
        sb.Append(".text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #dbeafe !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='20' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='20' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>NCCF Storage Charges Monitoring Drilldown - Scope: {0}</th></tr>", lblCurrentScope.Text);
        sb.AppendFormat("<tr><td colspan='20' style='text-align:right; font-weight:bold;'>Date Range Constraints: {0} to {1} | Generated On: {2}</td></tr>", txtFromDate.Text, txtToDate.Text, DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='20' style='border:none;'>&nbsp;</td></tr>");

        // Excel Headers matched to exact Grid Columns
        sb.Append("<tr><th>S.No.</th><th>Target Node Scale Location Desc</th>");
        sb.Append("<th>Godown Count</th><th>Generated Bills Count</th><th>Generated Bill Amt</th><th>Submitted Bills Count</th><th>Submitted Bill Amt</th>");
        sb.Append("<th>Pending Bills at Branch Count</th><th>Pending Bill Amt at Branch</th><th>RM Submitted Count</th><th>RM Submitted Amt</th>");
        sb.Append("<th>Pending Bills at RM Count</th><th>Pending Bill Amt at RM</th><th>NCCF Received Count</th><th>NCCF Received Amt</th>");
        sb.Append("<th>NCCF Deduction Amt</th><th>Total Pending Count at NCCF</th><th>Total Pending Amt at NCCF</th><th>PTG Bill Payment Count</th><th>PTG Bill Payment Amt</th></tr>");

        decimal[] totals = new decimal[18];
        int sNo = 1;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            decimal[] rowVals = new decimal[] {
                Convert.ToDecimal(row["noofgdwn"]), Convert.ToDecimal(row["NoOfGenerateBill"]), Convert.ToDecimal(row["BillAmt"]),
                Convert.ToDecimal(row["NoOfSUBBill"]), Convert.ToDecimal(row["SUBBillAmt"]), Convert.ToDecimal(row["PendingBillForSubmisionatBranch"]),
                Convert.ToDecimal(row["PendingBillAmountForSubmision"]), Convert.ToDecimal(row["RMsubmitbilltonccf"]), Convert.ToDecimal(row["RMsubmitbillAmttonccf"]),
                Convert.ToDecimal(row["PendingBillForSubmisionatRM"]), Convert.ToDecimal(row["PendingBillAmountForSubmisionatRM"]), Convert.ToDecimal(row["NoofbillPaymentReceivedFromNCCF"]),
                Convert.ToDecimal(row["NoofbillPaymentAmountReceivedFromNCCF"]), Convert.ToDecimal(row["PaymentDecuctionbyNCCF"]), Convert.ToDecimal(row["TotalNoofPendingBillatNCCF"]),
                Convert.ToDecimal(row["TotalNoofPendingBillAmountatNCCF"]), Convert.ToDecimal(row["NoOfBillPayment"]), Convert.ToDecimal(row["BillAmtPTG"])
            };

            for (int j = 0; j < 18; j++) totals[j] += rowVals[j];

            sb.Append("<tr>");
            sb.AppendFormat("<td class='text-center'>{0}</td>", sNo++);
            sb.AppendFormat("<td>{0}</td>", row["Display_Name"]);

            for (int idx = 0; idx < 18; idx++)
            {
                bool isInt = (idx == 0 || idx == 1 || idx == 3 || idx == 5 || idx == 7 || idx == 9 || idx == 11 || idx == 14 || idx == 16);
                string fmt = isInt ? "F0" : "F2";
                string mso = isInt ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
                sb.AppendFormat("<td class='text-right' style='{0}'>{1}</td>", mso, rowVals[idx].ToString(fmt));
            }
            sb.Append("</tr>");
        }

        // Grand Total row inside excel
        sb.Append("<tr class='grandtotal-row'>");
        sb.Append("<td colspan='2' style='text-align:right; font-weight:bold;'>State Grand Total Summary :</td>");
        for (int i = 0; i < 18; i++)
        {
            bool isInt = (i == 0 || i == 1 || i == 3 || i == 5 || i == 7 || i == 9 || i == 11 || i == 14 || i == 16);
            string fmt = isInt ? "F0" : "F2";
            string mso = isInt ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
            sb.AppendFormat("<td class='text-right' style='font-weight:bold; {0}'>{1}</td>", mso, totals[i].ToString(fmt));
        }
        sb.Append("</tr></table>");

        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verification bypass tracker mapping clear */
    }
}