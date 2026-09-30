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

public partial class StatePages_PaymentStatus_int : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                paymentmode();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

  
    protected void Display(object sender, EventArgs e)
    {
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    public void paymentmode()
    {
        string qry = "Select distinct PaymentMode from tbl_Payment_Status";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlPaymentMode.DataSource = ds.Tables[0];
            ddlPaymentMode.DataTextField = "PaymentMode";
            ddlPaymentMode.DataValueField = "PaymentMode";
            ddlPaymentMode.DataBind();
            ddlPaymentMode.Items.Insert(0, "--Select--");
        }
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
        if (ddlCategoryName.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District Name')", true);
            ddlCategoryName.Focus();
            return;
        }
        if (ddlPaymentMode.SelectedValue == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District Name')", true);
            ddlPaymentMode.Focus();
            return;
        }
        
        if (string.IsNullOrEmpty(lblBankReferenceNo.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Name can not be blank')", true);
            lblBankReferenceNo.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txAmount.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Warehouse Man Name can not be blank')", true);
            txAmount.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtStatus.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('License Code can not be blank')", true);
            txtStatus.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtREGISTRATIONID.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Licensed Capacity (MT) can not be blank')", true);
            txtREGISTRATIONID.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtTransactionDate.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issuance Date can not be blank')", true);
            txtTransactionDate.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtNAMEOFDEPOSITOR.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Valid Till can not be blank')", true);
            txtNAMEOFDEPOSITOR.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtCONTACTNO.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issuance Date can not be blank')", true);
            txtCONTACTNO.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtEMAILID.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Valid Till can not be blank')", true);
            txtEMAILID.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtFEE.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issuance Date can not be blank')", true);
            txtFEE.Focus();
            return;
        }
        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Valid Till can not be blank')", true);
            txtRemarks.Focus();
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
                SqlCommand cmd = new SqlCommand("Insert_Payment_Status", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@CategoryName", ddlCategoryName.SelectedValue);
                cmd.Parameters.AddWithValue("@PaymentMode", ddlPaymentMode.SelectedValue);
                cmd.Parameters.AddWithValue("@BankReferenceNo", lblBankReferenceNo.Text);
                cmd.Parameters.AddWithValue("@TransactionDate", getDate_MDY(txtTransactionDate.Text));
                cmd.Parameters.AddWithValue("@Amount", txAmount.Text);
                cmd.Parameters.AddWithValue("@Status", "Completed Successfully");
                cmd.Parameters.AddWithValue("@REGISTRATIONID", txtREGISTRATIONID.Text);
                cmd.Parameters.AddWithValue("@NAMEOFDEPOSITOR", txtNAMEOFDEPOSITOR.Text);
                cmd.Parameters.AddWithValue("@CONTACTNO", txtCONTACTNO.Text);
                cmd.Parameters.AddWithValue("@EMAILID", txtEMAILID.Text);
                cmd.Parameters.AddWithValue("@FEE", txtFEE.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Holder Register Details Successfully Submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                   
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
              
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }


  
}
