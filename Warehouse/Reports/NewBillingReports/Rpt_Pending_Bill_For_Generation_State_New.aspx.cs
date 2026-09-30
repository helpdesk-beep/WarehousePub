using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_NewBillingReports_Rpt_Pending_Bill_For_Generation_State_New : System.Web.UI.Page
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
            //fillRegion();
            FillGrid();
        }
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Pending_Bill_For_State", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdbill.DataSource = dt;
                            grdbill.DataBind();
                            //Div1.Visible = true;
                        }
                        else
                        {
                            grdbill.DataSource = null;
                            grdbill.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        FillGrid();
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
        bool showPager = grdbill.AllowPaging;
        grdbill.PagerSettings.Visible = false;

        // ✅ Rebind only current page data
        grdbill.DataSource = (DataTable)ViewState["dt"];
        grdbill.DataBind();

        // ✅ Render the GridView
        grdbill.RenderControl(hw);

        // ✅ Restore pager after export
        grdbill.PagerSettings.Visible = showPager;

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
    protected void grdbill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            string godownId = e.CommandArgument.ToString();
            // Redirect to new details page with Godown_ID as query string
            Response.Redirect("~/Region/Reports/Rpt_Godown_Bill_Details.aspx?GodownID=" + godownId);
        }
    }
}