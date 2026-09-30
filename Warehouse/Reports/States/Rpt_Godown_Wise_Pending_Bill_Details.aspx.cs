using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Godown_Wise_Pending_Bill_Details : System.Web.UI.Page
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
            if (!String.IsNullOrEmpty(Request.QueryString["BranchId"]))
            {
                fillgrid();
            }
        }
    }
    //private void fillRegion()
    //{
    //    try
    //    {
    //        string query = "";

    //        query = "SELECT DISTINCT [Region_ID],[Regionnm] FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";

    //        cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter();
    //        da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlRegion.Items.Clear();
    //            ddlRegion.DataSource = ds.Tables[0];
    //            ddlRegion.DataTextField = "Regionnm";
    //            ddlRegion.DataValueField = "Region_ID";
    //            ddlRegion.DataBind();
    //            ddlRegion.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("Get_Pandancy_Month_Days_Storage_Bill_Wise_From_MPSCSC_For_Dashboard_Godown_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@branchid", Request.QueryString["BranchId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Godown Wise Payment Pending From MPSCSC" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[5].Text = "Total";
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
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
}