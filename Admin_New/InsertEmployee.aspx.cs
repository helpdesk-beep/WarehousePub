using System;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

public partial class Admin_InsertEmployee : System.Web.UI.Page
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
        string image = "../Upload/" + str.ToString();
        string EmpNameH = txtNameH.Value;
        string EmpNameE = txtNameE.Value;
        string DOB = datepicker.Value;
        string DOJ = txtDOJ.Value;
        string Mobile = txtMobile.Value;
        string Email = txtEmail.Value;
        string Designation = txtDesignation.Value;

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

                        SqlCommand cmd = new SqlCommand("insert_employee", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpNameH", EmpNameH);
                        cmd.Parameters.AddWithValue("@EmpNameE", EmpNameE);
                        cmd.Parameters.AddWithValue("@DOB", DOB);
                        cmd.Parameters.AddWithValue("@DOJ", DOJ);
                        cmd.Parameters.AddWithValue("@Designation", Designation);
                        cmd.Parameters.AddWithValue("@Mobile", Mobile);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Image", image);
                       

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        lblErr.Text = "Employee Details Inserted Successfuly";
                        lblErr.ForeColor = System.Drawing.Color.ForestGreen;

                        txtNameH.Value = "";
                        txtNameE.Value = "";
                        txtMobile.Value = "";
                        txtEmail.Value = "";
                        txtDesignation.Value = "";
                        datepicker.Value = "";
                        txtDOJ.Value = "";

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