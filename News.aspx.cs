using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WLCBusinessLayer;


public partial class News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new Admin().GetNewsList();
        dt = new Admin().GetNewsList();
        rptNewsUpdate.DataSource = dt;
        rptNewsUpdate.DataBind();
    }
}