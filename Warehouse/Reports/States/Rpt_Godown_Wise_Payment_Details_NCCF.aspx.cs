using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Godown_Wise_Payment_Details_NCCF : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    // FIXED: Track context using Godown field instead of District
    string currentGodown = string.Empty;

    // Accumulators for Sub-total and Grand Total
    decimal subTotalAmount = 0;
    decimal grandTotalAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["PayDate"] != null)
            {
                string incomingDate = Request.QueryString["PayDate"].ToString();
                lblTargetDate.Text = incomingDate;
                BindDetailedReport(incomingDate);
            }
            else
            {
                Response.Write("<script>alert('Invalid parameters found!');</script>");
            }
        }
    }

    private void BindDetailedReport(string targetDate)
    {
        try
        {
            currentGodown = string.Empty;
            subTotalAmount = 0;
            grandTotalAmount = 0;

            SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Payment_details_Date_Wise_NCCF", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaymentDate", targetDate.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvDetails.DataSource = dt;
            gvDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvDetails.UseAccessibleHeader = true;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Failure: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // FIXED: Target 'Godown' field for dynamic row grouping checks
            string godownIdentifier = DataBinder.Eval(e.Row.DataItem, "Godown").ToString();

            if (string.IsNullOrEmpty(currentGodown))
            {
                currentGodown = godownIdentifier;
            }

            // Injects Godown Sub-Total row when Godown shifts
            if (currentGodown != godownIdentifier)
            {
                Table tbl = (Table)gvDetails.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentGodown = godownIdentifier;
                subTotalAmount = 0; // Reset sub-total for next Godown cluster
            }

            decimal amount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            subTotalAmount += amount;
            grandTotalAmount += amount;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell();
        // Displays Godown Name & ID in the summary context cell
        cell.Text = currentGodown + " - Sub Total";
        cell.ColumnSpan = 9; // Exactly aligns up to Bill Month column index
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
        row.Cells.Add(cell);

        TableCell amtCell = new TableCell { Text = subTotalAmount.ToString("N2") };
        amtCell.CssClass = "text-right-align";
        amtCell.Attributes.Add("style", "text-align:right !important; padding-right:12px !important; font-weight:bold !important;");
        row.Cells.Add(amtCell);

        return row;
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvDetails.Controls[0];

            // Add final group godown's subtotal row
            if (!string.IsNullOrEmpty(currentGodown))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            // Add Master Grand Total row
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell { Text = "Grand Total", ColumnSpan = 9 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            TableCell amtCell = new TableCell { Text = grandTotalAmount.ToString("N2") };
            amtCell.CssClass = "text-right-align";
            amtCell.Attributes.Add("style", "text-align:right !important; padding-right:12px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(amtCell);

            tbl.Rows.Add(grandTotalRow);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Wise_Payment_Details_NCCF.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                if (Request.QueryString["PayDate"] != null)
                {
                    BindDetailedReport(Request.QueryString["PayDate"].ToString());
                }

                gvDetails.GridLines = GridLines.Both;
                gvDetails.HeaderStyle.BackColor = System.Drawing.Color.FromName("#1e3a8a");
                gvDetails.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvDetails.HeaderStyle.Font.Bold = true;

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise NCCF Payment Received Details</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Payment Date: {0}</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", lblTargetDate.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                gvDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}