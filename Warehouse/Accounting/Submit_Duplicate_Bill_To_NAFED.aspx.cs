using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounting_Submit_Duplicate_Bill_To_NAFED : System.Web.UI.Page
{
    private string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Grand Totals variables
    private decimal grandNewClosingBal = 0;
    private decimal grandNewBillAmount = 0;
    private decimal grandOldClosingBal = 0;
    private decimal grandOldBillAmount = 0;
    private decimal grandDiffAmount = 0;
    private decimal grandNafedRecAmount = 0;
    private decimal grandRemainingAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDuplicateBillReport();
        }
    }

    private void BindDuplicateBillReport()
    {
        ResetCounters();
        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("Submit_Duplicate_Bill_To_NAFED", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    gvDuplicateBills.DataSource = dt;
                    gvDuplicateBills.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        gvDuplicateBills.UseAccessibleHeader = true;
                        gvDuplicateBills.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }

    //protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Print")
    //    {
    //        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
    //        Session["Bill_Number"] = (row.RowIndex).ToString();
    //        Label Billnumber = (Label)row.FindControl("lblBill_Number");
    //        string url = "Nafed_Print_Duplicate_Bill.aspx.aspx?BN=" + Base64Encode(Billnumber.Text);
    //        string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=100,top=100,resizable=yes');";
    //        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //    }
    //}

    //public static string Base64Encode(string plainText)
    //{
    //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    //    return System.Convert.ToBase64String(plainTextBytes);
    //}

    //protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Print")
    //    {
    //        GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
    //        Session["Bill_Number"] = (row.RowIndex).ToString();

    //        // Extracting Both Bill Numbers (New & Old) from Row Controls
    //        Label lblNewBillNo = (Label)row.FindControl("lblBill_Number"); // New / Duplicate Bill
    //        Label lblOldBillNo = (Label)row.FindControl("lblOld_Bill_Number"); // Old / Original Bill

    //        string newBillStr = lblNewBillNo != null ? lblNewBillNo.Text : "";
    //        string oldBillStr = lblOldBillNo != null ? lblOldBillNo.Text : newBillStr; // Fallback if null

    //        // Base64 Encoding both parameters
    //        string bnEncoded = Base64Encode(newBillStr);
    //        string bn1Encoded = Base64Encode(oldBillStr);

    //        // Constructing Target URL with BN and BN1 parameters
    //        string url = "Nafed_Print_Duplicate_Bill.aspx?BN=" + bnEncoded + "&BN1=" + bn1Encoded;
    //        string s = "window.open('" + url + "', 'popup_window', 'width=980,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", s, true);
    //    }
    //}

    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Session["Bill_Number"] = row.RowIndex.ToString();

            // Row Control se New aur Old Bill numbers nikalein
            Label lblNewBillNo = (Label)row.FindControl("lblBill_Number");
            Label lblOldBillNo = (Label)row.FindControl("lblOld_Bill_Number");

            string newBillStr = (lblNewBillNo != null) ? lblNewBillNo.Text.Trim() : "";
            string oldBillStr = (lblOldBillNo != null) ? lblOldBillNo.Text.Trim() : newBillStr;

            // Base64 Encoding Both Parameters (C# 5 syntax)
            string bnEncoded = Base64Encode(newBillStr);
            string bn1Encoded = Base64Encode(oldBillStr);

            // Target URL Construct karke Client-side popup trigger karna
            string url = "Nafed_Print_Duplicate_Bill.aspx?BN=" + bnEncoded + "&BN1=" + bn1Encoded;
            string script = "window.open('" + url + "', 'popup_window', 'width=980,height=700,left=100,top=100,resizable=yes,scrollbars=yes');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PrintPopup", script, true);
        }
    }

    public static string Base64Encode(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return "";
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    //protected void gvDuplicateBills_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // Extract & Sum Decimal Values SAFELY
    //        grandNewClosingBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "New_Closing_Balance") ?? 0);
    //        grandNewBillAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "New_Bill_Amount") ?? 0);
    //        grandOldClosingBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OLD_Closing_Balance") ?? 0);
    //        grandOldBillAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Old_Bill_Amount") ?? 0);
    //        grandDiffAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Difference_Amount") ?? 0);
    //        grandNafedRecAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Nafed_Received_Amount") ?? 0);
    //        grandRemainingAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Remaining_Amount") ?? 0);

    //        // Columns 11 to 17 are Amount & Balance Columns (Align Right & Formatting for Excel)
    //        for (int i = 11; i <= 17; i++)
    //        {
    //            e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
    //        }

    //        // Text formatting for Bill Numbers (Text format for Excel export)
    //        e.Row.Cells[9].Attributes.Add("style", "mso-number-format:\\@;");  // New Bill
    //        e.Row.Cells[10].Attributes.Add("style", "mso-number-format:\\@;"); // Old Bill
    //    }
    //    else if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        // Clear unnecessary cells
    //        for (int k = 0; k < e.Row.Cells.Count; k++)
    //        {
    //            e.Row.Cells[k].Text = "";
    //        }

    //        // Set "Grand Total" Label
    //        e.Row.Cells[2].Text = "Grand Total";
    //        e.Row.Cells[2].Attributes.Add("style", "text-align:left !important; font-weight:bold !important; padding-left:10px !important;");


    //        e.Row.Cells[11].Text = grandNewClosingBal.ToString("N2");
    //        e.Row.Cells[12].Text = grandNewBillAmount.ToString("N2");
    //        e.Row.Cells[13].Text = grandOldClosingBal.ToString("N2");
    //        e.Row.Cells[14].Text = grandOldBillAmount.ToString("N2");
    //        e.Row.Cells[15].Text = grandDiffAmount.ToString("N2");
    //        e.Row.Cells[16].Text = grandNafedRecAmount.ToString("N2");
    //        e.Row.Cells[17].Text = grandRemainingAmount.ToString("N2");

    //        // Apply Right Align and Bold Style to Footer Totals
    //        for (int j = 11; j <= 17; j++)
    //        {
    //            e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
    //        }
    //    }
    //}

    protected void gvDuplicateBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Extract & Sum Decimal Values SAFELY
            grandNewClosingBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "New_Closing_Balance") ?? 0);
            grandNewBillAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "New_Bill_Amount") ?? 0);
            grandOldClosingBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OLD_Closing_Balance") ?? 0);
            grandOldBillAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Old_Bill_Amount") ?? 0);
            grandDiffAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Difference_Amount") ?? 0);
            grandNafedRecAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Nafed_Received_Amount") ?? 0);
            grandRemainingAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Remaining_Amount") ?? 0);

            // Columns 11, 12, 13, 14, 15, 16, aur 18 Amount & Balance Columns hain (Align Right & Formatting for Excel)
            int[] amountCols = new int[] { 11, 12, 13, 14, 15, 16, 18 };
            foreach (int i in amountCols)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }

            // Text formatting for Bill Numbers (Text format for Excel export)
            e.Row.Cells[9].Attributes.Add("style", "mso-number-format:\\@;");  // New Bill (lblBill_Number)
            e.Row.Cells[10].Attributes.Add("style", "mso-number-format:\\@;"); // Old Bill (lblOld_Bill_Number)
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Clear unnecessary cells
            for (int k = 0; k < e.Row.Cells.Count; k++)
            {
                e.Row.Cells[k].Text = "";
            }

            // Set "Grand Total" Label
            e.Row.Cells[2].Text = "Grand Total";
            e.Row.Cells[2].Attributes.Add("style", "text-align:left !important; font-weight:bold !important; padding-left:10px !important;");

            // Correct Mapping of Calculated Totals to Column Indexes:
            // Index 11: New Closing Balance
            // Index 12: New Bill Amount
            // Index 13: Old Closing Balance
            // Index 14: Old Bill Amount
            // Index 15: Difference Amount
            // Index 16: NAFED Received Amount
            // Index 17: NAFED Payment Date (Skip/Empty)
            // Index 18: Remaining Amount

            e.Row.Cells[11].Text = grandNewClosingBal.ToString("N2");
            e.Row.Cells[12].Text = grandNewBillAmount.ToString("N2");
            e.Row.Cells[13].Text = grandOldClosingBal.ToString("N2");
            e.Row.Cells[14].Text = grandOldBillAmount.ToString("N2");
            e.Row.Cells[15].Text = grandDiffAmount.ToString("N2");
            e.Row.Cells[16].Text = grandNafedRecAmount.ToString("N2");

            // Index 17 is Date Column, so no total here.
            e.Row.Cells[18].Text = grandRemainingAmount.ToString("N2");

            // Apply Right Align and Bold Style to Footer Totals
            int[] footerCols = new int[] { 11, 12, 13, 14, 15, 16, 18 };
            foreach (int j in footerCols)
            {
                e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0\\.00;");
            }
        }
    }

    private string GetUserIPAddress()
    {
        string userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(userIP))
        {
            userIP = Request.ServerVariables["REMOTE_ADDR"];
        }

        if (string.IsNullOrEmpty(userIP))
        {
            userIP = Request.UserHostAddress;
        }

        return userIP;
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int insertedCount = 0;
        int duplicateCount = 0;
        string userIP = GetUserIPAddress();

        using (SqlConnection con = new SqlConnection(connString))
        {
            con.Open();
            foreach (GridViewRow row in gvDuplicateBills.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelectRow");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        string newBillNo = gvDuplicateBills.DataKeys[row.RowIndex].Value.ToString();

                        using (SqlCommand cmd = new SqlCommand("Insert_Approved_Nafed_Duplicate_Bill", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@New_Bill_No", newBillNo);
                            cmd.Parameters.AddWithValue("@User_IP", userIP);

                            int status = Convert.ToInt32(cmd.ExecuteScalar());
                            if (status == 1)
                            {
                                insertedCount++;
                            }
                            else
                            {
                                duplicateCount++;
                            }
                        }
                    }
                }
            }
        }

        string message = "";
        if (insertedCount > 0)
        {
            message += insertedCount + " bill(s) approved successfully. ";
        }
        if (duplicateCount > 0)
        {
            message += duplicateCount + " bill(s) skipped as they were already approved.";
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + HttpUtility.JavaScriptStringEncode(message) + "');", true);

        BindDuplicateBillReport();
    }

    private void ResetCounters()
    {
        grandNewClosingBal = 0;
        grandNewBillAmount = 0;
        grandOldClosingBal = 0;
        grandOldBillAmount = 0;
        grandDiffAmount = 0;
        grandNafedRecAmount = 0;
        grandRemainingAmount = 0;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvDuplicateBills.Rows.Count == 0) return;

        // Hide Checkbox Column during Excel Export
        gvDuplicateBills.Columns[0].Visible = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Duplicate_Bill_Report_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindDuplicateBillReport();

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");

                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='18' style='font-size:15pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='18' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Duplicate Storage Bill Details Submitted To NAFED</th></tr>
                        <tr><td colspan='9' style='text-align:left; font-weight:bold; color:#475569;'>Report Status: Approved Duplicate Bills</td><td colspan='9' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='18' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                string style = @"<style> 
                    th { background-color: #2563eb !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important;} 
                    td { border:1px solid #cbd5e1 !important; } 
                    .text-right-align { text-align: right !important; }
                    .text-center-align { text-align: center !important; }
                    .footer-style td { background-color: #eff6ff !important; font-weight: bold !important; color: #1e3a8a !important; }
                </style>";

                Response.Write(style);
                Response.Write(customExcelHeader);

                gvDuplicateBills.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();

                // Restore column visibility back to normal
                gvDuplicateBills.Columns[0].Visible = true;

                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for ASP.NET server control during export
    }
}