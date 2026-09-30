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

public partial class Accounting_RM_GdwnGeneratedBillDetuction_MPWLC : System.Web.UI.Page
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
                lblP_regionnm.Text = Session["UserName"].ToString();
                GetBranch();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBillData()
    {
      //  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Institution_Daily_Charges as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            // tr_printdiv.Visible = true;
            GD1.DataSource = ds;
            GD1.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Closing_Weight"));
            GD1.FooterRow.Cells[1].Text = "महायोग :-";
            GD1.FooterRow.Cells[5].Text = total.ToString("N2");
            lblclosingbal.Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Charges"));
            GD1.FooterRow.Cells[7].Text = total1.ToString("N2");
        }
    }

    public void GetBillOtherData()
    {
       // qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate from tbl_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on (mg.Godown_ID=SUBSTRING(b.Bill_Number,0,11))  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";

        qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,SUBSTRING(mg.Godown_ID,8,3) as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate from tbl_Institution_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id  where b.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'  ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblgdwnname.Text = dt.Rows[0]["Godown_Name"].ToString();
            lblcmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
          //  lblbillmonth.Text = dt.Rows[0]["Month"].ToString();
            lblbillno.Text = dt.Rows[0]["Bill_Number"].ToString();
            lblgdwnNo.Text = dt.Rows[0]["Godown_No"].ToString();
            lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();
            lbldatefromto.Text = dt.Rows[0]["Month"].ToString();


            lblrmmonth.Text = dt.Rows[0]["Month"].ToString();
            lblrmcmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
         //   txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            lblfrmdate.Text = dt.Rows[0]["FromDate"].ToString();
            lbltodate.Text = dt.Rows[0]["ToDate"].ToString();
            lblfinancial.Text = dt.Rows[0]["Financial_Year"].ToString();
        }
    }

    public void GetBillAmountDetails()
    {
        qry = "select Net_Amount,JVS_Net_Amount,MPWLC_Amt from tbl_GdwnRentBill_Detuction_RM where Ref_Bill_Number='" + ddlbillno.SelectedValue.ToString() +"'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblNetAmount.Text = dt.Rows[0]["Net_Amount"].ToString();
            lbljvsnetamt.Text = dt.Rows[0]["JVS_Net_Amount"].ToString();
            lblmpwlcnetamt.Text = dt.Rows[0]["MPWLC_Amt"].ToString();
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
    protected void ddlgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        // qry = " select distinct Bill_Number from tbl_Storage_Bill_Details where Godown_Id='"+ ddlgdwn.SelectedValue.ToString() +"' order by Bill_Number";  

       // qry = " select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "' order by Bill_Number";

        qry = "select distinct Bill_Number from tbl_Institution_Storage_Bill_Details where Godown_ID='" + ddlgdwn.SelectedValue.ToString() + "' and Bill_Number like ('19%')";
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
        }
    }

    protected void ddlbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        trdet.Visible = true;
        GetBillData();
        GetBillOtherData();
        GetBillAmountDetails();
    }
}
