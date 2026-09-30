using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

public partial class GalleryDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["hdnTitle"].ToString() != null)
        { 
           // string title=Request.QueryString["Title"].ToString();
            string title = Session["hdnTitle"].ToString();
            litTitle.Text = title;

            DataTable dt = new Common().GetGalleryByTitle(title);
            if (dt.Rows.Count > 0)
            {

                rptGallery.DataSource = dt;
                rptGallery.DataBind();

            
            }
           
        
        }
       

    }
}