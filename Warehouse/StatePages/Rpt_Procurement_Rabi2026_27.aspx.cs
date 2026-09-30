using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_Procurement_Rabi2026_27 : System.Web.UI.Page
{
    decimal qtyTotal1 = 0, qtyTotal2 = 0; // Sub Total variables
    decimal grQtyTotal1 = 0, grQtyTotal2 = 0; // Grand Total variables
    long storid = 0;
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labelName.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            fillgrid(); // अब यह एरर नहीं देगा
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Procurement_Rabi_2026_27", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", "0");
                cmd.Parameters.AddWithValue("@Type", "1");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // रिसेट टोटल
                    grQtyTotal1 = 0; grQtyTotal2 = 0;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            storid = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID"));
            decimal tmpTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalQty"));
            decimal tmpTotal2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AcceptQty"));

            qtyTotal1 += tmpTotal1;
            qtyTotal2 += tmpTotal2;

            grQtyTotal1 += tmpTotal1;
            grQtyTotal2 += tmpTotal2;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "GRAND TOTAL";
            e.Row.Cells[4].Font.Bold = true;

            Label lblTotalAQ = (Label)e.Row.FindControl("lblTotalAQ");
            Label lblTotalWHRQ = (Label)e.Row.FindControl("lblTotalWHRQ");
            Label lblAvgTotal = (Label)e.Row.FindControl("lblAvgTotal");

            if (lblTotalAQ != null) lblTotalAQ.Text = grQtyTotal1.ToString("N2");
            if (lblTotalWHRQ != null) lblTotalWHRQ.Text = grQtyTotal2.ToString("N2");
            if (grQtyTotal1 > 0)
                lblAvgTotal.Text = Math.Round((grQtyTotal2 * 100 / grQtyTotal1), 2).ToString() + "%";
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        bool newRow = false;
        if ((storid > 0) && (DataBinder.Eval(e.Row.DataItem, "Region_ID") != null))
        {
            if (storid != Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "Region_ID")))
                newRow = true;
        }
        if ((storid > 0) && (e.Row.RowType == DataControlRowType.Footer))
        {
            newRow = true;
            rowIndex = 0;
        }

        if (newRow)
        {
            GridView gv = (GridView)sender;
            GridViewRow subTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            subTotalRow.BackColor = System.Drawing.Color.LightGray;
            subTotalRow.Font.Bold = true;

            TableCell cell = new TableCell { Text = "Sub Total", ColumnSpan = 4, HorizontalAlign = HorizontalAlign.Right };
            subTotalRow.Cells.Add(cell);

            subTotalRow.Cells.Add(new TableCell { Text = qtyTotal1.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });
            subTotalRow.Cells.Add(new TableCell { Text = qtyTotal2.ToString("N2"), HorizontalAlign = HorizontalAlign.Right });

            decimal subAvg = qtyTotal1 > 0 ? Math.Round((qtyTotal2 * 100 / qtyTotal1), 2) : 0;
            subTotalRow.Cells.Add(new TableCell { Text = subAvg.ToString() + "%", HorizontalAlign = HorizontalAlign.Center });

            gv.Controls[0].Controls.AddAt(e.Row.RowIndex + rowIndex, subTotalRow);
            rowIndex++;
            qtyTotal1 = 0; qtyTotal2 = 0;
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}