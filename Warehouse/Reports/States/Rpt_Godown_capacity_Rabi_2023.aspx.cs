using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

public partial class Reports_States_Rpt_Godown_capacity_Rabi_2023 : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }
     private void fillgrid()
    {
        try
        {
            SqlCommand cmdd = new SqlCommand("Sp_Godown_Vacant_Capacity_Rabi2023", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            //cmdd.Parameters.AddWithValue("@branch_ID", Session["UserId"].ToString());
            //conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //btnUpdate.Visible = false;
            }
        }

        catch (Exception)
        {

        }

    }
    protected void ddlmaintainby_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}