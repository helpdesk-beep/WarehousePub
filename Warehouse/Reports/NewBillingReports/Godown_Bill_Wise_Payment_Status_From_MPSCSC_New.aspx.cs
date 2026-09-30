using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Reports_NewBillingReports_Godown_Bill_Wise_Payment_Status_From_MPSCSC_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
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
    decimal qtyTotal9 = 0;
    decimal qtyTotal10 = 0;
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;
    decimal qtyTotal13 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;

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
    string storid = "0";
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillDivision();
        }
    }
    protected void fillDivision()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown__Bill_Wise_Payment_Status_From_MPSCSC", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", Request.QueryString["Godown_ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grddivision.DataSource = dt;
                            grddivision.DataBind();
                            //divdivision.Visible = true;
                            grddivision.FooterRow.Style.Add("text-align", "Right");
                            grddivision.FooterRow.Cells[7].Text = "Total";
                            //grddivision.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBill")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmount")).ToString("#,##0.00");
                            //grddivision.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalReceivedBill")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString("#,##0.00");

                        }
                        else
                        {
                            grddivision.DataSource = null;
                            grddivision.DataBind();
                        }
                    }
                }
            }
        }
    }
}