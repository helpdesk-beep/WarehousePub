using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Branch_Wise_Online_Offline_Fumigation_Report_For_DO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Dynamic Consolidated Master Grand Totals Accumulators Pools 
    int grandGodowns = 0;
    int grandOnlineStacks = 0;
    int grandOnlineFumigated = 0;
    int grandOfflineFumigated = 0;
    int grandTotalFumigation = 0;
    int grandPendingFumigation = 0;
    int grandOpenedStacks = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindBranchReport();
        }
    }

    // FIXED OVERLOADS: Added missing base level parameterized capability caller signature natively
    private void BindBranchReport()
    {
        string districtId = Session["Depot_DistID"] != null ? Session["Depot_DistID"].ToString().Trim() : "0";
        BindBranchReport(districtId);
    }

    private void BindBranchReport(string districtId)
    {
        try
        {
            ResetGrandTotalCounters();

            // FIXED STORED PROCEDURE: Mapped precisely to Fumigation master procedure layout
            SqlCommand cmd = new SqlCommand("sp_Branch_Wise_Online_Offline_Fumigation_Report_For_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", districtId.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvBranchDetails.DataSource = dt;
            gvBranchDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvBranchDetails.UseAccessibleHeader = true;
                gvBranchDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error Context Exception: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvBranchDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grandGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            grandOnlineStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            grandOnlineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            grandOfflineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            grandTotalFumigation += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            grandPendingFumigation += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            grandOpenedStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            // Numerical fields align right from cell index 2 through index 8
            for (int i = 2; i <= 8; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    protected void gvBranchDetails_DataBound(object sender, EventArgs e)
    {
        if (gvBranchDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvBranchDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            // Table cells span covering exactly 2 columns (S.No and Depot Name fields)
            TableCell mainCell = new TableCell { Text = "Grand Total Summary", ColumnSpan = 2 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandGodowns.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineStacks.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineFumigated.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOfflineFumigated.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandTotalFumigation.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPendingFumigation.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpenedStacks.ToString("N0")));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important; mso-number-format:\\#\\,\\#\\#0;");
        return cell;
    }

    private void ResetGrandTotalCounters()
    {
        grandGodowns = 0; grandOnlineStacks = 0; grandOnlineFumigated = 0; grandOfflineFumigated = 0;
        grandTotalFumigation = 0; grandPendingFumigation = 0; grandOpenedStacks = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvBranchDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Depot_Wise_NCCF_Fumigation_Ledger.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string targetDistrict = Session["Depot_DistID"] != null ? Session["Depot_DistID"].ToString().Trim() : "0";
                BindBranchReport(targetDistrict);

                // FIXED SHEET STRIPPER: Clears Hyperlink anchors inside column index 1 during excel generations
                foreach (GridViewRow row in gvBranchDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell depotCell = row.Cells[1];
                        if (depotCell.Controls.Count > 0 && depotCell.Controls[1] is HyperLink)
                        {
                            string rawText = ((HyperLink)depotCell.Controls[1]).Text;
                            depotCell.Controls.Clear();
                            depotCell.Text = rawText.Trim();
                        }
                    }
                }

                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                // Layout wide block set to exactly 9 columns
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Depot Wise Online & Offline Fumigation Progress Ledger Summary</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Target Context Scope District ID: {0}</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", targetDistrict, dateTimeStr);

                Response.Write(customExcelHeader);
                gvBranchDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}