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

public partial class WarehouseLevel_Change_Password_PvtW : System.Web.UI.Page
{
    int status = -1;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if ((Session["Depot_DistID"] != null) && (Session["GodownID_New"] != null))
        {
            
            if (!IsPostBack)
            {
                
            }
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
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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
                
                //bool _valind = false;
                string User_Name = string.Empty;
                string Old_Pass = string.Empty;
                string Pass_New = txt_Pass_New.Text.Trim().ToString();
                string OldPass = txt_Old_Pass.Text.Trim().ToString();
                string M_Pass = string.Empty;
                con.Open();
                string str = "select Godown_Name,Godown_Id,Password,MasterPassword from Pvt_Warehouse_Login where Godown_Id ='" + Session["GodownID_New"].ToString() + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "Storage_Login");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    User_Name = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    Old_Pass = ds.Tables[0].Rows[0]["Password"].ToString();
                    M_Pass = ds.Tables[0].Rows[0]["Masterpassword"].ToString();
                }
                cmd.Dispose();
                // con.Close();

                if (Old_Pass == OldPass || M_Pass == OldPass)
                {

                    string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                    // Insert Log For password updation
                    //string updatelog = "Insert into Pvt_Warehouse_Login_PassLog([Godown_Id],[Godown_Name],[Password],[GM_Pwd],ChangeDate,Client_IP) Values ('" + Old_Pass + "','" + Logid + "',convert(varchar(10),getdate(),121),'" + ClientIP + "')";
                    string updatelog = "Insert into Pvt_Warehouse_Login_PassLog([Godown_Id],[Godown_Name],[Password],[GM_Pwd],ChangeDate,Client_IP) select [Godown_Id],[Godown_Name],[Password],[GM_Pwd],GETDATE(),'" + ClientIP + "' from [Pvt_Warehouse_Login] where [Pvt_Warehouse_Login].Godown_Id='" + Session["GodownID_New"].ToString() + "'";
                    cmd = new SqlCommand(updatelog, con);
                    cmd.ExecuteNonQuery();

                    // Update password in storage login table
                    string updateTable = "Update [Pvt_Warehouse_Login] set Password = '" + Pass_New + "' where Godown_Id='" + Session["GodownID_New"].ToString() + "'";
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
        Response.Redirect("~/Pvt_Warehouse_Welcome.aspx");
    }

    protected void btnchangebm_Click(object sender, EventArgs e)
    {
        try
        {
            string User_Name = string.Empty;
            string Old_Pass = string.Empty;
            string Pass_New = txtnewpwdbm.Text.Trim();
            string OldPass = txtoldpwdbm.Text.Trim();
            //string M_Pass = txtnewpwdbmc.Text.Trim();
            con.Open();
            //string str = "SELECT [BranchName],[IssueCenterName],[BranchID],[BranchTypeID],[BranchPwd] FROM [Intergrated_MP_STORAGE].[dbo].[MetaDataBranchWithIssueCenter] where BranchID='" + Session["BranchId"].ToString() + "'";
            string str = "select Godown_Name,Godown_Id,Password,MasterPassword,GM_Pwd from Pvt_Warehouse_Login where Godown_Id ='" + Session["GodownID_New"].ToString() + "' ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                User_Name = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                Old_Pass = ds.Tables[0].Rows[0]["GM_Pwd"].ToString();
                // M_Pass = ds.Tables[0].Rows[0]["Masterpassword"].ToString();
            }
            cmd.Dispose();
            // con.Close();

            if (Old_Pass == OldPass)
            {

                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];

                // Insert Log For password updation
                 
                string updatelog = "Insert into Pvt_Warehouse_Login_PassLog([Godown_Id],[Godown_Name],[Password],[GM_Pwd],ChangeDate,Client_IP) select [Godown_Id],[Godown_Name],[Password],[GM_Pwd],GETDATE(),'" + ClientIP + "' from [Pvt_Warehouse_Login] where [Pvt_Warehouse_Login].Godown_Id='" + Session["GodownID_New"].ToString() + "'";
                cmd = new SqlCommand(updatelog, con);
                int cnt=cmd.ExecuteNonQuery();
                if (cnt > 0)
                {
                    // Update password in storage login table
                    string updateTable = "Update [Pvt_Warehouse_Login] set GM_Pwd = '" + txtnewpwdbm.Text.Trim() + "' where Godown_Id='" + Session["GodownID_New"].ToString() + "'";
                    cmd = new SqlCommand(updateTable, con);
                    int req = cmd.ExecuteNonQuery();
                    if (req > 0)
                    {
                        Uxmsg.Visible = true;
                        lblGMMsg.Text = "Your Password has been  changed successfully ";
                        txt_Old_Pass.Text = " ";
                        txt_Pass_New.Text = " ";
                        txt_Pass_Confirm.Text = "";

                    }
                    else
                    {
                        Uxmsg.Visible = true;
                        lblGMMsg.Text = "There was some problem !";
                    }
                }
            }
            else
            {
                lblGMMsg.Visible = true;
                lblGMMsg.Text = "Your Old Password is not correct !";
                txt_Old_Pass.Text = " ";
                txt_Pass_New.Text = " ";
                txt_Pass_Confirm.Text = " ";

            }
        }
        catch (Exception ex)
        {

            lblGMMsg.Text = ex.Message;
        }




    }

    protected void btnclosebm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pvt_Warehouse_Welcome.aspx");
    }
}
