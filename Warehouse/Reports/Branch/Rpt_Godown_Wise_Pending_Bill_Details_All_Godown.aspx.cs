using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Branch_Rpt_Godown_Wise_Pending_Bill_Details_All_Godown : System.Web.UI.Page
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
            fillgrid();
            fillMonth();
        }

    }
    private void fillMonth()
    {
        try
        {
            cmd = new SqlCommand("Get_Pandancy_Month", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmonth.Items.Clear();
                ddlmonth.DataSource = ds.Tables[0];
                ddlmonth.DataTextField = "TotalPendingMonth";
                ddlmonth.DataValueField = "TotalPendingMonth";
                ddlmonth.DataBind();
                ddlmonth.Items.Insert(0, "--Select--");
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
    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Pandancy_Month_Days_Storage_Bill_Wise_From_MPSCSC_For_Dashboard_Godown_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@branchid", Session["BranchId"].ToString());
                if (ddlmonth.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@month", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@month", ddlmonth.SelectedValue);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "Godown Wise Payment Pending From MPSCSC" + "</b> ";
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[5].Text = "Total";
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            divshowdetails.Visible = true;
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                            divshowdetails.Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}