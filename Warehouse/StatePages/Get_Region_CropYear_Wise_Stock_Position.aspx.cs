using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Get_Region_CropYear_Wise_Stock_Position : System.Web.UI.Page
{
    // Web.config Connection String Name
    private string strConn = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    // Total Variables
    private decimal total16_17 = 0, total17_18 = 0, total18_19 = 0, total19_20 = 0, total20_21 = 0;
    private decimal total21_22 = 0, total22_23 = 0, total23_24 = 0, total24_25 = 0, total25_26 = 0, total26_27 = 0, grandTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStockGrid();
        }
    }

    private void BindStockGrid()
    {
        using (SqlConnection con = new SqlConnection(strConn))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_CropYear_Wise_Stock_Position", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        gvStockPosition.DataSource = dt;
                        gvStockPosition.DataBind();
                    }
                    else
                    {
                        gvStockPosition.DataSource = null;
                        gvStockPosition.DataBind();
                    }
                }
            }
        }
    }

    protected void gvStockPosition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total16_17 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2016-17") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2016-17") : 0);
            total17_18 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2017-18") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2017-18") : 0);
            total18_19 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2018-19") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2018-19") : 0);
            total19_20 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2019-20") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2019-20") : 0);
            total20_21 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2020-21") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2020-21") : 0);
            total21_22 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2021-22") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2021-22") : 0);
            total22_23 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2022-23") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2022-23") : 0);
            total23_24 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2023-24") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2023-24") : 0);
            total24_25 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2024-25") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2024-25") : 0);
            total25_26 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2025-26") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2025-26") : 0);
            total26_27 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "2026-27") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "2026-27") : 0);
            grandTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total") != DBNull.Value ? DataBinder.Eval(e.Row.DataItem, "Total") : 0);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            // S.No Cell [0] Blank Rakhenge
            e.Row.Cells[0].Text = "";

            // Region Column [1] me "Grand Total" text aayega
            e.Row.Cells[1].Text = "Grand Total";
            e.Row.Cells[1].Font.Bold = true;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

            // Correct Mapping of values with Header Columns
            e.Row.Cells[2].Text = total16_17.ToString("N2");
            e.Row.Cells[3].Text = total17_18.ToString("N2");
            e.Row.Cells[4].Text = total18_19.ToString("N2");
            e.Row.Cells[5].Text = total19_20.ToString("N2");
            e.Row.Cells[6].Text = total20_21.ToString("N2");
            e.Row.Cells[7].Text = total21_22.ToString("N2");
            e.Row.Cells[8].Text = total22_23.ToString("N2");
            e.Row.Cells[9].Text = total23_24.ToString("N2");
            e.Row.Cells[10].Text = total24_25.ToString("N2");
            e.Row.Cells[11].Text = total25_26.ToString("N2");
            e.Row.Cells[12].Text = total26_27.ToString("N2");
            e.Row.Cells[13].Text = grandTotal.ToString("N2");

            for (int i = 2; i <= 13; i++)
            {
                e.Row.Cells[i].Font.Bold = true;
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Region_CropYear_Stock_Position_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvStockPosition.AllowPaging = false;
            BindStockGrid();

            string style = @"<style> 
                                table { border-collapse: collapse; width: 100%; }
                                th { background-color: #f2f2f2; border: 0.5pt solid #000000; font-weight: bold; text-align: center; } 
                                td { border: 0.5pt solid #000000; mso-number-format:'\#\,\#\#0\.00'; } 
                             </style>";
            Response.Write(style);

            gvStockPosition.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel rendering
    }
}