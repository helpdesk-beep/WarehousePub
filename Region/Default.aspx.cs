using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Region_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        //IMGEmployee.ImageUrl = "../Images/No_file.jpg";
        //if (Session["username"] == null)
        //{
        //    Response.Redirect("/Login/Login.aspx");
        //}
        if (!IsPostBack)
        {
            BindRepeater();
        }
    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }
    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"].ToString());
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Image"].ToString() == null)
                    {
                        IMGEmployee.ImageUrl = "../Images/No_file.jpg";
                    }
                    else
                    {
                        IMGEmployee.ImageUrl = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Image"].ToString())? dt.Rows[0]["Image"].ToString():"");
                    }
                    lblEmpName.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Name"].ToString())? dt.Rows[0]["Name"].ToString():"");
                    lbldist.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Regionnm"].ToString())? dt.Rows[0]["Regionnm"].ToString():"");
                    //Repeater2.DataSource = cmd.ExecuteReader();
                    //Repeater2.DataBind();
                }
                con.Close();
            }
        }
    }
    protected void View(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin_New/News.aspx','_blank' );", true);
    }
}