using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Admin_ViewGradationList : System.Web.UI.Page
{
    int yearId = 0;
    DataTable dt;
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

    SqlConnection con;
    SqlDataAdapter adapt;
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
                Response.Redirect("/Login/Login.aspx");
            }
            GridView1.Enabled = true;
            BindGrid();
        }
    }

    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    //ShowData method for Displaying Data in Gridview  
    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_gradation_list"))
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void View(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/Gradation.aspx','_blank' );", true);
    }
}