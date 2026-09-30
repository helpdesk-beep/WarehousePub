using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_Region_Rpt_District_Wise_Payment_Status_Alll_Commodity_Wise : System.Web.UI.Page
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
            //fillRegion();
            //fillMonth();
            //fillgrid();
            //fillgrid();
            fillComodity();
        }
    }
    private void fillComodity()
    {
        try
        {
            string query = "";
            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select distinct Commodity_Id,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlComodity.Items.Clear();
                ddlComodity.DataSource = ds.Tables[0];
                ddlComodity.DataTextField = "Commodity_Name";
                ddlComodity.DataValueField = "Commodity_Id";
                ddlComodity.DataBind();
                ddlComodity.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Bill_Payment_Status_All_For_Region_CommodityWise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_Logid"].ToString());
                cmd.Parameters.AddWithValue("@FromDate", txtFDate.Text.ToString());
                cmd.Parameters.AddWithValue("@ToDate", txtTDate.Text.ToString());
                cmd.Parameters.AddWithValue("@CommodityID", ddlComodity.SelectedValue.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "District Wise Payment Status" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmt")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();

                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBillForSubmisionatBranch")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingBillAmountForSubmision")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoofbillPaymentReceivedFromMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofbillPaymentAmountReceivedFromMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PaymentDecuctionbyMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalNoofPendingBillatMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalNoofPendingBillAmountatMPSCSC")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfBillPayment")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmtPTG")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendintatRM")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendintBillAmountatRM")).ToString();
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
    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //    fillDistrict();
    //}
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
      
    }

    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //    fillBranch();
    //   // fillFinancialYear();
    //}

    //protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //   // fillFinancialYear();
    //}

    //protected void ddlfy_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //    fillMonth();
    //}

    //protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillgrid();
    //}

    protected void btnback_Click(object sender, EventArgs e)
    {

    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}