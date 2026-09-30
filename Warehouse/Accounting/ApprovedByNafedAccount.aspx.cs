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
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Accounting_ApprovedByNafedAccount : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        GetDistrict();
    //        GetCommodity();
    //        GetCropYear();
    //    }
    //    if (!string.IsNullOrEmpty(Request.QueryString["date"]))
    //    {
    //        string dateStr = Request.QueryString["date"];
    //        txtDate.Text = dateStr; // Fill textbox with date from querystring
    //        fillgrid();
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Initial data binding
            GetDistrict();
            GetCommodity();
            GetCropYear();

            // Fill grid if query string contains a date
            if (!string.IsNullOrEmpty(Request.QueryString["date"]))
            {
                string dateStr = Request.QueryString["date"];
                txtDate.Text = dateStr; // Fill textbox with date from querystring
                fillgrid(); // Bind grid only once
            }
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
            //ddldistrict.DataSource = ds.Tables[0];
            //ddldistrict.DataTextField = "District_Name";
            //ddldistrict.DataValueField = "District_Id";
            //ddldistrict.DataBind();
            //ddldistrict.Items.Insert(0, new ListItem("All", "0"));
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
            //ddlcropyear.DataSource = ds.Tables[0];
            //ddlcropyear.DataTextField = "Crop_Year";
            //ddlcropyear.DataValueField = "Crop_Year";
            //ddlcropyear.DataBind();
            //ddlcropyear.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetBranch()
    {
        //string qry = "";
        //qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        //SqlCommand cmd = new SqlCommand(qry, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    //ddlbranch.DataSource = ds.Tables[0];
        //    //ddlbranch.DataTextField = "DepotName";
        //    //ddlbranch.DataValueField = "DepotID";
        //    //ddlbranch.DataBind();
        //    //ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        //}
        //else
        //{

        //}
    }
    public void fillDraft()
    {
        //string Dist_id = ddldistrict.SelectedValue;
        //string Branch_Id = ddlbranch.SelectedValue;
        //SqlConnection con_WLC1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_Marketing_IN_MS_EXCEL_FORMAT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
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
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = true;
                //ddlFinancialyear.ClearSelection();
                //ddlmonth.ClearSelection();
                //ddlcommodity.ClearSelection();
                //ddlcropyear.ClearSelection();
                //ddldistrict.ClearSelection();
                //ddlbranch.ClearSelection();
                //lblOfficerList.Text = "0";
            }
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(1000);
    //    fillgrid();
    //    //fillDraft();
    //}
    public void fillgrid()
    {
        //string Dist_id = ddldistrict.SelectedValue;
        //string Branch_Id = ddlbranch.SelectedValue;
        string fy = Request.QueryString["fy"]; // ✅ FY URL से ले लिया
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {

            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_NafedAccountManager_New", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Created_On_AC", getDate_MDY(txtDate.Text));
            cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            cmd.Parameters.AddWithValue("@Financial_Year", fy);
            //cmd.Parameters.AddWithValue("@Account_Approve_Stutes", 'Y');

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
                //ddlFinancialyear.ClearSelection();
                //ddlmonth.ClearSelection();
                //ddlcommodity.ClearSelection();
                //ddlcropyear.ClearSelection();
                //ddldistrict.ClearSelection();
                //ddlbranch.ClearSelection();
                lblAmount.Text = "";
                txtCount.Text = "";
            }
        }
    }


    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
            return converted;
        }
    }



    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }


    //17-09-2025-------------------------------------------------------
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            // Single Print
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label Billnumber = (Label)row.FindControl("lblBill_Number");

            if (Billnumber != null)
            {
                string url = "State_Nafed_Print_Generate_Bill_AllBillprint.aspx?BN=" + Base64Encode(Billnumber.Text.Trim());
                string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "scriptSingle", s, true);
            }
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)GrdBills.HeaderRow.FindControl("chkSelectAll");
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null)
            {
                chk.Checked = chkAll.Checked;
            }
        }
    }

    //protected void btnPrintSelected_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(1000); // simulate delay
    //    List<string> selectedBills = new List<string>();

    //    foreach (GridViewRow row in GrdBills.Rows)
    //    {
    //        CheckBox chk = (CheckBox)row.FindControl("chkSelect");
    //        if (chk != null && chk.Checked)
    //        {
    //            Label Billnumber = (Label)row.FindControl("lblBill_Number");
    //            if (Billnumber != null && !string.IsNullOrEmpty(Billnumber.Text))
    //            {
    //                selectedBills.Add(Billnumber.Text.Trim());
    //            }
    //        }
    //    }

    //    if (selectedBills.Count > 0)
    //    {
    //        // ✅ Comma separated bill numbers
    //        string bills = string.Join(",", selectedBills);

    //        string url = "State_Nafed_Print_Generate_Bill.aspx?BN=" + Base64Encode(bills);
    //        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";
    //        ClientScript.RegisterStartupScript(this.GetType(), "scriptMulti", s, true);
    //    }
    //    else
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select at least one bill.');", true);
    //    }
    //}


    protected void btnPrintSelected_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        List<string> selectedBills = new List<string>();

        for (int i = 0; i < GrdBills.Rows.Count; i++)
        {
            GridViewRow row = GrdBills.Rows[i];
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");

            if (chk != null && chk.Checked)
            {
                // ✅ DataKeys se Bill Number lo (ye zyada efficient hai)
                string billNumber = GrdBills.DataKeys[i].Value.ToString();
                if (!string.IsNullOrEmpty(billNumber))
                {
                    selectedBills.Add(billNumber.Trim());
                }
            }
        }

        if (selectedBills.Count > 0)
        {
            Session["SelectedBillNos"] = selectedBills;
            string url = "State_Nafed_Print_Generate_Bill_AllBillPrint.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "scriptMulti", s, true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select at least one bill.');", true);
        }
    }



    public string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
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
    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {
        int count = 0;
        //decimal total = 0;
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox checkBox = (CheckBox)row.FindControl("Checked");
            Label Bill_Number = (Label)row.FindControl("lblBill_Number");
            if (checkBox != null && checkBox.Checked)
            {
                count++;
            }
            else
            {
                //lblAmount.Text = Session["Amount"].ToString();
            }
        }
        lblAmount.Text = Session["Amount"].ToString();
        lblAmount.Visible = true;
        //btnproceed.Visible = true;
    }
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string Commodity = ddlcommodity.SelectedValue;
        fillgrid();
    }
}