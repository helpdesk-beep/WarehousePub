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
using System.Globalization;
using System.Resources;
using System.Text;

public partial class IssueCenterLevel_Storage_DeliveryOrderCSC : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string District = "";
    string Depot = "";
    DataSet ds = new DataSet();
    SqlCommand cmd;
    String Language;
    string servername;
    HttpCookie mCookie = null;
    SqlTransaction sqltran;
    decimal isswt = 0;
    int issbag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender.EndDate = DateTime.Now;   //to dissable future  Date
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                if (Session["lang"] != null)
                {
                    if (Session["lang"].ToString() == "Hindi")
                    {
                        lblDeliveryOrderCSC.Text = Resources.hindi.lblDeliveryOrderCSC;
                        lblDepositorType.Text = Resources.hindi.lblDepositorType;
                        lblDepositorName.Text = Resources.hindi.lblDepositorName;
                        lblDeliveryOrderDate.Text = Resources.hindi.lblDeliveryOrderDate;
                        lblIssuedBags.Text = Resources.hindi.lblIssuedBags;
                        lblIssuedweight.Text = Resources.hindi.lblIssuedweight;
                        HPL_ShowDO.Text = Resources.hindi.HPL_ShowDO;
                    }
                }
                District = Session["Depot_DistID"].ToString();
                Depot = Session["Depot_DepotID"].ToString();
                if (!IsPostBack)
                {
                    Printcurrentdate();
                    FillDepositer_Type();
                    ddlDepositorType_SelectedIndexChanged(sender, e);
                    //fillgrid();
                    FillGodown();
                    GetCommodity();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void ddlDepositorType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                string qrySelect = "";
                ddlDepositor.Items.Clear();
                string depositer = ddlDepositorType.SelectedValue.ToString().Trim();
                string depotid = Session["Depot_DepotID"].ToString();
                if (depositer == "Institution")
                {
                    //qrySelect = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchID='" + Session["BranchId"] + "' and Depositor_Type ='Institution'";
                    qrySelect = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI','DMO Markfed','NAFED','HAFED','NCCF','Shrianna Federation') or Depositor_ID='10535' union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchID='" + Session["BranchId"] + "' and Depositor_Type ='Institution'";
                }
                else
                {
                    qrySelect = " select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE BranchID='" + Session["BranchId"] + "' and Depositor_Type ='" + depositer + "'";
                }
                SqlCommand cmd = new SqlCommand(qrySelect, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepositor.DataSource = ds;
                    ddlDepositor.DataTextField = "Depositor_Name";
                    ddlDepositor.DataValueField = "Depositor_Name";
                    ddlDepositor.DataBind();
                    ddlDepositor.Items.Insert(0, "--Select--");

                }
                fillgrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void FillDepositer_Type()
    {
        try
        {
            string str = "select  Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type ";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "tbl_MetaData_Depositor_Type");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositorType.DataSource = ds.Tables[0];
                ddlDepositorType.DataTextField = "Depositor_Type";
                ddlDepositorType.DataValueField = "Depositor_Type";
                ddlDepositorType.DataBind();
                ddlDepositorType.Items.Insert(0, "--Select--");
                ddlDepositorType.SelectedItem.Text = "Institution";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('NO Depositor Type exists !')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void ckGP_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gdGPHelp.Rows.Count; i++)
            {
                if (((CheckBox)gdGPHelp.Rows[i].FindControl("ckGP")).Checked == true)
                {
                    isswt = isswt + decimal.Parse(gdGPHelp.Rows[i].Cells[6].Text.ToString());
                    issbag = issbag + int.Parse(gdGPHelp.Rows[i].Cells[5].Text.ToString());
                }

                txtIssueBags.Text = issbag.ToString();
                txtIssueWt.Text = isswt.ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
        }
    }

    private void Rebind()
    {
        txtDoDate.Text = "";
        txtIssueBags.Text = "";
        txtIssueWt.Text = "";
        fillgrid();
    }

    protected void fillgrid()
    {
        try
        {
            if (ddlDepositor.SelectedItem.Text != "")
            {
                //string query = "select distinct gp.gatepass_no,[Depositor/Issuer_Name] as depo,com.Commodity_Name,gp.Vehicle_No,(gp.NO_of_Bage) as 'Bags',convert(decimal(18,4),gp.Weight) as 'Weight',convert(nvarchar(10),Issue_Date,103) as  Issue_Date from tbl_Storage_GatePass_Enrty gp inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id where gp.Issue_Source='RO' and gp.Status<>'Cancel' and gp.Issue_Source_ID='0' and [Depositor/Issuer_Name]='" + ddlDepositor.SelectedItem.Text.Trim().ToString() + "' and gp.BranchID ='" + Session["BranchId"] + "' and gp.District_ID = '" + District.ToString() + "' and gp.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and gp.Commodity_ID='"+ddlcommodty.SelectedValue.ToString()+"'";
                string query = "select distinct gp.gatepass_no,[Depositor/Issuer_Name] as depo,com.Commodity_Name,gp.Vehicle_No,(gp.NO_of_Bage) as 'Bags',convert(decimal(18,4),gp.Weight) as 'Weight',convert(nvarchar(10),Issue_Date,103) as  Issue_Date from tbl_Storage_GatePass_Enrty gp inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id where gp.Issue_Source='RO' and gp.Status<>'Cancel' and gp.Issue_Source_ID='0' and [Depositor/Issuer_Name]=N'" + ddlDepositor.SelectedItem.Text.Trim().ToString() + "' and gp.BranchID ='" + Session["BranchId"] + "' and gp.District_ID = '" + District.ToString() + "' and gp.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and gp.Commodity_ID='" + ddlcommodty.SelectedValue.ToString() + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gdGPHelp.DataSource = ds.Tables[0];
                    gdGPHelp.DataBind();
                    gdGPHelp.Visible = true;
                    lblRowCount.Text = "Total records are : " + gdGPHelp.Rows.Count.ToString();
                    trdo_Details.Visible = true;
                    Gatepdetails.Visible = true;
                    lbl_notfound.Visible = false;
                    lbl_notfound.Text = "";
                }
                else
                {
                    lbl_notfound.Visible = true;
                    lbl_notfound.Text = "There is No Gatepass Found";
                    gdGPHelp.DataSource = null;
                    gdGPHelp.DataBind();
                    gdGPHelp.Visible = false;
                    trdo_Details.Visible = false;
                    Gatepdetails.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
            {
                if (txtIssueBags.Text.ToString() == "0" && txtIssueWt.Text.ToString() == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast one record.')", true);
                    return;
                }
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string Deliveryid = "";
                int result = 0;
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                sqltran = con.BeginTransaction();
                string QueryMax = "select isnull(Max(StockDeliveryOrder_Id),0) from tbl_Storage_Final_Stock_Delivery_Order where District_Id = '" + District + "' and BranchID = '" + Session["BranchId"].ToString() + "'";
                cmd = new SqlCommand(QueryMax, con,sqltran); // 
                string str3 = cmd.ExecuteScalar().ToString();
                if (Convert.ToInt64(str3) != 0)
                {
                    Deliveryid = Convert.ToString(Convert.ToInt64(str3) + 1);
                    if (Deliveryid != String.Empty || Deliveryid != "")
                    {
                    Found:
                        string Query6 = "select count(StockDeliveryOrder_Id) from tbl_Storage_Final_Stock_Delivery_Order where StockDeliveryOrder_Id='" + Deliveryid + "'";
                        cmd = new SqlCommand(Query6, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                        string maxcount = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt16(maxcount) > 0)
                        {
                            Deliveryid = Convert.ToString(Convert.ToInt64(Deliveryid) + 1);
                            goto Found;
                        }
                    }
                }
                else
                {
                    string BranchId = Session["BranchId"].ToString();
                    Deliveryid = BranchId + System.DateTime.Now.Year.ToString().Substring(2, 2) + "1000";
                    if (Deliveryid != String.Empty || Deliveryid != "")
                    {
                    Found:
                        string Query6 = "select count(StockDeliveryOrder_Id) from tbl_Storage_Final_Stock_Delivery_Order where StockDeliveryOrder_Id='" + Deliveryid + "'";
                        cmd = new SqlCommand(Query6, con, sqltran); // check GatePass_No present in tbl_Storage_GatePass_Enrty table
                        string maxcount = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt16(maxcount) > 0)
                        {
                            Deliveryid = Convert.ToString(Convert.ToInt64(Deliveryid) + 1);
                            goto Found;
                        }
                    }
                }
                if (Deliveryid != "")
                {
                    string qry = "Insert Into tbl_Storage_Final_Stock_Delivery_Order (StockDeliveryOrder_Id,State_Id,District_Id,DepotId,WHR_Id,Delivery_Order_No,Delivery_Order_Date,Qty_Issued_No_Bags_Sound,Qty_Issued_Weight,CreatedBy,CreatedDate,BranchID) values ('" + Deliveryid.ToString() + "','23','" + District + "','" + Depot + "','" + Deliveryid.ToString() + "','" + Deliveryid.ToString() + "','" + getDate_MDY(txtDoDate.Text.Trim().ToString()) + "','" + txtIssueBags.Text.Trim().ToString() + "','" + txtIssueWt.Text.Trim().ToString() + "','" + ip + "',getdate(),'"+Session["BranchId"].ToString()+"')";
                   
                    cmd = new SqlCommand(qry, con, sqltran);
                    int c = cmd.ExecuteNonQuery();
                    if (c > 0)
                    {
                        if (gdGPHelp.Rows.Count > 0)
                        {
                            foreach (GridViewRow gr2 in gdGPHelp.Rows)
                            {
                                CheckBox ckGP = new CheckBox();
                                ckGP = (CheckBox)gr2.Cells[0].FindControl("ckGP");
                                if (ckGP.Checked == false || ckGP.Enabled == false)
                                {

                                }
                                else
                                {
                                    string Gatepassno = Convert.ToString(gdGPHelp.DataKeys[gr2.RowIndex].Value);
                                    cmd = con.CreateCommand();
                                    cmd.Transaction = sqltran;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "sp_Update_tbl_Storage_GatePass_Enrty";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@GatePass_No", Gatepassno);
                                    cmd.Parameters.AddWithValue("@Issue_Source_ID", Deliveryid);
                                    cmd.Connection = con;
                                    int x6 = cmd.ExecuteNonQuery();
                                    result++;
                                }
                            }  
                        }    
                        if (result > 0)
                        {
                            sqltran.Commit();
                          //  ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('The Record is added successfully')", true);
                            lbl_message.Visible = true;
                            lbl_del_no.Text = Deliveryid;
                            Session["DelOr"] = Deliveryid;
                            Rebind();
                            trDO.Visible = true;
                           // HPL_ShowDO.NavigateUrl = "Http://" + servername + "/ReportServer/Pages/ReportViewer.aspx?%2fFCI_DCP_MP_STORAGE_TRG%2fRpt_Delivery_Order_New&rs:Command=Render&DistrictId=" + District.ToString() + "&Depot=" + Depot.ToString() + "&Language=" + Language;
                           //To Print Delivery Order/..........

                            //string Roid = "PrintDeliveryOrder.aspx?do=" + Deliveryid;
                            //StringBuilder sb = new StringBuilder();
                            //sb.Append("<script>");
                            //sb.Append("window.open(");
                            //sb.Append("'" + Roid + "'");
                            //sb.Append(",'MyWindow', 'height=800,width=780');");
                            //sb.Append("</script>");
                            //this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());

                           // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('PrintDeliveryOrder.aspx?do" + Deliveryid + "');", true);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'PrintDeliveryOrder.aspx?do" + Deliveryid + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                            
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Atleast one record.')", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('There is no record for Save')", true);
                }
            }
        }
        catch (Exception ex)
        {
            sqltran.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
            lblmsg.Text = "Data could not be saved as some error has occurred ";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDoDate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGodown();
    }

    protected void FillGodown()
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            try
            {
                ddlgodown.Items.Clear();
                string query = "";
                //query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from [tbl_Storage_GatePass_Enrty] where BranchID ='" + Session["BranchId"].ToString() + "' and [Depositor/Issuer_Name] ='" + ddlDepositor.SelectedValue + "')  ORDER BY [Godown_Name] ";
                query = "SELECT [Godown_Name], [Godown_ID] FROM [tbl_MetaData_GODOWN] WHERE BranchId = '" + Session["BranchId"].ToString() + "' and Godown_ID in (select distinct Godown_ID from [tbl_Storage_GatePass_Enrty] where BranchID ='" + Session["BranchId"].ToString() + "' and [Depositor/Issuer_Name] =N'" + ddlDepositor.SelectedValue + "')  ORDER BY [Godown_Name] ";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlgodown.DataSource = ds.Tables[0];
                    ddlgodown.DataTextField = "Godown_Name";
                    ddlgodown.DataValueField = "Godown_ID";
                    ddlgodown.DataBind();
                    ddlgodown.Items.Insert(0, " --select--");
                }
                else
                {
                    ddlgodown.DataSource = null;
                    ddlgodown.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error in FillGodown has occurred , try again!')", true);
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    private void GetCommodity()
    {
        try
        {
          string  qry = "select * from dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(qry, con);
           IDataAdapter da = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodty.DataSource = ds.Tables[0];
                ddlcommodty.DataValueField = "Commodity_Id";
                ddlcommodty.DataTextField = "Commodity_Name";
                ddlcommodty.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
    }
    protected void ddlcommodty_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
