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

public partial class Reports_States_Rpt_Hired_Type_WHR_Acceptance_Qty : System.Web.UI.Page
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
    decimal qtyTotal19 = 0;
    decimal qtyTotal20 = 0;
    decimal qtyTotal21 = 0;
    decimal qtyTotal22 = 0;
    decimal qtyTotal23 = 0;
    decimal qtyTotal24 = 0;
    decimal qtyTotal25 = 0;
    decimal qtyTotal26 = 0;
    decimal qtyTotal27 = 0;
    decimal qtyTotal28 = 0;
    decimal qtyTotal29 = 0;
    decimal qtyTotal30 = 0;

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
    decimal grQtyTotal24 = 0;
    decimal grQtyTotal25 = 0;
    decimal grQtyTotal26 = 0;
    decimal grQtyTotal27 = 0;
    decimal grQtyTotal28 = 0;
    decimal grQtyTotal29 = 0;
    decimal grQtyTotal30 = 0;

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
        Decimal SWCPer = 0, CWCPer = 0, MarkfedPer = 0, TotalPer = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Hired_Type_WHR_Acceptance_Qty]", con))
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Agency Wise WHR as on " + DateTime.Now.ToString() + "</b> <span style='text-align:right;width:100%;'>Qty in MT</span>";

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SWCAcceptQty")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SWCWHRQty")).ToString();
                            SWCPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("SWCWHRQty")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("SWCAcceptQty")).ToString());
                            GridView1.FooterRow.Cells[4].Text = Math.Round(SWCPer, 2).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CWCAcceptQty")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CWCWHRQty")).ToString();
                            CWCPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("CWCWHRQty")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("CWCAcceptQty")).ToString());
                            GridView1.FooterRow.Cells[7].Text = Math.Round(CWCPer, 2).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MarkfedAcceptQty")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MarkfedWHRQty")).ToString();
                            MarkfedPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("MarkfedWHRQty")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("MarkfedAcceptQty")).ToString());
                            GridView1.FooterRow.Cells[10].Text = Math.Round(MarkfedPer, 2).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptance")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWhr")).ToString();
                            TotalPer = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWhr")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptance")).ToString());
                            GridView1.FooterRow.Cells[13].Text = Math.Round(TotalPer, 2).ToString();

                            //ShowingGroupingDataInGridView(GridView1.Rows, 0, 3);
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
            //storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SWCAcceptQty").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SWCWHRQty").ToString());
            // decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfDSCSingBill").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CWCAcceptQty").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CWCWHRQty").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BilldiffAmt").ToString());

            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MarkfedAcceptQty").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MarkfedWHRQty").ToString());
            // decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DSCsignedbyBM").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAcceptance").ToString());
            decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWhr").ToString());
            //decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofCSMS_BillAmt").ToString());





            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            //qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            //qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += tmpTotal11;



            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            //grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            //grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += tmpTotal11;
            //grQtyTotal12 += tmpTotal12;


        }

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell1 = new TableHeaderCell();

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.RowSpan = 1;
        cell1.Text = "S. No.";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.RowSpan = 1;
        cell1.Text = "District";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "SWC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "CWC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "Markfed";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 3;
        cell1.Text = "Total";
        row1.Controls.Add(cell1);

        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row1);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
}