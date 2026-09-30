using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_NCCF_Storage_Bill_Status_Branch_And_RM_Fro_State : System.Web.UI.Page
{
    string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    int serialNumber = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDistrictWiseGrid();
        }
    }

    private void BindDistrictWiseGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_NCCF_Bill_Detail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    grpendding.DataSource = dt;
                    grpendding.DataBind();
                }
            }
        }
    }

    protected void grpendding_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string regionName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Regionnm"));
            string districtName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Name"));
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");

            // 1. GRAND TOTAL ROW ALIGNMENT FIX
            if (regionName == "Grand Total")
            {
                e.Row.CssClass = "grandtotal-row";
                e.Row.Cells[0].Text = "GRAND TOTAL";
                e.Row.Cells[0].ColumnSpan = 3; // S.No, Region, District columns ko merge kiya
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right; // Right alignment applied
                e.Row.Cells[0].Style["padding-right"] = "15px";

                e.Row.Cells.RemoveAt(2); 
                e.Row.Cells.RemoveAt(1); 
            }
            // 2. REGION SUBTOTAL ROW ALIGNMENT FIX
            else if (districtName == "Subtotal")
            {
                e.Row.CssClass = "subtotal-row";
                e.Row.Cells[0].Text = regionName.ToUpper() + " TOTAL";
                e.Row.Cells[0].ColumnSpan = 3; // S.No, Region, District columns ko merge kiya
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right; // Right alignment applied
                e.Row.Cells[0].Style["padding-right"] = "15px";

                e.Row.Cells.RemoveAt(2); 
                e.Row.Cells.RemoveAt(1); 
            }
            // 3. NORMAL DATA ROW
            else
            {
                if (lblSNo != null)
                {
                    lblSNo.Text = serialNumber.ToString();
                    serialNumber++;
                }

                int pendingBM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_BM"));
                int pendingRM = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Bills_Pending_At_RM"));

                // Agar normal row me pending bills hain toh poori row soft red highlight hogi
                if (pendingBM > 0 || pendingRM > 0)
                {
                    e.Row.CssClass = "pending-row";

                    // Index 5 = Bills Pending at BM text styling
                    e.Row.Cells[5].Style["color"] = "#b70000";
                    e.Row.Cells[5].Style["font-weight"] = "700";

                    // Index 7 = Bills Pending at RM text styling
                    e.Row.Cells[7].Style["color"] = "#b70000";
                    e.Row.Cells[7].Style["font-weight"] = "700";
                }
            }
        }
    }

    protected void lnkDistrict_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        string selectedDistrictId = lnk.CommandArgument;

        Response.Redirect("NCCF_Storage_Bill_Status_Branch_Wise.aspx?DistId=" + Server.UrlEncode(selectedDistrictId));
    }
}