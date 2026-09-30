using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;



public partial class JointVentureScheme_GetOfferLogin : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.Cache.SetNoStore();

    //    if (Session["UserName"] != null)
    //    {
    //        if (!IsPostBack) { lbluser.Text = Session["UserName"].ToString(); }
    //    }
    //    else { Response.Redirect("Logins.aspx"); }
    //}

    //protected void btnHome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("DistrictWiseJVSOffer.aspx");
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { }
    }

    public void gerreg()
    {
        try
        {
            string searchInput = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchInput))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter a search value.')", true);
                return;
            }

            // Using the single search box for all three parameters to match the SP's OR logic
            SqlCommand cmd = new SqlCommand("Get_tbl_Warehouse_PreReg_For_Public", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Reg_No", searchInput);
            cmd.Parameters.AddWithValue("@MobilNo", searchInput);
            cmd.Parameters.AddWithValue("@EmailID", searchInput);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind(); // Binds data to display in the grid
            }
            else
            {
                RegGrid.DataSource = null;
                RegGrid.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gerreg();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
}