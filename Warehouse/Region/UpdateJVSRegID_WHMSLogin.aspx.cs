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

public partial class Region_UpdateJVSRegID_WHMSLogin : System.Web.UI.Page
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
                GetBranch();
                GetDist();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBranch()
    {
        string qry = "";
        // qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
            trhide1.Visible = false ;
            ddlRegID.DataSource = "";
            string qry = "";
            qry = "select Distinct INSP.Registration_ID,INSP.Registration_ID +' ('+ UPPER((select Warehouse_name from tbl_warehouseRegistration as WREG where WREG.Registration_id=INSP.Registration_id)) +')' as Warehouse_name  from tbl_godown_Agreement as INSP where INSP.BranchId='" + ddlBranch.SelectedValue.ToString() + "' and createdDate>'02/18/2019' order by Warehouse_name";
            SqlCommand cmd = new SqlCommand(qry, JVScon);
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
        //string qry = "select MG.Godown_ID as Godownid,MG.Godown_name,Hired_Type,isnull(WHRQty,0) as WHRQty from Pvt_Warehouse_Login  as PVT inner join  tbl_metadata_godown_2018 as MG on MG.Godown_ID=PVT.Godown_Id left join (SELECT sdw.Godownid,isnull(SUM([Qty_Rvd_Weight])/10,0) as WHRQty FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('22','33','63','64','52') and srd.IssueID='NA'and sdw.CropYear='2019-20' and sdw.Godownid  in (select PT.Godown_Id from Pvt_Warehouse_Login as PT where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ) group by sdw.Godownid) as WHR on WHR.Godownid=PVT.Godown_Id where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ";
        string qry = "select MG.Godown_ID as Godownid,MG.Godown_name,Hired_Type,isnull(WHRQty,0) as WHRQty from Pvt_Warehouse_Login  as PVT inner join  tbl_metadata_godown_2018 as MG on MG.Godown_ID=PVT.Godown_Id left join (SELECT sdw.Godownid,isnull(SUM([Qty_Rvd_Weight])/10,0) as WHRQty FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID where srd.Commodity_Id in ('22','33','63','64','52','13') and srd.IssueID='NA'and sdw.CropYear='2019-20' and sdw.Godownid  in (select PT.Godown_Id from Pvt_Warehouse_Login as PT where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ) group by sdw.Godownid) as WHR on WHR.Godownid=PVT.Godown_Id where Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' ";
        SqlCommand cmd = new SqlCommand(qry, con);
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


    private void GetDist()
    {
        //string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT as MD Where MD.Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
        string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT as MD Where MD.Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDLDistrict.DataSource = ds.Tables[0];
            DDLDistrict.DataTextField = "District_Name";
            DDLDistrict.DataValueField = "District_Id";
            DDLDistrict.DataBind();
            DDLDistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            DDLDistrict.Items.Insert(0, "--Select--");
        }
    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        // qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='"+ DDLDistrict.SelectedValue.ToString() +"' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDLBRANCH2.DataSource = ds.Tables[0];
            DDLBRANCH2.DataTextField = "DepotName";
            DDLBRANCH2.DataValueField = "BranchId";
            DDLBRANCH2.DataBind();
            DDLBRANCH2.Items.Insert(0, "--Select--");
            ddlWHMSGdwnID_SelectedIndexChanged(null,null);
        }
    }
    protected void DDLBRANCH2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDist = "select MG.Godown_ID +' ( '+ MG.Godown_Name +' )' as Godown_Name ,MG.Godown_ID from tbl_metadata_godown_2018 as MG inner join Pvt_Warehouse_Login  as PVT on PVT.Godown_Id=MG.Godown_ID  where MG.BranchID='" + DDLBRANCH2.SelectedValue.ToString() + "' and MG.Hired_Type in ('WDRA','Joint Venture(JV)') order by MG.Godown_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWHMSGdwnID.DataSource = ds.Tables[0];
            ddlWHMSGdwnID.DataTextField = "Godown_Name";
            ddlWHMSGdwnID.DataValueField = "Godown_ID";
            ddlWHMSGdwnID.DataBind();
            ddlWHMSGdwnID.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlWHMSGdwnID.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlWHMSGdwnID_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblgdid.Text = ddlWHMSGdwnID.SelectedValue;
    }
    private void GetAgreeCapacity()
    {
        //string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT as MD Where MD.Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
        string qry = "select Registration_Id,AgreementCapacity,isnull(WHRQty,0)/10 as WHRQty from (select Registration_Id,sum(Agree_Capacity) as AgreementCapacity from tbl_Agreemented_Godown_Mapping_2019 as JVS where Registration_Id='" + ddlRegID.SelectedValue.ToString() + "' group by Registration_Id) as JVS left join (SELECT sdw.District_ID,sdw.BranchID,Is_W19_RegID,isnull(SUM([Qty_Rvd_Weight]),0) as WHRQty FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Storage_Receipt_Details] as srd inner join dbo.tbl_storage_Depositor_WHR_Relation as sdw on srd.WHR_Id=sdw.Depositor_WHR_Id and srd.BranchID=sdw.BranchID inner join Pvt_Warehouse_Login as PVT on PVT.Godown_Id=sdw.Godownid where srd.Commodity_Id in ('22','33','63','64') and srd.IssueID='NA'and sdw.CropYear='2019-20' and PVT.Is_W19_RegID='" + ddlRegID.SelectedValue.ToString() + "' group by sdw.District_ID,sdw.BranchID,Is_W19_RegID ) as WHR on WHR.Is_W19_RegID=JVS.Registration_Id ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtagrcpt.Text = dt.Rows[0]["AgreementCapacity"].ToString();
            txtWHRCpt.Text = dt.Rows[0]["WHRQty"].ToString();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
            try
            {
                if (ddlWHMSGdwnID.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select WHMS Godown ID ')", true);
                }
                else if (ddlRegID.SelectedItem.Text == "--Select--")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select JVS Registartion ID ')", true);
                }
                else
                {
                    string ClienIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string log_qry = "insert into Pvt_Warehouse_Login_Log SELECT * FROM [Intergrated_MP_STORAGE].[dbo].[Pvt_Warehouse_Login] where [Godown_Id]='" + ddlWHMSGdwnID.SelectedValue.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(log_qry, con);
                    con.Open();
                    int s = cmd.ExecuteNonQuery();
                    con.Close();
                    if (s > 0)
                    {
                        con.Open();
                        string qry = "update Pvt_Warehouse_Login set Is_W19_Agreement='Y',[Is_W19_RegID]='" + ddlRegID.SelectedValue.ToString() + "',UpdateBy='" + ClienIP + "',UpdatedDate=GETDATE() where Godown_Id='" + ddlWHMSGdwnID.SelectedValue.ToString() + "'";
                        cmd = new SqlCommand(qry, con);
                        int d = cmd.ExecuteNonQuery();
                        con.Close();
                        if (d > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Updated Successfully')", true);
                            ddlRegID_SelectedIndexChanged(null, null);
                            ddlWHMSGdwnID.SelectedIndex = -1;
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error')", true);
            }
            finally
            {
                con.Close();
            }
    }
}
