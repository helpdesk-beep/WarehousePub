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
using System.Drawing;
public partial class JointVentureScheme_Branch_FillInspection_Kharif_JVS24_25 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    public string Srch_inspectionID = "";
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
                // CheckAllreadyExist();
                lbluser.Text = SessBranch;
                GetDist();
                GetWMSGodownID(SessBranchID);
                gerreg();
                GetFacilities();

                lblDate.Text = DateTime.Now.ToString();
                //  fillPaddygridedata();
                txtSystemFitunfit.Text = "FIT";
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    public void GetWMSGodownID(string BID)
    {
        string qry = "select Godown_Name +' ('+ Godown_ID +')' as GodownName,Godown_ID,Godown_Name as GodownN from Intergrated_MP_STORAGE.dbo.tbl_MetaData_Godown_2018 where BranchID='" + BID + "'  and IsActive='Y' order by GodownName";
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
        // string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'03/08/2022',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2022 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2022_23' or CO.OfferSeason='JVS2022_23' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";
        //string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'03/08/2022',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2022 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2022_23' or CO.OfferSeason='JVS2022_23' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   ";
        //string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate >= convert(varchar(10),'02/27/2023',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_warehouse_capacity_offer_Rabi2023 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='Rab2023_24' or CO.OfferSeason='Rab2023_24' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name";
        string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id AND (SELECT DATEDIFF(DAY, TransactionDate, Getdate()) AS DateDiff)<=1099) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate >= convert(varchar(10),'03/15/2024',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_Rabi_2024_25 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='Rab2024_25' or CO.OfferSeason='Rab2024_25' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  ";
        //string Rabi23_24= Session["Rab2023_24"].ToString();
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
            /*  qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2022 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2022_23' or OfferSeason='JVS2022_23') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2022 as INSP where INSP.Registration_Id ='" + */
            //qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2022 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2022_23' or OfferSeason='JVS2022_23')";
            //qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2022 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2022_23' or OfferSeason='JVS2022_23') and Godown_ID not in (select GodownID from tbl_Godown_Inspection_2022 where Registration_Id = '" + ddlWarName.SelectedValue.ToString() + "')";
           // qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_Rabi2023 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='Rab2023_24' or OfferSeason='Rab2023_24') and Godown_ID not in (select GodownID from tbl_Godown_Inspection_2023 where CreatedDate>='2023-12-05'  AND Jvs_session!=' Rab2023_24' AND Registration_Id = '" + ddlWarName.SelectedValue.ToString() + "')";
            qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_Rabi_2024_25 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='Rab2024_25' or OfferSeason='Rab2024_25') and Godown_ID not in (select GodownID from tbl_Godown_Inspection_2024_Kharif where Registration_Id = '" + ddlWarName.SelectedValue.ToString() + "')";
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
    public void CheckAllreadyExist()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Private_Warehouse_Related_Information_Check_By_GodownID]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Registration_ID", ddlWarName.SelectedValue);
        cmd.Parameters.AddWithValue("@GodownID", ddlWMSGodownID.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            if (dt.Rows[0]["Obstruction_BYGO"].ToString() == "Y" || dt.Rows[0]["Warehouse_Infested_Status"].ToString() == "Y")
            {
                if (dt.Rows[0]["LicNum"].ToString() != "")
                {
                    if (dt.Rows[0]["Is_allowed"].ToString() == "Y")
                    {

                        if (ViewState["Category"].ToString() == "A" && ViewState["Maintain_By"].ToString() == "1" || ViewState["Maintain_By"].ToString() == "2")
                        {
                            ddlJVCategory.SelectedValue = "50";
                        }
                        else if (ViewState["Category"].ToString() == "B" && ViewState["Maintain_By"].ToString() == "2" || ViewState["Maintain_By"].ToString() == "1")
                        {
                            ddlJVCategory.SelectedValue = "50";
                        }
                    }
                    else
                    {
                        string strMsg = "प्रिय गोदाम संचालक अभी आप ऑफर नहीं कर पाएंगे, क्योंकि आपके गोदाम में कीटग्रस्ता पाई गई होगी या आपके द्वारा स्कंध के भुगतान में व्यवधान उत्पन्न किया गया होगा।  ।";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/JointVentureScheme/BranchHome.aspx';", true);
                    }
                }
            }
            else
            {
                // string strMsg = "data has been final submitted successfully";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            }
        }

    }
    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOfferGodown();
        //CheckAllreadyExist();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegisterationData();
        GetGodwnOfferData();
        GetRegGodwnData();
        GetWareAdditionaldata();
        chkfitunfit();
        //chkscheme();
        //ddllicchk_SelectedIndexChanged(null, null);
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
        qry = "select WR.Registration_Id,wr.DistrictId,wr.TehsilID,wr.BranchId,W_Block,(select Regionnm from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as RegionName ,(select District_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as DistirctName ,(select DepotName from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT where BranchId=WR.BranchId) as BranchName ,(select NodalOfficeName from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT where BranchId=WR.BranchId) as NodalOfficeName ,(select Tehsil_Name from Tehsils where TehsilCode=wr.TehsilID) as Tehsil_Name ,Warehouse_Name,Warehouse_Address,WR.Mobile_No as OfficeNo ,Incharge_Name,wr.Incharge_MobileNo as InchMob,DistFNBranch ,Bank_Name,IFSC_Code,Account_No,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRAExpDate,Warehouse_LicenseNo,convert(varchar(10),Warehouse_LicenseDate,103) as Warehouse_LicenseDate,PAN_No,Aadhar_No,WPR.Auth_Person,WPR.EmailID,WPR.MobileNo from tbl_WarehouseRegistration AS wr  inner join tbl_Warehouse_PreReg as WPR on WPR.Reg_No=wr.Registration_Id where wr.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
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
        qry = "select NodalOfficeName from Intergrated_MP_STORAGE.dbo.tbl_metadata_depot where BranchId='" + Session["UserId"].ToString() + "'";
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
        //qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime FROM [tbl_Warehouse_Godown_Offer_2020] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2020_21' or OfferSeason='JVS2020_21') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2022 as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date";
        //qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime FROM [tbl_Warehouse_Godown_Offer_2022] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='JVS2022_23' or OfferSeason='JVS2022_23') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2022 as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date" +"";
        //qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime FROM [tbl_Warehouse_Godown_Offer_Rabi2023] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='Rab2023_24' or OfferSeason='Rab2023_24') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2023 as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date" +"";
       // qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime,Category,Maintain_By,Selected_Priority FROM [tbl_Warehouse_Godown_Offer_Rabi2023] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='Rab2023_24' or OfferSeason='Rab2023_24') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2023 as INSP where Jvs_session='Kharif2023-24' AND INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date,Category,Maintain_By,Selected_Priority" + "";
        qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),sum(G_OfferCapacity)) as G_OfferCapacity,G_Scheme,Capacity_Type,[Offer_Date],convert(varchar(10),[Offer_Date],108) as Offertime,Category,Maintain_By,Selected_Priority FROM [tbl_Warehouse_Godown_Offer_Rabi_2024_25] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and (OfferSeason='Rab2024_25' or OfferSeason='Rab2024_25') and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Inspection_2024_Kharif as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "') group by [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],G_Scheme,Capacity_Type,Offer_Date,Category,Maintain_By,Selected_Priority" + "";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //txtCategory.Text = dt.Rows[0]["Category"].ToString();
            //txtMaintain_By.Text = dt.Rows[0]["Maintain_By"].ToString();
            txtGodNo.Text = dt.Rows[0]["Godown_No"].ToString();
            //ddlJVCategory.SelectedValue = dt.Rows[0]["Category"].ToString();
            txtjvsofrcpt.Text = dt.Rows[0]["G_OfferCapacity"].ToString();
            txtOnlineOffer.Text = dt.Rows[0]["Offer_Date"].ToString();
            ddlCptOffer.SelectedValue = dt.Rows[0]["Capacity_Type"].ToString();
            lblGodownOfferID.Text = dt.Rows[0]["Godown_Offer_Id"].ToString();
            lblOfferID.Text = dt.Rows[0]["Offer_Id"].ToString();
            //if (dt.Rows[0]["G_Scheme"].ToString() == "78" || dt.Rows[0]["G_Scheme"].ToString() == "00")
            ViewState["Category"] = dt.Rows[0]["Category"].ToString();
            ViewState["Maintain_By"] = dt.Rows[0]["Maintain_By"].ToString();
            Session["Selected_Priority"] = dt.Rows[0]["Selected_Priority"].ToString();
            //if (dt.Rows[0]["Category"].ToString() == "A" && dt.Rows[0]["Maintain_By"].ToString() == "1")
            //{
            //    ddlJVCategory.SelectedValue = "80";
            //}

            //else if (dt.Rows[0]["Category"].ToString() == "B" && dt.Rows[0]["Maintain_By"].ToString() == "1")
            //{
            //    ddlJVCategory.SelectedValue = "75";
            //}

            //else if (dt.Rows[0]["Category"].ToString() == "A" && dt.Rows[0]["Maintain_By"].ToString() == "2")
            //{
            //    ddlJVCategory.SelectedValue = "55";
            //}
            //else if (dt.Rows[0]["Category"].ToString() == "B" && dt.Rows[0]["Maintain_By"].ToString() == "2")
            //{
            //    ddlJVCategory.SelectedValue = "50";
            //}
            if (dt.Rows[0]["Maintain_By"].ToString() == "1" && (dt.Rows[0]["Selected_Priority"].ToString() == "1" || dt.Rows[0]["Selected_Priority"].ToString() == "2"))
            {
                ddlJVCategory.SelectedValue = "67";
            }
            else if (dt.Rows[0]["Maintain_By"].ToString() == "1" && (dt.Rows[0]["Selected_Priority"].ToString() == "3" || dt.Rows[0]["Selected_Priority"].ToString() == "4" || dt.Rows[0]["Selected_Priority"].ToString() == "5"))
            {
                ddlJVCategory.SelectedValue = "62";
            }
            else if (dt.Rows[0]["Maintain_By"].ToString() == "2" && (dt.Rows[0]["Selected_Priority"].ToString() == "1" || dt.Rows[0]["Selected_Priority"].ToString() == "2"))
            {
                ddlJVCategory.SelectedValue = "45";
            }
            else if (dt.Rows[0]["Maintain_By"].ToString() == "2" && (dt.Rows[0]["Selected_Priority"].ToString() == "3" || dt.Rows[0]["Selected_Priority"].ToString() == "4" || dt.Rows[0]["Selected_Priority"].ToString() == "5"))
            {
                ddlJVCategory.SelectedValue = "40";
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
                //ddllicchk.SelectedValue = "1";
                //ddlgdwntype.SelectedValue = lict;
                txtlicno.Text = dt.Rows[0]["LicNo"].ToString();
                //txtlicissuedate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                //txtlicExpdate.Text = dt.Rows[0]["LicValidityDate"].ToString();
            }
            else if (lict == "0" || lict == "00")
            {
                //ddllicchk.SelectedValue = "0";
                // ddlappliedtype.SelectedValue = lict;
                //txtlicappliedno.Text = dt.Rows[0]["LicNo"].ToString();
                //txtlicappliceddate.Text = dt.Rows[0]["LicIssueDate"].ToString();
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
            //ddlRoadType.SelectedValue = dt.Rows[0]["RoadType"].ToString();
            //lblrode.Text = dt.Rows[0]["RoadType"].ToString();
            //ddlboundrytype.SelectedValue = dt.Rows[0]["W_BoundaryType"].ToString();
            //lblboundry.Text = dt.Rows[0]["W_BoundaryType"].ToString();
            //ddlElectWeigh.SelectedValue = dt.Rows[0]["WeighBridge"].ToString();
            //lblelectronicWeib.Text = dt.Rows[0]["WeighBridge"].ToString();
            //ddlGateType.SelectedValue = dt.Rows[0]["GateType"].ToString();
            //lblgatechk.Text = dt.Rows[0]["GateType"].ToString();

        }
    }
    private void GetDist()
    {
        string strDist = "SELECT District_Name,District_Id FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT order by District_Name";
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
        string strDist = "SELECT BranchId,DepotName FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT where DistrictId='" + DDLDistrict.Text + "'";
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
        if (ddlElectWeigh.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select सी.सी./सुदृढ़सुदृढ़ डामरीकृत/WBM')", true);
        }
        else if (ddlGateType.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select इलेक्ट्रॉनिक वेब्रिज')", true);

        }
        else if (ddlboundrytype.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select जालीदार शटर/जालीदार गेट')", true);

        }
        else if (ddlRoadType.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select नाइट विजन चालू हालत में कैमरे')", true);

        }
        else if (ddlInternet.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select कार्यालय, इंटरनेट सुविधायुक्‍त')", true);

        }
        else
        {

           // if (ddlJVCategory.SelectedValue == "80" || ddlJVCategory.SelectedValue == "75")
           if (ddlJVCategory.SelectedValue == "67" || ddlJVCategory.SelectedValue == "62")
            {
                if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && ddlboundrytype.SelectedValue == "1" && ddlRoadType.SelectedValue == "1" && ddlInternet.SelectedValue == "1")
                {
                    //ddlinspectionScheme.SelectedValue = "80";
                    ddlinspectionScheme.SelectedValue = "67";
                }
                else
                {
                    //ddlinspectionScheme.SelectedValue = "75";
                    ddlinspectionScheme.SelectedValue = "62";
                }
            }
            //else if (ddlJVCategory.SelectedValue == "55" || ddlJVCategory.SelectedValue == "50")
            else if (ddlJVCategory.SelectedValue == "45" || ddlJVCategory.SelectedValue == "40")
            {
                if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && ddlboundrytype.SelectedValue == "1" && ddlRoadType.SelectedValue == "1" && ddlInternet.SelectedValue == "1")
                {
                    //ddlinspectionScheme.SelectedValue = "55";
                    ddlinspectionScheme.SelectedValue = "45";
                }
                else
                {
                    //ddlinspectionScheme.SelectedValue = "50";
                    ddlinspectionScheme.SelectedValue = "40";
                }
            }
            //if (ddlJVCategory.SelectedValue == "55" || ddlJVCategory.SelectedValue == "50")
            //{

            //if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && ddlboundrytype.SelectedValue == "1" && ddlRoadType.SelectedValue == "1" && ddlJVCategory.SelectedValue == "55")
            //{
            //    ddlinspectionScheme.SelectedValue = "55";
            //}

            //else
            //{
            //    ddlinspectionScheme.SelectedValue = "50";
            //}
            //}
        }
    }

    //if (ddlJVCategory.SelectedValue == "60" || ddlJVCategory.SelectedValue == "55")
    //{

    //    if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && (ddlboundrytype.SelectedValue != "3" && ddlboundrytype.SelectedItem.Text != "--Select--") && (ddlRoadType.SelectedItem.Text != "--Select--" && ddlRoadType.SelectedValue != "4") && (ddlJVCategory.SelectedValue == "60"))
    //    {
    //        ddlinspectionScheme.SelectedValue = "60";
    //    }

    //    else
    //    {
    //        ddlinspectionScheme.SelectedValue = "55";
    //    }


    //}

    //if (ddlJVCategory.SelectedValue == "80" || ddlJVCategory.SelectedValue == "75")
    //{
    //    if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && ddlboundrytype.SelectedValue == "1"  && ddlRoadType.SelectedValue == "1" && (ddlJVCategory.SelectedValue == "80"))
    //    {
    //        ddlinspectionScheme.SelectedValue = "80";
    //    }
    //    else
    //    {
    //        ddlinspectionScheme.SelectedValue = "75";
    //    }
    //}

    //if (ddlJVCategory.SelectedValue == "55" || ddlJVCategory.SelectedValue == "50")
    //{

    //    if (ddlElectWeigh.SelectedValue == "1" && ddlGateType.SelectedValue == "1" && (ddlboundrytype.SelectedValue != "3" && ddlboundrytype.SelectedItem.Text != "--Select--") && (ddlRoadType.SelectedItem.Text != "--Select--" && ddlRoadType.SelectedValue != "4") && (ddlJVCategory.SelectedValue == "55"))
    //    {
    //        ddlinspectionScheme.SelectedValue = "55";
    //    }

    //    else
    //    {
    //        ddlinspectionScheme.SelectedValue = "50";
    //    }


    //}


    //}
    protected void ddlElectWeigh_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (ddlElectWeigh.SelectedValue == "1")
        //{
        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to yes')", true);
        //ddlElectWeigh.SelectedValue = lblelectronicWeib.Text;
        //ddlElectWeigh.SelectedValue = "0";
        chkscheme();
        //}
        //else
        //{

        //}
    }
    protected void ddlGateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlGateType.SelectedValue == "1")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Yes')", true);
        //    //ddlGateType.SelectedValue = "0";
        //}
        //else
        //{
        chkscheme();
        //}
    }
    protected void ddlboundrytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (lblboundry.Text == "0" && ddlboundrytype.SelectedValue == "1")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Boundry to Yes')", true);
        //    ddlboundrytype.SelectedValue = lblboundry.Text;
        //}
        //else
        //{
        chkscheme();
        //}
    }
    protected void ddlRoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (lblboundry.Text == "0" && ddlboundrytype.SelectedValue == "1")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Cannot Update No to Road to Yes')", true);
        //    ddlboundrytype.SelectedValue = lblboundry.Text;
        //}
        //else
        //{
        chkscheme();
        //}
    }
    //protected void ddllicvalidapplied_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    chkscheme();
    //}

    public void chkfitunfit()
    {
        //if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "Yes" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "No" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "Yes" && ddlGunnybals.SelectedItem.Text == "No" && ddlpurchaceCommodity.SelectedItem.Text == "No" && ddlGunnybals.SelectedItem.Text == "No" && ddlpurchaceCommodity.SelectedItem.Text == "No" && ddlIslicenseAvailable.SelectedItem.Text == "Yes" && ddlIsCaptureByBank.SelectedItem.Text == "No" && ddlIsFullfillPointThreeInstruction.SelectedItem.Text == "Yes")
        //{
        //    txtSystemFitunfit.Text = "FIT";
        //    lblDecFitUnfit.Text = "FIT";
        //}
        //else if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "Yes" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "No" && ddlGunnybals.SelectedItem.Text == "Yes" && ddlpurchaceCommodity.SelectedItem.Text == "Yes" && ddlGunnybals.SelectedItem.Text == "Yes" && ddlpurchaceCommodity.SelectedItem.Text == "No" && ddlIslicenseAvailable.SelectedItem.Text == "No" && ddlIsCaptureByBank.SelectedItem.Text == "Yes" && ddlIsFullfillPointThreeInstruction.SelectedItem.Text == "No")
        //{
        //    txtSystemFitunfit.Text = "PENDING";
        //    lblDecFitUnfit.Text = "PENDING";
        //    //txtSystemFitunfit.Text = "UNFIT";
        //    //lblDecFitUnfit.Text = "UNFIT";
        //}
        if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "Yes" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "No" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "Yes" && ddlIslicenseAvailable.SelectedItem.Text == "Yes" && ddlIsCaptureByBank.SelectedItem.Text == "No" && ddlIsFullfillPointThreeInstruction.SelectedItem.Text == "Yes")
        {
            txtSystemFitunfit.Text = "FIT";
            lblDecFitUnfit.Text = "FIT";
        }
        else if (ddlWareConstruct.SelectedItem.Text == "No" && ddlWarelitigation.SelectedItem.Text == "Yes" && ddlWareDamage.SelectedItem.Text == "No" && ddlWarePrivateDepositor.SelectedItem.Text == "Yes" && ddlBlackList.SelectedItem.Text == "No" && ddlcptless.SelectedItem.Text == "No" && ddlFacilities.SelectedItem.Text == "No" && ddlofrinfo.SelectedItem.Text == "No" && ddlIslicenseAvailable.SelectedItem.Text == "No" && ddlIsCaptureByBank.SelectedItem.Text == "Yes" && ddlIsFullfillPointThreeInstruction.SelectedItem.Text == "No")
        {
            txtSystemFitunfit.Text = "PENDING";
            lblDecFitUnfit.Text = "PENDING";
            //txtSystemFitunfit.Text = "UNFIT";
            //lblDecFitUnfit.Text = "UNFIT";
        }
        else
        {
            txtSystemFitunfit.Text = "PENDING";
            lblDecFitUnfit.Text = "PENDING";
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
    protected void ddlGunnybals_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }
    protected void ddlpurchaceCommodity_SelectedIndexChanged(object sender, EventArgs e)
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



        else if (ddlinsurance.SelectedValue == "YES" && txtSLDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इंश्योरेंस दिनांक दर्ज करे'); </script> ");
            txtSLDate.Focus();
        }

        else if (ddlinsurance.SelectedValue == "YES" && TextBox1.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इंश्योरेंस की समप्ति दिनांक दर्ज करे'); </script> ");
            TextBox1.Focus();
        }

        else if (ddlinsurance.SelectedValue == "YES" && txtInsuranceCapacity.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' इंश्योरेंस किये गए स्टॉक की क्षमता दर्ज करे'); </script> ");
            txtInsuranceCapacity.Focus();
        }

        else if (ddlinsurance.SelectedValue == "YES" && txtvaluestk.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('इंश्योरेंस किये गए स्टॉक की कीमत दर्ज करे'); </script> ");
            txtvaluestk.Focus();
        }

        else if (ddlinsurance.SelectedValue == "YES" && txtPremium.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Premium / अधिमूल्य राशि दर्ज करे'); </script> ");
            txtPremium.Focus();
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
        //else if (txtlicno.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //}
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
        else if (ddlGunnybals.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  दर्ज करे : :...'); </script> ");
            ddlGunnybals.Focus();
        }
        else if (ddlpurchaceCommodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या ऑनलाइन ऑफर संबंधी जानकारी गलत दी गयी है ?  दर्ज करे : :...'); </script> ");
            ddlpurchaceCommodity.Focus();
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
        //else if (lblchklicnovalid.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम लायसेंस/पंजीयन नंबर को चेक करने के बाद ही गोदाम का निरीक्षण पुर्ण किया जाना संभव हे '); </script> ");
        //}


        //else if (lblchklicnovalid.Text == "No")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //}

        else if (ddlinsurance.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में रवि गोदाम का इंश्योरेंस दर्ज करे '); </script> ");
            ddlinsurance.Focus();
        }

        else if (ddlGunnybals.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में बारदाना (Gunny bells ) भंडारित हैं?  दर्ज करे : :...'); </script> ");
            ddlGunnybals.Focus();
        }

        else if (ddlpurchaceCommodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम में खरीदी किया जा रहा स्कंध पूर्व से भंडारित ?  दर्ज करे : :...'); </script> ");
            ddlpurchaceCommodity.Focus();
        }

        else if (ddlIslicenseAvailable.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('स्कन्द की प्रथम भण्डारण दिनांक को WDRA पंजीयन अथवा राज्य शासन का वेयरहाउस लाइसेंस उपलब्ध?  दर्ज करे : :...'); </script> ");
            ddlIslicenseAvailable.Focus();
        }

        else if (ddlIsCaptureByBank.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम सरफेसी (वित्तीय आस्तियो के प्रतिभूतिकरण और पुननिर्माण और सुरक्षाहित) एक्ट-2002 तहत बैंक का कब्ज़ा है?  दर्ज करे : :...'); </script> ");
            ddlIsCaptureByBank.Focus();
        }

        else if (ddlIsFullfillPointThreeInstruction.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या जे.व्ही.एस. पालिसी में वर्णित कण्डिका 3 की समस्त अहार्ताएं पूर्ण करता हैं?  दर्ज करे : :...'); </script> ");
            ddlIsFullfillPointThreeInstruction.Focus();
        }
        else if (ddlWdragdwntype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Wdra license Type : :...'); </script> ");
            ddlWdragdwntype.Focus();
        }
        else if (ddlfssaigdwntype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Fassi Godown Type : :...'); </script> ");
            ddlfssaigdwntype.Focus();
        }
        else if (ddlNonWdragdwntype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Non WDRA license: :...'); </script> ");
            ddlNonWdragdwntype.Focus();
        }
        //else if (txtWdralicExpdate.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Lic Ex. Date : :...'); </script> ");
        //    txtWdralicExpdate.Focus();
        //}
        //else if (txtWdralicissuedate.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Lic Issue. Date : :...'); </script> ");
        //    txtWdralicissuedate.Focus();
        //}

        else if (ddlfssaigdwntype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter WDRA license Type : :...'); </script> ");
            ddlfssaigdwntype.Focus();
        }

        else if (txtfssailicno.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Fssai License No. : :...'); </script> ");
            txtfssailicno.Focus();
        }
        //else if (txtfssailicissuedate.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Fssai Issue Date. : :...'); </script> ");
        //    txtfssailicissuedate.Focus();
        //}

        //else if (txtfssailicExpdate.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Fssai Ex Date. : :...'); </script> ");
        //    txtfssailicExpdate.Focus();
        //}
        else if (ddlNonWdragdwntype.SelectedItem.Value == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Fssai Ex Date. : :...'); </script> ");
            ddlNonWdragdwntype.Focus();
        }

        else if (txtNonWdralicno.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Non Wdra Licence. : :...'); </script> ");
            txtNonWdralicno.Focus();
        }

        //else if (txtNonWdralicissuedate.Text == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Non Wdra Issue Date. : :...'); </script> ");
        //    txtNonWdralicissuedate.Focus();
        //}
        //else if (ddlIsBlockLastYear.SelectedItem.Text == "--Select--")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदामों में विगत वर्ष भण्डारित स्कंध की गुणवत्ता प्रतिकूल रूप से प्रभावित होने पर क्या ऐसे गोदाम संचालकों द्वारा आवेदन प्रस्तुत करते हुए, शपथ पत्र प्रस्तुत किया हैं?  दर्ज करे : :...'); </script> ");
        //    ddlIsBlockLastYear.Focus();
        //}




        else if (ddlWarePrivateDepositor.SelectedItem.Text.Trim() == "Yes" && txtCommodityName.Text == "" && txtDepositorName.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यदि गोदाम में स्कंध भंडारित हे तो संग्रहित स्कंध जमाकर्ता का नाम ,क्षमता (मे.टन),स्कंधो का नाम दर्ज करे'); </script> ");
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
                //string Slicno = "";
                //string SDate = "";
                //string SLicType = "";
                //string Slicexp = "";
                //if (ddllicchk.SelectedItem.Text == "Yes")
                //{
                //    Slicno = txtlicno.Text.Trim();
                //    SDate = txtlicissuedate.Text.Trim();
                //    SLicType = ddlgdwntype.SelectedValue.ToString().Trim();
                //    Slicexp = txtlicExpdate.Text.Trim();
                //}
                //else if (ddllicchk.SelectedItem.Text == "No")
                //{
                //    //Slicno = txtlicappliedno.Text.Trim();
                //    //SDate = txtlicappliceddate.Text.Trim();
                //    //SLicType = ddlappliedtype.SelectedValue.ToString().Trim();
                //    Slicexp = "";
                //}
                string WMS_Godown = "";
                if (ddlWMSGodownID.SelectedItem.Text == "New Godown")
                {
                    WMS_Godown = "NG";
                }
                else
                {
                    WMS_Godown = ddlWMSGodownID.SelectedValue.ToString();
                }

                //string WLicNo = "";
                //string WIssueDate = "";
                //string WValiDDate = "";
                //string WLicType = "";
                //string NWLicNo = "";
                //string NWIssueDate = "";
                //string NWValiDDate = "";
                //string NLicType = "";
                //string AWLicNo = "";
                //string AWIssueDate = "";
                //string ALicType = "";
                //if (ddllicchk.SelectedItem.Text.Trim() == "Yes")
                //{
                //    //if (ddlgdwntype.SelectedValue == "74")
                //    if (ddlgdwntype.SelectedValue == "68")
                //    {
                //        WLicNo = txtlicno.Text.Trim();
                //        WIssueDate = getDate_MDY(txtlicissuedate.Text);
                //        WValiDDate = getDate_MDY(txtlicExpdate.Text);
                //        WLicType = "1";
                //    }
                //else if (ddlgdwntype.SelectedValue == "69")
                //    else if (ddlgdwntype.SelectedValue == "63")
                //    {
                //        NWLicNo = txtlicno.Text.Trim();
                //        NWIssueDate = txtlicissuedate.Text;
                //        NWValiDDate = txtlicExpdate.Text;
                //        NLicType = "1";
                //    }
                //}
                //else if (ddllicchk.SelectedItem.Text.Trim() == "No")
                //{
                //    //AWLicNo = txtlicappliedno.Text;
                //    //AWIssueDate = txtlicappliceddate.Text;
                //   // ALicType = ddlappliedtype.SelectedValue.ToString();
                //}
                if (txtCurrentStoredComm.Text == "")
                {
                    txtCurrentStoredComm.Text = "0";
                }
                string Insp_Scheme = "";

                Insp_Scheme = ddlJVCategory.SelectedValue;
                if (ddlinspectionScheme.SelectedValue.ToString() == "63")
                {
                    Insp_Scheme = "80";
                }
                else if (ddlinspectionScheme.SelectedValue.ToString() == "68")
                {
                    Insp_Scheme = "85";
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                Srch_inspectionID = ChkInspectionID();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2022] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "')";

                //if (ddlinsurance.SelectedValue == "YES")
                //{
                //    qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2022] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[InsurenceCapacity],[valuestk],[Premium],[Idate],[Idatevaladity],[Jvs_Flag]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "','Rabi_2022_23','" + txtInsuranceCapacity.Text.Trim() + "','" + txtvaluestk.Text.Trim() + "','" + txtPremium.Text.Trim() + "','" + txtSLDate.Text.Trim() + "','" + TextBox1.Text.Trim() + "','" + ddlinsurance.SelectedValue + "')";
                //}

                //else
                {
                    //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2022] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[Jvs_Flag],[Is_Guuny_Storage],[Is_Proc_Cmd_Stored]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "','Rabi_2022_23','" + ddlinsurance.SelectedValue + "','" + ddlGunnybals.SelectedValue + "','" + ddlpurchaceCommodity.SelectedValue + "')";
                    //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2023] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[Present_Validity_WDRA],[WDRA_LicenseNo],[WDRA_LIssueDate],[WDRA_LicenseDate],[WDRAL_Present_Validity],[Warehouse_LicenseNo],[Warehouse_LicenseDate],[Warehouse_LIssueDate],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Lic_Navinikaran_no],[Lic_Navinikaran_Date],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[Jvs_Flag],[Is_Guuny_Storage],[Is_Proc_Cmd_Stored],[Is_license_Available],[Is_Capture_By_Bank],[Is_Fullfill_Point_Three_Instruction],[fssai_lic_no],[fssai_lic_issue_date],[fssai_lic_Exp_date],[Non_Wdra_lic_no],[Non_Wdra_lic_issue_date],[Non_Wdra_lic_Exp_date]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + WLicType + "','" + WLicNo + "','" + getDate_MDY(WIssueDate) + "','" + getDate_MDY(WValiDDate) + "','" + NLicType + "','" + NWLicNo + "','" + getDate_MDY(NWValiDDate) + "','" + getDate_MDY(NWIssueDate) + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + AWLicNo + "','" + getDate_MDY(AWIssueDate) + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "','Rabi_2022_23','" + ddlinsurance.SelectedValue + "','" + ddlGunnybals.SelectedValue + "','" + ddlpurchaceCommodity.SelectedValue + "','" + ddlIslicenseAvailable.SelectedValue + "','" + ddlIsCaptureByBank.SelectedValue + "','" + ddlIsFullfillPointThreeInstruction.SelectedValue + "','" + txtfssailicno.Text + "','" + txtfssailicissuedate.Text + "','" + txtfssailicExpdate.Text + "','" + txtNonWdralicno.Text + "','" + txtNonWdralicissuedate.Text + "','" + txtNonWdralicExpdate.Text + "')";
                    //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2023] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[Jvs_Flag],[Is_Guuny_Storage],[Is_Proc_Cmd_Stored],[Is_license_Available],[Is_Capture_By_Bank],[Is_Fullfill_Point_Three_Instruction],[fssai_lic_no],[fssai_lic_issue_date],[fssai_lic_Exp_date],[Non_Wdra_lic_no],[Non_Wdra_lic_issue_date],[Non_Wdra_lic_Exp_date],[fssai_gdwn_type],[NonWdra_gdwn_type],[Wdra_gdwn_type],[Wdra_lic_issue_date],[Wdra_lic_Exp_date],[Internet_Facility],[Wdra_lic_no]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + Insp_Scheme + "','" + ddlvillage.SelectedValue.ToString().Trim() + "',' Rab2023_24','" + ddlinsurance.SelectedValue + "','" + ddlGunnybals.SelectedValue + "','" + ddlpurchaceCommodity.SelectedValue + "','" + ddlIslicenseAvailable.SelectedValue + "','" + ddlIsCaptureByBank.SelectedValue + "','" + ddlIsFullfillPointThreeInstruction.SelectedValue + "','" + txtfssailicno.Text + "','" + getDate_MDY(txtfssailicissuedate.Text) + "','" + getDate_MDY(txtfssailicExpdate.Text) + "','" + txtNonWdralicno.Text + "','" + getDate_MDY(txtNonWdralicissuedate.Text) + "','" + getDate_MDY(txtNonWdralicExpdate.Text) + "','" + ddlfssaigdwntype.SelectedValue + "','" + ddlNonWdragdwntype.SelectedValue + "','" + ddlWdragdwntype.SelectedValue + "','" + getDate_MDY(txtWdralicissuedate.Text) + "','" + getDate_MDY(txtWdralicExpdate.Text) + "','" + ddlInternet.SelectedValue + "','" + txtlicno.Text + "')";
                    //qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2023] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[Jvs_Flag],[Is_Guuny_Storage],[Is_Proc_Cmd_Stored],[Is_license_Available],[Is_Capture_By_Bank],[Is_Fullfill_Point_Three_Instruction],[fssai_lic_no],[fssai_lic_issue_date],[fssai_lic_Exp_date],[Non_Wdra_lic_no],[Non_Wdra_lic_issue_date],[Non_Wdra_lic_Exp_date],[fssai_gdwn_type],[NonWdra_gdwn_type],[Wdra_gdwn_type],[Wdra_lic_issue_date],[Wdra_lic_Exp_date],[Internet_Facility],[Wdra_lic_no]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + ddlinspectionScheme.SelectedValue + "','" + ddlvillage.SelectedValue.ToString().Trim() + "',' Rab2023_24','" + ddlinsurance.SelectedValue + "','" + ddlGunnybals.SelectedValue + "','" + ddlpurchaceCommodity.SelectedValue + "','" + ddlIslicenseAvailable.SelectedValue + "','" + ddlIsCaptureByBank.SelectedValue + "','" + ddlIsFullfillPointThreeInstruction.SelectedValue + "','" + txtfssailicno.Text + "','" + getDate_MDY(txtfssailicissuedate.Text) + "','" + getDate_MDY(txtfssailicExpdate.Text) + "','" + txtNonWdralicno.Text + "','" + getDate_MDY(txtNonWdralicissuedate.Text) + "','" + getDate_MDY(txtNonWdralicExpdate.Text) + "','" + ddlfssaigdwntype.SelectedValue + "','" + ddlNonWdragdwntype.SelectedValue + "','" + ddlWdragdwntype.SelectedValue + "','" + getDate_MDY(txtWdralicissuedate.Text) + "','" + getDate_MDY(txtWdralicExpdate.Text) + "','" + ddlInternet.SelectedValue + "','" + txtlicno.Text + "')";

                    // Session Chahnge For Kharif 2023-24

                    qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Inspection_2024_Kharif] ([Inspection_Id],[Godown_Offer_Id],[Offer_Id],[Registration_Id],[GodownId],[Godown_No],[RegionId],[DistrictId],[BranchId],[Insp_Date],[Insp_Name_MPWLC],[Godown_Postal_Address],[Ware_DistrictId],[Ware_TehsilId],[Ware_Latitude],[Ware_Longitude],[Ware_Campus_Area],[Ware_ContactNo],[Ware_Inch_MobileNo],[Ware_Nearest_Branch],[Ware_Distance_MPWLC],[Owner_Name],[Owner_Postal_Add],[Owner_MobileNo],[Owner_Email],[Owner_PanNo],[Owner_AadharNo],[Capacity_Type],[G_OfferCapacity],[Offer_Scheme],[G_Length],[G_Width],[G_Height],[G_MaxCapacity],[G_ConstructedYear],[G_Unload_Capacity],[Stored_Commodities],[Stored_Com_Depositor],[Utilized_Capacity],[Vacant_Capacity],[RoadType],[Elec_Weigh_Avail],G_Gates_Condition,W_BoundaryType,[IsWare_Under_Constr],[IsWare_Disputed],[IsWare_Damage],[IsWareS_Comm_NGovt],[IsWare_BlackListed],[IsOnline_Info_Wrong],[IsMandatory_Condi_NAvail],[IsWH_CplessthenFiveHundred],[CreatedBy],[CreatedDate],[RackPoint],[RackPoint_Dist],[WMS_GodownID],[Fit_Unfit],[Remark],[AutoFit_Unfit],[Insp_Offer_Scheme],[Insp_Village_Code],[Jvs_session],[Jvs_Flag],[Is_Guuny_Storage],[Is_Proc_Cmd_Stored],[Is_license_Available],[Is_Capture_By_Bank],[Is_Fullfill_Point_Three_Instruction],[fssai_lic_no],[fssai_lic_issue_date],[fssai_lic_Exp_date],[Non_Wdra_lic_no],[Non_Wdra_lic_issue_date],[Non_Wdra_lic_Exp_date],[fssai_gdwn_type],[NonWdra_gdwn_type],[Wdra_gdwn_type],[Wdra_lic_issue_date],[Wdra_lic_Exp_date],[Internet_Facility],[Wdra_lic_no]) VALUES ('" + Srch_inspectionID.Trim() + "','" + lblGodownOfferID.Text.Trim() + "','" + lblOfferID.Text.Trim() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtGodNo.Text + "','" + lblRegionID.Text.Trim() + "','" + DDLDistrict.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + getDate_MDY(txtInspDate.Text) + "','" + txtBranchBM.Text + "','" + txtGodAdd.Value + "','" + DDLDistrict.SelectedValue.ToString() + "','" + DDLTehsil.SelectedValue.ToString() + "','" + txtLatitude.Text + "','" + txtLongitude.Text + "','" + txtWareArea.Text + "','" + txtGodContact.Text + "','" + txtInchMbNo.Text + "','" + ddlNearBranch.SelectedValue.ToString() + "','" + txtDistance.Text + "','" + txtOwnerName.Text + "','" + txtOwnerAdd.Text + "','" + txtOwnerMob.Text + "','" + txtOwnerEmail.Text + "','" + txtPanNo.Text + "','" + txtAadharNo.Text + "','" + ddlCptOffer.SelectedValue.ToString() + "','" + txtjvsofrcpt.Text + "','" + ddlJVCategory.SelectedValue.ToString() + "','" + txtLength.Text + "','" + txtWidth.Text + "','" + txtHeight.Text + "','" + txtMaxCpt.Text + "','" + txtConsYear.Text + "','" + txtUnloadCpt.Text + "','" + txtCommodityName.Text + "','" + txtDepositorName.Text + "','" + txtCurrentStoredComm.Text + "','" + txtvacantcpt.Text + "','" + ddlRoadType.SelectedValue.ToString() + "','" + ddlElectWeigh.SelectedValue.ToString() + "','" + ddlGateType.SelectedValue.ToString() + "','" + ddlboundrytype.SelectedValue.ToString() + "','" + ddlWareConstruct.SelectedValue.ToString() + "','" + ddlWarelitigation.SelectedValue.ToString() + "','" + ddlWareDamage.SelectedValue.ToString() + "','" + ddlWarePrivateDepositor.SelectedValue.ToString() + "','" + ddlBlackList.SelectedValue.ToString() + "','" + ddlofrinfo.SelectedValue.ToString() + "','" + ddlFacilities.SelectedValue.ToString() + "','" + ddlcptless.SelectedValue.ToString() + "','" + ip + "',GETDATE(),'" + txtrackpoint.Text + "','" + txtrackpintdist.Text + "','" + WMS_Godown + "','" + txtSystemFitunfit.Text + "','" + txtRemark.Value + "','" + txtSystemFitunfit.Text + "','" + ddlinspectionScheme.SelectedValue + "','" + ddlvillage.SelectedValue.ToString().Trim() + "','Kharif2024_25','" + ddlinsurance.SelectedValue + "','" + ddlGunnybals.SelectedValue + "','" + ddlpurchaceCommodity.SelectedValue + "','" + ddlIslicenseAvailable.SelectedValue + "','" + ddlIsCaptureByBank.SelectedValue + "','" + ddlIsFullfillPointThreeInstruction.SelectedValue + "','" + txtfssailicno.Text + "','" + getDate_MDY(txtfssailicissuedate.Text) + "','" + getDate_MDY(txtfssailicExpdate.Text) + "','" + txtNonWdralicno.Text + "','" + getDate_MDY(txtNonWdralicissuedate.Text) + "','" + getDate_MDY(txtNonWdralicExpdate.Text) + "','" + ddlfssaigdwntype.SelectedValue + "','" + ddlNonWdragdwntype.SelectedValue + "','" + ddlWdragdwntype.SelectedValue + "','" + getDate_MDY(txtWdralicissuedate.Text) + "','" + getDate_MDY(txtWdralicExpdate.Text) + "','" + ddlInternet.SelectedValue + "','" + txtlicno.Text + "')";
                }


                //////////
                //SqlCommand cmd1 = new SqlCommand("sp_fertilizer_insecticide", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.AddWithValue("@Inspection_Id", Srch_inspectionID.Trim());
                //cmd1.Parameters.AddWithValue("@Godown_Offer_Id", lblGodownOfferID.Text.Trim());
                //cmd1.Parameters.AddWithValue("@Offer_Id", lblOfferID.Text.Trim());
                //cmd1.Parameters.AddWithValue("@Registration_Id",ddlWarName.SelectedValue.Trim());
                //cmd1.Parameters.AddWithValue("@GodownId", ddlgodown.SelectedValue.Trim());
                //cmd1.Parameters.AddWithValue("@Godown_No", txtGodNo.Text);
                //cmd1.Parameters.AddWithValue("@RegionId", lblRegionID.Text.Trim()); 
                //cmd1.Parameters.AddWithValue("@DistrictId", DDLDistrict.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@BranchId", Session["UserId"].ToString());
                //cmd1.Parameters.AddWithValue("@Insp_Date", getDate_MDY(txtInspDate.Text));
                //cmd1.Parameters.AddWithValue("@Insp_Name_MPWLC", txtBranchBM.Text);
                //cmd1.Parameters.AddWithValue("@Godown_Postal_Address", txtGodAdd.Value);
                //cmd1.Parameters.AddWithValue("@Ware_DistrictId", DDLDistrict.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@Ware_TehsilId", DDLTehsil.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@Ware_Latitude", txtLatitude.Text);
                //cmd1.Parameters.AddWithValue("@Ware_Longitude", txtLongitude.Text);
                //cmd1.Parameters.AddWithValue("@Ware_Campus_Area", txtWareArea.Text);
                //cmd1.Parameters.AddWithValue("@Ware_ContactNo", txtGodContact.Text);
                //cmd1.Parameters.AddWithValue("@Ware_Inch_MobileNo", txtInchMbNo.Text);
                //cmd1.Parameters.AddWithValue("@Ware_Nearest_Branch", ddlNearBranch.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@Ware_Distance_MPWLC", txtDistance.Text);  
                //cmd1.Parameters.AddWithValue("@Owner_Name", txtOwnerName.Text);
                //cmd1.Parameters.AddWithValue("@Owner_Postal_Add", txtOwnerAdd.Text   );
                //cmd1.Parameters.AddWithValue("@Owner_MobileNo", txtOwnerMob.Text );
                //cmd1.Parameters.AddWithValue("@Owner_Email", txtOwnerEmail.Text);
                //cmd1.Parameters.AddWithValue("@Owner_PanNo", txtPanNo.Text);
                //cmd1.Parameters.AddWithValue("@Owner_AadharNo", txtAadharNo.Text);
                //cmd1.Parameters.AddWithValue("@Capacity_Type", ddlCptOffer.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@G_OfferCapacity",txtjvsofrcpt.Text);
                //cmd1.Parameters.AddWithValue("@Offer_Scheme", ddlJVCategory.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@G_Length",txtLength.Text);
                //cmd1.Parameters.AddWithValue("@G_Width",txtWidth.Text);
                //cmd1.Parameters.AddWithValue("@G_Height",txtHeight.Text);
                //cmd1.Parameters.AddWithValue("@G_MaxCapacity",txtMaxCpt.Text);
                //cmd1.Parameters.AddWithValue("@G_ConstructedYear", txtConsYear.Text);
                //cmd1.Parameters.AddWithValue("@G_Unload_Capacity", txtUnloadCpt.Text);
                //cmd1.Parameters.AddWithValue("@Stored_Commodities", txtCommodityName.Text);
                //cmd1.Parameters.AddWithValue("@Stored_Com_Depositor", txtDepositorName.Text);
                //cmd1.Parameters.AddWithValue("@Utilized_Capacity", txtCurrentStoredComm.Text);
                //cmd1.Parameters.AddWithValue("@Vacant_Capacity", txtvacantcpt.Text);
                //cmd1.Parameters.AddWithValue("@RoadType", ddlRoadType.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@Elec_Weigh_Avail",ddlElectWeigh.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@G_Gates_Condition",ddlGateType.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@W_BoundaryType",ddlboundrytype.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWare_Under_Constr",ddlWareConstruct.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWare_Disputed", ddlWarelitigation.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWare_Damage", ddlWareDamage.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWareS_Comm_NGovt",ddlWarePrivateDepositor.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWare_BlackListed",ddlBlackList.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsOnline_Info_Wrong", ddlofrinfo.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsMandatory_Condi_NAvail",ddlFacilities.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@IsWH_CplessthenFiveHundred", ddlcptless.SelectedValue.ToString());
                //cmd1.Parameters.AddWithValue("@CreatedBy",ip);   
                ////c1md.Parameters.AddWithValue("@CreatedBy", Request.UserHostAddress); 
                //cmd1.Parameters.AddWithValue("@CreatedDate", Request.UserHostAddress);  
                //cmd1.Parameters.AddWithValue("@RackPoint", txtrackpoint.Text);
                //cmd1.Parameters.AddWithValue("@RackPoint_Dist", txtrackpintdist.Text);
                //cmd1.Parameters.AddWithValue("@WMS_GodownID",WMS_Godown);
                //cmd1.Parameters.AddWithValue("@Fit_Unfit", txtSystemFitunfit.Text);
                //cmd1.Parameters.AddWithValue("@Remark", txtRemark.Value);
                //cmd1.Parameters.AddWithValue("@AutoFit_Unfit", txtSystemFitunfit.Text);
                //cmd1.Parameters.AddWithValue("@Insp_Offer_Scheme", Insp_Scheme);
                //cmd1.Parameters.AddWithValue("@Insp_Village_Code", ddlvillage.SelectedValue.ToString().Trim());
                //cmd1.Parameters.AddWithValue("@Jvs_session", Session["Rab2023_24"].ToString());
                //cmd1.Parameters.AddWithValue("@Jvs_Flag",ddlinsurance.SelectedValue );
                //cmd1.Parameters.AddWithValue("@Is_Guuny_Storage",ddlGunnybals.SelectedValue);
                //cmd1.Parameters.AddWithValue("@Is_Proc_Cmd_Stored",ddlpurchaceCommodity.SelectedValue);
                //cmd1.Parameters.AddWithValue("@Is_license_Available",ddlIslicenseAvailable.SelectedValue);
                //cmd1.Parameters.AddWithValue("@Is_Capture_By_Bank", ddlIsCaptureByBank.SelectedValue);
                //cmd1.Parameters.AddWithValue("@Is_Fullfill_Point_Three_Instruction",ddlIsFullfillPointThreeInstruction.SelectedValue);
                //cmd1.Parameters.AddWithValue("@fssai_lic_no", txtfssailicno.Text);
                //cmd1.Parameters.AddWithValue("@fssai_lic_issue_date",txtfssailicissuedate.Text);
                //cmd1.Parameters.AddWithValue("@fssai_lic_Exp_date ",txtfssailicExpdate.Text);
                //cmd1.Parameters.AddWithValue("@Non_Wdra_lic_no ",txtNonWdralicno.Text);
                //cmd1.Parameters.AddWithValue("@Non_Wdra_lic_issue_date",txtNonWdralicissuedate.Text);
                //cmd1.Parameters.AddWithValue("@Non_Wdra_lic_Exp_date",txtNonWdralicExpdate.Text);
                //    //cmd.Parameters.AddWithValue("@created_by",Request.UserHostAddress);


                //////////
                ///savan men code comment
                SqlCommand cmd1 = new SqlCommand(qry, con);
                int a = cmd1.ExecuteNonQuery();
                if (a == 1)
                {
                    //Get_Grid_Value();
                    //ModalPopupExtender1.Show();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save'); </script> ");
                    //lblchklicnovalid.Text = "";
                    txtWareName.Text = ""; txtInspDate.Text = ""; txtvacantcpt.Text = "";
                    ddlWarName_SelectedIndexChanged(null, null);
                    //lblmsg.visible = "true";

                }
            }
            else if (chkgdwn == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Already Inpection Check Inpection Report'); </script> ");
            }
        }
    }
    //protected void ddllicchk_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddllicchk.SelectedItem.Text == "Yes")
    //    {
    //        //trlicapplied1.Visible = false;
    //        //trlicapplied2.Visible = false;
    //        trlic1.Visible = true;
    //        trlic2.Visible = true;
    //        chkscheme();
    //    }
    //    else if (ddllicchk.SelectedItem.Text == "No")
    //    {
    //        trlic1.Visible = false;
    //        trlic2.Visible = false;
    //       // trlicapplied1.Visible = true;
    //        //trlicapplied2.Visible = true;
    //        chkscheme();
    //    }
    //}
    public int ChkInspectionGdwn()
    {
        int chk = 0;
        string strsql = "";
        //strsql = "select GodownId from tbl_Godown_Inspection_2024 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and Godown_Offer_Id='" + lblGodownOfferID.Text + "'";
        strsql = "select GodownId from tbl_Godown_Inspection_2024_Kharif where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "' and Godown_Offer_Id='" + lblGodownOfferID.Text + "'";
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
       // string QueryMax = "select count(Inspection_Id) as InsPecID from tbl_Godown_Inspection_2023 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        string QueryMax = "select count(Inspection_Id) as InsPecID from tbl_Godown_Inspection_2024_Kharif where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "" || str3 != "0")
        {
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "20" + Convert.ToString(Convert.ToInt32(str3) + 1);
            // MaxInsID = ddlWarName.SelectedValue.ToString() + "21" + Convert.ToString(Convert.ToInt32(str3) + 1);
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "22" + Convert.ToString(Convert.ToInt32(str3) + 1);
            MaxInsID = ddlWarName.SelectedValue.ToString() + "24" + Convert.ToString(Convert.ToInt32(str3) + 1);

        }
        else
        {
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "201";
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "211";
            //MaxInsID = ddlWarName.SelectedValue.ToString() + "221";
            MaxInsID = ddlWarName.SelectedValue.ToString() + "241";

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
        //if (ddlgdwntype.SelectedItem.Text == "WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        //{
        //    if (txtlicno.Text != "")
        //    {
        //        string strsql = "";
        //        strsql = "select WHCode,Validupto from [tbl_WDRA_Registration] where  WHCode='" + txtlicno.Text + "'";
        //        SqlCommand cmd = new SqlCommand(strsql, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
        //            lblchklicnovalid.Text = "Yes";
        //        }
        //        else
        //        {
        //            lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
        //            lblchklicnovalid.Text = "No";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस WDRA लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //        }
        //    }
        //}
        //else if (ddlgdwntype.SelectedItem.Text == "NON WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        //{
        //    if (txtlicno.Text != "")
        //    {
        //        string strsql = "";
        //        strsql = "select Whr_ID,IssuedAnugyptivalidityDate from [tbl_LiencesRegistrationNew] where Whr_ID='" + txtlicno.Text + "'";
        //        SqlCommand cmd = new SqlCommand(strsql, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
        //            lblchklicnovalid.Text = "Yes";
        //        }
        //        else if (ds.Tables[0].Rows.Count == 0)
        //        {
        //            strsql = "select * from tbl_LiencesRegistrationOld where Whr_ID='" + txtlicno.Text + "' ";
        //            SqlCommand cmd1 = new SqlCommand(strsql, con);
        //            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //            DataSet ds1 = new DataSet();
        //            da1.Fill(ds1);
        //            if (ds1.Tables[0].Rows.Count > 0)
        //            {
        //                lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
        //                lblchklicnovalid.Text = "Yes";
        //            }
        //            else if (ds1.Tables[0].Rows.Count == 0)
        //            {
        //                strsql = "select * from tbl_LicencesRegistration_2020 where Whr_ID='" + txtlicno.Text + "' ";
        //                SqlCommand cmd2 = new SqlCommand(strsql, con);
        //                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //                DataSet ds2 = new DataSet();
        //                da2.Fill(ds2);
        //                if (ds2.Tables[0].Rows.Count > 0)
        //                {
        //                    lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
        //                    lblchklicnovalid.Text = "Yes";
        //                }
        //                else
        //                {
        //                    lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
        //                    lblchklicnovalid.Text = "No";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //                }
        //            }
        //            else
        //            {
        //                lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
        //                lblchklicnovalid.Text = "No";
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //            }
        //        }
        //        else
        //        {
        //            lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
        //            lblchklicnovalid.Text = "No";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //        }
        //    }
        //}
        //else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "0")
        //{
        //    if (txtlicappliedno.Text != "")
        //    {
        //        string strsql = "";
        //        strsql = "select WHCode,Validupto from [tbl_WDRA_Registration] where  WHCode='" + txtlicappliedno.Text + "'";
        //        SqlCommand cmd = new SqlCommand(strsql, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
        //            lblchklicnovalid.Text = "Yes";
        //        }
        //        else
        //        {
        //            lbllicmessage.Text = "आवेदन नंबर वेध्य नहीं हे";
        //            lblchklicnovalid.Text = "No";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस WDRA पंजीयन/आवेदन नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //        }
        //    }
        //}
        //else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "00")
        //{
        //    if (txtlicappliedno.Text != "")
        //    {
        //        string strsql = "";
        //        strsql = "select Whr_ID,IssuedAnugyptivalidityDate from [tbl_LiencesRegistrationNew] where Whr_ID='" + txtlicappliedno.Text + "'";
        //        SqlCommand cmd = new SqlCommand(strsql, con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
        //            lblchklicnovalid.Text = "Yes";
        //        }
        //        else if (ds.Tables[0].Rows.Count == 0)
        //        {
        //            strsql = "select * from tbl_LiencesRegistrationOld where Whr_ID='" + txtlicappliedno.Text + "' ";
        //            SqlCommand cmd1 = new SqlCommand(strsql, con);
        //            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //            DataSet ds1 = new DataSet();
        //            da1.Fill(ds1);
        //            if (ds1.Tables[0].Rows.Count > 0)
        //            {
        //                lbllicmessage.Text = "आवेदन नंबर वेध्य हे";
        //                lblchklicnovalid.Text = "Yes";
        //            }
        //            else if (ds1.Tables[0].Rows.Count == 0)
        //            {
        //                strsql = "select * from tbl_LicencesRegistration_2020 where APPL_CODE='" + txtlicappliedno.Text + "' ";
        //                SqlCommand cmd2 = new SqlCommand(strsql, con);
        //                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        //                DataSet ds2 = new DataSet();
        //                da2.Fill(ds2);
        //                if (ds2.Tables[0].Rows.Count > 0)
        //                {
        //                    lbllicmessage.Text = "लायसेंस नंबर वेध्य हे";
        //                    lblchklicnovalid.Text = "Yes";
        //                }
        //                else
        //                {
        //                    lbllicmessage.Text = "लायसेंस नंबर वेध्य नहीं हे";
        //                    lblchklicnovalid.Text = "No";
        //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lbllicmessage.Text = "आवेदन नंबर वेध्य नहीं हे";
        //            lblchklicnovalid.Text = "No";
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस पंजीयन/आवेदन नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //        }
        //    }
        //}
        //else
        //{
        //    lblchklicnovalid.Text = "No";
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वेअरहाउस पंजीयन/आवेदन/लायसेंस नंबर वेध्य नहीं हे | निरीक्षण अपडेट किया जाना संभव नहीं हे कृप्या लायसेंस नंबर जाँचे '); </script> ");
        //}

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Branch_FillInspectionNew.aspx");
        //Response.Redirect("Branch_FillInspection_JVS22_23.aspx");
        Response.Redirect("Branch_FillInspection_Kharif_JVS24_25.aspx");
    }

    private void fillPaddygridedata()
    {
        string qry = "";
        string DistrictName = "";
        qry = "select District_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT where District_Id='" + Session["DistID"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(qry, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DistrictName = dt1.Rows[0]["District_Name"].ToString();
        }

        // qry = "select (select District_Name from tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=WLICNEW.District_ID ) as District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationNew as WLICNEW where District_ID='" + Session["DistID"].ToString() + "' union select District,Whr_Name,Whr_ID,Name_of_Owner,Whr_Address,Total_capicity,CONVERT(varchar(10),IssuedAnugyptivalidityDate,103) as ExpDate from tbl_LiencesRegistrationOld as LICOLD where LICOLD.District='" + DistrictName + "' order by District,Whr_Name";

        qry = "select (select District_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=tbl_LiencesRegistrationNew.District_ID ) as District, UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',IssuedAnugytikramank as 'Whr_ID',convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address,Total_capicity from tbl_LiencesRegistrationNew where IssuedAnugytikramank in (select Whr_ID from tbl_LiencesRegistrationOld ) and IssuedAnugytikramank is not null and District_ID='" + Session["DistID"].ToString() + "' union select (select District_Name from Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT as MDDIS where MDDIS.District_Id=tbl_LiencesRegistrationNew.District_ID ) as District, UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',Whr_ID,convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address ,Total_capicity from tbl_LiencesRegistrationNew where IssuedAnugytikramank is null and District_ID='" + Session["DistID"].ToString() + "' ";
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

        //if (ddlgdwntype.SelectedItem.Text == "NON WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        //{
        //    fillPaddygridedata();
        //    ModalPopupExtender2.Show();
        //}
        //else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "00")
        //{
        //    fillPaddygridedata();
        //    ModalPopupExtender2.Show();
        //}
        //else if (ddlgdwntype.SelectedItem.Text == "WDRA" && ddllicchk.SelectedItem.Text == "Yes")
        //{
        //    fillWDRALicDATA();
        //    ModalPopupExtender3.Show();
        //}
        //else if (ddllicchk.SelectedItem.Text == "No" && ddlappliedtype.SelectedValue == "0")
        //{
        //    fillWDRALicDATA();
        //    ModalPopupExtender3.Show();
        //}

    }

    protected void ddlinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlinsurance.SelectedValue == "NO")
        {
            txtSLDate.Enabled = false;
            TextBox1.Enabled = false;
            txtInsuranceCapacity.Enabled = false;
            txtvaluestk.Enabled = false;
            txtPremium.Enabled = false;

        }
        else
        {
            txtSLDate.Enabled = true;
            TextBox1.Enabled = true;
            txtInsuranceCapacity.Enabled = true;
            txtvaluestk.Enabled = true;
            txtPremium.Enabled = true;

        }
    }
    public void GetFacilities()
    {
        try
        {
            //DistID = Session["UserId"].ToString();
            //   string qry = "";
            qry = "select SN,Facilities,Till_2K_MT,Till_5K_MT,Till_10K_MT,Till_20K_MT,'' as Is_Available from tbl_Facilities_Master";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                AgreeGrid.DataSource = ds.Tables[0];
                AgreeGrid.DataBind();
            }
            else
            {
                AgreeGrid.DataSource = "";
                AgreeGrid.DataBind();
            }
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    AgreeGrid.DataSource = ds;
            //    AgreeGrid.DataBind();

            //    DataTable dt = ds.Tables[0];
            //    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("G_OfferCapacity"));
            //    AgreeGrid.FooterRow.Cells[1].Text = "Total";
            //    AgreeGrid.FooterRow.Cells[6].Text = total.ToString("N2");

            //    decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Insp_Capacity"));
            //    AgreeGrid.FooterRow.Cells[7].Text = total1.ToString("N2");

            //    decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Agree_Capacity"));
            //    AgreeGrid.FooterRow.Cells[8].Text = total2.ToString("N2");

            //}
            //else
            //{
            //    AgreeGrid.DataSource = null;
            //    AgreeGrid.DataBind();
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            //}
        }
        catch (Exception ex)
        {

        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell1 = new TableHeaderCell();

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "";
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "";
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 4;
        //cell1.Text = "गोदाम की भंडारण क्षमता अनुसार उपलब्ध करायी जाने वाली सामग्री/ संसाधन की मात्रा";
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "";
        //row1.Controls.Add(cell1);

        //row1.BackColor = ColorTranslator.FromHtml("#719CB6");
        //AgreeGrid.HeaderRow.Parent.Controls.AddAt(0, row1);


    }
    public void Get_Grid_Value()
    {
        string InspectionID = "";
        int Dunnage_Polythene = 1;
        int Electronic_Weighbrige = 1;
        int Wooden_Planke = 1;
        int Analysis_Kite = 1;
        int Fumigation_Cover = 1;
        int Fire_Extinguisher = 1;
        int Fire_Buckets = 1;
        int Safety_Guard = 1;
        int Spreay_Pump = 1;
        int Moisture_Meter = 1;
        int Cleaning_Labour = 1;
        int Elect_WaterWashroom = 1;
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        string Created_By = ipAddress;
        int Furniture_Office = 1;
        int DataEntry_Operator = 1;
        int Technical_Manpower = 1;
        int CCTV_Camera = 1;
        int IsReady_PCE = 1;
        int Facility_PC = 1;
        int[] YN_Arr_Value = new int[12];
        try
        {
            int i = 0;
            foreach (GridViewRow row in AgreeGrid.Rows)
            {
                string Y_N = "";

                if (row.RowType == DataControlRowType.DataRow)
                {
                    //TextBox ddlgrade = AgreeGrid.Rows.i
                    DropDownList ddlgrade = row.FindControl("ddlFacilities") as DropDownList;
                    Y_N = ddlgrade.SelectedValue;
                    YN_Arr_Value[i] = Convert.ToInt32(Y_N);
                    i++;
                }
            }
            Dunnage_Polythene = Convert.ToInt32(YN_Arr_Value[0]);
            Electronic_Weighbrige = Convert.ToInt32(YN_Arr_Value[1]);
            Wooden_Planke = Convert.ToInt32(YN_Arr_Value[2]);
            Analysis_Kite = Convert.ToInt32(YN_Arr_Value[3]);
            Fumigation_Cover = Convert.ToInt32(YN_Arr_Value[4]);
            Fire_Extinguisher = Convert.ToInt32(YN_Arr_Value[5]);
            Fire_Buckets = Convert.ToInt32(YN_Arr_Value[6]);
            Safety_Guard = Convert.ToInt32(YN_Arr_Value[7]);
            Spreay_Pump = Convert.ToInt32(YN_Arr_Value[8]);
            Moisture_Meter = Convert.ToInt32(YN_Arr_Value[9]);
            Cleaning_Labour = Convert.ToInt32(YN_Arr_Value[10]);
            Elect_WaterWashroom = Convert.ToInt32(YN_Arr_Value[11]);

            Furniture_Office = Convert.ToInt32(ddlFurniture_Office.SelectedValue);
            DataEntry_Operator = Convert.ToInt32(ddlDataEntry_Operator.SelectedValue);
            Technical_Manpower = Convert.ToInt32(ddlTechnical_Manpower.SelectedValue);
            CCTV_Camera = Convert.ToInt32(ddlCCTV_Camera.SelectedValue);
            IsReady_PCE = Convert.ToInt32(ddlProcCenterEsta.SelectedValue);
            Facility_PC = Convert.ToInt32(ddlProcCenterFacility.SelectedValue);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_Godown_Facilities", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@InspectionID", Srch_inspectionID);
            cmd.Parameters.AddWithValue("@Dunnage_Polythene", Dunnage_Polythene);
            cmd.Parameters.AddWithValue("@Electronic_Weighbrige", Electronic_Weighbrige);
            cmd.Parameters.AddWithValue("@Wooden_Planke", Wooden_Planke);
            cmd.Parameters.AddWithValue("@Analysis_Kite", Analysis_Kite);
            cmd.Parameters.AddWithValue("@Fumigation_Cover", Fumigation_Cover);
            cmd.Parameters.AddWithValue("@Fire_Extinguisher", Fire_Extinguisher);
            cmd.Parameters.AddWithValue("@Fire_Buckets", Fire_Buckets);
            cmd.Parameters.AddWithValue("@Safety_Guard", Safety_Guard);
            cmd.Parameters.AddWithValue("@Spreay_Pump", Spreay_Pump);
            cmd.Parameters.AddWithValue("@Moisture_Meter", Moisture_Meter);
            cmd.Parameters.AddWithValue("@Cleaning_Labour", Cleaning_Labour);
            cmd.Parameters.AddWithValue("@Elect_WaterWashroom", Elect_WaterWashroom);
            cmd.Parameters.AddWithValue("@Created_By", ipAddress);

            cmd.Parameters.AddWithValue("@Furniture_Office", Furniture_Office);
            cmd.Parameters.AddWithValue("@DataEntry_Operator", DataEntry_Operator);
            cmd.Parameters.AddWithValue("@Technical_Manpower", Technical_Manpower);
            cmd.Parameters.AddWithValue("@CCTV_Camera", CCTV_Camera);
            cmd.Parameters.AddWithValue("@Ready_PCE", IsReady_PCE);
            cmd.Parameters.AddWithValue("@Facility_PC", Facility_PC);
            ////////////////////
            // cmd.Parameters.AddWithValue("@Dunnage_Polythene", 1);
            //cmd.Parameters.AddWithValue("@Electronic_Weighbrige", 1);
            //cmd.Parameters.AddWithValue("@Wooden_Planke", 1);
            //cmd.Parameters.AddWithValue("@Analysis_Kite", 1);
            //cmd.Parameters.AddWithValue("@Fumigation_Cover", 1);
            //cmd.Parameters.AddWithValue("@Fire_Extinguisher", 1);
            //cmd.Parameters.AddWithValue("@Fire_Buckets", 1);
            //cmd.Parameters.AddWithValue("@Safety_Guard", 1);
            //cmd.Parameters.AddWithValue("@Spreay_Pump", 1);
            //cmd.Parameters.AddWithValue("@Moisture_Meter", 1);
            //cmd.Parameters.AddWithValue("@Cleaning_Labour", 1);
            //cmd.Parameters.AddWithValue("@Elect_WaterWashroom", 1);
            //cmd.Parameters.AddWithValue("@Created_By", ipAddress);

            //cmd.Parameters.AddWithValue("@Furniture_Office", 1);
            //cmd.Parameters.AddWithValue("@DataEntry_Operator", 1);
            //cmd.Parameters.AddWithValue("@Technical_Manpower", 1);
            //cmd.Parameters.AddWithValue("@CCTV_Camera", 1);
            //cmd.Parameters.AddWithValue("@Remark", GtxtRemark.Text);
            //cmd.Parameters.AddWithValue("@Createt_By", ipAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {

            }
        }
        catch (Exception ex)
        {

        }

    }


    protected void ddlIslicenseAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }

    protected void ddlIsCaptureByBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }

    protected void ddlIsFullfillPointThreeInstruction_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkfitunfit();
    }

    //protected void ddlIsBlockLastYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    chkfitunfit();
    //}

    protected void ddlInternet_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkscheme();
    }

    protected void ddlWdragdwntype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlWdragdwntype.SelectedValue == "--Select--")
        {
            show1.Visible = false;
        }
        else if (ddlWdragdwntype.SelectedValue == "1")
        {
            show1.Visible = true;
        }
        else if (ddlWdragdwntype.SelectedValue == "0")
        {
            show1.Visible = false;
        }
    }

    protected void ddlfssaigdwntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlfssaigdwntype.SelectedValue == "--Select--")
        {
            Tbody2.Visible = false;
        }
        else if (ddlfssaigdwntype.SelectedValue == "1")
        {
            Tbody2.Visible = true;
        }
        else if (ddlfssaigdwntype.SelectedValue == "0")
        {
            Tbody2.Visible = false;
        }
    }

    protected void ddlNonWdragdwntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNonWdragdwntype.SelectedValue == "--Select--")
        {
            NonWdra.Visible = false;
        }
        else if (ddlNonWdragdwntype.SelectedValue == "1")
        {
            NonWdra.Visible = true;
        }
        else if (ddlNonWdragdwntype.SelectedValue == "0")
        {
            NonWdra.Visible = false;
        }
    }

    protected void ddlWMSGodownID_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CheckAllreadyExist();
    }
}