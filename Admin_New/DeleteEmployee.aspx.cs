using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_DeleteEmployee : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (!IsPostBack)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("../Login/Login.aspx");


            }
            ShowData();
        }
    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    protected void ShowData()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_employee", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }
    protected void Delete(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            int id = int.Parse((sender as Button).CommandArgument);

            SqlCommand cmd = new SqlCommand("delete_employee", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();
            lblErr.Text = "Record Deleted Successfuly!!!";
            //Call ShowData method for displaying updated data  
            ShowData();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ShowData();
    }
}