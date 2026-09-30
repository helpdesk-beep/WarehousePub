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
public partial class SRV_Storage_Reports_Inspenctions_Rpt_Region_FY_DepositorWise_PaymentStatus_New : System.Web.UI.Page
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
            //fillDistrict();
            //fillBranch();
            fillDepositor();
            FillBillDetailsInGrid();
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
            using (SqlCommand cmd = new SqlCommand("usp_Get_DepositorWise_PaymentStatus_All", con))
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
                    //if (MainTable.Rows.Count > 0)
                    //{
                    //    GridView1.DataSource = MainTable;
                    //    GridView1.DataBind();


                    //    GridView1.FooterRow.Style.Add("text-align", "right");
                    //    GridView1.FooterRow.Cells[3].Text = "Grand Total";
                    //    GridView1.FooterRow.Cells[4].Text = MainTable.AsEnumerable().Sum(row => row.Field<int>("TotalBillPresentedinFY")).ToString();
                    //    GridView1.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("TotalBillAmountPresented")).ToString();
                    //    GridView1.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmountReceivedInFY")).ToString();
                    //    GridView1.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmountFromDepositor")).ToString();
                    //}
                    //else
                    //{
                    //    GridView1.DataSource = null;
                    //    GridView1.DataBind();
                    //}


                    if (MainTable.Rows.Count > 0)
                    {
                        GridView1.DataSource = MainTable;
                        GridView1.DataBind();

                        // 🔥 Grand Total TOP
                        pnlGrandTotal.Visible = true;

                        lblGTBills.Text =
                            MainTable.AsEnumerable().Sum(r => r.Field<int>("TotalBillPresentedinFY")).ToString();

                        lblGTBillAmt.Text =
                            MainTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalBillAmountPresented")).ToString("N2");

                        lblGTReceived.Text =
                            MainTable.AsEnumerable().Sum(r => r.Field<decimal>("TotalAmountReceivedInFY")).ToString("N2");

                        lblGTRemaining.Text =
                            MainTable.AsEnumerable().Sum(r => r.Field<decimal>("RemainingAmountFromDepositor")).ToString("N2");
                    }
                    else
                    {
                        pnlGrandTotal.Visible = false;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }


                }
            }
        }
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
            HeaderCell.ColumnSpan = 4;
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


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}