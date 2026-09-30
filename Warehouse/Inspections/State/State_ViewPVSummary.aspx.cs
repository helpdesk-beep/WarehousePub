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

public partial class Inspections_State_State_ViewPVSummary : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
         //lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
           // lbl_user.Text = Session["UserName"].ToString();
        }
    }

    protected void rdoInspOff_CheckedChanged(object sender, EventArgs e)
    {
        string strsql = "select (select Officer_Name from tbl_metadata_Inspection_officer as INSP where INSP.PF_ID=SDATE.PF_ID) as OfficeName,(select District_Name from tbl_metadata_district as MD where MD.District_id=SDATE.District_ID) as District_Name, (select Depotname from tbl_metadata_depot as MDDE where MDDE.BranchID=SDATE.Branch_ID) as Depotname,isnull(GDWN.NoOfGdwn,0) NoOfGdwn,isnull(PVGDWN.NoOfPVGdwn,0) as NoOfPVGdwn,isnull(Total_Bags_AsPerOnline,0) as Total_Bags_AsPerOnline,isnull(Total_Bags_AsPerPV,0) as Total_Bags_AsPerPV,Total_Bags_AsPerOnline-Total_Bags_AsPerPV as DiffBags from tbl_Inpection_Scheduled_Date as SDATE left join (select BranchID,count(Godown_ID) NoOfGdwn from tbl_metadata_godown_2018 group by BranchID)  as GDWN on GDWN.BranchID=SDATE.Branch_ID left join (select BranchID,Count(Distinct Godown_ID) as NoOfPVGdwn,sum(Total_Bags_AsPerOnline)as Total_Bags_AsPerOnline ,sum(Total_Bags_AsPerPV)as Total_Bags_AsPerPV from tbl_PV_Metadata_Godown group by BranchID) as PVGDWN on PVGDWN.BranchID=SDATE.Branch_ID group by PF_ID,District_ID,SDATE.Branch_ID,GDWN.NoOfGdwn,PVGDWN.NoOfPVGdwn,Total_Bags_AsPerOnline,Total_Bags_AsPerPV order by OfficeName,District_Name ";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.Visible = false;
            gvCustomers.Visible = true;
            gvCustomers.DataSource = ds;
            gvCustomers.DataBind();

            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<Int32>("NoOfGdwn"));
            gvCustomers.FooterRow.Cells[1].Text = "Total";
            gvCustomers.FooterRow.Cells[3].Text = total.ToString("N2");

            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Int32>("NoOfPVGdwn"));
            gvCustomers.FooterRow.Cells[4].Text = total1.ToString("N2");

            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Bags_AsPerOnline"));
            gvCustomers.FooterRow.Cells[5].Text = total2.ToString("N2");

            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Bags_AsPerPV"));
            gvCustomers.FooterRow.Cells[6].Text = total3.ToString("N2");
        }
        else
        {
            gvCustomers.DataSource = null;
            gvCustomers.DataBind();
        }
    }
    protected void rdoDist_CheckedChanged(object sender, EventArgs e)
    {
        string strsql = "select (select Officer_Name from tbl_metadata_Inspection_officer as INSP where INSP.PF_ID=SDATE.PF_ID) as OfficeName,(select District_Name from tbl_metadata_district as MD where MD.District_id=SDATE.District_ID) as District_Name,count(Branch_ID) as NoOfBranch,NoOfGdwn,NoOfPVBranch,NoOfPVGdwn from tbl_Inpection_Scheduled_Date as SDATE left join (select districtID,count(Godown_ID) NoOfGdwn from tbl_metadata_godown_2018 group by DistrictID)  as GDWN on GDWN.DistrictID=SDATE.District_ID left join (select DistrictID,Count(Distinct BranchID) as NoOfPVBranch from tbl_PV_Metadata_Godown group by DistrictID) as PVBranch on PVBranch.DistrictID=SDATE.District_ID left join (select DistrictID,Count(Distinct Godown_ID) as NoOfPVGdwn from tbl_PV_Metadata_Godown group by DistrictID) as PVGDWN on  PVGDWN.DistrictID=SDATE.District_ID group by PF_ID,District_ID,GDWN.NoOfGdwn,PVGDWN.NoOfPVGdwn,NoOfPVBranch order by OfficeName,District_Name";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvCustomers.Visible = false;
            GridView1.Visible = true;
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
}