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

public partial class Reports_NewBillingReports_Godown_Wise_Repush_Bill_Details_New : System.Web.UI.Page
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
            GetDistrict();
        }
    }
    public void GetDistrict()
    {
        string qry = "";
        qry = "select distinct District_Id,District_Name from tbl_MetaData_DISTRICT Order By District_Name ASC";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    public void fillgrid()
    {
        string Dist_id = ddldistrict.SelectedValue;
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_District_wise_details_paid_Zero_payment_From_MPSCSC_to_MPWLC", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", Dist_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
                grdone.Visible = true;
                GrdBills.FooterRow.Cells[4].Text = "Total";
                GrdBills.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Bill_Number")).ToString();
                GrdBills.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Net_Amount")).ToString();
                GrdBills.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Payable_Amount")).ToString();
                GrdBills.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Deduction_by_HO_MPSCSC")).ToString();
                GrdBills.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("TDS_Deduction_by_HO_MPSCSC")).ToString();
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(GrdBills);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //fillgrid();         
    }
    private void ExportGridViewToExcel(GridView GrdBills)
    {
        // Clear the response
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Godown_Wise_Zero_Payment.xls";
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
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}