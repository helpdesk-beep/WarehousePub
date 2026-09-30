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
using System.Resources;
using System.Globalization;
using System.Net;
using System.Net.Sockets;


public partial class WarehouseLevel_WLC_Procurement_GWLC_DepositorForm_WHR : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlTransaction sqltran = null;
    string Godown_Id = "";
    string Commodity_Id = "";
    string Depositor_Id = "";
    string Depositor_Name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null) && (Session["ReceiptId"] != null) && (Session["DepositorNo"] != null) && Session["GodownId"] != null && Session["G_BranchID"] != null)
        {
            try
            {
                btn_save.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to save this record , please be sure that you have entered correct data?');");
                if (Session["lang"].ToString() == "Hindi")
                {
                    lblDepositorFormhead.Text = Resources.hindi.lblDepositorFormhead;
                    lblInstruction.Text = Resources.hindi.lblInstruction;
                    lblDepositorName.Text = Resources.hindi.lblDepositorName;
                    lblCommodity.Text = Resources.hindi.lblCommodity;
                    lbltotalReceivedBags.Text = Resources.hindi.lbltotalReceivedBags;
                    lblTotalQuantityReceived.Text = Resources.hindi.lblTotalQuantityReceived;
                    lblWeighmentMode.Text = Resources.hindi.lblWeighmentMode;
                    lblWeightmentOn.Text = Resources.hindi.lblWeightmentOn;
                    lblAvgMoistureContent.Text = Resources.hindi.lblAvgMoistureContent;
                    lblLotNo.Text = Resources.hindi.lblLotNo;
                    lblMarketValue.Text = Resources.hindi.lblMarketValue;
                    lblSorcePfArrival.Text = Resources.hindi.lblSorcePfArrival;
                    lblWHRNumber.Text = Resources.hindi.lblWHRNumber;
                    lblWHRDate.Text = Resources.hindi.lblWHRDate;
                    lblDepositDate.Text = Resources.hindi.lblDepositDate;
                    lbl_To.Text = Resources.hindi.lbl_To;
                    Godown_Id = Session["GodownId"].ToString();
                    Depositor_Id = Session["DepositorId"].ToString();
                    Depositor_Name = Session["DepositorName"].ToString();
                    Commodity_Id = Session["CommodityId"].ToString();
                }

                if (!IsPostBack)
                {
                  // this Godown ID is after save record for previous page
                    Godown_Id = Session["GodownId"].ToString();

                    Depositor_Id = Session["DepositorId"].ToString();
                    Depositor_Name = Session["DepositorName"].ToString();
                    Commodity_Id = Session["CommodityId"].ToString();

                    lbl_whrno.Text = "";
                    showc.Visible = false;

                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }
                    fillDepositorType();
                    filldepositor();
                    FillddlSource();
                    Printcurrentdate();

                    fillCommodity();
                    fillGodnList();
                    fillCropYear();
                    Getrates();
                    fillCategory_non();
                    //Get Receipt Detail
                    GetReceiptDetail();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Some  error has occured! try again 79");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void Getrates()
    {
        try
        {
            string qry = "SELECT [Rate] FROM [Intergrated_MP_STORAGE].[dbo].[PurchaseRateMaster] where ComID='" + ddlcommodity.SelectedValue.ToString() + "' and CropYear='" + ddlcropyr.SelectedValue.ToString() + "'";
            cmd = new SqlCommand(qry, con);
            IDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtmarketvalue.Text = ds.Tables[0].Rows[0]["Rate"].ToString();


            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message.ToString();
        }
    }

    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            //string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  DepotId  ='" + Session["Depot_DepotID"].ToString() + "' order by Godown_Name Asc";
            string query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchId ='" + Session["G_BranchID"].ToString() + "' and Commodity_Id='" + ddlcommodity.SelectedValue + "')  ORDER BY [Godown_Name] Asc ";
            //string query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchId ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + ddlcommodity.SelectedValue + "') and Hired_Type in ('Owned','Hired') ORDER BY [Godown_Name] Asc ";
            //string query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from tbl_MetaData_STACK where BranchId ='" + Session["BranchId"].ToString() + "' and Commodity_Id='" + ddlcommodity.SelectedValue + "') and Hired_Type in ('Owned','Hired','JointVenture(JV)') and Godown_ID not in (select distinct Godown_Id from Pvt_Warehouse_Login where BranchID='" + Session["BranchId"].ToString() + "') ORDER BY [Godown_Name] Asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_godown.DataSource = ds.Tables[0];
                ddl_godown.DataTextField = "Godown_Name";
                ddl_godown.DataValueField = "Godown_ID";
                ddl_godown.DataBind();
                ddl_godown.Items.Insert(0, "--Select--");
            }
        }
    }

    //private void filldepositor()
    //{
    //    if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
    //    {
    //       // string query = "SELECT  distinct( tbl_Storage_Arrival_Stock.Depositor_Name)  as Depositor_Name  FROM tbl_Storage_Receipt_Details INNER JOIN tbl_Storage_Arrival_Stock ON tbl_Storage_Receipt_Details.StorageReceipt_Id = tbl_Storage_Arrival_Stock.Receipt_ID where tbl_Storage_Receipt_Details.Depotid ='" + Session["Depot_DepotID"].ToString() + "'  order by Depositor_Name";
    //        string query = "SELECT  distinct( DepositorName)  as Depositor_Name,[Depositor_ID]  FROM tbl_Storage_Receipt_Details left join  [tbl_MetaData_DEPOSITOR] on tbl_Storage_Receipt_Details.DepositorName=[tbl_MetaData_DEPOSITOR].Depositor_Name where tbl_Storage_Receipt_Details.BranchId ='" + Session["BranchId"].ToString() + "'  order by Depositor_Name";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddldepositorname.Items.Clear();
    //            ddldepositorname.DataSource = ds.Tables[0];
    //            ddldepositorname.DataTextField = "Depositor_Name";
    //            ddldepositorname.DataValueField = "Depositor_ID";
    //            ddldepositorname.DataBind();
    //            ddldepositorname.Items.Insert(0, "--Select--");
    //           // ddldepositorname.SelectedItem.Text = "MPSCSC";
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("~/SessionExpired.htm");
    //    }
    //}

    private void filldepositor()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
        {
            if (ddldepositortype.SelectedItem.Text == "Institution")
            {
                //For Institution
                string query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679','10535')";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ddldepositorname.DataSource = ds2;
                    ddldepositorname.DataTextField = "Depositor_Name";
                    ddldepositorname.DataValueField = "Depositor_ID";
                    ddldepositorname.DataBind();
                    ddldepositorname.Items.Insert(0, "--Select--");
                    ddldepositorname.SelectedValue = "129";
                }
                //For Institution

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name Asc";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            ddlcommodity.SelectedValue = "22";
        }
    }

    protected void FillddlSource()
    {
        try
        {
            //String query = "SELECT [Source_ID],[Source_Name] FROM [MPSCSC].[dbo].[Source_Arrival_Type] order by  [Source_ID]";         
            String query = " SELECT Source_ID,Source_Name FROM [MPSCSC].[dbo].[Source_Arrival_Type] UNION select 'BS' as Source_ID, 'Bhavantar Scheme' as Source_Name order by Source_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsource.Items.Clear();
                ddlsource.DataSource = ds.Tables[0];
                ddlsource.DataTextField = "Source_Name";
                ddlsource.DataValueField = "Source_ID";
                ddlsource.DataBind();
                //ddlsource.Items.Insert(0, "--Select--");
                ddlsource.SelectedValue = "01";
            }
            else
            {
                ddlsource.DataSource = null;
                ddlsource.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write("Some error has occured! try again 195");
        }
    }

    protected void Printcurrentdate()
    {
        String query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtwhrdate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            txtdepositdate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();

        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void gdtruckdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[12].Visible = false;
    }

    private void Empty()
    {
        try
        {
            txttotalbags.Text = null;
            txttotalweight.Text = null;
            txtmoisturecontent.Text = null;
            txtlotnumber.Text = null;
            txtmarketvalue.Text = null;
            txtwhrnumber.Text = null;
            gdtruckdetail.DataSource = null;
            gdtruckdetail.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("Some  error has occured! try again 255");
        }
    }

    protected void ddldepositorname_PreRender(object sender, EventArgs e)
    {
        try
        {
            int i;
            i = ddldepositorname.Items.Count;
            if (i <= 0)
            {
                lbl_message.Text = "There are no Depositor for which Depositor form has to be filled";
            }
        }
        catch (Exception ex)
        {
            Response.Write("Some  error has occured! try again 365");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";
    }

    protected void btnNewMC_Click(object sender, EventArgs e)
    {
        lbl_whrno.Text = "";
        showc.Visible = false;
        Session["RefreshButton"] = "No";
        Session["ReceiptGunny"] = null;
        Empty();
        Response.Redirect("~/IssueCenterLevel/Storage/DepositorForm.aspx");
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    public void whrnomax()
    {
        try
        {
            if (ddl_godown.SelectedValue != "0" || ddl_godown.SelectedItem.Text != "--Select--" || Session["GodownTypeId"].ToString() != null)
            {
                string GodownHTypeId = Session["GodownTypeId"].ToString();
                string GodownTypeId = GodownHTypeId.Trim().ToString();

                // from previous page godown id value asign in variable 
                string GodownId = Godown_Id;
                string WHR_Id = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //string QueryMax = "select isnull(Max(Did),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["G_BranchID"].ToString() + "' ";
                //string QueryMax = "select isnull(Max(Gid),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["G_BranchID"].ToString() + "' ";
                string QueryMax = "select isnull(Max(Gid),0)+1 from tbl_storage_Depositor_WHR_Relation where District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["G_BranchID"].ToString() + "' and GodownID='" + Session["GodownID_New"].ToString() + "'";

                cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
                string str3 = cmd.ExecuteScalar().ToString();
                if ((str3 == String.Empty) || str3 == "")
                {
                    str3 = "0";
                }
                if (Convert.ToInt64(str3) != 0)
                {
                    //string Depotid = Session["G_DepotID"].ToString();
                    WHR_Id = GodownId + GodownTypeId + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                }
                else
                {
                    string Depotid = Session["G_DepotID"].ToString();
                    WHR_Id = "";
                    WHR_Id = GodownId + GodownTypeId + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                }
                lbl_whrno.Text = WHR_Id.ToString();
                lbl_didid.Text = str3;
            }
            else
            {
                lbl_message.Text = "Try again!";
                return;
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            con.Close();
        }
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
       // this Godown id comes from after save record in PreviousPage page
        Godown_Id = Session["GodownId"].ToString();
        Depositor_Id = Session["DepositorId"].ToString();
        Depositor_Name = Session["DepositorName"].ToString();
        Commodity_Id = Session["CommodityId"].ToString();
        //Rabi 2019
        string GodownHTypeId = Session["GodownTypeId"].ToString();
        string GodownTypeId = GodownHTypeId.Trim().ToString();
        string Permission = "";
        if (GodownTypeId == "6" || GodownTypeId == "10")
        {
            Permission = CkAgreementCapacity();
        }
        if (ddlsource.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Source of Arrival...!')", true);
        }
        else if (txtmarketvalue.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Market Value...!')", true);
        }
        else if (TextBox1.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Storage(Rate) Starting Date...!')", true);
        }
        else if (Convert.ToDecimal(txtmoisturecontent.Text) <= 0 || Convert.ToDecimal(txtmoistcontent_To.Text) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Moisture Content...!')", true);
        }
        else if (Permission == "N" && (GodownTypeId == "6" || GodownTypeId == "10"))
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Can Not Deposit greater than Allowed Capacity...!')", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('JVS सिस्टम मे Updated Online Agreement(125%) के ऊपर की WHR नहीं बना सकते है! कृपया JVS System मे ऑनलाइन अग्रीमेंट अपडेट/चेक करे')", true);

        }
        else
        {
            string datestring = txtwhrdate.Text;
            string[] tempsplit = datestring.Split('/');
            string joinstring = "/";
            string newdate = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];
            string dateinus = new DateTime(Convert.ToInt16(tempsplit[2]), Convert.ToInt16(tempsplit[1]), Convert.ToInt16(tempsplit[0])).ToString("d", new CultureInfo("en-US"));
            string moisturecontent = "0";
            if (txtmoisturecontent.Text.Trim() == "")
            {
                moisturecontent = "0";
            }
            else
            {
                moisturecontent = txtmoisturecontent.Text.Trim();
            }
            string moisturecontent_to = "0";
            if (txtmoistcontent_To.Text == "")
            {
                moisturecontent_to = "0";
            }
            else
            {
                moisturecontent_to = txtmoistcontent_To.Text.Trim();
            }
            decimal marketvalue = 0;
            if (txtmarketvalue.Text.Trim() == "")
            {
                marketvalue = 0;
            }
            else
            {
                marketvalue = Convert.ToDecimal(txtmarketvalue.Text.Trim());
            }
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string BranchId = Session["G_BranchID"].ToString();
            ////Get Local IP
            //string Local_IP = "";
            //Local_IP = GetLocalIPAddress();
            whrnomax();
            if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null))
            {
                try
                {
                    for (int i = 0; i < gdtruckdetail.Rows.Count; i++)
                    {
                        if (((CheckBox)gdtruckdetail.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "Select  Isnull(Max(WHR_Id),'0') FROM tbl_Storage_Receipt_Details Where StorageReceipt_Id='" + gdtruckdetail.Rows[i].Cells[12].Text.ToString() + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            String StoredReceiptID = cmd.ExecuteScalar().ToString();
                            con.Close();
                            if (StoredReceiptID != "0")
                            {
                                lbl_message.Text = "The Receipt Already Saved.!Do Not Refresh or Try again!";
                                return;
                            }
                        }
                    }
                    if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                    {
                        Response.Redirect("WLC_DepositorForm_WHR.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");
                    }
                    else if (lbl_whrno.Text == "")
                    {
                        lbl_message.Text = "The WHR No is Required..";
                    }
                    else
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string Whr_No = lbl_whrno.Text;
                        sqltran = con.BeginTransaction();
                        string Query = "select count(Whr_No) from tbl_storage_Depositor_WHR_Relation where Whr_No = '" + Whr_No + "' and District_Id='" + Session["Depot_DistID"].ToString() + "' and BranchId='" + Session["G_BranchID"].ToString() + "' ";
                        cmd = new SqlCommand(Query, con, sqltran); // check WhrId present in whr_status table
                        string str2 = cmd.ExecuteScalar().ToString();

                        //-----------------------------------
                        if (Convert.ToInt64(str2) == 0) ////not found than insert else update
                        {
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            string Crop_Year = "";
                            if (Commodity_Id == "22")
                            {
                                Crop_Year = "2019-20";
                            }
                            else if (Commodity_Id == "63" || Commodity_Id == "64" || Commodity_Id == "33" || Commodity_Id == "52" || Commodity_Id == "13"|| Commodity_Id == "11" || Commodity_Id == "8")
                            {
                                  Crop_Year = "2018-19";
                                //Crop_Year = "2019-20";
                            }
                            else
                            {
                                Crop_Year = "2018-19";
                            }
                            //string queryy = "insert into tbl_storage_Depositor_WHR_Relation(Depositor_WHR_Id,District_Id ,DepotId ,Commodity_Id ,Category_Id  ,Whr_No ,Depositor_Name ,Date_of_Deposit,TotalBags_Received ,Total_Qty_Received ,Mode_of_weighment  ,BeamScale_LWB  ,AvgMoisture_Content ,Lot_No , MktValue_of_Commodity ,Arrival_Source ,WHR_Issue_Date ,state_id,AvgMoisture_Content_To,Did,CropYear,Remark,SangrahadDate,wday,wmon,wyear,BranchId,Client_IP,DepositorID,GodownID)values ('" + lbl_whrno.Text + "','" + Session["Depot_DistID"].ToString() + "' ,'" + Session["Depot_DepotID"].ToString() + "' ,'" + ddlcommodity.SelectedValue.ToString().Trim() + "' ,'" + ddlCategory_Non.SelectedValue.ToString() + "'  ,'" + lbl_whrno.Text + "' ,'" + ddldepositorname.SelectedItem.Text.ToString() + "' ,'" + getDate_MDY(txtdepositdate.Text.Trim()) + "','" + Convert.ToInt32(txttotalbags.Text.Trim()) + "' ,'" + Convert.ToDecimal(txttotalweight.Text.Trim()) + "' ,'" + ddlmode.SelectedItem.Text.Trim().ToString() + "'  ,'" + ddlweighon.SelectedItem.Text.Trim().ToString() + "'  ,'" + moisturecontent + "'  , '" + null + "' ,'" + marketvalue + "' ,'" + ddlsource.SelectedValue.ToString().Trim() + "' ,'" + dateinus + "' ,'23','" + moisturecontent_to + "' ,'" + lbl_didid.Text + "','" + ddlcropyr.SelectedItem.Text + "',N'" + TextBox2.Text.Trim().ToString() + "','" + TextBox1.Text + "','" + Convert.ToInt16(tempsplit[0]) + "','" + Convert.ToInt16(tempsplit[1]) + "','" + Convert.ToInt16(tempsplit[2]) + "','" + BranchId + "','" + ClientIP + "','" + ddldepositorname.SelectedValue + "','" + ddl_godown.SelectedValue.ToString() + "')";
                            //string queryy = "insert into tbl_storage_Depositor_WHR_Relation(Depositor_WHR_Id,District_Id ,DepotId ,Commodity_Id ,Category_Id  ,Whr_No ,Depositor_Name ,Date_of_Deposit,TotalBags_Received ,Total_Qty_Received ,Mode_of_weighment  ,BeamScale_LWB  ,AvgMoisture_Content ,Lot_No , MktValue_of_Commodity ,Arrival_Source ,WHR_Issue_Date ,state_id,AvgMoisture_Content_To,Did,CropYear,Remark,SangrahadDate,wday,wmon,wyear,BranchId,Client_IP,DepositorID,Gid,GodownID)values ('" + lbl_whrno.Text + "','" + Session["Depot_DistID"].ToString() + "' ,'" + Session["G_DepotID"] + "' ,'" + Commodity_Id + "' ,'" + ddlCategory_Non.SelectedValue.ToString() + "'  ,'" + lbl_whrno.Text + "' ,N'" + Session["DepositorName"].ToString() + "' ,'" + getDate_MDY(txtwhrdate.Text.Trim()) + "','" + Convert.ToInt32(txttotalbags.Text.Trim()) + "' ,'" + Convert.ToDecimal(txttotalweight.Text.Trim()) + "' ,'" + ddlmode.SelectedItem.Text.Trim().ToString() + "'  ,'" + ddlweighon.SelectedItem.Text.Trim().ToString() + "'  ,'" + moisturecontent + "'  , '" + null + "' ,'" + marketvalue + "' ,'" + ddlsource.SelectedValue.ToString().Trim() + "' ,'" + dateinus + "' ,'23','" + moisturecontent_to + "' ,'" + lbl_didid.Text + "','2018-19',N'" + TextBox2.Text.Trim().ToString() + "','" + TextBox1.Text + "','" + Convert.ToInt16(tempsplit[0]) + "','" + Convert.ToInt16(tempsplit[1]) + "','" + Convert.ToInt16(tempsplit[2]) + "','" + BranchId + "','" + ClientIP + "','" + Session["DepositorId"].ToString() + "','" + lbl_didid.Text + "','" + Godown_Id.ToString() + "')";
                            string queryy = "insert into tbl_storage_Depositor_WHR_Relation(Depositor_WHR_Id,District_Id ,DepotId ,Commodity_Id ,Category_Id  ,Whr_No ,Depositor_Name ,Date_of_Deposit,TotalBags_Received ,Total_Qty_Received ,Mode_of_weighment  ,BeamScale_LWB  ,AvgMoisture_Content ,Lot_No , MktValue_of_Commodity ,Arrival_Source ,WHR_Issue_Date ,state_id,AvgMoisture_Content_To,Did,CropYear,Remark,SangrahadDate,wday,wmon,wyear,BranchId,Client_IP,DepositorID,Gid,GodownID)values ('" + lbl_whrno.Text + "','" + Session["Depot_DistID"].ToString() + "' ,'" + Session["G_DepotID"] + "' ,'" + Commodity_Id + "' ,'" + ddlCategory_Non.SelectedValue.ToString() + "'  ,'" + lbl_whrno.Text + "' ,N'" + Session["DepositorName"].ToString() + "' ,'" + getDate_MDY(txtwhrdate.Text.Trim()) + "','" + Convert.ToInt32(txttotalbags.Text.Trim()) + "' ,'" + Convert.ToDecimal(txttotalweight.Text.Trim()) + "' ,'" + ddlmode.SelectedItem.Text.Trim().ToString() + "'  ,'" + ddlweighon.SelectedItem.Text.Trim().ToString() + "'  ,'" + moisturecontent + "'  , '" + null + "' ,'" + marketvalue + "' ,'" + ddlsource.SelectedValue.ToString().Trim() + "' ,'" + dateinus + "' ,'23','" + moisturecontent_to + "' ,'" + lbl_didid.Text + "','"+ Crop_Year +"',N'" + TextBox2.Text.Trim().ToString() + "','" + TextBox1.Text + "','" + Convert.ToInt16(tempsplit[0]) + "','" + Convert.ToInt16(tempsplit[1]) + "','" + Convert.ToInt16(tempsplit[2]) + "','" + BranchId + "','" + ClientIP + "','" + Session["DepositorId"].ToString() + "','" + lbl_didid.Text + "','" + Godown_Id.ToString() + "')";


                            cmd = new SqlCommand(queryy, con, sqltran);
                            cmd.CommandType = CommandType.Text;

                            int res = cmd.ExecuteNonQuery();

                            // updating the WHR Number in the Receipt details table 
                            if (res > 0)
                            {
                                int i;
                                for (i = 0; i < gdtruckdetail.Rows.Count; i++)
                                {
                                    if (((CheckBox)gdtruckdetail.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                                    {
                                        cmd = new SqlCommand("sp_updatewhrid_receiptdetails", con, sqltran);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@StorageReceipt_Id", gdtruckdetail.Rows[i].Cells[12].Text.ToString());
                                        cmd.Parameters.AddWithValue("@WHR_Id", lbl_whrno.Text);
                                        cmd.Connection = con;
                                        int w = cmd.ExecuteNonQuery();
                                    }
                                }
                                //////to insert in whrsprintstatus..//////////////////////////////////
                                string strw = "insert into [dbo].[whrprintstatus] (WHRID,[PrintStatus],[DateCreated],[IsActive]) values ('" + lbl_whrno.Text + "','1st',Getdate(),'Yes') ";
                                cmd = new SqlCommand(strw, con, sqltran);
                                int res2whr = cmd.ExecuteNonQuery();
                                /////////////////////////////////////////////////Update Status in Table Whr_Status ////////////////

                                string Status = "Select count(WhrId) from whr_status where WhrId = '" + lbl_whrno.Text + "'";
                                cmd = new SqlCommand(Status, con, sqltran); // check WhrId present in whr_status table
                                string str1 = cmd.ExecuteScalar().ToString();
                                if (Convert.ToInt16(str1) == 0) ////not found than insert else update
                                {
                                    string str = "Insert Into whr_status(WhrId,Statusflag) values('" + lbl_whrno.Text + "','N')";
                                    cmd = new SqlCommand(str, con, sqltran);
                                    int req = cmd.ExecuteNonQuery();
                                    if (req > 0)
                                    {
                                        //Empty();
                                        Session["RefreshButton"] = "Yes";
                                        btnNewMC.Visible = true;
                                        btnNewMC.Enabled = true;
                                        btn_save.Enabled = false;
                                        lbl_message.Text = "WHR record saved successfully";
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR record saved successfully!')", true);
                                        showc.Visible = true;
                                        sqltran.Commit();
                                        con.Close();
                                        ///////Inser LatLong//////////////
                                        //if (ViewState["Latitude"].ToString() != "" && ViewState["Longitude"].ToString() != "" && ViewState["Latitude"].ToString() != null && ViewState["Longitude"].ToString() != null)
                                        //{
                                        //    GetLatiLongi();
                                        //}
                                        ///////Finish LatLong//////////////
                                    }
                                    else
                                    {
                                        lbl_message.Text = "WHR record saved successfully";
                                    }
                                }
                                else
                                {
                                    string str = "Update whr_status Set Statusflag = 'N' where WhrId = '" + lbl_whrno.Text + "'";
                                    cmd = new SqlCommand(str, con, sqltran);
                                    int res2 = cmd.ExecuteNonQuery();
                                    if (res2 > 0)
                                    {
                                        //Empty();
                                        Session["RefreshButton"] = "Yes";
                                        btnNewMC.Visible = true;
                                        btnNewMC.Enabled = true;
                                        showc.Visible = true;
                                        btn_save.Enabled = false;
                                        lbl_message.Text = "WHR record saved successfully";
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR record saved successfully!')", true);
                                        showc.Visible = true;
                                        sqltran.Commit();
                                        con.Close();

                                        
                                    }
                                    else
                                    {
                                        lbl_message.Text = "Record Not save...";
                                    }
                                }
                                //Wheat Web Service 2019
                                //if (ddlcropyr.SelectedItem.Text == "2019-20")
                                //{
                                //    int j;
                                //    for (j = 0; j < gdtruckdetail.Rows.Count; j++)
                                //    {
                                //        string DFNo = gdtruckdetail.Rows[j].Cells[4].Text.ToString();
                                //        string Commodity="22";
                                //        string TC= gdtruckdetail.Rows[j].Cells[1].Text.ToString();

                                //        UpdateDepositerforWHRWheat2019.UpdateWHR_Wheat2019 wwhrupdate = new UpdateDepositerforWHRWheat2019.UpdateWHR_Wheat2019();
                                //        wwhrupdate.Insert_WHR_CSMS(DFNo, Session["Depot_DistID"].ToString(), lbl_whrno.Text, getDate_MDY(txtwhrdate.Text), Commodity, TC);
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            lbl_message.Text = "The WHR No already exist.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    sqltran.Rollback();
                    lbl_message.Text = ex.Message;
                    Response.Write("Some  error has occured! try again 883");
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
  
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
    }
    private void GetReceiptDetail()
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["G_DepotID"] != null) && (Session["ReceiptId"] != null) && (Session["DepositorNo"] != null))
            {
                gdtruckdetail.DataSource = null;
                gdtruckdetail.DataBind();
                {

                    //string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,5),SRD.Qty_Rvd_Weight) AS Weight,SRD.Mode_of_weighment,ISNULL(SRD.BeamScale_LWB,'---')as Beamscale,ISNULL(AST.Quality_Moisture,'0.0') AS Moisture,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,convert(varchar(10),SRD.Receipt_Date,103) as 'Receipt_Date' from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_storage_Stacking_Details AS SSD ON SRD.StorageReceipt_Id = SSD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '" + Session["BranchId"].ToString() + "' and AST.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' AND AST.Depositor_Name = '" + ddldepositorname.SelectedItem.Text.ToString() + "' and SSD.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' and convert(nvarchar(10),AST.DepositDate,103)=convert(nvarchar(10),'" + txtdepositdate.Text.Trim().ToString() + "',103)";
                    string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,5),SRD.Qty_Rvd_Weight) AS Weight,SRD.Mode_of_weighment,ISNULL(SRD.BeamScale_LWB,'---')as Beamscale,ISNULL(AST.Quality_Moisture,'0.0') AS Moisture,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,convert(varchar(10),SRD.Receipt_Date,103) as 'Receipt_Date',SRD.Remarks,SRD.Commodity_Id from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_storage_Stacking_Details AS SSD ON SRD.StorageReceipt_Id = SSD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '" + Session["G_BranchID"].ToString() + "' and SRD.Acpt_FCIRO_No='" + Session["DepositorNo"].ToString() + "' and SRD.StorageReceipt_Id='" + Session["ReceiptId"].ToString() + "'";
                    string Commodity = "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdtruckdetail.DataSource = ds.Tables[0];
                        gdtruckdetail.DataBind();
                        lblRowCount.Text = "Total No. of Receipts are - " + gdtruckdetail.Rows.Count.ToString();
                        //--------------------------------------------------------------
                        CheckBox cb = (CheckBox)gdtruckdetail.Rows[0].FindControl("ckboxtrucklist");
                        cb.Checked = true;
                        if (cb.Checked == true)
                        {
                            int bagvalue = 0;
                            decimal wtvalue = 0.00M;
                            int i;
                            for (i = 0; i < gdtruckdetail.Rows.Count; i++)
                            {
                                if (((CheckBox)gdtruckdetail.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                                {
                                    bagvalue = bagvalue + Convert.ToInt32(gdtruckdetail.Rows[i].Cells[6].Text.ToString());
                                    wtvalue = wtvalue + Convert.ToDecimal(gdtruckdetail.Rows[i].Cells[7].Text.ToString());
                                    Commodity = gdtruckdetail.Rows[i].Cells[3].Text.ToString();
                                }
                            }

                            //Add rate
                            if (Commodity == "GRAM")
                            {
                                txtmarketvalue.Text = "5082";
                            }
                            else if (Commodity == "LENTIL")
                            {
                                txtmarketvalue.Text = "4923";
                            }
                            else if (Commodity == "Mustard-Sarason")
                            {
                                txtmarketvalue.Text = "4620";
                            }
                            else if (Commodity == "Arahar")
                            {
                                txtmarketvalue.Text = "6242";
                            }
                            //
                            txtmoisturecontent.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txtmoistcontent_To.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txttotalbags.Text = bagvalue.ToString();
                            txttotalweight.Text = wtvalue.ToString();
                            txtwhrdate.Text = ds.Tables[0].Rows[0]["AcceptDate"].ToString();
                            TextBox1.Text = ds.Tables[0].Rows[0]["AcceptDate"].ToString();
                            //TextBox2.Text = "Moisture :"+ds.Tables[0].Rows[0]["Moisture"].ToString()+","+ds.Tables[0].Rows[0]["Remarks"].ToString();
                            TextBox2.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                            //Commodity_Id = ds.Tables[0].Rows[0]["Commodity_Id"].ToString();

                            if (ddlcommodity.SelectedValue.ToString() == "25" || ddlcommodity.SelectedValue.ToString() == "29" || ddlcommodity.SelectedValue.ToString() == "96" || ddlcommodity.SelectedValue.ToString() == "97")
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = true;
                                }
                            }
                            else
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = false;
                                }
                            }
                        }
                        lbl_message.Text = "";
                    }
                    else
                    {
                        gdtruckdetail.DataSource = null;
                        gdtruckdetail.DataBind();
                        btn_save.Enabled = false;
                        txttotalbags.Text = "";
                        txttotalweight.Text = "";
                        txtmoisturecontent.Text = "";
                        txtmoistcontent_To.Text = "";
                        lbl_message.Text = "No records found";
                    }
                    con.Close();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                gdtruckdetail.DataSource = null;
                gdtruckdetail.DataBind();
                if (ddldepositorname.Items.Count == 0)
                {
                    lbl_message.Text = "Please fill correct Deposotor & From";
                    return;
                }
                else
                {

                    string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,5),SRD.Qty_Rvd_Weight) AS Weight,SRD.Mode_of_weighment,ISNULL(SRD.BeamScale_LWB,'---')as Beamscale,ISNULL(AST.Quality_Moisture,'0.0') AS Moisture,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,convert(varchar(10),SRD.Receipt_Date,103) as 'Receipt_Date' from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_storage_Stacking_Details AS SSD ON SRD.StorageReceipt_Id = SSD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '" + Session["BranchId"].ToString() + "' and AST.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' AND AST.Depositor_Name = '" + ddldepositorname.SelectedItem.Text.ToString() + "' and SSD.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' and convert(nvarchar(10),AST.DepositDate,103)=convert(nvarchar(10),'" + txtdepositdate.Text.Trim().ToString() + "',103)";
                    // string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,5),SRD.Qty_Rvd_Weight) AS Weight,SRD.Mode_of_weighment,ISNULL(SRD.BeamScale_LWB,'---')as Beamscale,ISNULL(AST.Quality_Moisture,'0.0') AS Moisture,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,convert(varchar(10),SRD.Receipt_Date,103) as 'Receipt_Date' from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_storage_Stacking_Details AS SSD ON SRD.StorageReceipt_Id = SSD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '2320007' and AST.Commodity_Id = '22' ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdtruckdetail.DataSource = ds.Tables[0];
                        gdtruckdetail.DataBind();
                        lblRowCount.Text = "Total No. of Receipts are - " + gdtruckdetail.Rows.Count.ToString();
                        //--------------------------------------------------------------
                        CheckBox cb = (CheckBox)gdtruckdetail.Rows[0].FindControl("ckboxtrucklist");
                        cb.Checked = true;
                        if (cb.Checked == true)
                        {
                            int bagvalue = 0;
                            decimal wtvalue = 0.00M;
                            int i;
                            for (i = 0; i < gdtruckdetail.Rows.Count; i++)
                            {
                                if (((CheckBox)gdtruckdetail.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                                {
                                    bagvalue = bagvalue + Convert.ToInt32(gdtruckdetail.Rows[i].Cells[6].Text.ToString());
                                    wtvalue = wtvalue + Convert.ToDecimal(gdtruckdetail.Rows[i].Cells[7].Text.ToString());
                                }
                            }
                            txtmoisturecontent.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txtmoistcontent_To.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txttotalbags.Text = bagvalue.ToString();
                            txttotalweight.Text = wtvalue.ToString();

                            if (ddlcommodity.SelectedValue.ToString() == "25" || ddlcommodity.SelectedValue.ToString() == "29" || ddlcommodity.SelectedValue.ToString() == "96" || ddlcommodity.SelectedValue.ToString() == "97")
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = true;
                                }
                            }
                            else
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = false;
                                }
                            }
                        }
                        lbl_message.Text = "";
                    }
                    else
                    {
                        gdtruckdetail.DataSource = null;
                        gdtruckdetail.DataBind();
                        btn_save.Enabled = false;
                        txttotalbags.Text = "";
                        txttotalweight.Text = "";
                        txtmoisturecontent.Text = "";
                        txtmoistcontent_To.Text = "";
                        lbl_message.Text = "No records found";
                    }
                    con.Close();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlcropyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                gdtruckdetail.DataSource = null;
                gdtruckdetail.DataBind();
                if (ddldepositorname.Items.Count == 0)
                {
                    lbl_message.Text = "Please fill correct Deposotor & From";
                    return;
                }
                else
                {

                    string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,AST.Challan_No,AST.Truck_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,5),SRD.Qty_Rvd_Weight) AS Weight,SRD.Mode_of_weighment,ISNULL(SRD.BeamScale_LWB,'---')as Beamscale,ISNULL(AST.Quality_Moisture,'0.0') AS Moisture,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate,convert(varchar(10),SRD.Receipt_Date,103) as 'Receipt_Date' from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_storage_Stacking_Details AS SSD ON SRD.StorageReceipt_Id = SSD.StorageReceipt_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchId = '" + Session["BranchId"].ToString() + "' and AST.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' AND AST.Depositor_Name = '" + ddldepositorname.SelectedItem.Text.ToString() + "' and SSD.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' and convert(nvarchar(10),AST.DepositDate,103)=convert(nvarchar(10),'" + txtdepositdate.Text.Trim().ToString() + "',103) and AST.Crop_Year='" + ddlcropyr.SelectedItem.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gdtruckdetail.DataSource = ds.Tables[0];
                        gdtruckdetail.DataBind();
                        lblRowCount.Text = "Total No. of Receipts are - " + gdtruckdetail.Rows.Count.ToString();
                        //--------------------------------------------------------------
                        CheckBox cb = (CheckBox)gdtruckdetail.Rows[0].FindControl("ckboxtrucklist");
                        cb.Checked = true;
                        if (cb.Checked == true)
                        {
                            int bagvalue = 0;
                            decimal wtvalue = 0.00M;
                            int i;
                            for (i = 0; i < gdtruckdetail.Rows.Count; i++)
                            {
                                if (((CheckBox)gdtruckdetail.Rows[i].FindControl("ckboxtrucklist")).Checked == true)
                                {
                                    bagvalue = bagvalue + Convert.ToInt32(gdtruckdetail.Rows[i].Cells[6].Text.ToString());
                                    wtvalue = wtvalue + Convert.ToDecimal(gdtruckdetail.Rows[i].Cells[7].Text.ToString());
                                }
                            }
                            txtmoisturecontent.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txtmoistcontent_To.Text = ds.Tables[0].Rows[0]["Moisture"].ToString();
                            txttotalbags.Text = bagvalue.ToString();
                            txttotalweight.Text = wtvalue.ToString();
                            if (ddlcommodity.SelectedValue.ToString() == "25" || ddlcommodity.SelectedValue.ToString() == "29" || ddlcommodity.SelectedValue.ToString() == "96" || ddlcommodity.SelectedValue.ToString() == "97")
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = true;
                                }
                            }
                            else
                            {
                                if (wtvalue > 0)
                                {
                                    btn_save.Enabled = true;
                                }
                                else
                                {

                                    btn_save.Enabled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        gdtruckdetail.DataSource = null;
                        gdtruckdetail.DataBind();
                        btn_save.Enabled = false;
                        txttotalbags.Text = "";
                        txttotalweight.Text = "";
                        txtmoisturecontent.Text = "";
                        txtmoistcontent_To.Text = "";
                        lbl_message.Text = "No records found";
                    }
                    con.Close();
                    Getrates();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
        }
        catch (Exception ex)
        {

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
        ddlcropyr.Items.Insert(0, "All");
        ddlcropyr.SelectedIndex = 1;
        //ddlcropyr.SelectedIndex = 2;
        ddlcropyr.Items.AddRange(items);
        ddlcropyr.DataBind();
    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                fillGodnList();
                gdtruckdetail.DataSource = null;
                gdtruckdetail.DataBind();
                Getrates();

            }
        }
        catch (Exception ex)
        {

        }
    }
    private void fillCategory_non()
    {
        try
        {
            string query = "SELECT Category_Id, Category_Name FROM tbl_MetaData_STORAGE_CATEGORY";
            SqlCommand cmd = new SqlCommand(query, con);
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
    public void GetLatiLongi()
    {
        try
        {
            string qry = "";
            //string Trans_Type = "WHR";
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            string latitude = ViewState["Latitude"].ToString();
            string longitude = ViewState["Longitude"].ToString();

            if (latitude != "" && longitude != "" && latitude != null && longitude != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Transactional_LatLong]([Trans_Id],[Latitude],[Longitude],[Trans_Type],[Created_Date],[Client_IP]) VALUES ('" + lbl_whrno.Text + "','" + latitude + "','" + longitude + "','" + Trans_Type + "',getdate(),'" + ClientIP + "')";
                qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_Deposit_WHR_LatLong]([WHR_Id],[Latitude],[Longitude],[Created_Date],[Client_IP]) VALUES ('" + lbl_whrno.Text + "','" + latitude + "','" + longitude + "',getdate(),'" + ClientIP + "')";

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
    protected void ddldepositorname_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["Latitude"] = (this.Request.Form.Get("n_lati"));
        //ViewState["Longitude"] = (this.Request.Form.Get("n_long"));
        fillGodnList();
        gdtruckdetail.DataSource = null;
        gdtruckdetail.DataBind();
        Getrates();
    }
    protected void fillDepositorType()
    {
        try
        {
            string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Report_Seq_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositortype.DataSource = ds.Tables[0];
                ddldepositortype.DataTextField = "Depositor_Type";
                ddldepositortype.DataValueField = "Depositor_Type";
                ddldepositortype.DataBind();
                ddldepositortype.Items.Insert(0, "--Select--");
            }

            if (ddldepositortype.Items.Count > 0)
            {
                for (int d = 0; d < ddldepositortype.Items.Count; d++)
                {
                    if (ddldepositortype.Items[d].Text == "Institution")
                    {
                        ddldepositortype.Items[d].Selected = true;
                    }
                }
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddldepositorname.Items.Clear();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand();
                DataSet ds1 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.Connection = con;
                cmd = new SqlCommand("sp_getDepositor_Depo_wise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 20);
                cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
                cmd.Parameters.Add("@depot_id", SqlDbType.VarChar, 20);
                cmd.Parameters["@depot_id"].Value = Session["BranchId"].ToString();
                int Res = cmd.ExecuteNonQuery();
                da.SelectCommand = cmd;
                da.Fill(ds1, "temp");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    if (ddldepositortype.SelectedItem.Text == "Institution")
                    {
                        //For Institution
                        string query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','181','184','4679')";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2);
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            ddldepositorname.DataSource = ds2;
                            ddldepositorname.DataTextField = "Depositor_Name";
                            ddldepositorname.DataValueField = "Depositor_ID";
                            ddldepositorname.DataBind();
                            ddldepositorname.Items.Insert(0, "--Select--");
                            ddldepositorname.SelectedValue = "129";
                        }
                        //For Institution

                    }
                    else
                    {
                        ddldepositorname.DataSource = ds1;
                        ddldepositorname.DataTextField = "Depositor_Name";
                        ddldepositorname.DataValueField = "Depositor_ID";
                        ddldepositorname.DataBind();
                        ddldepositorname.Items.Insert(0, "--Select--");
                        // ddlDepositor.SelectedValue = "129";

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Depositor Found!')", true);

                    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depositor Found!'); </script> ");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }
    public string CkAgreementCapacity()
    {
        string Action_Type = "N";
        decimal AgreementCapacity = 0;
        decimal Agreementbalance = 0;
        decimal TenPerAgr = 0;
        decimal WHR_Req_Weight = (Convert.ToDecimal(txttotalweight.Text) / 10);
        //string QryAC = "select ISNULL(SUM(AgreementCapacity),0) as AgreementCapacity from tbl_MetaData_GODOWN_JV_Agreement where RegNumber='" + Session["WLC_Reg_No"].ToString() + "'";
        //string QryAC = "select ISNULL(SUM(Agree_Capacity),0) as AgreementCapacity from tbl_Agreemented_Godown_Mapping where Registration_Id='" + Session["WLC_Reg_No"].ToString() + "'";
        string QryAC = "select ISNULL(SUM(Agree_Capacity),0) as AgreementCapacity from tbl_Agreemented_Godown_Mapping_2019 where Registration_Id='" + Session["WLC_Reg_No"].ToString() + "'";


        SqlDataAdapter da = new SqlDataAdapter(QryAC, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            AgreementCapacity = Convert.ToDecimal(dr["AgreementCapacity"]);
            TenPerAgr = (AgreementCapacity * 25) / 100;
            AgreementCapacity = AgreementCapacity + TenPerAgr;
            if (AgreementCapacity > 0)
            {
                //string QryBalance = "select ISNULL(SUM(WHR.Total_Qty_Received/10),0) as WHR_Qty from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.GodownID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.W_Reg_Id='" + Session["WLC_Reg_No"].ToString() + "' and P.BranchID='" + Session["G_BranchID"].ToString() + "') and WHR.CropYear='2018-19' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and WHR.Arrival_Source='01' and Commodity_Id='22' and CONVERT(varchar(10),WHR.WHR_Issue_Date,101)>='03/15/2018'";
                //string QryBalance = "select ISNULL(SUM(WHR.Total_Qty_Received/10),0) as WHR_Qty from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.GodownID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.W_Reg_Id='" + Session["WLC_Reg_No"].ToString() + "' and P.BranchID='" + Session["G_BranchID"].ToString() + "') and WHR.CropYear='2018-19' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and CONVERT(varchar(10),WHR.WHR_Issue_Date,101)>='03/15/2018'";
                //string QryBalance = "select ISNULL(SUM(WHR.Total_Qty_Received/10),0) as WHR_Qty from tbl_storage_Depositor_WHR_Relation as WHR  where WHR.GodownID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.W_Reg_Id='" + Session["WLC_Reg_No"].ToString() + "' and P.BranchID='" + Session["G_BranchID"].ToString() + "') and WHR.CropYear='2018-19' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and CONVERT(varchar(10),WHR.WHR_Issue_Date,101)>='03/15/2018' and WHR.Arrival_Source='01'";
                //string QryBalance = "select ISNULL(SUM((WHR.RecQty-WHR.DelQty)/10),0) as WHR_Qty from View_WHRcurrentstock as WHR  where WHR.Godown_ID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.W_Reg_Id='" + Session["WLC_Reg_No"].ToString() + "' and P.BranchID='" + Session["G_BranchID"].ToString() + "') and WHR.CropYear='2018-19' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and CONVERT(varchar(10),WHR.WHR_Issue_Date,101)>='03/15/2018'";
                //string QryBalance = "select isnull(SUM(RecQty)-ISNULL(SUM(DelQty),0),0)/10 as WHR_Qty from (SELECT WHR.GodownID,WHR.Depositor_WHR_Id,TSS.Stack_ID,SUM(TSS.Bags) as RecBags,SUM(TSS.Weight) as RecQty FROM tbl_storage_Stacking_Details as TSS inner join tbl_storage_Depositor_WHR_Relation as WHR on TSS.WHRId = WHR.Depositor_WHR_Id where WHR_Issue_Date>=CONVERT(varchar(10),'03/15/2018',101) and WHR.CropYear='2018-19' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and WHR.GodownID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.W_Reg_Id='" + Session["WLC_Reg_No"].ToString() + "' and P.BranchID='" + Session["G_BranchID"].ToString() + "') group by WHR.GodownID,WHR.Depositor_WHR_Id,TSS.Stack_ID ) as RecDetail left join (SELECT dsdg.Godown_ID,dsdg.Depositor_WHR_Id,dsdg.Stack_ID,isnull(SUM(No_Of_Bags),0) as DelBags,isnull((SUM(Bags_Weight) - SUM(Gain)) + sum(Loss), 0) AS DelQty FROM tbl_Delivery_Stacking_Details_GatePass AS dsdg INNER JOIN tbl_Storage_GatePass_Enrty AS sge ON dsdg.GatePass_No = sge.GatePass_No WHERE sge.Status != 'Cancel' group by dsdg.Godown_ID,dsdg.Depositor_WHR_Id,dsdg.Stack_ID ) as DelDetails on RecDetail.GodownID=DelDetails.Godown_ID and RecDetail.Stack_ID=DelDetails.Stack_ID and RecDetail.Depositor_WHR_Id=DelDetails.Depositor_WHR_Id";
                string QryBalance = "select isnull(SUM(RecQty)-ISNULL(SUM(DelQty),0),0)/10 as WHR_Qty from (SELECT WHR.GodownID,WHR.Depositor_WHR_Id,TSS.Stack_ID,SUM(TSS.Bags) as RecBags,SUM(TSS.Weight) as RecQty FROM tbl_storage_Stacking_Details as TSS inner join tbl_storage_Depositor_WHR_Relation as WHR on TSS.WHRId = WHR.Depositor_WHR_Id where WHR_Issue_Date>=CONVERT(varchar(10),'03/15/2019',101) and WHR.CropYear='2019-20' and WHR.BranchID='" + Session["G_BranchID"].ToString() + "' and WHR.GodownID in (select distinct P.Godown_Id from Pvt_Warehouse_Login as P where P.Is_W19_RegID='" + Session["WLC_Reg_No"].ToString() + "' and P.Is_W19_Agreement='Y' and  P.BranchID='" + Session["G_BranchID"].ToString() + "') group by WHR.GodownID,WHR.Depositor_WHR_Id,TSS.Stack_ID ) as RecDetail left join (SELECT dsdg.Godown_ID,dsdg.Depositor_WHR_Id,dsdg.Stack_ID,isnull(SUM(No_Of_Bags),0) as DelBags,isnull((SUM(Bags_Weight) - SUM(Gain)) + sum(Loss), 0) AS DelQty FROM tbl_Delivery_Stacking_Details_GatePass AS dsdg INNER JOIN tbl_Storage_GatePass_Enrty AS sge ON dsdg.GatePass_No = sge.GatePass_No WHERE sge.Status != 'Cancel' group by dsdg.Godown_ID,dsdg.Depositor_WHR_Id,dsdg.Stack_ID ) as DelDetails on RecDetail.GodownID=DelDetails.Godown_ID and RecDetail.Stack_ID=DelDetails.Stack_ID and RecDetail.Depositor_WHR_Id=DelDetails.Depositor_WHR_Id";


                SqlDataAdapter da2 = new SqlDataAdapter(QryBalance, con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    DataRow dr2 = ds2.Tables[0].Rows[0];
                    Agreementbalance = Convert.ToDecimal(dr2["WHR_Qty"]);
                }
                if (Agreementbalance < AgreementCapacity && WHR_Req_Weight <= (AgreementCapacity - Agreementbalance))
                {
                    Action_Type = "Y";
                }
                else
                {
                    Action_Type = "N";
                }
            }
        }
        return Action_Type;
    }
}
