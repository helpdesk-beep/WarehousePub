using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WLCBusinessLayer;

public partial class _Default12 : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            DataTable dt = new Admin().GetNewsList();
            dt = new Admin().GetNewsList();
            //if (dt.Rows.Count > 0)
            //{
            //    rptNewsUpdate.DataSource = dt;
            //    rptNewsUpdate.DataBind();
            //    rptNewsUpdate.Visible = true;
            //}         
            //else
            //{
            //    rptNewsUpdate.Visible = false;
            //}

             dt = new Admin().GetDownloadList();
            if (dt.Rows.Count > 0)
            {
                rptDownload.DataSource = dt;
                rptDownload.DataBind();
            }
        }
        
    }
}

