using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Mobile_App_District_Wise_Fumigation_Report_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    DataTable dt = new DataTable();

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
        string RegionID = "0";

        if (Request.QueryString["Region_ID"] != null)
        {
            RegionID = Request.QueryString["Region_ID"].ToString();
        }

        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_District_Wise_Fumigation_Report_For_RO",
            con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
            "@RegionID",
            RegionID);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        da.Fill(dt);

        ViewState["dtReport"] = dt;

        gvReport.DataSource = dt;

        gvReport.DataBind();
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
            "attachment;filename=District_Wise_Fumigation_Report.xls");

        Response.Charset = "";

        Response.ContentType =
            "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw =
            new HtmlTextWriter(sw);

        gvReport.GridLines =
            GridLines.Both;

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());

        Response.End();
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.FooterRow == null)
            return;

        DataTable dtData = ViewState["dtReport"] as DataTable;

        if (dtData == null || dtData.Rows.Count == 0)
            return;

        decimal TotalGodowns = 0;
        decimal TotalStacks = 0;
        decimal Completed = 0;
        decimal Opened = 0;

        foreach (DataRow dr in dtData.Rows)
        {
            TotalGodowns += Convert.ToDecimal(dr["Total_Godowns_Covered"]);
            TotalStacks += Convert.ToDecimal(dr["Total_Stacks_Fumigated"]);
            Completed += Convert.ToDecimal(dr["Completed_Fumigations"]);
            Opened += Convert.ToDecimal(dr["Total_Opened_Stacks"]);
        }

        gvReport.FooterRow.CssClass = "totalRow";

        gvReport.FooterRow.Cells[0].Text = "";
        gvReport.FooterRow.Cells[1].Text = "TOTAL";
        gvReport.FooterRow.Cells[2].Text = "";

        gvReport.FooterRow.Cells[3].Text = TotalGodowns.ToString("N0");
        gvReport.FooterRow.Cells[4].Text = TotalStacks.ToString("N0");
        gvReport.FooterRow.Cells[5].Text = Completed.ToString("N0");
        gvReport.FooterRow.Cells[6].Text = Opened.ToString("N0");
    }

    public override void VerifyRenderingInServerForm(
        Control control)
    {
    }
}