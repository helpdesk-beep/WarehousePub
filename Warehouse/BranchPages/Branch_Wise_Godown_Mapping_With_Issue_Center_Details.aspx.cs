using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class BranchPages_Branch_Wise_Godown_Mapping_With_Issue_Center_Details : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillIssuecenter();
            fillGodown();

            BindGrid();
        }
    }
    private void fillIssuecenter()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select IssueCenterId,IssueCenterName from MetaDataBranchWithIssueCenter where Branchid ='" + Session["BranchId"].ToString() + "' Order By IssueCenterName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlIssueCenter.DataSource = ds.Tables[0];
                ddlIssueCenter.DataTextField = "IssueCenterName";
                ddlIssueCenter.DataValueField = "IssueCenterId";
                ddlIssueCenter.DataBind();
                ddlIssueCenter.Items.Insert(0, "Select");
            }
            else
            {
                ddlIssueCenter.Items.Clear();
                ddlIssueCenter.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            //////
        }
    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("USP_Insert_Branch_Wise_Godown_Mapping_With_Issuecenter", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IssueCenterID", ddlIssueCenter.SelectedValue);
            cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
            cmd.Parameters.AddWithValue("@Latitude", Convert.ToDecimal(txtLatitude.Text));
            cmd.Parameters.AddWithValue("@Longitude", Convert.ToDecimal(txtLongitude.Text));
            cmd.Parameters.AddWithValue("@CreatedBy", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_IP", ip);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + dt.Rows[0]["Msg"].ToString() + "');", true);
                if (dt.Rows[0]["Status"].ToString() == "1")
                {
                    ClearControl();
                    BindGrid();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('" + ex.Message.Replace("'", "") + "');", true);
        }
    }
    private void BindGrid()
    {
        SqlCommand cmd = new SqlCommand("USP_Get_Branch_Wise_Godown_Mapping_With_Issuecenter", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gvMapping.DataSource = dt;
        gvMapping.DataBind();
    }
    private void ClearControl()
    {
        ddlIssueCenter.ClearSelection();
        ddlGodown.ClearSelection();
        txtLatitude.Text = "";
        txtLongitude.Text = "";
    }
}