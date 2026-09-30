using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Admin_InsertAchalSampatti : System.Web.UI.Page
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
            ddlYear.DataSource = dt;
            ddlYear.Items.Clear();
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "id";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "- Select Year -");
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
            SqlCommand cmd = new SqlCommand("select_year", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            con.Close();
            con.Dispose();

        }
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

        string empNameH = empNameHindi.Value;
        string empNameE = empNameEng.Value;
        string designationH = designationHindi.Value;
        string designationE = designationEnglish.Value;

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

                        SqlCommand cmd = new SqlCommand("insert_achal_sampatti", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@empNameH", SqlDbType.NVarChar).Value = empNameH;
                        cmd.Parameters.Add("@empNameE", SqlDbType.VarChar).Value = empNameE;
                        cmd.Parameters.Add("@designationH", SqlDbType.NVarChar).Value = designationH;
                        cmd.Parameters.Add("@designationE", SqlDbType.VarChar).Value = designationE;
                        cmd.Parameters.Add("@yearId", SqlDbType.Int).Value = ddlYear.SelectedValue;
                        cmd.Parameters.Add("@fileName", SqlDbType.NVarChar).Value = fileName;
                        cmd.Parameters.Add("@contentType", SqlDbType.VarChar).Value = type;
                        cmd.Parameters.Add("@fileData", SqlDbType.Binary).Value = bytes;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        lblErr.ForeColor = System.Drawing.Color.Green;
                        lblErr.Text = "File Uploaded Successfully!!!";

                        empNameHindi.Value = "";
                        empNameEng.Value = "";
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