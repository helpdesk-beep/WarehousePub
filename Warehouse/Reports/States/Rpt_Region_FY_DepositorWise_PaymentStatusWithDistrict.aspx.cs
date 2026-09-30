using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_State_Rpt_Region_FY_DepositorWise_PaymentStatusWithDistrict : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
    int SubTotal = 0;
    int GrandTotal = 0;
    int varTotalBills = 0;
    decimal varTotalBillAmountPresented = 0;
    decimal varTotalAmountReceivedInFY = 0;
    decimal varRemainingAmountFromDepositor = 0;
    //int rowIndex = 1;
    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
    int qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;

    int grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;

    //int storid = 0;
    string storid = "0";
    int rowIndex = 1;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillRegion();
            fillDistrict();
            fillBranch();
            fillDepositor();
            //FillBillDetailsInGrid();
        }

    }

    private void fillDepositor()
    {
        string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.Items.Clear();
            ddlDepositor.DataSource = ds.Tables[0];
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
        }
    }

    private void fillRegion()
    {
        string query = "select distinct Region_ID,Regionnm from tbl_MetaData_district";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "--Select--");
        }

    }

    private void fillDistrict()
    {
        //string RegionID = Session["Region_ID"].ToString();
        string query = "select district_id,DIstrict_Name from tbl_MetaData_district where Region_ID ='" + ddlRegion.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "DIstrict_Name";
            ddldistrict.DataValueField = "district_id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }

    }

    private void fillBranch()
    {

        string query = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId ='" + ddldistrict.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }

    }

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string vRegion = string.Empty;
        string vDistrict = string.Empty;
        string vBranch = string.Empty;
        string vGodown = string.Empty;
        string vGodownType = string.Empty;
        string vDepositor = string.Empty;
        string vCommodity = string.Empty;
        string vCropYear = string.Empty;
        decimal subtotal = 0;

        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_DepositorWise_PaymentStatus_All_DistrictWise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlRegion.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@RegionID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                }
                if (ddldistrict.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DistrictID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue);
                }

                if (ddlbranch.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@BranchID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                }

                if (ddlDepositor.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DepositorID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue);
                }

                if (ddlFinancialYear.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", ddlFinancialYear.SelectedValue);
                }

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];
                    if (MainTable.Rows.Count > 0)
                    {
                        GV_FYDepositorWisePayment.DataSource = MainTable;
                        GV_FYDepositorWisePayment.DataBind();

                        //foreach (GridViewRow row in GV_FYDepositorWisePayment.Rows)
                        //{
                        //    if (row.RowType == DataControlRowType.DataRow)
                        //    {
                        //        int quantity = Convert.ToInt32(row.Cells[1].Text); // Quantity column
                        //        decimal price = Convert.ToDecimal(row.Cells[2].Text); // Price column

                        //        // Calculate row total
                        //        decimal rowTotal = quantity * price;
                        //        subtotal += rowTotal; // Accumulate subtotal
                        //    }
                        //}


                        GV_FYDepositorWisePayment.FooterRow.Style.Add("text-align", "right");
                        GV_FYDepositorWisePayment.FooterRow.Cells[5].Text = "Grand Total";
                        GV_FYDepositorWisePayment.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<int>("TotalBillPresentedinFY")).ToString();
                        GV_FYDepositorWisePayment.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("TotalBillAmountPresented")).ToString();
                        GV_FYDepositorWisePayment.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmountReceivedInFY")).ToString();
                        GV_FYDepositorWisePayment.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmountFromDepositor")).ToString();
                        //ShowingGroupingDataInGridView(GV_FYDepositorWisePayment.Rows, 0, 2);
                    }
                    else
                    {
                        GV_FYDepositorWisePayment.DataSource = null;
                        GV_FYDepositorWisePayment.DataBind();
                    }

                }
            }
        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_FYDepositorWisePayment_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    varTotalBills = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalBillPresentedinFY").ToString());
        //    varTotalBillAmountPresented = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBillAmountPresented").ToString());
        //    varTotalAmountReceivedInFY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmountReceivedInFY").ToString());
        //    varRemainingAmountFromDepositor = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RemainingAmountFromDepositor").ToString());
        //    int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SubTotal").ToString());
        //    SubTotal += tmpTotal;
        //    GrandTotal += tmpTotal;
        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
        //    lblTotalqty.Text = GrandTotal.ToString();
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RegionName").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalBillPresentedinFY").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBillAmountPresented").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmountReceivedInFY").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RemainingAmountFromDepositor").ToString());
            //decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
           
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
           

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }

    //int currentid = 0;
    //decimal subTotal = 0;
    //decimal total = 0;
    //int subTotalRowIndex = 0;
    //protected void GV_FYDepositorWisePayment_OnRowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    subTotal = 0;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
    //        String currentid = Convert.ToString(dt.Rows[e.Row.RowIndex]["FinancialYear"]);
    //        total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["TotalBillPresentedinFY"]);
    //        //if (Catagory != currentid)
    //        //{
    //        //    if (e.Row.RowIndex > 0)
    //        //    {
    //        //        for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
    //        //        {
    //        //            subTotal += Convert.ToDecimal(GV_FYDepositorWisePayment.Rows[i].Cells[2].Text);
    //        //        }
    //        //        this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
    //        //        subTotalRowIndex = e.Row.RowIndex;
    //        //    }
    //        //    currentid = Catagory;
    //        //}
    //    }

    //}

    //private void AddTotalRow(string p1, string p2)
    //{
    //    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
    //    row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
    //    row.Cells.AddRange(new TableCell[3] { new TableCell (), //Empty Cell
    //                                new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
    //                                new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

    //    GV_FYDepositorWisePayment.Controls[0].Controls.Add(row);
    //}

    //protected void GV_FYDepositorWisePayment_OnDataBound(object sender, EventArgs e)
    //{
    //    for (int i = subTotalRowIndex; i < GV_FYDepositorWisePayment.Rows.Count; i++)
    //    {
    //        subTotal += Convert.ToDecimal(GV_FYDepositorWisePayment.Rows[i].Cells[2].Text);
    //    }
    //    this.AddTotalRow("Sub Total", subTotal.ToString("N2"));
    //    this.AddTotalRow("Total", total.ToString("N2"));
    //}


    protected void GV_FYDepositorWisePayment_OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 10;
        cell.Text = "Depositor Wise Payment Status";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //GV_FYDepositorWisePayment.HeaderRow.Parent.Controls.AddAt(0, row);
    }


    public void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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


    protected void GV_FYDepositorWisePayment_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "RegionName") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "RegionName").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "RegionName") == null))
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
            HeaderCell.ColumnSpan = 6;
            NewTotalRow.Cells.Add(HeaderCell);

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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
           
        }


    }

}