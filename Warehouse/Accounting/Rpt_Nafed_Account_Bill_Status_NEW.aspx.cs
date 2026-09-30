using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Drawing;
using System.IO;

public partial class Accounting_Rpt_Nafed_Account_Bill_Status_NEW : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }

    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Bill_Status_at_NAFED_Account", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                GrdBills.FooterRow.Style.Add("text-align", "Right");
                GrdBills.FooterRow.Cells[2].Text = "Total";
                GrdBills.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Approved_By_Marketing")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_Marketing_Amount")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Approved_By_Account")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_Account_Amount")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BalancetobeApprove")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmounttobeApprove")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Paid_Bill_Count")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Paid_Bill_Amount")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Deduction_Amount")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Balancetobepaidincludingrejected")).ToString("#,##0.00");
                GrdBills.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmounttobepaidincludingrejected")).ToString("#,##0.00");

            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
            }
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        //cell.Text = "";
        cell.ColumnSpan = 3;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Bills Submitted by Marketing";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Bills Approved For Payment";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Pending Bills at NAFED Account";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Bill Paid";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Balance";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GrdBills.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GrdBills);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //fillgrid();         
    //}
    private void ExportGridViewToExcel(GridView GrdBills)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "NAFED_StorageBill_Approval_State.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GrdBills.GridLines = GridLines.Both;
        GrdBills.HeaderStyle.Font.Bold = true;
        GrdBills.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}