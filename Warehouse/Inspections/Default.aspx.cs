
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default: System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDist("1");
            GetRegionfordist();
            trregion.Visible = false;
            rbrm_CheckedChanged(null, null);
            RbTechnical_CheckedChanged(null, null);
            getlogintype();
            //string strMsg = "Record Update Successfully |||";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/BO/Godown_wise_Stock_Details.aspx';", true);
        }
    }
    protected void rbbranch_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = false;
        trBranch.Visible = false;
        trOfNm.Visible = true;
        GetInspOfficer();
    }
    protected void rbrm_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        trBranch.Visible = false;
        trregion.Visible = true;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
    }
    private void GetRegionfordist()
    {
        try
        {
            //string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            string qrySelect = "SELECT region,Region_Id FROM tbl_MetaData_Region order by region";

            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con_WLC);
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
            SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
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
            SqlDataAdapter da = new SqlDataAdapter(str, con_WLC);
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
    private void GetInspOfficer()
    {
        ddlInspOff.Items.Clear();
        try
        {
            //string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            //string qrySelect = "select PF_ID,OfficerName +' ('+ PF_ID +')' as Officer_Name from Insp_Officer_login order by Officer_Name";
            string qrySelect = "select A.PF_ID,A.OfficerName+ ' - ' + B.Designation +' ('+ A.PF_ID +')' as Officer_Name from Insp_Officer_login A INNER JOIN tbl_metadata_Inspection_officer B ON A.PF_ID=B.PF_ID order by Officer_Name";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, conStr);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlInspOff.DataSource = ds.Tables[0];
                ddlInspOff.DataTextField = "Officer_Name";
                ddlInspOff.DataValueField = "PF_ID";
                ddlInspOff.DataBind();
                ddlInspOff.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlInspOff.DataSource = null;
                ddlInspOff.DataBind();
            }
        }
        catch (Exception ex)
        {
            // lblError.Text = "";
            //lblError.Text = ex.Message;
        }
    }

    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }


    protected void btnlogin_Click(object sender, EventArgs e)
    {
        if (txtlogpwd.Value != "")
        {
            DoDepotLogin();
        }
        else
        {
            string strMsg = "Enter Password|||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        }
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
                Uname = ddlInspOff.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "D";
            }
            if (rbrm.Checked == true)
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
            else if (rdogodown.Checked == true)
            {
                //Uname = "HOMPWLC";
                Uname = ddl_godown.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "G";
            }
            else if (RbTQRegion.Checked == true)
            {
                Uname = ddlregion.SelectedValue.ToString();
                pwd = txtlogpwd.Value;
                Scope = "TQR";
            }
            else if (RBTQHO.Checked == true)
            {
                //Uname = "HOMPWLC";
                Uname = "109";
                pwd = txtlogpwd.Value;
                Scope = "TQHO";
            }
            if (Scope == "H" || Scope == "R" || Scope == "TQR" || Scope == "TQHO")
            {

                string strsql = "SELECT Salt,HashedPassword,FirstTimeLogin,[UserId],[UserName],[Password],[MasterPass],[Scope],[role],MasterHashedPassword,MasterSalt FROM [Inspection_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["UserId"].ToString();
                    Session["UserName"] = dr["UserName"].ToString();
                    Session["Scope"] = dr["Scope"].ToString();
                    Session["role"] = dr["role"].ToString();

                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "Inspection_Login", "UserId", Convert.ToString(dr["UserId"]), "Inspections/Default.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ///////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025///// With Master
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
                    ///// Check FIrst Time Login End ///// 

                    if (Existed)
                    {


                        // if (Uname == dr["UserId"].ToString() && (pwd == dr["Password"].ToString() || pwd == dr["MasterPass"].ToString()))
                        //{
                        if (Scope == "H")
                        {
                            Response.Redirect("~/Inspections/State/State_Welcome.aspx");
                        }
                        else if (Scope == "R")
                        {
                            Response.Redirect("~/Inspections/RO/RO_Welcome.aspx");
                        }
                        else if (Scope == "TQR")
                        {
                            Response.Redirect("~/Inspections/TQRO/RO_Welcome.aspx");
                        }
                        else if (Scope == "TQHO")
                        {
                            Response.Redirect("~/Inspections/TQHO/RO_Welcome.aspx");
                        }
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
            else if (Scope == "TQR" || Scope == "TQHO")
            {

                string strsql = "SELECT Salt,HashedPassword,FirstTimeLogin,[UserId],[UserName],[Password],[MasterPass],[Scope],[role],MasterHashedPassword,MasterSalt FROM [Inspection_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["UserId"].ToString();
                    Session["UserName"] = dr["UserName"].ToString();
                    Session["Scope"] = dr["Scope"].ToString();
                    Session["role"] = dr["role"].ToString();

                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "Inspection_Login", "UserId", Convert.ToString(dr["UserId"]), "Inspections/Default.aspx", connnectionObj };  ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ///////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///get Master password
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
                    ///// Check FIrst Time Login End ///// 

                    if (Existed)
                    {
                        //    if (Uname == dr["UserId"].ToString() && (pwd == dr["Password"].ToString() || pwd == dr["MasterPass"].ToString()))
                        //{
                        if (Scope == "TQR")
                        {
                            Response.Redirect("~/Inspections/TQRO/RO_Welcome.aspx");
                        }
                        else if (Scope == "TQHO")
                        {
                            Response.Redirect("~/Inspections/TQHO/RO_Welcome.aspx");
                        }
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
            else if (Scope == "D")
            {

                //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                string strsql = "select Salt,HashedPassword,FirstTimeLogin,OfficerName,A.PF_ID,O_Password as Password,Master_Password,role,B.Designation,MasterHashedPassword,MasterSalt from Insp_Officer_login A INNER JOIN tbl_metadata_Inspection_officer B ON A.PF_ID=B.PF_ID where A.PF_ID='" + ddlInspOff.SelectedValue.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string pass = dr["Password"].ToString();
                    Session["login"] = "true";
                    Session["UserId"] = dr["PF_ID"].ToString();
                    Session["UserName"] = dr["OfficerName"].ToString();
                    Session["role"] = dr["role"].ToString();
                    Session["Designation"] = dr["Designation"].ToString();
                    Session["Scope"] = "D";

                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "Insp_Officer_login", "PF_ID", Convert.ToString(dr["PF_ID"]), "Inspections/Default.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ///////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///get Master password
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
                    ///// Check FIrst Time Login End ///// 

                    if (Existed)
                    {

                        if (Uname == dr["PF_ID"].ToString() && (pwd == dr["Password"].ToString()) && (dr["Designation"].ToString() == "Technical Auditor") || (dr["Designation"].ToString() == "AGM") || (dr["Designation"].ToString() == "AQC") || (dr["Designation"].ToString() == "AQC(C)") || (dr["Designation"].ToString() == "QC") || (dr["Designation"].ToString() == "Manager(QC)") || (dr["Designation"].ToString() == "Manager(General)") || (dr["Designation"].ToString() == "Assistant accountant") || (dr["Designation"].ToString() == "Senior assistant") || (dr["Designation"].ToString() == "Junior Assistant") || (dr["Designation"].ToString() == "Stenographer") )
                        {
                            Response.Redirect("~/Inspections/Inspection_Officer/InspOfficer_ScheduledInspDetail.aspx");
                        }
                        else if (Uname == dr["PF_ID"].ToString() && (dr["Designation"].ToString() == "Account Auditor"))
                        {
                            Response.Redirect("~/Inspections/Audit/View_Account_Audit.aspx");
                        }
                        //else if (Uname == dr["PF_ID"].ToString() && (pwd == dr["Password"].ToString()) && (dr["Designation"].ToString() == "Account Auditor") || (pwd == dr["Master_Password"].ToString()))
                        //{
                        //    Response.Redirect("~/Inspections/Audit/Account_Audit.aspx");
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

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका यूसर नेम/पासवर्ड गलत हो सकता है')", true);

                }
            }
            else if (Scope == "B")
            {
                //string strsql = "SELECT [UserId],[UserName],[Password],[MasterPass],[Scope] FROM [JVS_Login] where UserId='" + Uname + "' and SCOPE='" + Scope + "'";
                string strsql = "SELECT Salt,HashedPassword,FirstTimeLogin,BID,BranchID,BranchName,BranchPwd as Password,role,MasterHashedPassword,MasterSalt FROM MetaDataBranchWithIssueCenter where BranchID='" + DDL_Branch.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["BranchID"].ToString();
                    Session["UserName"] = dr["BranchName"].ToString();
                    Session["role"] = dr["role"].ToString();
                    Session["DistID"] = DDL_Dist.SelectedValue.ToString();
                    Session["Scope"] = "B";
                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "MetaDataBranchWithIssueCenter", "BID", Convert.ToString(dr["BID"]), "Inspections/Default.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ///////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///get Master password
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
                    ///// Check FIrst Time Login End ///// 

                    if (Existed)
                    {
                        //if (Uname == dr["BranchID"].ToString() && (pwd == dr["Password"].ToString()))
                        //{
                            Session["Agency_ID"] = '1';
                            Response.Redirect("~/Inspections/BO/BO_Welcome.aspx");
                       // }
                        //else
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
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

            else if (Scope == "G")
            {

                string strsql = "select Login_Id,Salt,HashedPassword,FirstTimeLogin,Godown_Id,Godown_Name,Password,BranchID,DepotId,MasterHashedPassword,MasterSalt from Pvt_Warehouse_Login where Godown_Id='" + ddl_godown.SelectedValue.ToString() + "' and Active='Y' and GodownTypeId='" + ddllogintype.SelectedValue.ToString() + "'";
                //string strsql = "SELECT BranchID,BranchName,BranchPwd as Password,role FROM MetaDataBranchWithIssueCenter where BranchID='" + DDL_Branch.SelectedValue.ToString() + "' and BranchName='" + DDL_Branch.SelectedItem.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, con_WLC);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    Session["login"] = "true";
                    Session["UserId"] = dr["Godown_Id"].ToString();
                    Session["UserName"] = dr["Godown_Name"].ToString();
                    //Session["role"] = dr["role"].ToString();
                    Session["DistID"] = DDL_Dist.SelectedValue.ToString();
                    Session["BID"] = DDL_Branch.SelectedValue.ToString();
                    Session["Scope"] = "G";

                    ///////////////
                    string connnectionObj = "JVSGodownConString";
                    string[] queryArr = new string[] { "Pvt_Warehouse_Login", "Login_Id", Convert.ToString(dr["Login_Id"]), "Inspections/Default.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["Securelogin"] = queryArr;
                    ///////////////

                    ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///get Master password
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
                    ///// Check FIrst Time Login End ///// 

                    if (Existed)
                    {
                        //if (Uname == dr["Godown_Id"].ToString() && (pwd == dr["Password"].ToString()))
                        //{
                            //Session["Agency_ID"] = '1';
                            Response.Redirect("~/Inspections/Godown/Godown.aspx");
                       // }
                        //else
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
                        //}
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका पासवर्ड गलत हो सकता है')", true);
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
    private void getlogintype()
    {
        try
        {
            //string str = "SELECT * FROM [Storage_Agency_type] where Storage_Agency != 'MPWLC'";
            string str = "SELECT * FROM [Storage_Agency_type] where Storage_Agency not in ('MPWLC')";
            SqlDataAdapter da = new SqlDataAdapter(str, con_WLC);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllogintype.DataSource = ds.Tables[0];
                ddllogintype.DataTextField = "Storage_Agency";
                ddllogintype.DataValueField = "Storage_Agency_ID";
                ddllogintype.DataBind();
                ddllogintype.Items.Insert(0, "---Select---");
            }
            else
            {
                ddllogintype.Items.Clear();
            }

        }

        catch (Exception ex)
        {
            //lblError.Text = "";
            //lblError.Text = ex.Message;
        }
    }
    protected void ddllogintype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_godown.Items.Clear();
        if (ddllogintype.SelectedValue.ToString() == "2" || ddllogintype.SelectedValue.ToString() == "6" || ddllogintype.SelectedValue.ToString() == "7" || ddllogintype.SelectedValue.ToString() == "9" || ddllogintype.SelectedValue.ToString() == "10" || ddllogintype.SelectedValue.ToString() == "11" || ddllogintype.SelectedValue.ToString() == "3" || ddllogintype.SelectedValue.ToString() == "12" || ddllogintype.SelectedValue.ToString() == "5")
        {
            ddl_godown.Visible = true;
            lbl_godown.Visible = true;


        }
        else
        {
            ddl_godown.Visible = false;
            lbl_godown.Visible = false;


        }
    }
    protected void rbHo_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = false;
        trBranch.Visible = false;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
        ddllogintype.Visible = false;
        ddl_godown.Visible = false;
        lbl_godown.Visible = false;
    }
    protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = true;
        trBranch.Visible = true;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
        ddllogintype.Visible = false;
        ddl_godown.Visible = false;
        lbl_godown.Visible = false;
    }
    protected void rdogodown_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = true;
        trBranch.Visible = true;
        trOfNm.Visible = false;
        lbllogintype.Visible = true;
        ddllogintype.Visible = true;
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInspOfficer();
    }

    protected void DDL_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string hiredtype = ddllogintype.SelectedItem.Text;
        //if (ddllogintype.SelectedValue.ToString() == "7")
        //{

        //    hiredtype = "PVT.PEG";
        //}
        //else if (ddllogintype.SelectedValue.ToString() == "2")
        //{
        //    hiredtype = "SteelSilo";
        //}

        //string str = "select USER_NAME,Godown_Id from Private_Godown_Login where BranchID='" + DDL_Depot.SelectedValue.ToString() + "' and Active='Y' and GodownTypeId='" + ddllogintype.SelectedValue.ToString() + "'";
        string str = "select Godown_Name,Godown_Id from Pvt_Warehouse_Login where BranchID='" + DDL_Branch.SelectedValue.ToString() + "' and Active='Y' and GodownTypeId='" + ddllogintype.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(str, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // ddl_godown
            ddl_godown.DataSource = ds.Tables[0];
            ddl_godown.DataTextField = "Godown_Name";
            ddl_godown.DataValueField = "Godown_Id";
            ddl_godown.DataBind();
            ddl_godown.Items.Insert(0, "---Select---");
            Session["BranchType"] = "G";
            Session["GodownID_New"] = ddl_godown.SelectedValue.ToString();
        }
        else
        {
            //  ddl_godown.Items.Clear();
        }
    }

    protected void RbTechnical_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        trBranch.Visible = false;
        trregion.Visible = true;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
    }

    protected void RbTQRegion_CheckedChanged(object sender, EventArgs e)
    {
        trdest.Visible = false;
        trBranch.Visible = false;
        trregion.Visible = true;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
    }

    protected void RBTQHO_CheckedChanged(object sender, EventArgs e)
    {
        trregion.Visible = false;
        trdest.Visible = false;
        trBranch.Visible = false;
        trOfNm.Visible = false;
        lbllogintype.Visible = false;
        ddllogintype.Visible = false;
        ddl_godown.Visible = false;
        lbl_godown.Visible = false;
    }
}