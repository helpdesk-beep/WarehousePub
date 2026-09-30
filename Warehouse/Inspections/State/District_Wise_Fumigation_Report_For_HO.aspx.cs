using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_Fumigation_Report_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Multi-Level Accumulators
    int regGodowns = 0, regOnline = 0, regFumigated = 0, regPending = 0, regOpened = 0;
    int grandGodowns = 0, grandOnline = 0, grandFumigated = 0, grandPending = 0, grandOpened = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["RegID"] != null)
            {
                // FIXED CLEANER: Strips away illegal URL encoded plus characters safely
                string cleanRegId = Request.QueryString["RegID"].ToString().Replace("+", "").Trim();
                lblRegionInfo.Text = cleanRegId;

                BindDetailedReport(cleanRegId);
            }
            else
            {
                // If direct landing occurs, fire default 0 state framework
                lblRegionInfo.Text = "All State Regions Consolidated";
                BindDetailedReport("0");
            }
        }
    }

    private void BindDetailedReport(string regionId)
    {
        try
        {
            currentRegion = string.Empty;
            ResetRegionCounters();
            ResetGrandTotalCounters();

            // Calls the newly optimized procedure that handles dynamic Region conditions
            SqlCommand cmd = new SqlCommand("sp_District_Wise_Fumigation_Moisture_Report_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Safe conversion parsing fallback layer
            int parsedRegId = 0;
            int.TryParse(regionId, out parsedRegId);

            cmd.Parameters.AddWithValue("@DistrictId", 0); // Fetches all districts for the target Region boundary

            // Note: If you want to restrict procedure logic by Region ID natively, pass it here
            // Since proc is currently district-specific, it will segment automatically on the Grid tree layer below

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Filter data client-side if a specific Region filter is requested out of the broad Proc dataset
            if (parsedRegId > 0 && dt.Rows.Count > 0)
            {
                // If procedure logic is broad, DataTable DataView handles memory segments filtering safely
                DataView dv = new DataView(dt);

                // Matches against procedure's Region_ID field context
                // Tip: Ensure procedure select list contains Region_ID for dataview row state mapping
                if (dt.Columns.Contains("Region_ID"))
                {
                    dv.RowFilter = "Region_ID = " + parsedRegId;
                    dt = dv.ToTable();
                }
            }

            gvDetails.DataSource = dt;
            gvDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvDetails.UseAccessibleHeader = true;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Failure: " + ex.Message.Replace("'", "\\'") + "');</script>");
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
            int fumigated = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigated_Stack"));
            int pending = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            int opened = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            regGodowns += godowns; regOnline += online; regFumigated += fumigated; regPending += pending; regOpened += opened;
            grandGodowns += godowns; grandOnline += online; grandFumigated += fumigated; grandPending += pending; grandOpened += opened;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell { Text = currentRegion + " - Region Sub Total", ColumnSpan = 4 };
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
        row.Cells.Add(cell);

        row.Cells.Add(createTotalCell(regGodowns.ToString("N0")));
        row.Cells.Add(createTotalCell(regOnline.ToString("N0")));
        row.Cells.Add(createTotalCell(regFumigated.ToString("N0")));
        row.Cells.Add(createTotalCell(regPending.ToString("N0")));
        row.Cells.Add(createTotalCell(regOpened.ToString("N0")));

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
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell { Text = "Grand Total", ColumnSpan = 4 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandGodowns.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnline.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandFumigated.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPending.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpened.ToString("N0")));

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

    private void ResetRegionCounters() { regGodowns = 0; regOnline = 0; regFumigated = 0; regPending = 0; regOpened = 0; }
    private void ResetGrandTotalCounters() { grandGodowns = 0; grandOnline = 0; grandFumigated = 0; grandPending = 0; grandOpened = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_NCCF_Fumigation_Details.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string incomingId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString().Replace("+", "").Trim() : "0";
                BindDetailedReport(incomingId);

                Response.Write("<style> td { mso-number-format:\\@; white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise NCCF Moisture & Fumigation Status Details</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Filtered Target Region ID: {0}</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", lblRegionInfo.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                gvDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}