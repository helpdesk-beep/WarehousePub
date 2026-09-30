using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_Account_Reconcile_Reports_For_Audit_01_Receive_Payment : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("SP_Account_Reconcile_Reports_For_Audit_01", con))
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
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtfromdate.Text));
                cmd.Parameters.AddWithValue("@ToDate", getDate_MDY(txttodate.Text));
                //cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                //cmd.Parameters.AddWithValue("@ToDate", txttodate.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "MPWLC Payment Credit to Godown Owner through NEFT Payment System" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[13].Text = "Total";
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rent_Bill_AMT")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain_Detuction_Amount")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other_Detuction_Amt")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BM_Deduction")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Deduction_AMT")).ToString();
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PayToGO")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PAYtoMPWLC")).ToString();
                            GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StorageCharBillAMt")).ToString();
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

    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgriddatewise();
    }
}