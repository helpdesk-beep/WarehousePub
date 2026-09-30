using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Rpt_PEG_Godown_Bill_Details_For_Region : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Region_ID"].ToString() != ""))
        {
            if (!IsPostBack)
            {
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_PEG_Godown_Bill_Details_For_Region", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdRegion.DataSource = dt;
                GrdRegion.DataBind();
            }
            else
            {
                GrdRegion.DataSource = null;
                GrdRegion.DataBind();
                GrdRegion.Visible = true;
            }
        }
    }
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

        if (ddl.SelectedValue == "N") // Reject selected
        {
            txtRemark.Visible = true;
            txtRemark.Text = "";
        }
        else
        {
            txtRemark.Visible = false;
            txtRemark.Text = "NA";
        }
    }
    protected void GrdRegion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            string qry = "";
            int ICount = 0;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            con.Open();
            DropDownList drpApprovalStatus = (DropDownList)row.FindControl("ddlApprovalStatus");
            string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
            decimal RM_Other_Deduction = Convert.ToDecimal(((TextBox)row.FindControl("txtOther_Deduction")).Text);
            decimal RM_Payable_Amount = Convert.ToDecimal(((Label)row.FindControl("lblPayable_Amount")).Text);
            string Remark = (row.FindControl("txtRemark") as TextBox).Text;

            UpdateRow(Bill_Number.ToString(), drpApprovalStatus.SelectedValue.ToString(), Remark.ToString(),
                      RM_Other_Deduction, RM_Payable_Amount);
        }
        else if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Region_Vaccant_Capacity_Print_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public void UpdateRow(string BillNumber, string ApprovalStatus, string Remark, decimal RM_Other_Deduction, Decimal RM_Payable_Amount)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("Usp_Update_RM_Approval_For_BOT_Bill", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
            cmd.Parameters.AddWithValue("@RM_Approval_Status", ApprovalStatus);
            cmd.Parameters.AddWithValue("@RM_Other_Deduction", RM_Other_Deduction);
            cmd.Parameters.AddWithValue("@RM_Payble_Amount", RM_Payable_Amount);
            cmd.Parameters.AddWithValue("@Remark", Remark);
            cmd.Parameters.AddWithValue("@RM_Approval_By", ip);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully!');", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated!');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    protected void txtOther_Deduction_TextChanged(object sender, EventArgs e)
    {
        TextBox txtOther = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtOther.NamingContainer;

        // Net Amount
        //Label lblNet = (Label)row.FindControl("lblNet_Amount");
        //decimal netAmount = string.IsNullOrEmpty(lblNet.Text) ? 0 : Convert.ToDecimal(lblNet.Text);

        // TDS Amount
        //Label lblTds = (Label)row.FindControl("lblTDS_Amount");
        //decimal tdsAmount = string.IsNullOrEmpty(lblTds.Text) ? 0 : Convert.ToDecimal(lblTds.Text);

        // Account Payble Amount
        Label lblPayable_Amount = (Label)row.FindControl("lblPayable_Amount");
        decimal Account_Payable_Amount = string.IsNullOrEmpty(lblPayable_Amount.Text) ? 0 : Convert.ToDecimal(lblPayable_Amount.Text);

        // Other Deduction
        decimal otherDeduction = string.IsNullOrEmpty(txtOther.Text) ? 0 : Convert.ToDecimal(txtOther.Text);

        // Payable Amount
        decimal payable = Account_Payable_Amount - otherDeduction;

        // Set Payable Amount
        Label lblPayable = (Label)row.FindControl("lblPayable_Amount");
        lblPayable.Text = payable.ToString("0.00");
    }
}