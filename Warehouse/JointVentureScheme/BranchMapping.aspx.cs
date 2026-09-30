using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_BranchMapping : System.Web.UI.Page
{
    // Connection string from web.config[cite: 3, 5]
    string connectionString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Security: Prevent back button after logout[cite: 5]
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        // Session check and user display[cite: 3, 5]
        if (Session["UserName"] != null)
        {
            lblUser.Text = Session["UserName"].ToString();
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }

        if (!IsPostBack)
        {
            BindDistricts();
        }
    }

    private void BindDistricts()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT District_ID, District_Name FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT ORDER BY District_Name";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue != "0")
        {
            BindGrid();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a District');", true);
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetBranchBlockMapping", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_ID", ddlDistrict.SelectedValue);
               // cmd.Parameters.AddWithValue("@Block_ID", ""); // Blank to get all blocks for the selected district[cite: 3, 5]

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvMapping.DataSource = dt;
                gvMapping.DataBind();
            }
        }
    }

    protected void gvMapping_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvMapping.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void gvMapping_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvMapping.EditIndex = -1;
        BindGrid();
    }

    protected void gvMapping_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // Get primary keys from DataKeyNames[cite: 3, 5]
        string distId = gvMapping.DataKeys[e.RowIndex].Values["District_ID"].ToString();
        string blockId = gvMapping.DataKeys[e.RowIndex].Values["Block_ID"].ToString();

        // Extract updated values from textboxes
        GridViewRow row = gvMapping.Rows[e.RowIndex];
        string branchId = ((TextBox)row.FindControl("txtBranchID")).Text.Trim();
        string branchName = ((TextBox)row.FindControl("txtBranchName")).Text.Trim();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_UpdateBranchBlockMapping", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", branchId);
                cmd.Parameters.AddWithValue("@Branch_Name", branchName);
                cmd.Parameters.AddWithValue("@Block_ID", blockId);
                cmd.Parameters.AddWithValue("@District_ID", distId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        gvMapping.EditIndex = -1; // Exit edit mode
        BindGrid(); // Refresh grid data
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        // Redirect to home page as requested
        Response.Redirect("DistrictWiseJVSOffer.aspx");
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        // Clear session and redirect to login
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Logins.aspx");
    }

    // Keep your existing Page_Load, BindDistricts, and BindGrid methods below...
}