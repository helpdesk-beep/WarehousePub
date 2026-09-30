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

public partial class Rpt_GetBillPaymentFromMPSCSCInfo : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
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
            using (SqlCommand cmd = new SqlCommand("usp_BillPaymentDetailsFromMPSCSC", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 200;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "";
                            GV_BillPaymentInfoMPSCSC.DataSource = dt;
                            GV_BillPaymentInfoMPSCSC.DataBind();
                            GV_BillPaymentInfoMPSCSC.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Payment Credit to Godown Information Year wise";
                            GV_BillPaymentInfoMPSCSC.Columns[0].Visible = false;
                            GV_BillPaymentInfoMPSCSC.FooterRow.Style.Add("text-align", "right");
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[1].Text = "Total";
                            //GV_BillPaymentInfoMPSCSC.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2020-21")).ToString();
                            //GV_BillPaymentInfoMPSCSC.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                            //GV_BillPaymentInfoMPSCSC.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                            //GV_BillPaymentInfoMPSCSC.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Submitted Bill Amount")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross Amount Received From MPSCSC")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable Amount Received From MPSCSC")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other Deduction From MPSCSC")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS Deduction From MPSCSC")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rent Bill Amount")).ToString();
                            GV_BillPaymentInfoMPSCSC.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Pay to Godown Owner From MPWLC")).ToString();
                            //ShowingGroupingDataInGridView(GV_PaymentCreditToGodownInfo.Rows, 0, 10);
                        }
                        else
                        {
                            //dt.Rows.Add(null, null, null, null);
                            //dt.Rows.Add(null, null, null, null);
                            //dt.Rows.Add(null, null, null, null);
                            GV_BillPaymentInfoMPSCSC.DataSource = dt;
                            GV_BillPaymentInfoMPSCSC.DataBind();
                        }
                    }
                }
            }
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
    
    
}