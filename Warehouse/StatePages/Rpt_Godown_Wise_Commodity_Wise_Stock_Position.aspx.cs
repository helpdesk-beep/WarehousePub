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

public partial class StatePages_Rpt_Godown_Wise_Commodity_Wise_Stock_Position : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
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
    decimal qtyTotal17 = 0;
    decimal qtyTotal18 = 0;

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
    decimal grQtyTotal17 = 0;
    decimal grQtyTotal18 = 0;

    long storid = 0;
    int rowIndex = 1;


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCommodity();
            GetRegion();
            GetCropYear();
            FillDistrict();
        }
    }

    void GetCropYear()
    {
        try
        {
            qry = "select 'All' as CropText ,'2%' as CropYear union(select distinct CropYear, CropYear + '%' as CropYear from tbl_storage_Depositor_WHR_Relation where CropYear like('2%')) order by CropYear";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddlCropYear.DataSource = ds.Tables[0];
                ddlCropYear.DataTextField = "CropText";
                ddlCropYear.DataValueField = "CropText";
                ddlCropYear.DataBind();
                //ddlCropYear.Items.Insert(0, "--Select Crop Year--");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }

    private void FillDistrict()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + ddlregion.SelectedValue + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }

    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Fill_Branch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }

    public void FillCommodity()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Storage_Commodity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlcommodity.DataSource = cmd.ExecuteReader();
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, new ListItem("-- Select Commodity --", "0"));
            con.Close();
        }
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDistrict();
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchDetails();
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
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stock_Position_Godown_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cropyear", ddlCropYear.SelectedItem.ToString());
                
                if (ddldistrict.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DistrictId", 0);

                }
                else
                {
                cmd.Parameters.AddWithValue("@DistrictId", ddldistrict.SelectedValue);
                }
                if (ddlcommodity.SelectedValue == "-- Select Commodity --")
                {
                    cmd.Parameters.AddWithValue("@commodity", 0);

                }
                else
                {
                 cmd.Parameters.AddWithValue("@commodity", ddlcommodity.SelectedValue);
                }
                if (ddlbranch.SelectedValue == "-- Select Branch --")
                {
                    cmd.Parameters.AddWithValue("@BranchID", 0);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
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
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold;""> Branch Wise Commodity Wise Stock Position" + "</br> " + "Region Name" + " - " + ddlregion.SelectedItem.ToString() + "   -  " + "District Name" + " - " + ddldistrict.SelectedItem.ToString() + "</br> " + "Crop Year" + " - " + ddlCropYear.SelectedItem.ToString() + "   -  " + "Commodity Name" + " - " + ddlcommodity.SelectedItem.ToString();
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RecBags")).ToString();
                            //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("RecQty")).ToString();
                            //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DelBags")).ToString();
                            //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DelQty")).ToString();

                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BagBalance")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceQty")).ToString();

                            // ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);

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
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 4;
        row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 2;
        //cell.Text = "Received Balance";
        //row.Controls.Add(cell);

        //cell = new TableHeaderCell();
        //cell.ColumnSpan = 2;
        //cell.Text = "Delivered Balance" ;
        //row.Controls.Add(cell);


        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Balance (Qty in M.T)";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

       

    }
    
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}