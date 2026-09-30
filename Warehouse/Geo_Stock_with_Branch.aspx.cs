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

public partial class Geo_Stock_with_Branch : System.Web.UI.Page
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
        SqlDataAdapter da = null;
        DataTable dt = new DataTable();
        //string str = "SELECT vwhr.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=mg.BranchID) as Branch,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],sum([RecQty])-sum([DelQty]) as currentqty,sum([RecBags])-sum([DelBags]) as CurrentBags,[District_Id] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwhr inner join dbo.tbl_MetaData_GODOWN as mg on vwhr.Godown_ID=mg.Godown_ID and vwhr.BranchID=mg.BranchID where mg.Remarks='Y' and mg.Latitude is not null group by vwhr.[BranchID],mg.Godown_Name,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],mg.Godown_ID,[District_Id],mg.BranchID,mg.Longitude,mg.Latitude UNION ALL select [BranchID],'' as Godown_Name,[longitude] as Longitude,[latitude] as Latitude,[DepotName] as Branch ,0 as Godown_Scientific_Capacity,'' as [Godown_ID],'0' as currentqty,'0' as CurrentBags,[DistrictId] as District_Id from  dbo.DepotStockPosition_GMap where DepotStockPosition_GMap.latitude is not null";
        //string str = "SELECT mg.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,DepotName as Branch,mg.Godown_Scientific_Capacity,mg.[Godown_ID],(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=mg.Godown_ID and WHR.GodownID is not null) -(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and mg.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))  as currentqty ,mg.[DistrictId] FROM  dbo.tbl_MetaData_GODOWN as mg inner join tbl_MetaData_DEPOT as MDD on MDD.BranchID=mg.BranchID where mg.Remarks='Y' and mg.Latitude >'1' and mg.Longitude>'1' UNION ALL select [BranchID],'' as Godown_Name,[longitude] as Longitude,[latitude] as Latitude,[DepotName] as Branch ,0 as Godown_Scientific_Capacity,'' as [Godown_ID],'0' as currentqty,[DistrictId] as District_Id from  dbo.DepotStockPosition_GMap where DepotStockPosition_GMap.latitude is not null";
        //string str = "SELECT mg.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,DepotName as Branch,mg.Godown_Scientific_Capacity,mg.[Godown_ID],(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=mg.Godown_ID and WHR.GodownID is not null) -(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and mg.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))  as currentqty ,mg.[DistrictId] FROM  dbo.tbl_MetaData_GODOWN_2018 as mg inner join tbl_MetaData_DEPOT as MDD on MDD.BranchID=mg.BranchID where mg.IsActive='Y' and mg.Latitude >'0' and mg.Longitude>'0' UNION ALL select [BranchID],'' as Godown_Name,[longitude] as Longitude,[latitude] as Latitude,[DepotName] as Branch ,0 as Godown_Scientific_Capacity,'' as [Godown_ID],'0' as currentqty,[DistrictId] as District_Id from  dbo.DepotStockPosition_GMap where DepotStockPosition_GMap.latitude is not null";
        string str = "Select * from Stock_Capacity_For_Google_MAP_01";
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);
        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.257749, 77.403488);
        decimal vaccap = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            vaccap = Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()) - Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
            Boolean temp_red = false;
            GooglePoint GP1 = new GooglePoint();
            GP1.ID = "Godown_ID" + i + 1.ToString();
            GP1.Latitude = CheckNull(dt.Rows[i]["Latitude"].ToString());
            GP1.Longitude = CheckNull(dt.Rows[i]["Longitude"].ToString());
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
            if (CheckNullQty(dt.Rows[i]["currentqty"].ToString()) == 0.00)
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
            GP1.InfoHTML = "<b> Current Stock Balance at Godown : </b> " + dt.Rows[i]["Godown_Name"].ToString() + ", " + dt.Rows[i]["Branch"].ToString() + "<br />" + "Total Godown Capacity : " + dt.Rows[i]["Godown_Scientific_Capacity"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Current  Capacity: " + dt.Rows[i]["currentqty"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  Qtls." + "<br />";
            if (GP1.Latitude > 0 && GP1.Longitude > 0)
            {
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
            }
        }
    }

    protected double CheckNullQty(string Val)
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

    protected double CheckNull(string Val)
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
}