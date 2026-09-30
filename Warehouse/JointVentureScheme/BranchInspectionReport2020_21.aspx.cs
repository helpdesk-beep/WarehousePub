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

public partial class JointVentureScheme_BranchInspectionReport2020_21 : System.Web.UI.Page
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
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetExpires(DateTime.Now);
        //Response.Cache.SetNoStore();
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
            //Branch = Session["UserId"].ToString();
            string qry = "";
            if (rdoAll.Checked == true)
            {
                //if (ddl_session.SelectedValue == "Kharif1819")
                //{
                //    //qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where BranchId='" + Session["UserId"].ToString() + "'";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'10/19/2018' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
                //}
                //if (ddl_session.SelectedValue == "Rabi1819")
                //{
                //    //qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where BranchId='" + Session["UserId"].ToString() + "'";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate<'10/19/2018' and INSP.BranchId='" + Session["UserId"].ToString() + "'";

                //}
                qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark,INSP.Insp_Offer_Scheme from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer_2020 group by Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme) as ofr on ofr.Godown_Offer_Id=INSP.Godown_Offer_Id  left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'02/20/2020' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
            }
            else if (rdoFit.Checked == true)
            {
                //if (ddl_session.SelectedValue == "Kharif1819")
                //{
                //    //qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where Fit_Unfit='FIT' and BranchId='" + Session["UserId"].ToString() + "'";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'10/19/2018' and Fit_Unfit='FIT' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
                //}
                //if (ddl_session.SelectedValue == "Rabi1819")
                //{
                //   // qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where Fit_Unfit='FIT' and BranchId='" + Session["UserId"].ToString() + "'";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate<'10/19/2018' and Fit_Unfit='FIT' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
                //}
                qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark,INSP.Insp_Offer_Scheme from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer_2020 group by Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme) as ofr on ofr.Godown_Offer_Id=INSP.Godown_Offer_Id  left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'02/20/2020' and INSP.BranchId='" + Session["UserId"].ToString() + "' and INSP.AutoFit_Unfit='FIT'";
            }
            else if (rdoUnfit.Checked == true)
            {
                //if (ddl_session.SelectedValue == "Kharif1819")
                //{
                //    //qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where Fit_Unfit='UNFIT' and BranchId='" + Session["UserId"].ToString() + "' ";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'10/19/2018' and Fit_Unfit='UNFIT' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
                //}
                //if (ddl_session.SelectedValue == "Rabi1819")
                //{
                //    //qry = "select Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme,Remark from tbl_Godown_Inspection as INSP where Fit_Unfit='UNFIT' and BranchId='" + Session["UserId"].ToString() + "' ";
                //    qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer where CreatedDate>'10/19/2018' group by Registration_Id,Godown_ID,G_Scheme) as ofr on ofr.Registration_Id=INSP.Registration_Id left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate<'10/19/2018' and Fit_Unfit='UNFIT' and INSP.BranchId='" + Session["UserId"].ToString() + "'";
                //}
                qry = "select INSP.Registration_Id,Offer_Id,GodownId,Godown_No,convert(decimal(18,2),(INSP.Vacant_Capacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,Warehouse_Name, Mobile_No,''as DepotName,'' as District_Name,OfferCapacity,G_Scheme,Remark,INSP.Insp_Offer_Scheme from tbl_Godown_Inspection as INSP inner join (select Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme,convert(decimal(18,2),sum(G_OfferCapacity)) as OfferCapacity from tbl_Warehouse_Godown_Offer_2020 group by Registration_Id,Godown_ID,Godown_Offer_Id,G_Scheme) as ofr on ofr.Godown_Offer_Id=INSP.Godown_Offer_Id  left join tbl_WarehouseRegistration as reg on reg.Registration_Id=INSP.Registration_Id where  INSP.CreatedDate>'02/20/2020' and INSP.BranchId='" + Session["UserId"].ToString() + "' and INSP.AutoFit_Unfit='UNFIT'";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
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
                this.RegGrid.Columns[11].Visible = false;
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
    //protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    //{
    //    gerreg();
    //}
    //protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    gerreg();
    //}
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        gerreg();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
}
