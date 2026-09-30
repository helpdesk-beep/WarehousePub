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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.IO;

public partial class BranchPages_NCCF_Rent_Bill_BO_Approval : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    public void fillgrid()
    {
        string Branch_Id = Session["BranchId"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_NCCF_Rent_Bill_Details_For_Branch_Approval", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
            }
        }

    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Print")
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    Session["Bill_Number"] = (row.RowIndex).ToString();
        //    Label Billnumber = (Label)row.FindControl("lblBill_Number");
        //    string url = "Print_Nafed_BIll_.aspx?BN=" + Base64Encode(Billnumber.Text);
        //    string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}
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
            string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
            string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
            string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
            string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
            //string Month_Year = (row.FindControl("lblMonth") as Label).Text;
            string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
            // string Closing_Balance = (row.FindControl("lblClosing_Balance") as Label).Text;
            string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
            UpdateRow(Godown_Id.ToString(), Bill_Number.ToString(), Commodity_Id.ToString(), Crop_Year.ToString(), Financial_Year.ToString(), Month.ToString(), Convert.ToDecimal(Net_Amount), drpApprovalStatus.SelectedValue.ToString());
        }
    }
    public void UpdateRow(string Godown_Id, string BillNumber, string Commodity_Id, String Crop_Year, string Financial_Year, string Month, Decimal Net_Amount, string ApprovalStatus)
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
            SqlCommand cmd = new SqlCommand("Usp_Insert_NCCF_Rent_Branch_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
            cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
            cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id);
            cmd.Parameters.AddWithValue("@Crop_Year", Crop_Year);
            cmd.Parameters.AddWithValue("@Financial_Year", Financial_Year);
            cmd.Parameters.AddWithValue("@Month", Month);
            //cmd.Parameters.AddWithValue("@Closing_Balance", Closing_Balance);
            cmd.Parameters.AddWithValue("@Net_Amount", Net_Amount);
            cmd.Parameters.AddWithValue("@BM_Approval_Status", ApprovalStatus);
            cmd.Parameters.AddWithValue("@Created_By", Session["BranchId"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_IP", ip);
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
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdBills.Rows)
        {
            string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue.ToString();
            if (drpApprovalStatus.ToString() == "N")
            {
                TextBox Remark = (TextBox)row.FindControl("txtRemark");
                Remark.Visible = true;
            }
            else if (drpApprovalStatus.ToString() == "Y")
            {
                TextBox Remark = (TextBox)row.FindControl("txtRemark");
                Remark.Visible = false;
            }
            else
            {
                TextBox Remark = (TextBox)row.FindControl("txtRemark");
                Remark.Visible = false;
            }
        }
    }
}