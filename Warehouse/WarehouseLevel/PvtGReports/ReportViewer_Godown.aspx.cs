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

public partial class WarehouseLevel_PvtGReports_ReportViewer_Godown : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BranchId = "";
        string GodownId = "";
        ReportViewer_Godown.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
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
                if (Session["G_DepotID"].ToString() != "" && Session["GodownID_New"].ToString() != null)
                {
                    BranchId = Session["G_BranchId"].ToString();
                    GodownId = Session["GodownID_New"].ToString();
                }

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

                ReportViewer_Godown.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_Godown.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Godown.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_Godown.ServerReport.ReportServerUrl = new Uri(reportURL);

                if (url == "Pvt_whrDetail_godownwise")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depotid";
                    reportParameterCollection[0].Values.Add(BranchId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Depot";
                    reportParameterCollection[1].Values.Add(BranchId);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Godown_Id";
                    reportParameterCollection[2].Values.Add(GodownId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Pvt_Godown_WHR_Wise_CPT_UTL")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Godown_ID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "GodownWiseCommoditySummary")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Godown_Id";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchId";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "rptGodown_DO_Wise_Issue_Details")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Depot";
                    reportParameterCollection[0].Values.Add(BranchId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "districtId";
                    reportParameterCollection[1].Values.Add(Session["Depot_DistID"].ToString());
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Godown_OpeningClosing_CommodityWise_BTWDate")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownId";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BRANCHID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Godown_Wise_Stack_Capacity_Detail")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownId";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchId";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "OtherLogin_WheatProc1718")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }

                else if (url == "Pvt_Godown_WhrFromDepositForm")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Branch";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }

                else if (url == "Pvt_Godown_CommodityWiseWHRdetails")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Pvt_Godown_CropYearWise_WHRdetails")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }

                else if (url == "Pvt_Godown_StackStock")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BranchID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }

                else if (url == "PvtGodown_GatepassDetail_BetweenDates")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "BRANCHID";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Pvt_DateWise_WHRDetails")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Pvt_DateWise_WHRDetails")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Pvt_Delivery_Order_New")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "OtherLogin_Proc2018_19")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "GodownID";
                    reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "Branch";
                    reportParameterCollection[1].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
                }
                else if (url == "Branch_CropYearWise_GodownWise_Stock")
                {
                    ReportViewer_Godown.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Godown.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Godown.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "GodownID";
                    //reportParameterCollection[0].Values.Add(GodownId);
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch";
                    reportParameterCollection[0].Values.Add(BranchId);
                    ReportViewer_Godown.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Godown.ServerReport.Refresh();
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
