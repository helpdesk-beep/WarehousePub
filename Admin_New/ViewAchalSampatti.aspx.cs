using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_ViewAchalSampatti : System.Web.UI.Page
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
            DataTable dtYears = GetData();
            ddlYear.DataSource = dtYears;
            ddlYear.Items.Clear();
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "id";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("- Select Year -", "0"));
        }
    }

    DataTable GetData()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_achal_sampatti_by_yearId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adpt = new SqlDataAdapter(cmd))
                {
                    adpt.Fill(dt);
                }
            }
        }
        return dt;
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
            using (SqlCommand cmd = new SqlCommand("select_achal_sampatti_by_yearId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@yearId", yearId);

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
            using (SqlCommand cmd = new SqlCommand("select_achal_sampatti_doc_by_id", con))
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
            // 1. Sanitize file name to avoid Directory Traversal, Response Splitting / CRLF Injection
            string safeFileName = Path.GetFileName(fileName ?? "file");
            safeFileName = safeFileName.Replace("\r", "").Replace("\n", "").Replace("\"", "");

            // 2. Standardize Content-Type and enforce nosniff header to mitigate Persistent XSS via MIME-sniffing
            string cleanContentType = string.IsNullOrWhiteSpace(contentType)
                ? "application/octet-stream"
                : contentType.Replace("\r", "").Replace("\n", "");

            // Disallow dynamic execution of dangerous types like text/html or text/javascript from database contents
            if (cleanContentType.Contains("html") || cleanContentType.Contains("javascript") || cleanContentType.Contains("xml"))
            {
                cleanContentType = "application/octet-stream";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = cleanContentType;

            // 3. Security headers to force attachment download and block execution
            Response.AddHeader("X-Content-Type-Options", "nosniff");
            string encodedFileName = HttpUtility.UrlEncode(safeFileName).Replace("+", "%20");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + safeFileName + "\"; filename*=UTF-8''" + encodedFileName);
            Response.AddHeader("Content-Length", bytes.Length.ToString());

            // 4. Safe binary delivery
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
            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/AchalSampatti.aspx','_blank');", true);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedYear;
        if (int.TryParse(ddlYear.SelectedValue, out selectedYear))
        {
            yearId = selectedYear;
        }
        else
        {
            yearId = ddlYear.SelectedIndex;
        }
        BindGrid();
    }
}