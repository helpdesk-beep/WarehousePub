using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
public partial class WarehouseLevel_Rpt_Get_Rent_Bill_Details : System.Web.UI.Page
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
            if (!String.IsNullOrEmpty(Session["UserName"].ToString()))
            {
                fillgrid();
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Rent_Bill_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", Session["GodownID_New"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gdstackdetail.DataSource = dt;
                            gdstackdetail.DataBind();
                            gdstackdetail.Caption = @"<b style=""font-weight: bold;"">Godown Wise " + "</br> " + "Payment Credit From MPWLC To Godown " + "</br> ";

                            gdstackdetail.FooterRow.Style.Add("text-align", "right");
                            gdstackdetail.FooterRow.Cells[4].Text = "Total";
                            gdstackdetail.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                            gdstackdetail.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("CREDIT_AMOUNT")).ToString();
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            gdstackdetail.DataSource = dt;
                            gdstackdetail.DataBind();
                        }
                    }
                }
            }
        }
    }
}