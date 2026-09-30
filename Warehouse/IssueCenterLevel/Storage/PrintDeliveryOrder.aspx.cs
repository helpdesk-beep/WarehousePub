using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

public partial class IssueCenterLevel_Storage_PrintDeliveryOrder : System.Web.UI.Page
{
    string dist = "";
    string Depot = "";
    string url = "";
   // string DO = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         ReportViewer_Region.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        // string url = "Rpt_Delivery_Order_New";
         if (Session["BranchType"].ToString() == "G")
         {
             url = "RptPvt_DeliveryForm";
         }
         else
         {
             url = "RPT_DeliveryForm";
         }
         
        if (!IsPostBack)
        {
            try
            {
            //  string  DO = Request.QueryString["do"];
                string DO = Session["DelOr"].ToString();
                String Language;
                string folder = "";
                if (url == "RPT_DeliveryForm")
                {
                    //**Dynamic later
                    if (int.Parse(Session["RoleId"].ToString()) == 1 && Session["Depot_DepotID"].ToString() == "2303007")
                    {
                        if (int.Parse(Session["RoleId"].ToString()) == 1)
                        {
                            dist = Session["Depot_DistID"].ToString();
                            Depot = Session["BranchId"].ToString();
                        }
                    }
                    else
                    {
                        if (int.Parse(Session["RoleId"].ToString()) == 1)
                        {
                            dist = Session["Depot_DistID"].ToString();
                            Depot = Session["Depot_DepotID"].ToString();
                        }
                        else
                        {
                            dist = "2301";
                            Depot = "2301001";
                        }
                    }
                }
                else if (url == "RptPvt_DeliveryForm")
                {
                    dist = Session["Depot_DistID"].ToString();
                    Depot = Session["G_BranchId"].ToString();
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

                ReportViewer_Region.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL

                ReportViewer_Region.Visible = true;


                string uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                string pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                string domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();

                //for Print DO
                if (url == "RPT_DeliveryForm")
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
                    reportParameterCollection[0].Name = "District";
                    reportParameterCollection[0].Values.Add(dist);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depot";
                    reportParameterCollection[1].Values.Add(Depot);
                    
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "DO";
                    reportParameterCollection[2].Values.Add(DO);
                    
                    ReportViewer_Region.ServerReport.SetParameters(reportParameterCollection);

                }
                if (url == "RptPvt_DeliveryForm")
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
                    reportParameterCollection[0].Name = "District";
                    reportParameterCollection[0].Values.Add(dist);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchId";
                    reportParameterCollection[1].Values.Add(Depot);

                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "DO";
                    reportParameterCollection[2].Values.Add(DO);

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