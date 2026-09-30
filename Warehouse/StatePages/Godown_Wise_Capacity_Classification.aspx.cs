using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Godown_Wise_Capacity_Classification : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
    ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    DataTable dt = new DataTable();

    string CurrentRegion = "";
    decimal RegionTotal = 0;
    decimal GrandTotal = 0;

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

        ddlDistrict.Items.Insert(0, new ListItem("--All District--", ""));
        ddlDepot.Items.Insert(0, new ListItem("--All Branch--", ""));
        ddlGodown.Items.Insert(0, new ListItem("--All Godown--", ""));
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDistrict.Items.Clear();
        ddlDepot.Items.Clear();
        ddlGodown.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT District_ID, District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID=" +
        ddlRegion.SelectedValue +
        " ORDER BY District_Name", con);

        DataTable dtDistrict = new DataTable();

        da.Fill(dtDistrict);

        ddlDistrict.DataSource = dtDistrict;
        ddlDistrict.DataTextField = "District_Name";
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataBind();

        ddlDistrict.Items.Insert(0, new ListItem("--All District--", ""));

        ddlDepot.Items.Insert(0, new ListItem("--All Branch--", ""));
        ddlGodown.Items.Insert(0, new ListItem("--All Godown--", ""));

        BindReport();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDepot.Items.Clear();
        ddlGodown.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT BranchId,DepotName FROM tbl_metadata_Depot where DistrictId="
        + ddlDistrict.SelectedValue +
        " ORDER BY DepotName", con);

        DataTable dtDepot = new DataTable();

        da.Fill(dtDepot);

        ddlDepot.DataSource = dtDepot;
        ddlDepot.DataTextField = "DepotName";
        ddlDepot.DataValueField = "BranchId";
        ddlDepot.DataBind();

        ddlDepot.Items.Insert(0, new ListItem("--All Branch--", ""));
        ddlGodown.Items.Insert(0, new ListItem("--All Godown--", ""));

        BindReport();
    }


    protected void ddlDepot_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodown.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT Godown_ID, Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchID="
        + ddlDepot.SelectedValue +
        " ORDER BY Godown_Name", con);

        DataTable dtGodown = new DataTable();

        da.Fill(dtGodown);

        ddlGodown.DataSource = dtGodown;
        ddlGodown.DataTextField = "Godown_Name";
        ddlGodown.DataValueField = "Godown_ID";
        ddlGodown.DataBind();

        ddlGodown.Items.Insert(0, new ListItem("--All Godown--", ""));

        BindReport();
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReport();
    }


    private void BindReport()
    {
        SqlCommand cmd = new SqlCommand("USP_Godown_Wise_Capacity_Classification_Report", con);

        cmd.CommandType = CommandType.StoredProcedure;
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
            string Region = DataBinder.Eval(e.Row.DataItem, "Regionnm").ToString();
            decimal Capacity =Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Godown Capcity"));
            GrandTotal += Capacity;
            if (CurrentRegion == "")
            {
                CurrentRegion = Region;
            }
            if (CurrentRegion != Region)
            {
                GridViewRow row = new GridViewRow(
                0, 0,
                DataControlRowType.DataRow,
                DataControlRowState.Normal);

                TableCell cell = new TableCell();
                cell.Text = "Region Total : " + CurrentRegion;
                cell.ColumnSpan = 9;
                cell.CssClass = "subtotal";
                row.Cells.Add(cell);
                TableCell cell2 = new TableCell();
                cell2.Text = RegionTotal.ToString("N2");
                cell2.CssClass = "subtotal";
                row.Cells.Add(cell2);
                gvReport.Controls[0].Controls.AddAt(
                e.Row.RowIndex + 1,
                row);

                RegionTotal = 0;

                CurrentRegion = Region;
            }

            RegionTotal += Capacity;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells.Clear();

            TableCell cell = new TableCell();

            cell.Text = "Grand Total";

            cell.ColumnSpan = 9;

            cell.CssClass = "grandtotal";

            e.Row.Cells.Add(cell);

            TableCell cell2 = new TableCell();

            cell2.Text = GrandTotal.ToString("N2");

            cell2.CssClass = "grandtotal";

            e.Row.Cells.Add(cell2);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        BindReport();

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
        "content-disposition",
        "attachment;filename=Godown_Wise_Capacity_Classification.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}