using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Mobile_App_District_Wise_Moisture_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindReport();
        }
    }
    int TotalStack = 0;
    int PrevStack = 0;
    int TodayStack = 0;
    int TotalMoistureStack = 0;
    int PendingStack = 0;

    int PrevMoistureSentDM = 0;
    int TodayMoistureSentDM = 0;
    int TotalMoistureSentDM = 0;

    int PrevSubmitFCI = 0;
    int TodaySubmitFCI = 0;
    int TotalSubmitFCI = 0;

    int FCIInspectedStack = 0;

    private void BindReport()
    {
        try
        {
            if (Session["Depot_DistID"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            SqlCommand cmd = new SqlCommand(
                "Proc_Mobile_App_Branch_Wise_Moisture_Report_For_HO",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@DistrictId",
                Session["Depot_DistID"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvReport.DataSource = dt;
            gvReport.DataBind();

            lblTotalRecords.Text = dt.Rows.Count.ToString();

            btnExport.Visible = dt.Rows.Count > 0;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "msg",
                "alert('" + ex.Message.Replace("'", "") + "');",
                true);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        PrepareGridViewForExport(gvReport);

        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=DistrictWiseMoistureReport.xls");

        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        sw.WriteLine("<table border='0' width='100%'>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='15' align='center' style='font-size:18px;font-weight:bold'>");
        sw.WriteLine("M.P. Warehousing & Logistics Corporation");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='15' align='center' style='font-size:14px;font-weight:bold'>");
        sw.WriteLine("District Wise Moisture Report");
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("<tr>");
        sw.WriteLine("<td colspan='15' align='center'>");
        sw.WriteLine("Date : " + DateTime.Now.ToString("dd/MM/yyyy"));
        sw.WriteLine("</td>");
        sw.WriteLine("</tr>");

        sw.WriteLine("</table><br/>");

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    //protected void lnkBranch_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;

    //    Session["BranchID"] = lnk.CommandArgument;

    //    Response.Redirect(
    //        "~/District/Mobile_App_Godown_Wise_Moisture_Report.aspx");
    //}

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack"));
            PrevStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack"));
            TodayStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Stack"));
            TotalMoistureStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Stack"));
            PendingStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending Stack"));

            PrevMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Sent_DM"));
            TodayMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Sent_DM"));
            TotalMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Sent_DM"));

            PrevSubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Submit To FCI"));
            TodaySubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Submit To FCI"));
            TotalSubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Submit To FCI"));

            FCIInspectedStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "GRAND TOTAL";

            e.Row.Cells[2].Text = TotalStack.ToString();
            e.Row.Cells[3].Text = PrevStack.ToString();
            e.Row.Cells[4].Text = TodayStack.ToString();
            e.Row.Cells[5].Text = TotalMoistureStack.ToString();
            e.Row.Cells[6].Text = PendingStack.ToString();

            e.Row.Cells[7].Text = PrevMoistureSentDM.ToString();
            e.Row.Cells[8].Text = TodayMoistureSentDM.ToString();
            e.Row.Cells[9].Text = TotalMoistureSentDM.ToString();

            e.Row.Cells[10].Text = PrevSubmitFCI.ToString();
            e.Row.Cells[11].Text = TodaySubmitFCI.ToString();
            e.Row.Cells[12].Text = TotalSubmitFCI.ToString();

            e.Row.Cells[13].Text = FCIInspectedStack.ToString();

            e.Row.Font.Bold = true;
        }
    }

    private void PrepareGridViewForExport(Control gv)
    {
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            Control current = gv.Controls[i];

            if (current is LinkButton)
            {
                gv.Controls.Remove(current);

                gv.Controls.AddAt(i,
                    new LiteralControl((current as LinkButton).Text));
            }

            if (current.HasControls())
            {
                PrepareGridViewForExport(current);
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}