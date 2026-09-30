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

public partial class Reports_State_Rpt_Region_Wise_Stock_Position_Fill_Detail_By_BM : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;

    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;
    decimal qtyTotal7 = 0;
    decimal qtyTotal8 = 0;
    decimal qtyTotal9 = 0;
    decimal qtyTotal10 = 0;
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;
    decimal qtyTotal13 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;
    decimal qtyTotal17 = 0;
    decimal qtyTotal18 = 0;
    decimal qtyTotal19 = 0;
    decimal qtyTotal20 = 0;
    decimal qtyTotal21 = 0;
    decimal qtyTotal22 = 0;
    decimal qtyTotal23 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
    decimal grQtyTotal10 = 0;
    decimal grQtyTotal11 = 0;
    decimal grQtyTotal12 = 0;
    decimal grQtyTotal13 = 0;
    decimal grQtyTotal14 = 0;
    decimal grQtyTotal15 = 0;
    decimal grQtyTotal16 = 0;
    decimal grQtyTotal17 = 0;
    decimal grQtyTotal18 = 0;
    decimal grQtyTotal19 = 0;
    decimal grQtyTotal20 = 0;
    decimal grQtyTotal21 = 0;
    decimal grQtyTotal22 = 0;
    decimal grQtyTotal23 = 0;

    int storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillRegion();
            //fillMonth();
            //fillgrid();
            fillComodity();
            fillCropYear();
            fillgrid();
        }
    }
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
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
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, "--Select--");
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
    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select distinct CropYear from tbl_storage_Depositor_WHR_Relation where CropYear in('2017-18','2018-19','2019-20','2020-21','2021-22','2022-23','2023-24','2024-25')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlddlCropYear.Items.Clear();
                ddlddlCropYear.DataSource = ds.Tables[0];
                ddlddlCropYear.DataTextField = "CropYear";
                ddlddlCropYear.DataValueField = "CropYear";
                ddlddlCropYear.DataBind();
                ddlddlCropYear.Items.Insert(0, "--Select--");
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
        
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_Stock_Position_Fill_Detail_By_BM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlcommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CommodityID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue);
                }
                if (ddlddlCropYear.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYear", ddlddlCropYear.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "गोदामो में भण्‍डारित स्‍कंध की अवधि अनुसार जानकारी (in MT)" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Columns[1].Visible = false;
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Grand Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn6Months")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn9Months")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn12Months")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn18Months")).ToString();

                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn24Months")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn30Months")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionIn36Months")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionFiveYear")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StockPositionGreaterthanFiveYear")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 3);
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RegionID").ToString());
            // Label lblPendingNoOfGenerateBill = (Label)e.Row.FindControl("lblPendingNoOfGenerateBill");
            decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn6Months").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn9Months").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn12Months").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn18Months").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn24Months").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn30Months").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionIn36Months").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionFiveYear").ToString());
            // decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "StockPositionGreaterthanFiveYear").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());          
            //HiddenField hdnBranch_Id = (HiddenField)e.Row.FindControl("hdnBranch_Id");

            //if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill")) > 0)
            //{
            //    lblPendingNoOfGenerateBill.Text = "<a  href='Rpt_Pending_Bill_Details.aspx?ID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill") + "</a>";
            //}


            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            
            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            


        }

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
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);



        cell = new TableHeaderCell();
        cell.ColumnSpan = 10;
        cell.Text = "भंडारित अवधि";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "RegionID") != null))
        {
            if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RegionID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "RegionID") == null))
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
            HeaderCell.ColumnSpan = 2;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal.ToString();
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal6.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal7.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal8.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal9.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;
            qtyTotal6 = 0;
            qtyTotal7 = 0;
            qtyTotal8 = 0;
            qtyTotal9 = 0;
           

        }


    }
    //protected void btnback_Click(object sender, EventArgs e)
    //{

    //}

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlddlCropYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}