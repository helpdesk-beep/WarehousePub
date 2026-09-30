using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class StatePages_NCCF_Payment_Details_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    decimal grandTotal = 0;
    decimal regionTotal = 0;
    string currentRegion = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() != "MPSWLC")
            {
                Response.Redirect("~/SessionExpired.htm");
                return;
            }

            if (!IsPostBack)
            {
                FillRegion();
                FillEmptyDropdowns();
                LoadData();
            }
        }
        catch (Exception ex)
        {
            Response.Write("Page_Load Error: " + ex.Message);
        }
    }

    // ================= REGION =================
    private void FillRegion()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "select distinct Region_Id, Regionnm from tbl_MetaData_DISTRICT",
            con);

        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlRegion.DataSource = dt;
        ddlRegion.DataTextField = "Regionnm";
        ddlRegion.DataValueField = "Region_Id";
        ddlRegion.DataBind();

        ddlRegion.Items.Insert(0, new ListItem("--All--", "0"));
    }

    // ================= EMPTY DROPDOWNS =================
    private void FillEmptyDropdowns()
    {
        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Insert(0, new ListItem("--All--", "0"));

        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, new ListItem("--All--", "0"));

        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));
    }

    // ================= REGION CHANGE =================
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict();
        LoadData();
    }

    private void LoadDistrict()
    {
        ddlDistrict.Items.Clear();

        SqlCommand cmd = new SqlCommand(
            "select District_Id, District_Name from tbl_MetaData_DISTRICT where Region_ID=@RegionID",
            con);

        cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);

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

    // ================= DISTRICT =================
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBranch();
        LoadData();
    }

    private void LoadBranch()
    {
        ddlBranch.Items.Clear();

        SqlCommand cmd = new SqlCommand(
            "select BranchId, DepotName from tbl_MetaData_DEPOT where DistrictId=@DistrictId",
            con);

        cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlBranch.DataSource = dt;
        ddlBranch.DataTextField = "DepotName";
        ddlBranch.DataValueField = "BranchId";
        ddlBranch.DataBind();

        ddlBranch.Items.Insert(0, new ListItem("--All--", "0"));

        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));
    }

    // ================= BRANCH =================
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGodown();
        LoadData();
    }

    private void LoadGodown()
    {
        ddlGodown.Items.Clear();

        SqlCommand cmd = new SqlCommand(
            "select Godown_ID, Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID=@BranchID",
            con);

        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        ddlGodown.DataSource = dt;
        ddlGodown.DataTextField = "Godown_Name";
        ddlGodown.DataValueField = "Godown_ID";
        ddlGodown.DataBind();

        ddlGodown.Items.Insert(0, new ListItem("--All--", "0"));
    }

    // ================= GODOWN =================
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    // ================= LOAD DATA =================
    private void LoadData()
    {
        try
        {
            grandTotal = 0;
            regionTotal = 0;
            currentRegion = "";

            SqlCommand cmd = new SqlCommand("Get_NCCF_Payment_Received_Details_HO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RegionID", GetVal(ddlRegion));
            cmd.Parameters.AddWithValue("@DistrictID", GetVal(ddlDistrict));
            cmd.Parameters.AddWithValue("@Branchid", GetVal(ddlBranch));
            cmd.Parameters.AddWithValue("@GodownID", GetVal(ddlGodown));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvPayment.DataSource = dt;
            gvPayment.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("LoadData Error: " + ex.Message);
        }
    }

    // ================= SAFE VALUE =================
    private string GetVal(DropDownList ddl)
    {
        return (ddl.SelectedValue == "" || ddl.SelectedValue == null) ? "0" : ddl.SelectedValue;
    }

    // ================= GRID =================
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string region = DataBinder.Eval(e.Row.DataItem, "Regionnm").ToString();

                decimal val = 0;
                object obj = DataBinder.Eval(e.Row.DataItem, "ReceivedPayment");

                if (obj != DBNull.Value)
                    decimal.TryParse(obj.ToString(), out val);

                grandTotal += val;

                // first region set
                if (string.IsNullOrEmpty(currentRegion))
                    currentRegion = region;

                // REGION CHANGE → SUBTOTAL ROW INSERT
                if (currentRegion != region)
                {
                    AddSubTotalRow(currentRegion, regionTotal);

                    regionTotal = 0;
                    currentRegion = region;
                }

                regionTotal += val;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // last region subtotal
                AddSubTotalRow(currentRegion, regionTotal);

                // grand total row
                TableCell cell = new TableCell();
                cell.ColumnSpan = 12;
                cell.Text = "GRAND TOTAL";
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;

                TableCell val = new TableCell();
                val.Text = grandTotal.ToString("N2");
                val.Font.Bold = true;
                val.HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells.Clear();
                e.Row.Cells.Add(cell);
                e.Row.Cells.Add(val);
            }
        }
        catch (Exception ex)
        {
            Response.Write("Grid Error: " + ex.Message);
        }
    }
    private void AddSubTotalRow(string region, decimal amount)
    {
        GridViewRow row = new GridViewRow(0, 0,
            DataControlRowType.DataRow,
            DataControlRowState.Normal);

        TableCell cell = new TableCell();
        cell.ColumnSpan = 12;
        cell.Text = "Sub Total - " + region;
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Font.Bold = true;

        TableCell val = new TableCell();
        val.Text = amount.ToString("N2");
        val.Font.Bold = true;
        val.HorizontalAlign = HorizontalAlign.Center;

        row.Cells.Add(cell);
        row.Cells.Add(val);

        gvPayment.Controls[0].Controls.AddAt(gvPayment.Controls[0].Controls.Count - 1, row);
    }
}