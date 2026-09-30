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


public partial class IssueCenterLevel_Storage_ReportViewer_Branch_Vacant_Storage : System.Web.UI.Page
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

         
        Session["reporturl"] = "";
        Session["reporturl"] = "Godown_Vacant_Storage_For_Branch";
        //Response.Redirect("ReportViewer_Branch_Vacant_Storage.aspx");
    
        ReportViewer_Branch_Vacant_Storage.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string url = Session["reporturl"].ToString();
        try
        {
            if (!IsPostBack)
            {
                Get_Hired_Type();
                Get_Stored_Type();
                

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

                ReportViewer_Branch_Vacant_Storage.ServerReport.ReportServerUrl = new Uri(servername.ToString());
                ReportViewer_Branch_Vacant_Storage.Visible = true;
                uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
                pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
                domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
                reportURL = "";
                folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
                reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
                ReportViewer_Branch_Vacant_Storage.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer_Branch_Vacant_Storage.ServerReport.ReportServerUrl = new Uri(reportURL);
                



                               
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

    void Get_Hired_Type()
    {
        qry = "select Hired_Type from tbl_MetaData_GODOWN_2018 group by Hired_Type";

        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddl_hidtyp.DataSource = ds.Tables[0];
            ddl_hidtyp.DataTextField = "Hired_Type";
            ddl_hidtyp.DataValueField = "Hired_Type";
            // ddl_hidtyp.DataValueField = "Depositor_Type_Id";
            ddl_hidtyp.DataBind();
            //ddl_hidtyp.Items.Insert(0, "--Se|ect--");
            ddl_hidtyp.Items.Insert(0, new ListItem("Select ALL", "%"));
        }

    }

    void Get_Stored_Type()
    {
        qry = "select Storage_Type from tbl_MetaData_GODOWN_2018 group by Storage_Type";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddl_stgtype.DataSource = ds.Tables[0];
            ddl_stgtype.DataTextField = "Storage_Type";
            ddl_stgtype.DataValueField = "Storage_Type";
            // ddl_hidtyp.DataValueField = "Depositor_Type_Id";
            ddl_stgtype.DataBind();
            ddl_stgtype.Items.Insert(0, new ListItem("Select All", "%"));
            //ddl_stgtype.Items.Insert(0, new ListItem("Select ALL", "0"));
        }
    }
    protected void ddl_hidtyp_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddl_stgtype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btn_viewrpt_Click(object sender, EventArgs e)
    {
        string path = Request.Url.ToString();
        int index = path.IndexOf(":") + 3;
        string path2 = path.Substring(index);
        int index2 = path2.IndexOf("/");
        int index3 = path2.IndexOf("/", index2 + 1);

        string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
        //string servername = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();

        if (index3 > 0)

            serverFullName = path2.Substring(0, index3);
        else
            serverFullName = servername;

        ReportViewer_Branch_Vacant_Storage.ServerReport.ReportServerUrl = new Uri(servername.ToString());
        ReportViewer_Branch_Vacant_Storage.Visible = true;
        uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
        pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
        domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
        reportURL = "";
        folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
        reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
        ReportViewer_Branch_Vacant_Storage.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        ReportViewer_Branch_Vacant_Storage.ServerReport.ReportServerUrl = new Uri(reportURL);

        try
        {
            ReportViewer_Branch_Vacant_Storage.ServerReport.ReportPath = folder + "/" + "Godown_Vacant_Storage_For_Branch";
            ReportViewer_Branch_Vacant_Storage.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            ReportViewer_Branch_Vacant_Storage.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[3];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "BranchID";
            reportParameterCollection[0].Values.Add(Depot.Text);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Hired_Type";
            reportParameterCollection[1].Values.Add(ddl_hidtyp.SelectedValue);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "Storage_Type";
            reportParameterCollection[2].Values.Add(ddl_stgtype.SelectedValue);

            ReportViewer_Branch_Vacant_Storage.ServerReport.SetParameters(reportParameterCollection);
            ReportViewer_Branch_Vacant_Storage.ServerReport.Refresh();
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }

    }
}