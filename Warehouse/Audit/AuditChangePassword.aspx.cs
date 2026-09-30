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
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;

public partial class Audit_AuditChangePassword : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable Dt2 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HasAuthorizedSession())
        {
            Response.Redirect("InspectionLogin.aspx");
        }
    }
    protected void btn_Change_Pass_Click(object sender, EventArgs e)
    {
        if (!HasAuthorizedSession())
        {
            Response.Redirect("InspectionLogin.aspx");
            return;
        }

        try
        {
            if (txt_Pass_New.Text.Trim() == txt_Pass_Confirm.Text.Trim())
            {
                string roleId = Convert.ToString(Session["LoginType"]);
                string logId;
                if (roleId == "B")
                {
                    logId = Convert.ToString(Session["UserID"]);
                }
                else if (roleId == "R")
                {
                    logId = Convert.ToString(Session["RegionId"]);
                }
                else
                {
                    Response.Redirect("InspectionLogin.aspx");
                    return;
                }

                string userName;
                string oldPassword;
                string newPassword = txt_Pass_New.Text.Trim();
                Con.Open();
                using (SqlCommand command = new SqlCommand("select [UserID],[UserName],[InsPwd] from InspectionLogin where UserID=@UserID", Con))
                {
                    command.Parameters.AddWithValue("@UserID", logId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Uxmsg.Visible = true;
                            Uxmsg.Text = "User account was not found.";
                            return;
                        }
                        userName = Convert.ToString(reader["UserName"]);
                        oldPassword = Convert.ToString(reader["InsPwd"]);
                    }
                }

                if (oldPassword == txt_Old_Pass.Text.Trim())
                {
                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                    using (SqlCommand logCommand = new SqlCommand("Insert into [InspectionLogin_Log] ([UserID],[UserName],[InsPwd],[ModifiedDate],[ModifiedBy]) Values (@UserID,@UserName,'',GETDATE(),@ModifiedBy)", Con))
                    {
                        logCommand.Parameters.AddWithValue("@UserID", logId);
                        logCommand.Parameters.AddWithValue("@UserName", userName);
                        logCommand.Parameters.AddWithValue("@ModifiedBy", ClientIP ?? String.Empty);
                        logCommand.ExecuteNonQuery();
                    }

                    int req;
                    using (SqlCommand updateCommand = new SqlCommand("Update [InspectionLogin] set InsPwd=@NewPassword where UserID=@UserID", Con))
                    {
                        updateCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                        updateCommand.Parameters.AddWithValue("@UserID", logId);
                        req = updateCommand.ExecuteNonQuery();
                    }
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
            //Server.Transfer("../CustomError.aspx");
            Response.Redirect("InspectionLogin.aspx");
        }
        finally
        {
            Con.Close();
        }
    }

    private bool HasAuthorizedSession()
    {
        string loginType = Convert.ToString(Session["LoginType"]);
        return (loginType == "B" && !String.IsNullOrWhiteSpace(Convert.ToString(Session["UserID"]))) ||
               (loginType == "R" && !String.IsNullOrWhiteSpace(Convert.ToString(Session["RegionId"])));
    }
}
