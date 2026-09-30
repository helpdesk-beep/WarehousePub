using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_rpt_Get_Schedule_Inspection_Pendency_RO : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

    // ग्रैंड टोटल काउंटर्स
    int grandAllotted = 0;
    int grandCompleted = 0;
    int grandPending = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // सुरक्षा जांच: यदि सेशन समाप्त हो गया हो
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx"); // अपने ओरिजिनल लॉगिन पेज का पाथ यहाँ सेट करें
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

        // सेशन से लॉगिन रीजन आईडी प्राप्त करना
        int regionId = Convert.ToInt32(Session["UserId"]);

        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.rpt_Get_Schedule_Inspection_Pendency_Region", con))
            {
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.StoredProcedure;

                int inspectionTypeID = 0;
                if (!string.IsNullOrEmpty(ddlQuarter.SelectedValue))
                {
                    inspectionTypeID = Convert.ToInt32(ddlQuarter.SelectedValue);
                }

                // स्टोर प्रोसीजर पैरामीटर्स (रीजन आईडी के साथ)
                cmd.Parameters.AddWithValue("@InspectionTypeID", inspectionTypeID);
                cmd.Parameters.AddWithValue("@Inspection_month_ID", 0); // डिफॉल्ट 0
                cmd.Parameters.AddWithValue("@Region_Id", regionId);    // सेशन से भेजी जा रही रीजन आईडी

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvReport.DataSource = dt;
                    gvReport.DataBind();

                    if (dt.Rows.Count > 0)
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

            // फॉर्मेटिंग स्टाइल्स लागू करना
            for (int i = 3; i <= 5; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Attributes.Add("style", "text-align:left !important; font-weight:bold !important; padding-left:10px !important;");
            e.Row.Cells[2].Text = "";

            e.Row.Cells[3].Text = grandAllotted.ToString("N0");
            e.Row.Cells[4].Text = grandCompleted.ToString("N0");
            e.Row.Cells[5].Text = grandPending.ToString("N0");

            for (int j = 3; j <= 5; j++)
            {
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
        Response.AddHeader("content-disposition", "attachment;filename=RO_Inspection_Pendency_Report.xls");
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
                        <tr><th colspan='6' style='font-size:15pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='6' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Regional Office - Scheduled Inspection Status & Pendency Report</th></tr>
                        <tr><td colspan='3' style='text-align:left; font-weight:bold; color:#475569;'>Financial Year: 2026-27</td><td colspan='3' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='6' style='border:none;'>&nbsp;</td></tr>
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
        // एक्सेल एक्सपोर्ट रेंडरिंग कन्फर्मेशन के लिए जरूरी विधि
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pending")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string regionID = ((HiddenField)row.FindControl("hdnRegion_ID")).Value;
            string inspectionTypeID = ((HiddenField)row.FindControl("hdnInspection_type_ID")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerification_Type")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_year")).Value;

            // 🔷 पैरामीटर ऐरे तैयार करना और सेशन में डालना
            string[] paramArray = new string[4];
            paramArray[0] = regionID;          // यह सेशन से ली हुई Region_ID ही रहेगी
            paramArray[1] = inspectionTypeID;
            paramArray[2] = verificationType;
            paramArray[3] = financialYear;

            Session["GodownParams"] = paramArray;

            // नया पेज ओपन करने के लिए स्क्रिप्ट
            string script = "window.open('BranchWisePendingInspection_region.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}