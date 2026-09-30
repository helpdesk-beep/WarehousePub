using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_rpt_Get_Schedule_Inspection_Pendency_DO : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;

    // ग्रैंड टोटल काउंटर्स
    int grandAllotted = 0;
    int grandCompleted = 0;
    int grandPending = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // सुरक्षा जांच: यदि डिस्ट्रिक्ट यूजर लॉगिन सेशन मौजूद नहीं है
        if (Session["Depot_DistID"] == null)
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

        // सेशन से डिस्ट्रिक्ट आईडी प्राप्त की जा रही है
        int districtId = Convert.ToInt32(Session["Depot_DistID"]);

        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.rpt_Get_Schedule_Inspection_Pendency_District", con))
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
                cmd.Parameters.AddWithValue("@DistrictId", districtId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // डेटा सुरक्षा फ़िल्टर
                    if (dt.Columns.Contains("District_id"))
                    {
                        DataView dv = new DataView(dt);
                        dv.RowFilter = "District_id = " + districtId;
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

            // 🔷 फ्रंट-एंड के अनुसार न्यूमेरिकल कॉलम्स सेल्स इंडेक्स 4 और 5 पर हैं
            // Cells[4] = Allotted, Cells[5] = Completed, Cells[6] = Pending Link Button
            e.Row.Cells[4].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[5].Attributes.Add("style", "text-align:right !important; mso-number-format:\\#\\,\\#\\#0;");
            e.Row.Cells[6].Attributes.Add("style", "text-align:right !important;");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // 🔷 फ़ुटर सेल्स इंडेक्स को फ्रंट-एंड ग्रिडव्यू डिज़ाइन (6 कॉलम्स टोटल) के अनुसार सटीक मैप किया गया
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Attributes.Add("style", "text-align:left !important; font-weight:bold !important; padding-left:10px !important;");
            e.Row.Cells[2].Text = "";
            e.Row.Cells[3].Text = "";

            e.Row.Cells[4].Text = grandAllotted.ToString("N0");
            e.Row.Cells[5].Text = grandCompleted.ToString("N0");
            e.Row.Cells[6].Text = grandPending.ToString("N0");

            for (int j = 4; j <= 6; j++)
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
        Response.AddHeader("content-disposition", "attachment;filename=District_Inspection_Pendency_Report.xls");
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
                        <tr><th colspan='7' style='font-size:15pt; font-weight:bold; background-color:#1e3a8a; color:#ffffff; text-align:center;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>
                        <tr><th colspan='7' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>District Office - Scheduled Inspection Status Report</th></tr>
                        <tr><td colspan='4' style='text-align:left; font-weight:bold; color:#475569;'>Financial Year: 2026-27</td><td colspan='3' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='7' style='border:none;'>&nbsp;</td></tr>
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
        // Excel Verification
    }

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Pending")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string regionID = ((HiddenField)row.FindControl("hdnRegion_ID")).Value;
            string districtID = ((HiddenField)row.FindControl("hdnDistrict_ID")).Value;
            string inspectionTypeID = ((HiddenField)row.FindControl("hdnInspection_type_ID")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerification_Type")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_year")).Value;

            // 🔷 ऐरे साइज को ठीक करके 5 पैरामीटर के अनुसार सेट किया गया
            string[] paramArray = new string[5];
            paramArray[0] = regionID;
            paramArray[1] = inspectionTypeID;
            paramArray[2] = verificationType;
            paramArray[3] = financialYear;
            paramArray[4] = districtID;

            Session["GodownParams"] = paramArray;

            string script = "window.open('BranchWisePendingInspection_District.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}