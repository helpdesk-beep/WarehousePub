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

public partial class StatePages_Rpt_Commodity_wise_Stock_Position : System.Web.UI.Page
{


    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;

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
            //labelName.Text = DateTime.Now.ToString();
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Stock_Position_Data", con))
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
                            //GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "District Wise Stock Position as on Date" + "  -   " + dt.Rows[0]["StockPositionAsOnDate"].ToString(); ;//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "District Wise Stock Position" ;//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            //GridView1.Columns[1].Visible = false;
                            //// GridView1.columns.RemoveAt(1);
                            //lblsyncdate.Text = dt.Rows[0]["StockPositionAsOnDate"].ToString();

                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;color:red;", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBalance")).ToString();

                        }
                        else
                        {
                            // btnUpdate.Visible = false;
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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    string Averg = DataBinder.Eval(e.Row.DataItem, "statuswhr").ToString();
        //    if (Averg == "Red")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8C5BA");
        //        e.Row.Font.Bold = true;
        //    }
        //    else if (Averg == "Yellow")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FAF8C1");
        //        e.Row.Font.Bold = true;
        //    }
        //    else if (Averg == "Grean")
        //    {
        //        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5EC92");
        //        e.Row.Font.Bold = true;
        //    }
        //}
    }
   
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //bool newRow = false;

        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        //{
        //    if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
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
        //    HeaderCell.ColumnSpan = 3;

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


        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
        //    rowIndex++;
        //    qtyTotal1 = 0;
        //    qtyTotal2 = 0;
        //    qtyTotal3 = 0;

        //}


    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://mpwarehousing.mp.gov.in/");
    }

}