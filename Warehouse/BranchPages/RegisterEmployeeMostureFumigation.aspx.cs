using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

public partial class BranchPages_RegisterEmployeeMostureFumigation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        try
        {
            string query = @"SELECT Id, Name, MobileNo, Designation, CreatedDate 
                             FROM RegisterEmployeeMostureFumigation 
                             WHERE BranchId ='" + Session["BranchId"].ToString() + "' and DeletedDate IS NULL  ORDER BY Id DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvEmployee.DataSource = dt;
            gvEmployee.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error loading data: " + ex.Message + "');</script>");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            string ip = GetLocalIPAddress();

            // Check duplicate mobile
            string checkQuery = "SELECT COUNT(*) FROM RegisterEmployeeMostureFumigation WHERE MobileNo = @MobileNo AND DeletedDate IS NOT NULL";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text.Trim());

            con.Open();
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
            con.Close();

            if (count > 0)
            {
                Response.Write("<script>alert('Employee already exists with this Mobile Number');</script>");
                return;
            }

            // Insert record
            string insertQuery = @"INSERT INTO RegisterEmployeeMostureFumigation
                (BranchId, Name, MobileNo, Designation, CreatedDate, CreatedByIp)
                VALUES (@BranchId, @Name, @MobileNo, @Designation, GETDATE(), @CreatedByIp)";

            SqlCommand cmd = new SqlCommand(insertQuery, con);
            cmd.Parameters.AddWithValue("@BranchId", Session["BranchID"] ?? "0");
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
            cmd.Parameters.AddWithValue("@CreatedByIp", ip);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("<script>alert('Record Saved Successfully');</script>");
            ClearForm();
            BindGrid();
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open) con.Close();
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }

    protected void gvEmployee_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                string query = "SELECT * FROM RegisterEmployeeMostureFumigation WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    hfID.Value = id.ToString();
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtMobile.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtDesignation.Text = dt.Rows[0]["Designation"].ToString();

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                // 1️⃣ Pehle record select karo
                string selectQuery = "SELECT * FROM RegisterEmployeeMostureFumigation WHERE Id=@Id";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                selectCmd.Parameters.AddWithValue("@Id", id);

                SqlDataAdapter da = new SqlDataAdapter(selectCmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // 2️⃣ Log table me insert karo (Id ko exclude, identity hai)
                    string insertLogQuery = @"
            INSERT INTO RegisterEmployeeMostureFumigation_Log
            (BranchId, Name, MobileNo, Designation, CreatedDate, CreatedByIp, 
             UpdatedDate, UpdatedByIp, DeletedDate, DeletedByIp)
            VALUES
            (@BranchId, @Name, @MobileNo, @Designation, @CreatedDate, @CreatedByIp,
             @UpdatedDate, @UpdatedByIp, GETDATE(), @DeletedByIp)";

                    SqlCommand insertLogCmd = new SqlCommand(insertLogQuery, con);

                    insertLogCmd.Parameters.AddWithValue("@BranchId", dt.Rows[0]["BranchId"]);
                    insertLogCmd.Parameters.AddWithValue("@Name", dt.Rows[0]["Name"]);
                    insertLogCmd.Parameters.AddWithValue("@MobileNo", dt.Rows[0]["MobileNo"] ?? (object)DBNull.Value);
                    insertLogCmd.Parameters.AddWithValue("@Designation", dt.Rows[0]["Designation"] ?? (object)DBNull.Value);
                    insertLogCmd.Parameters.AddWithValue("@CreatedDate", dt.Rows[0]["CreatedDate"] ?? (object)DBNull.Value);
                    insertLogCmd.Parameters.AddWithValue("@CreatedByIp", dt.Rows[0]["CreatedByIp"] ?? (object)DBNull.Value);
                    insertLogCmd.Parameters.AddWithValue("@UpdatedDate", dt.Rows[0]["UpdatedDate"] ?? (object)DBNull.Value);
                    insertLogCmd.Parameters.AddWithValue("@UpdatedByIp", dt.Rows[0]["UpdatedByIp"] ?? (object)DBNull.Value);

                    // Current IP
                    string userIp = GetLocalIPAddress();
                    insertLogCmd.Parameters.AddWithValue("@DeletedByIp", userIp);

                    // 3️⃣ Open connection and insert log
                    con.Open();
                    insertLogCmd.ExecuteNonQuery();

                    // 4️⃣ Delete record from main table
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM RegisterEmployeeMostureFumigation WHERE Id=@Id", con);
                    deleteCmd.Parameters.AddWithValue("@Id", id);
                    deleteCmd.ExecuteNonQuery();

                    con.Close();

                    // 5️⃣ Success message and refresh grid
                    Response.Write("<script>alert('Record deleted successfully and logged.');</script>");
                    BindGrid();
                }
            }

        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open) con.Close();
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfID.Value)) return;
        if (!ValidateInput()) return;

        try
        {
            string ip = GetLocalIPAddress();

            string query = @"UPDATE RegisterEmployeeMostureFumigation
                             SET Name=@Name, MobileNo=@MobileNo, Designation=@Designation,
                                 UpdatedDate=GETDATE(), UpdatedByIp=@UpdatedByIp
                             WHERE Id=@Id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", hfID.Value);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
            cmd.Parameters.AddWithValue("@UpdatedByIp", ip);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("<script>alert('Record Updated Successfully');</script>");
            ClearForm();
            BindGrid();
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open) con.Close();
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtMobile.Text) ||
            string.IsNullOrWhiteSpace(txtDesignation.Text))
        {
            Response.Write("<script>alert('Please fill all required fields');</script>");
            return false;
        }

        long mobileNum;
        if (txtMobile.Text.Trim().Length != 10 || !long.TryParse(txtMobile.Text.Trim(), out mobileNum))
        {
            Response.Write("<script>alert('Please enter a valid 10-digit Mobile Number');</script>");
            return false;
        }

        return true;
    }

    private void ClearForm()
    {
        txtName.Text = "";
        txtMobile.Text = "";
        txtDesignation.Text = "";
        hfID.Value = "";

        btnSave.Visible = true;
        btnUpdate.Visible = false;
        btnCancel.Visible = false;
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        }
        return "0.0.0.0";
    }
}
