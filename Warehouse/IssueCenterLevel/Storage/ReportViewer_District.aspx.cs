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



public partial class IssueCenterLevel_Storage_ReportViewer_District : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       

        ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        //Response.Write(url); Response.End();

        if (!IsPostBack)
        {
            try
            {

                String Language;
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
                //string servername = path2.Substring(0, index2);
                string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                // servername = "localhost/reportserver"; //to be changed..dynamic..

                ReportViewer_District.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
                //ReportViewer_District.ServerReport.ReportPath = "/" + ConfigurationManager.AppSettings["AppReportsPath"].ToString() + "/" + url; // Report Name
                ReportViewer_District.Visible = true;
                //changed on 29 june
                //Response.Redirect("Http://" + servername.ToString() + "/ReportServer/Pages/ReportViewer.aspx?/" + ConfigurationManager.AppSettings["AppReportsPath"].ToString() + "/" + url.ToString() + "&rs:Command=Render");

                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                if (url == "rpt_CommodityDetails_regionwise")
                {

                    ReportViewer_District.ServerReport.Refresh();
                    string reportURL = "";
                    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_District.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_District.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_District.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_District.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();


                    //for parmeter passing
                    ReportViewer_District.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[4];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("0");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add("2011");
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "region_id";
                    reportParameterCollection[2].Values.Add("1");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    ReportViewer_District.ServerReport.SetParameters(reportParameterCollection);


                }

                else if (url == "rpt_MPWLC_C_U")
                {
                    ReportViewer_District.ServerReport.Refresh();

                    string reportURL = "";
                    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_District.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_District.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_District.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_District.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();



                    ReportViewer_District.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[4];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region_Id";
                    reportParameterCollection[0].Values.Add("1");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "month";
                    reportParameterCollection[1].Values.Add("0");
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "year";
                    reportParameterCollection[2].Values.Add("2011");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    ReportViewer_District.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "rpt_MPWLC_HQ02")
                {
                    ReportViewer_District.ServerReport.Refresh();

                    string reportURL = "";
                    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_District.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_District.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_District.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_District.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();



                    ReportViewer_District.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[5];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "month";
                    reportParameterCollection[0].Values.Add("0");
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "year";
                    reportParameterCollection[1].Values.Add("2011");
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Region_Id";
                    reportParameterCollection[2].Values.Add("1");
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "Language";
                    reportParameterCollection[3].Values.Add("2");

                    reportParameterCollection[4] = new ReportParameter();
                    reportParameterCollection[4].Name = "DepoBelongs";
                    reportParameterCollection[4].Values.Add("FCI");

                    ReportViewer_District.ServerReport.SetParameters(reportParameterCollection);
                }
                else if (url == "rpt_MPWLC_C_U_CSPS_now")
                {

                    ReportViewer_District.ServerReport.Refresh();
                    string reportURL = "";
                    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_District.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_District.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_District.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_District.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();


                    ReportViewer_District.ShowCredentialPrompts = false;

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
                    reportParameterCollection[1].Values.Add(Session["Region_RegionID"].ToString());

                    ReportViewer_District.ServerReport.SetParameters(reportParameterCollection);


                }

                else if (url == "rpt_MPWLC_C_U_CSPS_now_Scientific")
                {

                    ReportViewer_District.ServerReport.Refresh();
                    string reportURL = "";
                    string folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                    ReportViewer_District.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer_District.ServerReport.ReportServerUrl = new Uri(reportURL);
                    ReportViewer_District.ServerReport.ReportPath = folder + "/" + url;
                    //ReportViewer_District.ServerReport.ReportServerCredentials = New MyReportServerCredentials
                    ReportViewer_District.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();


                    ReportViewer_District.ShowCredentialPrompts = false;

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
                    reportParameterCollection[1].Values.Add(Session["Region_RegionID"].ToString());

                    ReportViewer_District.ServerReport.SetParameters(reportParameterCollection);


                }
            }

            catch (Exception ex)
            {
                Label1.Text = Label1.Text + ex.Message.ToString();
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
