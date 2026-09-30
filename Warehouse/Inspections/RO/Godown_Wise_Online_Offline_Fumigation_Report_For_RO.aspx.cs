using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_Godown_Wise_Online_Offline_Fumigation_Report_For_RO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Consolidated Master Grand Totals Counters Pools
    int grandOnline = 0, grandOnlineFum = 0, grandOfflineFum = 0, grandTotFum = 0, grandPending = 0, grandOpened = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["BranchId"] != null)
            {
                string targetBranch = Request.QueryString["BranchId"].ToString().Trim();
                //lblBranchInfo.Text = "Branch/Depot ID: " + targetBranch;
                BindGodownReport(targetBranch);
            }
            else
            {
                //lblBranchInfo.Text = "All Branches Consolidated";
                BindGodownReport("0");
            }
        }
    }

    private void BindGodownReport(string branchId)
    {
        try
        {
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_Godown_Wise_Online_Offline_Fumigation_Report_For_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchId", branchId.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvGodownDetails.DataSource = dt;
            gvGodownDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvGodownDetails.UseAccessibleHeader = true;
                gvGodownDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Data Read Failure: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvGodownDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grandOnline += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            grandOnlineFum += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            grandOfflineFum += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            grandTotFum += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            grandPending += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            grandOpened += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            // FIXED FOR EXCEL: Explicit alignment rules injected on raw numerical cell bounds
            for (int i = 2; i <= 7; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    protected void gvGodownDetails_DataBound(object sender, EventArgs e)
    {
        if (gvGodownDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvGodownDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell { Text = "Grand Total", ColumnSpan = 2 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandOnline.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOfflineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandTotFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPending.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpened.ToString("N0")));

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
        grandOnline = 0; grandOnlineFum = 0; grandOfflineFum = 0; grandTotFum = 0; grandPending = 0; grandOpened = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvGodownDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Level_Fumigation_Details.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string incomingBranchId = Request.QueryString["BranchId"] != null ? Request.QueryString["BranchId"].ToString() : "0";
                BindGodownReport(incomingBranchId);

                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                // Full 8 columns layout width locks colspan bounds
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='8' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='8' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise Online & Offline Fumigation Progress Summary Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Filtered Depot Branch ID: {0}</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='8' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);
                gvGodownDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush(); Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}