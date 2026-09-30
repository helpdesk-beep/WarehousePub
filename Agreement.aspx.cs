using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using WLCBusinessLayer;


public partial class Agreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            dt = new Common().GetAgreementBySecId(1);
            if (dt.Rows.Count > 0)
            {
                rptEst.DataSource = dt;
                rptEst.DataBind();
                rptEst.Visible = true;
            }

            dt = new Common().GetAgreementBySecId(2);
            if (dt.Rows.Count > 0)
            {

                rptSecr.DataSource = dt;
                rptSecr.DataBind();
                rptSecr.Visible = true;
            }


            dt = new Common().GetAgreementBySecId(3);
            if (dt.Rows.Count > 0)
            {
                rptBus.DataSource = dt;
                rptBus.DataBind();
                rptBus.Visible = true;
            }


            dt = new Common().GetAgreementBySecId(4);
            if (dt.Rows.Count > 0)
            {
                rptTech.DataSource = dt;
                rptTech.DataBind();
                rptTech.Visible = true;
            }


            dt = new Common().GetAgreementBySecId(5);
            if (dt.Rows.Count > 0)
            {
                rptAcc.DataSource = dt;
                rptAcc.DataBind();
                rptAcc.Visible = true;
            }


            dt = new Common().GetAgreementBySecId(6);
            if (dt.Rows.Count > 0)
            {
                rptCon.DataSource = dt;
                rptCon.DataBind();
                rptCon.Visible = true;
            }


        }
    }
}
