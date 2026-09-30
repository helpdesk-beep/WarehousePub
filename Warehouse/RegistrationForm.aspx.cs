using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.IO;

public partial class QCRegistration_RegistrationForm : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            datatableforteacherdetail();
            txtpercentage.Enabled = false;
            txtbsePer.Enabled = false;
            txtcseperce.Enabled = false;
            GetDistrict();
            //GrdDistribution.Visible = true;
        }
    }
    private void datatableforteacherdetail()
    {
        DataTable dt1 = new DataTable();
        try
        {
            if (dt1.Columns.Count == 0)
            {
                dt1.Columns.Add("Education_Id", typeof(int));
                dt1.Columns.Add(new DataColumn("Exam_Name", typeof(string)));
                dt1.Columns.Add(new DataColumn("Board", typeof(string)));
                // dt1.Columns.Add(new DataColumn("Subject", typeof(int)));
                dt1.Columns.Add(new DataColumn("TotalMarks", typeof(string)));
                dt1.Columns.Add(new DataColumn("Marks", typeof(string)));
                dt1.Columns.Add(new DataColumn("Rank", typeof(int)));
            }
            ViewState["dt1"] = dt1;
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning!", ex.Message);
        }
        finally
        {
            if (dt1 != null)
            {
                dt1.Dispose();
            }
        }
    }

    protected void txtPraptank_TextChanged(object sender, EventArgs e)
    {
        if (txtbsepra.Text != "")
        {
            int TotalPercentage = 0;
            int TotalNumber = Convert.ToInt32(txtbseTotal.Text);
            int PraptNumber = Convert.ToInt32(txtbsepra.Text);
            if (Convert.ToInt32(txtbsepra.Text) != 0)
            {
                TotalPercentage = PraptNumber * 100 / TotalNumber;
                txtbsePer.Text = TotalPercentage.ToString();
                txtbsePer.Enabled = false;
            }
        }
    }
    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {

        if (Ckeckbox.Checked)
        {
            btnsave.Visible = true;
        }
        else
        {
            btnsave.Visible = false;
        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        string ErrorMsg = "";
        if (!string.IsNullOrEmpty(txtDate.Text))
        {
            getDate_MDY(txtDate.Text);
        }
        //ErrorMsg += ddlitem.SelectedIndex > 0 ? "" : "Please Select Item... \\n";
        //ErrorMsg += ddldepartment.SelectedIndex > 0 ? "" : "Please Select Department... \\n";
        //ErrorMsg += !string.IsNullOrEmpty(txtDesignationname.Text) ? "" : "Enter Designation Name. \\n";
        //ErrorMsg += !string.IsNullOrEmpty(txtemp.Text) ? "" : "Enter Employe Name \\n";
        //ErrorMsg += !string.IsNullOrEmpty(txtdistributer.Text) ? "" : "Enter Distributer Name \\n";
        //ErrorMsg += !string.IsNullOrEmpty(txtdistribute.Text) ? "" : "Enter Distribute Qty \\n";
        //if (ErrorMsg == "")
        //{
        if (btnsave.Text == "Submit")
        {
            CandidatePhoto();
            CandidateSignature();
            String CandidateImage = ViewState["CandidateImage"].ToString();
            String CandidateSignatureM = ViewState["CandidateSignature"].ToString();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Insert_Candidate_Registration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Candidate_Name", txtname.Text);
            cmd.Parameters.AddWithValue("@Candidate_Father_Name", txtFather.Text);
            cmd.Parameters.AddWithValue("@Candidate_Mother_Name", txtMother.Text);
            cmd.Parameters.AddWithValue("@Birth_Date", getDate_MDY(txtDate.Text));
            cmd.Parameters.AddWithValue("@Gender", ddlgender.SelectedValue);
            cmd.Parameters.AddWithValue("@Cast", ddlcast.SelectedValue);
            cmd.Parameters.AddWithValue("@Nationality", ddlNationality.SelectedValue);
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Present_Address", txtpresentadd.Text);
            cmd.Parameters.AddWithValue("@Parmanent_Address", txtparmanentAdd.Text);
            cmd.Parameters.AddWithValue("@Candidate_Mobile_No", txtmobile.Text);
            cmd.Parameters.AddWithValue("@Candidate_Email_Id", txtemail.Text);
            cmd.Parameters.AddWithValue("@Maritial_Stutes", ddlmarried.SelectedValue);
            cmd.Parameters.AddWithValue("@Candidate_Image", CandidateImage);
            cmd.Parameters.AddWithValue("@Candidate_Signature", CandidateSignatureM);
            cmd.Parameters.AddWithValue("@Candidate_Check", Ckeckbox.Text);
            cmd.Parameters.AddWithValue("@Edu_Qualifi_10", txt10th.Text);
            cmd.Parameters.AddWithValue("@Board_10", txtbu.Text);
            cmd.Parameters.AddWithValue("@TotalNumber_10", txtpurnank.Text);
            cmd.Parameters.AddWithValue("@TotalReceived_10", txtPraptank.Text);
            cmd.Parameters.AddWithValue("@Percentage_10", txtpercentage.Text);
            cmd.Parameters.AddWithValue("@bse_Edu_Qualifi", txtbse.Text);
            cmd.Parameters.AddWithValue("@bseBoard", txtbseBoard.Text);
            cmd.Parameters.AddWithValue("@bseTotal", txtbseTotal.Text);
            cmd.Parameters.AddWithValue("@bsepra", txtbsepra.Text);
            cmd.Parameters.AddWithValue("@bsePer", txtbsePer.Text);
            cmd.Parameters.AddWithValue("@Computer_Qualification", txtComputer.Text);
            cmd.Parameters.AddWithValue("@cseboard", txtcseboard.Text);
            cmd.Parameters.AddWithValue("@csetotal", txtcsetotal.Text);
            cmd.Parameters.AddWithValue("@cserece", txtcserece.Text);
            cmd.Parameters.AddWithValue("@cseperce", txtcseperce.Text);
            cmd.Parameters.AddWithValue("@Created_By", '2');
            cmd.Parameters.AddWithValue("@Created_By_Ip", ip);
            cmd.Parameters.AddWithValue("@IP_Adress", ip);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Candidate Registration Insert Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //FillGrid();
                //TextClear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                //TextClear();
            }
        }

    }

    protected string CandidatePhoto()
    {
        string result = "";
        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";

            if (Nurseryphoto.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExt = 0;
                int iFailedCntSize = 0;
                string fileExt = System.IO.Path.GetExtension(Nurseryphoto.FileName).Substring(1);
                string[] supportedTypes = { "jpg", "png", "jpeg", "JPG", "PNG", "JPEG" };
                if (!supportedTypes.Contains(fileExt))
                {
                    iFailedCntExt += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Candidate Image Should be In JPG,PNG and JPEG Format Only')", true);
                    result = "Candidate Image Should be In JPG,PNG and JPEG Format Only";
                }
                else if (Nurseryphoto.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 100
                {
                    iFailedCntSize += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Candidate Image Should be Under 500KB')", true);
                    result = "Candidate Image Should be Under 500KB";
                }
                else
                {
                    strFileName = Nurseryphoto.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "");
                    strTimeStamp = strTimeStamp.Replace(" ", "");
                    strTimeStamp = strTimeStamp.Replace(":", "");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../warehouse/Candidate_Image/"), strFileName);
                    Nurseryphoto.SaveAs(path);
                    ViewState["CandidateImage"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else
            {
                string path3 = Path.Combine(Server.MapPath("../warehouse/Candidate_Image/"), strFileName);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex + "')", true);
            // ErrorLogCls.SendErrorToText(ex);
        }
        return result;
    }
    protected string CandidateSignature()
    {
        string result = "";
        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";

            if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExt = 0;
                int iFailedCntSize = 0;
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                string[] supportedTypes = { "jpg", "png", "jpeg", "JPG", "PNG", "JPEG" };
                if (!supportedTypes.Contains(fileExt))
                {
                    iFailedCntExt += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Candidate Signature Should be In JPG,PNG and JPEG Format Only')", true);
                    result = "Candidate Signature Image Should be In JPG,PNG and JPEG Format Only";
                }
                else if (FileUpload1.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 100
                {
                    iFailedCntSize += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Candidate Signature Image Should be Under 500KB')", true);
                    result = "Candidate Signature Image Should be Under 500KB";
                }
                else
                {
                    strFileName = FileUpload1.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "");
                    strTimeStamp = strTimeStamp.Replace(" ", "");
                    strTimeStamp = strTimeStamp.Replace(":", "");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../warehouse/Candidate_Signature/"), strFileName);
                    FileUpload1.SaveAs(path);
                    ViewState["CandidateSignature"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else
            {
                string path3 = Path.Combine(Server.MapPath("../warehouse/Candidate_Signature/"), strFileName);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex + "')", true);
            // ErrorLogCls.SendErrorToText(ex);
        }
        return result;
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

    protected void txtbsepra_TextChanged(object sender, EventArgs e)
    {
        if (txtbsepra.Text != "")
        {
            int TotalPercentage2 = 0;
            int TotalNumber1 = Convert.ToInt32(txtbseTotal.Text);
            int PraptNumber2 = Convert.ToInt32(txtbsepra.Text);
            if (Convert.ToInt32(txtbsepra.Text) != 0)
            {
                TotalPercentage2 = PraptNumber2 * 100 / TotalNumber1;
                txtbsePer.Text = TotalPercentage2.ToString();
                txtbsePer.Enabled = false;
            }
        }
    }

    protected void txtcserece_TextChanged(object sender, EventArgs e)
    {
        if (txtcserece.Text != "")
        {
            int TotalPercentage1 = 0;
            int TotalNumber3 = Convert.ToInt32(txtcsetotal.Text);
            int PraptNumber4 = Convert.ToInt32(txtcserece.Text);
            if (Convert.ToInt32(txtcserece.Text) != 0)
            {
                TotalPercentage1 = PraptNumber4 * 100 / TotalNumber3;
                txtcseperce.Text = TotalPercentage1.ToString();
                txtcseperce.Enabled = false;
            }
        }
    }
}