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

public partial class GodownGeoBranch : System.Web.UI.Page
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
        //  HttpContext.Current.Server.ScriptTimeout = 90000;
        string Branch = Session["BranchId"].ToString();
        //string str = "SELECT vwhr.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=mg.BranchID) as Branch,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],sum([RecQty])-sum([DelQty]) as currentqty,sum([RecBags])-sum([DelBags]) as CurrentBags ,[District_Id] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwhr inner join dbo.tbl_MetaData_GODOWN as mg on vwhr.Godown_ID=mg.Godown_ID and vwhr.BranchID=mg.BranchID where vwhr.BranchID='" + Session["BranchId"].ToString() + "' and mg.Remarks='Y' and mg.Latitude is not null group by vwhr.[BranchID],mg.Godown_Name,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],mg.Godown_ID,[District_Id],mg.BranchID,mg.Longitude,mg.Latitude";
        //string str = "select BranchID,Godown_Name,Longitude,Latitude,Branch,Godown_Scientific_Capacity,Godown_ID,RecQty-DelQty as currentqty,DistrictId from  (select MDG.BranchID,Godown_Name,Longitude,Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=MDG.BranchID) as Branch,MDG.Godown_Scientific_Capacity,Godown_ID,convert(int,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null)) as 'RecQty',convert(int,(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))) as 'DelQty',DistrictId from tbl_MetaData_GODOWN as MDG where MDG.Remarks='Y' and MDG.BranchID='" + Session["BranchId"].ToString() + "' and Latitude>'1' and Longitude>'1' ) as MRG ";
        string str = "select BranchID,Godown_Name,Longitude,Latitude,Branch,Godown_Scientific_Capacity,Godown_ID,RecQty-DelQty as currentqty,DistrictId from  (select MDG.BranchID,Godown_Name,Longitude,Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=MDG.BranchID) as Branch,MDG.Godown_Scientific_Capacity,Godown_ID,convert(int,(select isnull(SUM(Total_Qty_Received),0) from tbl_storage_Depositor_WHR_Relation as WHR where WHR.GodownID=MDG.Godown_ID and WHR.GodownID is not null)) as 'RecQty',convert(int,(isnull((select isnull(SUM(DSD.Bags_Weight)+SUM(DSD.Loss) -SUM(DSD.Gain) , 0) from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and MDG.Godown_ID=GP.Godown_ID group by GP.Godown_ID),0))) as 'DelQty',DistrictId from tbl_MetaData_GODOWN_2018 as MDG where MDG.BranchID='" + Session["BranchId"].ToString() + "' and Latitude>'1' and Longitude>'1' ) as MRG ";

        da = new SqlDataAdapter(str, con);
        da.Fill(dt);
        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 10;
        DataTable dt2 = new DataTable();

        string str2 = "SELECT [DepotName],[latitude],[longitude],[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[DepotStockPosition_GMap] where BranchID='" + Session["BranchId"].ToString() + "'";
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);

        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        if (dt.Rows.Count > 0)
        {
            string lati = dt.Rows[0]["latitude"].ToString();
            string longi = dt.Rows[0]["longitude"].ToString();
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", Convert.ToDouble(lati), Convert.ToDouble(longi));
        }
        else
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.179410, 75.785203);
        }

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
            if (CheckNull(dt.Rows[i]["currentqty"].ToString()) == 0)
            {
                if (Convert.ToDecimal(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()) != 0)
                {
                    temp_red = true;
                    GP1.IconImage = "/icons/Green.png";
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
            GP1.InfoHTML = "<b> Current Stock Balance at Godown : </b> " + dt.Rows[i]["Godown_Name"].ToString() + ", " + dt.Rows[i]["Branch"].ToString() + "<br />" + "Total Godown Capacity : " + dt.Rows[i]["Godown_Scientific_Capacity"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Current Capacity: " + dt.Rows[i]["currentqty"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  Qtls." + "<br />";
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