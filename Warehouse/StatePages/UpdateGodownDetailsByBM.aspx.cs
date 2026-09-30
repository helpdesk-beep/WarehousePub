using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class StatePages_UpdateGodownDetailsByBM : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDistricts();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, "--Select--");
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    private void FillDistricts()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT District_Name, District_Id FROM tbl_MetaData_DISTRICT ORDER BY District_Name";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {
            FillBranches(DropDownList1.SelectedValue);
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, "--Select--");
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    private void FillBranches(string districtId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "SELECT DepotName, BranchId FROM tbl_MetaData_DEPOT WHERE DistrictId=@DistrictId ORDER BY DepotName";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@DistrictId", districtId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }

        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            FillGridByBranch(ddlBranch.SelectedValue);
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    private void FillGridByBranch(string branchId)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Capacity_Stock_Position_and_Vacan", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", branchId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }

    //protected void Edit(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;
    //    GridViewRow row = (GridViewRow)lnk.NamingContainer;

    //    hdngdnid.Value = row.Cells[1].Text;
    //    Label8.Text = row.Cells[1].Text;
    //    Label9.Text = row.Cells[2].Text;

    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        string query = "SELECT Latitude, Longitude FROM Get_Godown_Wise_Capacity_Stock_Position_and_Vacant WHERE Godown_ID=@Godown_ID";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        cmd.Parameters.AddWithValue("@Godown_ID", hdngdnid.Value);

    //        con.Open();
    //        SqlDataReader dr = cmd.ExecuteReader();
    //        if (dr.Read())
    //        {
    //            TxtLatitude.Text = dr["Latitude"].ToString();
    //            TextLongitude.Text = dr["Longitude"].ToString();
    //        }
    //        con.Close();
    //    }

    //    popup.Show();
    //}

    protected void Edit(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;

        hdngdnid.Value = row.Cells[1].Text;
        Label8.Text = row.Cells[1].Text;
        Label9.Text = row.Cells[2].Text;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT Godown_Capacity, Godown_Scintific_Capacity, Latitude, Longitude, IsActive 
                         FROM Get_Godown_Wise_Capacity_Stock_Position_and_Vacant 
                         WHERE Godown_ID=@Godown_ID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Godown_ID", hdngdnid.Value);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                // Fill TextBoxes
                txtVacantCapacity.Text = dr["Godown_Capacity"].ToString();
                txtUnloadCapacity.Text = dr["Godown_Scintific_Capacity"].ToString();
                TxtLatitude.Text = dr["Latitude"].ToString();
                TextLongitude.Text = dr["Longitude"].ToString();

                // Set Status dropdown
                string status = dr["IsActive"].ToString();
                if (status == "Y")
                    Godownflag.SelectedValue = "Y";
                else if (status == "N")
                    Godownflag.SelectedValue = "N";
                else
                    Godownflag.SelectedValue = "0"; // Default -Select-
            }
            con.Close();
        }

        popup.Show();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtVacantCapacity.Text) || string.IsNullOrEmpty(txtUnloadCapacity.Text) || Godownflag.SelectedValue == "0")
        {
            lblmsg.Text = "Please fill Godown Capacity, Scientific Capacity, and Status.";
            return;
        }

        if (string.IsNullOrEmpty(TxtLatitude.Text) || string.IsNullOrEmpty(TextLongitude.Text))
        {
            lblmsg.Text = "Please fill Latitude and Longitude.";
            return;
        }

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sp_Update_Godown_Capacity_Stock_And_Active", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistID", Hiddendistid.Value);
                cmd.Parameters.AddWithValue("@Branch_ID", Hiddenbranch.Value);
                cmd.Parameters.AddWithValue("@Godown_ID", hdngdnid.Value);
                cmd.Parameters.AddWithValue("@GodownCapacity", txtVacantCapacity.Text);
                cmd.Parameters.AddWithValue("@GodownScienceCapacity", txtUnloadCapacity.Text);
                cmd.Parameters.AddWithValue("@Latitude", TxtLatitude.Text);
                cmd.Parameters.AddWithValue("@Longitude", TextLongitude.Text);
                cmd.Parameters.AddWithValue("@IsActive", Godownflag.SelectedValue);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        lblmsg.Text = "Godown updated successfully!";
        FillGridByBranch(ddlBranch.SelectedValue);
    }
}
