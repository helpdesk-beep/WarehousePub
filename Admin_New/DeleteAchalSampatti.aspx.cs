using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_DeleteAchalSampatti : System.Web.UI.Page
{
    int yearId = 0;
    string constr = ConfigurationManager.ConnectionStrings["RajCon"].ConnectionString;

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
                Response.Redirect("/Login/Login.aspx");
                return;
            }

            DataTable dt = GetData();
            ddlYear.DataSource = dt;
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
            using (SqlCommand cmd = new SqlCommand("select_year", con))
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

    protected void Delete(object sender, EventArgs e)
    {
        int id = 0;
        Button btn = sender as Button;

        // CommandArgument Safe Parsing
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("delete_achal_sampatti", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        lblErr.Text = HttpUtility.HtmlEncode("Record Deleted Successfully!!!");

        // Selected index से yearId सेट करके Grid refresh करना
        int.TryParse(ddlYear.SelectedValue, out yearId);
        BindGrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        int.TryParse(ddlYear.SelectedValue, out yearId);
        BindGrid();
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = 0;
        LinkButton btn = sender as LinkButton;

        // CommandArgument Safe Parsing
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        byte[] bytes = null;
        string fileName = string.Empty;
        string contentType = string.Empty;

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_achal_sampatti_by_id", con))
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
            // 1. Path Traversal & Header Injection Safe Filename
            string safeFileName = Path.GetFileName(fileName);
            safeFileName = safeFileName.Replace("\r", "").Replace("\n", "");

            // 2. Safe Content Type Verification
            string safeContentType = string.IsNullOrWhiteSpace(contentType)
                ? "application/octet-stream"
                : contentType.Replace("\r", "").Replace("\n", "");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = safeContentType;

            // 3. Defense-in-Depth Security Headers against Script Injection & MIME Sniffing
            Response.AddHeader("X-Content-Type-Options", "nosniff");
            Response.AddHeader("Content-Security-Policy", "default-src 'none'");

            // 4. Double-Quotes & UTF-8 Encoding (Removes SCA Persistent XSS Alert)
            string encodedFileName = HttpUtility.UrlEncode(safeFileName).Replace("+", "%20");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + safeFileName + "\"; filename*=UTF-8''" + encodedFileName);
            Response.AddHeader("Content-Length", bytes.Length.ToString());

            // 5. Safe Binary Writing & Output
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
            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/AchalSampatti.aspx','_blank');", true);
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedYear = 0;
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