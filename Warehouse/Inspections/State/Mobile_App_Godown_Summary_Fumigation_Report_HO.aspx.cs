using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Mobile_App_Godown_Summary_Fumigation_Report_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    DataTable dt = new DataTable();
    private int GrandTotalStack = 0;
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
        string BranchID = "0";

        if (Request.QueryString["BranchID"] != null)
        {
            BranchID =
                Request.QueryString["BranchID"].ToString();
        }

        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_Branch_Summary_Fumigation_Report_BO",
            con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
            "@BranchID",
            BranchID);

        SqlDataAdapter da =
            new SqlDataAdapter(cmd);

        dt.Clear();

        da.Fill(dt);

        ViewState["dtReport"] = dt;

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }

    protected void gvReport_DataBound(object sender, EventArgs e)
    {
        if (gvReport.FooterRow == null)
            return;

        decimal TotalStack = 0;

        DataTable dtData = ViewState["dtReport"] as DataTable;

        if (dtData != null)
        {
            foreach (DataRow dr in dtData.Rows)
            {
                TotalStack += Convert.ToDecimal(dr["Total_Stack"]);
            }
        }

        gvReport.FooterRow.CssClass = "totalRow";

        gvReport.FooterRow.Cells[1].Text = "TOTAL";

        gvReport.FooterRow.Cells[2].Text =
            TotalStack.ToString("N0");
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
            "attachment;filename=Branch_Summary_Fumigation_Report.xls");

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

    public override void VerifyRenderingInServerForm(
        Control control)
    {
    }

    protected void gvReport_RowDataBound(
    object sender,
    GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object value =
                DataBinder.Eval(
                e.Row.DataItem,
                "Total_Stack");

            if (value != DBNull.Value)
            {
                GrandTotalStack +=
                    Convert.ToInt32(value);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "totalRow";

            e.Row.Cells[0].Text = "";

            e.Row.Cells[1].Text = "TOTAL";

            e.Row.Cells[2].Text =
                GrandTotalStack.ToString();

            e.Row.Cells[3].Text = "";
            e.Row.Cells[4].Text = "";
            e.Row.Cells[5].Text = "";
            e.Row.Cells[6].Text = "";
            e.Row.Cells[7].Text = "";
            e.Row.Cells[8].Text = "";
            e.Row.Cells[9].Text = "";
            e.Row.Cells[10].Text = "";
            e.Row.Cells[11].Text = "";
        }
    }
}