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

public partial class JointVentureScheme_Rpt_Region_Offer_For_Rabi_2025_26 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        try
        {
            if (!IsPostBack)
            {
                gerreg();
                GPhase1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }

    public void gerreg()
    {
        try
        {
            string qry = "";
            //qry = "select MDDIS.District_Name,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,0),RegCapacity)) as RegCapacity,isnull(sum(convert(Decimal(18,0),Offer_Capacity)),0) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId left join  (select SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt) as  OfferAmt,Registration_Id from tbl_Warehouse_Capacity_Offer_2021  group by Registration_Id) as COFF on WAR.Registration_Id=COFF.Registration_Id where RegCapacity !='0.00' and WAR.Registration_Id in (select distinct REGISTRATIONID from tbl_Payment_Status)  group by District_Name  order by District_Name";
            //qry = "select MDDIS.District_Name,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,0),RegCapacity)) as RegCapacity,isnull(sum(convert(Decimal(18,0),Offer_Capacity)),0) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId left join  (select SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt) as  OfferAmt,Registration_Id from tbl_Warehouse_Capacity_Offer_Kharif2022  group by Registration_Id) as COFF on WAR.Registration_Id=COFF.Registration_Id where RegCapacity !='0.00' and WAR.Registration_Id in (select distinct REGISTRATIONID from tbl_Payment_Status)  group by District_Name  order by District_Name";
            SqlCommand cmd = new SqlCommand("Rpt_Region_Offer_For_Rabi2025_26", con);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
            //if (ddlChoise.SelectedValue == "--Select--")
            //{
            //    cmd.Parameters.AddWithValue("@Choise", "-1");

            //}            
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Choise", ddlChoise.SelectedValue);
            //}
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridDist.DataSource = dt;
                GridDist.DataBind();
               
               

                GridDist.FooterRow.Style.Add("text-align", "right");
                GridDist.FooterRow.Cells[3].Text = "Total";
                GridDist.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity")).ToString();
                //GridDist.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Choise_Filling")).ToString();
            }
            else
            {
                GridDist.DataSource = null;
                GridDist.DataBind();
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
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
            GridDist.Attributes["style"] = "border-collapse:separate";
            toexportDist.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
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

    protected void ddlChoise_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        gerreg();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("JVSRegionReport.aspx");
    }
}
