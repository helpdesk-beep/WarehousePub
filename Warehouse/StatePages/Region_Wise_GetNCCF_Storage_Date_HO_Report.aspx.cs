using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Region_Wise_GetNCCF_Storage_Date_HO_Report : System.Web.UI.Page
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
            if (Request.QueryString["RegID"] != null && Request.QueryString["RegName"] != null)
            {
                lblRegionTitle.Text = Request.QueryString["RegName"].ToString().Trim();
                lblPrintRegion.Text = lblRegionTitle.Text;
                lblDates.Text = Request.QueryString["From"] + " To " + Request.QueryString["To"];

                BindRegionData();
            }
            else
            {
                Response.Redirect("GetNCCF_Storage_Date_HO_Report.aspx");
            }
        }
    }

    private DataTable FetchLevel2Dataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetNCCF_Storage_Level2_Region_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", Request.QueryString["From"]);
                cmd.Parameters.AddWithValue("@ToDate", Request.QueryString["To"]);
                cmd.Parameters.AddWithValue("@RegionID", Request.QueryString["RegID"]);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindRegionData()
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

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("GetNCCF_Storage_Date_HO_Report.aspx");
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 2; i <= 19; i++)
            {
                string styleRule = (i == 2 || i == 3 || i == 5 || i == 7 || i == 9 || i == 11 || i == 13 || i == 16 || i == 18)
                    ? "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;"
                    : "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;";
                e.Row.Cells[i].Attributes.Add("style", styleRule);
            }
        }
    }

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
                    for (int i = 0; i < 18; i++) { dynamicSums[i] += ParseCellNumericValue(currentRow.Cells[i + 2]); }
                }
            }

            GridViewRow grandRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandRow.CssClass = "grandtotal-row"; grandRow.Font.Bold = true;

            TableCell labelCell = new TableCell { Text = "Regional Grand Total Summary :", ColumnSpan = 2, HorizontalAlign = HorizontalAlign.Right };
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
        decimal output = 0; string cleanString = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        if (string.IsNullOrEmpty(cleanString) && cell.Controls.Count > 0)
        {
            Control ctrl = cell.Controls[0];
            if (ctrl is LinkButton) cleanString = ((LinkButton)ctrl).Text.Replace(",", "").Trim();
        }
        decimal.TryParse(cleanString, out output); return output;
    }

    // FIXED NAVIGATION PROCESSOR: Redirection rule handles and passes safe parameters context to Layer 3 breakdown sheet
    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillToBranch")
        {
            string[] args = e.CommandArgument.ToString().Split('|');
            string districtID = args[0];
            string districtName = args[1];

            string regID = Request.QueryString["RegID"];
            string regName = Request.QueryString["RegName"];
            string fDate = Request.QueryString["From"];
            string tDate = Request.QueryString["To"];

            Response.Redirect(string.Format("District_Wise_GetNCCF_Storage_Date_HO_Report.aspx?DistID={0}&DistName={1}&RegID={2}&RegName={3}&From={4}&To={5}",
                Server.UrlEncode(districtID), Server.UrlEncode(districtName), Server.UrlEncode(regID), Server.UrlEncode(regName), Server.UrlEncode(fDate), Server.UrlEncode(tDate)));
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchLevel2Dataset();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_Regional_District_Report.xls");
        Response.ContentType = "application/vnd.ms-excel"; Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        // FIXED EXCEL PALETTE: Synchronized color strings with Corporate Deep Blue theme properties
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; } .text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #dbeafe !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='20' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='20' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>NCCF Storage Charges Regional Summary - Active Region: {0}</th></tr>", lblRegionTitle.Text);
        sb.AppendFormat("<tr><td colspan='20' style='text-align:right; font-weight:bold;'>Period Scope: {0} | Generated On: {1}</td></tr>", lblDates.Text, DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='20' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>District Name</th>");
        sb.Append("<th>Godown Count</th><th>Generated Bills Count</th><th>Generated Bill Amt</th><th>Submitted Bills Count</th><th>Submitted Bill Amt</th>");
        sb.Append("<th>Pending Bills at Branch Count</th><th>Pending Bill Amt at Branch</th><th>RM Submitted Count</th><th>RM Submitted Amt</th>");
        sb.Append("<th>Pending Bills at RM Count</th><th>Pending Bill Amt at RM</th><th>NCCF Received Count</th><th>NCCF Received Amt</th>");
        sb.Append("<th>NCCF Deduction Amt</th><th>Total Pending Count at NCCF</th><th>Total Pending Amt at NCCF</th><th>PTG Bill Payment Count</th><th>PTG Bill Payment Amt</th></tr>");

        decimal[] totals = new decimal[18]; int sNo = 1;
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

            sb.Append("<tr><td class='text-center'>" + sNo++ + "</td><td>" + row["Display_Name"] + "</td>");
            for (int idx = 0; idx < 18; idx++)
            {
                bool isInt = (idx == 0 || idx == 1 || idx == 3 || idx == 5 || idx == 7 || idx == 9 || idx == 11 || idx == 14 || idx == 16);
                string fmt = isInt ? "F0" : "F2";
                string mso = isInt ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
                sb.AppendFormat("<td class='text-right' style='{0}'>{1}</td>", mso, rowVals[idx].ToString(fmt));
            }
            sb.Append("</tr>");
        }

        sb.Append("<tr class='grandtotal-row'><td colspan='2' style='text-align:right; font-weight:bold;'>Regional Grand Total Summary :</td>");
        for (int i = 0; i < 18; i++)
        {
            bool isInt = (i == 0 || i == 1 || i == 3 || i == 5 || i == 7 || i == 9 || i == 11 || i == 14 || i == 16);
            string fmt = isInt ? "F0" : "F2";
            string mso = isInt ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";
            sb.AppendFormat("<td class='text-right' style='font-weight:bold; {0}'>{1}</td>", mso, totals[i].ToString(fmt));
        }
        sb.Append("</tr></table>");
        Response.Write(sb.ToString()); Response.Flush(); Response.End();
    }
}