using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NCCF_NCCF_Printed_Bills_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDistricts();
            //LoadPrintedBills();
            LoadCommodity();
            LoadCropYear();
        }
    }

    private void LoadDistricts()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT DISTINCT District_Name FROM tbl_NCCF_Printed_Bills_Log ORDER BY District_Name";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlDistrict.DataSource = reader;
                        ddlDistrict.DataTextField = "District_Name";
                        ddlDistrict.DataValueField = "District_Name";
                        ddlDistrict.DataBind();
                    }
                }
            }

            ddlDistrict.Items.Insert(0, new ListItem("All Districts", ""));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in LoadDistricts: " + ex.Message);
        }
    }

    private void LoadCommodity()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Distinct LEFT(Commodity, CHARINDEX(' ', Commodity) - 1) AS Commodity FROM tbl_NCCF_Printed_Bills_Log;";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlCommodity.DataSource = reader;
                        ddlCommodity.DataTextField = "Commodity";
                        ddlCommodity.DataValueField = "Commodity";
                        ddlCommodity.DataBind();
                    }
                }
            }

            ddlCommodity.Items.Insert(0, new ListItem("All Commodity", ""));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in LoadCommodity: " + ex.Message);
        }
    }

    private void LoadCropYear()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select Distinct Crop_Year from tbl_NCCF_Printed_Bills_Log";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ddlCropYear.DataSource = reader;
                        ddlCropYear.DataTextField = "Crop_Year";
                        ddlCropYear.DataValueField = "Crop_Year";
                        ddlCropYear.DataBind();
                    }
                }
            }

            ddlCropYear.Items.Insert(0, new ListItem("All Crop Year", ""));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in LoadCrop_Year: " + ex.Message);
        }
    }

    private void LoadPrintedBills()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Get_NCCF_Printed_Bills_Log", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    DateTime fromDate, toDate;

                    if (!string.IsNullOrEmpty(txtFromDate.Text) &&
                        DateTime.TryParseExact(txtFromDate.Text, "dd/MM/yyyy", null,
                        System.Globalization.DateTimeStyles.None, out fromDate))
                    {
                        cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    }

                    if (!string.IsNullOrEmpty(txtToDate.Text) &&
                        DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null,
                        System.Globalization.DateTimeStyles.None, out toDate))
                    {
                        cmd.Parameters.AddWithValue("@ToDate", toDate);
                    }

                    if (!string.IsNullOrEmpty(txtBillNo.Text))
                        cmd.Parameters.AddWithValue("@Bill_Number", txtBillNo.Text.Trim());

                    if (!string.IsNullOrEmpty(ddlDistrict.SelectedValue) && ddlDistrict.SelectedValue != "")
                        cmd.Parameters.AddWithValue("@District_Name", ddlDistrict.SelectedValue);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvPrintedBills.DataSource = dt;
                        gvPrintedBills.DataBind();

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "loadError",
                "alert('Error loading data: " + ex.Message + "');", true);
        }
    }


    protected void gvPrintedBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.ToolTip = "Printed Bill Record";

                if (e.Row.Cells.Count > 5)
                {
                    e.Row.Cells[5].Font.Bold = true;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in RowDataBound: " + ex.Message);
        }
    }

    protected void gvPrintedBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteRecord")
            {
                string billNumber = e.CommandArgument.ToString() ?? "";

                if (!string.IsNullOrEmpty(billNumber))
                {
                    // Show confirmation using MessageBox style
                    string confirmScript = "if(confirm('Are you sure you want to delete Bill: " + billNumber + "? This action cannot be undone!')){";

                    // Call delete method
                    bool deleted = DeletePrintedBillByBillNumber(billNumber);

                    if (deleted)
                    {
                        confirmScript += "alert('Bill " + billNumber + " deleted successfully!');";
                        confirmScript += "}";
                        ClientScript.RegisterStartupScript(this.GetType(), "deleteSuccess", confirmScript, true);

                        // Reload the grid
                        LoadPrintedBills();
                    }
                    else
                    {
                        confirmScript += "alert('Failed to delete Bill: " + billNumber + "');";
                        confirmScript += "}";
                        ClientScript.RegisterStartupScript(this.GetType(), "deleteError", confirmScript, true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "deleteError",
                        "alert('Invalid Bill Number!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "deleteError",
                "alert('Error: " + ex.Message + "');", true);
        }
    }

    private bool DeletePrintedBillByBillNumber(string billNumber)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            int userID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : 0;
            string userIP = Request.ServerVariables["REMOTE_ADDR"].ToString() ?? "0.0.0.0";

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Delete_NCCF_Printed_Bill", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;

                    cmd.Parameters.AddWithValue("@Bill_Number", billNumber.Trim());
                    cmd.Parameters.AddWithValue("@Deleted_By", userID);
                    cmd.Parameters.AddWithValue("@Deleted_By_IP", userIP);
                    cmd.Parameters.AddWithValue("@Deletion_Reason", DBNull.Value);

                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string status = reader["Status"].ToString() ?? "";

                            if (status == "Success")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in Delete: " + ex.Message);
            return false;
        }
    }

    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        int deletedCount = 0;

        foreach (GridViewRow row in gvPrintedBills.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            HiddenField hf = (HiddenField)row.FindControl("hfBillNo");

            if (chk != null && chk.Checked && hf != null)
            {
                string billNo = hf.Value;

                if (!string.IsNullOrEmpty(billNo))
                {
                    bool result = DeletePrintedBillByBillNumber(billNo);

                    if (result)
                        deletedCount++;
                }
            }
        }

        if (deletedCount > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg",
                "alert('" + deletedCount + " record(s) deleted successfully');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg",
                "alert('No record selected');", true);
        }

        LoadPrintedBills();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // Commodity validation
        if (string.IsNullOrEmpty(ddlCommodity.SelectedValue))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg",
                "alert('Please select Commodity');", true);
            return;
        }

        // Crop Year validation
        if (string.IsNullOrEmpty(ddlCropYear.SelectedValue))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg",
                "alert('Please select Crop Year');", true);
            return;
        }

        // If both selected → load data
        LoadPrintedBills();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtBillNo.Text = "";
        ddlDistrict.SelectedIndex = 0;
        LoadPrintedBills();
    }

    protected void gvPrintedBills_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPrintedBills.PageIndex = e.NewPageIndex;
        LoadPrintedBills();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "exportError",
                "alert('Error exporting to Excel: " + ex.Message + "');", true);
        }
    }

    private void ExportToExcel()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Usp_Get_NCCF_Printed_Bills_Log", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                DateTime fromDate, toDate;

                if (!string.IsNullOrEmpty(txtFromDate.Text) &&
                    DateTime.TryParseExact(txtFromDate.Text, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out fromDate))
                {
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                }

                if (!string.IsNullOrEmpty(txtToDate.Text) &&
                    DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out toDate))
                {
                    cmd.Parameters.AddWithValue("@ToDate", toDate);
                }

                if (!string.IsNullOrEmpty(txtBillNo.Text))
                    cmd.Parameters.AddWithValue("@Bill_Number", txtBillNo.Text);

                if (!string.IsNullOrEmpty(ddlDistrict.SelectedValue) && ddlDistrict.SelectedValue != "")
                    cmd.Parameters.AddWithValue("@District_Name", ddlDistrict.SelectedValue);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ExportDataTableToExcel(dt, "NCCF_Printed_Bills_History.xls");
                }
            }
        }
    }

    private void ExportDataTableToExcel(DataTable dt, string filename)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                GridView gridView = new GridView();
                gridView.AutoGenerateColumns = false;

                gridView.Columns.Add(new BoundField { DataField = "Godown_Id", HeaderText = "Godown ID" });
                gridView.Columns.Add(new BoundField { DataField = "Godown_Name", HeaderText = "Godown Name" });
                gridView.Columns.Add(new BoundField { DataField = "Branch_Name", HeaderText = "Branch Name" });
                gridView.Columns.Add(new BoundField { DataField = "Commodity", HeaderText = "Commodity" });
                gridView.Columns.Add(new BoundField { DataField = "Bill_Number", HeaderText = "Bill Number" });

                gridView.DataSource = dt;
                gridView.DataBind();

                gridView.HeaderStyle.Font.Bold = true;
                gridView.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

                gridView.RenderControl(hw);

                Response.Write(sw.ToString());
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for GridView export
    }
}