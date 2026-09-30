using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Mobile_App_Godown_Wise_Moisture_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
    ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            BindReport();
        }
    }

    private void BindReport()
    {
        string BranchID = "0";

        if (Request.QueryString["BranchID"] != null)
        {
            BranchID =
            Request.QueryString["BranchID"].ToString();
        }

        SqlCommand cmd = new SqlCommand(
            "Proc_Mobile_App_Godown_Wise_Moisture_Report_For_HO", con);

        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.AddWithValue("@BranchID",BranchID);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        da.Fill(dt);

        gvReport.DataSource = dt;
        gvReport.DataBind();

        lblTotalRecords.Text = dt.Rows.Count.ToString();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total_Stack") ?? 0);
            PrevStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack") ?? 0);
            TodayStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Stack") ?? 0);
            TotalMoistureStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Stack") ?? 0);
            PendingStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Pending Stack") ?? 0);

            PrevMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Sent_DM") ?? 0);
            TodayMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Sent_DM") ?? 0);
            TotalMoistureSentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Sent_DM") ?? 0);

            PrevSubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Moisture Submit To FCI") ?? 0);
            TodaySubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Today Moisture Submit To FCI") ?? 0);
            TotalSubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Moisture Submit To FCI") ?? 0);

            FCIInspectedStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack") ?? 0);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "";
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

            if (e.Row.Cells.Count > 14)
            {
                e.Row.Cells[14].Text = "";
            }

            e.Row.Font.Bold = true;
            e.Row.BackColor = System.Drawing.Color.LightGray;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=GodownWiseMoistureReport.xls");

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvReport.RenderControl(hw);

        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

}
