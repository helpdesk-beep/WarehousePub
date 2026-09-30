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

public partial class Procurement_WLC_Delete_ReceivedAccNote : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    string qry = "";
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltran;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            try
            {
                if (!IsPostBack)
                {
                    //Btn_Delete.Enabled = false;

                    //RadioButton1.Checked = true;

                    //FillGridBranch();
                    GetGodown();
                    Session["RefreshButton"] = "No";
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + PopMsg + "')", true);
                    }
                    // fillDistrict();
                    Btn_Delete.Attributes.Add("onclick", "javascript:return confirm('Are you sure and  wants to delete this record , please be sure for deleting data?');");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetGodown()
    {
        string qry = "";
        qry = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + Session["BranchID"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "Select");
        }
        
    }
    //public void FillGridBranch()
    //{
    //    string query = "";
    //    if (ddlCommType.SelectedItem.Text == "Wheat-PSS")
    //    {
    //       //query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'MPSCSC' as DepositorName,'Wheat-PSS' as commodity from Receive_Proc_Rabi2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null)";
    //        query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'MPSCSC' as DepositorName,'Wheat-PSS' as commodity from Receive_Proc_Rabi2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null and Commodity_Id in ('22'))";

    //    }
    //    else if (ddlCommType.SelectedItem.Text == "Dalhan")
    //    {
    //        //query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'MPSCSC' as DepositorName,'Dalhan' as commodity from Receive_Proc_CMS2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null)";
    //        query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'NAFED' as DepositorName,'Dalhan' as commodity from Receive_Proc_CMS2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null and Commodity_Id in ('63','64','33'))";

    //    }
    //    else if (ddlCommType.SelectedItem.Text == "Kharif2020")
    //    {
    //        //query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'NAFED' as DepositorName,'Dalhan' as commodity from Receive_Proc_CMS2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null and Commodity_Id in ('63','64','33'))";
    //        query = "select DF_Receipt_Id as Acpt_FCIRO_No,CONVERT(varchar(10),Acceptance_Date,103) as Receipt_Date,WLC_Bags as Qty_Rvd_No_of_Bags,[WLC_Qty] as Qty_Rvd_Weight,'' as DepositorName,'' as commodity from Receive_Proc_Kharif2020 where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id not in (select Acpt_FCIRO_No from tbl_Storage_Receipt_Details where BranchID='" + Session["BranchID"].ToString() + "' and Acpt_FCIRO_No is not null and Commodity_Id in ('13','8','11'))";

    //    }

    //    cmd = new SqlCommand(query, con);
    //    da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    Session["ds_GridInfo"] = ds;
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gv.DataSource = ds;
    //        gv.DataBind();
    //        lblRowCount.Text = "";
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        Btn_Delete.Enabled = true;
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
    //        lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
    //        gv.DataSource = null;
    //        gv.DataBind();
    //    }
    //}

    public void FillGridBranch()
    {
        string query = "";
        cmd = new SqlCommand("Get_Data_for_Delete_Receiving", con);
        da = new SqlDataAdapter(cmd);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Commodity", ddlCommType.SelectedValue);
        cmd.Parameters.AddWithValue("@Branchid", Session["BranchID"].ToString());
        cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            lblRowCount.Text = "";
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            Btn_Delete.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }

    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            int count = 0;
            try
            {
                if (gv.Rows.Count > 0)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    sqltran = con.BeginTransaction();
                    foreach (GridViewRow gr2 in gv.Rows)
                    {
                        DataSet ds = (DataSet)Session["ds_GridInfo"];
                        CheckBox chk_Delete = new CheckBox();
                        string ArrivalStock_Id = Convert.ToString(gv.DataKeys[gr2.RowIndex].Value);
                        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                        if (chk_Delete.Checked == true)
                        {
                            ds = (DataSet)Session["ds_GridInfo"];
                            foreach (DataRow drs in ds.Tables[0].Select("Acpt_FCIRO_No = '" + ArrivalStock_Id + "'"))
                            {
                                string Receiptid = drs[1].ToString();
                                 if (ddlCommType.SelectedItem.Text == "Wheat-PSS 2026-27")
                                {
                                   qry = "insert into [Receive_Proc_Rabi2026_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2026] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Dalhan 2026-27")
                                {
                                    qry = "insert into [Receive_Proc_CMS2026_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2026] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27','75')";

                                }
                               else if (ddlCommType.SelectedItem.Text == "Karif 2025-26")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    qry = "insert into [Receive_Proc_Kharif_2025_26_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif_2025_26] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','8','11','134','135')";

                                }
                               else if (ddlCommType.SelectedItem.Text == "Wheat-PSS 2025-26")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    qry = "insert into [Receive_Proc_Rabi2025_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2025] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Dalhan 2025-26")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    qry = "insert into [Receive_Proc_CMS2025_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2025] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27','75')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Wheat-PSS")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    //qry = "insert into [Receive_Proc_Rabi2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";
                                    qry = "insert into [Receive_Proc_Rabi2024_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2024] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Dalhan")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_CMS2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33')";
                                    //qry = "insert into [Receive_Proc_CMS2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27')";
                                    //qry = "insert into [Receive_Proc_CMS2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27')";
                                    qry = "insert into [Receive_Proc_CMS2024_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2024] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "CMR-Rice")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    //qry = "insert into [Receive_Proc_CMS2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33')";
                                    qry = "insert into [Receive_Proc_CMR2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMR2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('3','129')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Kharif2020")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_Kharif2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','11','8')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Kharif2021")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_Kharif2021_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2021] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','11','8')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Rabi2022")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_Rabi2022_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2022] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('22')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Pulses2022")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_CMS2022_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "'  ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2022] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Pulses2023")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_CMS2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "'  ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Kharif2022")
                                {
                                    //qry = "insert into [Receive_Proc_Rabi2020_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "'";
                                    qry = "insert into [Receive_Proc_Kharif2023_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2023] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','11','8')";

                                }

                                else if (ddlCommType.SelectedItem.Text == "Kharif2023")
                                {
                                    
                                    qry = "insert into [Receive_Proc_Kharif2024_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2024] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','11','8')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Soya-Beens")
                                {

                                    qry = "insert into [Receive_Proc_SoyaBeens2024_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_SoyaBeens2024] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('26')";

                                }
                                else if (ddlCommType.SelectedItem.Text == "Kharif2024_25")
                                {

                                    qry = "insert into [Receive_Proc_Kharif_2024_25_Log] SELECT [DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Moisture],[Book_No],[Aid],[WLC_Bags],[WLC_Qty],GETDATE(),'" + ip + "' ,[RecdBags_JuteNew],[RecdBags_PP],[RecdBags_JuteOld] FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif_2024_25] where Branch_Id='" + Session["BranchID"].ToString() + "' and DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Commodity_Id in ('13','11','8')";

                                }
                                cmd = new SqlCommand(qry, con, sqltran);
                                int rex = cmd.ExecuteNonQuery();
                                if (rex > 0)
                                {
                                     if (ddlCommType.SelectedItem.Text == "Wheat-PSS 2026-27")
                                    {
                                        qry = "delete from Receive_Proc_Rabi2026 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Dalhan 2026-27")
                                    {
                                       qry = "delete from Receive_Proc_CMS2026 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27','75')";

                                    }
                                   else if (ddlCommType.SelectedItem.Text == "Karif 2025-26")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        qry = "delete from Receive_Proc_Kharif_2025_26 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','8','11','134','135')";

                                    }
                                   else if (ddlCommType.SelectedItem.Text == "Wheat-PSS 2025-26")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        qry = "delete from Receive_Proc_Rabi2025 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Dalhan 2025-26")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        qry = "delete from Receive_Proc_CMS2025 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27','75')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Wheat-PSS")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        //qry = "delete from Receive_Proc_Rabi2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";
                                        qry = "delete from Receive_Proc_Rabi2024 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Dalhan")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        //qry = "delete from Receive_Proc_CMS2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27')";
                                        //qry = "delete from Receive_Proc_CMS2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27')";
                                        qry = "delete from Receive_Proc_CMS2024 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "CMR-Rice")
                                    {
                                        //qry = "delete from Receive_Proc_Rabi2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_CMR2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('3','129')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Kharif2020")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Kharif2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','11','8')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Kharif2021")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Kharif2021 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','11','8')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Rabi2022")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Rabi2022 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('22')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Pulses2022")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_CMS2022 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Pulses2023")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_CMS2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33','92','27')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Kharif2022")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Kharif2023 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','11','8')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Kharif2023")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Kharif2024 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','11','8')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Soya-Beens")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_SoyaBeens2024 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('26')";

                                    }
                                    else if (ddlCommType.SelectedItem.Text == "Kharif2024_25")
                                    {
                                        //qry = "delete from Receive_Proc_CMS2020 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('63','64','33')";
                                        qry = "delete from Receive_Proc_Kharif_2024_25 where DF_Receipt_Id='" + ArrivalStock_Id.ToString() + "' and Branch_Id='" + Session["BranchID"].ToString() + "' and Commodity_Id in ('13','11','8')";

                                    }
                                    cmd = new SqlCommand(qry, con, sqltran);
                                    int x = cmd.ExecuteNonQuery();
                                }

                                //////////////////////tbl_Storage_Receipt_Details_DeleteLog/////////////////////////

                                //qry = "Insert Into tbl_Storage_Receipt_Details_DeleteLog select [StorageReceipt_Id],[State_Id],[District_Id],[Depotid],[Commodity_Id],[Category_Id],[WHR_Flag],[WHR_Id],[Grade_Name],[Receipt_Date],[Gate_PassNo],[Mode_of_weighment],[BeamScale_LWB],[Qty_Rvd_No_of_Bags],[Qty_Rvd_Weight],[Supply_gunny_New],[Supply_gunny_Old],[Godown_Delivery_No],[Distance_From_PC],[Ack_Book_No],[Ack_Serial_No],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[AnalysisStatus],[Depositortype],[Supply_gunny_Once_used],[Type_of_gunny],[DepositorName],[ArrivalSource_ID],[Remarks],[Acpt_FCIRO_No],[Acpt_FCIRO_Date],[Client_IP],[IssueID],BranchID,Rid from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "' and  BranchID='" + Session["BranchID"].ToString() + "'";
                                //cmd = new SqlCommand(qry, con, sqltran);
                                //int rex1 = cmd.ExecuteNonQuery();
                                //if (rex1 > 0)
                                //{
                                //    qry = "Delete from tbl_Storage_Receipt_Details where StorageReceipt_Id='" + Receiptid + "' and  BranchID='" + Session["BranchID"].ToString() + "'";
                                //    cmd = new SqlCommand(qry, con, sqltran);
                                //    int x = cmd.ExecuteNonQuery();
                                //}

                                //////////////////////tbl_storage_Stacking_Details_DeleteLog/////////////////////////

                                //qry = "Insert Into tbl_storage_Stacking_Details_DeleteLog ([State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],[DeletedBy],[DeletedDate],[autoid],[WHRId],[Status],Branchid)  select [State_Id],[District_Id],[Depotid],[Godown_ID],[Stack_ID],[StorageReceipt_Id],[Bags],[Weight],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + ip + "',getdate(),[autoid],[WHRId],[Status],BranchID from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "'  and Branchid='" + Session["BranchID"].ToString() + "'";
                                //cmd = new SqlCommand(qry, con, sqltran);
                                //int rex2 = cmd.ExecuteNonQuery();
                                //if (rex2 > 0)
                                //{
                                //    qry = "Delete from tbl_storage_Stacking_Details where StorageReceipt_Id='" + Receiptid + "' and Branchid='" + Session["BranchID"].ToString() + "'";
                                //    cmd = new SqlCommand(qry, con, sqltran);
                                //    int x = cmd.ExecuteNonQuery();
                                //}

                                //////////////////////Receive_Proc_Kharif2016_Log/////////////////////////
                                //qry = "Insert Into Receive_Proc_Kharif2016_Log ([StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Deleted_By],[Deleted_Date]) SELECT [StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2016] where [StorageReceipt_Id]='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                //qry = "Insert Into Receive_Proc_Kharif2019_Log ([StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],[Deleted_By],[Deleted_Date]) SELECT [StorageReceipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],[Rec_Qty],[Depositor_Form_No],'" + ip + "',getdate() FROM [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2019] where [StorageReceipt_Id]='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";

                                //cmd = new SqlCommand(qry, con, sqltran);
                                //int rex3 = cmd.ExecuteNonQuery();
                                //if (rex2 > 0)
                                //{
                                //    //qry = "Delete from Receive_Proc_Kharif2016 where StorageReceipt_Id='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";
                                //    qry = "Delete from Receive_Proc_Kharif2019 where StorageReceipt_Id='" + Receiptid + "' and Branch_Id='" + Session["BranchID"].ToString() + "'";

                                //    cmd = new SqlCommand(qry, con, sqltran);
                                //    int x = cmd.ExecuteNonQuery();
                                //}

                                count++;
                            }
                        }
                    }
                    sqltran.Commit();
                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record for Deleting')", true);
                }
                if (count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record has successfully deleted')", true);
                    FillGridBranch();
                }
                else
                {
                    lbl_notfound.Visible = true;
                    lbl_notfound.Text = "Please Select Atleast One Record For Deleting";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast One Record For Deleting')", true);
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                lbl_notfound.Visible = true;
                lbl_notfound.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }


    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/StatePages/StateWelcome.aspx");
        Response.Redirect("~/Branch_Welcome.aspx");

    }

    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chk_Delete");
            CheckBox chkBxHeader = (CheckBox)this.gv.HeaderRow.FindControl("chkBxHeader");
            chkBxSelect.Attributes["onclick"] = string.Format
                                                   (
                                                      "javascript:ChildClick(this,'{0}');",
                                                      chkBxHeader.ClientID
                                                   );
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gv.PageIndex = e.NewPageIndex;
            FillGridBranch();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridBranch();
    }
}
