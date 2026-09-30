using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Inspections_State_Region_Wise_OnlineOffline_Moisture_Details_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    // Accumulators for Grand Total tracking
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
            grandTotalStack = 0; grandOnline = 0; grandOffline = 0; grandTotalMoisture = 0;
            grandPendingStack = 0; grandSentDM = 0; grandFciInspected = 0; grandPendingFci = 0;

            SqlCommand cmd = new SqlCommand("sp_Region_Wise_OnlineOffline_Moisture_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Compute totals directly out of the filled data rows
            foreach (DataRow dr in dt.Rows)
            {
                grandTotalStack += Convert.ToInt32(dr["Total Stack"]);
                grandOnline += Convert.ToInt32(dr["Online Moisture Up to Date"]);
                grandOffline += Convert.ToInt32(dr["Offline Moisture entry"]);
                grandTotalMoisture += Convert.ToInt32(dr["Total Moisture"]);
                grandPendingStack += Convert.ToInt32(dr["Pending Stack For Moisture"]);
                grandSentDM += Convert.ToInt32(dr["Stack Moisture Sent to DM MPSCSC/FCI"]);
                grandFciInspected += Convert.ToInt32(dr["FCI Inspected Stack"]);
                grandPendingFci += Convert.ToInt32(dr["Pending at FCI"]);
            }

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

    // Injects Grand Total at the absolute end of the tbody collection
    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 2; // Merges S.No. and Region Name
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Region_Wise_OnlineOffline_Moisture_Details.xls");
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
                            <th colspan='10' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='10' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>Region Wise Online & Offline Moisture Details</th>
                        </tr>
                        <tr>
                            <td colspan='5' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Report Type: Consolidated System Sync</td>
                            <td colspan='5' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>Generated On: {0}</td>
                        </tr>
                        <tr><td colspan='10' style='border:none;'>&nbsp;</td></tr>
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