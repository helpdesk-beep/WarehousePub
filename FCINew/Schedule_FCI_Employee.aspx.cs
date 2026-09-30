using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FCI_Schedule_FCI_Employee : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
            fillInpOff_Grid();
        }
    }

    public void fillInpOff_Grid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_FCI_Employee_Details_Region_Wise_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            Gridview_IsnpOff.DataSource = dt;
                            Gridview_IsnpOff.DataBind();
                            lblOfficerList.Text = dt.Rows.Count.ToString();
                        }
                        else
                        {
                            Gridview_IsnpOff.DataSource = null;
                            Gridview_IsnpOff.DataBind();
                            lblOfficerList.Text = "0";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    protected void Gridview_IsnpOff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            HiddenField hdnpfid = (HiddenField)e.Row.FindControl("hdnpfid");
            HiddenField hdnMobile = (HiddenField)e.Row.FindControl("hdnMobile");
            HiddenField hdnName = (HiddenField)e.Row.FindControl("hdnName");
            HiddenField hdnDesignation = (HiddenField)e.Row.FindControl("hdnDesignation");
            HiddenField hdnDistrict = (HiddenField)e.Row.FindControl("hdnDistrict");
            HiddenField hdnBranch = (HiddenField)e.Row.FindControl("hdnBranch");

            if (hdnpfid != null)
                hdnpfid.Value = drv["FCI_ID"].ToString();
            if (hdnMobile != null)
                hdnMobile.Value = drv["Mobile_No"].ToString();
            if (hdnName != null)
                hdnName.Value = drv["Emp_Name"].ToString();
            if (hdnDesignation != null)
                hdnDesignation.Value = drv["Designation"].ToString();

            if (hdnDistrict != null && drv.DataView.Table.Columns.Contains("District_ID"))
                hdnDistrict.Value = drv["District_ID"].ToString();

            if (hdnBranch != null && drv.DataView.Table.Columns.Contains("Branch_ID"))
                hdnBranch.Value = drv["Branch_ID"].ToString();
        }
    }

    protected void Gridview_IsnpOff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ScheduleInspection")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

            HiddenField hdnpfid = (HiddenField)row.FindControl("hdnpfid");
            HiddenField hdnMobile = (HiddenField)row.FindControl("hdnMobile");
            HiddenField hdnName = (HiddenField)row.FindControl("hdnName");
            HiddenField hdnDesignation = (HiddenField)row.FindControl("hdnDesignation");
            HiddenField hdnDistrict = (HiddenField)row.FindControl("hdnDistrict");
            HiddenField hdnBranch = (HiddenField)row.FindControl("hdnBranch");

            if (hdnpfid != null)
                Session["S_PFID"] = hdnMobile.Value;
            if (hdnName != null)
                txtInspOffName.Text = hdnName.Value;
            if (hdnDesignation != null)
                txtDesig.Text = hdnDesignation.Value;
            if (hdnMobile != null)
                txtCug.Text = hdnMobile.Value;

            LoadDistricts();

            // Set District properly
            if (hdnDistrict != null && !string.IsNullOrEmpty(hdnDistrict.Value))
            {
                if (ddl_dist.Items.FindByValue(hdnDistrict.Value) != null)
                {
                    ddl_dist.SelectedValue = hdnDistrict.Value;
                    LoadBranches(hdnDistrict.Value);
                }
            }

            // Set Branch properly after districts are loaded
            if (hdnBranch != null && !string.IsNullOrEmpty(hdnBranch.Value))
            {
                if (ddl_branch.Items.FindByValue(hdnBranch.Value) != null)
                {
                    ddl_branch.SelectedValue = hdnBranch.Value;
                }
            }

            LoadPreviousInspections();
            ResetFormFields();
            ShowModal();
        }
    }

    private void LoadDistricts()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT District_Name, District_Id FROM tbl_MetaData_DISTRICT ORDER BY District_Name";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    dt = new DataTable();
                    da.Fill(dt);

                    ddl_dist.DataSource = dt;
                    ddl_dist.DataTextField = "District_Name";
                    ddl_dist.DataValueField = "District_Id";
                    ddl_dist.DataBind();
                    ddl_dist.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    private void LoadBranches(string districtId)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Depotname, BranchID FROM tbl_MetaData_Depot WHERE DistrictID=@DistrictID ORDER BY Depotname";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@DistrictID", districtId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        ddl_branch.DataSource = dt;
                        ddl_branch.DataTextField = "Depotname";
                        ddl_branch.DataValueField = "BranchID";
                        ddl_branch.DataBind();
                        ddl_branch.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    private void LoadPreviousInspections()
    {
        try
        {
            if (Session["S_PFID"] == null) return;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_FCI_Employee_Details_For_Inspection_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employee_ID", Session["S_PFID"].ToString());

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        sda.Fill(dt);

                        Gridview_OfficerPreviousInsp.DataSource = dt;
                        Gridview_OfficerPreviousInsp.DataBind();
                        lblTotalInsp.Text = dt.Rows.Count.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    protected void Gridview_OfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // This ensures the grid is readonly - no editing events
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Just to ensure any edit buttons are properly configured
            Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            if (btnEdit != null)
            {
                btnEdit.CommandName = "EditInspection";
            }
        }
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(txtInspOffName.Text))
        {
            ShowAlert("Officer Name Cannot be Blank...");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtCug.Text))
        {
            ShowAlert("Officer CUG Mobile No. Cannot be Blank...");
            return false;
        }
        if (ddl_dist.SelectedValue == "0")
        {
            ShowAlert("Please Select District...");
            return false;
        }
        if (ddl_branch.SelectedValue == "0")
        {
            ShowAlert("Please Select Branch...");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txt_InspDate.Text))
        {
            ShowAlert("Please Enter Inspection Date...");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtManagerNM.Text))
        {
            ShowAlert("Please Enter Manager Name...");
            return false;
        }
        if (!string.IsNullOrEmpty(txtcugno.Text) && txtcugno.Text.Length != 10)
        {
            ShowAlert("Enter valid 10 digit mobile...");
            return false;
        }
        if (ddlquater.SelectedValue == "0")
        {
            ShowAlert("Please Select Inspection Type...");
            return false;
        }
        if (ddlmonth.SelectedValue == "0")
        {
            ShowAlert("Please Select Month...");
            return false;
        }
        if (ddlverification.SelectedValue == "0")
        {
            ShowAlert("Please Select Verification Type...");
            return false;
        }

        return true;
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            ShowModal();
            return;
        }

        string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ipAddress))
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Inspection_Allot_Branch_For_Officer_Insert_FCI_Employee_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Employee_ID", Session["S_PFID"]);
                    cmd.Parameters.AddWithValue("@District_ID", ddl_dist.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_ID", ddl_branch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
                    cmd.Parameters.AddWithValue("@Inspection_month_ID", ddlmonth.SelectedValue);
                    cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
                    cmd.Parameters.AddWithValue("@Order_No", string.IsNullOrEmpty(txt_OrderNo.Text) ? "" : txt_OrderNo.Text);
                    cmd.Parameters.AddWithValue("@Order_Date", ConvertToDate(txt_InspDate.Text));
                    cmd.Parameters.AddWithValue("@Branch_Manager_Name", txtManagerNM.Text);
                    cmd.Parameters.AddWithValue("@Manager_CUG_No", txtcugno.Text);
                    cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);

                    if (btn_saveInspDate.Text == "Update" && Session["S_InspID"] != null)
                    {
                        cmd.Parameters.AddWithValue("@ID", Session["S_InspID"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ID", DBNull.Value);
                    }

                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    string result = cmd.Parameters["@TheResult"].Value.ToString();

                    if (result.StartsWith("SUCCESS"))
                    {
                        ShowAlert(btn_saveInspDate.Text == "Update" ? "Updated Successfully" : "Saved Successfully");
                        LoadPreviousInspections();
                        ResetFormFields();
                    }
                    else
                    {
                        ShowAlert(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }

        ShowModal();
    }

    private DateTime ConvertToDate(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
            return new DateTime(1919, 1, 1);

        string[] formats = { "yyyy-MM-dd", "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "M/d/yyyy" };
        DateTime result;
        if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            return result;
        }
        return new DateTime(1919, 1, 1);
    }

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_dist.SelectedValue != "0")
        {
            LoadBranches(ddl_dist.SelectedValue);
        }
        ShowModal();
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_branch.SelectedValue != "0")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT NodalOfficeName, NodalOfficerphone FROM tbl_MetaData_DEPOT WHERE BranchId=@BranchId";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BranchId", ddl_branch.SelectedValue);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {

                                txtManagerNM.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["NodalOfficeName"].ToString()) ? dr["NodalOfficeName"].ToString() : "");
                                txtcugno.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dr["NodalOfficerphone"].ToString()) ? dr["NodalOfficerphone"].ToString() : "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(ex.Message);
            }
        }
        ShowModal();
    }

    protected void ddlquater_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMonthsByQuarter();
        ShowModal();
    }

    private void LoadMonthsByQuarter()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Month_Quater_Wise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Quarter_ID", ddlquater.SelectedValue);
                    con.Open();
                    ddlmonth.DataSource = cmd.ExecuteReader();
                    ddlmonth.DataTextField = "Month_Name";
                    ddlmonth.DataValueField = "ID";
                    ddlmonth.DataBind();
                    ddlmonth.Items.Insert(0, new ListItem("-- Select Month --", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    private void LoadInspectionDataForEdit(string id, string empId)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Employee_Data_For_Edit_For_FCIEmployee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Employee_ID", empId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];

                            btn_saveInspDate.Text = "Update";

                            if (row["Inspection_type_ID"] != DBNull.Value)
                                ddlquater.SelectedValue = row["Inspection_type_ID"].ToString();

                            LoadMonthsByQuarter();

                            if (row["Inspection_month_ID"] != DBNull.Value)
                                ddlmonth.SelectedValue = row["Inspection_month_ID"].ToString();

                            if (row["Verification_Type"] != DBNull.Value)
                                ddlverification.SelectedValue = row["Verification_Type"].ToString();

                            if (row["District_ID"] != DBNull.Value)
                            {
                                ddl_dist.SelectedValue = row["District_ID"].ToString();
                                LoadBranches(ddl_dist.SelectedValue);
                            }

                            if (row["Branch_ID"] != DBNull.Value)
                                ddl_branch.SelectedValue = row["Branch_ID"].ToString();

                            if (row["Order_Date"] != DBNull.Value)
                            {
                                DateTime orderDate = Convert.ToDateTime(row["Order_Date"]);
                                txt_InspDate.Text = orderDate.ToString("yyyy-MM-dd");
                            }

                            txtManagerNM.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(row["Branch_Manager_Name"].ToString()) ? row["Branch_Manager_Name"].ToString() : "");
                            txtcugno.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(row["Manager_CUG_No"].ToString()) ? row["Manager_CUG_No"].ToString() : "");
                            txt_OrderNo.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(row["Order_No"].ToString()) ? row["Order_No"].ToString() : "");
                            //Session["S_InspID"] = id;
                            int validId;
                            if (int.TryParse(Request.QueryString["id"], out validId))
                            {
                                Session["S_InspID"] = validId;
                            }
                            else
                            {
                                // Handle invalid input
                            }
                            LoadPreviousInspections();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    protected void Gridview_OfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditInspection")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Gridview_OfficerPreviousInsp.Rows[rowIndex];

            HiddenField hdnId = (HiddenField)row.FindControl("hdnId");
            HiddenField hdnEmpID = (HiddenField)row.FindControl("hdnEmpID");

            if (hdnId != null && hdnEmpID != null)
            {
                LoadInspectionDataForEdit(hdnId.Value, hdnEmpID.Value);
            }

            ShowModal();
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        ResetFormFields();
        ShowModal();
    }

    private void ResetFormFields()
    {
        txt_InspDate.Text = "";
        txt_OrderNo.Text = "";
        txtcugno.Text = "";
        txtManagerNM.Text = "";

        if (ddl_branch.Items.Count > 0) ddl_branch.SelectedIndex = 0;
        if (ddl_dist.Items.Count > 0) ddl_dist.SelectedIndex = 0;
        if (ddlquater.Items.Count > 0) ddlquater.SelectedIndex = 0;
        if (ddlmonth.Items.Count > 0) ddlmonth.SelectedIndex = 0;
        if (ddlverification.Items.Count > 0) ddlverification.SelectedIndex = 0;

        btn_saveInspDate.Text = "Submit";
        Session["S_InspID"] = null;
    }

    private void ShowAlert(string message)
    {
        string safeMsg = message.Replace("'", "\\'").Replace("\n", " ").Replace("\r", " ");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + safeMsg + "');", true);
    }

    private void ShowModal()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal",
            "setTimeout(function() { if(typeof showModal === 'function') showModal(); }, 100);", true);
    }
}