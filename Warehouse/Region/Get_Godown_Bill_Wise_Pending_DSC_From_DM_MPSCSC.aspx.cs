using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentDistrict = string.Empty;
    string currentDepot = string.Empty;

    // Subtotal Accumulators
    decimal subDepotAmount = 0;
    decimal subDistrictAmount = 0;
    decimal grandNetAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            FillDistrict();
            FillBranch("0"); // डिफ़ॉल्ट रूप से Branches में 'All' आइटम लोड होगा
            //BindReport();
        }
    }

    // 1. Session Region ID के आधार पर District DropDown भरना
    private void FillDistrict()
    {
        try
        {
            int regionId = 0;
            if (Session["Region_ID"] != null && !string.IsNullOrEmpty(Session["Region_ID"].ToString()))
            {
                regionId = Convert.ToInt32(Session["Region_ID"].ToString().Trim());
            }

            string query = "SELECT District_Id, District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID = @Region_ID ORDER BY District_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Region_ID", regionId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();

            ddlDistrict.Items.Insert(0, new ListItem("-- All District --", "0"));
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error in District: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    // 2. District ID के आधार पर Branch DropDown भरना
    private void FillBranch(string districtId)
    {
        try
        {
            ddlBranch.Items.Clear();

            if (districtId != "0" && !string.IsNullOrEmpty(districtId))
            {
                string query = "SELECT BranchId, DepotName FROM tbl_MetaData_DEPOT WHERE DistrictId = @DistrictId ORDER BY DepotName";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DistrictId", districtId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
            }

            ddlBranch.Items.Insert(0, new ListItem("-- All Branch --", "0"));
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error in Branch: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
    }

    // District Change Event
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranch(ddlDistrict.SelectedValue);
    }

    // Show Data Button Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    // Report Bind
    private void BindReport()
    {
        try
        {
            currentDistrict = string.Empty;
            currentDepot = string.Empty;
            subDepotAmount = 0;
            subDistrictAmount = 0;
            grandNetAmount = 0;

            SqlCommand cmd = new SqlCommand("Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            // Session से Region_ID पास करना
            if (Session["Region_ID"] != null && !string.IsNullOrEmpty(Session["Region_ID"].ToString()))
            {
                cmd.Parameters.AddWithValue("@RegionId", Convert.ToInt32(Session["Region_ID"].ToString().Trim()));
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionId", 0);
            }

            // Selected District ID (अगर All चुना है तो 0 जाएगा)
            int districtId = 0;
            if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedValue != "")
            {
                int.TryParse(ddlDistrict.SelectedValue, out districtId);
            }
            cmd.Parameters.AddWithValue("@DistrictId", districtId);

            // Selected Branch ID (अगर All चुना है तो 0 जाएगा)
            int branchId = 0;
            if (ddlBranch.SelectedValue != null && ddlBranch.SelectedValue != "")
            {
                int.TryParse(ddlBranch.SelectedValue, out branchId);
            }
            cmd.Parameters.AddWithValue("@BranchId", branchId);

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
            string districtName = DataBinder.Eval(e.Row.DataItem, "District_Name").ToString();
            string depotName = DataBinder.Eval(e.Row.DataItem, "DepotName").ToString();

            if (string.IsNullOrEmpty(currentDistrict)) currentDistrict = districtName;
            if (string.IsNullOrEmpty(currentDepot)) currentDepot = depotName;

            Table tbl = (Table)gvReport.Controls[0];
            int rowIndex = tbl.Rows.GetRowIndex(e.Row);

            // Branch Sub Total Row
            if (currentDepot != depotName)
            {
                GridViewRow depotSubTotalRow = CreateSubTotalRow(currentDepot + " - Branch Sub Total", subDepotAmount, "#f1f5f9", "#1e3a8a");
                tbl.Rows.AddAt(rowIndex, depotSubTotalRow);

                currentDepot = depotName;
                subDepotAmount = 0;
            }

            // District Sub Total Row
            if (currentDistrict != districtName)
            {
                GridViewRow districtSubTotalRow = CreateSubTotalRow(currentDistrict + " - District Sub Total", subDistrictAmount, "#e2e8f0", "#0f172a");
                tbl.Rows.AddAt(tbl.Rows.GetRowIndex(e.Row), districtSubTotalRow);

                currentDistrict = districtName;
                subDistrictAmount = 0;
            }

            decimal netAmt = 0;
            if (DataBinder.Eval(e.Row.DataItem, "Net_Amount") != DBNull.Value)
            {
                netAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Net_Amount"));
            }

            subDepotAmount += netAmt;
            subDistrictAmount += netAmt;
            grandNetAmount += netAmt;
        }
    }

    private GridViewRow CreateSubTotalRow(string titleText, decimal totalAmount, string bgColorHex, string textColorHex)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = System.Drawing.Color.FromName(bgColorHex);
        row.Font.Bold = true;
        row.ForeColor = System.Drawing.Color.FromName(textColorHex);

        TableCell cell = new TableCell();
        cell.Text = titleText;
        cell.ColumnSpan = 8; // Net Amount कॉलम से पहले 8 कॉलम स्पैन होंगे
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = totalAmount.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

        return row;
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            if (!string.IsNullOrEmpty(currentDepot))
            {
                GridViewRow finalDepotRow = CreateSubTotalRow(currentDepot + " - Branch Sub Total", subDepotAmount, "#f1f5f9", "#1e3a8a");
                tbl.Rows.Add(finalDepotRow);
            }

            if (!string.IsNullOrEmpty(currentDistrict))
            {
                GridViewRow finalDistrictRow = CreateSubTotalRow(currentDistrict + " - District Sub Total", subDistrictAmount, "#e2e8f0", "#0f172a");
                tbl.Rows.Add(finalDistrictRow);
            }

            // Grand Total
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 8;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            grandTotalRow.Cells.Add(new TableCell { Text = grandNetAmount.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            tbl.Rows.Add(grandTotalRow);
        }
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
        Response.AddHeader("content-disposition", "attachment;filename=Godown_Bill_Wise_Pending_DSC_Report.xls");
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

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:11pt;'>
                        <tr><th colspan='9' style='font-size:16pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH STATE WAREHOUSING CORPORATION</th></tr>
                        <tr><th colspan='9' style='font-size:13pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Godown Bill Wise Pending DSC Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Report Category: Pending DSC Summary</td><td colspan='5' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>
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