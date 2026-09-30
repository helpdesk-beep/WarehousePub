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

public partial class StatePages_Rpt_Prapatra : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;

    int qtyTotal = 0;
    int grQtyTotal = 0;
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

    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRegion();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }
     protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {       
        FillGrid();
    }

    
    private void FillGrid()
    {
        SqlCommand cmdd = new SqlCommand("Rpt_Paddy_Stored_JVS_Godown", con);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Regionid", ddlregion.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Columns[1].Visible = false;
            GridView1.Caption = @"<b style=""font-weight: bold;""> मध्यप्रदेश वेअरहाउससिंग ए ड लॉजिस्टिक्स कार्पोरेशन, क्षेत्रीय कार्यालय" +' '+ ddlregion.SelectedItem + "</br> " + "निगम की स्वयं निर्मित/ JVS एवं गोदाम में भंडारित धान की कुल क्षमता एवं भंडारित मात्रा की जानकारी दिनांक 22.11.2021 की स्थिति [M.T] में";
            GridView1.FooterRow.Style.Add("text-align", "center");
            GridView1.FooterRow.Cells[3].Text = "Total";

            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Sacks_201819")).ToString();
            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_of_Bags_201819")).ToString();
            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Stored_Quantity_201819")).ToString();
            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Sacks_201920")).ToString();
            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_of_Bags_201920")).ToString();
            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Stored_Quantity_201920")).ToString();
            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Sacks_202021")).ToString();
            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_of_Bags_202021")).ToString();
            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Stored_Quantity_202021")).ToString();
            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Balance")).ToString();
            ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }


    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "BranchID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Sacks_201819").ToString());
            decimal tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_of_Bags_201819").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Stored_Quantity_201819").ToString());
            decimal tmpTotal3 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Sacks_201920").ToString());
            decimal tmpTotal4 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_of_Bags_201920").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Stored_Quantity_201920").ToString());
            decimal tmpTotal6 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Sacks_202021").ToString());
            decimal tmpTotal7 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_of_Bags_202021").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Stored_Quantity_202021").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Balance").ToString());
           
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
        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell1 = new TableHeaderCell();

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "";
        //row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "";
        //row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "वर्ष 2018-19";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "वर्ष 2019-20";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "वर्ष 2020-21";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "Total Balance (7+10+13)";
        row1.Controls.Add(cell1);


        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row1);

        GridViewRow row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Branch Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Godown Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Depositer Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "स्टेको की संख्या";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "बोरे";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "भंडारित मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "स्टेको की संख्या";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "बोरे";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "भंडारित मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "स्टेको की संख्या";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "बोरे";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "भंडारित मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Balances (7+10+13)";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for Payment";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(1, row);



    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "BranchID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "BranchID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "BranchID") == null))
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

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal12.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

           
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
}