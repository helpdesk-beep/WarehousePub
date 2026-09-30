using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Warehouse_Reports_States_Rpt_DistrictandRegion_Wise_Qty_Available : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillComodity();
        }
    }

    private void fillComodity()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY WHERE Commodity_Id in ('3','8','11','12','22')";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpDwnCommodity.Items.Clear();
                drpDwnCommodity.DataSource = ds.Tables[0];
                drpDwnCommodity.DataTextField = "Commodity_Name";
                drpDwnCommodity.DataValueField = "Commodity_Id";
                drpDwnCommodity.DataBind();
                drpDwnCommodity.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[District_And_Region_Wise_Qty_Avarage_New]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CommodityID", drpDwnCommodity.SelectedValue);
                cmd.Parameters.AddWithValue("@Hired_Type", dllGodownType.SelectedValue);
                cmd.Parameters.AddWithValue("@CropYear", DdlCropyear.SelectedValue);
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
                           // GridView1.FooterRow.Style.Add("text-align", "center");
                          //  GridView1.FooterRow.Cells[7].Text = "Total";
                           // GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlQty")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void drpDwnCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void dllGodownType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDwnCommodity.SelectedValue != "--Select--")
            fillgrid();
    }

    protected void DdlCropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}