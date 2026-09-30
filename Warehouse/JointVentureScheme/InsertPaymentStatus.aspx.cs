using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_InsertPaymentStatus : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (Session["UserName"] != null)
        {
            if (!IsPostBack) { InitializeDataTable(); }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }

    protected void btnHome_Click(object sender, EventArgs e) { Response.Redirect("DistrictWiseJVSOffer.aspx"); }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        // Optional: Clear session if this is treated as a logout
        Session.Abandon();

        // Redirect to the login page
         Response.Redirect("Logins.aspx"); 
}

    private void InitializeDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {
            new DataColumn("CategoryName"), new DataColumn("PaymentMode"),
            new DataColumn("BankReferenceNo"), new DataColumn("TransactionDate"),
            new DataColumn("Amount"), new DataColumn("Status"),
            new DataColumn("REGISTRATIONID"), new DataColumn("NAMEOFDEPOSITOR"),
            new DataColumn("CONTACTNO"), new DataColumn("EMAILID"),
            new DataColumn("FEE"), new DataColumn("Remarks")
        });
        ViewState["PaymentTable"] = dt;
    }

    private void BindGrid()
    {
        DataTable dt = (DataTable)ViewState["PaymentTable"];
        gvPayments.DataSource = dt;
        gvPayments.DataBind();
        btnFinalSubmit.Visible = (dt != null && dt.Rows.Count > 0);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        // PARTICULAR FIELD VALIDATION
        string message = "";
        if (ddlCategory.SelectedIndex <= 0) message = "Please select a Category.";
        else if (ddlMode.SelectedIndex <= 0) message = "Please select a Payment Mode.";
        else if (string.IsNullOrWhiteSpace(txtBankRef.Text)) message = "Please enter Bank Reference No.";
        else if (string.IsNullOrWhiteSpace(txtRegID.Text)) message = "Please enter Registration ID.";
        else if (string.IsNullOrWhiteSpace(txtDepositor.Text)) message = "Please enter Depositor Name.";
        else if (string.IsNullOrWhiteSpace(txtContact.Text)) message = "Please enter Contact No.";
        else if (string.IsNullOrWhiteSpace(txtEmail.Text)) message = "Please enter Email ID.";
        else if (string.IsNullOrWhiteSpace(txtAmount.Text)) message = "Please enter Amount.";
        else if (string.IsNullOrWhiteSpace(txtFee.Text)) message = "Please enter Fee.";
        else if (string.IsNullOrWhiteSpace(txtDate.Text)) message = "Please select a Transaction Date.";
        else if (ddlStatus.SelectedIndex <= 0) message = "Please select a Status.";
        else if (string.IsNullOrWhiteSpace(txtRemarks.Text)) message = "Please enter Remarks.";

        if (message != "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
            return;
        }

        DataTable dt = (DataTable)ViewState["PaymentTable"];
        dt.Rows.Add(ddlCategory.SelectedValue, ddlMode.SelectedValue, txtBankRef.Text, txtDate.Text,
                    txtAmount.Text, ddlStatus.SelectedValue, txtRegID.Text, txtDepositor.Text,
                    txtContact.Text, txtEmail.Text, txtFee.Text, txtRemarks.Text);

        ViewState["PaymentTable"] = dt;
        BindGrid();
        ClearInputs();
    }

    private void ClearInputs()
    {
        ddlCategory.SelectedIndex = 0; ddlMode.SelectedIndex = 0; ddlStatus.SelectedIndex = 0;
        txtBankRef.Text = ""; txtRegID.Text = ""; txtDepositor.Text = ""; txtContact.Text = "";
        txtEmail.Text = ""; txtAmount.Text = ""; txtFee.Text = ""; txtDate.Text = ""; txtRemarks.Text = "";
    }

    protected void gvPayments_RowEditing(object sender, GridViewEditEventArgs e) { gvPayments.EditIndex = e.NewEditIndex; BindGrid(); }
    protected void gvPayments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) { gvPayments.EditIndex = -1; BindGrid(); }

    protected void gvPayments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            DropDownList ddlEditMode = (DropDownList)e.Row.FindControl("editMode");
            if (ddlEditMode != null)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string val = rowView["PaymentMode"].ToString();
                if (ddlEditMode.Items.FindByValue(val) != null) ddlEditMode.SelectedValue = val;
                else ddlEditMode.SelectedIndex = 0;
            }
        }
    }

    protected void gvPayments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["PaymentTable"];
        GridViewRow row = gvPayments.Rows[e.RowIndex];
        dt.Rows[e.RowIndex]["CategoryName"] = ((DropDownList)row.FindControl("editCategory")).SelectedValue;
        dt.Rows[e.RowIndex]["PaymentMode"] = ((DropDownList)row.FindControl("editMode")).SelectedValue;
        dt.Rows[e.RowIndex]["BankReferenceNo"] = ((TextBox)row.FindControl("editRefNo")).Text;
        dt.Rows[e.RowIndex]["Amount"] = ((TextBox)row.FindControl("editAmount")).Text;
        dt.Rows[e.RowIndex]["Status"] = ((DropDownList)row.FindControl("editStatus")).SelectedValue;
        dt.Rows[e.RowIndex]["REGISTRATIONID"] = ((TextBox)row.FindControl("editRegID")).Text;
        ViewState["PaymentTable"] = dt;
        gvPayments.EditIndex = -1;
        BindGrid();
    }

    protected void gvPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["PaymentTable"];
        dt.Rows.RemoveAt(e.RowIndex);
        ViewState["PaymentTable"] = dt;
        BindGrid();
    }

    protected void btnFinalSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["PaymentTable"];
        try
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                foreach (DataRow row in dt.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertPaymentStatus", sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CategoryName", row["CategoryName"]);
                        cmd.Parameters.AddWithValue("@PaymentMode", row["PaymentMode"]);
                        cmd.Parameters.AddWithValue("@BankReferenceNo", row["BankReferenceNo"]);
                        DateTime tDate;
                        cmd.Parameters.AddWithValue("@TransactionDate", DateTime.TryParse(row["TransactionDate"].ToString(), out tDate) ? (object)tDate : DBNull.Value);
                        decimal amt; decimal fee;
                        cmd.Parameters.AddWithValue("@Amount", decimal.TryParse(row["Amount"].ToString(), out amt) ? amt : 0);
                        cmd.Parameters.AddWithValue("@FEE", decimal.TryParse(row["FEE"].ToString(), out fee) ? fee : 0);
                        cmd.Parameters.AddWithValue("@Status", row["Status"]);
                        cmd.Parameters.AddWithValue("@REGISTRATIONID", row["REGISTRATIONID"]);
                        cmd.Parameters.AddWithValue("@NAMEOFDEPOSITOR", row["NAMEOFDEPOSITOR"]);
                        cmd.Parameters.AddWithValue("@CONTACTNO", row["CONTACTNO"]);
                        cmd.Parameters.AddWithValue("@EMAILID", row["EMAILID"]);
                        cmd.Parameters.AddWithValue("@Remarks", row["Remarks"]);
                        cmd.Parameters.AddWithValue("@Update_By", "Admin");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Records Saved Successfully!');", true);
            InitializeDataTable(); BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
        }
    }
}