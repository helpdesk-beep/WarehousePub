using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Get_Nafed_Payment_Detail_By_Godown : System.Web.UI.Page
{
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Totaling calculation ke liye global variables declare kiye hain
    decimal totalPayment = 0;
    decimal totalTDS = 0;
    decimal totalTDS1 = 0;
    decimal totalNetAmount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GodownID"] != null && Request.QueryString["CommodityID"] != null && Request.QueryString["Date"] != null)
            {
                string godownId = Request.QueryString["GodownID"].ToString();
                int commodityId = Convert.ToInt32(Request.QueryString["CommodityID"]);
                string paymentDate = Request.QueryString["Date"].ToString();

                BindGodownDetailReport(paymentDate, godownId, commodityId);
            }
            else
            {
                Response.Write("<script>alert('Invalid Access Parameters.'); window.history.back();</script>");
            }
        }
    }

    private void BindGodownDetailReport(string paymentDate, string godownId, int commodityId)
    {
        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Nafed_Payment_Detail_By_Godown", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);
                cmd.Parameters.AddWithValue("@Godown_ID", godownId);
                cmd.Parameters.AddWithValue("@Commodity_Id", commodityId);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        lblDistrict.Text = dt.Rows[0]["District_Name"].ToString();
                        lblGodown.Text = dt.Rows[0]["Godown_Name"].ToString();
                        lblCommodity.Text = dt.Rows[0]["Commodity_Name"].ToString();
                    }

                    gvNafedDetailsRow.DataSource = dt;
                    gvNafedDetailsRow.DataBind();
                }
            }
        }
    }

    // CHANGED: Added RowDataBound to compute sum and format footer beautifully
    protected void gvNafedDetailsRow_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // 1. Agar row Data Row hai toh sum calculate karein
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalPayment += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Payment"));
            totalTDS += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS"));
            totalTDS1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TDS1"));
            totalNetAmount += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
        }

        // 2. Agar row Footer row hai toh computed values fill karein
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Grand Total";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[0].ColumnSpan = 3; // Pehle 3 columns ko merge kiya (S.No, Crop Year, Date)

            // Baki merged fields ko remove karna zaroori hai layout clear rakhne ke liye
            e.Row.Cells.RemoveAt(1);
            e.Row.Cells.RemoveAt(1);

            // Total dynamic numerical assignments matching proper double accounting alignment structure
            e.Row.Cells[1].Text = totalPayment.ToString("N2");
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[2].Text = totalTDS.ToString("N2");
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[3].Text = totalTDS1.ToString("N2");
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[4].Text = totalNetAmount.ToString("N2");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].ForeColor = System.Drawing.Color.FromName("#1cc88a"); // Rich green color highlight for final net sum
        }
    }
}