using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_rpt_Nafed_Rent_Bill_Not_Generated_By_Region : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        // Session Check
        if (Session["Region_Id"] == null && Session["Region_ID"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            int regionId = GetRegionIdFromSession();
            BindDistricts(regionId);
            BindBranches("0", regionId);
            BindReportGrid();
        }
    }

    // Dynamic Session Key Reader
    private int GetRegionIdFromSession()
    {
        if (Session["Region_Id"] != null)
            return Convert.ToInt32(Session["Region_Id"]);
        else if (Session["Region_ID"] != null)
            return Convert.ToInt32(Session["Region_ID"]);

        return 0;
    }

    private void BindDistricts(int regionId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT District_Id, District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID = @Region_ID ORDER BY District_Name";
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

    private void BindBranches(string districtId, int regionId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT D.BranchId, D.DepotName 
                            FROM tbl_MetaData_DEPOT D
                            INNER JOIN tbl_MetaData_DISTRICT DIS ON D.DistrictId = DIS.District_Id
                            WHERE DIS.Region_ID = @Region_ID 
                              AND (@District_ID = '0' OR D.DistrictId = @District_ID) 
                            ORDER BY D.DepotName";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Region_ID", regionId);
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

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int regionId = GetRegionIdFromSession();
        string districtId = ddlDistrict.SelectedValue;
        BindBranches(districtId, regionId);
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReportGrid();
    }

    private void BindReportGrid()
    {
        int regionId = GetRegionIdFromSession();

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("rpt_Nafed_Rent_Bill_Not_Generated_By_Godown", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", regionId);
                cmd.Parameters.AddWithValue("@District_ID", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvReport.DataSource = dt;
                gvReport.DataBind();
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

            // Merge empty cells in footer
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
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Rent_Bill_Not_Generated_Region_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvReport.AllowPaging = false;
            gvReport.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel Export
    }
}