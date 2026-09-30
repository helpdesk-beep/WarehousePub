using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Region_States_Rpt_Get_Except_Cap : System.Web.UI.Page
{
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
   

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
   

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
            FillCommodity();
            //fillgrid();
        }
        
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Rpt_Get_Except_Cap", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Commodity_ID", ddlcommodity.SelectedValue);
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
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">Region Wise "+ "</br> " + "Payment Received Details From MPSCSC " + "</br> ";
                            GridView1.Columns[1].Visible = false;
                            GridView1.Columns.RemoveAt(1);

                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Cells[2].Text = "Total";
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentReceivedTilldecember")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalPayableAmount")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalPayableAmountToday")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();

                            //ShowingGroupingDataInGridView(GridView1.Rows, 0, 3);
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
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District").ToString());
            //decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "District_Name").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Branch").ToString());
          
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DepotName").ToString());
            //decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "District").ToString());
            //decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Branch").ToString());
           



            //qtyTotal += tmpTotal;
            //qtyTotal1 += tmpTotal1;
           
            //qtyTotal6 += tmpTotal6;
            //qtyTotal7 += tmpTotal7;
            //qtyTotal8 += tmpTotal8;
            
            //grQtyTotal += tmpTotal;
            //grQtyTotal1 += tmpTotal1;
          
            //grQtyTotal6 += tmpTotal6;
            //grQtyTotal7 += tmpTotal7;
            //grQtyTotal8 += tmpTotal8;
           


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
       
        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "District";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Branch";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "2018-19";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "2019-20";
        row.Controls.Add(cell);
        
        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "2020-21";
        //cell.Text = @"<b style=""font-weight: bold; color:white;"">MPSCSC से दिनांक " + "</br> " + DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + "</br> "+ "तक प्राप्त राशि रूपये में" +"</br> ";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "आज दिनांक"+ "</br> " + DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy") + "</br> " + "को प्राप्त राशि रूपये में";
        //// cell.Text = @"<b style=""font-weight: bold; color:white;"">आज दिनांक " + "</br> " + System.DateTime.Now.ToShortDateString() + "</br> "+"को प्राप्त राशि रूपये में" + "</br> ";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "प्रगतिशील प्राप्त राशि";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District") != null))
        {
            if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District") == null))
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
            //HeaderCell.Text = "Sub Total";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.ColumnSpan = 2;
            //NewTotalRow.Cells.Add(HeaderCell);

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


            //GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            //rowIndex++;
            //qtyTotal = 0;
            //qtyTotal1 = 0;

            //qtyTotal6 = 0;
            //qtyTotal7 = 0;
            //qtyTotal8 = 0;


        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    //protected void ExportToPDF(object sender, EventArgs e)
    //{
        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        GridView1.AllowPaging = false;
        //        fillgrid();

        //        GridView1.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A2, 20f, 20f, 20f, 20f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=Payment_Received_From_MPSCSC_Details_From_Aug.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}
           public void FillCommodity()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Get_Storage_Commodity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                ddlcommodity.DataSource = cmd.ExecuteReader();
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
                con.Close();
            }
        }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
