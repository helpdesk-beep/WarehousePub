using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_ApprovedByNafedAccountBySelectMonth : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCropYear();
            LoadCommodity();
            //LoadFinancialYear();
        }
    }

    private void LoadCropYear()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Crop_Year FROM tbl_Storage_Bill_Details ORDER BY Crop_Year DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ddlcropyear.DataSource = dt;
                ddlcropyear.DataTextField = "Crop_Year";
                ddlcropyear.DataValueField = "Crop_Year";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, new ListItem("-- Select Crop Year --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Error loading crop year: " + ex.Message);
        }
    }

    private void LoadCommodity()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"SELECT DISTINCT Commodity_Id, Commodity_Name 
                               FROM tbl_MetaData_STORAGE_COMMODITY 
                               WHERE Commodity_Id IN ('63', '64', '33', '52', '27', '92', '123', '31', '65', '26') 
                               ORDER BY Commodity_Name ASC";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlcommodity.DataSource = dt;
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            }
        }
        catch (Exception ex) 
        {
            ShowErrorMessage("Error loading commodity: " + ex.Message);
        }
    }

    //private void LoadFinancialYear()
    //{
    //    // Financial years already defined in the dropdown
    //    ddlFinancialyear.SelectedValue = DateTime.Now.Year.ToString();
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridData();
    }

    private void LoadGridData()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_Details_Approved_By_NafedAccountManager_MonthBill", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Get selected values
                string month = ddlmonth.SelectedValue;
                //string financialYear = ddlFinancialyear.SelectedValue;
                string commodityId = ddlcommodity.SelectedValue;
                string cropYear = ddlcropyear.SelectedValue;

                // Set parameters
                cmd.Parameters.AddWithValue("@Month", month == "0" ? DBNull.Value : (object)month);
                cmd.Parameters.AddWithValue("@Commodity_Id", commodityId == "0" ? DBNull.Value : (object)commodityId);
                //cmd.Parameters.AddWithValue("@Financial_Year", financialYear == "0" ? DBNull.Value : (object)financialYear);
                cmd.Parameters.AddWithValue("@Crop_Year", cropYear == "0" ? DBNull.Value : (object)cropYear);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    GrdBills.DataSource = dt;
                    GrdBills.DataBind();
                    grdbill.Visible = true;

                    // Calculate total amount
                    decimal totalAmount = dt.AsEnumerable().Sum(row =>
                        row.Field<decimal?>("Net_Amount") ?? 0);
                    lblAmount.Text = totalAmount.ToString("N2");
                    Session["Amount"] = lblAmount.Text;

                    txtCount.Text = GrdBills.Rows.Count.ToString();
                    txtSearch.Visible = true;
                }
                else
                {
                    GrdBills.DataSource = null;
                    GrdBills.DataBind();
                    grdbill.Visible = true;
                    lblAmount.Text = "0.00";
                    txtCount.Text = "0";
                    ShowInfoMessage("No records found for the selected criteria.");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Error loading data: " + ex.Message);
        }
    }

    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label Billnumber = (Label)row.FindControl("lblBill_Number");

            if (Billnumber != null && !string.IsNullOrEmpty(Billnumber.Text))
            {
                string encodedBillNo = Base64Encode(Billnumber.Text.Trim());
                string url = "State_Nafed_Print_Generate_Bill_AllBillprint.aspx?BN="+encodedBillNo+"";
                string script = "window.open('"+url+"', 'popup_window', 'width=800,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "scriptSingle", script, true);
            }
        }
    }

    protected void GrdBills_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdBills.PageIndex = e.NewPageIndex;
        LoadGridData();
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

    protected void btnPrintSelected_Click(object sender, EventArgs e)
    {
        List<string> selectedBills = new List<string>();

        for (int i = 0; i < GrdBills.Rows.Count; i++)
        {
            GridViewRow row = GrdBills.Rows[i];
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");

            if (chk != null && chk.Checked)
            {
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
            string script = "window.open('"+url+"', 'popup_window', 'width=800,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "scriptMulti", script, true);
        }
        else
        {
            ShowErrorMessage("Please select at least one bill to print.");
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GrdBills, "Nafed_Storage_Bill_Approval.xls");
    }

    private void ExportGridViewToExcel(GridView gridView, string fileName)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename="+fileName+"");

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            gridView.GridLines = GridLines.Both;
            gridView.HeaderStyle.Font.Bold = true;
            gridView.RenderControl(htmlWriter);

            Response.Write(stringWriter.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Error exporting to Excel: " + ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for exporting gridview
    }

    private string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    private void ShowErrorMessage(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "alert('"+message.Replace("'", "\\'")+"');", true);
    }

    private void ShowInfoMessage(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "info",
            "alert('"+message.Replace("'", "\\'")+"');", true);
    }
}