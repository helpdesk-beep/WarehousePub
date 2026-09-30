using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;


public partial class StatePages_Rpt_Pvt_Godown_Capacity : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if ((Session["UserName"] != null))
            {
                if (!IsPostBack)
                {
                    fillgrid();
                    FillGodownType();
                }
            }
            else
            {
                Response.Redirect("../Logout.aspx");
            }





        }


    }

    private void fillgrid()
    {
        try
        {
            SqlCommand cmdd = new SqlCommand("Get_All_Godown_Capacity", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.AddWithValue("@Hired_Type", ddlCapacity.SelectedValue);
            //conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.FooterRow.Style.Add("text-align", "center");
                GridView1.FooterRow.Cells[6].Text = "Total";
                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Capacity")).ToString();
                //btnUpdate.Visible = false;
            }
        }

        catch (Exception)
        {

        }
        
    }

    public void FillGodownType()
    {
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Type_2", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();
            ddlCapacity.DataSource = cmd.ExecuteReader(); 
            ddlCapacity.DataTextField = "GodownType";
            ddlCapacity.DataValueField = "GodownType";
            ddlCapacity.DataBind();
            ddlCapacity.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();

        }

    }

    protected void ddlCapacity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //using (SqlConnection con = new SqlConnection(const))
        fillgrid();
    }
}



    
