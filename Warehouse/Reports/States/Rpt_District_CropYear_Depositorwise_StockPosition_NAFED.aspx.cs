using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_State_Rpt_District_CropYear_Depositorwise_StockPosition_NAFED : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;

    //int storid = 0;
    string storid = "0";
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillRegion();
            //fillDistrict();
            //fillBranch();
            //FillGodownType();
            //fillGodown();
            fillDepositor();
            fillCommodity();
            //fillRegion();
            //FillBillDetailsInGrid();
        }

    }

    private void fillDepositor()
    {
        string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('10535','15478','4679')";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.Items.Clear();
            ddlDepositor.DataSource = ds.Tables[0];
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
        }
    }

    //private void fillRegion()
    //{
    //    //string RegionID = Session["Region_ID"].ToString();
    //    string query = "select distinct Region_ID,Regionnm from tbl_MetaData_district";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlRegion.DataSource = ds.Tables[0];
    //        ddlRegion.DataTextField = "Regionnm";
    //        ddlRegion.DataValueField = "Region_ID";
    //        ddlRegion.DataBind();
    //        ddlRegion.Items.Insert(0, "--Select--");
    //        //ddlregion.DataSource = ds.Tables[0];
    //        //ddlregion.DataTextField = "Regionnm";
    //        //ddlregion.DataValueField = "Region_ID";
    //        //ddlregion.Text = ds.Tables[0].Rows[0].ToString();

    //        //ddlregion.Text = ds.Tables[0].Rows[0]["Regionnm"].ToString();
    //        //ddlregion.DataBind();
    //        //ddlregion.Items.Insert(0, "--Select--");

    //    }

    //}

    //private void fillDistrict()
    //{
    //    //string RegionID = Session["Region_ID"].ToString();
    //    string query = "select district_id,DIstrict_Name from tbl_MetaData_district";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddldistrict.DataSource = ds.Tables[0];
    //        ddldistrict.DataTextField = "DIstrict_Name";
    //        ddldistrict.DataValueField = "district_id";
    //        ddldistrict.DataBind();
    //        ddldistrict.Items.Insert(0, "--Select--");
    //    }

    //}

    //private void fillBranch()
    //{

    //    string query = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId ='" + ddldistrict.SelectedValue + "'";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlbranch.DataSource = ds.Tables[0];
    //        ddlbranch.DataTextField = "DepotName";
    //        ddlbranch.DataValueField = "BranchId";
    //        ddlbranch.DataBind();
    //        ddlbranch.Items.Insert(0, "--Select--");
    //    }

    //}

    private void fillCommodity()
    {
        //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id in ('63','64','33','92','75','27')  order by Commodity_Name";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            //ddlcommodity.SelectedValue = "22";
        }
    }

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_ReportGetDistrict_CropYear_Commodity_DepositorWise_StockPosition", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if (ddlRegion.SelectedValue == "--Select--")
                //{
                //cmd.Parameters.AddWithValue("@Region", "0");
                //}
                //else
                //{
                //cmd.Parameters.AddWithValue("@Region", ddlRegion.SelectedValue);
                //}
                //if (ddldistrict.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@DistrictID", "0"); //ddldistrict.SelectedValue.ToString());// ?? vRegion); ?? vDistrict);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue); //ddldistrict.SelectedValue.ToString());// ?? vRegion); ?? vDistrict);
                //}

                //if (ddlbranch.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@BranchID", "0"); //ddlbranch.SelectedValue.ToString());// ?? vRegion); ?? vBranch);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue); //ddlbranch.SelectedValue.ToString());// ?? vRegion); ?? vBranch);
                //}

                if (ddlDepositor.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DepositorID", "0"); 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue); 
                }
                if (ddlcommodity.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CommodityID", "0"); 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue); 
                }
                //if (ddlCropYear.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@CropYear", "0"); 
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue); 
                //}
                string FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@FromDate", FromDate);
                //cmd.ExecuteNonQuery();
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];
                    if (MainTable.Rows.Count > 0)
                    {
                        GV_StockPositionDetails.DataSource = MainTable;
                        GV_StockPositionDetails.DataBind();

                        GV_StockPositionDetails.FooterRow.Style.Add("text-align", "right");
                        GV_StockPositionDetails.FooterRow.Cells[3].Text = "Grand Total";
                        GV_StockPositionDetails.FooterRow.Cells[4].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[16].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                    }
                    else
                    {
                        GV_StockPositionDetails.DataSource = null;
                        GV_StockPositionDetails.DataBind();
                    }

                }
            }
        }
    }
   
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillBranch();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillBillDetailsInGrid();
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_StockPositionDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            //decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }


    //protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillDistrict();
    //}

   
}