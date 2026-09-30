using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_rpt_Nafed_Rent_Bill_Not_Generated_By_Godown : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);// ConnectionString ka naam apne web.config ke mutabiq check kar len

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRegions();
            BindDistricts(0);
            BindBranches("0");
            BindReportGrid();
        }
    }

    private void BindRegions()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Distinct Region_ID, Regionnm FROM tbl_MetaData_DISTRICT ORDER BY Regionnm", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlRegion.DataSource = dt;
                ddlRegion.DataTextField = "Regionnm";
                ddlRegion.DataValueField = "Region_ID";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, new ListItem("-- All Regions --", "0"));
            }
        }
    }

    private void BindDistricts(int regionId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT District_Id, District_Name FROM tbl_MetaData_DISTRICT WHERE (Region_ID = @Region_ID) ORDER BY District_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Region_ID", regionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("-- All Districts --", "0"));
            }
        }
    }

    private void BindBranches(string districtId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT BranchId, DepotName FROM tbl_MetaData_DEPOT WHERE (DistrictId = @District_ID) ORDER BY DepotName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@District_ID", districtId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("-- All Branches --", "0"));
            }
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionId = Convert.ToInt32(ddlRegion.SelectedValue);
        BindDistricts(regionId);
        BindBranches("0");
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string districtId = ddlDistrict.SelectedValue;
        BindBranches(districtId);
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReportGrid();
    }
    private void BindReportGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("rpt_Nafed_Rent_Bill_Not_Generated_By_Godown", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Convert.ToInt32(ddlRegion.SelectedValue));
                cmd.Parameters.AddWithValue("@District_ID", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvReport.DataSource = dt;
                gvReport.DataBind();

                // Save for Excel Export
                ViewState["ReportData"] = dt;
            }
        }
    }

    decimal totalStorageAmount = 0;
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object amountObj = DataBinder.Eval(e.Row.DataItem, "Storage_Bill_Amount");
            if (amountObj != DBNull.Value && amountObj != null)
            {
                totalStorageAmount += Convert.ToDecimal(amountObj);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[0].ColumnSpan = 12;
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            // Hide merged cells
            for (int i = 1; i < 12; i++)
            {
                e.Row.Cells[i].Visible = false;
            }

            e.Row.Cells[12].Text = totalStorageAmount.ToString("N2");
            e.Row.Cells[12].Font.Bold = true;
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Rent_Bill_Not_Generated_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Render gridview to stream
            gvReport.AllowPaging = false;
            gvReport.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel Exporting GridView
    }
}