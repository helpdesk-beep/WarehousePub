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
using System.Text;

public partial class WarehouseLevel_StackMaster_W : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
    string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["GodownID_New"] as string))
        {
            try
            {
                txtStackNumber.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
                txt_Remarks.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
                txtStackQty.Attributes.Add("onkeypress", "return CheckIsNumeric(this);");
                btnUpdate.Attributes.Add("onclick", "chkgod();");

                if (Session["lang"].ToString() == "Hindi")
                {
                    //lblStackMaster.Text = Resources.hindi.lblStackMaster;
                    Label3.Text = Resources.hindi.godawonname;
                    Label4.Text = Resources.hindi.godawonname;
                    lbl_ParentStack.Text = Resources.hindi.lbl_ParentStack;
                    Label6.Text = Resources.hindi.stackname;
                    lbl_Remarks.Text = Resources.hindi.remark;
                    Label7.Text = Resources.hindi.stackcap;
                    Label8.Text = Resources.hindi.godawontype;
                    Label9.Text = Resources.hindi.storagetype;
                    btnAddNew.Text = Resources.hindi.btnaddnew;
                }

                if (!IsPostBack)
                {
                    if (Session["G_BranchId"].ToString() != "")
                    {
                        string depotId = Session["G_BranchId"].ToString();
                        GetGodown(depotId);
                        GetCommodity();
                        GetStack(depotId, Session["GodownId"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void GetStack(string depotid, string GodownId)

    {
        try
        {
            string BranchId = Session["G_BranchID"].ToString();
            stack_GridView.DataSource = null;
            stack_GridView.DataBind();
            //qry = "select TMS.Stack_ID,TMS.Stack_Name,TMS.Godown_ID,TMS.Commodity_Id,TMG.Godown_Name,TMSC.Commodity_Name,TMS.Hired_type,TMS.Storage_Type,(select case when (convert(decimal(18,4),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID and Godown_ID='" + GodownId + "') a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL') b))) > 0  THEN 'Stacked' ELSE 'Empty' END)AS 'StackingStatus',convert(decimal(18,2),TMS.Stack_capacity) as Stack_capacity,convert(decimal(18,4),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID and Godown_ID='" + GodownId + "') a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID and tbl_Delivery_Stacking_Details_GatePass.Godown_ID='" + GodownId + "' AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS join tbl_MetaData_GODOWN as TMG on TMS.Godown_ID = TMG.Godown_ID JOIN tbl_MetaData_STORAGE_COMMODITY AS TMSC on TMS.Commodity_Id = TMSC.Commodity_Id where TMS.BranchId = '" + BranchId + "' and TMS.Stack_Killed = 'N' and TMS.Godown_ID='" + GodownId + "' order by TMS.Stack_ID";
            //qry = "select TMS.Stack_ID,TMS.Stack_Name,TMS.Godown_ID,TMS.Commodity_Id,MDG.Godown_Name,CMD.Commodity_Name,MDG.Hired_Type,MDG.Storage_Type,(case when RecWeight-(IssueWeight+Loss-Gain)=0 then 'Empty' when RecWeight-(IssueWeight+Loss-Gain) IS NULL then 'Empty' else 'Stacked' end ) as StackingStatus ,Stack_capacity,isnull(RecWeight-(IssueWeight+Loss-Gain),0) as Current_Capacity from tbl_MetaData_STACK as TMS left join(select RecDetail.stack_id,RecWeight,isnull(IssueWeight,0)as IssueWeight,isnull(Loss,0)as Loss,isnull(Gain,0)as Gain from( select SSD.stack_id,SUM(Weight) as RecWeight,sum(Bags) as RecBags from tbl_storage_Stacking_Details as SSD where BranchID='" + BranchId + "'  and Godown_ID='" + GodownId + "' group by SSD.stack_id) as RecDetail left join( select DSD.Stack_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and DSD.Stack_ID in ( select SSD.stack_id from tbl_storage_Stacking_Details as SSD where BranchID='" + BranchId + "' and Godown_ID='" + GodownId + "') group by DSD.Stack_ID ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID ) as RedDel on TMS.Stack_ID=RedDel.Stack_ID inner join tbl_MetaData_STORAGE_COMMODITY as CMD on CMD.Commodity_Id=TMS.Commodity_Id  join tbl_MetaData_GODOWN as MDG on MDG.Godown_ID=TMS.Godown_ID  where TMS.Godown_ID='" + GodownId + "' and MDG.BranchID='" + BranchId + "' and TMS.Stack_Killed='N'";
            //qry = "select TMS.Stack_ID,TMS.Stack_Name,TMS.Godown_ID,TMS.Commodity_Id,MDG.Godown_Name,CMD.Commodity_Name,MDG.Hired_Type,MDG.Storage_Type,(case when RecWeight-(IssueWeight+Loss-Gain)=0 then 'Empty' when RecWeight-(IssueWeight+Loss-Gain) IS NULL then 'Empty' else 'Stacked' end ) as StackingStatus ,Stack_capacity,isnull(RecWeight-(IssueWeight+Loss-Gain),0) as Current_Capacity from tbl_MetaData_STACK as TMS left join(select RecDetail.stack_id,RecWeight,isnull(IssueWeight,0)as IssueWeight,isnull(Loss,0)as Loss,isnull(Gain,0)as Gain from( select SSD.stack_id,SUM(Weight) as RecWeight,sum(Bags) as RecBags from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "' group by SSD.stack_id) as RecDetail left join( select DSD.Stack_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and DSD.Stack_ID in ( select SSD.stack_id from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "') group by DSD.Stack_ID ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID ) as RedDel on TMS.Stack_ID=RedDel.Stack_ID inner join tbl_MetaData_STORAGE_COMMODITY as CMD on CMD.Commodity_Id=TMS.Commodity_Id  join tbl_MetaData_GODOWN as MDG on MDG.Godown_ID=TMS.Godown_ID  where TMS.Godown_ID='" + GodownId + "' and TMS.Stack_Killed='N'";
            // qry = "select TMS.Stack_ID,TMS.Stack_Name,TMS.Godown_ID,TMS.Commodity_Id,MDG.Godown_Name,CMD.Commodity_Name,MDG.Hired_Type,MDG.Storage_Type,(case when RecWeight-(IssueWeight+Loss-Gain)=0 then 'Empty' when RecWeight-(IssueWeight+Loss-Gain) IS NULL then 'Empty' else 'Stacked' end ) as StackingStatus ,Stack_capacity,isnull(RecWeight-(IssueWeight+Loss-Gain),0) as Current_Capacity from tbl_MetaData_STACK as TMS left join(select RecDetail.stack_id,RecWeight,isnull(IssueWeight,0)as IssueWeight,isnull(Loss,0)as Loss,isnull(Gain,0)as Gain from( select SSD.stack_id,SUM(Weight) as RecWeight,sum(Bags) as RecBags from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "' group by SSD.stack_id) as RecDetail left join( select DSD.Stack_ID,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and DSD.Stack_ID in ( select SSD.stack_id from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "') group by DSD.Stack_ID ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID ) as RedDel on TMS.Stack_ID=RedDel.Stack_ID inner join tbl_MetaData_STORAGE_COMMODITY as CMD on CMD.Commodity_Id=TMS.Commodity_Id  join tbl_MetaData_GODOWN as MDG on MDG.Godown_ID=TMS.Godown_ID  where TMS.Godown_ID='" + GodownId + "' and TMS.Stack_Killed='N'";
            qry = "select TMS.Stack_ID,TMS.Stack_Name, (select case when TMS.MarketingSeason = '1' then 'KMS' when TMS.MarketingSeason = '2' then 'RMS' else '' end) as MarketingSeason, (select case when TMS.BagType = '1' then 'SBT(580)' when TMS.BagType = '2' then 'SBT' when TMS.BagType = '3' then 'HDPE' else '' end) as BagType, TMS.CropYear ,TMS.Godown_ID,TMS.Commodity_Id,MDG.Godown_Name, CMD.Commodity_Name,MDG.Hired_Type,MDG.Storage_Type,(case when RecWeight-(IssueWeight+Loss-Gain)=0 then 'Empty' when RecWeight-(IssueWeight+Loss-Gain) IS NULL then 'Empty' else 'Stacked' end ) as StackingStatus ,Stack_capacity,isnull(RecWeight-(IssueWeight+Loss-Gain),0) as Current_Capacity from tbl_MetaData_STACK as TMS left join(select RecDetail.stack_id,RecWeight,isnull(IssueWeight,0)as IssueWeight,isnull(Loss,0)as Loss,isnull(Gain,0)as Gain from( select SSD.stack_id,SUM(Weight) as RecWeight,sum(Bags) as RecBags from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "' group by SSD.stack_id) as RecDetail left join( select DSD.Stack_ID,SUM(DSD.No_Of_Bags) as IssueBags, SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and DSD.Stack_ID in ( select SSD.stack_id from tbl_storage_Stacking_Details as SSD where Godown_ID='" + GodownId + "') group by DSD.Stack_ID ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID ) as RedDel on TMS.Stack_ID=RedDel.Stack_ID inner join tbl_MetaData_STORAGE_COMMODITY as CMD on CMD.Commodity_Id=TMS.Commodity_Id join tbl_MetaData_GODOWN as MDG on MDG.Godown_ID=TMS.Godown_ID where TMS.Godown_ID='" + GodownId + "' and TMS.Stack_Killed='N'";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsStack"] = ds;
                Session["Stckinfo"] = ds;
                stack_GridView.DataSource = ds.Tables[0];
                stack_GridView.DataBind();
                lblRowCount.Text = "Total records are : " + stack_GridView.Rows.Count.ToString();
            }

            else
            {
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    private void GetCommodity()
    {
        try
        {
            qry = "select * from dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dprlst_Commodity.DataSource = ds.Tables[0];
                dprlst_Commodity.DataValueField = "Commodity_Id";
                dprlst_Commodity.DataTextField = "Commodity_Name";
                dprlst_Commodity.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    private void GetGodown(string depotId)
    {
        try
        {
            string BranchId = Session["G_BranchID"].ToString();
            qry = "select * from dbo.tbl_MetaData_GODOWN_2018 where BranchId='" + BranchId + "' and Godown_ID='" + Session["GodownId"] + "'";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dprlst_Godown.DataSource = ds.Tables[0];
                dprlst_Godown.DataValueField = "Godown_ID";
                dprlst_Godown.DataTextField = "Godown_Name";
                dprlst_Godown.DataBind();
                // dprlst_Godown.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        PanelStack.Visible = false;
        btnAddNew.Visible = true;
        btn_Close.Visible = true;
        lblMsg.Text = "";
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {

        Newstackhead.InnerText = "Fill New Stack Details";
        dprlst_Hired.Enabled = true;
        dprlst_Storage.Enabled = true;
        dprlst_Godown.Enabled = true;
        dprlst_Godown.SelectedIndex = 0;
        txtStackQty.Text = "";
        txtStackNumber.Enabled = true;
        txtStackNumber.Text = "";
        drplst_ParentStack.Enabled = true;
        dprlst_Commodity.Enabled = true;
        Label4.Visible = true;
        dprlst_Commodity.Visible = true;
        btnAddNew.Visible = false;
        btn_Close.Visible = false;
        PanelStack.Visible = true;
        txtStackQty.Text = "2000";
        btnUpdate.Text = "Insert";
        drplst_ParentStack.Visible = true;
        lbl_ParentStack.Visible = true;
        drplst_ParentStack.Items.Insert(0, "Base Stack");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlCropType.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Marketing Season')", true);
        }
        else if (ddlBagType.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Bags Type')", true);
        }
        else if (ddlCropYear.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Crop Year')", true);
        }
        else
        {
            try
            {
                string stack_id = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string godownid = dprlst_Godown.SelectedValue.ToString();
                string capacity = txtStackQty.Text.Trim().ToString();
                string stackname = txtStackNumber.Text.Trim().ToString();
                string hired = dprlst_Hired.SelectedValue.ToString();
                string storage = dprlst_Storage.SelectedValue.ToString();
                string commodity = dprlst_Commodity.SelectedValue.ToString();
                string distid = Session["Depot_DistID"].ToString();
                string depotId = Session["G_DepotID"].ToString();
                string BranchId = Session["G_BranchID"].ToString();
                decimal Godowncapacity = 0;
                decimal Sumofstackcap = 0;
                decimal Allowstackcap = 0;
                con.Open();
                sqltran = con.BeginTransaction();
                //qry = "IF EXISTS (select * from tbl_metadata_stack where Godown_ID='" + godownid + "')BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "' and Stack_Killed='N' group by GD.Godown_Capacity,GD.Godown_ID END ELSE BEGIN select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity,ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from tbl_MetaData_GODOWN as GD LEFT JOIN tbl_MetaData_STACK as ST ON GD.Godown_ID = ST.Godown_ID where gd.Godown_ID='" + godownid + "' and gd.DepotId='" + depotId + "'  group by GD.Godown_Capacity,GD.Godown_ID END";
                //qry = "select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity ,[Storage_Type],(select ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from dbo.tbl_MetaData_STACK as ST where Godown_ID='" + godownid + "'  and Stack_Killed='N' and ST.BranchId='" + BranchId + "') as Stack_capacity from tbl_MetaData_GODOWN as GD where Godown_ID='" + godownid + "' and gd.BranchId='" + BranchId + "'";
                qry = "select convert(decimal(18,2),GD.Godown_Capacity) as Godown_Capacity ,[Storage_Type],(select ISNULL(convert(decimal(18,2),sum(ST.Stack_capacity)),0) as Stack_capacity from dbo.tbl_MetaData_STACK as ST where Godown_ID='" + godownid + "'  and Stack_Killed='N' and ST.BranchId='" + BranchId + "') as Stack_capacity from tbl_MetaData_GODOWN_2018 as GD where Godown_ID='" + godownid + "' and gd.BranchId='" + BranchId + "'";

                cmd = new SqlCommand(qry, con, sqltran);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Godowncapacity = Convert.ToDecimal(ds.Tables[0].Rows[0]["Godown_Capacity"].ToString());
                    Sumofstackcap = Convert.ToDecimal(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
                }
                Allowstackcap = Godowncapacity - Sumofstackcap;
                if (btnUpdate.Text == "Insert")
                {
                    if (Allowstackcap >= Convert.ToDecimal(capacity))
                    {
                        if (ds.Tables[0].Rows[0]["Stack_capacity"].ToString() != "SteelSilo")
                        {
                            if (Convert.ToDouble(txtStackQty.Text) <= 2000)
                            {
                                qry = "select isnull(Max(Stack_ID),0) from tbl_MetaData_STACK where BranchId='" + BranchId + "' and Godown_ID='" + godownid + "' ";
                                cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
                                string str3 = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt64(str3) != 0)
                                {
                                    stack_id = Convert.ToString(Convert.ToInt64(str3) + 1);
                                    lbl_stackid.Text = stack_id;
                                }
                                else
                                {
                                    stack_id = godownid + "0001";
                                    lbl_stackid.Text = stack_id;
                                }

                                cmd = new SqlCommand("sp_insertStackMaster_New", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Stack_ID", stack_id);
                                cmd.Parameters.AddWithValue("@DepotId", depotId);
                                cmd.Parameters.AddWithValue("@District_Id", distid);
                                cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                                cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                                cmd.Parameters.AddWithValue("@Commodity_Id", commodity);
                                cmd.Parameters.AddWithValue("@Category_Id", "1");
                                if (capacity != "")
                                {
                                    cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                                }

                                if (drplst_ParentStack.SelectedItem.Text == "Base Stack")
                                {
                                    cmd.Parameters.AddWithValue("@Parant_Stack_ID", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Parant_Stack_ID", drplst_ParentStack.SelectedItem.Value.ToString());
                                    cmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text.Trim());
                                }
                                cmd.Parameters.AddWithValue("@Storage_Type", dprlst_Storage.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Hired_type", dprlst_Hired.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@CreatedBy", ip);
                                cmd.Parameters.AddWithValue("@BranchId", BranchId);
                                cmd.Parameters.AddWithValue("@MarketingSeason", ddlCropType.SelectedValue);
                                cmd.Parameters.AddWithValue("@BagType", ddlBagType.SelectedValue);
                                cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                                int res = cmd.ExecuteNonQuery();
                                if (res > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record saved Successfully..')", true);
                                    drplst_ParentStack.SelectedItem.Selected = false;
                                    drplst_ParentStack.Items.Clear();
                                    drplst_ParentStack.Visible = false;
                                    lbl_ParentStack.Visible = false;
                                    lbl_Remarks.Visible = false;
                                    txtStackQty.Text = "";
                                    txtStackNumber.Text = "";
                                    txt_Remarks.Visible = false;
                                    btnAddNew.Visible = true;
                                    btn_Close.Visible = true;
                                    PanelStack.Visible = false;
                                    lblMsg.Text = "";
                                    lbl.Visible = true;
                                    lbl_stackid.Visible = true;
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved,Stack Name Already Exits..')", true);
                                }
                                sqltran.Commit();
                                GetStack(depotId, Session["GodownId"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than 2000 Qntl ')", true);
                            }
                        }
                        else
                        {
                            qry = "select isnull(Max(Stack_ID),0) from tbl_MetaData_STACK where BranchId='" + BranchId + "' and Godown_ID='" + godownid + "' ";
                            cmd = new SqlCommand(qry, con, sqltran); // check WhrId present in whr_status table
                            string str3 = cmd.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str3) != 0)
                            {
                                stack_id = Convert.ToString(Convert.ToInt64(str3) + 1);
                                lbl_stackid.Text = stack_id;
                            }
                            else
                            {
                                stack_id = godownid + "0001";
                                lbl_stackid.Text = stack_id;
                            }

                            cmd = new SqlCommand("sp_insertStackMaster_New", con, sqltran);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Stack_ID", stack_id);
                            cmd.Parameters.AddWithValue("@DepotId", depotId);
                            cmd.Parameters.AddWithValue("@District_Id", distid);
                            cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                            cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                            cmd.Parameters.AddWithValue("@Commodity_Id", commodity);
                            cmd.Parameters.AddWithValue("@Category_Id", "1");
                            if (capacity != "")
                            {
                                cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                            }

                            if (drplst_ParentStack.SelectedItem.Text == "Base Stack")
                            {
                                cmd.Parameters.AddWithValue("@Parant_Stack_ID", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Parant_Stack_ID", drplst_ParentStack.SelectedItem.Value.ToString());
                                cmd.Parameters.AddWithValue("@Remarks", txt_Remarks.Text.Trim());
                            }
                            cmd.Parameters.AddWithValue("@Storage_Type", dprlst_Storage.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Hired_type", dprlst_Hired.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@CreatedBy", ip);
                            cmd.Parameters.AddWithValue("@BranchId", BranchId);
                            int res = cmd.ExecuteNonQuery();
                            if (res > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record saved Successfully..')", true);

                                drplst_ParentStack.SelectedItem.Selected = false;
                                drplst_ParentStack.Items.Clear();
                                drplst_ParentStack.Visible = false;
                                lbl_ParentStack.Visible = false;
                                lbl_Remarks.Visible = false;
                                txtStackQty.Text = "";
                                txtStackNumber.Text = "";
                                txt_Remarks.Visible = false;
                                btnAddNew.Visible = true;
                                btn_Close.Visible = true;
                                PanelStack.Visible = false;
                                lblMsg.Text = "";
                                lbl.Visible = true;
                                lbl_stackid.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Saved,Stack Name Already Exits..')", true);
                            }
                            sqltran.Commit();
                            GetStack(depotId, Session["GodownId"].ToString());
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + Allowstackcap.ToString() + "')", true);
                        lblMsg.Text = "Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + Allowstackcap.ToString() + "(Qtls.kgsgms)";
                    }
                }
                else if (btnUpdate.Text == "Update")
                {
                    decimal alowcptupdtmesg = Godowncapacity - (Sumofstackcap - Convert.ToDecimal(lblselectedstkcpt.Text));
                    decimal actualcpt = Sumofstackcap - Convert.ToDecimal(lblselectedstkcpt.Text) + Convert.ToDecimal(capacity);
                    if (Convert.ToDecimal(Session["ChkStackcpt"].ToString()) > Convert.ToDecimal(capacity))
                    {
                        string stackid = Session["Stackid"].ToString();
                        qry = "select convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight)+sum(Loss)-sum(Gain) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS WHERE TMS.Stack_ID = '" + stackid + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        string str4 = cmd.ExecuteScalar().ToString();
                        Sumofstackcap = Convert.ToDecimal(str4);
                        if (Sumofstackcap <= Convert.ToDecimal(capacity))
                        {
                            qry = "Insert Into tbl_MetaData_STACK_UpdateLog SELECT * from tbl_MetaData_STACK where Stack_ID='" + stackid + "' and BranchId = '" + BranchId + "'";
                            cmd = new SqlCommand(qry, con, sqltran);
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0)
                            {
                                cmd = new SqlCommand("sp_stack_update_New", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Stack_ID", stackid);
                                int _status = 0;
                                if (txtStackNumber.Text.Trim() != "")
                                {
                                    cmd.Parameters.AddWithValue("@status", _status);
                                }
                                else
                                {
                                    _status = 1;
                                    cmd.Parameters.AddWithValue("@status", _status);
                                }
                                cmd.Parameters.AddWithValue("@DepotId", depotId);
                                cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                                cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                                cmd.Parameters.AddWithValue("@Commodity_Id", commodity);
                                cmd.Parameters.AddWithValue("@Category_Id", "1");
                                if (capacity != "")
                                {
                                    cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                                }
                                cmd.Parameters.AddWithValue("@Storage_Type", dprlst_Storage.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Hired_type", dprlst_Hired.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                                cmd.Parameters.AddWithValue("@MarketingSeason", ddlCropType.SelectedValue);
                                cmd.Parameters.AddWithValue("@BagType", ddlBagType.SelectedValue);
                                cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                                int res = cmd.ExecuteNonQuery();
                                if (res > 0)
                                {
                                    btnAddNew.Visible = true;
                                    btn_Close.Visible = true;
                                    PanelStack.Visible = false;
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated Successfully')", true);
                                }
                            }
                            sqltran.Commit();
                            GetStack(depotId, Session["GodownId"].ToString());
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Less then Available STock Capacity')", true);
                        }
                    }
                    else if (Session["ChkStkSatust"].ToString() == "Empty")
                    {
                        if (actualcpt <= Godowncapacity)
                        {
                            if (Convert.ToDecimal(capacity) <= 2000)
                            {
                                if (Allowstackcap >= 0 && alowcptupdtmesg >= Convert.ToDecimal(capacity))
                                {
                                    string stackid = Session["Stackid"].ToString();
                                    qry = "select convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight)+sum(Loss)-sum(Gain) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS WHERE TMS.Stack_ID = '" + stackid + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    string str4 = cmd.ExecuteScalar().ToString();
                                    Sumofstackcap = Convert.ToDecimal(str4);
                                    if (Sumofstackcap <= Convert.ToDecimal(capacity))
                                    {
                                        qry = "Insert Into tbl_MetaData_STACK_UpdateLog SELECT * from tbl_MetaData_STACK where Stack_ID='" + stackid + "' and BranchId = '" + BranchId + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int a = cmd.ExecuteNonQuery();
                                        if (a > 0)
                                        {
                                            cmd = new SqlCommand("sp_stack_update_New", con, sqltran);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Stack_ID", stackid);
                                            int _status = 0;
                                            if (txtStackNumber.Text.Trim() != "")
                                            {
                                                cmd.Parameters.AddWithValue("@status", _status);
                                            }
                                            else
                                            {
                                                _status = 1;
                                                cmd.Parameters.AddWithValue("@status", _status);
                                            }
                                            cmd.Parameters.AddWithValue("@DepotId", depotId);
                                            cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                                            cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                                            cmd.Parameters.AddWithValue("@Commodity_Id", commodity);
                                            cmd.Parameters.AddWithValue("@Category_Id", "1");
                                            if (capacity != "")
                                            {
                                                cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                                            }
                                            cmd.Parameters.AddWithValue("@Storage_Type", dprlst_Storage.SelectedItem.Text);
                                            cmd.Parameters.AddWithValue("@Hired_type", dprlst_Hired.SelectedItem.Text);
                                            cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                                            cmd.Parameters.AddWithValue("@MarketingSeason", ddlCropType.SelectedValue);
                                            cmd.Parameters.AddWithValue("@BagType", ddlBagType.SelectedValue);
                                            cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);

                                            int res = cmd.ExecuteNonQuery();
                                            if (res > 0)
                                            {
                                                btnAddNew.Visible = true;
                                                btn_Close.Visible = true;
                                                PanelStack.Visible = false;
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated Successfully')", true);
                                            }
                                        }
                                        sqltran.Commit();
                                        GetStack(depotId, Session["GodownId"].ToString());
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Less then Available STock Capacity')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + alowcptupdtmesg.ToString() + "')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Greater then 2000 Qntl. ')", true);
                            }
                        }
                        else if (Session["ChkSackCmd"].ToString() != commodity && Session["ChkStkSatust"].ToString() == "Empty" && Convert.ToDecimal(lblselectedstkcpt.Text) == Convert.ToDecimal(capacity))
                        {
                            string stackid = Session["Stackid"].ToString();
                            qry = "select convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight)+sum(Loss)-sum(Gain) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS WHERE TMS.Stack_ID = '" + stackid + "'";
                            cmd = new SqlCommand(qry, con, sqltran);
                            string str4 = cmd.ExecuteScalar().ToString();
                            Sumofstackcap = Convert.ToDecimal(str4);
                            if (Sumofstackcap <= Convert.ToDecimal(capacity))
                            {
                                qry = "Insert Into tbl_MetaData_STACK_UpdateLog SELECT * from tbl_MetaData_STACK where Stack_ID='" + stackid + "' and BranchId = '" + BranchId + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int a = cmd.ExecuteNonQuery();
                                if (a > 0)
                                {
                                    string qry1 = "update tbl_metadata_stack set Commodity_Id='" + dprlst_Commodity.SelectedValue.ToString() + "' ,  Category_Id='1' , UpdatedBy='" + ip + "' , UpdatedDate=GETDATE() where Stack_ID='" + stackid + "' and  BranchId = '" + BranchId + "' and Godown_ID='" + Session["GodownId"].ToString() + "'";
                                    cmd = new SqlCommand(qry1, con, sqltran);
                                    int a1 = cmd.ExecuteNonQuery();
                                    if (a1 > 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Update Commodity Successfully')", true);
                                    }
                                }
                                sqltran.Commit();
                                GetStack(depotId, Session["GodownId"].ToString());
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Greater then Godown capacity')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sum Of Stack capacity is Greater then Godown capacity , The Maximum allowed capacity is =" + alowcptupdtmesg.ToString() + " or You can Update Only Commodity If stack is Empty')", true);

                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sum Of Stack capacity is Greater then Godown capacity  You Can Change Only Commodity If Stack Is Empty')", true);
                        }
                    }
                    else if (Session["ChkStkSatust"].ToString() == "Stacked")
                    {
                        if (Convert.ToDecimal(capacity) <= 2000)
                        {
                            if (Allowstackcap >= 0 && alowcptupdtmesg >= Convert.ToDecimal(capacity))
                            {
                                string stackid = Session["Stackid"].ToString();
                                // qry = "select convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS WHERE TMS.Stack_ID = '" + stackid + "'";
                                //update on 28/12/2018
                                qry = "select convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight)+SUM(Loss)-SUM(Gain) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS WHERE TMS.Stack_ID = '" + stackid + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                string str4 = cmd.ExecuteScalar().ToString();
                                Sumofstackcap = Convert.ToDecimal(str4);
                                if (Sumofstackcap <= Convert.ToDecimal(capacity))
                                {
                                    qry = "Insert Into tbl_MetaData_STACK_UpdateLog SELECT * from tbl_MetaData_STACK where Stack_ID='" + stackid + "' and BranchId = '" + BranchId + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int a = cmd.ExecuteNonQuery();
                                    if (a > 0)
                                    {
                                        cmd = new SqlCommand("sp_stack_update_New", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@Stack_ID", stackid);
                                        int _status = 0;
                                        if (txtStackNumber.Text.Trim() != "")
                                        {
                                            cmd.Parameters.AddWithValue("@status", _status);
                                        }
                                        else
                                        {
                                            _status = 1;
                                            cmd.Parameters.AddWithValue("@status", _status);
                                        }
                                        cmd.Parameters.AddWithValue("@DepotId", depotId);
                                        cmd.Parameters.AddWithValue("@Godown_ID", godownid);
                                        cmd.Parameters.AddWithValue("@Stack_Name", stackname);
                                        cmd.Parameters.AddWithValue("@Commodity_Id", commodity);
                                        cmd.Parameters.AddWithValue("@Category_Id", "1");
                                        if (capacity != "")
                                        {
                                            cmd.Parameters.AddWithValue("@Stack_capacity", Convert.ToDecimal(capacity));
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Stack_capacity", 0);
                                        }
                                        cmd.Parameters.AddWithValue("@Storage_Type", dprlst_Storage.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@Hired_type", dprlst_Hired.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                                        cmd.Parameters.AddWithValue("@MarketingSeason", ddlCropType.SelectedValue);
                                        cmd.Parameters.AddWithValue("@BagType", ddlBagType.SelectedValue);
                                        cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                                        //cmd.Parameters.AddWithValue("@MarketingSeason", ddlCropType.SelectedValue);
                                        //cmd.Parameters.AddWithValue("@BagType", ddlBagType.SelectedValue);
                                        //cmd.Parameters.AddWithValue("@CropYear", ddlCropYear.SelectedValue);
                                        int res = cmd.ExecuteNonQuery();
                                        if (res > 0)
                                        {
                                            btnAddNew.Visible = true;
                                            btn_Close.Visible = true;
                                            PanelStack.Visible = false;
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record updated Successfully')", true);
                                        }
                                    }
                                    sqltran.Commit();
                                    GetStack(depotId, Session["GodownId"].ToString());
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Less then Available STock Capacity')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Can Not Insert Capacity More Than Gowdown Capacity ,The Maximum allowed capacity is =" + alowcptupdtmesg.ToString() + "')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack capacity Should not be Greater then 2000 Qntl. ')", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                //lblMsg.Text = "Stack record not found,please try again";
                lblMsg.Text = ex.Message;
            }
            finally
            {
                sqltran.Dispose();
                con.Close();
            }
        }
           
    }

    Int32 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = int.Parse(ValS);
        return ValF;
    }

    float CheckFloat(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        float ValF = float.Parse(ValS);
        return ValF;
    }

    protected void dprlst_Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        string godownid = Session["GodownId"].ToString();
        string commodity = dprlst_Commodity.SelectedValue;
        GetParentStack(godownid, commodity, "1");
    }

    private void GetParentStack(string godownid, string commodity, string category)
    {
        qry = "SELECT distinct Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + godownid + "') AND (Commodity_Id = '" + commodity + "')";
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            drplst_ParentStack.Items.Clear();
            drplst_ParentStack.DataSource = ds.Tables[0];
            drplst_ParentStack.DataValueField = "Stack_ID";
            drplst_ParentStack.DataTextField = "Stack_Name";
            drplst_ParentStack.DataBind();
            drplst_ParentStack.Items.Insert(0, "--Select--");
        }
    }

    protected void stack_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        string stackid = stack_GridView.DataKeys[row.RowIndex].Value.ToString();
        Session["Stackid"] = stackid;
        Session["ChkSackCmd"] = row.Cells[13].Text.Trim().ToString();
        Session["ChkStkSatust"] = row.Cells[7].Text.Trim().ToString();
        Session["ChkStackcpt"] = row.Cells[8].Text.Trim().ToString();
        if (e.CommandName == "Updates")
        {
            if (stackid != "")
            {
                row.BackColor = System.Drawing.Color.IndianRed;
                row.ForeColor = System.Drawing.Color.WhiteSmoke;
                btnUpdate.Text = "Update";
                Newstackhead.InnerText = "Update Stack Information";
                btnAddNew.Visible = false;
                btn_Close.Visible = false;
                PanelStack.Visible = true;
                string depotId = Session["G_DepotID"].ToString();
                dprlst_Godown.SelectedValue = row.Cells[12].Text.Trim().ToString();
                dprlst_Godown.Enabled = false;
                dprlst_Commodity.SelectedValue = row.Cells[13].Text.Trim().ToString();
                dprlst_Storage.SelectedItem.Text = row.Cells[10].Text.Trim().ToString();
                dprlst_Hired.SelectedItem.Text = row.Cells[11].Text.Trim().ToString();
                txtStackNumber.Text = row.Cells[5].Text.Trim().ToString();
                txtStackQty.Text = row.Cells[8].Text.Trim().ToString();
                lblsckstatus.Text = row.Cells[7].Text.Trim().ToString();
                lblselectedstkcpt.Text = row.Cells[8].Text.Trim().ToString();
                if (lblsckstatus.Text == "Stacked")
                {
                    if (dprlst_Commodity.SelectedValue == "1")
                    {
                        dprlst_Commodity.Enabled = true;
                    }
                    else
                    {
                        dprlst_Commodity.Enabled = false;
                    }
                    drplst_ParentStack.Enabled = false;
                    dprlst_Storage.Enabled = false;
                    dprlst_Hired.Enabled = false;
                }
            }
        }
        else if (e.CommandName == "Deletes")
        {
            lblsckstatus.Text = row.Cells[7].Text.Trim().ToString();
            if (lblsckstatus.Text == "Stacked")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sorry you can not delete.Because Stack have some commodity quantity.')", true);
                lblsckstatus.Text = "";
                return;
            }
            try
            {
                if (con != null)
                {
                    con.Open();
                    string depotId = Session["Depot_DepotID"].ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string BranchId = Session["BranchID"].ToString();
                    ////////////////////////for log ////////////////////////////
                    string qrylog = "Insert Into tbl_MetaData_STACK_Log SELECT [Stack_ID],[State_Id],[District_Id],[DepotId],[Godown_ID],[Commodity_Id],[Category_Id],[Stack_Name],[Stack_Formation_date],[Storage_Type],[Stack_capacity],[Stack_Killed],[Stack_Killed_Date],[NO_OF_BAG_OB],[Net_Weight_BAG_OB],[NO_OF_BAG_REceived],[Net_Weight_BAG_Received],[NO_OF_BAG_Issued],[Net_Weight_BAG_Issued],[NO_OF_BAG_Despatched],[Net_Weight_BAG_Despatched],[NO_OF_Made_UP_BAGS],[NO_OF_Real_BAGS],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[Parant_Stack_ID],[Hired_type],BranchId from tbl_MetaData_STACK where Stack_ID='" + stackid + "' and BranchID = '" + BranchId + "' ";
                    SqlCommand cmd = new SqlCommand(qrylog, con);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        string query = "update dbo.tbl_MetaData_STACK set Stack_Killed='Y' where Stack_ID='" + stackid + "' and BranchID = '" + BranchId + "'";
                        SqlCommand cmd1 = new SqlCommand(query, con);
                        int res = cmd1.ExecuteNonQuery();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack deleted Successfully......')", true);
                        GetStack(depotId, Session["GodownId"].ToString());
                        btnUpdate.Text = "";
                        PanelStack.Visible = false;
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Stack Not Deleted')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void dprlst_Commodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lblsckstatus.Text == "Stacked")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Sorry you can not change the commodity. because stack have another commodity.')", true);
            return;
        }
    }
    protected void fillFinancialYear()
    {
        ddlCropYear.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlCropYear.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlCropYear.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlCropYear.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        //ddlFyear.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        //ddlFyear.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
    }
}
