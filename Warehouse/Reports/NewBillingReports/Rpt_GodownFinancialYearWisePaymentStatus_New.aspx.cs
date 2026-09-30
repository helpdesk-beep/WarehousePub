using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_NewBillingReport_Rpt_GodownFinancialYearWisePaymentStatus_New : System.Web.UI.Page
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
            fillGodownType();
            fillFinasncialYear();
        }
    }

    private void fillGodownType()
    {
        try
        {
            string query = "";

            query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodity.Items.Clear();
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Hired_Type";
                ddlcommodity.DataValueField = "Hired_Type";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, "All");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }



    private void fillFinasncialYear()
    {
        try
        {
            string query = "";

            query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Financial_Year>'2019-2020'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlFY.Items.Clear();
                ddlFY.DataSource = ds.Tables[0];
                ddlFY.DataTextField = "Financial_Year";
                ddlFY.DataValueField = "Financial_Year";
                ddlFY.DataBind();
                ddlFY.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void fillgrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_HiredAType_Financial_Year_Wise_PaymentStatus", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlcommodity.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@HireType", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@HireType", ddlcommodity.SelectedValue);
                }
                if (ddlFY.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", ddlFY.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Payment Pending no of months,Days From MPSCSC" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SubmittedBillAmount")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GrossAmountReceivedFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDSDeductionFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeductionFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PayableAmountReceivedFromMPSCSC")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RentBillAmount")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaytoGodownOwnerFromMPWLC")).ToString("#,##0.00");
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
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }


    protected void btnback_Click(object sender, EventArgs e)
    {

    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}