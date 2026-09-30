using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_States_Andriod_ReceivingStockProc_Para : System.Web.UI.Page
{
     public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
     string Godown_Id = "";
     string qry = "";
     SqlCommand cmd = null;
     SqlDataAdapter da = null;
     string BranchID = "";
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
        {
            Label1.Visible = false;
            FillBranchDD();
        }
    }

    private void FillBranchDD()
    {
        divGridGodown.Visible = true;
        qry = "select [BranchId],[DepotName] From [tbl_MetaData_DEPOT] order by [DepotName]";
        con.Open();
        cmd = new SqlCommand(qry, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
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

    private void GetReportdata(string Godown_Id)
    {
        Label1.Visible = false;
        divGridGodown.Visible = false;
        rptDiv.Visible = true;
       
        RV_ARStockProc_Para.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        //string url = Session["reporturl"].ToString();
        try
        {
            string uname = "";
            string pwd = "";
            string domain = "";
            string reportURL = "";
            string folder = "";
            string Language = "";
            string serverFullName = null;
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

            RV_ARStockProc_Para.ServerReport.ReportServerUrl = new Uri(servername.ToString());
            RV_ARStockProc_Para.Visible = true;
            uname = ConfigurationManager.ConnectionStrings["uname"].ProviderName.ToString();
            pwd = ConfigurationManager.ConnectionStrings["psw"].ProviderName.ToString();
            domain = ConfigurationManager.ConnectionStrings["domain"].ProviderName.ToString();
            reportURL = "";
            folder = ConfigurationManager.ConnectionStrings["rptfolder"].ProviderName.ToString();
            reportURL = ConfigurationManager.ConnectionStrings["rpturl"].ProviderName.ToString();
            RV_ARStockProc_Para.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            RV_ARStockProc_Para.ServerReport.ReportServerUrl = new Uri(reportURL);
            RV_ARStockProc_Para.ServerReport.ReportPath = folder + "/" + "Rpt_ARSProc_GodownWise";
           // RV_ARStockProc_Para.ServerReport.ReportPath = folder + "Rpt_ARSProc_GodownWise";
            RV_ARStockProc_Para.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials();
            RV_ARStockProc_Para.ShowCredentialPrompts = false;
            ReportParameter[] reportParameterCollection = new ReportParameter[1];
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Language";
            reportParameterCollection[0].Values.Add(Language.ToString());
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "Godown_Id";
            reportParameterCollection[0].Values.Add(Godown_Id);
            RV_ARStockProc_Para.ServerReport.SetParameters(reportParameterCollection);
            RV_ARStockProc_Para.ServerReport.Refresh();
        }
        catch (Exception ex)
        {
            //  Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
        }
    }
    private void FillGodownGrid(string BranchID)
    {
        if (!string.IsNullOrEmpty(BranchID))
        {
            qry = "select [Godown_ID],[Godown_Name],[Hired_Type],[Storage_Type],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[Latitude],[Longitude],[Godown_Scientific_Capacity]";
            qry = qry + "from [tbl_MetaData_GODOWN] where [BranchID]='" + BranchID + "'";
            con.Open();
            cmd = new SqlCommand(qry, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGodown.DataSource = ds.Tables[0];
                gvGodown.DataBind();
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedValue == "0")
        {
            Label1.Visible = false;
            divGridGodown.Visible = false;
            rptDiv.Visible = false;
        }
    }
    protected void chbGodown_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkRowgd = null;
    
        foreach (GridViewRow row in gvGodown.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                chkRowgd = (row.Cells[0].FindControl("chbGodown") as CheckBox);
                if (chkRowgd.Checked)
                {
                    Godown_Id = row.Cells[1].Text;
                    chkRowgd.Checked = false;
                    break;
                }
            }
        }
        if (!string.IsNullOrEmpty(Godown_Id))
        {
            GetReportdata(Godown_Id);
        }
        else {
            string display = "No Values Available for Selected Godown";
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
        }
    }
    protected void btnGodownSearch_Click(object sender, EventArgs e)
    {
       
        if (ddlBranch.SelectedValue == "0")
        {
            Label1.Visible = false;
            divGridGodown.Visible = false;
            rptDiv.Visible = false;
        }
        else if(!string.IsNullOrEmpty(ddlBranch.SelectedValue))
        {
            Label1.Visible = true;
            divGridGodown.Visible = true;
            rptDiv.Visible = false;
            BranchID = ddlBranch.SelectedValue;
            FillGodownGrid(BranchID);
        }
    }
}
