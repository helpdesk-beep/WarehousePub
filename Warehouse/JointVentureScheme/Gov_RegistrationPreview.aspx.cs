using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_Gov_RegistrationPreview : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string SessBranchID = Session["UserId"].ToString();
            if (SessBranchID != "")
            {
                App_Id = SessBranchID;
                if (!IsPostBack)
                {
                    gerreg();
                }
            }
            else
            {

                Response.Redirect("Logins.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public void gerreg()
    {
        qry = "select Warehouse_Name,Registration_Id from tbl_WarehouseRegistration where BranchId='" + Session["UserId"].ToString() + "' and WarehouseHiredtype='1'";
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
    public void GetRegisterationData()
    {
        qry = "SELECT dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.EmailID,WR.Mobile_No,WR.Registration_Id,WR.RegAmt,convert(decimal(18,2),WR.RegCapacity) as RegCapacity,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,CONVERT(varchar(10),WR.Registration_Date,103) as Registration_Date,WR.Incharge_Name,WR.Incharge_Address,WR.Incharge_EmailID,WR.Incharge_MobileNo,WR.WDRA_LicenseNo,CONVERT(varchar(10),WR.WDRA_LicenseDate,103) AS WDRA_LicenseDate,WR.Warehouse_LicenseNo,CONVERT(varchar(10),WR.Warehouse_LicenseDate,103) AS Warehouse_LicenseDate,Bank_Name,IFSC_Code,Account_No,Warehouse_landmark,InchDesignation,(select Agency_Name from tbl_Storage_Agency where SAID=WarehouseHiredtype) as Agency_Name,case when MPWLC_Type ='1'  then 'Owned' when MPWLC_Type ='2' then 'PEG-HLC' when  MPWLC_Type ='3' then 'PEG-FCI' when  MPWLC_Type ='4' then 'Requisition By Collector' when  MPWLC_Type ='5' then 'Private'  else '' end MPWLC_Type FROM tbl_WarehouseRegistration as WR inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_MetaData_DISTRICT  as dt on dt.District_Id=WR.DistrictId where WR.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            App_Id = dt.Rows[0]["Registration_Id"].ToString();
            lblRegId.Text = App_Id;
            lblRegEmail.Text = dt.Rows[0]["EmailID"].ToString();
          //  lblAuth.Text = dt.Rows[0]["Auth_Person"].ToString();
            //lblDOB.Text = dt.Rows[0]["DOB"].ToString();
           // lblRegMobile.Text = dt.Rows[0]["MobileNo"].ToString();
            lblAppType.Text = dt.Rows[0]["Agency_Name"].ToString();
          //  lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString();
            lblWName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblWAddres.Text = dt.Rows[0]["Warehouse_Address"].ToString();
            lbl_wtype.Text = dt.Rows[0]["MPWLC_Type"].ToString();
            lblWtehsil.Text = dt.Rows[0]["Tehsil_Name"].ToString();
            lblWMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            lblWDist.Text = dt.Rows[0]["District_Name"].ToString();
            lblWBranch.Text = dt.Rows[0]["DepotName"].ToString();
            lblWBranchDist.Text = dt.Rows[0]["DistFNBranch"].ToString();
        //    lblPanNo.Text = dt.Rows[0]["PAN_No"].ToString();
        //    lblAadharNo.Text = dt.Rows[0]["Aadhar_No"].ToString();
            lblRegCpt.Text = dt.Rows[0]["RegCapacity"].ToString();
            lblRegDate.Text = dt.Rows[0]["Registration_Date"].ToString();
            lblstateLic.Text = dt.Rows[0]["Warehouse_LicenseNo"].ToString();
            lblstateLic_date.Text = dt.Rows[0]["Warehouse_LicenseDate"].ToString();
            lblWDRALic.Text = dt.Rows[0]["WDRA_LicenseNo"].ToString();
            lblWDRALic_date.Text = dt.Rows[0]["WDRA_LicenseDate"].ToString();
            lblInchAdd.Text = dt.Rows[0]["Incharge_Address"].ToString();
            lblInchEmail.Text = dt.Rows[0]["Incharge_EmailID"].ToString();
            lblInchMob.Text = dt.Rows[0]["Incharge_MobileNo"].ToString();
            lblInchName.Text = dt.Rows[0]["Incharge_Name"].ToString();
            lblbnkName.Text = dt.Rows[0]["Bank_Name"].ToString();
            lblifsc.Text = dt.Rows[0]["IFSC_Code"].ToString();
            lblacount.Text = dt.Rows[0]["Account_No"].ToString();
            lbldesig.Text = dt.Rows[0]["InchDesignation"].ToString();
            lblLandmrk.Text = dt.Rows[0]["Warehouse_landmark"].ToString();
            if (lblstateLic.Text == "")
            {
                lblstateLic_date.Text = "";
            }
            if (lblWDRALic.Text == "")
            {
                lblWDRALic_date.Text = "";
            }
        }
    }
    public void GetWFacility()
    {
        qry = "select Latitude,Longitude,RoadWidth,NoOfGate,convert(decimal(18,0),(WBCapacity)) as WBCapacity,InstallationYear,DistanceFGoodS,DistanceFHighway,DistanceFMandi,DistanceFRailS,case when PowerSupply=1 then '1' when PowerSupply=2 then '2' when PowerSupply=3 then '3' when PowerSupply=0 then 'No Supply' else '-' end PowerSupply,case when RoadType=1 then 'BT' when RoadType=2 then 'CC' when RoadType=3 then 'WBM' else '-' end RoadType,case when GateType=1 then 'Yes' when GateType=0 then 'No' else '-' end GateType,case when HighTentionLine=1 then 'Yes' when HighTentionLine=0 then 'No' else '-' end HighTentionLine,case when WaterFac=1 then 'Yes' when WaterFac=0 then 'No' else '-' end WaterFac,case when WaterOtherUsage=1 then 'Yes' when WaterOtherUsage=0 then 'No' else '-' end WaterOtherUsage,case when CCTV=1 then 'Yes' when CCTV=0 then 'No' else '-' end CCTV,case when FumigationEqup=1 then 'Yes' when FumigationEqup=0 then 'No' else '-' end FumigationEqup,case when GuardWithRoom=1 then 'Yes' when GuardWithRoom=0 then 'No' else '-' end GuardWithRoom,case when PlanksDunnage=1 then 'Yes' when PlanksDunnage=0 then 'No' else '-' end PlanksDunnage,case when FireBuckets=1 then 'Yes' when FireBuckets=0 then 'No' else '-' end FireBuckets,case when FireExting=1 then 'Yes' when FireExting=0 then 'No' else '-' end FireExting,case when WeighBridge=1 then 'Yes' when WeighBridge=0 then 'No' else '-' end WeighBridge,case when WBCertified=1 then 'Yes' when WBCertified=0 then 'No' else '-' end WBCertified,case when InterentCon=1 then 'Yes' when InterentCon=0 then 'No' else '-' end InterentCon,case when ConType=1 then 'Fixed Broadband Connections' when ConType=2 then 'Mobile Internet'  else '-' end ConType,case when HardAvl=1 then 'Yes' when HardAvl=0 then 'No' else '-' end HardAvl,N_RackPoint,DistanceFRackPoint from tbl_WarehouseAdditionalinfo where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbllat.Text = dt.Rows[0]["Latitude"].ToString();
            lbllong.Text = dt.Rows[0]["Longitude"].ToString();
            lblroadtype.Text = dt.Rows[0]["RoadType"].ToString();
            lblRW.Text = dt.Rows[0]["RoadWidth"].ToString();
            lblNFGate.Text = dt.Rows[0]["NoOfGate"].ToString();
            lblEWCap.Text = dt.Rows[0]["WBCapacity"].ToString();
            lblIYear.Text = dt.Rows[0]["InstallationYear"].ToString();
            lblNDGS.Text = dt.Rows[0]["DistanceFGoodS"].ToString();
            lblNDH.Text = dt.Rows[0]["DistanceFHighway"].ToString();
            lblNDR.Text = dt.Rows[0]["DistanceFRailS"].ToString();
            lblNDM.Text = dt.Rows[0]["DistanceFMandi"].ToString();
            lblPower.Text = dt.Rows[0]["PowerSupply"].ToString();
            lblGatetype.Text = dt.Rows[0]["GateType"].ToString();
            lblHTL.Text = dt.Rows[0]["HighTentionLine"].ToString();
            lblWaterFac.Text = dt.Rows[0]["WaterFac"].ToString();
            lblWaterOth.Text = dt.Rows[0]["WaterOtherUsage"].ToString();
            lblCCTV.Text = dt.Rows[0]["CCTV"].ToString();
            lblFumigation.Text = dt.Rows[0]["FumigationEqup"].ToString();
            lblGWGR.Text = dt.Rows[0]["GuardWithRoom"].ToString();
            lblWPD.Text = dt.Rows[0]["PlanksDunnage"].ToString();
            lblFireB.Text = dt.Rows[0]["FireBuckets"].ToString();
            lblFE.Text = dt.Rows[0]["FireExting"].ToString();
            lblEW.Text = dt.Rows[0]["WeighBridge"].ToString();
            lblEWCer.Text = dt.Rows[0]["WBCertified"].ToString();
            lblIntCon.Text = dt.Rows[0]["InterentCon"].ToString();
            lblConType.Text = dt.Rows[0]["ConType"].ToString();
            lblHard.Text = dt.Rows[0]["HardAvl"].ToString();
            lblRackName.Text = dt.Rows[0]["N_RackPoint"].ToString();
            lblracklDist.Text = dt.Rows[0]["DistanceFRackPoint"].ToString();

        }
    }
    //public void GetOfferGdwn()
    //{
    //    qry = "select Godown_No,CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type from tbl_Warehouse_Godown_Offer where Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        GridView1.DataSource = ds;
    //        GridView1.DataBind();
    //    }
    //}

    public void GetGdwn()
    {
        qry = "select Godown_No,CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Width) as G_Width,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,case when StorageType ='1'  then 'Covered' when StorageType ='2' then 'Permanent (CAP)' when  StorageType ='3' then 'Temporary (CAP)' when  StorageType ='4' then 'STEEL SILO'  when  StorageType ='5' then 'SILO BAG' else '' end StorageType from tbl_WarehouseGodown_Reg where G_ScientificCapacity > 0 and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' order by Godown_No ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds;
            gvGodown.DataBind();
        }
    }
    //public void GetOfred()
    //{
    //    qry = "select convert(decimal(18,2),sum(Offer_Capacity)) as Offer_Capacity,sum(OfferAmt) as OfferAmt from tbl_Warehouse_Capacity_Offer where Registration_Id='" + Session["Reg_no"].ToString() + "' ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblOfrCpt.Text = dt.Rows[0]["Offer_Capacity"].ToString();
    //        lbltotalamt.Text = dt.Rows[0]["OfferAmt"].ToString();
    //    }
    //}
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegisterationData();
        GetGdwn();
        GetWFacility();
    }
}
