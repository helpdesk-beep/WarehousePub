using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Mobile_App_Region_Wise_Mosture_Report_For_HO
    : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReport();
        }
    }

    private void BindReport()
    {
        SqlCommand cmd = new SqlCommand(
        "Proc_Mobile_App_Region_Wise_Moisture_Report_For_HO",
        con);

        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        dt.Clear();

        da.Fill(dt);

        gvReport.DataSource = dt;

        gvReport.DataBind();
    }

    protected void gvReport_RowDataBound(
        object sender,
        GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "totalRow";

            e.Row.Cells[0].Text = "";
            e.Row.Cells[1].Text = "TOTAL";

            DataTable dtData = (DataTable)gvReport.DataSource;

            if (dtData != null && dtData.Rows.Count > 0)
            {

                e.Row.Cells[2].Text =
                    dtData.Compute("SUM([Total_Stack])", "").ToString();

                e.Row.Cells[3].Text =
                    dtData.Compute("SUM([Prev Stack])", "").ToString();

                e.Row.Cells[4].Text =
                    dtData.Compute("SUM([Today Stack])", "").ToString();

                e.Row.Cells[5].Text =
                    dtData.Compute("SUM([Total Moisture Stack])", "").ToString();

                e.Row.Cells[6].Text =
                    dtData.Compute("SUM([Pending Stack])", "").ToString();

                e.Row.Cells[7].Text =
                    dtData.Compute("SUM([Prev Moisture Sent_DM])", "").ToString();

                e.Row.Cells[8].Text =
                    dtData.Compute("SUM([Today Moisture Sent_DM])", "").ToString();

                e.Row.Cells[9].Text =
                    dtData.Compute("SUM([Total Moisture Sent_DM])", "").ToString();

                e.Row.Cells[10].Text =
                    dtData.Compute("SUM([Prev Moisture Submit To FCI])", "").ToString();

                e.Row.Cells[11].Text =
                    dtData.Compute("SUM([Today Moisture Submit To FCI])", "").ToString();

                e.Row.Cells[12].Text =
                    dtData.Compute("SUM([Total Moisture Submit To FCI])", "").ToString();

                e.Row.Cells[13].Text =
                    dtData.Compute("SUM([FCI Inspected Stack])", "").ToString();

                e.Row.Cells[14].Text = "";
            }
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
        "attachment;filename=Mobile_App_Region_Wise_Moisture_Report.xls");

        Response.Charset = "";

        Response.ContentType =
        "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw =
        new HtmlTextWriter(sw);

        gvReport.GridLines = GridLines.Both;

        gvReport.HeaderStyle.Font.Bold = true;

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());

        Response.End();
    }

    public override void VerifyRenderingInServerForm(
    Control control)
    {

    }
}