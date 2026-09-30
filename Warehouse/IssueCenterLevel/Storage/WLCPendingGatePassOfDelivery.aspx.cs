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

public partial class IssueCenterLevel_Storage_WLCPendingGatePassOfDelivery : System.Web.UI.Page
{
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {  
        try
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                lblPendingGatePAssList.Text = Resources.hindi.lblPendingGatePAssList;
                lblDepositorType.Text = Resources.hindi.lblDepositorType;
                lblDepositorName.Text = Resources.hindi.lblDepositorName;
            }
            if ((Session["Depot_DistID"].ToString() != "") && (Session["Depot_DepotID"].ToString() != ""))
            {
                if (!IsPostBack)
                {
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
                    fillgrid();
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }     
        }
        catch (Exception ex)
        {
           //
        }

    }

    protected void fillgrid()
    {
        try
        {
            string District = Session["Depot_DistID"].ToString();
            string Depot = Session["Depot_DepotID"].ToString();
            string query = "select distinct gp.gatepass_no,[Depositor/Issuer_Name] as depo,com.Commodity_Name,gp.Vehicle_No,(gp.NO_of_Bage) as 'Bags',convert(decimal(18,2),gp.Weight) as 'Weight',convert(nvarchar(10),Issue_Date,103) as  Issue_Date from tbl_Storage_GatePass_Enrty gp inner join tbl_Delivery_Stacking_Details_GatePass as sgp on gp.GatePass_no=sgp.GatePass_no join tbl_MetaData_STORAGE_COMMODITY as com on gp.Commodity_ID = com.Commodity_Id where gp.Issue_Source='RO' and gp.Status<>'Cancel' and gp.Issue_Source_ID='0' and [Depositor/Issuer_Name]='" + ddlDepositor.SelectedItem.Text.Trim().ToString() + "' and gp.BranchID ='" + Session["BranchId"] + "' and gp.District_ID = '" + District.ToString() + "'";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gdGPHelp.DataSource = ds.Tables[0];
                gdGPHelp.DataBind();
                lblRowCount.Text = "Total records are : " + gdGPHelp.Rows.Count.ToString();

            }
            else
            {
                lbl_notfound.Visible = true;
                lbl_notfound.Text = "There is No Gatepass Found";
                gdGPHelp.DataSource = null;
                gdGPHelp.DataBind();
                gdGPHelp.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some error has occured, try again')", true);
        }
    }

    protected void fillControls()
    {
        try
        {    
            qry= "select Depositor_Type from tbl_MetaData_Depositor_Type order by Depositor_Type";
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
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
           //
        }
    }

    protected void ddldepositortype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDepositor.Items.Clear();    
            con.Open();
            cmd = new SqlCommand();
            ds = new DataSet();
            da = new SqlDataAdapter();
            cmd.Connection = con;
            cmd = new SqlCommand("sp_getDepositor_Depo_wise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Depositor_Type", ddldepositortype.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@depot_id", Session["BranchId"].ToString());
            int index = cmd.ExecuteNonQuery();
            da.SelectCommand = cmd;
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepositor.DataSource = ds;
                ddlDepositor.DataTextField = "Depositor_Name";
                ddlDepositor.DataValueField = "Depositor_Name";
                ddlDepositor.DataBind();
            }
            con.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
           ////
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
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}
