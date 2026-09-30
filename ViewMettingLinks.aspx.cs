using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ViewMettingLinks : System.Web.UI.Page
{
    int docTypeId = 0;
    DataTable dt;
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        if (!IsPostBack)
        {
            BindGrid();
            DataTable dt = GetData();
            ddlDocType.DataSource = dt;
            ddlDocType.Items.Clear();
            ddlDocType.DataTextField = "e_doc_type";
            ddlDocType.DataValueField = "id";
            ddlDocType.DataBind();
            ddlDocType.Items.Insert(0, "- Select Document Type -");
            ddlDocType.SelectedValue = "10";
            ddlDocType.Enabled = false;
        }

    }
    DataTable GetData()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select_doc_type", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            con.Close();
            con.Dispose();

        }
        return dt;
    }
   
    private void BindGrid()
    {

        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Meeting_Link", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@docTypeId", Convert.ToInt32(10));
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);

                        gvHindi.DataSource = dt;
                        gvHindi.DataBind();
                    }
                }
            }
        }
    }


    protected void gvHindi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHindi.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}