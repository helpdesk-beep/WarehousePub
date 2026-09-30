using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_District_Wise_Moisture_Report_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    int PrevStack = 0;
    int CurrentStack = 0;
    int TotalStack = 0;
    int SentDM = 0;
    int SubmitFCI = 0;
    int FCIInspect = 0;

    string RegionID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["RegionID"] != null)
            {
                RegionID = Request.QueryString["RegionID"].ToString();

                BindReport();
            }
        }
    }

    private void BindReport()
    {
        try
        {
            SqlCommand cmd = new SqlCommand(
                "Get_District_Wise_Mosture_Report_For_HO", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RegionID", RegionID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            gvDetails.DataSource = dt;

            gvDetails.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    protected void lnkDistrict_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        string DistrictID = lnk.CommandArgument;

        Response.Write("<script>window.open('Branch_Wise_Moisture_Report_For_HO.aspx?DistrictID="
    + DistrictID + "','_blank');</script>");
    }

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.BackColor = ColorTranslator.FromHtml("#0b6d90");
            e.Row.ForeColor = Color.White;
            e.Row.Font.Bold = true;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PrevStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack"));
            CurrentStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current Stack"));
            TotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Stack"));
            SentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Sent_DM"));
            SubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Submit To FCI"));
            FCIInspect += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));

            foreach (TableCell cell in e.Row.Cells)
            {
                cell.BorderStyle = BorderStyle.Solid;
                cell.BorderWidth = Unit.Pixel(1);
                cell.BorderColor = Color.Black;
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.BackColor = Color.LightGray;
            e.Row.Font.Bold = true;

            e.Row.Cells[0].Text = "TOTAL";
            e.Row.Cells[1].Text = "";

            e.Row.Cells[2].Text = PrevStack.ToString();
            e.Row.Cells[3].Text = CurrentStack.ToString();
            e.Row.Cells[4].Text = TotalStack.ToString();
            e.Row.Cells[5].Text = SentDM.ToString();
            e.Row.Cells[6].Text = SubmitFCI.ToString();
            e.Row.Cells[7].Text = FCIInspect.ToString();

            foreach (TableCell cell in e.Row.Cells)
            {
                cell.BorderStyle = BorderStyle.Solid;
                cell.BorderWidth = Unit.Pixel(1);
                cell.BorderColor = Color.Black;
                cell.HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;

    //    Response.AddHeader(
    //        "content-disposition",
    //        "attachment;filename=District_Wise_Moisture_Report_For_HO.xls");

    //    Response.Charset = "";

    //    Response.ContentType = "application/vnd.ms-excel";

    //    StringWriter sw = new StringWriter();

    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    BindReport();

    //    gvDetails.GridLines = GridLines.Both;

    //    gvDetails.RenderControl(hw);

    //    string style = @"
    //    <style>
    //        table{
    //            border-collapse:collapse;
    //            width:100%;
    //        }
    //        th{
    //            background-color:#0b6d90;
    //            color:white;
    //            border:1px solid black;
    //            padding:8px;
    //            text-align:center;
    //        }
    //        td{
    //            border:1px solid black;
    //            padding:8px;
    //            text-align:center;
    //        }
    //    </style>";

    //    Response.Write(style);

    //    Response.Output.Write(sw.ToString());

    //    Response.Flush();

    //    Response.End();
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}