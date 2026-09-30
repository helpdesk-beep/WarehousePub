using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Admin_DeleteWhatsNew : System.Web.UI.Page
{
    int docTypeId = 0;
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
                Response.Redirect("/Login/Login.aspx");
                return;
            }

            DataTable dt = GetData();
            ddlDocType.DataSource = dt;
            ddlDocType.Items.Clear();
            ddlDocType.DataTextField = "e_doc_type";
            ddlDocType.DataValueField = "id";
            ddlDocType.DataBind();
            ddlDocType.Items.Insert(0, new ListItem("- Select Document Type -", "0"));
        }
    }

    DataTable GetData()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_doc_type", con))
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

    protected void ShowData()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("select_whatsNew_by_docTypeId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@docTypeId", docTypeId);

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

        // CommandArgument की सुरक्षित पार्सिंग
        if (btn == null || !int.TryParse(btn.CommandArgument, out id) || id <= 0)
        {
            return;
        }

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("delete_whatsnew", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        lblErr.Text = HttpUtility.HtmlEncode("Record Deleted Successfully!!!");

        // Selected Dropdown Value के आधार पर docTypeId को पास करके Grid अपडेट करना
        int.TryParse(ddlDocType.SelectedValue, out docTypeId);
        ShowData();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        int.TryParse(ddlDocType.SelectedValue, out docTypeId);
        ShowData();
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
            using (SqlCommand cmd = new SqlCommand("select_whatsnew_by_id", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        if (sdr["fileDataHn"] != DBNull.Value)
                        {
                            bytes = (byte[])sdr["fileDataHn"];
                        }
                        contentType = Convert.ToString(sdr["contentTypeHn"]);
                        fileName = Convert.ToString(sdr["fileNameHn"]);
                    }
                }
            }
        }

        if (bytes != null && bytes.Length > 0)
        {
            // 1. Path Traversal & Header Split Prevention
            string safeFileName = Path.GetFileName(fileName);
            safeFileName = safeFileName.Replace("\r", "").Replace("\n", "");

            // 2. Safe Content Type
            string safeContentType = string.IsNullOrWhiteSpace(contentType)
                ? "application/octet-stream"
                : contentType.Replace("\r", "").Replace("\n", "");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = safeContentType;

            // 3. Double-Quotes & UTF-8 Encoded Header (SCA XSS Fix)
            string encodedFileName = HttpUtility.UrlEncode(safeFileName).Replace("+", "%20");
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + safeFileName + "\"; filename*=UTF-8''" + encodedFileName);
            Response.AddHeader("Content-Length", bytes.Length.ToString());

            // 4. Safe Output
            Response.BinaryWrite(bytes);
            Response.Flush();

            // ThreadAbortException रोकने के लिए CompleteRequest का उपयोग
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
            ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('/Admin/WhatsNew.aspx','_blank');", true);
        }
    }

    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedType = 0;
        if (int.TryParse(ddlDocType.SelectedValue, out selectedType))
        {
            docTypeId = selectedType;
        }
        else
        {
            docTypeId = ddlDocType.SelectedIndex;
        }
        ShowData();
    }
}