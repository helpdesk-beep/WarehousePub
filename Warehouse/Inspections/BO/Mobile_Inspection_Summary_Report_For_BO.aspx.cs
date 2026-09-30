using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
 
public partial class Inspections_BO_Mobile_Inspection_Summary_Report_For_BO : System.Web.UI.Page
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
        //if (Session["GodownParams"] == null)
        //{
        //    //lblCount.Text = "Session Expired";
        //    return;
        //}
        //string[] param = (string[])Session["GodownParams"];

        string Branch_ID = Session["UserId"].ToString();
        //string inspectionQuarter = param[1];
        //string verificationType = param[2];
        //string financialYear = param[3]; 

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Mobile_Inspection_Report_For_BO", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                //cmd.Parameters.AddWithValue("@Quater_Type", inspectionQuarter);
                //cmd.Parameters.AddWithValue("@Verification_Type", verificationType);
                //cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
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

                //lblTotalOnlineStackCard.Text = lblTotalOnlineStackCard.ToString();
                //lblTotalInspStackCard.Text = lblTotalInspStackCard.ToString();
                //lblTotalPVBagsCard.Text = lblTotalPVBagsCard.ToString();
                //lblTotalSpillageCard.Text = lblTotalSpillageCard.ToString();
                //lblCount.Text = dt.Rows.Count.ToString();
            }
        }

        //using (SqlConnection con = new SqlConnection(conStr))
        //{
        //    using (SqlCommand cmd = new SqlCommand("Get_Mobile_Inspection_Report_For_HO", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        gvReport.DataSource = dt;
        //        gvReport.DataBind();
        //        if (dt.Rows.Count > 0)
        //        {
        //            lblBranch.Text = dt.Rows.Count.ToString();
        //            lblGodown.Text = dt.Compute("SUM(Total_Godown)", "").ToString();
        //            lblStack.Text = dt.Compute("SUM(Total_Stack)", "").ToString();
        //            lblBags.Text = dt.Compute("SUM(Total_Bags)", "").ToString();
        //        }
        //    }
        //}
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
        public string Order_No { get; set; }
        public string VerificationType { get; set; }
    }
    //protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    LinkButton btn = (LinkButton)e.CommandSource;
    //    GridViewRow row = (GridViewRow)btn.NamingContainer;
    //    string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;
    //    string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
    //    string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
    //    string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
    //    string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
    //    string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;
    //    // 🔷 Array prepare
    //    string[] paramArray = new string[6];
    //    paramArray[0] = branchId;
    //    paramArray[1] = empId;
    //    paramArray[2] = inspectionQuarter;
    //    paramArray[3] = verificationType;
    //    paramArray[4] = financialYear;
    //    paramArray[5] = orderNo;
    //    // 🔷 Session store
    //    Session["GodownParams"] = paramArray;
    //    // 🔷 BranchId separate store (important)
    //    //Session["BranchId"] = branchId;
    //    // 🔷 New tab open
    //    string script = "window.open('Godown_Wise_Inspection_Details.aspx','_blank');";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
    //}
    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AllStock")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;
            string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
            string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
            string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;

            // Array Prepare
            string[] paramArray = new string[6];

            paramArray[0] = branchId;
            paramArray[1] = empId;
            paramArray[2] = inspectionQuarter;
            paramArray[3] = verificationType;
            paramArray[4] = financialYear;
            paramArray[5] = orderNo;

            // Session Store
            Session["StackParams"] = paramArray;

            // Open New Tab
            string script = "window.open('AllStock_Wise_Inspection_Details_BO.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        else if (e.CommandName == "ViewAllGodownWiseStack")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;
            string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
            string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
            string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;

            // Array Prepare
            string[] paramArray = new string[6];

            paramArray[0] = empId;
            paramArray[1] = inspectionQuarter;
            paramArray[2] = verificationType;
            paramArray[3] = financialYear;
            paramArray[4] = orderNo;
            paramArray[5] = branchId;

            // Session Store
            Session["StackParams"] = paramArray;

            // Open New Tab
            string script = "window.open('AllGodown_Wise_Inspection_Details_BO.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }

        else if (e.CommandName == "BranchClick")
        {
            LinkButton btn = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;
            string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
            string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
            string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;
            // 🔷 Array prepare
            string[] paramArray = new string[6];
            paramArray[0] = branchId;
            paramArray[1] = empId;
            paramArray[2] = inspectionQuarter;
            paramArray[3] = verificationType;
            paramArray[4] = financialYear;
            paramArray[5] = orderNo;
            // 🔷 Session store
            Session["GodownParams"] = paramArray;
            // 🔷 BranchId separate store (important)
            //Session["BranchId"] = branchId;
            // 🔷 New tab open
            string script = "window.open('Godown_Wise_Inspection_Details_BO.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
        }
    }
}