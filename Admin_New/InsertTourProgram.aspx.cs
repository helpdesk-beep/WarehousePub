using System;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class Admin_InsertTourProgram : System.Web.UI.Page
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
    }

    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        string tourProgE = txtTPE.Value;
        string tourProgH = txtTPH.Value;
        string tourDate = datepicker.Value;

        using (SqlConnection con = new SqlConnection(constr))
        {

            SqlCommand cmd = new SqlCommand("insert_tour_program", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tour_prog_e", tourProgE);
            cmd.Parameters.AddWithValue("@tour_prog_h", tourProgH);
            cmd.Parameters.AddWithValue("@tour_date", tourDate);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            lblErr.Text = "Tour Program Inserted Successfuly!!!";
            lblErr.ForeColor = System.Drawing.Color.ForestGreen;

            txtTPE.Value = "";
            txtTPH.Value = "";
            datepicker.Value = "";

        }

    }
}