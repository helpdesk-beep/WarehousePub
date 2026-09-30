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
using System.Linq;

public partial class Masters_Region_District_Branch_Wise_Warehouse_Details : System.Web.UI.Page
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
               // fillDistrict();
                fillRegion();
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillRegion()
    {
        try
        {
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT distinct Region_ID,Regionnm FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";
            }
            
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.Items.Clear();
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "Regionnm";
                ddlregion.DataValueField = "Region_ID";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, "--Select--");
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
    private void fillDistrict()
    {
        try
        {
            string region = "";
            string query = "";          
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlregion.SelectedValue + "' order by District_Name asc";
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
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' order by DepotName asc";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4' order by DepotName asc";

            }
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
        fillIssuecenter();
        GetBranchData();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();

    }
    public void GetBranchData()
    {
        try
        {
            string qry = "";
          //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Region_District_Branch_Wise_Maped_Godown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if(ddlregion.SelectedValue== "--Select--")
            {
                cmd.Parameters.AddWithValue("@RegionID", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@RegionID", ddlregion.SelectedValue);
            }
            if (ddlDistrict.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@DistrictID", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@DistrictID", ddlDistrict.SelectedValue);
            }
            if (ddlbranch.SelectedValue == "--Select--")
            {
                cmd.Parameters.AddWithValue("@BranchID", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                Depositor_Gridview.FooterRow.Style.Add("text-align", "center");
                Depositor_Gridview.FooterRow.Cells[3].Text = "Total";
                Depositor_Gridview.FooterRow.Cells[4].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Totalgodown")).ToString();
                Depositor_Gridview.FooterRow.Cells[5].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("noofmapgodown")).ToString();
                Depositor_Gridview.FooterRow.Cells[6].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("LeftGodownForMApedatbranch")).ToString();
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
     protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
     protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
        GetBranchData();
    }
}
