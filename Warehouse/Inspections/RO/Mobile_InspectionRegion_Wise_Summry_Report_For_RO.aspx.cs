using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_RO_Mobile_InspectionRegion_Wise_Summry_Report_For_RO : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindgrid();
        }
    }
    private void Bindgrid()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("Region_Wise_Mobile_Inspection_Report_For_RO", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvReport.DataSource = dt;
                gvReport.DataBind();
                if (dt.Rows.Count > 0)
                {
                    lblBranch.Text = dt.Rows.Count.ToString();
                    lblGodown.Text = dt.Compute("SUM(Total_Godown)", "").ToString();
                    lblStack.Text = dt.Compute("SUM(Total_Stack)", "").ToString();
                    lblBags.Text = dt.Compute("SUM(Total_Bags)", "").ToString();
                }
            }
        }
    }
    protected void gvReport_PreRender(object sender, EventArgs e)
    {
        if (gvReport.Rows.Count > 0)
        {
            gvReport.UseAccessibleHeader = true;
            gvReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    public class InspectionParams
    {
        public string BranchId { get; set; }
        public string FinancialYear { get; set; }
        public string Employee_ID { get; set; }
        public string Quarter { get; set; }
        public string VerificationType { get; set; }
    }
    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton btn = (LinkButton)e.CommandSource;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string Region_ID = ((HiddenField)row.FindControl("hdnRegion_ID")).Value;
        string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
        string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
        string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;

        // 🔷 Array prepare
        string[] paramArray = new string[4];
        paramArray[0] = Region_ID;
        paramArray[1] = inspectionQuarter;
        paramArray[2] = verificationType;
        paramArray[3] = financialYear;

        // 🔷 Session store
        Session["GodownParams"] = paramArray;
        // 🔷 BranchId separate store (important)
        //Session["BranchId"] = branchId;
        // 🔷 New tab open
        string script = "window.open('Mobile_Inspection_Branch_Summary_Report_For_RO.aspx','_blank');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Inspections/RO/RO_Welcome.aspx");
    }
}