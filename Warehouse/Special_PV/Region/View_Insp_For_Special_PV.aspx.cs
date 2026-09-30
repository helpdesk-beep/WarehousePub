using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Collections;

public partial class Special_PV_Region_View_Insp_For_Special_PV : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();

    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Region_ID = "";
    string Insp_ID = "";
    string Veri_ID = "";

    int AqtyTotal = 0;
    int AgrQtyTotal = 0;
    decimal AqtyTotal1 = 0;
    decimal AqtyTotal2 = 0;
    decimal AqtyTotal3 = 0;
    decimal AqtyTotal4 = 0;
    decimal AqtyTotal5 = 0;

    decimal AgrQtyTotal1 = 0;
    decimal AgrQtyTotal2 = 0;
    decimal AgrQtyTotal3 = 0;
    decimal AgrQtyTotal4 = 0;
    decimal AgrQtyTotal5 = 0;

    long Astorid = 0;
    int ArowIndex = 1;

    int BqtyTotal = 0;
    int BgrQtyTotal = 0;
    decimal BqtyTotal1 = 0;
    decimal BqtyTotal2 = 0;
    decimal BqtyTotal3 = 0;
    decimal BqtyTotal4 = 0;
    decimal BqtyTotal5 = 0;

    decimal BgrQtyTotal1 = 0;
    decimal BgrQtyTotal2 = 0;
    decimal BgrQtyTotal3 = 0;
    decimal BgrQtyTotal4 = 0;
    decimal BgrQtyTotal5 = 0;

    //long storid = 0;
    long Bstorid = 0;
    int BrowIndex = 1;

    int CuntyqtyTotal = 0;
    int CuntygrQtyTotal = 0;
    decimal CuntyqtyTotal1 = 0;
    decimal CuntyqtyTotal2 = 0;
    decimal CuntyqtyTotal3 = 0;
    decimal CuntyqtyTotal4 = 0;
    decimal CuntyqtyTotal5 = 0;

    decimal CuntygrQtyTotal1 = 0;
    decimal CuntygrQtyTotal2 = 0;
    decimal CuntygrQtyTotal3 = 0;
    decimal CuntygrQtyTotal4 = 0;
    decimal CuntygrQtyTotal5 = 0;

    long Cuntystorid = 0;
    long Cuntygodownid = 0;
    int CuntyrowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            filDistrict();
        }
    }
    private void filDistrict()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select District_Id,District_Name from tbl_MetaData_DISTRICT where Region_ID='"+ Session["UserId"] .ToString()+ "'";
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
                ddldistrict.Items.Insert(0, "--Select--");
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

    private void filBranch()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlBranch.Items.Clear();
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "--Select--");
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
    public void fillCountingSheat()
    {
        SqlCommand cmd = new SqlCommand("Get_Complite_Special_Inspection_Details", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            divshow.Visible = true;
            GD_StackBal.Columns[1].Visible = false;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Counting Sheat Details Godown Wise: " + "</b> ";
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[12].Text = "Total";
            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
            GD_StackBal.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
            ShowingGroupingDataInGridViewCountingsheat(GD_StackBal.Rows, 0, 0);
        }
        else
        {
            divshow.Visible = true;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            string strMsg = "इस ब्रांच के द्वारा वर्ष " + ddlBranch.SelectedItem.Value + " की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले ब्रांच के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);
        }
    }
    void ShowingGroupingDataInGridViewCountingsheat(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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
                    ShowingGroupingDataInGridViewCountingsheat(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
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
            ShowingGroupingDataInGridViewCountingsheat(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Cuntystorid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString());
            Cuntygodownid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString());
            int tmpTotal = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Number_Of_Block").ToString());
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "No_of_Bags").ToString());
            int tmpTotal2 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Up").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Below").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total_Bags").ToString());
            int tmpTotal5 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Spillage_bag").ToString());

            CuntyqtyTotal += tmpTotal;
            CuntyqtyTotal1 += tmpTotal1;
            CuntyqtyTotal2 += tmpTotal2;
            CuntyqtyTotal3 += tmpTotal3;
            CuntyqtyTotal4 += tmpTotal4;
            CuntyqtyTotal5 += tmpTotal5;

            CuntygrQtyTotal += tmpTotal;
            CuntygrQtyTotal1 += tmpTotal1;
            CuntygrQtyTotal2 += tmpTotal2;
            CuntygrQtyTotal3 += tmpTotal3;
            CuntygrQtyTotal4 += tmpTotal4;
            CuntygrQtyTotal5 += tmpTotal5;

        }

    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;

        //if ((Cuntystorid >= 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") != null))
        //{
        //    if (Cuntystorid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "stack_id").ToString()))
        //        newRow = true;
        //}
        //if ((Cuntystorid >= 0) && (DataBinder.Eval(e.Row.DataItem, "stack_id") == null) && (Cuntygodownid > 0))
        //{
        //    newRow = true;
        //    CuntyrowIndex = 0;
        //}
        //if (newRow)
        //{
        //    GridView GridView1 = (GridView)sender;
        //    GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        //    NewTotalRow.Font.Bold = true;
        //    // NewTotalRow.BackColor = System.Drawing.Color.Gray;
        //    NewTotalRow.ForeColor = System.Drawing.Color.Black;
        //    TableCell HeaderCell = new TableCell();
        //    HeaderCell.Text = "Sub Total";
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Right;
        //    HeaderCell.ColumnSpan = 12;


        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 3;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal1.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //    //HeaderCell.ColumnSpan = 4;
        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal2.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal3.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal4.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    NewTotalRow.Cells.Add(HeaderCell);
        //    HeaderCell = new TableCell();
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderCell.Text = CuntyqtyTotal5.ToString();
        //    NewTotalRow.Cells.Add(HeaderCell);

        //    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + CuntyrowIndex, NewTotalRow);
        //    CuntyrowIndex++;
        //    CuntyqtyTotal = 0;
        //    CuntyqtyTotal1 = 0;
        //    CuntyqtyTotal2 = 0;
        //    CuntyqtyTotal3 = 0;
        //    CuntyqtyTotal4 = 0;
        //    CuntyqtyTotal5 = 0;


        //}
        if ((Cuntygodownid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") != null))
        {
            if (Cuntygodownid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Godown_ID").ToString()))
                newRow = true;
        }
        if ((Cuntygodownid > 0) && (DataBinder.Eval(e.Row.DataItem, "Godown_ID") == null))
        {
            newRow = true;
            CuntyrowIndex = 0;
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
            HeaderCell.ColumnSpan = 12;


            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal3.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal4.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.Text = CuntyqtyTotal5.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + CuntyrowIndex, NewTotalRow);
            CuntyrowIndex++;
            CuntyqtyTotal = 0;
            CuntyqtyTotal1 = 0;
            CuntyqtyTotal2 = 0;
            CuntyqtyTotal3 = 0;
            CuntyqtyTotal4 = 0;
            CuntyqtyTotal5 = 0;


        }
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}