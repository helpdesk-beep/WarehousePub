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

public partial class Region_Report_Region : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                // lblRegionReports.Text = rm.GetString("lblRegionReports");
                lblRegionReports.Text = Resources.hindi.lblRegionReports;
            }

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_CommodityDetails";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_CommodityDetails_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
            //Response.Redirect("~/Reports/States/ReportViewer_Region.aspx");
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_C_U";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_C_U_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_HQ01";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }
        else
        {
            Session["reporturl"] = "";
            Session["reporturl"] = "rpt_MPWLC_HQ01_CSPS";
            ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "RegisteredOperator";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "OperatorLoginReport_issue";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl_mpscsc"] = "";
        Session["reporturl_mpscsc"] = "RegisteredOperatorDM";
        Session["reporturl"] = "";
        Session["reporturl"] = Session["reporturl_mpscsc"];
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_GodownwiseStackPosition";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_CommodityDetails_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_HQ01_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);

    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_Districtwise_between2dates";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region_dates.aspx\",\"_blank\")", true);

    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_CSPS_now";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"http://10.131.0.20/wlcgr/StockPosition_Depot.aspx\",\"_blank\")", true);

    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_MPWLC_C_U_CSPS_now_Scientific";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Stateregion_drilldown";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lnkdelWHRCompare_Click(object sender, EventArgs e)
    {

        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Compare_Old_New_WHR";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lnk_allentryReport_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_All_DataEntryDistrictandBrachwise";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }
    protected void lblDeleteAllWHRDOReport_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_state_delete_Summary";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Region/ReportViewer_Region.aspx");
    }


    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Pending_gatepassdetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptGodownwiseStackPosition_district";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }

    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_Pending_gatepassdetails_Opraterwise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Region.aspx\",\"_blank\")", true);
    }
}
