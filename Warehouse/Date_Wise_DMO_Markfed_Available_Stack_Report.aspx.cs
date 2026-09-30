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

public partial class StatePages_Date_Wise_DMO_Markfed_Available_Stack_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDepot = string.Empty;
    long subTotalBags = 0;
    decimal subTotalWeight = 0;
    long grandTotalBags = 0;
    decimal grandTotalWeight = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillCommodity();
            BindReport();
        }
    }

    private void FillCommodity()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY ORDER BY Commodity_Name", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCommodity.DataSource = dt;
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
            }
        }
        catch { }
        ddlCommodity.Items.Insert(0, new ListItem("All Commodities", "0"));
    }

    public void btnSearch_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        try
        {
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select a valid Date!');", true);
                return;
            }

            lblPrintDate.Text = Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy");
            lblPrintCommodity.Text = ddlCommodity.SelectedItem.Text;

            currentDepot = string.Empty;
            ResetSubTotalCounters();
            ResetGrandTotalCounters();

            SqlCommand cmd = new SqlCommand("dbo.usp_GetDmoMarkfedAvailableStockReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AsOnDate", txtDate.Text.Trim());
            cmd.Parameters.AddWithValue("@Commodity_Id", Convert.ToInt32(ddlCommodity.SelectedValue));

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
                    cell.Attributes.Add("style", "color:#ffffff !important; text-align:center !important; vertical-align:middle !important; background-color:#1e3a8a !important; font-weight:bold !important; font-size:13px !important; padding:12px !important;");
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

            long bags = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Available Bags"));
            decimal weight = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Available Weight"));

            subTotalBags += bags;
            subTotalWeight += weight;

            grandTotalBags += bags;
            grandTotalWeight += weight;
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#0f172a");

        TableCell cell = new TableCell();
        cell.Text = currentDepot + " - Branch Sub Total";
        cell.ColumnSpan = 7; // Godown ID हटने के कारण 8 से बदलकर 7 किया गया
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "font-weight: bold !important; border: 1px solid #e2e8f0 !important; font-size:13px !important; padding:10px !important; text-align:center !important;");
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = subTotalBags.ToString("N0") });
        row.Cells.Add(new TableCell { Text = subTotalWeight.ToString("N2") });

        for (int i = 1; i < row.Cells.Count; i++)
        {
            row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            row.Cells[i].VerticalAlign = VerticalAlign.Middle;
            row.Cells[i].Attributes.Add("style", "padding-right: 12px !important; font-weight: bold !important; border: 1px solid #e2e8f0 !important; font-size:13px !important; padding:10px !important;");
        }

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
            mainCell.ColumnSpan = 7; // Godown ID हटने के कारण 8 से बदलकर 7 किया गया
            mainCell.HorizontalAlign = HorizontalAlign.Center;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "font-weight: bold !important; border: 1px solid #e2e8f0 !important; font-size:13px !important; padding:10px !important; text-align:center !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandTotalBags.ToString("N0") });
            grandTotalRow.Cells.Add(new TableCell { Text = grandTotalWeight.ToString("N2") });

            for (int i = 1; i < grandTotalRow.Cells.Count; i++)
            {
                grandTotalRow.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                grandTotalRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
                grandTotalRow.Cells[i].Attributes.Add("style", "padding-right: 12px !important; font-weight: bold !important; border: 1px solid #e2e8f0 !important; font-size:13px !important; padding:10px !important;");
            }

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private void ResetSubTotalCounters() { subTotalBags = 0; subTotalWeight = 0; }
    private void ResetGrandTotalCounters() { grandTotalBags = 0; grandTotalWeight = 0; }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data available to export!');", true);
            return;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DMO_Markfed_Available_Stock_Report.xls");
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

                string dateStr = Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy");
                string commodityStr = ddlCommodity.SelectedItem.Text;

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr>
                            <th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center; vertical-align:middle;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th>
                        </tr>
                        <tr>
                            <th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center; vertical-align:middle;'>Date Wise DMO Markfed Available Stack Report</th>
                        </tr>
                        <tr>
                            <td colspan='4' style='text-align:left; font-weight:bold; color:#475569; vertical-align:middle;'>Commodity: {0}</td>
                            <td colspan='5' style='text-align:right; font-weight:bold; color:#475569; vertical-align:middle;'>As On Date: {1}</td>
                        </tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
                    </table>", commodityStr, dateStr);

                Response.Write(customExcelHeader);

                string style = @"<style> 
                                    th { background-color: #1e3a8a !important; color: #ffffff !important; font-weight: bold !important; text-align:center; font-size:11pt; } 
                                    td { border: 1px solid #ccc; font-size:10pt; } 
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
    }
}