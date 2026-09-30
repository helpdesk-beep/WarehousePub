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
public partial class SRV_Storage_Reports_Inspenctions_Rpt_DistrictAll_CropYear_Depositorwise_StockPosition_NAFED_New : System.Web.UI.Page
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

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    string storid = "0";
    int rowIndex = 1;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCommodity();
        }

    }

    private void fillCommodity()
    {
        //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id in ('63','64','33','92','75','27','26','13','29','97','96','25')  order by Commodity_Name";
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
                using (SqlCommand cmd = new SqlCommand("usp_ReportGet_AllDistrict_Summary_CropYear_Commodity_DepositorWise_StockPosition_New", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    string selectedIDs = string.Join(",",
                            ddlcommodity.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value)
                    );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";   // All commodities
                    else
                        //selectedIDs = "'" + selectedIDs + "'";
                        //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
                        cmd.Parameters.Add("@CommodityIDs", SqlDbType.VarChar).Value = selectedIDs;

                    cmd.Parameters.AddWithValue("@FromDate", DateTime.Now.ToString("yyyy-MM-dd"));
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

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Grand Total";
                            GridView1.FooterRow.Cells[3].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                            GridView1.FooterRow.Cells[4].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                            GridView1.FooterRow.Cells[5].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                            GridView1.FooterRow.Cells[6].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                            GridView1.FooterRow.Cells[7].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
                            //GridView1.FooterRow.Cells[16].Text = MainTable.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString();
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

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillBillDetailsInGrid();
    }

    protected void GV_StockPositionDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DepositorName").ToString());
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;


        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Session["DepositorID"] = ddlDepositor.SelectedValue.ToString();
        Session["CommodityID"] = ddlcommodity.SelectedValue.ToString();
        FillBillDetailsInGrid();
    }

    protected void GV_StockPositionDetails_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "DepositorName") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DepositorName").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "DepositorName") == null))
        {
            newRow = true;
            rowIndex = 0;
        }
        if (newRow)
        {
            GridView GridView1 = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;

        }


    }


}