using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Rpt_MPSCSC_Commodity_Region : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetReportdata();
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

    private void GetReportdata()
    {
        RV_MPSCSCRegion.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        //string url = Session["reporturl"].ToString();
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string Language = "";
            string serverFullName = null;
            Language = "2";


            string path = Request.Url.ToString();
            int index = path.IndexOf(":") + 3;
            string path2 = path.Substring(index);
            int index2 = path2.IndexOf("/");
            int index3 = path2.IndexOf("/", index2 + 1);
            string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

            if (index3 > 0)

                serverFullName = path2.Substring(0, index3);
            else
                serverFullName = servername;

            RV_MPSCSCRegion.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            RV_MPSCSCRegion.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            RV_MPSCSCRegion.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RV_MPSCSCRegion.ServerReport.ReportServerUrl = new Uri(reportURL);
            RV_MPSCSCRegion.ServerReport.ReportPath = folder + "/" + "RPT_WHR_Details_MPSCSC_Commodity_RegionWise";
            //RV_MPSCSCRegion.ServerReport.ReportPath = folder + "RPT_WHR_Details_MPSCSC_Commodity_RegionWise";
            RV_MPSCSCRegion.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            RV_MPSCSCRegion.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Language";
            reportParameterCollection[0].Values.Add(Language.ToString());
            RV_MPSCSCRegion.ServerReport.SetParameters(reportParameterCollection);
            RV_MPSCSCRegion.ServerReport.Refresh();
        }
        catch (Exception ex)
        {
            //  Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
}