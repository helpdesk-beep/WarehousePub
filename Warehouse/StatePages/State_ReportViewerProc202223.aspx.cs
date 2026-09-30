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

public partial class StatePages_State_ReportViewerProc202223 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        if (!IsPostBack)
        {
            try
            {
                string folder = "";
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
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
                ReportViewer_Region.Visible = true;

                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                if (url == "CMS_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Wheat_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Wheat_Procurement_2019_20_DistrictWise")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "State_CSM_Proc201920_District_Wise")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "State_CSM_Proc201920_District_Wise_GodownTypeWise")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "NAFED_WHRPrint_CMS_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Nafed_WHR_Detail_ToPay")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "State_Arhar_Proc201920_District_Wise")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "NAFED_WHRPrint_Arhar_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Wheat_Procurement_2019_20_eWHR")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Pending_Print_EWHR")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "NAFED_Com_WHRPrint_CMS_Proc_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Wheat_Procurement_2019_20_eWHR_MPWLCGdwn")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Paddy_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "CoarseGrain_Procurement_2019_20")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Paddy_Procurement_2019_20_MPWLC")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "Wheat_Procurement_2020_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "CMS_Procurement_2020_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "State_CMS_eWHR_2020")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                else if (url == "NAFED_WHRPrint_CMS_Proc_2012_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Paddy_Procurement_2020_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Jowar_Procurement_2020_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Bajra_Procurement_2020_21")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Wheat_Procurement_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "CMS_Procurement_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "CMS_GodownWise_eWHR_SubRpt_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Moong_Urad_Procurement_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "Nafed_Moong_Urad_GodownWise_eWHR_SubRpt_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "NAFED_WHRPrint_CMS_Proc_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "NAFED_WHRPrint_Moong_Urad_Proc_2021_22")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
                if (url == "NAFED_WHRPrint_CMS_Proc_2022_23")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                }
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
