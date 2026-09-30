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

public partial class Region_Rpt_Get_Bill_Detail_After_August_With_Amount_And_Difference_District_Wise : System.Web.UI.Page
{
   
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
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Detail_After_August__With_Amount_Difference_District_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_Logid"].ToString());
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Region " + "  -   " + dt.Rows[0]["Regionnm"].ToString() + "</br> " + "M.P. Warehousing & Logistics Corporarion " + "</br> " + "PVT Godown Storage Charges Bill(After August)" + "</br> "+ "Pending at DM MPSCSC Level" +"</b> ";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillGenerateAmt")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillAmtDSC")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SUBBillAmt")).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos10")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos11")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofCSMS_BillAmt")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos12")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos13")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PaymentReceviedfromDMSCSC")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PaymentReceviedfromDMSCSCAmount")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSCAmt")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos18")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Diffierenceinos19")).ToString();
                           
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


    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell = new TableHeaderCell();
        //cell.Text = "";
        //cell.ColumnSpan = 3;
        //row.Controls.Add(cell);



        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 6;
        //cell.Text = "By MPWLC BM";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 2;
        //cell.Text = "By MPSCSC ICM";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 4;
        //cell.Text = "BY MPWLC RM";
        //row.Controls.Add(cell);

        //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "1";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "2";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "3";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "4";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "5";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "6";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "7";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "8";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "9";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "10=6-8";
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.CssClass = "alert alert-info";
            //HeaderCell.Font.Bold = false;
            //HeaderCell.Font.Size = 10;
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.Text = "11=7-9";
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.CssClass = "alert alert-info";
            //HeaderCell.Font.Bold = false;
            //HeaderCell.Font.Size = 10;
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "10";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "11";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "12=8-10";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "13=9-11";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "14";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "15";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "16";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "17";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "18=14-16";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "19=15-17";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderGridRow.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 3;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Bill generated by BM";
        //    HeaderCell.ColumnSpan = 4;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Issue Center Manager";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    //HeaderCell = new TableCell();
        //    //HeaderCell.Text = "Difference";
        //    //HeaderCell.ColumnSpan = 2;
        //    //HeaderCell.CssClass = "alert alert-info";
        //    //HeaderCell.Font.Bold = false;
        //    //HeaderCell.Font.Size = 10;
        //    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    //HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Pendency at Isue Center Manager";
        //    HeaderCell.ColumnSpan = 4;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "BY MPWLC RM";
        //    HeaderCell.ColumnSpan = 4;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Pendency at RO Level";
        //    HeaderCell.ColumnSpan = 4;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    HeaderGridRow.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //    GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "By MPWLC BM";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            
            HeaderCell = new TableCell();
            HeaderCell.Text = "By ICM MPSCSC";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendency at MPSCSC";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "By MPWLC RM";
            HeaderCell.ColumnSpan = 4;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendency at RO Level";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderGridRow.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

    }
}