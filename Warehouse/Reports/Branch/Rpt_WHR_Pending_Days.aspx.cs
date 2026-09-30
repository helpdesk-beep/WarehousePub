using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

public partial class Reports_Branch_Rpt_WHR_Pending_Days : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string uname = "";
    string pwd = "";
    string domain = "";
    string reportURL = "";
    string folder = "";
    string Language = "";
    string serverFullName = null;
    string Depot;
    protected void Page_Load(object sender, EventArgs e)
    {
        //string st = Session["Region_ID"].ToString();
        //Rgnid.Text = st;
        //Session["reporturl"] = "";
        //Session["reporturl"] = "Rpt_WHR_Pending_Days";
        Depot = Session["BranchId"].ToString();

        ReportViewer_WHRPendingDays.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        try
        {
            if (!IsPostBack)
            {
               
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

                ReportViewer_WHRPendingDays.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_WHRPendingDays.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_WHRPendingDays.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_WHRPendingDays.ServerReport.ReportServerUrl = new Uri(reportURL);

                try
                {
                    ReportViewer_WHRPendingDays.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_WHRPendingDays.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_WHRPendingDays.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Branch_Id";
                    reportParameterCollection[0].Values.Add(Depot.ToString());

                    ReportViewer_WHRPendingDays.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_WHRPendingDays.ServerReport.Refresh();
                }
                catch (Exception ex)
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
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
      
    //protected void btn_Show_Click(object sender, EventArgs e)
    //{
    //    string path = Request.Url.ToString();
    //    int index = path.IndexOf(":") + 3;
    //    string path2 = path.Substring(index);
    //    int index2 = path2.IndexOf("/");
    //    int index3 = path2.IndexOf("/", index2 + 1);

    //    string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
    //    //string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

    //    if (index3 > 0)

    //        serverFullName = path2.Substring(0, index3);
    //    else
    //        serverFullName = servername;

    //    ReportViewer_WHRPendingDays.ServerReport.ReportServerUrl = new Uri(servername.ToString());
    //    ReportViewer_WHRPendingDays.Visible = true;
    //    uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
    //    pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
    //    domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
    //    reportURL = "";
    //    folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
    //    reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
    //    ReportViewer_WHRPendingDays.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
    //    ReportViewer_WHRPendingDays.ServerReport.ReportServerUrl = new Uri(reportURL);

    //    try
    //    {
    //        ReportViewer_WHRPendingDays.ServerReport.ReportPath = folder + "/" + "Rpt_WHR_Pending_Days";
    //        ReportViewer_WHRPendingDays.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
    //        ReportViewer_WHRPendingDays.ShowCredentialPrompts = false;
    //        ReportParameter[] reportParameterCollection = new ReportParameter[1];
    //        reportParameterCollection[0] = new ReportParameter();
    //        reportParameterCollection[0].Name = "Branch_ID";
    //        reportParameterCollection[0].Values.Add(Depot.ToString());

    //        ReportViewer_WHRPendingDays.ServerReport.SetParameters(reportParameterCollection);
    //        ReportViewer_WHRPendingDays.ServerReport.Refresh();
    //    }
    //    catch (Exception ex)
    //    {
    //        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
    //    }

    //}

}