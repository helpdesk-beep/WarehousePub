using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Rpt_NCCF_Godown_Wise_Payment_Status : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    decimal totalGenValue = 0;
    decimal totalPendBranchValue = 0;
    decimal totalPendNCCFValue = 0;
    decimal totalRecValue = 0;
    decimal totalRMSubAmt = 0;
    decimal totalRMPendingAmt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Region_ID"] != null))
        {
            if (!IsPostBack)
            {
                //int RegionID = Convert.ToInt32(Request.QueryString["RegionID"]);
                lblRegion.Text = Session["UserName"].ToString();

                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_NCCF_Godown_Wise_Bill_Payment_Status_For_Region", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                CalculateTotals(dt);
            }
        }
    }
    private void CalculateTotals(DataTable dt)
    {

        foreach (DataRow row in dt.Rows)
        {
            totalGenValue += Convert.ToDecimal(row["BillAmt"]);
            totalPendBranchValue += Convert.ToDecimal(row["PendingBillAmountForSubmision"]);
            totalPendNCCFValue += Convert.ToDecimal(row["TotalNoofPendingBillAmountatNCCF"]);
            totalRecValue += Convert.ToDecimal(row["NoofbillPaymentAmountReceivedFromNCCF"]);
            totalRMSubAmt += Convert.ToDecimal(row["RMsubmitbillAmttonccf"]);
            totalRMPendingAmt += Convert.ToDecimal(row["PendingBillAmountForSubmisionatRM"]);
        }

        totalGen.InnerText = totalGenValue.ToString("N2");
        totalPendBranch.InnerText = totalPendBranchValue.ToString("N2");
        //totalPendNCCF.InnerText = ((totalGen.InnerText) - (totalRec.InnerText));
        totalPendNCCF.InnerText = totalPendNCCFValue.ToString("N2");
        totalRec.InnerText = totalRecValue.ToString("N2");
        totalRMSub.InnerText = totalRMSubAmt.ToString("N2");
        totalRMPending.InnerText = totalRMPendingAmt.ToString("N2");
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal pendingAmt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PendingBillAmountForSubmision"));
            if (pendingAmt > 0)
            {
                e.Row.Cells[8].BackColor = System.Drawing.Color.MistyRose; // Highlight Pending Amount cell
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GodownReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

        GridView1.AllowPaging = false;

        int RegionID = Convert.ToInt32(Session["Region_ID"].ToString());
        BindGrid();

        GridView1.RenderControl(hw);

        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    // Required
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}