using System;
using System.Web.Services;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


/// <summary>
/// Summary description for SendMail
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SendMail : System.Web.Services.WebService
{

    public SendMail()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SendEmail(string sendTo, string subject, string message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(sendTo))
                return "Please enter email id.";
            if (string.IsNullOrWhiteSpace(subject))
                return "Please enter subject.";
            if (string.IsNullOrWhiteSpace(message))
                return "Please enter message.";
            if (!IsValidEmail(sendTo))
                return "Invalid email id. Please enter correct email address.";
            string currentYear = DateTime.Now.Year.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(
                "asharma11082005@gmail.com",
                "MPWLC Official"
            );
            mail.To.Add(sendTo);
            mail.Subject = subject;
            mail.IsBodyHtml = true;

            string htmlBody = @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>MPWLC Mail</title>
                    </head>

                    <body style='margin:0;padding:0;background:#f3f3f3;font-family:Arial,Helvetica,sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='padding:25px 10px;background:#f3f3f3;'>
                    <tr>
                    <td align='center'>

                    <table width='100%' cellpadding='0' cellspacing='0' border='0'
                    style='max-width:620px;background:#ffffff;border:1px solid #e0e0e0;border-radius:8px;'>

                    <tr>
                    <td style='height:5px;background:#b30000;'></td>
                    </tr>

                    <tr>
                    <td align='center' style='padding:35px 20px 15px 20px;'>
                    <div style='font-size:70px;'>🚀</div>
                    </td>
                    </tr>

                    <tr>
                    <td align='center'>
                    <div style='font-size:30px;font-weight:bold;color:#b30000;'>
                    Welcome!
                    </div>
                    </td>
                    </tr>

                    <tr>
                    <td align='center' style='padding:15px 35px;color:#666666;font-size:16px;line-height:28px;'>
                    You have successfully received an official communication from MPWLC.
                    </td>
                    </tr>

                    <tr>
                    <td align='center' style='padding:10px 35px;color:#333333;font-size:16px;line-height:30px;'>
                    " + message + @"
                    </td>
                    </tr>

                    <tr>
                    <td align='center' style='padding:20px 20px 35px 20px;'>

                    <a href='https://mpwarehousing.mp.gov.in/' target='_blank'
                    style='background:#b30000;
                    color:#ffffff;
                    text-decoration:none;
                    padding:14px 30px;
                    font-size:15px;
                    font-weight:bold;
                    display:inline-block;
                    border-radius:4px;'>

                    VISIT NOW

                    </a>

                    </td>
                    </tr>

                    <tr>
                    <td align='center'
                    style='background:#fafafa;padding:20px 25px;color:#777777;font-size:13px;line-height:24px;'>

                    Thank you for using MPWLC Services.<br/>
                    © " + currentYear + @" MPWLC. All Rights Reserved.

                    </td>
                    </tr>

                    </table>

                    </td>
                    </tr>
                    </table>

                    </body>
                    </html>";

            mail.Body = htmlBody;

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            ServicePointManager.SecurityProtocol = LegacyJsonHttpClient.Tls12Protocol;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential(
                "asharma11082005@gmail.com",
                "jqge wolh jceo hogk"
            );
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 20000;
            smtp.Send(mail);

            return "Mail Sent Successfully.";
        }
        catch (FormatException)
        {
            return "Invalid email id.";
        }
        catch (SmtpFailedRecipientsException)
        {
            return "Email address not found.";
        }
        catch (Exception ex)
        {
            return "ERROR DETAILS: " + ex.ToString();
        }
    }

    // Email Validation Function
    private bool IsValidEmail(string email)
    {
        try
        {
            MailAddress mail = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
