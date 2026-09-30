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

public partial class IssueCenterLevel_Storage_Reports_Rpt_Get_Bill_Detail_After_August_District_Branch_Godown_Wise_Difference_With_Amount : System.Web.UI.Page
{
   

    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    int StatusID;
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (string.IsNullOrEmpty(Session["UserName"] as string))
        //{
        //    Response.Redirect("~/login.aspx");
        //}
        //else if (Session["UserName"].ToString() == "MPSWLC")
        //{

        if (!IsPostBack)
        {
            fillgrid();
        }
        //}
        //else
        //{
        //    Response.Redirect("~/login.aspx");
        //}
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Godown_Wise_Get_Bill_Detail_After_August_Difference_With_Amount]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">District Branch Wise " + "</br> " + "M.P. Warehousing & Logistics Corporarion " + "</br> " + "PVT Godown Storage Charges Bill(After August)" + "</b> ";

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[2].Text = "Total";
                           // GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("noofgdwn")).ToString();
                            GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGenerateBill")).ToString();
                            GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillGenerateAmt")).ToString();
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfDSCSingBill")).ToString();
                            GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofBillAmtDSC")).ToString();
                            GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BilldiffDSC")).ToString();
                            GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BilldiffAmt")).ToString();
                            GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSUBBill")).ToString();
                            GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("SUBBillAmt")).ToString();
                            GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("DSCsignedbyBM")).ToString();
                            GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("DSCsignedbyBMAmt")).ToString();
                            GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfSignBill_CSMS")).ToString();
                            GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoofCSMS_BillAmt")).ToString();
                            GridView1.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("BMsubmited")).ToString();
                            GridView1.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("BMAmtsubmited")).ToString();
                            GridView1.FooterRow.Cells[17].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwnRM")).ToString();
                            GridView1.FooterRow.Cells[18].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Approved_By_AGM_Acc_of_RO_Leval_With_Amount")).ToString();
                            GridView1.FooterRow.Cells[19].Text = dt.AsEnumerable().Sum(row => row.Field<int>("SignedbyICM")).ToString();
                            GridView1.FooterRow.Cells[20].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AmtSignedbyICM")).ToString();
                            GridView1.FooterRow.Cells[21].Text = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfRMDSC")).ToString();
                            GridView1.FooterRow.Cells[22].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("NoOfRMDSCAmt")).ToString();
                            GridView1.FooterRow.Cells[23].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ApprovedbyAGM")).ToString();
                            GridView1.FooterRow.Cells[24].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("ApprovedAmtbyAGM")).ToString();
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "
        //Control 'gvCol7' of type 'GridView' must be placed inside a form tag with runat=server."
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "VC_Get_Details_Reports" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "District Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Branch Name";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "No. of Godown";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Bill generated by BM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Bill generated by BM (Amount in Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC Signed by BM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC Signed by BM (Amount in Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Bill Generated Diff. DSC Signed(in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Bill Generated Diff DSC Signed (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Submited to ICM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Submited to ICM (Amount in Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC Signed by BM Diff Submited to ICM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC Signed by BM Diff Submited to ICM (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Signed by ICM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Signed by ICM (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "BM Submited to ICM Diff Signed by ICM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "BM Submited to ICM Diff Signed by ICM (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Approved by AGM (A/C's) of RO (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Approved by AGM (A/C's) of RO (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Signed by ICM Diff Approved by AGM (A/C's) of RO (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Signed by ICM Diff Approved by AGM (A/C's) of RO (Amount Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC by RM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "DSC by RM (Amount in Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Approved by AGM (A/C's) of RO Diff DSC By RM (in No's)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Approved by AGM (A/C's) of RO Diff DSC By RM (Amount in Rs.)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

          
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }
}