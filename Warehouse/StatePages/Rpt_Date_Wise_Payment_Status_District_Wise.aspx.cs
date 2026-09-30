using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Date_Wise_Payment_Status_District_Wise : System.Web.UI.Page
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
        if(!IsPostBack)
        {
            //fillgrid();
        }
    }
    protected void fillgrid()
    {
        
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_District_Date_Month_Wise_Payment_Status_From_MPSCSC]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", txtdatefrom.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtdateto.Text);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "District Wise Pending Payment Status from MPSCSC(From August)" + "</br> "  +"From Date"  +" "+  txtdatefrom.Text +" "+ "To date"  +" "+  txtdateto.Text + "</b> ";

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofbill")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount")).ToString();
                           
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
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnDateSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void btnUTRSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}