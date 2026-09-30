using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Data;
using System.Web.Security;

/// <summary>
/// Summary description for Login_Check
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Login_Check : System.Web.Services.WebService {
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    private SqlCommand cmd = new SqlCommand();

    private SqlCommand cmd1 = new SqlCommand();

    private SqlDataAdapter dataAdapter;
    private DataSet dataset;
    private SqlTransaction trans = null;
    private SqlCommand commandt = null;
    string Query = "";
    static string saltValue = "s@!tVal*e";
    static string hashAlgorithm = "SHA1";
    static int passwordIterations = 2;
    static string initVector = "@1B2c3!@)#$%@#X6g7FG";
    static int keySize = 256;
    public Login_Check () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public static string Encrypt(string plainText, string passPhrase)
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

    public static string Decrypt(string cipherText, string passPhrase)
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
    [WebMethod(Description = "This Method Is Used to Validate Credential")]
    public string Check_Credential_Perm(string UserName,string Password, string Login_Type,string Credential)
    {
        //int saltSize = 5;
        //string salt = "";
        //salt = CreateSalt(saltSize);
        //Session["salt"] = salt.ToString();
        string Credentiall = "WLC2020DSCNicv38";
        try
        {
            //if (Credentiall == Credential)
                string Permission = "N";
                string UserIdW = UserName.ToString();
                string PassW = Password.ToString();
                string LoginTypeW = Login_Type.ToString();
                //
                string epwd;
                string dpwd1;
                string dpwd = "";
                //if (Session["salt"] != null)
                //{
                epwd = Encrypt(PassW, "password");
                dpwd1 = Decrypt(epwd, "password");
                dpwd = dpwd1.ToLower().ToString();
                //}
                //
                string query = "";
                if (LoginTypeW == "B")
                {
                    query = "Select login_id,User_Name,Password,DistrictId,Fname,Lname,Scope,Access_Restrict,BranchID as User_Id   from Storage_Login where BranchID='" + UserIdW + "'";
                }
                else if (LoginTypeW == "G")
                {
                    query = "select [login_id],Godown_Name as User_Name,[Password],[Godown_ID] as User_Id,[DistrictId],[BranchID],[Access_Restrict],GodownTypeId from Pvt_Warehouse_Login where [Godown_ID]='" + UserIdW + "'";
                }
                else if (LoginTypeW == "R")
                {
                    //query = "select login_id,User_Name,Password,login_id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login where login_id='" + UserIdW + "'";
                    //query = "select login_id,User_Name,Password,MR.Region_Id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login as R inner join  tbl_MetaData_Region as MR on MR.region=R.User_Name where MR.Region_Id='" + UserIdW + "'";
                    //query = "select User_Name,Password,login_id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login as R inner join  tbl_MetaData_Region as MR on MR.region=R.User_Name where login_id='" + UserIdW + "'";
                    query = "select User_Name,Password,MR.Region_Id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login as R inner join  tbl_MetaData_Region as MR on MR.region=R.User_Name where MR.Region_Id='" + UserIdW + "'";

                }
                else if (LoginTypeW == "M")
                {
                    //query = "select login_id,User_Name,Password,login_id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login where login_id='" + UserIdW + "'";
                    //query = "select User_Name,Lname as Password,login_id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login as R inner join  tbl_MetaData_Region as MR on MR.region=R.User_Name where login_id='" + UserIdW + "'";
                    query = "select User_Name,Lname as Password,MR.Region_Id as User_Id,'' as DistrictId,'' as BranchID from RegionState_Login as R inner join  tbl_MetaData_Region as MR on MR.region=R.User_Name where MR.Region_Id='" + UserIdW + "'";

                }
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string pwd = dr["Password"].ToString();
                    string login_ID = dr["User_Id"].ToString();
                    string User_Name = dr["User_Name"].ToString();
                    string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                    //string Region_Id = dr["Region_Id"].ToString();
                    //string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();
                    //if (dpwd == pwd)
                    string TBpass = PassW;
                    if (PassW == pwd && Credentiall == Credential)
                    //if (dpwd == hpwd)
                    {
                        Permission = User_Name;
                    }
                }
                return Permission;
        }

        catch (Exception)
        {

            throw;

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
    
}

