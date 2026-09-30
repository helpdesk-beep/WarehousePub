using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web; 
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspection_InspectionLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDist("1");
            GetRegionfordist();
            trregion.Visible = false;
            
        }
    }
    protected void rbbranch_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = true;
        trbranch.Visible = true;
    }
    protected void rbrm_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        trbranch.Visible = false;
        trregion.Visible = true;
    }
    private void GetRegionfordist()
    {
        try
        {
            string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "region";
                ddlregion.DataValueField = "Region_Id";
                ddlregion.DataBind();
                // ddlregion.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {
           // lblError.Text = "";
            //lblError.Text = ex.Message;
        }
    }


    private void GetDist(string Scope)
    {
        try
        {
            string srvr = System.Configuration.ConfigurationManager.AppSettings["HostedServer"].ToString();
            string strDist = "";
            if (Scope == "1")
            {

                strDist = "SELECT [District_Id] as login_id  ,[District_Name] as User_Name FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DISTRICT] order by District_Name";

            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "User_Name";
                DDL_Dist.DataValueField = "login_id";
                DDL_Dist.DataBind();
                DDL_Dist.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Dist.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {
            
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    private void getDepot(string distId)
    {
        try
        {
            string str = "SELECT mbi.BranchID,mbi.BranchName FROM [MetaDataBranchWithIssueCenter] as mbi  WHERE mbi.[DistrictId] = '" + distId.ToString() + "' and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by mbi.BranchID";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Depot.DataSource = ds.Tables[0];
                DDL_Depot.DataTextField = "BranchName";
                DDL_Depot.DataValueField = "BranchID";
                DDL_Depot.DataBind();
                DDL_Depot.Items.Insert(0, "---Select---");
                Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
            }
            else
            {
                DDL_Depot.Items.Clear();
            }
        //    lbl_Depot.Visible = true;
          //  DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {
           
        }
    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(DDL_Dist.SelectedValue);
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        DoDepotLogin();
    }
    private void DoDepotLogin()
    {
        try
        {
            string Uname = "";
            string pwd = "";
            if (rbbranch.Checked == true)
            {
                 Uname = DDL_Depot.SelectedValue.ToString();
                 pwd = txtlogpwd.Value;
            }
            else if (rbrm.Checked == true)
            {
                Uname = ddlregion.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
            }
               
                string strsql = "SELECT [InspectionAuth],[InsPost],[UserID],[UserName],[InsPwd],[InsMobile],[Insemail],LoginType FROM [InspectionLogin] where [UserID]='" + Uname + "'  and [InsPwd]='" + pwd + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["insname"] = dr["InspectionAuth"].ToString();
                    Session["UserName"] = dr["UserName"].ToString();
                    Session["UserID"] = dr["UserID"].ToString();
                    Session["email"] = dr["Insemail"].ToString();
                    Session["mobile"] = dr["InsMobile"].ToString();
                    Session["LoginType"] = dr["LoginType"].ToString();
                    // string em = dr["UEmail"].ToString();
                    if (rbbranch.Checked == true)
                    {
                        //Response.Redirect("BranchInspection.aspx");
                        Response.Redirect("OldBranchInsp.aspx");
                    }
                    else if (rbrm.Checked == true)
                    {
                        ddlregion.DataTextField = "region";
                        ddlregion.DataValueField = "Region_Id";
                        Session["RegionName"] = ddlregion.SelectedItem.Text;
                        Session["RegionId"] = ddlregion.SelectedItem.Value;
                        Session["LoginType"] = dr["LoginType"].ToString();
                        Response.Redirect("RegionInspection.aspx");
                    }

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका ई-मेल या पासवर्ड गलत हो सकता है')", true);

                }
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }

}