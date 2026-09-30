using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Admin_InsertVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        BindGrid();
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
        if (FileUpload1.PostedFile != null)
        {
            try
            {

                string str = FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("../Upload/" + str));
                string video = "../Upload/" + str.ToString();
                string videoCaption = txtCaption.Value;
                string filePath = FileUpload1.PostedFile.FileName;
                string fileName = Path.GetFileName(filePath);
                string videoDate = datepicker.Value;
                string ext = Path.GetExtension(fileName);
                string type = String.Empty;



                switch (ext) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".MP4":
                        type = "application/MP4";
                        break;
                    case ".mp4":
                        type = "application/mp4";
                        break;
                    case ".Mp4":
                        type = "application/Mp4";
                        break;
                    case ".mP4":
                        type = "application/mP4";
                        break;

                }
                if (type != String.Empty)
                {

                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files  
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);


                    string CS = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("insert_video", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@videoCaption", videoCaption);
                        cmd.Parameters.AddWithValue("@video", video);
                        cmd.Parameters.AddWithValue("@videoDate", videoDate);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);

                        cmd.ExecuteNonQuery();
                        BindGrid();
                        lblErr.Text = "Video file uploaded successfully";
                        lblErr.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblErr.ForeColor = System.Drawing.Color.Red;
                    lblErr.Text = "Select Only MP4 Files!!!"; // if file is other than speified extension   
                }
            }
            catch (Exception)
            {
                lblErr.Text = "Your file not uploaded";
                lblErr.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            lblErr.Text = "Please Upload Video";
            lblErr.ForeColor = System.Drawing.Color.Red;

        }
    }
    private void BindGrid()
    {
        string CS = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("select_Top_30_videos", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.DataBind();
        }
    }
}