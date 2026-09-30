using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class StatePages_ViewLicenceNoNew : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            if (!IsPostBack)
            {
                getdistrict2();
                getdistrictwdra();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void getdistrict2()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldst2.DataSource = ds.Tables[0];
            ddldst2.DataTextField = "District_Name";
            ddldst2.DataValueField = "District_Id";
            ddldst2.DataBind();
            ddldst2.Items.Insert(0, "--Select--");
        }
    }

    public void getdistrictwdra()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWDRADst.DataSource = ds.Tables[0];
            ddlWDRADst.DataTextField = "District_Name";
            ddlWDRADst.DataValueField = "District_Id";
            ddlWDRADst.DataBind();
            ddlWDRADst.Items.Insert(0, "--Select--");
        }
    }

    protected void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Licence_Details_By_Licence_number", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Licencenumber", txtLicenceNo.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    DataTable tableA = ds.Tables[0];
                    DataTable tableB = ds.Tables[1];
                    if (tableA.Rows.Count > 0)
                    {
                        Depositor_Gridview.DataSource = tableA;
                        Depositor_Gridview.DataBind();
                        grdshow.Visible = true;
                        grdwdra.Visible = false;
                    }
                    else
                    {
                        grdshow.Visible = false;
                        grdwdra.Visible = true;
                        WDRA_GridView.DataSource = tableB;
                        WDRA_GridView.DataBind();
                    }
                }
            }
        }
    }

    protected void Display(object sender, EventArgs e)
    {
        ClearNonWDRAForm();
        divNewInsp.Visible = true;
        divWDRA.Visible = false;
        ModalPopupExtender1.Show();
    }

    protected void Display2(object sender, EventArgs e)
    {
        ClearWDRAForm();
        divWDRA.Visible = true;
        divNewInsp.Visible = false;
        ModalPopupExtender1.Show();
    }

    protected string getDate_MDY(string inDate)
    {
        if (string.IsNullOrEmpty(inDate)) return "";
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
        }
        catch
        {
            return "";
        }
    }

    public bool checkvalidation()
    {
        if (ddldst2.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District Name')", true);
            ddldst2.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtApplicationCode.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Application Code can not be blank')", true);
            txtApplicationCode.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(lblgodownname.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Name can not be blank')", true);
            lblgodownname.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtWMN.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Man Name can not be blank')", true);
            txtWMN.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtLicenseCode.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('License Code can not be blank')", true);
            txtLicenseCode.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtLCMT.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Licensed Capacity (MT) can not be blank')", true);
            txtLCMT.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtIssuDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issuance Date can not be blank')", true);
            txtIssuDate.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtValidTill.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Valid Till can not be blank')", true);
            txtValidTill.Focus();
            return false;
        }
        return true;
    }

    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        try
        {
            if (!checkvalidation()) return;

            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("tbl_LicencesRegistration_2020_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Whr_ID", txtLicenseCode.Text);
                cmd.Parameters.AddWithValue("@APPL_CODE", txtApplicationCode.Text);
                cmd.Parameters.AddWithValue("@Whr_Name", lblgodownname.Text);
                cmd.Parameters.AddWithValue("@DISTRICT_NAME", ddldst2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@District_ID", ddldst2.SelectedValue);
                cmd.Parameters.AddWithValue("@Total_capicity", txtLCMT.Text);
                cmd.Parameters.AddWithValue("@Name_of_Owner", txtWMN.Text);
                cmd.Parameters.AddWithValue("@IssuedAnugyptiDate", getDate_MDY(txtIssuDate.Text));
                cmd.Parameters.AddWithValue("@IssuedAnugyptivalidityDate", getDate_MDY(txtValidTill.Text));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Non-WDRA Details Successfully Submitted!')", true);
                    ClearNonWDRAForm();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtLicenceNo.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Licence Number')", true);
            txtLicenceNo.Focus();
            return;
        }
        else
        {
            fillGrid();
        }
    }

    protected void btnwdra_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlWDRADst.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select District')", true);
                return;
            }
            if (string.IsNullOrEmpty(txtwdrawhn.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Name cannot be blank')", true);
                return;
            }
            if (string.IsNullOrEmpty(txtwdralicencecode.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('License Code cannot be blank')", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("tbl_LicencesRegistration_WDRA_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@NameandAddress", txtwdraapplicationcode.Text);
                cmd.Parameters.AddWithValue("@WarehousemanName", txtwdrawhmn.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtwdramobileno.Text);
                cmd.Parameters.AddWithValue("@Capacity", txtwdralicencecapacity.Text);
                cmd.Parameters.AddWithValue("@WHCode", txtwdralicencecode.Text);
                cmd.Parameters.AddWithValue("@WarehouseName", txtwdrawhn.Text);
                cmd.Parameters.AddWithValue("@District", ddlWDRADst.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RegistrationDate", getDate_MDY(txtwdraissuedate.Text));
                cmd.Parameters.AddWithValue("@Validupto", getDate_MDY(txtwdravaliddate.Text));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('WDRA Details Successfully Submitted!')", true);
                    ClearWDRAForm();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    // ==================== NON-WDRA UPDATE METHODS ====================

    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateNonWDRA")
        {
            string licenseCode = e.CommandArgument.ToString();
            LoadNonWDRAForUpdate(licenseCode);
        }
    }

    private void LoadNonWDRAForUpdate(string licenseCode)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT  Whr_Name as Warehouse_Name, 
                                        Name_of_Owner as Warehouse_Man_Name, 
                                        Total_capicity as Licensed_Capacity_MT, 
                                        Whr_ID as License_Code, 
                                        APPL_CODE as Application_Code, 
                                        IssuedAnugyptiDate as Issuance_Date, 
                                        IssuedAnugyptivalidityDate as Valid_Till, 
                                        District_ID
                                        FROM tbl_LicencesRegistration_2020 
                                        WHERE Whr_ID = @LicenseCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LicenseCode", licenseCode);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        hdnNonWDRAUpdateLicenseCode.Value = licenseCode;

                        if (dr["District_ID"] != DBNull.Value)
                            ddldst2.SelectedValue = dr["District_ID"].ToString();

                        txtApplicationCode.Text = dr["Application_Code"].ToString();
                        lblgodownname.Text = dr["Warehouse_Name"].ToString();
                        txtWMN.Text = dr["Warehouse_Man_Name"].ToString();
                        txtLicenseCode.Text = dr["License_Code"].ToString();
                        txtLCMT.Text = dr["Licensed_Capacity_MT"].ToString();

                        if (dr["Issuance_Date"] != DBNull.Value)
                            txtIssuDate.Text = Convert.ToDateTime(dr["Issuance_Date"]).ToString("dd/MM/yyyy");

                        if (dr["Valid_Till"] != DBNull.Value)
                            txtValidTill.Text = Convert.ToDateTime(dr["Valid_Till"]).ToString("dd/MM/yyyy");

                        btnAddCompany.Visible = false;
                        btnUpdateNonWDRA.Visible = true;

                        divNewInsp.Visible = true;
                        divWDRA.Visible = false;
                        ModalPopupExtender1.Show();
                    }
                    dr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1",
                "alert('Error loading data: " + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    protected void btnUpdateNonWDRA_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(hdnNonWDRAUpdateLicenseCode.Value))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid License Code for update')", true);
                return;
            }

            if (!checkvalidation()) return;

            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("tbl_LicencesRegistration_2020_Update_New", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();

                cmd.Parameters.AddWithValue("@Original_Whr_ID", hdnNonWDRAUpdateLicenseCode.Value);
                cmd.Parameters.AddWithValue("@Whr_ID", txtLicenseCode.Text);
                cmd.Parameters.AddWithValue("@APPL_CODE", txtApplicationCode.Text);
                cmd.Parameters.AddWithValue("@Whr_Name", lblgodownname.Text);
                cmd.Parameters.AddWithValue("@DISTRICT_NAME", ddldst2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@District_ID", ddldst2.SelectedValue);
                cmd.Parameters.AddWithValue("@Total_capicity", txtLCMT.Text);
                cmd.Parameters.AddWithValue("@Name_of_Owner", txtWMN.Text);
                cmd.Parameters.AddWithValue("@IssuedAnugyptiDate", getDate_MDY(txtIssuDate.Text));
                cmd.Parameters.AddWithValue("@IssuedAnugyptivalidityDate", getDate_MDY(txtValidTill.Text));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('Non-WDRA Details Updated Successfully!');", true);
                    ClearNonWDRAForm();
                    ModalPopupExtender1.Hide();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    private void ClearNonWDRAForm()
    {
        ddldst2.SelectedIndex = 0;
        txtApplicationCode.Text = "";
        lblgodownname.Text = "";
        txtWMN.Text = "";
        txtLicenseCode.Text = "";
        txtLCMT.Text = "";
        txtIssuDate.Text = "";
        txtValidTill.Text = "";
        hdnNonWDRAUpdateLicenseCode.Value = "";
        btnAddCompany.Visible = true;
        btnUpdateNonWDRA.Visible = false;
    }

    // ==================== WDRA UPDATE METHODS ====================

    protected void WDRA_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateWDRA")
        {
            string licenseCode = e.CommandArgument.ToString();
            LoadWDRAForUpdate(licenseCode);
        }
    }

    private void LoadWDRAForUpdate(string licenseCode)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT District, WarehouseName, WarehousemanName, Capacity, 
                            WHCode, Mobile, RegistrationDate, Validupto, NameandAddress
                            FROM tbl_WDRA_Registration 
                            WHERE WHCode = @LicenseCode";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@LicenseCode", licenseCode);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        hdnWDRAUpdateLicenseCode.Value = licenseCode;

                        if (dr["District"] != DBNull.Value)
                            ddlWDRADst.SelectedValue = dr["District"].ToString();

                        txtwdrawhn.Text = dr["WarehouseName"].ToString();
                        txtwdrawhmn.Text = dr["WarehousemanName"].ToString();
                        txtwdralicencecapacity.Text = dr["Capacity"].ToString();
                        txtwdralicencecode.Text = dr["WHCode"].ToString();
                        txtwdramobileno.Text = dr["Mobile"].ToString();

                        if (dr["RegistrationDate"] != DBNull.Value)
                            txtwdraissuedate.Text = Convert.ToDateTime(dr["RegistrationDate"]).ToString("dd/MM/yyyy");

                        if (dr["Validupto"] != DBNull.Value)
                            txtwdravaliddate.Text = Convert.ToDateTime(dr["Validupto"]).ToString("dd/MM/yyyy");

                        if (dr["NameandAddress"] != DBNull.Value)
                            txtwdraapplicationcode.Text = dr["NameandAddress"].ToString();

                        btnwdra.Visible = false;
                        btnUpdateWDRA.Visible = true;

                        divWDRA.Visible = true;
                        divNewInsp.Visible = false;
                        ModalPopupExtender1.Show();
                    }
                    dr.Close();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1",
                "alert('Error loading data: " + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    protected void btnUpdateWDRA_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(hdnWDRAUpdateLicenseCode.Value))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid License Code for update')", true);
                return;
            }

            if (ddlWDRADst.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select District')", true);
                return;
            }

            if (string.IsNullOrEmpty(txtwdrawhn.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Name cannot be blank')", true);
                return;
            }

            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("tbl_LicencesRegistration_WDRA_Update_New", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();

                cmd.Parameters.AddWithValue("@Original_WHCode", hdnWDRAUpdateLicenseCode.Value);
                cmd.Parameters.AddWithValue("@NameandAddress", txtwdraapplicationcode.Text);
                cmd.Parameters.AddWithValue("@WarehousemanName", txtwdrawhmn.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtwdramobileno.Text);
                cmd.Parameters.AddWithValue("@Capacity", txtwdralicencecapacity.Text);
                cmd.Parameters.AddWithValue("@WHCode", txtwdralicencecode.Text);
                cmd.Parameters.AddWithValue("@WarehouseName", txtwdrawhn.Text);
                cmd.Parameters.AddWithValue("@District", ddlWDRADst.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RegistrationDate", getDate_MDY(txtwdraissuedate.Text));
                cmd.Parameters.AddWithValue("@Validupto", getDate_MDY(txtwdravaliddate.Text));
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('WDRA Details Updated Successfully!');", true);
                    ClearWDRAForm();
                    ModalPopupExtender1.Hide();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message.Replace("'", "\\'") + "')", true);
        }
    }

    private void ClearWDRAForm()
    {
        ddlWDRADst.SelectedIndex = 0;
        txtwdrawhn.Text = "";
        txtwdrawhmn.Text = "";
        txtwdralicencecapacity.Text = "";
        txtwdralicencecode.Text = "";
        txtwdramobileno.Text = "";
        txtwdraissuedate.Text = "";
        txtwdravaliddate.Text = "";
        txtwdraapplicationcode.Text = "";
        hdnWDRAUpdateLicenseCode.Value = "";
        btnwdra.Visible = true;
        btnUpdateWDRA.Visible = false;
    }
}