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

public partial class RegionBandaranSulkDate : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    string Language = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetTodaydate();
            GetRegion();
            // filldepot();
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
                
                txttodate.Text = ds.Tables[0].Rows[0]["TodayDate"].ToString();

            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void GetRegion()
    {
        try
        {
            string qry = "SELECT [Region_Id],[region],[regionh] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_Region]";
            SqlCommand cmd = new SqlCommand(qry, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                DropDownList1.DataSource = ds.Tables[0];

                DropDownList1.DataTextField = "region";
                DropDownList1.DataValueField = "Region_Id";
                DropDownList1.DataBind();

            }
        }
        catch (Exception)
        {
            //////
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
    protected void BtnViewReport_Click(object sender, EventArgs e)
    {
        if (Session["RoleId"].ToString() != "2")
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
                string datestring = txttodate.Text;
                string[] tempsplit = datestring.Split('/');
                string joinstring = "/";
                string newdatefrom = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];
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

                if (url == "Bhugtanrashidatewise")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(DropDownList1.SelectedValue.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "date";
                    reportParameterCollection[1].Values.Add(newdatefrom);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }
                if (url == "BhugtanRashiRegion_Progresive")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(DropDownList1.SelectedValue.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "date";
                    reportParameterCollection[1].Values.Add(newdatefrom);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

                if (url == "BhugtanrashiMonday")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(DropDownList1.SelectedValue.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "date";
                    reportParameterCollection[1].Values.Add(newdatefrom);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }

        }
        else
        {
            ReportViewer_Depot.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            string url = "BhugtanrashiMonday";
            try
            {
                string uname = "";
                string pwd = "";
                string domain = "";
                string reportURL = "";
                string folder = "";
                string Language = "";
                string serverFullName = null;
                string datestring = txttodate.Text;
                string[] tempsplit = datestring.Split('/');
                string joinstring = "/";
                string newdatefrom = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];
               
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

              

                if (url == "BhugtanrashiMonday")
                {

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "Region";
                    reportParameterCollection[0].Values.Add(DropDownList1.SelectedValue.ToString());
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "date";
                    reportParameterCollection[1].Values.Add(newdatefrom);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }


        }
    }
}