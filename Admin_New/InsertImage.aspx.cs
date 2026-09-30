using System;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;
using System.Web.UI;

public partial class Admin_InsertImage : System.Web.UI.Page
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

        string str = FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(Server.MapPath("../Upload/" + str));
        string image = "../Upload/" + str.ToString();
        string imageCaption = txtCaption.Value;
        string filePath = FileUpload1.PostedFile.FileName;
        string fileName = Path.GetFileName(filePath);
        string imageDate = datepicker.Value;
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

                        SqlCommand cmd = new SqlCommand("insert_image", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@imageCaption", imageCaption);
                        cmd.Parameters.AddWithValue("@image", image);
                        cmd.Parameters.AddWithValue("@imageDate", imageDate);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@isCgImage", cbImage.Checked);
                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            con.Close();

                            lblErr.Text = "Image Uploaded Successfuly";
                            lblErr.ForeColor = System.Drawing.Color.ForestGreen;
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('"+lblErr.Text+"')", true);
                            txtCaption.Value = "";
                            datepicker.Value = "";
                        }
                    }
                }
                else
                {
                    lblErr.ForeColor = System.Drawing.Color.Red;
                    lblErr.Text = "Select Only jpg/jpeg/png or gif Files!!!"; // if file is other than speified extension   
                }
            }

            catch (Exception ex)
            {
                lblErr.Text = "Error: " + ex.Message.ToString();
            }
        }
        else
        {
            lblErr.Text = "Please Upload your Image";
            lblErr.ForeColor = System.Drawing.Color.Red;
        }
    }         
}