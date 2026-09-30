using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_DownloadAPK : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblMessage.Text = "";
            }
            catch (Exception ex) { }
        }

    }
    protected void btnOtp_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Table"] = "";
            Session["ip"] = "";
            string mobileno = txt_phoneNumber.Text.ToString().Trim();
            string Query = "", apptype = "", otp = "", ip = "";
            otp = new Random().Next(100000, 999999).ToString();

            if (radioInsp.Checked == true)
            { apptype = "Inspection"; }
            else if (radioOther.Checked == true)
            { apptype = "Godown"; }
            else { lblMessage.Text = "Please select app type !"; return; }
            ip = GetClientIP();

            if (mobileno != "")
            {
                if (mobileno.Length == 10 && "6789".Contains(mobileno[0].ToString()))
                {
                    if (apptype == "Inspection")
                    {
                        Query = "SELECT District_ID,Branch_ID as BranchID,'' as GodownID ,Officer_Name as Name,Per_MobileNo as MobileNo FROM jointventurescheme2018.dbo.tbl_metadata_Inspection_officer WHERE Per_MobileNo = @mobile;";
                    }
                    else
                    {
                        Query = " select dist.District_Id,mp.BranchID,mp.GodownID,mp.Name,mp.MobileNo from Intergrated_MP_STORAGE.dbo.MappingForMostureFumigation mp\r\n inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT depo on mp.BranchID=depo.BranchId\r\n inner join Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT dist on depo.DistrictId=dist.District_Id where mp.MobileNo=@mobile";
                    }
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand checkCmd = new SqlCommand(Query, conn);
                        checkCmd.Parameters.AddWithValue("@mobile", mobileno);
                        SqlDataAdapter adp = new SqlDataAdapter(checkCmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        // int exists = (int)checkCmd.ExecuteScalar();

                        if (dt.Rows.Count > 0)
                        {
                            Session["Table"] = dt;
                            Session["ip"] = ip;

                            string smsResponse = SendSmsOtp(mobileno, otp, apptype);
                            if (smsResponse.Contains("Success"))
                            {
                                SqlCommand insertCmd = new SqlCommand(
                       @"INSERT INTO JointVentureScheme2018.dbo.OTP_Log_for_DownloadAPK(Mobile, OTP, IPAddress, TimeStamp,App_Type)
                                     VALUES(@mobile, @otp, @ip, GETDATE(),@App_Type);",
                       conn
                   );
                                insertCmd.Parameters.AddWithValue("@mobile", mobileno);
                                insertCmd.Parameters.AddWithValue("@otp", otp);
                                insertCmd.Parameters.AddWithValue("@ip", ip);
                                insertCmd.Parameters.AddWithValue("@App_Type", apptype);
                                insertCmd.ExecuteNonQuery();

                                lblMessage.Text = "otp sent";

                            }
                            else
                            {
                                lblMessage.Text = "failed to send sms" + smsResponse;
                                return;
                            }

                            otpSection.Visible = true;
                            phoneSection.Visible = false;
                            lblMessage.Text = "Otp Sent";
                            lblMessage.CssClass = "text-success";


                        }
                        else
                        {
                            lblMessage.Text = "Please enter a register mobile number !";
                            txt_phoneNumber.Focus();
                            lblMessage.CssClass = "text-danger";
                            return;
                        }
                    }

                }
                else
                {
                    lblMessage.Text = "Please enter a valid 10-digit mobile number starting with 6-9.";
                    txt_phoneNumber.Focus();
                    lblMessage.CssClass = "text-danger";
                    return;
                }

            }
            else
            {
                lblMessage.CssClass = "text-danger";
                lblMessage.Text = "Please Enter mobile number!";
                txt_phoneNumber.Focus();
                return;
            }
            otpSection.Visible = true;
            phoneSection.Visible = false;
            btnOtp.Visible = false;
            btnVerify.Visible = true;
            lblMessage.Text = "Otp Sent";
            lblMessage.CssClass = "text-success";
            radioInsp.Enabled = false;
            radioOther.Enabled = false;
        }
        catch (Exception ex) { }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        try
        {
            string mobileno = txt_phoneNumber.Text.ToString().Trim();
            string otp = txt_otp.Text.ToString().Trim();
            if (mobileno != "" && otp != "")
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(
                            "SELECT COUNT(*) FROM JointVentureScheme2018.dbo.OTP_Log_for_DownloadAPK WHERE Mobile = @mobile AND OTP = @otp AND DATEDIFF(MINUTE, TimeStamp, GETDATE()) < 5",
                            conn
                        );
                        cmd.Parameters.AddWithValue("@mobile", mobileno);
                        cmd.Parameters.AddWithValue("@otp", otp);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            pnlLogin.Visible = false;
                            if (radioInsp.Checked == true)
                            {
                                pnlDownload.Visible = true;
                                panelgodownapk.Visible = false;
                            }
                            else
                            {
                                pnlDownload.Visible = false;
                                panelgodownapk.Visible = true;
                            }


                            lblMessage.CssClass = "text-success";
                            lblMessage.Text = "Login successful!";

                        }
                        else
                        {
                            lblMessage.Text = "Wrong otp";
                            lblMessage.CssClass = "text-danger";
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.CssClass = "text-danger";
                    return;
                }

            }
            else
            {
                lblMessage.Text = "Please enter otp !";
                lblMessage.CssClass = "text-danger";
                txt_otp.Focus();
                return;
            }


        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btnInspDownload_Click(object sender, EventArgs e)
    {
        try
        {
            //string path = @"E:\MPWLCGodownInspection.apk";
            string path = Server.MapPath("~/BranchPages/MPWLCGodownInspection.apk");
            SendApk(path, "MPWLCGodownInspection.apk");

            if (Session["Table"] != null)
            {
                DataTable dt = (DataTable)Session["Table"];
                string ip = Session["ip"].ToString();

                InsertDownloadLog(dt, ip, "Inspection");
            }

            btnInspDownload.Text = "APK Downloaded";
            btnInspDownload.CssClass = "btn btn-success";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.CssClass = "text-danger";
        }
    }


    protected void btn_godowndownload_Click(object sender, EventArgs e)
    {
        try
        {
            // string path = @"E:\WHIPM.apk";
            string path = Server.MapPath("~/BranchPages/WHIPM.apk");
            SendApk(path, "WHIPM.apk");

            if (Session["Table"] != null)
            {
                DataTable dt = (DataTable)Session["Table"];
                string ip = Session["ip"].ToString();

                InsertDownloadLog(dt, ip, "Godown");
            }
        }
        catch { }
    }


    public string GetClientIP()
    {
        try
        {
            string ip = "";

            string forwarded = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!String.IsNullOrEmpty(forwarded))
            {
                string[] arr = forwarded.Split(',');
                if (arr.Length > 0) ip = arr[0].Trim();
            }

            if (String.IsNullOrEmpty(ip))
            {
                ip = Request.ServerVariables["REMOTE_ADDR"];
            }

            System.Net.IPAddress addr;
            if (System.Net.IPAddress.TryParse(ip, out addr))
            {
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }

            return "";
        }
        catch
        {
            return "";
        }
    }




    private string SendSmsOtp(string mobile, string otp, string loginType)
    {
        try
        {
            string str1 = loginType + " App Download";
             string url = "http://10.115.145.40/aclsms/SENDOTP_NRT.asmx";
            //string url = "https://mpeuparjan.mp.gov.in/aclsms/sendOTP_nrt.asmx";

            string soapBody =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" "
                + "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" "
                + "xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                + "<soap:Body>"
                + "<SmsOTP xmlns=\"http://tempuri.org/\">"
                + "<UserName>csms</UserName>"
                + "<AppPwd>csms*#@123$nic</AppPwd>"
                + "<Mobileno>" + mobile + "</Mobileno>"
                + "<tmpltID>1307164878551844855</tmpltID>"
                + "<msg>OTP for " + str1 + " is:" + otp + ".By Dept of food</msg>"
                + "</SmsOTP>"
                + "</soap:Body>"
                + "</soap:Envelope>";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/xml; charset=utf-8";
            request.Headers.Add("SOAPAction", "\"http://tempuri.org/SmsOTP\"");

            using (Stream stream = request.GetRequestStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(soapBody);
                stream.Write(data, 0, data.Length);
            }

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();

                var doc = new System.Xml.XmlDocument();
                doc.LoadXml(result);

                var node = doc.GetElementsByTagName("SmsOTPResult")[0];
                if (node != null && node.InnerText.ToLower() == "true")
                {
                    return "Success";
                }
                else
                {
                    return "Failed: " + result;
                }
            }

        }
        catch (Exception ex)
        {
            return "Exception : " + ex.Message.ToString();
        }
    }

    protected void radioInsp_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            panelgodownapk.Visible = false;
            pnlDownload.Visible = false;
            otpSection.Visible = false;
            phoneSection.Visible = true;
            btnVerify.Visible = false;
            btnOtp.Visible = true;
            txt_otp.Text = string.Empty;
        }
        catch (Exception ex) { }
    }
    private void InsertDownloadLog(DataTable dt, string ip, string apptype)
    {
        try
        {
            string query =
                @"INSERT INTO JointVentureScheme2018.dbo.AppDownloadLog
              (District_ID,BranchID, GodownID, Name, MobileNo, DownloadDate, IP, AppType,Status)
              VALUES (@DistrictID,@BranchID,@GodownID,@Name,@MobileNo,GETDATE(),@IP,@AppType,@Status)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@DistrictID", dt.Rows[0]["District_Id"].ToString());
                cmd.Parameters.AddWithValue("@BranchID", dt.Rows[0]["BranchID"].ToString());
                cmd.Parameters.AddWithValue("@GodownID", dt.Rows[0]["GodownID"].ToString());
                cmd.Parameters.AddWithValue("@Name", dt.Rows[0]["Name"].ToString());
                cmd.Parameters.AddWithValue("@MobileNo", dt.Rows[0]["MobileNo"].ToString());
                cmd.Parameters.AddWithValue("@IP", ip);
                cmd.Parameters.AddWithValue("@AppType", apptype);
                cmd.Parameters.AddWithValue("@Status", "Downloaded");

                cmd.ExecuteNonQuery();
            }
        }
        catch { }
    }
    private void SendApk(string path, string fileName)
    {
        if (!File.Exists(path))
        {
            lblMessage.CssClass = "text-danger";
            lblMessage.Text = "APK file not found!";
            return;
        }

        Response.Clear();
        Response.ContentType = "application/vnd.android.package-archive";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.TransmitFile(path);
        Response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}