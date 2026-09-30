using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.HtmlControls;


public partial class Admin_UpdateNews : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            ShowData();
        }
    }

    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    //Connection String from web.config File  
    string cs = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
    SqlConnection con;
    SqlDataAdapter adapt;
    DataTable dt;

    //ShowData method for Displaying Data in Gridview  
    protected void ShowData()
    {
        dt = new DataTable();
        con = new SqlConnection(cs);
        con.Open();
        adapt = new SqlDataAdapter("select_message", con);
        adapt.Fill(dt);
        if (dt.Rows.Count > 0)
        {           
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GridView1.EditIndex = e.NewEditIndex;
        ShowData();
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        //Finding the controls from Gridview for the row which is going to update  
        Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;

        //TextBox caption = GridView1.Rows[e.RowIndex].FindControl("txt_Caption") as TextBox;
        //TextBox date = GridView1.Rows[e.RowIndex].FindControl("datepicker") as TextBox;

        HtmlTextArea titleH = (HtmlTextArea)GridView1.Rows[e.RowIndex].FindControl("txtTitle");
        HtmlInputText date = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("datepicker");
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");

        string path = "../Upload/";
        if (FileUpload1.HasFile)
        {

            path += FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(path));
            con = new SqlConnection(cs);
            con.Open();
            //updating the record  

            SqlCommand cmd = new SqlCommand("update_message", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@titleInHn", SqlDbType.NVarChar)).Value = titleH.Value;
            cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = date.Value;
            cmd.Parameters.AddWithValue("@fileNameHn", path);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        else
        {
            con = new SqlConnection(cs);
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand("update_message", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@titleInHn", SqlDbType.NVarChar)).Value = titleH.Value;
            cmd.Parameters.AddWithValue("@fileNameHn", "");
            cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = date.Value;
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }

    }
    protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GridView1.EditIndex = -1;
        ShowData();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ShowData();
    }
}