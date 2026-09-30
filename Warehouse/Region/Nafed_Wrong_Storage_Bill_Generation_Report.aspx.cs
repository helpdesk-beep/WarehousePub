using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Nafed_Wrong_Storage_Bill_Generation_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDepot = string.Empty;

    // Subtotal Accumulators
    decimal subStorageAmt = 0;
    decimal subRentAmt = 0;
    decimal subDiffAmt = 0;

    // Grand Total Accumulators
    decimal grandStorageAmt = 0;
    decimal grandRentAmt = 0;
    decimal grandDiffAmt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
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
            currentDepot = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("Nafed_Diffrence_Bill_Details_For_RM", con);
            cmd.CommandType = CommandType.StoredProcedure;
	    cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
            // Stored procedure Execution
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

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

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string depotName = DataBinder.Eval(e.Row.DataItem, "DepotName").ToString();

            if (string.IsNullOrEmpty(currentDepot))
            {
                currentDepot = depotName;
            }

            if (currentDepot != depotName)
            {
                Table tbl = (Table)gvReport.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentDepot = depotName;
                ResetSubTotalCounters();
            }

            decimal storageAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Storage_Bill_Amount"));
            decimal rentAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rent_Bill_Amount"));
            decimal diffAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Difference_Amount"));

            subStorageAmt += storageAmt;
            subRentAmt += rentAmt;
            subDiffAmt += diffAmt;

            grandStorageAmt += storageAmt;
            grandRentAmt += rentAmt;
            grandDiffAmt += diffAmt;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell();
        cell.Text = currentDepot + " - Branch Sub Total";
        cell.ColumnSpan = 9; // Spans up to Storage_Bill_Amount
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = subStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Center }); // Empty cell for Rent_Bill
        row.Cells.Add(new TableCell { Text = subRentAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subDiffAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        return row;
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            if (!string.IsNullOrEmpty(currentDepot))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 9;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = "", HorizontalAlign = HorizontalAlign.Center });
            grandTotalRow.Cells.Add(new TableCell { Text = grandRentAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandDiffAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters() { subStorageAmt = 0; subRentAmt = 0; subDiffAmt = 0; }
    private void ResetGrandTotalCounters() { grandStorageAmt = 0; grandRentAmt = 0; grandDiffAmt = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Storage_Bill_And_Rent_Difference_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                foreach (GridViewRow row in gvReport.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TableCell godownCell = row.Cells[3];
                        if (godownCell.Controls.Count > 0)
                        {
                            string rawContent = godownCell.Text;
                            if (string.IsNullOrEmpty(rawContent))
                            {
                                foreach (Control ctrl in godownCell.Controls)
                                {
                                    if (ctrl is LiteralControl) rawContent += ((LiteralControl)ctrl).Text;
                                }
                            }

                            if (!string.IsNullOrEmpty(rawContent) && rawContent.Contains("<a"))
                            {
                                int idxStart = rawContent.IndexOf(">") + 1;
                                int idxEnd = rawContent.LastIndexOf("</a");
                                if (idxStart > 0 && idxEnd > idxStart)
                                {
                                    rawContent = rawContent.Substring(idxStart, idxEnd - idxStart);
                                }
                            }

                            godownCell.Controls.Clear();
                            godownCell.Text = rawContent.Trim();
                        }
                    }
                }

                gvReport.GridLines = GridLines.Both;
                gvReport.HeaderStyle.BackColor = System.Drawing.Color.FromName("#2563eb");
                gvReport.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvReport.HeaderStyle.Font.Bold = true;

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='13' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH STATE WAREHOUSING CORPORATION</th></tr>
                        <tr><th colspan='13' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>NAFED Storage Charges & Rent Bill Difference Report</th></tr>
                        <tr><td colspan='7' style='text-align:left; font-weight:bold; color:#475569;'>Report Category: Summary</td><td colspan='6' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='13' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

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