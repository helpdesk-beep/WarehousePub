using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_HiredTypeWisePaymentStatus : System.Web.UI.Page
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

    //private void fillDistrict()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlRegion.SelectedValue + "' order by Regionnm asc";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddldistrict.Items.Clear();
    //            ddldistrict.DataSource = ds.Tables[0];
    //            ddldistrict.DataTextField = "District_Name";
    //            ddldistrict.DataValueField = "District_Id";
    //            ddldistrict.DataBind();
    //            ddldistrict.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}

    //private void fillBranch()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' order by DepotName asc";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlbranch.Items.Clear();
    //            ddlbranch.DataSource = ds.Tables[0];
    //            ddlbranch.DataTextField = "DepotName";
    //            ddlbranch.DataValueField = "BranchId";
    //            ddlbranch.DataBind();
    //            ddlbranch.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
    //private void fillMonth()
    //{
    //    try
    //    {
    //        cmd = new SqlCommand("Get_Pandancy_Month", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlmonth.Items.Clear();
    //            ddlmonth.DataSource = ds.Tables[0];
    //            ddlmonth.DataTextField = "TotalPendingMonth";
    //            ddlmonth.DataValueField = "TotalPendingMonth";
    //            ddlmonth.DataBind();
    //            ddlmonth.Items.Insert(0, "--Select--");
    //        }
    //        else
    //        {
    //            ////
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        //////
    //    }
    //}
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

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}