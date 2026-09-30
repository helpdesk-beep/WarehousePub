using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class Rent_Bill_Print_NCCF_Rent_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["GodownID_New"] != null)
        {
            if (!IsPostBack)
            {
                FillCommodity();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void FillCommodity()
    {
        string qry = "";
        qry = "Select distinct NS.Commodity_ID,SC.Commodity_Name from tbl_Institution_NCCF_Storage_Bill_Details NS Inner join tbl_MetaData_STORAGE_COMMODITY SC On SC.Commodity_Id=NS.Commodity_Id where NS.Godown_Id='" + Session["GodownID_New"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_ID";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
        }
    }
    protected void Fillbill()
    {
        string qry = "";
        qry = "select distinct RN.Bill_Number from tbl_Bill_Institution_Daily_Charges_NCCF  RN inner join tbl_Institution_NCCF_Storage_Bill_Details DN on RN.Bill_Number=DN.Bill_Number where DN.Godown_Id='" + Session["GodownID_New"].ToString() + "' And DN.Commodity_ID='" + ddlcommodity.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlactualbillno.DataSource = ds.Tables[0];
            ddlactualbillno.DataTextField = "Bill_Number";
            ddlactualbillno.DataValueField = "Bill_Number";
            ddlactualbillno.DataBind();
            ddlactualbillno.Items.Insert(0, "--Select--");
        }
    }
    public void GetBillData()
    {
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_Institution_Daily_Charges_NCCF as BDSC WHERE BDSC.Bill_Number='" + ddlactualbillno.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD1.DataSource = ds;
            GD1.DataBind();
            divrent.Visible = true;
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
        qry = "select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,MD.DepotName,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_NCCF_Storage_Bill_Details as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id Inner Join tbl_MetaData_DEPOT MD on MD.BranchId=b.Branch_Id  where b.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";
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
            lblGdnum.Text = dt.Rows[0]["Godown_ID"].ToString();
            lblbranch.Text = dt.Rows[0]["DepotName"].ToString();
        }
        else
        {
            lblgdwnname.Text = "";
            lblcmd.Text = "";
            lblbillmonth.Text = "";
            lblbillno.Text = "";
            lblGdnum.Text = "";
        }
    }
    protected void ddlactualbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillData();
        GetBillOtherData();
    }

    protected void btnviewbill_Click(object sender, EventArgs e)
    {
        GetBillData();
        GetBillOtherData();
    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillbill();
    }
}