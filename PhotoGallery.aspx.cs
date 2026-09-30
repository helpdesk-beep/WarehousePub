using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
public partial class PhotoGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        BindRepeater();
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_Top_30_images", con))
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