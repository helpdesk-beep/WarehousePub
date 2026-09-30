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

public partial class IssueCenterLevel_Storage_ReportViewer_MPSCSC_TillDate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());


    SqlCommand cmd = null;
    SqlTransaction sqltran = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CalendarExtender2.EndDate = DateTime.Now;   //to dissable future  Date
            Printcurrentdate();
        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (toDate.Text == "")
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Please Enter Till date');  </script> ");
        }
        else
        {
            ReportViewer_MPSCSCSDate.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
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
                    Depot.Text = Session["BranchId"].ToString();
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

                ReportViewer_MPSCSCSDate.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_MPSCSCSDate.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_MPSCSCSDate.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_MPSCSCSDate.ServerReport.ReportServerUrl = new Uri(reportURL);

                

                if (url == "TillDate_WheatCropYWiseReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);

                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
               
                else if (url == "TillDate_PaddyCropYWiseReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);


                   
                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
                else if (url == "TillDate_RiceCropYWiseReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);

                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
                else if (url == "TillDate_MaizeCropYWiseReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);

                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
                else if (url == "TillDate_CoarseGrainsCropYWiseReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[2];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);

                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
                else if (url == "TillDate_WheatCropYWise_BranceReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "brabchid";
                    reportParameterCollection[2].Values.Add(Depot.Text);


                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }
                else if (url == "TillDate_RiceCropYWise_BranchReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "brabchid";
                    reportParameterCollection[2].Values.Add(Depot.Text);
                    
                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }

               

                else if (url == "TillDate_PaddyCropYWise_BranchReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "brabchid";
                    reportParameterCollection[2].Values.Add(Depot.Text);


                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }

                else if (url == "TillDate_MaizeCropYWise_BranchReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "brabchid";
                    reportParameterCollection[2].Values.Add(Depot.Text);


                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }

                else if (url == "TillDate_CoarseGrainsCropYWise_BranchReport")
                {
                    ReportViewer_MPSCSCSDate.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_MPSCSCSDate.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_MPSCSCSDate.ShowCredentialPrompts = false;

                    ReportParameter[] reportParameterCollection = new ReportParameter[3];

                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "ToDate";
                    reportParameterCollection[0].Values.Add(getDate_MDY(toDate.Text.Trim()));
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "CropYear";
                    reportParameterCollection[1].Values.Add(ddlcropyear.SelectedItem.Text);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "brabchid";
                    reportParameterCollection[2].Values.Add(Depot.Text);


                    ReportViewer_MPSCSCSDate.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_MPSCSCSDate.ServerReport.Refresh();

                }


            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
    }
    protected void Printcurrentdate()
    {
        String query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            toDate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            toDate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();

        }
    }
    protected string getDate_MDY(string inDate)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));

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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if ((Session["BranchId"]) != null)
        {
            Response.Redirect("~/Reports/Branch/StockBal_ReportForInspection.aspx");
        }
        else
        {
            Response.Redirect("MPSCSC_Reports.aspx");
       }
    }


    }

