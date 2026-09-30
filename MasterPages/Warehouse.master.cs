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
using WLCBusinessLayer;

public partial class MasterPages_Warehouse : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            lblVCount.Text ="Visitors "+ Application["NoOfVisitors"].ToString();

            DataTable dt = new Admin().GetMarqueeList();
            if (dt.Rows.Count > 0)
            {
                rptMarquee.DataSource = dt;
                rptMarquee.DataBind();
            }
        
        }

    }
}
