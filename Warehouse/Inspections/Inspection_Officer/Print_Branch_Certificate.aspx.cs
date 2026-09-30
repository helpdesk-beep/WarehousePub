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

public partial class Inspections_Inspection_officer_Print_Branch_Certificate : System.Web.UI.Page
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
            lblbranchname.Text = Session["lblDepotname"].ToString();
            lblbranchname2.Text = Session["lblDepotname"].ToString();
            lblbranch.Text = Session["lblDepotname"].ToString();
            lblbranch2.Text = Session["lblDepotname"].ToString();
        }
    }


}