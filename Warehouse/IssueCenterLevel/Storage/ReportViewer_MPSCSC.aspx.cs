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
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Security.Principal;

public partial class IssueCenterLevel_Storage_ReportViewer_MPSCSC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        if (!IsPostBack)
        {
            try
            {

                String Language;
                string folder = "";
                //**Dynamic later
                if (int.Parse(Session["RoleId"].ToString()) == 1)
                {
                    District.Text = Session["Depot_DistID"].ToString();
                    Depot.Text = Session["Depot_DepotID"].ToString();
                }
                else
                {
                    District.Text = "2301";
                    Depot.Text = "2301001";
                }
                if (Session["lang"].ToString() == "Hindi")
                    Language = "1";
                else
                    Language = "2";

                //string path = Request.Url.ToString();
                //int index = path.IndexOf(":") + 3;
                //string path2 = path.Substring(index);
                //int index2 = path2.IndexOf("/");
                //int index3 = path2.IndexOf("/", index2 + 1);
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

                ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL

                ReportsViewer_MPSCSC.Visible = true;


                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                //for state reports
                if (url == "State_CropYearWise_MPSCSC_Stock")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "State_CommodityWise_CropYearWise_MPSCSC_Stock")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "rpt_AllCommodityStockReport")
                {

                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "Rpt_WHR_Detailed_MPWLC")
                {

                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "Rpt_WHR_Detailed_MPWLC_Commodity")
                {

                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "GodownsTotalRecDel")
                {

                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "Gunny_StockReport")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "MPSCSC_Rice_StockReport")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "State_WheatPSS_Stock")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "MPSCSC_WheatReportTillDate")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "MPSCSC_RiceReportTillDate")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "MPSCSC_CmdHiredTypeWise_TillDate")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "StockReportMPSCSC_TillDate")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Wheat_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Paddy_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Rice_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Maize_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_CG_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Salt_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Sugar_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "IssueCenterWise_Gunny_Stock_Report")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "GdwnTypeW_Diff_BW_GPIssue_N_EntryDate")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "GodownWiseIssueDetailwithGPandDO")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "MPSCSC_CurrentStock_with_ClosingBalance")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                if (url == "State_Cropyearwise_cmdtwise_stock")
                {
                    ReportsViewer_MPSCSC.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportsViewer_MPSCSC.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportsViewer_MPSCSC.ServerReport.ReportPath = folder + "/" + url;
                    ReportsViewer_MPSCSC.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportsViewer_MPSCSC.ShowCredentialPrompts = false;
                }
                //end for state reports
            }

            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
    }
    [Serializable]
    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
        out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            return false;
        }

        // Specifies the user to impersonate when connecting to a report server. 
        //A WindowsIdentity object representing the user to impersonate.
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        // Returns network credentials to be used for authentication with the report server. 
        //A NetworkCredentials object.
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                //you can place below settings in configuration xml file
                //string userName = "Administrator";
                //string password = "nic123";
                //string domain_warehouseName="VALUED-RDPRSG34\\SQL2008";
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion

    }
}
