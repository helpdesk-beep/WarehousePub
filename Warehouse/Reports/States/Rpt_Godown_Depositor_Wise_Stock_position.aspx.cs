using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_State_Rpt_Godown_Depositor_Wise_Stock_position : System.Web.UI.Page
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
            string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID in ('129','181','184','4679','10535','15478') order by Depositor_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlDepositor.DataSource = dt;
                    ddlDepositor.DataTextField = "Depositor_Name";
                    ddlDepositor.DataValueField = "Depositor_ID";
                    ddlDepositor.DataBind();
                    ddlDepositor.Items.Insert(0, new ListItem("--Select All--", "0"));
                }
            }
        }
        catch { }
    }

    private void fillCommodity()
    {
        try
        {
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id in ('63','64','33','92','75','27') order by Commodity_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ddlcommodity.DataSource = dt;
                    ddlcommodity.DataTextField = "Commodity_Name";
                    ddlcommodity.DataValueField = "Commodity_Id";
                    ddlcommodity.DataBind();
                    ddlcommodity.Items.Insert(0, new ListItem("--Select All--", "0"));
                }
            }
        }
        catch { }
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

    private void FillBillDetailsInGrid()
    {
        try
        {
            lblmsg.Text = "";
            using (SqlCommand cmd = new SqlCommand("usp_Get_Godown_depositor_wise_StockPosition", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue == "" ? "0" : ddlDepositor.SelectedValue);
                cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue == "" ? "0" : ddlcommodity.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GV_StockPositionDetails.DataSource = dt;
                        GV_StockPositionDetails.DataBind();

                        GV_StockPositionDetails.UseAccessibleHeader = true;
                        GV_StockPositionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                    else
                    {
                        GV_StockPositionDetails.DataSource = null;
                        GV_StockPositionDetails.DataBind();
                        lblmsg.Text = "No records captured inside parameters range scope context.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error Context: " + ex.Message;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

    // SCREEN RENDER PIPELINE: Generates runtime HTML rows for screen display & print layouts
    protected override void Render(HtmlTextWriter writer)
    {
        if (GV_StockPositionDetails.Rows.Count > 0)
        {
            InjectSubTotalsRows(GV_StockPositionDetails);
        }
        base.Render(writer);
    }

    // CORE MATHEMATICAL ALGORITHM ENGINE: Re-usable matrix manipulator to prevent duplicate script logic leaks
    private void InjectSubTotalsRows(GridView targetGrid)
    {
        Table gridTable = (Table)targetGrid.Controls[0];
        decimal[] subTotals = new decimal[11];
        decimal[] grandTotals = new decimal[11];

        string lastGodownName = gridTable.Rows[1].Cells[2].Text.Trim();

        for (int k = 1; k < gridTable.Rows.Count; k++)
        {
            GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
            if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
            {
                string currentGodownName = currentRow.Cells[2].Text.Trim();

                if (currentGodownName != lastGodownName)
                {
                    GridViewRow subRow = CreateSummaryRow(lastGodownName + " - Sub Total", subTotals, "subtotal-row");
                    gridTable.Rows.AddAt(k, subRow);
                    k++;

                    Array.Clear(subTotals, 0, subTotals.Length);
                    lastGodownName = currentGodownName;
                }

                for (int i = 0; i <= 10; i++)
                {
                    decimal val = ParseDecimalCell(currentRow.Cells[i + 4]);
                    subTotals[i] += val;
                    grandTotals[i] += val;
                }
            }
        }

        GridViewRow finalSubRow = CreateSummaryRow(lastGodownName + " - Sub Total", subTotals, "subtotal-row");
        gridTable.Rows.Add(finalSubRow);

        GridViewRow grandRow = CreateSummaryRow("Grand Total Summary", grandTotals, "grandtotal-row");
        gridTable.Rows.Add(grandRow);
    }

    private GridViewRow CreateSummaryRow(string titleText, decimal[] totalsArray, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        TableCell mainLabelCell = new TableCell { Text = titleText, ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
        mainLabelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
        row.Cells.Add(mainLabelCell);

        for (int i = 0; i <= 10; i++)
        {
            TableCell cell = new TableCell { Text = totalsArray[i].ToString("N2"), HorizontalAlign = HorizontalAlign.Right };
            cell.CssClass = "text-right-align";
            cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            row.Cells.Add(cell);
        }

        row.Cells.Add(new TableCell { Text = "" });
        return row;
    }

    private decimal ParseDecimalCell(TableCell cell)
    {
        decimal output = 0;
        string cleanString = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        decimal.TryParse(cleanString, out output);
        return output;
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 4; i <= 14; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    // FIXED 100% WORKING EXCEL GENERATION: Isolated GridView stream rendering bypasses ASP.NET page wrapper breakages
    protected void btnExcel_Click(object sender, EventArgs e)
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

                // Forces subtotal calculations directly over target excel rendering object memory tree
                InjectSubTotalsRows(GV_StockPositionDetails);

                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string activeSelectionDate = txtpaymentdate.Text.Trim();

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='15' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>M.P. WAREHOUSING & LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='15' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District, Godown, Depositor, Commodity Wise Stock Position Report</th></tr>
                        <tr><td colspan='8' style='text-align:left; font-weight:bold; color:#475569;'>Stock Position As On: {0} | Commodity: {1}</td><td colspan='7' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {2}</td></tr>
                        <tr><td colspan='15' style='border:none;'>&nbsp;</td></tr>
                    </table>", activeSelectionDate, ddlcommodity.SelectedItem.Text, dateTimeStr);

                Response.Write(customExcelHeader);

                // Directly serializes isolated compiled object cleanly into streaming pipe output
                GV_StockPositionDetails.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* REQUIRED: Bypasses server container limits during explicit grid excel conversion streams */
    }
}