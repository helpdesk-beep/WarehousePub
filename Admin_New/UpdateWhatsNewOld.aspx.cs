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
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
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
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_whatsNew_by_docTypeId1", con))
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

        datasource = new SqlDataSource(ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString, "select_whatsNew_by_docTypeId1");
        Session["datasource"] = datasource;
        ShowData();

        int newEditIndex = e.NewEditIndex;
        GridView1.EditIndex = newEditIndex;
        ShowData();
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        //Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        HiddenField hdnID = (HiddenField)GridView1.Rows[e.RowIndex].FindControl("hdnID");
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");
        FileUpload FileUpload2 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload2");
        HtmlTextArea titleHn = (HtmlTextArea)GridView1.Rows[e.RowIndex].FindControl("txtTitleHn");
        HtmlTextArea titleEn = (HtmlTextArea)GridView1.Rows[e.RowIndex].FindControl("txtTitleEn");
        HtmlInputText relDate = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("releaseDate");
        HtmlInputText expDate = (HtmlInputText)GridView1.Rows[e.RowIndex].FindControl("expireDate");


        string titleInHn = titleHn.Value;
        DateTime releaseDate = DateTime.ParseExact(relDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        string filePath1 = FileUpload1.PostedFile.FileName;
        string filePath2 = FileUpload2.PostedFile.FileName;

        string fileNameHn = Path.GetFileName(filePath1);
        string fileNameEn = Path.GetFileName(filePath2);

        string ext1 = Path.GetExtension(fileNameHn);
        string ext2 = Path.GetExtension(fileNameEn);

        string type1 = String.Empty;
        string type2 = String.Empty;


        if (FileUpload1.HasFile || FileUpload2.HasFile)
        {
            try
            {
                switch (ext1) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".PDF":
                        type1 = "application/pdf";
                        break;
                    case ".pdf":
                        type1 = "application/pdf";
                        break;
                }
                switch (ext2) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".PDF":
                        type2 = "application/pdf";
                        break;
                    case ".pdf":
                        type2 = "application/pdf";
                        break;
                }

                if (type1 != String.Empty || type2 != String.Empty)
                {

                    Stream fs1 = FileUpload1.PostedFile.InputStream;
                    BinaryReader br1 = new BinaryReader(fs1); //reads the binary files  
                    Byte[] bytes1 = br1.ReadBytes((Int32)fs1.Length);


                    Stream fs2 = FileUpload2.PostedFile.InputStream;
                    BinaryReader br2 = new BinaryReader(fs2); //reads the binary files  
                    Byte[] bytes2 = br2.ReadBytes((Int32)fs2.Length);//counting the file length into bytes  


                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("update_whats_new1", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@titleInHn", SqlDbType.NVarChar)).Value = titleHn.Value;
                        cmd.Parameters.Add(new SqlParameter("@titleInEn", SqlDbType.NVarChar)).Value = titleEn.Value;
                        cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = releaseDate;
                        if (expDate.Value == null || expDate.Value == "")
                        {
                            cmd.Parameters.AddWithValue("@expireDate", SqlDbType.Date).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = DateTime.ParseExact(expDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        cmd.Parameters.AddWithValue("@fileNameHn", fileNameHn);
                        cmd.Parameters.Add("@contentTypeHn", SqlDbType.VarChar).Value = type1;
                        cmd.Parameters.Add("@fileDataHn", SqlDbType.Binary).Value = bytes1;

                        cmd.Parameters.AddWithValue("@fileNameEn", fileNameEn);
                        cmd.Parameters.Add("@contentTypeEn", SqlDbType.VarChar).Value = type2;
                        cmd.Parameters.Add("@fileDataEn", SqlDbType.Binary).Value = bytes2;

                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hdnID.Value));

                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                        GridView1.EditIndex = -1;
                        //Call ShowData method for displaying updated data  
                        ShowData();

                        lblErr.ForeColor = System.Drawing.Color.Green;
                        lblErr.Text = "File Uploaded Successfully!!!";
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
                SqlCommand cmd = new SqlCommand("update_whats_new1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@titleInHn", SqlDbType.NVarChar)).Value = titleHn.Value;
                cmd.Parameters.Add(new SqlParameter("@titleInEn", SqlDbType.NVarChar)).Value = titleEn.Value;
                cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = releaseDate;

                if (expDate.Value == null || expDate.Value == "")
                {
                    cmd.Parameters.AddWithValue("@expireDate", SqlDbType.Date).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = DateTime.ParseExact(expDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                cmd.Parameters.AddWithValue("@fileNameHn", "");
                cmd.Parameters.AddWithValue("@contentTypeHn", "");
                cmd.Parameters.AddWithValue("@fileDataHn", 0);

                cmd.Parameters.AddWithValue("@fileNameEn", "");
                cmd.Parameters.AddWithValue("@contentTypeEn","" );
                cmd.Parameters.AddWithValue("@fileDataEn", 0);

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hdnID.Value));

                cmd.ExecuteNonQuery();
                con.Close();
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                ShowData();

                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "File Updated Successfully!!!";
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
    protected void ViewH(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/WhatsNewHn.aspx','_blank' );", true);
    }
    protected void ViewE(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/WhatsNewEn.aspx','_blank' );", true);
    }
    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        docTypeId = ddlDocType.SelectedIndex;
        ShowData();
    }
}