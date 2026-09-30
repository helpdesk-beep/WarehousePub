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

public partial class Reports_Branch_rptDaily_Commodity_Receipt_Details : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    string Language = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetTodaydate();

        }
    }
    private void GetTodaydate()
    {
        try
        {
            string qry = "select  convert(varchar(10), getdate(),103) as TodayDate";
            SqlCommand cmd = new SqlCommand(qry, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // txtfromdate.Text = ds.Tables[0].Rows[0]["TodayDate"].ToString();
                txttodate.Text = ds.Tables[0].Rows[0]["TodayDate"].ToString();

            }
        }
        catch (Exception)
        {
            //////
        }
    }

    protected void BtnViewReport_Click(object sender, EventArgs e)
    {
        ViewReport();
    }


    protected void ViewReport()
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

            if (url == "rptDaily_Commodity_Receipt_Details")
            {

                ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportViewer_Depot.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[4];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "districtId";
                reportParameterCollection[0].Values.Add(District.Text);
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "DepotId";
                reportParameterCollection[1].Values.Add(Depot.Text);
                reportParameterCollection[2] = new ReportParameter();
                reportParameterCollection[2].Name = "Language";
                reportParameterCollection[2].Values.Add(Language.ToString());
               
                reportParameterCollection[3] = new ReportParameter();
                reportParameterCollection[3].Name = "Pardate";
                reportParameterCollection[3].Values.Add(txttodate.Text);
                ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                ReportViewer_Depot.ServerReport.Refresh();
            }


        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

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