using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
      


    }
    public void Changepwd()
    {
        if (Page.IsValid)
        {
            if (txtCurPass.Text == "" || txtCurPass.Text == null)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please Insert Current Password...!!!";
            }
            if (txtNewPass.Text == "" || txtNewPass.Text == null)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please Insert Password...!!!";
            }
            if (txtNewPass.Text == txtConPass.Text)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please Enter Same  Password...!!!";
            }

            if (txtNewPass.Text.Length < 8)
            {
                lblMsg.Text="You need to write at least 8 characters";
            }

            string newpassword = txtNewPass.Text;
            string password = txtCurPass.Text;
            Session["password"] = password.ToString();
            Session["newpassword"] = newpassword.ToString();
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand("spAdminLogin", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userName", Session["username"]);
                //com.Parameters.AddWithValue("@userPassword", Session["password"]);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string pwd_db = dt.Rows[0]["hashedPassword"].ToString();
                    string salt = dt.Rows[0]["salt"].ToString();
                    byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(Session["password"] + salt);
                    string passsalt = Convert.ToBase64String(passwordAndSaltBytes);
                    byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
                    string hashString = Convert.ToBase64String(hashBytes);

                    if (hashString == pwd_db)
                    {
                        lblMsg.Visible = false;
                        Session["userName"] = dt.Rows[0]["userName"].ToString();

                        if (Session["username"].ToString() == Session["userName"].ToString() && Session["password"].ToString() == txtCurPass.Text)
                        {
                            string newsalt = dt.Rows[0]["salt"].ToString();
                            byte[] newpasswordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(newpassword + newsalt);
                            string newsaltpwd = Convert.ToBase64String(newpasswordAndSaltBytes);
                            byte[] newhashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(newpasswordAndSaltBytes);
                            string newhashString = Convert.ToBase64String(newhashBytes);

                            SqlConnection con1 = new SqlConnection();
                            string constr1 = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                            con1 = new SqlConnection(constr1);
                            con1.ConnectionString = constr1;
                            con1.Open();
                            SqlCommand cmd = new SqlCommand("ChangePassward", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@username", Session["userName"]);
                            cmd.Parameters.AddWithValue("@oldpwd", Session["password"]);
                            cmd.Parameters.AddWithValue("@newpwd", newpassword);
                            cmd.Parameters.AddWithValue("@salt", newsalt);
                            cmd.Parameters.AddWithValue("@hashpwd", newhashString);
                            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                            if (TheResult.StartsWith("SUCCESS"))
                            {
                                string strMsg = "आपका पासवर्ड सफलतापूर्वक बदल दिया गया है";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='../Login/Login.aspx';", true);
                                lblMsg.Text = "आपका पासवर्ड सफलतापूर्वक बदल दिया गया है";
                                lblMsg.Visible = true;
                            }
                            else
                            {
                                //string strMsg = "some thing wrong";
                                lblMsg.Text = "some thing wrong";
                                lblMsg.Visible = true;
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            }
                        }
                        else
                        {
                            //string strMsg = "Old Passward is Wrong,Please Enter Currect Old Passward";
                            lblMsg.Text = "Old Passward is Wrong,Please Enter Currect Old Passward";
                            lblMsg.Visible = true;
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        }

                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Invalid username or password.";
                        // lblMsg.Text = hashString + "  Invalid username or password.";
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid username or password.";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error: " + ex.Message.ToString();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int rvalue = 0;
        
        if (Session["UserName"] != null)
        {
            //uid = Session["UserName"].ToString();
            //rvalue = new Admin().AdminChangePassword(uid, txtCurPass.Text, txtNewPass.Text);

            //if (rvalue > 0)
            //{
            //    lblMsg.Text = "Change Successfully";
            //    lblMsg.ForeColor = Color.Green;
            //}

            //else
            //{
            //    lblMsg.Text = "Not Change";
            //    lblMsg.ForeColor = Color.Red;
            //}
            if (txtNewPass.Text.Length < 6)
            {
                lblMsg.Text="Passwords must be at least 6 characters long.";
                return /*false*/;
            }
            else
            {
                Changepwd();
            }


        }

        else
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("../Login/Login.aspx");
        }



    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}