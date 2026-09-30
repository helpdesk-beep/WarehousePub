using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI;

public partial class Admin_DeleteMeetingLinks : System.Web.UI.Page
{
    int docTypeId = 0;
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
            ShowData();
            DataTable dt = GetData();
            ddlDocType.DataSource = dt;
            ddlDocType.Items.Clear();
            ddlDocType.DataTextField = "e_doc_type";
            ddlDocType.DataValueField = "id";
            ddlDocType.DataBind();
            ddlDocType.Items.Insert(0, "- Select Document Type -");
            ddlDocType.SelectedValue = "10";
            ddlDocType.Enabled = false;
        }
    }
    DataTable GetData()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("select_doc_type", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            con.Close();
            con.Dispose();

        }
        return dt;
    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    protected void ShowData()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Meeting_Link", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@docTypeId", 10);
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
    protected void Delete(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            int id = int.Parse((sender as Button).CommandArgument);

            SqlCommand cmd = new SqlCommand("delete_tbl_Meeting_Link", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();
            lblErr.Text = "Meeting Link Deleted Successfuly!!!";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Meeting Link Deleted Successfuly!!!')", true);

            //Call ShowData method for displaying updated data  
            ShowData();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ShowData();
    }
    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        docTypeId = ddlDocType.SelectedIndex;
        ShowData();
    }
}