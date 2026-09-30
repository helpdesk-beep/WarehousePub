using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Region_NCCF_Payment_Details : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    decimal totalReceived = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Region_ID"] == null)
            {
                Response.Redirect("~/SessionExpired.htm");
                return;
            }

            if (!IsPostBack)
            {
                FillDistrict();
                LoadData();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // =========================
    // DISTRICT FILL
    // =========================
    private void FillDistrict()
    {
        try
        {
            string regionId = Session["Region_ID"].ToString();

            SqlCommand cmd = new SqlCommand(
                "select District_Id, District_Name from tbl_MetaData_DISTRICT where Region_ID=@Region_ID",
                con);

            cmd.Parameters.AddWithValue("@Region_ID", regionId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();

            ddlDistrict.Items.Insert(0, new ListItem("--All--", "0"));

            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("--All--", "0"));

            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // =========================
    // DISTRICT CHANGE
    // =========================
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlGodown.Items.Clear();

            SqlCommand cmd = new SqlCommand(
                "select BranchId, DepotName from tbl_MetaData_DEPOT where DistrictId=@DistrictId", con);

            cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();

            ddlBranch.Items.Insert(0, new ListItem("--All--", "0"));
            ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));

            LoadData(); // AUTO REFRESH
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // =========================
    // BRANCH CHANGE
    // =========================
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlGodown.Items.Clear();

            SqlCommand cmd = new SqlCommand(
                "select Godown_ID, Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID=@BranchID", con);

            cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlGodown.DataSource = dt;
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();

            ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));

            LoadData(); // AUTO REFRESH
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // =========================
    // GODOWN CHANGE
    // =========================
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData(); // AUTO REFRESH
    }

    // =========================
    // DATA LOAD
    // =========================
    private void LoadData()
    {
        try
        {
            totalReceived = 0;

            SqlCommand cmd = new SqlCommand("Get_NCCF_Payment_Received_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RegionID",
                Session["Region_ID"].ToString());

            cmd.Parameters.AddWithValue("@DistrictID",
                ddlDistrict.SelectedValue);

            cmd.Parameters.AddWithValue("@Branchid",
                ddlBranch.SelectedValue);

            cmd.Parameters.AddWithValue("@GodownID",
                ddlGodown.SelectedValue);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvPayment.DataSource = dt;
            gvPayment.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    // =========================
    // TOTAL CALCULATION
    // =========================
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal val = 0;

                string txt = e.Row.Cells[12].Text.Replace(",", "");

                decimal.TryParse(txt, out val);

                totalReceived += val;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].ColumnSpan = 12;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

                for (int i = 1; i < 11; i++)
                    e.Row.Cells[i].Visible = false;

                e.Row.Cells[11].Text = totalReceived.ToString("N2");
                e.Row.Cells[11].Font.Bold = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}