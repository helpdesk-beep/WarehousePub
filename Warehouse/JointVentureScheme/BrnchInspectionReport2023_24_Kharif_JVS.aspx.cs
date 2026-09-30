using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class JointVentureScheme_BrnchInspectionReport2023_24_Kharif_JVS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_Inspection_Report_For_Kharif_2023_24", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["UserId"].ToString());
            if (ddl_session.SelectedValue == "ALL")
            {
                cmd.Parameters.AddWithValue("@Type", "0");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Type", ddl_session.SelectedValue.ToString());
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = "";
                RegGrid.DataBind();
                RegGrid.DataSource = ds;
                RegGrid.DataBind();

                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("OfferCapacity"));
                RegGrid.FooterRow.Cells[1].Text = "Total";
                RegGrid.FooterRow.Cells[7].Text = total.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("InspectedCpt"));
                RegGrid.FooterRow.Cells[8].Text = total1.ToString("N2");
                this.RegGrid.Columns[1].Visible = false;
                this.RegGrid.Columns[2].Visible = false;
                //this.RegGrid.Columns[11].Visible = false;
            }
            else
            {
                RegGrid.DataSource = null;
                RegGrid.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AllOfferCapacity" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        RegGrid.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        gerreg();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }

    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerreg();
    }
}