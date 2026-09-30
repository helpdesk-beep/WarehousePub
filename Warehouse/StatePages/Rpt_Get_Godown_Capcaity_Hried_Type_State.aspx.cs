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


public partial class StatePages_Rpt_Get_Godown_Capcaity_Hried_Type_State : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("SP_Get_Godown_Capcaity_Hried_Type_State", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Type","2");
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
                            //GridView1.FooterRow.Style.Add("text-align","Right");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWeight")).ToString();
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Physical_Waight_Balances")).ToString();

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

        catch (Exception ex)
        {

        }
        
    }

    
}



   
