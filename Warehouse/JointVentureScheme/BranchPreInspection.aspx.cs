using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_BranchPreInspection : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }

    }
    public void gerreg(string qry)
    {
        //qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
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
        if (ddlWarName.SelectedItem.Text != "--Select--" && ddl_session.SelectedItem.Text !="--Select--")
        {
            Branch = Session["UserId"].ToString();
            string qry = "";

            if (ddl_session.SelectedValue.ToString() == "Rabi1819")
            {
                //  qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
                qry = "select distinct Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase<=9";
            }
            else if (ddl_session.SelectedValue.ToString() == "Kharif1819")
            {
                // qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
                qry = "select distinct Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase>=9";
            }
          //  qry = "select Godown_ID,Godown_No from tbl_Warehouse_Godown_Offer where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Name First ')", true);
        }
    }
    public void GetRegisterationData()
    {
        qry = "select WR.Registration_Id,(select Regionnm from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as RegionName ,(select District_Name from tbl_MetaData_DISTRICT where District_Id=WR.DistrictId) as DistirctName ,(select DepotName from tbl_MetaData_DEPOT where BranchId=WR.BranchId) as BranchName ,(select Tehsil_Name from Tehsils where TehsilCode=wr.TehsilID) as Tehsil_Name ,Warehouse_Name,Warehouse_Address,WR.Mobile_No as OfficeNo ,Incharge_Name,wr.Incharge_MobileNo as InchMob,DistFNBranch ,Bank_Name,IFSC_Code,Account_No,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRAExpDate,Warehouse_LicenseNo,convert(varchar(10),Warehouse_LicenseDate,103) as Warehouse_LicenseDate,PAN_No,Aadhar_No,WPR.Auth_Person,WPR.EmailID,WPR.MobileNo from tbl_WarehouseRegistration AS wr inner join  tbl_Warehouse_PreReg as WPR on WPR.Reg_No=wr.Registration_Id where wr.Registration_Id='" + ddlWarName.SelectedValue.ToString() + "'";
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
            lblAccNo.Text = dt.Rows[0]["Account_No"].ToString();
            lblIFSC.Text = dt.Rows[0]["IFSC_Code"].ToString();
            lblBankName.Text = dt.Rows[0]["Bank_Name"].ToString();
            lblWDRALicNo.Text = dt.Rows[0]["WDRA_LicenseNo"].ToString();
            lblWDRALicdate.Text = dt.Rows[0]["WDRAExpDate"].ToString();
            lblstateLic.Text = dt.Rows[0]["Warehouse_LicenseNo"].ToString();
            lblStateLicExpDate.Text = dt.Rows[0]["Warehouse_LicenseDate"].ToString();
            if (lblstateLic.Text == "")
            {
                lblStateLicExpDate.Text = "";
                lblstateLic.Text = "आफर मे दर्ज नहीं किया गया";
            }
            if (lblWDRALicNo.Text == "")
            {
                lblWDRALicdate.Text = "";
                lblWDRALicNo.Text = "आफर मे दर्ज नहीं किया गया";

            }
            if (lblBankName.Text == "--Select--" || lblBankName.Text == "Other")
            {
                lblBankName.Text = "_______________________";
            }
            PrintDiv.Visible = true;
        }
    }

    public void GetGodwnOfferData()
    {
        if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        {
            //  qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase<=9 ";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            // qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
            qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase>=9 ";
        }
       // qry = "SELECT [Offer_Id],[Godown_Offer_Id],[Godown_ID],[Godown_No],CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,G_Scheme,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type,convert(varchar(10),[Offer_Date],103) as OfferData,convert(varchar(10),[Offer_Date],108) as Offertime  FROM [tbl_Warehouse_Godown_Offer] where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Phase>=9 ";
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
            if (lblOferscheme.Text == "55")
            {
                lblOferscheme.Text = "Non WDRA";
            }
            else if (lblOferscheme.Text == "60")
            {
                lblOferscheme.Text = "WDRA";
            }
        }
    }

    public void GetRegGodwnData()
    {
        qry = " select CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_Width)as G_Width,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear from tbl_WarehouseGodown_Reg where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
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
            lblConYear.Text = dt.Rows[0]["G_ConstructedYear"].ToString();
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
        GetOfferGodown();
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text != "--Select--")
        {
            GetRegisterationData();
            GetGodwnOfferData();
            GetRegGodwnData();
            GetWareAdditionaldata();
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
                qry = "INSERT INTO [tbl_PrePrinted_InspForm_Detail] ([BranchId],[Registration_Id],[Godown_ID],[CreatedBy],[CreatedDate]) VALUES ( '" + Session["UserId"].ToString() + "','" + ddlgodown.SelectedValue + "','" + ddlgodown.SelectedValue + "','" + ip + "',GETDATE() )";
                SqlCommand cmd = new SqlCommand(qry, con);
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {

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

        if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        {
            //  qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and convert(varchar(10),TransactionDate,101) < convert(varchar(10),'10/19/2018',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.Phase < 9 and  WR.BranchId='" + Branch + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) ";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            // qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
            qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and convert(varchar(10),TransactionDate,101) >= convert(varchar(10),'10/19/2018',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.Phase  >= 9 and  WR.BranchId='" + Branch + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) ";
        }
        gerreg(qry);
    }
}
