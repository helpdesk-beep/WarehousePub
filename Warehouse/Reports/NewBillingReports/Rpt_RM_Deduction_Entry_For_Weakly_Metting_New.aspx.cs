using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class Reports_NewBillingReports_Rpt_RM_Deduction_Entry_For_Weakly_Metting_New : System.Web.UI.Page
{
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    long storid = 0;
    int rowIndex = 1;
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            fillgrid();
            fillFinasncialYear();
            FillCommodity();
        }
    }
    private void fillFinasncialYear()
    {
        try
        {
            string query = "";

            query = "select distinct CropYear from Branch_Godown_Wise_Pending_Bill_Details where startdate<Issue_Date";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlfinancial.Items.Clear();
                ddlfinancial.DataSource = ds.Tables[0];
                ddlfinancial.DataTextField = "CropYear";
                ddlfinancial.DataValueField = "CropYear";
                ddlfinancial.DataBind();
                ddlfinancial.Items.Insert(0, "Select");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void FillCommodity()
    {
        try
        {
            string query = "";
            query = "Select Distinct GD.Commodity_ID,SC.Commodity_Name From tbl_RM_Gail_Deduction GD Inner join tbl_MetaData_STORAGE_COMMODITY SC On GD.Commodity_ID=Sc.Commodity_Id";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodity.Items.Clear();
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_ID";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, "Select");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void fillgrid()
    {
        try
        {
            Decimal opcloavg = 0;
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_RM_Deduction_Entry_For_Weakly_Metting", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Crop Year parameter
                    if (ddlfinancial.SelectedValue == "Select")
                        cmd.Parameters.AddWithValue("@CropYEar", "0");
                    else
                        cmd.Parameters.AddWithValue("@CropYEar", ddlfinancial.SelectedValue);

                    // Commodity parameter
                    if (ddlcommodity.SelectedValue == "Select")
                        cmd.Parameters.AddWithValue("@Commodity_id", "0");
                    else
                        cmd.Parameters.AddWithValue("@Commodity_id", ddlcommodity.SelectedValue);

                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;

                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                GridView1.DataSource = dt;
                                GridView1.DataBind();

                                // Header / Caption
                                GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION</b><br/>" +
                                                    "क्षेत्रीय कार्यालय स्तर से 1% आधिक्य के विरुद्ध गोदाम संचालको के देयकों से 20% रोकी गई राशि की जानकारी अपडेट करना |";

                                // Hide Region_ID column
                                GridView1.Columns[1].Visible = false;

                                // Footer totals
                                GridView1.FooterRow.Cells[3].Text = "Total";
                                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DeductionAmount")).ToString("N2");
                                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount")).ToString("N2");
                                GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Amount_DALG")).ToString("N2");
                                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other_Deduction")).ToString("N2");

                                // Optional grouping logic
                                ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                            }
                            else
                            {
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            // Database specific error
            lblMsg.Text = "Database error: " + sqlEx.Message;
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
        catch (Exception ex)
        {
            // General error
            lblMsg.Text = "An error occurred: " + ex.Message;
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeductionAmount").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount_DALG").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Other_Deduction").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(tmpTotal1.ToString())*100/ Convert.ToDecimal(tmpTotal2.ToString());


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += 0;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += 0;
        }
        //if (qtyTotal1 != 0)
        //    qtyTotal3 = Math.Round(qtyTotal2 * 100 / qtyTotal1, 2);
        //else
        //    qtyTotal3 = 0;

    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 5;
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;

        }


    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }

    protected void ddlfinancial_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
