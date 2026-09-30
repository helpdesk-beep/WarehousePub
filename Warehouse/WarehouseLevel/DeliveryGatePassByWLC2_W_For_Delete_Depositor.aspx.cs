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

public partial class WarehouseLevel_DeliveryGatePassByWLC2_W_For_Delete_Depositor : System.Web.UI.Page
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
        //TextBox2_CalendarExtender.EndDate = Convert.ToDateTime("04/30/2018");   //to dissable future  Date
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchId"] != null) && (Session["GodownID_New"] != null))
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
                    //lbIssusedQty.Text = Resources.hindi.lbIssusedQty;
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
                    //lbIssusedQty.Text = Resources.hindi.lbIssusedQty;
                }

                Session["RefreshButton"] = "No";
                Session["dtRo"] = null;

                string script = "$(document).ready(function () { $('[id*=btnsave]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                GetDepositorType();
                //Getgodowns();
                ddlDepositorType_SelectedIndexChanged(sender, e);
                ddlDepositor_SelectedIndexChanged(sender, e);
                //trOtherDepot.Visible = false;
                fillTransporter();
                GetCommodity();
                fillMinitues();
                //Printcurrentdate();
                txtissuedbags.Enabled = false;
                txtissuedwt.Enabled = false;
                fillCropYear();
                fillCropYearGet();

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
    private void GetgodownsForFCIPDS()
    {
        //qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y'  order by Godown_Name ";
        //qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";
        //qry = "select * from tbl_MetaData_GODOWN_2018 as GD inner join tbl_Update_Godown_Wise_Remark_For_FIFO as FF on FF.GodownID = GD.Godown_ID and FF.CategoryID = '4' where GD.BranchID'" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";

        // For FIFO

        //qry = "select * from tbl_MetaData_GODOWN_2018 as GD where GD.BranchID='" + Session["G_BranchId"].ToString() + "' and GD.DistrictId = '" + Session["Depot_DistID"].ToString() + "' and IsActive='Y' and GD.Godown_ID='" + Session["GodownID_New"].ToString() + "' and GD.Godown_ID in (select FF.GodownID from tbl_Update_Godown_Wise_Remark_For_FIFO as FF where FF.CategoryID='4') order by Godown_Name";

        // For Non FIFO
        qry = "select * from tbl_MetaData_GODOWN_2018 where BranchID='" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and IsActive='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";

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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('शाखा प्रबंधक लोगिन से इस गोदाम हेतु फीफो संबंधित आप्शन चयन नहीं किया है, चयन करवाए!')", true);
        }
    }
    private void Getgodowns()
    {
        //qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y'  order by Godown_Name ";
        //qry = "select * from tbl_MetaData_GODOWN where BranchID='" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";


        //qry = "select * from tbl_MetaData_GODOWN_2018 as GD inner join tbl_Update_Godown_Wise_Remark_For_FIFO as FF on FF.GodownID = GD.Godown_ID and FF.CategoryID = '4' where GD.BranchID'" + Session["G_BranchId"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "'  order by Godown_Name ";

        qry = "select * from tbl_MetaData_GODOWN_2018 as GD where GD.BranchID='" + Session["G_BranchId"].ToString() + "' and GD.DistrictId = '" + Session["Depot_DistID"].ToString() + "' and IsActive='Y' and GD.Godown_ID='" + Session["GodownID_New"].ToString() + "' order by Godown_Name";

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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('शाखा प्रबंधक लोगिन से इस गोदाम हेतु फीफो संबंधित आप्शन चयन नहीं किया है, चयन करवाए!')", true);
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
                    //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    //Savan Depositor Name only Fci & Nafed
                    //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed','NAFED','HAFED','NCCF') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    // By Change by Dharmendra
                    qry = "SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR_Log] WHERE  BranchId='" + BranchID + "' and Depositor_Type ='Institution'";
                    //Savan

                    //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name in ('FCI','NAFED') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name in ('FCI','NAFED') and BranchId='" + BranchID + "' and Depositor_Type ='Institution'";

                    //ddlDeliveredAgnt.Enabled = true;
                }
                else
                {
                    //qry = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE BranchId='" + BranchID + "' and Depositor_Type ='" + depositer + "'";
                    qry = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR_Log] WHERE BranchId='" + BranchID + "' and Depositor_Type ='" + depositer + "'";
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
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchId"] != null))
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
                    GetgodownsForFCIPDS();

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
                    GetgodownsForFCIPDS();
                }
                else if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    //ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                    //trcomm.Visible = true;
                    Getgodowns();
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
                    Getgodowns();
                }
            }
            else
            {
                if (ddlDepositor.Items.Count > 0)
                {
                    //ddlDeliveredAgnt.SelectedIndex = 4;
                    ////ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
                    //ddlDeliveredAgnt.Enabled = false;
                    Getgodowns();
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
            if ((Session["Depot_DistID"] != null) && (Session["G_BranchId"] != null))
            {
                qry = "SELECT Transporter_Id, Transpoter_Name FROM tbl_metadata_transport where  DepotID  = '" + Session["G_BranchId"].ToString() + "' order by Transpoter_Name";
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
                //TextBox2.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                TextBox2.Text = "30/11/2017";
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
        //ListItem[] items = new ListItem[7];
        //items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        //items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        //items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        //items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        //items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        //items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        //items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        ListItem[] items = new ListItem[10];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        items[7] = new ListItem((DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString(), (DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2));
        items[8] = new ListItem((DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString(), (DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString().Substring(2, 2));
        items[9] = new ListItem((DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString(), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString().Substring(2, 2));

        //ddlcropyear.Items.Insert(0, "All");
        //ddlcropyear.SelectedIndex = 0;
        ddlcropyear.SelectedIndex = 1;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
    protected void ddlcomm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcropyr.SelectedValue == "2023-2024" && ddlcomm.SelectedValue == "22")
        {
            string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Rabi_Fifo_Godown_Check", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        cmd.Parameters.AddWithValue("GodownID", Session["GodownID_New"].ToString());
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            if (dt.Rows[0]["GodownID"].ToString() == "0")
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('फीफो के अनुसार इस गोडाउन का चुनाव ब्रांच लॉगिन से नहीं किया गया हैं ,फीफो में गोडाउन का चुनाव करने से पहले फीफो की लिस्ट का अध्ययन कर ले जो की ब्रांच की लॉगिन पर दी गई यदि उस लिस्ट में गोडाउन का नंबर सही हैं तो ही इस गोडाउन से स्कंद का भुगतान किया जाये , यदि किसी गोडाउन से फीफो का उल्लंघन करके स्कंद का भुगतान किया जा रहा  हैं तो इसकी पूर्ण जिम्मेदारी ब्रांच मैनेजर की होगी |')", true);
                            }
                            else
                            {
                                lblcom.Text = ddlcomm.SelectedValue.ToString();
                                WHRDetails();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            lblcom.Text = ddlcomm.SelectedValue.ToString();
            WHRDetails();
        }
    }
    private void WHRDetails()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchId"] != null))
        {
            string query = "";
            gdstackdetail.DataSource = null;
            gdstackdetail.DataBind();
            if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
            {
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') and (RecQty - DelQty)>0 and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('35','22','6') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('35','22','6') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('35','22','6') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('35','22','6') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
            }
            else if (lblcom.Text.ToString() == "1" || lblcom.Text.ToString() == "74" || lblcom.Text.ToString() == "20" || lblcom.Text.ToString() == "2" || lblcom.Text.ToString() == "47" || lblcom.Text.ToString() == "3" || lblcom.Text.ToString() == "4")
            {
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('1','74','20','2','47','3','7','4') and (RecQty - DelQty)>0 and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('1','74','20','2','47','3','7','4') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('1','74','20','2','47','3','7','4') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('1','74','20','2','47','3','7','4') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('1','74','20','2','47','3','7','4') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
            }
            else if (lblcom.Text.ToString() == "46" || lblcom.Text.ToString() == "49" || lblcom.Text.ToString() == "50" || lblcom.Text.ToString() == "17" || lblcom.Text.ToString() == "23")
            {
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('46','49','50','17','23') and (RecQty - DelQty)>0 and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('46','49','50','17','23') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('46','49','50','17','23') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('46','49','50','17','23') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('46','49','50','17','23') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
            }
            else if (lblcom.Text.ToString() == "25" || lblcom.Text.ToString() == "29" || lblcom.Text.ToString() == "96" || lblcom.Text.ToString() == "97")
            {
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('25','29','96','97') and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('25','29','96','97') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('25','29','96','97') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('25','29','96','97') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('25','29','96','97') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
            }
            else if (lblcom.Text.ToString() == "13" || lblcom.Text.ToString() == "14" || lblcom.Text.ToString() == "24")
            {
                //if (ddlDeliveredAgnt.SelectedItem.Text == "MPSCSC(Miller)")
                //{
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name in ('DMO Markfed','MPSCSC') and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name in ('DMO Markfed','MPSCSC') and commodity_id in ('13','14','24') and (RecQty - DelQty)>0 and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                if (ddlDepositorType.SelectedItem.Text == "Co-op Societies")
                {
                    query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name=N'" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name=N'" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                }
                else
                {
                    query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and Commodity_Id in ('13','14','24') and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";

                }
                //}
                //else
                //{
                //    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('13','14','24') and (RecQty - DelQty)>0  order by Depositor_whr_id";
                //}
            }
            else
            {
                if (ddlDepositorType.SelectedItem.Text == "Co-op Societies")
                {
                    query = "select top 50 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name=N'" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name=N'" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                }
                else
                {
                    //query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where BranchID = '" + Session["BranchId"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and (RecQty - DelQty)>0 and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR_Issue_Date<=convert(varchar(10),'11/30/2017',101) order by Depositor_whr_id";
                    //query = "select row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and WHR.WHR_Issue_Date<=convert(varchar(10),'04/30/2018',101) group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
                    query = "select top 100 row_number() OVER (ORDER BY WHRId ) AS 'SNo',WHRId as Depositor_whr_id,'' as Lot_No,GodownID as Godown_ID,stack_id as Stack_ID,(RecWeight-(IssueWeight+loss-gain)) as AvailQty,(RecBags-IssueBags) as AvailBags,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName as Stack_Name from (select RecDetail.WHRId,RecDetail.GodownID,RecDetail.stack_id,RecBags,RecWeight, isnull(IssueBags,0) as IssueBags,isnull(IssueWeight,0) as IssueWeight,isnull(Loss,0) as Loss,isnull(Gain,0)  as Gain,WHR_Issue_Date,Depositor_Name,Commodity_Name,StackName from ( select WHR.GodownID,SSD.stack_id,(select Stack_Name from tbl_metadata_stack as MDS where MDS.Stack_ID=SSD.stack_id) as StackName,(select Commodity_Name from tbl_metadata_storage_commodity as cmd where cmd.Commodity_Id=WHR.Commodity_Id) as Commodity_Name,WHRId,convert(varchar(20),WHR_Issue_Date,103) as WHR_Issue_Date,SUM(Weight) as RecWeight,sum(Bags) as RecBags,WHR.Depositor_Name from tbl_storage_Stacking_Details as SSD join tbl_storage_Depositor_WHR_Relation as WHR on SSD.WHRId=WHR.Depositor_WHR_Id where WHR.BranchID='" + Session["G_BranchId"].ToString() + "'  and WHR.GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' group by SSD.stack_id,WHRId,WHR.GodownID,WHR.Depositor_Name,WHR.Commodity_Id,WHR_Issue_Date ) as RecDetail left join ( select DSD.Stack_ID,Depositor_WHR_Id,SUM(DSD.No_Of_Bags) as IssueBags,SUM(DSD.Bags_Weight) as IssueWeight,SUM(DSD.Loss) as Loss,SUM(DSD.Gain) as Gain from tbl_Delivery_Stacking_Details_GatePass as DSD inner join tbl_Storage_GatePass_Enrty AS GP ON GP.GatePass_No = DSD.GatePass_No where GP.Status='Active' and Depositor_WHR_Id in ( select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["G_BranchId"].ToString() + "' and GodownID='" + ddlGodown.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' and Depositor_Name='" + ddlDepositor.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "') group by DSD.Stack_ID,DSD.Depositor_WHR_Id ) as DelDetail on RecDetail.stack_id=DelDetail.Stack_ID and RecDetail.WHRId=DelDetail.Depositor_WHR_Id ) as mrh where RecWeight-(IssueWeight+loss-gain) > 0";
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
                lblnotfound.Text = "आपने गोदाम मे इस Crop Year और Commodity के लिए कोई भी WHR नहीं बनाया है, कृपया जाँच लेवे !";
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
                                //lblIssusedQty.Text = totalwt.ToString();
                                //lblIssusedBags.Text = totalbags.ToString();
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
                            //lblIssusedQty.Text = totalwt.ToString();
                            //lblIssusedBags.Text = totalbags.ToString();
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No"; //Session["RefreshButton"];
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null) && Stockdetails.Visible == true)
        {
            try
            {
                Server.ScriptTimeout = 100000;
                decimal whrqt = 0;
                if (txtissuedbags.Text != "" && txtissuedwt.Text != "")
                {
                    whrqt = Convert.ToDecimal(txtissuedwt.Text);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No of Bags and Weight field cannot be empty!')", true);
                }
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
                //else if (Convert.ToDateTime(getDate_MDY(TextBox2.Text)) > Convert.ToDateTime("11/30/2017"))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You can not issue stock after 30/11/2017')", true);
                //}
                else if (UxTrans.SelectedItem.Text == "---Select---")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Transporter')", true);
                }
                else if (txttruckno.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Vehicle No.')", true);
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

                        cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue.ToString());

                        cmd.Parameters.AddWithValue("@Stack_ID", "stacked");
                        cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                        cmd.Parameters.AddWithValue("@Scheme_ID", "WGP");
                        // string gpdt = DateTime.ParseExact(TextBox2.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                        cmd.Parameters.AddWithValue("@issue_date", getDate_MDY(TextBox2.Text));
                        cmd.Parameters.AddWithValue("@Vehicle_Type", ddlVehicleType.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@BranchId", Session["G_BranchId"].ToString());
                        cmd.Parameters.AddWithValue("@Vehicle_No", txttruckno.Text.ToString());

                        cmd.Parameters.AddWithValue("@Driver_Name", UxTrans.SelectedItem.Text);

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
                            cmd.Parameters.AddWithValue("@Purpose_Of_Issue", "Pending GP");

                            cmd.Parameters.AddWithValue("@Rental_Amt_Received", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Cash_Credit", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_No", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_Date", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_BankId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Sample_Serial_No", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Vikas_Chand", DBNull.Value);
                            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                            cmd.Parameters.AddWithValue("@TransporterId", UxTrans.SelectedValue);

                            cmd.Parameters.AddWithValue("@FPS", "");

                            cmd.Parameters.AddWithValue("@FPSId", DBNull.Value);

                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                            //cmd.Parameters.AddWithValue("@DeliverdAgent", ddlDeliveredAgnt.SelectedValue);
                            cmd.Parameters.AddWithValue("@DeliverdAgent", "WGP");
                            cmd.Parameters.AddWithValue("@RecipientDistrict", DBNull.Value);
                            cmd.Parameters.AddWithValue("@RecipientDepot", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DelveredAddress", DBNull.Value);

                            cmd.Parameters.AddWithValue("@trans_id", DBNull.Value);

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
                        //GetLatiLongi();
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
                        //Rodetails.Visible = false;
                        //divMPSCSCTCData.Visible = false;
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
                //}
                //else
                //{
                //    lblmsg.Text = "Qty issued not equal to DO qty ";

                //}
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
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('First select record. to generate Gatepass')", true);
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
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
                    // UpdateWHRforCSMSIssue.Update_WHR_CSMS_Issue InsertWHROverGP = new UpdateWHRforCSMSIssue.Update_WHR_CSMS_Issue();
                    //InsertWHROverGP.Insert_WHR_CSMS(GateP_No, Dist_id, whrid, getDate_MDY(WhrDate), ddlcomm.SelectedValue.ToString(), Totissue_Wgt.ToString(), Bags.ToString(), Godid, stkid, GateP_No);

                    /////End WebService
                }
            }
        }
        return String.Format("<ROOT>{0}</ROOT>", sb.ToString());
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
    private void Gridclear()
    {
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
        //GvuFillMPSCSCTCData.DataSource = null;
        //GvuFillMPSCSCTCData.DataBind();
        //GvDOFPS_on_Fetch.DataSource = null;
        //GvDOFPS_on_Fetch.DataBind();
    }
    //protected void fillCropYearGet()
    //{
    //    ListItem[] items = new ListItem[10];
    //    items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString());
    //    items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString());
    //    items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString());
    //    items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString());
    //    items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString());
    //    items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString());
    //    items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString());
    //    items[7] = new ListItem((DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2), (DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString());
    //    items[8] = new ListItem((DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString().Substring(2, 2), (DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString());
    //    items[9] = new ListItem((DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString());

    //    //ListItem[] items = new ListItem[10];
    //    //items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
    //    //items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
    //    //items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
    //    //items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
    //    //items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
    //    //items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
    //    //items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
    //    //items[7] = new ListItem((DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2), (DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2));
    //    //items[8] = new ListItem((DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString().Substring(2, 2), (DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString().Substring(2, 2));
    //    //items[9] = new ListItem((DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString().Substring(2, 2));

    //    ddlcropyr.Items.Insert(0, "All");
    //    ddlcropyr.SelectedIndex = 2;
    //    //ddlcropyr.SelectedIndex = 2;
    //    ddlcropyr.Items.AddRange(items);
    //    ddlcropyr.DataBind();
    //}

    protected void fillCropYearGet()
    {
        ListItem[] items = new ListItem[15];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString());
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString());
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString());
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString());
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString());
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString());
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString());
        items[7] = new ListItem((DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString().Substring(2, 2), (DateTime.Now.Year - 7) + "-" + (DateTime.Now.Year - 6).ToString());
        items[8] = new ListItem((DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString().Substring(2, 2), (DateTime.Now.Year - 8) + "-" + (DateTime.Now.Year - 7).ToString());
        items[9] = new ListItem((DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 8).ToString());
        items[10] = new ListItem((DateTime.Now.Year - 10) + "-" + (DateTime.Now.Year - 9).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 9).ToString());
        items[11] = new ListItem((DateTime.Now.Year - 11) + "-" + (DateTime.Now.Year - 10).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 10).ToString());
        items[12] = new ListItem((DateTime.Now.Year - 12) + "-" + (DateTime.Now.Year - 11).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 11).ToString());
        items[13] = new ListItem((DateTime.Now.Year - 13) + "-" + (DateTime.Now.Year - 12).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 12).ToString());
        items[14] = new ListItem((DateTime.Now.Year - 14) + "-" + (DateTime.Now.Year - 13).ToString().Substring(2, 2), (DateTime.Now.Year - 9) + "-" + (DateTime.Now.Year - 13).ToString());
        ddlcropyr.Items.Insert(0, "All");
        ddlcropyr.SelectedIndex = 2;
        //ddlcropyr.SelectedIndex = 2;
        ddlcropyr.Items.AddRange(items);
        ddlcropyr.DataBind();
    }
    protected void ddlcropyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblcom.Text = ddlcomm.SelectedValue.ToString();
        WHRDetails();
        //if (ddlcropyr.SelectedValue == "2023-2024" && ddlcomm.SelectedValue == "22")
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("Get_Rabi_Fifo_Godown_Check", con))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                cmd.Connection = con;
        //                sda.SelectCommand = cmd;
        //                cmd.Parameters.AddWithValue("GodownID", Session["GodownID_New"].ToString());
        //                using (DataTable dt = new DataTable())
        //                {
        //                    sda.Fill(dt);
        //                    if (dt.Rows[0]["GodownID"].ToString() == "0")
        //                    {
        //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('फीफो के अनुसार इस गोडाउन का चुनाव ब्रांच लॉगिन से नहीं किया गया हैं ,फीफो में गोडाउन का चुनाव करने से पहले फीफो की लिस्ट का अध्ययन कर ले जो की ब्रांच की लॉगिन पर दी गई यदि उस लिस्ट में गोडाउन का नंबर सही हैं तो ही इस गोडाउन से स्कंद का भुगतान किया जाये , यदि किसी गोडाउन से फीफो का उल्लंघन करके स्कंद का भुगतान किया जा रहा  हैं तो इसकी पूर्ण जिम्मेदारी ब्रांच मैनेजर की होगी |')", true);
        //                    }
        //                    else
        //                    {
        //                        lblcom.Text = ddlcomm.SelectedValue.ToString();
        //                        WHRDetails();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    lblcom.Text = ddlcomm.SelectedValue.ToString();
        //    WHRDetails();
        //}
    }
}
