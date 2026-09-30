using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class CustomError : System.Web.UI.Page
{
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
           
            if (Session["errdesc"] != null)
            {
                lblErrorMsg.Text = "";
                lblErrorMsg.Text = "Error Description : " + Session["errdesc"].ToString();
                Session["lang"] = "";
                Session.Abandon();
                Session.Clear();

            }
            else 
            {
                Session["lang"] = "";
                Session.Abandon();
                Session.Clear();
                lblErrorMsg.Text = "";
            }
            
      



    }


    protected void lnlLogin_Click(object sender, EventArgs e)
    {
        Session["lang"] = "";
        Session.Abandon();

        Response.Redirect("login.aspx");
    }
}
