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

public partial class StatePages_State_DashBoard_DistWise_DrilDown : System.Web.UI.Page
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
                
                if (Session["R_Type"].ToString() == "SP_AsOn_01042019")
                {
                    fillGD1();
                }
                else if (Session["R_Type"].ToString() == "SP_AsOn_Date")
                {
                    fillGD6();
                }
                else if (Session["R_Type"].ToString() == "RD_ThisMonth")
                {
                    fillGD3();
                }
            }
    }
   public void fillGD1()
   {
       if (con.State == ConnectionState.Closed)
       {
            con.Open();
       }
       try
       {
           string query = "select MD.District_ID,District_Name,isnull(AvlQty_2016,0) as AvlBal16,isnull(AvlQty_2017,0) as AvlBal17,isnull(AvlQty_2018,0) as AvlBal18,isnull(AvlQty_2019,0) as AvlBal19,isnull(AvlQty_2016,0)+isnull(AvlQty_2017,0)+isnull(AvlQty_2018,0)+isnull(AvlQty_2019,0) as AvlOnDate01042019 from (select MDDIS.District_ID,District_Name from tbl_metadata_district as MDDIS) as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2016 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<=convert(varchar(10),'04/01/2019',101) and CropYear ='2016-17' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<=convert(varchar(10),'04/01/2019',101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID  left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2017 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<=convert(varchar(10),'04/01/2019',101) and CropYear ='2017-18' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<=convert(varchar(10),'04/01/2019',101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2017 on RECDEL2017.District_ID=MD.District_ID left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2018 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<=convert(varchar(10),'04/01/2019',101) and CropYear ='2018-19' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<=convert(varchar(10),'04/01/2019',101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2018 on RECDEL2018.District_ID=MD.District_ID left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2019 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.WHR_Issue_Date<=convert(varchar(10),'04/01/2019',101) and CropYear ='2019-20' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Issue_Date<=convert(varchar(10),'04/01/2019',101) group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2019 on RECDEL2019.District_ID=MD.District_ID order by District_Name";
           cmd = new SqlCommand(query, con);
           da = new SqlDataAdapter(cmd);
           ds = new DataSet();
           da.Fill(ds);
           if (ds.Tables[0].Rows.Count > 0)
           {
               DataTable dt = ds.Tables[0];
               GD1.DataSource = ds;
               GD1.DataBind();
               tr_trasondate01042019.Visible = true;

               decimal total55 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal16"));
               GD1.FooterRow.Cells[0].Text = "Total";
               GD1.FooterRow.Cells[1].Text = total55.ToString("N0");

               decimal total555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal17"));
               GD1.FooterRow.Cells[2].Text = total555.ToString("N0");

               decimal total5555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal18"));
               GD1.FooterRow.Cells[3].Text = total5555.ToString("N0");

               decimal total55555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal19"));
               GD1.FooterRow.Cells[4].Text = total55555.ToString("N0");

               decimal total555555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlOnDate01042019"));
               GD1.FooterRow.Cells[5].Text = total555555.ToString("N0");

           }
           else
           {
               tr_trasondate01042019.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }
   }
   public void fillGD6()
   {
       if (con.State == ConnectionState.Closed)
       {
           con.Open();
       }
       try
       {
           string query = "select MD.District_ID,District_Name,isnull(AvlQty_2016,0) as AvlBal16 from tbl_metadata_district as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2016 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear ='2016-17' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID order by District_Name";
           cmd = new SqlCommand(query, con);
           da = new SqlDataAdapter(cmd);
           ds = new DataSet();
           da.Fill(ds);
           if (ds.Tables[0].Rows.Count > 0)
           {
               DataTable dt = ds.Tables[0];
               for (int i = 0; i <= 2; i++)
               {
                   string CropY = "";
                   if (i == 0)
                   {
                       CropY = "2017-18";
                       dt.Columns.Add("AvlBal17", typeof(System.Decimal));
                       query = "select MD.District_ID,District_Name,isnull(AvlQty_2017,0) as Avl2017 from tbl_metadata_district as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2017 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear ='" + CropY + "' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID order by District_Name";
                       SqlCommand cmd1 = new SqlCommand(query, con);
                       SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                       DataTable dt1 = new DataTable();
                       da1.Fill(dt1);
                       for (int a = 0; a < dt1.Rows.Count; a++)
                       {
                           if (ds.Tables[0].Rows[a]["District_Name"].ToString().Trim() == dt1.Rows[a]["District_Name"].ToString().Trim())
                           {
                               dt.Rows[a][3] = dt1.Rows[a]["Avl2017"].ToString().Trim();
                           }
                       }
                   }
                   else if (i == 1)
                   {
                       CropY = "2018-19";
                       dt.Columns.Add("AvlBal18", typeof(System.Decimal));
                       query = "select MD.District_ID,District_Name,isnull(AvlQty_2018,0) as Avl2018 from tbl_metadata_district as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2018 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear ='" + CropY + "' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID order by District_Name";
                       SqlCommand cmd1 = new SqlCommand(query, con);
                       SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                       DataTable dt1 = new DataTable();
                       da1.Fill(dt1);
                       for (int a = 0; a < dt1.Rows.Count; a++)
                       {
                           if (ds.Tables[0].Rows[a]["District_Name"].ToString().Trim() == dt1.Rows[a]["District_Name"].ToString().Trim())
                           {
                               dt.Rows[a][4] = dt1.Rows[a]["Avl2018"].ToString().Trim();
                           }
                       }
                   }
                   else if (i == 2)
                   {
                       CropY = "2019-20";
                       dt.Columns.Add("AvlBal19", typeof(System.Decimal));
                       query = "select MD.District_ID,District_Name,isnull(AvlQty_2019,0) as Avl2019 from tbl_metadata_district as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty)-SUM(DelQty),0)/10) AvlQty_2019 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where CropYear ='" + CropY + "' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC left join (select DSD.Depositor_WHR_Id,DSD.Stack_ID,(isnull(SUM(DSD.Bags_Weight),0)+isnull(SUM(DSD.Loss),0)-isnull(SUM(DSD.Gain),0)) as DelQty from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' group by DSD.Depositor_WHR_Id,DSD.Stack_ID ) DEL on DEL.Depositor_WHR_Id=REC.Depositor_WHR_Id and DEL.Stack_ID=REC.Stack_ID  group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID order by District_Name";
                       SqlCommand cmd1 = new SqlCommand(query, con);
                       SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                       DataTable dt1 = new DataTable();
                       da1.Fill(dt1);
                       for (int a = 0; a < dt1.Rows.Count; a++)
                       {
                           if (ds.Tables[0].Rows[a]["District_Name"].ToString().Trim() == dt1.Rows[a]["District_Name"].ToString().Trim())
                           {
                               dt.Rows[a][5] = dt1.Rows[a]["Avl2019"].ToString().Trim();
                           }
                       }
                   }
               }
               dt.Columns.Add("AvlOnDateDate", typeof(System.Decimal));
               for (int a = 0; a < dt.Rows.Count; a++)
               {
                   dt.Rows[a][6] = Convert.ToInt32(dt.Rows[a]["AvlBal16"].ToString().Trim()) + Convert.ToInt32(dt.Rows[a]["AvlBal17"].ToString().Trim()) + Convert.ToInt32(dt.Rows[a]["AvlBal18"].ToString().Trim()) + Convert.ToInt32(dt.Rows[a]["AvlBal19"].ToString().Trim());
               }

               GD6.DataSource = dt;
               GD6.DataBind();
               GD6.Columns[6].Visible = false;
               tr_trasondate.Visible = true;

               decimal total55 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal16"));
               GD6.FooterRow.Cells[0].Text = "Total";
               GD6.FooterRow.Cells[1].Text = total55.ToString("N0");

               decimal total555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal17"));
               GD6.FooterRow.Cells[2].Text = total555.ToString("N0");

               decimal total5555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal18"));
               GD6.FooterRow.Cells[3].Text = total5555.ToString("N0");

               decimal total55555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal19"));
               GD6.FooterRow.Cells[4].Text = total55555.ToString("N0");

               decimal total555555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlOnDateDate"));
               GD6.FooterRow.Cells[5].Text = total555555.ToString("N0");
           }
           else
           {
               tr_trasondate.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }
   }

   public void fillGD3()
   {
       if (con.State == ConnectionState.Closed)
       {
           con.Open();
       }
       try
       {
           string query = "select MD.District_ID,District_Name,isnull(AvlQty_2016,0) as AvlBal16,isnull(AvlQty_2017,0) as AvlBal17,isnull(AvlQty_2018,0) as AvlBal18,isnull(AvlQty_2019,0) as AvlBal19,isnull(AvlQty_2016,0)+isnull(AvlQty_2017,0)+isnull(AvlQty_2018,0)+isnull(AvlQty_2019,0) as AvlOnDate01042019 from (select MDDIS.District_ID,District_Name from tbl_metadata_district as MDDIS) as MD left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty),0)/10) AvlQty_2016 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear ='2016-17' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC group by District_ID ) as RECDEL2016 on RECDEL2016.District_ID=MD.District_ID  left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty),0)/10) AvlQty_2017 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear ='2017-18' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC group by District_ID ) as RECDEL2017 on RECDEL2017.District_ID=MD.District_ID left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty),0)/10) AvlQty_2018 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear ='2018-19' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC  group by District_ID ) as RECDEL2018 on RECDEL2018.District_ID=MD.District_ID left join (select District_ID,convert(decimal(18,0),isnull(SUM(RecQty),0)/10) AvlQty_2019 from ( select WHR.District_ID,WHR.Depositor_WHR_Id,SSD.Stack_ID,isnull(sum(Weight),0) as RecQty from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where Month(WHR.WHR_Issue_Date)=MONTH(GETDATE()) and YEAR(WHR.WHR_Issue_Date)=YEAR(GETDATE()) and CropYear ='2019-20' and WHR.Commodity_Id='22' and WHR.GodownID in (select MDG.Godown_ID from tbl_MetaData_GODOWN_2018 as MDG) and SSD.Stack_ID in (select MDS.Stack_ID from tbl_MetaData_STACK as MDS where Stack_Killed='N') group by SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.District_ID) as REC group by District_ID ) as RECDEL2019 on RECDEL2019.District_ID=MD.District_ID order by District_Name";
           cmd = new SqlCommand(query, con);
           da = new SqlDataAdapter(cmd);
           ds = new DataSet();
           da.Fill(ds);
           if (ds.Tables[0].Rows.Count > 0)
           {
               GD3.DataSource = ds;
               GD3.DataBind();
               tr_RD_ThisM.Visible = true;

               DataTable dt = ds.Tables[0];
               decimal total55 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal16"));
               GD3.FooterRow.Cells[0].Text = "Total";
               GD3.FooterRow.Cells[1].Text = total55.ToString("N0");

               decimal total555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal17"));
               GD3.FooterRow.Cells[2].Text = total555.ToString("N0");

               decimal total5555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal18"));
               GD3.FooterRow.Cells[3].Text = total5555.ToString("N0");

               decimal total55555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBal19"));
               GD3.FooterRow.Cells[4].Text = total55555.ToString("N0");

               decimal total555555 = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlOnDate01042019"));
               GD3.FooterRow.Cells[5].Text = total555555.ToString("N0");
           }
           else
           {
               tr_RD_ThisM.Visible = false;
           }
       }
       catch (Exception ex)
       {

       }
   }
   protected void GD6_SelectedIndexChanged(object sender, EventArgs e)
   {
       GridViewRow gvr = GD6.SelectedRow;
       Session["DashBoardDistID"] = gvr.Cells[6].Text;
       Session["reporturl"] = "";
       Session["reporturl"] = "State_Dashboard_GdwnWiseAsOnDate";
      // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open("ReportViewer_Region.aspx\",\"_blank\")", true);
      // Response.Write("<script>window.open( '../IssueCenterLevel/Storage/ReportViewer_Region.aspx' , '-blank' );</script>");
       ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../IssueCenterLevel/Storage/ReportViewer_Region.aspx\",\"_blank\")", true);

   }
}
