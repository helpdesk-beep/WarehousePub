using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Region_States_Rpt_Get_Data_Not_Selection_For_FCI_PDS : System.Web.UI.Page
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
        }

    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //using (SqlCommand cmd = new SqlCommand("Get_Amount_From_MPSCSC_From_Aug", con))
            using (SqlCommand cmd = new SqlCommand("Get_Not_Selection_Data_For_PDS_FCI", con))
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
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Caption = @"<b style=""font-weight: bold;"">District Wise " + "</br> " + "FIFO पद्धति से उठाव हेतु फ्रिज नहीं किये गये Stock की जानकारी शाखाओ वार" + "</br> ";
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQTY")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalQTY_For_PDS_FCI")).ToString();
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
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
        cell.Text = "जिले का नाम";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा का नाम";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "फ्रिज करने हेतु कुल मात्रा";
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "शाखा द्वारा फ्रिज की गई कुल मात्रा";
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
}