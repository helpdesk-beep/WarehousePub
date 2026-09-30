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


public partial class IssueCenterLevel_Storage_ReportViewer_Region : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
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

                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

                ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL

                ReportViewer_Region.Visible = true;


                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                //for state reports
                if (url == "rpt_CommodityDetails")
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

                    ReportViewer_Region.ServerReport.Timeout = 1800000;

                    ReportViewer_Region.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("0");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add("2");
                   
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                else if (url == "rpt_CommodityDetails_CSPS")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "year";
                    reportParameterCollection[0].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add("2");
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "month";
                    reportParameterCollection[2].Values.Add("0");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                else if (url == "rpt_CommodityDetails_mfd")
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

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "year";
                    reportParameterCollection[0].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add("2");
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "month";
                    reportParameterCollection[2].Values.Add("0");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                else if (url == "rpt_MPWLC_C_U")
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
                    reportParameterCollection[2].Values.Add("1");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "rpt_MPWLC_C_U_Mfd")
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
                    reportParameterCollection[2].Values.Add("1");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }
                else if (url == "rpt_MPWLC_C_U_CSPS")
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
                    reportParameterCollection[0].Name = "Region_Id";
                    reportParameterCollection[0].Values.Add("1");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add("2");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "month";
                    reportParameterCollection[3].Values.Add("0");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                else if (url == "rpt_MPWLC_HQ01")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("7");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add(DateTime.Now.Year.ToString());

                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add("2");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "rpt_MPWLC_HQ01_CSPS")
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
                    reportParameterCollection[0].Name = "year";
                    reportParameterCollection[0].Values.Add(DateTime.Now.Year.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add("2");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                else if (url == "RegisteredOperator")
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

                }
                else if (url == "OperatorLoginReport_issue")
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
                }
                else if (url == "RegisteredOperatorDM")
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

                }

                else if (url == "rpt_GodownwiseStackPosition")
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
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotId";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Region.ServerReport.Refresh();
                }
                else if (url == "rptGodownwiseStackPosition_district")
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
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                   
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Region.ServerReport.Refresh();
                }
                else if (url == "rpt_Pending_gatepassdetails")
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
                    reportParameterCollection[0].Name = "District_ID";
                    reportParameterCollection[0].Values.Add(District.Text);

                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Region.ServerReport.Refresh();
                }
                else if (url == "rpt_MPWLC_C_U_Districtwise")
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
                    reportParameterCollection[2].Name = "district_Id";
                    reportParameterCollection[2].Values.Add("0");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                else if (url == "rpt_CommodityDetails_DistrictWise")
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
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add("2");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "district_Id";
                    reportParameterCollection[3].Values.Add("0");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                else if (url == "rpt_MPWLC_HQ01_DistrictWise")
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
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add("2");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "district_id";
                    reportParameterCollection[3].Values.Add("0");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);


                }

                else if (url == "rpt_MPWLC_C_U_CSPS_now")
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
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "month";
                    //reportParameterCollection[0].Values.Add("0");
                    //reportParameterCollection[1] = new ReportParameter();
                    //reportParameterCollection[1].Name = "year";
                    //reportParameterCollection[1].Values.Add("2011");
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Language";
                    reportParameterCollection[0].Values.Add("2");

                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Region_Id";
                    reportParameterCollection[1].Values.Add("0");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                else if (url == "ChangedPasswordReport")
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

                }

                else if (url == "rpt_MPWLC_C_U_CSPS_now_Scientific")
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
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "month";
                    //reportParameterCollection[0].Values.Add("0");
                    //reportParameterCollection[1] = new ReportParameter();
                    //reportParameterCollection[1].Name = "year";
                    //reportParameterCollection[1].Values.Add("2011");
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Language";
                    reportParameterCollection[0].Values.Add("2");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Region_Id";
                    reportParameterCollection[1].Values.Add("0");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                if (url == "Rpt_Whr_Detailed_Report")
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

                //if (url == "GodownStackedChart")
                //{

                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}

                if (url == "Rpt_Whr_Detailed_Report_MFD")
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

                if (url == "Rpt_Whr_Detailed_Report_Commodity")
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
                if (url == "Rpt_Whr_Detailed_Report_Commodity_MFD")
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


                if (url == "WHR_With_CropYear")
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

                if (url == "godownWiseCurrentStock")
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

                if (url == "PendingReciving")
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

                if (url == "CommodityWiseStateReport")
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
                if (url == "RegionWiseCommodityTotal")
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

                if (url == "GodownList_Region")
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
                if (url == "Crop_Commodity_wise_WHR_State")
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
                }
                if (url == "GodownCapacityDetails_Mfd")
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
                if (url == "commodityWiseCropyrlygodownsttus")
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
                if (url == "commodityWiseCropyrlygodownsttus_MFd")
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
                if (url == "PendingStatus")
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

                if (url == "ProcurmentReport15")
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

                if (url == "StoragetypeWiseCapacity")
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

                if (url == "WHRwithmenualrecord")
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

                if (url == "GodowntypewisetotalSTATE")
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
                if (url == "Godownwisewhrstaterpt")
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
                if (url == "Godowntypewiseprocdtl2015")
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
                
                if (url == "Godown_Utilization_Status")
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

                if (url == "DeleteResetDetail")
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
                if (url == "GodownCapacityDetailsState")
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
                if (url == "Godowntypewiseprocdtl2015own")
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
                if (url == "Master_Bill_Report")
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

                if (url == "GodowntypewisePaddyprocdtl15own")
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

                if (url == "WHRwithmenualrecordPaddy15")
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

                if (url == "TotalRecDelCropyrCommGodw")
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

                if (url == "paddyProcmfd_state1516")
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


                else if (url == "DaywiseRecvDelState")
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
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("1");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add("2012");

                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                else if (url == "CapacityNUtilNew")
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

                else if (url == "GodownutilGraph")
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

                else if (url == "DepositorWiseGraph")
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

                else if (url == "DailytransactionState")
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
                    reportParameterCollection[0].Name = "date";
                    reportParameterCollection[0].Values.Add("20160101");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }

                else if (url == "GodownWise_totalCommodity")
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
                    reportParameterCollection[0].Name = "date";
                    reportParameterCollection[0].Values.Add("20160101");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }

                else if (url == "BhugtanRashiTtlRegionWise")
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

                }

                else if (url == "Bhugtanrashidatewise")
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
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add("1");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "date";
                    reportParameterCollection[1].Values.Add("2016/01/01");
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "Godowntypewiseprocdtl2016own")
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
                if (url == "TotalWheatWHR2016-17")
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

                if (url == "rpt_StateGodownList")
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
                if (url == "PvtWTotalWheatWHR2016-17")
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

                if (url == "rpt_OnionStorageDetail")
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
                if (url == "CommodityLossDuringStorage_Manual")
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
                if (url == "Owned_Godown_Capacity_And_Utillazation")
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
                if (url == "SteelSilo_Godown_CPT_and_UTL")
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
                if (url == "WDRA_Godown_CPT_and_UTL")
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
                if (url == "PVT_PEG_Godown_CPT_and_UTL")
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
                if (url == "JVS_Godown_CPT_and_UTL")
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
                if (url == "Effective_rate_list")
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
                if (url == "CommodityLossStateSummary_Manual")
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
                if (url == "rpt_oldStock_before2015_16_Details")
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
                if (url == "CategoryWise_Current_Stock")
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
                if (url == "rpt_Branch_LastOperation_Details")
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
                if (url == "rpt_IssueCenterWiseBranch_Detail")
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
                if (url == "rpt_None_MPWLCBranch_Detail")
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
                if (url == "Diff_BW_WHRIssue_And_EntryDate")
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
                if (url == "rpt_RegionWise_GodownTypeWise_Proc_wheat_Stock")
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
                if (url == "Diff_BW_GatePassIssue_And_EntryDate")
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
                if (url == "rpt_SummaryOfLatiLongti")
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
                if (url == "rpt_Quality_Control_Report")
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

                if (url == "GodownList_of_unutiliza_tilldate")
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

                if (url == "BranchWiseGodownCount")
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
                if (url == "StateOpeningClosing_Rpt")
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
                if (url == "KharifProc2016-17")
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
                if (url == "State_GodownWiseCurrentStock")
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
                if (url == "KharifProc2016_17_AllComm")
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
                if (url == "Paddy_KharifProc2016_17")
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
                if (url == "CourseGrain_KharifProc2016_17")
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
                //if (url == "State_CropYearWise_MPSCSC_Stock")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}
                //if (url == "State_CommodityWise_CropYearWise_MPSCSC_Stock")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}
                if (url == "WheatPSS_Gain_StateSummary_Manual")
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
                //if (url == "MPSCSC_Rice_StockReport")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}

                //if (url == "State_WheatPSS_Stock")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}
                if (url == "KharifProc2016-17_GodownWise")
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
                //if (url == "MPSCSC_WheatReportTillDate")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}
                //if (url == "MPSCSC_RiceReportTillDate")
                //{
                //    ReportViewer_Region.ServerReport.Refresh();
                //    string reportURL = "";
                //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                //    ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //    ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(reportURL);
                //    ReportViewer_Region.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Region.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Region.ShowCredentialPrompts = false;
                //}
                //end for state reports

                if (url == "State_CommodityStock_GraphRepresentation")
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
                if (url == "Pvt_Godown_List")
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
                if (url == "Wheat_Procurement_2017_18")
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
                if (url == "WheatProc201718_GodownWise_HiredTypeWise")
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

                if (url == "WheatProc1718_districtwise")
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

                if (url == "State_wheatpss_1718_RemainingDF_For_WHR")
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
                if (url == "rpt_Arhar_Tuar_StorageDetail")
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
                if (url == "rpt_Urad_StorageDetail")
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
                if (url == "Arhar_Procurement_2017_18")
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
                if (url == "Onion_Procurement_2017_18")
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
                if (url == "rpt_StateGodownListwithOrg")
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
                if (url == "Pulses_Procurement_2017_18")
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
                if (url == "PremisesWise_GodownList")
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

                if (url == "State_GdwnWiseLossGainDtl")
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
                if (url == "PendingPremisesWise_GodownList")
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
                if (url == "PremisesWise_GodownComparision")
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
                if (url == "State_StorageCargesBillDetails")
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
                if (url == "State_Wheat2017_GdwnDepositePreority")
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
                if (url == "BS_CommodityWiseStockDepositSummary")
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

                if (url == "State_GodownOwner_Details")
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

                if (url == "State_StorageCargesBillDetails_BMAprove")
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
                if (url == "KharifProc2017_18_CommodityWiseF")
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
                if (url == "KharifPaddyProc2017_18")
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
                if (url == "BS_WHRWiseStockDepositDtl")
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
                if (url == "State_MonthWiseCreatedBillDetails")
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
                if (url == "State_WHRCpt_againt_AggrementCpt")
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
                if (url == "State_BranchAddtionalDeatils")
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
                if (url == "State_AgrementJVGodownDetails")
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
                if (url == "PaddyProc1718_Without_CWCFCIMFDG")
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
                if (url == "PaddyProc1718_CWCFCIMFDG")
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
                if (url == "Wheat_WHRCpt_againt_AggrementCpt")
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
                if (url == "State_DistrictWiseVacantCpt")
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
                if (url == "StateWise_ProcurementCenter_GodownMappingWheat2018_2019")
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

                if (url == "Wheat_Procurement_2018_19")
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
                if (url == "State_WHRCPT_Againt_JVSAgreementCPTNew")
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
                if (url == "State_WheatPSS_1819_RemainingDF_For_WHR")
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
                if (url == "rpt_DailyStockDepositeEntry2018")
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
                if (url == "CSM_Procurement_2018_19")
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
                if (url == "Sarso_Procurement_2018_19")
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
                if (url == "Masur_Procurement_2018_19")
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
                if (url == "WheatProc1819_Without_CWCFCIMFDG")
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
                if (url == "WheatProc1819_CWCFCIMFDG")
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
                if (url == "State_WheatPSS_1819_DiffHour")
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
                if (url == "State_DisttWiseCMS_rpt")
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
                if (url == "State_ProvDF_FinalDF_WHR")
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

                if (url == "State_WheatPSS_1819_Diff100Hour")
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
                if (url == "Nafed_BranchWise_CMS_WHRDetails")
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
                if (url == "State_RegionWiseDisttWiseCMS_rpt")
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
                if (url == "State_NafedCMS_StockReport")
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
                if (url == "State_RegionWise_HiredTypeWise_StockRpt")
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
                if (url == "WheatProc1819_Without_CWCFCIMFDG_DistWise")
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
                if (url == "CMS_Proc1819_Without_CWCFCIMFDG_DistWise")
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
                if (url == "CMS_Proc1819_CWCFCIMFDG_DistWise")
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
                if (url == "State_Wheat2018_GdwnDepositePreority")
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
                if (url == "rpt_Godown_Village_Mapping")
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
                if (url == "CMS_Proc1819_Comparative_CMS_WHR")
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
                if (url == "State_DeletedWHR_Details")
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
                if (url == "State_QCDeletedReport")
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
                if (url == "State_NonFAQ_CMS_SocietyWisWHR")
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
                if (url == "State_WHRDetails_Proc1819")
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
                if (url == "State_DeletedWHR_WithReqDate_DeletedDate")
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
                if (url == "State_GodownWise_DFandWHRDetails_Proc1819")
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
                if (url == "Commodity_Crop_Wise_MPSCSC_Stock")
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
                if (url == "rpt_State_New_Godown_Summary")
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
                if (url == "rpt_KrayaParisar_Godown_Report")
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
                if (url == "rpt_GreaterCpt_Godown_Report")
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
                if (url == "HiredTypeWiseCpt_VerifyGdwnRpt")
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
                if (url == "Kharif_Procurement_2018_19")
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
                if (url == "State_VerifyGDWN_CPT_LicDetails")
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
                if (url == "CoarseGrain_Procurement_2018_19")
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
                if (url == "Paddy_Procurement_2018_19")
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
                // nafed Login report
                if (url == "Nafed_BranchWise_DalhanTilhan_WHRDetails")
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

                if (url == "State_PaddyStorageInCapCapacity_2018")
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

                if (url == "State_Proc2018_19_RejectionWHR")
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

                if (url == "State_VerifyGdwnCPT_VacantCPT")
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

                if (url == "State_DistrictWiseGdwnVacantCPT_Closing")
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

                if (url == "State_NoOfGdwn_GdwnCPT_VacantCPT")
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
                if (url == "Wheat_Procurement_2019_20")
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
                if (url == "State_DSC_UploaderDetails")
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
                if (url == "State_CSM_Proc201920_District_Wise")
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

                if (url == "State_HiredTypeWise_CPT_UTL")
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

                if (url == "Markfed_WHRDetail_Proc201819")
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

                if (url == "State_Dashboard_GdwnWiseAsOnDate")
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
                    reportParameterCollection[0].Name = "DistrictID";
                    reportParameterCollection[0].Values.Add(Session["DashBoardDistID"].ToString());
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);
                }


                if (url == "State_StorageChrg_GnrtBillSummary2019_MPWLCGdwn")
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


                if (url == "State_StorageChrg_GnrtBillSummary2019_Other")
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
                if (url == "State_GRent_PassingOrder_Detail")
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
                if (url == "State_Bill_Payment_Status")
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
                if (url == "StorageBill_ETE_Billing_Status")
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
                if (url == "CMS_Procurement_2020_21")
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
                if (url == "Payment_BillsFromCSMStoMPWLC_Status_Rept")
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
