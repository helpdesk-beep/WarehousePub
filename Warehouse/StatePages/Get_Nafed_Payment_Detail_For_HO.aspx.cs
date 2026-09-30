using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Get_Nafed_Payment_Detail_For_HO : System.Web.UI.Page
{
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Running Accumulators for District Wise Sub Totals
    decimal subPayment = 0;
    decimal subTdsPss = 0;
    decimal subTdsPsf = 0;
    decimal subNetAmount = 0;

    // Accumulators for State Level Grand Totals
    decimal grandPayment = 0;
    decimal grandTdsPss = 0;
    decimal grandTdsPsf = 0;
    decimal grandNetAmount = 0;

    string currentDistrictName = "";
    int gridRowOffset = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Lock UI to target today's current date directly on initial thread boot
            txtPaymentDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            BindNafedReport(txtPaymentDate.Text);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPaymentDate.Text))
        {
            BindNafedReport(txtPaymentDate.Text);
        }
    }

    private void BindNafedReport(string paymentDate)
    {
        // Safe structural resets of calculation states before data rebinding
        subPayment = 0; subTdsPss = 0; subTdsPsf = 0; subNetAmount = 0;
        grandPayment = 0; grandTdsPss = 0; grandTdsPsf = 0; grandNetAmount = 0;
        currentDistrictName = "";
        gridRowOffset = 1;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Nafed_Payment_Detail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvNafedDetails.DataSource = dt;
                    gvNafedDetails.DataBind();
                }
            }
        }
    }

    protected void gvNafedDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            currentDistrictName = DataBinder.Eval(e.Row.DataItem, "District_Name").ToString();

            decimal pAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payment"));
            decimal pssTds = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS"));
            decimal psfTds = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS1"));
            decimal netAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));

            // Compile District Grouping Subtotals parameters
            subPayment += pAmt;
            subTdsPss += pssTds;
            subTdsPsf += psfTds;
            subNetAmount += netAmt;

            // Compile State Master Totals parameters
            grandPayment += pAmt;
            grandTdsPss += pssTds;
            grandTdsPsf += psfTds;
            grandNetAmount += netAmt;
        }
    }

    protected void gvNafedDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool createSubTotalRow = false;

        if (!string.IsNullOrEmpty(currentDistrictName) && DataBinder.Eval(e.Row.DataItem, "District_Name") != null)
        {
            if (currentDistrictName != DataBinder.Eval(e.Row.DataItem, "District_Name").ToString())
                createSubTotalRow = true;
        }
        if (!string.IsNullOrEmpty(currentDistrictName) && DataBinder.Eval(e.Row.DataItem, "District_Name") == null)
        {
            createSubTotalRow = true;
            gridRowOffset = 0; // Boundary limit handling block closure safely
        }

        if (createSubTotalRow)
        {
            GridView gv = (GridView)sender;
            GridViewRow subTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            subTotalRow.CssClass = "subtotal-row";

            TableCell cell = new TableCell { Text = currentDistrictName + " District Total :", ColumnSpan = 6, HorizontalAlign = HorizontalAlign.Right };
            cell.Attributes.Add("style", "text-align:right !important; padding-right:15px;");
            subTotalRow.Cells.Add(cell);

            subTotalRow.Cells.Add(CreateSummaryValueCell(subPayment.ToString("N2")));
            subTotalRow.Cells.Add(CreateSummaryValueCell(subTdsPss.ToString("N2")));
            subTotalRow.Cells.Add(CreateSummaryValueCell(subTdsPsf.ToString("N2")));
            subTotalRow.Cells.Add(CreateSummaryValueCell(subNetAmount.ToString("N2")));

            gv.Controls[0].Controls.AddAt(e.Row.RowIndex + gridRowOffset, subTotalRow);
            gridRowOffset++;

            // Clean reset sub-totals buffers for upcoming district segment
            subPayment = 0; subTdsPss = 0; subTdsPsf = 0; subNetAmount = 0;
        }
    }

    protected void gvNafedDetails_DataBound(object sender, EventArgs e)
    {
        if (gvNafedDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvNafedDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "STATE GRAND TOTAL SUMMARY :", ColumnSpan = 6 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(CreateSummaryValueCell(grandPayment.ToString("N2")));
            grandTotalRow.Cells.Add(CreateSummaryValueCell(grandTdsPss.ToString("N2")));
            grandTotalRow.Cells.Add(CreateSummaryValueCell(grandTdsPsf.ToString("N2")));
            grandTotalRow.Cells.Add(CreateSummaryValueCell(grandNetAmount.ToString("N2")));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell CreateSummaryValueCell(string textValue)
    {
        TableCell cell = new TableCell { Text = textValue, HorizontalAlign = HorizontalAlign.Right };
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important;");
        return cell;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string paymentDate = !string.IsNullOrEmpty(txtPaymentDate.Text) ? txtPaymentDate.Text : DateTime.Today.ToString("yyyy-MM-dd");
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Nafed_Payment_Detail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) { sda.Fill(dt); }
            }
        }

        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Payment_Details_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                StringBuilder sb = new StringBuilder();
                string formattedExcelDate = DateTime.Parse(paymentDate).ToString("dd-MM-yyyy");

                sb.Append("<style>");
                sb.Append("th { background-color:#4e73df !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #dee2e6; text-transform:uppercase; font-size:11px; }");
                sb.Append("td { border:1px solid #dee2e6; font-family: 'Segoe UI', Arial; font-size:11px; } .txt-r { text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00; } .txt-c { text-align:center !important; }");
                sb.Append(".subtotal-row td { background-color: #f1f5f9 !important; font-weight:bold !important; color:#334155 !important; }");
                sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #4e73df; border-bottom:2px solid #224abe; }");
                sb.Append("</style>");

                sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
                sb.Append("<tr><th colspan='10' style='font-size:16pt; background-color:#4e73df; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
                sb.Append("<tr><th colspan='10' style='font-size:12pt; background-color:#f8f9fc; color:#4e73df;'>NAFED Storage Payment Details Breakdown</th></tr>");
                sb.AppendFormat("<tr><td colspan='10' style='text-align:right; font-weight:bold;'>Target Date: {0} | Extracted On: {1}</td></tr>", formattedExcelDate, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
                sb.Append("<tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>");
                sb.Append("<tr><th>S.No.</th><th>District Name</th><th>Depot Name</th><th>Godown Name</th><th>Crop Year</th><th>Commodity</th><th>Payment Amt</th><th>TDS (PSS)</th><th>TDS (PSF)</th><th>Net Amount</th></tr>");

                string lastDistrict = "";
                int serialNo = 1;
                decimal sP = 0, gP = 0, sPss = 0, gPss = 0, sPsf = 0, gPsf = 0, sN = 0, gN = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dt.Rows[i];
                    string dName = r["District_Name"].ToString();
                    decimal p = Convert.ToDecimal(r["Payment"]);
                    decimal pss = Convert.ToDecimal(r["TDS"]);
                    decimal psf = Convert.ToDecimal(r["TDS1"]);
                    decimal net = Convert.ToDecimal(r["NetAmount"]);

                    if (lastDistrict != "" && lastDistrict != dName)
                    {
                        sb.AppendFormat("<tr class='subtotal-row'><td colspan='6' style='text-align:right; font-weight:bold;'>{0} District Total :</td><td class='txt-r'>{1:F2}</td><td class='txt-r'>{2:F2}</td><td class='txt-r'>{3:F2}</td><td class='txt-r'>{4:F2}</td></tr>", lastDistrict, sP, sPss, sPsf, sN);
                        sP = 0; sPss = 0; sPsf = 0; sN = 0;
                    }

                    sP += p; gP += p;
                    sPss += pss; gPss += pss;
                    sPsf += psf; gPsf += psf;
                    sN += net; gN += net;
                    lastDistrict = dName;

                    sb.AppendFormat("<tr><td class='txt-c'>{0}</td><td>{1}</td><td>{2}</td><td style='font-weight:bold;'>{3}</td><td class='txt-c'>{4}</td><td>{5}</td><td class='txt-r'>{6:F2}</td><td class='txt-r'>{7:F2}</td><td class='txt-r'>{8:F2}</td><td class='txt-r' style='font-weight:bold;'>{9:F2}</td></tr>",
                        serialNo++, dName, r["DepotName"], r["Godown_Name"], r["CropYear"], r["Commodity_Name"], p, pss, psf, net);
                }

                // Append the last district subtotal row and state master grand total row safely inside stream buffer
                sb.AppendFormat("<tr class='subtotal-row'><td colspan='6' style='text-align:right; font-weight:bold;'>{0} District Total :</td><td class='txt-r'>{1:F2}</td><td class='txt-r'>{2:F2}</td><td class='txt-r'>{3:F2}</td><td class='txt-r'>{4:F2}</td></tr>", lastDistrict, sP, sPss, sPsf, sN);
                sb.AppendFormat("<tr class='grandtotal-row'><td colspan='6' style='text-align:right; font-weight:bold;'>STATE GRAND TOTAL SUMMARY :</td><td class='txt-r'>{0:F2}</td><td class='txt-r'>{1:F2}</td><td class='txt-r'>{2:F2}</td><td class='txt-r'>{3:F2}</td></tr></table>", gP, gPss, gPsf, gN);

                Response.Write(sb.ToString());
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}