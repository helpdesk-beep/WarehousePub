using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_ViewMediaScan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (Session["username"] == null)
        {
            Response.Redirect("/Login/Login.aspx");
        }
        BindRepeater();
    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_media", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();

                rptCarousel.DataSource = cmd.ExecuteReader();
                rptCarousel.DataBind();
                con.Close();
            }
        }
    }
}