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

public partial class StatePages_State_AGMP_Reports : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_DistrictWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CSM_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_CSM_Proc201920_District_Wise_GodownTypeWise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "State_Arhar_Proc201920_District_Wise";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_Arhar_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_eWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Pending_Print_EWHR";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2019_20_eWHR_MPWLCGdwn";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CoarseGrain_Procurement_2019_20";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2019_20_MPWLC";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2012_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Paddy_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton19_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Bajra_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton20_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Jowar_Procurement_2020_21";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton24_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Moong_Urad_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
}