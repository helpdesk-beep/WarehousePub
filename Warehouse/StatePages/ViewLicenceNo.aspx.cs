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

public partial class StatePages_ViewLicenceNo : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                // getdistrict();
                getdistrict2();
                getdistrictwdra();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    // public void getdistrict()
    //{
    //    string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddldistrict.DataSource = ds.Tables[0];
    //        ddldistrict.DataTextField = "District_Name";
    //        ddldistrict.DataValueField = "District_Id";
    //        ddldistrict.DataBind();
    //        ddldistrict.Items.Insert(0, "--Select--");
    //    }
    //}

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
            // using (SqlCommand cmd = new SqlCommand("Get_Update_Licence_Details", con))
            using (SqlCommand cmd = new SqlCommand("Get_Licence_Details_By_Licence_number", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Licencenumber", txtLicenceNo.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //using (DataTable dt = new DataTable())
                    DataSet ds = new DataSet();
                    {
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
    }

    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGrid();
    //}
    protected void Display(object sender, EventArgs e)
    {
        divNewInsp.Visible = true;
        divWDRA.Visible = false;
        ModalPopupExtender1.Show();
    }
    protected void Display2(object sender, EventArgs e)
    {
        divWDRA.Visible = true;
        divNewInsp.Visible = false;
        ModalPopupExtender1.Show();
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    public void checkvalidation()
    {
        if (ddldst2.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District Name')", true);
            ddldst2.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtApplicationCode.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Application Code can not be blank')", true);
            txtApplicationCode.Focus();
            return;
        }
        if (string.IsNullOrEmpty(lblgodownname.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Name can not be blank')", true);
            lblgodownname.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWMN.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Man Name can not be blank')", true);
            txtWMN.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLicenseCode.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('License Code can not be blank')", true);
            txtLicenseCode.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLCMT.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Licensed Capacity (MT) can not be blank')", true);
            txtLCMT.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtIssuDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issuance Date can not be blank')", true);
            txtIssuDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtValidTill.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Valid Till can not be blank')", true);
            txtValidTill.Focus();
            return;
        }

    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
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
                    string strMsg = "Holder Register Details Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    // Clear();
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
                fillGrid();
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
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
            // checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
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
                    string strMsg = "WDRA Register Details Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    // Clear();
                    //fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
                // fillGrid();
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Show();
    //}
}
