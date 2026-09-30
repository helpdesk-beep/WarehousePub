using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class Masters_Update_Premices_Details_For_Branch : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetBranchData()
    {
        try
        {
            string qry = "";
            //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Premices_Details_For_Updatatio", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            // cmd.Parameters.AddWithValue("@StorageType", ddlWST.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                // trmobtxt.Visible = true;
                //lbldistrictid.Text = ds.Tables[0].Rows[0]["DistrictId"].ToString();
                //lblbranchid.Text = ds.Tables[0].Rows[0]["BranchID"].ToString();
                showgrid.Visible = true;

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                //trbtnhide.Visible = false;
                //trmobtxt.Visible = false;
                showgrid.Visible = false;

                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }



    protected void ddlWST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        // lblWHID.Text = (row.FindControl("lblWHID") as Label).Text;
        string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
        string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
        Session["hdnWHID"] = (row.FindControl("hdnWHID") as HiddenField).Value;
        Session["hdnBranchId"] = (row.FindControl("hdnBranchId") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
        FillMapGodown(hdnBranchId, hdnWHID);
    }
    public void FillMapGodown(string branchid, string WHID)
    {
        try
        {
            string qry = "";
            //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Premises_Wise_Godwon_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", branchid.ToString());
            cmd.Parameters.AddWithValue("@WHID", WHID.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);

                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
            string ddlEWC = (row.FindControl("ddlEWC") as DropDownList).SelectedValue;
            string lblelectroninc_weghbrige_make = (row.FindControl("lblelectroninc_weghbrige_make") as TextBox).Text;
            //string ddlrailsaided = (row.FindControl("ddlrailsaided") as DropDownList).SelectedValue;
            //string ddlrailsaidedcount = (row.FindControl("ddlrailsaidedcount") as DropDownList).SelectedValue;

            Session["hdnId"] = hdnId.ToString();
            Session["hdnWHID"] = hdnWHID.ToString();
            Session["ddlEWC"] = ddlEWC.ToString();
            Session["lblelectroninc_weghbrige_make"] = lblelectroninc_weghbrige_make.ToString();
            //Session["ddlrailsaided"] = ddlrailsaided.ToString();
            //Session["ddlrailsaidedcount"] = ddlrailsaidedcount.ToString();
            Update(hdnId,hdnWHID,ddlEWC,lblelectroninc_weghbrige_make);
            // RemoveRowJVS(hdnId);

        }
    }
    public void Update(string id,string whid,string ews,string ewm)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Update_Premices_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@WHID", whid);
            cmd.Parameters.AddWithValue("@electroninc_weghbrige_count", ews);
            cmd.Parameters.AddWithValue("@electroninc_weghbrige_make", ewm.ToString());
            cmd.Parameters.AddWithValue("@rail_sided", 2);
            cmd.Parameters.AddWithValue("@rail_siding_count", 0);
            cmd.Parameters.AddWithValue("@Createt_By", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Premices Details Detail Update Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                Depositor_Gridview.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                GetBranchData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void GrdOfficerPreviousInsp_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
       
    }


    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}
