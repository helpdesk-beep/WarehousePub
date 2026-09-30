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
using System.Threading;
public partial class IssueCenterLevel_Storage_WLC_FRM_01_02_03_Receipt_New : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (Session["lang"].ToString() == "Hindi")
                {
                    lblInstruction.Text = Resources.hindi.lblInstruction1;
                    lblDepositDetail.Text = Resources.hindi.lblSourceOfDeposit;
                    lblA_Dist.Text = Resources.hindi.lblA_Dist;
                    lblA_Depo.Text = Resources.hindi.lblA_Depo;
                    lblTCNo.Text = Resources.hindi.lblTCNo;
                    lblTruckNo.Text = Resources.hindi.lblTruckNumber;
                    lblCommodity.Text = Resources.hindi.lblCommodity;
                    lblCategoty.Text = Resources.hindi.lblCategory;
                    lblDepositDate.Text = Resources.hindi.lblDepositDate;
                    lblBags.Text = Resources.hindi.lblBagNumber;
                    lblQtyDeposit.Text = Resources.hindi.lblQtyDeposit;
                    lblMoisture.Text = Resources.hindi.lblMoisture;
                    lblScheme.Text = Resources.hindi.lblScheme;
                    lblWCMNo.Text = Resources.hindi.lblWCMNo;
                    lblweigmentMode.Text = Resources.hindi.lblweigmentMode;
                    lblTransporter.Text = Resources.hindi.lblTransporter;
                    lblCMR_Rice_Miller.Text = Resources.hindi.lblCMR_Rice_Miller;
                    lblDepositorType.Text = Resources.hindi.lblDepositorType;
                    lblDepositorName.Text = Resources.hindi.lblDepositorName;
                    lblSource.Text = Resources.hindi.lblSource;
                    lblTCNo_Non.Text = Resources.hindi.lblTCNo_Non;
                    lblTruckNo_Non.Text = Resources.hindi.lblTruckNumber;
                    lblCommodity_Non.Text = Resources.hindi.lblCommodity;
                    lblCategoty_Non.Text = Resources.hindi.lblCategry;
                    lblDepositDate_Non.Text = Resources.hindi.lblDepositDate;
                    lblBags_Non.Text = Resources.hindi.lblBagNumber;
                    lblQtyDeposit_Non.Text = Resources.hindi.lblQtyDeposit;
                    lblMoisture_Non.Text = Resources.hindi.lblMoisture;
                    lblScheme_Non.Text = Resources.hindi.lblScheme;
                    lblWCMNo_Non.Text = Resources.hindi.lblWCMNo;
                    lblweigmentMode_Non.Text = Resources.hindi.lblweigmentMode;
                    lblTransporter_Non.Text = Resources.hindi.lblTransporter;
                    lblCMR_Rice_Miller_Non.Text = Resources.hindi.lblCMR_Rice_Miller;
                    lblGodownNo.Text = Resources.hindi.lblGodownNo;
                    lblStackNo.Text = Resources.hindi.lblStackNo;
                    lblStackBags.Text = Resources.hindi.lblBagNumber;
                    lblStackWt.Text = Resources.hindi.lblStackWt;
                    lblStackMaxCap.Text = Resources.hindi.lblStackMaxCap;
                    lblStackCurrentCapacity.Text = Resources.hindi.lblStackCurrentCapacity;
                    lblStackAvailable.Text = Resources.hindi.lblStackAvailable;
                    btnAddStack.Text = Resources.hindi.btnAddStack;
                    lblStackingInform.Text = Resources.hindi.lblStackingInform;
                    lblRemarks.Text = Resources.hindi.lblRemarks;
                    lblBagsAcceptable.Text = Resources.hindi.lblBagsAcceptable;
                    lblQtyAcceptable.Text = Resources.hindi.lblQtyAcceptable;
                    lblBagsAcceptable_Non.Text = Resources.hindi.lblBagsAcceptable_Non;
                    lblQtyAcceptable_Non.Text = Resources.hindi.lblQtyAcceptable_Non;
                    lblDistrict.Text = Resources.hindi.lblDistrict;
                    lblSSociety.Text = Resources.hindi.lblSourcesociety;
                    lblDepot.Text = Resources.hindi.lblDepot;
                    fillCropYear();
                }

                if (!IsPostBack)
                {
                    fillCropYear();
                    string script = "$(document).ready(function () { $('[id*=btnsave]').click(); $('[id*=ddlGodownNo]').click(); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
                    

                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }

                    fillCropYear();
                    fillCommodity();
                    fillCommodity_non();
                    if (Session["WLCDepSource"] != null)
                    {

                        if (Session["WLCDepSource"].ToString() == "NON-MPSCSC")
                        {
                            fillDefaultsForNonMPSCSC();
                            ddldepositortype_SelectedIndexChanged(sender, e);
                           // ddlCommodity_Non_SelectedIndexChanged(sender, e);
                            pnlFCI_OTDepot.Visible = false;
                            pnlNONMPSCSC.Visible = true;
                            Session["Mode"] = "NON-Add";
                            //ddldepositortype.SelectedItem.Text = Session["depositortypeA"].ToString();
                          //  ddlDepositor.Text = Session["DepositorA"].ToString();
                        }
                        else
                        {
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }

                           // string query = "select source_name,Convert(varchar(10),getdate(),103) from MPSCSC.dbo.[Source_Arrival_Type] where Source_ID='" + Session["WLCDepSource"].ToString() + "'";
                            string query = "select source_name,Convert(varchar(10),getdate(),103) from MPSCSC.dbo.[Source_Arrival_Type] where Source_ID='" + Session["WLCDepSource"].ToString() + "'";
                            SqlCommand cmd = new SqlCommand(query, Con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            cmd.Dispose();

                            lblDepositDetail.Text = lblDepositDetail.Text + "( " + ds.Tables[0].Rows[0][0].ToString() + " )";
                            pnlFCI_OTDepot.GroupingText = ds.Tables[0].Rows[0][0].ToString();
                            string WLCDist = Session["Depot_DistID"].ToString().Substring(2, 2);
                            Todaydate = ds.Tables[0].Rows[0][1].ToString();
                            pnlNONMPSCSC.Visible = false;
                            
                        }
                    }
                    Session["RefreshButton"] = "No";
                    fillCategory();
                    fillCategoryNon();
                    ViewState["ckstat"] = "Empty";
                    ViewState["ckEditstat"] = "Empty";
                    fillCMR_Rice_Miller();
                    fillTransporter();
                    fillScheme();
                    if (Session["WLC_StorageReceipt_Id"] == null)
                    {
                        if (Session["Mode"] != null)
                        {
                            if ((Session["Mode"].ToString() == "Add") && (Session["WLCDepSource"].ToString() != "NON-MPSCSC"))
                            {
                                if (Todaydate.ToString() != "")
                                {
                                    txtDepositDate.Text = Todaydate.ToString();
                                }
                                if (Session["WLCDepSource"] != null)
                                {
                                    if (Session["WLCDepSource"].ToString() == "07")//From Rail Head
                                    {
                                        Session["dt1"] = null;
                                        pnlFCI_OTDepot.Visible = true;
                                        FillRailHeadData();
                                        pnlNONMPSCSC.Visible = false;
                                    }
                                    else
                                    {
                                        Session["dt1"] = null;
                                        pnlFCI_OTDepot.Visible = true;
                                        FillFCIOTDeoptData();
                                        pnlNONMPSCSC.Visible = false;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Invalid Record access, try again!')", true);
                                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record access, try again!'); </script> ");
                                }
                                btnUpdate.Visible = false;
                                btnsave.Visible = true;
                            }
                            else if ((Session["Mode"].ToString() == "NON-Add") && (Session["WLCDepSource"].ToString() == "NON-MPSCSC"))
                            {
                                //For NON_MPSCSC Add
                                pnlFCI_OTDepot.Visible = false;
                                pnlNONMPSCSC.Visible = true;
                                btnUpdate.Visible = false;
                                btnsave.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    Session.Remove("DType");
                    Session.Remove("Did");
                }
                if (Session["WLC_StorageReceipt_Id"] != null)
                {
                    if (Session["Mode"] != null)
                    {
                        if (Session["Mode"].ToString() == "NON-Edit")//Non MPSCSC Edit
                        {
                            //Edit For NON_MPSCSC
                            btnUpdate.Visible = true;
                           // imgFCI.Visible = false;
                            imgCalNon.Visible = false;
                            btnsave.Visible = false;
                            gdEditStackingDetails.Visible = true;
                            gdstackingdetails.Visible = false;
                            if (!IsPostBack)
                            {
                                FillDefaultsForUpdateNonMPSCSC();
                            }
                        }
                        else if (Session["Mode"].ToString() == "Edit")
                        {
                            btnUpdate.Visible = true;
                         //   imgFCI.Visible = false;
                            btnsave.Visible = false;
                            gdEditStackingDetails.Visible = true;
                            gdstackingdetails.Visible = false;
                            if (!IsPostBack)
                            {
                                FillDefaultsForUpdate();
                            }
                        }
                        else if (Session["Mode"].ToString() == "Add")
                        {
                            btnUpdate.Visible = false;
                            btnsave.Visible = true;
                            gdEditStackingDetails.Visible = false;
                            gdstackingdetails.Visible = true;
                            Session["EditStack"] = null;
                        }
                    }
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnsave.Visible = true;
                    gdEditStackingDetails.Visible = false;
                    gdstackingdetails.Visible = true;
                    Session["EditStack"] = null;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred(page Load) , try again!'); </script> ");
            }
            finally
            {
                fillCropYear();
                Con.Close();
                
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    private void fillCategoryNon()
    {
        try
        {
            string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory_Non.Items.Clear();
                ddlCategory_Non.DataSource = ds.Tables[0];
                ddlCategory_Non.DataTextField = "Category_Name";
                ddlCategory_Non.DataValueField = "Category_Id";
                ddlCategory_Non.DataBind();
            }
        }
        catch (Exception)
        {
            // throw;
        }
    }

    private void fillScheme()
    {
        try
        {
            string query = "SELECT Scheme_Name, Scheme_Id FROM tbl_MetaData_SCHEME where Status='Y' order by Qry_Order";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlScheme.DataSource = ds.Tables[0];
                ddlScheme.DataTextField = "Scheme_Name";
                ddlScheme.DataValueField = "Scheme_Id";
                ddlScheme.DataBind();
            }
        }
        catch (Exception)
        {

            //  throw;
        }
    }

    private void fillTransporter()
    {
        try
        {
           // string query = "SELECT Transporter_ID, Transporter_Name FROM MPSCSC.dbo.Transporter_Table WHERE IsActive = 'Y' order by Transporter_Name";
            string query = "SELECT Transporter_ID, Transporter_Name FROM MPSCSC.dbo.Transporter_Table WHERE IsActive = 'Y' order by Transporter_Name";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTransporter.Items.Clear();
                ddlTransporter.DataSource = ds.Tables[0];
                ddlTransporter.DataTextField = "Transporter_Name";
                ddlTransporter.DataValueField = "Transporter_ID";
                ddlTransporter.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    private void fillCMR_Rice_Miller()
    {
        try
        {
            if (Session["WLCDepSource"].ToString() == "04" || Session["WLCDepSource"].ToString() == "05")
            {
                //string query = "SELECT [Miller_ID], [Miller_Name] FROM MPSCSC.dbo.[Miller_Master]";
                //string query = "SELECT [Miller_ID], [Miller_Name] FROM MPSCSC.dbo.[Miller_Master]";
                string query = "SELECT [Registration_ID],[Mill_Name] FROM [MPSCSC].[dbo].[Miller_Registration] where Registration_ID in (SELECT distinct s_name FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCMR_Rice_Miller.Items.Clear();
                    ddlCMR_Rice_Miller.DataSource = ds.Tables[0];
                    ddlCMR_Rice_Miller.DataTextField = "Mill_Name";
                    ddlCMR_Rice_Miller.DataValueField = "Registration_ID";
                    ddlCMR_Rice_Miller.DataBind();
                }
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void fillDefaultsForNonMPSCSC()
    {
        try
        {
             string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id; select '0' as 'Transporter_ID',' NA' as 'Transporter_Name' Union SELECT Transporter_ID,Transporter_Name FROM MPSCSC.dbo.[Transporter_Table] WHERE IsActive = 'Y' and Distt_Id='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' order by Transporter_Name; SELECT '0' as [Source_ID] ,' --Select--' as [Source_Name] union SELECT [Source_ID],[Source_Name] FROM MPSCSC.dbo.[Source_Arrival_Type] order by  Source_ID; select Convert(varchar(10),getdate(),103)";
            //string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id; select '0' as 'Transporter_ID',' NA' as 'Transporter_Name' Union SELECT Transporter_ID,Transporter_Name FROM MPSCSC.dbo.[Transporter_Table] WHERE IsActive = 'Y' and Distt_Id='" + Session["Depot_DistID"].ToString().Substring(2, 2) + "' order by Transporter_Name; SELECT '0' as [Source_ID] ,' --Select--' as [Source_Name] union SELECT [Source_ID],[Source_Name] FROM MPSCSC.dbo.[Source_Arrival_Type] order by  Source_ID; select Convert(varchar(10),getdate(),103)";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddldepositortype.DataSource = ds.Tables[0];
            ddldepositortype.DataTextField = "Depositor_Type";
            ddldepositortype.DataValueField = "Depositor_Type";
            ddldepositortype.DataBind();
            ddldepositortype.Items.Insert(0, "--Select--");
            //For selected Depositor
            if (Session["DType"] != null)
            {
                for (int i = 0; i < ddldepositortype.Items.Count; i++)
                {
                    if (ddldepositortype.Items[i].Value == Session["DType"].ToString())
                    {
                      ddldepositortype.Items[i].Selected = true;
                    }
                }
            }
            //
            ddlTransporter_Non.DataSource = ds.Tables[1];
            ddlTransporter_Non.DataTextField = "Transporter_Name";
            ddlTransporter_Non.DataValueField = "Transporter_ID";
            ddlTransporter_Non.DataBind();
            ddldepositortype.SelectedItem.Text = Session["depositortypeA"].ToString();
            ddlDepositor.SelectedItem.Text = Session["DepositorA"].ToString();
            ddlArrival_Source.Items.Clear();
            ddlArrival_Source.DataSource = ds.Tables[2];

            ddlArrival_Source.DataTextField = "Source_Name";
            ddlArrival_Source.DataValueField = "Source_ID";
            ddlArrival_Source.DataBind();

            Con.Close();
            cmd.Dispose();

            txtDepositDate_Non.Text = ds.Tables[3].Rows[0][0].ToString();

        }
        catch (Exception ex)
        {
          //  Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FlillDefaultsNonMPSCSC has occurred , try again!'); </script> ");
        }
    }

    protected void FillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlGodownNo.Items.Clear();
                string query = "";
               
                if (Session["Mode"].ToString() == "NON-Edit")//Non MPSCSC Edit
                {
                    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchID = '" + Session["BranchId"].ToString() + "'  ORDER BY [Godown_Name] ";
                }

                else
                {
                   
                    if (Session["WLCDepSource"].ToString() == "NON-MPSCSC")
                    {
                        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchID = '" + Session["BranchId"].ToString() + "' ORDER BY [Godown_Name] ";
                    }
                    else
                    {
                        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchID = '" + Session["BranchId"].ToString() + "' ORDER BY [Godown_Name] ";
                    }
                }
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlGodownNo.DataSource = ds.Tables[0];
                    ddlGodownNo.DataTextField = "Godown_Name";
                    ddlGodownNo.DataValueField = "Godown_ID";
                    ddlGodownNo.DataBind();
                    ddlGodownNo.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlGodownNo.DataSource = null;
                    ddlGodownNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message.ToString();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillGodown has occurred , try again!'); </script> ");
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void FillDefaultsForUpdate()
    {
        try
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("WLC_FillDefaultsForUpdate", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@storageReceipt_ID", SqlDbType.NVarChar, 20);
            cmd.Parameters["@storageReceipt_ID"].Value = Session["WLC_StorageReceipt_Id"].ToString();

            da.SelectCommand = cmd;
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 1)
            {
                if (Session["WLCDepSource"].ToString() == "07")//From Rail Head
                {
                    txtTCNo.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString();
                    txtTCNo.Enabled = false;
                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtTruckNo.Enabled = false;
                    ddlCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
                    ddlCommodity.Enabled = false;
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                    ddlCategory.Enabled = false;
                    hfBags.Value = ds.Tables[0].Rows[0]["Qty_Rvd_No_of_Bags"].ToString();
                    txtQtyAcceptable.Text = ds.Tables[0].Rows[0]["Qty_Rvd_Weight"].ToString();
                    txtBagsAcceptable.Text = ds.Tables[0].Rows[0]["Qty_Rvd_No_of_Bags"].ToString();
                    txtQtyDeposit.Text = ds.Tables[0].Rows[0]["Qty_Wt"].ToString();
                    txtQtyDeposit.Enabled = false;
                    txtBags.Text = ds.Tables[0].Rows[0]["Qty_No_of_Bags"].ToString();
                    txtBags.Enabled = false;
                    ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_Id"].ToString();
                    ddlTransporter.Enabled = false;

                    if (ds.Tables[0].Rows[0]["scheme_Id"].ToString() == "0")
                    {
                        ddlScheme.Items[0].Selected = true;
                    }
                    else
                    {
                        ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["scheme_Id"].ToString();
                    }
                    txtDepositDate.Text = ds.Tables[0].Rows[0]["Receipt_Date"].ToString();
                   // txtDepositDate.Enabled = false;
                    txtMoisture.Text = ds.Tables[0].Rows[0]["Quality_Moisture"].ToString();
                 //   txtWCMNo.Text = ds.Tables[0].Rows[0]["WCMNo_Sending"].ToString();
                    ddlWeigmentMode.SelectedValue = ds.Tables[0].Rows[0]["Mode_of_weighment"].ToString();
                    ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Mode_of_weighment"].ToString();
                    ddlTransporter.Enabled = false;
                    ddlCMR_Rice_Miller.SelectedValue = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                    ddlCMR_Rice_Miller.Enabled = false;
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    lblA_Dist.Visible = false;
                    lblA_Depo.Visible = false;
                    ddlA_Dist.Visible = false;
                    ddlA_Depo.Visible = false;
                    ddlCMR_Rice_Miller.Visible = false;
                    lblCMR_Rice_Miller.Visible = false;
                    FillGodown();
                    fillStack();
                    pnlFCI_OTDepot.Visible = true;
                }
                else
                {
                    txtTCNo.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString();
                    txtTCNo.Enabled = false;
                    txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                    txtTruckNo.Enabled = false;
                    ddlCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
                    ddlCommodity.Enabled = false;
                    ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                    ddlCategory.Enabled = false;
                    hfBags.Value = ds.Tables[0].Rows[0]["Qty_Rvd_No_of_Bags"].ToString();
                    txtBagsAcceptable.Text = ds.Tables[0].Rows[0]["Qty_Rvd_No_of_Bags"].ToString();
                    txtQtyAcceptable.Text = ds.Tables[0].Rows[0]["Qty_Rvd_Weight"].ToString();
                    txtQtyDeposit.Text = ds.Tables[0].Rows[0]["Qty_Wt"].ToString();
                    txtQtyDeposit.Enabled = false;
                    txtBags.Text = ds.Tables[0].Rows[0]["Qty_No_of_Bags"].ToString();
                    txtBags.Enabled = false;
                    ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_Id"].ToString();
                    ddlTransporter.Enabled = false;
                    if (ds.Tables[0].Rows[0]["scheme_Id"].ToString() == "0")
                    {
                        ddlScheme.Items[0].Selected = true;
                    }
                    else
                    {
                        ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["scheme_Id"].ToString();
                    }
                    ddlScheme.Enabled = false;
                    txtDepositDate.Text = ds.Tables[0].Rows[0]["Receipt_Date"].ToString();
                    txtDepositDate.Enabled = false;
                    txtMoisture.Text = ds.Tables[0].Rows[0]["Quality_Moisture"].ToString();
                    //txtWCMNo.Text = ds.Tables[0].Rows[0]["WCMNo_Sending"].ToString();
                    ddlWeigmentMode.SelectedValue = ds.Tables[0].Rows[0]["Mode_of_weighment"].ToString();
                    ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Mode_of_weighment"].ToString();
                    ddlTransporter.Enabled = false;
                    ddlCMR_Rice_Miller.SelectedValue = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                    ddlCMR_Rice_Miller.Enabled = false;
                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                    if (Session["WLCDepSource"].ToString() == "04" || Session["WLCDepSource"].ToString() == "05")
                    {
                        ddlCMR_Rice_Miller.SelectedValue = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                        ddlCMR_Rice_Miller.Enabled = false;
                        lblA_Dist.Visible = false;
                        lblA_Depo.Visible = false;
                        ddlA_Dist.Visible = false;
                        ddlA_Depo.Visible = false;
                    }
                    else if (Session["WLCDepSource"].ToString() == "03")
                    {
                        ddlA_Dist.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderDist"].ToString(), ds.Tables[0].Rows[0]["Sender_district"].ToString()));
                        ddlA_Depo.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderGd"].ToString(), ds.Tables[0].Rows[0]["Sender_godown"].ToString()));

                        ddlA_Dist.Enabled = false;
                        ddlA_Depo.Enabled = false;

                        lblA_Dist.Text = "FCI AM Office";
                        lblA_Depo.Text = "Dispatch Depot";

                        ddlCMR_Rice_Miller.Visible = false;
                        lblCMR_Rice_Miller.Visible = false;
                    }
                    else if (Session["WLCDepSource"].ToString() == "02")
                    {
                        ddlA_Dist.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderDist"].ToString(), ds.Tables[0].Rows[0]["Sender_district"].ToString()));
                        ddlA_Depo.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderGd"].ToString(), ds.Tables[0].Rows[0]["Sender_godown"].ToString()));

                        ddlA_Dist.Enabled = false;
                        ddlA_Depo.Enabled = false;

                        lblA_Dist.Text = "Sending District";
                        lblA_Depo.Text = "Sending Depot";

                        ddlCMR_Rice_Miller.Visible = false;
                        lblCMR_Rice_Miller.Visible = false;
                    }
                    else
                    {
                        lblA_Dist.Visible = false;
                        lblA_Depo.Visible = false;

                        ddlA_Dist.Visible = false;
                        ddlA_Depo.Visible = false;

                        ddlCMR_Rice_Miller.Visible = false;
                        lblCMR_Rice_Miller.Visible = false;
                    }

                    FillGodown();
                    fillStack();
                    ddlGodownNo.Enabled = false;
                    ddlStackNo.Enabled = false;
                    pnlFCI_OTDepot.Visible = true;

                }

                ddlGodownNo.Enabled = false;
                ddlStackNo.Enabled = false;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["EditStack"] = ds.Tables[1];
                    gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                    gdEditStackingDetails.DataBind();
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record, Can not be edited !'); </script> ");
                Response.Redirect("Edit_WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record, Can not be edited!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FillDefaultsforUpdate has occurred , try again!'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }

    protected void FillFCIOTDeoptData()
    {
        try
        {
            string Dist_id = Session["WLC_Dep_Dist_Id"].ToString();
            Dist_id = Dist_id.Substring(2, 2);
            string query = "";
            if (Session["WLCDepSource"].ToString() == "04")
            {
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,No_of_Bags,s_name,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";

            }
            else if (Session["WLCDepSource"].ToString() == "03")
            {
                if (ddlCommodity.SelectedValue.ToString() == "25" || ddlCommodity.SelectedValue.ToString() == "29" || ddlCommodity.SelectedValue.ToString() == "96" || ddlCommodity.SelectedValue.ToString() == "97" || ddlCommodity_Non.SelectedValue.ToString() == "25" || ddlCommodity_Non.SelectedValue.ToString() == "29" || ddlCommodity_Non.SelectedValue.ToString() == "96" || ddlCommodity_Non.SelectedValue.ToString() == "97")
                {
                    //old online  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,0 as Recd_Qty,Recd_Qty as No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,convert(varchar(10),Rcpt.arrival_date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Dp.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    //offline  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                }
                else
                {
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,convert(varchar(10),Rcpt.arrival_date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Dp.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,Recieved_Bags as No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,convert(varchar(10),Rcpt.arrival_date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Dp.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";

                }
                }
            else if (Session["WLCDepSource"].ToString() == "02")
            {
                if (ddlCommodity.SelectedValue.ToString() == "25" || ddlCommodity.SelectedValue.ToString() == "29" || ddlCommodity.SelectedValue.ToString() == "96" || ddlCommodity.SelectedValue.ToString() == "97" || ddlCommodity_Non.SelectedValue.ToString() == "25" || ddlCommodity_Non.SelectedValue.ToString() == "29" || ddlCommodity_Non.SelectedValue.ToString() == "96" || ddlCommodity_Non.SelectedValue.ToString() == "97")
                {
                    // query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.A_Dist=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.A_Depo=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,0 as Recd_Qty, Recd_Qty as No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                }
                else
                {
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,Recieved_Bags as No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,Recieved_Bags as No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + Session["ChallanNo"].ToString() + "'";
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,Recieved_Bags as No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join MPSCSC.dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + Session["ChallanNo"].ToString() + "'";
                    // change depot id to branch
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,Recieved_Bags as No_of_Bags,A_Depo,A_Dist,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Transporter,Ds.District_Name,Dp.DepotName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join tbl_MetaData_DISTRICT as Ds on '23'+Rcpt.Dist_Id=Ds.District_Id join dbo.tbl_MetaData_DEPOT as Dp on  Rcpt.Depot_ID=Dp.DepotID where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Branch= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + Session["ChallanNo"].ToString() + "'";

                }
            }
            else if (Session["WLCDepSource"].ToString() == "05")
            {
                string challan_No = "";
                if (Session["ChallanNo"].ToString() != null)
                {
                    challan_No = Session["ChallanNo"].ToString();
                }
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details_2019] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + challan_No + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details_2019] as Rcpt with(Nolock) left join MPSCSC.dbo.RO_of_FCI as Ro with(Nolock) on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + challan_No + "'";

            }
            else if (Session["WLCDepSource"].ToString() == "15")
            {
                //old online  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,Recd_Qty,No_of_Bags,convert(varchar(10),arrival_date,103) as 'arrivaldate',Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                //new one without changes in bags   query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as  'No_of_Bags',s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                //Online
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recd_Qty as  'No_of_Bags',s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recd_Qty as  'No_of_Bags',s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.challan_no='" + Session["ChallanNo"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                //offline 
                //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter,Ds.District as 'District_Name',Dp.DepoName as 'DepotName' FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No join MPSCSC.dbo.DepoCode as Ds on Rcpt.A_Dist=Ds.District_Code join MPSCSC.dbo.DepoCode as Dp on  Rcpt.A_Depo=Dp.DepoCode where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
            }
            else if (Session["WLC_Dep_Dist_Id"].ToString() != null)
            {
                if (ddlCommodity.SelectedValue.ToString() == "25" || ddlCommodity.SelectedValue.ToString() == "29" || ddlCommodity.SelectedValue.ToString() == "96" || ddlCommodity.SelectedValue.ToString() == "97" || ddlCommodity_Non.SelectedValue.ToString() == "25" || ddlCommodity_Non.SelectedValue.ToString() == "29" || ddlCommodity_Non.SelectedValue.ToString() == "96" || ddlCommodity_Non.SelectedValue.ToString() == "97")
                {
                    // query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',0 as Recd_Qty,Recd_Qty as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    //  query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" +'";
                }
                else
                {
                    string challan_No = "";
                    if (Session["ChallanNo"].ToString() != null)
                    {
                        challan_No = Session["ChallanNo"].ToString();
                    }
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    //query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Depot_ID= '" + Session["WLC_Dep_Depot_ID"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "'";
                    query = "SELECT distinct Vehile_no,challan_no,Rcpt.Commodity,Category,Godown,convert(varchar(10),arrival_date,103) as 'arrivaldate',Recd_Qty,Recieved_Bags as No_of_Bags,s_name,Rcpt.RO_No,Convert(varchar(10),Ro_Date,103) as 'Ro_Date',A_Depo,A_Dist,Transporter FROM MPSCSC.dbo.[tbl_Receipt_Details] as Rcpt left join MPSCSC.dbo.RO_of_FCI as Ro on Ro.RO_No=Rcpt.RO_No where Rcpt.Dist_Id ='" + Dist_id + "' and Rcpt.Branch= '" + Session["BranchId"].ToString() + "' and Rcpt.Receipt_id= '" + Session["WLC_Dep_Receipt_id"].ToString() + "' and Rcpt.S_of_Arrival = '" + Session["WLCDepSource"].ToString() + "' and Rcpt.challan_no='" + challan_No + "'";


                }
            } 
            

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count >= 1)
            {
                txtTCNo.Text = ds.Tables[0].Rows[0]["challan_no"].ToString();
                txtTCNo.Enabled = false;
                txtTruckNo.Text = ds.Tables[0].Rows[0]["Vehile_no"].ToString();
                txtTruckNo.Enabled = false;
                ddlCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity"].ToString();
                ddlCommodity.Enabled = false;
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["Category"].ToString();
                txtDepositDate.Text = ds.Tables[0].Rows[0]["arrivaldate"].ToString();
                txtQtyDeposit.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                txtQtyDeposit.Enabled = false;
                hfBags.Value = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
                txtBags.Text = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
                txtBags.Enabled = false;
                ddlGodownNo.SelectedValue = ds.Tables[0].Rows[0]["Godown"].ToString();
                txtBagsAcceptable.Text = ds.Tables[0].Rows[0]["No_of_Bags"].ToString();
                txtQtyAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();

                ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter"].ToString();
                ddlTransporter.Enabled = false;

                if (Session["WLCDepSource"].ToString() == "04" || Session["WLCDepSource"].ToString() == "05")
                {
                    ddlCMR_Rice_Miller.SelectedValue = ds.Tables[0].Rows[0]["s_name"].ToString();
                    ddlCMR_Rice_Miller.Enabled = false;

                    lblA_Dist.Visible = false;
                    lblA_Depo.Visible = false;
                    ddlA_Dist.Visible = false;
                    ddlA_Depo.Visible = false;
                }
                else if (Session["WLCDepSource"].ToString() == "03")
                {
                    hfRoNo.Value = ds.Tables[0].Rows[0]["RO_No"].ToString();
                    hfRODate.Value = ds.Tables[0].Rows[0]["Ro_Date"].ToString();

                    ddlA_Dist.Items.Add(new ListItem(ds.Tables[0].Rows[0]["District_Name"].ToString(), ds.Tables[0].Rows[0]["A_Dist"].ToString()));
                    ddlA_Depo.Items.Add(new ListItem(ds.Tables[0].Rows[0]["DepotName"].ToString(), ds.Tables[0].Rows[0]["A_Depo"].ToString()));
                    ddlA_Dist.Enabled = false;
                    ddlA_Depo.Enabled = false;
                    lblA_Dist.Text = "FCI AM Office";
                    lblA_Depo.Text = "Dispatch Depot";
                    ddlCMR_Rice_Miller.Visible = false;
                    lblCMR_Rice_Miller.Visible = false;
                }
                else if (Session["WLCDepSource"].ToString() == "02")
                {
                    ddlA_Dist.Items.Add(new ListItem(ds.Tables[0].Rows[0]["District_Name"].ToString(), "23" + ds.Tables[0].Rows[0]["A_Dist"].ToString()));
                    ddlA_Depo.Items.Add(new ListItem(ds.Tables[0].Rows[0]["DepotName"].ToString(), ds.Tables[0].Rows[0]["A_Depo"].ToString()));
                    ddlA_Dist.Enabled = false;
                    ddlA_Depo.Enabled = false;
                    lblA_Dist.Text = "Sending District";
                    lblA_Depo.Text = "Sending Depot";
                    ddlCMR_Rice_Miller.Visible = false;
                    lblCMR_Rice_Miller.Visible = false;
                }
                else
                {
                    lblA_Dist.Visible = false;
                    lblA_Depo.Visible = false;
                    ddlA_Dist.Visible = false;
                    ddlA_Depo.Visible = false;
                    ddlCMR_Rice_Miller.Visible = false;
                    lblCMR_Rice_Miller.Visible = false;

                }
                FillGodown();
                fillStack();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record'); </script> ");
                Response.Redirect("WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FillFCIOTDeoptData has occured, try again'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }

    protected void FillRailHeadData()
    {
        try
        {
            string query = "";
            if (Session["WLCDepSource"].ToString() == "07")//From Rail Head
            {
                query = "SELECT distinct Truck_No,TC_Number,Commodity,Recd_Qty,Recd_Bags,Transporter_ID,Scheme  FROM MPSCSC.dbo.[RR_receipt_Depot] as Rcpt where  Rcpt.DepotID= '" + Session["WLC_Depot_ID"].ToString() + "' and Rcpt.TC_Number= '" + Session["WLC_TC_Number"].ToString() + "' ";
                  //query = "SELECT distinct Truck_No,TC_Number,Commodity,Recd_Qty,Recd_Bags,Transporter_ID,Scheme  FROM MPSCSC.dbo.[RR_receipt_Depot] as Rcpt where Rcpt.district_code ='" + Session["WLC_Distt_ID"].ToString() + "' and Rcpt.DepotID= '" + Session["WLC_Depot_ID"].ToString() + "' and Rcpt.TC_Number= '" + Session["WLC_TC_Number"].ToString() + "' ";
            }
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 1)
            {
                txtTCNo.Text = ds.Tables[0].Rows[0]["TC_Number"].ToString();
                txtTCNo.Enabled = false;
                txtTruckNo.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();
                txtTruckNo.Enabled = false;
                ddlCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity"].ToString();
                ddlCommodity.Enabled = false;

                txtQtyDeposit.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();
                txtQtyDeposit.Enabled = false;
                hfBags.Value = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtBags.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtBags.Enabled = false;

                txtBagsAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Bags"].ToString();
                txtQtyAcceptable.Text = ds.Tables[0].Rows[0]["Recd_Qty"].ToString();

                ddlTransporter.SelectedValue = ds.Tables[0].Rows[0]["Transporter_ID"].ToString();
                ddlTransporter.Enabled = false;
                ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["Scheme"].ToString();

                lblA_Dist.Visible = false;
                lblA_Depo.Visible = false;
                ddlA_Dist.Visible = false;
                ddlA_Depo.Visible = false;

                ddlCMR_Rice_Miller.Visible = false;
                lblCMR_Rice_Miller.Visible = false;
                FillGodown();
                fillStack();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record'); </script> ");
                Response.Redirect("WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error FillRailHeadData has occured, try again'); </script> ");
        }
    }
  
    protected void gdstackingdetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = gdstackingdetails.Rows.Count;
            if (count > 0)
            {
                btnsave.Enabled = true;
            }
            else
            {
                btnsave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_PreRender has occured, try again'); </script> ");
        }
    }

    protected void gdstackingdetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdStackingDetails_RowCreated has occured, try again'); </script> ");
        }
    }

    protected void gdstackingdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdstackingdetails.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gdstackingdetails.DataSource = (DataTable)Session["dt1"];
            gdstackingdetails.DataBind();
            chksum();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }

    protected void ddlStackNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStackCurrentCapacity.Text = "0";
        txtStackMaxCap.Text = "0";
        txtStackAvailable.Text = "0";
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {
               // String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b) AS 'Current_Capacity' from tbl_MetaData_STACK  where tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'";
                String query = "select tbl_MetaData_STACK.Stack_capacity,(select (isnull(a.wet,0) - isnull(b.wet2,0)-isnull(l.loss,0)+isnull(g.gain,0)) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details where Stack_ID = tbl_MetaData_STACK.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') b,(select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as l,(select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = tbl_MetaData_STACK.Stack_ID AND GP.Status !='CANCEL') as g) AS 'Current_Capacity' from tbl_MetaData_STACK  where BranchId='" + Session["BranchId"].ToString() + "' and tbl_MetaData_STACK.Stack_ID = '" + ddlStackNo.SelectedValue.ToString() + "'"; 
              //  string query = "select convert(decimal(18,2),TMS.Stack_capacity) as Stack_capacity,convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS join tbl_MetaData_GODOWN as TMG on TMS.Godown_ID = TMG.Godown_ID JOIN tbl_MetaData_STORAGE_COMMODITY AS TMSC on TMS.Commodity_Id = TMSC.Commodity_Id where TMS.DepotId = '" + Session["WLC_Depot_ID"].ToString() + "' and TMS.Stack_Killed = 'N' and TMS.Stack_ID='" + ddlStackNo.SelectedValue.ToString() + "' order by TMS.Stack_ID";
               // string query = "select convert(decimal(18,2),TMS.Stack_capacity) as Stack_capacity,convert(decimal(18,2),(select (isnull(a.wet,0) - isnull(b.wet2,0)- (select isnull(SUM(Loss),0) as loss from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')+ (select isnull(SUM(Gain),0) as gain from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL')  ) as Current_Capacity from (select SUM(Weight) as wet from tbl_storage_Stacking_Details join tbl_Storage_Receipt_Details on tbl_storage_Stacking_Details.StorageReceipt_Id = tbl_Storage_Receipt_Details.StorageReceipt_Id where Stack_ID = TMS.Stack_ID) a,(select SUM(Bags_Weight) as wet2 from tbl_Delivery_Stacking_Details_GatePass JOIN tbl_Storage_GatePass_Enrty GP ON tbl_Delivery_Stacking_Details_GatePass.GatePass_No = GP.GatePass_No where tbl_Delivery_Stacking_Details_GatePass.Stack_ID = TMS.Stack_ID AND GP.Status !='CANCEL') b)) AS 'Current_Capacity' from tbl_MetaData_STACK as TMS join tbl_MetaData_GODOWN as TMG on TMS.Godown_ID = TMG.Godown_ID JOIN tbl_MetaData_STORAGE_COMMODITY AS TMSC on TMS.Commodity_Id = TMSC.Commodity_Id where TMS.DepotId = '" + Session["WLC_Depot_ID"].ToString() + "' and TMS.Stack_Killed = 'N' and TMS.Stack_ID='" + ddlStackNo.SelectedValue.ToString() + "' order by TMS.Stack_ID";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double Stackcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Stack_capacity"].ToString());
                    double cureentcap = Convert.ToDouble(ds.Tables[0].Rows[0]["Current_Capacity"].ToString());
                    txtStackCurrentCapacity.Text = cureentcap.ToString();
                    txtStackMaxCap.Text = Stackcap.ToString();
                    txtStackAvailable.Text = String.Format("{0:0.00000}", (Convert.ToDouble(txtStackMaxCap.Text) - Convert.ToDouble(txtStackCurrentCapacity.Text)));  
                }
               
            }
            else
            {
                //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "No stack under the selected Commodity ,Category and Godown Number" + "'); </script> ");  
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in ddlStackNo_SelectedIndexChanged has occured, try again'); </script> ");
        }
    }

    protected void ddlStackNo_PreRender(object sender, EventArgs e)
    {
        ddlStackNo_SelectedIndexChanged(sender, e);
    }

    protected void ADD_EditStock()
    {
        bool checkEditstatus = false;
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {

                btnUpdate.Enabled = true;
                if (Session["EditStack"] == null)
                {
                    EditStack = CreateTableEditStack();

                    Session["EditStack"] = EditStack;

                }
                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["EditStack"]).NewRow();
                ((DataTable)Session["EditStack"]).AcceptChanges();
                dr["Godownid"] = ddlGodownNo.SelectedValue;
                dr["Stackid"] = ddlStackNo.SelectedValue;
                dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
                dr["StackName"] = ddlStackNo.SelectedItem.Text;
                dr["Bags"] = txtStackBags.Text.Trim();
                dr["Weight"] = txtStackWt.Text.Trim();
                if (gdEditStackingDetails.Rows.Count > 0)
                {
                    int i;
                    // checking whether or not the stack is already added to the grid view
                    for (i = 0; i <= gdEditStackingDetails.Rows.Count - 1; i++)
                    {
                        string stackid = gdEditStackingDetails.Rows[i].Cells[2].Text.ToString();
                        string selectstackid = ddlStackNo.SelectedValue.ToString();
                        if (stackid == selectstackid)
                        {
                            checkEditstatus = true;
                        }
                    }
                    if (checkEditstatus == false)
                    {
                        ((DataTable)Session["EditStack"]).Rows.Add(dr);
                        ((DataTable)Session["EditStack"]).AcceptChanges();
                        gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                        gdEditStackingDetails.DataBind();
                        txtStackBags.Text = null;
                        txtStackWt.Text = null;
                        chksumEdit();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                    }
                }
                else
                {
                    ((DataTable)Session["EditStack"]).Rows.Add(dr);
                    ((DataTable)Session["EditStack"]).AcceptChanges();
                    gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                    gdEditStackingDetails.DataBind();
                    txtStackBags.Text = null;
                    txtStackWt.Text = null;
                    chksumEdit();
                }
                btnUpdate.Enabled = true;
                gdEditStackingDetails.Enabled = true;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in Add_EDITStock has occured, try again'); </script> ");
        }
    }

    protected void btnAddStack_Click(object sender, EventArgs e)
    {
        bool checkstatus = false;
        try
        {
            int GridCount = 0;
            GridCount = gdstackingdetails.Rows.Count;
            //if (GridCount<1)
            //{
                if (txtStackBags.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No of Bags to be added in Stack is required!'); </script> ");
                    return;
                }
                else if (txtStackWt.Text == "")
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Bags Weight to be added in Stack is required!'); </script> ");
                    return;
                }
                else if ((txtStackAvailable.Text != "") && (Convert.ToDecimal(txtStackAvailable.Text) < Convert.ToDecimal(txtStackWt.Text)))
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Insuffient StackCapacity in the Selected Stack!'); </script> ");
                    return;
                }
                else
                {
                    if (Session["WLC_StorageReceipt_Id"] != null)
                    {
                        if (Session["Mode"] != null)
                        {
                            if (Session["Mode"].ToString() == "Edit")
                            {
                                ADD_EditStock();
                            }
                            else if (Session["Mode"].ToString() == "NON-Edit")
                            {
                                //Edit Stack for NON MPSCSC
                                ADD_EditStockNonMPSCSC();
                            }
                        }
                    }
                    else
                    {
                        if (ddlStackNo.Items.Count > 0)
                        {

                            btnsave.Enabled = true;
                            if (Session["dt1"] == null)
                            {
                                Dt1 = CreateTable();
                                Session["dt1"] = Dt1;
                            }
                            // adding rows to the datatable
                            DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            dr["Godownid"] = ddlGodownNo.SelectedValue;
                            dr["Stackid"] = ddlStackNo.SelectedValue;
                            dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
                            dr["StackName"] = ddlStackNo.SelectedItem.Text;
                            dr["Bags"] = txtStackBags.Text.Trim();
                            dr["Weight"] = txtStackWt.Text.Trim();
                            if (gdstackingdetails.Rows.Count > 0)
                            {
                                int i;

                                // checking whether or not the stack is already added to the grid view
                                for (i = 0; i <= gdstackingdetails.Rows.Count - 1; i++)
                                {
                                    string stackid = gdstackingdetails.Rows[i].Cells[2].Text.ToString();
                                    string selectstackid = ddlStackNo.SelectedValue.ToString();
                                    if (stackid == selectstackid)
                                    {
                                        checkstatus = true;
                                    }
                                }
                                if (checkstatus == false)
                                {
                                    ((DataTable)Session["dt1"]).Rows.Add(dr);
                                    ((DataTable)Session["dt1"]).AcceptChanges();
                                    gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                                    gdstackingdetails.DataBind();
                                    txtStackBags.Text = null;
                                    txtStackWt.Text = null;
                                    chksum();
                                }
                                else
                                {
                                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                                }
                            }
                            else
                            {
                                ((DataTable)Session["dt1"]).Rows.Add(dr);
                                ((DataTable)Session["dt1"]).AcceptChanges();
                                gdstackingdetails.DataSource = (DataTable)Session["dt1"];
                                gdstackingdetails.DataBind();
                                txtStackBags.Text = null;
                                txtStackWt.Text = null;
                                chksum();
                            }
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
                        }
                    }
                }
            //}
            //else
            //{
            //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('You can generate only stack wise WHR'); </script> ");
            //}
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in btnAddStack_Click has occured, try again'); </script> ");
        }
    }
    //protected void chksum()
    //{
    //    try
    //    {
    //        int stackbag = 0;
    //        decimal stackwts = 0;
    //        if (gdstackingdetails.Rows.Count > 0)
    //        {
    //            for (int s = 0; s < gdstackingdetails.Rows.Count; s++)
    //            {
    //                stackbag = stackbag + int.Parse(gdstackingdetails.Rows[s].Cells[5].Text.ToString());
    //                stackwts = stackwts + decimal.Parse(gdstackingdetails.Rows[s].Cells[6].Text.ToString());
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in Chksum has occured, try again'); </script> ");
    //    }
    //}
    protected void chksum()
    {
        try
        {
            int stackbag = 0;
            decimal stackwts = 0;
            if (gdstackingdetails.Rows.Count > 0)
            {
                for (int s = 0; s < gdstackingdetails.Rows.Count; s++)
                {
                    stackbag = stackbag + int.Parse(gdstackingdetails.Rows[s].Cells[5].Text.ToString());
                    stackwts = stackwts + decimal.Parse(gdstackingdetails.Rows[s].Cells[6].Text.ToString());

                }
                if ((Session["WLCDepSource"].ToString() == "NON-MPSCSC") || (pnlNONMPSCSC.Visible == true))
                {
                    //txtBags_Non.Text = stackbag.ToString();
                    //txtQty_Non.Text = stackwts.ToString();

                    //txtBagsAcceptable_Non.Text = stackbag.ToString();
                    //txtQtyAcceptable_Non.Text = stackwts.ToString();

                    txtBagsAcceptable_Non.Text = txtBags_Non.Text.ToString();
                    txtQtyAcceptable_Non.Text = txtQty_Non.Text.ToString();

                    txtBagsAcceptable_Non.Enabled = false;
                    txtQtyAcceptable_Non.Enabled = false;
                   
                }
            }
            else
            {
                if (Session["WLCDepSource"].ToString() == "NON-MPSCSC")
                {
                    txtBags_Non.Text = "0";
                    txtQty_Non.Text = "0";
                    txtBagsAcceptable_Non.Text = "0";
                    txtQtyAcceptable_Non.Text = "0";
                    txtBagsAcceptable_Non.Enabled = false;
                    txtQtyAcceptable_Non.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in Chksum has occured, try again'); </script> ");
        }
    }

    protected void chksumEdit()
    {
        try
        {
            int stackbag = 0;
            decimal stackwts = 0;
            if (gdEditStackingDetails.Rows.Count > 0)
            {
                for (int s = 0; s < gdEditStackingDetails.Rows.Count; s++)
                {
                    stackbag = stackbag + int.Parse(gdEditStackingDetails.Rows[s].Cells[5].Text.ToString());
                    stackwts = stackwts + decimal.Parse(gdEditStackingDetails.Rows[s].Cells[6].Text.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in chksumEdit has occured, try again'); </script> ");
        }
    }

    private DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dt.Columns.Add(Godownid);//Column is added to the DataTable
        dt.Columns.Add(Stackid);//Column is added to the DataTable
        dt.Columns.Add(GodownName);//Column is added to the DataTable
        dt.Columns.Add(StackName);//Column is added to the DataTable
        dt.Columns.Add(Bags);//Column is added to the DataTable
        dt.Columns.Add(Weight);//Column is added to the DataTable

        dt.AcceptChanges();
        return dt;
    }

    private DataTable CreateTableEditStack()
    {
        DataTable dtEditStack = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dtEditStack.Columns.Add(Godownid);//Column is added to the DataTable
        dtEditStack.Columns.Add(Stackid);//Column is added to the DataTable
        dtEditStack.Columns.Add(GodownName);//Column is added to the DataTable
        dtEditStack.Columns.Add(StackName);//Column is added to the DataTable
        dtEditStack.Columns.Add(Bags);//Column is added to the DataTable
        dtEditStack.Columns.Add(Weight);//Column is added to the DataTable

        dtEditStack.AcceptChanges();
        return dtEditStack;
    }

    private DataTable CreateTableEditStackNonMPSCSC()
    {
        DataTable dtEditStackNonMPSCSC = new DataTable();//DataTable is created
        DataColumn Godownid = new DataColumn("Godownid", Type.GetType("System.String"));
        DataColumn Stackid = new DataColumn("Stackid", Type.GetType("System.String"));
        DataColumn GodownName = new DataColumn("GodownName", Type.GetType("System.String"));
        DataColumn StackName = new DataColumn("StackName", Type.GetType("System.String"));
        DataColumn Bags = new DataColumn("Bags", Type.GetType("System.Int32"));
        DataColumn Weight = new DataColumn("Weight", Type.GetType("System.Decimal"));
        dtEditStackNonMPSCSC.Columns.Add(Godownid);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Stackid);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(GodownName);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(StackName);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Bags);//Column is added to the DataTable
        dtEditStackNonMPSCSC.Columns.Add(Weight);//Column is added to the DataTable

        dtEditStackNonMPSCSC.AcceptChanges();
        return dtEditStackNonMPSCSC;
    }

    protected void ddlGodownNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            fillStack();
            Session["dt1"] =null;
            //ddlStackNo_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error ddlGodownNo_Selected Index has occured, try again'); </script> ");
        }
    }

    protected void fillStack()
    {
        try
        {
            string query = "";
            ddlStackNo.Items.Clear();
            if (Session["Mode"].ToString() == "NON-Edit")//Non MPSCSC Edit
            {
                query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + ddlCommodity_Non.SelectedValue + "' and Category_Id  = '" + ddlCategory.SelectedValue + "' and Stack_Killed = 'N' ) order by Stack_Name";
            }
            else
            {
                if (Session["WLCDepSource"].ToString() == "NON-MPSCSC")
                {
                    query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + ddlCommodity_Non.SelectedValue + "' and Category_Id  = '" + ddlCategory.SelectedValue + "' and Stack_Killed = 'N' ) order by Stack_Name";
                }
                else
                {
                    query = "SELECT Stack_ID, Stack_Name FROM tbl_MetaData_STACK WHERE (Godown_ID = '" + ddlGodownNo.SelectedValue + "' and Commodity_Id = '" + ddlCommodity.SelectedValue + "' and Category_Id  = '" + ddlCategory.SelectedValue + "' and Stack_Killed = 'N' ) order by Stack_Name";
                }
            }
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStackNo.DataSource = ds.Tables[0];
                ddlStackNo.DataTextField = "Stack_Name";
                ddlStackNo.DataValueField = "Stack_ID";
                ddlStackNo.DataBind();
                ddlStackNo.Items.Insert(0, " --select--");
            }
            else
            {
                ddlStackNo.Items.Clear();
                ddlStackNo.DataSource = null;
                ddlStackNo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillStack has occured, try again '); </script> ");
        }
    }
    //Change 21-04-2014
    protected void saveRailHeadData()
    {
        if ((Session["Depot_DistID"].ToString() != "") && (Session["Depot_DepotID"].ToString() != ""))
        {
            try
            {
                if (Page.IsValid)
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    int stackbags = 0;

                    decimal stackwt = 0;
                    int l;
                    if (gdstackingdetails.Rows.Count > 0)
                    {
                        for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                        {
                            stackbags = stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                            stackwt = stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                        }
                        if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                        {
                            Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");
                        }

                        else if (Convert.ToInt32(txtBagsAcceptable.Text.ToString()) != Convert.ToInt32(stackbags.ToString()) || (decimal.Parse(txtQtyAcceptable.Text) != decimal.Parse(stackwt.ToString())))
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !'); </script> ");
                        }
                        else if (CheckValid == "0")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deposit Date should be greater that last Deposited Date and less then/equal to Current Date !'); </script> ");
                        }
                        else
                        {
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }
                            //to get arrival stock id........
                            string QueryMax = "select isnull(Max(ArrivalStock_Id),0) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchId"].ToString() + "' ";
                            SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                            string str3 = cmd2.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str3) != 0)
                            {
                                ArrivalStockid = Convert.ToString(Convert.ToInt64(str3) + 1);
                                if (ArrivalStockid != String.Empty || ArrivalStockid != "")
                                {
                                Found:
                                    string Queryc = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStockid.ToString() + "'";
                                    cmd2 = new SqlCommand(Queryc, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                    string maxcount = cmd2.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(maxcount) > 0)
                                    {
                                        ArrivalStockid = Convert.ToString(Convert.ToInt64(ArrivalStockid) + 1);
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                string Depotid = Session["Depot_DepotID"].ToString();
                                string BranchId = Session["BranchId"].ToString();

                                ArrivalStockid = BranchId + System.DateTime.Now.Year.ToString().Substring(2, 2) + "00001";
                            }

                            SqlCommand cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert", Con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                            cmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();
                            //cmd.Parameters.Add("@Gate_PassNo", SqlDbType.NVarChar, 20);
                            //cmd.Parameters["@Gate_PassNo"].Value = "0";

                            cmd.Parameters.Add("@DepositDate", SqlDbType.DateTime);
                            cmd.Parameters["@DepositDate"].Value = getDate_MDY(Convert.ToString(txtDepositDate.Text.Trim()));
                            cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Commodity_Id"].Value = ddlCommodity.SelectedValue;
                            cmd.Parameters.Add("@Mode_of_weighment", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Mode_of_weighment"].Value = ddlWeigmentMode.SelectedValue;

                            cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);
                            cmd.Parameters.AddWithValue("@AcceptanceNo", "");
                            cmd.Parameters.AddWithValue("@PurchasCentre", "");
                            cmd.Parameters.AddWithValue("@IssueID", "");
                            cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                            if (txtBags.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_No_of_Bags"].Value = stackbags;
                            }
                            else
                            {

                                cmd.Parameters["@Qty_No_of_Bags"].Value = Convert.ToInt64(txtBags.Text.ToString());
                            }

                            cmd.Parameters.Add("@Qty_Wt", SqlDbType.Decimal);
                            if (txtQtyDeposit.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Qty_Wt"].Value = Convert.ToDecimal(txtQtyDeposit.Text.Trim().ToString());
                            }
                            cmd.Parameters.Add("@Category_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Category_Id"].Value = ddlCategory.SelectedValue;
                            cmd.Parameters.Add("@Depositor_Name", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Depositor_Name"].Value = "MPSCSC";
                            cmd.Parameters.Add("@DepositorType", SqlDbType.VarChar, 20);
                            cmd.Parameters["@DepositorType"].Value = "Institution";
                            //change ddlcropyear 28-04
                            cmd.Parameters.Add("@Crop_Year", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@Crop_Year"].Value = ddlcropyear.SelectedValue;

                            cmd.Parameters.Add("@Sender_District", SqlDbType.NVarChar, 20);
                            cmd.Parameters.Add("@Sender_Godown", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Sender_District"].Value = "0";
                            cmd.Parameters["@Sender_Godown"].Value = "0";

                            cmd.Parameters.Add("@Challan_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Challan_No"].Value = txtTCNo.Text.ToString();
                            cmd.Parameters.Add("@Truck_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Truck_No"].Value = txtTruckNo.Text.ToString();
                            cmd.Parameters.Add("@Source_of_Arrival", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Source_of_Arrival"].Value = Session["WLCDepSource"].ToString();

                            cmd.Parameters.Add("@ArrivalSource_ID", SqlDbType.NVarChar, 2);
                            cmd.Parameters["@ArrivalSource_ID"].Value = Session["WLCDepSource"].ToString();

                            cmd.Parameters.Add("@Quality_Moisture", SqlDbType.Decimal);
                            if (txtMoisture.Text.ToString().Trim() == "")
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = Convert.ToDecimal(txtMoisture.Text.ToString());
                            }

                            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 250);
                            cmd.Parameters["@Remarks"].Value = txtRemarks.Text.ToString();
                            cmd.Parameters.Add("@Scheme_ID", SqlDbType.NVarChar, 10);
                            cmd.Parameters["@Scheme_ID"].Value = ddlScheme.SelectedValue;

                            cmd.Parameters.Add("@Acpt_FCIRO_No", SqlDbType.NVarChar, 50);
                            cmd.Parameters.Add("@Acpt_FCIRO_Date", SqlDbType.DateTime);
                            cmd.Parameters["@Acpt_FCIRO_No"].Value = DBNull.Value;
                            cmd.Parameters["@Acpt_FCIRO_Date"].Value = DBNull.Value;

                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@CreatedBy"].Value = Session["Depot_DepotID"].ToString();

                            cmd.Parameters.Add("@Client_IP", SqlDbType.VarChar, 20);
                            cmd.Parameters["@Client_IP"].Value = ClientIP.ToString();
                            cmd.Parameters.Add("@Transporter_id", SqlDbType.VarChar, 20);
                            if (ddlTransporter.Items.Count == 0)
                            {
                                cmd.Parameters["@Transporter_id"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Transporter_id"].Value = ddlTransporter.SelectedValue;
                            }

                            cmd.Parameters.Add("@Miller", SqlDbType.NVarChar, 100);
                            cmd.Parameters["@Miller"].Value = DBNull.Value;
                            cmd.Parameters.Add("@WCMNo_Sending", SqlDbType.NVarChar, 50);
                            if (txtWCMNo.Text.Trim() == "")
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = txtWCMNo.Text;
                            }

                            cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                            //cmd.Parameters.Add("@GatePassId", SqlDbType.NVarChar, 20);
                            //cmd.Parameters["@GatePassId"].Direction = ParameterDirection.Output;
                           // cmd.Parameters.Add("@ArrivalStockId", SqlDbType.NVarChar, 20);
                            //cmd.Parameters["@ArrivalStockId"].Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);

                            cmd.Parameters.Add("@Acceptable_Bags", SqlDbType.Int);
                            if (txtBagsAcceptable.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Bags"].Value = stackbags;
                            }
                            else
                            {

                                cmd.Parameters["@Acceptable_Bags"].Value = Convert.ToInt64(txtBagsAcceptable.Text.ToString());
                            }

                            cmd.Parameters.Add("@Acceptable_Wt", SqlDbType.Decimal);
                            if (txtQtyAcceptable.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = Convert.ToDecimal(txtQtyAcceptable.Text.Trim().ToString());
                            }

                            cmd.ExecuteNonQuery();
                            receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                           // gatePassid = cmd.Parameters["@GatePassId"].Value.ToString();

                            ArrivalStockid = cmd.Parameters["@ArrivalStockId"].Value.ToString();
                            txtArrivalSrcId.Text = ArrivalStockid;

                            cmd.Dispose();
                            Con.Close();

                            // adding the stack details to the database
                            string G = "";
                            string S = "";
                            int j;
                            for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                            {

                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                SqlCommand sqlCmd = new SqlCommand();
                                sqlCmd.CommandText = "MPWLC_sp_stackingdetails_insert";
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 50);
                                sqlCmd.Parameters.Add("@GodownId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@StackId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@SBags", SqlDbType.Int);
                                sqlCmd.Parameters.Add("@SWeight", SqlDbType.Decimal);
                                sqlCmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                sqlCmd.Connection = Con;

                                sqlCmd.Parameters["@receiptid"].Value = receiptid;

                                sqlCmd.Parameters["@GodownId"].Value = gdstackingdetails.Rows[j].Cells[1].Text.ToString();
                                sqlCmd.Parameters["@StackId"].Value = gdstackingdetails.Rows[j].Cells[2].Text.ToString();
                                sqlCmd.Parameters["@SBags"].Value = int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString());
                                sqlCmd.Parameters["@SWeight"].Value = decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString());
                                sqlCmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                                sqlCmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();

                                sqlCmd.ExecuteNonQuery();

                                G = G + gdstackingdetails.Rows[j].Cells[3].Text.ToString() + "/";
                                S = S + gdstackingdetails.Rows[j].Cells[4].Text.ToString() + "/";
                            }

                            G = G.TrimEnd('/');
                            S = S.TrimEnd('/');

                            SqlCommand sqlCmd2 = new SqlCommand();
                            sqlCmd2.CommandText = "update tbl_Storage_GatePass_Enrty set Godown_ID='" + G + "' ,Stack_ID='" + S + "' where GatePass_No='" + gatePassid.ToString() + "'";
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Connection = Con;
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }

                            sqlCmd2.ExecuteReader();
                            Con.Close();
                            sqlCmd2.Dispose();

                            Session["dt1"] = null;
                            gdstackingdetails.DataSource = null;
                            gdstackingdetails.DataBind();

                            Session["RefreshButton"] = "Yes";

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved successfully'); </script> ");

                            // and Rest of the Truck details for processing and same time not to be refreshed...
                            Session["RefreshButton"] = "No";

                            //Initialise other values
                            //string arrid = "Gate_Pass.aspx?src=MC&id=" + txtArrivalSrcId.Text;
                            //StringBuilder sb = new StringBuilder();
                            //sb.Append("<script>");
                            //sb.Append("window.open(");
                            //sb.Append("'" + arrid + "'");
                            //sb.Append(",'MyWindow', 'height=800,width=780');");
                            //sb.Append("</script>");
                            //this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());

                        } ///here transactions ends
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please add Stack information first'); </script> ");
                    }
                }
                else
                {
                    Session["errDesc"] = "Invalid input data";
                    Server.Transfer("../../CustomError.aspx");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in save RailHeadData has occurred'); </script> ");
            }
            finally
            {
                Con.Close();
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void gdEditStackingDetails_PreRender(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = gdEditStackingDetails.Rows.Count;
            if (count > 0)
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEdit StackingDetails has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ViewState["ckEditstat"].ToString() != "Delete")
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEditStackingDetails_RowCreated has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gdEditStackingDetails.Rows.Count < 1)
            {
                ViewState["ckEditstat"] = "Delete";
            }

            ((DataTable)Session["EditStack"]).Rows[i]["Bags"] = "0";
            ((DataTable)Session["EditStack"]).Rows[i]["Weight"] = "0";
            ((DataTable)Session["EditStack"]).AcceptChanges();

            gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
            gdEditStackingDetails.DataBind();
            chksumEdit();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in gdEditStackingDetails_RowDeleting has occured, try again'); </script> ");
        }
    }

    protected void gdEditStackingDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int i = e.NewEditIndex;
            if (gdEditStackingDetails.Rows.Count < 1)
            {
                ViewState["ckEditstat"] = "Edit";
            }

            ddlGodownNo.SelectedValue = ((DataTable)Session["EditStack"]).Rows[i][0].ToString();
            ddlGodownNo_SelectedIndexChanged(sender, e);
            ddlStackNo.SelectedValue = ((DataTable)Session["EditStack"]).Rows[i][1].ToString();
            ddlGodownNo.Enabled = false;
            ddlStackNo.Enabled = false;
            txtStackBags.Text = ((DataTable)Session["EditStack"]).Rows[i][4].ToString();
            txtStackWt.Text = ((DataTable)Session["EditStack"]).Rows[i][5].ToString();

            ((DataTable)Session["EditStack"]).Rows[i].Delete();

            ((DataTable)Session["EditStack"]).AcceptChanges();

            gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
            gdEditStackingDetails.DataBind();
            chksumEdit();
            btnUpdate.Enabled = false;
            gdEditStackingDetails.Enabled = false;
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error gdEditStackingDetails_RowEditing has occured, try again'); </script> ");
        }
    }

    protected void UpdateRecieptData()
    {
        if ((Session["Depot_DistID"].ToString() != "") && (Session["Depot_DepotID"].ToString() != ""))
        {
            this.Validate("SaveValid");
            if (Page.IsValid)
            {
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                int stackbags = 0;
                decimal stackwt = 0;
                int l;
                if (gdEditStackingDetails.Rows.Count > 0)
                {
                    for (l = 0; l < gdEditStackingDetails.Rows.Count; l++)
                    {
                        stackbags = stackbags + int.Parse(gdEditStackingDetails.Rows[l].Cells[5].Text.ToString());
                        stackwt = stackwt + decimal.Parse(gdEditStackingDetails.Rows[l].Cells[6].Text.ToString());
                    }

                    if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                    {
                        Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");
                    }
                    else if ((txtQtyAcceptable.Text != "") && (Convert.ToInt32(txtBagsAcceptable.Text.ToString()) != Convert.ToInt32(stackbags.ToString()) || ((decimal.Parse(txtQtyAcceptable.Text) != decimal.Parse(stackwt.ToString())))))
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !'); </script> ");
                    }
                    else
                    {
                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_Update", Con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);

                        if (txtBags.Text.Trim().ToString() == "")
                        {
                            cmd.Parameters["@Qty_No_of_Bags"].Value = stackbags;
                        }
                        else
                        {
                            cmd.Parameters["@Qty_No_of_Bags"].Value = Convert.ToInt64(txtBags.Text.ToString());
                        }

                        cmd.Parameters.Add("@Qty_Wt", SqlDbType.Decimal);
                        if (txtQtyDeposit.Text.Trim().ToString() == "")
                        {
                            cmd.Parameters["@Qty_Wt"].Value = stackwt;
                        }
                        else
                        {
                            cmd.Parameters["@Qty_Wt"].Value = Convert.ToDecimal(txtQtyDeposit.Text.Trim().ToString());
                        }

                        cmd.Parameters.Add("@Mode_of_weighment", SqlDbType.NVarChar, 20);
                        cmd.Parameters["@Mode_of_weighment"].Value = ddlWeigmentMode.SelectedValue;
                        cmd.Parameters.Add("@Quality_Moisture", SqlDbType.Decimal);
                        cmd.Parameters.AddWithValue("@DepositDate", getDate_MDY(Convert.ToString(txtDepositDate.Text.Trim())));
                        
                        if (txtMoisture.Text.ToString().Trim() == "")
                        {
                            cmd.Parameters["@Quality_Moisture"].Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters["@Quality_Moisture"].Value = Convert.ToDecimal(txtMoisture.Text.ToString());
                        }

                        cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 250);
                        cmd.Parameters["@Remarks"].Value = txtRemarks.Text.ToString();

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50);
                        cmd.Parameters["@UpdatedBy"].Value = Session["Depot_DepotID"].ToString();

                        cmd.Parameters.Add("@Client_IP", SqlDbType.VarChar, 20);
                        cmd.Parameters["@Client_IP"].Value = ClientIP.ToString();


                        cmd.Parameters.Add("@WCMNo_Sending", SqlDbType.NVarChar, 50);
                        cmd.Parameters["@WCMNo_Sending"].Value = txtWCMNo.Text;

                        cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                        cmd.Parameters["@receiptid"].Value = Session["WLC_StorageReceipt_Id"].ToString();

                        cmd.Parameters.Add("@GatePassId", SqlDbType.NVarChar, 20);
                        cmd.Parameters["@GatePassId"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add("@ArrivalStockId", SqlDbType.NVarChar, 20);
                        cmd.Parameters["@ArrivalStockId"].Direction = ParameterDirection.Output;


                        cmd.Parameters.Add("@Acceptable_Bags", SqlDbType.Int);
                        if (txtBagsAcceptable.Text.Trim().ToString() == "")
                        {
                            cmd.Parameters["@Acceptable_Bags"].Value = stackbags;
                        }
                        else
                        {

                            cmd.Parameters["@Acceptable_Bags"].Value = Convert.ToInt64(txtBagsAcceptable.Text.ToString());
                        }

                        cmd.Parameters.Add("@Acceptable_Wt", SqlDbType.Decimal);
                        if (txtQtyAcceptable.Text.Trim().ToString() == "")
                        {
                            cmd.Parameters["@Acceptable_Wt"].Value = stackwt;
                        }
                        else
                        {
                            cmd.Parameters["@Acceptable_Wt"].Value = Convert.ToDecimal(txtQtyAcceptable.Text.Trim().ToString());
                        }
                        //20-05-2014 UpdateCropYear-------------------------------------
                        cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue);
                        cmd.ExecuteNonQuery();
                        gatePassid = cmd.Parameters["@GatePassId"].Value.ToString();

                        txtArrivalSrcId.Text = cmd.Parameters["@ArrivalStockId"].Value.ToString();
                        cmd.Dispose();
                        Con.Close();

                        // adding the stack details to the database
                        string G = "";
                        string S = "";
                        int j;
                        for (j = 0; j < gdEditStackingDetails.Rows.Count; j++)
                        {

                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }
                            SqlCommand sqlCmd = new SqlCommand();
                            sqlCmd.CommandText = "MPWLC_sp_stackingdetails_Update";
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 50);
                            sqlCmd.Parameters.Add("@GodownId", SqlDbType.NVarChar, 20);
                            sqlCmd.Parameters.Add("@StackId", SqlDbType.NVarChar, 20);
                            sqlCmd.Parameters.Add("@SBags", SqlDbType.Int);
                            sqlCmd.Parameters.Add("@SWeight", SqlDbType.Decimal);
                            sqlCmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50);
                            sqlCmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                            sqlCmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                            sqlCmd.Connection = Con;
                            sqlCmd.Parameters["@receiptid"].Value = Session["WLC_StorageReceipt_Id"].ToString();
                            sqlCmd.Parameters["@GodownId"].Value = gdEditStackingDetails.Rows[j].Cells[1].Text.ToString();
                            sqlCmd.Parameters["@StackId"].Value = gdEditStackingDetails.Rows[j].Cells[2].Text.ToString();
                            sqlCmd.Parameters["@SBags"].Value = int.Parse(gdEditStackingDetails.Rows[j].Cells[5].Text.ToString());
                            sqlCmd.Parameters["@SWeight"].Value = decimal.Parse(gdEditStackingDetails.Rows[j].Cells[6].Text.ToString());
                            sqlCmd.Parameters["@UpdatedBy"].Value = Session["Depot_DepotID"].ToString();
                            sqlCmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                            sqlCmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();

                            sqlCmd.ExecuteNonQuery();



                            G = G + gdEditStackingDetails.Rows[j].Cells[3].Text.ToString() + "/";
                            S = S + gdEditStackingDetails.Rows[j].Cells[4].Text.ToString() + "/";
                        }

                        G = G.TrimEnd('/');
                        S = S.TrimEnd('/');
                        Session["EditStack"] = null;
                        gdEditStackingDetails.DataSource = null;
                        gdEditStackingDetails.DataBind();
                        Session["RefreshButton"] = "Yes";
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved successfully'); </script> ");
                        Session["RefreshButton"] = "No";
                        if (Con.State == ConnectionState.Open)
                        {
                            Con.Close();
                        }
                        Response.Redirect("Edit_WLC_Deposit_From.aspx?PopMsg=" + "Record saved successfully!" + "");

                    } ///here transactions ends
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please add Stack information first'); </script> ");
                }
            }
            else
            {
                Session["errDesc"] = "Invalid input data";
                Server.Transfer("../../CustomError.aspx");
            }
        }
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != "") && (Session["Depot_DepotID"] != ""))
        {
            try
            {
                ddlDepositor.Items.Clear();

                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                DataSet ds1 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.Connection = Con;
                cmd = new SqlCommand("sp_getDepositor_Depo_wise", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 20);
                cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
                cmd.Parameters.Add("@depot_id", SqlDbType.VarChar, 20);
                cmd.Parameters["@depot_id"].Value = Session["BranchId"].ToString();
                int _index = cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                da.SelectCommand = cmd;
                da.Fill(ds1, "temp");
                ddlDepositor.DataSource = ds1;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_ID";
                ddlDepositor.DataBind();
                for (int i = 0; i < ddlDepositor.Items.Count; i++)
                {
                    if (ddlDepositor.Items[i].Text == "MPSCSC")
                        ddlDepositor.Items.RemoveAt(i);
                }

                if (Session["Did"] != null)
                {
                    for (int i = 0; i < ddlDepositor.Items.Count; i++)
                    {
                        if (ddlDepositor.Items[i].Value == Session["Did"].ToString())
                        {
                            ddlDepositor.Items[i].Selected = true;
                        }
                    }

                }

                Con.Close();
                cmd.Dispose();
                Session["DType"] = ddldepositortype.SelectedValue.ToString().Trim();
                ddlDepositor_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in ddldepositortype_SelectedIndexChanged has occurred , try again!'); </script> ");
            }
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["DType"] = ddldepositortype.SelectedValue;
            if (ddlDepositor.Items.Count > 0)
            {
                if (ddlDepositor.SelectedItem.Text.ToUpper() == "FCI")
                {
                    fillDefaultsForNonMPSCSC();
                    ddlArrival_Source.SelectedValue = "03";
                    ddlArrival_Source_SelectedIndexChanged(sender, e);
                    ddlArrival_Source.Enabled = false;

                    ddlScheme_Non.Items.Clear();
                    ddlScheme_Non.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlScheme_Non.Visible = false;
                    lblScheme_Non.Visible = false;
                }
                else if (ddlDepositor.SelectedItem.Text.ToUpper() == "F.C.I.")
                {
                    fillDefaultsForNonMPSCSC();
                    ddlArrival_Source.SelectedValue = "03";
                    ddlArrival_Source_SelectedIndexChanged(sender, e);
                    ddlArrival_Source.Enabled = false;

                    ddlScheme_Non.Items.Clear();
                    ddlScheme_Non.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlScheme_Non.Visible = false;
                    lblScheme_Non.Visible = false;
                }
                else
                {
                    fillDefaultsForNonMPSCSC();
                    ddlArrival_Source.SelectedValue = "06";
                    ddlArrival_Source.Enabled = false;
                    fillDefaultDistDepot();

                    fillCropYear();

                    lblDistrict.Visible = false;
                    ddlDist_Non.Visible = false;
                    lblDepot.Visible = false;
                    lblSSociety.Visible = false;
                    ddlDepo_Non.Visible = false;

                    ddlScheme_Non.Items.Clear();
                    ddlScheme_Non.Items.Add(new ListItem(" Non Scheme", "0"));
                    ddlScheme_Non.Visible = false;
                    lblScheme_Non.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error ddlDepositor_SelectedIndexChanged has occurred , try again!'); </script> ");
        }
    }

    protected void fillDistDepot()
    {
        ddlDepo_Non.Items.Clear();
        ddlDist_Non.DataSource = null;
        ddlDist_Non.DataBind();
        ddlDepo_Non.DataSource = null;
        ddlDepo_Non.DataBind();
        if (ddlArrival_Source.Items.Count > 0)
        {   
            if (ddlArrival_Source.SelectedValue == "02") //Other Depot
            {
                string query = "select '0' as 'District_id',' --select--' as 'District_Name' union select District_id,District_Name FROM MPSCSC.dbo.tbl_MetaData_DISTRICT order by District_Name";
                //string query = "select '0' as 'District_id',' --select--' as 'District_Name' union select District_id,District_Name FROM MPSCSC.dbo.tbl_MetaData_DISTRICT order by District_Name";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDist_Non.DataSource = ds;
                    ddlDist_Non.DataTextField = "District_Name";
                    ddlDist_Non.DataValueField = "District_id";
                    ddlDist_Non.DataBind();
                }

                lblSSociety.Visible = false;
                lblDepot.Visible = true;

                ddlcropyear.Items.Clear();
                ddlcropyear.Items.Add("Crop Year Not Indicated");
                lblDistrict.Text = "District";

            }
            else if (ddlArrival_Source.SelectedValue == "03")//FCI Depot
            {
                string query = "select '0' as 'District_Code',' --select--' as 'District' union select distinct District_Code,District FROM MPSCSC.dbo.DepoCode order by District";
               // string query = "select '0' as 'District_Code',' --select--' as 'District' union select distinct District_Code,District FROM MPSCSC.dbo.DepoCode order by District";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDist_Non.DataSource = ds;
                    ddlDist_Non.DataTextField = "District";
                    ddlDist_Non.DataValueField = "District_Code";
                    ddlDist_Non.DataBind();
                }

                lblSSociety.Visible = false;
                lblDepot.Visible = true;

                ddlcropyear.Items.Clear();
                ddlcropyear.Items.Add("Not Indicated");

                lblDistrict.Text = "Region/District";

                lblDepot.Visible = true;
                ddlDist_Non.Visible = true;
                ddlDepo_Non.Visible = true;
                lblDistrict.Visible = true;
            }
            else
            {
                fillDefaultDistDepot();
                lblSSociety.Visible = false;
                lblDepot.Visible = false;
                ddlDist_Non.Visible = false;
                ddlDepo_Non.Visible = false;
                lblDistrict.Visible = false;
                ddlcropyear.Items.Clear();
                ddlcropyear.Items.Add("Crop Year Not Indicated");
                lblDistrict.Text = "District";
            }
        }
    }

    protected void fillDefaultDistDepot()
    {
        if ((Session["Depot_DistID"] != "") && (Session["Depot_DepotID"] != ""))
        {
            try
            {
                string query = "SELECT 0 as 'District_Id', ' --Select--' as 'District_Name' union select [District_Id], [District_Name]  FROM [tbl_MetaData_DISTRICT] order by District_Name";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDist_Non.DataSource = ds;
                    ddlDist_Non.DataTextField = "District_Name";
                    ddlDist_Non.DataValueField = "District_Id";
                    ddlDist_Non.DataBind();

                }

                //////Depot binding.......................... 
                string query1 = "SELECT  DepotID, DepotName,BranchID FROM tbl_MetaData_DEPOT  where DistrictId='" + Session["Depot_DistID"] + "' order by DepotName";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlDepo_Non.DataSource = ds1;
                    ddlDepo_Non.DataTextField = "DepotName";
                    ddlDepo_Non.DataValueField = "BranchID";
                    ddlDepo_Non.DataBind();
                    ddlDepo_Non.Items.Insert(0, "--Select--");
                }

                if (Convert.ToInt32(ddlDist_Non.Items.Count) > 0)
                {
                    int k = 0;
                    for (k = 0; k < ddlDist_Non.Items.Count; k++)
                    {
                        if (ddlDist_Non.Items[k].Value == Session["Depot_DistID"].ToString())
                        {
                            ddlDist_Non.Items[k].Selected = true;
                        }
                    }
                }
                if (Convert.ToInt32(ddlDepo_Non.Items.Count) > 0)
                {
                    int k = 0;
                    for (k = 0; k < ddlDepo_Non.Items.Count; k++)
                    {
                        if (ddlDepo_Non.Items[k].Value == Session["BranchId"].ToString())
                        {
                            ddlDepo_Non.Items[k].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error fillDefaultDistDepot has occurred , try again!'); </script> ");
            }
        }
    }

    protected void ddlCommodity_Non_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlMiller_Non.Items.Clear();
            FillGodown();
            fillStack();
            if (ddlCommodity_Non.SelectedItem.Text.ToString().Substring(0, 4).ToUpper() == "RICE")
            {
                fillMiller();
                ddlMiller_Non.Visible = true;
                lblCMR_Rice_Miller_Non.Visible = true;
            }
            else
            {
                ddlMiller_Non.Visible = false;
                lblCMR_Rice_Miller_Non.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error ddlCommodity_Non_SelectedIndexChanged has occurred , try again!'); </script> ");
        }
    }

    protected void fillMiller()
    {
        try
        {
            string query = "select '0' as 'Miller_ID',' NA' as 'Miller_Name' Union SELECT [Miller_ID], [Miller_Name] FROM MPSCSC.dbo.[Miller_Master] order by Miller_Name";
           // string query = "select '0' as 'Miller_ID',' NA' as 'Miller_Name' Union SELECT [Miller_ID], [Miller_Name] FROM MPSCSC.dbo.[Miller_Master] order by Miller_Name";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMiller_Non.DataSource = ds;
                ddlMiller_Non.DataTextField = "Miller_Name";
                ddlMiller_Non.DataValueField = "Miller_ID";
                ddlMiller_Non.DataBind();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in fillMiller has occurred , try again!'); </script> ");
        }
    }

    protected void ADD_EditStockNonMPSCSC()
    {
        bool checkEditstatusNonMPCSCS = false;
        try
        {
            if (ddlStackNo.Items.Count > 0)
            {

                btnUpdate.Enabled = true;
                if (Session["EditStack"] == null)
                {
                    EditStack = CreateTableEditStack();

                    Session["EditStack"] = EditStack;

                }
                // adding rows to the datatable
                DataRow dr = ((DataTable)Session["EditStack"]).NewRow();
                ((DataTable)Session["EditStack"]).AcceptChanges();
                dr["Godownid"] = ddlGodownNo.SelectedValue;
                dr["Stackid"] = ddlStackNo.SelectedValue;
                dr["GodownName"] = ddlGodownNo.SelectedItem.Text;
                dr["StackName"] = ddlStackNo.SelectedItem.Text;
                dr["Bags"] = txtStackBags.Text.Trim();
                dr["Weight"] = txtStackWt.Text.Trim();
                if (gdEditStackingDetails.Rows.Count > 0)
                {
                    int i;
                    // checking whether or not the stack is already added to the grid view
                    for (i = 0; i <= gdEditStackingDetails.Rows.Count - 1; i++)
                    {
                        string stackid = gdEditStackingDetails.Rows[i].Cells[2].Text.ToString();
                        string selectstackid = ddlStackNo.SelectedValue.ToString();
                        if (stackid == selectstackid)
                        {
                            checkEditstatusNonMPCSCS = true;
                        }
                    }
                    if (checkEditstatusNonMPCSCS == false)
                    {
                        ((DataTable)Session["EditStack"]).Rows.Add(dr);
                        ((DataTable)Session["EditStack"]).AcceptChanges();
                        gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                        gdEditStackingDetails.DataBind();
                        txtStackBags.Text = null;
                        txtStackWt.Text = null;
                        chksumEditNonMPSCSC();
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Entry for this stack is already done'); </script> ");
                    }
                }
                else
                {
                    ((DataTable)Session["EditStack"]).Rows.Add(dr);
                    ((DataTable)Session["EditStack"]).AcceptChanges();
                    gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                    gdEditStackingDetails.DataBind();
                    txtStackBags.Text = null;
                    txtStackWt.Text = null;
                    chksumEditNonMPSCSC();
                }
                btnUpdate.Enabled = true;
                gdEditStackingDetails.Enabled = true;
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack under the selected Commodity ,Category and Godown Number'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in ADD_EditStockNonMPSCSC has occured, try again'); </script> ");
        }
    }

    protected void chksumEditNonMPSCSC()
    {
        try
        {
            int stackbag = 0;
            decimal stackwts = 0;
            if (gdEditStackingDetails.Rows.Count > 0)
            {
                for (int s = 0; s < gdEditStackingDetails.Rows.Count; s++)
                {
                    stackbag = stackbag + int.Parse(gdEditStackingDetails.Rows[s].Cells[5].Text.ToString());
                    stackwts = stackwts + decimal.Parse(gdEditStackingDetails.Rows[s].Cells[6].Text.ToString());
                }
                txtBags_Non.Text = stackbag.ToString();
                txtQty_Non.Text = stackwts.ToString();
            }
            else
            {
                txtBags_Non.Text = "0";
                txtQty_Non.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error- chksumEditNonMPSCSC has occured, try again'); </script> ");
        }
    }

    protected void saveNonMPSCSCData()
    {
        if ((Session["Depot_DistID"].ToString() != "") && (Session["Depot_DepotID"].ToString() != ""))
        {
            try
            {
                Page.Validate("GD_Stack");
                Page.Validate("Non");
                if (Page.IsValid)
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                    int stackbags = 0;
                    decimal stackwt = 0;
                    int l;
                    if (gdstackingdetails.Rows.Count > 0)
                    {
                        for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                        {
                            stackbags = stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                            stackwt = stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                        }

                        if (ddldepositortype.SelectedItem.Text == "--Select--" || ddlDepositor.Items.Count == 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Type /Name of Depositor not specified correctly !'); </script> ");
                        }

                        else if (ddlArrival_Source.SelectedItem.Text == " --Select--" || ddlArrival_Source.SelectedValue == "0")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Arrival source not specified correctly !'); </script> ");
                        }

                        else if (Convert.ToInt32(txtBagsAcceptable_Non.Text.ToString()) != Convert.ToInt32(stackbags.ToString()) || (decimal.Parse(txtQtyAcceptable_Non.Text) != decimal.Parse(stackwt.ToString())))
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !'); </script> ");
                        }
                        else if (CheckValid == "0")
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Deposit Date should be greater that last Deposited Date and less then/equal to Current Date !'); </script> ");
                        }
                        else
                        {
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }
                            string QueryMax = "select isnull(Max(ArrivalStock_Id),0) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["BranchID"].ToString() + "' ";
                            SqlCommand cmd2 = new SqlCommand(QueryMax, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                            string str3 = cmd2.ExecuteScalar().ToString();
                            if (Convert.ToInt64(str3) != 0)
                            {
                                ArrivalStockid = Convert.ToString(Convert.ToInt64(str3) + 1);
                                if (ArrivalStockid != String.Empty || ArrivalStockid != "")
                                {
                                Found:
                                    string Queryc = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStockid.ToString() + "'";
                                    cmd2 = new SqlCommand(Queryc, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                    string maxcount = cmd2.ExecuteScalar().ToString();
                                    if (Convert.ToInt16(maxcount) > 0)
                                    {
                                        ArrivalStockid = Convert.ToString(Convert.ToInt64(ArrivalStockid) + 1);
                                        goto Found;
                                    }
                                }
                            }
                            else
                            {
                                string Depotid = Session["Depot_DepotID"].ToString();
                                string BranchId = Session["BranchId"].ToString();
                                ArrivalStockid = BranchId + System.DateTime.Now.Year.ToString().Substring(2, 2) + "00001";
                            }


                            //SqlCommand cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert_NonMPSCSC", Con);
                            SqlCommand cmd = new SqlCommand("[MPWLC_sp_MomentChallan_Reciept_insert]", Con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                            cmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();
                            cmd.Parameters.Add("@PurchasCentre", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@PurchasCentre"].Value = "0";

                            cmd.Parameters.Add("@DepositDate", SqlDbType.DateTime);
                            cmd.Parameters["@DepositDate"].Value = getDate_MDY(Convert.ToString(txtDepositDate_Non.Text.Trim()));
                            cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Commodity_Id"].Value = ddlCommodity_Non.SelectedValue;
                            cmd.Parameters.Add("@Mode_of_weighment", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Mode_of_weighment"].Value = ddlWeighmentMode_Non.SelectedValue;
                            cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                            cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);

                            if (txtBags_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_No_of_Bags"].Value = stackbags;
                            }
                            else
                            {

                                cmd.Parameters["@Qty_No_of_Bags"].Value = Convert.ToInt64(txtBags_Non.Text.ToString());
                            }

                            cmd.Parameters.Add("@Qty_Wt", SqlDbType.Decimal);
                            if (txtQty_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Qty_Wt"].Value = Convert.ToDecimal(txtQty_Non.Text.Trim().ToString());
                            }
                            cmd.Parameters.Add("@Category_Id", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Category_Id"].Value = ddlCategory_Non.SelectedValue;
                            cmd.Parameters.Add("@Depositor_Name", SqlDbType.NVarChar, 100);
                            cmd.Parameters["@Depositor_Name"].Value = ddlDepositor.SelectedItem.Text;
                            cmd.Parameters.Add("@DepositorType", SqlDbType.VarChar, 50);
                            cmd.Parameters["@DepositorType"].Value = ddldepositortype.SelectedItem.Text;
                            cmd.Parameters.AddWithValue("@AcceptanceNo", "");
                            cmd.Parameters.Add("@IssueID", SqlDbType.NVarChar, 30);
                            cmd.Parameters["@IssueID"].Value = ddldepositortype.SelectedItem.Text;
                            //change cropyear28-04
                            cmd.Parameters.Add("@Crop_Year", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@Crop_Year"].Value = ddlcropyear.SelectedValue;


                            cmd.Parameters.Add("@Sender_District", SqlDbType.NVarChar, 20);
                            cmd.Parameters.Add("@Sender_Godown", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Sender_District"].Value = ddlDist_Non.SelectedValue;
                            cmd.Parameters["@Sender_Godown"].Value = ddlDepo_Non.SelectedValue;


                            cmd.Parameters.Add("@Challan_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Challan_No"].Value = txtTCNo_Non.Text.ToString();
                            cmd.Parameters.Add("@Truck_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Truck_No"].Value = txtTruckNo_Non.Text.ToString();
                            cmd.Parameters.Add("@Source_of_Arrival", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Source_of_Arrival"].Value = ddlArrival_Source.SelectedValue;

                            cmd.Parameters.Add("@ArrivalSource_ID", SqlDbType.NVarChar, 2);
                            cmd.Parameters["@ArrivalSource_ID"].Value = ddlArrival_Source.SelectedValue;

                            cmd.Parameters.Add("@Quality_Moisture", SqlDbType.Decimal);
                            if (txtMoisture_Non.Text.ToString().Trim() == "")
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = Convert.ToDecimal(txtMoisture_Non.Text.ToString());
                            }

                            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 250);
                            cmd.Parameters["@Remarks"].Value = txtRemarks.Text.ToString();
                            cmd.Parameters.Add("@Scheme_ID", SqlDbType.NVarChar, 10);
                            cmd.Parameters["@Scheme_ID"].Value = ddlScheme_Non.SelectedValue;


                            cmd.Parameters.Add("@Acpt_FCIRO_No", SqlDbType.NVarChar, 50);
                            cmd.Parameters.Add("@Acpt_FCIRO_Date", SqlDbType.DateTime);
                            cmd.Parameters["@Acpt_FCIRO_No"].Value = DBNull.Value;
                            cmd.Parameters["@Acpt_FCIRO_Date"].Value = DBNull.Value;
                            //cmd.Parameters.AddWithValue("@PurchasCentre", "");
                            //cmd.Parameters.AddWithValue("@IssueID", "");
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@CreatedBy"].Value = Session["Depot_DepotID"].ToString();


                            cmd.Parameters.Add("@Client_IP", SqlDbType.VarChar, 20);
                            cmd.Parameters["@Client_IP"].Value = ClientIP.ToString();
                            cmd.Parameters.Add("@Transporter_id", SqlDbType.VarChar, 20);
                            if (ddlTransporter_Non.Items.Count == 0)
                            {
                                cmd.Parameters["@Transporter_id"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Transporter_id"].Value = ddlTransporter_Non.SelectedValue;
                            }

                            //Storing MillerId here for WLC
                            cmd.Parameters.Add("@Miller", SqlDbType.NVarChar, 100);
                            if (ddlMiller_Non.Visible == false)
                            {
                                cmd.Parameters["@Miller"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Miller"].Value = ddlMiller_Non.SelectedValue;
                            }

                            cmd.Parameters.Add("@WCMNo_Sending", SqlDbType.NVarChar, 50);
                            if (txtWCMNo_Non.Text.Trim() == "")
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = txtWCMNo_Non.Text;
                            }

                            cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                            //cmd.Parameters.Add("@GatePassId", SqlDbType.NVarChar, 20);
                            //cmd.Parameters["@GatePassId"].Direction = ParameterDirection.Output;

                            //cmd.Parameters.Add("@ArrivalStockId", SqlDbType.NVarChar, 20);
                            //cmd.Parameters["@ArrivalStockId"].Direction = ParameterDirection.Output;
                            cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);
                            cmd.Parameters.Add("@Acceptable_Bags", SqlDbType.Int);
                            if (txtBagsAcceptable_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Bags"].Value = stackbags;
                            }
                            else
                            {

                                cmd.Parameters["@Acceptable_Bags"].Value = Convert.ToInt64(txtBagsAcceptable_Non.Text.ToString());
                            }

                            cmd.Parameters.Add("@Acceptable_Wt", SqlDbType.Decimal);
                            if (txtQtyAcceptable_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = Convert.ToDecimal(txtQtyAcceptable_Non.Text.Trim().ToString());
                            }

                            cmd.ExecuteNonQuery();
                            receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                           // gatePassid = cmd.Parameters["@GatePassId"].Value.ToString();

                            ArrivalStockid = cmd.Parameters["@ArrivalStockId"].Value.ToString();
                            txtArrivalSrcId.Text = ArrivalStockid;

                            cmd.Dispose();
                            Con.Close();

                            // adding the stack details to the database
                            string G = "";
                            string S = "";
                            int j;
                            for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                            {

                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                SqlCommand sqlCmd = new SqlCommand();
                                sqlCmd.Connection = Con;
                                sqlCmd.CommandText = "MPWLC_sp_stackingdetails_insert";
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 50);
                                sqlCmd.Parameters.Add("@GodownId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@StackId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@SBags", SqlDbType.Int);
                                sqlCmd.Parameters.Add("@SWeight", SqlDbType.Decimal);
                                sqlCmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());

                                

                                sqlCmd.Parameters["@receiptid"].Value = receiptid;

                                sqlCmd.Parameters["@GodownId"].Value = gdstackingdetails.Rows[j].Cells[1].Text.ToString();
                                sqlCmd.Parameters["@StackId"].Value = gdstackingdetails.Rows[j].Cells[2].Text.ToString();
                                sqlCmd.Parameters["@SBags"].Value = int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString());
                                sqlCmd.Parameters["@SWeight"].Value = decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString());
                                sqlCmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                                sqlCmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();

                                sqlCmd.ExecuteNonQuery();

                                G = G + gdstackingdetails.Rows[j].Cells[3].Text.ToString() + "/";
                                S = S + gdstackingdetails.Rows[j].Cells[4].Text.ToString() + "/";
                            }

                            G = G.TrimEnd('/');
                            S = S.TrimEnd('/');

                            SqlCommand sqlCmd2 = new SqlCommand();
                            sqlCmd2.CommandText = "update tbl_Storage_GatePass_Enrty set Godown_ID='" + G + "' ,Stack_ID='" + S + "' where GatePass_No='" + gatePassid.ToString() + "'";
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Connection = Con;
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }

                            sqlCmd2.ExecuteReader();
                            Con.Close();
                            sqlCmd2.Dispose();

                            Session["dt1"] = null;
                            gdstackingdetails.DataSource = null;
                            gdstackingdetails.DataBind();

                            Session["RefreshButton"] = "Yes";

                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved successfully'); </script> ");
                            Session["RefreshButton"] = "No";

                            Session["Mode"] = "NON-Add";
                            Session["WLCDepSource"] = "NON-MPSCSC";
                            EmptyNonMPSCSCfields();

                            //string arrid = "Gate_Pass.aspx?src=MC&id=" + txtArrivalSrcId.Text;
                            //StringBuilder sb = new StringBuilder();
                            //sb.Append("<script>");
                            //sb.Append("window.open(");
                            //sb.Append("'" + arrid + "'");
                            //sb.Append(",'MyWindow', 'height=800,width=780');");
                            //sb.Append("</script>");
                            //this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());

                        } ///here transactions ends
                    }
                    else
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please add Stack information first'); </script> ");
                    }
                }
                else
                {
                    Session["errDesc"] = "Invalid input data";
                    Server.Transfer("../../CustomError.aspx");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error saveNonMPSCSCData has occurred'); </script> ");
            }
            finally
            {
                Con.Close();
            }
        }
    }

    protected void EmptyNonMPSCSCfields()
    {
        ddlArrival_Source.ClearSelection();
        ddldepositortype.ClearSelection();
        ddlDepositor.Items.Clear();
        ddlDist_Non.Items.Clear();
        ddlDepo_Non.Items.Clear();
        txtTCNo_Non.Text = "";
        txtTruckNo_Non.Text = "";
        txtDepositDate_Non.Text = "";
        txtBags_Non.Text = "";
        txtMoisture_Non.Text = "";
        txtWCMNo_Non.Text = "";
        txtTCNo_Non.Text = "";
        ddlCommodity_Non.ClearSelection();
        ddlCategory_Non.ClearSelection();
        ddlScheme_Non.ClearSelection();
        ddlTransporter_Non.ClearSelection();
        ddlMiller_Non.ClearSelection();
        ddlWeighmentMode_Non.ClearSelection();
    }

    protected void ddlArrival_Source_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistDepot();
    }

    protected void ddlDist_Non_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepo_Non.Items.Clear();
            ddlDepo_Non.DataSource = null;
            ddlDepo_Non.DataBind();
            if (ddlArrival_Source.SelectedValue == "01")//Procurement then show Source Society i.e. PC 
            {
                string query = "select PcId,purchaseCenterName from TBL_MetaData_Purchase_Center where DistrictId='" + ddlDist_Non.SelectedValue.ToString() + "' order by purchaseCenterName";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepo_Non.DataSource = ds.Tables[0];
                    ddlDepo_Non.DataTextField = "purchaseCenterName";
                    ddlDepo_Non.DataValueField = "PcId";
                    ddlDepo_Non.DataBind();
                    ddlDepo_Non.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depot Found!'); </script> ");
                }
            }
            else if (ddlArrival_Source.SelectedValue == "02") //Other Depot
            {
                string query = "select DepotID,DepotName FROM MPSCSC.dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepotName";
                //string query = "select DepotID,DepotName FROM MPSCSC.dbo.tbl_MetaData_DEPOT  where DistrictId='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepotName";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepo_Non.DataSource = ds.Tables[0];
                    ddlDepo_Non.DataTextField = "DepotName";
                    ddlDepo_Non.DataValueField = "DepotID";
                    ddlDepo_Non.DataBind();
                    ddlDepo_Non.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depot Found!'); </script> ");
                }
            }
            else if (ddlArrival_Source.SelectedValue == "03")//FCI Depot
            {
                string query = "select DepoCode,DepoName FROM MPSCSC.dbo.DepoCode  where District_Code='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepoName";
               // string query = "select DepoCode,DepoName FROM MPSCSC.dbo.DepoCode  where District_Code='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepoName";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepo_Non.DataSource = ds.Tables[0];
                    ddlDepo_Non.DataTextField = "DepoName";
                    ddlDepo_Non.DataValueField = "DepoCode";
                    ddlDepo_Non.DataBind();
                    ddlDepo_Non.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depot Found!'); </script> ");
                }
            }
            else
            {
                string query = "SELECT  DepotID, DepotName FROM tbl_MetaData_DEPOT  where DistrictId='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepotName";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepo_Non.DataSource = ds;
                    ddlDepo_Non.DataTextField = "DepotName";
                    ddlDepo_Non.DataValueField = "DepotID";
                    ddlDepo_Non.DataBind();
                    ddlDepo_Non.Items.Insert(0, "--Select--");
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depot Found!'); </script> ");
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error -ddlDist_Non_SelectedIndexChanged has occured, try again'); </script> ");
        }
    }

    protected void FillDefaultsForUpdateNonMPSCSC()
    {
        try
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("WLC_FillDefaultsForUpdate", Con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@storageReceipt_ID", SqlDbType.NVarChar, 20);
            cmd.Parameters["@storageReceipt_ID"].Value = Session["WLC_StorageReceipt_Id"].ToString();


            da.SelectCommand = cmd;
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 1)
            {
                fillDefaultsForNonMPSCSC();

                ddldepositortype.Items.Clear();
                ddlDepositor.Items.Clear();
                ddldepositortype.Items.Add(new ListItem(ds.Tables[0].Rows[0]["Depositortype"].ToString(), ds.Tables[0].Rows[0]["Depositortype"].ToString()));
                ddlDepositor.Items.Add(new ListItem(ds.Tables[0].Rows[0]["DepositorName"].ToString(), ds.Tables[0].Rows[0]["DepositorName"].ToString()));

                ddldepositortype.Enabled = false;
                ddlDepositor.Enabled = false;


                SourceOfArrival();

                ddlArrival_Source.SelectedValue = ds.Tables[0].Rows[0]["ArrivalSource_ID"].ToString();
                ddlArrival_Source.Enabled = false;

                txtTCNo_Non.Text = ds.Tables[0].Rows[0]["Challan_No"].ToString();

                txtTruckNo_Non.Text = ds.Tables[0].Rows[0]["Truck_No"].ToString();

                ddlCommodity_Non.DataBind();
                ddlCommodity_Non.SelectedValue = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();
                ddlCommodity_Non.Enabled = false;

                FillGodown();
                fillStack();
                fillCategory_non();


                ddlCategory_Non.SelectedValue = ds.Tables[0].Rows[0]["Category_Id"].ToString();
                ddlCategory_Non.Enabled = false;
                txtDepositDate_Non.Text = ds.Tables[0].Rows[0]["Receipt_Date"].ToString();
                txtDepositDate_Non.Enabled = false;


                txtBagsAcceptable_Non.Text = ds.Tables[0].Rows[0]["Qty_Rvd_No_of_Bags"].ToString();
                txtQtyAcceptable_Non.Text = ds.Tables[0].Rows[0]["Qty_Rvd_Weight"].ToString();
                txtBags_Non.Text = ds.Tables[0].Rows[0]["Qty_No_of_Bags"].ToString();
                txtQty_Non.Text = ds.Tables[0].Rows[0]["Qty_Wt"].ToString();


                txtMoisture_Non.Text = ds.Tables[0].Rows[0]["Quality_Moisture"].ToString();

                ddlScheme_Non.DataBind();
                if (ds.Tables[0].Rows[0]["scheme_Id"].ToString() == "0")
                {
                    ddlScheme_Non.Items[0].Selected = true;
                }
                else
                {
                    ddlScheme_Non.SelectedValue = ds.Tables[0].Rows[0]["scheme_Id"].ToString();
                }
                txtWCMNo_Non.Text = ds.Tables[0].Rows[0]["WCMNo_Sending"].ToString();
                ddlWeighmentMode_Non.SelectedValue = ds.Tables[0].Rows[0]["Mode_of_weighment"].ToString();
                ddlTransporter_Non.SelectedValue = ds.Tables[0].Rows[0]["Transporter_Id"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                if (ds.Tables[0].Rows[0]["Commodity_Name"].ToString().Substring(0, 4).ToUpper() == "RICE")
                {
                    fillMiller();
                    ddlMiller_Non.SelectedValue = ds.Tables[0].Rows[0]["Miller_Name"].ToString();
                    ddlMiller_Non.Visible = true;
                    lblCMR_Rice_Miller_Non.Visible = true;


                }
                else
                {
                    ddlMiller_Non.ClearSelection();
                    ddlMiller_Non.Visible = false;
                    lblCMR_Rice_Miller_Non.Visible = false;
                }
                if (Session["WLCDepSource"].ToString() == "01")
                {
                    ddlDist_Non.Items.Clear();
                    ddlDepo_Non.Items.Clear();
                    ddlDist_Non.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderDist"].ToString(), ds.Tables[0].Rows[0]["Sender_district"].ToString()));
                    ddlDepo_Non.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderGd"].ToString(), ds.Tables[0].Rows[0]["Sender_godown"].ToString()));
                    ddlDist_Non.Enabled = false;
                    ddlDepo_Non.Enabled = false;

                    lblDepot.Visible = false;
                    lblSSociety.Visible = false;
                    ddlDist_Non.Visible = false;
                    ddlDepo_Non.Visible = false;
                    lblDistrict.Visible = false;

                }
                else if (Session["WLCDepSource"].ToString() == "02")
                {
                    ddlDist_Non.Items.Clear();
                    ddlDepo_Non.Items.Clear();
                    ddlDist_Non.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderDist"].ToString(), ds.Tables[0].Rows[0]["Sender_district"].ToString()));
                    ddlDepo_Non.Items.Add(new ListItem(ds.Tables[0].Rows[0]["SenderGd"].ToString(), ds.Tables[0].Rows[0]["Sender_godown"].ToString()));

                    ddlDist_Non.Enabled = false;
                    ddlDepo_Non.Enabled = false;

                    lblDepot.Visible = false;
                    lblSSociety.Visible = false;
                    ddlDist_Non.Visible = false;
                    ddlDepo_Non.Visible = false;
                    lblDistrict.Visible = false;

                }
                else if (Session["WLCDepSource"].ToString() == "03")
                {
                    ddlDist_Non.Items.Clear();
                    ddlDepo_Non.Items.Clear();
                    fillDistDepot();
                    ddlDist_Non.SelectedValue = ds.Tables[0].Rows[0]["Sender_district"].ToString();
                    fillFCIDepotForDist();
                    ddlDepo_Non.SelectedValue = ds.Tables[0].Rows[0]["Sender_godown"].ToString();

                    lblA_Depo.Visible = true;
                    lblSSociety.Visible = false;

                    lblDistrict.Text = "Region/District";
                    lblDepot.Text = "Depot";
                }
                else
                {
                    fillDistDepot();
                    ddlDist_Non.SelectedValue = ds.Tables[0].Rows[0]["SenderDist"].ToString();
                    ddlDepo_Non.SelectedValue = ds.Tables[0].Rows[0]["SenderGd"].ToString();
                    lblSSociety.Visible = false;
                    lblDepot.Visible = false;
                    lblSSociety.Visible = false;
                    ddlDist_Non.Visible = false;
                    ddlDepo_Non.Visible = false;
                    lblDistrict.Visible = false;
                }
                ddlGodownNo.Enabled = false;
                ddlStackNo.Enabled = false;
                pnlNONMPSCSC.Visible = true;
                pnlFCI_OTDepot.Visible = false;

                //fillStack();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    Session["EditStack"] = ds.Tables[1];
                    gdEditStackingDetails.DataSource = (DataTable)Session["EditStack"];
                    gdEditStackingDetails.DataBind();
                }
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Record, Can not be edited'); </script> ");
                Response.Redirect("Edit_WLC_Deposit_From.aspx?PopMsg=" + "Invalid Record, Can not be edited!" + "");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in FillDefaultsForUpdateNonMPSCSC has occured, try again'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }

    private void SourceOfArrival()
    {
        try
        {
            string query = "SELECT '0' as [Source_ID] ,' --Select--' as [Source_Name] union SELECT [Source_ID],[Source_Name] FROM MPSCSC.dbo.[Source_Arrival_Type] order by  Source_ID";
           // string query = "SELECT '0' as [Source_ID] ,' --Select--' as [Source_Name] union SELECT [Source_ID],[Source_Name] FROM MPSCSC.dbo.[Source_Arrival_Type] order by  Source_ID";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlArrival_Source.Items.Clear();
                ddlArrival_Source.DataSource = ds.Tables[0];
                ddlArrival_Source.DataTextField = "Source_Name";
                ddlArrival_Source.DataValueField = "Source_ID";
                ddlArrival_Source.DataBind();
            }
        }
        catch (Exception)
        {

            // throw;
        }

    }

    private void fillCategory_non()
    {
        try
        {
            string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory_Non.Items.Clear();
                ddlCategory_Non.DataSource = ds.Tables[0];
                ddlCategory_Non.DataTextField = "Category_Name";
                ddlCategory_Non.DataValueField = "Category_Id";
                ddlCategory_Non.DataBind();

            }
        }
        catch (Exception)
        {

            /// throw;
        }

    }

    protected void fillFCIDepotForDist()
    {
        try
        {
            string query = "select DepoCode,DepoName FROM MPSCSC.dbo.DepoCode  where District_Code='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepoName";
           // string query = "select DepoCode,DepoName FROM MPSCSC.dbo.DepoCode  where District_Code='" + ddlDist_Non.SelectedValue.ToString() + "' order by DepoName";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepo_Non.DataTextField = "DepoName";
                ddlDepo_Non.DataValueField = "DepoCode";
                ddlDepo_Non.DataBind();
                ddlDepo_Non.Items.Insert(0, "--Select--");
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depot Found!'); </script> ");
            }
        }
        catch (Exception ex)
        {
            ////
        }
    }

    protected void UpdateRecieptDataNonMPSCSC()
    {

        if ((Session["Depot_DistID"] != "") && (Session["Depot_DepotID"] != ""))
        {

            try
            {


                if (Page.IsValid)
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    int stackbags = 0;

                    decimal stackwt = 0;
                    int l;
                    if (gdEditStackingDetails.Rows.Count > 0)
                    {
                        for (l = 0; l < gdEditStackingDetails.Rows.Count; l++)
                        {
                            stackbags = stackbags + int.Parse(gdEditStackingDetails.Rows[l].Cells[5].Text.ToString());
                            stackwt = stackwt + decimal.Parse(gdEditStackingDetails.Rows[l].Cells[6].Text.ToString());
                        }



                        if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                        {
                            Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");

                        }

                        else if ((txtQtyAcceptable_Non.Text != "") && (Convert.ToInt32(txtBagsAcceptable_Non.Text.ToString()) != Convert.ToInt32(stackbags.ToString())))
                        {
                            StringBuilder str1 = new StringBuilder();
                            str1.Append("<script>");
                            str1.Append("alert('" + "Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
                        }
                        else if ((txtQtyAcceptable_Non.Text != "") && ((decimal.Parse(txtQtyAcceptable_Non.Text) != decimal.Parse(stackwt.ToString()))))
                        {
                            StringBuilder str1 = new StringBuilder();
                            str1.Append("<script>");
                            str1.Append("alert('" + "Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
                        }
                        else
                        {



                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_Update_NonMPSCSC", Con);
                            cmd.CommandType = CommandType.StoredProcedure;



                            cmd.Parameters.Add("@Qty_No_of_Bags", SqlDbType.Int);
                            if (txtBags_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_No_of_Bags"].Value = stackbags;
                            }
                            else
                            {
                                cmd.Parameters["@Qty_No_of_Bags"].Value = Convert.ToInt64(txtBags_Non.Text.ToString());
                            }

                            cmd.Parameters.Add("@Qty_Wt", SqlDbType.Decimal);
                            if (txtQty_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Qty_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Qty_Wt"].Value = Convert.ToDecimal(txtQty_Non.Text.Trim().ToString());
                            }

                            cmd.Parameters.Add("@Mode_of_weighment", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Mode_of_weighment"].Value = ddlWeighmentMode_Non.SelectedValue;

                            cmd.Parameters.Add("@Sender_District", SqlDbType.NVarChar, 20);
                            cmd.Parameters.Add("@Sender_Godown", SqlDbType.NVarChar, 20);
                            if (ddlArrival_Source.SelectedValue == "03")
                            {
                                cmd.Parameters["@Sender_District"].Value = ddlDist_Non.SelectedValue;
                                cmd.Parameters["@Sender_Godown"].Value = ddlDepo_Non.SelectedValue;
                            }
                            else
                            {
                                cmd.Parameters["@Sender_District"].Value = Session["Depot_DistID"].ToString();
                                cmd.Parameters["@Sender_Godown"].Value = Session["Depot_DepotID"].ToString();
                            }
                            cmd.Parameters.Add("@Challan_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Challan_No"].Value = txtTCNo_Non.Text.ToString();
                            cmd.Parameters.Add("@Truck_No", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@Truck_No"].Value = txtTruckNo_Non.Text.ToString();
                            cmd.Parameters.Add("@Quality_Moisture", SqlDbType.Decimal);
                            if (txtMoisture_Non.Text != "")
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = Convert.ToDecimal(txtMoisture_Non.Text.ToString());
                            }
                            else
                            {
                                cmd.Parameters["@Quality_Moisture"].Value = DBNull.Value;
                            }
                            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 250);
                            cmd.Parameters["@Remarks"].Value = txtRemarks.Text.ToString();
                            cmd.Parameters.Add("@Client_IP", SqlDbType.VarChar, 20);
                            cmd.Parameters["@Client_IP"].Value = ClientIP.ToString();
                            cmd.Parameters.Add("@Transporter_id", SqlDbType.VarChar, 20);
                            if (ddlTransporter_Non.Items.Count == 0)
                            {
                                cmd.Parameters["@Transporter_id"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Transporter_id"].Value = ddlTransporter_Non.SelectedValue;
                            }
                            cmd.Parameters.Add("@WCMNo_Sending", SqlDbType.NVarChar, 50);
                            if (txtWCMNo_Non.Text.Trim() == "")
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@WCMNo_Sending"].Value = txtWCMNo_Non.Text;
                            }


                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50);
                            cmd.Parameters["@UpdatedBy"].Value = Session["Depot_DepotID"].ToString();

                            //Storing MillerId here for WLC
                            cmd.Parameters.Add("@Miller", SqlDbType.NVarChar, 100);
                            if (ddlMiller_Non.Visible == false)
                            {
                                cmd.Parameters["@Miller"].Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters["@Miller"].Value = ddlMiller_Non.SelectedValue;
                            }


                            cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@receiptid"].Value = Session["WLC_StorageReceipt_Id"].ToString();

                            cmd.Parameters.Add("@GatePassId", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@GatePassId"].Direction = ParameterDirection.Output;


                            cmd.Parameters.Add("@ArrivalStockId", SqlDbType.NVarChar, 20);
                            cmd.Parameters["@ArrivalStockId"].Direction = ParameterDirection.Output;




                            cmd.Parameters.Add("@Acceptable_Bags", SqlDbType.Int);
                            if (txtBagsAcceptable_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Bags"].Value = stackbags;
                            }
                            else
                            {

                                cmd.Parameters["@Acceptable_Bags"].Value = Convert.ToInt64(txtBagsAcceptable_Non.Text.ToString());
                            }

                            cmd.Parameters.Add("@Acceptable_Wt", SqlDbType.Decimal);
                            if (txtQtyAcceptable_Non.Text.Trim().ToString() == "")
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = stackwt;
                            }
                            else
                            {
                                cmd.Parameters["@Acceptable_Wt"].Value = Convert.ToDecimal(txtQtyAcceptable_Non.Text.Trim().ToString());
                            }



                            cmd.ExecuteNonQuery();

                            gatePassid = cmd.Parameters["@GatePassId"].Value.ToString();

                            txtArrivalSrcId.Text = cmd.Parameters["@ArrivalStockId"].Value.ToString();

                            cmd.Dispose();
                            Con.Close();


                            // adding the stack details to the database



                            string G = "";
                            string S = "";
                            int j;
                            for (j = 0; j < gdEditStackingDetails.Rows.Count; j++)
                            {

                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                SqlCommand sqlCmd = new SqlCommand();
                                sqlCmd.CommandText = "MPWLC_sp_stackingdetails_Update";
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 50);
                                sqlCmd.Parameters.Add("@GodownId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@StackId", SqlDbType.NVarChar, 20);
                                sqlCmd.Parameters.Add("@SBags", SqlDbType.Int);
                                sqlCmd.Parameters.Add("@SWeight", SqlDbType.Decimal);

                                sqlCmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 50);

                                sqlCmd.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);

                                sqlCmd.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);


                                sqlCmd.Connection = Con;

                                sqlCmd.Parameters["@receiptid"].Value = Session["WLC_StorageReceipt_Id"].ToString();

                                sqlCmd.Parameters["@GodownId"].Value = gdEditStackingDetails.Rows[j].Cells[1].Text.ToString();
                                sqlCmd.Parameters["@StackId"].Value = gdEditStackingDetails.Rows[j].Cells[2].Text.ToString();
                                sqlCmd.Parameters["@SBags"].Value = int.Parse(gdEditStackingDetails.Rows[j].Cells[5].Text.ToString());
                                sqlCmd.Parameters["@SWeight"].Value = decimal.Parse(gdEditStackingDetails.Rows[j].Cells[6].Text.ToString());

                                sqlCmd.Parameters["@UpdatedBy"].Value = Session["Depot_DepotID"].ToString();
                                sqlCmd.Parameters["@District_Id"].Value = Session["Depot_DistID"].ToString();
                                sqlCmd.Parameters["@DepotId"].Value = Session["Depot_DepotID"].ToString();

                                sqlCmd.ExecuteNonQuery();



                                G = G + gdEditStackingDetails.Rows[j].Cells[3].Text.ToString() + "/";
                                S = S + gdEditStackingDetails.Rows[j].Cells[4].Text.ToString() + "/";
                            }

                            G = G.TrimEnd('/');
                            S = S.TrimEnd('/');


                            SqlCommand sqlCmd2 = new SqlCommand();

                            sqlCmd2.CommandText = "insert into tbl_Storage_GatePass_Enrty_Log  select * from tbl_Storage_GatePass_Enrty where GatePass_No='" + gatePassid.ToString() + "';update tbl_Storage_GatePass_Enrty set Godown_ID='" + G + "' ,Stack_ID='" + S + "' where GatePass_No='" + gatePassid.ToString() + "'";
                            sqlCmd2.CommandType = CommandType.Text;
                            sqlCmd2.Connection = Con;
                            if (Con.State == ConnectionState.Closed)
                            {
                                Con.Open();
                            }

                            sqlCmd2.ExecuteReader();
                            Con.Close();
                            sqlCmd2.Dispose();

                            Session["EditStack"] = null;
                            gdEditStackingDetails.DataSource = null;
                            gdEditStackingDetails.DataBind();





                            Session["RefreshButton"] = "Yes";

                            StringBuilder str = new StringBuilder();
                            str.Append("<script>");
                            str.Append("alert('" + "Record saved successfully" + "');</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());


                            Session["RefreshButton"] = "No";

                            string arrid = "Gate_Pass.aspx?src=MC&id=" + txtArrivalSrcId.Text;
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<script>");
                            sb.Append("window.open(");
                            sb.Append("'" + arrid + "'");
                            sb.Append(",'MyWindow', 'height=800,width=780');");
                            sb.Append("</script>");
                            this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());

                            Response.Redirect("Edit_WLC_Deposit_From.aspx?PopMsg=" + "Record saved successfully!" + "");
                        } ///here transactions ends



                    }
                    else
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + "Please add Stack information first" + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                    }
                }
                else
                {
                    Session["errDesc"] = "Invalid input data";
                    Server.Transfer("CustomError.aspx");
                }
            }
            catch (Exception ex)
            {

                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Error in UpdateRecieptDataNonMPSCSC has occurred" + ex.Message + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                //lblmsg.ForeColor = System.Drawing.Color.Red;
                //lblmsg.Text = "Data could not be saved as some error has occurred ";
            }
            finally
            {
                Con.Close();
            }
        }
        else
        {
        }
    }

    private void fillCommodity_non()
    {
        try
        {
            // string qrySelect = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity_Non.DataSource = ds.Tables[0];
                ddlCommodity_Non.DataTextField = "Commodity_Name";
                ddlCommodity_Non.DataValueField = "Commodity_Id";
                ddlCommodity_Non.DataBind();
                ddlCommodity_Non.Items.Remove(ddlCommodity_Non.Items.FindByText("Wheat-PSS"));
                ddlCommodity_Non.Items.Remove(ddlCommodity_Non.Items.FindByText("Wheat-PDS"));

            }
        }
        catch (Exception)
        {

            /// throw;
        }

    }

    private void fillCommodity()
    {
        try
        {
            //string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order";
            string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommodity.DataSource = ds.Tables[0];
                ddlCommodity.DataTextField = "Commodity_Name";
                ddlCommodity.DataValueField = "Commodity_Id";
                ddlCommodity.DataBind();
            }
        }
        catch (Exception)
        {

            //// throw;
        }
    }

    private void fillCategory()
    {
        try
        {
            string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = ds.Tables[0];
                ddlCategory.DataTextField = "Category_Name";
                ddlCategory.DataValueField = "Category_Id";
                ddlCategory.DataBind();
            }
        }
        catch (Exception)
        {

            //// throw;
        }

    }

    protected void fillCropYear()
    {
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString());
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString());
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString());
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString());
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString());
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString());
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString());
        ddlcropyear.Items.Insert(0, "All");
        ddlcropyear.SelectedIndex = 1;
        //ddlcropyr.SelectedIndex = 2;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["WLC_StorageReceipt_Id"] != null)
            {
                if (Session["Mode"].ToString() == "NON-Edit")
                {
                    //Insert NON MPSCSC data
                    UpdateRecieptDataNonMPSCSC();
                    //saveNonMPSCSCData();
                }
                else
                {
                    UpdateRecieptData();
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Error in (btnUpdateClick) has occured, try again'); </script> ");
        
        
        
        }
    }
    //Change 21-04-2014
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                btnsave.Enabled = false;

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                if ((Session["WLCDepSource"].ToString() == "NON-MPSCSC") && (Session["Mode"].ToString() == "NON-Add"))
                {
                    //Insert NON MPSCSC data
                    saveNonMPSCSCData();
                }
                else if (Session["WLCDepSource"].ToString() == "07")//Rail Head
                {
                    saveRailHeadData();
                }
                else if (Session["WLCDepSource"].ToString() != "01")//Other than Procurement (MPSCSC)
                {
                    try
                    {
                        int _stackbags = 0;
                        decimal _stackwt = 0;
                        int l;
                        if (gdstackingdetails.Rows.Count > 0)
                        {
                            for (l = 0; l < gdstackingdetails.Rows.Count; l++)
                            {
                                _stackbags = _stackbags + int.Parse(gdstackingdetails.Rows[l].Cells[5].Text.ToString());
                                _stackwt = _stackwt + decimal.Parse(gdstackingdetails.Rows[l].Cells[6].Text.ToString());
                            }
                            if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                            {
                                Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");

                            }
                            else if (Convert.ToInt32(txtBagsAcceptable.Text.ToString()) != Convert.ToInt32(_stackbags.ToString()) || (decimal.Parse(txtQtyAcceptable.Text) != decimal.Parse(_stackwt.ToString())))
                            {
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Total Bags/Weight Recieved should be equal to sum of Stack Bags/Weight !'); </script> ");
                            }
                            else
                            {
                                if (Con.State == ConnectionState.Closed)
                                {
                                    Con.Open();
                                }
                                string QueryMax = "select isnull(Max(ArrivalStock_Id),0) from tbl_Storage_Arrival_Stock where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchID='" + Session["BranchId"].ToString() + "' ";
                                cmd = new SqlCommand(QueryMax, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                string str3 = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt64(str3) != 0)
                                {
                                    ArrivalStockid = Convert.ToString(Convert.ToInt64(str3) + 1);
                                    if (ArrivalStockid != String.Empty || ArrivalStockid != "")
                                    {
                                    Found:
                                        string Queryc = "select count(ArrivalStock_Id) from tbl_Storage_Arrival_Stock where ArrivalStock_Id='" + ArrivalStockid.ToString() + "'";
                                        cmd = new SqlCommand(Queryc, Con); // check ArrivalStockid present in tbl_Storage_Arrival_Stock table
                                        string maxcount = cmd.ExecuteScalar().ToString();
                                        if (Convert.ToInt16(maxcount) > 0)
                                        {
                                            ArrivalStockid = Convert.ToString(Convert.ToInt64(ArrivalStockid) + 1);
                                            goto Found;
                                        }
                                    }
                                }
                                else
                                {
                                    string Depotid = Session["Depot_DepotID"].ToString();
                                    string BranchId = Session["BranchId"].ToString();
                                    ArrivalStockid = BranchId + System.DateTime.Now.Year.ToString().Substring(2, 2) + "00001";
                                }
                               
                                cmd = new SqlCommand("MPWLC_sp_MomentChallan_Reciept_insert", Con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ArrivalStockId", ArrivalStockid);
                                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                cmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                cmd.Parameters.AddWithValue("@DepositDate", getDate_MDY(Convert.ToString(txtDepositDate.Text.Trim())));
                                cmd.Parameters.AddWithValue("@Commodity_Id", ddlCommodity.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@Mode_of_weighment", ddlWeigmentMode.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@AcceptanceNo", "");
                                cmd.Parameters.AddWithValue("@PurchasCentre", "");
                                cmd.Parameters.AddWithValue("@IssueID", "");
                                cmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                if (txtBags.Text.Trim().ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Qty_No_of_Bags", _stackbags);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Qty_No_of_Bags", Convert.ToInt64(txtBags.Text.ToString()));
                                }
                                if (txtQtyDeposit.Text.Trim().ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Qty_Wt", _stackwt);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Qty_Wt", Convert.ToDecimal(txtQtyDeposit.Text.Trim().ToString()));
                                }
                                cmd.Parameters.AddWithValue("@Category_Id", ddlCategory.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@Depositor_Name", "MPSCSC");
                                cmd.Parameters.Add("@DepositorType", SqlDbType.VarChar, 20);
                                cmd.Parameters["@DepositorType"].Value = "Institution";
                                cmd.Parameters.AddWithValue("@Crop_Year", ddlcropyear.SelectedValue.ToString());
                                if ((Session["WLCDepSource"].ToString() == "02") || (Session["WLCDepSource"].ToString() == "03"))//Other Depot & FCI
                                {
                                    cmd.Parameters.AddWithValue("@Sender_District", ddlA_Dist.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Sender_Godown", ddlA_Depo.SelectedValue);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Sender_District", Session["Depot_DistID"].ToString());
                                    cmd.Parameters.AddWithValue("@Sender_Godown", Session["Depot_DepotID"].ToString());
                                }
                                cmd.Parameters.AddWithValue("@Challan_No", txtTCNo.Text.ToString());
                                cmd.Parameters.AddWithValue("@Truck_No", txtTruckNo.Text.ToString());
                                cmd.Parameters.AddWithValue("@Source_of_Arrival", Session["WLCDepSource"].ToString());
                                cmd.Parameters.Add("@ArrivalSource_ID", Session["WLCDepSource"].ToString());
                                if (Session["WLCDepSource"].ToString() == "04")//Levy Rice
                                {
                                    cmd.Parameters.AddWithValue("@Miller", ddlCMR_Rice_Miller.SelectedValue);
                                }
                                else if (Session["WLCDepSource"].ToString() == "05")//CMR
                                {
                                    cmd.Parameters.AddWithValue("@Miller", ddlCMR_Rice_Miller.SelectedValue);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Miller", DBNull.Value);
                                }
                              
                                if (txtMoisture.Text.ToString().Trim() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Quality_Moisture", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Quality_Moisture", Convert.ToDecimal(txtMoisture.Text.ToString()));
                                }
                                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.ToString());
                                cmd.Parameters.AddWithValue("@Scheme_ID", "0");
                                if (Session["WLCDepSource"].ToString() == "03")//FCI
                                {
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_No", hfRoNo.Value.ToString());
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date", getDate_MDY(Convert.ToString(hfRODate.Value.Trim())));
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_No",DBNull.Value);
                                    cmd.Parameters.AddWithValue("@Acpt_FCIRO_Date",DBNull.Value);
                                }
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["Depot_DepotID"].ToString());
                                cmd.Parameters.AddWithValue("@Client_IP", ClientIP.ToString());
                                if (ddlTransporter.Items.Count == 0)
                                {
                                    cmd.Parameters.AddWithValue("@Transporter_id", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Transporter_id",ddlTransporter.SelectedValue.ToString());
                                }
                                if (txtWCMNo.Text.Trim() == "")
                                {
                                    cmd.Parameters.AddWithValue("@WCMNo_Sending", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@WCMNo_Sending",txtWCMNo.Text.Trim().ToString());
                                }

                                cmd.Parameters.Add("@receiptid", SqlDbType.NVarChar, 20);
                                cmd.Parameters["@receiptid"].Direction = ParameterDirection.Output;
                                if (txtBagsAcceptable.Text.Trim().ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Acceptable_Bags", _stackbags);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Acceptable_Bags", Convert.ToInt64(txtBagsAcceptable.Text.ToString()));
                                }
                                if (txtQtyAcceptable.Text.Trim().ToString() == "")
                                {
                                    cmd.Parameters.AddWithValue("@Acceptable_Wt", _stackwt);
                                }
                                else
                                {
                                   cmd.Parameters.AddWithValue("@Acceptable_Wt",Convert.ToDecimal(txtQtyAcceptable.Text.Trim().ToString()));
                                }

                                cmd.ExecuteNonQuery();
                                receiptid = cmd.Parameters["@receiptid"].Value.ToString();
                                txtArrivalSrcId.Text = ArrivalStockid;
                                cmd.Dispose();
                                Con.Close();

                                // adding the stack details to the database
                                string G = "";
                                string S = "";
                                int j;
                                for (j = 0; j < gdstackingdetails.Rows.Count; j++)
                                {
                                    if (Con.State == ConnectionState.Closed)
                                    {
                                        Con.Open();
                                    }
                                    SqlCommand sqlCmd = new SqlCommand();
                                    sqlCmd.Connection = Con;
                                    sqlCmd.CommandText = "MPWLC_sp_stackingdetails_insert";
                                    sqlCmd.CommandType = CommandType.StoredProcedure;
                                    sqlCmd.Parameters.AddWithValue("@receiptid", receiptid);
                                    sqlCmd.Parameters.AddWithValue("@GodownId", gdstackingdetails.Rows[j].Cells[1].Text.ToString());
                                    sqlCmd.Parameters.AddWithValue("@StackId",gdstackingdetails.Rows[j].Cells[2].Text.ToString());
                                    sqlCmd.Parameters.AddWithValue("@SBags", int.Parse(gdstackingdetails.Rows[j].Cells[5].Text.ToString()));
                                    sqlCmd.Parameters.AddWithValue("@SWeight", decimal.Parse(gdstackingdetails.Rows[j].Cells[6].Text.ToString()));
                                    sqlCmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                                    sqlCmd.Parameters.AddWithValue("@DepotId", Session["Depot_DepotID"].ToString());
                                    sqlCmd.Parameters.AddWithValue("@BranchId", Session["BranchId"].ToString());
                                    sqlCmd.ExecuteNonQuery();

                                    //changed added fn to update the gate pass godown/stack info in gate pass table also
                                    G = G + gdstackingdetails.Rows[j].Cells[3].Text.ToString() + "/";
                                    S = S + gdstackingdetails.Rows[j].Cells[4].Text.ToString() + "/";
                                }

                                G = G.TrimEnd('/');
                                S = S.TrimEnd('/');       
                                Session["dt1"] = null;
                                gdstackingdetails.DataSource = null;
                                gdstackingdetails.DataBind();
                                Session["RefreshButton"] = "Yes";
                                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved successfully'); </script> ");
                                Session["RefreshButton"] = "No";
                            } ///here transactions ends
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please add Stack information first'); </script> ");
                        }

                    }//Try end
                    catch (Exception ex)
                    {
                        throw ;
                        lblmsg.Text = ex.Message.ToString();
                     //   Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data could not be saved Error in btnSave has occurred'); </script> ");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred'); </script> ");
                }
            }
            catch (Exception ex)
            {
                //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred'); </script> ");
                lblmsg.Text = ex.ToString();
            }
            finally
            {
                Con.Close();
                btnsave.Enabled = true;
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {

    }
}