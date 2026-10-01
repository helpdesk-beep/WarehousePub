using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;


public partial class Admin_UpdateEmployee : System.Web.UI.Page
{     
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (String.IsNullOrWhiteSpace(Convert.ToString(Session["username"])))
        {
            Response.Redirect("/Login/Login.aspx");
            return;
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
        adapt = new SqlDataAdapter("select_employee", con);
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
        if (String.IsNullOrWhiteSpace(Convert.ToString(Session["username"])))
        {
            Response.Redirect("/Login/Login.aspx");
            return;
        }

        //Finding the controls from Gridview for the row which is going to update  
        Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;

        //TextBox caption = GridView1.Rows[e.RowIndex].FindControl("txt_Caption") as TextBox;
        //TextBox date = GridView1.Rows[e.RowIndex].FindControl("datepicker") as TextBox;

        HtmlInputText EmpNameH = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("txtEmpName");
        HtmlInputText DOB = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("datepicker");
        HtmlInputText Mobile = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("txtMobile");
        HtmlInputText Designation = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("txtDesignation");
        HtmlInputText DOJ = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("txtDOJ");
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");

        try
        {
            string imagePath = FileUpload1.HasFile ? EmployeeImageUpload.Save(FileUpload1, "~/Upload/EmployeeImages/") : String.Empty;
            using (SqlConnection connection = new SqlConnection(cs))
            using (SqlCommand command = new SqlCommand("update_employee", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpNameH", EmpNameH.Value);
                command.Parameters.AddWithValue("@DOB", DOB.Value);
                command.Parameters.AddWithValue("@DOJ", DOJ.Value);
                command.Parameters.AddWithValue("@Mobile", Mobile.Value);
                command.Parameters.AddWithValue("@Designation", Designation.Value);
                command.Parameters.AddWithValue("@Image", imagePath);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                connection.Open();
                command.ExecuteNonQuery();
            }

            GridView1.EditIndex = -1;
            ShowData();
        }
        catch (ArgumentException ex)
        {
            lblUploadError.Text = Server.HtmlEncode(ex.Message);
        }
        catch (Exception)
        {
            lblUploadError.Text = "Unable to update employee details.";
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