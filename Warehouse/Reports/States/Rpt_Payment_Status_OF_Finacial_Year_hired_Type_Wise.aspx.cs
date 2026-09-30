using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Reports_State_Rpt_Payment_Status_OF_Finacial_Year_hired_Type_Wise : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          fillgrid();
            fillgrid2();
            fillgrid3();
        }
    }
   
   
    protected void fillgrid()
    {                
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Status_of_All_Type", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                //cmd.Parameters.AddWithValue("@GodownID", Session["GodownID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("BillAmt")).ToString();
                            GridView1.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("SUBBillAmt")).ToString();
                            GridView1.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("PendingForSubmision")).ToString();
                            GridView1.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingAmountForSubmision")).ToString();
                            GridView1.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoofbillPaymentReceivedFromMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[11].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[12].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                            GridView1.FooterRow.Cells[13].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[14].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[15].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[16].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[17].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                            GridView1.FooterRow.Cells[18].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();

                            GridView1.FooterRow.Cells[19].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfBillPayment")).ToString();
                            GridView1.FooterRow.Cells[20].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("RentBillAmt")).ToString();
                            GridView1.FooterRow.Cells[21].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PayToMPWLC")).ToString();
                            GridView1.FooterRow.Cells[22].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPWLC")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillgrid2()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Status_of_All_Type", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@GodownID", Session["GodownID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            GridView2.DataSource = DS.Tables[1];
                            GridView2.DataBind();

                            //// GridView1.Caption = @"<b style=""font-weight: bold; text-align:center; font-size:large;""> Godown Name" + "  -   " + dt.Rows[0]["Godown_Name"].ToString() ;
                            GridView2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView2.FooterRow.Cells[1].Text = "Total";
                           // GridView2.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfGenerateBill")).ToString();
                            GridView2.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("BillAmt")).ToString();
                            //GridView2.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("NoOfSUBBill")).ToString();
                            GridView2.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("SUBBillAmt")).ToString();
                            //GridView2.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("PendingForSubmision")).ToString();
                            //GridView2.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingAmountForSubmision")).ToString();
                            //GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoofbillPaymentReceivedFromMPSCSC")).ToString();
                            GridView2.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView2.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView2.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView2.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                           // GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();
                            //GridView2.FooterRow.Cells[14].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            //GridView2.FooterRow.Cells[15].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            //GridView2.FooterRow.Cells[16].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            //GridView2.FooterRow.Cells[17].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                            GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();

                            //GridView2.FooterRow.Cells[19].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfBillPayment")).ToString();
                            GridView2.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("RentBillAmt")).ToString();
                            GridView2.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PayToMPWLC")).ToString();
                            GridView2.FooterRow.Cells[11].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPWLC")).ToString();
                            //// lblgdnname.Text = dt.Rows[0]["Godown_Name"].ToString();
                        }
                        
                    }
                }
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Bill Generated by Branch Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 6;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 5;
        cell.Text = "Private Warehouse Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 4;
        cell.Text = "Rent Bill Payment Details";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }


    protected void GridView2_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Storage Charges Bill Generated by Branch Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 5;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 5;
        //cell.Text = "Private Warehouse Storage Charges Payment Status From MPSCSC";
        //row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Rent Bill Payment Details";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView2.HeaderRow.Parent.Controls.AddAt(0, row);
    }

    protected void fillgrid3()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Status_Godown_Type", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@GodownID", Session["GodownID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView3.DataSource = DS.Tables[0];
                            GridView3.DataBind();

                            //// GridView1.Caption = @"<b style=""font-weight: bold; text-align:center; font-size:large;""> Godown Name" + "  -   " + dt.Rows[0]["Godown_Name"].ToString() ;
                            GridView3.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView3.FooterRow.Cells[1].Text = "Total";
                            // GridView2.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfGenerateBill")).ToString();
                            GridView3.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("BillAmt")).ToString();
                            //GridView2.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("NoOfSUBBill")).ToString();
                            GridView3.FooterRow.Cells[3].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("SUBBillAmt")).ToString();
                            //GridView2.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("PendingForSubmision")).ToString();
                            //GridView2.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingAmountForSubmision")).ToString();
                            //GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoofbillPaymentReceivedFromMPSCSC")).ToString();
                            GridView3.FooterRow.Cells[4].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            GridView3.FooterRow.Cells[5].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            GridView3.FooterRow.Cells[6].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView3.FooterRow.Cells[7].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                            // GridView2.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();
                            //GridView2.FooterRow.Cells[14].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("Gross_Amount")).ToString();
                            //GridView2.FooterRow.Cells[15].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("TDS_Amt")).ToString();
                            //GridView2.FooterRow.Cells[16].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            //GridView2.FooterRow.Cells[17].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PaymentReceivedAfterDeduction")).ToString();
                            GridView3.FooterRow.Cells[8].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPSCSC")).ToString();

                            //GridView2.FooterRow.Cells[19].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Int32>("NoOfBillPayment")).ToString();
                            //GridView3.FooterRow.Cells[9].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("RentBillAmt")).ToString();
                            //GridView3.FooterRow.Cells[10].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PayToMPWLC")).ToString();
                            //GridView3.FooterRow.Cells[11].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<Decimal>("PendingatMPWLC")).ToString();
                            //// lblgdnname.Text = dt.Rows[0]["Godown_Name"].ToString();
                        }

                    }
                }
            }
        }
    }

    protected void GridView3_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Storage Charges Bill Generated by Branch Manager";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 5;
        cell.Text = "Storage Charges Payment Status From MPSCSC";
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 5;
        //cell.Text = "Private Warehouse Storage Charges Payment Status From MPSCSC";
        //row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView3.HeaderRow.Parent.Controls.AddAt(0, row);
    }
}