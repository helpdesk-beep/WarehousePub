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
            using (SqlCommand cmd = new SqlCommand("Rpt_Godown_Capacity", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Type","2");
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
                            GridView1.FooterRow.Style.Add("text - align", "center");
                            GridView1.FooterRow.Cells[4].Text = "Total";
                            GridView1.FooterRow.Cells[5].Text= dt.AsEnumerable().Sum(row => row.Field<decimal>("Capacity")).ToString();

                        }
                        else
                        {
                            // btnUpdate.Visible = false;
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }

                    }
                }
            }
        }

        catch (Exception)
        {

        }
        
    }

    
}



   
