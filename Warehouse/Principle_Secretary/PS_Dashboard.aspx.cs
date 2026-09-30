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

public partial class Principle_Secretary_PS_Dashboard : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGD1();
        }
    }
    public void fillGD1()
    {
        query = "select * from tbl_StateDashBoardData order by CropYear desc";
        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD1.DataSource = ds;
            GD1.DataBind();
            DataTable dt = ds.Tables[0];
            decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("SP_AsOnDate_01042019"));
            GD1.FooterRow.Cells[0].Text = "Total";
            GD1.FooterRow.Cells[1].Text = total.ToString("N0");
            Label1.Text = total.ToString("N0");

            GD2.DataSource = ds;
            GD2.DataBind();
            decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("R_DuringThisMonth"));
            GD2.FooterRow.Cells[0].Text = "Total";
            GD2.FooterRow.Cells[1].Text = total1.ToString("N0");
            Label4.Text = total1.ToString("N0");

            GD3.DataSource = ds;
            GD3.DataBind();
            decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("SP_EndOfPreviousMonth"));
            GD3.FooterRow.Cells[0].Text = "Total";
            GD3.FooterRow.Cells[1].Text = total2.ToString("N0");
            Label7.Text = total2.ToString("N0");

            GD4.DataSource = ds;
            GD4.DataBind();
            decimal total3 = dt.AsEnumerable().Sum(row => row.Field<decimal>("SP_AsOnDate"));
            GD4.FooterRow.Cells[0].Text = "Total";
            GD4.FooterRow.Cells[1].Text = total3.ToString("N0");
            Label10.Text = total3.ToString("N0");

            GD5.DataSource = ds;
            GD5.DataBind();
            decimal total4 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ID_PreviousMonth"));
            GD5.FooterRow.Cells[0].Text = "Total";
            GD5.FooterRow.Cells[1].Text = total4.ToString("N0");
            Label20.Text = total4.ToString("N0");

            GD6.DataSource = ds;
            GD6.DataBind();
            decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ID_ThisMonth"));
            GD6.FooterRow.Cells[0].Text = "Total";
            GD6.FooterRow.Cells[1].Text = total5.ToString("N0");
            Label16.Text = total5.ToString("N0");

            GD7.DataSource = ds;
            GD7.DataBind();
            decimal total6 = dt.AsEnumerable().Sum(row => row.Field<decimal>("R_tillFirstC_Month"));
            GD7.FooterRow.Cells[0].Text = "Total";
            GD7.FooterRow.Cells[1].Text = total6.ToString("N0");
            LinkButton2.Text = total6.ToString("N0");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        int sum = 0;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            // query = "SELECT CropYear,Convert(decimal(18,0),(isnull(sum(RecQty),0)-isnull(sum(DelQty),0))/10) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<='04/01/2019' and CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id  left join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID, (isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<='04/01/2019' group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDS.Stack_Killed='N' and MDS.Commodity_Id='22' and CropYear is not null group by CropYear";
            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty from (select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<=convert(varchar(10),'04/01/2019',101) and CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,CropYear) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<=convert(varchar(10),'04/01/2019',101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and REC.Stack_ID=DEL.Stack_ID group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set SP_AsOnDate_01042019='" + ds.Tables[0].Rows[i]["AvlQty"].ToString() + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                    else
                    {

                    }
                }
            }

            // query = "SELECT CropYear,Convert(decimal(18,0),isnull(sum(RecQty),0)/10) as RecQtyThis FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id where MDS.Stack_Killed='N' and MDS.Commodity_Id='22' and CropYear is not null group by CropYear";
            query = "select CropYear,convert(decimal(18,0),isnull(sum(Weight),0)/10) as RecQtyThis from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and  SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N')  group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["RecQtyThis"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set R_DuringThisMonth='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }

                }
            }

            //  query = "SELECT CropYear,Convert(decimal(18,0),(isnull(sum(RecQty),0)-isnull(sum(DelQty),0))/10) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id  left join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDS.Stack_Killed='N' and MDS.Commodity_Id='22' and CropYear is not null group by CropYear";
            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty from (select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by WHR.Depositor_WHR_Id,SSD.Stack_ID,CropYear) as REC  left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and REC.Stack_ID=DEL.Stack_ID group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set SP_AsOnDate='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                }
            }

            // query = "SELECT CropYear,Convert(decimal(18,0),(isnull(sum(RecQty),0)-isnull(sum(DelQty),0))/10) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<= GETDATE()- DAY(GETDATE()) and CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id  left join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<= GETDATE()- DAY(GETDATE()) group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDS.Stack_Killed='N' and MDS.Commodity_Id='22'and CropYear is not null group by CropYear ";
            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty from  (select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<= GETDATE()- DAY(GETDATE()) and  CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by WHR.Depositor_WHR_Id,SSD.Stack_ID,CropYear) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<= convert(varchar(10),GETDATE()- DAY(GETDATE()),101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and REC.Stack_ID=DEL.Stack_ID group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set SP_EndOfPreviousMonth='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                }
            }


            // query = "SELECT CropYear,Convert(decimal(18,0),isnull(sum(DelQty),0)/10) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id  RIGHT join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<= GETDATE()- DAY(GETDATE()) group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDS.Stack_Killed='N' and MDS.Commodity_Id='22' and CropYear is not null group by CropYear ";
            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty from (select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by WHR.Depositor_WHR_Id,SSD.Stack_ID,CropYear) as REC right join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<= convert(varchar(10),GETDATE()- DAY(GETDATE()),101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID  = REC.Stack_ID group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set ID_PreviousMonth='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                }
            }

            // query = "SELECT CropYear,Convert(decimal(18,0),isnull(sum(DelQty),0)/10) as AvlQty FROM tbl_MetaData_STACK as MDS inner join tbl_MetaData_GODOWN_2018 as MDG on MDG.Godown_ID=MDS.Godown_ID inner join tbl_metadata_storage_commodity as CMD on CMD.Commodity_ID=MDS.Commodity_ID left join (select WHR.BranchID,WHR.GodownID,CropYear,WHR.Depositor_WHR_Id,WHR.commodity_ID,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') group by WHR.BranchID,WHR.GodownID,WHR.Depositor_WHR_Id,SSD.Stack_ID,WHR.commodity_ID,CropYear) as REC on REC.Stack_ID=MDS.Stack_ID and REC.commodity_ID=MDS.Commodity_Id  RIGHT join (select DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Month(Issue_Date)=MONTH(GETDATE()) and YEAR(Issue_Date)=YEAR(GETDATE()) group by DSD.Godown_ID,DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEl.Stack_ID = MDS.Stack_ID where MDS.Stack_Killed='N' and MDS.Commodity_Id='22' and CropYear is not null group by CropYear ";
            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty from (select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by WHR.Depositor_WHR_Id,SSD.Stack_ID,CropYear) as REC right join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Month(Issue_Date)=MONTH(GETDATE()) and YEAR(Issue_Date)=YEAR(GETDATE()) group by DSD.Depositor_WHR_Id,DSD.Stack_ID) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID  = REC.Stack_ID group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set ID_ThisMonth='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                }
            }

            query = "select CropYear,convert(decimal(18,0),isnull(SUM(RecQty),0)/10) AvlQty from ( select CropYear,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date between convert(varchar(10),'04/01/2019',101)  and convert(varchar(10),GETDATE()- DAY(GETDATE()),101) and CropYear in ('2019-20','2018-19','2017-18','2016-17') and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,CropYear ) as REC group by CropYear";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ABags = ds.Tables[0].Rows[i]["AvlQty"].ToString();
                    if (ABags != null && ABags != "")
                    {
                        query = "Update tbl_StateDashBoardData set R_tillFirstC_Month='" + ABags + "' where CropYear='" + ds.Tables[0].Rows[i]["CropYear"].ToString() + "'";
                        cmd = new SqlCommand(query, con);
                        int a = cmd.ExecuteNonQuery();
                    }
                }
            }
            Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Update...');if(alert){ window.location='State_Welcome_DashBoard.aspx';}</script>");
        }
        catch (Exception ex)
        {
            Page.RegisterStartupScript("UserMsg", "<script>alert('Error..');if(alert){ window.location='State_Welcome_DashBoard.aspx';}</script>");
        }
    }
    protected void Label1_Click(object sender, EventArgs e)
    {
        Session["R_Type"] = "SP_AsOn_01042019";
        Session["Crop_Year"] = "All";
        Response.Redirect("State_DashBoard_DistWise_DrilDown.aspx");
    }
    protected void GD2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow gvr = GD1.SelectedRow;
        //Session["R_Type"] = "SP_AsOn_01042019";
        //Session["Crop_Year"] = gvr.Cells[0].Text;
        //Response.Redirect("State_DashBoard_DistWise_DrilDown.aspx");
    }
    protected void Label10_Click(object sender, EventArgs e)
    {
        Session["R_Type"] = "SP_AsOn_Date";
        Session["Crop_Year"] = "All";
        Response.Redirect("State_DashBoard_DistWise_DrilDown.aspx");
    }
    protected void Label4_Click(object sender, EventArgs e)
    {
        Session["R_Type"] = "RD_ThisMonth";
        Session["Crop_Year"] = "All";
        Response.Redirect("State_DashBoard_DistWise_DrilDown.aspx");
    }
}