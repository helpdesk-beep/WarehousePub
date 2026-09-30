using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Region_Reports_Godown_Bill_Wise_Deduction_Report : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Region_ID"] != null)
            {
                string regionId = Session["Region_ID"].ToString();
                BindDistrict(regionId);
            }
            else
            {
                Response.Write("<script>alert('Session Expired! Please login again.');</script>");
            }
        }
    }

    private void BindDistrict(string regionId)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "SELECT DISTINCT District_ID, District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID = @RegionID ORDER BY District_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RegionID", regionId);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataBind();
                }
            }
        }
        ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0, new ListItem("--Select Godown--", "0"));
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue != "0")
        {
            string districtId = ddlDistrict.SelectedValue;
            BindBranch(districtId);
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("--Select Godown--", "0"));
        }
    }

    private void BindBranch(string districtId)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = "SELECT DISTINCT BranchId, DepotName FROM tbl_MetaData_DEPOT WHERE DistrictId = @DistrictID ORDER BY DepotName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@DistrictID", districtId);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    ddlBranch.DataSource = dt;
                    ddlBranch.DataTextField = "DepotName";
                    ddlBranch.DataValueField = "BranchId";
                    ddlBranch.DataBind();
                }
            }
        }
        ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));

        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0, new ListItem("--Select Godown--", "0"));
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue != "0")
        {
            string branchId = ddlBranch.SelectedValue;
            BindGodown(branchId);
        }
        else
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("--Select Godown--", "0"));
        }
    }

    private void BindGodown(string branchId)
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            //string query = "SELECT DISTINCT Godown_Name, Godown_ID FROM tbl_MetaData_GODOWN_2018 WHERE BranchID = @BranchID ORDER BY Godown_Name";
            string query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID=@BranchID And Hired_Type in('Joint Venture(JV)', 'WDRA', 'Hired', 'Steel Silo', 'BOT', 'Tribal Scheme', 'PVT.PEG', 'Silo Bags') Order By Godown_Name ASC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@BranchID", branchId);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    ddlGodown.DataSource = dt;
                    ddlGodown.DataTextField = "Godown_Name";
                    ddlGodown.DataValueField = "Godown_ID";
                    ddlGodown.DataBind();
                }
            }
        }
        ddlGodown.Items.Insert(0, new ListItem("--Select Godown--", "0"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0" || ddlBranch.SelectedValue == "0")
        {
            Response.Write("<script>alert('Please select District and Branch first!');</script>");
            return;
        }
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Deduction_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                gvReport.DataSource = dt;
                gvReport.DataBind();
            }
            else
            {
                gvReport.DataSource = null;
                gvReport.DataBind();
            }
        }
        //using (SqlConnection con = new SqlConnection(connStr))
        //{
        //    string query = "SELECT * FROM tbl_Godown_Bill_Wise_Deduction WHERE DistrictID = @DistrictID AND BranchID = @BranchID";

        //    if (ddlGodown.SelectedValue != "0")
        //    {
        //        query += " AND GodownID = @GodownID";
        //    }

        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue);
        //        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);

        //        if (ddlGodown.SelectedValue != "0")
        //        {
        //            cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
        //        }

        //        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //        {
        //            DataTable dt = new DataTable();
        //            sda.Fill(dt);

        //            gvReport.DataSource = dt;
        //            gvReport.DataBind();
        //        }
        //    }
        //}
    }
}