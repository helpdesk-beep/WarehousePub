using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Linq;
public partial class Special_PV_State_Pendency_At_BM_And_RM : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";

    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;
    decimal qtyTotal14 = 0;
    decimal qtyTotal15 = 0;
    decimal qtyTotal16 = 0;
    decimal qtyTotal25 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
    decimal grQtyTotal16 = 0;


    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            GetRegion();
            fillRegiongrid();
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT Distinct Region_ID,Regionnm FROM tbl_MetaData_DISTRICT  order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "All");
        }
        else
        {
            ddlRegion.Items.Insert(0, "All");
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID = '" + ddlRegion.SelectedValue + "' order by District_Name ";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "All");
        }
        else
        {
            ddldistrict.Items.Insert(0, "All");
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDist();
        // fillDistrictgrid();
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "All");
        }
        else
        {
            ddlBranch.Items.Insert(0, "All");
        }
    }
    protected void fillRegiongrid()
    {
        Decimal opcloavg = 0;
        Decimal opcloavgpms = 0;
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Region_Wise_Special_PV_Status", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlRegion.SelectedItem.ToString() == "All")
                {
                    cmd.Parameters.AddWithValue("@Region_ID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
                }
                //cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            divRegion.Visible = true;
                            grdRegion.DataSource = dt;
                            grdRegion.DataBind();
                            //GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "'Owned','PVT.PEG','BOT-AUB','CWC','Steel Silo','Hired','Tribal Schemes' Godown Capacity and available Stock Position in M.T.";//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            grdRegion.Columns[0].Visible = false;
                            //grdRegion.Columns[3].Visible = false;
                            //GridView1.columns.RemoveAt(1);
                            grdRegion.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            grdRegion.FooterRow.Cells[3].Text = "Total";
                            grdRegion.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalBags")).ToString();
                            grdRegion.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Blance_As_Per_PV_By_Branch")).ToString();
                            grdRegion.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DiffirenceOnlineandPV")).ToString();
                            ShowingGroupingDataInGridView1(grdRegion.Rows, 0, 0);

                        }
                        else
                        {
                            divRegion.Visible = false;
                            grdRegion.DataSource = null;
                            grdRegion.DataBind();
                        }
                    }
                }
            }
        }
    }
    void ShowingGroupingDataInGridView1(GridViewRowCollection gridViewRows, int startIndex, int totalColumns)
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
                    ShowingGroupingDataInGridView1(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
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
            ShowingGroupingDataInGridView1(new GridViewRowCollection(lst), startIndex + 1, totalColumns - 1);
        }
        count = 1;
        lst.Clear();
    }
    protected void grdRegion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID").ToString());
            decimal tmpTotal11 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalBags").ToString());
            decimal tmpTotal12 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Blance_As_Per_PV_By_Branch").ToString());
            decimal tmpTotal13 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DiffirenceOnlineandPV").ToString());

            qtyTotal14 += tmpTotal11;
            qtyTotal15 += tmpTotal12;
            qtyTotal16 += tmpTotal13;
            // qtyTotal4 += tmpTotal4;
            qtyTotal25 += 0;

            grQtyTotal7 += qtyTotal14;
            grQtyTotal8 += qtyTotal15;
            grQtyTotal9 += qtyTotal16;
            //grQtyTotal4 += tmpTotal4;
            grQtyTotal16 += 0;
        }
    }
    protected void grdRegion_RowCreated(object sender, GridViewRowEventArgs e)
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
            GridView grdRegion = (GridView)sender;
            GridViewRow NewTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            NewTotalRow.Font.Bold = true;
            // NewTotalRow.BackColor = System.Drawing.Color.Gray;
            NewTotalRow.ForeColor = System.Drawing.Color.Black;
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sub Total";
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.ColumnSpan = 3;

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

            grdRegion.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal14 = 0;
            qtyTotal15 = 0;
            qtyTotal16 = 0;
        }
    }
    protected void grdRegion_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 8;
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        grdRegion.HeaderRow.Parent.Controls.AddAt(0, row);
    }
}