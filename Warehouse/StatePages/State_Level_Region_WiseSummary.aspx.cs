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


public partial class IssueCenterLevel_Storage_State_Level_Region_WiseSummary : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
    int qtyTotal1 = 0;
    int qtyTotal2 = 0;
    int qtyTotal3 = 0;
    int qtyTotal4 = 0;
    int qtyTotal5 = 0;
    int qtyTotal6 = 0;
    //int qtyTotal7 = 0;
    //int qtyTotal8 = 0;
    //int qtyTotal9 = 0;
    int grQtyTotal1 = 0;
    int grQtyTotal2 = 0;
    int grQtyTotal3 = 0;
    int grQtyTotal4 = 0;
    int grQtyTotal5 = 0;
    int grQtyTotal6 = 0;
    //int grQtyTotal7 = 0;
    //int grQtyTotal8 = 0;
    //int grQtyTotal9 = 0;
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

        if (string.IsNullOrEmpty(Session["UserName"] as string))
        {
            Response.Redirect("~/login.aspx");
        }
        else if (Session["UserName"].ToString() == "MPSWLC")
        {

            if (!IsPostBack)
            {
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("MPWH_State_Level_Region_Wise_Summary", con))
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
                            GridView1.Caption = "State Level Region,District Wise Summary";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            GridView1.FooterRow.Style.Add("text-align", "center");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 3);
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
        //for (int i = GridView1.Rows.Count - 1; i > 0; i--)
        //{
        //    GridViewRow row = GridView1.Rows[i];
        //    GridViewRow previousRow = GridView1.Rows[i - 1];
        //    for (int j = 0; j < row.Cells.Count; j++)
        //    {
        //        if (row.Cells[j].Text == previousRow.Cells[j].Text)
        //        {
        //            if (previousRow.Cells[j].RowSpan == 0)
        //            {
        //                if (row.Cells[j].RowSpan == 0)
        //                {
        //                    previousRow.Cells[j].RowSpan += 2;
        //                }
        //                else
        //                {
        //                    previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
        //                }
        //                row.Cells[j].Visible = false;
        //            }
        //        }
        //    }
        //}
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Region_Id").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofGdn").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofBill").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NooffinalBill").ToString());
            int tmpTotal3 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofDSC").ToString());
            int tmpTotal4 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SubmitFB").ToString());
            int tmpTotal5 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofdscissuecentermanager").ToString());
            int tmpTotal6 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "noofdscbydistrictmanager").ToString());
            //int tmpTotal7 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofBillDSCForAD").ToString());
            //int tmpTotal8 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofBillDSCForGR").ToString());
            //int tmpTotal9 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "NoofBillDSCForHG").ToString());

            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            //qtyTotal7 += tmpTotal7;
            //qtyTotal8 += tmpTotal8;
            //qtyTotal9 += tmpTotal9;
            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            //grQtyTotal7 += tmpTotal7;
            //grQtyTotal8 += tmpTotal8;
            //grQtyTotal9 += tmpTotal9;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            Label lblTotalqty1 = (Label)e.Row.FindControl("lblTotalqty1");
            Label lblTotalqty2 = (Label)e.Row.FindControl("lblTotalqty2");
            Label lblTotalqty3 = (Label)e.Row.FindControl("lblTotalqty3");
            Label lblTotalqty4 = (Label)e.Row.FindControl("lblTotalqty4");
            Label lblTotalqty5 = (Label)e.Row.FindControl("lblTotalqty5");
            Label lblTotalqty6 = (Label)e.Row.FindControl("lblTotalqty6");
            //Label lblTotalqty7 = (Label)e.Row.FindControl("lblTotalqty7");
            //Label lblTotalqty8 = (Label)e.Row.FindControl("lblTotalqty8");
            //Label lblTotalqty9 = (Label)e.Row.FindControl("lblTotalqty9");

            lblTotalqty.Text = grQtyTotal.ToString();
            lblTotalqty1.Text = grQtyTotal1.ToString();
            lblTotalqty2.Text = grQtyTotal2.ToString();
            lblTotalqty3.Text = grQtyTotal3.ToString();
            lblTotalqty4.Text = grQtyTotal4.ToString();
            lblTotalqty5.Text = grQtyTotal5.ToString();
            lblTotalqty6.Text = grQtyTotal6.ToString();
            //lblTotalqty7.Text = grQtyTotal7.ToString();
            //lblTotalqty8.Text = grQtyTotal8.ToString();
            //lblTotalqty9.Text = grQtyTotal9.ToString();
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

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_Id") != null))
        {
            if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Region_Id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_Id") == null))
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
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
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

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal7.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal8.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal9.ToString();
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
            //qtyTotal7 = 0;
            //qtyTotal8 = 0;
            //qtyTotal9 = 0;
        }
    }
   
}