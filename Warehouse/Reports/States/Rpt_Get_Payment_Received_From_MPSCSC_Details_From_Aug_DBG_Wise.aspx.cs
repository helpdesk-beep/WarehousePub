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

public partial class Region_States_Rpt_Get_Payment_Received_From_MPSCSC_Details_From_Aug_DBG_Wise : System.Web.UI.Page
{
    int qtyTotal = 0;
    int grQtyTotal = 0;
    decimal qtyTotal1 = 0;
    decimal qtyTotal2 = 0;
    decimal qtyTotal3 = 0;
    decimal qtyTotal4 = 0;
    decimal qtyTotal5 = 0;
    decimal qtyTotal6 = 0;


    decimal grQtyTotal1 = 0;
    decimal grQtyTotal2 = 0;
    decimal grQtyTotal3 = 0;
    decimal grQtyTotal4 = 0;
    decimal grQtyTotal5 = 0;
    decimal grQtyTotal6 = 0;
    decimal grQtyTotal7 = 0;


    int storid = 0;
    int rowIndex = 1;


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
            fillDistrict();
            DateTime dNow = DateTime.Now;

            DD_Monthbind();
            for (int i = 1; i <= 12; i++)
            {
                ddlmonth.Items.Add(i.ToString());
            }
            ddlmonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true; // Set current month as selected
                                                                                              // ddlMonth.Enabled = false;
                                                                                              // lblmonthid.Value = ddlmonth.SelectedValue;
        }
        //}
        //else
        //{
        //    Response.Redirect("~/login.aspx");
        //}
    }
    private void DD_Monthbind()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            // DropDownList1.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));

        }

    }
    public void fillDistrict()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            string str = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            // lbl_err.Text = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message.ToString() + "')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    public void fillBranch()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            SqlCommand cmd = new SqlCommand("Fill_Branch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds;
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, new ListItem("-- Select Branch --", "0"));

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            // lbl_err.Text = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message.ToString() + "')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }

    public void fillGodown()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            { con.Open(); }
            SqlCommand cmd = new SqlCommand("Fill_Godown_List", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_ID", ddlDepotList.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cmd.CommandTimeout = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds;
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_id";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, new ListItem("-- Select Branch --", "0"));

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Not Available!')", true);
            }

        }
        catch (Exception ex)
        {
            // lbl_err.Text = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message.ToString() + "')", true);
        }
        finally
        { if (con.State == ConnectionState.Open) { con.Close(); } }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Payment_Details_From_Aug_Godown_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlDepotList.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_ID", ddlgodown.SelectedValue);
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
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
                            GridView1.Caption = @"<b style=""font-weight: bold;"">Region,District,Branch,Godown Wise " + "</br> " + "Payment Received Details From MPSCSC " + "</br> ";
                            //GridView1.Columns[1].Visible = false;
                            // GridView1.columns.RemoveAt(1);

                            GridView1.FooterRow.Style.Add("text-align", "right");
                            GridView1.FooterRow.Cells[4].Text = "Total";
                            GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
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
        cell.Text = "Commodity Name";
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
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Distirct Name!....')", true);
            ddlDistrict.Focus();
            return;
        }
        else if (ddlDepotList.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch Name!....')", true);
            ddlDepotList.Focus();
            return;
        }
        else if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Section Name!....')", true);
            ddlDistrict.Focus();
            return;
        }
        else if (ddlmonth.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Month!....')", true);
            ddlmonth.Focus();
            return;
        }
        fillgrid();

    }
}