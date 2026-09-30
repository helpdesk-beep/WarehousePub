using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_NCCF_Storage_Bill_Status_Godown_Wise : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Core summary counters for footer
    int totalGen = 0;
    int subRM = 0;
    int penBM = 0;
    int subNCCF = 0;
    int penRM = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Pichle page ka URL query string ke sath ViewState me safe kiya
            if (Request.UrlReferrer != null)
            {
                ViewState["BackUrl"] = Request.UrlReferrer.ToString();
            }

            // CRITICAL CHECK: Saari mandatory fields query string se fetch kar rahe hain
            if (Request.QueryString["BranchId"] != null &&
                Request.QueryString["Commodity"] != null &&
                Request.QueryString["CropYear"] != null &&
                Request.QueryString["FinYear"] != null)
            {
                string branchId = Request.QueryString["BranchId"].ToString();
                string commodity = Request.QueryString["Commodity"].ToString();
                string cropYear = Request.QueryString["CropYear"].ToString();
                string finYear = Request.QueryString["FinYear"].ToString();

                // Optional parameter agar aap use kar rahe hain
                string districtId = Request.QueryString["DistrictId"] != null ? Request.QueryString["DistrictId"].ToString() : "";

                // Grid ko saari filtered values pass karein
                BindGodownWiseGrid(branchId, commodity, cropYear, finYear, districtId);
            }
            else
            {
                // Fallback agar koi direct page pe aane ki koshish kare
                Response.Redirect("NCCF_Storage_Bill_Status_Branch_Wise.aspx");
            }
        }
    }

    private void BindGodownWiseGrid(string branchId, string commodity, string cropYear, string finYear, string districtId)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("Godown_Wise_NCCF_Bill_Pendding_For_Submission", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Saare naye parameters ko Stored Procedure me add kiya
                cmd.Parameters.AddWithValue("@BranchId", branchId);
                cmd.Parameters.AddWithValue("@Commodity_ID", commodity); // SP ke exact parameter name se match karein
                cmd.Parameters.AddWithValue("@Crop_Year", cropYear);       // SP ke exact parameter name se match karein
                cmd.Parameters.AddWithValue("@Financial_Year", finYear);   // SP ke exact parameter name se match karein

                if (!string.IsNullOrEmpty(districtId))
                {
                    cmd.Parameters.AddWithValue("@District_Id", districtId);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Stored procedure me agar Heading data hai
                        if (dt.Columns.Contains("Branch_Name"))
                            litDistrictHeading.Text = dt.Rows[0]["Branch_Name"].ToString();
                        else if (dt.Columns.Contains("DepotName"))
                            litDistrictHeading.Text = dt.Rows[0]["DepotName"].ToString();
                        else
                            litDistrictHeading.Text = "Selected Branch";

                        // Heading me criteria dikhane ke liye aap ise badha sakte hain:
                        litDistrictHeading.Text += string.Format(" ({0} | FY: {1})", commodity, finYear);

                        grdGodown.DataSource = dt;
                        grdGodown.DataBind();
                    }
                    else
                    {
                        grdGodown.DataSource = null;
                        grdGodown.DataBind();
                        litDistrictHeading.Text = "No Data Found for Selected Filter";
                    }
                }
            }
        }
    }

    protected void grdGodown_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // 1. DATA ROWS PROCESSING
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int pendingBM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_BM"));
            int pendingRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_RM"));

            // Matrix values calculations
            totalGen += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Bills_Generated_At_BM"));
            subRM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_BM_To_RM"));
            penBM += pendingBM;
            subNCCF += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_RM_To_NCCF"));
            penRM += pendingRM;

            // Strict checking logic for coloring rows if bills are pending
            if (pendingBM > 0 || pendingRM > 0)
            {
                e.Row.CssClass = "pending-row";

                // NOTE: GridView ke columns ke actual layout ke hisab se indices (4 aur 6) cross-check kar lein
                if (e.Row.Cells.Count > 6)
                {
                    e.Row.Cells[4].Style["color"] = "#b70000 !important";
                    e.Row.Cells[4].Style["font-weight"] = "700 !important";

                    e.Row.Cells[6].Style["color"] = "#b70000 !important";
                    e.Row.Cells[6].Style["font-weight"] = "700 !important";
                }
            }
        }

        // 2. FOOTER ROW PROCESSING
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "grandtotal-row";

            e.Row.Cells[0].Text = "GRAND TOTAL";
            e.Row.Cells[0].ColumnSpan = 5;
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[0].Style["text-align"] = "right !important";
            e.Row.Cells[0].Style["padding-right"] = "15px";

            // Totals values placement
            e.Row.Cells[2].Text = totalGen.ToString();
            e.Row.Cells[3].Text = subRM.ToString();
            e.Row.Cells[4].Text = penBM.ToString();
            e.Row.Cells[5].Text = subNCCF.ToString();
            e.Row.Cells[6].Text = penRM.ToString();

            for (int i = 2; i <= 6; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[i].Style["text-align"] = "right !important";
            }

            // Cell removal logic as per your grid structure
            if (e.Row.Cells.Count > 1)
            {
                e.Row.Cells.RemoveAt(1);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (ViewState["BackUrl"] != null)
        {
            Response.Redirect(ViewState["BackUrl"].ToString());
        }
        else
        {
            Response.Redirect("NCCF_Storage_Bill_Status_Branch_Wise.aspx");
        }
    }
}