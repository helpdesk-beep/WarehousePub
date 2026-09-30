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
using System.Resources;

public partial class WarehouseLevel_WLC_FRM_05_Delivery_GatePass_Details_PvtW : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    string GateP_No = string.Empty;
    String GP_SL_No = "";
    decimal issuelosswt = 0;
    decimal issuegainwt = 0;
    int calflag = 0;
    SqlTransaction sqltran;
    string godownid = "0";
    string latitude = "";
    string longitude = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TextBox2_CalendarExtender.EndDate = DateTime.Now;   //to dissable future  Date
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null) && (Session["GodownID_New"]!=null))
        {
            if (!IsPostBack)
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
                txtissuedbags.Enabled = false;
                txtissuedwt.Enabled = false;
                fillCropYear();
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
            if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
            {
                qry = "SELECT Transporter_Id, Transpoter_Name FROM tbl_metadata_transport where  DepotID  = '" + Session["G_DepotID"].ToString() + "' order by Transpoter_Name";
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
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
                else if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    //ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                    //trcomm.Visible = true;
                }
                else if (ddlDepositor.SelectedItem.Text == "NAFED")
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    //ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                    //trcomm.Visible = true;
                }
                else
                {
                    string notin = "OTHER DEPOT','LEAD SOCIETY','FPS";
                    bindIssuedTo(notin);
                    trOtherDepot.Visible = false;
                    FillRecDist();
                    ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
                    FillRecDepot(ddlRecDistrict.SelectedValue);
                    ddlRecDepot.SelectedValue = Session["G_DepotID"].ToString();
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                ddlDepositor.Items.Clear();
                string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
                string depotid = Session["G_DepotID"].ToString();
                string BranchID = Session["G_BranchId"].ToString();

                if (depositer == "Institution")
                {
                    //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed') or Depositor_ID='10535' union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";

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
                    if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                    {
                        ddlDeliveredAgnt.SelectedIndex = 4;
                        trcomm.Visible = true;
                        lblgatepasstype.Visible = false;
                        txtdateofissue.Visible = false;
                        rbdate.Visible = false;
                        rbdo.Visible = false;
                    }
                    else
                    {
                        trcomm.Visible = false;
                    }
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
            //if (Session["G_BranchId"].ToString() == "2330007")
            //{
            //    qry = "SELECT Source_ID,Source_Name FROM [tbl_MetaData_Issued_To]";
            //}
            //else
            //{
            //    qry = "SELECT Source_ID,Source_Name FROM [tbl_MetaData_Issued_To] where Source_ID not in ('11')";
            //}
            qry = "SELECT Source_ID,Source_Name FROM [tbl_MetaData_Issued_To] where Source_ID not in ('10','11')";
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                // qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join [MPSCSC].[dbo].tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["G_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //27-12-14--ori--- qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["G_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
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
    //03_10_15.............................other depot on date
    protected void fillGridMPSCSCTCDatadate()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                string dateoftc = getDate_MDY(txtdateofissue.Text);
                qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and tc.Challan_Date='" + dateoftc.ToString() + "' and tc.Commodity='" + ddlcomm.SelectedValue.ToString() + "' and  challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Sendto_District as SendToDist,tc.Sendto_IC as SendToDepot,tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_Truck_challan] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where  DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                decimal iqty = 0;
                decimal ibags = 0;
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
                        CheckBox checkbox = ((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist"));
                        checkbox.Checked = true;
                        checkbox.Enabled = false;
                        lblcom.Text = GvuFillMPSCSCTCData.Rows[i].Cells[11].Text;


                        iqty += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[7].Text);
                        lblIssusedQty.Text = iqty.ToString();


                        ibags += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[6].Text);
                        lblIssusedBags.Text = ibags.ToString();
                    }

                    //  lblcom.Text = ds.Tables[0].Rows[0]["Commodity"].ToString();
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
                WHRDetails();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                //qry = "SELECT com.Commodity_Name,tc.Commodity,isnull(tc.TO_Number,'--') as 'TO_Number',tc.Truck_no,tc.Challan_No,convert(varchar(10),tc.Challan_Date,103) as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join [MPSCSC].[dbo].tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select Trans_id from tbl_Storage_Final_Stock_Delivery_GatePass DGP join tbl_Storage_GatePass_Enrty as gp on DGP.GatePass_No=gp.Gatepass_no where Trans_id is not null and DGP.District_id='" + Session["Depot_DistID"].ToString() + "' and DGP.DepotId='" + Session["G_DepotID"].ToString() + "' and DGP.DeliverdAgent='" + ddlDeliveredAgnt.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //   qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
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

    protected void fillGridMPSCSCTCRailDataDate()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
                string dateoftc = getDate_MDY(txtdateofissue.Text);
                //ori  qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and tc.Challan_Date='" + dateoftc.ToString()+ "' and tc.Commodity='" + ddlcomm.SelectedValue.ToString() + "' and  challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                //   qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP join tbl_Storage_GatePass_Enrty as gp on DGP.Gatepass=gp.Gatepass_no where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "' and gp.[status]!='Cancel') order by Challan_Date desc";
                qry = "SELECT com.Commodity_Name,tc.Commodity,tc.Truck_no,isnull(tc.RailHead,'--') as 'TO_Number',tc.Challan_No,tc.Challan_Date as 'Challan_Date',tc.Transporter,tc.Bags,tc.Qty_send,tc.Sendto_District as SendToDist,tc.Rack_NO as SendToDepot,gd.Godown_Name,[Month] as 'Allotment_Month',[Year] as 'Allotment_Year' FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] tc left outer join tbl_MetaData_GODOWN gd on gd.Godown_Id=tc.Dispatch_Godown join tbl_MetaData_STORAGE_COMMODITY as com on tc.Commodity = com.Commodity_Id where Dist_ID='" + Dist_id + "' and [Depot_Id]='" + Session["G_DepotID"].ToString() + "' and Dispatch_Godown = '" + ddlGodown.SelectedValue.ToString() + "'  and tc.Challan_Date='" + dateoftc.ToString() + "' and tc.Commodity='" + ddlcomm.SelectedValue.ToString() + "' and  challan_No not in (select ChallanNum from OtherDepoChallanDetails DGP  where DGP.SendingDist='" + Session["Depot_DistID"].ToString() + "' and DGP.SendingDepot='" + Session["G_DepotID"].ToString() + "' and DGP.Godown='" + ddlGodown.SelectedValue.ToString() + "') order by Challan_Date desc";
                cmd = new SqlCommand(qry, con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                decimal iqty = 0;
                decimal ibags = 0;
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

                        CheckBox checkbox = ((CheckBox)GvuFillMPSCSCTCData.Rows[i].FindControl("ckboxtrucklist"));
                        checkbox.Checked = true;
                        checkbox.Enabled = false;
                        lblcom.Text = GvuFillMPSCSCTCData.Rows[i].Cells[11].Text;


                        iqty += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[7].Text);
                        lblIssusedQty.Text = iqty.ToString();


                        ibags += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[6].Text);
                        lblIssusedBags.Text = ibags.ToString();
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
                WHRDetails();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }



    protected void fillDO()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                ddlDONumber.Items.Clear();
                string depotid = Session["G_DepotID"].ToString();
                string distidsub = Session["Depot_DistID"].ToString().Substring(2, 2);
                string distid = Session["Depot_DistID"].ToString();
                string deliagent = ddlDeliveredAgnt.SelectedItem.Text;
                string godownid = ddlGodown.SelectedValue.ToString();
                if (deliagent == "Lead Society")
                {
                    //qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "'";
                    // qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    // qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' ";
                    //  qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) ";
                    //  qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) ";
                    //13_01_16  qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where  iDo.IssueCentre_code='" + depotid + "'  and Issue_Type in ('L','LF') and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                    qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where  iDo.IssueCentre_code='" + depotid + "'  and Issue_Type in ('L','LF') and iDo.Godown = '" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID='" + Session["G_BranchId"].ToString() + "') order by iDo.Delivery_order_no";
                }
                else if (deliagent == "FPS")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='F' order by iDo.Delivery_order_no";
                }
                else if (deliagent == "Depositor")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='O' order by iDo.Delivery_order_no";
                }
                else if (deliagent == "MPSCSC")
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='O' order by iDo.Delivery_order_no";
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
                        //from issuagainsdo tbl qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.DOType in ('DS','TO') and iDo.Godown='" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID ='" + Session["G_BranchId"].ToString() + "') order by iDo.Delivery_order_no";
                        //qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.issue_against_Doorstep_do iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.Godown='" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID ='" + Session["G_BranchId"].ToString() + "') order by iDo.Delivery_order_no";
                        qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.issue_against_Doorstep_do iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.Godown='" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID ='" + Session["G_BranchId"].ToString() + "') union select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.issue_against_Doorstep_TO_otherscheme iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.Godown='" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID ='" + Session["G_BranchId"].ToString() + "') order by iDo.Delivery_order_no";


                        //13_01_16  qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.DOType in ('DS','TO') and iDo.Godown='" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') order by iDo.Delivery_order_no";
                        //loc    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and iDo.DOType in ('DS','TO') and iDo.Godown='" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.[issue_against_do] where Delivery_order_no=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and iDo.issueCentre_code='" + depotid + "') order by iDo.Delivery_order_no";
                    }
                }
                else if (deliagent == "Other Scheme")
                {
                    if (rbdate.Checked)
                    {
                        ddlDONumber.Visible = false;
                        return;
                    }
                    else if (rbdo.Checked)
                    {
                        qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.issue_against_Doorstep_TO_otherscheme iDo where iDo.IssueCentre_code='" + depotid + "'  and iDo.Godown='" + godownid + "' and ido.trans_id not in (select trans_id from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no and tbl_RO_Details.BranchID ='" + Session["G_BranchId"].ToString() + "') order by iDo.Delivery_order_no";
                    }
                }
                else if (deliagent == "MPSCSC(Miller)")
                {
                    //26_02_16    qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where  iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                    //qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                    //server
                    //qry = "Select distinct IADO.delivery_order_no As Delivery_order_no From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO where IADO.district_code='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and IADO.issueCentre_code='" + depotid + "' and IADO.Godown='" + godownid + "' and IADO.trans_id not in (select trans_id from tbl_RO_Details where BranchID='" + Session["G_BranchId"].ToString() + "' and Commodity_Id in ('13','14','24'))";
                    qry = "Select distinct IADO.delivery_order_no As Delivery_order_no From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO where IADO.district_code='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and IADO.issueCentre_code='" + depotid + "' and IADO.Godown='" + godownid + "' and IADO.CropYear='" + GPddl.SelectedItem.Text + "' and IADO.trans_id not in (select trans_id from tbl_RO_Details where BranchID='" + Session["G_BranchId"].ToString() + "' and Commodity_Id in ('13','14','24'))";

                    //demo
                    //qry = "Select distinct IADO.delivery_order_no As Delivery_order_no From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO where IADO.district_code='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and IADO.issueCentre_code='" + depotid + "' and IADO.Godown='" + godownid + "' and IADO.trans_id not in (select trans_id from tbl_RO_Details where BranchID='" + Session["G_BranchId"].ToString() + "' and Commodity_Id in ('13','14','24'))";

                }
                else if (deliagent == "MPSCSC(OpenSale)")
                {
                    qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where  iDo.IssueCentre_code='" + depotid + "'  and Issue_Type !='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                    //qry = "select distinct iDo.Delivery_order_no FROM MPSCSC.dbo.issue_against_OpenSale_do iDo join MPSCSC.dbo.OpenSale_DO as DO on iDo.Delivery_order_no=DO.Delivery_order_no where iDo.District_code='" + distidsub + "' and iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='1' and iDo.Godown = '" + godownid + "' and (select count(trans_id) from MPSCSC.dbo.issue_against_OpenSale_do where Delivery_order_no=iDo.delivery_order_no) != (select count(trans_id) from tbl_RO_Details where [Release_Order_No]=iDo.delivery_order_no) order by iDo.Delivery_order_no";
                }

                else
                {
                    qry = "select distinct iDo.Delivery_order_no  FROM MPSCSC.dbo.[issue_against_do] iDo join MPSCSC.dbo.delivery_order_mpscsc as DO on iDo.Delivery_order_no=DO.Delivery_order_no where  iDo.IssueCentre_code='" + depotid + "'  and Issue_Type='L' order by iDo.Delivery_order_no";
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
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert(" + ex.Message + ");", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGridMPSCSCFPSData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                // for miller 29-01-15.....
                if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                {
                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";

                    //qry = "Select IADO.qty_issue as 'DO_Qty',IADO.Godown,gd.Godown_Name,convert(varchar(10),IADO.issue_date,103) as 'issue_date',isnull(mm.Mill_Name,'NA') as 'FPS_Name',IADO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IADO.qty_issue as 'Issue_Qty',IADO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IADO.Gate_pass,IADO.Trans_Id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Id From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO left join tbl_MetaData_GODOWN gd on (gd.Godown_Id=IADO.Godown) JOIN MPSCSC.dbo.Miller_Registration as mm on IADO.Partyname=mm.Registration_ID JOIN tbl_MetaData_STORAGE_COMMODITY ON (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id='13') where IADO.delivery_order_no='" + ddlDONumber.SelectedValue.ToString() + "' and IADO.District_code='" + distcode + "' and IADO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IADO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and IADO.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' )";
                    //qry = "Select IADO.qty_issue as 'DO_Qty',IADO.Godown,gd.Godown_Name,convert(varchar(10),IADO.issue_date,103) as 'issue_date',isnull(mm.Mill_Name,'NA') as 'FPS_Name',IADO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IADO.qty_issue as 'Issue_Qty',IADO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IADO.Gate_pass,IADO.Trans_Id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Id From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO left join tbl_MetaData_GODOWN gd on (gd.Godown_Id=IADO.Godown) JOIN MPSCSC.dbo.Miller_Registration_2017 as mm on IADO.Partyname=mm.Registration_ID JOIN tbl_MetaData_STORAGE_COMMODITY ON (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id='13') where IADO.delivery_order_no='" + ddlDONumber.SelectedValue.ToString() + "' and IADO.District_code='" + distcode + "' and IADO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IADO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and IADO.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' )";
                    if (GPddl.SelectedItem.Text == "2016-2017" || GPddl.SelectedItem.Text == "2017-2018")
                    {
                        qry = "Select IADO.qty_issue as 'DO_Qty',IADO.Godown,gd.Godown_Name,convert(varchar(10),IADO.issue_date,103) as 'issue_date',isnull(mm.Mill_Name,'NA') as 'FPS_Name',IADO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IADO.qty_issue as 'Issue_Qty',IADO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IADO.Gate_pass,IADO.Trans_Id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Id From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO left join tbl_MetaData_GODOWN gd on (gd.Godown_Id=IADO.Godown) JOIN MPSCSC.dbo.Miller_Registration_2017 as mm on IADO.Partyname=mm.Registration_ID JOIN tbl_MetaData_STORAGE_COMMODITY ON (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id='13') where IADO.delivery_order_no='" + ddlDONumber.SelectedValue.ToString() + "' and IADO.District_code='" + distcode + "' and IADO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IADO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and IADO.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' )";
                    }
                    else
                    {
                        qry = "Select IADO.qty_issue as 'DO_Qty',IADO.Godown,gd.Godown_Name,convert(varchar(10),IADO.issue_date,103) as 'issue_date',isnull(mm.Mill_Name,'NA') as 'FPS_Name',IADO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IADO.qty_issue as 'Issue_Qty',IADO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IADO.Gate_pass,IADO.Trans_Id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Id From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO left join tbl_MetaData_GODOWN gd on (gd.Godown_Id=IADO.Godown) JOIN MPSCSC.dbo.Miller_Registration as mm on IADO.Partyname=mm.Registration_ID JOIN tbl_MetaData_STORAGE_COMMODITY ON (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id='13') where IADO.delivery_order_no='" + ddlDONumber.SelectedValue.ToString() + "' and IADO.District_code='" + distcode + "' and IADO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IADO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and IADO.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' )";
                    }
                    //demo
                    //qry = "Select IADO.qty_issue as 'DO_Qty',IADO.Godown,gd.Godown_Name,convert(varchar(10),IADO.issue_date,103) as 'issue_date',isnull(mm.Mill_Name,'NA') as 'FPS_Name',IADO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IADO.qty_issue as 'Issue_Qty',IADO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IADO.Gate_pass,IADO.Trans_Id,tbl_MetaData_STORAGE_COMMODITY.Commodity_Id From MPSCSC.dbo.PaddyMilling_IssueAgainst_DO As IADO left join tbl_MetaData_GODOWN gd on (gd.Godown_Id=IADO.Godown) JOIN MPSCSC.dbo.Miller_Registration as mm on IADO.Partyname=mm.Registration_ID JOIN tbl_MetaData_STORAGE_COMMODITY ON (tbl_MetaData_STORAGE_COMMODITY.Commodity_Id='13') where IADO.delivery_order_no='" + ddlDONumber.SelectedValue.ToString() + "' and IADO.District_code='" + distcode + "' and IADO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IADO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and IADO.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' )";

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
                else if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(OpenSale)")
                {
                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";

                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(osp.PartyName,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_OpenSale_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSC.dbo.[OpenSale_DO] DOM on  IDO.district_code=DOM.district_code and  IDO.delivery_order_no=DOM.delivery_order_no  join tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left join [MPSCSC].[dbo].[OpenSaleParty] as osp on IDO.district_code=osp.District and IDO.Partyname=osp.PartyId  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IDO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "' and tbl_RO_Details.BranchID='" + Session["G_BranchId"].ToString() + "' )";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(mm.Miller_Name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code','NA' as allotment_month,'NA' as allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_OpenSale_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSC.dbo.[OpenSale_DO] DOM on  IDO.district_code=DOM.district_code and  IDO.delivery_order_no=DOM.delivery_order_no JOIN mpscsc.dbo.Miller_Master as mm on DOM.Partyname=mm.Miller_ID join tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id   where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and IDO.Godown='" + ddlGodown.SelectedValue.ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
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
                    //old srv  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //loc  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)"; 
                    //from tblissuagantdo  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "') and iDo.DOType in ('DS','TO')";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "') union select IDO.qty_issue as 'DO_Qty','' as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_TO_otherscheme] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
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
                ///for other scheme
                else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                {

                    //for door step delivery.....

                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";
                    //old srv  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //loc  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)"; 
                    //from tblissuagantdo  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "') and iDo.DOType in ('DS','TO')";
                    qry = "select IDO.qty_issue as 'DO_Qty','' as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_TO_otherscheme] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
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
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(ido.Issue_to_LS_name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_do] IDO left  join tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join  MPSCSC.dbo.delivery_order_mpscsc DOM on  IDO.district_code=DOM.district_code and IDO.issueCentre_code=DOM.issueCentre_code and IDO.delivery_order_no=DOM.delivery_order_no JOIN tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Godown,gd.Godown_Name,convert(varchar(10),IDO.issue_date,103) as 'issue_date',isnull(ido.Issue_to_LS_name,'NA') as 'FPS_Name',IDO.issue_to as 'FPS_Code',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty' ,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.Gate_pass,IDO.Trans_Id,DOM.commodity_id from MPSCSC.dbo.[issue_against_do] IDO left  join MPSCSC.dbo.tbl_MetaData_GODOWN gd on gd.Godown_Id=IDO.Godown left join MPSCSC.dbo.delivery_order_mpscsc DOM on  IDO.district_code=DOM.district_code and IDO.issueCentre_code=DOM.issueCentre_code and IDO.delivery_order_no=DOM.delivery_order_no JOIN tbl_MetaData_STORAGE_COMMODITY ON DOM.commodity_id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where IDO.delivery_order_no= '" + ddlDONumber.SelectedValue.ToString() + "'  and IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
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
                        decimal ibags = 0;
                        ibags += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[6].Text);
                        lblIssusedBags.Text = ibags.ToString();

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

                        decimal ibags = 0;
                        ibags += decimal.Parse(GvuFillMPSCSCTCData.Rows[i].Cells[6].Text);
                        lblIssusedBags.Text = ibags.ToString();
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_Truck_challan] as TC join [MPSCSC].[dbo].tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["G_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            // qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_Truck_challan] as TC join [MPSCSC].[dbo].tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["G_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Rack_NO as Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] as TC join tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["G_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
            //qry = "SELECT Dispatch_Godown,Godown_Name,Sendto_District,Rack_NO as Sendto_IC,Commodity,Bags,Qty_send,Truck_no,Challan_No,convert(varchar(10),Challan_Date,103),Transporter FROM [MPSCSC].[dbo].[SCSC_RailHead_TC] as TC left join tbl_MetaData_GODOWN  as Gd on TC.Dispatch_Godown=Gd.Godown_ID where Dist_ID='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' and Depot_Id='" + Session["G_DepotID"].ToString() + "' and Challan_No='" + ViewState["TCNo"].ToString() + "'";
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
            qry = "select District_id,District_Name FROM [MPSCSC].dbo.tbl_MetaData_DISTRICT order by District_Name";
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
            qry = "select DepotID,DepotName FROM [MPSCSC].dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlRecDistrict.SelectedValue.ToString() + "' order by DepotName";
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
            Server.ScriptTimeout = 11500;
            for (s = 0; s < gdstackdetail.Rows.Count; s++)
            {
                if (((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked == true)
                {
                    if (((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled == true)
                    {
                        Stack_id = gdstackdetail.Rows[s].Cells[12].Text.ToString();
                        WHR_ID = gdstackdetail.Rows[s].Cells[3].Text.ToString();
                        int Bags = int.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Text.ToString());
                        //if (Bags == int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
                        // {
                        decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString());
                        decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[6].Text.ToString());
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
                        if (Bags < int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
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
                                Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[6].Text.ToString());
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
                        else if (Bags == int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
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
                        if (ddlDepositorType.SelectedItem.Text == "Institution")
                        {
                            if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                            {
                                txtissuedbags.Text = totalbags.ToString();
                                txtissuedwt.Text = totalwt.ToString();
                                lblIssusedQty.Text = totalwt.ToString();
                                lblIssusedBags.Text = totalbags.ToString();
                            }
                            else if (ddlDeliveredAgnt.SelectedItem.Text == "Same Godown Transfer(Handover)")
                            {
                                txtissuedbags.Text = totalbags.ToString();
                                txtissuedwt.Text = totalwt.ToString();
                                lblIssusedQty.Text = totalwt.ToString();
                                lblIssusedBags.Text = totalbags.ToString();
                            }
                            if (ddlDepositorType.SelectedItem.Text == "Institution" && ddlDepositor.SelectedItem.Text == "NAFED")
                            {
                                //For Nafed Gatepass
                                txtissuedbags.Text = totalbags.ToString();
                                txtissuedwt.Text = totalwt.ToString();
                                lblIssusedQty.Text = totalwt.ToString();
                                lblIssusedBags.Text = totalbags.ToString();
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
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Text = "0";
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text = "0";
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Text = "0";
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Text = "0";
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
                else if (ddlDeliveredAgnt.SelectedItem.Text == "Same Godown Transfer(Handover)")
                {
                    calflag = 0;
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
            Response.Redirect("WLC_FRM_05_Delivery_GatePass_Details_PvtW.aspx");
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {
                Server.ScriptTimeout = 100000;
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

                            qry = "select isnull(Max(GP_SL_No),0) from tbl_Storage_GatePass_Enrty where  BranchID='" + Session["G_BranchId"].ToString() + "' ";
                            cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                            string str4 = cmd.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str4) != 0)
                            {
                                GP_SL_No = Convert.ToString(Convert.ToInt64(str4) + 1);
                                string Depotid = Session["G_DepotID"].ToString();
                                string BranchId = Session["G_BranchId"].ToString();
                                GateP_No = BranchId + System.DateTime.Now.Date.ToString("yy") + System.DateTime.Now.Date.ToString("MM") + GP_SL_No;
                            }
                            else
                            {
                                GP_SL_No = "1";
                                string Depotid = Session["G_DepotID"].ToString();
                                string BranchId = Session["G_BranchId"].ToString();
                                GateP_No = BranchId + System.DateTime.Now.Date.ToString("yy") + System.DateTime.Now.Date.ToString("MM") + GP_SL_No;
                            }
                            //////////////////////////////////////////////////////////////////////////////
                            cmd = new SqlCommand("MPWLC_sp_DOGatePass_GatePass_insert", con, sqltran);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@State_ID", "23");
                            cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                            cmd.Parameters.AddWithValue("@Depot_Id", Session["G_DepotID"].ToString());
                            if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                            {
                                //for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                                //{
                                //    if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistDSD")).Checked == true)
                                //    {
                                //        cmd.Parameters.AddWithValue("@Godown_ID", GvDOFPSDsd.Rows[k].Cells[14].Text);
                                //    }
                                //}
                                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue.ToString());
                            }
                            else
                            {

                                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue.ToString());

                            }
                            cmd.Parameters.AddWithValue("@Stack_ID", "stacked");
                            cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.Text.ToString());
                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                            cmd.Parameters.AddWithValue("@Scheme_ID", DBNull.Value);
                            // string gpdt = DateTime.ParseExact(TextBox2.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                            cmd.Parameters.AddWithValue("@issue_date", getDate_MDY(TextBox2.Text));
                            cmd.Parameters.AddWithValue("@Vehicle_Type", ddlVehicleType.SelectedItem.Text.ToString());
                            cmd.Parameters.AddWithValue("@BranchId", Session["G_BranchId"].ToString());
                            if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                            {
                                cmd.Parameters.AddWithValue("@Vehicle_No", txttruckno.Text.ToString());
                            }
                            else
                            {
                                if (txttruckno.Text.ToString().Trim() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Vehicle_No", "");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Vehicle_No", txttruckno.Text.ToString());
                                }
                            }
                            if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                            {
                                cmd.Parameters.AddWithValue("@Driver_Name", UxTrans.SelectedItem.Text);
                            }
                            else
                            {
                                if (UxTrans.SelectedItem.Text == "")
                                {
                                    cmd.Parameters.AddWithValue("@Driver_Name", DBNull.Value);
                                }
                                else
                                {

                                    cmd.Parameters.AddWithValue("@Driver_Name", UxTrans.SelectedItem.Text);
                                }
                            }
                            // cmd.Parameters.AddWithValue("@Driver_Name", DBNull.Value);
                            cmd.Parameters.AddWithValue("@NO_of_Bage", int.Parse(txtissuedbags.Text.Trim().ToString()));
                            cmd.Parameters.AddWithValue("@Weight", decimal.Parse(txtissuedwt.Text));
                            cmd.Parameters.AddWithValue("@Issue_Source", "RO");
                            cmd.Parameters.AddWithValue("@Issue_Source_ID", "0");
                            cmd.Parameters.AddWithValue("@CreatedBy", ClientIP.ToString());
                            cmd.Parameters.AddWithValue("@Status", "Active");
                            cmd.Parameters.AddWithValue("@Printed", "NO");
                            cmd.Parameters.AddWithValue("@License_No", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Valid_Upto", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Arrival_Dep_Time", ddl1.SelectedValue + ":" + ddl2.SelectedValue + ":" + ddl3.SelectedValue);
                            cmd.Parameters.AddWithValue("@Remarks", txtTruckDetails.Text.ToString().Trim());
                            cmd.Parameters.AddWithValue("@Miller_Id", UxTrans.SelectedValue);
                            cmd.Parameters.AddWithValue("@GP_SL_No", GP_SL_No);
                            cmd.Parameters.AddWithValue("@CropYear", ddlcropyear.SelectedValue.ToString());
                            int index3 = cmd.ExecuteNonQuery();
                            txtGpid.Text = GateP_No;
                            cmd.Dispose();

                            ////////////////////////////////////////////////////////////////////////////////////
                            if (index3 > 0)
                            {
                                if (ddlDeliveredAgnt.SelectedItem.Text != "Door Step Delivery" && ddlDeliveredAgnt.SelectedItem.Text != "Other Scheme")
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
                                            cmd.Parameters.AddWithValue("@DepotId", Session["G_DepotID"].ToString());
                                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                                            cmd.Parameters.AddWithValue("@Release_Order_No", ddlDONumber.SelectedValue);
                                            cmd.Parameters.AddWithValue("@Release_Order_Date", getDate_MDY(GvDOFPS_on_Fetch.Rows[k].Cells[9].Text));
                                            cmd.Parameters.AddWithValue("@RO_Quantity", GvDOFPS_on_Fetch.Rows[k].Cells[7].Text);
                                            cmd.Parameters.AddWithValue("@FPSId", GvDOFPS_on_Fetch.Rows[k].Cells[0].Text);
                                            cmd.Parameters.AddWithValue("@FPS", GvDOFPS_on_Fetch.Rows[k].Cells[2].Text);
                                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                            cmd.Parameters.AddWithValue("@Truckno", GvDOFPS_on_Fetch.Rows[k].Cells[5].Text);
                                            cmd.Parameters.AddWithValue("@RO_Quantity_issued", GvDOFPS_on_Fetch.Rows[k].Cells[6].Text);
                                            cmd.Parameters.AddWithValue("@BranchID", Session["G_BranchId"].ToString());
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
                                            cmd.Parameters.AddWithValue("@DepotId", Session["G_DepotID"].ToString());
                                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                                            cmd.Parameters.AddWithValue("@Release_Order_No", GvDOFPSDsd.Rows[k].Cells[18].Text);
                                            cmd.Parameters.AddWithValue("@Release_Order_Date", getDate_MDY(GvDOFPSDsd.Rows[k].Cells[9].Text));
                                            cmd.Parameters.AddWithValue("@RO_Quantity", GvDOFPSDsd.Rows[k].Cells[7].Text);
                                            cmd.Parameters.AddWithValue("@FPSId", GvDOFPSDsd.Rows[k].Cells[0].Text);
                                            cmd.Parameters.AddWithValue("@FPS", GvDOFPSDsd.Rows[k].Cells[2].Text);
                                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                            cmd.Parameters.AddWithValue("@BranchID", Session["G_BranchId"].ToString());
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
                                            try
                                            {
                                                /////Call WebService
                                                //Insert_WHR_CSMS(string DCNO, string DistrictId, string gatepass, String Commodity)
                                                //if (con.State == ConnectionState.Open)
                                                //{
                                                //    con.Close();
                                                //    Empty();
                                                //}
                                                //UpdateCSMSDOforWLCGatepass.Update_gatepass_CSMS_Issue DOupdateByGP = new UpdateCSMSDOforWLCGatepass.Update_gatepass_CSMS_Issue();
                                                //DOupdateByGP.Insert_WHR_CSMS(GvDOFPSDsd.Rows[k].Cells[18].Text, Session["Depot_DistID"].ToString(), GateP_No, lblcom.Text.ToString());

                                                /////End WebService
                                            }
                                            catch (Exception ex)
                                            {
                                                //lbl_message.Text = ex.Message;
                                                //Response.Write("Some  error has occured! In Webservice");
                                            }
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
                                        cmd.Parameters.AddWithValue("@SendingDepot", Session["G_DepotID"].ToString());
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
                                        cmd.Parameters.AddWithValue("@BranchId", Session["G_BranchId"].ToString());
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

                                qry = "select isnull(Max(StockDeliveryOrderGatePass_Id),0) from tbl_Storage_Final_Stock_Delivery_GatePass where  BranchID='" + Session["G_BranchId"].ToString() + "' ";
                                cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                string str1 = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt64(str1) != 0)
                                {
                                    _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(str1) + 1);
                                    Session["stokdelvrygpnum"] = _StockDeliveryOrderGatePass_Id;
                                    if (_StockDeliveryOrderGatePass_Id != String.Empty || _StockDeliveryOrderGatePass_Id != "")
                                    {
                                    Found:
                                        qry = "select count(StockDeliveryOrderGatePass_Id) from tbl_Storage_Final_Stock_Delivery_GatePass where StockDeliveryOrderGatePass_Id='" + _StockDeliveryOrderGatePass_Id + "'";
                                        cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                        string maxcount = cmd.ExecuteScalar().ToString();
                                        if (Convert.ToInt16(maxcount) > 0)
                                        {
                                            _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(_StockDeliveryOrderGatePass_Id) + 1);
                                            Session["stokdelvrygpnum"] = _StockDeliveryOrderGatePass_Id;
                                            goto Found;
                                        }
                                    }
                                }
                                else
                                {
                                    string Depotid = Session["G_DepotID"].ToString();
                                    string BranchId = Session["G_BranchId"].ToString();
                                    _StockDeliveryOrderGatePass_Id = BranchId + "1";
                                    Session["stokdelvrygpnum"] = _StockDeliveryOrderGatePass_Id;
                                    if (_StockDeliveryOrderGatePass_Id != String.Empty || _StockDeliveryOrderGatePass_Id != "")
                                    {
                                    Found:
                                        qry = "select count(StockDeliveryOrderGatePass_Id) from tbl_Storage_Final_Stock_Delivery_GatePass where StockDeliveryOrderGatePass_Id='" + _StockDeliveryOrderGatePass_Id + "'";
                                        cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                        string maxcount = cmd.ExecuteScalar().ToString();
                                        if (Convert.ToInt16(maxcount) > 0)
                                        {
                                            _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(_StockDeliveryOrderGatePass_Id) + 1);
                                            Session["stokdelvrygpnum"] = _StockDeliveryOrderGatePass_Id;
                                            goto Found;
                                        }
                                    }
                                }
                                cmd = new SqlCommand("MPWLC_sp_tbl_Storage_Final_Stock_Delivery_GatePass_insert", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@StockDeliveryOrderGatePass_Id", _StockDeliveryOrderGatePass_Id);
                                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                cmd.Parameters.AddWithValue("@DepotId", Session["G_DepotID"].ToString());
                                cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                                cmd.Parameters.AddWithValue("@WHR_Id", _StockDeliveryOrderGatePass_Id);
                                cmd.Parameters.AddWithValue("@Qty_Issued_No_Bags_Sound", Convert.ToInt32(_issedBags));
                                cmd.Parameters.AddWithValue("@Qty_Issued_Weight", Convert.ToDecimal(_issedwt));
                                cmd.Parameters.AddWithValue("@Value_Stock_Delivered", DBNull.Value);
                                cmd.Parameters.AddWithValue("@BranchId", Session["G_BranchId"].ToString());
                                if (txtmoisture.Text.Trim().ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Moisture_Content", 0);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Moisture_Content", Convert.ToDecimal(txtmoisture.Text.Trim().ToString()));
                                }

                                if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
                                {
                                    cmd.Parameters.AddWithValue("@Purpose_Of_Issue", "ds");
                                }
                                else
                                {

                                    cmd.Parameters.AddWithValue("@Purpose_Of_Issue", DBNull.Value);
                                }

                                cmd.Parameters.AddWithValue("@Rental_Amt_Received", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Cash_Credit", DBNull.Value);
                                cmd.Parameters.AddWithValue("@DD_No", DBNull.Value);
                                cmd.Parameters.AddWithValue("@DD_Date", DBNull.Value);
                                cmd.Parameters.AddWithValue("@DD_BankId", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Sample_Serial_No", DBNull.Value);
                                cmd.Parameters.AddWithValue("@Vikas_Chand", DBNull.Value);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());

                                if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                                {
                                    //for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                                    //{
                                    //    if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistDSD")).Checked == true)
                                    //    {

                                    //        cmd.Parameters.AddWithValue("@TransporterId", GvDOFPSDsd.Rows[k].Cells[17].Text);
                                    //    }
                                    //}
                                    cmd.Parameters.AddWithValue("@TransporterId", UxTrans.SelectedValue);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TransporterId", UxTrans.SelectedValue);
                                }

                                if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                                {
                                    //for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                                    //{
                                    //    if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistDSD")).Checked == true)
                                    //    {
                                    //        cmd.Parameters.AddWithValue("@FPS", GvDOFPSDsd.Rows[k].Cells[2].Text);
                                    //    }
                                    //}
                                    cmd.Parameters.AddWithValue("@FPS", "FPS");
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FPS", DBNull.Value);
                                }

                                if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery" || ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
                                {
                                    if (rbdo.Checked)
                                    {
                                        for (int k = 0; k < GvDOFPSDsd.Rows.Count; k++)
                                        {
                                            if (((CheckBox)GvDOFPSDsd.Rows[k].FindControl("ckboxDOlistDSD")).Checked == true)
                                            {
                                                cmd.Parameters.AddWithValue("@FPSId", GvDOFPSDsd.Rows[k].Cells[0].Text);
                                            }
                                        }
                                    }
                                    else if (rbdate.Checked)
                                    {
                                        cmd.Parameters.AddWithValue("@FPSId", DBNull.Value);

                                    }
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FPSId", DBNull.Value);
                                }
                                cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                cmd.Parameters.AddWithValue("@DeliverdAgent", ddlDeliveredAgnt.SelectedValue);

                                if ((ddlDepositor.SelectedItem.Text.ToUpper() != "MPWLC") && ((ddlDepositor.SelectedItem.Text.ToUpper() != "MPSCSC")))
                                {
                                    cmd.Parameters.AddWithValue("@RecipientDistrict", Session["Depot_DistID"].ToString());
                                    cmd.Parameters.AddWithValue("@RecipientDepot", Session["G_DepotID"].ToString());
                                    cmd.Parameters.AddWithValue("@DelveredAddress", DBNull.Value);
                                }
                                else if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPWLC")
                                {
                                    cmd.Parameters.AddWithValue("@RecipientDistrict", ddlRecDistrict.SelectedValue);
                                    cmd.Parameters.AddWithValue("@RecipientDepot", ddlRecDepot.SelectedValue);
                                    cmd.Parameters.AddWithValue("@DelveredAddress", DBNull.Value);
                                }
                                else if ((ddlDeliveredAgnt.Items.Count > 0) && (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot") || (trOtherDepot.Visible == true))
                                {
                                    cmd.Parameters.AddWithValue("@RecipientDistrict", ddlRecDistrict.SelectedValue);
                                    cmd.Parameters.AddWithValue("@RecipientDepot", ddlRecDepot.SelectedValue);
                                    cmd.Parameters.AddWithValue("@DelveredAddress", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@RecipientDistrict", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@RecipientDepot", DBNull.Value);
                                    cmd.Parameters.AddWithValue("@DelveredAddress", DBNull.Value);
                                }
                                if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPSCSC")
                                {
                                    cmd.Parameters.AddWithValue("@trans_id", txtTransId.Text.Trim().ToString());
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@trans_id", DBNull.Value);
                                }
                                int y = cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                //for  3rd table ie:tbl_Delivery_Stacking_Details_GatePass(StockDeliveryOrderGatePass_Id,GatePass_No)

                                #region thirdtable

                                string EmployeeXML = CreateEmployeeXML();
                                //CreateEmployeeXML();
                                cmd = new SqlCommand("sp_tbl_Delivery_Stacking_Details_GatePass_insert_XML", con, sqltran);
                                cmd.CommandType = CommandType.StoredProcedure;
                                //string EmployeeXML = CreateEmployeeXML();
                                //Pass employee data in xml format to stored procedure
                                cmd.Parameters.AddWithValue("@EmployeeXml", EmployeeXML);

                                cmd.ExecuteNonQuery();
                                cmd.Dispose();

                                #endregion
                            }
                            sqltran.Commit();
                            GetLatiLongi();
                        #endregion

                            Empty();
                            Gridclear();

                            Session["RefreshButton"] = "Yes";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                            Session["dtRo"] = null;

                            //For gate pass link and pop up
                            trlnk.Visible = true;
                            string Roid = "../IssueCenterLevel/Storage/Gate_Pass.aspx?src=RO&vu=" + GateP_No;
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


    private string CreateEmployeeXML()
    {
        StringBuilder sb = new StringBuilder();
        //Loop through each row of gridview
        string stkid = "";
        string whrid = "";
        string Godid = "";
        decimal _chkwt;
        string WhrDate = "";
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        for (int ino = 0; ino < gdstackdetail.Rows.Count; ino++)
        {
            if (((CheckBox)gdstackdetail.Rows[ino].FindControl("ckstack")).Checked == true)
            {
                if (((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Enabled == false)
                {
                    Godid = gdstackdetail.Rows[ino].Cells[11].Text.ToString();
                    stkid = gdstackdetail.Rows[ino].Cells[12].Text.ToString();
                    whrid = gdstackdetail.Rows[ino].Cells[3].Text.ToString();
                    decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtweight")).Text.ToString()) + decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString()) - decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString());
                    decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[ino].Cells[6].Text.ToString());
                    int Bags = int.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Text.ToString());
                    decimal gainqty = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtgain")).Text.ToString());
                    decimal lossqty = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtloss")).Text.ToString());
                    if (Bags > int.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString()))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issued bags are more than No of bags available')", true);
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Issued bags are more than No of bags available";
                    }

                    else if (Bags <= int.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString()))
                    {
                        sb.Append(String.Format("<Employee Godown_ID='{0}' Stack_ID='{1}' No_Of_Bags='{2}' Bags_Weight='{3}' CreatedBy='{4}' Depositor_WHR_Id='{5}' StockDeliveryOrderGatePass_Id='{6}' Loss='{7}' Gain='{8}' GatePass_No='{9}'/>", Godid, stkid, Bags, Totissue_Wgt, Session["UserName"].ToString(), whrid, Session["stokdelvrygpnum"].ToString(), lossqty, gainqty, GateP_No));
                    }

                    /////Call WebService
                    WhrDate = gdstackdetail.Rows[ino].Cells[15].Text.ToString();
                    //Insert_WHR_CSMS(string DCNO, string DistrictId, string WHR_Number, String WHR_Date, String Commodity, String qty_issue, String bags, String Godown, String Stack_id, String mpwlc_gatepass)
                    //UpdateWHRforCSMSIssue.Update_WHR_CSMS_Issue InsertWHROverGP = new UpdateWHRforCSMSIssue.Update_WHR_CSMS_Issue();
                    //InsertWHROverGP.Insert_WHR_CSMS(GateP_No, Dist_id, whrid, getDate_MDY(WhrDate), ddlcomm.SelectedValue.ToString(), Totissue_Wgt.ToString(), Bags.ToString(), Godid, stkid, GateP_No);

                    /////End WebService
                }
            }
        }
        return String.Format("<ROOT>{0}</ROOT>", sb.ToString());
    }

    private void WHRDetails()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            if (ddlDeliveredAgnt.SelectedItem.Text != "Door Step Delivery" && ddlDeliveredAgnt.SelectedItem.Text != "Other Scheme")
            {

                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    //query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["G_BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id in ('35','22','6') group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
                }
                else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','7') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                }
                else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "25" || lblcom.Text.ToString() == "29" || lblcom.Text.ToString() == "96" || lblcom.Text.ToString() == "97")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('25','29','96','97')  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                {
                    if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name in ('DMO Markfed','MPSCSC') and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    }
                }
                else
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                    // query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["G_BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
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
                        gdstackdetail.HeaderRow.Cells[11].Visible = false;
                        gdstackdetail.HeaderRow.Cells[12].Visible = false;
                        gdstackdetail.HeaderRow.Cells[7].Visible = false;
                        for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                        {
                            gdstackdetail.Rows[j].Cells[11].Visible = false;
                            gdstackdetail.Rows[j].Cells[12].Visible = false;
                            gdstackdetail.Rows[j].Cells[7].Visible = false;
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
            else
            {

                //door step delivery .......

                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (rbdate.Checked)
                {
                    godownid = ddlGodown.SelectedValue.ToString();
                    if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                    }
                    //rice
                    else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                    }
                    else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    }
                    else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date ,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecBags-DelBags)>0 order by Depositor_whr_id";
                        //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                    }
                }
                else if (rbdo.Checked)
                {
                    if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                    }
                    //rice
                    else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecBags-DelBags)>0 order by Depositor_whr_id";
                    }
                    else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    }
                    else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecBags-DelBags)>0 order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date ,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecBags-DelBags)>0 order by Depositor_whr_id";
                        //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
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
                        gdstackdetail.HeaderRow.Cells[11].Visible = false;
                        gdstackdetail.HeaderRow.Cells[12].Visible = false;
                        gdstackdetail.HeaderRow.Cells[7].Visible = false;
                        for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                        {
                            gdstackdetail.Rows[j].Cells[11].Visible = false;
                            gdstackdetail.Rows[j].Cells[12].Visible = false;
                            gdstackdetail.Rows[j].Cells[7].Visible = false;
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
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    //For Steel Silo
    private void WHRDetailsForSilo()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            if (ddlDeliveredAgnt.SelectedItem.Text != "Door Step Delivery" && ddlDeliveredAgnt.SelectedItem.Text != "Other Scheme")
            {

                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    //query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["G_BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id in ('35','22','6') group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
                }
                else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','7') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                }
                else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "25" || lblcom.Text.ToString() == "29" || lblcom.Text.ToString() == "96" || lblcom.Text.ToString() == "97")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('25','29','96','97')  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                {
                    if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name in ('DMO Markfed','MPSCSC') and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    }
                }
                else
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                    // query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["G_BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
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
                        gdstackdetail.HeaderRow.Cells[11].Visible = false;
                        gdstackdetail.HeaderRow.Cells[12].Visible = false;
                        gdstackdetail.HeaderRow.Cells[7].Visible = false;
                        for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                        {
                            gdstackdetail.Rows[j].Cells[11].Visible = false;
                            gdstackdetail.Rows[j].Cells[12].Visible = false;
                            gdstackdetail.Rows[j].Cells[7].Visible = false;
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
            else
            {

                //door step delivery .......

                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (rbdate.Checked)
                {
                    godownid = ddlGodown.SelectedValue.ToString();
                    if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    }
                    //rice
                    else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    }
                    else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0 order by Depositor_whr_id";

                    }
                    else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0 order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date ,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 order by Depositor_whr_id";
                        //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
                    }
                }
                else if (rbdo.Checked)
                {
                    if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    }
                    //rice
                    else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','4') and (RecQty - DelQty)>0 order by Depositor_whr_id";
                    }
                    else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0 order by Depositor_whr_id";

                    }
                    else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0 order by Depositor_whr_id";

                    }
                    else
                    {
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date ,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["G_BranchId"].ToString() + "' and Godown_ID = '" + godownid + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 order by Depositor_whr_id";
                        //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["G_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
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
                        gdstackdetail.HeaderRow.Cells[11].Visible = false;
                        gdstackdetail.HeaderRow.Cells[12].Visible = false;
                        gdstackdetail.HeaderRow.Cells[7].Visible = false;
                        for (int j = 0; j < gdstackdetail.Rows.Count; j++)
                        {
                            gdstackdetail.Rows[j].Cells[11].Visible = false;
                            gdstackdetail.Rows[j].Cells[12].Visible = false;
                            gdstackdetail.Rows[j].Cells[7].Visible = false;
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
                        //WHRDetails();
                        string GodownHTypeId = Session["GodownTypeId"].ToString();
                        string GodownTypeId = GodownHTypeId.Trim().ToString();

                        if (GodownTypeId == "2" || GodownTypeId == "3")
                        {
                            WHRDetailsForSilo();
                        }
                        else
                        {
                            WHRDetails();
                        }

                    }
                }
                lblIssusedQty.Text = roqty.ToString();
                lblIssusedBags.Text = bgas.ToString();
            }
        }
        /////Other Scheme
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
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
        //qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y'  order by Godown_Name ";
        qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";
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
        qry = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
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
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
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
                                if (rbdate.Checked)
                                {
                                    fillGridMPSCSCTCDatadate();
                                    FillRecDist();
                                    FillRecDepot(ddlRecDistrict.SelectedValue);

                                }
                                else if (rbdo.Checked)
                                {
                                    fillGridMPSCSCTCData();
                                    FillRecDist();
                                    FillRecDepot(ddlRecDistrict.SelectedValue);
                                    ddlRecDepot.SelectedValue = Session["G_DepotID"].ToString();
                                }
                            }
                            else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot(By Rack)")
                            {
                                if (rbdate.Checked)
                                {
                                    fillGridMPSCSCTCRailDataDate();
                                    FillRecDist();
                                    FillRecDepot(ddlRecDistrict.SelectedValue);
                                }
                                else if (rbdo.Checked)
                                {
                                    fillGridMPSCSCTCRailData();
                                    FillRecDist();
                                    FillRecDepot(ddlRecDistrict.SelectedValue);
                                    ddlRecDepot.SelectedValue = Session["G_DepotID"].ToString();

                                }
                            }
                            //issue to Same Godown Transfer(Handover)
                            else if (ddlDeliveredAgnt.SelectedItem.Text == "Same Godown Transfer(Handover)")
                            {
                                GvDOFPS_on_Fetch.DataSource = null;
                                GvDOFPS_on_Fetch.DataBind();
                                GvuFillMPSCSCTCData.DataSource = null;
                                GvuFillMPSCSCTCData.DataBind();
                                divMPSCSCTCData.Visible = false;
                                trOtherDepot.Visible = false;
                                Stockdetails.Visible = false;
                                Issuedetails.Visible = false;
                                trcomm.Visible = true;
                                Rodetails.Visible = false;
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
                            ddlRecDepot.SelectedValue = Session["G_DepotID"].ToString();
                        }
                        else
                        {
                            trOtherDepot.Visible = false;
                        }

                    }
                    else if (ddlDepositor.SelectedItem.Text.Trim() == "DMO Markfed" && ddlDeliveredAgnt.SelectedItem.Text == "Depositor")
                    {
                        GvDOFPS_on_Fetch.DataSource = null;
                        GvDOFPS_on_Fetch.DataBind();
                        GvuFillMPSCSCTCData.DataSource = null;
                        GvuFillMPSCSCTCData.DataBind();
                        divMPSCSCTCData.Visible = false;
                        trOtherDepot.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                        trcomm.Visible = true;
                        Rodetails.Visible = false;

                    }
                    else if (ddlDepositor.SelectedItem.Text.Trim() == "NAFED" && ddlDeliveredAgnt.SelectedItem.Text == "Depositor" && ddlDepositorType.SelectedItem.Text == "Institution")
                    {
                        GvDOFPS_on_Fetch.DataSource = null;
                        GvDOFPS_on_Fetch.DataBind();
                        GvuFillMPSCSCTCData.DataSource = null;
                        GvuFillMPSCSCTCData.DataBind();
                        divMPSCSCTCData.Visible = false;
                        trOtherDepot.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;
                        trcomm.Visible = true;
                        Rodetails.Visible = false;

                    }
                    else if (ddlDepositor.SelectedItem.Text.Trim() == "DMO Markfed")
                    {
                        GvDOFPS_on_Fetch.DataSource = null;
                        GvDOFPS_on_Fetch.DataBind();
                        GvuFillMPSCSCTCData.DataSource = null;
                        GvuFillMPSCSCTCData.DataBind();
                        divMPSCSCTCData.Visible = false;
                        trOtherDepot.Visible = false;
                        Stockdetails.Visible = false;
                        Issuedetails.Visible = false;

                        fillDO();
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
        if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
        {
            trGPCY.Visible = true;
            fillGPCropYear();

        }
        else
        {
            trGPCY.Visible = false;
        }
        if (ddlDeliveredAgnt.SelectedValue.ToString() == "1" || ddlDeliveredAgnt.SelectedValue.ToString() == "7")
        {
            trdo.Visible = true;
            ddlGodown.Enabled = true;
            ddlDONumber.Visible = false;
            lblDONo.Visible = false;
            txtdateofissue.Visible = true;
            lbldate.Visible = true;
            ddlcomm.Visible = true;
            lblComm.Visible = true;
            trcomm.Visible = true;
            lblgatepasstype.Visible = true;
            rbdate.Visible = true;
            rbdo.Visible = true;
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
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
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
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            //GvDOFPSDsd
            fillDoorStep();

        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Scheme")
        {
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            fillDoorStepToOtherScheme();
        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot")
        {
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            fillGridMPSCSCTCDatadate();
            FillRecDist();
            FillRecDepot(ddlRecDistrict.SelectedValue);
        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot(By Rack)")
        {
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            fillGridMPSCSCTCRailDataDate();
            FillRecDist();
            FillRecDepot(ddlRecDistrict.SelectedValue);
        }
        else
        {
            lblcom.Text = ddlcomm.Text;
            WHRDetails();
        }
    }

    protected void fillDoorStep()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {

                string depotid = Session["G_DepotID"].ToString();
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
                    //old srv  qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='"+ddlGodown.SelectedValue.ToString()+"' and convert(varchar(20),IDO.issue_date,101) ='" + getDate_MDY(txtdateofissue.Text) + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //loc    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,tt.Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id left join MPSCSC.dbo.Transporter_Table as tt on ido.Transporter_id =tt.Transporter_ID and  ido.district_code=tt.Distt_ID where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + getDate_MDY(txtdateofissue.Text) + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "' and  tt.Transport_ID='7' and iDo.Trans_id not in (select trans_id from tbl_RO_Details)";
                    //form tbl issuceagainstdo qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.commodity as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.commodity = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.commodity='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "') and iDo.DOType in ('DS','TO')";
                    //qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.cmd='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
                    qry = "select IDO.qty_issue as 'DO_Qty',IDO.Issue_to_LS_name as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_do] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.cmd='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "') union select IDO.qty_issue as 'DO_Qty','' as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_TO_otherscheme] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.cmd='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";

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
                        string GodownHTypeId = Session["GodownTypeId"].ToString();
                        string GodownTypeId = GodownHTypeId.Trim().ToString();

                        if (GodownTypeId == "2" || GodownTypeId == "3")
                        {
                            WHRDetailsForSilo();
                        }
                        else
                        {
                            WHRDetails();
                        }
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
    protected void fillDoorStepToOtherScheme()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            try
            {

                string depotid = Session["G_DepotID"].ToString();
                string distidsub = Session["Depot_DistID"].ToString().Substring(2, 2);
                string distid = Session["Depot_DistID"].ToString();
                string deliagent = ddlDeliveredAgnt.SelectedItem.Text;
                string godownid = ddlGodown.SelectedValue.ToString();

                if (deliagent == "Other Scheme")
                {

                    string datestring = txtdateofissue.Text;
                    string[] tempsplit = datestring.Split('/');
                    string joinstring = "/";
                    string newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];

                    GvDOFPSDsd.DataSource = null;
                    GvDOFPSDsd.DataBind();
                    string distcode = Session["Depot_DistID"].ToString().Substring(2, 2);
                    lblIssusedQty.Text = "0";
                    qry = "select IDO.qty_issue as 'DO_Qty','' as 'FPS_Name',IDO.delivery_order_no,IDO.trans_id as FPS_Code ,IDO.Godown,convert(varchar(10),IDO.issue_date,103) as 'issue_date',IDO.allotment_month,IDO.allotment_year,IDO.qty_issue as 'Issue_Qty',IDO.bags ,ido.cmd as commid,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name as  'Commodity',IDO.truck_no,IDO.Trans_Id,IDO.Transporter_id  as Transpoter_ID,gd.Godown_Name,'NA' as Transporter_Name from   MPSCSC.dbo.[issue_against_Doorstep_TO_otherscheme] IDO  inner JOIN tbl_MetaData_STORAGE_COMMODITY ON IDO.cmd = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id left  join  tbl_MetaData_GODOWN gd on IDO.Godown=gd.Godown_Id  where IDO.District_code='" + distcode + "' and IDO.IssueCentre_code='" + Session["G_DepotID"].ToString() + "' and Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and convert(varchar(20),IDO.issue_date,101) ='" + newdate + "' and IDO.cmd='" + ddlcomm.SelectedValue.ToString() + "'  and iDo.Trans_id not in (select trans_id from tbl_RO_Details where tbl_RO_Details.District_Id='" + Session["Depot_DistID"].ToString() + "')";
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
        if (ddlDeliveredAgnt.SelectedItem.Text == "Door Step Delivery")
        {
            fillDoorStep();
        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot")
        {
            fillGridMPSCSCTCDatadate();
            FillRecDist();
            FillRecDepot(ddlRecDistrict.SelectedValue);
        }
        else if (ddlDeliveredAgnt.SelectedItem.Text == "Other Depot(By Rack)")
        {
            fillGridMPSCSCTCRailDataDate();
            FillRecDist();
            FillRecDepot(ddlRecDistrict.SelectedValue);
        }
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
    public void GetLatiLongi()
    {
        try
        {
            string qry = "";
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            if (Session["latitude"].ToString() != null && Session["longitude"].ToString() != null)
            {
                latitude = Session["latitude"].ToString();
                longitude = Session["longitude"].ToString();
            }
            if (latitude != "" && longitude != "" && latitude != null && longitude != null && GateP_No != "" && GateP_No != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Issue_GatePass_LatLong]([GatePass_No],[Latitude],[Longitude],[Created_Date],[Client_IP]) VALUES ('" + GateP_No + "','" + latitude + "','" + longitude + "',getdate(),'" + ClientIP + "')";

                cmd = new SqlCommand(qry, con);
                int c = cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Coordinates Updated '); </script> ");
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            else
            {
                //lblmsg.Text = "Errror";
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void fillCropYear()
    {
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        //ddlcropyear.Items.Insert(0, "All");
        //ddlcropyear.SelectedIndex = 0;
        ddlcropyear.SelectedIndex = 1;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
    protected void fillGPCropYear()
    {
        GPddl.Items.Clear();
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));

        GPddl.Items.AddRange(items);
        GPddl.DataBind();
        GPddl.SelectedIndex = 1;
    }
    protected void GPddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getgodowns();
    }
}
