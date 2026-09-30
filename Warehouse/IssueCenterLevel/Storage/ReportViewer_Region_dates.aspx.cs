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


public partial class IssueCenterLevel_Storage_ReportViewer_Region_dates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fromDate.Attributes.Add("onkeypress", "return CheckCalDate(this);");
        toDate.Attributes.Add("onkeypress", "return CheckCalDate(this);");
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (fromDate.Text == "" || toDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter fromdate and todate');  </script> ");
        }
        else
        {

            ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            string url = Session["reporturl"].ToString();


            try
            {

                String Language;
                string folder = "";
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
                if (url == "rpt_MPWLC_C_U_Districtwise_between2dates")
                {

                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "Godown_CPT_and_UTL_betweenDate")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "rpt_BranchWise_openindClosing_Btwn_date")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "rpt_MPWLC_C_U_Districtwise_between2dates_HiredType")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "rpt_MPWLC_C_U_Districtwise_between2dates_StorageType")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "GodownWiseReceiveIssue_BetweenDates")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "CommodityWiseGodownWiseTotalState")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "fromdate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "todate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "State_GatepassDateWiseStockIssueDetail")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "NewGodowns_Created_BW_Dates")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "State_WheatPSS_1819_DiffHourBTWN_Date")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "State_WheatPSS_1819_BTWN_DateandTime")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Nafed_WHR_Detail_ToPay")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Region.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "FromDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(fromDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "ToDate";
                    reportParameterCollection[1].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
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
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

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
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                string userName = ConfigurationManager.ConnectionStrings["uname"].ProviderName;
                string password = ConfigurationManager.ConnectionStrings["psw"].ProviderName;
                string domain_warehouseName = ConfigurationManager.ConnectionStrings["domain"].ProviderName;
                return new System.Net.NetworkCredential(userName, password, domain_warehouseName);
            }
        }

        #endregion

    }
}
