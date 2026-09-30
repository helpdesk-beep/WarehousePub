using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_Mobile_App_Branch_Wise_Fumigation_Report_For_RO
    : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

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
        string DistrictID = "0";

        if (Request.QueryString["DistrictID"] != null)
        {
            DistrictID =
                Request.QueryString["DistrictID"].ToString();
        }

        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_Branch_Wise_Fumigation_Report_For_RO",
            con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
            "@DistrictID",
            DistrictID);

        SqlDataAdapter da =
            new SqlDataAdapter(cmd);

        DataTable dt =
            new DataTable();

        da.Fill(dt);

        ViewState["dtReport"] = dt;

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }

    protected void gvReport_DataBound(
        object sender,
        EventArgs e)
    {
        if (gvReport.FooterRow == null)
            return;

        DataTable dt =
            ViewState["dtReport"] as DataTable;

        if (dt == null || dt.Rows.Count == 0)
            return;

        gvReport.FooterRow.CssClass =
            "totalRow";

        gvReport.FooterRow.Cells[1].Text =
            "TOTAL";

        gvReport.FooterRow.Cells[2].Text = "";

        gvReport.FooterRow.Cells[3].Text =
            dt.Compute(
            "SUM(Total_Godowns_Covered)",
            "").ToString();

        gvReport.FooterRow.Cells[4].Text =
            dt.Compute(
            "SUM(Total_Stacks_Fumigated)",
            "").ToString();

        gvReport.FooterRow.Cells[5].Text =
            dt.Compute(
            "SUM(Completed_Fumigations)",
            "").ToString();

        gvReport.FooterRow.Cells[6].Text =
            dt.Compute(
            "SUM(Total_Opened_Stacks)",
            "").ToString();
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
            "attachment;filename=Branch_Wise_Fumigation_Report.xls");

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