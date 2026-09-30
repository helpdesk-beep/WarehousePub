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
using System.Collections.Generic;

public partial class NCCF_Bill_Approved_By_Business_Section : System.Web.UI.Page
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
            GetCommodity();
            GetCropYear();
            GetDistrict();
        }
    }

    public void GetCropYear()
    {
        string qry = "Select Distinct Crop_Year from tbl_NCCF_Storage_Bill_Details Order By Crop_Year ASC";
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
    }

    public void GetCommodity()
    {
        string qry = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where Commodity_Id in ('63', '64', '33', '52', '27', '92', '123', '31', '65','26','75') order by Commodity_Name asc";
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
    }

    public void GetDistrict()
    {
        using (SqlCommand cmd = new SqlCommand("Get_NCCF_District_List", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                ddldistrict.DataSource = dt;
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
            }
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public void GetBranch()
    {
        string qry = "Select BranchId,DepotName from tbl_MetaData_DEPOT Where DistrictId='23" + ddldistrict.SelectedValue + "' Order By DepotName ASC";
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
    }

    public void GetGodown()
    {
        string qry = "Select Distinct SB.Godown_Id,MG.Godown_Name from tbl_NCCF_Storage_Bill_Details Sb Inner join tbl_MetaData_GODOWN_2018 MG on SB.Godown_Id=MG.Godown_ID  Where SB.Branch_Id='" + ddlbranch.SelectedValue + "' Order By Godown_Name ASC";
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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlcommodity.SelectedValue == "0" || ddlcommodity.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select Commodity!');", true);
            return;
        }
        if (ddlcropyear.SelectedValue == "0" || ddlcropyear.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select Crop Year!');", true);
            return;
        }
        if (ddldistrict.SelectedValue == "0" || ddldistrict.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select District!');", true);
            return;
        }
        fillgrid();
        fillForExcel();
    }

    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_RM_Submit_Bill_To_NCCF_New_Multiple_Print", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@District_Id", SqlDbType.Int).Value = ddldistrict.SelectedValue == "0" ? 0 : Convert.ToInt32(ddldistrict.SelectedValue);
            cmd.Parameters.Add("@Branch_Id", SqlDbType.Int).Value = ddlbranch.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlbranch.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@Godown_Id", SqlDbType.VarChar, 20) { Value = ddlGodown.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@Financial_Year", SqlDbType.VarChar, 10) { Value = ddlFinancialyear.SelectedValue });
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = ddlmonth.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlmonth.SelectedValue);
            cmd.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = ddlcommodity.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlcommodity.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@Crop_Year", SqlDbType.VarChar, 10) { Value = ddlcropyear.SelectedValue });

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

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

    protected void GrdBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNetAmount = (Label)e.Row.FindControl("lblNet_Amount");
            TextBox txtPSS = (TextBox)e.Row.FindControl("txtPSS");
            TextBox txtPSF = (TextBox)e.Row.FindControl("txtPSF");

            if (lblNetAmount != null && txtPSS != null)
            {
                decimal netAmount = 0;
                decimal.TryParse(lblNetAmount.Text, out netAmount);
                txtPSS.Text = netAmount.ToString("0.00");
            }
            if (txtPSF != null)
            {
                txtPSF.Text = "0";
            }
        }
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
        if (e.CommandName == "EditRow")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = (row.RowIndex).ToString();
            DropDownList drpApprovalStatus = (DropDownList)row.FindControl("ddlApprovalStatus");
            string District_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
            string Branch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
            string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
            string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
            string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
            string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
            string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
            string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
            string PSS = (row.FindControl("txtPSS") as TextBox).Text;
            string PSF = (row.FindControl("txtPSF") as TextBox).Text;
            string Remark = (row.FindControl("txtRemark") as TextBox).Text;

            UpdateRow(District_Id, Branch_Id, Godown_Id, Bill_Number, Commodity_Id, Crop_Year, Financial_Year, Month, Convert.ToDecimal(Net_Amount), Convert.ToDecimal(PSS), Convert.ToDecimal(PSF), drpApprovalStatus.SelectedValue, Remark);
        }
    }

    public void UpdateRow(String District_Id, String Branch_Id, string Godown_Id, string BillNumber, string Commodity_Id, String Crop_Year, string Financial_Year, string Month, Decimal Net_Amount, Decimal PSS, Decimal PSF, string ApprovalStatus, string Remark)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            con.Open();
            try
            {
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                SqlCommand cmd = new SqlCommand("Usp_Insert_NCCF_Marketing_Stutes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", District_Id);
                cmd.Parameters.AddWithValue("@Branch_Id", Branch_Id);
                cmd.Parameters.AddWithValue("@Godown_Id", Godown_Id);
                cmd.Parameters.AddWithValue("@Bill_Number", BillNumber);
                cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id);
                cmd.Parameters.AddWithValue("@Crop_Year", Crop_Year);
                cmd.Parameters.AddWithValue("@Financial_Year", Financial_Year);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Net_Amount", Net_Amount);
                cmd.Parameters.AddWithValue("@PSS", PSS);
                cmd.Parameters.AddWithValue("@PSF", PSF);
                cmd.Parameters.AddWithValue("@Marketing_Approve_Stutes", ApprovalStatus);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@Created_By", Session["UserID"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy_IP", ip);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bill Sent To Account Section!');", true);
                    fillgrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Updated!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
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
        int totalBills = GrdBills.Rows.Count;
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

    protected void ddlbranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetGodown();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Avoid form validation error during runtime HTML injection
    }

    // YEH PURANA BUTTON KA EVENT HAI: Jo pure database call ki clean summary banata hai
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = Session["GridData"] as DataTable;
    //    if (dt == null || dt.Rows.Count == 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No data to export');", true);
    //        return;
    //    }

    //    DataTable dtClean = GetCleanStructure();
    //    string netAmtCol = dt.Columns.Contains("Net_Amount") ? "Net_Amount" : (dt.Columns.Contains("Net_Amt") ? "Net_Amt" : "");
    //    string billNumCol = dt.Columns.Contains("Bill_Number") ? "Bill_Number" : (dt.Columns.Contains("Bill_No") ? "Bill_No" : "");
    //    string monthCol = dt.Columns.Contains("MonthYear") ? "MonthYear" : (dt.Columns.Contains("Month_Year") ? "Month_Year" : "");
    //    string bagsCol = dt.Columns.Contains("Total_Chargeble_Bags") ? "Total_Chargeble_Bags" : (dt.Columns.Contains("Chargeble_Bags") ? "Chargeble_Bags" : "");

    //    int serialNo = 1;
    //    foreach (DataRow row in dt.Rows)
    //    {
    //        DataRow dr = dtClean.NewRow();
    //        dr["S.No"] = serialNo++;
    //        dr["District Name"] = dt.Columns.Contains("District_Name") && row["District_Name"] != DBNull.Value ? row["District_Name"].ToString() : "";
    //        dr["Branch Name"] = dt.Columns.Contains("Branch_Name") && row["Branch_Name"] != DBNull.Value ? row["Branch_Name"].ToString() : "";
    //        dr["Godown Name"] = dt.Columns.Contains("Godown_Name") && row["Godown_Name"] != DBNull.Value ? row["Godown_Name"].ToString() : "";
    //        dr["Bill Name"] = !string.IsNullOrEmpty(billNumCol) && row[billNumCol] != DBNull.Value ? row[billNumCol].ToString() : "";
    //        dr["Commodity Name"] = dt.Columns.Contains("Commodity_Name") && row["Commodity_Name"] != DBNull.Value ? row["Commodity_Name"].ToString() : "";
    //        dr["Crop Year"] = dt.Columns.Contains("Crop_Year") && row["Crop_Year"] != DBNull.Value ? row["Crop_Year"].ToString() : "";
    //        dr["Financial Year"] = dt.Columns.Contains("Financial_Year") && row["Financial_Year"] != DBNull.Value ? row["Financial_Year"].ToString() : "";
    //        dr["Month"] = !string.IsNullOrEmpty(monthCol) && row[monthCol] != DBNull.Value ? row[monthCol].ToString() : "";
    //        dr["Chargable Bags"] = !string.IsNullOrEmpty(bagsCol) && row[bagsCol] != DBNull.Value ? row[bagsCol].ToString() : "0";

    //        decimal netAmt = 0;
    //        if (!string.IsNullOrEmpty(netAmtCol) && row[netAmtCol] != DBNull.Value && decimal.TryParse(row[netAmtCol].ToString(), out netAmt))
    //            dr["Net Amount"] = netAmt.ToString("0.00");
    //        else
    //            dr["Net Amount"] = "0.00";

    //        dr["Approve"] = dt.Columns.Contains("Marketing_Approve_Stutes") && row["Marketing_Approve_Stutes"] != DBNull.Value ? row["Marketing_Approve_Stutes"].ToString() : "Approve";
    //        dr["PSS"] = dt.Columns.Contains("PSS") && row["PSS"] != DBNull.Value ? row["PSS"].ToString() : dr["Net Amount"];
    //        dr["PSF"] = dt.Columns.Contains("PSF") && row["PSF"] != DBNull.Value ? row["PSF"].ToString() : "0.00";

    //        dtClean.Rows.Add(dr);
    //    }

    //    FlushExcelToBrowser(dtClean, "Complete_Database_Report.xls");
    //}


    protected void btnExport_Click(object sender, EventArgs e)
    {
        // 1. Session ki jagah ViewState se data nikalenge jisme poora dataset hota hai
        DataTable dt = ViewState["Bills"] as DataTable;

        // Agar ViewState khali ho to fallback ke liye Session check karenge
        if (dt == null)
        {
            dt = Session["GridData"] as DataTable;
        }

        if (dt == null || dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('No data to export');", true);
            return;
        }

        DataTable dtClean = GetCleanStructure();
        string netAmtCol = dt.Columns.Contains("Net_Amount") ? "Net_Amount" : (dt.Columns.Contains("Net_Amt") ? "Net_Amt" : "");
        string billNumCol = dt.Columns.Contains("Bill_Number") ? "Bill_Number" : (dt.Columns.Contains("Bill_No") ? "Bill_No" : "");
        string monthCol = dt.Columns.Contains("MonthYear") ? "MonthYear" : (dt.Columns.Contains("Month_Year") ? "Month_Year" : "");
        string bagsCol = dt.Columns.Contains("Total_Chargeble_Bags") ? "Total_Chargeble_Bags" : (dt.Columns.Contains("Chargeble_Bags") ? "Chargeble_Bags" : "");

        int serialNo = 1;
        foreach (DataRow row in dt.Rows)
        {
            DataRow dr = dtClean.NewRow();
            dr["S.No"] = serialNo++;
            dr["District Name"] = dt.Columns.Contains("District_Name") && row["District_Name"] != DBNull.Value ? row["District_Name"].ToString() : "";
            dr["Branch Name"] = dt.Columns.Contains("Branch_Name") && row["Branch_Name"] != DBNull.Value ? row["Branch_Name"].ToString() : "";
            dr["Godown Name"] = dt.Columns.Contains("Godown_Name") && row["Godown_Name"] != DBNull.Value ? row["Godown_Name"].ToString() : "";
            dr["Bill Name"] = !string.IsNullOrEmpty(billNumCol) && row[billNumCol] != DBNull.Value ? row[billNumCol].ToString() : "";
            dr["Commodity Name"] = dt.Columns.Contains("Commodity_Name") && row["Commodity_Name"] != DBNull.Value ? row["Commodity_Name"].ToString() : "";
            dr["Crop Year"] = dt.Columns.Contains("Crop_Year") && row["Crop_Year"] != DBNull.Value ? row["Crop_Year"].ToString() : "";
            dr["Financial Year"] = dt.Columns.Contains("Financial_Year") && row["Financial_Year"] != DBNull.Value ? row["Financial_Year"].ToString() : "";
            dr["Month"] = !string.IsNullOrEmpty(monthCol) && row[monthCol] != DBNull.Value ? row[monthCol].ToString() : "";
            dr["Chargable Bags"] = !string.IsNullOrEmpty(bagsCol) && row[bagsCol] != DBNull.Value ? row[bagsCol].ToString() : "0";

            decimal netAmt = 0;
            if (!string.IsNullOrEmpty(netAmtCol) && row[netAmtCol] != DBNull.Value && decimal.TryParse(row[netAmtCol].ToString(), out netAmt))
                dr["Net Amount"] = netAmt.ToString("0.00");
            else
                dr["Net Amount"] = "0.00";

            dr["Approve"] = dt.Columns.Contains("Marketing_Approve_Stutes") && row["Marketing_Approve_Stutes"] != DBNull.Value ? row["Marketing_Approve_Stutes"].ToString() : "Approve";
            dr["PSS"] = dt.Columns.Contains("PSS") && row["PSS"] != DBNull.Value ? row["PSS"].ToString() : dr["Net Amount"];
            dr["PSF"] = dt.Columns.Contains("PSF") && row["PSF"] != DBNull.Value ? row["PSF"].ToString() : "0.00";

            dtClean.Rows.Add(dr);
        }

        FlushExcelToBrowser(dtClean, "Complete_Database_Report.xls");
    }

    private DataTable GetCleanStructure()
    {
        DataTable dtClean = new DataTable();
        dtClean.Columns.Add("S.No");
        dtClean.Columns.Add("District Name");
        dtClean.Columns.Add("Branch Name");
        dtClean.Columns.Add("Godown Name");
        dtClean.Columns.Add("Bill Name");
        dtClean.Columns.Add("Commodity Name");
        dtClean.Columns.Add("Crop Year");
        dtClean.Columns.Add("Financial Year");
        dtClean.Columns.Add("Month");
        dtClean.Columns.Add("Chargable Bags");
        dtClean.Columns.Add("Net Amount");
        dtClean.Columns.Add("Approve");
        dtClean.Columns.Add("PSS");
        dtClean.Columns.Add("PSF");
        return dtClean;
    }

    private void FlushExcelToBrowser(DataTable dt, string fileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                GridView gvExport = new GridView();
                gvExport.DataSource = dt;
                gvExport.DataBind();

                gvExport.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(142, 169, 197);
                gvExport.HeaderStyle.ForeColor = System.Drawing.Color.White;
                gvExport.HeaderStyle.Font.Bold = true;

                gvExport.RenderControl(hw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }

    public void fillForExcel()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_RM_Submit_Bill_To_NCCFFor_Excel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@District_Id", SqlDbType.Int).Value = ddldistrict.SelectedValue == "0" ? 0 : Convert.ToInt32(ddldistrict.SelectedValue);
            cmd.Parameters.Add("@Branch_Id", SqlDbType.Int).Value = ddlbranch.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlbranch.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@Godown_Id", SqlDbType.VarChar, 20) { Value = ddlGodown.SelectedValue });
            cmd.Parameters.Add(new SqlParameter("@Financial_Year", SqlDbType.VarChar, 10) { Value = ddlFinancialyear.SelectedValue });
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = ddlmonth.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlmonth.SelectedValue);
            cmd.Parameters.Add("@Commodity_Id", SqlDbType.Int).Value = ddlcommodity.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlcommodity.SelectedValue);
            cmd.Parameters.Add(new SqlParameter("@Crop_Year", SqlDbType.VarChar, 10) { Value = ddlcropyear.SelectedValue });

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt != null && dt.Rows.Count > 0)
            {
                Session["GridData"] = dt;
            }
            else
            {
                Session["GridData"] = null;
            }
        }
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)GrdBills.HeaderRow.FindControl("chkSelectAll");
        int selectedCount = 0;
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null)
            {
                if (chkAll.Checked && selectedCount < 100)
                {
                    chk.Checked = true;
                    selectedCount++;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }
        lblSelectionMessage.Text = (chkAll.Checked && selectedCount >= 100) ? "Maximum 100 bills selected." : "";
    }

    protected void btnPrintSelected_Click(object sender, EventArgs e)
    {
        List<string> selectedBills = new List<string>();
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            if (chk != null && chk.Checked)
            {
                Label Billnumber = (Label)row.FindControl("lblBill_Number");
                if (Billnumber != null && !string.IsNullOrEmpty(Billnumber.Text))
                {
                    selectedBills.Add(Billnumber.Text.Trim());
                    if (selectedBills.Count >= 100) break;
                }
            }
        }

        if (selectedBills.Count > 0)
        {
            Session["SelectedNCCFBills"] = selectedBills;
            string url = ResolveUrl("~/NCCF/Branch_NCCF_Print_Bill_Multiple_Bill.aspx?mode=multiple&t=" + DateTime.Now.Ticks);
            string script = @"<script type='text/javascript'>
                var printWindow = window.open('" + url + @"', 'PrintWindow', 'width=1000,height=700,left=50,top=50,scrollbars=yes,resizable=yes');
                if (!printWindow) { alert('Popup blocked! Please allow popups for this site.'); }
            </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "PrintMultiple", script);
            fillgrid();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select at least one bill to print.');", true);
        }
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}