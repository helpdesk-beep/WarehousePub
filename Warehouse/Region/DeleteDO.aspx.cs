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

public partial class Region_DeleteDO : System.Web.UI.Page
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
     SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                if (!Page.IsPostBack)
                {
                    fillDistrict();
                    getComm();
                }
                btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillGatepassGrid()
    {
        try
        {
            //string query = "select SFSD.StockDeliveryOrder_Id,SGE.[Depositor/Issuer_Name],com.Commodity_Name,convert(varchar(15),SFSD.CreatedDate,103)DODate,SFSD.GatePass_No as GatePassNO,SFSD.Qty_Issued_No_Bags_Sound as Qty,SFSD.Qty_Issued_Weight as  Wet from tbl_Storage_Final_Stock_Delivery_Order as SFSD inner join tbl_Storage_GatePass_Enrty AS SGE on SFSD.GatePass_No=SGE.GatePass_No join tbl_MetaData_STORAGE_COMMODITY as com on SGE.Commodity_ID = com.Commodity_Id and SFSD.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and SFSD.DepotId='" + ddlDepotList.SelectedValue.ToString() + "' order by DODate Asc";
            string query = "select distinct gp.gatepass_no as GatePassNO,[Depositor/Issuer_Name],gp.Issue_Source_ID as StockDeliveryOrder_Id,convert(varchar(10),sdo.Delivery_Order_Date,103) as Delivery_Order_Date,com.Commodity_Name,(gp.NO_of_Bage) as Qty,convert(decimal(18,2),gp.Weight) as Wet ,convert(varchar(15),sdo.CreatedDate,103) as GPDate from tbl_Storage_GatePass_Enrty gp inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID=sdo.Delivery_Order_No where gp.Issue_Source='DO' and gp.Status!='Cancel' and gp.Issue_Source_ID!='0' and gp.BranchID ='" + ddlDepotList.SelectedValue.ToString() + "' and gp.District_ID = '" + ddlDistrict.SelectedValue.ToString() + "' and gp.Commodity_ID='" + ddlcommodity.SelectedValue.ToString() + "' group by gp.gatepass_no,[Depositor/Issuer_Name],gp.Issue_Source_ID,com.Commodity_Name,gp.NO_of_Bage,gp.Weight,sdo.CreatedDate,sdo.Delivery_Order_Date order by Delivery_Order_Date Asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_notfound.Visible = false;
                btn_Delete.Enabled = true;
                gvDeliveryOrder.DataSource = ds.Tables[0];
                gvDeliveryOrder.DataBind();
                lbl_Count.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
            else
            {
                btn_Delete.Enabled = false;
                lbl_notfound.Visible = true;
                lbl_notfound.Text = "There is NO Delivery Order Found";
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                lbl_Count.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            con.Close();
        }
        finally
        {
            con.Close();
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sqltran = con.BeginTransaction();
            foreach (GridViewRow gvrows in gvDeliveryOrder.Rows)
            {
                CheckBox chk_Delete = new CheckBox();
                chk_Delete = (CheckBox)gvrows.Cells[0].FindControl("chk_Delete");
                if (chk_Delete.Checked == true)
                {

                    string StockDO_Id = gvDeliveryOrder.DataKeys[gvrows.RowIndex].Value.ToString();
                    string Bags = gvrows.Cells[5].Text.ToString();
                    string Quantity = gvrows.Cells[6].Text.ToString();
                    string Gatepass_no = gvrows.Cells[7].Text.ToString();
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    string qryins_deltran = "Insert Into tbl_Storage_Final_Stock_Delivery_Order_DeleteLog SELECT [StockDeliveryOrder_Id],[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[WHR_Id],[Release_Order_No],[Release_Order_Date],[Delivery_Order_No],[Delivery_Order_Date],[Qty_Issued_No_Bags_Sound],[Qty_Issued_No_Bags_Spilage],[Qty_Issued_Weight],[Value_Stock_Delivered],[Moisture_Content],[Purpose_Of_Issue],[Issue_within_Outside],[Rental_Amt_Received],[RO_Quantity],[RO_Rate_Quintal],[RO_Validity],[Cash_Credit],[DD_No],[DD_Date],[DD_BankId],[Sample_Serial_No],[Vikas_Chand],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[RecipientDistrict],[RecipientDepot],[TransporterId],[DeliverdAgent],[Districtdeliverd],[deliveredName],[DelveredAddress],[CWCstatus],[FPSId],[FPS],[GatePass_No],[BranchID],'R'  From tbl_Storage_Final_Stock_Delivery_Order where  District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and BranchID ='" + ddlDepotList.SelectedValue.ToString() + "' and StockDeliveryOrder_Id='" + StockDO_Id + "' ";
                    cmd = new SqlCommand(qryins_deltran, con, sqltran);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        //Perform Delete Operation here
                        string qry = "Delete from tbl_Storage_Final_Stock_Delivery_Order where  District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and StockDeliveryOrder_Id='" + StockDO_Id + "'";
                        cmd = new SqlCommand(qry, con, sqltran);
                        int b = cmd.ExecuteNonQuery();
                    }
                    //Perform Deletr Operation here 
                    string qryin = "select Stack_ID From [tbl_Delivery_Stacking_Details_GatePass] where GatePass_No='" + Gatepass_no + "' ";
                    cmd = new SqlCommand(qryin, con, sqltran);
                    String StackId = cmd.ExecuteScalar().ToString();

                    #region //change whr flag when whr have some qty from whr_status

                    string qryinwhr = "select Depositor_WHR_Id from tbl_Delivery_Stacking_Details_GatePass where GatePass_No='" + Gatepass_no + "'";
                    cmd = new SqlCommand(qryinwhr, con, sqltran);
                    DataSet dswhr = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dswhr);
                    String Depositor_WHR_Id = "";
                    if (dswhr != null)
                    {
                        foreach (DataRow drw in dswhr.Tables[0].Rows)
                        {
                            Depositor_WHR_Id = drw["Depositor_WHR_Id"].ToString();
                            if (Depositor_WHR_Id != "")
                            {
                                cmd = new SqlCommand();
                                cmd = con.CreateCommand();
                                cmd.Transaction = sqltran;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "sp_updatewhr_status_DelDo";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@whrid", Depositor_WHR_Id);
                                cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@DepotId", ddlDepotList.SelectedValue.ToString());
                                int x = cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                        }
                        //cmd = new SqlCommand(qryinwhr,con );
                        //String Depositor_WHR_Id = cmd.ExecuteScalar().ToString();
                        //endArun
                    }

                    #endregion

                    if (StackId != "")
                    {
                        //This Proc is used for calculation ,Stack Master Which substtracted the issed items from stack stock
                        cmd = new SqlCommand();
                        cmd = con.CreateCommand();
                        cmd.Transaction = sqltran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "upd_tbl_MetaData_STACK";
                        cmd.Parameters.AddWithValue("@StackId", StackId);
                        cmd.Parameters.AddWithValue("@Bags", Bags);
                        cmd.Parameters.AddWithValue("@Weight", Quantity);
                        int x = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        //Perform Insert Operation for maintaining log 
                        string qryin3 = "Insert  Into DailyStacking_TransactionStatus_DeleteLog SELECT [Stackid],[OpeningBalanceBags],[OpeningBalanceWts],[ReceiptBags],[ReceiptWts],[IssuedBags],[IssuedWts],[TransactionDate],[Autoid],[Loss],[Gain],'" + ip + "',getdate() from DailyStacking_TransactionStatus where Stackid='" + StackId + "' and Autoid=(select MAX(Autoid) from DailyStacking_TransactionStatus where DailyStacking_TransactionStatus.Stackid='" + StackId + "')";
                        cmd = new SqlCommand(qryin3, con, sqltran);
                        int r = cmd.ExecuteNonQuery();

                        //Perform Delte operation from Daily Transaction Status
                        if (r > 0)
                        {

                            #region DailyStacking_TransactionStatus update

                            ////////We Have to update DailyStacking_TransactionStatus IssueBags and IssueQty Arun Sonare Update 18/02/2014/////////

                            cmd = new SqlCommand();
                            cmd = con.CreateCommand();
                            cmd.Transaction = sqltran;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "sp_Update_dailystackingissue_entry";
                            cmd.Parameters.AddWithValue("@StackId", StackId);
                            cmd.Parameters.AddWithValue("@Bags", Bags);
                            cmd.Parameters.AddWithValue("@Weight", Quantity);
                            cmd.Parameters.AddWithValue("@UpdatedBy", ip);
                            int z = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            #endregion

                            //Keshav lidoriya 
                            //////string qryin4 = "Delete from DailyStacking_TransactionStatus where Stackid='" + StackId + "' and Autoid=(select MAX(Autoid) from DailyStacking_TransactionStatus where DailyStacking_TransactionStatus.Stackid='" + StackId + "' )";
                            //////cmd = new SqlCommand(qryin4, con);
                            //////int r2 = cmd.ExecuteNonQuery();
                        }

                        //Perform Status updation of table tbl_MetaData_STACK on the basis of satack id
                        cmd = new SqlCommand();
                        cmd = con.CreateCommand();
                        cmd.Transaction = sqltran;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "upd_Status_tbl_MetaData_STACK";
                        cmd.Parameters.AddWithValue("@StackId", StackId);
                        int x2 = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        //Here We Have to update whr status on the basis of Delete DO
                        //Now perform Delete operation from gatePass entry....... that is having delivery order record.

                        string qryin5 = "Insert Into tbl_Delivery_Stacking_Details_GatePass_DeleteLog SELECT [Godown_ID],[Stack_ID],[No_Of_Bags],[Bags_Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[Depositor_WHR_Id],[StockDeliveryOrderGatePass_Id],[Autoid],[Loss],[Gain],[GatePass_No] from tbl_Delivery_Stacking_Details_GatePass where  GatePass_No='" + Gatepass_no + "'";
                        cmd = new SqlCommand(qryin5, con, sqltran);
                        int r3 = cmd.ExecuteNonQuery();

                        if (r3 > 0)
                        {
                            //Perform delete Operation
                            string qryin6 = "delete from tbl_Delivery_Stacking_Details_GatePass where  GatePass_No='" + Gatepass_no + "'";
                            cmd = new SqlCommand(qryin6, con, sqltran);
                            int r4 = cmd.ExecuteNonQuery();
                        }
                        //Next Insert
                        string qryin7 = "Insert Into tbl_Storage_GatePass_Enrty_DeleteLog SELECT [GatePass_No],[State_ID],[District_ID],[Depot_ID],[Godown_ID],[Stack_ID],[Depositor/Issuer_Name],[Commodity_ID],[Scheme_ID],[Vehicle_Type],[Vehicle_No],[Driver_Name],[NO_of_Bage],[Weight],[Issue_Source],[Issue_Source_ID],[CreatedBy],[Issue_Date],[Status],[Printed],[License_No],[Valid_Upto],[Arrival_Dep_Time],[Remarks],[Miller_Id],[ReasonforCancelation],'" + ip + "',getdate(),[GP_FIN_YR],[GP_SL_No],OGatePass_No,[BranchID],[GodownNam] from  tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass_no + "' and Issue_Source_ID='" + StockDO_Id + "'";
                        cmd = new SqlCommand(qryin7, con, sqltran);
                        int r5 = cmd.ExecuteNonQuery();
                        if (r5 > 0)
                        {
                            //Perform Delete operation

                            string qryin8 = "delete from tbl_Storage_GatePass_Enrty where  GatePass_No='" + Gatepass_no + "' and Issue_Source_ID='" + StockDO_Id + "' ";
                            cmd = new SqlCommand(qryin8, con, sqltran);
                            int r6 = cmd.ExecuteNonQuery();
                        }
                        string qry10 = "insert into [tbl_RO_Details_Log] SELECT [Trans_ID],[State_Id],[District_Id],[DepotId],[Commodity_Id],[Release_Order_No],[Release_Order_Date],[RO_Quantity],[FPSId],[FPS],[Truckno],[GatePass_No],[RO_Quantity_issued],[allotment_month],[allotment_year],'" + ip + "',GETDATE(),[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_RO_Details] where GatePass_No='" + Gatepass_no + "'";
                        cmd = new SqlCommand(qry10, con, sqltran);
                        int t = cmd.ExecuteNonQuery();
                        if (t >= 0)
                        {

                            string qry11 = "Delete from tbl_RO_Details where GatePass_No = '" + Gatepass_no + "'";
                            cmd = new SqlCommand(qry11, con, sqltran);
                            int x11 = cmd.ExecuteNonQuery();
                        }

                        #region tbl_Storage_Final_Stock_Delivery_GatePass
                        //perform  delete to Storage_Final_Stock_Delivery_GatePass update-18/02/2014
                        //string selectc = "select * from tbl_Storage_Final_Stock_Delivery_GatePass where District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and DepotId='" + ddlBranch.SelectedValue.ToString() + "' and tbl_Storage_Final_Stock_Delivery_GatePass.GatePass_No='" + Gatepass_no + "'";
                        string qryin9 = "insert into tbl_Storage_Final_Stock_Delivery_GatePass_DelLog select * from tbl_Storage_Final_Stock_Delivery_GatePass where District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and  tbl_Storage_Final_Stock_Delivery_GatePass.GatePass_No='" + Gatepass_no + "'";
                        cmd = new SqlCommand(qryin9, con, sqltran);
                        int r7 = cmd.ExecuteNonQuery();
                        if (r7 > 0)
                        {
                            //Perform Delete operation

                            string qryin10 = "delete from tbl_Storage_Final_Stock_Delivery_GatePass where District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and  tbl_Storage_Final_Stock_Delivery_GatePass.GatePass_No='" + Gatepass_no + "'";
                            cmd = new SqlCommand(qryin10, con, sqltran);
                            int r8 = cmd.ExecuteNonQuery();
                        }
                        #endregion

                    }

                    result++;
                }
            }
            sqltran.Commit();
            if (result > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record has been Deleted successfully.'); </script> ");
                fillGatepassGrid();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Atleast One Record to Deleting'); </script> ");
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            sqltran.Rollback();
            con.Close();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has been occured,Please Try Again'); </script> ");
        }
        finally
        {
            sqltran.Dispose();
            con.Close();
            //getDO();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
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

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            // fillGatepassGrid();
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
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
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

    protected void gvDeliveryOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvDeliveryOrder.PageIndex = e.NewPageIndex;
            fillGatepassGrid();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "select depo.DepotID,depo.BranchId,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                //ddlDepotList.DataValueField = "DepotID";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
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

    private void getComm()
    {
        try
        {
            string query = "SELECT [Commodity_Id],[Commodity_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STORAGE_COMMODITY] order by Commodity_Name asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "Commodity_Id";
                ddlcommodity.DataBind();
                ddlcommodity.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlcommodity.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        fillGatepassGrid();
    }

    private void fillDOGrid()
    {
        try
        {
            //string query = "select SFSD.StockDeliveryOrder_Id,SGE.[Depositor/Issuer_Name],com.Commodity_Name,convert(varchar(15),SFSD.CreatedDate,103)DODate,SFSD.GatePass_No as GatePassNO,SFSD.Qty_Issued_No_Bags_Sound as Qty,SFSD.Qty_Issued_Weight as  Wet from tbl_Storage_Final_Stock_Delivery_Order as SFSD inner join tbl_Storage_GatePass_Enrty AS SGE on SFSD.GatePass_No=SGE.GatePass_No join tbl_MetaData_STORAGE_COMMODITY as com on SGE.Commodity_ID = com.Commodity_Id and SFSD.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and SFSD.DepotId='" + ddlDepotList.SelectedValue.ToString() + "' order by DODate Asc";
            string query = "select distinct gp.gatepass_no as GatePassNO,[Depositor/Issuer_Name],gp.Issue_Source_ID as StockDeliveryOrder_Id,convert(varchar(10),sdo.Delivery_Order_Date,103) as Delivery_Order_Date,com.Commodity_Name,(gp.NO_of_Bage) as Qty,convert(decimal(18,2),gp.Weight) as Wet ,convert(varchar(15),sdo.CreatedDate,103) as GPDate from tbl_Storage_GatePass_Enrty gp inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id join tbl_Storage_Final_Stock_Delivery_Order as sdo on gp.Issue_Source_ID=sdo.Delivery_Order_No where gp.Issue_Source='DO' and gp.Status!='Cancel' and gp.Issue_Source_ID!='0' and gp.BranchID ='" + ddlDepotList.SelectedValue.ToString() + "' and gp.District_ID = '" + ddlDistrict.SelectedValue.ToString() + "' and gp.Commodity_ID='" + ddlcommodity.SelectedValue.ToString() + "' and gp.Issue_Source_ID='" + txtdonum.Text + "' group by gp.gatepass_no,[Depositor/Issuer_Name],gp.Issue_Source_ID,com.Commodity_Name,gp.NO_of_Bage,gp.Weight,sdo.CreatedDate,sdo.Delivery_Order_Date order by Delivery_Order_Date Asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_notfound.Visible = false;
                btn_Delete.Enabled = true;
                gvDeliveryOrder.DataSource = ds.Tables[0];
                gvDeliveryOrder.DataBind();
                lbl_Count.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
            else
            {
                btn_Delete.Enabled = false;
                lbl_notfound.Visible = true;
                lbl_notfound.Text = "There is NO Delivery Order Found";
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                lbl_Count.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            con.Close();
        }
        finally
        {
            con.Close();
        }
    }

    protected void btndonum_Click(object sender, EventArgs e)
    {
        fillDOGrid();
    }
}