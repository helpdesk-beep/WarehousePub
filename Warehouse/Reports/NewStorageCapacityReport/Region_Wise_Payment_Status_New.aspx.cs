using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Region_Wise_Payment_Status_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_Get_Region_Wise_PaymentStatus", con))
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
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Region Wise Payment Status" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBillPresentedinFY")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalBillAmountPresented")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAmountReceivedInFY")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RemainingAmountFromDepositor")).ToString();
                        }


                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
}