using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

public partial class IssueCenterLevel_Storage_WLC_DepositeForm_ChkQuality : System.Web.UI.Page
{
    SqlConnection con_MPStorage;
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlTransaction sqltran;
    DataSet ds;
    string BranchId;
    string DistId;
    string Check_QualityID = "";
    int BID = 0;
    public string GenerateOTP = "", OTPSMS = "";
   // string strcon = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString()); //Integrated_MP_Storage
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                BranchId = Session["BranchId"].ToString();
                DistId = Session["Depot_DistID"].ToString();
                Session["userBranch"] = Session["BranchId"].ToString();   // Login Branch Code
                GetCommodities();
               string MoBNo = ChkMobNo_ForOTP();
               txtMobNum.Text = MoBNo;
               lblcug.Text = txtMobNum.Text;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetCommodities()
    {
        {
            try
            {
                string select = "";
                select = "Select Commodity_Id,Commodity_Name From tbl_MetaData_STORAGE_COMMODITY Where Commodity_Id in ('33','63','64')";
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_commodity.DataSource = ds.Tables[0];
                    ddl_commodity.DataTextField = "Commodity_Name";
                    ddl_commodity.DataValueField = "Commodity_Id";
                    ddl_commodity.DataBind();
                    ddl_commodity.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddl_commodity.DataSource = "";
                    ddl_commodity.DataBind();
                    ddl_commodity.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    public void GetDepositer()
    {
        {
            try
            {
                string select = "";
               // select = "select Distinct WHR_Request from MPSCSC.dbo.DepositerForm_CSM2018 where CommodityId = '" + ddl_commodity.SelectedValue + "' and godown in (select Recd_Godown from MPSCSC.dbo.SCSC_Procurement_CSM where Branch_Id = '" + Session["BranchId"].ToString() + "')";
               // select = "select Distinct WHR_Request from MPSCSC.dbo.DepositerForm_CSM2018 where CommodityId = '" + ddl_commodity.SelectedValue + "' and WHR_Request not in (select Distinct WHR_Request from ChkQuality_ProviosnalDepositorForm) and godown in (select Recd_Godown from MPSCSC.dbo.SCSC_Procurement_CSM where Branch_Id = '" + Session["BranchId"].ToString() + "')";
                
               // updated on 11/06/2018
                select = "select Distinct WHR_Request from MPSCSC.dbo.DepositerForm_CSM2018 where CommodityId = '" + ddl_commodity.SelectedValue + "' and WHR_Request not in (select Distinct WHR_Request from ChkQuality_ProviosnalDepositorForm where Branch_ID='" + Session["BranchId"].ToString() + "') and godown in (select Distinct Recd_Godown from MPSCSC.dbo.SCSC_Procurement_CSM where Branch_Id = '" + Session["BranchId"].ToString() + "')";
                
                da = new SqlDataAdapter(select, con);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_depositerform.DataSource = ds.Tables[0];
                    ddl_depositerform.DataTextField = "WHR_Request";
                    ddl_depositerform.DataValueField = "WHR_Request";
                    ddl_depositerform.DataBind();
                    ddl_depositerform.Items.Insert(0, "--Select--");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Depositor Form Found ....')", true);
                    ddl_depositerform.DataSource = "";
                    ddl_depositerform.DataBind();
                    ddl_depositerform.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }

    protected void ddl_depositerform_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_commodity.SelectedValue == "33")  // Sarso
        {
            GridView2_Sarso.DataSource = "";
            GridView2_Sarso.DataBind();
            // string qry = "SELECT pds.districtsmp.district_name, RecdTbl.Distt_ID ,Recd_Godown as GdnId  ,StackName ,StackNumber ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request ,IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,CMS_GodownSurveyor_Inspection.SurveyorName,CMS_GodownSurveyor_Inspection.SurveyorMob ,ImpuriForemttrincldTarmir ,AdmxtrothrtypincldTor ,UnripShrvlldimmatre ,DamgdWvilled ,Smllatrophidsid  ,MositureContent ,Sarson_ImpForeignmatt_IncTaramira ,Sarson_Admix_WOT_IncToria ,Sarson_Unripe_ShirvellImmature,Sarson_DamagedAndWeevilled ,Sarson_SmallAtrophiedSeeds ,Sarson_MoistureContent, Srvyr_Reg_Initial.Srvyr_Nm , Srvyr_Reg_Initial.Srvyr_MobNo , Society_MSP.Society_Id,Society_MSP.Society_Name FROM SCSC_Procurement_CSM as RecdTbl inner join DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join IssueToSangrahanaKendra_CSM2018 on IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number inner join CMS_GodownSurveyor_Inspection on CMS_GodownSurveyor_Inspection.SurveyorID = IssueToSangrahanaKendra_CSM2018.G_Surveyor and CMS_GodownSurveyor_Inspection.TruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join IssueFAQ_Final_Sarson on IssueFAQ_Final_Sarson.TeruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join Srvyr_Reg_Initial on Srvyr_Reg_Initial.Srvyr_Id = IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join pds.districtsmp on pds.districtsmp.district_code = RecdTbl.Distt_ID  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join Society_MSP on Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "'";

           // string qry = "SELECT tbl_MetaData_DISTRICT.district_name, RecdTbl.Distt_ID ,Recd_Godown as GdnId  ,StackName ,StackNumber ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request ,MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorName,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorMob ,ImpuriForemttrincldTarmir ,AdmxtrothrtypincldTor ,UnripShrvlldimmatre ,DamgdWvilled ,Smllatrophidsid  ,MositureContent ,Sarson_ImpForeignmatt_IncTaramira ,Sarson_Admix_WOT_IncToria ,Sarson_Unripe_ShirvellImmature,Sarson_DamagedAndWeevilled ,Sarson_SmallAtrophiedSeeds ,Sarson_MoistureContent, MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Nm , MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_MobNo ,Society_MSP.Society_Id,Society_MSP.Society_Name   FROM MPSCSC.dbo.SCSC_Procurement_CSM as RecdTbl inner join  MPSCSC.dbo.DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join  MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018 on MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number inner join  MPSCSC.dbo.CMS_GodownSurveyor_Inspection on MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorID = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.G_Surveyor  and MPSCSC.dbo.CMS_GodownSurveyor_Inspection.TruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join  MPSCSC.dbo.IssueFAQ_Final_Sarson on MPSCSC.dbo.IssueFAQ_Final_Sarson.TeruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join  MPSCSC.dbo.Srvyr_Reg_Initial on MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Id = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join  tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id = '23'+RecdTbl.Distt_ID  inner join  tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join  tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join  Society_MSP on Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "'";

            string qry = "SELECT tbl_MetaData_DISTRICT.district_name, RecdTbl.Distt_ID ,Recd_Godown as GdnId  ,StackName ,StackNumber ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request ,MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorName,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorMob ,ImpuriForemttrincldTarmir as Foreignmatter ,AdmxtrothrtypincldTor as Admixture ,UnripShrvlldimmatre as ImmatureShrivelled ,DamgdWvilled as Damagedpuls ,Smllatrophidsid as Slightlydamaged ,'' as Weevilled ,MositureContent as MoistureContent ,Sarson_ImpForeignmatt_IncTaramira ,Sarson_Admix_WOT_IncToria ,Sarson_Unripe_ShirvellImmature,Sarson_DamagedAndWeevilled ,Sarson_SmallAtrophiedSeeds ,Sarson_MoistureContent, MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Nm , MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_MobNo ,MPSCSC.dbo.Society_MSP.Society_Id,MPSCSC.dbo.Society_MSP.Society_Name    FROM MPSCSC.dbo.SCSC_Procurement_CSM as RecdTbl inner join   MPSCSC.dbo.DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join   MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018 on MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number inner join   MPSCSC.dbo.CMS_GodownSurveyor_Inspection on MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorID = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.G_Surveyor   and MPSCSC.dbo.CMS_GodownSurveyor_Inspection.TruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join   MPSCSC.dbo.IssueFAQ_Final_Sarson on MPSCSC.dbo.IssueFAQ_Final_Sarson.TeruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join   MPSCSC.dbo.Srvyr_Reg_Initial on MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Id = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join   tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id = '23'+RecdTbl.Distt_ID  inner join   tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join   tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join   MPSCSC.dbo.Society_MSP on MPSCSC.dbo.Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "'";
            
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2_Sarso.DataSource = ds.Tables[0];
                GridView2_Sarso.DataBind();
                pnltr.Visible = true;
            }
            else
            {
                GridView2_Sarso.DataSource = "";
                GridView2_Sarso.DataBind();
                pnltr.Visible = false;
            }
            GridView2_Chana.Visible = false;
            GridView2_Masoor.Visible = false;
            GridView2_Sarso.Visible = true;
        }

        else if (ddl_commodity.SelectedValue == "63")   // Chana
        {
            GridView2_Chana.DataSource = "";
            GridView2_Chana.DataBind();
            // string qry = "SELECT pds.districtsmp.district_name, RecdTbl.Distt_ID  ,StackName ,StackNumber,Recd_Godown as GdnId ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request ,IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,CMS_GodownSurveyor_Inspection.SurveyorName,CMS_GodownSurveyor_Inspection.SurveyorMob , foreignMatter,OtherFoodGrains,DamagedGrains,SligDamagedTouchGrains,ImmaShrivAndBroGrains,AdmixOfOtherVarieties ,WeevilleGrains,MositureContent  ,[Gram_ForeignMatter] ,[Gram_OtherFoodGrains] ,[Gram_DamagedGrains] ,[Gram_SligDamagedTouchGrains] ,[Gram_ImmaShrivAndBroGrains]  ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent], Srvyr_Reg_Initial.Srvyr_Nm , Srvyr_Reg_Initial.Srvyr_MobNo , Society_MSP.Society_Id,Society_MSP.Society_Name FROM SCSC_Procurement_CSM as RecdTbl inner join DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join IssueToSangrahanaKendra_CSM2018 on IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number  inner join CMS_GodownSurveyor_Inspection on CMS_GodownSurveyor_Inspection.SurveyorID = IssueToSangrahanaKendra_CSM2018.G_Surveyor and CMS_GodownSurveyor_Inspection.TruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join IssueFAQ_Final_Chana on IssueFAQ_Final_Chana.TeruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join Srvyr_Reg_Initial on Srvyr_Reg_Initial.Srvyr_Id = IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join pds.districtsmp on pds.districtsmp.district_code = RecdTbl.Distt_ID  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join Society_MSP on Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "' ";

            string qry = "SELECT tbl_MetaData_DISTRICT.District_Name, RecdTbl.Distt_ID  ,StackName ,StackNumber,Recd_Godown as GdnId ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request ,MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorName,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorMob , foreignMatter,OtherFoodGrains,DamagedGrains,SligDamagedTouchGrains,ImmaShrivAndBroGrains,AdmixOfOtherVarieties ,WeevilleGrains,MositureContent  ,[Gram_ForeignMatter] ,[Gram_OtherFoodGrains] ,[Gram_DamagedGrains] ,[Gram_SligDamagedTouchGrains] ,[Gram_ImmaShrivAndBroGrains]  ,[Gram_AdmixOfOtherVarieties] ,[Gram_WeevilleGrains] ,[Gram_MositureContent], MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Nm , MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_MobNo , MPSCSC.dbo.Society_MSP.Society_Id,MPSCSC.dbo.Society_MSP.Society_Name FROM MPSCSC.dbo.SCSC_Procurement_CSM as RecdTbl inner join MPSCSC.dbo.DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018 on MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number  inner join MPSCSC.dbo.CMS_GodownSurveyor_Inspection on MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorID = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.G_Surveyor and MPSCSC.dbo.CMS_GodownSurveyor_Inspection.TruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join MPSCSC.dbo.IssueFAQ_Final_Chana on MPSCSC.dbo.IssueFAQ_Final_Chana.TeruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join MPSCSC.dbo.Srvyr_Reg_Initial on MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Id = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id = '23'+RecdTbl.Distt_ID  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join MPSCSC.dbo.Society_MSP on MPSCSC.dbo.Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "'";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2_Chana.DataSource = ds.Tables[0];
                GridView2_Chana.DataBind();
                pnltr.Visible = true;
            }
            else
            {
                GridView2_Chana.DataSource = "";
                GridView2_Chana.DataBind();
                pnltr.Visible = false;
            }
            GridView2_Chana.Visible = true;
            GridView2_Masoor.Visible = false;
            GridView2_Sarso.Visible = false;
        }

        else if (ddl_commodity.SelectedValue == "64")   // Masoor
        {
            GridView2_Masoor.DataSource = "";
            GridView2_Masoor.DataBind();
            //string qry = "SELECT pds.districtsmp.district_name, RecdTbl.Distt_ID,Recd_Godown as GdnId  ,StackName ,StackNumber ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags  ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request  ,IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,CMS_GodownSurveyor_Inspection.SurveyorName,CMS_GodownSurveyor_Inspection.SurveyorMob ,Foreignmatter ,Admixture ,Damagedpuls ,Slightlydamaged ,ImmatureShrivelled ,Weevilled ,MoistureContent  ,Massur_ForeignMatter ,Massur_Admixture ,Massur_DamagedPulses ,Massur_SligDamagedPulses ,Massur_ImmaShrivellPulses  ,Massur_weevilledPulses ,Massur_MoistureContent , Srvyr_Reg_Initial.Srvyr_Nm , Srvyr_Reg_Initial.Srvyr_MobNo , Society_MSP.Society_Id,Society_MSP.Society_Name FROM SCSC_Procurement_CSM as RecdTbl inner join DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join IssueToSangrahanaKendra_CSM2018 on IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number inner join CMS_GodownSurveyor_Inspection on CMS_GodownSurveyor_Inspection.SurveyorID = IssueToSangrahanaKendra_CSM2018.G_Surveyor and CMS_GodownSurveyor_Inspection.TruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join IssueFAQ_Final_Masoor on IssueFAQ_Final_Masoor.TeruckChallan = IssueToSangrahanaKendra_CSM2018.TruckChalanNo  inner join Srvyr_Reg_Initial on Srvyr_Reg_Initial.Srvyr_Id = IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join pds.districtsmp on pds.districtsmp.district_code = RecdTbl.Distt_ID  inner join tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join Society_MSP on Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "'";
            string qry = "SELECT tbl_MetaData_DISTRICT.district_name, RecdTbl.Distt_ID,Recd_Godown as GdnId  ,StackName ,StackNumber ,RecdTbl.Acceptance_No ,convert(varchar(10),Acceptance_Date,103)Acceptance_Date ,convert(varchar(10),RecdTbl.Created_Date,103)Created_Date ,Recd_Bags  ,RecdBags_JuteNew as Jute_Newbags  ,RecdBags_PP as PP_Bags  ,RecdBags_JuteOld as Jute_oldBags  ,RecdBags_A_twill as Atwill_Bags  ,NetWeight  ,RecdTbl.TC_Number ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name  ,Crop_Year, tbl_MetaData_GODOWN.Godown_Name , DPFrm.WHR_Request  ,MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID , G_Surveyor ,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorName,MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorMob ,Foreignmatter ,Admixture ,Damagedpuls ,Slightlydamaged ,ImmatureShrivelled ,Weevilled ,MoistureContent  ,Massur_ForeignMatter ,Massur_Admixture ,Massur_DamagedPulses ,Massur_SligDamagedPulses ,Massur_ImmaShrivellPulses  ,Massur_weevilledPulses ,Massur_MoistureContent , MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Nm , MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_MobNo , MPSCSC.dbo.Society_MSP.Society_Id,MPSCSC.dbo.Society_MSP.Society_Name   FROM MPSCSC.dbo.SCSC_Procurement_CSM as RecdTbl inner join  MPSCSC.dbo.DepositerForm_CSM2018 as DPFrm on DPFrm.TC_Number = RecdTbl.TC_Number inner join  MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018 on MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo = RecdTbl.TC_Number inner join  MPSCSC.dbo.CMS_GodownSurveyor_Inspection on MPSCSC.dbo.CMS_GodownSurveyor_Inspection.SurveyorID = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.G_Surveyor and  MPSCSC.dbo.CMS_GodownSurveyor_Inspection.TruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo inner join  MPSCSC.dbo.IssueFAQ_Final_Masoor on MPSCSC.dbo.IssueFAQ_Final_Masoor.TeruckChallan = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.TruckChalanNo  inner join  MPSCSC.dbo.Srvyr_Reg_Initial on MPSCSC.dbo.Srvyr_Reg_Initial.Srvyr_Id = MPSCSC.dbo.IssueToSangrahanaKendra_CSM2018.Srvyr_ID inner join  tbl_MetaData_DISTRICT on tbl_MetaData_DISTRICT.District_Id = '23'+RecdTbl.Distt_ID  inner join  tbl_MetaData_GODOWN on tbl_MetaData_GODOWN.Godown_ID = Recd_Godown inner join tbl_MetaData_STORAGE_COMMODITY on tbl_MetaData_STORAGE_COMMODITY.Commodity_Id = RecdTbl.Commodity_Id inner join  MPSCSC.dbo.Society_MSP on MPSCSC.dbo.Society_MSP.Society_Id = DPFrm.Purchase_Center where DPFrm.WHR_Request = '" + ddl_depositerform.SelectedValue + "' ";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2_Masoor.DataSource = ds.Tables[0];
                GridView2_Masoor.DataBind();
                pnltr.Visible = true;
            }
            else
            {
                GridView2_Masoor.DataSource = "";
                GridView2_Masoor.DataBind();
                pnltr.Visible = false;
            }
            GridView2_Chana.Visible = false;
            GridView2_Masoor.Visible = true;
            GridView2_Sarso.Visible = false;
        }
        else
        {
            GridView2_Sarso.DataSource = "";
            GridView2_Sarso.DataBind();
            GridView2_Chana.DataSource = "";
            GridView2_Chana.DataBind();
            GridView2_Masoor.DataSource = "";
            GridView2_Masoor.DataBind();
            GridView2_Chana.Visible = false;
            GridView2_Masoor.Visible = false;
            GridView2_Sarso.Visible = false;
        }
    }

    protected void ddl_commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositer();
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        int s = 0; int GCount = 0;
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        sqltran = con.BeginTransaction();
        try
        {
            if (ddl_commodity.SelectedItem.Text == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Commodity ....')", true);
            }
            else if (ddl_depositerform.SelectedItem.Text == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Depositor ....')", true);
            }
            //else if ( txtCheckOTP.Text=="")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter OTP ....')", true);
            //}
            else if (hdfOTP.Value != txtCheckOTP.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Correct OTP ....')", true);
            }
            else
            {

                if (ddl_commodity.SelectedItem.Text == "GRAM")
                {
                    for (s = 0; s < GridView2_Chana.Rows.Count; s++)
                    {
                        string chkQtyDefault = "";
                        GetApplicationNo();
                        if (((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_2")).SelectedValue == "N")
                        {
                            chkQtyDefault = "Y";
                        }
                        else if (((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_2")).SelectedValue == "Y")
                        {
                            chkQtyDefault = "N";
                        }
                        string Qry = "INSERT INTO [ChkQuality_ProviosnalDepositorForm] ([ChkQ_ID],[District_ID],[Branch_ID],[Godown_ID],[TC_Number],[Acceptance_No],[Acceptance_Date],[WHR_Request],[Recd_Bags],[NetWeight],[G_MositureContent],[M_MositureContent],[Society_Id],[Society_Name],[Qualitychk_Status],[Rejection_Status],[QualityChk_Default],[CreatedDate],[CreatedBy],CommodityID,AID) VALUES ('" + Check_QualityID + "','" + Session["Depot_DistID"].ToString() + "','" + Session["BranchId"].ToString() + "','" + GridView2_Chana.Rows[s].Cells[0].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[3].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[6].Text.ToString() + "','" + getDate_MDY(GridView2_Chana.Rows[s].Cells[7].Text.ToString()) + "','" + ddl_depositerform.SelectedValue.ToString() + "','" + GridView2_Chana.Rows[s].Cells[8].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[14].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[21].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[30].Text.ToString() + "','" + GridView2_Chana.Rows[s].Cells[4].Text.ToString() + "',N'" + GridView2_Chana.Rows[s].Cells[5].Text.ToString() + "','" + ((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_1")).SelectedValue.ToString() + "'   ,'" + ((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_2")).SelectedValue.ToString() + "' ,'" + chkQtyDefault + "',GETDATE(),'" + ip + "','" + ddl_commodity.SelectedValue.ToString() + "','" + BID + "')";
                        SqlCommand cmd = new SqlCommand(Qry, con, sqltran);
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            GCount = s + 1;
                        }
                    }
                    if (GridView2_Chana.Rows.Count == GCount && GridView2_Chana.Rows.Count != 0 )
                    {
                        sqltran.Commit();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save ....')", true);
                        ddl_commodity_SelectedIndexChanged(sender, e);
                        hidepanl();
                        GridView2_Chana.DataSource = "";
                        GridView2_Chana.DataBind();
                    }
                }
                else if (ddl_commodity.SelectedItem.Text == "LENTIL")
                {
                    for (s = 0; s < GridView2_Masoor.Rows.Count; s++)
                    {
                        string chkQtyDefault = "";
                        GetApplicationNo();
                        if (((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_4")).SelectedValue == "N")
                        {
                            chkQtyDefault = "Y";
                        }
                        else if (((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_4")).SelectedValue == "Y")
                        {
                            chkQtyDefault = "N";
                        }
                        string Qry = "INSERT INTO [ChkQuality_ProviosnalDepositorForm] ([ChkQ_ID],[District_ID],[Branch_ID],[Godown_ID],[TC_Number],[Acceptance_No],[Acceptance_Date],[WHR_Request],[Recd_Bags],[NetWeight],[G_MositureContent],[M_MositureContent],[Society_Id],[Society_Name],[Qualitychk_Status],[Rejection_Status],[QualityChk_Default],[CreatedDate],[CreatedBy],CommodityID,AID) VALUES ('" + Check_QualityID + "','" + Session["Depot_DistID"].ToString() + "','" + Session["BranchId"].ToString() + "','" + GridView2_Masoor.Rows[s].Cells[0].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[3].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[6].Text.ToString() + "','" + getDate_MDY(GridView2_Masoor.Rows[s].Cells[7].Text.ToString()) + "','" + ddl_depositerform.SelectedValue.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[8].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[13].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[20].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[28].Text.ToString() + "','" + GridView2_Masoor.Rows[s].Cells[4].Text.ToString() + "',N'" + GridView2_Masoor.Rows[s].Cells[5].Text.ToString() + "','" + ((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_3")).SelectedValue.ToString() + "'   ,'" + ((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_4")).SelectedValue.ToString() + "' ,'" + chkQtyDefault + "',GETDATE(),'" + ip + "','" + ddl_commodity.SelectedValue.ToString() + "','" + BID + "')";
                        SqlCommand cmd = new SqlCommand(Qry, con, sqltran);
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            GCount = s + 1;
                        }
                    }
                    if (GridView2_Masoor.Rows.Count == GCount && GridView2_Masoor.Rows.Count != 0)
                    {
                        sqltran.Commit();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save ....')", true);
                        ddl_commodity_SelectedIndexChanged(sender, e);
                        hidepanl();
                        GridView2_Masoor.DataSource = "";
                        GridView2_Masoor.DataBind();
                    }
                }
                else if (ddl_commodity.SelectedItem.Text == "Mustard-Sarason")
                {
                    for (s = 0; s < GridView2_Sarso.Rows.Count; s++)
                    {
                        string chkQtyDefault = "";
                        GetApplicationNo();
                        if (((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_6")).SelectedValue == "N")
                        {
                            chkQtyDefault = "Y";
                        }
                        else if (((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_6")).SelectedValue == "Y")
                        {
                            chkQtyDefault = "N";
                        }
                        string Qry = "INSERT INTO [ChkQuality_ProviosnalDepositorForm] ([ChkQ_ID],[District_ID],[Branch_ID],[Godown_ID],[TC_Number],[Acceptance_No],[Acceptance_Date],[WHR_Request],[Recd_Bags],[NetWeight],[G_MositureContent],[M_MositureContent],[Society_Id],[Society_Name],[Qualitychk_Status],[Rejection_Status],[QualityChk_Default],[CreatedDate],[CreatedBy],CommodityID,AID) VALUES ('" + Check_QualityID + "','" + Session["Depot_DistID"].ToString() + "','" + Session["BranchId"].ToString() + "','" + GridView2_Sarso.Rows[s].Cells[0].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[3].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[6].Text.ToString() + "','" + getDate_MDY(GridView2_Sarso.Rows[s].Cells[7].Text.ToString()) + "','" + ddl_depositerform.SelectedValue.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[8].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[13].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[19].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[27].Text.ToString() + "','" + GridView2_Sarso.Rows[s].Cells[4].Text.ToString() + "',N'" + GridView2_Sarso.Rows[s].Cells[5].Text.ToString() + "','" + ((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_5")).SelectedValue.ToString() + "'   ,'" + ((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_6")).SelectedValue.ToString() + "' ,'" + chkQtyDefault + "',GETDATE(),'" + ip + "','" + ddl_commodity.SelectedValue.ToString() + "','" + BID + "')";
                        SqlCommand cmd = new SqlCommand(Qry, con, sqltran);
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            GCount = s + 1;
                        }
                    }
                    if (GridView2_Sarso.Rows.Count == GCount && GridView2_Sarso.Rows.Count != 0)
                    {
                        sqltran.Commit();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save ....')", true);
                        ddl_commodity_SelectedIndexChanged(sender, e);
                        hidepanl();
                        GridView2_Sarso.DataSource = "";
                        GridView2_Sarso.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error Try Again ....')", true);
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public void GetApplicationNo()
    {
        string AGC = "";
        string qry = "select max(AId) as BId from ChkQuality_ProviosnalDepositorForm where Branch_ID='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con, sqltran);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                BID = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = BID + 1;
                string INCN = SubBN.ToString();
                if (INCN.Length == 1)
                {
                    AGC = "00" + INCN.ToString();
                }
                else if (INCN.Length == 2)
                {
                    AGC = "0" + INCN.ToString();
                }
                else if (INCN.Length == 3)
                {
                    AGC = INCN.ToString();
                }
                else if (INCN.Length > 3)
                {
                    AGC = INCN.ToString();
                }
                Check_QualityID = Session["BranchId"].ToString() + "18" + AGC.ToString();
                BID = SubBN;
            }
            else
            {
                Check_QualityID = Session["BranchId"].ToString() + "18" + "001";
                BID = 1;
            }
        }
        else
        {
            Check_QualityID = Session["BranchId"].ToString() + "18" + "001";
            BID = 1;
        }
    }
    public int chkinsp()
    {
        int ch = 0;
        int s;
        int Count_Rows=0;
        if (ddl_commodity.SelectedValue == "63")
        {
            for (s = 0; s < GridView2_Chana.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_1")).SelectedValue=="Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        else if (ddl_commodity.SelectedValue == "64")
        {
           
            for (s = 0; s < GridView2_Masoor.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_3")).SelectedValue == "Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        else if (ddl_commodity.SelectedValue == "33")
        {
            
            for (s = 0; s < GridView2_Sarso.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_5")).SelectedValue == "Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        return ch;
    }

    public int chkRejection()
    {
        int ch = 0;
        int s;
        int Count_Rows = 0;
        if (ddl_commodity.SelectedValue == "63")
        {
            for (s = 0; s < GridView2_Chana.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_1")).SelectedValue == "N" && ((DropDownList)GridView2_Chana.Rows[s].FindControl("ddl_status_2")).SelectedValue == "Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        else if (ddl_commodity.SelectedValue == "64")
        {

            for (s = 0; s < GridView2_Masoor.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_3")).SelectedValue == "N" && ((DropDownList)GridView2_Masoor.Rows[s].FindControl("ddl_status_4")).SelectedValue == "Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        else if (ddl_commodity.SelectedValue == "33")
        {

            for (s = 0; s < GridView2_Sarso.Rows.Count; s++)
            {
                if (((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_5")).SelectedValue == "N" && ((DropDownList)GridView2_Sarso.Rows[s].FindControl("ddl_status_6")).SelectedValue == "Y")
                {
                    Count_Rows = s + 1;
                }
            }
            return ch = Count_Rows;
        }
        return ch;
    }

    protected void btnOTP_Click(object sender, EventArgs e)
    {
        if (ddl_commodity.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Commodity ....')", true);
        }
        else if (ddl_depositerform.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Depositor ....')", true);
        }
        else
        {
            int chk = chkinsp();
            if (chk > 0)
            {
                int chkrej = chkRejection();
                if (chkrej == 0)
                {
                    if (txtMobNum.Text != "" && lblcug.Text != "")
                    {
                        // Call Jquery Function TimerFunc()
                        txtCheckOTP.Text = "";
                        GenerateUniqueOTP();
                        //   btnsendOTP.Enabled = false;
                        //   txtCheckOTP.Enabled = true;
                        //   btncheckOTP.Enabled = true;
                        txtCheckOTP.Focus();
                        trbtn.Visible = true;
                        btnOTP.Enabled = false;
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:TimerFunc(); ", true);
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('Mobile Number Not Available'); </script> ");
                        return;
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया यह सुनिस्चित करें की रिजेक्ट की स्थिति मे  20 % गुणवत्ता कि जाँच की गई हैं । '); </script> ");
                }
            }
            else if(chk==0)
            {
                Page.RegisterClientScriptBlock("mymsg1", "<script language=javascript> alert('कृपया यह सुनिस्चित करें की जमा फार्म के विरुध 20 % गुणवत्ता कि जाँच की गई हैं । '); </script> ");
            }
        }
    }
    protected void GenerateUniqueOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;

        characters += alphabets + small_alphabets + numbers;

        string MONumber = ddl_depositerform.SelectedItem.Text;
        int lastdigit = int.Parse(MONumber.Substring(6));
        int length = 0;
        if (lastdigit >= 6)
        {
            length = 8;
        }
        else if (lastdigit >= 3 && lastdigit <= 5)
        {
            length = 6;
        }
        else
        {
            length = 5;
        }

        //int length = int.Parse(ddlMvmtNo.SelectedItem.Value);
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            }   while (otp.IndexOf(character) != -1);
            otp += character;
        }

        GenerateOTP = otp;
        lblotpis.Text = otp;
        hdfOTP.Value = otp;

        //OTPSMS = "'" + ddl_commodity.SelectedItem.Text + "' Depositor Form Number " + ddl_depositerform.SelectedItem.Text + " OTP Is '" + otp + "'";
        //hdfOTP.Value = "";
        //hdfOTP.Value = otp;
        //SMS Message = new SMS();
        //string MobileNo = "";
        //MobileNo = txtMobNum.Text;
        //Message.SendSMS(MobileNo, OTPSMS);
    }

    public void hidepanl()
    {
        txtCheckOTP.Text = "";
        btnOTP.Enabled = true;
        pnltr.Visible = false;
        trbtn.Visible = false;
    }
    public string ChkMobNo_ForOTP()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string ReturnMobNo = "";
        string QueryMax = "select Mobile_No from [tbl_Warehousing_Contact] where Branch_ID='"+ Session["BranchId"].ToString() +"'";
        SqlCommand cmd = new SqlCommand(QueryMax, con);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "" || str3 != "0")
        {
            ReturnMobNo = str3;
        }
        else
        {
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Mobile No Available For Sending OTP Please Contact HeadOffice Bhopal ....')", true);
        }
        return ReturnMobNo;
    }
}
