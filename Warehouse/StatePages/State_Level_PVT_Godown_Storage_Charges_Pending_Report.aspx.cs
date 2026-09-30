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

public partial class IssueCenterLevel_State_Level_PVT_Godown_Storage_Charges_Pending_Report : System.Web.UI.Page
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
    decimal qtyTotal9 = 0;
    decimal qtyTotal10 = 0;
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;

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

        //if (string.IsNullOrEmpty(Session["UserName"] as string))
        //{
        //    Response.Redirect("~/login.aspx");
        //}
        //else if (Session["UserName"].ToString() == "MPSWLC")
        //{

        if (!IsPostBack)
        {
            fillgrid();
        }
        //}
        //else
        //{
        //    Response.Redirect("~/login.aspx");
        //}
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_PVT_Godown_Storage_Charges_Panding_Report", con))
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "PVT Godown Storage Charges Pending Report (at ICM,MPSCSC)" + "</b> ";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Cells[2].Text = "Total";
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 3);
                            GridView1.FooterRow.Style.Add("text-align", "center");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmounts")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BMsDSCatBills")).ToString();
                            GridView1.FooterRow.Cells[7].Text =dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountafteDSC")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SubmittoICM")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountofBillsatICM")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ICMsDSCatBill")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmountafterDSC")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pnoofbill")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pbillamount")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pdscatbill")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pamountofdsc")).ToString();
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
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofgdwn").ToString());

            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBill").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BillAmounts").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BMsDSCatBills").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmountafteDSC").ToString());

            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SubmittoICM").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmountofBillsatICM").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ICMsDSCatBill").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmountafterDSC").ToString());

            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pnoofbill").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pbillamount").ToString());
            decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pdscatbill").ToString());
            decimal tmpTotal12 =Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Pamountofdsc").ToString());




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
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += tmpTotal11;
            qtyTotal12 += tmpTotal12;

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
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += tmpTotal11;
            grQtyTotal12 += tmpTotal12;

        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
        //    Label lblTotalqty1 = (Label)e.Row.FindControl("lblTotalqty1");
        //    Label lblTotalqty2 = (Label)e.Row.FindControl("lblTotalqty2");
        //    Label lblTotalqty3 = (Label)e.Row.FindControl("lblTotalqty3");
        //    Label lblTotalqty4 = (Label)e.Row.FindControl("lblTotalqty4");
        //    Label lblTotalqty5 = (Label)e.Row.FindControl("lblTotalqty5");
        //    Label lblTotalqty6 = (Label)e.Row.FindControl("lblTotalqty6");
        //    Label lblTotalqty7 = (Label)e.Row.FindControl("lblTotalqty7");
        //    Label lblTotalqty8 = (Label)e.Row.FindControl("lblTotalqty8");
        //    Label lblTotalqty9 = (Label)e.Row.FindControl("lblTotalqty9");
        //    Label lblTotalqty10 = (Label)e.Row.FindControl("lblTotalqty10");
        //    Label lblTotalqty11 = (Label)e.Row.FindControl("lblTotalqty11");
        //    Label lblTotalqty12 = (Label)e.Row.FindControl("lblTotalqty12");

        //    lblTotalqty.Text = grQtyTotal.ToString();
        //    lblTotalqty1.Text = grQtyTotal1.ToString();
        //    lblTotalqty2.Text = grQtyTotal2.ToString();
        //    lblTotalqty3.Text = grQtyTotal3.ToString();
        //    lblTotalqty4.Text = grQtyTotal4.ToString();
        //    lblTotalqty5.Text = grQtyTotal5.ToString();
        //    lblTotalqty6.Text = grQtyTotal6.ToString();
        //    lblTotalqty7.Text = grQtyTotal7.ToString();
        //    lblTotalqty8.Text = grQtyTotal8.ToString();
        //    lblTotalqty9.Text = grQtyTotal9.ToString();
        //    lblTotalqty10.Text = grQtyTotal10.ToString();
        //    lblTotalqty11.Text = grQtyTotal11.ToString();
        //    lblTotalqty12.Text = grQtyTotal12.ToString();

        //}
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
        cell.ColumnSpan = 1;
        cell.Text = "";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 4;
        cell.Text = "Bills Generated by BM MPWLC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 4;
        cell.Text = "Verified by MPSCSC Issue Center Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 4;
        cell.Text = "Bills Pending from BM,MPWLC to ICM";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;
       
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal10.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal11.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal12.ToString();
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
            qtyTotal10 = 0;
            qtyTotal11 = 0;
            qtyTotal12 = 0;

        }
       

    }
}