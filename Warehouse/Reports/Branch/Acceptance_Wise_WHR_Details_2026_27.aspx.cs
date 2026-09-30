using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_Acceptance_Wise_WHR_Details_2026_27 : System.Web.UI.Page
{
    // योग के लिए वेरिएबल्स
    int totalAcceptBags = 0;
    int totalWHRBags = 0;
    decimal totalAcceptQty = 0;
    decimal totalWHRQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Acceptance_Wise_WHR_Details_2026_27", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (Session["BranchId"] != null)
                {
                    cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // टोटल वेरिएबल्स रिसेट करें (पेजिंग/रिफ्रेश के लिए)
                    totalAcceptBags = 0; totalWHRBags = 0;
                    totalAcceptQty = 0; totalWHRQty = 0;

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // हर रो का डेटा जोड़ना (डेटाबेस कॉलम के नाम चेक कर लें)
            totalAcceptBags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "No_of_Bags"));
            totalWHRBags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalBags_Received"));
            totalAcceptQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Rec_Qty"));
            totalWHRQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Qty_Received"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // फुटर में योग दिखाना
            e.Row.Cells[4].Text = "TOTAL:";
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].Font.Bold = true;

            ((Label)e.Row.FindControl("lblTotalAB")).Text = totalAcceptBags.ToString();
            ((Label)e.Row.FindControl("lblTotalWB")).Text = totalWHRBags.ToString();
            ((Label)e.Row.FindControl("lblTotalAQ")).Text = totalAcceptQty.ToString("N3");
            ((Label)e.Row.FindControl("lblTotalWQ")).Text = totalWHRQty.ToString("N3");

            e.Row.Font.Bold = true;
        }
    }
}