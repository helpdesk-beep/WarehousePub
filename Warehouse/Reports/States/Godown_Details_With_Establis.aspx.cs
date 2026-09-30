using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;

public partial class StatePages_Godown_Details_With_Establis : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filDivision();
            //fillGodown();
            fillCropYear();
            GetCommodityType();
            GetDepositorType();
            fillGodownType();
        }
    }
    private void filDivision()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT Order By Regionnm ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.Items.Clear();
                ddldivision.DataSource = ds.Tables[0];
                ddldivision.DataTextField = "Regionnm";
                ddldivision.DataValueField = "Region_ID";
                ddldivision.DataBind();
                ddldivision.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        filDistrict();
    }
    private void filDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Id,District_Name from tbl_MetaData_DISTRICT Where Region_ID ='" + ddldivision.SelectedValue + "' Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        filBranch();
    }
    private void filBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select DepotID,DepotName from tbl_MetaData_DEPOT Where DistrictId ='" + ddldistrict.SelectedValue + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "DepotID";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodowntype.Items.Clear();
                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                ddlgodowntype.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where DepotId ='" + ddlbranch.SelectedValue + "' And Hired_Type ='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.Items.Clear();
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select distinct CropYear from tbl_storage_Depositor_WHR_Relation where CropYear not in('All','Before 2009','Before 2013','Before 2014','','--Select--','0')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "CropYear";
                ddlcropyear.DataValueField = "CropYear";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    public void GetCommodityType()
    {
        string qry = "";
        qry = "Select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group Order By Group_name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommoditytype.DataSource = ds.Tables[0];
            ddlCommoditytype.DataTextField = "Group_name";
            ddlCommoditytype.DataValueField = "Comm_Group_id";
            ddlCommoditytype.DataBind();
            ddlCommoditytype.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Where  Rep_Grp_Code='" + ddlCommoditytype.SelectedValue + "' order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    private void GetDepositorType()
    {
        string qry = "";
        qry = "select Depositor_Type_Id,Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositorType.DataSource = ds.Tables[0];
            ddlDepositorType.DataTextField = "Depositor_Type";
            ddlDepositorType.DataValueField = "Depositor_Type_Id";
            ddlDepositorType.DataBind();
            ddlDepositorType.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    private void GetDepositor()
    {
        //string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
        //string qry = "";
        //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "'";
        //SqlCommand cmd = new SqlCommand(qry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlDepositor.DataSource = ds.Tables[0];
        //    ddlDepositor.DataTextField = "Depositor_Name";
        //    ddlDepositor.DataValueField = "Depositor_ID";
        //    ddlDepositor.DataBind();
        //    ddlDepositor.Items.Insert(0, new ListItem("All", "0"));
        //}
        //else
        //{

        // }
        if (ddlDepositorType.SelectedItem.Text == "Institution")
        {
            //For Institution
            string query2 = "";
            //if (District_Id == "2333" || District_Id == "2309" || District_Id == "2311" || District_Id == "2327")
            //{
            //    query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','4679','181')";
            //}
            //else
            //{
            //query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535')";
            //query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679')";
            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181')";

            //}
            SqlCommand cmd2 = new SqlCommand(query2, con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds2;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, "--Select--");
                //ddlDepositor.SelectedValue = "129";
            }
            //For Institution

        }
        else
        {
            string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
            string qry = "";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE DepositorType_ID ='" + ddlDepositorType.SelectedValue + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                ddlDepositor.Items.Insert(0, new ListItem("All", "0"));
            }
        }
    }

    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("USP_Godoen_Details_Estd", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            // txtDivision.Text = dt.Rows[0]["Regionnm"].ToString();
                            // txtdistrict.Text = dt.Rows[0]["District_Name"].ToString();
                            txtgodownId.Text = dt.Rows[0]["Godown_ID"].ToString();
                            txtestd.Text = dt.Rows[0]["Establised"].ToString();
                            txtOpration.Text = dt.Rows[0]["Operation"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void fillDepositorgrid()
    {
        string DepositorType = ddlDepositorType.SelectedItem.Text.ToString();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        if (DepositorType == "Cultivator" || DepositorType == "Trader(Business)" || DepositorType == "Others")
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DepositorDetails", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepositorType", ddlDepositorType.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@BranchId", ddlbranch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                grddetails.Visible = true;
                                GrdDipositor.DataSource = dt;
                                GrdDipositor.DataBind();
                            }
                            else
                            {
                                GrdDipositor.DataSource = null;
                                GrdDipositor.DataBind();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            grddetails.Visible = false;
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {

        fillgrid();
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDepositorgrid();
    }
    protected void ddlCommoditytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    protected void ddlWHRStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWHRStatus.SelectedValue == "1")
        {
            lblwhr.Visible = true;
            ddlWHRIssued.Visible = true;
        }
        else
        {
            lblwhr.Visible = false;
            ddlWHRIssued.Visible = false;
        }
    }
    protected void fillWhrgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GodownStockWHRDetails", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
                cmd.Parameters.AddWithValue("@DepositorName", ddlDepositor.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue);
                cmd.Parameters.AddWithValue("@DepotId", ddlbranch.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdwhr.DataSource = dt;
                            grdwhr.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdwhr.DataSource = null;
                            grdwhr.DataBind();
                        }
                    }
                }
            }
        }
    }


    protected void ddlWHRIssued_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWHRIssued.SelectedValue == "1")
        {
            fillWhrgrid();
        }
        else
        {
            //lblwhr.Visible = false;
            //ddlWHRIssued.Visible = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        //string con = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand("usp_InsertGodownOfflineStackDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@RegionId", ddldivision.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DepotID", ddlbranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@HiredType", ddlgodowntype.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@GodownName", ddlGodown.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@GodownFormationDate", txtestd.Text.ToString());
        cmd.Parameters.AddWithValue("@GodownOperationDate", txtOpration.Text.ToString());
        cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@DepositorType", ddlDepositorType.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@CommGroupid", ddlCommoditytype.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CommodityId", ddlcommodity.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@WHRStatus", ddlWHRStatus.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@WHRIssueStatus", ddlWHRIssued.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@OfflineStackID", txtstack.Text.ToString());
        cmd.Parameters.AddWithValue("@OfflineDepositor_WHR_Id]", txtWHR.Text.ToString());
        cmd.Parameters.AddWithValue("@Offline_TotalBagsAvailable]", txtBags.Text.ToString());
        cmd.Parameters.AddWithValue("@Offline_TotalQtyAvailable]", txtweight.Text.ToString());
        cmd.Parameters.AddWithValue("@IsWDRACompliant", ddlwdra.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text.ToString());
        cmd.Parameters.AddWithValue("@CreatedBy", GetLocalIPAddress());
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Record Saved Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
        }
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        //string con = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlCommand cmd = new SqlCommand("usp_InsertGodownStackDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@RegionId", ddldivision.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@DepotID", ddlbranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@HiredType", ddlgodowntype.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@GodownName", ddlGodown.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@GodownFormationDate", txtestd.Text.ToString());
        cmd.Parameters.AddWithValue("@GodownOperationDate", txtOpration.Text.ToString());
        cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedItem.Text);
        cmd.Parameters.AddWithValue("@DepositorType", ddlDepositorType.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.ToString());
        cmd.Parameters.AddWithValue("@CommGroupid", ddlCommoditytype.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CommodityId", ddlcommodity.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@WHRStatus", ddlWHRStatus.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@WHRIssueStatus", ddlWHRIssued.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@TotalNoOfBagsAvailable", txtTotalAvailableBags.Text.ToString());
        cmd.Parameters.AddWithValue("@TotalQuantityAvailable", txtTotalAvailableWeight.Text.ToString());
        cmd.Parameters.AddWithValue("@IsWDRACompliant", ddlwdra.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Remarks", txtremarks.Text.ToString());
        cmd.Parameters.AddWithValue("@CreatedBy", GetLocalIPAddress());
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Record Saved Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
        }
    }
}