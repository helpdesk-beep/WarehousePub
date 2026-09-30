using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

public partial class IssueCenterLevel_Storage_Print_DepositerForm : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    String District = "";
    String Depot = "";
    string Branch = "";
    string Language = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            fillwhrList();
            Panel1.Visible = true;
            
        }
    }
    private void fillwhrList()
    {
        if (Session["Depot_DistID"] != null)
        {
            District = Session["Depot_DistID"].ToString();
            Depot = Session["Depot_DepotID"].ToString();
            Branch = Session["BranchId"].ToString();
            // string query = "select distinct Depositor_whr_id from tbl_storage_Depositor_WHR_Relation JOIN tbl_storage_Stacking_Details on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.Depotid='" + Depot + "' order by tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id desc";

            string query = "select distinct Depositor_whr_id from tbl_storage_Depositor_WHR_Relation JOIN tbl_storage_Stacking_Details on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Branch + "' order by tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id desc";

            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DDLwhr.DataSource = ds.Tables[0];
                DDLwhr.DataTextField = "Depositor_whr_id";
                DDLwhr.DataValueField = "Depositor_whr_id";
                DDLwhr.DataBind();
                DDLwhr.Items.Insert(0, "--Select--");
            }
            else
            {
                DDLwhr.Items.Insert(0, "--Select--");
            }
        }
    }
    private void GetReportdatafromdate()
    {
        ReportViewer_Depot.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string serverFullName = null;

            if (int.Parse(Session["RoleId"].ToString()) == 1)
            {
                District = Session["Depot_DistID"].ToString();
                Depot = Session["Depot_DepotID"].ToString();
            }
            if (Session["lang"].ToString() == "Hindi")
            {
                Language = "1";
            }
            else
            {
                Language = "2";
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

            if (Session["Depot_DepotID"].ToString() != null)
            {
                ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + "rdl_deposit_form";
                ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                ReportViewer_Depot.ShowCredentialPrompts = false;
                ReportParameter[] reportParameterCollection = new ReportParameter[1];
                reportParameterCollection[0] = new ReportParameter();
                reportParameterCollection[0].Name = "whrid";
                if (DDLwhr.SelectedItem.Text != "--Select--")
                {
                    reportParameterCollection[0].Values.Add(DDLwhr.SelectedItem.Text.Trim().ToString());
                }
                else
                {
                    reportParameterCollection[0].Values.Add(txtwhrnu.Text.Trim().ToString());
                }
                ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                ReportViewer_Depot.ServerReport.Refresh();  
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    protected void DDLwhr_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtwhrnu.Text = "";
        GetReportdatafromdate();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        DDLwhr.SelectedIndex = 0;
        GetReportdatafromdate();
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