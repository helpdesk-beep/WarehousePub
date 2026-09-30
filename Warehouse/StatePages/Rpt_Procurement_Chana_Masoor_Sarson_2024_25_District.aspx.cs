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

public partial class StatePages_Rpt_Procurement_Chana_Masoor_Sarson_2024_25_District : System.Web.UI.Page
{
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;

    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;

    long storid = 0;
    int rowIndex = 1;


    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            if (!IsPostBack)
            {
                labelName.Text = DateTime.Now.ToString();
                fillgrid();
            }
        }
    }
    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Procurement_Chana_Masoor_Sarson_2024_25_District", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", "0");
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
                            GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Chana,Masoor,Sarson WHR Status Procurement 2024-25";//+ "</br> " + "Region Name" + "  -   " + dt.Rows[0]["Region"].ToString()
                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[0].Text = "Total";
                            GridView1.FooterRow.Cells[1].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty")).ToString();
                            GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AcceptQty")).ToString();
                            opcloavg = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("AcceptQty")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQty")).ToString());
                            GridView1.FooterRow.Cells[3].Text = Math.Round(opcloavg, 2).ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
                        }
                    }
                }
            }
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}