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

public partial class JointVentureScheme_BranchInspectionForm : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                 GetDist();
                 //gerreg();
                 GetWMSGodownID();
                 lblDate.Text=DateTime.Now.ToString();
                 SetInitialRow();
                 ddlWareSeasonCpt_SelectedIndexChanged(null, null);
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    //public void GetWMSGodownID()
    //{
    //    qry = "select Godown_Name +' ('+ Godown_ID +')' as GodownName,Godown_ID from tbl_MetaData_Godown where Remarks='Y' and  BranchID='" + Session["UserId"].ToString() + "' order by GodownName";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlWMSGodownID.DataSource = ds.Tables[0];
    //        ddlWMSGodownID.DataTextField = "GodownName";
    //        ddlWMSGodownID.DataValueField = "Godown_ID";
    //        ddlWMSGodownID.DataBind();
    //        ddlWMSGodownID.Items.Insert(0, "--Select--");
    //        ddlWMSGodownID.Items.Insert(1, "New Godown");
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
    //    }
    //}
    public void gerreg( string qury)
    {
        qry = qury;
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWarName.DataSource = ds.Tables[0];
            ddlWarName.DataTextField = "Warehouse_Name";
            ddlWarName.DataValueField = "Registration_Id";
            ddlWarName.DataBind();
            ddlWarName.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }

    public void GetOfferGodown()
    {
        if (ddlWarName.SelectedItem.Text != "--Select--")
        {
            if (ddl_session.SelectedValue.ToString() == "Kharif1819")
            {
                //qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'  and Phase>=9";
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'  and Phase>=9 and Godown_ID not in (select GodownID from tbl_Godown_Inspection where Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate>= convert(varchar(10),'10/19/2018',101))";
            }
            else if (ddl_session.SelectedValue.ToString() == "Rabi1819")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Godown_ID not in (select GodownID from tbl_Godown_Inspection where Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate < convert(varchar(10),'10/19/2018',101)) and Phase<9";
            }
          //  qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Godown_ID not in (select GodownID from tbl_Godown_Inspection where Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_No";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlgodown.Items.Clear();
                ddlgodown.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Another Godown Found')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Name First ')", true);
        }
    }
    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOfferGodown();
    }

    public void GetRegisterationData()
    {
        qry = "select WR.Registration_Id,wr.DistrictId,wr.TehsilID,wr.BranchId,(select Regionnm from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as RegionName ,(select District_Name from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as DistirctName ,(select DepotName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as BranchName ,(select Tehsil_Name from Tehsils where TehsilCode=wr.TehsilID) as Tehsil_Name ,Warehouse_Name,Warehouse_Address,WR.Mobile_No as OfficeNo ,Incharge_Name,wr.Incharge_MobileNo as InchMob,DistFNBranch ,Bank_Name,IFSC_Code,Account_No,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRAExpDate,Warehouse_LicenseNo,convert(varchar(10),Warehouse_LicenseDate,103) as Warehouse_LicenseDate,PAN_No,Aadhar_No,WPR.Auth_Person,WPR.EmailID,WPR.MobileNo from tbl_WarehouseRegistration AS wr  inner join tbl_Warehouse_PreReg as WPR on WPR.Reg_No=wr.Registration_Id where wr.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblDistID.Text = dt.Rows[0]["DistrictId"].ToString();
            lblBranchID.Text = dt.Rows[0]["BranchId"].ToString();
            txtRegion.Text = dt.Rows[0]["RegionName"].ToString();
            txtDistrict.Text = dt.Rows[0]["DistirctName"].ToString();
            txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
            DDLDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            DDLDistrict_SelectedIndexChanged(null, null);
            DDLTehsil.SelectedValue = dt.Rows[0]["TehsilID"].ToString();
            txtGodAdd.Value = dt.Rows[0]["Warehouse_Address"].ToString();
            txtWareName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblDecWareName.Text = txtWareName.Text;
            txtGodContact.Text = dt.Rows[0]["OfficeNo"].ToString();
            txtInchMbNo.Text = dt.Rows[0]["InchMob"].ToString();
            ddlNearBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
            txtDistance.Text = dt.Rows[0]["DistFNBranch"].ToString();
            txtOwnerMob.Text = dt.Rows[0]["MobileNo"].ToString();
            txtOwnerEmail.Text = dt.Rows[0]["EmailID"].ToString();
            txtPanNo.Text = dt.Rows[0]["PAN_No"].ToString();
            txtAadharNo.Text = dt.Rows[0]["Aadhar_No"].ToString();
            txtOwnerName.Text = dt.Rows[0]["Auth_Person"].ToString();
            txtBankNo.Text = dt.Rows[0]["Account_No"].ToString();
            txtIFSC.Text = dt.Rows[0]["IFSC_Code"].ToString();
            txtBankName.Text = dt.Rows[0]["Bank_Name"].ToString();
            txtWDRALicNo.Text = dt.Rows[0]["WDRA_LicenseNo"].ToString();
            txtWDRAValidDate.Text = dt.Rows[0]["WDRAExpDate"].ToString();
            if (txtWDRALicNo.Text != "")
            {
                ddlWDRALic.SelectedValue = "1";
                ddlWDRALic_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlWDRALic.SelectedValue = "0";
                ddlWDRALic_SelectedIndexChanged(null, null);
            }
            txtLicenseNo.Text = dt.Rows[0]["Warehouse_LicenseNo"].ToString();
            txtlicExpdate.Text = dt.Rows[0]["Warehouse_LicenseDate"].ToString();

            if (txtLicenseNo.Text != "")
            {
                ddlstateLic.SelectedValue = "1";
                ddlstateLic_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlstateLic.SelectedValue = "0";
                ddlstateLic_SelectedIndexChanged(null, null);
            }
            if (txtBankName.Text == "Other" || txtBankName.Text == "--Select--")
            {
                txtBankName.Text = "";
            }
        }
    }

    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegisterationData();
        GetGodwnOfferData();
        GetRegGodwnData();
        GetWareAdditionaldata();
    }

    private void GetDist()
    {
            string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLDistrict.DataSource = ds.Tables[0];
                DDLDistrict.DataTextField = "District_Name";
                DDLDistrict.DataValueField = "District_Id";
                DDLDistrict.DataBind();
                DDLDistrict.Items.Insert(0, "---Select---");
                Session["SDist"] = ds;
            }
            else
            {
                DDLDistrict.Items.Insert(0, "---Select---");
            }
    }
    private void GetBranch()
    {
        string strDist = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='"+ DDLDistrict.Text +"'";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlNearBranch.DataSource = ds.Tables[0];
            ddlNearBranch.DataTextField = "DepotName";
            ddlNearBranch.DataValueField = "BranchId";
            ddlNearBranch.DataBind();
            ddlNearBranch.Items.Insert(0, "---Select---");
        }
        else
        {
            ddlNearBranch.Items.Insert(0, "---Select---");
        }
    }
    public void Get_Blocks()
    {
        string DistrictId = DDLDistrict.SelectedValue.ToString();
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
            DDLTehsil.DataSource = ds.Tables[0];
            DDLTehsil.DataTextField = "Tehsil_Name";
            DDLTehsil.DataValueField = "TehsilCode";
            DDLTehsil.DataBind();
            DDLTehsil.Items.Insert(0, "--Select--");
        }
    }
    public void GetGodwnOfferData()
    {
        if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase<9 group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase>=9 group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date";
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtGodNo.Text = dt.Rows[0]["Godown_No"].ToString();
            ddlJVCategory.SelectedValue = dt.Rows[0]["G_Scheme"].ToString();
            txtCptJV.Text = dt.Rows[0]["G_OfferCapacity"].ToString();
            txtOnlineOffer.Text = dt.Rows[0]["Offer_Date"].ToString();
            ddlCptOffer.SelectedValue = dt.Rows[0]["Capacity_Type"].ToString();
            lblGodownOfferID.Text = dt.Rows[0]["Godown_Offer_Id"].ToString();
            lblOfferID.Text = dt.Rows[0]["Offer_Id"].ToString();
        }
    }

    public void GetRegGodwnData()
    {
        qry = "select CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_Width)as G_Width,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear from tbl_WarehouseGodown_Reg where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtLength.Text = dt.Rows[0]["G_Length"].ToString();
            txtWidth.Text = dt.Rows[0]["G_Width"].ToString();
            txtHeight.Text = dt.Rows[0]["G_Height"].ToString();
            txtMaxCpt.Text = dt.Rows[0]["G_ScientificCapacity"].ToString();
            txtConsYear.Text = dt.Rows[0]["G_ConstructedYear"].ToString();
        }
    }
    public void GetWareAdditionaldata()
    {
        qry = "select Latitude,Longitude,FireBuckets,FireExting from tbl_WarehouseAdditionalinfo where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtLatitude.Text = dt.Rows[0]["Latitude"].ToString();
            txtLongitude.Text = dt.Rows[0]["Longitude"].ToString();
            ddlFireBucket.SelectedValue = dt.Rows[0]["FireBuckets"].ToString();
            ddlFireExting.SelectedValue = dt.Rows[0]["FireExting"].ToString();
        }
    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        Get_Blocks();
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
    protected string getNumericVal(string txtval)
    {
        if (txtval == "" || txtval == null)
        {
            return "0";
        }
        else
        {
            return txtval;
        }
    }
    public string ChkInspectionID()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string MaxInsID = "";
        string QueryMax = "select count(Inspection_Id) as InsPecID from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "" || str3 !="0")
        {
            //int lencount = Convert.ToInt32(str3.Length.ToString());
            //string part1 = str3.Substring(0, lencount - 2);
            //int partlen1 = Convert.ToInt32(part1.Length.ToString());
            //string part2 = str3.Substring(partlen1, lencount - partlen1);
            //MaxOfrID = part1 + Convert.ToString(Convert.ToInt32(part2) + 1);

            //int lencount = Convert.ToInt32(str3.Length.ToString());
            //string part1 = str3.Substring(0, lencount - 1);
            //int partlen1 = Convert.ToInt32(part1.Length.ToString());
            //string part2 = str3.Substring(partlen1, lencount - partlen1);
            MaxInsID = ddlWarName.SelectedValue.ToString() + "18" + Convert.ToString(Convert.ToInt32(str3) + 1);
        }
        else
        {
            MaxInsID = ddlWarName.SelectedValue.ToString() + "181";
        }
        con.Close();
        return MaxInsID;
    }
    public void InsertInspection()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string Srch_inspectionID = ChkInspectionID();
        string SContype = "";
        string SHardAvl = "";
        if (ddlInternetCon.SelectedItem.Text == "Yes")
        {
            SContype = ddlConType.SelectedValue.ToString();
            SHardAvl=ddlHardwareAvl.SelectedValue.ToString();
        }
        else
        {
            SContype = "0";
            SHardAvl = "0";
        }
        string SWDRALicNo = "";
        string SWDRAIssueDate = "";
        string SWDRAValiDDate = "";
        string SWDRACertAgenncy = "";
        string SWDRACertNo = "";
        string SWDRADateCer = "";
        if (ddlWDRALic.SelectedItem.Text == "Yes")
        {
            SWDRALicNo = txtWDRALicNo.Text;
            SWDRAIssueDate = txtWDRAstartDate.Text;
            SWDRAValiDDate = txtWDRAValidDate.Text;
            SWDRACertAgenncy = txtWDRALicAuthority.Text;
            SWDRACertNo = txtWDRAAgencyCert.Text;
            SWDRADateCer = txtWDRADateOfCert.Text;
        }
        else
        {
            SWDRALicNo = "";
            SWDRAIssueDate = "";
            SWDRAValiDDate = "";
            SWDRACertAgenncy = "";
            SWDRACertNo = "";
            SWDRADateCer = "";
        }
        string SStateLicNo = "";
        string SStateLicValidDate = "";
        string SStateLicIssueDate = "";
        if (ddlstateLic.SelectedItem.Text == "Yes")
        { 
              SStateLicNo = txtLicenseNo.Text;
              SStateLicValidDate = txtlicExpdate.Text;
              SStateLicIssueDate = txtDateOfIssuance.Text;
        }
        else
        {
              SStateLicNo = "";
              SStateLicValidDate = "";
              SStateLicIssueDate = "";
        }
        string CHKPvtStockIssue = "";
        if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlPvtCpt.SelectedItem.Text=="Yes")
        {
            CHKPvtStockIssue = "Y";
        }
        else
        {
            if (ddlWarePrivateDepositor.SelectedItem.Text == "No")
            {

            }
            else
            {
                CHKPvtStockIssue = "N";
            }
        }
        if (ddlProcurement.SelectedItem.Text == "No")
        {
            txtPreviouseCpt.Text = "0";
        }
        if (ddlElectWeighBridge.SelectedItem.Text == "No")
        {
            txtCptElectWeigh.Text = "0";
        }
        string WMS_Godown="";
        if (ddlWMSGodownID.SelectedItem.Text == "New Godown")
        {
            WMS_Godown = "NG";
        }
        else
        {
            WMS_Godown = ddlWMSGodownID.SelectedValue.ToString();
        }
        string sql = "INSERT INTO tbl_Godown_Inspection([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPSCSC],[Insp_Des_MPSCSC],[Insp_Name_MPWLC],[Insp_Des_MPWLC],[Insp_Name_Markfed],[Insp_Des_Markfed],[Insp_Name_DSO],[Insp_Des_DSO],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[VacantCpt_Condition11],[VacantCpt_Condition12],[VacantCpt_Condition13],[No_Of_Gate],[No_Of_Stack],[Fire_Extinguisher],[Fire_Buckets],[Account_No],[IFSC_Code],[Bank_Name],[Bank_PB_Check],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LicenseDate],[WDRA_LIssueDate],[License_Authority],[Certificate_No],[Certification_Date],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[Building_Ins_Company],[Building_Ins_Policy_No],[Building_Sum_Assured],[Building_P_Issued_Date],[Building_P_Expired_Date],[Comm_Ins_Company],[Comm_Ins_Policy_No],[Comm_Sum_Assured],[Comm_P_Issued_Date],[Comm_P_Expired_Date],[Comm_SumAss_Condition],[Compre_Ins_Condition],[G_Gates_Condition],[Elec_Weigh_Operation],[Fumi_Equip_Availability],[ElecBeam_Scale_Avail],[MoistureM_Equip_Avail],[Cameras_Availability],[Security_Guard_Avail],[RoadType],[RoadWidth],[Road_Quality_Satisfaction],[Ware_Boundary_Avail],[Godown_Open_Space],[IsProc_Centre_LastYear],[Proc_Comm_LastYear],[Adeq_Wooden_Avail],[Drinking_Water_Avail],[Usage_Water_Avail],[Adeq_Equip_Avail],[Tech_Resource_Avail],[Equip_Avai_InWare],[Adeq_Manpower_Avail],[PowerSupply],[HighTentionLineFree],[Elec_Weigh_Avail],[WBCapacity],[Calibration_Certified],[Calib_Cert_IssueDate],[Nearest_Weigh_Avail],[Internet_Con],[ConType],[HardAvl],[Operator_Avail],[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsNotAgm_AOffer_LastYr],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[PolicyAgreAvl],[PvtCptAvl],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Offer_Scheme]) VALUES (@Inspection_Id,@Godown_Offer_Id,@Offer_Id,@Registration_Id,@GodownId,@Godown_No,@RegionId,@DistrictId,@BranchId,@Insp_Date,@Insp_Name_MPSCSC,@Insp_Des_MPSCSC,@Insp_Name_MPWLC,@Insp_Des_MPWLC,@Insp_Name_Markfed,@Insp_Des_Markfed,@Insp_Name_DSO,@Insp_Des_DSO,@Godown_Postal_Address,@Ware_DistrictId,@Ware_TehsilId,@Ware_Latitude,@Ware_Longitude,@Ware_Campus_Area,@Ware_ContactNo,@Ware_Inch_MobileNo,@Ware_Nearest_Branch,@Ware_Distance_MPWLC,@Owner_Name,@Owner_Postal_Add,@Owner_MobileNo,@Owner_Email,@Owner_PanNo,@Owner_AadharNo,@Capacity_Type,@G_OfferCapacity,@G_Length,@G_Width,@G_Height,@G_MaxCapacity,@G_ConstructedYear,@G_Unload_Capacity,@Stored_Commodities,@Stored_Com_Depositor,@Utilized_Capacity,@Vacant_Capacity,@VacantCpt_Condition11,@VacantCpt_Condition12,@VacantCpt_Condition13,@No_Of_Gate,@No_Of_Stack,@Fire_Extinguisher,@Fire_Buckets,@Account_No,@IFSC_Code,@Bank_Name,@Bank_PB_Check,@Present_Validity_WDRA,@WDRA_LicenseNo,@WDRA_LicenseDate,@WDRA_LIssueDate,@License_Authority,@Certificate_No,@Certification_Date,@WDRAL_Present_Validity,@Warehouse_LicenseNo,@Warehouse_LicenseDate,@Warehouse_LIssueDate,@Building_Ins_Company,@Building_Ins_Policy_No,@Building_Sum_Assured,@Building_P_Issued_Date,@Building_P_Expired_Date,@Comm_Ins_Company,@Comm_Ins_Policy_No,@Comm_Sum_Assured,@Comm_P_Issued_Date,@Comm_P_Expired_Date,@Comm_SumAss_Condition,@Compre_Ins_Condition,@G_Gates_Condition,@Elec_Weigh_Operation,@Fumi_Equip_Availability,@ElecBeam_Scale_Avail,@MoistureM_Equip_Avail,@Cameras_Availability,@Security_Guard_Avail,@RoadType,@RoadWidth,@Road_Quality_Satisfaction,@Ware_Boundary_Avail,@Godown_Open_Space,@IsProc_Centre_LastYear,@Proc_Comm_LastYear,@Adeq_Wooden_Avail,@Drinking_Water_Avail,@Usage_Water_Avail,@Adeq_Equip_Avail,@Tech_Resource_Avail,@Equip_Avai_InWare,@Adeq_Manpower_Avail,@PowerSupply,@HighTentionLineFree,@Elec_Weigh_Avail,@WBCapacity,@Calibration_Certified, @Calib_Cert_IssueDate,@Nearest_Weigh_Avail,@Internet_Con,@ConType,@HardAvl,@Operator_Avail,@IsWare_Under_Constr,@IsWare_Disputed,@IsWare_Damage,@IsWareS_Comm_NGovt,@IsWare_BlackListed,@IsOnline_Info_Wrong,@IsMandatory_Condi_NAvail,@IsNotAgm_AOffer_LastYr,@CreatedBy,@CreatedDate,@RackPoint,@RackPoint_Dist,@WMS_GodownID,@Lic_Navinikaran_no,@Lic_Navinikaran_Date,@PolicyAgreAvl,@PvtCptAvl,@Fit_Unfit,@Remark,@AutoFit_Unfit,@ddlJVCategory)";
      //string sql = "INSERT INTO tbl_Godown_Inspection([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPSCSC],[Insp_Des_MPSCSC],[Insp_Name_MPWLC],[Insp_Des_MPWLC],[Insp_Name_Markfed],[Insp_Des_Markfed],[Insp_Name_DSO],[Insp_Des_DSO],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[VacantCpt_Condition11],[VacantCpt_Condition12],[VacantCpt_Condition13],[No_Of_Gate],[No_Of_Stack],[Fire_Extinguisher],[Fire_Buckets],[Account_No],[IFSC_Code],[Bank_Name],[Bank_PB_Check],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LicenseDate],[WDRA_LIssueDate],[License_Authority],[Certificate_No],[Certification_Date],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[Building_Ins_Company],[Building_Ins_Policy_No],[Building_Sum_Assured],[Building_P_Issued_Date],[Building_P_Expired_Date],[Comm_Ins_Company],[Comm_Ins_Policy_No],[Comm_Sum_Assured],[Comm_P_Issued_Date],[Comm_P_Expired_Date],[Comm_SumAss_Condition]) VALUES (@Inspection_Id,@Godown_Offer_Id,@Offer_Id,@Registration_Id,@GodownId,@Godown_No,@RegionId,@DistrictId,@BranchId,@Insp_Date,@Insp_Name_MPSCSC,@Insp_Des_MPSCSC,@Insp_Name_MPWLC,@Insp_Des_MPWLC,@Insp_Name_Markfed,@Insp_Des_Markfed,@Insp_Name_DSO,@Insp_Des_DSO,@Godown_Postal_Address,@Ware_DistrictId,@Ware_TehsilId,@Ware_Latitude,@Ware_Longitude,@Ware_Campus_Area,@Ware_ContactNo,@Ware_Inch_MobileNo,@Ware_Nearest_Branch,@Ware_Distance_MPWLC,@Owner_Name,@Owner_Postal_Add,@Owner_MobileNo,@Owner_Email,@Owner_PanNo,@Owner_AadharNo,@Capacity_Type,@G_OfferCapacity,@G_Length,@G_Width,@G_Height,@G_MaxCapacity,@G_ConstructedYear,@G_Unload_Capacity,@Stored_Commodities,@Stored_Com_Depositor,@Utilized_Capacity,@Vacant_Capacity,@VacantCpt_Condition11,@VacantCpt_Condition12,@VacantCpt_Condition13,@No_Of_Gate,@No_Of_Stack,@Fire_Extinguisher,@Fire_Buckets,@Account_No,@IFSC_Code,@Bank_Name,@Bank_PB_Check,@Present_Validity_WDRA,@WDRA_LicenseNo,@WDRA_LicenseDate,@WDRA_LIssueDate,@License_Authority,@Certificate_No,@Certification_Date,@WDRAL_Present_Validity,@Warehouse_LicenseNo,@Warehouse_LicenseDate,@Warehouse_LIssueDate,@Building_Ins_Company,@Building_Ins_Policy_No,@Building_Sum_Assured,@Building_P_Issued_Date,@Building_P_Expired_Date,@Comm_Ins_Company,@Comm_Ins_Policy_No,@Comm_Sum_Assured,@Comm_P_Issued_Date,@Comm_P_Expired_Date,@Comm_SumAss_Condition)";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlParameter[] prms = new SqlParameter[133];

        prms[0] = new SqlParameter("@Inspection_Id", SqlDbType.VarChar , 20);
        prms[0].Value = Srch_inspectionID;

        prms[1] = new SqlParameter("@Godown_Offer_Id", SqlDbType.VarChar, 20);
        prms[1].Value = lblGodownOfferID.Text;

        prms[2] = new SqlParameter("@Offer_Id", SqlDbType.VarChar, 20);
        prms[2].Value = lblOfferID.Text;

        prms[3] = new SqlParameter("@Registration_Id", SqlDbType.VarChar, 20);
        prms[3].Value = ddlWarName.SelectedValue.ToString();

        prms[4] = new SqlParameter("@GodownId", SqlDbType.VarChar, 20);
        prms[4].Value = ddlgodown.SelectedValue.ToString();

        prms[5] = new SqlParameter("@Godown_No", SqlDbType.VarChar, 4);
        prms[5].Value = txtGodNo.Text;

        prms[6] = new SqlParameter("@RegionId", SqlDbType.VarChar, 2);
        prms[6].Value = lblRegionID.Text;

        prms[7] = new SqlParameter("@DistrictId", SqlDbType.VarChar, 4);
        prms[7].Value = lblDistID.Text;

        prms[8] = new SqlParameter("@BranchId", SqlDbType.VarChar, 20);
        prms[8].Value = lblBranchID.Text;

        prms[9] = new SqlParameter("@Insp_Date", SqlDbType.DateTime);
        prms[9].Value = getDate_MDY(txtInspDate.Text);

        prms[10] = new SqlParameter("@Insp_Name_MPSCSC", SqlDbType.NVarChar, 100);
        prms[10].Value = txtInspNameMPS.Text;

        prms[11] = new SqlParameter("@Insp_Des_MPSCSC", SqlDbType.NVarChar, 50);
        prms[11].Value = txtDesMPS.Text;

        prms[12] = new SqlParameter("@Insp_Name_MPWLC", SqlDbType.NVarChar, 100);
        prms[12].Value = txtNameMPW.Text;

        prms[13] = new SqlParameter("@Insp_Des_MPWLC", SqlDbType.NVarChar, 50);
        prms[13].Value = txtDesMPW.Text;

        prms[14] = new SqlParameter("@Insp_Name_Markfed", SqlDbType.NVarChar, 100);
        prms[14].Value = txtNameMark.Text;

        prms[15] = new SqlParameter("@Insp_Des_Markfed", SqlDbType.NVarChar, 50);
        prms[15].Value = txtDesMark.Text;

        prms[16] = new SqlParameter("@Insp_Name_DSO", SqlDbType.NVarChar, 100);
        prms[16].Value = txtNameDSO.Text;

        prms[17] = new SqlParameter("@Insp_Des_DSO", SqlDbType.NVarChar, 50);
        prms[17].Value = txtDesDSO.Text;

        prms[18] = new SqlParameter("@Godown_Postal_Address", SqlDbType.VarChar, 200);
        prms[18].Value = txtGodAdd.Value;

        prms[19] = new SqlParameter("@Ware_DistrictId", SqlDbType.VarChar, 4);
        prms[19].Value = DDLDistrict.SelectedValue.ToString();

        prms[20] = new SqlParameter("@Ware_TehsilId", SqlDbType.VarChar, 10);
        prms[20].Value = DDLTehsil.SelectedValue.ToString();

        prms[21] = new SqlParameter("@Ware_Latitude", SqlDbType.VarChar, 20);
        prms[21].Value = txtLatitude.Text;

        prms[22] = new SqlParameter("@Ware_Longitude", SqlDbType.VarChar, 20);
        prms[22].Value = txtLongitude.Text;

        prms[23] = new SqlParameter("@Ware_Campus_Area", SqlDbType.Decimal);
        prms[23].Value = txtWareArea.Text;

        prms[24] = new SqlParameter("@Ware_ContactNo", SqlDbType.VarChar, 12);
        prms[24].Value = txtGodContact.Text;

        prms[25] = new SqlParameter("@Ware_Inch_MobileNo", SqlDbType.VarChar, 12);
        prms[25].Value = txtInchMbNo.Text;

        prms[26] = new SqlParameter("@Ware_Nearest_Branch", SqlDbType.VarChar, 50);
        prms[26].Value = ddlNearBranch.SelectedValue.ToString();

        prms[27] = new SqlParameter("@Ware_Distance_MPWLC", SqlDbType.Decimal);
        prms[27].Value = txtDistance.Text;

        prms[28] = new SqlParameter("@Owner_Name", SqlDbType.VarChar, 50);
        prms[28].Value = txtOwnerName.Text;

        prms[29] = new SqlParameter("@Owner_Postal_Add", SqlDbType.VarChar, 100);
        prms[29].Value = txtOwnerAdd.Text;
        prms[30] = new SqlParameter("@Owner_MobileNo", SqlDbType.VarChar, 12);
        prms[30].Value = txtOwnerMob.Text;

        prms[31] = new SqlParameter("@Owner_Email", SqlDbType.VarChar, 50);
        prms[31].Value = txtOwnerEmail.Text;

        prms[32] = new SqlParameter("@Owner_PanNo", SqlDbType.VarChar, 10);
        prms[32].Value = txtPanNo.Text;

        prms[33] = new SqlParameter("@Owner_AadharNo", SqlDbType.VarChar, 12);
        prms[33].Value = txtAadharNo.Text;

        prms[34] = new SqlParameter("@Capacity_Type", SqlDbType.Char, 1);
        prms[34].Value = ddlCptOffer.SelectedValue.ToString();

        prms[35] = new SqlParameter("@G_OfferCapacity", SqlDbType.Decimal);
        prms[35].Value = txtCptJV.Text;

        prms[36] = new SqlParameter("@G_Length", SqlDbType.Decimal);
        prms[36].Value = txtLength.Text;

        prms[37] = new SqlParameter("@G_Width", SqlDbType.Decimal);
        prms[37].Value = txtWidth.Text;

        prms[38] = new SqlParameter("@G_Height", SqlDbType.Decimal);
        prms[38].Value = txtHeight.Text;

        prms[39] = new SqlParameter("@G_MaxCapacity", SqlDbType.Decimal);
        prms[39].Value = txtMaxCpt.Text;

        prms[40] = new SqlParameter("@G_ConstructedYear", SqlDbType.VarChar, 4);
        prms[40].Value = txtConsYear.Text;

        prms[41] = new SqlParameter("@G_Unload_Capacity", SqlDbType.Decimal);
        prms[41].Value = txtUnloadCpt.Text;

        prms[42] = new SqlParameter("@Stored_Commodities", SqlDbType.VarChar, 100);
        prms[42].Value = txtCommodityName.Text;

        prms[43] = new SqlParameter("@Stored_Com_Depositor", SqlDbType.VarChar, 100);
        prms[43].Value = txtDepositorName.Text;

        prms[44] = new SqlParameter("@Utilized_Capacity", SqlDbType.Decimal);
        prms[44].Value = txtCurrentStoredComm.Text;

        prms[45] = new SqlParameter("@Vacant_Capacity", SqlDbType.Decimal);
        prms[45].Value = txtVacantCpt.Text;

        prms[46] = new SqlParameter("@VacantCpt_Condition11", SqlDbType.Char, 1);
        prms[46].Value = ddlVacantCptLess.SelectedValue.ToString();

        prms[47] = new SqlParameter("@VacantCpt_Condition12", SqlDbType.Char, 1);
        prms[47].Value = ddlVanactCptUnused.SelectedValue.ToString();

        prms[48] = new SqlParameter("@VacantCpt_Condition13", SqlDbType.Char, 1);
        prms[48].Value = ddlGovtDepositor.SelectedValue.ToString();

        prms[49] = new SqlParameter("@No_Of_Gate", SqlDbType.Int);
        prms[49].Value = getNumericVal(txtGateNo.Text);

        prms[50] = new SqlParameter("@No_Of_Stack", SqlDbType.Int);
        prms[50].Value = getNumericVal(txtGodStack.Text);

        prms[51] = new SqlParameter("@Fire_Extinguisher", SqlDbType.Char, 1);
        prms[51].Value = ddlFireExting.SelectedValue.ToString();

        prms[52] = new SqlParameter("@Fire_Buckets", SqlDbType.Char, 1);
        prms[52].Value = ddlFireBucket.SelectedValue.ToString();

        prms[53] = new SqlParameter("@Account_No", SqlDbType.VarChar, 30);
        prms[53].Value = txtBankNo.Text;

        prms[54] = new SqlParameter("@IFSC_Code", SqlDbType.VarChar, 20);
        prms[54].Value = txtIFSC.Text;

        prms[55] = new SqlParameter("@Bank_Name", SqlDbType.NVarChar, 50);
        prms[55].Value = txtBankName.Text;

        prms[56] = new SqlParameter("@Bank_PB_Check", SqlDbType.Char, 1);
        prms[56].Value = ddlBankDetail.SelectedValue.ToString();

        prms[57] = new SqlParameter("@Present_Validity_WDRA", SqlDbType.VarChar, 4);
        prms[57].Value = ddlWDRALic.SelectedValue.ToString();

        prms[58] = new SqlParameter("@WDRA_LicenseNo", SqlDbType.VarChar, 50);
        prms[58].Value = SWDRALicNo;

        prms[59] = new SqlParameter("@WDRA_LicenseDate", SqlDbType.DateTime);
        prms[59].Value = getDate_MDY(SWDRAIssueDate);

        prms[60] = new SqlParameter("@WDRA_LIssueDate", SqlDbType.DateTime);
        prms[60].Value = getDate_MDY(SWDRAValiDDate);

        prms[61] = new SqlParameter("@License_Authority", SqlDbType.VarChar, 50);
        prms[61].Value = SWDRACertAgenncy;

        prms[62] = new SqlParameter("@Certificate_No", SqlDbType.VarChar, 50);
        prms[62].Value = SWDRACertNo;

        prms[63] = new SqlParameter("@Certification_Date", SqlDbType.DateTime);
        prms[63].Value = getDate_MDY(SWDRADateCer);

        prms[64] = new SqlParameter("@WDRAL_Present_Validity", SqlDbType.Char, 1);
        prms[64].Value = ddlstateLic.SelectedValue.ToString();

        prms[65] = new SqlParameter("@Warehouse_LicenseNo", SqlDbType.VarChar, 50);
        prms[65].Value = SStateLicNo;

        prms[66] = new SqlParameter("@Warehouse_LicenseDate", SqlDbType.DateTime);
        prms[66].Value = getDate_MDY(SStateLicValidDate);

        prms[67] = new SqlParameter("@Warehouse_LIssueDate", SqlDbType.DateTime);
        prms[67].Value = getDate_MDY(SStateLicIssueDate);

        prms[68] = new SqlParameter("@Building_Ins_Company", SqlDbType.VarChar, 50);
        prms[68].Value = txtInsuarnceName.Text;

        prms[69] = new SqlParameter("@Building_Ins_Policy_No", SqlDbType.VarChar, 50);
        prms[69].Value = txtPolicyNo.Text;

        prms[70] = new SqlParameter("@Building_Sum_Assured", SqlDbType.VarChar, 50);
        prms[70].Value = getNumericVal(txtPolicyAmt.Text);

        prms[71] = new SqlParameter("@Building_P_Issued_Date", SqlDbType.DateTime);
        prms[71].Value = getDate_MDY(txtPolicyIssue.Text);

        prms[72] = new SqlParameter("@Building_P_Expired_Date", SqlDbType.DateTime);
        prms[72].Value = getDate_MDY(txtPolicyExpired.Text);

        prms[73] = new SqlParameter("@Comm_Ins_Company", SqlDbType.VarChar, 50);
        prms[73].Value = txtComWareInsName.Text;

        prms[74] = new SqlParameter("@Comm_Ins_Policy_No", SqlDbType.VarChar, 50);
        prms[74].Value = txtComWarePolicyNo.Text;

        prms[75] = new SqlParameter("@Comm_Sum_Assured", SqlDbType.VarChar, 50);
        prms[75].Value = getNumericVal(txtSumAssured.Text);

        prms[76] = new SqlParameter("@Comm_P_Issued_Date", SqlDbType.DateTime);
        prms[76].Value = getDate_MDY(txtComInsuDateIssue.Text);

        prms[77] = new SqlParameter("@Comm_P_Expired_Date", SqlDbType.DateTime);
        prms[77].Value = getDate_MDY(txtComInsuDateExpired.Text);

        prms[78] = new SqlParameter("@Comm_SumAss_Condition", SqlDbType.Char, 1);
        prms[78].Value = ddlSumAdequate.SelectedValue.ToString();

        prms[79] = new SqlParameter("@Compre_Ins_Condition", SqlDbType.Char, 1);
        prms[79].Value = ddljvsIns.SelectedValue.ToString();

        prms[80] = new SqlParameter("@G_Gates_Condition", SqlDbType.Char, 1);
        prms[80].Value = ddlShutterGates.SelectedValue.ToString();

        prms[81] = new SqlParameter("@Elec_Weigh_Operation", SqlDbType.Char, 1);
        prms[81].Value = ddlElecWeighbridge.SelectedValue.ToString();

        prms[82] = new SqlParameter("@Fumi_Equip_Availability", SqlDbType.Char, 1);
        prms[82].Value = ddlFumigation.SelectedValue.ToString();

        prms[83] = new SqlParameter("@ElecBeam_Scale_Avail", SqlDbType.Char, 1);
        prms[83].Value = ddlElecBeamScale.SelectedValue.ToString();

        prms[84] = new SqlParameter("@MoistureM_Equip_Avail", SqlDbType.Char, 1);
        prms[84].Value = ddlMarkedMoisture.SelectedValue.ToString();

        prms[85] = new SqlParameter("@Cameras_Availability", SqlDbType.Char, 1);
        prms[85].Value = ddlCameras.SelectedValue.ToString();

        prms[86] = new SqlParameter("@Security_Guard_Avail", SqlDbType.Char, 1);
        prms[86].Value = ddlGuard.SelectedValue.ToString();

        prms[87] = new SqlParameter("@RoadType", SqlDbType.Char, 1);
        prms[87].Value = ddlMotorableRoadType.SelectedValue.ToString();

        prms[88] = new SqlParameter("@RoadWidth", SqlDbType.Decimal);
        prms[88].Value = txtMotorableRoadWidth.Text;

        prms[89] = new SqlParameter("@Road_Quality_Satisfaction", SqlDbType.Char, 1);
        prms[89].Value = ddlRoadSatisfactory.SelectedValue.ToString();

        prms[90] = new SqlParameter("@Ware_Boundary_Avail", SqlDbType.Char, 1);
        prms[90].Value = ddlBoundaryWall.SelectedValue.ToString();

        prms[91] = new SqlParameter("@Godown_Open_Space", SqlDbType.Char, 1);
        prms[91].Value = ddlGodownSpace.SelectedValue.ToString();

        prms[92] = new SqlParameter("@IsProc_Centre_LastYear", SqlDbType.Char, 1);
        prms[92].Value = ddlProcurement.SelectedValue.ToString();

        prms[93] = new SqlParameter("@Proc_Comm_LastYear", SqlDbType.Decimal);
        prms[93].Value = getNumericVal(txtPreviouseCpt.Text);

        prms[94] = new SqlParameter("@Adeq_Wooden_Avail", SqlDbType.Int);
        prms[94].Value = ddlwoodenplank.SelectedValue.ToString();

        prms[95] = new SqlParameter("@Drinking_Water_Avail", SqlDbType.Char, 1);
        prms[95].Value = ddlwater.SelectedValue.ToString();

        prms[96] = new SqlParameter("@Usage_Water_Avail", SqlDbType.Char, 1);
        prms[96].Value = ddlwaterother.SelectedValue.ToString();

        prms[97] = new SqlParameter("@Adeq_Equip_Avail", SqlDbType.Char, 1);
        prms[97].Value = ddlFumiEquip.SelectedValue.ToString();

        prms[98] = new SqlParameter("@Tech_Resource_Avail", SqlDbType.Char, 1);
        prms[98].Value = ddlTechResource.SelectedValue.ToString();

        prms[99] = new SqlParameter("@Equip_Avai_InWare", SqlDbType.Char, 1);
        prms[99].Value = ddlFumiPestEquipments.SelectedValue.ToString();

        prms[100] = new SqlParameter("@Adeq_Manpower_Avail", SqlDbType.Char, 1);
        prms[100].Value = ddlManpower.SelectedValue.ToString();

        prms[101] = new SqlParameter("@PowerSupply", SqlDbType.Char, 1);
        prms[101].Value = ddlPowerSupply.SelectedValue.ToString();

        prms[102] = new SqlParameter("@HighTentionLineFree", SqlDbType.Char, 1);
        prms[102].Value = ddlElectTensionLine.SelectedValue.ToString();

        prms[103] = new SqlParameter("@Elec_Weigh_Avail", SqlDbType.Char, 1);
        prms[103].Value = ddlElectWeighBridge.SelectedValue.ToString();

        prms[104] = new SqlParameter("@WBCapacity", SqlDbType.Decimal);
        prms[104].Value = getNumericVal(txtCptElectWeigh.Text);

        prms[105] = new SqlParameter("@Calibration_Certified", SqlDbType.Char, 1);
        prms[105].Value = ddlCalibration.SelectedValue.ToString();

        prms[106] = new SqlParameter("@Calib_Cert_IssueDate", SqlDbType.DateTime);
        prms[106].Value = getDate_MDY(txtCalib_cert_date.Text);

        prms[107] = new SqlParameter("@Nearest_Weigh_Avail", SqlDbType.Char, 1);
        prms[107].Value = ddlCampusDistance.SelectedValue.ToString();

        prms[108] = new SqlParameter("@Internet_Con", SqlDbType.Char, 1);
        prms[108].Value = ddlInternetCon.SelectedValue.ToString();

        prms[109] = new SqlParameter("@ConType", SqlDbType.Char, 1);
        prms[109].Value = SContype;

        prms[110] = new SqlParameter("@HardAvl", SqlDbType.Char, 1);
        prms[110].Value = SHardAvl;

        prms[111] = new SqlParameter("@Operator_Avail", SqlDbType.Char, 1);
        prms[111].Value = ddlComputerOperator.SelectedValue.ToString();

        prms[112] = new SqlParameter("@IsWare_Under_Constr", SqlDbType.Char, 1);
        prms[112].Value = ddlWareConstruct.SelectedValue.ToString();

        prms[113] = new SqlParameter("@IsWare_Disputed", SqlDbType.Char, 1);
        prms[113].Value = ddlWarelitigation.SelectedValue.ToString();

        prms[114] = new SqlParameter("@IsWare_Damage", SqlDbType.Char, 1);
        prms[114].Value = ddlWareDamage.SelectedValue.ToString();

        prms[115] = new SqlParameter("@IsWareS_Comm_NGovt", SqlDbType.Char, 1);
        prms[115].Value = ddlWarePrivateDepositor.SelectedValue.ToString();

        prms[116] = new SqlParameter("@IsWare_BlackListed", SqlDbType.Char, 1);
        prms[116].Value = ddlBlackList.SelectedValue.ToString();

        prms[117] = new SqlParameter("@IsOnline_Info_Wrong", SqlDbType.Char, 1);
        prms[117].Value = ddlWareWrongInfo.SelectedValue.ToString();

        prms[118] = new SqlParameter("@IsMandatory_Condi_NAvail", SqlDbType.Char, 1);
        prms[118].Value = ddlFacilities.SelectedValue.ToString();

        prms[119] = new SqlParameter("@IsNotAgm_AOffer_LastYr", SqlDbType.VarChar, 50);
        prms[119].Value = ddlWareSeasonCpt.SelectedValue.ToString();

        prms[120] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
        prms[120].Value = ip;

        prms[121] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
        prms[121].Value = DateTime.Now;

        prms[122] = new SqlParameter("@RackPoint", SqlDbType.VarChar, 30);
        prms[122].Value = txtrackpoint.Text;

        prms[123] = new SqlParameter("@RackPoint_Dist", SqlDbType.Decimal);
        prms[123].Value = txtrackpintdist.Text;

        prms[124] = new SqlParameter("@WMS_GodownID", SqlDbType.VarChar, 20);
        prms[124].Value = WMS_Godown;

        prms[125] = new SqlParameter("@Lic_Navinikaran_no", SqlDbType.VarChar, 20);
        prms[125].Value = txtLicNotPreset.Text;

        prms[126] = new SqlParameter("@Lic_Navinikaran_Date", SqlDbType.DateTime);
        prms[126].Value = getDate_MDY(txtLicNotPresetDate.Text);

        prms[127] = new SqlParameter("@PolicyAgreAvl", SqlDbType.Char , 1);
        prms[127].Value = lblInsPolicyChkbox.Text;

        prms[128] = new SqlParameter("@PvtCptAvl", SqlDbType.Char ,1);
        prms[128].Value =  CHKPvtStockIssue;

        prms[129] = new SqlParameter("@Fit_Unfit", SqlDbType.VarChar, 10);
        prms[129].Value = ddlfitunfit.SelectedItem.Text;

        prms[130] = new SqlParameter("@Remark", SqlDbType.NVarChar, 500);
        prms[130].Value = txtRemark.Value;

        prms[131] = new SqlParameter("@AutoFit_Unfit", SqlDbType.VarChar,10);
        prms[131].Value = txtSystemFitunfit.Text;

        prms[132] = new SqlParameter("@ddlJVCategory", SqlDbType.Int);
        prms[132].Value = ddlJVCategory.SelectedItem.Text;

        int CT = 0;
        cmd.Parameters.AddRange(prms);
        con.Open();
        CT = cmd.ExecuteNonQuery();
        con.Close();
        if (CT > 0)
        {
           int ProcCenter = insertGodownProcCenter();
           if (ProcCenter > 0)
           {
               ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save...'); </script> ");
               btnsubmit.Enabled = false;
           }
           else
           {
               ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Not Save...'); </script> ");
           }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //Send_Unfit_SMS();

        if (ddlWarName.SelectedItem.Text != "--Select--" && ddlgodown.SelectedItem.Text != "--Select--")
        {
            if (ddlWMSGodownID.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  सॉफ्टवेर (WMS) से  Godown ID दर्ज करे  :...'); </script> ");
            }
            else if (txtInspDate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('निरीक्षण दिनांक दर्ज करे :...'); </script> ");
            }
            else if (txtInspNameMPS.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('जिला प्रबंधक MPSCSC के प्रतिनिधि का नाम  दर्ज करे :...'); </script> ");
            }
            else if (txtDesMPS.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('जिला प्रबंधक MPSCSC के प्रतिनिधि का पद  दर्ज करे :...'); </script> ");
            }
            else if (txtNameMPW.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('MPWLC के जिला स्तरीय नोडल अधिकारी अथवा संबंधित शाखा के शाखा प्रबंधक -(संयोजक) नाम  दर्ज करे : :...'); </script> ");
            }
            else if (txtDesMPW.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('MPWLC के जिला स्तरीय नोडल अधिकारी अथवा संबंधित शाखा के शाखा प्रबंधक -(संयोजक) पद  दर्ज करे : :...'); </script> ");
            }
            else if (txtNameMark.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('जिला प्रबंधक Markfed के प्रतिनिधि का नाम  दर्ज करे  :...'); </script> ");
            }
            else if (txtDesMark.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(जिला प्रबंधक Markfed के प्रतिनिधि का पद दर्ज करे :...'); </script> ");
            }
            else if (txtWareArea.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस परिसर का कुल क्षेत्रफल  दर्ज करे :...'); </script> ");
            }
            else if (txtrackpoint.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस की निकटतम रेल्वे रेक पॉइंट  का नाम  दर्ज करे : :...'); </script> ");
            }
            else if (txtrackpintdist.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' रेल्वे रेक पॉइंट  से दूरी दर्ज करे  :...'); </script> ");
            }
            else if (txtOwnerAdd.Text == "")
            {
               // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(वेअरहाउस  संचालक  का पता दर्ज करे :...'); </script> ");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  संचालक  का पता दर्ज करे :...'); </script> ");
            }
            else if (txtUnloadCpt.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की एक दिन में अनुमानित उतराई क्षमता  दर्ज करे : :...'); </script> ");
            }
            else if (txtVacantCpt.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वर्तमान में गोदाम की रिक्त क्षमता  दर्ज करे  :...'); </script> ");
            }
            else if (ddlVacantCptLess.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम की रिक्त क्षमता offer की गयी क्षमता से कम है? दर्ज करे  :...'); </script> ");
            }
            else if (ddlVanactCptUnused.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या मौजूदा रिक्त क्षमता (Vacant Capacity)1800(मे.टन)से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी नहीं  है दर्ज करे : :...'); </script> ");
            }
            else if (ddlGovtDepositor.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या मौजूदा रिक्त क्षमता (Vacant Capacity)500(मे.टन) से कम है, और संग्रहीत स्कंध का  जमाकर्ता सरकारी एजेंसी है दर्ज करे : :...'); </script> ");
            }
            else if (txtGateNo.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में गेट्स की संख्या दर्ज करे  :...'); </script> ");
            }
            else if (txtGodStack.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में स्टैक्स की संख्या की स्वीकार्य सीमा  दर्ज करे :...'); </script> ");
            }
            else if (ddlFireExting.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('अग्नि हाइड्रंट्स  के साथ आग बुझाने की पर्याप्त उपलब्धता  दर्ज करे  :...'); </script> ");
            }
            else if (ddlFireBucket.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में फायर बकेट की पर्याप्त उपलब्धता  दर्ज करे : :...'); </script> ");
            }
            else if (ddlBankDetail.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या बैंक विवरण सही हैं (पास बुक विवरण के साथ जांच करें) दर्ज करे : :...'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text =="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA लायसेंस  दर्ज करे : '); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicNo.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA लायसेंस नंबर दर्ज करे  :'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicNo.Text != "" && txtWDRAstartDate.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA लायसेंस जारी दिनांक  दर्ज करे  :'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicNo.Text != "" && txtWDRAstartDate.Text != "" && txtWDRAValidDate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA लायसेंस वैधता दिनांक  दर्ज करे  :'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicAuthority.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA अधिकृत एक्रीडेशन एजेंसी का नाम   :'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicAuthority.Text != "" && txtWDRAAgencyCert.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA अधिकृत एक्रीडेशन एजेंसी का जारी प्रमाण पत्र का क्रमांक  दर्ज करे  :'); </script> ");
            }
            else if (ddlWDRALic.SelectedItem.Text == "Yes" && txtWDRALicAuthority.Text != "" && txtWDRAAgencyCert.Text != "" && txtWDRADateOfCert.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA अधिकृत एक्रीडेशन एजेंसी का जारी प्रमाण पत्र का जारी दिनांक  दर्ज करे  :...'); </script> ");
            }
            else if (ddlstateLic.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('State लायसेंस  दर्ज करे  :...'); </script> ");
            }
            else if (ddlstateLic.SelectedItem.Text == "Yes" && txtlicExpdate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('स्टेट  लायसेंस नंबर   दर्ज करे  :...'); </script> ");
            }
            else if (ddlstateLic.SelectedItem.Text == "Yes" && txtlicExpdate.Text != "" && txtLicenseNo.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('स्टेट  लायसेंस  वैधता दिनांक  दर्ज करे  :...'); </script> ");
            }
            else if (ddlstateLic.SelectedItem.Text == "Yes" && txtlicExpdate.Text != "" && txtLicenseNo.Text != "" && txtDateOfIssuance.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('स्टेट  लायसेंस जारी  दिनांक  दर्ज करे  :...'); </script> ");
            }
            else if (ddlstateLic.SelectedItem.Text == "No" && ddlWDRALic.SelectedItem.Text == "No" && ddlLicNotPre.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यदि WDRA लायसेंस या वेअरहाउस लायसेंस वैध नहीं है तो क्या इसे नवीनीकरण हेतु आवेदन किया गया है? दर्ज करे  :...'); </script> ");
            }
            else if (ddlLicNotPre.SelectedItem.Text == "Yes" && txtLicNotPreset.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('नवीनीकरण हेतु आवेदन किया गया है तो आवेदन क्रमांक  दर्ज करे  :...'); </script> ");
            }
            else if (ddlLicNotPre.SelectedItem.Text == "Yes" && txtLicNotPreset.Text != "" && txtLicNotPresetDate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('नवीनीकरण हेतु आवेदन किया गया है तो  आवेदन दिनांक दर्ज करे  :...'); </script> ");
            }
            else if (ddlLicNotPre.SelectedItem.Text == "No" && ddlstateLic.SelectedItem.Text == "No" && ddlWDRALic.SelectedItem.Text == "No")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('WDRA लायसेंस , वेअरहाउस लायसेंस या नवीनीकरण हेतु आवेदन किया गया है किन्ही एक का चूनाव करना अनिवार्य हे :...'); </script> ");
            }
            else if (txtInsuarnceName.Text == "" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' बिल्डिंग बीमा पॉलिसी जारी करने वाली एजेंसी का नाम दर्ज करे :'); </script> ");
            }
            else if (txtInsuarnceName.Text != "" &&  txtPolicyNo.Text =="" )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की बिल्डिंग के बीमा पॉलिसी क्रमांक दर्ज करे :'); </script> ");
            }
            else if (txtInsuarnceName.Text != "" && txtPolicyNo.Text != "" && txtPolicyAmt.Text== "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की बिल्डिंग के बीमित राशि रू दर्ज करे :'); </script> ");
            }
            else if (txtInsuarnceName.Text != "" && txtPolicyNo.Text != "" && txtPolicyAmt.Text != "" && txtPolicyIssue.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की बिल्डिंग के बीमा पॉलिसी जारी दिनांक दर्ज करे :'); </script> ");
            }
            else if (txtInsuarnceName.Text != "" && txtPolicyNo.Text != "" && txtPolicyAmt.Text != "" && txtPolicyIssue.Text != "" && txtPolicyExpired.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की बिल्डिंग के बीमा पॉलिसी वैधता दिनांक दर्ज करे :'); </script> ");
            }
            else if (txtComWareInsName.Text =="" && chkboxInsPolicy.Checked==false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यदि बीमा पॉलिसी नहीं है तो शपथ पत्र जमा कराए जिसमे उल्लेख हो की अग्रीमेंट के समय तक आवश्यक वैध  बीमा पॉलिसी उपलब्ध करा ली जावेगी  दर्ज करे :'); </script> ");
            }
            else if (ddljvsIns.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('संयुक्त भागीदारी योजना के अनुबंधित गोदाम क्षमता पर राषि रूपये 20,000 प्रति मे.टन की दर से काम्प्रीहेंसिव बीमा है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlShutterGates.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम के प्रत्येक शटर के अलावा अतिरिक्त रूप से अन्दर की ओर ’’जालीदार शटर’’ है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlElecWeighbridge.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम परिसर /संलग्न परिसर  में मानक क्षमता का चालू हालत में प्रमाणित इलेक्ट्रानिक वेब्रिज है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlFumigation.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('भण्डारित स्कंध के कीटोपचार हेतु संबंधित गोदाम परिसर पर फ्यूमीगेषन कव्हर (IS 14611.1998 or  BIS मानक - Up to date ammendment )सेण्ड स्नेक्स सहित, मानव संसाधन एवं पावर स्प्रेयर पंप आदि उपलब्ध हैं ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlElecBeamScale.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('प्रत्येक गोदाम परिसर में कम से कम 200 किलो क्षमता तक के प्रमाणित इलेक्ट्रानिक बीम स्केल उपलब्ध कराना होंगे  दर्ज करे : :...'); </script> ");
            }
            else if (ddlMarkedMoisture.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम परिसर में ISI मार्क के डिजिटल नमी मापक यंत्र उपलब्ध है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlCameras.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में भण्डारित स्कंध की सुरक्षा-व्यवस्था हेतु उच्च गुणवत्ता के CCTV कैमरे (Night Vision सुविधा सहित) जिसकी मेमोरी दो माह तक सुरक्षित  हैं ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlGuard.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गार्ड रूम के साथ सुरक्षा गार्ड की 24X7 उपलब्धता है  :...'); </script> ");
            }
            else if (ddlMotorableRoadType.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('मोटर योग्य सड़क का प्रकार दर्ज करे : :...'); </script> ");
            }
            else if (txtMotorableRoadWidth.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' मोटर योग्य सड़क की चौड़ाई दर्ज करे : :...'); </script> ");
            }
            else if (ddlRoadSatisfactory.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('सभी मौसमों के लिए रोड की गुणवत्ता संतोषजनक है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlBoundaryWall.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में चारों ओर सीमा में दीवार है और प्रवेश और निकास द्वार है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlGodownSpace.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में उपलब्ध खुली जगह  दर्ज करे : :...'); </script> ");
            }
            else if (ddlProcurement.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम पिछले साल खरीद केंद्र के रूप में काम किया है?  दर्ज करे : :...'); </script> ");
            }
            else if (ddlProcurement.SelectedItem.Text=="Yes" && txtPreviouseCpt.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम पिछले साल खरीद केंद्र के रूप में काम किया है तो पिछले साल कितनी मात्रा की खरीदी गई दर्ज करे : :...'); </script> ");
            }
            else if (ddlwoodenplank.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('पर्याप्त लकड़ी के तख्ते / डनेज और अन्य उपयोग योग्य सामान की उपलब्धता है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlwater.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('पेयजल सुविधा उपलब्ध है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlwaterother.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' गोदाम में स्प्रे और अन्य उपयोग के लिए जल की सुविधा उपलब्ध है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlFumiEquip.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('धूमन और कीट नियंत्रण के लिए पर्याप्त उपकरण  दर्ज करे : :...'); </script> ");
            }
            else if (ddlTechResource.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('अनुभवी तकनीकी संसाधन (बीएससी और 02 साल का अनुभव ) दर्ज करे : :...'); </script> ");
            }
            else if (ddlFumiPestEquipments.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या धूमन और कीट नियंत्रण के लिए सुविधाएं/उपकरण मानकों के अनुसार हैं और गोदाम में उपलब्ध हैं? दर्ज करे :...'); </script> ");
            }
            else if (ddlManpower.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('जब आवश्यक हो तो गोदाम में पर्याप्त श्रमिक(labour) उपलब्ध होगी? दर्ज करे : :...'); </script> ");
            }
            else if (ddlPowerSupply.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('विद्युत आपूर्ति की उपलब्धता  दर्ज करे : :...'); </script> ");
            }
            else if (ddlElectTensionLine.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम को किसी भी तनाव विद्युत लाइन से गुजरने से मुक्त होना चाहिए और इस तरह की रेखाओं से गुजरने की स्थिति में,भंडारण संरचना की योजना बनाते समय प्रासंगिक विद्युत कोड प्रावधानों को ध्यान में रखा जाना चाहिए। गोदाम गैस / तेल पाइप लाइनों से मुक्त होना चाहिए।    दर्ज करे  :...'); </script> ");
            }
            else if (ddlElectWeighBridge.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इलेक्ट्रॉनिक वेब्रिज (तुलाचौकी) दर्ज करे : :...'); </script> ");
            }
            else if (ddlElectWeighBridge.SelectedItem.Text == "Yes" && txtCptElectWeigh.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इलेक्ट्रॉनिक वेब्रिज की क्षमता दर्ज करे : :...'); </script> ");
            }
            else if (ddlCalibration.SelectedItem.Text=="Yes" && txtCalib_cert_date.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इलेक्ट्रॉनिक वेब्रिज  कैलिब्रेशन प्रमाणपत्र जारी करने की तिथि प्रविष्ट करें  दर्ज करे : :...'); </script> ");
            }
            else if (ddlCampusDistance.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यदि इलेक्ट्रॉनिक वेब्रिज परिसर में नहीं है, तो कोई दूसरा प्रमाणित इलेक्ट्रॉनिक वेब्रिज गोदाम से लगभग 500 मीटर की दूरी पर उपलब्ध है? दर्ज करे : :...'); </script> ");
            }
            else if (ddlInternetCon.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इंटरनेट कनेक्टिविटी  दर्ज करे : :...'); </script> ");
            }
            else if (ddlInternetCon.SelectedItem.Text == "Yes" && ddlConType.SelectedItem.Text == "--Select--" && ddlHardwareAvl.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कनेक्टिविटी का प्रकार कंप्यूटर और आवश्यक हार्डवेयर की उपलब्धता  दर्ज करे : :...'); </script> ");
            }
            else if (ddlComputerOperator.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कंप्यूटर ऑपरेटर की उपलब्धता दर्ज करे : :...'); </script> ");
            }
            else if (ddlWareConstruct.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम निर्माणाधीन है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWarelitigation.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम विवादग्रस्त है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWareDamage.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम क्षतिग्रस्त है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWarePrivateDepositor.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम में शासकीय स्कंध के अतिरिक्त पूर्व से स्कंध भण्डारित है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlPvtCpt.SelectedItem.Text=="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('शासकीय स्कंध के अतिरिक्त पुर्व से  भण्डारित स्कंध कि स्थिति मे क्या  गोदाम संचालक द्वारा इस भंडारित स्कंध का उठाव  कर आवस्यक छमता उपलब्ध कराई जाएगी  दर्ज करे : :...'); </script> ");
            }
            else if (ddlBlackList.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम ’’ब्लेक लिस्टेड’’ है ? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWareWrongInfo.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  दर्ज करे : :...'); </script> ");
            }
            else if (ddlFacilities.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या अनिवार्य सुविधा में मानक स्तर की डनेज शीट, कम्प्यूटर सिस्टम, अग्निशामक यंत्र और फायर बकेट्स है एवं बारहमासी (All Weather Approach Road) पहुंचमार्ग जो कम से कम WBM स्तर का हो, यह सभी अनिवार्य सुविधा उपलब्ध नहीं कराई गई हैं? दर्ज करे : :...'); </script> ");
            }
            else if (ddlWareSeasonCpt.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम खरीफ सीजन 2017-18 में आॅफर होने के वाबजूद आवश्यकता होने पर गोदाम संचालक द्वारा अनुबंधित नहीं हुआ है।  दर्ज करे : :...'); </script> ");
            }
            else if (Convert.ToString(((DropDownList)gvGodown.Rows[0].FindControl("ddltxtProcName")).SelectedItem.Text) == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Nearest Procurement Centre Name '); </script> ");
            }
            else if (Convert.ToString(((TextBox)gvGodown.Rows[0].FindControl("txtProcDistance")).Text.ToString()) == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Procurement Centre Distance From Warehouse '); </script> ");
            }
            else if (Convert.ToString(((DropDownList)gvGodown.Rows[0].FindControl("ddlProcDist")).SelectedItem.Text) == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Procurement Centre District '); </script> ");
            }
            else if (ddlfitunfit.SelectedItem.Text == "--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Fit / Unfit'); </script> ");
            }
            else if (txtSystemFitunfit.Text != ddlfitunfit.SelectedItem.Text && txtRemark.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Fill Remark in case of Unfit'); </script> ");
            }
            else if (txtSystemFitunfit.Text == "UNFIT" && txtRemark.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Fill Remark in case of Unfit'); </script> ");
            }
            else if (ddlfitunfit.SelectedItem.Text == "UNFIT" && txtRemark.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Fill Remark in case of Unfit'); </script> ");
            }
            else if (chkDeclaration.Checked == false)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Declaration before proceed'); </script> ");
            }
            else if (Convert.ToDecimal(txtCptJV.Text) < Convert.ToDecimal(txtVacantCpt.Text))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की रिक्त क्षमता ऑफर की गयी क्षमता से अधिक दर्ज किया जाना संभव नहीं हे'); </script> ");
            }
            else
            {
                int chkgdwn = ChkInspectionGdwn();
                if (chkgdwn == 0)
                {
                    InsertInspection();
                    InsertWHMSGdwn();
                    btnsubmit.Enabled = false;
                    if (ddlfitunfit.SelectedItem.Text == "UNFIT")
                    {
                        //SMS
                        Send_Unfit_SMS();
                    }
                }
                else if (chkgdwn == 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Already Inpection Check Inpection Report'); </script> ");
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Warehouse Name and Godown No. Both...'); </script> ");
        }
    }
    public void Send_Unfit_SMS()
    {

        //string Mobileno = "";
        string Mobileno = txtOwnerMob.Text.ToString();
        string UserName = "warehouse";
        string AppPwd = "whr*$123@321#";

        string RegID = "";
        string GodownNo = "";
        RegID = ddlWarName.SelectedValue.ToString();
        GodownNo = ddlgodown.SelectedItem.Text;

        //string msg = "नोट: वेयरहाउस रजिस्ट्रेशन न॰ " + RegID + "(गोदाम क्र॰ " + GodownNo + ")" + " दिनांक:-" + txtInspDate.Text +" को निरीक्षण के दौरान भंडारण के लिए अनुपयुक्त पाया गया, पुनः निरीक्षण के लिए संबन्धित क्षेत्रीय प्रबन्धक को अपील कर सकते है। ";
        string msg = "नोट: वेयरहाउस रजिस्ट्रेशन न॰ " + RegID + "(गोदाम क्र॰ " + GodownNo + ")" + " दिनांक:-" + txtInspDate.Text + " को निरीक्षण के दौरान भंडारण के लिए अनुपयुक्त पाया गया, इस संबंध मे आप अपना अभ्यावेदन संबन्धित क्षेत्रीय प्रबन्धक को 3 दिवस के अंतर्गत अपील के रूप मे कर सकते है। ";

        con.Open();
        Warehouse_SMS.SendSms_WebSrv WSMS = new Warehouse_SMS.SendSms_WebSrv();
        WSMS.Sms(UserName, AppPwd, Mobileno, msg);
        con.Close();
    }
    protected void ddlWDRALic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWDRALic.SelectedItem.Text == "No" || ddlWDRALic.SelectedItem.Text == "--Select--")
        {
            lblWDRALicNo.Visible = false;
            txtWDRALicNo.Visible = false;
            WDRA1.Visible = false;
            WDRA2.Visible = false;
            WDRA3.Visible = false;
            //UploadWDRALic.Visible = false;
            //lvlWDRAAtechFile.Visible = false;
            txtWDRALicNo.Text = "";
            txtWDRAValidDate.Text = "";
            txtWDRAstartDate.Text = "";
            txtWDRALicAuthority.Text = "";
            txtWDRAAgencyCert.Text = "";
            txtWDRADateOfCert.Text="";
        }
        else
        {
            lblWDRALicNo.Visible = true;
            txtWDRALicNo.Visible = true;
            //UploadWDRALic.Visible = true;
            //lvlWDRAAtechFile.Visible = true;
            WDRA1.Visible = true;
            WDRA2.Visible = true;
            WDRA3.Visible = true;
        }
    }
    protected void ddlstateLic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstateLic.SelectedItem.Text == "No" || ddlstateLic.SelectedItem.Text == "--Select--")
        {
            txtlicExpdate.Text = "";
            txtLicenseNo.Text = "";
            txtDateOfIssuance.Text = "";
            trLic3.Visible = false;
            trLic2.Visible = false;
            //lblStateLicAtchFile.Visible = false;
            //UploadWarehouseLicense.Visible = false;
        }
        else
        {
            trLic3.Visible = true;
            trLic2.Visible = true;
            //lblStateLicAtchFile.Visible = true;
            //UploadWarehouseLicense.Visible = true;
        }
    }
    protected void ddlInternetCon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInternetCon.SelectedItem.Text != "No" && ddlInternetCon.SelectedItem.Text != "--Select--")
        {
            trConType.Visible = true;
        }
        else
        {
            trConType.Visible = false;
            ddlConType.SelectedValue = "0";
            ddlHardwareAvl.SelectedValue = "0";
        }
    }
    protected void ddlLicNotPre_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLicNotPre.SelectedItem.Text == "No" || ddlLicNotPre.SelectedItem.Text == "--Select--")
        {
            trLicNotePre.Visible = false;
            txtLicNotPreset.Text = "";
            txtLicNotPresetDate.Text = "";
        }
        else
        {
            trLicNotePre.Visible = true;
        }
    }
    protected void chkboxInsPolicy_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxInsPolicy.Checked == true)
        {
            txtComWareInsName.Text = "";
            txtComWarePolicyNo.Text = "";
            txtSumAssured.Text = "";
            txtComInsuDateIssue.Text = "";
            txtComInsuDateExpired.Text = "";
            lblInsPolicyChkbox.Text = "Y";
        }
        else
        {
            lblInsPolicyChkbox.Text = "N";
        }
    }
    protected void ddlWarePrivateDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes")
        {
            trPvtCpt.Visible = true;
           // trPvtCpt.Visible = false;
            ddlWareSeasonCpt_SelectedIndexChanged(null, null);
        }
        else
        {
            trPvtCpt.Visible = false;
            ddlWareSeasonCpt_SelectedIndexChanged(null, null);
        }
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    //private void AddNewRowToGrid()
    //{

    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        DataRow drCurrentRow = null;

    //        if (dtCurrentTable.Rows.Count > 0)
    //        {
    //            drCurrentRow = dtCurrentTable.NewRow();
    //           // drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

    //          //  drCurrentRow["TID"] = dtCurrentTable.Rows.Count + 1;

    //            drCurrentRow["ProcName"] = "";
    //            drCurrentRow["ProcDist"] = "";
    //            drCurrentRow["District"] = 0;

    //            //add new row to DataTable
    //            dtCurrentTable.Rows.Add(drCurrentRow);
    //            //Store the current data to ViewState
    //            ViewState["CurrentTable"] = dtCurrentTable;

    //            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
    //            {
    //                //if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
    //                //{
    //                //extract the DropDownList Selected Items
    //                //DropDownList ddl1 = (DropDownList)gvImprest.Rows[i].Cells[1].FindControl("DropDownList1");
    //                TextBox ProcName = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtProcName");
    //                TextBox ProcDist = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtProcDistance");
    //                DropDownList District = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddlProcDist");

    //                //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
    //                //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
    //                dtCurrentTable.Rows[i]["ProcName"] = Convert.ToString(ProcName.Text);
    //                dtCurrentTable.Rows[i]["ProcDist"] = Convert.ToString(ProcDist.Text);
    //                dtCurrentTable.Rows[i]["District"] = Convert.ToString(District.SelectedValue.ToString());
    //            }
    //            //Rebind the Grid with the current data
    //            gvGodown.DataSource = dtCurrentTable;
    //            gvGodown.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState is null");
    //    }
    //    SetPreviousData();
    //}
    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["Tid"] = dtCurrentTable.Rows.Count + 1;
               // drCurrentRow["SocietyName"] = "";
                drCurrentRow["ProcDist"] = "";
                drCurrentRow["District"] = 0;

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
                    DropDownList ProcName = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddltxtProcName");
                    TextBox ProcDist = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtProcDistance");
                    DropDownList District = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddlProcDist");

                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Proc_Name"] = Convert.ToString(ProcName.Text);

                    dtCurrentTable.Rows[i]["SocietyName"] = Convert.ToString(ProcName.SelectedValue.ToString());
                    dtCurrentTable.Rows[i]["ProcDist"] = Convert.ToString(ProcDist.Text);
                    dtCurrentTable.Rows[i]["District"] = Convert.ToString(District.SelectedValue.ToString());
                }
                //Rebind the Grid with the current data
                gvGodown.DataSource = dtCurrentTable;
                gvGodown.DataBind();
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

    //private void SetInitialRow()
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr = null;
    //    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //    dt.Columns.Add(new DataColumn("ProcName", typeof(string)));
    //    dt.Columns.Add(new DataColumn("ProcDist", typeof(string)));
    //    dt.Columns.Add(new DataColumn("District", typeof(string)));
    //    dr = dt.NewRow();
    //    dr["RowNumber"] = 1;
    //    dr["ProcName"] = string.Empty;
    //    dr["ProcDist"] = string.Empty;
    //    dr["District"] = 0;
    //    dt.Rows.Add(dr);
    //    ViewState["CurrentTable"] = dt;
    //    gvGodown.DataSource = dt;
    //    gvGodown.DataBind();
    //}

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Tid", typeof(string)));
        dt.Columns.Add(new DataColumn("SocietyName", typeof(string)));
        dt.Columns.Add(new DataColumn("ProcDist", typeof(string)));
        dt.Columns.Add(new DataColumn("District", typeof(string)));
        dr = dt.NewRow();
        dr["Tid"] = 1;
        dr["SocietyName"] = string.Empty;
        dr["ProcDist"] = string.Empty;
        dr["District"] = 0;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        gvGodown.DataSource = dt;
        gvGodown.DataBind();
    }

    private int insertGodownProcCenter()
    {
        int ch = 0;
        int s = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {
            if (((DropDownList)gvGodown.Rows[s].FindControl("ddltxtProcName")).SelectedItem.Text != "" && ((TextBox)gvGodown.Rows[s].FindControl("txtProcDistance")).Text != "" && ((DropDownList)gvGodown.Rows[s].FindControl("ddlProcDist")).SelectedItem.Text.ToString() != "--Select--")
            {
                string qry1 = "INSERT INTO [tbl_Godown_ProcurrementCenter] ([Registration_ID],[Godown_ID],[Godown_No],[Pro_Center_Name],[Proc_Center_Dist],[N_DistrictId],[CreatedBy],[CreatedDate],[Proc_Code])  VALUES ('" + ddlWarName.SelectedValue.ToString() + "','" + ddlgodown.SelectedValue.ToString() + "','" + ddlgodown.SelectedItem.Text + "',N'" + Convert.ToString(((DropDownList)gvGodown.Rows[s].FindControl("ddltxtProcName")).SelectedItem.Text) + "', '" + Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtProcDistance")).Text.ToString()) + "','" + Convert.ToString(((DropDownList)gvGodown.Rows[s].FindControl("ddlProcDist")).SelectedValue.ToString()) + "' ,'" + ip + "',GETDATE(),'" + Convert.ToString(((DropDownList)gvGodown.Rows[s].FindControl("ddltxtProcName")).SelectedValue.ToString()) + "')";
                SqlCommand cmd1 = new SqlCommand(qry1, con);
                int CT1 = 0;
                CT1 = cmd1.ExecuteNonQuery();
                if (CT1 > 0)
                {
                    ch = 1;
                }
            }
        }
        con.Close();
        return ch;
    }
    protected void ddlWareSeasonCpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "No" && ddlWareDamage.SelectedItem.Text == "No" && ddlBlackList.SelectedItem.Text == "No" && ddlWareWrongInfo.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlWareSeasonCpt.SelectedItem.Text == "No")
        {
            if (ddlWarePrivateDepositor.SelectedItem.Text == "No" || ddlWarePrivateDepositor.SelectedItem.Text == "Yes")
            {
                txtSystemFitunfit.Text = "FIT";
            }
            else if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlPvtCpt.SelectedItem.Text == "Yes")
            {
                txtSystemFitunfit.Text = "FIT";
            }
            else if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlPvtCpt.SelectedItem.Text == "No")
            {
                txtSystemFitunfit.Text = "UNFIT";
            }
            else
            {
                txtSystemFitunfit.Text = "UNFIT";
            }
        }
        else
        {
            txtSystemFitunfit.Text = "UNFIT";
        }
    }
    protected void ddlWareConstruct_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlWareDamage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlBlackList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlFacilities_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlWarelitigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlWareWrongInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    protected void ddlfitunfit_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDecFitUnfit.Text = ddlfitunfit.SelectedItem.Text;
    }
    protected void gvGodown_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            con.Open();
            var ddl = (DropDownList)e.Row.FindControl("ddltxtProcName");
            int DistrictId = Convert.ToInt32(e.Row.Cells[0].Text);
            SqlCommand cmd = new SqlCommand("select Society_Name +' ('+ Society_Id +')' as SocietyName,Society_Id from Society where IsWheat='Y' and DistrictId='" + Session["DistID"].ToString() + "' order by SocietyName", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            ddl.DataSource = ds;
            ddl.DataTextField = "SocietyName";
            ddl.DataValueField = "Society_Id";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    public void GetWMSGodownID()
    {
        string qry = "select Godown_Name +' ('+ Godown_ID +')' as GodownName,Godown_ID,Godown_Name as GodownN from tbl_MetaData_Godown where Remarks='Y' and  BranchID='" + Session["UserId"].ToString() + "' order by GodownName";
        SqlCommand cmd = new SqlCommand(qry, sqlcon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ddlGodownWHMS.DataSource = ds.Tables[0];
            //ddlGodownWHMS.DataTextField = "GodownName";
            //ddlGodownWHMS.DataValueField = "Godown_ID";
            //ddlGodownWHMS.DataBind();
            //ddlGodownWHMS.Items.Insert(0, "--Select--");

            ddlWMSGodownID.DataSource = ds.Tables[0];
            ddlWMSGodownID.DataTextField = "GodownName";
            ddlWMSGodownID.DataValueField = "Godown_ID";
            ddlWMSGodownID.DataBind();
            ddlWMSGodownID.Items.Insert(0, "--Select--");
            ddlWMSGodownID.Items.Insert(1, "New Godown");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    public int ChkInspectionGdwn()
    {
        int chk = 0;
        string strsql = "";
        if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            strsql = "select GodownId from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and CreatedDate>= convert(varchar(10),'10/19/2018',101) ";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        {
            strsql = "select GodownId from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and CreatedDate< convert(varchar(10),'10/19/2018',101) ";
        }
       // string strsql = "select GodownId from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }
    protected void ddlPvtCpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWareSeasonCpt_SelectedIndexChanged(null, null);
    }
    private void InsertWHMSGdwn()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string schm = "";
        if (ddlJVCategory.SelectedItem.Text == "55")
        {
            schm = "NON-WDRA";
        }
        else if (ddlJVCategory.SelectedItem.Text == "60")
        {
            schm = "WDRA";
        }
        //string insertqry = "INSERT INTO [tbl_Inspected_Godown_Mapping] ([Godown_ID],[Registration_Id],[Warehouse_Name],[GodownId_JVS],[GodownNo_JVS],[Offer_Capacity],[Vacant_Capacity],[Godown_Type],[DistrictId],[BranchId],[Is_Active],[CreatedBy],[CreatedDate]) VALUES('" + ddlWMSGodownID.SelectedValue.ToString() + "','" + ddlWarName.SelectedValue.ToString() + "','" + txtWareName.Text + "','" + ddlgodown.SelectedValue.ToString() + "','" + ddlgodown.SelectedItem.Text + "','" + txtCptJV.Text + "','" + txtVacantCpt.Text + "','" + schm + "','" + DDLDistrict.SelectedValue.ToString() + "','" + lblBranchID.Text + "','Y','" + ip + "',GETDATE())";
        string insertqry = "INSERT INTO [tbl_Inspected_Godown_Mapping] ([Godown_ID],[Registration_Id],[Warehouse_Name],[GodownId_JVS],[GodownNo_JVS],[Offer_Capacity],[Vacant_Capacity],[Godown_Type],[DistrictId],[BranchId],[Is_Active],[CreatedBy],[CreatedDate],Fit_Unfit) VALUES('" + ddlWMSGodownID.SelectedValue.ToString() + "','" + ddlWarName.SelectedValue.ToString() + "','" + txtWareName.Text + "','" + ddlgodown.SelectedValue.ToString() + "','" + ddlgodown.SelectedItem.Text + "','" + txtCptJV.Text + "','" + txtVacantCpt.Text + "','" + schm + "','" + DDLDistrict.SelectedValue.ToString() + "','" + lblBranchID.Text + "','Y','" + ip + "',GETDATE(),'" + ddlfitunfit.SelectedItem.Text + "')";
        sqlcon.Open();
        SqlCommand cmd2 = new SqlCommand(insertqry, sqlcon);
        int a2 = cmd2.ExecuteNonQuery();
        sqlcon.Close();
    }
    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate >= convert(varchar(10),'10/19/2018',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.Phase>='9' and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by Warehouse_Name ";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        {
            qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate < convert(varchar(10),'10/19/2018',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.Phase<'9' and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by Warehouse_Name ";
        }
        gerreg(qry);
    }
}
