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
using System.Text;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;

public partial class IssueCenterLevel_Change_Password : System.Web.UI.Page
{
    int status = -1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"].ToString() == "Hindi")
        {
            lblChangePassword.Text = Resources.hindi.lblChangePassword;
            lblOldPassword.Text = Resources.hindi.lblOldPassword;
            lblNewPassword.Text = Resources.hindi.lblNewPassword;
            lblConfirmPassword.Text = Resources.hindi.lblConfirmPassword;
            btn_Change_Pass.Text = Resources.hindi.Uxchange;
            btn_Cancle.Text = Resources.hindi.btn_Cancle;
            //Uxmsg.Text = Resources.hindi.Uxmsg;
        }

        try
        {
            try
            {
                string RoleID = "";
                string Logid = "";
                RoleID = Session["RoleId"].ToString();
                if (RoleID == "1")
                {

                    Logid = Session["Depot_Logid"].ToString();

                }
                else if (RoleID == "2")
                {

                    Logid = Session["Region_Logid"].ToString();

                }
                else if (RoleID == "3")
                {

                    Logid = Session["State_Logid"].ToString();

                }
                else
                {

                    //

                }

                // HFUID.Value = Logid.ToString();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                // Gv_ChangePassword_Log.DataBind();
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "There was some error , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
        catch (Exception ex)
        {
            Session["errDesc"] = "Sorry ,retry or login again";
            Server.Transfer("../CustomError.aspx");
        }
        txt_Old_Pass.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
        txt_Old_Pass.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");


        txt_Pass_New.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
        txt_Pass_New.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");


        txt_Pass_Confirm.Attributes.Add("onkeypress", "return checksqlkey_pswnew(event,this)");
        txt_Pass_Confirm.Attributes.Add("onkeydown", "return checksqlkey_special(event,this);");

        // btn_Change_Pass.Attributes.Add("onclick", "md5auth();");
    }
    protected void btn_Change_Pass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_Pass_New.Text.Trim() == txt_Pass_Confirm.Text.Trim())
            {
                string RoleID = "";
                string Logid = "";
                RoleID = Session["RoleId"].ToString();
                if (RoleID == "1")
                {

                    Logid = Session["Depot_Logid"].ToString();

                }
                else if (RoleID == "2")
                {

                    Logid = Session["Region_Logid"].ToString();

                }
                else if (RoleID == "3")
                {

                    Logid = Session["State_Logid"].ToString();

                }
                else
                {

                    //

                }

                //bool _valind = false;
                string User_Name = string.Empty;
                string Old_Pass = string.Empty;
                string Pass_New = txt_Pass_New.Text.Trim().ToString();
                string OldPass = txt_Old_Pass.Text.Trim().ToString();
                string M_Pass = string.Empty;
                con.Open();
                string str = "select user_name ,password,Masterpassword from Storage_Login where login_id ='" + Logid.ToString() + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Storage_Login");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    User_Name = ds.Tables[0].Rows[0]["user_name"].ToString();
                    Old_Pass = ds.Tables[0].Rows[0]["password"].ToString();
                    M_Pass = ds.Tables[0].Rows[0]["Masterpassword"].ToString();
                }
                cmd.Dispose();
                // con.Close();

                if (Old_Pass == OldPass || M_Pass == OldPass)
                {

                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    // Insert Log For password updation
                    string updatelog = "Insert into Change_Password_Log (Password,Login_ID,ChangeDateTime,Client_IP) Values ('" + Old_Pass + "','" + Logid + "',convert(varchar(10),getdate(),121),'" + ClientIP + "')";
                    cmd = new SqlCommand(updatelog, con);
                    cmd.ExecuteNonQuery();

                    // Update password in storage login table
                    string updateTable = "Update  Storage_Login set Password = '" + Pass_New + "' where login_id= '" + Logid + "'";
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
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void btnchangebm_Click(object sender, EventArgs e)
    {
        try
        {
            string User_Name = string.Empty;
            string Old_Pass = string.Empty;
            string Pass_New = txtnewpwdbm.Text.Trim();
            string OldPass = txtoldpwdbm.Text.Trim();
            string M_Pass = txtnewpwdbmc.Text.Trim();
            con.Open();
            string str = "SELECT [BranchName],[IssueCenterName],[BranchID],[BranchTypeID],[BranchPwd] FROM [Intergrated_MP_STORAGE].[dbo].[MetaDataBranchWithIssueCenter] where BranchID='" + Session["BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                User_Name = ds.Tables[0].Rows[0]["BranchID"].ToString();
                Old_Pass = ds.Tables[0].Rows[0]["BranchPwd"].ToString();
               // M_Pass = ds.Tables[0].Rows[0]["Masterpassword"].ToString();
            }
            cmd.Dispose();
            // con.Close();

            if (Old_Pass == OldPass)
            {

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                // Insert Log For password updation
                //string updatelog = "Insert into Change_Password_Log (Password,Login_ID,ChangeDateTime,Client_IP) Values ('" + Old_Pass + "','" + Logid + "',convert(varchar(10),getdate(),121),'" + ClientIP + "')";
                //cmd = new SqlCommand(updatelog, con);
                //cmd.ExecuteNonQuery();

                // Update password in storage login table
                string updateTable = "Update  MetaDataBranchWithIssueCenter set BranchPwd = '" + txtnewpwdbm.Text.Trim() + "' where BranchID= '" + User_Name + "'";
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
        catch (Exception ex)
        {

            Uxmsg.Text = ex.Message;
        }




    }
    
    protected void btnclosebm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}
