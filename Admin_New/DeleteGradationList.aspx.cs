using System;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_DeleteGradationList : System.Web.UI.Page
{
    int yearId = 0;
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (Session["username"] == null)
        {
            Response.Redirect("/Login/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_gradation_list", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
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

    protected void Delete(object sender, EventArgs e)
    {
        int id = 0;
        Button btn = sender as Button;
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("delete_gradation_list", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        lblErr.Text = HttpUtility.HtmlEncode("Record Deleted Successfully!!!");
        BindGrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = 0;
        LinkButton btn = sender as LinkButton;

        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        byte[] bytes = null;
        string fileName = string.Empty;
        string contentType = string.Empty;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_gradation_list_by_id", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        if (sdr["fileData"] != DBNull.Value)
                        {
                            bytes = (byte[])sdr["fileData"];
                        }
                        contentType = Convert.ToString(sdr["contentType"]);
                        fileName = Convert.ToString(sdr["fileName"]);
                    }
                }
            }
        }

        if (bytes != null && bytes.Length > 0)
        {
            // 1. Sanitize file name to prevent Path Traversal, Response Splitting & Header Injection
            string safeFileName = Path.GetFileName(fileName ?? "file");
            safeFileName = safeFileName.Replace("\r", "").Replace("\n", "").Replace("\"", "");

            // 2. Clean and validate Content-Type
            string cleanContentType = string.IsNullOrWhiteSpace(contentType)
                ? "application/octet-stream"
                : contentType.Replace("\r", "").Replace("\n", "");

            // Enforce octet-stream for executable/script/HTML types to neutralize Persistent XSS
            if (cleanContentType.Contains("html") || cleanContentType.Contains("javascript") || cleanContentType.Contains("xml"))
            {
                cleanContentType = "application/octet-stream";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = cleanContentType;

            // 3. Prevent MIME sniffing and enforce file download attachment header
            Response.AddHeader("X-Content-Type-Options", "nosniff");
            string encodedFileName = HttpUtility.UrlEncode(safeFileName).Replace("+", "%20");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + safeFileName + "\"; filename*=UTF-8''" + encodedFileName);
            Response.AddHeader("Content-Length", bytes.Length.ToString());

            // 4. Safe Binary Write
            Response.BinaryWrite(bytes);
            Response.Flush();

            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    protected void View(object sender, EventArgs e)
    {
        int id = 0;
        LinkButton btn = sender as LinkButton;
        if (btn != null && int.TryParse(btn.CommandArgument, out id) && id > 0)
        {
            Session["Id"] = id;
            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/Gradation.aspx','_blank');", true);
        }
    }
}