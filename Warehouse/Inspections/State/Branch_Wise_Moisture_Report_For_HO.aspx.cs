using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_Branch_Wise_Moisture_Report_For_HO : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["WLCGodownGatePass"].ConnectionString);

    int PrevStack = 0;
    int CurrentStack = 0;
    int TotalStack = 0;
    int SentDM = 0;
    int SubmitFCI = 0;
    int FCIInspect = 0;

    string DistrictID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["DistrictID"] != null)
            {
                DistrictID = Request.QueryString["DistrictID"].ToString();

                BindReport();
            }
        }
    }

    private void BindReport()
    {
        SqlCommand cmd = new SqlCommand(
            "Get_Branch_Wise_Mosture_Report_For_HO", con);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@DistrictID", DistrictID);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        da.Fill(dt);

        gvDetails.DataSource = dt;

        gvDetails.DataBind();
    }

    protected void lnkBranch_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        string BranchID = lnk.CommandArgument;

        //Response.Redirect(
        //    "Godown_Wise_Moisture_Report_For_HO.aspx?BranchID=" + BranchID);
        Response.Write("<script>window.open('Godown_Wise_Moisture_Report_For_HO.aspx?BranchID=" + BranchID + "','_blank');</script>");
    }

    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(
    //        "District_Wise_Moisture_Report_For_HO.aspx");
    //}

    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PrevStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Prev Stack"));
            CurrentStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Current Stack"));
            TotalStack += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total Stack"));
            SentDM += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Sent_DM"));
            SubmitFCI += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Submit To FCI"));
            FCIInspect += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FCI Inspected Stack"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Font.Bold = true;
            e.Row.BackColor = Color.LightGray;

            e.Row.Cells[0].Text = "TOTAL";
            e.Row.Cells[1].Text = "";

            e.Row.Cells[2].Text = PrevStack.ToString();
            e.Row.Cells[3].Text = CurrentStack.ToString();
            e.Row.Cells[4].Text = TotalStack.ToString();
            e.Row.Cells[5].Text = SentDM.ToString();
            e.Row.Cells[6].Text = SubmitFCI.ToString();
            e.Row.Cells[7].Text = FCIInspect.ToString();
        }
    }

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;

    //    Response.AddHeader("content-disposition",
    //        "attachment;filename=Branch_Wise_Moisture_Report.xls");

    //    Response.ContentType = "application/vnd.ms-excel";

    //    StringWriter sw = new StringWriter();

    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    BindReport();

    //    gvDetails.RenderControl(hw);

    //    Response.Write(sw.ToString());

    //    Response.End();
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}