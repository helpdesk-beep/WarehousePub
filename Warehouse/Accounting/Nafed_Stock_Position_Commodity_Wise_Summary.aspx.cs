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
using System.IO;

public partial class Accounting_Nafed_Stock_Position_Commodity_Wise_Summary : System.Web.UI.Page
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
            //txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fillgrid();
        }
    }
    public void fillgrid()

    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Commodit_Wise_Nafed_Stock_Summary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtTranferDate.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdStock.DataSource = dt;
                GrdStock.DataBind();
                GrdStock.FooterRow.Cells[2].Text = "Total";
                GrdStock.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Available_Bags")).ToString();
                GrdStock.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Available_Qty")).ToString();
                Session["Date"] = txtTranferDate.Text.ToString();
                lblCrop_Year.InnerText = dt.Rows[0]["CropYear"].ToString();
                //Session["Crop_Year"]= lblCrop_Year.InnerText;
            }
            else
            {
                GrdStock.DataSource = null;
                GrdStock.DataBind();
            }
        }
    }
    protected void GrdStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdStock.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GrdStock);
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
    private void ExportGridViewToExcel(GridView GrdStock)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Nafed_Commodity_Wise_Stock_Position.xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GrdStock.GridLines = GridLines.Both;
        GrdStock.HeaderStyle.Font.Bold = true;
        GrdStock.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

}