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

public partial class JointVentureScheme_Agreemented_Godown_Report : System.Web.UI.Page
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
        try
        {
            //DistID = Session["UserId"].ToString();
            string qry = "";
            qry = "select (Select District_Name from tbl_MetaData_DISTRICT d where d.District_Id=g.DistrictID)as District,(Select DepotName from tbl_MetaData_DEPOT b where b.BranchId=g.BranchId)as Branch,(select Warehouse_Name from tbl_WarehouseRegistration w where w.Registration_Id=g.Registration_Id)Warehouse_Name,g.Registration_Id,g.Godown_No,Convert(Varchar,g.Agree_Sign_Date,103) as Issue_Date, Convert(Varchar,g.Agree_End_Date,103) as Expiry_Date,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer I where I.Godown_ID=g.GodownId ) G_OfferCapacity, (select Insp_Capacity from tbl_Godown_Inspection I where I.GodownId=g.GodownId ) Insp_Capacity,Agree_Capacity  from tbl_Godown_Agreement g";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                AgreeGrid.DataSource = ds;
                AgreeGrid.DataBind();

                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("G_OfferCapacity"));
                AgreeGrid.FooterRow.Cells[1].Text = "Total";
                AgreeGrid.FooterRow.Cells[8].Text = total.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Insp_Capacity"));
                AgreeGrid.FooterRow.Cells[9].Text = total1.ToString("N2");

                decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Agree_Capacity"));
                AgreeGrid.FooterRow.Cells[10].Text = total2.ToString("N2");

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
