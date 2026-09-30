using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;

public partial class Reports_NewStorageCapacityReport_RM_Pending_Report : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCommodities();
            LoadCropYears();
            LoadRegions();
        }
    }

    private void LoadCropYears()
    {
        ddlCropYear.Items.Clear();

        int startYear = 2016;
        int currentYear = DateTime.Now.Year;
        int currentMonth = DateTime.Now.Month;

        // Financial Year calculation (April se start)
        if (currentMonth < 4)
            currentYear = currentYear - 1;

        for (int year = startYear; year <= currentYear; year++)
        {
            string cropYear = year + "-" + (year + 1);
            ddlCropYear.Items.Add(cropYear);
        }

        // Default selected current financial year
        if (ddlCropYear.Items.Count > 0)
            ddlCropYear.SelectedIndex = ddlCropYear.Items.Count - 1;
    }

    private void LoadCommodities()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id IN ('22','13','129','3') ORDER BY Commodity_Name",
                con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlCommodity.DataSource = dt;
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();

            // Add "All" option at the top
            //ddlCommodity.Items.Insert(0, new ListItem("-- All Commodities --", ""));
        }
    }

    private void LoadRegions()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT GROUP BY Region_ID, Regionnm", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlRegion.DataSource = dt;
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            //ddlRegion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", ""));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // Reset totals
        totalDeduction = 0;
        totalAmount = 0;
        totalDALG = 0;
        totalOtherDed = 0;
        totalPending = 0;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("RM_Pending_Amount_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                //cmd.Parameters.AddWithValue("@CommodityID", string.IsNullOrEmpty(ddlCommodity.SelectedValue) ? (object)DBNull.Value : ddlCommodity.SelectedValue);
                string selectedIDs = string.Join(",",
                        ddlCommodity.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDs))
                    selectedIDs = "0";   // All commodities
                else
                    selectedIDs = "'" + selectedIDs + "'";
                cmd.Parameters.AddWithValue("@CommodityID", selectedIDs);

                // ****************************************
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }

    decimal totalDeduction = 0;
    decimal totalAmount = 0;
    decimal totalDALG = 0;
    decimal totalOtherDed = 0;
    decimal totalPending = 0;

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Safe conversion with null check
            totalDeduction += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeductionAmount") ?? 0);
            totalAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount") ?? 0);
            totalDALG += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount_DALG") ?? 0);
            totalOtherDed += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other_Deduction") ?? 0);
            totalPending += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingatRM") ?? 0);

            // Format numeric columns
            e.Row.Cells[7].Text = string.IsNullOrEmpty(e.Row.Cells[7].Text) ? "0.00" : Convert.ToDecimal(e.Row.Cells[7].Text).ToString("N2");
            e.Row.Cells[8].Text = string.IsNullOrEmpty(e.Row.Cells[8].Text) ? "0.00" : Convert.ToDecimal(e.Row.Cells[8].Text).ToString("N2");
            e.Row.Cells[9].Text = string.IsNullOrEmpty(e.Row.Cells[9].Text) ? "0.00" : Convert.ToDecimal(e.Row.Cells[9].Text).ToString("N2");
            e.Row.Cells[10].Text = string.IsNullOrEmpty(e.Row.Cells[10].Text) ? "0.00" : Convert.ToDecimal(e.Row.Cells[10].Text).ToString("N2");
            e.Row.Cells[11].Text = string.IsNullOrEmpty(e.Row.Cells[11].Text) ? "0.00" : Convert.ToDecimal(e.Row.Cells[11].Text).ToString("N2");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "TOTAL";
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            // Format footer values
            e.Row.Cells[7].Text = totalDeduction.ToString("N2");
            e.Row.Cells[8].Text = totalAmount.ToString("N2");
            e.Row.Cells[9].Text = totalDALG.ToString("N2");
            e.Row.Cells[10].Text = totalOtherDed.ToString("N2");
            e.Row.Cells[11].Text = totalPending.ToString("N2");

            // Right align numeric columns in footer
            for (int i = 7; i <= 11; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }

            e.Row.Font.Bold = true;
            e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            // Add ARIA attributes for accessibility
            GridView1.HeaderRow.Attributes.Add("aria-rowindex", "1");
        }
    }
}