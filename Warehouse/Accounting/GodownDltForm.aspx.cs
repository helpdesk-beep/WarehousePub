using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web;

public partial class Accounting_GodownDltForm : System.Web.UI.Page
{
 
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
   public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                fill();
            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }

     
    } 


    protected void fill()
    {         

        string query1 = "select Godown_ID,Godown_Name  from tbl_MetaData_GODOWN_2018  where branchID = '" + Session["BranchId"].ToString() + "' ";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }



    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdngodownid = GridView1.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Sp_Godown_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;       
            cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);   

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        fill();
        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Godown Remove Successfully')", true);

    }
}
    