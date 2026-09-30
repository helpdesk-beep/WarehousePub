using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Delete_DMO_Markfed_Bills : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == "MPSWLC" || Session["Region_ID"] != null)
        {
            if (!IsPostBack)
            {
                FillDistrict();
                FillCommodity();
                BindEmptyBranch();
                BindEmptyGodown();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    void BindEmptyBranch()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Insert(0, new ListItem("All", "0"));
        ddlBranch.SelectedIndex = 0;
    }
    void BindEmptyGodown()
    {
        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        ddlGodown.SelectedIndex = 0;
    }
    void FillDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Where Region_ID='" + Session["Region_ID"].ToString() + "' Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlBranch.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
        }
        ddlBranch.Items.Insert(0, new ListItem("All", "0"));
        ddlBranch.SelectedIndex = 0;

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            BindEmptyBranch();
            BindEmptyGodown();
        }
        else
        {
            GetBranch();
            BindEmptyGodown();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue == "0")
        {
            BindEmptyGodown();
            return;
        }
        using (SqlConnection con = new SqlConnection(conStr))
        {
            //SqlDataAdapter da = new SqlDataAdapter("SELECT Godown_ID, Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchId=" + ddlBranch.SelectedValue, con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ddlGodown.DataSource = dt;
            //ddlGodown.DataTextField = "Godown_Name";
            //ddlGodown.DataValueField = "Godown_ID";
            //ddlGodown.DataBind();
            //ddlGodown.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Godown--", "0"));
            string qry = "";
            qry = "SELECT Godown_ID, Godown_Name FROM tbl_MetaData_GODOWN_2018 where BranchID='" + ddlBranch.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlGodown.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
            }
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
            ddlGodown.SelectedIndex = 0;
        }
    }
    void FillCommodity()
    {
        string qry = "";
        qry = "Select Distinct SDM.Commodity_Id,Sc.Commodity_Name from tbl_institution_storage_Bill_Details_DMO_MarkFed SDM Inner join tbl_MetaData_STORAGE_COMMODITY Sc On SDM.Commodity_Id=Sc.Commodity_Id order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommodity.DataSource = ds.Tables[0];
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        BindBillsGrid();
    }
    private void BindBillsGrid()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_DMO_Bill_For_Delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvBills.DataSource = dt;
                gvBills.DataBind();
            }
        }
    }
    protected void gvBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteBill")
        {
            try
            {
                string billNumber = e.CommandArgument.ToString();
                int commodityId = Convert.ToInt32(ddlCommodity.SelectedValue);
                string deletedBy = Session["Region_ID"].ToString();
                DeleteBillWithLog(billNumber, commodityId, deletedBy);
                BindBillsGrid();
                ShowSweetAlertSuccess("Bill deleted successfully.");
            }
            catch (Exception ex)
            {
                ShowSweetAlertError("Error", ex.Message);
            }
        }
    }
    private void DeleteBillWithLog(string billNumber, int commodityId, string deletedBy)
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteBillWithLog_DMO_MarkFed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Bill_Number", SqlDbType.VarChar, 50).Value = billNumber;
                cmd.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = commodityId;
                cmd.Parameters.Add("@DeletedBy", SqlDbType.NVarChar, 50).Value = deletedBy;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    private void ShowSweetAlertSuccess(string message)
    {
        message = message.Replace("'", "").Replace("\r", "").Replace("\n", "");
        string script = "Swal.fire('Success', '" + message + "', 'success');";
        ScriptManager.RegisterStartupScript(this, GetType(), "swalOk", script, true);
    }
    private void ShowSweetAlertError(string title, string message)
    {
        message = message.Replace("'", "").Replace("\r", "").Replace("\n", "");
        string script = "Swal.fire('" + title + "', '" + message + "', 'error');";
        ScriptManager.RegisterStartupScript(this, GetType(), "swalErr", script, true);
    }

}