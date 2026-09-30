using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class StatePages_NCCF_Storage_Bill_Status_Branch_Wise : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Grand Total Variables
    int totalGen = 0;
    int subRM = 0;
    int penBM = 0;
    int subNCCF = 0;
    int penRM = 0;

    // Sub Total Variables (Branch Wise)
    int sub_totalGen = 0;
    int sub_subRM = 0;
    int sub_penBM = 0;
    int sub_subNCCF = 0;
    int sub_penRM = 0;

    // Tracker for grouping
    string prevBranchId = string.Empty;
    string prevBranchName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["DistId"] != null)
            {
                string districtId = Request.QueryString["DistId"].ToString();
                BindBranchWiseGrid(districtId);
            }
            else
            {
                Response.Redirect("NCCF_Storage_Bill_Status_Branch_And_RM_Fro_State.aspx");
            }
        }
    }

    private void BindBranchWiseGrid(string districtId)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            // Note: Ensure your Stored Procedure returns data ordered by Branch_Name/Branch_Id
            using (SqlCommand cmd = new SqlCommand("Branch_Wise_NCCF_Bill_Pendding", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictId", districtId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        litDistrictHeading.Text = dt.Rows[0]["District_Name"].ToString();
                        grdBranch.DataSource = dt;
                        grdBranch.DataBind();
                    }
                    else
                    {
                        grdBranch.DataSource = null;
                        grdBranch.DataBind();
                        litDistrictHeading.Text = "No Data Found";
                    }
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("NCCF_Storage_Bill_Status_Branch_And_RM_Fro_State.aspx");
    }

    protected void grdBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string currentBranchId = DataBinder.Eval(e.Row.DataItem, "Branch_Id").ToString();
            string currentBranchName = DataBinder.Eval(e.Row.DataItem, "Branch_Name").ToString();

            // Agar branch change ho rahi hai (aur ye pehli row nahi hai), toh pichli branch ka subtotal insert karein
            if (!string.IsNullOrEmpty(prevBranchId) && prevBranchId != currentBranchId)
            {
                AddSubTotalRow(e.Row.RowIndex);
            }

            // Values ko parse karein
            int pendingBM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_BM"));
            int pendingRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_RM"));
            int currentGen = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Bills_Generated_At_BM"));
            int currentSubRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_BM_To_RM"));
            int currentSubNCCF = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Submitted_By_RM_To_NCCF"));

            // Sub Total calculations
            sub_totalGen += currentGen;
            sub_subRM += currentSubRM;
            sub_penBM += pendingBM;
            sub_subNCCF += currentSubNCCF;
            sub_penRM += pendingRM;

            // Grand Total calculations
            totalGen += currentGen;
            subRM += currentSubRM;
            penBM += pendingBM;
            subNCCF += currentSubNCCF;
            penRM += pendingRM;

            // Trackers update karein
            prevBranchId = currentBranchId;
            prevBranchName = currentBranchName;

            // Style pending rows
            if (pendingBM > 0 || pendingRM > 0)
            {
                e.Row.CssClass = "pending-row";
                e.Row.Cells[4].Style["color"] = "#b70000 !important";
                e.Row.Cells[4].Style["font-weight"] = "700 !important";
                e.Row.Cells[6].Style["color"] = "#b70000 !important";
                e.Row.Cells[6].Style["font-weight"] = "700 !important";
            }
        }
    }

    private void AddSubTotalRow(int rowIndex)
    {
        // GridView mein dynamically subtotal row insert karne ke liye helper function
        Table tbl = (Table)grdBranch.Controls[0];
        GridViewRow subTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        subTotalRow.CssClass = "subtotal-row";

        // Subtotal row cells structuring
        TableCell cell1 = new TableCell();
        cell1.Text = "Sub Total (" + prevBranchName + ")";
        cell1.ColumnSpan = 4; // S.No + Branch Name + Commodity + Crop Year (Humne cells badal diye hain page ke hisab se)
        cell1.Style["text-align"] = "right !important";
        cell1.Style["font-weight"] = "700";
        subTotalRow.Cells.Add(cell1);

        subTotalRow.Cells.Add(new TableCell { Text = sub_totalGen.ToString(), HorizontalAlign = HorizontalAlign.Right });
        subTotalRow.Cells.Add(new TableCell { Text = sub_subRM.ToString(), HorizontalAlign = HorizontalAlign.Right });
        subTotalRow.Cells.Add(new TableCell { Text = sub_penBM.ToString(), HorizontalAlign = HorizontalAlign.Right });
        subTotalRow.Cells.Add(new TableCell { Text = sub_subNCCF.ToString(), HorizontalAlign = HorizontalAlign.Right });
        subTotalRow.Cells.Add(new TableCell { Text = sub_penRM.ToString(), HorizontalAlign = HorizontalAlign.Right });

        // Format and append cells
        for (int i = 1; i < subTotalRow.Cells.Count; i++)
        {
            subTotalRow.Cells[i].Style["font-weight"] = "700";
            subTotalRow.Cells[i].Style["text-align"] = "right";
        }

        // Row index par insert karein
        tbl.Controls.AddAt(tbl.Controls.Count - 1, subTotalRow);

        // Reset Sub Total indicators for the next branch
        sub_totalGen = 0;
        sub_subRM = 0;
        sub_penBM = 0;
        sub_subNCCF = 0;
        sub_penRM = 0;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Aakhri branch ka subtotal hamesha GridView ke end me lagane ke liye
        if (!string.IsNullOrEmpty(prevBranchId) && grdBranch.Rows.Count > 0)
        {
            AddSubTotalRow(grdBranch.Rows.Count);
        }

        // Render Grand Total in Footer
        if (grdBranch.Rows.Count > 0 && grdBranch.FooterRow != null)
        {
            GridViewRow footer = grdBranch.FooterRow;
            footer.CssClass = "grandtotal-row";

            footer.Cells[0].Text = "GRAND TOTAL";
            footer.Cells[0].ColumnSpan = 5; // structural columns layout count update
            footer.Cells[0].Style["text-align"] = "right !important";
            footer.Cells[0].Style["font-weight"] = "800";

            // Unwanted dynamic structural cell removals
            while (footer.Cells.Count > 6)
            {
                footer.Cells.RemoveAt(1);
            }

            footer.Cells[1].Text = totalGen.ToString();
            footer.Cells[2].Text = subRM.ToString();
            footer.Cells[3].Text = penBM.ToString();
            footer.Cells[4].Text = subNCCF.ToString();
            footer.Cells[5].Text = penRM.ToString();

            for (int i = 1; i <= 5; i++)
            {
                footer.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                footer.Cells[i].Style["text-align"] = "right !important";
            }
        }
    }

    //protected void lnkBranch_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;
    //    string selectedBranch = lnk.CommandArgument;
    //    Response.Redirect("NCCF_Storage_Bill_Status_Godown_Wise.aspx?BranchId=" + Server.UrlEncode(selectedBranch));
    //}
    protected void lnkBranch_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string commandArgs = btn.CommandArgument;

        // String ko split karke saari values nikalenge
        string[] args = commandArgs.Split('|');

        if (args.Length >= 4)
        {
            string branchId = args[0];
            string commodity = args[1];
            string cropYear = args[2];
            string finYear = args[3];

            // Agar District ID session ya query string mein upar se aa rahi hai toh use bhi le sakte hain
            // string districtId = Request.QueryString["DistrictId"] ?? ""; 

            // Agle page ka URL taiyar karein (Maan lijiye agla page NCCF_Storage_Bill_Details.aspx hai)
            string nextUrl = string.Format("NCCF_Storage_Bill_Status_Godown_Wise.aspx?BranchId={0}&Commodity={1}&CropYear={2}&FinYear={3}",
                                            Server.UrlEncode(branchId),
                                            Server.UrlEncode(commodity),
                                            Server.UrlEncode(cropYear),
                                            Server.UrlEncode(finYear));

            // Redirect karein
            Response.Redirect(nextUrl);
        }
    }
}