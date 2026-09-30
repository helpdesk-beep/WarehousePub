using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Depositor_Wise_CropYear_Wise_Commodity_Wise_Stock_Position : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDepositor = string.Empty;

    decimal[] subTotals = new decimal[12];
    decimal[] grandTotals = new decimal[12];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblPrintDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            BindStockReport(true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtFromDate.Text))
        {
            lblPrintDate.Text = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy");
        }
        BindStockReport(true);
    }

    protected void chkCommodityList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Automatically triggers filtering instantly on checkbox change
        BindStockReport(false);

        // FIXED HINT: Injects JS to keep the checkbox dropdown open so user can keep checking multiple items seamlessly
        ScriptManager.RegisterStartupScript(this, this.GetType(), "KeepDropdownOpen", "document.getElementById('dropdownMenuContainer').style.display='block'; updateCaptionLabelText();", true);
    }

    private void BindStockReport(bool reloadCheckboxes)
    {
        try
        {
            currentDepositor = string.Empty;
            Array.Clear(subTotals, 0, subTotals.Length);
            Array.Clear(grandTotals, 0, grandTotals.Length);

            DataTable dtMaster;

            if (reloadCheckboxes || ViewState["MasterStockData"] == null)
            {
                SqlCommand cmd = new SqlCommand("Depositor_Wise_CropYear_Wise_Commodity_Wise_Stock_Position", con);
                cmd.CommandType = CommandType.StoredProcedure;

                DateTime parsedDate;
                if (DateTime.TryParse(txtFromDate.Text, out parsedDate))
                {
                    cmd.Parameters.AddWithValue("@FromDate", parsedDate.ToString("yyyy-MM-dd"));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Now.ToString("yyyy-MM-dd"));
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtMaster = new DataTable();
                da.Fill(dtMaster);

                ViewState["MasterStockData"] = dtMaster;

                if (reloadCheckboxes)
                {
                    FillCommodityCheckBoxList(dtMaster);
                }
            }
            else
            {
                dtMaster = (DataTable)ViewState["MasterStockData"];
            }

            DataTable dtFiltered = dtMaster;

            List<string> selectedCommodities = new List<string>();
            bool hasAllSelected = false;

            foreach (ListItem item in chkCommodityList.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "0")
                    {
                        hasAllSelected = true;
                        break;
                    }
                    selectedCommodities.Add(item.Value.Replace("'", "''"));
                }
            }

            if (!hasAllSelected && selectedCommodities.Count > 0)
            {
                DataView dv = new DataView(dtMaster);
                StringBuilder sbFilter = new StringBuilder();
                sbFilter.Append("Commodity IN (");

                for (int i = 0; i < selectedCommodities.Count; i++)
                {
                    sbFilter.Append("'" + selectedCommodities[i] + "'");
                    if (i < selectedCommodities.Count - 1) sbFilter.Append(",");
                }
                sbFilter.Append(")");

                dv.RowFilter = sbFilter.ToString();
                dtFiltered = dv.ToTable();

                lblPrintCommodity.Text = string.Join(", ", selectedCommodities);
            }
            else
            {
                lblPrintCommodity.Text = "All Commodities";
                if (chkCommodityList.Items.Count > 0 && selectedCommodities.Count == 0)
                {
                    chkCommodityList.Items[0].Selected = true;
                }
            }

            gvStock.DataSource = dtFiltered;
            gvStock.DataBind();

            if (dtFiltered.Rows.Count > 0)
            {
                gvStock.UseAccessibleHeader = true;
                gvStock.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution failure logs: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    private void FillCommodityCheckBoxList(DataTable dt)
    {
        List<string> previouslySelected = new List<string>();
        foreach (ListItem item in chkCommodityList.Items)
        {
            if (item.Selected) previouslySelected.Add(item.Value);
        }

        chkCommodityList.Items.Clear();
        ListItem allItem = new ListItem("-- All Commodities --", "0");
        allItem.Selected = (previouslySelected.Count == 0 || previouslySelected.Contains("0"));
        chkCommodityList.Items.Add(allItem);

        if (dt != null && dt.Rows.Count > 0)
        {
            DataView view = new DataView(dt);
            DataTable distinctCommodities = view.ToTable(true, "Commodity");

            foreach (DataRow row in distinctCommodities.Rows)
            {
                if (row["Commodity"] != DBNull.Value && !string.IsNullOrEmpty(row["Commodity"].ToString()))
                {
                    string name = row["Commodity"].ToString().Trim();
                    ListItem item = new ListItem(name, name);

                    if (previouslySelected.Contains(name) && !previouslySelected.Contains("0")) item.Selected = true;

                    chkCommodityList.Items.Add(item);
                }
            }
        }
    }

    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string depositorName = DataBinder.Eval(e.Row.DataItem, "depositor_Name").ToString();

            if (string.IsNullOrEmpty(currentDepositor))
            {
                currentDepositor = depositorName;
            }

            if (currentDepositor != depositorName)
            {
                Table tbl = (Table)gvStock.Controls[0];
                int rowIndex = tbl.Rows.GetRowIndex(e.Row);

                GridViewRow subTotalRow = CreateSubTotalRow();
                tbl.Rows.AddAt(rowIndex, subTotalRow);

                currentDepositor = depositorName;
                Array.Clear(subTotals, 0, subTotals.Length);
            }

            for (int i = 0; i <= 10; i++)
            {
                decimal val = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[" + (2016 + i) + "-" + (17 + i - (17 + i >= 100 ? 100 : 0)).ToString("D2") + "]"));
                subTotals[i] += val;
                grandTotals[i] += val;
            }

            decimal totalQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            subTotals[11] += totalQty;
            grandTotals[11] += totalQty;

            for (int cellIdx = 3; cellIdx <= 14; cellIdx++)
            {
                e.Row.Cells[cellIdx].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    private GridViewRow CreateSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName("#f1f5f9");
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

        TableCell cell = new TableCell { Text = currentDepositor + " - Total Balance Summary", ColumnSpan = 3 };
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:12px !important; font-weight:bold !important;");
        row.Cells.Add(cell);

        for (int i = 0; i <= 11; i++)
        {
            row.Cells.Add(createTotalCell(subTotals[i].ToString("N2")));
        }

        return row;
    }

    protected void gvStock_DataBound(object sender, EventArgs e)
    {
        if (gvStock.Rows.Count > 0)
        {
            Table tbl = (Table)gvStock.Controls[0];

            if (!string.IsNullOrEmpty(currentDepositor))
            {
                GridViewRow finalSubTotalRow = CreateSubTotalRow();
                tbl.Rows.Add(finalSubTotalRow);
            }

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell { Text = "State Grand Total Position", ColumnSpan = 3 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:12px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            for (int i = 0; i <= 11; i++)
            {
                grandTotalRow.Cells.Add(createTotalCell(grandTotals[i].ToString("N2")));
            }

            tbl.Rows.Add(grandTotalRow);
        }
    }

    private TableCell createTotalCell(string text)
    {
        TableCell cell = new TableCell { Text = text, HorizontalAlign = HorizontalAlign.Right };
        cell.CssClass = "text-right-align";
        cell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:10px !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
        return cell;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvStock.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Depositor_CropYear_Wise_Stock_Summary.xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindStockReport(false);
                Response.Write("<style> .text-right-align { text-align:right !important; } td { white-space:normal; } </style>");

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string activeSelectionDate = !string.IsNullOrEmpty(txtFromDate.Text) ? Convert.ToDateTime(txtFromDate.Text).ToString("dd-MM-yyyy") : dateTimeStr;

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='15' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='15' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Depositor Wise, Crop Year Wise & Commodity Wise Stock Position</th></tr>
                        <tr><td colspan='8' style='text-align:left; font-weight:bold; color:#475569;'>Stock Position As On: {0} | Filter: {1}</td><td colspan='7' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {2}</td></tr>
                        <tr><td colspan='15' style='border:none;'>&nbsp;</td></tr>
                    </table>", activeSelectionDate, lblPrintCommodity.Text, dateTimeStr);

                Response.Write(customExcelHeader);
                gvStock.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}