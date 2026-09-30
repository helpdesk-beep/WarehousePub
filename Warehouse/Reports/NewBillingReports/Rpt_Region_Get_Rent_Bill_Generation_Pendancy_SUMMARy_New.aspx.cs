using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NewBillingReports_Rpt_Region_Get_Rent_Bill_Generation_Pendancy_SUMMARy_New : System.Web.UI.Page
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
            fillFinasncialYear();
            fillgrid();
        }
    }
    private void fillFinasncialYear()
    {
        try
        {
            string query = "";

            query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where From_Date>='08/01/2020' and Financial_Year!='' AND Financial_Year>'2019-2020'";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlfinancial.Items.Clear();
                ddlfinancial.DataSource = ds.Tables[0];
                ddlfinancial.DataTextField = "Financial_Year";
                ddlfinancial.DataValueField = "Financial_Year";
                ddlfinancial.DataBind();
                ddlfinancial.Items.Insert(0, "--Select--");
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

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_Rent_Bill_Generation_Pendancy_SUMMARY", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlfinancial.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYEar", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYEar", ddlfinancial.SelectedValue);
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
                            divdivision.Visible = true;
                            DivDistrict.Visible = false;
                            DivBranch.Visible = false;
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalSCBillGenerated")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRentBillGenerated")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingRentBillForGenerate")).ToString();

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

    protected void fillgridForDistrict()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Rent_Bill_Generation_Pendancy_SUMMARY", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlfinancial.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYEar", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYEar", ddlfinancial.SelectedValue);
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
                            DivDistrict.Visible = true;
                            DivBranch.Visible = false;
                            GrdDistrict.DataSource = dt;
                            GrdDistrict.DataBind();
                            GrdDistrict.FooterRow.Style.Add("text-align", "right");
                            GrdDistrict.FooterRow.Cells[1].Text = "Total";
                            GrdDistrict.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalSCBillGenerated")).ToString();
                            GrdDistrict.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRentBillGenerated")).ToString();
                            GrdDistrict.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingRentBillForGenerate")).ToString();

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
    protected void fillgridForBranch()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Branch_Wise_Rent_Bill_Generation_Pendancy_SUMMARY", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlfinancial.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@CropYEar", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CropYEar", ddlfinancial.SelectedValue);
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
                            DivDistrict.Visible = false;
                            DivBranch.Visible = true;
                            GrdBranch.DataSource = dt;
                            GrdBranch.DataBind();
                            GrdBranch.FooterRow.Style.Add("text-align", "right");
                            GrdBranch.FooterRow.Cells[3].Text = "Total";
                            GrdBranch.Columns[2].Visible = false;
                            GrdBranch.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalSCBillGenerated")).ToString();
                            GrdBranch.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRentBillGenerated")).ToString();
                            GrdBranch.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingRentBillForGenerate")).ToString();

                        }
                        else
                        {
                            GrdBranch.DataSource = null;
                            GrdBranch.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GrdBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2010-11]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2011-12]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2012-13]").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalSCBillGenerated").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalRentBillGenerated").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingRentBillForGenerate").ToString());

            //qtyTotal1 += tmpTotal1;
            //qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;

            //qtyTotal16 += tmpTotal16;

            //grQtyTotal1 += tmpTotal1;
            //grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;

            //grQtyTotal16 += tmpTotal16;


        }
    }

    protected void GrdBranch_RowCreated(object sender, GridViewRowEventArgs e)
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
            HeaderCell.Text = qtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal6.ToString();
            NewTotalRow.Cells.Add(HeaderCell);


            //NewTotalRow.Cells.Add(HeaderCell);
            //HeaderCell = new TableCell();
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.Text = qtyTotal16.ToString();
            //NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;
            qtyTotal6 = 0;

            //qtyTotal16 = 0;

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "1")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue == "2")
        {
            fillgridForDistrict();
        }
        else if (ddldistrict.SelectedValue == "3")
        {
            fillgridForBranch();
        }
    }



    protected void ddlfinancial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue == "1")
        {
            fillgrid();
        }
        else if (ddldistrict.SelectedValue == "2")
        {
            fillgridForDistrict();
        }
        else if (ddldistrict.SelectedValue == "3")
        {
            fillgridForBranch();
        }
    }
}