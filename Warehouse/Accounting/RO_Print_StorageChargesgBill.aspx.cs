using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;

public partial class Accounting_RO_Print_StorageChargesgBill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    decimal TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                // string  A = Convert.ToString(Convert.ToDouble(Convert.ToDecimal( 68 / 31)));



                // using Decimal.Truncate(Decimal) Method 
                //  Decimal val1 = Decimal.Truncate(68/31),'4'; 
                //d = (Math.Round(3.4679, 4, MidpointRounding.AwayFromZero));
                //string s = d.ToString("N4");
                //Console.WriteLine(s);

                lblP_regionnm.Text = Session["UserName"].ToString();
                lbld_rmname.Text = Session["UserName"].ToString();
                lbleporm.Text = Session["UserName"].ToString(); ;

                // GetBillGdwn();
                GetBranch();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select District_ID from tbl_metadata_district where Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();
        lblbranch.Text = ddlBranch.SelectedItem.Text.Trim(); 
        Label12.Text = Session["UserName"].ToString();
        lblAcBranch.Text = ddlBranch.SelectedItem.Text.Trim();
        lblepobranch.Text = ddlBranch.SelectedItem.Text;

        string qry = "";
        qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgdwn.DataSource = ds.Tables[0];
            ddlgdwn.DataTextField = "Godown_name";
            ddlgdwn.DataValueField = "Godown_ID";
            ddlgdwn.DataBind();
            ddlgdwn.Items.Insert(0, "--Select--");
        }
    }
    public void GetBillData()
    {
        //  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        //qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD1.DataSource = ds;
            GD1.DataBind();

            DataTable dt = ds.Tables[0];
            GD1.FooterRow.Cells[1].Text = "महायोग :-";

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Charges"));
            GD1.FooterRow.Cells[7].Text = total1.ToString("N2");
        }
        else
        {
            GD1.DataSource = "";
            GD1.DataBind();
        }
    }

    public void GetBillOtherData()
    {
        qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblgdwnname.Text = dt.Rows[0]["Godown_Name"].ToString();
            lblcmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
            lblbillmonth.Text = dt.Rows[0]["Month"].ToString();
            lblbillno.Text = dt.Rows[0]["Bill_Number"].ToString();
            lblGdnum.Text = dt.Rows[0]["Godown_No"].ToString();
            //  lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();

            lbld_warehousename.Text = dt.Rows[0]["Godown_Name"].ToString();
            lbld_commodity.Text = dt.Rows[0]["Commodity_Name"].ToString();
            lbld_month.Text = dt.Rows[0]["Month"].ToString();
            lbld_billno.Text = dt.Rows[0]["Bill_Number"].ToString();
            lbld_gdno.Text = dt.Rows[0]["Godown_No"].ToString();
            //   lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();

            lblRentMonth.Text = dt.Rows[0]["Month"].ToString();
            lblRentCmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
            // txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblRentFromDate.Text = dt.Rows[0]["FromDate"].ToString();
            lblRentToDate.Text = dt.Rows[0]["ToDate"].ToString();
            lblRentF_Year.Text = dt.Rows[0]["Financial_Year"].ToString();
            lblRentCropYear.Text = dt.Rows[0]["Crop_Year"].ToString();
        }

        else
        {
            lblgdwnname.Text = "";
            lblcmd.Text = "";
            lblbillmonth.Text = "";
            lblbillno.Text = "";
            lblGdnum.Text = "";
            //  lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();

            lbld_warehousename.Text = "";
            lbld_commodity.Text = "";
            lbld_month.Text = "";
            lbld_billno.Text = "";
            lbld_gdno.Text = "";
            //   lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();

            lblRentMonth.Text = "";
            lblRentCmd.Text = "";
            // txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblRentFromDate.Text = "";
            lblRentToDate.Text = "";
            lblRentF_Year.Text = "";
            lblRentCropYear.Text = "";
        }
    }

    public void GetBillDetuctionData()
    {
        qry = "select VIV.Res_ID,(select MDR.Res_Det_Vivran from tbl_metadata_RecDetuction as MDR where MDR.Rec_ID=VIV.Res_ID) as Res_Det_Vivran ,Res_Det_Karan,Deduction_Amt,Res_Det_Remark from tbl_Godown_Rent_Deduction_Vivran as VIV where Bill_No='" + ddlbillno.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_Detuc.DataSource = dt;
            DataBind();

            GD_Detuc.FooterRow.Cells[0].Text = "महायोग :-";
            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Deduction_Amt"));
            GD_Detuc.FooterRow.Cells[3].Text = total1.ToString("N2");
        }
        else
        {
            GD_Detuc.DataSource = "";
            DataBind();
        }
    }

    protected void ddlgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetActualBilNo();
        GetFinacialYear();
    }

    public void GetActualBilNo()
    {
        string qry = "";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number like ('1%')";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and (Bill_Number like ('19%') or Bill_Number like ('20%'))";
        qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Type='AD' AND Financial_Year='"+ddlFY.SelectedValue+"' Order by Bill_Number";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            trdet.Visible = true;
            ddlactualbillno.DataSource = ds.Tables[0];
            ddlactualbillno.DataTextField = "Bill_Number";
            ddlactualbillno.DataValueField = "Bill_Number";
            ddlactualbillno.DataBind();
            ddlactualbillno.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlactualbillno.DataSource = "";
            ddlactualbillno.DataBind();
            ddlactualbillno_SelectedIndexChanged(null, null);
        }
    }

    protected void ddlbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdet.Visible = true;
        GetBillData();
        GetBillOtherData();
        GetBillDetuctionData();
        DSCSignJVS();
    }

    public void GetActual_BillData()
    {
        //  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_Institution_Daily_Charges as BDSC WHERE BDSC.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // tr_printdiv.Visible = true;
            GD2.DataSource = ds;
            GD2.DataBind();

            //DataTable dt = ds.Tables[0];
            //decimal total = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Closing_Weight"));
            //GD2.FooterRow.Cells[1].Text = "महायोग :-";
            //GD2.FooterRow.Cells[5].Text = total.ToString("N2");

            //decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Charges"));
            //GD2.FooterRow.Cells[7].Text = total1.ToString("N2");
        }

        else
        {
            GD2.DataSource = "";
            GD2.DataBind();
        }
    }
    protected void ddlactualbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdet.Visible = true;
        GetBillOtherDataActual();
        GetActual_BillData();
        getActualBillAmount();
        DSCSign();
        DSCSignRM();

        if (ddlactualbillno.DataSource != null || ddlactualbillno.DataSource != "")
        {
            string qry = "";
            //  qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Number not like ('1%') and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and  order by Bill_Number";
            qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where (Bill_Type='GR' or Bill_Type='HG' or Bill_Type='AD')  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbillno.SelectedValue + "')  order by Bill_Number";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                trdet.Visible = true;
                ddlbillno.DataSource = ds.Tables[0];
                ddlbillno.DataTextField = "Bill_Number";
                ddlbillno.DataValueField = "Bill_Number";
                ddlbillno.DataBind();
                ddlbillno.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
                ddlbillno_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlbillno.DataSource = "";
                ddlbillno.DataBind();
                ddlbillno_SelectedIndexChanged(null, null);
            }
        }
    }

    public void GetBillOtherDataActual()
    {
        // qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate from tbl_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=SUBSTRING(b.Bill_Number,0,11))  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";

        qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id  where b.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'  ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lbldatefromto.Text = dt.Rows[0]["Month"].ToString();
            lbldepositor.Text = "MPSCSC";
            lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
            lblcmd_ac.Text = dt.Rows[0]["Commodity_Name"].ToString();

            lblrmmonth.Text = dt.Rows[0]["Month"].ToString();
            lblrmcmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
            // txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblfrmdate.Text = dt.Rows[0]["FromDate"].ToString();
            lbltodate.Text = dt.Rows[0]["ToDate"].ToString();
            lblfinancial.Text = dt.Rows[0]["Financial_Year"].ToString();
            lblCPY.Text = dt.Rows[0]["Crop_Year"].ToString();

            lblAcGdwnName.Text = dt.Rows[0]["Godown_Name"].ToString();
            lblCropyear.Text = dt.Rows[0]["Crop_Year"].ToString();

            lblepobillno.Text = dt.Rows[0]["Bill_Number"].ToString();
            lblepocmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
            lblepocropyear.Text = dt.Rows[0]["Crop_Year"].ToString();
            lblepofinan.Text = dt.Rows[0]["Financial_Year"].ToString();
            lblepomonth.Text = dt.Rows[0]["Month"].ToString();
            lblepodepos.Text = "MPSCSC";
            lblepogdwn.Text = dt.Rows[0]["Godown_Name"].ToString();
        }

        else
        {
            lbldatefromto.Text = "";
            lbldepositor.Text = "";
            lblbillno_Actual.Text = "";
            lblcmd_ac.Text = "";

            lblrmmonth.Text = "";
            lblrmcmd.Text = "";
            // txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblfrmdate.Text = "";
            lbltodate.Text = "";
            lblfinancial.Text = "";
            lblCPY.Text = "";

            lblAcGdwnName.Text = "";
            lblCropyear.Text = "";

            lblepobillno.Text = "";
            lblepocmd.Text = "";
            lblepocropyear.Text = "";
            lblepofinan.Text = "";
            lblepomonth.Text = "";
            lblepodepos.Text = "";
            lblepogdwn.Text = "";

        }
    }

    public void DSCSign()
    {
        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details where Ref_Bill_No='" + ddlactualbillno.SelectedValue.ToString() + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image1.Visible = true;
            lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }
        else
        {
            Image1.Visible = false;
            lblBSerialNo.Text = "";
            lblBIP.Text = "";
            lblBHolderName.Text = "";
            lblBCreatedDate.Text = "";
        }

        //qry = "SELECT 'eSing'as UserName,'eSing@$data#$!Verification'as Pwd,d.Bill_Number,d.District_Id,d.Branch_Id,d.Depositor_Id,d.Commodity_Id,d.Financial_Year,d.Per_Month_Rate,d.Per_Day_Rate,d.Net_Amount,d.Sub_Amount,d.GST_Perc,d.GST_Amt,d.Created_Date_Bill,d.Created_By_Bill,d.Month_No,d.Godown_Id,d.Crop_Year, d.Account_No,d.IFSC_Code,d.WHR_Check_Sum,d.CreatedDate,STUFF(STUFF(CONVERT(CHAR(20), CreatedDate, 113),3,1, '-'),7,1,'-')CreatedDate1,d.CreatedBy,d.DSC_Serial_No,d.DSC_Holder_Name,d.Client_Ip,d.DSC_User_Type,d.csms_DSC_H_Name,d.csms_DSC_Serial_No,d.csms_DSC_User_Type ,d.csms_CreatedBy,d.csms_Check_Sum ,d.csms_CreatedDate,d.csms_Client_Ip,(select distinct c.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY c where c.Commodity_Id=d.Commodity_Id)CommodityName,(select distinct  i.BranchName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.BranchID=Branch_Id)BranchName,(select distinct  g.Godown_Name from  Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 g where g.Godown_ID=d.Godown_Id)GodownName,(select distinct  i.IssueCenterName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.IssueCenterId=d.csms_CreatedBy)IssueCenterName,STUFF(STUFF(CONVERT(CHAR(20), csms_CreatedDate, 113),3,1, '-'),7,1,'-')csms_CreatedDate1,(select REPLACE(CONVERT(VARCHAR(11),min(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "')FromDate,(select REPLACE(CONVERT(VARCHAR(11),Max(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "')ToDate from MPSCSC.dbo.Digitally_Sing_StorageBill d where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";
        qry = "SELECT 'eSing'as UserName,'eSing@$data#$!Verification'as Pwd,d.Bill_Number,d.District_Id,d.Branch_Id,d.Depositor_Id,d.Commodity_Id,d.Financial_Year,d.Per_Month_Rate,d.Per_Day_Rate,d.Net_Amount,d.Sub_Amount,d.GST_Perc,d.GST_Amt,d.Created_Date_Bill,d.Created_By_Bill,d.Month_No,d.Godown_Id,d.Crop_Year, d.Account_No,d.IFSC_Code,d.WHR_Check_Sum,d.CreatedDate,STUFF(STUFF(CONVERT(CHAR(20), CreatedDate, 113),3,1, '-'),7,1,'-')CreatedDate1,d.CreatedBy,d.DSC_Serial_No,d.DSC_Holder_Name,d.Client_Ip,d.DSC_User_Type,d.csms_DSC_H_Name,d.csms_DSC_Serial_No,d.csms_DSC_User_Type ,d.csms_CreatedBy,d.csms_Check_Sum ,d.csms_CreatedDate,d.csms_Client_Ip,(select distinct c.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY c where c.Commodity_Id=d.Commodity_Id)CommodityName,(select distinct  i.BranchName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.BranchID=Branch_Id)BranchName,(select distinct  g.Godown_Name from  Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 g where g.Godown_ID=d.Godown_Id)GodownName,(select distinct  i.IssueCenterName from Intergrated_MP_STORAGE.dbo.MetaDataBranchWithIssueCenter i where i.IssueCenterId=d.csms_CreatedBy)IssueCenterName,STUFF(STUFF(CONVERT(CHAR(20), csms_CreatedDate, 113),3,1, '-'),7,1,'-')csms_CreatedDate1,(select REPLACE(CONVERT(VARCHAR(11),min(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "')FromDate,(select REPLACE(CONVERT(VARCHAR(11),Max(csms_Dates),106), ' ','-') from MPSCSC.dbo.StorageBillVerification2019 s where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "')ToDate from MPSCSC.dbo.Digitally_Sign_StorageBill_IC d where d.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";

        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            Image2.Visible = true;
            lblICSerialNo.Text = "DSC Serial No : " + dt2.Rows[0]["csms_DSC_Serial_No"].ToString();
            lblICIp.Text = "Client IP : " + dt2.Rows[0]["csms_Client_Ip"].ToString();
            lblICHoldername.Text = "DSC Holder Name : " + dt2.Rows[0]["csms_DSC_H_Name"].ToString();
            lblICCreatedDate.Text = "DSC Sign Date : " + dt2.Rows[0]["csms_CreatedDate1"].ToString();
        }
        else
        {
            Image2.Visible = false;
            lblICSerialNo.Text = "";
            lblICIp.Text = "";
            lblICHoldername.Text = "";
            lblICCreatedDate.Text = "";
        }
    }

    public void DSCSignJVS()
    {
        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_PVT where Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  and DSC_User_Type='B'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image5B.Visible = true;
            lbldscTB.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblIpAddB.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblDSC_HolderB.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblSigningDateB.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();

            if (GD_Detuc.Rows.Count > 0)
            {
                Image4.Visible = true;
                lblDSNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
                lblDIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
                lblDHolderNm.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
                lblDSignDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
            }
            else
            {
                Image4.Visible = false;
                lblDSNo.Text = "";
                lblDIP.Text = "";
                lblDHolderNm.Text = "";
                lblDSignDate.Text = "";
            }
        }

        else
        {
            Image5B.Visible = false;
            lbldscTB.Text = "";
            lblIpAddB.Text = "";
            lblDSC_HolderB.Text = "";
            lblSigningDateB.Text = "";

            if (GD_Detuc.Rows.Count == 0)
            {
                Image4.Visible = false;
                lblDSNo.Text = "";
                lblDIP.Text = "";
                lblDHolderNm.Text = "";
                lblDSignDate.Text = "";
            }
        }

        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_PVT where Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  and DSC_User_Type='G'";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            Image5.Visible = true;
            lbldscT.Text = "DSC Serial No : " + dt2.Rows[0]["DSC_Serial_No"].ToString();
            lblIpAdd.Text = "Client IP : " + dt2.Rows[0]["Client_Ip"].ToString();
            lblDSC_Holder.Text = "DSC Holder Name : " + dt2.Rows[0]["DSC_Holder_Name"].ToString();
            lblSigningDate.Text = "DSC Sign Date : " + dt2.Rows[0]["CreatedDate"].ToString();
        }

        else
        {
            Image5.Visible = false;
            lbldscT.Text = "";
            lblIpAdd.Text = "";
            lblDSC_Holder.Text = "";
            lblSigningDate.Text = "";
        }
    }

    public void getActualBillAmount()
    {
        String qry = "select Ref_Bill_Number,JVS_Bill_Amount,TDS_Detuction_Amount,Total_Detuction_Amt,MPWLC_Amt,Net_Amount,JVS_Net_Amount from tbl_GdwnRentBill_Detuction_RM where Ref_Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            lblE_MPWLCHissa.Text = dt2.Rows[0]["MPWLC_Amt"].ToString();
            lblE_DeykRashi.Text = dt2.Rows[0]["JVS_Bill_Amount"].ToString();
            lblE_MPWLCTDS.Text = dt2.Rows[0]["TDS_Detuction_Amount"].ToString();
            lblE_MPWLCDetuc.Text = dt2.Rows[0]["Total_Detuction_Amt"].ToString();
            lblE_JVSDeyak.Text = dt2.Rows[0]["JVS_Net_Amount"].ToString();
            lblepoGrandTotal.Text = dt2.Rows[0]["Net_Amount"].ToString();

            lblE_JVSNetPay.Text = lblE_JVSDeyak.Text;
            lblE_MPWLCNetpay.Text = Convert.ToString((Convert.ToDecimal(lblE_MPWLCHissa.Text) + Convert.ToDecimal(lblE_MPWLCTDS.Text) + Convert.ToDecimal(lblE_MPWLCDetuc.Text)));
        }
        else
        {
            lblE_MPWLCHissa.Text = "0";
            lblE_DeykRashi.Text = "0";
            lblE_MPWLCTDS.Text = "0";
            lblE_MPWLCDetuc.Text = "0";
            lblE_JVSDeyak.Text = "0";
            lblepoGrandTotal.Text = "0";

            lblepobillno.Text = "";
            lblepocmd.Text = "";
            lblepocropyear.Text = "";
            lblepofinan.Text = "";
            lblepomonth.Text = "";
            lblepodepos.Text = "";
            lblepogdwn.Text = "";
        }
    }

    public void DSCSignRM()
    {
        qry = " select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_RO  where Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'  and DSC_User_Type='R'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Image7.Visible = true;
            lblrmdscserial.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
            lblrmdscip.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
            lblrmdscHN.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
            lblrmdscSD.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
        }

        else
        {
            Image7.Visible = false;
            lblrmdscserial.Text = "";
            lblrmdscip.Text = "";
            lblrmdscHN.Text = "";
            lblrmdscSD.Text = "";
        }

        qry = " select DSC_Serial_No_RAM,DSC_Holder_Name_RAM,Client_Ip_RAM,Convert(varchar(10),CreatedDate_RAM,103) as CreatedDate from tbl_Digitally_Signed_Bill_RO  where Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'  and DSC_User_Type_RAM='M'";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            Image3.Visible = true;
            lblRMMSei.Text = "DSC Serial No : " + dt2.Rows[0]["DSC_Serial_No_RAM"].ToString();
            lblRMMIP.Text = "Client IP : " + dt2.Rows[0]["Client_Ip_RAM"].ToString();
            lblRMMHN.Text = "DSC Holder Name : " + dt2.Rows[0]["DSC_Holder_Name_RAM"].ToString();
            lblRMMDate.Text = "DSC Sign Date : " + dt2.Rows[0]["CreatedDate"].ToString();
        }

        else
        {
            Image3.Visible = false;
            lblRMMSei.Text = "";
            lblRMMIP.Text = "";
            lblRMMHN.Text = "";
            lblRMMDate.Text = "";
        }
    }

    protected void GD2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblClosing_Weight = (Label)e.Row.FindControl("lblClosing_Weight");
            Label lblTotal_Charges = (Label)e.Row.FindControl("lblTotal_Charges");
            TT2 += Convert.ToDecimal(lblClosing_Weight.Text);
            TT3 += Convert.ToDecimal(lblTotal_Charges.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "<div style='text-align: left;color: #40262E !important'>" + "महायोग :-" + "</div>";
            //e.Row.Cells[1].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[5].Text = "<div style='text-align: right;color: #40262E !important'>" + TT2.ToString("N") + "</div>";
            //e.Row.Cells[5].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[7].Text = "<div style='text-align: right;color: #40262E !important'>" + TT3.ToString("N") + "</div>";
           // e.Row.Cells[7].BackColor = System.Drawing.Color.Silver;
        }
    }

    public void GetFinacialYear()
    {
        string qry = "";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number like ('1%')";
        //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and (Bill_Number like ('19%') or Bill_Number like ('20%'))";
        qry = "select Distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' AND Financial_Year>'2020-2021'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            trdet.Visible = true;
            ddlFY.DataSource = ds.Tables[0];
            ddlFY.DataTextField = "Financial_Year";
            ddlFY.DataValueField = "Financial_Year";
            ddlFY.DataBind();
            ddlFY.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlFY.DataSource = "";
            ddlFY.DataBind();
        }
    }
    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetActualBilNo();
    }
}
