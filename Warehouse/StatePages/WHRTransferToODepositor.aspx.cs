using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

public partial class StatePages_WHRTransferToODepositor : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
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
                        Btn_Delete.Enabled = false;
                        Session["RefreshButton"] = "No";
                        string PopMsg = "";
                        PopMsg = Request.QueryString["PopMsg"];
                        if (PopMsg != null)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                        }

                        fillDistrict();

                        Btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to transfer this record , please be sure for transfer WHR?');");
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
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
                ddlGodown.DataSource = null;
                ddlGodown.DataBind();
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
        string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.BranchID = '" + ddlDepotList.SelectedValue.ToString() + "' and WHR.District_Id = '" + ddlDistrict.SelectedValue.ToString() + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' AND tbl_Storage_Receipt_Details.WHR_Flag = 'Y' order by WHR.Depositor_WHR_Id asc";
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
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
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

    private void Getgodowns()
    {
        string str = "select Godown_Name,Godown_id from tbl_MetaData_GODOWN where BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId = '" + ddlDistrict.SelectedValue.ToString() + "' and Remarks='Y'  order by Godown_Name";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
            gv.DataSource = null;
            gv.DataBind();
        }
        else
        {
            ddlGodown.DataSource = null;
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
        else if (ddlGodown.SelectedIndex != 0)
        {
            FillGrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Godown First')", true);
        }
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            Getgodowns();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        //string Stackid = "";
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
                    string WHRID = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                        ds = (DataSet)Session["ds_GridInfo"];
                        foreach (DataRow drs in ds.Tables[0].Select("Depositor_WHR_Id = '" + WHRID + "'"))
                        {
                            qry = "Insert Into [tbl_storage_Depositor_WHR_Relation_Log] SELECT [Depositor_WHR_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[Whr_No],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[Mode_of_weighment],[BeamScale_LWB],[AvgMoisture_Content],[Lot_No] ,[MktValue_of_Commodity],[Arrival_Source],[WHR_Issue_Date],[CreatedBy],[CreatedDate],'" + ip + "',getdate(),DeletedBy,DeletedDate,[MadeUpBags],[Client_IP],[AvgMoisture_Content_To],[Did] ,[CropYear],[Remark] ,[SangrahadDate],[LicenseNo],[LicenseDate] ,[wday],[wmon],[wyear],[BranchID],[DepositorID],[GodownID],[Gid] from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
                            cmd = new SqlCommand(qry, con, sqltran);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                qry = "update tbl_storage_Depositor_WHR_Relation set Depositor_Name='DMO Markfed',DepositorID='4679',UpdatedBy='" + ip + "',UpdatedDate=GETDATE() where Depositor_WHR_Id='" + WHRID + "'";
                                cmd = new SqlCommand(qry, con, sqltran);
                                int d = cmd.ExecuteNonQuery();
                                if (d > 0)
                                {
                                    qry = "insert into [WhrPrintResetDetail] values('" + WHRID + "','" + ip + "',GetDate(),'" + ddlDepotList.SelectedValue.ToString() + "')";
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x1 = cmd.ExecuteNonQuery();
                                    if (x1 > 0)
                                    {
                                        qry = "update [whrprintstatus] set PrintStatus='1st' , DateUpdated=GETDATE() where WHRID='" + WHRID + "'";
                                        cmd = new SqlCommand(qry, con, sqltran);
                                        int x = cmd.ExecuteNonQuery();
                                        if (x > 0)
                                        {
                                            qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],'" + ip + "',getdate(),DeletedBy,DeletedDate,[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],BranchID,Rid from tbl_Storage_Receipt_Details where WHR_id='" + WHRID + "'";
                                            cmd = new SqlCommand(qry, con, sqltran);
                                            int rex1 = cmd.ExecuteNonQuery();
                                            if (rex1 > 0)
                                            {
                                                qry = "update tbl_Storage_Receipt_Details set DepositorName='DMO Markfed' where WHR_Id = '" + WHRID + "'";
                                                cmd = new SqlCommand(qry, con, sqltran);
                                                int rex2 = cmd.ExecuteNonQuery();                                           
                                            }                                          
                                        }
                                    }
                                }
                            }
                        }
                        count++;
                    }
                }
                sqltran.Commit();
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Transfer')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully Transfered')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Transfer')", true);
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
}
