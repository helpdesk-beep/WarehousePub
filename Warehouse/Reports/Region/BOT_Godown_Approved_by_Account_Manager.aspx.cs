using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_Region_BOT_Godown_Approved_by_Account_Manager : System.Web.UI.Page
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
            //SqlCommand cmd = new SqlCommand("Get_Bot_Godown_Bill_Details_For_Region", con);
            SqlCommand cmd = new SqlCommand("Get_Bot_Godown_Bill_Details_For_Account_Manager", con);
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
            // Bill Number निकाले
            string billNumber = e.CommandArgument.ToString();

            // Row identify करो
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            // Hidden fields से IDs
            int districtId = Convert.ToInt32(((HiddenField)row.FindControl("hdnDistrict_Id")).Value);
            int branchId = Convert.ToInt32(((HiddenField)row.FindControl("hdnBranch_Id")).Value);
            // int godownId = ((HiddenField)row.FindControl("hdnGodown_Id").Value);
            string godownId = (row.FindControl("hdnGodown_Id") as HiddenField).Value;

            // बाकी values labels से
            decimal godownCapacity = Convert.ToDecimal(((Label)row.FindControl("lblGodownCapacity")).Text);
            decimal avlQty = Convert.ToDecimal(((Label)row.FindControl("lblAvlQty")).Text);
            decimal vacantCapacity = Convert.ToDecimal(((Label)row.FindControl("lblVacant_Capacity")).Text);
            string financialYear = ((Label)row.FindControl("lblFinancial_Year")).Text;
            string month = ((Label)row.FindControl("lblMonth")).Text;
            decimal netAmount = Convert.ToDecimal(((Label)row.FindControl("lblNet_Amount")).Text);
            decimal TDS_Amount = Convert.ToDecimal(((Label)row.FindControl("lblTDS_Amount")).Text);
            decimal Other_Deduction = Convert.ToDecimal(((TextBox)row.FindControl("txtOther_Deduction")).Text);
            decimal Payable_Amount = Convert.ToDecimal(((Label)row.FindControl("lblPayable_Amount")).Text);

            // Approve dropdown और Remark textbox
            string approvalStatus = ((DropDownList)row.FindControl("ddlApprovalStatus")).SelectedValue;
            string remark = ((TextBox)row.FindControl("txtRemark")).Text;

            // अब Insert function call करो
            InsertBillApproval(billNumber, districtId, branchId, godownId, godownCapacity,
                               avlQty, vacantCapacity, financialYear, month, netAmount, TDS_Amount, Other_Deduction, Payable_Amount,
                               approvalStatus, remark);
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
    private void InsertBillApproval(string billNumber, int districtId, int branchId, String godownId, decimal godownCapacity, decimal avlQty, decimal vacantCapacity,
        string financialYear, string month, decimal netAmount, decimal TDS_Amount, decimal Other_Deduction, decimal Payable_Amount, string approvalStatus, string remark)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            //using (SqlCommand cmd = new SqlCommand("usp_Insert_BOT_Bill_Approved_By_RM", con))
            using (SqlCommand cmd = new SqlCommand("usp_Insert_BOT_Bill_Approved_By_Account_Manager", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Input Parameters
                cmd.Parameters.AddWithValue("@Bill_Number", billNumber);
                cmd.Parameters.AddWithValue("@District_Id", districtId);
                cmd.Parameters.AddWithValue("@Branch_Id", branchId);
                cmd.Parameters.AddWithValue("@Godown_Id", godownId);
                cmd.Parameters.AddWithValue("@GodownCapacity", godownCapacity);
                cmd.Parameters.AddWithValue("@AvlQty", avlQty);
                cmd.Parameters.AddWithValue("@Vacant_Capacity", vacantCapacity);
                cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Net_Amount", netAmount);
                cmd.Parameters.AddWithValue("@TDS_Amount", TDS_Amount);
                cmd.Parameters.AddWithValue("@Other_Deduction", Other_Deduction);
                cmd.Parameters.AddWithValue("@Payble_Amount", Payable_Amount);
                cmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
                cmd.Parameters.AddWithValue("@Account_Approval_Status", 'Y');
                cmd.Parameters.AddWithValue("@Account_Approval_By", ip);
                cmd.Parameters.AddWithValue("@Remark", remark);
                cmd.Parameters.AddWithValue("@Approved_By", Session["Region_ID"].ToString());
                // Output Parameter
                SqlParameter outMsg = new SqlParameter("@TheResult", SqlDbType.NVarChar, 100);
                outMsg.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outMsg);

                con.Open();
                cmd.ExecuteNonQuery();

                // Output parameter value
                string message = outMsg.Value.ToString();
                fillgrid();
                // अब आप message UI पर दिखा सकते हो
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{message}');", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + message + "')", true);
            }
        }


        //SqlConnection con = new SqlConnection(constr);
        //if (con.State == ConnectionState.Closed)
        //{
        //    con.Open();
        //}
        //SqlCommand cmd1 = new SqlCommand();
        //try
        //{
        //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //    SqlCommand cmd = new SqlCommand("usp_Insert_BOT_Bill_Approved_By_RM", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Bill_Number", billNumber);
        //    cmd.Parameters.AddWithValue("@District_Id", districtId);
        //    cmd.Parameters.AddWithValue("@Branch_Id", branchId);
        //    cmd.Parameters.AddWithValue("@Godown_Id", godownId);
        //    cmd.Parameters.AddWithValue("@GodownCapacity", godownCapacity);
        //    cmd.Parameters.AddWithValue("@AvlQty", avlQty);
        //    cmd.Parameters.AddWithValue("@Vacant_Capacity", vacantCapacity);
        //    cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
        //    cmd.Parameters.AddWithValue("@Month", month);
        //    cmd.Parameters.AddWithValue("@Net_Amount", netAmount);
        //    cmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);
        //    cmd.Parameters.AddWithValue("@Remark", remark);
        //    cmd.Parameters.AddWithValue("@Approved_By", Session["Region_ID"].ToString());
        //    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        //    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        //    cmd.ExecuteNonQuery();
        //    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        //    if (TheResult.StartsWith("SUCCESS"))
        //    {
        //        string strMsg = "Bill Approved Successfully |||";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
        //        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        //        //Depositor_Gridview.EditIndex = -1;
        //        //Call ShowData method for displaying updated data  
        //        //GetBranchData();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Not Approved Updated !');", true);
        //    }
        //    //if (rowsAffected > 0)
        //    //{
        //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully!');", true);
        //    //    fillgrid();
        //    //}
        //    //else
        //    //{
        //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated!');", true);
        //    //}
        //}
        //catch (Exception ex)
        //{
        //    string strMsg2 = ex.Message.ToString();
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        //}
    }
    protected void txtOther_Deduction_TextChanged(object sender, EventArgs e)
    {
        TextBox txtOther = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtOther.NamingContainer;

        // Net Amount
        Label lblNet = (Label)row.FindControl("lblNet_Amount");
        decimal netAmount = string.IsNullOrEmpty(lblNet.Text) ? 0 : Convert.ToDecimal(lblNet.Text);

        // TDS Amount
        Label lblTds = (Label)row.FindControl("lblTDS_Amount");
        decimal tdsAmount = string.IsNullOrEmpty(lblTds.Text) ? 0 : Convert.ToDecimal(lblTds.Text);

        // Other Deduction
        decimal otherDeduction = string.IsNullOrEmpty(txtOther.Text) ? 0 : Convert.ToDecimal(txtOther.Text);

        // Payable Amount
        decimal payable = netAmount - tdsAmount - otherDeduction;

        // Set Payable Amount
        Label lblPayable = (Label)row.FindControl("lblPayable_Amount");
        lblPayable.Text = payable.ToString("0.00");
    }
}