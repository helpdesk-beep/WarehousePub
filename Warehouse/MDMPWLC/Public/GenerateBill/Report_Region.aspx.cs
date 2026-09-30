using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Resources;
using System.Reflection;
//using ChattishgrahDemo;
using System.Globalization;
using System.Threading;

using System.Text;

public partial class MSMPWLC_Report_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {

                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);

                // lblRegionReports.Text = rm.GetString("lblRegionReports");
               //blRegionReports.Text = Resources.hindi.lblRegionReports;
            }

        }
    }
    protected void LinkButton202_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton203_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Bill_Detail_After_Region_Difference_Wise_With_Amount.aspx\",\"_blank\")", true);
    }

    protected void LinkButton204_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_Pending_Bill_Detail_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton205_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Pending_Bill_Details_Motn_Wise.aspx\",\"_blank\")", true);
    }

    protected void LinkButton206_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Region_Wise_Summary_of_Panding_Payment_For_Godown.aspx\",\"_blank\")", true);
    }

    protected void LinkButton208_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_Except_and_Received_Payment_From_MPSCSC_Details_From_Aug.aspx\",\"_blank\")", true);

    }

    protected void LinkButton207_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Bill_Detail_After_Region_Difference_Wise_With_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton209_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Search_HO_StorageBillVerification_N.aspx\",\"_blank\")", true);

    }

    protected void LinkButton210_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_StorageBill_SearchStatus.aspx\",\"_blank\")", true);
    }

    protected void LinkButton211_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Region_And_Month_Wise_Pending_Amount.aspx\",\"_blank\")", true);

    }

    protected void LinkButton212_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Payment_Credit_From_MPWLC_To_Godown.aspx\",\"_blank\")", true);

    }

    protected void LinkButton213_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Payment_BillsFromCSMStoMPWLC_Status_Rept_Region_Wise.aspx\",\"_blank\")", true);

    }

    protected void LinkButton214_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_UnexpiredAmount_From_Aug.aspx\",\"_blank\")", true);
    }

    protected void LinkButton215_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_UnexpiredAmount_From_Jan.aspx\",\"_blank\")", true);
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../Reports/States/Rpt_Get_Recived_and_Pending_Amount_From_Aug.aspx\",\"_blank\")", true);

    }
    protected void LinkButton21_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Wheat_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton22_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "CMS_Procurement_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }

    protected void LinkButton23_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "NAFED_WHRPrint_CMS_Proc_2021_22";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../../../StatePages/State_ReportViewerProc201920.aspx\",\"_blank\")", true);
    }
}
