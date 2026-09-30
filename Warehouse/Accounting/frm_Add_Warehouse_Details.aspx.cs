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
using System.Security.Principal;

public partial class Accounting_frm_Add_Warehouse_Details : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                iflicense.Visible = true;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void checkvalidation()
    {
        if (ddlWHT.SelectedValue=="0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Type')", true);
            ddlWHT.Focus();
            return;
        }
        if (ddlOST.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Ownsership Type')", true);
            ddlOST.Focus();
            return;
        }
        if (ddlWST.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Storage Type')", true);
            ddlWST.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWHName.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Warehouse Name')", true);
            txtWHName.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWHON.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Name of warehouse owner')", true);
            txtWHON.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWHMN.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Warehouse Manger’s name')", true);
            txtWHMN.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWCN.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Warehouse Contact No')", true);
            txtWCN.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtWEID.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Warehouse email-Id')", true);
            txtWEID.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtpincode.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter 6 Digit Pin Code  of Warehouse')", true);
            txtpincode.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtSCMT.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Storage Capacity in Metric Tons(MT) of Warehouse')", true);
            txtSCMT.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLat.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Latitude of Warehouse')", true);
            txtLat.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtLog.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Longitude of Warehouse')", true);
            txtLog.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtAdd1.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Postal Address of Warehouse')", true);
            txtAdd1.Focus();
            return;
        }       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert_Warehouse_Details", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@WHID", Session["WHID"].ToString());
                cmd.Parameters.AddWithValue("@WHTID", ddlWHT.SelectedValue);
                cmd.Parameters.AddWithValue("@OSTID", ddlOST.SelectedValue);
                cmd.Parameters.AddWithValue("@WHSTID", ddlWST.SelectedValue);
                cmd.Parameters.AddWithValue("@WarehouseOwnerName", txtWHON.Text);
                cmd.Parameters.AddWithValue("@NameofWarehouseManager", txtWHMN.Text);
                cmd.Parameters.AddWithValue("@SelfOwnedContactNo", txtWCN.Text);
                cmd.Parameters.AddWithValue("@SelfOwnedEmailId", txtWEID.Text);
                //cmd.Parameters.AddWithValue("@StateCode", txtFromDate.Text);
                //cmd.Parameters.AddWithValue("@DistrictCode", txtCIServiceable.Text);
                cmd.Parameters.AddWithValue("@PostalAddress", txtAdd1.Text);
                cmd.Parameters.AddWithValue("@Pincode", txtpincode.Text);
                cmd.Parameters.AddWithValue("@WarehouseName", txtWHName.Text);
                cmd.Parameters.AddWithValue("@StorageCapacityMT", txtSCMT.Text);
                cmd.Parameters.AddWithValue("@LicenseObtained", ddliflicense.SelectedValue);
                cmd.Parameters.AddWithValue("@License_RegNo", txtLRN.Text);
                cmd.Parameters.AddWithValue("@LicenseValidity", getDate_MDY(txtVoL.Text));
                cmd.Parameters.AddWithValue("@Latitude", txtLat.Text);
                cmd.Parameters.AddWithValue("@Longitude", txtLog.Text);
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Warehouse Details Successfully Submitted|||";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Masters/Map_Godown.aspx';", true);
                }
                else
                {
                    string strMsg3 = "Warehouse Details Successfully Submitted|||";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg3 + "');window.location ='/warehouse/Masters/Map_Godown.aspx';", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void ddliflicense_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddliflicense.SelectedValue == "1")
        {
            iflicense.Visible = true;
        }
        else
        {
            iflicense.Visible = false;
        }
    }
}
