using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Branch_Rpt_Rabi_Pending_WHR_Details_2026 : System.Web.UI.Page
{
    // योग के लिए वेरिएबल्स
    int totalBags = 0;
    decimal totalQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
                fillgrid();
            else
                Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            // यहाँ अपनी SP का नाम सुनिश्चित कर लें (Rabi के लिए)
            using (SqlCommand cmd = new SqlCommand("Get_Rabi_Pending_WHR_Details_2026", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DistrictID", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                // बाइंडिंग से पहले टोटल रिसेट करें
                totalBags = 0;
                totalQty = 0;

                Depositor_Gridview.DataSource = dt;
                Depositor_Gridview.DataBind();
            }
        }
    }

    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // डेटा को जोड़ना
            totalBags += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recd_Bags"));
            totalQty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Recd_Qty"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // फुटर में टोटल दिखाना
            e.Row.Cells[8].Text = "GRAND TOTAL";
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;

            Label lblTotalBags = (Label)e.Row.FindControl("lblTotalBags");
            Label lblTotalQty = (Label)e.Row.FindControl("lblTotalQty");

            if (lblTotalBags != null) lblTotalBags.Text = totalBags.ToString();
            if (lblTotalQty != null) lblTotalQty.Text = totalQty.ToString("N3");
        }
    }

    protected void Depositor_Gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Depositor_Gridview.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}