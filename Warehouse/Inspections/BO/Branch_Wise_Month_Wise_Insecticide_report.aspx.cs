using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_Branch_Wise_Month_Wise_Insecticide_report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);

    string currentDistrict = string.Empty;

    // Subtotal Accumulators per District row group
    decimal subRecQty = 0, subRecMktVal = 0, subRecVal = 0;
    decimal subConsQty = 0, subConsMktVal = 0, subConsVal = 0;
    decimal subTransQty = 0, subTransMktVal = 0, subTransVal = 0;
    decimal subClosingQty = 0;

    // Grand Total Accumulators
    decimal grandRecQty = 0, grandRecMktVal = 0, grandRecVal = 0;
    decimal grandConsQty = 0, grandConsMktVal = 0, grandConsVal = 0;
    decimal grandTransQty = 0, grandTransMktVal = 0, grandTransVal = 0;
    decimal grandClosingQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        try
        {
            // Session se BranchID/UserId read karne ka safe logic
            string branchID = Session["UserId"].ToString().Trim();

            currentDistrict = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            // Naye update huye procedure name ke sath connection establish kiya
            SqlCommand cmd = new SqlCommand("Branch_Wise_Month_Wise_Insecticide_Report", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Naye procedure parameters mapping rules
            cmd.Parameters.AddWithValue("@Branch_ID", Convert.ToInt32(branchID));
            cmd.Parameters.AddWithValue("@Insecticide_ID", Convert.ToInt32(ddlInsecticide.SelectedValue));
            cmd.Parameters.AddWithValue("@Month", Convert.ToInt32(ddlMonth.SelectedValue));

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
            Response.Write("<script>alert('Execution Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string districtName = DataBinder.Eval(e.Row.DataItem, "District_Name").ToString();

            if (string.IsNullOrEmpty(currentDistrict))
            {
                currentDistrict = districtName;
            }

            if (currentDistrict != districtName)
            {
                Table tbl = (Table)gvReport.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                //GridViewRow subTotalRow = CreateSubTotalRow();
                //tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentDistrict = districtName;
                ResetSubTotalCounters();
            }

            // Stored Proc me pehle se ISNULL laga hai, isliye ab direct direct convert safe chalega
            decimal rQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Receipt_Balance_quantity"));
            decimal rMkt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Receipt_Balance_market_value"));
            decimal rVal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Receipt_Balance_value"));

            decimal cQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Consumption_Balance_quantity"));
            decimal cMkt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Consumption_Balance_market_value"));
            decimal cVal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Consumption_Balance_value"));

            decimal tQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Transfer_Balance_quantity"));
            decimal tMkt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Transfer_Balance_market_value"));
            decimal tVal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Transfer_Balance_value"));

            decimal clQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ClosingBalance"));

            // Sum up group level subtotals
            subRecQty += rQty; subRecMktVal += rMkt; subRecVal += rVal;
            subConsQty += cQty; subConsMktVal += cMkt; subConsVal += cVal;
            subTransQty += tQty; subTransMktVal += tMkt; subTransVal += tVal;
            subClosingQty += clQty;

            // Sum up global layout grand totals
            grandRecQty += rQty; grandRecMktVal += rMkt; grandRecVal += rVal;
            grandConsQty += cQty; grandConsMktVal += cMkt; grandConsVal += cVal;
            grandTransQty += tQty; grandTransMktVal += tMkt; grandTransVal += tVal;
            grandClosingQty += clQty;
        }
    }

    //private GridViewRow CreateSubTotalRow()
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
    //    row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
    //    row.Font.Bold = true;
    //    row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

    //    TableCell cell = new TableCell();
    //    cell.Text = currentDistrict + " - District Sub Total";
    //    cell.ColumnSpan = 4;
    //    cell.HorizontalAlign = HorizontalAlign.Right;
    //    cell.VerticalAlign = VerticalAlign.Middle;
    //    cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
    //    row.Cells.Add(cell);

    //    row.Cells.Add(new TableCell { Text = subRecQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subRecMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subRecVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

    //    row.Cells.Add(new TableCell { Text = subConsQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subConsMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subConsVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

    //    row.Cells.Add(new TableCell { Text = subTransQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subTransMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
    //    row.Cells.Add(new TableCell { Text = subTransVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

    //    row.Cells.Add(new TableCell { Text = subClosingQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

    //    return row;
    //}

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            //if (!string.IsNullOrEmpty(currentDistrict))
            //{
            //    GridViewRow finalSubTotalRow = CreateSubTotalRow();
            //    tbl.Rows.Add(finalSubTotalRow);
            //}

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Total";
            mainCell.ColumnSpan = 4;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandRecQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandRecMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandRecVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            grandTotalRow.Cells.Add(new TableCell { Text = grandConsQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandConsMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandConsVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            grandTotalRow.Cells.Add(new TableCell { Text = grandTransQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTransMktVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTransVal.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            grandTotalRow.Cells.Add(new TableCell { Text = grandClosingQty.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters()
    {
        subRecQty = 0; subRecMktVal = 0; subRecVal = 0;
        subConsQty = 0; subConsMktVal = 0; subConsVal = 0;
        subTransQty = 0; subTransMktVal = 0; subTransVal = 0;
        subClosingQty = 0;
    }

    private void ResetGrandTotalCounters()
    {
        grandRecQty = 0; grandRecMktVal = 0; grandRecVal = 0;
        grandConsQty = 0; grandConsMktVal = 0; grandConsVal = 0;
        grandTransQty = 0; grandTransMktVal = 0; grandTransVal = 0;
        grandClosingQty = 0;
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
        Response.AddHeader("content-disposition", "attachment;filename=Branch_Wise_Month_Wise_Insecticide_Report.xls");
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

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string filterItemText = ddlInsecticide.SelectedIndex > 0 ? ddlInsecticide.SelectedItem.Text : "All Insecticides";
                string filterMonthText = ddlMonth.SelectedIndex > 0 ? ddlMonth.SelectedItem.Text : "All Months";

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='14' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='14' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Branch Wise & Month Wise Insecticide Stock Position Report</th></tr>
                        <tr><td colspan='7' style='text-align:left; font-weight:bold; color:#475569;'>Filter: {0} | Month: {1}</td><td colspan='7' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {2}</td></tr>
                        <tr><td colspan='14' style='border:none;'>&nbsp;</td></tr>
                    </table>", filterItemText, filterMonthText, dateTimeStr);

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