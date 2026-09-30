using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
public partial class BranchPages_Nafed_Branch_Print_Bill : System.Web.UI.Page
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

                //lblP_regionnm.Text = Session["UserName"].ToString();
                //lbld_rmname.Text = Session["UserName"].ToString();
                //lbleporm.Text = Session["UserName"].ToString(); ;
                GetBranch();
                Fillgodown();
                //DSCSign(Base64Decode(Request.QueryString["BN"].ToString()));
                //DSCSignR(Base64Decode(Request.QueryString["BN"].ToString()));
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
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where BranchId='" + Session["BranchId"].ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtbranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
           // lblbranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
            Session["DepotName"] = ds.Tables[0].Rows[0]["DepotName"].ToString();

        }
    }

    protected void Fillgodown()
    {
        //lbld_branch.Text = ddlBranch.SelectedItem.Text.Trim();
        //lblbranch.Text = ddlBranch.SelectedItem.Text.Trim();
        //Label12.Text = Session["UserName"].ToString();
        //lblAcBranch.Text = ddlBranch.SelectedItem.Text.Trim();
        //lblepobranch.Text = ddlBranch.SelectedItem.Text;

        string qry = "";
        qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "'";
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
        qry = "select distinct RN.Bill_Number from tbl_Bill_PVT_Godown_Daily_Rent_NAFED  RN inner join tbl_Institution_Storage_Bill_Details_For_NAFED DN on RN.Bill_Number=DN.Bill_Number where DN.Godown_Id='" + ddlgdwn.SelectedValue.ToString() + "'";
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
        //  qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,Convert(decimal(18,2),BDSC.Opening_Weight) as Opening_Weight,Convert(decimal(18,2),BDSC.Receive_Weight) as Receive_Weight,Convert(decimal(18,2), BDSC.Issue_Weight) as Issue_Weight,Convert(decimal(18,2),BDSC.Closing_Weight) as Closing_Weight,Convert(decimal(18,2),BDSC.Per_Day_Rate) as Per_Day_Rate,Convert(decimal(18,2),BDSC.Total_Charges) as Total_Charges FROM tbl_Bill_Godown_JVS_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        //qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent as BDSC WHERE BDSC.Bill_Number='" + ddlbillno.SelectedValue.ToString() + "'";
        qry = " SELECT convert(varchar(10),BDSC.Dates,103) as Date,BDSC.Opening_Weight as Opening_Weight,BDSC.Receive_Weight as Receive_Weight, BDSC.Issue_Weight as Issue_Weight,BDSC.Closing_Weight as Closing_Weight,BDSC.Per_Day_Rate as Per_Day_Rate,BDSC.Total_Charges as Total_Charges FROM tbl_Bill_PVT_Godown_Daily_Rent_NAFED as BDSC WHERE BDSC.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'";

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
        qry = " select (CONVERT(varchar(10),b.BId)+'/'+CONVERT(varchar(10),b.Created_Date,105)) as ReceiptNo,b.Bill_Number,b.District_Id,b.Branch_Id,mg.Godown_ID,GodownNum as Godown_No,mg.Godown_APN as Godown_Owner,mg.Godown_Name,cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity,mg.Hired_Type,b.Commodity_Id,(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id=b.Commodity_Id) as Commodity_Name, (SELECT DateName(mm,DATEADD(mm,b.Month,-1)) as [MonthName]) as Month,CONVERT(varchar(10),b.From_Date,103) as FromDate,CONVERT(varchar(10),b.To_Date,103) as ToDate,b.Financial_Year,b.Commodity_Rate,b.Net_Amount,b.Sub_Amount,b.Service_Tax_Perc,b.Service_Tax_Amt,b.Rebate_Perc,b.Rebate_Amt,b.Per_Day_Rate,b.Crop_Year from tbl_Institution_Storage_Bill_Details_For_NAFED as b inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID=b.Godown_Id  where b.Bill_Number='" + ddlactualbillno.SelectedValue.ToString() + "'  ";
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
    //protected void fillgrid(String Bill_No)
    //{
    //    try
    //    {
    //        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("Get_Nafed_Storage_Bill_Print", con))
    //            {
    //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //                cmd.Parameters.AddWithValue("@Bill_Number", Bill_No);
    //                using (SqlDataAdapter sda = new SqlDataAdapter())
    //                {
    //                    cmd.Connection = con;
    //                    sda.SelectCommand = cmd;
    //                    using (DataTable dt = new DataTable())
    //                    {
    //                        sda.Fill(dt);
    //                        if (dt.Rows.Count > 0)
    //                        {
    //                            GD2.DataSource = dt;
    //                            GD2.DataBind();
    //                            GD2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:center;", "Center");
    //                            GD2.FooterRow.Cells[9].Text = "Total";
    //                            GD2.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Charges")).ToString();
    //                            lblRegion.Text = dt.Rows[0]["Regionnm"].ToString();
    //                            lbldist.Text = dt.Rows[0]["District_Name"].ToString();
    //                            lblBranch.Text = dt.Rows[0]["DepotName"].ToString();
    //                            lblbillno_Actual.Text = dt.Rows[0]["Bill_Number"].ToString();
    //                            lblAcGdwnName.Text = dt.Rows[0]["Godown"].ToString();
    //                            lblgodownid.Text = dt.Rows[0]["Godown_Id"].ToString();
    //                            lbldatefromto.Text = dt.Rows[0]["Bill_Month"].ToString();
    //                            lblbillingdate.Text = dt.Rows[0]["Billing_Date"].ToString();
    //                            lblcmd_ac.Text = dt.Rows[0]["Commodity"].ToString();
    //                            lblam.Text = dt.Rows[0]["Net_Amount"].ToString();
    //                        }
    //                        else
    //                        {
    //                            GD2.DataSource = null;
    //                            GD2.DataBind();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}
    //public void DSCSign(String Bill_No)
    //{
    //    qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='B'";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        Image1.Visible = true;
    //        lblBSerialNo.Text = "DSC Serial No : " + dt.Rows[0]["DSC_Serial_No"].ToString();
    //        lblBIP.Text = "Client IP : " + dt.Rows[0]["Client_Ip"].ToString();
    //        lblBHolderName.Text = "DSC Holder Name : " + dt.Rows[0]["DSC_Holder_Name"].ToString();
    //        lblBCreatedDate.Text = "DSC Sign Date : " + dt.Rows[0]["CreatedDate"].ToString();
    //    }
    //    else
    //    {
    //        Image1.Visible = false;
    //        lblBSerialNo.Text = "";
    //        lblBIP.Text = "";
    //        lblBHolderName.Text = "";
    //        lblBCreatedDate.Text = "";
    //    }
    //}
    //public void DSCSignR(String Bill_No)
    //{

    //    qry = "select DSC_Serial_No,DSC_Holder_Name,Client_Ip,Convert(varchar(10),CreatedDate,103) as CreatedDate from tbl_Digitally_Signed_Bill_Details_NAFED where Ref_Bill_No='" + Bill_No + "'  and DSC_User_Type='R'";
    //    SqlCommand cmd1 = new SqlCommand(qry, con);
    //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
    //    DataTable dt1 = new DataTable();
    //    da1.Fill(dt1);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        Image2.Visible = true;
    //        lblICSerialNo.Text = "DSC Serial No : " + dt1.Rows[0]["DSC_Serial_No"].ToString();
    //        lblICIp.Text = "Client IP : " + dt1.Rows[0]["Client_Ip"].ToString();
    //        lblICHoldername.Text = "DSC Holder Name : " + dt1.Rows[0]["DSC_Holder_Name"].ToString();
    //        lblICCreatedDate.Text = "DSC Sign Date : " + dt1.Rows[0]["CreatedDate"].ToString();
    //    }
    //    else
    //    {
    //        Image2.Visible = false;
    //        lblICSerialNo.Text = "";
    //        lblICIp.Text = "";
    //        lblICHoldername.Text = "";
    //        lblICCreatedDate.Text = "";
    //    }
    //}
    //public static string Base64Decode(string base64EncodedData)
    //{
    //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
    //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    //}

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
//    public void fillgrid()
//    {
//        string Branch_Id = Session["BranchId"].ToString();
//        using (SqlConnection con = new SqlConnection(con))
//        {
//            SqlCommand cmd = new SqlCommand("Get_Nafed_Rent_Bill_Details_For_Branch_Approval", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
//            SqlDataAdapter da = new SqlDataAdapter(cmd);
//            DataTable dt = new DataTable();
//            da.Fill(dt);
//            if (con.State == ConnectionState.Open)
//            { con.Close(); }
//            if (dt.Rows.Count > 0)
//            {
//                GD1.DataSource = dt;
//                GD1.DataBind();
//            }
//            else
//            {
//                GD1.DataSource = null;
//                GD1.DataBind();
//            }
//        }

//    }
}