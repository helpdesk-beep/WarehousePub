using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_OnlineOffline_Moisture_Details_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Subtotal properties per Region
    int subTotalStack = 0, subOnline = 0, subOffline = 0, subTotalMoisture = 0;
    int subPendingStack = 0, subSentDM = 0, subFciInspected = 0, subPendingFci = 0;

    // Grand total properties 
    int grandTotalStack = 0, grandOnline = 0, grandOffline = 0, grandTotalMoisture = 0;
    int grandPendingStack = 0, grandSentDM = 0, grandFciInspected = 0, grandPendingFci = 0;

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

            SqlCommand cmd = new SqlCommand("sp_District_Wise_OnlineOffline_Moisture_Details_For_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RegionID", 0);

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

            if (currentRegion != regionName)
            {
                Table tbl = (Table)gvReport.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentRegion = regionName;
                ResetSubTotalCounters();
            }

            int tStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Stack"));
            int online = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Online Moisture Up to Date"));
            int offline = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Offline Moisture Entry"));
            int tMoisture = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture"));
            int pStack = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending Stack For Moisture"));
            int sentDM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stack Moisture Sent to DM MPSCSC/FCI"));
            int fciInsp = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));
            int pFci = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending at FCI"));

            // Sum up Subtotals
            subTotalStack += tStack; subOnline += online; subOffline += offline; subTotalMoisture += tMoisture;
            subPendingStack += pStack; subSentDM += sentDM; subFciInspected += fciInsp; subPendingFci += pFci;

            // Sum up Grand Totals
            grandTotalStack += tStack; grandOnline += online; grandOffline += offline; grandTotalMoisture += tMoisture;
            grandPendingStack += pStack; grandSentDM += sentDM; grandFciInspected += fciInsp; grandPendingFci += pFci;
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

        row.Cells.Add(new TableCell { Text = subTotalStack.ToString() });
        row.Cells.Add(new TableCell { Text = subOnline.ToString() });
        row.Cells.Add(new TableCell { Text = subOffline.ToString() });
        row.Cells.Add(new TableCell { Text = subTotalMoisture.ToString() });
        row.Cells.Add(new TableCell { Text = subPendingStack.ToString() });
        row.Cells.Add(new TableCell { Text = subSentDM.ToString() });
        row.Cells.Add(new TableCell { Text = subFciInspected.ToString() });
        row.Cells.Add(new TableCell { Text = subPendingFci.ToString() });

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

            // Final Region Group Sub Total
            if (!string.IsNullOrEmpty(currentRegion))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            // Injects dynamic final Grand Total row inside tbody context safely to block print repeating duplicates
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

            grandTotalRow.Cells.Add(new TableCell { Text = grandTotalStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandOnline.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandOffline.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotalMoisture.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPendingStack.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandSentDM.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandFciInspected.ToString() });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPendingFci.ToString() });

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
        subTotalStack = 0; subOnline = 0; subOffline = 0; subTotalMoisture = 0;
        subPendingStack = 0; subSentDM = 0; subFciInspected = 0; subPendingFci = 0;
    }

    private void ResetGrandTotalCounters()
    {
        grandTotalStack = 0; grandOnline = 0; grandOffline = 0; grandTotalMoisture = 0;
        grandPendingStack = 0; grandSentDM = 0; grandFciInspected = 0; grandPendingFci = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_OnlineOffline_Moisture_Details_For_HO.xls");
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
                            <th colspan='11' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='11' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>District Wise Online & Offline Moisture Details For HO</th>
                        </tr>
                        <tr>
                            <td colspan='5' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Report Type: Consolidated System Sync</td>
                            <td colspan='6' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>Generated On: {0}</td>
                        </tr>
                        <tr><td colspan='11' style='border:none;'>&nbsp;</td></tr>
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