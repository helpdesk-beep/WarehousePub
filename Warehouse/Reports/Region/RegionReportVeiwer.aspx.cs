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
using System.Data.SqlClient;

public partial class Reports_Region_RegionReportVeiwer : System.Web.UI.Page
{
    string url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            url = Session["reporturl"].ToString();
            try
            {
                string folder = "";
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
                ReportViewer_Region.Visible = true;
                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                if (url == "Rpt_dist_whr_detail_cropyr")
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
                    reportParameterCollection[0].Name = "Dist";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "rpt_rigionwise_pendinggatepass")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Rpt_RigionWise_Whr_Do_PendingReciving")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }


                if (url == "Rpt_RegionWise_CropWise_WHR")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Godownlist_on_Region")
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
                    reportParameterCollection[0].Name = "Regionid";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "ProcurmentPendingRigion")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "ProcurementstatusRegion15")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "ProcurementstatusRegion16")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "paddyProcmfd1516")
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
                    reportParameterCollection[0].Name = "Dist";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Regionwisemtotalwhr15")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "rpt_MPWLC_C_U_Region")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[4];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("0");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Region_Id";
                    reportParameterCollection[2].Values.Add(Session["Region_ID"].ToString());
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "GodownCapacityDetails")
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
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "GodownCapacityDetails_Region")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }


                if (url == "paddyProcRegion516")
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
                    reportParameterCollection[0].Name = "rid";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Bhugtanrashidatewise")
                {
                    url = "BhugtanRashiTtlRegionWise";
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add("1");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_Master_Bill_Report")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "RegionId";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "CommodityLossBranchWiseSummary")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_WDRA_Godown_CPT_and_UTL")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_SteelSilo_Godown_CPT_and_UTL")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_PVT_PEG_Godown_CPT_and_UTL")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_JVS_Godown_CPT_and_UTL")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }
                if (url == "Region_Owned_Godown_Capacity_And_Utillazation")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }
                if (url == "RegionDiff_BW_GPIssueAndEntryDate")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }
                if (url == "Region_DiffBW_WHRIssueAndEntryDate")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }
                if (url == "Region_BranchWiseStockReport")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "Region_GodownWiseCurrentStock")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_KharifProc2016_17_AllComm")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_KharifProc2016_17_AllComodity")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_GodownWise_CommodityWise_MPSCSC_Stock")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "WheatPSS_Gain_RegionSummary_Manual")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CmdHiredTypeWise_TillDate")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_StockReportMPSCSC_TillDate")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RegionDistrictWise_Paddy_Stock")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "RegionDistrictWise_Rice_Stock")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_IssueCenterWise_BranchDetail")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "RegionCommodityStock_GraphRepresentation")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "RegionCropYeraWiseStock_ChartRepresentation")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "RegionCpt_Utl_GraphRepresentation")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_GodownWise_Stack_StockPosition")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Wheat_Procurement2017_18")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_wheatpss_1718_RemainingDF_For_WHR")
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
                    reportParameterCollection[0].Name = "region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_GatepassDateWiseStockIssueDtl")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Arhar_Proc_2017_18")
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
                    reportParameterCollection[0].Name = "RegionId";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Onion_Proc_2017_18")
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
                    reportParameterCollection[0].Name = "RegionId";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RegionPremWise_GodownComparision")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_KharifProc2017_18_CommodityWise")
                {
                    ReportViewer_Region.ServerReport.Refresh();
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    string reportURL = "";
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Region.ServerReport.Timeout = 1800000;
                    ReportViewer_Region.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_GodownOwner_Details")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_MonthWiseCreatedBillDetails")
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
                    reportParameterCollection[0].Name = "Region_ID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Paddy_1718_RemainingDF_For_WHR")
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
                    reportParameterCollection[0].Name = "region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_WheatProcurement_2018_19")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_GramProc_2018_19")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_WheatProc1819_Without_CWCFCIMFDG")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_WheatProc1819_CWCFCIMFDG")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_ProvDF_FinalDF_WHR")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CMS_Proc1819_Without_CWCFCIMFDG_DistWise")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CMS_Proc1819_CWCFCIMFDG_DistWise")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_QCDeletedReport")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_DeletedWHR_Details")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_DeletedWHR_WithReqDate_DeletedDate")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_DistrictWise_HiredTypeWise_StockRpt")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_DistrictWiseVacantCpt")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "rpt_Region_New_Godown_Summary")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_VerifyGdwnReport2018")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_HiredTypeWiseCpt_VerifyGdwnRpt")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_VerifyGodownVacantCPT")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Kharif_Procurement_2018_19")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CoarseGrain_Procurement_2018_19")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Paddy_Procurement_2018_19")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_Wheat_Procurement_2019_20")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_DSC_UploaderDetails")
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
                    reportParameterCollection[0].Name = "RegionDI";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CMS_Procurement_2019_20")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_NAFED_WHRPrint_CMS_Procurement_2019_20")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Arahar_Procurement_2019_20")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Wheat_Procurement_2019_20_eWHR")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Godown_Owner_Account_Detail")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                if (url == "Region_StorageChrg_GnrtBillSummary2019_MPWLCGdwn")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_StorageChrg_GnrtBillSummary2019_Other")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_GRent_PassingOrder_Detail")
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
                    reportParameterCollection[0].Name = "Region_Id";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_Kharif_Procurement_2019_20")
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
                    reportParameterCollection[0].Name = "RegionID";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_Bill_Payment_Status")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "Region_CourseGrain_Proc_2019_20")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_GRent_PassingOrder_Detail_Old")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_Wheat_Procurement_2020_21")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_CSM_Procurement_2020_21")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_NAFED_WHRPrint_CMS_Proc_2012_21")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                if (url == "RO_NAFED_WHRPrint_CMS_Proc_2021_22")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }



            }
            catch (Exception ex)
            {


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