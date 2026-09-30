using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Godown_Wise_Online_Offline_Moisture_Details : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Dynamic Master Accumulator Metrics Pools
    int grandTotalStack = 0, grandOnlineMoisture = 0, grandOfflineMoisture = 0;
    int grandTotalMoisture = 0, grandPendingMoisture = 0, grandSentToDM = 0;
    int grandFCIInspected = 0, grandPendingAtFCI = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["BranchId"] != null)
            {
                string incomingBranch = Request.QueryString["BranchId"].ToString().Trim();
                //lblBranchInfo.Text = incomingBranch;
                BindGodownWiseLedger(incomingBranch);
            }
            else
            {
                //lblBranchInfo.Text = "No Branch Parameter Provided";
                BindGodownWiseLedger("0");
            }
        }
    }

    private void BindGodownWiseLedger(string branchId)
    {
        try
        {
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_Godown_Wise_OnlineOffline_Moisture_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchId", branchId.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvGodownDetails.DataSource = dt;
            gvGodownDetails.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvGodownDetails.UseAccessibleHeader = true;
                gvGodownDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvGodownDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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

            // FIXED INDEX RANGE: Due to Godown_ID column removal, data columns are now indices 2 through 9
            for (int i = 2; i <= 9; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    protected void gvGodownDetails_DataBound(object sender, EventArgs e)
    {
        if (gvGodownDetails.Rows.Count > 0)
        {
            Table tbl = (Table)gvGodownDetails.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            // FIXED COLUMN SPAN: Set to 2 (Covers precisely S.No and Godown Name fields)
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
        if (gvGodownDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Wise_Moisture_Entry_Summary.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                string incomingBranchId = Request.QueryString["BranchId"] != null ? Request.QueryString["BranchId"].ToString().Trim() : "0";
                BindGodownWiseLedger(incomingBranchId);

                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");
                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                // FIXED EXCEL COLSPAN: Modified to exactly 10 columns width to match the grid perfectly
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='10' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='10' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Wise Online & Offline Moisture Entry Progress Summary Report</th></tr>
                        <tr><td colspan='5' style='text-align:left; font-weight:bold; color:#475569;'>Target Depot/Branch ID Context: {0}</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>
                    </table>", incomingBranchId, dateTimeStr);

                Response.Write(customExcelHeader);
                gvGodownDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}