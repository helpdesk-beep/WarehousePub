using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_Offline_Moisture_Report_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Subtotal properties
    int subGodown = 0, subStack = 0, subSent = 0, subInspected = 0, subPending = 0;

    // Grand total properties
    int grandGodown = 0, grandStack = 0, grandSent = 0, grandInspected = 0, grandPending = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            currentRegion = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_District_Wise_Offline_Moisture_Report", con);
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
            Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string regionName = DataBinder.Eval(e.Row.DataItem, "Regionnm").ToString();

            if (string.IsNullOrEmpty(currentRegion))
            {
                currentRegion = regionName;
            }

            if (currentRegion != regionName)
            {
                Table tbl = (Table)gvReport.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentRegion = regionName;
                ResetSubTotalCounters();
            }

            int godowns = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            int stacks = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack"));
            int sent = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Sent to FCI/DM"));
            int inspected = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Inspected By FCI"));
            int pending = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending As FCI"));

            // Sum up Subtotals
            subGodown += godowns; subStack += stacks; subSent += sent; subInspected += inspected; subPending += pending;

            // Sum up Grand Totals
            grandGodown += godowns; grandStack += stacks; grandSent += sent; grandInspected += inspected; grandPending += pending;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell();
        cell.Text = currentRegion + " - Sub Total";
        cell.ColumnSpan = 3;
        cell.HorizontalAlign = HorizontalAlign.Left;
        cell.VerticalAlign = VerticalAlign.Middle;
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = subGodown.ToString() });
        row.Cells.Add(new TableCell { Text = subStack.ToString() });
        row.Cells.Add(new TableCell { Text = subSent.ToString() });
        row.Cells.Add(new TableCell { Text = subInspected.ToString() });
        row.Cells.Add(new TableCell { Text = subPending.ToString() });

        for (int i = 1; i < row.Cells.Count; i++)
        {
            row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[i].VerticalAlign = VerticalAlign.Middle;
        }
        return row;
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            // Injects final region's subtotal row
            if (!string.IsNullOrEmpty(currentRegion))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            // Injects dynamic single Grand Total row inside tbody context directly
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 3;
            mainCell.HorizontalAlign = HorizontalAlign.Center;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandGodown.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandSent.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandInspected.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPending.ToString() });

            for (int i = 1; i < grandTotalRow.Cells.Count; i++)
            {
                grandTotalRow.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                grandTotalRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
            }

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters()
    {
        subGodown = 0; subStack = 0; subSent = 0; subInspected = 0; subPending = 0;
    }

    private void ResetGrandTotalCounters()
    {
        grandGodown = 0; grandStack = 0; grandSent = 0; grandInspected = 0; grandPending = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Offline_Moisture_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                gvReport.GridLines = GridLines.Both;

                gvReport.HeaderStyle.BackColor = System.Drawing.Color.FromName("#2563eb");
                gvReport.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvReport.HeaderStyle.Font.Bold = true;
                gvReport.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                gvReport.HeaderStyle.VerticalAlign = VerticalAlign.Middle;

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr>
                            <th colspan='8' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='8' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>District Wise Offline Moisture Report</th>
                        </tr>
                        <tr>
                            <td colspan='4' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Report Type: Offline Entry System</td>
                            <td colspan='4' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>Generated On: {0}</td>
                        </tr>
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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}