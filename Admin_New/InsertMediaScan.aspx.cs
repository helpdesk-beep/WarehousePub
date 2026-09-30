using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class Admin_InsertMediaScan : System.Web.UI.Page
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
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

        string str = FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(Server.MapPath("../Upload/" + str));
        string mdImage = "../Upload/" + str.ToString();
        string mdTitle = txtTitle.Value;
        string mdDate = datepicker.Value;
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
                    case ".JPG":
                        type = "application/JPG";
                        break;
                    case ".jpg":
                        type = "application/jpg";
                        break;
                    case ".PNG":
                        type = "application/PNG";
                        break;
                    case ".png":
                        type = "application/png";
                        break;
                    case ".JPEG":
                        type = "application/JPEG";
                        break;
                    case ".jpeg":
                        type = "application/jpeg";
                        break;
                    case ".GIF":
                        type = "application/GIF";
                        break;
                    case ".gif":
                        type = "application/gif";
                        break;
                }
                if (type != String.Empty)
                {
                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files  
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        SqlCommand cmd = new SqlCommand("insert_media", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mdTitle", mdTitle);
                        cmd.Parameters.AddWithValue("@mdImage", mdImage);
                        cmd.Parameters.AddWithValue("@mdDate", mdDate);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        lblErr.Text = "Media Scan Uploaded Successfuly";
                        lblErr.ForeColor = System.Drawing.Color.ForestGreen;

                        txtTitle.Value = "";
                        datepicker.Value = "";

                    }
                    txtTitle.Value = "";
                }
                else
                {
                    lblErr.ForeColor = System.Drawing.Color.Red;
                    lblErr.Text = "Select Only jpg/jpeg/png or gif Files!!!";
                }
                
            }
            catch (Exception ex)
            {
                lblErr.Text = "Error: " + ex.Message.ToString();
            }
        }
        else
        {
            lblErr.Text = "Please Upload Your Media Scan File";
            lblErr.ForeColor = System.Drawing.Color.Red;
        }
    }
}