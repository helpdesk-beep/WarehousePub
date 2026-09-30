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

public partial class Reports_Rpt_Pendancy_At_Verius_Level : System.Web.UI.Page
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
    decimal qtyTotal31 = 0;
    decimal qtyTotal32 = 0;
    decimal qtyTotal33 = 0;
    decimal qtyTotal34 = 0;
    decimal qtyTotal35 = 0;
    decimal qtyTotal36 = 0;
    decimal qtyTotal37 = 0;

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
    decimal grQtyTotal31 = 0;
    decimal grQtyTotal32 = 0;
    decimal grQtyTotal33 = 0;
    decimal grQtyTotal34 = 0;
    decimal grQtyTotal35 = 0;
    decimal grQtyTotal36 = 0;
    decimal grQtyTotal37 = 0;

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
                fillgrid("0");
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Bill_Detail_After_District_Difference_Wise_With_Amount]", con))
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_District_Wise_Pendancy_at_Varius_Lavel]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@Region_ID", RID);
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
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                           // GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BMAmtsubmited")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BilldiffDSC")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DSCsignedbyBM")).ToString();
                            //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BMsubmited")).ToString();
                            //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BMAmtsubmited")).ToString();
                            //GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingDMBill")).ToString();
                            //GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();

                            //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ApprovedbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfPaymentBill_HO")).ToString();
                            //GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount_HO")).ToString();
                            //GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt_HO")).ToString();
                            //GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction_HO")).ToString();
                            //GridView1.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DM_BillAmt_HO")).ToString();
                            //GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingDMBill")).ToString();
                            //GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingDMAmt")).ToString();

                            //GridView1.FooterRow.Cells[26].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            //GridView1.FooterRow.Cells[27].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_AGM_Acc_of_RO_Leval_With_Amount")).ToString();
                            //GridView1.FooterRow.Cells[28].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SignedbyICM")).ToString();
                            //GridView1.FooterRow.Cells[29].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmtSignedbyICM")).ToString();
                            //GridView1.FooterRow.Cells[30].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            //GridView1.FooterRow.Cells[31].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoOfRMDSCAmt")).ToString();
                            //GridView1.FooterRow.Cells[32].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ApprovedbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[33].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[34].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Generated")).ToString();
                            //GridView1.FooterRow.Cells[35].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Generated")).ToString();
                            //GridView1.FooterRow.Cells[36].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_File_Uploaded")).ToString();
                            //GridView1.FooterRow.Cells[37].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_File_Amount_Uploaded")).ToString();

                            //GridView1.FooterRow.Cells[38].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofBillNEFT")).ToString();
                            //GridView1.FooterRow.Cells[39].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NEFT_Amount_By_Bank")).ToString();
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
        cell1.ColumnSpan = 2;
        cell1.Text = "Branch Manager MPWLC";
        row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 1;
        //cell1.Text = "Centre Incharge MPSCSC";
        //row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "District Manager/HO MPSCSC";
        row1.Controls.Add(cell1);

        cell1 = new TableHeaderCell();
        cell1.ColumnSpan = 1;
        cell1.Text = "Regional Manager MPWLC";
        row1.Controls.Add(cell1);

        //cell1 = new TableHeaderCell();
        //cell1.ColumnSpan = 6;
        //cell1.Text = "Payment Details";
        //row1.Controls.Add(cell1);

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
        cell.Text = "District Name";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "No. of Godown";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Bill Generated";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Generated Bill Amount";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bill";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bill Amount";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for Digital Signing";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bill Amount for Digital Signing";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Bills Submitted to Centre Incharge";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Bills Amount Submitted to Centre Incharge";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for Submission to Centre Incharge (MPSCSC)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill Amount for Submission to Centre Incharge (MPSCSC)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills Amount";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bills for Digital Signing";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bills Amount for Digital Signing";
        row.Controls.Add(cell);


        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Total Bill DSC by DM";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Total DSC Bill Amount";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "No Of Payment Bill HO";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Gross Amount by HO";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "TDS Amount Deduction by HO";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Other Deduction by HO";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Recieved Amount From MPSCSC";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Amount For Payment";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Total Bill Passed by Account Manager";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Total Bill Amount Passed by Account Manager";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill for Passing at Account Manager ";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bill Amount for Passing at Account Manager";
        //row.Controls.Add(cell);


        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills by RM";
        //row.Controls.Add(cell);


        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Digitally Signed Bills Amount by RM";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Pending Bills for Digital Signing by RM";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Pending Bills Amount for Digital Signing by RM";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Generated Payment File (in No's.)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Generated Payment File (Amount in Rs.)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Uploaded Payment File (in No's.)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "Uploaded Payment File (Amount in Rs.)";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "NEFT Payment to Godown Owner (in No's.)";
        //row.Controls.Add(cell);


        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 1;
        //cell.Text = "NEFT Payment to Godown Owner (Amount in Rs.)";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(1, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //bool newRow = false;

        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        //{
        //    if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
        //        newRow = true;
        //}
        //if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
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
        //    HeaderCell.ColumnSpan = 2;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

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

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal4.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal5.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal6.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal7.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal8.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal9.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal10.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal11.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal12.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal13.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal14.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal15.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal16.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal17.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal18.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal19.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal20.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal21.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal22.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal23.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.Text = qtyTotal24.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
        //    rowIndex++;
        //    qtyTotal = 0;
        //    qtyTotal1 = 0;
        //    qtyTotal2 = 0;
        //    qtyTotal3 = 0;
        //    qtyTotal4 = 0;
        //    qtyTotal5 = 0;
        //    qtyTotal6 = 0;
        //    qtyTotal7 = 0;
        //    qtyTotal8 = 0;
        //    qtyTotal9 = 0;
        //    qtyTotal10 = 0;
        //    qtyTotal11 = 0;
        //    qtyTotal12 = 0;
        //    qtyTotal13 = 0;
        //    qtyTotal14 = 0;
        //    qtyTotal15 = 0;
        //    qtyTotal16 = 0;
        //    qtyTotal17 = 0;
        //    qtyTotal18 = 0;
        //    qtyTotal19 = 0;
        //    qtyTotal20 = 0;
        //    qtyTotal21 = 0;
        //    qtyTotal22 = 0;
        //    qtyTotal23 = 0;
        //    qtyTotal24 = 0;
        // }



    }
}