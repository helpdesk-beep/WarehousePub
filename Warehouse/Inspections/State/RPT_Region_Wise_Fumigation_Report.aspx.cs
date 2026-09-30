using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_RPT_Region_Wise_Fumigation_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Global Grand Total Accumulators
    int grandGodowns = 0;
    int grandOnlineStacks = 0;
    int grandFumigatedStacks = 0;
    int grandPendingStacks = 0;
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

            SqlCommand cmd = new SqlCommand("RPT_Region_Wise_Fumigation_Report", con);
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

                foreach (TableCell cell in gvReport.HeaderRow.Cells)
                {
                    cell.Attributes.Add("style", "color:#ffffff !important; text-align:center !important; vertical-align:middle !important; background-color:#2563eb !important; font-weight:bold !important;");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Sum up mathematical metrics securely into the total row pools
            grandGodowns += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            grandOnlineStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            grandFumigatedStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigated_Stack"));
            grandPendingStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            grandOpenedStacks += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));
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
            mainCell.ColumnSpan = 2; // Spans across S.No, Region ID, and Region Name
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            // Append right-aligned high priority total figures
            grandTotalRow.Cells.Add(createTotalCell(grandGodowns.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineStacks.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandFumigatedStacks.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPendingStacks.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpenedStacks.ToString("N0")));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important;");
        return cell;
    }

    private void ResetGrandTotalCounters()
    {
        grandGodowns = 0; grandOnlineStacks = 0; grandFumigatedStacks = 0; grandPendingStacks = 0; grandOpenedStacks = 0;
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
        Response.AddHeader("content-disposition", "attachment;filename=Region_Wise_Fumigation_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                // Forces cell data to wrap tightly inside Excel output sheet
                Response.Write("<style> td { mso-number-format:\\@; white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                // Colspan='8' matches total grid layout columns exactly
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='8' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='8' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Region Wise Fumigation & Stack Balance Summary Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Scope: Head Office Monitor</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='8' style='border:none;'>&nbsp;</td></tr>
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