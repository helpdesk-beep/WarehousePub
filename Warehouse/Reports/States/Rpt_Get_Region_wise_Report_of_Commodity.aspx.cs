using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Get_Region_wise_Report_of_Commodity : System.Web.UI.Page
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
            loadCommodity();
        }
    }
    private void loadCommodity()
    {
        List<Commodity> commoList = new List<Commodity>();
        commoList.Add(new Commodity(0, "Select"));
        commoList.Add(new Commodity(13, "Paddy-Common"));
        commoList.Add(new Commodity(8, "Bajra"));
        commoList.Add(new Commodity(11, "Jowar"));

        drpDwnCommodity.DataSource = commoList;
        drpDwnCommodity.DataTextField = "commodityName";
        drpDwnCommodity.DataValueField = "commodityID";
        drpDwnCommodity.DataBind();

    }
    class Commodity
    {
        public int commodityID { get; set; }
        public string commodityName { get; set; }

        public Commodity(int comID, string comName)
        {
            this.commodityID = comID;
            this.commodityName = comName;
        }

        public Commodity() { }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Region_wise_Report_of_Commodity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", 0);
                cmd.Parameters.AddWithValue("@Commodity_Id", drpDwnCommodity.SelectedValue);
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
                            GridView1.Caption = @"M.P. Warehousing & Logistics Corporarion" + "</br> " + "PVT Godown Storage Charges Bill";

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
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAQty = (Label)e.Row.FindControl("lblAQty");
            Label lblWQty = (Label)e.Row.FindControl("lblWQty");

            TT2 += Convert.ToDecimal(lblAQty.Text);
            TT3 += Convert.ToDecimal(lblWQty.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "<div style='text-align: left'>" + "Total" + "</div>";
            e.Row.Cells[1].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[2].Text = "<div style='text-align: right'>" + TT2.ToString() + "</div>";
            e.Row.Cells[2].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[3].Text = "<div style='text-align: right'>" + TT3.ToString() + "</div>";
            e.Row.Cells[3].BackColor = System.Drawing.Color.Silver;
            TT1 = (Convert.ToDecimal(TT3) * 100) / Convert.ToDecimal(TT2);
            e.Row.Cells[4].Text = "<div style='text-align: right'>" + Math.Round(TT1, 2).ToString() + "</div>";
            e.Row.Cells[4].BackColor = System.Drawing.Color.Silver;
        }
    }
}