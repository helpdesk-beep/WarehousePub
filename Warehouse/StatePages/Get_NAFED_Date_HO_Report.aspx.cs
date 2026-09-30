using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Get_NAFED_Date_HO_Report : System.Web.UI.Page
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
            BindLedgerAbstract();
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
        catch { return defaultDate; }
    }

    private DataTable FetchAbstractDataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("Get_NAFED_Date_HO_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", GetFormattedDateString(txtFromDate.Text, "01/04/2025"));
                cmd.Parameters.AddWithValue("@ToDate", GetFormattedDateString(txtToDate.Text, "31/03/2026"));

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

    private void BindLedgerAbstract()
    {
        DataTable dt = FetchAbstractDataset();
        gvReport.DataSource = dt;
        gvReport.DataBind();
        if (dt.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        lblPrintScope.Text = txtFromDate.Text + " To " + txtToDate.Text;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLedgerAbstract();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // All columns from Index 1 up to index 8 are amount fields (In Cr. double decimal format)
            for (int i = 1; i <= 8; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchAbstractDataset();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_RentClaim_HO_Abstract.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:11px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:12px; font-family: 'Segoe UI', Arial; }");
        sb.Append(".text-right { text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00; }");
        sb.Append(".text-center { text-align:center !important; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='10' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.Append("<tr><th colspan='10' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>NAFED Rent Claim & Owner Settlement HO Abstract Statement</th></tr>");
        sb.AppendFormat("<tr><td colspan='10' style='text-align:right; font-weight:bold;'>Filter Period Scope: {0} to {1} | Generated On: {2}</td></tr>", txtFromDate.Text, txtToDate.Text, DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>Submitted To NAFED (In Cr.)</th><th>Received From NAFED (In Cr.)</th><th>Pending at NAFED (In Cr.)</th>");
        sb.Append("<th>Total Rent Bill Amount (In Cr.)</th><th>Passed By RM After Deductions (In Cr.)</th><th>Pending at RM (In Cr.)</th>");
        sb.Append("<th>Total Deduction By RM (In Cr.)</th><th>Pay To Godown Owner (In Cr.)</th><th>Pending at RM For Owner Pay (In Cr.)</th></tr>");

        int sNo = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            sb.Append("<tr>");
            sb.AppendFormat("<td class='text-center'>{0}</td>", sNo++);
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["TotalBillAmountSubmittedtoNAFED"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["AmountRecivedFromNAfed"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["PendingatNafed"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["TotalRentBillAmountD"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["TotalAmountPassedbyRMAfterAllDeduction"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["PendingatRM"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["TotalDeductionbyRM"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["PaytogodownOwner"]).ToString("F2"));
            sb.AppendFormat("<td class='text-right'>{0}</td>", Convert.ToDecimal(row["PendingatRMForPaytogodownOwner"]).ToString("F2"));
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Clearance lock bypass container mapping */
    }
}