using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_DeleteNews : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (!IsPostBack)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("../Login/Login.aspx");
                return;
            }
            ShowData();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ViewStateUserKey = Session.SessionID;
    }

    protected void ShowData()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_news", con))
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

        // CommandArgument safe parsing
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("delete_news", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        lblErr.Text = HttpUtility.HtmlEncode("Record Deleted Successfully!!!");
        ShowData();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ShowData();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = 0;
        LinkButton btn = sender as LinkButton;

        // 1. Safe parsing to avoid InvalidCastException / FormatException
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        byte[] bytes = null;
        string fileName = string.Empty;
        string contentType = string.Empty;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_news_by_id", con))
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
            // 2. Path Traversal & CRLF Injection Prevention
            string safeFileName = Path.GetFileName(fileName);
            safeFileName = safeFileName.Replace("\r", "").Replace("\n", "");

            // 3. Fallback to generic binary application stream if Content-Type is missing/untrusted
            string safeContentType = string.IsNullOrWhiteSpace(contentType)
                ? "application/octet-stream"
                : contentType.Replace("\r", "").Replace("\n", "");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = safeContentType;

            // 4. Defense-in-depth security headers against persistent script rendering / MIME-sniffing
            Response.AddHeader("X-Content-Type-Options", "nosniff");
            Response.AddHeader("Content-Security-Policy", "default-src 'none'");

            // 5. Encoded Content-Disposition Header (Forces download & prevents browser inline rendering)
            string encodedFileName = HttpUtility.UrlEncode(safeFileName).Replace("+", "%20");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + safeFileName + "\"; filename*=UTF-8''" + encodedFileName);
            Response.AddHeader("Content-Length", bytes.Length.ToString());

            // 6. Secure Binary Output
            Response.BinaryWrite(bytes);
            Response.Flush();

            // Prevents ThreadAbortException caused by Response.End()
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    protected void View(object sender, EventArgs e)
    {
        int id = 0;
        LinkButton btn = sender as LinkButton;
        if (btn != null && int.TryParse(btn.CommandArgument, out id))
        {
            Session["Id"] = id;
            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/News.aspx','_blank');", true);
        }
    }
}