using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;

public partial class StatePages_PrapatraWheet : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegion();
            //if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            //{
            //    fillgrid(Request.QueryString["ID"].ToString());
            //}
            //else
            //{
            //    fillgrid("0");
            //}
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }
    private void GetDist(string RegionID)
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + RegionID + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
    private void GetBranch(string DistID)
    {
        string strBranch = "";
        //strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        strBranch = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strBranch, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "Depotname";
            ddlbranch.DataValueField = "BranchID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlbranch.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillGrid();
        //GetRegion();
        GetDist(ddlregion.SelectedValue.ToString());
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(ddldistrict.SelectedValue.ToString());
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Sp_Wheet_Stored_JVS_Godown", con);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Branchid", ddlbranch.SelectedValue);
        cmdd.Parameters.AddWithValue("@Depositerid", ddldepositer.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCol3.DataSource = dt;
            gvCol3.DataBind();
        }
        else
        {
            gvCol3.DataSource = null;
            gvCol3.DataBind();
        }
    }

    protected void BtnForCol2To6_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCol3.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hdngodownid = row.FindControl("hdngodownid") as HiddenField;
                TextBox txtwheat1 = row.FindControl("txtSacks_201819") as TextBox;

                TextBox txtSacks_201819 = row.FindControl("txtSacks_201819") as TextBox;
                TextBox txtBags_201819 = row.FindControl("txtBags_201819") as TextBox;
                TextBox txtQuantity_201819 = row.FindControl("txtQuantity_201819") as TextBox;

                TextBox txtSacks_201920 = row.FindControl("txtSacks_201920") as TextBox;
                TextBox txtBags_201920 = row.FindControl("txtBags_201920") as TextBox;
                TextBox txtQuantity_201920 = row.FindControl("txtQuantity_201920") as TextBox;

                TextBox txtSacks_202021 = row.FindControl("txtSacks_202021") as TextBox;
                TextBox txtBags_202021 = row.FindControl("txtBags_202021") as TextBox;
                TextBox txtQuantity_202021 = row.FindControl("txtQuantity_202021") as TextBox;

                TextBox txttotalbalance = row.FindControl("txttotalbalance") as TextBox;


                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Wheet_Stored_JVS_Godown_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
                    cmd.Parameters.AddWithValue("@DepositorName", ddldepositer.SelectedValue);
                    cmd.Parameters.AddWithValue("@Number_Of_Sacks_201819", txtSacks_201819.Text);
                    cmd.Parameters.AddWithValue("@Number_of_Bags_201819", txtBags_201819.Text);
                    cmd.Parameters.AddWithValue("@Stored_Quantity_201819", txtQuantity_201819.Text);
                    cmd.Parameters.AddWithValue("@Number_Of_Sacks_201920", txtSacks_201920.Text);
                    cmd.Parameters.AddWithValue("@Number_of_Bags_201920", txtBags_201920.Text);
                    cmd.Parameters.AddWithValue("@Stored_Quantity_201920", txtQuantity_201920.Text);
                    cmd.Parameters.AddWithValue("@Number_Of_Sacks_202021", txtSacks_202021.Text);
                    cmd.Parameters.AddWithValue("@Number_of_Bags_202021", txtBags_202021.Text);
                    cmd.Parameters.AddWithValue("@Stored_Quantity_202021", txtQuantity_202021.Text);
                    cmd.Parameters.AddWithValue("@Total_Balance", txttotalbalance.Text);
                    cmd.Parameters.AddWithValue("@Create_By", ipAddress);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Record Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    protected void ddldepositer_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}