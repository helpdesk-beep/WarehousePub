using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Date_Region_Wise_Payment_Reports : System.Web.UI.Page
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
            fillRegion2();
        }
    }
    //protected void fillgrid()
    //{

    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_StoragePayment_Details", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            //cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
    //            //cmd.Parameters.AddWithValue("@FinancialYEar", ddlfy.SelectedValue);
    //            if (ddlRegion.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@Region_ID", 0);
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
    //            }
    //            if (ddlfy.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@FinancialYEar", 0);
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@FinancialYEar", ddlfy.SelectedValue);
    //            }
    //            if (ddldistrict.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@Districtid", 0);
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@Districtid", ddldistrict.SelectedValue);
    //            }
    //            if (ddlbranch.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@branchid", 0);
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@branchid", ddlbranch.SelectedValue);
    //            }
    //            if (ddlmonth.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@Month", 0);
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
    //            }

    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "received Payment from MPSCSC through NEFT Payment System " + "</b> ";
    //                        GridView1.DataSource = dt;
    //                        GridView1.DataBind();
    //                        GridView1.FooterRow.Style.Add("text-align", "right");
    //                        GridView1.FooterRow.Cells[8].Text = "Total";
    //                        GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString();
    //                        GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
    //                        GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString();
    //                        GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
    //                        //fillFinancialYear();
    //                        //fillMonth();
    //                    }
    //                    else
    //                    {
    //                        GridView1.DataSource = null;
    //                        GridView1.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    private void fillRegion2()
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
                ddlregion2.Items.Clear();
                ddlregion2.DataSource = ds.Tables[0];
                ddlregion2.DataTextField = "Regionnm";
                ddlregion2.DataValueField = "Region_ID";
                ddlregion2.DataBind();
                ddlregion2.Items.Insert(0, "--Select--");
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
    protected void fillgriddatewise()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Receiving_Details_From_MPSCSC_Date_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlregion2.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@RegionID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", ddlregion2.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txttodate.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "received Payment from MPSCSC through NEFT Payment System " + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[3].Text = "Total";
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CreditAmount")).ToString();
                            //fillFinancialYear();
                            //fillMonth();
                            grd1.Visible = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Payment Received .');", true);
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillgriddatewise2()
    {

        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Receiving_Date_Wise_Only", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@FromDate", txtfrom2.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtto2.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView2.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "received Payment from MPSCSC through NEFT Payment System " + "</b> ";
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            GridView2.FooterRow.Style.Add("text-align", "right");
                            GridView2.FooterRow.Cells[2].Text = "Total";
                            GridView2.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CreditAmount")).ToString();
                            //fillFinancialYear();
                            //fillMonth();
                            grd2.Visible = true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Payment Received .');", true);
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgriddatewise();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgriddatewise2();
    }
}