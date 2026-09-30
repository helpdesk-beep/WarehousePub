using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Get_Duplicate_Bill_For_Submit_To_Nafed : System.Web.UI.Page
{
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    decimal totalNetAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string branchId = Session["BranchId"] != null ? Session["BranchId"].ToString() : "0";
            BindGrid(branchId);
        }
    }

    private void BindGrid(string branchId)
    {
        totalNetAmount = 0;

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Duplicate_Bill_For_Submit_To_Nafed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", branchId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvDuplicateBills.DataSource = dt;
                    gvDuplicateBills.DataBind();
                }
            }
        }
    }

    protected void gvDuplicateBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalNetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Net_Amount"));
        }
    }

    protected void gvDuplicateBills_DataBound(object sender, EventArgs e)
    {
        if (gvDuplicateBills.Rows.Count > 0)
        {
            Table tbl = (Table)gvDuplicateBills.Controls[0];

            GridViewRow grandTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            grandTotalRow.CssClass = "grandtotal-row";

            TableCell mainCell = new TableCell { Text = "GRAND TOTAL NET AMOUNT :", ColumnSpan = 9 };
            mainCell.HorizontalAlign = HorizontalAlign.Right;
            mainCell.Attributes.Add("style", "text-align:right !important; padding-right:15px !important; font-weight:bold !important;");
            grandTotalRow.Cells.Add(mainCell);

            TableCell valueCell = new TableCell { Text = totalNetAmount.ToString("N2"), HorizontalAlign = HorizontalAlign.Right };
            valueCell.Attributes.Add("style", "text-align:right !important; font-weight:bold !important; padding-right:12px !important; white-space:nowrap;");
            grandTotalRow.Cells.Add(valueCell);

            tbl.Rows.Add(grandTotalRow);

            if (gvDuplicateBills.HeaderRow != null)
            {
                gvDuplicateBills.UseAccessibleHeader = true;
                gvDuplicateBills.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }

    // HANDLER TO SEND SELECTED BILLS TO NAFED
    protected void btnSendToNafed_Click(object sender, EventArgs e)
    {
        int selectedCount = 0;
        int successCount = 0;
        string branchId = Session["BranchId"] != null ? Session["BranchId"].ToString() : "0";

        foreach (GridViewRow row in gvDuplicateBills.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                Label lblBillNo = (Label)row.FindControl("lblBillNumber");

                if (chk != null && chk.Checked && lblBillNo != null)
                {
                    selectedCount++;
                    string billNumber = lblBillNo.Text.Trim();

                    // Execute process or stored procedure to mark or submit the bill to NAFED
                    if (UpdateBillSubmissionToNafed(billNumber, branchId))
                    {
                        successCount++;
                    }
                }
            }
        }

        if (selectedCount == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select at least one bill to send to NAFED!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", string.Format("alert('{0} out of {1} selected bill(s) submitted to NAFED successfully.');", successCount, selectedCount), true);
            BindGrid(branchId); // Refresh Grid after action
        }
    }

    private bool UpdateBillSubmissionToNafed(string billNo, string branchId)
    {
        try
        {
            // Client User IP Address extract karne ka tareeka
            string userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userIP))
            {
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userIP))
            {
                userIP = Request.UserHostAddress;
            }

            using (SqlConnection con = new SqlConnection(strConn))
            {
                // Direct Update Query executing on target columns
                string query = @"UPDATE tbl_Storage_Bill_Details_Nafed_Duplicate_Bill 
                            SET BO_Approval_Status = 'Y', 
                                BO_Approval_Date = GETDATE(), 
                                BO_Approval_IP = @IPAddress 
                            WHERE Bill_Number = @BillNo 
                              AND Branch_Id = @BranchID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BillNo", billNo);
                    cmd.Parameters.AddWithValue("@BranchID", branchId);
                    cmd.Parameters.AddWithValue("@IPAddress", userIP ?? "127.0.0.1");

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            // Exception hander
            return false;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string branchId = Session["BranchId"] != null ? Session["BranchId"].ToString() : "0";
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Duplicate_Bill_For_Submit_To_Nafed", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", branchId);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) { sda.Fill(dt); }
            }
        }

        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=NAFED_Duplicate_Bills_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<style>");
                sb.Append("th { background-color:#4e73df !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #dee2e6; text-transform:uppercase; font-size:11px; }");
                sb.Append("td { border:1px solid #dee2e6; font-family: 'Segoe UI', Arial; font-size:11px; } .txt-r { text-align:right !important; mso-number-format:\\#\\,\\#\\#0\\.00; } .txt-c { text-align:center !important; }");
                sb.Append(".grandtotal-row td { background-color: #eff6ff !important; font-weight:bold !important; color:#1e3a8a !important; border-top:2px solid #4e73df; border-bottom:2px double #224abe; }");
                sb.Append("</style>");

                sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
                sb.Append("<tr><th colspan='9' style='font-size:16pt; background-color:#4e73df; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
                sb.Append("<tr><th colspan='9' style='font-size:12pt; background-color:#f8f9fc; color:#4e73df;'>Duplicate Digitally Signed Bills For NAFED Submission</th></tr>");
                sb.AppendFormat("<tr><td colspan='9' style='text-align:right; font-weight:bold;'>Branch ID: {0} | Extracted On: {1}</td></tr>", branchId, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
                sb.Append("<tr><td colspan='9' style='border:none;'>&nbsp;</td></tr>");
                sb.Append("<tr><th>S.No.</th><th>District Name</th><th>Branch Name</th><th>Godown Name</th><th>Financial Year</th><th>Crop Year</th><th>Bill Number</th><th>Month</th><th>Net Amount (₹)</th></tr>");

                int serialNo = 1;
                decimal grandNet = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow r = dt.Rows[i];
                    decimal net = Convert.ToDecimal(r["Net_Amount"]);
                    grandNet += net;

                    sb.AppendFormat("<tr><td class='txt-c'>{0}</td><td>{1}</td><td>{2}</td><td style='font-weight:bold;'>{3}</td><td class='txt-c'>{4}</td><td class='txt-c'>{5}</td><td class='txt-c' style='font-weight:bold; color:#4e73df;'>{6}</td><td class='txt-c'>{7}</td><td class='txt-r' style='font-weight:bold;'>{8:F2}</td></tr>",
                        serialNo++, r["District_Name"], r["Branch_Name"], r["Godown_Name"], r["Financial_Year"], r["Crop_Year"], r["Bill_Number"], r["Month"], net);
                }

                sb.AppendFormat("<tr class='grandtotal-row'><td colspan='8' style='text-align:right; font-weight:bold;'>GRAND TOTAL NET AMOUNT :</td><td class='txt-r'>{0:F2}</td></tr></table>", grandNet);

                Response.Write(sb.ToString());
                Response.Flush();
                Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}