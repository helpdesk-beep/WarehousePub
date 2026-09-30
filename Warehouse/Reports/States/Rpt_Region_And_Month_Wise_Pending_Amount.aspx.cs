using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Region_And_Month_Wise_Pending_Amount : System.Web.UI.Page
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
            loadMonth();
            fillRegion();
            fillgrid();
        }
    }
    private void loadMonth()
    {
        List<Month> commoList = new List<Month>();
        commoList.Add(new Month(0, "All"));
        commoList.Add(new Month(1, "1"));
        commoList.Add(new Month(2, "2"));
        commoList.Add(new Month(3, "3"));
        commoList.Add(new Month(4, "4"));
        commoList.Add(new Month(5, "5"));
        commoList.Add(new Month(6, "6"));
        commoList.Add(new Month(7, "7"));
        commoList.Add(new Month(8, "8"));
        commoList.Add(new Month(9, "9"));
        commoList.Add(new Month(10, "10"));
        commoList.Add(new Month(11, "11"));
        commoList.Add(new Month(12, "12"));

        ddlMonth.DataSource = commoList;
        ddlMonth.DataTextField = "MonthName";
        ddlMonth.DataValueField = "MonthID";
        ddlMonth.DataBind();

    }
    class Month
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }

        public Month(int MonthID, string MonthName)
        {
            this.MonthID = MonthID;
            this.MonthName = MonthName;
        }
        public Month() { }
    }
    private void fillRegion()
    {
        try
        {
            string query = "";

            query = "SELECT DISTINCT [Region_ID],[Regionnm] FROM [tbl_MetaData_DISTRICT] order by Regionnm asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlRegion.Items.Clear();
                //ddlRegion.DataSource = ds.Tables[0];
                //ddlRegion.DataTextField = "Regionnm";
                //ddlRegion.DataValueField = "Region_ID";
                //ddlRegion.DataBind();
                //ddlRegion.Items.Insert(0, "--Select--");
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
        string RegionID = "0";
        //if (ddlRegion.SelectedValue == "--Select--")
        //    RegionID = "0";
        //else RegionID = ddlRegion.SelectedValue;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Region_And_Month_Wise_Pending_Amount", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
                cmd.Parameters.AddWithValue("@RegionID", RegionID);
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
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotalGodown = (Label)e.Row.FindControl("lblTotalGodown");
            Label lblTotalBill = (Label)e.Row.FindControl("lblTotalBill");
            Label lblAmount = (Label)e.Row.FindControl("lblAmount");

            TT1 += Convert.ToDecimal(lblTotalGodown.Text);
            TT2 += Convert.ToDecimal(lblTotalBill.Text);
            TT3 += Convert.ToDecimal(lblAmount.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "<div style='text-align: left'>" + "Total" + "</div>";
            e.Row.Cells[1].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[2].Text = "<div style='text-align: right'>" + TT1.ToString() + "</div>";
            e.Row.Cells[2].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[3].Text = "<div style='text-align: right'>" + TT2.ToString() + "</div>";
            e.Row.Cells[3].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[4].Text = "<div style='text-align: right'>" + TT3.ToString() + "</div>";
            e.Row.Cells[4].BackColor = System.Drawing.Color.Silver;
        }
    }
}