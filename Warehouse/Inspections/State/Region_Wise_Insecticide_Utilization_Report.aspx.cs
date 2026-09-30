using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Inspections_State_Region_Wise_Insecticide_Utilization_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);

    // Grand Total रखने के लिए वैरियेबल्स की घोषणा
    decimal grandHOTransfer = 0, grandTotalReceived = 0, grandPendingRMRec = 0;
    decimal grandBalRM = 0, grandTotalBalRM = 0, grandROTransferBranch = 0;
    decimal grandPendingRMTrans = 0, grandConsOwnGodown = 0, grandTransJVS = 0, grandPendingBranch = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReport();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        try
        {
            lblPrintInsecticide.Text = ddlInsecticide.SelectedItem.Text;
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("dbo.usp_GetInsecticideReportByRegion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Insecticide_ID", Convert.ToInt32(ddlInsecticide.SelectedValue));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvReport.UseAccessibleHeader = true;
                gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;

                // हेडर को ज़बरदस्ती बोल्ड और वाइट टेक्स्ट स्टाइल देने के लिए (Moisture रिपोर्ट की तरह)
                foreach (TableCell cell in gvReport.HeaderRow.Cells)
                {
                    cell.Attributes.Add("style", "color:#ffffff !important; text-align:center !important; vertical-align:middle !important; background-color:#1e3a8a !important; font-weight:bold !important;");
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error: " + ex.Message.Replace("'", "\\'") + "');", true);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // हर पंक्ति बाइंड होते समय संख्याओं को जोड़ना
            grandHOTransfer += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "HO_Transfer_To_RM"));
            grandTotalReceived += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Received_Quantity_To_RM"));
            grandPendingRMRec += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pending For RM Receiving"));
            grandBalRM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance at RM"));
            grandTotalBalRM += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Balance at RM"));
            grandROTransferBranch += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RO_Transfer_to_Branch"));
            grandPendingRMTrans += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pending For RM Transfer to Branch"));
            grandConsOwnGodown += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Consumption_TO_OWN_Godown"));
            grandTransJVS += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Transfer_To_JVS_Godown"));
            grandPendingBranch += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pending_At_Branch"));
        }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
{
    if (gvReport.Rows.Count > 0)
    {
        Table tbl = (Table)gvReport.Controls[0];

        // dynamic final Grand Total पंक्ति बनाना
        GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff"); // लाइट ब्लू बैकग्राउंड
        grandTotalRow.Font.Bold = true;
        grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell mainCell = new TableCell();
        mainCell.Text = "Grand Total";
        mainCell.ColumnSpan = 3;
        mainCell.HorizontalAlign = HorizontalAlign.Center;
        mainCell.VerticalAlign = VerticalAlign.Middle;
        grandTotalRow.Cells.Add(mainCell);

        // कॉलम्स में टोटल वैल्यू जोड़ना
        grandTotalRow.Cells.Add(new TableCell { Text = grandHOTransfer.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandTotalReceived.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandPendingRMRec.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandBalRM.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandTotalBalRM.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandROTransferBranch.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandPendingRMTrans.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandConsOwnGodown.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandTransJVS.ToString("N2") });
        grandTotalRow.Cells.Add(new TableCell { Text = grandPendingBranch.ToString("N2") });

        // स्टाइल अलाइनमेंट सेट करना
        for (int i = 1; i < grandTotalRow.Cells.Count; i++)
        {
            grandTotalRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            grandTotalRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
            grandTotalRow.Cells[i].Attributes.Add("style", "padding-right: 8px !important; font-weight: bold !important; border: 1px solid #e2e8f0 !important;");
        }

        // सीधे आखिरी डेटा रो के बाद जोड़ रहे हैं बिना किसी गैप के
        tbl.Rows.Add(grandTotalRow);
    }
}

    private void ResetGrandTotalCounters()
    {
        grandHOTransfer = 0; grandTotalReceived = 0; grandPendingRMRec = 0;
        grandBalRM = 0; grandTotalBalRM = 0; grandROTransferBranch = 0;
        grandPendingRMTrans = 0; grandConsOwnGodown = 0; grandTransJVS = 0; grandPendingBranch = 0;
    }

    // Custom Excel Export Logic (Moisture रिपोर्ट की तरह भव्य हेडर ब्लॉक के साथ)
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Region_Wise_Insecticide_Utilization_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                gvReport.GridLines = GridLines.Both;
                gvReport.HeaderStyle.BackColor = System.Drawing.Color.FromName("#1e3a8a");
                gvReport.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvReport.HeaderStyle.Font.Bold = true;

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string selectedInsecticide = ddlInsecticide.SelectedItem.Text;

                // Moisture रिपोर्ट जैसा ही टेबल स्ट्रक्चर एक्सेल हेडर के लिए
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr>
                            <th colspan='13' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='13' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>Region Wise Insecticide Utilization Report</th>
                        </tr>
                        <tr>
                            <td colspan='6' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Insecticide: {0}</td>
                            <td colspan='7' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>Generated On: {1}</td>
                        </tr>
                        <tr><td colspan='13' style='border:none;'>&nbsp;</td></tr>
                    </table>", selectedInsecticide, dateTimeStr);

                Response.Write(customExcelHeader);

                // एक्सेल फ़ाइल के लिए ग्रिडव्यू की इनलाइन स्टाइलिंग इंजेक्ट करना
                string style = @"<style> 
                                    th { background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important; text-align:center; } 
                                    td { border: 1px solid #ccc; } 
                                 </style>";
                Response.Write(style);

                gvReport.RenderControl(hw);
                Response.Write(sw.ToString());

                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // एक्सेल रेंडरिंग एरर को रोकने के लिए आवश्यक है
    }
}