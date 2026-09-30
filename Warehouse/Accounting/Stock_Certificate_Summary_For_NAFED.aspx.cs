using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
public partial class Accounting_Stock_Certificate_Summary_For_NAFED : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fillgrid();
        }
    }
    public void fillgrid()

    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Commodit_Wise_Nafed_Stock_Summary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdStock.DataSource = dt;
                GrdStock.DataBind();
                GrdStock.FooterRow.Cells[2].Text = "Total";
                GrdStock.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Available_Bags")).ToString();
                GrdStock.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Available_Qty")).ToString();
            }
            else
            {
                GrdStock.DataSource = null;
                GrdStock.DataBind();
            }
        }
    }

    protected void GrdStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdStock.PageIndex = e.NewPageIndex;
        fillgrid();
    }
}