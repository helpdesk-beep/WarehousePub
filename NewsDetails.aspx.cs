using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using WLCBusinessLayer;

public partial class NewsDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new Admin().GetNewsList();

        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
              int id= Convert.ToInt32( Request.QueryString["Id"].ToString());
              dt=new Admin().GetNewsById(id);
              if (dt.Rows.Count > 0)
              {
                  lblTitle.Text = dt.Rows[0]["Title"].ToString();
                  lblDesc.Text = dt.Rows[0]["Description"].ToString();
                 // hplDownload.NavigateUrl = "Admin/news_file/" + dt.Rows[0]["Filename"].ToString();
                 // hplDownload.Visible = true;
                 frFileViewer.Attributes["src"] = "Admin/news_file/" + dt.Rows[0]["Filename"].ToString();
                 frFileViewer.Attributes["style"] = "width: 100%; height: 1000px;";
              }

              else
              {
                 // hplDownload.Visible = false;
              
              }
            
            
            }
        
        }
    }
}