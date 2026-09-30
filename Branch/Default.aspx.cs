using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Branch_Default : System.Web.UI.Page
{  
    DataTable dt =null;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        //IMGEmployee.ImageUrl = "../Images/No_file.jpg";
        if (Session["username"] == null || Session["username"].ToString() == "")
        {
            Response.Redirect("/Login/Login.aspx");
        }
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
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Image"].ToString() == null)
                    {
                        IMGEmployee.ImageUrl = "../Images/No_file.jpg";
                    }
                    else
                    {
                        IMGEmployee.ImageUrl = dt.Rows[0]["Image"].ToString();
                    }
                    lblEmpName.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Name"].ToString()) ? dt.Rows[0]["Name"].ToString() : "");
                    lbldist.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["DepotName"].ToString()) ? dt.Rows[0]["DepotName"].ToString() : "");
                    lblTBC.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["BTC"].ToString()) ? dt.Rows[0]["BTC"].ToString() : "");
                    lblmobile.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Mobile_No"].ToString()) ? dt.Rows[0]["Mobile_No"].ToString() : "");
                    lblTBUC.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["BTCU"].ToString()) ? dt.Rows[0]["BTCU"].ToString() : "");
                    lblDOJ.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["DOJ"].ToString()) ? dt.Rows[0]["DOJ"].ToString() : "");
                    lblEmpID.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Emp_ID"].ToString()) ? dt.Rows[0]["Emp_ID"].ToString() : "");
                    
                    Session["Name"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Name"].ToString()) ? dt.Rows[0]["Name"].ToString() : "");
                    Session["DepotName"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["DepotName"].ToString()) ? dt.Rows[0]["DepotName"].ToString() : "");
                    Session["BTC"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["BTC"].ToString()) ? dt.Rows[0]["BTC"].ToString() : "");
                    Session["Mobile_No"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Mobile_No"].ToString()) ? dt.Rows[0]["Mobile_No"].ToString() : "");
                    Session["BTCU"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["BTCU"].ToString()) ? dt.Rows[0]["BTCU"].ToString() : "");
                    Session["DOJ"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["DOJ"].ToString()) ? dt.Rows[0]["DOJ"].ToString() : "");
                    Session["Emp_ID"] = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(dt.Rows[0]["Emp_ID"].ToString()) ? dt.Rows[0]["Emp_ID"].ToString() : "");
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