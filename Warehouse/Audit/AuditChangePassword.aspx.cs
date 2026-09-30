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
        if(!IsPostBack)
        {
            if (Session["UserID"] != null || Session["RegionId"] != null || Session["LoginType"]!=null)
            {

            }
        }
    }
    protected void btn_Change_Pass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_Pass_New.Text.Trim() == txt_Pass_Confirm.Text.Trim())
            {
                string RoleID = "";
                string Logid = "";
                RoleID = Session["LoginType"].ToString();
                if (RoleID == "B")
                {

                    Logid = Session["UserID"].ToString();

                }
                else if (RoleID == "R")
                {

                    Logid = Session["RegionId"].ToString();

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
                Con.Open();
                string str = "select [UserID],[UserName],[InsPwd] from InspectionLogin where UserID ='" + Session["UserID"].ToString() + "' ";
                SqlCommand cmd = new SqlCommand(str, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Storage_Login");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    User_Name = ds.Tables[0].Rows[0]["UserName"].ToString();
                    Old_Pass = ds.Tables[0].Rows[0]["InsPwd"].ToString();
                  
                }
                cmd.Dispose();
                // con.Close();

                if (Old_Pass == OldPass)
                {

                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    // Insert Log For password updation
                    string updatelog = "Insert into [InspectionLogin_Log] ([UserID],[UserName],[InsPwd],[ModifiedDate],[ModifiedBy]) Values ('" + Session["UserID"].ToString() + "','" + User_Name + "','" + Old_Pass + "',getdate(),'" + ClientIP + "')";
                    cmd = new SqlCommand(updatelog, Con);
                    cmd.ExecuteNonQuery();

                    // Update password in storage login table
                    string updateTable = "Update  [InspectionLogin] set InsPwd = '" + Pass_New + "' where UserID= '" + Session["UserID"].ToString() + "'";
                    cmd = new SqlCommand(updateTable, Con);
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
            //Server.Transfer("../CustomError.aspx");
            Response.Redirect("InspectionLogin.aspx");
        }
        finally
        {
            Con.Close();
        }
    }
}
