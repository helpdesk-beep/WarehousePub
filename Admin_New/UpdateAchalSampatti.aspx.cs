using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Admin_UpdateAchalSampatti : System.Web.UI.Page
{
    int yearId = 0;
    DataTable dt;
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

    SqlConnection con;
    SqlDataAdapter adapt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (!IsPostBack)
        {
            GridView1.Enabled = true;
            DataTable dt = GetData();
            ddlYear.DataSource = dt;
            ddlYear.Items.Clear();
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "id";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "- Select Year -");
            BindGrid();
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
            SqlCommand cmd = new SqlCommand("select_top_two_year", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);
            con.Close();
            con.Dispose();

        }
        return dt;
    }

    //ShowData method for Displaying Data in Gridview  
    private void BindGrid()
    {
        int id;
        Int32.TryParse(ddlYear.SelectedValue, out id);
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_achal_sampatti_by_yearId", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.Parameters.AddWithValue("@yearId", id);
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }
    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GridView1.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;
        HiddenField id = GridView1.Rows[e.RowIndex].FindControl("hdnID") as HiddenField;
        FileUpload FileUpload1 = (FileUpload)GridView1.Rows[e.RowIndex].FindControl("FileUpload1");
        TextBox NameH = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNameH");
        TextBox NameE = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNameE");
        TextBox DesigH = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDesignationH");
        TextBox DesigE = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtDesignationE");

        string empNameH = NameH.Text;
        string empNameE = NameE.Text;
        string designationH = DesigH.Text;
        string designationE = DesigE.Text;

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
                        SqlCommand cmd = new SqlCommand("update_achal_sampatti", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@empNameH", SqlDbType.NVarChar)).Value = empNameH;
                        cmd.Parameters.Add(new SqlParameter("@empNameE", SqlDbType.VarChar)).Value = empNameE;
                        cmd.Parameters.Add(new SqlParameter("@designationH", SqlDbType.NVarChar)).Value = designationH;
                        cmd.Parameters.Add(new SqlParameter("@designationE", SqlDbType.VarChar)).Value = designationE;
                        cmd.Parameters.AddWithValue("@fileName", fileName);
                        cmd.Parameters.Add("@fileData", SqlDbType.Binary).Value = bytes;
                        cmd.Parameters.AddWithValue("@id", id.Value);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                        GridView1.EditIndex = -1;
                        //Call ShowData method for displaying updated data  
                        BindGrid();

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
                SqlCommand cmd = new SqlCommand("update_achal_sampatti", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@empNameH", SqlDbType.NVarChar)).Value = empNameH;
                cmd.Parameters.Add(new SqlParameter("@empNameE", SqlDbType.VarChar)).Value = empNameE;
                cmd.Parameters.Add(new SqlParameter("@designationH", SqlDbType.NVarChar)).Value = designationH;
                cmd.Parameters.Add(new SqlParameter("@designationE", SqlDbType.VarChar)).Value = designationE;
                cmd.Parameters.AddWithValue("@fileName", "");
                cmd.Parameters.AddWithValue("@fileData", 0);
                cmd.Parameters.AddWithValue("@id", id.Value);
                // cmd.Parameters.Add("@fileData", SqlDbType.VarBinary).Value = fData.Value;
                cmd.ExecuteNonQuery();
                con.Close();
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GridView1.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                BindGrid();

                lblErr.ForeColor = System.Drawing.Color.Green;
                lblErr.Text = "Achal Sampatti Updated Successfully!!!";
            }
        }
    }
    protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GridView1.EditIndex = -1;
        BindGrid();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void View(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        Session["Id"] = id;
        ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/AchalSampatti.aspx','_blank' );", true);
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        yearId = ddlYear.SelectedIndex;
        BindGrid();
    }
}