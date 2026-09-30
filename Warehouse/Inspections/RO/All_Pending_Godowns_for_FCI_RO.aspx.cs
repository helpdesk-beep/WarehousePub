using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_All_Pending_Godowns_for_FCI_RO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string activeRegId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString().Trim() : "0";
            string activeDistId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString().Trim() : "0";
            string activeBranchId = Request.QueryString["BranchId"] != null ? Request.QueryString["BranchId"].ToString().Trim() : "0";

            BindPendingReport(activeRegId, activeDistId, activeBranchId);
        }
    }

    private DataTable FetchPendingDataset(string regId, string distId, string branchId)
    {
        DataTable dt = new DataTable();
        try
        {
            using (SqlCommand cmd = new SqlCommand("sp_Get_Pending_Godowns_FCI_Filters", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                int pReg = 0, pDist = 0, pBranch = 0;
                int.TryParse(regId, out pReg);
                int.TryParse(distId, out pDist);
                int.TryParse(branchId, out pBranch);

                cmd.Parameters.AddWithValue("@Region_Id", pReg);
                cmd.Parameters.AddWithValue("@District_Id", pDist);
                cmd.Parameters.AddWithValue("@Branch_Id", pBranch);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Execution Engine Fault: " + ex.Message.Replace("'", "\\'") + "');</script>");
        }
        return dt;
    }

    private void BindPendingReport(string regId, string distId, string branchId)
    {
        DataTable dt = FetchPendingDataset(regId, distId, branchId);
        gvDetails.DataSource = dt;
        gvDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            gvDetails.UseAccessibleHeader = true;
            gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            string contextText = "State Framework Filter Summary";
            if (dt.Columns.Contains("Region Name") && dt.Rows[0]["Region Name"] != DBNull.Value)
            {
                if (branchId != "0" && dt.Columns.Contains("Branch Name")) contextText = "Branch View: " + dt.Rows[0]["Branch Name"].ToString();
                else if (distId != "0") contextText = "District View: " + dt.Rows[0]["District Name"].ToString();
                else if (regId != "0") contextText = "Region Scope: " + dt.Rows[0]["Region Name"].ToString();
            }

            lblScopeHeader.Text = contextText;
            lblPrintRegion.Text = contextText;
        }
        else
        {
            lblScopeHeader.Text = "No Exceptions Found";
            lblPrintRegion.Text = "No Exceptions Found";
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string activeRegId = Request.QueryString["RegID"] != null ? Request.QueryString["RegID"].ToString().Trim() : "0";
        string activeDistId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString().Trim() : "0";
        string activeBranchId = Request.QueryString["BranchId"] != null ? Request.QueryString["BranchId"].ToString().Trim() : "0";

        DataTable dt = FetchPendingDataset(activeRegId, activeDistId, activeBranchId);
        if (dt == null || dt.Rows.Count == 0) return;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=FCI_Inspection_Pending_Godowns_Report.xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringBuilder sb = new StringBuilder();
        sb.Append("<style>");
        sb.Append("th { background-color:#1e3a8a !important; color:#ffffff !important; font-weight:bold !important; text-align:center !important; border:1px solid #172554; text-transform:uppercase; font-size:10px; }");
        sb.Append("td { border:1px solid #cbd5e1; font-size:11px; font-family: 'Segoe UI', Arial; } .text-center { text-align:center !important; }");
        sb.Append("</style>");

        sb.Append("<table cellspacing='0' cellpadding='4' border='1'>");
        sb.Append("<tr><th colspan='6' style='font-size:16pt; background-color:#1e3a8a; color:#ffffff;'>MADHYA PRADESH WAREHOUSING AND LOGISTICS CORPORATION</th></tr>");
        sb.AppendFormat("<tr><th colspan='6' style='font-size:12pt; background-color:#f1f5f9; color:#1e3a8a;'>FCI Inspection Pending Godowns Exception Sheet - Scope: {0}</th></tr>", lblScopeHeader.Text);
        sb.AppendFormat("<tr><td colspan='6' style='text-align:right; font-weight:bold;'>Generated On: {0}</td></tr>", DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"));
        sb.Append("<tr><td colspan='6' style='border:none;'>&nbsp;</td></tr>");

        // केवल आपके द्वारा माँगे गए 5 हेडर्स (और एक S.No.)
        sb.Append("<tr><th>S.No.</th><th>Region Name</th><th>District Name</th><th>Depot Name</th><th>Godown Name</th><th>Is Godown Inspected By FCI</th></tr>");

        int sNo = 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow r = dt.Rows[i];
            sb.AppendFormat("<tr><td class='text-center'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td style='font-weight:bold;'>{4}</td><td class='text-center'>{5}</td></tr>",
                            sNo++, r["Region Name"], r["District Name"], r["DepotName"], r["Godown_Name"], r["Is_Godown_Inspected_By_FCI"]);
        }

        sb.Append("</table>");

        Response.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}