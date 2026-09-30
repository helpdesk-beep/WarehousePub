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
using System.Text;
using System.Collections.Generic;

public partial class Special_PV_Region_Update_Remark_By_RM : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDist();
            //fillScheduleInsp_Grid();
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Id,District_Name FROM tbl_MetaData_DISTRICT where Region_ID = '" + Session["UserId"].ToString() + "' order by District_Name ";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "All");
        }
        else
        {
            ddldistrict.Items.Insert(0, "All");
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch();
        //fillgrid();
    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "All");
        }
        else
        {
            ddlBranch.Items.Insert(0, "All");
        }
    }
    protected void btn_Remark_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {
            string ErrorMsg = "";
            ErrorMsg += !string.IsNullOrEmpty(txtremark.Text) ? "" : "Enter Remark \\n";
            if (ErrorMsg == "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Insp_Special_PV_RM_Remark_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //tn = con.BeginTransaction();
                //cmd.Transaction = tn;
                cmd.Parameters.AddWithValue("@Branch_ID",ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Rm_Remark", txtremark.Text);
                cmd.Parameters.AddWithValue("@Rm_Update_By", Session["UserId"].ToString());
                //cmd.Parameters.AddWithValue("@Update_Ip_BM", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Update Remark successfully";
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');", true);
                    //fillScheduleInsp_Grid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Updeted!');", true);
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            // tn.Rollback();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
}