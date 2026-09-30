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

public partial class Inspections_State_Godown_Password_Rest_Inspection : System.Web.UI.Page
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
               
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Password_rest", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", DropDownList1.SelectedValue);
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

  
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void Depositor_Gridview_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        //Finding the controls from Gridview for the row which is going to update  
        Label lblGodown_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_ID") as Label;
        Label lblStateId = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblStateId") as Label;
        Label lblDistrictId = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblDistrictId") as Label;
        Label lblDepotId = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblDepotId") as Label;
        Label lblGodown_Name = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Name") as Label;
        Label lblGodown_Capacity = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Capacity") as Label;
        Label lblRemarks = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblRemarks") as Label;
        Label lblHired_Type = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblHired_Type") as Label;
        Label lblStorage_Type = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblStorage_Type") as Label;
        Label lblGodown_Scientific_Capacity = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Scientific_Capacity") as Label;
        Label lblGodown_APN = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_APN") as Label;
        Label lblGodown_Email = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Email") as Label;
        Label lblGodown_Mobile = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Mobile") as Label;
        Label lblGodown_Address = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Address") as Label;
        Label lblBranchID = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblBranchID") as Label;
        Label lblLicNum = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblLicNum") as Label;
        Label lblPAN = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblPAN") as Label;
        Label lblBank_ID = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblBank_ID") as Label;
        Label lblAccNo = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblAccNo") as Label;
        Label lblIFSC_Code = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblIFSC_Code") as Label;
        Label lblBank_Add = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblBank_Add") as Label;
        Label lblLatitude = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblLatitude") as Label;
        Label lblLongitude = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblLongitude") as Label;
        Label lblGodownNum = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodownNum") as Label;
        Label lblKhasranum = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblKhasranum") as Label;
        Label lblRakwanum = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblRakwanum") as Label;
        Label lblTehshilID = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblTehshilID") as Label;
        Label lblVillageName = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblVillageName") as Label;
        Label lblOrg_Name = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblOrg_Name") as Label;
        Label lblGInchargeName = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGInchargeName") as Label;
        Label lblGInchargeAddress = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGInchargeAddress") as Label;
        Label lblGInchargeMobile = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGInchargeMobile") as Label;
        Label lblGInchargeEmail = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGInchargeEmail") as Label;
        Label lblWeightmentType = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblWeightmentType") as Label;
        Label lblGodown_Reg_No = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblGodown_Reg_No") as Label;
        Label lblIsActive = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblIsActive") as Label;
        Label lblLR_TehsilCode = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblLR_TehsilCode") as Label;
        Label lblLR_VillageCode = Depositor_Gridview.Rows[e.RowIndex].FindControl("lblLR_VillageCode") as Label;
        

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Godown_Password_Insert", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@Godown_ID", lblGodown_ID.Text);
        cmd.Parameters.AddWithValue("@StateId", lblStateId.Text);
        cmd.Parameters.AddWithValue("@DistrictId", lblDistrictId.Text);
        cmd.Parameters.AddWithValue("@DepotId", lblDepotId.Text);
        cmd.Parameters.AddWithValue("@Godown_Name", lblGodown_Name.Text);
        cmd.Parameters.AddWithValue("@Godown_Capacity", lblGodown_Capacity.Text);
        cmd.Parameters.AddWithValue("@Remarks", lblRemarks.Text);
        cmd.Parameters.AddWithValue("@Hired_Type", lblHired_Type.Text);
        cmd.Parameters.AddWithValue("@Storage_Type", lblStorage_Type.Text);
        cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", lblGodown_Scientific_Capacity.Text);
        cmd.Parameters.AddWithValue("@Godown_APN", lblGodown_APN.Text);
        cmd.Parameters.AddWithValue("@Godown_Email", lblGodown_Email.Text);
        cmd.Parameters.AddWithValue("@Godown_Mobile", lblGodown_Mobile.Text);
        cmd.Parameters.AddWithValue("@Godown_Address", lblGodown_Address.Text);
        cmd.Parameters.AddWithValue("@BranchID", lblBranchID.Text);
        cmd.Parameters.AddWithValue("@LicNum", lblLicNum.Text);
        cmd.Parameters.AddWithValue("@PAN", lblPAN.Text);
        cmd.Parameters.AddWithValue("@Bank_ID", lblBank_ID.Text);
        cmd.Parameters.AddWithValue("@AccNo", lblAccNo.Text);
        cmd.Parameters.AddWithValue("@IFSC_Code", lblIFSC_Code.Text);
        cmd.Parameters.AddWithValue("@Bank_Add", lblBank_Add.Text);
        cmd.Parameters.AddWithValue("@Latitude", lblLatitude.Text);
        cmd.Parameters.AddWithValue("@Longitude", lblLongitude.Text);
        cmd.Parameters.AddWithValue("@GodownNum", lblGodownNum.Text);
        cmd.Parameters.AddWithValue("@Khasranum", lblKhasranum.Text);
        cmd.Parameters.AddWithValue("@Rakwanum", lblRakwanum.Text);
        cmd.Parameters.AddWithValue("@TehshilID", lblTehshilID.Text);
        cmd.Parameters.AddWithValue("@VillageName", lblVillageName.Text);
        cmd.Parameters.AddWithValue("@Org_Name", lblOrg_Name.Text);
        cmd.Parameters.AddWithValue("@GInchargeName", lblGInchargeName.Text);
        cmd.Parameters.AddWithValue("@GInchargeAddress", lblGodown_Address.Text);
        cmd.Parameters.AddWithValue("@GInchargeMobile", lblGInchargeMobile.Text);
        cmd.Parameters.AddWithValue("@GInchargeEmail", lblGInchargeEmail.Text);
        cmd.Parameters.AddWithValue("@WeightmentType", lblWeightmentType.Text);
        cmd.Parameters.AddWithValue("@Godown_Reg_No", lblGodown_Reg_No.Text);
        cmd.Parameters.AddWithValue("@IsActive", lblIsActive.Text);
        cmd.Parameters.AddWithValue("@LR_TehsilCode", lblLR_TehsilCode.Text);
        cmd.Parameters.AddWithValue("@LR_VillageCode", lblLR_VillageCode.Text);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Annaxure B Entry Detail Update Successfully |||";
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
}
