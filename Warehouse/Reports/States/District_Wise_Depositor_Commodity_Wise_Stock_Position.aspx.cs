using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_District_Wise_Depositor_Commodity_Wise_Stock_Position : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    int serialNumberCounter = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtpaymentdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillDepositors();
            FillCommodities();
        }
    }

    private void FillDepositors()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID IN ('129','181','184','4679','10535','15478') ORDER BY Depositor_Name", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cblDepositor.Items.Clear();
            cblDepositor.Items.Add(new ListItem("ALL DEPOSITORS", "0")); // Index 0

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cblDepositor.Items.Add(new ListItem(row["Depositor_Name"].ToString(), row["Depositor_ID"].ToString()));
                }
            }

            // By Default "ALL" ko checked set karna
            cblDepositor.Items[0].Selected = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Depositor Load Error: " + ex.Message;
        }
    }

    private void FillCommodities()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Name IS NOT NULL ORDER BY Commodity_Name", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cblCommodity.Items.Clear();
            cblCommodity.Items.Add(new ListItem("ALL COMMODITIES", "0")); // Index 0

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cblCommodity.Items.Add(new ListItem(row["Commodity_Name"].ToString(), row["Commodity_Id"].ToString()));
                }
            }

            // By Default "ALL" ko checked set karna
            cblCommodity.Items[0].Selected = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Commodity Load Error: " + ex.Message;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridReport();
    }

    private void BindGridReport()
    {
        lblmsg.Text = "";
        if (string.IsNullOrEmpty(txtpaymentdate.Text.Trim()))
        {
            lblmsg.Text = "Please select or enter As On Date!";
            return;
        }

        string selectedDepositors = GetSelectedCheckBoxValues(cblDepositor);
        string selectedCommodities = GetSelectedCheckBoxValues(cblCommodity);

        try
        {
            serialNumberCounter = 1;
            SqlCommand cmd = new SqlCommand("usp_Get_District_depositor_wise_StockPosition", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DepositorID", selectedDepositors);
            cmd.Parameters.AddWithValue("@CommodityID", selectedCommodities);
            cmd.Parameters.AddWithValue("@FromDate", txtpaymentdate.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataTable processedDt = CalculateHierarchicalTotals(dt);
                GV_StockPositionDetails.DataSource = processedDt;
                GV_StockPositionDetails.DataBind();

                GV_StockPositionDetails.UseAccessibleHeader = true;
                GV_StockPositionDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                GV_StockPositionDetails.DataSource = null;
                GV_StockPositionDetails.DataBind();
                lblmsg.Text = "No records found for the selected checked criteria.";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Execution Error: " + ex.Message;
        }
    }

    private string GetSelectedCheckBoxValues(CheckBoxList cbl)
    {
        StringBuilder sb = new StringBuilder();

        // Agar "ALL" checked hai toh backend me sirf '0' bhejenge
        if (cbl.Items.Count > 0 && cbl.Items[0].Selected)
        {
            return "0";
        }

        for (int i = 1; i < cbl.Items.Count; i++)
        {
            if (cbl.Items[i].Selected)
            {
                sb.Append(cbl.Items[i].Value + ",");
            }
        }

        string result = sb.ToString().TrimEnd(',');
        return string.IsNullOrEmpty(result) ? "0" : result;
    }

    private DataTable CalculateHierarchicalTotals(DataTable dt)
    {
        DataTable targetDt = dt.Clone();
        targetDt.Columns.Add("RowTypeMarker", typeof(string));

        string currentRegion = string.Empty;
        string currentDistrict = string.Empty;

        string[] years = { "2017-18", "2018-19", "2019-20", "2020-21", "2021-22", "2022-23", "2023-24", "2024-25", "2025-26", "2026-27", "Total" };

        decimal[] districtTotals = new decimal[years.Length];
        decimal[] regionTotals = new decimal[years.Length];
        decimal[] grandTotals = new decimal[years.Length];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = dt.Rows[i];
            string rMarker = row["Region"].ToString();
            string dMarker = row["District"].ToString();

            if (string.IsNullOrEmpty(currentRegion)) currentRegion = rMarker;
            if (string.IsNullOrEmpty(currentDistrict)) currentDistrict = dMarker;

            if (currentDistrict != dMarker)
            {
                AddSummaryRow(targetDt, currentDistrict + " Total", districtTotals, "DIST_TOTAL", years);
                Array.Clear(districtTotals, 0, districtTotals.Length);
                currentDistrict = dMarker;
            }

            if (currentRegion != rMarker)
            {
                AddSummaryRow(targetDt, currentRegion + " Region Total", regionTotals, "REGION_TOTAL", years);
                Array.Clear(regionTotals, 0, regionTotals.Length);
                currentRegion = rMarker;
            }

            DataRow dRow = targetDt.NewRow();
            dRow.ItemArray = row.ItemArray;
            dRow["RowTypeMarker"] = "DATA";
            targetDt.Rows.Add(dRow);

            for (int k = 0; k < years.Length; k++)
            {
                decimal val = row[years[k]] != DBNull.Value ? Convert.ToDecimal(row[years[k]]) : 0;
                districtTotals[k] += val;
                regionTotals[k] += val;
                grandTotals[k] += val;
            }

            if (i == dt.Rows.Count - 1)
            {
                AddSummaryRow(targetDt, currentDistrict + " Total", districtTotals, "DIST_TOTAL", years);
                AddSummaryRow(targetDt, currentRegion + " Region Total", regionTotals, "REGION_TOTAL", years);
            }
        }

        AddSummaryRow(targetDt, "Grand Total", grandTotals, "GRANDTOTAL", years);
        return targetDt;
    }

    private void AddSummaryRow(DataTable table, string title, decimal[] sums, string marker, string[] columns)
    {
        DataRow sRow = table.NewRow();
        sRow["District"] = title;
        sRow["RowTypeMarker"] = marker;
        for (int m = 0; m < columns.Length; m++)
        {
            sRow[columns[m]] = sums[m];
        }
        table.Rows.Add(sRow);
    }

    protected void GV_StockPositionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string flag = DataBinder.Eval(e.Row.DataItem, "RowTypeMarker").ToString();

            if (flag == "DATA")
            {
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                if (lblSNo != null)
                {
                    lblSNo.Text = serialNumberCounter.ToString();
                    serialNumberCounter++;
                }
            }
            else if (flag == "DIST_TOTAL" || flag == "REGION_TOTAL" || flag == "GRANDTOTAL")
            {
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "District").ToString();
                e.Row.Cells[1].ColumnSpan = 3;
                e.Row.Cells[1].Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");

                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;

                if (flag == "DIST_TOTAL")
                {
                    e.Row.Attributes.Add("style", "background-color: #ffedd5 !important; font-weight: bold !important; color: #7c2d12 !important;");
                }
                else if (flag == "REGION_TOTAL")
                {
                    e.Row.Attributes.Add("style", "background-color: #fed7aa !important; font-weight: bold !important; color: #000000 !important;");
                }
                else if (flag == "GRANDTOTAL")
                {
                    e.Row.Attributes.Add("style", "background-color: #dbeafe !important; font-weight: bold !important; color: #1e3a8a !important;");
                }
            }
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (GV_StockPositionDetails.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Hierarchical_Stock_Position_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindGridReport();

                GV_StockPositionDetails.GridLines = GridLines.Both;
                GV_StockPositionDetails.HeaderStyle.BackColor = System.Drawing.Color.FromName("#1e3a8a");
                GV_StockPositionDetails.HeaderStyle.ForeColor = System.Drawing.Color.White;

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='15' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>M.P. WAREHOUSING & LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='15' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Hierarchical Stock Position Summary (Qty in MT)</th></tr>
                        <tr><td colspan='7' style='text-align:left; font-weight:bold;'>As On Date: {0}</td><td colspan='8' style='text-align:right; font-weight:bold;'>Generated On: {1}</td></tr>
                        <tr><td colspan='15' style='border:none;'>&nbsp;</td></tr>
                    </table>", txtpaymentdate.Text.Trim(), DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));

                Response.Write(customExcelHeader);
                GV_StockPositionDetails.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}