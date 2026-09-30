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



public partial class Reports_Region_Region_CropYear_ReportViewer : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string qry = "";
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                fillCropYear();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }

    }

    private void fillCropYear()
    {
        try
        {
            string query = "select 'All' as CropText , '2%' as CropYear union (select distinct CropYear , CropYear+'%' as CropYear  from tbl_storage_Depositor_WHR_Relation where CropYear like ('2%')) order by CropYear";
            SqlCommand cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropyear.Items.Clear();
                ddlcropyear.DataSource = ds.Tables[0];
                ddlcropyear.DataTextField = "CropText";
                ddlcropyear.DataValueField = "CropYear";
                ddlcropyear.DataBind();
                ddlcropyear.Items.Insert(0, "---Select---");
            }
            else
            {

            }
        }
        catch (Exception)
        {
        }
    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uname = "";
        string pwd = "";
        string domain = "";
        string reportURL = "";
        string folder = "";
        string serverFullName = null;
        Region_CropYearViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
        Region_CropYearViewer.ServerReport.ReportServerUrl = new Uri(servername.ToString()); // Report Server URL
        Region_CropYearViewer.Visible = true;
        uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
        pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
        domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            if (url == "Rpt_RegionWise_CropWise_WHR")
            {
                Region_CropYearViewer.ServerReport.Refresh();
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                Region_CropYearViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                Region_CropYearViewer.ServerReport.ReportServerUrl = new Uri(reportURL);
                Region_CropYearViewer.ServerReport.ReportPath = folder + "/" + url;
                Region_CropYearViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                Region_CropYearViewer.ServerReport.Timeout = 1800000;
                Region_CropYearViewer.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "RegionID";
                reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "CropYear";
                reportParameterCollection[1].Values.Add(ddlcropyear.SelectedValue.ToString());
                Region_CropYearViewer.ServerReport.SetParameters(reportParameterCollection);
            }

            if (url == "Region_BranchWiseStockReport")
            {
                Region_CropYearViewer.ServerReport.Refresh();
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                Region_CropYearViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                Region_CropYearViewer.ServerReport.ReportServerUrl = new Uri(reportURL);
                Region_CropYearViewer.ServerReport.ReportPath = folder + "/" + url;
                Region_CropYearViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                Region_CropYearViewer.ServerReport.Timeout = 1800000;
                Region_CropYearViewer.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "RegionID";
                reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "CropYear";
                reportParameterCollection[1].Values.Add(ddlcropyear.SelectedValue.ToString());
                Region_CropYearViewer.ServerReport.SetParameters(reportParameterCollection);

            }
            if (url == "Region_GodownWiseCurrentStock")
            {
                Region_CropYearViewer.ServerReport.Refresh();
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                Region_CropYearViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                Region_CropYearViewer.ServerReport.ReportServerUrl = new Uri(reportURL);
                Region_CropYearViewer.ServerReport.ReportPath = folder + "/" + url;
                Region_CropYearViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                Region_CropYearViewer.ServerReport.Timeout = 1800000;
                Region_CropYearViewer.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "RegionID";
                reportParameterCollection[0].Values.Add(Session["Region_ID"].ToString());
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "CropYear";
                reportParameterCollection[1].Values.Add(ddlcropyear.SelectedValue.ToString());
                Region_CropYearViewer.ServerReport.SetParameters(reportParameterCollection);
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
