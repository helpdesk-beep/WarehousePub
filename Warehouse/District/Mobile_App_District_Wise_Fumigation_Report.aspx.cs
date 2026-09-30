using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Mobile_App_District_Wise_Fumigation_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    int TotalGodownsCovered = 0;
    int TotalStacksFumigated = 0;
    int CompletedFumigations = 0;
    int TotalOpenedStacks = 0;

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
            if (Session["Depot_DistID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            SqlCommand cmd = new SqlCommand(
                "Proc_Mobile_App_Branch_Wise_Fumigation_Report_For_RO", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@DistrictId",
                Session["Depot_DistID"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            lblTotalRecords.Text = dt.Rows.Count.ToString();

            btnExport.Visible = dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('" + ex.Message.Replace("'", "") + "');",
                true);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalGodownsCovered += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem, "Total_Godowns_Covered"));

            TotalStacksFumigated += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem, "Total_Stacks_Fumigated"));

            CompletedFumigations += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem, "Completed_Fumigations"));

            TotalOpenedStacks += Convert.ToInt32(
                DataBinder.Eval(e.Row.DataItem, "Total_Opened_Stacks"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "GRAND TOTAL";

            e.Row.Cells[3].Text = TotalGodownsCovered.ToString();
            e.Row.Cells[4].Text = TotalStacksFumigated.ToString();
            e.Row.Cells[5].Text = CompletedFumigations.ToString();
            e.Row.Cells[6].Text = TotalOpenedStacks.ToString();

            e.Row.Font.Bold = true;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        PrepareGridViewForExport(gvReport);

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=DistrictWiseFumigationReport.xls");

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        sw.WriteLine("<table width='100%' border='0'>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='7' align='center' style='font-size:20px;font-weight:bold;'>");
        sw.WriteLine("M.P. Warehousing & Logistics Corporation");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='7' align='center' style='font-size:15px;font-weight:bold;'>");
        sw.WriteLine("District Wise Fumigation Report");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='7' align='center'>");
        sw.WriteLine("Date : " + DateTime.Now.ToString("dd/MM/yyyy"));
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("</table><br/>");

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    private void PrepareGridViewForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];

            if (current is HyperLink)
            {
                control.Controls.Remove(current);

                control.Controls.AddAt(i,
                    new LiteralControl(((HyperLink)current).Text));
            }

            if (current.HasControls())
            {
                PrepareGridViewForExport(current);
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}