using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


public partial class StatePages_Rpt_Godown_Lat_long_For_Precurement : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegion();
            fillgridforstate();  // Load all data initially
        }
    }

    // ✅ Get Region List
    public void GetRegion()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string qry = "SELECT DISTINCT Region_ID, Regionnm FROM tbl_MetaData_DISTRICT ORDER BY Regionnm ASC";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "Regionnm";
                ddlregion.DataValueField = "Region_ID";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }

    // ✅ Region change event
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrict();
        ddlbranch.Items.Clear();
        ddlbranch.Items.Insert(0, new ListItem("All", "0"));

        fillgridforstate(); // Load grid according to selected region
    }

    // ✅ Get District List
    public void GetDistrict()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string qry = "SELECT DISTINCT District_Id, District_Name FROM tbl_MetaData_DISTRICT WHERE (@Region_ID='0' OR Region_ID=@Region_ID) ORDER BY District_Name ASC";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }

    // ✅ District change event
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        fillgridforstate(); // Load grid according to district
    }

    // ✅ Get Branch List
    public void GetBranch()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            string qry = "SELECT DISTINCT DepotID, DepotName FROM tbl_MetaData_DEPOT WHERE (@District_Id='0' OR DistrictId=@District_Id)";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "DepotID";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }

    // ✅ Branch change event
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgridforstate(); // Load grid according to branch
    }

    // ✅ Fill Grid (Dynamic — Region/District/Branch all handled)
    public void fillgridforstate()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Old_Godown_Lat_Long_Status_For_State", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue == "0" ? (object)DBNull.Value : ddlregion.SelectedValue);
                    cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue == "0" ? (object)DBNull.Value : ddldistrict.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue == "0" ? (object)DBNull.Value : ddlbranch.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        grdstate.Visible = true;
                        DivRegion.Visible = false;
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        grdstate.Visible = false;
                        DivRegion.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Optional: Display error if needed
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
        }
    }
}