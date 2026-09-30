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
using System.Data.SqlClient;

public partial class StatePages_ReportViewer_VacStorage_Morethan_1_Lac_mt : System.Web.UI.Page
{
    string uname = "";
    string pwd = "";
    string domain = "";
    string reportURL = "";
    string folder = "";
    string Language = "";
    string serverFullName = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_VacStorage_Morethan_1_Lac_MetricTon_For_State";
        //Response.Redirect("ReportViewer_Branch_Vacant_Storage.aspx");

        ReportViewer_VacStorage_Morethan_1_Lac_mt.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        try
        {
            if (!IsPostBack)
            {


                if (int.Parse(Session["RoleId"].ToString()) == 1)
                {
                    District.Text = Session["Depot_DistID"].ToString();
                    Depot.Text = Session["BranchId"].ToString();

                    // Hiredtype.Text = Session["BranchId"].ToString();

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

                ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_VacStorage_Morethan_1_Lac_mt.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_VacStorage_Morethan_1_Lac_mt.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.ReportServerUrl = new Uri(reportURL);

                try
                {
                    ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.ReportPath = folder + "/" + "Godown_VacStorage_Morethan_1_Lac_MetricTon_For_State";
                    ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_VacStorage_Morethan_1_Lac_mt.ShowCredentialPrompts = false;
                    //ReportParameter[] reportParameterCollection = new ReportParameter[1];
                    //reportParameterCollection[0] = new ReportParameter();
                    //reportParameterCollection[0].Name = "BranchID";
                    //reportParameterCollection[0].Values.Add(Depot.Text);
                    //ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_VacStorage_Morethan_1_Lac_mt.ServerReport.Refresh();
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
}