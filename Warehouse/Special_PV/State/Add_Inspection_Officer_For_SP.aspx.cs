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
using System.Configuration;
using System;

public partial class Special_PV_State_Add_Inspection_Officer_For_SP : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_addnewoff_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        try
        {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Add_insp_Officer_For_Special_PV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Inspection_Officer_Name", txtofficername.Text);
                cmd.Parameters.AddWithValue("@Personal_Mobile_No", txtMob.Text);
                cmd.Parameters.AddWithValue("@Designation", ddl_Desig.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Created_By", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy_IP", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Inspection Officer Add Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    // FillGrid();
                    // TextClear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    //TextClear();
                }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}