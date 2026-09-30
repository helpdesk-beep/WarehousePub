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
public partial class JointVentureScheme_OtherLoginRegistration : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        CalendarExtender3.StartDate = DateTime.Now;
        CalendarExtender2.StartDate = DateTime.Now;
        if ((Session["UserName"] != null) && (Session["Agency_ID"] != null))
        {
            if (!IsPostBack)
            {
                get_Districts();
                SetInitialRow();
                rdoNoW_CheckedChanged(null, null);
                if (Session["Scope"].ToString() == "B")
                {
                    lblWType.Visible = true;
                    ddlWType.Visible = true;
                    ddlWarDistrict.SelectedValue = Session["DistID"].ToString();
                    ddlWarDistrict_SelectedIndexChanged(null, null);
                    ddlBranch.SelectedValue = Session["UserId"].ToString();
                    ddlWarDistrict.Enabled = false;
                    ddlBranch.Enabled = false;
                    //txtDistance.Text = "0";
                    //txtDistance.Enabled = false;
                }
            }

        }
        else
        {
            Response.Redirect("OtherGovLogin.aspx");
        }  
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        
        if (Session["Scope"].ToString() == "B")
        {
            Response.Redirect("Logins.aspx");
        }
        else if (Session["Scope"].ToString() == "O")
        {
            Response.Redirect("OtherGovLogin.aspx");
        }
        else
        {
            Response.Redirect("OtherGovLogin.aspx");
        }
    }
    public int CheckEmail()
    {
        int ch = 0;
        //string strsql = "select EmailID from tbl_WarehouseRegistration where EmailID='" + Session["email"].ToString() + "'";

        //SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपका रजिस्ट्रेशन हो चुका है कृपया चेक करे...'); </script> ");
        //    //txtREgemail.Focus();
        //    ch = 1;
        //}
        //else
        //{
        //    ch = 0;
        //}
        return ch;
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual
        DateTime _effective_date = Convert.ToDateTime("03/12/2018 11:00:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("09/15/2018 11:59:00 PM");
        ////Old
        //DateTime _effective_date = Convert.ToDateTime("06/27/2017 11:30:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("07/17/2017 05:00:00 PM");

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
        //string TStatus = Tcheckdatetimes();
        string TStatus = "Y";

        if (TStatus == "Y")
        {
            DateTime STDate = Convert.ToDateTime(getDate_MDY(txtSLDate.Text));
            DateTime WDDate = Convert.ToDateTime(getDate_MDY(txtWDRALDate.Text));
            int SK = 0;
            //SK = CheckEmail();
            //if (SK == 0)
            //{
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
                }
                else if (ddlBlock.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Tehsil...'); </script> ");
                }
                else if (ddlBranch.SelectedItem.Text == "---Select---")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Branch...'); </script> ");
                }
                else if (txtDistance.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Distance from nearest branch of MPWLC (in KM):...'); </script> ");
                }
                else if (rdoYes.Checked == false && rdoYesW.Checked == false)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Atleast One Licence Required :...'); </script> ");
                    //txtWareAddress.Focus();
                }
                else if (rdoYes.Checked == true && (txtWLicNo.Value == "" || txtSLDate.Text == ""))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Warehouse State Licence No & Date...'); </script> ");
                }
                else if (rdoYesW.Checked == true && (txtWDRALicenceNo.Value == "" || txtWDRALDate.Text == ""))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter WDRA Registration No & date :...'); </script> ");
                }
                else if (rdoYes.Checked == true && (STDate.Date < DateTime.Now.Date))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid State Licence Date..'); </script> ");
                }
                else if (rdoYesW.Checked && (WDDate.Date < DateTime.Now.Date))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid WDRA Licence Date..'); </script> ");
                }
                else if (txtIncharge.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge/Manager Name :..'); </script> ");
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
                }
                else if (txtInchargeEmail.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Incharge Email ID...'); </script> ");
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
                else if (txtrackpointDist.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Distance of Warehouse From Railway Rack Point (in KM)...'); </script> ");
                    txtGS.Focus();
                }
                else if (txtrackpointName.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Rack Point Name...'); </script> ");
                    txtGS.Focus();
                }
                else if (ddlRoadType.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Motorable Approach Road Type...'); </script> ");
                }
                else if (txtRoadWidth.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Width of Road (In Meters)...'); </script> ");
                }
                else if (ddlGateType.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Shutter/Jali/Chanel Gate in Godown...'); </script> ");
                }
                else if (txtNoGate.Text == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter No of Gates...'); </script> ");
                }
                else if (ddlPowersuply.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Power Supply...'); </script> ");
                }
                else if (ddlTentionline.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Is Godown free from Passing over of any tension electric line...'); </script> ");
                }
                else if (ddlWaterFac.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Water Facility..'); </script> ");
                }
                else if (ddlWaterSpry.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Water Facility for spray and other usage..'); </script> ");
                }
                else if (ddlCCTV.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select CCTV Camera..'); </script> ");
                }
                else if (ddlFumigation.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability Of Fumigation & Pest Control Equipment..'); </script> ");
                }
                else if (ddlGuard.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Guard With Guard Room..'); </script> ");
                }
                else if (ddlPlanks.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability of Wooden Planks/Dunnage..'); </script> ");
                }
                else if (ddlFireBuc.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability fo Fire Buckets..'); </script> ");
                }
                else if (ddlFireExt.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Availability of Fire extinguisher with fire hydrants..'); </script> ");
                }
                else if (ddlElectWeigh.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Electronic Weighbridge..'); </script> ");
                }
                else if (ddlWeighCertified.SelectedItem.Text == "--Select--" && ddlElectWeigh.SelectedItem.Text == "Yes")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Weighbridge is Certified By Controler..'); </script> ");
                }
                else if ((Convert.ToDecimal(txtWeighCpt.Text) < 30 || txtWeighCpt.Text == "") && (ddlElectWeigh.SelectedItem.Text == "Yes"))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Electronic Weighbridge Capacity should greater than or equal to 30 MT'); </script> ");
                    //lblTotalCapacity.Focus();
                }
                else if (ddlInternetCon.SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Internet Connectivity..'); </script> ");
                }
                else if (ddlHardwareAvl.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Availability of Computer ..'); </script> ");
                }
                else if (ddlConType.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Connectivity Type ..'); </script> ");
                }
                else if (ddlInstallationyear.SelectedItem.Text == "--Select--" && ddlInternetCon.SelectedItem.Text == "Yes")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Year Of Installation ..'); </script> ");
                }
                else if (CheckBox1.Checked == false)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Self Declaration Check box...'); </script> ");
                }
                else
                {

                    GetApplicationNo();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    sqltran = con.BeginTransaction();
                    try
                    {
                        Insert_Registration_Detail();
                        int ckdinsert = 0;
                        ckdinsert = Insert_Registration_Detail2();
                        if (ckdinsert == 1)
                        {
                            sqltran.Commit();
                            btnsubmit.Enabled = false;
                            Session["Reg_No"] = Registration_No;
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save ...'); </script> ");
                            Response.Redirect("OtherPrintReg.aspx");
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' Data Not Save Something Error ...'); </script> ");
                        }
                    }
                    catch (Exception es)
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
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 12/03/2018 11:00:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
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
                else if (INCN.Length > 3)
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
        string BankName = "";
        if (ddlBank.SelectedItem.Text == "--Select--")
        {
            txtAccNo.Text = "";
            txtIFSC.Value = "";
            BankName = "";
        }
        else
        {
            BankName = ddlBank.SelectedItem.Text;
        }
        string StateLicenceDate = "";
        string WDRALicenceDate = "";
        string StateLicenceNo = "";
        string WDRALicenceNO = "";
        if (rdoYes.Checked == true)
        {
            StateLicenceDate = getDate_MDY(txtSLDate.Text);
            StateLicenceNo = txtWLicNo.Value;
        }
        else if (rdoNo.Checked == true)
        {
            StateLicenceDate = null;
            StateLicenceNo = null;
        }
        if (rdoYesW.Checked == true)
        {
            WDRALicenceDate = getDate_MDY(txtWDRALDate.Text);
            WDRALicenceNO = txtWDRALicenceNo.Value;
        }
        else if (rdoNoW.Checked == true)
        {
            WDRALicenceDate = null;
            WDRALicenceNO = null;
        }
       string Hiretype ="";

       if (Session["Scope"].ToString() == "O")
       {
           Hiretype = "";
       }
       else
       {
           if (Session["Scope"].ToString() == "M" || Session["Scope"].ToString() == "B")
           {
               Hiretype = ddlWType.SelectedValue.ToString();
           }
       }
       string sql = "INSERT INTO [tbl_WarehouseRegistration]([Registration_Id],[RegionId],[DistrictId],[BranchId],[Registration_Date],[Warehouse_Name],[Mobile_No],[PhoneNo],[EmailID],[Warehouse_Address],[TehsilID],[Warehouse_Capacity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[WDRA_LicenseNo],[WDRA_LicenseDate],[Incharge_Name],[Incharge_MobileNo],[Incharge_EmailID],[Incharge_Address],[Bank_Name],[Account_No],[IFSC_Code],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],[DeletedBy],[DeletedDate],[FeesStatus],[FeesTransID],RegAmt,RegCapacity,[IsActive],[AId],[AppPicName],[AppPicType],[AppPic],[WareDocName],[WareDocType],[WareDocPic],DistFNBranch,Warehouse_landmark,InchDesignation,Phase,WarehouseHiredtype,MPWLC_Type) VALUES ('" + Registration_No + "','','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + txtWareContactNo.Text + "','','" + txtEmailID.Text + "','" + txtWareAddress.Value + "','" + ddlBlock.SelectedValue + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','" + StateLicenceNo + "','" + StateLicenceDate + "','" + WDRALicenceNO + "','" + WDRALicenceDate + "','" + txtIncharge.Value + "','" + txtInchMob.Text + "','" + txtInchargeEmail.Value + "','" + txtInchAdd.Value + "','" + BankName + "','" + txtAccNo.Text + "','" + txtIFSC.Value + "','" + ip + "',GETDATE(),'','','','','N','','" + Convert.ToDecimal(lblTotalRegAmt.Text) + "','" + Convert.ToDecimal(lblTotalCapacity.Text) + "','N','" + BID + "','null','null',null,'null','null',null,'" + txtDistance.Text + "','" + txtLandmark.Value + "','" + txtDesign.Value + "','0','" + Session["Agency_ID"].ToString() + "','" + Hiretype + "')";

        SqlCommand cmd = new SqlCommand(sql, con,sqltran);
        int CT = 0;
        CT = cmd.ExecuteNonQuery();
        if (CT > 0)
        {
                Insert_Godown_Detail();
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
            TextBox Lenghts = (TextBox)gvGodown.Rows[j].Cells[1].FindControl("txtLenght");
            TextBox Widths = (TextBox)gvGodown.Rows[j].Cells[2].FindControl("txtWidth");
            TextBox Heights = (TextBox)gvGodown.Rows[j].Cells[3].FindControl("txtHeight");
            TextBox Capacitys = (TextBox)gvGodown.Rows[j].Cells[5].FindControl("txtCapacity");
            TextBox ConstructionYears = (TextBox)gvGodown.Rows[j].Cells[6].FindControl("txtConstY");
            TextBox GVGate = (TextBox)gvGodown.Rows[j].Cells[7].FindControl("txtGVGate");
            TextBox GVUnloadCpt = (TextBox)gvGodown.Rows[j].Cells[8].FindControl("txtGVUnloadCpt");
            DropDownList GVStorageType = (DropDownList)gvGodown.Rows[j].Cells[9].FindControl("txtGVddlStorageType");

            decimal GodownNo = Convert.ToDecimal(gvGodown.Rows[j].Cells[0].Text.ToString());
            decimal Length = Convert.ToDecimal(Lenghts.Text);
            decimal Width = Convert.ToDecimal(Widths.Text);
            decimal Height = Convert.ToDecimal(Heights.Text);
            decimal Capacity = Convert.ToDecimal(Capacitys.Text);
            string ConstructionY = ConstructionYears.Text;
            string SGVGate = GVGate.Text;
            decimal SGVUnlCpt = Convert.ToDecimal(GVUnloadCpt.Text);
            string SGVStorageType = GVStorageType.Text;
            string GodownId = Registration_No + (j + 1);
            string qryGd = "INSERT INTO [tbl_WarehouseGodown_Reg] ([Registration_Id],[Godown_ID],[DistrictId],[BranchId],[Registration_Date],[Godown_Name],[Godown_No],[G_Length],[G_Width],[G_Height],[G_ScientificCapacity],[G_MaxCapacity],[G_ConstructedYear],[CreatedBy],[CreatedDate],[IsActive],[AId],[NoOfGate],[UnloadCpt],[StorageType]) VALUES ('" + Registration_No + "','" + GodownId + "','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + GodownNo + "','" + Length + "','" + Width + "','" + Height + "','" + Capacity + "'," + Convert.ToDecimal(0) + ",'" + ConstructionY + "','" + ClientIP + "',GETDATE(),'Y','" + Convert.ToDecimal(0) + "','" + SGVGate + "','" + SGVUnlCpt + "','" + SGVStorageType + "')";
            cmd = new SqlCommand(qryGd, con, sqltran);
            int c = cmd.ExecuteNonQuery();
            if (c > 0)
            {
             
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
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }

    public void GetBankList()
    {
        ddlBank.Items.Clear();
        string qry = "select distinct BANK from [IfscBankmar15] where districtId='" + ddlWarDistrict.SelectedValue.ToString() + "' order by BANK";
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
        string qry = "select ID from [IfscBankmar15] where Branch='" + ddlBBranch.SelectedItem.Text + "' and Bank='" + ddlBank.SelectedValue.ToString() + "' and districtId='" + ddlWarDistrict.SelectedValue.ToString() + "'";
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
                drCurrentRow["Width"] = 0;
                drCurrentRow["Height"] = 0;
                drCurrentRow["Capacity"] = 0;
                drCurrentRow["ConstY"] = 0;
                drCurrentRow["GVGate"] = 0;
                drCurrentRow["GVUnloadCpt"] = 0;
                drCurrentRow["GVStorageType"] = 0;

                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    TextBox Lenght = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtLenght");
                    TextBox Width = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtWidth");
                    TextBox Height = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtHeight");
                    TextBox Capacity = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtCapacity");
                    TextBox ConstY = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtConstY");
                    TextBox GVGate = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtGVGate");
                    TextBox GVUnloadCpt = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtGVUnloadCpt");
                    DropDownList GVStorageType = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("txtGVddlStorageType");

                    dtCurrentTable.Rows[i]["Lenght"] = Convert.ToDecimal(Lenght.Text);
                    dtCurrentTable.Rows[i]["Width"] = Convert.ToDecimal(Width.Text);
                    dtCurrentTable.Rows[i]["Height"] = Convert.ToDecimal(Height.Text);
                    dtCurrentTable.Rows[i]["Capacity"] = Convert.ToDecimal(Capacity.Text);
                    dtCurrentTable.Rows[i]["ConstY"] = Convert.ToDecimal(ConstY.Text);
                    dtCurrentTable.Rows[i]["GVGate"] = Convert.ToString(GVGate.Text);
                    dtCurrentTable.Rows[i]["GVUnloadCpt"] = Convert.ToDecimal(GVUnloadCpt.Text);
                    dtCurrentTable.Rows[i]["GVStorageType"] = Convert.ToString(GVStorageType.Text);
                }

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
                    rowIndex++;
                }
            }
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Lenght", typeof(string)));
        dt.Columns.Add(new DataColumn("Width", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Height", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Capacity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ConstY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GVGate", typeof(string)));
        dt.Columns.Add(new DataColumn("GVUnloadCpt", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GVStorageType", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Lenght"] = 0;
        dr["Width"] = 0;
        dr["Height"] = 0;
        dr["Capacity"] = 0;
        dr["ConstY"] = 0;
        dr["GVGate"] = 0;
        dr["GVUnloadCpt"] = 0;
        dr["GVStorageType"] = 0;

        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        gvGodown.DataSource = dt;
        gvGodown.DataBind();
    }

    protected void ddlWarDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot();
        Get_Blocks();
        GetBankList();

    }
    public void Get_Blocks()
    {
        ddlBlock.Items.Clear(); ;
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
            string DistrictId = ddlWarDistrict.SelectedValue.ToString();
            string str = "SELECT mbi.BranchID,mbi.BranchName FROM MetaDataBranchWithIssueCenter as mbi  WHERE mbi.[DistrictId] = '" + DistrictId.ToString() + "' and  mbi.BranchTypeID not in ('2','3','4','5','6','7','8','9','10') order by mbi.BranchID";
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
        }
        else
        {
            txtWeighCpt.Visible = false;
            Label1.Visible = false;
            Label2.Visible = false;
            ddlWeighCertified.Visible = false;
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
    public int Insert_Registration_Detail2()
    {
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string WeighCertified = "";
        string ConType = "";
        string HardwareAvl = "";
        string Installationyear = "";
        int F = 0;

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
            
            qry = "INSERT INTO [tbl_WarehouseAdditionalinfo] (Registration_ID,[Latitude],[Longitude],[RoadType],[RoadWidth],[GateType],[NoOfGate],[PowerSupply],[HighTentionLine],[WaterFac],[WaterOtherUsage],[CCTV],[FumigationEqup],[GuardWithRoom],[PlanksDunnage],[FireBuckets],[FireExting],[WeighBridge],[WBCertified],[WBCapacity],[InterentCon],[ConType],[HardAvl],[InstallationYear],[Createdby],[CreatedDate],DistanceFHighway,DistanceFRailS,DistanceFMandi,DistanceFGoodS,DistanceFRackPoint,N_RackPoint) VALUES ('" + Registration_No.ToString() + "','" + txtlat.Text + "','" + txtlong.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + txtRoadWidth.Text + "','" + ddlGateType.SelectedValue.ToString() + "','" + txtNoGate.Text + "','" + ddlPowersuply.SelectedValue.ToString() + "','" + ddlTentionline.SelectedValue.ToString() + "','" + ddlWaterFac.SelectedValue.ToString() + "','" + ddlWaterSpry.SelectedValue.ToString() + "','" + ddlCCTV.SelectedValue.ToString() + "','" + ddlFumigation.SelectedValue.ToString() + "','" + ddlGuard.SelectedValue.ToString() + "','" + ddlPlanks.SelectedValue.ToString() + "','" + ddlFireBuc.SelectedValue.ToString() + "','" + ddlFireExt.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + WeighCertified.ToString() + "','" + txtWeighCpt.Text + "','" + ddlInternetCon.SelectedValue.ToString() + "','" + ConType.ToString() + "','" + HardwareAvl.ToString() + "','" + Installationyear.ToString() + "','" + Client_Ip + "',GETDATE(),'" + Convert.ToDecimal(txtHighway.Text) + "','" + Convert.ToDecimal(txtRailway.Text) + "','" + Convert.ToDecimal(txtMandi.Text) + "','" + Convert.ToDecimal(txtGS.Text) + "','" + Convert.ToDecimal(txtrackpointDist.Text) + "','"+ txtrackpointName.Text +"') ";
            cmd = new SqlCommand(qry, con, sqltran);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                return F = 1;
            }
            return F;
    }
    protected void rdoNo_CheckedChanged(object sender, EventArgs e)
    {
        TrStateL.Visible = false;
    }
    protected void rdoYes_CheckedChanged(object sender, EventArgs e)
    {
        TrStateL.Visible = true;
    }
    protected void rdoNoW_CheckedChanged(object sender, EventArgs e)
    {
        TWDRAL.Visible = false;
    }
    protected void rdoYesW_CheckedChanged(object sender, EventArgs e)
    {
        TWDRAL.Visible = true;
    }
    protected void link1_Click(object sender, EventArgs e)
    {
        if (Session["Scope"].ToString() == "B")
        {
            Response.Redirect("BranchHome.aspx");
        }
        else if (Session["Scope"].ToString() == "O")
        {
            Response.Redirect("OtherLoginWelcome.aspx");
        }
        else
        {
            Response.Redirect("JointVentureSchemeApp.aspx");
        }
    }
}
