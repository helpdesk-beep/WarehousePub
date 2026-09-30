using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
public partial class TribalGodown_UserReg : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TribalGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
      //  checkdatetime();
    }


    private void checkdatetime()
    {
        try
        {
           
            string strsql = "SELECT GETDATE() AS CurrentDateTime";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DateTime currdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CurrentDateTime"].ToString());
             // DateTime todate=Convert.ToDateTime("2016-01-01 15:53:33.900");
               
                //DateTime todate = new DateTime(2016,01,25,11,00,00);
                //DateTime todate2 = new DateTime(2016,01,30,17,00,00);
                DateTime todate = new DateTime(2017, 01, 25, 11, 00, 00);
                DateTime todate2 = new DateTime(2018, 01, 30, 17, 00, 00);
                if (currdate >= todate && currdate <= todate2)
                {

                    btnreg.Enabled = true;
                }
                else
                {
                    btnreg.Enabled = false;

                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }


    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        DateTime _effective_date = Convert.ToDateTime("10/30/2017 08:35:00 PM");
        DateTime _Closing_date = Convert.ToDateTime("11/15/2017 05:00:00 PM");
        //Actual
        //DateTime _effective_date = Convert.ToDateTime("10/31/2017 11:30:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("11/15/2017 05:00:00 PM");
        ////Old
        //DateTime _effective_date = Convert.ToDateTime("06/27/2017 11:30:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("07/17/2017 05:00:00 PM");

        string S="";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con); 
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();
       
        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            ServerDate = ServerDate.AddMinutes(-0);
        }
        if (ServerDate < _effective_date)
        {
            S = "NS";
        }
        else if (ServerDate > _Closing_date)
        {
            S = "NE";
        }
        else
        {
            S = "Y";
        }
        return S;
    }
    protected void btnreg_Click(object sender, EventArgs e)
    {
        string TStatus=Tcheckdatetimes();
        if (TStatus == "Y")
        {
            if (txtregpwd.Value == txtregconpwd.Value)
            {

                if (txtfname.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया प्रथम नाम दर्ज करे...'); </script> ");
                    txtfname.Focus();
                }
                else if (txtlname.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया सरनेम दर्ज करे...'); </script> ");
                    txtlname.Focus();
                }
                else if (txtmobile.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया मोबाइल नंबर दर्ज करे...'); </script> ");
                    txtmobile.Focus();
                }
                else if (txtREgemail.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Email दर्ज करे...'); </script> ");
                    txtREgemail.Focus();
                }
                else if (txtregpwd.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Password दर्ज करे...'); </script> ");
                    txtregpwd.Focus();
                }
                else if (txtregconpwd.Value == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया  Confirm Password दर्ज करे...'); </script> ");
                    txtregconpwd.Focus();
                }
                else
                {

                    string strsql = "SELECT [UEmail] FROM [Tribal_Godown].[dbo].[tbl_tribal_Reg] where UEmail='" + txtREgemail.Value + "'";
                    SqlDataAdapter da = new SqlDataAdapter(strsql, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('यह email id पहले से register है कृपया दूसरी id use करें...'); </script> ");
                        txtREgemail.Focus();
                    }
                    else
                    {
                        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        qry = "insert into [tbl_tribal_Reg] ([Name],[UMobile],[UEmail],[PWD],[CreatedDate],[CreatedBy],[MName],[LName]) values ('" + txtfname.Value + "','" + txtmobile.Value + "','" + txtREgemail.Value + "','" + txtregpwd.Value + "',getdate(),'" + ClientIP + "','" + txtmname.Value + "','" + txtlname.Value + "')";
                        cmd = new SqlCommand(qry, con);
                        int c = cmd.ExecuteNonQuery();
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        if (c > 0)
                        {
                            Session["login"] = "true";
                            Session["fname"] = txtfname.Value;
                            Session["lname"] = txtlname.Value;
                            Session["mname"] = txtmname.Value;
                            Session["email"] = txtREgemail.Value;
                            Session["mobile"] = txtmobile.Value;
                            Response.Redirect("neeti.aspx");
                        }
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपके पासवर्ड मैच नहीं हो रहे हैं'); </script> ");
                txtregconpwd.Focus();
            }
        }
        else if (TStatus=="NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 31/10/2017 11:30:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for online Application under Tribal Area has been closed..!'); </script> ");
        }
    }
    private void DoDepotLogin()
    {
        try
        {
            string Uname = txtLogemail.Value;
            string pwd = txtlogpwd.Value;
            string strsql = "SELECT  [Tid],[Name],[UMobile],[UEmail],[PWD],[CreatedDate],[CreatedBy],[MName],[LName] FROM [tbl_tribal_Reg] where UEmail='"+Uname.ToString()+"' and PWD='"+pwd.ToString()+"'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                //string spwd = dr["Password"].ToString();
                //string pwdM = dr["MasterPassword"].ToString();
                //string login_ID = dr["login_id"].ToString();

                //string hpwd = CreatePasswordHash(pwd.ToLower()).ToLower();
                //string hpwdM = CreatePasswordHash(pwdM.ToLower()).ToLower();
                //string TBpass = txt_password.Text.ToLower().Trim().ToString();
                //if (TBpass == hpwd || TBpass == hpwdM || txt_password.Text.Trim().ToString() == pwd || txt_password.Text.Trim().ToString() == pwdM)
                //{
                Session["login"] = "true";
                Session["fname"] = dr["Name"].ToString();
                Session["lname"] = dr["LName"].ToString();
                Session["mname"] = dr["MName"].ToString();
                Session["email"] = dr["UEmail"].ToString();
                Session["mobile"] = dr["UMobile"].ToString();
                string em = dr["UEmail"].ToString();
                if (em == "admin@mpwlc.com")
                {
                    Response.Redirect("AdminPanel.aspx");
                }
                else
                {
                    checkifreg();
                }
                // }
                //else
                //{
                // }
            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपका ई-मेल या पासवर्ड गलत हो सकता है')", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }

    private void checkifreg()
    {
        try
        {
            string Uname = txtLogemail.Value;
            string pwd = txtlogpwd.Value;
            string strsql = "select[TAID], [Email] FROM [Tribal_Godown].[dbo].[Tbl_TribalReg] where Email='" + txtLogemail.Value + "'";
            SqlDataAdapter da = new SqlDataAdapter(strsql, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["App_ID"] = ds.Tables[0].Rows[0]["TAID"].ToString(); ;
                Response.Redirect("PrintReg.aspx");
            }
            else
            {

                Response.Redirect("Neeti.aspx");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter the correct Password')", true);

        }
    }
   
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();
        if (TStatus == "Y")
        {
        if (txtLogemail.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Email दर्ज करे...'); </script> ");
            txtLogemail.Focus();
        }
        else if (txtlogpwd.Value == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('कृपया Password दर्ज करे...'); </script> ");
            txtlogpwd.Focus();
        }
        else
        {
           
            DoDepotLogin();
        }
         }
        else if (TStatus=="NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 31/10/2017 11:30:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for online Application under Tribal Area has been closed..!'); </script> ");
        }
        
    }


    protected void SendEmail(object sender, EventArgs e)
    {
        string strsql = "SELECT  [Tid],[Name],[UMobile],[UEmail],[PWD] FROM [tbl_tribal_Reg] where UEmail='" + txtpdwrec.Value + "'";
        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string passwordd = ds.Tables[0].Rows[0]["PWD"].ToString();
            string name = ds.Tables[0].Rows[0]["Name"].ToString();

            using (MailMessage mm = new MailMessage("mpwlchelpdesk@gmail.com", txtpdwrec.Value))
            {
                mm.Subject = "Password की जानकारी ";
                mm.Body = "Hi," + name + "<br/>" + "Your Password is: &nbsp;&nbsp;" + passwordd;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                mm.From = new MailAddress("mpwlchelpdesk@gmail.com");
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("mpwlchelpdesk@gmail.com", "mpwlc2015");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.Send(mm);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपके द्वारा दर्ज email गलत है'); </script> ");


        }


       


    }
    protected void btngetpwd_Click(object sender, EventArgs e)
    {

    }


}