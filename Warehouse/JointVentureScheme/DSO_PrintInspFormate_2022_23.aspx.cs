using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
public partial class JointVentureScheme_DSO_PrintInspFormate_2022_23 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        GetFacilities();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                GetFacilities();
                Get_Branch();

            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    public void gerreg(string qry)
    {

        if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
        {
            qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2022 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + ddlBranch.SelectedValue.ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
        }


        //else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
        //{
        //    qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2021 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
        //}
        else if (ddl_session.SelectedValue.ToString() == "JVS2020_21")
        {
            qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2020 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + ddlBranch.SelectedValue.ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
        }
        //else
        //{
        //    qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
        //}

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
    public void GetBMName()
    {
        qry = "select NodalOfficeName from tbl_metadata_depot where BranchId='" + Session["UserId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, sqlcon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblbmname.Text = dt.Rows[0]["NodalOfficeName"].ToString();
        }

    }
    public void GetRegisterationData()
    {
        qry = "select WR.Registration_Id,(select Regionnm from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as RegionName ,(select District_Name from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as DistirctName ,(select DepotName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as BranchName ,(select NodalOfficeName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as NodalOfficeName ,(select Tehsil_Name from Tehsils where TehsilCode=wr.TehsilID) as Tehsil_Name ,(select distinct Block_Name from tbl_Branch_Block_Mapping where Block_ID=wr.W_Block) as Block_Name ,Warehouse_Name,Warehouse_Address,WR.Mobile_No as OfficeNo ,Incharge_Name,wr.Incharge_MobileNo as InchMob,DistFNBranch ,Bank_Name,IFSC_Code,Account_No,PAN_No,Aadhar_No,WPR.Auth_Person,WPR.EmailID,WPR.MobileNo from tbl_WarehouseRegistration AS wr inner join  tbl_Warehouse_PreReg as WPR on WPR.Reg_No=wr.Registration_Id where wr.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtRegNo.Value = dt.Rows[0]["Registration_Id"].ToString();
            lblRegion.Text = dt.Rows[0]["RegionName"].ToString();
            lbldistname.Text = dt.Rows[0]["DistirctName"].ToString();
            lblbranch.Text = dt.Rows[0]["BranchName"].ToString();
            lblWarehouseName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblWareAddress.Text = dt.Rows[0]["Warehouse_Address"].ToString();
            lblTehsil.Text = dt.Rows[0]["Tehsil_Name"].ToString();
            lblDistrict2.Text = dt.Rows[0]["DistirctName"].ToString();
            lblOfficeMob.Text = dt.Rows[0]["OfficeNo"].ToString();
            lblInchMob.Text = dt.Rows[0]["InchMob"].ToString();
            lblnearbranch.Text = dt.Rows[0]["BranchName"].ToString();
            lblnearbranchDist.Text = dt.Rows[0]["DistFNBranch"].ToString();
            lblmob.Text = dt.Rows[0]["MobileNo"].ToString();
            lblemail.Text = dt.Rows[0]["EmailID"].ToString();
            lblpan.Text = dt.Rows[0]["PAN_No"].ToString();
            lblaadhar.Text = dt.Rows[0]["Aadhar_No"].ToString();
            lblAuthPerson.Text = dt.Rows[0]["Auth_Person"].ToString();
            // lblbmname.Text = dt.Rows[0]["NodalOfficeName"].ToString();
            lblblocktxt.Text = dt.Rows[0]["Block_Name"].ToString();
            PrintDiv.Visible = true;
        }
    }

    public void GetGodwnOfferData()
    {
        if (ddl_session.SelectedValue.ToString() == "Rabi1920")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer_2019] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='R2019' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/17/2019' )";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif1920")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer_2019] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='R2019' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'11/28/2019')";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2020_21")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer_2020] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2020_21' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/20/2020')";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer_2021] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2021_22' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/19/2021')";
        }

        else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
        {
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer_2022] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2022_23' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'03/08/2022')";
        }


        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblGdwnNo.Text = dt.Rows[0]["Godown_No"].ToString();
            lblOferscheme.Text = dt.Rows[0]["G_Scheme"].ToString();
            lbloffercpt.Text = dt.Rows[0]["G_OfferCapacity"].ToString();
            lblofferdt.Text = dt.Rows[0]["OfferData"].ToString();
            lbloffertype.Text = dt.Rows[0]["Capacity_Type"].ToString();
            lbltime.Text = dt.Rows[0]["Offertime"].ToString();
            lblgdwnofrid.Text = dt.Rows[0]["Godown_Offer_Id"].ToString();
            if (lblOferscheme.Text == "85")
            {
                lblOferscheme.Text = "A(Rs. 85)";
            }
            else if (lblOferscheme.Text == "80")
            {
                lblOferscheme.Text = "B(Rs. 80)";
            }

            else if (lblOferscheme.Text == "60")
            {
                lblOferscheme.Text = "A-ब(Rs. 60)";
            }

            else if (lblOferscheme.Text == "55")
            {
                lblOferscheme.Text = "B-ब(Rs. 55)";
            }
        }
    }

    public void GetRegGodwnData()
    {
        qry = "select CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_Width)as G_Width,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,LicType,LicNo,convert(varchar(10),LicIssueDate,103) as LicIssueDate,convert(varchar(10),LicValidityDate,103) LicValidityDate from tbl_WarehouseGodown_Reg where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblLength.Text = dt.Rows[0]["G_Length"].ToString();
            lblWeidth.Text = dt.Rows[0]["G_Width"].ToString();
            lblHeight.Text = dt.Rows[0]["G_Height"].ToString();
            lblTotalCpt.Text = dt.Rows[0]["G_ScientificCapacity"].ToString();
            lbllictype.Text = dt.Rows[0]["LicType"].ToString().Trim();
            if (lbllictype.Text == "63")
            {
                lbllictype.Text = "NON WDRA";
                lblstateLic.Text = dt.Rows[0]["LicNo"].ToString();
                lblissuedate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                lblStateLicExpDate.Text = dt.Rows[0]["LicValidityDate"].ToString();
                lblapplied.Text = "____________";
                lblapplieddate.Text = "_______";
                lblaplliedtxttype.Text = "_______";
            }
            else if (lbllictype.Text == "68")
            {
                lbllictype.Text = "WDRA";
                lblstateLic.Text = dt.Rows[0]["LicNo"].ToString();
                lblissuedate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                lblStateLicExpDate.Text = dt.Rows[0]["LicValidityDate"].ToString();
                lblapplied.Text = "____________";
                lblapplieddate.Text = "_______";
                lblaplliedtxttype.Text = "_______";
            }

            if (lbllictype.Text == "0")
            {
                lblaplliedtxttype.Text = "APPLIED For WDRA";
                lblstateLic.Text = "________";
                lblissuedate.Text = "_____";
                lblStateLicExpDate.Text = "_____";
                lblapplied.Text = dt.Rows[0]["LicNo"].ToString();
                lblapplieddate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                lbllictype.Text = "_______";

            }
            else if (lbllictype.Text == "00")
            {
                lblaplliedtxttype.Text = "APPLIED For NON WDRA";
                lblstateLic.Text = "______";
                lblissuedate.Text = "_____";
                lblStateLicExpDate.Text = "_____";
                lblapplied.Text = dt.Rows[0]["LicNo"].ToString();
                lblapplieddate.Text = dt.Rows[0]["LicIssueDate"].ToString();
                lbllictype.Text = "_______";
            }
        }
    }
    public void GetWareAdditionaldata()
    {
        qry = "select Latitude,Longitude from tbl_WarehouseAdditionalinfo where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            // Cross Enter Data Due To Label ID
            lbllongitude.Text = dt.Rows[0]["Latitude"].ToString();
            lbllatitude.Text = dt.Rows[0]["Longitude"].ToString();
        }
    }

    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarName.SelectedItem.Text != "--Select--" && ddl_session.SelectedItem.Text != "--Select--")
        {
            Branch = Session["UserId"].ToString();
            string qry = "";

            if (ddl_session.SelectedValue.ToString() == "Rabi1920")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2019 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='R2019' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/17/2019' )";
            }
            else if (ddl_session.SelectedValue.ToString() == "Kharif1920")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2019 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='K2019' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'11/28/2019' )";
            }
            else if (ddl_session.SelectedValue.ToString() == "JVS2020_21")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2020 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2020_21' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/20/2020' )";
            }
            else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2021 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2021_22' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'02/19/2021' )";
            }

            else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
            {
                qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer_2022 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and OfferSeason='JVS2022_23' and Godown_Offer_Id not in (select PrintInsp.Godown_Offer_Id from tbl_PrePrinted_InspForm_Detail as PrintInsp where PrintInsp.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "' and CreatedDate >'03/08/2022' and JVS_Session='Kharif_2022_23' )";
            }
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
                ddlgodown.DataSource = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Name First ')", true);
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text != "--Select--")
        {
            GetRegisterationData();
            GetGodwnOfferData();
            GetRegGodwnData();
            GetWareAdditionaldata();
            GetBMName();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown First ')", true);
            PrintDiv.Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (Session["UserId"].ToString() != "")
        {
            if (txtRegNo.Value != "" && ddlgodown.SelectedItem.Text != "--Select--" && ddlWarName.SelectedItem.Text != "--Select--")
            {
                //qry = "INSERT INTO [tbl_PrePrinted_InspForm_Detail] ([BranchId],[Registration_Id],[Godown_ID],[CreatedBy],[CreatedDate],[Godown_Offer_Id]) VALUES ( '" + Session["UserId"].ToString() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString().Trim() + "','" + ip + "',GETDATE(),'" + lblgdwnofrid.Text + "' )";
                qry = "INSERT INTO [tbl_PrePrinted_InspForm_Detail] ([BranchId],[Registration_Id],[Godown_ID],[CreatedBy],[CreatedDate],[Godown_Offer_Id],JVS_Session,User_Type) VALUES ( '" + Session["UserId"].ToString() + "','" + ddlWarName.SelectedValue.ToString().Trim() + "','" + ddlgodown.SelectedValue.ToString().Trim() + "','" + ip + "',GETDATE(),'" + lblgdwnofrid.Text + "','Kharif_2022_23','DSO' )";
                SqlCommand cmd = new SqlCommand(qry, con);
                int a = cmd.ExecuteNonQuery();
                if (a > 0)
                {
                    //  GetOfferGodown();
                    ddlWarName_SelectedIndexChanged(null, null);
                    PrintDiv.Visible = false;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown First ')", true);
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Logins.aspx");
    }
    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        Branch = Session["UserId"].ToString();
        string qry = "";
        if (ddl_session.SelectedValue.ToString() == "Rabi1920")
        {
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/17/2019',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2019 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='R2019' and  WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)";

            //  qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/09/2019',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2019 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='R2019' and  WR.BranchId='" + Branch + "' ) as FinalOfferList ";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif1920")
        {
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'11/28/2019',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2019 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='K2019' and  WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2020_21")
        {
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/20/2020',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2020 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='JVS2020_21' and  WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
        {
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2021',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2021 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='JVS2021_22' and  WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)";
        }

        else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
        {
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'03/08/2022',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2022 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='JVS2022_23' and  WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)";
        }
        gerreg(qry);
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell1 = new TableHeaderCell();

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 4;
        cell1.Text = "गोदाम की भंडारण क्षमता अनुसार उपलब्ध करायी जाने वाली सामग्री/ संसाधन की मात्रा";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        row1.BackColor = ColorTranslator.FromHtml("#719CB6");
        AgreeGrid.HeaderRow.Parent.Controls.AddAt(0, row1);


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
    protected void Get_Branch()
    {
        string SessBranchID = Session["UserId"].ToString();
        qry = "SELECT BranchID,BranchName FROM MetaDataBranchWithIssueCenter where DistrictId='"+ SessBranchID + "'";
            
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
            }
            else
            {
            ddlBranch.DataSource = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
    }
}