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

public partial class Reports_States_Rpt_Pending_Bill_Details_Motn_Wise : System.Web.UI.Page
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
             //fillgrid();        

        }

    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Pending_Bill_Details_Month_Wise_For_State", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (ddlmonth.SelectedValue == "0")
                {
                    cmd.Parameters.AddWithValue("@Month", "");
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
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold;""> M.P. Warehousing & Logistics Corporarion" + "</br> " + "Storage Charges Bill(After August)";
                            lblNoofAC.Text = dt.Rows.Count.ToString();
                            divshowdetails.Visible = true;
                        }
                        else
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            divshowdetails.Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
}