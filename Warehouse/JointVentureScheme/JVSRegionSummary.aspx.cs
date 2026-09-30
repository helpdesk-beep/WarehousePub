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
using System.IO;

public partial class JointVentureScheme_JVSRegionSummary : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gerreg();
        }
    }

    public void gerreg()
    {
       string regionid = Session["UserId"].ToString();
        try
        {
            //DistID = Session["UserId"].ToString();
            string qry = "";
            qry = "select (Select District_Name from tbl_MetaData_DISTRICT d where d.District_Id=WGO.DistrictID)as District,Count(Godown_ID) as NoOfOfferedGodown,convert(Decimal(18,2), SUM(G_OfferCapacity)) as OfferedCapacity,isnull(g.FIT,0) as FIT,isnull(g.FITCapacity,0) as FITCapacity,isnull(g.UNFIT,0) as UNFIT ,isnull(g.UNFITCapacity,0) as UNFITCapacity,isnull(g.Agreement_Capacity,0) as Agreement_Capacity from tbl_Warehouse_Godown_Offer as WGO left join (select (Select Count(Fit_Unfit) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='FIT') as FIT,(Select SUM(Vacant_Capacity) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='FIT') as FITCapacity,(Select Count(Fit_Unfit) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='UNFIT') as UNFIT,Isnull((Select SUM(G_OfferCapacity) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='UNFIT'),0) as UNFITCapacity,Isnull((Select Sum(Agree_Capacity) from tbl_Godown_Agreement GA where GA.DistrictId=g.DistrictId),0) as Agreement_Capacity,g.DistrictID from tbl_Godown_Inspection g group by g.DistrictID ) as g on WGO.DistrictId=g.DistrictId where WGO.DistrictId in (select MDDIS.District_Id from tbl_MetaData_DISTRICT as MDDIS where Region_ID='" + regionid + "' )  group by WGO.DistrictId,g.DistrictId,g.FIT,g.FITCapacity,g.UNFIT,g.UNFITCapacity,g.Agreement_Capacity order by District";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                AgreeGrid.DataSource = ds;
                AgreeGrid.DataBind();
                DataTable dt = ds.Tables[0];

                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("FITCapacity"));
                AgreeGrid.FooterRow.Cells[1].Text = "Total";
                AgreeGrid.FooterRow.Cells[5].Text = total.ToString("N2");

                decimal total6 = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfOfferedGodown"));
                AgreeGrid.FooterRow.Cells[2].Text = total6.ToString("N2");

                decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("OfferedCapacity"));
                AgreeGrid.FooterRow.Cells[3].Text = total5.ToString("N2");

                decimal total4 = dt.AsEnumerable().Sum(row => row.Field<int>("FIT"));
                AgreeGrid.FooterRow.Cells[4].Text = total4.ToString("N2");

                decimal total3 = dt.AsEnumerable().Sum(row => row.Field<int>("UNFIT"));
                AgreeGrid.FooterRow.Cells[6].Text = total3.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("UNFITCapacity"));
                AgreeGrid.FooterRow.Cells[7].Text = total1.ToString("N2");

                decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Agreement_Capacity"));
                AgreeGrid.FooterRow.Cells[8].Text = total2.ToString("N2");

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
        AgreeGrid.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
}
