using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
public partial class JointVentureScheme_District_Wise_Offerd_Capacity_details_2026_27 : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || string.IsNullOrEmpty(Session["UserId"].ToString()))
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            // Agar Session mil jata hai, to hi baaki ka logic chalega
            if (!IsPostBack)
            {
                Fillgrid(); // Aapka purana function
            }
        }
    }
    private void Fillgrid()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Offer_Godown_Details_2026_27", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_ID", Session["UserId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);

            gvBranchReport.DataSource = dt;
            gvBranchReport.DataBind();

            // Summary Calculations
            if (dt.Rows.Count > 0)
            {
                litTotalBranches.Text = dt.Rows.Count.ToString();
                litTotalGodowns.Text = dt.Compute("SUM(Total_Godowns)", "").ToString();
                litTotalCapacity.Text = Convert.ToDouble(dt.Compute("SUM(Total_Offered_Capacity)", "")).ToString("N2");
            }
            else
            {
                litTotalBranches.Text = "0";
                litTotalGodowns.Text = "0";
                litTotalCapacity.Text = "0.00";
            }
        }
    }
    protected void gvBranchReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetails")
        {
            // CommandArgument se Branch ID nikalna
            int branchId = Convert.ToInt32(e.CommandArgument);

            // Godowns load karein ID ke base par
            LoadGodownDetails(branchId);

            // Modal open script
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    private void LoadGodownDetails(int branchId)
    {
        string connStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            // Procedure ka naam pass karein
            using (SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Offer_Capacity_Godown_Detail_2026_27", con))
            {
                // CommandType ko StoredProcedure set karein
                cmd.CommandType = CommandType.StoredProcedure;

                // Parameter add karein
                cmd.Parameters.AddWithValue("@BranchID", branchId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    // GridView bind karein
                    gvGodownDetails.DataSource = dt;
                    gvGodownDetails.DataBind();
                }
                catch (Exception ex)
                {
                    // Error handling (aap chahen to label par show kar sakte hain)
                    // lblError.Text = "Data load karne mein samasya aayi: " + ex.Message;
                }
            }
        }
    }
}