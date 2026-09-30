using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;

public partial class BusinessReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt;
            dt = new Admin().GetBusinessReportList();
            if (dt.Rows.Count > 0)
            {
                rptMonRep.DataSource = dt;
                rptMonRep.DataBind();
            }
        
        }
    }
}