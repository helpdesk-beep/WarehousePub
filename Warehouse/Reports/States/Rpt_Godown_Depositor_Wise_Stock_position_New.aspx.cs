using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Storage_Capacity_Reports_Rpt_Godown_Depositor_Wise_Stock_position_New : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtpaymentdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fillCommodity();
            fillDepositor();
        }
    }

    private void fillDepositor()
    {
        try
        {
            string query = "SELECT Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID in ('129','181','184','4679','10535','15478') ORDER BY Depositor_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddldepositor.DataSource = dt;
                    ddldepositor.DataTextField = "Depositor_Name";
                    ddldepositor.DataValueField = "Depositor_ID";
                    ddldepositor.DataBind();
                    ddldepositor.Items.Insert(0, new ListItem("All Depositors", "0"));
                    ddldepositor.Items[0].Selected = true;
                }
            }
        }
        catch { }
    }

    private void fillCommodity()
    {
        try
        {
            string query = "SELECT DISTINCT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY ORDER BY Commodity_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlComodity.DataSource = dt;
                    ddlComodity.DataTextField = "Commodity_Name";
                    ddlComodity.DataValueField = "Commodity_Id";
                    ddlComodity.DataBind();
                    ddlComodity.Items.Insert(0, new ListItem("All Commodities", "0"));
                    ddlComodity.Items[0].Selected = true;
                }
            }
        }
        catch { }
    }

    private string GetCsvFromCheckBoxList(CheckBoxList cbl)
    {
        if (cbl.Items.Count > 0 && cbl.Items[0].Selected)
        {
            return "0";
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i < cbl.Items.Count; i++)
        {
            if (cbl.Items[i].Selected)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(cbl.Items[i].Value);
            }
        }
        return sb.Length == 0 ? "0" : sb.ToString();
    }

    protected string getDate_MDY(string inDate)
    {
        if (string.IsNullOrEmpty(inDate))
        {
            return DateTime.Now.ToString("MM/dd/yyyy");
        }
        try
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy" };
            return DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
        }
        catch
        {
            return DateTime.Now.ToString("MM/dd/yyyy");
        }
    }

    //private void FillBillDetailsInGrid()
    //{
    //    try
    //    {
    //        using (SqlCommand cmd = new SqlCommand("usp_Get_Godown_depositor_wise_StockPosition_New_Report", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text.Trim()));
    //            cmd.Parameters.AddWithValue("@DepositorID", GetCsvFromCheckBoxList(ddldepositor));
    //            cmd.Parameters.AddWithValue("@CommodityID", GetCsvFromCheckBoxList(ddlComodity));
    //            cmd.CommandTimeout = 300;
    //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //            {
    //                DataTable dt = new DataTable();
    //                sda.Fill(dt);

    //                if (dt.Rows.Count > 0)
    //                {
    //                    GV_StockPositionDetails.DataSource = dt;
    //                    GV_StockPositionDetails.DataBind();

    //                    ApplyColumnVisibility();

    //                    GV_StockPositionDetails.UseAccessibleHeader = true;
    //                    GV_StockPositionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                }
    //                else
    //                {
    //                    GV_StockPositionDetails.DataSource = null;
    //                    GV_StockPositionDetails.DataBind();
    //                }
    //            }
    //        }
    //    }
    //    catch { }
    //}


    private void FillBillDetailsInGrid()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("usp_Get_Godown_depositor_wise_StockPosition_New_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@DepositorID", GetCsvFromCheckBoxList(ddldepositor));
                cmd.Parameters.AddWithValue("@CommodityID", GetCsvFromCheckBoxList(ddlComodity));

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // 1. Pehle check karein ki kaun-kaun se Financial Years screen par SELECTED hain
                        List<string> selectedYears = new List<string>();
                        foreach (ListItem item in cblColumns.Items)
                        {
                            if (item.Selected)
                            {
                                selectedYears.Add(item.Text.Trim()); // Jaise "2017-18", "2018-19" etc.
                            }
                        }

                        // 2. Ek naya filtered DataTable banayein jisme 0 waale records nahi aayenge
                        DataTable filteredDt = dt.Clone();
                        foreach (DataRow row in dt.Rows)
                        {
                            decimal rowTotal = 0;
                            foreach (string year in selectedYears)
                            {
                                if (dt.Columns.Contains(year) && row[year] != DBNull.Value)
                                {
                                    decimal val = 0;
                                    if (decimal.TryParse(row[year].ToString(), out val))
                                    {
                                        rowTotal += val;
                                    }
                                }
                            }

                            // 3. Agar selected years ka total 0 nahi hai, tabhi row ko include karein
                            if (rowTotal != 0)
                            {
                                filteredDt.ImportRow(row);
                            }
                        }

                        // 4. Agar filtered data mein records hain, tabhi GridView bind karein
                        if (filteredDt.Rows.Count > 0)
                        {
                            GV_StockPositionDetails.DataSource = filteredDt;
                            GV_StockPositionDetails.DataBind();

                            ApplyColumnVisibility();

                            GV_StockPositionDetails.UseAccessibleHeader = true;
                            GV_StockPositionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        }
                        else
                        {
                            GV_StockPositionDetails.DataSource = null;
                            GV_StockPositionDetails.DataBind();
                        }
                    }
                    else
                    {
                        GV_StockPositionDetails.DataSource = null;
                        GV_StockPositionDetails.DataBind();
                    }
                }
            }
        }
        catch { }
    }


    private void ApplyColumnVisibility()
    {
        // Base Header Text Map mapping array to explicitly identify year columns by text instead of fragile indices
        string[] yearHeaders = { "2017-18", "2018-19", "2019-20", "2020-21", "2021-22", "2022-23", "2023-24", "2024-25", "2025-26", "2026-27" };

        // S.No, District, Branch, Godown, Commodity (0 to 4) always visible
        for (int idx = 0; idx <= 4; idx++)
        {
            GV_StockPositionDetails.Columns[idx].Visible = true;
        }

        // Explicitly iterate through columns to cross reference data matching visibility model context bounds
        for (int i = 0; i < yearHeaders.Length; i++)
        {
            int targetColumnIndex = i + 5; // Index 5 matches the start of financial years
            ListItem item = cblColumns.Items.FindByText(yearHeaders[i]);
            if (item != null)
            {
                GV_StockPositionDetails.Columns[targetColumnIndex].Visible = item.Selected;
            }
        }

        // Total column (Index 15) is always visible
        GV_StockPositionDetails.Columns[15].Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

    protected void cblColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Re-trigger database binding or persist active data matrix state to structural grid
        if (GV_StockPositionDetails.Rows.Count > 0 || !string.IsNullOrEmpty(txtpaymentdate.Text))
        {
            FillBillDetailsInGrid();
        }
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (GV_StockPositionDetails.Rows.Count > 0)
        {
            InjectCalculatedSummaryMatrix(GV_StockPositionDetails);
        }
        base.Render(writer);
    }

    // 1. UPDATED LOGIC ENGINE (Column shifts handled properly)
    private void InjectCalculatedSummaryMatrix(GridView targetGrid)
    {
        Table gridTable = (Table)targetGrid.Controls[0];

        decimal[] subTotals = new decimal[11];
        decimal[] grandTotals = new decimal[11];

        string lastDistrictName = gridTable.Rows[1].Cells[1].Text.Trim();

        for (int k = 1; k < gridTable.Rows.Count; k++)
        {
            GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
            if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
            {
                if (currentRow.CssClass == "subtotal-row" || currentRow.CssClass == "grandtotal-row")
                    continue;

                string currentDistrictName = currentRow.Cells[1].Text.Trim();

                if (currentDistrictName != lastDistrictName)
                {
                    GridViewRow subRow = CreateSummaryRow(lastDistrictName + " - Sub Total", subTotals, "subtotal-row");
                    gridTable.Rows.AddAt(k, subRow);
                    k++;

                    Array.Clear(subTotals, 0, subTotals.Length);
                    lastDistrictName = currentDistrictName;
                }

                decimal currentActiveRowTotal = 0;

                for (int i = 0; i <= 9; i++)
                {
                    int yearColIndex = i + 5;
                    if (targetGrid.Columns[yearColIndex].Visible)
                    {
                        decimal cellValue = ParseLabelControlValue(currentRow.Cells[yearColIndex], "lblCY" + (i + 1));
                        currentActiveRowTotal += cellValue;

                        subTotals[i] += cellValue;
                        grandTotals[i] += cellValue;
                    }
                }

                Label lblRowTotal = (Label)currentRow.FindControl("lblTotal");
                if (lblRowTotal != null)
                {
                    lblRowTotal.Text = currentActiveRowTotal.ToString("N2");
                }

                subTotals[10] += currentActiveRowTotal;
                grandTotals[10] += currentActiveRowTotal;

                for (int m = 5; m <= 15; m++)
                {
                    if (targetGrid.Columns[m].Visible)
                    {
                        currentRow.Cells[m].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
                    }
                }
            }
        }

        GridViewRow finalSubRow = CreateSummaryRow(lastDistrictName + " - Sub Total", subTotals, "subtotal-row");
        gridTable.Rows.Add(finalSubRow);

        GridViewRow grandRow = CreateSummaryRow("Grand Total Summary", grandTotals, "grandtotal-row");
        gridTable.Rows.Add(grandRow);
    }

    // 2. UPDATED METHOD TO CREATE SUMMARY ROW WITH CORRECT COLUMN SPAN
    private GridViewRow CreateSummaryRow(string titleText, decimal[] totalsArray, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        TableCell mainLabelCell = new TableCell { Text = titleText, ColumnSpan = 5, HorizontalAlign = HorizontalAlign.Right };
        mainLabelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important; border-top:1px solid #cbd5e1 !important; border-bottom:1px solid #cbd5e1 !important;");
        row.Cells.Add(mainLabelCell);

        for (int i = 0; i <= 9; i++)
        {
            int colIndex = i + 5;
            if (GV_StockPositionDetails.Columns[colIndex].Visible)
            {
                TableCell cell = new TableCell { Text = totalsArray[i].ToString("N2"), HorizontalAlign = HorizontalAlign.Right };
                cell.CssClass = "text-right-align";
                cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00; border-top:1px solid #cbd5e1 !important; border-bottom:1px solid #cbd5e1 !important;");
                row.Cells.Add(cell);
            }
        }

        TableCell totalSumCell = new TableCell { Text = totalsArray[10].ToString("N2"), HorizontalAlign = HorizontalAlign.Right };
        totalSumCell.CssClass = "text-right-align";
        totalSumCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00; border-top:1px solid #cbd5e1 !important; border-bottom:1px solid #cbd5e1 !important;");
        row.Cells.Add(totalSumCell);

        return row;
    }

    private decimal ParseLabelControlValue(TableCell cell, string controlId)
    {
        Label lbl = (Label)cell.FindControl(controlId);
        if (lbl != null && !string.IsNullOrEmpty(lbl.Text))
        {
            decimal output = 0;
            string cleanString = lbl.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
            decimal.TryParse(cleanString, out output);
            return output;
        }
        return 0;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        if (GV_StockPositionDetails.Rows.Count == 0)
        {
            FillBillDetailsInGrid();
        }
        if (GV_StockPositionDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Depositor_Commodity_Wise_Stock_Position.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                GV_StockPositionDetails.GridLines = GridLines.Both;
                InjectCalculatedSummaryMatrix(GV_StockPositionDetails);

                int totalActiveColumns = 0;
                for (int idx = 0; idx < GV_StockPositionDetails.Columns.Count; idx++)
                {
                    if (GV_StockPositionDetails.Columns[idx].Visible) totalActiveColumns++;
                }

                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; border:1px solid #cbd5e1; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string activeSelectionDate = txtpaymentdate.Text.Trim();

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='{2}' style='font-size:16pt; font-weight:bold; background-color:#0d6efd; color:#ffffff; text-align:center;'>M.P. WAREHOUSING & LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='{2}' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#0d6efd; text-align:center;'>District, Godown, Depositor, Commodity Wise Stock Position Report</th></tr>
                        <tr><td colspan='{3}' style='text-align:left; font-weight:bold; color:#475569;'>Stock Position As On: {0}</td><td colspan='{4}' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='{2}' style='border:none;'>&nbsp;</td></tr>
                    </table>",
                    activeSelectionDate,
                    dateTimeStr,
                    totalActiveColumns,
                    totalActiveColumns / 2 + 1,
                    totalActiveColumns - (totalActiveColumns / 2 + 1)
                );

                Response.Write(customExcelHeader);
                GV_StockPositionDetails.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* REQUIRED */
    }
}