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
using System.IO;
using System.Data.SqlClient;

public partial class JointVentureScheme_JVSCapacityComparisionReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
  
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // gerreg();
        }
    }

    public void gerreg()
    {
        try
        {
            AgreeGrid.DataSource = "";
            AgreeGrid.DataBind();
            //DistID = Session["UserId"].ToString();
            string qry = "";
            qry = "select Regionnm,District_Name as District,MDDIS.District_Id,isnull(NoOfGdwn1,0) NoOfGdwn1 ,isnull(G_OfferCapacity1,0) as G_OfferCapacity1 from tbl_MetaData_DISTRICT as MDDIS left join (select OFR.DistrictId,count(OFR.Godown_ID) as NoOfGdwn1 ,convert(Decimal(18,2),SUM(OFR.G_OfferCapacity)) as G_OfferCapacity1  from tbl_Warehouse_Godown_Offer_2019 as OFR where G_OfferCapacity >= '"+ ddlcpttype.SelectedValue.ToString() +"' group by OFR.DistrictId) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id order by Regionnm,District_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                AgreeGrid.DataSource = ds;
                AgreeGrid.DataBind();
                DataTable dt = ds.Tables[0];

                decimal total6 = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfGdwn1"));
                AgreeGrid.FooterRow.Cells[3].Text = total6.ToString("N2");

                decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("G_OfferCapacity1"));
                AgreeGrid.FooterRow.Cells[4].Text = total5.ToString("N2");
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void ddlcpttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerreg();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void Button1_Click(object sender, EventArgs e)
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
        AgreeGrid.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}
