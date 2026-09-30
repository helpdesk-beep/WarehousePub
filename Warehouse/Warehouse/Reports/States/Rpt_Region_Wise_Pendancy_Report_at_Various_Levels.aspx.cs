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

public partial class Reports_States_Rpt_Region_Wise_Pendancy_Report_at_Various_Levels : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_Pandency_Report_at_Various_Level_From_Aug", con))
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Region Wise Pendancy Report at Various Levels" + "</br> " + "M.P. Warehousing & Logistics Corporarion " + "</br> " + "PVT Godown Storage Charges Bill(After August)" + "</b> ";
                            GridView1.Columns[2].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillGenerateAmt")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillAmtDSC")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofCSMS_BillAmt")).ToString();
                            //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_AGM_Acc_of_RO_Leval_With_Amount")).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoOfRMDSCAmt")).ToString();
                           
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
        cell.ColumnSpan = 4;
        cell.Text = "By MPWLC BM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Pendancy at ICM Level";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "BY MPWLC RM";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Pendancy at RO Level";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
}