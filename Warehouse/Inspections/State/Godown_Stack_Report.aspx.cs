using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Godown_Stack_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    int TotalPrevStack = 0;
    int TotalCurrentStack = 0;
    int TotalStack = 0;
    int TotalSentDM = 0;
    int TotalSubmitFCI = 0;
    int TotalFCIInspected = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReport();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("USP_Godown_Stack_Report", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // DATA ROW
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalPrevStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack"));
            TotalCurrentStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current Stack"));
            TotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Stack"));
            TotalSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Sent_DM"));
            TotalSubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Submit To FCI"));
            TotalFCIInspected += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));
        }

        // FOOTER
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "TOTAL";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Cells[3].Text = TotalPrevStack.ToString();
            e.Row.Cells[4].Text = TotalCurrentStack.ToString();
            e.Row.Cells[5].Text = TotalStack.ToString();
            e.Row.Cells[6].Text = TotalSentDM.ToString();
            e.Row.Cells[7].Text = TotalSubmitFCI.ToString();
            e.Row.Cells[8].Text = TotalFCIInspected.ToString();

            e.Row.Font.Bold = true;
        }
    }
}