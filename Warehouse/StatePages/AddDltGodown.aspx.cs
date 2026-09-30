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

public partial class StatePages_AddDltGodown : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                fill1();


            }
        }
    }

    protected void fill1 ()
    {
        string query = "select District_Id,District_Name from tbl_metadata_district Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        DdlDist.DataSource = ds.Tables[0];
        DdlDist.DataTextField = "District_Name";
        DdlDist.DataValueField = "District_Id";
        DdlDist.DataBind();
        DdlDist.Items.Insert(0, new ListItem("जिला चुने", "0"));

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //GetBranchData();
        fill();
        // FAKEa  2 second delay
        System.Threading.Thread.Sleep(2000);
    }


    protected void fill()
    {

        string query1 = "select Godown_ID,Godown_Name  from tbl_MetaData_GODOWN_2018_Remove_Log  where branchID = '" + ddlbranch.SelectedValue + "' and DistrictId='" + DdlDist.SelectedValue + "' ";
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
            SqlCommand cmd = new SqlCommand("Sp_Godown_Delete_Return", con);
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
        ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Godown ADD Successfully')", true);

    }

    protected void DdlDist_TextChanged(object sender, EventArgs e)
    {
        string query = "select  BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId ='" + DdlDist.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        ddlbranch.DataSource = ds.Tables[0];
        ddlbranch.DataTextField = "DepotName";
        ddlbranch.DataValueField = "BranchId";
        ddlbranch.DataBind();
        ddlbranch.Items.Insert(0, new ListItem("Branch चुने", "0"));


    }

    protected void ddlbranch_TextChanged(object sender, EventArgs e)
    {

        fill();
    }
}