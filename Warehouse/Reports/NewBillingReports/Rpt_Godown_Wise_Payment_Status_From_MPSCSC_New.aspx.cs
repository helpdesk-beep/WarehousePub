using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NewBillingReports_Rpt_Godown_Wise_Payment_Status_From_MPSCSC_New : System.Web.UI.Page
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
            filDivision();
            fillCropYear();
            GetCommodity();
            fillDivision();

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
        fillDivision();

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

        filBranch();
        fillDivision();
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
    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }
    private void fillCropYear()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            //query = "Select distinct CropYear from tbl_storage_Depositor_WHR_Relation where CropYear not in('All','Before 2009','Before 2013','Before 2014','','All','0')";
            query = "select distinct Financial_Year from tbl_Institution_Storage_Bill_Details where Financial_Year>='2019-2020'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "Financial_Year";
                ddlcropyear.DataValueField = "Financial_Year";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "All");
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
    public void GetCommodity()
    {
        string qry = "";
        qry = "select Distinct C.Commodity_Id,C.Commodity_Name from tbl_Institution_Storage_Bill_Details A INNER JOIN tbl_MetaData_STORAGE_COMMODITY C ON A.Commodity_Id = C.Commodity_Id Order by C.Commodity_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

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
    protected void fillDivision()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Payment_Status_From_MPSCSC", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddldivision.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@RegionID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", ddldivision.SelectedValue);
                }
                if (ddldistrict.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@DistrictID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue);
                }
                if (ddlbranch.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Branchid", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Branchid", ddlbranch.SelectedValue);
                }
                if (ddlgodowntype.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@GodownType", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GodownType", ddlgodowntype.SelectedValue);
                }
                if (ddlcropyear.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYear", ddlcropyear.SelectedValue);
                }

                if (ddlcommodity.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Commodity_Id", ddlcommodity.SelectedValue);
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
                            grddivision.DataSource = dt;
                            grddivision.DataBind();
                            divdivision.Visible = true;
                            grddivision.FooterRow.Style.Add("text-align", "Right");
                            grddivision.FooterRow.Cells[4].Text = "Total";
                            grddivision.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBill")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BillAmount")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalReceivedBill")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendinbillatMPSCSC")).ToString("#,##0.00");
                            grddivision.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendinbillAmountatMPSCSC")).ToString("#,##0.00");

                        }
                        else
                        {
                            grddivision.DataSource = null;
                            grddivision.DataBind();
                        }
                    }
                }
            }
        }
    }


    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillDivision();
    }


    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }

    //protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }
    //protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDivision();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());
            qtyTotal16 += tmpTotal16;
            grQtyTotal16 += tmpTotal16;
        }
    }
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "BranchId") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BranchId").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "BranchId") == null))
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
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }
    }
    protected void grdgdn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            //decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2010-11]").ToString());
            //decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2011-12]").ToString());
            //decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2012-13]").ToString());
            //decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2013-14]").ToString());
            //decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2014-15]").ToString());
            //decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2015-16]").ToString());
            //decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2016-17]").ToString());
            //decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2017-18]").ToString());
            //decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2018-19]").ToString());
            //decimal tmpTotal10 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2019-20]").ToString());
            //decimal tmpTotal11 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2020-21]").ToString());
            //decimal tmpTotal12 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2021-22]").ToString());
            //decimal tmpTotal13 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2022-23]").ToString());
            //decimal tmpTotal14 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2023-24]").ToString());
            //decimal tmpTotal15 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "[2024-25]").ToString());
            decimal tmpTotal16 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total").ToString());

            //qtyTotal1 += tmpTotal1;
            //qtyTotal2 += tmpTotal2;
            //qtyTotal3 += tmpTotal3;
            //qtyTotal4 += tmpTotal4;
            //qtyTotal5 += tmpTotal5;
            //qtyTotal6 += tmpTotal6;
            //qtyTotal7 += tmpTotal7;
            //qtyTotal8 += tmpTotal8;
            //qtyTotal9 += tmpTotal9;
            //qtyTotal10 += tmpTotal10;
            //qtyTotal11 += tmpTotal11;
            //qtyTotal12 += tmpTotal12;
            //qtyTotal13 += tmpTotal13;
            //qtyTotal14 += tmpTotal14;
            //qtyTotal15 += tmpTotal15;
            qtyTotal16 += tmpTotal16;

            //grQtyTotal1 += tmpTotal1;
            //grQtyTotal2 += tmpTotal2;
            //grQtyTotal3 += tmpTotal3;
            //grQtyTotal4 += tmpTotal4;
            //grQtyTotal5 += tmpTotal5;
            //grQtyTotal6 += tmpTotal6;
            //grQtyTotal7 += tmpTotal7;
            //grQtyTotal8 += tmpTotal8;
            //grQtyTotal9 += tmpTotal9;
            //grQtyTotal10 += tmpTotal10;
            //grQtyTotal11 += tmpTotal11;
            //grQtyTotal12 += tmpTotal12;
            //grQtyTotal13 += tmpTotal13;
            //grQtyTotal14 += tmpTotal14;
            //grQtyTotal15 += tmpTotal15;
            grQtyTotal16 += tmpTotal16;


        }
    }
    protected void grdgdn_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (storid != Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((storid != "0") && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
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
            HeaderCell.Text = qtyTotal16.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal16 = 0;

        }
    }


}