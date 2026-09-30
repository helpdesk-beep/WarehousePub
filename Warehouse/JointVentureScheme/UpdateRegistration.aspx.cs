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
using System.Resources;


public partial class JointVentureScheme_RegistrationUpdate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
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
    string Registration_No = "";
    int BID = 0;
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    int chkupdate = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.StartDate = DateTime.Now;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if ((Session["email"] != null) && (Session["mobile"] != null) && Session["Reg_No"] != null)
        {
            lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
            lblEmail.Text = Session["email"].ToString();
            lblMob.Text = Session["mobile"].ToString();
            if (!IsPostBack)
            {
                int a = chkofrstopupdate();
                //if (a == 0)
                //{
                    get_Districts();
                    get_Applicant_Type();
                    // ddlWarDistrict_SelectedIndexChanged(null, EventArgs.Empty);
                    GetRegisterationData();
                    GetPreReg();
                    GetWFacility();
                    GetGdwn();
                //}
                //else
                //{
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Cannot Update Registration after offer...'); </script> ");
                //}
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("UserReg.aspx");
        }
 
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public int CheckEmail()
    {
        int ch = 0;
        string strsql = "select EmailID from tbl_WarehouseRegistration where EmailID='" + Session["email"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }
    public int checkWRFacility()
    {
        int WF = 0;
        string strsql = "select * from tbl_WarehouseAdditionalinfo where Registration_ID='" + Session["Reg_No"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strsql, con, sqltran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            WF = 1;
        }
        else
        {
            WF = 0;
        }
        return WF;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        {

      //  DateTime STDate = Convert.ToDateTime(getDate_MDY(txtSLDate.Text));
      //  DateTime WDDate = Convert.ToDateTime(getDate_MDY(txtWDRALDate.Text));

        int SK = 0;
        SK = CheckEmail();
        if (SK == 1)
        {
            if (txtAadharNo.Value.Length != 12)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Correct Aadhar No....'); </script> ");
                txtAadharNo.Focus();
            }
            else if (txtPAN.Value.Length!=10)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Correct Pan No....'); </script> ");
                txtPAN.Focus();
            }
            else if (ddlAppType.SelectedItem.Text=="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Applicant Type....'); </script> ");
                ddlAppType.Focus();
            }
            else if (ddlDistrict.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Applicant Type....'); </script> ");
                ddlDistrict.Focus();
            }
            else if (txtAuthPerson.Value=="")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Authorized Person....'); </script> ");
                txtAuthPerson.Focus();
            }
            else if (txtWarehouseName.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse Name...'); </script> ");
                txtWarehouseName.Focus();
            }
            else if (txtWareAddress.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse/Office Address with Postal Address:...'); </script> ");
                txtWareAddress.Focus();
            }
            else if (txtWareContactNo.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Office Contact No./Mobile No:...'); </script> ");
                txtWareContactNo.Focus();
            }
            else if (txtLandmark.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Landmark Near Warehouse:...'); </script> ");
                txtLandmark.Focus();
            }
            else if (ddlWarDistrict.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select District...'); </script> ");
                ddlWarDistrict.Focus();
            }
            else if (ddlBlock.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Tehsil...'); </script> ");
                ddlBlock.Focus();
            }
            else if (ddlBranch.SelectedItem.Text == "---Select---")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Branch...'); </script> ");
                ddlBranch.Focus();
            }
            else if (txtDistance.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Distance from nearest branch of MPWLC (in KM):...'); </script> ");
                txtDistance.Focus();
            }
            //    Licence Detail Commecnt date 01/02/2019
            //else if (rdoYes.Checked == false && rdoYesW.Checked == false)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Atleast One Licence Required :...'); </script> ");
            //   // txtAadharNo.Focus();
            //}
            //else if (rdoYes.Checked == true && (txtWLicNo.Value == "" || txtSLDate.Text == ""))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse State Licence No & Date...'); </script> ");
            //    txtWLicNo.Focus();
            //}
            //else if (rdoYesW.Checked == true && (txtWDRALicenceNo.Value == "" || txtWDRALDate.Text == ""))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter WDRA Registration No & date :...'); </script> ");
            //    txtWDRALicenceNo.Focus();
            //}
            //else if (rdoYes.Checked == true && (STDate.Date < DateTime.Now.Date))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid State Licence Date..'); </script> ");
                
            //}
            //else if (rdoYesW.Checked && (WDDate.Date < DateTime.Now.Date))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid WDRA Licence Date..'); </script> ");
               
            //}


            else if (txtIncharge.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge/Manager Name :..'); </script> ");
                txtIncharge.Focus();
            }
            else if (txtInchAdd.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge/Manager Address with Postal Address:...'); </script> ");
                txtInchAdd.Focus();
            }
            else if (txtDesign.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Designation :...'); </script> ");
                txtDesign.Focus();
            }
            else if (txtInchMob.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Mobile No. :...'); </script> ");
                txtInchMob.Focus();
            }
            else if (txtInchMob.Text.Length!=10)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Correct Incharge Mobile No. :...'); </script> ");
                txtInchMob.Focus();
            }
            else if (txtInchargeEmail.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Email ID...'); </script> ");
                txtInchargeEmail.Focus();
            }
            else if (txtAccNo.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Account Number...'); </script> ");
                txtAccNo.Focus();
            }
            else if (txtIFSC.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter IFSC Code...'); </script> ");
                txtIFSC.Focus();
            }
            else if (txtHighway.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Distance of Warehouse From National Highway/State Highway (in KM)...'); </script> ");
                txtHighway.Focus();
            }
            else if (txtRailway.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Distance of Warehouse From Railway Station (in KM)...'); </script> ");
                txtRailway.Focus();
            }
            else if (txtMandi.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Distance of Warehouse From Mandi (in KM)...'); </script> ");
                txtMandi.Focus();
            }
            else if (txtGS.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Distance of Warehouse From Goods Shed (in KM)...'); </script> ");
                txtGS.Focus();
            }
            else if (ddlRoadType.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Motorable Approach Road Type...'); </script> ");
                ddlRoadType.Focus();
            }
            else if (txtRoadWidth.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Width of Road (In Meters)...'); </script> ");
                txtRoadWidth.Focus();
            }
            else if (ddlGateType.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Shutter/Jali/Chanel Gate in Godown...'); </script> ");
                ddlGateType.Focus();
            }
            else if (txtNoGate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter No of Gates...'); </script> ");
                txtNoGate.Focus();
            }
            else if (ddlPowersuply.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Power Supply...'); </script> ");
                ddlPowersuply.Focus();
            }
            else if (ddlTentionline.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Is Godown free from Passing over of any tension electric line...'); </script> ");
                ddlTentionline.Focus();
            }
            else if (ddlWaterFac.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Water Facility..'); </script> ");
                ddlWaterFac.Focus();
            }
            else if (ddlWaterSpry.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Water Facility for spray and other usage..'); </script> ");
                ddlWaterSpry.Focus();
            }
            else if (ddlCCTV.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select CCTV Camera..'); </script> ");
                ddlCCTV.Focus();
            }
            else if (ddlFumigation.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability Of Fumigation & Pest Control Equipment..'); </script> ");
                ddlFumigation.Focus();
            }
            else if (ddlGuard.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Guard With Guard Room..'); </script> ");
                ddlGuard.Focus();
            }
            else if (ddlPlanks.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability of Wooden Planks/Dunnage..'); </script> ");
                ddlPlanks.Focus();
            }
            else if (ddlFireBuc.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability fo Fire Buckets..'); </script> ");
                ddlFireBuc.Focus();
            }
            else if (ddlFireExt.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability of Fire extinguisher with fire hydrants..'); </script> ");
                ddlFireExt.Focus();
            }
            else if (ddlElectWeigh.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Electronic Weighbridge..'); </script> ");
                ddlElectWeigh.Focus();
            }
            else if (ddlWeighCertified.SelectedItem.Text == "--Select--" && ddlElectWeigh.SelectedItem.Text == "Yes")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Weighbridge is Certified By Controler..'); </script> ");
                ddlWeighCertified.Focus();
            }
            else if ((Convert.ToDecimal(txtWeighCpt.Text) < 30 || txtWeighCpt.Text == "") && (ddlElectWeigh.SelectedItem.Text == "Yes"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Electronic Weighbridge Capacity should greater than or equal to 30 MT'); </script> ");
                txtWeighCpt.Focus();
            }
            else if (ddlWeighCertified.SelectedItem.Text=="Yes" && txtWB_calibrationdate.Text=="")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Electronic Weighbridge Certified Validity Date '); </script> ");
                txtWB_calibrationdate.Focus();
            }
            else if (ddlInternetCon.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Internet Connectivity..'); </script> ");
                ddlInternetCon.Focus();
            }
            else if (ddlHardwareAvl.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Availability of Computer ..'); </script> ");
                ddlHardwareAvl.Focus();
            }
            else if (ddlConType.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Connectivity Type ..'); </script> ");
                ddlConType.Focus();
            }
            else if (ddlInstallationyear.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Year Of Installation ..'); </script> ");
                ddlInstallationyear.Focus();
            }
            else if (CheckBox1.Checked == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Self Declaration Check box...'); </script> ");
                CheckBox1.Focus();
            }
            else if (ddlCast.SelectedItem.Text=="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Category ....'); </script> ");
                ddlCast.Focus();
            }
            else if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "Yes" && (txtWMCpt.Text == "" || txtWMCpt.Text == "0.00" || txtWMCpt.Text == "0"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Weighing Machine Capacity(In M.T) ....'); </script> ");
                txtWMCpt.Focus();
            }
            else if (ddlblocknew.SelectedItem.Text =="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Block....'); </script> ");
                ddlblocknew.Focus();
            }
            else if (ddlboundrytype.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Warehouse Boundray Type....'); </script> ");
                ddlboundrytype.Focus();
            }
            else if (txtlat.Text == "" && txtlat.Text.Trim().Length<6)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Latitude....'); </script> ");
                txtlat.Focus();
            }
            else if (txtlong.Text == "" && txtlong.Text.Trim().Length < 6)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Longitude....'); </script> ");
                txtlong.Focus();
            }
            else
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int chklic = checklicnodatevalidation();
                if (chklic == 1)
                {
                    try
                    {
                        sqltran = con.BeginTransaction();
                        Insert_Registration_Detail();
                        if (chkupdate == 1)
                        {
                            sqltran.Commit();
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Update Records...'); </script> ");
                            btnsubmit.Visible = false;
                            //Response.Redirect("WarehouseHome.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Problem to Update...'); </script> ");
                        }
                    }
                    catch (Exception ex)
                    {
                        sqltran.Rollback();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
                    }
                    finally
                    {
                        sqltran.Dispose();
                        con.Close();
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Email ID Not Registered ...'); </script> ");
            Response.Redirect("WarehouseHome.aspx");
        }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 19/02/2018 11:00:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Updation in Warehouse Registration under Joint Venture Scheme has been closed..!'); </script> ");
        }
    }
    public void get_Applicant_Type()
    {
        
        string qry = "SELECT [Applicant_TypeId],[Applicant_Type] FROM [tbl_Metadata_ApplicantType]";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlAppType.DataSource = ds.Tables[0];
            ddlAppType.DataTextField = "Applicant_Type";
            ddlAppType.DataValueField = "Applicant_TypeId";
            ddlAppType.DataBind();
            ddlAppType.Items.Insert(0, "--Select--");
        }
    }

    public void get_Districts()
    {
        string qry = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlWarDistrict.DataSource = ds1.Tables[0];
            ddlWarDistrict.DataTextField = "District_Name";
            ddlWarDistrict.DataValueField = "District_Id";
            ddlWarDistrict.DataBind();
            ddlWarDistrict.Items.Insert(0, "--Select--");
            ddlDistrict.DataSource = ds1.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
        }

    }
    public void GetApplicationNo()
    {
        string AGC = "";
        qry = "select max(AId) as BId from tbl_WarehouseRegistration";
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
                string INCN = SubBN.ToString();
                if (INCN.Length == 1)
                {
                    AGC = "00" + INCN.ToString();
                }
                else if (INCN.Length == 2)
                {
                    AGC = "0" + INCN.ToString();
                }
                else if (INCN.Length == 3)
                {
                    AGC = INCN.ToString();
                }
                //App_No = "042017" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + AGC.ToString();
                Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "18" + AGC.ToString();
                BID = SubBN;
            }
            else
            {
                Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "18" + "001";
                BID = 1;
            }
        }
        else
        {
            Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "18" + "001";
            BID = 1;
        }
    }
    public void Insert_Registration_Detail()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        qry = "INSERT INTO [tbl_Warehouse_PreReg_Log] select [TID],[StateID],[DistrictID],[ApplicantType],[WarehouseType],[Auth_Person],[EmailID],[MobileNo],[DOB],[Password],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[Reg_No],[IsActive],[Aadhar_No],[PAN_No],[Salt],[HashedPassword],[FirstTimeLogin],[MasterSalt],[MasterHashedPassword] FROM [tbl_Warehouse_PreReg] where EmailID='" + Session["email"].ToString() + "' and Reg_No='" + Session["Reg_No"].ToString() + "' ";
        cmd = new SqlCommand(qry, con, sqltran);
        int A1 = 0;
        A1 = cmd.ExecuteNonQuery();
        if (A1 == 1)
        {
            qry = "UPDATE [tbl_Warehouse_PreReg] SET [DistrictID] = '" + ddlDistrict.SelectedValue.ToString() + "',[ApplicantType] = '" + ddlAppType.SelectedValue.ToString() + "',[Auth_Person] = '" + txtAuthPerson.Value + "' ,[UpdatedBy] = '" + ip + "',[UpdatedDate] = GETDATE(),[Aadhar_No] = '" + txtAadharNo.Value + "',[PAN_No] = '" + txtPAN.Value + "' where EmailID='" + Session["email"].ToString() + "' and Reg_No='" + Session["Reg_No"].ToString() + "'";
            cmd = new SqlCommand(qry, con, sqltran);
            int CT = 0;
            CT = cmd.ExecuteNonQuery();
            if (CT == 1)
            {
                //Commecnt Licence Details  insert 
                //string StateLicenceDate = "";
                //string WDRALicenceDate = "";
                //string StateLicenceNo = "";
                //string WDRALicenceNO = "";
                //if (rdoYes.Checked == true)
                //{
                //    StateLicenceDate = getDate_MDY(txtSLDate.Text);
                //    StateLicenceNo = txtWLicNo.Value;
                //}
                //else if (rdoNo.Checked == true)
                //{
                //    StateLicenceDate = null;
                //    StateLicenceNo = null;
                //}
                //if (rdoYesW.Checked == true)
                //{
                //    WDRALicenceDate = getDate_MDY(txtWDRALDate.Text);
                //    WDRALicenceNO = txtWDRALicenceNo.Value;
                //}
                //else if (rdoNoW.Checked == true)
                //{
                //    WDRALicenceDate = null;
                //    WDRALicenceNO = null;
                //}
              
                qry = "INSERT INTO [tbl_WarehouseRegistration_Log] select [TId],[Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[WDRA_LicenseNo],[WDRA_LicenseDate],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],[RegAmt],[RegCapacity],[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],[DistFNBranch],[Warehouse_landmark],[InchDesignation],[Phase],[WarehouseHiredtype],[MPWLC_Type],[Casts],[W_Block],[Reg_Season] from [tbl_WarehouseRegistration] where EmailID='" + Session["email"].ToString() + "' and Registration_Id='" + Session["Reg_No"].ToString() + "' ";
                cmd = new SqlCommand(qry, con, sqltran);
                int A3 = 0;
                A3 = cmd.ExecuteNonQuery();
                if (A3 == 1)
                {
                    //qry = "UPDATE [tbl_WarehouseRegistration] SET [DistrictId] = '" + ddlWarDistrict.SelectedValue.ToString() + "',[BranchId] = '" + ddlBranch.SelectedValue.ToString() + "',[Warehouse_Name] = '" + txtWarehouseName.Value + "',[Mobile_No] = '" + txtWareContactNo.Text + "',[Warehouse_Address] = '" + txtWareAddress.Value + "',[TehsilID] = '" + ddlBlock.SelectedValue.ToString() + "',[Warehouse_LicenseNo] = '" + StateLicenceNo + "',[Warehouse_LicenseDate] = '" + StateLicenceDate + "',[WDRA_LicenseNo] = '" + WDRALicenceNO + "',[WDRA_LicenseDate] = '" + WDRALicenceDate + "',[Incharge_Name] = '" + txtIncharge.Value + "',[Incharge_MobileNo] = '" + txtInchMob.Text + "',[Incharge_EmailID] = '" + txtInchargeEmail.Value + "',[Incharge_Address] = '" + txtInchAdd.Value + "',[Account_No] ='" + txtAccNo.Text + "',[IFSC_Code] = '" + txtIFSC.Value + "',[UpdateBy] = '" + ip + "',[UpdatedDate] = GETDATE(),[DistFNBranch] = '" + txtDistance.Text + "',[Warehouse_landmark]='" + txtLandmark.Value + "',[InchDesignation]='" + txtDesign.Value + "',[Casts]='" + ddlCast.SelectedItem.Text + "',[W_Block]='" + ddlblocknew.SelectedValue + "' where EmailID='" + Session["email"].ToString() + "' and Registration_Id='" + Session["Reg_No"].ToString() + "'";
                    qry = "UPDATE [tbl_WarehouseRegistration] SET [DistrictId] = '" + ddlWarDistrict.SelectedValue.ToString() + "',[BranchId] = '" + ddlBranch.SelectedValue.ToString() + "',[Warehouse_Name] = '" + txtWarehouseName.Value + "',[Mobile_No] = '" + txtWareContactNo.Text + "',[Warehouse_Address] = '" + txtWareAddress.Value + "',[TehsilID] = '" + ddlBlock.SelectedValue.ToString() + "',[Incharge_Name] = '" + txtIncharge.Value + "',[Incharge_MobileNo] = '" + txtInchMob.Text + "',[Incharge_EmailID] = '" + txtInchargeEmail.Value + "',[Incharge_Address] = '" + txtInchAdd.Value + "',[Account_No] ='" + txtAccNo.Text + "',[IFSC_Code] = '" + txtIFSC.Value + "',[UpdateBy] = '" + ip + "',[UpdatedDate] = GETDATE(),[DistFNBranch] = '" + txtDistance.Text + "',[Warehouse_landmark]='" + txtLandmark.Value + "',[InchDesignation]='" + txtDesign.Value + "',[Casts]='" + ddlCast.SelectedItem.Text + "',[W_Block]='" + ddlblocknew.SelectedValue + "',[IsActive]='Y' where EmailID='" + Session["email"].ToString() + "' and Registration_Id='" + Session["Reg_No"].ToString() + "'";
                    cmd = new SqlCommand(qry, con, sqltran);
                    int A4 = 0;
                    A4 = cmd.ExecuteNonQuery();
                    if (A4 == 1)
                    {
                        
                        string WeighCertified = "";
                        string WCertifiedValidity = "";
                        string ConType = "";
                        string HardwareAvl = "";
                        string Installationyear = "";

                        if (ddlElectWeigh.SelectedItem.Text == "No")
                        {
                            WeighCertified = "0";
                            txtWeighCpt.Text = "0";
                            WCertifiedValidity = "";
                        }
                        else if (ddlElectWeigh.SelectedItem.Text == "Yes")
                        {
                            WeighCertified = ddlElectWeigh.SelectedValue;
                            WCertifiedValidity = txtWB_calibrationdate.Text;
                        }
                        if (ddlInternetCon.SelectedItem.Text == "No")
                        {
                            ConType = "0";
                            HardwareAvl = "0";
                            Installationyear = "0";
                        }
                        else if (ddlInternetCon.SelectedItem.Text == "Yes")
                        {
                            ConType = ddlConType.SelectedValue;
                            HardwareAvl = ddlHardwareAvl.SelectedValue;
                            Installationyear = ddlInstallationyear.SelectedValue;
                        }
                        string WMachn = ""; string WMachCpt = "0";
                        if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "No")
                        {
                            WMachn = "0"; WMachCpt = "0";
                        }
                        else if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "Yes")
                        {
                            WMachn = ddl_W_Machine.SelectedValue.ToString();
                            WMachCpt = txtWMCpt.Text;
                        }
                        int chkfac = 0;
                        chkfac = checkWRFacility();
                        if (chkfac == 1)
                        {
                            qry = "INSERT INTO [tbl_WarehouseAdditionalinfo_log] select * from [tbl_WarehouseAdditionalinfo] where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                            cmd = new SqlCommand(qry, con, sqltran);
                            int a = cmd.ExecuteNonQuery();
                            if (a == 1)
                            {
                                qry = "UPDATE [tbl_WarehouseAdditionalinfo] SET [Latitude] = '" + txtlat.Text + "',[Longitude] = '" + txtlong.Text + "',[RoadType] = '" + ddlRoadType.SelectedValue.ToString() + "',[RoadWidth] = '" + txtRoadWidth.Text + "',[GateType] = '" + ddlGateType.SelectedValue.ToString() + "',[NoOfGate] = '" + txtNoGate.Text + "',[PowerSupply] = '" + ddlPowersuply.SelectedValue.ToString() + "',[HighTentionLine] = '" + ddlTentionline.SelectedValue.ToString() + "',[WaterFac] = '" + ddlWaterFac.SelectedValue.ToString() + "',[WaterOtherUsage] = '" + ddlWaterSpry.SelectedValue.ToString() + "',[CCTV] = '" + ddlCCTV.SelectedValue.ToString() + "',[FumigationEqup] = '" + ddlFumigation.SelectedValue.ToString() + "',[GuardWithRoom] = '" + ddlGuard.SelectedValue.ToString() + "',[PlanksDunnage] = '" + ddlPlanks.SelectedValue.ToString() + "',[FireBuckets] ='" + ddlFireBuc.SelectedValue.ToString() + "',[FireExting] = '" + ddlFireExt.SelectedValue.ToString() + "',[WeighBridge] = '" + ddlElectWeigh.SelectedValue.ToString() + "',[WBCertified] = '" + WeighCertified.ToString() + "',[WBCapacity] = '" + txtWeighCpt.Text + "',[InterentCon] = '" + ddlInternetCon.SelectedValue.ToString() + "',[ConType] = '" + ConType.ToString() + "',[HardAvl] = '" + HardwareAvl.ToString() + "',[InstallationYear] = '" + Installationyear.ToString() + "',[UpdateBy] = '" + ip + "',[UpdateDate] = GETDATE(),[DistanceFHighway] = '" + txtHighway.Text + "',[DistanceFRailS] = '" + txtRailway.Text + "',[DistanceFMandi] = '" + txtMandi.Text + "',[DistanceFGoodS] = '" + txtGS.Text + "',[Weighing_Machine]='" + WMachn + "',[WB_CalibrationExpDate]='" + getDate_MDY(WCertifiedValidity) + "' , [Weighinh_M_Capacity]='" + WMachCpt + "',[W_BoundaryType]='" + ddlboundrytype.SelectedValue.ToString().Trim() + "'  where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int a1 = cmd.ExecuteNonQuery();
                                if (a1 == 1)
                                {
                                    qry = "insert into tbl_WarehouseGodown_Reg_Log select * from tbl_WarehouseGodown_Reg where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int A9 = cmd.ExecuteNonQuery();
                                    if (A9 > 0)
                                    {
                                        int rcount=0;
                                        for (int i=0; gvGodown.Rows.Count > i; i++)
                                        {
                                            qry = "update tbl_WarehouseGodown_Reg set LicNo='" + ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text.ToString().Trim() + "',LicType='" + ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedValue.ToString() + "' , LicIssueDate='" + getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim()) + "' ,LicValidityDate='" + getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim()) + "' , UpdateBy='" + ip + "' , UpdatedDate=GETDATE() where Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_ID='" + gvGodown.Rows[i].Cells[9].Text.ToString().Trim() + "'";
                                            cmd = new SqlCommand(qry, con, sqltran);
                                            int A11 = cmd.ExecuteNonQuery();
                                            if (A11 == 1)
                                            {
                                                rcount = rcount + 1;
                                            }
                                        }
                                        if (rcount == gvGodown.Rows.Count)
                                        {
                                            chkupdate = 1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (chkfac == 0)
                            {
                                qry = "INSERT INTO [tbl_WarehouseAdditionalinfo] (Registration_ID,[Latitude],[Longitude],[RoadType],[RoadWidth],[GateType],[NoOfGate],[PowerSupply],[HighTentionLine],[WaterFac],[WaterOtherUsage],[CCTV],[FumigationEqup],[GuardWithRoom],[PlanksDunnage],[FireBuckets],[FireExting],[WeighBridge],[WBCertified],[WBCapacity],[InterentCon],[ConType],[HardAvl],[InstallationYear],[Createdby],[CreatedDate],DistanceFHighway,DistanceFRailS,DistanceFMandi,DistanceFGoodS,[Weighing_Machine],[WB_CalibrationExpDate], [Weighinh_M_Capacity],[W_BoundaryType] ) VALUES ('" + Session["Reg_No"].ToString() + "','" + txtlat.Text + "','" + txtlong.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + txtRoadWidth.Text + "','" + ddlGateType.SelectedValue.ToString() + "','" + txtNoGate.Text + "','" + ddlPowersuply.SelectedValue.ToString() + "','" + ddlTentionline.SelectedValue.ToString() + "','" + ddlWaterFac.SelectedValue.ToString() + "','" + ddlWaterSpry.SelectedValue.ToString() + "','" + ddlCCTV.SelectedValue.ToString() + "','" + ddlFumigation.SelectedValue.ToString() + "','" + ddlGuard.SelectedValue.ToString() + "','" + ddlPlanks.SelectedValue.ToString() + "','" + ddlFireBuc.SelectedValue.ToString() + "','" + ddlFireExt.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + WeighCertified.ToString() + "','" + txtWeighCpt.Text + "','" + ddlInternetCon.SelectedValue.ToString() + "','" + ConType.ToString() + "','" + HardwareAvl.ToString() + "','" + Installationyear.ToString() + "','" + ip + "',GETDATE(),'" + Convert.ToDecimal(txtHighway.Text) + "','" + Convert.ToDecimal(txtRailway.Text) + "','" + Convert.ToDecimal(txtMandi.Text) + "','" + Convert.ToDecimal(txtGS.Text) + "','" + WMachn + "','" + getDate_MDY(txtWB_calibrationdate.Text) + "','" + WMachCpt + "','" + ddlboundrytype.SelectedValue.ToString().Trim() + "') ";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int a = cmd.ExecuteNonQuery();
                                if (a == 1)
                                {

                                    qry = "insert into tbl_WarehouseGodown_Reg_Log select * from tbl_WarehouseGodown_Reg where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int A9 = cmd.ExecuteNonQuery();
                                    if (A9 > 0)
                                    {
                                        int rcount = 0;
                                        for (int i = 0; gvGodown.Rows.Count > i; i++)
                                        {
                                            qry = "update tbl_WarehouseGodown_Reg set LicNo='" + ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text.ToString().Trim() + "',LicType='" + ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedValue.ToString() + "' , LicIssueDate='" + getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim()) + "' ,LicValidityDate='" + getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim()) + "' , UpdateBy='" + ip + "' , UpdatedDate=GETDATE() where Registration_Id='" + Session["Reg_No"].ToString() + "' and Godown_ID='" + gvGodown.Rows[i].Cells[9].Text.ToString().Trim() + "'";
                                            cmd = new SqlCommand(qry, con, sqltran);
                                            int A11 = cmd.ExecuteNonQuery();
                                            if (A11 == 1)
                                            {
                                                rcount = rcount + 1;
                                            }
                                        }
                                        if (rcount == gvGodown.Rows.Count)
                                        {
                                            chkupdate = 1;
                                        }
                                    }
                                }
                            }
                        }
                       // Insert_Registration_Detail2();
                    }
                }
               // Insert_Registration_Warehouse_detail();
            }
        }
    }

    protected void FileUploadComplete(object sender, EventArgs e)
    {

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
          //  converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
            return converted;
        }
    }

    protected void ddlBBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowIndex++;
                }
            }
        }
    }


    protected void ddlWarDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
       // getDepot();
        Get_Blocks();
        get_TehsilBlock();

    }
    public void Get_Blocks()
    {
        ddlBlock.DataSource = null;
        ddlBlock.DataBind();
        string DistrictId = ddlWarDistrict.SelectedValue.ToString();
        string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlBlock.DataSource = ds.Tables[0];
            ddlBlock.DataTextField = "Tehsil_Name";
            ddlBlock.DataValueField = "TehsilCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, "--Select--");
        }
    }
    private void getDepot()
    {
        try
        {
            //string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
            string str = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi WHERE mbi.BranchID in (select [Branch_ID] from tbl_Branch_Block_Mapping where [Block_ID]='" + ddlblocknew.SelectedValue.ToString().Trim() + "')";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   FillBankBranches();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No"; //Session["RefreshButton"];
    }

    
    protected void gvGodown_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void ddlElectWeigh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlElectWeigh.SelectedItem.Text == "Yes")
        {
            Label1.Visible = true;
            Label2.Visible = true;
            txtWeighCpt.Visible = true;
            ddlWeighCertified.Visible = true;
            lblWB.Visible = true;
            txtWB_calibrationdate.Visible = true;
        }
        else
        {
            txtWeighCpt.Visible = false;
            Label1.Visible = false;
            Label2.Visible = false;
            ddlWeighCertified.Visible = false;
            lblWB.Visible = false;
            txtWB_calibrationdate.Visible = false;
            tr_W_Machine.Visible = true;
        }
    }
    protected void ddlInternetCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInternetCon.SelectedItem.Text == "Yes")
        {
            Label4.Visible = true;
            Label3.Visible = true;
            Label5.Visible = true;
            ddlConType.Visible = true;
            ddlHardwareAvl.Visible = true;
            ddlInstallationyear.Visible = true;
        }
        else
        {
            Label4.Visible = false;
            Label3.Visible = false;
            Label5.Visible = false;
            ddlConType.Visible = false;
            ddlHardwareAvl.Visible = false;
            ddlInstallationyear.Visible = false;
        }
    }
    public void Insert_Registration_Detail2()
    {
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string WeighCertified = "";
        string ConType = "";
        string HardwareAvl = "";
        string Installationyear = "";

            if (ddlElectWeigh.SelectedItem.Text == "No")
            {
                WeighCertified = "0";
                txtWeighCpt.Text = "0";
            }
            else if (ddlElectWeigh.SelectedItem.Text == "Yes")
            {
                WeighCertified = ddlElectWeigh.SelectedValue;
            }
            if (ddlInternetCon.SelectedItem.Text == "No")
            {
                ConType = "0";
                HardwareAvl = "0";
                Installationyear = "0";
            }
            else if (ddlInternetCon.SelectedItem.Text == "Yes")
            {
                ConType = ddlConType.SelectedValue;
                HardwareAvl = ddlHardwareAvl.SelectedValue;
                Installationyear = ddlInstallationyear.SelectedValue;
            }
            string WMachn = ""; string WMachCpt = "";
            if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "No")
            {
                WMachn = "0"; WMachCpt = "0";
            }
            else if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "Yes")
            {
                WMachn = ddl_W_Machine.SelectedValue.ToString();
                WMachCpt = txtWMCpt.Text;
            }
            else if (ddlElectWeigh.SelectedItem.Text == "Yes")
            {
                WMachn = "0"; WMachCpt = "0";
            }
            int chkfac = 0;
            chkfac = checkWRFacility();
            if (chkfac == 1)
            {
                qry = "INSERT INTO [tbl_WarehouseAdditionalinfo_log] select * from [tbl_WarehouseAdditionalinfo] where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                cmd = new SqlCommand(qry, con, sqltran);
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    string qry1 = "UPDATE [tbl_WarehouseAdditionalinfo] SET [Latitude] = '" + txtlat.Text + "',[Longitude] = '" + txtlong.Text + "',[RoadType] = '" + ddlRoadType.SelectedValue.ToString() + "',[RoadWidth] = '" + txtRoadWidth.Text + "',[GateType] = '" + ddlGateType.SelectedValue.ToString() + "',[NoOfGate] = '" + txtNoGate.Text + "',[PowerSupply] = '" + ddlPowersuply.SelectedValue.ToString() + "',[HighTentionLine] = '" + ddlTentionline.SelectedValue.ToString() + "',[WaterFac] = '" + ddlWaterFac.SelectedValue.ToString() + "',[WaterOtherUsage] = '" + ddlWaterSpry.SelectedValue.ToString() + "',[CCTV] = '" + ddlCCTV.SelectedValue.ToString() + "',[FumigationEqup] = '" + ddlFumigation.SelectedValue.ToString() + "',[GuardWithRoom] = '" + ddlGuard.SelectedValue.ToString() + "',[PlanksDunnage] = '" + ddlPlanks.SelectedValue.ToString() + "',[FireBuckets] ='" + ddlFireBuc.SelectedValue.ToString() + "',[FireExting] = '" + ddlFireExt.SelectedValue.ToString() + "',[WeighBridge] = '" + ddlElectWeigh.SelectedValue.ToString() + "',[WBCertified] = '" + WeighCertified.ToString() + "',[WBCapacity] = '" + txtWeighCpt.Text + "',[InterentCon] = '" + ddlInternetCon.SelectedValue.ToString() + "',[ConType] = '" + ConType.ToString() + "',[HardAvl] = '" + HardwareAvl.ToString() + "',[InstallationYear] = '" + Installationyear.ToString() + "',[UpdateBy] = '" + Client_Ip.ToString() + "',[UpdateDate] = GETDATE(),[DistanceFHighway] = '" + txtHighway.Text + "',[DistanceFRailS] = '" + txtRailway.Text + "',[DistanceFMandi] = '" + txtMandi.Text + "',[DistanceFGoodS] = '" + txtGS.Text + "',[Weighing_Machine]='" + WMachn + "',[WB_CalibrationExpDate]='" + getDate_MDY(txtWB_calibrationdate.Text) +"' , [Weighinh_M_Capacity]='" + WMachCpt + "'  where Registration_ID='" + Session["Reg_No"].ToString() + "' ";
                    SqlCommand cmd1 = new SqlCommand(qry1, con, sqltran);
                    int a1 = cmd1.ExecuteNonQuery();
                    if (a1 == 1)
                    {
                        chkupdate = 1;
                    }
                }
            }
            else
            {
                if (chkfac == 0)
                {
                       qry = "INSERT INTO [tbl_WarehouseAdditionalinfo] (Registration_ID,[Latitude],[Longitude],[RoadType],[RoadWidth],[GateType],[NoOfGate],[PowerSupply],[HighTentionLine],[WaterFac],[WaterOtherUsage],[CCTV],[FumigationEqup],[GuardWithRoom],[PlanksDunnage],[FireBuckets],[FireExting],[WeighBridge],[WBCertified],[WBCapacity],[InterentCon],[ConType],[HardAvl],[InstallationYear],[Createdby],[CreatedDate],DistanceFHighway,DistanceFRailS,DistanceFMandi,DistanceFGoodS,[Weighing_Machine],[WB_CalibrationExpDate], [Weighinh_M_Capacity] ) VALUES ('" + Session["Reg_No"].ToString() + "','" + txtlat.Text + "','" + txtlong.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + txtRoadWidth.Text + "','" + ddlGateType.SelectedValue.ToString() + "','" + txtNoGate.Text + "','" + ddlPowersuply.SelectedValue.ToString() + "','" + ddlTentionline.SelectedValue.ToString() + "','" + ddlWaterFac.SelectedValue.ToString() + "','" + ddlWaterSpry.SelectedValue.ToString() + "','" + ddlCCTV.SelectedValue.ToString() + "','" + ddlFumigation.SelectedValue.ToString() + "','" + ddlGuard.SelectedValue.ToString() + "','" + ddlPlanks.SelectedValue.ToString() + "','" + ddlFireBuc.SelectedValue.ToString() + "','" + ddlFireExt.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + WeighCertified.ToString() + "','" + txtWeighCpt.Text + "','" + ddlInternetCon.SelectedValue.ToString() + "','" + ConType.ToString() + "','" + HardwareAvl.ToString() + "','" + Installationyear.ToString() + "','" + Client_Ip + "',GETDATE(),'" + Convert.ToDecimal(txtHighway.Text) + "','" + Convert.ToDecimal(txtRailway.Text) + "','" + Convert.ToDecimal(txtMandi.Text) + "','" + Convert.ToDecimal(txtGS.Text) + "','" + WMachn + "','" + getDate_MDY(txtWB_calibrationdate.Text) + "','" + WMachCpt + "') ";
                       cmd = new SqlCommand(qry, con, sqltran);
                       int a = cmd.ExecuteNonQuery();
                       if (a == 1)
                       {
                           chkupdate = 1;
                       }
                }
            }
    }

    //public void Insert_Registration_Warehouse_detail()
    //{
    //    string StateLicenceDate = "";
    //    string WDRALicenceDate = "";
    //    string StateLicenceNo = "";
    //    string WDRALicenceNO = "";
    //    if (rdoYes.Checked == true)
    //    {
    //        StateLicenceDate = getDate_MDY(txtSLDate.Text);
    //        StateLicenceNo = txtWLicNo.Value;
    //    }
    //    else if (rdoNo.Checked == true)
    //    {
    //        StateLicenceDate = null;
    //        StateLicenceNo = null;
    //    }
    //    if (rdoYesW.Checked == true)
    //    {
    //        WDRALicenceDate = getDate_MDY(txtWDRALDate.Text);
    //        WDRALicenceNO = txtWDRALicenceNo.Value;
    //    }
    //    else if (rdoNoW.Checked == true)
    //    {
    //        WDRALicenceDate = null;
    //        WDRALicenceNO = null;
    //    }


    //    string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //    string sql3 = "INSERT INTO [tbl_WarehouseRegistration_Log] select [TId],[Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[WDRA_LicenseNo],[WDRA_LicenseDate],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],[RegAmt],[RegCapacity],[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],[DistFNBranch],[Warehouse_landmark],[InchDesignation],[Phase],[WarehouseHiredtype],[MPWLC_Type],[Casts],[W_Block],[Reg_Season] from [tbl_WarehouseRegistration] where EmailID='" + Session["email"].ToString() + "' and Registration_Id='" + Session["Reg_No"].ToString() + "' ";
    //    SqlCommand cmd3 = new SqlCommand(sql3, con,sqltran);
    //    int A3 = 0;
    //    A3 = cmd3.ExecuteNonQuery();
    //    if (A3 == 1 )
    //    {
    //        string sql4 = "UPDATE [tbl_WarehouseRegistration] SET [DistrictId] = '" + ddlWarDistrict.SelectedValue.ToString() + "',[BranchId] = '" + ddlBranch.SelectedValue.ToString() + "',[Warehouse_Name] = '" + txtWarehouseName.Value + "',[Mobile_No] = '" + txtWareContactNo.Text + "',[Warehouse_Address] = '" + txtWareAddress.Value + "',[TehsilID] = '" + ddlBlock.SelectedValue.ToString() + "',[Warehouse_LicenseNo] = '" + StateLicenceNo + "',[Warehouse_LicenseDate] = '" + StateLicenceDate + "',[WDRA_LicenseNo] = '" + WDRALicenceNO + "',[WDRA_LicenseDate] = '" + WDRALicenceDate + "',[Incharge_Name] = '" + txtIncharge.Value + "',[Incharge_MobileNo] = '" + txtInchMob.Text + "',[Incharge_EmailID] = '" + txtInchargeEmail.Value + "',[Incharge_Address] = '" + txtInchAdd.Value + "',[Account_No] ='" + txtAccNo.Text + "',[IFSC_Code] = '" + txtIFSC.Value + "',[UpdateBy] = '" + Client_Ip + "',[UpdatedDate] = GETDATE(),[DistFNBranch] = '" + txtDistance.Text + "',[Warehouse_landmark]='" + txtLandmark.Value + "',[InchDesignation]='" + txtDesign.Value + "',[Casts]='" + ddlCast.SelectedItem.Text + "',[W_Block]='" + ddlblocknew.SelectedValue + "' where EmailID='" + Session["email"].ToString() + "' and Registration_Id='" + Session["Reg_No"].ToString() + "'";
    //        SqlCommand cmd4 = new SqlCommand(sql4, con,sqltran);
    //        int A4 = 0;
    //        A4 = cmd4.ExecuteNonQuery();
    //        if (A4 == 1)
    //        {
    //            Insert_Registration_Detail2();
    //        }
    //    }
    //}

    //protected void rdoNo_CheckedChanged(object sender, EventArgs e)
    //{
    //    TrStateL.Visible = false;
    //}
    //protected void rdoYes_CheckedChanged(object sender, EventArgs e)
    //{
    //    TrStateL.Visible = true;
    //}
    //protected void rdoNoW_CheckedChanged(object sender, EventArgs e)
    //{
    //    TWDRAL.Visible = false;
    //}
    //protected void rdoYesW_CheckedChanged(object sender, EventArgs e)
    //{
    //    TWDRAL.Visible = true;
    //}

//---------------- Updatecode here ------------------

    public void GetRegisterationData()
    {
        qry = "SELECT WR.DistrictID,WR.BranchId,WR.TehsilID,WP.[Tid],WR.Registration_Id,CONVERT(varchar(10),WR.Registration_Date,103)as Registration_Date  ,[Auth_Person],[MobileNo],WP.Auth_Person,WP.[EmailID],CONVERT(varchar(10),DOB,103) as DOB,[Password],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.EmailID,WR.Mobile_No,WR.Registration_Id,WR.RegAmt,WR.RegCapacity,WP.PAN_No,WP.Aadhar_No,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,CONVERT(varchar(10),WR.Registration_Date,103) as Registration_Date,WR.Incharge_Name,WR.Incharge_Address,WR.Incharge_EmailID,WR.Incharge_MobileNo,WR.WDRA_LicenseNo,CONVERT(varchar(10),WR.WDRA_LicenseDate,103) AS WDRA_LicenseDate,WR.Warehouse_LicenseNo,CONVERT(varchar(10),WR.Warehouse_LicenseDate,103) AS Warehouse_LicenseDate,Bank_Name,IFSC_Code,Account_No,Warehouse_landmark,InchDesignation, (case WHEN WR.Casts='GEN' then '4' WHEN WR.Casts='OBC' then '3' WHEN WR.Casts='ST  ' then '2' WHEN WR.Casts='SC' then '1' else '' end ) Casts,W_Block,WR.IsActive FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode where WR.Registration_Id='" + Session["Reg_No"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblregid.Text = dt.Rows[0]["Registration_Id"].ToString();
            lblregdate.Text = dt.Rows[0]["Registration_Date"].ToString();
            txtWarehouseName.Value = dt.Rows[0]["Warehouse_Name"].ToString();
            txtWareAddress.Value = dt.Rows[0]["Warehouse_Address"].ToString();
            txtWareContactNo.Text = dt.Rows[0]["Mobile_No"].ToString();

            ddlWarDistrict.SelectedValue = dt.Rows[0]["DistrictID"].ToString();

            ddlWarDistrict_SelectedIndexChanged(null, EventArgs.Empty);

            
            txtDistance.Text = dt.Rows[0]["DistFNBranch"].ToString();
            ddlBlock.SelectedValue = dt.Rows[0]["TehsilID"].ToString();
           
            //txtWLicNo.Value = dt.Rows[0]["Warehouse_LicenseNo"].ToString();
            //txtSLDate.Text = dt.Rows[0]["Warehouse_LicenseDate"].ToString();
            //txtWDRALicenceNo.Value = dt.Rows[0]["WDRA_LicenseNo"].ToString();
            //txtWDRALDate.Text = dt.Rows[0]["WDRA_LicenseDate"].ToString();


            txtInchAdd.Value = dt.Rows[0]["Incharge_Address"].ToString();
            txtInchargeEmail.Value = dt.Rows[0]["Incharge_EmailID"].ToString();
            txtIncharge.Value = dt.Rows[0]["Incharge_Name"].ToString();
            txtInchMob.Text = dt.Rows[0]["Incharge_MobileNo"].ToString();
          //  ddlBank.SelectedItem.Value = dt.Rows[0]["Bank_Name"].ToString();
            txtIFSC.Value = dt.Rows[0]["IFSC_Code"].ToString();
            txtAccNo.Text = dt.Rows[0]["Account_No"].ToString();
            txtLandmark.Value = dt.Rows[0]["Warehouse_landmark"].ToString();
            txtDesign.Value = dt.Rows[0]["InchDesignation"].ToString();
            ddlCast.SelectedValue = dt.Rows[0]["Casts"].ToString();
            

            //if (txtWLicNo.Value == "")
            //{
            //    rdoNo.Checked = true;
            //    rdoNo_CheckedChanged(null, EventArgs.Empty);
            //}
            //else
            //{
            //    rdoYes.Checked = true;
            //    rdoYes_CheckedChanged(null, EventArgs.Empty);
            //}
            //if (txtWDRALicenceNo.Value == "")
            //{
            //    rdoNoW.Checked = true;
            //    rdoNoW_CheckedChanged(null, EventArgs.Empty);
            //}
            //else
            //{
            //    rdoYesW.Checked = true;
            //    rdoYesW_CheckedChanged(null, EventArgs.Empty);
            //}

            if (dt.Rows[0]["W_Block"].ToString() != "")
            {
                ddlblocknew.SelectedValue = dt.Rows[0]["W_Block"].ToString();
            }

            ddlblocknew_SelectedIndexChanged(null, null);
            ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
        }
    }
    public void GetWFacility()
    {
        try
        {
            qry = "select Latitude,Longitude,RoadWidth,NoOfGate,convert(int,WBCapacity) as WBCapacity,InstallationYear,DistanceFGoodS,DistanceFHighway,DistanceFMandi,DistanceFRailS,PowerSupply,RoadType,GateType,case when HighTentionLine=1 then '1' when HighTentionLine=0 then '0' else '-' end HighTentionLine,case when WaterFac=1 then '1' when WaterFac=0 then '0' else '-' end WaterFac,case when WaterOtherUsage=1 then '1' when WaterOtherUsage=0 then '0' else '-' end WaterOtherUsage,case when CCTV=1 then '1' when CCTV=0 then '0' else '-' end CCTV,case when FumigationEqup=1 then '1' when FumigationEqup=0 then '0' else '-' end FumigationEqup,case when GuardWithRoom=1 then '1' when GuardWithRoom=0 then '0' else '-' end GuardWithRoom,case when PlanksDunnage=1 then '1' when PlanksDunnage=0 then '0' else '-' end PlanksDunnage,case when FireBuckets=1 then '1' when FireBuckets=0 then '0' else '-' end FireBuckets,case when FireExting=1 then '1' when FireExting=0 then '0' else '-' end FireExting,case when WeighBridge=1 then '1' when WeighBridge=0 then '0' else '-' end WeighBridge,case when WBCertified=1 then '1' when WBCertified=0 then '0' else '-' end WBCertified,case when InterentCon=1 then '1' when InterentCon=0 then '0' else '-' end InterentCon,ConType,case when HardAvl=1 then '1' when HardAvl=0 then '0' else '-' end HardAvl, case when Weighing_Machine=1 then '1' when Weighing_Machine=0 then '0' else '-' end Weighing_Machine,Weighinh_M_Capacity,Convert(varchar(10),WB_CalibrationExpDate,103) as WB_CalibrationExpDate,W_BoundaryType from tbl_WarehouseAdditionalinfo where Registration_Id='" + Session["Reg_No"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtlat.Text = dt.Rows[0]["Latitude"].ToString();
                txtlong.Text = dt.Rows[0]["Longitude"].ToString();
                ddlRoadType.SelectedValue = dt.Rows[0]["RoadType"].ToString();
                txtRoadWidth.Text = dt.Rows[0]["RoadWidth"].ToString();
                txtNoGate.Text = dt.Rows[0]["NoOfGate"].ToString();
                txtWeighCpt.Text = dt.Rows[0]["WBCapacity"].ToString();
                ddlInstallationyear.SelectedValue = dt.Rows[0]["InstallationYear"].ToString();
                txtGS.Text = dt.Rows[0]["DistanceFGoodS"].ToString();
                txtHighway.Text = dt.Rows[0]["DistanceFHighway"].ToString();
                txtRailway.Text = dt.Rows[0]["DistanceFRailS"].ToString();
                txtMandi.Text = dt.Rows[0]["DistanceFMandi"].ToString();
                ddlPowersuply.SelectedValue = dt.Rows[0]["PowerSupply"].ToString();
                ddlGateType.SelectedValue = dt.Rows[0]["GateType"].ToString();
                ddlTentionline.SelectedValue = dt.Rows[0]["HighTentionLine"].ToString();
                ddlWaterFac.SelectedValue = dt.Rows[0]["WaterFac"].ToString();
                ddlWaterSpry.SelectedValue = dt.Rows[0]["WaterOtherUsage"].ToString();
                ddlCCTV.SelectedValue = dt.Rows[0]["CCTV"].ToString();
                ddlFumigation.SelectedValue = dt.Rows[0]["FumigationEqup"].ToString();
                ddlGuard.SelectedValue = dt.Rows[0]["GuardWithRoom"].ToString();
                ddlPlanks.SelectedValue = dt.Rows[0]["PlanksDunnage"].ToString();
                ddlFireBuc.SelectedValue = dt.Rows[0]["FireBuckets"].ToString();
                ddlFireExt.SelectedValue = dt.Rows[0]["FireExting"].ToString();
                ddlElectWeigh.SelectedValue = dt.Rows[0]["WeighBridge"].ToString();
                ddlWeighCertified.SelectedValue = dt.Rows[0]["WBCertified"].ToString();
                ddlInternetCon.SelectedValue = dt.Rows[0]["InterentCon"].ToString();
                ddlConType.SelectedValue = dt.Rows[0]["ConType"].ToString();
                ddlHardwareAvl.SelectedValue = dt.Rows[0]["HardAvl"].ToString();
                ddlElectWeigh_SelectedIndexChanged(null, EventArgs.Empty);
                ddlInternetCon_SelectedIndexChanged(null, EventArgs.Empty);

                txtWB_calibrationdate.Text = dt.Rows[0]["WB_CalibrationExpDate"].ToString();
                txtWMCpt.Text = dt.Rows[0]["Weighinh_M_Capacity"].ToString();
                if (dt.Rows[0]["Weighing_Machine"].ToString() != "" && ddlElectWeigh.SelectedItem.Text!="Yes")
                {
                    ddl_W_Machine.SelectedValue = dt.Rows[0]["Weighing_Machine"].ToString();
                }
                ddlboundrytype.SelectedValue = dt.Rows[0]["W_BoundaryType"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GetPreReg()
    {
        try
        {
            qry = "select EmailID,Reg_No,ApplicantType,Auth_Person,PAN_No,Aadhar_No,MobileNo,DistrictID from tbl_Warehouse_PreReg where Reg_No='" + Session["Reg_No"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtAuthPerson.Value = dt.Rows[0]["Auth_Person"].ToString();
                txtPAN.Value = dt.Rows[0]["PAN_No"].ToString();
                txtAadharNo.Value = dt.Rows[0]["Aadhar_No"].ToString();
                ddlAppType.SelectedValue = dt.Rows[0]["ApplicantType"].ToString();
                ddlDistrict.SelectedValue = dt.Rows[0]["DistrictID"].ToString();
                lblEmail.Text = dt.Rows[0]["EmailID"].ToString();
                lblMob.Text = dt.Rows[0]["MobileNo"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual
       // DateTime _effective_date = Convert.ToDateTime("01/01/2020 11:59:00 AM");
       // DateTime _Closing_date = Convert.ToDateTime("04/10/2022 11:59:00 PM");

        DateTime _effective_date = Convert.ToDateTime("03/08/2022 10:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("04/10/2028 10:59:00 PM");

        ////Old
        //  DateTime _effective_date = Convert.ToDateTime("2018-02-19 14:27:28.150");
        //  DateTime _Closing_date = Convert.ToDateTime("2019-02-19 14:27:28.150");

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
    public void GetGdwn()
    {
        qry = "select Godown_ID,Godown_No,CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Width) as G_Width,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,LicType,LicNo,convert(varchar(10),LicIssueDate,103)LicIssueDate ,convert(varchar(10),LicValidityDate,103) LicValidityDate from tbl_WarehouseGodown_Reg where G_ScientificCapacity > 0 and Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds;
            gvGodown.DataBind();
            this.gvGodown.Columns[9].Visible = false;
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            for (int i = 0; gvGodown.Rows.Count > i; i++)
            {
                 //var a = dt.Rows[i]["LicType"].ToString().Trim();
                 //if (a == "WDRA")
                 //{
                 //    ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedIndex = 1;
                 //}
                 //else if (a == "NON WDRA")
                 //{
                 //    ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedIndex = 2;
                 //}
                 //else if (a == "APPLIED")
                 //{
                 //    ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedIndex = 2;
                 //}

                ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedValue = dt.Rows[i]["LicType"].ToString().Trim();
                ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text = dt.Rows[i]["LicNo"].ToString();
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text = dt.Rows[i]["LicIssueDate"].ToString();
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text = dt.Rows[i]["LicValidityDate"].ToString();
            }
        }
    }
    public int checklicnodatevalidation()
    {
        int WF = 0;
        int ch = 0;
        var formatedDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        for (int i = 0; gvGodown.Rows.Count > i; i++)
        {
            var Issuedate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim());
            var Validdate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim());
            if (((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Licence Type..'); </script> ");
                break;
            }
            else if (((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text.ToString().Trim() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Number..'); </script> ");
                ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Focus();
                break;
            }
            else if (((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Issue Date..'); </script> ");
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Focus();
                break;
            }
            else if (Convert.ToDateTime(Issuedate) > Convert.ToDateTime(formatedDate))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Godown Licence issue date/Application Date is cannot be greater then today...'); </script> ");
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Focus();
                break;
            }
            else if (((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim() == "" && ((((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "WDRA") || ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "NON WDRA"))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Expiry Date..'); </script> ");
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Focus();
                break;
            }
            else if (((((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "WDRA") || ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "NON WDRA") && Convert.ToDateTime(Validdate) < Convert.ToDateTime(formatedDate))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Godown Licence Expiry Date is cannot be Less then today...'); </script> ");
                ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Focus();
                break;
            }
            else
            {
                ch = ch + 1;
                if (gvGodown.Rows.Count == ch)
                {
                    WF = 1;
                }
            }
            
        }
        return WF;
    }
    public void get_TehsilBlock()
    {
        ddlblocknew.DataSource = null;
        ddlblocknew.DataBind();
        string DistrictId = ddlWarDistrict.SelectedValue.ToString();
        string qry = "select distinct [Block_ID],[Block_Name] from tbl_Branch_Block_Mapping where [District_ID]='" + ddlWarDistrict.SelectedValue.ToString() + "' and Block_ID is not null ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlblocknew.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlblocknew.DataSource = ds.Tables[0];
            ddlblocknew.DataTextField = "Block_Name";
            ddlblocknew.DataValueField = "Block_ID";
            ddlblocknew.DataBind();
            ddlblocknew.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  get_TehsilBlock();
    }
    public void GetGdwnLicData()
    {
        try
        {
            qry = "select Godown_ID,LicType,LicNo,convert(varchar(10),LicIssueDate,103)LicIssueDate ,convert(varchar(10),LicValidityDate,103) LicValidityDate from tbl_WarehouseGodown_Reg where Registration_Id='" + Session["Reg_No"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i=0; dt.Rows.Count > 0; i++)
                {
                    ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedValue = dt.Rows[i]["LicType"].ToString();
                    ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text = dt.Rows[i]["LicNo"].ToString();
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text = dt.Rows[i]["LicIssueDate"].ToString();
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text = dt.Rows[i]["LicValidityDate"].ToString();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlblocknew_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot();
    }
    public int chkofrstopupdate()
    {
        int ch = 0;
        //string strsql = "select * from tbl_Warehouse_Capacity_Offer_2019 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        //string strsql = "select * from tbl_Warehouse_Capacity_Offer_2020 where Registration_Id='" + Session["Reg_No"].ToString() + "'";
        string strsql = "select * from tbl_Warehouse_Capacity_Offer_Rabi2023 where Registration_Id='" + Session["Reg_No"].ToString() + "'";

        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ch = 1;
        }
        else
        {
            ch = 0;
        }
        return ch;
    }
}