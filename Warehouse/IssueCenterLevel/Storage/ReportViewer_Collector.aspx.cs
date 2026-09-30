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

public partial class IssueCenterLevel_Storage_ReportViewer_Collector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();

        if (!IsPostBack)
        {
            try
            {
                String Language;
                string folder = "";
                //**Dynamic later
              
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

                ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL

                ReportViewer_Collector.Visible = true;

                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                //for state reports
                if (url == "District_Owned_Godown_CPT_And_UTL")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];                  
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_JVS_Godown_CPT_and_UTL")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_WDRA_Godown_CPT_And_UTL")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_SteelSilo_CPT_and_UTL")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "rpGodownWise_openindClosing_Btwn_date_Dist")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_PVTPEG_CPT_And_UTL")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "CommodityWiseStock_Coll_rpt")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DIS";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_PaddyKharifProc2016_17")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_CoarseGrainsKharif201617")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_BranchWise_GodonWise_Stock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DIS";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_CropYearWise_WhrWise_Stock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DIS";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_GodownWise_MPSCSCStock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DIS";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_BranchWise_GodownList")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DIS";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_Godown_Capacity_And_Utillazation")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtID";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_CommodityWise_CropYearWise_Stock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtID";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "Collector_CommodityWise_MPSCSC_Stock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtID";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_WheatCropYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_PaddyCropYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_RiceCropYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_MaizeCYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_CoarseGrainsCYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Collector_DepositorWise_commodityWise_Stock")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }


                else if (url == "Collector_BranchOpeningClosing")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_TillDate_GunnyCYWiseReport")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "District_GodownWiseCmdSummary")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District_Id";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "Dist_Godown_Login_Detail")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "District_WheatProc_2017_18")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_PaddyKharifProc2019_20")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "District_CoarseGrainsKharif2019_20")
                {
                    ReportViewer_Collector.ServerReport.Refresh();
                    string reportURL = "";
                    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_Collector.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_Collector.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_Collector.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_Collector.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_Collector.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Collector.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Collector.ServerReport.SetParameters(reportParameterCollection);
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
