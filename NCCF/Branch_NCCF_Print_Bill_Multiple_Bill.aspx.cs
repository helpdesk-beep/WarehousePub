using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NCCF_Branch_NCCF_Print_Bill_Multiple_Bill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // CASE 1: Multiple Bills from Session
            if (Request.QueryString["mode"] == "multiple")
            {
                if (Session["SelectedNCCFBills"] != null)
                {
                    List<string> billNumbers = (List<string>)Session["SelectedNCCFBills"];

                    // 🔹 LOG PRINTED BILLS FIRST
                    LogPrintedBills(billNumbers);

                    // 🔹 THEN LOAD BILLS FOR DISPLAY
                    LoadMultipleBills(billNumbers);
                    //ShowSummary(billNumbers);

                    Session.Remove("SelectedNCCFBills"); // Clear after use
                }
                else
                {
                    Response.Write("<script>alert('Session expired! Please select bills again.'); window.close();</script>");
                }
            }
            // CASE 2: Single Bill from QueryString (comma-separated)
            else if (!string.IsNullOrEmpty(Request.QueryString["BN"]))
            {
                string decodedBills = Base64Decode(Request.QueryString["BN"]);
                string[] billArray = decodedBills.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> billNumbers = new List<string>(billArray);

                if (billNumbers.Count > 0)
                {
                    // 🔹 LOG PRINTED BILLS FIRST
                    LogPrintedBills(billNumbers);

                    // 🔹 THEN LOAD BILLS FOR DISPLAY
                    LoadMultipleBills(billNumbers);
                    //ShowSummary(billNumbers);
                }
                else
                {
                    Response.Write("<script>alert('No valid bills found!'); window.close();</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No bills selected!'); window.close();</script>");
            }
        }
    }


    private void LogPrintedBills(List<string> billNumbers)
    {
        try
        {
            string userIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            int userID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : 0;
            string sessionID = Session.SessionID;

            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                foreach (string billNo in billNumbers)
                {
                    if (string.IsNullOrWhiteSpace(billNo)) continue;

                    // Get bill details for logging
                    DataTable dt = GetBillData(billNo.Trim());

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        using (SqlCommand cmd = new SqlCommand("Usp_Insert_NCCF_Printed_Bill_Log", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            // ✅ Add ALL parameters from your data
                            cmd.Parameters.AddWithValue("@Regionnm", SafeString(row["Regionnm"]));
                            cmd.Parameters.AddWithValue("@District_Name", SafeString(row["District_Name"]));
                            cmd.Parameters.AddWithValue("@Branch_Name", SafeString(row["DepotName"])); // Branch_Name = DepotName
                            cmd.Parameters.AddWithValue("@DepotName", SafeString(row["DepotName"]));
                            cmd.Parameters.AddWithValue("@Godown_Name", SafeString(row["Godown_Name"]));
                            cmd.Parameters.AddWithValue("@Godown_Id", SafeString(row["Godown_Id"]));
                            cmd.Parameters.AddWithValue("@PAN", SafeString(row["PAN"]));
                            cmd.Parameters.AddWithValue("@GST", SafeString(row["GST"]));
                            cmd.Parameters.AddWithValue("@Commodity", SafeString(row["Commodity"]));
                            cmd.Parameters.AddWithValue("@Crop_Year", SafeString(row["Crop_Year"]));
                            cmd.Parameters.AddWithValue("@Billing_Date", SafeString(row["Billing_Date"]));
                            cmd.Parameters.AddWithValue("@Bill_Number", billNo.Trim());
                            cmd.Parameters.AddWithValue("@Opening_Balance", SafeDecimal(row["Opening_Balance"]));
                            cmd.Parameters.AddWithValue("@Receive_Bags", SafeDecimal(row["Receive_Bags"]));
                            cmd.Parameters.AddWithValue("@Issue_Bags", SafeDecimal(row["Issue_Bags"]));
                            cmd.Parameters.AddWithValue("@Bill_Month", SafeString(row["Bill_Month"]));
                            cmd.Parameters.AddWithValue("@Closing_Balance", SafeDecimal(row["Closing_Balance"]));
                            cmd.Parameters.AddWithValue("@Reserve_Bags", SafeDecimal(row["Reserve_Bags"]));
                            cmd.Parameters.AddWithValue("@Chargable_Bags", SafeDecimal(row["Chargable_Bags"]));
                            cmd.Parameters.AddWithValue("@Total_Charges", SafeDecimal(row["Total_Charges"]));
                            cmd.Parameters.AddWithValue("@Dates_Period", SafeString(row["Dates_Period"]));
                            cmd.Parameters.AddWithValue("@Net_Amount", SafeDecimal(row["Net_Amount"]));
                            cmd.Parameters.AddWithValue("@Commodity_Rate", SafeDecimal(row["Commodity_Rate"]));

                            // Net Amount in Words (already in your data)
                            cmd.Parameters.AddWithValue("@Net_Amount_In_Words", SafeString(row["Net_Amount"])); // या अलग से words निकालना हो तो

                            // User tracking
                            cmd.Parameters.AddWithValue("@Printed_By", userID);
                            cmd.Parameters.AddWithValue("@Printed_By_IP", userIP);
                            cmd.Parameters.AddWithValue("@SessionID", sessionID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log error but don't stop the printing process
            System.Diagnostics.Debug.WriteLine("Error logging printed bills: " + ex.Message);
            // Optional: आप चाहें तो error को किसी error log table में भी save कर सकते हैं
        }
    }

    // ... (rest of your existing methods remain the same)
    private void LoadMultipleBills(List<string> billNumbers)
    {
        try
        {
            phBills.Controls.Clear();

            string username = Session["Username"] != null ? Session["Username"].ToString().ToLower() : "";
            string branchText = "NCCF";

            if (username.Contains("bhopal"))
                branchText = "BHOPAL NCCF";
            else if (username.Contains("indore"))
                branchText = "INDORE NCCF";

            int billIndex = 1;
            int totalBills = billNumbers.Count;

            foreach (string billNo in billNumbers)
            {
                if (string.IsNullOrWhiteSpace(billNo)) continue;

                DataTable dt = GetBillData(billNo.Trim());

                if (dt.Rows.Count > 0)
                {
                    // Create bill container
                    Panel billPanel = new Panel();
                    billPanel.CssClass = "bill-page";
                    billPanel.ID = "bill_" + billIndex;

                    // Add bill components
                    billPanel.Controls.Add(CreateBillHeader(dt, billNo.Trim(), billIndex, totalBills, branchText));
                    billPanel.Controls.Add(CreateInfoBox(dt, billNo.Trim(), branchText));
                    billPanel.Controls.Add(CreateGridView(dt));
                    billPanel.Controls.Add(CreateAmountBox(dt));
                    billPanel.Controls.Add(CreateSignatureBox(billNo.Trim()));
                    billPanel.Controls.Add(CreateNoticeBox());

                    phBills.Controls.Add(billPanel);
                    billIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }

    private Control CreateBillHeader(DataTable dt, string billNo, int billIndex, int totalBills, string branchText)
    {
        Panel headerPanel = new Panel();
        headerPanel.CssClass = "invoice-header";

        DataRow row = dt.Rows[0];

        Literal headerHtml = new Literal();
        headerHtml.Text = "<strong>M.P. Warehousing & Logistics Corporation - Bhopal<br />" +
                         "STORAGE BILL</strong>" +
                         "<div>" +
                         "<span>GST: </span><strong>23AADCM7742B3ZS</strong>" +
                         "&nbsp;&nbsp;" +
                         "<span>PAN: </span><strong>AADCM7742B</strong>" +
                         "</div>" +
                         "<div>" +
                         "<span>Region: </span><strong>" + SafeString(row["Regionnm"]) + "</strong>" +
                         "&nbsp;&nbsp;" +
                         "<span>District: </span><strong>" + SafeString(row["District_Name"]) + "</strong>" +
                         "&nbsp;&nbsp;" +
                         "<span>Branch: </span><strong>" + SafeString(row["DepotName"]) + "</strong>" +
                         "</div>";

        headerPanel.Controls.Add(headerHtml);
        return headerPanel;
    }

    private Control CreateInfoBox(DataTable dt, string billNo, string branchText)
    {
        Panel infoPanel = new Panel();
        infoPanel.CssClass = "info-box";

        DataRow row = dt.Rows[0];

        Literal infoHtml = new Literal();
        infoHtml.Text = "<table>" +
                        "<tr>" +
                        "<td>Warehouse Name: <strong>" + SafeString(row["Godown_Name"]) + " (" + SafeString(row["Godown_Id"]) + ")</strong></td>" +
                        "<td>Branch Name: <strong>" + branchText + "</strong></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Month: <strong>" + SafeString(row["Bill_Month"]) + "</strong></td>" +
                        "<td>Billing Date: <strong>" + DateTime.Now.ToString("dd/MM/yyyy") + "</strong></td>" +
                        "<td>Bill Number: <strong>" + billNo + "</strong></td>" +
                        "</tr>" +
                        "<tr>" +
                        "<td>Depositor Name: <strong>NCCF</strong></td>" +
                        "<td>Commodity: <strong>" + SafeString(row["Commodity"]) + "</strong></td>" +
                        "<td>Charges: <strong>" + SafeString(row["Commodity_Rate"]) + "</strong></td>" +
                        "</tr>" +
                        "</table>";

        infoPanel.Controls.Add(infoHtml);
        return infoPanel;
    }

    private Control CreateGridView(DataTable dt)
    {
        GridView gv = new GridView();
        gv.CssClass = "EU_DataTable";
        gv.AutoGenerateColumns = false;
        gv.ShowFooter = true;
        gv.GridLines = GridLines.Both;
        gv.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        gv.RowStyle.HorizontalAlign = HorizontalAlign.Center;

        // Add columns
        gv.Columns.Add(CreateTemplateField("S. No."));
        gv.Columns.Add(new BoundField { DataField = "Commodity", HeaderText = "Commodity" });
        gv.Columns.Add(new BoundField { DataField = "Bill_Month", HeaderText = "Bill Month" });
        gv.Columns.Add(new BoundField { DataField = "Dates_Period", HeaderText = "Dates Period" });
        gv.Columns.Add(new BoundField { DataField = "Opening_Balance", HeaderText = "Opening Balance" });
        gv.Columns.Add(new BoundField { DataField = "Receive_Bags", HeaderText = "Receive Bags" });
        gv.Columns.Add(new BoundField { DataField = "Issue_Bags", HeaderText = "Issue Bags" });
        gv.Columns.Add(new BoundField { DataField = "Closing_Balance", HeaderText = "Closing Balance" });
        gv.Columns.Add(new BoundField { DataField = "Reserve_Bags", HeaderText = "Reserve Bags" });
        gv.Columns.Add(new BoundField { DataField = "Chargable_Bags", HeaderText = "Chargable Bags" });
        gv.Columns.Add(new BoundField { DataField = "Total_Charges", HeaderText = "Total Charges" });

        gv.DataSource = dt;
        gv.DataBind();

        // Footer total
        if (gv.FooterRow != null)
        {
            decimal totalCharges = dt.AsEnumerable().Sum(r => SafeDecimal(r["Total_Charges"]));
            gv.FooterRow.Cells[9].Text = "Total";
            gv.FooterRow.Cells[10].Text = totalCharges.ToString("N2");
        }

        return gv;
    }

    private TemplateField CreateTemplateField(string headerText)
    {
        TemplateField tf = new TemplateField();
        tf.HeaderText = headerText;
        tf.ItemTemplate = new RowNumberTemplate();
        return tf;
    }

    //private Control CreateAmountBox(DataTable dt)
    //{
    //    Panel amountPanel = new Panel();
    //    amountPanel.CssClass = "amount-box";

    //    decimal netAmount = 0;
    //    if (dt.Rows.Count > 0 && dt.Columns.Contains("Net_Amount"))
    //    {
    //        netAmount = SafeDecimal(dt.Rows[0]["Net_Amount"]);
    //    }

    //    Literal amountHtml = new Literal();
    //    amountHtml.Text = "* Rupees: " + NumberToWords(Convert.ToInt64(netAmount)) + " Only<br/>" +
    //                     "<span style='font-size: 16px;'>Amount: ₹ " + netAmount.ToString("N2") + "</span>";

    //    amountPanel.Controls.Add(amountHtml);
    //    return amountPanel;
    //}

    //private Control CreateAmountBox(DataTable dt)
    //{
    //    Panel amountPanel = new Panel();
    //    amountPanel.CssClass = "amount-box";

    //    decimal totalAmount = 0;

    //    if (dt.Rows.Count > 0)
    //    {
    //        // Saari rows ka Total_Charges ka sum karo
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            if (dt.Columns.Contains("Total_Charges"))
    //            {
    //                totalAmount += SafeDecimal(row["Total_Charges"]);
    //            }
    //        }
    //    }

    //    Literal amountHtml = new Literal();

    //    amountHtml.Text = "* Rupees: " + NumberToWords(Convert.ToInt64(totalAmount)) + " Only<br/>";
    //    //+
    //    //                 "<span style='font-size: 16px;'>Amount: ₹ " + totalAmount.ToString("N2") + "</span>";

    //    amountPanel.Controls.Add(amountHtml);
    //    return amountPanel;
    //}


    private Control CreateAmountBox(DataTable dt)
    {
        Panel amountPanel = new Panel();
        amountPanel.CssClass = "amount-box";

        decimal totalAmount = 0;

        // Saari rows ka Total_Charges sum karo
        foreach (DataRow row in dt.Rows)
        {
            totalAmount += SafeDecimal(row["Total_Charges"]);
        }

        // Split into rupees and paise
        long rupees = (long)Math.Floor(totalAmount);
        int paise = (int)Math.Round((totalAmount - rupees) * 100);

        // Generate words for rupees and paise separately
        string amountInWords;

        if (paise == 0)
        {
            amountInWords = NumberToWords(rupees) + " Only";
        }
        else
        {
            amountInWords = NumberToWords(rupees) + " and " +
                           NumberToWords(paise) + " Paise Only";
        }

        Literal amountHtml = new Literal();
        amountHtml.Text = "* Rupees: " + amountInWords + "";
            //+
            //             "<span style='font-size: 16px;'>Amount: ₹ " + totalAmount.ToString("N2") + "</span>";

        amountPanel.Controls.Add(amountHtml);
        return amountPanel;
    }

    private Control CreateSignatureBox(string billNo)
    {
        Panel sigPanel = new Panel();
        sigPanel.CssClass = "signature-box";

        DataTable dtBranch = GetDSCDetails(billNo, "B");
        DataTable dtRegional = GetDSCDetails(billNo, "R");

        string branchImageHtml = (dtBranch.Rows.Count > 0) ? "<img src='../../images/dsc1.png' style='height:30px; width:80px;' />" : "";
        string regionalImageHtml = (dtRegional.Rows.Count > 0) ? "<img src='../../images/dsc1.png' style='height:30px; width:80px;' />" : "";

        Literal sigHtml = new Literal();
        sigHtml.Text = "<table>" +
                        "<tr>" +
                        "<td>" +
                        "<div style='text-align: center;'>" + regionalImageHtml + "</div>" +
                        GetDSCHtml(dtRegional) +
                        "<span class='signature-title'>Signature of Regional Manager</span>" +
                        "</td>" +
                        "<td>" +
                        "<div style='text-align: center;'>" + branchImageHtml + "</div>" +
                        GetDSCHtml(dtBranch) +
                        "<span class='signature-title'>Signature of Branch Manager</span>" +
                        "</td>" +
                        "</tr>" +
                        "</table>";

        sigPanel.Controls.Add(sigHtml);
        return sigPanel;
    }

    private Control CreateNoticeBox()
    {
        Panel noticePanel = new Panel();
        noticePanel.CssClass = "notice-box";

        Literal noticeHtml = new Literal();
        noticeHtml.Text = "* THE ABOVE STORED STOCKS ARE KEPT IN GOOD CONDITION WITH PROPER FUMIGATION & SCIENTIFIC STORAGE BY MPWLC.<br />" +
                         "* This bill is digitally signed, therefore it does not require any stamp & signature.";

        noticePanel.Controls.Add(noticeHtml);
        return noticePanel;
    }

    private DataTable GetBillData(string billNumber)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_NCCF_Bill_Branch_For_Print", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bill_Number", billNumber);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
        }
        return dt;
    }

    private DataTable GetDSCDetails(string billNo, string userType)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "select DSC_Serial_No, DSC_Holder_Name, Client_Ip, " +
                          "Convert(varchar(10), CreatedDate, 103) as CreatedDate " +
                          "from tbl_Digitally_Signed_Bill_Details_NCCF " +
                          "where Ref_Bill_No=@BillNo and DSC_User_Type=@UserType";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@BillNo", billNo);
                cmd.Parameters.AddWithValue("@UserType", userType);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }

    private string GetDSCHtml(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            return "<strong>DSC Serial No: " + SafeString(row["DSC_Serial_No"]) + "</strong><br/>" +
                   "DSC Holder Name: " + SafeString(row["DSC_Holder_Name"]) + "<br/>" +
                   "Date: " + SafeString(row["CreatedDate"]) + "<br/>" +
                   "IP: " + SafeString(row["Client_Ip"]) + "<br/>";
        }
        return "";
    }

    //private void ShowSummary(List<string> billNumbers)
    //{
    //    //summaryBox.Visible = true;

    //    decimal totalAmount = 0;
    //    int totalBills = billNumbers.Count;

    //    foreach (string billNo in billNumbers)
    //    {
    //        DataTable dt = GetBillData(billNo);
    //        if (dt.Rows.Count > 0 && dt.Columns.Contains("Net_Amount"))
    //        {
    //            totalAmount += SafeDecimal(dt.Rows[0]["Net_Amount"]);
    //        }
    //    }

    //    string billList = string.Join(", ", billNumbers.Take(3));
    //    if (billNumbers.Count > 3)
    //        billList = billList + " and " + (billNumbers.Count - 3) + " more";

    //    //lblSummary.Text = "Total Bills: " + totalBills + " | Total Amount: ₹ " + totalAmount.ToString("N2") + " | Bills: " + billList;
    //}

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        // Client-side PDF export handled by JavaScript
    }

    private string SafeString(object value)
    {
        return value != null && value != DBNull.Value ? value.ToString() : "";
    }

    private decimal SafeDecimal(object value)
    {
        if (value != null && value != DBNull.Value)
        {
            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
                return result;
        }
        return 0;
    }

    private string NumberToWords(long number)
    {
        if (number == 0) return "Zero";

        string words = "";

        if ((number / 10000000) > 0)
        {
            words = words + NumberToWords(number / 10000000) + " Crore ";
            number = number % 10000000;
        }

        if ((number / 100000) > 0)
        {
            words = words + NumberToWords(number / 100000) + " Lakh ";
            number = number % 100000;
        }

        if ((number / 1000) > 0)
        {
            words = words + NumberToWords(number / 1000) + " Thousand ";
            number = number % 1000;
        }

        if ((number / 100) > 0)
        {
            words = words + NumberToWords(number / 100) + " Hundred ";
            number = number % 100;
        }

        if (number > 0)
        {
            if (words != "")
                words = words + "and ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number < 20)
                words = words + unitsMap[number];
            else
            {
                words = words + tensMap[number / 10];
                if ((number % 10) > 0)
                    words = words + " " + unitsMap[number % 10];
            }
        }

        return words;
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}

// Template class for row numbers
public class RowNumberTemplate : ITemplate
{
    public void InstantiateIn(Control container)
    {
        Literal literal = new Literal();
        literal.DataBinding += (sender, e) =>
        {
            Literal lit = (Literal)sender;
            GridViewRow row = (GridViewRow)lit.NamingContainer;
            lit.Text = (row.RowIndex + 1).ToString();
        };
        container.Controls.Add(literal);
    }
}