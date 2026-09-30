using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class StatePages_ReceivePaymentFromNAFED_Details_For_HO : System.Web.UI.Page
{
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Accumulators definition matching matrix
    decimal sumTotalBillAmt = 0;
    int sumTotalBillsCount = 0;
    decimal sumPssBillAmt = 0;
    decimal sumPsfBillAmt = 0;
    decimal sumTdsDeduct = 0;
    decimal sumOtherDeduct = 0;
    decimal sumReceivedPayment = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["PDate"] != null && Request.QueryString["Region"] != null)
            {
                string paymentDate = Request.QueryString["PDate"].ToString().Trim();
                string regionId = Request.QueryString["Region"].ToString().Trim();

                lblSelectedDate.Text = paymentDate == "1900-01-01" ? "Payment From NAFED Before 2026" : paymentDate;

                BindDetailsGrid(paymentDate, regionId);
            }
        }
    }

    private void BindDetailsGrid(string paymentDate, string regionId)
    {
        // Safe resets state variables before collection loop binding
        sumTotalBillAmt = 0; sumTotalBillsCount = 0; sumPssBillAmt = 0;
        sumPsfBillAmt = 0; sumTdsDeduct = 0; sumOtherDeduct = 0; sumReceivedPayment = 0;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("sp_NAFED_Storage_Bill_And_Payment_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                if (regionId == "0" || string.IsNullOrEmpty(regionId))
                    cmd.Parameters.AddWithValue("@RegionID", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@RegionID", regionId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvPaymentDetails.DataSource = dt;
                    gvPaymentDetails.DataBind();
                }
            }
        }
    }

    protected void gvPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            sumTotalBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill Amount"));
            sumTotalBillsCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill"));
            sumPssBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSS Bill Amount"));
            sumPsfBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSF Bill Amount"));
            sumTdsDeduct += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS Deduction From NAFED"));
            sumOtherDeduct += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other Deduction From NAFED"));
            sumReceivedPayment += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Received Payment From NAFED"));
        }
    }

    protected void gvPaymentDetails_DataBound(object sender, EventArgs e)
    {
        if (gvPaymentDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvPaymentDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "Grand Total Summary :", ColumnSpan = 4 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(CreateSummaryCell(sumTotalBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumTotalBillsCount.ToString("N0"), HorizontalAlign.Center));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumPssBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumPsfBillAmt.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumTdsDeduct.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumOtherDeduct.ToString("N2"), HorizontalAlign.Right));
            grandTotalRow.Cells.Add(CreateSummaryCell(sumReceivedPayment.ToString("N2"), HorizontalAlign.Right));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell CreateSummaryCell(string textDisplay, HorizontalAlign align)
    {
        TableCell cell = new TableCell { Text = textDisplay, HorizontalAlign = align };
        cell.Attributes.Add("style", "font-weight:bold !important;");
        return cell;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (gvPaymentDetails.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Godown_Payment_Details_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // MASTER PAGE BYPASS TRICK: Explicit container rendering ensures master HTML isolation completely
                    Table table = new Table();
                    TableRow rowHeader = new TableRow();
                    TableCell cellHeader = new TableCell();

                    cellHeader.Text = "<h2 style='text-align:center; color:#008CBA;'>MP WAREHOUSING AND LOGISTICS CORPORATION</h2>" +
                                     "<h3 style='text-align:center;'>Godown Wise Payment Details (NAFED)</h3>" +
                                     "<p style='text-align:center;'>Context Date: " + lblSelectedDate.Text + " | Generated On: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "</p><br/>";
                    cellHeader.ColumnSpan = gvPaymentDetails.Columns.Count;
                    cellHeader.Font.Bold = true;
                    rowHeader.Cells.Add(cellHeader);
                    table.Rows.Add(rowHeader);

                    table.RenderControl(hw);
                    gvPaymentDetails.RenderControl(hw);

                    // Output stream styling rules clean integration
                    Response.Write(sbExcelStyleBuilder() + sw.ToString());
                    Response.Flush();
                    Response.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }
    }

    private string sbExcelStyleBuilder()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#008CBA !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #CCCCCC; text-transform:uppercase; }");
        sb.Append("td { border:1px solid #CCCCCC; font-family: 'Segoe UI', Arial, sans-serif; }");
        sb.Append(".grandtotal-row td { background-color: #e2f1f6 !important; font-weight:bold !important; color:#006699 !important; border-top:2px solid #008CBA; border-bottom:2px solid #005f73; }");
        sb.Append("</style>");
        return sb.ToString();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // GridView execution override requirements satisfied
    }
}