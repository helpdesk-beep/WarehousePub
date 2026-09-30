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

public partial class Inspections_Audit_Loss_Gain_Statement_for_Inspection : System.Web.UI.Page
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
            if (Session["role"] != null)
            {
                if (Session["Designation"].ToString() != "Account Auditor")
                {
                    Session.Abandon();
                    Response.Redirect("/Warehouse/Audit/View_Account_Audit.aspx");
                }
                else
                {
                    Fill_Godown();
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("/Warehouse/Inspections/Default.aspx");
            }
        }
    }


    public void Fill_Godown()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Details_Branch_wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranhcID", Session["hdnbranchid"].ToString());
            con.Open();
            ddlGodown.DataSource = cmd.ExecuteReader();
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Loss_Gain_Statement_for_Inspection", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtFDate.Text));
                cmd.Parameters.AddWithValue("@ToDate", getDate_MDY(txtTDate.Text));
                cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
                cmd.Parameters.AddWithValue("@GodownID", ddlGodown.SelectedValue);
                // cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue);
                // cmd.Parameters.AddWithValue("@HiredType", ddlhiredType.SelectedItem.ToString());
                // cmd.Parameters.AddWithValue("@StorageType", ddlstoragetype.SelectedItem.ToString());
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
                            // GridView1.Caption = @"<b style=""font-weight: bold;""> भण्डारण कमी / आधिक्य का मासिक पत्रक" + "</br> " + "From Date" + " - " + txtFDate.Text + "   -  " + "To Date" + " - " + txtTDate.Text + "</br> " +"Hired Type"+" - "+ddlhiredType.SelectedItem.ToString()+" , "+"Storage Type"+" - "+ddlstoragetype.SelectedItem.ToString();
                            GridView1.Columns[1].Visible = false;
                            submitbtn.Visible = true;
                            grddetails.Visible = true;
                            // GridView1.columns.RemoveAt(1);
                            //GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            //GridView1.FooterRow.Cells[3].Text = "Total";
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("OpeningBags")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OpeningQty")).ToString();
                            //opcloavg = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("OpeningQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("OpeningBags")).ToString());
                            //GridView1.FooterRow.Cells[6].Text = Math.Round(opcloavg, 2).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecQty")).ToString();

                            //decimal opcloavg2 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("RecQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString());
                            //GridView1.FooterRow.Cells[9].Text = Math.Round(opcloavg2, 2).ToString();

                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("OPRecBags")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OPRecQty")).ToString();

                            //decimal opcloavg3 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("OPRecQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("OPRecBags")).ToString());
                            //GridView1.FooterRow.Cells[12].Text = Math.Round(opcloavg3, 2).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CalculateWeight")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString();

                            // decimal opcloavg4 = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString())) / Convert.ToInt32(dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString());
                            // GridView1.FooterRow.Cells[15].Text = Math.Round(opcloavg4, 2).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceWeight")).ToString();
                            // GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CalculateWeight")).ToString();
                            // GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString();
                            // decimal LossPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("Loss")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString());
                            // GridView1.FooterRow.Cells[18].Text = Math.Round(LossPer, 2).ToString();

                            //GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain")).ToString();

                            // decimal gainper = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString());
                            // GridView1.FooterRow.Cells[20].Text = Math.Round(gainper, 2).ToString();

                            //GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BalanceBags")).ToString();
                            //GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceWeight")).ToString();
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);

                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            submitbtn.Visible = false;
                            grddetails.Visible = false;
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
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "BranchID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OpeningBags").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningQty").ToString());
            //decimal tmpTotal2 =Math.Round(Convert.ToDecimal(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningQty").ToString()))/ Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OpeningBags").ToString()),2);
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecBags").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecQty").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DelBags").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OPRecBags").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OPRecQty").ToString());
            // decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Loss").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DelBags").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DelQty").ToString());
            //decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BalanceWeight").ToString());
            decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CalculateWeight").ToString());
            decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Loss").ToString());
            decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Gain").ToString());
            //decimal tmpTotal17 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BalanceBags").ToString());
            //decimal tmpTotal18 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BalanceWeight").ToString());



            qtyTotal += tmpTotal;
            qtyTotal1 += tmpTotal1;
            //qtyTotal2 += 0;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += 0;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += 0;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += 0;
            qtyTotal12 += tmpTotal12;
            qtyTotal13 += tmpTotal13;
            qtyTotal14 += 0;
            //qtyTotal15 += tmpTotal15;
            //qtyTotal16 += 0;
            //qtyTotal17 += tmpTotal17;
            //qtyTotal18 += tmpTotal18;

            grQtyTotal += tmpTotal;
            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += 0;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += 0;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += 0;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += 0;
            grQtyTotal12 += tmpTotal12;
            grQtyTotal13 += tmpTotal13;
            grQtyTotal14 += 0;
            //grQtyTotal15 += tmpTotal15;
            //grQtyTotal16 += 0;
            //grQtyTotal17 += tmpTotal17;
            //grQtyTotal18 += tmpTotal18;


        }
        if (qtyTotal != 0)
            qtyTotal2 = Math.Round(qtyTotal1 / qtyTotal, 2);
        else
            qtyTotal12 = 0;
        if (qtyTotal3 != 0)
            qtyTotal5 = Math.Round(qtyTotal4 / qtyTotal3, 2);
        else
            qtyTotal5 = 0;
        if (qtyTotal6 != 0)
            qtyTotal8 = Math.Round(qtyTotal7 / qtyTotal6, 2);
        else
            qtyTotal8 = 0;

        if (qtyTotal9 != 0)
            qtyTotal11 = Math.Round(qtyTotal10 / qtyTotal9, 2);
        else
            qtyTotal11 = 0;
        //Loss Gain Percantage//
        if (qtyTotal10 != 0)
            qtyTotal14 = Convert.ToDecimal(Math.Round((qtyTotal13) * 100 / (qtyTotal10), 2));
        else
            qtyTotal14 = 0;

        if (qtyTotal10 != 0)
            qtyTotal16 = Math.Round(qtyTotal15 * 100 / qtyTotal10, 2);
        else
            qtyTotal16 = 0;


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
        cell.ColumnSpan = 3;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Opening Balance" + " - " + txtFDate.Text.ToString();
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Deposit" + " - " + txtFDate.Text.ToString() + " To " + txtTDate.Text.ToString();
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Opening + Deposit";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Actual Delivery" + " - " + txtFDate.Text.ToString() + " To " + txtTDate.Text.ToString();
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Avg Delivery Weight";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Loss";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Total Gain";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 2;
        //cell.Text = "Balance";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
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
            HeaderCell.Text = "Total";
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal13.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal14.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal15.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal16.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal17.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal18.ToString();
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
            qtyTotal10 = 0;
            qtyTotal11 = 0;
            qtyTotal12 = 0;
            qtyTotal13 = 0;
            qtyTotal14 = 0;
            //qtyTotal15 = 0;
            //qtyTotal16 = 0;
            //qtyTotal17 = 0;
            //qtyTotal18 = 0;


        }


    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnGodown_ID = row.FindControl("hdnGodown_ID") as HiddenField;
                HiddenField hdnCommodity_Id = row.FindControl("hdnCommodity_Id") as HiddenField;
                Label lblOpeningBags = row.FindControl("lblOpeningBags") as Label;
                Label lblOpeningQty = row.FindControl("lblOpeningQty") as Label;
                Label lblOPAverage = row.FindControl("lblOPAverage") as Label;
                Label lblRecBags = row.FindControl("lblRecBags") as Label;
                Label lblRecQty = row.FindControl("lblRecQty") as Label;
                Label lblRecAverage = row.FindControl("lblRecAverage") as Label;
                Label lblOPRecBags = row.FindControl("lblOPRecBags") as Label;
                Label lblOPRecQty = row.FindControl("lblOPRecQty") as Label;
                Label lblAverage = row.FindControl("lblAverage") as Label;
                Label lblDelBags = row.FindControl("lblDelBags") as Label;
                Label lblDelQty = row.FindControl("lblDelQty") as Label;
                Label lblDelAverage = row.FindControl("lblDelAverage") as Label;
                TextBox txtDOW = row.FindControl("txtDOW") as TextBox;
                TextBox txtLoss = row.FindControl("txtLoss") as TextBox;
                TextBox txtGain = row.FindControl("txtGain") as TextBox;
                //Label lblPrice = row.FindControl("lblPrice") as Label;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Loss_Gain_Statement_For_Inspection_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Godown_ID", hdnGodown_ID.Value.ToString());
                    cmd.Parameters.AddWithValue("@Employee_ID", Session["hdnEmployeeID"].ToString());
                    cmd.Parameters.AddWithValue("@Inspection_Quater", Session["hdninsptype"].ToString());
                    cmd.Parameters.AddWithValue("@Verification_Type", Session["hdnVerificationType"].ToString());
                    cmd.Parameters.AddWithValue("@Financial_Year", Session["hdnfinancialYear"].ToString());
                    cmd.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_Id.Value.ToString());
                    if (lblOpeningBags.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OpeningBags", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OpeningBags", lblOpeningBags.Text);
                    }
                    //==================
                    if (lblOpeningQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OpeningQty", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OpeningQty", lblOpeningQty.Text);
                    }

                    //cmd.Parameters.AddWithValue("@OpeningQty", lblOpeningQty.Text);
                    if (lblOPAverage.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OPAverage", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OPAverage", lblOPAverage.Text);
                    }
                    // cmd.Parameters.AddWithValue("@OPAverage", lblOPAverage.Text);
                    if (lblRecBags.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@RecBags", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RecBags", lblRecBags.Text);
                    }
                    //cmd.Parameters.AddWithValue("@RecBags", lblRecBags.Text);
                    if (lblRecQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@RecQty", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RecQty", lblRecQty.Text);
                    }
                    //cmd.Parameters.AddWithValue("@RecQty", lblRecQty.Text);
                    if (lblRecAverage.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@RecAverage", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RecAverage", lblRecAverage.Text);
                    }
                    //cmd.Parameters.AddWithValue("@RecAverage", lblRecAverage.Text);
                    if (lblOPRecBags.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OPRecBags", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OPRecBags", lblOPRecBags.Text);
                    }
                    //  cmd.Parameters.AddWithValue("@OPRecBags", lblOPRecBags.Text);
                    if (lblOPRecQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OPRecQty", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OPRecQty", lblOPRecQty.Text);
                    }
                    // cmd.Parameters.AddWithValue("@OPRecQty", lblOPRecQty.Text);
                    if (lblAverage.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@OPRecAverage", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OPRecAverage", lblAverage.Text);
                    }
                    //  cmd.Parameters.AddWithValue("@OPRecAverage", lblAverage.Text);
                    if (lblDelBags.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DelBags", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DelBags", lblDelBags.Text);
                    }
                    // cmd.Parameters.AddWithValue("@DelBags", lblDelBags.Text);
                    if (lblDelQty.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DelQty", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DelQty", lblDelQty.Text);
                    }
                    // cmd.Parameters.AddWithValue("@DelQty", lblDelQty.Text);
                    if (lblDelAverage.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DelAverage", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DelAverage", lblDelAverage.Text);
                    }
                    // cmd.Parameters.AddWithValue("@DelAverage", lblDelAverage.Text);

                    cmd.Parameters.AddWithValue("@Actual_Delivery_Weight", txtDOW.Text);
                    cmd.Parameters.AddWithValue("@Total_Loss", txtLoss.Text);
                    cmd.Parameters.AddWithValue("@Total_Gain", txtGain.Text);
                    cmd.Parameters.AddWithValue("@Inserted_By", ipAddress);
                    cmd.Parameters.AddWithValue("@From_Date", getDate_MDY(txtFDate.Text));
                    cmd.Parameters.AddWithValue("@To_Date", getDate_MDY(txtTDate.Text));
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Loss/ Gain Statement Inserted Successfully!');", true);
                        fillgrid();
                        submitbtn.Visible = false;
                        grddetails.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    string strMsg = ex.Message.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('');", true);
                    //Console.WriteLine(ex.Message);
                }
            }
        }
    }



}