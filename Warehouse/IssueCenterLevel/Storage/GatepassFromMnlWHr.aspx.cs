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
using System.Text;
using System.Resources;

public partial class IssueCenterLevel_Storage_GatepassFromMnlWHr : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    string GateP_No = string.Empty;
    String GP_SL_No = "";
    string mid = "";
    decimal issuelosswt = 0;
    decimal issuegainwt = 0;
    int calflag = 0;
    SqlTransaction sqltran;
    string godownid = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                lblInstruction.Text = Resources.hindi.lblInstruction;
                lblDeliveryOrderOfStock.Text = Resources.hindi.lblDeliveryOrderOfStock;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                lblIssuedTo.Text = Resources.hindi.lblIssuedTo;
                lblDONo.Text = Resources.hindi.lblDONo;
                lbIssusedQty.Text = Resources.hindi.lbIssusedQty;
                lblGodownNo.Text = Resources.hindi.lblGodownNo;
                lblIssuedBags.Text = Resources.hindi.lblIssuedBags;
                lblIssuedweight.Text = Resources.hindi.lblIssuedweight;
                lblPercentMoisture.Text = Resources.hindi.lblPercentMoisture;
                lblRecDistrict.Text = Resources.hindi.lblRecDistrict;
                lblRecDepot.Text = Resources.hindi.lblRecDepot;
                lblTrans.Text = Resources.hindi.lblTrans;
                lblTypeVehicle.Text = Resources.hindi.lblTypeVehicle;
                lblTruckNumber.Text = Resources.hindi.lblTruckNumber;
                lblArrivalTime.Text = Resources.hindi.lblArrivalTime;
                lblDesc.Text = Resources.hindi.lblDesc;
                lblDONo.Text = Resources.hindi.lblDONo;
                lblStockforIssue.Text = Resources.hindi.lblStockforIssue;
                lblDetailsofRO.Text = Resources.hindi.lblDetailsofRO1;
                lbIssusedQty.Text = Resources.hindi.lbIssusedQty;
            }
            if (!IsPostBack)
            {
                Session["RefreshButton"] = "No";
                Session["dtRo"] = null;

                string script = "$(document).ready(function () { $('[id*=btnsave]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                GetDepositorType();
                Getgodowns();
                ddlDepositorType_SelectedIndexChanged(sender, e);
                ddlDepositor_SelectedIndexChanged(sender, e);
                trOtherDepot.Visible = false;
                fillTransporter();
                GetCommodity();
                fillMinitues();
                Printcurrentdate();
                //for (int i = 0; i < gdstackdetail.Rows.Count; i++)
                //{
                //    if (((CheckBox)gdstackdetail.Rows[i].FindControl("ckstack")).Checked == true)
                //    {

                //        ((TextBox)gdstackdetail.Rows[i].FindControl("txtgain")).Enabled = false;
                //        ((TextBox)gdstackdetail.Rows[i].FindControl("txtloss")).Enabled = false;
                //    }
                //}
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextBox2.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                txtdateofissue.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    //protected string getDate_MDY(string inDate)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    //}

    private void fillMinitues()
    {
        for (int Min = 0; Min < 60; Min++)
        {
            string mi = Min.ToString();
            if (mi.Length == 1)
            {
                mi = "0" + mi;
            }
            ddl2.Items.Add(mi.ToString());
            ddl2.DataValueField = mi.ToString();
            ddl2.DataValueField = mi.ToString();
        }
    }

    private void fillTransporter()
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                qry = "SELECT Transporter_Id, Transpoter_Name FROM tbl_metadata_transport where  DepotID  = '" + Session["Depot_DepotID"].ToString() + "' order by Transpoter_Name";
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UxTrans.Items.Clear();
                    UxTrans.DataSource = ds.Tables[0];
                    UxTrans.DataTextField = "Transpoter_Name";
                    UxTrans.DataValueField = "Transporter_ID";
                    UxTrans.DataBind();
                    UxTrans.Items.Insert(0, "---Select---");
                }
                else
                {
                    UxTrans.Items.Insert(0, "NA");
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (ddlDepositorType.SelectedItem.Text == "Institution")
            {
                ddlDeliveredAgnt.Enabled = true;

                if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPSCSC")
                {
                    // ddlDeliveredAgnt.Items.Clear();
                    txtissuedbags.Enabled = false;
                    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
                    txtissuedwt.Enabled = false;
                    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
                    string notin = "DEPOSITOR";
                    bindIssuedTo(notin);
                }
                else if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPWLC")
                {
                    txtissuedbags.Enabled = false;
                    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
                    txtissuedwt.Enabled = false;
                    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
                    string notin = "DEPOSITOR','LEAD SOCIETY','FPS";
                    bindIssuedTo(notin);
                    GvuFillMPSCSCTCData.DataSource = null;
                    GvuFillMPSCSCTCData.DataBind();
                    divMPSCSCTCData.Visible = false;
                }
                else
                {
                    string notin = "OTHER DEPOT','LEAD SOCIETY','FPS";
                    bindIssuedTo(notin);
                    trOtherDepot.Visible = false;
                    FillRecDist();
                    ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
                    FillRecDepot(ddlRecDistrict.SelectedValue);
                    ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();
                    GvuFillMPSCSCTCData.DataSource = null;
                    GvuFillMPSCSCTCData.DataBind();
                    divMPSCSCTCData.Visible = false;
                }
            }
            else
            {
                if (ddlDepositor.Items.Count > 0)
                {
                    ddlDeliveredAgnt.SelectedIndex = 4;
                    //ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    ddlDeliveredAgnt.Enabled = false;
                }

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlDepositor.Items.Clear();
                string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
                string depotid = Session["Depot_DepotID"].ToString();
                string BranchID = Session["BranchId"].ToString();

                if (depositer == "Institution")
                {
                    qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    ddlDeliveredAgnt.Enabled = true;
                }
                else
                {
                    qry = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE BranchId='" + BranchID + "' and Depositor_Type ='" + depositer + "'";
                    ddlDeliveredAgnt.Enabled = false;

                }
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepositor.DataSource = ds;
                    ddlDepositor.DataTextField = "Depositor_Name";
                    ddlDepositor.DataValueField = "Depositor_Name";
                    ddlDepositor.DataBind();

                }
                if (depositer != "Institution")
                {
                    if (ddlDepositor.Items.Count > 0)
                    {
                        ddlDeliveredAgnt.SelectedIndex = 4;
                        trcomm.Visible = true;
                        lblgatepasstype.Visible = false;
                        txtdateofissue.Visible = false;
                        rbdate.Visible = false;
                        rbdo.Visible = false;
                        // ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    }
                }
                else
                {
                    trcomm.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in depositor type selected index!')", true);
            }
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
                ddlcomm.DataSource = ds.Tables[0];
                ddlcomm.DataValueField = "Commodity_Id";
                ddlcomm.DataTextField = "Commodity_Name";
                ddlcomm.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
    }
    protected void bindIssuedTo(string notin)
    {
        try
        {
            ddlDeliveredAgnt.DataSource = null;
            ddlDeliveredAgnt.DataBind();
            qry = "SELECT Source_ID,Source_Name FROM [tbl_MetaData_Issued_To]";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDeliveredAgnt.DataSource = ds;
                ddlDeliveredAgnt.DataTextField = "Source_Name";
                ddlDeliveredAgnt.DataValueField = "Source_ID";
                ddlDeliveredAgnt.DataBind();
                ddlDeliveredAgnt.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDeliveredAgnt.DataSource = null;
                ddlDeliveredAgnt.DataBind();
                ddlDeliveredAgnt.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in Bind Issue to')", true);
        }
    }

    protected void fillGridMPSCSCTCData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                // qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join [MPSCSCSVR].[MPSCSC].[dbo].tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["Depot_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //27-12-14--ori--- qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["Depot_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["Depot_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["Depot_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuFillMPSCSCTCData.DataSource = ds.Tables[0];
                    GvuFillMPSCSCTCData.DataBind();
                    divMPSCSCTCData.Visible = true;
                    trOtherDepot.Visible = true;

                    GvuFillMPSCSCTCData.HeaderRow.Cells[11].Visible = false;
                    GvuFillMPSCSCTCData.HeaderRow.Cells[12].Visible = false;
                    GvuFillMPSCSCTCData.HeaderRow.Cells[13].Visible = false;
                    for (int i = 0; i < GvuFillMPSCSCTCData.Rows.Count; i++)
                    {

                        GvuFillMPSCSCTCData.Rows[i].Cells[11].Visible = false;
                        GvuFillMPSCSCTCData.Rows[i].Cells[12].Visible = false;
                        GvuFillMPSCSCTCData.Rows[i].Cells[13].Visible = false;
                    }
                    lblcom.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
                }
                else
                {
                    Gridclear();
                    divMPSCSCTCData.Visible = false;
                    Rodetails.Visible = false;
                    Stockdetails.Visible = false;
                    Issuedetails.Visible = false;
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    GvuFillMPSCSCTCData.DataSource = null;
                    GvuFillMPSCSCTCData.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record found!')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fillGridMPSCSCTCData')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    //protected string getDate_MDY(string inDate)
    //{

    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
    //    return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    //}


    protected void fillGridMPSCSCTCRailData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                //qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join [MPSCSCSVR].[MPSCSC].[dbo].tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["Depot_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["Depot_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //   qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["Depot_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["Depot_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuFillMPSCSCTCData.DataSource = ds.Tables[0];
                    GvuFillMPSCSCTCData.DataBind();
                    divMPSCSCTCData.Visible = true;
                    trOtherDepot.Visible = true;

                    GvuFillMPSCSCTCData.HeaderRow.Cells[11].Visible = false;
                    GvuFillMPSCSCTCData.HeaderRow.Cells[12].Visible = false;
                    GvuFillMPSCSCTCData.HeaderRow.Cells[13].Visible = false;
                    for (int i = 0; i < GvuFillMPSCSCTCData.Rows.Count; i++)
                    {

                        GvuFillMPSCSCTCData.Rows[i].Cells[11].Visible = false;
                        GvuFillMPSCSCTCData.Rows[i].Cells[12].Visible = false;
                        GvuFillMPSCSCTCData.Rows[i].Cells[13].Visible = false;
                    }
                    lblcom.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
                }
                else
                {
                    Gridclear();
                    divMPSCSCTCData.Visible = false;
                    Rodetails.Visible = false;
                    Stockdetails.Visible = false;
                    Issuedetails.Visible = false;
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    GvuFillMPSCSCTCData.DataSource = null;
                    GvuFillMPSCSCTCData.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record found!')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fillGridMPSCSCTCData rack')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    protected void fillDO()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlDONumber.Items.Clear();
                string depotid = Session["Depot_DepotID"].ToString();
                string distidsub = Session["Depot_DistID"].ToString().Substring(2, 2);
                string distid = Session["Depot_DistID"].ToString();
                string deliagent = ddlDeliveredAgnt.SelectedItem.Text;
                string godownid = ddlGodown.SelectedValue.ToString();
                if (deliagent == "Lead Society")
                {
                    //qry = "select distinct iDo.Delivery_order_no FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "'";
                    // qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    // qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' ";
                    //  qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) ";
                    //  qry = "select distinct iDo.Delivery_order_no FROM MPSCSCSVR.MPSCSC.dbo.[issue_against_do] iDo join MPSCSCSVR.MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSCSVR.MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) ";
                    qry = "select distinct iDo.Delivery_order_no FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type in ('L','LF') and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSCSVR.MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                }
                else if (deliagent == "FPS")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='F' order by iDo.Delivery_order_no";
                }
                else if (deliagent == "Depositor")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='O' order by iDo.Delivery_order_no";
                }
                else if (deliagent == "MPSCSC")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='O' order by iDo.Delivery_order_no";
                }
                else if (deliagent == "Door Step Delivery")
                {
                    if (rbdate.Checked)
                    {
                        ddlDONumber.Visible = false;
                        return;
                    }
                    else if (rbdo.Checked)
                    {
                        qry = "select distinct iDo.Delivery_order_no  FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and iDo.DOType in ('DS','TO') and iDo.Godown='" + godownid + "' and (select count(trans_id) from MPSCSCSVR.MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') order by iDo.Delivery_order_no";
                        //loc    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and iDo.DOType in ('DS','TO') and iDo.Godown='" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') order by iDo.Delivery_order_no";
                    }
                }
                else if (deliagent == "MPSCSC(Miller)")
                {
                    qry = "select distinct iDo.Delivery_order_no FROM MPSCSCSVR.MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSCSVR.MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSCSVR.MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                    //qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                }

                else
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] iDo join [MPSCSCSVR].MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' order by iDo.Delivery_order_no";
                    // qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L'";
                }
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlDeliveredAgnt.SelectedValue.ToString() != "1")
                    {
                        trdo.Visible = true;
                        ddlDONumber.Visible = true;
                        lblDONo.Visible = true;
                    }
                    else
                    {
                        trdo.Visible = false;
                    }
                    ddlDONumber.DataSource = ds.Tables[0];
                    ddlDONumber.DataTextField = "Delivery_order_no";
                    ddlDONumber.DataValueField = "Delivery_order_no";
                    ddlDONumber.DataBind();
                    ddlDONumber.Items.Insert(0, " --Select--");
                }
                else
                {
                    Gridclear();
                    divMPSCSCTCData.Visible = false;
                    Rodetails.Visible = false;
                    Stockdetails.Visible = false;
                    Issuedetails.Visible = false;
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    ddlDONumber.DataSource = null;
                    ddlDONumber.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Delivery Order found!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('error in fillDO()!');", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGridMPSCSCFPSData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                // for miller 29-01-15.....
                if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                {
                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";

                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(mm.Miller_Name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSCSVR.MPSCSC.dbo.[issue_against_OpenSale_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSCSVR.MPSCSC.dbo.[OpenSale_DO] DOM on  IDO.district_code=DOM.district_code and  IDO.delivery_order_no=DOM.delivery_order_no JOIN MPSCSCSVR.mpscsc.dbo.Miller_Master as mm on DOM.Partyname=mm.Miller_ID join tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id   where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and IDO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(mm.Miller_Name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_OpenSale_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSC.dbo.[OpenSale_DO] DOM on  IDO.district_code=DOM.district_code and  IDO.delivery_order_no=DOM.delivery_order_no JOIN mpscsc.dbo.Miller_Master as mm on DOM.Partyname=mm.Miller_ID join tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id   where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and IDO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    cmd = new SqlCommand(qry, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Rodetails.Visible = true;
                        GvDOFPS_on_Fetch.DataSource = ds;
                        GvDOFPS_on_Fetch.DataBind();
                        GvDOFPS_on_Fetch.HeaderRow.Cells[0].Visible = false;
                        GvDOFPS_on_Fetch.HeaderRow.Cells[13].Visible = false;
                        for (int i = 0; i < GvDOFPS_on_Fetch.Rows.Count; i++)
                        {
                            GvDOFPS_on_Fetch.Rows[i].Cells[0].Visible = false;
                            GvDOFPS_on_Fetch.Rows[i].Cells[13].Visible = false;

                        }
                        ddlRecDistrict.Items.Clear();
                        ddlRecDepot.Items.Clear();
                        txtTransId.Text = "";
                        txtTransId.Text = ds.Tables[0].Rows[0]["Trans_Id"].ToString();
                        txttruckno.Text = ds.Tables[0].Rows[0]["Gate_pass"].ToString();
                        lblcom.Text = ds.Tables[0].Rows[0]["commodity_id"].ToString();
                        if (lblcom.Text != "")
                        {
                            WHRDetails();
                        }
                    }
                    else
                    {
                        Gridclear();
                        divMPSCSCTCData.Visible = false;
                        Rodetails.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                        lblIssusedQty.Text = "0";
                        Issuedetails.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Issue Against DO Found in CSMS Module')", true);
                    }
                }
                else if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
                {

                    //for door step delivery.....

                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";
                    //old srv  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join [MPSCSCSVR].MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //loc  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)"; 
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details) and iDo.DOType in ('DS','TO')";
                    cmd = new SqlCommand(qry, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //  Rodetails.Visible = true;
                        GvDOFPSDsd.DataSource = ds;
                        GvDOFPSDsd.DataBind();
                        GvDOFPSDsd.HeaderRow.Cells[0].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[17].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[13].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[14].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[15].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[16].Visible = false;
                        for (int i = 0; i < GvDOFPSDsd.Rows.Count; i++)
                        {
                            GvDOFPSDsd.Rows[i].Cells[0].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[17].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[13].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[14].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[15].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[16].Visible = false;
                        }
                        ddlRecDistrict.Items.Clear();
                        ddlRecDepot.Items.Clear();
                        txtTransId.Text = "";
                        txtTransId.Text = ds.Tables[0].Rows[0]["Trans_Id"].ToString();
                        //txttruckno.Text = ds.Tables[0].Rows[0]["Gate_pass"].ToString();
                        //lblcom.Text = ds.Tables[0].Rows[0]["commodity"].ToString();
                        //if (lblcom.Text != "")
                        //{
                        //    WHRDetails();
                        //}
                    }
                    else
                    {
                        Gridclear();
                        divMPSCSCTCData.Visible = false;
                        Rodetails.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                        lblIssusedQty.Text = "0";
                        Issuedetails.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Issue Against DO Found in CSMS Module')", true);
                    }

                }
                //for other than miller and door step do.......29.01.2015
                else
                {

                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(ido.Issue_to_LS_name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSCSVR.MPSCSC.dbo.[issue_against_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join  MPSCSCSVR.MPSCSC.dbo.delivery_order_mpscsc DOM on  IDO.district_code=DOM.district_code and IDO.issueCentre_code=DOM.issueCentre_code and IDO.delivery_order_no=DOM.delivery_order_no JOIN tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(ido.Issue_to_LS_name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty' ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_do] IDO left  join MPSCSC.dbo.tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSC.dbo.delivery_order_mpscsc DOM on  IDO.district_code=DOM.district_code and IDO.issueCentre_code=DOM.issueCentre_code and IDO.delivery_order_no=DOM.delivery_order_no JOIN tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    cmd = new SqlCommand(qry, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Rodetails.Visible = true;
                        GvDOFPS_on_Fetch.DataSource = ds;
                        GvDOFPS_on_Fetch.DataBind();
                        GvDOFPS_on_Fetch.HeaderRow.Cells[0].Visible = false;
                        GvDOFPS_on_Fetch.HeaderRow.Cells[13].Visible = false;
                        for (int i = 0; i < GvDOFPS_on_Fetch.Rows.Count; i++)
                        {
                            GvDOFPS_on_Fetch.Rows[i].Cells[0].Visible = false;
                            GvDOFPS_on_Fetch.Rows[i].Cells[13].Visible = false;

                        }
                        ddlRecDistrict.Items.Clear();
                        ddlRecDepot.Items.Clear();
                        txtTransId.Text = "";
                        txtTransId.Text = ds.Tables[0].Rows[0]["Trans_Id"].ToString();
                        txttruckno.Text = ds.Tables[0].Rows[0]["Gate_pass"].ToString();
                        lblcom.Text = ds.Tables[0].Rows[0]["commodity_id"].ToString();
                        if (lblcom.Text != "")
                        {
                            WHRDetails();
                        }
                    }
                    else
                    {
                        Gridclear();
                        divMPSCSCTCData.Visible = false;
                        Rodetails.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                        lblIssusedQty.Text = "0";
                        Issuedetails.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Issue Against DO Found in CSMS Module')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again!');", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void ckboxtrucklist_CheckedChanged(object sender, EventArgs e)
    {
        lblIssusedQty.Text = "0";
        int _checkboxstatus = -1;
        int i;
        for (i = 0; i < GvuFillMPSCSCTCData.Rows.Count; i++)
        {
            if (((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
            {
                if (_checkboxstatus == -1)
                {
                    _checkboxstatus = _checkboxstatus + 2;
                }
                else
                {
                    txttruckno.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Only one selection is allowed !')", true);
                    ((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist")).Checked = false;
                }
            }

        }
        if (_checkboxstatus == 1)
        {
            for (i = 0; i < GvuFillMPSCSCTCData.Rows.Count; i++)
            {
                if (ddlDeliveredAgnt.SelectedItem.Text != "Other Depot(By Rack)")
                {
                    if (((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                    {
                        ViewState["TONo"] = GvuFillMPSCSCTCData.Rows[i].Cells[1].Text;
                        txttruckno.Text = GvuFillMPSCSCTCData.Rows[i].Cells[4].Text;
                        ViewState["TCNo"] = GvuFillMPSCSCTCData.Rows[i].Cells[2].Text;
                        ViewState["Bags"] = GvuFillMPSCSCTCData.Rows[i].Cells[6].Text;
                        ViewState["Qty"] = GvuFillMPSCSCTCData.Rows[i].Cells[7].Text;
                        lblcom.Text = GvuFillMPSCSCTCData.Rows[i].Cells[11].Text;
                        fillMPCSCSOTData();
                        //ddlCommodity.Enabled = false;
                        decimal iqty = 0;
                        iqty += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[7].Text);
                        lblIssusedQty.Text = iqty.ToString();
                    }
                }
                else
                {
                    if (((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                    {
                        ViewState["TONo"] = GvuFillMPSCSCTCData.Rows[i].Cells[1].Text;
                        txttruckno.Text = GvuFillMPSCSCTCData.Rows[i].Cells[4].Text;
                        ViewState["TCNo"] = GvuFillMPSCSCTCData.Rows[i].Cells[2].Text;
                        ViewState["Bags"] = GvuFillMPSCSCTCData.Rows[i].Cells[6].Text;
                        ViewState["Qty"] = GvuFillMPSCSCTCData.Rows[i].Cells[7].Text;
                        lblcom.Text = GvuFillMPSCSCTCData.Rows[i].Cells[11].Text;
                        fillMPCSCSOTDataRack();
                        //ddlCommodity.Enabled = false;
                        decimal iqty = 0;
                        iqty += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[7].Text);
                        lblIssusedQty.Text = iqty.ToString();
                    }

                }

            }
        }
        else
        {
            lblnotfound.Visible = false;
            lblnotfound.Text = "";
            txttruckno.Text = "";
            lblIssusedQty.Text = "0";
            ddlRecDistrict.Enabled = true;
            ddlRecDepot.Enabled = true;
        }
    }

    protected void fillMPCSCSOTData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_Truck_challan] as TC join [MPSCSCSVR].[MPSCSC].[dbo].tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["Depot_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            // qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_Truck_challan] as TC join [MPSCSC].[dbo].tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["Depot_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ddlGodown.Items.Count > 1)
                {
                    for (int g = 0; g < ddlGodown.Items.Count; g++)
                    {
                        if (ddlGodown.Items[g].Text.ToUpper() == ds1.Tables[0].Rows[0]["Godown_Name"].ToString().ToUpper())
                        {
                            ddlGodown.ClearSelection();
                            ddlGodown.Items[g].Selected = true;
                        }
                    }
                }
                else
                {
                    ddlGodown.Items.Clear();
                    ddlGodown.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Godown_Name"].ToString(), ds1.Tables[0].Rows[0]["Dispatch_Godown"].ToString()));
                }
                FillRecDist();
                if (ddlRecDistrict.Items.Count > 1)
                {
                    for (int d = 0; d < ddlRecDistrict.Items.Count; d++)
                    {
                        if (ddlRecDistrict.Items[d].Value == "23" + ds1.Tables[0].Rows[0]["Sendto_District"].ToString())
                        {
                            ddlRecDistrict.ClearSelection();
                            ddlRecDistrict.Items[d].Selected = true;
                            ddlRecDistrict.Enabled = false;
                        }
                    }
                }
                else
                {
                    ddlRecDistrict.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Sendto_District"].ToString(), ds1.Tables[0].Rows[0]["Sendto_District"].ToString()));
                    ddlRecDistrict.ClearSelection();
                    ddlRecDistrict.Items[0].Selected = true;
                    ddlRecDistrict.Enabled = true;
                }
                FillRecDepot(ds1.Tables[0].Rows[0]["Sendto_IC"].ToString());

                if (ddlRecDepot.Items.Count > 1)
                {
                    for (int r = 0; r < ddlRecDepot.Items.Count; r++)
                    {
                        if (ddlRecDepot.Items[r].Value == ds1.Tables[0].Rows[0]["Sendto_IC"].ToString())
                        {
                            ddlRecDepot.ClearSelection();
                            ddlRecDepot.Items[r].Selected = true;
                            ddlRecDepot.Enabled = false;
                        }
                    }
                }
                else
                {
                    ddlRecDepot.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Sendto_IC"].ToString(), ds1.Tables[0].Rows[0]["Sendto_IC"].ToString()));
                    ddlRecDepot.ClearSelection();
                    ddlRecDepot.Items[0].Selected = true;
                    ddlRecDepot.Enabled = true;
                }
                WHRDetails();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('nvalid Record or some data is missing !')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillMPCSCSOTDataRack()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Rack_NO as Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSCSVR].[MPSCSC].[dbo].[SCSC_RailHead_TC] as TC join tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["Depot_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            //qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Rack_NO as Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] as TC left join tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["Depot_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ddlGodown.Items.Count > 1)
                {
                    for (int g = 0; g < ddlGodown.Items.Count; g++)
                    {
                        if (ddlGodown.Items[g].Text.ToUpper() == ds1.Tables[0].Rows[0]["Godown_Name"].ToString().ToUpper())
                        {
                            ddlGodown.ClearSelection();
                            ddlGodown.Items[g].Selected = true;
                        }
                    }
                }
                else
                {
                    ddlGodown.Items.Clear();
                    ddlGodown.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Godown_Name"].ToString(), ds1.Tables[0].Rows[0]["Dispatch_Godown"].ToString()));
                }
                FillRecDist();
                if (ddlRecDistrict.Items.Count > 1)
                {
                    for (int d = 0; d < ddlRecDistrict.Items.Count; d++)
                    {
                        if (ddlRecDistrict.Items[d].Value == "23" + ds1.Tables[0].Rows[0]["Sendto_District"].ToString())
                        {
                            ddlRecDistrict.ClearSelection();
                            ddlRecDistrict.Items[d].Selected = true;
                            ddlRecDistrict.Enabled = false;
                        }
                    }
                }
                else
                {
                    ddlRecDistrict.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Sendto_District"].ToString(), ds1.Tables[0].Rows[0]["Sendto_District"].ToString()));
                    ddlRecDistrict.ClearSelection();
                    ddlRecDistrict.Items[0].Selected = true;
                    ddlRecDistrict.Enabled = true;
                }
                // FillRecDepot(ds1.Tables[0].Rows[0]["Sendto_IC"].ToString());

                if (ddlRecDepot.Items.Count > 1)
                {
                    for (int r = 0; r < ddlRecDepot.Items.Count; r++)
                    {
                        if (ddlRecDepot.Items[r].Value == ds1.Tables[0].Rows[0]["Sendto_IC"].ToString())
                        {
                            ddlRecDepot.ClearSelection();
                            ddlRecDepot.Items[r].Selected = true;
                            ddlRecDepot.Enabled = false;
                        }
                    }
                }
                else
                {
                    ddlRecDepot.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Sendto_IC"].ToString(), ds1.Tables[0].Rows[0]["Sendto_IC"].ToString()));
                    ddlRecDepot.ClearSelection();
                    ddlRecDepot.Items[0].Selected = true;
                    ddlRecDepot.Enabled = true;
                }
                WHRDetails();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('nvalid Record or some data is missing !')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void FillRecDist()
    {
        try
        {
            ddlRecDistrict.Items.Clear();
            // qry = "select District_id,District_Name FROM [MPSCSC].dbo.tbl_MetaData_DISTRICT order by District_Name";
            qry = "select District_id,District_Name FROM [MPSCSCSVR].[MPSCSC].dbo.tbl_MetaData_DISTRICT order by District_Name";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRecDistrict.DataSource = ds;
                ddlRecDistrict.DataTextField = "District_Name";
                ddlRecDistrict.DataValueField = "District_id";
                ddlRecDistrict.DataBind();
                ddlRecDistrict.Items.Insert(0, "--Select--");
                ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fill rec district !')", true);
        }
    }

    protected void FillRecDepot(string Dist)
    {
        try
        {
            qry = "select DepotID,DepotName FROM [MPSCSCSVR].[MPSCSC].dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlRecDistrict.SelectedValue.ToString() + "' order by DepotName";
            // qry = "select DepotID,DepotName FROM [MPSCSC].dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlRecDistrict.SelectedValue.ToString() + "' order by DepotName";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlRecDepot.DataSource = ds;
                ddlRecDepot.DataTextField = "DepotName";
                ddlRecDepot.DataValueField = "DepotID";
                ddlRecDepot.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Depot Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fill rec depot')", true);
        }
    }

    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool calculationflag = true;
            int s;
            int Count_Rows;
            string Stack_id;
            string WHR_ID;
            Count_Rows = gdstackdetail.Rows.Count;

            for (s = 0; s < gdstackdetail.Rows.Count; s++)
            {
                if (((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked == true)
                {
                    if (((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled == true)
                    {
                       // Stack_id = gdstackdetail.Rows[s].Cells[12].Text.ToString();
                        WHR_ID = gdstackdetail.Rows[s].Cells[3].Text.ToString();
                        int Bags = int.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Text.ToString());
                        //if (Bags == int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
                        // {
                        decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString());
                        decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString());
                        if (Totissue_Wgt > Avai_Wgt)
                        {
                            //gain
                            string msg;
                            decimal bal = Totissue_Wgt - Avai_Wgt;
                            // msg = "Entered weight is more than avilable capacity";
                            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + msg + "')", true);
                            // ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text = gdstackdetail.Rows[s].Cells[6].Text.ToString();
                            ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                            calculationflag = false;
                            //  ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                            ((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Enabled = true;
                            ((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Enabled = true;
                        }
                        else if (Totissue_Wgt < Avai_Wgt)
                        {
                            // loss 
                            //    string msg;
                            //    decimal bal = Avai_Wgt - Totissue_Wgt;
                            //    //  msg = "Entered weight is less than avilable capacity";
                            //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + msg + "')", true);
                            //    // ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text = gdstackdetail.Rows[s].Cells[6].Text.ToString();
                            //    //((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                            //    ((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Text = "0";
                            //    ((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Text = bal.ToString();
                            //    ((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Enabled = false;
                        }
                        else if (Totissue_Wgt == Avai_Wgt)
                        {
                            // loss 
                            string msg;
                            decimal bal = Avai_Wgt - Totissue_Wgt;
                            //  msg = "Entered weight is less than avilable capacity";
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + msg + "')", true);
                            // ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text = gdstackdetail.Rows[s].Cells[6].Text.ToString();
                            //((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Text = "0";
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Text = "0";
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Enabled = false;
                        }
                        if (Bags < int.Parse(gdstackdetail.Rows[s].Cells[4].Text.ToString()))
                        {
                            if (decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString()) == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid No of Bags/Weight Issued from stack')", true);
                                ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                                calculationflag = false;
                            }
                            else
                            {
                                Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString());
                                Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString());
                                if (Totissue_Wgt > Avai_Wgt)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entered weight cannot be greater than the Available weight in the stack until Bags are equal')", true);
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Enteret weight cannot be greater than the Available weight in the stack until Bags are equal";
                                    ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                                    calculationflag = false;
                                }
                                else
                                {
                                    calculationflag = true;
                                }
                            }
                        }
                        else if (Bags == int.Parse(gdstackdetail.Rows[s].Cells[4].Text.ToString()))
                        {
                            if (Totissue_Wgt > Avai_Wgt)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entered weight cannot be greater than the Available weight in the stack until Bags are equal')", true);
                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                lblmsg.Text = "Enteret weight cannot be greater than the Available weight in the stack until Bags are equal";
                                ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                                calculationflag = false;
                            }
                            else
                            {
                                calculationflag = true;
                            }
                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bags in the stack  cannot be greater than the Available Bags in the stack')", true);
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Bags in the stack  cannot be greater than the Available Bags in the stack";
                            ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                            calculationflag = false;
                        }
                        //}
                    }
                    else
                    {
                        ((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled = true;
                        ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Enabled = true;
                    }


                    // disabling the controls in the gridview once the 
                    if (calculationflag == true)
                    {
                        int i;
                        int totalbags = 0;
                        decimal totalwt = 0;
                        for (i = 0; i < gdstackdetail.Rows.Count; i++)
                        {
                            if (((CheckBox)gdstackdetail.Rows[i].FindControl("ckstack")).Checked == true)
                            {
                                totalbags = totalbags + int.Parse(((TextBox)gdstackdetail.Rows[i].FindControl("txtbagnumber")).Text.ToString());
                                totalwt = totalwt + decimal.Parse(((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Text.ToString()) + decimal.Parse(((TextBox)gdstackdetail.Rows[i].FindControl("txtgain")).Text.ToString()) - decimal.Parse(((TextBox)gdstackdetail.Rows[i].FindControl("txtloss")).Text.ToString());
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtbagnumber")).Enabled = false;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Enabled = false;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtgain")).Enabled = false;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtloss")).Enabled = false;
                            }
                            else
                            {
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtbagnumber")).Enabled = true;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Enabled = true;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtgain")).Enabled = true;
                                ((TextBox)gdstackdetail.Rows[i].FindControl("txtloss")).Enabled = true;
                            }

                        }
                        if (ddlDepositorType.SelectedItem.Text != "Institution")
                        {
                            txtissuedbags.Text = totalbags.ToString();
                            txtissuedwt.Text = totalwt.ToString();
                            lblIssusedQty.Text = totalwt.ToString();
                            lblIssusedBags.Text = totalbags.ToString();
                        }
                        else
                        {
                            txtissuedbags.Text = totalbags.ToString();
                            txtissuedwt.Text = totalwt.ToString();

                        }
                    }
                }
                else
                {
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled = true;
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Enabled = true;
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Enabled = true;
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Enabled = true;
                    calculationflag = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in ckstack_CheckedChanged')", true);
        }
    }

    private void Empty()
    {
        btnsave.Enabled = false;
        txtissuedbags.Text = null;
        txtissuedwt.Text = null;
        txtmoisture.Text = null;
        txtTruckDetails.Text = null;
        UxTrans.ClearSelection();
        ddlVehicleType.ClearSelection();
        ddl1.ClearSelection();
        ddl2.ClearSelection();
        ddl3.ClearSelection();
    }

    protected void CheckRo()
    {
        try
        {
            decimal roqty = 0;
            if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPSCSC")
            {
                if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot")
                {
                    if (GvuFillMPSCSCTCData.Rows.Count > 0)
                    {
                        for (int t = 0; t < GvuFillMPSCSCTCData.Rows.Count; t++)
                        {
                            if (((CheckBox)GvuFillMPSCSCTCData.Rows[t].FindControl("ckboxtrucklist")).Checked == true)
                            {
                                roqty = roqty + decimal.Parse(GvuFillMPSCSCTCData.Rows[t].Cells[7].Text);
                            }

                        }
                        if (roqty != decimal.Parse(txtissuedwt.Text))
                        {
                            calflag = 1;
                        }
                    }
                    else
                    {
                        calflag = 0;
                    }
                }
                else
                {
                    if (GvDOFPS_on_Fetch.Rows.Count > 0)
                    {
                        for (int k = 0; k < GvDOFPS_on_Fetch.Rows.Count; k++)
                        {
                            if (((CheckBox)GvDOFPS_on_Fetch.Rows[k].FindControl("ckboxDOlist")).Checked == true)
                            {
                                roqty = roqty + decimal.Parse(GvDOFPS_on_Fetch.Rows[k].Cells[6].Text);
                            }
                        }
                        if (roqty != decimal.Parse(txtissuedwt.Text))
                        {
                            calflag = 1;
                        }
                    }
                    else
                    {
                        calflag = 0;
                    }
                }
            }
            else
            {
                calflag = 0;

            }

        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Some error has occured, try again!'); </script> ");
        }
    }

    private void Gridclear()
    {
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
        GvuFillMPSCSCTCData.DataSource = null;
        GvuFillMPSCSCTCData.DataBind();
        GvDOFPS_on_Fetch.DataSource = null;
        GvDOFPS_on_Fetch.DataBind();
    }

    protected void ddlDONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblIssusedQty.Text = "0";
        Gridclear();
        fillGridMPSCSCFPSData();
    }

    protected void ddlRecDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillRecDepot(ddlRecDistrict.SelectedValue);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No"; //Session["RefreshButton"];
    }

    protected void btnNewMC_Click(object sender, EventArgs e)
    {
        try
        {
            Empty();
            Gridclear();
            gdstackdetail.DataSource = null;
            gdstackdetail.DataBind();
            Session["RefreshButton"] = "No";
            Response.Redirect("WLC_FRM_05_Delivery_GatePass_Details.aspx");
        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                decimal whrqt = Convert.ToDecimal(txtissuedwt.Text);
                decimal DOqt = Convert.ToDecimal(lblIssusedQty.Text);

                if (whrqt == DOqt)
                {
                    CheckRo();
                    int l;
                    int _numberofstackschecked = 0;
                    string StockDeliveryOrderGatePass_Id = string.Empty;
                    string ClientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    for (l = 0; l < gdstackdetail.Rows.Count; l++)
                    {
                        if (((CheckBox)gdstackdetail.Rows[l].FindControl("ckstack")).Checked == true)
                        {
                            _numberofstackschecked = 1;
                            break;
                        }
                    }
                    if (_numberofstackschecked == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No stack is checked!')", true);
                    }
                    else if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                    {
                        Response.Redirect("PendingGatePassOfDelivery.aspx?PopMsg=The Record Already Saved!!Do Not Refresh again!!!Select GatePass to modify!");
                    }
                    else if (txtissuedbags.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No of bags field cannot be empty!')", true);
                    }
                    else if (txtissuedwt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Weight field cannot be empty!')", true);
                    }
                    else if (calflag == 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('For MPSCSC, Lead Society/FPS Qty or Qty of selected Truck Challan should be equal to Issued Quantity!')", true);
                    }
                    else if (TextBox2.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Gatepass date!')", true);
                    }
                    else
                    {
                        _numberofstackschecked = 0;
                        string _StockDeliveryOrderGatePass_Id = string.Empty;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        #region sqltrans
                        sqltran = con.BeginTransaction();
                        try
                        {

                            /////////////////////////////////////////////////////////////////////////////

                            qry = "SELECT isnull(max([AutoId]),0) FROM [Intergrated_MP_STORAGE].[dbo].[MenualWhrDeliveryDtl] where BranchId='" + Session["BranchId"].ToString() + "' ";
                            cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                            string str4 = cmd.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str4) != 0)
                            {
                                GP_SL_No = Convert.ToString(Convert.ToInt64(str4) + 1);
                                string Depotid = Session["Depot_DepotID"].ToString();
                                string BranchId = Session["BranchId"].ToString();
                                GateP_No = 'M'+BranchId + System.DateTime.Now.Date.ToString("yy") + System.DateTime.Now.Date.ToString("MM") + GP_SL_No;
                            }
                            else
                            {
                                GP_SL_No = "1";
                                string Depotid = Session["Depot_DepotID"].ToString();
                                string BranchId = Session["BranchId"].ToString();
                                GateP_No = 'M'+BranchId + System.DateTime.Now.Date.ToString("yy") + System.DateTime.Now.Date.ToString("MM") + GP_SL_No;
                            }
                            //////////////////////////////////////////////////////////////////////////////
                                if (ddlDeliveredAgnt.SelectedItem.Text != "Door Step Delivery")
                                {
                                    for (int k = 0; k < GvDOFPS_on_Fetch.Rows.Count; k++)
                                    {
                                        if (((CheckBox)GvDOFPS_on_Fetch.Rows[k].FindControl("ckboxDOlist")).Checked == true)
                                        {
                                            cmd = new SqlCommand("sp_Insert_RO_Details", con, sqltran);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@State_ID", "23");
                                            cmd.Parameters.AddWithValue("@Trans_ID", GvDOFPS_on_Fetch.Rows[k].Cells[13].Text);
                                            cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                            cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                                            cmd.Parameters.AddWithValue("@Release_Order_No", ddlDONumber.SelectedValue);
                                            cmd.Parameters.AddWithValue("@Release_Order_Date", getDate_MDY(GvDOFPS_on_Fetch.Rows[k].Cells[9].Text));
                                            cmd.Parameters.AddWithValue("@RO_Quantity", GvDOFPS_on_Fetch.Rows[k].Cells[7].Text);
                                            cmd.Parameters.AddWithValue("@FPSId", GvDOFPS_on_Fetch.Rows[k].Cells[0].Text);
                                            cmd.Parameters.AddWithValue("@FPS", GvDOFPS_on_Fetch.Rows[k].Cells[2].Text);
                                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                            cmd.Parameters.AddWithValue("@Truckno", GvDOFPS_on_Fetch.Rows[k].Cells[5].Text);
                                            cmd.Parameters.AddWithValue("@RO_Quantity_issued", GvDOFPS_on_Fetch.Rows[k].Cells[6].Text);
                                            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                                            if (GvDOFPS_on_Fetch.Rows[k].Cells[10].Text != "NA")
                                            {
                                                cmd.Parameters.AddWithValue("@allotment_month", int.Parse(GvDOFPS_on_Fetch.Rows[k].Cells[10].Text));
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@allotment_month", int.Parse("0"));

                                            }
                                            if (GvDOFPS_on_Fetch.Rows[k].Cells[11].Text != "NA")
                                            {
                                                cmd.Parameters.AddWithValue("@allotment_year", int.Parse(GvDOFPS_on_Fetch.Rows[k].Cells[11].Text));

                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@allotment_year", int.Parse("0"));
                                            }
                                            cmd.Parameters.AddWithValue("@CreatedBy", ClientIP.ToString());
                                            int res = cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                        }
                                    }
                                }
                                else
                                {

                                    for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                                    {
                                        if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistdsd")).Checked == true)
                                        {
                                            cmd = new SqlCommand("sp_Insert_RO_Details", con, sqltran);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@State_ID", "23");
                                            cmd.Parameters.AddWithValue("@Trans_ID", GvDOFPSDsd.Rows[k].Cells[13].Text);
                                            cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                            cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                                            cmd.Parameters.AddWithValue("@Release_Order_No", GvDOFPSDsd.Rows[k].Cells[18].Text);
                                            cmd.Parameters.AddWithValue("@Release_Order_Date", getDate_MDY(GvDOFPSDsd.Rows[k].Cells[9].Text));
                                            cmd.Parameters.AddWithValue("@RO_Quantity", GvDOFPSDsd.Rows[k].Cells[7].Text);
                                            cmd.Parameters.AddWithValue("@FPSId", GvDOFPSDsd.Rows[k].Cells[0].Text);
                                            cmd.Parameters.AddWithValue("@FPS", GvDOFPSDsd.Rows[k].Cells[2].Text);
                                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
                                            if (GvDOFPSDsd.Rows[k].Cells[5].Text != null)
                                            {
                                                cmd.Parameters.AddWithValue("@Truckno", txttruckno.Text.ToString());
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@Truckno", txttruckno.Text.ToString());

                                            }
                                            //cmd.Parameters.AddWithValue("@Truckno", GvDOFPSDsd.Rows[k].Cells[5].Text);
                                            cmd.Parameters.AddWithValue("@RO_Quantity_issued", GvDOFPSDsd.Rows[k].Cells[6].Text);
                                            cmd.Parameters.AddWithValue("@allotment_month", int.Parse(GvDOFPSDsd.Rows[k].Cells[10].Text));
                                            cmd.Parameters.AddWithValue("@allotment_year", int.Parse(GvDOFPSDsd.Rows[k].Cells[11].Text));
                                            cmd.Parameters.AddWithValue("@CreatedBy", ClientIP.ToString());
                                            int res = cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                        }
                                    }


                                }
                                // to inser other depot value(by road/ by rack)

                                for (int k = 0; k < GvuFillMPSCSCTCData.Rows.Count; k++)
                                {
                                    if (((CheckBox)GvuFillMPSCSCTCData.Rows[k].FindControl("ckboxtrucklist")).Checked == true)
                                    {
                                        cmd = new SqlCommand("insert_OtherDepotDetails", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@SendingDist", Session["Depot_DistID"].ToString());
                                        cmd.Parameters.AddWithValue("@SendingDepot", Session["Depot_DepotID"].ToString());
                                        cmd.Parameters.AddWithValue("@TONum", GvuFillMPSCSCTCData.Rows[k].Cells[1].Text);
                                        cmd.Parameters.AddWithValue("@TruckNum", GvuFillMPSCSCTCData.Rows[k].Cells[4].Text);
                                        cmd.Parameters.AddWithValue("@ChallanDate", GvuFillMPSCSCTCData.Rows[k].Cells[5].Text);
                                        cmd.Parameters.AddWithValue("@Transpoter", UxTrans.SelectedItem.Text);
                                        cmd.Parameters.AddWithValue("@Bags", Convert.ToDecimal(GvuFillMPSCSCTCData.Rows[k].Cells[6].Text));
                                        cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(GvuFillMPSCSCTCData.Rows[k].Cells[7].Text));
                                        cmd.Parameters.AddWithValue("@Godown", ddlGodown.SelectedValue.ToString());
                                        cmd.Parameters.AddWithValue("@Gatepass ", GateP_No);
                                        cmd.Parameters.AddWithValue("@ChallanNum", GvuFillMPSCSCTCData.Rows[k].Cells[2].Text);
                                        cmd.Parameters.AddWithValue("@Commodity", GvuFillMPSCSCTCData.Rows[k].Cells[11].Text);
                                        cmd.Parameters.AddWithValue("@SendToDist", GvuFillMPSCSCTCData.Rows[k].Cells[12].Text);
                                        cmd.Parameters.AddWithValue("@SendToDepot", GvuFillMPSCSCTCData.Rows[k].Cells[13].Text);
                                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                        //cmd.Parameters.AddWithValue("@CreatedDate",Convert.ToDateTime(DateTime.Now.Date.ToString()));
                                        int res = cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                    }
                                }



                                //end of other depot entries
                                decimal _issedwt = 0;
                                int _issedBags = 0;
                                for (int ino = 0; ino < gdstackdetail.Rows.Count; ino++)
                                {
                                    if (((CheckBox)gdstackdetail.Rows[ino].FindControl("ckstack")).Checked == true)
                                    {
                                        _issedwt = _issedwt + decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtweight")).Text.ToString()) + decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString()) - decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString());
                                        _issedBags = _issedBags + int.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Text.ToString());
                                    }
                                }

                               

                                //for  3rd table ie:tbl_Delivery_Stacking_Details_GatePass(StockDeliveryOrderGatePass_Id,GatePass_No)

                                #region thirdtable

                                string stkid = "";
                                string whrid = "";
                                string Godid = "";
                                decimal _chkwt;
                                for (int ino = 0; ino < gdstackdetail.Rows.Count; ino++)
                                {
                                    if (((CheckBox)gdstackdetail.Rows[ino].FindControl("ckstack")).Checked == true)
                                    {
                                        if (((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Enabled == false)
                                        {
                                          //  Godid = gdstackdetail.Rows[ino].Cells[11].Text.ToString();
                                           // stkid = gdstackdetail.Rows[ino].Cells[12].Text.ToString();
                                            whrid = gdstackdetail.Rows[ino].Cells[3].Text.ToString();
                                            decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtweight")).Text.ToString()) + decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString()) - decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString());
                                            decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString());
                                            int Bags = int.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Text.ToString());
                                            decimal gainqty = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString());
                                            decimal lossqty = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString());
                                            if (Bags > int.Parse(gdstackdetail.Rows[ino].Cells[4].Text.ToString()))
                                            {
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issued bags are more than No of bags available')", true);
                                                lblmsg.ForeColor = System.Drawing.Color.Red;
                                                lblmsg.Text = "Issued bags are more than No of bags available";

                                            }

                                            else if (Bags <= int.Parse(gdstackdetail.Rows[ino].Cells[4].Text.ToString()))
                                            {
                                                // insert when No loss or Gain...
                                              //  mid=gdstackdetail.Rows[ino].Cells[13].Text.ToString();
                                                cmd = new SqlCommand("insert into [MenualWhrDeliveryDtl] values ('" + Session["Depot_DistID"].ToString() + "','" + Session["BranchId"].ToString() + "','" + GateP_No + "','" + ddlGodown.SelectedValue.ToString() + "','" + Bags + "','" + Totissue_Wgt + "','" + whrid + "','" + ((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString() + "','" + ((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString() + "','" + ClientIP + "',getdate(),'" + GP_SL_No + "','"+gdstackdetail.Rows[ino].Cells[13].Text.ToString()+"')", con, sqltran);
                                                cmd.CommandType = CommandType.Text;
                                                cmd.ExecuteNonQuery();
                                                cmd.Dispose();
                                                issuegainwt = 0;
                                                issuelosswt = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            
                            sqltran.Commit();
                        #endregion

                            Empty();
                            Gridclear();

                            Session["RefreshButton"] = "Yes";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                            Session["dtRo"] = null;
                            Session["Gtpass"] = GateP_No;
                           // Session["mid"] = mid;

                            //For gate pass link and pop up
                            trlnk.Visible = true;
                            string Roid = "GatepassFrmMenualWHR.aspx.aspx?src=RO&vu=" + GateP_No;
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<script>");
                            sb.Append("window.open(");
                            sb.Append("'" + Roid + "'");
                            sb.Append(",'MyWindow', 'height=800,width=780');");
                            sb.Append("</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
                            btnNewMC.Enabled = true;
                            btnsave.Enabled = false;
                            Rodetails.Visible = false;
                            divMPSCSCTCData.Visible = false;
                            Issuedetails.Visible = false;
                            Stockdetails.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            sqltran.Rollback();
                            lblmsg.Text = ex.ToString();
                        }
                        finally
                        {
                            sqltran.Dispose();
                            con.Close();
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Qty issued not equal to DO qty ";

                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }

    private void WHRDetails()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (ddlDeliveredAgnt.SelectedItem.Text != "Door Step Delivery")
            {
                try
                {
                    string query = "";
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    string BranchID = Session["BranchId"].ToString();
                    if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                    {
                        query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo', mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + BranchID + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                        //query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id in ('35','22','6') group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
                    }
                    //else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                    //{
                    //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','7') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                    //}
                    //else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                    //{
                    //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    //}
                    //else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                    //{
                    //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    //}
                    else
                    {
                        query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo', mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + BranchID + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                        //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["Depot_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                        // query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
                    }
                    cmd = new SqlCommand(query, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblnotfound.Visible = false;
                        lblnotfound.Text = "";
                        gdstackdetail.DataSource = ds;
                        gdstackdetail.DataBind();
                        if (gdstackdetail.Rows.Count > 0)
                        {
                            Issuedetails.Visible = true;
                            Stockdetails.Visible = true;
                            gdstackdetail.HeaderRow.Cells[9].Visible = false;
                            gdstackdetail.HeaderRow.Cells[12].Visible = false;
                            for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                            {
                                gdstackdetail.Rows[j].Cells[9].Visible = false;
                                gdstackdetail.Rows[j].Cells[12].Visible = false;
                                
                            }
                        }
                        if (gdstackdetail.Rows.Count == 0)
                        {
                            Issuedetails.Visible = false;
                            Stockdetails.Visible = false;
                            lblStockforIssue.Visible = false;
                            btnsave.Enabled = false;
                        }
                        else
                        {
                            lblStockforIssue.Visible = true;
                            btnsave.Enabled = true;
                        }
                    }
                    else
                    {
                        gdstackdetail.DataSource = null;
                        gdstackdetail.DataBind();
                        lblnotfound.Visible = true;
                        lblnotfound.Text = "आपने इस गोदाम,Commodity पर कोई भी WHR नहीं बनाया है,कृपया पहले WHR बनाये !";
                        btnsave.Enabled = false;
                    }
                    ds.Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fill WHRDetails')", true);
                    lblmsg.Text =ex.Message;
                }
            }
            else
            {

                //door step delivery .......
                try
                {
                    string query = "";
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    if (rbdate.Checked)
                    {
                        godownid = ddlGodown.SelectedValue.ToString();
                        if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                        {
                            query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo',mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + Session["BranchId"].ToString() + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                        }
                        ////rice
                        //else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                        //}
                        //else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                        //}
                        //else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                        //}
                        else
                        {
                            query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo',mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + Session["BranchId"].ToString() + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                            //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["Depot_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                        }
                    }
                    else if (rbdo.Checked)
                    {
                        if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                        {
                            query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo',mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + Session["BranchId"].ToString() + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                        }
                        ////rice
                        //else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                        //}
                        //else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                        //}
                        //else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                        //{
                        //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                        //}
                        else
                        {
                            query = "select row_number() OVER (ORDER BY mw.MenualWHR) AS 'SNo',mw.[Mid],ISNULL(mw.Bags,0)-sum(isnull(mwd.Bags,0)) as avlBags ,isnull(mw.Qty,0)-SUM(isnull(mwd.Weight,0))-SUM(isnull(mwd.Gain,0))+SUM(isnull(mwd.Loss,0)) as avlQty ,mw.MenualWHR,mw.WHRDate,mw.GodownID,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=mw.Commidity) as Commodity_Name,(select Depositor_Name from dbo.tbl_MetaData_DEPOSITOR where  tbl_MetaData_DEPOSITOR.Depositor_ID= mw.Depositor) as Depositor_Name from [tbl_MenualWHR] as mw left join [MenualWhrDeliveryDtl] as mwd on mw.MenualWHR=mwd.[MenualWHR] and mw.GodownID=mwd.[GodownID] where mw.BranchID='" + Session["BranchId"].ToString() + "' and mw.GodownID='" + ddlGodown.SelectedValue.ToString() + "' group by mw.[Mid],mw.MenualWHR,mw.WHRDate,mw.GodownID ,mw.Depositor,mw.Commidity,mw.Bags,mw.Qty";
                            //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["Depot_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                        }
                    }
                    cmd = new SqlCommand(query, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblnotfound.Visible = false;
                        lblnotfound.Text = "";
                        gdstackdetail.DataSource = ds;
                        gdstackdetail.DataBind();
                        if (gdstackdetail.Rows.Count > 0)
                        {
                            Issuedetails.Visible = true;
                            Stockdetails.Visible = true;
                            gdstackdetail.HeaderRow.Cells[9].Visible = false;
                            gdstackdetail.HeaderRow.Cells[12].Visible = false;
                            for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                            {
                                gdstackdetail.Rows[j].Cells[9].Visible = false;
                                gdstackdetail.Rows[j].Cells[12].Visible = false;
                            }
                        }
                        if (gdstackdetail.Rows.Count == 0)
                        {
                            Issuedetails.Visible = false;
                            Stockdetails.Visible = false;
                            lblStockforIssue.Visible = false;
                            btnsave.Enabled = false;
                        }
                        else
                        {
                            lblStockforIssue.Visible = true;
                            btnsave.Enabled = true;
                        }
                    }
                    else
                    {
                        gdstackdetail.DataSource = null;
                        gdstackdetail.DataBind();
                        lblnotfound.Visible = true;
                        lblnotfound.Text = "आपने इस गोदाम,Commodity पर कोई भी WHR नहीं बनाया है,कृपया पहले WHR बनाये !";
                        btnsave.Enabled = false;
                    }
                    ds.Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fill WHRDetails')", true);
                    lblmsg.Text = ex.Message;
                }

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void ckboxDOlist_CheckedChanged(object sender, EventArgs e)
    {
        decimal roqty = 0;
        int bgas = 0;
        if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
        {
            //for one row selection at a time......./
            if (GvDOFPSDsd.Rows.Count > 0)
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow gv = (GridViewRow)chk.NamingContainer;
                int rownumber = gv.RowIndex;

                if (chk.Checked)
                {
                    int i;
                    for (i = 0; i <= GvDOFPSDsd.Rows.Count - 1; i++)
                    {
                        if (i != rownumber)
                        {
                            CheckBox chkcheckbox = ((CheckBox)(GvDOFPSDsd.Rows[i].FindControl("ckboxDOlistdsd")));
                            chkcheckbox.Checked = false;
                        }
                    }
                }

                //to select whr on selected values............
                for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                {
                    if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistdsd")).Checked == true)
                    {
                        //my use
                        //string a =GvDOFPSDsd.Rows[k].Cells[0].Text;
                        //string b = GvDOFPSDsd.Rows[k].Cells[1].Text;
                        //string c = GvDOFPSDsd.Rows[k].Cells[2].Text;
                        //string d = GvDOFPSDsd.Rows[k].Cells[3].Text;
                        //string eqwq = GvDOFPSDsd.Rows[k].Cells[4].Text;
                        //string ee = GvDOFPSDsd.Rows[k].Cells[5].Text;
                        //string eee = GvDOFPSDsd.Rows[k].Cells[6].Text;
                        //string eeee = GvDOFPSDsd.Rows[k].Cells[7].Text;
                        //string rrrr = GvDOFPSDsd.Rows[k].Cells[8].Text;
                        //string r = GvDOFPSDsd.Rows[k].Cells[9].Text;
                        //string t = GvDOFPSDsd.Rows[k].Cells[10].Text;
                        //string ar = GvDOFPSDsd.Rows[k].Cells[11].Text;
                        //string are = GvDOFPSDsd.Rows[k].Cells[12].Text;
                        //string ag = GvDOFPSDsd.Rows[k].Cells[13].Text;
                        //string ah = GvDOFPSDsd.Rows[k].Cells[14].Text;

                        //end of my use
                        godownid = GvDOFPSDsd.Rows[k].Cells[14].Text;
                        roqty = roqty + decimal.Parse(GvDOFPSDsd.Rows[k].Cells[6].Text);
                        bgas = bgas + int.Parse(GvDOFPSDsd.Rows[k].Cells[8].Text);

                        lblcom.Text = GvDOFPSDsd.Rows[k].Cells[15].Text;
                        txttruckno.Text = GvDOFPSDsd.Rows[k].Cells[5].Text;
                        WHRDetails();

                    }
                }
                lblIssusedQty.Text = roqty.ToString();
                lblIssusedBags.Text = bgas.ToString();
            }
        }
        else
        {
            if (GvDOFPS_on_Fetch.Rows.Count > 0)
            {
                for (int k = 0; k < GvDOFPS_on_Fetch.Rows.Count; k++)
                {
                    if (((CheckBox)GvDOFPS_on_Fetch.Rows[k].FindControl("ckboxDOlist")).Checked == true)
                    {
                        roqty = roqty + decimal.Parse(GvDOFPS_on_Fetch.Rows[k].Cells[6].Text);
                        bgas = bgas + int.Parse(GvDOFPS_on_Fetch.Rows[k].Cells[8].Text);
                    }
                }
                lblIssusedQty.Text = roqty.ToString();
                lblIssusedBags.Text = bgas.ToString();

            }

        }
    }

    private void Getgodowns()
    {
        qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y'  order by Godown_Name ";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodown.DataSource = ds.Tables[0];
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_id";
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlGodown.DataSource = null;
            ddlGodown.DataBind();
            ddlGodown.Items.Insert(0, "--Select--");
        }
    }

    private void GetDepositorType()
    {
        qry = "select Depositor_Type from tbl_MetaData_Depositor_Type where Depositor_Type='Institution'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositorType.DataSource = ds.Tables[0];
            ddlDepositorType.DataTextField = "Depositor_Type";
            ddlDepositorType.DataValueField = "Depositor_Type";
            ddlDepositorType.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('NO Depositor Type exists')", true);
        }
        if (ddlDepositorType.Items.Count > 0)
        {
            for (int d = 0; d < ddlDepositorType.Items.Count; d++)
            {
                if (ddlDepositorType.Items[d].Text == "Institution")
                {
                    ddlDepositorType.Items[d].Selected = true;
                }
            }
        }
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (ddlDeliveredAgnt.SelectedIndex != 0)
                {
                    lblIssusedQty.Text = "0";
                    GvDOFPS_on_Fetch.DataSource = null;
                    GvDOFPS_on_Fetch.DataBind();
                    gdstackdetail.DataSource = null;
                    gdstackdetail.DataBind();
                    if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPSCSC")
                    {
                        if (ddlDeliveredAgnt.SelectedValue != "0")
                        {
                            if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot")
                            {
                                fillGridMPSCSCTCData();
                                FillRecDist();
                                FillRecDepot(ddlRecDistrict.SelectedValue);
                                ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();
                            }
                            else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot(By Rack)")
                            {
                                fillGridMPSCSCTCRailData();
                                FillRecDist();
                                FillRecDepot(ddlRecDistrict.SelectedValue);
                                ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();

                            }
                            //case 2,3: FPS,Lead Society
                            //use data from dbo. do_fps and show FPS/Lead Society area
                            else// 
                            {
                                fillDO();
                                trOtherDepot.Visible = false;
                                GvuFillMPSCSCTCData.DataSource = null;
                                GvuFillMPSCSCTCData.DataBind();
                                divMPSCSCTCData.Visible = false;
                            }
                        }
                    }
                    else if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPWLC")
                    {
                        GvuFillMPSCSCTCData.DataSource = null;
                        GvuFillMPSCSCTCData.DataBind();
                        divMPSCSCTCData.Visible = false;
                        if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot")
                        {
                            trOtherDepot.Visible = true;
                            FillRecDist();
                            ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
                            FillRecDepot(ddlRecDistrict.SelectedValue);
                            ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();
                        }
                        else
                        {
                            trOtherDepot.Visible = false;
                        }

                    }
                    else
                    {
                        GvDOFPS_on_Fetch.DataSource = null;
                        GvDOFPS_on_Fetch.DataBind();
                        GvuFillMPSCSCTCData.DataSource = null;
                        GvuFillMPSCSCTCData.DataBind();
                        divMPSCSCTCData.Visible = false;
                        trOtherDepot.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                    }
                }
                else
                {
                    ddlGodown.SelectedIndex = 0;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Issue to First...')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('error in ddlGodown_SelectedIndexChanged!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void ddlDeliveredAgnt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeliveredAgnt.SelectedValue.ToString() == "1" || ddlDeliveredAgnt.SelectedValue.ToString() == "7")
        {
            trdo.Visible = false;
            ddlGodown.Enabled = true;
            txtdateofissue.Visible = false;
            lbldate.Visible = false;
            lblgatepasstype.Visible = false;
            rbdate.Visible = false;
            rbdo.Visible = false;
        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
        {
            trdo.Visible = true;
            Rodetails.Visible = false;
            ddlGodown.Enabled = true;
            lblDONo.Visible = false;
            ddlDONumber.Visible = false;
            txtdateofissue.Visible = true;
            lbldate.Visible = true;
            ddlcomm.Visible = true;
            lblComm.Visible = true;
            trcomm.Visible = true;
            lblgatepasstype.Visible = true;
            rbdate.Visible = true;
            rbdo.Visible = true;
            // ddlcomm.SelectedValue = "22";
            //  fillDO();

        }
        else
        {
            ddlGodown.Enabled = true;
            Rodetails.Visible = true;
            txtdateofissue.Visible = false;
            lbldate.Visible = false;
            lblgatepasstype.Visible = false;
            rbdate.Visible = false;
            rbdo.Visible = false;
        }
    }
    protected void ddlcomm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
        {
            lblcom.Text = ddlcomm.Text;
            //GvDOFPSDsd
            fillDoorStep();

        }
        else
        {
            lblcom.Text = ddlcomm.Text;
            WHRDetails();
        }
    }

    protected void fillDoorStep()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {

                string depotid = Session["Depot_DepotID"].ToString();
                string distidsub = Session["Depot_DistID"].ToString().Substring(2, 2);
                string distid = Session["Depot_DistID"].ToString();
                string deliagent = ddlDeliveredAgnt.SelectedItem.Text;
                string godownid = ddlGodown.SelectedValue.ToString();

                if (deliagent == "Door Step Delivery")
                {

                    string datestring = txtdateofissue.Text;
                    string[] tempsplit = datestring.Split('/');
                    string joinstring = "/";
                    string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];

                    GvDOFPSDsd.DataSource = null;
                    GvDOFPSDsd.DataBind();
                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";
                    //old srv  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join [MPSCSCSVR].MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and Godown_ID='"+ddlGodown.SelectedValue.ToString()+"' and convert(varchar(20),IDO.issue_date,101) ='" + getDate_MDY(txtdateofissue.Text) + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //loc    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + getDate_MDY(txtdateofissue.Text) + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   [MPSCSCSVR].MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["Depot_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details) and iDo.DOType in ('DS','TO')";
                    cmd = new SqlCommand(qry, con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    decimal tqty = 0;
                    int tbags = 0;
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //  Rodetails.Visible = true;
                        GvDOFPSDsd.DataSource = ds;
                        GvDOFPSDsd.DataBind();
                        GvDOFPSDsd.HeaderRow.Cells[0].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[17].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[13].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[14].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[15].Visible = false;
                        GvDOFPSDsd.HeaderRow.Cells[16].Visible = false;
                        for (int i = 0; i < GvDOFPSDsd.Rows.Count; i++)
                        {
                            GvDOFPSDsd.Rows[i].Cells[0].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[17].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[13].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[14].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[15].Visible = false;
                            GvDOFPSDsd.Rows[i].Cells[16].Visible = false;

                            CheckBox chkcheckbox = ((CheckBox)(GvDOFPSDsd.Rows[i].FindControl("ckboxDOlistdsd")));
                            chkcheckbox.Checked = true;
                            chkcheckbox.Enabled = false;
                            tqty = tqty + decimal.Parse(GvDOFPSDsd.Rows[i].Cells[6].Text);
                            tbags = tbags + int.Parse(GvDOFPSDsd.Rows[i].Cells[8].Text);
                        }
                        lblIssusedQty.Text = tqty.ToString();
                        lblIssusedBags.Text = tbags.ToString();
                        ddlRecDistrict.Items.Clear();
                        ddlRecDepot.Items.Clear();
                        txtTransId.Text = "";
                        txtTransId.Text = ds.Tables[0].Rows[0]["Trans_Id"].ToString();
                        //txttruckno.Text = ds.Tables[0].Rows[0]["Gate_pass"].ToString();
                        //lblcom.Text = ds.Tables[0].Rows[0]["commodity"].ToString();
                        //if (lblcom.Text != "")
                        //{
                        //    WHRDetails();
                        //}
                        WHRDetails();
                    }
                    else
                    {
                        //WHRDetails();

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('error in fillDO()!');", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void txtdateofissue_TextChanged(object sender, EventArgs e)
    {
        fillDoorStep();
    }
    protected void rbdate_CheckedChanged(object sender, EventArgs e)
    {
        lblComm.Visible = true;
        ddlcomm.Visible = true;
        lbldate.Visible = true;
        txtdateofissue.Visible = true;
        ddlDONumber.Visible = false;
        lblDONo.Visible = false;
        ddlGodown.SelectedIndex = 0;
        ddlcomm.SelectedIndex = 0;
        GvDOFPSDsd.DataSource = null;
        GvDOFPSDsd.DataBind();
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
    }
    protected void rbdo_CheckedChanged(object sender, EventArgs e)
    {
        lblComm.Visible = false;
        ddlcomm.Visible = false;
        lbldate.Visible = false;
        txtdateofissue.Visible = false;
        ddlGodown.SelectedIndex = 0;
        ddlcomm.SelectedIndex = 0;
        GvDOFPSDsd.DataSource = null;
        GvDOFPSDsd.DataBind();
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
    }
}
