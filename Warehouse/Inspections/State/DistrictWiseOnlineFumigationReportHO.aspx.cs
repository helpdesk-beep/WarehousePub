using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_DistrictWiseOnlineFumigationReportHO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindFumigationReport();
        }
    }

    private void BindFumigationReport()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Online_Fumigation_Summary_HO", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    sda.Fill(dt);

                    gvFumigation.DataSource = dt;
                    gvFumigation.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvFumigation.UseAccessibleHeader = true;
                        gvFumigation.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution failure logging trace: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        finally
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }

    // SCREEN DISPLAY PIPELINE: Renders the subtotal and grand total blocks dynamically into the page context
    protected override void Render(HtmlTextWriter writer)
    {
        if (gvFumigation.Rows.Count > 0)
        {
            InjectHierarchicalSummaryRows(gvFumigation);
        }
        base.Render(writer);
    }

    // CORE MATHEMATICAL ALGORITHM ENGINE: Processes dynamic Region Wise Sub-Totals & State Grand Totals smoothly
    private void InjectHierarchicalSummaryRows(GridView targetGrid)
    {
        Table gridTable = (Table)targetGrid.Controls[0];

        // Group-wise accumulator pools array 
        int[] rTotals = new int[5]; // 0:Godowns, 1:Yesterday, 2:Today, 3:Completed, 4:Opened
        int[] gTotals = new int[5];

        // Region_Name column text extraction lock mapped safely at Cells[1]
        string lastRegionName = gridTable.Rows[1].Cells[1].Text.Trim();

        for (int k = 1; k < gridTable.Rows.Count; k++)
        {
            GridViewRow currentRow = gridTable.Rows[k] as GridViewRow;
            if (currentRow != null && currentRow.RowType == DataControlRowType.DataRow)
            {
                string currentRegionName = currentRow.Cells[1].Text.Trim();

                if (currentRegionName != lastRegionName)
                {
                    // Region Name switches -> Inject dynamic Sub-Total row summary lines
                    GridViewRow subRow = CreateSummaryRow(lastRegionName + " Region Total :", rTotals, "subtotal-row");
                    gridTable.Rows.AddAt(k, subRow);
                    k++; // Advance loop track counter offset mapped index

                    Array.Clear(rTotals, 0, rTotals.Length);
                    lastRegionName = currentRegionName;
                }

                // FIXED COLUMN MATRIX: Extract visible index 4 (Total_Godowns) to index 8 (Opened_Stacks) values cleanly
                for (int i = 0; i < 5; i++)
                {
                    int val = ParseIntegerCell(currentRow.Cells[i + 4]);
                    rTotals[i] += val;
                    gTotals[i] += val;
                }
            }
        }

        // Append last trailing pending dataset group row subtotal balance lines
        GridViewRow finalSubRow = CreateSummaryRow(lastRegionName + " Region Total :", rTotals, "subtotal-row");
        gridTable.Rows.Add(finalSubRow);

        // Append full state level grand total position statement summary row
        GridViewRow grandRow = CreateSummaryRow("State Grand Total Summary :", gTotals, "grandtotal-row");
        gridTable.Rows.Add(grandRow);
    }

    private GridViewRow CreateSummaryRow(string titleText, int[] totalsArray, string cssClass)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.CssClass = cssClass;
        row.Font.Bold = true;

        // FIXED COLUMN SPAN: Covers S.No, Region Name, District Name, and Financial Year columns cleanly (Indices 0 to 3) -> ColumnSpan = 4
        TableCell mainLabelCell = new TableCell { Text = titleText, ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
        mainLabelCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:15px !important;");
        row.Cells.Add(mainLabelCell);

        // Bind calculated integer counts cleanly across the remaining columns elements (Columns 4 to 8)
        for (int i = 0; i < 5; i++)
        {
            TableCell cell = new TableCell { Text = totalsArray[i].ToString("N0"), HorizontalAlign = HorizontalAlign.Right };
            cell.CssClass = "text-right-align";
            cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0;");
            row.Cells.Add(cell);
        }

        // FIXED CODE: Extra trailing TableCell addition completely removed to prevent right-side alignment overflow box leak

        return row;
    }

    private int ParseIntegerCell(TableCell cell)
    {
        int output = 0;
        string cleanString = cell.Text.Replace("&nbsp;", "").Replace(",", "").Trim();
        int.TryParse(cleanString, out output);
        return output;
    }

    protected void gvFumigation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Forces formatting alignments right directly over visible content fields (Indices 4 to 8)
            for (int i = 4; i <= 8; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    // FIXED ISOLATED EXCEL CONVERSION STREAM: Forces total calculation injection directly into clean grid stream objects
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvFumigation.Rows.Count == 0)
        {
            BindFumigationReport();
        }
        if (gvFumigation.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DistrictWiseOnlineFumigationReport.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvFumigation.GridLines = GridLines.Both;

                // Force layout injection engine to parse totals directly onto grid controls context memory tree
                InjectHierarchicalSummaryRows(gvFumigation);

                Response.Write("<style> .text-right-align { text-align:right !important; } .subtotal-row { background-color: #fed7aa !important; font-weight:bold; } .grandtotal-row { background-color: #dbeafe !important; font-weight:bold; } td { white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>M.P. WAREHOUSING & LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise Stack Fumigation Progress Statement Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Report Scope: Head Office Consolidated View</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);

                // Directly serializes target isolated data table securely into streaming excel file pipelines
                gvFumigation.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* REQUIRED: Kept intentionally blank to bypass ASP.NET engine server-form control compliance checks */
    }
}