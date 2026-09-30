using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using System.Resources;

public partial class IssueCenterLevel_Storage_Delete_Delivery_Gatepass : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] != null)
            {
                if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        string PopMsg = "";
                        PopMsg = Request.QueryString["PopMsg"];
                        if (Request.QueryString["PopMsg"] != null)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                        }
                        fillDistrict();
                    }
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void fillGatepassGrid()
    {
        try
        {
           // string query = "select DISTINCT SGE.GatePass_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SGE.NO_of_Bage,CONVERT (DECIMAL(18,2),SGE.Weight) AS Weight,convert(NVARCHAR(10),SGE.issue_Date,103) as Issue_Date from tbl_Storage_GatePass_Enrty AS SGE  join tbl_Storage_Final_Stock_Delivery_GatePass AS FSGE ON SGE.GatePass_No = FSGE.GatePass_No JOIN tbl_MetaData_STORAGE_COMMODITY on SGE.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where issue_Source='RO' and SGE.Status !='Cancel' and SGE.GatePass_No not in (select GatePass_No from tbl_Storage_Final_Stock_Delivery_Order) AND SGE.District_ID = '" + ddlDistrict.SelectedValue.ToString() + "' AND SGE.Depot_ID = '" + ddlDepotList.SelectedValue.ToString() + "' ORDER BY SGE.GatePass_No";
            string query = "select DISTINCT SGE.GatePass_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SGE.NO_of_Bage,CONVERT (DECIMAL(18,2),SGE.Weight) AS Weight,convert(NVARCHAR(10),SGE.issue_Date,103) as Issue_Date  from tbl_Storage_GatePass_Enrty AS SGE   JOIN tbl_MetaData_STORAGE_COMMODITY on SGE.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where SGE.Status !='Cancel' and Issue_Source='RO'    and Issue_Source_ID='0' and SGE.District_ID = '" + ddlDistrict.SelectedValue.ToString() + "' AND SGE.BranchID = '" + ddlDepotList.SelectedValue.ToString() + "'  ORDER BY SGE.GatePass_No";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_Empty.Text = "";
                lbl_Empty.Visible = false;
                gv_gatepass.DataSource = ds.Tables[0];
                gv_gatepass.DataBind();
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
            }
            else
            {
                lbl_Empty.Visible = true;
                lbl_Empty.Text = "There is No Delivery GatePass Pending";
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

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
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
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

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            fillGatepassGrid();
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
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
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

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            if (gv_gatepass.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach (GridViewRow gr2 in gv_gatepass.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    string Gatepass = Convert.ToString(gv_gatepass.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == true)
                    {
                     //   qry = "Insert Into tbl_Storage_GatePass_Enrty_DeleteLog SELECT [GatePass_No],[State_ID],[District_ID],[Depot_ID],[Godown_ID],[Stack_ID],[Depositor/Issuer_Name],[Commodity_ID],[Scheme_ID],[Vehicle_Type],[Vehicle_No],[Driver_Name],[NO_of_Bage],[Weight],[Issue_Source],[Issue_Source_ID],[CreatedBy],[Issue_Date],[Status],[Printed] ,[License_No],[Valid_Upto],[Arrival_Dep_Time],[Remarks],[Miller_Id],[ReasonforCancelation],'" + ip + "',getdate(),[GP_FIN_YR],[GP_SL_No],[OGatePass_No] from tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                        qry = "Insert Into tbl_Storage_GatePass_Enrty_DeleteLog SELECT [GatePass_No],[State_ID],[District_ID],[Depot_ID],[Godown_ID],[Stack_ID],[Depositor/Issuer_Name],[Commodity_ID],[Scheme_ID],[Vehicle_Type],[Vehicle_No],[Driver_Name],[NO_of_Bage],[Weight],[Issue_Source],[Issue_Source_ID],[CreatedBy],[Issue_Date],[Status],[Printed],[License_No],[Valid_Upto],[Arrival_Dep_Time],[Remarks],[Miller_Id],[ReasonforCancelation],'" + ip + "',getdate(),[GP_FIN_YR],[GP_SL_No],OGatePass_No,[BranchID],[GodownNam] from  tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                        cmd = new SqlCommand(qry, con);
                        int c = cmd.ExecuteNonQuery();
                        if (c > 0)
                        {
                            qry = "Delete from tbl_Storage_GatePass_Enrty where GatePass_No='" + Gatepass + "'";
                            cmd = new SqlCommand(qry, con);
                            int d = cmd.ExecuteNonQuery();
                            if (d > 0)
                            {
                                qry = "insert into [tbl_RO_Details_Log] SELECT [Trans_ID],[State_Id],[District_Id],[DepotId],[Commodity_Id],[Release_Order_No],[Release_Order_Date],[RO_Quantity],[FPSId],[FPS],[Truckno],[GatePass_No],[RO_Quantity_issued],[allotment_month],[allotment_year],'" + ip + "',GETDATE(),[BranchID] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_RO_Details] where GatePass_No='" + Gatepass + "'";
                                cmd = new SqlCommand(qry, con);
                                int t = cmd.ExecuteNonQuery();
                                if (t >= 0)
                                {

                                }
                                    qry = "Delete from tbl_RO_Details where GatePass_No = '" + Gatepass + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int x = cmd.ExecuteNonQuery();
                                    if (x >= 0)
                                    {

                                    }
                                        qry = "insert into  tbl_Storage_Final_Stock_Delivery_GatePass_DelLog Select [StockDeliveryOrderGatePass_Id] ,[State_Id],[District_Id],[DepotId],[Commodity_Id],[Category_Id],[WHR_Id],[Delivery_Order_No],[Delivery_Order_Date],[Qty_Issued_No_Bags_Sound] ,[Qty_Issued_No_Bags_Spilage],[Qty_Issued_Weight],[Value_Stock_Delivered],[Moisture_Content],[Purpose_Of_Issue],[Issue_within_Outside],[Rental_Amt_Received],[Cash_Credit],[DD_No],[DD_Date],[DD_BankId],[Sample_Serial_No],[Vikas_Chand],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[RecipientDistrict],[RecipientDepot],[TransporterId],[DeliverdAgent],[Districtdeliverd],[deliveredName],[DelveredAddress],[CWCstatus],[FPSId],[FPS],[GatePass_No],[DeliveryOrderID],[trans_id] from tbl_Storage_Final_Stock_Delivery_GatePass where GatePass_No = '" + Gatepass + "'";
                                        qry = "insert into tbl_Storage_Final_Stock_Delivery_GatePass_DelLog select * from tbl_Storage_Final_Stock_Delivery_GatePass where District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and  tbl_Storage_Final_Stock_Delivery_GatePass.GatePass_No='" + Gatepass + "'";
                                        cmd = new SqlCommand(qry, con);
                                        int y = cmd.ExecuteNonQuery();
                                        if (y > 0)
                                        {
                                            qry = "Delete from tbl_Storage_Final_Stock_Delivery_GatePass where GatePass_No='" + Gatepass + "'";
                                            cmd = new SqlCommand(qry, con);
                                            int f = cmd.ExecuteNonQuery();
                                        }

                                        /////////////////////////////////////////////

                                        qry = "insert into tbl_Delivery_Stacking_Details_GatePass_DeleteLog select [Godown_ID],[Stack_ID],[No_Of_Bags],[Bags_Weight],[CreatedBy],[CreatedDate] ,[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[Depositor_WHR_Id],[StockDeliveryOrderGatePass_Id],[Autoid] ,[Loss],[Gain],[GatePass_No] from tbl_Delivery_Stacking_Details_GatePass  where GatePass_No = '" + Gatepass + "'";
                                        cmd = new SqlCommand(qry, con);
                                        int z = cmd.ExecuteNonQuery();
                                        if (z > 0)
                                        {
                                            qry = "Delete from tbl_Delivery_Stacking_Details_GatePass where GatePass_No='" + Gatepass + "'";
                                            cmd = new SqlCommand(qry, con);
                                            int i = cmd.ExecuteNonQuery();
                                        }
                                       
                                    
                                
                            }

                        }
                    }
               }
                count++;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                fillGatepassGrid();
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
}
