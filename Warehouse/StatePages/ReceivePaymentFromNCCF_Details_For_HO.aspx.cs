using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_ReceivePaymentFromNCCF_Details : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    // Summing accumulators for details columns
    decimal totalBillAmt = 0; decimal totalPss = 0; decimal totalPsf = 0;
    decimal totalTds = 0; decimal totalOther = 0; decimal totalReceived = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["GdnID"] != null && !string.IsNullOrEmpty(Request.QueryString["GdnID"].ToString()))
            {
                string godownID = Request.QueryString["GdnID"].ToString().Trim();
                LoadGodownBillDetails(godownID);
            }
            else
            {
                Response.Redirect("ReceivePaymentFromNCCF.aspx");
            }
        }
    }

    private void LoadGodownBillDetails(string godownID)
    {
        try
        {
            totalBillAmt = 0; totalPss = 0; totalPsf = 0; totalTds = 0; totalOther = 0; totalReceived = 0;

            SqlCommand cmd = new SqlCommand("sp_NCCF_Godown_Wise_Bill_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GodownID", godownID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string dynamicGodownTitle = dt.Rows[0]["Godown_Name"].ToString();
                lblGodownName.Text = dynamicGodownTitle;
                lblPrintGodown.Text = dynamicGodownTitle;

                gvDetails.DataSource = dt;
                gvDetails.DataBind();

                gvDetails.UseAccessibleHeader = true;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

                foreach (TableCell cell in gvDetails.HeaderRow.Cells)
                {
                    cell.Attributes.Add("style", "color:#ffffff !important; text-align:center !important; vertical-align:middle !important; background-color:#1e3a8a !important; font-weight:bold !important;");
                }
            }
            else
            {
                lblGodownName.Text = "No records associated with ID: " + godownID;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBillAmt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill Amount"));
            totalPss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSS Bill Amount"));
            totalPsf += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSF Bill Amount"));
            totalTds += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS Deduction From NCCF"));
            totalOther += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other Deduction From NCCF"));
            totalReceived += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Received Payment From NCCF"));
        }
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvDetails.Controls[0];

            GridViewRow totalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            totalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            totalRow.Font.Bold = true;
            totalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Total";
            mainCell.ColumnSpan = 2; // Merges S.No and Bill Number
            mainCell.HorizontalAlign = HorizontalAlign.Center;
            rowStyleApply(mainCell);
            totalRow.Cells.Add(mainCell);

            totalRow.Cells.Add(createCell(totalBillAmt.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell("-", HorizontalAlign.Center)); // Deduction status text block padding
            totalRow.Cells.Add(createCell(totalPss.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell(totalPsf.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell(totalTds.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell(totalOther.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell(totalReceived.ToString("N2"), HorizontalAlign.Right));
            totalRow.Cells.Add(createCell("-", HorizontalAlign.Center));
            totalRow.Cells.Add(createCell("-", HorizontalAlign.Center));

            tbl.Rows.Add(totalRow);
        }
    }

    private TableCell createCell(string text, HorizontalAlign align)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = align };
        rowStyleApply(cell);
        return cell;
    }

    private void rowStyleApply(TableCell cell)
    {
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "font-weight: bold !important; border: 1px solid #e2e8f0 !important; font-size:12px !important; padding:10px !important;");
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("NCCF_Received_Payemt_Details_For_HO.aspx");
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear(); Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_NCCF_Bill_Details.xls");
        Response.Charset = ""; Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                if (Request.QueryString["GdnID"] != null)
                {
                    LoadGodownBillDetails(Request.QueryString["GdnID"].ToString());
                }

                gvDetails.GridLines = GridLines.Both;
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='11' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='11' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise Billwise Settlement History</th></tr>
                        <tr><td colspan='6' style='text-align:left; font-weight:bold; color:#475569;'>Selected Godown: {0}</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='11' style='border:none;'>&nbsp;</td></tr>
                    </table>", lblGodownName.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                gvDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush(); Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}