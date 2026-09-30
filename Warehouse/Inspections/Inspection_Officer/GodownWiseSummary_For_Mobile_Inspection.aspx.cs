using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_Inspection_Officer_GodownWiseSummary_For_Mobile_Inspection : System.Web.UI.Page
{
    int totalOnlineStack = 0;
    int totalOnlineBags = 0;
    int totalInspStack = 0;
    int totalPV = 0;
    int totalSpillage = 0;
    int totalDiff = 0; // Proper Bag level difference store karne ke liye

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
                lblCount.Text = "Session Expired";
                return;
            }

            string[] param = (string[])Session["GodownParams"];

            string empId = param[0];
            string inspectionQuarter = param[1];
            string verificationType = param[2];
            string financialYear = param[3];
            string orderNo = param[4];
            string branchId = Session["BranchId"].ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_BranchWise_PV_Summary", con)) 
                {
                    cmd.CommandTimeout = 1600;
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

                    lblCount.Text = dt.Rows.Count.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblCount.Text = "Error";
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindData(); // Fresh data binding

        string fileName = "GodownSummary_" + DateTime.Now.ToString("dd-MMM-yyyy_HH:mm:ss") + ".xls";

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // ⚡ USER CRITICAL: Excel export se pehle "Without Image" column ko hide karein
                // Aapke GridView mein kul 11 columns hain (Index 0 se 10). "Without Image" sabse aakhiri (Index 10) par hai.
                int lastColumnIndex = gvGodown.Columns.Count - 1;
                if (lastColumnIndex >= 0)
                {
                    gvGodown.Columns[lastColumnIndex].Visible = false;
                }

                // Paging disabled aur re-bind taaki hidden settings apply ho sakein
                gvGodown.AllowPaging = false;
                gvGodown.DataBind();

                // Excel Top Blue Header Layout (Ab iska colspan 9 ho jayega kyunki 1 column kam ho gaya)
                hw.Write("<table style='width:100%; border-collapse:collapse;'>");
                hw.Write("<tr><td colspan='11' style='text-align:center; font-size:18px; font-weight:bold; background-color:#0984e3; color:white; padding:10px;'>INSPECTION OFFICER PV REPORT</td></tr>");
                hw.Write("<tr><td colspan='11' style='text-align:center; font-size:11px; padding:4px;'>Generated On: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "</td></tr>");
                hw.Write("<tr><td colspan='11'>&nbsp;</td></tr>");
                hw.Write("</table>");

                // Header Formatting
                if (gvGodown.HeaderRow != null)
                {
                    gvGodown.HeaderRow.Style.Add("background-color", "#f8f9fa");
                    gvGodown.HeaderRow.Style.Add("font-weight", "bold");
                    foreach (TableCell cell in gvGodown.HeaderRow.Cells)
                    {
                        cell.Attributes.Add("style", "text-align:center; background-color:#dfe6e9; font-weight:bold; padding:8px;");
                    }
                }

                // Data Rows Handling (Borders aur control cleanings)
                foreach (GridViewRow row in gvGodown.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            cell.Attributes.Add("style", "text-align:center; padding:6px;");

                            // LinkButtons ko direct text literal se replace karein (Jaise Godown Name link)
                            if (cell.Controls.Count > 0)
                            {
                                for (int i = cell.Controls.Count - 1; i >= 0; i--)
                                {
                                    if (cell.Controls[i] is LinkButton)
                                    {
                                        LinkButton btn = (LinkButton)cell.Controls[i];
                                        Literal literal = new Literal();
                                        literal.Text = btn.Text;
                                        cell.Controls.RemoveAt(i);
                                        cell.Controls.AddAt(i, literal);
                                    }
                                }
                            }
                        }
                    }
                }

                // Footer Row Formatting
                if (gvGodown.FooterRow != null)
                {
                    foreach (TableCell cell in gvGodown.FooterRow.Cells)
                    {
                        cell.Attributes.Add("style", "text-align:center; font-weight:bold; background-color:#b2bec3; padding:8px;");
                    }
                }

                // Render output to sheet stream
                gvGodown.RenderControl(hw);

                Response.Output.Write(sw.ToString());
                Response.Flush();

                // ⚡ IMPORTANT: Export hone ke baad web page par column wapas dikhane ke liye visible true karein
                if (lastColumnIndex >= 0)
                {
                    gvGodown.Columns[lastColumnIndex].Visible = true;
                }

                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Required validation for GridView controls export markup */
    }

    protected void gvGodown_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int onlineStack = 0, onlineBags = 0, inspStack = 0, pvBags = 0, spillageBags = 0;

            // Safe C# 5 Object Fetching
            object objOnlineStack = DataBinder.Eval(e.Row.DataItem, "Online_Stack");
            object objOnlineBags = DataBinder.Eval(e.Row.DataItem, "OnlineBags");
            object objInspStack = DataBinder.Eval(e.Row.DataItem, "Insp_Stack");
            object objPV = DataBinder.Eval(e.Row.DataItem, "TotalBags_AsPerPV");
            object objSpillage = DataBinder.Eval(e.Row.DataItem, "SpillageBags_AsPerPV");

            if (objOnlineStack != null && objOnlineStack != DBNull.Value) int.TryParse(objOnlineStack.ToString(), out onlineStack);
            if (objOnlineBags != null && objOnlineBags != DBNull.Value) int.TryParse(objOnlineBags.ToString(), out onlineBags);
            if (objInspStack != null && objInspStack != DBNull.Value) int.TryParse(objInspStack.ToString(), out inspStack);
            if (objPV != null && objPV != DBNull.Value) int.TryParse(objPV.ToString(), out pvBags);
            if (objSpillage != null && objSpillage != DBNull.Value) int.TryParse(objSpillage.ToString(), out spillageBags);

            totalOnlineStack += onlineStack;
            totalOnlineBags += onlineBags;
            totalInspStack += inspStack;
            totalPV += pvBags;
            totalSpillage += spillageBags;

            // ⚡ USER CRITICAL MATCH LOGIC: Bags (PV) - Online Bags
            int currentDiff = pvBags - onlineBags;
            totalDiff += currentDiff;

            // Dynamic Styling for Grid Rows (Live Screen Structure)
            Label lblRowDiff = (Label)e.Row.FindControl("lblRowDiff");
            if (lblRowDiff != null)
            {
                if (currentDiff > 0)
                {
                    lblRowDiff.Text = "+" + currentDiff.ToString();
                    lblRowDiff.CssClass = "fw-bold text-primary";
                }
                else if (currentDiff < 0)
                {
                    lblRowDiff.Text = currentDiff.ToString(); // Automatic '-' sign is carried by integer conversion
                    lblRowDiff.CssClass = "fw-bold text-danger";
                }
                else
                {
                    lblRowDiff.Text = "0";
                    lblRowDiff.CssClass = "fw-bold text-success";
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Set running summaries values
            Label lblTotalOnlineStack = (Label)e.Row.FindControl("lblTotalOnlineStack");
            Label lblTotalOnlineBags = (Label)e.Row.FindControl("lblTotalOnlineBags");
            Label lblTotalInspStack = (Label)e.Row.FindControl("lblTotalInspStack");
            Label lblTotalPV = (Label)e.Row.FindControl("lblTotalPV");
            Label lblTotalSpillage = (Label)e.Row.FindControl("lblTotalSpillage");
            Label lblFooterDiff = (Label)e.Row.FindControl("lblTotalDiff");

            if (lblTotalOnlineStack != null) lblTotalOnlineStack.Text = totalOnlineStack.ToString();
            if (lblTotalOnlineBags != null) lblTotalOnlineBags.Text = totalOnlineBags.ToString();
            if (lblTotalInspStack != null) lblTotalInspStack.Text = totalInspStack.ToString();
            if (lblTotalPV != null) lblTotalPV.Text = totalPV.ToString();
            if (lblTotalSpillage != null) lblTotalSpillage.Text = totalSpillage.ToString();

            // ⚡ Grand Total Sign Check (+ / - / 0)
            if (lblFooterDiff != null)
            {
                if (totalDiff > 0)
                {
                    lblFooterDiff.Text = "+" + totalDiff.ToString();
                    lblFooterDiff.ForeColor = System.Drawing.Color.Blue;
                }
                else if (totalDiff < 0)
                {
                    lblFooterDiff.Text = totalDiff.ToString();
                    lblFooterDiff.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblFooterDiff.Text = "0";
                    lblFooterDiff.ForeColor = System.Drawing.Color.Green;
                }
            }

            e.Row.BackColor = System.Drawing.Color.FromName("#f1f2f6");
            e.Row.Font.Bold = true;
        }
    }

    protected void gvGodown_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewStack" || e.CommandName == "ViewGodownWiseStack")
        {
            string[] data = e.CommandArgument.ToString().Split('|');
            string GodownID = data[0];
            string BranchID = data[1];

            string[] parent = (string[])Session["GodownParams"];
            string[] paramArray = new string[7];

            paramArray[0] = parent[0]; // EmpId
            paramArray[1] = parent[1]; // Quarter
            paramArray[2] = parent[2]; // Verification
            paramArray[3] = parent[3]; // Financial Year
            paramArray[4] = parent[4]; // Order No
            paramArray[5] = BranchID;
            paramArray[6] = GodownID;

            Session["StackParams"] = paramArray;

            string pageName = (e.CommandName == "ViewStack") ? "StackwiseDetails_For_Mobile_Inspection.aspx" : "StackwiseDetails_For_Mobile_Inspection_For_Inspection_Officer.aspx";
            string script = string.Format("window.open('{0}','_blank');", pageName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenTab", script, true);
        }
    }
}