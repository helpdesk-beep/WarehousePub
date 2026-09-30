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

public partial class GenericErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //Exception ex=null;
        //ex=Server.GetLastError();

        //lblError.Text = ex.ToString();

    }
    protected void lnlLogin_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../../login.aspx");
        Response.Redirect("~/login.aspx");
    }
}
