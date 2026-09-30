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

public partial class IssueCenterLevel_Storage_ReportViewer_Depot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportViewer_Depot.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string Language = "";
            string serverFullName = null;
            if (!IsPostBack)
            {
                if (int.Parse(Session["RoleId"].ToString()) == 1)
                {
                    District.Text = Session["Depot_DistID"].ToString();
                    Depot.Text = Session["BranchId"].ToString();
                }
                else
                {
                    District.Text = "2301";
                    Depot.Text = "2301001";
                }
                //if ( Session["lang"] != null || Session["lang"].ToString() == "Hindi")
                //    Language = "1";
                //else
                //    Language = "2";
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
              
                ReportViewer_Depot.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_Depot.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Depot.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_Depot.ServerReport.ReportServerUrl = new Uri(reportURL);

                if (url == "rptDepositorLedger")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                  
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DepotId";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDetails_of_Daily_Issue_Commoditywise_ForAllCommodity")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStackwiseRegister_ForAllStack")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "DistrictId";
                    //reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DepotId";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    //reportParameterCollection[2] = new ReportParameter();
                    //reportParameterCollection[2].Name = "Language";
                    //reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStockRegister_GodownNCommodity")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "DistrictId";
                    //reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch_ID";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    //reportParameterCollection[2] = new ReportParameter();
                    //reportParameterCollection[2].Name = "Language";
                    //reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "RptDailyReceiptAndReleaseRegister_Report")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictName";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Warehouse";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDeliveryOrder")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depot";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptAcknowledgement")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotID";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptWarehouseReceipt")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depot";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStackWiseConditionReportRegister")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District_id";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotId";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rpt_gatepassdetails")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depot";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                   
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptTransactions")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depoid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptSchemeWiseOutflow")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotID";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStackwiseRegister")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
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
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStockRegister")
                {
                    
                    ReportViewer_Depot.ShowPrintButton = true;
                    
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "DistrictId";
                    //reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "depotid";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Language";
                    reportParameterCollection[1].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDaily_Commodity_Receipt_Details")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "districtId";
                    //reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DepotId";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    //reportParameterCollection[2] = new ReportParameter();
                    //reportParameterCollection[2].Name = "Language";
                    //reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDO_Wise_Issue_Details")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptStockValuationRegister")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "DistrictId";
                    //reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    //reportParameterCollection[2] = new ReportParameter();
                    //reportParameterCollection[2].Name = "Language";
                    //reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptTruckChallan_SendingDetails")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotId";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rpt_GodownwiseStackPosition")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                   ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
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
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDetails_of_Daily_Issue_Commoditywise")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rpt_WHRwise_LossGain")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                
                }
                else if (url == "rpt_Godownwise_stackWise_WHR")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "districtId";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();

                }

                else if (url == "GapassDetailReciverWise")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "District_Id";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "DepotId"; 
                    reportParameterCollection[1].Values.Add(Depot.Text);
                   
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();

                }
                else if (url == "GodownWiseRecort")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DepotId";
                    reportParameterCollection[0].Values.Add(Depot.Text);

                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();

                }
                else if (url == "ReceivingDetails")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Districtid";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depotid";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "GodownWisewhrdetailbwdates")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());

                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "DepositerwiseWhrBwDates")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());

                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "DepositorWiseReportTillMonth")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "DepositorWiseGodownDetail")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "rptStockRegister")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[0];
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "WhrFromDepositForm")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "MpwlctotalAllwhr")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depotid";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "GodownList")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "SocietyWiseWHR15")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Session["BranchID"].ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                //else if (url == "rpt_GodownwiseStackPosition")
                //{
                //    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                //    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Depot.ShowCredentialPrompts = false;
                //    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                //    reportParameterCollection[0] = new ReportParameter();
                //    reportParameterCollection[0].Name = "DistrictId";
                //    reportParameterCollection[0].Values.Add(District.Text);
                //    reportParameterCollection[1] = new ReportParameter();
                //    reportParameterCollection[1].Name = "DepotId";
                //    reportParameterCollection[1].Values.Add(Depot.Text);
                //    reportParameterCollection[2] = new ReportParameter();
                //    reportParameterCollection[2].Name = "Language";
                //    reportParameterCollection[2].Values.Add(Language.ToString());
                //    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                //    ReportViewer_Depot.ServerReport.Refresh();
                //}
                else if (url == "Rptwhrdetailsnew")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Rptwhrdetailsnew";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depot";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "Rpt_Receiptdetails")
                {
                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Rpt_Receiptdetails";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depotid";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Districtid";
                    reportParameterCollection[1].Values.Add(District.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rpt_whrDetail_godownwise_new13")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rpt_whrDetail_godownwise_new13";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depotid";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "RptDailyReceiptAndReleaseRegister_Report")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "RptDailyReceiptAndReleaseRegister_Report";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictName";
                    reportParameterCollection[0].Values.Add(District.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Warehouse";
                    reportParameterCollection[1].Values.Add(Depot.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "rptDO_Wise_Issue_Details2")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rptDO_Wise_Issue_Details2";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depot";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "districtId";
                    reportParameterCollection[1].Values.Add(District.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "totalrecivingdetailbranch")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "totalrecivingdetailbranch";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);                   
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "CommodityWiseWHRBranch")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "CommodityWiseWHRBranch";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchID";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "DepositerWiseWHRRecord")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "DepositerWiseWHRRecord";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "DepositerLezer")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "DepositerLezer";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                //else if (url == "WHRandDepositerformdetails")
                //{

                //    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "WHRandDepositerformdetails";
                //    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                //    ReportViewer_Depot.ShowCredentialPrompts = false;
                //    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                //    reportParameterCollection[0] = new ReportParameter();
                //    reportParameterCollection[0].Name = "Branch";
                //    reportParameterCollection[0].Values.Add(Depot.Text);
                //    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                //    ReportViewer_Depot.ServerReport.Refresh();
                //}
                else if (url == "WHRPrint")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "WHRPrint";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Reqid";
                    string rid = Session["ComReq_Id"].ToString();
                    reportParameterCollection[1].Values.Add(rid);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "BranchWiseProcDtl")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchWiseProcDtl";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "BranchWiseProcDtl2016")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchWiseProcDtl2016";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "DOTOwiseGatepass")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "DOTOwiseGatepass";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                else if (url == "WHRKillReport")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "WHRKillReport";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "MenualWHHRDetailBranch")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "MenualWHHRDetailBranch";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "Reservation_Register")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Reservation_Register";
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "BranchId";
                    reportParameterCollection[0].Values.Add(Depot.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                else if (url == "PendingDOTOBranch")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "PendingDOTOBranch";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "Dist";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('"+ex.Message+"'); </script> ");
                    }
                }
                else if (url == "rptStackwiseRegister")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rptStackwiseRegister";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "DepotId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "DistrictId";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Godown_Register")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Godown_Register";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);                    
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "GodownListBranch")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "GodownListBranch";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "GodowntypewisetotalBranch")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "GodowntypewisetotalBranch";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }


                else if (url == "CommoditywiseOpeningClosing")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "CommoditywiseOpeningClosing";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[4];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "datefrom";
                        reportParameterCollection[1].Values.Add(DateTime.Now.ToShortDateString());
                        reportParameterCollection[2] = new ReportParameter();
                        reportParameterCollection[2].Name = "dateto";
                        reportParameterCollection[2].Values.Add(DateTime.Now.ToShortDateString());
                        reportParameterCollection[3] = new ReportParameter();
                        reportParameterCollection[3].Name = "comm";
                        reportParameterCollection[3].Values.Add("22");
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "CommodityWiseGodownWiseTotalBranch")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "CommodityWiseGodownWiseTotalBranch";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_CropYear_Wise_WHR")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CropYear_Wise_WHR";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);                    
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Mapped_Godown_Details")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Mapped_Godown_Details";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch_ld";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "BranchId";
                        reportParameterCollection[1].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "rpt_BranchCommodityLoss_Manual")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rpt_BranchCommodityLoss_Manual";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "DistrictId";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Godown_List_For_Updation")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Godown_List_For_Updation";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_SteelSilo_Godown_CPT_and_UTL")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_SteelSilo_Godown_CPT_and_UTL";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_JVS_Godown_CPT_and_UTL")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_JVS_Godown_CPT_and_UTL";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_PVT_PEG_Godown_CPT_and_UTL")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_PVT_PEG_Godown_CPT_and_UTL";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_WDRA_Godown_CPT_and_UTL")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_WDRA_Godown_CPT_and_UTL";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Owned_Godown_CPT_And_UTL")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Owned_Godown_CPT_And_UTL";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BranchGodownReceiveIssue_BWDates")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchGodownReceiveIssue_BWDates";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_CommodityWiseWHR_Report")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CommodityWiseWHR_Report";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GodownWiseCommoditySummary")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GodownWiseCommoditySummary";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "rpt_PendingPaddyDO_Detail")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rpt_PendingPaddyDO_Detail";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[3];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "District";
                        reportParameterCollection[1].Values.Add((District.Text).Substring(2,2));
                        reportParameterCollection[2] = new ReportParameter();
                        reportParameterCollection[2].Name = "Depot";
                        reportParameterCollection[2].Values.Add(Session["Depot_DepotID"].ToString());
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GodownStockHandover")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GodownStockHandover";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);                    
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_OpeningClosing_CommodityWise_BTWDate")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_OpeningClosing_CommodityWise_BTWDate";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BRANCHID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_KharifProc2016_17")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_KharifProc2016_17";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "rpt_Branch_WheatPSS_Gain_Manual")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rpt_Branch_WheatPSS_Gain_Manual";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "DistrictId";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {

                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_MPSCSC_Stock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_MPSCSC_Stock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_DepositorWise_commodityWise_Stock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_DepositorWise_commodityWise_Stock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_HiredTypeWise_GodownWise_MPSCSC_stock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_HiredTypeWise_GodownWise_MPSCSC_stock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_CommodityWise_GwdnWise_MPSCSCstock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CommodityWise_GwdnWise_MPSCSCstock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "BranchStock_ChartRepresentation")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchStock_ChartRepresentation";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "BranchCommodityStock_GraphRepresentation")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchCommodityStock_GraphRepresentation";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BranchCpt_Utl_GraphRepresentation")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BranchCpt_Utl_GraphRepresentation";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Godown_Wise_Stack_Capacity")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Godown_Wise_Stack_Capacity";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_TC_ChallanWsie_ReceiveDetail")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_TC_ChallanWsie_ReceiveDetail";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "PendingDO_TO_From_DS")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "PendingDO_TO_From_DS";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "Dist";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "PendingDO_TO_From_OS")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "PendingDO_TO_From_OS";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[2];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        reportParameterCollection[1] = new ReportParameter();
                        reportParameterCollection[1].Name = "Dist";
                        reportParameterCollection[1].Values.Add(District.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }


                else if (url == "Branch_CropYearWise_GodownWise_Stock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CropYearWise_GodownWise_Stock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_ProcWheat_17_18")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_ProcWheat_17_18";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Wheatpss_1718_RemainingDF_For_WHR")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Wheatpss_1718_RemainingDF_For_WHR";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GodownAvailablelStock")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GodownAvailablelStock";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_GdwnWiseClosingBal_GivenDate")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GdwnWiseClosingBal_GivenDate";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_CmdtyWiseClosingBal_GivenDate")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CmdtyWiseClosingBal_GivenDate";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_PrmWise_GdwnComparision")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_PrmWise_GdwnComparision";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_KharifProc2017_18")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_KharifProc2017_18";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_CreatedBillDetails")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CreatedBillDetails";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_ProcWheat_18_19")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_ProcWheat_18_19";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_ProcGram_18_19")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_ProcGram_18_19";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Fill_FAQ_CMS_report")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Fill_FAQ_CMS_report";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "rpt_BranchNewVerifyGodownList")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rpt_BranchNewVerifyGodownList";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_VerifyGodownVacantCPT")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_VerifyGodownVacantCPT";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Kharif_Procurement_2018_19")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Kharif_Procurement_2018_19";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_CoarseGrain_Procurement_2018_19")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CoarseGrain_Procurement_2018_19";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_Paddy_Procurement_2018_19")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Paddy_Procurement_2018_19";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_Wheat_Procurement_2019_20")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_Wheat_Procurement_2019_20";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_DSC_UploaderDetails")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_DSC_UploaderDetails";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_NAFED_WHRPrint_CMS_Procurement_2019_20")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CMS_Proc201920";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GdwnWiseCmdWiseStackBal")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GdwnWiseCmdWiseStackBal";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "DepotId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GdwnWiseCmdWiseStockBalance")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GdwnWiseCmdWiseStockBalance";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "DepotId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "Branch_GdwnWiseWHRWiseStockBalance")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_GdwnWiseWHRWiseStockBalance";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "DepotId";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "Branch_CmdWise_CropYearWise_WHR")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "Branch_CmdWise_CropYearWise_WHR";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "SubRdl_GodownWiseBillStatus")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "SubRdl_GodownWiseBillStatus";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
				else if (url == "SubRdl_GodownWiseBillStatusFromAugust")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "SubRdl_GodownWiseBillStatusFromAugust";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_Bill_Payment_Status")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_Bill_Payment_Status";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_GRent_PassingOrder_Detail")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_GRent_PassingOrder_Detail";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "BranchID";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_Wheat_Procurement_2020_21")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_Wheat_Procurement_2020_21";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_WHR_Wise_CMS_Acceptance_2022-23")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_CMS_Acceptance_2022-23";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_CMS_Procurement_2020_21")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_CMS_Procurement_2020_21";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_WHR_Wise_CMS_Acceptance")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_CMS_Acceptance";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_WHR_Wise_Wheat_Acceptance")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_Wheat_Acceptance";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_WHR_Wise_Wheat_Acceptance_2021_22")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_Wheat_Acceptance_2021_22";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
                else if (url == "BO_WHR_Wise_Wheat_Acceptance_2022_23")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_Wheat_Acceptance_2022_23";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }

                else if (url == "BO_WHR_Wise_Wheat_Acceptance_2022_23_Paddy")
                {
                    try
                    {
                        ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "BO_WHR_Wise_Wheat_Acceptance_2022_23_Paddy";
                        ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                        ReportViewer_Depot.ShowCredentialPrompts = false;
                        ReportParameter[] reportParameterCollection = new ReportParameter[1];
                        reportParameterCollection[0] = new ReportParameter();
                        reportParameterCollection[0].Name = "Branch";
                        reportParameterCollection[0].Values.Add(Depot.Text);
                        ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                        ReportViewer_Depot.ServerReport.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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

