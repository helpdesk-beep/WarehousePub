using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_InsertCategory : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
    SqlConnection con;
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        dvErr.Visible = false;
        lblErr.Visible = false;

        if (Session["username"] == null)
        {
            Response.Redirect("/Login/Login.aspx");
        }

    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("insert_Inventry_category", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;              
                cmd.Parameters.Add("@CategoryE", SqlDbType.VarChar).Value = txtCategoryE.Text;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                dvErr.Visible = true;
                lblErr.Visible = true;
                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "Category Inserted Successfully!!!";               
                txtCategoryE.Text = "";
            }

        }
        catch (Exception ex)
        {
            dvErr.Visible = true;
            lblErr.Visible = true;
            lblErr.Text = "Error: " + ex.Message.ToString();
        }
    }
}











