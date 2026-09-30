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

public partial class Special_PV_Branch_Remark_Update : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillScheduleInsp_Grid();
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
                SqlCommand cmd = new SqlCommand("Insert_BM_Final_Remark", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                //tn = con.BeginTransaction();
                //cmd.Transaction = tn;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@BM_Remark", txtremark.Text);
                cmd.Parameters.AddWithValue("@UpdateBy_BM", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Update_Ip_BM", ipAddress);
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