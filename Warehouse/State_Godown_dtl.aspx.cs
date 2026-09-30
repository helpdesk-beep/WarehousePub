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

public partial class State_Godown_dtl : System.Web.UI.Page
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
        //string Branch = Session["BranchId"].ToString();
        //string str = "SELECT vwhr.[BranchID],mg.Godown_Name,mg.Longitude,mg.Latitude,(select DepotName from dbo.tbl_MetaData_DEPOT where BranchID=mg.BranchID) as Branch,Godown_Capacity,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],sum([RecQty])-sum([DelQty]) as currentqty,sum([RecBags])-sum([DelBags]) as CurrentBags ,[District_Id] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] as vwhr inner join dbo.tbl_MetaData_GODOWN as mg on vwhr.Godown_ID=mg.Godown_ID and vwhr.BranchID=mg.BranchID where mg.Remarks='Y'  and mg.Latitude is not null group by vwhr.[BranchID],mg.Godown_Name,mg.Godown_Capacity,mg.Godown_Scientific_Capacity,vwhr.[Godown_ID],mg.Godown_ID,[District_Id],mg.BranchID,mg.Longitude,mg.Latitude";
        //string str = "select MDD.BranchId as BranchID,MG.Godown_Name,convert(nvarchar(50),MG.Longitude) as Longitude,convert(nvarchar(50),MG.Latitude) as Latitude,DepotName as Branch,MG.Godown_Capacity,MG.Godown_Scientific_Capacity,MG.Godown_ID,RecQty-(IssueWeight-Gain+Loss) as currentqty,RecBags-IssueBags as CurrentBags,MDD.DistrictId as District_Id from (select WHR.GodownID,sum(TotalBags_Received) as RecBags,sum(Total_Qty_Received) as RecQty,isnull(DSDGP.IssueBags,0) as IssueBags,isnull(DSDGP.IssueWeight,0) as IssueWeight,isnull(DSDGP.Loss,0) as Loss,isnull(DSDGP.Gain,0) as Gain from tbl_storage_Depositor_WHR_Relation as WHR left join (  select  DSD.Godown_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight ) as  IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain  from tbl_Delivery_Stacking_Details_GatePass as DSD inner join  tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No  where GP.Status='Active' group by DSD.Godown_ID) as DSDGP on DSDGP.Godown_ID = whr.GodownID  group by WHR.GodownID,IssueBags,IssueWeight,Loss,Gain ) as MRG join tbl_MetaData_GODOWN as MG on MG.Godown_ID=MRG.GodownID join  tbl_MetaData_DEPOT as MDD on MDD.BranchId=MG.BranchID where MG.Remarks='Y' and Latitude>'1' and Longitude>'1'";
        //string str = "select MDD.BranchId as BranchID,MG.Godown_Name,convert(nvarchar(50),MG.Longitude) as Longitude,convert(nvarchar(50),MG.Latitude) as Latitude,DepotName as Branch,CONVERT(DECIMAL(18,0),MG.Godown_Capacity/10) as Godown_Capacity,CONVERT(DECIMAL(18,0),MG.Godown_Scientific_Capacity/10) as Godown_Scientific_Capacity,MG.Godown_ID,CONVERT(DECIMAL(18,0),(RecQty-(IssueWeight-Gain+Loss))/10) as currentqty,RecBags-IssueBags as CurrentBags,MDD.DistrictId as District_Id from (select WHR.GodownID,sum(TotalBags_Received) as RecBags,sum(Total_Qty_Received) as RecQty,isnull(DSDGP.IssueBags,0) as IssueBags,isnull(DSDGP.IssueWeight,0) as IssueWeight,isnull(DSDGP.Loss,0) as Loss,isnull(DSDGP.Gain,0) as Gain from tbl_storage_Depositor_WHR_Relation as WHR left join (  select  DSD.Godown_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight ) as  IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain  from tbl_Delivery_Stacking_Details_GatePass as DSD inner join  tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No  where GP.Status='Active' group by DSD.Godown_ID) as DSDGP on DSDGP.Godown_ID = whr.GodownID  group by WHR.GodownID,IssueBags,IssueWeight,Loss,Gain ) as MRG join tbl_MetaData_GODOWN as MG on MG.Godown_ID=MRG.GodownID join  tbl_MetaData_DEPOT as MDD on MDD.BranchId=MG.BranchID where MG.Remarks='Y' and Latitude>'1' and Longitude>'1'";
        string str = "select MDD.BranchId as BranchID,MG.Godown_Name,convert(nvarchar(50),MG.Longitude) as Longitude,convert(nvarchar(50),MG.Latitude) as Latitude,DepotName as Branch,CONVERT(DECIMAL(18,0),MG.Godown_Capacity/10) as Godown_Capacity,CONVERT(DECIMAL(18,0),MG.Godown_Scientific_Capacity/10) as Godown_Scientific_Capacity,MG.Godown_ID,CONVERT(DECIMAL(18,0),(RecQty-(IssueWeight-Gain+Loss))/10) as currentqty,RecBags-IssueBags as CurrentBags,MDD.DistrictId as District_Id from (select WHR.GodownID,sum(TotalBags_Received) as RecBags,sum(Total_Qty_Received) as RecQty,isnull(DSDGP.IssueBags,0) as IssueBags,isnull(DSDGP.IssueWeight,0) as IssueWeight,isnull(DSDGP.Loss,0) as Loss,isnull(DSDGP.Gain,0) as Gain from tbl_storage_Depositor_WHR_Relation as WHR left join (  select  DSD.Godown_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight ) as  IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain  from tbl_Delivery_Stacking_Details_GatePass as DSD inner join  tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No  where GP.Status='Active' group by DSD.Godown_ID) as DSDGP on DSDGP.Godown_ID = whr.GodownID  group by WHR.GodownID,IssueBags,IssueWeight,Loss,Gain ) as MRG join tbl_MetaData_GODOWN_2018 as MG on MG.Godown_ID=MRG.GodownID join  tbl_MetaData_DEPOT as MDD on MDD.BranchId=MG.BranchID where MG.Latitude>'0' and MG.Longitude>'0'";

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
            vaccap = Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) - Convert.ToDecimal(dt.Rows[i]["currentqty"].ToString());
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
            if (Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) == 0)
            {
                forzero2 = 1;
            }
            else
            {
                forzero2 = Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString());
            }
            avi = (forzero * 100) / forzero2;
            if (CheckNullQty(dt.Rows[i]["currentqty"].ToString()) <=0 )
            {
                if (Convert.ToDecimal(dt.Rows[i]["Godown_Capacity"].ToString()) != 0)
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
            GP1.InfoHTML = "<b> Current Stock Balance at Godown : </b> " + dt.Rows[i]["Godown_Name"].ToString() + ", " + dt.Rows[i]["Branch"].ToString() + "<br />" + "Total Godown Max Capacity : " + dt.Rows[i]["Godown_Capacity"].ToString() + "  M.T." + "<br />" + "<br />" + "Total Current Capacity: " + dt.Rows[i]["currentqty"].ToString() + "  M.T." + "<br />" + "<br />" + "Total Capacity Vacant : " + vaccap + "  M.T." + "<br />";
            if (GP1.Latitude > 0 && GP1.Longitude > 0)
            {
                GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);
            }
        }
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

    protected double CheckNullQty (string Val)
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