using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.IO;


public partial class JVSMiller_Registration : System.Web.UI.Page
{
    DataTable dt;
    int rvalue;
   
    SqlParameter param;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        

        if (Session["MillerRegId"] == null || Session["Id"] == null)
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {

            ModalPopupExtender2.Show();

           

         


            fillDistrict();


            DataTable dt = new DataTable();
            string mregid=Session["MillerRegId"].ToString();

            dt = GetMillerPreRegByRegId(mregid);
            if (dt.Rows.Count > 0)
            {
               hdnMRegId.Value = dt.Rows[0]["Registration_ID"].ToString();

               litRegId.Text= dt.Rows[0]["Registration_ID"].ToString();
               litMillName.Text= dt.Rows[0]["Miller"].ToString();
               litMobile.Text = dt.Rows[0]["Mobile"].ToString();
               litEmail.Text = dt.Rows[0]["Email"].ToString();
               litAdhar.Text=  dt.Rows[0]["Aadhar"].ToString();
               litPan.Text= dt.Rows[0]["Pan"].ToString();
            
            }
        
        }
    }

    protected void btnAgree_Click(object sender, EventArgs e)
    {
        if (chkDistrictIndustry.Checked == true && chkMpscscAgree.Checked == true && ckhPaddy.Checked == true && chkreg1.Checked == true && chkreg2.Checked == true)
        {
            panelRegForm.Visible = true;
            ModalPopupExtender2.Hide();
        }

        else {

            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('रजिस्ट्रेशन करने के लिए सभी आवश्यक निर्देशों का चुनाव करना आनिवार्य हें |......'); </script> ");
            ModalPopupExtender2.Show();
        
        
        }
        
        

    }



   
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTehsil();
        fillTehsilBlock();
    }


    protected void ddlTehsilBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBranch();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

      // bool filesizevalid = ValidateFileSize();

        bool filesizevalid = true;
        if (filesizevalid)
        {


            string filename = Path.GetFileNameWithoutExtension(fuRegDoc.PostedFile.FileName);

            string extention = Path.GetExtension(fuRegDoc.PostedFile.FileName);
 
            byte[] imagebyte = getImageToBinary();

            int rvalue = SaveMillRegistrationMaster(hdnMRegId.Value.ToString(), txtMill.Text, float.Parse(txtRiceCap.Text.ToString()), txtOfficeNo.Text, txtMLandmark.Text, "23",
                ddlDistrict.SelectedValue.ToString(), ddlTehsil.SelectedValue.ToString(), ddlTehsilBlock.SelectedValue.ToString(),
                ddlBranch.SelectedValue.ToString(), float.Parse(txtNDistance.Text.ToString()), txtOfficeAddr.Text, txtInchPerson.Text, txtInchPost.Text,
                txtInchEmail.Text, txtInchMobile.Text, txtInchAddr.Text, txtMLat.Text, txtMLat.Text, ClientIP, txtRegNoDistInd.Text, txtRegNoIssueDt.Text,ddlmpscsc_markfed.SelectedValue.ToString(), txtAgreeMilerId.Text, txtAgreeDate.Text, txtAgreeCap.Text, txtPaddyCap.Text,
                filename, extention, imagebyte);

            if (rvalue == -1)
            {
                Response.Write("<script>alert('Already Registered.')</script>");

            }

            else if (rvalue > 0)
            {
                Response.Write("<script>alert('You have successfully Registered')</script>");
                Response.Redirect("RegistrationPrint.aspx");

            }
        }

        else
        {
            Response.Write("<script>alert('File size must not exceed 100 KB.')</script>");
            
        
        }
    }

    //public bool ValidateFileSize()
    //{
    //    System.Drawing.Image img = System.Drawing.Image.FromStream(fuRegDoc.PostedFile.InputStream);
    //    int height = img.Height;
    //    int width = img.Width;
    //    decimal size = Math.Round(((decimal)fuRegDoc.PostedFile.ContentLength / (decimal)1024), 2);
    //    if (size > 100)
    //    {
    //        //CustomValidator1.ErrorMessage = "File size must not exceed 100 KB.";
    //        return false;
    //    }
    //    //if (height > 100 || width > 100)
    //    //{
    //    //    CustomValidator1.ErrorMessage = "Height and Width must not exceed 100px.";
    //    //    e.IsValid = false;
    //    //}

    //    return true;
    //}

    //fill district
    public void fillDistrict()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetDistrict();
            if (dt.Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

        }
    }

    //fill tehsil
    public void fillTehsil()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetTehsil(ddlDistrict.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                ddlTehsil.Items.Clear();
                ddlTehsil.DataSource = dt;
                ddlTehsil.DataTextField = "Tehsil_Name";
                ddlTehsil.DataValueField = "TehsilCode";
                ddlTehsil.DataBind();
                ddlTehsil.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

        }


    }

    //fill block
    public void fillTehsilBlock()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetTehsilBlock(ddlDistrict.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                ddlTehsilBlock.Items.Clear();
                ddlTehsilBlock.DataSource = dt;
                ddlTehsilBlock.DataTextField = "Block_Name";
                ddlTehsilBlock.DataValueField = "Block_ID";
                ddlTehsilBlock.DataBind();
                ddlTehsilBlock.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

        }
    }

    //fill branch
    public void FillBranch()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetBranch(ddlTehsilBlock.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

        }
    }



    public DataTable GetMillerPreRegByRegId(string mregid)
    {
        string qr = "select * from dbo.PreRegistration where Registration_ID='" + mregid + "'";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }




    // Mill Registration
    public int SaveMillRegistrationMaster(string mregid, string mill, float ricecap, string officeno, string mlndmark, string stateid, string distid, string tehcode, string blckid, string nbranchid, float ndistance, string officeadd, string inchnm, string inchpost, string inchemail, string inchmob, string inchadd, string mlat, string mlong, string ip, string Dist_Ind_RegNo, string RegNo_Issue_Date, string Mpscsc_Markfed, string Agree_Miller_Id, string Agree_Date, string Agree_Capacity, string Paddy_Capacity, string Doc_File_Name, string Doc_File_Type, byte[] Doc_File)
    {
        ArrayList list = new ArrayList();

        param = new SqlParameter("@Registration_ID", mregid);
        list.Add(param);
        param = new SqlParameter("@Mill_Name", mill);
        list.Add(param);
       
        param = new SqlParameter("@Rice_Capacity", ricecap);
        list.Add(param);
        param = new SqlParameter("@Office_Contact", officeno);
        list.Add(param);
        param = new SqlParameter("@Mill_Landmark", mlndmark);
        list.Add(param);
        param = new SqlParameter("@State_Id", stateid);
        list.Add(param);
        param = new SqlParameter("@District_Id", distid);
        list.Add(param);
        param = new SqlParameter("@Tehsil_Code", tehcode);
        list.Add(param);
        param = new SqlParameter("@Block_Id", blckid);
        list.Add(param);
        param = new SqlParameter("@Near_Branch_Id", nbranchid);
        list.Add(param);
        param = new SqlParameter("@Near_Distance", ndistance);
        list.Add(param);
        param = new SqlParameter("@Mill_Office_Address", officeadd);
        list.Add(param);
        param = new SqlParameter("@Incharge_Peson", inchnm);
        list.Add(param);
        param = new SqlParameter("@Incharge_Post", inchpost);
        list.Add(param);
        param = new SqlParameter("@Incharge_Email", inchemail);
        list.Add(param);
        param = new SqlParameter("@Incharge_Mobile", inchmob);
        list.Add(param);
        param = new SqlParameter("@Incharge_Address", inchadd);
        list.Add(param);
        param = new SqlParameter("@Mill_Lat", mlat);
        list.Add(param);
        param = new SqlParameter("@Mill_Long", mlong);
        list.Add(param);
        param = new SqlParameter("@IPAddress ", ip);
        list.Add(param);


        param = new SqlParameter("@Dist_Ind_RegNo", Dist_Ind_RegNo);
        list.Add(param);
        param = new SqlParameter("@RegNo_Issue_Date", RegNo_Issue_Date);
        list.Add(param);

        param = new SqlParameter("@Mpscsc_Markfed", Mpscsc_Markfed);
        list.Add(param);

        param = new SqlParameter("@Agree_Miller_Id", Agree_Miller_Id);
        list.Add(param);
        param = new SqlParameter("@Agree_Date", Agree_Date);
        list.Add(param);
        param = new SqlParameter("@Agree_Capacity", Agree_Capacity);
        list.Add(param);
        param = new SqlParameter("@Paddy_Capacity", Paddy_Capacity);
        list.Add(param);

        //file binary
        param = new SqlParameter("@Doc_File_Name", Doc_File_Name);
        list.Add(param);
        param = new SqlParameter("@Doc_File_Type", Doc_File_Type);
        list.Add(param);
        param = new SqlParameter("@Doc_File", Doc_File);
        list.Add(param);

        try
        {
            rvalue = new Sqldatalayer().ExecuteScalar("mycon", "[spMillRegistrationMaster_Insert]", list, true);
        }

        catch (Exception ex) { }
        finally { }
        return rvalue;
    }


    public DataTable GetState()
    {
        string qr = "select State_Code,State_Name from tbl_Metadata_State where State_Code=23";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public DataTable GetDistrict()
    {
        string qr = "select District_Id,District_Name from tbl_MetaData_DISTRICT where State_Id=23 order by District_Name";
        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public DataTable GetTehsil(string dstid)
    {
        string qr = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + dstid + "' order by [Tehsil_Name]";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public DataTable GetTehsilBlock(string dstid)
    {
        string qr = "select distinct [Block_ID],[Block_Name] from tbl_Branch_Block_Mapping where [District_ID]='" + dstid + "' and Block_ID is not null ";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }

    public DataTable GetBranch(string tblock)
    {
        string qr = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi WHERE mbi.BranchID in (select [Branch_ID] from tbl_Branch_Block_Mapping where [Block_ID]='" + tblock.Trim() + "')";

        try
        {
            dt = new Sqldatalayer().SelectData("mycon", qr, null, false);
        }

        catch (Exception ex) { }
        finally { }
        return dt;
    }


    public byte[] getImageToBinary()
    {
        byte[] bytes;
        using (BinaryReader br = new BinaryReader(fuRegDoc.PostedFile.InputStream))
        {
            bytes = br.ReadBytes(fuRegDoc.PostedFile.ContentLength);
        }


        

        return bytes;

    }

    
   
}