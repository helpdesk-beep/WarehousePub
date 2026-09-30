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

public partial class Reports_States_Rpt_District_and_Date_Wise_Pendancy : System.Web.UI.Page
{
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
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                fillgrid(Request.QueryString["ID"].ToString());
            }
            else
            {
                //fillgrid("0");
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Bill_Detail_After_District_Difference_Wise_With_Amount]", con))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_District_and_Date_Wise_Pendancy]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", txtFDate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtTDate.Text);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "District Wise Bill Pending Status At Various Levels(From August)" + "</b> ";

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                           // GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CentreInchargeDscPending")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingHOAmt")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingPassingOrder")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAGMDSC")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingCreatedEPF")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingNefHOAmount")).ToString();
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
        GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell1 = new TableHeaderCell();

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 4;
        cell1.Text = "Branch Manager MPWLC";
        row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "Centre Incharge MPSCSC";
        //row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "DM MPSCSC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "HO MPSCSC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 4;
        cell1.Text = "Regional Manager MPWLC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "HO MPWLC";
        row1.Controls.Add(cell1);

        row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row1);

        GridViewRow row = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "S.No.";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Region Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "District Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "No. of Godown";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Generate";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for DSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Submission";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for DSC";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for DSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Payment";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Passing Order(Acc. Mgr.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for DSC(Acc. Mgr.)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for DSC(RM)";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending EPF for Generation";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill for Payment";
        row.Controls.Add(cell);


        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(1, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid("0");
    }
}