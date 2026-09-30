using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_BO_rpt_Get_Schedule_Inspection_Pendency_BO : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

    // ग्रैंड टोटल काउंटर्स
    int grandAllotted = 0;
    int grandCompleted = 0;
    int grandPending = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // सुरक्षा जांच: यदि ब्रांच लॉगिन सेशन मौजूद नहीं है
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindInspectionReport();
        }
    }

    private void BindInspectionReport()
    {
        ResetCounters();

        // 🔷 सेशन से अब सीधे ब्रांच आईडी निकाली जा रही है
        int branchId = Convert.ToInt32(Session["UserId"]);

        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.rpt_Get_Schedule_Inspection_Pendency_Branch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                int inspectionTypeID = 0;
                if (!string.IsNullOrEmpty(ddlQuarter.SelectedValue))
                {
                    inspectionTypeID = Convert.ToInt32(ddlQuarter.SelectedValue);
                }

                cmd.Parameters.AddWithValue("@InspectionTypeID", inspectionTypeID);
                cmd.Parameters.AddWithValue("@Inspection_month_ID", 0);
                cmd.Parameters.AddWithValue("@Region_Id", 0);
                cmd.Parameters.AddWithValue("@DistrictId", 0); // डिस्ट्रिक्ट को 0 किया क्योंकि हम सीधे ब्रांच से फ़िल्टर कर रहे हैं
                cmd.Parameters.AddWithValue("@BranchId", branchId); // 🔷 यहाँ सेशन की ब्रांच आईडी पास कर दी गई

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // 🔷 डेटा को ब्रांच आईडी से डबल सिक्योर फ़िल्टर करने के लिए
                    if (dt.Columns.Contains("Branch_ID"))
                    {
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "Branch_ID = " + branchId;
                        gvReport.DataSource = dv.ToTable();
                    }
                    else
                    {
                        gvReport.DataSource = dt;
                    }

                    gvReport.DataBind();

                    if (gvReport.Rows.Count > 0)
                    {
                        gvReport.UseAccessibleHeader = true;
                        gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grandAllotted += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofallottedInspection"));
            grandCompleted += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofCompleteInspection"));
            grandPending += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofpendingInspection"));

            // न्यूमेरिकल कॉलम सेल्स इंडेक्स 5, 6 पर राइट एलाइनमेंट फॉर्मेटिंग लगाना
            for (int i = 5; i <= 7; i++)
            {
                if (i < 7)
                {
                    e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Attributes.Add("style", "text-align:left !important; font-weight:bold !important; padding-left:10px !important;");
            e.Row.Cells[2].Text = "";
            e.Row.Cells[3].Text = "";
            e.Row.Cells[4].Text = "";

            // कुल योग वैल्यूज को सही सेल लोकेशंस पर मैप किया गया
            e.Row.Cells[5].Text = grandAllotted.ToString("N0");
            e.Row.Cells[6].Text = grandCompleted.ToString("N0");
            e.Row.Cells[7].Text = grandPending.ToString("N0");

            for (int j = 5; j <= 7; j++)
            {
                j.ToString();
                e.Row.Cells[j].Attributes.Add("style", "text-align:right !important; font-weight:bold !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
    }

    private void ResetCounters()
    {
        grandAllotted = 0;
        grandCompleted = 0;
        grandPending = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindInspectionReport();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BO_Branch_Inspection_Pendency_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                BindInspectionReport();

                string dateTimeStr = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
                string customExcelHeader = String.Format(@"
                    <table cellspacing='0' cellpadding='4' border='1' style='font-family:Arial, sans-serif; font-size:10pt;'>
                        <tr><th colspan='8' style='font-size:15pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='8' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Branch Office - Inspection Status Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Financial Year: 2026-27</td><td colspan='4' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='8' style='border:none;'>&nbsp;</td></tr>
                    </table>", dateTimeStr);

                string style = @"<style> 
                    th { background-color: #2563eb !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important;} 
                    td { border:1px solid #cbd5e1 !important; } 
                    .text-right-align { text-align: right !important; }
                    .footer-style td { background-color: #eff6ff !important; font-weight: bold !important; color: #1e3a8a !important; }
                </style>";

                Response.Write(style);
                Response.Write(customExcelHeader);

                gvReport.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Excel Rendering Verification
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pending")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string regionID = ((HiddenField)row.FindControl("hdnRegion_ID")).Value;
            string branchID = ((HiddenField)row.FindControl("hdnBranch_ID")).Value;
            string inspectionTypeID = ((HiddenField)row.FindControl("hdnInspection_type_ID")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerification_Type")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_year")).Value;

            // पैरामीटर ऐरे (सत्र/रो से प्राप्त ब्रांच विवरण को आगे भेजा जा रहा है)
            string[] paramArray = new string[6];
            paramArray[0] = regionID;
            paramArray[1] = inspectionTypeID;
            paramArray[2] = verificationType;
            paramArray[3] = financialYear;
            paramArray[4] = branchID;

            Session["GodownParams"] = paramArray;

            string script = "window.open('BranchWisePendingInspection_Branch.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}