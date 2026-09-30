using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
public partial class TribalGodown_Registration : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    string App_No = "";
    int BID = 0;
    SqlCommand cmd = null;
   // SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //////////Expire
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        /////////
        try
        {
            string sess = Session["Agry"].ToString();
            if (sess == "agry")
            {
              
                //if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0)
                //    FileUploadComplete();//
                lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
                txtfname.Value = Session["fname"].ToString();
                txtmname.Value = Session["mname"].ToString();
                txtlname.Value = Session["lname"].ToString();
                txtemail.Value = Session["email"].ToString();
                txtmobile.Text = Session["mobile"].ToString();

                if (!IsPostBack)
                {
                    get_TDistricts();
                    get_CPDistricts();
                    //get_CDistricts();
                    //fillBankList();
                    get_Edu_Type();
                    //ddlEduc2.Items.Add(new ListItem("--Select--", "0"));
                }
            }
            else
            {
                Response.Redirect("UserReg.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();

        //Test
        //DateTime _effective_date = Convert.ToDateTime("10/30/2017 11:30:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("11/15/2017 05:00:00 PM");
        //Actual
        DateTime _effective_date = Convert.ToDateTime("04/05/2021 11:30:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("04/26/2021 05:00:00 PM");
        //old
        //DateTime _effective_date = Convert.ToDateTime("06/27/2017 11:30:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("07/17/2017 05:00:00 PM");

        string S = "";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();
        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            ServerDate = ServerDate.AddMinutes(-0);
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string TStatus=Tcheckdatetimes();
        if (TStatus == "Y")
        {
        if (txtfname.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया प्रथम नाम दर्ज करे...'); </script> ");
            txtfname.Focus();
        }
        else if (txtlname.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया सरनेम दर्ज करे...'); </script> ");
            txtlname.Focus();
        }
        else if (txtMotherName.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया माता का नाम दर्ज करे...'); </script> ");
            txtMotherName.Focus();
        }
        else if (txtFatherName.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया पिता का नाम दर्ज करे...'); </script> ");
            txtFatherName.Focus();
        }
        else if (txtmobile.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया मोबाइल नंबर दर्ज करे...'); </script> ");
            txtmobile.Focus();
        }
        else if (txtDOB.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया जन्मतिथि दर्ज करे...'); </script> ");
        }
        else if (txtFIncome.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया कुल परिवार की आय दर्ज करे...'); </script> ");
            txtFIncome.Focus();
        }
        else if (txtFMember.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया कुल परिवार के सदस्यों की संख्या दर्ज करे...'); </script> ");
            txtFMember.Focus();
        }
        else if (ddlEmp.SelectedItem.Text=="--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया व्यवसाय का चयन करे...'); </script> ");
            
        }
        else if (txtTPV.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया कुल परियोजना लागत दर्ज करे...'); </script> ");
            txtTPV.Focus();
        }
        else if (txtRKN.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया रोजगार कार्यालय नंबर दर्ज करे...'); </script> ");
            txtRKN.Focus();
        }
        else if (txtSOI.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वित्त का स्रोत दर्ज करे...'); </script> ");
            txtSOI.Focus();
        }
        else if (txtCaddress.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वर्तमान पता दर्ज करे...'); </script> ");
            txtCaddress.Focus();
        }
        else if (txtPaddress.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया स्थाई पता दर्ज करे...'); </script> ");
        }
        else if (txtCPIN.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वर्तमान पिन कोड दर्ज करे...'); </script> ");
            txtCPIN.Focus();
        }
        else if (txtPPIN.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया स्थाई पिन कोड दर्ज करे...'); </script> ");
            txtPPIN.Focus();
        }
        else if (ddlCDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वर्तमान जिला चुने...'); </script> ");
        }
        else if (ddlPDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया स्थाई जिला चुने...'); </script> ");
        }
        else if (ddlCBlock.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वर्तमान खंड चुने...'); </script> ");
        }
        else if (ddlPBlock.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया स्थाई खंड चुने...'); </script> ");
        }
        else if (txtVID.Value=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया परिचय पत्र नंबर दर्ज करे...'); </script> ");
        }
        else if (txtAno.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया आधार कार्ड नंबर दर्ज करे...'); </script> ");
        }
        else if (txtPAN.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया पैन कार्ड नंबर दर्ज करे...'); </script> ");
        }
        else if (ddlDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वेयरहाउस संचालन जिला चुने...'); </script> ");
        }
        else if (ddlBlock.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वेयरहाउस संचालन खंड चुने...'); </script> ");
        }
        else if (txtWAddr.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया वेयरहाउस संचालन पता दर्ज करे...'); </script> ");
            txtWAddr.Focus();
        }
        else if (txtDFTO.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया मुख्यालय से दूरी दर्ज करे...'); </script> ");
            txtDFTO.Focus();
        }
        else if (txtQofForm.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया भूमि की मात्रा दर्ज करे...'); </script> ");
            txtQofForm.Focus();
        }
        else if (ddlBank.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया बैंक का नाम चुने...'); </script> ");
        }
        else if (txtAccNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया अकाउंट नंबर दर्ज करे...'); </script> ");
            txtAccNo.Focus();
        }
        else if (txtIFSC.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया IFSC कोड दर्ज करे...'); </script> ");
            txtIFSC.Focus();
        }
        else if (ddlEmp.SelectedItem.Text != "--Select--" && ddlEmp.SelectedItem.Text != "Unemployment")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You are not Eligible to Fill this Form...'); </script> ");
        }
        else if (txtP1.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 1 व्यक्ति का नाम दर्ज करे...'); </script> ");
        }
        else if (txtP1Add.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 1 व्यक्ति का पता दर्ज करे...'); </script> ");
        }
        else if (txtP1Mob.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 1 व्यक्ति का मोबाइल नं दर्ज करे...'); </script> ");
        }
        else if (txtP1Rel.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 1 व्यक्ति संबंध दर्ज करे...'); </script> ");
        }
        else if (txtP2.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 2 व्यक्ति का नाम दर्ज करे...'); </script> ");
        }
        else if (txtP2Add.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 2 व्यक्ति का पता दर्ज करे...'); </script> ");
        }
        else if (txtP2Mob.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 2 व्यक्ति का मोबाइल नं दर्ज करे...'); </script> ");
        }
        else if (txtP2Rel.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया 2 व्यक्ति संबंध दर्ज करे...'); </script> ");
        }
        else if (fileuploadimage.PostedFile.ContentLength == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('कृपया फोटो चुने....!');</script>");
        }
        else if (fileuploadDoc.PostedFile.ContentLength == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('कृपया रोजगार कार्यालय पंजीयन चुने....!');</script>");
        }
        else if (fileuploadCert.PostedFile.ContentLength == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('कृपया शैक्षणिक प्रमाण पत्र चुने....!');</script>");
        }
        else if (fileuploadCast.PostedFile.ContentLength == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript>alert('कृपया जाती प्रमाण पत्र चुने....!');</script>");
        }
        else if (CheckBox1.Checked==false)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('घोसना के चेक बॉक्स को क्लिक करे...'); </script> ");
        }
        else if (CheckBox2.Checked == false)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('घोसना के चेक बॉक्स को क्लिक करे...'); </script> ");
        }
        else
        {
            string CHKV = "";
            CHKV = Check_Image();
            if (CHKV == "Y")
            {
                GetApplicationNo();
                Insert_Registration_Detail();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया फोटो या डॉकयुमेंट jpg/png/gif फ़ारमैट मे चुने...'); </script> ");
            }
        }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 05/04/2021 11:30:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for online Application under Tribal Area has been closed..!'); </script> ");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public string Check_Image()
    {
        string CHK = "Y";
        string fileName = fileuploadimage.PostedFile.FileName;
        string Extension = Path.GetExtension(fileuploadimage.PostedFile.FileName);

        string fileName2 = fileuploadDoc.PostedFile.FileName;
        string Extension2 = Path.GetExtension(fileuploadDoc.PostedFile.FileName);

        string fileName3 = fileuploadCert.PostedFile.FileName;
        string Extension3 = Path.GetExtension(fileuploadCert.PostedFile.FileName);

        string fileName4 = fileuploadCast.PostedFile.FileName;
        string Extension4 = Path.GetExtension(fileuploadCast.PostedFile.FileName);

        if ((Extension == ".jpg" || Extension == ".png" || Extension == ".gif") && (Extension2 == ".jpg" || Extension2 == ".png" || Extension2 == ".gif") && (Extension3 == ".jpg" || Extension3 == ".png" || Extension3 == ".gif") && (Extension4 == ".jpg" || Extension4 == ".png" || Extension4 == ".gif"))
        {
            CHK = "Y";
        }
        else
        {
            CHK = "N";
        }
        return CHK;
    }
    public void get_TDistricts()
    {
        string qry = "SELECT District_Name_HI,District_Id FROM tbl_MetaData_DISTRICT WHERE remarks='T' order by District_Name_HI";
        SqlCommand cmd = new SqlCommand(qry,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {

            ddlCDistrict.DataSource = ds1.Tables[0];
            ddlCDistrict.DataTextField = "District_Name_HI";
            ddlCDistrict.DataValueField = "District_Id";
            ddlCDistrict.DataBind();
            ddlCDistrict.Items.Insert(0, "--Select--");
        }

    }
    public void get_CPDistricts()
    {
        string qry = "SELECT District_Name_HI,District_Id FROM tbl_MetaData_DISTRICT order by District_Name_HI";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            //ddlCDistrict.DataSource = ds1.Tables[0];
            //ddlCDistrict.DataTextField = "District_Name_HI";
            //ddlCDistrict.DataValueField = "District_Id";
            //ddlCDistrict.DataBind();
            //ddlCDistrict.Items.Insert(0, "--Select--");

            ddlPDistrict.DataSource = ds1.Tables[0];
            ddlPDistrict.DataTextField = "District_Name_HI";
            ddlPDistrict.DataValueField = "District_Id";
            ddlPDistrict.DataBind();
            ddlPDistrict.Items.Insert(0, "--Select--");
        }

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBlock.Enabled = true;
        string DistrictId = ddlDistrict.SelectedValue.ToString();
        //string DistrictId = ddlCDistrict.SelectedValue.ToString();
        string qry = "select BlockName_H,BlockID from [tbl_Blocks] where DistrictId='" + DistrictId + "' order by BlockName_H";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            //ddlBlock.DataSource = ds1.Tables[0];
            //ddlBlock.DataTextField = "BlockName_H";
            //ddlBlock.DataValueField = "BlockID";
            //ddlBlock.DataBind();
            //ddlBlock.Items.Insert(0, "--Select--");

            ddlCBlock.DataSource = ds1;
            ddlCBlock.DataTextField = "BlockName_H";
            ddlCBlock.DataValueField = "BlockID";
            ddlCBlock.DataBind();
            ddlCBlock.Items.Insert(0, "--Select--");
        }
    }
   
    public void GetApplicationNo()
    {
        qry = "select max(AID) as BId from Tbl_TribalReg";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = BID + 1;
                //App_No = "042017" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2,2) + SubBN.ToString();
                //App_No = "092019" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + SubBN.ToString();
                App_No = "042021" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + SubBN.ToString();

                BID = SubBN;
            }
            else
            {
                // App_No = "042017" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";
                //App_No = "092019" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";
                App_No = "042021" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";

                BID = 1;
            }
        }
        else
        {
            // App_No = "042017" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";
            //App_No = "082019" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";
            App_No = "042021" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + "1";

            BID = 1;
        }
    }
    public void Insert_Registration_Detail()
    {
        string Sex = "";
        string Education = "";
        string GraduationCategory = "";
        if (rdomale.Checked)
        {
            Sex = "Male";
        }
        else if (rdofemale.Checked)
        {
            Sex = "Female";
        }
        //if (rdoEducate.Checked)
        //{
        //    Education = "Educated";
        //}
        //else if (rdoNonEducate.Checked)
        //{
        //    Education = "NoEducated";
        //}
        if (ddlEduc2.Enabled == false || ddlEduc2.SelectedValue.ToString()=="0")
        {
            GraduationCategory = "0";
            Education = ddlEduc1.SelectedValue.ToString();
        }
        else
        {
            GraduationCategory = ddlEduc2.SelectedValue.ToString();
            Education = ddlEduc1.SelectedValue.ToString();
        }
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        string fileName = fileuploadimage.PostedFile.FileName;
        int fileLength = fileuploadimage.PostedFile.ContentLength;
        byte[] imageBytes = new byte[fileLength];
        fileuploadimage.PostedFile.InputStream.Read(imageBytes, 0, fileLength);
        string StrimageBytes = Convert.ToBase64String(imageBytes);
        string Extension = Path.GetExtension(fileuploadimage.PostedFile.FileName);

        string fileName2 = fileuploadDoc.PostedFile.FileName;
        int fileLength2 = fileuploadDoc.PostedFile.ContentLength;
        byte[] imageBytes2 = new byte[fileLength2];
        fileuploadDoc.PostedFile.InputStream.Read(imageBytes2, 0, fileLength2);
        string StrimageBytes2 = Convert.ToBase64String(imageBytes2);
        string Extension2 = Path.GetExtension(fileuploadDoc.PostedFile.FileName);

        string fileName3 = fileuploadCert.PostedFile.FileName;
        int fileLength3 = fileuploadCert.PostedFile.ContentLength;
        byte[] imageBytes3 = new byte[fileLength3];
        fileuploadCert.PostedFile.InputStream.Read(imageBytes3, 0, fileLength3);
        string StrimageBytes3 = Convert.ToBase64String(imageBytes3);
        string Extension3 = Path.GetExtension(fileuploadCert.PostedFile.FileName);

        string fileName4 = fileuploadCast.PostedFile.FileName;
        int fileLength4 = fileuploadCast.PostedFile.ContentLength;
        byte[] imageBytes4 = new byte[fileLength4];
        fileuploadCast.PostedFile.InputStream.Read(imageBytes4, 0, fileLength4);
        string StrimageBytes4 = Convert.ToBase64String(imageBytes4);
        string Extension4 = Path.GetExtension(fileuploadCast.PostedFile.FileName);

        string BankName = "";
        //if (ddlBank.SelectedItem.Text == "OTHER BANK")
        //{
        //    BankName = txtOBank.Text;
        //}
        //else
        //{
        BankName = ddlBank.SelectedItem.Text;
        //}
        //string sql = "INSERT INTO Tbl_TribalReg([TAID],[FristName],[MName],[LName],[MothersName],[FathersName],[ACaste],[Email],[MobileNo],[DOB],[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],[CurDist],[ParDist],[CurBlock],[ParBlock],[Education],[VoterId],[AdharCard],[PaNNo],[WarDist],[WarBlock],[WarAddress],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate],CreateBy,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],AID,Graduation_Category,EducationPicName,EducationPicType,EducationPic,CastPicName,CastPicType,CastPic) VALUES (@TAID,@FristName,@MName,@LName,@MothersName,@FathersName,@ACaste,@Email,@MobileNo,@DOB,@TtlFmlIncome,@Sex,@TtlFmlMembers,@Occupation,@ProjCost,@Rojgarnum,@Sorcfinc,@CurrAddress,@ParAddress,@CurrPinCode,@ParPinCode,@CurDist,@ParDist,@CurBlock,@ParBlock,@Education,@VoterId,@AdharCard,@PaNNo,@WarDist,@WarBlock,@WarAddress,@BankName,@BankAcct,@IFSC,@FristKpName,@SecKpName,@FristKpAdd,@SecKpAdd,@FirstKpMob,@SecKpMob,@FirstKpEmail,@SecKpEmail,@FirstKpRel,@SecKpRel,@CretaedDate,@CreateBy,@RojgarPicName,@RojgarPicType,@RojgarPic,@AppPicName,@AppPicType,@AppPic,@AID,@Graduation_Category,@EducationPicName,@EducationPicType,@EducationPic,@CastPicName,@CastPicType,@CastPic)";
        string sql = "INSERT INTO Tbl_TribalReg([TAID],[FristName],[MName],[LName],[MothersName],[FathersName],[ACaste],[Email],[MobileNo],[DOB],[TtlFmlIncome],[Sex],[TtlFmlMembers],[Occupation],[ProjCost],[Rojgarnum],[Sorcfinc],[CurrAddress],[ParAddress],[CurrPinCode],[ParPinCode],[CurDist],[ParDist],[CurBlock],[ParBlock],[Education],[VoterId],[AdharCard],[PaNNo],[WarDist],[WarBlock],[WarAddress],[BankName],[BankAcct],[IFSC],[FristKpName],[SecKpName],[FristKpAdd],[SecKpAdd],[FirstKpMob],[SecKpMob],[FirstKpEmail],[SecKpEmail],[FirstKpRel],[SecKpRel],[CretaedDate],CreateBy,[RojgarPicName],[RojgarPicType],[RojgarPic],[AppPicName],[AppPicType],[AppPic],AID,Graduation_Category,EducationPicName,EducationPicType,EducationPic,CastPicName,CastPicType,CastPic,DistFromTO,QuantityOfForm) VALUES (@TAID,@FristName,@MName,@LName,@MothersName,@FathersName,@ACaste,@Email,@MobileNo,@DOB,@TtlFmlIncome,@Sex,@TtlFmlMembers,@Occupation,@ProjCost,@Rojgarnum,@Sorcfinc,@CurrAddress,@ParAddress,@CurrPinCode,@ParPinCode,@CurDist,@ParDist,@CurBlock,@ParBlock,@Education,@VoterId,@AdharCard,@PaNNo,@WarDist,@WarBlock,@WarAddress,@BankName,@BankAcct,@IFSC,@FristKpName,@SecKpName,@FristKpAdd,@SecKpAdd,@FirstKpMob,@SecKpMob,@FirstKpEmail,@SecKpEmail,@FirstKpRel,@SecKpRel,@CretaedDate,@CreateBy,@RojgarPicName,@RojgarPicType,@RojgarPic,@AppPicName,@AppPicType,@AppPic,@AID,@Graduation_Category,@EducationPicName,@EducationPicType,@EducationPic,@CastPicName,@CastPicType,@CastPic,@DistFromTO,@QuantityOfForm)";


        SqlCommand cmd = new SqlCommand(sql, con);
        SqlParameter[] prms = new SqlParameter[63];
       

        prms[0] = new SqlParameter("@TAID", SqlDbType.VarChar, 50);
        prms[0].Value = App_No;
        prms[1] = new SqlParameter("@FristName", SqlDbType.NVarChar, 50);
        prms[1].Value = txtfname.Value;
        prms[2] = new SqlParameter("@MName", SqlDbType.NVarChar, 50);
        prms[2].Value = txtmname.Value;
        prms[3] = new SqlParameter("@LName", SqlDbType.NVarChar, 50);
        prms[3].Value = txtlname.Value;
        prms[4] = new SqlParameter("@MothersName", SqlDbType.NVarChar, 50);
        prms[4].Value = txtMotherName.Value;
        prms[5] = new SqlParameter("@FathersName", SqlDbType.NVarChar, 50);
        prms[5].Value = txtFatherName.Value;
        prms[6] = new SqlParameter("@ACaste", SqlDbType.VarChar, 50);
        prms[6].Value = ddlcaste.SelectedItem.Text;
        prms[7] = new SqlParameter("@Email", SqlDbType.VarChar, 50);
        prms[7].Value = txtemail.Value;
        prms[8] = new SqlParameter("@MobileNo", SqlDbType.VarChar, 50);
        prms[8].Value = txtmobile.Text;
        prms[9] = new SqlParameter("@DOB", SqlDbType.DateTime);
        prms[9].Value = getDate_MDY(txtDOB.Text);
        prms[10] = new SqlParameter("@TtlFmlIncome", SqlDbType.Decimal);
        prms[10].Value = txtFIncome.Text;
        prms[11] = new SqlParameter("@Sex", SqlDbType.VarChar, 50);
        prms[11].Value = Sex;
        prms[12] = new SqlParameter("@TtlFmlMembers", SqlDbType.VarChar, 50);
        prms[12].Value = txtFMember.Text;
        prms[13] = new SqlParameter("@Occupation", SqlDbType.NVarChar, 50);
        prms[13].Value = ddlEmp.SelectedItem.Text;
        prms[14] = new SqlParameter("@ProjCost", SqlDbType.VarChar, 50);
        prms[14].Value = txtTPV.Text;
        prms[15] = new SqlParameter("@Rojgarnum", SqlDbType.VarChar, 50);
        prms[15].Value = txtRKN.Value;
        prms[16] = new SqlParameter("@Sorcfinc", SqlDbType.NVarChar, 50);
        prms[16].Value = txtSOI.Value;
        prms[17] = new SqlParameter("@CurrAddress", SqlDbType.NVarChar, 50);
        prms[17].Value = txtCaddress.Value;
        prms[18] = new SqlParameter("@ParAddress", SqlDbType.NVarChar, 50);
        prms[18].Value = txtPaddress.Value;
        prms[19] = new SqlParameter("@CurrPinCode", SqlDbType.VarChar, 50);
        prms[19].Value = txtCPIN.Text;
        prms[20] = new SqlParameter("@ParPinCode", SqlDbType.VarChar, 50);
        prms[20].Value = txtPPIN.Text;
        prms[21] = new SqlParameter("@CurDist", SqlDbType.VarChar, 50);
        prms[21].Value = ddlCDistrict.SelectedValue.ToString();
        prms[22] = new SqlParameter("@ParDist", SqlDbType.VarChar, 50);
        prms[22].Value = ddlPDistrict.SelectedValue.ToString();
        prms[23] = new SqlParameter("@CurBlock", SqlDbType.VarChar, 50);
        prms[23].Value = ddlCBlock.SelectedValue.ToString();
        prms[24] = new SqlParameter("@ParBlock", SqlDbType.VarChar, 50);
        prms[24].Value = ddlPBlock.SelectedValue.ToString();
        prms[25] = new SqlParameter("@Education", SqlDbType.VarChar, 50);
        prms[25].Value = Education;
        prms[26] = new SqlParameter("@VoterId", SqlDbType.VarChar, 50);
        prms[26].Value = txtVID.Value;
        prms[27] = new SqlParameter("@AdharCard", SqlDbType.VarChar, 50);
        prms[27].Value = txtAno.Value;
        prms[28] = new SqlParameter("@PaNNo", SqlDbType.VarChar, 50);
        prms[28].Value = txtPAN.Value;
        prms[29] = new SqlParameter("@WarDist", SqlDbType.VarChar, 50);
        prms[29].Value = ddlDistrict.SelectedValue.ToString();
        prms[30] = new SqlParameter("@WarBlock", SqlDbType.VarChar, 50);
        prms[30].Value = ddlBlock.SelectedValue.ToString();
        prms[31] = new SqlParameter("@WarAddress", SqlDbType.NVarChar, 50);
        prms[31].Value = txtWAddr.Value;

        prms[32] = new SqlParameter("@BankName", SqlDbType.NVarChar, 50);
        prms[32].Value = BankName;
        prms[33] = new SqlParameter("@BankAcct", SqlDbType.VarChar, 50);
        prms[33].Value = txtAccNo.Text;
        prms[34] = new SqlParameter("@IFSC", SqlDbType.VarChar, 50);
        prms[34].Value = txtIFSC.Value;
        prms[35] = new SqlParameter("@FristKpName", SqlDbType.NVarChar, 50);
        prms[35].Value = txtP1.Value;
        prms[36] = new SqlParameter("@SecKpName", SqlDbType.NVarChar, 50);
        prms[36].Value = txtP2.Value;
        prms[37] = new SqlParameter("@FristKpAdd", SqlDbType.NVarChar, 50);
        prms[37].Value = txtP1Add.Value;
        prms[38] = new SqlParameter("@SecKpAdd", SqlDbType.NVarChar, 50);
        prms[38].Value = txtP2Add.Value;
        prms[39] = new SqlParameter("@FirstKpMob", SqlDbType.VarChar, 50);
        prms[39].Value = txtP1Mob.Text;
        prms[40] = new SqlParameter("@SecKpMob", SqlDbType.VarChar, 50);
        prms[40].Value = txtP2Mob.Text;
        prms[41] = new SqlParameter("@FirstKpEmail", SqlDbType.VarChar, 50);
        prms[41].Value = txtP1Email.Value;
        prms[42] = new SqlParameter("@SecKpEmail", SqlDbType.VarChar, 50);
        prms[42].Value = txtP2Email.Value;
        prms[43] = new SqlParameter("@FirstKpRel", SqlDbType.NVarChar, 50);
        prms[43].Value = txtP1Rel.Value;
        prms[44] = new SqlParameter("@SecKpRel", SqlDbType.NVarChar, 50);
        prms[44].Value = txtP2Rel.Value;
        prms[45] = new SqlParameter("@CretaedDate", SqlDbType.DateTime);
        prms[45].Value = DateTime.Now;
        prms[46] = new SqlParameter("@CreateBy", SqlDbType.VarChar, 50);
        prms[46].Value = ip;

        prms[47] = new SqlParameter("@RojgarPicName", SqlDbType.NVarChar, 50);
        prms[47].Value = fileName2;
        prms[48] = new SqlParameter("@RojgarPicType", SqlDbType.VarChar, 50);
        prms[48].Value = Extension2;
        prms[49] = new SqlParameter("@RojgarPic", SqlDbType.Image);
        prms[49].Value = imageBytes2;
        prms[50] = new SqlParameter("@AppPicName", SqlDbType.NVarChar, 50);
        prms[50].Value = fileName;
        prms[51] = new SqlParameter("@AppPicType", SqlDbType.VarChar, 50);
        prms[51].Value = Extension;
        prms[52] = new SqlParameter("@AppPic", SqlDbType.Image);
        prms[52].Value = imageBytes;
        prms[53] = new SqlParameter("@AID", SqlDbType.Int);
        prms[53].Value = BID;
        prms[54] = new SqlParameter("@Graduation_Category", SqlDbType.VarChar, 50);
        prms[54].Value = GraduationCategory;
        prms[55] = new SqlParameter("@EducationPicName", SqlDbType.NVarChar, 50);
        prms[55].Value = fileName3;
        prms[56] = new SqlParameter("@EducationPicType", SqlDbType.VarChar, 50);
        prms[56].Value = Extension3;
        prms[57] = new SqlParameter("@EducationPic", SqlDbType.Image);
        prms[57].Value = imageBytes3;
        prms[58] = new SqlParameter("@CastPicName", SqlDbType.NVarChar, 50);
        prms[58].Value = fileName4;
        prms[59] = new SqlParameter("@CastPicType", SqlDbType.VarChar, 50);
        prms[59].Value = Extension4;
        prms[60] = new SqlParameter("@CastPic", SqlDbType.Image);
        prms[60].Value = imageBytes4;
        prms[61] = new SqlParameter("@DistFromTO", SqlDbType.Decimal);
        prms[61].Value = txtDFTO.Text;
        prms[62] = new SqlParameter("@QuantityOfForm", SqlDbType.Decimal);
        prms[62].Value = txtQofForm.Text;
        

        //,EducationPicName,EducationPicType,EducationPic
        int CT = 0;
        cmd.Parameters.AddRange(prms);
        con.Open();
        CT=cmd.ExecuteNonQuery();
        con.Close();
        if (CT > 0)
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Save Successfully...'); </script> ");
            Session["App_ID"] = App_No;
            Response.Redirect("PrintReg.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
    }
    protected void ddlCDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrict.Items.Clear();
        ddlCBlock.Enabled = true;
        Get_Blocks1();
        ddlDistrict.Items.Add(new ListItem(ddlCDistrict.SelectedItem.Text, ddlCDistrict.SelectedValue.ToString()));
        GetBankList();
        get_Region_Detail();
    }
    protected void ddlPDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPBlock.Enabled = true;
        Get_Blocks2();
    }
    public void Get_Blocks1()
    {
        string DistrictId = ddlCDistrict.SelectedValue.ToString();
        //string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
        string qry = "select BlockName_H,BlockID from [tbl_Blocks] where DistrictId='" + DistrictId + "' and Active='Y' order by BlockName_H";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {

            ddlCBlock.DataSource = ds1.Tables[0];
            ddlCBlock.DataTextField = "BlockName_H";
            ddlCBlock.DataValueField = "BlockID";
            ddlCBlock.DataBind();
            ddlCBlock.Items.Insert(0, "--Select--");

            ddlBlock.Enabled = true;
            ddlBlock.DataSource = ds1.Tables[0];
            ddlBlock.DataTextField = "BlockName_H";
            ddlBlock.DataValueField = "BlockID";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, "--Select--");
            


        }
    }
    public void Get_Blocks2()
    {
        string DistrictId = ddlPDistrict.SelectedValue.ToString();
        string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlPBlock.DataSource = ds1.Tables[0];
            ddlPBlock.DataTextField = "Tehsil_Name";
            ddlPBlock.DataValueField = "TehsilCode";
            ddlPBlock.DataBind();
            ddlPBlock.Items.Insert(0, "--Select--");
        }
    }
    protected void FileUploadComplete(object sender, EventArgs e)
    {
        //string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
        //AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void ddlCBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDistrict.Items.Add(new ListItem(ddlCDistrict.SelectedItem.Text, ddlCDistrict.SelectedValue.ToString()));
        //ddlBlock.Items.Add(new ListItem(ddlCBlock.SelectedItem.Text, ddlCBlock.SelectedValue.ToString()));
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBankBranches();
        txtIFSC.Value = "";
        //string OBank = "";
        //OBank = ddlBank.SelectedItem.Text;
        //if (OBank == "OTHER BANK")
        //{
        //    txtOBank.Visible = true;
        //    lblOBank.Visible = true;
        //}
        //else
        //{
        //    txtOBank.Visible = false;
        //    lblOBank.Visible = false;
        //}
    }
    protected void ddlEduc1_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_Edu_name();
    }
    public void get_Edu_Type()
    {
        string qry = "select distinct Edu_type from tbl_MetaDataEducation order by Edu_type";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds2);
        if (ds2 == null)
        {
        }
        else
        {

            ddlEduc1.DataSource = ds2.Tables[0];
            ddlEduc1.DataTextField = "Edu_type";
            ddlEduc1.DataValueField = "Edu_type";
            ddlEduc1.DataBind();
            ddlEduc1.Items.Insert(0, "--Select--");
        }
        ddlEduc2.Items.Clear();

    }

    public void get_Edu_name()
    {
        ddlEduc2.Items.Clear();
        string qry = "select Education_Name,EId from tbl_MetaDataEducation where Edu_Type='" + ddlEduc1.SelectedValue.ToString() + "' order by Education_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds2);
        if (ds2 == null)
        {
        }
        else
        {

            ddlEduc2.DataSource = ds2.Tables[0];
            ddlEduc2.DataTextField = "Education_Name";
            ddlEduc2.DataValueField = "EId";
            ddlEduc2.DataBind();
            ddlEduc2.Items.Insert(0, "--Select--");
        }

    }
    public void GetBankList()
    {
        ddlBank.Items.Clear();
        string qry = "select distinct BANK from [IfscBankmar15] where districtId='"+ ddlCDistrict.SelectedValue.ToString() +"' order by BANK";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds2);
        if (ds2 == null)
        {
        }
        else
        {

            ddlBank.DataSource = ds2.Tables[0];
            ddlBank.DataTextField = "BANK";
            ddlBank.DataValueField = "BANK";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, "--Select--");
            ddlBank.Items.Insert(ddlBank.Items.Count, new ListItem("Other", "0"));
        }

    }
    public void FillBankBranches()
    {
        ddlBBranch.Items.Clear();
        if (ddlBank.SelectedItem.Text != "Other")
        {
            
            string qry = "select Branch from [IfscBankmar15] where Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlCDistrict.SelectedValue.ToString() + "' order by BANK";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds2);
            if (ds2 == null)
            {
            }
            else
            {

                ddlBBranch.DataSource = ds2.Tables[0];
                ddlBBranch.DataTextField = "Branch";
                ddlBBranch.DataValueField = "Branch";
                ddlBBranch.DataBind();
                ddlBBranch.Items.Insert(0, "--Select--");
                ddlBBranch.Enabled = true;
            }
        }
        else
        {
            ddlBBranch.Enabled = false;
        }
    }
    protected void ddlBBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetIFSCCode();
    }
    public void GetIFSCCode()
    {

        //ddlBBranch.Items.Clear();
        //string qry = "select Branch from [IfscBankmar15] where Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlCDistrict.SelectedValue.ToString() + "' order by BANK";
        string qry = "select ID from [IfscBankmar15] where Branch='" + ddlBBranch.SelectedItem.Text + "' and Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlCDistrict.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt=new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtIFSC.Value = dt.Rows[0]["ID"].ToString();
        }
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedItem.Text != "--Select--" && ddlEmp.SelectedItem.Text != "Unemployment")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You are not Eligible to Fill this Form...'); </script> ");
            ddlEmp.SelectedIndex = -1;
            //Session.Abandon();
            //Response.Redirect("UserReg.aspx");
        }
        
    }
    public void get_Region_Detail()
    {
        string DistrictId = ddlCDistrict.SelectedValue.ToString();
        string qry = "SELECT MR.region,MR.Address,MR.Phone_No,MR.Mob_No,MR.Email FROM tbl_MetaData_Region as MR inner join tbl_MetaData_DISTRICT as MD on MD.Region_ID=MR.Region_Id where MD.District_Id='" + DistrictId + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt=new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblRegionAdd.Text = "क्षे॰ का॰ नाम :- "+dt.Rows[0]["region"].ToString() + ',' +"    पता :- "+ dt.Rows[0]["Address"].ToString() + ',' +"    फोन :- "+ dt.Rows[0]["Phone_No"].ToString() + ',' + dt.Rows[0]["Mob_No"].ToString();
            //lblRegionAdd.Text = "क्षे॰ का॰ -" + dt.Rows[0]["region"].ToString() +"," + dt.Rows[0]["Address"].ToString() +"," + dt.Rows[0]["Phone_No"].ToString() + "," + dt.Rows[0]["Mob_No"].ToString() + "," + dt.Rows[0]["Email"].ToString();
            lblRegionAdd.Text = "क्षे॰ का॰ नाम एवं पता :- " + dt.Rows[0]["region"].ToString() + "," + dt.Rows[0]["Address"].ToString();
            lblRegionAdd2.Text = "दूरभाष क्र॰ एवं ईमेल :- " + dt.Rows[0]["Phone_No"].ToString() + "," + dt.Rows[0]["Mob_No"].ToString() + "," + dt.Rows[0]["Email"].ToString();
        }
        else
        {
            
        }  
    }
   
}