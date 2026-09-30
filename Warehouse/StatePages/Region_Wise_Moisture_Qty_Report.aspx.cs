using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Region_Wise_Moisture_Qty_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
    ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
decimal TotalReceivedQty = 0;
    decimal TotalMoistureQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindReport();
        }
    }

    private void BindReport()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(
                "Proc_Region_Wise_Moisture_Qty_Report", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            lblTotalRecords.Text =
                dt.Rows.Count.ToString();

            btnExport.Visible =
                dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('" +
                ex.Message.Replace("'", "") +
                "');",
                true);
        }
    }

    protected void gvReport_RowDataBound(
        object sender,
        GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalReceivedQty += Convert.ToDecimal(
                DataBinder.Eval(
                e.Row.DataItem,
                "Total Received Qty"));

            TotalMoistureQty += Convert.ToDecimal(
                DataBinder.Eval(
                e.Row.DataItem,
                "Total Moisture Qty"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text =
                "GRAND TOTAL";

            e.Row.Cells[2].Text =
                TotalReceivedQty.ToString("N2");

            e.Row.Cells[3].Text =
                TotalMoistureQty.ToString("N2");

            e.Row.Font.Bold = true;
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=RegionWiseMoistureQtyReport.xls");

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        sw.WriteLine("<table>");
        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='4' style='font-size:18px;font-weight:bold;text-align:center;'>");
        sw.WriteLine("M.P. Warehousing & Logistics Corporation");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='4' style='font-size:14px;font-weight:bold;text-align:center;'>");
        sw.WriteLine("Region Wise Moisture Quantity Report"+" " + "As On Date : " + DateTime.Now.ToString("dd/MM/yyyy"));
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("</table>");

        sw.WriteLine("<br/>");

        gvReport.HeaderStyle.BackColor =
            System.Drawing.ColorTranslator.FromHtml("#1a5276");

        gvReport.HeaderStyle.ForeColor =
            System.Drawing.Color.White;

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());

        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(
        Control control)
    {
    }

}
