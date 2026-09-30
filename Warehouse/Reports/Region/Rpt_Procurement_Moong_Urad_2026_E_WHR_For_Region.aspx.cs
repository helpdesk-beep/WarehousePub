using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Reports_Region_Rpt_Procurement_Moong_Urad_2026_E_WHR_For_Region : System.Web.UI.Page
{
    decimal qtyTotal1 = 0, qtyTotal2 = 0, qtyTotal3 = 0, qtyTotal4 = 0, qtyTotal5 = 0, qtyTotal6 = 0, qtyTotal7 = 0;
    long storid = 0;
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); 
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Status_Report_For_Chana_Masoor_Sarson_2026_For_Region", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_Logid"].ToString());
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Columns[1].Visible = false;

                    // Footer Totals
                    GridView1.FooterRow.Cells[2].Text = "Grand Total";
                    GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(x => x.Field<decimal>("AcceptQty")).ToString("N2");
                    GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(x => x.Field<int>("totalwhr")).ToString();
                    GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(x => x.Field<decimal>("TotalQty")).ToString("N2");
                    GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(x => x.Field<decimal>("Qty_Print")).ToString("N2");
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID"));
            qtyTotal1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AcceptQty"));
            qtyTotal2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalwhr"));
            qtyTotal3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQty"));
            qtyTotal4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfWHR_Submission"));
            qtyTotal5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty_Submission"));
            qtyTotal6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoOfWHR_Print"));
            qtyTotal7 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty_Print"));
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)GridView1.DataSource;
            if (dt != null && e.Row.RowIndex < dt.Rows.Count)
            {
                long currentRegionId = Convert.ToInt64(dt.Rows[e.Row.RowIndex]["Region_ID"]);
                if (storid != 0 && storid != currentRegionId)
                {
                    AddSubTotalRow("Sub Total", qtyTotal1, qtyTotal2, qtyTotal3, qtyTotal4, qtyTotal5, qtyTotal6, qtyTotal7, e.Row.RowIndex);
                }
            }
        }
    }

    private void AddSubTotalRow(string text, decimal q1, decimal q2, decimal q3, decimal q4, decimal q5, decimal q6, decimal q7, int index)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
        row.Font.Bold = true;
        row.BackColor = System.Drawing.Color.LightGray;

        TableCell cell = new TableCell { Text = text, ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
        row.Cells.Add(cell);

        row.Cells.Add(new TableCell { Text = q1.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q2.ToString("0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q3.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q4.ToString("0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q5.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q6.ToString("0"), HorizontalAlign = HorizontalAlign.Right });
        row.Cells.Add(new TableCell { Text = q7.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });

        GridView1.Controls[0].Controls.AddAt(index + rowIndex, row);
        rowIndex++;
        ResetTotals();
    }

    private void ResetTotals()
    {
        qtyTotal1 = qtyTotal2 = qtyTotal3 = qtyTotal4 = qtyTotal5 = qtyTotal6 = qtyTotal7 = 0;
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}