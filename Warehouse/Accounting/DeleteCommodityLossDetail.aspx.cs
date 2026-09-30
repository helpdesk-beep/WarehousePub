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


public partial class Accounting_DeleteCommodityLossDetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            //string region = Session["Region_ID"].ToString();
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        fillDistrict();
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                if (Session["Region_ID"].ToString() != null)
                {
                    region = Session["Region_ID"].ToString();

                }
            }
            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + region + "' order by District_Name asc";
            }
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }
    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                //ddlGodown.DataSource = null;
                //ddlGodown.DataBind();
                gv.DataSource = null;
                gv.DataBind();
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }
    public void FillGrid()
    {
       // string query = "select SB.Bill_Number,case when SB.Bill_Type='AD' then 'Daily Storage Charges Bill' when SB.Bill_Type='AU' then 'Accrued Storage Charges Bill' when SB.Bill_Type='OD' then 'Daily Over & Above Storage Charges Bill' when SB.Bill_Type='HG' then 'Godown(Hired) Rent Bill' when SB.Bill_Type='GR' then 'Godown(JVS) Rent Bill' when SB.Bill_Type='OU' then 'Accrued Over & Above Storage Charges Bill' when SB.Bill_Type='RB' then 'Reservation Bill' else '' end as Bill_Name,Bill_Type,(select Depositor_Name from tbl_MetaData_DEPOSITOR as MD where MD.Depositor_ID=SB.Depositor_Id) as Depositor_Name,CONVERT(varchar(10),SB.Created_Date,103) as DateOfBill,SB.Net_Amount from tbl_Storage_Bill_Details as SB where SB.Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "'";
        string query = "SELECT Id,[DepositorName],(select tbl_MetaData_STORAGE_COMMODITY.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY where tbl_MetaData_STORAGE_COMMODITY.Commodity_Id=[tbl_Paddy_Loss_Detail].CommodityId) as Commodity,[CropYear],[Godown],CONVERT(varchar(10),[FirstDepositDate],103) as DepositDate,([RecWeight]-[IssueWeight]) as LossQty,CONVERT(varchar(10),[LastIssueDate],103) as IssueDate FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Paddy_Loss_Detail] where [BranchId]='" + ddlDepotList.SelectedValue.ToString() + "'";

        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }
    public void FillGrid_Wheat()
    {
        string query = "select WLG_Id,Storage_Type,Deposit_CropYear,Deposit_Weight,Total_Gain,Total_Loss from tbl_WheatPSS_GainLoss_Detail where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "'";

        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridViewWheat.DataSource = ds;
            GridViewWheat.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            GridViewWheat.DataSource = null;
            GridViewWheat.DataBind();
        }
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCmdType.SelectedItem.Text == "Paddy")
        {
            FillGrid();
            trPaddy.Visible = true;
        }
        if (ddlCmdType.SelectedItem.Text == "Wheat")
        {
            FillGrid_Wheat();
            trWheat.Visible = true;
        }
       
    }
    public void Delete_Paddy()
    {
        int count = 0;
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //int i = 0;
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string LossId = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    //string Bill_Type = gv.Rows[gr2.RowIndex].Cells[2].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        ds = (DataSet)Session["ds_GridInfo"];
                        foreach (DataRow drs in ds.Tables[0].Select("Id = '" + LossId + "'"))
                        {
                            {
                                string log_qry = "insert into tbl_Paddy_Loss_Detail_Log SELECT [Id],[DistrictId],[BranchId],[RegionId],[DepositorName],[DepositorId],[CommodityId],[CropYear],[Godown],[FirstDepositDate],[RecWeight],[RecAvgMois],[RecModeOfWeight],[LastIssueDate],[IssueWeight],[IssueAvgMois],[IssueModeOfWeight],[StorageMonth],[StorageDay],[Reason],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[tbl_Paddy_Loss_Detail] where Id='" + LossId + "'";
                                cmd = new SqlCommand(log_qry, con);
                                int s = cmd.ExecuteNonQuery();
                                ////////////////deleted/////////////////////////
                                if (s > 0)
                                {
                                    qry = "Delete from tbl_Paddy_Loss_Detail where Id='" + LossId + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int d = cmd.ExecuteNonQuery();
                                    if (d > 0)
                                    {
                                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully...')", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                                }
                            }
                        }
                        count++;
                    }
                    //i = i + 1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    public void Delete_Wheat()
    {
        int count = 0;
        try
        {
            if (GridViewWheat.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //int i = 0;
                foreach (GridViewRow gr2 in GridViewWheat.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string LossId = Convert.ToString(GridViewWheat.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        ds = (DataSet)Session["ds_GridInfo"];
                        foreach (DataRow drs in ds.Tables[0].Select("WLG_Id = '" + LossId + "'"))
                        {
                            {
                                string log_qry = "insert into tbl_WheatPSS_GainLoss_Detail_Log SELECT [AId],[WLG_Id],[Region_Id],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Storage_Type],[Deposit_CropYear],[Deposit_Weight],[Total_Gain],[Total_Loss],[Created_By],[Created_Date],'" + ip + "',getdate(),[PID],[Remark] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_WheatPSS_GainLoss_Detail] where Branch_Id='" + ddlDepotList.SelectedValue.ToString() + "' and WLG_Id='" + LossId + "'";
                                cmd = new SqlCommand(log_qry, con);
                                int s = cmd.ExecuteNonQuery();
                                ////////////////deleted/////////////////////////
                                if (s > 0)
                                {
                                    qry = "Delete from tbl_WheatPSS_GainLoss_Detail where WLG_Id='" + LossId + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int d = cmd.ExecuteNonQuery();
                                    if (d > 0)
                                    {
                                        string qry2 = "Delete from tbl_WheatPSS_YearWise_Issue where WLG_Id='" + LossId + "'";
                                        cmd = new SqlCommand(qry2, con);
                                        int e = cmd.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured')", true);
                                }
                            }
                        }
                        count++;
                    }
                    //i = i + 1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                FillGrid_Wheat();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        if (ddlCmdType.SelectedItem.Text == "Paddy")
        {
            Delete_Paddy();
        }
        if (ddlCmdType.SelectedItem.Text == "Wheat")
        {
            Delete_Wheat();
        }
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlCmdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
        trPaddy.Visible = false;
        trWheat.Visible = false;
    }
}
