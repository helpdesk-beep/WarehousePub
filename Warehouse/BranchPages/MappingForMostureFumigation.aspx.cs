using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_MappingForMostureFumigation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEmployee(con);
            BindGrid();
            ClearForm();
        }
    }

    private void BindGrid()
    {
        string query = @"
            SELECT 
                G.Godown_ID, 
                G.Godown_Name, 
                F.Id AS FumigationId,
                F.Designation,
                F.Financial_Year,
                F.Month,
                CASE 
                    WHEN F.Type = 1 THEN 'Moisture'
                    WHEN F.Type = 2 THEN 'Fumigation'
                    WHEN F.Type = 3 THEN 'Both'
                    ELSE 'Unknown'
                END AS Type,
                F.Name AS FumigationName,
                F.MobileNo, 
                R.Id AS EmployeeId, 
                R.MobileNo AS EmployeeMobileNo, 
                R.Name AS EmployeeName, 
                R.BranchId,
                F.CreatedDate
            FROM dbo.MappingForMostureFumigation F 
            INNER JOIN dbo.RegisterEmployeeMostureFumigation R ON F.EmployeeId = R.Id 
            INNER JOIN dbo.tbl_MetaData_GODOWN_2018 G ON F.GodownID = G.Godown_ID
            WHERE F.BranchId = @BranchId 
            ORDER BY F.Id DESC";

        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvMapping.DataSource = dt;
        gvMapping.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ip = GetLocalIPAddress();

            //// Check for duplicate record
            //SqlCommand checkCmd = new SqlCommand(@"
            //    SELECT TOP 1 Id 
            //    FROM MappingForMostureFumigation
            //    WHERE EmployeeId = @EmployeeId 
            //    AND GodownID = @GodownID 
            //    AND Financial_Year = @FinYear 
            //    AND Month = @Month
            //    AND Type = @Type", con);

            //checkCmd.Parameters.AddWithValue("@EmployeeId", ddlEmployee.SelectedValue);
            //checkCmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
            //checkCmd.Parameters.AddWithValue("@FinYear", ddlFinancial_Year.SelectedValue);
            //checkCmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
            //checkCmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);

            //con.Open();
            //object result = checkCmd.ExecuteScalar();
            //con.Close();

            //if (result != null)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
            //        "alert('Duplicate Record Found! This Employee is already mapped to this Godown for the selected Year, Month and Type.');", true);
            //    return;
            //}

            // Check for duplicate/conflicting record
            SqlCommand checkCmd = new SqlCommand(@"
            SELECT TOP 1 Id
FROM MappingForMostureFumigation
WHERE EmployeeId = @EmployeeId
  AND GodownID = @GodownID
  AND Financial_Year = @FinYear
  AND Month = @Month
  AND (
        Type = @Type
        OR (@Type = 3 AND Type IN (1,2))
        OR (@Type IN (1,2) AND Type = 3)
      )", con);
            checkCmd.Parameters.AddWithValue("@EmployeeId", ddlEmployee.SelectedValue);
            checkCmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
            checkCmd.Parameters.AddWithValue("@FinYear", ddlFinancial_Year.SelectedValue);
            checkCmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
            checkCmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);

            con.Open();
            object result = checkCmd.ExecuteScalar();
            con.Close();

            //if (result != null)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
            //        "alert('Employee is already mapped for the selected Godown, Year and Month. Type 1/2 and Type 3 cannot be assigned together.');", true);
            //    return;
            //}

            if (result != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('चयनित कर्मचारी की मैपिंग पहले से मौजूद है। यदि कर्मचारी Moisture या Fumigation में मैप है तो उसे Both में मैप नहीं किया जा सकता तथा यदि वह Both में मैप है तो उसे टाइप Moisture या Fumigation में मैप नहीं किया जा सकता।');", true);
                return;
            }

            // Insert new record
            InsertEmployeeRecord(ip);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Record Saved Successfully!');", true);

            ClearForm();
            BindGrid();
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open) con.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error: " + ex.Message + "');", true);
        }
    }

    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string ip = GetLocalIPAddress();
    //        UpdateEmployeeRecord(hfID.Value, ip);
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
    //            "alert('Record Updated Successfully!');", true);

    //        ClearForm();
    //        BindGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
    //            "alert('Error: " + ex.Message + "');", true);
    //    }
    //}

    private void InsertEmployeeRecord(string ip)
    {
        SqlCommand cmd = new SqlCommand(@"
            INSERT INTO MappingForMostureFumigation
                (BranchID, GodownID, Name, MobileNo, Designation, CreatedDate, CreatedByIp, Type, EmployeeId, Financial_Year, Month)
            VALUES
                (@BranchID, @GodownID, @Name, @MobileNo, @Designation, GETDATE(), @CreatedByIp, @Type, @EmployeeId, @Financial_Year, @Month)", con);

        cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
        cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
        cmd.Parameters.AddWithValue("@Name", ddlEmployee.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text.Trim());
        cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
        cmd.Parameters.AddWithValue("@CreatedByIp", ip);
        cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);
        cmd.Parameters.AddWithValue("@EmployeeId", ddlEmployee.SelectedValue);
        cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancial_Year.SelectedValue);
        cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);

        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }

    private void UpdateEmployeeRecord(string fumigationId, string ip)
    {
        SqlCommand updateCmd = new SqlCommand(@"
            UPDATE MappingForMostureFumigation
            SET Name = @Name,
                MobileNo = @MobileNo,
                Designation = @Designation,                
                Type = @Type,
                UpdatedDate = GETDATE(),
                UpdatedByIp = @UpdatedByIp
            WHERE Id = @Id", con);

        updateCmd.Parameters.AddWithValue("@Id", fumigationId);
        updateCmd.Parameters.AddWithValue("@Name", ddlEmployee.SelectedItem.Text);
        updateCmd.Parameters.AddWithValue("@MobileNo", txtMobile.Text.Trim());
        updateCmd.Parameters.AddWithValue("@Designation", txtDesignation.Text.Trim());
        updateCmd.Parameters.AddWithValue("@UpdatedByIp", ip);
        updateCmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);

        con.Open();
        updateCmd.ExecuteNonQuery();
        con.Close();
        ClearForm();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    protected void gvMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DeleteRecord(id);
            BindGrid();
        }
        else if (e.CommandName == "EditRecord")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            LoadRecordForEdit(id);
        }
    }

    private void DeleteRecord(int id)
    {
        try
        {
            // Get record data for logging
            string selectQuery = "SELECT * FROM MappingForMostureFumigation WHERE Id = @Id";
            SqlCommand selectCmd = new SqlCommand(selectQuery, con);
            selectCmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(selectCmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Insert into log table
                string insertLogQuery = @"
                    INSERT INTO MappingForMostureFumigation_Log
                    (Id, BranchID, GodownID, Name, MobileNo, Designation, CreatedDate, CreatedByIp,
                     DeletedDate, DeletedByIp, UpdatedDate, UpdatedByIp, Type, Financial_Year, Month, EmployeeId, ActionType)
                    VALUES
                    (@Id, @BranchID, @GodownID, @Name, @MobileNo, @Designation, @CreatedDate, @CreatedByIp,
                     GETDATE(), @DeletedByIp, @UpdatedDate, @UpdatedByIp, @Type, @Financial_Year, @Month, @EmployeeId, @ActionType)";

                SqlCommand insertLogCmd = new SqlCommand(insertLogQuery, con);
                insertLogCmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"]);
                insertLogCmd.Parameters.AddWithValue("@BranchID", dt.Rows[0]["BranchID"]);
                insertLogCmd.Parameters.AddWithValue("@GodownID", dt.Rows[0]["GodownID"]);
                insertLogCmd.Parameters.AddWithValue("@Name", dt.Rows[0]["Name"]);
                insertLogCmd.Parameters.AddWithValue("@MobileNo", dt.Rows[0]["MobileNo"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@Designation", dt.Rows[0]["Designation"]);
                insertLogCmd.Parameters.AddWithValue("@CreatedDate", dt.Rows[0]["CreatedDate"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@CreatedByIp", dt.Rows[0]["CreatedByIp"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@UpdatedDate", dt.Rows[0]["UpdatedDate"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@UpdatedByIp", dt.Rows[0]["UpdatedByIp"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@Type", dt.Rows[0]["Type"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@Financial_Year", dt.Rows[0]["Financial_Year"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@Month", dt.Rows[0]["Month"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@EmployeeId", dt.Rows[0]["EmployeeId"] ?? DBNull.Value);
                insertLogCmd.Parameters.AddWithValue("@DeletedByIp", Request.ServerVariables["REMOTE_ADDR"]);
                insertLogCmd.Parameters.AddWithValue("@ActionType", "DELETE");

                con.Open();
                insertLogCmd.ExecuteNonQuery();

                // Delete from main table
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM MappingForMostureFumigation WHERE Id=@Id", con);
                deleteCmd.Parameters.AddWithValue("@Id", id);
                deleteCmd.ExecuteNonQuery();
                con.Close();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Record deleted successfully!');", true);
            }
        }
        catch (Exception ex)
        {
            if (con.State == ConnectionState.Open) con.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error deleting record: " + ex.Message + "');", true);
        }
    }

    private void LoadRecordForEdit(int id)
    {
        try
        {
            string query = @"
                SELECT M.Id, M.GodownID, M.Type, M.Financial_Year, M.Month,
                       E.Id AS EmployeeId, E.Name AS EmployeeName, E.MobileNo, E.Designation
                FROM MappingForMostureFumigation M
                INNER JOIN RegisterEmployeeMostureFumigation E ON M.EmployeeId = E.Id
                WHERE M.Id = @Id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                hfID.Value = dt.Rows[0]["Id"].ToString();
                hfEditMode.Value = "true";

                // Fill form fields
                txtMobile.Text = dt.Rows[0]["MobileNo"].ToString();
                txtDesignation.Text = dt.Rows[0]["Designation"].ToString();

                // Set dropdown values
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                ddlType.SelectedValue = dt.Rows[0]["Type"].ToString();
                ddlFinancial_Year.SelectedValue = dt.Rows[0]["Financial_Year"].ToString();
                ddlMonth.SelectedValue = dt.Rows[0]["Month"].ToString();

                // Handle Godown
                string godownId = dt.Rows[0]["GodownID"].ToString();
                string godownName = GetGodownName(godownId);

                // Add to dropdown if not exists
                if (ddlGodown.Items.FindByValue(godownId) == null)
                {
                    ddlGodown.Items.Add(new ListItem(godownName, godownId));
                }
                ddlGodown.SelectedValue = godownId;

                // Disable fields in edit mode
                //ddlType.Enabled = false;
                ddlFinancial_Year.Enabled = false;
                ddlMonth.Enabled = false;
                ddlGodown.Enabled = false;

                // Add disabled CSS class
                //ddlType.CssClass += " disabled-field";
                ddlFinancial_Year.CssClass += " disabled-field";
                ddlMonth.CssClass += " disabled-field";
                ddlGodown.CssClass += " disabled-field";

                // Show/Hide buttons
                btnSave.Visible = false;
                //btnUpdate.Visible = true;
                btnCancel.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error loading record: " + ex.Message + "');", true);
        }
    }

    private string GetGodownName(string godownId)
    {
        SqlCommand cmd = new SqlCommand("SELECT Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE Godown_ID=@ID", con);
        cmd.Parameters.AddWithValue("@ID", godownId);
        con.Open();
        object val = cmd.ExecuteScalar();
        con.Close();
        return val != null ? val.ToString() : "";
    }

    private void ClearForm()
    {
        ddlEmployee.ClearSelection();
        if (ddlEmployee.Items.Count > 0)
            ddlEmployee.SelectedIndex = 0;

        ddlGodown.ClearSelection();
        txtMobile.Text = "";
        txtDesignation.Text = "";
        ddlType.SelectedIndex = 0;
        ddlFinancial_Year.SelectedIndex = 0;
        ddlMonth.SelectedIndex = 0;

        hfEmployeeValue.Value = "";
        hfGodownValue.Value = "";
        hfID.Value = "";
        hfEditMode.Value = "false";

        // Enable all fields
        ddlType.Enabled = true;
        ddlFinancial_Year.Enabled = true;
        ddlMonth.Enabled = true;
        ddlGodown.Enabled = true;

        // Remove disabled CSS class
        ddlType.CssClass = ddlType.CssClass.Replace(" disabled-field", "");
        ddlFinancial_Year.CssClass = ddlFinancial_Year.CssClass.Replace(" disabled-field", "");
        ddlMonth.CssClass = ddlMonth.CssClass.Replace(" disabled-field", "");
        ddlGodown.CssClass = ddlGodown.CssClass.Replace(" disabled-field", "");

        btnSave.Visible = true;
        //btnUpdate.Visible = false;
        btnCancel.Visible = false;

        // Reinitialize Select2
        ScriptManager.RegisterStartupScript(this, GetType(), "reinitSelect2",
            "$('.select2').select2();", true);
    }

    public string GetLocalIPAddress()
    {
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        }
        return "0.0.0.0";
    }

    private void fillGodown(string selectedMonth, string selectFinancial_year)
    {
        try
        {
            string query = @"
                SELECT Godown_ID, Godown_Name 
                FROM tbl_MetaData_GODOWN_2018 
                WHERE BranchID = @BranchId  
                AND Godown_ID NOT IN (
                    SELECT GodownID 
                    FROM MappingForMostureFumigation 
                    WHERE Month = @Month AND Financial_Year = @Financial_Year AND Type=@Type
                ) 
                ORDER BY Godown_Name ASC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Month", selectedMonth);
                cmd.Parameters.AddWithValue("@Financial_Year", selectFinancial_year);
                cmd.Parameters.AddWithValue("@Type", ddlType.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlGodown.Items.Clear();
                ddlGodown.Items.Add(new ListItem("--Select--", "0"));

                foreach (DataRow row in dt.Rows)
                {
                    ddlGodown.Items.Add(new ListItem(row["Godown_Name"].ToString(), row["Godown_ID"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error loading godowns: " + ex.Message + "');", true);
        }
    }

    private void BindEmployee(SqlConnection con)
    {
        try
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT Id, Name, MobileNo 
                FROM RegisterEmployeeMostureFumigation 
                WHERE BranchID = @BranchId AND DeletedDate IS NULL 
                ORDER BY Name";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            ddlEmployee.DataSource = dt;
            ddlEmployee.DataTextField = "Name";
            ddlEmployee.DataValueField = "Id";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error loading employees: " + ex.Message + "');", true);
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmployee.SelectedValue == "0")
        {
            txtMobile.Text = "";
            txtDesignation.Text = "";
            return;
        }

        try
        {
            using (SqlCommand cmd = new SqlCommand("SELECT MobileNo, Designation FROM RegisterEmployeeMostureFumigation WHERE Id = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", ddlEmployee.SelectedValue);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMobile.Text = dr["MobileNo"].ToString();
                    txtDesignation.Text = dr["Designation"].ToString();
                }
                dr.Close();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error: " + ex.Message + "');", true);
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedValue == "0" || ddlFinancial_Year.SelectedValue == "0")
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("--Select--", "0"));
            return;
        }

        try
        {
            if (ddlEmployee.SelectedValue != "0")
            {
                using (SqlCommand cmd = new SqlCommand("SELECT MobileNo, Designation FROM RegisterEmployeeMostureFumigation WHERE Id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", ddlEmployee.SelectedValue);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtMobile.Text = dr["MobileNo"].ToString();
                        txtDesignation.Text = dr["Designation"].ToString();
                    }
                    dr.Close();
                    con.Close();
                }
            }

            fillGodown(ddlMonth.SelectedValue, ddlFinancial_Year.SelectedValue);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                "alert('Error: " + ex.Message + "');", true);
        }
    }
    protected void ddlFinancial_Year_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFinancial_Year.SelectedValue != "0" && ddlMonth.SelectedValue != "0")
        {
            fillGodown(ddlMonth.SelectedValue, ddlFinancial_Year.SelectedValue);
        }
        else
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}