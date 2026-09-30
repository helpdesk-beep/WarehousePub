using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Drawing;

public partial class Region_Rpt_Update_Godown_Wise_Loss_Gain_By_RM : System.Web.UI.Page
{
    SqlTransaction sqltrans;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
                fillCropYear();
                fillCommodity();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT]  where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
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
    private void fillBranch()
    {
        try
        {
            string query = "";

            query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue + "' order by DepotName asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "--Select--");
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

            query = "select distinct Crop_Year from tbl_Institution_Storage_Bill_Details where Crop_Year not in('0','','--Select--','All')";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "Crop_Year";
                ddlcropyear.DataValueField = "Crop_Year";
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

    private void fillCommodity()
    {
        try
        {
            string query = "";

            query = "select Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY Order by Commodity_Name";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity.Items.Clear();
                ddlCommodity.DataSource = ds.Tables[0];
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
                ddlCommodity.Items.Insert(0, "--Select--");
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    public void FillGridwhr()
    {

        gv_whr.DataSource = null;
        gv_whr.DataBind();

        SqlCommand cmd = new SqlCommand("Get_RM_Gain_Deduction_Godown_Wise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CommodityID", ddlCommodity.SelectedValue.ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv_whr.DataSource = ds;
            gv_whr.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            gv_whr.DataSource = null;
            gv_whr.DataBind();
        }
    }
    protected void gv_whr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        Label lblGodown_Name = gv_whr.Rows[e.RowIndex].FindControl("lblGodown_Name") as Label;
        Label lblGodown_ID = gv_whr.Rows[e.RowIndex].FindControl("lblGodown_ID") as Label;
        TextBox txtAmount = gv_whr.Rows[e.RowIndex].FindControl("txtAmount") as TextBox;
        TextBox txtAmount_DALG = gv_whr.Rows[e.RowIndex].FindControl("txtAmount_DALG") as TextBox;
        TextBox txtOther_Deduction = gv_whr.Rows[e.RowIndex].FindControl("txtOther_Deduction") as TextBox;

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Update_RM_Gain_Deduction_Godown_Wise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", lblGodown_ID.Text);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
            cmd.Parameters.AddWithValue("@Amount_DALG", txtAmount_DALG.Text);
            cmd.Parameters.AddWithValue("@Other_Deduction", txtOther_Deduction.Text);
            cmd.Parameters.AddWithValue("@CommodityID", ddlCommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_by", Request.ServerVariables["REMOTE_ADDR"].ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        FillGridwhr();
        //ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Record Save Successfully')", true);
        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Save Successfully ..'); </script> ");
    }

    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridwhr(); 
    }
}