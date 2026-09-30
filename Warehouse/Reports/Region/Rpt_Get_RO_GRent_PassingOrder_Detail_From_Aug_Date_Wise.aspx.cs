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
using System.Text;

public partial class Region_Reports_Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug_Date_Wise : System.Web.UI.Page
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
           
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
   
    private void ExportGridView()
    {
        string attachment = "attachment; filename=Contacts.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    //private void ExportGridToExcel()
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ClearContent();
    //    Response.ClearHeaders();
    //    Response.Charset = "";
    //    string FileName = "VC_Get_Details_Reports" + DateTime.Now + ".xls";
    //    StringWriter strwritter = new StringWriter();
    //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
    //    GridView1.GridLines = GridLines.Both;
    //    GridView1.HeaderStyle.Font.Bold = true;
    //    GridView1.RenderControl(htmltextwrtter);
    //    Response.Write(strwritter.ToString());
    //    Response.End();

    //}
    //private void ExportGridToExcel()
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ClearContent();
    //    Response.ClearHeaders();
    //    Response.Charset = "";
    //    string FileName = "Vithal" + DateTime.Now + ".xls";
    //    StringWriter strwritter = new StringWriter();
    //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
    //    GridView1.GridLines = GridLines.Both;
    //    GridView1.HeaderStyle.Font.Bold = true;
    //    GridView1.RenderControl(htmltextwrtter);
    //    Response.Write(strwritter.ToString());
    //    Response.End();

    //}
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Rpt_Get_RO_GRent_PassingOrder_Detail_From_Aug.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
            fillgrid();

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
   
    protected void Print(object sender, EventArgs e)
    {
        GridView1.UseAccessibleHeader = true;
        GridView1.Columns[0].HeaderText = "Header text";
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        GridView1.Attributes["style"] = "border-collapse:separate";
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
            {
                row.Attributes["style"] = "page-break-after:always;";

                //"Bio-Data For Devi Ahila University Indore";
            }
        }
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        GridView1.RenderControl(hw);
        // gvCol7.Columns[0].HeaderText = "Header text";
        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=10,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
        sb.Append(style + gridHTML);
        sb.Append("\");");
        //sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        //sb.Append("printWin.close();");
        sb.Append("};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        GridView1.DataBind();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_RO_GRent_PassingOrder_Detail_From_Aug_Date_wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_Logid"].ToString());
                cmd.Parameters.AddWithValue("@FromDate", txtdatefrom.Text);
                cmd.Parameters.AddWithValue("@ToDate", txtdateto.Text);
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
                            GridView1.Caption = @"<b>"+" M.P. Warehousing & Logistics Corporarion " + "</br> " + "Passing Order Details From"+" - " +txtdatefrom.Text+ " To "+txtdateto.Text+"</b> ";
                            // GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);
                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[13].Text = "Total";
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Rent_Bill_AMT")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Amt")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Gain_Detuction_Amount")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Other_Detuction_Amt")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BM_Deduction")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Deduction_AMT")).ToString();
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PayToGO")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("PAYtoMPWLC")).ToString();
                            GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("StorageCharBillAMt")).ToString();
                            divshowdetails.Visible = true;
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            divshowdetails.Visible = false;
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
       
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {       

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {       
            fillgrid();
    }
}