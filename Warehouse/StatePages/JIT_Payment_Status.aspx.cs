using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_JIT_Payment_Status : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
    ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    DataTable dt = new DataTable();

    string CurrentRegion = "";

    int RegionBillCount = 0;
    decimal RegionBillAmount = 0;
    int RegionBillReceived = 0;
    decimal RegionReceivedAmount = 0;
    int RegionBillPaid = 0;
    decimal RegionPaidAmount = 0;

    int GrandBillCount = 0;
    decimal GrandBillAmount = 0;
    int GrandBillReceived = 0;
    decimal GrandReceivedAmount = 0;
    int GrandBillPaid = 0;
    decimal GrandPaidAmount = 0;

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
        "SELECT DISTINCT District_ID,District_Name FROM tbl_MetaData_DISTRICT WHERE Region_ID=" +
        ddlRegion.SelectedValue +
        " ORDER BY District_Name", con);

        DataTable dtDistrict = new DataTable();

        da.Fill(dtDistrict);

        ddlDistrict.DataSource = dtDistrict;
        ddlDistrict.DataTextField = "District_Name";
        ddlDistrict.DataValueField = "District_ID";
        ddlDistrict.DataBind();

        ddlDistrict.Items.Insert(0,
        new ListItem("--All District--", "0"));

        ddlDepot.Items.Insert(0,
        new ListItem("--All Branch--", "0"));

        ddlGodown.Items.Insert(0,
        new ListItem("--All Godown--", "0"));

        BindReport();
    }

    protected void ddlDistrict_SelectedIndexChanged(
    object sender,
    EventArgs e)
    {
        ddlDepot.Items.Clear();
        ddlGodown.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT BranchId,DepotName FROM tbl_metadata_Depot WHERE DistrictId=" +
        ddlDistrict.SelectedValue +
        " ORDER BY DepotName", con);

        DataTable dtDepot = new DataTable();

        da.Fill(dtDepot);

        ddlDepot.DataSource = dtDepot;
        ddlDepot.DataTextField = "DepotName";
        ddlDepot.DataValueField = "BranchId";
        ddlDepot.DataBind();

        ddlDepot.Items.Insert(0,
        new ListItem("--All Branch--", "0"));

        ddlGodown.Items.Insert(0,
        new ListItem("--All Godown--", "0"));

        BindReport();
    }

    protected void ddlDepot_SelectedIndexChanged(
    object sender,
    EventArgs e)
    {
        ddlGodown.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter(
        "SELECT DISTINCT Godown_ID,Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchID=" +
        ddlDepot.SelectedValue +
        " ORDER BY Godown_Name", con);

        DataTable dtGodown = new DataTable();

        da.Fill(dtGodown);

        ddlGodown.DataSource = dtGodown;
        ddlGodown.DataTextField = "Godown_Name";
        ddlGodown.DataValueField = "Godown_ID";
        ddlGodown.DataBind();

        ddlGodown.Items.Insert(0,
        new ListItem("--All Godown--", "0"));

        BindReport();
    }

    protected void ddlGodown_SelectedIndexChanged(
    object sender,
    EventArgs e)
    {
        BindReport();
    }

    private void BindReport()
    {
        CurrentRegion = "";

        RegionBillCount = 0;
        RegionBillAmount = 0;
        RegionBillReceived = 0;
        RegionReceivedAmount = 0;
        RegionBillPaid = 0;
        RegionPaidAmount = 0;

        GrandBillCount = 0;
        GrandBillAmount = 0;
        GrandBillReceived = 0;
        GrandReceivedAmount = 0;
        GrandBillPaid = 0;
        GrandPaidAmount = 0;

        SqlCommand cmd = new SqlCommand(
        "Get_JIT_Bill_Details", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
        "@Region_ID",
        ddlRegion.SelectedValue);

        cmd.Parameters.AddWithValue(
        "@District_ID",
        ddlDistrict.SelectedValue);

        cmd.Parameters.AddWithValue(
        "@BranchId",
        ddlDepot.SelectedValue);

        cmd.Parameters.AddWithValue(
        "@Godown_ID",
        ddlGodown.SelectedValue);

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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Region =
            Convert.ToString(
            DataBinder.Eval(
            e.Row.DataItem,
            "Regionnm"));

            int NoOfBill =
            Convert.ToInt32(
            DataBinder.Eval(
            e.Row.DataItem,
            "No_Of_Bill") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "No_Of_Bill"));

            decimal BillAmount =
            Convert.ToDecimal(
            DataBinder.Eval(
            e.Row.DataItem,
            "Bill_Amount") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "Bill_Amount"));

            int BillReceived =
            Convert.ToInt32(
            DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Received From MPSCSC") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Received From MPSCSC"));

            decimal ReceivedAmount =
            Convert.ToDecimal(
            DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Amount Received From MPSCSC") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Amount Received From MPSCSC"));

            int BillPaid =
            Convert.ToInt32(
            DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Pay to Godown Owner") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Pay to Godown Owner"));

            decimal PaidAmount =
            Convert.ToDecimal(
            DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Amount Pay to Godown Owner") == DBNull.Value
            ? 0
            : DataBinder.Eval(
            e.Row.DataItem,
            "Total Bill Amount Pay to Godown Owner"));

            if (CurrentRegion == "")
            {
                CurrentRegion = Region;
            }

            if (CurrentRegion != Region)
            {
                AddRegionTotalRow();

                RegionBillCount = 0;
                RegionBillAmount = 0;
                RegionBillReceived = 0;
                RegionReceivedAmount = 0;
                RegionBillPaid = 0;
                RegionPaidAmount = 0;

                CurrentRegion = Region;
            }

            RegionBillCount += NoOfBill;
            RegionBillAmount += BillAmount;
            RegionBillReceived += BillReceived;
            RegionReceivedAmount += ReceivedAmount;
            RegionBillPaid += BillPaid;
            RegionPaidAmount += PaidAmount;

            GrandBillCount += NoOfBill;
            GrandBillAmount += BillAmount;
            GrandBillReceived += BillReceived;
            GrandReceivedAmount += ReceivedAmount;
            GrandBillPaid += BillPaid;
            GrandPaidAmount += PaidAmount;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            AddRegionTotalRow();

            e.Row.Cells.Clear();

            TableCell cell = new TableCell();

            cell.Text = "Grand Total";

            cell.ColumnSpan = 6;

            cell.CssClass = "grandtotal";

            e.Row.Cells.Add(cell);

            e.Row.Cells.Add(GetGrandCellInt(GrandBillCount));
            e.Row.Cells.Add(GetGrandCellDecimal(GrandBillAmount));
            e.Row.Cells.Add(GetGrandCellInt(GrandBillReceived));
            e.Row.Cells.Add(GetGrandCellDecimal(GrandReceivedAmount));
            e.Row.Cells.Add(GetGrandCellInt(GrandBillPaid));
            e.Row.Cells.Add(GetGrandCellDecimal(GrandPaidAmount));
        }
    }

    private void AddRegionTotalRow()
    {
        GridViewRow row = new GridViewRow(
        0,
        0,
        DataControlRowType.DataRow,
        DataControlRowState.Normal);

        TableCell cell = new TableCell();

        cell.Text = "Region Total : " + CurrentRegion;

        cell.ColumnSpan = 6;

        cell.CssClass = "subtotal";

        row.Cells.Add(cell);

        row.Cells.Add(GetSubTotalCellInt(RegionBillCount));
        row.Cells.Add(GetSubTotalCellDecimal(RegionBillAmount));
        row.Cells.Add(GetSubTotalCellInt(RegionBillReceived));
        row.Cells.Add(GetSubTotalCellDecimal(RegionReceivedAmount));
        row.Cells.Add(GetSubTotalCellInt(RegionBillPaid));
        row.Cells.Add(GetSubTotalCellDecimal(RegionPaidAmount));

        gvReport.Controls[0].Controls.AddAt(
        gvReport.Controls[0].Controls.Count - 1,
        row);
    }

    private TableCell GetSubTotalCellInt(int value)
    {
        TableCell cell = new TableCell();

        cell.Text = value.ToString();

        cell.CssClass = "subtotal";

        return cell;
    }

    private TableCell GetSubTotalCellDecimal(decimal value)
    {
        TableCell cell = new TableCell();

        cell.Text = value.ToString("N2");

        cell.CssClass = "subtotal";

        return cell;
    }

    private TableCell GetGrandCellInt(int value)
    {
        TableCell cell = new TableCell();

        cell.Text = value.ToString();

        cell.CssClass = "grandtotal";

        return cell;
    }

    private TableCell GetGrandCellDecimal(decimal value)
    {
        TableCell cell = new TableCell();

        cell.Text = value.ToString("N2");

        cell.CssClass = "grandtotal";

        return cell;
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
        "attachment;filename=JIT_Payment_Status.xls");

        Response.ContentType =
        "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw =
        new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();
    }

    public override void VerifyRenderingInServerForm(
    Control control)
    {

    }
}