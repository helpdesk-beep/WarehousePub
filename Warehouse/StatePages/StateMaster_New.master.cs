using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage_StateMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                spanHome.InnerText = Resources.hindi.spanHome;
                hypHome.Text = Resources.hindi.hypHome;
                spanReports.InnerText = Resources.hindi.spanReports;
                hypStateReports.Text = Resources.hindi.hypStateReports;
            }
        }
      
    }
}
