using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Attendance : System.Web.UI.Page
{
    static string GridVal = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) PopulateDropdowns();
    }

    private void PopulateDropdowns()
    {
        int currentYear = DateTime.Now.Year;
        for (int i = 0; i < 3; i++)
        {
            int year = currentYear - 1 + i;
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
        ddlYear.SelectedValue = currentYear.ToString();

        string[] monthNames = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
        for (int i = 0; i < 12; i++)
        {
            ddlMonth.Items.Add(new ListItem(monthNames[i], (i + 1).ToString()));
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private DataTable GetAttendanceDataGridview1()
    {
        DataTable dt = (DataTable)Session["AttendanceData"];
        if (dt == null)
        {
            dt = CreateDataGrid1();
            Session["AttendanceData"] = dt;
        }
        return dt;
    }

    private DataTable GetAttendanceDataGridview2()
    {
        DataTable dt = (DataTable)Session["AttendanceData2"];
        if (dt == null)
        {
            dt = CreateDataGrid2();
            Session["AttendanceData2"] = dt;
        }
        return dt;
    }

    private DataTable CreateDataGrid1()
    {
        DateTime first = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), 1);
        DateTime last = first.AddMonths(1).AddDays(-1);
        DataTable dt = new DataTable();
        dt.Columns.Add("SNo", typeof(int)); // Must be the DataKey
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("PlaceOfPosting", typeof(string));
        dt.Columns.Add("Designation", typeof(string));
        dt.Columns.Add("WorkingPeriodFrom", typeof(string));
        dt.Columns.Add("WorkingPeriodTo", typeof(string));
        dt.Columns.Add("Absent", typeof(string));

        dt.Rows.Add(1, "Dharmendra Vishwakarma", "Bhopal", "Project Manager", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(2, "Shobhit Kumar Namdeo", "Bhopal", "Sr. Programmer", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(3, "Shahrukh Khan", "Bhopal", "Asst. Programmer", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(4, "Shubham Vishwakarma", "Bhopal", "Asst. Programmer", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(5, "Fanikant Yadav", "Bhopal", "Asst. Programmer", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(6, "Ashwani Kumar", "Bhopal", "Technical Assistant", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(7, "Neha Malviya", "Bhopal", "Technical Assistant", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        dt.Rows.Add(8, "Ashutosh Sharma", "Bhopal", "Technical Assistant", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");

        return dt;
    }

    private DataTable CreateDataGrid2()
    {
        DateTime first = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), 1);
        DateTime last = first.AddMonths(1).AddDays(-1);
        DataTable dt = new DataTable();
        dt.Columns.Add("S_No", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("PlaceOfPosting", typeof(string));
        dt.Columns.Add("Designation", typeof(string));
        dt.Columns.Add("WorkingPeriodFrom", typeof(string));
        dt.Columns.Add("WorkingPeriodTo", typeof(string));
        dt.Columns.Add("Absent", typeof(string));

        dt.Rows.Add(1, "Fanikant Yadav", "Bhopal", "Asst. Programmer", first.ToString("dd-MM-yyyy"), last.ToString("dd-MM-yyyy"), "00");
        return dt;
    }

    private void BindGrid1()
    {
        H3Title.InnerText = "Month " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
        GridView1.DataSource = GetAttendanceDataGridview1();
        GridView1.DataBind();
    }

    private void BindGrid2()
    {
        H3Title.InnerText = "Month " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
        GridView2.DataSource = GetAttendanceDataGridview2();
        GridView2.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e) { Session["AttendanceData"] = null; Session["AttendanceData2"] = null; }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e) { Session["AttendanceData"] = null; Session["AttendanceData2"] = null; }

    protected void btnITSalarySlip_Click(object sender, EventArgs e)
    {
        GridVal = "Grid1";
        BindGrid1();
        GridView1.Visible = true;
        GridView2.Visible = false;
    }
    protected void btnNICSalarySlip_Click(object sender, EventArgs e)
    {
        GridVal = "Grid2";
        BindGrid2();
        GridView2.Visible = true;
        GridView1.Visible = false;
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) { GridView1.EditIndex = e.NewEditIndex; BindGrid1(); }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e) { GridView2.EditIndex = e.NewEditIndex; BindGrid2(); }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) { GridView1.EditIndex = -1; BindGrid1(); }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) { GridView2.EditIndex = -1; BindGrid2(); }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int sNo = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        GridViewRow row = GridView1.Rows[e.RowIndex];
        TextBox txtAbsent = (TextBox)row.FindControl("txtAbsent");
        string val = txtAbsent.Text;

        DataTable dt = (DataTable)Session["AttendanceData"];
        DataRow[] dr = dt.Select("SNo=" + sNo);
        dr[0]["Absent"] = val;
        Session["AttendanceData"] = dt;

        GridView1.EditIndex = -1;
        BindGrid1();
    }

    protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int sNo = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);
        GridViewRow row = GridView2.Rows[e.RowIndex];
        TextBox txtAbsent = (TextBox)row.FindControl("txtAbsent");
        string val = txtAbsent.Text;

        DataTable dt = (DataTable)Session["AttendanceData2"];
        DataRow[] dr = dt.Select("S_No=" + sNo);
        dr[0]["Absent"] = val;
        Session["AttendanceData2"] = dt;

        GridView2.EditIndex = -1;
        BindGrid2();
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e) { MergeHeader(GridView1, e); }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e) { MergeHeader(GridView2, e); }

    private void MergeHeader(GridView gv, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // ========= FIRST HEADER ROW =========
            GridViewRow headerRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            headerRow1.Cells.Add(CreateHeaderCell("S.No", 2));
            headerRow1.Cells.Add(CreateHeaderCell("Name", 2));
            headerRow1.Cells.Add(CreateHeaderCell("Place of Posting", 2));
            headerRow1.Cells.Add(CreateHeaderCell("Designation", 2));

            // WORKING PERIOD MERGED (From + To)
            headerRow1.Cells.Add(CreateHeaderCell("Working Period", 1, 2));

            headerRow1.Cells.Add(CreateHeaderCell("Absent", 2));
            headerRow1.Cells.Add(CreateHeaderCell("Action", 2, 1, true)); // ✅ Action column

            gv.Controls[0].Controls.AddAt(0, headerRow1);

            // ========= SECOND HEADER ROW =========
            GridViewRow headerRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            headerRow2.Cells.Add(CreateHeaderCell("From"));
            headerRow2.Cells.Add(CreateHeaderCell("To"));

            gv.Controls[0].Controls.AddAt(1, headerRow2);

            // REMOVE DEFAULT AUTO HEADER
            e.Row.Cells.Clear();
        }
    }


    private TableCell CreateHeaderCell(string text, int rowSpan = 1, int colSpan = 1, bool isAction = false)
    {
        TableCell cell = new TableCell();
        cell.Text = text;
        cell.RowSpan = rowSpan;
        cell.ColumnSpan = colSpan;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.VerticalAlign = VerticalAlign.Middle;
        cell.BorderWidth = 1;
        cell.BorderStyle = BorderStyle.Solid;
        cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#e0f2f1");
        cell.Font.Bold = true;

        if (isAction)
        {
            cell.CssClass = "no-print action-col";
        }

        return cell;
    }



    public override void VerifyRenderingInServerForm(Control control) { }
}
