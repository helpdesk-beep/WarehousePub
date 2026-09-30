using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using WLCBusinessLayer;

public partial class Recruitment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            dt = new Common().GetRecruitment();
            if (dt.Rows.Count > 0)
            {
                rptRec.DataSource = dt;
                rptRec.DataBind();
            }

            else
            {
                lblErrorMsg.Text = "No Record Found";

            }

            
        }
    }
}