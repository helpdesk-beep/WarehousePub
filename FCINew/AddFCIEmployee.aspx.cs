using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

public partial class FCI_AddFCIEmployee : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDistricts();
            BindGrid();
        }
    }

    private void BindDistricts()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            string query = "SELECT DISTINCT District_Id, District_Name FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT ORDER BY District_Name";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select District--", "0"));
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBranches(ddlDistrict.SelectedValue);
    }

    private void BindBranches(string districtId)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            string query = "SELECT DISTINCT BranchId, DepotName FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT WHERE DistrictId=@DistID ORDER BY DepotName";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@DistID", districtId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            ddlBranch.DataSource = dt;
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
        }
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            // JOIN query with Collation fix
            string query = @"SELECT E.*, D.District_Name, B.DepotName 
                             FROM FCI_Employee E
                             LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT D 
                                ON E.District_Id COLLATE DATABASE_DEFAULT = D.District_Id COLLATE DATABASE_DEFAULT
                             LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT B 
                                ON E.Branch_Id COLLATE DATABASE_DEFAULT = B.BranchId COLLATE DATABASE_DEFAULT
                             ORDER BY E.FCI_ID DESC";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            dt = new DataTable();
            sda.Fill(dt);
            gvEmployees.DataSource = dt;
            gvEmployees.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0" || ddlBranch.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select District and Branch');", true);
            return;
        }

        SqlConnection con = new SqlConnection(connString);

        try
        {
            string query = string.IsNullOrEmpty(hfEmployeeID.Value)
                ? "INSERT INTO FCI_Employee (Emp_Name, Mobile_No, Allocated_Date, Status, District_Id, Branch_Id, Designation) VALUES (@Name, @Mobile, @Date, @Status, @Dist, @Branch, @Designation)"
                : "UPDATE FCI_Employee SET Emp_Name=@Name, Mobile_No=@Mobile, Allocated_Date=@Date, Status=@Status, District_Id=@Dist, Branch_Id=@Branch, Designation=@Designation WHERE FCI_ID=@ID";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Date", string.IsNullOrEmpty(txtDate.Text) ? (object)DBNull.Value : txtDate.Text);
            cmd.Parameters.AddWithValue("@Status", chkStatus.Checked);
            cmd.Parameters.AddWithValue("@Dist", ddlDistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());

            if (!string.IsNullOrEmpty(hfEmployeeID.Value))
                cmd.Parameters.AddWithValue("@ID", hfEmployeeID.Value);

            con.Open();
            cmd.ExecuteNonQuery();

            ClearFields();
            BindGrid();

            ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Record saved successfully!');", true);
        }
        catch (SqlException ex)
        {
            // 🔴 DUPLICATE MOBILE ERROR
            if (ex.Number == 2627 || ex.Number == 2601)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "dup", "alert('Mobile number already exists! Please enter a different number.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Database error occurred!');", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "error", "alert('Something went wrong!');", true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditEmployee")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int id = Convert.ToInt32(gvEmployees.DataKeys[rowIndex].Value);

            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM FCI_Employee WHERE FCI_ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    hfEmployeeID.Value = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["FCI_ID"].ToString()) ? dr["FCI_ID"].ToString() : "");
                    txtName.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["Emp_Name"].ToString()) ? dr["Emp_Name"].ToString() : ""); 
                    txtMobile.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["Mobile_No"].ToString()) ? dr["Mobile_No"].ToString() : "");
                    if (dr["Allocated_Date"] != DBNull.Value) 
                        txtDate.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(Convert.ToDateTime(dr["Allocated_Date"]).ToString("yyyy-MM-dd")) ? Convert.ToDateTime(dr["Allocated_Date"]).ToString("yyyy-MM-dd") : "");
                    chkStatus.Checked = Convert.ToBoolean(HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["Status"].ToString()) ? dr["Status"].ToString() : "0"));

                    ddlDistrict.SelectedValue = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["District_Id"].ToString()) ? dr["District_Id"].ToString() : "");
                    BindBranches(ddlDistrict.SelectedValue);
                    ddlBranch.SelectedValue = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["Branch_Id"].ToString()) ? dr["Branch_Id"].ToString() : "");
                    txtDesignation.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["Designation"].ToString()) ? dr["Designation"].ToString() : "");


                    // Update UI to Edit Mode
                    btnSave.Text = "<i class='fas fa-sync-alt me-2'></i>Update Employee";
                    btnSave.CssClass = "btn btn-warning px-5 fw-bold shadow-sm";
                    btnCancel.Visible = true;
                }
            }
        }
    }

    protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);
        using (SqlConnection con = new SqlConnection(connString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM FCI_Employee WHERE FCI_ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
            ScriptManager.RegisterStartupScript(this, GetType(), "del", "alert('Record deleted');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        hfEmployeeID.Value = "";
        txtName.Text = txtMobile.Text = txtDesignation.Text = txtDate.Text = "";
        ddlDistrict.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("--Select Branch--", "0"));
        chkStatus.Checked = false;
        btnSave.Text = "<i class='fas fa-save me-2'></i>Save Employee";
        btnSave.CssClass = "btn btn-primary px-5 fw-bold shadow-sm";
        btnCancel.Visible = false;
    }
}