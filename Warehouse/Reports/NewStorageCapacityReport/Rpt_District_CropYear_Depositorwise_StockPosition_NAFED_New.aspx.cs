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
public partial class SRV_Storage_Reports_Inspenctions_Rpt_District_CropYear_Depositorwise_StockPosition_NAFED_New : System.Web.UI.Page
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
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;

    //int storid = 0;
    string storid = "0";
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillDepositor();
            fillCommodity();
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
            //ddlcommodity.Items.Insert(0, "--Select--");
            //ddlcommodity.SelectedValue = "22";
        }
    }

    protected void FillBillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand("usp_ReportGetDistrict_CropYear_Commodity_DepositorWise_StockPosition_New", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (ddlDepositor.SelectedValue == "--Select--")
                    {
                        cmd.Parameters.AddWithValue("@DepositorID", "0");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DepositorID", ddlDepositor.SelectedValue);
                    }
                    //if (ddlcommodity.SelectedValue == "--Select--")
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", "0");
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@CommodityID", ddlcommodity.SelectedValue);
                    //}
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    string selectedIDs = string.Join(",",
                            ddlcommodity.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;

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
                            GridView1.DataSource = MainTable;
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align", "left");
                            GridView1.FooterRow.Cells[4].Text = "Grand Total";
                            GridView1.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2019-20")).ToString();
                            GridView1.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2020-21")).ToString();
                            GridView1.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                            GridView1.FooterRow.Cells[8].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                            GridView1.FooterRow.Cells[9].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                            GridView1.FooterRow.Cells[10].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                            GridView1.FooterRow.Cells[11].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                            //GV_StockPositionDetails.FooterRow.Cells[16].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
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
        catch (Exception ex)
        {
            string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
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
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2019-20]").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2020-21]").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
            //decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillBillDetailsInGrid();
    }



}