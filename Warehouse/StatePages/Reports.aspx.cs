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

public partial class StatePages_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"].ToString() == "Hindi")
        {
            LinkButton1.Text = Resources.hindi.HyperLink1;
            LinkButton12.Text = Resources.hindi.HyperLink12;
            LinkButton2.Text = Resources.hindi.HyperLink2;
            LinkButton13.Text = Resources.hindi.HyperLink13;
            LinkButton4.Text = Resources.hindi.HyperLink4;
            LinkButton6.Text = Resources.hindi.HyperLink6;
            LinkButton7.Text = Resources.hindi.HyperLink7;
            LinkButton8.Text = Resources.hindi.HyperLink8;
            LinkButton9.Text = Resources.hindi.HyperLink9;
            LinkButton18.Text = Resources.hindi.HyperLink18;
            LinkButton16.Text = Resources.hindi.HyperLink16;
            LinkButton19.Text = Resources.hindi.HyperLink19;
            LinkButton14.Text = Resources.hindi.HyperLink14;
            LinkButton15.Text = Resources.hindi.HyperLink15;
            LinkButton11.Text = Resources.hindi.HyperLink11;
            LinkButton10.Text = Resources.hindi.HyperLink10;
            LinkButton17.Text = Resources.hindi.HyperLink17;
            LinkButton5.Text = Resources.hindi.HyperLink5;
            LinkButton21.Text = Resources.hindi.LinkButton21;
            LinkButton20.Text = Resources.hindi.LinkButton20;
            LinkButton3.Text = Resources.hindi.LinkButton3;
            lblStorageReports.Text = Resources.hindi.lblStorageReports;
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDepositorLedger";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }

    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockRegister";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Rpt_Delivery_Order_New";
        //ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Depot/ReportViewer_Depot.aspx");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptWarehouseReceipt";
        // ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
        Response.Redirect("~/Reports/Branch/RptWarehouseReceipt.aspx");
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStackWiseConditionReportRegister";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDetailsofReceiptIssueStockAgaintWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_gatepassdetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rpt_GodownwiseStackPosition";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockValuationRegister";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDetails_of_Daily_Issue_Commoditywise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDaily_Commodity_Receipt_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDO_Wise_Issue_Details";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptSchemeWiseOutflow";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptTransactions";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptTruckChallan_SendingDetails";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptAcknowledgement";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStockRegister_GodownNCommodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptDetails_of_Daily_Issue_Commoditywise_ForAllCommodity";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "rptStackwiseRegister_ForAllStack";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"ReportViewer_Depot.aspx\",\"_blank\")", true);
    }
}
