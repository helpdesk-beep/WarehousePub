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

public partial class IssueCenterLevel_Storage_EditDepositorwithWHR : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
  
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Region_ID"] != null)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        Session["RefreshButton"] = "No";
                        string PopMsg = "";
                        PopMsg = Request.QueryString["PopMsg"];
                        if (PopMsg != null)
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                        }
                        fillDistrict();
                        //getDepot(Session["Depot_DistID"].ToString());
                        //FillDateOpen();
                        FillGridOnLoad();
                        Btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
                    }
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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
        try
        {
            if (Session["Region_ID"] != null)
            {
                string query = "select depo.DepotID,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
                cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepotList.DataSource = ds.Tables[0];
                    ddlDepotList.DataTextField = "DepotName";
                    ddlDepotList.DataValueField = "DepotID";
                    ddlDepotList.DataBind();
                    ddlDepotList.Items.Insert(0, "---Select---");
                    ddlDepotList.SelectedValue = Session["Depot_DepotID"].ToString();
                    ddlDepotList.Enabled = false;
                }
                else
                {
                    ddlDepotList.Items.Insert(0, "---Select---");
                }
            }
        }
        catch (Exception)
        {
            ///////
        }
    }

    public void FillGrid()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.Connection = con;
        cmd.CommandText = "prc_viewOpeningBalance";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@CreatedDate", ddlDateWise.SelectedValue.ToString());
        da.SelectCommand = cmd;
        da.Fill(ds);

        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            gv.DataSource = null;
            gv.DataBind();
        }
        con.Close();
        cmd.Dispose();
    }

    public void FillGridOnLoad()
    {
        con.Open();
        cmd = new SqlCommand();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.Connection = con;
        cmd.CommandText = "prc_viewOpeningBalanceComplete";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@District_Id", ddlDistrict.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@Depotid", ddlDepotList.SelectedValue.ToString());
        da.SelectCommand = cmd;
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are - " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lbl_notfound.Visible = true;
            lbl_notfound.Text = "There is No WHR Found..";
            gv.DataSource = null;
            gv.DataBind();
        }
        con.Close();
        cmd.Dispose();
    }

  

    private void fillDistrict()
    {
        try
        {
            if (Session["Region_ID"] != null)
            {
                string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
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
                    ddlDistrict.SelectedValue = Session["Region_ID"].ToString();
                    //ddlDistrict.Enabled = false;
                }
                else
                {
                    ////
                }
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void ddlDateWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        string Stackid = "";
        int count = 0;
        //Perform Delete operation with log
        try
        {
            if (gv.Rows.Count > 0)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                foreach (GridViewRow gr2 in gv.Rows)
                {
                    DataSet ds = (DataSet)Session["ds_GridInfo"];
                    CheckBox chk_Delete = new CheckBox();
                    string WHRID = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    chk_Delete = (CheckBox)gr2.Cells[5].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        ds = (DataSet)Session["ds_GridInfo"];
                        foreach (DataRow drs in ds.Tables[0].Select("WHRID = '" + WHRID + "'"))
                        {
                            Stackid = drs[17].ToString();
                            qry = "Insert Into tbl_storage_Depositor_WHR_Relation_DeleteLog SELECT [Depositor_WHR_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[Whr_No],[Depositor_Name],[Date_of_Deposit],[TotalBags_Received],[Total_Qty_Received],[Mode_of_weighment],[BeamScale_LWB],[AvgMoisture_Content],[Lot_No],[MktValue_of_Commodity],[Arrival_Source],[WHR_Issue_Date],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[MadeUpBags],[Client_IP],[AvgMoisture_Content_To] from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                qry = "Delete from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + WHRID + "'";
                                cmd = new SqlCommand(qry, con);
                                int d = cmd.ExecuteNonQuery();
                                if (d > 0)
                                {
                                    qry = "Update whr_status Set Statusflag = 'Y' where WhrId = '" + WHRID + "'";
                                    cmd = new SqlCommand(qry, con);
                                    int x = cmd.ExecuteNonQuery();
                                    if (x > 0)
                                    {
                                        qry = "Update tbl_Storage_Receipt_Details Set WHR_Flag = 'N',WHR_Id ='' where StorageReceipt_Id = '" + WHRID + "'";
                                        cmd = new SqlCommand(qry, con);
                                        int y = cmd.ExecuteNonQuery();

                                        ///////////////////////////////////////////////

                                        qry = "Update tbl_storage_Stacking_Details Set WHRId ='' where StorageReceipt_Id = '" + WHRID + "' and Stack_ID = '" + Stackid + "'";
                                        cmd = new SqlCommand(qry, con);
                                        int z = cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record has successfully deleted .'); </script> ");
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Select Atleast One Record For Deleting .'); </script> ");
            }
        }
        catch (Exception)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error occured ,please try again. '); </script> ");
        }
        finally
        {
            con.Close();
            FillGridOnLoad();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["Region_ID"] != null)
            {
                string query = "select depo.DepotID,depo.DepotName from tbl_MetaData_DEPOT as depo inner join tbl_MetaData_DISTRICT as dis on depo.DistrictId=dis.District_Id where depo.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepotList.DataSource = ds.Tables[0];
                    ddlDepotList.DataTextField = "DepotName";
                    ddlDepotList.DataValueField = "DepotID";
                    ddlDepotList.DataBind();
                    ddlDepotList.Items.Insert(0, "---Select---");
                    ddlDepotList.SelectedValue = Session["Region_ID"].ToString();
                    //ddlBranch.Enabled = false;
                }
                else
                {
                    ddlDepotList.Items.Insert(0, "---Select---");
                }
            }
        }
        catch (Exception)
        {
            ////
        }
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        qry = "SELECT Distinct  CONVERT(varchar,(SDW.CreatedDate ) ,103 ) as CreatDate FROM [tbl_storage_Depositor_WHR_Relation] AS SDW inner join tbl_MetaData_STORAGE_COMMODITY On SDW.Commodity_Id=tbl_MetaData_STORAGE_COMMODITY.Commodity_Id inner join tbl_MetaData_STORAGE_CATEGORY on SDW.Category_Id=tbl_MetaData_STORAGE_CATEGORY.Category_Id inner join tbl_storage_Stacking_Details on SDW.Depositor_WHR_Id =tbl_storage_Stacking_Details.StorageReceipt_Id where SDW.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' and SDW.Depotid='" + ddlDepotList.SelectedValue.ToString() + "' order by CreatDate  desc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
}
