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


public partial class Inspections_RO_View_Fill_Inspection_By_Officer : System.Web.UI.Page
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
            cmd.Parameters.AddWithValue("@Inspection_Id", lblinspid.Text.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {

            }
            else
            {

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
            cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
            cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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

    protected void Getcapacity_in_Qntl()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Inspection_capacity_and_utility_of_the_branch_capacity_in_Qntl", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InspectionID", lblinspid.Text);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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
                cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnEmpID"].ToString());
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

}