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

public partial class Reports_States_ReportViewer_Payment_Response : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {     
        try
        {
            if (!IsPostBack)
            {
                Get_District();
              
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

                ReportViewer_Payment_Response.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_Payment_Response.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Payment_Response.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_Payment_Response.ServerReport.ReportServerUrl = new Uri(reportURL);

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

    void Get_District()
    {
        string sql = "Select [District_Id],[District_Name] from [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DISTRICT] ORDER BY District_Name";
        if (con.State == ConnectionState.Closed) { con.Open(); }
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dt = new DataSet();
       
        da.Fill(dt);
        cmd.ExecuteNonQuery();
        if (con.State == ConnectionState.Closed) { con.Close(); }
        ddl_District.DataSource = dt.Tables[0];
        ddl_District.DataTextField = "District_Name";
        ddl_District.DataValueField = "District_Id";
        ddl_District.DataBind();
        ddl_District.Items.Insert(0, new ListItem("Select ALL", "%"));
        
    }

    void Get_Branch()
    {
        qry = "SELECT DepotID, DepotName FROM  tbl_MetaData_DEPOT WHERE DistrictId = '" + ddl_District.SelectedValue + "' ORDER BY DepotName";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddl_Branch.DataSource = ds.Tables[0];
            ddl_Branch.DataTextField = "DepotName";
            ddl_Branch.DataValueField = "DepotID";
            // ddl_hidtyp.DataValueField = "Depositor_Type_Id";
            ddl_Branch.DataBind();
            ddl_Branch.Items.Insert(0, new ListItem("Select All", "%"));
            //ddl_stgtype.Items.Insert(0, new ListItem("Select ALL", "0"));
        }
    }
    protected void ddl_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_Branch();
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btn_viewrpt_Click(object sender, EventArgs e)
    {

        ReportViewer_Payment_Response.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
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
        ReportViewer_Payment_Response.ServerReport.ReportServerUrl = new Uri(servername.ToString());
        ReportViewer_Payment_Response.Visible = true;
        uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
        pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
        domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
        reportURL = "";
        folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
        ReportViewer_Payment_Response.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer_Payment_Response.ServerReport.ReportServerUrl = new Uri(reportURL);

       
            if (url == "PaymentNotReceived")
            {
                ReportViewer_Payment_Response.ServerReport.ReportPath = folder + "/" + url;
                ReportViewer_Payment_Response.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportViewer_Payment_Response.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "DistrictID";
                reportParameterCollection[0].Values.Add(ddl_District.SelectedValue.ToString());
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "BranchID";
                reportParameterCollection[1].Values.Add(ddl_Branch.SelectedValue.ToString());

                ReportViewer_Payment_Response.ServerReport.SetParameters(reportParameterCollection);
                ReportViewer_Payment_Response.ServerReport.Refresh();
            }
            else if(url == "PaymentRecieved")
            {
                ReportViewer_Payment_Response.ServerReport.ReportPath = folder + "/" + url;
                ReportViewer_Payment_Response.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportViewer_Payment_Response.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[2];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "DistrictID";
                reportParameterCollection[0].Values.Add(ddl_District.SelectedValue.ToString());
                reportParameterCollection[1] = new ReportParameter();
                reportParameterCollection[1].Name = "BranchID";
                reportParameterCollection[1].Values.Add(ddl_Branch.SelectedValue.ToString());

                ReportViewer_Payment_Response.ServerReport.SetParameters(reportParameterCollection);
                ReportViewer_Payment_Response.ServerReport.Refresh();

            }       

    }
}