using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FCINew_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_For_FCI : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            txtpaymentdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblPrintDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            fillComodity();
        }
    }

    protected string getDate_MDY(string inDate)
    {
        if (string.IsNullOrEmpty(inDate))
        {
            return "01/01/1919";
        }
        else
        {
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "M/d/yyyy", "dd MMM yyyy", "yyyy/MM/dd", "MM/dd/yyyy" };
            return DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }

        if (!string.IsNullOrEmpty(txtpaymentdate.Text))
        {
            lblPrintDate.Text = Convert.ToDateTime(txtpaymentdate.Text).ToString("dd-MM-yyyy");
        }

        List<string> selectedNames = new List<string>();
        foreach (ListItem item in chkCommodityList.Items)
        {
            if (item.Selected && item.Value != "0") selectedNames.Add(item.Text);
        }
        lblPrintCommodity.Text = selectedNames.Count > 0 ? string.Join(", ", selectedNames) : "All Commodities";

        fillgrid();
    }

    private void fillComodity()
    {
        try
        {
            string query = "SELECT Commodity_Id,Commodity_Name FROM dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkCommodityList.Items.Clear();
                chkCommodityList.DataSource = ds.Tables[0];
                chkCommodityList.DataTextField = "Commodity_Name";
                chkCommodityList.DataValueField = "Commodity_Id";
                chkCommodityList.DataBind();

                chkCommodityList.Items.Insert(0, new ListItem("-- All Commodities --", "0"));
                chkCommodityList.Items[0].Selected = true;
            }
        }
        catch (Exception) { }
    }

    private string GetSelectedCommodities()
    {
        List<string> selectedIDs = new List<string>();
        bool allSelected = false;

        foreach (ListItem item in chkCommodityList.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "0") { allSelected = true; break; }
                selectedIDs.Add(item.Value);
            }
        }

        if (allSelected || selectedIDs.Count == 0) return "0";
        return string.Join(",", selectedIDs);
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Date_Wise_All_Crop_wise_Balance_Details_District_Wise_For_FCI", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));
                cmd.Parameters.AddWithValue("@CommodityID", GetSelectedCommodities());

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dtSource = new DataTable();
                    sda.Fill(dtSource);

                    if (dtSource.Rows.Count > 0)
                    {
                        DataTable dtProcessed = dtSource.Clone();
                        dtProcessed.Columns["District"].DataType = typeof(string);

                        string lastDistrict = dtSource.Rows[0]["District"].ToString();
                        decimal[] subTotals = new decimal[12];
                        decimal[] grandTotals = new decimal[12];

                        string[] cropYears = { "2016-17", "2017-18", "2018-19", "2019-20", "2020-21", "2021-22", "2022-23", "2023-24", "2024-25", "2025-26", "2026-27", "Total" };

                        foreach (DataRow row in dtSource.Rows)
                        {
                            string currentDistrict = row["District"].ToString();

                            if (currentDistrict != lastDistrict)
                            {
                                AddSubTotalRow(dtProcessed, lastDistrict, subTotals, cropYears);
                                Array.Clear(subTotals, 0, subTotals.Length);
                                lastDistrict = currentDistrict;
                            }

                            dtProcessed.ImportRow(row);

                            for (int i = 0; i < cropYears.Length; i++)
                            {
                                decimal val = row[cropYears[i]] != DBNull.Value ? Convert.ToDecimal(row[cropYears[i]]) : 0;
                                subTotals[i] += val;
                                grandTotals[i] += val;
                            }
                        }

                        AddSubTotalRow(dtProcessed, lastDistrict, subTotals, cropYears);

                        GridView1.DataSource = dtProcessed;
                        GridView1.DataBind();

                        GridView1.UseAccessibleHeader = true;
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                        GridView1.FooterRow.Cells[1].Text = "State Grand Total Position";
                        GridView1.FooterRow.Cells[1].Font.Bold = true;
                        GridView1.FooterRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
                        GridView1.FooterRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

                        for (int i = 0; i < cropYears.Length; i++)
                        {
                            GridView1.FooterRow.Cells[i + 3].Text = grandTotals[i].ToString("N2");
                            GridView1.FooterRow.Cells[i + 3].Font.Bold = true;
                            GridView1.FooterRow.Cells[i + 3].HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }

    private void AddSubTotalRow(DataTable dt, string districtName, decimal[] totals, string[] cropYears)
    {
        DataRow subRow = dt.NewRow();
        subRow["District"] = districtName + " - Total Balance Summary";
        subRow["Commodity"] = DBNull.Value;

        for (int i = 0; i < cropYears.Length; i++)
        {
            subRow[cropYears[i]] = totals[i];
        }
        dt.Rows.Add(subRow);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string districtVal = DataItemValue(e.Row.DataItem, "District");

            // FIXED: Centering text alignment for District Total rows via merging left columns safely
            if (districtVal.Contains("Total Balance Summary"))
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
                e.Row.Font.Bold = true;
                e.Row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

                // Merging cell index 0 (S.No), index 1 (District), and index 2 (Commodity ID) into one centered cell
                e.Row.Cells[0].Text = districtVal;
                e.Row.Cells[0].ColumnSpan = 3;
                e.Row.Cells[0].Attributes.Add("style", "text-align:center !important; vertical-align:middle !important; font-weight:bold !important;");

                // Remove the extra cells to balance table DOM spans structure
                e.Row.Cells.RemoveAt(2); // Remove Commodity Cell
                e.Row.Cells.RemoveAt(1); // Remove District Cell
            }

            // Align values data matching specific table layout bounds
            int cellStartIdx = districtVal.Contains("Total Balance Summary") ? 1 : 3;
            for (int i = cellStartIdx; i < e.Row.Cells.Count; i++)
            {
                // Ensure text formatting styles applies safely on remaining dynamic crop elements
                if (!districtVal.Contains("Total Balance Summary") || i > 0)
                {
                    e.Row.Cells[i].CssClass = "text-right-align";
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
                }
            }
        }
    }

    private string DataItemValue(object dataItem, string columnName)
    {
        if (dataItem == null) return string.Empty;
        var drv = dataItem as DataRowView;
        if (drv != null) return drv[columnName].ToString();
        return string.Empty;
    }

    protected void GridView1_DataBound(object sender, EventArgs e) { }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=District_Wise_Qty_Available_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                fillgrid();

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string activeSelectionDate = !string.IsNullOrEmpty(txtpaymentdate.Text) ? Convert.ToDateTime(txtpaymentdate.Text).ToString("dd-MM-yyyy") : dateTimeStr;

                List<string> selectedNames = new List<string>();
                foreach (ListItem item in chkCommodityList.Items)
                {
                    if (item.Selected && item.Value != "0") selectedNames.Add(item.Text);
                }
                string selectedComm = selectedNames.Count > 0 ? string.Join(", ", selectedNames) : "All Commodities";

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='15' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='15' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District, Commodity & Date Wise Stock Position</th></tr>
                        <tr><td colspan='8' style='text-align:left; font-weight:bold; color:#475569;'>Stock Position As On: {0} | Commodity: {1}</td><td colspan='7' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {2}</td></tr>
                        <tr><td colspan='15' style='border:none;'>&nbsp;</td></tr>
                    </table>", activeSelectionDate, selectedComm, dateTimeStr);

                Response.Write("<html xmlns:x='urn:schemas-microsoft-com:office:excel'><head><style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style></head><body>");
                Response.Write(customExcelHeader);

                GridView1.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Write("</body></html>");
                Response.Flush();

                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}