using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_InsertWhatsNew : System.Web.UI.Page
{
    DataTable dt;
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
       

        string TitleInHn = txtTitleHn.Value;
        string TitleInEn = txtTitleEn.Value;

        DateTime expDate;
        DateTime relDate = DateTime.ParseExact(releaseDate.Value, "dd/MM/yyyy", null);
        if (expireDate.Value == "")
        {
             expDate = DateTime.MinValue;
        }
        else
        {
             expDate = DateTime.ParseExact(expireDate.Value, "dd/MM/yyyy", null);
        }

        string filePathHn = FileUploadH.PostedFile.FileName;
        string fileNameHn = Path.GetFileName(filePathHn);
        string extHn = Path.GetExtension(fileNameHn);
        string typeHn = String.Empty;


        string filePathEn = FileUploadE.PostedFile.FileName;
        string fileNameEn = Path.GetFileName(filePathEn);
        string extEn = Path.GetExtension(fileNameEn);
        string typeEn = String.Empty;



        if (FileUploadH.HasFile)
        {
            try
            {
                switch (extHn) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".PDF":
                        typeHn = "application/pdf";
                        break;
                    case ".pdf":
                        typeHn = "application/pdf";
                        break;
                }

                if (typeHn != String.Empty)
                {

                    Stream fsHn = FileUploadH.PostedFile.InputStream;
                    BinaryReader brHn = new BinaryReader(fsHn); //reads the binary files  
                    Byte[] bytesHn = brHn.ReadBytes((Int32)fsHn.Length); //counting the file length into bytes  



                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        SqlCommand cmd = new SqlCommand("insert_whatsNew1", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DocTypeId", ddlDocType.SelectedValue);

                        cmd.Parameters.Add("@TitleHn", SqlDbType.NVarChar).Value = TitleInHn;
                        cmd.Parameters.Add("@TitleEn", SqlDbType.NVarChar).Value = TitleInEn;

                        cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = relDate;
                        cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = expDate;

                        cmd.Parameters.Add("@fileNameHn", SqlDbType.NVarChar).Value = fileNameHn;
                        cmd.Parameters.Add("@contentTypeHn", SqlDbType.VarChar).Value = typeHn;
                        cmd.Parameters.Add("@fileDataHn", SqlDbType.Binary).Value = bytesHn;


                        if (FileUploadE.HasFile)
                        {
                            try
                            {
                                switch (extEn) // this switch code validate the files which allow to upload only PDF file   
                                {
                                    case ".PDF":
                                        typeEn = "application/pdf";
                                        break;
                                    case ".pdf":
                                        typeEn = "application/pdf";
                                        break;
                                }

                                if (typeEn != String.Empty)
                                {

                                    Stream fsEn = FileUploadE.PostedFile.InputStream;
                                    BinaryReader brEn = new BinaryReader(fsEn); //reads the binary files  
                                    Byte[] bytesEn = brEn.ReadBytes((Int32)fsEn.Length);


                                    cmd.Parameters.Add("@fileNameEn", SqlDbType.NVarChar).Value = fileNameEn;
                                    cmd.Parameters.Add("@contentTypeEn", SqlDbType.VarChar).Value = typeEn;
                                    cmd.Parameters.Add("@fileDataEn", SqlDbType.Binary).Value = bytesEn;

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

                            cmd.Parameters.Add("@fileNameEn", SqlDbType.NVarChar).Value = DBNull.Value;
                            cmd.Parameters.Add("@contentTypeEn", SqlDbType.VarChar).Value = DBNull.Value;
                            cmd.Parameters.Add("@fileDataEn", SqlDbType.Binary).Value = DBNull.Value;
                        }

                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

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

            ddlDocType.SelectedIndex = 0;
            txtTitleHn.Value = "";
            txtTitleEn.Value = "";
            releaseDate.Value = "";
            expireDate.Value = "";
            lblErr.Visible = true;
            lblErr.ForeColor = System.Drawing.Color.Green;
            lblErr.Text = "File Uploaded Successfully!!!";
        }
        else
        {
            lblErr.Text = "Please Upload Your File";
            lblErr.ForeColor = System.Drawing.Color.Red;
        }
    }
}