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

public partial class BranchPages_IssueGatepassByDate : System.Web.UI.Page
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                if (Session["lang"].ToString() == "Hindi")
                {
                    lblInstruction.Text = Resources.hindi.lblInstruction;
                    lblDeliveryOrderOfStock.Text = Resources.hindi.lblDeliveryOrderOfStock;
                    lblDepositorType.Text = Resources.hindi.lblDepositorType;
                    lblDepositorName.Text = Resources.hindi.lblDepositorName;
                    //lblIssuedTo.Text = Resources.hindi.lblIssuedTo;
                    //lblDONo.Text = Resources.hindi.lblDONo;
                    lbIssusedQty.Text = Resources.hindi.lbIssusedQty;
                    lblGodownNo.Text = Resources.hindi.lblGodownNo;
                    lblIssuedBags.Text = Resources.hindi.lblIssuedBags;
                    lblIssuedweight.Text = Resources.hindi.lblIssuedweight;
                    lblPercentMoisture.Text = Resources.hindi.lblPercentMoisture;
                    //lblRecDistrict.Text = Resources.hindi.lblRecDistrict;
                    //lblRecDepot.Text = Resources.hindi.lblRecDepot;
                    lblTrans.Text = Resources.hindi.lblTrans;
                    lblTypeVehicle.Text = Resources.hindi.lblTypeVehicle;
                    lblTruckNumber.Text = Resources.hindi.lblTruckNumber;
                    lblArrivalTime.Text = Resources.hindi.lblArrivalTime;
                    lblDesc.Text = Resources.hindi.lblDesc;
                    //lblDONo.Text = Resources.hindi.lblDONo;
                    lblStockforIssue.Text = Resources.hindi.lblStockforIssue;
                    //lblDetailsofRO.Text = Resources.hindi.lblDetailsofRO1;
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
                //trOtherDepot.Visible = false;
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
                    qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    //ddlDeliveredAgnt.Enabled = true;
                }
                else
                {
                    qry = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE BranchId='" + BranchID + "' and Depositor_Type ='" + depositer + "'";
                    //ddlDeliveredAgnt.Enabled = false;

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
                //Disable at 11/14/2017
                //if (depositer != "Institution")
                //{
                //    if (ddlDepositor.Items.Count > 0)
                //    {
                //        ddlDeliveredAgnt.SelectedIndex = 4;
                //        trcomm.Visible = true;
                //        lblgatepasstype.Visible = false;
                //        txtdateofissue.Visible = false;
                //        rbdate.Visible = false;
                //        rbdo.Visible = false;
                //        // ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                //    }
                //}
                //else
                //{
                //    if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                //    {
                //        ddlDeliveredAgnt.SelectedIndex = 4;
                //        trcomm.Visible = true;
                //        lblgatepasstype.Visible = false;
                //        txtdateofissue.Visible = false;
                //        rbdate.Visible = false;
                //        rbdo.Visible = false;
                //    }
                //    else
                //    {
                //        trcomm.Visible = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in depositor type selected index!')", true);
            }
        }
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (ddlDepositorType.SelectedItem.Text == "Institution")
            {
                //ddlDeliveredAgnt.Enabled = true;

                if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPSCSC")
                {
                    // ddlDeliveredAgnt.Items.Clear();
                    txtissuedbags.Enabled = false;
                    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
                    txtissuedwt.Enabled = false;
                    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
                    string notin = "DEPOSITOR";
                    //bindIssuedTo(notin);
                }
                else if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPWLC")
                {
                    txtissuedbags.Enabled = false;
                    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
                    txtissuedwt.Enabled = false;
                    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
                    string notin = "DEPOSITOR','LEAD SOCIETY','FPS";
                    //bindIssuedTo(notin);
                    //GvuFillMPSCSCTCData.DataSource = null;
                    //GvuFillMPSCSCTCData.DataBind();
                    //divMPSCSCTCData.Visible = false;
                }
                else if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    //ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                    //trcomm.Visible = true;
                }
                else
                {
                    string notin = "OTHER DEPOT','LEAD SOCIETY','FPS";
                    //bindIssuedTo(notin);
                    //trOtherDepot.Visible = false;
                    //FillRecDist();
                    //ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
                    //FillRecDepot(ddlRecDistrict.SelectedValue);
                    //ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();
                    //GvuFillMPSCSCTCData.DataSource = null;
                    //GvuFillMPSCSCTCData.DataBind();
                    //divMPSCSCTCData.Visible = false;
                }
            }
            else
            {
                if (ddlDepositor.Items.Count > 0)
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    ////ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                }

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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
                //txtdateofissue.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
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
    protected void ddlcomm_SelectedIndexChanged(object sender, EventArgs e)
    {
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            WHRDetails();   
    }
    private void WHRDetails()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {         
                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    //query = "SELECT DISTINCT row_number() OVER (ORDER BY WHR.Depositor_whr_id) AS 'SNo', GD.Godown_ID,SSD.Stack_ID,WHR.Depositor_WHR_Id,WHR.Depositor_Name,(GD.Godown_Name) as Godown_Name,comd.Commodity_Name,(CONVERT(decimal(18,2),sum(SSD.Weight))-(select ISNULL(CONVERT(DECIMAL(18,2),SUM(DSD.Bags_Weight)),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')-(select ISNULL(SUM(DSD.Loss),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')+(select ISNULL(SUM(DSD.Gain),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableQty,(SUM(SSD.Bags)-(select ISNULL(SUM(DSD.No_Of_Bags),'0') from tbl_Delivery_Stacking_Details_GatePass as DSD join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where DSD.Depositor_WHR_Id = WHR.Depositor_WHR_Id AND GP.Status !='CANCEL')) as avilableBags FROM tbl_MetaData_GODOWN AS GD JOIN tbl_storage_Stacking_Details AS SSD on SSD.Godown_ID = GD.Godown_ID INNER JOIN tbl_storage_Depositor_WHR_Relation AS WHR ON WHR.Depositor_WHR_Id = SSD.WHRId  JOIN tbl_MetaData_STORAGE_COMMODITY AS comd ON comd.Commodity_Id = WHR.Commodity_Id WHERE WHR.BranchId = '" + Session["BranchId"].ToString() + "' and WHR.Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and SSD.Godown_ID='" + ddlGodown.SelectedValue.ToString() + "' and WHR.Commodity_Id in ('35','22','6') group by WHR.Depositor_WHR_Id,gd.Godown_ID,WHR.Depositor_Name,GD.Godown_Name,comd.Commodity_Name,SSD.Stack_ID order by GD.Godown_ID asc ";  
                }
                else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','7') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                }
                else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "25" || lblcom.Text.ToString() == "29" || lblcom.Text.ToString() == "96" || lblcom.Text.ToString() == "97")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('25','29','96','97')  order by Depositor_whr_id";

                }
                else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
                {
                    //if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                    //{
                        query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name in ('DMO Markfed','MPSCSC') and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";

                    //}
                    //else
                    //{
                    //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                    //}
                }
                else
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 order by Depositor_whr_id";
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
                        //btnsave.Enabled = true;
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
            Response.Redirect("~/SessionExpired.htm");
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
                            //else if (ddlDeliveredAgnt.SelectedItem.Text == "Same Godown Transfer(Handover)")
                            //{
                            //    txtissuedbags.Text = totalbags.ToString();
                            //    txtissuedwt.Text = totalwt.ToString();
                            //    lblIssusedQty.Text = totalwt.ToString();
                            //    lblIssusedBags.Text = totalbags.ToString();
                            //}
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

}
