using System;
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

public partial class Inspections_State_Insert_Depositer_Name : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                getdistrict();
                fillDepositertype();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void fillDepositertype()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Depositor_Type", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();

            ddldepositertype.DataSource = cmd.ExecuteReader();
            ddldepositertype.DataTextField = "Depositor_Type";
            ddldepositertype.DataValueField = "DepositorType_ID";
            ddldepositertype.DataBind();
            ddldepositertype.Items.Insert(0, new ListItem("-- Select Depositor_Type --", "0"));
            con.Close();
        }
    }

    protected void ddldepositertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Depositer_Name_For_Insert_in_JVS", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddldepositertype.SelectedValue == "-- Select Depositor_Type --")
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Depositor_Type", ddldepositertype.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
                //cmd.Parameters.AddWithValue("@BranchPwd",txtBranchPwd.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(DropDownList1.SelectedValue.ToString());
    }
    public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }
    protected void Depositor_Gridview_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //Finding the controls from Gridview for the row which is going to update  
        HiddenField hdnID = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnID") as HiddenField;
        HiddenField hdnState_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnState_ID") as HiddenField;
        HiddenField hdnDistrict_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnDistrict_ID") as HiddenField;
        HiddenField hdnDepot_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnDepot_ID") as HiddenField;
        HiddenField hdnDepositor_Type = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnDepositor_Type") as HiddenField;
        HiddenField hdnDepositor_Name = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnDepositor_Name") as HiddenField;
        HiddenField hdnAddress = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnAddress") as HiddenField;
        HiddenField hdnContact_No = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnContact_No") as HiddenField;
        HiddenField hdnIsActive = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnIsActive") as HiddenField;
        HiddenField hdnBranchId = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnBranchId") as HiddenField;
        HiddenField hdnDepositorType_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("hdnDepositorType_ID") as HiddenField;


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Update_DSC_Null_District", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@State_ID", hdnState_ID.Value);
        cmd.Parameters.AddWithValue("@District_ID", hdnDistrict_ID.Value);
        cmd.Parameters.AddWithValue("@Depot_ID", hdnDepot_ID.Value);
        cmd.Parameters.AddWithValue("@Depositor_Type", hdnDepositor_Type.Value);
        cmd.Parameters.AddWithValue("@Depositor_Name", hdnDepositor_Name.Value);
        cmd.Parameters.AddWithValue("@Address", hdnAddress.Value);
        cmd.Parameters.AddWithValue("@Contact_No", hdnContact_No.Value);
        cmd.Parameters.AddWithValue("@IsActive", hdnIsActive.Value);
        cmd.Parameters.AddWithValue("@BranchId", hdnBranchId.Value);
        cmd.Parameters.AddWithValue("@DepositorType_ID", hdnDepositorType_ID.Value);

        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Depositer Name Entry Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Depositor_Gridview.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            fillScheduleInsp_Grid();
        }
    }
    protected void Depositor_Gridview_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        Depositor_Gridview.EditIndex = -1;
        fillScheduleInsp_Grid();
    }
    protected void Depositor_Gridview_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        Depositor_Gridview.EditIndex = e.NewEditIndex;
        fillScheduleInsp_Grid();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}
