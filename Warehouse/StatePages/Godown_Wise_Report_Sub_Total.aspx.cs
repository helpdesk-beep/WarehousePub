using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Godown_Wise_Report_Sub_Total
    : System.Web.UI.Page
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

        ddlRegion.Items.Insert(0,
        new ListItem("--All Region--", "0"));

        ddlDistrict.Items.Clear();
        ddlDistrict.Items.Insert(0,
        new ListItem("--All District--", "0"));

        ddlDepot.Items.Clear();
        ddlDepot.Items.Insert(0,
        new ListItem("--All Branch--", "0"));

        ddlGodown.Items.Clear();
        ddlGodown.Items.Insert(0,
        new ListItem("--All Godown--", "0"));
    }

    protected void ddlRegion_SelectedIndexChanged(
    object sender,
    EventArgs e)
    {
        ddlDistrict.Items.Clear();
        ddlDepot.Items.Clear();
        ddlGodown.Items.Clear();
        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT District_ID,District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID='" +
        ddlRegion.SelectedValue +
        "' ORDER BY District_Name", con);
        DataTable dtDistrict = new DataTable();
        da.Fill(dtDistrict);
        ddlDistrict.DataSource = dtDistrict;
        ddlDistrict.DataTextField = "District_Name";
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0,new ListItem("--All District--", "0"));
        ddlDepot.Items.Insert(0,new ListItem("--All Branch--", "0"));
        ddlGodown.Items.Insert(0,new ListItem("--All Godown--", "0"));
        BindReport();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender,EventArgs e)
    {
        ddlDepot.Items.Clear();
        ddlGodown.Items.Clear();
        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT BranchId,DepotName FROM tbl_metadata_Depot WHERE DistrictId='" +
        ddlDistrict.SelectedValue +
        "' ORDER BY DepotName", con);
        DataTable dtDepot = new DataTable();
        da.Fill(dtDepot);
        ddlDepot.DataSource = dtDepot;
        ddlDepot.DataTextField = "DepotName";
        ddlDepot.DataValueField = "BranchId";
        ddlDepot.DataBind();
        ddlDepot.Items.Insert(0,new ListItem("--All Branch--", "0"));
        ddlGodown.Items.Insert(0,new ListItem("--All Godown--", "0"));

        BindReport();
    }

    protected void ddlDepot_SelectedIndexChanged(object sender,EventArgs e)
    {
        ddlGodown.Items.Clear();
        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT Godown_ID,Godown_Name FROM tbl_MetaData_GODOWN WHERE BranchID='" +
        ddlDepot.SelectedValue + "' ORDER BY Godown_Name", con);
        DataTable dtGodown = new DataTable();
        da.Fill(dtGodown);
        ddlGodown.DataSource = dtGodown;
        ddlGodown.DataTextField = "Godown_Name";
        ddlGodown.DataValueField = "Godown_ID";
        ddlGodown.DataBind();
        ddlGodown.Items.Insert(0,new ListItem("--All Godown--", "0"));
        BindReport();
    }

    protected void ddlGodown_SelectedIndexChanged(object sender,EventArgs e)
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
        SqlCommand cmd =new SqlCommand("Proc_Godown_Wise_Report_Sub_Total_New",con);
        cmd.CommandType =CommandType.StoredProcedure;
        cmd.CommandTimeout = 300;
        cmd.Parameters.AddWithValue("@Region_ID",ddlRegion.SelectedValue);
        cmd.Parameters.AddWithValue("@District_ID",ddlDistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@BranchId",ddlDepot.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID",ddlGodown.SelectedValue);
        SqlDataAdapter da =new SqlDataAdapter(cmd);
        dt.Clear();
        da.Fill(dt);
        gvReport.DataSource = dt;
        gvReport.DataBind();
    }

    protected void gvReport_RowDataBound(
object sender,
GridViewRowEventArgs e)
    {
        if (e.Row.RowType ==
            DataControlRowType.DataRow)
        {
            string Godown =DataBinder.Eval(e.Row.DataItem,"Godown_Name").ToString();
            if (CurrentGodown == "")
            {
                CurrentGodown = Godown;
            }

            if (CurrentGodown != Godown)
            {
                AddSubTotalRow();

                for (int i = 0; i < 13; i++)
                {
                    GodownTotals[i] = 0;
                }

                CurrentGodown = Godown;
            }
            for (int i = 8; i <= 20; i++)
            {
                decimal val = 0;

                decimal.TryParse(
                    e.Row.Cells[i].Text.Replace(",", ""),
                    out val);

                GodownTotals[i - 8] += val;
                GrandTotals[i - 8] += val;

                e.Row.Cells[i].Text = val.ToString("N2");
                e.Row.Cells[i].CssClass = "right";
            }
        }

        if (e.Row.RowType ==
            DataControlRowType.Footer)
        {
            AddSubTotalRow();

            e.Row.Cells.Clear();

            TableCell cell = new TableCell();

            cell.Text = "Grand Total";
            cell.ColumnSpan = 7;
            cell.CssClass = "grandtotal";

            e.Row.Cells.Add(cell);

            // Capacity column blank
            TableCell capCell = new TableCell();
            capCell.Text = "";
            capCell.CssClass = "grandtotal";
            e.Row.Cells.Add(capCell);

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
        GridViewRow row = new GridViewRow(
            0, 0,
            DataControlRowType.DataRow,
            DataControlRowState.Normal);

        TableCell cell = new TableCell();

        cell.Text = "Godown Total : " + CurrentGodown;
        cell.ColumnSpan = 7;
        cell.CssClass = "subtotal";

        row.Cells.Add(cell);

        // Capacity column blank
        TableCell capCell = new TableCell();
        capCell.Text = "";
        capCell.CssClass = "subtotal";
        row.Cells.Add(capCell);

        // Year-wise totals
        for (int i = 0; i < 13; i++)
        {
            TableCell c = new TableCell();

            c.Text = GodownTotals[i].ToString("N2");
            c.CssClass = "subtotal right";

            row.Cells.Add(c);
        }

        gvReport.Controls[0].Controls.AddAt(
            gvReport.Controls[0].Controls.Count - 1,
            row);
    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        BindReport();

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=GodownWiseReport.xls");

        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        // Remove controls that break Excel rendering
        gvReport.AllowPaging = false;
        gvReport.GridLines = GridLines.Both;

        gvReport.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

        gvReport.RenderControl(hw);

        string html = @"
    <html>
    <head>
        <meta charset='utf-8'>
        <style>
            table { border-collapse:collapse; }
            td, th {
                border:1px solid #000;
                padding:5px;
                font-size:12px;
            }
            .subtotal {
                background-color:#d9e1f2;
                font-weight:bold;
            }
            .grandtotal {
                background-color:#c6e0b4;
                font-weight:bold;
            }
            .right { text-align:right; }
        </style>
    </head>
    <body>
    " + sw.ToString() + @"
    </body>
    </html>";

        Response.Output.Write(html);
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(
    Control control)
    {

    }
}