using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class Special_PV_Region_View_Reports_New_For_Special_PV : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        lbl_user.Text = Session["UserName"].ToString();
        txt_Region.Text = Session["UserName"].ToString();
        // ddl_Region.SelectedItem.Text = Session["UserId"].ToString();
        if (!IsPostBack)
        {
            //fillInpOff_Grid();
            // GetRegion();
            GetDist();
            //fillFinsncilYear();
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID = '" + Session["UserId"].ToString() + "' order by District_Name ";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "DepotID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Complite_Special_Inspection_Details", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdOfficerPreviousInsp.DataSource = dt;
                GrdOfficerPreviousInsp.DataBind();
                divremark.Visible = true;
                divverify.Visible = true;
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
            }
            else
            {
                GrdOfficerPreviousInsp.DataSource = null;
                GrdOfficerPreviousInsp.DataBind();
                // lblOfficerList.Text = "0";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillInpOff_Grid();
    }

    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Insp_Special_PV_RM_VErify_Status", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            //tn = con.BeginTransaction();
            //cmd.Transaction = tn;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Rm_Remark", txtremark.Text);
            cmd.Parameters.AddWithValue("@Rm_Update_By", Session["UserId"].ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "data has been final submitted successfully";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');", true);
                //fillScheduleInsp_Grid();
                divremark.Visible = false;
                divverify.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
            con.Close();

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}