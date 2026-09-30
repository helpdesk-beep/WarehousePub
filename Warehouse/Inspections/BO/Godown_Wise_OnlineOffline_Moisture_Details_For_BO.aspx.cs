using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Godown_Wise_OnlineOffline_Moisture_Details_For_BO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Dynamic Master Grand Total Accumulator Pools
    int grandTotalStack = 0, grandOnlineMoisture = 0, grandOfflineMoisture = 0;
    int grandTotalMoisture = 0, grandPendingMoisture = 0, grandSentToDM = 0;
    int grandFCIInspected = 0, grandPendingAtFCI = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // FIXED HINT: Safe profile state checking layer
        if (Session["UserId"] == null)
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

            // Calls stored procedure sp_Godown_Wise_OnlineOffline_Moisture_Details natively
            SqlCommand cmd = new SqlCommand("sp_Godown_Wise_OnlineOffline_Moisture_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // FIXED PARAMETER REFERENCE: Extracted branch identity token directly from active session profiles
            string dynamicBranchID = Session["UserId"].ToString().Trim();
            cmd.Parameters.AddWithValue("@BranchId", dynamicBranchID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            gvReport.DataSource = dt;
            gvReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvReport.UseAccessibleHeader = true;
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Error Trace: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grandTotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Stack"));
            grandOnlineMoisture += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Online Moisture Up to Date"));
            grandOfflineMoisture += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Offline Moisture entry"));
            grandTotalMoisture += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture"));
            grandPendingMoisture += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending Stack For Moisture"));
            grandSentToDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Stack Moisture Sent to DM MPSCSC/FCI"));
            grandFCIInspected += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));
            grandPendingAtFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending at FCI"));

            // FIXED SHRUNK RANGE INDEX: Godown_ID hatne ke baad numeric columns are strictly indices 2 to 9
            for (int i = 2; i <= 9; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
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

            // FIXED COLUMN SPAN: Unified to 2 (Covers precisely S.No and Godown Name text columns layout width)
            TableCell mainCell = new TableCell { Text = "Grand Total Summary", ColumnSpan = 2 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(createTotalCell(grandTotalStack.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOnlineMoisture.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandOfflineMoisture.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandTotalMoisture.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPendingMoisture.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandSentToDM.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandFCIInspected.ToString("N0")));
            grandTotalRow.Cells.Add(createTotalCell(grandPendingAtFCI.ToString("N0")));

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

    private void ResetGrandTotalCounters()
    {
        grandTotalStack = 0; grandOnlineMoisture = 0; grandOfflineMoisture = 0;
        grandTotalMoisture = 0; grandPendingMoisture = 0; grandSentToDM = 0;
        grandFCIInspected = 0; grandPendingAtFCI = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Wise_Moisture_Summary_HO.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string sessionBranchScope = Session["UserId"] != null ? Session["UserId"].ToString().Trim() : "0";

                // FIXED SHEET HEADER COLSPAN: Adjusted to exactly 10 columns total sheet block structure width width
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='10' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='10' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise Online & Offline Moisture Entry Progress Summary</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Active Depot ID Scope: {0}</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>
                    </table>", sessionBranchScope, dateTimeStr);

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