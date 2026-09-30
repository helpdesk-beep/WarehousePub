using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_District_Wise_Qty_Available_Last_10_Year : System.Web.UI.Page
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
            fillComodity();
            //fillgrid();
        }
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
                drpDwnCommodity.Items.Clear();
                drpDwnCommodity.DataSource = ds.Tables[0];
                drpDwnCommodity.DataTextField = "Commodity_Name";
                drpDwnCommodity.DataValueField = "Commodity_Id";
                drpDwnCommodity.DataBind();
                drpDwnCommodity.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("District_Wise_Stock_Position_Last_10_Years", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if (drpDwnCommodity.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@CommodityID", 0);
                //}
                //else {
                cmd.Parameters.AddWithValue("@CommodityID", drpDwnCommodity.SelectedValue);
                //}
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "Right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2016-17")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2017-18")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2018-19")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2019-20")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2020-21")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2025-26")).ToString("#,##0.00");
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
    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void dllGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDwnCommodity.SelectedValue != "--Select--")
            fillgrid();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
    }
}