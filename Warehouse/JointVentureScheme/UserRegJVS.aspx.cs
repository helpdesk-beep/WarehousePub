using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
public partial class JointVentureScheme_UserRegJVS : System.Web.UI.Page
{
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet ds3 = new DataSet();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    public string GenerateOTP = "", OTPSMS = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //CalendarExtender1.EndDate = DateTime.Now;  

        if (!IsPostBack)
        {
            get_Applicant_Type();
            get_State();
            get_Districts();
            FillCapctha();
        }
    }
    void FillCapctha()
    {
        try
        {
            txtCaptcha.Text = "";
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Session["captcha"] = captcha.ToString();

            imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
        }
        catch
        {
            throw;
        }
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual

        DateTime _effective_date = Convert.ToDateTime("02/20/2021 11:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("07/10/2030 11:59:00 PM");

        ////Old
        //   DateTime _effective_date = Convert.ToDateTime("2019-01-10 17:34:14.140");
        //   DateTime _Closing_date = Convert.ToDateTime("2019-07-29 17:34:14.140");

        string S = "";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con);
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();

        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            //ServerDate = ServerDate.AddMinutes(-4);
            ServerDate = ServerDate.AddMinutes(-2);
        }
        if (ServerDate < _effective_date)
        {
            S = "NS";
        }
        else if (ServerDate > _Closing_date)
        {
            S = "NE";
        }
        else
        {
            S = "Y";
        }
        return S;
    }
    private string GenerateHash(string password, out string saltHex)
    {
        byte[] saltBytes = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        saltHex = BitConverter.ToString(saltBytes).Replace("-", "");

        using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
        {
            byte[] hash = pbkdf2.GetBytes(20);
            return Convert.ToBase64String(hash);
        }
    }
    protected void btnreg_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();

        if (TStatus == "Y")
        {
            if (ddlAppType.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Type of Applicant...'); </script> ");
                ddlAppType.Focus();
            }
            else if (ddlState.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select State...'); </script> ");
                ddlState.Focus();
            }
            else if (ddlDistrict.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select District of Residence...'); </script> ");
                ddlDistrict.Focus();
            }
            //else if (txtDOB.Text == "")
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Date ...'); </script> ");
            //    txtDOB.Focus();
            //}
            else if (txtAuthPerson.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Authorised Person...'); </script> ");
                txtAuthPerson.Focus();
            }
            else if (txtAadharNo.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Aadhar No...'); </script> ");
                txtAuthPerson.Focus();
            }
            else if (txtPAN.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Valid PAN No...'); </script> ");
                txtAuthPerson.Focus();
            }
            else if (txtREgemail.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Email...'); </script> ");
                txtREgemail.Focus();
            }
            else if (txtmobile.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Mobile No...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtmobile.Value.Length != 10)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Correct Mobile No...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtregpwd.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Password...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtregconpwd.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Confirm Password...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtregpwd.Value != txtregconpwd.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Password Does Not Match...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtAadharNo.Value.Length != 12)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Correct Aadhar No...'); </script> ");
                txtmobile.Focus();
            }
            else if (txtPAN.Value.Length != 10)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Correct PAN No...'); </script> ");
                txtmobile.Focus();
            }
            else
            {

                string strsql = "select EmailID from tbl_Warehouse_PreReg where EmailID='" + txtREgemail.Value + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यह  Email Id पहले से register है कृपया दूसरी Email Id use करें...'); </script> ");
                    txtREgemail.Focus();
                }
                else
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string salt;
                    string hashedPassword = GenerateHash(txtregpwd.Value, out salt);

                    qry = "INSERT INTO [tbl_Warehouse_PreReg]([StateID],[DistrictID],[ApplicantType],[WarehouseType],[Auth_Person],[EmailID],[MobileNo],[DOB],[Password],[CreatedBy],[CreatedDate],[IsActive],Aadhar_No,PAN_No,Salt,HashedPassword,FirstTimeLogin) " +
                        "VALUES ('23','" + ddlDistrict.SelectedValue + "','" + ddlAppType.SelectedValue + "','" + ddlWhrType.SelectedValue + "','" + txtAuthPerson.Value + "','" + txtREgemail.Value + "','" + txtmobile.Value + "',null,'" + txtregpwd.Value + "','" + ClientIP + "',getdate(),'Y','" + txtAadharNo.Value + "','" + txtPAN.Value + "','" + salt + "','" + hashedPassword + "',1)";
                    cmd = new SqlCommand(qry, con);
                    int c = cmd.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    if (c > 0)
                    {
                        Session["login"] = "true";
                        Session["fname"] = txtAuthPerson.Value;
                        Session["lname"] = "";
                        Session["mname"] = "";
                        Session["email"] = txtREgemail.Value;
                        Session["mobile"] = txtmobile.Value;
                        Session["DOB"] = null;
                        Session["AppType"] = ddlAppType.SelectedItem.Text;
                        Session["District"] = ddlDistrict.SelectedItem.Text;
                        Session["Reg_No"] = null;
                        Response.Redirect("WarehouseHome.aspx");
                    }
                }
            }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 20/02/2021 11:59:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
        }
    }
    private void DoDepotLogin()
    {
        try
        {
            string Uname = txtLogemail.Value;
            string pwd = txtlogpwd.Value;
            //string strsql = "SELECT  [Tid],[Auth_Person],[MobileNo],[EmailID],CONVERT(varchar(10),DOB,103) as DOB,[Password],[CreatedDate],[CreatedBy] FROM [tbl_Warehouse_PreReg] where [EmailID]='" + Uname.ToString() + "' and [Password]='" + pwd.ToString() + "'";
            string strsql = "SELECT wp.Salt,wp.HashedPassword,wp.FirstTimeLogin,[Tid],[Auth_Person],[MobileNo],[EmailID],CONVERT(varchar(10),DOB,103) as DOB,[Password]," +
                "AT.Applicant_Type,dt.District_Name,WP.Reg_No FROM [tbl_Warehouse_PreReg] as WP " +
                "inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType " +
                "inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID where [EmailID]='" + Uname.ToString() + "' and [Password]='" + pwd.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //string spwd = dr["Password"].ToString();
                //string pwdM = dr["MasterPassword"].ToString();
                //string login_ID = dr["login_id"].ToString();

                //string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                //string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();
                //string TBpass = txt_password.Text.ToLower().Trim().ToString();
                //if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)
                //{
                Session["login"] = "true";
                Session["fname"] = dr["Auth_Person"].ToString();
                Session["lname"] = "";
                Session["mname"] = "";
                Session["email"] = dr["EmailID"].ToString();
                Session["mobile"] = dr["MobileNo"].ToString();
                Session["DOB"] = dr["DOB"].ToString();
                Session["AppType"] = dr["Applicant_Type"].ToString();
                Session["District"] = dr["District_Name"].ToString();
                Session["Reg_No"] = dr["Reg_No"].ToString();
                string em = dr["EmailID"].ToString();


               
               
                ///////////////
                string connnectionObj = "JVSGodownConString";
                string[] queryArr = new string[] { "tbl_Warehouse_PreReg", "TID", Convert.ToString(dr["TID"]), "JointVentureScheme/UserReg.aspx", connnectionObj }; ///Array Generated for tablename , userid column, value of Userid
                Session["Securelogin"] = queryArr;
                ///////////////

                ///// Check FIrst Time Login Start for Secured Password Generation /// New Login Date 22 Dec 2025/////
                bool isFirstTimelogin = dr["FirstTimeLogin"] == DBNull.Value ? false : true;
                string HashedPassword = dr["HashedPassword"] == DBNull.Value ? null : dr["HashedPassword"].ToString();
                string Salt = dr["Salt"] == DBNull.Value ? null : dr["Salt"].ToString();
                bool Existed = false;
                if (isFirstTimelogin == true)
                {
                    Existed = SecurityPolicy.VerifyPassword(hdnActualPassword.Value, HashedPassword, Salt); ///Varify Password after login...
                }
                else
                {
                    Response.Redirect("~/ChangePassword.aspx", true);
                }
                ///// Check FIrst Time Login End ///// 

                if (Existed)
                {
                    if (em == "admin@mpwlc.com")
                    {
                        Response.Redirect("AdminPanel.aspx");
                    }
                    else
                    {
                        //checkifreg();
                        Response.Redirect("WarehouseHome.aspx");
                    }
                }
            }
            else
            {
                txtCaptcha.Text = "";
                FillCapctha();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका ई-मेल या पासवर्ड गलत हो सकता है')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }
    private void checkifreg()
    {
        try
        {
            string Uname = txtLogemail.Value;
            string pwd = txtlogpwd.Value;
            string strsql = "select[TAID], [Email] FROM [Tribal_Godown].[dbo].[Tbl_TribalReg] where Email='" + txtLogemail.Value + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["App_ID"] = ds.Tables[0].Rows[0]["TAID"].ToString(); ;
                Response.Redirect("PrintReg.aspx");
            }
            else
            {

                Response.Redirect("Neeti.aspx");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        {

            if (txtLogemail.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Email दर्ज करे...'); </script> ");
                txtLogemail.Focus();
            }
            else if (txtlogpwd.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Password दर्ज करे...'); </script> ");
                txtlogpwd.Focus();
            }
            else
            {
                if (Session["captcha"].ToString() != txtCaptcha.Text)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Captcha Code'); </script> ");
                    FillCapctha();
                }
                else if (Session["captcha"].ToString() == txtCaptcha.Text)
                {
                    DoDepotLogin();
                }
            }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register/Login before 20/02/2021 11:59:00 AM'); </script> ");
        }
        //else if (TStatus == "NE")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for online Application under Tribal Area has been closed..!'); </script> ");
        //}        
    }
    protected void btngetpwd_Click(object sender, EventArgs e)
    {

    }
    public void get_Applicant_Type()
    {
        string qry = "SELECT [Applicant_TypeId],[Applicant_Type] FROM [tbl_Metadata_ApplicantType]";
        //string qry = "SELECT [Applicant_TypeId],[Applicant_Type] FROM [tbl_Metadata_ApplicantType] where Applicant_TypeId='2'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlAppType.DataSource = ds1.Tables[0];
            ddlAppType.DataTextField = "Applicant_Type";
            ddlAppType.DataValueField = "Applicant_TypeId";
            ddlAppType.DataBind();
            ddlAppType.Items.Insert(0, "--Select--");
        }

    }
    public void get_Warehouse_Type()
    {
        string qry = "SELECT [Warehouse_TypeId],[Warehouse_Type],[Applicant_TypeId] FROM [tbl_Metadata_GovWarehouseType] where [Applicant_TypeId]='" + ddlAppType.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlWhrType.DataSource = ds1.Tables[0];
            ddlWhrType.DataTextField = "Warehouse_Type";
            ddlWhrType.DataValueField = "Warehouse_TypeId";
            ddlWhrType.DataBind();
            ddlWhrType.Items.Insert(0, "--Select--");
            trWT.Visible = true;
        }
        else
        {
            trWT.Visible = false;

        }

    }
    public void get_State()
    {
        string qry = "select State_Code,State_Name from [tbl_Metadata_State] order by State_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds2);
        if (ds2 == null)
        {
        }
        else
        {

            ddlState.DataSource = ds2.Tables[0];
            ddlState.DataTextField = "State_Name";
            ddlState.DataValueField = "State_Code";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "--Select--");
            ddlState.SelectedIndex = 20;

        }
    }
    public void get_Districts()
    {
        ddlDistrict.Items.Clear();
        string qry2 = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where State_Id='23' order by District_Name";
        SqlCommand cmd2 = new SqlCommand(qry2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        da2.Fill(ds3);
        if (ds3 == null)
        {
        }
        else
        {
            ddlDistrict.DataSource = ds3.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlAppType_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Warehouse_Type();

    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/2000";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            // converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");
            return converted;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //get_Districts();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        FillCapctha();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string mobileNo = "";
            string strsql = "select MobileNo,PreReg.EmailID,Auth_Person,Reg_No from tbl_Warehouse_PreReg as PreReg where PreReg.EmailID='" + txtForgotPassword.Value + "' or Reg_No='" + txtForgotPassword.Value + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                mobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                lblmb.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('मोबाइल नंबर " + mobileNo + "  पर पासवर्ड भेज दिया गया हे कृप्या चेक करें |')", true);
                txtForgotPassword.Value = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृप्या सही रजिस्ट्रेशन नंबर या रजिस्टर्ड ईमेल आईडी दर्ज करें |')", true);
                txtForgotPassword.Value = "";
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
    }
    protected void GenerateUniqueOTP()
    {
        // string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";
        string characters = numbers;
        characters += numbers;
        string MONumber = txtForgotPassword.Value;
        int lastdigit = int.Parse(MONumber.Substring(6));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        //  OTPSMS = "'" + ddl_commodity.SelectedItem.Text + "' Depositor Form Number " + ddl_depositerform.SelectedItem.Text + " OTP Is '" + otp + "'";
        OTPSMS = " OTP Is ' " + otp + " '";
        string hdfOTP = "";
        hdfOTP = otp;
        SMS Message = new SMS();
        string MobileNo = "";
        //   Message.SendSMS(lblmb.Text, OTPSMS);

        //  lblotpdisplaytxt.Text = OTPSMS;
    }
    protected void btngetpass_Click(object sender, EventArgs e)
    {
        Response.Redirect("../JointVentureScheme/Get_Password_Jvslogin_For_Public.aspx");
    }
}

