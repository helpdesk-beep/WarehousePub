using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

public partial class Reports_States_Rpt_Stock_Reconciliation_August_2022_Region_With_Remark : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("get_Stock_Reconciliation_Data_For_State_With_Remark", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Check", ddlmaintainby.SelectedValue.ToString());
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
                            //GridView1.FooterRow.Cells[2].Text = "Total";
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWeight")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Physical_Waight_Balances")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingForPhysicallverification")).ToString();

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
    protected void ddlmaintainby_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}