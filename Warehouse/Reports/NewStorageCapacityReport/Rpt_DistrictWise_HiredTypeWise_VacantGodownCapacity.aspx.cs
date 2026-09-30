using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_DistrictWise_HiredTypeWise_VacantGodownCapacity : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) GetDistrict();
    }

    public void GetDistrict()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string qry = "select distinct District_Id, District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddldistrict.DataSource = dt;
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string qry = "select distinct DepotID, DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlbranch.DataSource = dt;
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
            ddlbranch.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedItem.Text != "--Select--") FillGrid();
    }

    protected void ddlCapacityRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_District_Branch_Wise_Godown_Vacant_Capacity_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Districtid", ddldistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@BranchId", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Range", ddlCapacityRange.SelectedIndex > 0 ? ddlCapacityRange.SelectedValue : "0");

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandTimeout = 3600;
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();

                // Calculate Grand Total for Footer [cite: 1]
                GridView1.FooterRow.Cells[0].Text = "GRAND TOTAL";
                GridView1.FooterRow.Cells[4].Text = dt.Compute("Sum(GodownCapacity)", "").ToString();
                GridView1.FooterRow.Cells[5].Text = dt.Compute("Sum(Quantityofstockstoredinwarehouse)", "").ToString();
                GridView1.FooterRow.Cells[6].Text = dt.Compute("Sum(VacantCapacity)", "").ToString();
                GridView1.FooterRow.CssClass = "grand-total";
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }
}