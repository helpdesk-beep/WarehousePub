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

public partial class ResetLoginForBussiness : System.Web.UI.Page
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
        btn_login.Attributes.Add("onkeypress", "return LoginOnEnter(this);");
        if (!IsPostBack)
        {
            int saltSize = 5;
            string salt = "";
            salt = CreateSalt(saltSize);
            Session["salt"] = salt.ToString();
            getlogintype();
        }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblLoginType.SelectedValue == "3")
            {
                DoStateLogin();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "";
            lblError.Text = ex.Message;
        }
    }
    private void DoStateLogin()
    {
        try
        {
            //string Uname = DDL_Dist.SelectedItem.Text;
            string strsql = "Select Salt,HashedPassword,FirstTimeLogin,login_id,User_Name,convert(varchar(300),password) as Password,DepotId,DistrictId,Fname,Lname,Scope,convert(varchar(300)," +
                "MasterPassword) as MasterPassword,Access_Restrict,MasterHashedPassword,MasterSalt  from RegionState_Login  where User_Name='Business(MPWLC)'";
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
    protected void rblLoginType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblLoginType.SelectedValue.ToString() == "3")
        {



            lbllogintype.Visible = true;
            ddllogintype.Visible = true;

        }
        if (rblLoginType.SelectedValue.ToString() == "4")
        {

            lbllogintype.Visible = false;
            ddllogintype.Visible = false;

        }
        if (rblLoginType.SelectedValue.ToString() == "5")
        {

            lbllogintype.Visible = false;
            ddllogintype.Visible = false;

        }
        if (rblLoginType.SelectedValue.ToString() == "6")
        {


            lbllogintype.Visible = true;
            ddllogintype.Visible = true;

        }
    }
    private void getlogintype()
    {
        try
        {
            //string str = "SELECT * FROM [Storage_Agency_type] where Storage_Agency != 'MPWLC'";
            string str = "SELECT  [User_Name] ,[login_id] FROM [RegionState_Login] WHERE Scope=3 AND login_id=500 order by Srno";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllogintype.DataSource = ds.Tables[0];
                ddllogintype.DataTextField = "User_Name";
                ddllogintype.DataValueField = "login_id";
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








