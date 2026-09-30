using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_rpt_Get_Schedule_Inspection_Pendency : System.Web.UI.Page
{
    // Web.config connection string integration
    string connString = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    // Summary Global Counters for Calculations
    int grandAllotted = 0;
    int grandCompleted = 0;
    int grandPending = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindInspectionReport();
            //fillMonth();
        }
    }

    private void BindInspectionReport()
    {
        ResetCounters();
        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.rpt_Get_Schedule_Inspection_Pendency", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                int inspectionTypeID = 0;
                if (!string.IsNullOrEmpty(ddlQuarter.SelectedValue))
                {
                    inspectionTypeID = Convert.ToInt32(ddlQuarter.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@InspectionTypeID", inspectionTypeID);
                int inspectionMonthID = 0;
                //if (!string.IsNullOrEmpty(ddlmonth.SelectedValue))
                //{
                //    inspectionMonthID = Convert.ToInt32(ddlmonth.SelectedValue);
                //}
                //cmd.Parameters.AddWithValue("@Inspection_month_ID", inspectionMonthID);
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
    //private void fillMonth()
    //{
    //    try
    //    {
    //        string query = "";
    //        query = "Select Distinct Inspection_month_ID,(CASE WHEN Inspection_month_ID ='1' THEN 'January' WHEN Inspection_month_ID ='2' THEN 'February' WHEN Inspection_month_ID ='3' THEN 'March' WHEN Inspection_month_ID ='4' THEN 'April' WHEN Inspection_month_ID ='5' THEN 'May' WHEN Inspection_month_ID ='6' THEN 'June' WHEN Inspection_month_ID ='7' THEN 'July' WHEN Inspection_month_ID ='8' THEN 'August' WHEN Inspection_month_ID ='9' THEN 'September' WHEN Inspection_month_ID ='10' THEN 'October' WHEN Inspection_month_ID ='11' THEN 'November' WHEN Inspection_month_ID ='12' THEN 'December' END) As Month_Name from JointVentureScheme2018.dbo.Inspection_Scheduled_For_Officer Where Inspection_month_ID NOT IN(0,13,14) Order By Inspection_month_ID ASC";
    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlmonth.DataSource = ds.Tables[0];
    //            ddlmonth.DataTextField = "Month_Name";
    //            ddlmonth.DataValueField = "Inspection_month_ID";
    //            ddlmonth.DataBind();
    //            ddlmonth.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ddlmonth.Items.Clear();
    //            ddlmonth.Items.Insert(0, "Select");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Cumulative evaluations matching dataset columns explicitly
            grandAllotted += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofallottedInspection"));
            grandCompleted += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofCompleteInspection"));
            grandPending += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofpendingInspection"));

            // Text Right Alignment Alignment Rules Injection via MSO Number styles
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

            // Mapping absolute computed metrics safely to final visible layout rows
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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Inspection_Pendency_Summary_Report.xls");
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
                        <tr><th colspan='6' style='font-size:12pt; font-weight:bold; background-color:#f1f5f9; color:#1e3a8a; text-align:center;'>Scheduled Inspection Status & Pendency Analysis Report</th></tr>
                        <tr><td colspan='3' style='text-align:left; font-weight:bold; color:#475569;'>Financial Year: 2026-27</td><td colspan='3' style='text-align:right; font-weight:bold; color:#475569;'>Generated On: {0}</td></tr>
                        <tr><td colspan='6' style='border:none;'>&nbsp;</td></tr>
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

                gvReport.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindInspectionReport();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
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
           // string Inspection_month_ID = ((HiddenField)row.FindControl("hdnInspection_month_ID")).Value;
            // 🔷 Array prepare
            string[] paramArray = new string[4];
            paramArray[0] = regionID;
            paramArray[1] = inspectionTypeID;
            paramArray[2] = verificationType;
            paramArray[3] = financialYear;
            //paramArray[4] = Inspection_month_ID;
            // 🔷 Session store
            Session["GodownParams"] = paramArray;
            string script = "window.open('BranchWisePendingInspection.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}