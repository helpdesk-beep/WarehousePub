using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MPSCSC_WS;
public partial class Accounting_frm_MPSCSC_SC_Final_Bill_For_Vacant_Capacity : System.Web.UI.Page
{
    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    /*CSMS_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new CSMS_WS.MPSCSC_InstituitionStorageBillDetails();*/

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    string DFReceive_ID = "";
    string Bill_No = "";
    int BID = 0;
    string Depositor_ID = "129";
    int Days_In_Month = 0;
    decimal Total_Charge = 0;
    decimal Rate_M = 0;
    DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    string Crop_Year = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                //GetCropYear();
                fillMonth();
                fillYearNew();
                //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Branch_Welcome.aspx';", true);
            }

        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
    }
    protected void fillYearNew()
    {
        //ddlmonth.ClearSelection();
        ddlFYearNew.Items.Clear();
        ddlFYearNew.Items.Add(new ListItem("--Select--", "0"));
        //ddlFYearNew.Items.Add(new ListItem("2020", "2020"));
        //ddlFYearNew.Items.Add(new ListItem("2019", "2019"));
        ddlFYearNew.Items.Add(new ListItem("2025", "2025-26"));
        ddlFYearNew.Items.Add(new ListItem("2024", "2024-25"));
        ddlFYearNew.Items.Add(new ListItem("2023", "2023-24"));
        ddlFYearNew.Items.Add(new ListItem("2022", "2022-23"));
        ddlFYearNew.Items.Add(new ListItem("2021", "2021-22"));
        ddlFYearNew.Items.Add(new ListItem("2020", "2020-21"));
        ddlFYearNew.Items.Add(new ListItem("2019", "2019-20"));
        ddlFYearNew.SelectedIndex = 0;
    }
    //void GetCropYear()
    //{
    //    //qry = "select distinct CropYear from View_WHRcurrentstock where Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
    //    qry = "select distinct CropYear from View_WHRcurrentstock";

    //    da = new SqlDataAdapter(qry, con);
    //    ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds == null)
    //    {
    //    }
    //    else
    //    {
    //        ddlCropYear.DataSource = ds.Tables[0];
    //        ddlCropYear.DataTextField = "CropYear";
    //        ddlCropYear.DataValueField = "CropYear";
    //        ddlCropYear.DataBind();
    //        ddlCropYear.Items.Insert(0, "--Select--");
    //    }
    //}
    public void GetBillData()
    {
        if (ddlFYearNew.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Year- Month'); </script> ");
        }
        else
        {
            string CFnancialYear = ddlFYearNew.SelectedItem.Text;
            int ContMD = 0;
            //ContMD = DateTime.DaysInMonth(Convert.ToInt32(ddlyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            ContMD = DateTime.DaysInMonth(Convert.ToInt32(CFnancialYear), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
            DateTime LDateTime = new DateTime();
            //DateTime StartDate = new DateTime();
            //DateTime EndDate = new DateTime();
            string SD = "";
            string ED = "";
            if (Convert.ToInt32(ddlmonth.SelectedValue) <= 9)
            {
                SD = "01/" + "0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/0" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }
            else
            {
                SD = "01/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
                ED = ContMD + "/" + ddlmonth.SelectedValue.ToString() + "/" + CFnancialYear;
            }

            StartDate = Convert.ToDateTime(getDate_MDY(SD));
            //EndDate = Convert.ToDateTime(getDate_MDY(ED));
            EndDate = Convert.ToDateTime(getDate_MDY(SD));
            ViewState["SD"] = SD;
            ViewState["ED"] = ED;
            Get_Rate();
            GetBillsDetail();
            //Panel1.Visible = false;
        }
    }
    private void GetBillsDetail()
    {
        try
        {

            string Dist_id = Session["Depot_DistID"].ToString();
            string str = "";
            //if (ddlBillType.SelectedValue.ToString() == "1")
            //{
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.Commodity_Id='"+ ddlcomodity.SelectedValue + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('Hired','Owned','WDRA','Joint Venture(JV)')  and Storage_Type='Covered' and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Storage_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "'  and BB.Fin_Bill_No is not null) order by SB.Created_Date";
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.Commodity_Id='" + ddlcomodity.SelectedValue + "' and SB.District_Id='" + Dist_id + "' and (BO_Approval_Status is null OR BO_Approval_Status='') and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('Hired','Owned','WDRA','Joint Venture(JV)')  and Storage_Type='Covered' and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Storage_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "'  and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Vacant_Capacity_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'07/06/2025',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "'  and (BO_Approval_Status is null OR BO_Approval_Status='') and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('PVT.PEG')  and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "'  and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            str = "Select Bill_Number,Godown_Id,CONVERT(varchar(10),Created_Date,103) as Billing_Date,Floor(Sub_Amount) as Charges_Amount,Floor(isnull(Service_Tax_Amt,0)) as GST_AMT ,Floor(isnull(MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(GST_Amt_SC,0)) as GST_Sup_Amt,Floor(Net_Amount) as Net_Amount,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Vacant_Capacity_Bill_Details SB Where SB.Branch_Id='" + Session["BranchId"].ToString() + "' and (BO_Approval_Status is null OR BO_Approval_Status='') and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('PVT.PEG','BOT') and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='SS' And Net_Amount >'0' and SB.Month='" + ddlmonth.SelectedValue + "' AND SB.Financial_Year='"+ddlFinancialYear.SelectedValue.ToString()+"' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "' and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            //}
            //else
            //{
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.Commodity_Id='" + ddlcomodity.SelectedValue + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('" + ddlBillType.SelectedItem.Text + "') and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Storage_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "' and BB.Fin_Bill_No is not null  and BB.Fin_Bill_No is not null) order by SB.Created_Date";
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Storage_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.Commodity_Id='" + ddlcomodity.SelectedValue + "' and SB.District_Id='" + Dist_id + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('" + ddlBillType.SelectedItem.Text + "') and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Storage_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "' and BB.Fin_Bill_No is not null  and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            //str = "select SB.Bill_Number,SB.Godown_Id,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0)) as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Vacant_Capacity_Bill_Details as SB where SB.Created_Date>=CONVERT(varchar(10),'07/06/2025',101) and SB.Branch_Id='" + Session["BranchId"].ToString() + "' and BO_Approval_Status is null and SB.Depositor_Id='129' and SB.Crop_Year='" + ddlCropYear.SelectedValue.ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('" + ddlBillType.SelectedItem.Text + "') and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='AD' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "' and BB.Fin_Bill_No is not null  and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            // str = "Select Bill_Number,Godown_Id,CONVERT(varchar(10),Created_Date,103) as Billing_Date,Floor(Sub_Amount) as Charges_Amount,Floor(isnull(Service_Tax_Amt,0)) as GST_AMT ,Floor(isnull(MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(GST_Amt_SC,0)) as GST_Sup_Amt,Floor(Net_Amount) as Net_Amount,(select Godown_Name from tbl_MetaData_GODOWN_2018 as G where G.Godown_ID=SB.Godown_Id)+'('+Godown_Id+')' as Godown from tbl_Institution_Vacant_Capacity_Bill_Details SB Where SB.Branch_Id='" + Session["BranchId"].ToString() + "' and SB.Godown_Id in (select Godown_Id from tbl_MetaData_GODOWN_2018 where Hired_Type in ('PVT.PEG') and Branch_Id='" + Session["BranchId"].ToString() + "') and SB.Bill_Type='SS' and SB.Month='" + ddlmonth.SelectedValue + "' and SB.Bill_Number not in (select Bill_Number from tbl_Institution_Vacant_Capacity_Bill_Details as BB where BB.Branch_Id='" + Session["BranchId"].ToString() + "' and (BB.Fin_Bill_No is not null and BB.Fin_Bill_No!='')) order by SB.Created_Date";
            //}
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOBillApp.DataSource = ds.Tables[0];
                gvBOBillApp.DataBind();
                gvBOBillApp.Columns[1].Visible = false;
                trnewproc.Visible = true;
                tblbtn.Visible = true;
                lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
                btn_save.Enabled = true;
                //pnlCofirmmsg.Visible = false;
                //Panel2.Visible = false;


            }
            else
            {
                gvBOBillApp.DataSource = null;
                gvBOBillApp.DataBind();
                trnewproc.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data Found'); </script> ");
            }

        }

        catch (Exception ex)
        {

        }
    }
    //private void GetFinalBillDetail()
    //{
    //    try
    //    {
    //        string Dist_id = Session["Depot_DistID"].ToString();
    //        string Branch_Id = Session["BranchId"].ToString();          
    //        string str = "select DT.District_Name,DP.DepotName,SB.Commodity_Rate,SB.Bill_Number,(select D.Depositor_Name from tbl_MetaData_DEPOSITOR as D where D.Depositor_ID=SB.Depositor_Id) as Depositor_Name,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=SB.Commodity_Id) as Commodity,CONVERT(varchar(10),SB.Created_Date,103) as Billing_Date,Floor(SB.Sub_Amount) as Charges_Amount,Floor(isnull(SB.Service_Tax_Amt,0)) as GST_AMT,Floor(isnull(SB.MPWLC_SC,0)) as Sup_Charges_Amt,Floor(isnull(SB.GST_Amt_SC,0))as GST_Sup_Amt,Floor(SB.Net_Amount) as Net_Amount,DateName( month , DateAdd( month ,SB.Month , 0 ) - 1 ) as Bill_Month,SB.Crop_Year from tbl_Institution_Storage_Bill_Summary as SB inner join tbl_MetaData_DEPOT as dp on dp.BranchId=SB.Branch_Id inner join tbl_MetaData_DISTRICT as DT on DT.District_Id=SB.District_Id  where SB.Created_Date>=CONVERT(varchar(10),'10/01/2019',101) and SB.Branch_Id='" + Branch_Id +"' and Bill_Number='"+ Bill_No + "'and SB.District_Id='"+ Dist_id +"' and BO_Approval_Status is null and SB.Depositor_Id='129' order by SB.Created_Date";

    //        SqlDataAdapter da = new SqlDataAdapter(str, con);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvBill.DataSource = ds.Tables[0];
    //            gvBill.DataBind();
    //            //gvBOBillApp.Columns[1].Visible = false;
    //            //trnewproc.Visible = true;
    //            //tblbtn.Visible = true;
    //            //lblNoofAC.Text = ds.Tables[0].Rows.Count.ToString();
    //            //btn_save.Enabled = true;

    //            lblDistrict.Text=ds.Tables[0].Rows[0]["District_Name"].ToString();
    //            lblBranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
    //            Label12.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
    //            lblRate.Text= ds.Tables[0].Rows[0]["Commodity_Rate"].ToString();
    //            lblBillingDate.Text= ds.Tables[0].Rows[0]["Billing_Date"].ToString();
    //            btnNo.Enabled = false;
    //        }
    //        else
    //        {
    //            gvBill.DataSource = "";
    //            gvBill.DataBind();
    //        }

    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //ViewState["TotalBags"] = lblTotalBags.Text;
        //string changedLabelValue = hdnLabelState.Value;
        try
        {
            int TotalR = 0;
            decimal TotalC = 0;
            decimal TGSTA = 0;
            decimal TotalSC = 0;
            decimal TotalGSTOnSC = 0;
            decimal TotalBillAmt = 0;
            foreach (GridViewRow row in gvBOBillApp.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                if (chkbox.Checked == true)
                {
                    TotalR = TotalR + 1;
                    TextBox TotalC1 = (TextBox)row.FindControl("txtcharges");
                    TotalC = TotalC + Convert.ToDecimal(TotalC1.Text);
                    TextBox TGSTA1 = (TextBox)row.FindControl("txtGSTAmt");
                    TGSTA = TGSTA + Convert.ToDecimal(TGSTA1.Text);
                    TextBox TotalSC1 = (TextBox)row.FindControl("txtSupcharges");
                    TotalSC = TotalSC + Convert.ToDecimal(TotalSC1.Text);
                    TextBox TotalGSTOnSC1 = (TextBox)row.FindControl("txtGSTPer");
                    TotalGSTOnSC = TotalGSTOnSC + Convert.ToDecimal(TotalGSTOnSC1.Text);
                    TextBox TotalBillAmt1 = (TextBox)row.FindControl("txtweight");
                    TotalBillAmt = TotalBillAmt + Convert.ToDecimal(TotalBillAmt1.Text);
                }
            }
            string Deposit_Date = gvBOBillApp.Rows[0].Cells[3].Text;
            //lblTotalRecord.Text = hdnLabelState.Value;
            lblBillAmount.Text = hdnLabelStateQty.Value;
            lblSTotalCharges.Text = HiddenField11.Value;
            lblSGSTAmt.Text = HiddenField12.Value;
            lblSSupCharges.Text = HiddenField13.Value;
            lblSGSTonSupChar.Text = HiddenField14.Value;
            if (hdnLabelState.Value == "0" || hdnLabelState.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select At least one Record'); </script> ");
            }
            else if (hdnLabelState.Value == "" || hdnLabelState.Value != TotalR.ToString())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else if (TotalC.ToString() != HiddenField11.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else if (TGSTA.ToString() != HiddenField12.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else if (TotalSC.ToString() != HiddenField13.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else if (TotalGSTOnSC.ToString() != HiddenField14.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else if (TotalBillAmt.ToString() != hdnLabelStateQty.Value)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Miss matched record'); </script> ");
            }
            else
            {
                GetReceivedSummary();
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    public void GetReceivedSummary()
    {
        lblBillCategory.Text = ddlBillType.SelectedItem.Text;
        //lblCommodity.Text = ddlcomodity.SelectedItem.Text;
        //lblCropYear.Text = ddlCropYear.SelectedValue;
        lblMonths.Text = ddlmonth.SelectedItem.Text;
        lblTotalRecord.Text = hdnLabelState.Value;
        lblBillAmount.Text = hdnLabelStateQty.Value;
        lblSTotalCharges.Text = HiddenField11.Value;
        lblSGSTAmt.Text = HiddenField12.Value;
        lblSSupCharges.Text = HiddenField13.Value;
        lblSGSTonSupChar.Text = HiddenField14.Value;
        hdnBillCategoryID.Text = ddlBillType.SelectedValue;
        lblRPM.Text = ViewState["Rate_PM"].ToString();
        lblRPD.Text = ViewState["Rate_PD"].ToString();

        //lblGodown.Text = ddl_godown.SelectedItem.Text;
        //lblSendBags.Text = lblTotalBagSend.Text;
        //lblSendQty.Text = lblTotalQtySend.Text;
        //lblRcdBags.Text = hdnLabelState.Value;
        //lblRecdQty.Text = hdnLabelStateQty.Value;
        //lblDepositor.Text = ddlDepositor.SelectedItem.Text;
        //lblCropYear.Text = ddlcropyear.SelectedValue;
        //lblCommodity.Text = ddlProcCmd.SelectedItem.Text;
        //hdnGodownID.Text = ddl_godown.SelectedValue;
        //hdnDepositorID.Text = ddlDepositor.SelectedValue;
        //hdnCommodityID.Text = ddlProcCmd.SelectedValue;
        ModalPopupExtender2.Show();
        Button2.Visible = true;
        //
        //lblTotalBags.Text = hdnLabelState.Value;
        //lblTotalQty.Text = hdnLabelStateQty.Value;
    }
    public void GetStorageBillNo()
    {
        Crop_Year = ddlFinancialYear.SelectedItem.Text;
        string Crop_YearEnd = Crop_Year.Substring(Crop_Year.Length - 2, 2);
        //string CommodityId = ddlcomodity.SelectedValue;
        //Depositor_ID = "129";ddl
        string BillSubType = ddlBillType.SelectedValue.ToString();
        string MonthSub = "";
        string MonthStr = ddlmonth.SelectedValue;
        int Month = Convert.ToInt32(ddlmonth.SelectedValue);
        int MonthLen = MonthStr.Length;
        if (MonthLen == 1)
        {
            MonthSub = "0" + MonthStr;
        }
        else
        {
            MonthSub = MonthStr;
        }
        //string FinYear = ddlFYearNew.SelectedItem.Text;
        string FinYear = ddlFYearNew.SelectedItem.Value;
        string FinYearN = "";
        //FinYear = NineCrop(FinYear);
        FinYearN = NineCrop(FinYear);
        string BranchID = Session["BranchID"].ToString();
        //qry = "select max(BId) as BId from tbl_Institution_Storage_Bill_Summary where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "'";
        //qry = "select max(BId) as BId from tbl_Institution_Storage_Bill_Summary where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "' and Financial_Year='"+ FinYear + "'";
        qry = "select max(BId) as BId from tbl_Institution_Vacant_Capacity_Bill_Summary where branch_Id='" + BranchID + "' and Depositor_Id='" + Depositor_ID + "' and Financial_Year='" + FinYearN + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        //string Bill_No = "";
        //string Godown_Id = ddlgodown.SelectedValue.ToString();
        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);

                int SubBN = BID + 1;
                //9-1-2302001-20-11-0001
                //Bill_No = "9" + BillSubType + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + SubBN.ToString();
                Bill_No = "91" + Crop_YearEnd + BillSubType + BranchID + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();

                BID = SubBN;
            }
            else
            {
                //Bill_No = "9" + BillSubType + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
                Bill_No = "91" + Crop_YearEnd + BillSubType + BranchID + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                BID = 1;
            }
        }
        else
        {
            //Bill_No = "9" + BillSubType + BranchID + ((DateTime.Now.Year).ToString()).Substring(2, 2) + MonthSub + "1";
            Bill_No = "91" + Crop_YearEnd + BillSubType + BranchID + "01" + MonthSub + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            BID = 1;
        }
        //ViewState["BillNo"] = Bill_No;
        //if (ddlGodownType.SelectedValue == "1")
        //{
        //    ViewState["BillNo"] = "91"+Bill_No;
        //}
        //else
        //{
        ViewState["BillNo"] = Bill_No;
        //}
        ViewState["BID"] = BID;
    }
    public void Insert_Bill_Detail()
    {
        try
        {
            SqlTransaction sqltran;
            string Godown_Bill_No = "";
            string Godown_ID = "";
            string qry = "";
            Bill_No = ViewState["BillNo"].ToString();
            string Is_Rebate = "N";
            string DeposCategory = "";
            decimal RebatePer = 0;
            decimal NoBags = 0;
            string Rin_Pustika_No = "";
            string Cast_Certificate_No = "";
            decimal ChargeOfTotalWeight = 0;
            //string CropYear = ddlCropYear.SelectedItem.Text;
            decimal NetAmount = 0;
            string Bill_Type = hdnBillCategoryID.Text;

            //string FinYear = "2020-21";
            string FinYear = ddlFinancialYear.SelectedValue.ToString();
            if (ddlFinancialYear.SelectedValue == "2025-2026" && Convert.ToInt32(ddlmonth.SelectedValue) >= 4)
            {
                FinYear = "2025-26";
            }
            else if (ddlFinancialYear.SelectedValue == "2025-2026" && Convert.ToInt32(ddlmonth.SelectedValue) <= 3)
            {
                FinYear = "2024-25";
            }
            //string FinYear = ddlFyear.SelectedItem.Text;
            //CropYear = NineCrop(CropYear);
            //FinYear = NineCrop(FinYear);
            Is_Rebate = "N";
            RebatePer = 0;
            NoBags = 0;
            Rin_Pustika_No = "0";
            Cast_Certificate_No = "0";
            //ChargeOfTotalWeight=
            //decimal SerTax = (ChargeOfTotalWeight * Convert.ToDecimal(txtstax.Text)) / 100;
            decimal GST_Per = 0;
            decimal GST_Amount = Convert.ToDecimal(lblSGSTAmt.Text);
            if (GST_Amount != 0 && GST_Amount.ToString() != null)
            {
                GST_Per = 18;
            }
            else
            {
                GST_Per = 0;
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string Dist_id = Session["Depot_DistID"].ToString();
            string BranchID = Session["BranchID"].ToString();
            string VStartDate = ViewState["SD"].ToString();
            string VEndDate = ViewState["ED"].ToString();
            //Get_Bill_Type();
            BID = Convert.ToInt32(ViewState["BID"]);
            //For Silo Bags Only
            decimal MPWLC_SC = 0;
            //MPWLC_SC = Math.Round(((Math.Round(ChargeOfTotalWeight) * 10) / 100));
            MPWLC_SC = Math.Round(Convert.ToDecimal(lblSSupCharges.Text));


            //GST_Amt_SC = Math.Round((MPWLC_SC * GST_Per_SC) / 100);
            decimal GST_Per_SC = 0;
            decimal GST_Amt_SC = 0;
            GST_Amt_SC = Math.Round(Convert.ToDecimal(lblSGSTonSupChar.Text)); ;
            if (GST_Amt_SC == 0 || GST_Amt_SC.ToString() == null)
            {
                GST_Per_SC = 0;
            }
            else
            {
                GST_Per_SC = 18;
            }
            int ICount = 0;
            //
            //Rounding
            //decimal Rount_NetAmount = Math.Round(NetAmount);
            decimal Rount_NetAmount = Math.Round(Convert.ToDecimal(lblBillAmount.Text));
            NetAmount = Rount_NetAmount;
            decimal Rount_ChargeOfTotalWeight = Math.Round(Convert.ToDecimal(lblSTotalCharges.Text));
            ChargeOfTotalWeight = Rount_ChargeOfTotalWeight;
            con.Open();
            sqltran = con.BeginTransaction();
            //if (ddlGodownType.SelectedValue.ToString() != "4" && ddlGodownType.SelectedValue.ToString() != "6")
            //{
            //qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + Convert.ToDecimal(lblRPM.Text) + "," + NetAmount + "," + ChargeOfTotalWeight + ",18," + SerTax + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','"+ ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "',18,'" + Convert.ToDecimal(lblSGSTonSupChar.Text) + "')";
            //qry = "INSERT INTO tbl_Institution_Storage_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Commodity_Rate,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Crop_Year,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + Convert.ToDecimal(lblRPM.Text) + "," + NetAmount + "," + ChargeOfTotalWeight + ",'"+ GST_Per + "'," + GST_Amount + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + CropYear + "','" + ddlmonth.SelectedValue + "','" + ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "','"+ GST_Per_SC + "','" + GST_Amt_SC + "')";
            qry = "INSERT INTO tbl_Institution_Vacant_Capacity_Bill_Summary(Bill_Number,District_Id,Branch_Id,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,Commodity_Rate,Crop_Year,From_Date,To_Date,Packing_Type,Weight,Financial_Year,Net_Amount,Sub_Amount,Service_Tax_Perc,Service_Tax_Amt,Depositor_Category,Rebate_Perc,Rebate_Amt,Rebate_on_Unit,Khasra_Number,Is_Rebate,Created_Date,Modified_Date,Client_IP,Per_Day_Rate,Rin_Pustika_No,Cast_Certificate_No,Bill_Type,BId,Month,Year,MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Bill_Count) values('" + Bill_No + "','" + Dist_id + "','" + BranchID + "','4','129','3','0','0','0','" + getDate_MDY(VStartDate) + "','" + getDate_MDY(VEndDate) + "','1','5','" + FinYear + "'," + NetAmount + "," + ChargeOfTotalWeight + ",'" + GST_Per + "'," + GST_Amount + ",'" + DeposCategory + "'," + RebatePer + ",0," + NoBags + ",'0','" + Is_Rebate + "',getdate(),'','" + ip + "','" + Convert.ToDecimal(lblRPD.Text) + "','" + Rin_Pustika_No + "','" + Cast_Certificate_No + "','" + Bill_Type + "','" + BID + "','" + ddlmonth.SelectedValue + "','" + ddlFYearNew.SelectedItem.Text + "','" + Convert.ToDecimal(lblSSupCharges.Text) + "','" + GST_Per_SC + "','" + GST_Amt_SC + "','" + lblTotalRecord.Text + "')";

            SqlCommand cmd = new SqlCommand(qry, con, sqltran);
            //con.Open();
            int n = cmd.ExecuteNonQuery();
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            //Web Service Call For data to MPSCSC
            //System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
            //MPSCSCDemo.EDAddInstitutionStorageBillSummary(Bill_No, Dist_id, BranchID, "4", "129", "3", ddlcomodity.SelectedValue.ToString(), getDate_MDY(VStartDate), getDate_MDY(VEndDate), "1", "5", FinYear, Convert.ToDecimal(txtcomrate.Text), NetAmount, ChargeOfTotalWeight, 0, 0, DeposCategory, RebatePer, 0, Convert.ToInt32(NoBags), "0", Rin_Pustika_No, Cast_Certificate_No, Is_Rebate, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, Convert.ToDecimal(txtCPRate.Text), Bill_Type, BID, Convert.ToInt32(ddlmonth.SelectedValue), string.Empty, CropYear, MPWLC_SC, GST_Per_SC, GST_Amt_SC, Convert.ToInt32(lblTotalRecord.Text));
            //System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
            //MPSCSCDemo.EDAddInstitutionStorageBillSummary(Bill_No, Dist_id, BranchID, "4", "129", "3", "0", getDate_MDY(VStartDate), getDate_MDY(VEndDate), "1", "5", FinYear, Convert.ToDecimal(txtcomrate.Text), NetAmount, ChargeOfTotalWeight, 0, 0, DeposCategory, RebatePer, 0, Convert.ToInt32(NoBags), "0", Rin_Pustika_No, Cast_Certificate_No, Is_Rebate, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip, Convert.ToDecimal(txtCPRate.Text), Bill_Type, BID, Convert.ToInt32(ddlmonth.SelectedValue), string.Empty, "0", MPWLC_SC, GST_Per_SC, GST_Amt_SC, Convert.ToInt32(lblTotalRecord.Text));
            //con.Close();
            if (n > 0)
            {
                foreach (GridViewRow row in gvBOBillApp.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                    if (chkbox.Checked == true)
                    {
                        Godown_Bill_No = row.Cells[0].Text;
                        Godown_ID = row.Cells[1].Text;

                        string qry2 = "update tbl_Institution_Vacant_Capacity_Bill_Details set Fin_Bill_No='" + Bill_No + "' where Bill_Number='" + Godown_Bill_No + "' and Godown_Id='" + Godown_ID + "'";
                        SqlCommand cmd2 = new SqlCommand(qry2, con, sqltran);
                        //con.Open();
                        int i = cmd2.ExecuteNonQuery();
                        //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                        //Web Service Call For data to MPSCSC
                        //System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                        //MPSCSCDemo.EDUpdateInstituitionStorageBillDetailswithFinBillNo(Bill_No, Godown_Bill_No, Godown_ID);
                        
                        
                        //System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                        //MPSCSCDemo.EDUpdateInstituitionStorageBillDetailswithFinBillNo(Bill_No, Godown_Bill_No, Godown_ID);
                        //con.Close();
                        ICount = ICount + i;
                    }
                }
                if (!string.IsNullOrEmpty(lblTotalRecord.Text))
                {
                    if (Convert.ToInt32(lblTotalRecord.Text) == ICount)
                    {
                        sqltran.Commit();
                        con.Close();
                    }
                    else
                    {
                        sqltran.Rollback();
                        con.Close();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in button save click Please Try Again')", true);
                    }
                }

                lblrmsg.Visible = true;
                lblrmsg.Text = "Bill Number : " + ViewState["BillNo"].ToString() + " Generated Successfully...";
                btnNewBill.Visible = true;
                Button2.Visible = false;
                //Button1.Visible = true;
                //GetFinalBillDetail();
                ModalPopupExtender2.Show();
                //pnlCofirmmsg.Visible = true;

            }
        }
        catch (Exception ex)
        {
            lblrmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public string NineCrop(string INCROPS)
    {
        string InCrop = INCROPS;
        string OutCrop = "";
        if (InCrop == "2015-16")
        {
            OutCrop = "2015-2016";
        }
        else if (InCrop == "2016-17")
        {
            OutCrop = "2016-2017";
        }
        else if (InCrop == "2017-18")
        {
            OutCrop = "2017-2018";
        }
        else if (InCrop == "2018-19")
        {
            OutCrop = "2018-2019";
        }
        else if (InCrop == "2019-20")
        {
            OutCrop = "2019-2020";
        }
        else if (InCrop == "2020-21")
        {
            OutCrop = "2020-2021";
        }
        else if ((InCrop == "2021-22" && ddlmonth.SelectedValue == "1") || (InCrop == "2021-22" && ddlmonth.SelectedValue == "2") || (InCrop == "2021-22" && ddlmonth.SelectedValue == "3"))
        {
            OutCrop = "2021-2022";
        }
        else if (InCrop == "2021-22")
        {
            OutCrop = "2021-2022";
        }
        else if (InCrop == "2022-23")
        {
            OutCrop = "2022-2023";
        }
        else if (InCrop == "2023-24")
        {
            OutCrop = "2023-2024";
        }
        else if (InCrop == "2024-25")
        {
            OutCrop = "2024-2025";
        }
        else if (InCrop == "2025-26")
        {
            OutCrop = "2025-2026";
        }

        return OutCrop;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (lblSTotalCharges.Text == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please check Total Charges Amount'); </script> ");
        }
        else if (lblBillAmount.Text == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please check Total Bill Amount'); </script> ");
        }
        else
        {
            GetStorageBillNo();
            Insert_Bill_Detail();
        }
    }
    public void Get_Rate()
    {
        //Days_In_Month = DateTime.DaysInMonth(Convert.ToInt32(ddlFyear.SelectedItem.Text), Convert.ToInt32(ddlmonth.SelectedValue.ToString()));
        int Month_No = Convert.ToInt32(ddlmonth.SelectedValue);
        if (Month_No == 1 || Month_No == 3 || Month_No == 5 || Month_No == 7 || Month_No == 8 || Month_No == 10 || Month_No == 12)
        {
            Days_In_Month = 31;
        }
        else if (Month_No == 2)
        {
            Days_In_Month = 29;
            // Days_In_Month = 28;
        }
        else
        {
            Days_In_Month = 30;
        }
        //Rate_M = 0;
        //string RPM_Str = "";
        //txtcomrate.Text = Rate_M.ToString();
        //txtcomrate.Text = "93.60";
        //if (ddlBillType.SelectedValue != "2")
        //{
        //    RPM_Str = Set_Rate();
        //}
        //else if (ddlBillType.SelectedValue == "2")
        //{
        //    RPM_Str = "24";
        //}
        //Rate_M = Convert.ToDecimal(RPM_Str);
        ////txtcomrate.Text = "93.60";
        //txtcomrate.Text = RPM_Str;
        //txtCPRate.Text = Math.Round((93.60 / Days_In_Month), 4).ToString();
        txtCPRate.Text = Math.Round((Rate_M / Days_In_Month), 4).ToString();
        //txtCPRate.Text = Math.Round((Rate_M / 30), 4).ToString();
        ViewState["Days_In_Month"] = Days_In_Month;
        ViewState["Rate_PM"] = txtcomrate.Text.ToString();
        ViewState["Rate_PD"] = txtCPRate.Text.ToString();
    }
    //public string Set_Rate()
    //{
    //    string CropYear = ddlCropYear.SelectedItem.Text;
    //    //string CmdId = ddlcomodity.SelectedValue;
    //    string RatePM = "";
    //    if (CropYear == "2025-26")
    //    {
    //        //RatePM = "101.60";
    //        RatePM = "102.00";
    //    }
    //    else if (CropYear == "2024-25")
    //    {
    //        //RatePM = "101.60";
    //        RatePM = "102.00";
    //    }
    //    else if (CropYear == "2023-24")
    //    {
    //        RatePM = "99.20";
    //    }
    //    else if (CropYear == "2019-20")
    //    {
    //        RatePM = "93.60";
    //    }
    //    else if (CropYear == "2020-21")
    //    {
    //        RatePM = "104.20";
    //    }
    //    else if (CropYear == "2021-22")
    //    {
    //        RatePM = "104.20";
    //    }

    //    else if (CropYear == "2022-23")
    //    {
    //        RatePM = "104.20";
    //    }
    //    else if (CropYear == "2018-19")
    //    {
    //        RatePM = "86";
    //    }
    //    else if (CropYear == "2017-18")
    //    {
    //        RatePM = "83";
    //    }
    //    else if (CropYear == "2016-17")
    //    {
    //        RatePM = "74";
    //    }
    //    else if (CropYear == "2015-16")
    //    {
    //        RatePM = "67.60";
    //    }
    //    else if (CropYear == "2014-15" )
    //    {
    //        RatePM = "61.40";
    //    }
    //    else if (CropYear == "2013-14")
    //    {
    //        RatePM = "58.40";
    //    }
    //    else if (CropYear == "2012-13")
    //    {
    //        RatePM = "54.60";
    //    }
    //    //other
    //    //else if (CropYear == "2023-24" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    //{
    //    //    RatePM = "107.80";
    //    //}
    //    else if (CropYear == "2024-25" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.80";
    //        RatePM = "102.00";
    //    }
    //    else if (CropYear == "2023-24" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        //RatePM = "107.80";
    //        RatePM = "102.00";
    //    }
    //    else if (CropYear == "2022-23" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2021-22" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2020-21" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3" || CmdId == "129" || CmdId == "131"))
    //    {
    //        RatePM = "107.80";
    //    }
    //    else if (CropYear == "2019-20" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "104.20";
    //    }
    //    else if (CropYear == "2018-19" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "86";
    //    }
    //    else if (CropYear == "2017-18" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "83";
    //    }
    //    else if (CropYear == "2016-17" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "74";
    //    }
    //    else if (CropYear == "2015-16" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "67.60";
    //    }
    //    else if (CropYear == "2014-15" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "61.40";
    //    }
    //    else if (CropYear == "2013-14" && (CmdId == "13" || CmdId == "8" || CmdId == "11" || CmdId == "12" || CmdId == "3"))
    //    {
    //        RatePM = "58.40";
    //    }
    //    //Sugar
    //    else if (CmdId == "46" || CmdId == "23")
    //    {
    //        //RatePM = "104.20";
    //        RatePM = "137";
    //    }
    //    //Salt
    //    else if (CmdId == "19" || CmdId == "122")
    //    {
    //        //RatePM = "104.20";
    //        RatePM = "100";

    //    }
    //    //Gunny
    //    //else if (CmdId == "25")
    //    //{
    //    //    RatePM = "46";
    //    //}

    //    return RatePM;
    //}
    protected void btnNewBill_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_MPSCSC_SC_Final_Bill_For_Vacant_Capacity.aspx");
    }
    protected void ddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBillData();
    }
    //protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBillData();
    //}
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_MPSCSC_SC_Final_Bill_For_Vacant_Capacity.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetBillData();
    }
}