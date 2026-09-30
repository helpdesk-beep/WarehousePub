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
using System.Text;
using System.Resources;

public partial class IssueCenterLevel_Storage_Edit_WLC_Deposit_From : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (Session["lang"].ToString() == "Hindi")
                {
                    lblEDitDepositDetail.Text = Resources.hindi.lblEDitDepositDetail;
                    lblDepositorType.Text = Resources.hindi.lblDepositorType;
                    lblDepositorName.Text = Resources.hindi.lblDepositorName;
                    lblSourceOfDeposit.Text = Resources.hindi.lblSourceOfDeposit;
                }
                if (!IsPostBack)
                {
                    Session["RefreshButton"] = "No";

                    if (Session["WLCDepSource"] != null)
                    {
                        Session["WLCDepSource"] = null;
                    }
                    if (Session["WLC_StorageReceipt_Id"] != null)
                    {
                        Session["WLC_StorageReceipt_Id"] = null;
                    }
                    fillDepositorType();
                    ddldepositortype_SelectedIndexChanged(sender, e);
                    FillArrivalSourceddl();
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (PopMsg != null)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                    }
                }
               
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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
                ddldepositortype.SelectedItem.Text = "Institution";
            }
            else
            {
                ddldepositortype.Items.Insert(0, "--Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void FillArrivalSourceddl()
    {
        try
        {
            ddlArrival_Source.Items.Clear();
            string query = "SELECT [Source_ID],[Source_Name] FROM [Source_Arrival_Type] order by  Source_ID";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlArrival_Source.DataSource = ds.Tables[0];
                ddlArrival_Source.DataTextField = "Source_Name";
                ddlArrival_Source.DataValueField = "Source_ID";
                ddlArrival_Source.DataBind();
                ddlArrival_Source.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlArrival_Source.Items.Insert(0, "--Select--");
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid Operation, Source of arrival not found!'); </script> ");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void fillGridFCI_OTDepot()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("MPWLC_Fill_DispatchGrid_for_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dist_Id", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Dist_Id"].Value = Session["Depot_DistID"].ToString();
                cmd.Parameters.Add("@Depot_ID", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Depot_ID"].Value = Session["Depot_DepotID"].ToString();
                cmd.Parameters.Add("@S_of_arrival", SqlDbType.NVarChar, 2);
                cmd.Parameters["@S_of_arrival"].Value = ddlArrival_Source.SelectedValue;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuEditFromFCI_OTHDepot.DataSource = ds;
                    GvuEditFromFCI_OTHDepot.DataBind();
                    pnlGrid.Visible = true;
                    lbl_msg.Visible = false;
                    lbl_head.Text = "Dispatch from " + ddlArrival_Source.SelectedItem.Text.ToString() + " details";
                }
                else
                {
                    GvuEditDispatchFromPC.DataSource = null;
                    GvuEditDispatchFromPC.DataBind();
                    gvuEditFrom_RailHead.DataSource = null;
                    gvuEditFrom_RailHead.DataBind();
                    GvuEditFromFCI_OTHDepot.DataSource = null;
                    GvuEditFromFCI_OTHDepot.DataBind();
                    gvuNonMPSCSC.DataSource = null;
                    gvuNonMPSCSC.DataBind();
                    pnlGrid.Visible = false;
                    lbl_msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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

    protected void fillGridRailHead()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("MPWLC_Fill_DispatchGrid_for_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dist_Id", SqlDbType.NVarChar, 20);//10
                cmd.Parameters["@Dist_Id"].Value = Session["Depot_DistID"].ToString();
                cmd.Parameters.Add("@Depot_ID", SqlDbType.NVarChar, 20);//10
                cmd.Parameters["@Depot_ID"].Value = Session["Depot_DepotID"].ToString();
                cmd.Parameters.Add("@S_of_arrival", SqlDbType.NVarChar, 2);//25
                cmd.Parameters["@S_of_arrival"].Value = ddlArrival_Source.SelectedValue;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvuEditFrom_RailHead.DataSource = ds;
                    gvuEditFrom_RailHead.DataBind();
                    pnlGrid.Visible = true;
                    lbl_msg.Visible = false;
                    lbl_head.Text = "Dispatch from " + ddlArrival_Source.SelectedItem.Text.ToString() + " details";
                }
                else
                {
                    GvuEditDispatchFromPC.DataSource = null;
                    GvuEditDispatchFromPC.DataBind();
                    gvuEditFrom_RailHead.DataSource = null;
                    gvuEditFrom_RailHead.DataBind();
                    GvuEditFromFCI_OTHDepot.DataSource = null;
                    GvuEditFromFCI_OTHDepot.DataBind();
                    gvuNonMPSCSC.DataSource = null;
                    gvuNonMPSCSC.DataBind();
                    pnlGrid.Visible = false;
                    lbl_msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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

    protected void fillGridProc()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,AST.Challan_No,AST.Truck_No,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.DepotId = '" + Session["Depot_DepotID"].ToString() + "' AND AST.District_Id = '" + Session["Depot_DistID"].ToString() + "' ORDER BY AST.ArrivalStock_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GvuEditDispatchFromPC.DataSource = ds;
                    GvuEditDispatchFromPC.DataBind();
                    pnlGrid.Visible = true;
                    lbl_msg.Visible = false;
                    lbl_head.Text = "Dispatch from " + ddlArrival_Source.SelectedItem.Text.ToString() + " details";
                }
                else
                {
                    GvuEditDispatchFromPC.DataSource = null;
                    GvuEditDispatchFromPC.DataBind();
                    gvuEditFrom_RailHead.DataSource = null;
                    gvuEditFrom_RailHead.DataBind();
                    GvuEditFromFCI_OTHDepot.DataSource = null;
                    GvuEditFromFCI_OTHDepot.DataBind();
                    gvuNonMPSCSC.DataSource = null;
                    gvuNonMPSCSC.DataBind();
                    pnlGrid.Visible = false;
                    lbl_msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGridNonMPSCSC()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("MPWLC_Fill_DispatchGrid_for_Edit_NonMPSCSC", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Dist_Id", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Dist_Id"].Value = Session["Depot_DistID"].ToString();
                cmd.Parameters.Add("@Depot_ID", SqlDbType.NVarChar, 20);
                cmd.Parameters["@Depot_ID"].Value = Session["Depot_DepotID"].ToString();
                cmd.Parameters.Add("@Depositor_Name", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Depositor_Name"].Value = ddlDepositor.SelectedItem.Text.Trim();
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvuNonMPSCSC.DataSource = ds;
                    gvuNonMPSCSC.DataBind();
                    pnlGrid.Visible = true;
                    lbl_msg.Visible = false;
                    lbl_head.Text = "Dispatch from " + ddlArrival_Source.SelectedItem.Text.ToString() + " details";
                }
                else
                {
                    GvuEditDispatchFromPC.DataSource = null;
                    GvuEditDispatchFromPC.DataBind();
                    gvuEditFrom_RailHead.DataSource = null;
                    gvuEditFrom_RailHead.DataBind();
                    GvuEditFromFCI_OTHDepot.DataSource = null;
                    GvuEditFromFCI_OTHDepot.DataBind();
                    gvuNonMPSCSC.DataSource = null;
                    gvuNonMPSCSC.DataBind();
                    pnlGrid.Visible = false;
                    lbl_msg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
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

    protected void ddlArrival_Source_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlArrival_Source.SelectedValue == "01")
            {
                fillGridProc();
            }
            else if (ddlArrival_Source.SelectedValue == "07")
            {
                fillGridRailHead();
            }
            else if ((ddlArrival_Source.SelectedValue == "02") || (ddlArrival_Source.SelectedValue == "03") || (ddlArrival_Source.SelectedValue == "04") || (ddlArrival_Source.SelectedValue == "05") || (ddlArrival_Source.SelectedValue == "06"))
            {
                fillGridFCI_OTDepot();
            }

        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkFCI = (LinkButton)sender;
            Session["WLC_StorageReceipt_Id"] = lnkFCI.CommandArgument.ToString();
            Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            Session["Mode"] = "Edit";
            Response.Redirect("WLC_FRM_01_02_03_Receipt_A.aspx");
        }
        catch (Exception ex)
        {
           // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }

    }

    protected void lnkDepositProc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkProc = (LinkButton)sender;
            Session["WLC_StorageReceipt_Id"] = lnkProc.CommandArgument.ToString();
            Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            Session["Mode"] = "Edit";
            Response.Redirect("~/BranchPages/Receiptfrom_Procurement.aspx");
        }
        catch (Exception ex)
        {
           // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void lnkRailHead_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRHead = (LinkButton)sender;
            Session["WLC_StorageReceipt_Id"] = lnkRHead.CommandArgument.ToString();
            Session["WLCDepSource"] = ddlArrival_Source.SelectedValue;
            Session["Mode"] = "Edit";
            Response.Redirect("WLC_FRM_01_02_03_Receipt_A.aspx");

        }
        catch (Exception ex)
        {
           // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void lnkDepositNonMPSCSC_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkNonMPSCSC = (LinkButton)sender;
            string[] CommandArgument = lnkNonMPSCSC.CommandArgument.Split(',');
            Session["WLC_StorageReceipt_Id"] = CommandArgument[0];
            Session["WLCDepSource"] = CommandArgument[1];
            Session["Mode"] = "NON-Edit";
            Response.Redirect("WLC_FRM_01_02_03_Receipt_A.aspx");
        }
        catch (Exception ex)
        {
            // Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void GvuEditFromFCI_OTHDepot_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            GvuEditFromFCI_OTHDepot.PageIndex = e.NewPageIndex;
            fillGridFCI_OTDepot();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void gvuEditFrom_RailHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvuEditFrom_RailHead.PageIndex = e.NewPageIndex;
            fillGridRailHead();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void GvuEditDispatchFromPC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            GvuEditDispatchFromPC.PageIndex = e.NewPageIndex;
            fillGridProc();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                String query = "";
                ddlDepositor.Items.Clear();
                if (ddldepositortype.SelectedValue.ToString().Trim() == "Institution")
                {
                    query = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  depot_id='" + Session["Depot_DepotID"].ToString() + "' and Depositor_Type ='Institution'";
                }
                else
                {
                    query = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE depot_id= '" + Session["Depot_DepotID"].ToString() + "' and Depositor_Type ='" + ddldepositortype.SelectedValue.ToString().Trim() + "'";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepositor.DataSource = ds;
                    ddlDepositor.DataTextField = "Depositor_Name";
                    ddlDepositor.DataValueField = "Depositor_ID";
                    ddlDepositor.DataBind();
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('No Depositor Found'); </script> ");
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("../../Logout.aspx");
        }
    }

    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepositor.SelectedItem.Text.Trim().ToUpper() == "MPSCSC")
        {
            FillArrivalSourceddl();
        }
        else
        {
            ddlArrival_Source.Items.Clear();
            ddlArrival_Source.Items.Insert(0, "--Select--");
        }
    }

    protected void gvuNonMPSCSC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int indx = e.NewPageIndex;
            gvuNonMPSCSC.PageIndex = e.NewPageIndex;
            fillGridNonMPSCSC();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
}
