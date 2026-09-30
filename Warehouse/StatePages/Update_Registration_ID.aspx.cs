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
using MPSCSC_GodownDetails;

public partial class StatePages_Update_Registration_ID : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                getdistrict();
                fillGodownType();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(DropDownList1.SelectedValue.ToString());
    }
    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    public void GetRegID()
    {
        ddlRegID.DataSource = "";
        string qry = "";
        qry = "select Registration_ID,UPPER(Warehouse_name) +' ( '+ Registration_ID +' )' as Warehouse_name from tbl_warehouseRegistration as WREG where WREG.BranchId='" + ddlBranch.SelectedValue + "' and RegCapacity>0 order by Warehouse_name";
        SqlCommand cmd = new SqlCommand(qry, JVScon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegID.DataSource = ds.Tables[0];
            ddlRegID.DataTextField = "Warehouse_name";
            ddlRegID.DataValueField = "Registration_ID";
            ddlRegID.DataBind();
            ddlRegID.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlRegID.Items.Insert(0, "--Select--");
        }
    }
    public void GetBranchData()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Data_For_Updatation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                trmobtxt.Visible = true;
                tr1.Visible = true;
                tr2.Visible = true;
                trbtnhide.Visible = true;
                txtGdwnID.Text = "";
                txtclosing.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
                trmobtxt.Visible = false;
                trbtnhide.Visible = false;
                tr1.Visible = false;
                tr2.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
                txtGdwnID.Text = "";
                txtclosing.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            }
            else if (txtclosing.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Closing Balance')", true);
            }
            else if (txtlicno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Licence Expiry Date')", true);
            }
            else if (txtlicdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Licence Expiry Date')", true);
            }
            else if (txtscieCPT.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Scientific Capacity')", true);
            }
            else if (ddlRegID.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select JVS Registration ID')", true);
            }
            else
            {
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Insert_Gdn_Data_in_2023", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Name", lblgodownname.Text);
                cmd.Parameters.AddWithValue("@Closing_Balance", txtclosing.Text.Trim());
                cmd.Parameters.AddWithValue("@LicNum", txtlicno.Text);
                cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                cmd.Parameters.AddWithValue("@Hired_Type", ddllst_hired.SelectedValue);
                cmd.Parameters.AddWithValue("@Storage_Type", ddllst_storage.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", txtscieCPT.Text);
                cmd.Parameters.AddWithValue("@Godown_Capacity", txtMaxCpt.Text);
                cmd.Parameters.AddWithValue("@IsActive", ddlactiveinactive.SelectedValue);
                cmd.Parameters.AddWithValue("@Maintain_By", ddlmaintainby.SelectedValue);
                cmd.Parameters.AddWithValue("@jvseRegid", ddlRegID.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Insert Successfully')", true);
                    sqltrans.Commit();
                    trmobtxt.Visible = false;
                    trbtnhide.Visible = false;
                    lblgodownname.Text = "";
                    txtGdwnID.Text = "";
                    txtclosing.Text = "";
                    txtlicno.Text = "";
                    txtlicdate.Text = "";
                    Depositor_Gridview.DataSource = "";
                    Depositor_Gridview.DataBind();
                    GetBranchData();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Inserted')", true);

                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("lblGodown_ID") as Label).Text;
        txtMaxCpt.Text = (row.FindControl("lblGodown_Capacity") as Label).Text; ;
        txtscieCPT.Text = (row.FindControl("lblGodown_Scientific_Capacity") as Label).Text;
        fillGodownType();
        ddllst_hired.SelectedValue = (row.FindControl("lblHired_Type") as Label).Text;
        if ((row.FindControl("lblStorage_Type") as Label).Text.Equals("SiloBags"))
            (row.FindControl("lblStorage_Type") as Label).Text = "Silo Bags";
        ddllst_storage.SelectedValue = (row.FindControl("lblStorage_Type") as Label).Text;
        txtclosing.Text = (row.FindControl("lblClosing_Balance") as Label).Text;
        txtlicno.Text = (row.FindControl("lblLicNum") as Label).Text;
        txtlicdate.Text = (row.FindControl("lblLicDate") as Label).Text;
        divNewInsp.Visible = true;
        GetRegID();
        ModalPopupExtender1.Show();
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllst_hired.Items.Clear();
                ddllst_hired.DataSource = ds.Tables[0];
                ddllst_hired.DataTextField = "GodownType";
                ddllst_hired.DataValueField = "GodownType";
                ddllst_hired.DataBind();
            }
            else
            {

            }
        }
        catch (Exception)
        {
        }
    }
}