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
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
using System.Text;
using System.Globalization;

public partial class Region_States_Rpt_Get_District_Wise_Prapatra_II : System.Web.UI.Page
{
   
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;

    decimal qtyTotal = 0;
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
    decimal qtyTotal19 = 0;
    decimal qtyTotal20 = 0;
    decimal qtyTotal21 = 0;
    decimal qtyTotal22 = 0;
    decimal qtyTotal23 = 0;
    decimal qtyTotal24 = 0;
    decimal qtyTotal25 = 0;
    decimal qtyTotal26 = 0;
    decimal qtyTotal27 = 0;
    decimal qtyTotal28 = 0;
    decimal qtyTotal29 = 0;
    decimal qtyTotal30 = 0;
    decimal qtyTotal31 = 0;
    decimal qtyTotal32 = 0;
    decimal qtyTotal33 = 0;
    decimal qtyTotal34 = 0;
    decimal qtyTotal35 = 0;
    decimal qtyTotal36 = 0;
    decimal qtyTotal37 = 0;
    decimal qtyTotal38 = 0;
    decimal qtyTotal39 = 0;
    decimal qtyTotal40 = 0;
    decimal qtyTotal41 = 0;
    decimal qtyTotal42 = 0;
    decimal qtyTotal43 = 0;
    decimal qtyTotal44 = 0;
    decimal qtyTotal45 = 0;

    decimal grQtyTotal = 0;
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
    decimal grQtyTotal19 = 0;
    decimal grQtyTotal20 = 0;
    decimal grQtyTotal21 = 0;
    decimal grQtyTotal22 = 0;
    decimal grQtyTotal23 = 0;
    decimal grQtyTotal24 = 0;
    decimal grQtyTotal25 = 0;
    decimal grQtyTotal26 = 0;
    decimal grQtyTotal27 = 0;
    decimal grQtyTotal28 = 0;
    decimal grQtyTotal29 = 0;
    decimal grQtyTotal30 = 0;
    decimal grQtyTotal31 = 0;
    decimal grQtyTotal32 = 0;
    decimal grQtyTotal33 = 0;
    decimal grQtyTotal34 = 0;
    decimal grQtyTotal35 = 0;
    decimal grQtyTotal36 = 0;
    decimal grQtyTotal37 = 0;
    decimal grQtyTotal38 = 0;
    decimal grQtyTotal39 = 0;
    decimal grQtyTotal40 = 0;
    decimal grQtyTotal41 = 0;
    decimal grQtyTotal42 = 0;
    decimal grQtyTotal43 = 0;
    decimal grQtyTotal44 = 0;
    decimal grQtyTotal45 = 0;
    decimal grQtyTotal46 = 0;
    decimal grQtyTotal47 = 0;

    long storid = 0;
    int rowIndex = 1;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //GetEmployeeDetails();
            // ddlMonth.Enabled = false;
            GetRegion();
            GetEmployeeDetails();
            txtpaymentdate.Attributes.Add("readonly", "readonly");                                                                                // lblmonthid.Value = ddlmonth.SelectedValue;
        }
       
    }

    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
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
    private void DD_Monthbind()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            // DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));

        }

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
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_District_Wise_Prapatra_II_For_HO_New", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", getDate_MDY(txtpaymentdate.Text));
                if (ddlregion.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@RegionID", '0');
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", ddlregion.SelectedValue);
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

                            GrdPrapatraI.DataSource = dt;
                            GrdPrapatraI.DataBind();
                            GrdPrapatraI.Columns[1].Visible = false;
                            GrdPrapatraI.FooterRow.Style.Add("text-align", "right");
                            GrdPrapatraI.FooterRow.Cells[2].Text = "Total";
                            GrdPrapatraI.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Weight")).ToString();
                            GrdPrapatraI.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Fat_grain_Weight")).ToString();
                            GrdPrapatraI.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_A_B")).ToString();
                            GrdPrapatraI.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_JVS1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_HA1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_CWC1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Arremented_PVT_PEG1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OILFED1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Godown_Capacity1")).ToString();
                            GrdPrapatraI.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Cap2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Shed2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pvt_PEG_Cap2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Cap_Capacity2")).ToString();
                            GrdPrapatraI.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_JVS3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_HA3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_CWC3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[24].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Arremented_PVT_PEG3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[25].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[26].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OILFED3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[27].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Godown_Capacity3")).ToString();
                            GrdPrapatraI.FooterRow.Cells[28].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[29].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Cap4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[30].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Shed4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[31].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[32].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pvt_PEG_Cap4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[33].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Cap_Capacity4")).ToString();
                            GrdPrapatraI.FooterRow.Cells[34].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[35].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_JVS5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[36].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_HA5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[37].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_CWC5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[38].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Arremented_PVT_PEG5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[39].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[40].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OILFED5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[41].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Godown_Capacity5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[42].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_OWN6")).ToString();
                            GrdPrapatraI.FooterRow.Cells[43].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Cap5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[44].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLC_Mandi_Shed5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[45].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Markfed6")).ToString();
                            GrdPrapatraI.FooterRow.Cells[46].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pvt_PEG_Cap5")).ToString();
                            GrdPrapatraI.FooterRow.Cells[47].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Cap_Capacity5")).ToString();
                            showdetails.Visible = true;
                        }
                        else
                        {
                            GrdPrapatraI.DataSource = null;
                            GrdPrapatraI.DataBind();
                            showdetails.Visible = false;
                        }

                    }

                }
            }
        }
    }
    protected void GrdPrapatraI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }
    protected void GrdPrapatraI_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

             HeaderCell = new TableCell();
            HeaderCell.Text = "District Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

             HeaderCell = new TableCell();
            HeaderCell.Text = "Commodity";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "धान का वजन";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "मोटा अनाज";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "योग";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "भण्डारण हेतु कुल आवश्यक क्षमता";
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.CssClass = "alert alert-info";
            //HeaderCell.Font.Bold = false;
            //HeaderCell.Font.Size = 10;
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (9 to 15)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (17 to 21)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (23 to 28)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (30 to 34)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "JVS";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Hired + Adhigrahan";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Agrementedd PVT PEG Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CWC";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Oilfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total Godown Capacity (36 to 42)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC OWN";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Mandi Shed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Markfed";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Pvt. PEG CAP";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Total CAP Capacity (44 to 48)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "क्रमांक";
            //HeaderCell.ColumnSpan = 1;
            //HeaderCell.CssClass = "alert alert-info";
            //HeaderCell.Font.Bold = false;
            //HeaderCell.Font.Size = 10;
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow.Cells.Add(HeaderCell);

             HeaderCell = new TableCell();
            HeaderCell.Text = "District Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

             HeaderCell = new TableCell();
            HeaderCell.Text = "Commodity";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "अनुमानित उपार्जन";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "MPWLC";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "GODOWN CAPACITY";
            HeaderCell.ColumnSpan = 8;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "CAP CAPACITY";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();

            HeaderCell.Text = "कवर्ड गोडाम एवं कैप की दिनांक " + "-----------------" + " की स्थिति में कुल रिक्त भण्डारण क्षमता(में.टन)";

            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "दिनांक " + "-----------------" + "  के बाद आज दिनांक " + "-----------------" + " तक मिली गोदाम / कैप की कुल रिक्त भण्डारण क्षमता(स्कंध के उठाव तथा नविन प्राप्त क्षमता)(में.टन)";
            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "आज दिनांक तक उपलब्ध कुल प्रगतिशील रिक्त क्षमता (में.टन)";
            HeaderCell.ColumnSpan = 14;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GrdPrapatraI.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }

      
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {
        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        GridView1.AllowPaging = false;
        //        fillgrid();

        //        GridView1.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A2, 20f, 20f, 20f, 20f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=Payment_Received_From_MPSCSC_Details_From_Aug.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }

        GetEmployeeDetails();

    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmployeeDetails();
    }
}