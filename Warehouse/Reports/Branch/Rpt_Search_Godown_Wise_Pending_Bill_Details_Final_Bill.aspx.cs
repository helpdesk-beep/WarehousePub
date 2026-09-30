using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_Rpt_Search_Godown_Wise_Pending_Bill_Details_Final_Bill : System.Web.UI.Page
{


    public SqlConnection Con_WH = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string IC_Id = "", Dist_Id = "";
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    double WhereHouseTotalBalance;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["FinalBIllNo"]))
            {
                divshowdetails.Visible = true;
                Getallbill_DetailsatIC();
                Getallbill_atICDigtal_Details();
                Getallbill_ByDist_Details();
                Getallbill_ByDistDigital_Details();
                Getallbill_NEFT_Deails();
                Getallbill_NEFT_UTR_Details();
            }
        }

    }

    public void Getallbill_DetailsatIC()
    {

        // DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_NetAmt,a.CSMS_NetAmt,a.Godown_Type,b.district_name ,cmd.Commodity_Name ,c.Godown_Name ,d.MonthName,'Done' as Status , Financial_Year from StorageBillVerifi2019_Remarks  a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.CommodityID left join FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  ) where Fin_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "' ", CommandType.Text, Con_CSMS);
        // DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_NetAmt,a.CSMS_NetAmt,a.Godown_Type,b.district_name ,cmd.Commodity_Name ,c.Godown_Name ,d.MonthName,'Done' as Status , Financial_Year from mpscsc.dbo.StorageBillVerifi2019_Remarks  a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID=c.Godown_ID  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.CommodityID left join mpscsc.dbo.FIN_MonthMaster d on d.MonthID=(select case when len(a.Month) = 1 then '0'+ a.Month else a.Month end  ) where Fin_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_WH);
        DataSet ds = rEturnDs("select a.Crop_Year,a.BillNo,a.WearHouse_NetAmt,a.CSMS_NetAmt,a.Godown_Type,b.district_name ,cmd.Commodity_Name ,c.Godown_Name ,d.MonthName,'Done' as Status , Financial_Year from mpscsc.dbo.StorageBillVerifi2019_Remarks a inner join mpscsc.pds.districtsmp b on a.District_Id = '23' + b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.GodownID = c.Godown_ID left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id = a.CommodityID left join mpscsc.dbo.FIN_MonthMaster d on d.MonthID = (select case when len(a.Month) = 1 then '0' + a.Month else a.Month end) where BillNo= '" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd_details.DataSource = ds.Tables[0];
            grd_details.DataBind();

        }

        else
        {
            grd_details.DataSource = null;
            grd_details.DataBind();

        }


    }

    public void Getallbill_atICDigtal_Details()
    {
        //DataSet ds = rEturnDs("select  Bill_Number,Crop_Year, cmd.Commodity_Name, Financial_Year,Net_Amount,Sub_Amount,b.district_name ,d.MonthName,c.Godown_Name from Digitally_Sign_StorageBill_IC a inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id left join FIN_MonthMaster d on a.Month_No=d.MonthID where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs("select  Bill_Number,Crop_Year, cmd.Commodity_Name, Financial_Year,Net_Amount,Sub_Amount,b.district_name ,d.MonthName,c.Godown_Name from mpscsc.dbo.Digitally_Sign_StorageBill_IC a inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID  left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id left join mpscsc.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grd_digital_sign.DataSource = ds.Tables[0];
            grd_digital_sign.DataBind();

        }

        else
        {
            grd_digital_sign.DataSource = null;
            grd_digital_sign.DataBind();
        }
    }


    public void Getallbill_ByDist_Details()
    {

        //DataSet ds = rEturnDs("select  b.district_name,a.CSMS_Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.CSMS_Sub_Amount,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month_No , d.MonthName ,cmd.Commodity_Name from [dbo].[tbl_Processing_StorageBill_AtDM] a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month_No=d.MonthID left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs("select  b.district_name,a.CSMS_Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.CSMS_Sub_Amount,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month_No , d.MonthName ,cmd.Commodity_Name from mpscsc.dbo.[tbl_Processing_StorageBill_AtDM] a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join mpscsc.dbo.FIN_MonthMaster d on a.Month_No=d.MonthID left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDMlevel.DataSource = ds.Tables[0];
            grdDMlevel.DataBind();
        }

        else
        {
            grdDMlevel.DataSource = null;
            grdDMlevel.DataBind();

        }
    }

    public void Getallbill_ByDistDigital_Details()
    {

        //DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month , d.MonthName ,cmd.Commodity_Name from [dbo].tbl_Digital_Sign_StorageBill_Final_ForNeft a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month=d.MonthID left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.Sub_Amount,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month , d.MonthName ,cmd.Commodity_Name from mpscsc.dbo.tbl_Digital_Sign_StorageBill_Final_ForNeft a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join mpscsc.dbo.FIN_MonthMaster d on a.Month=d.MonthID left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "'", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDMlevelDigital.DataSource = ds.Tables[0];
            grdDMlevelDigital.DataBind();
        }

        else
        {
            grdDMlevelDigital.DataSource = null;
            grdDMlevelDigital.DataBind();

        }
    }


    public void Getallbill_NEFT_Deails()
    {
        //DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month , d.MonthName ,cmd.Commodity_Name, a.NEFT_Ref_Id as DistrictLotId ,isnull(a.HO_NEFT_Ref_Id,'NA') as HoLotID  from [dbo].tbl_Storage_Payment_MPSCSC_NEFT a  inner join pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join FIN_MonthMaster d on a.Month= cast (d.MonthID as int) left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "' ", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs("select  b.district_name,a.Net_Amount, a.Crop_Year, Financial_Year , a.Bill_Number,a.Ref_Bill_No ,a.Party_Name,c.Godown_Name , a.Month , d.MonthName ,cmd.Commodity_Name, a.NEFT_Ref_Id as DistrictLotId ,isnull(a.HO_NEFT_Ref_Id,'NA') as HoLotID  from mpscsc.dbo.tbl_Storage_Payment_MPSCSC_NEFT a  inner join mpscsc.pds.districtsmp b on a.District_Id='23'+ b.district_code left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 c on a.Godown_Id=c.Godown_ID left join mpscsc.dbo.FIN_MonthMaster d on a.Month= cast (d.MonthID as int) left join Intergrated_MP_STORAGE.dbo.tbl_MetaData_STORAGE_COMMODITY cmd on cmd.Commodity_Id=a.Commodity_Id where a.Ref_Bill_No='" + Request.QueryString["FinalBIllNo"].ToString() + "' ", CommandType.Text, Con_WH);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdNeft.DataSource = ds.Tables[0];
            grdNeft.DataBind();

        }

        else
        {
            grdNeft.DataSource = null;
            grdNeft.DataBind();

        }

    }

    public void Getallbill_NEFT_UTR_Details()
    {
        //DataSet ds = rEturnDs("select Bill_Number, Ref_Bill_No, Branch_Name, Godown_Name, Payable_Amount, BranchBill_BankUTRNo, BranchBillPaymentDate from StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 where Ref_Bill_No ='" + Request.QueryString["FinalBIllNo"].ToString() + "' ", CommandType.Text, Con_CSMS);
        DataSet ds = rEturnDs("select Bill_Number, Ref_Bill_No, Branch_Name, Godown_Name, Payable_Amount, BranchBill_BankUTRNo, BranchBillPaymentDate from mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 where Ref_Bill_No ='" + Request.QueryString["FinalBIllNo"].ToString() + "' ", CommandType.Text, Con_WH);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gridNeftUTR.DataSource = ds.Tables[0];
            gridNeftUTR.DataBind();

        }

        else
        {
            gridNeftUTR.DataSource = null;
            gridNeftUTR.DataBind();

        }

    }

    protected DataSet rEturnDs(string sQL, CommandType commandType, SqlConnection conn)
    {
        DataSet dS = new DataSet();
        SqlCommand cmd = new SqlCommand(sQL);
        cmd.CommandType = commandType;
        int i = 0;
        checked
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dS);

            }
            catch (Exception e)
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                //throw new Exception(e.Message);
                //  Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + e.Message + "'); </script> ");
                ClientScript.RegisterStartupScript(this.GetType(), "script", e.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dS;
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}