using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//public partial class Inspections_RO_Ro_Change_Password : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}
public partial class Inspections_ROTQ_Ro_Change_Password : System.Web.UI.Page
{
    public SqlConnection conStr2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string RegionID = "";
    SqlTransaction sqltran;
    int a_id = 0;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        RegionID = Session["UserId"].ToString();
        if (!IsPostBack)
        {

            if (!String.IsNullOrEmpty(Session["UserName"].ToString()))
            {

            }
            if (Session["hdnID"] != null)
            {
                if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                {


                }
            }
        }
    }
    public void clear()

    {
        txtConPass.Text = "";
        txtNewPass.Text = "";
        txtOldPass.Text = "";

    }


    protected void btnsubmit_Click(object sender, EventArgs e)

    {
        try
        {

            {

                string qry = "select * from Inspection_Login where UserId='" + RegionID + "' and Password='" + txtOldPass.Text.Trim() + "'";
                SqlCommand cmd1 = new SqlCommand(qry, conStr);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    if (txtNewPass.Text.Trim() == txtConPass.Text.Trim())
                    {

                        SqlCommand cmd = new SqlCommand("Sp_Update_Branch_Password_Ins", conStr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        conStr.Open();
                        cmd.Parameters.AddWithValue("@BranchID", RegionID);
                        cmd.Parameters.AddWithValue("@NewPassword", txtNewPass.Text.Trim());
                        cmd.Parameters.AddWithValue("@Type",4);
                        int i = cmd.ExecuteNonQuery();
                        clear();
                        if (i > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Password Change Successfully!');", true);

                        }
                        conStr.Close();
                    }
                    else
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Password and Conform Password is Not Match!');", true);

                    }
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Old Password Not Match!');", true);

                }

            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
}