using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TribalGodown_StateRegionDistrictLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
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
        //trbranch.Visible = true;
    }
    protected void rbrm_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        //trbranch.Visible = false;
        trregion.Visible = true;
    }
    private void GetRegionfordist()
    {
        try
        {
            //string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            string qrySelect = "SELECT * FROM tbl_MetaData_Region where remark='T' order by region";

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

                strDist = "SELECT District_Name_HI,District_Id FROM tbl_MetaData_DISTRICT WHERE remarks='T' order by District_Name_HI";

            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "District_Name_HI";
                DDL_Dist.DataValueField = "District_Id";
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
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getDepot(DDL_Dist.SelectedValue);
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
            string Scope = "";
            if (rbbranch.Checked == true)
            {
                Uname = DDL_Dist.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "D";
            }
            else if (rbrm.Checked == true)
            {
                Uname = ddlregion.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "R";
            }
            else if (rbHo.Checked == true)
            {
                //Uname = "HOMPWLC";
                Uname = "100";
                pwd = txtlogpwd.Value;
                Scope = "H";
            }

            string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [Tribal_Godown].[dbo].[Tribal_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                Session["login"] = "true";
                Session["UserId"] = dr["UserId"].ToString();
                Session["UserName"] = dr["UserName"].ToString();
                Session["Scope"] = dr["Scope"].ToString();
                if (Uname == dr["UserId"].ToString() && (pwd == dr["Password"].ToString() || pwd == dr["MasterPass"].ToString()))
                {
                    Response.Redirect("Tribal_State_Home.aspx");  
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);

                    Response.Redirect("StateRegionDistrictLogin.aspx");  
                }

            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम गलत हो सकता है')", true);

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }
    protected void rbHo_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = false;
    }
}
