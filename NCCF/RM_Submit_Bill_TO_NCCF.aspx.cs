using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NCCF_RM_Submit_Bill_TO_NCCF : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

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
        string qry = "Select Distinct SB.Godown_Id,MG.Godown_Name from tbl_NCCF_Storage_Bill_Details Sb Inner join tbl_MetaData_GODOWN_2018 MG on SB.Godown_Id=MG.Godown_ID Where SB.Branch_Id='" + ddlbranch.SelectedValue + "' Order By Godown_Name ASC";
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
        fillgrid();
    }

    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_RM_Submit_Bill_To_NCCF", con);
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
                ViewState["Bills"] = null;
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
            Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Branch_NCCF_Print_Bill.aspx?BN=" + Base64Encode(Billnumber.Text);
            string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        if (e.CommandName == "EditRow")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
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

    public void UpdateRow(String District_Id, String Branch_Id, string Godown_Id, string BillNumber, string Commodity_Id, String Crop_Year, string Financial_Year, string Month, Decimal Net_Amount, Decimal PSS, Decimal PSF, string approvalStatus, string Remark)
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
                cmd.Parameters.AddWithValue("@Marketing_Approve_Stutes", approvalStatus);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@Created_By", Session["UserID"] != null ? Session["UserID"].ToString() : "System");
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GrdBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkRow");
            if (chk != null && chk.Checked)
            {
                try
                {
                    string District_Id = (row.FindControl("hdnDistrict_Id") as HiddenField).Value;
                    string Branch_Id = (row.FindControl("hdnBranch_Id") as HiddenField).Value;
                    string Godown_Id = (row.FindControl("hdnGodown_Id") as HiddenField).Value;
                    string Bill_Number = (row.FindControl("lblBill_Number") as Label).Text;
                    string Commodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
                    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
                    string Financial_Year = (row.FindControl("lblFinancial_Year") as Label).Text;
                    string Month = (row.FindControl("hdnMonth") as HiddenField).Value;
                    Decimal Net_Amount = Convert.ToDecimal((row.FindControl("lblNet_Amount") as Label).Text);

                    Decimal PSS = 0, PSF = 0;
                    decimal.TryParse((row.FindControl("txtPSS") as TextBox).Text, out PSS);
                    decimal.TryParse((row.FindControl("txtPSF") as TextBox).Text, out PSF);

                    string Remark = (row.FindControl("txtRemark") as TextBox).Text;
                    DropDownList drpApprovalStatus = (DropDownList)row.FindControl("ddlApprovalStatus");

                    UpdateRow(District_Id, Branch_Id, Godown_Id, Bill_Number, Commodity_Id, Crop_Year, Financial_Year, Month, Net_Amount, PSS, PSF, drpApprovalStatus.SelectedValue, Remark);
                }
                catch { }
            }
        }
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
        // Required for GridView Export
    }

    // =========================================================================
    // EXPORT DIRECT FROM GRID (NO PAGING + FORMATTED EXCEL OUTPUT)
    // =========================================================================
    // =========================================================================
    // EXPORT DIRECT FROM GRID (PRECISE BORDER & BACKGROUND ALIGNMENT)
    // =========================================================================
    protected void btnExportSelectedGrid_Click(object sender, EventArgs e)
    {
        DataTable dtBills = ViewState["Bills"] as DataTable;

        if (dtBills == null || dtBills.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", "alert('Screen par koi data available nahi hai!');", true);
            return;
        }

        // Disable Paging to export ALL records
        GrdBills.AllowPaging = false;
        GrdBills.DataSource = dtBills;
        GrdBills.DataBind();

        // Branch / Center Name
        string centerName = "";
        if (ddlbranch.SelectedIndex > 0 && ddlbranch.SelectedValue != "0")
        {
            centerName = ddlbranch.SelectedItem.Text;
        }

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Pending_Bills_For_Processing_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Table structure with exact borders and strictly bounded header rows
                hw.Write("<table border='1' cellspacing='0' cellpadding='4' style='border-collapse:collapse; table-layout:fixed; width:100%; font-family:Calibri, Arial;'>");

                // Top Header: Corporation Name (Exact 11 Columns Bounded)
                hw.Write("<tr>");
                hw.Write("<td colspan='11' align='center' style='font-size:15px; font-weight:bold; background-color:#d9ebff; color:#000000; border:0.5pt solid #8ea9c5; padding:6px;'>Madhya Pradesh Warehousing & Logistics Corporation</td>");
                hw.Write("</tr>");

                // Optional Center Name
                if (!string.IsNullOrEmpty(centerName))
                {
                    hw.Write("<tr>");
                    hw.Write("<td colspan='11' align='center' style='font-size:12px; font-weight:bold; background-color:#ffffff; color:#000000; border:0.5pt solid #8ea9c5; padding:4px;'>Center: " + centerName + "</td>");
                    hw.Write("</tr>");
                }

                // Report Title
                hw.Write("<tr>");
                hw.Write("<td colspan='11' align='center' style='font-size:13px; font-weight:bold; background-color:#ffffff; color:#0066cc; border:0.5pt solid #8ea9c5; padding:4px;'>Pending Bills For Processing</td>");
                hw.Write("</tr>");

                // Timestamp Row
                hw.Write("<tr>");
                hw.Write("<td colspan='11' align='right' style='font-size:10px; font-style:italic; background-color:#ffffff; color:#333333; border:0.5pt solid #8ea9c5; padding:3px;'>Report Generated On: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "</td>");
                hw.Write("</tr>");

                // Empty Buffer Row with Clean Border
                hw.Write("<tr><td colspan='11' style='background-color:#ffffff; border:0.5pt solid #ffffff; height:5px;'></td></tr>");

                // Table Column Headers
                hw.Write("<tr style='background-color:#1f497d; color:#FFFFFF; font-weight:bold; text-align:center;'>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>S.No</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>District Name</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Branch Name</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Godown Name</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Bill Name</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Commodity Name</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Crop Year</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Financial Year</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Month</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Chargable Bags</th>");
                hw.Write("<th style='border:0.5pt solid #000000; background-color:#1f497d; color:#FFFFFF;'>Net Amount</th>");
                hw.Write("</tr>");

                // Totals Tracking
                int serialNo = 1;
                long grandTotalBags = 0;
                decimal grandTotalAmount = 0;

                // Process All Data Rows
                foreach (GridViewRow row in GrdBills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblDist = (Label)row.FindControl("lblDistrict_Name");
                        Label lblBranch = (Label)row.FindControl("lblBranch_Name");
                        Label lblGodown = (Label)row.FindControl("lblGodown_Name");
                        Label lblBill = (Label)row.FindControl("lblBill_Number");
                        Label lblCommodity = (Label)row.FindControl("lblCommodity_Name");
                        Label lblCrop = (Label)row.FindControl("lblCrop_Year");
                        Label lblFinYear = (Label)row.FindControl("lblFinancial_Year");
                        Label lblMonth = (Label)row.FindControl("lblMonth");
                        Label lblBags = (Label)row.FindControl("lblTotal_Chargeble_Bags");
                        Label lblNetAmt = (Label)row.FindControl("lblNet_Amount");

                        long bags = 0;
                        decimal netAmt = 0;

                        if (lblBags != null) long.TryParse(lblBags.Text, out bags);
                        if (lblNetAmt != null) decimal.TryParse(lblNetAmt.Text, out netAmt);

                        grandTotalBags += bags;
                        grandTotalAmount += netAmt;

                        hw.Write("<tr>");
                        hw.Write("<td align='center' style='border:0.5pt solid #cccccc;'>" + serialNo.ToString() + "</td>");
                        hw.Write("<td style='border:0.5pt solid #cccccc;'>" + (lblDist != null ? lblDist.Text : "") + "</td>");
                        hw.Write("<td style='border:0.5pt solid #cccccc;'>" + (lblBranch != null ? lblBranch.Text : "") + "</td>");
                        hw.Write("<td style='border:0.5pt solid #cccccc;'>" + (lblGodown != null ? lblGodown.Text : "") + "</td>");

                        // Bill Number Text Formatting (Prevents Scientific Notation without green error triangle)
                        string billNoStr = lblBill != null ? lblBill.Text : "";
                        hw.Write("<td align='left' style='border:0.5pt solid #cccccc; mso-number-format:\"\\@\";'>" + billNoStr + "</td>");

                        hw.Write("<td style='border:0.5pt solid #cccccc;'>" + (lblCommodity != null ? lblCommodity.Text : "") + "</td>");
                        hw.Write("<td align='center' style='border:0.5pt solid #cccccc;'>" + (lblCrop != null ? lblCrop.Text : "") + "</td>");
                        hw.Write("<td align='center' style='border:0.5pt solid #cccccc;'>" + (lblFinYear != null ? lblFinYear.Text : "") + "</td>");
                        hw.Write("<td style='border:0.5pt solid #cccccc;'>" + (lblMonth != null ? lblMonth.Text : "") + "</td>");
                        hw.Write("<td align='right' style='border:0.5pt solid #cccccc;'>" + bags.ToString("#,##0") + "</td>");
                        hw.Write("<td align='right' style='border:0.5pt solid #cccccc;'>" + netAmt.ToString("0.00") + "</td>");
                        hw.Write("</tr>");

                        serialNo++;
                    }
                }

                // Perfectly Bounded Bottom Total Row
                hw.Write("<tr style='font-weight:bold;'>");
                hw.Write("<td colspan='9' align='right' style='background-color:#ffc107; color:#000000; font-weight:bold; border:0.5pt solid #000000; padding:5px;'>Total:</td>");
                hw.Write("<td align='right' style='background-color:#ffc107; color:#000000; font-weight:bold; border:0.5pt solid #000000; padding:5px;'>" + grandTotalBags.ToString("#,##0") + "</td>");
                hw.Write("<td align='right' style='background-color:#ffc107; color:#000000; font-weight:bold; border:0.5pt solid #000000; padding:5px;'>" + grandTotalAmount.ToString("#,##0.00") + "</td>");
                hw.Write("</tr>");

                hw.Write("</table>");

                // Explicit CSS for cell borders & text formatting
                string style = @"<style> .textmode { mso-number-format:'\@'; } TD { border: 0.5pt solid #cccccc; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();

                // Reset Grid Paging for Web Page display
                GrdBills.AllowPaging = true;
                GrdBills.DataSource = dtBills;
                GrdBills.DataBind();

                Response.End();
            }
        }
    }
}