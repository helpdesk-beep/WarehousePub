using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Admin_InsertNews : System.Web.UI.Page
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

    }
    protected override void OnInit(System.EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

        string newsTitle = txtTitle.Value;
        string newsDate = datepicker.Value;
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

                        SqlCommand cmd = new SqlCommand("insert_news", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@newsTitle", SqlDbType.NVarChar).Value = newsTitle;
                        cmd.Parameters.Add("@newsDate", SqlDbType.VarChar).Value = newsDate;
                        cmd.Parameters.Add("@fileName", SqlDbType.NVarChar).Value = fileName;
                        cmd.Parameters.Add("@contentType", SqlDbType.VarChar).Value = type;
                        cmd.Parameters.Add("@fileData", SqlDbType.Binary).Value = bytes;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.AddWithValue("@isCgNews", cbNews.Checked);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        lblErr.ForeColor = System.Drawing.Color.Green;
                        lblErr.Text = "News File Uploaded Successfully!!!";

                        txtTitle.Value = "";
                        datepicker.Value = "";
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
            lblErr.Text = "Please Upload Your News File";
            lblErr.ForeColor = System.Drawing.Color.Red;
        }
    }  
}










    
