//using Microsoft.JScript;
using Microsoft.ReportingServices.Rendering.ImageRenderer;
using Microsoft.ReportingServices.ReportRendering;
using System;
using System.Activities.Expressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Xml.Linq;
using iTextSharp.tool.xml;
using System.Collections.Generic;


public partial class Accounting_State_Nafed_Print_Generate_Bill_AllBillPrint : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string qry;
    decimal TT1;

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["BillNos"] != null)
            {
                fillgrid(Session["BillNos"].ToString());

            }
            else
            {
                Response.Write("<script>alert('No bills selected!');</script>");
            }
            //List<string> selectedBills = Session["SelectedBillNos"] as List<string>;
            //if (selectedBills != null && selectedBills.Count > 0)
            //{
            //    string bills = string.Join(",", selectedBills); // same format as fillgrid expects
            //    fillgrid(bills);

            //    // ✅ Optional: Store again in Session for PDF export
            //    Session["BillNos"] = bills;
            //}
            //else
            //{
            //    Response.Write("<script>alert('No bills selected!');</script>");
            //}
        }
    }



    protected void fillgrid(string BillNos)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                // Split comma separated Bill Numbers
                string[] billArray = BillNos.Split(',');

                // Clear placeholder
                phBills.Controls.Clear();

                foreach (string billNo in billArray)
                {
                    if (string.IsNullOrWhiteSpace(billNo)) continue;

                    using (SqlCommand cmd = new SqlCommand("Get_Nafed_Bill_For_Print_New", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bill_Number", billNo.Trim());

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                // ✅ हर Bill अलग Panel (नए पेज पर जाएगा)
                                Panel billPanel = new Panel();
                                billPanel.CssClass = "bill-page";

                                // ✅ Bill Header
                                Label lblHeader = new Label();
                                lblHeader.Text = String.Format(@"
                                <div style='width: 100%; background-repeat: no-repeat; background-position: center;'>
                                    <div style='position: relative; border-bottom: 3px solid black;'>
                                        <p style='text-align: center;'>
                                            <strong>M.P.Warehousing & Logistics Corporation - Bhopal 
                                            <br style='margin-top: 10px' />STORAGE BILL</strong>
                                            <br />
                                            <span style='margin-left: 5px; padding: 5px;'>GST :</span><strong>23AADCM7742B3ZS</strong>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<span style='margin-left: 5px; padding: 5px;'>PAN :</span><strong>AADCM7742B</strong><br />
                                            <span style='margin-left: 5px; margin-top: 10px; padding: 5px;'>Region Name :</span><strong>{0}</strong>
                                            <span style='margin-left: 5px; margin-top: 10px; padding: 5px;'>District Name :</span><strong>{1}</strong>
                                            <span style='margin-left: 5px; margin-top: 10px; padding: 5px;'>Branch Name :</span><strong>{2}</strong>
                                        </p>
                                    </div>
                                </div>

                                <table border='1' width='100%'>
                                    <tr>
                                        <td align='center' colspan='4'>Warehouse Name: &nbsp;&nbsp;<strong>{3} ({4})</strong></td>
                                    </tr>
                                    <tr>
                                        <td align='left' colspan='2'>
                                            Month :- <strong>&nbsp;&nbsp;{5}</strong><br />
                                            Billing Date :- <strong>&nbsp;&nbsp;{6}</strong><br />
                                            Bill Number :- <strong>&nbsp;&nbsp;{7}</strong><br />
                                        </td>
                                        <td align='left'>
                                            Depositor Name :- <strong>&nbsp;&nbsp;NAFED-BHOPAL</strong><br />
                                            Commodity :- <strong>&nbsp;&nbsp;{8}</strong><br />
                                            Charges :- <strong>Rs. {9} PER BAG / PER MONTH</strong><br />
                                        </td>
                                    </tr>
                                </table>",
                                    dt.Rows[0]["Regionnm"],        // {0}
                                    dt.Rows[0]["District_Name"],   // {1}
                                    dt.Rows[0]["DepotName"],       // {2}
                                    dt.Rows[0]["Godown_Name"],     // {3}
                                    dt.Rows[0]["Godown_Id"],       // {4}
                                    dt.Rows[0]["Bill_Month"],      // {5}
                                    dt.Rows[0]["Billing_Date"],    // {6}
                                    dt.Rows[0]["Bill_Number"],     // {7}
                                    dt.Rows[0]["Commodity"],       // {8}
                                    dt.Rows[0]["Rate"]             // {9}
                                );

                                billPanel.Controls.Add(lblHeader);

                                // ✅ GridView
                                GridView gv = new GridView();
                                gv.ID = "GD_" + billNo.Trim();
                                gv.CssClass = "EU_DataTable";
                                gv.AutoGenerateColumns = false;
                                gv.ShowFooter = true;
                                gv.GridLines = GridLines.Both;
                                gv.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                                gv.RowStyle.HorizontalAlign = HorizontalAlign.Center;

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

                                // ✅ Footer total
                                decimal totalCharges = dt.AsEnumerable().Sum(r => r.Field<decimal>("Total_Charges"));
                                gv.FooterRow.Cells[8].Text = "Total";
                                gv.FooterRow.Cells[9].Text = totalCharges.ToString("N2");

                                billPanel.Controls.Add(gv);

                                // ✅ Bill Footer
                                Label lblFooter = new Label();
                                lblFooter.Text = String.Format(@"
                                <div style='width: 100%; background-repeat: no-repeat; background-position: center;'>
                                    <div style='position: relative; border-bottom: 3px solid black;'>
                                        <p style='text-align: start;'>
                                            <strong style='color: red'> *Rupees :- </strong><strong>{0}</strong>
                                            <br />
                                            <strong style='color: red'> *THE ABOVE STORED STOCK ARE KEPT IN GOOD CONDITION WITH PROPER FUMIGATION & SCIENTIFIC STORAGE BY MPWLC!!!</strong>
                                            <br />
                                        </p>
                                    </div>
                                </div>",
                                    dt.Rows[0]["Net_Amount"]
                                );

                                billPanel.Controls.Add(lblFooter);

                                // ✅ Add billPanel into placeholder
                                phBills.Controls.Add(billPanel);

                                // ✅ Call DSC methods for each bill
                                AddDSCDetails(billNo.Trim(), billPanel);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.Message);
        }
    }


    public void AddDSCDetails(string Bill_No, Panel billPanel)
    {
        string qry = "select DSC_User_Type, DSC_Serial_No, DSC_Holder_Name, Client_Ip," +
                     "Convert(varchar(10),CreatedDate,103) as CreatedDate " +
                     "from tbl_Digitally_Signed_Bill_Details_NAFED " +
                     "where Ref_Bill_No=@BillNo and DSC_User_Type IN ('B','R')";

        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(qry, con))
        {
            cmd.Parameters.AddWithValue("@BillNo", Bill_No);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string bmHtml = "";
                string rmHtml = "";

                DataRow[] bm = dt.Select("DSC_User_Type = 'B'");
                if (bm.Length > 0)
                {
                    bmHtml = "<div style='flex:1; padding:10px; margin-right:5px; text-align:end;'>" +
                             "<img src='../images/dsc1.png' width='80' /><br/>" +
                             "<b>DSC (BM) Details</b><br/>" +
                             "DSC Serial No: " + bm[0]["DSC_Serial_No"] + "<br/>" +
                             "Client IP: " + bm[0]["Client_Ip"] + "<br/>" +
                             "Holder Name: " + bm[0]["DSC_Holder_Name"] + "<br/>" +
                             "Sign Date: " + bm[0]["CreatedDate"] + "<br/>" +
                             "<p>Signature Of Branch Mananger</p>" +
                             "</div>";
                }

                DataRow[] rm = dt.Select("DSC_User_Type = 'R'");
                if (rm.Length > 0)
                {
                    rmHtml = "<div style='flex:1; padding:10px; margin-left:5px; text-align:end;'>" +
                             "<img src='../images/dsc1.png' width='80' /><br/>" +
                             "<b>DSC (RM) Details</b><br/>" +
                             "DSC Serial No: " + rm[0]["DSC_Serial_No"] + "<br/>" +
                             "Client IP: " + rm[0]["Client_Ip"] + "<br/>" +
                             "Holder Name: " + rm[0]["DSC_Holder_Name"] + "<br/>" +
                             "Sign Date: " + rm[0]["CreatedDate"] + "<br/>" +
                             "<p>Signature Of Regional Mananger</p>" +
                             "</div>";
                }

                Label lbl = new Label();
                lbl.Text = "<div style='display:flex; justify-content:space-between; width:100%;'>" +
                           bmHtml + rmHtml + "</div>" +
                           "<div style = 'width: 100%; background-repeat: no-repeat; background-position: center;' >" +
                                "<div style = 'position: relative; border-bottom: 3px solid black;' >" +
                                "<hr/>" +
                                    "<p style = 'text-align: center;' >" +
                                        "<strong style = 'color: red' >" +
                                             "*This bill is digitally signed, therefore, it does not require any stamp &sign!!!" +
                                        "</strong >" +
                                        "<br />" +
                                    "</p>" +
                                "</div>" +
                            "</div>";

                billPanel.Controls.Add(lbl);
            }
        }
    }

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        if (Session["BillNos"] != null)
        {
            fillgrid(Session["BillNos"].ToString());
        }
        else
        {
            Response.Write("⚠️ No bills to export!");
            return;
        }

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        phBills.RenderControl(hw);
        string htmlContent = sw.ToString();

        string fullHtml = "<html><head><style>" +
                          "body { font-family: Cambria; font-size: 10pt; }" +
                          "table { border-collapse: collapse; width:100%; font-size:9pt; }" +
                          "th, td { border:1px solid black; padding:4px; text-align:center; }" +
                          ".EU_DataTable { width:100%; border-collapse: collapse; }" +
                          ".EU_DataTable th { background-color:#f2f2f2; }" +
                          ".EU_DataTable td { text-align:center; }" +
                          "</style></head><body>" + htmlContent + "</body></html>";

        using (MemoryStream ms = new MemoryStream())
        {
            using (Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 20f))
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();
                using (StringReader sr = new StringReader(fullHtml))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                }
                pdfDoc.Close();
            }

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Bills.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }


    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
