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
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
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
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
       

        string TitleInHn = txtTitleHn.Value;

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

        string str = FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(Server.MapPath("/Upload/" + str));
        string file = "/Upload/" + str.ToString();
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

                    Stream fsHn = FileUpload1.PostedFile.InputStream;
                    BinaryReader brHn = new BinaryReader(fsHn); //reads the binary files  
                    Byte[] bytesHn = brHn.ReadBytes((Int32)fsHn.Length); //counting the file length into bytes  


                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        SqlCommand cmd = new SqlCommand("insert_whatsNew", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DocTypeId", ddlDocType.SelectedValue);
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = txtTitleHn.InnerText;
                        cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = relDate;
                        cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = expDate;
                        cmd.Parameters.AddWithValue("@fileName", file);
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