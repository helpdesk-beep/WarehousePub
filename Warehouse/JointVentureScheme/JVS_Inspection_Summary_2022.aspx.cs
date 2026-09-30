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
public partial class JointVentureScheme_JVS_Inspection_Summary_2022 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    private decimal opcloavg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void fillgrid()
    {

        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("[dbo].[Sp_JVS_Inspection_Summary_2022]", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            AgreeGrid.DataSource = dt;
                            AgreeGrid.DataBind();
                            // AgreeGrid.Caption = @"<b style=""font-weight: bold;"">M.P. Warehousing & Logistics Corporarion " + "</br> " + "District Wise Pending Payment Status from MPSCSC(From August)" + "</br> " + "From Date" + " " + txtdatefrom.Text + " " + "To date" + " " + txtdateto.Text + "</b> ";

                            AgreeGrid.FooterRow.Style.Add("text-align", "Center");
                            AgreeGrid.FooterRow.Cells[1].Text = "Total";
                            AgreeGrid.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Offer_Godown")).ToString();
                            AgreeGrid.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Insp_Done")).ToString();
                            AgreeGrid.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pending_for_Inspection")).ToString();
                            AgreeGrid.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Pre_Printed_Download")).ToString();
                            opcloavg = (Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<int>("Insp_Done")).ToString()) * 100) / Convert.ToDecimal(dt.AsEnumerable().Sum(row => row.Field<int>("Total_Offer_Godown")).ToString());
                            AgreeGrid.FooterRow.Cells[6].Text = Math.Round(opcloavg, 2).ToString();
                            //AgreeGrid.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Percentage")).ToString();

                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            AgreeGrid.DataSource = dt;
                            AgreeGrid.DataBind();
                        }
                    }
                }
            }
        }
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
