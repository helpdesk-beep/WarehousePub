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

public partial class Special_PV_State_Special_PV_State_Welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (Session["role"] != null)
        {
            //lbl_user.Text = Session["UserName"].ToString();
            //lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        else
        {
            Session.Abandon();
            Response.Redirect("/Special_PV/Special_PV_Default.aspx");
        }
    }
}