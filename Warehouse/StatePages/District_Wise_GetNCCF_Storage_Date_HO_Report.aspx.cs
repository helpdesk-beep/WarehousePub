using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_District_Wise_GetNCCF_Storage_Date_HO_Report : System.Web.UI.Page
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
            // Verifying the parameter injection routes
            if (Request.QueryString["DistID"] != null && Request.QueryString["RegID"] != null)
            {
                lblDistrictTitle.Text = Request.QueryString["DistName"].ToString().Trim();
                lblPrintDistrict.Text = lblDistrictTitle.Text;

                lblRegionScope.Text = Request.QueryString["RegName"].ToString().Trim();
                lblPrintRegion.Text = lblRegionScope.Text;

                lblDates.Text = Request.QueryString["From"] + " To " + Request.QueryString["To"];

                BindDistrictWiseBranches();
            }
            else
            {
                Response.Redirect("GetNCCF_Storage_Date_HO_Report.aspx");
            }
        }
    }

    private DataTable FetchLevel3Dataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetNCCF_Storage_Level3_District_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", Request.QueryString["From"]);
                cmd.Parameters.AddWithValue("@ToDate", Request.QueryString["To"]);
                cmd.Parameters.AddWithValue("@DistrictID", Request.QueryString["DistID"]);

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

    private void BindDistrictWiseBranches()
    {
        DataTable dt = FetchLevel3Dataset();
        gvReport.DataSource = dt;
        gvReport.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrillToGodown")
        {
            string[] args = e.CommandArgument.ToString().Split('|');
            string branchID = args[0];
            string branchName = args[1];

            // Pipeline extraction parameters chain from url
            string distID = Request.QueryString["DistID"];
            string distName = Request.QueryString["DistName"];
            string regID = Request.QueryString["RegID"];
            string regName = Request.QueryString["RegName"];
            string fDate = Request.QueryString["From"];
            string tDate = Request.QueryString["To"];

            // REDIRECT COMMAND: Moves cleanly to final Level 4 detailed master registry page context
            Response.Redirect(string.Format("Branch_Wise_GetNCCF_Storage_Date_HO_Report.aspx?BranchID={0}&BranchName={1}&DistID={2}&DistName={3}&RegID={4}&RegName={5}&From={6}&To={7}",
                Server.UrlEncode(branchID), Server.UrlEncode(branchName), Server.UrlEncode(distID), Server.UrlEncode(distName), Server.UrlEncode(regID), Server.UrlEncode(regName), Server.UrlEncode(fDate), Server.UrlEncode(tDate)));
        }
    }

    protected void lnkBackToRegion_Click(object sender, EventArgs e)
    {
        // Safe tracking rollback back to Region page context
        Response.Redirect(string.Format("Region_Wise_GetNCCF_Storage_Date_HO_Report.aspx?RegID={0}&RegName={1}&From={2}&To={3}",
            Server.UrlEncode(Request.QueryString["RegID"]), Server.UrlEncode(Request.QueryString["RegName"]),
            Server.UrlEncode(Request.QueryString["From"]), Server.UrlEncode(Request.QueryString["To"])));
    }

    protected void lnkBackToHO_Click(object sender, EventArgs e)
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

            TableCell labelCell = new TableCell { Text = "District Grand Total Summary :", ColumnSpan = 2, HorizontalAlign = HorizontalAlign.Right };
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
        decimal.TryParse(cleanString, out output); return output;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchLevel3Dataset();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_District_Branch_Report.xls");
        Response.ContentType = "application/vnd.ms-excel"; Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#0f766e !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #115e59; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; } .text-center { text-align:center !important; }");
        sb.Append(".grandtotal-row td { background-color: #ccfbf1 !important; font-weight:bold !important; color:#115e59 !important; border-top:2px solid #0f766e; border-bottom:2px solid #115e59; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='20' style='font-size:16pt; background-color:#0f766e; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='20' style='font-size:12pt; background-color:#f1f5f9; color:#115e59;'>NCCF Storage Charges District Wise Summary - Active District: {0}</th></tr>", lblDistrictTitle.Text);
        sb.AppendFormat("<tr><td colspan='20' style='text-align:right; font-weight:bold;'>Period Scope: {0} | Generated On: {1}</td></tr>", lblDates.Text, DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='20' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>Branch Name</th>");
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

        sb.Append("<tr class='grandtotal-row'><td colspan='2' style='text-align:right; font-weight:bold;'>District Grand Total Summary :</td>");
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

    public override void VerifyRenderingInServerForm(Control control) { }
}