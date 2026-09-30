using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class BranchPages_Print_Nafed_BIll_ : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                fillgrid(Base64Decode(Request.QueryString["BN"].ToString()));
                //DSCSign(Base64Decode(Request.QueryString["BN"].ToString()));
                //DSCSignR(Base64Decode(Request.QueryString["BN"].ToString()));
            }
        }
    }
    protected void fillgrid(String Bill_No)
    {
        GetBillData( Bill_No);
        GetBillOtherData( Bill_No);


    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    public void GetBillData(String Bill_No)
    {
        //  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        //qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent_NAFED as BDSC WHERE BDSC.Bill_Number='" + Bill_No + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD1.DataSource = ds;
            GD1.DataBind();
            //divrent.Visible = true;
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
    public void GetBillOtherData(String Bill_No)
    {
        qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details_For_NAFED as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id  where b.Bill_Number='" + Bill_No + "'  ";
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
            lblbranch.Text = Session["DepotName"].ToString();

            //  lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();

            //lblgdwnname.Text = dt.Rows[0]["Godown_Name"].ToString();
            //lbld_commodity.Text = dt.Rows[0]["Commodity_Name"].ToString();
            //lbld_month.Text = dt.Rows[0]["Month"].ToString();
            //lbld_billno.Text = dt.Rows[0]["Bill_Number"].ToString();
            //lblGdnum.Text = dt.Rows[0]["Godown_No"].ToString();
            ////   lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();

            //lblbillmonth.Text = dt.Rows[0]["Month"].ToString();
            //lblRentCmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
            //// txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            //lblRentFromDate.Text = dt.Rows[0]["FromDate"].ToString();
            //lblRentToDate.Text = dt.Rows[0]["ToDate"].ToString();
            //lblRentF_Year.Text = dt.Rows[0]["Financial_Year"].ToString();
            //lblRentCropYear.Text = dt.Rows[0]["Crop_Year"].ToString();
        }

        else
        {
            lblgdwnname.Text = "";
            lblcmd.Text = "";
            lblbillmonth.Text = "";
            lblbillno.Text = "";
            lblGdnum.Text = "";
            //  lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();

            //lbld_warehousename.Text = "";
            //lbld_commodity.Text = "";
            //lbld_month.Text = "";
            //lbld_billno.Text = "";
            //lbld_gdno.Text = "";
            ////   lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();

            //lblRentMonth.Text = "";
            //lblRentCmd.Text = "";
            //// txtBillAmt.Text = dt.Rows[0]["Sub_Amount"].ToString();
            //lblRentFromDate.Text = "";
            //lblRentToDate.Text = "";
            //lblRentF_Year.Text = "";
            //lblRentCropYear.Text = "";
        }
    }
}