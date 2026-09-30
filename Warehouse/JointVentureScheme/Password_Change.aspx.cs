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

public partial class JointVentureScheme_Password_Change : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranchID != "" && SessBranch != "")
        {
            if (!IsPostBack)
            {
                //GetWareName();
                //GetDist();
                //divHide();
            }
        }

            txt_Old_Pass.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
            txt_Old_Pass.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");


            txt_Pass_New.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
            txt_Pass_New.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");


            txt_Pass_Confirm.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
            txt_Pass_Confirm.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");
    }
    protected void btn_Change_Pass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_Pass_New.Text.Trim() == txt_Pass_Confirm.Text.Trim())
            {

                //bool _valind = false;
                string User_Name = string.Empty;
                string Old_Pass = string.Empty;
                string Pass_New = txt_Pass_New.Text.Trim().ToString();
                string OldPass = txt_Old_Pass.Text.Trim().ToString();
                string M_Pass = string.Empty;
                con.Open();
                string str = "SELECT BranchID,BranchName,BranchPwd,MasterPassword FROM MetaDataBranchWithIssueCenter where BranchID='" + Session["UserId"].ToString() + "' and DistrictId='" + Session["DistID"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "MetaDataBranchWithIssueCenter");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    User_Name = ds.Tables[0].Rows[0]["BranchID"].ToString();
                    Old_Pass = ds.Tables[0].Rows[0]["BranchPwd"].ToString();
                    M_Pass = ds.Tables[0].Rows[0]["MasterPassword"].ToString();
                }
                cmd.Dispose();
                // con.Close();

                if (Old_Pass == OldPass || M_Pass == OldPass)
                {

                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    // Insert Log For password updation
                    string updatelog = "insert into MetaDataBranchWithIssueCenter_Log select * from MetaDataBranchWithIssueCenter where BranchID='" + Session["UserId"].ToString() + "' and DistrictId='" + Session["DistID"].ToString() + "'";
                    cmd = new SqlCommand(updatelog, con);
                    cmd.ExecuteNonQuery();

                    // Update password in storage login table
                    string updateTable = "Update MetaDataBranchWithIssueCenter set BranchPwd = '" + Pass_New + "',UpdatedDate=GETDATE() where BranchID='" + Session["UserId"].ToString() + "' and DistrictId='" + Session["DistID"].ToString() + "'";
                    cmd = new SqlCommand(updateTable, con);
                    int req = cmd.ExecuteNonQuery();
                    if (req > 0)
                    {
                        Uxmsg.Visible = true;
                        Uxmsg.Text = "Your Password has been  changed successfully ";
                        txt_Old_Pass.Text = " ";
                        txt_Pass_New.Text = " ";
                        txt_Pass_Confirm.Text = "";

                    }
                    else
                    {
                        Uxmsg.Visible = true;
                        Uxmsg.Text = "There was some problem !";
                    }
                }
                else
                {
                    Uxmsg.Visible = true;
                    Uxmsg.Text = "Your Old Password is not correct !";
                    txt_Old_Pass.Text = " ";
                    txt_Pass_New.Text = " ";
                    txt_Pass_Confirm.Text = " ";

                }
            }
            else
            {
                Uxmsg.Visible = true;
                Uxmsg.Text = "New Password and Confirm password should be same !";
                txt_Old_Pass.Text = "";
                txt_Pass_New.Text = "";
                txt_Pass_Confirm.Text = "";

            }
        }
        catch (Exception ex)
        {
            Session["errDesc"] = "Sorry ,retry or login again..";
            Server.Transfer("../CustomError.aspx");
        }
        finally
        {
            con.Close();
        }
    }
    protected void btn_Cancle_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Password_Change.aspx");
    }
}
