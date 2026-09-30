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

public partial class Reports_States_StockReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                string url = Session["reporturl"].ToString();
                string folder = "";
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
                ReportViewer1.Visible = true;
                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                if (url == "MPSCSC_CurrentStock_with_ClosingBalance")
                {
                    ReportViewer1.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer1.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer1.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer1.ShowCredentialPrompts = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

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
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        string folder = ConfigurationManager.ConnectionStrings["rptfolder_csms"].ProviderName;
        if (this.ReportViewer1.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer1.PerformBack();
        }
        else
        {
            Response.Redirect("~/PPDS/Default.aspx");
        }
    }
}

