using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Region_Reports_Rpt_AGM_DSC_Pendency_at_RM : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["BID"]))
            {
                fillgrid(Request.QueryString["BID"].ToString());
            }
        }
    }
    protected void fillgrid(string RID)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Get_AGM_DSC_Pendency_at_RM]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", RID);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "Branch Wise Bill Pending Status At Various Levels(From August 2020)" + "</b> ";

                            //GridView1.FooterRow.Style.Add("text-align", "right");
                            //GridView1.FooterRow.Style.Add("font-size", "X-Large");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingPassingOrder")).ToString();
                            //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingAGMDSC")).ToString();
                            //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
                            //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PendingCreatedEPF")).ToString();
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
}