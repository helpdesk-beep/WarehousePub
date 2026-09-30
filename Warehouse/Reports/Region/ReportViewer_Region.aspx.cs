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
using System.Security.Principal;


public partial class IssueCenterLevel_Storage_ReportViewer_Region : System.Web.UI.Page
{
    string url = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Showreport();
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
 
    protected void ddl_Language_SelectedIndexChanged(object sender, EventArgs e)
    {
        Showreport();
    }

    private void Showreport()
    {
        ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        url = Session["reporturl"].ToString();

        try
        {
            String Language;
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
            if (Session["lang"].ToString() == "Hindi")
                Language = "1";
            else
                Language = "2";


            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
            ReportViewer_Region.Visible = true;

            string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

            //for state reports

            if (url == "Rpt_Stateregion_drilldown")
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
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "Language";
                reportParameterCollection[0].Values.Add(ddl_Language.SelectedValue.ToString());
                ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
            }
            if (url == "rpt_Compare_Old_New_WHR")
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
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "Language";
                reportParameterCollection[0].Values.Add(ddl_Language.SelectedValue.ToString());
                ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
            }

            if (url == "rpt_All_DataEntryDistrictandBrachwise")
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
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "Language";
                reportParameterCollection[0].Values.Add(ddl_Language.SelectedValue.ToString());
                ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
            
            }

            if (url == "rpt_state_delete_Summary")
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
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "Language";
                reportParameterCollection[0].Values.Add(ddl_Language.SelectedValue.ToString());
                ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (this.ReportViewer_Region.ServerReport.IsDrillthroughReport)
        {
            this.ReportViewer_Region.PerformBack();
        }
        else
        {
            Response.Redirect("~/IssueCenterLevel/Storage/Report_Region.aspx");
        }
    }
}
