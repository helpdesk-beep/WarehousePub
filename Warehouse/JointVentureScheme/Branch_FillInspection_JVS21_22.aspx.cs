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
public partial class JointVentureScheme_Branch_FillInspection_JVS21_22 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                GetDist();
                GetWMSGodownID(SessBranchID);
                gerreg();
                lblDate.Text = DateTime.Now.ToString();
                //  fillPaddygridedata();
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    public void GetWMSGodownID(string BID)
    {
        string qry = "select Godown_Name +' ('+ Godown_ID +')' as GodownName,Godown_ID,Godown_Name as GodownN from tbl_MetaData_Godown_2018 where BranchID='" + BID + "' order by GodownName";
        SqlCommand cmd = new SqlCommand(qry, sqlcon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWMSGodownID.DataSource = ds.Tables[0];
            ddlWMSGodownID.DataTextField = "GodownName";
            ddlWMSGodownID.DataValueField = "Godown_ID";
            ddlWMSGodownID.DataBind();
            ddlWMSGodownID.Items.Insert(0, "--Select--");
            ddlWMSGodownID.Items.Insert(1, "New Godown");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHMS Godown Data Found ')", true);
        }
    }
    public void Get_TVillage()
    {
        //string DistrictId = ddlDistrict.SelectedValue.ToString();
        string qry = "select villagenameh,bhucode from VillageLR where  Tehsil_ID='" + DDLTehsil.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        DataSet ds1 = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        if (ds1 == null)
        {
        }
        else
        {
            ddlvillage.DataSource = ds1.Tables[0];
            ddlvillage.DataTextField = "villagenameh";
            ddlvillage.DataValueField = "bhucode";
            ddlvillage.DataBind();
            ddlvillage.Items.Insert(0, "--Select--");
        }
    }

    public void gerreg()
    {
        //string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2020',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2020 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2020_21' or CO.OfferSeason='JVS2020_21' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";
        string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2021',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2021 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2021_22' or CO.OfferSeason='JVS2021_22' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";


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
            qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2021 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2021_22' or OfferSeason='JVS2021_22') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "')";
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
                gerreg();
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
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegisterationData();
        GetGodwnOfferData();
        GetRegGodwnData();
        GetWareAdditionaldata();
        chkfitunfit();
        chkscheme();
        ddllicchk_SelectedIndexChanged(null, null);
        Get_TVillage();
        GetBMName();
    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        Get_Blocks();
        Get_tehsil();
    }
    public void GetRegisterationData()
    {
        qry = "select WR.Registration_Id,wr.DistrictId,wr.TehsilID,wr.BranchId,W_Block,(select Regionnm from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as RegionName ,(select District_Name from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as DistirctName ,(select DepotName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as BranchName ,(select NodalOfficeName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as NodalOfficeName ,(select Tehsil_Name from Tehsils where TehsilCode=wr.TehsilID) as Tehsil_Name ,Warehouse_Name,Warehouse_Address,WR.Mobile_No as OfficeNo ,Incharge_Name,wr.Incharge_MobileNo as InchMob,DistFNBranch ,Bank_Name,IFSC_Code,Account_No,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRAExpDate,Warehouse_LicenseNo,convert(varchar(10),Warehouse_LicenseDate,103) as Warehouse_LicenseDate,PAN_No,Aadhar_No,WPR.Auth_Person,WPR.EmailID,WPR.MobileNo from tbl_WarehouseRegistration AS wr  inner join tbl_Warehouse_PreReg as WPR on WPR.Reg_No=wr.Registration_Id where wr.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //  lblDistID.Text = dt.Rows[0]["DistrictId"].ToString();
            //  lblBranchID.Text = dt.Rows[0]["BranchId"].ToString();
            //    txtRegion.Text = dt.Rows[0]["RegionName"].ToString();
            //    txtDistrict.Text = dt.Rows[0]["DistirctName"].ToString();
            //    txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
            DDLDistrict.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
            DDLDistrict_SelectedIndexChanged(null, null);
            DDLTehsil.SelectedValue = dt.Rows[0]["TehsilID"].ToString();
            ddlblocknew.SelectedValue = dt.Rows[0]["W_Block"].ToString();
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

            //  txtBranchBM.Text = dt.Rows[0]["NodalOfficeName"].ToString();
        }
    }
    public void GetBMName()
    {
        qry = "select NodalOfficeName from tbl_metadata_depot where BranchId='" + Session["UserId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, sqlcon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtBranchBM.Text = dt.Rows[0]["NodalOfficeName"].ToString();
        }
    }

    public void GetGodwnOfferData()
    {
        //qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime FROM [tbl_Warehouse_Godown_Offer_2020] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2020_21' or OfferSeason='JVS2020_21') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date";
        qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime FROM [tbl_Warehouse_Godown_Offer_2021] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2021_22' or OfferSeason='JVS2021_22') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtGodNo.Text = dt.Rows[0]["Godown_No"].ToString();
            //  ddlJVCategory.SelectedValue = dt.Rows[0]["G_Scheme"].ToString();
            txtjvsofrcpt.Text = dt.Rows[0]["G_OfferCapacity"].ToString();
            txtOnlineOffer.Text = dt.Rows[0]["Offer_Date"].ToString();
            ddlCptOffer.SelectedValue = dt.Rows[0]["Capacity_Type"].ToString();
            lblGodownOfferID.Text = dt.Rows[0]["Godown_Offer_Id"].ToString();
            lblOfferID.Text = dt.Rows[0]["Offer_Id"].ToString();
            if (dt.Rows[0]["G_Scheme"].ToString() == "78" || dt.Rows[0]["G_Scheme"].ToString() == "00")
            {
                ddlJVCategory.SelectedValue = "78";
            }
            else if (dt.Rows[0]["G_Scheme"].ToString() == "83" || dt.Rows[0]["G_Scheme"].ToString() == "0")
            {
                ddlJVCategory.SelectedValue = "83";
            }
        }
    }
    public void GetRegGodwnData()
    {
        qry = "select CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_Width)as G_Width,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,LicType,Convert(varchar(10),LicIssueDate,103) as LicIssueDate,LicNo,Convert(varchar(10),LicValidityDate,103) as LicValidityDate from tbl_WarehouseGodown_Reg where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
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
            string lict = dt.Rows[0]["LicType"].ToString().Trim();
            if (lict == "63" || lict == "68")
            {
                ddllicchk.SelectedValue = "1";
                ddlgdwntype.SelectedValue = lict;
                txtlicno.Text = dt.Rows[0]["LicNo"].ToString();
                txtlicissuedate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                txtlicExpdate.Text = dt.Rows[0]["LicValidityDate"].ToString();
            }
            else if (lict == "0" || lict == "00")
            {
                ddllicchk.SelectedValue = "0";
                ddlappliedtype.SelectedValue = lict;
                txtlicappliedno.Text = dt.Rows[0]["LicNo"].ToString();
                txtlicappliceddate.Text = dt.Rows[0]["LicIssueDate"].ToString();
            }
        }
    }
    public void GetWareAdditionaldata()
    {
        qry = "select Latitude,Longitude,FireBuckets,FireExting,RoadType,case when GateType='1' then '1' else '0'  end GateType,case when WeighBridge='True' then '1' else '0'  end WeighBridge,W_BoundaryType from tbl_WarehouseAdditionalinfo where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtLatitude.Text = dt.Rows[0]["Latitude"].ToString();
            txtLongitude.Text = dt.Rows[0]["Longitude"].ToString();
            ddlRoadType.SelectedValue = dt.Rows[0]["RoadType"].ToString();
            lblrode.Text = dt.Rows[0]["RoadType"].ToString();
            ddlboundrytype.SelectedValue = dt.Rows[0]["W_BoundaryType"].ToString();
            lblboundry.Text = dt.Rows[0]["W_BoundaryType"].ToString();
            ddlElectWeigh.SelectedValue = dt.Rows[0]["WeighBridge"].ToString();
            lblelectronicWeib.Text = dt.Rows[0]["WeighBridge"].ToString();
            ddlGateType.SelectedValue = dt.Rows[0]["GateType"].ToString();
            lblgatechk.Text = dt.Rows[0]["GateType"].ToString();

        }
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
        string strDist = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + DDLDistrict.Text + "'";
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
    public void Get_tehsil()
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
    public void Get_Blocks()
    {
        string DistrictId = DDLDistrict.SelectedValue.ToString();
        string qry = "select distinct Block_ID,Block_Name from tbl_Branch_Block_Mapping where District_ID='" + DistrictId + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {

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
    public void chkscheme()
    {
        if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && (ddlboundrytype.SelectedValue != "3" && ddlboundrytype.SelectedItem.Text != "--Select--") && (ddlRoadType.SelectedItem.Text != "--Select--" && ddlRoadType.SelectedValue != "4"))
        {
            ddlinspectionScheme.SelectedValue = "68";
        }
        else if (ddlboundrytype.SelectedItem.Text != "--Select--" && ddlGateType.SelectedItem.Text != "--Select--" && ddlElectWeigh.SelectedItem.Text != "--Select--" && ddlRoadType.SelectedItem.Text != "--Select--")
        {
            ddlinspectionScheme.SelectedValue = "63";
        }
        else
        {
            ddlinspectionScheme.SelectedValue = "0";
        }
    }
    protected void ddlElectWeigh_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (lblelectronicWeib.Text == "0" && ddlElectWeigh.SelectedValue == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to yes')", true);
            ddlElectWeigh.SelectedValue = lblelectronicWeib.Text;
        }
        else
        {
            chkscheme();
        }
    }
    protected void ddlGateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lblgatechk.Text == "0" && ddlGateType.SelectedValue == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Yes')", true);
            ddlGateType.SelectedValue = lblgatechk.Text;
        }
        else
        {
            chkscheme();
        }
    }
    protected void ddlboundrytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lblboundry.Text == "3" && ddlboundrytype.SelectedValue != "3")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Boundry to Yes')", true);
            ddlboundrytype.SelectedValue = lblboundry.Text;
        }
        else
        {
            chkscheme();
        }
    }
    protected void ddlRoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lblboundry.Text == "4" && ddlboundrytype.SelectedValue != "4")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Road to Yes')", true);
            ddlboundrytype.SelectedValue = lblboundry.Text;
        }
        else
        {
            chkscheme();
        }
    }
    //protected void ddllicvalidapplied_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    chkscheme();
    //}

    public void chkfitunfit()
    {
        if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "No" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "No" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "No")
        {
            txtSystemFitunfit.Text = "FIT";
            lblDecFitUnfit.Text = "FIT";
        }
        else if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "No" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "No")
        {
            txtSystemFitunfit.Text = "PENDING";
            lblDecFitUnfit.Text = "PENDING";
            //txtSystemFitunfit.Text = "UNFIT";
            //lblDecFitUnfit.Text = "UNFIT";
        }
        else
        {
            txtSystemFitunfit.Text = "UNFIT";
            lblDecFitUnfit.Text = "UNFIT";
        }
    }
    protected void ddlWareConstruct_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWarelitigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWareDamage_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWarePrivateDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlBlackList_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlcptless_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlFacilities_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlWareSeasonCpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlofrinfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if (ddlWarName.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस का नाम दर्ज करे  :...'); </script> ");
            ddlWMSGodownID.Focus();
        }
        else if (ddlWMSGodownID.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  सॉफ्टवेर (WMS) से  Godown ID दर्ज करे  :...'); </script> ");
            ddlWMSGodownID.Focus();
        }
        else if (txtBranchBM.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('शाखा प्रबन्धक का नाम करे :...'); </script> ");
            txtBranchBM.Focus();
        }
        else if (txtInspDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('निरीक्षण दिनांक दर्ज करे :...'); </script> ");
            txtInspDate.Focus();
        }
        else if (DDLDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' जिले का नाम करे :...'); </script> ");
            DDLDistrict.Focus();
        }
        else if (DDLTehsil.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('तहसील का नाम  दर्ज करे :...'); </script> ");
            DDLTehsil.Focus();
        }
        else if (ddlblocknew.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('विकासखंड का नाम दर्ज करे :...'); </script> ");
            ddlblocknew.Focus();
        }
        //else if (ddlvillage.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गाँव का नाम दर्ज करे :...'); </script> ");
        //}
        else if (ddlNearBranch.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस की निकटतम शाखा का नाम  दर्ज करे :...'); </script> ");
            ddlNearBranch.Focus();
        }
        else if (txtDistance.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस की निकटतम शाखा से गोदाम की दूरी दर्ज करे :...'); </script> ");
            txtDistance.Focus();
        }
        else if (txtWareName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' वेअरहाउस/गोदाम का नाम दर्ज करे :...'); </script> ");
            txtWareName.Focus();
        }
        else if (txtGodNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' वेअरहाउस/गोदाम का  नंबर दर्ज करे :...'); </script> ");
            txtGodNo.Focus();
        }
        else if (txtGodAdd.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' वेअरहाउस का पता दर्ज करे :...'); </script> ");
            txtGodAdd.Focus();
        }
        else if (txtWareArea.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस परिसर का कुल क्षेत्रफल  दर्ज करे :...'); </script> ");
            txtWareArea.Focus();
        }
        else if (txtGodContact.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस कार्यालय का मोबाइल नंबर दर्ज करे : :...'); </script> ");
            txtGodContact.Focus();
        }
        else if (txtInchMbNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस प्रभारी का मोबाइल नंबर  दर्ज करे : :...'); </script> ");
            txtInchMbNo.Focus();
        }
        else if (txtrackpoint.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस की निकटतम रेल्वे रेक पॉइंट  का नाम  दर्ज करे : :...'); </script> ");
            txtrackpoint.Focus();
        }
        else if (txtrackpintdist.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' रेल्वे रेक पॉइंट  से दूरी दर्ज करे  :...'); </script> ");
            txtrackpintdist.Focus();
        }
        else if (txtOwnerName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  संचालक  का नाम दर्ज करे :...'); </script> ");
            txtOwnerName.Focus();
        }
        else if (txtOwnerAdd.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  संचालक  का पता दर्ज करे :...'); </script> ");
            txtOwnerAdd.Focus();
        }
        else if (txtOwnerMob.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  संचालक  का मोबाइल नंबर दर्ज करे : :...'); </script> ");
            txtOwnerMob.Focus();
        }
        else if (txtOwnerEmail.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस  संचालक  का email ID दर्ज करे : :...'); </script> ");
            txtOwnerEmail.Focus();
        }
        else if (txtPanNo.Text == "")
        {

            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('PAN No दर्ज करे : :...'); </script> ");
            txtPanNo.Focus();
        }
        else if (txtAadharNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Aadhaar No. दर्ज करे : :...'); </script> ");
            txtAadharNo.Focus();
        }
        else if (txtLength.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('लंबाई मापन दर्ज करे : :...'); </script> ");
            txtLength.Focus();
        }
        else if (txtWidth.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('चौडाई  मापन दर्ज करे : :...'); </script> ");
            txtWidth.Focus();
        }
        else if (txtHeight.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('ऊँचाई मापन (अधिकतम 18 फिट) दर्ज करे : :...'); </script> ");
            txtHeight.Focus();
        }
        else if (txtMaxCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('संगणित भण्डारण क्षमता दर्ज करे : :...'); </script> ");
            txtMaxCpt.Focus();
        }
        else if (txtConsYear.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('निर्माण वर्ष दर्ज करे : :...'); </script> ");
            txtConsYear.Focus();
        }
        else if (txtUnloadCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम की एक दिन में अनुमानित उतराई क्षमता  दर्ज करे : :...'); </script> ");
            txtUnloadCpt.Focus();
        }
        else if (txtCommodityName.Text != "" && txtDepositorName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में संग्रहित स्कंध जमाकर्ता का नाम एवं संग्रहित स्कंध की क्षमता दर्ज करे : :...'); </script> ");
            txtDepositorName.Focus();
        }
        else if (txtvacantcpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वर्तमान में गोदाम की रिक्त क्षमता दर्ज करे : :...'); </script> ");
            txtvacantcpt.Focus();
        }
        else if (txtlicno.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        }
        else if (txtjvsofrcpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वर्तमान में गोदाम की रिक्त क्षमता दर्ज करे : :...'); </script> ");
            txtjvsofrcpt.Focus();
        }
        else if (Convert.ToDecimal(txtvacantcpt.Text) > Convert.ToDecimal(txtjvsofrcpt.Text))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' रिक्त क्षमता गोदाम की ऑफर की गई क्षमता से ज्यादा दर्ज करना संभव नहीं हे : :...'); </script> ");
            txtvacantcpt.Focus();
        }
        else if (ddlWareConstruct.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम निर्माणाधीन है ? दर्ज करे : :...'); </script> ");
            ddlWareConstruct.Focus();
        }
        else if (ddlWarelitigation.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम विवादग्रस्त है ? दर्ज करे : :...'); </script> ");
            ddlWarelitigation.Focus();
        }
        else if (ddlWareDamage.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम क्षतिग्रस्त है ? दर्ज करे : :...'); </script> ");
            ddlWareDamage.Focus();
        }
        else if (ddlWarePrivateDepositor.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम में शासकीय स्कंध के अतिरिक्त पूर्व से स्कंध भण्डारित है ? दर्ज करे : :...'); </script> ");
            ddlWarePrivateDepositor.Focus();
        }
        else if (ddlBlackList.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम ’’ब्लेक लिस्टेड’’ है ? दर्ज करे : :...'); </script> ");
            ddlBlackList.Focus();
        }
        else if (ddlofrinfo.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  दर्ज करे : :...'); </script> ");
            ddlofrinfo.Focus();
        }
        else if (ddlcptless.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम की भण्डारण क्षमता एक परिसर में न्यूनतम 500 मे.टन से कम है ?  दर्ज करे : :...'); </script> ");
            ddlcptless.Focus();
        }
        else if (ddlFacilities.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम वैज्ञानिक भंडारण हेतु अयोग्य हे ( यदि गोदाम योग्य हे तो यह भी सुनिश्चित करे की वायुसंचरण हेतु रोशनदान हो तथा गोदाम की ऊंचाई कम से कम 14 फिट है) दर्ज करे : :...'); </script> ");
            ddlFacilities.Focus();
        }
        else if (ddlvillage.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' गाँव का नाम  दर्ज करे : :...'); </script> ");
            ddlvillage.Focus();
        }
        else if (txtRemark.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृप्या रिमार्क लिखें'); </script> ");
            txtSystemFitunfit.Focus();
        }
        else if (chkDeclaration.Checked == false)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Declaration before proceed'); </script> ");
            chkDeclaration.Focus();
        }
        else if (lblchklicnovalid.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम लायसेंस/पंजीयन नंबर को चेक करने के बाद ही गोदाम का निरीक्षण पुर्ण किया जाना संभव हे '); </script> ");
        }
        else if (lblchklicnovalid.Text == "No")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        }
        else if (ddlWarePrivateDepositor.SelectedItem.Text.Trim() == "Yes" && txtCommodityName.Text == "" && txtDepositorName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यदि गोदाम में स्कंध भंडारित हे तो संग्रहित स्कंध जमाकर्ता का नाम ,क्षमता (मे.टन),स्कंधो का नाम दुर्ज करे'); </script> ");
        }
        //else if (txtSystemFitunfit.Text=="UNFIT")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('जो गोदाम अपात्र हे उनका निरीक्षण ऑनलाइन अपडेट किया जाना अभी संभव नहीं हे'); </script> ");
        //}
        else
        {
            int chkgdwn = ChkInspectionGdwn();
            if (chkgdwn == 0)
            {
                string Slicno = "";
                string SDate = "";
                string SLicType = "";
                string Slicexp = "";
                if (ddllicchk.SelectedItem.Text == "Yes")
                {
                    Slicno = txtlicno.Text.Trim();
                    SDate = txtlicissuedate.Text.Trim();
                    SLicType = ddlgdwntype.SelectedValue.ToString().Trim();
                    Slicexp = txtlicExpdate.Text.Trim();
                }
                else if (ddllicchk.SelectedItem.Text == "No")
                {
                    Slicno = txtlicappliedno.Text.Trim();
                    SDate = txtlicappliceddate.Text.Trim();
                    SLicType = ddlappliedtype.SelectedValue.ToString().Trim();
                    Slicexp = "";
                }
                string WMS_Godown = "";
                if (ddlWMSGodownID.SelectedItem.Text == "New Godown")
                {
                    WMS_Godown = "NG";
                }
                else
                {
                    WMS_Godown = ddlWMSGodownID.SelectedValue.ToString();
                }

                string WLicNo = "";
                string WIssueDate = "";
                string WValiDDate = "";
                string WLicType = "";
                string NWLicNo = "";
                string NWIssueDate = "";
                string NWValiDDate = "";
                string NLicType = "";
                string AWLicNo = "";
                string AWIssueDate = "";
                string ALicType = "";
                if (ddllicchk.SelectedItem.Text.Trim() == "Yes")
                {
                    //if (ddlgdwntype.SelectedValue == "74")
                    if (ddlgdwntype.SelectedValue == "68")
                    {
                        WLicNo = txtlicno.Text.Trim();
                        WIssueDate = getDate_MDY(txtlicissuedate.Text);
                        WValiDDate = getDate_MDY(txtlicExpdate.Text);
                        WLicType = "1";
                    }
                    //else if (ddlgdwntype.SelectedValue == "69")
                    else if (ddlgdwntype.SelectedValue == "63")
                    {
                        NWLicNo = txtlicno.Text.Trim();
                        NWIssueDate = txtlicissuedate.Text;
                        NWValiDDate = txtlicExpdate.Text;
                        NLicType = "1";
                    }
                }
                else if (ddllicchk.SelectedItem.Text.Trim() == "No")
                {
                    AWLicNo = txtlicappliedno.Text;
                    AWIssueDate = txtlicappliceddate.Text;
                    ALicType = ddlappliedtype.SelectedValue.ToString();
                }
                if (txtCurrentStoredComm.Text == "")
                {
                    txtCurrentStoredComm.Text = "0";
                }
                string Insp_Scheme = "";
                if (ddlinspectionScheme.SelectedValue.ToString() == "63")
                {
                    Insp_Scheme = "78";
                }
                else if (ddlinspectionScheme.SelectedValue.ToString() == "68")
                {
                    Insp_Scheme = "83";
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string Srch_inspectionID = ChkInspectionID();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "')";
                qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "')";

                SqlCommand cmd1 = new SqlCommand(qry, con);
                int a = cmd1.ExecuteNonQuery();
                if (a == 1)
                {
                    ModalPopupExtender1.Show();
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save'); </script> ");
                    //lblchklicnovalid.Text = "";
                    //txtWareName.Text = ""; txtInspDate.Text = ""; txtvacantcpt.Text = "";
                    //ddlWarName_SelectedIndexChanged(null, null);

                }
            }
            else if (chkgdwn == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Already Inpection Check Inpection Report'); </script> ");
            }
        }
    }
    protected void ddllicchk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllicchk.SelectedItem.Text == "Yes")
        {
            trlicapplied1.Visible = false;
            trlicapplied2.Visible = false;
            trlic1.Visible = true;
            trlic2.Visible = true;
            chkscheme();
        }
        else if (ddllicchk.SelectedItem.Text == "No")
        {
            trlic1.Visible = false;
            trlic2.Visible = false;
            trlicapplied1.Visible = true;
            trlicapplied2.Visible = true;
            chkscheme();
        }
    }
    public int ChkInspectionGdwn()
    {
        int chk = 0;
        string strsql = "";
        strsql = "select GodownId from tbl_Godown_Inspection where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and Godown_Offer_Id='" + lblGodownOfferID.Text + "'";
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
        if ((str3 != String.Empty) || str3 != "" || str3 != "0")
        {
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "20" + Convert.ToString(Convert.ToInt32(str3) + 1);
            MaxInsID = ddlWarName.SelectedValue.ToString() + "21" + Convert.ToString(Convert.ToInt32(str3) + 1);

        }
        else
        {
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "201";
            MaxInsID = ddlWarName.SelectedValue.ToString() + "211";

        }
        con.Close();
        return MaxInsID;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void btnvalidatelicno_Click(object sender, EventArgs e)
    {
        if (ddlgdwntype.SelectedItem.Text == "WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        {
            if (txtlicno.Text != "")
            {
                string strsql = "";
                strsql = "select WHCode,Validupto from [tbl_WDRA_Registration] where  WHCode='" + txtlicno.Text + "'";
                SqlCommand cmd = new SqlCommand(strsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
                    lblchklicnovalid.Text = "Yes";
                }
                else
                {
                    lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
                    lblchklicnovalid.Text = "No";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस WDRA लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                }
            }
        }
        else if (ddlgdwntype.SelectedItem.Text == "NON WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        {
            if (txtlicno.Text != "")
            {
                string strsql = "";
                strsql = "select Whr_ID,IssuedAnugyptivalidityDate from [tbl_LiencesRegistrationNew] where Whr_ID='" + txtlicno.Text + "'";
                SqlCommand cmd = new SqlCommand(strsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
                    lblchklicnovalid.Text = "Yes";
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    strsql = "select * from tbl_LiencesRegistrationOld where Whr_ID='" + txtlicno.Text + "' ";
                    SqlCommand cmd1 = new SqlCommand(strsql, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
                        lblchklicnovalid.Text = "Yes";
                    }
                    else if (ds1.Tables[0].Rows.Count == 0)
                    {
                        strsql = "select * from tbl_LicencesRegistration_2020 where Whr_ID='" + txtlicno.Text + "' ";
                        SqlCommand cmd2 = new SqlCommand(strsql, con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
                            lblchklicnovalid.Text = "Yes";
                        }
                        else
                        {
                            lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
                            lblchklicnovalid.Text = "No";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                        }
                    }
                    else
                    {
                        lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
                        lblchklicnovalid.Text = "No";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                    }
                }
                else
                {
                    lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
                    lblchklicnovalid.Text = "No";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                }
            }
        }
        else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "0")
        {
            if (txtlicappliedno.Text != "")
            {
                string strsql = "";
                strsql = "select WHCode,Validupto from [tbl_WDRA_Registration] where  WHCode='" + txtlicappliedno.Text + "'";
                SqlCommand cmd = new SqlCommand(strsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
                    lblchklicnovalid.Text = "Yes";
                }
                else
                {
                    lbllicmessage.Text = "आवेदन नंबर वेध्य नहीं हे";
                    lblchklicnovalid.Text = "No";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस WDRA पंजीयन/आवेदन नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                }
            }
        }
        else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "00")
        {
            if (txtlicappliedno.Text != "")
            {
                string strsql = "";
                strsql = "select Whr_ID,IssuedAnugyptivalidityDate from [tbl_LiencesRegistrationNew] where Whr_ID='" + txtlicappliedno.Text + "'";
                SqlCommand cmd = new SqlCommand(strsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
                    lblchklicnovalid.Text = "Yes";
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    strsql = "select * from tbl_LiencesRegistrationOld where Whr_ID='" + txtlicappliedno.Text + "' ";
                    SqlCommand cmd1 = new SqlCommand(strsql, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
                        lblchklicnovalid.Text = "Yes";
                    }
                    else if (ds1.Tables[0].Rows.Count == 0)
                    {
                        strsql = "select * from tbl_LicencesRegistration_2020 where APPL_CODE='" + txtlicappliedno.Text + "' ";
                        SqlCommand cmd2 = new SqlCommand(strsql, con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
                            lblchklicnovalid.Text = "Yes";
                        }
                        else
                        {
                            lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
                            lblchklicnovalid.Text = "No";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                        }
                    }
                }
                else
                {
                    lbllicmessage.Text = "आवेदन नंबर वेध्य नहीं हे";
                    lblchklicnovalid.Text = "No";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस पंजीयन/आवेदन नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
                }
            }
        }
        else
        {
            lblchklicnovalid.Text = "No";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस पंजीयन/आवेदन/लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Branch_FillInspectionNew.aspx");
        Response.Redirect("Branch_FillInspection_JVS21_22.aspx");
    }

    private void fillPaddygridedata()
    {
        string qry = "";
        string DistrictName = "";
        qry = "select District_Name from tbl_MetaData_DISTRICT where District_Id='" + Session["DistID"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DistrictName = dt1.Rows[0]["District_Name"].ToString();
        }

        // qry = "select (select District_Name from tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=WLICNEW.District_ID ) as District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationNew as WLICNEW where District_ID='" + Session["DistID"].ToString() + "' union select District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationOld as LICOLD where LICOLD.District='" + DistrictName + "' order by District,Whr_Name";

        qry = "select (select District_Name from tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=tbl_LiencesRegistrationNew.District_ID ) as District, UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',IssuedAnugytikramank as 'Whr_ID',convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address,Total_capicity from tbl_LiencesRegistrationNew where IssuedAnugytikramank in (select Whr_ID from tbl_LiencesRegistrationOld ) and IssuedAnugytikramank is not null and District_ID='" + Session["DistID"].ToString() + "' union select (select District_Name from tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=tbl_LiencesRegistrationNew.District_ID ) as District, UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',Whr_ID,convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address ,Total_capicity from tbl_LiencesRegistrationNew where IssuedAnugytikramank is null and District_ID='" + Session["DistID"].ToString() + "' ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_Paddy.DataSource = ds;
            GD_Paddy.DataBind();
        }
    }
    private void fillWDRALicDATA()
    {
        string qry = "select NameandAddress,WarehousemanName,Mobile,Capacity,WHCode,convert(varchar(10),Validupto,103) as ExpDate from tbl_WDRA_Registration order by NameandAddress";
        //qry = "select (select District_Name from tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=WLICNEW.District_ID ) as District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationNew as WLICNEW where District_ID='" + Session["DistID"].ToString() + "' union select District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationOld as LICOLD where LICOLD.District='" + DistrictName + "' order by District,Whr_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }

    protected void lnklicdetail_Click(object sender, EventArgs e)
    {

        if (ddlgdwntype.SelectedItem.Text == "NON WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        {
            fillPaddygridedata();
            ModalPopupExtender2.Show();
        }
        else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "00")
        {
            fillPaddygridedata();
            ModalPopupExtender2.Show();
        }
        else if (ddlgdwntype.SelectedItem.Text == "WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        {
            fillWDRALicDATA();
            ModalPopupExtender3.Show();
        }
        else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "0")
        {
            fillWDRALicDATA();
            ModalPopupExtender3.Show();
        }

    }
}