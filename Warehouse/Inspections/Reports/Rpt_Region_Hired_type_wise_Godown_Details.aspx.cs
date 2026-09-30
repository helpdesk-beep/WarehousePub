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
using System.Globalization;

public partial class Inspections_Reports_Rpt_Region_Hired_type_wise_Godown_Details : System.Web.UI.Page
{
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

    long storid = 0;
    int rowIndex = 1;
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString();
            FillStorageType();
            FillGodownType();
            filDivision();
            fillDivision();
            FillStorageType();
            FillGodownType();
            //fillgrid();
        }
    }
    private void filDivision()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Distinct Region_ID,Regionnm from tbl_MetaData_DISTRICT Order By Regionnm ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.Items.Clear();
                ddldivision.DataSource = ds.Tables[0];
                ddldivision.DataTextField = "Regionnm";
                ddldivision.DataValueField = "Region_ID";
                ddldivision.DataBind();
                ddldivision.Items.Insert(0, "All");
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
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        filDistrict();
        //fillgrid();
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            filldistrictgrid();
        }
        //else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        //{
        //    fillDistrictgrid();
        //}
        //else if (ddlbranch.SelectedValue != "All")
        //{
        //    fillGodowngrid();
        //}
    }
    private void filDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select District_Id,District_Name from tbl_MetaData_DISTRICT where Region_id='" + ddldivision.SelectedValue + "' Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "All");
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
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divregion.Visible = false;
        filBranch();
        //if (ddldistrict.SelectedValue == "All")
        //{
        //    fillgrid();
        //}
        //else
        //{
        //    fillDistrictgrid();
        //}
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            filldistrictgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillBranchgrid();
        }
        //else if (ddlbranch.SelectedValue != "All")
        //{
        //    fillGodowngrid();
        //}

    }
    private void filBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue.ToString() + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "All");
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
    private void FillStorageType()
    {

        string query = " select distinct Storage_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlstorageType.DataSource = ds.Tables[0];
            ddlstorageType.DataTextField = "Storage_Type";
            ddlstorageType.DataValueField = "Storage_Type";
            ddlstorageType.DataBind();
            //ddlstorageType.Items.Insert(0, "All");
            ddlstorageType.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    private void FillGodownType()
    {

        string query = "select distinct Hired_Type from tbl_MetaData_GODOWN_2018";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodownType.DataSource = ds.Tables[0];
            ddlGodownType.DataTextField = "Hired_Type";
            ddlGodownType.DataValueField = "Hired_Type";
            ddlGodownType.DataBind();
            ddlGodownType.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void fillDivision()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_hired_Type_Wise_Godown_Capacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlstorageType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue);
                }
                if (ddlGodownType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlGodownType.SelectedValue);
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
                            //divdivision.Visible = true;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            divdivision.Visible = true;
                            divdistrict.Visible = false;
                            divBranch.Visible = false;
                            DivGodown.Visible = false;
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "District Hired Type Godown Capacity";//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            GridView1.Columns[1].Visible = false;
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("currentlyvacantcapacity")).ToString();
                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            decimal tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalGodown").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GodownCapacity").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantityofstockstoredinwarehouse").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "currentlyvacantcapacity").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Averg").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(tmpTotal1.ToString())*100/ Convert.ToDecimal(tmpTotal2.ToString());


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += 0;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += 0;
        }
        //if (qtyTotal1 != 0)
        //    qtyTotal3 = Math.Round(qtyTotal2 * 100 / qtyTotal1, 2);
        //else
        //    qtyTotal3 = 0;

    }
    void ShowingGroupingDataInGridView(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
    {
        if (totalColumns == 0) return;
        int i, count = 1;
        ArrayList lst = new ArrayList();
        lst.Add(gridViewRows[0]);
        var ctrl = gridViewRows[0].Cells[startIndex];
        for (i = 1; i < gridViewRows.Count; i++)
        {
            TableCell nextTbCell = gridViewRows[i].Cells[startIndex];
            if (ctrl.Text == nextTbCell.Text)
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
        if (count > 1)
        {
            ctrl.RowSpan = count;
            ShowingGroupingDataInGridView(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //TableHeaderCell cell = new TableHeaderCell();
        //cell.Text = "";
        //cell.ColumnSpan = 5;
        //row.Controls.Add(cell);

        //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        //GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") == null))
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
            HeaderCell.ColumnSpan = 2;

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

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal5.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            //qtyTotal5 = 0;

        }


    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
    protected void ddlstorageType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            filldistrictgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillBranchgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }
    protected void ddlGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            filldistrictgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillBranchgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }
    protected void filldistrictgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_hired_Type_Wise_Godown_Capacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlstorageType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue);
                }
                if (ddlGodownType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlGodownType.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@Region_ID", ddldivision.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            divdivision.Visible = false;
                            divdistrict.Visible = true;
                            DivGodown.Visible = false;
                            divBranch.Visible = false;
                            // DivGodown.Visible = false;
                            //divdivision.Visible = false;
                            GridView2.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView2.FooterRow.Cells[2].Text = "Total";
                            GridView2.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            GridView2.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString();
                            GridView2.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                            GridView2.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("currentlyvacantcapacity")).ToString();
                        }
                        else
                        {
                            GridView2.DataSource = null;
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            decimal tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalGodown").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GodownCapacity").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantityofstockstoredinwarehouse").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "currentlyvacantcapacity").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Averg").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(tmpTotal1.ToString())*100/ Convert.ToDecimal(tmpTotal2.ToString());


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += 0;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += 0;
        }
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
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
            HeaderCell.ColumnSpan = 2;

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

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal5.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            //qtyTotal5 = 0;

        }
    }

    protected void fillBranchgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Branch_hired_Type_Wise_Godown_Capacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlstorageType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue);
                }
                if (ddlGodownType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlGodownType.SelectedValue);
                }
                if (ddldistrict.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@District_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
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
                            grdbranch.DataSource = dt;
                            grdbranch.DataBind();
                            divdistrict.Visible = false;
                            divdivision.Visible = false;
                            //divdistrict.Visible = true;
                            divBranch.Visible = true;
                            DivGodown.Visible = false;
                            //divdivision.Visible = false;
                            grdbranch.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            grdbranch.FooterRow.Cells[2].Text = "Total";
                            grdbranch.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            grdbranch.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString();
                            grdbranch.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                            grdbranch.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("currentlyvacantcapacity")).ToString();
                        }
                        else
                        {
                            grdbranch.DataSource = null;
                            grdbranch.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillGodowngrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_hired_Type_Wise_Godown_Capacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlstorageType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Storage_Type", ddlstorageType.SelectedValue);
                }
                if (ddlGodownType.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlGodownType.SelectedValue);
                }
                if (ddlbranch.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
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
                            GrdGodown.DataSource = dt;
                            GrdGodown.DataBind();
                            divdistrict.Visible = false;
                            divdivision.Visible = false;
                            //divdistrict.Visible = true;
                            divBranch.Visible = false;
                            DivGodown.Visible = true;
                            //divdivision.Visible = false;
                            GrdGodown.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GrdGodown.FooterRow.Cells[2].Text = "Total";
                            //GrdGodown.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalGodown")).ToString();
                            GrdGodown.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("GodownCapacity")).ToString();
                            GrdGodown.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantityofstockstoredinwarehouse")).ToString();
                            GrdGodown.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("currentlyvacantcapacity")).ToString();
                        }
                        else
                        {
                            grdbranch.DataSource = null;
                            grdbranch.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void grdbranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString());
            decimal tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalGodown").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "GodownCapacity").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Quantityofstockstoredinwarehouse").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "currentlyvacantcapacity").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Averg").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(tmpTotal1.ToString())*100/ Convert.ToDecimal(tmpTotal2.ToString());


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += 0;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += 0;
        }
    }

    protected void grdbranch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "BranchId") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString()))
                newRow = true;
        }
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "BranchId") == null))
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
            HeaderCell.ColumnSpan = 2;

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

            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal5.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            //qtyTotal5 = 0;

        }
    }

    protected void GrdGodown_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GrdGodown_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedValue == "All")
        {
            fillDivision();
        }
        else if (ddldivision.SelectedValue != "All" & ddldistrict.SelectedValue == "All")
        {
            filldistrictgrid();
        }
        else if (ddldistrict.SelectedValue != "All" & ddlbranch.SelectedValue == "All")
        {
            fillBranchgrid();
        }
        else if (ddlbranch.SelectedValue != "All")
        {
            fillGodowngrid();
        }
    }
}