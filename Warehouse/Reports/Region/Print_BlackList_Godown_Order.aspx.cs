using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

public partial class Reports_Region_Print_BlackList_Godown_Order : System.Web.UI.Page
{
    const String SUCCESS = "SUCCESS";
    const String FAIL = "FAIL";
    //int rqid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lbltdate.Text= DateTime.Now.Date.ToString("dd/MM/yyyy");
            //lbldate.Text= DateTime.Now.Date.ToString("dd/MM/yyyy");
            //lbldatet.Text= DateTime.Now.Date.ToString("dd/MM/yyyy");
            //lbldatett.Text= DateTime.Now.Date.ToString("dd/MM/yyyy");
            //lbltdate.Text = "--------------------";
            //lbldatett.Text = "--------------------";
            //lbldatet.Text = "--------------------";
            //lbldate.Text = "--------------------";

            lblwarehousename.Text = Session["hdngodownname"].ToString();
            lblbarnch.Text = Session["hdnbranch"].ToString();
            lbldistirict1.Text = Session["hdndistrict"].ToString();
            lblregionname.Text = Session["hdnregionname"].ToString();
            lblression.Text = Session["hdncategory"].ToString();
        }
    }


}