using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using MPSCSC_WS;

public partial class Region_frm_DelStorage_Charges_Bill_Number_Test : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;

    MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new MPSCSC_WS.MPSCSC_InstituitionStorageBillDetails();
    //CSMS_WS.MPSCSC_InstituitionStorageBillDetails MPSCSCDemo = new CSMS_WS.MPSCSC_InstituitionStorageBillDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        fillddlbillType();
                        //string strMsg = "यहाँ सुविधा कुछ दिनों के लिए सॉफ्टवेयर में कार्य होने कारण बंद कर दी गई हैं |||";


                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Welcome.aspx';", true);
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
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillddlbillType()
    {
        try
        {

            string query = "";

            query = "select distinct Bill_Type from tbl_Institution_Storage_Bill_Details";

            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbillType.Items.Clear();
                ddlbillType.DataSource = ds.Tables[0];
                ddlbillType.DataTextField = "Bill_Type";
                ddlbillType.DataValueField = "Bill_Type";
                ddlbillType.DataBind();
                ddlbillType.Items.Insert(0, "---Select---");
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
    //public void FillGrid()
    //{
    //    string query = "select ISNULL(Fin_Bill_No,0) Fin_Bill_No,SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Institution_Storage_Bill_Details as SB INNER JOIN tbl_MetaData_DISTRICT D ON D.District_Id=SB.District_Id where D.Region_ID='" + Session["Region_ID"].ToString() + "' AND SB.Bill_Number='" + txtBillNumber.Text + "'";

    //    cmd = new SqlCommand(query, con);
    //    da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    Session["ds_GridInfo"] = ds;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gv.DataSource = ds;
    //        gv.DataBind();
    //        hdnFin_Bill_No.Value = ds.Tables[0].Rows[0]["Fin_Bill_No"].ToString();
    //        hdnBill_Type.Value = ds.Tables[0].Rows[0]["Bill_Type"].ToString();
    //        lblRowCount.Text = "";
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        Btn_Delete.Enabled = true;
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Correct Bill Number')", true);
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        gv.DataSource = null;
    //        gv.DataBind();
    //        hdnFin_Bill_No.Value = "0";
    //        Btn_Delete.Visible = false;
    //    }
    //}

    public void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Details_For_Delete_Bill_From_RM_By_Bill_Number", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
                cmd.Parameters.AddWithValue("@BillNumber", txtBillNumber.Text.ToString());
                cmd.Parameters.AddWithValue("@BillType", ddlbillType.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        Session["ds_GridInfo"] = dt;
                        if (dt.Rows.Count > 0)
                        {
                            gv.DataSource = dt;
                            gv.DataBind();
                            hdnFin_Bill_No.Value = dt.Rows[0]["Fin_Bill_No"].ToString();
                            hdnBill_Type.Value = dt.Rows[0]["Bill_Type"].ToString();
                            lblRowCount.Text = "";
                            lblRowCount.Text = "Total No. Of records are : " + dt.Rows.Count.ToString();
                            Btn_Delete.Enabled = true;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया बिल नंबर चेक करे //  उपरोक्त बिल का ब्रांच मैनेजर के द्वारा कटोत्रा कर दिया गया है //  बिल का प्रकार सही सेलेक्ट करे !!')", true);
                            lblRowCount.Text = "Total No. Of records are : " + dt.Rows.Count.ToString();
                            gv.DataSource = null;
                            gv.DataBind();
                            hdnFin_Bill_No.Value = "0";
                            Btn_Delete.Visible = false;
                        }
                    }
                }
            }
        }
    }
    //protected void txtBillNumber_TextChanged(object sender, EventArgs e)
    //{
    //    if (Session["UserName"] != null)
    //    {
    //        if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
    //        {
    //            if (!String.IsNullOrEmpty(txtBillNumber.Text))
    //            {
    //                FillGrid();
    //            }
    //            else
    //            {
    //                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Correct Bill Number'); </script> ");
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("~/SessionExpired.htm");
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("~/SessionExpired.htm");
    //    }
    //}
    protected void Btn_Delete_Click(object sender, EventArgs e)
    
    {
        int count = 0;
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (hdnFin_Bill_No.Value == "0")
            {
                string log_qry = "insert into tbl_Institution_Storage_Bill_Details_Log (Sno,[Bill_Number],[District_Id],[Branch_Id],[Depositor_Type_Id],[Depositor_Id],[Commodity_Type_Id],[Commodity_Id],[From_Date],[To_Date],[Packing_Type],[Weight],[Financial_Year],[Commodity_Rate],[Net_Amount],[Sub_Amount],[Service_Tax_Perc],[Service_Tax_Amt],[Depositor_Category],[Rebate_Perc],[Rebate_Amt],[Rebate_on_Unit],[Khasra_Number],[Rin_Pustika_No],[Cast_Certificate_No],[Is_Rebate],[Created_Date],[Modified_Date],[Client_IP],[Per_Day_Rate],[Bill_Type],[BId],[Month],[Godown_Id],[Crop_Year],[UpdatedBy],[UpdatedDate],DeletedBy,DeletedDate,[BO_Approval_Status],[BO_Approval_Date],[BO_Approval_IP],[RO_Approval_Status],[RO_Approval_Date],[RO_Approval_IP],[HO_Approval_Status],[HO_Approval_Date],[HO_Approval_IP],MPWLC_SC,GST_Perc_SC,GST_Amt_SC,Fin_Bill_No,Delete_Flag)" +
                                            "select D.Sno,D.[Bill_Number],D.[District_Id],D.[Branch_Id],D.[Depositor_Type_Id],D.[Depositor_Id],D.[Commodity_Type_Id],D.[Commodity_Id],D.[From_Date],D.[To_Date],D.[Packing_Type],D.[Weight],D.[Financial_Year],D.[Commodity_Rate],D.[Net_Amount],D.[Sub_Amount],D.[Service_Tax_Perc],D.[Service_Tax_Amt],D.[Depositor_Category],D.[Rebate_Perc],D.[Rebate_Amt],D.[Rebate_on_Unit],D.[Khasra_Number],D.[Rin_Pustika_No],D.[Cast_Certificate_No],D.[Is_Rebate],D.[Created_Date],D.[Modified_Date],D.[Client_IP],D.[Per_Day_Rate],D.[Bill_Type],D.[BId],D.[Month],D.[Godown_Id],D.[Crop_Year],D.[UpdatedBy],D.[UpdatedDate],'" + ip + "',getdate(),D.[BO_Approval_Status],D.[BO_Approval_Date],D.[BO_Approval_IP],D.[RO_Approval_Status],D.[RO_Approval_Date],D.[RO_Approval_IP],D.[HO_Approval_Status],D.[HO_Approval_Date],D.[HO_Approval_IP],D.MPWLC_SC,GST_Perc_SC,D.GST_Amt_SC,D.Fin_Bill_No,'R' from tbl_Institution_Storage_Bill_Details D " +
                                            "LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC IC ON IC.Ref_Bill_No=D.Fin_Bill_No " +
                                            "where D.Bill_Number='" + txtBillNumber.Text + "' and IC.Ref_Bill_No IS NULL";
                cmd = new SqlCommand(log_qry, con);
                int s = cmd.ExecuteNonQuery();

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                //Web Service Call For data to MPSCSC
                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                MPSCSCDemo.EDAddInstitutionStorageBillDetailsInLogDeletebyBillNo(txtBillNumber.Text.ToString(), ip);

                if (s > 0)
                {
                    qry = "Delete BD from tbl_Institution_Storage_Bill_Details BD LEFT JOIN MPSCSC.dbo.Digitally_Sign_StorageBill_IC as CDS ON BD.Bill_Number=CDS.Ref_Bill_No WHERE BD.Bill_Number='" + txtBillNumber.Text + "' AND CDS.Ref_Bill_No IS NULL";

                    cmd = new SqlCommand(qry, con);
                    int d = cmd.ExecuteNonQuery();
                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    //Web Service Call For data to MPSCSC
                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                    MPSCSCDemo.EDDeleteInstitutionStorageBillDetailsbyBillNo(txtBillNumber.Text.ToString());
                    count++;
                    if (d > 0)
                    {
                        count++;
                        string Qrys = "";
                        if (hdnBill_Type.Value == "AD")
                        {
                            Qrys = "insert into tbl_Bill_Institution_Daily_Charges_Log(Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],DeletedBy,DeletedDate) SELECT Id,[Bill_Number],[Dates],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],'" + ip + "',getdate() FROM [tbl_Bill_Institution_Daily_Charges] where Bill_Number='" + txtBillNumber.Text + "'";

                            cmd = new SqlCommand(Qrys, con);
                            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                            //Web Service Call For data to MPSCSC
                            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                            MPSCSCDemo.EDAddInstitutionBillDailyChargesInLogByBillNo(txtBillNumber.Text.ToString(), ip);
                            int x = cmd.ExecuteNonQuery();
                            if (x > 0)
                            {
                                count++;
                                qry = "Delete from tbl_Bill_Institution_Daily_Charges where Bill_Number='" + txtBillNumber.Text + "'";

                                cmd = new SqlCommand(qry, con);
                                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                //Web Service Call For data to MPSCSC
                                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
                                MPSCSCDemo.EDDeleteInstitutionStorageDailyBillCharges(txtBillNumber.Text.ToString());

                                //End API to MPSCSC
                                int y = cmd.ExecuteNonQuery();
                                if (y > 0)
                                {
                                    count++;
                                    Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log ([AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],DeletedBy,DeletedDate,[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No]) SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Ref_Bill_No='" + txtBillNumber.Text + "' ";

                                    cmd = new SqlCommand(Qrys, con);
                                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                    //MPSCSCDemo.EDAddGodownRentDeductionLogByBillNo(txtBillNumber.Text.ToString(), ip);
                                    int z = cmd.ExecuteNonQuery();
                                    if (z > 0)
                                    {
                                        count++;
                                        qry = " delete from tbl_Godown_Rent_Deduction_Amount where Ref_Bill_No='" + txtBillNumber.Text + "' ";

                                        cmd = new SqlCommand(qry, con);
                                        cmd.ExecuteNonQuery();
                                        //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                        //MPSCSCDemo.EDDeleteGodownRentDeductionAmountByBillNo(txtBillNumber.Text.ToString());
                                    }
                                }
                            }
                        }
                        else if (hdnBill_Type.Value == "GR")
                        {
                            //Qrys = "insert into tbl_Bill_PVT_Godown_Daily_Rent_log SELECT [Bill_Number],[Commodity_Id],[WHR_No],[Deposit_Date],[Deposit_Bags],[Delivery_Date],[Deliver_Bags],[Monthly_Rate],[Per_Day_Rate],[Total_Charges],[Rebate_Bags],[Rebate_Per],[Rebate_Amt],[STax_Per],[STax_Amt],[Net_Amount],[Created_Date],[Modified_Date],[Client_IP],[Godown_Id],[Deposit_Weight],[Deliver_Weight],[SP_In_MM],[SP_In_DD],[AP_In_MM],[AP_In_DD] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Bill_PVT_Godown_Daily_Rent] where Bill_Number='" + Bill_No + "'";
                            Qrys = "insert into tbl_Bill_PVT_Godown_Daily_Rent_log(Id,[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],DeletedBy,DeletedDate) SELECT Id,[Bill_Number],[Dates],[Opening_Weight],[Receive_Weight],[Issue_Weight],[Closing_Weight],[Opening_Balance],[Receive_Bags],[Issue_Bags],[Closing_Bag_Balance],[Per_Day_Rate],[Total_Charges],[Created_Date],[Modified_Date],'" + ip + "',getdate() FROM [tbl_Bill_PVT_Godown_Daily_Rent] where Bill_Number='" + txtBillNumber.Text + "'";

                            cmd = new SqlCommand(Qrys, con);
                            int x = cmd.ExecuteNonQuery();
                            //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                            //MPSCSCDemo.EDAddPrivateGodownDailyRentInLog(txtBillNumber.Text.ToString(), ip);
                            if (x > 0)
                            {
                                count++;
                                qry = "Delete from tbl_Bill_PVT_Godown_Daily_Rent where Bill_Number='" + txtBillNumber.Text + "'";

                                cmd = new SqlCommand(qry, con);
                                int y = cmd.ExecuteNonQuery();
                                //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                //MPSCSCDemo.EDDeletePvtBillGodownDailyRentAmount(txtBillNumber.Text.ToString());
                                if (y > 0)
                                {
                                    count++;
                                    Qrys = "insert into tbl_Godown_Rent_Deduction_Amount_Log([AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],DeletedBy,DeletedDate,[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No]) SELECT [AID],[Bill_No],[District_Id],[Branch_Id],[Godown_Id],[Dunnage_PS_Amt],[Elect_Beam_Scale_Amt],[Wooden_Planke_Amt],[Ana_Kit_Set_Amt],[Fumigation_Cover_Amt],[Fire_Extinguisher_Amt],[Fire_Buckets_Amt],[Security_Guard_Amt],[Sprey_Pump_Amt],[Digital_Moisture_Meter_Amt],[Fumigation_Emloyee_Amt],[Common_Facility_Amt],[Insectiside_Cost],[TResources_Deduct_Amt],[TBill_Deduct_Amt],[TBill_Amount],[Net_Bill_Amount],[CreatedBy],[CreatedDate],[UpdateBy],[UpdatedDate],'" + ip + "',getdate(),[Godown_Closing_Balance],[Total_Stock_Deposit],[Ref_Bill_No] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Godown_Rent_Deduction_Amount] where Bill_No='" + txtBillNumber.Text + "'";

                                    cmd = new SqlCommand(Qrys, con);
                                    int z = cmd.ExecuteNonQuery();
                                    //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                    //MPSCSCDemo.EDAddGodownRentDeductionLogByBillNo(txtBillNumber.Text.ToString(), ip);
                                    if (z > 0)
                                    {
                                        count++;
                                        qry = " delete from tbl_Godown_Rent_Deduction_Amount where Bill_No='" + txtBillNumber.Text + "'";

                                        cmd = new SqlCommand(qry, con);
                                        cmd.ExecuteNonQuery();
                                        //System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                                        //MPSCSCDemo.EDDeleteGodownRentDeductionAmountByBillNo(txtBillNumber.Text.ToString());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Delete DSC First...')", true);
                    }
                }

            }
            else
            {
                lbl_notfound.Visible = true;
                lbl_notfound.Text = "इस स्टोरेज बिल " + txtBillNumber.Text + " का फाइनल बिल बन चुका है, कृपया इसका पहले फाइनल बिल डिलीट करवाएं|";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('इस स्टोरेज बिल '" + txtBillNumber.Text + "' का फाइनल बिल बन चुका है,कृपया इसका पहले फाइनल बिल डिलीट करवाएं|')", true);
            }
            if (count > 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid();
                txtBillNumber.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
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

    protected void ddlbillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                if (!String.IsNullOrEmpty(txtBillNumber.Text))
                {
                    FillGrid();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Correct Bill Number'); </script> ");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
}