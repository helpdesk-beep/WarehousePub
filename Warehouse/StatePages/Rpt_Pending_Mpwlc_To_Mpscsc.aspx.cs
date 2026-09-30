using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class StatePages_Rpt_Pending_Mpwlc_To_Mpscsc : System.Web.UI.Page
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
            fillgrid();
        }
    }

    private void fillgrid()
    {
        try
        {
            SqlCommand cmdd = new SqlCommand("Godown_Type_Wise_PAyment_Dtails", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            if (ddlCapacity.SelectedValue == "--Select--")
            {
                cmdd.Parameters.AddWithValue("@Hiretype", 0);
            }
            else
            {
                cmdd.Parameters.AddWithValue("@Hiretype", ddlCapacity.SelectedValue);
            }
            //cmd.Parameters.AddWithValue("@FromDate", txtFDate.Text);
            //cmd.Parameters.AddWithValue("@ToDate", txtTDate.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.FooterRow.Cells[2].Text = "Total";
                GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPWLCAmount")).ToString();
                GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("MPSCSCAmount")).ToString();
                GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAmount")).ToString();

                //btnUpdate.Visible = false;
            }
        }

        catch (Exception ex)
        { 
        }
    }
    private void fillComodity()
    {
        try
        {

            string query = "";
            query = "select distinct Hired_Type  from tbl_MetaData_GODOWN_2018";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCapacity.Items.Clear();
                ddlCapacity.DataSource = ds.Tables[0];
                ddlCapacity.DataTextField = "Hired_Type";
                ddlCapacity.DataValueField = "Hired_Type";
                ddlCapacity.DataBind();
                ddlCapacity.Items.Insert(0, "--Select--");
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

    

    protected void ddlCapacity_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}