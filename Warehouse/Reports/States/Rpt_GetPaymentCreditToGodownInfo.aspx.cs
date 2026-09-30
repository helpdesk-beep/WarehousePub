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

public partial class Rpt_GetPaymentCreditToGodownInfo : System.Web.UI.Page
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
  
    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;
    decimal grQtyTotal8 = 0;
    decimal grQtyTotal9 = 0;
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
            using (SqlCommand cmd = new SqlCommand("usp_PaymentCreditToGodownInfo", con))
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
                            GV_PaymentCreditToGodownInfo.DataSource = dt;
                            GV_PaymentCreditToGodownInfo.DataBind();
                            GV_PaymentCreditToGodownInfo.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Payment Credit to Godown Information Year wise";
                            GV_PaymentCreditToGodownInfo.Columns[0].Visible = false;
                            GV_PaymentCreditToGodownInfo.FooterRow.Style.Add("text-align", "right");
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[1].Text = "Total";
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2020-21")).ToString();
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2021-22")).ToString();
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2022-23")).ToString();
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2023-24")).ToString();
                            GV_PaymentCreditToGodownInfo.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("2024-25")).ToString();
                            //ShowingGroupingDataInGridView(GV_PaymentCreditToGodownInfo.Rows, 0, 10);
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GV_PaymentCreditToGodownInfo.DataSource = dt;
                            GV_PaymentCreditToGodownInfo.DataBind();
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