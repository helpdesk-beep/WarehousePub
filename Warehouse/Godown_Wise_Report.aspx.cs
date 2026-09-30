using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Godown_Wise_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
       ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    DataTable dt = new DataTable();

    string CurrentGodown = "";

    decimal[] GodownTotals = new decimal[13];
    decimal[] GrandTotals = new decimal[13];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRegion();
            BindReport();
        }
    }

    private void FillRegion()
    {
        SqlDataAdapter da = new SqlDataAdapter(
            "SELECT DISTINCT Region_ID,Regionnm FROM tbl_MetaData_DISTRICT ORDER BY Regionnm", con);

        DataTable dtRegion = new DataTable();
        da.Fill(dtRegion);

        ddlRegion.DataSource = dtRegion;
        ddlRegion.DataTextField = "Regionnm";
        ddlRegion.DataValueField = "Region_ID";
        ddlRegion.DataBind();

        ddlRegion.Items.Insert(0, new ListItem("--All Region--", "0"));

        ddlDistrict.Items.Insert(0, new ListItem("--All District--", "0"));
        ddlDepot.Items.Insert(0, new ListItem("--All Branch--", "0"));
        ddlGodown.Items.Insert(0, new ListItem("--All Godown--", "0"));
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        CurrentGodown = "";

        for (int i = 0; i < 13; i++)
        {
            GodownTotals[i] = 0;
            GrandTotals[i] = 0;
        }

        SqlCommand cmd = new SqlCommand("Proc_Godown_Wise_Report_Sub_Total_New", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 300;
        cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
        cmd.Parameters.AddWithValue("@District_ID", ddlDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchId", ddlDepot.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        dt.Clear();
        da.Fill(dt);

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string godown = DataBinder.Eval(e.Row.DataItem, "Godown_Name").ToString();

            if (CurrentGodown == "") CurrentGodown = godown;

            if (CurrentGodown != godown)
            {
                AddSubTotalRow();

                for (int i = 0; i < 13; i++)
                    GodownTotals[i] = 0;

                CurrentGodown = godown;
            }

            for (int i = 8; i <= 20; i++)
            {
                decimal val;
                decimal.TryParse(e.Row.Cells[i].Text.Replace(",", ""), out val);

                GodownTotals[i - 8] += val;
                GrandTotals[i - 8] += val;

                e.Row.Cells[i].CssClass = "right";
                e.Row.Cells[i].Text = val.ToString("N2");
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            AddSubTotalRow();

            e.Row.Cells.Clear();

            TableCell cell = new TableCell();
            cell.Text = "Grand Total";
            cell.ColumnSpan = 8;
            cell.CssClass = "grandtotal";
            e.Row.Cells.Add(cell);

            for (int i = 0; i < 13; i++)
            {
                TableCell c = new TableCell();
                c.Text = GrandTotals[i].ToString("N2");
                c.CssClass = "grandtotal right";
                e.Row.Cells.Add(c);
            }
        }
    }

    private void AddSubTotalRow()
    {
        GridViewRow row = new GridViewRow(0, 0,
            DataControlRowType.DataRow, DataControlRowState.Normal);

        TableCell cell = new TableCell();
        cell.Text = "Godown Total : " + CurrentGodown;
        cell.ColumnSpan = 8;
        cell.CssClass = "subtotal";
        row.Cells.Add(cell);

        for (int i = 0; i < 13; i++)
        {
            TableCell c = new TableCell();
            c.Text = GodownTotals[i].ToString("N2");
            c.CssClass = "subtotal right";
            row.Cells.Add(c);
        }

        gvReport.Controls[0].Controls.AddAt(
            gvReport.Controls[0].Controls.Count - 1, row);
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        BindReport();

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=GodownReport.xls");
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control) { }
}