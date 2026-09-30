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
using AjaxControlToolkit;

public partial class BM_GodownRegistrationDtls : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    //string RegionID = Session["Region_ID"].ToString();
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblRegionName.Text = Session["BranchId"].ToString();
        //if (Session["UserName"].ToString() != null || Session["Region_ID"].ToString() != null)
        //{
        //    if (!IsPostBack)
        //    {
        GetRegionWiseBranchData();
        //    }
        //}
        //else
        //{
        //    Response.Redirect("~/SessionExpired.htm");
        //}
    }

    //private void fillDistrict()
    //{
    //    try
    //    {
    //        string region = "";
    //        string query = "";
    //        query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + lblRegionName.Text.ToString() + "' order by District_Name asc";
    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDistrict.Items.Clear();
    //            ddlDistrict.DataSource = ds.Tables[0];
    //            ddlDistrict.DataTextField = "District_Name";
    //            ddlDistrict.DataValueField = "District_Id";
    //            ddlDistrict.DataBind();
    //            ddlDistrict.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    //private void fillIssuecenter()
    //{
    //    try
    //    {
    //        string region = "";
    //        string query = "";
    //        if (Session["UserName"].ToString() == "MPSWLC")
    //        {
    //            query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' order by DepotName asc";
    //        }
    //        else
    //        {
    //            query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4' order by DepotName asc";

    //        }
    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlbranch.Items.Clear();
    //            ddlbranch.DataSource = ds.Tables[0];
    //            ddlbranch.DataTextField = "DepotName";
    //            ddlbranch.DataValueField = "BranchId";
    //            ddlbranch.DataBind();
    //            ddlbranch.Items.Insert(0, "--Select--");

    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    //protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillIssuecenter();
    //    GetBranchData();
    //}
    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranchData();

    //}

    public void GetRegionWiseBranchData()
    {
        try
        {
            
            string query = "";
            string BranchID = Session["BranchId"].ToString();            
            con.Open();
            query = "select dst.Regionnm AS Region, dst.District_Name AS District,md.DepotName AS Branch,B.Registration_Id as RegistrationID, " +
                    " " + "WR.Warehouse_Name WarehouseName, WR.Warehouse_Address WarehouseAddress, A.Godown_ID As GodownID, " +
                    " " + "A.Godown_Name as GodownName from JointVentureScheme2018.dbo.tbl_Warehouse_Capacity_Offer_Rabi_2024_25 B " +
                    " " + "INNER JOIN JointVentureScheme2018.dbo.tbl_WarehouseRegistration WR ON B.Registration_Id = WR.Registration_Id" +
                    " " + "INNER JOIN tbl_MetaData_DEPOT md ON B.BranchId = md.BranchId" +
                    " " + "INNER JOIN tbl_MetaData_DISTRICT dst ON md.DistrictId = dst.District_Id" +
                    " " + "LEFT JOIN tbl_Metadata_godown_2024 A ON A.JVS_RegNo = B.Registration_Id" +
                    " " + "where A.Godown_ID is null  and md.BranchID ='" + BranchID + "' order by dst.Regionnm,dst.District_Name,md.DepotName";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                showgrid.Visible = true;

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                showgrid.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();

            }


        }
        catch (Exception ex)
        {

        }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddlWST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegionWiseBranchData();
    }

    protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Depositor_Gridview.PageIndex = e.NewPageIndex;
        GetRegionWiseBranchData();
    }

    private void GetRegion()
    {
        try
        {
            string region = Session["Region_ID"].ToString();
            string query = "";
            query = "select Region_Id, region from tbl_MetaData_Region where Region_Id ='" + region + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRegionName.Text = ds.Tables[0].Columns["region"].ToString();
            }
            else
            {
                
            }
        }
        catch (Exception)
        {
            
        }
    }
}
