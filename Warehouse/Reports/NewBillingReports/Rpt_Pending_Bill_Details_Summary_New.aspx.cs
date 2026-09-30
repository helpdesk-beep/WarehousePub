using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Reports_NewBillingReports_Rpt_Pending_Bill_Details_Summary_New : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillFinasncialYear();
            fillgrid();
        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_pendingBill_Summary", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //if (ddlFY.SelectedValue == "--Select--")
                //{
                //    cmd.Parameters.AddWithValue("@Year", "0");
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@Year", ddlFY.SelectedValue);
                //}
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet DS = new DataSet())
                    {
                        sda.Fill(DS);
                        if (DS.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();

                            GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;", "right");
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[2].Text = DS.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalBillPendingForGeneration")).ToString();
                        }
                        else
                        {
                            GridView1.DataSource = DS.Tables[0];
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
    }


    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }



    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();

    }
}