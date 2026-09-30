using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Get_District_Password : System.Web.UI.Page
{
  // Database connection using the key from web.config [cite: 1]
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                FillDistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void FillDistrict()
    {
      // Populates the dropdown with District names [cite: 3]
        string qry = "SELECT District_Name, District_Id FROM tbl_MetaData_DISTRICT ORDER BY District_Name";
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillDistrictGrid();
    }

    private void FillDistrictGrid()
    {
        try
        {
          // Calling the Stored Procedure 
            using (SqlCommand cmd = new SqlCommand("Get_District_Password_New", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
              // Passes the selected value from dropdown [cite: 3]
                cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                  // Binds result to the GridView 
                    gvDistrict.DataSource = dt;
                    gvDistrict.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            // Simple error handling to see if something fails
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }
}