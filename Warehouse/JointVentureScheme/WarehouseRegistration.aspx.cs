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
using System.Collections;
using System.Resources;
using System.Text;

public partial class JointVentureScheme_WarehouseRegistration : System.Web.UI.Page
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
    string R_Phase = "";
    string Reg_season = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        CalendarExtender1.StartDate = DateTime.Now;
     //   CalendarExtender2.StartDate = DateTime.Now; 
        if ((Session["email"] != null) && (Session["mobile"] != null))
        {
            if (Session["Reg_No"] == null || Session["Reg_No"]=="")
            {
                lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
                lblAuthPerson.Text = Session["fname"].ToString();
                lblEmail.Text = Session["email"].ToString();
                lblMob.Text = Session["mobile"].ToString();
                //lblDate.Text = Session["DOB"].ToString();
                lblAppType.Text = Session["AppType"].ToString();
                lblDistrict.Text = Session["District"].ToString();
                R_Phase = "1";
                //Reg_season = "R2019";
                //Reg_season = "K2019";
                Reg_season = "JVS2020_21";
                if (!IsPostBack)
                {
                    get_Districts();
                    SetInitialRow();
                }
            }
            else
            {
              //  ModalPopupExtender1.Show();
            } 
        }
        else
        {

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
         int ch=0;
         string strsql = "select EmailID from tbl_WarehouseRegistration where EmailID='" + Session["email"].ToString() + "'";

         SqlDataAdapter da = new SqlDataAdapter(strsql, con);
         DataSet ds = new DataSet();
         da.Fill(ds);
         if (ds.Tables[0].Rows.Count > 0)
         {
             //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपका रजिस्ट्रेशन हो चुका है कृपया चेक करे...'); </script> ");
             //txtREgemail.Focus();
             ch = 1;
         }
         else
         {
             ch = 0;

         }
         return ch; 
     }
     public string Tcheckdatetimes()
     {
         DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual

        //DateTime _effective_date = Convert.ToDateTime("10/14/2019 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("10/30/2019 11:59:00 PM");

        //server
        DateTime _effective_date = Convert.ToDateTime("02/20/2021 11:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("02/19/2028 11:59:00 PM");

        ////Local pages savan
        //DateTime _effective_date = Convert.ToDateTime("20/12/2021 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("28/01/2028 11:59:00 PM");
        ////Old
        //   DateTime _effective_date = Convert.ToDateTime("2018-01-18 12:55:05.870");
        //   DateTime _Closing_date = Convert.ToDateTime("2020-01-18 12:55:05.870");

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
     protected void btnsubmit_Click(object sender, EventArgs e)
     {
         string TStatus = Tcheckdatetimes();
        
        if (TStatus == "Y")
        {
            //DateTime STDate = Convert.ToDateTime(getDate_MDY(txtSLDate.Text));
            //DateTime WDDate = Convert.ToDateTime(getDate_MDY(txtWDRALDate.Text));
         int SK = 0;
         SK = CheckEmail();
         if (SK == 0)
         {
             if (txtWarehouseName.Value == "")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse Name...'); </script> ");
                 txtWarehouseName.Focus();
             }
             else if (txtWareAddress.Value == "")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse/Office Address with Postal Address:...'); </script> ");
                 txtWareAddress.Focus();
             }
             else if (ddlCast.SelectedItem.Text == "--Select--")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please select Category...'); </script> ");
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

            //Comment Licence Details

             //else if (rdoYes.Checked == false && rdoYesW.Checked == false)
             //{
             //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Atleast One Licence Required :...'); </script> ");
             //    //txtWareAddress.Focus();
             //}
             //else if (rdoYes.Checked == true && (txtWLicNo.Value == "" || txtSLDate.Text == ""))
             //{
             //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse State Licence No & Date...'); </script> ");
             //}
             //else if (rdoYesW.Checked == true && (txtWDRALicenceNo.Value == "" || txtWDRALDate.Text == ""))
             //{
             //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter WDRA Registration No & date :...'); </script> ");
             //}
             //else if (rdoYes.Checked == true && (STDate.Date < DateTime.Now.Date))
             //{
             //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid State Licence Date..'); </script> ");
             //}
             //else if (rdoYesW.Checked && (WDDate.Date < DateTime.Now.Date))
             //{
             //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid WDRA Licence Date..'); </script> ");
             //}

                 //Comment End Licence Details

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
             else if (txtInchargeEmail.Value == "")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Email ID...'); </script> ");
                 txtInchargeEmail.Focus();
             }
             else if (lblTotalCapacity.Text == "0.00")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registered Capacity should grater than 0.00'); </script> ");
                 lblTotalCapacity.Focus();
             }
             else if (lblTotalRegAmt.Text == "0.00")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Fees should grater than 0.00'); </script> ");
                 lblTotalCapacity.Focus();
             }
             else if (ddlBank.SelectedItem.Text == "--Select--")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Bank Name...'); </script> ");
                 ddlBank.Focus();
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
             else if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "--Select--")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Weighing Machine Availability ....'); </script> ");
                 txtWMCpt.Focus();
             }
             else if (ddlElectWeigh.SelectedItem.Text == "No" && ddl_W_Machine.SelectedItem.Text == "Yes" && (txtWMCpt.Text == "" || txtWMCpt.Text=="0"))
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Weighing Machine Capacity(In M.T) ....'); </script> ");
                 txtWMCpt.Focus();
             }
             else if (ddlblocknew.SelectedItem.Text == "--Select--")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Block....'); </script> ");
                 ddlblocknew.Focus();
             }
             else if (ddlboundrytype.SelectedItem.Text == "--Select--")
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Warehouse Boundray Type....'); </script> ");
                 ddlboundrytype.Focus();
             }
             else if (txtlat.Text == "" && txtlat.Text.Length != 6)
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Latitude....'); </script> ");
                 txtlat.Focus();
             }
             else if (txtlong.Text == "" && txtlong.Text.Length != 6)
             {
                 ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Longitude....'); </script> ");
                 txtlong.Focus();
             }
             else
             {
                 GetApplicationNo();
                 int chklic = checklicnodatevalidation();
                 if (chklic == 1)
                 {
                     Insert_Registration_Detail();
                 }
                 else
                 {
                     ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Godown Entered data...'); </script> ");
                 }
             }
         }
         else
         {
             Response.Redirect("WarehouseHome.aspx");
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
              
    public void get_Districts()
    {
        string qry = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlCommand cmd = new SqlCommand(qry,con);
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
                else if (INCN.Length>3)
                {
                    AGC = INCN.ToString();
                }
                //App_No = "042017" + "" + "" + ddlCDistrict.SelectedValue.ToString().Substring(2, 2) + AGC.ToString();
                //Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "19" + AGC.ToString();
                Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "20" + AGC.ToString();
                BID = SubBN;
            }
            else
            {
                //Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "19" + "001";
                Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "20" + "001";
                BID = 1;
            }
        }
        else
        {
            //Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "19" + "001";
            Registration_No = ddlBlock.SelectedValue.ToString().Substring(2, 4) + "20" + "001";
            BID = 1;
        }
    }
    public void Insert_Registration_Detail()
    {      
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string BankName = "";
        //if (ddlBank.SelectedItem.Text == "OTHER BANK")
        //{
        //    BankName = txtOBank.Text;
        //}
        //else
        //{
        BankName = ddlBank.SelectedItem.Text;
        //}


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
        //    StateLicenceDate=null;
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
        //byte ss = Convert.ToByte(imageBytes);
        //string sql = "INSERT INTO [tbl_WarehouseRegistration]([Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[WDRA_LicenseNo],[WDRA_LicenseDate],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],RegAmt,RegCapacity,[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],DistFNBranch,Warehouse_landmark,InchDesignation,Phase) VALUES ('" + Registration_No + "','','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + txtWareContactNo.Text + "','','" + lblEmail.Text + "','" + txtWareAddress.Value + "','" + ddlBlock.SelectedValue + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','" + StateLicenceNo + "','" + StateLicenceDate + "','" + WDRALicenceNO + "','" + WDRALicenceDate + "','" + txtIncharge.Value + "','" + txtInchMob.Text + "','" + txtInchargeEmail.Value + "','" + txtInchAdd.Value + "','" + ddlBank.SelectedItem.Text + "','" + txtAccNo.Text + "','" + txtIFSC.Value + "','" + ip + "',GETDATE(),'','','','','N','','" + Convert.ToDecimal(lblTotalRegAmt.Text) + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','N','" + BID + "','null','null',null,'null','null',null,'" + txtDistance.Text + "','" + txtLandmark.Value + "','" + txtDesign.Value + "','6')";
        //string sql = "INSERT INTO [tbl_WarehouseRegistration]([Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[WDRA_LicenseNo],[WDRA_LicenseDate],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],RegAmt,RegCapacity,[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],DistFNBranch,Warehouse_landmark,InchDesignation,Phase,Casts) VALUES ('" + Registration_No + "','','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + txtWareContactNo.Text + "','','" + lblEmail.Text + "','" + txtWareAddress.Value + "','" + ddlBlock.SelectedValue + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','" + StateLicenceNo + "','" + StateLicenceDate + "','" + WDRALicenceNO + "','" + WDRALicenceDate + "','" + txtIncharge.Value + "','" + txtInchMob.Text + "','" + txtInchargeEmail.Value + "','" + txtInchAdd.Value + "','" + ddlBank.SelectedItem.Text + "','" + txtAccNo.Text + "','" + txtIFSC.Value + "','" + ip + "',GETDATE(),'','','','','N','','" + Convert.ToDecimal(lblTotalRegAmt.Text) + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','N','" + BID + "','null','null',null,'null','null',null,'" + txtDistance.Text + "','" + txtLandmark.Value + "','" + txtDesign.Value + "','8','"+ ddlCast.SelectedItem.Text +"')";
        string sql = "INSERT INTO [tbl_WarehouseRegistration]([Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],RegAmt,RegCapacity,[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],DistFNBranch,Warehouse_landmark,InchDesignation,Phase,Casts,W_Block,Reg_Season) VALUES ('" + Registration_No + "','','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + txtWareContactNo.Text + "','','" + lblEmail.Text + "','" + txtWareAddress.Value + "','" + ddlBlock.SelectedValue + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','" + txtIncharge.Value + "','" + txtInchMob.Text + "','" + txtInchargeEmail.Value + "','" + txtInchAdd.Value + "','" + ddlBank.SelectedItem.Text + "','" + txtAccNo.Text + "','" + txtIFSC.Value + "','" + ip + "',GETDATE(),'','','','','N','','" + Convert.ToDecimal(lblTotalRegAmt.Text) + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','N','" + BID + "','null','null',null,'null','null',null,'" + txtDistance.Text + "','" + txtLandmark.Value + "','" + txtDesign.Value + "','" + R_Phase + "','" + ddlCast.SelectedItem.Text + "','" + ddlblocknew.SelectedValue + "','" + Reg_season + "')";

        SqlCommand cmd = new SqlCommand(sql, con);
        //cmd = new SqlCommand(qry, con);
        //int c = cmd.ExecuteNonQuery();
        //SqlParameter[] prms = new SqlParameter[63];

        //,EducationPicName,EducationPicType,EducationPic
        int CT = 0;
        //cmd.Parameters.AddRange(prms);
        con.Open();
        CT = cmd.ExecuteNonQuery();
        con.Close();
        if (CT > 0)
        {
            qry = "update tbl_Warehouse_PreReg set Reg_No='" + Registration_No + "' where EmailID='" + Session["email"].ToString() + "'";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                 con.Open();
                 int CT2 = cmd2.ExecuteNonQuery();
                 con.Close();
                if (CT2 > 0)
                {
                    Insert_Godown_Detail();
                    Insert_Registration_Detail2();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Save Successfully...'); </script> ");
                    Session["Reg_No"] = Registration_No;
                    if (Session["Reg_No"] != null || Session["Reg_No"] != "")
                    {
                        btnprint.Visible = true;
                        btnpayment.Visible = true;
                        btnsubmit.Enabled = false;
                    }

          //  qry = ""; 

          //  Response.Redirect("PrintReg.aspx");
                }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
    }

    public void Insert_Godown_Detail()
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        for (int j = 0; j < gvGodown.Rows.Count; j++)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (((CheckBox)gvGodown.Rows[j].FindControl("ckstack")).Checked == true)
            {
                TextBox Lenghts = (TextBox)gvGodown.Rows[j].Cells[1].FindControl("txtLenght");
                TextBox Widths = (TextBox)gvGodown.Rows[j].Cells[2].FindControl("txtWidth");
                TextBox Heights = (TextBox)gvGodown.Rows[j].Cells[3].FindControl("txtHeight");
                TextBox Capacitys = (TextBox)gvGodown.Rows[j].Cells[5].FindControl("txtCapacity");
                TextBox ConstructionYears = (TextBox)gvGodown.Rows[j].Cells[6].FindControl("txtConstY");

                decimal GodownNo = Convert.ToDecimal(gvGodown.Rows[j].Cells[0].Text.ToString());
                decimal Length = Convert.ToDecimal(Lenghts.Text);
                decimal Width = Convert.ToDecimal(Widths.Text);
                decimal Height = Convert.ToDecimal(Heights.Text);
                decimal Capacity = Convert.ToDecimal(Capacitys.Text);
                string ConstructionY = ConstructionYears.Text;
                string GodownId = Registration_No + (j + 1);
                string qryGd = "INSERT INTO [tbl_WarehouseGodown_Reg] ([Registration_Id],[Godown_ID],[DistrictId],[BranchId],[Registration_Date],[Godown_Name],[Godown_No],[G_Length],[G_Width],[G_Height],[G_ScientificCapacity],[G_MaxCapacity],[G_ConstructedYear],[CreatedBy],[CreatedDate],[IsActive],[AId],LicNo,LicType,LicIssueDate,LicValidityDate) VALUES ('" + Registration_No + "','" + GodownId + "','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + GodownNo + "','" + Length + "','" + Width + "','" + Height + "','" + Capacity + "'," + Convert.ToDecimal(0) + ",'" + ConstructionY + "','" + ClientIP + "',GETDATE(),'Y','" + Convert.ToDecimal(0) + "','" + ((TextBox)gvGodown.Rows[j].FindControl("Gtxtlicno")).Text.ToString().Trim() + "','" + ((DropDownList)gvGodown.Rows[j].FindControl("ddlgdwntype")).SelectedValue.ToString() + "','" + getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicIssuedate")).Text.ToString().Trim()) + "' ,'" + getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicExpdate")).Text.ToString().Trim()) + "')";
                cmd = new SqlCommand(qryGd, con);
                int c = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //Registration_No = Registration_No;
                if (c > 0)
                {

                }
            }
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
            return "01/01/2000";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
           // converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
            return converted;
        }
    }
    //protected string getDate_MDY(string inDate)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    //}

    public void GetBankList()
    {
        ddlBank.Items.Clear();
        string qry = "select distinct BANK from [IfscBankmar15] where districtId='" + ddlWarDistrict.SelectedValue.ToString() + "' order by BANK";
        //string qry = "select distinct BANK from IfscBankmar15_eUP where District_Id='" + ddlWarDistrict.SelectedValue.ToString() + "' order by BANK";

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

            string qry = "select Branch from [IfscBankmar15] where Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlWarDistrict.SelectedValue.ToString() + "' order by BANK";
            //string qry = "select Branch from IfscBankmar15_eUP where Bank='" + ddlBank.SelectedValue.ToString() + "' and District_Id='" + ddlWarDistrict.SelectedValue.ToString() + "' order by BANK";

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
        string qry = "select ID from [IfscBankmar15] where Branch='" + ddlBBranch.SelectedItem.Text + "' and Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlWarDistrict.SelectedValue.ToString() + "'";
        //string qry = "select ID from IfscBankmar15_eUP where Branch='" + ddlBBranch.SelectedItem.Text + "' and Bank='" + ddlBank.SelectedValue.ToString() + "' and District_Id='" + ddlWarDistrict.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtIFSC.Value = dt.Rows[0]["ID"].ToString();
        }
    }


    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["Lenght"] = 0;

                //drCurrentRow["RptAmt"] = 0;
                drCurrentRow["Width"] = 0;
                drCurrentRow["Height"] = 0;
                drCurrentRow["Capacity"] = 0;
                drCurrentRow["ConstY"] = 0;

                drCurrentRow["LNo"] = "";
                drCurrentRow["LIssueDate"] = "";
                drCurrentRow["LExpDate"] = "";
              
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
                    //{
                        //extract the DropDownList Selected Items
                        //DropDownList ddl1 = (DropDownList)gvImprest.Rows[i].Cells[1].FindControl("DropDownList1");
                        TextBox Lenght = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtLenght");
                        TextBox Width = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtWidth");
                        TextBox Height = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtHeight");
                        TextBox Capacity = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtCapacity");
                        TextBox ConstY = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtConstY");

                        DropDownList ddlgdwntype = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddlgdwntype");
                        TextBox LNo = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("Gtxtlicno");
                        TextBox LIssueDate = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("GtxtLicIssuedate");
                        TextBox LExpDate = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("GtxtLicExpdate");


                        //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["Lenght"] = Convert.ToDecimal(Lenght.Text);
                        dtCurrentTable.Rows[i]["Width"] = Convert.ToDecimal(Width.Text);
                        dtCurrentTable.Rows[i]["Height"] = Convert.ToDecimal(Height.Text);
                        dtCurrentTable.Rows[i]["Capacity"] = Convert.ToDecimal(Capacity.Text);
                        dtCurrentTable.Rows[i]["ConstY"] = Convert.ToDecimal(ConstY.Text);


                        dtCurrentTable.Rows[i]["ddlgdwntype"] = ddlgdwntype.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["LNo"] = LNo.Text;
                        dtCurrentTable.Rows[i]["LIssueDate"] = LIssueDate.Text;
                        dtCurrentTable.Rows[i]["LExpDate"] = LExpDate.Text;


                }
             
                //Rebind the Grid with the current data
                gvGodown.DataSource = dtCurrentTable;
                gvGodown.DataBind();
              
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    if (Convert.ToDecimal(((TextBox)gvGodown.Rows[i].FindControl("txtCapacity")).Text) != 0)
                    {
                        ((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked = true;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtLenght")).Enabled = false;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtWidth")).Enabled = false;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtHeight")).Enabled = false;
                    }
                   
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
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
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    //DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
                    DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("ddlgdwntype");
                    //Fill the DropDownList with Data
                    FillDropDownList(ddl1);
                    if (i < dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByText(dt.Rows[i]["ddlgdwntype"].ToString()).Selected = true;
                    }

                    rowIndex++;
                }
            }
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        
                  
        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Lenght", typeof(string)));
        dt.Columns.Add(new DataColumn("Width", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Height", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Capacity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ConstY", typeof(decimal)));

        // add Licence Data

        dt.Columns.Add(new DataColumn("ddlgdwntype", typeof(string)));
        dt.Columns.Add(new DataColumn("LNo", typeof(string)));
        dt.Columns.Add(new DataColumn("LIssueDate", typeof(string)));
        dt.Columns.Add(new DataColumn("LExpDate", typeof(string)));


        // End Licence Data
       

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Lenght"] = 0;
        dr["Width"] = 0;
        dr["Height"] = 0;
        dr["Capacity"] = 0;
        dr["ConstY"] = 0;


        //
        dr["ddlgdwntype"] = "";
        dr["LNo"] = "";
        dr["LIssueDate"] = "";
        dr["LExpDate"] = "";
        //

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        //Bind the DataTable to the Grid
        gvGodown.DataSource = dt;
        gvGodown.DataBind();

        //Extract and Fill the DropDownList with Data
        DropDownList ddl1 = (DropDownList)gvGodown.Rows[0].Cells[1].FindControl("ddlgdwntype");
        FillDropDownList(ddl1);
    }
    private void FillDropDownList(DropDownList ddlgdwntype)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddlgdwntype.Items.Add(item);
        }
    }
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("WDRA", "68"));
        arr.Add(new ListItem("NON WDRA", "63"));
        arr.Add(new ListItem("APPLIED For WDRA", "0"));
        arr.Add(new ListItem("APPLIED For NON WDRA", "00"));
        return arr;
    }
    protected void ddlWarDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
       // getDepot();
        Get_Blocks();
        get_TehsilBlock();
        GetBankList();
    }
    public void Get_Blocks()
    {
        string DistrictId = ddlWarDistrict.SelectedValue.ToString();
        string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlBlock.DataSource = ds1.Tables[0];
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
        FillBankBranches();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No"; //Session["RefreshButton"];
    }
    //private void getCheck()
    //{

    //}
    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        //try
        //{
        //bool calculationflag = true;
        int s;
        int Count_Rows;
        string RowNumber;
        //string WHR_ID;
        decimal TotalCapt = 0;
        decimal TotalCapacity = 0;
        Count_Rows = gvGodown.Rows.Count;
        Server.ScriptTimeout = 11500;
        decimal TotalRCapacity = 0;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {
            //lblTotalCapacity.Text = "0";
            if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
            //if (((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled==true)
            {
                
                if (Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text) >= 4 && Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text) <= 18)
                {
                    RowNumber = gvGodown.Rows[s].Cells[0].Text.ToString();
                    //WHR_ID = gdstackdetail.Rows[s].Cells[3].Text.ToString();
                    decimal Length = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Text.ToString());
                    decimal Width = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Text.ToString());
                    decimal Height = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text.ToString());
                    // Formula for Capacity = [Length*Breadth*(Height-3)/80] 
                    decimal calculate = (Length * Width * (Height - 3) / 80);
                    TotalCapacity = calculate;
                    //decimal Avai_Wgt = Convert.ToDecimal(gvGodown.Rows[s].Cells[6].Text.ToString());
                    ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = false;

                    if (lblTotalCapacity.Text != null && lblTotalCapacity.Text != "")
                    {
                        TotalCapt = TotalCapacity + (Convert.ToDecimal(lblTotalCapacity.Text));
                    }
                    else
                    {
                        TotalCapt = TotalCapacity;
                    }
                    lblTotalCapacity.Text = TotalCapt.ToString();
                    ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = calculate.ToString();                                                 
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Height should be between 4 ft to 18 ft.')", true);
                    ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = true;
                }
            }
            else
            {
                ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = false;
                ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = true;
                ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = true;
                ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = true;
                //((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
                //lblTotalCapacity.Text = TotalCapt.ToString();
            }
            //decimal TotalRCapacity = 0;
             TotalRCapacity = (Convert.ToDecimal(TotalRCapacity) + Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString()));
        }
        lblTotalCapacity.Text = TotalRCapacity.ToString();
        lblTotalRegAmt.Text = Math.Round((TotalRCapacity * Convert.ToDecimal(.40)), 0).ToString();
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
            txtWB_calibrationdate.Visible = true;
            lblWB.Visible = true;
            tr_W_Machine.Visible = false;
        }
        else
        {
            txtWeighCpt.Visible = false;
            Label1.Visible = false;
            Label2.Visible = false;
            ddlWeighCertified.Visible = false;
            txtWB_calibrationdate.Visible = false;
            lblWB.Visible = false;
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
        try
        {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (ddlElectWeigh.SelectedItem.Text == "No")
        {
            WeighCertified = "0";
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

        sqltran = con.BeginTransaction();
        //qry = "INSERT INTO [tbl_WarehouseAdditionalinfo] (Registration_ID,[Latitude],[Longitude],[RoadType],[RoadWidth],[GateType],[NoOfGate],[PowerSupply],[HighTentionLine],[WaterFac],[WaterOtherUsage],[CCTV],[FumigationEqup],[GuardWithRoom],[PlanksDunnage],[FireBuckets],[FireExting],[WeighBridge],[WBCertified],[WBCapacity],[InterentCon],[ConType],[HardAvl],[InstallationYear],[Createdby],[CreatedDate],DistanceFHighway,DistanceFRailS,DistanceFMandi,DistanceFGoodS) VALUES ('" + Registration_No.ToString() + "','" + txtlat.Text + "','" + txtlong.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + txtRoadWidth.Text + "','" + ddlGateType.SelectedValue.ToString() + "','" + txtNoGate.Text + "','" + ddlPowersuply.SelectedValue.ToString() + "','" + ddlTentionline.SelectedValue.ToString() + "','" + ddlWaterFac.SelectedValue.ToString() + "','" + ddlWaterSpry.SelectedValue.ToString() + "','" + ddlCCTV.SelectedValue.ToString() + "','" + ddlFumigation.SelectedValue.ToString() + "','" + ddlGuard.SelectedValue.ToString() + "','" + ddlPlanks.SelectedValue.ToString() + "','" + ddlFireBuc.SelectedValue.ToString() + "','" + ddlFireExt.SelectedValue.ToString() + "','" + WeighCertified.ToString() + "','" + ddlWeighCertified.SelectedValue.ToString() + "','" + txtWeighCpt.Text + "','" + ddlInternetCon.SelectedValue.ToString() + "','" + ConType.ToString() + "','" + HardwareAvl.ToString() + "','" + Installationyear.ToString() + "','" + Client_Ip + "',GETDATE(),'" + Convert.ToDecimal(txtHighway.Text) + "','" + Convert.ToDecimal(txtRailway.Text) + "','" + Convert.ToDecimal(txtMandi.Text) + "','" + Convert.ToDecimal(txtGS.Text) + "') ";
        qry = "INSERT INTO [tbl_WarehouseAdditionalinfo] (Registration_ID,[Latitude],[Longitude],[RoadType],[RoadWidth],[GateType],[NoOfGate],[PowerSupply],[HighTentionLine],[WaterFac],[WaterOtherUsage],[CCTV],[FumigationEqup],[GuardWithRoom],[PlanksDunnage],[FireBuckets],[FireExting],[WeighBridge],[WBCertified],[WBCapacity],[InterentCon],[ConType],[HardAvl],[InstallationYear],[Createdby],[CreatedDate],DistanceFHighway,DistanceFRailS,DistanceFMandi,DistanceFGoodS,[Weighing_Machine],[WB_CalibrationExpDate],[Weighinh_M_Capacity],[W_BoundaryType]) VALUES ('" + Registration_No.ToString() + "','" + txtlat.Text + "','" + txtlong.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + txtRoadWidth.Text + "','" + ddlGateType.SelectedValue.ToString() + "','" + txtNoGate.Text + "','" + ddlPowersuply.SelectedValue.ToString() + "','" + ddlTentionline.SelectedValue.ToString() + "','" + ddlWaterFac.SelectedValue.ToString() + "','" + ddlWaterSpry.SelectedValue.ToString() + "','" + ddlCCTV.SelectedValue.ToString() + "','" + ddlFumigation.SelectedValue.ToString() + "','" + ddlGuard.SelectedValue.ToString() + "','" + ddlPlanks.SelectedValue.ToString() + "','" + ddlFireBuc.SelectedValue.ToString() + "','" + ddlFireExt.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + WeighCertified.ToString() + "','" + txtWeighCpt.Text + "','" + ddlInternetCon.SelectedValue.ToString() + "','" + ConType.ToString() + "','" + HardwareAvl.ToString() + "','" + Installationyear.ToString() + "','" + Client_Ip + "',GETDATE(),'" + Convert.ToDecimal(txtHighway.Text) + "','" + Convert.ToDecimal(txtRailway.Text) + "','" + Convert.ToDecimal(txtMandi.Text) + "','" + Convert.ToDecimal(txtGS.Text) + "','" + WMachn + "','" + getDate_MDY(txtWB_calibrationdate.Text) + "','" + WMachCpt + "','"+ ddlboundrytype.SelectedValue.ToString().Trim() +"') ";

        cmd = new SqlCommand(qry, con, sqltran);
        int a = cmd.ExecuteNonQuery();
        if (a == 1)
        {
            sqltran.Commit();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Registration...'); </script> ");
            btnprint.Focus();
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

    public int checklicnodatevalidation()
    {
        int WF = 0;
        int ch = 0;
        var formatedDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        for (int i = 0; gvGodown.Rows.Count > i; i++)
        {
            if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
            {
                var Issuedate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim());
                var Validdate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim());
                if (((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Licence Type..!'); </script> ");
                    break;
                }
                else if (((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text.ToString().Trim() == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Number..!'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Focus();
                    break;
                }
                else if (((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim() == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Issue Date..!'); </script> ");
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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Expiry Date..!'); </script> ");
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
        }
        return WF;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        string strsql = "select Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,RegCapacity,RegAmt from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No  where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblRegID.Text = dt.Rows[0]["Registration_Id"].ToString().Trim();
            lblOwn.Text = dt.Rows[0]["Auth_Person"].ToString().Trim();
            lblcontact.Text = dt.Rows[0]["MobileNo"].ToString().Trim();
            lblemailid.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            lblRegCapacity.Text = dt.Rows[0]["RegCapacity"].ToString().Trim();
            lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString().Trim();
        }
        ModalPopupExtender2.Show();
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("PrintReg.aspx");
        string Roid = "../JointVentureScheme/PrintReg.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open(");
        sb.Append("'" + Roid + "'");
        sb.Append(",'MyWindow', 'height=800,width=1100');");
        sb.Append("</script>");
        this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
    }
    protected void btncloseconfrm_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseHome.aspx");
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
    protected void ddlblocknew_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot();
    }
}


