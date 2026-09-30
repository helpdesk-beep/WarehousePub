using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_Logins : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
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
        trBranch.Visible = false;
    }
    protected void rbrm_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        trBranch.Visible = false;
        trregion.Visible = true;
    }
    private void GetRegionfordist()
    {
        try
        {
            //string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            string qrySelect = "SELECT region,Region_Id FROM tbl_MetaData_Region order by region";

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

                strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";

            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "District_Name";
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
    private void GetBranch()
    {
        try
        {
            string DistrictId = DDL_Dist.SelectedValue.ToString();
            //string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
            string str = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi  WHERE mbi.[DistrictId] = '" + DistrictId.ToString() + "' and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by mbi.BranchName";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Branch.DataSource = ds.Tables[0];
                DDL_Branch.DataTextField = "BranchName";
                DDL_Branch.DataValueField = "BranchID";
                DDL_Branch.DataBind();
                DDL_Branch.Items.Insert(0, "---Select---");
                //Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
            }
            else
            {
                DDL_Branch.Items.Clear();
            }
            //lbl_Depot.Visible = true;
            //DDL_Depot.Visible = true;
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
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
            if (rbDSO.Checked == true)
            {
                Uname = DDL_Dist.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "DSO";
            }
            else if (rbbranch.Checked == true)
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
            else if (rdoBranch.Checked == true)
            {
                Uname = DDL_Branch.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "B";
            }
            else if (rbHo.Checked == true)
            {
                //Uname = "HOMPWLC";
                Uname = "100";
                pwd = txtlogpwd.Value;
                Scope = "H";
            }
            if (Scope == "H" || Scope == "R")
            {
                string strsql = "SELECT [Salt],[HashedPassword],[FirstTimeLogin],[UserId],[UserName],[Password],[MasterPass],[Scope],MasterHashedPassword,MasterSalt FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
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
                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "JVS_Login", "UserId", Convert.ToString(dr["UserId"]), "JointVentureScheme/Logins.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    //string[] queryArr = new string[] { "JVS_Login", "UserId", Convert.ToString(dr["UserId"]) }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///Get Master password
                    string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                    string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                    ///////////////////////////////
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        Existed = SecurityPolicy.VerifyPasswordWithMaster(hdnActualPassword.Value, HashedPassword, Salt,MasterHashedPassword,MasterSalt); ///Varify Password after login...
                    }
                    else
                    {
                        Response.Redirect("~/ChangePassword.aspx", true);
                    }
                    if (Existed)
                    {
                        //if (Uname == dr["UserId"].ToString() && (pwd == dr["Password"].ToString() || pwd == dr["MasterPass"].ToString()))
                        //{
                        if (Scope == "H")
                        {
                            //Response.Redirect("JVSStateReport.aspx");
                            Response.Redirect("DistrictWiseJVSOffer.aspx");
                        }
                        else if (Scope == "R")
                        {
                            Response.Redirect("JVSRegionReport.aspx");
                            //}
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
                            //Response.Redirect("Logins.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);
                    }

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम/पासवर्ड गलत हो सकता है')", true);

                }
            }
            else if (Scope == "D")
            {
                //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                string strsql = "SELECT [Salt],[HashedPassword],[FirstTimeLogin], District_Id,District_Name,DMPassword as Password  FROM [tbl_MetaData_DISTRICT] where District_Id='" + DDL_Dist.SelectedValue.ToString() + "' and District_Name='" + DDL_Dist.SelectedItem.Text.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["District_Id"].ToString();
                    Session["UserName"] = dr["District_Name"].ToString();
                    Session["Scope"] = "D";
                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "tbl_MetaData_DISTRICT", "District_Id", Convert.ToString(dr["District_Id"]), "JointVentureScheme/Logins.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    //string[] queryArr = new string[] { "JVS_Login", "UserId", Convert.ToString(dr["UserId"]) }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        Existed = SecurityPolicy.VerifyPassword(hdnActualPassword.Value, HashedPassword, Salt); ///varify Password after login...
                    }
                    else
                    {
                        Response.Redirect("~/ChangePassword.aspx", true);
                    }

                    if (Existed)
                    {
                        //if (Uname == dr["District_Id"].ToString() && (pwd == dr["Password"].ToString()))
                        //  {
                        Response.Redirect("JVSDistrictofferReport.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);

                        //Response.Redirect("Logins.aspx");
                        // }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found!')", true);
                }
            }
            else if (Scope == "DSO")
            {

                //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                string strsql = "SELECT Salt_DSO,HashedPassword_DSO,FirstTimeLogin_DSO,District_Id,District_Name,DSOPassord as Password  FROM [tbl_MetaData_DISTRICT] where District_Id='" + DDL_Dist.SelectedValue.ToString() + "' and District_Name='" + DDL_Dist.SelectedItem.Text.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["District_Id"].ToString();
                    Session["UserName"] = dr["District_Name"].ToString();
                    Session["Scope"] = "DSO";
                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "tbl_MetaData_DISTRICT_DSO", "District_Id", Convert.ToString(dr["District_Id"]), "JointVentureScheme/Logins.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    //string[] queryArr = new string[] { "JVS_Login", "UserId", Convert.ToString(dr["UserId"]) }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin_DSO"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword_DSO"] == DBNull.Value ? null : dr["HashedPassword_DSO"].ToString();
                    string Salt = dr["Salt_DSO"] == DBNull.Value ? null : dr["Salt_DSO"].ToString();
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        Existed = SecurityPolicy.VerifyPassword(hdnActualPassword.Value, HashedPassword, Salt); ///varify Password after login...
                    }
                    else
                    {
                        Response.Redirect("~/ChangePassword.aspx", true);
                    }
                    if (Uname == dr["District_Id"].ToString() && (pwd == dr["Password"].ToString()))
                    {
                        Response.Redirect("JVS_DSO_Home.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
                    }

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम/पासवर्ड गलत हो सकता है')", true);

                }
            }
            else if (Scope == "B")
            {
                //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                string strsql = "SELECT Salt,HashedPassword,FirstTimeLogin,BranchID,BranchName,BranchPwd as Password,MasterHashedPassword,MasterSalt FROM MetaDataBranchWithIssueCenter where BranchID='" + DDL_Branch.SelectedValue.ToString() + "' and BranchName='" + DDL_Branch.SelectedItem.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["BranchID"].ToString();
                    Session["UserName"] = dr["BranchName"].ToString();
                    Session["DistID"] = DDL_Dist.SelectedValue.ToString();
                    Session["Scope"] = "B";
                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "MetaDataBranchWithIssueCenter", "BranchID", Convert.ToString(dr["BranchID"]), "JointVentureScheme/Logins.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    //string[] queryArr = new string[] { "JVS_Login", "UserId", Convert.ToString(dr["UserId"]) }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///Get Master password
                    string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                    string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                    ///////////////////////////////
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        Existed = SecurityPolicy.VerifyPasswordWithMaster(hdnActualPassword.Value, HashedPassword, Salt, MasterHashedPassword, MasterSalt); ///Varify Password after login...
                    }
                    else
                    {
                        Response.Redirect("~/ChangePassword.aspx", true);
                    }
                    if (Existed)
                    {
                        //if (Uname == dr["BranchID"].ToString() && (pwd == dr["Password"].ToString()))
                        //if (dr["BranchID"].ToString() == dr["BranchID"].ToString())
                        //{
                        Session["Agency_ID"] = '1';
                        Response.Redirect("BranchHome.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);

                        //Response.Redirect("Logins.aspx");

                    }
                  
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम/पासवर्ड गलत हो सकता है')", true);
                }
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
        trBranch.Visible = false;
    }
    protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = true;
        trBranch.Visible = true;
    }

    protected void rbDSO_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = true;
        trBranch.Visible = false;
    }
}
