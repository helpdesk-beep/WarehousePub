using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Drawing;
public partial class RM_BlackListGodown_EntryForm : System.Web.UI.Page
{
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlConnection jvscon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
                GetBranch();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT]  where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    public void GetBranch()
    {
        string DistrictID = ddlDistrict.SelectedValue.ToString();
        string qry = "";
        //qry = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId in (select District_ID from tbl_metadata_district where Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        qry = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId ='" + DistrictID + "' order by DepotName";
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
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlGodownName.ClearSelection();
        lblGodownRegId.Text = null;
        GetBranch();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gdnBranchId = ddlBranch.SelectedValue.ToString();
        string gdnDistrictId = ddlDistrict.SelectedValue.ToString();
        getGodownName(gdnBranchId, gdnDistrictId);
    }
    public void getGodownName(string @gdnBranchId, string @gdnDistrictId)
    {

        string qry = "";
        qry = "SELECT gdn.Godown_ID as Godown_ID,  gdn.Godown_Name as Godown_Name from tbl_MetaData_GODOWN_2018 gdn where gdn.BranchID ='" + @gdnBranchId + "' and gdn.DistrictId = '" + @gdnDistrictId + "' order by Godown_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodownName.DataSource = ds.Tables[0];
            ddlGodownName.DataTextField = "Godown_Name";
            ddlGodownName.DataValueField = "Godown_ID";
            ddlGodownName.DataBind();
            ddlGodownName.Items.Insert(0, "--Select--");
        }
        //getGodownJVSRegistrationID(ddlGodownName.SelectedValue.ToString());
        /*
        SqlCommand cmd = new SqlCommand("Get_Godown_Name_By_Godown_ID", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodownName.DataSource = ds.Tables[0];
            ddlGodownName.DataTextField = "Godown_Name";
            ddlGodownName.DataValueField = "Godown_ID";
            ddlGodownName.DataBind();
            ddlGodownName.Items.Insert(0, "--Select--");
            ddlGodownName.Enabled = false;
        }
        else
        {
            ddlGodownName.DataSource = "";
            ddlGodownName.DataBind();
            ddlGodownName.Items.Insert(0, "--Select--");
        }*/
    }
    public void getGodownJVSRegistrationID(string @GodownID)
    {
        //string @GodownID = ddlGodownName.SelectedValue.ToString();  
        string qry = "";
        //qry = "SELECT gdn.JVS_RegNo as GodownJVSRegID from tbl_MetaData_GODOWN_2018 gdn  where Godown_ID = '" + @GodownID + "'";
        qry = "SELECT CASE WHEN gdn.JVS_RegNo IS NULL THEN 'NOT AVAILABLE' WHEN gdn.JVS_RegNo = '' THEN 'NOT AVAILABLE' ELSE gdn.JVS_RegNo END as GodownJVSRegID from tbl_MetaData_GODOWN_2018 gdn  where Godown_ID = '" + @GodownID + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        lblGodownRegId.Text = ds.Tables[0].Rows[0]["GodownJVSRegID"].ToString();

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            string ErrorMsg = "";

            ErrorMsg += ddlGodownName.SelectedIndex > 0 ? "" : "Please Select Godown Name... \\n";
            ErrorMsg += ddlDistrict.SelectedIndex > 0 ? "" : "Please Select District Name... \\n";
            ErrorMsg += ddlBranch.SelectedIndex > 0 ? "" : "Please Select Branch Name... \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtGodownRegId.Text) ? "" : "Enter Godown Registration No. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtRemark.Text) ? "" : "Enter Remark. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtBlackListOrderNo.Text) ? "" : "Enter Black List Order No. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtBlackListedDate.Text) ? "" : "Enter Black List Date. \\n";
            ErrorMsg += !string.IsNullOrEmpty(txtBlackListTillDate.Text) ? "" : "Enter Black Till Date. \\n";
            if (ErrorMsg == "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("BlackList_Godown_Details_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@GodownID", ddlGodownName.SelectedValue);
                cmd.Parameters.AddWithValue("@Region_Id", Session["Region_ID"].ToString());
                cmd.Parameters.AddWithValue("@DistrictId", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@DepotId", ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@GodownJVSRegistrationId", lblGodownRegId.Text);
                cmd.Parameters.AddWithValue("@GodownJVSRegistrationIdByRM", txtGodownRegId.Text);
                cmd.Parameters.AddWithValue("@Godown_Name", ddlGodownName.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemark.Text);
                cmd.Parameters.AddWithValue("@BlackList_OrderNo", txtBlackListOrderNo.Text);
                cmd.Parameters.AddWithValue("@BlackListOrderDate", getDate_MDY(txtBlackListedDate.Text));
                cmd.Parameters.AddWithValue("@BlackListedTillDate", getDate_MDY(txtBlackListTillDate.Text));
                cmd.Parameters.AddWithValue("@CreatedBy", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Record submitted successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    Empty();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Already Exists!');", true);
                    Empty();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }

        }
        catch (Exception ex)
        {

        }
        finally { con.Close(); }
    }
    protected void ddlGodownName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGodownName.SelectedValue.ToString() != null || ddlGodownName.SelectedValue.ToString() != "--Select--")
        {

            getGodownJVSRegistrationID(ddlGodownName.SelectedValue.ToString());
        }
        else
        {
            lblGodownRegId.Text = "Not Available";
        }

    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    private void Empty()
    {
        try
        {
            ddlGodownName.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            lblGodownRegId.Text = null;
            txtGodownRegId.Text = null;
            txtRemark.Text = null;
            txtRemark.Text = null;
            txtBlackListOrderNo.Text = null;
            txtBlackListedDate.Text = null;
            txtBlackListTillDate.Text = null;
        }
        catch (Exception ex)
        {
            Response.Write("Some  error has occured! try again 255");
        }
    }
}