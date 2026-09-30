using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_Online_Moisture_Report_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Subtotal variables per region group
    int subTotStack = 0, subPrevStack = 0, subTodayStack = 0, subTotMoistStack = 0, subPendStack = 0;
    int subPrevDm = 0, subTodayDm = 0, subTotDm = 0;
    int subPrevFci = 0, subTodayFci = 0, subTotFci = 0;
    int subFciInsp = 0;

    // Global Grand Total Accumulators 
    int grandTotStack = 0, grandPrevStack = 0, grandTodayStack = 0, grandTotMoistStack = 0, grandPendStack = 0;
    int grandPrevDm = 0, grandTodayDm = 0, grandTotDm = 0;
    int grandPrevFci = 0, grandTodayFci = 0, grandTotFci = 0;
    int grandFciInsp = 0;

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
            // Reset totals on every binding call
            currentRegion = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_District_Wise_Online_Moisture_Report", con);
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
            string regionName = DataBinder.Eval(e.Row.DataItem, "Region Name").ToString();

            if (string.IsNullOrEmpty(currentRegion))
            {
                currentRegion = regionName;
            }

            // Region Badalney par Sub Total add karein
            if (currentRegion != regionName)
            {
                Table tbl = (Table)gvReport.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentRegion = regionName;
                ResetSubTotalCounters();
            }

            // Sub Total and Grand Total calculation logic combined safely
            int totStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack"));
            int prevStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack"));
            int todayStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Stack"));
            int totMoist = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Stack"));
            int pendStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending Stack"));
            int pDm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Sent_DM"));
            int tDm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Sent_DM"));
            int totDm = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Sent_DM"));
            int pFci = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Submit To FCI"));
            int tFci = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Submit To FCI"));
            int totFci = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Submit To FCI"));
            int fciInsp = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));

            // 1. Accumulate Subtotals
            subTotStack += totStack; subPrevStack += prevStack; subTodayStack += todayStack; subTotMoistStack += totMoist; subPendStack += pendStack;
            subPrevDm += pDm; subTodayDm += tDm; subTotDm += totDm;
            subPrevFci += pFci; subTodayFci += tFci; subTotFci += totFci;
            subFciInsp += fciInsp;

            // 2. Accumulate Grand Totals
            grandTotStack += totStack; grandPrevStack += prevStack; grandTodayStack += todayStack; grandTotMoistStack += totMoist; grandPendStack += pendStack;
            grandPrevDm += pDm; grandTodayDm += tDm; grandTotDm += totDm;
            grandPrevFci += pFci; grandTodayFci += tFci; grandTotFci += totFci;
            grandFciInsp += fciInsp;
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

        row.Cells.Add(new TableCell { Text = subTotStack.ToString() });
        row.Cells.Add(new TableCell { Text = subPrevStack.ToString() });
        row.Cells.Add(new TableCell { Text = subTodayStack.ToString() });
        row.Cells.Add(new TableCell { Text = subTotMoistStack.ToString() });
        row.Cells.Add(new TableCell { Text = subPendStack.ToString() });
        row.Cells.Add(new TableCell { Text = subPrevDm.ToString() });
        row.Cells.Add(new TableCell { Text = subTodayDm.ToString() });
        row.Cells.Add(new TableCell { Text = subTotDm.ToString() });
        row.Cells.Add(new TableCell { Text = subPrevFci.ToString() });
        row.Cells.Add(new TableCell { Text = subTodayFci.ToString() });
        row.Cells.Add(new TableCell { Text = subTotFci.ToString() });
        row.Cells.Add(new TableCell { Text = subFciInsp.ToString() });
        row.Cells.Add(new TableCell { Text = "" });

        for (int i = 1; i < row.Cells.Count; i++)
        {
            row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[i].VerticalAlign = VerticalAlign.Middle;
        }
        return row;
    }

    // Dynamic row injections at runtime processing boundary
    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            // 1. Last Region Group ka Sub Total add karein
            if (!string.IsNullOrEmpty(currentRegion))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            // 2. Pure Table ka final Grand Total dynamic row banakar custom add karein (Taki page break par repeat na ho)
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff"); // Soft light corporate blue rows
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 3;
            mainCell.HorizontalAlign = HorizontalAlign.Center;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandTotStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPrevStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTodayStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotMoistStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPendStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPrevDm.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTodayDm.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotDm.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPrevFci.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTodayFci.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotFci.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandFciInsp.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = "" });

            for (int i = 1; i < grandTotalRow.Cells.Count; i++)
            {
                grandTotalRow.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                grandTotalRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
            }

            tbl.Rows.Add(grandTotalRow); // Injected inside tbody dynamically!
        }
    }

    private void ResetSubTotalCounters()
    {
        subTotStack = 0; subPrevStack = 0; subTodayStack = 0; subTotMoistStack = 0; subPendStack = 0;
        subPrevDm = 0; subTodayDm = 0; subTotDm = 0;
        subPrevFci = 0; subTodayFci = 0; subTotFci = 0;
        subFciInsp = 0;
    }

    private void ResetGrandTotalCounters()
    {
        grandTotStack = 0; grandPrevStack = 0; grandTodayStack = 0; grandTotMoistStack = 0; grandPendStack = 0;
        grandPrevDm = 0; grandTodayDm = 0; grandTotDm = 0;
        grandPrevFci = 0; grandTodayFci = 0; grandTotFci = 0;
        grandFciInsp = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Online_Moisture_Report.xls");
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
                            <th colspan='16' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='16' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>District Wise Online Moisture Report</th>
                        </tr>
                        <tr>
                            <td colspan='8' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Report Type: Online System Sync</td>
                            <td colspan='8' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>Generated On: {0}</td>
                        </tr>
                        <tr><td colspan='16' style='border:none;'>&nbsp;</td></tr>
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