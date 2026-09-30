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

public partial class StatePages_Rpt_Districtwise_StockDataImport_OnorBefore2024_25 : System.Web.UI.Page
{

    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {

                labelName.Text = DateTime.Now.ToString();
    }

    //protected void fillgrid()
    //{
    //    Decimal opcloavg = 0;
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Procurement_Rabi_2023", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@RegionID", "0");
    //            cmd.Parameters.AddWithValue("@Type", "1");
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        GridView1.DataSource = dt;
    //                        GridView1.DataBind();
    //                        GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Wheat WHR Status Procurement 2022-23";//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
    //                        GridView1.Columns[1].Visible = false;
    //                        // GridView1.columns.RemoveAt(1);
    //                        GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
    //                        GridView1.FooterRow.Cells[3].Text = "Total";
    //                        GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty")).ToString();
    //                        GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AcceptQty")).ToString();
    //                        // GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Averg")).ToString();
    //                        opcloavg = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("AcceptQty")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty")).ToString());
    //                        GridView1.FooterRow.Cells[6].Text = Math.Round(opcloavg, 2).ToString();
    //                        ShowingGroupingDataInGridView(GridView1.Rows, 0, 10);

    //                    }
    //                    else
    //                    {
    //                        // btnUpdate.Visible = false;
    //                        GridView1.DataSource = null;
    //                        GridView1.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    string TheResult = "";
    string TableName = "";

    protected void btnExportData_Click(object sender, EventArgs e)
    {
        string FileDateTime = DateTime.Now.ToString("ddMMyyyyhhmmss");
        SqlCommand cmd = new SqlCommand("usp_ReportForStockOnorBefore202425", con);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //string qry = "SELECT TOP 10 * FROM tbl_Metadata_Godown_2018";
        con.Open();
	cmd.CommandTimeout = 100;
        //SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dt.TableName = "Records";
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string attachment = "attachment; filename=DistrictWise_Godown_StockPosition_OnBefore2024_25_" + FileDateTime + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        con.Close();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }

}