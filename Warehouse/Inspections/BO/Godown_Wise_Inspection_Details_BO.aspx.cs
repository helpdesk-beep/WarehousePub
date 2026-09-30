using System;
using System.Data;
using System.Data.SqlClient; 
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Inspections_BO_Godown_Wise_Inspection_Details_BO : System.Web.UI.Page
{
    int totalOnlineStack = 0;
    int totalOnlineBags = 0;
    int totalInspStack = 0;
    int totalPV = 0;
    int totalSpillage = 0;
    int totalDiff = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    private void BindData()
    {
        try
        {
            if (Session["GodownParams"] == null)
            {
                //lblCount.Text = "Session Expired";
                return;
            }

            string[] param = (string[])Session["GodownParams"];

            string branchId = param[0];
            string empId = param[1];
            string inspectionQuarter = param[2];
            string verificationType = param[3];
            string financialYear = param[4];
            string orderNo = param[5];
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_BranchWise_PV_Summary", con))
                {
                    cmd.CommandTimeout = 300;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emp_ID", empId);
                    cmd.Parameters.AddWithValue("@Quater_Type", inspectionQuarter);
                    cmd.Parameters.AddWithValue("@Verification_Type", verificationType);
                    cmd.Parameters.AddWithValue("@Financial_Year", financialYear);
                    cmd.Parameters.AddWithValue("@Order_No", orderNo);
                    cmd.Parameters.AddWithValue("@Branch_ID", branchId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvGodown.DataSource = dt;
                    gvGodown.DataBind();
                    //lblTotalOnlineStackCard.Text = lblTotalOnlineStackCard.ToString();
                    //lblTotalInspStackCard.Text = lblTotalInspStackCard.ToString();
                    //lblTotalPVBagsCard.Text = lblTotalPVBagsCard.ToString();
                    //lblTotalSpillageCard.Text = lblTotalSpillageCard.ToString();
                    //lblCount.Text = dt.Rows.Count.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            // lblCount.Text = "Error";
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        gvGodown.AllowPaging = false;
        BindData();
        string fileName = "GodownSummary_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        hw.Write("<table style='width:100%; border-collapse:collapse;'>");
        hw.Write("<tr><td colspan='10' style='text-align:center; font-size:20px; font-weight:bold;'>Godown Wise PV Report</td></tr>");
        hw.Write("<tr><td colspan='10' style='text-align:center; font-size:12px;'>Generated On: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "</td></tr>");
        hw.Write("<tr><td colspan='10'>&nbsp;</td></tr>");
        hw.Write("</table>");
        gvGodown.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Export
    }

    protected void gvGodown_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Safe conversion handling DBNull values
            int onlineStack = e.Row.DataItem != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Online_Stack") != DBNull.Value
                ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Online_Stack")) : 0;

            int onlineBags = e.Row.DataItem != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "OnlineBags") != DBNull.Value
                ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OnlineBags")) : 0;

            int inspStack = e.Row.DataItem != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "Insp_Stack") != DBNull.Value
                ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Insp_Stack")) : 0;

            int pv = e.Row.DataItem != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "TotalBags_AsPerPV") != DBNull.Value
                ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalBags_AsPerPV")) : 0;

            int spillage = e.Row.DataItem != DBNull.Value && DataBinder.Eval(e.Row.DataItem, "SpillageBags_AsPerPV") != DBNull.Value
                ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SpillageBags_AsPerPV")) : 0;

            // Current Row Difference = Bags (PV) - Online Bags
            int currentDiff = pv - onlineBags;

            // Running Totals Accumulation
            totalOnlineStack += onlineStack;
            totalOnlineBags += onlineBags;
            totalInspStack += inspStack;
            totalPV += pv;
            totalSpillage += spillage;

            // Sum up row-wise differences correctly
            totalDiff += currentDiff;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTotalOnlineStack")).Text = totalOnlineStack.ToString("#,##0");
            ((Label)e.Row.FindControl("lblTotalOnlineBags")).Text = totalOnlineBags.ToString("#,##0");
            ((Label)e.Row.FindControl("lblTotalInspStack")).Text = totalInspStack.ToString("#,##0");
            ((Label)e.Row.FindControl("lblTotalPV")).Text = totalPV.ToString("#,##0");
            ((Label)e.Row.FindControl("lblTotalSpillage")).Text = totalSpillage.ToString("#,##0");

            Label lblFooterDiff = (Label)e.Row.FindControl("lblTotalDiff");

            // Grand Total Difference = Total Bags (PV) - Total Online Bags
            int grandTotalDiff = totalPV - totalOnlineBags;

            if (grandTotalDiff > 0)
            {
                lblFooterDiff.Text = "+" + grandTotalDiff.ToString("#,##0");
                lblFooterDiff.ForeColor = System.Drawing.Color.Blue;
            }
            else if (grandTotalDiff < 0)
            {
                lblFooterDiff.Text = grandTotalDiff.ToString("#,##0"); // Negative sign automatically built-in
                lblFooterDiff.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblFooterDiff.Text = "0";
                lblFooterDiff.ForeColor = System.Drawing.Color.Green;
            }

            e.Row.BackColor = System.Drawing.Color.FromName("#e9ecef");
            e.Row.Font.Bold = true;
        }
    }
    protected void gvReport_PreRender(object sender, EventArgs e)
    {
        if (gvGodown.Rows.Count > 0)
        {
            gvGodown.UseAccessibleHeader = true;
            gvGodown.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void gvGodown_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewStack")
        {
            string[] data = e.CommandArgument.ToString().Split('|');

            string GodownID = data[0];
            // string BranchID = data[1];

            string[] parent = (string[])Session["GodownParams"];

            string[] paramArray = new string[7];

            // ✅ SAME ORDER maintain karo
            paramArray[0] = parent[0]; // BranchId
            paramArray[1] = parent[1]; // EmpId
            paramArray[2] = parent[2]; // Quarter
            paramArray[3] = parent[3]; // VerificationType
            paramArray[4] = parent[4]; // FinancialYear
            paramArray[5] = parent[5]; // OrderNo
            paramArray[6] = GodownID;  // Extra (Godown)

            Session["StackParams"] = paramArray;

            string script = "window.open('StackwiseDetails_For_Mobile_Inspection_For_BO.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenTab", script, true);
        }
        else if (e.CommandName == "ViewGodownWiseStack")
        {
            string[] data = e.CommandArgument.ToString().Split('|');

            string GodownID = data[0];
            // string BranchID = data[1];

            string[] parent = (string[])Session["GodownParams"];

            string[] paramArray = new string[7];

            // ✅ SAME ORDER maintain karo
            paramArray[0] = parent[0]; // BranchId
            paramArray[1] = parent[1]; // EmpId
            paramArray[2] = parent[2]; // Quarter
            paramArray[3] = parent[3]; // VerificationType
            paramArray[4] = parent[4]; // FinancialYear
            paramArray[5] = parent[5]; // OrderNo
            paramArray[6] = GodownID;  // Extra (Godown)

            Session["StackParams"] = paramArray;

            string script = "window.open('StackwiseDetails_For_Mobile_Inspection_For_BO_New.aspx','_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenTab", script, true);
        }
        
    }
}