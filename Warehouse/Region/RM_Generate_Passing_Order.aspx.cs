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

public partial class Region_RM_Generate_Passing_Order : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                //    lblP_regionnm.Text = Session["UserName"].ToString();
                //    lbld_rmname.Text = Session["UserName"].ToString();
                GetBranch();
                txtlocknotopen.Enabled = false;
                txtRoadBlock.Enabled = false;
                /////Change On 21-04-2026
                RestoreDropdownValues();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBillData()
    {
        ////  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='"+ ddlbillno.SelectedValue.ToString() +"'";
        //qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";

        //SqlCommand cmd = new SqlCommand(qry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    // tr_printdiv.Visible = true;
        //    GD1.DataSource = ds;
        //    GD1.DataBind();

        //    DataTable dt = ds.Tables[0];
        //    decimal total = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Closing_Weight"));
        //    GD1.FooterRow.Cells[1].Text = "महायोग :-";
        //    GD1.FooterRow.Cells[5].Text = total.ToString("N2");
        //    //   lblclosingbal.Text = total.ToString("N2");

        //    decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Charges"));
        //    GD1.FooterRow.Cells[7].Text = total1.ToString("N2");
        //}
        //else
        //{
        //    GD1.DataSource = "";
        //    GD1.DataBind();
        //}
    }

    public void GetBillOtherData()
    {
        if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate, CONVERT(decimal(18,0), b.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),b.Sub_Amount) as Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=b.Godown_Id)  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";
        }
        else if (ddlGodownType.SelectedItem.Text == "Silo Bags")
        {
            //qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate, CONVERT(decimal(18,0), b.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),b.Sub_Amount) as Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=b.Godown_Id)  where b.Bill_Number='" + ddlactualbill.SelectedValue.ToString() + "'  ";
            qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate, CONVERT(decimal(18,0), b.Net_Amount) as Net_Amount,CONVERT(decimal(18,0),b.Sub_Amount) as Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=b.Godown_Id)  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";

        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblgdwnname.Text = dt.Rows[0]["Godown_Name"].ToString();
            //lblcmd.Text = dt.Rows[0]["Commodity_Name"].ToString() + "  " + dt.Rows[0]["Crop_Year"].ToString();
            //lblbillmonth.Text = dt.Rows[0]["Month"].ToString();
            //lblbillno.Text = dt.Rows[0]["Bill_Number"].ToString();
            //lblGdnum.Text = dt.Rows[0]["Godown_No"].ToString();
            //lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();
            // lblCPYear.Text = dt.Rows[0]["Crop_Year"].ToString();
            lblCPYear.Text = dt.Rows[0]["Commodity_Rate"].ToString();

            //lbld_warehousename.Text = dt.Rows[0]["Godown_Name"].ToString();
            //lbld_commodity.Text = dt.Rows[0]["Commodity_Name"].ToString() + "  " + dt.Rows[0]["Crop_Year"].ToString();
            //lbld_month.Text = dt.Rows[0]["Month"].ToString();
            //lbld_billno.Text = dt.Rows[0]["Bill_Number"].ToString();
            //lbld_gdno.Text = dt.Rows[0]["Godown_No"].ToString();
            //lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();

            //Add Crop in Session
            //BEGIN
            Session["Cropyear"]= dt.Rows[0]["Crop_Year"].ToString();
            //END

            lblrmmonth.Text = dt.Rows[0]["Month"].ToString();
            lblrmcmd.Text = dt.Rows[0]["Commodity_Name"].ToString() + "  " + dt.Rows[0]["Crop_Year"].ToString();
            txtPAmount.Text = dt.Rows[0]["Sub_Amount"].ToString();
           // txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblfrmdate.Text = dt.Rows[0]["FromDate"].ToString();
            lbltodate.Text = dt.Rows[0]["ToDate"].ToString();
            lblfinancial.Text = dt.Rows[0]["Financial_Year"].ToString();

            lblRentAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();

        }
        else
        {
            //lblgdwnname.Text = "";
            //lblcmd.Text = "";
            //lblbillmonth.Text = "";
            //lblbillno.Text = "";
            //lblGdnum.Text = "";
            //lblbranch.Text = "";
            // lblCPYear.Text = dt.Rows[0]["Crop_Year"].ToString();
            lblCPYear.Text = "";

            //lbld_warehousename.Text = "";
            //lbld_commodity.Text = "";
            //lbld_month.Text = "";
            //lbld_billno.Text = "";
            //lbld_gdno.Text = "";
            //lbld_branch.Text = "";

            lblrmmonth.Text = "";
            lblrmcmd.Text = "";
            txtBillAmt.Text = "0";
            lblfrmdate.Text = "";
            lbltodate.Text = "";
            lblfinancial.Text = "";
        }
    }

    public void GetBillDetuctionData()
    {
        //qry = "select CONVERT(decimal(18,0), TResources_Deduct_Amt) as TResources_Deduct_Amt,TBill_Deduct_Amt,TBill_Amount,Net_Bill_Amount from tbl_Godown_Rent_Deduction_Amount where Bill_No ='" + ddlbillno.SelectedValue.ToString() + "' ";
        qry = "select CONVERT(decimal(18,0), TResources_Deduct_Amt) as TResources_Deduct_Amt,TBill_Deduct_Amt,TBill_Amount,Net_Bill_Amount,Ref_Bill_No,Net_Amount,ISNULL(Dunnage_PS_Amt,0) AS Dunnage_PS_Amt,ISNULL(Elect_Beam_Scale_Amt,0) AS Elect_Beam_Scale_Amt from tbl_Godown_Rent_Deduction_Amount inner join tbl_Institution_Storage_Bill_Details on tbl_Institution_Storage_Bill_Details.Bill_Number=tbl_Godown_Rent_Deduction_Amount.Ref_Bill_No where Bill_No ='" + ddlbillno.SelectedValue.ToString() + "' ";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //    lbl_TResources_Deduct_Amt.Text = dt.Rows[0]["TResources_Deduct_Amt"].ToString();
            txtresdetAmt.Text = dt.Rows[0]["TResources_Deduct_Amt"].ToString();
            lblSBill.Text = dt.Rows[0]["Ref_Bill_No"].ToString();
            lblPrastut_netamount.Text = dt.Rows[0]["Net_Amount"].ToString();
            txtlocknotopen.Text = dt.Rows[0]["Dunnage_PS_Amt"].ToString();
            txtRoadBlock.Text = dt.Rows[0]["Elect_Beam_Scale_Amt"].ToString();
        }

        else
        {
            //   lbl_TResources_Deduct_Amt.Text = "0";
            txtresdetAmt.Text = "0";
            txtlocknotopen.Text = "0";
            txtRoadBlock.Text = "0";
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
    /////Change On 21-04-2026
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodownType.SelectedIndex = 0;

        ddlgdwn.Items.Clear();
        ddlgdwn.Items.Insert(0, "--Select--");
        ddlgdwn.SelectedIndex = 0;

        ddlbillno.Items.Clear();
        ddlbillno.Items.Insert(0, "--Select--");
        ddlbillno.SelectedIndex = 0;
        trdet.Visible = false;
    }
    protected void ddlgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetActualBilNo();
        //if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        //{
        //    qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number='" + ddlactualbill.SelectedValue.ToString() + "'";
        //}
        //else if (ddlGodownType.SelectedItem.Text == "Silo Bags" || ddlGodownType.SelectedItem.Text == "Tribal Scheme")
        //{
        //    qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,CONVERT(decimal(18,0), Sub_Amount) as Sub_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number='" + ddlactualbill.SelectedValue.ToString() + "'";
        //}
        //SqlCommand cmd = new SqlCommand(qry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    lblPrastut_netamount.Text = dt.Rows[0]["Net_Amount"].ToString();

        //if (ddlactualbill.DataSource != null || ddlactualbill.DataSource != "")
        //{
        //  qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and  order by Bill_Number";

        //  qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "')  order by Bill_Number";
        if (ddlGodownType.SelectedItem.Text == "Hired Godowns")
        {
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='HG'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=1) order by Bill_Number";
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='HG'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=1) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') order by Bill_Number";
            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='HG'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=1) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') order by Bill_Number";

        }
        else if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year='2020-21' and Month_No>=8 group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') order by Bill_Number";
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year='2020-2021' and Month_No>=8 group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') order by Bill_Number";
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year='2020-2021' group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and From_Date>='08/01/2020' order by Bill_Number";
            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year in ('2020-2021','2021-2022','2022-2023','2023-2024','2024-2025','2025-2026','2026-2027') group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and From_Date>='08/01/2020' order by Bill_Number";

        }
        else if (ddlGodownType.SelectedItem.Text == "Silo Bags" || ddlGodownType.SelectedItem.Text == "Tribal Scheme")
        {
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "'";
            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year='2020-2021' and Month_No>=8 group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') order by Bill_Number";
            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year in ('2020-2021','2021-2022','2022-2023','2023-2024','2024-2025','2025-2026','2026-2027') group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and From_Date>='08/01/2020' order by Bill_Number";

        }
        else if (ddlGodownType.SelectedItem.Text == "CAP-PMS")
        {
            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT where Financial_Year in('2020-2021','2021-2022','2022-2023','2023-2024','2024-2025','2025-2026','2026-2027') group by Bill_Number having COUNT(Bill_Number)=2) and Bill_Number not in (select distinct JVS_Bill_Number from tbl_GdwnRentBill_Detuction_RM where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "') and From_Date>='08/01/2020' order by Bill_Number";

        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbillno.DataSource = ds.Tables[0];
            ddlbillno.DataTextField = "Bill_Number";
            ddlbillno.DataValueField = "Bill_Number";
            ddlbillno.DataBind();
            ddlbillno.Items.Insert(0, "--Select--");
            //ddlbillno.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
            //ddlbillno_SelectedIndexChanged(null, null);
            trdet.Visible = false;

        }
        else
        {
            ddlbillno.DataSource = "";
            ddlbillno.DataBind();
            //ddlbillno_SelectedIndexChanged(null, null);
            ddlbillno.Items.Insert(0, "--Select--");
            trdet.Visible = false;
        }
        //}

        //getcalculation();
        //}

    }

    //public void GetActualBilNo()
    //{
    //    string qry = "";
    //    //regular
    //    //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and  Bill_Number not in (select D.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as D where D.GodownID='" + ddlgdwn.SelectedValue.ToString() + "') and Bill_Number in (select CMS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CMS where CMS.Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' )";
    //    qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and  Bill_Number not in (select D.Ref_Bill_Number from tbl_GdwnRentBill_Detuction_RM as D where D.GodownID='" + ddlgdwn.SelectedValue.ToString() + "')";

    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        trdet.Visible = true;
    //        ddlactualbill.DataSource = ds.Tables[0];
    //        ddlactualbill.DataTextField = "Bill_Number";
    //        ddlactualbill.DataValueField = "Bill_Number";
    //        ddlactualbill.DataBind();
    //        ddlactualbill.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        trdet.Visible = false;
    //        ddlactualbill.DataSource="";
    //        ddlactualbill.DataBind();
    //    }
    //}
    private void GetSCBillsDetail()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_for_Find_actual_Payment", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", ddlbillno.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            txtSC.Text = dt.Rows[0]["TotalCapacity"].ToString();
                            txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                            txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                            txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    private void GetSCBillsDetailForSiloBags()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_for_Find_actual_Payment_SiloBags", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", ddlbillno.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Crop_Year"].ToString() == "2023-2024" && dt.Rows[0]["Hired_Type"].ToString()== "Silo Bags")
                            {
                                txtSC.Text = "30000";
                                txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                                txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                                txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                            }
                            //else
                            //{
                            //    txtSC.Text = dt.Rows[0]["TotalCapacity"].ToString();
                            //    txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                            //    txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                            //    txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                            //}
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }

    private void GetSCBillsDetailForSiloBagsOtherCropYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_for_Find_actual_Payment_SiloBags_OtherCropYear", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BillNumber", ddlbillno.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["Crop_Year"].ToString() != "2023-2024" && dt.Rows[0]["Hired_Type"].ToString() == "Silo Bags")
                            {
                                txtSC.Text = dt.Rows[0]["TotalCapacity"].ToString();
                                txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                                txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                                txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                            }
                           else if (dt.Rows[0]["Crop_Year"].ToString() == "2023-2024" && dt.Rows[0]["Hired_Type"].ToString() == "Silo Bags" && (ddlgdwn.SelectedValue != "23180040306" || ddlgdwn.SelectedValue != "23220020113" || ddlgdwn.SelectedValue != "23220030081" || ddlgdwn.SelectedValue != "23070030090" || ddlgdwn.SelectedValue != "2337006010296" || ddlgdwn.SelectedValue != "2340002070" || ddlgdwn.SelectedValue != "23370030329"))
                            {
                                txtSC.Text = dt.Rows[0]["TotalCapacity"].ToString();
                                txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                                txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                                txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                            }
                            //else
                            //{
                            //    txtSC.Text = dt.Rows[0]["TotalCapacity"].ToString();
                            //    txtnoofdays.Text = dt.Rows[0]["Days"].ToString();
                            //    txtPer_Day_Rate.Text = dt.Rows[0]["Per_Day_Rate"].ToString();
                            //    txtRASC.Text = dt.Rows[0]["PaybleAmount"].ToString();
                            //}
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
    protected void ddlbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbillno.SelectedValue.ToString() == "0" || ddlbillno.SelectedValue.ToString() == "--Select--")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Bill Number');", true);
            trdet.Visible = false;
            return;
        }
        getcalculation();
        //old
        trdet.Visible = true;
        GetBillData();
        GetBillOtherData();
        GetBillDetuctionData();
        //if (ddlGodownType.SelectedValue == "4")
        //{
        //    if ((Session["Cropyear"].ToString() == "2023-2024") && (ddlgdwn.SelectedValue.ToString() != "23180040306" || ddlgdwn.SelectedValue.ToString() != "23220020113" || ddlgdwn.SelectedValue.ToString() != "23220030081" || ddlgdwn.SelectedValue.ToString() != "23070030090" || ddlgdwn.SelectedValue.ToString() != "2337006010296" || ddlgdwn.SelectedValue.ToString() != "2340002070" || ddlgdwn.SelectedValue.ToString()!= "23370030329"))
        //    {
        //        GetSCBillsDetailForSiloBags();
        //    }
        //    else
        //    {
        //        GetSCBillsDetailForSiloBagsOtherCropYear();
        //    }
        //}
        //else
        //{
        //    GetSCBillsDetail();
        //}
        GetSCBillsDetail();
        if (Convert.ToDecimal(txtPAmount.Text) <= Convert.ToDecimal(txtRASC.Text))
        {
            txtBillAmt.Text = txtPAmount.Text;
        }
        else if (Convert.ToDecimal(txtPAmount.Text) > Convert.ToDecimal(txtRASC.Text))
        {
            txtBillAmt.Text = txtRASC.Text;
        }

        //txtTDSAmt.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(txtBillAmt.Text) * 10) / 100)));
        //txtTDSAmt.Text = Convert.ToString(Math.Round(((Convert.ToDouble(txtBillAmt.Text) * 7.5) / 100)));
        txtTDSAmt.Text = Convert.ToString(Math.Round(((Convert.ToDouble(txtBillAmt.Text) * 10) / 100)));
        if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        {
            txtGainAmt.Text = Convert.ToString(Math.Round(((Convert.ToDecimal(txtBillAmt.Text) * 20) / 100)));
        }
        else if (ddlGodownType.SelectedItem.Text == "Silo Bags")
        {
            txtGainAmt.Text = "0";
        }
        getcalculation();

    }
    public void getcalculation()
    {
        if (txtBillAmt.Text != null || txtBillAmt.Text != "")
        {
            //Decimal sumtotal = Convert.ToDecimal(txtlocknotopen.Text)+ Convert.ToDecimal(txtRoadBlock.Text);
            //decimal Gtotal = Convert.ToDecimal(txtresdetAmt.Text) - Convert.ToDecimal(sumtotal);
            lblFinalTDS.Text = txtTDSAmt.Text;
            lblgrandtotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtBillAmt.Text) - Convert.ToDecimal(txtresdetAmt.Text) - Convert.ToDecimal(txtTDSAmt.Text) - Convert.ToDecimal(txtGainAmt.Text) - Convert.ToDecimal(txtscrDep.Text) - Convert.ToDecimal(txtotherdetuction.Text) - Convert.ToDecimal(txtlocknotopen.Text) - Convert.ToDecimal(txtRoadBlock.Text)));
            lbltotDetuc.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtresdetAmt.Text) + Convert.ToDecimal(txtGainAmt.Text) + Convert.ToDecimal(txtscrDep.Text) + Convert.ToDecimal(txtotherdetuction.Text)+ Convert.ToDecimal(txtlocknotopen.Text) + Convert.ToDecimal(txtRoadBlock.Text)));
            //lbltotDetuc.Text = Convert.ToString(Math.Round(Convert.ToDecimal(sumtotal) + Convert.ToDecimal(txtGainAmt.Text) + Convert.ToDecimal(txtscrDep.Text) + Convert.ToDecimal(txtotherdetuction.Text)));
            //lblMPWLCPartAmt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(lblPrastut_netamount.Text) - Convert.ToDecimal(txtBillAmt.Text)));
            lblMPWLCPartAmt.Text = (Convert.ToDecimal(txtBillAmt.Text) - Convert.ToDecimal(lblgrandtotal.Text)).ToString();

            lblRentAmt.Text = txtBillAmt.Text;
        }
        else
        {

        }
    }

    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

        //if (ddlactualbill.SelectedItem.Text == "--Select--")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown...')", true);
        //}
        if (ddlBranch.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch...')", true);
        }
        //else if (ddlactualbill.SelectedItem.Text == "--Select--")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select STorage Charges Bill Number...')", true);
        //}
        else if (ddlbillno.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown Rent Bill...')", true);
        }
        else if (txtBillAmt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Amount Not Blank...')", true);
        }
        else if (lblMPWLCPartAmt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('MPWLC Part Amount Not Blank...')", true);
        }
        else if (lblPrastut_netamount.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Storage Charges Amount Not Blank..')", true);
        }
        else if (Convert.ToDecimal(lblPrastut_netamount.Text) < 0 || Convert.ToDecimal(lbltotDetuc.Text) < 0 || Convert.ToDecimal(lblFinalTDS.Text) < 0 || Convert.ToDecimal(lblgrandtotal.Text) < 0 || Convert.ToDecimal(lblMPWLCPartAmt.Text) < 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Amount should be Positive..')", true);
        }
        else
        {
            //qry = "INSERT INTO [tbl_GdwnRentBill_Detuction_RM] ([BranchID],[GodownID],[JVS_Bill_Number],[Ref_Bill_Number],[JVS_Bill_Amount],[Res_Detuction_Amount],[TDS_Detuction_Amount],[Gain_Detuction_Amount],[JVS_Net_Amount],[CreatedBy],[CreatedDate],[Net_Amount],[Security_Detuction_Amt],[Other_Detuction_Amt],[Total_Detuction_Amt],[MPWLC_Amt]) VALUES ('" + ddlBranch.SelectedValue.ToString() + "','" + ddlgdwn.SelectedValue.ToString() + "','" + ddlbillno.SelectedValue.ToString() + "','" + ddlactualbill.SelectedValue.ToString() + "','" + txtBillAmt.Text.Trim() + "','" + txtresdetAmt.Text.Trim() + "','" + txtTDSAmt.Text.Trim() + "','" + txtGainAmt.Text.Trim() + "','" + lblgrandtotal.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + lblPrastut_netamount.Text.Trim() + "','" + txtscrDep.Text.Trim() + "','" + txtotherdetuction.Text.Trim() + "','" + lbltotDetuc.Text.Trim() + "','" + lblMPWLCPartAmt.Text.Trim() + "')";
            qry = "INSERT INTO [tbl_GdwnRentBill_Detuction_RM] ([BranchID],[GodownID],[JVS_Bill_Number],[Ref_Bill_Number],[JVS_Bill_Amount],[Res_Detuction_Amount],[TDS_Detuction_Amount],[Gain_Detuction_Amount],[JVS_Net_Amount],[CreatedBy],[CreatedDate],[Net_Amount],[Security_Detuction_Amt],[Other_Detuction_Amt],[Total_Detuction_Amt],[MPWLC_Amt],Other_Detuction_Res,Deduction_11,Deduction_12) VALUES ('" + ddlBranch.SelectedValue.ToString() + "','" + ddlgdwn.SelectedValue.ToString() + "','" + ddlbillno.SelectedValue.ToString() + "','" + lblSBill.Text + "','" + txtBillAmt.Text.Trim() + "','" + txtresdetAmt.Text.Trim() + "','" + txtTDSAmt.Text.Trim() + "','" + txtGainAmt.Text.Trim() + "','" + lblgrandtotal.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + lblPrastut_netamount.Text.Trim() + "','" + txtscrDep.Text.Trim() + "','" + txtotherdetuction.Text.Trim() + "','" + lbltotDetuc.Text.Trim() + "','" + lblMPWLCPartAmt.Text.Trim() + "','"+ txtODResion.Text.Trim() + "','" + txtlocknotopen.Text.Trim() + "','" + txtRoadBlock.Text.Trim() + "')";

            SqlCommand cmd = new SqlCommand(qry, con);
            int a = cmd.ExecuteNonQuery();
            //if (a == 1)
            //{
            //    con.Close();
            //    btnGenerateBill.Enabled = false;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Successfully Save Record'); window.location =('RM_Generate_Passing_Order.aspx');", true);
            //}
            /////Change On 21-04-2026

            if (a == 1)
            {
                con.Close();

                // Store values in Session
                Session["Branch"] = ddlBranch.SelectedValue;
                Session["GodownType"] = ddlGodownType.SelectedValue;
                Session["Godown"] = ddlgdwn.SelectedValue;

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Successfully Save Record'); window.location='RM_Generate_Passing_Order.aspx';",
                true);
            }

            //END
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error..')", true);
            }
        }
    }

    /////Change On 21-04-2025
    private void RestoreDropdownValues()
    {
        if (Session["Branch"] != null)
        {
            ddlBranch.SelectedValue = Session["Branch"].ToString();
        }

        if (Session["GodownType"] != null)
        {
            ddlGodownType.SelectedValue = Session["GodownType"].ToString();
            ddlGodownType_SelectedIndexChanged(null, null);
        }

        if (Session["Godown"] != null)
        {
            ddlgdwn.SelectedValue = Session["Godown"].ToString();
            ddlgdwn_SelectedIndexChanged(null, null);
        }

        ddlbillno.SelectedIndex = 0;
        Session.Remove("Branch");
        Session.Remove("GodownType");
        Session.Remove("Godown");
    }
    protected void ddlactualbill_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        //{
        //    qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number='" + ddlactualbill.SelectedValue.ToString() + "'";
        //}
        //else if (ddlGodownType.SelectedItem.Text == "Silo Bags" || ddlGodownType.SelectedItem.Text == "Tribal Scheme")
        //{
        //    qry = "select CONVERT(decimal(18,0), Net_Amount) as Net_Amount,CONVERT(decimal(18,0), Sub_Amount) as Sub_Amount,Month,(SELECT DateName(mm,DATEADD(mm,Month,-1)) as [MonthName]) as MonthName,Financial_Year from tbl_Institution_Storage_Bill_Details where Bill_Number='" + ddlactualbill.SelectedValue.ToString() + "'";
        //}
        //    SqlCommand cmd = new SqlCommand(qry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //      lblPrastut_netamount.Text = dt.Rows[0]["Net_Amount"].ToString();

        //    if (ddlactualbill.DataSource != null || ddlactualbill.DataSource != "")
        //    {
        //        //  qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD' and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and  order by Bill_Number";

        //      //  qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "')  order by Bill_Number";
        //        if (ddlGodownType.SelectedItem.Text == "Hired Godowns")
        //        {
        //            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='HG'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=1) order by Bill_Number";
        //        }
        //        else if (ddlGodownType.SelectedItem.Text != "Silo Bags")
        //        {
        //            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='GR'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "') and Bill_Number in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=2) order by Bill_Number";
        //        }
        //        else if (ddlGodownType.SelectedItem.Text == "Silo Bags"|| ddlGodownType.SelectedItem.Text == "Tribal Scheme")
        //        {
        //            //qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number not in (select Bill_No from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + ddlactualbill.SelectedValue + "') and Bill_Number not in (select distinct  Bill_Number from tbl_Digitally_Signed_Bill_PVT group by Bill_Number having COUNT(Bill_Number)=1) order by Bill_Number";
        //            qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Bill_Type='AD'  and Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number='" + ddlactualbill.SelectedValue + "'";
        //        }
        //        cmd = new SqlCommand(qry, con);
        //        da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlbillno.DataSource = ds.Tables[0];
        //            ddlbillno.DataTextField = "Bill_Number";
        //            ddlbillno.DataValueField = "Bill_Number";
        //            ddlbillno.DataBind();
        //            ddlbillno.SelectedValue = ds.Tables[0].Rows[0][0].ToString();
        //            ddlbillno_SelectedIndexChanged(null, null);
        //        }
        //        else
        //        {
        //            ddlbillno.DataSource = "";
        //            ddlbillno.DataBind();
        //            ddlbillno_SelectedIndexChanged(null, null);
        //        }
        //    }

        //    getcalculation();
        //}
    }
    protected void txtscrDep_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }
    protected void txtTDSAmt_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }
    protected void txtotherdetuction_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }
    protected void txtGainAmt_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        /////Change On 21-04-2025
        Session["ddlGodownType"] = null;
        Session["ddlgdwn"] = null;
        Session["ddlbillno"] = null;

        Session["ddlGodownType"] = ddlGodownType.SelectedValue;
        string qry = "";
        if (ddlBranch.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Branch');", true);
            return;
        }
        else if (ddlGodownType.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Godown Type');", true);
            return;
        }
        //END
        else if (ddlGodownType.SelectedValue.ToString() == "2")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sign_StorageBill_IC as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Joint Venture(JV)','WDRA')";
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Joint Venture(JV)','WDRA')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('Joint Venture(JV)','WDRA')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "3")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sign_StorageBill_IC as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Hired')";
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Hired')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('Hired')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "4")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sign_StorageBill_IC as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Silo Bags')";
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Silo Bags')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('Silo Bags')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "6")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from mpscsc.dbo.Digitally_Sign_StorageBill_IC as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Tribal Scheme')";
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('Tribal Scheme')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('Tribal Scheme')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "7")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('CAP-PMS')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('CAP-PMS')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "8")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('BOT')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('BOT')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "9")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('BOT-AUB')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('BOT-AUB')";

        }
        else if (ddlGodownType.SelectedValue.ToString() == "10")
        {
            //qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID in (select distinct CS.Godown_Id from tbl_Digitally_Signed_Bill_PVT as CS where CS.Branch_Id='" + ddlBranch.SelectedValue.ToString() + "') and Hired_Type in ('BOT-AUB')";
            qry = "select Godown_name + ' ('+ gdn.Godown_ID +')' as Godown_name ,gdn.Godown_ID from tbl_metadata_godown_2018 gdn INNER JOIN (Select distinct Godown_Id From tbl_Digitally_Signed_Bill_PVT )DP ON gdn.Godown_ID=DP.Godown_Id where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('PVT.PEG')";

        }

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
            trdet.Visible = false;
        }
        else 
        {
            ddlgdwn.Items.Clear();
            ddlgdwn.Items.Insert(0, "--Select--");
            ddlgdwn.SelectedIndex = 0;

            ddlbillno.Items.Clear();
            ddlbillno.Items.Insert(0, "--Select--");
            ddlbillno.SelectedIndex = 0;
            trdet.Visible = false;
            trdet.Visible = false; 
        }
    }

    protected void ddlyesno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlyesno.SelectedValue== "0")
        {
            txtlocknotopen.Enabled = false;
            txtRoadBlock.Enabled = false;
        }
        else if(ddlyesno.SelectedValue == "1")
        {
            txtlocknotopen.Enabled = false;
            txtRoadBlock.Enabled = false;
        }
        else if (ddlyesno.SelectedValue == "2")
        {
            txtlocknotopen.Enabled = true;
            txtRoadBlock.Enabled = true;
        }
    }

    protected void txtlocknotopen_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }

    protected void txtRoadBlock_TextChanged(object sender, EventArgs e)
    {
        getcalculation();
    }
}