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

public partial class JointVentureScheme_JVS2021_22_OfferSummary : System.Web.UI.Page
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
            GetKharif2019_20();
        }
    }
    public void gerreg(string qry)
    {
        try
        {
            AgreeGrid.DataSource = "";
            AgreeGrid.DataBind();
            //DistID = Session["UserId"].ToString();
            // string qry = "";
            // qry = "select *,(NoOfOfferedGodown-(FIT+UNFIT)) as RemForInsp from (select (Select District_Name from tbl_MetaData_DISTRICT d where d.District_Id=WGO.DistrictID)as District,Count(Godown_ID) as NoOfOfferedGodown,convert(Decimal(18,2), SUM(G_OfferCapacity)) as OfferedCapacity,isnull(g.FIT,0) as FIT,isnull(g.FITCapacity,0) as FITCapacity,isnull(g.UNFIT,0) as UNFIT ,isnull(g.UNFITCapacity,0) as UNFITCapacity,isnull(g.Agreement_Capacity,0) as Agreement_Capacity from tbl_Warehouse_Godown_Offer as WGO left join (select (Select Count(Fit_Unfit) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='FIT') as FIT,(Select SUM(Vacant_Capacity) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='FIT') as FITCapacity,(Select Count(Fit_Unfit) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='UNFIT') as UNFIT,Isnull((Select SUM(G_OfferCapacity) from tbl_Godown_Inspection GI where GI.DistrictId=g.DistrictId and Fit_Unfit='UNFIT'),0) as UNFITCapacity,Isnull((Select Sum(Agree_Capacity) from tbl_Godown_Agreement GA where GA.DistrictId=g.DistrictId),0) as Agreement_Capacity,g.DistrictID from tbl_Godown_Inspection g group by g.DistrictID ) as g on WGO.DistrictId=g.DistrictId group by WGO.DistrictId,g.DistrictId,g.FIT,g.FITCapacity,g.UNFIT,g.UNFITCapacity,g.Agreement_Capacity ) as MegAll order by District";
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
                AgreeGrid.FooterRow.Cells[6].Text = total.ToString("N2");

                decimal total6 = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfOfferedGodown"));
                AgreeGrid.FooterRow.Cells[3].Text = total6.ToString("N2");

                decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("OfferedCapacity"));
                AgreeGrid.FooterRow.Cells[4].Text = total5.ToString("N2");

                decimal total4 = dt.AsEnumerable().Sum(row => row.Field<int>("FIT"));
                AgreeGrid.FooterRow.Cells[5].Text = total4.ToString("N2");

                decimal total3 = dt.AsEnumerable().Sum(row => row.Field<int>("UNFIT"));
                AgreeGrid.FooterRow.Cells[7].Text = total3.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("UNFITCapacity"));
                AgreeGrid.FooterRow.Cells[8].Text = total1.ToString("N2");

                decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Agreement_Capacity"));
                AgreeGrid.FooterRow.Cells[9].Text = total2.ToString("N2");

                //decimal total7 = dt.AsEnumerable().Sum(row => row.Field<int>("RemForInsp"));
                //AgreeGrid.FooterRow.Cells[9].Text = total7.ToString("N2");

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
    public void GetKharif2019_20()
    {
        string qry = "";
        //qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2),SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt  from tbl_Warehouse_Godown_Offer_2020 as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id and OFR.OfferSeason='JVS2020_21' left join  (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Fit_Unfit='FIT' and Insp_Offer_Scheme in ('69','74') group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt  from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Fit_Unfit='UNFIT' and Insp_Offer_Scheme in ('69','74') group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join  (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Crop_Year='2020-21' group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID   group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name";
        //qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2),SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt  from tbl_Warehouse_Godown_Offer_2021 as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id and OFR.OfferSeason='JVS2021_22' left join  (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Fit_Unfit='FIT' and Insp_Offer_Scheme in ('69','74') group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt  from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Fit_Unfit='UNFIT' and Insp_Offer_Scheme in ('69','74') group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join  (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>CONVERT(varchar(10),'03/03/2020',101) and Crop_Year='2020-21' group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID   group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name";
        qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2),SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt  from tbl_Warehouse_Godown_Offer_2021 as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id and OFR.OfferSeason='JVS2021_22' left join  (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'02/20/2021',101) and Fit_Unfit='FIT' and Insp_Offer_Scheme in ('78','83') group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt  from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'02/20/2021',101) and Fit_Unfit='UNFIT' and Insp_Offer_Scheme in ('78','83') group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join  (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>CONVERT(varchar(10),'02/20/2021',101) and Crop_Year='2021-22' group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID   group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name";


        gerreg(qry);
    }

    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qry = "";
        //if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        //{
        //    qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity ,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity  from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2), SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt from tbl_Warehouse_Godown_Offer as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id left join (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate>=CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='FIT' group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate>=CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>=CONVERT(varchar(10),'10/19/2018',101) group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID where OFR.Phase >= 9  group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name";
        //}
        //else if (ddl_session.SelectedValue.ToString() == "Rabi1819")
        //{
        //    qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity ,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity  from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2), SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt from tbl_Warehouse_Godown_Offer as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id left join (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate<CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='FIT' group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt from tbl_Godown_Inspection where CreatedDate<CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate<CONVERT(varchar(10),'10/19/2018',101) group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID where OFR.Phase < 9  group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name ";
        //}
        //else if (ddl_session.SelectedValue.ToString() == "Rabi1920")
        //{
        //    qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity ,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity  from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2), SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt  from tbl_Warehouse_Godown_Offer_2019 as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id left join  (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection  where CreatedDate>CONVERT(varchar(10),'02/17/2019',101) and Fit_Unfit='FIT' group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt  from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'02/17/2019',101) and Fit_Unfit='UNFIT'  group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join  (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>CONVERT(varchar(10),'02/17/2019',101)  group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID   group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG  on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name ";
        //}
        //else if (ddl_session.SelectedValue.ToString() == "Kharif1920")
        //{
        //    qry = "select Regionnm,District_Name as District,MDDIS.District_Id,COUNT(Godown_ID) as NoOfOfferedGodown,isnull(SUM(G_OfferCapacity),0) as OfferedCapacity,COUNT(INSPFitGdwn) as FIT,isnull(SUM(SumOfFIt),0) as FITCapacity ,COUNT(INSPUnFitGdwn) as UNFIT,isnull(SUM(SumOfUnfit),0) as UNFITCapacity,isnull(SUM(AgrCpt),0) as Agreement_Capacity  from tbl_MetaData_DISTRICT as MDDIS left join (select WREG.DistrictId,OFR.Registration_Id,OFR.Godown_ID,convert(Decimal(18,2), SUM(OFR.G_OfferCapacity)) as G_OfferCapacity,INSP.GodownId as INSPFitGdwn ,ISNULL(convert(Decimal(18,2), INSP.VCTCpt),0) as SumOfFIt,INSPUnfit.GodownId as INSPUnFitGdwn,ISNULL(convert(Decimal(18,2), INSPUnfit.VCTCpt),0) as SumOfUnfit,ISNULL(convert(Decimal(18,2), AGR.Agree_Capacity),0) as AgrCpt  from tbl_Warehouse_Godown_Offer_2019 as OFR left join tbl_WarehouseRegistration as WREG on WREG.Registration_Id=OFR.Registration_Id left join  (select Registration_Id,GodownId,isnull(SUM(Vacant_Capacity),0) as VCTCpt from tbl_Godown_Inspection  where CreatedDate>CONVERT(varchar(10),'11/28/2019',101) and Fit_Unfit='FIT' group by Registration_Id,GodownId) as INSP on INSP.GodownId=OFR.Godown_ID  left join (select Registration_Id,GodownId,isnull(SUM(G_OfferCapacity),0) as VCTCpt  from tbl_Godown_Inspection where CreatedDate>CONVERT(varchar(10),'11/28/2019',101) and Fit_Unfit='UNFIT'  group by Registration_Id,GodownId) as INSPUnfit on INSPUnfit.GodownId=OFR.Godown_ID left join  (select GodownId,SUM(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>CONVERT(varchar(10),'11/28/2019',101)  group by GodownId) as AGR on AGR.GodownId=OFR.Godown_ID   group by OFR.Godown_ID,OFR.Registration_Id,WREG.DistrictId,INSP.GodownId ,INSP.VCTCpt,INSPUnfit.GodownId,INSPUnfit.VCTCpt,AGR.Agree_Capacity) as OFRMRG  on OFRMRG.DistrictId=MDDIS.District_Id group by District_Id,District_Name,Regionnm order by Regionnm,District_Name ";
        //}

        //gerreg(qry);
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
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
}