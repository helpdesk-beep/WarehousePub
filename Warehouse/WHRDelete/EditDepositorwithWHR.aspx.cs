using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Data;
using DataAccess;
using System.Data.SqlClient;
using System.Text;


public partial class IssueCenterLevel_Storage_EditDepositorwithWHR : System.Web.UI.Page
{
    SqlConnection _sqlCon = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    protected Common ComObj = null;
    public string qry = "";
    DataReader drObj = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            ComObj = new Common(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
            try
            {
                if (!IsPostBack)
                {
                    panelContainer.Visible = false;
                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + PopMsg + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                    }
                    fillDistrict();
                    ImageButton1.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
                }
            }

            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
            finally
            {
                if (_sqlCon.State == ConnectionState.Open)
                {
                    _sqlCon.Close();
                }
            }
        }
        else
        {
            Response.Redirect("~/Logout.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    private void getDepot(string distId)
    {
        drObj = new DataReader(ComObj);
        string str = "select DepotID,DepotName from tbl_MetaData_DEPOT  WHERE [DistrictId] = '" + distId.ToString() + "' order by DepotName asc";
        DataSet ds = drObj.selectAny(str);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "DepotID";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
            }
            else
            {
            }
        }
        ddlDepotList.Visible = true;
    }

    #region GridView
    public void FillGrid()
    {
        _sqlCon.Open();
        SqlCommand _cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        _cmd.Connection = _sqlCon;
        //_cmd.CommandText = "select * from tbl_storage_Depositor_WHR_Relation where Depotid='" + ddlDepotList.SelectedValue.ToString() + "' and District_Id='" + Session["Depot_DistID"].ToString() + "' and Arrival_Source ='" + arrivalsorc + "'";
        _cmd.CommandText = "prc_viewOpeningBalance";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
        _cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
        _cmd.Parameters.AddWithValue("@CreatedDate", ddlDateWise.SelectedValue.ToString());
        da.SelectCommand = _cmd;
        da.Fill(ds);

        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            panelContainer.Visible = true;
            lblRowCount.Text = "";
            lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();


        }
        else
        {
            panelContainer.Visible = false;
            gv.DataSource = null;
            gv.DataBind();

        }
        _sqlCon.Close();
        _cmd.Dispose();
    }
    public void FillGridOnLoad()
    {
        _sqlCon.Open();
        SqlCommand _cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        _cmd.Connection = _sqlCon;
        //_cmd.CommandText = "select * from tbl_storage_Depositor_WHR_Relation where Depotid='" + ddlDepotList.SelectedValue.ToString() + "' and District_Id='" + Session["Depot_DistID"].ToString() + "' and Arrival_Source ='" + arrivalsorc + "'";
        _cmd.CommandText = "prc_viewOpeningBalanceComplete";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
        _cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
        da.SelectCommand = _cmd;
        da.Fill(ds);

        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            panelContainer.Visible = true;
            lblRowCount.Text = "";
            lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            panelContainer.Visible = false;
            gv.DataSource = null;
            gv.DataBind();
        }
        _sqlCon.Close();
        _cmd.Dispose();
    }
    #endregion

    #region DropDownList
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridOnLoad(); FillDateOpen();
    }
    private void FillDateOpen()
    {
        _sqlCon.Open();
        SqlCommand _cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        _cmd.Connection = _sqlCon;
        //_cmd.CommandText = "select * from tbl_storage_Depositor_WHR_Relation where Depotid='" + ddlDepotList.SelectedValue.ToString() + "' and District_Id='" + Session["Depot_DistID"].ToString() + "' and Arrival_Source ='" + arrivalsorc + "'";
        _cmd.CommandText = "prc_viewOpeningBalanceDate";
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
        _cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
        da.SelectCommand = _cmd;
        da.Fill(ds);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDateWise.DataSource = ds.Tables[0];
                ddlDateWise.DataTextField = "CreatDate";
                ddlDateWise.DataValueField = "CreatDate";
                ddlDateWise.DataBind();
                ddlDateWise.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDateWise.Items.Clear();
                ddlDateWise.Items.Insert(0, "--Select--");

            }

        }

        _sqlCon.Close();
        _cmd.Dispose();
    }
    private void fillDistrict()
    {
        drObj = new DataReader(ComObj);
        string qrySelect = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
        DataSet ds = drObj.selectAny(qrySelect);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
        }
    }
    #endregion

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDepot(ddlDistrict.SelectedValue.ToString());
        FillGrid();
    }
    protected void ddlDateWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblAvailCapStk.Text = "";
        lblCrtCapStk.Text = "";
        FillGrid();
    }
    private void GetCurAndMaxCap(string stkId)
    {
        try
        {
            if (_sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
            String StrCurCap = "select Isnull(Max((OpeningBalancewts+ReceiptWts+isnull(Gain,0))-(IssuedWts+Isnull(Loss,0))),0) from DailyStacking_TransactionStatus where  autoid= (select max(autoid) from DailyStacking_TransactionStatus where Stackid='" + stkId + "')";
            SqlCommand cmdCurCap = new SqlCommand(StrCurCap, _sqlCon);
            string CurStackCap = cmdCurCap.ExecuteScalar().ToString();
            lblCrtCapStk.Text = "";
            lblCrtCapStk.Text = CurStackCap.ToString();

            String strMaxCap = "SELECT   isnull(max([Stack_capacity]),0) FROM [tbl_MetaData_STACK] WHERE Stack_ID='" + stkId + "'";
            SqlCommand cmdMaxcap = new SqlCommand(strMaxCap, _sqlCon);
            string MaxStackCap = cmdMaxcap.ExecuteScalar().ToString();
            string AvlStackCap = String.Format("{0:0.00000}", (Convert.ToDouble(MaxStackCap) - Convert.ToDouble(CurStackCap)));
            lblAvailCapStk.Text = "";
            lblAvailCapStk.Text = AvlStackCap.ToString();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
        finally
        {
            if (_sqlCon.State == ConnectionState.Open)
            {
                _sqlCon.Close();
            }
        }
    }
    protected void gv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            HiddenField1.Value = "";
            int rindx = e.NewSelectedIndex;
            gv.Rows[rindx].Focus();
            Label lbl = (Label)gv.Rows[rindx].FindControl("Label1");
            string WHRID = lbl.Text;
            HiddenField1.Value = WHRID;
            ds = new DataSet();
            if (Session["ds_GridInfo"] != null)
            {
                ds = (DataSet)Session["ds_GridInfo"];

                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Select("WHRID Like '%" + WHRID + "'"))
                    {
                        string stackID = dr[18].ToString();
                        string stsckName = dr[2].ToString();

                        //Change rovins gupta 25102013

                        //if (stsckName == "")
                        //{
                        //    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No stack assign for this record , you can not perform any action for this .'); </script> ");
                        //    return;
                        //}
                        GetCurAndMaxCap(stackID);
                    }
                }
                else
                {

                }
            }
        }
        catch (Exception)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('please check Godwon/Stack ,This may be deletd .please try agin.'); </script> ");
            return;
        }


    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string StackId = "";
        string OpeningBalanceBags = "";
        string OpeningBalanceWts = "";
        string ReceiptBags = "";
        string ReceiptWts = "";
        string IssuedBags = "";
        string IssuedWts = "";
        string TotalBags = "";
        string TotalWeight = "";

        //Perform Delete operation with log
        try
        {

            HiddenField2.Value = "";
            //int rindx = e.RowIndex;
            int rindx = gv.SelectedIndex;
            gv.Rows[rindx].Focus();
            Label lbl = (Label)gv.Rows[rindx].FindControl("Label1");
            string WHRID = lbl.Text;
            HiddenField2.Value = WHRID;
            if (Session["ds_GridInfo"] != null)
            {
                ds = (DataSet)Session["ds_GridInfo"];
                foreach (DataRow drs in ds.Tables[0].Select("WHRID Like '%" + WHRID + "'"))
                {
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string Bags = drs[9].ToString();
                    string Qty = drs[10].ToString();
                    string GodnId = drs[17].ToString();
                    StackId = drs[18].ToString();

                    if (_sqlCon.State == ConnectionState.Closed)
                    {
                        _sqlCon.Open();
                    }
                    //First Select 

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prc_getBagWts";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@Stackid", StackId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dss = new DataSet();
                    da.Fill(dss);
                    foreach (DataRow dr in dss.Tables[0].Rows)
                    {
                        OpeningBalanceBags = dr["OpeningBalanceBags"].ToString();
                        OpeningBalanceWts = dr["OpeningBalanceWts"].ToString();
                        ReceiptBags = dr["ReceiptBags"].ToString();
                        ReceiptWts = dr["ReceiptWts"].ToString();
                        IssuedBags = dr["IssuedBags"].ToString();
                        IssuedWts = dr["IssuedWts"].ToString();
                        TotalBags = Convert.ToString((Convert.ToInt32(OpeningBalanceBags) + Convert.ToInt32(ReceiptBags)) - Convert.ToInt32(IssuedBags));
                        TotalWeight = Convert.ToString((Convert.ToDouble(OpeningBalanceWts) + Convert.ToDouble(ReceiptWts)) - Convert.ToDouble(IssuedWts));
                    }
                    string RemaingBag = Convert.ToString(Convert.ToInt32(TotalBags) - Convert.ToInt32(Bags));
                    string RemaingWat = Convert.ToString(Convert.ToDouble(TotalWeight) - Convert.ToDouble(Qty));

                    //fIRST uPDATE 
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prc_updStackDailyTransaction";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@Stackid", StackId);
                    cmd.Parameters.AddWithValue("@ReceiptBags", RemaingBag);
                    cmd.Parameters.AddWithValue("@ReceiptWts", RemaingWat);
                    int up1 = cmd.ExecuteNonQuery();


                    //First Delete  
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_tbl_storage_Depositor_WHR_Relation_DeleteLog";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@Depositor_WHR_Id", HiddenField2.Value.ToString());
                    cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
                    int o1 = cmd.ExecuteNonQuery();

                    //Second Delete
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_tbl_Storage_Arrival_Stock_DeleteLog";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@ArrivalStock_Id", HiddenField2.Value.ToString());
                    cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
                    int t2 = cmd.ExecuteNonQuery();


                    //Third Delete
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "del_tbl_Storage_Receipt_Details_DeleteLog";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@StorageReceipt_Id", HiddenField2.Value.ToString());
                    cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
                    int t3 = cmd.ExecuteNonQuery();



                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Intbl_storage_Stacking_Details_DeleteLog";
                    cmd.Connection = _sqlCon;
                    cmd.Parameters.AddWithValue("@State_Id", "23");
                    cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Godown_ID", GodnId);
                    cmd.Parameters.AddWithValue("@Stack_ID", StackId);
                    cmd.Parameters.AddWithValue("@StorageReceipt_Id ", HiddenField2.Value.ToString());
                    cmd.Parameters.AddWithValue("@Bags ", Bags);
                    cmd.Parameters.AddWithValue("@Weight ", Qty);
                    int ret = cmd.ExecuteNonQuery();

                    if (ret > 0)
                    {
                        //fourth delete
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_tbl_storage_Stacking_Details_DeleteLog";
                        cmd.Connection = _sqlCon;
                        cmd.Parameters.AddWithValue("@StorageReceipt_Id", HiddenField2.Value.ToString());
                        cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@Godown_ID", GodnId);
                        cmd.Parameters.AddWithValue("@Stack_ID", StackId);
                        int res = cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "del_INIP";
                        cmd.Connection = _sqlCon;
                        cmd.Parameters.AddWithValue("@Depositor_WHR_Id", HiddenField2.Value.ToString());
                        cmd.Parameters.AddWithValue("@Client_IP", ip);
                        int delret = cmd.ExecuteNonQuery();


                        // NullControls();
                        if (res > 0)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record has successfully deleted .'); </script> ");
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error occured ,please try again. '); </script> ");
        }
        finally
        {
            _sqlCon.Close();
            FillGridOnLoad();
            GetCurAndMaxCap(StackId);
        }

    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}
