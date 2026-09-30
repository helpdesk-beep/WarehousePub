using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise : System.Web.UI.Page
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
            fillRegion();
            fillRegion2();
            fillFinancialYear();
            fillMonth();
        }
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
                ddlRegion.Items.Clear();
                ddlRegion.DataSource = ds.Tables[0];
                ddlRegion.DataTextField = "Regionnm";
                ddlRegion.DataValueField = "Region_ID";
                ddlRegion.DataBind();
                ddlRegion.Items.Insert(0, "--Select--");
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

    private void fillDistrict()
    {
        try
        {
            string query = "";

            query = "SELECT District_Id,District_Name FROM [tbl_MetaData_DISTRICT] where Region_ID='" + ddlRegion.SelectedValue + "' order by Regionnm asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.Items.Clear();
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, "--Select--");
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

    private void fillBranch()
    {
        try
        {
            string query = "";

            query = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' order by DepotName asc";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");
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

    private void fillFinancialYear()
    {
        try
        {
            string query = "";

            query = "select distinct Financial_Year from mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlfy.Items.Clear();
                ddlfy.DataSource = ds.Tables[0];
                ddlfy.DataTextField = "Financial_Year";
                ddlfy.DataValueField = "Financial_Year";
                ddlfy.DataBind();
                ddlfy.Items.Insert(0, "--Select--");
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

    private void fillMonth()
    {
        try
        {
            string query = "";

            query = "select distinct Month from mpscsc.dbo.StoragePayment_BillsFromCSMStoMPWLC_AfterAugust2020 where Financial_Year='" + ddlfy.SelectedValue + "' ";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmonth.Items.Clear();
                ddlmonth.DataSource = ds.Tables[0];
                ddlmonth.DataTextField = "Month";
                ddlmonth.DataValueField = "Month";
                ddlmonth.DataBind();
                ddlmonth.Items.Insert(0, "--Select--");
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
            using (SqlCommand cmd = new SqlCommand("Get_Region_Wise_StoragePayment_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
                //cmd.Parameters.AddWithValue("@FinancialYEar", ddlfy.SelectedValue);
                if (ddlRegion.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Region_ID", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Region_ID", ddlRegion.SelectedValue);
                }
                if (ddlfy.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@FinancialYEar", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FinancialYEar", ddlfy.SelectedValue);
                }
                if (ddldistrict.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Districtid", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Districtid", ddldistrict.SelectedValue);
                }
                if (ddlbranch.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@branchid", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@branchid", ddlbranch.SelectedValue);
                }
                if (ddlmonth.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@Month", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                }

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
                            GridView1.FooterRow.Cells[8].Text = "Total";
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
                            //fillFinancialYear();
                            //fillMonth();
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
            using (SqlCommand cmd = new SqlCommand("Get_Date_Wise_StoragePayment_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", ddlregion2.SelectedValue);
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
                            GridView1.FooterRow.Cells[8].Text = "Total";
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gross_Amount")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("OtherDeduction")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
                            //fillFinancialYear();
                            //fillMonth();
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
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillgrid();
        fillDistrict();
        fillFinancialYear();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        // fillgrid();
        fillBranch();
        // fillFinancialYear();
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
        // fillFinancialYear();
    }

    protected void ddlfy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
        fillMonth();
    }

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fillgrid();
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgriddatewise();
    }

    protected void tbnview_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}