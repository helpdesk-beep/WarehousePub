using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_Reports_JIT_Bill_Details_For_Region : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_JIT_Bill_Process_For_Region", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdJitbill.DataSource = dt;
                            grdJitbill.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdJitbill.DataSource = null;
                            grdJitbill.DataBind();
                        }
                    }
                }
            }
        }
    }
}