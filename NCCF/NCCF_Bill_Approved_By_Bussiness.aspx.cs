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
using System.IO;
using iTextSharp.text.pdf;
public partial class NCCF_NCCF_Bill_Approved_By_Bussiness : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataTable dt = new DataTable();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDistrict();
            GetCommodity();
            GetCropYear();
        }
    }
    public void GetCropYear()
    {
        string qry = "";
        qry = "Select Distinct Crop_Year from tbl_NCCF_Storage_Bill_Details Order By Crop_Year ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcropyear.DataSource = ds.Tables[0];
            ddlcropyear.DataTextField = "Crop_Year";
            ddlcropyear.DataValueField = "Crop_Year";
            ddlcropyear.DataBind();
            ddlcropyear.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26') order by Commodity_Name asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT  Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranch();
    //}
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "0")
        {
            // 🔴 District = All
            ddlbranch.Items.Clear();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));


            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            // 🟢 Load Branch
            ddlbranch.Enabled = true;
            GetBranch(ddldistrict.SelectedValue);

            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
            //ddlGodown.Enabled = false;
        }
    }
    public void GetBranch(string districtId)
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + districtId + "'Order By DepotName ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetGodown();
    //}
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedValue == "0")
        {
            ddlGodown.Items.Clear();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlGodown.Enabled = true;
            GetGodown(ddlbranch.SelectedValue);
        }
    }
    public void GetGodown(string branchId)
    {
        string qry = "";
        qry = "select distinct Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID='" + branchId + "'Order By Godown_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        string CommodityID = ddlcommodity.SelectedValue;
        string CropYear = ddlcropyear.SelectedValue;
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        string User_Type = Session["UserID"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_NCCF_Bill_Approved_By_Marketing", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@District_Id", SqlDbType.Int).Value = ddldistrict.SelectedValue == "0" ? 0 : Convert.ToInt32(ddldistrict.SelectedValue);
            cmd.Parameters.Add("@Branch_Id", SqlDbType.Int).Value = ddlbranch.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlbranch.SelectedValue);
            SqlParameter pGodown = new SqlParameter("@Godown_Id", SqlDbType.VarChar, 20); pGodown.Value = ddlGodown.SelectedValue; cmd.Parameters.Add(pGodown);
            SqlParameter pFy = new SqlParameter("@Financial_Year", SqlDbType.VarChar, 10); pFy.Value = ddlFinancialyear.SelectedValue; cmd.Parameters.Add(pFy);
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = ddlmonth.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlmonth.SelectedValue);
            cmd.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = ddlcommodity.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlcommodity.SelectedValue);
            SqlParameter pCrop = new SqlParameter("@Crop_Year", SqlDbType.VarChar, 10);
            pCrop.Value = ddlcropyear.SelectedValue;
            cmd.Parameters.Add(pCrop);
            cmd.Parameters.Add("@Created_By", SqlDbType.Int).Value = User_Type;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt != null && dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                ViewState["Bills"] = dt;
                grdbill.Visible = true;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
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
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Branch_NCCF_Print_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else if (e.CommandName == "EditRow")
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
            string Remark = (row.FindControl("txtRemark") as TextBox).Text;

            UpdateRow(Bill_Number.ToString(), drpApprovalStatus.SelectedValue.ToString(), Remark.ToString());
        }
    }
    public void UpdateRow(string BillNumber, string ApprovalStatus, string Remark)
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
            SqlCommand cmd = new SqlCommand("Usp_NCCF_Bill_Approved_By_Account_Manager", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
            cmd.Parameters.AddWithValue("@Account_Approve_Stutes", ApprovalStatus);
            cmd.Parameters.AddWithValue("@Ac_Remark", Remark);
            cmd.Parameters.AddWithValue("@Created_By_Ac", Session["UserID"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_IP_AC", ip);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Checked and process to payment!');", true);
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int successCount = 0;
        int failCount = 0;

        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkRow");

            if (chk != null && chk.Checked)
            {
                try
                {
                    string billNumber = ((Label)row.FindControl("lblBill_Number")).Text;
                    DropDownList drpApprovalStatus = (DropDownList)row.FindControl("ddlApprovalStatus");
                    // Pass Amount textbox se remark/status bhi le sakte ho
                    TextBox txtPass = (TextBox)row.FindControl("txtpass");

                    string approvalStatus = drpApprovalStatus.SelectedValue; // or your logic
                    string remark = txtPass != null ? txtPass.Text.Trim() : "";

                    UpdateRow(billNumber, approvalStatus, remark);

                    successCount++;
                }
                catch
                {
                    failCount++;
                }
            }
        }

        ScriptManager.RegisterClientScriptBlock(
            this,
            this.GetType(),
            "msg",
            "alert('Bill Checked and process to payment!' | 'Updated: " + successCount + " | Failed: " + failCount + "');",
            true
        );

        fillgrid();
    }
    protected void GrdBills_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = ViewState["Bills"] as DataTable;

        if (dt != null)
        {
            DataView dv = dt.DefaultView;

            string sortDirection = "ASC";

            if (ViewState["SortDirection"] != null && ViewState["SortExpression"] != null)
            {
                if (ViewState["SortExpression"].ToString() == e.SortExpression)
                {
                    sortDirection = (ViewState["SortDirection"].ToString() == "ASC") ? "DESC" : "ASC";
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = e.SortExpression;

            dv.Sort = e.SortExpression + " " + sortDirection;

            GrdBills.DataSource = dv;
            GrdBills.DataBind();
        }
    }
    protected void GrdBills_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdBills.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState["Bills"] as DataTable;

        if (dt != null)
        {
            GrdBills.DataSource = dt;
            GrdBills.DataBind();
        }
    }
    protected void GrdBills_DataBound(object sender, EventArgs e)
    {
        int totalBills = GrdBills.Rows.Count; // current page rows
        decimal totalAmount = 0;

        foreach (GridViewRow row in GrdBills.Rows)
        {
            Label lblAmount = (Label)row.FindControl("lblNet_Amount");
            if (lblAmount != null)
            {
                decimal amt = 0;
                decimal.TryParse(lblAmount.Text, out amt);
                totalAmount += amt;
            }
        }

        litTotalBills.Text = totalBills.ToString();
        litTotalAmount.Text = totalAmount.ToString("N2");
    }
}