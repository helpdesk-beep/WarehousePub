using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lblVCount.Text ="Visitors "+ Application["NoOfVisitors"].ToString();

            //  DataTable dt = new Admin().GetMarqueeList();
            //if (dt.Rows.Count > 0)
            //{
            //    rptMarquee.DataSource = dt;
            //    rptMarquee.DataBind();
            //}
        
        }
        Application_PreSendRequestHeaders();
    }
    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-Powered-By");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
    }
}
