using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Check_Godown_Status_For_All_New : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;

    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //getdistrict();
            //filldepositer();
            //fillScheduleInsp_Grid();
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("Godown_Check", con))
            //using (SqlCommand cmd = new SqlCommand("Godown_Check_03Jan2024", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGodownID.Text);
                //cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    //using (DataTable dt = new DataTable())
                    //{
                    //sda.Fill(dt);
                    sda.Fill(ds);
                    DataTable tableA = ds.Tables[0];
                    DataTable tableB = ds.Tables[1];
                    if (tableA.Rows.Count > 0)
                    {
                        Depositor_Gridview.DataSource = tableA;
                        Depositor_Gridview.DataBind();
                        GV_Capacity.DataSource = tableB;
                        GV_Capacity.DataBind();
                        GV_Capacity.FooterRow.Style.Add("text-align", "Center");
                        GV_Capacity.FooterRow.Cells[4].Text = "Total";
                        GV_Capacity.FooterRow.Cells[5].Text = tableB.AsEnumerable().Sum(row => row.Field<decimal>("WHR_Quantity")).ToString();
                    }
                    else
                    {
                        Depositor_Gridview.DataSource = null;
                        Depositor_Gridview.DataBind();
                        GV_Capacity.DataSource = null;
                        GV_Capacity.DataBind();
                    }

                }
            }
        }
    }
    public void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Payment_Status_by_Godown_ID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godownid", txtGodownID.Text.Trim());
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Godown Wise Payment Status" + "</b> ";
                GrdGodownBill.DataSource = dt;
                GrdGodownBill.DataBind();
                GrdGodownBill.Visible = true;
                GrdGodownBill.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                GrdGodownBill.FooterRow.Cells[4].Text = "Total";
                GrdGodownBill.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalStorageBill")).ToString();
                GrdGodownBill.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Amount")).ToString();
                GrdGodownBill.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalSubmittedBill")).ToString();
                GrdGodownBill.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("SubmittedBillAmount")).ToString();
                GrdGodownBill.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingForSubmission")).ToString();
                GrdGodownBill.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingForSubmissionAmount")).ToString();

                GrdGodownBill.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalBillReceivedPayment")).ToString();
                GrdGodownBill.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                GrdGodownBill.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                GrdGodownBill.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("OtherDeduction")).ToString();
                GrdGodownBill.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Payable_Amount")).ToString();
                GrdGodownBill.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("NoofBillPendingatMPSCSC")).ToString();
                GrdGodownBill.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("NoofBillAmountPendingatMPSCSC")).ToString();

                GrdGodownBill.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("TotalRentBill")).ToString();
                GrdGodownBill.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("RentBillAmt")).ToString();

                GrdGodownBill.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("ReceivedRentBill")).ToString();
                GrdGodownBill.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("ReceivedRentBillAmount")).ToString();

                //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total")).ToString();
                GrdGodownBill.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PayBilltoGodownOwner")).ToString();
                GrdGodownBill.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PaytoGodownOwner")).ToString();

                GrdGodownBill.FooterRow.Cells[24].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("PendingBillatMPWLC")).ToString();
                GrdGodownBill.FooterRow.Cells[25].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("PendingBillAmountatMPWLC")).ToString();
            }
            else
            {
                GrdGodownBill.DataSource = null;
                GrdGodownBill.DataBind();
            }
        }
    }
    protected void fillStockPosition()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Stock_position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", txtGodownID.Text);
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
                            GridView1.Columns[1].Visible = false;
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[4].Text = "Total";
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Qty_Received")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("DeliveryWeight")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("AvailableQty")).ToString();
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }


    protected void btnCheck_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        fillStockPosition();
        fillgrid();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "GodownID").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Qty_Received").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DeliveryWeight").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AvailableQty").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(tmpTotal1.ToString())*100/ Convert.ToDecimal(tmpTotal2.ToString());


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
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

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "GodownID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "GodownID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "GodownID") == null))
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
            HeaderCell.ColumnSpan = 5;

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

        }


    }
    protected void GrdGodownBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //Session["GodownID"] = (row.RowIndex).ToString();
            string Godownid = Convert.ToString(e.CommandArgument);
            Session["GodownID"] = Godownid.ToString();
            // Label Billnumber = (Label)row.FindControl("lblBill_Number");
            string url = "Rpt_Godown_Bill_Wise_Payment_Status.aspx?BN=" + (Session["GodownID"].ToString());
            string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }

}