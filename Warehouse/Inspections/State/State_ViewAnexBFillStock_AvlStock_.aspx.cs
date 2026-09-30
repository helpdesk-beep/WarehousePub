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

public partial class Inspections_State_State_ViewAnexBFillStock_AvlStock_ : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // lbl_user.Text = Session["UserName"].ToString();
            GetDistrict();
        }
    }
    public void GetDistrict()
    {
        string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.DataSource = "";
            ddl_dist.DataBind();
        }
    }
    public void fillAvlStockgdwnWise()
    {
        string strsql = "select SDATE.Branch_ID,(select Officer_Name from tbl_metadata_Inspection_officer as INSP where INSP.PF_ID=SDATE.PF_ID) as OfficeName,(select District_Name from tbl_metadata_district as MD where MD.District_id=SDATE.District_ID) as District_Name, (select Depotname from tbl_metadata_depot as MDDE where MDDE.BranchID=SDATE.Branch_ID) as Depotname,isnull(GDWN.NoOfGdwn,0) NoOfGdwn,isnull(PVGDWN.NoOfPVGdwn,0) as NoOfPVGdwn,isnull(Total_Bags_AsPerOnline,0) as Total_Bags_AsPerOnline,isnull(Total_Bags_AsPerPV,0) as Total_Bags_AsPerPV,Total_Bags_AsPerOnline-Total_Bags_AsPerPV as DiffBags from tbl_Inpection_Scheduled_Date as SDATE left join (select BranchID,count(Godown_ID) NoOfGdwn from tbl_metadata_godown_2018 group by BranchID)  as GDWN on GDWN.BranchID=SDATE.Branch_ID left join (select BranchID,Count(Distinct Godown_ID) as NoOfPVGdwn,sum(Total_Bags_AsPerOnline)as Total_Bags_AsPerOnline ,sum(Total_Bags_AsPerPV)as Total_Bags_AsPerPV from tbl_PV_Metadata_Godown group by BranchID) as PVGDWN on PVGDWN.BranchID=SDATE.Branch_ID  where  SDATE.District_ID='" + ddl_dist.SelectedValue.ToString() + "' group by PF_ID,District_ID,SDATE.Branch_ID,GDWN.NoOfGdwn,PVGDWN.NoOfPVGdwn,Total_Bags_AsPerOnline,Total_Bags_AsPerPV order by OfficeName,District_Name ";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ds.Tables[0].Columns.Add("AvlBagsCur");
            ds.Tables[0].Columns.Add("AvlBagsCurDiff");
            for (int aa = 0; aa < ds.Tables[0].Rows.Count; aa++)
            {
                string BID = ds.Tables[0].Rows[aa]["Branch_ID"].ToString();
                int sumgdwntotal = 0;
                string strsq2 = "select Godown_ID,convert(varchar(10),Inspection_Date,101) as Inspection_Date from tbl_PV_Metadata_Godown where BranchID='" + BID + "' order by Godown_ID";
                SqlCommand cmd2 = new SqlCommand(strsq2, conStr);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    con_WLC.Open();
                    for (int intCount = 0; intCount < ds2.Tables[0].Rows.Count; intCount++)
                    {
                        string GDID = ds2.Tables[0].Rows[intCount]["Godown_ID"].ToString();
                        string INSPDATE = ds2.Tables[0].Rows[intCount]["Inspection_Date"].ToString();
                        {
                            if (GDID != "" && GDID != null)
                            {
                                string strsql22 = ("SELECT isnull(sum(RecBags),0)-isnull(sum(DelBags),0) as AvlBagsCurent FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Bags),0) as RecBags from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<='" + INSPDATE + "' and WHR.GodownID='" + GDID + "' group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id left join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,isnull(SUM(DSD.No_Of_Bags),0) as DelBags from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<='" + INSPDATE + "' and DSD.Godown_ID='" + GDID + "' group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDG.Godown_ID='" + GDID + "' and MDS.Stack_Killed='N'  group by MDG.Godown_ID");
                                SqlCommand cmdd22 = new SqlCommand(strsql22, con_WLC);
                                SqlDataAdapter da22 = new SqlDataAdapter(cmdd22);
                                DataSet ds22 = new DataSet();
                                da22.Fill(ds22);
                                if (ds22.Tables[0].Rows.Count > 0)
                                {
                                    sumgdwntotal = sumgdwntotal + Convert.ToInt32(ds22.Tables[0].Rows[0]["AvlBagsCurent"].ToString());
                                }
                            }
                        }
                    }
                    con_WLC.Close();
                }
                ds.Tables[0].Rows[aa]["AvlBagsCur"] = sumgdwntotal;
                ds.Tables[0].Rows[aa]["AvlBagsCurDiff"] = Convert.ToInt32(ds.Tables[0].Rows[aa]["AvlBagsCur"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[aa]["Total_Bags_AsPerPV"].ToString());
            }
            gridbranch.DataSource = ds;
            gridbranch.DataBind();
        }
    }

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillAvlStockgdwnWise();
    }
    protected void gridbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gridbranch.SelectedRow;
        lblbName.Text = gvr.Cells[3].Text;
        string BID = gvr.Cells[0].Text;
        string strsql = "";
        strsql = "select Depotname,Godown_Name,PVGDWN.Godown_ID,PVGDWN.Hired_type,PVGDWN.Storage_Type,convert(Varchar(10),Inspection_Date,103) as InspDate,Total_Bags_AsPerOnline,Total_Bags_AsPerPV,Total_Bags_AsPerOnline-Total_Bags_AsPerPV as DiffBags from tbl_PV_Metadata_Godown as PVGDWN inner join tbl_MetaData_GODOWN_2018 as MG on MG.Godown_ID=PVGDWN.godown_ID inner join tbl_metadata_depot as MD on MD.BranchID=PVGDWN.branchID inner join tbl_metadata_district as MDDIS on MDDIS.District_ID=PVGDWN.DistrictID inner join tbl_metadata_Inspection_officer as INSP on INSP.PF_ID=Insp_Officer_ID where PVGDWN.BranchID='" + BID + "' order by Godown_Name";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string strsq2 = "select Godown_ID,convert(varchar(10),Inspection_Date,101) as Inspection_Date from tbl_PV_Metadata_Godown where BranchID='" + BID + "'";
            SqlCommand cmd2 = new SqlCommand(strsq2, conStr);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Add("AvlBagsCur");
                ds.Tables[0].Columns.Add("AvlBagsCurDiff");
                con_WLC.Open();
                for (int intCount = 0; intCount < ds2.Tables[0].Rows.Count; intCount++)
                {
                    string GDID = ds2.Tables[0].Rows[intCount]["Godown_ID"].ToString();
                    string INSPDATE = ds2.Tables[0].Rows[intCount]["Inspection_Date"].ToString();
                    {
                        string qry22 = ("SELECT isnull(sum(RecBags),0)-isnull(sum(DelBags),0) as AvlBagsCurent FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Bags),0) as RecBags from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<='" + INSPDATE + "' and WHR.GodownID='" + GDID + "' group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id left join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,isnull(SUM(DSD.No_Of_Bags),0) as DelBags from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<='" + INSPDATE + "' and DSD.Godown_ID='" + GDID + "' group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDG.Godown_ID='" + GDID + "' and MDS.Stack_Killed='N'  group by MDG.Godown_ID");
                        SqlCommand cmdd22 = new SqlCommand(qry22, con_WLC);
                        SqlDataAdapter da22 = new SqlDataAdapter(cmdd22);
                        DataSet ds22 = new DataSet();
                        da22.Fill(ds22);
                        if (ds22.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[intCount]["AvlBagsCur"] = ds22.Tables[0].Rows[0]["AvlBagsCurent"].ToString();
                            ds.Tables[0].Rows[intCount]["AvlBagsCurDiff"] = Convert.ToInt32(ds.Tables[0].Rows[intCount]["AvlBagsCur"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[intCount]["Total_Bags_AsPerPV"].ToString());
                        }
                        else
                        {
                            ds.Tables[0].Rows[intCount]["AvlBagsCur"] = 0;
                            ds.Tables[0].Rows[intCount]["AvlBagsCurDiff"] = 0 - Convert.ToInt32(ds.Tables[0].Rows[intCount]["Total_Bags_AsPerPV"].ToString());
                        }
                    }
                }
                con_WLC.Close();
                gvCustomers.DataSource = ds;
                gvCustomers.DataBind();

                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Bags_AsPerOnline"));
                gvCustomers.FooterRow.Cells[1].Text = "Total";
                gvCustomers.FooterRow.Cells[6].Text = total.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Bags_AsPerPV"));
                gvCustomers.FooterRow.Cells[7].Text = total1.ToString("N2");

                decimal total2 = dt.AsEnumerable().Sum(row => row.Field<Int32>("DiffBags"));
                gvCustomers.FooterRow.Cells[8].Text = total2.ToString("N2");

                //decimal total3 = dt.AsEnumerable().Sum(row => row.Field<Int32>("AvlBagsCur"));
                //gvCustomers.FooterRow.Cells[9].Text = total3.ToString("N2");

                //decimal total4 = dt.AsEnumerable().Sum(row => row.Field<Int32>("AvlBagsCurDiff"));
                //gvCustomers.FooterRow.Cells[10].Text = total4.ToString("N2");
            }
            else
            {
                gvCustomers.DataSource = null;
                gvCustomers.DataBind();
            }

        }
        else
        {
            gvCustomers.DataSource = null;
            gvCustomers.DataBind();
        }
    }
}