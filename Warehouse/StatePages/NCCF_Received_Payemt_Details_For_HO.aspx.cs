using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_NCCF_Received_Payemt_Details_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDepot = string.Empty;

    // Subtotal Accumulators
    decimal subStorageAmt = 0; int subStorageCount = 0; decimal subPssAmt = 0;
    decimal subPsfAmt = 0; decimal subTdsAmt = 0; decimal subOtherAmt = 0; decimal subReceivedAmt = 0;

    // Grand Total Accumulators
    decimal grandStorageAmt = 0; int grandStorageCount = 0; decimal grandPssAmt = 0;
    decimal grandPsfAmt = 0; decimal grandTdsAmt = 0; decimal grandOtherAmt = 0; decimal grandReceivedAmt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            FillRegionDropdown();
            BindReport();
        }
    }

    private void FillRegionDropdown()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT WHERE Region_ID IS NOT NULL AND Regionnm IS NOT NULL ORDER BY Regionnm", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlRegion.DataSource = dt;
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();

            // Insert "All Regions" option at the absolute top index boundary
            ddlRegion.Items.Insert(0, new ListItem("-- All Regions --", ""));
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Dropdown Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        try
        {
            currentDepot = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("sp_NCCF_Storage_Bill_And_Payment_Report", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Parameter filtration rule based on dropdown choice
            if (!string.IsNullOrEmpty(ddlRegion.SelectedValue))
            {
                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue.Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionID", DBNull.Value); // Passes NULL to fetch all entries
            }

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
            Response.Write("<script>alert('Data Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string depotName = DataBinder.Eval(e.Row.DataItem, "Depot Name").ToString();

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

            decimal storageAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill Amount"));
            int storageCount = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Storage Charges Bill"));
            decimal pssAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSS Bill Amount"));
            decimal psfAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PSF Bill Amount"));
            decimal tdsAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS Deduction From NCCF"));
            decimal otherAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other Deduction From NCCF"));
            decimal receivedAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Received Payment From NCCF"));

            subStorageAmt += storageAmt; subStorageCount += storageCount; subPssAmt += pssAmt;
            subPsfAmt += psfAmt; subTdsAmt += tdsAmt; subOtherAmt += otherAmt; subReceivedAmt += receivedAmt;

            grandStorageAmt += storageAmt; grandStorageCount += storageCount; grandPssAmt += pssAmt;
            grandPsfAmt += psfAmt; grandTdsAmt += tdsAmt; grandOtherAmt += otherAmt; grandReceivedAmt += receivedAmt;
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
        cell.ColumnSpan = 4;
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = subStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subStorageCount.ToString("N0"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subPssAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subPsfAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subTdsAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subOtherAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subReceivedAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

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
            mainCell.ColumnSpan = 4;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandStorageCount.ToString("N0"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPssAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPsfAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTdsAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandOtherAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandReceivedAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters() { subStorageAmt = 0; subStorageCount = 0; subPssAmt = 0; subPsfAmt = 0; subTdsAmt = 0; subOtherAmt = 0; subReceivedAmt = 0; }
    private void ResetGrandTotalCounters() { grandStorageAmt = 0; grandStorageCount = 0; grandPssAmt = 0; grandPsfAmt = 0; grandTdsAmt = 0; grandOtherAmt = 0; grandReceivedAmt = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NCCF_HO_Storage_Bill_And_Payment_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                // Flattens anchors to normal text for excel spreadsheet compatibility safely
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
                string selectedRegionText = ddlRegion.SelectedIndex > 0 ? ddlRegion.SelectedItem.Text : "All Regions";

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='11' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='11' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>NCCF Storage Charges & Received Payment Report (HO)</th></tr>
                        <tr><td colspan='6' style='text-align:left; font-weight:bold; color:#475569;'>Filter Region: {0}</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='11' style='border:none;'>&nbsp;</td></tr>
                    </table>", selectedRegionText, dateTimeStr);

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