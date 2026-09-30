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

public partial class Reports_Branch_Rpt_Stock_Register : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    String District = "";
    String Depot = "";
    string Language = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Printcurrentdate();
            fillDepositor();
          
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
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillwhrno();
    }
    protected void BtnViewReport_Click(object sender, EventArgs e)
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
                    District = Session["Depot_DistID"].ToString();
                    Depot = Session["Depot_DepotID"].ToString();
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

                if (url == "rptStockRegister")
                {

                    ReportViewer_Depot.ShowPrintButton = true;

                    ReportViewer_Depot.ServerReport.ReportPath = folder + "/" + url;
                    ReportViewer_Depot.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
                    ReportViewer_Depot.ShowCredentialPrompts = false;
                    ReportParameter[] reportParameterCollection = new ReportParameter[6];
                    reportParameterCollection[0] = new ReportParameter();
                    reportParameterCollection[0].Name = "DistrictId";
                    reportParameterCollection[0].Values.Add(District);
                    reportParameterCollection[1] = new ReportParameter();
                    reportParameterCollection[1].Name = "depotid";
                    reportParameterCollection[1].Values.Add(Depot);
                    reportParameterCollection[2] = new ReportParameter();
                    reportParameterCollection[2].Name = "Language";
                    reportParameterCollection[2].Values.Add(Language.ToString());
                    reportParameterCollection[3] = new ReportParameter();
                    reportParameterCollection[3].Name = "ParDate";
                    reportParameterCollection[3].Values.Add(txttodate.Text.Trim().ToString());
                    reportParameterCollection[4] = new ReportParameter();
                    reportParameterCollection[4].Name = "Depositor_Whr_id";
                    reportParameterCollection[4].Values.Add(ddlwhr.SelectedValue.ToString());
                    reportParameterCollection[5] = new ReportParameter();
                    reportParameterCollection[5].Name = "Depositor_Name";
                    reportParameterCollection[5].Values.Add(ddldepositor.Text);
                    ReportViewer_Depot.ServerReport.SetParameters(reportParameterCollection);
                    ReportViewer_Depot.ServerReport.Refresh();
                }

            
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  convert(varchar(10),getdate(),103) as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
               
                txttodate.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    private void fillDepositor()
    {
        if (Session["Depot_DistID"] != null)
        {
            District = Session["Depot_DistID"].ToString();
            Depot = Session["Depot_DepotID"].ToString();
            string query = "select distinct sa.Depositor_name from tbl_Storage_Arrival_Stock sa join tbl_Storage_Receipt_Details as sr on sr.StorageReceipt_id=sa.Receipt_id join tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_whr_id=sr.whr_id join tbl_Storage_Stacking_Details as sd on sd.whrid=tbl_storage_Depositor_WHR_Relation.Depositor_whr_id where sa.Receipt_status='Y' and tbl_storage_Depositor_WHR_Relation .District_Id='" + District + "' and tbl_storage_Depositor_WHR_Relation.Depotid='" + Depot + "' order by Depositor_name";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                
                ddldepositor.DataSource = ds.Tables[0];
                ddldepositor.DataTextField = "Depositor_name";
               // ddldepositor.DataValueField = "Godown_ID";
                ddldepositor.DataBind();
                ddldepositor.Items.Insert(0, "--Select--");
            }
            else
            {
                // ddlgodown.Items.Insert(0, "--Select--");
            }
        }
    }

    private void fillwhrno()
    {
        if (ddldepositor.SelectedItem.Text != "--Select--")
        {
            District = Session["Depot_DistID"].ToString();
            Depot = Session["Depot_DepotID"].ToString();
            string query = "select Depositor_Whr_id,whr_no from tbl_storage_Depositor_WHR_Relation where depotid='" + Depot + "' and District_id='" + District + "' and Depositor_Name='" + ddldepositor.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    ddlwhr.DataSource = ds.Tables[0];
                    ddlwhr.DataTextField = "whr_no";
                    ddlwhr.DataValueField = "Depositor_Whr_id";
                    ddlwhr.DataBind();
                    ddlwhr.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddldepositor.Items.Insert(0, "--Select--");
                }
            
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select depositor frist!');", true);
        }
    }
}