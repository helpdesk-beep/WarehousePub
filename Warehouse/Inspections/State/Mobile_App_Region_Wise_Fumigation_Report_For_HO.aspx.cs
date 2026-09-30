using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Mobile_App_Region_Wise_Fumigation_Report_For_HO
    : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    DataTable dt = new DataTable();
    private int TotalGodowns = 0;
    private int TotalStacks = 0;
    private int TotalCompleted = 0;
    private int TotalOpened = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {
            BindReport();
        }
    }

    private void BindReport()
    {
        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_Region_Wise_Fumigation_Report_For_HO",
            con);

        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da =
            new SqlDataAdapter(cmd);

        dt.Clear();

        da.Fill(dt);

        gvReport.DataSource = dt;

        gvReport.DataBind();

        ViewState["dtReport"] = dt;
    }

    protected void gvReport_RowDataBound(
    object sender,
    GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalGodowns += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem,
                "Total_Godowns_Covered"));

            TotalStacks += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem,
                "Total_Stacks_Fumigated"));

            TotalCompleted += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem,
                "Completed_Fumigations"));

            TotalOpened += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem,
                "Total_Opened_Stacks"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "totalRow";

            e.Row.Cells[1].Text = "TOTAL";
            e.Row.Cells[2].Text = "";

            e.Row.Cells[3].Text =
                TotalGodowns.ToString();

            e.Row.Cells[4].Text =
                TotalStacks.ToString();

            e.Row.Cells[5].Text =
                TotalCompleted.ToString();

            e.Row.Cells[6].Text =
                TotalOpened.ToString();
        }
    }

    protected void btnExcel_Click(
        object sender,
        EventArgs e)
    {
        BindReport();

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=Region_Wise_Fumigation_Report.xls");

        Response.Charset = "";

        Response.ContentType =
            "application/vnd.ms-excel";

        StringWriter sw =
            new StringWriter();

        HtmlTextWriter hw =
            new HtmlTextWriter(sw);

        gvReport.GridLines =
            GridLines.Both;

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());

        Response.End();
    }

    public override void VerifyRenderingInServerForm(
        Control control)
    {
    }
}