using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_State_Rpt_Godown_CropYear_Depositorwise_StockPosition : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
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
            //fillDepositor();
            //fillCommodity();
            //fillRegion();
            //FillBillDetailsInGrid();
            fillDepositor();
            fillComodity();

        }

    }

    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }
        FillBillDetailsInGrid();

    }
    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
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
                ddlComodity.Items.Insert(0, "--Select--");
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

    private void fillDepositor()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478','16985')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositor.Items.Clear();
                ddldepositor.DataSource = ds.Tables[0];
                ddldepositor.DataTextField = "Depositor_Name";
                ddldepositor.DataValueField = "Depositor_ID";
                ddldepositor.DataBind();
                ddldepositor.Items.Insert(0, "--Select--");
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

    //private void fillDepositor()
    //{
    //    string query = "select Depositor_ID, Depositor_Name FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('10535','15478','4679')";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlDepositor.Items.Clear();
    //        ddlDepositor.DataSource = ds.Tables[0];
    //        ddlDepositor.DataTextField = "Depositor_Name";
    //        ddlDepositor.DataValueField = "Depositor_ID";
    //        ddlDepositor.DataBind();
    //        ddlDepositor.Items.Insert(0, "--Select--");
    //    }
    //}

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

    //private void fillCommodity()
    //{
    //    //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
    //    string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id in ('63','64','33','92','75','27')  order by Commodity_Name";
    //    SqlCommand cmd = new SqlCommand(query, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlcommodity.Items.Clear();
    //        ddlcommodity.DataSource = ds.Tables[0];
    //        ddlcommodity.DataTextField = "Commodity_Name";
    //        ddlcommodity.DataValueField = "Commodity_Id";
    //        ddlcommodity.DataBind();
    //        ddlcommodity.Items.Insert(0, "--Select--");
    //        //ddlcommodity.SelectedValue = "22";
    //    }
    //}

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Report_Get_Godown_CropYear_Commodity_DepositorWise_StockPosition", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));
                cmd.Parameters.AddWithValue("@DepositorID", ddldepositor.SelectedValue);
                cmd.Parameters.AddWithValue("@CommodityID", ddlComodity.SelectedValue);
                //if (Request.QueryString["DepositorID"].ToString() == null)
                //{
                //    cmd.Parameters.AddWithValue("@CommodityID", Request.QueryString["CommodityID"].ToString());

                //}
                //if(Request.QueryString["CommodityID"].ToString() == null)
                //{
                //    cmd.Parameters.AddWithValue("@DepositorID", Request.QueryString["DepositorID"].ToString());
                //}


                //if (ddlDepositor.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@DepositorID", "0"); 
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue); 
                //}
                //if (ddlcommodity.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@CommodityID", "0"); 
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue); 
                //}
                //if (ddlCropYear.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@CropYear", "0"); 
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue); 
                //}

                //cmd.Parameters.AddWithValue("@FromDate", DateTime.Now.ToString("yyyy-MM-dd"));
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

                        GV_StockPositionDetails.FooterRow.Cells[4].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2017-18")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2018-19")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2019-20")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2020-21")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[10].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[11].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[12].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2025-26")).ToString();
                        GV_StockPositionDetails.FooterRow.Cells[13].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[4].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                        //GV_StockPositionDetails.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
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
   
    //protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //fillBranch();
    //}

    //protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void GV_StockPositionDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2017-18]").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2018-19]").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2019-20]").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2020-21]").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2025-26]").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;

        }
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    FillBillDetailsInGrid();
    //}

   
}