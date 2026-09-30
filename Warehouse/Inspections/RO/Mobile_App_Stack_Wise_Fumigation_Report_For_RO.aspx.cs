using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_RO_Mobile_App_Stack_Wise_Fumigation_Report_For_RO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
    ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {
            BindReport();
        }
    }

    //private void BindReport()
    //{
    //    string GodownID = "0";

    //    if (Request.QueryString["GodownID"] != null)
    //    {
    //        GodownID =
    //            Request.QueryString["GodownID"];
    //    }

    //    SqlCommand cmd = new SqlCommand(
    //        "Proc_Mobile_App_Godown_Detail_Fumigation_Report_BO",
    //        con);

    //    cmd.CommandType =
    //        CommandType.StoredProcedure;

    //    cmd.Parameters.AddWithValue(
    //        "@GodownID",
    //        GodownID);

    //    SqlDataAdapter da =
    //        new SqlDataAdapter(cmd);

    //    DataTable dt =
    //        new DataTable();

    //    da.Fill(dt);

    //    gvReport.DataSource = dt;

    //    gvReport.DataBind();
    //}

    private void BindReport()
    {
        string GodownID = "0";
        string CommodityId = "0";

        if (Request.QueryString["GodownID"] != null)
        {
            GodownID =
                Request.QueryString["GodownID"].ToString();
        }

        if (Request.QueryString["Commodity_Id"] != null)
        {
            CommodityId =
                Request.QueryString["Commodity_Id"].ToString();
        }

        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_Godown_Detail_Fumigation_Report_BO",
            con);

        cmd.CommandType =
            CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue(
            "@GodownID",
            GodownID);

        cmd.Parameters.AddWithValue(
            "@Commodity_Id",
            CommodityId);

        SqlDataAdapter da =
            new SqlDataAdapter(cmd);

        DataTable dt =
            new DataTable();

        da.Fill(dt);

        gvReport.DataSource = dt;
        gvReport.DataBind();
    }

    //protected void btnBack_Click(
    //    object sender,
    //    EventArgs e)
    //{
    //    Response.Redirect(
    //        "Mobile_App_Branch_Summary_Fumigation_Report_BO.aspx");
    //}

    protected void btnExcel_Click(
        object sender,
        EventArgs e)
    {
        BindReport();

        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=Godown_Detail_Fumigation_Report.xls");

        Response.Charset = "";

        Response.ContentType =
            "application/vnd.ms-excel";

        StringWriter sw =
            new StringWriter();

        HtmlTextWriter hw =
            new HtmlTextWriter(sw);

        gvReport.GridLines =
            GridLines.Both;

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());

        Response.End();
    }

    public override void VerifyRenderingInServerForm(
        Control control)
    {
    }

}
