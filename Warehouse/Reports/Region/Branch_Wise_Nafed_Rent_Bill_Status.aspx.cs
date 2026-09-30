using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class Reports_Region_Branch_Wise_Nafed_Rent_Bill_Status : System.Web.UI.Page
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
            fillgrid();
        }
    }
    public void fillgrid()
    {
        //String Region = Session["Region_ID"].ToString();
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Godown_Wise_Nafed_Rent_Bill_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_ID", Request.QueryString["District_Id"].ToString());
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                grpendding.DataSource = dt;
                grpendding.DataBind();
                grdbill.Visible = true;
                grpendding.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                grpendding.FooterRow.Cells[2].Text = "Total";
                grpendding.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_rent_Bill_Generated")).ToString();
                grpendding.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_rent_Bill_Amount")).ToString();

            }
            else
            {
                grpendding.DataSource = null;
                grpendding.DataBind();
                grdbill.Visible = true;
            }
        }
    }
}