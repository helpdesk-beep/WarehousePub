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
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;


public partial class Reports_Branch_CheckGdwnLink_JVSRegID : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetRegID();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetRegID()
    {
        trhide1.Visible = false;
        ddlRegID.DataSource = "";
        string qry = "";
        //qry = "select Distinct INSP.Registration_ID,INSP.Registration_ID +' ('+ UPPER((select Warehouse_name from tbl_warehouseRegistration as WREG where WREG.Registration_id=INSP.Registration_id)) +')' as Warehouse_name  from tbl_godown_Agreement as INSP where INSP.BranchId='" + Session["BranchId"].ToString() + "' and createdDate>'03/08/2022' order by Warehouse_name";
        SqlCommand cmd = new SqlCommand("Get_Registration_Godown_List", JVScon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegID.DataSource = ds.Tables[0];
            ddlRegID.DataTextField = "Warehouse_name";
            ddlRegID.DataValueField = "Registration_ID";
            ddlRegID.DataBind();
            ddlRegID.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
    {
       // string qry = "select MG.Godown_ID as Godownid,MG.Godown_name,Hired_Type,isnull(WHRQty,0) as WHRQty from Pvt_Warehouse_Login  as PVT inner join  tbl_metadata_godown_2018 as MG on MG.Godown_ID=PVT.Godown_Id left join (SELECT sdw.Godownid,isnull(SUM([Qty_Rvd_Weight])/10,0) as WHRQty FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('22','33','63','64','52') and srd.IssueID='NA'and sdw.CropYear='2019-20' and sdw.Godownid  in (select PT.Godown_Id from Pvt_Warehouse_Login as PT where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ) group by sdw.Godownid) as WHR on WHR.Godownid=PVT.Godown_Id where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ";
        SqlCommand cmd = new SqlCommand("Get_Agreement_capacity_Against_WHR", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegID", ddlRegID.SelectedValue.ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        trhide1.Visible = true;
        if (ds.Tables[0].Rows.Count > 0)
        {
            Depositor_Gridview.DataSource = ds;
            Depositor_Gridview.DataBind();
            lblgdrowcount.Text = "कुल WHMS गोडाउन की संख्या जो इस JVS रजिस्ट्रेशन आईडी से लिंक हे : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblgdrowcount.Text = "कुल WHMS गोडाउन की संख्या जो इस JVS रजिस्ट्रेशन आईडी से लिंक हे  : 0";
            Depositor_Gridview.DataSource = "";
            Depositor_Gridview.DataBind();
            txtagrcpt.Text = "";
            txtWHRCpt.Text = "";
        }
        GetAgreeCapacity();
    }


    private void GetAgreeCapacity()
    {
        //string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT as MD Where MD.Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
        //string qry = "select Registration_Id,AgreementCapacity,isnull(WHRQty,0)/10 as WHRQty from (select Registration_Id,sum(Agree_Capacity) as AgreementCapacity from tbl_Agreemented_Godown_Mapping_2019 as JVS where Registration_Id='" + ddlRegID.SelectedValue.ToString() + "' group by Registration_Id) as JVS left join (SELECT sdw.District_ID,sdw.BranchID,Is_W19_RegID,isnull(SUM([Qty_Rvd_Weight]),0) as WHRQty FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID inner join Pvt_Warehouse_Login as PVT on PVT.Godown_Id=sdw.Godownid where srd.Commodity_Id in ('22','33','63','64') and srd.IssueID='NA'and sdw.CropYear='2019-20' and PVT.Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' group by sdw.District_ID,sdw.BranchID,Is_W19_RegID ) as WHR on WHR.Is_W19_RegID=JVS.Registration_Id ";
        SqlCommand cmd = new SqlCommand("Get_Agreement_Capacity", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegID", ddlRegID.SelectedValue.ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtagrcpt.Text = dt.Rows[0]["AgreementCapacity"].ToString();
            txtWHRCpt.Text = dt.Rows[0]["WHRQty"].ToString();
        }
    }
}
