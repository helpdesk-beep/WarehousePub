using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Globalization;

public partial class Admin_UpdateWhatsNew : System.Web.UI.Page
{
    int docTypeId = 0;
    DataTable dt;
    SqlConnection con;
    SqlDataAdapter adapt;
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    public SqlDataSource datasource;

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

            DataTable dt = GetData();
            ddlDocType.DataSource = dt;

            ddlDocType.Items.Clear();
            ddlDocType.DataTextField = "e_doc_type";
            ddlDocType.DataValueField = "id";
            ddlDocType.DataBind();
            ddlDocType.Items.Insert(0, "- Select Document Type -");
        }
    }

    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
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
    //Connection String from web.config File  


    //ShowData method for Displaying Data in Gridview  
    protected void ShowData()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_whatsNew_by_docTypeId", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.Add("@docTypeId", SqlDbType.Int).Value = Convert.ToInt32(ddlDocType.SelectedIndex);
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        Session["datasource"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {

        datasource = new SqlDataSource(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString, "select_whatsNew_by_docTypeId");
        Session["datasource"] = datasource;
        ShowData();

        int newEditIndex = e.NewEditIndex;
        GridView1.EditIndex = newEditIndex;
        ShowData();
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        //Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        HiddenField hdnID = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hdnID");
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");
        HtmlTextArea title = (HtmlTextArea)GridView1.Rows[e.RowIndex].FindControl("txtTitleHn");
        HtmlInputText relDate = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("releaseDate");
        HtmlInputText expDate = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("expireDate");

        DateTime releaseDate = DateTime.ParseExact(relDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        string filePath1 = FileUpload1.PostedFile.FileName;
        string fileNameHn = Path.GetFileName(filePath1);
        string ext = Path.GetExtension(fileNameHn);
        string type = String.Empty;

        string path = "../Upload/";

        if (FileUpload1.HasFile)
        {
            path += FileUpload1.FileName;
            FileUpload1.SaveAs(MapPath(path));
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
               

                if (type != String.Empty )
                {

                    Stream fs1 = FileUpload1.PostedFile.InputStream;
                    BinaryReader br1 = new BinaryReader(fs1); //reads the binary files  
                    Byte[] bytes1 = br1.ReadBytes((Int32)fs1.Length);


                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("update_whats_new", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar)).Value = title.Value;
                        cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = releaseDate;
                        if (expDate.Value == null || expDate.Value == "")
                        {
                            cmd.Parameters.AddWithValue("@expireDate", SqlDbType.Date).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = DateTime.ParseExact(expDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        cmd.Parameters.AddWithValue("@fileName", path);                     
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hdnID.Value));

                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                        GridView1.EditIndex = -1;
                        //Call ShowData method for displaying updated data  
                        ShowData();

                        lblErr.ForeColor = System.Drawing.Color.Green;
                        lblErr.Text = "Details Updated Successfully!!!";
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
                SqlCommand cmd = new SqlCommand("update_whats_new", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar)).Value = title.Value;

                cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = releaseDate;

                if (expDate.Value == null || expDate.Value == "")
                {
                    cmd.Parameters.AddWithValue("@expireDate", SqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = DateTime.ParseExact(expDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                cmd.Parameters.AddWithValue("@fileName", "");
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hdnID.Value));

                cmd.ExecuteNonQuery();
                con.Close();
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                ShowData();

                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "Details Updated Successfully!!!";
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
        GridView myGV = (GridView)sender;
        int newPageIndex = e.NewPageIndex;
        GridView1.PageIndex = newPageIndex;
        ShowData();
    }

    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        docTypeId = ddlDocType.SelectedIndex;
        ShowData();
    }
}