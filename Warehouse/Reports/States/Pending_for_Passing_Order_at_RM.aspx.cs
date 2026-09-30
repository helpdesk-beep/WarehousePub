using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Pending_for_Passing_Order_at_RM_aspx : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void fillgrid()
    {

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_Godown_Wise_Passing_Order_Pendancy_From_Received_Amount_By_MPSCSC]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", Request.QueryString["District_Id"].ToString());
                if (Session["txtUTRNo"] == null)
                {
                    cmd.Parameters.AddWithValue("@UTRNo", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@UTRNo", Session["txtUTRNo"].ToString());
                }
                //cmd.Parameters.AddWithValue("@UTRNo", string.IsNullOrEmpty(Session["txtUTRNo"].ToString()));
                cmd.Parameters.AddWithValue("@FromDate", Session["txtdatefrom"].ToString());
                cmd.Parameters.AddWithValue("@ToDate", Session["txtdateto"].ToString());
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
                            GridView1.FooterRow.Cells[8].Text = "Total";
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DEDTBill_Amount")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR2Gross_Amount")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR2Payable_Amount")).ToString();
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR3DEDTBill_Amount")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("POJVS_Bill_Amount")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR4POJVS_Net_Amount")).ToString();
                            //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingPassingOrder")).ToString();
                            //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR5RPONet_Amount")).ToString();
                            //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR6PFCredit_Amount")).ToString();
                            //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingforGenerateFile")).ToString();
                            //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("UTR7PFCredit_Amount")).ToString();

                            //Session["txtUTRNo"] = txtUTRNo.Text;
                            //Session["txtdatefrom"] = txtdatefrom.Text;
                            //Session["txtdateto"] = txtdateto.Text;
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
}