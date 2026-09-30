using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Data;

public partial class UpdateFirstLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    chksql chk = null;

    static string saltValue = "s@!tVal*e";
    static string hashAlgorithm = "SHA1";
    static int passwordIterations = 2;
    static string initVector = "@1B2c3!@)#$%@#X6g7FG";
    static int keySize = 256;
    public static string Encrypt1(string plainText, string passPhrase)
    {

        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                        passPhrase,
                                                        saltValueBytes,
                                                        hashAlgorithm,
                                                        passwordIterations);


        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.ECB;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        string cipherText = Convert.ToBase64String(cipherTextBytes);
        return cipherText;
    }
    public static string Decrypt1(string cipherText, string passPhrase)
    {

        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.ECB;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        //Start decrypting.
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

        // Return decrypted string.  
        return plainText;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // txt_password.Focus();
        // txt_password.Attributes.Add("onkeypress", "return  checksqlkey_psw(event,this);");
        // txt_password.Attributes.Add("onchange", "return chksqltxt_psw(this),MD5(this);");
        // txt_password.Attributes.Add("onKeyUp", "return Do_login();");
        btn_login.Attributes.Add("onkeypress", "return LoginOnEnter(this);");

        //ArrayList ctrllist = new ArrayList();
        //ctrllist.Add(txt_password.Text);
        //if (chk == null)
        //{
        //}
        //else
        //{
        //    bool chkstr = chk.chksql_server(ctrllist);
        //    if (chkstr == true)
        //    {
        //        Page.Server.Transfer(HttpContext.Current.Request.Path);
        //    }
        //}
        //Updated By Ashutosh

        //Updated By Ashutosh END

        if (!IsPostBack)
        {


            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);
            Session["salt"] = salt.ToString();
            GetDist(rblLoginType.SelectedValue.ToString());
            lbl_Depot.Visible = false;
            DDL_Depot.Visible = false;
            getlogintype();
            GetRegionfordist();
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            Session["GodownID"] = null;
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
                if (srvr == "egrains")
                {
                    GetRegionfordist();
                    // GetDistrict();
                    strDist = "SELECT [District_Id] as login_id  ,[District_Name] as User_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlregion.SelectedValue.ToString() + "' order by District_Name";
                }
                else
                {
                    GetRegionfordist();
                    // GetDistrict();
                    strDist = "SELECT [District_Id] as login_id  ,[District_Name] as User_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlregion.SelectedValue.ToString() + "'order by District_Name";
                }
            }
            else if (Scope == "2")
            {
                strDist = "SELECT [login_id], [User_Name] FROM [Storage_Login] WHERE Scope=2 order by [User_Name]";
            }
            else if (Scope == "3")
            {
                strDist = "SELECT  [User_Name] ,[login_id] FROM [RegionState_Login] WHERE Scope=3 order by Srno";
            }
            else if (Scope == "4")
            {
                strDist = "SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
            }
            else if (Scope == "6")
            {
                if (srvr == "egrains")
                {
                    strDist = "SELECT login_id , User_Name  from Storage_Login where scope=10 union SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
                }
                else
                {
                    strDist = "SELECT [District_Id] as  [login_id], [District_Name] as  [User_Name] FROM [tbl_MetaData_DISTRICT] order by [User_Name]";
                }
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
            lblError.Text = "";
            lblError.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void DDL_Dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblLoginType.SelectedValue == "1")
        {
            getDepot(DDL_Dist.SelectedValue);
            DDL_Depot.Visible = true;
            lbl_Depot.Visible = true;
            regionblock.Visible = true;
        }
        else if (rblLoginType.SelectedValue == "2")
        {
            getDepot("0");
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            regionblock.Visible = false;
        }
        else if (rblLoginType.SelectedValue == "3")
        {
            getDepot("0");
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            regionblock.Visible = false;
        }
        else if (rblLoginType.SelectedValue == "6")
        {
            getotherDepot(DDL_Dist.SelectedValue);
            if (RadioButton1.Checked)
            {
                DDL_Depot.Visible = true;
                lbl_Depot.Visible = true;
            }
            else
            {
                DDL_Depot.Visible = false;
                lbl_Depot.Visible = false;
            }
            regionblock.Visible = false;
        }
    }
    private void GetRegion()
    {
        try
        {
            string qrySelect = "SELECT * FROM tbl_MetaData_Region order by region";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDL_Dist.DataSource = ds.Tables[0];
                DDL_Dist.DataTextField = "region";
                DDL_Dist.DataValueField = "Region_Id";
                DDL_Dist.DataBind();
                DDL_Dist.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
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
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }
    private void GetDistrict()
    {
        try
        {
            string qrySelect = "SELECT [District_Id] as login_id  ,[District_Name] as User_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlregion.SelectedValue.ToString() + "' order by District_Name";
            SqlDataAdapter da = new SqlDataAdapter(qrySelect, con);
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
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {

            string epwd;
            string dpwd1;
            string dpwd;
            if (rblLoginType.SelectedValue == "1")
            {
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

                    //DoDepotLogin();
                    DoDepotLogin();
                }
            }
            else if (rblLoginType.SelectedValue == "2")
            {
                if (DDL_Dist.SelectedItem.Text == "---Select---" || DDL_Dist.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Region Name')", true);
                }
                else
                {
                    //DoRegionLogin();
                    DoRegionLogin();
                }
            }
            else if (rblLoginType.SelectedValue == "3")
            {
                if (DDL_Dist.SelectedItem.Text == "---Select---" || DDL_Dist.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District Name')", true);
                }
                else
                {
                    //DoStateLogin();
                    DoStateLogin();
                }
            }
            else if (rblLoginType.SelectedValue == "4")
            {
                if (DDL_Dist.SelectedItem.Text == "---Select---" || DDL_Dist.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District Name')", true);
                }
                else
                {
                    //CollectorLog();
                    CollectorLog();
                }
            }
            else if (rblLoginType.SelectedValue == "5")
            {
                //AdminLog();
                AdminLog();
            }
            else if (rblLoginType.SelectedValue == "6")
            {
                if (DDL_Dist.SelectedItem.Text == "---Select---" || DDL_Dist.Items.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District Name')", true);
                }
                //else if (DDL_Depot.SelectedItem.Text == "---Select---" || DDL_Depot.Items.Count == 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch Name')", true);
                //}
                else
                {
                    if (RadioButton1.Checked)
                    {
                        // DoDepotLogin();
                        DoDepotLogin();
                    }
                    else
                    {
                        //DoDistLogin();
                        DoDistLogin();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }
    // private void AdminLog()
    private void AdminLog()
    {
        try
        {
            string Uname = "Admin";
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,User_Name,convert(varchar(300),password) as Password,Scope,convert(varchar(300),MasterPassword) as MasterPassword," +
                "Access_Restrict,MasterHashedPassword,MasterSalt  from RegionState_Login  where User_Name='" + Uname + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();
                string login_ID = dr["login_id"].ToString();
                //Session["Region_Logid"] = dr["login_id"].ToString();
                Session["State_Logid"] = dr["login_id"].ToString();
                string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();

                ///////////////
                string connnectionObj = "FCIConnectionString";
                string[] queryArr = new string[] { "RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                Session["SecurePassword"] = queryArr;
                ////////////

                ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                ///Get Master password
                // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                ///////////////////////////////
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    UpdateUserLogin("RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                }

            }
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    //private void CollectorLog()
    private void CollectorLog()
    {
        try
        {
            string Uname = DDL_Dist.SelectedItem.Text;
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,User_Name,convert(varchar(300),password) as Password,Scope,convert(varchar(300),MasterPassword) as MasterPassword," +
                "Access_Restrict,MasterHashedPassword,MasterSalt  from StateCollector_Login where User_Name='" + Uname + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();
                string login_ID = dr["login_id"].ToString();
                Session["State_Logid"] = dr["login_id"].ToString();
                string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();
                // string TBpass = txt_password.Text.ToLower().Trim().ToString();
                //if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)

                ///////////////
                string connnectionObj = "FCIConnectionString";
                string[] queryArr = new string[] { "StateCollector_Login", "login_id", Convert.ToString(dr["login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid

                Session["SecurePassword"] = queryArr;
                ////////////

                ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();

                ///Get Master password
                // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                ///////////////////////////////
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    UpdateUserLogin("StateCollector_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }

    // private void DoStateLogin()
    private void DoStateLogin()
    {
        try
        {
            string Uname = DDL_Dist.SelectedItem.Text;
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,User_Name,convert(varchar(300),password) as Password,DepotId,DistrictId,Fname,Lname,Scope,convert(varchar(300)," +
                "MasterPassword) as MasterPassword,Access_Restrict,MasterHashedPassword,MasterSalt  from RegionState_Login  where User_Name='" + Uname + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();
                string login_ID = dr["login_id"].ToString();
                //Session["Region_Logid"] = dr["login_id"].ToString();
                Session["State_Logid"] = dr["login_id"].ToString();

                //if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)
                ///////////////
                string connnectionObj = "FCIConnectionString";
                string[] queryArr = new string[] { "RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                Session["SecurePassword"] = queryArr;
                ////////////

                ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                ///Get Master password
                //  // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                // // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                ///////////////////////////////
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    UpdateUserLogin("RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                }
            }

        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }

    // private void DoDepotLogin()
    private void DoDepotLogin()
    {

        if (ddllogintype.SelectedValue.ToString() == "2" || ddllogintype.SelectedValue.ToString() == "6" || ddllogintype.SelectedValue.ToString() == "7" || ddllogintype.SelectedValue.ToString() == "10" || ddllogintype.SelectedValue.ToString() == "11" || ddllogintype.SelectedValue.ToString() == "3" || ddllogintype.SelectedValue.ToString() == "12" || ddllogintype.SelectedValue.ToString() == "5" || ddllogintype.SelectedValue.ToString() == "13" || ddllogintype.SelectedValue.ToString() == "14" || ddllogintype.SelectedValue.ToString() == "15" || ddllogintype.SelectedValue.ToString() == "16")
        {
            Session["Logintype"] = ddllogintype.SelectedValue.ToString();
            try
            {
                if (Session["salt"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                string Uname = ddl_godown.SelectedItem.Text;
                string Did = DDL_Dist.SelectedValue;
                string strsql = "select A.FirstTimeLogin,A.HashedPassword,A.Salt, A.[login_id],A.Maintain_By,A.PMS_Password,A.Godown_Name,A.[Password],A.[Godown_ID],A.[DistrictId],A.[BranchID],A.DepotId,A.[MasterPassword]," +
        "A.[Access_Restrict],A.GodownTypeId,A.Is_W19_RegID as W_Reg_No_19,A.W_Reg_No as W_Reg_No_18,A.Is_W20_RegID as W_Reg_No,B.Hired_Type,A.MasterHashedPassword,A.MasterSalt " +
        " from Pvt_Warehouse_Login A " +
        " LEFT JOIN tbl_MetaData_GODOWN_2018 B ON A.Godown_Id = B.Godown_ID where A.[Godown_ID]='" + ddl_godown.SelectedValue.ToString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string pwd = dr["Password"].ToString();
                    string pmspwd = dr["PMS_Password"].ToString();
                    string pwdM = dr["MasterPassword"].ToString();
                    string pmspwdM = dr["MasterPassword"].ToString();
                    string login_ID = dr["login_id"].ToString();
                    //Session["Depot_Logid"] = dr["login_id"].ToString();
                    Session["State_StateID"] = "23";


                    //if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)


                    ///////////////
                    string connnectionObj = "FCIConnectionString";
                    string[] queryArr = new string[] { "Pvt_Warehouse_Login", "Login_id", Convert.ToString(dr["Login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["SecurePassword"] = queryArr;
                    ////////////

                    ///// Check FIrst Time Login Start /////
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                    ///Get Master password
                    // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                    // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                    ///////////////////////////////
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        UpdateUserLogin("Pvt_Warehouse_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "";
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured...')", true);

            }

        }
        else
        {
            try
            {

                string Uname = DDL_Depot.SelectedItem.Text;
                string Did = DDL_Dist.SelectedValue;
                string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,User_Name,convert(varchar(300),password) as Password,DepotId,DistrictId,Fname,Lname,Scope,convert(varchar(300)," +
                    "MasterPassword) as MasterPassword,Access_Restrict,BranchID,MasterHashedPassword,MasterSalt  from Storage_Login where User_Name='" + Uname + "' and DistrictId='" + Did + "'";
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

                    ///// Check FIrst Time Login Start /////m
                    bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                    string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                    string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();

                    ///////////////
                    string connnectionObj = "FCIConnectionString";
                    string[] queryArr = new string[] { "Storage_Login", "login_id", Convert.ToString(dr["login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                    Session["SecurePassword"] = queryArr;
                    ////////////

                    ///Get Master password
                    // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                    // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                    ///////////////////////////////
                    bool Existed = false;
                    if (isFirstTimelogin == true)
                    {
                        UpdateUserLogin("Storage_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                    }


                }

            }
            catch (Exception ex)
            {
                lblError.Text = "";
                lblError.Text = ex.Message;
            }

        }
    }
    //private void DoRegionLogin()
    private void DoRegionLogin()
    {
        try
        {
            string Uname = DDL_Dist.SelectedItem.Text;
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,Region_Id,User_Name,convert(varchar(300),password) as Password,DepotId,DistrictId,Fname,Lname,Scope,convert(varchar(300)," +
                "MasterPassword) as MasterPassword,Access_Restrict,MasterHashedPassword,MasterSalt  from RegionState_Login,tbl_MetaData_Region  where User_Name='" + Uname + "' and region='" + Uname + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();

                string login_ID = dr["login_id"].ToString();
                Session["Region_ID"] = dr["Region_ID"].ToString();
                Session["Depot_Logid"] = dr["login_id"].ToString();
                Session["State_StateID"] = "23";

                ///// Check FIrst Time Login Start /////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();



                ///////////////
                string connnectionObj = "FCIConnectionString";
                string[] queryArr = new string[] { "RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                Session["SecurePassword"] = queryArr;
                ////////////
                ///Get Master password
                // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                ///////////////////////////////
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    UpdateUserLogin("RegionState_Login", "login_id", Convert.ToString(dr["login_id"]), connnectionObj);
                }
            }




        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }


    //private void DoDistLogin()
    private void DoDistLogin()
    {
        try
        {
            string Uname = DDL_Dist.SelectedItem.Text;
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,[DistId] as login_id,[LoginType], [DistId] as User_Name,convert(varchar(300)," +
                "[PWD]) as Password,[DistId],[Status],convert(varchar(300),[Mpwd]) as MasterPassword,MasterHashedPassword,MasterSalt  from [DistrictLogin]  " +
                "where [LoginType]='" + ddllogintype.SelectedValue.ToString() + "' and [DistId]='" + DDL_Dist.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string pwd = dr["Password"].ToString();
                string pwdM = dr["MasterPassword"].ToString();

                string login_ID = dr["Did"].ToString();

                Session["Depot_Logid"] = dr["login_id"].ToString();
                Session["State_StateID"] = "23";


                ///////////////

                string connnectionObj = "FCIConnectionString";
                string[] queryArr = new string[] { "DistrictLogin", "Did", Convert.ToString(dr["Did"]), "login.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                Session["SecurePassword"] = queryArr;
                ////////////

                ///// Check FIrst Time Login Start /////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                ///Get Master password
                // string MasterHashedPassword = dr["MasterHashedPassword"] == DBNull.Value ? null : dr["MasterHashedPassword"].ToString();
                // string MasterSalt = dr["MasterSalt"] == DBNull.Value ? null : dr["MasterSalt"].ToString();
                ///////////////////////////////
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    UpdateUserLogin("DistrictLogin", "Did", Convert.ToString(dr["Did"]), connnectionObj);
                }
            }


        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }

    protected void rblLoginType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblLoginType.SelectedValue.ToString() == "1")
        {
            DDL_Depot.AutoPostBack = false;
            GetDist(rblLoginType.SelectedValue);
            getDepot(DDL_Dist.SelectedValue);
            lbl_dist.Visible = true;
            DDL_Dist.Visible = true;
            lbl_dist.Text = "District";
            lbllogintype.Visible = false;
            ddllogintype.Visible = false;
            regionblock.Visible = true;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            lbl_godown.Visible = false;
            ddl_godown.Visible = false;
        }
        else if (rblLoginType.SelectedValue.ToString() == "2")
        {
            DDL_Depot.AutoPostBack = false;
            GetDist(rblLoginType.SelectedValue);
            getDepot("0");
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            lbl_dist.Visible = true;
            DDL_Dist.Visible = true;
            lbl_dist.Text = "Region";
            lbllogintype.Visible = false;
            ddllogintype.Visible = false;
            regionblock.Visible = false;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            lbl_godown.Visible = false;
            ddl_godown.Visible = false;
        }
        else if (rblLoginType.SelectedValue.ToString() == "3")
        {
            DDL_Depot.AutoPostBack = false;
            GetDist(rblLoginType.SelectedValue);
            getDepot("0");
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            lbl_dist.Visible = true;
            DDL_Dist.Visible = true;
            lbl_dist.Text = "LoginType";
            lbllogintype.Visible = false;
            ddllogintype.Visible = false;
            regionblock.Visible = false;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            lbl_godown.Visible = false;
            ddl_godown.Visible = false;
        }
        if (rblLoginType.SelectedValue.ToString() == "4")
        {
            DDL_Depot.AutoPostBack = false;
            GetDist(rblLoginType.SelectedValue);
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            lbl_dist.Visible = true;
            DDL_Dist.Visible = true;
            lbl_dist.Text = "District";
            lbllogintype.Visible = false;
            ddllogintype.Visible = false;
            regionblock.Visible = false;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            lbl_godown.Visible = false;
            ddl_godown.Visible = false;
        }
        if (rblLoginType.SelectedValue.ToString() == "5")
        {
            DDL_Depot.AutoPostBack = false;
            lbl_dist.Visible = false;
            DDL_Dist.Visible = false;
            DDL_Depot.Visible = false;
            lbl_Depot.Visible = false;
            lbllogintype.Visible = false;
            ddllogintype.Visible = false;
            regionblock.Visible = false;
            RadioButton1.Visible = false;
            RadioButton2.Visible = false;
            lbl_godown.Visible = false;
            ddl_godown.Visible = false;
        }
        if (rblLoginType.SelectedValue.ToString() == "6")
        {
            DDL_Depot.AutoPostBack = true;
            GetDist(rblLoginType.SelectedValue);
            getotherDepot(DDL_Dist.SelectedValue);
            lbl_dist.Visible = true;
            DDL_Dist.Visible = true;
            lbl_dist.Text = "District";
            lbllogintype.Visible = true;
            ddllogintype.Visible = true;
            regionblock.Visible = false;
            RadioButton1.Visible = true;
            RadioButton2.Visible = true;
            lbl_godown.Visible = true;
            ddl_godown.Visible = true;
        }
    }
    private void getlogintype()
    {
        try
        {
            //string str = "SELECT * FROM [Storage_Agency_type] where Storage_Agency != 'MPWLC'";
            string str = "SELECT * FROM [Storage_Agency_type] where Storage_Agency not in ('MPWLC')";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
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
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }
    private void getDepot(string distId)
    {
        try
        {
            string str = "SELECT [login_id], [User_Name],mbi.BranchTypeID FROM [Storage_Login] as sl inner join [MetaDataBranchWithIssueCenter] as mbi on sl.BranchID=mbi.BranchID WHERE sl.[DistrictId] = '" + distId.ToString() + "' and Scope=1 and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by [User_Name]";
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
                Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
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
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }

    private void getotherDepot(string distId)
    {
        try
        {
            if (ddllogintype.SelectedValue.ToString() == "2" || ddllogintype.SelectedValue.ToString() == "6" || ddllogintype.SelectedValue.ToString() == "7" || ddllogintype.SelectedValue.ToString() == "9" || ddllogintype.SelectedValue.ToString() == "10" || ddllogintype.SelectedValue.ToString() == "11" || ddllogintype.SelectedValue.ToString() == "3" || ddllogintype.SelectedValue.ToString() == "12" || ddllogintype.SelectedValue.ToString() == "5" || ddllogintype.SelectedValue.ToString() == "13" || ddllogintype.SelectedValue.ToString() == "14" || ddllogintype.SelectedValue.ToString() == "15" || ddllogintype.SelectedValue.ToString() == "16")
            {
                string str = "SELECT mbi.[BranchID] as [login_id], mbi.[BranchName] as[User_Name],mbi.BranchTypeID from [MetaDataBranchWithIssueCenter] as mbi  WHERE mbi.[DistrictId] = '" + distId.ToString() + "'  and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by [User_Name]";
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
                    Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
                }
                else
                {
                    DDL_Depot.Items.Clear();
                }
                lbl_Depot.Visible = true;
                DDL_Depot.Visible = true;
            }
            else
            {
                string str = "SELECT [login_id],mbi.BranchTypeID, [User_Name] FROM [Storage_Login] as sl inner join [MetaDataBranchWithIssueCenter] as mbi on sl.BranchID=mbi.BranchID WHERE sl.[DistrictId] = '" + distId.ToString() + "' and Scope=1 and mbi.BranchTypeID='" + ddllogintype.SelectedValue.ToString() + "' order by [User_Name]";
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
                    Session["BranchType"] = ds.Tables[0].Rows[0]["BranchTypeID"].ToString();
                }
                else
                {
                    DDL_Depot.Items.Clear();
                }
                lbl_Depot.Visible = true;
                DDL_Depot.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
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
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrict();
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        lbl_Depot.Visible = true;
        DDL_Depot.Visible = true;
        lbl_godown.Visible = true;
        ddl_godown.Visible = true;
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        lbl_Depot.Visible = false;
        DDL_Depot.Visible = false;
        lbl_godown.Visible = false;
        ddl_godown.Visible = false;
    }
    protected void ddllogintype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_godown.Items.Clear();
        if (ddllogintype.SelectedValue.ToString() == "2" || ddllogintype.SelectedValue.ToString() == "6" || ddllogintype.SelectedValue.ToString() == "7" || ddllogintype.SelectedValue.ToString() == "9" || ddllogintype.SelectedValue.ToString() == "10" || ddllogintype.SelectedValue.ToString() == "11" || ddllogintype.SelectedValue.ToString() == "3" || ddllogintype.SelectedValue.ToString() == "12" || ddllogintype.SelectedValue.ToString() == "5" || ddllogintype.SelectedValue.ToString() == "13" || ddllogintype.SelectedValue.ToString() == "14" || ddllogintype.SelectedValue.ToString() == "15" || ddllogintype.SelectedValue.ToString() == "16")
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
    protected void DDL_Depot_SelectedIndexChanged(object sender, EventArgs e)
    {
        string hiredtype = "";
        string str = "";
        if (ddllogintype.SelectedValue.ToString() == "14")
        {
            //hiredtype = "14";
            str = "select Godown_Name,Godown_Id from Pvt_Warehouse_Login where BranchID='" + DDL_Depot.SelectedValue.ToString() + "' and Active='Y' and Maintain_By='0'";
        }
        else
        {
            str = "select Godown_Name,Godown_Id from Pvt_Warehouse_Login where BranchID='" + DDL_Depot.SelectedValue.ToString() + "' and Active='Y' and GodownTypeId='" + ddllogintype.SelectedValue.ToString() + "'";
            //hiredtype = ddllogintype.SelectedValue;
        }
        //string str = "select USER_NAME,Godown_Id from Private_Godown_Login where BranchID='" + DDL_Depot.SelectedValue.ToString() + "' and Active='Y' and GodownTypeId='" + ddllogintype.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
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
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["GodownID_New"] = ddl_godown.SelectedValue.ToString();
    }
    public void UpdateUserLogin(string tableName, string loginId, string loginIdValue, string connectionString)
    {
        // 1. Define your connection string (keep this in a config file usually)
        // string connectionString = "Your_Connection_String_Here";
        string con = ConfigurationManager.ConnectionStrings[connectionString].ToString();
        // 2. Construct the SQL using parameters (@tags) instead of raw variables
        string query = "UPDATE " + tableName + " SET FirstTimeLogin = NULL WHERE " + loginId + " = @id";

        using (SqlConnection connection = new SqlConnection(con))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // 3. Map parameters to values and define data types
                // command.Parameters.Add("@firstLogin", SqlDbType.DateTime).Value = firstTimeLogin;
                command.Parameters.Add("@id", SqlDbType.NVarChar).Value = loginIdValue;

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + rowsAffected + " row(s) updated successfully.')", true);

                }
                catch (SqlException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Database error: " + ex.Message + "')", true);
                    // Handle potential database errors
                    Console.WriteLine("Database error: " + ex.Message);
                }
            }
        }
    }
}








