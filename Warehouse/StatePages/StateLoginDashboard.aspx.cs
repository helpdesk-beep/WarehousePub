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

public partial class StatePages_StateLoginDashboard : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            if (!IsPostBack)
            {
                getchart();
                getPaddychart();
                filldata();
                fillPaddygridedata();
                fillDalhantilhangridedata();
                fillCoarsegraingridedata();
                fillWheatgridedata();

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }

    }
    public void getchart()
    {
        DataTable dt = new DataTable();
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select dm.Regionnm,convert(decimal(18,0),ISNULL(sum(VacantCapacity),0)/10) as VacantCapacity from View_GodownVacantCPT_2018 as VC inner join tbl_MetaData_DISTRICT as DM on DM.District_Id=VC.DistrictId group by Regionnm,Region_ID order by Region_ID asc", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        foreach (DataRow dr in dt.Rows)
        {
            countrychart.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = dr["Regionnm"].ToString(),
                Data = Convert.ToDecimal(dr["VacantCapacity"]),
            });
        }
    }
    public void getPaddychart()
    {
        DataTable dt = new DataTable();
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select 'CAP' as Hired_Type,ISNULL(SUM(RecQty),0) as RecQty from tbl_MetaData_GODOWN_2018 as MG left join (select GodownID,convert(decimal(18,0),SUM(Total_Qty_Received)/10) as RecQty from tbl_storage_Depositor_WHR_Relation as WHR  where Commodity_Id='13' and CropYear='2018-19' and Arrival_Source='01' group by GodownID) as WHR on MG.Godown_ID=whr.GodownID   where Storage_Type in ('Permanent(CAP)','Temporary(CAP)')  and Hired_Type not in ('WDRA','Joint Venture(JV)') union    select Hired_Type,ISNULL(SUM(RecQty),0) as RecQty from tbl_MetaData_GODOWN_2018 as MG left join (select GodownID,SUM(Total_Qty_Received)/10 as RecQty from tbl_storage_Depositor_WHR_Relation as WHR  where Commodity_Id='13' and CropYear='2018-19' and Arrival_Source='01' group by GodownID) as WHR on MG.Godown_ID=whr.GodownID  where Storage_Type not in ('Permanent(CAP)','Temporary(CAP)') and RecQty>0 group by Hired_Type", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        foreach (DataRow dr in dt.Rows)
        {
            PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
            {
                Category = dr["Hired_Type"].ToString(),
                Data = Convert.ToDecimal(dr["RecQty"]),
            });
        }
    }

    private void filldata()
    {
        string qry = "";
        decimal dcm = 0;
        qry = "select (SELECT  convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight]/10),0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('8','11')  and srd.IssueID='NA' and sdw.CropYear='2018-19')as 'TotalWHRQty', convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty' FROM MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as Prc where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and Prc.Commodity_Id in ('8','11')";
        SqlDataAdapter da = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Clbldf.Text = dt.Rows[0]["TotalAcceptQty"].ToString();
            clblwhr.Text = dt.Rows[0]["TotalWHRQty"].ToString();
            dcm = Decimal.Round((Convert.ToDecimal(clblwhr.Text) * 100 / Convert.ToDecimal(Clbldf.Text)), 2);
            clblper.Text = Convert.ToString(dcm);
        }

        qry = "select (SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join  dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('123','31','92','65','27')  and srd.IssueID='NA' and sdw.CropYear='2018-19')as 'TotalWHRQty',  convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty' FROM MPSCSC.dbo.Acceptance_Note_Dalhan2018 as Prc where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and Prc.Commodity_Id  in ('123','31','92','65','27')";
        SqlDataAdapter da1 = new SqlDataAdapter(qry, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
            DTlbldf.Text = dt1.Rows[0]["TotalAcceptQty"].ToString();
            DTlblwhr.Text = dt1.Rows[0]["TotalWHRQty"].ToString();
            dcm = Decimal.Round((Convert.ToDecimal(DTlblwhr.Text) * 100 / Convert.ToDecimal(DTlbldf.Text)), 2);
            DTlblper.Text = Convert.ToString(dcm);
        }

        qry = "select (SELECT  convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight]/10),0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('13','14')  and srd.IssueID='NA' and sdw.CropYear='2018-19')as 'TotalWHRQty', convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty' FROM MPSCSC.dbo.Acceptance_Note_Kharif2018 as Prc where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and Prc.Commodity_Id in ('13','14')";
        SqlDataAdapter da2 = new SqlDataAdapter(qry, con);
        DataTable dt2 = new DataTable();
        da2.Fill(dt2);
        if (dt1.Rows.Count > 0)
        {
            Plbldf.Text = dt2.Rows[0]["TotalAcceptQty"].ToString();
            PlblWHR.Text = dt2.Rows[0]["TotalWHRQty"].ToString();
            dcm = Decimal.Round((Convert.ToDecimal(PlblWHR.Text) * 100 / Convert.ToDecimal(Plbldf.Text)), 2);
            PlblPer.Text = Convert.ToString(dcm);
        }

        qry = "SELECT convert(decimal(18,0),ISNULL(sum([Accept_Qty])/10,0))  as AcceptQty,(SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0)) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('22') and srd.IssueID='NA' and sdw.CropYear='2018-19')as 'TotalQty' FROM mpscsc.dbo.Acceptance_Note_Wheat2018 as Prc  inner join MPSCSC.dbo.SCSC_Procurement_Wheat2018 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No inner join tbl_MetaData_DISTRICT as md on '23'+sp.Distt_ID=md.District_Id inner join dbo.tbl_MetaData_GODOWN as mg on sp.Recd_Godown=mg.Godown_ID where Prc.WHR_Request is not null and  sp.Crop_Year='2018-2019' and Prc.CommodityId in ('22') and mg.Remarks='Y'";
        SqlDataAdapter da3 = new SqlDataAdapter(qry, con);
        DataTable dt3 = new DataTable();
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            wlbldf.Text = dt3.Rows[0]["AcceptQty"].ToString();
            wlblwhr.Text = dt3.Rows[0]["TotalQty"].ToString();
            dcm = Decimal.Round(Convert.ToDecimal(wlbldf.Text) * 100 / Convert.ToDecimal(wlblwhr.Text), 2);
            wlblper.Text = Convert.ToString(dcm);
        }
    }
    private void fillPaddygridedata()
    {
        string qry = "";
        qry = "select MD.District_Id,District_Name,ISNULL(WhrRequest,0) as 'TotalDF',isnull(TotalAcceptQty,0) as 'TotalAcceptQty', ISNULL(totalwhr,0) as 'totalwhr',ISNULL(TotalWHRQty,0) as 'TotalWHRQty' from tbl_MetaData_DISTRICT as MD left join   (select '23'+Prc.Distt_ID as  Distt_ID,isnull(count(distinct DepositerNo),0) as 'WhrRequest',  convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty',  (SELECT  count(distinct srd.WHR_Id) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join  dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID  where srd.Commodity_Id in ('13','14') and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID )as 'totalwhr',  (SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join   dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID  where srd.Commodity_Id in ('13','14')  and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID)as 'TotalWHRQty'   FROM MPSCSC.dbo.Acceptance_Note_Kharif2018 as Prc  where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and  Prc.Commodity_Id  in ('13','14') group by Prc.Distt_ID ) as RecQty on RecQty.Distt_ID=MD.District_Id order by District_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_Paddy.DataSource = ds;
            GD_Paddy.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<int>("TotalDF"));
            GD_Paddy.FooterRow.Cells[2].Text = "Total";
            GD_Paddy.FooterRow.Cells[3].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptQty"));
            GD_Paddy.FooterRow.Cells[4].Text = total1.ToString("N2");

            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<int>("totalwhr"));
            GD_Paddy.FooterRow.Cells[5].Text = total2.ToString("N2");

            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWHRQty"));
            GD_Paddy.FooterRow.Cells[6].Text = total3.ToString("N2");
        }
    }
    private void fillDalhantilhangridedata()
    {
        string qry = "";
        qry = "select MD.District_Id,District_Name,ISNULL(WhrRequest,0) as 'TotalDF',isnull(TotalAcceptQty,0) as 'TotalAcceptQty', ISNULL(totalwhr,0) as 'totalwhr',ISNULL(TotalWHRQty,0) as 'TotalWHRQty' from tbl_MetaData_DISTRICT as MD left join (select '23'+Prc.Distt_ID as  Distt_ID,isnull(count(distinct DepositerNo),0) as 'WhrRequest',convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty',(SELECT  count(distinct srd.WHR_Id) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('123','31','92','65','27') and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID )as 'totalwhr', (SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join  dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('123','31','92','65','27')  and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID)as 'TotalWHRQty' FROM MPSCSC.dbo.Acceptance_Note_Dalhan2018 as Prc where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and Prc.Commodity_Id  in ('123','31','92','65','27') group by Prc.Distt_ID ) as RecQty on RecQty.Distt_ID=MD.District_Id order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_DalhanTilhan.DataSource = ds;
            GD_DalhanTilhan.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<int>("TotalDF"));
            GD_DalhanTilhan.FooterRow.Cells[2].Text = "Total";
            GD_DalhanTilhan.FooterRow.Cells[3].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptQty"));
            GD_DalhanTilhan.FooterRow.Cells[4].Text = total1.ToString("N2");

            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<int>("totalwhr"));
            GD_DalhanTilhan.FooterRow.Cells[5].Text = total2.ToString("N2");

            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWHRQty"));
            GD_DalhanTilhan.FooterRow.Cells[6].Text = total3.ToString("N2");
        }
    }
    private void fillCoarsegraingridedata()
    {
        string qry = "";
        qry = "select MD.District_Id,District_Name,ISNULL(WhrRequest,0) as 'TotalDF',isnull(TotalAcceptQty,0) as 'TotalAcceptQty', ISNULL(totalwhr,0) as 'totalwhr',ISNULL(TotalWHRQty,0) as 'TotalWHRQty' from tbl_MetaData_DISTRICT as MD left join   (select '23'+Prc.Distt_ID as  Distt_ID,isnull(count(distinct DepositerNo),0) as 'WhrRequest',  convert(decimal(18,0),ISNULL(sum(NetWeight)/10,0))  as 'TotalAcceptQty',  (SELECT  count(distinct srd.WHR_Id) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join  dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID  where srd.Commodity_Id in ('8','11') and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID )as 'totalwhr',  (SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0))  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join   dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID  where srd.Commodity_Id in ('8','11')  and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id='23'+Prc.Distt_ID)as 'TotalWHRQty'   FROM MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as Prc  where Prc.DepositerNo is not null and  Prc.Crop_Year='2018-2019' and  Prc.Commodity_Id  in ('8','11') group by Prc.Distt_ID ) as RecQty on RecQty.Distt_ID=MD.District_Id order by District_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_Corasegrain.DataSource = ds;
            GD_Corasegrain.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<int>("TotalDF"));
            GD_Corasegrain.FooterRow.Cells[2].Text = "Total";
            GD_Corasegrain.FooterRow.Cells[3].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptQty"));
            GD_Corasegrain.FooterRow.Cells[4].Text = total1.ToString("N2");

            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<int>("totalwhr"));
            GD_Corasegrain.FooterRow.Cells[5].Text = total2.ToString("N2");

            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWHRQty"));
            GD_Corasegrain.FooterRow.Cells[6].Text = total3.ToString("N2");
        }
    }
    private void fillWheatgridedata()
    {
        string qry = "";
        qry = "SELECT md.District_Id,md.District_Name,convert(decimal(18,0),ISNULL(sum([Accept_Qty])/10,0))  as TotalAcceptQty,count(distinct [WHR_Request]) as TotalDF, (SELECT convert(decimal(18,0),isnull(SUM([Qty_Rvd_Weight])/10,0)) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID  where srd.Commodity_Id in ('22') and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id=md.District_Id)as 'TotalWHRQty',(SELECT count(distinct srd.WHR_Id) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('22') and srd.IssueID='NA' and sdw.CropYear='2018-19' and sdw.District_Id=md.District_Id) as 'totalwhr' FROM mpscsc.dbo.Acceptance_Note_Wheat2018 as Prc  inner join MPSCSC.dbo.SCSC_Procurement_Wheat2018 as sp on Prc.IssueID=sp.Receipt_Id and Prc.Acceptance_No=sp.Acceptance_No inner join tbl_MetaData_DISTRICT as md on '23'+sp.Distt_ID=md.District_Id inner join dbo.tbl_MetaData_GODOWN as mg on sp.Recd_Godown=mg.Godown_ID where Prc.WHR_Request is not null and  sp.Crop_Year='2018-2019' and Prc.CommodityId in ('22') and mg.Remarks='Y' group by md.District_Id,md.District_Name order by md.District_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_wheatpopup.DataSource = ds;
            GD_wheatpopup.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<int>("TotalDF"));
            GD_wheatpopup.FooterRow.Cells[2].Text = "Total";
            GD_wheatpopup.FooterRow.Cells[3].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalAcceptQty"));
            GD_wheatpopup.FooterRow.Cells[4].Text = total1.ToString("N2");

            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<int>("totalwhr"));
            GD_wheatpopup.FooterRow.Cells[5].Text = total2.ToString("N2");

            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("TotalWHRQty"));
            GD_wheatpopup.FooterRow.Cells[6].Text = total3.ToString("N2");
        }
    }
}
