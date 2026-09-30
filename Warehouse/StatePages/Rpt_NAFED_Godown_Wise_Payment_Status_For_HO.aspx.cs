using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_NAFED_Godown_Wise_Payment_Status_For_HO : System.Web.UI.Page
{
    private string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Totals Calculation Variables
    private decimal tot_NoOfGenerateBill = 0;
    private decimal tot_BillAmt = 0;
    private decimal tot_NoOfSUBBill = 0;
    private decimal tot_SUBBillAmt = 0;
    private decimal tot_PendingBillBranch = 0;
    private decimal tot_PendingAmtBranch = 0;
    private decimal tot_RMsubmitbill = 0;
    private decimal tot_RMsubmitbillAmt = 0;
    private decimal tot_PendingBillRM = 0;
    private decimal tot_PendingAmtRM = 0;
    private decimal tot_PaymentRecvBill = 0;
    private decimal tot_PaymentRecvAmt = 0;
    private decimal tot_DeductionNafed = 0;
    private decimal tot_PendingBillNafed = 0;
    private decimal tot_PendingAmtNafed = 0;
    private decimal tot_NoOfBillPayment = 0;
    private decimal tot_BillAmtPTG = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            BindReportData();
        }
    }

    private void BindReportData()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_NAFED_Godown_Wise_Bill_Payment_Status_For_State", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    gvGodownStatus.DataSource = dt;
                    gvGodownStatus.DataBind();

                    // Sets Header to <thead> so it repeats on every page when printing
                    if (gvGodownStatus.Rows.Count > 0)
                    {
                        gvGodownStatus.UseAccessibleHeader = true;
                        gvGodownStatus.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
                }
            }
        }
    }

    protected void gvGodownStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tot_NoOfGenerateBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill"));
            tot_BillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BillAmt") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "BillAmt"));
            tot_NoOfSUBBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill"));
            tot_SUBBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBBillAmt") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "SUBBillAmt"));

            tot_PendingBillBranch += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatBranch") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatBranch"));
            tot_PendingAmtBranch += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmision") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmision"));

            tot_RMsubmitbill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RMsubmitbilltoNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "RMsubmitbilltoNafed"));
            tot_RMsubmitbillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RMsubmitbillAmttoNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "RMsubmitbillAmttoNafed"));

            tot_PendingBillRM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatRM") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatRM"));
            tot_PendingAmtRM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmisionatRM") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmisionatRM"));

            tot_PaymentRecvBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentReceivedFromNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentReceivedFromNafed"));
            tot_PaymentRecvAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentAmountReceivedFromNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentAmountReceivedFromNafed"));

            tot_DeductionNafed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaymentDecuctionbyNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "PaymentDecuctionbyNafed"));
            tot_PendingBillNafed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillatNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillatNafed"));
            tot_PendingAmtNafed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillAmountatNafed") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillAmountatNafed"));

            tot_NoOfBillPayment += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfBillPayment") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "NoOfBillPayment"));
            tot_BillAmtPTG += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BillAmtPTG") == DBNull.Value ? 0 : DataBinder.Eval(e.Row.DataItem, "BillAmtPTG"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "grid-footer";

            e.Row.Cells[1].Text = "Total";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[5].Text = tot_NoOfGenerateBill.ToString("N0");
            e.Row.Cells[6].Text = tot_BillAmt.ToString("N2");
            e.Row.Cells[7].Text = tot_NoOfSUBBill.ToString("N0");
            e.Row.Cells[8].Text = tot_SUBBillAmt.ToString("N2");

            e.Row.Cells[9].Text = tot_PendingBillBranch.ToString("N0");
            e.Row.Cells[10].Text = tot_PendingAmtBranch.ToString("N2");

            e.Row.Cells[11].Text = tot_RMsubmitbill.ToString("N0");
            e.Row.Cells[12].Text = tot_RMsubmitbillAmt.ToString("N2");

            e.Row.Cells[13].Text = tot_PendingBillRM.ToString("N0");
            e.Row.Cells[14].Text = tot_PendingAmtRM.ToString("N2");

            e.Row.Cells[15].Text = tot_PaymentRecvBill.ToString("N0");
            e.Row.Cells[16].Text = tot_PaymentRecvAmt.ToString("N2");

            e.Row.Cells[17].Text = tot_DeductionNafed.ToString("N2");
            e.Row.Cells[18].Text = tot_PendingBillNafed.ToString("N0");
            e.Row.Cells[19].Text = tot_PendingAmtNafed.ToString("N2");

            e.Row.Cells[20].Text = tot_NoOfBillPayment.ToString("N0");
            e.Row.Cells[21].Text = tot_BillAmtPTG.ToString("N2");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MPWLC_NAFED_Payment_Status_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvGodownStatus.AllowPaging = false;
            this.BindReportData();

            sw.Write("<table border='1'><tr><td colspan='22' style='text-align:center;font-weight:bold;font-size:16px;'>MADHYA PRADESH WAREHOUSING & LOGISTICS CORPORATION</td></tr>");
            sw.Write("<tr><td colspan='22' style='text-align:center;font-weight:bold;font-size:14px;'>NAFED Godown Wise Bill Payment Status Report</td></tr></table><br/>");

            gvGodownStatus.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Control Export
    }
}