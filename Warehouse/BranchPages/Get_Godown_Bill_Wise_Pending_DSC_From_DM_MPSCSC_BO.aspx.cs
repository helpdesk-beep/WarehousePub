using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC_BO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    string currentGodown = string.Empty;

    // Grand Total Accumulator
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
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            grandNetAmount = 0;

            SqlCommand cmd = new SqlCommand("Get_Godown_Bill_Wise_Pending_DSC_From_DM_MPSCSC", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RegionId", 0);
            cmd.Parameters.AddWithValue("@DistrictId", 0);

            // Session se Branch ID nikalna
            int branchId = 0;
            if (Session["BranchID"] != null && !string.IsNullOrEmpty(Session["BranchID"].ToString()))
            {
                int.TryParse(Session["BranchID"].ToString().Trim(), out branchId);
            }
            else if (Session["Branch_ID"] != null && !string.IsNullOrEmpty(Session["Branch_ID"].ToString()))
            {
                int.TryParse(Session["Branch_ID"].ToString().Trim(), out branchId);
            }
            else if (Session["Depot_ID"] != null && !string.IsNullOrEmpty(Session["Depot_ID"].ToString()))
            {
                int.TryParse(Session["Depot_ID"].ToString().Trim(), out branchId);
            }
            else if (Session["Depot_DistID"] != null && !string.IsNullOrEmpty(Session["Depot_DistID"].ToString()))
            {
                int.TryParse(Session["Depot_DistID"].ToString().Trim(), out branchId);
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
            decimal netAmt = 0;
            if (DataBinder.Eval(e.Row.DataItem, "Net_Amount") != DBNull.Value)
            {
                netAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Net_Amount"));
            }

            grandNetAmount += netAmt;
        }
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            Table tbl = (Table)gvReport.Controls[0];

            // Grand Total Row
            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.BackColor = System.Drawing.Color.FromName("#eff6ff");
            grandTotalRow.Font.Bold = true;
            grandTotalRow.ForeColor = System.Drawing.Color.FromName("#1e3a8a");

            // 1st to 7th Column Merge
            TableCell mainCell = new TableCell();
            mainCell.Text = "Grand Total";
            mainCell.ColumnSpan = 8;
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.VerticalAlign = VerticalAlign.Middle;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important;");
            grandTotalRow.Cells.Add(mainCell);

            // 8th Column - Net Amount
            grandTotalRow.Cells.Add(new TableCell { Text = grandNetAmount.ToString("N2"), HorizontalAlign = HorizontalAlign.Right, CssClass = "text-right-align" });

            // 9th Column - Empty Cell (Bill Number Alignment ke liye)
            grandTotalRow.Cells.Add(new TableCell { Text = "" });

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