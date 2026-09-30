using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Text;
/// <summary>
/// Summary description for CommonFuction
/// using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Data.SqlClient;

/// </summary>
public class CommonFuction
{
    
    public CommonFuction()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string sql;

    public DataSet   FillDatasetByProc(string Proc)
    {
         DBAccess db = new DBAccess();
         db.execute(Proc ,DBAccess.SQLType.IS_QUERY);
         return db.records();
    }
    public DataTable FillTableByProc(string Proc)
    {
        DBAccess db = new DBAccess();
        db.execute(Proc, DBAccess.SQLType.IS_QUERY);
        return db.records1();
    }
    public string dtFormat(string date)
    {
        if (date.Trim().IndexOf("-") > 0)
        {
            date = date.Split('-')[2] + "/" + date.Split('-')[1] + "/" + date.Split('-')[0];
        }
        else
            date = date.Split('/')[2] + "/" + date.Split('/')[1] + "/" + date.Split('/')[0];
        return date;
    }
    public void EmptyTextBoxes(Control parent)
    {

        // Loop through all the controls on the page

        foreach (Control c in parent.Controls)
        {

            // Check and see if it's a textbox

            if ((c.GetType() == typeof(TextBox)))
            {

                // Since its a textbox clear out the text  

                ((TextBox)(c)).Text = "";

            }
            else if ((c.GetType() == typeof(DropDownList)))
            {

                // Since its a textbox clear out the text  

                ((DropDownList)(c)).SelectedIndex = 0;

            }
            else if ((c.GetType() == typeof(RadioButton)))
            {
                ((RadioButton)(c)).Checked = false;
            }


            // Now we need to call itself (recursive) because

            // all items (Panel, GroupBox, etc) is a container

            // so we need to check all containers for any

            // textboxes so we can clear them

            if (c.HasControls())
            {

                EmptyTextBoxes(c);

            }

        }
    }
    
        public string GetFinYear()
    {
        int CurrentYear = DateTime.Today.Year;
        string PreviousYear = (CurrentYear - 1).ToString();
        string NextYear = (CurrentYear + 1).ToString();
        string finYear = "";
        //if (DateTime.Today.Month > 3)
        //    finYear = CurrentYear.ToString() + "-" + NextYear;
        //else
        //    finYear = PreviousYear + "-" + CurrentYear.ToString();


        if (DateTime.Today.Month > 3)
            finYear = (CurrentYear).ToString() + "-" + Convert.ToInt32(NextYear) ;
        else
            finYear = CurrentYear.ToString() + "-" + NextYear.ToString();

        return finYear;
    }
    public string GetFinYearNew()
    {
        int CurrentYear = DateTime.Today.Year;
        string PreviousYear = (CurrentYear - 1).ToString();
        string NextYear = (CurrentYear + 1).ToString();
        string finYear = "";
        //if (DateTime.Today.Month > 3)
        //    finYear = CurrentYear.ToString() + "-" + NextYear;
        //else
        //    finYear = PreviousYear + "-" + CurrentYear.ToString();


        if (DateTime.Today.Month > 3)
            finYear = (CurrentYear).ToString() + "-" + Convert.ToInt32(NextYear) ;
        else
            finYear = CurrentYear.ToString() + "-" + NextYear.ToString();

        return finYear;
    }
    public string SendMailwi(string toMail, string mailSubject, string mailBody)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("182.18.133.191");
            StringBuilder sb = new StringBuilder();

            mail.From = new MailAddress("C-NET<no-reply@cnet-india.com>");
            mail.To.Add(toMail);
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;

            mail.Body = mailBody;

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("no-reply@cnet-india.com", "no@cnet123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string SendMail(string toMail, string mailSubject, string mailBody, string filepath)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("182.18.133.191");

            mail.From = new MailAddress("C-NET<no-reply@cnet-india.com>");
            mail.To.Add(toMail);
            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = mailBody;
            Attachment att = new Attachment(filepath);
            mail.Attachments.Add(att);

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("no-reply@cnet-india.com", "no@cnet123");
            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
            return "Success";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public void Export(string fileName, GridView gv, string cap)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a form to contain the grid
                Table table = new Table();
                table.Caption = cap;
                table.BorderWidth = 1;
                table.BorderColor = System.Drawing.Color.Black;

                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                    CommonFuction.PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    row.BorderWidth = 1;
                    row.BorderColor = System.Drawing.Color.Black;

                    CommonFuction.PrepareControlForExport(row);


                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    CommonFuction.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                CommonFuction.PrepareControlForExport(current);
            }
        }
    }
    //HttpPostedFile fileupload = context.Request.Files[0];

    //   filename w/o the path
    //   string file = Path.GetFileName(fileupload.FileName);
    //   string saveLocation = HttpContext.Current.Server.MapPath("UploadDirName") + "\\" + file;
    //   fileupload.SaveAs(saveLocation);            

    //   MailMessage message = new MailMessage();

    //   *****useless stuff********
    //   message.To.Add("abc@xxx.com");
    //   message.Subject = "test";
    //   message.From = new MailAddress("test@aaa.com");
    //   message.IsBodyHtml = true;
    //   message.Body = "testing";
    //    *****useless stuff********

    //   Fault line
    //   message.Attachments.Add(new Attachment(saveLocation, MediaTypeNames.Application.Octet))

    //   Send mail 
    //   SmtpClient smtp = new System.Net.Mail.SmtpClient("xxxx", 25);
    //   smtp.UseDefaultCredentials = false;
    //   smtp.Credentials = new NetworkCredential("xxx", "xxxx");
    //   smtp.Send(message);
    //public string GetFinYear()
    //{
    //    int CurrentYear = DateTime.Today.Year;
    //    string PreviousYear = (CurrentYear - 1).ToString();
    //    string NextYear = (CurrentYear + 1).ToString();
    //    string finYear = "";
    //    if (DateTime.Today.Month > 3)
    //        finYear = CurrentYear.ToString() + "-" + NextYear;
    //    else
    //        finYear = PreviousYear + "-" + CurrentYear.ToString();


    //    //if (DateTime.Today.Month > 3)
    //    //    finYear = (CurrentYear + 1).ToString() + "-" + NextYear + 1;
    //    //else
    //    //    finYear = CurrentYear.ToString() + "-" + NextYear.ToString();

    //    return finYear;
  //  }
}


