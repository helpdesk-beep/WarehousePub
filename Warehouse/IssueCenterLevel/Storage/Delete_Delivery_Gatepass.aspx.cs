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

public partial class IssueCenterLevel_Storage_Delete_Delivery_Gatepass : System.Web.UI.Page
{
    bool checkgrid = false;
    DataSet ds = new DataSet();
    SqlCommand cmd = new SqlCommand();
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
                lblPendingGatePAssList.Text = Resources.hindi.lblPendingGatePAssList;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
                Label1.Text = Resources.hindi.Label2;
                lblGPNo.Text = Resources.hindi.lblGPNo;
                lblBags.Text = Resources.hindi.lblBags;
                lblwt.Text = Resources.hindi.lblwt;
                lblArrTime.Text = Resources.hindi.lblArrTime;
                lblVehicleNO.Text = Resources.hindi.lblVehicleNO;
                lblDriver.Text = Resources.hindi.lblDriver;
                lblDepositor.Text = Resources.hindi.lblDepositor;
                lblAvailPapers.Text = Resources.hindi.lblAvailPapers;
                lblReasonforCancelation.Text = Resources.hindi.lblReasonforCancelation;
                Button1.Text = Resources.hindi.Button1;
            }

            if (Session["Region_ID"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    string PopMsg = "";
                    PopMsg = Request.QueryString["PopMsg"];
                    if (Request.QueryString["PopMsg"] != null)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + PopMsg + "'); </script> ");
                    }

                    fillControls();
                    if (ddldepositortype.Items.Count > 0)
                    {
                        for (int d = 0; d < ddldepositortype.Items.Count; d++)
                        {
                            if (ddldepositortype.Items[d].Text == "Institution")
                            {
                                ddldepositortype.Items[d].Selected = true;
                                ddldepositortype_SelectedIndexChanged(sender, e);
                            }
                        }
                    }

                    //fillGatepassGrid();
                }
            }
            else
            {
                Response.Redirect("../../Logout.aspx");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void fillGatepassGrid()
    {
        if (Session["Region_ID"] != null)
        {
            try
            {
                string query = "select GatePass_No,GP_FIN_YR+'/'+convert(nvarchar,GP_SL_No) as 'GP_No',convert(DateTime,Isnull(issue_Date,'01/01/1900'),103) as Issue_Date from tbl_Storage_GatePass_Enrty inner join tbl_MetaData_DISTRICT as dis on dis.District_Id=tbl_Storage_GatePass_Enrty.District_ID  where issue_Source='RO' and Status !='Cancel' and Issue_source_id not in (select StockDeliveryOrder_Id from tbl_Storage_Final_Stock_Delivery_Order)and dis.Region_ID='" + Session["Region_ID"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl_Empty.Text = "";
                    lbl_Empty.Visible = false;
                    gv_gatepass.DataSource = ds.Tables[0];
                    gv_gatepass.DataBind();
                    lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
                }
                else
                {
                    lbl_Empty.Visible = true;
                    lbl_Empty.Text = "There is No Delivery GatePass Pending";
                    lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
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

    protected void fillControls()
    {
        try
        {
            string query = "select Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldepositortype.DataSource = ds;
                ddldepositortype.DataTextField = "Depositor_Type";
                ddldepositortype.DataValueField = "Depositor_Type";
                ddldepositortype.DataBind();
                ddldepositortype.Items.Insert(0, " --Select--");
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepositor.Items.Clear();

            con.Open();
            SqlCommand _cmd = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            _cmd.Connection = con;

            //Added MPWLC & FCI in Depositor Master and showing here too regardless of Depot Selected
            _cmd = new SqlCommand("sp_getDepositorDepo_Region_wise", con);

            _cmd.CommandType = CommandType.StoredProcedure;

            _cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 20);
            _cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
            _cmd.Parameters.Add("@region_id", SqlDbType.VarChar, 20);
            _cmd.Parameters["@region_id"].Value = Session["Region_ID"].ToString();


            int _index = _cmd.ExecuteNonQuery();
            con.Close();
            _cmd.Dispose();

            da.SelectCommand = _cmd;
            da.Fill(ds1, "temp");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds1;

                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_Name";
                ddlDepositor.DataBind();
            }
            con.Close();
            //_cmd.Dispose();
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "There was some error , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtReasonforCancelation.Text == "")
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please mention the reason for cancellation first to cancel the Gate Pass !'); </script> ");
            }
            else
            {
                if (txtGPNo.Text.ToString() != "")
                {
                    string gatepassno = txtGPNo.Text.ToString();
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "update tbl_Storage_GatePass_Enrty set Status='Cancel', ReasonforCancelation='" + txtReasonforCancelation.Text.ToString() + "' where gatepass_no='" + gatepassno + "'";
                    cmd.Connection = con;
                    int index = cmd.ExecuteNonQuery();
                    if (index == 1)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Selected Gatepass Deleted Successfully'); </script> ");
                        lblmsg.Visible = true;
                        lblmsg.Text = "Selected Gatepass Deleted Successfully";
                        plnCancel.Visible = false;
                        fillGatepassGrid();
                        con.Close();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "Can not cancel, some error occured!";
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Can not Deleted, some error occured!'); </script> ");
                    }
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Can not Deleted, some error occured!'); </script> ");
                    lblmsg.Visible = true;
                    lblmsg.Text = "Can not cancel, some error occured!";
                    plnCancel.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again!'); </script> ");
        }
        finally
        {
            con.Close();
        }
    }

    protected void CVDepositorType_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand _cmd = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            _cmd.Connection = con;
            _cmd.CommandText = "select Depositor_Type from tbl_MetaData_Depositor_Type ";
            _cmd.CommandType = CommandType.Text;
            da.SelectCommand = _cmd;
            da.Fill(ds, "tbl_MetaData_Depositor_Type");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string deptype;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    deptype = datarow["Depositor_Type"].ToString();
                    // Compare e-mail address against user's entry
                    if (deptype == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }

    protected void CVDepositor_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand _cmd = new SqlCommand();
            DataSet ds1 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            _cmd.Connection = con;
            _cmd = new SqlCommand("sp_getDepositorDepo_Region_wise", con);
            _cmd.CommandType = CommandType.StoredProcedure;

            _cmd.Parameters.Add("@Depositor_Type", SqlDbType.VarChar, 50);//20
            _cmd.Parameters["@Depositor_Type"].Value = ddldepositortype.SelectedValue.ToString().Trim();
            _cmd.Parameters.Add("@depot_id", SqlDbType.VarChar, 20);
            _cmd.Parameters["@depot_id"].Value = Session["Depot_DepotID"].ToString();


            con.Close();
            _cmd.Dispose();

            da.SelectCommand = _cmd;
            da.Fill(ds1, "temp");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds1.Tables[0].DefaultView;
                string dep;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    dep = datarow["Depositor_Name"].ToString();
                    // Compare e-mail address against user's entry
                    if (dep == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }

    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }

    protected void gv_gatepass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        txtReasonforCancelation.Text = "";
        if (e.CommandName == "Deletes")
        {
            try
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string gatepassno = gv_gatepass.DataKeys[row.RowIndex].Value.ToString();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                string strq = "select GatePass_No,[Depositor/Issuer_name] as 'Dep',isnull(Vehicle_no,'--')as 'Vehicle_no',isnull(Driver_name,'--') as 'Driver_name',No_of_bage,Weight,Arrival_Dep_time,Remarks from tbl_Storage_GatePass_Enrty where gatepass_no= '" + gatepassno + "'";
                SqlCommand _cmd = new SqlCommand(strq, con);
                _cmd.CommandType = CommandType.Text;
                da.SelectCommand = _cmd;
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 1)
                {
                    txtGPNo.Text = dt.Rows[0]["GatePass_No"].ToString();
                    txtBags.Text = dt.Rows[0]["No_of_bage"].ToString();
                    txtWt.Text = dt.Rows[0]["Weight"].ToString();
                    txtArrTime.Text = dt.Rows[0]["Arrival_Dep_time"].ToString();
                    txtVehicleNO.Text = dt.Rows[0]["Vehicle_no"].ToString();
                    txtDepositor.Text = dt.Rows[0]["Dep"].ToString();
                    plnCancel.Visible = true;
                }
                else
                {
                    plnCancel.Visible = false;
                }
                _cmd.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
    }
    protected void ddlDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGatepassGrid();
    }
}
