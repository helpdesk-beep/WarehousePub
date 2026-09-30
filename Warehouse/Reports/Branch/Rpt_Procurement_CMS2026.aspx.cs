using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Reports_Branch_Rpt_Procurement_CMS2026 : System.Web.UI.Page
{
    decimal totalAQ = 0;
    decimal totalWHRQ = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Branch_Wise_Procurement_CSM2026", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (Session["BranchId"] != null)
                {
                    cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        totalAQ = 0; totalWHRQ = 0; // Reset Totals
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalAQ += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQty"));
            totalWHRQ += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AcceptQty"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "GRAND TOTAL";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

            Label lblTotalAQ = (Label)e.Row.FindControl("lblTotalAQ");
            Label lblTotalWHRQ = (Label)e.Row.FindControl("lblTotalWHRQ");
            Label lblAvgTotal = (Label)e.Row.FindControl("lblAvgTotal");

            if (lblTotalAQ != null) lblTotalAQ.Text = totalAQ.ToString("N2");
            if (lblTotalWHRQ != null) lblTotalWHRQ.Text = totalWHRQ.ToString("N2");

            if (totalAQ > 0)
                lblAvgTotal.Text = Math.Round((totalWHRQ * 100 / totalAQ), 2).ToString() + "%";
        }
    }
}