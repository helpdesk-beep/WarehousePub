using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Region_Wise_Online_Offline_Fumigation_Report_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

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
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_Region_Wise_Online_Offline_Fumigation_Report_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvReport.UseAccessibleHeader = true;
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Data Retrieval Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
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

            // FIXED FOR EXCEL ALIGNMENT: Explicitly injecting right alignment directly to the numeric data cells 
            for (int i = 2; i <= 8; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 2;
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
        // FIXED FOR EXCEL SUB-ROWS: Inject right alignment inline explicitly
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
        if (gvReport.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Region_Wise_Fumigation_Summary_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                // FORCE RIGHT ALIGN ON ALL NUMERIC COLS & TEXT WRAP OVER SHEET EXPORT PATHS
                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");

                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell regionCell = row.Cells[1];
                        if (regionCell.Controls.Count > 0)
                        {
                            string plainContent = string.Empty;
                            foreach (Control ctrl in regionCell.Controls)
                            {
                                if (ctrl is LiteralControl) plainContent += ((LiteralControl)ctrl).Text;
                                else if (ctrl is DataBoundLiteralControl) plainContent += ((DataBoundLiteralControl)ctrl).Text;
                            }

                            if (string.IsNullOrEmpty(plainContent)) plainContent = regionCell.Text;

                            if (!string.IsNullOrEmpty(plainContent) && plainContent.Contains("<a"))
                            {
                                int idxStart = plainContent.IndexOf(">") + 1;
                                int idxEnd = plainContent.LastIndexOf("</a");
                                if (idxStart > 0 && idxEnd > idxStart) plainContent = plainContent.Substring(idxStart, idxEnd - idxStart);
                            }

                            regionCell.Controls.Clear();
                            regionCell.Text = plainContent.Trim();
                        }
                    }
                }

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Region Wise Online & Offline Fumigation Progress Summary Report</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Scope: Corporate Head Office Overview</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);
                gvReport.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}