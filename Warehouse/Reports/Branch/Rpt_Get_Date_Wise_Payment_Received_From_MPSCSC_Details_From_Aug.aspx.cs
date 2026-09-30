using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
using System.Text;
using System.Globalization;

public partial class Reports_Branch_Rpt_Get_Date_Wise_Payment_Received_From_MPSCSC_Details_From_Aug : System.Web.UI.Page
{
   
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillgrid();
            // ddlMonth.Enabled = false;
            txtpaymentdate.Attributes.Add("readonly", "readonly");                                                                                // lblmonthid.Value = ddlmonth.SelectedValue;
        }
       
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    private void DD_Monthbind()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            // DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));

        }

    }
    
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Details_From_Aug_Branch_Date_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Disrict_ID", Session["Depot_DistID"].ToString());
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Date", txtpaymentdate.Text);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Date Wise " + "</br> " + "Payment Received Details From MPSCSC " + "</br> ";
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[8].Text = "Total";
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
                            showdetails.Visible = true;
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            showdetails.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "S.No.";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Region";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "District";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Branch Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Godown Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Bill Number";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Month Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Commodity Name";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Payment Received Date";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "Payable Amount";
        row.Controls.Add(cell);


        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {
        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        GridView1.AllowPaging = false;
        //        fillgrid();

        //        GridView1.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A2, 20f, 20f, 20f, 20f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=Payment_Received_From_MPSCSC_Details_From_Aug.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (txtpaymentdate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date!....')", true);
            txtpaymentdate.Focus();
            return;
        }

        fillgrid();

    }
}