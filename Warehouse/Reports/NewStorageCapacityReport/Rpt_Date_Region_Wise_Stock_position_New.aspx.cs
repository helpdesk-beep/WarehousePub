using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SRV_Storage_Reports_Inspenctions_Rpt_Date_Region_Wise_Stock_position_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    decimal qtyTotal = 0;
    decimal grQtyTotal = 0;
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
    decimal qtyTotal11 = 0;
    decimal qtyTotal12 = 0;
    decimal qtyTotal13 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;

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
    decimal grQtyTotal11 = 0;
    decimal grQtyTotal12 = 0;
    decimal grQtyTotal13 = 0;
    decimal grQtyTotal14 = 0;
    decimal grQtyTotal15 = 0;
    decimal grQtyTotal16 = 0;
    string storid = "0";
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillComodity();
            fillDepositor();
            fillGodownType();
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
        //Session["Date"] = txtpaymentdate.Text.ToString();
        //Session["Commodity"] = ddlcommodity.SelectedValue.ToString();
        if (ddldistrict.SelectedValue == "1")
        {
            fillgrid();
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
                ddlcommodity.Items.Clear();
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                //ddlcommodity.Items.Insert(0, "All");
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
            query = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.Items.Clear();
                ddlDepositor.DataSource = ds.Tables[0];
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                //ddlDepositor.Items.Insert(0, "All");
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

    private void fillGodownType()
    {
        try
        {

            string query = "";
            query = "Select  Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodowntype.Items.Clear();
                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                //ddlgodowntype.Items.Insert(0, "All");
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
            using (SqlCommand cmd = new SqlCommand("Get_Date_Region_CropYear_Wise_Stock_position_New", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));

                string selectedIDCommodityIDs = string.Join(",",
                        ddlcommodity.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDCommodityIDs))
                    selectedIDCommodityIDs = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Commodity_Id", selectedIDCommodityIDs);

                string selectedIDDepositor = string.Join(",",
                        ddlDepositor.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDDepositor))
                    selectedIDDepositor = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Depositor_ID", selectedIDDepositor);

                string selectedIDgodowntype = string.Join(",",
                        ddlgodowntype.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value)
                );

                if (string.IsNullOrEmpty(selectedIDgodowntype))
                    selectedIDgodowntype = "0";   // All commodities
                else
                    //selectedIDs = "'" + selectedIDs + "'";
                    cmd.Parameters.AddWithValue("@Hired_Type", selectedIDgodowntype);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divdivision.Visible = true;
                            divDepositor.Visible = false;
                            divdistrivt.Visible = false;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Columns[2].Visible = false;
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("A")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("B")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("C")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("D")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("E")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("F")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("G")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("H")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("I")).ToString("#,##0.00");
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                            Session["Date"] = txtpaymentdate.Text.ToString();
                            //Session["Commodity"] = ddlComodity.SelectedValue.ToString();
                            Session["DepositorID"] = ddlDepositor.SelectedValue.ToString();
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2010-11]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2011-12]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2012-13]").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2013-14]").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2014-15]").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2015-16]").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "A").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "B").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "C").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "D").ToString());
            decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "E").ToString());
            decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "F").ToString());
            decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "G").ToString());
            decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "H").ToString());
            decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "I").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            //qtyTotal1 += tmpTotal1;
            //qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            //qtyTotal4 += tmpTotal4;
            //qtyTotal5 += tmpTotal5;
            //qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += tmpTotal11;
            qtyTotal12 += tmpTotal12;
            qtyTotal13 += tmpTotal13;
            qtyTotal14 += tmpTotal14;
            qtyTotal15 += tmpTotal15;
            qtyTotal16 += tmpTotal16;

            //grQtyTotal1 += tmpTotal1;
            //grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            //grQtyTotal4 += tmpTotal4;
            //grQtyTotal5 += tmpTotal5;
            //grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += tmpTotal11;
            grQtyTotal12 += tmpTotal12;
            grQtyTotal13 += tmpTotal13;
            grQtyTotal14 += tmpTotal14;
            grQtyTotal15 += tmpTotal15;
            grQtyTotal16 += tmpTotal16;


        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Region_ID") == null))
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal7.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal8.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal9.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal10.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal11.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal12.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal13.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal14.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal15.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal7 = 0;
            qtyTotal8 = 0;
            qtyTotal9 = 0;
            qtyTotal10 = 0;
            qtyTotal11 = 0;
            qtyTotal12 = 0;
            qtyTotal13 = 0;
            qtyTotal14 = 0;
            qtyTotal15 = 0;
            qtyTotal16 = 0;

        }
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {

        //fillgrid();
        //fillGodowngrid();
    }
    //protected void ddlCommoditytype_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    //protected void dllGodownType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //        fillgrid();
    //}

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "1")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue == "2")
        {
            fillDistrictGrd();
        }
        else if (ddldistrict.SelectedValue == "3")
        {
            fillDepositorGrd();
        }
    }

    protected void fillDistrictGrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Date_District_CropYear_Wise_Stock_position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));

                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }
                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
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
                            divdivision.Visible = false;
                            divDepositor.Visible = false;
                            divdistrivt.Visible = true;
                            GrdDistrict.DataSource = dt;
                            GrdDistrict.DataBind();
                            GrdDistrict.Columns[2].Visible = false;
                            GrdDistrict.FooterRow.Style.Add("text-align", "right");
                            GrdDistrict.FooterRow.Cells[3].Text = "Total";
                            GrdDistrict.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("A")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("B")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("C")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("D")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("E")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("F")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("G")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("H")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("I")).ToString("#,##0.00");
                            GrdDistrict.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                            Session["Date"] = txtpaymentdate.Text.ToString();
                            //Session["Commodity"] = ddlComodity.SelectedValue.ToString();
                            Session["DepositorID"] = ddlDepositor.SelectedValue.ToString();
                        }
                        else
                        {
                            GrdDistrict.DataSource = null;
                            GrdDistrict.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GrdDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2010-11]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2011-12]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2012-13]").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2013-14]").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2014-15]").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2015-16]").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "A").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "B").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "C").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "D").ToString());
            decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "E").ToString());
            decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "F").ToString());
            decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "G").ToString());
            decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "H").ToString());
            decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "I").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            //qtyTotal1 += tmpTotal1;
            //qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            //qtyTotal4 += tmpTotal4;
            //qtyTotal5 += tmpTotal5;
            //qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += tmpTotal11;
            qtyTotal12 += tmpTotal12;
            qtyTotal13 += tmpTotal13;
            qtyTotal14 += tmpTotal14;
            qtyTotal15 += tmpTotal15;
            qtyTotal16 += tmpTotal16;

            //grQtyTotal1 += tmpTotal1;
            //grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            //grQtyTotal4 += tmpTotal4;
            //grQtyTotal5 += tmpTotal5;
            //grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += tmpTotal11;
            grQtyTotal12 += tmpTotal12;
            grQtyTotal13 += tmpTotal13;
            grQtyTotal14 += tmpTotal14;
            grQtyTotal15 += tmpTotal15;
            grQtyTotal16 += tmpTotal16;


        }
    }

    protected void GrdDistrict_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "District_Id") == null))
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal7.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal8.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal9.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal10.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal11.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal12.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal13.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal14.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal15.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GrdDistrict.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal7 = 0;
            qtyTotal8 = 0;
            qtyTotal9 = 0;
            qtyTotal10 = 0;
            qtyTotal11 = 0;
            qtyTotal12 = 0;
            qtyTotal13 = 0;
            qtyTotal14 = 0;
            qtyTotal15 = 0;
            qtyTotal16 = 0;

        }
    }

    protected void fillDepositorGrd()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Date_Depositor_CropYear_Wise_Stock_position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtpaymentdate.Text));

                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
                }
                if (ddlDepositor.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_ID", ddlDepositor.SelectedValue);
                }
                if (ddlgodowntype.SelectedValue == "All")
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
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
                            divdivision.Visible = false;
                            divdistrivt.Visible = false;
                            divDepositor.Visible = true;
                            GrdDepositor.DataSource = dt;
                            GrdDepositor.DataBind();
                            GrdDepositor.Columns[2].Visible = false;
                            GrdDepositor.FooterRow.Style.Add("text-align", "right");
                            GrdDepositor.FooterRow.Cells[3].Text = "Total";
                            GrdDepositor.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("A")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("B")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("C")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("D")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("E")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("F")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("G")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("H")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("I")).ToString("#,##0.00");
                            GrdDepositor.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total")).ToString("#,##0.00");
                            Session["Date"] = txtpaymentdate.Text.ToString();
                            //Session["Commodity"] = ddlComodity.SelectedValue.ToString();
                            Session["DepositorID"] = ddlDepositor.SelectedValue.ToString();
                        }
                        else
                        {
                            GrdDepositor.DataSource = null;
                            GrdDepositor.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GrdDepositor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DepositorID").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2010-11]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2011-12]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2012-13]").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2013-14]").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2014-15]").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2015-16]").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "A").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "B").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "C").ToString());
            decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "D").ToString());
            decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "E").ToString());
            decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "F").ToString());
            decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "G").ToString());
            decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "H").ToString());
            decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "I").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            //qtyTotal1 += tmpTotal1;
            //qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            //qtyTotal4 += tmpTotal4;
            //qtyTotal5 += tmpTotal5;
            //qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9 += tmpTotal9;
            qtyTotal10 += tmpTotal10;
            qtyTotal11 += tmpTotal11;
            qtyTotal12 += tmpTotal12;
            qtyTotal13 += tmpTotal13;
            qtyTotal14 += tmpTotal14;
            qtyTotal15 += tmpTotal15;
            qtyTotal16 += tmpTotal16;

            //grQtyTotal1 += tmpTotal1;
            //grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            //grQtyTotal4 += tmpTotal4;
            //grQtyTotal5 += tmpTotal5;
            //grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9 += tmpTotal9;
            grQtyTotal10 += tmpTotal10;
            grQtyTotal11 += tmpTotal11;
            grQtyTotal12 += tmpTotal12;
            grQtyTotal13 += tmpTotal13;
            grQtyTotal14 += tmpTotal14;
            grQtyTotal15 += tmpTotal15;
            grQtyTotal16 += tmpTotal16;


        }
    }

    protected void GrdDepositor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "DepositorID") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DepositorID").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "DepositorID") == null))
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal7.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal8.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal9.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal10.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal11.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal12.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal13.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal14.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal15.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GrdDepositor.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal7 = 0;
            qtyTotal8 = 0;
            qtyTotal9 = 0;
            qtyTotal10 = 0;
            qtyTotal11 = 0;
            qtyTotal12 = 0;
            qtyTotal13 = 0;
            qtyTotal14 = 0;
            qtyTotal15 = 0;
            qtyTotal16 = 0;

        }
    }
}