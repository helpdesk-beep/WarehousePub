using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_MLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        txt_password.Focus();
        txt_password.Attributes.Add("onkeypress", "return checksqlkey_psw(event,this)");
        txt_password.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
        txt_password.Attributes.Add("onKeyUp", "return Do_login();");
        btn_login.Attributes.Add("onkeypress", "return LoginOnEnter(this);");
        if (!IsPostBack)
        {
            txt_password.Focus();
            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);
            Session["salt"] = salt.ToString();
            GetDist();
            lbl_Depot.Visible = true;
            DDL_Depot.Visible = true;
           // getlogintype();
        }
    }
    private void DoDepotLogin()
    {
        try
        {
            if (Session["salt"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            string Uname = DDL_Depot.SelectedItem.Text;
            string Did = DDL_Dist.SelectedValue;
            string strsql = "Select login_id,User_Name,convert(varchar(300),password) as Password,DepotId,DistrictId,Fname,Lname,Scope,convert(varchar(300),MasterPassword) as MasterPassword,Access_Restrict,BranchID  from Storage_Login where User_Name='" + Uname + "' and DistrictId='" + Did + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();
                string login_ID = dr["login_id"].ToString();
                Session["Depot_Logid"] = dr["login_id"].ToString();
                Session["State_StateID"] = "23";
                string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();
                string TBpass = txt_password.Text.ToLower().Trim().ToString();
                if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)
                {
                    Session["Depot_DistID"] = dr["DistrictId"].ToString();
                    Session["Depot_DistName"] = DDL_Dist.SelectedItem.Text;
                    Session["RoleId"] = 1;
                    Session["Depot_DepotID"] = dr["DepotId"].ToString();
                    Session["UserName"] = DDL_Depot.SelectedItem.Text;
                    Session["IsSussess"] = "Success";
                    Session["BranchId"] = dr["BranchID"].ToString();
                    string Branch = Session["BranchID"].ToString();
                    Response.Redirect("~/Mobile/Home.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);
                }
            }
        }
        catch (Exception ex)
        {
           
        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        try
        {
            Session["lang"] = "Empty";
          
           
                if (DDL_Dist.SelectedItem.Text == "---Select---" || DDL_Dist.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District Name')", true);
                }
                else if (DDL_Depot.SelectedItem.Text == "---Select---" || DDL_Depot.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch Name')", true);
                }
                else
                {
                    DoDepotLogin();
                }
          
        }
        catch (Exception ex)
        {
           
        }
    }

    private static string CreateSalt(int size)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size + 1];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string CreatePasswordHash(string dist)
    {
        string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(dist.ToString().Trim(), "MD5");
        return hashedPwd;
    }
    private void GetDist()
    {
        try
        {
            string srvr = System.Configuration.ConfigurationManager.AppSettings["HostedServer"].ToString();
            string strDist = "";
          
            strDist = "SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
             
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
            string str = "SELECT [login_id], [User_Name] FROM [Storage_Login] as sl inner join [MetaDataBranchWithIssueCenter] as mbi on sl.BranchID=mbi.BranchID WHERE sl.[DistrictId] = '" + distId.ToString() + "' and Scope=1 and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by [User_Name]";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Depot.DataSource = ds.Tables[0];
                DDL_Depot.DataTextField = "USER_NAME";
                DDL_Depot.DataValueField = "login_id";
                DDL_Depot.DataBind();
                DDL_Depot.Items.Insert(0, "---Select---");
            }
            else
            {
                DDL_Depot.Items.Clear();
            }
            lbl_Depot.Visible = true;
            DDL_Depot.Visible = true;
        }

        catch (Exception ex)
        {
           
        }
    }


    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(DDL_Dist.SelectedValue);
    }
}