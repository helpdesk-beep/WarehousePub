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
using System.Text;
using System.Collections.Generic;

public partial class Inspections_BO_InspOfficer_FillOverall_PVInsp : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            lblinspid.Text = Session["SInspID"].ToString();
            FatchScheduleInspData();
            FatchInspData();
            FatchPreviousfilldata();
            chkfinalsub();
            txtofficername.Attributes.Add("readonly", "readonly");
           // txtlincenceno.Attributes.Add("readonly", "readonly");
            txtinspectiondate.Attributes.Add("readonly", "readonly");
            txtexdate.Attributes.Add("readonly", "readonly");
            txtinspexdatefrom.Attributes.Add("readonly", "readonly");
            txtinspexdateto.Attributes.Add("readonly", "readonly");
            txtpriciusinspdate.Attributes.Add("readonly", "readonly");
            txtskandhdatefrom.Attributes.Add("readonly", "readonly");
            txtskandhdateto.Attributes.Add("readonly", "readonly");
            txtskandhletterdatefrom.Attributes.Add("readonly", "readonly");
            txtskandhletterdateto.Attributes.Add("readonly", "readonly");
            txtwarehouserecieptdate.Attributes.Add("readonly", "readonly");
        }
    }
    public void FatchScheduleInspData()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Insp_GetInspection_Schedule_For_Inspection_Officer", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", lblinspid.Text.ToString());
            //cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                txtofficername.Text = dt.Rows[0]["Officer_Name"].ToString().Trim();
                txtlincenceno.Text = dt.Rows[0]["Licence_No"].ToString().Trim();
                txtexdate.Text = dt.Rows[0]["Licence_Exp_Date"].ToString().Trim();
                txtinspexdatefrom.Text = dt.Rows[0]["Observation_Date_From"].ToString().Trim();
                txtinspexdateto.Text = dt.Rows[0]["Observation_Date_To"].ToString().Trim();
                txtpriciusinspdate.Text = dt.Rows[0]["Date_of_last_inspection"].ToString().Trim();
                txtpriveusinspofficername.Text = dt.Rows[0]["Name_of_previous_inspection_officer"].ToString().Trim();
                ddl_Desig.SelectedValue = dt.Rows[0]["previous_inspection_officer_Designation"].ToString().Trim();
                txtskandhsno.Text = dt.Rows[0]["Skandh_Application_No"].ToString().Trim();
                txtskandhdatefrom.Text = dt.Rows[0]["Skandh_Application_No_Date"].ToString().Trim();
                txtskandhsnoto.Text = dt.Rows[0]["Skandh_Application_No_2"].ToString().Trim();
                txtskandhdateto.Text = dt.Rows[0]["Skandh_Application_No_2_Date"].ToString().Trim();
                txtskandhpaymentleetersno.Text = dt.Rows[0]["Payment_Letter_No"].ToString().Trim();
                txtskandhletterdatefrom.Text = dt.Rows[0]["Payment_Letter_Date"].ToString().Trim();
                txtskandhpaymentleetersnoto.Text = dt.Rows[0]["Payment_Letter_No_2"].ToString().Trim();
                txtskandhletterdateto.Text = dt.Rows[0]["Payment_Letter_No_2_Date"].ToString().Trim();
                txtlastwarehousereciept.Text = dt.Rows[0]["Last_Ware_House_Receipt_no"].ToString().Trim();
                txtwarehouserecieptdate.Text = dt.Rows[0]["Last_Ware_House_Receipt_Date"].ToString().Trim();
                txtschemebore.Text = dt.Rows[0]["EIESC_Scheme"].ToString().Trim();
                txtamount.Text = dt.Rows[0]["Amount"].ToString().Trim();
                txtaprilprofitstatus.Text = dt.Rows[0]["Profit_status_inspection"].ToString().Trim();
                txtwarerecieptdetaisl.Text = dt.Rows[0]["Warehouse_Receipt_Details"].ToString().Trim();
                lblbranch.Text = dt.Rows[0]["Depotname"].ToString().Trim();
                lblinsptype.Text = dt.Rows[0]["Insp_Type"].ToString().Trim();
                lblInspPeriod.Text = dt.Rows[0]["Insp_Period"].ToString().Trim();
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public void FatchInspData()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Godown_Detilas_on_Branches", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@godownID", lblinspid.Text.ToString());
           // cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                ddlavakyesno.SelectedValue = dt.Rows[0]["Is_Incoming_Rregister"].ToString().Trim();
                txtjavakpanji.Text = dt.Rows[0]["Is_Outward_Register"].ToString().Trim();
                ddlsthanidakbook.SelectedValue = dt.Rows[0]["Localpostalbook"].ToString().Trim();
                ddlstashnar.SelectedValue = dt.Rows[0]["Stationery_register"].ToString().Trim();
                ddldedstock.SelectedValue = dt.Rows[0]["Dead_stock_register"].ToString().Trim();
                ddlGHO.SelectedValue = dt.Rows[0]["Is_Pandingat_HO_RO"].ToString().Trim();
                txtgodamdistance.Text = dt.Rows[0]["How_Many_Circumference"].ToString().Trim();
                ddlA.SelectedValue = dt.Rows[0]["From_the_point_of_view_of_scientific_storage"].ToString().Trim();
                if(ddlA.SelectedValue=="1")
                {

                }
                else if (ddlA.SelectedValue == "2")
                {
                    showtxtA.Visible = true;
                    txtA.Text = dt.Rows[0]["Remark_A"].ToString().Trim();
                }
                if (ddlB.SelectedValue == "1")
                {

                }
                else if(ddlB.SelectedValue == "2")
                {
                    txtB.Text = dt.Rows[0]["Remark_B"].ToString().Trim();
                    showtxtB.Visible = true;
                }
                if (ddlC.SelectedValue == "1")
                {

                }
                else if (ddlC.SelectedValue == "2")
                {
                    txtC.Text = dt.Rows[0]["Remark_C"].ToString().Trim();
                    showtxtC.Visible = true;
                }
                ddlB.SelectedValue = dt.Rows[0]["Commercially"].ToString().Trim();
                ddlC.SelectedValue = dt.Rows[0]["From_the_point_of_view_of_surveillance_and_security"].ToString().Trim();
                ddl2.SelectedValue = dt.Rows[0]["Is_the_storage_of_warehouse_register_updated_systematically"].ToString().Trim();
                ddl3.SelectedValue = dt.Rows[0]["Is_the_MOW_log_book_updated_in_a_systematic_manner"].ToString().Trim();
                txt4.Text = dt.Rows[0]["LOW__recommended_for_low_level_construction_shortage_and_evacuation_due_to_any_reason"].ToString().Trim();
                txtcleimremark.Text = dt.Rows[0]["insurance_company_is_claimed"].ToString().Trim();
                ddl8.SelectedValue = dt.Rows[0]["Is_the_required_corridor_left_between_each_steak"].ToString().Trim();
                ddl9.SelectedValue = dt.Rows[0]["Is_Has_the_steak_been_planned_in_the_warehouse"].ToString().Trim();
                ddl10.SelectedValue = dt.Rows[0]["Is_Has_the_steak_been_prepared_in_a_systematic_and_scientific_manner"].ToString().Trim();
                ddl11.SelectedValue = dt.Rows[0]["Is_entries_marked_on_both_sides"].ToString().Trim();
                ddl12.SelectedValue = dt.Rows[0]["Is_the_steak_register_maintained_properly"].ToString().Trim();
                ddl13.SelectedValue = dt.Rows[0]["Is_regular_insecticide_mistreatment_done_in_time_to_keep_the_wing_free_of_pests"].ToString().Trim();
                ddl14.SelectedValue = dt.Rows[0]["Is_the_wing_sampled_at_the_time_of_submission"].ToString().Trim();
                ddl15.SelectedValue = dt.Rows[0]["Is_the_sample_register_been_updated"].ToString().Trim();
                ddl16.SelectedValue = dt.Rows[0]["weight_Remark"].ToString().Trim();
                ddl17.SelectedValue = dt.Rows[0]["Is_signature_authorization_letter_are_properly_maintained"].ToString().Trim();
                ddl18.SelectedValue = dt.Rows[0]["Is_warehouse_receipt"].ToString().Trim();
                ddl19.SelectedValue = dt.Rows[0]["Is_original_warehouse_receipt_from_depositor_full_payment_canceled_along_with_the_relevant_payment_sheet"].ToString().Trim();
                ddl20.SelectedValue = dt.Rows[0]["Is_as_per_the_warehouse_receipt"].ToString().Trim();
                ddl21.SelectedValue = dt.Rows[0]["Is_stock_submission_and_payment_done_properly_and_correctly"].ToString().Trim();
                ddl22.SelectedValue = dt.Rows[0]["Is_warehouse_receipt_deposit_applications_as_per_instructions"].ToString().Trim();
                ddl23.SelectedValue = dt.Rows[0]["Is_Whether_the_quantity_remaining_stockasshownonthe_Warehouse_Receipt_matches_the_total_balance"].ToString().Trim();
                ddl24.SelectedValue = dt.Rows[0]["Is_warehouse_receipt_holders_updated_in_register_entries"].ToString().Trim();
                ddl25.SelectedValue = dt.Rows[0]["important_documents_safe_or_not"].ToString().Trim();
                ddl26.SelectedValue = dt.Rows[0]["keys_deposited_in_the_bank_by_the_branch_manager"].ToString().Trim();
                ddl27.SelectedValue = dt.Rows[0]["Is_adequate_arrangement_care_protection_stock_and_warehouses_stored_satisfactory"].ToString().Trim();
                ddl28.SelectedValue = dt.Rows[0]["Whether_or_not_branch_manager_has_cordial_relationship_with_the_depositors"].ToString().Trim();
                ddl29.SelectedValue = dt.Rows[0]["Department_wise_full_justifiable_statement_of_requirements"].ToString().Trim();
                ddl30.SelectedValue = dt.Rows[0]["Is_Suggestions_of_Inspection_Officer"].ToString().Trim();
                ddl31.SelectedValue = dt.Rows[0]["Is_Observation_officer_comments"].ToString().Trim();
                Button2.Visible = false;

                Label86.Visible = true;
                Label86.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    public void checkvalidation()
    {
        if (txtlincenceno.Text == "" || txtlincenceno.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Lincence No.!....')", true);
            txtlincenceno.Focus();
            return;
        }
        else if (txtexdate.Text == "" || txtexdate.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Expiry Date!....')", true);
            txtexdate.Focus();
            return;
        }

    }
    protected void btnsaveprofile_Click(object sender, EventArgs e)
    {

        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Inspection_Details_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
                cmd.Parameters.AddWithValue("@PF_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Licence_No", txtlincenceno.Text);
                cmd.Parameters.AddWithValue("@Licence_Exp_Date", getDate_MDY(txtexdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@Observation_Date_From", getDate_MDY(txtinspexdatefrom.Text.Trim()));
                cmd.Parameters.AddWithValue("@Observation_Date_To", getDate_MDY(txtinspexdateto.Text.Trim()));
                cmd.Parameters.AddWithValue("@Date_of_last_inspection", getDate_MDY(txtpriciusinspdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@Name_of_previous_inspection_officer", txtpriveusinspofficername.Text);
                cmd.Parameters.AddWithValue("@previous_inspection_officer_Designation", ddl_Desig.SelectedValue);
                cmd.Parameters.AddWithValue("@Skandh_Application_No", txtskandhsno.Text);
                cmd.Parameters.AddWithValue("@Skandh_Application_No_Date", getDate_MDY(txtskandhdatefrom.Text.Trim()));
                cmd.Parameters.AddWithValue("@Skandh_Application_No_2", txtskandhsnoto.Text);
                cmd.Parameters.AddWithValue("@Skandh_Application_No_2_Date", getDate_MDY(txtskandhdateto.Text.Trim()));
                cmd.Parameters.AddWithValue("@Payment_Letter_No", txtskandhpaymentleetersno.Text);
                cmd.Parameters.AddWithValue("@Payment_Letter_Date", getDate_MDY(txtskandhletterdatefrom.Text.Trim()));
                cmd.Parameters.AddWithValue("@Payment_Letter_No_2", txtskandhpaymentleetersnoto.Text);
                cmd.Parameters.AddWithValue("@Payment_Letter_No_2_Date", getDate_MDY(txtskandhletterdateto.Text.Trim()));
                cmd.Parameters.AddWithValue("@Last_Ware_House_Receipt_no", txtlastwarehousereciept.Text);
                cmd.Parameters.AddWithValue("@Last_Ware_House_Receipt_Date", getDate_MDY(txtwarehouserecieptdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@EIESC_Scheme", txtschemebore.Text);
                cmd.Parameters.AddWithValue("@Amount", txtamount.Text);
                cmd.Parameters.AddWithValue("@Profit_status_inspection", txtaprilprofitstatus.Text);
                cmd.Parameters.AddWithValue("@Warehouse_Receipt_Details", txtwarerecieptdetaisl.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", IPAddress);

                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Employee Inspection Details Successfully submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Button3.Visible = false;
                    pnlofferpopup.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }

    protected void btnsaveGodownDetilasonBranches_Click(object sender, EventArgs e)
    {

        try
        {
          //  checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Godown_Detilas_on_Branches_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                //cmd.Parameters.AddWithValue("@TransID", Session["Godown_ID"].ToString());
                cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
                cmd.Parameters.AddWithValue("@Officer_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@District_ID", Session["SInsp_DisID"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["SInsp_BranchID"].ToString());
                cmd.Parameters.AddWithValue("@Is_Incoming_Rregister", ddlavakyesno.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Outward_Register", txtjavakpanji.Text);
                cmd.Parameters.AddWithValue("@Localpostalbook", ddlsthanidakbook.SelectedValue);
                cmd.Parameters.AddWithValue("@Stationery_register", ddlstashnar.SelectedValue);
                cmd.Parameters.AddWithValue("@Dead_stock_register", ddldedstock.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Pandingat_HO_RO", ddlGHO.SelectedValue);
                cmd.Parameters.AddWithValue("@How_Many_Circumference", txtgodamdistance.Text);
                cmd.Parameters.AddWithValue("@From_the_point_of_view_of_scientific_storage", ddlA.SelectedValue);
                cmd.Parameters.AddWithValue("@Commercially", ddlB.SelectedValue);
                cmd.Parameters.AddWithValue("@From_the_point_of_view_of_surveillance_and_security", ddlC.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark_A", txtA.Text);
                cmd.Parameters.AddWithValue("@Remark_B", txtB.Text);
                cmd.Parameters.AddWithValue("@Remark_C", txtC.Text);
                cmd.Parameters.AddWithValue("@Is_the_storage_of_warehouse_register_updated_systematically", ddl2.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_the_MOW_log_book_updated_in_a_systematic_manner", ddl3.SelectedValue);
                cmd.Parameters.AddWithValue("@LOW__recommended_for_low_level_construction_shortage_and_evacuation_due_to_any_reason", txt4.Text);
                cmd.Parameters.AddWithValue("@insurance_company_is_claimed", txtcleimremark.Text);
                cmd.Parameters.AddWithValue("@Is_the_required_corridor_left_between_each_steak", ddl8.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Has_the_steak_been_planned_in_the_warehouse", ddl9.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Has_the_steak_been_prepared_in_a_systematic_and_scientific_manner", ddl10.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_entries_marked_on_both_sides", ddl11.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_the_steak_register_maintained_properly", ddl12.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_regular_insecticide_mistreatment_done_in_time_to_keep_the_wing_free_of_pests", ddl13.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_the_wing_sampled_at_the_time_of_submission", ddl14.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_the_sample_register_been_updated", ddl15.SelectedValue);
                cmd.Parameters.AddWithValue("@weight_Remark", ddl16.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_signature_authorization_letter_are_properly_maintained", ddl17.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_warehouse_receipt", ddl18.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_original_warehouse_receipt_from_depositor_full_payment_canceled_along_with_the_relevant_payment_sheet", ddl19.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_as_per_the_warehouse_receipt", ddl20.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_stock_submission_and_payment_done_properly_and_correctly", ddl21.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_warehouse_receipt_deposit_applications_as_per_instructions", ddl22.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Whether_the_quantity_remaining_stockasshownonthe_Warehouse_Receipt_matches_the_total_balance", ddl23.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_warehouse_receipt_holders_updated_in_register_entries", ddl24.SelectedValue);
                cmd.Parameters.AddWithValue("@important_documents_safe_or_not", ddl25.SelectedValue);
                cmd.Parameters.AddWithValue("@keys_deposited_in_the_bank_by_the_branch_manager", ddl26.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_adequate_arrangement_care_protection_stock_and_warehouses_stored_satisfactory", ddl27.SelectedValue);
                cmd.Parameters.AddWithValue("@Whether_or_not_branch_manager_has_cordial_relationship_with_the_depositors", ddl28.SelectedValue);
                cmd.Parameters.AddWithValue("@Department_wise_full_justifiable_statement_of_requirements", ddl29.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Suggestions_of_Inspection_Officer", ddl30.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Observation_officer_comments", ddl31.SelectedValue);
                cmd.Parameters.AddWithValue("@CreatedBy", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Inspection Details Successfully submitted |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Button2.Visible = false;

                    Label86.Visible = true;
                    Label86.Text = "यह जानकारी भरी जा चुकी है ।";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string transid = "";
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            a_id = ChkInspAID(lblinspid.Text.Trim());
            transid = lblinspid.Text.Trim().Substring(0, 5) + a_id;
            string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate],[Chml_Avl])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE(),'Y')";
            SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
            int CT2 = 0;
            CT2 = cmd2.ExecuteNonQuery();
            if (CT2 == 1)
            {
                sqltran.Commit();
                Button3.Visible = false;
                pnlofferpopup.Visible = true;
                ModalPopupExtender1.Show();
                //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    public int ChkInspAID(string InspID)
    {
        int MaxAID = 0;
        string QueryMax = "select MAX(AID) as AID from tbl_PVInspection_Fumigation_Details where Officer_ID='" + PFID + "' ";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int a = Convert.ToInt32(str3.ToString());
            MaxAID = a + 1;
        }
        else
        {
            MaxAID = 1;
        }
        return MaxAID;
    }

    public int ChkInspAIDCash(string InspID)
    {
        int MaxAID = 0;
        string QueryMax = "select MAX(AID) as AID from tbl_PVInspection_CashDetails where Officer_ID='" + PFID + "' ";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int a = Convert.ToInt32(str3.ToString());
            MaxAID = a + 1;
        }
        else
        {
            MaxAID = 1;
        }
        return MaxAID;
    }

    public int ChkInspAIDemp(string InspID)
    {
        int MaxAID = 0;
        string QueryMax = "select MAX(AID) as AID from tbl_PVInspection_BranchEmploye where Officer_ID='" + PFID + "' ";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int a = Convert.ToInt32(str3.ToString());
            MaxAID = a + 1;
        }
        else
        {
            MaxAID = 1;
        }
        return MaxAID;
    }
    public int ChkInspAIDCPT(string InspID)
    {
        int MaxAID = 0;
        string QueryMax = "select MAX(AID) as AID from tbl_PVInspection_CPT_UTL where Officer_ID='" + PFID + "' ";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            int a = Convert.ToInt32(str3.ToString());
            MaxAID = a + 1;
        }
        else
        {
            MaxAID = 1;
        }
        return MaxAID;
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            string QueryMax = "select TransID from tbl_PVInspection_Fumigation_Details where Officer_ID='" + PFID + "' and Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
            string str3 = cmd.ExecuteScalar().ToString();
            if ((str3 != String.Empty) || str3 != "")
            {
                string gdwnqry = "UPDATE [tbl_PVInspection_Fumigation_Details] SET  [Fmg_Details] = 'Y',[Fmg_Owned_TotalNoOfStack] = '" + txtOFMG_TotalStack.Text.Trim() + "',[Fmg_Owned_FmgNoOfStack] = '" + txtOFMG_fmgStack.Text.Trim() + "',[Fmg_Owned_Rem_FmgNoOfStack] = '" + txtOFMG_BalFmgStack.Text.Trim() + "',[Fmg_Owned_FmgChemical] = N'" + txtOFMG_Chml.Text.Trim() + "',[Fmg_Owned_Remark] = N'" + txtOFMG_Remark.Text.Trim() + "',[Fmg_PVTPEG_TotalNoOfStack] = '" + txtPEGFMG_NoOfStakc.Text.Trim() + "',[Fmg_PVTPEG_FmgNoOfStack] = '" + txtPEGFMG_fmgstack.Text.Trim() + "',[Fmg_PVTPEG_Rem_FmgNoOfStack] = '" + txtPEGFMG_Balfmgstack.Text.Trim() + "',[Fmg_PVTPEG_FmgChemical] = N'" + txtPEGFMG_chml.Text.Trim() + "',[Fmg_PVTPEG_Remark] = N'" + txtPEGFMG_Remark.Text.Trim() + "',[Fmg_JVS_TotalNoOfStack] = '" + txtJVSFMG_TotalStack.Text.Trim() + "',[Fmg_JVS_FmgNoOfStack] = '" + txtJVSFMG_fmgstack.Text.Trim() + "',[Fmg_JVS_Rem_FmgNoOfStack] ='" + txtJVSFMG_BalFmgStack.Text.Trim() + "',[Fmg_JVS_FmgChemical] =N'" + txtJVSFMG_chml.Text.Trim() + "',[Fmg_JVS_Remark] = N'" + txtJVSFMG_Remark.Text.Trim() + "',[Fmg_Hired_TotalNoOfStack] = '" + txtHFMG_TotalStack.Text.Trim() + "',[Fmg_Hired_FmgNoOfStack] ='" + txtHFMG_fmgstack.Text.Trim() + "',[Fmg_Hired_Rem_FmgNoOfStack] = '" + txtHFMG_BalmgStack.Text.Trim() + "',[Fmg_Hired_FmgChemical] = N'" + txtHFMG_chml.Text.Trim() + "',[Fmg_Hired_Remark] = '" + txtHFMG_Remark.Text.Trim() + "',[Fmg_CAP_TotalNoOfStack] = '" + txtCAPFMG_TotalStack.Text.Trim() + "',[Fmg_CAP_FmgNoOfStack] = '" + txtCAPFMG_fmgStack.Text.Trim() + "',[Fmg_CAP_Rem_FmgNoOfStack] = '" + txtCAPFMG_BalFmgStack.Text.Trim() + "',[Fmg_CAP_FmgChemical] = N'" + txtCAPFMG_chml.Text.Trim() + "',[Fmg_CAP_Remark] = N'" + txtCAPFMG_Remark.Text.Trim() + "',[Fmg_Other_TotalNoOfStack] = '" + txtOthFMG_TotalStack.Text.Trim() + "',[Fmg_Other_FmgNoOfStack] = '" + txtOthFMG_FmgStack.Text.Trim() + "',[Fmg_Other_Rem_FmgNoOfStack] = '" + txtOthFMG_BalFmgStack.Text.Trim() + "',[Fmg_Other_FmgChemical] = N'" + txtOthFMG_Chml.Text.Trim() + "',[Fmg_Other_Remark] = N'" + txtOthFMG_Remark.Text.Trim() + "',[Fmg_UpdatedBy] = '" + ClientIP + "',[Fmg_UpdatedDate] = GETDATE()  WHERE Inspection_ID='" + Session["Godown_ID"].ToString() + "' and TransID='" + str3 + "'";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    Button7.Visible = false;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            string QueryMax = "select TransID from tbl_PVInspection_Fumigation_Details where Officer_ID='" + PFID + "' and Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
            string str3 = cmd.ExecuteScalar().ToString();
            if ((str3 != String.Empty) || str3 != "")
            {
                string gdwnqry = "UPDATE [tbl_PVInspection_Fumigation_Details] SET [Gdwn_Clsf_Details] = 'Y',[Gdwn_Clsf_Owned_TotalNoOfStack] = '" + txtOCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_Owned_C] = '" + txtOCl_C.Text.Trim() + "',[Gdwn_Clsf_Owned_F] = '" + txtOCl_F.Text.Trim() + "',[Gdwn_Clsf_Owned_H] ='" + txtOCl_H.Text.Trim() + "',[Gdwn_Clsf_Owned_Action] = N'" + txtOCl_Process.Text.Trim() + "',[Gdwn_Clsf_JVS_TotalNoOfStack] = '" + txtJVSCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_JVS_C] = '" + txtJVSCl_F.Text.Trim() + "' ,[Gdwn_Clsf_JVS_F] = '" + txtJVSCl_F.Text.Trim() + "',[Gdwn_Clsf_JVS_H] = '" + txtJVSCl_H.Text.Trim() + "',[Gdwn_Clsf_JVS_Action] = N'" + txtJVSCl_Remark.Text.Trim() + "',[Gdwn_Clsf_PVTPEG_TotalNoOfStack] = '" + txtPEGCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_PVTPEG_C] = '" + txtPEGCl_C.Text.Trim() + "',[Gdwn_Clsf_PVTPEG_F] = '" + txtPEGCl_F.Text.Trim() + "',[Gdwn_Clsf_PVTPEG_H] ='" + txtPEGCl_H.Text.Trim() + "',[Gdwn_Clsf_PVTPEG_Action] = N'" + txtPEGCl_Remark.Text.Trim() + "',[Gdwn_Clsf_Hired_TotalNoOfStack] = '" + txtHCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_Hired_C] = '" + txtHCl_C.Text.Trim() + "',[Gdwn_Clsf_Hired_F] = '" + txtHCl_F.Text.Trim() + "',[Gdwn_Clsf_Hired_H] = '" + txtHCl_H.Text.Trim() + "',[Gdwn_Clsf_Hired_Action] = N'" + txtHCl_Remark.Text.Trim() + "',[Gdwn_Clsf_CAP_TotalNoStack] = '" + txtCAPCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_CAP_C] = '" + txtCAPCl_C.Text.Trim() + "',[Gdwn_Clsf_CAP_F] = '" + txtCAPCl_F.Text.Trim() + "',[Gdwn_Clsf_CAP_H] = '" + txtCAPCl_H.Text.Trim() + "',[Gdwn_Clsf_CAP_Action] = '" + txtCAPCl_Remark.Text.Trim() + "',[Gdwn_Clsf_Other_TotalNoOfStack] = '" + txtOthCl_TotalStack.Text.Trim() + "',[Gdwn_Clsf_Other_C] = '" + txtOthCl_C.Text.Trim() + "',[Gdwn_Clsf_Other_F] = '" + txtOthCl_F.Text.Trim() + "',[Gdwn_Clsf_Other_H] ='" + txtOthCl_H.Text.Trim() + "',[Gdwn_Clsf_Other_Action] = N'" + txtOthCl_Remark.Text.Trim() + "',[Gdwn_Clsf_UpdatedBy] = '" + ClientIP + "',[Gdwn_Clsf_UpdatedDate] = GETDATE()  WHERE Inspection_ID='" + Session["Godown_ID"].ToString() + "' and TransID='" + str3 + "'";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    Button9.Visible = false;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        string transid = "";
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            a_id = ChkInspAIDCash(lblinspid.Text.Trim());
            transid = lblinspid.Text.Trim().Substring(0, 5) + a_id;
            //string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE())";
            string gdwnqry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_PVInspection_CashDetails] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Str_Chrg_MPSC_BilllSubmitedMonth],[Str_Chrg_MPSC_Prapt_Month],[Str_Chrg_MPSC_BillPendingMonth],[Str_Chrg_MPSC_PendingResion],[Str_Chrg_MPSC_Remark],[Str_Chrg_NFD_BillSubmitedMonth],[Str_Chrg_NFD_Prapt_Month],[Str_Chrg_NFD_BillPendingMonth],[Str_Chrg_NFD_PendingResion],[Str_Chrg_NFD_Resion],[Str_Chrg_MFD_BillSubmitedMonth],[Str_Chrg_MFD_Prapt_Month],[Str_Chrg_MFD_BilPendingMonth],[Str_Chrg_MFD_PendingResion],[Str_Chrg_MFD_Remark],[Str_Chrg_Other_BillSumbitedMonth],[Str_Chrg_Other_Prapt_Month],[Str_Chrg_Other_BillPendingMonth],[Str_Chrg_Other_PendingResion],[Str_Chrg_Other_Remark],[Str_Chrg_UpdatedBy],[Str_Chrg_UpdatedDate],[AID],[CreatedBy],[CreatedDate],[Str_Chrg])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtMPSC_Prastut.Text.Trim() + "','" + txtMPSC_Prapti.Text.Trim() + "','" + txtMPSC_Bal.Text.Trim() + "','" + txtMPSC_Reason.Text.Trim() + "',N'" + txtMPSC_Remark.Text.Trim() + "','" + txtNFD_Prastus.Text.Trim() + "','" + txtNFD_Prapti.Text.Trim() + "','" + txtNFD_Bal.Text.Trim() + "','" + txtNFD_Resone.Text.Trim() + "','" + txtNFD_Remark.Text.Trim() + "','" + txtMFD_Prastut.Text.Trim() + "','" + txtMFD_Prapti.Text.Trim() + "','" + txtMFD_Bal.Text.Trim() + "','" + txtMFD_Reasone.Text.Trim() + "',N'" + txtMFD_Remark.Text.Trim() + "','" + txtOth_prastut.Text.Trim() + "','" + txtOth_Prapit.Text.Trim() + "','" + txtOth_Bal.Text.Trim() + "','" + txtOth_Reasone.Text.Trim() + "','" + txtOth_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE(),'Y')";
            SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
            int CT2 = 0;
            CT2 = cmd2.ExecuteNonQuery();
            if (CT2 == 1)
            {
                sqltran.Commit();
                Button11.Visible = false;
                pnlofferpopup.Visible = true;
                ModalPopupExtender1.Show();
                //   ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            string QueryMax = "select TransID from tbl_PVInspection_CashDetails where Officer_ID='" + PFID + "' and Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
            string str3 = cmd.ExecuteScalar().ToString();
            if ((str3 != String.Empty) || str3 != "")
            {
                string gdwnqry = "UPDATE [JointVentureScheme2018].[dbo].[tbl_PVInspection_CashDetails] SET [C_Mukhya_AsPerRecord] = '" + txtM_Recordrashi.Text.Trim() + "',[C_Mukhya_AsPerPV] = '" + txtM_PVRashi.Text.Trim() + "',[C_Mukhya_Diff] ='" + txtM_Anter.Text.Trim() + "',[C_Mukhya_Remark] = '" + txtM_remark.Text.Trim() + "',[C_Insprest_AsPerRecord] = '" + txtS_RecordRashi.Text.Trim() + "',[C_Insprest_AsPerPV] = '" + txtS_PVRashi.Text.Trim() + "',[C_Insprest_Diff] = '" + txtS_Anter.Text.Trim() + "',[C_Insprest_Remark] = N'" + txtS_Remark.Text.Trim() + "',[C_Nirman_AsPerRecord] = '" + txtN_RecordRashi.Text.Trim() + "',[C_Nirman_AsPerPV] = '" + txtN_PVRashi.Text.Trim() + "',[C_Nirman_Diff] = '" + txtN_Anter.Text.Trim() + "',[C_Nirman_Remark] = N'" + txtN_Remark.Text.Trim() + "',[C_Ticket_AsPerRecord] = '" + txtT_RecordRashi.Text.Trim() + "',[C_Ticket_AsPerPV] = '" + txtT_PVRashi.Text.Trim() + "',[C_Ticket_Diff] = '" + txtT_Anter.Text.Trim() + "',[C_Ticket_Remark] = N'" + txtT_Remark.Text.Trim() + "',[C_Other_AsPerRecord] ='" + txtO_RecordRashi.Text.Trim() + "',[C_Other_AsPerPV] = '" + txtO_PVRashi.Text.Trim() + "',[C_Other_Diff] = '" + txtO_Amter.Text.Trim() + "',[C_Other_Remark] = N'" + txtOCash_Remark.Text.Trim() + "' ,[C_UpdatedBy] ='" + ClientIP + "',[C_UpdatedDate] = GETDATE(),[C_Fill]='Y' WHERE Inspection_ID='" + Session["Godown_ID"].ToString() + "' and TransID='" + str3 + "'";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    Button13.Visible = false;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }

    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            string QueryMax = "select TransID from tbl_PVInspection_CashDetails where Officer_ID='" + PFID + "' and Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(QueryMax, conStr, sqltran);
            string str3 = cmd.ExecuteScalar().ToString();
            if ((str3 != String.Empty) || str3 != "")
            {
                string gdwnqry = "UPDATE [JointVentureScheme2018].[dbo].[tbl_PVInspection_CashDetails]   SET [JVS_GdwnBill_Submition] = '" + txtprastutmahtuk.Text + "'  ,[JVS_GdwnBill_BhuktanMonth] = '" + txtbhugtanmahtuk.Text + "' ,[JVS_GdwnBill_PendingResion] =N'" + txtbhuktanlambitkaran.Text + "' ,[JVS_GdwnBill_Katotra1819] =N'" + txtkatotraparichan.Text + "' ,[JVS_GdwnBill_KamiAdhik_Katotra] = N'" + txtlossgainsamayojan.Text + "' ,JVS_Fill='Y'  WHERE Inspection_ID='" + Session["Godown_ID"].ToString() + "' and TransID='" + str3 + "'";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    Button18.Visible = false;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        string transid = "";
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            a_id = ChkInspAIDemp(lblinspid.Text.Trim());
            transid = lblinspid.Text.Trim().Substring(0, 5) + a_id;
            //string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE())";
            string gdwnqry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_PVInspection_BranchEmploye] ([Trans_ID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[BM_NoOFPersone],[BM_InchargeDate],[BM_InchargeYear],[BM_Remark],[AQC_NoOfPersone],[AQC_InchargeDate],[AQC_InchargeYear],[AQC_Remark],[QC_NoOfPersone],[QC_InchargeDate],[QC_InchargeYear],[QC_Remark],[CCH_NoOfPersone],[CCH_InchargeDate],[CCH_InchargeYear],[CCH_Remark],[SthaiKarmi_NoOfPersone],[SthaiKarmi_InchargeDate],[SthaiKarmi_InchargeYear],[SthaiKarmi_Remark],[Danik_NoOfPersone],[Danik_InchargeDate],[Danik_InchargeYear],[Danik_Remark],[AID],[Created_Date],[CreatedBy]) VALUES ('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtBMNoOfPer.Text.Trim() + "','" + getDate_MDY(txtBMInchDate.Text.Trim()) + "','" + txtBMInchYear.Text.Trim() + "',N'" + txtBMRemark.Text.Trim() + "','" + txtAQCNoOfPer.Text.Trim() + "','" + getDate_MDY(txtAQCInchDate.Text.Trim()) + "','" + txtAQCIncgYear.Text.Trim() + "',N'" + txtAQCRemark.Text.Trim() + "','" + txtQCNoOfPer.Text.Trim() + "','" + getDate_MDY(txtQCInchDate.Text.Trim()) + "','" + txtQCInchYear.Text.Trim() + "',N'" + txtQCInchRemark.Text.Trim() + "' ,'" + txtCCHNoOfPer.Text.Trim() + "','" + getDate_MDY(txtCCHInchDate.Text.Trim()) + "','" + txtCCHInchYear.Text.Trim() + "',N'" + txtCCHReamrk.Text.Trim() + "','" + txtSthaiKarmiNoOfPer.Text.Trim() + "','" + getDate_MDY(txtSthaiKarmiInchDate.Text.Trim()) + "','" + txtSthaiKarmiInchYear.Text.Trim() + "',N'" + txtSthaiKarmiRemark.Text.Trim() + "','" + txtDainikNoOfPer.Text.Trim() + "','" + getDate_MDY(txtDainikInchDate.Text.Trim()) + "','" + txtDainikInchYear.Text.Trim() + "',N'" + txtDainikRemark.Text.Trim() + "','" + a_id + "',GETDATE(),'" + ClientIP + "')";
            SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
            int CT2 = 0;
            CT2 = cmd2.ExecuteNonQuery();
            if (CT2 == 1)
            {
                sqltran.Commit();
                Button15.Visible = false;
                pnlofferpopup.Visible = true;
                ModalPopupExtender1.Show();
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
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
    protected void btn_addnewoff_Click(object sender, EventArgs e)
    {
        string transid = "";
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            if (txtinspectiondate.Text == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Inspection Date...'); </script> ");
                txtinspectiondate.Focus();
            }
            else
            {
                a_id = ChkInspAIDCPT(lblinspid.Text.Trim());
                transid = lblinspid.Text.Trim().Substring(0, 5) + a_id;
                //string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE())";
                string gdwnqry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_PVInspection_CPT_UTL] ([TransID],[Inspection_ID],[District_ID],[Branch_ID],[Officer_ID],[PV_CPTandUTL],[Owned_NoOfGdwn],[Owned_Capacity],[Owned_AvlBags],[Owned_AvlQty],[Owned_Utilization],[Owned_Remark],[PvtPEG_NoOfGdwn],[Pvt_PEG_Capacity],[PvtPEG_AvlBags],[PvtPEG_AvlQty],[PvtPEG_Utilization],[Pvt_PEG_Remark],[JVS_NoOfGdwn],[JVS_Capacity],[JVS_AvlBags],[JVS_AvlQty],[JVS_Utilization],[JVS_Remark],[Hired_NoOfGdwn],[Hired_Capacity],[Hired_AvlBags],[Hired_AvlQty],[Hired_Utilization],[Hired_Remark],[CAP_NoOfGdwn],[CAP_Capacity],[CAP_AvlBags],[CAP_AvlQty],[CAP_Utilization],[CAP_Remark],[Other_NoOfGdwn],[Other_Capacity],[Other_AvlBags],[Other_AvlQty],[Other_Utilization],[Other_Remark],[CPT_UTL_UpdateBy],[CPT_UTL_UpdatedDate],[CreatedBy],[CreatedDate],[AID],[Inspection_Date]) VALUES ('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + Session["UserId"].ToString() + "','Y','" + txt_Ogdwn.Text.Trim() + "','" + txt_OgdwnCpt.Text.Trim() + "','" + txt_OABags.Text.Trim() + "','" + txt_OAQty.Text.Trim() + "','" + txt_OUtil.Text.Trim() + "',N'" + txt_ORemark.Text.Trim() + "','" + txt_Pgdwn.Text.Trim() + "','" + txt_PgdwnCpt.Text.Trim() + "','" + txt_PABags.Text.Trim() + "','" + txt_PAQty.Text.Trim() + "','" + txt_PUtil.Text.Trim() + "',N'" + txt_PRemark.Text.Trim() + "' ,'" + txt_Jgdwn.Text.Trim() + "' ,'" + txt_JgdwnCpt.Text.Trim() + "' ,'" + txt_JABags.Text.Trim() + "' ,'" + txt_JAQty.Text.Trim() + "' ,'" + txt_JUtil.Text.Trim() + "' ,N'" + txt_JRemark.Text.Trim() + "' ,'" + txt_Hgdwn.Text.Trim() + "' ,'" + txt_HgdwnCpt.Text.Trim() + "' ,'" + txt_HABags.Text.Trim() + "' ,'" + txt_HAQty.Text.Trim() + "' ,'" + txt_HUtil.Text.Trim() + "' ,N'" + txt_HRemark.Text.Trim() + "' ,'" + txt_Cgdwn.Text.Trim() + "' ,'" + txt_CgdwnCpt.Text.Trim() + "' ,'" + txt_CABags.Text.Trim() + "' ,'" + txt_CAQty.Text.Trim() + "','" + txt_CUtil.Text.Trim() + "','" + txt_CRemark.Text.Trim() + "' ,'" + txt_Othgdwn.Text.Trim() + "' ,'" + txt_OthgdwnCpt.Text.Trim() + "' ,'" + txt_OthABags.Text.Trim() + "' ,'" + txt_OthAQty.Text.Trim() + "' ,'" + txt_OthUtil.Text.Trim() + "','" + txt_OthRemark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + ClientIP + "',GETDATE(),'" + a_id + "','" + getDate_MDY(txtinspectiondate.Text.Trim()) + "')";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    btn_addnewoff.Visible = false;
                    pnlofferpopup.Visible = true;
                    ModalPopupExtender1.Show();
                    // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            string gdwnqry = " UPDATE [JointVentureScheme2018].[dbo].[tbl_PVInspection_CPT_UTL] SET [Online_RegDetails] = 'Y',[Owned_Reg_NoOfGdwn] = '" + txt_RegOwnNoOfGdwn.Text.Trim() + "',[Owned_Reg_Capacity] = '" + txt_RegOwnCapacity.Text.Trim() + "',[Owned_Reg_Remark] = N'" + txt_RegOwnRemark.Text.Trim() + "',[PvtPEG_Reg_NoOfGdwn] = '" + txt_RegPEGNoOfGdwn.Text.Trim() + "',[PvtPEG_Reg_Capacity] = '" + txt_RegPEGCpt.Text.Trim() + "',[PvtPEG_Reg_NoOfOfferGdwn] = '" + txt_RegPEGNoOfOfferedGdwn.Text.Trim() + "',[PvtPEG_Reg_OfferCapacity] = '" + txt_RegPEGOfferCpt.Text.Trim() + "',[PvtPEG_Agree_NoOfGdwn] = '" + txt_RegPEGAgreeNoofgdwn.Text.Trim() + "',[PvtPEG_AgreeCapacity] = '" + txt_RegPEGAgreeCpt.Text.Trim() + "',[PvtPEG_RegRemark]  = '" + txt_RegPEGRemark.Text.Trim() + "',[JVS_Reg_NoOfGdwn] = '" + txt_RegJvsNoOfGdwn.Text.Trim() + "',[JVS_Reg_Capacity] = '" + txt_RegJVSCpt.Text.Trim() + "',[JVS_Offer_NoOfGdwn] = '" + txt_RegJVSNoOfOfferedGdwn.Text.Trim() + "',[JVS_Offer_Capacity] = '" + txt_RegJVSOfferCPT.Text.Trim() + "',[JVS_Agree_NoOfGdwn] = '" + txt_RegJVSAgreeNoOfGdwn.Text.Trim() + "',[JVS_Agree_Capacity] = '" + txt_RegJVSAgreeCpt.Text.Trim() + "',[JVS_RegAgreeRemark] ='" + txt_RegJVSRemark.Text.Trim() + "',[Hired_Reg_NoOfGdwn] = '" + txt_RegHNoOfGdwn.Text.Trim() + "',[Hired_Reg_Capacity] = '" + txt_RegHCpt.Text.Trim() + "',[Hired_Agree_NoOfGdwn] = '" + txt_RegHAgreeNoOf.Text.Trim() + "',[Hired_Agree_Capacity] = '" + txt_RegHAgreeCpt.Text.Trim() + "',[Hired_RegRemark] =N'" + txt_RegHAgreeRemark.Text.Trim() + "',[CAP_Reg_NoOfGdwn] = '" + txt_RegCAPNoOdGdwnReg.Text.Trim() + "',[CAP_Reg_Capacity] = '" + txt_RegCAPCpt.Text.Trim() + "',[CAP_Agree_NoOFGdwn] = '" + txt_RegAgreeNoOfGdwn.Text.Trim() + "',[CAP_Agree_Capacity] = '" + txt_RegAgreeCpt.Text.Trim() + "',[CAP_RegRemark] = '" + txt_RegCAPRemark.Text.Trim() + "',[Oth_RegNoOfGdwn] = '" + txt_RegOthNoOfCpt.Text.Trim() + "',[Oth_RegCpt] = '" + txt_RegOthRegCpt.Text.Trim() + "',[Oth_RegAgreeNoOfGdwn] = '" + txt_RegOthNoOfGdwn.Text.Trim() + "',[Oth_RegAgreeCpt] = '" + txt_RegOthAgreeCpt.Text.Trim() + "',[Oth_RegRemark] = '" + txt_RegOthRemark.Text.Trim() + "',[Online_Reg_UpdatedBy] = '" + ClientIP + "',[Online_Reg_UpdateDate] = GETDATE() where Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
            int CT2 = 0;
            CT2 = cmd2.ExecuteNonQuery();
            if (CT2 == 1)
            {
                sqltran.Commit();
                Button1.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            // ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error...'); </script> ");
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            sqltran = conStr.BeginTransaction();
            //string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE())";
            string gdwnqry = " UPDATE [JointVentureScheme2018].[dbo].[tbl_PVInspection_CPT_UTL] SET [PV_CAP_Details] = 'Y',[CAP_NoOfStack] = '" + txtCAP_Upyog.Text.Trim() + "',[CAP_RemStack_Used] = '" + txtCAP_BalUpyog.Text.Trim() + "',[CAP_RemStack_UnUsed] = '" + txtCAP_UnUpyog.Text.Trim() + "',[CAP_More_Availability] = '" + txtCAP_MoreReq.Text.Trim() + "',[CAP_PV_Remark] = N'" + txtCAP_RemarkS.Text.Trim() + "',[CAP_Cover] = '" + txtCAP_NoOfCover.Text.Trim() + "',[CAP_Cover_RemForUsed] = '" + txtCAP_BalCover.Text.Trim() + "',[CAP_Cover_RemForUnUsed] = '" + txtCAP_UnUsedCover.Text.Trim() + "',[CAP_Cover_More_Avail] = '" + txtCAP_ReqCover.Text.Trim() + "',[CAP_Cover_Remark] = N'" + txtCAP_RemarkCover.Text.Trim() + "',[CAP_Cover_T] = '" + txtCAP_NoOfTop.Text.Trim() + "',[CAP_Cover_T_RemUsed] = '" + txtCAP_BalUsedTop.Text.Trim() + "',[CAP_Cover_T_RemUnUsed] = '" + txtCAP_UnusedTOP.Text.Trim() + "',[CAP_Cover_T_MoreAvail] = '" + txtCAP_ReqTop.Text.Trim() + "',[CAP_Cover_T_Remark] = N'" + txtCAP_RemarkTop.Text.Trim() + "',[CAP_Rassi] = '" + txtCAP_NoOfRasi.Text.Trim() + "',[CAP_Rassi_RemUsed] = '" + txtCAP_BalRassi.Text.Trim() + "',[CAP_Rassi_RemUnUsed] = '" + txtCAP_UnUsedRassi.Text.Trim() + "',[CAP_Rassi_MoreAvail] = '" + txtCAP_ReqRassi.Text.Trim() + "',[CAP_Rassi_Remark] = N'" + txtCAP_RemarkRassi.Text.Trim() + "',[CAP_Huk] = '" + txtCAP_NoOfHuk.Text.Trim() + "',[CAP_Huk_RemUsed] = '" + txtCAP_BalHuk.Text.Trim() + "',[CAP_Huk_RemUnUsed] = '" + txtCAP_UnUsedHuk.Text.Trim() + "',[CAP_Huk_MoreAvail] = '" + txtCAP_ReqHuk.Text.Trim() + "',[CAP_Huk_Remark] = N'" + txtCAP_RemarkHuk.Text.Trim() + "',[CAP_Agni] = '" + txt_AgniUse.Text.Trim() + "',[CAP_Agni_RemUsed] = '" + txt_Agniseshmatra.Text.Trim() + "',[CAP_Agni_RemUnUsed] = '" + txt_agniunusedmatra.Text.Trim() + "',[CAP_Agni_MoreAvail] = '" + txt_agnimorereq.Text.Trim() + "',[CAP_Agni_Reamrk] = N'" + txt_agniremark.Text.Trim() + "',[CAP_UpdatedBy] = '" + ClientIP + "',[CAP_UpdatedDate] = GETDATE(),[CAP_Security] = N'" + txtSecurity.Text.Trim() + "' WHERE  Inspection_ID='" + Session["Godown_ID"].ToString() + "' ";
            SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
            int CT2 = 0;
            CT2 = cmd2.ExecuteNonQuery();
            if (CT2 == 1)
            {
                sqltran.Commit();
                Button5.Visible = false;
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            throw ex;
        }
        finally
        {
            sqltran.Dispose();
            conStr.Close();
        }
    }

    public void FillCapacityGodown()
    {
        //string strsql = "select (select isnull(Count(Godown_ID),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID=MD.BranchId   ) as 'NoOfOwned',(select Isnull(SUM(Godown_Scientific_Capacity),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID=MD.BranchId ) as 'Owned',(select isnull(Count(Godown_ID),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID=MD.BranchId ) as 'NoOfJVS',(select Isnull(SUM(Godown_Scientific_Capacity),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID=MD.BranchId ) as 'JVS',(select isnull(Count(Godown_ID),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('PVT.PEG') and PV.BranchID=MD.BranchId ) as 'NoOfPVTPEG',(select Isnull(SUM(Godown_Scientific_Capacity),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('PVT.PEG') and PV.BranchID=MD.BranchId ) as 'PVTPEG',(select isnull(isnull(Count(Godown_ID),0),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID=MD.BranchId ) as 'NoOfHired',(select isnull(Isnull(SUM(Godown_Scientific_Capacity),0),0) from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID=MD.BranchId ) as 'Hired',(select isnull(isnull(Count(Godown_ID),0),0) from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type='Owned' and PV.BranchID=MD.BranchId ) as 'NoOfCAPOwned',(select Isnull(SUM(Godown_Scientific_Capacity),0) from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type='Owned' and PV.BranchID=MD.BranchId ) as 'CAPOwned',(select isnull(Count(Godown_ID),0) from tbl_PV_Metadata_Godown as PV where Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID=MD.BranchId ) as 'NoOfOthers' ,(select Isnull(SUM(Godown_Scientific_Capacity),0) from tbl_PV_Metadata_Godown as PV where Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID=MD.BranchId ) as 'Others' from tbl_metadata_depot as MD where MD.BranchId='" + Session["SInsp_BranchID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand("Inspection_Get_Capacity_Detials", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@@Branch_ID", lblinspid.Text.ToString());
        // cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
       // SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txt_Ogdwn.Text = dt.Rows[0]["NoOfOwned"].ToString();
            txt_OgdwnCpt.Text = dt.Rows[0]["Owned"].ToString();
            txt_Pgdwn.Text = dt.Rows[0]["NoOfPVTPEG"].ToString();
            txt_PgdwnCpt.Text = dt.Rows[0]["PVTPEG"].ToString();
            txt_Jgdwn.Text = dt.Rows[0]["NoOfJVS"].ToString();
            txt_JgdwnCpt.Text = dt.Rows[0]["JVS"].ToString();
            txt_Hgdwn.Text = dt.Rows[0]["NoOfHired"].ToString();
            txt_HgdwnCpt.Text = dt.Rows[0]["Hired"].ToString();
            txt_Cgdwn.Text = dt.Rows[0]["NoOfCAPOwned"].ToString();
            txt_CgdwnCpt.Text = dt.Rows[0]["CAPOwned"].ToString();
            txt_Othgdwn.Text = dt.Rows[0]["NoOfOthers"].ToString();
            txt_OthgdwnCpt.Text = dt.Rows[0]["Others"].ToString();
        }
        else
        {

        }
    }

    public void FillPVBags()
    {
        // string strsql = "select (select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OwnPVBags ,(select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as JVSPVBags,(select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('PVT.PEG') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as PPVBags,(select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as HPVBags,(select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as CAPPVBags,(select isnull(sum(Avl_Bags_AsPer_PV),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OtherPVBags from tbl_metadata_depot as MD where MD.BranchId='" + Session["SInsp_BranchID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand("Inspection_Get_Fill_PVBags_Details", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@@Branch_ID", lblinspid.Text.ToString());
        // cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        // SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
        //DataTable dt = new DataTable();
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txt_OABags.Text = dt.Rows[0]["OwnPVBags"].ToString();
            txt_PABags.Text = dt.Rows[0]["PPVBags"].ToString();
            txt_JABags.Text = dt.Rows[0]["JVSPVBags"].ToString();
            txt_HABags.Text = dt.Rows[0]["HPVBags"].ToString();
            txt_CABags.Text = dt.Rows[0]["CAPPVBags"].ToString();
            txt_OthABags.Text = dt.Rows[0]["OtherPVBags"].ToString();
        }
        else
        {

        }
    }
    public void FillPVStackClassification()
    {
        string strsql = "select (select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OwnNoOfStack ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID=MD.BranchId)) as StackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as StackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as StackHeavy ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as JVSPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as JVSStackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as JVSStackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as JVSStackHeavy ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as PPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as PStackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as PStackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as PStackHeavy ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as HPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as HStackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID=MD.BranchId)) as HStackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as HStackHeavy ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as CAPPVBags,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type ='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as CAPStackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type ='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as CAPStackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type ='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as CAPStackHeavy ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OtherPVBags,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='C' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OthStackClear ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='F' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OthStackFew ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Stack_Classification='H' and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OthStackHeavy from tbl_metadata_depot as MD where MD.BranchId='" + Session["SInsp_BranchID"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtOCl_TotalStack.Text = dt.Rows[0]["OwnNoOfStack"].ToString();
            txtOCl_C.Text = dt.Rows[0]["StackClear"].ToString();
            txtOCl_F.Text = dt.Rows[0]["StackFew"].ToString();
            txtOCl_H.Text = dt.Rows[0]["StackHeavy"].ToString();

            txtJVSCl_TotalStack.Text = dt.Rows[0]["JVSPVStack"].ToString();
            txtJVSCl_C.Text = dt.Rows[0]["JVSStackClear"].ToString();
            txtJVSCl_F.Text = dt.Rows[0]["JVSStackFew"].ToString();
            txtJVSCl_H.Text = dt.Rows[0]["JVSStackHeavy"].ToString();

            txtPEGCl_TotalStack.Text = dt.Rows[0]["PPVStack"].ToString();
            txtPEGCl_C.Text = dt.Rows[0]["PStackClear"].ToString();
            txtPEGCl_F.Text = dt.Rows[0]["PStackFew"].ToString();
            txtPEGCl_H.Text = dt.Rows[0]["PStackHeavy"].ToString();

            txtHCl_TotalStack.Text = dt.Rows[0]["HPVStack"].ToString();
            txtHCl_C.Text = dt.Rows[0]["HStackClear"].ToString();
            txtHCl_F.Text = dt.Rows[0]["HStackFew"].ToString();
            txtHCl_H.Text = dt.Rows[0]["HStackHeavy"].ToString();

            txtCAPCl_TotalStack.Text = dt.Rows[0]["CAPPVBags"].ToString();
            txtCAPCl_C.Text = dt.Rows[0]["CAPStackClear"].ToString();
            txtCAPCl_F.Text = dt.Rows[0]["CAPStackFew"].ToString();
            txtCAPCl_H.Text = dt.Rows[0]["CAPStackHeavy"].ToString();

            txtOthCl_TotalStack.Text = dt.Rows[0]["OtherPVBags"].ToString();
            txtOthCl_C.Text = dt.Rows[0]["OthStackClear"].ToString();
            txtOthCl_F.Text = dt.Rows[0]["OthStackFew"].ToString();
            txtOthCl_H.Text = dt.Rows[0]["OthStackHeavy"].ToString();
        }
        else
        {

        }
    }

    public void FillPVStackFumigation()
    {
        string strsql = "select (select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OwnPVStack ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101) and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as StackFumi ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as JVSPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101)  and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type in ('Joint Venture(JV)','WDRA') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as JVSStackFumi ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as PPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101)  and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='PVT.PEG' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as PStackFumi ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as HPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101)  and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type='Covered' and Hired_Type ='Hired' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as HStackFumi ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "' )) as CAPPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101)  and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type ='Owned' and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as CAPStackFumi ,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OtherPVStack,(select isnull(Count(StackID),0) from tbl_stackwiseBal_Annex_B where Convert(varchar(10),Last_Fumigation_Date,101) > Convert(varchar(10),DATEADD(MONTH, -3, GETDATE()),101)  and GodownID in (select Distinct Godown_ID from tbl_PV_Metadata_Godown as PV where Storage_Type!='Covered' and Hired_Type in ('Others','Silo Bags','Tribal Scheme','Steel Silo') and PV.BranchID='" + Session["SInsp_BranchID"].ToString() + "')) as OthStackFumi from tbl_metadata_depot as MD where MD.BranchId='" + Session["SInsp_BranchID"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtOFMG_TotalStack.Text = dt.Rows[0]["OwnPVStack"].ToString();
            txtOFMG_fmgStack.Text = dt.Rows[0]["StackFumi"].ToString();
            txtOFMG_BalFmgStack.Text = Convert.ToString((Convert.ToInt32(txtOFMG_TotalStack.Text.Trim()) - Convert.ToInt32(txtOFMG_fmgStack.Text.Trim())));

            txtPEGFMG_NoOfStakc.Text = dt.Rows[0]["PPVStack"].ToString();
            txtPEGFMG_fmgstack.Text = dt.Rows[0]["PStackFumi"].ToString();
            txtPEGFMG_Balfmgstack.Text = Convert.ToString((Convert.ToInt32(txtPEGFMG_NoOfStakc.Text.Trim()) - Convert.ToInt32(txtPEGFMG_fmgstack.Text.Trim())));

            txtJVSFMG_TotalStack.Text = dt.Rows[0]["JVSPVStack"].ToString();
            txtJVSFMG_fmgstack.Text = dt.Rows[0]["JVSStackFumi"].ToString();
            txtJVSFMG_BalFmgStack.Text = Convert.ToString((Convert.ToInt32(txtJVSFMG_TotalStack.Text.Trim()) - Convert.ToInt32(txtJVSFMG_fmgstack.Text.Trim())));

            txtHFMG_TotalStack.Text = dt.Rows[0]["HPVStack"].ToString();
            txtHFMG_fmgstack.Text = dt.Rows[0]["HStackFumi"].ToString();
            txtHFMG_BalmgStack.Text = Convert.ToString((Convert.ToInt32(txtHFMG_TotalStack.Text.Trim()) - Convert.ToInt32(txtHFMG_fmgstack.Text.Trim())));

            txtCAPFMG_TotalStack.Text = dt.Rows[0]["CAPPVStack"].ToString();
            txtCAPFMG_fmgStack.Text = dt.Rows[0]["CAPStackFumi"].ToString();
            txtCAPFMG_BalFmgStack.Text = Convert.ToString((Convert.ToInt32(txtCAPFMG_TotalStack.Text.Trim()) - Convert.ToInt32(txtCAPFMG_fmgStack.Text.Trim())));

            txtOthFMG_TotalStack.Text = dt.Rows[0]["OtherPVStack"].ToString();
            txtOthFMG_FmgStack.Text = dt.Rows[0]["OthStackFumi"].ToString();
            txtOthFMG_BalFmgStack.Text = Convert.ToString((Convert.ToInt32(txtOthFMG_TotalStack.Text.Trim()) - Convert.ToInt32(txtOthFMG_FmgStack.Text.Trim())));
        }
        else
        {

        }
    }

    public void FillOnlineRegCapacity()
    {
        string strsql = "select REGG.BranchID,RegNoOfGdwn,RegCapacity,isnull(OfrNoOfGdwn,0)OfrNoOfGdwn,isnull(OfferCpt,0)OfferCpt,isnull(AgrNoOfGdwn,0)AgrNoOfGdwn,isnull(AgreeCpt,0)AgreeCpt,isnull(OwnRegNoOfGdwn,0) as OwnRegNoOfGdwn,isnull(OwnRegCapacity,0)OwnRegCapacity,isnull(MANDIRegNoOfGdwn,0) as MANDIRegNoOfGdwn,isnull(MANDIRegCapacity,0)MANDIRegCapacity from (select BranchID,count(Godown_ID) as RegNoOfGdwn,sum(G_ScientificCapacity) RegCapacity from tbl_WarehouseGodown_Reg where Registration_ID in (select REG.Registration_ID from tbl_WarehouseRegistration as REG where WarehouseHiredtype is null) group by BranchID ) as REGG left join (select BranchID,count(Godown_ID) as OfrNoOfGdwn,sum(G_OfferCapacity) OfferCpt from tbl_Warehouse_Godown_Offer_2019 group by BranchID ) as OFR on REGG.BranchID=OFR.BranchID left join (select BranchID,count(Godown_ID) as OwnRegNoOfGdwn,sum(G_ScientificCapacity) OwnRegCapacity from tbl_WarehouseGodown_Reg where Registration_ID in (select REG.Registration_ID from tbl_WarehouseRegistration as REG where WarehouseHiredtype ='1') group by BranchID ) as OWNREG on OWNREG.BranchID=REGG.BranchID left join(select BranchID,count(Godown_ID) as MANDIRegNoOfGdwn,sum(G_ScientificCapacity) MANDIRegCapacity from tbl_WarehouseGodown_Reg where Registration_ID in (select REG.Registration_ID from tbl_WarehouseRegistration as REG where WarehouseHiredtype ='6') group by BranchID ) as MANDI on MANDI.BranchID=REGG.BranchID left join (select branchID,count(GodownID) as AgrNoOfGdwn,sum(Agree_Capacity) as AgreeCpt from tbl_Godown_Agreement where CreatedDate>'02/19/2019' group by  BranchID ) as AGR  on AGR.BranchID=REGG.BranchID where REGG.BranchID='" + Session["SInsp_BranchID"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, conStr);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txt_RegOwnNoOfGdwn.Text = dt.Rows[0]["OwnRegNoOfGdwn"].ToString();
            txt_RegOwnCapacity.Text = dt.Rows[0]["OwnRegCapacity"].ToString();
            txt_RegJvsNoOfGdwn.Text = dt.Rows[0]["RegNoOfGdwn"].ToString();
            txt_RegJVSCpt.Text = dt.Rows[0]["RegCapacity"].ToString();
            txt_RegJVSNoOfOfferedGdwn.Text = dt.Rows[0]["OfrNoOfGdwn"].ToString();
            txt_RegJVSOfferCPT.Text = dt.Rows[0]["OfferCpt"].ToString();
            txt_RegJVSAgreeNoOfGdwn.Text = dt.Rows[0]["AgrNoOfGdwn"].ToString();
            txt_RegJVSAgreeCpt.Text = dt.Rows[0]["AgreeCpt"].ToString();
            txt_RegHNoOfGdwn.Text = dt.Rows[0]["MANDIRegNoOfGdwn"].ToString();
            txt_RegHCpt.Text = dt.Rows[0]["MANDIRegCapacity"].ToString();
        }
        else
        {

        }
    }

    public void FatchPreviousfilldata()
    {
        string SrchQry = "select * from tbl_PVInspection_CPT_UTL where Inspection_ID='" + lblinspid.Text.Trim() + "' ";
        SqlDataAdapter da1 = new SqlDataAdapter(SrchQry, conStr);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0]["PV_CPTandUTL"].ToString() == "Y")
            {
                btn_addnewoff.Visible = false;
                lblcptmessage.Visible = true;
                lblcptmessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            else
            {
                FillCapacityGodown();
                FillPVBags();
            }
            if (dt1.Rows[0]["Online_RegDetails"].ToString() == "Y")
            {
                Button1.Visible = false;
                lblOnlineRegCptMessage.Visible = true;
                lblOnlineRegCptMessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            else
            {
                FillOnlineRegCapacity();
            }
            if (dt1.Rows[0]["PV_CAP_Details"].ToString() == "Y")
            {
                Button5.Visible = false;
                lblcapmessage.Visible = true;
                lblcapmessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
        }
        else
        {
            FillPVBags();
            FillCapacityGodown();
            FillOnlineRegCapacity();
        }

        string SrchQry2 = "select * from tbl_PVInspection_Fumigation_Details where Inspection_ID='" + lblinspid.Text.Trim() + "' ";
        SqlDataAdapter da2 = new SqlDataAdapter(SrchQry2, conStr);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt2.Rows.Count > 0)
        {
            if (dt2.Rows[0]["Chml_Avl"].ToString() == "Y")
            {
                Button3.Visible = false;
                lblKitnasakmesage.Visible = true;
                lblKitnasakmesage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            if (dt2.Rows[0]["Fmg_Details"].ToString() == "Y")
            {
                Button7.Visible = false;
                lblkitopcharmessage.Visible = true;
                lblkitopcharmessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            else
            {
                FillPVStackFumigation();
            }
            if (dt2.Rows[0]["Gdwn_Clsf_Details"].ToString() == "Y")
            {
                Button9.Visible = false;
                lblkigrastamessage.Visible = true;
                lblkigrastamessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            else
            {
                FillPVStackClassification();
            }
        }
        else
        {
            FillPVStackFumigation();
            FillPVStackClassification();
        }

        string SrchQry3 = "select * from tbl_PVInspection_CashDetails where Inspection_ID='" + lblinspid.Text.Trim() + "' ";
        SqlDataAdapter da3 = new SqlDataAdapter(SrchQry3, conStr);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            if (dt3.Rows[0]["Str_Chrg"].ToString() == "Y")
            {
                Button11.Visible = false;
                lblbhandarnsulkmessage.Visible = true;
                lblbhandarnsulkmessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            if (dt3.Rows[0]["JVS_Fill"].ToString() == "Y")
            {
                Button18.Visible = false;
                lvljvskatotramessage.Visible = true;
                lvljvskatotramessage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            if (dt3.Rows[0]["C_Fill"].ToString() == "Y")
            {
                Button13.Visible = false;
                lblcasemesaage.Visible = true;
                lblcasemesaage.Text = "यह जानकारी भरी जा चुकी है ।";
            }
        }
        else
        {

        }

        string SrchQry4 = "select * from tbl_PVInspection_BranchEmploye where Inspection_ID='" + lblinspid.Text.Trim() + "' ";
        SqlDataAdapter da4 = new SqlDataAdapter(SrchQry4, conStr);
        DataTable dt4 = new DataTable();
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            Button15.Visible = false;
            lblpadsttmessage.Visible = true;
            lblpadsttmessage.Text = "यह जानकारी भरी जा चुकी है ।";
        }
    }
    protected void btncnfrmmessage_Click(object sender, EventArgs e)
    {
        Response.Redirect("InspOfficer_FillOverall_PVInsp.aspx");
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        if (Button15.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button13.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (btn_addnewoff.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button1.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button3.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button5.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button7.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button9.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button11.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (Button18.Visible == true)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check All Record save or not...'); </script> ");
        }
        else if (chkDec.Checked == false)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Self Decleration...'); </script> ");
            chkDec.Focus();
        }
        else
        {
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }
            try
            {
                sqltran = conStr.BeginTransaction();
                //string gdwnqry = "INSERT INTO [tbl_PVInspection_Fumigation_Details] ([TransID],[Inspection_ID],[Officer_ID],[District_ID],[Branch_ID],[Chml_Avl_AliminiumQty],[Chml_Avl_PV_AluminiumQty],[Chml_Avl_Aluminium_UnUsed_ExpQty],[Chm_Avl_Aluminium_MoreAvl],[Chml_Avl_Aluminium_Remark],[Chml_Avl_MethiyanQty],[Chml_Avl_PV_MethiyanQty],[Chml_Avl_Methiyan_UnUsed_ExpQty],[Chml_Avl_Methiyan_MoreAvl],[Chml_Avl_Methiyan_Remark],[Chml_Avl_DDVPQty],[Chml_Avl_PV_DDPVQty],[Chml_Avl_DDPV_UnUsed_ExpQty],[Chml_Avl_DDPV_MoreAvl],[Chml_Avl_DDPV_Remark],[Chml_Avl_DeltaQty],[Chml_Avl_PV_DeltaQty],[Chml_Avl_Delta_Unused_ExpQty],[Chml_Avl_Delta_MoreAvl],[Chml_Avl_Delta_Remark],[Chml_Avl_OtherQty],[Chml_Avl_PV_OtherQty],[Chml_Avl_Other_Unused_ExpQty],[Chml_Avl_Other_MoreAvl],[Chml_Avl_Other_Remark],[Chml_Avl_UpdatedBy],[Chml_Avl_UpdatedDate],[AID],[CreatedBy],[CreatedDate])     VALUES('" + transid + "','" + Session["Godown_ID"].ToString() + "','" + Session["UserId"].ToString() + "','" + Session["SInsp_DisID"].ToString() + "','" + Session["SInsp_BranchID"].ToString() + "','" + txtAlumiQty.Text.Trim() + "','" + txtAlumi_PVQty.Text.Trim() + "','" + txtAlum_Exp.Text.Trim() + "','" + txtAlum_Req.Text.Trim() + "',N'" + txtAlum_Remark.Text.Trim() + "','" + txtMeth_Qty.Text.Trim() + "','" + txtMeth_PVQty.Text.Trim() + "','" + txtMeth_Exp.Text.Trim() + "','" + txtMeth_Req.Text.Trim() + "',N'" + txtMeth_Remark.Text.Trim() + "','" + txtDDVP_Qty.Text.Trim() + "','" + txtDDVP_PVQty.Text.Trim() + "','" + txtDDVP_Exp.Text.Trim() + "','" + txtDDVP_Req.Text.Trim() + "',N'" + txtDDVP_Remark.Text.Trim() + "','" + txtDelta_Qty.Text.Trim() + "','" + txtDelta_PVQty.Text.Trim() + "','" + txtDelta_Exp.Text.Trim() + "','" + txtDelta_Req.Text.Trim() + "',N'" + txtDelta_Remark.Text.Trim() + "','" + txtOther_Qty.Text.Trim() + "','" + txtOther_PVQty.Text.Trim() + "','" + txtOther_Exp.Text.Trim() + "','" + txtOther_Req.Text.Trim() + "',N'" + txtOther_Remark.Text.Trim() + "','" + ClientIP + "',GETDATE(),'" + a_id + "','" + ClientIP + "',GETDATE())";
                string gdwnqry = "UPDATE [JointVentureScheme2018].[dbo].[tbl_PVInspection_BranchEmploye] set FinalSubmission='Y' where Inspection_ID='" + Session["Godown_ID"].ToString() + "'";
                SqlCommand cmd2 = new SqlCommand(gdwnqry, conStr, sqltran);
                int CT2 = 0;
                CT2 = cmd2.ExecuteNonQuery();
                if (CT2 == 1)
                {
                    sqltran.Commit();
                    pnlofferpopup.Visible = true;
                    ModalPopupExtender1.Show();
                    //     ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save This Record...'); </script> ");
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                throw ex;
            }
            finally
            {
                sqltran.Dispose();
                conStr.Close();
            }
        }
    }
    public void chkfinalsub()
    {
        string SrchQry4 = "select * from tbl_PVInspection_BranchEmploye where Inspection_ID='" + lblinspid.Text.Trim() + "' and FinalSubmission='Y'";
        SqlDataAdapter da4 = new SqlDataAdapter(SrchQry4, conStr);
        DataTable dt4 = new DataTable();
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            Button17.Visible = false;
        }
        else
        {

        }
    }

   
}