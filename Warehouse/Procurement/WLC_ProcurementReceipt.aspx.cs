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

public partial class Procurement_WLC_ProcurementReceipt : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                lblDepositDetail.Text = Resources.hindi.lblDepositDetail;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                //lblSourceOfDeposit.Text = Resources.hindi.lblSourceOfDeposit;
            }
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=ddlArrival_Source]').click();  });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                //if (RadioButton1.Checked)
                //{
                //    pnldate.Visible = false;
                //}
                //fillCropYear();
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
                    fillProcRabi2019();
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
                    fillProcNew();
                }
                
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
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string District_Id=Session["Depot_DistID"].ToString();
                ddlDepositor.Items.Clear();
                if (ddldepositortype.SelectedItem.Text == "Institution")
                    {
                        //For Institution
                        string query2 = "";
                        if (District_Id == "2333" || District_Id == "2309" || District_Id == "2311" || District_Id == "2327")
                        {
                            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','4679','181')";
                        }
                        else
                        {
                            query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_ID  in ('129','4679','10535')";
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
                fillProcRabi2019();
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
        lblcropyr.Text = ddlcropyear.SelectedItem.Text.ToString();
        //FillArrivalSourceddl();
        //fillProcRabi2019();
        if ((ddlProcCmd.SelectedValue == "13" || ddlProcCmd.SelectedValue == "11" || ddlProcCmd.SelectedValue == "8") && ddlcropyear.SelectedValue.ToString() == "2019-20")
        {
            fillProcKharif2019();
        }
        else if (ddlProcCmd.SelectedValue == "22")
        {
            fillProcRabi2019();
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
            fillProcNew();
        }
        
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
                Session["ProcComm"] = ddlProcCmd.SelectedValue.ToString();
                Session["RecDepositor"] = ddlDepositor.SelectedValue.ToString();
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
                fillProcRabi2019();
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
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        items[1] = new ListItem((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString(), (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        items[2] = new ListItem((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        items[3] = new ListItem((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString(), (DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        items[4] = new ListItem((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString(), (DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        items[5] = new ListItem((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString(), (DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
        items[6] = new ListItem((DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString(), (DateTime.Now.Year - 6) + "-" + (DateTime.Now.Year - 5).ToString().Substring(2, 2));
        ddlcropyear.Items.Insert(0, "All");
        //ddlcropyear.SelectedIndex = 1;
        ddlcropyear.SelectedIndex = 2;
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
                            if ((ddlProcCmd.SelectedValue == "8" || ddlProcCmd.SelectedValue == "11")&& ddlDepositor.SelectedItem.Value=="129")
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
    protected void fillProcRabi2019()
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
                    if (ddlProcCmd.SelectedValue == "22")
                    {
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "')";  
                        //Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_CoarseGrain2018 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
                        Query = "select AD.DepositerNo,AD.Acceptance_No,CONVERT(varchar(10),AD.Acceptance_Date,103) as Acceptance_Date,AD.TC_Number,AD.Truck_Number,(select Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as C where C.Commodity_Id=AD.Commodity_Id) as Commodity_Name,AD.Recd_Bags,AD.NetWeight as Recd_Qty,(select (GD.Godown_Name+'('+GD.Godown_ID+')') as Godown from tbl_MetaData_GODOWN_2018 as GD where GD.Godown_ID=AD.Recd_Godown) as Godown from MPSCSC.dbo.Acceptance_Note_Rabi2019 as AD where AD.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "' and AD.Branch_Id='" + Session["BranchId"].ToString() + "' and AD.Crop_Year='" + ddlcropyear.SelectedItem.Text.ToString() + "' and AD.Book_No!='Rejected' and AD.DepositerNo not in (select sss.AcceptanceNo from tbl_Storage_Arrival_Stock As sss where  sss.BranchID='" + Session["BranchId"].ToString() + "' and sss.AcceptanceNo=AD.DepositerNo and sss.IssueID='NA' and  sss.Commodity_Id=AD.Commodity_Id and sss.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') and AD.DepositerNo not in (select distinct QC.Depositor_Form_No from tbl_WLCQC_Proc_Kharif2018 as QC where QC.Branch_Id='" + Session["BranchId"].ToString() + "' and QC.Commodity_Id='" + ddlProcCmd.SelectedValue.ToString() + "') order by AD.Acceptance_Date asc";
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
                    Proc_Center_Id = Depositor_Name.Substring(PFrom+1, 6);

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
}