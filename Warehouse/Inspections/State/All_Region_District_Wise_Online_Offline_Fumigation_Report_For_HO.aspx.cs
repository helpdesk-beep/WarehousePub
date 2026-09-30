using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_All_Region_District_Wise_Online_Offline_Fumigation_Report_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    string currentRegion = string.Empty;

    // Region Subtotal Accumulators
    int regGodowns = 0, regOnline = 0, regOnlineFum = 0, regOfflineFum = 0, regTotFum = 0, regPending = 0, regOpened = 0;

    // Global Grand Total Accumulators
    int grandGodowns = 0, grandOnline = 0, grandOnlineFum = 0, grandOfflineFum = 0, grandTotFum = 0, grandPending = 0, grandOpened = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["Region_ID"] != null)
            {
                string incomingRegId = Request.QueryString["Region_ID"].ToString().Trim();
                BindDetailedReport(incomingRegId);
            }
            else
            {
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

            SqlCommand cmd = new SqlCommand("sp_District_Wise_Online_Offline_Fumigation_Report_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            int parsedRegId = 0;
            int.TryParse(regionId, out parsedRegId);
            cmd.Parameters.AddWithValue("@Region_Id", parsedRegId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

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
            Response.Write("<script>alert('Execution Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
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

            // Injects Region level Sub-Total Row when group boundary shifts
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
            int onlineFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Online_Fumigated_Stack"));
            int offlineFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Offline_Fumigated"));
            int totFum = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Fumigation_Stack"));
            int pending = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Stack_For_Fumigation"));
            int opened = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));

            regGodowns += godowns; regOnline += online; regOnlineFum += onlineFum; regOfflineFum += offlineFum; regTotFum += totFum; regPending += pending; regOpened += opened;
            grandGodowns += godowns; grandOnline += online; grandOnlineFum += onlineFum; grandOfflineFum += offlineFum; grandTotFum += totFum; grandPending += pending; grandOpened += opened;

            // FIXED FOR SHRUNK INDEX: Data fields now start from index 3 up to 9
            for (int i = 3; i <= 9; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        // FIXED COLUMN SPAN: Set to 3 because District ID column is deleted (covers S.No, Region, District)
        TableCell cell = new TableCell { Text = currentRegion + " - Region Sub Total", ColumnSpan = 3 };
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
        row.Cells.Add(cell);

        row.Cells.Add(createTotalCell(regGodowns.ToString("N0")));
        row.Cells.Add(createTotalCell(regOnline.ToString("N0")));
        row.Cells.Add(createTotalCell(regOnlineFum.ToString("N0")));
        row.Cells.Add(createTotalCell(regOfflineFum.ToString("N0")));
        row.Cells.Add(createTotalCell(regTotFum.ToString("N0")));
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

            // FIXED COLUMN SPAN: Aligned perfectly to 3
            TableCell mainCell = new TableCell { Text = "Grand Total Summary", ColumnSpan = 3 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandGodowns.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnline.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOfflineFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandTotFum.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPending.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOpened.ToString("N0")));

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important; mso-number-format:\\#\\,\\#\\#0;");
        return cell;
    }

    private void ResetRegionCounters() { regGodowns = 0; regOnline = 0; regOnlineFum = 0; regOfflineFum = 0; regTotFum = 0; regPending = 0; regOpened = 0; }
    private void ResetGrandTotalCounters() { grandGodowns = 0; grandOnline = 0; grandOnlineFum = 0; grandOfflineFum = 0; grandTotFum = 0; grandPending = 0; grandOpened = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Fumigation_Details.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string incomingId = Request.QueryString["Region_ID"] != null ? Request.QueryString["Region_ID"].ToString() : "0";
                BindDetailedReport(incomingId);

                // FIXED EXCEL ROW CELL PARSER: Dynamic stripping based on text columns re-index 
                foreach (GridViewRow row in gvDetails.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell distCell = row.Cells[2]; // Target index shifted to 2 (District Name TemplateField)
                        if (distCell.Controls.Count > 0 && distCell.Controls[1] is HyperLink)
                        {
                            string textContent = ((HyperLink)distCell.Controls[1]).Text;
                            distCell.Controls.Clear();
                            distCell.Text = textContent.Trim();
                        }
                    }
                }

                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                // FIXED EXCEL TOTAL COLSPAN: 10 columns architecture setup
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='10' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='10' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Wise NCCF Moisture & Fumigation Status Details</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Filtered View Scope ID</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                Response.Write(customExcelHeader);
                gvDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush(); Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}