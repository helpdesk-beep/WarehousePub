using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Branch_Wise_Online_Offline_Fumigation_Percentage_Report_HO : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;

    // Trackers for grand calculations (Exactly aligned with the 12 dynamic table columns)
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
        if (!IsPostBack && Request.QueryString["District_Id"] != null) { BindGrid(); }
    }

    private void BindGrid()
    {
        ResetTotalCounters();
        int districtId = Convert.ToInt32(Request.QueryString["District_Id"]);

        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.sp_GetFumigationHOReport_Level3_Branch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictId", districtId);
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
            // Parse and aggregate global values row contextually
            grandGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            grandOnlineStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            grandGodownsCovered += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godowns_Covered"));
            grandOnlineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            grandOfflineFumigated += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            grandTotalFumigation += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            grandPendingGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Pending_Godown"));
            grandPendingStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            grandOpenedStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            // Inject numerical alignment and format structures into cell rows
            for (int i = 3; i <= 11; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "";
            e.Row.Cells[2].Text = "Grand Total";
            e.Row.Cells[2].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");

            // Mapping metric values precisely across target footer cells bounds
            e.Row.Cells[3].Text = grandGodowns.ToString("N0");
            e.Row.Cells[4].Text = grandOnlineStacks.ToString("N0");
            e.Row.Cells[5].Text = grandGodownsCovered.ToString("N0");
            e.Row.Cells[6].Text = grandOnlineFumigated.ToString("N0");
            e.Row.Cells[7].Text = grandOfflineFumigated.ToString("N0");
            e.Row.Cells[8].Text = grandTotalFumigation.ToString("N0");
            e.Row.Cells[9].Text = grandPendingGodowns.ToString("N0");
            e.Row.Cells[10].Text = grandPendingStacks.ToString("N0");
            e.Row.Cells[11].Text = grandOpenedStacks.ToString("N0");

            // Overall cumulative summary percentage calculations
            double structuralPercent = 0.00;
            if (grandOnlineStacks > 0)
            {
                structuralPercent = (Convert.ToDouble(grandTotalFumigation) * 100.0) / Convert.ToDouble(grandOnlineStacks);
            }
            e.Row.Cells[12].Text = structuralPercent.ToString("N2") + "%";

            // Loop to structure exact styling attributes inside clean layout streams
            for (int j = 3; j <= 12; j++)
            {
                e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important;");
                if (j <= 11)
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
        Response.AddHeader("content-disposition", "attachment;filename=Branch_Wise_Fumigation_Percentage_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindGrid();

                // Swap dynamic links out for direct strings before pushing data to spreadsheet pipeline
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        HyperLink hyp = (HyperLink)row.FindControl("lnkBranch");
                        Label lbl = (Label)row.FindControl("lblBranchPrint");
                        if (hyp != null && lbl != null)
                        {
                            row.Cells[2].Controls.Clear();
                            row.Cells[2].Text = lbl.Text;
                        }
                    }
                }

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='13' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='13' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Branch Wise Online & Offline Fumigation Progress Summary Report</th></tr>
                        <tr><td colspan='6' style='text-align:left; font-weight:bold; color:#475569;'>Scope: Head Office Branch Context Summary</td><td colspan='7' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='13' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                string style = @"<style> 
                    th { background-color: #2563eb !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important;} 
                    td { border:1px solid #cbd5e1 !important; white-space:normal; } 
                    .text-right-align { text-align: right !important; }
                    .text-center-align { text-align: center !important; }
                    .footer-style td { background-color: #eff6ff !important; font-weight: bold !important; color: #1e3a8a !important; }
                </style>";

                Response.Write(style);
                Response.Write(customExcelHeader);

                gvReport.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Pipeline signature verification
    }
}