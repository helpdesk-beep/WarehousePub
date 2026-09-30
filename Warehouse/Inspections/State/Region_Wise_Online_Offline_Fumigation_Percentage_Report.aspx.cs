using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Region_Wise_Online_Offline_Fumigation_Report_HO : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;

    // Trackers for grand calculations (Exactly matching your 11 columns structure)
    int grandGodowns = 0;
    int grandOnlineStacks = 0;
    int grandGodownsCovered = 0;
    int grandOnlineFumigated = 0;
    int grandOfflineFumigated = 0;
    int grandTotalFumigation = 0;
    int grandPendingGodowns = 0;
    int grandPendingStacks = 0;
    int grandOpenedStacks = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { BindGrid(); }
    }

    private void BindGrid()
    {
        ResetTotalCounters();

        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetFumigationHOReport_Level1_Region", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_Id", 0);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvReport.DataSource = dt;
                    gvReport.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvReport.UseAccessibleHeader = true;
                        gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Adding current row values into aggregate matrices safely
            grandGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            grandOnlineStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            grandGodownsCovered += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godowns_Covered"));
            grandOnlineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            grandOfflineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            grandTotalFumigation += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            grandPendingGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Pending_Godown"));
            grandPendingStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            grandOpenedStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            // Inject styles for clean number formats inside Excel spreadsheet
            for (int i = 2; i <= 10; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");

            // Mapping variables precisely across exact column cells bounds
            e.Row.Cells[2].Text = grandGodowns.ToString("N0");
            e.Row.Cells[3].Text = grandOnlineStacks.ToString("N0");
            e.Row.Cells[4].Text = grandGodownsCovered.ToString("N0");
            e.Row.Cells[5].Text = grandOnlineFumigated.ToString("N0");
            e.Row.Cells[6].Text = grandOfflineFumigated.ToString("N0");
            e.Row.Cells[7].Text = grandTotalFumigation.ToString("N0");
            e.Row.Cells[8].Text = grandPendingGodowns.ToString("N0");
            e.Row.Cells[9].Text = grandPendingStacks.ToString("N0");
            e.Row.Cells[10].Text = grandOpenedStacks.ToString("N0");

            // Overall Summary Percentage evaluation formulas
            double overallPercent = 0.00;
            if (grandOnlineStacks > 0)
            {
                overallPercent = (Convert.ToDouble(grandTotalFumigation) * 100.0) / Convert.ToDouble(grandOnlineStacks);
            }
            e.Row.Cells[11].Text = overallPercent.ToString("N2") + "%";

            // Formatting Footer elements explicitly for rendering engines
            for (int j = 2; j <= 11; j++)
            {
                e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important;");
                if (j <= 10)
                {
                    e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0;");
                }
            }
        }
    }

    private void ResetTotalCounters()
    {
        grandGodowns = 0; grandOnlineStacks = 0; grandGodownsCovered = 0;
        grandOnlineFumigated = 0; grandOfflineFumigated = 0; grandTotalFumigation = 0;
        grandPendingGodowns = 0; grandPendingStacks = 0; grandOpenedStacks = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Region_Wise_Fumigation_Percentage_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindGrid();

                // Convert hyperlinks dynamically to plain text labels for native Excel views
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        HyperLink hyp = (HyperLink)row.FindControl("lnkRegion");
                        Label lbl = (Label)row.FindControl("lblRegionPrint");
                        if (hyp != null && lbl != null)
                        {
                            row.Cells[1].Controls.Clear();
                            row.Cells[1].Text = lbl.Text;
                        }
                    }
                }

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string excelMetaHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='12' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='12' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Region Wise Online & Offline Fumigation Progress Summary Report</th></tr>
                        <tr><td colspan='6' style='text-align:left; font-weight:bold; color:#475569;'>Scope: Head Office Corporate Summary View</td><td colspan='6' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='12' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                string style = @"<style> 
                    th { background-color: #2563eb !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important;} 
                    td { border:1px solid #cbd5e1 !important; white-space:normal; } 
                    .text-right-align { text-align: right !important; }
                    .text-center-align { text-align: center !important; }
                    .footer-style td { background-color: #eff6ff !important; font-weight: bold !important; color: #1e3a8a !important; }
                </style>";

                Response.Write(style);
                Response.Write(excelMetaHeader);

                gvReport.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verified Context Layout pipeline targets passed safely
    }
}