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

public partial class Accounting_NAFED_NAFED_StorageBill_Approval_State_CheckBox : System.Web.UI.Page
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
            GetRegion();
            GetCommodity();
            GetCropYear();
        }
    }
    public void GetRegion()
    {
        string qry = "";
        qry = "select distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT Order By Regionnm ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT where Region_ID='" + ddlRegion.SelectedValue + "' Order By District_Name ASC";
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
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDistrict();
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
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'Order By DepotName ASC";
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
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    public void GetGodown()
    {
        string qry = "";
        qry = "select distinct Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID='" + ddlbranch.SelectedValue + "'Order By Godown_Name ASC";
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
        fillExporttoexcel();
    }
    public void fillgrid()
    {
        string Region_Id = ddlRegion.SelectedValue;
        string CommodityID = ddlcommodity.SelectedValue;
        string CropYear = ddlcropyear.SelectedValue;
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Marketing", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //if (ddlcommodity.SelectedValue == "0")
            //{
            //    cmd.Parameters.AddWithValue("@Commodity_Id", 0);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Commodity_Id", CommodityID);
            //}
            //if (ddlcropyear.SelectedValue == "0")
            //{
            //    cmd.Parameters.AddWithValue("@Crop_Year", 0);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Crop_Year", CropYear);
            //}
            if (ddlRegion.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Region_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Region_Id", Region_Id);
            }

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
            if (ddlGodown.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Godown_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue);
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
            if (ddlcommodity.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            if (ddlcropyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Crop_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
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
    public void fillExporttoexcel()
    {
        string Region_Id = ddlRegion.SelectedValue;
        string Dist_id = ddldistrict.SelectedValue;
        string Branch_Id = ddlbranch.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlRegion.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Region_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Region_Id", Region_Id);
            }

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
            if (ddlGodown.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Godown_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Godown_Id", ddlGodown.SelectedValue);
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
            if (ddlcommodity.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
            if (ddlcropyear.SelectedValue == "0")
            {
                cmd.Parameters.AddWithValue("@Crop_Year", 0);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                grdexpo.DataSource = dt;
                grdexpo.DataBind();
                grdexpo.Visible = true;
                lblAmount.Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                Session["Amount"] = lblAmount.Text;
                txtCount.Text = GrdBills.Rows.Count.ToString();
                //txtCount.Enabled = false;
                //lblAmount.Enabled = false;
            }
            else
            {
                grdexpo.DataSource = null;
                grdexpo.DataBind();
                //grdexpo.Visible = true;                
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(grdexpo);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void ExportGridViewToExcel(GridView grdexpo)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "NAFED_StorageBill_Approval_State.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        grdexpo.GridLines = GridLines.Both;
        grdexpo.HeaderStyle.Font.Bold = true;
        grdexpo.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
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
        //else if (e.CommandName == "EditRow")
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
        //    string Region_Name = (row.FindControl("lblRegion_Name") as Label).Text;
        //    string District_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
        //    string Branch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
        //    string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
        //    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
        //    string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
        //    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
        //    string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
        //    //string Month_Year = (row.FindControl("lblMonth") as Label).Text;
        //    string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
        //    string Closing_Balance = (row.FindControl("lblClosing_Balance") as Label).Text;
        //    string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
        //    string Remark = (row.FindControl("txtRemark") as TextBox).Text;
        //    UpdateRow(Region_Name.ToString(), District_Id.ToString(), Branch_Id.ToString(), Godown_Id.ToString(), Bill_Number.ToString(), Commodity_Id.ToString(), Crop_Year.ToString(), Financial_Year.ToString(), Month.ToString(), Convert.ToDecimal(Closing_Balance), Convert.ToDecimal(Net_Amount), drpApprovalStatus.SelectedValue.ToString(), Remark.ToString());
        //}
        //else if (e.CommandName == "Check")
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
        //    string Region_Name = (row.FindControl("lblRegion_Name") as Label).Text;
        //    string District_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
        //    string Branch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
        //    string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
        //    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
        //    string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
        //    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
        //    string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
        //    //string Month_Year = (row.FindControl("lblMonth") as Label).Text;
        //    string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
        //    string Closing_Balance = (row.FindControl("lblClosing_Balance") as Label).Text;
        //    string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
        //    string Remark = (row.FindControl("txtRemark") as TextBox).Text;
        //    Session["Region_Name"] = Region_Name.ToString();
        //    Session["District_Id"] = District_Id.ToString();
        //    Session["Branch_Id"] = Branch_Id.ToString();
        //    Session["Godown_Id"] = Godown_Id.ToString();
        //    Session["Bill_Number"] = Bill_Number.ToString();
        //    Session["Commodity_Id"] = Commodity_Id.ToString();
        //    Session["Crop_Year"] = Crop_Year.ToString();
        //    Session["Financial_Year"] = Financial_Year.ToString();
        //    Session["Month"] = Month.ToString();
        //    Session["Closing_Balance"] = Convert.ToDecimal(Closing_Balance);
        //    Session["Net_Amount"] = Convert.ToDecimal(Net_Amount);
        //    Session["drpApprovalStatus"] = drpApprovalStatus.SelectedValue.ToString();
        //    Session["Remark"] = Remark.ToString();
        //}
    }
    protected void Checked_CheckedChanged(object sender, EventArgs e)
    {
        int count = 0;
        decimal total = 0;
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
            }
        }
        lblAmount.Text = total.ToString();
        lblAmount.Visible = true;
        btnproceed.Visible = true;
    }
    public void UpdateRow(string Region_Name, String District_Id, String Branch_Id, string Godown_Id, string BillNumber, string Commodity_Id, String Crop_Year, string Financial_Year, string Month, Decimal Closing_Balance, Decimal Net_Amount, string ApprovalStatus, string Remark)
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
            SqlCommand cmd = new SqlCommand("Usp_Insert_Nafed_MArketing_Stutes", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_Name", Region_Name);
            cmd.Parameters.AddWithValue("@District_Id", District_Id);
            cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
            cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
            cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
            cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id);
            cmd.Parameters.AddWithValue("@Crop_Year", Crop_Year);
            cmd.Parameters.AddWithValue("@Financial_Year", Financial_Year);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Closing_Balance", Closing_Balance);
            cmd.Parameters.AddWithValue("@Net_Amount", Net_Amount);
            cmd.Parameters.AddWithValue("@Marketing_Approve_Stutes", ApprovalStatus);
            cmd.Parameters.AddWithValue("@Remark", Remark);
            cmd.Parameters.AddWithValue("@Created_By", ip);
            cmd.Parameters.AddWithValue("@CreatedBy_IP", Session["State_Logid"].ToString());
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
    //protected void ddlApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow row in GrdBills.Rows)
    //    {
    //        string drpApprovalStatus = (row.FindControl("ddlApprovalStatus") as DropDownList).SelectedValue.ToString();
    //        if (drpApprovalStatus.ToString() == "N")
    //        {
    //            TextBox Remark = (TextBox)row.FindControl("txtRemark");
    //            Remark.Visible = true;
    //        }
    //        else if (drpApprovalStatus.ToString() == "Y")
    //        {
    //            TextBox Remark = (TextBox)row.FindControl("txtRemark");
    //            Remark.Visible = false;
    //        }
    //        else
    //        {
    //            TextBox Remark = (TextBox)row.FindControl("txtRemark");
    //            Remark.Visible = false;
    //        }
    //    }
    //}
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
                    string Region_Name = (row.FindControl("lblRegion_Name") as Label).Text;
                    string District_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
                    string Branch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
                    string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
                    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
                    string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
                    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
                    string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
                    string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
                    string Closing_Balance = (row.FindControl("lblClosing_Balance") as Label).Text;
                    string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
                    string Remark = (row.FindControl("txtRemark") as TextBox).Text;
                    Region_Name = Region_Name.ToString();
                    District_Id = District_Id.ToString();
                    Branch_Id = Branch_Id.ToString();
                    Godown_Id = Godown_Id.ToString();
                    Bill_Number = Bill_Number.ToString();
                    Commodity_Id = Commodity_Id.ToString();
                    Crop_Year = Crop_Year.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    SqlCommand cmd3 = new SqlCommand("Usp_Insert_Nafed_MArketing_Stutes", con2);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@Region_Name", Region_Name.ToString());
                    cmd3.Parameters.AddWithValue("@District_Id", District_Id.ToString());
                    cmd3.Parameters.AddWithValue("@Branch_Id", Branch_Id.ToString());
                    cmd3.Parameters.AddWithValue("@Godown_Id", Godown_Id.ToString());
                    cmd3.Parameters.AddWithValue("@Bill_Number", Bill_Number.ToString());
                    cmd3.Parameters.AddWithValue("@Commodity_Id", Commodity_Id.ToString());
                    cmd3.Parameters.AddWithValue("@Crop_Year", Crop_Year.ToString());
                    cmd3.Parameters.AddWithValue("@Financial_Year", Financial_Year.ToString());
                    cmd3.Parameters.AddWithValue("@Month", Month.ToString());
                    cmd3.Parameters.AddWithValue("@Closing_Balance", Closing_Balance.ToString());
                    cmd3.Parameters.AddWithValue("@Net_Amount", Net_Amount.ToString());
                    cmd3.Parameters.AddWithValue("@Marketing_Approve_Stutes", drpApprovalStatus.ToString());
                    cmd3.Parameters.AddWithValue("@Remark", Remark.ToString());
                    cmd3.Parameters.AddWithValue("@Created_By", ip);
                    cmd3.Parameters.AddWithValue("@CreatedBy_IP", Session["State_Logid"].ToString());
                    int i = cmd3.ExecuteNonQuery();
                    ICount = ICount + i;
                }
            }
            string strMsg = "Bill Sent To Account Section!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            fillgrid();
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
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
                decimal total = 0;
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
                    }
                }
                lblAmount.Text = total.ToString();
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
}