using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class GioStock_Godown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            plotpoint_depot();
        }
    }

    private void plotpoint_depot()
    {
        //HttpContext.Current.Server.ScriptTimeout = 90000;
        //Page.Server.ScriptTimeout = 120;
        SqlDataAdapter da = null;
        DataTable dt = new DataTable();
        //string str = "SELECT vwhr.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=mg.BranchID) as Branch,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],sum([RecQty])-sum([DelQty]) as currentqty,sum([RecBags])-sum([DelBags]) as CurrentBags,(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%Wheat%' and vwc.Godown_ID=mg.Godown_ID) as 'Wheat',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%rice%' and vwc.Godown_ID=mg.Godown_ID) as 'Rice',(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%sugar%' and vwc.Godown_ID=mg.Godown_ID) as 'Sugar',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%salt%' and vwc.Godown_ID=mg.Godown_ID) as 'Salt',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%paddy%' and vwc.Godown_ID=mg.Godown_ID) as 'Paddy' ,[District_Id] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwhr inner join dbo.tbl_MetaData_GODOWN as mg on vwhr.Godown_ID=mg.Godown_ID and vwhr.BranchID=mg.BranchID where mg.Remarks='Y' and mg.Latitude is not null group by vwhr.[BranchID],mg.Godown_Name,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],mg.Godown_ID,[District_Id],mg.BranchID,mg.Longitude,mg.Latitude";
        //string str = "select BranchID,Godown_Name,Longitude,Latitude,Branch,Godown_Scientific_Capacity,Godown_ID,(RecQty-DelQty) as currentqty ,(Rec_Whaet-DelWheat) as 'Wheat',(Rec_Rice-DelRice) as 'Rice',(Rec_Sugar-DelSugar) as 'Sugar',(Rec_Salt-DelSalt) as 'Salt' ,(Rec_Paddy-DelPaddy) as 'Paddy',District_Id from (select MDG.BranchID,Godown_Name,Longitude,Latitude,mdd.DepotName as Branch,Godown_Scientific_Capacity,Godown_ID,convert(int,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null)) as 'RecQty',convert(int,(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))) as 'DelQty' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('2','3' , '4' , '74','47','1','20')) as 'Rec_Rice',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('2','3' , '4' , '74','47','1','20') group by GP.Godown_ID),0) as 'DelRice' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('5','6' , '7' , '22','35','28','53','54')) as 'Rec_Whaet',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('5','6','7','22','35','28','53','54') group by GP.Godown_ID),0) as 'DelWheat' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('13' , '14' , '24')) as 'Rec_Paddy',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('13' , '14' , '24') group by GP.Godown_ID),0) as 'DelPaddy' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id ='19') as 'Rec_Salt',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID ='19' group by GP.Godown_ID),0) as 'DelSalt' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('46' , '49' , '50', '17' , '23')) as 'Rec_Sugar',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('46' , '49' , '50', '17' , '23') group by GP.Godown_ID),0) as 'DelSugar' ,MDG.DistrictId as District_Id from tbl_metadata_godown as MDG inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=MDG.BranchID  where MDG.Remarks='Y' and MDG.Latitude >'0' and  Longitude >'0') as MRG";
        string str = "select BranchID,Godown_Name,Longitude,Latitude,Branch,Godown_Scientific_Capacity,Godown_ID,(RecQty-DelQty) as currentqty ,(Rec_Whaet-DelWheat) as 'Wheat',(Rec_Rice-DelRice) as 'Rice',(Rec_Sugar-DelSugar) as 'Sugar',(Rec_Salt-DelSalt) as 'Salt' ,(Rec_Paddy-DelPaddy) as 'Paddy',District_Id from (select MDG.BranchID,Godown_Name,MDG.Longitude,MDG.Latitude,mdd.DepotName as Branch,Godown_Scientific_Capacity,Godown_ID,convert(int,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null)) as 'RecQty',convert(int,(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))) as 'DelQty' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('2','3' , '4' , '74','47','1','20')) as 'Rec_Rice',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('2','3' , '4' , '74','47','1','20') group by GP.Godown_ID),0) as 'DelRice' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('5','6' , '7' , '22','35','28','53','54')) as 'Rec_Whaet',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('5','6','7','22','35','28','53','54') group by GP.Godown_ID),0) as 'DelWheat' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('13' , '14' , '24')) as 'Rec_Paddy',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('13' , '14' , '24') group by GP.Godown_ID),0) as 'DelPaddy' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id ='19') as 'Rec_Salt',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID ='19' group by GP.Godown_ID),0) as 'DelSalt' ,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null and whr.Commodity_Id in ('46' , '49' , '50', '17' , '23')) as 'Rec_Sugar',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID and GP.Commodity_ID in ('46' , '49' , '50', '17' , '23') group by GP.Godown_ID),0) as 'DelSugar' ,MDG.DistrictId as District_Id from tbl_MetaData_GODOWN_2018 as MDG inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=MDG.BranchID  where MDG.Latitude >'0' and  MDG.Longitude >'0') as MRG";
        
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);
        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 15;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.257749, 77.403488);
        decimal vaccap = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            vaccap = Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()) - Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
            Boolean temp_red = false;
            GooglePoint GP1 = new GooglePoint();
            GP1.ID = "Godown_ID" + i + 1.ToString();

            GP1.Latitude = CheckNulllatlong(dt.Rows[i]["Latitude"].ToString());
            GP1.Longitude = CheckNulllatlong(dt.Rows[i]["Longitude"].ToString());
            GP1.ToolTip = dt.Rows[i]["Godown_Name"].ToString();
            decimal avi = 0;
            decimal forzero = 0;
            decimal forzero2 = 0;
            if (Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString()) == 0)
            {
                forzero = 1;
            }
            else
            {
                forzero = Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
            }
            if (Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()) == 0)
            {
                forzero2 = 1;
            }
            else
            {
                forzero2 = Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString());
            }
            avi = (forzero * 100) / forzero2;
            if (CheckNull(dt.Rows[i]["currentqty"].ToString()) <= 0)
            {
                if (Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()) != 0)
                {
                    temp_red = true;
                    GP1.IconImage = "icons/Green.png";
                }
                else
                {
                    GP1.IconImage = "icons/wr.png";

                }
            }

            else if (avi > 80)
            {
                temp_red = false;
                GP1.IconImage = "icons/Red.png";
            }
            else
            {
                GP1.IconImage = "icons/Blue.png";
            }
            GP1.InfoHTML = "<b> Current Stock Balance at Godown : </b> " + dt.Rows[i]["Godown_Name"].ToString() + ", " + dt.Rows[i]["Branch"].ToString() + "<br />" + "Total Godown Capacity : " + dt.Rows[i]["Godown_Scientific_Capacity"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Current Capacity: " + dt.Rows[i]["currentqty"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  Qtls." + "<br />" + "Wheat : " + dt.Rows[i]["Wheat"].ToString() + "  Qtls." + "<br />" + "Rice : " + dt.Rows[i]["Rice"].ToString() + "  Qtls." + "<br />" + "Sugar : " + dt.Rows[i]["Sugar"].ToString() + "  Qtls." + "<br />" + "Salt : " + dt.Rows[i]["Salt"].ToString() + " Qtls. " + "<br />" + "Paddy : " + dt.Rows[i]["Paddy"].ToString() + "  Qtls." + "<br />";
            if (GP1.Latitude > 0 && GP1.Longitude > 0)
            {
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
            }
        }
    }

    protected double CheckNulllatlong(string Val)
    {
        string chkval = "0"; string dotClean = "0"; string othr = "0"; string mrgval = "0";
        double rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null || Val == "0")
        {
            rval = 0;
        }
        else
        {
            try
            {
                if (Val.Length > 4)
                {
                    chkval = Val.Substring(0, 3);
                    dotClean = Val.Replace(".", "");
                    othr = dotClean.Remove(0, 2);
                    mrgval = chkval + othr;
                    rval = Convert.ToDouble(mrgval);
                }
            }
            catch (Exception ex)
            {

            }
        }
        return rval;
    }

    protected double CheckNull(string Val)
    {
        double rval = 0;
        if (Val == "" || Val.ToLower().Contains("&nbsp;") || Val == null)
        {
            rval = 0;
        }
        else
        {
            rval = Convert.ToDouble(Val);
        }
        return rval;
    }
}