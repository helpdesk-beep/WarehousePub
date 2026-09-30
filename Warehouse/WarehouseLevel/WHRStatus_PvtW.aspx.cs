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

public partial class WarehouseLevel_WHRStatus_PvtW : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["G_DepotID"].ToString() != "")
            {
                string depotId = Session["G_DepotID"].ToString();
                GetWHRDetails(depotId);
            }
        }
    }
    private void GetWHRDetails(string depotid)
    {
        try
        {
            if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
            {
                //string qry = "SELECT [Commodity_Id],[BranchID],[CropYear],convert(varchar(15),[WHR_Issue_Date],103) as WHRDATE,[Depositor_whr_id],[Commodity_Name],[RecQty] as Weight,[DelQty] as Issueweigt,[RecBags] as Bags,[DelBags] as Issuebags,(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=dbo.[View_WHRcurrentstock].Godown_ID ) as Godown_Name,[Depositor_Name],RecQty-DelQty as avilableQty,RecBags-DelBags as avilableBags,[Stack_Name] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["G_BranchId"].ToString() + "' ";
                string qry = "SELECT [Commodity_Id],[BranchID],[CropYear],convert(varchar(15),[WHR_Issue_Date],103) as WHRDATE,[Depositor_whr_id],[Commodity_Name],[RecQty] as Weight,[DelQty] as Issueweigt,[RecBags] as Bags,[DelBags] as Issuebags,(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=dbo.[View_WHRcurrentstock].Godown_ID ) as Godown_Name,[Depositor_Name],RecQty-DelQty as avilableQty,RecBags-DelBags as avilableBags,[Stack_Name] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["G_BranchId"].ToString() + "' and Godown_ID='" + Session["GodownID_New"].ToString() + "' ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV_WHRDETAILS.DataSource = ds.Tables[0];
                    GV_WHRDETAILS.DataBind();
                    lblRowCount.Text = "Total WHR Available : " + GV_WHRDETAILS.Rows.Count.ToString();
                }
                else
                {
                    GV_WHRDETAILS.DataSource = null;
                    GV_WHRDETAILS.DataBind();
                    lblmsg.Visible = true;
                    lblmsg.Text = "There is No WHR Present,Please Make WHR First";
                }
            }
            else
            {
                // string qry = "SELECT DISTINCT GD.Godown_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name +'  ('+ GD.Godown_ID + ')') as Godown_Name,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=whr.Commodity_Id  )as Commodity_Name,SUM(SSD.Bags) as Bags,CONVERT(decimal(18,4),sum(SSD.Weight)) as Weight,(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL') AS loss,(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status ='Active') AS gain,(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL') AS Issuebags,(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL') AS Issueweigt ,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId WHERE WHR.BranchId = '" + Session["G_BranchId"].ToString() + "' group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,whr.Commodity_Id having ((SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')))>0 order by GD.Godown_ID asc  ";
                string qry = "SELECT [Commodity_Id],[BranchID],[CropYear],convert(varchar(15),[WHR_Issue_Date],103) as WHRDATE,[Depositor_whr_id],[Commodity_Name],[RecQty] as Weight,[DelQty] as Issueweigt,[RecBags] as Bags,[DelBags] as Issuebags,(select Godown_Name from dbo.tbl_MetaData_GODOWN where Godown_ID=dbo.[View_WHRcurrentstock].Godown_ID ) as Godown_Name,[Depositor_Name],RecQty-DelQty as avilableQty,RecBags-DelBags as avilableBags,[Stack_Name] FROM [Intergrated_MP_STORAGE].[dbo].[View_WHRcurrentstock] where BranchID='" + Session["G_BranchId"].ToString() + "' ";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV_WHRDETAILS.DataSource = ds.Tables[0];
                    GV_WHRDETAILS.DataBind();
                    lblRowCount.Text = "Total WHR Available : " + GV_WHRDETAILS.Rows.Count.ToString();
                }
                else
                {
                    GV_WHRDETAILS.DataSource = null;
                    GV_WHRDETAILS.DataBind();
                    lblmsg.Visible = true;
                    lblmsg.Text = "There is No WHR Present,Please Make WHR First";
                }
            }
        }
        catch (Exception)
        {
            ///////
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Session["reporturl"] = "";
        //Session["reporturl"] = "WHRKillReport";
        //// ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        //Response.Redirect("ReportViewer_Depot.aspx");
    }
}
