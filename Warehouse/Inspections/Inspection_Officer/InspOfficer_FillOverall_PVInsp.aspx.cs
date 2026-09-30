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


public partial class Inspections_Inspection_Officer_InspOfficer_FillOverall_PVInsp : System.Web.UI.Page
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
       // PFID = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            lblinspid.Text = Session["hdnInspection_ID"].ToString(); ;
            FatchScheduleInspData();
            FatchInspData();

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

            Getcapacity_in_Qntl();
            Getcapacity_in_MT();
            pesticidesDetails();
            CapeDetails();
            Getstatusofinsecticide();
            Getinsecticidestatus();
            Getstoragedutysubmision();
            GetCaseBalanceDetails();
            GetEmployeeDetails();
            CHECKFINALSTATUS();
            //GetJVSBillAmount();
            //ShowMessage.WebMsgBox.Show("Your message here");
        }
    }
    //protected void gvCol2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox txt1 = (TextBox)e.Row.FindControl("txtStorage_Capacity");
    //        TextBox txt2 = (TextBox)e.Row.FindControl("txtStored_Weight");
    //        int add = Convert.ToInt32(txt2.Text) / Convert.ToInt32(txt1.Text);
    //        (e.Row.FindControl("txtPercentage_Of_Utility") as TextBox).Text = add.ToString();
    //    }
    //}
    protected void Grdstatusofinsecticide_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView data = (DataRowView)e.Row.DataItem;
        //    TextBox txtFumigation_date = (TextBox)e.Row.FindControl("txtFumigation_date");
        //    TextBox txtsprayed_date = (TextBox)e.Row.FindControl("txtsprayed_date");
        //    txtFumigation_date.Enabled = false;
        //    txtsprayed_date.Enabled = false;
        //}
    }
    public void CHECKFINALSTATUS()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_FInal_Submission_States", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Inspection_Id", lblinspid.Text.ToString());
            cmd.Parameters.AddWithValue("@Branch_Id", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@Insp_Officer_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year", Session["hdnfinancialYear"].ToString());
            cmd.Parameters.AddWithValue("@Quater_Type", Session["hdninsptype"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                btnsaveprofile.Visible = false;
                btnCapacityInMT.Visible = false;
                btnCapacityQntlIn.Visible = false;
                brnDescriptionofpesticides.Visible = false;
                btncapeinspectiondetails.Visible = false;
                btnstatusofinsecticide.Visible = false;
                btnGrdinsecticidestatus.Visible = false;
                btnGrdstoragedutysubmision.Visible = false;
                btnCaseBalance.Visible = false;
                btnEmployeeDetails.Visible = false;
                Button2.Visible = false;
                btnFinalSubmittion.Visible = false;
            }
            else
            {
                btnsaveprofile.Visible = true;
                btnCapacityInMT.Visible = true;
                btnCapacityQntlIn.Visible = true;
                brnDescriptionofpesticides.Visible = true;
                btncapeinspectiondetails.Visible = true;
                btnstatusofinsecticide.Visible = true;
                btnGrdinsecticidestatus.Visible = true;
                btnGrdstoragedutysubmision.Visible = true;
                btnCaseBalance.Visible = true;
                btnEmployeeDetails.Visible = true;
                Button2.Visible = true;
                btnFinalSubmittion.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
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
            cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
            cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
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
                txtinspectiondate.Text = dt.Rows[0]["Inspection_Date"].ToString().Trim();
                //Label86.Visible = true;
                //Label79.Text = "यह जानकारी भरी जा चुकी है ।";
                //btnsaveprofile.Visible = false;
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
            cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
            cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
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
                ddlGHO.Text = dt.Rows[0]["Is_Pandingat_HO_RO"].ToString().Trim();
                txtgodamdistance.Text = dt.Rows[0]["How_Many_Circumference"].ToString().Trim();
                ddlA.SelectedValue = dt.Rows[0]["From_the_point_of_view_of_scientific_storage"].ToString().Trim();
                if (ddlA.SelectedValue == "1")
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
                else if (ddlB.SelectedValue == "2")
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
                ddl16.Text = dt.Rows[0]["weight_Remark"].ToString().Trim();
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
                ddl29.Text = dt.Rows[0]["Department_wise_full_justifiable_statement_of_requirements"].ToString().Trim();
                ddl30.Text = dt.Rows[0]["Is_Suggestions_of_Inspection_Officer"].ToString().Trim();
                ddl31.Text = dt.Rows[0]["Is_Observation_officer_comments"].ToString().Trim();
                //Button2.Visible = false;
                //Label86.Visible = true;
                //Label86.Text = "यह जानकारी भरी जा चुकी है ।";
            }
            //else
            //{

            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            //}

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
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
                cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
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
                cmd.Parameters.AddWithValue("@Inspection_Date", getDate_MDY(txtinspectiondate.Text));

                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Employee Inspection Details Successfully submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //btnsaveprofile.Visible = false;
                    //pnlofferpopup.Visible = true;
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
                cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                cmd.Parameters.AddWithValue("@Officer_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@District_ID", Session["hdndistrictid"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@Is_Incoming_Rregister", ddlavakyesno.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Outward_Register", txtjavakpanji.Text);
                cmd.Parameters.AddWithValue("@Localpostalbook", ddlsthanidakbook.SelectedValue);
                cmd.Parameters.AddWithValue("@Stationery_register", ddlstashnar.SelectedValue);
                cmd.Parameters.AddWithValue("@Dead_stock_register", ddldedstock.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_Pandingat_HO_RO", ddlGHO.Text);
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
                cmd.Parameters.AddWithValue("@weight_Remark", ddl16.Text);
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
                cmd.Parameters.AddWithValue("@Department_wise_full_justifiable_statement_of_requirements", ddl29.Text);
                cmd.Parameters.AddWithValue("@Is_Suggestions_of_Inspection_Officer", ddl30.Text);
                cmd.Parameters.AddWithValue("@Is_Observation_officer_comments", ddl31.Text);
                cmd.Parameters.AddWithValue("@CreatedBy", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Inspection Details Successfully submitted |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //Button2.Visible = false;

                    //Label86.Visible = true;
                    //Label86.Text = "यह जानकारी भरी जा चुकी है ।";
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
    protected void Getcapacity_in_Qntl()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_capacity_and_utility_of_the_branch_capacity_in_Qntl", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvCol2.DataSource = dt;
                            gvCol2.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            gvCol2.DataSource = dt;
                            gvCol2.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void Getcapacity_in_MT()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_capacity_and_utility_of_the_branch_capacity_in_MT", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvCol3.DataSource = dt;
                            gvCol3.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            gvCol3.DataSource = dt;
                            gvCol3.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void pesticidesDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_Get_pesticides_available_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvCol4.DataSource = dt;
                            gvCol4.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            gvCol4.DataSource = dt;
                            gvCol4.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void CapeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_Get_Details_of_Cape_Inspection", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            CapeGrd.DataSource = dt;
                            CapeGrd.DataBind();
                            //txtFirebrigade.Text = dt.Rows[0]["Fire_brigade_Details"].ToString();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            CapeGrd.DataSource = dt;
                            CapeGrd.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void Getstatusofinsecticide()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_Inspection_Description_of_insecticide_status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Grdstatusofinsecticide.DataSource = dt;
                            Grdstatusofinsecticide.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            Grdstatusofinsecticide.DataSource = dt;
                            Grdstatusofinsecticide.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void Getinsecticidestatus()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Inspection_Description_of_insecticide_status_in_the_warehouse", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Grdinsecticidestatus.DataSource = dt;
                            Grdinsecticidestatus.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            Grdinsecticidestatus.DataSource = dt;
                            Grdinsecticidestatus.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void Getstoragedutysubmision()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Inspection_Statement_of_storage_duty_submission_status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Grdstoragedutysubmision.DataSource = dt;
                            Grdstoragedutysubmision.DataBind();
                            Grdstoragedutysubmision.Columns[3].Visible = false;
                            Grdstoragedutysubmision.Columns[4].Visible = false;
                            Grdstoragedutysubmision.Columns[6].Visible = false;

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            Grdstoragedutysubmision.DataSource = dt;
                            Grdstoragedutysubmision.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GetCaseBalanceDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Inspection_Cash_balance_status_details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdCaseBalance.DataSource = dt;
                            GrdCaseBalance.DataBind();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GrdCaseBalance.DataSource = dt;
                            GrdCaseBalance.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Inspection_List_of_all_the_employees_posted_at_the_branch", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdEmployeeDetails.DataSource = dt;
                            GrdEmployeeDetails.DataBind();
                            GrdEmployeeDetails.Columns[3].Visible = false;
                            GrdEmployeeDetails.Columns[4].Visible = false;

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GrdEmployeeDetails.DataSource = dt;
                            GrdEmployeeDetails.DataBind();
                        }
                    }
                }
            }
        }
    }

    //public void GetJVSBillAmount()
    //{
    //    try
    //    {
    //        if (conStr.State == ConnectionState.Closed)
    //        {
    //            conStr.Open();

    //        }
    //        SqlCommand cmd = new SqlCommand("Inspection_Inspection_JVS_Bill_Presentation_Status_Details", conStr);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text.ToString());
    //        // cmd.Parameters.AddWithValue("@godownID", Session["Godown_ID"].ToString());
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da.Fill(dt);
    //        if (conStr.State == ConnectionState.Open)
    //        { conStr.Close(); }

    //        if (dt.Rows.Count > 0)
    //        {
    //            ddlJVSbilltillthemonthpresented.SelectedValue = dt.Rows[0]["JVS_bill_till_the_month_presented"].ToString().Trim();
    //            ddlPaymenttowarehouseoperatorsbymonth.SelectedValue = dt.Rows[0]["Payment_to_warehouse_operators_by_month"].ToString().Trim();
    //            txtpayamoutlatereason.Text = dt.Rows[0]["Payment_pending_reason"].ToString().Trim();
    //            txt201920duetocut.Text = dt.Rows[0]["Cut_off_from_JVS_due"].ToString().Trim();
    //            txtReductionamount.Text = dt.Rows[0]["Reduction_amount_and_adjustment_status"].ToString().Trim();


    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
    //    }
    //    finally
    //    { if (con.State == ConnectionState.Open) { con.Close(); } }
    //}

    protected void btnCapacityQntlIn_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCol2.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField CT_ID = row.FindControl("CT_ID") as HiddenField;
                TextBox txtnoofgodown = row.FindControl("txtnoofgodown") as TextBox;
                TextBox txtStorage_Capacity = row.FindControl("txtStorage_Capacity") as TextBox;
                TextBox txtStored_Bag = row.FindControl("txtStored_Bag") as TextBox;
                TextBox txtStored_Weight = row.FindControl("txtStored_Weight") as TextBox;
                TextBox txtPercentage_Of_Utility = row.FindControl("txtPercentage_Of_Utility") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Review_of_capacity_and_utility_of_the_branch_capacity_in_Qntl_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@No_Of_Godown", txtnoofgodown.Text.ToString());
                    cmd.Parameters.AddWithValue("@Capacity_type_ID", CT_ID.Value);
                    cmd.Parameters.AddWithValue("@Storage_Capacity", txtStorage_Capacity.Text.ToString());
                    cmd.Parameters.AddWithValue("@Stored_Bag", txtStored_Bag.Text);
                    cmd.Parameters.AddWithValue("@Stored_Weight", txtStored_Weight.Text);
                    cmd.Parameters.AddWithValue("@Percentage_Of_Utility", txtPercentage_Of_Utility.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        Getcapacity_in_Qntl();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btnCapacityInMT_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCol3.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField CT_ID = row.FindControl("CT_ID") as HiddenField;
                TextBox txtReg_No_Of_Godown = row.FindControl("txtReg_No_Of_Godown") as TextBox;
                TextBox txtReg_Storage_Capacity = row.FindControl("txtReg_Storage_Capacity") as TextBox;
                TextBox txtOffered_No_Of_Godown = row.FindControl("txtOffered_No_Of_Godown") as TextBox;
                TextBox txtOffered_Storage_Capacity = row.FindControl("txtOffered_Storage_Capacity") as TextBox;
                TextBox txtContracted_No_Of_Godown = row.FindControl("txtContracted_No_Of_Godown") as TextBox;
                TextBox txtContracted_Storage_Capacity = row.FindControl("txtContracted_Storage_Capacity") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Details_of_online_entries_at_the_branch_Capacity_In_MT_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Reg_No_Of_Godown", txtReg_No_Of_Godown.Text.ToString());
                    cmd.Parameters.AddWithValue("@Capacity_type_ID", CT_ID.Value);
                    cmd.Parameters.AddWithValue("@Reg_Storage_Capacity", txtReg_Storage_Capacity.Text.ToString());
                    cmd.Parameters.AddWithValue("@Offered_No_Of_Godown", txtOffered_No_Of_Godown.Text);
                    cmd.Parameters.AddWithValue("@Offered_Storage_Capacity", txtOffered_Storage_Capacity.Text);
                    cmd.Parameters.AddWithValue("@Contracted_No_Of_Godown", txtContracted_No_Of_Godown.Text);
                    cmd.Parameters.AddWithValue("@Contracted_Storage_Capacity", txtContracted_Storage_Capacity.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        Getcapacity_in_MT();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void brnDescriptionofpesticides_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCol4.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField PesticideID = row.FindControl("PesticideID") as HiddenField;
                TextBox txtnoofgodown = row.FindControl("txtnoofgodown") as TextBox;
                TextBox txtStorage_Capacity = row.FindControl("txtStorage_Capacity") as TextBox;
                TextBox txtStored_Weight = row.FindControl("txtStored_Weight") as TextBox;
                TextBox txtInsecticideExpiryDate = row.FindControl("txtInsecticideExpiryDate") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Details_of_pesticides_available_at_the_branch_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pesticides_type_ID", PesticideID.Value);
                    cmd.Parameters.AddWithValue("@Quantity", txtnoofgodown.Text);
                    cmd.Parameters.AddWithValue("@Physical_verification_quantity", txtStorage_Capacity.Text.ToString());
                    cmd.Parameters.AddWithValue("@Expiry_Date", getDate_MDY(txtInsecticideExpiryDate.Text));
                    cmd.Parameters.AddWithValue("@Additional_requirement_for_the_year", txtStored_Weight.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        pesticidesDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btncapeinspectiondetails_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in CapeGrd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField CIT_ID = row.FindControl("CIT_ID") as HiddenField;
                TextBox txtNumber_of_Uses = row.FindControl("txtNumber_of_Uses") as TextBox;
                TextBox txtRemaining_Useful_Number_Quantity = row.FindControl("txtRemaining_Useful_Number_Quantity") as TextBox;
                TextBox txtRemaining_Unusable_Number_Quantity = row.FindControl("txtRemaining_Unusable_Number_Quantity") as TextBox;
                TextBox txtAdditional_Requirement = row.FindControl("txtAdditional_Requirement") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Details_of_Cape_Inspection_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Cape_type_ID", CIT_ID.Value);
                    cmd.Parameters.AddWithValue("@Number_of_Uses", txtNumber_of_Uses.Text);
                    cmd.Parameters.AddWithValue("@Remaining_Useful_Number_Quantity", txtRemaining_Useful_Number_Quantity.Text.ToString());
                    cmd.Parameters.AddWithValue("@Remaining_Unusable_Number_Quantity", txtRemaining_Unusable_Number_Quantity.Text);
                    cmd.Parameters.AddWithValue("@Additional_Requirement", txtAdditional_Requirement.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@Fire_brigade_Details", "");
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        CapeDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btnstatusofinsecticide_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grdstatusofinsecticide.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField CIT_ID = row.FindControl("CIT_ID") as HiddenField;
                TextBox txtNumber_of_total_stakes = row.FindControl("txtNumber_of_total_stakes") as TextBox;
                TextBox txtNumber_of_fumigation_Stackes = row.FindControl("txtNumber_of_fumigation_Stackes") as TextBox;
                TextBox txtRemaining_Stake_Number_for_Fumigation = row.FindControl("txtRemaining_Stake_Number_for_Fumigation") as TextBox;
                TextBox txtFumigation_date = row.FindControl("txtFumigation_date") as TextBox;
                TextBox txtNumber_of_Stackes_sprayed = row.FindControl("txtNumber_of_Stackes_sprayed") as TextBox;
                TextBox txtRemaining_Stake_Number_for_sprayed = row.FindControl("txtRemaining_Stake_Number_for_sprayed") as TextBox;
                TextBox txtsprayed_date = row.FindControl("txtsprayed_date") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Description_of_insecticide_status_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Capecity_type_ID", CIT_ID.Value);
                    cmd.Parameters.AddWithValue("@Number_of_total_stakes", txtNumber_of_total_stakes.Text);
                    cmd.Parameters.AddWithValue("@Number_of_fumigation_Stackes", txtNumber_of_fumigation_Stackes.Text.ToString());
                    cmd.Parameters.AddWithValue("@Remaining_Stake_Number_for_Fumigation", txtRemaining_Stake_Number_for_Fumigation.Text);
                    cmd.Parameters.AddWithValue("@Fumigation_date", getDate_MDY(txtFumigation_date.Text));
                    cmd.Parameters.AddWithValue("@Number_of_Stackes_sprayed", txtNumber_of_Stackes_sprayed.Text);
                    cmd.Parameters.AddWithValue("@Remaining_Stake_Number_for_sprayed", txtRemaining_Stake_Number_for_sprayed.Text);
                    cmd.Parameters.AddWithValue("@sprayed_date", getDate_MDY(txtsprayed_date.Text));
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        Getstatusofinsecticide();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btnGrdinsecticidestatus_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grdinsecticidestatus.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField hdnid = row.FindControl("hdnid") as HiddenField;
                HiddenField CIT_ID = row.FindControl("CIT_ID") as HiddenField;
                TextBox txtTotal_number_of_storages_in_the_warehouse = row.FindControl("txtTotal_number_of_storages_in_the_warehouse") as TextBox;
                TextBox txtSteak_C = row.FindControl("txtSteak_C") as TextBox;
                TextBox txtSteak_F = row.FindControl("txtSteak_F") as TextBox;
                TextBox txtSteak_H = row.FindControl("txtSteak_H") as TextBox;
                TextBox txtAction_in_case_of_insecticide = row.FindControl("txtAction_in_case_of_insecticide") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Description_of_insecticide_status_in_the_warehouse_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Capecity_type_ID", CIT_ID.Value);
                    cmd.Parameters.AddWithValue("@Total_number_of_storages_in_the_warehouse", txtTotal_number_of_storages_in_the_warehouse.Text);
                    cmd.Parameters.AddWithValue("@Steak_C", txtSteak_C.Text);
                    cmd.Parameters.AddWithValue("@Steak_F", txtSteak_F.Text);
                    cmd.Parameters.AddWithValue("@Steak_H", txtSteak_H.Text);
                    cmd.Parameters.AddWithValue("@Action_in_case_of_insecticide", txtAction_in_case_of_insecticide.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        Getinsecticidestatus();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btnGrdstoragedutysubmision_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grdstoragedutysubmision.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField hdnid = row.FindControl("hdnid") as HiddenField;
                HiddenField CIT_ID = row.FindControl("CIT_ID") as HiddenField;
                TextBox txtBill_Number = row.FindControl("txtBill_Number") as TextBox;
                TextBox txtBill_Date = row.FindControl("txtBill_Date") as TextBox;
                TextBox txtBill_Pay_date = row.FindControl("txtBill_Pay_date") as TextBox;
                TextBox txtBill_Payment_Amount = row.FindControl("txtBill_Payment_Amount") as TextBox;
                TextBox txtBill_Payment_received_Date = row.FindControl("txtBill_Payment_received_Date") as TextBox;
                TextBox txtPayment_Amount = row.FindControl("txtPayment_Amount") as TextBox;
                TextBox txtPending_Amount = row.FindControl("txtPending_Amount") as TextBox;
                TextBox txtPayment_pending_reason = row.FindControl("txtPayment_pending_reason") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Statement_of_storage_duty_submission_status_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Depositor_Name_ID", CIT_ID.Value);
                    cmd.Parameters.AddWithValue("@Bill_Number", txtBill_Number.Text);
                    cmd.Parameters.AddWithValue("@Bill_Date", getDate_MDY(txtBill_Date.Text));
                    cmd.Parameters.AddWithValue("@Bill_Pay_date", getDate_MDY(txtBill_Pay_date.Text));
                    cmd.Parameters.AddWithValue("@Bill_Payment_received_Date", getDate_MDY(txtBill_Payment_received_Date.Text));
                    cmd.Parameters.AddWithValue("@Bill_Payment_Amount", txtBill_Payment_Amount.Text);
                    cmd.Parameters.AddWithValue("@Payment_Amount", txtPayment_Amount.Text);
                    cmd.Parameters.AddWithValue("@Pending_Amount", txtPending_Amount.Text);
                    cmd.Parameters.AddWithValue("@Payment_pending_reason", txtPayment_pending_reason.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        Getstoragedutysubmision();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    //protected void btnJVSBillAmountDetails_Click(object sender, EventArgs e)
    //{

    //    string ipAddress;
    //    ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (ipAddress == "" || ipAddress == null)
    //        ipAddress = Request.ServerVariables["REMOTE_ADDR"];

    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    //        SqlCommand cmd = new SqlCommand("Inspection_JVS_Bill_Presentation_Status_Details_Insert", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        con.Open();
    //        cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
    //        cmd.Parameters.AddWithValue("@JVS_bill_till_the_month_presented", ddlJVSbilltillthemonthpresented.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Payment_to_warehouse_operators_by_month", ddlPaymenttowarehouseoperatorsbymonth.SelectedValue);
    //        cmd.Parameters.AddWithValue("@Payment_pending_reason", txtpayamoutlatereason.Text);
    //        cmd.Parameters.AddWithValue("@Cut_off_from_JVS_due", txt201920duetocut.Text);
    //        cmd.Parameters.AddWithValue("@Reduction_amount_and_adjustment_status", txtReductionamount.Text);
    //        cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
    //        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //        if (TheResult.StartsWith("SUCCESS"))
    //        {
    //            string strMsg = "Inspection Details Successfully submitted |||";
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //            Getstoragedutysubmision();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    protected void btnCaseBalance_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdCaseBalance.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField hdnid = row.FindControl("hdnid") as HiddenField;
                HiddenField MCBSD_ID = row.FindControl("MCBSD_ID") as HiddenField;
                TextBox txtAmount_according_to_record = row.FindControl("txtAmount_according_to_record") as TextBox;
                TextBox txtAmount_found_in_physical_verification = row.FindControl("txtAmount_found_in_physical_verification") as TextBox;
                TextBox txtDifference = row.FindControl("txtDifference") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_Cash_balance_status_details_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Case_Name_ID", MCBSD_ID.Value);
                    cmd.Parameters.AddWithValue("@Amount_according_to_record", txtAmount_according_to_record.Text);
                    cmd.Parameters.AddWithValue("@Amount_found_in_physical_verification", txtAmount_found_in_physical_verification.Text);
                    cmd.Parameters.AddWithValue("@Difference", txtDifference.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        GetCaseBalanceDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    protected void btnEmployeeDetails_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdEmployeeDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                // HiddenField hdnid = row.FindControl("hdnid") as HiddenField;
                HiddenField Dsgn_ID = row.FindControl("Dsgn_ID") as HiddenField;
                TextBox txtNumber_of_Employee = row.FindControl("txtNumber_of_Employee") as TextBox;
                TextBox txtDate_of_joining = row.FindControl("txtDate_of_joining") as TextBox;
                TextBox txtJoining_Year = row.FindControl("txtJoining_Year") as TextBox;
                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Inspection_List_of_all_the_employees_posted_at_the_branch_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    //cmd.Parameters.AddWithValue("@ID", hdnid.Value);
                    cmd.Parameters.AddWithValue("@Inspection_ID", lblinspid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Designation_ID", Dsgn_ID.Value);
                    cmd.Parameters.AddWithValue("@Number_of_Employee", txtNumber_of_Employee.Text);
                    cmd.Parameters.AddWithValue("@Date_of_joining", getDate_MDY(txtDate_of_joining.Text));
                    cmd.Parameters.AddWithValue("@Joining_Year", txtJoining_Year.Text);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@IP_Address", ipAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Inspection Details Successfully submitted |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        GetEmployeeDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                }
                catch (Exception ex)
                {

                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
  
    protected void btnFinalSubmittion_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Inspection_Final_Submit_by_Officer_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Insp_Officer_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Inspection_Id", lblinspid.Text.ToString());
            cmd.Parameters.AddWithValue("@Branch_Id", Session["hdnbranchid"].ToString());
            cmd.Parameters.AddWithValue("@Financial_Year", Session["hdnfinancialYear"].ToString());
            cmd.Parameters.AddWithValue("@Quater_Type", Session["hdninsptype"].ToString());
            cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
            cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Final Details Successfully submitted By Inspection Officer |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                Getstoragedutysubmision();
                CHECKFINALSTATUS();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
        catch (Exception ex)
        {

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
            Console.WriteLine(ex.Message);
        }
    }
}