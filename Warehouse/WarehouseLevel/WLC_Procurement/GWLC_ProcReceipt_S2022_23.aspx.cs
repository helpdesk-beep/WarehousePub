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


public partial class WarehouseLevel_GWLC_ProcReceipt_S2022_23 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltran;
    SqlCommand cmd = null;
    string DFReceive_ID = "";
    string CheckPaymentNotDone = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["GodownID_New"] != null) && (Session["G_BranchID"] != null))
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                //lblDepositDetail.Text = Resources.hindi.lblDepositDetail;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                //lblSourceOfDeposit.Text = Resources.hindi.lblSourceOfDeposit;
            }
            if (!IsPostBack)
            {
                //lblTotalBags.Text = ViewState["TotalBags"].ToString();
                string script = "$(document).ready(function () { $('[id*=ddlArrival_Source]').click();  });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                //if (RadioButton1.Checked)
                //{
                //    pnldate.Visible = false;
                //}
                //fillCropYear();


                // Check Payment Status
                //PaymenrNotDone();
                //END

                fillGodnList();
                fillDepositorType();
                ddldepositortype_SelectedIndexChanged(sender, e);
                //FillArrivalSourceddl();
                //fillProcNew();
                //fillCommodity();
                //FillGodown();
                //rbdate.Visible = false;
                fillCropYear();
                if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8") && ddlcropyear.SelectedValue.ToString() == "2019-20")
                {
                    fillProcKharif2019();
                }
                else if (ddlProcCmd.SelectedValue == "22")
                {
                    //fillProcRabi2020();
                }
                else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                {
                    fillProcCMS2019();
                }
                else if (ddlProcCmd.SelectedValue == "52")
                {
                    fillProcArhar2019();
                }
                else
                {
                    //fillProcNew();
                }

            }
            if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedValue == "2022-23")
            {
                IDLuster.Visible = true;
            }
            else
            {
                IDLuster.Visible = false;

            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
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
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            try
            {
                string District_Id = Session["Depot_DistID"].ToString();
                ddlDepositor.Items.Clear();
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    //For Institution
                    string query2 = "";
                    //if (District_Id == "2333" || District_Id == "2309" || District_Id == "2311" || District_Id == "2327")
                    //{
                    //    query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','4679','181')";
                    //}
                    //else
                    //{
                    //query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535')";
                    //query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679')";
                    query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','10535','4679','181','15478')";

                    //}
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ddlDepositor.DataSource = ds2;
                        ddlDepositor.DataTextField = "Depositor_Name";
                        ddlDepositor.DataValueField = "Depositor_ID";
                        ddlDepositor.DataBind();
                        ddlDepositor.Items.Insert(0, "--Select--");
                        ddlDepositor.SelectedValue = "129";
                    }
                    //For Institution

                }
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    //For Co-op Societies
                    string query2 = "";
                    if (ddlcropyear.SelectedItem.Text == "2019-2020")
                    {
                        //query2 = "select distinct Dp.Depositor_Name as Depositor_Name,Dp.Depositor_ID as Depositor_ID from tbl_MetaData_DEPOSITOR as Dp where Dp.BranchId='" + Session["G_BranchID"].ToString() + "' and Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Rabi2019 as KR where KR.Branch_Id='" + Session["G_BranchID"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_CSM2019 as KR where KR.Branch_Id='" + Session["G_BranchID"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Tuar2019 as KR where KR.Branch_Id='" + Session["G_BranchID"].ToString() + "' and KR.Book_No='Rejected')";
                        query2 = "select distinct Dp.Depositor_Name as Depositor_Name,Dp.Depositor_ID as Depositor_ID from tbl_MetaData_DEPOSITOR as Dp where Dp.BranchId='" + Session["BranchId"].ToString() + "' and Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Rabi2019 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Rabi2019 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_CSM2019 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected')";
                    }
                    else
                    {
                        query2 = "select distinct Dp.Depositor_Name as Depositor_Name,Dp.Depositor_ID as Depositor_ID from tbl_MetaData_DEPOSITOR as Dp where Dp.BranchId='" + Session["BranchId"].ToString() + "' and Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_Kharif2018 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected') or Dp.LicNum in (select distinct KR.Purchase_Center from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as KR where KR.Branch_Id='" + Session["BranchId"].ToString() + "' and KR.Book_No='Rejected')";
                    }

                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2);
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ddlDepositor.DataSource = ds2;
                        ddlDepositor.DataTextField = "Depositor_Name";
                        ddlDepositor.DataValueField = "Depositor_ID";
                        ddlDepositor.DataBind();
                        ddlDepositor.Items.Insert(0, "--Select--");
                        //ddlDepositor.SelectedValue = "10535";
                    }
                    //For Institution

                }
                else
                {
                    ddlDepositor.DataSource = null;
                    ddlDepositor.DataTextField = "Depositor_Name";
                    ddlDepositor.DataValueField = "Depositor_ID";
                    ddlDepositor.DataBind();
                    ddlDepositor.Items.Insert(0, "--Select--");
                    // ddlDepositor.SelectedValue = "129";

                }
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Depositor Found!')", true);

                //    //Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depositor Found!'); </script> ");
                //}
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
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedItem.Text == "2019-2020")
            {
                fillProcRabi2020();
            }
            else
            {
                fillProcNew();
            }
            //fillProcNew();
            //if (ddlDepositor.SelectedItem.Text == "MPSCSC")
            //{
            //    fillProcRabi2019();
            //}
            //if (ddlDepositor.Items.Count > 0)
            //{
            //    if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPSCSC")
            //    {
            //        Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
            //        Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
            //        //FillArrivalSourceddl();

            //    }
            //    else if (ddlDepositor.SelectedItem.Text == "DMO Markfed")
            //    {
            //        Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
            //        Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
            //        Session["WLCDepSource"] = "NON-MPSCSC";
            //        Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

            //    }
            //    else if (ddldepositortype.SelectedItem.Text != "Institution")
            //    {
            //        Session["depositortypeA"] = ddldepositortype.SelectedItem.Text;
            //        Session["DepositorA"] = ddlDepositor.SelectedItem.Text;
            //        Session["WLCDepSource"] = "NON-MPSCSC";
            //        Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");

            //    }
            //    else
            //    {
            //        //GvuDispatchFromPC.DataSource = null;
            //        //GvuDispatchFromPC.DataBind();
            //        //gvuFrom_RailHead.DataSource = null;
            //        //gvuFrom_RailHead.DataBind();
            //        //GvuFromFCI_OTHDepot.DataSource = null;
            //        //GvuFromFCI_OTHDepot.DataBind();
            //        //ddlArrival_Source.DataSource = "";
            //        //ddlArrival_Source.DataBind();
            //        //ddlArrival_Source.Items.Insert(0, "--Select--");
            //        //ddl_society.DataSource = "";
            //        //ddl_society.DataBind();


            //    }
            //}
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    protected void ddlProcCmd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProcCmd.SelectedValue == "22" && ddlDepositor.SelectedItem.Text != "MPSCSC")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
            //fillGodnList();
        }
        else if (ddlProcCmd.SelectedValue != "22" && ddlDepositor.SelectedItem.Text == "MPSCSC")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
            //fillGodnList();
        }
        else
        {
            fillGodnList();
        }
        //lblcropyr.Text = ddlcropyear.SelectedItem.Text.ToString();
        ////FillArrivalSourceddl();
        ////fillProcRabi2019();
        //if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8") && ddlcropyear.SelectedValue.ToString() == "2019-20")
        //{
        //    fillProcKharif2019();
        //}
        //else if (ddlProcCmd.SelectedValue == "22")
        //{
        //    fillProcRabi2020();
        //}
        //else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
        //{
        //    fillProcCMS2019();
        //}
        //else if (ddlProcCmd.SelectedValue == "52")
        //{
        //    fillProcArhar2019();
        //}
        //else
        //{
        //    fillProcNew();
        //}

    }
    protected void lnkDepositProcNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepositor.SelectedItem.Text != "--Select--")
            {
                LinkButton lnkProc = (LinkButton)sender;
                string[] CommandArgument = lnkProc.CommandArgument.Split(',');

                Session["whrreq"] = CommandArgument[0];
                Session["AccepDate"] = CommandArgument[1];
                string acc = Session["AccepDate"].ToString();
                Session["Mode"] = "Add";
                Session["WLCDepSource"] = "01";
                //Session["ProcComm"] = ddlProcCmd.SelectedValue.ToString();
                //Session["RecDepositor"] = ddlDepositor.SelectedValue.ToString();
                //from godown owner
                // Session["RecType"] = ddlProcCmd.SelectedItem.Text;
                //Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
                // Response.Redirect("WLC_FRM_01_02_03_Receipt.aspx");
                //Response.Redirect("~/BranchPages/Receiptfrom_Procurement.aspx");
                Response.Redirect("~/Procurement/WLC_Proc_WHReceiptStacking.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor...'); </script> ");
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }

    }
    protected void gdnewproc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gdnewproc.PageIndex = e.NewPageIndex;
            //fillProcNew();
            //fillProcRabi2019();
            //fillProcCMS2019();
            if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8") && ddlcropyear.SelectedValue.ToString() == "2019-20")
            {
                fillProcKharif2019();
            }
            else if (ddlProcCmd.SelectedValue == "22")
            {
                fillProcRabi2020();
            }
            else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
            {
                fillProcCMS2019();
            }
            else
            {
                fillProcNew();
            }
        }
        catch (Exception ex)
        {
            StringBuilder str1 = new StringBuilder();
            str1.Append("<script>");
            str1.Append("alert('" + "Some error has occured, try again" + "," + "Error:-" + ex.Message + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str1.ToString());
        }
    }
    protected void fillCropYear()
    {
        ListItem[] items = new ListItem[5];

        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        //items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        //items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        //ddlcropyear.Items.Insert(0, "All");
        ddlcropyear.SelectedIndex = 0;
        //ddlcropyear.SelectedIndex = 2;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
    protected void fillProcNew()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if ((ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlDepositor.SelectedItem.Value == "129")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA')";     
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "')";  
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14") && (ddlDepositor.SelectedItem.Value == "129" || ddlDepositor.SelectedItem.Value == "4679"))
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA')";       
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "')";       
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA')";  
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "')";        
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";       
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    int PFrom = Depositor_Name.IndexOf("0");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity,godown,date and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcRabi2020()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.Depositor_Form_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.Depositor_Form_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    //int PFrom = Depositor_Name.IndexOf("4");
                    int PFrom = Depositor_Name.IndexOf("(");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcRabi2021()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    //else if (ddlProcCmd.SelectedValue == "3")
                    //{
                    //    //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    //    //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    //    Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2021-2022' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    //}
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    //int PFrom = Depositor_Name.IndexOf("4");
                    int PFrom = Depositor_Name.IndexOf("(");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcRabi2024_For_DCI()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {

                    if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                    {

                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2024_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif_2024_25 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {

                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity) as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice+AD.Accept_GradeARice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_MPWLC) as Godown from csms.dbo.CMR_QualityInspection_FCI as AD INNER JOIN tbl_MetaData_GODOWN_2018 gdn on AD.Godown_MPWLC=gdn.Godown_ID INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId where MD.BranchId='" + Session["BranchId"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_MPWLC='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Commodity='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["BranchId"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

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
                            txtdepositdate.Enabled = false;
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
    protected void fillProcKharif2024()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {

                    if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2024_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif_2024_25 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    //else if (ddlProcCmd.SelectedValue == "3")
                    //{
                    //    //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    //    //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    //    Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2021-2022' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    //}
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'0' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                string msg = "Insert Error:";
                msg += ex.Message;
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

    protected void fillProcKharif2025()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {

                    if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2025_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif_2025_26 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    //else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    //{
                    //    Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    //}
                    //else if (ddlProcCmd.SelectedValue == "3")
                    //{
                    //    //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    //    //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    //    Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2021-2022' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    //}
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'0' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcKharif2023()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {

                    if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2023 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2024 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    //else if (ddlProcCmd.SelectedValue == "3")
                    //{
                    //    //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    //    //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    //    Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2021-2022' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    //}
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'0' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    //int PFrom = Depositor_Name.IndexOf("4");
                    int PFrom = Depositor_Name.IndexOf("(");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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

    protected void fillProcRabi2022()
    {
        if ((Session["Depot_DistID"] != null) && (Session["G_BranchID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                string LusterLossc = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "22")
                    {

                        if (ddlProcCmd.SelectedValue == "22")
                        {
                            if (DdlLusterLoss.SelectedValue == "0")
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Choose Luster Loss'); </script> ");
                            }
                            else
                            {

                                if (DdlLusterLoss.SelectedValue == "1")
                                {
                                    LusterLossc = "N";
                                }
                                else
                                {
                                    LusterLossc = "Y";
                                }
                                //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                                //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2022_WH as AD left join MPSCSC.dbo.LusterLoss_acceptanceandrejectionMapping_Rabi2022 as LusterLoss on LusterLoss.Acceptance_No = AD.Acceptance_No where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and LusterLoss.RejectionduetoLusterLoss = '" + LusterLossc + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                                Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2022_WH as AD left join MPSCSC.dbo.LusterLoss_acceptanceandrejectionMapping_Rabi2022 as LusterLoss on LusterLoss.Acceptance_No = AD.Acceptance_No and LusterLoss.RejectionduetoLusterLoss = '" + LusterLossc + "' where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "'  and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                            }
                        }

                    }
                    else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2023 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2023 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2024 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }

                    else if (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,AD.RecdBags_JuteNew,AD.RecdBags_PP,AD.RecdBags_JuteOld ,AD.Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Pulses2022_WH as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2022 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.acceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.acceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_kharif2021 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2021 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

                    }
                    //else if (ddlProcCmd.SelectedValue == "3")
                    //{
                    //    //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    //    //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                    //    Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2021-2022' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";

                    //}
                    else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMS2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Code='" + Session["G_BranchID"].ToString() + "' and sss.Godown_Code='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        //Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='2020-2021' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";
                        Query = "select AD.Acceptance_No as DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Date,103) as Acceptance_Date,AD.DO_Number as TC_Number,AD.Truck_No as Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id='3') as Commodity_Name,AD.Bags as Recd_Bags,AD.Accept_CommonRice as Recd_Qty,AD.Bags as Recd_Bags2,AD.Accept_CommonRice as Recd_Qty2,AD.JuteNewBags RecdBags_JuteNew,'0' AS RecdBags_PP,AD.JuteOldBags RecdBags_JuteOld ,'' AS Moisture,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Godown_Code) as Godown from csms.dbo.CMR_QualityInspection_2019 as AD where AD.Branch_Code='" + Session["G_BranchID"].ToString() + "' and AD.CropYear='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Godown_Code='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Rejected=0 and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_CMR2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "') order by AD.Date asc";


                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.NetWeight as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Rabi2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.AcceptanceQty as Recd_Qty,AD.Recd_Bags as Recd_Bags2,AD.AcceptanceQty as Recd_Qty2,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from CSMS.dbo.Acceptance_Note_kharif2020 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["G_BranchID"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Recd_Godown='" + ddl_godown.SelectedValue + "' and CONVERT(varchar(10),AD.Acceptance_Date,101)='" + getDate_MDY(txtdepositdate.Text) + "' and AD.Book_No!='Rejected' and AD.Acceptance_No not in (select sss.Acceptance_No from Receive_Proc_Kharif2020 As sss where sss.Branch_Id='" + Session["G_BranchID"].ToString() + "' and sss.Godown='" + ddl_godown.SelectedValue + "' and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    //int PFrom = Depositor_Name.IndexOf("4");
                    int PFrom = Depositor_Name.IndexOf("(");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcKharif2019()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";

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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    //int PFrom = Depositor_Name.IndexOf("4");
                    int PFrom = Depositor_Name.IndexOf("(");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcCMS2019()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "33" || ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CSM2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    //int PFrom = Depositor_Name.IndexOf("2");
                    int PFrom = Depositor_Name.IndexOf("0");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom, 6);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    protected void fillProcArhar2019()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            //if (ddlDepositor.SelectedItem.Text == "NAFED")
            //{
            try
            {
                string Query = "";
                if (ddldepositortype.SelectedItem.Text == "Institution")
                {
                    if (ddlProcCmd.SelectedValue == "52")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Tuar2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                else if (ddldepositortype.SelectedItem.Text == "Co-op Societies")
                {
                    string Depositor_Name = "";
                    Depositor_Name = ddlDepositor.SelectedItem.Text;
                    //int PFrom = Depositor_Name.IndexOf("(");
                    int PFrom = Depositor_Name.IndexOf("2");
                    //int PTo=Depositor_Name.IndexOf(")");
                    string Proc_Center_Id = "";
                    //Proc_Center_Id = Depositor_Name.Substring(PFrom + 1, 8);
                    Proc_Center_Id = Depositor_Name.Substring(PFrom, 8);

                    if (ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else if (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "14")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Kharif2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                    }
                    else
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Reject_Bags as Recd_Bags,AD.Rejected_NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Dalhan2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No='Rejected' and AD.Purchase_Center='" + Proc_Center_Id + "' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                            //tr_Disfromprc.Visible = true;
                            //trfromrailhead.Visible = false;
                            //trfromothdepot.Visible = false;
                            trnewproc.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = "No pending Depositor Form for this commodity and depositor";
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
                string msg = "Insert Error:";
                msg += ex.Message;
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
    private void fillGodnList()

    {
        string query = "";
        if (Session["Depot_DistID"] != null)
        {
            //    if (ddlProcCmd.SelectedValue == "22")
            //    {
            //        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Godown_ID in (select distinct Recd_Godown from MPSCSC.dbo.Acceptance_Note_Rabi2020 where BranchID='" + Session["G_BranchID"].ToString() + "') and Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Steel Silo') and Godown_ID='" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] Asc ";
            //    }
            //    else if (ddlProcCmd.SelectedValue == "63"|| ddlProcCmd.SelectedValue == "64"|| ddlProcCmd.SelectedValue == "33")
            //    {
            //        query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Godown_ID in (select distinct Recd_Godown from MPSCSC.dbo.Acceptance_Note_CSM2020 where BranchID='" + Session["G_BranchID"].ToString() + "') and Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Steel Silo') and Godown_ID='" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] Asc ";
            //    }
            //    else
            //    {
            //query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Steel Silo') and Godown_ID='" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] Asc ";
            //query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Steel Silo','Oil-Fed') and Godown_ID='" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] Asc ";
            query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN_2018] WHERE BranchId = '" + Session["G_BranchID"].ToString() + "' and Hired_Type in ('Joint Venture(JV)','Silo Bags','WDRA','PVT.PEG','Tribal Scheme','Steel Silo','Oil-Fed','CAP-PMS','Owned','Hired','BOT','BOT-AUB') and Godown_ID='" + Session["GodownID_New"].ToString() + "'  ORDER BY [Godown_Name] Asc ";

            //}
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
                //ddl_godown.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtdepositdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Choose Date...'); </script> ");
            fillGodnList();
        }
        else if (ddlProcCmd.SelectedValue == "22" && ddlDepositor.SelectedItem.Text != "MPSCSC")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
            fillGodnList();
        }
        else if (ddlProcCmd.SelectedValue != "22" && ddlDepositor.SelectedItem.Text == "MPSCSC")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Depositor and Commodity...'); </script> ");
            fillGodnList();
        }
        else
        {
            fillProcRabi2020();
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
            lblTotalQtySend.Text = decimal.Round((decimal)WLC_Qty, 4).ToString();
            lblNoofAC.Text = CountDF.ToString();
        }
    }

    //private void PaymenrNotDone()
    //{
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Payment_Status_Not_Done", jvscon);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@RegID", Session["WLC_Reg_No"].ToString());
    //        SqlDataAdapter da3 = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        da3.Fill(dt);
    //        if (dt.Rows.Count > 0)
    //        {
    //            Session["Paymentnotdoneregid"] = dt.Rows[0]["Paymentnotdoneregid"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // lblMsg.Text = ex.Message.ToString();
    //    }
    //}
    protected void txtdepositdate_TextChanged(object sender, EventArgs e)
    {
        //if (Session["Paymentnotdoneregid"].ToString() == "0" || Session["Paymentnotdoneregid"].ToString() == "")
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपके द्वारा ऑफ़र का पेमेंट नहीं किया गया है कृपया पहले ऑफर का पेमेंट करे |...'); </script> ");
        //}
        //else { 

        if (txtdepositdate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Choose Date...'); </script> ");
            //fillGodnList();
        }
        //else if (ddlProcCmd.SelectedValue == "22" && ddlDepositor.SelectedItem.Text != "MPSCSC")
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
            //fillProcRabi2020();
            //
            //if (ddlcropyear.SelectedItem.Text == "2024-2025")
            //{
            //    fillProcKharif2024();
            //}

            if (ddlcropyear.SelectedItem.Text == "2025-2026")
            {
                fillProcKharif2025();
            }
            else if (ddlcropyear.SelectedItem.Text == "2024-2025")
            {
                if (ddlDepositor.SelectedValue.ToString() == "181")
                {
                    fillProcRabi2024_For_DCI();
                }
                else
                {
                    fillProcKharif2024();
                }
            }


            else if (ddlcropyear.SelectedItem.Text == "2023-2024")
            {
                fillProcKharif2023();
            }
            else if (ddlcropyear.SelectedItem.Text == "2022-2023")
            {
                fillProcRabi2022();
            }

            else if (ddlcropyear.SelectedItem.Text == "2021-2022")
            {
                fillProcRabi2021();
            }
            else
            {
                fillProcRabi2020();
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
            //lblTotalQtySend.Text = decimal.Round((decimal)WLC_Qty, 4).ToString();
            lblTotalQtySend.Text = decimal.Round((decimal)WLC_Qty, 5).ToString();
            lblNoofAC.Text = CountDF.ToString();
        }
        //}


    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //ViewState["TotalBags"] = lblTotalBags.Text;
        //string changedLabelValue = hdnLabelState.Value;
        try
        {
            decimal sum = 0;
            decimal sumQty = 0;
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
            //lblRcdBags.Text = hdnLabelState.Value;
            //lblRecdQty.Text = hdnLabelStateQty.Value;
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
            else
            {
                GetReceivedSummary();
            }
            //if (ddlDepositor.SelectedItem.Text != "--Select--")
            //{
            //    GetReceivedSummary();
            //}
            //else
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Depositor...'); </script> ");
            //}
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
        lblDepostDate.Text = txtdepositdate.Text;
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
                if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlcropyear.SelectedItem.Text == "2025-2026")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif_2025_26 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
               else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlcropyear.SelectedItem.Text == "2024-2025")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif_2024_25 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlcropyear.SelectedItem.Text == "2023-2024")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif2024 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }

                else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlcropyear.SelectedItem.Text == "2022-2023")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif2023 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                else if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedItem.Text == "2022-2023")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Rabi2022 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMS2022 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "' and Commodity_Id='" + ddlProcCmd.SelectedValue + "'";
                }
                else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMS2022 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "' and Commodity_Id='" + ddlProcCmd.SelectedValue + "'";
                }
                else if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Rabi2021 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }

                else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11") && ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif2021 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }

                else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMS2021 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "' and Commodity_Id='" + ddlProcCmd.SelectedValue + "'";
                }
                else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMS2021 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "' and Commodity_Id='" + ddlProcCmd.SelectedValue + "'";
                }
                else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                {
                    //QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                    //QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["BranchId"].ToString() + "' and Godown='" + Godown_Id + "'";

                    //QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMR2020 where Godown='" + Godown_Id + "'";

                }
                else if (ddlProcCmd.SelectedValue == "22")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Rabi2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_CMS2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                else
                {
                    QueryMax = "select isnull(Max(Aid),0)+1 from Receive_Proc_Kharif2020 where Distt_ID ='" + District_Id + "' and Branch_Id='" + Session["G_BranchID"].ToString() + "' and Godown='" + Godown_Id + "'";
                }
                cmd = new SqlCommand(QueryMax, con); // check WhrId present in whr_status table
                string str3 = cmd.ExecuteScalar().ToString();
                if (ddlcropyear.SelectedItem.Text == "2025-2026")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D220" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K20" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH2526" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH26" + Convert.ToString(Convert.ToInt64(str3));

                    }
                }
               else if (ddlcropyear.SelectedItem.Text == "2024-2025")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D220" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K20" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH24" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH25" + Convert.ToString(Convert.ToInt64(str3));

                    }
                }
                else if (ddlcropyear.SelectedItem.Text == "2023-2024")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        if (Session["G_BranchID"].ToString() == "2312002")
                        {
                            DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KHF200" + Convert.ToString(Convert.ToInt64(str3));
                        }
                        else
                        {
                            //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                            //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D220" + Convert.ToString(Convert.ToInt64(str3));
                            //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K20" + Convert.ToString(Convert.ToInt64(str3));
                            DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH200" + Convert.ToString(Convert.ToInt64(str3));
                        }
                    }
                    else
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "KH2" + Convert.ToString(Convert.ToInt64(str3));

                    }
                }

                if (ddlcropyear.SelectedItem.Text == "2022-2023")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D220" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K20" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K200" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D22" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "K2" + Convert.ToString(Convert.ToInt64(str3));

                    }
                }
                else if (ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    if ((str3 == String.Empty) || str3 == "")
                    {
                        str3 = "0";
                    }
                    if (Convert.ToInt64(str3) != 0)
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D21" + Convert.ToString(Convert.ToInt64(str3));

                    }
                    else
                    {
                        string Depotid = Session["G_BranchID"].ToString();
                        DFReceive_ID = "";
                        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D21" + Convert.ToString(Convert.ToInt64(str3));
                    }
                }
                //else
                //{
                //    if ((str3 == String.Empty) || str3 == "")
                //    {
                //        str3 = "0";
                //    }
                //    if (Convert.ToInt64(str3) != 0)
                //    {
                //        string Depotid = Session["G_BranchID"].ToString();
                //        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                //        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D" + Convert.ToString(Convert.ToInt64(str3));

                //    }
                //    else
                //    {
                //        string Depotid = Session["G_BranchID"].ToString();
                //        DFReceive_ID = "";
                //        //DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + "D" + Convert.ToString(Convert.ToInt64(str3));
                //        DFReceive_ID = GodownId + System.DateTime.Now.Date.ToString("yy") + ddlProcCmd.SelectedValue.ToString() + "D" + Convert.ToString(Convert.ToInt64(str3));
                //    }
                //}
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

                if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedItem.Text == "2022-2023")
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_Rabi2022_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }

                //Kharif 2023-24 WHR
                else if (ddlcropyear.SelectedItem.Text == "2024-2025" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_kharif2024_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                //Kharif 2025-26 WHR
                else if (ddlcropyear.SelectedItem.Text == "2025-2026" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_kharif2025_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if (ddlcropyear.SelectedItem.Text == "2023-2024" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_kharif2023] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }

                //END

                else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_kharif2022_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }


                else if (ddlProcCmd.SelectedValue == "22" && ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2021] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2021] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_CSM2022_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                {
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No,Prc.RecdBags_JuteNew,Prc.RecdBags_PP,Prc.RecdBags_JuteOld FROM MPSCSC.dbo.[Acceptance_Note_Pulses2022_WH] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Pulses2021] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else if ((hdnCommodityID.Text.ToString() == "3") || (hdnCommodityID.Text.ToString() == "129") || (hdnCommodityID.Text.ToString() == "131"))
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
                //else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                //{
                //    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                //    //query2 = "SELECT Prc.District as Distt_ID,prc.issueCentre_code as IssueCenter_ID,'' as Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.Book_Number as TC_Number,prc.Truck_No as Truck_No,Prc.CropYear as CropYear,Prc.Acceptance_No, convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.StackNumber as IssueID,prc.Godown_Code as godown,ISNULL(prc.Bags,0) as Recd_Bags,ISNULL(Prc.Accept_CommonRice,0) AS Recd_Qty,'3' as CommodityId,Prc.Book_Number as Depositor_Form_No,Prc.Branch_Code as Branch_Id,Prc.Agreement_ID as TaulParchi,Prc.Inspector_ID as Weighbridge_ID ,'' as Weighbridge_TaulParchi,ISNULL(0,0) as Weighbridge_Qty,0 as Moisture,Prc.Book_Number as Book_No FROM csms.dbo.CMR_QualityInspection_2019 as Prc where Prc.Acceptance_No is not null and Prc.Branch_Code='" + Session["G_BranchID"].ToString() + "' and Prc.Book_Number='" + DepositorNo + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Godown_Code='" + Godown + "'";
                //    query2 = "SELECT Prc.District as Distt_ID,prc.issueCentre_code as IssueCenter_ID,'' as Purchase_Center,convert(varchar(10),prc.Date,101) as Dispatch_Date,prc.Book_Number as TC_Number,prc.Truck_No as Truck_No,Prc.CropYear as CropYear,Prc.Acceptance_No, convert(varchar(10),Prc.Date,101) as Acceptance_Date,Prc.StackNumber as IssueID,prc.Godown_Code as godown,ISNULL(prc.Bags,0) as Recd_Bags,ISNULL(Prc.Accept_CommonRice,0) AS Recd_Qty,'3' as CommodityId,Prc.Book_Number as Depositor_Form_No,Prc.Branch_Code as Branch_Id,Prc.Agreement_ID as TaulParchi,Prc.Inspector_ID as Weighbridge_ID ,'' as Weighbridge_TaulParchi,ISNULL(0,0) as Weighbridge_Qty,Prc.JuteNewBags RecdBags_JuteNew,0 AS RecdBags_PP,Prc.JuteOldBags RecdBags_JuteOld ,0 AS Moisture,Prc.Book_Number as Book_No FROM csms.dbo.CMR_QualityInspection_2019 as Prc where Prc.Acceptance_No is not null and Prc.Branch_Code='" + Session["G_BranchID"].ToString() + "' and Prc.Book_Number='" + DepositorNo + "' and convert(varchar(10),Prc.Date,103)='" + AccptDate + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Godown_Code='" + Godown + "'";
                //}
                else if (ddlProcCmd.SelectedValue == "22")
                {
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                }
                else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                {
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_CSM2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                }
                else if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8") && ddlcropyear.SelectedItem.Text == "2021-2022")
                {
                    //query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.NetWeight,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_Rabi2020] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.acceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM MPSCSC.dbo.[Acceptance_Note_kharif2021] as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";

                }
                else
                {
                    query2 = "SELECT Prc.Distt_ID,prc.IssueCenter_ID,prc.Purchase_Center,convert(varchar(10),prc.Dispatch_Date,101) as Dispatch_Date,prc.TC_Number,prc.Truck_Number as Truck_No,Prc.Crop_Year as CropYear,Prc.Acceptance_No,convert(varchar(10),Prc.Acceptance_Date,101) as Acceptance_Date,Prc.Receipt_Id as IssueID,prc.Recd_Godown as godown,ISNULL(prc.Recd_Bags,0) as Recd_Bags,ISNULL(Prc.AcceptanceQty,0) AS Recd_Qty,Prc.Commodity_Id as CommodityId,Prc.DepositerNo as Depositor_Form_No,Prc.Branch_Id,Prc.TaulParchi,Prc.Weighbridge_ID,Prc.Weighbridge_TaulParchi,ISNULL(Prc.Weighbridge_LoadedQty,0) as Weighbridge_Qty,Moisture,Book_No FROM CSMS.dbo.Acceptance_Note_kharif2020 as Prc where Prc.DepositerNo is not null and Prc.Branch_Id='" + Session["G_BranchID"].ToString() + "' and Prc.DepositerNo='" + DepositorNo + "' and convert(varchar(10),Prc.Acceptance_Date,103)='" + AccptDate + "' and Prc.Commodity_Id='" + hdnCommodityID.Text + "' and Prc.Acceptance_No='" + AccptNo + "' and Prc.Recd_Godown='" + Godown + "'";
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

                        //int JuteNew = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_JuteNew"]);
                        //int RecdBagsPP = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_PP"]);
                        //int RecdBagsJuteOld = Convert.ToInt16(ds2.Tables[0].Rows[z]["RecdBags_JuteOld"]);
                        int RecdBags_JuteNew = Convert.ToInt32(ds2.Tables[0].Rows[z]["RecdBags_JuteNew"]);
                        int RecdBags_PP = Convert.ToInt32(ds2.Tables[0].Rows[z]["RecdBags_PP"]);
                        int RecdBags_JuteOld = Convert.ToInt32(ds2.Tables[0].Rows[z]["RecdBags_JuteOld"]);
                        //string Created_Date = "";
                        //string IP_Address = "";

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        string RQry = "";

                        if (ddlcropyear.SelectedItem.Text == "2022-2023" && ddlProcCmd.SelectedValue == "22")
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2022]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "','" + RecdBags_JuteNew + "','" + RecdBags_PP + "','" + RecdBags_JuteOld + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2024-2025" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif_2024_25]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2025-2026" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif_2025_26]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2023-2024" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2024]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }

                        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2023]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && ddlProcCmd.SelectedValue == "22")
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2021]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2021]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2022]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2022]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "','" + RecdBags_JuteNew + "','" + RecdBags_PP + "','" + RecdBags_JuteOld + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                        {
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2022]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty,RecdBags_JuteNew,RecdBags_PP,RecdBags_JuteOld) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "','" + RecdBags_JuteNew + "','" + RecdBags_PP + "','" + RecdBags_JuteOld + "')";

                        }
                        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "92" || ddlProcCmd.SelectedValue == "27"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2021]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

                        }
                        else if (ddlProcCmd.SelectedValue == "3" || ddlProcCmd.SelectedValue == "129" || ddlProcCmd.SelectedValue == "131")
                        {
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMR2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                        }
                        else if (ddlProcCmd.SelectedValue == "22")
                        {
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                        }
                        else if (ddlProcCmd.SelectedValue == "63" || ddlProcCmd.SelectedValue == "64" || ddlProcCmd.SelectedValue == "33")
                        {
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_CMS2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                        }
                        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8"))
                        {
                            //RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Rabi2020]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";
                            RQry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[Receive_Proc_Kharif2021]([DF_Receipt_Id],[Distt_ID],[IssueCenter_ID],[Purchase_Center],[Dispatch_Date],[TC_Number],[Truck_Number],[Commodity_Id],[Crop_Year],[No_of_Bags],[Acceptance_No],[Acceptance_Date],[Godown],[IssueId],[Branch_Id],[TaulParchi],[Weighbridge_ID],[Weighbridge_TaulParchi],[Weighbridge_Qty],[Created_Date],[IP_Address],Rec_Qty,Depositor_Form_No,Moisture,Book_No,Aid,WLC_Bags,WLC_Qty) VALUES ('" + DFReceive_ID + "','" + Distt_ID + "','" + IssueCenter_ID + "','" + Purchase_Center + "','" + Dispatch_Date + "','" + TC_Number + "','" + Truck_Number + "','" + Commodity_Id + "','" + Crop_Year + "','" + No_of_Bags + "','" + Acceptance_No + "','" + Acceptance_Date + "','" + Godown + "','" + IssueId + "','" + Branch_Id + "','" + TaulParchi + "','" + Weighbridge_ID + "','" + Weighbridge_TaulParchi + "','" + Weighbridge_Qty + "',getdate(),'" + ClientIP + "','" + Rec_Qty + "','" + Depositor_Form_No + "','" + Moisture + "','" + Book_NO + "','" + AutoID + "','" + WLC_Bags + "','" + WLC_Qty + "')";

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

        if (hdnCommodityID.Text.ToString() == "22" && ddlcropyear.SelectedItem.Text == "2022-2023")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Rabi2022 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2024-2025" && (hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif_2024_25 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2025-2026" && (hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif_2025_26 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2023-2024" && (hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif2024 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }

        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif2023 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }

        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (hdnCommodityID.Text.ToString() == "92" || hdnCommodityID.Text.ToString() == "27"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMS2022 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (hdnCommodityID.Text.ToString() == "22" && ddlcropyear.SelectedItem.Text == "2021-2022")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Rabi2021 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if ((hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8") && ddlcropyear.SelectedItem.Text == "2021-2022")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Kharif2021 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2022-2023" && (hdnCommodityID.Text.ToString() == "63" || hdnCommodityID.Text.ToString() == "64" || hdnCommodityID.Text.ToString() == "33"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMS2022 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (hdnCommodityID.Text.ToString() == "63" || hdnCommodityID.Text.ToString() == "64" || hdnCommodityID.Text.ToString() == "33"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMS2021 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (ddlcropyear.SelectedItem.Text == "2021-2022" && (hdnCommodityID.Text.ToString() == "92" || hdnCommodityID.Text.ToString() == "27"))
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMS2021 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (hdnCommodityID.Text.ToString() == "3" || hdnCommodityID.Text.ToString() == "129" || hdnCommodityID.Text.ToString() == "131")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMR2020 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (hdnCommodityID.Text.ToString() == "22")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_Rabi2020 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (hdnCommodityID.Text.ToString() == "63" || hdnCommodityID.Text.ToString() == "64" || hdnCommodityID.Text.ToString() == "33")
        {
            qryc1 = "select SUM(WLC_Bags) from Receive_Proc_CMS2020 where DF_Receipt_Id ='" + DFReceive_ID + "'";
        }
        else if (hdnCommodityID.Text.ToString() == "13" || hdnCommodityID.Text.ToString() == "11" || hdnCommodityID.Text.ToString() == "8")
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
            Session["Crop_Year"] = lblCropYear.Text;
            Response.Redirect("~/WarehouseLevel/WLC_Procurement/GWLC_ProcStacking_S2022_23.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Try again later'); </script> ");
        }
        //End Check
    }
}