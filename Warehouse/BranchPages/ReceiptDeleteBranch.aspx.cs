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

public partial class BranchPages_ReceiptDeleteBranch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Depot_DepotID"].ToString() != "")
        //{
            //try
            //{
                if (!IsPostBack)
                {
                    Btn_Delete.Enabled = false;
                  
                    RadioButton1.Checked = true;
                 
                    FillGridBranch();
                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }
                    // fillDistrict();
                    Btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
                }
            //}
            //catch (Exception ex)
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            //}
        //}
        //else
        //{
        //    Response.Redirect("~/SessionExpired.htm");
        //}
    }

    public void FillGridBranch()
    {
        //string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,AST.Challan_No,AST.Truck_No,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.DepotId = '" + ddlDepotList.SelectedValue.ToString() + "' AND AST.District_Id = '" + ddlDistrict.SelectedValue.ToString() + "' ORDER BY AST.ArrivalStock_Id";
        //oldest  string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.Depotid = '" + ddlDepotList.SelectedValue.ToString() + "' and WHR.District_Id = '" + ddlDistrict.SelectedValue.ToString() + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' AND tbl_Storage_Receipt_Details.WHR_Flag = 'Y' order by WHR.Depositor_WHR_Id asc";

        string query = "select  DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,(select Commodity_Name from  tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=srd.Commodity_Id) as Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate from  tbl_Storage_Receipt_Details as srd inner join tbl_Storage_Arrival_Stock as AST on srd.StorageReceipt_Id=AST.Receipt_ID where srd.BranchID='" + Session["BranchID"].ToString() + "' and WHR_Id is null";
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
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
                    sqltran = con.BeginTransaction();
                    foreach (GridViewRow gr2 in gv.Rows)
                    {
                        DataSet ds = (DataSet)Session["ds_GridInfo"];
                        CheckBox chk_Delete = new CheckBox();
                        string ArrivalStock_Id = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                        if (chk_Delete.Checked == true)
                        {
                            ds = (DataSet)Session["ds_GridInfo"];
                            foreach (DataRow drs in ds.Tables[0].Select("ArrivalStock_Id = '" + ArrivalStock_Id + "'"))
                            {
                                string Receiptid = drs[1].ToString();
                                //////////////////////del_tbl_Storage_Arrival_Stock_DeleteLog/////////////////////////

                                qry = "Insert Into tbl_Storage_Arrival_Stock_DeleteLog select [ArrivalStock_Id] ,[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[Crop_Year],[Depositor_Name],[Sender_District],[Sender_Godown],[Book_No],[Challan_No],[Movement_Challan_Date],[Gate_PassNo],[Transporter_id],[Truck_No],[Truck_Driver_Name],[Source_of_Arrival],[Rice_Category],[Miller_Name],[Qty_No_of_Bags],[Qty_Wt],[Lot_No],[Quality_Moisture],[Quality_category],[CreatedBy],[UpdatedBy],'" + ip + "',[Gunny_bags_type],[Gunny_bags_new],[Gunny_bags_old],[Remarks],[DepositorType],[Receipt_Status],[Receipt_ID],[DepositDate],[CreatedDate],[Scheme_ID],[Client_IP],getdate(),[AcceptanceNo],[PurchasCentre],[IssueID],BranchID from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStock_Id.ToString() + "' and BranchID='" + Session["BranchID"].ToString() + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex = cmd.ExecuteNonQuery();
                                if (rex > 0)
                                {
                                    qry = "Delete from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStock_Id.ToString() + "' and BranchID = '" + Session["BranchID"].ToString() + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                }

                                //////////////////////tbl_Storage_Receipt_Details_DeleteLog/////////////////////////

                                qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],BranchID,Rid from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "' and  BranchID='" + Session["BranchID"].ToString() + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex1 = cmd.ExecuteNonQuery();
                                if (rex1 > 0)
                                {
                                    qry = "Delete from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "' and  BranchID='" + Session["BranchID"].ToString() + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                }

                                //////////////////////tbl_storage_Stacking_Details_DeleteLog/////////////////////////

                                qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],Branchid)  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],BranchID from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "'  and Branchid='" + Session["BranchID"].ToString() + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex2 = cmd.ExecuteNonQuery();
                                if (rex2 > 0)
                                {
                                    qry = "Delete from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "' and Branchid='" + Session["BranchID"].ToString() + "'";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                }

                                //////////////////////Receive_Proc_Kharif2016_Log/////////////////////////
                                //qry = "Insert Into Receive_Proc_Kharif2016_Log ([StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Deleted_By],[Deleted_Date]) SELECT [StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2016] where [StorageReceipt_Id]='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                qry = "Insert Into Receive_Proc_Kharif2019_Log ([StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Deleted_By],[Deleted_Date]) SELECT [StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2019] where [StorageReceipt_Id]='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";

                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex3 = cmd.ExecuteNonQuery();
                                if (rex2 > 0)
                                {
                                    //qry = "Delete from Receive_Proc_Kharif2016 where StorageReceipt_Id='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                    qry = "Delete from Receive_Proc_Kharif2019 where StorageReceipt_Id='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";

                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                }

                                count++;
                            }
                        }
                    }
                    sqltran.Commit();
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                    FillGridBranch();
                }
                else
                {
                    lbl_notfound.Visible = true;
                    lbl_notfound.Text = "Please Select Atleast One Record For Deleting";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                lbl_notfound.Visible = true;
                lbl_notfound.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
       

    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/StatePages/StateWelcome.aspx");
        Response.Redirect("~/Branch_Welcome.aspx");

    }

    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chk_Delete");
            CheckBox chkBxHeader = (CheckBox)this.gv.HeaderRow.FindControl("chkBxHeader");
            chkBxSelect.Attributes["onclick"] = string.Format
                                                   (
                                                      "javascript:ChildClick(this,'{0}');",
                                                      chkBxHeader.ClientID
                                                   );
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gv.PageIndex = e.NewPageIndex;
            FillGridBranch();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

}
