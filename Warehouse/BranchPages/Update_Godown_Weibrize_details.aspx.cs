using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class BranchPages_Update_Godown_Weibrize_details : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;

    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                fillGodown();
                fillDistrict();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select A.Godown_ID,A.Godown_Name from tbl_MetaData_GODOWN_2018 A LEFT JOIN Godown_WeightBridge_Details B ON A.Godown_ID=B.Godown_Id Where A.BranchID ='" + Session["BranchId"].ToString() + "' AND B.Godown_Id is null Order By Godown_Name ASC";
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
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillDistrict()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Id,District_Name from tbl_MetaData_DISTRICT  Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                //ddlDistrict.Items.Insert('0', "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    private void fillBranch()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select BranchId,DepotName from tbl_MetaData_DEPOT Where DistrictId='" + ddlDistrict.SelectedValue + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Usp_Get_Godown_WeightBridge_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", Session["BranchId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                Grdweight.DataSource = dt;
                Grdweight.DataBind();
                grddetails.Visible = true;
            }
            else
            {
                Grdweight.DataSource = null;
                Grdweight.DataBind();
            }
        }
    }

    protected void ddlWeighbridge_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWeighbridge.SelectedValue == "1")
        {
            //divWeight.Visible = true;
            fsNoWB.Visible = false;
            btnsubmit.Visible = true;
            //rfvWeight.Enabled = true;
            rfvDistrict.Enabled = false;
            rfvbranch.Enabled = false;
            rfvAddress.Enabled = false;
            rfvDistance.Enabled = false;
        }
        else if (ddlWeighbridge.SelectedValue == "2")
        {
            // divWeight.Visible = false;
            fsNoWB.Visible = true;
            btnsubmit.Visible = true;
            //rfvWeight.Enabled = false;
            rfvDistrict.Enabled = true;
            rfvbranch.Enabled = true;
            rfvAddress.Enabled = true;
            rfvDistance.Enabled = true;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Usp_Godown_WeightBridge_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Email_Id", txtEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@Mobile_No", txtMobileNo.Text.Trim());
        cmd.Parameters.AddWithValue("@Weighbridge_Available", ddlWeighbridge.SelectedValue.ToString());
        cmd.Parameters.Add("@WeightBridge_Weight_MT", SqlDbType.Decimal).Value = GetDecimalOrNull(txtWeightbridgeWeight.Text);
        cmd.Parameters.Add("@District_Id", SqlDbType.Int).Value = GetIntOrNull(ddlDistrict.SelectedValue);
        cmd.Parameters.Add("@Branch_Id", SqlDbType.Int).Value = GetIntOrNull(ddlBranch.SelectedValue);
        SqlParameter pAddress = new SqlParameter("@WeightBridge_Address", SqlDbType.NVarChar);
        if (string.IsNullOrWhiteSpace(txtWBAddress.Text))
        {
            pAddress.Value = DBNull.Value;
        }
        else
        {
            pAddress.Value = txtWBAddress.Text.Trim();
        }
        cmd.Parameters.Add(pAddress);
        cmd.Parameters.Add("@Distance_From_Warehouse_KM", SqlDbType.Decimal).Value = GetDecimalOrNull(txtDistanceKM.Text);
        cmd.Parameters.AddWithValue("@Created_By", Session["BranchId"].ToString());
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Record Saved Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            FillGrid();
            ddlGodown.ClearSelection();
            ddlWeighbridge.ClearSelection();
            ddlDistrict.ClearSelection();
            ddlBranch.ClearSelection();
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtWBAddress.Text = "";
            txtWeightbridgeWeight.Text = "";
            txtDistanceKM.Text = "";
            fsNoWB.Visible = false;
            //divWeight.Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
        }
    }
    object GetDecimalOrNull(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        return Convert.ToDecimal(value);
    }
    object GetIntOrNull(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        return Convert.ToInt32(value);
    }
    protected void Grdweight_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            string godownId = Grdweight.DataKeys[rowIndex].Value.ToString();

            DeleteGodownWeighbridge(godownId);

            FillGrid();   // grid refresh

            // Show success message
            lblMessage.Text = "Record deleted successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Visible = true;
        }
    }
    private void DeleteGodownWeighbridge(string godownId)
    {
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Godown_WeightBridge_Details WHERE Godown_Id = @Godown_Id", con))
            {
                cmd.Parameters.Add("@Godown_Id", SqlDbType.VarChar).Value = godownId;

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
