using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_Godown_Wise_Payment_Status_For_Owned_Godown_New : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Bill_Payment_Status_For_Owned_Godown", con))
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
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Owned Godown Wise Payment Status" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[4].Text = "Total";
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmt")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString("#,##0.00");

                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBillForSubmisionatBranch")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingBillAmountForSubmision")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofbillPaymentReceivedFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofbillPaymentAmountReceivedFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentDecuctionbyMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalNoofPendingBillatMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalNoofPendingBillAmountatMPSCSC")).ToString("#,##0.00");
                            //GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfBillPayment")).ToString();
                            //GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmtPTG")).ToString();
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    protected void btnback_Click(object sender, EventArgs e)
    {

    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}