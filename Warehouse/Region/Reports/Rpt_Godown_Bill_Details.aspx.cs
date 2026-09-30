using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Region_Reports_Rpt_Godown_Bill_Details : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GodownID"] != null)
            {
                string godownId = Request.QueryString["GodownID"];
                BindBillDetails(godownId);
            }
        }
    }
    private void BindBillDetails(string godownId)
    {
        DataTable dt = new DataTable();

        using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Pending_Bill_For_Submition", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", godownId);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }

        grdBillDetails.DataSource = dt;
        grdBillDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            lblGodownInfo.Text = "Godown: " + dt.Rows[0]["Godown_Name"].ToString() +
                                 " (" + dt.Rows[0]["District_Name"].ToString() + ")";
        }
    }

    protected void grdBillDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBillDetails.PageIndex = e.NewPageIndex;
        if (Request.QueryString["GodownID"] != null)
        {
            BindBillDetails(Request.QueryString["GodownID"]);
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        // Prepare response
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Pending_Bill_For_Generation_CurrentPage.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        // ✅ Hide the pager temporarily
        bool showPager = grdBillDetails.AllowPaging;
        grdBillDetails.PagerSettings.Visible = false;

        // ✅ Rebind only current page data
        grdBillDetails.DataSource = (DataTable)ViewState["dt"];
        grdBillDetails.DataBind();

        // ✅ Render the GridView
        grdBillDetails.RenderControl(hw);

        // ✅ Restore pager after export
        grdBillDetails.PagerSettings.Visible = showPager;

        // ✅ Optional styling for better Excel look
        Response.Output.Write("<style> td, th { border:1px solid black; padding:4px; } th { font-weight:bold; background:#cfe2f3; }</style>");
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // This confirms that the control is rendered properly for export
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Region/Reports/Rpt_Pending_Bill_For_Generation.aspx");
    }
}