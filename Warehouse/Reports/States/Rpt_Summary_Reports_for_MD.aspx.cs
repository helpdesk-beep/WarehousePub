using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Summary_Reports_for_MD : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT9, TT10, TT11;
    string RID = "";
    string MID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadMonth();
            fillRegion();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]) && !string.IsNullOrEmpty(Request.QueryString["RID"]))
            {
                MID = Request.QueryString["MID"].ToString();
                RID = Request.QueryString["RID"].ToString();
                ddlMonth.SelectedValue = MID;
                //DDLRgl.SelectedValue = RID;

                fillDistrict();
            }
            //else
            //{
            //    hdnRequestQuery.Value = "0";
            //}

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
                //DDLRgl.Items.Clear();
                //DDLRgl.DataSource = ds.Tables[0];
                //DDLRgl.DataTextField = "Regionnm";
                //DDLRgl.DataValueField = "Region_ID";
                //DDLRgl.DataBind();
                //DDLRgl.Items.Insert(0, new ListItem("--Select--", "0"));
                //DDLRgl.Items.Insert(0, "--Select--");
            }
            else
            {
                ////
            }
        }
        catch (Exception ex)
        {
            //////
        }
    }
    private void fillDistrict()
    {
        try
        {
            string query = "";

            //query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] WHERE Region_ID='" + DDLRgl.SelectedValue + "' order by District_Name asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlDistrict.Items.Clear();
                //ddlDistrict.DataSource = ds.Tables[0];
                //ddlDistrict.DataTextField = "District_Name";
                //ddlDistrict.DataValueField = "District_Id";
                //ddlDistrict.DataBind();
                //ddlDistrict.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("Summary_Reports_for_MD", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);
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
    protected void DDLRgl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DDLRgl.SelectedValue != "--Select--")
        //    fillDistrict();
        fillgrid();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotalGdown = (Label)e.Row.FindControl("lblTotalGdown");
            Label lblnoofgdwn = (Label)e.Row.FindControl("lblnoofgdwn");
            Label lblNoOfGenerateBill = (Label)e.Row.FindControl("lblNoOfGenerateBill");
            Label lblCREDIT_BILL = (Label)e.Row.FindControl("lblCREDIT_BILL");
            Label lblPendBill = (Label)e.Row.FindControl("lblPendBill");
            Label lblPendAmount = (Label)e.Row.FindControl("lblPendAmount");
            Label lblPendingRMAmt = (Label)e.Row.FindControl("lblPendingRMAmt");
            Label lblPendingHOAmt = (Label)e.Row.FindControl("lblPendingHOAmt");
            Label lblPendingDMBillAmt = (Label)e.Row.FindControl("lblPendingDMBillAmt");
            Label lblPendingICmBillAmt = (Label)e.Row.FindControl("lblPendingICmBillAmt");
            Label lblPendingBMAmt = (Label)e.Row.FindControl("lblPendingBMAmt");

            TT1 += Convert.ToDecimal(lblTotalGdown.Text);
            TT2 += Convert.ToDecimal(lblnoofgdwn.Text);
            TT3 += Convert.ToDecimal(lblNoOfGenerateBill.Text);
            TT4 += Convert.ToDecimal(lblCREDIT_BILL.Text);
            TT5 += Convert.ToDecimal(lblPendBill.Text);
            TT6 += Convert.ToDecimal(lblPendAmount.Text);
            TT7 += Convert.ToDecimal(lblPendingRMAmt.Text);
            TT8 += Convert.ToDecimal(lblPendingHOAmt.Text);
            TT9 += Convert.ToDecimal(lblPendingDMBillAmt.Text);
            TT10 += Convert.ToDecimal(lblPendingICmBillAmt.Text);
            TT11 += Convert.ToDecimal(lblPendingBMAmt.Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "<div style='text-align: left'>" + "Total" + "</div>";
            e.Row.Cells[2].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[3].Text = "<div style='text-align: right'>" + TT1.ToString() + "</div>";
            e.Row.Cells[3].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[4].Text = "<div style='text-align: right'>" + TT2.ToString() + "</div>";
            e.Row.Cells[4].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[5].Text = "<div style='text-align: right'>" + TT3.ToString() + "</div>";
            e.Row.Cells[5].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[6].Text = "<div style='text-align: right'>" + TT4.ToString() + "</div>";
            e.Row.Cells[6].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[7].Text = "<div style='text-align: right'>" + TT5.ToString() + "</div>";
            e.Row.Cells[7].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[8].Text = "<div style='text-align: right'>" + TT6.ToString() + "</div>";
            e.Row.Cells[8].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[9].Text = "<div style='text-align: right'>" + TT7.ToString() + "</div>";
            e.Row.Cells[9].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[10].Text = "<div style='text-align: right'>" + TT8.ToString() + "</div>";
            e.Row.Cells[10].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[11].Text = "<div style='text-align: right'>" + TT9.ToString() + "</div>";
            e.Row.Cells[11].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[12].Text = "<div style='text-align: right'>" + TT10.ToString() + "</div>";
            e.Row.Cells[12].BackColor = System.Drawing.Color.Silver;
            e.Row.Cells[13].Text = "<div style='text-align: right'>" + TT11.ToString() + "</div>";
            e.Row.Cells[13].BackColor = System.Drawing.Color.Silver;
        }
    }
}