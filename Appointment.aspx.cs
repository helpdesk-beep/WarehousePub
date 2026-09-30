using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using WLCBusinessLayer;

public partial class Appointment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            dt = new Common().GetAppointment();
            if (dt.Rows.Count > 0)
            {
                rptAppnt.DataSource = dt;
                rptAppnt.DataBind();
            }

            else
            {
                lblErrorMsg.Text = "No Record Found";

            }


        }
    }
}