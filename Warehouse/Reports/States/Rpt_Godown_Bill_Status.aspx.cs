using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Text;

public partial class Reports_States_Rpt_Godown_Bill_Status : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PopMsg = "";
            PopMsg = Request.QueryString["PopMsg"];
            if (Request.QueryString["PopMsg"] != null)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
            }
            fillDistrict();
            CropYear();
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";


            string query = "";

            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
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
                ddlDistrict.Items.Insert(0, "--Select--");
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

    private void fillIssuecenter()
    {
        try
        {
            string region = "";


            string query = "";

            query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' ";

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
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");
                depottype = ds.Tables[0].Rows[0]["DepoTypeID"].ToString().Trim();

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
    private void fillGodown()
    {
        try
        {

            string query = "";

            query = "SELECT Godown_ID,Godown_Name FROM [dbo].[tbl_MetaData_GODOWN] where BranchId='" + ddlbranch.SelectedValue + "'";

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
    protected void fillgrid()
    {
        string Godown = "0",CropYear="0";
        if (ddlGodown.SelectedValue == "--All--")
        {
            Godown = "0";
        }
        else if (ddlGodown.SelectedValue == "--Select--")
        {
            Godown = "0";
        }
        else
        {
            Godown = ddlGodown.SelectedValue;
        }

        if (ddlCropYear.SelectedValue == "--All--")
        {
            CropYear = "0";
        }
        else if (ddlCropYear.SelectedValue == "--Select--")
        {
            CropYear = "0";
        }
        else
        {
            CropYear = ddlCropYear.SelectedValue;
        }
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Godown_Bill_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@MonthID", ddlmonth.SelectedValue);
                cmd.Parameters.AddWithValue("@GodownID", Godown);
                cmd.Parameters.AddWithValue("@CropYear", CropYear);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void CropYear()
    {
        try
        {
            qry = "select Full_Crop_Year CropYear from tbl_MetaData_Crop_Year";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropYear";
                ddlCropYear.DataValueField = "CropYear";
                ddlCropYear.DataBind();
                ddlCropYear.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
        fillgrid();
    }
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}