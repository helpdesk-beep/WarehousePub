using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

public partial class Inspections_Inspection_Officer_Inspection_Office_Mobile_Inspection_Summary_Report : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ToString());
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindInspectionGrid();
        }

    }

    protected void BindInspectionGrid()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
            {
                // Aapka stored procedure call
                SqlCommand cmd = new SqlCommand("Inspection_Officer_Mobile_Inspection_Summary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_ID", Session["UserId"].ToString());

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gvInspectionSummary.DataSource = dt;
                    gvInspectionSummary.DataBind();
                    lblTotalCount.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvInspectionSummary.DataSource = null;
                    gvInspectionSummary.DataBind();
                    lblTotalCount.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            // Handle error
        }
    }
    protected void lnkViewSummary_Command(object sender, CommandEventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
        string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
        string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
        string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
        string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;
        string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;

        // 🔷 Array prepare
        string[] paramArray = new string[5];

        paramArray[0] = empId;
        paramArray[1] = inspectionQuarter;
        paramArray[2] = verificationType;
        paramArray[3] = financialYear;
        paramArray[4] = orderNo;

        // 🔷 Session store
        Session["GodownParams"] = paramArray;

        // 🔷 BranchId separate store (important)
        Session["BranchId"] = branchId;

        // 🔷 New tab open
        string script = "window.open('GodownWiseSummary_For_Mobile_Inspection.aspx','_blank');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
    }
    //protected void lnkViewSummary_Command(object sender, CommandEventArgs e)
    //{
    //    LinkButton btn = (LinkButton)sender;
    //    GridViewRow row = (GridViewRow)btn.NamingContainer;

    //    string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
    //    string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
    //    string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
    //    string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
    //    string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;
    //    string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;

    //    // ✅ QueryString ko array/collection me convert kiya
    //    NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

    //    query["EmpId"] = empId;
    //    query["InspectionQuarter"] = inspectionQuarter;
    //    query["VerificationType"] = verificationType;
    //    query["FinancialYear"] = financialYear;
    //    query["OrderNo"] = orderNo;
    //    query["BranchId"] = branchId;

    //    string url = "GodownWiseSummary_For_Mobile_Inspection.aspx?" + query.ToString();
    //    string script = "window.open('" + url + "', '_blank');";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
    //    //Response.Redirect(url);
    //}
}