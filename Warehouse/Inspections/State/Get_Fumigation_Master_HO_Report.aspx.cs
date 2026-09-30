using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Get_Fumigation_Master_HO_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Region Accumulators
    int regGodowns = 0, regOnline = 0, regCovered = 0, regOnlineFum = 0, regOfflineFum = 0, regTotFum = 0, regPendGdn = 0, regPending = 0, regOpened = 0;

    // Grand Total Accumulators
    int grandGodowns = 0, grandOnline = 0, grandCovered = 0, grandOnlineFum = 0, grandOfflineFum = 0, grandTotFum = 0, grandPendGdn = 0, grandPending = 0, grandOpened = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindDetailedReport();
        }
    }

    private DataTable FetchFumigationDataset()
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetFumigationMasterHOReport", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_Id", 0);
                cmd.Parameters.AddWithValue("@DistrictId", 0);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Database Query Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindDetailedReport()
    {
        currentRegion = string.Empty;
        ResetRegionCounters();
        ResetGrandTotalCounters();

        DataTable dt = FetchFumigationDataset();
        gvDetails.DataSource = dt;
        gvDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvDetails.UseAccessibleHeader = true;
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Table tbl = (Table)gvDetails.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentRegion = regionName;
                ResetRegionCounters();
            }

            int godowns = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godown"));
            int online = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Stack"));
            int covered = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Godowns_Covered"));
            int onlineFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            int offlineFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            int totFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            int pendgdn = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Pending_Godown"));
            int pending = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            int opened = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            regGodowns += godowns; regOnline += online; regCovered += covered; regOnlineFum += onlineFum; regOfflineFum += offlineFum; regTotFum += totFum; regPendGdn += pendgdn; regPending += pending; regOpened += opened;
            grandGodowns += godowns; grandOnline += online; grandCovered += covered; grandOnlineFum += onlineFum; grandOfflineFum += offlineFum; grandTotFum += totFum; grandPendGdn += pendgdn; grandPending += pending; grandOpened += opened;

            // Safe dynamic checking to avoid ArgumentOutOfRangeException
            for (int i = 3; i < e.Row.Cells.Count; i++)
            {
                if (i == 12)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; font-weight:bold; mso-number-format:'0.00%';");
                }
                else
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
                }
            }
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = "subtotal-row";

        TableCell cell = new TableCell { Text = currentRegion + " - Region Sub Total", ColumnSpan = 3 };
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
        row.Cells.Add(cell);

        row.Cells.Add(createTotalCell(regGodowns.ToString("N0")));
        row.Cells.Add(createTotalCell(regOnline.ToString("N0")));
        row.Cells.Add(createTotalCell(regCovered.ToString("N0")));
        row.Cells.Add(createTotalCell(regOnlineFum.ToString("N0")));
        row.Cells.Add(createTotalCell(regOfflineFum.ToString("N0")));
        row.Cells.Add(createTotalCell(regTotFum.ToString("N0")));
        row.Cells.Add(createTotalCell(regPendGdn.ToString("N0")));
        row.Cells.Add(createTotalCell(regPending.ToString("N0")));
        row.Cells.Add(createTotalCell(regOpened.ToString("N0")));

        // Percentage calculated explicitly for total matching
        decimal subPercentage = regOnline > 0 ? ((decimal)regTotFum / (decimal)regOnline) * 100 : 0;
        row.Cells.Add(createTotalCell(subPercentage.ToString("0.00") + "%"));

        return row;
    }

    protected void gvDetails_DataBound(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvDetails.Controls[0];

            if (!string.IsNullOrEmpty(currentRegion))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "Grand Total Summary :", ColumnSpan = 3 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandGodowns.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnline.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandCovered.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOfflineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandTotFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPendGdn.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPending.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpened.ToString("N0")));

            decimal grandPercentage = grandOnline > 0 ? ((decimal)grandTotFum / (decimal)grandOnline) * 100 : 0;
            grandTotalRow.Cells.Add(createTotalCell(grandPercentage.ToString("0.00") + "%"));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important;");
        return cell;
    }

    private void ResetRegionCounters() { regGodowns = 0; regOnline = 0; regCovered = 0; regOnlineFum = 0; regOfflineFum = 0; regTotFum = 0; regPendGdn = 0; regPending = 0; regOpened = 0; }
    private void ResetGrandTotalCounters() { grandGodowns = 0; grandOnline = 0; grandCovered = 0; grandOnlineFum = 0; grandOfflineFum = 0; grandTotFum = 0; grandPendGdn = 0; grandPending = 0; grandOpened = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = FetchFumigationDataset();
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Fumigation_Master_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-right { text-align:right !important; mso-number-format:\\#\\,\\#\\#0; } .text-center { text-align:center !important; }");
        sb.Append(".subtotal-row td { background-color: #f1f5f9 !important; font-weight:bold !important; color:#1e3a8a !important; }");
        sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #2563eb; border-bottom:2px solid #1e3a8a; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='13' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.Append("<tr><th colspan='13' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>District Wise NCCF Moisture & Fumigation Status Details</th></tr>");
        sb.AppendFormat("<tr><td colspan='13' style='text-align:right; font-weight:bold;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='13' style='border:none;'>&nbsp;</td></tr>");

        sb.Append("<tr><th>S.No.</th><th>Regionnm</th><th>District_Name</th><th>Total_Godown</th><th>Total_Online_Stack</th><th>Total_Godowns_Covered</th>");
        sb.Append("<th>Total_Online_Fumigated_Stack</th><th>Total_Offline_Fumigated</th><th>Total_Fumigation_Stack</th><th>Total_Pending_Godown</th><th>Pending_Stack_For_Fumigation</th><th>Total_Opened_Stacks</th><th>Fumigation_Percentage</th></tr>");

        string loopRegion = string.Empty;
        int subG = 0, subO = 0, subC = 0, subOF = 0, subOffF = 0, subT = 0, subPg = 0, subP = 0, subOp = 0;
        int totG = 0, totO = 0, totC = 0, totOF = 0, totOffF = 0, totT = 0, totPg = 0, totP = 0, totOp = 0;
        int sNo = 1;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            string rName = row["Regionnm"].ToString();

            if (string.IsNullOrEmpty(loopRegion)) loopRegion = rName;

            if (loopRegion != rName)
            {
                decimal subPerc = subO > 0 ? ((decimal)subT / (decimal)subO) * 100 : 0;
                sb.AppendFormat("<tr class='subtotal-row'><td colspan='3' style='text-align:right; font-weight:bold;'>{0} - Region Sub Total</td>", loopRegion);
                sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right'>{7}</td><td class='text-right'>{8}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{9}%</td></tr>", subG, subO, subC, subOF, subOffF, subT, subPg, subP, subOp, subPerc.ToString("F2"));

                subG = 0; subO = 0; subC = 0; subOF = 0; subOffF = 0; subT = 0; subPg = 0; subP = 0; subOp = 0;
                loopRegion = rName;
            }

            int g = Convert.ToInt32(row["Total_Godown"]);
            int o = Convert.ToInt32(row["Total_Online_Stack"]);
            int c = Convert.ToInt32(row["Total_Godowns_Covered"]);
            int of = Convert.ToInt32(row["Total_Online_Fumigated_Stack"]);
            int offf = Convert.ToInt32(row["Total_Offline_Fumigated"]);
            int t = Convert.ToInt32(row["Total_Fumigation_Stack"]);
            int pg = Convert.ToInt32(row["Total_Pending_Godown"]);
            int p = Convert.ToInt32(row["Pending_Stack_For_Fumigation"]);
            int op = Convert.ToInt32(row["Total_Opened_Stacks"]);

            // Database value ko explicitly decimal parse kiya format ke liye
            decimal pct = 0;
            if (row["Fumigation_Percentage"] != DBNull.Value)
            {
                pct = Convert.ToDecimal(row["Fumigation_Percentage"]);
            }

            subG += g; subO += o; subC += c; subOF += of; subOffF += offf; subT += t; subPg += pg; subP += p; subOp += op;
            totG += g; totO += o; totC += c; totOF += of; totOffF += offf; totT += t; totPg += pg; totP += p; totOp += op;

            sb.AppendFormat("<tr><td class='text-center'>{0}</td><td>{1}</td><td>{2}</td>", sNo++, rName, row["District_Name"]);
            sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right'>{7}</td><td class='text-right'>{8}</td><td class='text-right' style='mso-number-format:\"0.00%\";'>{9}%</td></tr>", g, o, c, of, offf, t, pg, p, op, pct.ToString("F2"));
        }

        if (!string.IsNullOrEmpty(loopRegion))
        {
            decimal subPerc = subO > 0 ? ((decimal)subT / (decimal)subO) * 100 : 0;
            sb.AppendFormat("<tr class='subtotal-row'><td colspan='3' style='text-align:right; font-weight:bold;'>{0} - Region Sub Total</td>", loopRegion);
            sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right'>{7}</td><td class='text-right'>{8}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{9}%</td></tr>", subG, subO, subC, subOF, subOffF, subT, subPg, subP, subOp, subPerc.ToString("F2"));
        }

        decimal grandPerc = grandOnline > 0 ? ((decimal)grandTotFum / (decimal)grandOnline) * 100 : 0;
        sb.Append("<tr class='grandtotal-row'><td colspan='3' style='text-align:right; font-weight:bold;'>Grand Total Summary :</td>");
        sb.AppendFormat("<td class='text-right'>{0}</td><td class='text-right'>{1}</td><td class='text-right'>{2}</td><td class='text-right'>{3}</td><td class='text-right'>{4}</td><td class='text-right'>{5}</td><td class='text-right'>{6}</td><td class='text-right'>{7}</td><td class='text-right'>{8}</td><td class='text-right' style='font-weight:bold; mso-number-format:\"0.00%\";'>{9}%</td></tr>", totG, totO, totC, totOF, totOffF, totT, totPg, totP, totOp, grandPerc.ToString("F2"));
        sb.Append("</table>");

        Response.Write(sb.ToString());
        Response.Flush(); Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}