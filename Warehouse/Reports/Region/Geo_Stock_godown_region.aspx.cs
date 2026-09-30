using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_Region_Geo_Stock_godown_region : System.Web.UI.Page
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
        string reg=Session["Region_ID"].ToString();
        string str = "SELECT vwhr.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=mg.BranchID) as Branch,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],sum([RecQty])-sum([DelQty]) as currentqty,sum([RecBags])-sum([DelBags]) as CurrentBags,(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%Wheat%' and vwc.Godown_ID=mg.Godown_ID) as 'Wheat',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%rice%' and vwc.Godown_ID=mg.Godown_ID) as 'Rice',(SELECT isnull((sum([RecQty])-sum([DelQty])),0)  FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%sugar%' and vwc.Godown_ID=mg.Godown_ID) as 'Sugar',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%salt%' and vwc.Godown_ID=mg.Godown_ID) as 'Salt',(SELECT isnull((sum([RecQty])-sum([DelQty])),0) FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwc where Commodity_Name like '%paddy%' and vwc.Godown_ID=mg.Godown_ID) as 'Paddy' ,[District_Id] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwhr inner join dbo.tbl_MetaData_GODOWN as mg on vwhr.Godown_ID=mg.Godown_ID and vwhr.BranchID=mg.BranchID where mg.Remarks='Y' and vwhr.Region='" + Session["Region_ID"].ToString() + "' and mg.Latitude is not null group by vwhr.[BranchID],mg.Godown_Name,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],mg.Godown_ID,[District_Id],mg.BranchID,mg.Longitude,mg.Latitude";
        da = new SqlDataAdapter(str, con);
        da.Fill(dt);
        Session["dt"] = dt;
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "100%";
        GoogleMapForASPNet1.GoogleMapObject.Height = "700px";
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 7;
        if (reg == "1")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.257749, 77.403488);
        }
        else if (reg == "2")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 26.218400, 78.189514);

        }
        else if (reg == "3")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.181045, 79.981109);

        }
        else if (reg == "4")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 22.719809, 75.854618);

        }
        else if (reg == "5")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.179410, 75.785203);

        }
        else if (reg == "6")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 23.838581, 78.737811);

        }
        else if (reg == "7")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 24.537518, 81.302647);

        }
        else if (reg == "8")
        {
            GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 22.744844, 77.732767);

        }
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
            if (CheckNull(dt.Rows[i]["currentqty"].ToString()) == CheckNull(dt.Rows[i]["Godown_Scientific_Capacity"].ToString()))
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
            GP1.InfoHTML = "<b> Current Stock Balance at Godown : </b> " + dt.Rows[i]["Godown_Name"].ToString() + ", " + dt.Rows[i]["Branch"].ToString() + "<br />" + "Total Godown Capacity : " + dt.Rows[i]["Godown_Scientific_Capacity"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Current Capacity: " + dt.Rows[i]["currentqty"].ToString() + "  Qtls." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  Qtls." + "<br />" + "Wheat : " + dt.Rows[i]["Wheat"].ToString() + "  Qtls." + "<br />" + "Rice : " + dt.Rows[i]["Rice"].ToString() + "  Qtls." + "<br />" + "Sugar : " + dt.Rows[i]["Sugar"].ToString() + "  Qtls." + "<br />" + "Salt : " + dt.Rows[i]["Salt"].ToString() + " Qtls. " + "<br />" + "Paddy : " + dt.Rows[i]["Paddy"].ToString() + "  Qtls." + "<br />";
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