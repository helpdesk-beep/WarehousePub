using System;
using System.Data;
using System.Data.SqlClient;
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

public partial class Reports_Branch_ReportViewer_Branch_New_Reports : System.Web.UI.Page
{

    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    string uname = "";
    string pwd = "";
    string domain = "";
    string reportURL = "";
    string folder = "";
    string Language = "";
    string serverFullName = null;


    public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Depot.Text = Session["BranchId"].ToString();
        Godown.Text = "";
        ReportViewer_Branch_New_Reports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
       
        try
        {

            if (!IsPostBack)
            {
                Get_Godown();
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

                ReportViewer_Branch_New_Reports.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_Branch_New_Reports.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Branch_New_Reports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_Branch_New_Reports.ServerReport.ReportServerUrl = new Uri(reportURL);
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



    void Get_Godown()
    {
        string qry;

        qry = "select (Godown_Name+'   ('+Godown_ID+')') as Godown,Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 where BranchID='" + Depot.Text.ToString() + "'";

        da = new SqlDataAdapter(qry, conn);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddl_Godown.DataSource = ds.Tables[0];
            ddl_Godown.DataTextField = "Godown";
            ddl_Godown.DataValueField = "Godown_ID";
            ddl_Godown.DataBind();
            ddl_Godown.Items.Insert(0, "--Select--");
            //ddl_dist.Items.Insert(0, new ListItem("Select ALL", "%"));
        }

    }

    protected void ddl_Godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReportViewer_Branch_New_Reports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
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

        ReportViewer_Branch_New_Reports.ServerReport.ReportServerUrl = new Uri(servername.ToString());
        ReportViewer_Branch_New_Reports.Visible = true;
        uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
        pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
        domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
        reportURL = "";
        folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
        ReportViewer_Branch_New_Reports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer_Branch_New_Reports.ServerReport.ReportServerUrl = new Uri(reportURL);


         if (url == "Payment_Not_Recieved")
        {

            ReportViewer_Branch_New_Reports.ServerReport.ReportPath = folder + "/" + url;
            ReportViewer_Branch_New_Reports.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_Branch_New_Reports.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Godown_Id";
            reportParameterCollection[0].Values.Add(ddl_Godown.SelectedValue.ToString());
            ReportViewer_Branch_New_Reports.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_Branch_New_Reports.ServerReport.Refresh();
        }
    }

}