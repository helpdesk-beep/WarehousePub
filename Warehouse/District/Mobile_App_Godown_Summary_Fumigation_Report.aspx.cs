using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Mobile_App_Godown_Summary_Fumigation_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    int TotalStack = 0;

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
            string BranchID = "0";

            if (Request.QueryString["BranchID"] != null)
            {
                BranchID = Request.QueryString["BranchID"].ToString();
            }

            SqlCommand cmd = new SqlCommand(
                "Proc_Mobile_App_Branch_Summary_Fumigation_Report_BO",
                con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);

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

    protected void gvReport_RowDataBound(object sender,
        GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object obj =
                DataBinder.Eval(e.Row.DataItem, "Total_Stack");

            if (obj != DBNull.Value)
            {
                TotalStack += Convert.ToInt32(obj);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "GRAND TOTAL";
            e.Row.Cells[2].Text = TotalStack.ToString();

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
            "attachment;filename=GodownSummaryFumigationReport.xls");

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        sw.WriteLine("<table width='100%'>");
        sw.WriteLine("<tr><td colspan='12' align='center' style='font-size:18px;font-weight:bold'>M.P. Warehousing & Logistics Corporation</td></tr>");
        sw.WriteLine("<tr><td colspan='12' align='center' style='font-size:14px;font-weight:bold'>Godown Summary Fumigation Report</td></tr>");
        sw.WriteLine("<tr><td colspan='12' align='center'>Date : " + DateTime.Now.ToString("dd/MM/yyyy") + "</td></tr>");
        sw.WriteLine("</table><br/>");

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.End();
    }

    private void PrepareGridViewForExport(Control gv)
    {
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            Control current = gv.Controls[i];

            if (current is HyperLink)
            {
                gv.Controls.Remove(current);
                gv.Controls.AddAt(i,
                    new LiteralControl((current as HyperLink).Text));
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