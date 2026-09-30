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

public partial class Accounting_Nafed_Storage_Bill_Approval_By_AccountManager_CheckBox : System.Web.UI.Page
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
            GetDistrict();
            GetCommodity();
            GetCropYear();
        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
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
    public void GetCommodity()
    {
        string qry = "";
        qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26','75') order by Commodity_Name asc";
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
    public void GetCropYear()
    {
        string qry = "";
        qry = "Select Distinct Crop_Year from tbl_Storage_Bill_Details Order By Crop_Year ASC";
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
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void fillDraft()
    {
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        //SqlConnection con_WLC1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_Marketing_IN_MS_EXCEL_FORMAT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            }
            if (ddlFinancialyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                GridView1.FooterRow.Style.Add("text-align", "right");
                GridView1.FooterRow.Cells[12].Text = "Total";
                GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("STotalCharges")).ToString();
                GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("FTotalCharges")).ToString();
                GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ClosingBalance")).ToString();
                GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmount")).ToString();
                //ddlFinancialyear.ClearSelection();
                //ddlmonth.ClearSelection();
                //ddlcommodity.ClearSelection();
                //ddlcropyear.ClearSelection();
                //ddldistrict.ClearSelection();
                //ddlbranch.ClearSelection();
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
                txtSearch.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = true;
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                //ddlcropyear.ClearSelection();
                //ddldistrict.ClearSelection();
                ddlbranch.ClearSelection();
                //lblOfficerList.Text = "0";
            }
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
        fillDraft();
    }
    public void fillgrid()
    {
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_Marketing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddldistrict.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@District_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            }
            if (ddlbranch.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Branch_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            }
            if (ddlFinancialyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Financial_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Financial_Year", ddlFinancialyear.SelectedValue);
            }
            if (ddlmonth.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Month", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                grdbill.Visible = true;
                lblAmount.Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                Session["Amount"] = lblAmount.Text;
                txtCount.Text = GrdBills.Rows.Count.ToString();
                txtCount.Enabled = false;
                lblAmount.Enabled = false;
                txtSearch.Visible = true;
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
                grdbill.Visible = true;
                ddlFinancialyear.ClearSelection();
                ddlmonth.ClearSelection();
                ddlcommodity.ClearSelection();
                ddlcropyear.ClearSelection();
                ddldistrict.ClearSelection();
                ddlbranch.ClearSelection();
                lblAmount.Text = "";
                txtCount.Text = "";
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
            string url = "State_Nafed_Print_Generate_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        //if (e.CommandName == "EditRow")
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    Session["Bill_Number"] = (row.RowIndex).ToString();
        //    Label RowNumber = (Label)row.FindControl("lblRowNumber");
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        //    string qry = "";
        //    int ICount = 0;
        //    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //    con.Open();
        //    DropDownList drpApprovalStatus = (DropDownList)row.FindControl("ddlApprovalStatus");
        //    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
        //    string Remark = (row.FindControl("txtRemark") as TextBox).Text;

        //UpdateRow( Bill_Number.ToString(), drpApprovalStatus.SelectedValue.ToString(), Remark.ToString());
    }

    //public void UpdateRow(string BillNumber, string ApprovalStatus, string Remark)
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    SqlConnection con = new SqlConnection(constr);
    //    if (con.State == ConnectionState.Closed)
    //    {
    //        con.Open();
    //    }
    //    SqlCommand cmd1 = new SqlCommand();
    //    try
    //    {
    //        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //        SqlCommand cmd = new SqlCommand("Usp_Account_Manager_Approval_Stutes", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
    //        cmd.Parameters.AddWithValue("@Account_Approve_Stutes", ApprovalStatus);
    //        cmd.Parameters.AddWithValue("@Ac_Remark", Remark);
    //        cmd.Parameters.AddWithValue("@Created_By_Ac", ip);
    //        cmd.Parameters.AddWithValue("@CreatedBy_IP_AC", Session["State_Logid"].ToString());
    //        int rowsAffected = cmd.ExecuteNonQuery();
    //        if (rowsAffected > 0)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully!');", true);
    //            fillgrid();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated!');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strMsg2 = ex.Message.ToString();
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //    }
    //}
    protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdBills.Rows)
        {
            string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue;
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //fillgrid();         
    }
    private void ExportGridViewToExcel(GridView GridView1)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Nafed_Storage_Bill_Approval_By_AccountManager.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    // Do nothing
    //}
    //protected void Checked_CheckedChanged(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    //decimal total = 0;
    //    foreach (GridViewRow row in GrdBills.Rows)
    //    {
    //        CheckBox checkBox = (CheckBox)row.FindControl("Checked");
    //        Label Bill_Number = (Label)row.FindControl("lblBill_Number");
    //        if (checkBox != null && checkBox.Checked)
    //        {
    //            count++;
    //        }
    //        else
    //        {
    //            //lblAmount.Text = Session["Amount"].ToString();
    //        }
    //    }
    //    lblAmount.Text = Session["Amount"].ToString();
    //    lblAmount.Visible = true;
    //    btnproceed.Visible = true;
    //}
    //====================================================Single Check=======================================================
    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {
        bool anyChecked = false;

        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("Checked");
            if (chk != null && chk.Checked)
            {
                anyChecked = true;
                break; // ek bhi mil gaya to kaafi hai
            }
        }

        btnproceed.Visible = anyChecked;
        lblAmount.Visible = anyChecked;

        if (anyChecked)
            lblAmount.Text = Session["Amount"].ToString();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkHeaderCheck = (CheckBox)sender;
        foreach (GridViewRow gRow in GrdBills.Rows)
        {
            CheckBox ckRowSel = (CheckBox)gRow.FindControl("Checked");
            //TextBox PassAmount = (TextBox)gRow.FindControl("txtpass");
            ckRowSel.Checked = chkHeaderCheck.Checked;
            if (ckRowSel != null && ckRowSel.Checked)
            {
                int count = 0;
                //decimal total = 0;
                foreach (GridViewRow row in GrdBills.Rows)
                {
                    CheckBox checkBox = (CheckBox)row.FindControl("Checked");
                    Label Bill_Number = (Label)row.FindControl("lblBill_Number");
                    //string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
                    if (checkBox != null && checkBox.Checked)
                    {
                        count++;
                        // lblAmount.Text = Net_Amount;
                    }
                    else
                    {
                    }
                }
                lblAmount.Text = Session["Amount"].ToString();
                lblAmount.Visible = true;
                btnproceed.Visible = true;
            }
            else
            {
                lblAmount.Visible = false;
                btnproceed.Visible = false;
            }
        }
    }
    protected void btnproceed_Click(object sender, EventArgs e)
    {
        try
        {
            int ICount = 0;
            string con1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            SqlConnection con2 = new SqlConnection(con1);
            if (con2.State == ConnectionState.Closed)
            {
                con2.Open();
            }
            foreach (GridViewRow row in GrdBills.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("Checked");
                if (chkbox.Checked == true)
                {
                    string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue.ToString();
                    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
                    string Remark = (row.FindControl("txtRemark") as TextBox).Text;

                    //string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue.ToString();
                    //string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
                    //string Remark = (row.FindControl("txtRemark") as TextBox).Text;
                    Bill_Number = Bill_Number.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlCommand cmd = new SqlCommand("Usp_Account_Manager_Approval_Stutes", con2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bill_Number", Bill_Number);
                    cmd.Parameters.AddWithValue("@Account_Approve_Stutes", drpApprovalStatus.ToString());
                    cmd.Parameters.AddWithValue("@Ac_Remark", Remark);
                    cmd.Parameters.AddWithValue("@Created_By_Ac", Session["State_Logid"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedBy_IP_AC", ip);
                    //int rowsAffected = cmd.ExecuteNonQuery();
                    //if (rowsAffected > 0)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully!');", true);
                    //    fillgrid();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated!');", true);
                    //}
                    int i = cmd.ExecuteNonQuery();
                    ICount = ICount + i;
                }
            }
            string strMsg = "Bill Approved Successfully!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            fillgrid();
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}