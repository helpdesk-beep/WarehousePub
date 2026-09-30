using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JointVentureScheme_UpdatePaymentStatus : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (Session["UserName"] != null)
        {
            if (!IsPostBack) { lbluser.Text = Session["UserName"].ToString(); }
        }
        else { Response.Redirect("Logins.aspx"); }
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistrictWiseJVSOffer.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtRegID.Text) && string.IsNullOrEmpty(txtBankRefNo.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please enter Registration ID or Bank Ref No');", true);
            pnlGridArea.Visible = false;
            return;
        }
        Search();
        pnlGridArea.Visible = true; // Show grid area after search
        TRHide.Visible = false;      // Hide edit panel if search is performed again
    }

    public void Search()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("Get_Payment_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@REGISTRATIONID", string.IsNullOrEmpty(txtRegID.Text) ? "0" : txtRegID.Text.Trim());
            cmd.Parameters.AddWithValue("@BankReferenceNo", string.IsNullOrEmpty(txtBankRefNo.Text) ? "0" : txtBankRefNo.Text.Trim());

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            gvGodown.DataSource = dt;
            gvGodown.DataBind();
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        TRHide.Visible = true; // Show the single-row edit panel
        GridViewRow row = gvGodown.SelectedRow;
        hdnTid.Value = gvGodown.DataKeys[row.RowIndex].Value.ToString();

        // 1. Populate Category (Cell 1)
        string cat = HttpUtility.HtmlDecode(row.Cells[1].Text).Trim();
        if (ddlCategory.Items.FindByValue(cat) != null) ddlCategory.SelectedValue = cat;

        // 2. Populate Bank Ref - Read Only (Cell 3)
        txtBankRefEdit.Text = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();

        // 3. Populate Amount (Cell 5)
        txtFees.Text = HttpUtility.HtmlDecode(row.Cells[5].Text).Replace(",", "").Trim();

        // 4. Populate Registration ID (Cell 7)
        txtRegistrationID.Text = HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "scroll", "window.scrollTo(0,document.body.scrollHeight);", true);
    }

    protected void btnUpdateCpt_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("Update_Payment_Status", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@TId", hdnTid.Value);
                cmd.Parameters.AddWithValue("@CategoryName", ddlCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@REGISTRATIONID", txtRegistrationID.Text.Trim());
                cmd.Parameters.AddWithValue("@FEE", txtFees.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount", txtFees.Text.Trim());
                cmd.Parameters.AddWithValue("@UpdateBy", Request.ServerVariables["REMOTE_ADDR"]);

                SqlParameter outputParam = new SqlParameter("@TheResult", SqlDbType.VarChar, 250) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputParam);
                cmd.ExecuteNonQuery();

                if (outputParam.Value.ToString().StartsWith("SUCCESS")) { ModalPopupExtender1.Show(); }
                else { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('" + outputParam.Value + "');", true); }
            }
        }
        catch (Exception ex) { ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "err", "alert('" + ex.Message.Replace("'", "") + "');", true); }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        // Resets the page state: Hides panels and clears text
        txtRegID.Text = "";
        txtBankRefNo.Text = "";
        pnlGridArea.Visible = false;
        TRHide.Visible = false;
        gvGodown.DataSource = null;
        gvGodown.DataBind();
 	txtGridSearch.Text = "";
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Logins.aspx");
    }
}