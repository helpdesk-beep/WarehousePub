using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_RO_Mobile_Inspection_Branch_Summary_Report_For_RO : System.Web.UI.Page
{
    // फुटर योग की गणना के लिए वेरिएबल डिक्लेरेशन
    private int grandTotalGodowns = 0;
    private int grandTotalStacks = 0;
    private long grandTotalBags = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindgrid();
        }
    }

    private void Bindgrid()
    {
        if (Session["GodownParams"] == null)
        {
            return;
        }

        string[] param = (string[])Session["GodownParams"];
        string Region_ID = param[0];
        string inspectionQuarter = param[1];
        string verificationType = param[2];
        string financialYear = param[3];

        // री-बाइंडिंग से पहले काउंटर्स को रीसेट करें
        grandTotalGodowns = 0;
        grandTotalStacks = 0;
        grandTotalBags = 0;

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Mobile_Inspection_Report_For_HO", con))
            {
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Region_ID", Region_ID);
                cmd.Parameters.AddWithValue("@Quater_Type", inspectionQuarter);
                cmd.Parameters.AddWithValue("@Verification_Type", verificationType);
                cmd.Parameters.AddWithValue("@Financial_Year", financialYear);

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

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // रनटाइम पर प्रत्येक पंक्ति का सम जोड़ने के लिए
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int godowns = 0, stacks = 0;
            long bags = 0;

            if (DataBinder.Eval(e.Row.DataItem, "Total_Godown") != DBNull.Value)
                int.TryParse(DataBinder.Eval(e.Row.DataItem, "Total_Godown").ToString(), out godowns);

            if (DataBinder.Eval(e.Row.DataItem, "Total_Stack") != DBNull.Value)
                int.TryParse(DataBinder.Eval(e.Row.DataItem, "Total_Stack").ToString(), out stacks);

            if (DataBinder.Eval(e.Row.DataItem, "Total_Bags") != DBNull.Value)
                long.TryParse(DataBinder.Eval(e.Row.DataItem, "Total_Bags").ToString(), out bags);

            grandTotalGodowns += godowns;
            grandTotalStacks += stacks;
            grandTotalBags += bags;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // फुटर लेबल्स में समरी वैल्यूज असाइन करना
            Label lblFooterGodowns = (Label)e.Row.FindControl("lblFooterGodowns");
            Label lblFooterStacks = (Label)e.Row.FindControl("lblFooterStacks");
            Label lblFooterBags = (Label)e.Row.FindControl("lblFooterBags");

            if (lblFooterGodowns != null) lblFooterGodowns.Text = grandTotalGodowns.ToString();
            if (lblFooterStacks != null) lblFooterStacks.Text = grandTotalStacks.ToString();
            if (lblFooterBags != null) lblFooterBags.Text = grandTotalBags.ToString();
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

    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BranchClick" || e.CommandName == "ViewAllGodownWiseStack")
        {
            // सुरक्षित तरीके से रो इंडेक्स प्राप्त करना
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvReport.Rows[rowIndex];

            // हिडन फ़ील्ड्स से मान निकालना
            string branchId = ((HiddenField)row.FindControl("hdnbranchid")).Value;
            string empId = ((HiddenField)row.FindControl("hdnemployeeid")).Value;
            string inspectionQuarter = ((HiddenField)row.FindControl("hdninsp_type_id")).Value;
            string verificationType = ((HiddenField)row.FindControl("hdnVerificationType")).Value;
            string financialYear = ((HiddenField)row.FindControl("hdnFinancial_Year")).Value;
            string orderNo = ((HiddenField)row.FindControl("hdnOrder_no")).Value;

            if (e.CommandName == "BranchClick")
            {
                string[] paramArray = new string[6];
                paramArray[0] = branchId;
                paramArray[1] = empId;
                paramArray[2] = inspectionQuarter;
                paramArray[3] = verificationType;
                paramArray[4] = financialYear;
                paramArray[5] = orderNo;

                // सेशन अपडेट करना
                Session["GodownParams"] = paramArray;

                // नया टैब खोलना
                string script = "window.open('Godown_Wise_Inspection_Details_RO.aspx','_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
            }
            else if (e.CommandName == "ViewAllGodownWiseStack")
            {
                string[] paramArray = new string[6];
                paramArray[0] = empId;
                paramArray[1] = inspectionQuarter;
                paramArray[2] = verificationType;
                paramArray[3] = financialYear;
                paramArray[4] = orderNo;
                paramArray[5] = branchId;

                // सेशन अपडेट करना 
                Session["StackParams"] = paramArray;

                // नया टैब खोलना
                string script = "window.open('AllGodown_Wise_Inspection_Details_RO.aspx','_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenWindow", script, true);
            }
        }
    }
}