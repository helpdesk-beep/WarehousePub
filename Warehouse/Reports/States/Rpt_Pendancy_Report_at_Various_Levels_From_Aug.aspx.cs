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

public partial class Reports_States_Rpt_Pendancy_Report_at_Various_Levels_From_Aug : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Pandency_Report_at_Various_Level_After_Aug_For_MD", con))
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Region Wise Pendancy Report at Various Levels" + "</br> " + "M.P. Warehousing & Logistics Corporarion " + "</br> " + "PVT Godown Storage Charges Bill(Before August)" + "</b> ";
                            GridView1.Columns[2].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PandancyatBMLevelNoofbill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PandancyatBMLevelNoofamount")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PandancyatICMLevelNoofBills")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PandancyatICMLevelNoofAmount")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PandancyatDMMPSCSCLevelNoofBills")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PandancyatDMMPSCSCLevelNoofAmount")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PandancyatAccountheadLevelNoofBills")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PandancyatAccountheadLevelNoofAmount")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PandancyatRMLevelNoofBills")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PandancyatRMLevelNoofAmount")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillNEFT")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NEFT_Amount_By_Bank")).ToString();

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
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 3;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Pendancy at RO MPWLC Level";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendancy at BM MPWLC Level";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendancy at Issue Center Level";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendancy at DM MPWLC Level";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendancy at Account Head";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pendancy at RM (MPWLC) Level";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "NEFT Payment to Godown Owner";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

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
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total Integrated Registration,Verification,PassGenerated";
        //    HeaderCell.ColumnSpan = 9;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = true;
        //    HeaderCell.Font.Size = 13;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total PWD Registration,Verification,PassGenerated";
        //    HeaderCell.ColumnSpan = 9;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = true;
        //    HeaderCell.Font.Size = 13;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total Volunteer Registration,Verification,PassGenerated";
        //    HeaderCell.ColumnSpan = 9;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = true;
        //    HeaderCell.Font.Size = 13;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total Driver Registration,Verification,PassGenerated";
        //    HeaderCell.ColumnSpan = 9;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = true;
        //    HeaderCell.Font.Size = 13;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Total Pregnant And Lactating Registration,Verification,PassGenerated";
        //    HeaderCell.ColumnSpan = 3;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = true;
        //    HeaderCell.Font.Size = 13;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    Grd_RecordRPT.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}

    }
}