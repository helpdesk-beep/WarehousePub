using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Get_Pending_Bill_Details_Report_BO : System.Web.UI.Page
{
    private string connStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"] != null
                             ? ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString
                             : "";

    // Total Calculation Variables
    private decimal totalBalance = 0;
    private int totalExpectedBills = 0;
    private int totalActualBills = 0;
    private int totalPendingBills = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["Depot_DepotID"] as string))
        {
            if (!IsPostBack)
            {
                // OnLoad par kewal Region dropdown fill hoga
                BindGodowns();

                printArea.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    #region Initial & Cascading Dropdown Binding

    private void BindGodowns()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string ddlBranch = Session["Depot_DepotID"].ToString();
            string query = "SELECT DISTINCT Godown_ID, Godown_Name FROM tbl_MetaData_GODOWN_2018 WHERE BranchID = '"+ ddlBranch + "' ORDER BY Godown_Name";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlGodown.DataSource = dt;
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, new ListItem("-- Select All Godowns --", "0"));
            }
        }
    }

    #endregion

    #region Helper Reset Methods


    #endregion

    #region Cascading Events

    #endregion

    #region Show Button & Report Processing

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindReportGrid();
        printArea.Visible = true;
    }

    private void BindReportGrid()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string ddlRegion = "0";
            string ddlDistrict = "0";
            string ddlBranch = Session["Depot_DepotID"].ToString();
            using (SqlCommand cmd = new SqlCommand("Get_Pending_Bill_Details_Report", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.AddWithValue("@RegionID", (ddlRegion != "0" && !string.IsNullOrEmpty(ddlRegion)) ? ddlRegion.Trim() : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DistrictID", (ddlDistrict != "0" && !string.IsNullOrEmpty(ddlDistrict)) ? ddlDistrict.Trim() : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BranchID", (ddlBranch != "0" && !string.IsNullOrEmpty(ddlBranch)) ? ddlBranch.Trim() : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GodownID", (ddlGodown.SelectedValue != "0" && !string.IsNullOrEmpty(ddlGodown.SelectedValue)) ? ddlGodown.SelectedValue.Trim() : (object)DBNull.Value);

                DataTable dtResult = new DataTable();
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dtResult);
                }

                gvDetails.DataSource = dtResult;
                gvDetails.DataBind();
            }
        }
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalBalance += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Balance") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "Balance") : 0);
            totalExpectedBills += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Expected_Bills_Till_July") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "Expected_Bills_Till_July") : 0);
            totalActualBills += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Actual_Bills_Generated") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "Actual_Bills_Generated") : 0);
            totalPendingBills += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending_Bills") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "Pending_Bills") : 0);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Sabhi footer cells ko pehle khali karein
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Text = "";
            }

            // Cell Index 1: Grand Total Label
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

            // Cell Index 8: Balance Total
            e.Row.Cells[7].Text = totalBalance.ToString("N5");
            e.Row.Cells[7].Font.Bold = true;
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            // Cell Index 9: Expected Bills Total
            e.Row.Cells[8].Text = totalExpectedBills.ToString();
            e.Row.Cells[8].Font.Bold = true;
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;

            // Cell Index 10: Actual Bills Total
            e.Row.Cells[9].Text = totalActualBills.ToString();
            e.Row.Cells[9].Font.Bold = true;
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;

            // Cell Index 11: Pending Bills Total
            e.Row.Cells[10].Text = totalPendingBills.ToString();
            e.Row.Cells[10].Font.Bold = true;
            e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
        }
    }

    #endregion

    #region Excel Export

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MPWLC_Pending_Bill_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvDetails.AllowPaging = false;
            BindReportGrid();

            string style = @"<style> 
                                table { border-collapse: collapse; width: 100%; }
                                th { background-color: #0b2545; color:#ffffff; border: 0.5pt solid #000000; font-weight: bold; text-align: center; } 
                                td { border: 0.5pt solid #000000; mso-number-format:'\@'; } 
                             </style>";
            Response.Write(style);

            gvDetails.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel rendering
    }

    #endregion

    #region Helper Functions

    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
        }
        return dt;
    }

    private DataTable GetDataWithCmd(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connStr))
        {
            cmd.Connection = con; // 1. Connection associate karta hai
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dt);     // 2. Query execute karke DataTable me data bharta hai
            }
        }
        return dt;
    }

    #endregion
}