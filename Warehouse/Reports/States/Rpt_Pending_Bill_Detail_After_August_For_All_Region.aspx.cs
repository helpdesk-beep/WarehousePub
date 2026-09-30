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

public partial class Reports_States_Rpt_Pending_Bill_Detail_After_August_For_All_Region : System.Web.UI.Page
{
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

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;

    int storid = 0;
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
            fillgrid();
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Detail_After_August_For_All_Region", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            GridView1.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill(After August)";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[0].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingNooGodown")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingNoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountafterRMDSC")).ToString();
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GridView1.DataSource = dt;
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
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofgdwn").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PendingNooGodown").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGenerateBill").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfDSCSingBill").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSUBBill").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfSignBill_CSMS").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfGdwnRM").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfRMDSC").ToString());



            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;

            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;


        }

    }
   
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 5;
        row.Controls.Add(cell);



        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "By MPWLC BM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "By MPSCSC ICM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "BY MPWLC RM";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //bool newRow = false;

        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        //{
        //    if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
        //        newRow = true;
        //}
        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") == null))
        //{
        //    newRow = true;
        //    rowIndex = 0;
        //}
        //if (newRow)
        //{
        //    GridView GridView1 = (GridView)sender;
        //    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //    NewTotalRow.Font.Bold = true;
        //    // NewTotalRow.BackColor = System.Drawing.Color.Gray;
        //    NewTotalRow.ForeColor = System.Drawing.Color.Black;
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Sub Total";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.ColumnSpan = 2;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 3;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal1.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 4;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal2.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal3.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal4.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal5.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal6.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal7.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal8.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
        //    rowIndex++;
        //    qtyTotal = 0;
        //    qtyTotal1 = 0;
        //    qtyTotal2 = 0;
        //    qtyTotal3 = 0;
        //    qtyTotal4 = 0;
        //    qtyTotal5 = 0;
        //    qtyTotal6 = 0;
        //    qtyTotal7 = 0;
        //    qtyTotal8 = 0;


        //}


    }
}