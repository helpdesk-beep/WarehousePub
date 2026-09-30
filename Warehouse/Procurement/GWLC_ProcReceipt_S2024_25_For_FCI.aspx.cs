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
using AjaxControlToolkit;

public partial class GWLC_ProcReceipt_S2024_25_For_FCI : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltran;
    SqlCommand cmd = null;
    string DFReceive_ID = "";
    string CheckPaymentNotDone = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                fillGodnList();
                fillDepositorType();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
        //illCropYear();
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

    private void fillGodnList()
    {
        string qry = "";
        qry = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Hired_Type not in ('Joint Venture(JV)','Silo Bags','Others','WDRA','PVT.PEG','Tribal Scheme','Steel Silo','FCI','CAP-PMS','BOT','BOT-AUB') and Godown_ID not in (Select distinct Godown_Id from Pvt_Warehouse_Login where BranchID= '" + Session["BranchId"].ToString() + "')  ORDER BY [Godown_Name] Asc";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_godown.DataSource = ds.Tables[0];
            ddl_godown.DataTextField = "Godown_Name";
            ddl_godown.DataValueField = "Godown_ID";
            ddl_godown.DataBind();
            ddl_godown.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {

        }

        //ddl_godown.ClearSelection();
        //if (Session["Depot_DistID"] != null)
        //{
        //    string query = "";
        //    query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Hired_Type not in ('Joint Venture(JV)','Silo Bags','Others','WDRA','PVT.PEG','Tribal Scheme','Steel Silo','FCI','CAP-PMS','BOT','BOT-AUB') and Godown_ID not in (Select distinct Godown_Id from Pvt_Warehouse_Login where BranchID= '" + Session["BranchId"].ToString() + "')  ORDER BY [Godown_Name] Asc ";
        //    SqlCommand cmd = new SqlCommand(query, con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ddl_godown.DataSource = ds.Tables[0];
        //        ddl_godown.DataTextField = "Godown_Name";
        //        ddl_godown.DataValueField = "Godown_ID";
        //        ddl_godown.DataBind();
        //        ddl_godown.Items.Insert(0, "Select");
        //    }
        //    else
        //    {
        //        ddl_godown.DataSource = null;
        //        ddl_godown.DataTextField = "Godown_Name";
        //        ddl_godown.DataValueField = "Godown_ID";
        //        ddl_godown.DataBind();
        //        ddl_godown.Items.Insert(0, "Select");
        //    }
        //}
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddl_godown.SelectedValue != "--Select--")
        {
            fillProcRabi2024_FCI();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown...'); </script> ");

        }
    }
    protected void fillProcRabi2024_FCI()
    {
        //if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        //{
        //    try
        //    {
        //        string Query = "";
        //        string LusterLossc = "";
        //        if (ddldepositortype.SelectedItem.Text == "Institution")
        //        {

        //            if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
        //            {
        //                Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_MPWLC) as Godown from csms.dbo.CMR_QualityInspection_FCI as AD INNER JOIN tbl_MetaData_GODOWN_2018 gdn on AD.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_MPWLC='" + Session["GodownID_New"].ToString() + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtDate.Text) + "' and AD.Rejected=0 and AD.Commodity='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + Session["GodownID_New"].ToString() + "') order by AD.Date asc";

        //            }

        //            SqlDataAdapter da = new SqlDataAdapter();
        //            DataSet ds = new DataSet();
        //            SqlCommand cmd = new SqlCommand();

        //            cmd = new SqlCommand(Query, con);

        //            cmd.CommandType = CommandType.Text;

        //            da.SelectCommand = cmd;
        //            if (cmd.CommandText != null && cmd.CommandText != "")
        //            {
        //                da.Fill(ds);
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {

        //                    gdnewproc.DataSource = ds;
        //                    gdnewproc.DataBind();
        //                    lblMsg.Text = "";
        //                    lblMsg.Visible = false;
        //                    trnewproc.Visible = true;

        //                    ddlDepositor.Enabled = false;
        //                    txtDate.Enabled = false;
        //                    ddlProcCmd.Enabled = false;
        //                    //CalendarExtender1.Enabled = false;

        //                }
        //                else
        //                {
        //                    lblMsg.Text = "No pending Depositor Form for this commodity,depositor, godown and Date";
        //                    lblMsg.Visible = true;
        //                    //GvuDispatchFromPC.DataSource = null;
        //                    //GvuDispatchFromPC.DataBind();
        //                    //tr_Disfromprc.Visible = false;
        //                    //trfromrailhead.Visible = false;
        //                    //trfromothdepot.Visible = false;
        //                    trnewproc.Visible = false;
        //                }
        //            }
        //            else
        //            {
        //                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
        //                trnewproc.Visible = false;
        //            }
        //        }

        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        string msg = ex.Message;
        //        //msg += ex.Message;
        //        throw new Exception(msg);
        //    }
        //    //}
        //    //else
        //    //{
        //    //    lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
        //    //    lblMsg.Visible = true;

        //    //    trnewproc.Visible = false;
        //    //}
        //}

        //else
        //{
        //    Response.Redirect("~/SessionExpired.htm");
        //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
        //    //trnewproc.Visible = false;
        //}

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {

                    if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_MPWLC) as Godown from csms.dbo.CMR_QualityInspection_FCI as AD INNER JOIN tbl_MetaData_GODOWN_2018 gdn on AD.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_MPWLC='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Commodity='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_MPWLC) as Godown from csms.dbo.CMR_QualityInspection_FCI as AD INNER JOIN tbl_MetaData_GODOWN_2018 gdn on AD.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_MPWLC='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtDate.Text) + "' and AD.Rejected=0 and AD.Commodity='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand(Query, con);

                    cmd.CommandType = CommandType.Text;

                    da.SelectCommand = cmd;
                    if (cmd.CommandText != null && cmd.CommandText != "")
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            gdnewproc.DataSource = ds;
                            gdnewproc.DataBind();
                            lblMsg.Text = "";
                            lblMsg.Visible = false;
                            trnewproc.Visible = true;

                            ddlDepositor.Enabled = false;
                            txtDate.Enabled = false;
                            ddlProcCmd.Enabled = false;
                            //CalendarExtender1.Enabled = false;

                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity,depositor, godown and Date";
                            lblMsg.Visible = true;
                            //GvuDispatchFromPC.DataSource = null;
                            //GvuDispatchFromPC.DataBind();
                            //tr_Disfromprc.Visible = false;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = false;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
                        trnewproc.Visible = false;
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = ex.Message;
                //msg += ex.Message;
                throw new Exception(msg);
            }
            //}
            //else
            //{
            //    lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
            //    lblMsg.Visible = true;

            //    trnewproc.Visible = false;
            //}
        }

        else
        {
            Response.Redirect("~/SessionExpired.htm");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
            //trnewproc.Visible = false;
        }
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Choose Date...'); </script> ");
            //fillGodnList();
        }
        //else if (ddlProcCmd.SelectedValue=="22"&& ddlDepositor.SelectedItem.Text!="MPSCSC")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
        //    //fillGodnList();
        //}
        //else if (ddlProcCmd.SelectedValue != "22" && ddlDepositor.SelectedItem.Text == "MPSCSC")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
        //    //fillGodnList();
        //}
        else
        {
            if (ddlcropyear.SelectedItem.Text == "2024-2025")
            {
                if (ddlDepositor.SelectedValue.ToString() == "181")
                {
                    fillProcRabi2024_FCI();
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    fillProcRabi2024();
                }
            }
           else if (ddlcropyear.SelectedItem.Text == "2025-2026")
            {
                if (ddlDepositor.SelectedValue.ToString() == "181")
                {
                    fillProcRabi2024_FCI();
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    fillProcRabi2024();
                }
            }
            tblbtn.Visible = true;
            btn_save.Enabled = true;
            int sum = 0;
            decimal sumQty = 0;
            int WLC_Bags = 0;
            decimal WLC_Qty = 0;
            int CountDF = 0;
            foreach (GridViewRow row in gdnewproc.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                //if (chkbox.Checked == true)
                //{
                //sum += Convert.ToInt32(gdnewproc.Rows[i].Cells[5].Text);
                //sumQty += Convert.ToDecimal(gdnewproc.Rows[i].Cells[6].Text);
                TextBox WBAGS = (TextBox)row.FindControl("sendb");
                WLC_Bags = WLC_Bags + Convert.ToInt32(WBAGS.Text);
                TextBox WWEIGHT = (TextBox)row.FindControl("sendq");
                WLC_Qty = WLC_Qty + Convert.ToDecimal(WWEIGHT.Text);
                CountDF = CountDF + 1;
                //}
            }
            //lblTotalBagSend.Text = sum.ToString();
            //lblTotalQtySend.Text = sumQty.ToString();
            //lblTotalBagSend.Text = sum.ToString();
            //lblTotalQtySend.Text = decimal.Round((decimal)sumQty, 4).ToString();
            lblTotalBagSend.Text = WLC_Bags.ToString();
            lblTotalQtySend.Text = decimal.Round((decimal)WLC_Qty, 5).ToString();
            lblNoofAC.Text = CountDF.ToString();
        }
    }
    protected void fillProcRabi2024()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {



                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {

                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_ID) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Commodity_ID='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_MPWLC) as Godown from csms.dbo.CMR_QualityInspection_FCI as AD INNER JOIN tbl_MetaData_GODOWN_2018 gdn on AD.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_MPWLC='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtDate.Text) + "' and AD.Rejected=0 and AD.Commodity_ID='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    }

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand(Query, con);

                    cmd.CommandType = CommandType.Text;

                    da.SelectCommand = cmd;
                    if (cmd.CommandText != null && cmd.CommandText != "")
                    {
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Enabled = true;
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Enabled = true;
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtgain")).Enabled = true;
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtloss")).Enabled = true;
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtbagnumber")).Text = "0";
                            //((TextBox)gdstackdetail.Rows[s].FindControl("txtweight")).Text = "0";
                            //((TextBox)gdnewproc.Rows[0].FindControl("txtbagnumber")).Text = "5";
                            //((TextBox)gdnewproc.Rows[1].FindControl("txtweight")).Text = "5";


                            gdnewproc.DataSource = ds;
                            gdnewproc.DataBind();
                            lblMsg.Text = "";
                            lblMsg.Visible = false;
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;

                            ddlDepositor.Enabled = false;
                            txtDate.Enabled = false;
                            ddlProcCmd.Enabled = false;
                            //CalendarExtender1.Enabled = false;

                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity,depositor, godown and Date";
                            lblMsg.Visible = true;
                            //GvuDispatchFromPC.DataSource = null;
                            //GvuDispatchFromPC.DataBind();
                            //tr_Disfromprc.Visible = false;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = false;
                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
                        trnewproc.Visible = false;
                    }
                }

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = ex.Message;
                //msg += ex.Message;
                throw new Exception(msg);
            }
            //}
            //else
            //{
            //    lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
            //    lblMsg.Visible = true;

            //    trnewproc.Visible = false;
            //}
        }

        else
        {
            Response.Redirect("~/SessionExpired.htm");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No data found...'); </script> ");
            //trnewproc.Visible = false;
        }
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            int WLC_Bags = 0;
            decimal WLC_Qty = 0;
            int CheckBoxCount = 0;
            foreach (GridViewRow row in gdnewproc.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
                if (chkbox.Checked == true)
                {
                    CheckBoxCount = CheckBoxCount + 1;
                    TextBox WBAGS = (TextBox)row.FindControl("txtbagnumber");
                    WLC_Bags = WLC_Bags + Convert.ToInt32(WBAGS.Text);
                    TextBox WWEIGHT = (TextBox)row.FindControl("txtweight");
                    WLC_Qty = WLC_Qty + Convert.ToDecimal(WWEIGHT.Text);
                }
            }
            string Deposit_Date = gdnewproc.Rows[0].Cells[2].Text;
            if (CheckBoxCount > 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपके द्वारा एक से अधिक ट्रक चालान को सिलैक्ट किया है, एक बार मे केवल एक ही ट्रक चालान सिलैक्ट किया जा सकता है|'); </script> ");
                lblTotalBags.Text = hdnLabelState.Value;
                lblTotalQty.Text = hdnLabelStateQty.Value;
            }
            else if (hdnLabelState.Value == "" || hdnLabelStateQty.Value == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select At least one Challan/Acc. Note'); </script> ");
                lblTotalBags.Text = hdnLabelState.Value;
                lblTotalQty.Text = hdnLabelStateQty.Value;
            }
            else if (Convert.ToInt32(WLC_Bags) != Convert.ToInt32(hdnLabelState.Value) || Convert.ToDecimal(WLC_Qty) != Convert.ToDecimal(hdnLabelStateQty.Value))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Selected Challan and Total of Bags/Qty. Does not Match'); </script> ");
                lblTotalBags.Text = hdnLabelState.Value;
                lblTotalQty.Text = hdnLabelStateQty.Value;
            }
            else if (Deposit_Date != txtDate.Text)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Selected Date and Acceptance Date Does not Match'); </script> ");
                lblTotalBags.Text = hdnLabelState.Value;
                lblTotalQty.Text = hdnLabelStateQty.Value;
            }
            else
            {
                GetReceivedSummary();
                System.Threading.Thread.Sleep(1000);
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    public void GetReceivedSummary()
    {
        lblDepostDate.Text = txtDate.Text;
        lblGodown.Text = ddl_godown.SelectedItem.Text;
        lblSendBags.Text = lblTotalBagSend.Text;
        lblSendQty.Text = lblTotalQtySend.Text;
        lblRcdBags.Text = hdnLabelState.Value;
        lblRecdQty.Text = hdnLabelStateQty.Value;
        lblDepositor.Text = ddlDepositor.SelectedItem.Text;
        lblCropYear.Text = ddlcropyear.SelectedValue;
        lblCommodity.Text = ddlProcCmd.SelectedItem.Text;
        hdnGodownID.Text = ddl_godown.SelectedValue;
        hdnDepositorID.Text = ddlDepositor.SelectedValue;
        hdnCommodityID.Text = ddlProcCmd.SelectedValue;
        ModalPopupExtender2.Show();
        //
        lblTotalBags.Text = hdnLabelState.Value;
        lblTotalQty.Text = hdnLabelStateQty.Value;

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        decimal sum = 0;
        decimal sumQty = 0;
        int WLC_Bags = 0;
        decimal WLC_Qty = 0;
        foreach (GridViewRow row in gdnewproc.Rows)
        {
            CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
            if (chkbox.Checked == true)
            {
                TextBox WBAGS = (TextBox)row.FindControl("txtbagnumber");
                WLC_Bags = WLC_Bags + Convert.ToInt32(WBAGS.Text);
                TextBox WWEIGHT = (TextBox)row.FindControl("txtweight");
                WLC_Qty = WLC_Qty + Convert.ToDecimal(WWEIGHT.Text);
            }
        }
        if (WLC_Bags == Convert.ToInt32(lblRcdBags.Text) && WLC_Qty == Convert.ToDecimal(lblRecdQty.Text))
        {
            Insert_Depositor_Form_Detail();
            System.Threading.Thread.Sleep(1000);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Selected Challan and Total of Bags/Qty. Does't Match'); </script> ");
        }
    }
    public void Insert_Depositor_Form_Detail()
    {
        string DepositorNo = "";
        string AccptNo = "";
        string AccptDate = "";
        string Godown = hdnGodownID.Text;
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        string query2 = "";
        GetWLC_Depositor_FN();
        string AutoID = ViewState["AID"].ToString();
        int WLC_Bags = 0;
        decimal WLC_Qty = 0;
        foreach (GridViewRow row in gdnewproc.Rows)
        {
            CheckBox chkbox = (CheckBox)row.FindControl("chk_Sum");
            if (chkbox.Checked == true)
            {
                //TextBox DFNo = (TextBox)row.FindControl("DepositerNo");
                //DepositorNo = DFNo.Text;
                //TextBox ACNo = (TextBox)row.FindControl("Acceptance_No");
                //AccptNo = ACNo.Text;
                //TextBox ACDate = (TextBox)row.FindControl("Acceptance_Date");
                //AccptDate = ACDate.Text;
                DepositorNo = row.Cells[0].Text;
                AccptNo = row.Cells[1].Text;
                AccptDate = row.Cells[2].Text;
                TextBox WBAGS = (TextBox)row.FindControl("txtbagnumber");
                WLC_Bags = Convert.ToInt32(WBAGS.Text);
                TextBox WWEIGHT = (TextBox)row.FindControl("txtweight");
                WLC_Qty = Convert.ToDecimal(WWEIGHT.Text);
                if (ddlcropyear.SelectedItem.Text == "2024-2025" || ddlcropyear.SelectedItem.Text == "2025-2026" && (hdnCommodityID.Text.ToString() == "3") || (hdnCommodityID.Text.ToString() == "129") || (hdnCommodityID.Text.ToString() == "131"))
                {
                    if (ddlDepositor.SelectedValue.ToString() == "181")
                    {
                        query2 = "SELECT MD.DistrictId as Distt_ID,'' IssueCenter_ID,'' Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.DO_Number AS TC_Number,prc.Truck_No AS Truck_No,Prc.CropYear AS CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.ToulReceiptNo as IssueID,prc.Godown_MPWLC as godown,ISNULL(prc.Bags,0) as Recd_Bags,Prc.Accept_CommonRice+Prc.Accept_GradeARice AS Recd_Qty,Prc.Commodity as CommodityId,Prc.CMRDO_Number as Depositor_Form_No,MD.BranchId AS Branch_Id,'' TaulParchi,'' Weighbridge_ID,'' Weighbridge_TaulParchi,'0' as Weighbridge_Qty,'0' Moisture,'' Book_No,Prc.HDPEPPbags as RecdBags_PP,JuteNewBags AS RecdBags_JuteNew,JuteOldBags as RecdBags_JuteOld FROM [CSMS ].dbo.CMR_QualityInspection_FCI as Prc INNER JOIN tbl_MetaData_GODOWN_2018 gdn on Prc.Godown_MPWLC = gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID = MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Commodity='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and gdn.Godown_ID='" + Godown + "'";
                        //query2 = "SELECT Prc.District as Distt_ID,prc.issueCentre_code as IssueCenter_ID,'' as Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.Book_Number as TC_Number,prc.Truck_No as Truck_No,Prc.CropYear as CropYear,Prc.Acceptance_No, convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.StackNumber as IssueID,prc.Godown_MPWLC as godown,ISNULL(prc.Bags,0) as Recd_Bags,ISNULL(Prc.Accept_GradeARice,0) AS Recd_Qty,'3' as CommodityId,Prc.Book_Number as Depositor_Form_No,Prc.Branch_Code as Branch_Id,Prc.Agreement_ID as TaulParchi,Prc.Inspector_ID as Weighbridge_ID ,'' as Weighbridge_TaulParchi,ISNULL(0,0) as Weighbridge_Qty,Prc.JuteNewBags RecdBags_JuteNew,0 AS RecdBags_PP,Prc.JuteOldBags RecdBags_JuteOld ,0 AS Moisture,Prc.Book_Number as Book_No FROM csms.dbo.CMR_QualityInspection_FCI as Prc INNER JOIN tbl_MetaData_GODOWN_2018 gdn on Prc.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where Prc.Acceptance_No is not null and MD.BranchId='" + Session["BranchId"].ToString() + "' and Prc.Book_Number='" + DepositorNo + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Acceptance_No='" + AccptNo + "' and gdn.Godown_ID='" + Godown + "'";
                    }
                    else
                    {
                        //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2021] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                        //query2 = "INSERT INTO [dbo].[Receive_Proc_CMR2020] ([DF_Receipt_Id] ,[District] ,[issueCentre_code] ,[CropYear] ,[Book_Number] ,[Date] ,[Mill_Name] ,[Milling_Type] ,[DO_Number] ,[Agreement_ID] ,[LotNumber] ,[Acceptance_No] ,[Rejection_No] ,[Truck_No] ,[LD_No] ,[TotaGA] ,[TotaS] ,[TotaRemark] ,[BookOnlyNumber] ,[IP_Address] ,[Current_DateTime] ,[User_Agent] ,[Submited] ,[Rejected] ,[Daane] ,[Accept_CommonRice] ,[Accept_GradeARice] ,[Reject_CommonRice] ,[Reject_GradeARice] ,[Branch_Code] ,[Godown_Code] ,[Bags] ,[BagType] ,[Tags] ,[TagNo] ,[TruckNo1] ,[ToulReceiptNo] ,[Inspector_ID] ,[StackNumber] ,[StackName] ,[IsStackAccepted] ,[IsStackRejected] ,[Sortex_type] ,[JuteNewBags] ,[JuteOldBags] ,[HDPEPPbags] ,[Aid] ,[WLC_Bags] ,[WLC_Qty] ,[Deleted_Date] ,[Deleted_By] ,[Created_Date] ,[Created_By]) VALUES ([DF_Receipt_Id] ,[District] ,[issueCentre_code] ,[CropYear] ,[Book_Number] ,[Date] ,[Mill_Name] ,[Milling_Type] ,[DO_Number] ,[Agreement_ID] ,[LotNumber] ,[Acceptance_No] ,[Rejection_No] ,[Truck_No] ,[LD_No] ,[TotaGA] ,[TotaS] ,[TotaRemark] ,[BookOnlyNumber] ,[IP_Address] ,[Current_DateTime] ,[User_Agent] ,[Submited] ,[Rejected] ,[Daane] ,[Accept_CommonRice] ,[Accept_GradeARice] ,[Reject_CommonRice] ,[Reject_GradeARice] ,[Branch_Code] ,[Godown_Code] ,[Bags] ,[BagType] ,[Tags] ,[TagNo] ,[TruckNo1] ,[ToulReceiptNo] ,[Inspector_ID] ,[StackNumber] ,[StackName] ,[IsStackAccepted] ,[IsStackRejected] ,[Sortex_type] ,[JuteNewBags] ,[JuteOldBags] ,[HDPEPPbags] ,[Aid] ,[WLC_Bags] ,[WLC_Qty] ,[Deleted_Date] ,[Deleted_By] ,[Created_Date] ,[Created_By])";
                        //query2 = "SELECT Prc.District as Distt_ID,prc.issueCentre_code as IssueCenter_ID,'' as Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.Book_Number as TC_Number,prc.Truck_No as Truck_No,Prc.CropYear as CropYear,Prc.Acceptance_No, convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.StackNumber as IssueID,prc.Godown_Code as godown,ISNULL(prc.Bags,0) as Recd_Bags,ISNULL(Prc.Accept_CommonRice,0) AS Recd_Qty,'3' as CommodityId,Prc.Book_Number as Depositor_Form_No,Prc.Branch_Code as Branch_Id,Prc.Agreement_ID as TaulParchi,Prc.Inspector_ID as Weighbridge_ID ,'' as Weighbridge_TaulParchi,ISNULL(0,0) as Weighbridge_Qty,'' as Moisture,Prc.Book_Number as Book_No FROM csms.dbo.CMR_QualityInspection_2019 as Prc where Prc.Acceptance_No is not null and Prc.Branch_Code='" + Session["BranchId"].ToString() + "' and Prc.Book_Number='" + DepositorNo + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Godown_Code='" + Godown + "'";
                        query2 = "SELECT Prc.District as Distt_ID,prc.issueCentre_code as IssueCenter_ID,'' as Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.Book_Number as TC_Number,prc.Truck_No as Truck_No,Prc.CropYear as CropYear,Prc.Acceptance_No, convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.StackNumber as IssueID,prc.Godown_Code as godown,ISNULL(prc.Bags,0) as Recd_Bags,ISNULL(Prc.Accept_CommonRice,0) AS Recd_Qty,'3' as CommodityId,Prc.Book_Number as Depositor_Form_No,Prc.Branch_Code as Branch_Id,Prc.Agreement_ID as TaulParchi,Prc.Inspector_ID as Weighbridge_ID ,'' as Weighbridge_TaulParchi,ISNULL(0,0) as Weighbridge_Qty,Prc.JuteNewBags RecdBags_JuteNew,0 AS RecdBags_PP,Prc.JuteOldBags RecdBags_JuteOld ,0 AS Moisture,Prc.Book_Number as Book_No FROM csms.dbo.CMR_QualityInspection_2019 as Prc where Prc.Acceptance_No is not null and Prc.Branch_Code='" + Session["BranchId"].ToString() + "' and Prc.Book_Number='" + DepositorNo + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Godown_Code='" + Godown + "'";
                        // Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    }
                }
                else
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.AcceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM CSMS.dbo.[Acceptance_Note_kharif2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["BranchId"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                SqlCommand cmd2 = new SqlCommand(query2, con, sqltran);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count >= 1)
                {
                    for (int z = 0; z < ds2.Tables[0].Rows.Count; z++)
                    {
                        //string StorageReceipt_Id = "";
                        string Distt_ID = ds2.Tables[0].Rows[z]["Distt_ID"].ToString();
                        string IssueCenter_ID = ds2.Tables[0].Rows[z]["IssueCenter_ID"].ToString();
                        string Purchase_Center = ds2.Tables[0].Rows[z]["Purchase_Center"].ToString();
                        string Dispatch_Date = ds2.Tables[0].Rows[z]["Dispatch_Date"].ToString();
                        string TC_Number = ds2.Tables[0].Rows[z]["TC_Number"].ToString();
                        string Truck_Number = ds2.Tables[0].Rows[z]["Truck_No"].ToString();
                        string Commodity_Id = hdnCommodityID.Text;
                        string Crop_Year = ds2.Tables[0].Rows[z]["CropYear"].ToString();
                        int No_of_Bags = Convert.ToInt32(ds2.Tables[0].Rows[z]["Recd_Bags"]);
                        string Acceptance_No = ds2.Tables[0].Rows[z]["Acceptance_No"].ToString();
                        string Acceptance_Date = ds2.Tables[0].Rows[z]["Acceptance_Date"].ToString();
                        Godown = ds2.Tables[0].Rows[z]["godown"].ToString();
                        string IssueId = ds2.Tables[0].Rows[z]["IssueID"].ToString();
                        string Branch_Id = ds2.Tables[0].Rows[z]["Branch_Id"].ToString();
                        string TaulParchi = ds2.Tables[0].Rows[z]["TaulParchi"].ToString();
                        string Weighbridge_ID = ds2.Tables[0].Rows[z]["Weighbridge_ID"].ToString();
                        string Weighbridge_TaulParchi = ds2.Tables[0].Rows[z]["Weighbridge_TaulParchi"].ToString();
                        float Weighbridge_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Weighbridge_Qty"]);
                        float Rec_Qty = Convert.ToSingle(ds2.Tables[0].Rows[z]["Recd_Qty"]);
                        string Depositor_Form_No = ds2.Tables[0].Rows[z]["Depositor_Form_No"].ToString();
                        Decimal Moisture = Convert.ToDecimal(ds2.Tables[0].Rows[z]["Moisture"]);
                        string Book_NO = ds2.Tables[0].Rows[z]["Book_No"].ToString();

                        int JuteNew = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_JuteNew"]);
                        int RecdBagsPP = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_PP"]);
                        int RecdBagsJuteOld = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_JuteOld"]);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string RQry = "";
                        if ((hdnCommodityID.Text.ToString() == "3") || (hdnCommodityID.Text.ToString() == "129") || (hdnCommodityID.Text.ToString() == "131"))
                        {
                            if (ddlDepositor.SelectedValue == "181")
                            {
                                RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMR2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            }
                            else
                            {
                                RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMR2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            }
                        }
                        else
                        {
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                        }

                        cmd = new SqlCommand(RQry, con, sqltran);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }
        //Start Check
        string qryc1 = "";
        if ((ddlcropyear.SelectedItem.Text == "2024-2025" || ddlcropyear.SelectedItem.Text == "2025-2026") && (hdnCommodityID.Text.ToString() == "3" || hdnCommodityID.Text.ToString() == "129" || hdnCommodityID.Text.ToString() == "131"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMR2020 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif2020 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        cmd = new SqlCommand(qryc1, con);
        con.Open();
        string aa1 = cmd.ExecuteScalar().ToString();
        con.Close();
        if (Convert.ToInt32(aa1) == Convert.ToInt32(lblRcdBags.Text))
        {
            Session["Mode"] = "Add";
            Session["WLCDepSource"] = "01";
            Session["ProcComm"] = hdnCommodityID.Text.ToString();
            Session["RecDepositor"] = hdnDepositorID.Text.ToString();
            Session["SendBags"] = lblSendBags.Text.Trim().ToString();
            Session["SendQty"] = lblSendQty.Text.Trim().ToString();
            Session["RecBags"] = lblRcdBags.Text.Trim().ToString();
            Session["RecQty"] = lblRecdQty.Text.Trim().ToString();
            Session["GodownID"] = hdnGodownID.Text.ToString();
            Session["DepositDate"] = lblDepostDate.Text;
            Session["DF_Receipt_ID"] = DFReceive_ID;
            Response.Redirect("~/Procurement/WLC_ProStacking_S2024_25_For_FCI.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Try again later'); </script> ");
        }
        //End Check

    }
    public void GetWLC_Depositor_FN()
    {
        try
        {
            string Godown_Id = hdnGodownID.Text;
            if (Godown_Id != "" && Godown_Id != null)
            {
                // string GodownId = ddl_godown.SelectedValue;
                string GodownId = Godown_Id;
                string District_Id = Session["Depot_DistID"].ToString();
                District_Id = District_Id.Substring(2, 2);
                DFReceive_ID = "";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string QueryMax = "";
                if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                {
                    //QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and Godown='" + Godown_Id + "'";
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Branch_Id='" + Session["BranchId"].ToString() + "' and Godown='" + Godown_Id + "'";
                }

                cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
                string str3 = cmd.ExecuteScalar().ToString();
                if (ddlcropyear.SelectedItem.Text == "2024-2025")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["Depot_DepotID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy")+ "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH24" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["Depot_DepotID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K25" + Convert.ToString(Convert.ToInt64(str3));

                    }

                }
               else if (ddlcropyear.SelectedItem.Text == "2025-2026")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["Depot_DepotID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy")+ "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH25" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["Depot_DepotID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K26" + Convert.ToString(Convert.ToInt64(str3));

                    }

                }
                ViewState["AID"] = str3;
                //lbl_whrno.Text = WHR_Id.ToString();
                //lbl_didid.Text = str3;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Something Error with DFN'); </script> ");
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
}