using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class StatePages_CreatePvtGodownLogin : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDistrict();
                    GetStorageType();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('An error occurred during load.');", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception) { }
    }

    private void Getgodowns()
    {
        string str = "select Godown_Name,Godown_id from tbl_MetaData_GODOWN_2018 where BranchId='" + ddlDepotList.SelectedValue + "' and DistrictId = '" + ddlDistrict.SelectedValue + "' and Godown_ID not in (select distinct Godown_ID from Pvt_Warehouse_Login) order by Godown_Name ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, "--Select--");
            trddlgd.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SeachGodown();
    }

    private void SeachGodown()
    {
        try
        {
            string qry = "select PW.Login_Id,PW.Godown_Name,PW.Godown_Id,PW.Access_Restrict,SA.Storage_Agency,MD.DepotName as IssueCenter,MD.BranchId as IssueCenterId,PW.Is_W20_RegID as W_Reg_No,PW.Is_W20_Agreement as Is_Agreement from Pvt_Warehouse_Login as PW inner join Storage_Agency_type as SA on SA.Storage_Agency_ID=PW.GodownTypeId inner join tbl_MetaData_DEPOT as MD on MD.BranchId=PW.BranchID where PW.Godown_Id='" + txtSearch.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            fillGrid(ds);
        }
        catch (Exception) { }
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + distId + "' order by DepotName asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlDepotList.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
            }
            ddlDepotList.Items.Insert(0, "---Select---");
        }
        catch (Exception) { }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
            getDepot(ddlDistrict.SelectedValue);
        else
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "msg", "alert('Please Select District')", true);
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepotList.SelectedIndex != 0)
        {
            Getgodowns();
            GetGodown();
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblGId.Text = ddlGodown.SelectedValue;
        trgodown.Visible = false;
        trICC.Visible = false;
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string CIP = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            con.Open();
            if (btnupdate.Text == "Save")
            {
                string LoginId = GetMax();
                qry = "INSERT INTO [Pvt_Warehouse_Login]([Login_Id],[Godown_Name],[Password],[Godown_Id],[DistrictId],[BranchID],[DepotId],[Scope],[MasterPassword],[Access_Restrict],[GodownTypeId],[Active],[GM_Pwd],Is_W20_Agreement,Is_W20_RegID) VALUES ('" + LoginId + "','" + ddlGodown.SelectedItem.Text + "','wlc2015','" + ddlGodown.SelectedValue + "','" + ddlDistrict.SelectedValue + "','" + ddlDepotList.SelectedValue + "','" + ddlDepotList.SelectedValue + "','1','whrNic16','N','" + ddlGodownType.SelectedValue + "','Y','wlc2015','" + ddlAgree.SelectedValue + "','" + txtRegNo.Text + "')";
            }
            else
            {
                string log_qry = "insert into Pvt_Warehouse_Login_Log SELECT * FROM [Pvt_Warehouse_Login] where Login_Id='" + lblGId.Text + "'";
                new SqlCommand(log_qry, con).ExecuteNonQuery();

                qry = "update Pvt_Warehouse_Login set GodownTypeId='" + ddlGodownType.SelectedValue + "', Godown_Name='" + txtGodownName.Text + "', DepotId='" + txtIssueCCode.Text + "', Is_W20_Agreement='" + ddlAgree.SelectedValue + "', [Is_W20_RegID]='" + txtRegNo.Text + "', UpdatedDate=GETDATE(), UpdateBy='" + CIP + "' where Login_Id='" + lblGId.Text + "'";
            }

            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "msg", "alert('Success!');", true);
            GetGodown();
            Getgodowns();
        }
        catch (Exception ex) { }
        finally { con.Close(); }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    private void GetStorageType()
    {
        SqlDataAdapter da = new SqlDataAdapter("select Storage_Agency,Storage_Agency_ID from Storage_Agency_type", con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ddlGodownType.DataSource = ds;
        ddlGodownType.DataTextField = "Storage_Agency";
        ddlGodownType.DataValueField = "Storage_Agency_ID";
        ddlGodownType.DataBind();
        ddlGodownType.Items.Insert(0, "--Select--");
    }

    private string GetMax()
    {
        cmd = new SqlCommand("select ISNULL(MAX(Login_Id),0)+1 from Pvt_Warehouse_Login", con);
        //con.Open();
        string val = cmd.ExecuteScalar().ToString();
        //con.Close();
        return val;
    }

    private void GetGodown()
    {
        string qry = "select PW.Login_Id,PW.Godown_Name,PW.Godown_Id,PW.Access_Restrict,SA.Storage_Agency,MD.DepotName as IssueCenter,MD.BranchId as IssueCenterId,PW.Is_W20_RegID as W_Reg_No,PW.Is_W20_Agreement as Is_Agreement from Pvt_Warehouse_Login as PW inner join Storage_Agency_type as SA on SA.Storage_Agency_ID=PW.GodownTypeId inner join tbl_MetaData_DEPOT as MD on MD.BranchId=PW.BranchID where PW.BranchID='" + ddlDepotList.SelectedValue + "' and PW.DistrictId='" + ddlDistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        fillGrid(ds);
    }

    private void fillGrid(DataSet ds)
    {
        godown_GridView.DataSource = ds;
        godown_GridView.DataBind();
    }

    protected void godown_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string gid = godown_GridView.DataKeys[e.RowIndex].Value.ToString();
        try
        {
            con.Open();
            string log = "INSERT INTO Pvt_Warehouse_Login_Log SELECT * FROM Pvt_Warehouse_Login WHERE Login_Id='" + gid + "'";
            new SqlCommand(log, con).ExecuteNonQuery();
            new SqlCommand("DELETE FROM Pvt_Warehouse_Login WHERE Login_Id='" + gid + "'", con).ExecuteNonQuery();
            GetGodown();
        }
        catch { }
        finally { con.Close(); }
    }

    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnupdate.Text = "Update";
        trddlgd.Visible = false;
        trgodown.Visible = true;
        trICC.Visible = true;
        string gid = godown_GridView.SelectedDataKey.Value.ToString();

        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [Pvt_Warehouse_Login] where Login_Id='" + gid + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            txtGodownName.Text = dt.Rows[0]["Godown_Name"].ToString();
            lblGId.Text = dt.Rows[0]["Login_Id"].ToString();
            ddlGodownType.SelectedValue = dt.Rows[0]["GodownTypeId"].ToString().Trim();
            ddlAgree.SelectedValue = dt.Rows[0]["Is_W20_Agreement"].ToString() != "" ? dt.Rows[0]["Is_W20_Agreement"].ToString().Trim() : "-1";
            txtIssueCCode.Text = dt.Rows[0]["DepotId"].ToString();
            txtRegNo.Text = dt.Rows[0]["Is_W20_RegID"].ToString();
        }
    }
}