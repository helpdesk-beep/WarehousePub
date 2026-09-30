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

public partial class Regionwise_Report_Updated_IssueCenterLatLong : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    //string RegionID = Session["Region_ID"].ToString();
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lblRegionName.Text = Session["Region_ID"].ToString();
        //GetRegionWiseBranchData();
        if (!IsPostBack)
        {
            Fillgrid();
        }

        //if (!IsPostBack)
        //{
        //    if (Session["UserName"] != null)
        //    {
        //        if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
        //        {
        //            if (!IsPostBack)
        //            {
        //                Fillgrid();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("~/SessionExpired.htm");
        //    }
        //    //fillgrid();
        //    //GetRegion();
        //}

    }
/*
    private void fillDistrict()
    {
        try
        {
            string region = "";
            string query = "";
            query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + lblRegionName.Text.ToString() + "' order by District_Name asc";
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
*/
/*    private void fillIssuecenter()
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
    } */
    /*
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
        GetBranchData();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();

    }
    */
    //public void GetRegionWiseBranchData()
    //{
    //    try
    //    {
            
    //        string qry = "";
    //        SqlCommand cmd = new SqlCommand("Get_Godown_Details_For_Update_Details_RegionWise", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            Depositor_Gridview.DataSource = ds;
    //            Depositor_Gridview.DataBind();
    //            showgrid.Visible = true;

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
    //            showgrid.Visible = false;
    //            Depositor_Gridview.DataSource = null;
    //            Depositor_Gridview.DataBind();

    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
 
    protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Depositor_Gridview.PageIndex = e.NewPageIndex;
        Fillgrid();
    }

    private void GetRegion()
    {
        try
        {
            string region = "";
            string query = "";
            query = "select Region_Id, region from tbl_MetaData_Region where Region_Id = " + Session["Region_ID"].ToString() + "'";
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

    public void GetBranchData()
    {
        try
        {
            string qry = "";
            //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Premices_Details_For_Updatatio", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
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
/*
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection())
        {
            SqlCommand cmd = new SqlCommand("Fill_Branch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
*/
/*
    private void FillDistrict()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID= '" + Session["Region_ID"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlDistrict.Items.Insert(0, "--Select--");
        }
    }
*/
    protected void Fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetRegionDistrictBranchWise_IssueCenterLatLong", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {

                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    //protected void btnshow_Click(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddIssueCenterLatLong")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            string hdnDepotID = (row.FindControl("hdnDepotID") as HiddenField).Value;
            string hdnIssueCenterID = (row.FindControl("txtIssueCenterID") as Label).Text.ToString();
            //string hdnIssueCenterName = (row.FindControl("hdnIssueCenterName") as TextBox).Text;
            string hdnDistrictID = (row.FindControl("hdnDistrictID") as HiddenField).Value;
            string hdnDistrictName = (row.FindControl("lblDistrictName") as Label).Text;
            string hdnBranchID = (row.FindControl("hdnBranchID") as HiddenField).Value;
            string hdnBranchName = (row.FindControl("lblDepotName") as Label).Text.ToString();
            string hdnLatitude = (row.FindControl("txtLatitude") as TextBox).Text.ToString();
            string hdnLongitude = (row.FindControl("txtLongitude") as TextBox).Text.ToString();
            //string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            Session["hdnDepotID"] = hdnDepotID.ToString();
            Session["hdnIssueCenterID"] = hdnIssueCenterID.ToString();
            //Session["hdnIssueCenterName"] = hdnIssueCenterName.ToString();
            Session["hdnDistrictID"] = hdnDistrictID.ToString();
            Session["hdnDistrictName"] = hdnDistrictName.ToString();
            Session["hdnBranchID"] = hdnBranchID.ToString();
            Session["hdnBranchName"] = hdnBranchName.ToString();
            Session["hdnLatitude"] = hdnLatitude.ToString();
            Session["hdnLongitude"] = hdnLongitude.ToString();
            AddIssueCenterDetails(hdnIssueCenterID,hdnBranchID, hdnLatitude, hdnLongitude);
        }
    }

    protected void AddIssueCenterDetails(string IssueCenterID, string BranchID, string Latitude, string Longitude)
    {
        //string BranchID = Session["BranchId"].ToString();
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            //if (txtlat.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Latitude of Issue Center')", true);
            //}

            //else if (txtlong.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Longitude of Issue Center')", true);
            //}
            //else if (txtIssueCenterID.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Issue Center ID of Issue Center')", true);
            //}

            //else
            {
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Add_IssueCenter_LatLong", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@IssueCenterID", IssueCenterID);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@Latitude", Convert.ToDecimal(Latitude));
                cmd.Parameters.AddWithValue("@Longitude", Convert.ToDecimal(Longitude));
                cmd.Parameters.AddWithValue("@ClientIP", Client_Ip);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Details Added Successfully')", true);
                    sqltrans.Commit();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Added')", true);

                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
}
