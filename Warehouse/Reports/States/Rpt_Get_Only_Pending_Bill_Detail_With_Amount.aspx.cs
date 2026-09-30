using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Rpt_Get_Only_Pending_Bill_Detail_With_Amount : System.Web.UI.Page
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
    decimal qtyTotal19 = 0;
    decimal qtyTotal20 = 0;
    decimal qtyTotal21 = 0;
    decimal qtyTotal22 = 0;
    decimal qtyTotal23 = 0;
  
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
  
    int storid = 0;
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
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Bill_Amount_Detail_After_August_For_All_District", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            GridView1.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill With Amount(After August)";
                            GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingNooGodown")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingNoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingNoOfGenerateAmt")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingatICMS")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmtatICMS")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBillCSMS")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmtCSMS")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("PendingBillDM")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmtDM")).ToString();
                            ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);
                            //btnUpdate.Visible = true;
                        }
                        else
                        {
                            //btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void fillUpdateMonths()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Update_Pending_Months", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblMsg.Text = "Pending Months calculated successfully. Please run report again";
                        }
                        else
                        {
                            lblMsg.Text = "Timed Out Error.";
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
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString());
            Label lblPendingNoOfGenerateBill = (Label)e.Row.FindControl("lblPendingNoOfGenerateBill");
            Label lblPendingatICMS = (Label)e.Row.FindControl("lblPendingatICMS");
            Label lblPendingBillCSMS = (Label)e.Row.FindControl("lblPendingBillCSMS");
            Label lblPendingBillDM = (Label)e.Row.FindControl("lblPendingBillDM");
            int tmpTotal1 = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PendingNooGodown").ToString());
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill").ToString());
            decimal tmpTotal3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateAmt").ToString());
            decimal tmpTotal4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingatICMS").ToString());
            decimal tmpTotal5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingAmtatICMS").ToString());
            decimal tmpTotal6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillCSMS").ToString());
            decimal tmpTotal7 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingAmtCSMS").ToString());
            decimal tmpTotal8 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillDM").ToString());
            decimal tmpTotal9 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingAmtDM").ToString());
            HiddenField hdnBranch_Id = (HiddenField)e.Row.FindControl("hdnBranch_Id");
         
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill")) > 0)
            {
                lblPendingNoOfGenerateBill.Text = "<a  href='Rpt_Pending_Bill_Details.aspx?ID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingNoOfGenerateBill") + "</a>";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingatICMS")) > 0)
            {
                lblPendingatICMS.Text = "<a  href='Pending_Bill_for_Submission_at_BM.aspx?ID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingatICMS") + "</a>";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillCSMS")) > 0)
            {
                lblPendingBillCSMS.Text = "<a  href='Rpt_Pending_Bill_for_Digital_Signing_to_Centre_Incharge.aspx?ID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingBillCSMS") + "</a>";
            }
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillDM")) > 0)
            {
                lblPendingBillDM.Text = "<a  href='Rpt_Get_Pending_Bill_for_DM.aspx?ID=" + (hdnBranch_Id.Value) + "' target='_blank' style='color: Blue'>" + DataBinder.Eval(e.Row.DataItem, "PendingBillDM") + "</a>";
            }


            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;
            qtyTotal3 += tmpTotal3;
            qtyTotal4 += tmpTotal4;
            qtyTotal5 += tmpTotal5;
            qtyTotal6 += tmpTotal6;
            qtyTotal7 += tmpTotal7;
            qtyTotal8 += tmpTotal8;
            qtyTotal9  += tmpTotal9 ;
            

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
            grQtyTotal3 += tmpTotal3;
            grQtyTotal4 += tmpTotal4;
            grQtyTotal5 += tmpTotal5;
            grQtyTotal6 += tmpTotal6;
            grQtyTotal7 += tmpTotal7;
            grQtyTotal8 += tmpTotal8;
            grQtyTotal9  += tmpTotal9 ;
           

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
    protected void OnDataBound(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

        bool newRow = false;
       
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "District_Id") != null))
        {
            if (storid != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "District_Id").ToString()))
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
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal1.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            //HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 3;
            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal2.ToString();
            NewTotalRow.Cells.Add(HeaderCell);

            // HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //HeaderCell.ColumnSpan = 4;
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

            NewTotalRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            HeaderCell.Text = qtyTotal6.ToString();
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


            GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow);
            rowIndex++;
            qtyTotal = 0;
            qtyTotal1 = 0;
            qtyTotal2 = 0;
            qtyTotal3 = 0;
            qtyTotal4 = 0;
            qtyTotal5 = 0;
            qtyTotal6 = 0;
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
            qtyTotal17 = 0;
            qtyTotal18 = 0;
            qtyTotal19 = 0;
            qtyTotal20 = 0;
            qtyTotal21 = 0;
            qtyTotal22 = 0;
            qtyTotal23 = 0;

        }
       

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        fillUpdateMonths();
    }
}