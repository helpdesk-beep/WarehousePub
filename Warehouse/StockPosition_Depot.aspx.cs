using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class StockPosition_Depot : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            plotpoint_depot();
        }
    }

    private void plotpoint_depot()
    {
        SqlDataAdapter da = null;
        DataTable dt = new DataTable();
        //string str = "SELECT ds.DistrictId as  District_Id,(select tbl_MetaData_DISTRICT.District_Name from dbo.tbl_MetaData_DISTRICT where tbl_MetaData_DISTRICT.District_Id=ds.DistrictId)as  District_Name,ds.BranchID as DepotID,ds.DepotName,ds.latitude,ds.longitude,(SELECT isnull(sum([Godown_Capacity]),0)FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where [tbl_MetaData_GODOWN].BranchID=ds.BranchId and [tbl_MetaData_GODOWN].Remarks='Y') as 'totalCap',(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%Wheat%' and vwc.BranchID=ds.BranchId) as 'Wheat',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%rice%' and vwc.BranchID=ds.BranchId) as 'Rice',(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%sugar%' and vwc.BranchID=ds.BranchId) as 'Sugar',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%salt%' and vwc.BranchID=ds.BranchId) as 'Salt',(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%paddy%' and vwc.BranchID=ds.BranchId) as 'Paddy' , (SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where vwc.BranchID=ds.BranchId) as 'totalcapavl' FROM DepotStockPosition_GMap as ds";
      // 06/07/2017
        //string str = "select DistrictId as District_Id,District_Name,DepotID,DepotName,latitude,longitude,totalCap,(Rec_Whaet-DelWheat) as 'Wheat',(Rec_Rice-DelRice) as 'Rice',(Rec_Sugar-DelSugar) as 'Sugar',(Rec_Salt-DelSalt) as 'Salt' ,(Rec_Paddy-DelPaddy) as 'Paddy' ,(totalcapavlRec-totalcapavlDel) as 'totalcapavl' from (select MDG.DistrictId, (select tbl_MetaData_DISTRICT.District_Name from dbo.tbl_MetaData_DISTRICT where tbl_MetaData_DISTRICT.District_Id=MDG.DistrictId)as  District_Name,BranchID as DepotID,DepotName,isnull(latitude,0) as latitude,isnull(longitude,0) as longitude,(SELECT isnull(sum([Godown_Capacity]),0)FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where [tbl_MetaData_GODOWN].BranchID=MDG.BranchId and [tbl_MetaData_GODOWN].Remarks='Y') as 'totalCap',(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('2','3' , '4' , '74','47','1','20')) as 'Rec_Rice',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('2','3' , '4' , '74','47','1','20') group by GP.BranchID),0) as 'DelRice' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('5','6' , '7' , '22','35','28','53','54')) as 'Rec_Whaet',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('5','6','7','22','35','28','53','54') group by GP.BranchID),0) as 'DelWheat' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('13' , '14' , '24')) as 'Rec_Paddy',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('13' , '14' , '24') group by GP.BranchID),0) as 'DelPaddy' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id ='19') as 'Rec_Salt', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID and GP.Commodity_ID ='19' group by GP.BranchID),0) as 'DelSalt' ,   (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('46' , '49' , '50', '17' , '23')) as 'Rec_Sugar', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('46' , '49' , '50', '17' , '23') group by GP.BranchID),0) as 'DelSugar' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null) as 'totalcapavlRec', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID group by GP.BranchID),0) as 'totalcapavlDel' from DepotStockPosition_GMap as MDG) as MRG";
        string str = "select DistrictId as District_Id,District_Name,DepotID,DepotName,latitude,longitude,CONVERT(DECIMAL(18,0),(totalCap/10)) as totalCap,CONVERT(DECIMAL(18,0),(Rec_Whaet-DelWheat)/10) as 'Wheat',CONVERT(DECIMAL(18,0),(Rec_Rice-DelRice)/10) as 'Rice',CONVERT(DECIMAL(18,0),(Rec_Sugar-DelSugar)/10) as 'Sugar',CONVERT(DECIMAL(18,0),(Rec_Salt-DelSalt)/10) as 'Salt' ,CONVERT(DECIMAL(18,0),(Rec_Paddy-DelPaddy)/10) as 'Paddy' ,CONVERT(DECIMAL(18,0),(totalcapavlRec-totalcapavlDel)/10) as 'totalcapavl' from (select MDG.DistrictId, (select tbl_MetaData_DISTRICT.District_Name from dbo.tbl_MetaData_DISTRICT where tbl_MetaData_DISTRICT.District_Id=MDG.DistrictId)as  District_Name,BranchID as DepotID,DepotName,isnull(latitude,0) as latitude,isnull(longitude,0) as longitude,(SELECT isnull(sum([Godown_Capacity]),0)FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where [tbl_MetaData_GODOWN].BranchID=MDG.BranchId and [tbl_MetaData_GODOWN].Remarks='Y') as 'totalCap',(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('2','3' , '4' , '74','47','1','20')) as 'Rec_Rice',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('2','3' , '4' , '74','47','1','20') group by GP.BranchID),0) as 'DelRice' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('5','6' , '7' , '22','35','28','53','54')) as 'Rec_Whaet',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('5','6','7','22','35','28','53','54') group by GP.BranchID),0) as 'DelWheat' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('13' , '14' , '24')) as 'Rec_Paddy',isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('13' , '14' , '24') group by GP.BranchID),0) as 'DelPaddy' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id ='19') as 'Rec_Salt', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID and GP.Commodity_ID ='19' group by GP.BranchID),0) as 'DelSalt' ,   (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null and whr.Commodity_Id in ('46' , '49' , '50', '17' , '23')) as 'Rec_Sugar', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID and GP.Commodity_ID in ('46' , '49' , '50', '17' , '23') group by GP.BranchID),0) as 'DelSugar' , (select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.BranchID=MDG.BranchID and WHR.GodownID is not null) as 'totalcapavlRec', isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD  inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and  MDG.BranchID=GP.BranchID group by GP.BranchID),0) as 'totalcapavlDel' from DepotStockPosition_GMap as MDG) as MRG";

        da = new SqlDataAdapter(str,con);
        da.Fill(dt);
        Session["dt"] = dt;        
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];      
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";       
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 5;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.257749, 77.403488);
        decimal vaccap = 0;
        
        for (int i = 0; i < dt.Rows.Count ; i++)
        {
            vaccap = Convert.ToDecimal(dt.Rows[i]["totalCap"].ToString()) - Convert.ToDecimal(dt.Rows[i]["totalcapavl"].ToString());
            Boolean temp_red = false;
            GooglePoint GP1 = new GooglePoint();
            GP1.ID = "depot" + i+1.ToString();
            GP1.Latitude = CheckNull(dt.Rows[i]["latitude"].ToString());
            GP1.Longitude = CheckNull(dt.Rows[i]["longitude"].ToString());
            GP1.ToolTip = dt.Rows[i]["DepotName"].ToString();
            decimal avi = 0;
            decimal forzero =0;
            decimal forzero2 = 0;
            if (Convert.ToDecimal(dt.Rows[i]["totalcapavl"].ToString()) == 0)
            {
                forzero = 1;
            }
            else
            {
                forzero=Convert.ToDecimal(dt.Rows[i]["totalcapavl"].ToString());
            }
            if (Convert.ToDecimal(dt.Rows[i]["totalCap"].ToString()) == 0)
            {
                forzero2 = 1;
            }
            else
            {
                forzero2 = Convert.ToDecimal(dt.Rows[i]["totalCap"].ToString());
            }
            avi = (forzero * 100) / forzero2;
            if (CheckNull(dt.Rows[i]["totalcapavl"].ToString()) ==0)
            {
               temp_red = true;
               GP1.IconImage = "icons/Green.png";
            }

            else if (avi>80)
            {
                temp_red = false;
                GP1.IconImage = "icons/Red.png";
            }
            else
            {
                GP1.IconImage = "icons/Blue.png";
            }
            GP1.InfoHTML = "<b> Current Stock Balance at Branch : </b> " + dt.Rows[i]["DepotName"].ToString() + ", " + dt.Rows[i]["District_Name"].ToString() + "<br />" + "Total Branch Capacity : " + dt.Rows[i]["totalCap"].ToString() + "  M.T." + "<br />" + "<br />" + "Total Capacity Avilable : " + dt.Rows[i]["totalcapavl"].ToString() + "  M.T." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  M.T." + "<br />" + "Wheat : " + dt.Rows[i]["Wheat"].ToString() + "  M.T." + "<br />" + "Rice : " + dt.Rows[i]["Rice"].ToString() + "  M.T." + "<br />" + "Sugar : " + dt.Rows[i]["Sugar"].ToString() + "  M.T." + "<br />" + "Salt : " + dt.Rows[i]["Salt"].ToString() + " M.T. " + "<br />" + "Paddy : " + dt.Rows[i]["Paddy"].ToString() + "  M.T." + "<br />";
            if (GP1.Latitude > 0 && GP1.Longitude > 0)
            {
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
            }
        }  
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