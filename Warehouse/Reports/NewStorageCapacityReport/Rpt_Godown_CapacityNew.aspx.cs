using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Rpt_Godown_CapacityNew : System.Web.UI.Page
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

        }
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_private_Godown_DetailsNew", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GodownType", drpGodownType.SelectedValue);
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
                                GridView1.FooterRow.Style.Add("text-align", "left");
                                GridView1.FooterRow.Cells[5].Text = "Total";
                                GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Godown_Capacity")).ToString("#,##0.00");
                                GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Godown_Scientific_Capacity")).ToString("#,##0.00");
                            }
                            else
                            {
                                GridView1.DataSource = null;
                                GridView1.DataBind();
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Success!', 'Something Went Wrong!', 'success');", true);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Error!', 'Something Went Wrong!'" + ex.Message + ", 'error');", true);
        }
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            fillgrid();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "showSweetAlert('Error!', 'Something Went Wrong!'" + ex.Message + ", 'error');", true);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SRV/Storage_Reports/Inspenctions/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_CAPNew.aspx", true);
    }
}