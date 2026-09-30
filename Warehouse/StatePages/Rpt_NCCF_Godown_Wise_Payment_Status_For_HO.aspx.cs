using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_NCCF_Godown_Wise_Payment_Status_For_HO : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    decimal totalGenValue = 0;
    decimal totalPendBranchValue = 0;
    decimal totalPendNCCFValue = 0;
    decimal totalRecValue = 0;
    decimal totalRMSubAmt = 0;
    decimal totalRMPendingAmt = 0;

    // Fixed dynamic row schema accumulators
    decimal tGenBill = 0, tGenAmt = 0, tSubBill = 0, tSubAmt = 0;
    decimal tPendBrBill = 0, tPendBrAmt = 0, tRmSubBill = 0, tRmSubAmt = 0;
    decimal tRmPendBill = 0, tRmPendAmt = 0, tNccfRecBill = 0, tNccfRecAmt = 0;
    decimal tNccfDedAmt = 0, tNccfPendBill = 0, tNccfPendAmt = 0;
    decimal tPtgBill = 0, tPtgAmt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_NCCF_Godown_Wise_Bill_Payment_Status_For_State", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                CalculateTotals(dt);
            }
        }
    }

    private void CalculateTotals(DataTable dt)
    {
        totalGenValue = 0; totalPendBranchValue = 0; totalPendNCCFValue = 0;
        totalRecValue = 0; totalRMSubAmt = 0; totalRMPendingAmt = 0;

        foreach (DataRow row in dt.Rows)
        {
            totalGenValue += Convert.ToDecimal(row["BillAmt"]);
            totalPendBranchValue += Convert.ToDecimal(row["PendingBillAmountForSubmision"]);
            totalPendNCCFValue += Convert.ToDecimal(row["TotalNoofPendingBillAmountatNCCF"]);
            totalRecValue += Convert.ToDecimal(row["NoofbillPaymentAmountReceivedFromNCCF"]);
            totalRMSubAmt += Convert.ToDecimal(row["RMsubmitbillAmttonccf"]);
            totalRMPendingAmt += Convert.ToDecimal(row["PendingBillAmountForSubmisionatRM"]);
        }

        totalGen.InnerText = totalGenValue.ToString("N2");
        totalPendBranch.InnerText = totalPendBranchValue.ToString("N2");
        totalPendNCCF.InnerText = totalPendNCCFValue.ToString("N2");
        totalRec.InnerText = totalRecValue.ToString("N2");
        totalRMSub.InnerText = totalRMSubAmt.ToString("N2");
        totalRMPending.InnerText = totalRMPendingAmt.ToString("N2");
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // SAFE EXTRACTION PIPELINE: Using direct field mappings to prevent Index Out of Range error
            tGenBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill"));
            tGenAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BillAmt"));
            tSubBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill"));
            tSubAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBBillAmt"));

            tPendBrBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatBranch"));
            tPendBrAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmision"));
            tRmSubBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RMsubmitbilltonccf"));
            tRmSubAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RMsubmitbillAmttonccf"));

            tRmPendBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillForSubmisionatRM"));
            tRmPendAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmisionatRM"));
            tNccfRecBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentReceivedFromNCCF"));
            tNccfRecAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofbillPaymentAmountReceivedFromNCCF"));

            tNccfDedAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaymentDecuctionbyNCCF"));
            tNccfPendBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillatNCCF"));
            tNccfPendAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalNoofPendingBillAmountatNCCF"));
            tPtgBill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfBillPayment"));
            tPtgAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BillAmtPTG"));

            // Highlights cells dynamically based on UI elements
            decimal pendingAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmision"));
            if (pendingAmt > 0)
            {
                // Dynamic styling fallback
                for (int cellIdx = 0; cellIdx < e.Row.Cells.Count; cellIdx++)
                {
                    if (e.Row.Cells[cellIdx].Text.Contains(pendingAmt.ToString("N2")))
                    {
                        e.Row.Cells[cellIdx].BackColor = System.Drawing.Color.MistyRose;
                    }
                }
            }
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            Table mainTable = (Table)GridView1.Controls[0];
            GridViewRow totalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            totalRow.Attributes.Add("style", "background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important;");

            TableCell titleCell = new TableCell { Text = "State Grand Total Summary :", ColumnSpan = 5, HorizontalAlign = HorizontalAlign.Right };
            titleCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important; border:1px solid #cbd5e1;");
            totalRow.Cells.Add(titleCell);

            // Sequence ordered arrays to compile total elements safely into grid collection
            decimal[] summaryArray = {
                tGenBill, tGenAmt, tSubBill, tSubAmt, tPendBrBill, tPendBrAmt,
                tRmSubBill, tRmSubAmt, tRmPendBill, tRmPendAmt, tNccfRecBill, tNccfRecAmt,
                tNccfDedAmt, tNccfPendBill, tNccfPendAmt, tPtgBill, tPtgAmt
            };

            for (int i = 0; i < summaryArray.Length; i++)
            {
                bool isCount = (i == 0 || i == 2 || i == 4 || i == 6 || i == 8 || i == 10 || i == 13 || i == 15);
                string display = isCount ? summaryArray[i].ToString("N0") : summaryArray[i].ToString("N2");
                string mso = isCount ? "mso-number-format:\\#\\,\\#\\#0;" : "mso-number-format:\\#\\,\\#\\#0\\.00;";

                TableCell cell = new TableCell { Text = display, HorizontalAlign = HorizontalAlign.Right };
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:8px !important; border:1px solid #cbd5e1;" + mso);
                totalRow.Cells.Add(cell);
            }

            mainTable.Rows.Add(totalRow);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_Godown_Wise_Payment_Status_HO.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                GridView1.AllowPaging = false;
                BindGrid();

                string css = "<style> th { background-color: #212529 !important; color: white !important; font-weight: bold !important; text-align: center !important; } td { border: 1px solid #dee2e6 !important; } </style>";
                Response.Write(css);
                GridView1.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}