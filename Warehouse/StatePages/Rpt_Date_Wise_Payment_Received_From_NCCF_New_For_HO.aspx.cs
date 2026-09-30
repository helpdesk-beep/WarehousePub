using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Date_Wise_Payment_Received_From_NCCF_New_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    // Grand Total Variables
    decimal grandNet = 0, grandTds = 0, grandOther = 0, grandApproved = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            PopulateYearDropdown();
            BindSummaryReport();
        }
    }

    private void PopulateYearDropdown()
    {
        // FIXED: Loop 2020 se lekar current year (2026) tak kewal plain numbers select karega
        int startYear = 2020;
        int currentYear = DateTime.Now.Year;

        ddlYear.Items.Clear();
        ddlYear.Items.Add(new ListItem("-- All Years --", "0"));

        for (int year = currentYear; year >= startYear; year--)
        {
            // Dropdown ke Text aur Value dono me kewal saal ka number (jaise 2026, 2025) hi add hoga
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSummaryReport();
    }

    private void BindSummaryReport()
    {
        try
        {
            grandNet = 0; grandTds = 0; grandOther = 0; grandApproved = 0;

            SqlCommand cmd = new SqlCommand("Get_Date_Wise_Payment_Received_Payment_From_NCCF", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Sends dropdown value selection ('0' or '2026-27') to matching safe range parameters inside stored proc
            cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);

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
            Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grandNet += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalNetAmount"));
            grandTds += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalTDSDeduction"));
            grandOther += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalOtherDeduction"));
            grandApproved += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalApprovedAmount"));
        }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            GridViewRow totalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            totalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            totalRow.Font.Bold = true;
            totalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell { Text = "Grand Total", ColumnSpan = 2 };
            mainCell.HorizontalAlign = HorizontalAlign.Center;
            mainCell.Attributes.Add("style", "text-align:center !important; font-weight:bold !important;");
            totalRow.Cells.Add(mainCell);

            totalRow.Cells.Add(createTotalCell(grandNet.ToString("N2")));
            totalRow.Cells.Add(createTotalCell(grandTds.ToString("N2")));
            totalRow.Cells.Add(createTotalCell(grandOther.ToString("N2")));
            totalRow.Cells.Add(createTotalCell(grandApproved.ToString("N2")));

            tbl.Rows.Add(totalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text };
        cell.CssClass = "text-right-align";
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important;");
        return cell;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_Date_Wise_Payment_Summary.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindSummaryReport();

                // Extracts string contents safely using dynamic control tree literal scanning
                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell dateCell = row.Cells[1];
                        if (dateCell.Controls.Count > 0)
                        {
                            string plainText = string.Empty;

                            foreach (Control ctrl in dateCell.Controls)
                            {
                                if (ctrl is LiteralControl)
                                {
                                    plainText += ((LiteralControl)ctrl).Text;
                                }
                                else if (ctrl is DataBoundLiteralControl)
                                {
                                    plainText += ((DataBoundLiteralControl)ctrl).Text;
                                }
                                else if (ctrl is WebControl)
                                {
                                    ControlCollection subControls = ctrl.Controls;
                                    if (subControls.Count > 0 && subControls[0] is LiteralControl)
                                    {
                                        plainText += ((LiteralControl)subControls[0]).Text;
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(plainText))
                            {
                                plainText = dateCell.Text;
                            }

                            if (!string.IsNullOrEmpty(plainText) && plainText.Contains("<a"))
                            {
                                int startIdx = plainText.IndexOf(">") + 1;
                                int endIdx = plainText.LastIndexOf("</a");
                                if (startIdx > 0 && endIdx > startIdx)
                                {
                                    plainText = plainText.Substring(startIdx, endIdx - startIdx);
                                }
                            }

                            dateCell.Controls.Clear();
                            dateCell.Text = plainText.Trim();
                        }
                    }
                }

                gvReport.GridLines = GridLines.Both;
                gvReport.HeaderStyle.BackColor = System.Drawing.Color.FromName("#2563eb");
                gvReport.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvReport.HeaderStyle.Font.Bold = true;

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string scopeText = ddlYear.SelectedIndex > 0 ? ddlYear.SelectedItem.Text : "All Years Registered";

                string customHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='6' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='6' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Date Wise Payment Received From NCCF - Summary</th></tr>
                        <tr><td colspan='3' style='text-align:left; font-weight:bold; color:#475569;'>Selected Financial Year: {0}</td><td colspan='3' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='6' style='border:none;'>&nbsp;</td></tr>
                    </table>", scopeText, dateTimeStr);

                Response.Write(customHeader);
                gvReport.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}