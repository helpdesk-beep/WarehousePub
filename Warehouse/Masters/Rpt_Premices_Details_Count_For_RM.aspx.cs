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

public partial class Masters_Rpt_Premices_Details_Count_For_RM : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                //fillDistrict();
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    //private void fillDistrict()
    //{
    //    try
    //    {
    //        string region = "";
    //        string query = "";
    //        if (Session["UserName"].ToString() == "MPSWLC")
    //        {
    //            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
    //        }
    //        else
    //        {
    //            if (Session["RoleId"].ToString() == "2")
    //            {
    //                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
    //            }
    //            else
    //            {
    //                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

    //            }
    //        }
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
    //}
    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranchData();
    //}
    public void GetBranchData()
    {
        try
        {
            string qry = "";
          //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Premicess_Count_Report_For_RM", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegionID", Session["Region_Logid"].ToString());
           // cmd.Parameters.AddWithValue("@StorageType", ddlWST.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
               // trmobtxt.Visible = true;
                //lbldistrictid.Text = ds.Tables[0].Rows[0]["DistrictId"].ToString();
                //lblbranchid.Text = ds.Tables[0].Rows[0]["BranchID"].ToString();
                showgrid.Visible = true;
           
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                //trbtnhide.Visible = false;
                //trmobtxt.Visible = false;
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
        GetBranchData();
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        // lblWHID.Text = (row.FindControl("lblWHID") as Label).Text;
        string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
        string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
        Session["hdnWHID"] = (row.FindControl("hdnWHID") as HiddenField).Value;
        Session["hdnBranchId"] = (row.FindControl("hdnBranchId") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
        FillMapGodown(hdnBranchId, hdnWHID);
    }
    public void FillMapGodown(string branchid,string WHID)
    {
        try
        {
            string qry = "";
            //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Premises_Wise_Godwon_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", branchid.ToString());
            cmd.Parameters.AddWithValue("@WHID", WHID.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
               
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }

   
}
