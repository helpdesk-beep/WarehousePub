using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Data.SqlClient;
using System.Security;
using System.Configuration;
using System.Data;
using System.Text;
public partial class IssueCenterLevel_Storage_Private_Gatepass : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    string GateP_No = string.Empty;
    String GP_SL_No = "";
    decimal issuestack_wt;
    decimal issuelosswt = 0;
    decimal issuegainwt = 0;
    int calflag = 0;
    SqlTransaction sqltran;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                Session["RefreshButton"] = "No";
                Session["dtRo"] = null;
                GetDepositorType();
                Getgodowns();
                ddlDepositorType_SelectedIndexChanged(sender, e);
                ddlDepositor_SelectedIndexChanged(sender, e);
                trOtherDepot.Visible = false;
                fillTransporter();
                GetCommodity();
                fillMinitues();
            }
           
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    private void GetDepositorType()
    {
        qry = "select Depositor_Type from tbl_MetaData_Depositor_Type where Depositor_Type_Id!='4' order by Depositor_Type";
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
    private void Getgodowns()
    {
        qry = "select * from tbl_MetaData_GODOWN where Depotid='" + Session["Depot_DepotID"].ToString() + "' and DistrictId = '" + Session["Depot_DistID"].ToString() + "'  order by Godown_Name ";
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


    private void WHRDetails()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string query = "";
                gdstackdetail.DataSource = null;
                gdstackdetail.DataBind();
                if (lblcom.Text.ToString() == "35" || lblcom.Text.ToString() == "22" || lblcom.Text.ToString() == "6")
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where depotid = '" + Session["Depot_DepotID"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id in ('35','22','6') order by Depositor_whr_id";
                }
                else
                {
                    query = "SELECT SNo,Depositor_whr_id,Lot_No,Godown_ID,Stack_ID,(RecQty - DelQty) as AvailQty,(RecBags - DelBags) as AvailBags,Depositor_Name,Commodity_Name,Stack_Name FROM [View_WHRcurrentstock] where depotid = '" + Session["Depot_DepotID"].ToString() + "' and Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' and Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' and commodity_id = '" + lblcom.Text.Trim().ToString() + "' order by Depositor_whr_id";
                    //  query = "select distinct row_number() over( order by WHR.Depositor_whr_id) as 'S.No.',WHR.Depositor_whr_id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,sum(SSD.Bags) as Bags,sum(CONVERT(DECIMAL(18,2),SSD.Weight)) AS Weight,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name FROM tbl_storage_Depositor_WHR_Relation AS WHR JOIN tbl_storage_Stacking_Details AS SSD ON WHR.Depositor_WHR_Id = SSD.WHRId join tbl_MetaData_STORAGE_COMMODITY as COM ON WHR.Commodity_Id = COM.Commodity_Id join tbl_MetaData_STACK on SSD.Stack_ID = tbl_MetaData_STACK.Stack_ID WHERE WHR.Depotid = '" + Session["Depot_DepotID"].ToString() + "' AND WHR.Depositor_Name ='" + ddlDepositor.SelectedValue.ToString() + "' AND WHR.Commodity_Id = '" + lblcom.Text.Trim().ToString() + "' AND SSD.Godown_ID = '" + ddlGodown.SelectedValue.ToString() + "' group by WHR.Depositor_WHR_Id,WHR.Lot_No,SSD.Godown_ID,SSD.Stack_ID,WHR.Depositor_Name,COM.Commodity_Name,tbl_MetaData_STACK.Stack_Name";
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
                    lblnotfound.Visible = true;
                    lblnotfound.Text = "आपने इस गोदाम,Commodity पर कोई भी WHR नहीं बनाया है,कृपया पहले WHR बनाये !";
                    btnsave.Enabled = false;
                }
                ds.Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in fill WHRDetails')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                btnsave.Enabled = false;
               // CheckRo();
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
                //else if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                //{
                //    Response.Redirect("PendingGatePassOfDelivery.aspx?PopMsg=The Record Already Saved!!Do Not Refresh again!!!Select GatePass to modify!");
                //}
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

                        qry = "select isnull(Max(GatePass_No),0) from tbl_Storage_GatePass_Enrty where District_Id='" + Session["Depot_DistID"].ToString() + "' and Depot_ID='" + Session["Depot_DepotID"].ToString() + "'";
                        cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            GateP_No = Convert.ToString(Convert.ToInt64(str3) + 1);
                            if (GateP_No != String.Empty || GateP_No != "")
                            {
                            Found:
                                qry = "select count(GatePass_No) from tbl_Storage_GatePass_Enrty where GatePass_No='" + GateP_No.ToString() + "'";
                                cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                string maxcount = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt16(maxcount) > 0)
                                {
                                    GateP_No = Convert.ToString(Convert.ToInt64(GateP_No) + 1);
                                    goto Found;
                                }
                            }
                        }
                        else
                        {
                            string Depotid = Session["Depot_DepotID"].ToString();
                            GateP_No = Depotid + "001";
                        }

                        /////////////////////////////////////////////////////////////////////////////

                        qry = "select isnull(Max(GP_SL_No),0) from tbl_Storage_GatePass_Enrty where District_Id='" + Session["Depot_DistID"].ToString() + "' and Depot_ID='" + Session["Depot_DepotID"].ToString() + "' ";
                        cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                        string str4 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str4) != 0)
                        {
                            GP_SL_No = Convert.ToString(Convert.ToInt64(str4) + 1);
                        }
                        else
                        {
                            GP_SL_No = "1";
                        }
                        //////////////////////////////////////////////////////////////////////////////
                        cmd = new SqlCommand("MPWLC_sp_DOGatePass_GatePass_insert", con, sqltran);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@State_ID", "23");
                        cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                        cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                        cmd.Parameters.AddWithValue("@Depot_Id", Session["Depot_DepotID"].ToString());
                        cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@Stack_ID", "stacked");
                        cmd.Parameters.AddWithValue("@Depositor_Name", ddlDepositor.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                        cmd.Parameters.AddWithValue("@Scheme_ID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Vehicle_Type", ddlVehicleType.SelectedItem.Text.ToString());
                        cmd.Parameters.AddWithValue("@issue_date", Convert.ToDateTime(TextBox2.Text));
                        if (txttruckno.Text.ToString().Trim() == "")
                        {
                            cmd.Parameters.AddWithValue("@Vehicle_No", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Vehicle_No", txttruckno.Text.ToString());
                        }
                        cmd.Parameters.AddWithValue("@Driver_Name", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NO_of_Bage", int.Parse(txtissuedbags.Text.Trim().ToString()));
                        cmd.Parameters.AddWithValue("@Weight", decimal.Parse(txtissuedwt.Text));
                        cmd.Parameters.AddWithValue("@Issue_Source", "RO");
                        cmd.Parameters.AddWithValue("@Issue_Source_ID", "0");
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                        cmd.Parameters.AddWithValue("@Status", "Active");
                        cmd.Parameters.AddWithValue("@Printed", "NO");
                        cmd.Parameters.AddWithValue("@License_No", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Valid_Upto", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Arrival_Dep_Time", ddl1.SelectedValue + ":" + ddl2.SelectedValue + ":" + ddl3.SelectedValue);
                        cmd.Parameters.AddWithValue("@Remarks", txtTruckDetails.Text.ToString().Trim());
                        cmd.Parameters.AddWithValue("@Miller_Id", UxTrans.SelectedValue);
                        cmd.Parameters.AddWithValue("@GP_SL_No", GP_SL_No);
                        int index3 = cmd.ExecuteNonQuery();
                        txtGpid.Text = GateP_No;
                        cmd.Dispose();

                        ////////////////////////////////////////////////////////////////////////////////////
                        if (index3 > 0)
                        {
                           
                            decimal _issedwt = 0;
                            int _issedBags = 0;
                            for (int ino = 0; ino < gdstackdetail.Rows.Count; ino++)
                            {
                                if (((CheckBox)gdstackdetail.Rows[ino].FindControl("ckstack")).Checked == true)
                                {
                                    _issedwt = _issedwt + decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtweight")).Text.ToString());
                                    _issedBags = _issedBags + int.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Text.ToString());
                                }
                            }

                            qry = "select isnull(Max(StockDeliveryOrderGatePass_Id),0) from tbl_Storage_Final_Stock_Delivery_GatePass where District_Id='" + Session["Depot_DistID"].ToString() + "' and DepotId='" + Session["Depot_DepotID"].ToString() + "' ";
                            cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                            string str1 = cmd.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str1) != 0)
                            {
                                _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(str1) + 1);
                                if (_StockDeliveryOrderGatePass_Id != String.Empty || _StockDeliveryOrderGatePass_Id != "")
                                {
                                Found:
                                    qry = "select count(StockDeliveryOrderGatePass_Id) from tbl_Storage_Final_Stock_Delivery_GatePass where StockDeliveryOrderGatePass_Id='" + _StockDeliveryOrderGatePass_Id + "'";
                                    cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                    string maxcount = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(maxcount) > 0)
                                    {
                                        _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(_StockDeliveryOrderGatePass_Id) + 1);
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                string Depotid = Session["Depot_DepotID"].ToString();
                                _StockDeliveryOrderGatePass_Id = Depotid + "1";
                                if (_StockDeliveryOrderGatePass_Id != String.Empty || _StockDeliveryOrderGatePass_Id != "")
                                {
                                Found:
                                    qry = "select count(StockDeliveryOrderGatePass_Id) from tbl_Storage_Final_Stock_Delivery_GatePass where StockDeliveryOrderGatePass_Id='" + _StockDeliveryOrderGatePass_Id + "'";
                                    cmd = new SqlCommand(qry, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                                    string maxcount = cmd.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(maxcount) > 0)
                                    {
                                        _StockDeliveryOrderGatePass_Id = Convert.ToString(Convert.ToInt64(_StockDeliveryOrderGatePass_Id) + 1);
                                        goto Found;
                                    }
                                }
                            }
                            cmd = new SqlCommand("MPWLC_sp_tbl_Storage_Final_Stock_Delivery_GatePass_insert", con, sqltran);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StockDeliveryOrderGatePass_Id", _StockDeliveryOrderGatePass_Id);
                            cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                            cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                            cmd.Parameters.AddWithValue("@Commodity_Id", lblcom.Text.ToString());
                            cmd.Parameters.AddWithValue("@WHR_Id", _StockDeliveryOrderGatePass_Id);
                            cmd.Parameters.AddWithValue("@Qty_Issued_No_Bags_Sound", Convert.ToInt32(_issedBags));
                            cmd.Parameters.AddWithValue("@Qty_Issued_Weight", Convert.ToDecimal(_issedwt));
                            cmd.Parameters.AddWithValue("@Value_Stock_Delivered", DBNull.Value);

                            if (txtmoisture.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters.AddWithValue("@Moisture_Content", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Moisture_Content", Convert.ToDecimal(txtmoisture.Text.Trim().ToString()));
                            }
                            cmd.Parameters.AddWithValue("@Purpose_Of_Issue", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Rental_Amt_Received", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Cash_Credit", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_No", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_Date", DBNull.Value);
                            cmd.Parameters.AddWithValue("@DD_BankId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Sample_Serial_No", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Vikas_Chand", DBNull.Value);
                            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                            cmd.Parameters.AddWithValue("@TransporterId", UxTrans.SelectedValue);
                            cmd.Parameters.AddWithValue("@FPS", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FPSId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                            cmd.Parameters.AddWithValue("@DeliverdAgent", ddlDeliveredAgnt.SelectedValue);

                            if ((ddlDepositor.SelectedItem.Text.ToUpper() != "MPWLC") && ((ddlDepositor.SelectedItem.Text.ToUpper() != "MPSCSC")))
                            {
                                cmd.Parameters.AddWithValue("@RecipientDistrict", Session["Depot_DistID"].ToString());
                                cmd.Parameters.AddWithValue("@RecipientDepot", Session["Depot_DepotID"].ToString());
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
                                        Godid = gdstackdetail.Rows[ino].Cells[11].Text.ToString();
                                        stkid = gdstackdetail.Rows[ino].Cells[12].Text.ToString();
                                        whrid = gdstackdetail.Rows[ino].Cells[3].Text.ToString();
                                        decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtweight")).Text.ToString());
                                        decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[ino].Cells[6].Text.ToString());
                                        int Bags = int.Parse(((TextBox)gdstackdetail.Rows[ino].FindControl("txtbagnumber")).Text.ToString());
                                        if (Bags > int.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString()))
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Issued bags are more than No of bags available')", true);
                                            lblmsg.ForeColor = System.Drawing.Color.Red;
                                            lblmsg.Text = "Issued bags are more than No of bags available";
                                        }
                                        else if (Bags == int.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString()))
                                        {
                                            // if the bags entered is equal to the bags available of the stack than we calculate the Loss and gain of weights for that stack
                                            if (Totissue_Wgt > Avai_Wgt)
                                            {
                                                //gain of weight
                                                _chkwt = Totissue_Wgt - Avai_Wgt;
                                                issuegainwt = _chkwt;
                                                issuelosswt = 0;
                                                cmd = new SqlCommand("INSERT INTO tbl_Delivery_Stacking_Details_GatePass(Godown_ID,Stack_ID,No_Of_Bags    ,Bags_Weight ,CreatedBy,Depositor_WHR_Id,StockDeliveryOrderGatePass_Id,Loss,Gain,GatePass_No)VALUES(@Godown_ID ,@Stack_ID ,@No_Of_Bags ,@Bags_Weight,@CreatedBy ,@Depositor_WHR_Id ,@StockDeliveryOrderGatePass_Id ,@Loss ,@Gain,@GatePass_No )", con, sqltran);
                                                cmd.CommandType = CommandType.Text;
                                                cmd.Parameters.AddWithValue("@Godown_ID", Godid);
                                                cmd.Parameters.AddWithValue("@Stack_ID", stkid);
                                                cmd.Parameters.AddWithValue("@No_Of_Bags", Bags);
                                                cmd.Parameters.AddWithValue("@Bags_Weight", Totissue_Wgt);
                                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                                                cmd.Parameters.AddWithValue("@Depositor_WHR_Id", whrid);
                                                cmd.Parameters.AddWithValue("@StockDeliveryOrderGatePass_Id", _StockDeliveryOrderGatePass_Id);
                                                cmd.Parameters.AddWithValue("@Loss", issuelosswt);
                                                cmd.Parameters.AddWithValue("@Gain", issuegainwt);
                                                cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                                cmd.ExecuteNonQuery();
                                                cmd.Dispose();
                                                issuegainwt = 0;
                                                issuelosswt = 0;
                                            }
                                            else if (Totissue_Wgt < Avai_Wgt)
                                            {
                                                // loss of weight
                                                _chkwt = Avai_Wgt - Totissue_Wgt;
                                                issuelosswt = _chkwt;
                                                issuegainwt = 0;
                                                cmd = new SqlCommand("sp_tbl_Delivery_Stacking_Details_GatePass_insert", con, sqltran);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                cmd.Parameters.AddWithValue("@Godown_ID", Godid);
                                                cmd.Parameters.AddWithValue("@Stack_ID", stkid);
                                                cmd.Parameters.AddWithValue("@No_Of_Bags", Bags);
                                                cmd.Parameters.AddWithValue("@Bags_Weight", Totissue_Wgt);
                                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                                                cmd.Parameters.AddWithValue("@Depositor_WHR_Id", whrid);
                                                cmd.Parameters.AddWithValue("@StockDeliveryOrderGatePass_Id", _StockDeliveryOrderGatePass_Id);
                                                cmd.Parameters.AddWithValue("@Loss", issuelosswt);
                                                cmd.Parameters.AddWithValue("@Gain", issuegainwt);
                                                cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                                cmd.ExecuteNonQuery();
                                                cmd.Dispose();
                                                issuegainwt = 0;
                                                issuelosswt = 0;
                                            }

                                        }
                                        else if (Bags < int.Parse(gdstackdetail.Rows[ino].Cells[5].Text.ToString()))
                                        {
                                            // insert when No loss or Gain...
                                            cmd = new SqlCommand("sp_tbl_Delivery_Stacking_Details_GatePass_insert", con, sqltran);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@Godown_ID", Godid);
                                            cmd.Parameters.AddWithValue("@Stack_ID", stkid);
                                            cmd.Parameters.AddWithValue("@No_Of_Bags", Bags);
                                            cmd.Parameters.AddWithValue("@Bags_Weight", Totissue_Wgt);
                                            cmd.Parameters.AddWithValue("@CreatedBy", Session["UserName"].ToString());
                                            cmd.Parameters.AddWithValue("@Depositor_WHR_Id", whrid);
                                            cmd.Parameters.AddWithValue("@StockDeliveryOrderGatePass_Id", _StockDeliveryOrderGatePass_Id);
                                            cmd.Parameters.AddWithValue("@Loss", issuelosswt);
                                            cmd.Parameters.AddWithValue("@Gain", issuegainwt);
                                            cmd.Parameters.AddWithValue("@GatePass_No", GateP_No);
                                            cmd.ExecuteNonQuery();
                                            cmd.Dispose();
                                            issuegainwt = 0;
                                            issuelosswt = 0;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        sqltran.Commit();
                    #endregion

                        Empty();
                        Gridclear();

                        Session["RefreshButton"] = "Yes";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                        Session["dtRo"] = null;

                        //For gate pass link and pop up
                        trlnk.Visible = true;
                        string Roid = "Gate_Pass.aspx?src=RO&vu=" + GateP_No;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script>");
                        sb.Append("window.open(");
                        sb.Append("'" + Roid + "'");
                        sb.Append(",'MyWindow', 'height=800,width=780');");
                        sb.Append("</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
                        btnNewMC.Enabled = true;
                        btnsave.Enabled = false;
                       
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
            catch (Exception ex)
            {
                lblmsg.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        btnsave.Enabled = true;
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
    protected void btnNewMC_Click(object sender, EventArgs e)
    {
        try
        {
            Empty();
            Gridclear();
            gdstackdetail.DataSource = null;
            gdstackdetail.DataBind();
            Session["RefreshButton"] = "No";
            Response.Redirect("Private_Gatepass.aspx");
        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg3", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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
                        Stack_id = gdstackdetail.Rows[s].Cells[12].Text.ToString();
                        WHR_ID = gdstackdetail.Rows[s].Cells[3].Text.ToString();
                        int Bags = int.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Text.ToString());
                        if (Bags == int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
                        {
                            decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString());
                            decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[6].Text.ToString());
                            if (Totissue_Wgt > Avai_Wgt)
                            {
                                //gain
                                string msg;
                                decimal bal = Totissue_Wgt - Avai_Wgt;
                                msg = "There is" + " " + Convert.ToString(bal) + " " + "Qtls.kgs gain of weight for the stack";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + msg + "')", true);
                            }
                            else if (Totissue_Wgt < Avai_Wgt)
                            {
                                // loss 
                                string msg;
                                decimal bal = Avai_Wgt - Totissue_Wgt;
                                msg = "There is" + " " + Convert.ToString(bal) + " " + "Qtls.kgs loss of weight for the stack";
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + msg + "')", true);
                            }
                        }
                        else if (Bags < int.Parse(gdstackdetail.Rows[s].Cells[5].Text.ToString()))
                        {
                            if (decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString()) == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid No of Bags/Weight Issued from stack')", true);
                                ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                                calculationflag = false;
                            }
                            else
                            {
                                decimal Totissue_Wgt = decimal.Parse(((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text.ToString());
                                decimal Avai_Wgt = decimal.Parse(gdstackdetail.Rows[s].Cells[6].Text.ToString());
                                if (Totissue_Wgt > Avai_Wgt)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entered weight cannot be greater than the Available weight in the stack until Bags are equal')", true);
                                    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    lblmsg.Text = "Entered weight cannot be greater than the Available weight in the stack until Bags are equal";
                                    ((CheckBox)gdstackdetail.Rows[s].FindControl("ckstack")).Checked = false;
                                    calculationflag = false;
                                }
                                else
                                {
                                    calculationflag = true;
                                }
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
                    }
                }
                else
                {
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled = true;
                    ((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Enabled = true;
                }

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
                        totalwt = totalwt + decimal.Parse(((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Text.ToString());
                        ((TextBox)gdstackdetail.Rows[i].FindControl("txtbagnumber")).Enabled = false;
                        ((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Enabled = false;

                    }
                    else
                    {
                        ((TextBox)gdstackdetail.Rows[i].FindControl("txtbagnumber")).Enabled = true;
                        ((TextBox)gdstackdetail.Rows[i].FindControl("txtweight")).Enabled = true;
                    }

                }
                txtissuedbags.Text = totalbags.ToString();
                txtissuedwt.Text = totalwt.ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in ckstack_CheckedChanged')", true);
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
                if (depositer == "Institution")
                {
                    qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  depot_id='" + depotid + "' and Depositor_Type ='Institution'";
                }
                else
                {
                    qry = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE depot_id='" + depotid + "' and Depositor_Type ='" + depositer + "'";
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
                    ddlDeliveredAgnt.DataSource = ds;
                    ddlDeliveredAgnt.DataTextField = "Depositor_Name";
                    ddlDeliveredAgnt.DataValueField = "Depositor_Name";
                    ddlDeliveredAgnt.DataBind();
                }
                else
                {
                    //ddlDeliveredAgnt.DataSource = null;
                   
                    //ddlDeliveredAgnt.DataBind();
                    //ddlDeliveredAgnt.SelectedItem.Text = null;
                    //ddlDeliveredAgnt.Text = "";
                }
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
            if (ddlDepositor.Items.Count > 0)
            {
                ddlDeliveredAgnt.SelectedItem.Text = ddlDepositor.SelectedItem.Text;
            }
            //if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPSCSC")
            //{
            //    txtissuedbags.Enabled = false;
            //    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
            //    txtissuedwt.Enabled = false;
            //    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
            //    string notin = "DEPOSITOR";
            //   // bindIssuedTo(notin);
            //}
            //else if (ddlDepositor.SelectedItem.Text.ToUpper() == "MPWLC")
            //{
            //    txtissuedbags.Enabled = false;
            //    txtissuedbags.BackColor = System.Drawing.Color.LemonChiffon;
            //    txtissuedwt.Enabled = false;
            //    txtissuedwt.BackColor = System.Drawing.Color.LemonChiffon;
            //    string notin = "DEPOSITOR','LEAD SOCIETY','FPS";
               
            //}
            //else
            //{
            //    string notin = "OTHER DEPOT','LEAD SOCIETY','FPS";
               
            //    trOtherDepot.Visible = false;
               
            //    ddlRecDistrict.SelectedValue = Session["Depot_DistID"].ToString();
                
            //    ddlRecDepot.SelectedValue = Session["Depot_DepotID"].ToString();
               
            //}
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
           
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
   

    private void Gridclear()
    {
        gdstackdetail.DataSource = null;
        gdstackdetail.DataBind();
       
    }

    protected void fillGridMPSCSCFPSData()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
               
                   
                        WHRDetails();
                    
                
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
    
    protected void ddlDeliveredAgnt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcomm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodown.SelectedItem.Text != "--Select--")
        {
            lblcom.Text = ddlcomm.Text;
            WHRDetails();
        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select godown first')", true);
        }
    }
}