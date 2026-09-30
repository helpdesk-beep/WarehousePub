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
    string cs = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    SqlConnection con;
    SqlDataAdapter adapt;
    DataTable dt;

    //ShowData method for Displaying Data in Gridview  
    protected void ShowData()
    {
        dt = new DataTable();
        con = new SqlConnection(cs);
        con.Open();
        adapt = new SqlDataAdapter("select_news", con);
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
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");
        HtmlTextArea Title = (HtmlTextArea)GridView1.Rows[e.RowIndex].FindControl("txtTitle");
        HtmlInputText date = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("datepicker");

        string newsTitle = Title.Value;
        string newsDate = date.Value;
        string filePath = FileUpload1.PostedFile.FileName;
        string fileName = Path.GetFileName(filePath);
        string ext = Path.GetExtension(fileName);
        string type = String.Empty;


        if (FileUpload1.HasFile)
        {
            try
            {
                switch (ext) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".PDF":
                        type = "application/pdf";
                        break;
                    case ".pdf":
                        type = "application/pdf";
                        break;
                }

                if (type != String.Empty)
                {

                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files  
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length); //counting the file length into bytes  


                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("update_news", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@newsTitle", SqlDbType.NVarChar)).Value = Title.Value;
                        cmd.Parameters.AddWithValue("@newsDate", date.Value);
                        cmd.Parameters.AddWithValue("@fileName", fileName);
                        cmd.Parameters.Add("@fileData", SqlDbType.Binary).Value = bytes;
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                        
                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                        GridView1.EditIndex = -1;
                        //Call ShowData method for displaying updated data  
                        ShowData();

                        lblErr.ForeColor = System.Drawing.Color.Green;
                        lblErr.Text = "News File Uploaded Successfully!!!";
                    }
                }
                else
                {
                    lblErr.ForeColor = System.Drawing.Color.Red;
                    lblErr.Text = "Select Only PDF Files!!!"; // if file is other than speified extension   
                }

            }
            catch (Exception ex)
            {
                lblErr.Text = "Error: " + ex.Message.ToString();
            }
        }
        else

        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("update_news", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@newsTitle", SqlDbType.NVarChar)).Value = Title.Value;
                cmd.Parameters.AddWithValue("@newsDate", date.Value);
                cmd.Parameters.AddWithValue("@fileName", "");
                cmd.Parameters.AddWithValue("@fileData", 0);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                // cmd.Parameters.Add("@fileData", SqlDbType.VarBinary).Value = fData.Value;
                cmd.ExecuteNonQuery();
                con.Close();
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                ShowData();

                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "News Updated Successfully!!!";
            }

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
    protected void View(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/News.aspx','_blank' );", true);
    }
}