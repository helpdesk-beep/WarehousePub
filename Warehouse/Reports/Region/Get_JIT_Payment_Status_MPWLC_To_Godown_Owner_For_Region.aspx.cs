using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Get_JIT_Payment_Status_MPWLC_To_Godown_Owner_For_Region : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDistrict = string.Empty;

    // Subtotal Accumulators per District row group
    decimal subRate = 0, subRentAmt = 0, subTds = 0, subGain = 0, subOther = 0, subBMDeduct = 0;
    decimal subTotalDeduct = 0, subPayToGO = 0, subPayToWLC = 0, subStorageAmt = 0, subPaidToOwner = 0;

    // Global Grand Total Accumulators
    decimal grandRate = 0, grandRentAmt = 0, grandTds = 0, grandGain = 0, grandOther = 0, grandBMDeduct = 0;
    decimal grandTotalDeduct = 0, grandPayToGO = 0, grandPayToWLC = 0, grandStorageAmt = 0, grandPaidToOwner = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Safety Layer Context Checks
        if (Session["UserName"] == null || Session["Region_ID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // Auto fill current month limits boundary variables
            txtFromDate.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            txtToDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            if (string.IsNullOrEmpty(txtFromDate.Text) || string.IsNullOrEmpty(txtToDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please choose active From and To calendar date frames.');", true);
                return;
            }

            currentDistrict = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            // Regional specific procedure triggered
            SqlCommand cmd = new SqlCommand("Get_JIT_Payment_Status_MPWLC_To_Godown_Owner_For_Region", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Map region id directly out of active regional session parameters securely
            cmd.Parameters.AddWithValue("@Region_Id", Convert.ToInt32(Session["Region_ID"].ToString().Trim()));
            cmd.Parameters.AddWithValue("@FDate", txtFromDate.Text.Trim());
            cmd.Parameters.AddWithValue("@TDate", txtToDate.Text.Trim());

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
            Response.Write("<script>alert('Data Retrieval Failure: " + ex.Message.Replace("'", "\\'") + "');</script>");
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

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentDistrict = districtName;
                ResetSubTotalCounters();
            }

            decimal rate = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Per_Month_Rate"));
            decimal rentAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rent_Bill_AMT"));
            decimal tds = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS_Amt"));
            decimal gain = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Gain_Detuction_Amount"));
            decimal other = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other_Detuction_Amt"));
            decimal bmDeduct = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BM_Deduction"));
            decimal totDeduct = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Deduction_AMT"));
            decimal payGo = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PayToGO"));
            decimal payWlc = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PAYtoMPWLC"));
            decimal storageAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StorageCharBillAMt"));
            decimal paidOwner = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MPWLCPAYToGodown"));

            subRate += rate; subRentAmt += rentAmt; subTds += tds; subGain += gain; subOther += other; subBMDeduct += bmDeduct;
            subTotalDeduct += totDeduct; subPayToGO += payGo; subPayToWLC += payWlc; subStorageAmt += storageAmt; subPaidToOwner += paidOwner;

            grandRate += rate; grandRentAmt += rentAmt; grandTds += tds; grandGain += gain; grandOther += other; grandBMDeduct += bmDeduct;
            grandTotalDeduct += totDeduct; grandPayToGO += payGo; grandPayToWLC += payWlc; grandStorageAmt += storageAmt; grandPaidToOwner += paidOwner;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell();
        cell.Text = currentDistrict + " - Sub Total";
        cell.ColumnSpan = 13;
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = subRate.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subRentAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subTds.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subGain.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subOther.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subBMDeduct.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subTotalDeduct.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subPayToGO.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subPayToWLC.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = subStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
        row.Cells.Add(new TableCell { Text = subPaidToOwner.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
        row.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });

        for (int i = 1; i < row.Cells.Count; i++) row.Cells[i].VerticalAlign = VerticalAlign.Middle;
        return row;
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            if (!string.IsNullOrEmpty(currentDistrict))
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
            mainCell.ColumnSpan = 13;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandRate.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandRentAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTds.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandGain.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandOther.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandBMDeduct.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotalDeduct.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPayToGO.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPayToWLC.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandStorageAmt.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            grandTotalRow.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = grandPaidToOwner.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });
            grandTotalRow.Cells.Add(new TableCell { Text = "-", HorizontalAlign = HorizontalAlign.Center, CssClass = "text-center-align" });

            for (int i = 1; i < grandTotalRow.Cells.Count; i++) grandTotalRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters()
    {
        subRate = 0; subRentAmt = 0; subTds = 0; subGain = 0; subOther = 0; subBMDeduct = 0;
        subTotalDeduct = 0; subPayToGO = 0; subPayToWLC = 0; subStorageAmt = 0; subPaidToOwner = 0;
    }

    private void ResetGrandTotalCounters()
    {
        grandRate = 0; grandRentAmt = 0; grandTds = 0; grandGain = 0; grandOther = 0; grandBMDeduct = 0;
        grandTotalDeduct = 0; grandPayToGO = 0; grandPayToWLC = 0; grandStorageAmt = 0; grandPaidToOwner = 0;
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
        Response.AddHeader("content-disposition", "attachment;filename=Regional_JIT_Payment_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindReport();

                // FORCE AUTOMATIC CELL TEXT WRAPPING INSIDE EXCEL SHEET GENERATOR
                Response.Write("<style> td { mso-number-format:\\@; white-space:normal; } </style>");

                string rangeStr = "From: " + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy") + " To: " + Convert.ToDateTime(txtToDate.Text).ToString("dd-MM-yyyy");

                // Full 27 column horizontal span lock for clean unified look
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='27' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='27' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>JIT Payment Settlement Status Report (Regional View)</th></tr>
                        <tr><td colspan='14' style='text-align:left; font-weight:bold; color:#475569;'>Date Range Context: {0}</td><td colspan='13' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {1}</td></tr>
                        <tr><td colspan='27' style='border:none;'>&nbsp;</td></tr>
                    </table>", rangeStr, DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));

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