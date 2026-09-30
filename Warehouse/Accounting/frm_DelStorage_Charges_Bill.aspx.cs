using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_frm_DelStorage_Charges_Bill : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            //string region = Session["Region_ID"].ToString();
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        fillDistrict();
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }
    public void FillGrid()
    {
        //string query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "'";
        string query = "select ISNULL(Fin_Bill_No,0) Fin_Bill_No,SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "'";

        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Bill Data Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        //string Stackid = "";
        int count = 0;
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //int i = 0;
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string Bill_No = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string Bill_Type = gv.Rows[gr2.RowIndex].Cells[2].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    HiddenField hdnFin_Bill_No = (HiddenField)(gr2.FindControl("hdnFin_Bill_No"));
                    if (string.IsNullOrEmpty(hdnFin_Bill_No.Value))
                    {
                        hdnFin_Bill_No.Value = "0";
                    }
                    if (chk_Delete.Checked == true)
                    {
                        if (hdnFin_Bill_No.Value == "0")
                        {
                            ds = (DataSet)Session["ds_GridInfo"];
                            foreach (DataRow drs in ds.Tables[0].Select("Bill_Number = '" + Bill_No + "'"))
                            {
                                {
                                    ////////////////tbl_Storage_Bill_Details_Log/////////////////////////
                                    //string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP] from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";                    
                                    //string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";    
                                    //string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null)";

                                    string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
                                        "select Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],'127.0.0.1',getdate(),[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and Bill_Number not in (select CDS.Ref_Bill_No " +
                                        "from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null)";

                                    cmd = new SqlCommand(log_qry, con);
                                    int s = cmd.ExecuteNonQuery();
                                    ////////////////tbl_Storage_Bill_Details_Log/////////////////////////
                                    if (s > 0)
                                    {
                                        //qry = "Delete from tbl_Storage_Bill_Details where Bill_Number='" + Bill_No + "'";
                                        //qry = "Delete from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and BO_Approval_Status is null";
                                        qry = "Delete from tbl_Institution_Storage_Bill_Details where Bill_Number='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "' and Bill_Number not in (select CDS.Ref_Bill_No from MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and CDS.Ref_Bill_No is not null)";

                                        cmd = new SqlCommand(qry, con);
                                        int d = cmd.ExecuteNonQuery();
                                        if (d > 0)
                                        {
                                            string Qrys = "";
                                            if (Bill_Type == "AD")
                                            {
                                                //Qrys = "insert into tbl_Bills_Daily_Storage_Charges_Details_Log SELECT [Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight] FROM [tbl_Bills_Daily_Storage_Charges_Details] where Bill_Number='" + Bill_No + "'";
                                                Qrys = "insert into tbl_Bill_Institution_Daily_Charges_Log SELECT Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + ip + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges] where Bill_Number='" + Bill_No + "'";

                                                cmd = new SqlCommand(Qrys, con);
                                                int x = cmd.ExecuteNonQuery();
                                                if (x > 0)
                                                {
                                                    //qry = "Delete from tbl_Bills_Daily_Storage_Charges_Details where Bill_Number='" + Bill_No + "'";
                                                    qry = "Delete from tbl_Bill_Institution_Daily_Charges where Bill_Number='" + Bill_No + "'";

                                                    cmd = new SqlCommand(qry, con);
                                                    int y = cmd.ExecuteNonQuery();
                                                    if (y > 0)
                                                    {
                                                        //Deduction
                                                        //Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                                        Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Ref_Bill_No='" + Bill_No + "'";

                                                        cmd = new SqlCommand(Qrys, con);
                                                        int z = cmd.ExecuteNonQuery();
                                                        if (z > 0)
                                                        {
                                                            //qry = "Delete from tbl_Bill_Institution_Daily_Charges where Bill_Number='" + Bill_No + "'";
                                                            //qry = " delete from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                                            qry = " delete from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + Bill_No + "'";

                                                            cmd = new SqlCommand(qry, con);
                                                            cmd.ExecuteNonQuery();
                                                        }
                                                        //
                                                    }
                                                }

                                            }
                                            else if (Bill_Type == "GR")
                                            {
                                                //Qrys = "insert into tbl_Bill_PVT_Godown_Daily_Rent_log SELECT [Bill_Number],[Commodity_Id],[WHR_No],[Deposit_Date],[Deposit_Bags],[Delivery_Date],[Deliver_Bags],[Monthly_Rate],[Per_Day_Rate],[Total_Charges],[Rebate_Bags],[Rebate_Per],[Rebate_Amt],[STax_Per],[STax_Amt],[Net_Amount],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Deposit_Weight],[Deliver_Weight],[SP_In_MM],[SP_In_DD],[AP_In_MM],[AP_In_DD] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Bill_PVT_Godown_Daily_Rent] where Bill_Number='" + Bill_No + "'";
                                                Qrys = "insert into tbl_Bill_PVT_Godown_Daily_Rent_log SELECT Id,[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],'" + ip + "',getdate() FROM [tbl_Bill_PVT_Godown_Daily_Rent] where Bill_Number='" + Bill_No + "'";

                                                cmd = new SqlCommand(Qrys, con);
                                                int x = cmd.ExecuteNonQuery();
                                                if (x > 0)
                                                {
                                                    //qry = "Delete from tbl_Accrued_Storage_Charges_WHR_Details where Bill_Number='" + Bill_No + "'";
                                                    qry = "Delete from tbl_Bill_PVT_Godown_Daily_Rent where Bill_Number='" + Bill_No + "'";

                                                    cmd = new SqlCommand(qry, con);
                                                    int y = cmd.ExecuteNonQuery();
                                                    if (y > 0)
                                                    {
                                                        //Deduction
                                                        //Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                                        Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Bill_No='" + Bill_No + "'";

                                                        cmd = new SqlCommand(Qrys, con);
                                                        int z = cmd.ExecuteNonQuery();
                                                        if (z > 0)
                                                        {
                                                            //qry = "Delete from tbl_Bill_Institution_Daily_Charges where Bill_Number='" + Bill_No + "'";
                                                            //qry = " delete from tbl_Godown_Rent_Deduction_Amount where Bill_No='" + Bill_No + "' and Branch_Id='" + ddlDepotList.SelectedValue + "'";
                                                            qry = " delete from tbl_Godown_Rent_Deduction_Amount where Bill_No='" + Bill_No + "'";

                                                            cmd = new SqlCommand(qry, con);
                                                            cmd.ExecuteNonQuery();
                                                        }
                                                        //
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Log Storage')", true);
                                    }
                                }
                            }
                            count++;
                        }
                        else
                        {
                            lbl_notfound.Visible = true;
                            lbl_notfound.Text = "इस स्टोरेज बिल " + Bill_No + " का फाइनल बिल बन चुका है, कृपया इसका पहले फाइनल बिल डिलीट करवाएं|";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इस स्टोरेज बिल '" + Bill_No + "' का फाइनल बिल बन चुका है,कृपया इसका पहले फाइनल बिल डिलीट करवाएं|')", true);
                            break;
                        }
                    }
                    //i = i + 1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}