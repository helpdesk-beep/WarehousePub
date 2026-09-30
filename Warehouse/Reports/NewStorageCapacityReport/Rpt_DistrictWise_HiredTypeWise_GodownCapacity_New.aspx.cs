using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;

public partial class Rpt_DistrictWise_HiredTypeWise_GodownCapacity_New : System.Web.UI.Page
{
    decimal qtyTotal1 = 0, qtyTotal2 = 0, qtyTotal3 = 0, qtyTotal4 = 0;
    long storid = 0;
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillStorageType();
            FillGodownType();
        }
    }

    private void FillStorageType()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "select distinct Storage_Type from tbl_MetaData_GODOWN_2018";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlstorageType.DataSource = ds.Tables[0];
                ddlstorageType.DataTextField = "Storage_Type";
                ddlstorageType.DataValueField = "Storage_Type";
                ddlstorageType.DataBind();
                ddlstorageType.Items.Insert(0, "--Select--");
            }
        }
    }

    private void FillGodownType()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodownType.DataSource = ds.Tables[0];
                ddlGodownType.DataTextField = "Hired_Type";
                ddlGodownType.DataValueField = "Hired_Type";
                ddlGodownType.DataBind();
            }
        }
    }

    //protected void fillgrid()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(constr))
    //        {
    //            using (SqlCommand cmd = new SqlCommand("Get_District_hired_Type_Wise_Godown_Capacity_New", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                string selectedIDs = string.Join(",",
    //                    ddlGodownType.Items.Cast<ListItem>()
    //                    .Where(i => i.Selected)
    //                    .Select(i => i.Value)
    //                    );
    //                if (string.IsNullOrEmpty(selectedIDs))
    //                    selectedIDs = "0";   // All commodities
    //                else
    //                    //selectedIDs = "'" + selectedIDs + "'";
    //                    //cmd.Parameters.AddWithValue("@CommodityIDs", selectedIDs);
    //                    cmd.Parameters.Add("@Hired_Type", SqlDbType.VarChar).Value = selectedIDs;
    //                //cmd.Parameters.AddWithValue("@Hired_Type", string.IsNullOrEmpty(selectedIDs) ? "0" : "'" + selectedIDs + "'");
    //                cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue == "--Select--" ? "0" : ddlstorageType.SelectedValue);

    //                SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //                DataTable dt = new DataTable();
    //                sda.Fill(dt);

    //                if (dt.Rows.Count > 0)
    //                {
    //                    GridView1.DataSource = dt;
    //                    GridView1.DataBind();

    //                    // ESSENTIAL FOR DATATABLES: Force <thead> and <tfoot> rendering
    //                    GridView1.UseAccessibleHeader = true;
    //                    if (GridView1.HeaderRow != null)
    //                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                    if (GridView1.FooterRow != null)
    //                        GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

    //                    GridView1.Caption = "<b>M.P. WAREHOUSING & LOGISTICS CORPORATION</b><br/>District Hired Type Godown Capacity";
    //                    GridView1.Columns[1].Visible = false; // Hide District_Id

    //                    GridView1.FooterRow.Cells[2].Text = "Total";
    //                    GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(r => r.Field<int>("TotalGodown")).ToString();
    //                    GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("GodownCapacity")).ToString();
    //                    GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
    //                    GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("currentlyvacantcapacity")).ToString();

    //                    ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
    //                }
    //                else
    //                {
    //                    GridView1.DataSource = null;
    //                    GridView1.DataBind();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string script = "alert('Error: " + ex.Message.Replace("'", "\\'") + "');";
    //        ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
    //    }

    //}

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_District_hired_Type_Wise_Godown_Capacity_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    string selectedIDs = string.Join(",",
                        ddlGodownType.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                        );

                    if (string.IsNullOrEmpty(selectedIDs))
                        selectedIDs = "0";
                    else
                        cmd.Parameters.Add("@Hired_Type", SqlDbType.VarChar).Value = selectedIDs;

                    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue == "--Select--" ? "0" : ddlstorageType.SelectedValue);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    //if (dt.Rows.Count > 0)
                    //{
                    //    GridView1.DataSource = dt;
                    //    GridView1.DataBind();

                    //    // ESSENTIAL: Force GridView to render <thead> and <tfoot> for jQuery DataTables
                    //    GridView1.UseAccessibleHeader = true;
                    //    if (GridView1.HeaderRow != null)
                    //        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //    if (GridView1.FooterRow != null)
                    //        GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

                    //    GridView1.Caption = "<b>M.P. WAREHOUSING & LOGISTICS CORPORATION</b><br/>District Hired Type Godown Capacity";
                    //    //GridView1.Columns[1].Visible = false; // Hide District_Id

                    //    // Calculate Totals for Footer
                    //    GridView1.FooterRow.Cells[3].Text = "Total";
                    //    GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(r => r.Field<int>("TotalGodown")).ToString();
                    //    GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("GodownCapacity")).ToString();
                    //    GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                    //    GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("currentlyvacantcapacity")).ToString();

                    //    // Apply grouping logic
                    //    ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                    //}
                    //else
                    //{
                    //    GridView1.DataSource = null;
                    //    GridView1.DataBind();
                    //}
                    //if (dt.Rows.Count > 0)
                    //{
                    //    GridView1.DataSource = dt;
                    //    GridView1.DataBind();

                    //    // ESSENTIAL FOR DATATABLES: Force <thead> and <tfoot> rendering
                    //    GridView1.UseAccessibleHeader = true;
                    //    if (GridView1.HeaderRow != null)
                    //        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //    if (GridView1.FooterRow != null)
                    //        GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

                    //    GridView1.Caption = "<b>M.P. WAREHOUSING & LOGISTICS CORPORATION</b><br/>District Hired Type Godown Capacity";
                    //    GridView1.Columns[1].Visible = false; // Hide District_Id

                    //    GridView1.FooterRow.Cells[2].Text = "Total";
                    //    GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(r => r.Field<int>("TotalGodown")).ToString();
                    //    GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("GodownCapacity")).ToString();
                    //    GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                    //    GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(r => r.Field<decimal>("currentlyvacantcapacity")).ToString();

                    //    ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                    //}
                    //else
                    //{
                    //    GridView1.DataSource = null;
                    //    GridView1.DataBind();
                    //}

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        GridView1.UseAccessibleHeader = true;

                        if (GridView1.HeaderRow != null)
                            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

                        if (GridView1.FooterRow != null)
                            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;

                        // Hide District_Id AFTER footer setup
                        GridView1.Columns[1].Visible = false;

                        // Totals
                        int totalGodown = dt.AsEnumerable().Sum(r => r.Field<int>("TotalGodown"));
                        decimal totalCapacity = dt.AsEnumerable().Sum(r => r.Field<decimal>("GodownCapacity"));
                        decimal totalStock = dt.AsEnumerable().Sum(r => r.Field<decimal>("Quantityofstockstoredinwarehouse"));
                        decimal totalVacant = dt.AsEnumerable().Sum(r => r.Field<decimal>("currentlyvacantcapacity"));

                        // Footer Safe Assignment
                        GridView1.FooterRow.Cells[2].Text = "Total";
                        GridView1.FooterRow.Cells[3].Text = totalGodown.ToString();
                        GridView1.FooterRow.Cells[4].Text = totalCapacity.ToString("N2");
                        GridView1.FooterRow.Cells[5].Text = totalStock.ToString("N2");
                        GridView1.FooterRow.Cells[6].Text = totalVacant.ToString("N2");
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "District_Id"));
            qtyTotal1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalGodown"));
            qtyTotal2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GodownCapacity"));
            qtyTotal3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantityofstockstoredinwarehouse"));
            qtyTotal4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "currentlyvacantcapacity"));
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "District_Id"))) newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
        {
            newRow = true;
            rowIndex = 0;
        }

        if (newRow)
        {
            GridView gv = (GridView)sender;
            GridViewRow totalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            totalRow.Font.Bold = true;
            totalRow.BackColor = Color.LightGray;

            TableCell cell = new TableCell { Text = "Sub Total", HorizontalAlign = HorizontalAlign.Right, ColumnSpan = 2 };
            totalRow.Cells.Add(cell);

            totalRow.Cells.Add(new TableCell { Text = qtyTotal1.ToString(), HorizontalAlign = HorizontalAlign.Right });
            totalRow.Cells.Add(new TableCell { Text = qtyTotal2.ToString(), HorizontalAlign = HorizontalAlign.Right });
            totalRow.Cells.Add(new TableCell { Text = qtyTotal3.ToString(), HorizontalAlign = HorizontalAlign.Right });
            totalRow.Cells.Add(new TableCell { Text = qtyTotal4.ToString(), HorizontalAlign = HorizontalAlign.Right });

            gv.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, totalRow);
            rowIndex++;
            qtyTotal1 = qtyTotal2 = qtyTotal3 = qtyTotal4 = 0;
        }
    }

    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (gridViewRows.Count == 0 || totalColumns == 0) return;
        int count = 1;
        ArrayList lst = new ArrayList { gridViewRows[0] };
        var ctrl = gridViewRows[0].Cells[startIndex];

        for (int i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text && ctrl.Text != "&nbsp;")
            {
                count++;
                nextTbCell.Visible = false;
                lst.Add(gridViewRows[i]);
            }
            else
            {
                if (count > 1)
                {
                    ctrl.RowSpan = count;
                    ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
                }
                count = 1;
                lst.Clear();
                ctrl = gridViewRows[i].Cells[startIndex];
                lst.Add(gridViewRows[i]);
            }
        }
        if (count > 1) ctrl.RowSpan = count;
    }

    protected void btnSubmit_Click(object sender, EventArgs e) { fillgrid(); }
    protected void btnCancel_Click(object sender, EventArgs e) { Response.Redirect(Request.RawUrl); }
}